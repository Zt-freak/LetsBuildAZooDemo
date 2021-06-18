// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.OverWorldEnvironmentManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.Fog;
using TinyZoo.OverWorld.OverWorldEnv.Ground;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;
using TinyZoo.Utils.DeveloperMenu;
using TinyZoo.Z_BuldMenu;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers;
using TinyZoo.Z_OverWorld.Airspace;
using TinyZoo.Z_Threading;

namespace TinyZoo.OverWorld.OverWorldEnv
{
  internal class OverWorldEnvironmentManager
  {
    private FogManager fogmanager;
    private GroundManager groundmanager;
    public WallsAndFloorsManager wallsandfloors;
    public AnimalsInPens animalsinpens;
    internal static OverWorldCamera overworldcam;
    internal static float FlashTimer;
    private float UpwardsOnConstruct;
    internal static AirspaceManager airspacemanager;
    internal static DeadPeople deadPeople;
    private float GameIntroTimer;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public OverWorldEnvironmentManager(Player player)
    {
      if (!OverWorldManager.heatmapmanager.IsWaterMapMade())
        OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
      OverWorldEnvironmentManager.airspacemanager = new AirspaceManager();
      OverWorldEnvironmentManager.deadPeople = new DeadPeople();
      this.fogmanager = new FogManager();
      this.groundmanager = new GroundManager();
      this.wallsandfloors = new WallsAndFloorsManager(player.prisonlayout.layout);
      if (DebugFlags.LoadYvonnesZoo)
        RebuildShopStatuses.DoRebuildShopStatuses(player);
      Z_GameFlags.pathfinder.RecreateCityBlocks(player);
      CustomerManager.RemoveAllAnimals();
      this.animalsinpens = new AnimalsInPens(player);
      OverWorldEnvironmentManager.overworldcam = new OverWorldCamera(player, TargetPixelSnap: 1f);
      if (GameFlags.PhotoMode)
        OverWorldEnvironmentManager.overworldcam.SetPhotoMode();
      else if (OverWorldManager.IsGameIntro)
      {
        if (PlayerStats.LandSize > 0)
        {
          OverWorldManager.IsGameIntro = false;
        }
        else
        {
          this.GameIntroTimer = 0.0f;
          if (TutorialManager.currenttutorial == TUTORIALTYPE.None)
            FeatureFlags.SetFlagsOnStartCamCutScene();
          OverWorldEnvironmentManager.overworldcam.DoIntro();
        }
      }
      this.UpwardsOnConstruct = Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Enemy GetRandomPerson() => this.animalsinpens.GetRandomPerson();

    public void SellStructure(
      Vector2Int position,
      LayoutEntry _layoutentry,
      LayoutData layoutdata,
      bool ForceSell = false,
      bool SkipRemakeList = false)
    {
      this.wallsandfloors.SellStructure(position, _layoutentry, layoutdata, ForceSell, SkipRemakeList);
    }

    public void DeletePeopleAfterSellingPrison(int CellUID) => this.animalsinpens.DeletePeopleAfterSellingPrison(CellUID);

    public void UpdateDrawFlag() => this.wallsandfloors.UpdateDrawFlag();

    public void UpdateOverWorldEnvironmentManager(
      float SimulationTime,
      Player player,
      float DeltaTime)
    {
      if (PlayerStats.LanguageChanged_RemakeGameLogo)
        PlayerStats.LanguageChanged_RemakeGameLogo = false;
      if ((double) Math.Abs(this.UpwardsOnConstruct - Sengine.ScreenRatioUpwardsMultiplier.Y) > 1.0 / 1000.0)
      {
        this.groundmanager = new GroundManager();
        this.wallsandfloors = new WallsAndFloorsManager(player.prisonlayout.layout);
        Z_GameFlags.pathfinder.RecreateCityBlocks(player);
        this.animalsinpens = new AnimalsInPens(player);
        this.UpwardsOnConstruct = Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      if (OverWorldManager.IsGameIntro)
      {
        this.GameIntroTimer += DeltaTime;
        if ((double) this.GameIntroTimer > 1.0)
        {
          OverWorldEnvironmentManager.overworldcam.DoPanForIntro();
          if (TutorialManager.currenttutorial != TUTORIALTYPE.WelcomeToTheZoo)
          {
            FeatureFlags.DrawRoofInOverWorld = false;
            FogManager.StartFogFade(2f, 0.0f);
          }
        }
      }
      OverWorldEnvironmentManager.airspacemanager.UpdateAirspaceManager(SimulationTime, DeltaTime, player);
      OverWorldEnvironmentManager.FlashTimer += DeltaTime;
      if ((double) OverWorldEnvironmentManager.FlashTimer > 2.0)
        OverWorldEnvironmentManager.FlashTimer = 0.0f;
      this.wallsandfloors.UpdateWallsAndFloorsManager(DeltaTime, SimulationTime);
      if (GameFlags.GraveYardUpdated)
      {
        this.wallsandfloors.VallidateGraveYardAndApplyToLayout(player);
        this.wallsandfloors.RemakeTileList();
        GameFlags.GraveYardUpdated = false;
      }
      if (OverWorldManager.overworldstate != OverWOrldState.Intake && OverWorldManager.overworldstate != OverWOrldState.QuickPickEmployee && (OverWorldManager.overworldstate != OverWOrldState.Shop && TinyZoo.Game1.gamestate != GAMESTATE.ManageShop))
      {
        bool AllowZoom = OverWorldManager.overworldstate == OverWOrldState.Build && ThingToBuildManager.placetype == PlaceType.PlacingCellBlock || OverWorldManager.overworldstate == OverWOrldState.GraveYard || GameFlags.PhotoMode;
        if (OverWorldManager.overworldstate != OverWOrldState.Build || !DebugFlags.IsPCVersion || !Z_BuildingIconPanel.DisableDrag(player))
          OverWorldEnvironmentManager.overworldcam.UpdateWorldCamera(DeltaTime, player, OverWorldManager.IsGameIntro || FeatureFlags.BlockPlayerMoveCamera, FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag, AllowZoom, this.animalsinpens);
      }
      this.groundmanager.UpdateGroundManager();
      this.animalsinpens.UpdatePeopleAndBeamsManager(SimulationTime);
      this.fogmanager.UpateFogManager(DeltaTime);
    }

    internal static bool IsOverBuilding(Vector2 ScreenSPaceLocation, Player player)
    {
      Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(ScreenSPaceLocation);
      if (TileMath.TileIsInWorld(spaceToTileLocation.X, spaceToTileLocation.Y))
      {
        LayoutEntry thisDungeonTile = player.prisonlayout.GetThisDungeonTile(spaceToTileLocation.X, spaceToTileLocation.Y);
        if (thisDungeonTile.tiletype != TILETYPE.None && thisDungeonTile.tiletype != TILETYPE.PinkMoonPlant && (thisDungeonTile.tiletype != TILETYPE.BoundaryTree && !TileData.GetIsPrisonWall(thisDungeonTile.tiletype)))
          return true;
      }
      return false;
    }

    public void DrawOverWorldEnvironmentManager()
    {
      if (TrailerDemoFlags.HasTrailerFlag)
        TinyZoo.Game1.ClsCLR.SetAllColours(TrailerDemoFlags.FloorColour);
      Thread_FloorRender.DrawFloor(this.groundmanager, this.wallsandfloors);
      if (OverWorldManager.overworldstate == OverWOrldState.Build)
      {
        if (Z_DebugFlags.UseRenderThreading)
          throw new Exception("JAMES YOU NEED TO REWRITE THIS FOR THREADING - JUST A TEST TO SEE FI IT LOOKS OK");
        Z_GameFlags.pathfinder.DrawEntranceBlocks();
      }
      OverWorldEnvironmentManager.deadPeople.DrawDeadPeople();
      this.wallsandfloors.DrawWallsAndFloorsManager(false, ref OverWorldEnvironmentManager.ThreadLoc, ref OverWorldEnvironmentManager.ThreadScale);
      this.animalsinpens.DRawPeopleAndBeamsManager();
      if (!LiveStats.IsChristmas)
        return;
      this.fogmanager.DrawFogManager();
    }

    public void DrawOverWorldEnvironmentManagerAfterBus()
    {
      this.wallsandfloors.DrawWallsAndFloorsManager(true, ref OverWorldEnvironmentManager.ThreadLoc, ref OverWorldEnvironmentManager.ThreadScale);
      if (!TrailerDemoFlags.HasTrailerFlag || TrailerDemoFlags.DrawEffectsLayer)
        this.wallsandfloors.smearmanager.DrawSmearManager();
      OverWorldEnvironmentManager.airspacemanager.DrawAirspaceManager();
    }
  }
}
