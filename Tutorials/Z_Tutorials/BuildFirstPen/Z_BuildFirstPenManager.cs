// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.Z_Tutorials.BuildFirstPen.Z_BuildFirstPenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.Tutorials.BuildThing;
using TinyZoo.Z_BuldMenu.IconGrid;
using TinyZoo.Z_HUD.ControlHint;

namespace TinyZoo.Tutorials.Z_Tutorials.BuildFirstPen
{
  internal class Z_BuildFirstPenManager
  {
    private int StateCounter;
    private Arrow arrow;
    private Vector2 ArrowLocation;
    private SmartCharacterBox charactertextbox;
    private BuiodHereFootPrint buildherefootprint;
    private bool HasLaunchedHint;
    private bool[] TempForcedStates;
    private bool arrowDisabled;

    public Z_BuildFirstPenManager(Player player)
    {
      FeatureFlags.FullyBlockControlHint = true;
      this.charactertextbox = new SmartCharacterBox("Welcome to your zoo! We invested heavily in getting this business started, but now it's up to you to make it a success!", AnimalType.Administrator, _ScaleMult: Sengine.UltraWideSreenDownardsMultiplier);
      this.charactertextbox.AddNewText(new textBoxPair("To get started you need to build an animal enclosure.", AnimalType.Administrator));
      this.charactertextbox.AddNewText(new textBoxPair("Select the grass enclosure type and build a pen with at least 3 Units of space.", AnimalType.Administrator));
      this.buildherefootprint = new BuiodHereFootPrint();
      player.Stats.GiveCash(1000, player);
      FeatureFlags.BlockTicketPrice = true;
      FeatureFlags.DemolishEnabled = false;
      FeatureFlags.BlockBuild = true;
      FeatureFlags.BlockSpeedUp = true;
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = true;
      FeatureFlags.BlockStats = true;
      FeatureFlags.BlockBreeding = true;
      FeatureFlags.BlockTimer = true;
      FeatureFlags.BlockCash = true;
      FeatureFlags.BlockManage = true;
      FeatureFlags.BlockCloseBuildMenu = true;
      FeatureFlags.LockToBuildPen = true;
    }

    public bool UpdateZ_BuildFirstPenManager(
      ref float SimulationTime,
      ref float DeltaTime,
      Player player)
    {
      SimulationTime = 0.0f;
      if (this.StateCounter == 0)
      {
        if (!this.HasLaunchedHint)
        {
          FeatureFlags.FullyBlockControlHint = false;
          Z_GameFlags.ForceControllerHintsToThe = ControllerHintSummary.BaseNavigation;
          this.HasLaunchedHint = true;
        }
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player);
        if (this.charactertextbox.ThisLine == 1)
        {
          FeatureFlags.BlockBuild = false;
          ++this.StateCounter;
          this.arrow = new Arrow();
          FeatureFlags.BlockBuild = false;
          FeatureFlags.LockToBuildPen = true;
          this.ArrowLocation = new Vector2(1024f - OWCategoryButton.SizeBTN, OverwoldMainButtons.textbuttons[3].Location.Y);
        }
      }
      else if (this.StateCounter == 1)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true);
        if (OverWorldManager.overworldstate == OverWOrldState.Build)
        {
          this.arrow = new Arrow(true);
          this.arrow.SetVertical(true);
          this.ArrowLocation = Z_IconPanel.TargetBuildTileLocation;
          this.StateCounter = 2;
          Z_GameFlags.ForceControllerHintsToThe = ControllerHintSummary.BuildPen;
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
        }
      }
      else if (this.StateCounter == 2)
      {
        this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true, DoNotClearInput: true);
        this.ArrowLocation = Z_IconPanel.TargetBuildTileLocation;
        if (player.prisonlayout.cellblockcontainer.prisonzones.Count > 0)
        {
          this.StateCounter = 3;
          this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, ForceContinue: true);
          FeatureFlags.ForceExitBuild = true;
        }
        this.arrowDisabled = ObjectInfoPanel.z_penbuilder != null;
      }
      else if (this.StateCounter == 3)
      {
        if (this.charactertextbox != null && this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
        {
          this.charactertextbox = (SmartCharacterBox) null;
          FeatureFlags.LockToBuildPen = false;
        }
        this.arrow = (Arrow) null;
        FeatureFlags.BlockCloseBuildMenu = false;
        if (OverWorldManager.overworldstate != OverWOrldState.Build)
          return true;
      }
      if (this.arrow != null)
        this.arrow.UpdateArrow(DeltaTime);
      return false;
    }

    public void DrawZ_BuildFirstPenManager()
    {
      if (this.charactertextbox != null)
        this.charactertextbox.DrawSmartCharacterBox();
      if (this.arrow == null || this.arrowDisabled)
        return;
      this.arrow.DrawArrow(this.ArrowLocation);
    }
  }
}
