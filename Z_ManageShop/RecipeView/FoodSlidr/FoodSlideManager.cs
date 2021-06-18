// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.FoodSlidr.FoodSlideManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_Manage.Hiring;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView.FoodSlidr
{
  internal class FoodSlideManager
  {
    private GameObjectNineSlice frameobject;
    public Vector2 Position;
    private ScreenHeading Name;
    private BigPersonFrame leftfood;
    private BigPersonFrame rightfood;
    public DragAndBar dragandbar;
    private float OverallScaleMultipler;
    private ShopStockStatus REF_playershopstockstatus;
    private ShopEntry REF_thisshop;
    private int INDEX;
    private float BaseScale;
    private string HeaderString;

    public FoodSlideManager(
      RecipeEntry recipeentry,
      float _OverallScaleMultipler,
      ShopStockStatus playershopstockstatus,
      int _INDEX,
      float _BaseScale,
      ShopEntry thisshop = null)
    {
      this.BaseScale = _BaseScale;
      if ((double) this.BaseScale > -1.0)
      {
        this.OverallScaleMultipler = this.BaseScale;
        _OverallScaleMultipler = this.BaseScale;
      }
      this.INDEX = _INDEX;
      float _CurrentDragPercent;
      if (playershopstockstatus != null)
      {
        this.REF_playershopstockstatus = playershopstockstatus;
        while (playershopstockstatus.StockSliderValues.Count < this.INDEX + 1)
          playershopstockstatus.StockSliderValues.Add(0.5f);
        _CurrentDragPercent = playershopstockstatus.StockSliderValues[this.INDEX];
      }
      else
      {
        this.REF_thisshop = thisshop;
        _CurrentDragPercent = thisshop.SeasoningValue;
      }
      this.OverallScaleMultipler = _OverallScaleMultipler;
      this.frameobject = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out Vector3 _), 7);
      this.frameobject.scale = this.BaseScale;
      if (playershopstockstatus != null)
      {
        this.Name = new ScreenHeading("Adjust Recipe: " + recipeentry.SetName, BaseScale: this.BaseScale, UseSmallerOnePointFiveFont: true);
      }
      else
      {
        this.HeaderString = "Adjust: " + recipeentry.SetName;
        this.Name = new ScreenHeading(this.HeaderString + " " + (object) (int) ((double) this.REF_thisshop.SeasoningValue * 100.0) + "%", 40f, BaseScale: this.BaseScale, UseSmallerOnePointFiveFont: true);
      }
      this.Name.header.vLocation = Vector2.Zero;
      this.dragandbar = new DragAndBar(this.OverallScaleMultipler, (double) recipeentry.Cost == 0.0 && (double) recipeentry.premiumCost <= 0.0, _CurrentDragPercent, 200f * _OverallScaleMultipler, _OverallScaleMultipler, 1f);
      this.dragandbar.ExtraHeight = 100f;
      this.dragandbar.ExtraWidth = 100f;
      if (recipeentry.PremiumName == FOODTYPE.Count)
      {
        this.rightfood = new BigPersonFrame(recipeentry.MinFood_LeftFood, false, _OverallScaleMod: this.OverallScaleMultipler, BaseScale: this.BaseScale);
        this.dragandbar.RightText = FoodIconData.GetFoodTypeToString(recipeentry.MinFood_LeftFood);
      }
      else
      {
        this.leftfood = new BigPersonFrame(recipeentry.MinFood_LeftFood, false, _OverallScaleMod: this.OverallScaleMultipler, BaseScale: this.BaseScale);
        this.rightfood = new BigPersonFrame(recipeentry.PremiumName, false, _OverallScaleMod: this.OverallScaleMultipler, BaseScale: this.BaseScale);
        this.dragandbar.LeftText = FoodIconData.GetFoodTypeToString(recipeentry.MinFood_LeftFood);
        this.dragandbar.RightText = FoodIconData.GetFoodTypeToString(recipeentry.PremiumName);
      }
      if ((double) this.BaseScale <= -1.0 || playershopstockstatus != null)
        return;
      this.Name.header.vLocation.X -= this.Name.header.VScale.X * 0.5f;
      this.Name.header.vLocation.X -= 10f * this.BaseScale;
    }

    public void OverrideSlider(Player player, float Movement) => this.dragandbar.ForceFullness(player, Movement);

    public bool UpdateFoodSlideManager(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Position;
      this.dragandbar.UpdateDragAndBar(player, DeltaTime, Offset);
      if (this.REF_playershopstockstatus != null)
      {
        if ((double) this.REF_playershopstockstatus.StockSliderValues[this.INDEX] != (double) this.dragandbar.CurrentDragPercent)
        {
          this.REF_playershopstockstatus.SetSliderValuer(this.INDEX, this.dragandbar.CurrentDragPercent);
          return true;
        }
      }
      else if ((double) this.REF_thisshop.SeasoningValue != (double) this.dragandbar.CurrentDragPercent)
      {
        this.REF_thisshop.SeasoningValue = this.dragandbar.CurrentDragPercent;
        this.Name.SetNewString(this.HeaderString + " " + (object) (int) ((double) this.REF_thisshop.SeasoningValue * 100.0) + "%");
        return true;
      }
      return false;
    }

    public void DrawFoodSlideManager(Vector2 Offset)
    {
      Offset += this.Position;
      if (this.REF_playershopstockstatus != null)
        this.Name.DrawScreenHeading(Offset + new Vector2(0.0f, this.BaseScale * -50f), AssetContainer.pointspritebatchTop05);
      else
        this.Name.DrawScreenHeading(Offset + new Vector2(this.dragandbar.VSCALEOutSide.X * -0.5f, 0.0f), AssetContainer.pointspritebatchTop05);
      this.dragandbar.DrawDragAndBar(AssetContainer.pointspritebatchTop05, Offset);
      if (this.leftfood != null)
        this.leftfood.DrawBigPersonFrame(Offset + new Vector2(this.dragandbar.VSCALEOutSide.X * -0.5f, 0.0f) + new Vector2(-20f * this.leftfood.OverallScaleMod, 0.0f), AssetContainer.pointspritebatchTop05, true);
      this.rightfood.DrawBigPersonFrame(Offset + new Vector2((float) ((double) this.dragandbar.VSCALEOutSide.X * 0.5 + (double) this.rightfood.OverallScaleMod * 5.0), 0.0f) + new Vector2(20f * this.rightfood.OverallScaleMod, 0.0f), AssetContainer.pointspritebatchTop05, true);
    }
  }
}
