// Decompiled with JetBrains decompiler
// Type: TinyZoo.Blance.CstBase
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;

namespace TinyZoo.Blance
{
  internal class CstBase
  {
    private List<Decimal> values;
    private List<Decimal> BellValues;
    private int StartValue;
    private Decimal Millipede;
    private bool UsingBell;

    public void SetUp(int _StartValue, float _Millipede)
    {
      this.StartValue = _StartValue;
      this.Millipede = (Decimal) _Millipede;
      this.values = new List<Decimal>();
      this.values.Add((Decimal) _StartValue);
    }

    public void SetUpBell(int _StartValue, float _Millipede, float Bell = 0.9f)
    {
      this.StartValue = _StartValue;
      this.Millipede = (Decimal) Bell;
      this.values = new List<Decimal>();
      this.UsingBell = true;
      this.BellValues = new List<Decimal>();
      this.BellValues.Add((Decimal) _Millipede);
      this.values.Add((Decimal) _StartValue);
      for (int index = 0; index < 100; ++index)
      {
        this.BellValues.Add(this.BellValues[this.BellValues.Count - 1] * this.Millipede);
        this.values.Add(this.values[this.values.Count - 1] * (1M + this.BellValues[this.BellValues.Count - 1]));
        if (Math.Round(this.values[this.values.Count - 1]) > 2147483647M)
          throw new Exception("Number Too big!");
      }
    }

    public void SetUpStorage()
    {
      this.values = new List<Decimal>();
      this.Millipede = 1M;
      this.values.Add(500M);
      this.values.Add(1500M);
      this.values.Add(2000M);
      this.values.Add(5000M);
      this.values.Add(7500M);
      this.values.Add(10000M);
      this.values.Add(15000M);
      this.values.Add(20000M);
      this.values.Add(30000M);
      this.values.Add(50000M);
      this.values.Add(125000M);
      this.values.Add(250000M);
      this.values.Add(500000M);
      this.values.Add(1250000M);
      this.values.Add(2500000M);
      this.values.Add(3750000M);
      this.values.Add(5000000M);
      this.values.Add(7500000M);
      this.values.Add(10000000M);
      this.values.Add(15000000M);
      this.values.Add(25000000M);
      this.values.Add(50000000M);
      this.values.Add(100000000M);
      this.values.Add(150000000M);
      this.values.Add(250000000M);
      this.values.Add(350000000M);
      this.values.Add(425000000M);
    }

    public int GetValue(int ThisPurchase)
    {
      if (this.UsingBell)
      {
        while (this.values.Count < ThisPurchase + 1)
        {
          this.values.Add(this.values[this.values.Count - 1] * (1M + this.BellValues[this.BellValues.Count - 1]));
          this.BellValues.Add(this.BellValues[this.BellValues.Count - 1] * this.Millipede);
        }
        return (int) Math.Round(this.values[ThisPurchase]);
      }
      if (this.Millipede == 1M)
        return ThisPurchase > this.values.Count - 1 ? (int) this.values[this.values.Count - 1] : (int) this.values[ThisPurchase];
      while (this.values.Count < ThisPurchase + 1)
      {
        this.values.Add(this.values[this.values.Count - 1] * this.Millipede);
        if (this.values[this.values.Count - 1] > 999999999M)
          this.values[this.values.Count - 1] = 999999999M;
      }
      int num = (int) Math.Ceiling(this.values[ThisPurchase]);
      if ((Decimal) num < this.values[ThisPurchase])
        num = int.MaxValue;
      return num;
    }
  }
}
