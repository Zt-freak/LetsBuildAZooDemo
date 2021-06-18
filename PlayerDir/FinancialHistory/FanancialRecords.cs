// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.FinancialHistory.FanancialRecords
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Tile_Data;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_WeekOver.V2.Cubes.Cubes.EndOfWeek;

namespace TinyZoo.PlayerDir.FinancialHistory
{
  internal class FanancialRecords
  {
    public List<DayStats> daystats;
    private long Zoo_DaysPassed = -1;
    private List<DayStats> Weeks_Archive;
    private DayStats Totals;
    private int TotalHumanDeaths;
    private int TotalAnimalDeaths;
    private int TotalAnimalBirth;
    private int TotalVisitors;
    private int TotalTradesCompleted;
    private int TotalAnimals;
    public int HighestEarningsInOneDay;
    public int MostVisitorsInOneDay;
    public int TotalBlackMarketTrades;
    private long TotalMoneySpent;
    private long TotalMoneyEarned;
    public int[] ThingsPurchased_HighScores;
    public bool[] HighScoreRgistered;
    private int TempCents;

    public FanancialRecords()
    {
      this.Totals = new DayStats(0L);
      this.Weeks_Archive = new List<DayStats>();
      this.daystats = new List<DayStats>();
      this.daystats.Add(new DayStats(0L));
      this.ThingsPurchased_HighScores = new int[743];
      this.HighScoreRgistered = new bool[743];
    }

    public int GetTotalVisitors() => this.TotalVisitors;

    public int GetTotalHumanDeaths() => this.TotalHumanDeaths;

    public int GetTotalAnimalDeaths() => this.TotalAnimalDeaths;

    public int GetTotalAnimalBirths() => this.TotalAnimalBirth;

    public void AnimalAddedToZoo() => ++this.TotalAnimals;

    public int GetTotalAnimalsInZoo() => this.TotalAnimals;

    public long GetLifetimeProfit() => this.TotalMoneyEarned - this.TotalMoneySpent;

    public long GetLifetimeRevenue() => this.TotalMoneyEarned;

    public void PurchasedFromBlackMrket() => ++this.TotalBlackMarketTrades;

    public long GetDaysPassed() => this.Zoo_DaysPassed == -1L ? 0L : this.Zoo_DaysPassed;

    public int GetProfitToday() => this.daystats[this.daystats.Count - 1].TotalMoneyEarnedThisDay - this.daystats[this.daystats.Count - 1].TotalMoneySpentThisDay;

    public int GetWeekSummaryValue(
      IncomeCubeType thisvaluetype,
      bool IsPreviousweek,
      out bool HasData)
    {
      HasData = false;
      if (this.Weeks_Archive.Count == 0)
        return 0;
      DayStats dayStats = this.Weeks_Archive[this.Weeks_Archive.Count - 1];
      if (IsPreviousweek)
      {
        if (this.Weeks_Archive.Count > 1)
        {
          dayStats = this.Weeks_Archive[this.Weeks_Archive.Count - 2];
        }
        else
        {
          HasData = false;
          return 0;
        }
      }
      HasData = true;
      switch (thisvaluetype)
      {
        case IncomeCubeType.Income:
          return dayStats.TotalMoneyEarnedThisDay;
        case IncomeCubeType.Profit:
          return dayStats.TotalMoneyEarnedThisDay - dayStats.TotalMoneySpentThisDay;
        case IncomeCubeType.Expanditure:
          return dayStats.TotalMoneySpentThisDay;
        default:
          throw new Exception("NOT HERE");
      }
    }

    public int GetTotalOfTheseManufactured(AnimalFoodType animalfoodtype, bool JustYesterday = true)
    {
      if (!JustYesterday)
        return this.Totals.GetTotalOfTheseManufactured(animalfoodtype);
      return this.daystats.Count > 1 ? this.daystats[this.daystats.Count - 2].GetTotalOfTheseManufactured(animalfoodtype) : 0;
    }

    public void ManufacturedAThing(
      int ShopUID,
      AnimalFoodType animalfoodtype,
      bool AddToPreviousDay = false)
    {
      if (animalfoodtype == AnimalFoodType.Count)
        return;
      if (AddToPreviousDay)
      {
        if (this.daystats.Count > 1)
          this.daystats[this.daystats.Count - 2].ManufacturedAThing(ShopUID, animalfoodtype);
      }
      else
        this.daystats[this.daystats.Count - 1].ManufacturedAThing(ShopUID, animalfoodtype);
      this.Totals.ManufacturedAThing(ShopUID, animalfoodtype);
    }

    public void PlayerSpentMoney(
      SpendingCashOnThis spendingonthis,
      int Value,
      bool AddToPreviousDay = false)
    {
      this.MoneyOut(Value);
      if (AddToPreviousDay)
      {
        if (this.daystats.Count > 1)
          this.daystats[this.daystats.Count - 2].PlayerSpentMoney(Value);
      }
      else
        this.daystats[this.daystats.Count - 1].PlayerSpentMoney(Value);
      this.Totals.PlayerSpentMoney(Value);
    }

    public void SetLoanOnClosing(int Value, bool AddToPreviousDay = false)
    {
      if (AddToPreviousDay)
        this.daystats[this.daystats.Count - 2].LoanOnClosing = Value;
      else
        this.daystats[this.daystats.Count - 1].LoanOnClosing = Value;
    }

    public void PlayerTookLoan(int Value, bool AddToPreviousDay = false)
    {
      if (AddToPreviousDay)
        this.daystats[this.daystats.Count - 2].PlayerTookLoan(Value);
      else
        this.daystats[this.daystats.Count - 1].PlayerTookLoan(Value);
    }

    public void CompletedTrade() => ++this.TotalTradesCompleted;

    public int GetPeopleCollectedToday(BUSROUTE route) => this.daystats[this.daystats.Count - 1].PeopleWhoTookBusByStop[(int) route];

    public int GetPeopleWhoWantedToBeOnBusToday(BUSROUTE route) => this.daystats[this.daystats.Count - 1].PeopleAtBusStop[(int) route];

    public void BusDidPickUp(BUSROUTE route, int PeopleWhoGotOnBus) => this.daystats[this.daystats.Count - 1].BusDidPickUp(route, PeopleWhoGotOnBus);

    public List<DayStats> GetDaystatsByTimeSegment(
      TimeSegmentType timesegment,
      out bool NoData,
      bool GetPreviousSegment = false)
    {
      NoData = false;
      List<DayStats> dayStatsList = new List<DayStats>();
      switch (timesegment)
      {
        case TimeSegmentType.Today:
          if (GetPreviousSegment)
          {
            if (Player.financialrecords.daystats.Count > 1)
            {
              dayStatsList.Add(Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2]);
              return dayStatsList;
            }
            NoData = true;
            return dayStatsList;
          }
          if (Player.financialrecords.daystats.Count > 0)
          {
            dayStatsList.Add(Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1]);
            return dayStatsList;
          }
          NoData = true;
          return dayStatsList;
        case TimeSegmentType.Yesterday:
          if (GetPreviousSegment)
          {
            if (Player.financialrecords.daystats.Count > 2)
            {
              dayStatsList.Add(Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 3]);
              return dayStatsList;
            }
            NoData = true;
            return dayStatsList;
          }
          if (Player.financialrecords.daystats.Count > 1)
          {
            dayStatsList.Add(Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 2]);
            return dayStatsList;
          }
          NoData = true;
          return dayStatsList;
        case TimeSegmentType.Last7Days:
        case TimeSegmentType.AllTime:
          int num = 0;
          for (int index = Player.financialrecords.daystats.Count - 1; index > -1; --index)
          {
            if (!GetPreviousSegment)
              dayStatsList.Add(Player.financialrecords.daystats[index]);
            ++num;
            if (GetPreviousSegment)
            {
              if (timesegment == TimeSegmentType.Last7Days && num > 7)
              {
                if (num <= 14)
                {
                  dayStatsList.Add(Player.financialrecords.daystats[index]);
                }
                else
                {
                  NoData = dayStatsList.Count == 0;
                  return dayStatsList;
                }
              }
            }
            else if (timesegment == TimeSegmentType.Last7Days && num >= 7)
            {
              NoData = dayStatsList.Count == 0;
              return dayStatsList;
            }
          }
          NoData = dayStatsList.Count == 0;
          return dayStatsList;
        default:
          return dayStatsList;
      }
    }

    public void DidNotPurchasedATicket() => this.daystats[this.daystats.Count - 1].DidNotPurchasedATicket();

    public void PurchasedATicket(int TicketValue)
    {
      ++this.TotalVisitors;
      this.MoneyIn(TicketValue);
      this.daystats[this.daystats.Count - 1].PurchasedATicket(TicketValue);
      if (this.daystats[this.daystats.Count - 1].PeopleWhoCame > this.MostVisitorsInOneDay)
        this.MostVisitorsInOneDay = this.daystats[this.daystats.Count - 1].PeopleWhoCame;
      this.Totals.PurchasedATicket(TicketValue);
    }

    public void RecievedGrant(int GrantValue)
    {
      this.MoneyIn(GrantValue);
      this.Totals.RecievedGrant(GrantValue);
      this.daystats[this.daystats.Count - 1].RecievedGrant(GrantValue);
    }

    private void MoneyIn(int Value) => this.TotalMoneyEarned += (long) Value;

    private void MoneyOut(int Value) => this.TotalMoneySpent += (long) Value;

    public void PlayerSpentOnIngredientsInAShop(int COGS)
    {
      this.MoneyOut(COGS);
      this.daystats[this.daystats.Count - 1].CostofIngredientsInAShop(COGS);
    }

    public void CustomerPurchasedFromAShop(int ItemSellPrice, TILETYPE tiletype)
    {
      this.TempCents += ItemSellPrice - ItemSellPrice / 100 * 100;
      this.MoneyIn(ItemSellPrice / 100);
      if (this.TempCents >= 100)
      {
        this.MoneyIn(1);
        this.TempCents -= 100;
      }
      this.daystats[this.daystats.Count - 1].PurchasedFromAShop(ItemSellPrice, tiletype);
      this.Totals.PurchasedFromAShop(ItemSellPrice, tiletype);
      if (this.ThingsPurchased_HighScores[(int) tiletype] >= this.daystats[this.daystats.Count - 1].ThingsPurchasedValue[(int) tiletype])
        return;
      this.ThingsPurchased_HighScores[(int) tiletype] = this.daystats[this.daystats.Count - 1].ThingsPurchasedValue[(int) tiletype];
      if (!this.HighScoreRgistered[(int) tiletype] && this.ThingsPurchased_HighScores[(int) tiletype] > 0)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.Menu_Splash, 0.3f);
        NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("New Sales Record!", TileData.GetTileStats(tiletype).Name + " $" + Z_GameFlags.GetCostAsDOllarsAndCentsFromInt(this.ThingsPurchased_HighScores[(int) tiletype]), _notificationicon: NoticicationExtraIcon.Crown, _frametype: NotificationBubbleFrameType.Gilded));
      }
      this.HighScoreRgistered[(int) tiletype] = true;
    }

    public void PlayerSoldAnimalOnBlackMarket(int sellCost)
    {
      this.MoneyIn(sellCost);
      this.daystats[this.daystats.Count - 1].SoldAnimalOnBlackMarket(sellCost);
      this.Totals.SoldAnimalOnBlackMarket(sellCost);
    }

    public void EndedADay(Player player)
    {
      this.HighScoreRgistered = new bool[743];
      GameFlags.IsDay = false;
      this.daystats[this.daystats.Count - 1].BankBalanceOnClosing = player.Stats.GetCashHeld();
      this.daystats[this.daystats.Count - 1].LoanOnClosing = player.Stats.GetCurrentLoan();
      player.employees.EndedADay();
      player.worldhistory.StartNewDay();
      ++this.Zoo_DaysPassed;
      long count = (long) this.daystats.Count;
      long zooDaysPassed = this.Zoo_DaysPassed;
    }

    public void EndedWeek()
    {
      DayStats dayStats = new DayStats((long) this.Weeks_Archive.Count);
      this.Weeks_Archive.Add(dayStats);
      for (int index = 0; index < 7; ++index)
      {
        if (this.daystats.Count - index <= -1)
          throw new Exception("HAS ENTERED NEGATOVE WEEK");
        dayStats.AddDayToWeek(this.daystats[this.daystats.Count - (index + 1)], index == 0);
      }
    }

    public void StartedNewDay() => this.daystats.Add(new DayStats(this.Zoo_DaysPassed));

    public void DoSomeWeirdFunction(Player player)
    {
      if (this.Zoo_DaysPassed != -1L)
        return;
      this.Zoo_DaysPassed = 0L;
    }

    private void RecalculateTotalMoneyEarnedAndSpend()
    {
      this.TotalMoneyEarned = 0L;
      this.TotalMoneySpent = 0L;
      for (int index = 0; index < this.Weeks_Archive.Count; ++index)
      {
        this.TotalMoneyEarned += (long) this.Weeks_Archive[index].TotalMoneyEarnedThisDay;
        this.TotalMoneySpent += (long) this.Weeks_Archive[index].TotalMoneySpentThisDay;
      }
      for (int index = 0; index < this.daystats.Count; ++index)
      {
        this.TotalMoneyEarned += (long) this.daystats[index].TotalMoneyEarnedThisDay;
        this.TotalMoneySpent += (long) this.daystats[index].TotalMoneySpentThisDay;
      }
    }

    public void ResearchedSomething()
    {
      this.Totals.ResearchedSomething();
      this.daystats[this.daystats.Count - 1].ResearchedSomething();
      LiveStats.EarnedResearch = true;
    }

    public int GetTotalResearch() => this.Totals.ResearchUnlocked;

    public FanancialRecords(Reader reader, int VersionNumberForLoad)
    {
      int num1 = (int) reader.ReadLong("f", ref this.Zoo_DaysPassed);
      int _out = 0;
      int num2 = (int) reader.ReadInt("f", ref _out);
      this.daystats = new List<DayStats>();
      for (int index = 0; index < _out; ++index)
        this.daystats.Add(new DayStats(reader, VersionNumberForLoad));
      int num3 = (int) reader.ReadInt("f", ref this.TotalHumanDeaths);
      int num4 = (int) reader.ReadInt("f", ref this.TotalAnimalDeaths);
      int num5 = (int) reader.ReadInt("f", ref this.TotalAnimalBirth);
      int num6 = (int) reader.ReadInt("f", ref this.TotalVisitors);
      int num7 = (int) reader.ReadInt("f", ref this.TotalTradesCompleted);
      int num8 = (int) reader.ReadInt("f", ref this.HighestEarningsInOneDay);
      int num9 = (int) reader.ReadInt("f", ref this.MostVisitorsInOneDay);
      int num10 = (int) reader.ReadInt("f", ref this.TotalBlackMarketTrades);
      if (VersionNumberForLoad > 21)
      {
        int num11 = (int) reader.ReadInt("f", ref _out);
        this.ThingsPurchased_HighScores = new int[_out];
        for (int index = 0; index < _out; ++index)
        {
          int num12 = (int) reader.ReadInt("f", ref this.ThingsPurchased_HighScores[index]);
        }
      }
      else
        this.ThingsPurchased_HighScores = new int[743];
      if (VersionNumberForLoad > 22)
      {
        this.Weeks_Archive = new List<DayStats>();
        int num11 = (int) reader.ReadInt("f", ref _out);
        for (int index = 0; index < _out; ++index)
          this.Weeks_Archive.Add(new DayStats(reader, VersionNumberForLoad));
      }
      else
        this.Weeks_Archive = new List<DayStats>();
      if (VersionNumberForLoad > 25)
      {
        int num13 = (int) reader.ReadInt("f", ref this.TotalAnimals);
      }
      this.Totals = VersionNumberForLoad <= 39 ? new DayStats(0L) : new DayStats(reader, VersionNumberForLoad);
      this.HighScoreRgistered = new bool[743];
      this.RecalculateTotalMoneyEarnedAndSpend();
      Z_GameFlags.HasStartedFirstDay = true;
    }

    public void SaveFanancialRecords(Writer writer)
    {
      writer.WriteLong("f", this.Zoo_DaysPassed);
      writer.WriteInt("f", this.daystats.Count);
      for (int index = 0; index < this.daystats.Count; ++index)
        this.daystats[index].SaveDayStats(writer);
      writer.WriteInt("f", this.TotalHumanDeaths);
      writer.WriteInt("f", this.TotalAnimalDeaths);
      writer.WriteInt("f", this.TotalAnimalBirth);
      writer.WriteInt("f", this.TotalVisitors);
      writer.WriteInt("f", this.TotalTradesCompleted);
      writer.WriteInt("f", this.HighestEarningsInOneDay);
      writer.WriteInt("f", this.MostVisitorsInOneDay);
      writer.WriteInt("f", this.TotalBlackMarketTrades);
      writer.WriteInt("f", this.ThingsPurchased_HighScores.Length);
      for (int index = 0; index < this.ThingsPurchased_HighScores.Length; ++index)
        writer.WriteInt("f", this.ThingsPurchased_HighScores[index]);
      writer.WriteInt("f", this.Weeks_Archive.Count);
      for (int index = 0; index < this.Weeks_Archive.Count; ++index)
        this.Weeks_Archive[index].SaveDayStats(writer);
      writer.WriteInt("f", this.TotalAnimals);
      this.Totals.SaveDayStats(writer);
    }
  }
}
