using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.StartCode)]
    [Name(SRECClassificationType.ClassificationNames.StartCode)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECStartCodeFormat : ClassificationFormatDefinition
    {
        public SRECStartCodeFormat()
        {
            this.DisplayName = "SREC Start Code Definition";
            this.ForegroundColor = Colors.Gray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.ByteCount)]
    [Name(SRECClassificationType.ClassificationNames.ByteCount)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECByteCountFormat : ClassificationFormatDefinition
    {
        public SRECByteCountFormat()
        {
            this.DisplayName = "SREC Byte Count Definition";
            this.ForegroundColor = Colors.Green;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.Address)]
    [Name(SRECClassificationType.ClassificationNames.Address)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECAddressFormat : ClassificationFormatDefinition
    {
        public SRECAddressFormat()
        {
            this.DisplayName = "SREC Address Definition";
            this.ForegroundColor = Colors.Purple;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.RecordType)]
    [Name(SRECClassificationType.ClassificationNames.RecordType)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECRecordTypeFormat : ClassificationFormatDefinition
    {
        public SRECRecordTypeFormat()
        {
            this.DisplayName = "SREC Record Type Definition";
            this.ForegroundColor = Colors.Maroon;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.Data)]
    [Name(SRECClassificationType.ClassificationNames.Data)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECDataFormat : ClassificationFormatDefinition
    {
        public SRECDataFormat()
        {
            this.DisplayName = "SREC Data Definition";
            this.ForegroundColor = Colors.Teal;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.ChecksumOK)]
    [Name(SRECClassificationType.ClassificationNames.ChecksumOK)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECChecksumFormat : ClassificationFormatDefinition
    {
        public SRECChecksumFormat()
        {
            this.DisplayName = "SREC Checksum Definition";
            this.ForegroundColor = Colors.Olive;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = SRECClassificationType.ClassificationNames.ChecksumFail)]
    [Name(SRECClassificationType.ClassificationNames.ChecksumFail)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class SRECBadChecksumFormat : ClassificationFormatDefinition
    {
        public SRECBadChecksumFormat()
        {
            this.DisplayName = "SREC Bad Checksum Definition";
            this.ForegroundColor = Colors.Olive;
            this.BackgroundColor = Colors.Red;
        }
    }

    internal static class SRECClassificationType
    {
        public static class ClassificationNames
        {
            public const string StartCode = "srec.startcode";
            public const string ByteCount = "srec.bytecount";
            public const string Address = "srec.address";
            public const string RecordType = "srec.recordtype";
            public const string Data = "srec.data";
            public const string ChecksumOK = "srec.checksum";
            public const string ChecksumFail = "srec.checksum.bad";
        }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.StartCode)]
        internal static ClassificationTypeDefinition SRECStartCodeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.ByteCount)]
        internal static ClassificationTypeDefinition SRECByteCountDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.Address)]
        internal static ClassificationTypeDefinition SRECAddressDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.RecordType)]
        internal static ClassificationTypeDefinition SRECRecordTypeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.Data)]
        internal static ClassificationTypeDefinition SRECDataDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.ChecksumOK)]
        internal static ClassificationTypeDefinition SRECChecksumDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(SRECClassificationType.ClassificationNames.ChecksumFail)]
        internal static ClassificationTypeDefinition SRECBadChecksumDefinition { get; set; }
    }
}
