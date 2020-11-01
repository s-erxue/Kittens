using System.Collections.Generic;

namespace Kittens
{
  public class Player
  {
    public string Name;
    public readonly List<Card> Hand;
    public bool Attacked;
    public ushort TurnsLeft;
    public bool Exploded;

    public Player()
    {
      Attacked = false;
      Hand = new List<Card>();
      TurnsLeft = 0;
    }
  }
}