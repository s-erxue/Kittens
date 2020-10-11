using System;
using System.Collections.Generic;
using System.Linq;
using static Kittens.Helpers;

namespace Kittens
{
  public static class Original
  {
    public static void Play()
    {
      #region setup
      (List<Player> players, int numPlayers, List<Card> deck) = Setup();
      #endregion
      Player currentPlayer = players[0];
      Console.WriteLine($"{currentPlayer.name}'s turn!");
      #region game
      #region pass or play
      while (currentPlayer.turnsLeft > 0)
      {

        while (true)
        {
          switch (GetIntFromRange(@"What do you want to do?
1. View hand
2. Play card
3. Pass

> ", 3))
          {
            case 1:
              Console.WriteLine("Your hand:\n");
              foreach (Card card in currentPlayer.hand)
              {
                PrintCardWithNewline(card);
              }
              break;
            case 2:
              switch (GetIntFromRange(@"How to play cards?

1. Play single card
2. Play special combo

> ", 2))
              {
                case 1:
                  var actionCards = currentPlayer.hand.Where(
                    (card) =>
                      Array.Exists(new Card[] { Card.Attack2x, Card.Skip, Card.Favor, Card.Shuffle, Card.SeeTheFuture3x }, actionCard => actionCard == card)
                  ).Distinct();
                  Console.WriteLine("Which one?\n");
                  for (int i = 0; i < actionCards.Count(); i++)
                  {
                    Console.Write($"{i + 1}. ");
                    PrintCardWithNewline(actionCards.ElementAt(i));
                  }
                  Card cardToPlay = actionCards.ElementAt(GetIntFromRange("\n> ", actionCards.Count()));
                  switch (cardToPlay)
                  {
                    case Card.Attack2x:
                      break;
                    case Card.Skip:
                      break;
                    case Card.Favor:
                      break;
                    case Card.Shuffle:
                      break;
                    case Card.SeeTheFuture3x:
                      break;
                  }
                  break;
                case 2:
                  break;
              }
              break;
            case 3:
              currentPlayer = NextInRotatingArray(players, currentPlayer);
              Console.WriteLine($"{currentPlayer.name}'s turn!");
              break;
          }
        }
      }
      #endregion 
      #endregion
    }

    private static (List<Player> players, int numPlayers, List<Card> deck) Setup()
    {
      int numPlayers = GetIntFromRange("How many players? ", 2, 5);
      List<Player> players = new List<Player>();
      for (int i = 0; i < numPlayers; i++)
      {
        Console.Write($"Player {i + 1}'s name? (enter in order of play) ");
        players[i] = new Player
        {
          name = Console.ReadLine()
        };
      }
      foreach (Player player in players)
      {
        player.hand.Add(Card.Defuse);
      }
      List<Card> deck = new List<Card>();
      deck.AddRange(Repeat(Card.Nope, 5));
      deck.AddRange(Repeat(Card.Attack2x, 4));
      deck.AddRange(Repeat(Card.Skip, 4));
      deck.AddRange(Repeat(Card.Favor, 4));
      deck.AddRange(Repeat(Card.Shuffle, 4));
      deck.AddRange(Repeat(Card.SeeTheFuture3x, 5));
      foreach (Card cat in new Card[] {
        Card.Tacocat,
        Card.Cattermelon,
        Card.HairyPotatoCat,
        Card.BeardCat,
        Card.RainbowRalphingCat
      })
      {
        deck.AddRange(Repeat(cat, 4));
      }
      deck.Shuffle();
      foreach (Player player in players)
      {
        for (int i = 0; i < 7; i++)
        {
          player.hand.Add(deck[0]);
          deck.RemoveAt(0);
        }
      }
      deck.AddRange(Repeat(Card.Defuse, (ushort)(6 - numPlayers)));
      deck.AddRange(Repeat(Card.ExplodingKitten, (ushort)(numPlayers - 1)));
      deck.Shuffle();
      return (players, numPlayers, deck);
    }
  }
}