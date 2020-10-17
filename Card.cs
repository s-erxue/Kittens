using System;

namespace Kittens
{
  public enum Card
  {
    ExplodingKitten     = 1,
    Defuse              = 1 << 1,
    Nope                = 1 << 2,
    Attack2x            = 1 << 3,
    Skip                = 1 << 4,
    Favor               = 1 << 5,
    Shuffle             = 1 << 6,
    SeeTheFuture3x      = 1 << 7,
    Tacocat             = 1 << 8,
    Cattermelon         = 1 << 9,
    HairyPotatoCat      = 1 << 10,
    BeardCat            = 1 << 11,
    RainbowRalphingCat  = 1 << 12,
  }
}