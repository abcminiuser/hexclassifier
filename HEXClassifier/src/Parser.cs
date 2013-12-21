using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    interface Parser
    {
        public static IEnumerable<Tuple<TokenEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line);
    }
}
