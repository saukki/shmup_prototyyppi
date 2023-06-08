using System;

namespace PrototypeGame.Scripts.Enums
{
    [Flags]
    public enum ShotAngle
    {
        North = 1,
        NorthEast = 2,
        East = 4,
        SouthEast = 8,
        South = 16,
        SouthWest = 32,
        West = 64,
        NorthWest = 128,
    }
}