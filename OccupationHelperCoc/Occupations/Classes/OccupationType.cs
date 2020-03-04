using System;

namespace Occupations.Classes
{
    [Flags]
    public enum OccupationType
    {
        Standard = 1,
        Modern = 2,
        Criminal = 4
    }
}
