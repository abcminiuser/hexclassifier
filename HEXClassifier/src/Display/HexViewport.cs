using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Threading;

namespace FourWalledCubicle.HEXClassifier
{
    [Name(HexViewport.MarginName)]
    class HexViewport : Canvas, IWpfTextViewMargin
    {
        public const string MarginName = "Hex Information";

        private readonly IWpfTextView _textView;
        private readonly IClassifier _classifier;
        private readonly IClassificationFormatMap _classificationFormatMap;

        private bool _isDisposed = false;

        int? _currentStartLine;
        int? _currentEndLine;

        public HexViewport(IWpfTextView textView, InformationMarginFactory factory)
        {
            _textView = textView;
            _classifier = factory.ClassifierAggregatorService.GetClassifier(textView.TextBuffer);
            _classificationFormatMap = factory.ClassificationMapService.GetClassificationFormatMap(textView);

            this.ClipToBounds = true;

            UpdateWidth();

            _textView.LayoutChanged += (s, e) => { UpdateDisplay(false); };
            _textView.TextBuffer.ChangedLowPriority += (s, e) => { UpdateDisplay(true); };
            _textView.ViewportWidthChanged += (s, e) => { UpdateWidth(); };
        }

        private void UpdateWidth()
        {
            this.Width = _textView.ViewportWidth / 2;
        }

        private void UpdateDisplay(bool forcedUpdate)
        {
            int startLine = _textView.TextViewLines.FirstVisibleLine.Start.GetContainingLine().LineNumber;
            int endLine = _textView.TextViewLines.LastVisibleLine.End.GetContainingLine().LineNumber;

            if (forcedUpdate || (_currentStartLine != startLine) || (_currentEndLine != endLine))
            {
                _currentStartLine = startLine;
                _currentEndLine = endLine;

                this.Dispatcher.BeginInvoke(new Action(RenderText), DispatcherPriority.Render);
            }
        }

        private void RenderText()
        {
            if (_textView.IsClosed)
                return;

            this.Children.Clear();

            Line deliminator = new Line();
            deliminator.Stroke = Brushes.DarkGray;
            deliminator.StrokeThickness = 1;
            deliminator.X1 = this.Width - 1;
            deliminator.Y1 = 0;
            deliminator.X2 = deliminator.X1;
            deliminator.Y2 = _textView.ViewportHeight;
            this.Children.Add(deliminator);

            for (int currLine = (int)_currentStartLine; currLine < _currentEndLine; currLine++)
            {
                ITextSnapshotLine line = _textView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(currLine);

                string lineDataASCII = string.Empty;

                IList<ClassificationSpan> lineClassifications = _classifier.GetClassificationSpans(line.Extent);
                foreach (ClassificationSpan c in lineClassifications)
                {

                    if ((c.ClassificationType.Classification != HEXClassificationType.ClassificationNames.Data) &&
                        (c.ClassificationType.Classification != SRECClassificationType.ClassificationNames.Data))
                    {
                        continue;
                    }

                    string lineData = c.Span.GetText();
                    for (int dataPair = 0; dataPair < lineData.Length; dataPair += 2)
                    {
                        try
                        {
                            string currDataHex = lineData.Substring(dataPair, 2);

                            int currDataInt = 0;
                            int.TryParse(currDataHex, System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out currDataInt);

                            char currDataChar = (char)currDataInt;
                            lineDataASCII += string.Format(" {0}", Char.IsControl(currDataChar) ? '.' : currDataChar);
                        }
                        catch { }
                    }
                }

                TextBlock lineText = new TextBlock();
                lineText.FontFamily = _classificationFormatMap.DefaultTextProperties.Typeface.FontFamily;
                lineText.FontSize = _classificationFormatMap.DefaultTextProperties.FontRenderingEmSize;
                if (lineDataASCII == string.Empty)
                {
                    lineText.Foreground = Brushes.DarkGray;
                    lineText.Text = "No data for this line";
                    lineText.FontStyle = FontStyles.Italic;
                }
                else
                {
                    lineText.Foreground = Brushes.Black;
                    lineText.Text = lineDataASCII;
                }

                Canvas.SetLeft(lineText, 0);
                Canvas.SetTop(lineText, (currLine - (int)_currentStartLine) * _textView.LineHeight);
                this.Children.Add(lineText);
            }
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(MarginName);
        }

        public FrameworkElement VisualElement
        {
            get
            {
                ThrowIfDisposed();
                return this;
            }
        }

        public double MarginSize
        {
            get
            {
                ThrowIfDisposed();
                return this.Width;
            }
        }

        public bool Enabled
        {
            get
            {
                ThrowIfDisposed();
                return true;
            }
        }

        public ITextViewMargin GetTextViewMargin(string marginName)
        {
            return (marginName == HexViewport.MarginName) ? (IWpfTextViewMargin)this : null;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }
        }
    }
}
