// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ZooValues.AnimalTempData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_ZooValues
{
  internal class AnimalTempData
  {
    public AnimalType thissnimal;
    private float Popularity;
    private float GroupSize;
    private float TotalAnimals;
    private float AllHealth;
    public float AllPopularity;
    private float OverGroupSizeSubtraction;
    private float OverGroupSizeMinimum;
    private List<int> Variants;

    public AnimalTempData(AnimalType _thissnimal)
    {
      this.thissnimal = _thissnimal;
      this.Variants = new List<int>();
      this.GroupSize = (float) AnimalData.GetIdealGroupSize(this.thissnimal);
      this.Popularity = AnimalData.SetUp(this.thissnimal);
      this.Popularity = (float) ((double) this.Popularity * 0.800000011920929 + 0.200000002980232);
      this.OverGroupSizeSubtraction = AnimalData.GetOverGroupSizeSubtraction(this.thissnimal);
      this.OverGroupSizeMinimum = AnimalData.GetOverGroupSizeMinimum(this.thissnimal);
    }

    public void AddTestAnimals(int Total)
    {
      this.AllHealth += (float) Total;
      this.AllPopularity += this.Popularity * (float) Total;
      this.TotalAnimals += (float) Total;
    }

    public void AddAnimal(PrisonerInfo animal, ref int ZonePopularity)
    {
      this.AllHealth += animal.GetHealthWellBeing();
      this.AllPopularity += this.Popularity;
      ZonePopularity += (int) ((double) this.Popularity * 100.0);
      if (animal.GetIsABaby())
      {
        this.AllPopularity += this.Popularity;
        ZonePopularity += (int) ((double) this.Popularity * 100.0) * 2;
      }
      if (!this.Variants.Contains(animal.intakeperson.CLIndex))
      {
        this.Variants.Add(animal.intakeperson.CLIndex);
        ZonePopularity += (int) ((double) this.Popularity * 100.0 * 0.333299994468689);
      }
      ++this.TotalAnimals;
    }

    public new void Finalize()
    {
      this.AllPopularity *= this.AllHealth / this.TotalAnimals;
      float num1 = this.AllPopularity / this.TotalAnimals;
      float num2 = 0.0f;
      if (this.Variants.Count > 1)
        num2 = this.Popularity * (float) (this.Variants.Count - 1) * 0.5f;
      if ((double) this.TotalAnimals < (double) this.GroupSize)
      {
        float num3 = (float) ((double) num1 * 0.5 * ((double) this.TotalAnimals / (double) this.GroupSize));
        if ((double) num3 > (double) num1 * 0.5 || (double) num3 < 0.0)
          throw new Exception("SOmething bad happened here");
        this.AllPopularity = (num1 - num3) * this.TotalAnimals;
      }
      else if ((double) this.TotalAnimals != (double) this.GroupSize && (double) this.TotalAnimals > (double) this.GroupSize)
      {
        float num3 = num1 * this.GroupSize;
        float num4 = 1f - this.OverGroupSizeSubtraction;
        float num5 = 0.0f;
        for (int index = 0; (double) index < (double) this.TotalAnimals - (double) this.GroupSize; ++index)
        {
          num5 += num1 * num4;
          num4 = Math.Max(this.OverGroupSizeMinimum, num4 - this.OverGroupSizeSubtraction);
        }
        this.AllPopularity = num3 + num5;
      }
      this.AllPopularity += num2;
    }
  }
}
