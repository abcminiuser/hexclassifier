using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    internal abstract class SRECParser : Parser
    {
        public static IEnumerable<Tuple<TokenEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (text.Length < 1)
                yield break;

            if (text[0] != 'S')
                yield break;

            yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                 TokenEntryTypes.START_CODE, new SnapshotSpan(line.Snapshot, line.Start, 1));

            if (text.Length < 3)
                yield break;

            int recordType = 0;
            if (int.TryParse(text.Substring(1, 1), System.Globalization.NumberStyles.Integer, CultureInfo.CurrentCulture, out recordType) == false)
                yield break;
            yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                TokenEntryTypes.RECORD_TYPE, new SnapshotSpan(line.Snapshot, line.Start + 1, 1));

            if (text.Length < 5)
                yield break;

            int byteCount = 0;
            if (int.TryParse(text.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out byteCount) == false)
                yield break;

            // Unknown records
            if (recordType > 9 || recordType < 0 || recordType == 4)
                yield break;

            yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                TokenEntryTypes.BYTE_COUNT, new SnapshotSpan(line.Snapshot, line.Start + 2, 2));

            int addressBytes = 0;
            switch (recordType)
            {
                    // 2 Address bytes
                case 0:
                case 1:
                case 5:
                case 9:
                    addressBytes = 4;
                    break;
                    // 3 Address bytes
                case 2:
                case 8:
                    addressBytes = 6;
                    break;
                    // 4 Address bytes
                case 3:
                case 7:
                    addressBytes = 8;
                    break;
            }

            int address = 0;
            if (int.TryParse(text.Substring(4, addressBytes), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out address) == false)
                yield break;

            if (text.Length < 5 + addressBytes)
                yield break;

            yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                TokenEntryTypes.ADDRESS, new SnapshotSpan(line.Snapshot, line.Start + 4, addressBytes));

            // Check if we expect data in this record
            if (new List<int> { 0, 1, 2, 3 }.Contains(recordType))
            {
                int dataLength = (byteCount * 2) - addressBytes - 2;
                if (text.Length < (5 + dataLength))
                    yield break;

                yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                    TokenEntryTypes.DATA, new SnapshotSpan(line.Snapshot, line.Start + 4 + addressBytes, dataLength));
            }

            int calculatedChecksum = CalculateChecksum(text);
            int fileChecksum = -1;
            int.TryParse(text.Substring(text.Length - 2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out fileChecksum);

            yield return new Tuple<TokenEntryTypes, SnapshotSpan>(
                                (fileChecksum == calculatedChecksum) ? SRECEntryTypes.CHECKSUM : SRECEntryTypes.CHECKSUM_BAD,
                                new SnapshotSpan(line.Snapshot, line.End - 2, 2));
        }

        private static int CalculateChecksum(string textLine)
        {
            string checksumText = textLine.Substring(2, textLine.Length - 4);
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

            return (~(temp & 0xFF)) & 0xFF;
        }
    }
}
