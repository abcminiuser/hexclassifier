using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "hex.startcode")]
    [Name("hex.startcode")]
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
    [ClassificationType(ClassificationTypeNames = "hex.bytecount")]
    [Name("hex.bytecount")]
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
    [ClassificationType(ClassificationTypeNames = "hex.address")]
    [Name("hex.address")]
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
    [ClassificationType(ClassificationTypeNames = "hex.recordtype")]
    [Name("hex.recordtype")]
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
    [ClassificationType(ClassificationTypeNames = "hex.data")]
    [Name("hex.data")]
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
    [ClassificationType(ClassificationTypeNames = "hex.checksum")]
    [Name("hex.checksum")]
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

    internal static class HEXClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.startcode")]
        internal static ClassificationTypeDefinition HEXStartCodeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.bytecount")]
        internal static ClassificationTypeDefinition HEXByteCountDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.address")]
        internal static ClassificationTypeDefinition HEXAddressDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.recordtype")]
        internal static ClassificationTypeDefinition HEXRecordTypeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.data")]
        internal static ClassificationTypeDefinition HEXDataDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("hex.checksum")]
        internal static ClassificationTypeDefinition HEXChecksumDefinition { get; set; }
    }
}
