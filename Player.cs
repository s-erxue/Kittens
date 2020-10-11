using System.Collections.Generic;

namespace Kittens
{
  public class Player
  {
    public string name;
    public List<Card> hand;

    public Player()
    {
      hand = new List<Card>();
    }
  }
}