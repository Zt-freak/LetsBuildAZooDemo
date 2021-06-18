// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.PauseScreen.PauseButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tutorials;

namespace TinyZoo.GamePlay.PauseScreen
{
  internal class PauseButton : GameObject
  {
    private bool MouseOver;
    private LerpHandler_Float lerper;

    public PauseButton()
    {
      this.DrawRect = new Rectangle(146, 0, 9, 8);
      this.SetDrawOriginToCentre();
      this.scale = 4f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, -1f, 3f);
    }

    public bool UpdatePauseButton(Player player, float DeltaTime, bool IsPlaying)
    {
      if (TutorialManager.currenttutorial == TUTORIALTYPE.GamePlayIntro || TutorialManager.currenttutorial == TUTORIALTYPE.TeachShakeShake)
        IsPlaying = false;
      this.vLocation = new Vector2(970f, 30f);
      Vector2 zero = Vector2.Zero;
      if (IsPlaying && (double) this.lerper.TargetValue != 0.0)
        this.lerper.SetLerp(false, -1f, 0.0f, 3f, true);
      if (!IsPlaying && (double) this.lerper.TargetValue != -1.0)
        this.lerper.SetLerp(false, -1f, -1f, 3f, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      int num1 = 20;
      int num2 = 10;
      if (IsPlaying && !GameFlags.GamePaused)
      {
        if (GameFlags.IsUsingMouse)
        {
          if (MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
            this.MouseOver = true;
          else if (MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation))
            this.MouseOver = true;
        }
        if (!GameFlags.IsUsingController && MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
        {
          player.player.touchinput.ReleaseTapArray[0].X = -10000f;
          return true;
        }
        if (player.inputmap.PressedThisFrame[20])
          return true;
      }
      return false;
    }

    public void DrawPauseButton()
    {
      this.fAlpha = 0.4f;
      if (this.MouseOver)
        this.fAlpha = 1f;
      Vector2 Offset = new Vector2(0.0f, this.lerper.Value * 200f);
      if (!GameFlags.IsUsingController)
        this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      this.MouseOver = false;
    }
  }
}
