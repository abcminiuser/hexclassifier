using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    interface Parser
    {
        IEnumerable<Tuple<TokenEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line);
        Dictionary<TokenEntryTypes, string> GetClassifierTypeNames();
    }
}
