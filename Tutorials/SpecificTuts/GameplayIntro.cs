// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.SpecificTuts.GameplayIntro
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.Tutorials.Informatic;

namespace TinyZoo.Tutorials.SpecificTuts
{
  internal class GameplayIntro
  {
    private SmartCharacterBox charactertextbox;
    private int StateCounter;
    private Play_Informatic playinformatic;
    private float Timer;
    private FlyHereTutorial flyhere;
    private bool IsSHowingBarThing;
    private bool FinalBit;
    private int ControllerStep;
    private bool LockedToControllerTutorial;

    public GameplayIntro()
    {
      this.StateCounter = 0;
      FeatureFlags.VirtualSTickEnabled = false;
      FeatureFlags.BlockEnemySpawn = true;
      FeatureFlags.BlockDroneMovement = true;
      FeatureFlags.BlockBeamFiring = true;
      this.charactertextbox = new SmartCharacterBox(" ", AnimalType.Administrator);
      this.charactertextbox.Delay = 0.1f;
      FeatureFlags.ShowGamePlayBeams = false;
      FeatureFlags.ShowGamePlayPeopleToSave = false;
      FeatureFlags.ShowGamePlayProgressBar = false;
    }

    public bool UpdateGameplayIntro(
      ref float DeltaTime,
      Player player,
      ref Arrow arrow,
      ref Vector2 ArrowLocation)
    {
      if (this.StateCounter == 0)
      {
        if (this.charactertextbox.UpdateSmartCharacterBox(GameFlags.RefDeltaTime, player, (double) DeltaTime == 0.0, (double) DeltaTime > 0.0))
        {
          this.charactertextbox = (SmartCharacterBox) null;
          this.StateCounter = 1;
          this.playinformatic = new Play_Informatic();
        }
      }
      else if (this.StateCounter == 1)
      {
        if (this.playinformatic.UpdatePlay_Informatic(DeltaTime, player))
        {
          this.playinformatic = (Play_Informatic) null;
          this.StateCounter = 2;
          FeatureFlags.BlockDroneMovement = false;
          this.flyhere = new FlyHereTutorial();
          string FirstText = "";
          string _Text = "";
          if (GameFlags.IsUsingController)
          {
            FirstText = "Use the stick to fly your drone to a new location.";
            _Text = "Deploy a laser now.";
          }
          this.charactertextbox = new SmartCharacterBox(FirstText, AnimalType.Administrator);
          this.charactertextbox.AddNewText(new textBoxPair(_Text, AnimalType.Administrator));
          if (GameFlags.IsUsingController)
          {
            this.LockedToControllerTutorial = true;
            this.charactertextbox.AddControllerButtonToLasttextBoxPair(new TinyTextAndButton(ControllerButton.XboxA, "Create laser"));
            this.charactertextbox.AddNewText(new textBoxPair("You can deploy lasers in two directions.~Try rotating now.", AnimalType.Administrator, SetToBottom: true));
            this.charactertextbox.AddControllerButtonToLasttextBoxPair(new TinyTextAndButton(ControllerButton.XboxX, "Rotate"));
            this.charactertextbox.AddNewText(new textBoxPair("Now try deploying another laser.", AnimalType.Administrator, SetToBottom: true));
            this.charactertextbox.AddControllerButtonToLasttextBoxPair(new TinyTextAndButton(ControllerButton.XboxA, "Create laser"));
          }
          this.charactertextbox.AddNewText(new textBoxPair("", AnimalType.Administrator, SetToBottom: true));
        }
        player.inputmap.ClearAllInput(player);
        DeltaTime = 0.0f;
      }
      else if (this.StateCounter == 2)
      {
        player.inputmap.PressedThisFrame[1] = false;
        this.flyhere.UpdateFlyHereTutorial(DeltaTime);
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, !this.flyhere.IsComplete(), this.flyhere.IsComplete());
        if (this.charactertextbox.ThisLine >= 1)
        {
          this.StateCounter = 3;
          this.IsSHowingBarThing = true;
          FeatureFlags.BlockBeamFiring = false;
        }
      }
      else if (this.IsSHowingBarThing && !this.FinalBit)
      {
        if (GameFlags.IsUsingController)
        {
          if (GameFlags.BeamsLockedOrDead == 1 && this.ControllerStep == 0)
          {
            this.ControllerStep = 1;
            this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
          }
          if (player.inputmap.PressedThisFrame[2])
          {
            if (this.ControllerStep == 0 || this.ControllerStep == 2)
            {
              if (this.charactertextbox.ThisLine == 2 && this.LockedToControllerTutorial)
                this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
              else
                player.inputmap.PressedThisFrame[2] = false;
            }
            else
            {
              this.ControllerStep = 2;
              this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
              player.inputmap.PressedThisFrame[2] = true;
            }
          }
          if (player.inputmap.PressedThisFrame[1] && this.ControllerStep == 1)
            player.inputmap.PressedThisFrame[1] = false;
        }
        this.flyhere.UpdateFlyHereTutorial(DeltaTime);
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, GameFlags.BeamsLockedOrDead < 2, GameFlags.BeamsLockedOrDead == 2);
        if (this.charactertextbox.ThisLine >= 2 && !this.LockedToControllerTutorial || this.charactertextbox.ThisLine >= 4 && this.LockedToControllerTutorial)
        {
          this.FinalBit = true;
          this.StateCounter = 4;
          FeatureFlags.VirtualSTickEnabled = true;
          FeatureFlags.ShowGamePlayProgressBar = true;
          FeatureFlags.BlockBeamFiring = true;
          FeatureFlags.BlockDroneMovement = true;
          this.Timer = 0.0f;
          arrow = new Arrow();
          arrow.Rotation = 3.141593f;
          ArrowLocation.X = 280f;
          ArrowLocation.Y = 35f;
        }
      }
      else if (this.FinalBit)
      {
        this.flyhere.UpdateFlyHereTutorial(DeltaTime);
        this.Timer += DeltaTime;
        player.inputmap.PressedThisFrame[1] = false;
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, (double) this.Timer < 2.0))
          return true;
      }
      return false;
    }

    public void DrawGameplayIntro()
    {
      if (this.flyhere != null)
        this.flyhere.DrawFlyHereTutorial();
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.playinformatic == null)
        return;
      this.playinformatic.DrawPlay_Informatic();
    }
  }
}
