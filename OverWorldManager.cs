// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorldManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Localization;
using SpringIAP;
using TinyZoo.Audio;
using TinyZoo.FullScreenAdvert;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.PauseScreen;
using TinyZoo.GenericUI.FakeMouse;
using TinyZoo.OverWorld;
using TinyZoo.OverWorld.GraveYardCutScene;
using TinyZoo.OverWorld.Intake;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.Fog;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.OverWorld.OverworldSelectedThing;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.OverWorld.SelectCell;
using TinyZoo.OverWorld.Speech;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Store_Local;
using TinyZoo.Tutorials;
using TinyZoo.Utils;
using TinyZoo.Z_BalanceSystems.CustomerStats;
using TinyZoo.Z_BarMenu.Land;
using TinyZoo.Z_BuldMenu.BuyLand;
using TinyZoo.Z_BuldMenu.MoveBuilding;
using TinyZoo.Z_Bus;
using TinyZoo.Z_ControllerLayouts;
using TinyZoo.Z_CustomizePen;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_EditZone;
using TinyZoo.Z_Employees;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_Events;
using TinyZoo.Z_Garbage;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD;
using TinyZoo.Z_Manage;
using TinyZoo.Z_Morality;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_OverWorld.AvatarUI;
using TinyZoo.Z_Particles;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_PeopleSummary;
using TinyZoo.Z_TicketPrice;
using TinyZoo.Z_Trailer;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo
{
  internal class OverWorldManager
  {
    private AvatarUIManager avatarUIManager;
    internal static Z_EventsManager eventsmanager;
    internal static DayNightManager z_daynightmanager;
    private static OverWOrldState _overworldstate = OverWOrldState.MainMenu;
    private OverwoldMainButtons overworldbuttons;
    private OverworldBuildManager buildmanager;
    private StoreLocalManager breedmanager;
    private IntakeManager intakemanager;
    private OverWorldHeatMapManager overworldheatmapviewer;
    private Z_ManageManager managemanager;
    internal static OverWorldEnvironmentManager overworldenvironment;
    private OWHUDManager hudmanager;
    internal static HeatMapManager heatmapmanager = new HeatMapManager();
    internal static Pen_SelectedPenManager customizepenmanager;
    internal static FiringManager firingmanager;
    internal static TicketPriceManager_Holder ticketPriceManager;
    internal static ZoneEditManager zoneEditManager;
    internal static Z_BuyLand buymoreland;
    internal static SelectedThingManager selectedthingmanager;
    private CellSelectManager cellselectmanger;
    internal static bool IsGameIntro = true;
    private GraveYardManager graveYardManager;
    private FakeMouseManager fakemouse;
    private bool PhotoModeMultiTouchFlag;
    internal static FullScreenAdvertManager fulladvertmanager;
    private bool ForceToStore;
    private SpeechManager speechmanager;
    internal static ZooPopUps zoopopupHolder = new ZooPopUps();
    internal static Z_BusManager zbus;
    internal static Z_MoveBuildingManager movebuilding;
    internal static CustomerManager crowd;
    private MoneyRenderer moneyrenderer;
    internal static InfoPopUpManager Z_infopopupmanager;
    internal static Z_TrashManager trashmanager;
    private ZHudManager ZHUD;
    internal static ParticleManager particlemanager;
    internal static QuickPickEmployeeManager quickpickemployeemanager;
    private Controler_OverworldMatrix controlleroverirde;
    public Z_Trailermanager trailermanager;
    internal static Z_GarbageManager garbagemanager;
    public PauseScreenManager pausescreen;

    public OverWorldManager(Player player)
    {
      OverWorldManager.garbagemanager = new Z_GarbageManager(player);
      if (TrailerDemoFlags.HasTrailerFlag)
        this.trailermanager = new Z_Trailermanager(player);
      this.buildmanager = new OverworldBuildManager(player);
      OverWorldManager.particlemanager = new ParticleManager();
      this.ZHUD = new ZHudManager(player);
      OverWorldManager.eventsmanager = new Z_EventsManager(player);
      if (OverWorldManager.z_daynightmanager == null)
        OverWorldManager.z_daynightmanager = new DayNightManager();
      if (OverWorldManager.zbus == null)
        OverWorldManager.zbus = new Z_BusManager(player);
      bool flag = false;
      if (OverWorldManager.crowd == null)
      {
        OverWorldManager.crowd = new CustomerManager();
        if (!TrailerDemoFlags.PlayBusIntro)
          OverWorldManager.crowd.AddZooKeeper(player);
        flag = true;
      }
      OverWorldManager.trashmanager = new Z_TrashManager(player);
      GameFlags.BountyMode = false;
      OverWorldManager.fulladvertmanager = (FullScreenAdvertManager) null;
      this.moneyrenderer = new MoneyRenderer();
      this.speechmanager = new SpeechManager();
      if (!FullScreenAdvertManager.ShownPopUp && !OverWorldManager.IsGameIntro && !GameFlags.PhotoMode)
      {
        FullScreenAdvertManager.ShownPopUp = true;
        if (!GameFlags.IsConsoleVersion && player.Stats.TutorialsComplete[15])
        {
          if (!player.Stats.ADisabled(true, player) && player.playerbehaviour.GetTotalUniqueDaysAppLaunchedOnAsInt() >= 2)
            OverWorldManager.fulladvertmanager = new FullScreenAdvertManager(true);
          else if (!player.Stats.Vortex() && player.playerbehaviour.GetTotalUniqueDaysAppLaunchedOnAsInt() >= 2)
            OverWorldManager.fulladvertmanager = new FullScreenAdvertManager(false);
          else if (!player.Stats.GetFlower() && player.playerbehaviour.GetTotalUniqueDaysAppLaunchedOnAsInt() >= 2 && PlayerStats.language == Language.English)
            OverWorldManager.fulladvertmanager = new FullScreenAdvertManager(false, true);
        }
      }
      ControllerType controllerType = ControllerInfo.GetControllerType(0);
      if (controllerType != ControllerType.Count)
        GameFlags.SelectedControllerType = controllerType;
      player.Stats.research.AllComplete();
      IAPHolder.CheckPurchases(SpringIAPManager.Instance, player);
      OverWorldManager.overworldstate = OverWOrldState.MainMenu;
      Game1.ClsCLR.SetAllColours(0.0f, 0.0f, 0.0f);
      this.overworldbuttons = new OverwoldMainButtons();
      GameStateManager.tutorialmanager.EnteredOverWorld(player);
      OverWorldManager.overworldenvironment = new OverWorldEnvironmentManager(player);
      this.fakemouse = new FakeMouseManager();
      this.hudmanager = new OWHUDManager(player);
      this.avatarUIManager = new AvatarUIManager();
      OverWorldManager.selectedthingmanager = new SelectedThingManager();
      if (flag)
      {
        for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          player.prisonlayout.cellblockcontainer.prisonzones[index].penItems.ReaddPenItemsOnMapBuild(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID, OverWorldManager.overworldenvironment.animalsinpens);
      }
      if (TutorialManager.currenttutorial != TUTORIALTYPE.WelcomeToTheZoo)
        FogManager.StartFogFade(1f, 0.0f);
      if (player.livestats.NewlyDeads.Count > 0)
      {
        OverWorldManager.overworldstate = OverWOrldState.GraveYard;
        this.graveYardManager = new GraveYardManager(player);
      }
      if (Game1.Previousgamestate != GAMESTATE.Settings && Game1.Previousgamestate != GAMESTATE.IAPStore && Game1.Previousgamestate != GAMESTATE.WorldMap)
        MusicManager.BeginFadeOut(0.5f, SongTitle.RandomOverWorldMusic);
      player.prisonlayout.cellblockcontainer.SetConsumption(player.livestats.consumptionstatus, player);
      if (player.livestats.AnimalsJustTraded != null)
      {
        this.cellselectmanger = new CellSelectManager(player);
        OverWorldManager.overworldstate = OverWOrldState.CellSelect;
      }
      this.controlleroverirde = new Controler_OverworldMatrix();
      QuestScrubber.ScrubQuests(player);
      CalculateStat.GetCellUIDWithThisAnimal(player, AnimalType.Rabbit);
      MoralityCalculator.CalculateMorality(player);
      OverWorldManager.overworldenvironment.wallsandfloors.SetUpAnimalsOnOrder(player);
      OverWorldManager.overworldenvironment.wallsandfloors.SetUpFarmSigns(player);
    }

    internal static OverWOrldState overworldstate
    {
      get => OverWorldManager._overworldstate;
      set => OverWorldManager._overworldstate = value;
    }

    public void QuickUpdateOverWorldManager(
      Player[] players,
      float DeltaTime,
      float SimulationTime)
    {
      OverWorldManager.overworldenvironment.UpdateOverWorldEnvironmentManager(SimulationTime, players[0], DeltaTime);
    }

    public void UpdateOverWorldManager(
      Player[] players,
      float DeltaTime,
      SpringIAPManager springIAPmanager)
    {
      if (TrailerDemoFlags.HasTrailerFlag)
        this.trailermanager.UpdateZ_TrailerManager(DeltaTime, this.avatarUIManager, OverWorldManager.crowd, players[0]);
      if (players[0].livestats.AnimalsJustTraded != null && OverWorldManager.overworldstate != OverWOrldState.CellSelect)
      {
        this.cellselectmanger = new CellSelectManager(players[0]);
        OverWorldManager.overworldstate = OverWOrldState.CellSelect;
      }
      this.CheckForMouseOverAnyPanel(players[0]);
      if (Z_GameFlags.ScrubOtherHeroQuest)
      {
        Z_GameFlags.ScrubOtherHeroQuest = false;
        QuestScrubber.ScrubOnCompleteHeroQuest(players[0]);
      }
      bool flag1 = true;
      if (FeatureFlags.NewAnimalGot)
      {
        FeatureFlags.NewAnimalGot = false;
        if (players[0].livestats.AnimalsJustTraded != null)
        {
          this.cellselectmanger = new CellSelectManager(players[0]);
          OverWorldManager.overworldstate = OverWOrldState.CellSelect;
        }
      }
      if ((double) Game1.screenfade.fAlpha != 0.0)
        players[0].inputmap.ClearAllInput(players[0]);
      if (OverWorldManager.Z_infopopupmanager != null)
      {
        flag1 = false;
        if (OverWorldManager.Z_infopopupmanager.UpdateInfoPopUpManager(players[0], DeltaTime))
          OverWorldManager.Z_infopopupmanager = (InfoPopUpManager) null;
        players[0].inputmap.ClearAllInput(players[0]);
      }
      if (this.pausescreen == null && !Flags.PlatformIsIOS && (players[0].inputmap.PressedThisFrame[20] || PC_KeyState.Escape_Released && !MainBarManager.BarIsOnScreen || PauseManager.ForcePause) && (OverWorldManager.zoopopupHolder.IsNull() && OverWorldManager.zoopopupHolder.TopLayerIsNull() && Z_GameFlags.DRAW_heatmaptype == HeatMapType.None))
      {
        PauseManager.ForcePause = false;
        this.pausescreen = new PauseScreenManager(players[0], true, Flags.PlatformIsMobile);
      }
      else
      {
        MainBarManager.BarIsOnScreen = false;
        bool flag2;
        if (this.pausescreen != null)
        {
          flag2 = false;
          if (this.pausescreen.UpdatePauseScreenManager(DeltaTime, players[0]))
            this.pausescreen = (PauseScreenManager) null;
          players[0].inputmap.ClearAllInput(players[0]);
          OverWorldManager.overworldenvironment.UpdateOverWorldEnvironmentManager(0.0f, players[0], 0.0f);
          GameFlags.RefDeltaTime = 0.0f;
        }
        else if (OverWorldManager.overworldstate == OverWOrldState.FireEmployees)
        {
          if (OverWorldManager.firingmanager.UpdateFiringManager(players[0], DeltaTime))
          {
            OverWorldManager.overworldstate = OverWOrldState.MainMenu;
            this.hudmanager.ReturnToMainMenu();
            players[0].inputmap.ClearAllInput(players[0]);
          }
          OverWorldManager.overworldenvironment.UpdateOverWorldEnvironmentManager(0.0f, players[0], 0.0f);
          GameFlags.RefDeltaTime = 0.0f;
        }
        else
        {
          if (Z_GameFlags.ScrubForSaleSigns)
            AddForSaleSigns.AddSigns(players[0]);
          float SimulationTime = DeltaTime;
          if (players[0].livestats.SpeedUpSimulation == 1)
            SimulationTime *= 2f;
          else if (players[0].livestats.SpeedUpSimulation == 2)
            SimulationTime *= 4f;
          else if (players[0].livestats.SpeedUpSimulation == 3)
            SimulationTime = 0.0f;
          if (players[0].livestats.SimulationIsPaused)
            SimulationTime = 0.0f;
          if (OverWorldManager.overworldstate == OverWOrldState.CellSelect)
            SimulationTime = 0.0f;
          LiveStats.UpdateInOverworld(DeltaTime, players[0], OverWorldManager.overworldenvironment.wallsandfloors, this, SimulationTime);
          if (OverWorldManager.zoopopupHolder.UpdateZooPopUps(players[0], DeltaTime))
            OverWorldManager.zoopopupHolder.SetNull();
          else if (OverWorldManager.zoopopupHolder.ShouldCancelDeltaTime())
          {
            flag1 = false;
            DeltaTime = 0.0f;
            SimulationTime = 0.0f;
          }
          if (GameFlags.PhotoMode)
          {
            flag2 = false;
            OverWorldManager.overworldenvironment.UpdateOverWorldEnvironmentManager(DeltaTime, players[0], DeltaTime);
          }
          else
          {
            this.fakemouse.UpdateFakeMouseManager(DeltaTime, players[0], OverWorldManager.overworldenvironment);
            if (flag1)
              OverWorldManager.particlemanager.UpdateParticleManager(DeltaTime);
            GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, players[0]);
            bool GoToCellSelectFromTransfer = false;
            this.ZHUD.UpdateZHudManager(players[0], DeltaTime);
            OverWorldManager.heatmapmanager.UpdateHeatMapManager(players[0], DeltaTime);
            if (this.hudmanager.PreUpdateHUD(DeltaTime, players[0], ref GoToCellSelectFromTransfer, OverWorldManager.overworldenvironment.wallsandfloors))
            {
              OverWorldManager.overworldenvironment.UpdateDrawFlag();
              players[0].Stats.UpdateGameTime(SimulationTime, players[0]);
              this.hudmanager.UpdateHUDManager(DeltaTime, players[0], springIAPmanager);
            }
            else
            {
              if (GoToCellSelectFromTransfer)
              {
                this.cellselectmanger = new CellSelectManager(players[0]);
                OverWorldManager.overworldstate = OverWOrldState.CellSelect;
              }
              if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.MoveGate || Z_GameFlags.ForceToNewScreen == ForceToNewScreen.MovePen)
              {
                this.buildmanager = new OverworldBuildManager(players[0]);
                OverWorldManager.overworldstate = OverWOrldState.Build;
              }
              players[0].Stats.UpdateGameTime(SimulationTime, players[0]);
              this.hudmanager.UpdateHUDManager(DeltaTime, players[0], springIAPmanager);
              OverWorldManager.overworldenvironment.UpdateOverWorldEnvironmentManager(SimulationTime, players[0], DeltaTime);
              OverWorldManager.eventsmanager.UpdateZ_EventsManager(DeltaTime, SimulationTime, players[0]);
              OverWorldManager.zbus.UpdateZ_BusManager(SimulationTime, players[0]);
              OverWorldManager.garbagemanager.UpdateZ_GarbageManager(players[0]);
              OverWorldManager.trashmanager.UpdateZ_TrashManager(DeltaTime, players[0]);
              OverWorldManager.crowd.UpdateCustomerManager(SimulationTime, players[0]);
              this.moneyrenderer.UpdateMoneyRenderer(DeltaTime, players[0]);
              GameStateManager.tutorialmanager.UpdateZTips(DeltaTime, players[0], OverWorldManager.overworldstate);
              this.controlleroverirde.UpdateControler_OverworldUI(OverWorldManager.overworldstate, DeltaTime, this.overworldbuttons, players[0]);
              OverWOrldState OverrideOverworldState = OverWOrldState.Count;
              if (!players[0].livestats.SimulationIsPaused)
                OverWorldManager.z_daynightmanager.UpdateDayNightManager(players[0], DeltaTime, SimulationTime);
              bool flag3 = false;
              if (OverWorldManager.overworldstate == OverWOrldState.Build && this.buildmanager.AllowClickOut(players[0]))
                flag3 = true;
              if (OverWorldManager.overworldstate == OverWOrldState.MainMenu | flag3)
              {
                if (OverWorldManager.overworldstate == OverWOrldState.MainMenu && Z_GameFlags.ForceToNewScreen != ForceToNewScreen.None && OverWorldManager.zoopopupHolder.IsNull())
                {
                  if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.DietManagement && Game1.GetNextGameState() != GAMESTATE.ManagePenSetUp)
                  {
                    Game1.SetNextGameState(GAMESTATE.ManagePenSetUp);
                    Game1.screenfade.BeginFade(true);
                  }
                  if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.TicketPrice)
                  {
                    Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
                    OverWorldManager.zoopopupHolder.CreateZooPopUps(players[0], POPUPSTATE.Ticket);
                  }
                }
                bool StartedExit = false;
                if (this.ForceToStore || this.overworldbuttons.UpdateOverwoldMainButtons(players[0], DeltaTime, out StartedExit))
                {
                  if (this.ForceToStore)
                  {
                    this.overworldbuttons.buttonpressed = OverworldButtons.Store;
                    this.ForceToStore = false;
                  }
                  switch (this.overworldbuttons.buttonpressed)
                  {
                    case OverworldButtons.Settings:
                      if (Game1.GetNextGameState() != GAMESTATE.SettingsSetUp && !FixPixID_ToNewServer.IsCheckingCloud)
                      {
                        if (Z_DebugFlags.IsBetaVersion)
                        {
                          this.overworldbuttons.buttonpressed = OverworldButtons.Count;
                          PauseManager.ForcePause = true;
                          break;
                        }
                        Game1.screenfade.BeginFade(true);
                        Game1.SetNextGameState(GAMESTATE.SettingsSetUp);
                        break;
                      }
                      break;
                    case OverworldButtons.Intake:
                      break;
                    case OverworldButtons.Breeding:
                      SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuOpen);
                      this.breedmanager = new StoreLocalManager(players[0]);
                      OverWorldManager.overworldstate = OverWOrldState.Shop;
                      break;
                    case OverworldButtons.Build:
                      if (!flag3)
                      {
                        Z_DebugFlags.TempNewBuildingMenu = true;
                        this.buildmanager = new OverworldBuildManager(players[0]);
                        OverWorldManager.overworldstate = OverWOrldState.Build;
                        ThingToBuildManager.placetype = PlaceType.PlacingBuildings;
                        break;
                      }
                      break;
                    case OverworldButtons.Manage:
                      this.managemanager = new Z_ManageManager(players[0]);
                      OverWorldManager.overworldstate = OverWOrldState.Manage;
                      SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuOpen);
                      break;
                    case OverworldButtons.Store:
                      if (Game1.GetNextGameState() != GAMESTATE.IAPStoreSetUp)
                      {
                        Game1.screenfade.BeginFade(true);
                        Game1.SetNextGameState(GAMESTATE.IAPStoreSetUp);
                        OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                        break;
                      }
                      break;
                    case OverworldButtons.HeatMapView:
                      this.overworldheatmapviewer = new OverWorldHeatMapManager();
                      OverWorldManager.overworldstate = OverWOrldState.ShowHeatMaps;
                      break;
                    default:
                      this.overworldbuttons = new OverwoldMainButtons();
                      OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                      break;
                  }
                }
                else
                  this.avatarUIManager.UpdareAvatarUIManager(players[0], DeltaTime, OverWorldManager.selectedthingmanager, ref OverrideOverworldState);
                if (OverrideOverworldState == OverWOrldState.MainMenu)
                  OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                if (OverWorldManager.overworldstate == OverWOrldState.MainMenu | flag3)
                {
                  bool ChangedState;
                  OverWorldManager.selectedthingmanager.UpdateSelectedThingManager(players[0], DeltaTime, OverWorldManager.overworldenvironment, out ChangedState);
                  if (flag3 && ChangedState)
                  {
                    this.CancelBuildMenu(players);
                    this.overworldbuttons.buttonpressed = OverworldButtons.Count;
                    OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                  }
                  if (OverWorldManager.overworldstate != OverWOrldState.MainMenu)
                    this.avatarUIManager.SkipDrawOnNextUpdate();
                }
                if (players[0].inputmap.PressedThisFrame[28] && Z_DebugFlags.AllowAvatarDirectControl)
                  OverWorldManager.overworldstate = OverWOrldState.PlayingAsAvatar;
                if (StartedExit)
                {
                  switch (this.overworldbuttons.buttonpressed)
                  {
                    case OverworldButtons.Intake:
                      Game1.SetNextGameState(GAMESTATE.WorldMapSetUp);
                      Game1.screenfade.BeginFade(true);
                      break;
                    case OverworldButtons.Breeding:
                      this.hudmanager.ExitMainMenu();
                      break;
                    case OverworldButtons.Build:
                      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                      Z_GameFlags.SetHeatMapType(HeatMapType.None);
                      break;
                  }
                }
              }
              else if (OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar)
              {
                this.avatarUIManager.UpdareAvatarUIManager(players[0], DeltaTime, OverWorldManager.selectedthingmanager, ref OverrideOverworldState);
                if (OverrideOverworldState == OverWOrldState.MainMenu)
                {
                  OverrideOverworldState = OverWOrldState.Count;
                  Z_GameFlags.IsWaitingToReturnToControllerWalk = true;
                  OverWorldManager.selectedthingmanager.UpdateSelectedThingManager(players[0], DeltaTime, OverWorldManager.overworldenvironment, out bool _);
                  OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                }
              }
              if (OverWorldManager.overworldstate == OverWOrldState.Intake)
              {
                bool _GoToCellBlockSelect;
                if (this.intakemanager.UpdateIntakeManager(DeltaTime, players[0], out _GoToCellBlockSelect) && !_GoToCellBlockSelect)
                {
                  this.overworldbuttons = new OverwoldMainButtons();
                  OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                  this.hudmanager.ReturnToMainMenu();
                }
                if (_GoToCellBlockSelect)
                {
                  this.cellselectmanger = new CellSelectManager(players[0]);
                  OverWorldManager.overworldstate = OverWOrldState.CellSelect;
                }
              }
              if (OverWorldManager.overworldstate == OverWOrldState.ShowHeatMaps && this.overworldheatmapviewer.UpdateOverWorldHeatMapManager(players[0]))
              {
                this.overworldbuttons = new OverwoldMainButtons();
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                Z_GameFlags.SetHeatMapType(HeatMapType.None);
              }
              if (OverWorldManager.overworldstate == OverWOrldState.BuyMoreLand && OverWorldManager.buymoreland.UpdateZ_BuyLand(players[0], DeltaTime, OverWorldManager.overworldenvironment))
              {
                OverWorldManager.buymoreland = (Z_BuyLand) null;
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.avatarUIManager.UpdareAvatarUIManager(players[0], 0.0f, OverWorldManager.selectedthingmanager, ref OverrideOverworldState);
                if (OverWorldManager.overworldstate != OverWOrldState.BuyMoreLand)
                  OverWorldManager.selectedthingmanager.UpdateSelectedThingManager(players[0], DeltaTime, OverWorldManager.overworldenvironment, out bool _);
              }
              if (OverWorldManager.overworldstate == OverWOrldState.CustomizePen)
              {
                if (OverWorldManager.customizepenmanager == null)
                  OverWorldManager.customizepenmanager = new Pen_SelectedPenManager(players[0]);
                if (OverWorldManager.customizepenmanager.UpdatePen_SelectedPenManager(DeltaTime, players[0], OverWorldManager.overworldenvironment, OverWorldManager.overworldenvironment.wallsandfloors, this.buildmanager))
                  OverWorldManager.customizepenmanager = (Pen_SelectedPenManager) null;
              }
              if (OverWorldManager.overworldstate == OverWOrldState.GraveYard && this.graveYardManager.UpdateGraveYardManager(DeltaTime, players[0], OverWorldManager.overworldenvironment.wallsandfloors))
              {
                FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = false;
                FeatureFlags.BlockPlayerMoveCamera = false;
                this.overworldbuttons = new OverwoldMainButtons();
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.hudmanager.ReturnToMainMenu();
              }
              if (OverWorldManager.overworldstate == OverWOrldState.Shop && this.breedmanager.UpdateStoreLocalManager(DeltaTime, players[0]))
              {
                this.overworldbuttons = new OverwoldMainButtons();
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.hudmanager.ReturnToMainMenu();
              }
              if (OverWorldManager.overworldstate == OverWOrldState.MoveBuilding && OverWorldManager.movebuilding.UpdateZ_MoveBuildingManager(DeltaTime, players[0], OverWorldManager.overworldenvironment.wallsandfloors))
              {
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.hudmanager.ReturnToMainMenu();
              }
              if (OverWorldManager.overworldstate == OverWOrldState.Manage && this.managemanager.UpdateZ_ManageManager(DeltaTime, players[0]))
              {
                this.overworldbuttons = new OverwoldMainButtons();
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                OverWorldManager.z_daynightmanager.ReLerpButton();
                this.hudmanager.ReturnToMainMenu();
              }
              if (OverWorldManager.overworldstate == OverWOrldState.Build && (this.buildmanager.UpdateOverworldBuildManager(DeltaTime, players[0], OverWorldManager.overworldenvironment.wallsandfloors, OverWorldManager.overworldenvironment.animalsinpens, OverWorldManager.overworldenvironment) || GameFlags.ForceExitBuildNow))
                this.CancelBuildMenu(players);
              if (OverWorldManager.overworldstate == OverWOrldState.CellSelect)
              {
                bool ExitBackToGame = false;
                this.cellselectmanger.UpdateCellSelectManager(players[0], DeltaTime, ref ExitBackToGame);
                if (ExitBackToGame)
                {
                  FeatureFlags.BlockTimer = false;
                  FeatureFlags.BlockCash = false;
                  this.overworldbuttons = new OverwoldMainButtons();
                  OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                  this.hudmanager.ReturnToMainMenu();
                  players[0].livestats.LevelIsTransferFromHoldingCell = false;
                  GameStateManager.tutorialmanager.TrasferdAnimalsToEnclosuer(players[0]);
                }
              }
              if (OverWorldManager.overworldstate == OverWOrldState.EditZone)
                OverWorldManager.zoneEditManager.UpdateZoneEditManager(players[0], DeltaTime);
              if (OverWorldManager.overworldstate == OverWOrldState.QuickPickEmployee && OverWorldManager.quickpickemployeemanager.UpdateQuickPickEmployeeManager(players[0], DeltaTime))
              {
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.hudmanager.ReturnToMainMenu();
              }
              if (OverWorldManager.overworldstate == OverWOrldState.TicketPricing && OverWorldManager.ticketPriceManager.UpdateTicketPriceManager_Holder(players[0], DeltaTime, true))
              {
                OverWorldManager.overworldstate = OverWOrldState.MainMenu;
                this.hudmanager.ReturnToMainMenu();
              }
              this.speechmanager.UpdateSpeechManager(DeltaTime, OverWorldManager.overworldenvironment);
            }
          }
        }
      }
    }

    private void CancelBuildMenu(Player[] players)
    {
      if (Z_GameFlags.DRAW_heatmaptype == HeatMapType.Water)
        Z_GameFlags.DRAW_heatmaptype = HeatMapType.None;
      Z_NotificationManager.MadeABuilding = true;
      GameFlags.ForceExitBuildNow = false;
      this.overworldbuttons = new OverwoldMainButtons();
      Pen_SelectedPenManager.Reconstruct = true;
      OverWorldManager.overworldstate = OverWOrldState.MainMenu;
      FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = false;
      FeatureFlags.BlockPlayerMoveCamera = false;
      players[0].livestats.SimulationIsPaused = false;
    }

    public void CheckForMouseOverAnyPanel(Player player)
    {
      Z_GameFlags.MouseIsOverAPanel = false;
      Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = false;
      if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
      {
        Z_GameFlags.MouseIsOverAPanel |= this.overworldbuttons.CheckMouseOver(player);
        Z_GameFlags.MouseIsOverAPanel |= OverWorldManager.selectedthingmanager.CheckMouseOver(player);
      }
      if (OverWorldManager.overworldstate == OverWOrldState.Build)
        Z_GameFlags.MouseIsOverAPanel |= this.buildmanager.CheckMouseOver(player);
      if (OverWorldManager.overworldstate == OverWOrldState.QuickPickEmployee)
        Z_GameFlags.MouseIsOverAPanel |= OverWorldManager.quickpickemployeemanager.CheckMouseOver(player, Vector2.Zero);
      Z_GameFlags.MouseIsOverAPanel |= this.ZHUD.CheckMouseOver(player);
      if (!OverWorldManager.zoopopupHolder.IsNull())
        Z_GameFlags.MouseIsOverAPanel |= OverWorldManager.zoopopupHolder.CheckMouseOver(player);
      if (OverWorldManager.overworldstate != OverWOrldState.CellSelect || this.cellselectmanger == null)
        return;
      Z_GameFlags.MouseIsOverAPanel |= this.cellselectmanger.CheckMouseOver(player);
    }

    public bool IsCurrentlyMovingThisPen(int CELLUID) => OverWorldManager.overworldstate == OverWOrldState.Build && OverWorldManager.customizepenmanager != null && OverWorldManager.customizepenmanager.IsCurrentlyMovingThisPen(CELLUID);

    public bool IsMouseOverButton(Player player) => OverWorldManager.overworldstate == OverWOrldState.CustomizePen && OverWorldManager.customizepenmanager.IsMouseOverButton(player);

    public void DrawOverWorldManager(Player player)
    {
      OverWorldManager.overworldenvironment.DrawOverWorldEnvironmentManager();
      OverWorldManager.crowd.DrawCustomerManager(OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray.GetLength(1) - 4);
      OverWorldManager.garbagemanager.DrawZ_GarbageManager();
      if (!GameFlags.PhotoMode)
        OverWorldManager.heatmapmanager.DrawHeatMapManager();
      OverWorldManager.eventsmanager.DrawZ_EventsManager();
      if (!GameFlags.PhotoMode)
        this.ZHUD.DrawBussHUD();
      OverWorldManager.zbus.DrawZ_BusManager();
      OverWorldManager.crowd.DrawCustomerManagerAfterBus();
      OverWorldManager.overworldenvironment.DrawOverWorldEnvironmentManagerAfterBus();
      if (GameFlags.PhotoMode)
        return;
      this.moneyrenderer.DrawMoneyRenderer();
      switch (OverWorldManager.overworldstate)
      {
        case OverWOrldState.MainMenu:
          this.avatarUIManager.DrawAvatarUIManager();
          OverWorldManager.selectedthingmanager.DrawSelectedThingManager(player);
          if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
          {
            OverWorldManager.z_daynightmanager.DrawDayNightManager();
            break;
          }
          break;
        case OverWOrldState.Intake:
          this.intakemanager.DrawIntakeManager();
          break;
        case OverWOrldState.Shop:
          this.breedmanager.DrawStoreLocalManager();
          break;
        case OverWOrldState.Build:
          this.buildmanager.DrawOverworldBuildManager(OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray, player);
          break;
        case OverWOrldState.CellSelect:
          this.cellselectmanger.DrawCellSelectManager();
          break;
        case OverWOrldState.GraveYard:
          this.graveYardManager.DrawGraveYardManager(OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray);
          break;
        case OverWOrldState.Manage:
          this.managemanager.DrawZ_ManageManager();
          break;
        case OverWOrldState.MoveBuilding:
          OverWorldManager.movebuilding.DrawZ_MoveBuildingManager(OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray);
          break;
        case OverWOrldState.PlayingAsAvatar:
          Z_GameFlags.avatarcontroller.DrawAvatarController();
          break;
        case OverWOrldState.CustomizePen:
          if (OverWorldManager.customizepenmanager != null)
          {
            OverWorldManager.customizepenmanager.DrawPen_SelectedPenManager(player);
            break;
          }
          break;
        case OverWOrldState.BuyMoreLand:
          OverWorldManager.buymoreland.DrawZ_BuyLand(player);
          break;
        case OverWOrldState.QuickPickEmployee:
          OverWorldManager.quickpickemployeemanager.DrawQuickPickEmployeeManager();
          break;
        case OverWOrldState.FireEmployees:
          OverWorldManager.firingmanager.DrawFiringManager();
          break;
        case OverWOrldState.TicketPricing:
          OverWorldManager.ticketPriceManager.DrawTicketPriceManager_Holder();
          break;
        case OverWOrldState.EditZone:
          OverWorldManager.zoneEditManager.DrawZoneEditManager();
          break;
      }
      if (OverWorldManager.overworldstate != OverWOrldState.MainMenu)
        this.avatarUIManager.DrawAvatarUIManager();
      if (TrailerDemoFlags.HideUI)
        return;
      this.ZHUD.DrawZHudManager(this.overworldbuttons);
      this.hudmanager.DrawHUDManager();
      OverWorldManager.zoopopupHolder.DrawZooPopUps();
      if (OverWorldManager.fulladvertmanager != null)
        OverWorldManager.fulladvertmanager.DrawFullScreenAdvertManager();
      this.speechmanager.DrawSpeechManager();
      if (this.pausescreen == null)
        return;
      TutorialManager.SkipDraw = true;
      this.pausescreen.DrawPauseScreenManager();
    }
  }
}
