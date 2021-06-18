// Decompiled with JetBrains decompiler
// Type: TinyZoo.Player
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.FileInOut;
using SpringIAP;
using SpringIAP.IAP_User;
using SpringSocial;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Animals;
using TinyZoo.PlayerDir.BlackMarket;
using TinyZoo.PlayerDir.Breeding;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.PlayerDir.Carbon;
using TinyZoo.PlayerDir.Commodities;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.FinancialHistory;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Processing;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.PlayerDir.ZooQuest;
using TinyZoo.Utils;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_Player;

namespace TinyZoo
{
  internal class Player
  {
    public SEngine.Player.Player player;
    public int PlayerIndex;
    private bool HasKeyboard;
    public Farms farms;
    public Unlocks unlocks;
    public InputMap inputmap;
    internal static bool HasCompletedFileLoad_orMadeNewSave;
    public PrisonLayout prisonlayout;
    public WorldHistory worldhistory;
    public Unions unions;
    public Employees employees;
    internal static CarbonDrops carbon;
    public Inventory inventory;
    public Z_Research z_research;
    public Intakes intakes;
    public SocialManagerMain socialmanager;
    public LiveStats livestats;
    public ShopStatus shopstatus;
    public PlayerStats Stats;
    public IAPUser iapuser;
    public Playerbehaviour playerbehaviour;
    public PlayerTracking tracking;
    public CurrentZQuests zquests;
    public Breeds breeds;
    public CRISPR_Breed crisprBreeds;
    public TinyZoo.PlayerDir.StoreRooms.StoreRooms storerooms;
    internal static FanancialRecords financialrecords;
    public AnimalCollection animalcollection;
    public AnimalProcessing animalProcessing;
    public BusInfo busroutes;
    public HeroQuestProgress heroquestprogress;
    public AnimalsOnOrder animalsonorder;
    public Warehouse warehouse;
    internal static Garbage garbage;
    public BlackMarketStats blackmarketstats;
    public AnimalQuarantine animalquarantine;
    public AnimalIncineration animalincineration;
    internal static CriticalChoices criticalchoices;
    public Sponsorships sponsorships;
    internal static CurrentActiveResearchBonuses currentActiveResearchBonuses;
    public ShelterStocks shelterstocks;

    internal static string FileNameForSave() => GameFlags.IsConsoleVersion ? "ZSV_C" : "ZSV";

    public Player(int _PlayerIndex)
    {
      this.sponsorships = new Sponsorships();
      this.warehouse = new Warehouse();
      this.farms = new Farms();
      this.animalsonorder = new AnimalsOnOrder();
      this.animalProcessing = new AnimalProcessing();
      this.unions = new Unions();
      this.z_research = new Z_Research();
      this.worldhistory = new WorldHistory();
      this.shopstatus = new ShopStatus();
      TinyZoo.Player.financialrecords = new FanancialRecords();
      this.breeds = new Breeds();
      this.crisprBreeds = new CRISPR_Breed();
      this.zquests = new CurrentZQuests();
      this.Stats = new PlayerStats();
      this.inventory = new Inventory();
      this.player = new SEngine.Player.Player(_PlayerIndex);
      this.player.touchinput.ReleaseTapTime = 0.5f;
      this.player.touchinput.DragTolleranceOnReleaseTap = 25f;
      this.PlayerIndex = _PlayerIndex;
      this.intakes = new Intakes(this);
      TinyZoo.Player.garbage = new Garbage();
      if (this.PlayerIndex == 0)
      {
        this.HasKeyboard = true;
        this.inputmap = new InputMap();
      }
      this.unlocks = new Unlocks();
      this.storerooms = new TinyZoo.PlayerDir.StoreRooms.StoreRooms();
      this.livestats = new LiveStats(this);
      TinyZoo.Player.criticalchoices = new CriticalChoices();
      this.prisonlayout = new PrisonLayout(this.livestats.consumptionstatus, this);
      this.socialmanager = new SocialManagerMain(true, MainVariables.ThisGame, false, MainVariables.CloudSaveFile);
      this.iapuser = new IAPUser();
      this.playerbehaviour = new Playerbehaviour();
      this.tracking = new PlayerTracking();
      this.employees = new Employees();
      this.animalcollection = new AnimalCollection();
      this.busroutes = new BusInfo();
      this.heroquestprogress = new HeroQuestProgress();
      this.blackmarketstats = new BlackMarketStats();
      this.animalquarantine = new AnimalQuarantine();
      this.animalincineration = new AnimalIncineration();
      TinyZoo.Player.currentActiveResearchBonuses = new CurrentActiveResearchBonuses();
      if (Z_DebugFlags.ZooTutoriallsDisabled)
        FeatureFlags.BlockAllUI = false;
      this.shelterstocks = new ShelterStocks(this);
      TinyZoo.Player.carbon = new CarbonDrops();
    }

    public void UpdateGamePlayer(float DeltaTime)
    {
      this.player.UpdatePlayer(DeltaTime);
      if ((double) this.player.touchinput.MultiTouchTouchLocations[0].X >= 0.0)
        GameFlags.IsUsingController = false;
      this.inputmap.UpdateInputMap(this.player.genericinput, this.player, DeltaTime, this.Stats.userkeybindings);
      this.socialmanager.UpdateSocialManager();
      if (Game1.gamestate == GAMESTATE.OverWorld)
        this.Stats.research.UpdateResearcher(this);
      this.livestats.UpdateLiveStats(DeltaTime, this.iapuser, this);
      if (SpringIAPManager.Instance.GetWaitingToSave() | this.socialmanager.GetUser().GetThisUserWantsToSave() && TinyZoo.Player.HasCompletedFileLoad_orMadeNewSave)
      {
        this.OldSaveThisPlayer();
        SpringIAPManager.Instance.SetWaitingToSave(false);
        this.socialmanager.GetUser().ThisUserWantsToSave = false;
      }
      int num = TinyZoo.Player.HasCompletedFileLoad_orMadeNewSave ? 1 : 0;
      if ((double) Game1.screenfade.fAlpha == 0.0)
        return;
      this.inputmap.ClearAllInput(this);
    }

    public void UpdateAfterbaeeDraw(GraphicsDevice graphicsDevice) => this.socialmanager.UpdateAfterBaseDraw(graphicsDevice);

    public void LoadThisPlayerFromCloudSave(string SaveString)
    {
      Reader cloudreader = new Reader();
      cloudreader.FromString(SaveString);
      this.LoadThisPlayer(cloudreader);
    }

    public List<PrisonerInfo> GetAllAnimalsFiagnosedWithThisDisease(int SicknessUID)
    {
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>();
      for (int index = 0; index < this.prisonlayout.AnimalsNotInPens.Count; ++index)
      {
        if (this.prisonlayout.AnimalsNotInPens[index].GetIsSick() && this.prisonlayout.AnimalsNotInPens[index].SicknessUID == SicknessUID)
          prisonerInfoList.Add(this.prisonlayout.AnimalsNotInPens[index]);
      }
      for (int index1 = 0; index1 < this.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (this.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].GetIsSick() && this.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].SicknessUID == SicknessUID)
            prisonerInfoList.Add(this.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2]);
        }
      }
      return prisonerInfoList;
    }

    public bool LoadThisPlayer(Reader cloudreader = null, bool _DelayUntilNextFrame = false)
    {
      if (Z_DebugFlags.ForceLoadString.Length > 0)
      {
        cloudreader = new Reader();
        cloudreader.FromString(Z_DebugFlags.ForceLoadString);
        CloudSaveUtil.JustLoadedFromCloud = true;
        Z_DebugFlags.ForceLoadString = "";
        _DelayUntilNextFrame = false;
      }
      if (_DelayUntilNextFrame && ThreadedSaveStatus.GetIsThreadedSave())
      {
        Z_GameFlags.LoadNextFrame = true;
        return true;
      }
      TinyZoo.Player.HasCompletedFileLoad_orMadeNewSave = true;
      bool flag = false;
      ParkRating.NeedsRecalculating = true;
      if (DebugFlags.LoadGame)
      {
        if (Reader.DoesFileExist(TinyZoo.Player.FileNameForSave()))
        {
          Z_GameFlags.DidLoadSave = true;
          flag = true;
        }
        if (cloudreader != null)
        {
          this.LoadPlayer(cloudreader);
        }
        else
        {
          Reader reader = new Reader(TinyZoo.Player.FileNameForSave(), FlagSettings.SaveIsEncrypted);
          if (!ThreadedSaveStatus.GetIsThreadedSave())
            this.LoadPlayer(reader);
        }
      }
      return flag;
    }

    private void LoadPlayer(Reader reader)
    {
      Logger.Print("PLAYER NOW READING VARIABLES FROM FILE!!!");
      Z_GameFlags.JustloadedGame = true;
      PlayerSave.LoadPlayer(reader, this);
    }

    public string OldSaveThisPlayer(bool GetAsString = false, bool DelayUntilNextFrame = true, bool IsEndOfDay = false) => "THIS ISNT ACTUALLY A SAVE";

    public string SaveThisPlayer(bool GetAsString = false, bool DelayUntilNextFrame = true, bool IsEndOfDay = false)
    {
      if (DebugFlags.SaveGame && (GetAsString || IsEndOfDay || !Z_DebugFlags.SaveAtEndOfDayOnly))
      {
        if (!GetAsString & DelayUntilNextFrame && ThreadedSaveStatus.GetIsThreadedSave())
        {
          Z_GameFlags.SaveNextFrame = true;
          return "";
        }
        TinyZoo.Player.HasCompletedFileLoad_orMadeNewSave = true;
        AchievementScrubber.ScrubAllAchievements(this);
        Logger.Print("PLAYER CALLED SAVE!!!");
        this.livestats.SaveTime = 0.0f;
        Writer writer = new Writer(TinyZoo.Player.FileNameForSave(), FlagSettings.SaveIsEncrypted);
        PlayerSave.SavePlayer(writer, this);
        if (GetAsString)
          return writer.PrepareAndGetDataToWrite();
        writer.Close();
        Logger.Print("PLAYER SAVED THE GAME!!!");
      }
      return "";
    }
  }
}
