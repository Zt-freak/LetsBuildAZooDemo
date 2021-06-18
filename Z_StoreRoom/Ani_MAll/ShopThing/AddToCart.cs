// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing.AddToCart
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using SEngine.Input;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing
{
  internal class AddToCart : GameObject
  {
    private Vector2 VSCALE;
    private bool MouseOver;
    private GameObject Higligt;
    public ButtonRepeater buttonrepeater;
    private string TEXT;
    private float BaseScale;
    private bool Disabled;

    public AddToCart(float _BaseScale = 1f, bool IsConFirmPurchase = false, bool KeepSmaller = false, bool IsRemove = false)
    {
      this.BaseScale = _BaseScale;
      this.buttonrepeater = new ButtonRepeater();
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.Higligt = new GameObject((GameObject) this);
      this.Higligt.SetAlpha(0.2f);
      this.SetAllColours(0.8745098f, 0.5019608f, 0.4588235f);
      this.VSCALE = new Vector2(110f, 20f) * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier;
      this.buttonrepeater.SetSpeedStep(10);
      this.buttonrepeater.SetSlowSpeedStep(30);
      this.buttonrepeater.SetSpeedStep(50);
      if (IsConFirmPurchase)
      {
        this.VSCALE.X = 200f * this.BaseScale;
        if (KeepSmaller)
          this.VSCALE.X = 130f * this.BaseScale;
        this.TEXT = "Confirm Order";
      }
      else if (IsRemove)
        this.TEXT = "Clear";
      else
        this.TEXT = "Add to cart";
    }

    public void Enable()
    {
      this.Disabled = false;
      this.SetAllColours(0.8745098f, 0.5019608f, 0.4588235f);
    }

    public void Disable()
    {
      this.Disabled = true;
      this.SetAllColours(0.7f, 0.7f, 0.7f);
    }

    public Vector2 GetSize() => this.VSCALE;

    public bool UpdateAddToCart(Player player, float DeltaTime, Vector2 Offset)
    {
      if (this.Disabled)
        return false;
      Offset += this.vLocation;
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCALE.X, this.VSCALE.Y, player.inputmap.PointerLocation);
      return this.buttonrepeater.UpdateButtonHoldRepeater(DeltaTime, this.MouseOver && (MouseStatus.LMouseHeld || MouseStatus.RMouseHeld));
    }

    public void DrawAddToCart(Vector2 Offset, SpriteBatch DrawWithThis)
    {
      this.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset, this.VSCALE);
      Offset += this.vLocation;
      TextFunctions.DrawJustifiedText(this.TEXT, this.BaseScale, Offset + new Vector2(0.0f, 0.0f * this.BaseScale), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, DrawWithThis);
      if (!this.MouseOver)
        return;
      this.Higligt.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset, this.VSCALE);
    }
  }
}
