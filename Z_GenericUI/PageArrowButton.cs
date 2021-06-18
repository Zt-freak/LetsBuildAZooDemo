// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.PageArrowButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class PageArrowButton : GameObject
  {
    private bool MouseOver;
    private CustomerFrame surround;
    private UIScaleHelper uiscale;

    public PageArrowButton(bool FacingRight, float BaseScale, bool IncludeFrame = false)
    {
      this.uiscale = new UIScaleHelper(BaseScale);
      this.DrawRect = new Rectangle(227, 75, 13, 13);
      this.SetDrawOriginToCentre();
      this.FlipRender = FacingRight;
      this.scale = BaseScale;
      if (!IncludeFrame)
        return;
      this.surround = new CustomerFrame(new Vector2(BaseScale * 22f, BaseScale * 22f * Sengine.ScreenRatioUpwardsMultiplier.Y), true, BaseScale * 2f, true);
    }

    public void SetGrey(bool isGrey)
    {
      if (isGrey)
      {
        this.SetAllColours(Color.LightGray.ToVector3());
        this.SetAlpha(0.5f);
      }
      else
      {
        this.SetAllColours(Color.White.ToVector3());
        this.SetAlpha(1f);
      }
    }

    public Vector2 GetSize() => this.surround != null ? this.surround.VSCale : this.uiscale.ScaleVector2(new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height));

    public bool UpdatePageArrowButton(float DeltaTime, Player player, Vector2 Offset)
    {
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset + this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      return this.MouseOver && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
    }

    public void DrawPageArrowButton(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (this.surround != null)
        this.surround.DrawCustomerFrame(Offset + this.vLocation, spritebatch);
      if (this.MouseOver)
      {
        Offset.Y += this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.MouseOver = false;
      }
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }
  }
}
