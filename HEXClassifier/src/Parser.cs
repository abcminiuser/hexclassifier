using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    interface Parser
    {
        IEnumerable<SpanClassification> Parse(ITextSnapshotLine line);
        Dictionary<TokenEntryTypes, string> GetClassifierTypeNames();
    }
}
