using System;

namespace MarsRoverJM2018.Enums
{
    /// <summary>
    /// Mission type encoder
    /// (sure, it's basically two bools, but they stringify!)
    /// </summary>
    [Flags]
    public enum MissionType
    {
        NoArgs = 0,
        Optimistic = 1,
        Debug = 2
    }
}
