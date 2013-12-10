using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    internal static class SRECContentDefinition
    {
        [Export]
        [Name("srec")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition srecContentTypeDefinition { get; set; }

        [Export]
        [ContentType("srec")]
        [FileExtension(".srec")]
        internal static FileExtensionToContentTypeDefinition srecContentTypeDefinitionFileExtension { get; set; }
    }
}
