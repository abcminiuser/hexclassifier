using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Name(HexViewport.MarginName)]
    class HexViewport : IWpfTextViewMargin
    {
        public const string MarginName = "Hex Information";

        private readonly IWpfTextView m_textView;
        private readonly IVerticalScrollBar m_scrollBar;
        private readonly IClassificationFormatMap m_classificationFormatMap;
        private readonly IEditorFormatMap m_editorFormatMap;
        private readonly Canvas m_canvasElement;

        private bool m_isDisposed = false;

        public HexViewport(IWpfTextView textView, InformationMarginFactory factory)
        {
            m_textView = textView;
            m_classificationFormatMap = factory.ClassificationMapService.GetClassificationFormatMap(textView);
            m_editorFormatMap = factory.EditorFormatSerivce.GetEditorFormatMap(textView);

            m_editorFormatMap.FormatMappingChanged += HandleFormatMappingChanged;
            m_textView.Closed += (sender, e) => { m_editorFormatMap.FormatMappingChanged -= HandleFormatMappingChanged; };
            textView.Options.OptionChanged += HandleOptionsChanged;

            m_canvasElement = new Canvas();
            m_canvasElement.Width = 250;

            RenderText();

            m_textView.LayoutChanged += (s, e) => { RenderText(); };
        }

        private void RenderText()
        {
            int sl = m_textView.TextViewLines.FirstVisibleLine.Start.GetContainingLine().LineNumber;
            int el = m_textView.TextViewLines.LastVisibleLine.End.GetContainingLine().LineNumber;

            m_canvasElement.Children.Clear();

            for (int i = sl; i < el; i++)
            {
                ITextSnapshotLine line = m_textView.TextBuffer.CurrentSnapshot.GetLineFromLineNumber(i);

                TextBlock lineText = new TextBlock();

                lineText.Text = line.GetText();
                lineText.Foreground = new SolidColorBrush(Colors.Black);

                Canvas.SetLeft(lineText, 0);
                Canvas.SetTop(lineText, (i - sl) * lineText.FontSize * lineText.FontFamily.LineSpacing + 2);
                m_canvasElement.Children.Add(lineText);
            }
        }

        private void ThrowIfDisposed()
        {
            if (m_isDisposed)
                throw new ObjectDisposedException(MarginName);
        }

        private void HandleFormatMappingChanged(object sender, FormatItemsEventArgs e)
        {
            if (m_isDisposed)
                return;
        }

        private void HandleOptionsChanged(object sender, EditorOptionChangedEventArgs e)
        {
            if (!m_isDisposed)
                return;
        }

        #region IWpfTextViewMargin Members

        public FrameworkElement VisualElement
        {
            get
            {
                ThrowIfDisposed();
                return m_canvasElement;
            }
        }

        #endregion

        #region ITextViewMargin Members

        public double MarginSize
        {
            get
            {
                ThrowIfDisposed();
                return m_canvasElement.ActualHeight;
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
        #endregion
    }
}
