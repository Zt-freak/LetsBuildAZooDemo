// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.FoodCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.Z_StoreRoom
{
  internal class FoodCollection
  {
    public List<AnimalFoodEntry> animalfoodentry;
    public float FullDailyFoodRquirement;
    public float PriceForEverythingTemp;
    public float RelativeCostOfIngredientsNormalized;
    public float Hardynees;
    public int Calc_FullServingSize;
    private float WaterRequirement = -1f;

    public FoodCollection(float _DailyFoodVolume)
    {
      this.FullDailyFoodRquirement = (float) ((int) ((double) _DailyFoodVolume / 590.0) * 100);
      this.Calc_FullServingSize = -1;
      this.FullDailyFoodRquirement = _DailyFoodVolume;
      this.animalfoodentry = new List<AnimalFoodEntry>();
    }

    public void AddNewFoodType(AnimalFoodEntry foodentry) => this.animalfoodentry.Add(foodentry);

    public float GetWaterRequirement()
    {
      if (Z_DebugFlags.IsBetaVersion)
        return 0.02f;
      if ((double) this.WaterRequirement > -1.0)
        return this.WaterRequirement;
      this.WaterRequirement = (double) this.FullDailyFoodRquirement > 0.200000002980232 ? ((double) this.FullDailyFoodRquirement > 0.699999988079071 ? ((double) this.FullDailyFoodRquirement > 1.20000004768372 ? ((double) this.FullDailyFoodRquirement > 1.20000004768372 ? 0.3f : 0.25f) : 0.2f) : 0.15f) : 0.1f;
      return this.WaterRequirement;
    }
  }
}
