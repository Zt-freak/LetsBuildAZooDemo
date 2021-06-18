// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.FoodOptionsList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions.Summary;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodOptions
{
  internal class FoodOptionsList
  {
    private List<FoodDIstributionRow> foodrows;
    private CurrentCostSummary currentcostsummary;
    private CurrentCostPerDay currentcostperday;
    private float BaseScale;

    public FoodOptionsList(
      Vector2 VSCaleForSubFrame,
      FoodCollection foodcollection,
      FoodSet REF_foodset,
      int TotalAnimals,
      int TotalBabies,
      Player player,
      float _BaseScale,
      ref float Height)
    {
      this.BaseScale = _BaseScale;
      this.currentcostsummary = new CurrentCostSummary();
      this.foodrows = new List<FoodDIstributionRow>();
      for (int index = 0; index < foodcollection.animalfoodentry.Count; ++index)
        this.foodrows.Add(new FoodDIstributionRow(foodcollection.animalfoodentry[index], VSCaleForSubFrame, REF_foodset.FoodUnitsPerDay[index], index, REF_foodset, foodcollection.FullDailyFoodRquirement, player, TotalAnimals, TotalBabies, this.BaseScale));
      this.currentcostperday = new CurrentCostPerDay(this.BaseScale);
      this.currentcostperday.SetTotalCost(Z_GameFlags.GetCostAsDOllarsAndCents(this.GetTotalCost()));
      Height += 30f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.currentcostperday.Location.Y += Height;
      Height += 60f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num1 = 45f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.BaseScale + 15f * this.BaseScale;
      for (int index = 0; index < foodcollection.animalfoodentry.Count; ++index)
      {
        this.foodrows[index].Location.Y = num1 * (float) index;
        this.foodrows[index].Location.Y += Height;
      }
      float num2 = (float) foodcollection.animalfoodentry.Count * num1 - 25f * this.BaseScale * Sengine.ScreenRationReductionMultiplier.Y;
      Height += num2;
    }

    private float GetTotalCost()
    {
      float num = 0.0f;
      for (int index = 0; index < this.foodrows.Count; ++index)
        num += this.foodrows[index].column5.COST;
      return num;
    }

    public bool UpdateFoodOptionsList(
      Vector2 TopCenter,
      float DeltaTime,
      Player player,
      int TotalAnimals,
      int TotalBabies)
    {
      bool flag = false;
      int CurrentTotal = 0;
      for (int index = 0; index < this.foodrows.Count; ++index)
        CurrentTotal += this.foodrows[index].column3.priceadjuster.CurrentValue;
      for (int index = 0; index < this.foodrows.Count; ++index)
      {
        if (this.foodrows[index].UpdateFoodRow(TopCenter, player, DeltaTime, CurrentTotal >= 150, CurrentTotal))
        {
          flag = true;
          this.currentcostperday.SetTotalCost(Z_GameFlags.GetCostAsDOllarsAndCents(this.GetTotalCost()));
        }
      }
      return flag;
    }

    public void DrawFoodOptionsList(Vector2 TopCenter)
    {
      this.currentcostperday.DrawCurrentCostPerDay(TopCenter);
      for (int index = 0; index < this.foodrows.Count; ++index)
        this.foodrows[index].DrawFoodRow(TopCenter);
    }
  }
}
