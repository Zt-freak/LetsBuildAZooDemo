// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.StockStatus.StockStatusManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_Manage.Hiring;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_ManageShop.RecipeView.StockStatus
{
  internal class StockStatusManager
  {
    private GameObjectNineSlice frameobject;
    public Vector2 Position;
    private ScreenHeading Name;
    private Vector2 VSCALE;
    private GameObject TEXTOBJECT;
    private BigPersonFrame BigFood;
    private TextButton Order;
    private TextButton IncreaseStock;
    private GameObjectNineSlice FOODFRAME;
    private float OverAllMultiplier;

    public StockStatusManager(ShopStatEntry shopstat, float _OverAllMultiplier = 1f)
    {
      this.OverAllMultiplier = _OverAllMultiplier;
      Vector3 SecondaryColour;
      this.frameobject = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.Name = new ScreenHeading("STOCK");
      this.Name.header.vLocation = Vector2.Zero;
      this.VSCALE = new Vector2(600f, 100f);
      this.frameobject.scale = 2f;
      this.frameobject.vLocation.X = 768f;
      this.TEXTOBJECT = new GameObject();
      this.TEXTOBJECT.SetAllColours(SecondaryColour);
      this.Order = new TextButton("Restock", 120f);
      this.IncreaseStock = new TextButton("Upgrade Capacity", 120f);
      this.Order.SetButtonColour(BTNColour.Pink);
      this.FOODFRAME = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.FOODFRAME.vLocation.X = 256f;
      this.BigFood = new BigPersonFrame(shopstat.MainItemForSale, false, _OverallScaleMod: this.OverAllMultiplier);
    }

    public void UpdateStockStatusManager(Player player, Vector2 Offset, float DeltaTime)
    {
      this.Position.X = 0.0f;
      Offset.Y = 150f;
      this.frameobject.vLocation.X = 570f;
      this.VSCALE.X = 780f;
      Vector2 Offset1 = this.frameobject.vLocation + Offset;
      this.Order.UpdateTextButton(player, Offset1, DeltaTime);
      this.IncreaseStock.UpdateTextButton(player, Offset1, DeltaTime);
    }

    public void DrawStockStatusManager(Vector2 Offset)
    {
      Offset.Y = 150f;
      this.frameobject.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.Name.DrawScreenHeading(Offset + new Vector2(512f, 0.0f) + new Vector2(0.0f, this.VSCALE.Y * -0.5f), AssetContainer.pointspritebatch03);
      Vector2 Offset1 = this.frameobject.vLocation + Offset;
      TextFunctions.DrawTextWithDropShadow("STOCK:", 0.7f * this.OverAllMultiplier, Offset1 + new Vector2(-290f, -4f * this.OverAllMultiplier), this.TEXTOBJECT.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, true);
      TextFunctions.DrawTextWithDropShadow("12/20:", 1.5f * this.OverAllMultiplier, Offset1 + new Vector2(-270f, -26f * this.OverAllMultiplier), this.TEXTOBJECT.GetColour(), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, false);
      TextFunctions.DrawTextWithDropShadow("New Recipe Resets Stock To 0", RenderMath.GetPixelSizeBestMatch(2f * this.OverAllMultiplier), Offset1 + new Vector2(-360f, 15f), Color.Red, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, false, false);
      this.Order.vLocation.X = 200f;
      this.Order.vLocation.Y = -20f;
      this.Order.DrawTextButton(Offset1);
      this.IncreaseStock.vLocation.X = 200f;
      this.IncreaseStock.vLocation.Y = 20f;
      this.IncreaseStock.DrawTextButton(Offset1);
      this.FOODFRAME.vLocation.X = 115f;
      this.FOODFRAME.scale = 2f;
      this.FOODFRAME.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, new Vector2(this.VSCALE.Y, this.VSCALE.Y));
      this.BigFood.DrawBigPersonFrame(Offset + this.FOODFRAME.vLocation, AssetContainer.pointspritebatch03, true);
    }
  }
}
