// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarInfo.MainBar.Scroll.Z_ScrollButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_BarInfo.MainBar.Scroll
{
  internal class Z_ScrollButton : GameObject
  {
    private bool MouseOver;
    private LerpHandler_Float lerper;
    private GameObject Dark;

    public Z_ScrollButton(bool IsLeft, float BaseScale = -1f)
    {
      this.DrawRect = new Rectangle(129, 53, 16, 16);
      this.SetDrawOriginToCentre();
      if ((double) BaseScale != -1.0)
        this.scale = BaseScale;
      else
        this.scale = RenderMath.GetPixelSizeBestMatch(1.3f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.lerper.SetDelay(0.8f);
      this.Dark = new GameObject((GameObject) this);
      this.Dark.DrawRect = new Rectangle(146, 53, 16, 16);
      if (!IsLeft)
        return;
      this.FlipRender = true;
      this.Dark.FlipRender = true;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool MouseOverlapping(Player player, Vector2 Offset) => (double) this.lerper.Value == 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation);

    public bool UpdateZ_ScrollButton(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      bool IsRendering)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        this.MouseOver = MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation);
        if (this.MouseOver && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
          return true;
      }
      return false;
    }

    public void DrawZ_ScrollButton(SpriteBatch spritebatch) => this.DrawZ_ScrollButton(new Vector2(0.0f, this.lerper.Value * 300f), spritebatch);

    public void DrawZ_ScrollButton(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (!this.MouseOver)
        this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      else
        this.Dark.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation);
    }
  }
}
