using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    internal static class HEXContentDefinition
    {
        [Export]
        [Name("hex")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition hexContentTypeDefinition { get; set; }
        
        [Export]
        [ContentType("hex")]
        [FileExtension(".hex")]
        internal static FileExtensionToContentTypeDefinition hexContentTypeDefinitionFileExtension { get; set; }

        [Export]
        [ContentType("hex")]
        [FileExtension(".eep")]
        internal static FileExtensionToContentTypeDefinition hexEEPROMContentTypeDefinitionFileExtension { get; set; }    
    }
}
