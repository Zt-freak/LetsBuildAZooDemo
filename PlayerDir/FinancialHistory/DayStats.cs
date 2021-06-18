// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.FinancialHistory.DayStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Tile_Data;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.FinancialHistory
{
  internal class DayStats
  {
    public int Day;
    public int PeopleWhoCame;
    public int PeopleWhoWantedToCome;
    public int TotalMoneyEarnedThisDay;
    public int TotalMoneySpentThisDay;
    public int BankBalanceOnClosing;
    public int LoanOnClosing;
    public int LoanTaken;
    public int MoneyFromTicketSales;
    public int MoneyFromFood;
    public int MoneyFromDrinks;
    public int MoneyFromSouvenirs;
    public int MonsyFromBlackMarket;
    public int MoneyFromCommodities;
    public int MoneyFromDonations;
    public int MoneyFromGrants;
    public int[] PeopleAtBusStop;
    public int[] PeopleWhoTookBusByStop;
    public int ResearchUnlocked;
    public int FoodItemsSold;
    public int SouveniersSold;
    public int DrinksSold;
    public int CommoditiesSold;
    private int[] ThingsManufactured;
    public int[] ThingsPurchasedValue;
    public int[] ThingsPurchasedTotal;
    private int LeftOverCents;

    public DayStats(long CurrentDay)
    {
      this.Day = (int) CurrentDay;
      this.PeopleWhoTookBusByStop = new int[10];
      this.PeopleAtBusStop = new int[10];
      this.ThingsPurchasedValue = new int[743];
      this.ThingsPurchasedTotal = new int[743];
      this.ThingsManufactured = new int[88];
    }

    public void AddDayToWeek(DayStats day, bool IsSundayClose)
    {
      this.PeopleWhoCame += day.PeopleWhoCame;
      this.PeopleWhoWantedToCome += day.PeopleWhoWantedToCome;
      this.TotalMoneyEarnedThisDay += day.TotalMoneyEarnedThisDay;
      this.TotalMoneySpentThisDay += day.TotalMoneySpentThisDay;
      if (IsSundayClose)
      {
        this.BankBalanceOnClosing = day.PeopleWhoWantedToCome;
        this.LoanOnClosing = day.LoanOnClosing;
      }
      this.MoneyFromTicketSales += day.MoneyFromTicketSales;
      this.MoneyFromFood += day.MoneyFromFood;
      this.MoneyFromDrinks += day.MoneyFromDrinks;
      this.MoneyFromSouvenirs += day.MoneyFromSouvenirs;
      this.MonsyFromBlackMarket += day.MonsyFromBlackMarket;
      this.MoneyFromCommodities += day.MoneyFromCommodities;
      this.MoneyFromDonations += day.MoneyFromDonations;
      for (int index = 0; index < this.PeopleAtBusStop.Length; ++index)
        this.PeopleAtBusStop[index] += day.PeopleAtBusStop[index];
      for (int index = 0; index < this.PeopleWhoTookBusByStop.Length; ++index)
        this.PeopleWhoTookBusByStop[index] += day.PeopleWhoTookBusByStop[index];
      this.FoodItemsSold += day.MoneyFromDonations;
      this.SouveniersSold += day.MoneyFromDonations;
      this.DrinksSold += day.MoneyFromDonations;
      this.CommoditiesSold += day.MoneyFromDonations;
      for (int index = 0; index < this.ThingsPurchasedValue.Length; ++index)
      {
        this.ThingsPurchasedValue[index] += day.ThingsPurchasedValue[index];
        this.ThingsPurchasedTotal[index] += day.ThingsPurchasedTotal[index];
      }
    }

    public void ManufacturedAThing(int ShopUID, AnimalFoodType animalfoodtype)
    {
      if (animalfoodtype == AnimalFoodType.Count)
        return;
      ++this.ThingsManufactured[(int) animalfoodtype];
    }

    public void PlayerTookLoan(int SpentThis) => this.LoanTaken += SpentThis;

    public void PlayerSpentMoney(int SpentThis) => this.TotalMoneySpentThisDay += SpentThis;

    public void DidNotPurchasedATicket() => ++this.PeopleWhoWantedToCome;

    public void PurchasedATicket(int TicketCost)
    {
      this.MoneyFromTicketSales += TicketCost;
      ++this.PeopleWhoCame;
      this.TotalMoneyEarnedThisDay += TicketCost;
      ++this.PeopleWhoWantedToCome;
    }

    public void DayEndedPeopleLeftAtStop(BUSROUTE route, int PeopleLeftAtStop) => this.PeopleAtBusStop[(int) route] = PeopleLeftAtStop;

    public void BusDidPickUp(BUSROUTE route, int PeopleWhoGotOnBus) => this.PeopleWhoTookBusByStop[(int) route] += PeopleWhoGotOnBus;

    public void SartDay_EstimatedPeopleForRoute(BUSROUTE route, int PeopleForTheDay) => throw new Exception("NOT YSED");

    public void SomePeopleGotOnTheBusOnThisRoute(int PeopleGotOnBus, BUSROUTE route) => throw new Exception("NOT YSED");

    public void CostofIngredientsInAShop(int COGS) => this.TotalMoneySpentThisDay += COGS;

    public void RecievedGrant(int Money)
    {
      this.MoneyFromGrants += Money;
      this.TotalMoneyEarnedThisDay += Money;
    }

    public void PurchasedFromAShop(int ItemSellPrice, TILETYPE tiletype)
    {
      this.ThingsPurchasedValue[(int) tiletype] += ItemSellPrice;
      ++this.ThingsPurchasedTotal[(int) tiletype];
      if (TileData.IsForSouvenir(tiletype))
      {
        this.MoneyFromSouvenirs += ItemSellPrice;
        ++this.SouveniersSold;
      }
      else if (TileData.IsForThirst(tiletype))
      {
        ++this.DrinksSold;
        this.MoneyFromDrinks += ItemSellPrice;
      }
      else if (TileData.IsForFood(tiletype))
      {
        ++this.FoodItemsSold;
        this.MoneyFromFood += ItemSellPrice;
      }
      this.LeftOverCents += ItemSellPrice - ItemSellPrice / 100 * 100;
      this.TotalMoneyEarnedThisDay += ItemSellPrice / 100;
      if (this.LeftOverCents < 100)
        return;
      ++this.TotalMoneyEarnedThisDay;
      this.LeftOverCents -= 100;
    }

    public void ResearchedSomething() => ++this.ResearchUnlocked;

    public void SoldAnimalOnBlackMarket(int cost) => this.MonsyFromBlackMarket += cost;

    public int GetTotalOfTheseManufactured(AnimalFoodType manufacturedthing) => this.ThingsManufactured[(int) manufacturedthing];

    public DayStats(Reader reader, int VersionNumberForLoad)
    {
      int num1 = (int) reader.ReadInt("d", ref this.Day);
      int num2 = (int) reader.ReadInt("d", ref this.PeopleWhoCame);
      int num3 = (int) reader.ReadInt("d", ref this.PeopleWhoWantedToCome);
      int num4 = (int) reader.ReadInt("d", ref this.TotalMoneyEarnedThisDay);
      int num5 = (int) reader.ReadInt("d", ref this.TotalMoneySpentThisDay);
      int num6 = (int) reader.ReadInt("d", ref this.BankBalanceOnClosing);
      int num7 = (int) reader.ReadInt("d", ref this.MoneyFromTicketSales);
      int num8 = (int) reader.ReadInt("d", ref this.MoneyFromFood);
      int num9 = (int) reader.ReadInt("d", ref this.MoneyFromDrinks);
      int num10 = (int) reader.ReadInt("d", ref this.MoneyFromSouvenirs);
      int num11 = (int) reader.ReadInt("d", ref this.MonsyFromBlackMarket);
      int num12 = (int) reader.ReadInt("d", ref this.MoneyFromCommodities);
      int num13 = (int) reader.ReadInt("d", ref this.MoneyFromDonations);
      this.PeopleAtBusStop = new int[10];
      this.PeopleWhoTookBusByStop = new int[10];
      int _out1 = 0;
      int num14 = (int) reader.ReadInt("d", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        int num15 = (int) reader.ReadInt("d", ref this.PeopleAtBusStop[index]);
        int num16 = (int) reader.ReadInt("d", ref this.PeopleWhoTookBusByStop[index]);
      }
      if (VersionNumberForLoad > 17)
      {
        int num15 = (int) reader.ReadInt("d", ref this.FoodItemsSold);
        int num16 = (int) reader.ReadInt("d", ref this.DrinksSold);
        int num17 = (int) reader.ReadInt("d", ref this.SouveniersSold);
        int num18 = (int) reader.ReadInt("d", ref this.CommoditiesSold);
      }
      if (VersionNumberForLoad > 22)
      {
        int num15 = (int) reader.ReadInt("d", ref this.LoanTaken);
        int num16 = (int) reader.ReadInt("d", ref this.LoanOnClosing);
        this.ThingsPurchasedValue = new int[743];
        this.ThingsPurchasedTotal = new int[743];
        int num17 = (int) reader.ReadInt("d", ref _out1);
        for (int index = 0; index < _out1; ++index)
        {
          int num18 = (int) reader.ReadInt("d", ref this.ThingsPurchasedValue[index]);
          int num19 = (int) reader.ReadInt("d", ref this.ThingsPurchasedTotal[index]);
        }
      }
      else
      {
        this.ThingsPurchasedValue = new int[743];
        this.ThingsPurchasedTotal = new int[743];
      }
      if (VersionNumberForLoad > 38)
      {
        this.ThingsManufactured = new int[88];
        int num15 = (int) reader.ReadInt("d", ref _out1);
        int _out2 = 0;
        for (int index = 0; index < _out1; ++index)
        {
          int num16 = (int) reader.ReadInt("d", ref _out2);
          this.ThingsManufactured[index] = _out2;
        }
        int num17 = (int) reader.ReadInt("d", ref this.MoneyFromGrants);
      }
      else
        this.ThingsManufactured = new int[88];
      if (VersionNumberForLoad <= 42)
        return;
      int num20 = (int) reader.ReadInt("d", ref this.ResearchUnlocked);
    }

    public void SaveDayStats(Writer writer)
    {
      writer.WriteInt("d", this.Day);
      writer.WriteInt("d", this.PeopleWhoCame);
      writer.WriteInt("d", this.PeopleWhoWantedToCome);
      writer.WriteInt("d", this.TotalMoneyEarnedThisDay);
      writer.WriteInt("d", this.TotalMoneySpentThisDay);
      writer.WriteInt("d", this.BankBalanceOnClosing);
      writer.WriteInt("d", this.MoneyFromTicketSales);
      writer.WriteInt("d", this.MoneyFromFood);
      writer.WriteInt("d", this.MoneyFromDrinks);
      writer.WriteInt("d", this.MoneyFromSouvenirs);
      writer.WriteInt("d", this.MonsyFromBlackMarket);
      writer.WriteInt("d", this.MoneyFromCommodities);
      writer.WriteInt("d", this.MoneyFromDonations);
      writer.WriteInt("d", this.PeopleAtBusStop.Length);
      for (int index = 0; index < this.PeopleAtBusStop.Length; ++index)
      {
        writer.WriteInt("d", this.PeopleAtBusStop[index]);
        writer.WriteInt("d", this.PeopleWhoTookBusByStop[index]);
      }
      writer.WriteInt("d", this.FoodItemsSold);
      writer.WriteInt("d", this.DrinksSold);
      writer.WriteInt("d", this.SouveniersSold);
      writer.WriteInt("d", this.CommoditiesSold);
      writer.WriteInt("d", this.LoanTaken);
      writer.WriteInt("d", this.LoanOnClosing);
      writer.WriteInt("d", this.ThingsPurchasedValue.Length);
      for (int index = 0; index < this.ThingsPurchasedValue.Length; ++index)
      {
        writer.WriteInt("d", this.ThingsPurchasedValue[index]);
        writer.WriteInt("d", this.ThingsPurchasedTotal[index]);
      }
      writer.WriteInt("d", this.ThingsManufactured.Length);
      for (int index = 0; index < this.ThingsManufactured.Length; ++index)
        writer.WriteInt("d", this.ThingsManufactured[index]);
      writer.WriteInt("d", this.MoneyFromGrants);
      writer.WriteInt("d", this.ResearchUnlocked);
    }
  }
}
