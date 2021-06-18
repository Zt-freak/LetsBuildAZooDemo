// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.NewLayout.ExtraIngredient
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_ManageShop.RecipeView.NewLayout
{
  internal class ExtraIngredient
  {
    public FoodSlideManager foodslider;
    public Vector2 Location;
    private GameObjectNineSlice box;
    private Vector2 VSCALE;
    private float BaseScale;
    private LittleSummaryButton InfoThing;
    private LittleSummaryButton InfoCancel;
    private bool ShowingInfo;
    private SimpleTextHandler texthandler;

    public ExtraIngredient(RecipeEntry Seasoning, ShopEntry thisshop, float _BaseScale)
    {
      this.InfoThing = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: _BaseScale);
      this.InfoCancel = new LittleSummaryButton(LittleSummaryButtonType.RedCloseCircle, _BaseScale: _BaseScale);
      this.ShowingInfo = false;
      this.BaseScale = _BaseScale;
      this.Location = new Vector2(770f, 100f);
      this.foodslider = new FoodSlideManager(Seasoning, 0.5f, (ShopStockStatus) null, 0, this.BaseScale, thisshop);
      this.box = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out Vector3 _), 7);
      this.box.scale = this.BaseScale;
      this.VSCALE = new Vector2(550f, 70f) * this.BaseScale;
      this.box.vLocation.X = this.BaseScale * -20f;
      this.box.vLocation.Y = this.BaseScale * -5f;
      this.InfoThing.vLocation = new Vector2(this.VSCALE.X * 0.33f, this.VSCALE.Y * -0.2f);
      this.InfoCancel.vLocation = this.InfoThing.vLocation;
      this.texthandler = new SimpleTextHandler(FoodIconData.GetExtarInredientDescription(Seasoning.MinFood_LeftFood), this.VSCALE.X * 0.8f, true, this.BaseScale, true, true);
      this.texthandler.SetAllColours(ColourData.Z_TextBrown);
      this.texthandler.Location.Y = (float) (-(double) this.texthandler.paragraph.GetSize(true).Y * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.texthandler.Location.Y += this.texthandler.paragraph.GetHeightOfLines(1) * 0.5f;
    }

    public void OverrideFoodSlider(Player player, float Movement) => this.foodslider.OverrideSlider(player, Movement);

    public bool UpdateExtraIngredient(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.ShowingInfo)
      {
        if (this.InfoThing.UpdateLittleSummaryButton(DeltaTime, player, this.box.vLocation + Offset))
          this.ShowingInfo = true;
      }
      else if (this.InfoCancel.UpdateLittleSummaryButton(DeltaTime, player, this.box.vLocation + Offset))
        this.ShowingInfo = false;
      return this.foodslider.UpdateFoodSlideManager(player, DeltaTime, Offset);
    }

    public void DrawExtraIngredient(Vector2 Offset, bool Selected)
    {
      Offset += this.Location;
      if (GameFlags.IsUsingController)
      {
        if (Selected)
        {
          this.box.fAlpha = FlashingAlpha.Slow.fAlpha * 0.5f;
          this.box.fAlpha += 0.5f;
          this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(-70f * this.BaseScale, 0.0f), this.VSCALE + new Vector2(4f, 4f));
          this.box.fAlpha = 1f;
        }
        else
          this.box.fAlpha = 0.4f;
        this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(-70f * this.BaseScale, 0.0f), this.VSCALE);
        this.box.fAlpha = 1f;
      }
      else
        this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(-70f * this.BaseScale, 0.0f), this.VSCALE);
      if (this.ShowingInfo)
      {
        this.InfoCancel.DrawLittleSummaryButton(this.box.vLocation + Offset, AssetContainer.pointspritebatchTop05);
        this.texthandler.DrawSimpleTextHandler(this.box.vLocation + Offset + new Vector2(-70f * this.BaseScale, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        this.foodslider.DrawFoodSlideManager(Offset);
        this.InfoThing.DrawLittleSummaryButton(this.box.vLocation + Offset, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
