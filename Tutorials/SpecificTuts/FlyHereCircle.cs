// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.FlyHereCircle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class FlyHereCircle : GameObject
  {
    public float Delay;
    public bool IsDone;

    public FlyHereCircle()
    {
      this.DrawRect = new Rectangle(856, 304, 42, 42);
      if (GameFlags.IsUsingController)
      {
        this.DrawRect.X = 813;
        this.DrawRect.Y = 369;
      }
      this.scale = 2f;
      this.SetDrawOriginToCentre();
      this.SetAlpha(false, 0.2f, 0.0f, 1f);
      this.SetAllColours(ColourData.IconYellow);
      this.bActive = false;
    }

    public bool CheckCollision(Vector2 LOC)
    {
      if (!this.bActive || this.IsDone || !MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height, LOC))
        return false;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade, Pitch: 1f);
      this.SetAllColours(ColourData.FernGreen);
      this.DrawRect = new Rectangle(899, 304, 42, 42);
      this.Delay = 0.3f;
      this.IsDone = true;
      return true;
    }

    public void UpdateFlyHereCircle(float DeltaTime, out bool ActivateNext)
    {
      ActivateNext = false;
      if (!this.bActive && !this.IsDone)
        return;
      this.ColourCycle(0.5f, ColourData.IconYellow.X, ColourData.IconYellow.Y, ColourData.IconYellow.Z, ColourData.FernGreen.X, ColourData.FernGreen.Y, ColourData.FernGreen.Z);
      this.UpdateColours(GameFlags.RefDeltaTime);
      if (this.IsDone && (double) this.Delay > 0.0)
      {
        this.Delay -= DeltaTime;
        if ((double) this.Delay <= 0.0)
        {
          ActivateNext = true;
          this.SetAlpha(false, 0.3f, 1f, 0.0f);
        }
      }
      this.UpdateColours(DeltaTime);
    }

    public void DrawFlyHereCircle()
    {
      if (!this.bActive && !this.IsDone)
        return;
      this.scale = 0.7f;
      this.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
    }
  }
}
