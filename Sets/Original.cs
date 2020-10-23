using System;
using System.Collections.Generic;
using System.Linq;
using static Kittens.Helpers;

namespace Kittens.Sets
{
  public static class Original
  {
    public static void Play()
    {
      (List<Player> players, int numPlayers, List<Card> deck) = Setup();
      List<Card> discardPile = new List<Card>();
      Player currentPlayer = players[0];
      Console.WriteLine($"{currentPlayer.Name}'s turn!");
      currentPlayer.TurnsLeft = 1;
      int nextPlayerTurns;
      while (currentPlayer.TurnsLeft > 0)
      {
        nextPlayerTurns = PlayOrPass(currentPlayer, deck, players, currentPlayer.TurnsLeft, discardPile);
      }
    }

    private static int PlayOrPass(Player currentPlayer, List<Card> deck, List<Player> players, ushort turnsLeft, List<Card> discardPile)
    {
      ushort turnsLeftLocal = turnsLeft;
      while (turnsLeftLocal > 0)
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
              foreach (Card card in currentPlayer.Hand)
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
                  List<Card> actionCards = currentPlayer.Hand.Where(
                      c =>
                        Array.Exists(
                          new[] {Card.Attack2X, Card.Skip, Card.Favor, Card.Shuffle, Card.SeeTheFuture3X},
                          card => card == c
                        )
                    )
                    .Distinct()
                    .ToList();
                  Console.WriteLine("Which one?\n");
                  for (int i = 0; i < actionCards.Count(); i++)
                  {
                    Console.Write($"{i + 1}. ");
                    PrintCardWithNewline(actionCards[i]);
                  }

                  Card cardToPlay = actionCards[GetIntFromRange("\n> ", actionCards.Count) - 1];
                  // ReSharper disable SwitchStatementMissingSomeEnumCasesNoDefault
                  switch (cardToPlay)
                    // ReSharper restore SwitchStatementMissingSomeEnumCasesNoDefault
                  {
                    case Card.Attack2X:
                      break;
                    case Card.Skip:
                      turnsLeftLocal--;
                      break;
                    case Card.Favor:
                      List<Player> playersToStealFrom = players
                        .Where(player => player != currentPlayer && player.Hand.Count != 0)
                        .ToList();
                      Console.WriteLine("Who to steal from?\n");
                      for (int i = 0; i < playersToStealFrom.Count; i++)
                      {
                        Console.WriteLine($"{i + 1}. {playersToStealFrom[i].Name}");
                      }

                      Player playerToStealFrom =
                        playersToStealFrom[GetIntFromRange("\n>", 1, playersToStealFrom.Count) - 1];
                      Console.Write($"{playerToStealFrom.Name}, press a key when you are ready.");
                      Console.ReadKey();
                      Console.WriteLine("Which card to give?\n");
                      for (int i = 0; i < playerToStealFrom.Hand.Count; i++)
                      {
                        Console.Write($"{i + 1}. ");
                        PrintCardWithNewline(playerToStealFrom.Hand[i]);
                      }

                      int cardToStealIndex = GetIntFromRange("\n>", 1, playerToStealFrom.Hand.Count) - 1;
                      currentPlayer.Hand.Add(playerToStealFrom.Hand[cardToStealIndex]);
                      playerToStealFrom.Hand.RemoveAt(cardToStealIndex);
                      break;
                    case Card.Shuffle:
                      deck.Shuffle();
                      break;
                    case Card.SeeTheFuture3X:
                      Console.WriteLine("Cards from top to bottom:\n");
                      List<Card> topThree = deck.GetRange(deck.Count - 3, 3);
                      topThree.Reverse();
                      foreach (Card card in topThree)
                      {
                        PrintCardWithNewline(card);
                      }

                      break;
                  }

                  currentPlayer.Hand.Remove(cardToPlay);
                  discardPile.Add(cardToPlay);

                  break;
                case 2:
                  break;
              }

              break;
            case 3:
              return 1;
          }
        }
      }

      return 1;
    }
    
    
      
      

    private static (List<Player> players, int numPlayers, List<Card> deck) Setup()
    {
      int numPlayers = GetIntFromRange("How many players? ", 2, 5);
      List<Player> players = new List<Player>();
      for (int i = 0; i < numPlayers; i++)
      {
        Console.Write($"Player {i + 1}'s name? (enter in order of play) ");
        players.Add(new Player
        {
          Name = Console.ReadLine()
        });
      }
      foreach (Player player in players)
      {
        player.Hand.Add(Card.Defuse);
      }

      List<Card> deck = new List<Card>();
      deck.AddRange(Repeat(Card.Nope, 5));
      deck.AddRange(Repeat(Card.Attack2X, 4));
      deck.AddRange(Repeat(Card.Skip, 4));
      deck.AddRange(Repeat(Card.Favor, 4));
      deck.AddRange(Repeat(Card.Shuffle, 4));
      deck.AddRange(Repeat(Card.SeeTheFuture3X, 5));

      foreach (Card cat in new[] {
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
          player.Hand.Add(deck[deck.Count - 1]);
          deck.RemoveAt(deck.Count - 1);
        }
      }
      
      for (int i = 0; i < 6 - numPlayers; i++)
      {
        deck.Insert(RandomInt(0, deck.Count - 1), Card.Defuse);
      }

      for (int i = 0; i < numPlayers - 1; i++)
      {
        deck.Insert(RandomInt(0, deck.Count - 1), Card.ExplodingKitten);
      }

      return (players, numPlayers, deck);
    }
  }
}