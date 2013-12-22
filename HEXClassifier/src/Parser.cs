using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    enum TokenEntryTypes
    {
        START_CODE,
        BYTE_COUNT,
        ADDRESS,
        RECORD_TYPE,
        DATA,
        CHECKSUM,
        CHECKSUM_BAD,
    };

    struct SpanClassification
    {
        public TokenEntryTypes Entry;
        public SnapshotSpan Span;
    }

    interface Parser
    {
        IEnumerable<SpanClassification> Parse(ITextSnapshotLine line);
        Dictionary<TokenEntryTypes, string> GetClassifierTypeNames();
    }
}
