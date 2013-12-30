using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.StartCode)]
    [Name(HEXClassificationType.ClassificationNames.StartCode)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXStartCodeFormat : ClassificationFormatDefinition
    {
        public HEXStartCodeFormat()
        {
            this.DisplayName = "HEX Start Code Definition";
            this.ForegroundColor = Colors.Gray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.ByteCount)]
    [Name(HEXClassificationType.ClassificationNames.ByteCount)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXByteCountFormat : ClassificationFormatDefinition
    {
        public HEXByteCountFormat()
        {
            this.DisplayName = "HEX Byte Count Definition";
            this.ForegroundColor = Colors.Green;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.Address)]
    [Name(HEXClassificationType.ClassificationNames.Address)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXAddressFormat : ClassificationFormatDefinition
    {
        public HEXAddressFormat()
        {
            this.DisplayName = "HEX Address Definition";
            this.ForegroundColor = Colors.Purple;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.RecordType)]
    [Name(HEXClassificationType.ClassificationNames.RecordType)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXRecordTypeFormat : ClassificationFormatDefinition
    {
        public HEXRecordTypeFormat()
        {
            this.DisplayName = "HEX Record Type Definition";
            this.ForegroundColor = Colors.Maroon;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.Data)]
    [Name(HEXClassificationType.ClassificationNames.Data)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXDataFormat : ClassificationFormatDefinition
    {
        public HEXDataFormat()
        {
            this.DisplayName = "HEX Data Definition";
            this.ForegroundColor = Colors.Teal;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.ChecksumOK)]
    [Name(HEXClassificationType.ClassificationNames.ChecksumOK)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXChecksumFormat : ClassificationFormatDefinition
    {
        public HEXChecksumFormat()
        {
            this.DisplayName = "HEX Checksum Definition";
            this.ForegroundColor = Colors.Olive;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HEXClassificationType.ClassificationNames.ChecksumFail)]
    [Name(HEXClassificationType.ClassificationNames.ChecksumFail)]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class HEXBadChecksumFormat : ClassificationFormatDefinition
    {
        public HEXBadChecksumFormat()
        {
            this.DisplayName = "HEX Bad Checksum Definition";
            this.ForegroundColor = Colors.Olive;
            this.BackgroundColor = Colors.Red;
        }
    }

    internal static class HEXClassificationType
    {
        public static class ClassificationNames
        {
            public const string StartCode    = "hex.startcode";
            public const string ByteCount    = "hex.bytecount";
            public const string Address      = "hex.address";
            public const string RecordType   = "hex.recordtype";
            public const string Data         = "hex.data";
            public const string ChecksumOK   = "hex.checksum";
            public const string ChecksumFail = "hex.checksum.bad";
        }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.StartCode)]
        internal static ClassificationTypeDefinition HEXStartCodeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.ByteCount)]
        internal static ClassificationTypeDefinition HEXByteCountDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.Address)]
        internal static ClassificationTypeDefinition HEXAddressDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.RecordType)]
        internal static ClassificationTypeDefinition HEXRecordTypeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.Data)]
        internal static ClassificationTypeDefinition HEXDataDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.ChecksumOK)]
        internal static ClassificationTypeDefinition HEXChecksumDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(HEXClassificationType.ClassificationNames.ChecksumFail)]
        internal static ClassificationTypeDefinition HEXBadChecksumDefinition { get; set; }
    }
}
