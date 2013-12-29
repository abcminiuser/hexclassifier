using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [Name(HexViewport.MarginName)]
    [Order(After = PredefinedMarginNames.Spacer, Before = PredefinedMarginNames.Outlining)]
    [MarginContainer(PredefinedMarginNames.LeftSelection)]
    [ContentType("hex")]
    [ContentType("srec")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal sealed class InformationMarginFactory : IWpfTextViewMarginProvider
    {
        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost textViewHost, IWpfTextViewMargin containerMargin)
        {
            return new HexViewport(textViewHost.TextView, this, ClassifierAggregatorService);
        }

        [Import]
        internal IClassifierAggregatorService ClassifierAggregatorService { get; set; }

        [Import]
        internal ITextDocumentFactoryService TextDocumentFactoryService { get; private set; }

        [Import]
        internal IClassificationFormatMapService ClassificationMapService { get; private set; }

        [Import]
        internal IEditorFormatMapService EditorFormatSerivce { get; private set; }
    }
}
