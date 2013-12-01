using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("hex")]
    internal class HEXClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService mClassificationRegistry { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            Func<IClassifier> classifierFunc =
                () => new HEXCodeClassifier(buffer, mClassificationRegistry) as IClassifier;
            return buffer.Properties.GetOrCreateSingletonProperty<IClassifier>(classifierFunc);
        }
    }
}
