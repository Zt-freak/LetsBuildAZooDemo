// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.ShakeShakeTutorial
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.HUD.StatusArea;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class ShakeShakeTutorial
  {
    private SmartCharacterBox charactertextbox;

    public ShakeShakeTutorial(ref Arrow arrow, ref Vector2 ArrowLocation, Player player)
    {
      arrow = new Arrow(true);
      float x = ShuffleButton.XPos;
      float y = 40f * Sengine.UltraWideSreenUpwardsMultiplier;
      if (GameFlags.MobileUIScale)
      {
        x = 980f;
        y = (float) (768.0 - 100.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
      ArrowLocation = new Vector2(x, y);
      arrow.Rotation = -1.570796f;
      if (GameFlags.MobileUIScale)
        arrow.Rotation = 1.570796f;
      FeatureFlags.BlockBeamFiring = true;
      FeatureFlags.BlockDroneMovement = true;
      this.charactertextbox = new SmartCharacterBox("You can randomize the locations of the inmates with the shake button.~Press it now!", AnimalType.Administrator, !GameFlags.MobileUIScale);
    }

    public bool UpdateShakeShakeTutorial(float DeltaTime, Player player)
    {
      player.inputmap.PressedThisFrame[1] = false;
      return PRISONPLANET_GamePlayManager.lockdownintro == null && this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, !GameFlags.JustShuffled, GameFlags.JustShuffled);
    }

    public void DrawShakeShakeTutorial() => this.charactertextbox.DrawSmartCharacterBox();
  }
}
