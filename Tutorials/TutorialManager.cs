// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tutorials.TutorialManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials.BuildCellBlock;
using TinyZoo.Tutorials.BuildThing;
using TinyZoo.Tutorials.FinalSummary;
using TinyZoo.Tutorials.Informatic;
using TinyZoo.Tutorials.SpecificTuts;
using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Tutorials.Z_Tutorials;
using TinyZoo.Tutorials.Z_Tutorials.BuildFirstPen;

namespace TinyZoo.Tutorials
{
  internal class TutorialManager
  {
    private BlackOut blackout;
    internal static TUTORIALTYPE currenttutorial;
    private SmartCharacterBox charactertextbox;
    private Arrow arrow;
    private int StateCounter;
    private Vector2 ArrowLocation;
    private float Timer;
    private EarningsNew earnings;
    private BuildFirstThing buildfirstthing;
    private Play_Informatic playinformatic;
    private SummaryManager summarymanager;
    private Z_ManageShop z_manageshop;
    private Z_SurpriseBirth z_SurpriseBirth;
    private BankReminder bankreminder;
    private StartTheDayTutorial tarttheday;
    private GameplayIntro gameplayintro;
    private ResearchBuildReminder researchreminder;
    private DeathRetryPopUp deathretrypopup;
    private NoBeams nobeams;
    private ProductionTypeInformation productiontype;
    private LowBeamsBeforeStart lowbeams;
    private RevealPrison revealprison;
    private BuildCellBlockTutorial buildcellblock;
    private ShakeShakeTutorial shakeshake;
    private RateGame rategame;
    private WelcomeToTheZoo welcometothezoo;
    private TeachBreeding teachbreeding;
    private ZTipManager ztips;
    private Z_BuildFirstPenManager zbuildfirstpenmanager;
    private Z_HintManager zhintmanager;
    private Z_AdjustFood feedtherabbits;
    private Z_FirstNight Z_FirstNight;
    private Z_EmployZooKeeper z_employzookeeper;
    private DateTime cashcaptime;
    private float TipTimer;
    private ZTipType LAST_ztiptype;
    private int ZTIPRepeats;
    private int ZTIPCYCLE;
    internal static bool SkipDraw;

    public void pressedMainMenuShortcut(OverworldButtons shortcuttype)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.WelcomeToTheZoo || shortcuttype != OverworldButtons.Intake)
        return;
      this.welcometothezoo.PressedMap();
    }

    public void ForceEndAllTutorials()
    {
      TutorialManager.currenttutorial = TUTORIALTYPE.None;
      this.arrow = (Arrow) null;
    }

    public void StartNewTutorial(TUTORIALTYPE tutorialtype, Player player) => this.StartTutorial(tutorialtype, player);

    public void TrasferdAnimalsToEnclosuer(Player player)
    {
      if (player.Stats.TutorialsComplete[27])
        return;
      this.StartTutorial(TUTORIALTYPE.Z_FeedRabbits, player);
    }

    public void StartedGamePlay(Player player)
    {
      if (GameFlags.IsArcadeMode)
        return;
      if (TutorialManager.currenttutorial == TUTORIALTYPE.SelectIntake)
      {
        this.DeactivateTutorial(player);
      }
      else
      {
        if (!player.Stats.TutorialsComplete[14] || player.Stats.TutorialsComplete[25])
          return;
        this.StartTutorial(TUTORIALTYPE.TeachShakeShake, player);
      }
    }

    public void TryToDoPopUpForMoneyEarned(long FullMoney, int HowMuchActuallyEarned)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        return;
      this.earnings = new EarningsNew(FullMoney, HowMuchActuallyEarned);
      TutorialManager.currenttutorial = TUTORIALTYPE.EarnngPopUp;
    }

    public void StartBeamPopUp(int BeamsLeft)
    {
      TutorialManager.currenttutorial = TUTORIALTYPE.LowBeams;
      this.lowbeams = new LowBeamsBeforeStart(BeamsLeft);
    }

    public void PopUpStatExplanaition(ProductionType __productiontype, Player player)
    {
      this.productiontype = new ProductionTypeInformation(__productiontype, player);
      TutorialManager.currenttutorial = TUTORIALTYPE.ProductionType;
    }

    public void DoDeathPopUp(Player player) => this.StartTutorial(TUTORIALTYPE.DeathRetryPopUp, player);

    public void DoBuildMoreReseacrhPopUp()
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        return;
      TutorialManager.currenttutorial = TUTORIALTYPE.SimplePopUp;
      this.charactertextbox = new SmartCharacterBox("You should build more research buildings, the more you have the faster things go!", AnimalType.Scientist);
    }

    public void DoNoBeamPopUp(Player player) => this.StartTutorial(TUTORIALTYPE.NoBeamsPopUp, player);

    public void CannotAffordFuel(int CST)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        return;
      TutorialManager.currenttutorial = TUTORIALTYPE.SimplePopUp;
      this.charactertextbox = new SmartCharacterBox("You need to pay fuel costs to fly the prisoners to this planet, you need $" + (object) CST + ".", AnimalType.Administrator);
    }

    public void DOCellBLockPop()
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        return;
      TutorialManager.currenttutorial = TUTORIALTYPE.SimplePopUp;
      this.charactertextbox = new SmartCharacterBox("This tab contains cell blocks.~Build one now so you have more space for prisoners.", AnimalType.Administrator);
    }

    public void DoCashCapChangePopUp(int NewCash, int OldCash)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        return;
      this.cashcaptime = new DateTime();
      this.blackout = new BlackOut();
      this.blackout.SetAllColours(ColourData.IconYellow);
      this.blackout.SetAlpha(false, 0.4f, 0.0f, 0.7f);
      TutorialManager.currenttutorial = TUTORIALTYPE.SimplePopUp;
      if (NewCash > OldCash)
        this.charactertextbox = new SmartCharacterBox("Cash limit increased to $" + (object) NewCash, AnimalType.Administrator);
      else if (NewCash == OldCash)
        this.charactertextbox = new SmartCharacterBox("Cash limit is already at maximum.", AnimalType.Administrator);
      else
        this.charactertextbox = new SmartCharacterBox("Cash limit reduced to $" + (object) NewCash, AnimalType.Administrator);
    }

    public void EnteredOverWorld(Player player)
    {
      if (!player.Stats.TutorialsComplete[1])
      {
        if (player.prisonlayout.cellblockcontainer.GetCountOfThisSpecialBuilding(TILETYPE.LifeSupport) > 0)
        {
          player.Stats.TutorialsComplete[1] = true;
          player.Stats.TutorialsComplete[13] = true;
        }
        else
          this.StartTutorial(TUTORIALTYPE.Z_BuildFirstPen, player);
      }
      else
      {
        if (!player.Stats.TutorialsComplete[13])
          return;
        if (!GameFlags.IsConsoleVersion && !player.Stats.HasRatedGame && player.playerbehaviour.GetTotalMinutesPlayed() >= 12.0)
        {
          if (TutorialManager.currenttutorial != TUTORIALTYPE.None || player.livestats.NewlyDeads.Count != 0)
            return;
          int num = FeatureFlags.GraveYrardCreatorActive ? 1 : 0;
        }
        else if (player.prisonlayout.cellblockcontainer.GetCountOfThisSpecialBuilding(TILETYPE.Research_PrisonPlanet) == 0)
        {
          if (TutorialManager.currenttutorial != TUTORIALTYPE.None || player.livestats.NewlyDeads.Count != 0)
            return;
          int num = FeatureFlags.GraveYrardCreatorActive ? 1 : 0;
        }
        else
        {
          if (player.livestats.CheckCellBlockReminderTutorial(player) || player.prisonlayout.GetTotalResearch() > 0 && !Researcher.IsCurrentlyResearching && (!player.Stats.research.AllComplete() && !player.Stats.research.OnlyHasAlienResearch_AndItsBlockedByRank(player)) || (player.prisonlayout.GetTotalResearch() <= 0 || !Researcher.ResearchComplete))
            return;
          player.Stats.research.AllComplete();
        }
      }
    }

    public void CheckCashCap(Player player)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None || OverWorldManager.overworldstate != OverWOrldState.MainMenu || (!player.Stats.TutorialsComplete[14] || FeatureFlags.BlockCash) || (!OWHUDManager.IsCashOnScreen() || OverWorldManager.overworldstate != OverWOrldState.MainMenu || (OWHUDManager.reseachsummary != null || TinyZoo.Game1.gamestate == GAMESTATE.GamePlay)) || GameFlags.BountyMode)
        return;
      bool flag = false;
      DateTime cashcaptime = this.cashcaptime;
      if (DateTime.UtcNow.Ticks - this.cashcaptime.Ticks > new TimeSpan(0, 5, 0).Ticks)
      {
        flag = true;
        this.cashcaptime = DateTime.UtcNow;
      }
      if (!flag)
        return;
      this.StartTutorial(TUTORIALTYPE.BankReminder, player);
    }

    public void WentToQuestCityView(Player player)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.WelcomeToTheZoo)
        return;
      this.welcometothezoo.WentToQuestSelection(player);
    }

    private void StartTutorial(TUTORIALTYPE tuttype, Player player)
    {
      if (tuttype == TUTORIALTYPE.Z_Hints && TutorialManager.currenttutorial == TUTORIALTYPE.Z_Hints || DebugFlags.DisableTutorials_DEP_KeepTrue)
        return;
      TutorialManager.currenttutorial = tuttype;
      switch (tuttype)
      {
        case TUTORIALTYPE.WelcomeToTheZoo:
          this.welcometothezoo = new WelcomeToTheZoo(player);
          break;
        case TUTORIALTYPE.Z_BuildFirstPen:
          this.zbuildfirstpenmanager = new Z_BuildFirstPenManager(player);
          break;
        case TUTORIALTYPE.Z_FeedRabbits:
          this.feedtherabbits = new Z_AdjustFood();
          break;
        case TUTORIALTYPE.Z_Hints:
          this.zhintmanager = new Z_HintManager(player);
          break;
        case TUTORIALTYPE.Z_ManageShop:
          this.z_manageshop = new Z_ManageShop();
          break;
        case TUTORIALTYPE.EmployKeeper:
          this.z_employzookeeper = new Z_EmployZooKeeper(player);
          break;
        case TUTORIALTYPE.FirstNight:
          this.Z_FirstNight = new Z_FirstNight();
          break;
        case TUTORIALTYPE.SurpriseBirth:
          this.z_SurpriseBirth = new Z_SurpriseBirth();
          break;
        case TUTORIALTYPE.Breeding:
          this.teachbreeding = new TeachBreeding();
          break;
        case TUTORIALTYPE.GamePlayIntro:
          this.gameplayintro = new GameplayIntro();
          this.charactertextbox = (SmartCharacterBox) null;
          break;
        case TUTORIALTYPE.RevealPrison:
          this.Timer = 0.0f;
          this.charactertextbox = new SmartCharacterBox("", AnimalType.Administrator);
          FeatureFlags.DemolishEnabled = false;
          FeatureFlags.BlockBuild = true;
          FeatureFlags.BlockIntake = true;
          FeatureFlags.BlockSettings = true;
          FeatureFlags.BlockStats = true;
          FeatureFlags.BlockBreeding = true;
          FeatureFlags.BlockTimer = true;
          FeatureFlags.BlockCash = true;
          FeatureFlags.BlockPremiumStore_DEPRICATED = true;
          this.revealprison = new RevealPrison();
          this.charactertextbox.Delay = 0.3f;
          FeatureFlags.BlockIntake = true;
          FeatureFlags.BlockPlayerMoveCamera = true;
          break;
        case TUTORIALTYPE.RevealCashAndBuild:
          this.buildfirstthing = new BuildFirstThing(ref this.arrow, ref this.ArrowLocation, player);
          break;
        case TUTORIALTYPE.BuildFinished:
          this.summarymanager = new SummaryManager(ref this.charactertextbox);
          break;
        case TUTORIALTYPE.BuildResearch:
          this.researchreminder = new ResearchBuildReminder(ref this.arrow, ref this.ArrowLocation, player);
          break;
        case TUTORIALTYPE.BankReminder:
          this.bankreminder = new BankReminder(ref this.arrow, ref this.ArrowLocation, player);
          break;
        case TUTORIALTYPE.DeathRetryPopUp:
          this.deathretrypopup = new DeathRetryPopUp();
          break;
        case TUTORIALTYPE.NoBeamsPopUp:
          this.nobeams = new NoBeams();
          break;
        case TUTORIALTYPE.BuildCellBlockReminer:
          this.buildcellblock = new BuildCellBlockTutorial(ref this.arrow, ref this.ArrowLocation, player);
          break;
        case TUTORIALTYPE.TeachShakeShake:
          this.shakeshake = new ShakeShakeTutorial(ref this.arrow, ref this.ArrowLocation, player);
          break;
        case TUTORIALTYPE.RateOurGamme:
          this.rategame = new RateGame();
          break;
        case TUTORIALTYPE.StartTheDay:
          this.tarttheday = new StartTheDayTutorial();
          break;
      }
    }

    public void BuildAStructure(TILETYPE tiletype, Player player)
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.RevealCashAndBuild)
        return;
      if (this.buildfirstthing == null)
        throw new Exception("THIS IS BAD");
      this.buildfirstthing.BuiltaThing();
      this.DeactivateTutorial(player);
    }

    public void ActivatedPetSelectManager()
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.Breeding)
        return;
      this.teachbreeding.ActivatedPetSelectManager();
    }

    public void GoToBreedPairingView()
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.Breeding)
        return;
      this.teachbreeding.GoToBreedPairingView();
    }

    public void BreedButtonPressed()
    {
      if (TutorialManager.currenttutorial != TUTORIALTYPE.Breeding)
        return;
      this.teachbreeding.BreedButtonPressed();
    }

    private void DeactivateTutorial(Player player)
    {
      AnnalyticsEvents.CompletedTutorial(player, TutorialManager.currenttutorial);
      switch (TutorialManager.currenttutorial)
      {
        case TUTORIALTYPE.WelcomeToTheZoo:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[1] = true;
          this.welcometothezoo = (WelcomeToTheZoo) null;
          break;
        case TUTORIALTYPE.Z_BuildFirstPen:
          this.zbuildfirstpenmanager = (Z_BuildFirstPenManager) null;
          player.Stats.TutorialsComplete[2] = true;
          this.StartTutorial(TUTORIALTYPE.WelcomeToTheZoo, player);
          break;
        case TUTORIALTYPE.Z_FeedRabbits:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          this.feedtherabbits = (Z_AdjustFood) null;
          player.Stats.TutorialsComplete[3] = true;
          this.welcometothezoo = (WelcomeToTheZoo) null;
          break;
        case TUTORIALTYPE.Z_Hints:
          this.zhintmanager = (Z_HintManager) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[4] = true;
          break;
        case TUTORIALTYPE.EmployKeeper:
          this.z_employzookeeper = (Z_EmployZooKeeper) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[6] = true;
          break;
        case TUTORIALTYPE.FirstNight:
          this.Z_FirstNight = (Z_FirstNight) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[7] = true;
          break;
        case TUTORIALTYPE.SurpriseBirth:
          this.z_SurpriseBirth = (Z_SurpriseBirth) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[9] = true;
          break;
        case TUTORIALTYPE.Breeding:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          break;
        case TUTORIALTYPE.SelectIntake:
          this.StartTutorial(TUTORIALTYPE.GamePlayIntro, player);
          player.Stats.TutorialsComplete[11] = true;
          break;
        case TUTORIALTYPE.GamePlayIntro:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          player.Stats.TutorialsComplete[12] = true;
          FeatureFlags.BlockEnemySpawn = false;
          FeatureFlags.BlockBeamFiring = false;
          FeatureFlags.BlockDroneMovement = false;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          this.arrow = (Arrow) null;
          break;
        case TUTORIALTYPE.RevealPrison:
          this.revealprison = (RevealPrison) null;
          player.Stats.TutorialsComplete[13] = true;
          this.StartTutorial(TUTORIALTYPE.RevealCashAndBuild, player);
          break;
        case TUTORIALTYPE.RevealCashAndBuild:
          FeatureFlags.ShowGamePlayBeams = true;
          FeatureFlags.ShowGamePlayPeopleToSave = true;
          FeatureFlags.ShowGamePlayProgressBar = true;
          this.buildfirstthing = (BuildFirstThing) null;
          FeatureFlags.OnlyALlowTisThingsToBeBuilt = (List<TILETYPE>) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          FeatureFlags.BlockCloseBuildMenu = false;
          FeatureFlags.BlockPageCycleInBuild = false;
          this.StartTutorial(TUTORIALTYPE.BuildFinished, player);
          player.Stats.TutorialsComplete[14] = true;
          break;
        case TUTORIALTYPE.BuildFinished:
          FeatureFlags.BlockIntake = false;
          FeatureFlags.BlockBuild = false;
          FeatureFlags.BlockPlayerMoveCamera = false;
          FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = false;
          FeatureFlags.BlockSettings = false;
          FeatureFlags.BlockTimer = false;
          FeatureFlags.DemolishEnabled = true;
          FeatureFlags.BlockBuyPanel = false;
          FeatureFlags.BlockCash = false;
          FeatureFlags.BlockPremiumStore_DEPRICATED = false;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          this.arrow = (Arrow) null;
          player.Stats.TutorialsComplete[15] = true;
          break;
        case TUTORIALTYPE.BuildResearch:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          this.arrow = (Arrow) null;
          break;
        case TUTORIALTYPE.BankReminder:
          this.arrow = (Arrow) null;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          this.bankreminder = (BankReminder) null;
          break;
        case TUTORIALTYPE.DeathRetryPopUp:
          player.Stats.TutorialsComplete[19] = true;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          break;
        case TUTORIALTYPE.NoBeamsPopUp:
          player.Stats.TutorialsComplete[20] = true;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          break;
        case TUTORIALTYPE.TeachShakeShake:
          player.Stats.TutorialsComplete[25] = true;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          FeatureFlags.BlockBeamFiring = false;
          FeatureFlags.BlockDroneMovement = false;
          break;
        case TUTORIALTYPE.RateOurGamme:
          player.Stats.TutorialsComplete[26] = true;
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          break;
        case TUTORIALTYPE.StartTheDay:
          TutorialManager.currenttutorial = TUTORIALTYPE.None;
          break;
      }
    }

    public void UpdateZTips(float DeltaTime, Player player, OverWOrldState overworldstate)
    {
    }

    private bool SHouldSHowRepeatTip()
    {
      ++this.ZTIPCYCLE;
      if (this.ZTIPRepeats == 0 && this.ZTIPCYCLE == 2)
      {
        ++this.ZTIPRepeats;
        return true;
      }
      if (this.ZTIPRepeats == 1 && this.ZTIPCYCLE == 3)
      {
        ++this.ZTIPRepeats;
        return true;
      }
      if (this.ZTIPRepeats <= 1 || this.ZTIPCYCLE != 5)
        return false;
      ++this.ZTIPRepeats;
      return true;
    }

    public void UpdateTutorialManager(ref float DeltaTime, ref float SimulationTime, Player player)
    {
      if (this.ztips != null && this.ztips.UpdateZTipManager(DeltaTime, player))
        this.ztips = (ZTipManager) null;
      if (OverWorldManager.fulladvertmanager != null && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld || TinyZoo.Game1.gamestate == GAMESTATE.IAPStore)
        return;
      if (FeatureFlags.GraveYrardCreatorActive)
      {
        TutorialManager.currenttutorial = TUTORIALTYPE.None;
      }
      else
      {
        if (TutorialManager.currenttutorial == TUTORIALTYPE.None || OverWorldManager.IsGameIntro)
          return;
        if (this.arrow != null)
          this.arrow.UpdateArrow(DeltaTime);
        switch (TutorialManager.currenttutorial)
        {
          case TUTORIALTYPE.WelcomeToTheZoo:
            if (!this.welcometothezoo.UpdateWelcomeToTheZoo(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.Z_BuildFirstPen:
            if (!this.zbuildfirstpenmanager.UpdateZ_BuildFirstPenManager(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.Z_FeedRabbits:
            if (!this.feedtherabbits.UpdateZ_AdjustFood(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            this.StartNewTutorial(TUTORIALTYPE.StartTheDay, player);
            break;
          case TUTORIALTYPE.Z_Hints:
            if (!this.zhintmanager.UpdateZ_Hints(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            this.StartNewTutorial(TUTORIALTYPE.Z_ManageShop, player);
            break;
          case TUTORIALTYPE.Z_ManageShop:
            if (!this.z_manageshop.UpdateZ_Z_ManageShop(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            this.StartNewTutorial(TUTORIALTYPE.EmployKeeper, player);
            break;
          case TUTORIALTYPE.EmployKeeper:
            if (!this.z_employzookeeper.UpdateZ_EmployZooKeeper(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.FirstNight:
            if (!this.Z_FirstNight.UpdateZ_FirstNight(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.SurpriseBirth:
            if (!this.z_SurpriseBirth.UpdateSurpriseBirth(ref SimulationTime, ref DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.Breeding:
            if (!this.teachbreeding.UpdateTeachBreeding(ref SimulationTime, ref DeltaTime, player))
              break;
            this.teachbreeding = (TeachBreeding) null;
            player.Stats.TutorialsComplete[10] = true;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.SelectIntake:
            SimulationTime = 0.0f;
            this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
            break;
          case TUTORIALTYPE.GamePlayIntro:
            if (!this.gameplayintro.UpdateGameplayIntro(ref DeltaTime, player, ref this.arrow, ref this.ArrowLocation))
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.RevealPrison:
            this.revealprison.UpdateRevealPrison();
            if ((double) this.Timer < 1.0)
            {
              this.Timer += DeltaTime;
              if ((double) this.Timer >= 1.0)
                FeatureFlags.DrawRoofInOverWorld = false;
            }
            if (!this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player) || (double) Sengine.WorldOriginandScale.Z <= 1.89999997615814)
              break;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.RevealCashAndBuild:
            this.buildfirstthing.UpdateBuildFirstThing(ref DeltaTime, player, ref this.arrow, ref this.ArrowLocation);
            break;
          case TUTORIALTYPE.BuildFinished:
            if (!this.summarymanager.UpdateSummaryManager(player, ref DeltaTime, this.charactertextbox, ref this.arrow, ref this.ArrowLocation))
              break;
            this.summarymanager = (SummaryManager) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.BuildResearch:
            if (!this.researchreminder.UpdateResearchBuildReminder(ref DeltaTime, player))
              break;
            this.researchreminder = (ResearchBuildReminder) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.BankReminder:
            if (!this.bankreminder.UpdateBankReminder(ref DeltaTime, player))
              break;
            this.researchreminder = (ResearchBuildReminder) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.SimplePopUp:
            if (this.blackout != null)
            {
              if (this.charactertextbox.Exiting && (double) this.blackout.fTargetAlpha != 0.0)
                this.blackout.SetAlpha(true, 0.3f, 1f, 0.0f);
              this.blackout.UpdateColours(DeltaTime);
            }
            if (this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player))
            {
              TutorialManager.currenttutorial = TUTORIALTYPE.None;
              this.charactertextbox = (SmartCharacterBox) null;
              this.blackout = (BlackOut) null;
            }
            DeltaTime = 0.0f;
            player.inputmap.ClearAllInput(player);
            break;
          case TUTORIALTYPE.DeathRetryPopUp:
            if (!this.deathretrypopup.UpdateDeathRetryPopUp(DeltaTime, player))
              break;
            this.deathretrypopup = (DeathRetryPopUp) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.NoBeamsPopUp:
            if (!this.nobeams.UpdateNoBeams(DeltaTime, player))
              break;
            this.nobeams = (NoBeams) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.ProductionType:
            if (!this.productiontype.UpdateProductionTypeInformation(ref DeltaTime, player))
              break;
            this.productiontype = (ProductionTypeInformation) null;
            player.inputmap.ClearAllInput(player);
            TutorialManager.currenttutorial = TUTORIALTYPE.None;
            break;
          case TUTORIALTYPE.EarnngPopUp:
            if (!this.earnings.UpdateEarningsNew(ref DeltaTime, player))
              break;
            this.earnings = (EarningsNew) null;
            player.inputmap.ClearAllInput(player);
            TutorialManager.currenttutorial = TUTORIALTYPE.None;
            break;
          case TUTORIALTYPE.LowBeams:
            if (!this.lowbeams.UpdateLowBeamsBeforeStart(ref DeltaTime, player))
              break;
            FeatureFlags.BlockTimer = false;
            FeatureFlags.BlockCash = false;
            this.lowbeams = (LowBeamsBeforeStart) null;
            player.inputmap.ClearAllInput(player);
            TutorialManager.currenttutorial = TUTORIALTYPE.None;
            break;
          case TUTORIALTYPE.BuildCellBlockReminer:
            if (!this.buildcellblock.UpdateBuildCellBlockTutorial(player, ref DeltaTime))
              break;
            this.arrow = (Arrow) null;
            this.buildcellblock = (BuildCellBlockTutorial) null;
            player.inputmap.ClearAllInput(player);
            TutorialManager.currenttutorial = TUTORIALTYPE.None;
            break;
          case TUTORIALTYPE.TeachShakeShake:
            if (!this.shakeshake.UpdateShakeShakeTutorial(DeltaTime, player))
              break;
            this.arrow = (Arrow) null;
            this.DeactivateTutorial(player);
            break;
          case TUTORIALTYPE.RateOurGamme:
            if (!this.rategame.UpdateRateGame(DeltaTime, player))
              break;
            this.DeactivateTutorial(player);
            this.rategame = (RateGame) null;
            break;
          case TUTORIALTYPE.StartTheDay:
            if (!this.tarttheday.UpdateStartTheDay(DeltaTime, player, ref SimulationTime))
              break;
            this.DeactivateTutorial(player);
            this.tarttheday = (StartTheDayTutorial) null;
            player.Stats.TutorialsComplete[27] = true;
            break;
        }
      }
    }

    public void DrawTutorialManager()
    {
      if (FeatureFlags.GraveYrardCreatorActive || TutorialManager.SkipDraw)
      {
        TutorialManager.SkipDraw = false;
      }
      else
      {
        if (OverWorldManager.fulladvertmanager != null && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld || TinyZoo.Game1.gamestate == GAMESTATE.IAPStore)
          return;
        if (this.ztips != null)
          this.ztips.DrawZTipManager();
        if (TutorialManager.currenttutorial == TUTORIALTYPE.None)
          return;
        if (this.blackout != null)
          this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
        switch (TutorialManager.currenttutorial)
        {
          case TUTORIALTYPE.WelcomeToTheZoo:
            this.welcometothezoo.DrawWelcomeToTheZoo();
            break;
          case TUTORIALTYPE.Z_BuildFirstPen:
            this.zbuildfirstpenmanager.DrawZ_BuildFirstPenManager();
            break;
          case TUTORIALTYPE.Z_FeedRabbits:
            this.feedtherabbits.DrawZ_FirstNight();
            break;
          case TUTORIALTYPE.Z_Hints:
            this.zhintmanager.DrawZ_Hints();
            break;
          case TUTORIALTYPE.Z_ManageShop:
            this.z_manageshop.DrawZ_ManageShop();
            break;
          case TUTORIALTYPE.EmployKeeper:
            this.z_employzookeeper.DrawZ_EmployZooKeeper();
            break;
          case TUTORIALTYPE.FirstNight:
            this.Z_FirstNight.DrawZ_FirstNight();
            break;
          case TUTORIALTYPE.SurpriseBirth:
            this.z_SurpriseBirth.DrawSurpriseBirth();
            break;
          case TUTORIALTYPE.Breeding:
            this.teachbreeding.DrawTeachBreeding();
            break;
          case TUTORIALTYPE.GamePlayIntro:
            this.gameplayintro.DrawGameplayIntro();
            break;
          case TUTORIALTYPE.RevealCashAndBuild:
            this.buildfirstthing.DrawBuildFirstThing();
            break;
          case TUTORIALTYPE.BuildResearch:
            this.researchreminder.DrawResearchBuildReminder();
            break;
          case TUTORIALTYPE.BankReminder:
            this.bankreminder.DrawBankReminder();
            break;
          case TUTORIALTYPE.DeathRetryPopUp:
            this.deathretrypopup.DrawDeathRetryPopUp();
            break;
          case TUTORIALTYPE.NoBeamsPopUp:
            this.nobeams.DrawNoBeams();
            break;
          case TUTORIALTYPE.ProductionType:
            this.productiontype.DrawProductionTypeInformation();
            break;
          case TUTORIALTYPE.EarnngPopUp:
            this.earnings.DrawEarningsNew();
            break;
          case TUTORIALTYPE.LowBeams:
            this.lowbeams.DrawLowBeamsBeforeStart();
            break;
          case TUTORIALTYPE.BuildCellBlockReminer:
            this.buildcellblock.DrawBuildCellBlockTutorial();
            break;
          case TUTORIALTYPE.TeachShakeShake:
            this.shakeshake.DrawShakeShakeTutorial();
            break;
          case TUTORIALTYPE.RateOurGamme:
            this.rategame.DrawRateGame();
            break;
          case TUTORIALTYPE.StartTheDay:
            this.tarttheday.DrawStartTheDay();
            break;
        }
        if (this.charactertextbox != null)
          this.charactertextbox.DrawSmartCharacterBox();
        if (this.arrow != null)
          this.arrow.DrawArrow(this.ArrowLocation);
        if (this.playinformatic != null)
          this.playinformatic.DrawPlay_Informatic();
        if (this.ztips == null)
          return;
        this.ztips.DrawZTipManager();
      }
    }
  }
}
