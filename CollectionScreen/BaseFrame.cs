// Decompiled with JetBrains decompiler
// Type: TinyZoo.CollectionScreen.BaseFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.CollectionScreen
{
  internal class BaseFrame : GameObject
  {
    private Rectangle BaseRect;
    public bool MouseOver;
    public GameObject MouseOverObj;
    public bool MouseOverWhite;

    public BaseFrame()
    {
      this.BaseRect = new Rectangle(983, 310, 30, 30);
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.scale = 2f;
      this.MouseOverObj = new GameObject((GameObject) this);
      this.MouseOverObj.DrawRect.Y += 31;
    }

    public Vector2 GetSize() => new Vector2((float) this.BaseRect.Width, (float) this.BaseRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool UpdateBaseFrame(Vector2 Offset, Player player)
    {
      this.MouseOver = false;
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.MultiTouchTouchLocations[0]))
      {
        if (!GameFlags.IsUsingController || GameFlags.IsUsingMouse)
          this.MouseOver = true;
      }
      else if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.inputmap.PointerLocation) && (!GameFlags.IsUsingController || GameFlags.IsUsingMouse))
        this.MouseOver = true;
      return MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, player.player.touchinput.ReleaseTapArray[0]);
    }

    public void DrawBaseFrame(Vector2 Offset, SpriteBatch DrawWithTHis, float AlphaMult = 1f)
    {
      if (this.MouseOver)
      {
        this.MouseOverObj.scale = this.scale;
        this.MouseOverObj.vLocation = this.vLocation;
        this.MouseOverObj.Draw(DrawWithTHis, AssetContainer.SpriteSheet, Offset, this.MouseOverObj.fAlpha * AlphaMult);
        this.MouseOver = false;
        if (this.MouseOverWhite)
          this.Draw(DrawWithTHis, AssetContainer.SpriteSheet, Offset, this.fAlpha * AlphaMult);
      }
      else
        this.Draw(DrawWithTHis, AssetContainer.SpriteSheet, Offset, this.fAlpha * AlphaMult);
      this.MouseOverWhite = false;
    }
  }
}
