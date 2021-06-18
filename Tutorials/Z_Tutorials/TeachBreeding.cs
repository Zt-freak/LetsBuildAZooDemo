// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.TeachBreeding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;

namespace TinyZoo.Tutorials.Z_Tutorials
{
  internal class TeachBreeding
  {
    private int StateCounter;
    private Arrow arrow;
    private Vector2 ArrowLocation;
    private SmartCharacterBox charactertextbox;

    public TeachBreeding()
    {
      FeatureFlags.BlockBuild = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = true;
      FeatureFlags.BlockExitFromBreed = true;
      this.charactertextbox = new SmartCharacterBox("Two rabbits! That's amazing, but three rabbits is better than two. It's time to start breeding animals!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("Tap button to go to the breeding area.", AnimalType.Administrator));
    }

    public void ActivatedPetSelectManager()
    {
      if (this.StateCounter != 2)
        return;
      ++this.StateCounter;
      this.charactertextbox = new SmartCharacterBox("Select the Rabbit", AnimalType.Administrator, true, Sengine.UltraWideSreenDownardsMultiplier);
      ++this.StateCounter;
    }

    public void GoToBreedPairingView()
    {
      ++this.StateCounter;
      this.charactertextbox = new SmartCharacterBox("Select the pair of rabbits, and then let nature take its course.", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      ++this.StateCounter;
    }

    public void BreedButtonPressed()
    {
      this.StateCounter = 100;
      FeatureFlags.BlockIntake = false;
      FeatureFlags.BlockExitFromBreed = false;
      FeatureFlags.BlockBuild = false;
      FeatureFlags.DemolishEnabled = true;
    }

    public bool UpdateTeachBreeding(ref float SimulationTime, ref float DeltaTime, Player player)
    {
      if (this.StateCounter == 0)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
        if (this.charactertextbox.ThisLine == 1)
        {
          FeatureFlags.BlockBreeding = false;
          ++this.StateCounter;
          this.arrow = new Arrow();
          this.ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[2].Location.Y);
        }
      }
      else if (this.StateCounter == 1)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
        if (OverWorldManager.overworldstate == OverWOrldState.Shop)
        {
          this.arrow = (Arrow) null;
          ++this.StateCounter;
          this.charactertextbox = new SmartCharacterBox("Here we use selective breeding to try and bring as much variety to our animals as possible.~Tap to go view the available options.", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
        }
      }
      else if (this.StateCounter == 2)
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      else if (this.StateCounter == 3)
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      else if (this.StateCounter == 4)
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      if (this.arrow != null)
        this.arrow.UpdateArrow(DeltaTime);
      return this.StateCounter == 100;
    }

    public void DrawTeachBreeding()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}
