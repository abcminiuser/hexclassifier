using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class CodeClassifier : IClassifier
    {
        private readonly ITextBuffer mTextBuffer;
        private readonly IClassificationTypeRegistryService mClassificationTypeRegistry;
        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();
        private readonly Parser mParser;

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067

        public CodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry, Parser parser)
        {
            mTextBuffer = buffer;
            mClassificationTypeRegistry = classifierTypeRegistry;
            mParser = parser;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            classifications.Clear();

            if (span.Length == 0)
                return classifications;

            ITextSnapshotLine line = span.Start.GetContainingLine();

            Dictionary<TokenEntryTypes, IClassificationType> classificationCache = new Dictionary<TokenEntryTypes,IClassificationType>();

            foreach (SpanClassification classification in mParser.Parse(line))
            {
                if (classificationCache.ContainsKey(classification.Entry) == false)
                    classificationCache[classification.Entry] = mClassificationTypeRegistry.GetClassificationType(mParser.GetClassifierTypeNames()[classification.Entry]);

                IClassificationType classificationType = classificationCache[classification.Entry];
                classifications.Add(new ClassificationSpan(classification.Span, classificationType));
            }

            return classifications;
        }
    }
}
