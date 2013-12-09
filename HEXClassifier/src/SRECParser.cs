﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using System.Globalization;

namespace FourWalledCubicle.HEXClassifier
{
    internal abstract class SRECParser
    {
        public enum SRECEntryTypes
        {
            START_CODE,
            RECORD_TYPE,
            BYTE_COUNT,
            ADDRESS,
            DATA,
            CHECKSUM
        };

        public static IEnumerable<Tuple<SRECEntryTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (text.Length < 1)
                yield break;

            if (text[0] != 'S')
                yield break;

            yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                                 SRECEntryTypes.START_CODE, new SnapshotSpan(line.Snapshot, line.Start, 1));

            if (text.Length < 3)
                yield break;

            int recordType = 0;
            if (int.TryParse(text.Substring(1, 2), System.Globalization.NumberStyles.Integer, CultureInfo.CurrentCulture, out recordType) == false)
                yield break;
            yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                SRECEntryTypes.RECORD_TYPE, new SnapshotSpan(line.Snapshot, line.Start + 1, 2));

            if (text.Length < 5)
                yield break;

            int byteCount = 0;
            if (int.TryParse(text.Substring(3, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out byteCount) == false)
                yield break;

            // Unknown records
            if (recordType > 9 || recordType < 0 || recordType == 4)
                yield break;

            yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                SRECEntryTypes.BYTE_COUNT, new SnapshotSpan(line.Snapshot, line.Start + 3, 2));

            int addressBytes = 0;
            switch (byteCount)
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
            if (int.TryParse(text.Substring(5, addressBytes), System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out address) == false)
                yield break;

            if (text.Length < 5 + addressBytes)
                yield break;

            yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                SRECEntryTypes.ADDRESS, new SnapshotSpan(line.Snapshot, line.Start + 5, addressBytes));

            // Check if we expect data in this record
            if (new List<int> { 0, 1, 2, 3 }.Contains(recordType))
            {
                int dataLength = (byteCount * 2) - addressBytes - 2;
                if (text.Length < 5 + dataLength)
                    yield break;
                yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                    SRECEntryTypes.DATA, new SnapshotSpan(line.Snapshot, line.Start + 5 + addressBytes, dataLength));
            }

            yield return new Tuple<SRECEntryTypes, SnapshotSpan>(
                SRECEntryTypes.CHECKSUM, new SnapshotSpan(line.Snapshot, line.End - 2, 2));
        }
    }
}