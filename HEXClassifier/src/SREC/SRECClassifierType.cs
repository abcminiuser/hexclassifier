using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "srec.startcode")]
    [Name("srec.startcode")]
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
    [ClassificationType(ClassificationTypeNames = "srec.bytecount")]
    [Name("srec.bytecount")]
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
    [ClassificationType(ClassificationTypeNames = "srec.address")]
    [Name("srec.address")]
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
    [ClassificationType(ClassificationTypeNames = "srec.recordtype")]
    [Name("srec.recordtype")]
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
    [ClassificationType(ClassificationTypeNames = "srec.data")]
    [Name("srec.data")]
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
    [ClassificationType(ClassificationTypeNames = "srec.checksum")]
    [Name("srec.checksum")]
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
    [ClassificationType(ClassificationTypeNames = "srec.checksum.bad")]
    [Name("srec.checksum.bad")]
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
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.startcode")]
        internal static ClassificationTypeDefinition SRECStartCodeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.bytecount")]
        internal static ClassificationTypeDefinition SRECByteCountDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.address")]
        internal static ClassificationTypeDefinition SRECAddressDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.recordtype")]
        internal static ClassificationTypeDefinition SRECRecordTypeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.data")]
        internal static ClassificationTypeDefinition SRECDataDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.checksum")]
        internal static ClassificationTypeDefinition SRECChecksumDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("srec.checksum.bad")]
        internal static ClassificationTypeDefinition SRECBadChecksumDefinition { get; set; }
    }
}
