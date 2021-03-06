﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(IWpfTextViewMarginProvider))]
    [Name(HEXMargin.MarginName)]
    [Order(After = PredefinedMarginNames.Spacer, Before = PredefinedMarginNames.Outlining)]
    [MarginContainer(PredefinedMarginNames.LeftSelection)]
    [ContentType("hex")]
    [ContentType("srec")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal sealed class HEXMarginFactory : IWpfTextViewMarginProvider
    {
        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost textViewHost, IWpfTextViewMargin containerMargin)
        {
            return new HEXMargin(textViewHost.TextView, this);
        }

        [Import]
        internal IClassifierAggregatorService ClassifierAggregatorService { get; set; }

        [Import]
        internal IClassificationFormatMapService ClassificationMapService { get; private set; }
    }
}
