// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Ships.ShipRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;

namespace TinyZoo.GamePlay.Ships
{
  internal class ShipRenderer : GameObject
  {
    private Rectangle BaseRect = new Rectangle(200, 155, 21, 21);
    private int Frame;
    private int TotalFrames = 5;
    private bool IsVertical;
    private int TargetFrame;
    private float FrameTime;
    private float FrameRate = 0.2f;

    public ShipRenderer()
    {
      this.BaseRect = !GameFlags.BountyMode || TinyZoo.Game1.gamestate != GAMESTATE.GamePlaySetUp ? new Rectangle(200, 155, 21, 21) : new Rectangle(84, 249, 21, 21);
      this.IsVertical = true;
      this.DrawRect = this.BaseRect;
      this.DrawRect.X += (this.DrawRect.Width + 1) * this.TotalFrames;
      this.SetDrawOriginToCentre();
      this.TargetFrame = this.TotalFrames;
      this.Frame = this.TotalFrames;
    }

    public bool IsTransforming() => this.TargetFrame != this.Frame;

    public bool IsHorizontal() => this.Frame == 0;

    public void Transform()
    {
      if (this.TargetFrame > 0)
      {
        this.TargetFrame = 0;
        this.FrameTime = this.FrameRate;
      }
      else if (this.TargetFrame == 0)
      {
        this.TargetFrame = this.TotalFrames;
        this.FrameTime = this.FrameRate;
      }
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.Rotate, 0.6f);
    }

    private bool GetCanShoot() => this.TargetFrame == this.Frame;

    public void UpdateShipRenderer(float DeltaTime)
    {
      if (this.TargetFrame != this.Frame)
      {
        this.FrameTime += DeltaTime;
        if ((double) this.FrameTime > (double) this.FrameRate)
        {
          if (this.Frame > this.TargetFrame)
            --this.Frame;
          else
            ++this.Frame;
          this.FrameTime -= DeltaTime;
          if ((double) this.FrameTime > (double) this.FrameRate)
            this.FrameTime = 0.0f;
          this.DrawRect = this.BaseRect;
          this.DrawRect.X += (this.DrawRect.Width + 1) * this.Frame;
        }
      }
      this.UpdateColours(DeltaTime);
    }

    public void DrawShipRenderer()
    {
      Vector2 vLocation = this.vLocation;
      this.vLocation = this.vLocation + new Vector2(0.0f, 3f);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet, this.vLocation, this.scale, this.Rotation, this.DrawRect, 0.4f, Color.Black);
      this.vLocation = vLocation;
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }
  }
}
