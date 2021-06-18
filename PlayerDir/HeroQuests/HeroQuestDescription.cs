// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.HeroQuestDescription
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Quests;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.HeroQuests
{
  internal class HeroQuestDescription
  {
    public HEROQUESTTYPE heroquesttype;
    public StringID ThisQuestHeading = StringID.Count;
    public StringID ThisQuestDescriptin = StringID.Count;
    public StringID ThisQuestCompleteText = StringID.Count;
    public bool UseCustomQuestDescriptionString;
    public bool WillAutoPin;
    public NoticicationExtraIcon AlsoDrawThistileTypeOnComplete;
    public int OtherValue;
    private int AdditionalNumber;
    public int MustHaveThisMany;
    public int AnotherValue;
    public int UID;
    public List<HeroQuestDescription> UnlockThisQuestCriteria;
    public bool HasReward;
    private RewardPack rewardpack;
    public bool WillAutoPopOnUnlock = true;
    public bool WillAutoPopOnComplete;
    public string ThoughtBubbleHold = "";
    public StringID TaskShortSummary = StringID.Count;
    public TutorialQuestSpecial tutorialquestspecial;
    public bool WillShortcutOnPanelOpenIfomplete;
    public HeroCharacter herocharacter;

    public HeroQuestDescription(ref int _UID, HeroCharacter _herocharacter)
    {
      this.UnlockThisQuestCriteria = new List<HeroQuestDescription>();
      this.herocharacter = _herocharacter;
      this.UID = _UID;
      ++_UID;
    }

    public HeroQuestDescription()
    {
    }

    public bool CheckUnlockQuestsCriteriaComplete(Player player)
    {
      for (int index = 0; index < this.UnlockThisQuestCriteria.Count; ++index)
      {
        if (!this.UnlockThisQuestCriteria[index].CheckIsComplete(player))
          return false;
      }
      return true;
    }

    public string GetHeaderForUnlockCriteria(
      out string Progress,
      Player player,
      out int ProgressStart,
      out int Progressend)
    {
      int index = 0;
      if (index < this.UnlockThisQuestCriteria.Count)
        return SEngine.Localization.Localization.GetText(760) + this.UnlockThisQuestCriteria[index].GetObjectiveHeading(out Progress, player, out ProgressStart, out Progressend);
      Progress = "ERROR";
      ProgressStart = 0;
      Progressend = 0;
      return "QUEST TEXT ERROR";
    }

    public void SetReward(RewardPack _rewardpack)
    {
      this.HasReward = true;
      this.rewardpack = _rewardpack;
    }

    public void ProcessReward(Player player) => this.rewardpack.ProcessReward(player);

    public string GetRewardString() => this.rewardpack.GetRewardString();

    public bool CheckIsComplete(Player player)
    {
      switch (this.heroquesttype)
      {
        case HEROQUESTTYPE.CompleteZooTrade:
          return this.CheckCompleteZooTrade(player);
        case HEROQUESTTYPE.BuyBus:
          return this.CheckBuyBus(player);
        case HEROQUESTTYPE.CurrentAnimalNumber:
          return this.CheckCompleteumberOfAnAnimal(player);
        case HEROQUESTTYPE.BuildPen:
          return this.CheckBuildPen(player);
        case HEROQUESTTYPE.OpenForBusiness:
          return Z_GameFlags.HasStartedFirstDay;
        case HEROQUESTTYPE.AtBeginingOfGame:
          return true;
        case HEROQUESTTYPE.DaysPassed:
          return Player.financialrecords.GetDaysPassed() >= (long) this.MustHaveThisMany;
        case HEROQUESTTYPE.ThingsBuilt:
          return this.CheckThingsBuilt(player) >= this.MustHaveThisMany;
        case HEROQUESTTYPE.OtherHeroCharacterprogress:
          return this.CheckOtherHeroCharacterprogress(player);
        case HEROQUESTTYPE.EvilPoints:
          return (double) player.livestats.MoralityScore <= (double) -this.MustHaveThisMany;
        case HEROQUESTTYPE.GoodPoints:
          return (double) player.livestats.MoralityScore >= (double) -this.MustHaveThisMany;
        case HEROQUESTTYPE.Events:
          throw new Exception("WHAT IS AN EVENT?");
        case HEROQUESTTYPE.HumanDeaths:
          return Player.financialrecords.GetTotalHumanDeaths() >= -this.MustHaveThisMany;
        case HEROQUESTTYPE.AnimalDeaths:
          return Player.financialrecords.GetTotalAnimalDeaths() >= -this.MustHaveThisMany;
        case HEROQUESTTYPE.Birth:
          return Player.financialrecords.GetTotalAnimalBirths() >= -this.MustHaveThisMany;
        case HEROQUESTTYPE.GenomeMap_OrVariantsFound:
          return this.CheckGenomeMap(player);
        case HEROQUESTTYPE.Wealth:
          return this.GetWealth(player);
        case HEROQUESTTYPE.CrossBreeds:
          return this.GetCrossBreeds(player);
        case HEROQUESTTYPE.VistorCount:
          return this.CheckVisitorTarget(player);
        case HEROQUESTTYPE.Reputation:
          throw new Exception("NOT DONE");
        case HEROQUESTTYPE.Research:
          return this.OtherValue > -1 ? player.unlocks.UnlockedThings[this.OtherValue] > 0 : Player.financialrecords.GetTotalResearch() >= this.MustHaveThisMany;
        case HEROQUESTTYPE.ZooSize:
          return this.CheckLandUnlocked();
        case HEROQUESTTYPE.DonateMoney:
          return false;
        case HEROQUESTTYPE.BuyAnimalFromBlackMarket:
          return Player.financialrecords.TotalBlackMarketTrades >= this.MustHaveThisMany;
        case HEROQUESTTYPE.ViewTasks:
          return Z_GameFlags.HasViewedTasks;
        case HEROQUESTTYPE.ParkRating:
          return this.CheckParkRating(player);
        case HEROQUESTTYPE.EmployThisPerson:
          return player.employees.GetCountEmployeesOfThisType((EmployeeType) this.OtherValue) >= this.MustHaveThisMany;
        case HEROQUESTTYPE.UnlockBuilding:
          return this.CheckOnUnlockBuilding(player);
        case HEROQUESTTYPE.ReachDecoLevel:
          DecoCalculator.GetDecoQuestComplete(this.MustHaveThisMany, this.OtherValue, out int _, out int _);
          break;
      }
      throw new Exception("NOT HANDLED");
    }

    public string GetObjectiveHeading(
      out string Progress,
      Player player,
      out int ProgressStart,
      out int Progressend)
    {
      Progress = "";
      ProgressStart = 0;
      Progressend = 0;
      switch (this.heroquesttype)
      {
        case HEROQUESTTYPE.CompleteZooTrade:
          if (this.OtherValue == 56)
          {
            ProgressStart = player.zquests.GetQuestsCompletedWithThisZooKeeper((AnimalType) this.OtherValue);
            Progressend = this.MustHaveThisMany;
            Progress = ProgressStart.ToString() + "/" + (object) this.MustHaveThisMany;
            return string.Format(SEngine.Localization.Localization.GetText(875), (object) this.MustHaveThisMany);
          }
          ProgressStart = player.zquests.GetQuestsCompletedWithThisZooKeeper((AnimalType) this.OtherValue);
          Progressend = this.MustHaveThisMany;
          Progress = player.zquests.GetQuestsCompletedWithThisZooKeeper((AnimalType) this.OtherValue).ToString() + "/" + (object) this.MustHaveThisMany;
          HeroCharacter animalTypeToHero = HeroQuestData.GetAnimalTypeToHero((AnimalType) this.OtherValue);
          return string.Format(SEngine.Localization.Localization.GetText(876), (object) this.MustHaveThisMany, (object) HeroQuestData.GetHeroCharacterToString(animalTypeToHero));
        case HEROQUESTTYPE.BuyBus:
          if (this.AdditionalNumber == 10)
          {
            if (this.AdditionalNumber == 4)
            {
              ProgressStart = player.busroutes.GetTotalBussesOwned((BUSTYPE) this.AdditionalNumber);
              Progressend = this.MustHaveThisMany;
              Progress = player.busroutes.GetTotalBussesOwned((BUSTYPE) this.AdditionalNumber).ToString() + "/" + (object) this.MustHaveThisMany;
              return string.Format(SEngine.Localization.Localization.GetText(877), (object) this.MustHaveThisMany);
            }
            ProgressStart = player.busroutes.GetTotalBussesOwned((BUSTYPE) this.AdditionalNumber);
            Progressend = this.MustHaveThisMany;
            Progress = player.busroutes.GetTotalBussesOwned((BUSTYPE) this.AdditionalNumber).ToString() + "/" + (object) this.MustHaveThisMany;
            return string.Format(SEngine.Localization.Localization.GetText(878), (object) this.MustHaveThisMany, (object) BusTimes.GetBusName((BUSTYPE) this.AdditionalNumber));
          }
          if (this.AdditionalNumber == 4)
          {
            ProgressStart = player.busroutes.GetBussesByRoute((BUSROUTE) this.OtherValue, (BUSTYPE) this.AdditionalNumber).Count;
            Progressend = this.MustHaveThisMany;
            Progress = player.busroutes.GetBussesByRoute((BUSROUTE) this.OtherValue, (BUSTYPE) this.AdditionalNumber).Count.ToString() + "/" + (object) this.MustHaveThisMany;
            return string.Format(SEngine.Localization.Localization.GetText(877), (object) this.MustHaveThisMany);
          }
          ProgressStart = player.busroutes.GetBussesByRoute((BUSROUTE) this.OtherValue, (BUSTYPE) this.AdditionalNumber).Count;
          Progressend = this.MustHaveThisMany;
          Progress = player.busroutes.GetBussesByRoute((BUSROUTE) this.OtherValue, (BUSTYPE) this.AdditionalNumber).Count.ToString() + "/" + (object) this.MustHaveThisMany;
          return string.Format(SEngine.Localization.Localization.GetText(879), (object) this.MustHaveThisMany, (object) BusTimes.GetBusRouteName((BUSROUTE) this.OtherValue, false));
        case HEROQUESTTYPE.CurrentAnimalNumber:
          ProgressStart = player.prisonlayout.GetTotalOfThisAnimal((AnimalType) this.OtherValue, this.AnotherValue);
          Progressend = this.MustHaveThisMany;
          Progress = player.prisonlayout.GetTotalOfThisAnimal((AnimalType) this.OtherValue, this.AnotherValue).ToString() + "/" + (object) this.MustHaveThisMany;
          if (this.OtherValue == 56)
            return string.Format(SEngine.Localization.Localization.GetText(880), (object) this.MustHaveThisMany);
          return this.AnotherValue == -1 ? string.Format(SEngine.Localization.Localization.GetText(881), (object) this.MustHaveThisMany, (object) AnimalData.GetAnimalName((AnimalType) this.OtherValue)) : string.Format(SEngine.Localization.Localization.GetText(882), (object) this.MustHaveThisMany, (object) AnimalData.GetAnimalName((AnimalType) this.OtherValue), (object) this.AnotherValue);
        case HEROQUESTTYPE.BuildPen:
          ProgressStart = player.prisonlayout.cellblockcontainer.prisonzones.Count;
          Progressend = this.MustHaveThisMany;
          Progress = player.prisonlayout.cellblockcontainer.prisonzones.Count.ToString() + "/" + (object) this.MustHaveThisMany;
          return this.MustHaveThisMany == 1 ? string.Format(SEngine.Localization.Localization.GetText(883), (object) this.MustHaveThisMany) : string.Format(SEngine.Localization.Localization.GetText(884), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.OpenForBusiness:
          return SEngine.Localization.Localization.GetText(885);
        case HEROQUESTTYPE.AtBeginingOfGame:
          return "NO STRING REQUIRED";
        case HEROQUESTTYPE.DaysPassed:
          ProgressStart = (int) Player.financialrecords.GetDaysPassed();
          Progressend = this.MustHaveThisMany;
          Progress = Player.financialrecords.GetDaysPassed().ToString() + "/" + (object) this.MustHaveThisMany;
          return SEngine.Localization.Localization.GetText(886);
        case HEROQUESTTYPE.ThingsBuilt:
          int num = this.CheckThingsBuilt(player);
          ProgressStart = num;
          Progressend = this.MustHaveThisMany;
          Progress = num.ToString() + "/" + (object) this.MustHaveThisMany;
          return this.OtherValue != 743 ? string.Format(SEngine.Localization.Localization.GetText(887), (object) this.MustHaveThisMany, (object) TileData.GetTileStats((TILETYPE) this.OtherValue).Name) : string.Format(SEngine.Localization.Localization.GetText(888), (object) this.MustHaveThisMany, (object) CategoryData.GetCategoryToname((CATEGORYTYPE) this.OtherValue));
        case HEROQUESTTYPE.OtherHeroCharacterprogress:
          if (this.MustHaveThisMany == 0)
          {
            ProgressStart = 0;
            Progressend = 1;
            if (player.heroquestprogress.ProgressArray[this.OtherValue].IsUnlocked)
              ProgressStart = 1;
            Progress = ProgressStart.ToString() + "/" + (object) this.MustHaveThisMany;
            return string.Format(SEngine.Localization.Localization.GetText(889), (object) HeroQuestData.GetHeroCharacterToString((HeroCharacter) this.OtherValue));
          }
          int completeFromThisHero = player.heroquestprogress.GetQuestsCompleteFromThisHero((HeroCharacter) this.OtherValue);
          ProgressStart = completeFromThisHero;
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) this.MustHaveThisMany;
          return string.Format(SEngine.Localization.Localization.GetText(890), (object) this.MustHaveThisMany, (object) HeroQuestData.GetHeroCharacterToString((HeroCharacter) this.OtherValue));
        case HEROQUESTTYPE.EvilPoints:
          return "NOT CODED";
        case HEROQUESTTYPE.GoodPoints:
          return "NOT CODED";
        case HEROQUESTTYPE.Events:
          return "NOT CODED";
        case HEROQUESTTYPE.HumanDeaths:
          return "NOT CODED";
        case HEROQUESTTYPE.AnimalDeaths:
          return "NOT CODED";
        case HEROQUESTTYPE.Birth:
          return "NOT CODED";
        case HEROQUESTTYPE.GenomeMap_OrVariantsFound:
          return "NOT CODED";
        case HEROQUESTTYPE.Wealth:
          if (this.OtherValue == 1)
          {
            ProgressStart = Player.financialrecords.HighestEarningsInOneDay;
            Progressend = this.MustHaveThisMany;
            Progress = Player.financialrecords.GetDaysPassed().ToString() + "/" + (object) this.MustHaveThisMany;
            return string.Format(SEngine.Localization.Localization.GetText(891), (object) this.MustHaveThisMany);
          }
          ProgressStart = player.Stats.GetCashHeld();
          Progressend = this.MustHaveThisMany;
          Progress = Player.financialrecords.GetDaysPassed().ToString() + "/" + (object) this.MustHaveThisMany;
          return string.Format(SEngine.Localization.Localization.GetText(892), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.CrossBreeds:
          ProgressStart = player.prisonlayout.GetTotalCrossbreed((AnimalType) this.OtherValue, (AnimalType) this.AnotherValue);
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format("Have Hybrid Animals", (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.VistorCount:
          if (this.OtherValue > 0)
          {
            ProgressStart = Player.financialrecords.MostVisitorsInOneDay;
            Progressend = this.MustHaveThisMany;
            Progress = ProgressStart.ToString() + "/" + (object) Progressend;
            return string.Format(SEngine.Localization.Localization.GetText(893), (object) this.MustHaveThisMany);
          }
          ProgressStart = Player.financialrecords.GetTotalVisitors();
          Progressend = this.MustHaveThisMany;
          Progress = Math.Min(ProgressStart, Progressend).ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(894), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.Reputation:
          return "NOT CODED";
        case HEROQUESTTYPE.Research:
          if (this.OtherValue > -1)
          {
            ProgressStart = 0;
            Progressend = 1;
            Progress = Math.Min(ProgressStart, Progressend).ToString() + "/" + (object) Progressend;
            return "Research hire additioanal researcher.";
          }
          ProgressStart = Player.financialrecords.GetTotalResearch();
          Progressend = this.MustHaveThisMany;
          Progress = Math.Min(ProgressStart, Progressend).ToString() + "/" + (object) Progressend;
          return this.MustHaveThisMany == 1 ? "Unlock one Research entry" : string.Format("Unlock at least {0} Research entries", (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.ZooSize:
          ProgressStart = PlayerStats.GetTotalLandUnlocked() - 1;
          Progressend = this.MustHaveThisMany - 1;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(895), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.DonateMoney:
          ProgressStart = 0;
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(899), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.BuyAnimalFromBlackMarket:
          ProgressStart = Player.financialrecords.TotalBlackMarketTrades;
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(896), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.SellAnimalOnBlackMarket:
          return "NOT CODED";
        case HEROQUESTTYPE.ViewTasks:
          Progressend = 1;
          ProgressStart = 0;
          if (Z_GameFlags.HasViewedTasks)
          {
            Progressend = 1;
            ProgressStart = 1;
          }
          return SEngine.Localization.Localization.GetText(898);
        case HEROQUESTTYPE.ParkRating:
          ProgressStart = ParkRating.GetParkRating(player);
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(897), (object) this.MustHaveThisMany);
        case HEROQUESTTYPE.EmployThisPerson:
          ProgressStart = player.employees.GetCountEmployeesOfThisType((EmployeeType) this.OtherValue);
          Progressend = this.MustHaveThisMany;
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return string.Format(SEngine.Localization.Localization.GetText(1054), (object) this.MustHaveThisMany, (object) EmployeesStats.GetJobTitle((EmployeeType) this.OtherValue, AnimalType.None, IncludeSeniorityPrepend: false));
        case HEROQUESTTYPE.ReachDecoLevel:
          DecoCalculator.GetDecoQuestComplete(this.MustHaveThisMany, this.OtherValue, out ProgressStart, out Progressend);
          Progress = ProgressStart.ToString() + "/" + (object) Progressend;
          return this.OtherValue == -1 ? string.Format("Reach {0}% decoration level in all unlocked areas of your park. To View current progress use the heat map view", (object) this.MustHaveThisMany, (object) this.OtherValue) : string.Format("Reach {0}% decoration level in at least {1} areas of your park. To View current progress use the heat map view", (object) this.MustHaveThisMany, (object) this.OtherValue);
        default:
          throw new Exception("NNNNOOO");
      }
    }

    public void SetUpDoZooTrade(AnimalType thiszookeeper = AnimalType.None, int TotalTrades = 0)
    {
      this.heroquesttype = HEROQUESTTYPE.CompleteZooTrade;
      this.OtherValue = (int) thiszookeeper;
      this.MustHaveThisMany = TotalTrades;
    }

    private bool CheckCompleteZooTrade(Player player) => player.zquests.GetQuestsCompletedWithThisZooKeeper((AnimalType) this.OtherValue) >= this.MustHaveThisMany;

    public void SetUpBuyBus(BUSROUTE busroute = BUSROUTE.Count, BUSTYPE bustobuy = BUSTYPE.Count, int _MustHaveThisMany = 1)
    {
      this.heroquesttype = HEROQUESTTYPE.BuyBus;
      this.OtherValue = (int) busroute;
      this.AdditionalNumber = (int) bustobuy;
      this.MustHaveThisMany = _MustHaveThisMany;
    }

    private bool CheckBuyBus(Player player)
    {
      if (this.AdditionalNumber == 10)
        return player.busroutes.GetTotalBussesOwned((BUSTYPE) this.OtherValue) >= this.MustHaveThisMany;
      int additionalNumber = this.AdditionalNumber;
      return player.busroutes.GetBussesByRoute((BUSROUTE) this.OtherValue, (BUSTYPE) this.AdditionalNumber).Count >= this.MustHaveThisMany;
    }

    public void SetUpGetNumberOfAnAnimal(AnimalType animal, int GetThisMany, int Variant = -1)
    {
      this.heroquesttype = HEROQUESTTYPE.CurrentAnimalNumber;
      this.OtherValue = (int) animal;
      this.MustHaveThisMany = GetThisMany;
      this.AnotherValue = Variant;
    }

    private bool CheckCompleteumberOfAnAnimal(Player player) => player.prisonlayout.GetTotalOfThisAnimal((AnimalType) this.OtherValue, this.AnotherValue) >= this.MustHaveThisMany;

    public void SetUpUniqueAnimalTypes(int GetThisMany)
    {
      this.heroquesttype = HEROQUESTTYPE.UniqueAnimalTypes;
      this.MustHaveThisMany = GetThisMany;
    }

    private bool CheckCompleteUniqueAnimalTypes(Player player) => player.prisonlayout.GetTotalOfThisAnimal((AnimalType) this.OtherValue, this.AnotherValue) >= this.MustHaveThisMany;

    public void SetUpBuildPen(int _MustHaveThisMany)
    {
      this.heroquesttype = HEROQUESTTYPE.BuildPen;
      this.MustHaveThisMany = _MustHaveThisMany;
    }

    private bool CheckBuildPen(Player player) => player.prisonlayout.cellblockcontainer.prisonzones.Count >= this.MustHaveThisMany;

    public void SetUpOpenForBusiness() => this.heroquesttype = HEROQUESTTYPE.OpenForBusiness;

    public void SetUpUnlockAtStartOfGame() => this.heroquesttype = HEROQUESTTYPE.AtBeginingOfGame;

    public void SetUpDaysPassed(int Days)
    {
      this.MustHaveThisMany = Days;
      this.heroquesttype = HEROQUESTTYPE.DaysPassed;
    }

    public void SetUpOnUnlockBuilding(TILETYPE tiletype)
    {
      this.MustHaveThisMany = (int) tiletype;
      this.heroquesttype = HEROQUESTTYPE.UnlockBuilding;
    }

    public bool CheckOnUnlockBuilding(Player player) => player.Stats.research.BuildingsResearched.Contains((TILETYPE) this.MustHaveThisMany);

    public void SetUpThingsBuilt(
      int TotalToBuild,
      TILETYPE buildingtype = TILETYPE.Count,
      CATEGORYTYPE catergorytype = CATEGORYTYPE.Count)
    {
      this.heroquesttype = HEROQUESTTYPE.ThingsBuilt;
      this.OtherValue = (int) buildingtype;
      this.MustHaveThisMany = TotalToBuild;
      this.AnotherValue = (int) catergorytype;
    }

    private int CheckThingsBuilt(Player player)
    {
      if (this.OtherValue == 743)
        return player.prisonlayout.layout.GetTotalOfThese((CATEGORYTYPE) this.OtherValue);
      if (this.OtherValue != 192)
        return player.prisonlayout.layout.GetTotalOfThese((TILETYPE) this.OtherValue);
      return Z_GameFlags.HasBuiltStoreRoom ? 1 : 0;
    }

    public void SetUpOtherHeroCharacterprogress(
      HeroCharacter hero,
      int QuestsComplete_ZeroUnluckCharacter = 0)
    {
      this.heroquesttype = HEROQUESTTYPE.OtherHeroCharacterprogress;
      this.OtherValue = (int) hero;
      this.MustHaveThisMany = QuestsComplete_ZeroUnluckCharacter;
    }

    private bool CheckOtherHeroCharacterprogress(Player player) => player.heroquestprogress.CheckCharacterProgress((HeroCharacter) this.OtherValue, this.MustHaveThisMany);

    public void SetUpEvilPoints(int EvilPointTarget)
    {
      this.heroquesttype = HEROQUESTTYPE.EvilPoints;
      this.MustHaveThisMany = EvilPointTarget;
    }

    public void SetUpGoodPoints(int EvilPointTarget)
    {
      this.heroquesttype = HEROQUESTTYPE.GoodPoints;
      this.MustHaveThisMany = EvilPointTarget;
    }

    public void SetUpEvents(int EventsComplete)
    {
      this.heroquesttype = HEROQUESTTYPE.Events;
      this.MustHaveThisMany = EventsComplete;
    }

    public void SetUpHumanDeaths(int HumanDeaths)
    {
      this.heroquesttype = HEROQUESTTYPE.HumanDeaths;
      this.MustHaveThisMany = HumanDeaths;
    }

    public void SetUpParkRating(int TargetRating)
    {
      this.heroquesttype = HEROQUESTTYPE.ParkRating;
      this.MustHaveThisMany = TargetRating;
    }

    private bool CheckParkRating(Player player) => ParkRating.GetParkRating(player) >= this.MustHaveThisMany;

    public void SetUpGenome(int UseThisTotal_IgnoreAnimal = 0, AnimalType IsJustThisAnimal = AnimalType.None)
    {
      this.heroquesttype = HEROQUESTTYPE.GenomeMap_OrVariantsFound;
      this.MustHaveThisMany = UseThisTotal_IgnoreAnimal;
      this.OtherValue = (int) IsJustThisAnimal;
    }

    private bool CheckGenomeMap(Player player) => this.MustHaveThisMany > 0 ? player.Stats.GetTotalVaiantsFound((AnimalType) this.OtherValue) >= this.MustHaveThisMany : player.Stats.IsThisGenomeMapped((AnimalType) this.OtherValue);

    public void SetUpMoneyCashTarget(int HveThisMushMoney, bool EarnInOneDay)
    {
      this.heroquesttype = HEROQUESTTYPE.Wealth;
      this.MustHaveThisMany = HveThisMushMoney;
      if (!EarnInOneDay)
        return;
      this.OtherValue = 1;
    }

    private bool GetWealth(Player player) => this.OtherValue > 0 ? Player.financialrecords.HighestEarningsInOneDay >= this.MustHaveThisMany : player.Stats.GetCashHeld() >= this.MustHaveThisMany;

    public void SetUpCrossBreed(int ThisMany, AnimalType AnimalA = AnimalType.None, AnimalType AnimaB = AnimalType.None)
    {
      this.heroquesttype = HEROQUESTTYPE.CrossBreeds;
      this.MustHaveThisMany = ThisMany;
      this.OtherValue = (int) AnimalA;
      this.AnotherValue = (int) AnimaB;
    }

    private bool GetCrossBreeds(Player player) => player.prisonlayout.GetTotalCrossbreed((AnimalType) this.OtherValue, (AnimalType) this.AnotherValue) >= this.MustHaveThisMany;

    public void SetUpVisitorTarget(bool IsOneDay, int ThisManyVisitors)
    {
      this.heroquesttype = HEROQUESTTYPE.VistorCount;
      this.MustHaveThisMany = ThisManyVisitors;
      if (!IsOneDay)
        return;
      this.OtherValue = 1;
    }

    private bool CheckVisitorTarget(Player player) => this.OtherValue > 0 ? Player.financialrecords.MostVisitorsInOneDay >= this.MustHaveThisMany : Player.financialrecords.GetTotalVisitors() >= this.MustHaveThisMany;

    public void SetUpLandUnlocked(int UnlockThisMany)
    {
      this.heroquesttype = HEROQUESTTYPE.ZooSize;
      this.MustHaveThisMany = UnlockThisMany;
    }

    private bool CheckLandUnlocked() => PlayerStats.GetTotalLandUnlocked() >= this.MustHaveThisMany;

    public void SetUpResearch(int UnlockThisMany, UnlockTYPE unlocktypeToCheck = UnlockTYPE.Count)
    {
      this.OtherValue = -1;
      if (unlocktypeToCheck != UnlockTYPE.Count)
        this.OtherValue = (int) unlocktypeToCheck;
      this.heroquesttype = HEROQUESTTYPE.Research;
      this.MustHaveThisMany = UnlockThisMany;
    }

    public void SetUpGiveMoney(int GiveThisMuch)
    {
      this.heroquesttype = HEROQUESTTYPE.DonateMoney;
      this.MustHaveThisMany = GiveThisMuch;
    }

    public void SetUpBuyAnimalFromBlackMarket(int DoTheseManyTrades)
    {
      this.heroquesttype = HEROQUESTTYPE.BuyAnimalFromBlackMarket;
      this.MustHaveThisMany = DoTheseManyTrades;
    }

    private bool CheckBuyAnimalFromBlackMarket(Player player) => false;

    public void SetUpSellAnimalOnBlackMarket(int SellThisManyanimals)
    {
      this.heroquesttype = HEROQUESTTYPE.SellAnimalOnBlackMarket;
      this.MustHaveThisMany = SellThisManyanimals;
    }

    private bool CheckSellAnimalOnBlackMarkett(Player player) => false;

    public void SetUpViewTasks() => this.heroquesttype = HEROQUESTTYPE.ViewTasks;

    public void SetUpEmployThisPerson(EmployeeType employee, int EmployThisMany = 1)
    {
      this.MustHaveThisMany = EmployThisMany;
      this.OtherValue = (int) employee;
      this.heroquesttype = HEROQUESTTYPE.EmployThisPerson;
    }

    public void SetUpReachDecoLevel(int TargetPercentage, int NumberOfZones_MinusOneForAllUnlocked)
    {
      this.MustHaveThisMany = TargetPercentage;
      this.OtherValue = NumberOfZones_MinusOneForAllUnlocked;
      this.heroquesttype = HEROQUESTTYPE.ReachDecoLevel;
    }

    public string GetQuestHeading() => SEngine.Localization.Localization.GetText((int) this.ThisQuestHeading);

    public string GetQuestDescription()
    {
      if (this.UseCustomQuestDescriptionString)
      {
        switch (this.herocharacter)
        {
          case HeroCharacter.FootballCaptain:
            if (this.UID == 0)
              return string.Format(SEngine.Localization.Localization.GetText((int) this.ThisQuestDescriptin), (object) BusTimes.GetBusRouteName(BUSROUTE.Route01, false));
            break;
          case HeroCharacter.TransportPlaner:
            if (this.UID == 0)
              return string.Format(SEngine.Localization.Localization.GetText((int) this.ThisQuestDescriptin), (object) 100);
            break;
        }
      }
      return SEngine.Localization.Localization.GetText((int) this.ThisQuestDescriptin);
    }

    public string GetQuestCompleteText() => this.ThisQuestCompleteText != StringID.Count ? SEngine.Localization.Localization.GetText((int) this.ThisQuestCompleteText) : string.Empty;

    public string GetTaskShortSummary() => this.TaskShortSummary != StringID.Count ? SEngine.Localization.Localization.GetText((int) this.TaskShortSummary) : string.Empty;
  }
}
