using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.HEXClassifier
{
    internal static class HEXContentDefinition
    {
        [Export]
        [Name("hex")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition HEXContentTypeDefinition { get; set; }
        
        [Export]
        [ContentType("hex")]
        [FileExtension(".hex")]
        internal static FileExtensionToContentTypeDefinition HEXContentTypeDefinitionFileExtension { get; set; }

        [Export]
        [ContentType("hex")]
        [FileExtension(".eep")]
        internal static FileExtensionToContentTypeDefinition EEPROMContentTypeDefinitionFileExtension { get; set; }    
    }
}
