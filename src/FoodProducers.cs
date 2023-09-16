using System;

namespace IdleVillager;

[Flags]
internal enum FoodProducers
{
    None = 0,
    FarmsAndGardens = 0x1,
    FishingSpot = 0x2,
    FishingTrap = 0x4,
    Greenhouse  = 0x8,
    Poop = 0x10,
}

