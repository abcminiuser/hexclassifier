using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FourWalledCubicle.HEXClassifier
{
    enum HEXEntryTypes
    {
        START_CODE,
        BYTE_COUNT,
        ADDRESS,
        RECORD_TYPE,
        DATA,
        CHECKSUM,
        CHECKSUM_BAD,
    };
}
