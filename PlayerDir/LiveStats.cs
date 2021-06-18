// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.LiveStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SpringIAP;
using SpringIAP.IAP_User;
using System;
using System.Collections.Generic;
using TinyZoo.Blance;
using TinyZoo.GamePlay;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.PlayerDir.StoreRooms;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;
using TinyZoo.Utils;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_Breeding.RandomBreeding;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_Fights;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir
{
  internal class LiveStats
  {
    internal static int ToiletMaxCapacity = 1;
    internal static int AnimalOrderUID = 0;
    public LiveSlectedShop SelectedSHop;
    private WaveInfo _waveinfo;
    public IntakeInfo intakefornextlevel;
    public IntakeInfo intakeUseForQuit;
    public int RemovedIndex;
    public WaveInfo waveinfoFromPrison;
    public int SpeedUpSimulation;
    public bool SimulationIsPaused;
    public int SelectedPrisonID = 1;
    public ConsumptionStatus consumptionstatus;
    public bool LevelIsTransferFromHoldingCell;
    internal static ReqForPeople reqforpeople;
    public List<IntakePerson> NewlyDeads;
    private BlingDingCosts blindingcosts;
    public BonusAdvertChecker bonusadvertchecker;
    public int HCH;
    public float SaveTime;
    public bool skp;
    public static bool DraggingDragZone;
    private static float CycleTimer;
    internal static int EnemyMovementCycle;
    public bool IsDraggingShip;
    public bool ArcadeModeJustCompleted;
    public bool IsFullScreen;
    public StockTime stocktimes;
    public float LastCalculatedFacilities = -1f;
    public bool eventdone;
    public AnimalType eventenemy;
    public bool EVWasMoney;
    public bool WasNotPerfectButFinishedBounty;
    public bool GaveUpBounty;
    internal static bool IsCNY = false;
    internal static bool IsChristmas = false;
    private float _MoralityScore;
    private static bool _AHumanDied;
    private static bool _MoneyWentUp;
    private static bool _EarnedResearch;
    private static float TreeTimeGap;
    private static float CurrentTreeTimer = -1f;
    private static List<ZooMoment> zoomoments = new List<ZooMoment>();
    internal static List<TheDead> DeathUIDs = new List<TheDead>();
    internal static List<Employee> Quitters = new List<Employee>();
    internal static List<int> GatesToBreak = new List<int>();
    internal static List<RenderComponent> ComponentsForUpdate;
    private static bool ScrubMorality;
    private static List<Employee> removethese = new List<Employee>();
    private static bool SomethingHappened;
    public float SecondsZooWasOpen;
    public List<HungryAnimal> hungryanimals;
    public WaveInfo AnimalsJustTraded;

    internal static bool AHumanDied
    {
      get => LiveStats._AHumanDied;
      set
      {
        if (value)
        {
          LiveStats.SomethingHappened = true;
          LiveStats._AHumanDied = true;
        }
        else
          LiveStats._AHumanDied = false;
      }
    }

    internal static bool EarnedResearch
    {
      get => LiveStats._EarnedResearch;
      set
      {
        if (value)
        {
          LiveStats.SomethingHappened = true;
          LiveStats._EarnedResearch = true;
        }
        else
          LiveStats._EarnedResearch = false;
      }
    }

    public bool HasHungyAnimalsInThisPen(int PenUID)
    {
      for (int index = 0; index < this.hungryanimals.Count; ++index)
      {
        if (this.hungryanimals[index].Cell_UID == PenUID)
          return true;
      }
      return false;
    }

    internal static void SetCOTWO_TreeTimer(float TimeStamp)
    {
      LiveStats.TreeTimeGap = TimeStamp;
      if ((double) LiveStats.TreeTimeGap == -1.0)
      {
        LiveStats.CurrentTreeTimer = -1f;
      }
      else
      {
        if ((double) LiveStats.CurrentTreeTimer != -1.0)
          return;
        LiveStats.CurrentTreeTimer = TimeStamp * 0.5f;
      }
    }

    internal static bool MoneyWentUp
    {
      get => LiveStats._MoneyWentUp;
      set
      {
        if (value)
        {
          LiveStats.SomethingHappened = true;
          LiveStats._MoneyWentUp = true;
        }
        else
          LiveStats._MoneyWentUp = false;
      }
    }

    public LiveStats(Player player)
    {
      this.bonusadvertchecker = new BonusAdvertChecker(player);
      this.NewlyDeads = new List<IntakePerson>();
      this.consumptionstatus = new ConsumptionStatus();
      if (LiveStats.reqforpeople != null)
        return;
      LiveStats.reqforpeople = new ReqForPeople();
    }

    internal static void AddEventToTheDay(ZooMoment zoommoment) => LiveStats.zoomoments.Add(zoommoment);

    internal static void AddComponentThatNeedsLink(RenderComponent factory)
    {
      if (LiveStats.ComponentsForUpdate == null)
        LiveStats.ComponentsForUpdate = new List<RenderComponent>();
      LiveStats.ComponentsForUpdate.Add(factory);
    }

    internal static void RemoveJobPosting(TILETYPE tiletype, EmployeeType roamingemployeetype)
    {
      for (int index = LiveStats.zoomoments.Count - 1; index > -1; --index)
      {
        if (LiveStats.zoomoments[index].zoomoment == ZOOMOMENT.NewApplicant && (TILETYPE) LiveStats.zoomoments[index].tileType_int == tiletype && (EmployeeType) LiveStats.zoomoments[index].roamingemployeetype == roamingemployeetype)
          LiveStats.zoomoments.RemoveAt(index);
      }
    }

    internal static void AddEmployeeToQuitList(Employee employeetoquit) => LiveStats.Quitters.Add(employeetoquit);

    internal static void AddGateToBreak(int PenUID)
    {
      LiveStats.GatesToBreak.Add(PenUID);
      LiveStats.zoomoments.Add(new ZooMoment(ZOOMOMENT.GateBroke));
    }

    internal static void CheckQuitersOnFiring(Employee EmployeeJustFired)
    {
      for (int index = LiveStats.Quitters.Count - 1; index > -1; --index)
      {
        if (LiveStats.Quitters[index] == EmployeeJustFired)
        {
          LiveStats.Quitters.RemoveAt(index);
          break;
        }
      }
    }

    public float MoralityScore
    {
      get => this._MoralityScore;
      set
      {
        if ((double) this._MoralityScore == (double) value)
          return;
        if ((int) this._MoralityScore != (int) value)
        {
          LiveStats.ScrubMorality = true;
          LiveStats.SomethingHappened = true;
        }
        this._MoralityScore = value;
      }
    }

    internal static void RemoveThisTempEmployee(Employee employee)
    {
      LiveStats.removethese.Add(employee);
      LiveStats.SomethingHappened = true;
    }

    public void EndWorkingDay(float _SecondsZooWasOpen) => this.SecondsZooWasOpen = _SecondsZooWasOpen;

    public void AddHungryAnimal(
      PrisonerInfo prisoner,
      Vector2Int GateLocation,
      FoodSet foodsetforthisanimal,
      int Cell_UID)
    {
      bool flag = false;
      for (int index = 0; index < this.hungryanimals.Count; ++index)
      {
        if (this.hungryanimals[index].GateLocation.CompareMatches(GateLocation))
        {
          this.hungryanimals[index].AddAnimal(prisoner, foodsetforthisanimal);
          flag = true;
        }
      }
      if (flag)
        return;
      this.hungryanimals.Add(new HungryAnimal(prisoner, GateLocation, foodsetforthisanimal, Cell_UID));
    }

    public void SetWaterStatusForTrough(int XLox, int Yloc, int HasWater_zeroFalse)
    {
      if (Z_GameFlags.WaterTroughStates == null)
        Z_GameFlags.WaterTroughStates = new List<Vector3Int>();
      Z_GameFlags.WaterTroughStates.Add(new Vector3Int(XLox, Yloc, HasWater_zeroFalse));
    }

    public void SortHungryAnimals() => this.hungryanimals.Sort(new Comparison<HungryAnimal>(HungryAnimal.SortHungryAnimal));

    public bool HasActiveEvent() => true;

    internal static void UpdateInOverworld(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors,
      OverWorldManager overworldmanager,
      float SimulationTime)
    {
      if ((double) LiveStats.CurrentTreeTimer > 0.0 && Z_GameFlags.ParkIsOpen())
      {
        LiveStats.CurrentTreeTimer -= SimulationTime;
        if ((double) LiveStats.CurrentTreeTimer < 0.0)
        {
          CarbonMap.SpawnC02();
          LiveStats.CurrentTreeTimer = LiveStats.TreeTimeGap;
          if (CarbonMap.NeedtORecreateCarbonMap)
            CarbonMap.RecreateCarbonMap(player);
        }
      }
      if (LiveStats.SomethingHappened)
      {
        if (LiveStats._MoneyWentUp)
        {
          LiveStats._MoneyWentUp = false;
          QuestScrubber.ScrubOnCash(player);
        }
        if (LiveStats._AHumanDied)
        {
          QuestScrubber.ScrubOnHumanDeath(player);
          LiveStats._AHumanDied = false;
        }
        if (LiveStats.ScrubMorality)
        {
          LiveStats.ScrubMorality = false;
          QuestScrubber.ScrubOnMoralityPointChange(player);
        }
        if (LiveStats._EarnedResearch)
        {
          LiveStats._EarnedResearch = false;
          QuestScrubber.ScrubOnResearch(player);
        }
        LiveStats.SomethingHappened = false;
        if (LiveStats.removethese.Count > 0)
        {
          for (int index = 0; index < LiveStats.removethese.Count; ++index)
          {
            CustomerManager.RemoveThisEmployee(LiveStats.removethese[index]);
            player.employees.RemoveEmployee(LiveStats.removethese[index]);
          }
          LiveStats.removethese = new List<Employee>();
        }
      }
      player.animalsonorder.UpdateAnimalsOnOrder(player, overworldmanager);
      if (LiveStats.ComponentsForUpdate != null)
      {
        for (int index = 0; index < LiveStats.ComponentsForUpdate.Count; ++index)
        {
          if (LiveStats.ComponentsForUpdate[index].componenttype == RenderComponentType.FactorySmoke)
          {
            RenderComponent renderComponent = LiveStats.ComponentsForUpdate[index];
          }
        }
        LiveStats.ComponentsForUpdate = (List<RenderComponent>) null;
      }
      if (LiveStats.zoomoments.Count <= 0)
        return;
      for (int index1 = LiveStats.zoomoments.Count - 1; index1 > -1; --index1)
      {
        if ((OverWorldManager.zoopopupHolder.IsNull() || LiveStats.zoomoments[index1].AllowUpdateDuringPopUp) && (double) Z_GameFlags.DayTimer > (double) LiveStats.zoomoments[index1].TimeOfDay)
        {
          bool flag1 = true;
          switch (LiveStats.zoomoments[index1].zoomoment)
          {
            case ZOOMOMENT.Birth:
              int ChildUI;
              bool ParentAlreadyDead;
              bool IsBreedingRoomBaby;
              player.breeds.TryAndBirth(player, out ChildUI, out ParentAlreadyDead, out IsBreedingRoomBaby);
              if (ChildUI != -1 && !ParentAlreadyDead)
              {
                if (IsBreedingRoomBaby)
                {
                  Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalBirthInBreedingRoom, ChildUI)
                  {
                    AlertStatus = NotificationAlertStatus.Special_Heart
                  }, player);
                  QuestScrubber.ScrubOnBirth(player, true);
                  break;
                }
                if (LiveStats.zoomoments[index1].zoomoment == ZOOMOMENT.Birth && !player.Stats.TutorialsComplete[9])
                  GameStateManager.tutorialmanager.StartNewTutorial(TUTORIALTYPE.SurpriseBirth, player);
                Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalBirth, ChildUI)
                {
                  AlertStatus = NotificationAlertStatus.Special_Heart
                }, player);
                QuestScrubber.ScrubOnBirth(player);
                break;
              }
              break;
            case ZOOMOMENT.AnimalDeath:
            case ZOOMOMENT.NonPenAnimalDeath:
              bool flag2 = false;
              for (int index2 = 0; index2 < LiveStats.DeathUIDs.Count; ++index2)
              {
                if (!flag2)
                {
                  int CellBoockUID = -1;
                  PrisonerInfo prisonerInfo = LiveStats.zoomoments[index1].zoomoment != ZOOMOMENT.NonPenAnimalDeath ? player.prisonlayout.GetThisAnimal(LiveStats.DeathUIDs[index2].UID, out CellBoockUID) : player.prisonlayout.GetThisNotInPenAnimal(LiveStats.DeathUIDs[index2].UID);
                  if (prisonerInfo != null && !prisonerInfo.IsDead)
                  {
                    prisonerInfo.IsDead = true;
                    Z_GameFlags.CheckDeaths = true;
                    Z_NotificationManager.RescrubDeath = true;
                    flag2 = true;
                    if (LiveStats.zoomoments[index1].zoomoment == ZOOMOMENT.NonPenAnimalDeath)
                      player.breeds.KilledAnAnimal(prisonerInfo.intakeperson.UID);
                    QuestScrubber.ScrubOnAnimalDeath(player);
                  }
                }
              }
              break;
            case ZOOMOMENT.EmployeeQuit:
              if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
              {
                OverWorldManager.zoopopupHolder.CreateZooPopUps(LiveStats.Quitters[0], ZOOMOMENT.EmployeeQuit);
                LiveStats.Quitters.RemoveAt(0);
                break;
              }
              flag1 = false;
              break;
            case ZOOMOMENT.AnimalFight:
              if (OverWorldManager.overworldstate == OverWOrldState.MainMenu)
              {
                if (!FightMaker.TryToStartFight(player, LiveStats.zoomoments[index1].UID, LiveStats.zoomoments[index1].PenUID, LiveStats.zoomoments[index1].StatsFlag, out PrisonerInfo _))
                  break;
                break;
              }
              flag1 = false;
              break;
            case ZOOMOMENT.TransferAnimalFromBreedingHouse:
              player.breeds.TransferBabyFromBreedingRoomToPen(player, LiveStats.zoomoments[index1].UID);
              Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalTransferedFromBreedingRoom, LiveStats.zoomoments[index1].UID), player);
              break;
            case ZOOMOMENT.GateBroke:
              if (LiveStats.GatesToBreak.Count > 0 && !Z_DebugFlags.IsBetaVersion && player.prisonlayout.cellblockcontainer.BreakGate(LiveStats.GatesToBreak[0], player))
              {
                Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.F_GateBroke, LiveStats.GatesToBreak[0]), player);
                OverWorldManager.zoopopupHolder.CreateZooPopUps(player, true, player.prisonlayout.cellblockcontainer.prisonzones[0]);
                OverWorldManager.eventsmanager.StartBreakOutEvent(player, player.prisonlayout.cellblockcontainer.prisonzones[0]);
                OverWorldManager.overworldenvironment.animalsinpens.StartBreakOut(LiveStats.GatesToBreak[0], player.prisonlayout.cellblockcontainer.GetThisCellBlock(LiveStats.GatesToBreak[0]).GetGateLocation(), player.prisonlayout.cellblockcontainer.GetThisCellBlock(LiveStats.GatesToBreak[0]).GetSpaceBehindGate(player));
                LiveStats.GatesToBreak.RemoveAt(0);
                break;
              }
              break;
            case ZOOMOMENT.CRISPR_Birth:
              bool WasNewVariant = false;
              AnimalRenderDescriptor animalBorn;
              if (player.crisprBreeds.BreedIsComplete(LiveStats.zoomoments[index1].UID, player, ref WasNewVariant, out animalBorn))
              {
                Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_CRISPR_HybridBirth, LiveStats.zoomoments[index1].UID)
                {
                  hybridAnimal = animalBorn,
                  AnimalOrPenUID = LiveStats.zoomoments[index1].UID,
                  AlertStatus = NotificationAlertStatus.Special_Heart
                }, player);
                break;
              }
              break;
            case ZOOMOMENT.FactoryBuildComplete:
              for (int index2 = 0; index2 < player.shopstatus.FacilitiesWithEmployees.Count; ++index2)
              {
                if (player.shopstatus.FacilitiesWithEmployees[index2].ShopUID == LiveStats.zoomoments[index1].UID && player.shopstatus.FacilitiesWithEmployees[index2].factoryproduction != null)
                  player.shopstatus.FacilitiesWithEmployees[index2].factoryproduction.CompleteManufacturing(player.shopstatus.FacilitiesWithEmployees[index2].tiletype, player);
              }
              break;
            case ZOOMOMENT.NewApplicant:
              List<OpenPositions> allOpenPositions = player.employees.openPositionsContainer.GetAllOpenPositions();
              for (int index2 = 0; index2 < allOpenPositions.Count; ++index2)
              {
                if (allOpenPositions[index2].tileType == (TILETYPE) LiveStats.zoomoments[index1].tileType_int && allOpenPositions[index2].RoamingEmployeeType == (EmployeeType) LiveStats.zoomoments[index1].roamingemployeetype)
                {
                  AnimalType animalType = allOpenPositions[index2].AddApplicantFromZooMoment(player);
                  string bodytext_ = allOpenPositions[index2].tileType == TILETYPE.Count || allOpenPositions[index2].tileType == TILETYPE.None ? EmployeesStats.GetJobTitle(allOpenPositions[index2].RoamingEmployeeType, animalType, IncludeSeniorityPrepend: false) : "at: " + TileData.GetTileStats(allOpenPositions[index2].tileType).Name;
                  Z_NotificationManager.RescrubJobApplicants = true;
                  AnimalRenderDescriptor _animalRenderDescriptor = new AnimalRenderDescriptor(animalType);
                  NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("New Applicant", bodytext_, _animalRenderDescriptor: _animalRenderDescriptor));
                }
              }
              break;
            default:
              throw new Exception("NOt handled");
          }
          if (!flag1)
            break;
          LiveStats.zoomoments.RemoveAt(index1);
          break;
        }
      }
    }

    public WaveInfo waveinfo
    {
      set => this._waveinfo = value;
      get => this._waveinfo;
    }

    public BlingDingCosts GetBlingdingcosts()
    {
      if (this.blindingcosts == null)
        this.blindingcosts = new BlingDingCosts();
      return this.blindingcosts;
    }

    public bool CheckCellBlockReminderTutorial(Player player) => player.prisonlayout.cellblockcontainer.prisonzones.Count == 1 && player.prisonlayout.cellblockcontainer.prisonzones[0].prisonercontainer.prisoners.Count > 3;

    public void TransferNewlyDeadsToGraveYards(Player player, ref Vector2Int LocationOfLastGrave)
    {
      for (int index = 0; index < this.NewlyDeads.Count; ++index)
        player.prisonlayout.AddNewlyDead(this.NewlyDeads[index], ref LocationOfLastGrave);
      this.NewlyDeads = new List<IntakePerson>();
    }

    public void ResetWaveForRetry()
    {
      if (GameFlags.IsArcadeMode)
        return;
      int num1 = 0;
      while (num1 < this.waveinfo.People.Count)
        ++num1;
      int num2 = 0;
      while (num2 < this.waveinfoFromPrison.People.Count)
        ++num2;
    }

    public int GetCost(TILETYPE tiletype, Player player, bool QuickGet) => player.livestats.GetBlingdingcosts().GetCost(tiletype, player.prisonlayout.cellblockcontainer.GetCountOfThisSpecialBuilding(tiletype));

    public void SetPrisonersForNextPlay(IntakeInfo _NewPrisoners)
    {
      if (this.waveinfo == null)
        this.waveinfo = new WaveInfo(_NewPrisoners);
      else
        this.waveinfo.MergeIntakeInfo(_NewPrisoners);
    }

    public void UpdateLiveStats(float DeltaTime, IAPUser iapuser, Player player)
    {
      if (TinyZoo.Game1.gamestate != GAMESTATE.GamePlay || PRISONPLANET_GamePlayManager.lockdownintro == null)
      {
        LiveStats.CycleTimer += DeltaTime;
        if ((double) LiveStats.CycleTimer > (double) GameFlags.CycleSpeed)
        {
          LiveStats.CycleTimer = 0.0f;
          ++LiveStats.EnemyMovementCycle;
          if (LiveStats.EnemyMovementCycle >= 24)
            LiveStats.EnemyMovementCycle = 0;
        }
      }
      if (Player.HasCompletedFileLoad_orMadeNewSave && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld)
      {
        this.SaveTime += DeltaTime;
        if ((double) this.SaveTime > 60.0)
          player.OldSaveThisPlayer();
      }
      if (this.SpeedUpSimulation > 0 && !(player.Stats.adxx == "p") && (!GameFlags.IsConsoleVersion && !player.Stats.TimeTravelIsActiveFromAdvert()))
        this.SpeedUpSimulation = 0;
      if (Player.HasCompletedFileLoad_orMadeNewSave)
        SpringIAPManager.Instance.Updatepurchasemanager(DeltaTime, iapuser);
      if (TinyZoo.Game1.gamestate != GAMESTATE.OverWorld && TinyZoo.Game1.gamestate != GAMESTATE.CollectionView && (TinyZoo.Game1.gamestate != GAMESTATE.GamePlay && TinyZoo.Game1.gamestate != GAMESTATE.ProfitLadder))
        return;
      player.Stats.research.UpdateConsoleResearchTimer(DeltaTime, player);
    }

    public void Gv(IAPTYPE P, Player player)
    {
      switch (P)
      {
        case IAPTYPE.DisableAdverts:
          player.Stats.adxx = "p";
          player.Stats.QR = "Fk";
          player.tracking.UnblockedAdverts(player);
          break;
        case IAPTYPE.BuyVortex:
          if (!(player.Stats.vx != "b"))
            break;
          player.Stats.vx = "b";
          player.Stats.V = "Ct";
          player.tracking.PurchasedVortex(player);
          player.Stats.research.JustDidIAP();
          break;
        case IAPTYPE.BuyFlower:
          if (!(player.Stats.HlfLfe != "g"))
            break;
          player.Stats.HlfLfe = "g";
          player.Stats.HlfLfecse = "n";
          player.tracking.PurchasedFlower(player);
          break;
      }
    }

    public void AddFreshMeatToCorpseQueue(IntakePerson person) => this.NewlyDeads.Add(person);

    public void AddNewAnimalsToEnclosure(Player player, int PrisonID)
    {
      float AnimalValue1 = 0.0f;
      PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(PrisonID);
      if (player.livestats.AnimalsJustTraded != null && player.livestats.AnimalsJustTraded.CameFromHere != CityName.Count)
      {
        player.animalsonorder.AddNewOrder(player.livestats.AnimalsJustTraded, PrisonID, player);
        player.livestats.AnimalsJustTraded = (WaveInfo) null;
      }
      else
      {
        ParkRating.NeedsRecalculating = true;
        int HasHybrid;
        double valueAndPopularity1 = (double) ParkPopularity.CalculateAnimalValueAndPopularity(player, out AnimalValue1, out HasHybrid);
        if (player.livestats.AnimalsJustTraded != null && player.livestats.AnimalsJustTraded.animalinfo != null && player.livestats.AnimalsJustTraded.animalinfo.Count > 0)
        {
          for (int index = 0; index < player.livestats.AnimalsJustTraded.animalinfo.Count; ++index)
          {
            if (!player.livestats.AnimalsJustTraded.animalinfo[index].DONOTRESETDATA_MovedAnimal)
              throw new Exception("THIS MEANS THIS WAS NOT A TRANSFER.....YOU WERE ASSUMING THAT THESE WERE ALL TRANSFERS");
            thisCellBlock.prisonercontainer.prisoners.Add(player.livestats.AnimalsJustTraded.animalinfo[index]);
            thisCellBlock.prisonercontainer.FoodForAnimals.AddAnimal(player.livestats.AnimalsJustTraded.animalinfo[index].intakeperson.animaltype);
            thisCellBlock.prisonercontainer.SetUpTempAnimals(thisCellBlock.Cell_UID, thisCellBlock.CellBLOCKTYPE, player, true);
            player.prisonlayout.GetHungryAnaimals(player);
            Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
            Z_NotificationManager.RescrubWater = true;
            Z_NotificationManager.AddPenIDTorecountWater(thisCellBlock.Cell_UID);
          }
        }
        else if (player.livestats.AnimalsJustTraded != null)
        {
          for (int index = 0; index < player.livestats.AnimalsJustTraded.People.Count; ++index)
          {
            IntakePerson intakePerson = player.livestats.AnimalsJustTraded.People[index];
            if (player.livestats.AnimalsJustTraded.enemyrenderersFromChopper != null)
            {
              Player.financialrecords.AnimalAddedToZoo();
              thisCellBlock.prisonercontainer.prisoners.Add(new PrisonerInfo(player.livestats.AnimalsJustTraded.People[index], false, thisCellBlock.GetRandomLocationInCellBlock(), thisCellBlock.CellBLOCKTYPE, player.livestats.AnimalsJustTraded.enemyrenderersFromChopper[index].vLocation));
            }
            else
            {
              Player.financialrecords.AnimalAddedToZoo();
              thisCellBlock.prisonercontainer.prisoners.Add(new PrisonerInfo(player.livestats.AnimalsJustTraded.People[index], false, thisCellBlock.GetRandomLocationInCellBlock(), thisCellBlock.CellBLOCKTYPE));
            }
            thisCellBlock.prisonercontainer.prisoners[thisCellBlock.prisonercontainer.prisoners.Count - 1].SetAgeOnGet(thisCellBlock.prisonercontainer.prisoners[thisCellBlock.prisonercontainer.prisoners.Count - 1].DaysAsABaby + 1);
            thisCellBlock.prisonercontainer.FoodForAnimals.AddAnimal(player.livestats.AnimalsJustTraded.People[index].animaltype);
            Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
            if (player.livestats.AnimalsJustTraded.People[index].HeadType == AnimalType.None)
              player.Stats.AnimalBredOrFound(intakePerson.animaltype, intakePerson.CLIndex, intakePerson.IsAGirl);
            else
              player.unlocks.TryUnlockNewHybrid(intakePerson.animaltype, intakePerson.HeadType, intakePerson.CLIndex, intakePerson.HeadVariant);
          }
        }
        if (player.livestats.AnimalsJustTraded != null && player.livestats.AnimalsJustTraded.OrderUID > -1)
          player.animalsonorder.ReomveOrderAfterChopprDropOff(player.livestats.AnimalsJustTraded.OrderUID, player);
        thisCellBlock.prisonercontainer.ThisWasTehCellBlockThatChanged = true;
        GameFlags.prisonersJustChangedInHoldingCell = true;
        GameFlags.CellBlockContentsChanged = true;
        player.livestats.AnimalsJustTraded = (WaveInfo) null;
        float AnimalValue2 = 0.0f;
        double valueAndPopularity2 = (double) ParkPopularity.CalculateAnimalValueAndPopularity(player, out AnimalValue2, out HasHybrid);
        NotificationBubbleManager.QuickAddNotification(AnimalValue1, AnimalValue2, BubbleMainType.Animals);
        if (!player.Stats.TutorialsComplete[8])
        {
          player.Stats.TutorialsComplete[8] = true;
          CheckRandomBreedsOnStartDay.ForceTutorialBreed(player, thisCellBlock.prisonercontainer);
        }
        QuestScrubber.ScrubOnRecievingAnimal(player);
      }
    }

    public void AddExtraEnemiesForNextPlay(Player player, int PrisonID)
    {
      PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(PrisonID);
      IntakeInfo prisoners = new IntakeInfo();
      for (int index = 0; index < thisCellBlock.prisonercontainer.prisoners.Count; ++index)
      {
        if (!thisCellBlock.prisonercontainer.prisoners[index].IsDead)
          prisoners.People.Add(thisCellBlock.prisonercontainer.prisoners[index].intakeperson);
      }
      this.waveinfoFromPrison = new WaveInfo(prisoners);
    }

    public void ReturnedToOverWorldAfterGame()
    {
      this.waveinfoFromPrison = (WaveInfo) null;
      this.waveinfo = (WaveInfo) null;
    }
  }
}
