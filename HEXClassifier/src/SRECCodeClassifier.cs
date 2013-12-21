using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class SRECCodeClassifier : IClassifier
    {
        private static readonly Dictionary<TokenEntryTypes, string> mClassifierTypeNames = new Dictionary<TokenEntryTypes, string>() {
            { TokenEntryTypes.START_CODE, "srec.startcode" },
            { TokenEntryTypes.BYTE_COUNT, "srec.bytecount" },
            { TokenEntryTypes.ADDRESS, "srec.address" },
            { TokenEntryTypes.RECORD_TYPE, "srec.recordtype" },
            { TokenEntryTypes.DATA, "srec.data" },
            { TokenEntryTypes.CHECKSUM, "srec.checksum" },
            { TokenEntryTypes.CHECKSUM_BAD, "srec.checksum.bad" },
        };

        private readonly ITextBuffer mTextBuffer;
        private readonly IClassificationTypeRegistryService mClassificationTypeRegistry;
        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067

        public SRECCodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry)
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

            foreach (Tuple<TokenEntryTypes, SnapshotSpan> segment in SRECParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
