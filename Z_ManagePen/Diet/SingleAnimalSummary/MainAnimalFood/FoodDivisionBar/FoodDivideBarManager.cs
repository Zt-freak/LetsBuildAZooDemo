// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar.FoodDivideBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_ManagePen.Diet.SingleAnimalSummary.MainAnimalFood.FoodDivisionBar
{
  internal class FoodDivideBarManager
  {
    private string Status;
    public Vector2 Location;
    private FoodBarRenderer foodbarrenderer;
    private GameObject StatusTExt;
    private float PercentWidthPerUnit;
    public float Satiation;

    public FoodDivideBarManager(
      FoodSet foodset,
      Player player,
      FoodCollection foodcollection,
      float BaseScale,
      ref float Height)
    {
      this.foodbarrenderer = new FoodBarRenderer(BaseScale);
      float num1 = (float) this.foodbarrenderer.DrawRect.Width * BaseScale;
      this.StatusTExt = new GameObject();
      this.StatusTExt.scale = BaseScale;
      this.StatusTExt.SetAllColours(ColourData.Z_Cream);
      this.StatusTExt.vLocation.X = (float) ((double) num1 * 0.5 - 5.0 * (double) BaseScale);
      this.StatusTExt.vLocation.Y = -20f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Location.Y = 30f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num2 = 0.0f;
      this.PercentWidthPerUnit = foodcollection.FullDailyFoodRquirement * 1.5f;
      this.PercentWidthPerUnit = 1f / this.PercentWidthPerUnit;
      for (int index = 0; index < foodset.FoodUnitsPerDay.Count; ++index)
      {
        this.foodbarrenderer.AddSegment(num2 + foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit, AnimalFoodData.IsThisMeat(foodcollection.animalfoodentry[index].foodtype), foodcollection.animalfoodentry[index], (double) foodset.FoodUnitsPerDay[index] > 0.0);
        num2 += foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit;
      }
      this.ChangeFoodBarValues(foodset, foodcollection, player);
      Height = this.Location.Y + 20f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void UpdateFoodDivideBarManager(float DeltaTime) => this.foodbarrenderer.UpdateFoodBarRenderer(DeltaTime);

    public void ChangeFoodBarValues(FoodSet foodset, FoodCollection foodcollection, Player player)
    {
      this.Satiation = 0.0f;
      float num1 = 0.0f;
      float num2 = 0.0f;
      for (int index = 0; index < foodset.FoodUnitsPerDay.Count; ++index)
      {
        bool HasStock = true;
        bool LowStock = false;
        if (player.livestats.stocktimes.GetDays(foodcollection.animalfoodentry[index].foodtype) < 1)
          HasStock = false;
        else if (player.livestats.stocktimes.GetDays(foodcollection.animalfoodentry[index].foodtype) <= AnimalFoodData.GetAnimalFoodInfo(foodcollection.animalfoodentry[index].foodtype).DeliveryTime)
          LowStock = true;
        this.foodbarrenderer.SetValue(num1 + foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit, (double) foodset.FoodUnitsPerDay[index] > 0.0, index, HasStock, LowStock);
        num1 += foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit;
        if (!HasStock)
          num2 += foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit;
        this.Satiation += foodset.FoodUnitsPerDay[index] * this.PercentWidthPerUnit;
      }
      this.Satiation *= 1.5f;
      float num3 = 0.0f;
      if ((double) num1 - (double) num3 > 0.709999978542328)
      {
        this.Status = "Overfed";
        if ((double) num1 - (double) num3 > 0.850000023841858)
          this.Status = "Very Overfed";
      }
      else if ((double) num1 - (double) num3 < 0.66100001335144)
      {
        this.Status = "Underfed";
        if ((double) num1 - (double) num3 < 0.333330005407333)
          this.Status = "Starving";
      }
      else
        this.Status = "Satisfied";
      this.Status = this.Status.ToUpper();
    }

    public void DrawFoodDivideBarManager(Vector2 TopCenter)
    {
      TopCenter += this.Location;
      TextFunctions.DrawTextWithDropShadow(this.Status, this.StatusTExt.scale, this.StatusTExt.vLocation + TopCenter, this.StatusTExt.GetColour(), this.StatusTExt.fAlpha, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false, true, false, 0.0f, 0);
      this.foodbarrenderer.DrawFoodBarRenderer(TopCenter);
    }
  }
}
