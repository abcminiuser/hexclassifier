// Guids.cs
// MUST match guids.h
using System;

namespace FourWalledCubicle.HEXClassifier
{
    static class GuidList
    {
        public const string guidHEXClassifierPkgString = "480b3145-39ad-48e1-b6f8-8cd13f6e4f72";
        public const string guidHEXClassifierCmdSetString = "d4c6ec7f-94f4-42f2-9c46-d0ca95a57afa";

        public static readonly Guid guidHEXClassifierCmdSet = new Guid(guidHEXClassifierCmdSetString);
    };
}