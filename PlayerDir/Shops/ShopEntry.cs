// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_BalanceSystems.Customers;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_Employees;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopEntry
  {
    public ShopEntryType shoptype;
    public int ShopUID;
    public Vector2Int LocationOfThisShop;
    public TILETYPE tiletype;
    public List<ShopStockStatus> shopstockstatus;
    public float SeasoningValue;
    public List<MemberOfThePublic> people_usingShop;
    public List<MemberOfThePublic> people_walkingTo_SHOP;
    public int MaxPeopleCanUseShopAtOnce = 1;
    private List<Employee> employeeshere;
    private int emplyeeIndex;
    private int ThisShopsInternalQueueLength;
    public FactoryProductionLine factoryproduction;
    public int GarbageInThisBin;
    public bool IsBin;
    public DirectionPressed DirectionCustomerWillFaceUsingAShop = DirectionPressed.None;

    public ShopEntry(
      Vector2Int _Location,
      TILETYPE thingYouBuilt,
      bool IsMove,
      int OverrideCapacity = -1,
      int _RotationClockWise = -1)
    {
      this.SetDirection(_RotationClockWise);
      ParkRating.NeedsRecalculating = true;
      this.employeeshere = new List<Employee>();
      this.MaxPeopleCanUseShopAtOnce = OverrideCapacity <= -1 ? ShopData.GetServingCapacity(thingYouBuilt) : OverrideCapacity;
      this.people_usingShop = new List<MemberOfThePublic>();
      this.people_walkingTo_SHOP = new List<MemberOfThePublic>();
      this.ShopUID = IsMove ? -1 : Z_GameFlags.GetShopUID();
      if (TileData.IsAFactory(thingYouBuilt) || TileData.IsAMeatProcessingPlant(thingYouBuilt) || (TileData.IsAVegetableProcessingPlant(thingYouBuilt) || TileData.IsAnIncinerator(thingYouBuilt)))
        this.factoryproduction = new FactoryProductionLine(thingYouBuilt, this.ShopUID);
      if (_Location == null)
        throw new Exception("iusfe");
      this.tiletype = thingYouBuilt;
      this.LocationOfThisShop = new Vector2Int(_Location);
      ShopStatsCollection shopInfo = ShopData.GetShopInfo(thingYouBuilt);
      this.shopstockstatus = new List<ShopStockStatus>();
      if (shopInfo != null)
      {
        for (int index = 0; index < shopInfo.shopstats.Count; ++index)
          this.shopstockstatus.Add(new ShopStockStatus(shopInfo.shopstats[index]));
        this.shopstockstatus[0].Unlocked = true;
        this.shopstockstatus[0].StartNewDay((int) Player.financialrecords.GetDaysPassed(), this.shopstockstatus[0].GetCurrentPrice());
      }
      this.SetShopType();
    }

    public void AddSmokeController(FactorySmoke smoke) => this.factoryproduction.AddSmokeController(smoke);

    private void SetDirection(int _RotationClockWise)
    {
      switch (_RotationClockWise)
      {
        case 0:
          this.DirectionCustomerWillFaceUsingAShop = DirectionPressed.Up;
          break;
        case 1:
          this.DirectionCustomerWillFaceUsingAShop = DirectionPressed.Right;
          break;
        case 2:
          this.DirectionCustomerWillFaceUsingAShop = DirectionPressed.Down;
          break;
        case 3:
          this.DirectionCustomerWillFaceUsingAShop = DirectionPressed.Left;
          break;
      }
    }

    public void AddGarbageToBin(int Garbage) => this.GarbageInThisBin += Garbage;

    private void SetShopType()
    {
      this.shoptype = ShopEntryType.Shop;
      if (TileData.IsThisABench(this.tiletype))
        this.shoptype = ShopEntryType.Bench;
      else if (TileData.IsThisABin(this.tiletype))
        this.shoptype = ShopEntryType.Bin;
      else if (TileData.IsThisAnATM(this.tiletype))
        this.shoptype = ShopEntryType.ATM;
      else if (TileData.IsThisaToilet(this.tiletype))
      {
        this.ThisShopsInternalQueueLength = 2;
        this.shoptype = ShopEntryType.Toilet;
        this.MaxPeopleCanUseShopAtOnce = LiveStats.ToiletMaxCapacity;
      }
      this.IsBin = TileData.IsThisABin(this.tiletype);
    }

    public bool HasSpaceToBeServed() => this.people_usingShop.Count < this.MaxPeopleCanUseShopAtOnce;

    public int GetEmplyeeCount() => this.employeeshere.Count;

    public List<Employee> GetListOfEmployeesHere() => this.employeeshere;

    public bool RemoveThisEmployee(Employee FiredEmployee)
    {
      if (FiredEmployee.quickemplyeedescription != null)
      {
        for (int index = this.employeeshere.Count - 1; index > -1; --index)
        {
          if (this.employeeshere[index].quickemplyeedescription == FiredEmployee.quickemplyeedescription)
          {
            this.employeeshere.RemoveAt(index);
            return true;
          }
        }
      }
      return false;
    }

    public void ScrubMaxCapacities()
    {
      if (this.shoptype != ShopEntryType.Toilet)
        return;
      this.MaxPeopleCanUseShopAtOnce = LiveStats.ToiletMaxCapacity;
    }

    public void OnDestroyThisShop(Player player, bool IsMove)
    {
      if (!IsMove)
      {
        for (int index = 0; index < this.employeeshere.Count; ++index)
          this.employeeshere[index].quickemplyeedescription.FireThisEmployee = true;
        if (this.employeeshere.Count > 0)
        {
          OverWorldManager.overworldstate = OverWOrldState.FireEmployees;
          OverWorldManager.firingmanager = new FiringManager(this.employeeshere.Count, false);
        }
        if (player.shopstatus.GetTotalOfThese(this.tiletype) == 1)
        {
          Console.WriteLine("LAST SHOP OF THIS TYPE BEING DESTROYED");
          player.employees.openPositionsContainer.RemoveThisOpenPosition(this.tiletype);
        }
      }
      for (int index = 0; index < this.people_walkingTo_SHOP.Count; ++index)
        this.people_walkingTo_SHOP[index].JustMovedorSoldShop = true;
      for (int index = 0; index < this.people_usingShop.Count; ++index)
        this.people_usingShop[index].JustMovedorSoldShop = true;
    }

    public void OnThisShopMoveComplete(Vector2Int Loction)
    {
      for (int index = 0; index < this.employeeshere.Count; ++index)
        this.employeeshere[index].quickemplyeedescription.JustMovedShop = true;
      for (int index = 0; index < this.people_walkingTo_SHOP.Count; ++index)
        this.people_walkingTo_SHOP[index].JustMovedorSoldShop = true;
      for (int index = 0; index < this.people_usingShop.Count; ++index)
        this.people_usingShop[index].JustMovedorSoldShop = true;
      this.people_walkingTo_SHOP = new List<MemberOfThePublic>();
      this.people_usingShop = new List<MemberOfThePublic>();
    }

    public void CloneOnMove(ShopEntry orginal)
    {
      this.ShopUID = orginal.ShopUID;
      this.people_usingShop = orginal.people_usingShop;
      this.people_walkingTo_SHOP = orginal.people_walkingTo_SHOP;
      this.employeeshere = orginal.employeeshere;
      this.MaxPeopleCanUseShopAtOnce = orginal.MaxPeopleCanUseShopAtOnce;
      this.shopstockstatus = orginal.shopstockstatus;
    }

    public void AddEmployee(Employee employee) => this.employeeshere.Add(employee);

    public void StartNewDay(float SecondsZooWasOpen)
    {
      for (int index = 0; index < this.employeeshere.Count; ++index)
        this.employeeshere[index].quickemplyeedescription.StartNewDay(SecondsZooWasOpen, this.employeeshere[index]);
    }

    public bool TryToStartBeingServed(MemberOfThePublic person, ref float _ServingTime)
    {
      if (!this.people_usingShop.Contains(person))
        return false;
      if (this.employeeshere.Count > 0)
      {
        _ServingTime = this.employeeshere[this.emplyeeIndex].quickemplyeedescription.GetServingTimeAndServe();
        ++this.emplyeeIndex;
        if (this.emplyeeIndex >= this.employeeshere.Count)
          this.emplyeeIndex = 0;
      }
      else
      {
        if (this.people_usingShop.Count > this.MaxPeopleCanUseShopAtOnce)
          return false;
        _ServingTime = this.shoptype != ShopEntryType.Toilet ? (this.shoptype != ShopEntryType.Bin ? (this.shoptype != ShopEntryType.Bench ? (float) TinyZoo.Game1.Rnd.Next(3, 15) : 10f) : 1f) : 10f;
      }
      person.CurrentQuestForText = CustomerQuest.IsBeingServedAtShop;
      return true;
    }

    public bool IsSomeoneInfrontOfMe(
      int MyQueueNumber,
      int NextX,
      int NextY,
      MemberOfThePublic me)
    {
      int pathLength = me.refpathnavigator.GetPathLength();
      for (int index = 0; index < this.people_walkingTo_SHOP.Count; ++index)
      {
        if (this.people_walkingTo_SHOP[index].QueueNumber < MyQueueNumber && this.people_walkingTo_SHOP[index].refpathnavigator.CurrentPathCrossesThis(NextX, NextY))
          return true;
      }
      if (this.people_usingShop.Count >= this.MaxPeopleCanUseShopAtOnce && (me.refpathnavigator.GetPathLength() <= MyQueueNumber + this.ThisShopsInternalQueueLength || me.refpathnavigator.GetPathLength() < 1) || this.people_usingShop.Count >= this.MaxPeopleCanUseShopAtOnce && pathLength < 2)
        return true;
      int canUseShopAtOnce = this.MaxPeopleCanUseShopAtOnce;
      for (int index = 0; index < this.people_usingShop.Count; ++index)
      {
        if (this.people_usingShop[index].QueueNumber < MyQueueNumber && this.people_usingShop[index].refpathnavigator.CurrentTile.X == NextX && this.people_usingShop[index].refpathnavigator.CurrentTile.Y == NextY)
        {
          --canUseShopAtOnce;
          if (canUseShopAtOnce <= 0)
            return true;
        }
      }
      return false;
    }

    public void AddPersonToUseShopList(MemberOfThePublic memberofthepublic)
    {
      memberofthepublic.CurrentQuestForText = CustomerQuest.InQueueForShop;
      this.RemoveMeFromQueue(memberofthepublic);
      this.people_usingShop.Add(memberofthepublic);
    }

    public void RemoveMeFromQueue(MemberOfThePublic memberofthepublic)
    {
      this.people_walkingTo_SHOP.Remove(memberofthepublic);
      for (int index = 0; index < this.people_walkingTo_SHOP.Count; ++index)
        --this.people_walkingTo_SHOP[index].QueueNumber;
    }

    public void RemoveMeFromAllListsOnReset(MemberOfThePublic memberofthepublic)
    {
      if (this.people_usingShop.Contains(memberofthepublic))
        this.people_usingShop.Remove(memberofthepublic);
      if (!this.people_walkingTo_SHOP.Contains(memberofthepublic))
        return;
      this.people_walkingTo_SHOP.Remove(memberofthepublic);
      for (int index = 0; index < this.people_walkingTo_SHOP.Count; ++index)
        --this.people_walkingTo_SHOP[index].QueueNumber;
    }

    public void LeftQueueWithoutPaying()
    {
      if (this.shopstockstatus.Count <= 0)
        return;
      this.shopstockstatus[0].LeftQueueWithoutPaying();
    }

    public void AddPersonOnWayToShop(MemberOfThePublic memberofthepublic)
    {
      this.people_walkingTo_SHOP.Add(memberofthepublic);
      memberofthepublic.QueueNumber = this.people_walkingTo_SHOP.Count;
    }

    public bool TryAndUseThisShop(
      ref int CashHeld,
      CustomerLedger purchaseledger,
      GeneralWellbeing wellbeing,
      Player player,
      Vector2 vLocationOfPerson,
      MemberOfThePublic memberofthepublic)
    {
      if (this.shopstockstatus.Count > 0)
      {
        if (this.shopstockstatus[0].REF_shopentry == null)
        {
          ShopStatsCollection shopInfo = ShopData.GetShopInfo(this.tiletype);
          for (int index = 0; index < this.shopstockstatus.Count; ++index)
            this.shopstockstatus[index].REF_shopentry = shopInfo.shopstats[index];
        }
        int num = 0;
        int index1 = -1;
        for (int index2 = 0; index2 < this.shopstockstatus.Count; ++index2)
        {
          if (this.shopstockstatus[index2].Unlocked && this.shopstockstatus[index2].GetCurrentPrice() <= CashHeld && this.shopstockstatus[index2].CurrentStock > 0)
          {
            int desirability = this.shopstockstatus[index2].GetDesirability(wellbeing, memberofthepublic.customerneeds);
            if (desirability > num)
            {
              index1 = index2;
              num = desirability;
            }
          }
        }
        if (index1 > -1 && num >= wellbeing.Thriftiness)
        {
          CashHeld -= this.shopstockstatus[index1].GetCurrentPrice() * 10;
          MoneyRenderer.EarnMoney(vLocationOfPerson - new Vector2(0.0f, 16f), this.shopstockstatus[index1].GetCurrentPrice() * 10, true);
          memberofthepublic.BuyThing(this.shopstockstatus[index1].GetCurrentPrice() * 10, this.shopstockstatus[index1].REF_shopentry.MainItemForSale);
          Player.financialrecords.CustomerPurchasedFromAShop(this.shopstockstatus[index1].GetCurrentPrice() * 10, this.tiletype);
          if (this.shopstockstatus[index1].StockPrice == -1)
            this.shopstockstatus[index1].SetStockPrice();
          Player.financialrecords.PlayerSpentOnIngredientsInAShop(this.shopstockstatus[index1].StockPrice);
          UseShop.PurchasedSomething(this.shopstockstatus[index1], memberofthepublic, this);
          this.shopstockstatus[index1].AddNewPurchaseToLedger();
          memberofthepublic.CompletedUsingShop();
          memberofthepublic.ShopsUsedUID.Add(this.ShopUID);
          return true;
        }
        memberofthepublic.ShopsUsedUID.Add(this.ShopUID);
        memberofthepublic.CompletedUsingShop();
      }
      return false;
    }

    public void AddStockToThisFactory(int STock)
    {
      if (this.factoryproduction == null)
        return;
      this.factoryproduction.AddStock(STock);
    }

    public void StartNewDay()
    {
      if (this.factoryproduction != null)
        this.factoryproduction.StartNewDay(this.ShopUID);
      if (this.people_walkingTo_SHOP.Count <= 0)
        return;
      this.people_walkingTo_SHOP = new List<MemberOfThePublic>();
    }

    public void SaveShopEntry(Writer writer)
    {
      this.LocationOfThisShop.SaveVector2Int(writer);
      writer.WriteInt("s", (int) this.tiletype);
      writer.WriteInt("s", this.shopstockstatus.Count);
      for (int index = 0; index < this.shopstockstatus.Count; ++index)
        this.shopstockstatus[index].SaveShopStockStatus(writer);
      writer.WriteInt("s", this.ShopUID);
      writer.WriteBool("s", this.factoryproduction != null);
      if (this.factoryproduction != null)
        this.factoryproduction.SaveFactoryProductionLine(writer);
      if (!TileData.IsThisABin(this.tiletype))
        return;
      writer.WriteInt("s", this.GarbageInThisBin);
    }

    public ShopEntry(Reader reader, int VersionNumberForLoad, Player player)
    {
      this.people_usingShop = new List<MemberOfThePublic>();
      this.people_walkingTo_SHOP = new List<MemberOfThePublic>();
      this.LocationOfThisShop = new Vector2Int(reader);
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("s", ref _out1);
      this.tiletype = (TILETYPE) _out1;
      this.shopstockstatus = new List<ShopStockStatus>();
      int num2 = (int) reader.ReadInt("s", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.shopstockstatus.Add(new ShopStockStatus(reader, VersionNumberForLoad));
      int num3 = (int) reader.ReadInt("s", ref this.ShopUID);
      this.employeeshere = new List<Employee>();
      this.SetShopType();
      if (VersionNumberForLoad > 10)
      {
        bool _out2 = false;
        int num4 = (int) reader.ReadBool("s", ref _out2);
        if (_out2)
          this.factoryproduction = new FactoryProductionLine(reader, this.tiletype, this.ShopUID, VersionNumberForLoad);
      }
      else if (TileData.IsAFactory(this.tiletype) || TileData.IsAMeatProcessingPlant(this.tiletype) || (TileData.IsAVegetableProcessingPlant(this.tiletype) || TileData.IsAnIncinerator(this.tiletype)))
        this.factoryproduction = new FactoryProductionLine(this.tiletype, this.ShopUID);
      if (VersionNumberForLoad > 13 && TileData.IsThisABin(this.tiletype))
      {
        this.IsBin = true;
        int num4 = (int) reader.ReadInt("s", ref this.GarbageInThisBin);
      }
      this.SetDirection(player.prisonlayout.layout.BaseTileTypes[this.LocationOfThisShop.X, this.LocationOfThisShop.Y].RotationClockWise);
    }
  }
}
