// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopRight.SideBarButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopRight
{
  internal class SideBarButton : GameObject
  {
    private GameObject Highlight;
    public bool Selected;
    private string TEXT;
    private GameObject ICon;
    public Vector2 Location;
    private Vector2 VSCALE;
    private bool MouseOver;
    public int TotalThingsInCart;
    private GameObject Frame;

    public SideBarButton(SideBarButtonTYPE sidebarbuttontype, float Width)
    {
      if (sidebarbuttontype == SideBarButtonTYPE.CheckOut)
      {
        this.Frame = new GameObject();
        this.Frame.DrawRect = new Rectangle(2, 343, 64, 64);
        this.Frame.SetDrawOriginToCentre();
      }
      this.vLocation.X = 50f;
      this.vLocation.Y = -10f;
      this.VSCALE = new Vector2(Width, 70f);
      this.Highlight = new GameObject();
      this.Highlight.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Highlight.SetAllColours(0.8745098f, 0.5019608f, 0.4588235f);
      this.Highlight.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      switch (sidebarbuttontype)
      {
        case SideBarButtonTYPE.Main:
          this.TEXT = "Main";
          break;
        case SideBarButtonTYPE.CheckOut:
          this.TEXT = "Checkout";
          this.ICon = new GameObject();
          this.ICon.DrawRect = new Rectangle(333, 477, 42, 26);
          break;
        case SideBarButtonTYPE.MyOrders:
          this.TEXT = "My Orders";
          this.ICon = new GameObject();
          this.ICon.DrawRect = new Rectangle(335, 506, 30, 30);
          break;
        case SideBarButtonTYPE.History:
          this.TEXT = "History";
          this.ICon = new GameObject();
          this.ICon.DrawRect = new Rectangle(335, 506, 30, 30);
          break;
      }
      if (this.ICon == null)
        return;
      this.ICon.SetDrawOriginToCentre();
      this.ICon.scale = 0.666f;
      this.ICon.vLocation.X = 23f;
    }

    public bool UpdateSideBarButton(Vector2 Offset, Player player)
    {
      Offset += this.Location;
      if (MathStuff.CheckPointCollision(true, Offset + new Vector2(100f, 0.0f), 1f, this.VSCALE.X, this.VSCALE.Y, player.player.touchinput.ReleaseTapArray[0]))
        return true;
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset + new Vector2(100f, 0.0f), 1f, this.VSCALE.X, this.VSCALE.Y, player.inputmap.PointerLocation);
      return false;
    }

    public void DrawSideBarButton(Vector2 Offset)
    {
      Offset += this.Location;
      if (this.Selected)
        this.Highlight.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      if (this.ICon != null)
        this.ICon.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawTextWithDropShadow(this.TEXT, RenderMath.GetPixelSizeBestMatch(1f), Offset + this.vLocation, Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03, false);
      if (this.TotalThingsInCart > 0)
      {
        this.Frame.scale = 0.5f;
        this.Frame.SetAllColours(0.8392157f, 0.3568628f, 0.2980392f);
        this.Frame.SetDrawOriginToCentre();
        this.Frame.Draw(AssetContainer.pointspritebatch03, AssetContainer.UISheet, Offset + this.vLocation + new Vector2(118f, 7f));
        TextFunctions.DrawJustifiedText(string.Concat((object) this.TotalThingsInCart), 1f, Offset + this.vLocation + new Vector2(118f, 7f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch03);
      }
      if (!this.MouseOver)
        return;
      this.MouseOver = false;
      this.Highlight.fAlpha = 0.3f;
      this.Highlight.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      this.Highlight.fAlpha = 1f;
    }
  }
}
