// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut.BottomBar.BottomBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut.BottomBar
{
  internal class BottomBarManager
  {
    private GameObject BaseFrame;
    private Vector2 VSCALE;
    private AddToCart addtocart;
    private int Cost;

    public BottomBarManager(int CoST)
    {
      this.Cost = CoST;
      this.BaseFrame = new GameObject();
      this.BaseFrame.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.VSCALE = new Vector2(1024f, 50f);
      this.BaseFrame.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
      this.addtocart = new AddToCart(IsConFirmPurchase: true);
      this.addtocart.vLocation = new Vector2(650f, 743f);
    }

    public bool UpdateBottomBarManager(Player player, float DeltaTime)
    {
      Vector2 zero = Vector2.Zero;
      return this.addtocart.UpdateAddToCart(player, DeltaTime, zero);
    }

    public void DrawBottomBarManager()
    {
      Vector2 zero = Vector2.Zero;
      this.BaseFrame.vLocation.Y = 768f;
      this.BaseFrame.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, zero, this.VSCALE);
      TextFunctions.DrawTextWithDropShadow("Total Payment: $" + (object) this.Cost, 0.8f, new Vector2(530f, 737f), new Color(ColourData.Z_DarkTextGray), 1f, AssetContainer.roundaboutFont, AssetContainer.pointspritebatch03, false, true);
      this.addtocart.DrawAddToCart(zero, AssetContainer.pointspritebatch03);
    }
  }
}
