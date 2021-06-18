// Decompiled with JetBrains decompiler
// Type: TinyZoo.FeatureFlags
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;
using TinyZoo.Z_Manage.MainButtons;

namespace TinyZoo
{
  internal class FeatureFlags
  {
    internal static bool InstantBlockTopBar = false;
    internal static bool FlashBuildShop;
    internal static bool FlashBuildDrinksShop;
    internal static bool FlashBuildFoodShop;
    internal static bool FlashBuildGiftShop;
    internal static bool FlashHireStaffFromShop;
    internal static bool FlashCRISPRFromBirth;
    internal static bool FlashHireFromGateForQuest;
    internal static bool FlashBuildFacility;
    internal static bool FlashBuildDecoration;
    internal static bool FlashBuildBench;
    internal static bool FlashBuildBin;
    internal static bool FlashBuildToilet;
    internal static bool FlashBuildPen;
    internal static bool FlashBuildStoreRoom;
    internal static bool FlashCRISPRFromTask;
    internal static bool FlashResearchFromTask;
    internal static bool FlashStoreRoomFromTask;
    internal static bool FlashHasApplicantsAtGate;
    internal static bool BlockBackFromMainBar;
    internal static bool FoodAndDeathEnabled = true;
    internal static bool FlashBuildFromTask = false;
    internal static bool FlashBuildFromNotificationTrack = true;
    internal static bool FlashTrade = true;
    internal static bool BlockMouseOverOnBuildBar = false;
    internal static bool BlockBuyLand = true;
    internal static bool BlockStats = false;
    internal static bool BlockPersonInfo = false;
    internal static bool BlockSpeedUp = false;
    internal static bool BlockCarbon = false;
    private static bool _BlockBuild = false;
    internal static bool NewAnimalGot = false;
    internal static bool BlockAlerts = false;
    internal static bool BlockExitFromBreed = false;
    private static bool BreedBlocked = true;
    internal static bool BlockIntake = false;
    internal static bool BlockSettings = false;
    internal static bool BlockTicketPrice = false;
    internal static bool BlockTimer = false;
    private static bool CashBlocked = false;
    internal static bool VirtualSTickEnabled = true;
    internal static bool GraveYrardCreatorActive = false;
    internal static bool BlockPremiumStore_DEPRICATED = false;
    internal static bool BlockManage = false;
    internal static bool BlockShake = false;
    internal static bool ForceExitBuild = false;
    internal static bool BlockPlayerMoveCameraDuringCellBlockDrag;
    private static bool _BlockAllUI = true;
    internal static bool FullyBlockControlHint = false;
    internal static bool LockToBuildPen = false;
    internal static bool BlockCloseNotifcation = false;
    internal static bool BlockExitFromManage = false;
    internal static bool BlockDayUI = false;
    internal static ManageButtonType DarkenAllButThisInMANAGE = ManageButtonType.Count;
    internal static bool AllowPenSelectOnly = false;
    internal static bool BlockExitFromWorldMap = false;
    internal static bool ForceAllowBuild = false;
    internal static bool ForceAllowWorldMap = false;
    internal static bool ForceAllowControlHint = false;
    internal static bool BlockEnemySpawn = false;
    internal static bool BlockDroneMovement = false;
    internal static bool BlockBeamFiring = false;
    internal static bool ShowGamePlayProgressBar = true;
    internal static bool ShowGamePlayPeopleToSave = true;
    internal static bool ShowGamePlayBeams = true;
    internal static bool DemolishEnabled = true;
    internal static int ShipMoved;
    internal static Vector2 ShipMovedHere;
    internal static bool BlockPlayerMoveCamera = false;
    internal static bool DrawRoofInOverWorld = true;
    internal static bool BlockPageCycleInBuild = false;
    internal static bool LockToBuildToCurrentLocation = false;
    internal static bool BlockCloseBuildMenu = false;
    internal static bool ForceActivateStatsBar = false;
    internal static List<TILETYPE> OnlyALlowTisThingsToBeBuilt;
    internal static bool BlockBuyPanel = false;
    internal static bool DoingMainCameraCutSceneIntro = false;

    internal static bool BlockBuild
    {
      get => FeatureFlags._BlockBuild;
      set => FeatureFlags._BlockBuild = value;
    }

    internal static bool BlockAllUI
    {
      get => FeatureFlags._BlockAllUI;
      set => FeatureFlags._BlockAllUI = value;
    }

    internal static bool BlockCash
    {
      get => FeatureFlags.CashBlocked;
      set => FeatureFlags.CashBlocked = value;
    }

    internal static bool BlockBreeding
    {
      get => FeatureFlags.BreedBlocked;
      set => FeatureFlags.BreedBlocked = value;
    }

    internal static bool GetIsThisSubIconBlocked(OverworldButtons overworldbutton)
    {
      if (GameFlags.IsUsingController && SellUIManager.selectedtileandsell != null)
        return true;
      switch (overworldbutton)
      {
        case OverworldButtons.Settings:
          return FeatureFlags.BlockSettings || FeatureFlags.BlockAllUI;
        case OverworldButtons.Intake:
          if (FeatureFlags.BlockIntake)
            return true;
          return FeatureFlags.BlockAllUI && !FeatureFlags.ForceAllowWorldMap;
        case OverworldButtons.Breeding:
          return FeatureFlags.BlockBreeding || FeatureFlags.BlockAllUI;
        case OverworldButtons.Build:
          if (FeatureFlags.BlockBuild)
            return true;
          return FeatureFlags.BlockAllUI && !FeatureFlags.ForceAllowBuild;
        case OverworldButtons.Manage:
          return FeatureFlags.BlockManage || FeatureFlags.BlockAllUI;
        case OverworldButtons.Store:
          return DebugFlags.IsPCVersion || FeatureFlags.BlockPremiumStore_DEPRICATED;
        case OverworldButtons.AlertBuilding:
        case OverworldButtons.AlertStaff:
        case OverworldButtons.AlertAnimals:
        case OverworldButtons.AlertEmergency:
          return FeatureFlags.BlockAlerts || FeatureFlags.BlockAllUI;
        case OverworldButtons.HeatMapView:
          return FeatureFlags.BlockAllUI;
        default:
          return false;
      }
    }

    internal static void SetFlagsOnStartCamCutScene()
    {
      FeatureFlags.DoingMainCameraCutSceneIntro = true;
      FeatureFlags.BlockStats = true;
      FeatureFlags.BlockBuild = true;
      FeatureFlags.BlockBreeding = true;
      FeatureFlags.BlockIntake = true;
      FeatureFlags.BlockSettings = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = true;
      FeatureFlags.BlockTimer = true;
      FeatureFlags.DemolishEnabled = false;
      FeatureFlags.BlockCash = true;
    }

    internal static void EndIntroCameraCitScene()
    {
      if (TutorialManager.currenttutorial == TUTORIALTYPE.WelcomeToTheZoo || TutorialManager.currenttutorial == TUTORIALTYPE.RevealPrison)
        return;
      FeatureFlags.DoingMainCameraCutSceneIntro = false;
      FeatureFlags.BlockStats = false;
      FeatureFlags.BlockBuild = false;
      FeatureFlags.BlockIntake = false;
      FeatureFlags.BlockSettings = false;
      FeatureFlags.BlockTimer = false;
      FeatureFlags.BlockCash = false;
      FeatureFlags.DemolishEnabled = true;
      FeatureFlags.BlockPremiumStore_DEPRICATED = false;
    }

    internal static void ResetFeatureFlagsForArcadeMode()
    {
      FeatureFlags.BlockEnemySpawn = false;
      FeatureFlags.BlockDroneMovement = false;
      FeatureFlags.BlockBeamFiring = false;
      FeatureFlags.ShowGamePlayProgressBar = true;
      FeatureFlags.ShowGamePlayPeopleToSave = true;
      FeatureFlags.ShowGamePlayBeams = true;
    }
  }
}
