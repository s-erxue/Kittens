using System.Collections.Generic;

namespace Kittens
{
  public class Player
  {
    public string name;
    public List<Card> hand;
    public ushort turnsLeft;

    public Player()
    {
      hand = new List<Card>();
      turnsLeft = 0;
    }
  }
}