// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.FoodViewSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.BaseScaled;
using TinyZoo.Z_ManageShop.RecipeView.NewLayout;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_ManageShop.RecipeView
{
  internal class FoodViewSet
  {
    private ShopStockStatus REF_playershopstockstatus;
    private GameObjectNineSlice box;
    private Vector2 VSCALE;
    private GameObject TextColourer;
    public Vector2 Location;
    private CostSlider CutomerBuy_PriceAdjuster;
    private CostSlider WholeSaleCost_PriceAdjuster;
    private FoodAndName foodandname;
    private RecipeSliders recipesliders;
    private ShopStatEntry shopstatentry;
    private int IngredientCOST;
    private bool CostWasChanged;
    private bool IngredientsWereChanged;
    private GameObject ColumnObj;
    private GameObject ColumnObj2;
    private Vector2 ColumnVScale1;
    private Vector2 ColumnVAscale2;
    private P_And_L pandl;
    private float BaseScale;

    public FoodViewSet(
      ShopStatEntry _shopstatentry,
      ShopStockStatus playershopstockstatus,
      float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.shopstatentry = _shopstatentry;
      this.REF_playershopstockstatus = playershopstockstatus;
      Vector3 SecondaryColour;
      this.box = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.box.scale = this.BaseScale;
      this.VSCALE = new Vector2(900f, 120f) * this.BaseScale;
      this.TextColourer = new GameObject();
      this.TextColourer.SetAllColours(SecondaryColour);
      this.CutomerBuy_PriceAdjuster = new CostSlider(playershopstockstatus, SecondaryColour, this.BaseScale);
      this.CutomerBuy_PriceAdjuster.Price.SpreadButtons(10f * this.BaseScale);
      this.foodandname = new FoodAndName(this.shopstatentry.MainItemForSale, SecondaryColour, this.BaseScale, 2f);
      this.foodandname.Location.X = -380f * this.BaseScale;
      this.foodandname.Location.Y = 16f * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CutomerBuy_PriceAdjuster.Location.X = -250f * this.BaseScale;
      this.CutomerBuy_PriceAdjuster.Location.Y = 12f * this.BaseScale;
      this.recipesliders = new RecipeSliders(this.shopstatentry, playershopstockstatus, this.BaseScale);
      this.recipesliders.Location.X = 0.0f * this.BaseScale;
      this.WholeSaleCost_PriceAdjuster = new CostSlider(playershopstockstatus, SecondaryColour, this.BaseScale, true);
      this.WholeSaleCost_PriceAdjuster.Price.SpreadButtons(10f * this.BaseScale);
      this.WholeSaleCost_PriceAdjuster.Location.X = 190f * this.BaseScale;
      this.IngredientCOST = -1;
      this.WholeSaleCost_PriceAdjuster.Location.Y = this.CutomerBuy_PriceAdjuster.Location.Y;
      this.ColumnObj = new GameObject();
      this.ColumnObj.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.ColumnObj.SetDrawOriginToCentre();
      this.ColumnObj.SetAllColours(0.0f, 0.0f, 0.0f);
      this.ColumnObj.SetAlpha(0.1f);
      this.ColumnVScale1 = new Vector2(150f * this.BaseScale, this.VSCALE.Y);
      this.ColumnVAscale2 = new Vector2(100f * this.BaseScale, this.VSCALE.Y);
      this.ColumnObj2 = new GameObject(this.ColumnObj);
      this.ColumnVAscale2.X = 400f * this.BaseScale;
      this.ColumnObj2.vLocation.X = this.CutomerBuy_PriceAdjuster.Location.X + 100f * this.BaseScale;
      this.pandl = new P_And_L(this.VSCALE.Y, this.BaseScale);
      this.pandl.Location.X = this.WholeSaleCost_PriceAdjuster.Location.X + 150f * this.BaseScale;
    }

    public void CheckAddToShopLedgerOnExit(Player player)
    {
      if (this.CostWasChanged)
      {
        this.REF_playershopstockstatus.CheckSetNewPrice();
        this.REF_playershopstockstatus.StockPrice = this.IngredientCOST;
      }
      if (!this.IngredientsWereChanged)
        return;
      MoralityCalculator.CalculateMorality(player);
    }

    public void OverrideFoodSlider(Player player, float Movement, float DeltaTime)
    {
      if (player.inputmap.HeldButtons[5] || player.inputmap.HeldButtons[6])
        this.CutomerBuy_PriceAdjuster.ForceUpdateSlider(player.inputmap.HeldButtons[5], player.inputmap.HeldButtons[6]);
      else
        this.recipesliders.OverrideSlider(player, Movement);
    }

    public void UpdateFoodViewSet(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      Offset.Y -= 10f * this.BaseScale;
      int num = this.recipesliders.UpdateRecipeSliders(Offset, player, DeltaTime) ? 1 : 0;
      if (num != 0)
        this.IngredientsWereChanged = true;
      if (num != 0 || this.IngredientCOST == -1)
      {
        if (this.REF_playershopstockstatus.StockSliderValues.Count > 0)
          this.IngredientCOST = this.REF_playershopstockstatus.GetStockPrice();
        this.WholeSaleCost_PriceAdjuster.SetCost(this.IngredientCOST);
        this.SetPurchaseString();
      }
      if (this.CutomerBuy_PriceAdjuster.UpdateCostSlider(player, Offset, DeltaTime))
      {
        this.SetPurchaseString();
        this.CostWasChanged = true;
      }
      this.WholeSaleCost_PriceAdjuster.UpdateCostSlider(player, Offset, DeltaTime);
    }

    private void SetPurchaseString()
    {
      if ((double) this.REF_playershopstockstatus.GetCurrentPrice() > (double) this.shopstatentry.IdealCost * 1.5)
        this.CutomerBuy_PriceAdjuster.SetSecondStrring("CRAZY!");
      else if (this.REF_playershopstockstatus.GetCurrentPrice() > this.shopstatentry.IdealCost)
        this.CutomerBuy_PriceAdjuster.SetSecondStrring("EXPENSIVE");
      else if (this.REF_playershopstockstatus.GetCurrentPrice() == this.shopstatentry.IdealCost)
        this.CutomerBuy_PriceAdjuster.SetSecondStrring("SRP");
      else if (this.REF_playershopstockstatus.GetCurrentPrice() > this.shopstatentry.IdealCost / 2)
        this.CutomerBuy_PriceAdjuster.SetSecondStrring("CHEAP");
      else
        this.CutomerBuy_PriceAdjuster.SetSecondStrring("VERY CHEAP");
      if (this.IngredientCOST > this.REF_playershopstockstatus.GetCurrentPrice())
      {
        this.pandl.SetStatus("LOSS:", this.CostToDisplayString((float) (this.REF_playershopstockstatus.GetCurrentPrice() - this.IngredientCOST)), true);
        this.WholeSaleCost_PriceAdjuster.SetSecondStrring("LOSS: " + this.CostToDisplayString((float) (this.REF_playershopstockstatus.GetCurrentPrice() - this.IngredientCOST)));
      }
      if (this.IngredientCOST < this.REF_playershopstockstatus.GetCurrentPrice())
      {
        this.pandl.SetStatus("PROFIT:", this.CostToDisplayString((float) (this.REF_playershopstockstatus.GetCurrentPrice() - this.IngredientCOST)), IsProfit: true);
        this.WholeSaleCost_PriceAdjuster.SetSecondStrring("PROFIT: " + this.CostToDisplayString((float) (this.REF_playershopstockstatus.GetCurrentPrice() - this.IngredientCOST)));
      }
      if (this.IngredientCOST != this.REF_playershopstockstatus.GetCurrentPrice())
        return;
      this.pandl.SetStatus("BREAKEVEN", "");
      this.WholeSaleCost_PriceAdjuster.SetSecondStrring("BREAKEVEN");
    }

    public string CostToDisplayString(float cost)
    {
      string str = "$" + (object) Math.Round((double) Math.Abs(cost * 0.1f), 2);
      if ((double) cost % 10.0 == 0.0)
        str += ".0";
      return str + "0";
    }

    public void DrawFoodViewSet(Vector2 Offset, bool Selected)
    {
      Offset += this.Location;
      if (GameFlags.IsUsingController)
      {
        if (Selected)
        {
          this.box.fAlpha = FlashingAlpha.Slow.fAlpha * 0.5f;
          this.box.fAlpha += 0.5f;
          this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE + new Vector2(4f, 4f));
          this.box.fAlpha = 1f;
        }
        else
          this.box.fAlpha = 0.4f;
        this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE);
        this.box.fAlpha = 1f;
      }
      else
        this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      float num = 10f * this.BaseScale;
      Offset.Y -= num;
      this.foodandname.DrawFoodAndName(Offset);
      this.ColumnObj.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(this.CutomerBuy_PriceAdjuster.Location.X + Offset.X, Offset.Y + this.box.vLocation.Y + num), this.ColumnVScale1, this.ColumnObj.fAlpha);
      this.CutomerBuy_PriceAdjuster.DrawCostSlider(Offset);
      this.ColumnObj2.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, new Vector2(this.WholeSaleCost_PriceAdjuster.Location.X + Offset.X, Offset.Y + this.box.vLocation.Y + num), this.ColumnVAscale2, this.ColumnObj.fAlpha);
      this.WholeSaleCost_PriceAdjuster.DrawCostSlider(Offset);
      this.recipesliders.DrawRecipeSliders(Offset);
      this.pandl.DrawP_And_L(Offset);
    }
  }
}
