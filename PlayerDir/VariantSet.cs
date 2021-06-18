// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.VariantSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir
{
  internal class VariantSet
  {
    private List<int> NumberOfEachFoundDEPRICATED;
    private int[] TotalVariantsFound;
    private int[] DayDiscovered;

    public VariantSet()
    {
      this.NumberOfEachFoundDEPRICATED = new List<int>();
      this.TotalVariantsFound = new int[10];
      this.DayDiscovered = new int[10];
    }

    public bool FoundVariant(int Variant)
    {
      ++this.TotalVariantsFound[Variant];
      if (this.TotalVariantsFound[Variant] != 1)
        return false;
      this.DayDiscovered[Variant] = (int) Player.financialrecords.GetDaysPassed();
      return true;
    }

    public int GetTotalOfThisVariantFound(int Variant)
    {
      if (Variant != -1)
        return this.TotalVariantsFound[Variant];
      int num = 0;
      for (int index = 0; index < this.TotalVariantsFound.Length; ++index)
        num += this.TotalVariantsFound[index];
      return num;
    }

    public int GetDayDiscovered(int Variant) => this.TotalVariantsFound[Variant] > 0 ? this.DayDiscovered[Variant] : -1;

    public int GetTotalVaiantsFound()
    {
      int num = 0;
      for (int index = 0; index < this.TotalVariantsFound.Length; ++index)
      {
        if (this.TotalVariantsFound[index] > 0)
          ++num;
      }
      return num;
    }

    public bool IsThisGenomeMapped()
    {
      for (int index = 0; index < this.TotalVariantsFound.Length; ++index)
      {
        if (this.TotalVariantsFound[index] <= 0)
          return false;
      }
      return true;
    }

    public void ForceUnlockGenome()
    {
      for (int index = 0; index < this.TotalVariantsFound.Length; ++index)
      {
        if (this.TotalVariantsFound[index] <= 0)
          this.TotalVariantsFound[index] = 1;
      }
    }

    public void SaveVariantSet(Writer writer)
    {
      writer.WriteInt("f", this.TotalVariantsFound.Length);
      for (int index = 0; index < this.TotalVariantsFound.Length; ++index)
      {
        writer.WriteInt("f", this.TotalVariantsFound[index]);
        writer.WriteInt("f", this.DayDiscovered[index]);
      }
    }

    public void LoadVariantSet(Reader reader, int VersionNumberForLoad)
    {
      int _out1 = 0;
      if (VersionNumberForLoad <= 2)
      {
        this.NumberOfEachFoundDEPRICATED = new List<int>();
        int num1 = (int) reader.ReadInt("f", ref _out1);
        for (int index = 0; index < _out1; ++index)
        {
          int _out2 = 0;
          int num2 = (int) reader.ReadInt("f", ref _out2);
          this.NumberOfEachFoundDEPRICATED.Add(_out2);
        }
      }
      else
      {
        int num1 = (int) reader.ReadInt("f", ref _out1);
        this.TotalVariantsFound = new int[_out1];
        this.DayDiscovered = new int[_out1];
        for (int index = 0; index < _out1; ++index)
        {
          int _out2 = 0;
          int num2 = (int) reader.ReadInt("f", ref _out2);
          this.TotalVariantsFound[index] = _out2;
          int num3 = (int) reader.ReadInt("f", ref _out2);
          this.DayDiscovered[index] = _out2;
        }
      }
    }
  }
}
