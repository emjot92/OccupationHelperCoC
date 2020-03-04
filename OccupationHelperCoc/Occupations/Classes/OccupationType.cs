using System;
using System.ComponentModel;

namespace Occupations.Classes
{
    [Flags]
    public enum OccupationType
    {
        [Description("Standard")]
        Standard = 1,
        [Description("Modern")]
        Modern = 2,
        [Description("Criminal")]
        Criminal = 4
    }
}
