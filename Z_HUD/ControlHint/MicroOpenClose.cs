// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.ControlHint.MicroOpenClose
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_HUD.ControlHint
{
  internal class MicroOpenClose : GameObject
  {
    private bool UseNewBig;
    private bool IsMouseOver;
    private Rectangle baseRect;
    private Rectangle mouseOverRect;

    public MicroOpenClose(float BaseScale, bool _UseNewBig = true)
    {
      this.UseNewBig = _UseNewBig;
      this.SetAsArrow();
      this.scale = BaseScale;
      this.DrawRect = this.baseRect;
    }

    public void SetAsArrow()
    {
      if (this.UseNewBig)
      {
        this.baseRect = new Rectangle(895, 395, 18, 20);
        this.mouseOverRect = new Rectangle(381, 706, 18, 20);
      }
      else
      {
        this.baseRect = new Rectangle(100, 780, 16, 13);
        this.mouseOverRect = new Rectangle(626, 826, 16, 13);
      }
    }

    public void SetAsSkip()
    {
      if (!this.UseNewBig)
        return;
      this.baseRect = new Rectangle(100, 780, 16, 13);
      this.mouseOverRect = new Rectangle(626, 826, 16, 13);
    }

    public void SetAsClose()
    {
      if (this.UseNewBig)
      {
        this.baseRect = new Rectangle(712, 940, 18, 20);
        this.mouseOverRect = new Rectangle(382, 685, 18, 20);
      }
      else
      {
        this.baseRect = new Rectangle(609, 826, 16, 13);
        this.mouseOverRect = new Rectangle(592, 826, 16, 13);
      }
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.vLocation;
      return MathStuff.CheckPointCollision(true, offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
    }

    public bool UpdateMicroOpenClose(Player player, float DeltaTime, Vector2 Offset)
    {
      this.IsMouseOver = this.CheckMouseOver(player, Offset);
      if (this.IsMouseOver)
        this.DrawRect = this.mouseOverRect;
      else
        this.DrawRect = this.baseRect;
      this.SetDrawOriginToCentre();
      return MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawMicroOpenClose(Vector2 Offset, SpriteBatch spriteBtach) => this.Draw(spriteBtach, AssetContainer.SpriteSheet, Offset);
  }
}
