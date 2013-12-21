using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class HEXCodeClassifier : IClassifier
    {
        private static readonly Dictionary<TokenEntryTypes, string> mClassifierTypeNames = new Dictionary<TokenEntryTypes, string>() {
            { TokenEntryTypes.START_CODE, "hex.startcode" },
            { TokenEntryTypes.BYTE_COUNT, "hex.bytecount" },
            { TokenEntryTypes.ADDRESS, "hex.address" },
            { TokenEntryTypes.RECORD_TYPE, "hex.recordtype" },
            { TokenEntryTypes.DATA, "hex.data" },
            { TokenEntryTypes.CHECKSUM, "hex.checksum" },
            { TokenEntryTypes.CHECKSUM_BAD, "hex.checksum.bad" },
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

            foreach (Tuple<TokenEntryTypes, SnapshotSpan> segment in HEXParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
