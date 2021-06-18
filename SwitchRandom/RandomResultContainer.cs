// Decompiled with JetBrains decompiler
// Type: TinyZoo.SwitchRandom.RandomResultContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.SwitchRandom
{
  internal class RandomResultContainer
  {
    private int[] Results;
    private int LastIndex;
    private string resultsstring;
    private Random rand;

    public RandomResultContainer(int Seed)
    {
      if (!GameFlags.IsArcadeMode)
        this.rand = new Random(Seed);
      this.LastIndex = 0;
      this.Results = SwitchRandomData.GetRandoms(Seed);
      this.resultsstring = nameof (Seed) + (object) Seed + "////";
      this.rand = new Random(Seed);
    }

    public RandomResultContainer(Random _rand) => this.rand = _rand;

    public int Next(int Min, int Max)
    {
      if (this.rand != null)
        return this.rand.Next(Min, Max);
      if (this.Results[this.LastIndex] >= Min && this.Results[this.LastIndex] < Max)
      {
        int result = this.Results[this.LastIndex];
        ++this.LastIndex;
        return result;
      }
      Logger.Print("RANDOM IS DIFFERENT, IT CHANGED????!!!");
      return TinyZoo.Game1.Rnd.Next(Min, Max);
    }
  }
}
