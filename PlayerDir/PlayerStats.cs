// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.PlayerStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Localization;
using SEngine.Objects;
using SEngine.Time;
using SEngine.Utils;
using Spring.UI;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Time;
using TinyZoo.Tile_Data;
using TinyZoo.TimeSystem;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.LoadValues;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_Diseases;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TRC_Helper;

namespace TinyZoo.PlayerDir
{
  internal class PlayerStats
  {
    public GFX_Resolution gfxresolution = GFX_Resolution.FullScreen;
    public UserKeyBindings userkeybindings;
    public bool HasPickedCharacter;
    internal static int Z_AnimalUID = 0;
    internal static int LandSize;
    public int ZooKeeperAvatarIndex;
    public GameTimeManager gametimemanager;
    private NumberObfiscatorV1 Loan;
    private Longfiscator_V1 cashlong;
    public long Reputation;
    public Researcher research;
    private long DaysPassed;
    private Longfiscator_V1 CurrentCashCap_Depricated;
    public bool[] TutorialsComplete;
    public bool[] AvailableNeedsCategories;
    private int[] AliensCaptured;
    public VariantsFound variantsfound;
    public bool NeedsChanged;
    public bool HitCashCap;
    public DateTimeManager datetimemanager;
    public string adxx = "0";
    public string QR = "0";
    public string vx = "0";
    public string V = "0";
    public string HlfLfe = "0";
    public string HlfLfecse = "0";
    public TimeSpan LastResume;
    internal static long Z_DaysFinished;
    public PayoutTimer payoutimer;
    private DateTime LastResearchAdvert;
    private DateTime timetravelEnd;
    public int[] ArcadeProgress;
    public bool TDDLink;
    internal static Language language;
    internal static bool LanguageChanged_RemakeGameLogo = false;
    public WardenRank bestrank;
    public bool HasRatedGame;
    public Bounty bounty;
    public CurrentEvent currentEvent;
    public bool NeedToDoPixFix;
    public string OldPixID = "x";
    public static bool WillPlayIntro = true;
    public static bool WillCacheAdverts = true;
    internal static float UXMult = -1f;
    private int Z_TicketCost;
    private bool Z_TicketIsFree;
    public int Z_WeekEndsShown;
    internal static int LastSetNursingOption = 1;
    internal static int LastSetProductionTargetOption = 1;
    internal static bool LastSetIsArticificalInsemination = false;
    public bool SunHasRisen;
    public int BabiesAborted;
    public int BabiesMovedLate;
    public int BabiesMovedEarly;
    internal static bool[,] unblockedSectors;
    internal static int UnlockedSectorCount;
    public List<Disease> ActiveDiseases;
    public List<Disease> InnacitveDiseases;
    private bool WasServerTime;
    public int CENTS;

    internal static int RecountUnblockedSectors()
    {
      PlayerStats.UnlockedSectorCount = 0;
      for (int index1 = 0; index1 < PlayerStats.unblockedSectors.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < PlayerStats.unblockedSectors.GetLength(1); ++index2)
        {
          if (PlayerStats.unblockedSectors[index1, index2])
            ++PlayerStats.UnlockedSectorCount;
        }
      }
      return PlayerStats.UnlockedSectorCount;
    }

    internal static int GetTotalLandUnlocked() => PlayerStats.UnlockedSectorCount;

    internal static void SetNewMaxToiletUse(int NewMax, Player player)
    {
      LiveStats.ToiletMaxCapacity = NewMax;
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
        player.shopstatus.shopentries[index].ScrubMaxCapacities();
    }

    public PlayerStats()
    {
      this.Z_TicketCost = 5;
      this.variantsfound = new VariantsFound();
      this.bounty = new Bounty();
      this.timetravelEnd = new DateTime();
      this.LastResearchAdvert = new DateTime();
      this.payoutimer = new PayoutTimer();
      this.adxx = "k";
      this.research = new Researcher();
      this.DaysPassed = 0L;
      this.CurrentCashCap_Depricated = new Longfiscator_V1();
      this.CurrentCashCap_Depricated.ForceSetNewValue(1000L);
      this.Loan = new NumberObfiscatorV1();
      this.Loan.ForceSetNewValue(0);
      this.cashlong = new Longfiscator_V1();
      this.cashlong.ForceSetNewValue((long) BVal.OverrideStartCash);
      this.gametimemanager = new GameTimeManager();
      this.TutorialsComplete = new bool[32];
      this.AvailableNeedsCategories = new bool[10];
      this.AvailableNeedsCategories[0] = true;
      this.AvailableNeedsCategories[1] = true;
      this.datetimemanager = new DateTimeManager();
      this.datetimemanager.UpdateDateTimeToServerNow();
      this.AliensCaptured = new int[70];
      this.ArcadeProgressCreate();
      PlayerStats.language = SystemLanguage.GetSystemLanguage();
      this.currentEvent = new CurrentEvent();
      this.ActiveDiseases = new List<Disease>();
      this.InnacitveDiseases = new List<Disease>();
      PlayerStats.unblockedSectors = new bool[TileMath.GetOverWorldMapSize_XDefault() / TileMath.SectorSize, TileMath.GetOverWorldMapSize_YSize() / TileMath.SectorSize];
      this.userkeybindings = new UserKeyBindings();
    }

    public int GetTicketCost() => this.Z_TicketCost;

    public void SetTicketCost(int _TicketCost) => this.Z_TicketCost = _TicketCost;

    public bool GetTicketIsFree() => this.Z_TicketIsFree;

    public bool HasCureForThisDisease(int UID)
    {
      for (int index = 0; index < this.ActiveDiseases.Count; ++index)
      {
        if (this.ActiveDiseases[index].IsResearched && this.ActiveDiseases[index].UID == UID)
          return true;
      }
      for (int index = 0; index < this.InnacitveDiseases.Count; ++index)
      {
        if (this.InnacitveDiseases[index].UID == UID && this.InnacitveDiseases[index].IsResearched)
          return true;
      }
      return false;
    }

    public void SetTicketFree(bool isFree) => this.Z_TicketIsFree = isFree;

    public void AddNewDisease(Disease disease) => this.ActiveDiseases.Add(disease);

    public void SpendCash_AllowLoan(
      int MoneyToSpend,
      SpendingCashOnThis spendingonthis,
      Player player,
      bool AddToPreviousDay = false)
    {
      int SpendThis = (int) this.cashlong.SmartGetValue(true);
      if (SpendThis >= MoneyToSpend)
      {
        this.SpendCash(MoneyToSpend, spendingonthis, player, AddToPreviousDay);
      }
      else
      {
        if (SpendThis > 0)
        {
          this.SpendCash(SpendThis, spendingonthis, player, AddToPreviousDay);
          MoneyToSpend -= SpendThis;
        }
        Player.financialrecords.PlayerTookLoan(MoneyToSpend, AddToPreviousDay);
        this.Loan.SmartAddValue_MinimumThreshold(false, MoneyToSpend, 0);
        if (!AddToPreviousDay)
          return;
        Player.financialrecords.SetLoanOnClosing(player.Stats.GetCurrentLoan(), true);
      }
    }

    public int PayOffLoanAtEndOfWeek(Player player)
    {
      if (this.Loan.SmartGetValue(true) > 0)
      {
        int num = (int) this.cashlong.SmartGetValue(true);
        if (num > 0)
        {
          int SpendThis = num / 10;
          this.SpendCash(SpendThis, SpendingCashOnThis.PayingOffLoan, player, true);
          this.Loan.SmartAddValue_MinimumThreshold(false, -SpendThis, 0);
          return SpendThis;
        }
      }
      return 0;
    }

    public int GetTotalOfThisVariantFound(AnimalType animal, int VarientIndex) => this.variantsfound.GetTotalOfThisVariantFound(animal, VarientIndex);

    public int GetTotalVaiantsFound(AnimalType animal = AnimalType.None) => this.variantsfound.GetTotalVaiantsFound(animal);

    public bool IsThisGenomeMapped(AnimalType anaimaltype) => this.variantsfound.IsThisGenomeMapped(anaimaltype);

    public int GetTotalSpeciesFound(out int totalSpeciesInGame)
    {
      int num = 0;
      totalSpeciesInGame = 56;
      for (int index = 0; index < 56; ++index)
      {
        if (this.GetAliensCaptured((AnimalType) index) > 0)
          ++num;
      }
      return num;
    }

    public bool AnimalBredOrFound(AnimalType animal, int Skin, bool IsAGirl)
    {
      ++this.AliensCaptured[(int) animal];
      this.research.AddAnimalToResearchComplete(animal, Skin, 1, IsAGirl);
      bool flag = this.variantsfound.AnimalBredOrFound(animal, Skin);
      if (flag)
      {
        int totalVaiantsFound = this.variantsfound.GetTotalVaiantsFound(animal);
        if (totalVaiantsFound == 10)
        {
          OverWorldManager.zoopopupHolder.CreateZooPopUps(animal, POPUPSTATE.NewThing);
        }
        else
        {
          AnimalRenderDescriptor _animalRenderDescriptor = new AnimalRenderDescriptor(animal, Skin);
          NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("New Variant!", AnimalData.GetAnimalName(animal) + ": " + (object) totalVaiantsFound + "/10", _animalRenderDescriptor: _animalRenderDescriptor, _frametype: NotificationBubbleFrameType.Colored_ForVariants));
        }
      }
      return flag;
    }

    public void GetFirstAndLastVariantFoundDates(
      AnimalType animalType,
      out int firstVariantIndex,
      out int firstVariant_dayFound,
      out int lastVariantIndex,
      out int lastVariant_dayFound)
    {
      firstVariantIndex = -1;
      firstVariant_dayFound = -1;
      lastVariantIndex = -1;
      lastVariant_dayFound = -1;
      if (this.GetTotalVaiantsFound(animalType) <= 0)
        return;
      for (int Variant = 0; Variant < 10; ++Variant)
      {
        int dayDiscovered = this.variantsfound.GetDayDiscovered(animalType, Variant);
        if (dayDiscovered > -1)
        {
          if (firstVariant_dayFound == -1 || dayDiscovered < firstVariant_dayFound)
          {
            firstVariantIndex = Variant;
            firstVariant_dayFound = dayDiscovered;
          }
          if (dayDiscovered > lastVariant_dayFound)
          {
            lastVariantIndex = Variant;
            lastVariant_dayFound = dayDiscovered;
          }
        }
      }
    }

    public int GetAliensCaptured(AnimalType animal) => this.AliensCaptured[(int) animal];

    public bool HasPlayedResearchAdvertToday()
    {
      long ticks = DateTime.UtcNow.Ticks - this.LastResearchAdvert.Ticks;
      return ticks <= 0L || new TimeSpan(ticks).TotalHours < 4.0;
    }

    public Disease GetThisDisease(int UID)
    {
      for (int index = 0; index < this.ActiveDiseases.Count; ++index)
      {
        if (this.ActiveDiseases[index].UID == UID)
          return this.ActiveDiseases[index];
      }
      throw new Exception("No Disease found");
    }

    public bool CanGetExtraCash() => !this.bounty.bountyevent.HasBeenClaimed(this.datetimemanager);

    public bool CanBuildThis(TILETYPE building, Player player)
    {
      if (this.research.BuildingsResearched.Contains(building))
        return true;
      switch (building)
      {
        case TILETYPE.PinkTreeIAP:
        case TILETYPE.BlueTreeIAP:
        case TILETYPE.GoatIAP:
          if (this.ADisabled(true, player))
            return true;
          break;
        case TILETYPE.ResearchIAP:
        case TILETYPE.TrashCompactorIAP:
          if (this.Vortex())
            return true;
          break;
        case TILETYPE.FlowerIAP:
          if (this.GetFlower())
            return true;
          break;
      }
      return false;
    }

    public string GetTimeUntilAdvertAvaialbel()
    {
      long ticks1 = DateTime.UtcNow.Ticks;
      long ticks2 = this.LastResearchAdvert.AddHours(4.0).Ticks - ticks1;
      return ticks2 > 0L ? TimeUtils.GetTimeAsString(new DateTime(ticks2)) : " ";
    }

    public void ReduceFromAdvert(Player player)
    {
      this.LastResearchAdvert = DateTime.UtcNow;
      this.research.ReduceFromAdvert();
      player.OldSaveThisPlayer();
    }

    public bool TimeChangeEnabled(Player player) => this.ADisabled(true, player) || this.datetimemanager.GetDateTimeNow(out this.WasServerTime).Ticks < this.timetravelEnd.Ticks;

    public void EnableTimeTravel(Player player)
    {
      this.timetravelEnd = this.datetimemanager.GetDateTimeNow(out bool _);
      this.timetravelEnd = this.timetravelEnd.AddMinutes(10.0);
      if (player.livestats.SpeedUpSimulation != 0)
        return;
      player.livestats.SpeedUpSimulation = 1;
    }

    public bool TimeTravelIsActiveFromAdvert() => this.datetimemanager.GetDateTimeNow(out this.WasServerTime).Ticks < this.timetravelEnd.Ticks;

    public string TimeTravelTimeLeft() => TimeUtils.GetDifferenceInTimeAsString(this.timetravelEnd, this.datetimemanager.GetDateTimeNow(out this.WasServerTime));

    public int GetCurrentLoan(bool QuickGet = true) => this.Loan.SmartGetValue(QuickGet);

    public int GetCashHeld(bool QuickGet = true) => DebugFlags.HasEndlessMoney ? 1000000 : (int) this.cashlong.SmartGetValue(QuickGet);

    public int GetCashHeldAsCents(bool QuickGet = true) => DebugFlags.HasEndlessMoney ? 1000000 : (int) (this.cashlong.SmartGetValue(QuickGet) * 100L) + this.CENTS;

    public bool SpendCash(
      int SpendThis,
      SpendingCashOnThis spendingonthis,
      Player player,
      bool AddToPreviousDay = false)
    {
      if (DebugFlags.HasEndlessMoney)
        return true;
      if (this.cashlong.SmartGetValue(false) < (long) SpendThis)
        return false;
      Player.financialrecords.PlayerSpentMoney(spendingonthis, SpendThis, AddToPreviousDay);
      this.cashlong.SmartAddValue_MinimumThreshold(false, (long) -SpendThis, 0L);
      return true;
    }

    public void GiveReputation(int GiveThis, Player player) => this.Reputation += (long) GiveThis;

    public void GiveCash(int GiveThis, Player player, bool SkipCashAtMax = false, int CentsGiven = 0)
    {
      this.CENTS += CentsGiven;
      if (this.CENTS >= 100)
      {
        GiveThis += this.CENTS / 100;
        this.CENTS %= 100;
      }
      this.cashlong.SmartAddValue_MinimumThreshold(false, (long) GiveThis, 0L);
      LiveStats.MoneyWentUp = true;
    }

    public bool ADisabled(bool IsCheckingForGoat, Player player)
    {
      if (TinyZoo.GameFlags.IsConsoleVersion)
        return true;
      if (!IsCheckingForGoat)
      {
        int num = 5;
        if (player.playerbehaviour.GetTotalMinutesPlayed() < (double) num)
          return true;
      }
      return this.adxx == "p";
    }

    public bool Vortex() => !TinyZoo.GameFlags.IsConsoleVersion && this.vx == "b";

    public bool GetFlower() => TinyZoo.GameFlags.IsConsoleVersion || this.HlfLfe == "g";

    public void UpdateGameTime(float DeltaTIme, Player player)
    {
      int num = FeatureFlags.BlockTimer ? 1 : 0;
    }

    public long GetTotalDays() => this.gametimemanager.GetTotalDaysPassed();

    public string GetTimeOfDay() => this.gametimemanager.GetTimeOfDay();

    public string GetDisplayTime() => this.gametimemanager.GetDayDisplay();

    public Language GetLanguage() => PlayerStats.language;

    public void SetLanguage(Language _language, Player player)
    {
      if (PlayerStats.language != _language)
      {
        PlayerStats.language = _language;
        PlayerStats.LanguageChanged_RemakeGameLogo = true;
        TRC_Main.InitializeTRC_Helper(TinyZoo.Game1.WhitePixelRect, PlayerStats.language);
        SpringUI_Main.Instance.SetLanguage(PlayerStats.language);
        player.intakes.ResetForLanguage();
        player.prisonlayout.ResetForLanguage();
        CrimeData.CrimeNames = (List<string>) null;
      }
      TinyZoo.TextSettings.SwitchLanguage(PlayerStats.language);
    }

    public int GetNumberOfAvailableNeeds()
    {
      int num = 0;
      for (int index = 0; index < this.AvailableNeedsCategories.Length; ++index)
      {
        if (this.AvailableNeedsCategories[index])
          ++num;
      }
      return num;
    }

    public void SaveStats(Writer writer)
    {
      writer.WriteInt("i", LiveStats.AnimalOrderUID);
      writer.WriteInt("i", this.Z_TicketCost);
      writer.WriteBool("i", this.Z_TicketIsFree);
      writer.WriteInt("i", PlayerStats.Z_AnimalUID);
      writer.WriteBool("i", this.HasPickedCharacter);
      writer.WriteInt("i", this.ZooKeeperAvatarIndex);
      this.Loan.SaveObfiscator(writer);
      writer.WriteInt("i", this.TutorialsComplete.Length);
      for (int index = 0; index < this.TutorialsComplete.Length; ++index)
        writer.WriteBool("i", this.TutorialsComplete[index]);
      this.research.SaveResearcher(writer);
      this.cashlong.SaveObfiscator(writer);
      writer.WriteInt("i", (int) PlayerStats.language);
      writer.WriteInt("i", PlayerStats.unblockedSectors.GetLength(0));
      writer.WriteInt("i", PlayerStats.unblockedSectors.GetLength(1));
      for (int index1 = 0; index1 < PlayerStats.unblockedSectors.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < PlayerStats.unblockedSectors.GetLength(1); ++index2)
          writer.WriteBool("i", PlayerStats.unblockedSectors[index1, index2]);
      }
      writer.WriteInt("i", this.ActiveDiseases.Count);
      for (int index = 0; index < this.ActiveDiseases.Count; ++index)
        this.ActiveDiseases[index].SaveDisease(writer);
      writer.WriteInt("i", this.InnacitveDiseases.Count);
      for (int index = 0; index < this.InnacitveDiseases.Count; ++index)
        this.InnacitveDiseases[index].SaveDisease(writer);
      writer.WriteInt("i", this.AliensCaptured.Length);
      for (int index = 0; index < this.AliensCaptured.Length; ++index)
        writer.WriteInt("i", this.AliensCaptured[index]);
      writer.WriteInt("i", this.Z_WeekEndsShown);
      this.variantsfound.SaveVariantsfound(writer);
      writer.WriteInt("i", (int) this.gfxresolution);
    }

    public PlayerStats(Reader reader, int VersionForLoad)
    {
      this.CurrentCashCap_Depricated = new Longfiscator_V1();
      int num1 = (int) reader.ReadInt("i", ref LiveStats.AnimalOrderUID);
      int num2 = (int) reader.ReadInt("i", ref this.Z_TicketCost);
      int num3 = (int) reader.ReadBool("i", ref this.Z_TicketIsFree);
      int num4 = (int) reader.ReadInt("i", ref PlayerStats.Z_AnimalUID);
      int num5 = (int) reader.ReadBool("i", ref this.HasPickedCharacter);
      int num6 = (int) reader.ReadInt("i", ref this.ZooKeeperAvatarIndex);
      if (VersionForLoad <= 3)
        this.variantsfound = new VariantsFound(reader, VersionForLoad);
      this.Loan = new NumberObfiscatorV1();
      this.Loan.LoadObfiscatorValuesFromFile(reader);
      int _out1 = 0;
      int num7 = (int) reader.ReadInt("i", ref _out1);
      this.TutorialsComplete = new bool[32];
      for (int index = 0; index < _out1; ++index)
      {
        bool _out2 = false;
        int num8 = (int) reader.ReadBool("i", ref _out2);
        this.TutorialsComplete[index] = _out2;
      }
      this.datetimemanager = new DateTimeManager();
      this.research = new Researcher(reader);
      this.cashlong = new Longfiscator_V1(reader);
      int _out3 = 0;
      int num9 = (int) reader.ReadInt("i", ref _out3);
      PlayerStats.language = (Language) _out3;
      int _out4 = 0;
      int _out5 = 0;
      int num10 = (int) reader.ReadInt("i", ref _out4);
      int num11 = (int) reader.ReadInt("i", ref _out5);
      PlayerStats.unblockedSectors = new bool[_out4, _out5];
      for (int index1 = 0; index1 < _out4; ++index1)
      {
        for (int index2 = 0; index2 < _out5; ++index2)
        {
          int num8 = (int) reader.ReadBool("i", ref PlayerStats.unblockedSectors.Address(index1, index2));
        }
      }
      PlayerStats.RecountUnblockedSectors();
      this.ActiveDiseases = new List<Disease>();
      int num12 = (int) reader.ReadInt("i", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.ActiveDiseases.Add(new Disease(reader, VersionForLoad));
      this.InnacitveDiseases = new List<Disease>();
      int num13 = (int) reader.ReadInt("i", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.InnacitveDiseases.Add(new Disease(reader, VersionForLoad));
      this.AliensCaptured = new int[70];
      int num14 = (int) reader.ReadInt("i", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num8 = (int) reader.ReadInt("i", ref this.AliensCaptured[index]);
      }
      int num15 = (int) reader.ReadInt("i", ref this.Z_WeekEndsShown);
      this.variantsfound = VersionForLoad <= 2 ? new VariantsFound() : new VariantsFound(reader, VersionForLoad);
      if (VersionForLoad > 32)
      {
        int num8 = (int) reader.ReadInt("i", ref _out1);
        this.gfxresolution = (GFX_Resolution) _out1;
      }
      this.userkeybindings = new UserKeyBindings();
    }

    private void ArcadeProgressCreate()
    {
      this.ArcadeProgress = new int[50];
      for (int index = 0; index < this.ArcadeProgress.Length; ++index)
        this.ArcadeProgress[index] = -1;
      this.ArcadeProgress[0] = 0;
    }
  }
}
