using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.HEXClassifier
{
    internal sealed class HEXParser : Parser
    {
        public IEnumerable<SpanClassification> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (text.Length < 1)
                yield break;

            if (text[0] != ':')
                yield break;

            yield return new SpanClassification
            {
                Entry = TokenEntryTypes.START_CODE,
                Span = new SnapshotSpan(line.Snapshot, line.Start, 1)
            };

            if (text.Length < 3)
                yield break;

            int byteCount = 0;
            if (int.TryParse(text.Substring(1, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out byteCount) == false)
                yield break;

            yield return new SpanClassification
            {
                Entry = TokenEntryTypes.BYTE_COUNT,
                Span = new SnapshotSpan(line.Snapshot, line.Start + 1, 2)
            };

            if (text.Length < 7)
                yield break;

            yield return new SpanClassification 
            {
                Entry = TokenEntryTypes.ADDRESS,
                Span = new SnapshotSpan(line.Snapshot, line.Start + 3, 4)
            };

            if (text.Length < 9)
                yield break;

            yield return new SpanClassification
            {
                Entry = TokenEntryTypes.RECORD_TYPE,
                Span = new SnapshotSpan(line.Snapshot, line.Start + 7, 2)
            };

            byteCount = Math.Min(text.Length - 9, byteCount * 2);

            yield return new SpanClassification
            {
                Entry = TokenEntryTypes.DATA,
                Span = new SnapshotSpan(line.Snapshot, line.Start + 9, byteCount)
            };
        
            if (text.Length < (11 + byteCount))
                yield break;

            int calculatedChecksum = CalculateChecksum(text);
            int fileChecksum = -1;
            int.TryParse(text.Substring(text.Length - 2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out fileChecksum);

            yield return new SpanClassification
            {
                Entry = (fileChecksum == calculatedChecksum) ? TokenEntryTypes.CHECKSUM : TokenEntryTypes.CHECKSUM_BAD,
                Span = new SnapshotSpan(line.Snapshot, line.Start + 9 + byteCount, 2)
            };
        }

        private int CalculateChecksum(string textLine)
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

        #region ClassifierTypeNames

        private readonly Dictionary<TokenEntryTypes, string> mClassifierTypeNames = new Dictionary<TokenEntryTypes, string>() {
            { TokenEntryTypes.START_CODE, "hex.startcode" },
            { TokenEntryTypes.BYTE_COUNT, "hex.bytecount" },
            { TokenEntryTypes.ADDRESS, "hex.address" },
            { TokenEntryTypes.RECORD_TYPE, "hex.recordtype" },
            { TokenEntryTypes.DATA, "hex.data" },
            { TokenEntryTypes.CHECKSUM, "hex.checksum" },
            { TokenEntryTypes.CHECKSUM_BAD, "hex.checksum.bad" },
        };

        public Dictionary<TokenEntryTypes, string> GetClassifierTypeNames()
        {
            return mClassifierTypeNames;
        }

        #endregion
    }
}
