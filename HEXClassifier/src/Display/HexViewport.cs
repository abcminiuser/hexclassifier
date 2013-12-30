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

        private readonly IWpfTextView m_textView;
        private readonly IClassifier m_classifier;
        private readonly IClassificationFormatMap m_classificationFormatMap;

        private bool m_isDisposed = false;

        int? m_currentStartLine;
        int? m_currentEndLine;

        public HexViewport(IWpfTextView textView, InformationMarginFactory factory)
        {
            m_textView = textView;
            m_classifier = factory.ClassifierAggregatorService.GetClassifier(textView.TextBuffer);
            m_classificationFormatMap = factory.ClassificationMapService.GetClassificationFormatMap(textView);

            this.ClipToBounds = true;

            UpdateWidth();

            m_textView.LayoutChanged += (s, e) => { UpdateDisplay(false); };
            m_textView.TextBuffer.ChangedLowPriority += (s, e) => { UpdateDisplay(true); };
            m_textView.ViewportWidthChanged += (s, e) => { UpdateWidth(); };
        }

        private void UpdateWidth()
        {
            this.Width = m_textView.ViewportWidth / 2;
        }

        private void UpdateDisplay(bool forcedUpdate)
        {
            int startLine = m_textView.TextViewLines.FirstVisibleLine.Start.GetContainingLine().LineNumber;
            int endLine = m_textView.TextViewLines.LastVisibleLine.End.GetContainingLine().LineNumber;

            if (forcedUpdate || (m_currentStartLine != startLine) || (m_currentEndLine != endLine))
            {
                m_currentStartLine = startLine;
                m_currentEndLine = endLine;

                this.Dispatcher.BeginInvoke(new Action(RenderText), DispatcherPriority.Render);
            }
        }

        private void RenderText()
        {
            if (m_textView.IsClosed)
                return;

            this.Children.Clear();

            Line deliminator = new Line();
            deliminator.Stroke = Brushes.DarkGray;
            deliminator.StrokeThickness = 1;
            deliminator.X1 = this.Width - 1;
            deliminator.Y1 = 0;
            deliminator.X2 = deliminator.X1;
            deliminator.Y2 = m_textView.ViewportHeight;
            this.Children.Add(deliminator);

            for (int currLine = (int)m_currentStartLine; currLine < m_currentEndLine; currLine++)
            {
                ITextSnapshotLine line = m_textView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(currLine);

                string lineDataASCII = string.Empty;

                IList<ClassificationSpan> lineClassifications = m_classifier.GetClassificationSpans(line.Extent);
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
                lineText.FontFamily = m_classificationFormatMap.DefaultTextProperties.Typeface.FontFamily;
                lineText.FontSize = m_classificationFormatMap.DefaultTextProperties.FontRenderingEmSize;
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
                Canvas.SetTop(lineText, (currLine - (int)m_currentStartLine) * m_textView.LineHeight);
                this.Children.Add(lineText);
            }
        }

        private void ThrowIfDisposed()
        {
            if (m_isDisposed)
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
            if (!m_isDisposed)
            {
                GC.SuppressFinalize(this);
                m_isDisposed = true;
            }
        }
    }
}
