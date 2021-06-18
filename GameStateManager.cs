// Decompiled with JetBrains decompiler
// Type: TinyZoo.GameStateManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Debug;
using Spring.Comms;
using SpringIAP;
using SpringSocial;
using System.Collections.Generic;
using TinyZoo.ArcadeCredits;
using TinyZoo.ArcadeMode;
using TinyZoo.BonusAdvert;
using TinyZoo.BountyResults;
using TinyZoo.CollectionScreen;
using TinyZoo.DebugViewer;
using TinyZoo.Event;
using TinyZoo.ExtraCash;
using TinyZoo.GamePlay;
using TinyZoo.IAPScreen;
using TinyZoo.Intro;
using TinyZoo.ModeSelect;
using TinyZoo.OverWorld.NewDiscoveryScreen;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.Settings;
using TinyZoo.PathFinding;
using TinyZoo.ProfitLadder;
using TinyZoo.RankReward;
using TinyZoo.SplashScreen;
using TinyZoo.TitleScreen;
using TinyZoo.Tutorials;
using TinyZoo.Utils;
using TinyZoo.Utils.FileDownloader;
using TinyZoo.Z_BetaEnd;
using TinyZoo.Z_BreedResult;
using TinyZoo.Z_HUD.VirtualMouse;
using TinyZoo.Z_Manage.Research;
using TinyZoo.Z_ManagePen;
using TinyZoo.Z_ManageShop;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_NewGameSettings;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_PhotoMode;
using TinyZoo.Z_PickSex;
using TinyZoo.Z_Processing;
using TinyZoo.Z_Shelter;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_TicketPrice;
using TinyZoo.Z_Trailer;
using TinyZoo.Z_WeekOver;
using TinyZoo.Z_WorldMap;

namespace TinyZoo
{
  internal class GameStateManager
  {
    private WeekSummaryManager weeksummary;
    private CharacterSelectManager characterselectmamager;
    private WorldMapManager worldmapmanager;
    private DebugAnimalViewer debuganimalviewer;
    private NewDiscovery newdiscovery;
    private BountyResultsManager bountyresultsmanager;
    private SplashManager splashmanager;
    private BreedResultManager breedresults;
    private PRISONPLANET_GamePlayManager gameplaymanager;
    private ModerSelectManager Modeselectmanager;
    private TitleScreenManager titlescreenmanager;
    private IntroManager intromanager;
    private SettingsManager settingsmanager;
    public OverWorldManager overworldmanager;
    private TicketPriceManager_Holder ticketpricemanager;
    internal static TutorialManager tutorialmanager;
    private IAPScreenManager iapscreen;
    private IAPStore iapstore;
    private BetaBanner betabanner;
    private ArcadeModeManager arcademodemanager;
    private IPhoneX_Frame iphonexframe;
    private ArcadeCreditsManager arcadecreditsmanager;
    private CollectionScreenManager collectionsreen;
    private ProfitLadderManager profitladdermanger;
    private RankRewardManager rankreward;
    private EventScreenManager eventscreenmanager;
    private ExtraCashManager extracashmanager;
    private Z_ProcessingManager ProcessingManager;
    private PenManager penmanager;
    private ShopManager shopmanager;
    private NewGameSettingsManager newgamesettingsmamager;
    private Z_StoreRoomViewManager z_storeroommanager;
    private BuildingResearch buildingresearch;
    private Z_ShelterManager z_sheltermanager;
    private VirtualMouseManager virtualmousemanager = new VirtualMouseManager();
    private FPSCounter fpscounter = new FPSCounter();
    private MoralitySummmaryManager moralitysummarymanager;
    private BetaEndingManager betaresultsmanager;
    private PhotoModeManager photomodemanager;
    internal static FileGetManager filegetmanager;

    public void UpdateGameStateManager(
      float DeltaTime,
      ref Player[] players,
      ContentManager Content,
      SpringIAPManager springIAPmanager,
      GraphicsDeviceManager graphics,
      GraphicsDevice _GraphicsDevice)
    {
      if (Game1.saveiconmanager != null && Game1.saveiconmanager.IsSaveEventBlocking())
        return;
      this.virtualmousemanager.UpdateMousePointerManager(players[0], DeltaTime);
      int num1 = (int) Game1.screenfade.UpdateScreenFade(DeltaTime);
      if (Game1.screenfade.FadeToBlackComplete || Game1.ForceSwitchToNextGameState)
      {
        Game1.ForceSwitchToNextGameState = false;
        Game1.Previousgamestate = Game1.gamestate;
        if (Game1.Previousgamestate == GAMESTATE.NewBreed)
          BreedResultManager.newthingget = (newThingRenderer) null;
        Game1.gamestate = Game1.GetNextGameState();
        if ((Game1.gamestate == GAMESTATE.OverWorld || Game1.gamestate == GAMESTATE.OverWorldSetUp) && (Game1.Previousgamestate == GAMESTATE.WorldMap || Game1.Previousgamestate == GAMESTATE.Settings || (Game1.Previousgamestate == GAMESTATE.Shelter || Game1.Previousgamestate == GAMESTATE.ArchitectResearch)))
        {
          OverWorldCamera.ResetCameraOnReturnFromMap();
          TileMath.ResetMapSize();
        }
        if (Game1.gamestate == GAMESTATE.OverWorldSetUp)
          Z_GameFlags.TempBlockVirtualMouse = false;
        Game1.screenfade.BeginFade(false);
      }
      if (Game1.gamestate == GAMESTATE.SplashScreenSetUp || Game1.gamestate == GAMESTATE.SplashScreen)
      {
        if (Game1.gamestate == GAMESTATE.SplashScreenSetUp)
        {
          this.splashmanager = new SplashManager();
          GameStateManager.tutorialmanager = new TutorialManager();
          if (GameStateManager.filegetmanager == null)
            GameStateManager.filegetmanager = new FileGetManager();
        }
        this.splashmanager.UpdateSplashManager(players, Content, DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.ArchitectResearchSetUp || Game1.gamestate == GAMESTATE.ArchitectResearch)
      {
        if (Game1.gamestate == GAMESTATE.ArchitectResearchSetUp)
        {
          Game1.gamestate = GAMESTATE.ArchitectResearch;
          this.buildingresearch = new BuildingResearch(players[0], true);
        }
        this.buildingresearch.UpdateBuildingResearch(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.GameSettingsSetUp || Game1.gamestate == GAMESTATE.GameSettings)
      {
        if (Game1.gamestate == GAMESTATE.GameSettingsSetUp)
        {
          Game1.gamestate = GAMESTATE.GameSettings;
          this.newgamesettingsmamager = new NewGameSettingsManager();
        }
        this.newgamesettingsmamager.UpdateNewGameSettingsManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.IntroScene || Game1.gamestate == GAMESTATE.IntroSceneSetUp)
      {
        if (Game1.gamestate == GAMESTATE.IntroSceneSetUp)
        {
          this.intromanager = new IntroManager();
          Game1.gamestate = GAMESTATE.IntroScene;
        }
        this.intromanager.UpdateIntroManager(DeltaTime, players);
      }
      if (Game1.gamestate == GAMESTATE.Shelter || Game1.gamestate == GAMESTATE.ShelterSetUp)
      {
        if (Game1.gamestate == GAMESTATE.ShelterSetUp)
        {
          this.z_sheltermanager = new Z_ShelterManager(players[0]);
          Game1.gamestate = GAMESTATE.Shelter;
        }
        this.z_sheltermanager.UpdateZ_ShelterManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.TitleScreenSetUp || Game1.gamestate == GAMESTATE.TitleScreen)
      {
        if (Game1.gamestate == GAMESTATE.TitleScreenSetUp)
        {
          GameFlags.HasPassedSplash = true;
          this.titlescreenmanager = new TitleScreenManager();
          Game1.gamestate = GAMESTATE.TitleScreen;
          if (TrailerDemoFlags.HasTrailerFlag)
            SpawnBlockComponent.player = players[0];
        }
        this.titlescreenmanager.UpdateTitleScreenManager(ref players, DeltaTime, Content);
      }
      if (Game1.gamestate == GAMESTATE.SettingsSetUp || Game1.gamestate == GAMESTATE.Settings)
      {
        if (Game1.gamestate == GAMESTATE.SettingsSetUp)
        {
          this.settingsmanager = new SettingsManager(players[0]);
          Game1.gamestate = GAMESTATE.Settings;
        }
        this.settingsmanager.UpdateSettingsManager(DeltaTime, players[0], graphics, _GraphicsDevice);
      }
      if (Game1.gamestate == GAMESTATE.NewBreed || Game1.gamestate == GAMESTATE.NewBreedSetUp)
      {
        if (Game1.gamestate == GAMESTATE.NewBreedSetUp)
        {
          Game1.gamestate = GAMESTATE.NewBreed;
          this.breedresults = new BreedResultManager();
        }
        this.breedresults.UpdateBreedResultManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.ManagePen || Game1.gamestate == GAMESTATE.ManagePenSetUp)
      {
        if (Game1.gamestate == GAMESTATE.ManagePenSetUp)
        {
          Game1.gamestate = GAMESTATE.ManagePen;
          this.penmanager = new PenManager(players[0]);
        }
        this.penmanager.UpdatePenManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.ManageShop || Game1.gamestate == GAMESTATE.ManageShopSetUp)
      {
        if (Game1.gamestate == GAMESTATE.ManageShopSetUp)
        {
          Game1.gamestate = GAMESTATE.ManageShop;
          this.shopmanager = new ShopManager(players[0]);
        }
        this.shopmanager.UpdateShopManager(DeltaTime, players[0]);
        players[0].player.touchinput.DragVectorThisFrame = Vector2.Zero;
        players[0].inputmap.momentumwheel.MovementThisFrame = 0.0f;
        this.overworldmanager.QuickUpdateOverWorldManager(players, 0.0f, 0.0f);
      }
      if (Game1.gamestate == GAMESTATE.GamePlay || Game1.gamestate == GAMESTATE.GamePlaySetUp)
      {
        if (Game1.gamestate == GAMESTATE.GamePlaySetUp)
        {
          this.gameplaymanager = new PRISONPLANET_GamePlayManager(players[0]);
          Game1.gamestate = GAMESTATE.GamePlay;
        }
        this.gameplaymanager.UpdateGamePlayManager(players, DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.OverWorldSetUp)
      {
        WardenRank currentRank = ProfitLadderData.GetCurrentRank(players[0], out float _, out float _, out int _);
        if (currentRank > players[0].Stats.bestrank)
        {
          players[0].Stats.bestrank = currentRank;
          Game1.gamestate = GAMESTATE.RankRewardSetup;
        }
      }
      if (Game1.gamestate == GAMESTATE.OverWorld || Game1.gamestate == GAMESTATE.OverWorldSetUp)
      {
        if (Game1.gamestate == GAMESTATE.OverWorldSetUp)
        {
          if (TrailerDemoFlags.HasTrailerFlag)
            SpawnBlockComponent.player = players[0];
          Z_GameFlags.BlockPointer = false;
          GameFlags.HasPassedSplash = true;
          this.overworldmanager = new OverWorldManager(players[0]);
          Game1.gamestate = GAMESTATE.OverWorld;
        }
        if (this.overworldmanager == null)
          AnnalyticsEvents.GotEverythingNULL(players[0]);
        this.overworldmanager.UpdateOverWorldManager(players, DeltaTime, springIAPmanager);
      }
      UnifiedWaterAnimator.UpdateUnifiedWaterAnimator(DeltaTime);
      if (Game1.gamestate == GAMESTATE.PhotoMode || Game1.gamestate == GAMESTATE.PhotoModeSetUp)
      {
        if (Game1.gamestate == GAMESTATE.PhotoModeSetUp)
        {
          this.photomodemanager = new PhotoModeManager();
          Game1.gamestate = GAMESTATE.PhotoMode;
        }
        this.photomodemanager.UpdatePhotoModeManager(players[0], DeltaTime, this.overworldmanager, players);
      }
      if (Game1.gamestate == GAMESTATE.RankReward || Game1.gamestate == GAMESTATE.RankRewardSetup)
      {
        if (Game1.gamestate == GAMESTATE.RankRewardSetup)
        {
          this.rankreward = new RankRewardManager(players[0]);
          Game1.gamestate = GAMESTATE.RankReward;
        }
        this.rankreward.UpdateRankRewardManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.WeekSummary || Game1.gamestate == GAMESTATE.WeekSummarySetUp)
      {
        if (Game1.gamestate == GAMESTATE.WeekSummarySetUp)
        {
          this.weeksummary = new WeekSummaryManager(players[0]);
          Game1.gamestate = GAMESTATE.WeekSummary;
        }
        this.weeksummary.UpdateWeekSummaryManager(players[0], DeltaTime, this.overworldmanager, players);
      }
      if (Game1.gamestate == GAMESTATE.EventView || Game1.gamestate == GAMESTATE.EventViewSetUp)
      {
        if (Game1.gamestate == GAMESTATE.EventViewSetUp)
        {
          Game1.gamestate = GAMESTATE.EventView;
          this.eventscreenmanager = new EventScreenManager(players[0]);
        }
        if (Game1.gamestate == GAMESTATE.EventView)
          this.eventscreenmanager.UpdateEventScreenManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.ManageStoreRoomSetUp || Game1.gamestate == GAMESTATE.ManageStoreRoom)
      {
        if (Game1.gamestate == GAMESTATE.ManageStoreRoomSetUp)
        {
          this.z_storeroommanager = new Z_StoreRoomViewManager(players[0]);
          Game1.gamestate = GAMESTATE.ManageStoreRoom;
        }
        this.z_storeroommanager.UpdateZ_StoreRoomViewManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.ModeSelectSetUp || Game1.gamestate == GAMESTATE.ModeSelect)
      {
        if (Game1.gamestate == GAMESTATE.ModeSelectSetUp)
        {
          this.Modeselectmanager = new ModerSelectManager();
          Game1.gamestate = GAMESTATE.ModeSelect;
        }
        this.Modeselectmanager.UpdateCharacterSelectManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.BetaResults || Game1.gamestate == GAMESTATE.BetaResultsSetUp)
      {
        if (Game1.gamestate == GAMESTATE.BetaResultsSetUp)
        {
          this.betaresultsmanager = new BetaEndingManager(players[0]);
          Game1.gamestate = GAMESTATE.BetaResults;
        }
        this.betaresultsmanager.UpdateBetaEndingManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.Reward || Game1.gamestate == GAMESTATE.RewardSetUp)
      {
        if (Game1.gamestate == GAMESTATE.RewardSetUp)
        {
          this.newdiscovery = new NewDiscovery(players[0]);
          Game1.gamestate = GAMESTATE.Reward;
        }
        this.newdiscovery.UpdateNewDiscovery(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.ExtraCash || Game1.gamestate == GAMESTATE.ExtraCashSetUp)
      {
        if (Game1.gamestate == GAMESTATE.ExtraCashSetUp)
        {
          this.extracashmanager = new ExtraCashManager(players[0]);
          Game1.gamestate = GAMESTATE.ExtraCash;
        }
        this.extracashmanager.UpdateExtraCashManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.IAPSCreenSetUp || Game1.gamestate == GAMESTATE.IAPScreen)
      {
        if (Game1.gamestate == GAMESTATE.IAPSCreenSetUp)
        {
          this.iapscreen = new IAPScreenManager(players[0]);
          Game1.gamestate = GAMESTATE.IAPScreen;
        }
        this.iapscreen.UpdateIAPScreenManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.IAPStore || Game1.gamestate == GAMESTATE.IAPStoreSetUp)
      {
        if (Game1.gamestate == GAMESTATE.IAPStoreSetUp)
        {
          this.iapstore = new IAPStore(players[0]);
          Game1.gamestate = GAMESTATE.IAPStore;
        }
        this.iapstore.UpdateIAPStore(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.ArcadeModeSetUp || Game1.gamestate == GAMESTATE.ArcadeMode)
      {
        if (Game1.gamestate == GAMESTATE.ArcadeModeSetUp)
        {
          Game1.gamestate = GAMESTATE.ArcadeMode;
          this.arcademodemanager = new ArcadeModeManager(players[0]);
        }
        this.arcademodemanager.UpdteArcadeModeManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.ArcadeCreditsSetUp || Game1.gamestate == GAMESTATE.ArcadeCredits)
      {
        if (Game1.gamestate == GAMESTATE.ArcadeCreditsSetUp)
        {
          Game1.gamestate = GAMESTATE.ArcadeCredits;
          this.arcadecreditsmanager = new ArcadeCreditsManager();
        }
        this.arcadecreditsmanager.UpdateArcadeCreditsManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.ProfitLadder || Game1.gamestate == GAMESTATE.ProfitLadderSetUp)
      {
        if (Game1.gamestate == GAMESTATE.ProfitLadderSetUp)
        {
          Game1.gamestate = GAMESTATE.ProfitLadder;
          this.profitladdermanger = new ProfitLadderManager(players[0]);
        }
        this.profitladdermanger.UpdateProfitLadderManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.BountyResults || Game1.gamestate == GAMESTATE.BountyResultsSetUp)
      {
        if (Game1.gamestate == GAMESTATE.BountyResultsSetUp)
        {
          Game1.gamestate = GAMESTATE.BountyResults;
          this.bountyresultsmanager = new BountyResultsManager(players[0]);
        }
        this.bountyresultsmanager.UpdateBountyResultsManager(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.CollectionView || Game1.gamestate == GAMESTATE.CollectionViewSetUp)
      {
        if (Game1.gamestate == GAMESTATE.CollectionViewSetUp)
        {
          Game1.gamestate = GAMESTATE.CollectionView;
          this.collectionsreen = new CollectionScreenManager(players[0], true);
        }
        bool ExitDone;
        int num2 = (int) this.collectionsreen.UpdateCollectionScreenManager(Vector2.Zero, DeltaTime, players[0], out ExitDone, out bool _);
        if (ExitDone && Game1.GetNextGameState() != GAMESTATE.OverWorld)
        {
          Game1.screenfade.BeginFade(true);
          Game1.SetNextGameState(GAMESTATE.OverWorld);
        }
      }
      if (Game1.gamestate == GAMESTATE.CharacterSelectSetUp || Game1.gamestate == GAMESTATE.CharacterSelect)
      {
        if (Game1.gamestate == GAMESTATE.CharacterSelectSetUp)
        {
          Game1.gamestate = GAMESTATE.CharacterSelect;
          this.characterselectmamager = new CharacterSelectManager();
        }
        this.characterselectmamager.UpdateCharacterSelectManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.WorldMap || Game1.gamestate == GAMESTATE.WorldMapSetUp)
      {
        if (Game1.gamestate == GAMESTATE.WorldMapSetUp)
        {
          Game1.gamestate = GAMESTATE.WorldMap;
          this.worldmapmanager = new WorldMapManager(players[0]);
        }
        this.worldmapmanager.UpdateWorldMapManager(players[0], DeltaTime);
      }
      if (Game1.gamestate == GAMESTATE.DebugAnimalViewer || Game1.gamestate == GAMESTATE.DebugAnimalViewerSetUp)
      {
        if (Game1.gamestate == GAMESTATE.DebugAnimalViewerSetUp)
        {
          Game1.gamestate = GAMESTATE.DebugAnimalViewer;
          this.debuganimalviewer = new DebugAnimalViewer(players[0]);
        }
        this.debuganimalviewer.UpdateDebugAnimalViewer(DeltaTime, players[0]);
      }
      if (Game1.gamestate == GAMESTATE.MoralitySummary || Game1.gamestate == GAMESTATE.MoralitySummarySetUp)
      {
        if (Game1.gamestate == GAMESTATE.MoralitySummarySetUp)
        {
          Game1.gamestate = GAMESTATE.MoralitySummary;
          this.moralitysummarymanager = new MoralitySummmaryManager(players[0]);
        }
        this.moralitysummarymanager.UpdateMoralitySummary(players[0], DeltaTime);
      }
      if (!Z_DebugFlags.DisplayFPS)
        return;
      this.fpscounter.UpdateFPS(DeltaTime);
    }

    public void DrawGameStateManager(Player player, float DeltaTime)
    {
      switch (Game1.gamestate)
      {
        case GAMESTATE.SplashScreen:
          this.splashmanager.DrawSplashManager();
          break;
        case GAMESTATE.GameSettings:
          this.newgamesettingsmamager.DrawNewGameSettingsManager();
          break;
        case GAMESTATE.IntroScene:
          this.intromanager.DrawIntroManager();
          break;
        case GAMESTATE.TitleScreen:
          this.titlescreenmanager.DrawTitleScreenManager();
          break;
        case GAMESTATE.ModeSelect:
          this.Modeselectmanager.DrawCharacterSelectManager();
          break;
        case GAMESTATE.Settings:
          this.settingsmanager.DrawSettingsManager();
          break;
        case GAMESTATE.GamePlay:
          this.gameplaymanager.DrawGamePlayManager();
          break;
        case GAMESTATE.OverWorld:
          this.overworldmanager.DrawOverWorldManager(player);
          break;
        case GAMESTATE.Reward:
          this.newdiscovery.DrawNewDiscovery();
          break;
        case GAMESTATE.IAPScreen:
          this.iapscreen.DrawIAPScreenManager();
          break;
        case GAMESTATE.IAPStore:
          this.iapstore.DrawIAPStore();
          break;
        case GAMESTATE.ArcadeMode:
          this.arcademodemanager.DrawArcadeModeManager();
          break;
        case GAMESTATE.CollectionView:
          this.collectionsreen.DrawCollectionScreenManager(Vector2.Zero);
          break;
        case GAMESTATE.ArcadeCredits:
          this.arcadecreditsmanager.DrawArcadeCreditsManager();
          break;
        case GAMESTATE.ProfitLadder:
          this.profitladdermanger.DrawProfitLadderManager();
          break;
        case GAMESTATE.RankReward:
          this.rankreward.DrawRankRewardManager();
          break;
        case GAMESTATE.EventView:
          this.eventscreenmanager.DrawEventScreenManager();
          break;
        case GAMESTATE.ExtraCash:
          this.extracashmanager.DrawExtraCashManager();
          break;
        case GAMESTATE.BountyResults:
          this.bountyresultsmanager.DrawBountyResultsManager();
          break;
        case GAMESTATE.CharacterSelect:
          this.characterselectmamager.DrawCharacterSelectManager();
          break;
        case GAMESTATE.WorldMap:
          this.worldmapmanager.DrawWorldMapManager();
          break;
        case GAMESTATE.DebugAnimalViewer:
          this.debuganimalviewer.DrawDebugAnimalViewer();
          break;
        case GAMESTATE.NewBreed:
          this.breedresults.DrawBreedResultManager();
          break;
        case GAMESTATE.ManagePen:
          this.penmanager.DrawPenManager();
          break;
        case GAMESTATE.ManageShop:
          this.shopmanager.DrawShopManager(this.overworldmanager, player);
          break;
        case GAMESTATE.TicketPrice:
          this.ticketpricemanager.DrawTicketPriceManager_Holder();
          break;
        case GAMESTATE.WeekSummary:
          this.weeksummary.DrawWeekSummaryManager(this.overworldmanager, player);
          break;
        case GAMESTATE.ManageStoreRoom:
          this.z_storeroommanager.DrawZ_StoreRoomViewManager();
          break;
        case GAMESTATE.ArchitectResearch:
          this.buildingresearch.DrawBuildingResearch();
          break;
        case GAMESTATE.Shelter:
          this.z_sheltermanager.DrawZ_ShelterManager();
          break;
        case GAMESTATE.BetaResults:
          this.betaresultsmanager.DrawBetaEndingManager();
          break;
        case GAMESTATE.MoralitySummary:
          this.moralitysummarymanager.DrawMoralitySummary();
          break;
        case GAMESTATE.PhotoMode:
          this.photomodemanager.DrawPhotoModeManager(this.overworldmanager, player);
          break;
        default:
          TextFunctions.DrawJustifiedText("HELLO", 5f, new Vector2(512f, 300f), Color.Black, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch01);
          break;
      }
      GameStateManager.tutorialmanager.DrawTutorialManager();
      Game1.screenfade.DrawFade(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      this.virtualmousemanager.DrawMousePointerManager(player);
      if (DebugFlags.IAPTest)
      {
        int count = 14;
        List<string> log = new List<string>();
        int num = SpringIAPManager.Instance.CheckLog(ref log, false);
        if (num > count)
          log = log.GetRange(num - count, count);
        for (int index = 0; index < log.Count; ++index)
        {
          TextFunctions.DrawJustifiedText(log[index], 3f, new Vector2(514f, (float) (52 + index * 50)), Color.Black, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
          TextFunctions.DrawJustifiedText(log[index], 3f, new Vector2(512f, (float) (50 + index * 50)), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
        }
      }
      if (!GameFlags.HasDoneForceDraw && (double) Game1.screenfade.fAlpha == 1.0)
      {
        GameObject gameObject = new GameObject();
        gameObject.DrawRect = new Rectangle(0, 0, 1024, 1024);
        gameObject.Draw(AssetContainer.pointspritebatch0, AssetContainer.EnvironmentSheet);
        gameObject.Draw(AssetContainer.pointspritebatch0, AssetContainer.EnvironmentSheet2);
        gameObject.Draw(AssetContainer.pointspritebatch0, AssetContainer.AnimalSheet);
        gameObject.Draw(AssetContainer.pointspritebatch0, AssetContainer.Fog);
        gameObject.Draw(AssetContainer.pointspritebatch0, AssetContainer.UISheet);
      }
      if (Z_DebugFlags.DrawCollision)
      {
        CollisionRenderer.RenderCollisionRenderer(GameFlags.pathset, 15f);
        if (Z_GameFlags.pathfinder != null)
        {
          Z_GameFlags.pathfinder.DrawPathFindingManager(8f);
          Z_GameFlags.pathfinder.DrawEntranceBlocks();
        }
        Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation));
        TextFunctions.DrawTextWithDropShadow("Cursor Grid = X:" + (object) worldSpaceToTile.X + " Y:" + (object) worldSpaceToTile.Y, RenderMath.GetPixelZoomOneToOne() * 2f, new Vector2(1000f, 740f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, true, true);
      }
      if (DebugFlags.ShowDebuggingServerInfo)
      {
        string str1 = "";
        string str2 = "";
        if (player != null)
        {
          bool HasThisSocial;
          SocialPair thisSocialPair1 = player.socialmanager.getThisSocialPair(MainVariables.ThisGame, out HasThisSocial);
          if (HasThisSocial)
            str1 = thisSocialPair1.UID;
          SocialPair thisSocialPair2 = player.socialmanager.getThisSocialPair(SocialType.Pixona, out HasThisSocial);
          if (HasThisSocial)
            str2 = thisSocialPair2.UID;
        }
        TextFunctions.DrawTextWithDropShadow("PIP: " + str1, 2f, new Vector2(0.0f, 690f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
        TextFunctions.DrawTextWithDropShadow("PIXID: " + str2, 2f, new Vector2(0.0f, 710f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
        TextFunctions.DrawTextWithDropShadow("SESSION CONNECTION: " + SpringCommManager.Singleton.IsSessionConnected.ToString(), 2f, new Vector2(0.0f, 730f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
        TextFunctions.DrawTextWithDropShadow("SESSION LOGIN: " + SpringCommManager.Singleton.ConnectedToServer.ToString(), 2f, new Vector2(0.0f, 750f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
        TextFunctions.DrawTextWithDropShadow("SERVERTIME: " + (object) player.Stats.datetimemanager.TimeOfLastServerGet + "  ||| ISSERVERTIME?" + player.Stats.datetimemanager.IsCurrentlyUsingServerTime.ToString(), 2f, new Vector2(0.0f, 670f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
      }
      if (Z_DebugFlags.DisplayFPS && AssetContainer.springFont != null)
        this.fpscounter.DrawFPS(DeltaTime, AssetContainer.springFont, AssetContainer.pointspritebatch07Final);
      if (!Z_DebugFlags.IsBetaVersion || Game1.gamestate == GAMESTATE.SplashScreen)
        return;
      if (this.betabanner == null)
        this.betabanner = new BetaBanner();
      if (GameFlags.PhotoMode)
        return;
      this.betabanner.DrawBetaBanner();
    }
  }
}
