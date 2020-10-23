using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Kittens
{
  public static class Helpers
  {
    public static int GetIntFromRange(string prompt, int start, int end)
    {
      while (true)
      {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int result))
        {
          if (result >= start && result <= end)
          {
            return result;
          }
          Console.WriteLine("Your input was not understood.");
        }
        else
        {
          Console.WriteLine("Your input was not understood.");
        }
      }
    }

    public static int GetIntFromRange(string prompt, int end) => GetIntFromRange(prompt, 1, end);

    public static void Shuffle<T>(this IList<T> list)
    {
      RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
      int n = list.Count;
      while (n > 1)
      {
        byte[] box = new byte[1];
        do
        {
          provider.GetBytes(box);
        } while (!(box[0] < n * (byte.MaxValue / n)));
        int k = box[0] % n;
        n--;
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }

    public static int RandomInt(int min, int max)
    {
      Random random = new Random();
      return random.Next(min, max);
    }

    public static List<T> Repeat<T>(T element, ushort count)
    {
      List<T> ret = new List<T>();
      for (int i = 0; i < count; i++)
      {
        ret.Add(element);
      }
      return ret;
    }

    public static void PrintCard(Card card)
    {
      switch (card)
      {
        case Card.ExplodingKitten:
          Console.Write("Exploding Kitten");
          break;
        case Card.Defuse:
          Console.Write("Defuse");
          break;
        case Card.Nope:
          Console.Write("Nope");
          break;
        case Card.Attack2X:
          Console.Write("Attack 2x");
          break;
        case Card.Skip:
          Console.Write("Skip");
          break;
        case Card.Favor:
          Console.Write("Favor");
          break;
        case Card.Shuffle:
          Console.Write("Shuffle");
          break;
        case Card.SeeTheFuture3X:
          Console.Write("See the Future 3x");
          break;
        case Card.Tacocat:
          Console.Write("Tacocat");
          break;
        case Card.Cattermelon:
          Console.Write("Cattermelon");
          break;
        case Card.HairyPotatoCat:
          Console.Write("Hairy Potato Cat");
          break;
        case Card.BeardCat:
          Console.Write("Beard Cat");
          break;
        case Card.RainbowRalphingCat:
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("R");
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write("a");
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("in");
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.Write("b");
          Console.ForegroundColor = ConsoleColor.Blue;
          Console.Write("o");
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write("w");
          Console.ResetColor();
          Console.Write("-ralphing Cat");
          break;
      }
    }

    public static void PrintCardWithNewline(Card card)
    {
      PrintCard(card);
      Console.WriteLine();
    }

    public static T NextInRotatingArray<T>(List<T> values, T value)
    {
      int length = values.Count;
      return values[((values.IndexOf(value) % length) + 1) % length];
    }
  }
}