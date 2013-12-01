using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using System.Globalization;

namespace FourWalledCubicle.HEXClassifier
{
    internal abstract class HEXParser
    {
        public enum HEXEntryTypes
        {
            START_CODE,
            BYTE_COUNT,
            ADDRESS,
            RECORD_TYPE,
            DATA,
            CHECKSUM,
        };

        public static IEnumerable<Tuple<HEXEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (text[0] != ':')
                yield break;

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.START_CODE, new SnapshotSpan(line.Snapshot, line.Start, 1));

            if (text.Length < 3)
                yield break;

            int byteCount = 0;
            if (int.TryParse(text.Substring(1, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out byteCount) == false)
                yield break;

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.BYTE_COUNT, new SnapshotSpan(line.Snapshot, line.Start + 1, 2));

            if (text.Length < 7)
                yield break;

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.ADDRESS, new SnapshotSpan(line.Snapshot, line.Start + 3, 4));

            if (text.Length < 9)
                yield break;

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.RECORD_TYPE, new SnapshotSpan(line.Snapshot, line.Start + 7, 2));

            byteCount = Math.Min(text.Length - 9, byteCount * 2);

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.DATA, new SnapshotSpan(line.Snapshot, line.Start + 9, byteCount));
        
            if (text.Length < (11 + byteCount))
                yield break;

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                 HEXEntryTypes.CHECKSUM, new SnapshotSpan(line.Snapshot, line.Start + 9 + byteCount, 2));
        }
    }
}
