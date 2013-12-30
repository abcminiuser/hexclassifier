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
        private readonly ITextBuffer _textBuffer;
        private readonly IClassificationTypeRegistryService _classificationTypeRegistry;
        private readonly List<ClassificationSpan> _classifications = new List<ClassificationSpan>();
        private readonly Parser _parser;

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067

        public CodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry, Parser parser)
        {
            _textBuffer = buffer;
            _classificationTypeRegistry = classifierTypeRegistry;
            _parser = parser;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            _classifications.Clear();

            if (span.Length == 0)
                return _classifications;

            ITextSnapshotLine line = span.Start.GetContainingLine();

            Dictionary<TokenEntryTypes, IClassificationType> classificationCache = new Dictionary<TokenEntryTypes,IClassificationType>();

            foreach (SpanClassification classification in _parser.Parse(line))
            {
                if (classificationCache.ContainsKey(classification.Entry) == false)
                    classificationCache[classification.Entry] = _classificationTypeRegistry.GetClassificationType(_parser.GetClassifierTypeNames()[classification.Entry]);

                IClassificationType classificationType = classificationCache[classification.Entry];
                _classifications.Add(new ClassificationSpan(classification.Span, classificationType));
            }

            return _classifications;
        }
    }
}
