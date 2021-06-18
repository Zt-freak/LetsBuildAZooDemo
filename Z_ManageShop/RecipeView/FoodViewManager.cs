// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.FoodViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.RecipeView.NewLayout;
using TinyZoo.Z_ManageShop.RecipeView.RecipeCost;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView
{
  internal class FoodViewManager
  {
    private ShopEntry thisshop;
    private ShopStatsCollection thisstore;
    private RecipeViewControllerMatrix controllermatrix;
    public Vector2 Position;
    private List<FoodViewSet> foodsets;
    public ExtraIngredient extraingredient;
    public bool ExtraIngredientChanged;

    public FoodViewManager(Player player, float BaseScale, int OverrideIndex = -1)
    {
      this.thisshop = player.shopstatus.GetThisShop(player.livestats.SelectedSHop.Location, player.livestats.SelectedSHop.tiletype);
      this.thisstore = ShopData.GetShopInfo(player.livestats.SelectedSHop.tiletype);
      if (OverrideIndex > -1)
      {
        this.thisshop = player.shopstatus.shopentries[OverrideIndex];
        this.thisstore = ShopData.GetShopInfo(this.thisshop.tiletype);
      }
      if (this.thisstore.Seasoning != null)
        this.extraingredient = new ExtraIngredient(this.thisstore.Seasoning, this.thisshop, BaseScale);
      this.foodsets = new List<FoodViewSet>();
      for (int index = 0; index < this.thisstore.shopstats.Count; ++index)
      {
        if (this.thisshop.shopstockstatus[index].REF_shopentry == null)
          this.thisshop.shopstockstatus[index].REF_shopentry = this.thisstore.shopstats[index];
        this.foodsets.Add(new FoodViewSet(this.thisstore.shopstats[index], this.thisshop.shopstockstatus[index], BaseScale));
        this.foodsets[index].Location.Y = (float) (90.0 * (double) BaseScale + 190.0 * (double) Sengine.ScreenRationReductionMultiplier.Y * (double) index);
        this.foodsets[index].Location.X = 0.0f;
      }
      if (this.extraingredient != null)
      {
        this.extraingredient.Location = new Vector2(0.0f * BaseScale, 0.0f * BaseScale);
        this.extraingredient.Location.X += BaseScale * 265f;
      }
      this.controllermatrix = new RecipeViewControllerMatrix(this.foodsets.Count, this.extraingredient != null);
    }

    public void CheckAddToShopLedgerOnExit(Player player)
    {
      for (int index = 0; index < this.foodsets.Count; ++index)
        this.foodsets[index].CheckAddToShopLedgerOnExit(player);
    }

    public bool UpdateFoodViewManager(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      out bool SwitchState)
    {
      Offset += this.Position;
      this.controllermatrix.UpdateRecipeViewControllerMatrix(player, DeltaTime);
      SwitchState = false;
      if (this.extraingredient != null)
      {
        if (this.controllermatrix.ExtraIngredientSelected())
          this.controllermatrix.UpdateExBar(player, DeltaTime, this.extraingredient, (FoodViewSet) null);
        if (this.extraingredient.UpdateExtraIngredient(player, DeltaTime, Offset))
          this.ExtraIngredientChanged = true;
      }
      for (int index = 0; index < this.foodsets.Count; ++index)
      {
        if (this.controllermatrix.IsSelected(index))
          this.controllermatrix.UpdateExBar(player, DeltaTime, (ExtraIngredient) null, this.foodsets[index]);
        this.foodsets[index].UpdateFoodViewSet(player, DeltaTime, Offset);
      }
      return false;
    }

    public void DrawFoodViewManager(Vector2 Offset)
    {
      Offset += this.Position;
      for (int index = 0; index < this.foodsets.Count; ++index)
        this.foodsets[index].DrawFoodViewSet(Offset, this.controllermatrix.IsSelected(index));
      if (this.extraingredient == null)
        return;
      this.extraingredient.DrawExtraIngredient(Offset, this.controllermatrix.ExtraIngredientSelected());
    }
  }
}
