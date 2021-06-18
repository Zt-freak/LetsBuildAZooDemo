// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Ships.ArrowButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.GamePlay.Ships
{
  internal class ArrowButtons
  {
    private GameObject Upwards;
    private GameObject Horizontal;
    private Vector2 RootLocation;

    public ArrowButtons()
    {
      this.Upwards = new GameObject();
      this.Upwards.DrawRect = new Rectangle(256, 36, 15, 15);
      this.Upwards.SetDrawOriginToCentre();
      this.Horizontal = new GameObject();
      this.Horizontal.DrawRect = new Rectangle(272, 36, 15, 15);
      this.Horizontal.SetDrawOriginToCentre();
      this.Upwards.SetAlpha(0.0f);
      this.Horizontal.SetAlpha(0.0f);
    }

    public void UpdateArrowButtons(
      float DeltaTime,
      Vector2 vLocation,
      bool Islerping,
      bool isFiring,
      bool IsHorizontal,
      Player player,
      bool HasLeftRight,
      bool HasUp,
      out bool FiredUp,
      out bool FiredRight)
    {
      FiredUp = false;
      FiredRight = false;
      if (!GameFlags.AllowMouseControl || FeatureFlags.BlockBeamFiring || GameFlags.IsUsingController)
        return;
      if (!HasUp && (double) this.Upwards.fTargetAlpha != 0.0)
        this.Upwards.SetAlpha(true, 0.1f, 1f, 0.0f);
      if (!HasLeftRight && (double) this.Horizontal.fTargetAlpha != 0.0)
        this.Horizontal.SetAlpha(true, 0.1f, 1f, 0.0f);
      if (isFiring | Islerping)
      {
        if ((double) this.Horizontal.fTargetAlpha != 0.0)
          this.Horizontal.SetAlpha(true, 0.1f, 1f, 0.0f);
        if ((double) this.Upwards.fTargetAlpha != 0.0)
          this.Upwards.SetAlpha(true, 0.1f, 1f, 0.0f);
      }
      else
      {
        if ((double) this.Horizontal.fTargetAlpha != 1.0 & HasLeftRight)
          this.Horizontal.SetAlpha(true, 0.1f, 1f, 1f);
        if ((double) this.Upwards.fTargetAlpha != 1.0 & HasUp)
          this.Upwards.SetAlpha(true, 0.1f, 1f, 1f);
      }
      this.Upwards.UpdateColours(DeltaTime);
      this.Horizontal.UpdateColours(DeltaTime);
      this.RootLocation = RenderMath.TranslateWorldSpaceToScreenSpace(vLocation);
      this.Upwards.vLocation = this.RootLocation;
      this.Horizontal.vLocation = this.RootLocation;
      float num = Sengine.WorldOriginandScale.Z;
      if ((double) num < 2.0)
        num = 2f;
      this.Horizontal.scale = num;
      this.Upwards.scale = num;
      if ((double) this.Upwards.vLocation.Y < 200.0)
      {
        this.Upwards.vLocation.Y += 20f * num * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Upwards.Rotation = 3.141593f;
      }
      else
      {
        this.Upwards.vLocation.Y -= 20f * num * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Upwards.Rotation = 0.0f;
      }
      if ((double) this.Horizontal.vLocation.X < 900.0)
      {
        this.Horizontal.vLocation.X += 20f * num;
        this.Horizontal.Rotation = 0.0f;
      }
      else
      {
        this.Horizontal.vLocation.X -= 20f * num;
        this.Horizontal.Rotation = 3.141593f;
      }
      if ((double) this.Horizontal.fAlpha == 1.0 && !GameFlags.GamePaused && MathStuff.CheckPointCollision(true, this.Horizontal.vLocation, this.Horizontal.scale, (float) (this.Horizontal.DrawRect.Width + 4), (float) (this.Horizontal.DrawRect.Height + 4) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
      {
        player.player.touchinput.ReleaseTapArray[0].X = -10000f;
        FiredRight = true;
      }
      if ((double) this.Upwards.fAlpha != 1.0 || GameFlags.GamePaused || (!MathStuff.CheckPointCollision(true, this.Upwards.vLocation, this.Upwards.scale, (float) (this.Upwards.DrawRect.Width + 8), ((float) this.Upwards.DrawRect.Height + 8f) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]) || FiredRight))
        return;
      player.player.touchinput.ReleaseTapArray[0].X = -10000f;
      FiredUp = true;
    }

    public void DrawArrowButtons()
    {
      if (!GameFlags.AllowMouseControl || FeatureFlags.BlockBeamFiring || GameFlags.IsUsingController)
        return;
      this.Upwards.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.Horizontal.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }
  }
}
