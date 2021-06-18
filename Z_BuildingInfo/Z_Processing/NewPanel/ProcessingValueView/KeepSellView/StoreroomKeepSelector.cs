// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView.StoreroomKeepSelector
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.NewPanel.ProcessingValueView.KeepSellView
{
  internal class StoreroomKeepSelector
  {
    public Vector2 location;
    private Vector2 size;
    private PriceAdjuster priceAdjuster;
    private SimpleBuildingRenderer storeRoom;

    public StoreroomKeepSelector(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = uiScaleHelper.DefaultBuffer * 0.5f;
      this.priceAdjuster = new PriceAdjuster(0, 99, 0, _BaseScale: BaseScale);
      this.priceAdjuster.SetSImpleRendererTextColur(ColourData.Z_Cream);
      this.storeRoom = new SimpleBuildingRenderer(TILETYPE.StoreRoom);
      Vector2 vector2_2 = uiScaleHelper.ScaleVector2(Vector2.One * 25f);
      this.storeRoom.SetSize(vector2_2.X, BaseScale);
      this.storeRoom.vLocation.X += vector2_2.X * 0.5f;
      this.size.X += vector2_2.X;
      this.size.X += vector2_1.X;
      this.priceAdjuster.Location.X = this.size.X;
      this.priceAdjuster.Location.X += this.priceAdjuster.GetSize().X * 0.5f;
      this.size.X += this.priceAdjuster.GetSize().X;
      this.size.Y = Math.Max(vector2_2.Y, this.priceAdjuster.GetSize().Y);
      this.SetString(true);
    }

    public Vector2 GetSize() => this.size;

    private void SetString(bool isCreate) => this.priceAdjuster.SetToString(this.priceAdjuster.CurrentValue.ToString(), 50f, isCreate);

    public void UpdateStoreroomKeepSelector(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.priceAdjuster.UpdatePriceAdjuster(player, offset, DeltaTime))
        return;
      this.SetString(false);
    }

    public void DrawStoreroomKeepSelector(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.priceAdjuster.DrawPriceAdjuster(offset, spriteBatch);
      this.storeRoom.DrawSimpleBuildingRenderer(offset, spriteBatch);
    }
  }
}
