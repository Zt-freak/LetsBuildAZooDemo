// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.WelcomeToTheZoo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.Z_HUD.ControlHint;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class WelcomeToTheZoo
  {
    private int StateCounter;
    private Arrow arrow;
    private Vector2 ArrowLocation;
    private SmartCharacterBox charactertextbox;

    public WelcomeToTheZoo(Player player)
    {
      this.StateCounter = 0;
      FeatureFlags.BlockExitFromWorldMap = true;
      FeatureFlags.DemolishEnabled = false;
      FeatureFlags.BlockBuild = true;
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = true;
      FeatureFlags.BlockStats = true;
      FeatureFlags.BlockBreeding = true;
      FeatureFlags.BlockTimer = true;
      FeatureFlags.BlockCash = true;
      FeatureFlags.BlockAlerts = true;
      FeatureFlags.BlockAlerts = true;
      FeatureFlags.BlockBreeding = true;
      player.Stats.GiveCash(1000, player);
      this.charactertextbox = new SmartCharacterBox("Now you have an enclosure, lets attract some customers by putting something in it!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("Zoos get animals by trading with each other, get new animals from here!", AnimalType.Administrator));
      Z_GameFlags.ForceControllerHintsToThe = ControllerHintSummary.BaseNavigation;
    }

    public void WentToQuestSelection(Player player) => this.charactertextbox.UpdateSmartCharacterBox(0.0f, player, ForceContinue: true);

    public void PressedMap()
    {
      this.arrow = (Arrow) null;
      this.charactertextbox.AutoLerpOff();
    }

    public bool UpdateWelcomeToTheZoo(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      SimulationTime = 0.0f;
      if (this.StateCounter == 0)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
        if (this.charactertextbox.ThisLine == 1)
        {
          ++this.StateCounter;
          this.arrow = new Arrow();
          FeatureFlags.BlockIntake = false;
          this.ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[1].Location.Y);
        }
      }
      else if (this.StateCounter == 1)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
        if (TinyZoo.Game1.gamestate == GAMESTATE.WorldMap)
        {
          ++this.StateCounter;
          this.charactertextbox = new SmartCharacterBox("It looks like the zoo in Australia has had a population explosion with their rabbits.", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
        }
      }
      else if (this.StateCounter == 2)
      {
        if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true))
        {
          this.charactertextbox = (SmartCharacterBox) null;
          ++this.StateCounter;
        }
      }
      else if (this.StateCounter == 3 && player.Stats.research.HasThisAnimalBeenResearched(AnimalType.Rabbit))
      {
        FeatureFlags.BlockExitFromWorldMap = false;
        return true;
      }
      if (this.arrow != null)
        this.arrow.UpdateArrow(DeltaTime);
      return false;
    }

    public void DrawWelcomeToTheZoo()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}
