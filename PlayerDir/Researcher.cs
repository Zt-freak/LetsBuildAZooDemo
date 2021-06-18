// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Researcher
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.Blance;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.Research;
using TinyZoo.PlayerDir.Time;
using TinyZoo.ProfitLadder.LevelSummary;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir
{
  internal class Researcher
  {
    public ResearchType reasearchingThis;
    private TimeToResult researchtimeleft;
    internal static bool IsCurrentlyResearching;
    internal static bool ResearchComplete;
    internal static bool AllResearchIsComplete;
    private NumberObfiscatorV1 Alienstotalresearchcomplete;
    private NumberObfiscatorV1 Buildingstotalresearchcomplete;
    private NumberObfiscatorV1 CellBlockstotalresearchcomplete;
    private List<AnimalType> AliensResearched;
    public List<TILETYPE> BuildingsResearched;
    public List<TILETYPE> CellBlocksReseacrhed;
    internal static bool[] BusTypesReseacred;
    private RealTime_TimeToResult console_researchtimeleft;
    private ChildrenBred childrenbred;

    public Researcher()
    {
      this.childrenbred = new ChildrenBred();
      this.Alienstotalresearchcomplete = new NumberObfiscatorV1();
      this.Buildingstotalresearchcomplete = new NumberObfiscatorV1();
      this.CellBlockstotalresearchcomplete = new NumberObfiscatorV1();
      this.AliensResearched = new List<AnimalType>();
      this.BuildingsResearched = new List<TILETYPE>();
      this.CellBlocksReseacrhed = new List<TILETYPE>();
      this.CellBlocksReseacrhed.Add(TILETYPE.GrassEnclosure);
      Researcher.BusTypesReseacred = new bool[4];
      Researcher.BusTypesReseacred[0] = true;
      Researcher.IsCurrentlyResearching = false;
      this.researchtimeleft = new TimeToResult(DateTime.UtcNow, DateTime.UtcNow, 0);
      this.console_researchtimeleft = new RealTime_TimeToResult((int) DateTime.UtcNow.Ticks);
      this.BuildingsResearched.Add(TILETYPE.StoreRoom);
      this.BuildingsResearched.Add(TILETYPE.WaterPumpStation);
      this.BuildingsResearched.Add(TILETYPE.WoodenToilet);
      this.BuildingsResearched.Add(TILETYPE.GreenDustbin);
      this.BuildingsResearched.Add(TILETYPE.WhiteDustbin);
      this.BuildingsResearched.Add(TILETYPE.RedShelter);
      this.BuildingsResearched.Add(TILETYPE.RedFlag);
      this.BuildingsResearched.Add(TILETYPE.SmallDesertRockDeco);
      this.BuildingsResearched.Add(TILETYPE.BearStandee);
      this.BuildingsResearched.Add(TILETYPE.OwlClock);
      this.BuildingsResearched.Add(TILETYPE.PlantPot);
      this.BuildingsResearched.Add(TILETYPE.FlowerPatch);
      this.BuildingsResearched.Add(TILETYPE.ZooTree);
      this.BuildingsResearched.Add(TILETYPE.RedFlower);
      this.BuildingsResearched.Add(TILETYPE.PurpleFlower);
      this.BuildingsResearched.Add(TILETYPE.PurpleFlowerPatch);
      this.BuildingsResearched.Add(TILETYPE.WhiteFlower);
      this.BuildingsResearched.Add(TILETYPE.GreenTree);
      this.BuildingsResearched.Add(TILETYPE.LongGrass);
      this.BuildingsResearched.Add(TILETYPE.YellowPlantPot);
      this.BuildingsResearched.Add(TILETYPE.Ferns);
      this.BuildingsResearched.Add(TILETYPE.SmallRock);
      this.BuildingsResearched.Add(TILETYPE.MediumRock);
      this.BuildingsResearched.Add(TILETYPE.SmallGiftShop);
      this.BuildingsResearched.Add(TILETYPE.LionHotDogShop);
      this.BuildingsResearched.Add(TILETYPE.DrinksVendingMachine);
      this.BuildingsResearched.Add(TILETYPE.SnacksVendingMachine);
      this.BuildingsResearched.Add(TILETYPE.RoundFountain);
      this.BuildingsResearched.Add(TILETYPE.ThickSignboard);
      this.BuildingsResearched.Add(TILETYPE.ZooMap);
      this.BuildingsResearched.Add(TILETYPE.Lamppost);
      this.BuildingsResearched.Add(TILETYPE.BrownBench);
      this.BuildingsResearched.Add(TILETYPE.WhiteBench);
      this.BuildingsResearched.Add(TILETYPE.Floor_Dirt);
      this.BuildingsResearched.Add(TILETYPE.Floor_GreenGrass);
      this.BuildingsResearched.Add(TILETYPE.Floor_WoodenBoards);
      this.BuildingsResearched.Add(TILETYPE.Floor_GreyBricks);
      this.BuildingsResearched.Add(TILETYPE.Floor_MetalDecoTile);
      this.BuildingsResearched.Add(TILETYPE.WaterTrough_Metal_Single);
      this.BuildingsResearched.Add(TILETYPE.WaterTrough_Wooden_Single);
      this.BuildingsResearched.Add(TILETYPE.WaterTrough_TreeTrunk);
      if (!Z_DebugFlags.UnlockAllBuildingsHack)
        return;
      this.DebugUnlockAllResearch();
    }

    public void CancelResearch() => Researcher.IsCurrentlyResearching = false;

    public void DebugUnlockAllResearch()
    {
    }

    public bool AddAnimalToResearchComplete(
      AnimalType animaltype,
      int Variant = -1,
      int Total = -1,
      bool IsAGirl = false)
    {
      return false;
    }

    public AnimalType GetResearchedAnimalByIndex(int Index) => throw new Exception("SHIT");

    public bool HasThisAnimalBeenResearched(AnimalType animaltype, int Variant = -1, bool IsAGirl = false) => throw new Exception("SHIT");

    public int AnimalsResearchedLength() => throw new Exception("SHIT");

    public void ReduceFromAdvert() => this.researchtimeleft.ForceReduceTimeRemaining(new TimeSpan(2, 0, 0));

    public int GetCurrentScientists() => this.researchtimeleft.MuliplierSpeedPublic;

    public bool AllComplete()
    {
      Researcher.AllResearchIsComplete = this.IsComplete(ResearchType.Alien) && this.IsComplete(ResearchType.Building) && this.IsComplete(ResearchType.CellType);
      return Researcher.AllResearchIsComplete;
    }

    public void JustDidIAP()
    {
      if (!Researcher.IsCurrentlyResearching)
        return;
      this.researchtimeleft.Multiply(0.75M);
    }

    public void AddedOrRemovedScientist(Player player)
    {
      if (!Researcher.IsCurrentlyResearching)
        return;
      int totalResearch = player.prisonlayout.GetTotalResearch();
      if (!this.researchtimeleft.IsComplete(player.Stats.datetimemanager, out bool _, false) && this.researchtimeleft.GetMultiplier() != totalResearch)
        this.researchtimeleft.AddorRemoveUnit(player.Stats.datetimemanager, totalResearch - this.researchtimeleft.GetMultiplier());
      player.OldSaveThisPlayer();
    }

    public bool IsAlienUnlocked(AnimalType enemy) => this.AliensResearched.Contains(enemy);

    public void beginResearch(ResearchType reasearch, Player player)
    {
      Researcher.IsCurrentlyResearching = true;
      this.reasearchingThis = reasearch;
      int TimeInMinutes = this.GetGetNextResearchTime(reasearch);
      bool WasServerTime;
      DateTime _StartTime = player.Stats.datetimemanager.GetDateTimeNow(out WasServerTime);
      DateTime dateTime = new DateTime();
      if (player.Stats.Vortex())
        TimeInMinutes = (int) Math.Ceiling((double) TimeInMinutes * 0.75);
      DateTime _LengthOfTimeToObjective = dateTime.AddMinutes((double) TimeInMinutes);
      if (!WasServerTime)
      {
        bool TimeHasBeenSet;
        if (_StartTime < player.Stats.datetimemanager.GetLastServerTime(out TimeHasBeenSet))
          _StartTime = player.Stats.datetimemanager.GetLastServerTime(out TimeHasBeenSet).AddMinutes(60.0);
        if (!TimeHasBeenSet)
          _StartTime = DateTime.UtcNow;
      }
      if (GameFlags.IsConsoleVersion)
        this.console_researchtimeleft = new RealTime_TimeToResult(TimeInMinutes);
      else
        this.researchtimeleft = new TimeToResult(_StartTime, _LengthOfTimeToObjective, player.prisonlayout.GetTotalResearch());
      player.OldSaveThisPlayer();
    }

    public int GetGetNextResearchTime(ResearchType reasearch)
    {
      switch (reasearch)
      {
        case ResearchType.Building:
          return ResearchData.GetReaserchTimeBuildings(this.Buildingstotalresearchcomplete.SmartGetValue(false, 50));
        case ResearchType.Alien:
          return ResearchData.GetReaserchTimeAliens(this.Alienstotalresearchcomplete.SmartGetValue(false, 50));
        case ResearchType.CellType:
          return ResearchData.GetReaserchTimeCellBlocks(this.CellBlockstotalresearchcomplete.SmartGetValue(false, 50));
        default:
          return 0;
      }
    }

    public string GetGetNextResearchTimeDisplay(ResearchType reasearch, int TotalScienceStations)
    {
      float num1 = (float) this.GetGetNextResearchTime(reasearch) / (float) TotalScienceStations;
      if (GameFlags.IsConsoleVersion)
        num1 *= 0.25f;
      if ((double) num1 < 10.0)
      {
        int num2 = (int) num1;
        int num3 = (int) (((double) num1 - (double) num2) * 60.0);
        if (num2 <= 0)
          return num3.ToString() + " s";
        return num2.ToString() + " min " + (object) num3 + " s";
      }
      if ((double) num1 < 60.0)
        return ((int) num1).ToString() + "min";
      return ((int) num1 / 60).ToString() + "h " + (object) ((int) num1 % 60) + "min";
    }

    public float GetPercentComplete(Player player, out string TimeString)
    {
      float dateTimePercent;
      if (GameFlags.IsConsoleVersion)
      {
        dateTimePercent = this.console_researchtimeleft.GetDateTimePercent(true, this.GetGetNextResearchTime(this.reasearchingThis), player.prisonlayout.GetTotalResearch());
        TimeString = this.console_researchtimeleft.TimeLeftString;
      }
      else
      {
        dateTimePercent = this.researchtimeleft.GetDateTimePercent(player.Stats.datetimemanager, true);
        TimeString = this.researchtimeleft.TimeLeftString;
      }
      return dateTimePercent;
    }

    public static bool IsHighEnoughRankToResearchNextAien(Player player, out int TargetRank)
    {
      WardenRank currentRank = ProfitLadderData.GetCurrentRank(player, out float _, out float _, out int _);
      TargetRank = (int) (currentRank + 1);
      if (currentRank + 1 >= WardenRank.Count)
        return true;
      RandkInfo rankData = ProfitLadderData.GetRankData(currentRank + 1);
      return player.Stats.research.AliensResearched.Count < rankData.ResearchAlienLimit;
    }

    public bool IsComplete(ResearchType reasearch)
    {
      switch (reasearch)
      {
        case ResearchType.Building:
          return this.BuildingsResearched.Count == ResearchData.GetBuildingTypesInOrder().Count;
        case ResearchType.Alien:
          return this.AliensResearched.Count == ResearchData.GetAliensReseachedInOrder().Count + 1;
        case ResearchType.CellType:
          return this.CellBlocksReseacrhed.Count - 1 >= (this.AliensResearched.Count - 1) / 5;
        default:
          return false;
      }
    }

    public bool IsCellBlockResearchBlocked() => this.IsComplete(ResearchType.CellType) && this.CellBlocksReseacrhed.Count < ResearchData.GetCellTypeInOrder().Count + 1;

    public void Vallidate()
    {
    }

    public void UpdateConsoleResearchTimer(float DeltaTime, Player player)
    {
      if (this.console_researchtimeleft == null)
        return;
      this.console_researchtimeleft.RealTimeUpdateOnConsole(DeltaTime, player.prisonlayout.GetTotalResearch());
    }

    public void UpdateResearcher(Player player)
    {
      if (this.console_researchtimeleft == null)
        return;
      if (GameFlags.IsConsoleVersion)
      {
        if (Researcher.IsCurrentlyResearching)
          Researcher.ResearchComplete = this.console_researchtimeleft.IsComplete();
        else
          Researcher.ResearchComplete = false;
      }
      else if (Researcher.IsCurrentlyResearching)
        Researcher.ResearchComplete = this.researchtimeleft.QuestGetIsComplete(player.Stats.datetimemanager);
      else
        Researcher.ResearchComplete = false;
    }

    public AnimalType TryToUnlockAlien(
      Player player,
      out bool ServerTimeError,
      out bool AlreadyClaimed)
    {
      AlreadyClaimed = false;
      bool thingClaimed = this.researchtimeleft.ThingClaimed;
      bool flag1 = this.researchtimeleft.IsComplete(player.Stats.datetimemanager, out ServerTimeError);
      if (GameFlags.IsConsoleVersion)
      {
        thingClaimed = this.console_researchtimeleft.ThingClaimed;
        flag1 = this.console_researchtimeleft.IsComplete();
        ServerTimeError = false;
      }
      if (thingClaimed)
      {
        AlreadyClaimed = true;
        ServerTimeError = false;
        return AnimalType.None;
      }
      if (Researcher.IsCurrentlyResearching && flag1)
      {
        bool flag2 = false;
        if (flag1)
        {
          for (int index = 0; index < ResearchData.GetAliensReseachedInOrder().Count; ++index)
          {
            if (!flag2 && !this.AliensResearched.Contains(ResearchData.GetAliensReseachedInOrder()[index]))
            {
              this.AliensResearched.Add(ResearchData.GetAliensReseachedInOrder()[index]);
              if (GameFlags.IsConsoleVersion)
                this.console_researchtimeleft.ClaimReward();
              else
                this.researchtimeleft.ClaimReward();
              Researcher.IsCurrentlyResearching = false;
              this.Alienstotalresearchcomplete.SmartAddValue_MinimumThreshold(false, 1, 0, this.AliensResearched.Count);
              player.OldSaveThisPlayer();
              this.VallidateUnlocks(player);
              return this.AliensResearched[this.AliensResearched.Count - 1];
            }
          }
        }
      }
      return AnimalType.None;
    }

    public TILETYPE TryToUnlockCellBlock(
      Player player,
      out bool ServerTimeError,
      out bool AlreadyClaimed)
    {
      AlreadyClaimed = false;
      bool thingClaimed = this.researchtimeleft.ThingClaimed;
      bool flag1 = this.researchtimeleft.IsComplete(player.Stats.datetimemanager, out ServerTimeError);
      if (GameFlags.IsConsoleVersion)
      {
        thingClaimed = this.console_researchtimeleft.ThingClaimed;
        flag1 = this.console_researchtimeleft.IsComplete();
        ServerTimeError = false;
      }
      if (thingClaimed)
      {
        AlreadyClaimed = true;
        ServerTimeError = false;
        return TILETYPE.Count;
      }
      if (Researcher.IsCurrentlyResearching && flag1)
      {
        bool flag2 = false;
        if (flag1)
        {
          for (int index = 0; index < ResearchData.GetCellTypeInOrder().Count; ++index)
          {
            if (!flag2 && !this.CellBlocksReseacrhed.Contains(ResearchData.GetCellTypeInOrder()[index]))
            {
              this.CellBlocksReseacrhed.Add(ResearchData.GetCellTypeInOrder()[index]);
              if (GameFlags.IsConsoleVersion)
                this.console_researchtimeleft.ClaimReward();
              else
                this.researchtimeleft.ClaimReward();
              Researcher.IsCurrentlyResearching = false;
              this.CellBlockstotalresearchcomplete.SmartAddValue_MinimumThreshold(false, 1, 0, this.CellBlocksReseacrhed.Count);
              player.OldSaveThisPlayer();
              return this.CellBlocksReseacrhed[this.CellBlocksReseacrhed.Count - 1];
            }
            Console.WriteLine("WAS ALREADY UNLOCKED");
          }
        }
      }
      return TILETYPE.Count;
    }

    public TILETYPE TryToUnlockBuilding(
      Player player,
      out bool ServerTimeError,
      out bool AlreadyClaimed)
    {
      AlreadyClaimed = false;
      int num1 = this.researchtimeleft.ThingClaimed ? 1 : 0;
      bool flag1 = this.researchtimeleft.IsComplete(player.Stats.datetimemanager, out ServerTimeError);
      if (GameFlags.IsConsoleVersion)
      {
        int num2 = this.console_researchtimeleft.ThingClaimed ? 1 : 0;
        flag1 = this.console_researchtimeleft.IsComplete();
        ServerTimeError = false;
      }
      if (this.researchtimeleft.ThingClaimed)
      {
        AlreadyClaimed = true;
        ServerTimeError = false;
        return TILETYPE.Count;
      }
      if (Researcher.IsCurrentlyResearching && flag1)
      {
        bool flag2 = false;
        if (flag1)
        {
          for (int index = 0; index < ResearchData.GetBuildingTypesInOrder().Count; ++index)
          {
            if (!flag2 && !this.BuildingsResearched.Contains(ResearchData.GetBuildingTypesInOrder()[index]))
            {
              this.BuildingsResearched.Add(ResearchData.GetBuildingTypesInOrder()[index]);
              if (GameFlags.IsConsoleVersion)
                this.console_researchtimeleft.ClaimReward();
              else
                this.researchtimeleft.ClaimReward();
              Researcher.IsCurrentlyResearching = false;
              this.Buildingstotalresearchcomplete.SmartAddValue_MinimumThreshold(false, 1, 0, this.BuildingsResearched.Count);
              this.VallidateUnlocks(player);
              player.OldSaveThisPlayer();
              return this.BuildingsResearched[this.BuildingsResearched.Count - 1];
            }
          }
        }
      }
      return TILETYPE.Count;
    }

    public void VallidateUnlocks(Player player)
    {
      if ((this.AliensResearched.Contains(AnimalType.Pig) || this.BuildingsResearched.Contains(TILETYPE.Water)) && !player.Stats.AvailableNeedsCategories[2])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[2] = true;
      }
      if ((this.AliensResearched.Contains(AnimalType.Badger) || this.BuildingsResearched.Contains(TILETYPE.Farm)) && !player.Stats.AvailableNeedsCategories[3])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[3] = true;
      }
      if ((this.AliensResearched.Contains(AnimalType.Bear) || this.BuildingsResearched.Contains(TILETYPE.KitchenZone)) && !player.Stats.AvailableNeedsCategories[4])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[4] = true;
      }
      if ((this.AliensResearched.Contains(AnimalType.Donkey) || this.BuildingsResearched.Contains(TILETYPE.Janitor)) && !player.Stats.AvailableNeedsCategories[5])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[5] = true;
      }
      if ((this.AliensResearched.Contains(AnimalType.Tortoise) || this.BuildingsResearched.Contains(TILETYPE.Medical)) && !player.Stats.AvailableNeedsCategories[6])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[6] = true;
      }
      if ((this.AliensResearched.Contains(AnimalType.Panther) || this.BuildingsResearched.Contains(TILETYPE.Security)) && !player.Stats.AvailableNeedsCategories[7])
      {
        player.Stats.NeedsChanged = true;
        player.Stats.AvailableNeedsCategories[7] = true;
      }
      for (int Index = 0; Index < player.inventory.SecretAliensAvailable.Length; ++Index)
      {
        if (player.inventory.SecretAliensAvailable[Index])
        {
          if (LiveStats.reqforpeople == null)
            LiveStats.reqforpeople = new ReqForPeople();
          PReq preq = LiveStats.reqforpeople.wantsbyperson[(int) player.inventory.GetThisSecretAlien(Index)];
          for (int index = 0; index < preq.Uses.Length; ++index)
          {
            if (preq.Uses[index] > 0 && !player.Stats.AvailableNeedsCategories[index])
            {
              player.Stats.NeedsChanged = true;
              player.Stats.AvailableNeedsCategories[index] = true;
            }
          }
        }
      }
    }

    public bool OnlyHasAlienResearch_AndItsBlockedByRank(Player player) => this.IsComplete(ResearchType.Building) && this.IsComplete(ResearchType.CellType) && (!this.IsComplete(ResearchType.Alien) && !Researcher.IsHighEnoughRankToResearchNextAien(player, out int _));

    public void SaveResearcher(Writer writer)
    {
      this.childrenbred.SaveChildrenBred(writer);
      this.researchtimeleft.SaveTimeToResult(writer);
      writer.WriteBool("n", Researcher.IsCurrentlyResearching);
      this.Alienstotalresearchcomplete.SaveObfiscator(writer);
      this.Buildingstotalresearchcomplete.SaveObfiscator(writer);
      this.CellBlockstotalresearchcomplete.SaveObfiscator(writer);
      writer.WriteInt("n", this.AliensResearched.Count);
      for (int index = 0; index < this.AliensResearched.Count; ++index)
        writer.WriteInt("n", (int) this.AliensResearched[index]);
      writer.WriteInt("n", this.BuildingsResearched.Count);
      for (int index = 0; index < this.BuildingsResearched.Count; ++index)
        writer.WriteInt("n", (int) this.BuildingsResearched[index]);
      writer.WriteInt("n", this.CellBlocksReseacrhed.Count);
      for (int index = 0; index < this.CellBlocksReseacrhed.Count; ++index)
        writer.WriteInt("n", (int) this.CellBlocksReseacrhed[index]);
      writer.WriteInt("n", (int) this.reasearchingThis);
      writer.WriteInt("n", Researcher.BusTypesReseacred.Length);
      for (int index = 0; index < Researcher.BusTypesReseacred.Length; ++index)
        writer.WriteBool("n", Researcher.BusTypesReseacred[index]);
    }

    public Researcher(Reader reader)
    {
      this.childrenbred = new ChildrenBred(reader);
      this.AliensResearched = new List<AnimalType>();
      this.BuildingsResearched = new List<TILETYPE>();
      this.CellBlocksReseacrhed = new List<TILETYPE>();
      this.researchtimeleft = new TimeToResult(reader);
      int num1 = (int) reader.ReadBool("n", ref Researcher.IsCurrentlyResearching);
      this.Alienstotalresearchcomplete = new NumberObfiscatorV1(reader);
      this.Buildingstotalresearchcomplete = new NumberObfiscatorV1(reader);
      this.CellBlockstotalresearchcomplete = new NumberObfiscatorV1(reader);
      int _out1 = 0;
      int _out2 = 0;
      int num2 = (int) reader.ReadInt("n", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num3 = (int) reader.ReadInt("n", ref _out2);
        this.AliensResearched.Add((AnimalType) _out2);
      }
      int num4 = (int) reader.ReadInt("n", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num3 = (int) reader.ReadInt("n", ref _out2);
        this.BuildingsResearched.Add((TILETYPE) _out2);
      }
      int num5 = (int) reader.ReadInt("n", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num3 = (int) reader.ReadInt("n", ref _out2);
        this.CellBlocksReseacrhed.Add((TILETYPE) _out2);
      }
      int num6 = (int) reader.ReadInt("n", ref _out1);
      this.reasearchingThis = (ResearchType) _out1;
      Researcher.BusTypesReseacred = new bool[4];
      int num7 = (int) reader.ReadInt("n", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num3 = (int) reader.ReadBool("n", ref Researcher.BusTypesReseacred[index]);
      }
      this.console_researchtimeleft = new RealTime_TimeToResult((int) DateTime.UtcNow.Ticks);
      if (!Z_DebugFlags.UnlockAllBuildingsHack)
        return;
      this.DebugUnlockAllResearch();
    }

    public void SaveResearcherV2(Writer writer) => this.console_researchtimeleft.SaveRealTime_TimeToResult(writer);

    public void LoadResearcherV2(Reader reader) => this.console_researchtimeleft = new RealTime_TimeToResult(reader);
  }
}
