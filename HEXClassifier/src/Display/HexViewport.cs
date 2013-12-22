using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Classification;
using System.Windows;

namespace FourWalledCubicle.HEXClassifier
{
    [Name(HexViewport.MarginName)]
    class HexViewport : IWpfTextViewMargin
    {
        public const string MarginName = "Hex Information";

        private readonly IWpfTextView _textView;
        private readonly IClassificationFormatMap _classificationFormatMap;
        private readonly IEditorFormatMap _editorFormatMap;
        private readonly FrameworkElement _element;

        private bool _isDisposed = false;


        public HexViewport(IWpfTextView textView, InformationMarginFactory factory)
        {
            _textView = textView;
            _classificationFormatMap = factory.ClassificationMapService.GetClassificationFormatMap(textView);
            _editorFormatMap = factory.EditorFormatSerivce.GetEditorFormatMap(textView);

            _editorFormatMap.FormatMappingChanged += HandleFormatMappingChanged;
            _textView.Closed += (sender, e) => { _editorFormatMap.FormatMappingChanged -= HandleFormatMappingChanged; };
            textView.Options.OptionChanged += HandleOptionsChanged;

            /* Test visual element */
            _element = new RichTextBox();
            _element.Width = 200;
        }

        private void ThrowIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(MarginName);
        }

        private void HandleFormatMappingChanged(object sender, FormatItemsEventArgs e)
        {
            if (_isDisposed)
                return;
        }

        private void HandleOptionsChanged(object sender, EditorOptionChangedEventArgs e)
        {
            if (!_isDisposed)
                return;
        }

        #region IWpfTextViewMargin Members

        public FrameworkElement VisualElement
        {
            get
            {
                ThrowIfDisposed();
                return _element;
            }
        }

        #endregion

        #region ITextViewMargin Members

        public double MarginSize
        {
            get
            {
                ThrowIfDisposed();
                return _element.ActualHeight;
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
        #endregion
    }
}
