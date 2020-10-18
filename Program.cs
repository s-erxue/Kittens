using Kittens.Sets;
using System;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable ArrangeTypeMemberModifiers

namespace Kittens
{
  class Program
  {
    static void Main()
    {
      switch (Helpers.GetIntFromRange(@"Which version:

1. Original
2. Party Pack
3. Quarantined Kittens

> ", 3))
      {
        case 1:
          Original.Play();
          break;
        case 2:
          Party.Play();
          break;
        case 3:
          Quarantined.Play();
          break;
      }

      Console.WriteLine("Press a key when you are done.");
      Console.ReadKey();
    }
  }
}
