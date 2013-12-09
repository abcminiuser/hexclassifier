using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class SRECCodeClassifier : IClassifier
    {
        private static readonly Dictionary<SRECParser.SRECEntryTypes, string> mClassifierTypeNames = new Dictionary<SRECParser.SRECEntryTypes, string>() {
            { SRECParser.SRECEntryTypes.START_CODE, "srec.startcode" },
            { SRECParser.SRECEntryTypes.BYTE_COUNT, "srec.bytecount" },
            { SRECParser.SRECEntryTypes.ADDRESS, "srec.address" },
            { SRECParser.SRECEntryTypes.RECORD_TYPE, "srec.recordtype" },
            { SRECParser.SRECEntryTypes.DATA, "srec.data" },
            { SRECParser.SRECEntryTypes.CHECKSUM, "srec.checksum" },
            { SRECParser.SRECEntryTypes.CHECKSUM_BAD, "srec.checksum.bad" },
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

            foreach (Tuple<SRECParser.SRECEntryTypes, SnapshotSpan> segment in SRECParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
