// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.AvatarUI.Selection.MicroTimerBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_OverWorld.AvatarUI.Selection
{
  internal class MicroTimerBar : GameObject
  {
    private float TimeTotal;
    private float TimeSpent;
    private GameObject Bar;
    private Vector2 VSCaleFrame;
    private Vector2 VSCaleInner;
    private bool HasStarted;
    private float InnerSize;

    public MicroTimerBar()
    {
      this.bActive = false;
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(0.0f, 0.0f, 0.0f);
      this.Bar = new GameObject((GameObject) this);
      this.Bar.SetAllColours(new Vector3(0.2f, 0.3f, 0.6f));
      this.Bar.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.VSCaleFrame = new Vector2(16f, 4f);
      this.VSCaleInner = new Vector2(14f, 2f);
      this.InnerSize = 14f;
    }

    public void Activate(float Time)
    {
      this.bActive = true;
      this.HasStarted = true;
      this.TimeTotal = Time;
      this.TimeSpent = 0.0f;
    }

    public bool UpdateMicroTimerBar(float DeltaTime, bool IsScrolling)
    {
      if (this.HasStarted)
      {
        if (IsScrolling)
        {
          this.TimeSpent += DeltaTime;
          if ((double) this.TimeSpent > (double) this.TimeTotal)
          {
            this.TimeSpent = this.TimeTotal;
            this.bActive = false;
            return true;
          }
        }
        else
        {
          this.bActive = false;
          this.HasStarted = false;
        }
      }
      return false;
    }

    public void Deactivate()
    {
      this.TimeSpent = 0.0f;
      this.bActive = false;
    }

    public void DrawMicroTimerBar(Vector2 Location, float HeightToBottom, SpriteBatch spriteBatch)
    {
      if (!this.bActive)
        return;
      Location.Y += HeightToBottom;
      Location.Y += 4f * Sengine.WorldOriginandScale.Z * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.Draw(spriteBatch, AssetContainer.SpriteSheet, Location, this.VSCaleFrame * Sengine.WorldOriginandScale.Z);
      Location.X -= 7f * Sengine.WorldOriginandScale.Z;
      this.VSCaleInner.X = this.InnerSize * (this.TimeSpent / this.TimeTotal);
      this.Bar.Draw(spriteBatch, AssetContainer.SpriteSheet, Location, this.VSCaleInner * Sengine.WorldOriginandScale.Z);
    }
  }
}
