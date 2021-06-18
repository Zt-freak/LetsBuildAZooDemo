// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Z_ScrollingBG.ScrollingItem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;

namespace TinyZoo.Z_GenericUI.Z_ScrollingBG
{
  internal class ScrollingItem : GameObject
  {
    private float scrollSpeed;
    private float maxHeight;
    private UIScaleHelper scaleHelper;
    private float BaseScale;
    private Rectangle baseRect;
    private Vector2 baseDrawOrigin;

    public ScrollingItem(
      float _BaseScale,
      Vector3 color,
      float _maxHeight_scaled,
      float _scrollSpeed_scaled)
    {
      this.scrollSpeed = _scrollSpeed_scaled;
      this.maxHeight = _maxHeight_scaled;
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.baseRect = new Rectangle(324, 128, 20, 16);
      this.DrawRect = this.baseRect;
      this.SetAllColours(color);
      this.scale = this.BaseScale;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.baseDrawOrigin = this.DrawOrigin;
    }

    public Vector2 GetSize() => this.scaleHelper.ScaleVector2(new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height));

    public void UpdateScrollingItem(float DeltaTime, Vector2 offset)
    {
      this.vLocation.Y -= this.scrollSpeed * DeltaTime;
      this.ResetRectangle();
      float num1 = (-this.vLocation.Y - this.maxHeight) * Sengine.ScreenRationReductionMultiplier.Y / this.BaseScale;
      if ((double) num1 > 0.0)
      {
        int num2 = MathHelper.Clamp((int) Math.Floor((double) num1), 0, this.baseRect.Height);
        this.DrawRect.Y = this.baseRect.Y + num2;
        this.DrawRect.Height = this.baseRect.Height - num2;
        this.DrawOrigin.Y = this.baseDrawOrigin.Y - (float) num2;
        if (num2 == this.baseRect.Height)
          this.ResetFromBottom();
      }
      float num3 = (this.vLocation.Y + this.scaleHelper.ScaleY((float) this.baseRect.Height)) * Sengine.ScreenRationReductionMultiplier.Y / this.BaseScale;
      if ((double) num3 <= 0.0)
        return;
      this.DrawRect.Height = this.baseRect.Height - MathHelper.Clamp((int) Math.Floor((double) num3), 0, this.baseRect.Height);
    }

    private void ResetFromBottom()
    {
      this.vLocation.Y = 0.0f;
      this.ResetRectangle();
    }

    private void ResetRectangle()
    {
      this.DrawOrigin = this.baseDrawOrigin;
      this.DrawRect = this.baseRect;
    }

    public void DrawScrollingItem(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
