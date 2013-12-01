using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class HEXCodeClassifier : IClassifier
    {
        private static readonly Dictionary<HEXParser.HEXEntryTypes, string> mClassifierTypeNames = new Dictionary<HEXParser.HEXEntryTypes, string>() {
            { HEXParser.HEXEntryTypes.START_CODE, "hex.startcode" },
            { HEXParser.HEXEntryTypes.BYTE_COUNT, "hex.bytecount" },
            { HEXParser.HEXEntryTypes.ADDRESS, "hex.address" },
            { HEXParser.HEXEntryTypes.RECORD_TYPE, "hex.recordtype" },
            { HEXParser.HEXEntryTypes.DATA, "hex.data" },
            { HEXParser.HEXEntryTypes.CHECKSUM, "hex.checksum" },
        };

        private readonly ITextBuffer mTextBuffer;
        private readonly IClassificationTypeRegistryService mClassificationTypeRegistry;
        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067

        public HEXCodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry)
        {
            mTextBuffer = buffer;
            mClassificationTypeRegistry = classifierTypeRegistry;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            classifications.Clear();

            if (span.Length == 0)
                return classifications;

            ITextSnapshotLine line = span.Start.GetContainingLine();

            foreach (Tuple<HEXParser.HEXEntryTypes, SnapshotSpan> segment in HEXParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
