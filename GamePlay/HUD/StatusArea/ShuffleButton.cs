// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.HUD.StatusArea.ShuffleButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Tutorials;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.GamePlay.HUD.StatusArea
{
  internal class ShuffleButton : GameObject
  {
    private bool MouseOver;
    private Rectangle baserect;
    private LerpHandler_Float lerper;
    private TRC_ButtonDisplay ControllerButton;
    internal static float XPos = 650f;
    private int PressCunter;

    public ShuffleButton()
    {
      this.baserect = new Rectangle(983, 434, 30, 30);
      this.DrawRect = this.baserect;
      this.SetDrawOriginToCentre();
      this.scale = 3f;
      this.vLocation = new Vector2(ShuffleButton.XPos, 30f * Sengine.UltraWideSreenUpwardsMultiplier);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.ControllerButton = new TRC_ButtonDisplay(3f);
      this.ControllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, SEngine.ControllerButton.XboxY);
      if (!TinyZoo.GameFlags.MobileUIScale)
        return;
      this.vLocation = new Vector2(980f, (float) (768.0 - (double) this.scale * 15.0 * (double) Sengine.Portrait_ScreenRatioUpwardsMultiplier.Y));
    }

    public bool UpdateShuffleButton(
      Player player,
      float DeltaTime,
      bool BeamFiring,
      bool IsResultsOrIntro)
    {
      if (TinyZoo.GameFlags.IsUsingController && TutorialManager.currenttutorial != TUTORIALTYPE.TeachShakeShake)
      {
        this.vLocation = new Vector2(ShuffleButton.XPos, 30f * Sengine.UltraWideSreenUpwardsMultiplier);
        this.scale = 3f;
      }
      else if (TinyZoo.GameFlags.MobileUIScale || TutorialManager.currenttutorial != TUTORIALTYPE.TeachShakeShake)
      {
        this.scale = 2.5f;
        this.vLocation = new Vector2(980f, (float) (768.0 - (double) this.scale * 15.0 * (double) Sengine.Portrait_ScreenRatioUpwardsMultiplier.Y));
      }
      if (IsResultsOrIntro)
      {
        if ((double) this.lerper.TargetValue != -1.0)
          this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
      }
      else if ((double) this.lerper.TargetValue != 0.0)
        this.lerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
      if (FeatureFlags.BlockShake)
        return TinyZoo.GameFlags.IsArcadeMode && !BeamFiring && player.inputmap.HeldButtons[21];
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None && TutorialManager.currenttutorial != TUTORIALTYPE.TeachShakeShake)
        BeamFiring = true;
      this.scale = 1.5f;
      if (TinyZoo.GameFlags.MobileUIScale)
        this.scale = 2f;
      this.fAlpha = 1f;
      if (TinyZoo.GameFlags.MobileUIScale)
        this.fAlpha = 0.8f;
      if (BeamFiring)
        this.fAlpha = 0.1f;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value < 0.0)
        return false;
      int num1 = 40;
      int num2 = 20;
      if (BeamFiring)
      {
        if (MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) (this.DrawRect.Width + num1), (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
          player.player.touchinput.ReleaseTapArray[0].X = -1000f;
        return false;
      }
      Vector2 zero = Vector2.Zero;
      if (MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation))
        this.MouseOver = true;
      if (player.inputmap.PressedThisFrame[12])
      {
        this.PressCunter = 5;
        return true;
      }
      if (!MathStuff.CheckPointCollision(true, this.vLocation + zero, this.scale, (float) (this.DrawRect.Width + num1), (float) (this.DrawRect.Height + num2) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
        return false;
      player.player.touchinput.ReleaseTapArray[0].X = -1000f;
      this.PressCunter = 5;
      return true;
    }

    public void DrawShuffleButton()
    {
      if (FeatureFlags.BlockShake)
        return;
      this.DrawRect = this.baserect;
      if (this.MouseOver || this.PressCunter > 0)
      {
        --this.PressCunter;
        this.DrawRect.Y += this.baserect.Height + 1;
      }
      this.MouseOver = false;
      float num = 200f;
      if (TinyZoo.GameFlags.MobileUIScale && !TinyZoo.GameFlags.IsUsingController)
        num = -250f;
      this.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, new Vector2(0.0f, this.lerper.Value * num));
      if (this.ControllerButton == null || !TinyZoo.GameFlags.IsUsingController)
        return;
      this.ControllerButton.scale = 3f;
      this.ControllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.vLocation + new Vector2(-40f, this.lerper.Value * 200f), this.fAlpha);
    }
  }
}
