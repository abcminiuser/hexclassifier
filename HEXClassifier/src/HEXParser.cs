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
            CHECKSUM_BAD,
        };

        public static IEnumerable<Tuple<HEXEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (text.Length < 1)
                yield break;

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

            int calculatedChecksum = CalculateChecksum(text);
            int fileChecksum = -1;
            int.TryParse(text.Substring(text.Length - 2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out fileChecksum);

            yield return new Tuple<HEXEntryTypes, SnapshotSpan>(
                                    (fileChecksum == calculatedChecksum) ? HEXEntryTypes.CHECKSUM : HEXEntryTypes.CHECKSUM_BAD,
                                    new SnapshotSpan(line.Snapshot, line.Start + 9 + byteCount, 2));
        }

        private static int CalculateChecksum(string textLine)
        {
            string checksumText = textLine.Substring(1, textLine.Length - 3);

            if (checksumText.Length % 2 != 0)
                return -1;

            int temp = 0;
            for (int i = 0; i < checksumText.Length; i += 2)
            {
                int bytePair = 0;
                if (int.TryParse(checksumText.Substring(i, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out bytePair) == false)
                    return -1;
                temp += bytePair;
            }

            return (256 - (temp & 0xFF) ) & 0xFF;
        }
    }
}
