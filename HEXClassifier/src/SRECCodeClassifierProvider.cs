using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("srec")]
    internal class SRECClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService mClassificationRegistry { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            Func<IClassifier> classifierFunc =
                () => new SRECCodeClassifier(buffer, mClassificationRegistry) as IClassifier;
            return buffer.Properties.GetOrCreateSingletonProperty<IClassifier>(classifierFunc);
        }
    }
}
