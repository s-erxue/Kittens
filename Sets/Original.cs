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
      Console.WriteLine($"{currentPlayer.name}'s turn!");
      currentPlayer.turnsLeft = 1;
      while (currentPlayer.turnsLeft > 0)
      {
        PlayOrPass(currentPlayer, deck, players);
        return;
      }
    }

    private static void PlayOrPass(Player currentPlayer, List<Card> deck, List<Player> players)
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
                var actionCards = currentPlayer.hand.Where(c => Array.Exists(new[] { Card.Attack2x, Card.Skip, Card.Favor, Card.Shuffle, Card.SeeTheFuture3x }, card => card == c)).Distinct().ToList();
                Console.WriteLine("Which one?\n");
                for (int i = 0; i < actionCards.Count(); i++)
                {
                  Console.Write($"{i + 1}. ");
                  PrintCardWithNewline(actionCards[i]);
                }
                Card cardToPlay = actionCards[GetIntFromRange("\n> ", actionCards.Count()) - 1];
                switch (cardToPlay)
                {
                    case Card.Attack2x:
                        break;
                    case Card.Skip:
                        break;
                    case Card.Favor:
                        List<Player> playersToStealFrom = players
                            .Where(player => player != currentPlayer && player.hand.Count != 0)
                            .ToList();
                        Console.WriteLine("Who to steal from?\n");
                        for (int i = 0; i < playersToStealFrom.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {playersToStealFrom[i].name}");
                        }
                        Player playerToStealFrom = playersToStealFrom[GetIntFromRange("\n>", 1, playersToStealFrom.Count) - 1];
                        Console.Write($"{playerToStealFrom.name}, press a key when you are ready.");
                        Console.ReadKey();
                        Console.WriteLine("Which card to give?\n");
                        for (int i = 0; i < playerToStealFrom.hand.Count; i++)
                        {
                            Console.Write($"{i + 1}. ");
                            PrintCardWithNewline(playerToStealFrom.hand[i]);
                        }
                        int cardToStealIndex = GetIntFromRange("\n>", 1, playerToStealFrom.hand.Count) - 1;
                        currentPlayer.hand.Add(playerToStealFrom.hand[cardToStealIndex]);
                        playerToStealFrom.hand.RemoveAt(cardToStealIndex);
                        break;
                    case Card.Shuffle:
                        deck.Shuffle();
                        break;
                    case Card.SeeTheFuture3x:
                        Console.WriteLine("Cards from top to bottom:\n");
                        List<Card> topThree = deck.GetRange(deck.Count - 3, 3);
                        topThree.Reverse();
                        foreach (Card card in topThree)
                        {
                            PrintCardWithNewline(card);
                        }
                        break;
                }
                break;
              case 2:
                break;
            }
            break;
          case 3:
            return;
        }
      }
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
          name = Console.ReadLine()
        });
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
          player.hand.Add(deck[deck.Count - 1]);
          deck.RemoveAt(deck.Count - 1);
        }
      }

      // TODO insert cards into deck instead of shuffle again
      deck.AddRange(Repeat(Card.Defuse, (ushort)(6 - numPlayers)));
      deck.AddRange(Repeat(Card.ExplodingKitten, (ushort)(numPlayers - 1)));
      deck.Shuffle();

      return (players, numPlayers, deck);
    }
  }
}