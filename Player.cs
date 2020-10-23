using System.Collections.Generic;

namespace Kittens
{
  public class Player
  {
    public string Name;
    public readonly List<Card> Hand;
    public ushort TurnsLeft;

    public Player()
    {
      Hand = new List<Card>();
      TurnsLeft = 0;
    }
  }
}