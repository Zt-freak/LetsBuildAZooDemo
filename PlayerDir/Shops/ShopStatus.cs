// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.PlayerDir.StoreRooms;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopStatus
  {
    public List<ShopEntry> shopentries;
    public List<ShopEntry> Toilets;
    public List<ShopEntry> Bins;
    public List<ShopEntry> Benches;
    public List<ShopEntry> ArchitectOffice;
    public List<ShopEntry> FacilitiesWithEmployees;
    public List<ShopEntry> ATMS;
    private static ShopEntry LastSoldBuilding;

    public ShopStatus()
    {
      this.shopentries = new List<ShopEntry>();
      this.Toilets = new List<ShopEntry>();
      this.Bins = new List<ShopEntry>();
      this.Benches = new List<ShopEntry>();
      this.ArchitectOffice = new List<ShopEntry>();
      this.FacilitiesWithEmployees = new List<ShopEntry>();
      this.ATMS = new List<ShopEntry>();
    }

    public bool HasThisFacility(TILETYPE tiletype)
    {
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].tiletype == tiletype)
          return true;
      }
      return false;
    }

    public ShopEntry TryAndFindThisShop(Vector2Int _CenterBottomTile, TILETYPE tiletype)
    {
      if (TileData.IsThisAShopWithShopStats(tiletype))
      {
        for (int index = 0; index < this.shopentries.Count; ++index)
        {
          if (this.shopentries[index].LocationOfThisShop.CompareMatches(_CenterBottomTile))
            return this.shopentries[index];
        }
      }
      else if (TileData.IsThisanArchitectOffice(tiletype))
      {
        for (int index = 0; index < this.ArchitectOffice.Count; ++index)
        {
          if (this.ArchitectOffice[index].LocationOfThisShop.CompareMatches(_CenterBottomTile))
            return this.ArchitectOffice[index];
        }
      }
      else
      {
        if (TileData.IsAStoreRoom(tiletype))
          return (ShopEntry) null;
        if (TileData.IsThisAFacility(tiletype))
        {
          for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
          {
            if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(_CenterBottomTile))
              return this.FacilitiesWithEmployees[index];
          }
        }
      }
      return (ShopEntry) null;
    }

    public int GetTotalOfThisFacility(TILETYPE tiletype)
    {
      int num = 0;
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].tiletype == tiletype)
          ++num;
      }
      return num;
    }

    public int GetTotalOfThese(TILETYPE tiletype)
    {
      int num = 0;
      for (int index = 0; index < this.shopentries.Count; ++index)
      {
        if (this.shopentries[index].tiletype == tiletype)
          ++num;
      }
      for (int index = 0; index < this.ArchitectOffice.Count; ++index)
      {
        if (this.ArchitectOffice[index].tiletype == tiletype)
          ++num;
      }
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].tiletype == tiletype)
          ++num;
      }
      for (int index = 0; index < this.Bins.Count; ++index)
      {
        if (this.Bins[index].tiletype == tiletype)
          ++num;
      }
      return num;
    }

    public bool RemoveThisEmployee(Employee FiredEmployee)
    {
      TILETYPE worksHere = FiredEmployee.quickemplyeedescription.WorksHere;
      if (worksHere == TILETYPE.Logo)
        return true;
      if (TileData.IsAnArchitectOffice(worksHere))
      {
        for (int index = 0; index < this.ArchitectOffice.Count; ++index)
        {
          if (this.ArchitectOffice[index].RemoveThisEmployee(FiredEmployee))
            return true;
        }
      }
      else if (TileData.IsThisAShopWithShopStats(worksHere))
      {
        for (int index = 0; index < this.shopentries.Count; ++index)
        {
          if (this.shopentries[index].RemoveThisEmployee(FiredEmployee))
            return true;
        }
      }
      else
      {
        for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
        {
          if (this.FacilitiesWithEmployees[index].RemoveThisEmployee(FiredEmployee))
            return true;
        }
      }
      return false;
    }

    public void BuiltABuilding(
      Vector2Int Location,
      TILETYPE thingYouBuilt,
      int RotationClockWise,
      Player player,
      bool IsMove,
      out int Shop_UID,
      int ForceThisShopUID = -1)
    {
      Shop_UID = -1;
      Z_GameFlags.pathfinder.AddNode(thingYouBuilt, Location.X, Location.Y, RotationClockWise);
      if (IsMove && TileData.IsAManagementOffice(thingYouBuilt))
        PointOffScreenManager.SetTaskPointerLocation(Location.X, Location.Y);
      if (TileData.IsAStoreRoom(thingYouBuilt))
        player.storerooms.AddStoreRoom(new StoreRoomContents(), Location);
      else if (TileData.IsThisABin(thingYouBuilt))
      {
        this.Bins.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 0));
        if (IsMove)
          this.Bins[this.Bins.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          this.Bins[this.Bins.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.Bins[this.Bins.Count - 1].ShopUID;
      }
      else if (TileData.IsThisAFacility(thingYouBuilt))
      {
        this.FacilitiesWithEmployees.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1]);
        if (IsMove)
        {
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
          player.animalProcessing.MoveBuilding_NotNeededUnlessWeAdLocation(ShopStatus.LastSoldBuilding.LocationOfThisShop, Location);
        }
        if (ForceThisShopUID > -1)
        {
          Shop_UID = ForceThisShopUID;
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ForceThisShopUID;
        }
        else
          Shop_UID = this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID;
        if (IsMove)
          return;
        if (TileData.IsAQuarantineBuilding(thingYouBuilt))
          player.animalquarantine.AddQuaratineBuilding(Shop_UID);
        else if (TileData.IsAnIncinerator(thingYouBuilt))
          player.animalincineration.AddIncinerationBuilding(Shop_UID);
        else
          player.animalProcessing.AddMeatProcessorBuilding(Shop_UID);
      }
      else if (TileData.IsAFactory(thingYouBuilt))
      {
        this.FacilitiesWithEmployees.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1]);
        if (IsMove)
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          Shop_UID = ForceThisShopUID;
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ForceThisShopUID;
        }
        else
          Shop_UID = this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID;
      }
      else if (TileData.IsABreedingRoom(thingYouBuilt))
      {
        if (IsMove)
          player.breeds.MoveBuilding(ShopStatus.LastSoldBuilding.LocationOfThisShop, Location);
        else
          player.breeds.AddNurseryBuilding(Location);
        this.FacilitiesWithEmployees.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1]);
        if (IsMove)
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID;
      }
      else if (TileData.IsACRISPRBuilding(thingYouBuilt))
      {
        if (IsMove)
          player.crisprBreeds.MoveCRISPRBuilding(ShopStatus.LastSoldBuilding.LocationOfThisShop, Location);
        else
          player.crisprBreeds.AddNewCRISPRBuilding(Location);
        this.FacilitiesWithEmployees.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1]);
        if (IsMove)
        {
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].CloneOnMove(ShopStatus.LastSoldBuilding);
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].OnThisShopMoveComplete(Location);
        }
        if (ForceThisShopUID > -1)
        {
          this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.FacilitiesWithEmployees[this.FacilitiesWithEmployees.Count - 1].ShopUID;
      }
      else if (TileData.IsThisanArchitectOffice(thingYouBuilt))
      {
        this.ArchitectOffice.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.ArchitectOffice[this.ArchitectOffice.Count - 1]);
        if (IsMove)
        {
          this.ArchitectOffice[this.ArchitectOffice.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
          this.ArchitectOffice[this.ArchitectOffice.Count - 1].CloneOnMove(ShopStatus.LastSoldBuilding);
          this.ArchitectOffice[this.ArchitectOffice.Count - 1].OnThisShopMoveComplete(Location);
        }
        if (ForceThisShopUID > -1)
        {
          this.ArchitectOffice[this.ArchitectOffice.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.ArchitectOffice[this.ArchitectOffice.Count - 1].ShopUID;
      }
      else if (TileData.IsThisaToilet(thingYouBuilt))
      {
        this.Toilets.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 2, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.Toilets[this.Toilets.Count - 1]);
        if (IsMove)
          this.Toilets[this.Toilets.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          this.Toilets[this.Toilets.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.Toilets[this.Toilets.Count - 1].ShopUID;
      }
      else if (TileData.IsThisAnATM(thingYouBuilt))
      {
        MechanicController.ResetMechanicStructures = true;
        this.ATMS.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 1, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.ATMS[this.ATMS.Count - 1]);
        if (IsMove)
          this.ATMS[this.ATMS.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          this.ATMS[this.ATMS.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.ATMS[this.ATMS.Count - 1].ShopUID;
      }
      else if (TileData.IsThisAnyKindOfDeco(thingYouBuilt) || TileData.DoesThisImapactPublicty(thingYouBuilt))
      {
        int num1 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
        DecoCalculator.AddOrRemovedDeco(Location);
        if (TileData.DoesThisImapactPublicty(thingYouBuilt))
        {
          int publicity1 = PublicityCalculator.CalculatePublicity(player);
          ParkRating.NeedsRecalculating = true;
          PublicityCalculator.RecalculatePublicity = true;
          double deco = (double) DecoCalculator.CalculateDeco(player);
          int publicity2 = PublicityCalculator.CalculatePublicity(player);
          NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
        }
        else
        {
          int num2 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
          NotificationBubbleManager.QuickAddNotification((float) num1, (float) num2, BubbleMainType.Deco);
        }
      }
      else if (TileData.IsThisABench(thingYouBuilt))
      {
        this.Benches.Add(new ShopEntry(Location, thingYouBuilt, IsMove, 1, RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.Benches[this.Benches.Count - 1]);
        if (IsMove)
          this.Benches[this.Benches.Count - 1].ShopUID = ShopStatus.LastSoldBuilding.ShopUID;
        if (ForceThisShopUID > -1)
        {
          this.Benches[this.Benches.Count - 1].ShopUID = ForceThisShopUID;
          Shop_UID = ForceThisShopUID;
        }
        else
          Shop_UID = this.Benches[this.Benches.Count - 1].ShopUID;
      }
      else if (TileData.IsThisAnyKindOfDeco(thingYouBuilt) || TileData.DoesThisImapactPublicty(thingYouBuilt))
      {
        int num1 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
        DecoCalculator.AddOrRemovedDeco(Location);
        if (TileData.DoesThisImapactPublicty(thingYouBuilt))
        {
          int publicity1 = PublicityCalculator.CalculatePublicity(player);
          ParkRating.NeedsRecalculating = true;
          PublicityCalculator.RecalculatePublicity = true;
          double deco = (double) DecoCalculator.CalculateDeco(player);
          int publicity2 = PublicityCalculator.CalculatePublicity(player);
          NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
        }
        else
        {
          int num2 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
          NotificationBubbleManager.QuickAddNotification((float) num1, (float) num2, BubbleMainType.Deco);
        }
      }
      else
      {
        if (!TileData.IsThisAShopWithShopStats(thingYouBuilt) && !TileData.IsThisaToilet(thingYouBuilt) && !TileData.HasAirVehicle(thingYouBuilt))
          return;
        int num = TileData.HasAirVehicle(thingYouBuilt) ? 1 : 0;
        this.shopentries.Add(new ShopEntry(Location, thingYouBuilt, IsMove, _RotationClockWise: RotationClockWise));
        ShopNavigation.AddShop(Location, thingYouBuilt, RotationClockWise, this.shopentries[this.shopentries.Count - 1]);
        if (IsMove)
        {
          this.shopentries[this.shopentries.Count - 1].CloneOnMove(ShopStatus.LastSoldBuilding);
          this.shopentries[this.shopentries.Count - 1].OnThisShopMoveComplete(Location);
        }
        if (ForceThisShopUID > -1)
        {
          Shop_UID = ForceThisShopUID;
          this.shopentries[this.shopentries.Count - 1].ShopUID = ForceThisShopUID;
        }
        else
          Shop_UID = this.shopentries[this.shopentries.Count - 1].ShopUID;
        if (num == 0)
          return;
        OverWorldEnvironmentManager.airspacemanager.AddBuildingWithRide(Location, thingYouBuilt, RotationClockWise, IsMove, Shop_UID);
      }
    }

    public void ReplaceThisShopOnMove(
      Vector2Int TileLocation,
      TILETYPE tiletypeonconstruct,
      ShopEntry shopentry)
    {
      if (ShopStatus.LastSoldBuilding != null)
      {
        if (ShopStatus.LastSoldBuilding.tiletype != tiletypeonconstruct)
          throw new Exception("DOES NOT MATCH");
        this.shopentries.Add(ShopStatus.LastSoldBuilding);
        ShopStatus.LastSoldBuilding.LocationOfThisShop = new Vector2Int(TileLocation);
        ShopStatus.LastSoldBuilding = (ShopEntry) null;
      }
      else
      {
        ShopStatus.LastSoldBuilding = (ShopEntry) null;
        for (int index = 0; index < this.shopentries.Count; ++index)
        {
          if (this.shopentries[index].LocationOfThisShop.CompareMatches(TileLocation))
          {
            shopentry.LocationOfThisShop = new Vector2Int(TileLocation);
            this.shopentries[index] = shopentry;
          }
        }
      }
    }

    public void UseThisBench(
      Vector2Int TargetShopRootLocation,
      Player player,
      MemberOfThePublic memberofthepublic)
    {
      for (int index = 0; index < this.Benches.Count; ++index)
      {
        if (this.Benches[index].LocationOfThisShop.CompareMatches(TargetShopRootLocation))
        {
          this.Benches[index].AddPersonToUseShopList(memberofthepublic);
          break;
        }
      }
    }

    public void UseThisToilet(
      Vector2Int TargetShopRootLocation,
      Player player,
      MemberOfThePublic memberofthepublic)
    {
      for (int index = 0; index < this.Toilets.Count; ++index)
      {
        if (this.Toilets[index].LocationOfThisShop.CompareMatches(TargetShopRootLocation))
        {
          this.Toilets[index].AddPersonToUseShopList(memberofthepublic);
          break;
        }
      }
    }

    public void EnterQueueForThisShop(
      Vector2Int TargetShopRootLocation,
      Player player,
      MemberOfThePublic memberofthepublic)
    {
      for (int index = 0; index < this.shopentries.Count; ++index)
      {
        if (this.shopentries[index].LocationOfThisShop.CompareMatches(TargetShopRootLocation))
        {
          this.shopentries[index].AddPersonToUseShopList(memberofthepublic);
          break;
        }
      }
    }

    public bool TryAndUseThisShop(
      GeneralWellbeing generalwellbeing,
      Vector2Int TargetShopRootLocation,
      ref int CashHeld,
      CustomerLedger purchaseledger,
      Player player,
      Vector2 vLocationOfPerson,
      MemberOfThePublic memberofthepublic)
    {
      for (int index = 0; index < this.shopentries.Count; ++index)
      {
        if (this.shopentries[index].LocationOfThisShop.CompareMatches(TargetShopRootLocation))
          return this.shopentries[index].TryAndUseThisShop(ref CashHeld, purchaseledger, generalwellbeing, player, vLocationOfPerson, memberofthepublic);
      }
      return false;
    }

    public bool HasShops() => this.shopentries.Count > 0;

    public void SellBuilding(
      Vector2Int Loction,
      TILETYPE thingYouBuilt,
      Player player,
      bool _IsMove)
    {
      if (TileData.IsAStoreRoom(thingYouBuilt))
        player.storerooms.RemoveStoreRoom(Loction, thingYouBuilt);
      if (TileData.IsThisanArchitectOffice(thingYouBuilt))
      {
        for (int index = this.ArchitectOffice.Count - 1; index > -1; --index)
        {
          if (this.ArchitectOffice[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.ArchitectOffice[index];
            this.ArchitectOffice.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsThisABin(thingYouBuilt))
      {
        for (int index = this.Bins.Count - 1; index > -1; --index)
        {
          if (this.Bins[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.Bins[index];
            this.Bins.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsThisAFacility(thingYouBuilt))
      {
        for (int index = this.FacilitiesWithEmployees.Count - 1; index > -1; --index)
        {
          if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(Loction))
          {
            if (!_IsMove)
            {
              if (thingYouBuilt == TILETYPE.VetOffice)
                Console.WriteLine("Vet Building Was Sold - need to fire him");
              else if (TileData.IsAnIncinerator(thingYouBuilt))
                player.animalincineration.RemoveIncinerationBuilding(this.FacilitiesWithEmployees[index].ShopUID);
              else if (TileData.IsAMeatProcessingPlant(thingYouBuilt))
                player.animalProcessing.SoldAProcessingBuilding(this.FacilitiesWithEmployees[index].ShopUID, player);
            }
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.FacilitiesWithEmployees[index];
            this.FacilitiesWithEmployees.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsABreedingRoom(thingYouBuilt) || TileData.IsACRISPRBuilding(thingYouBuilt) || TileData.IsAQuarantineBuilding(thingYouBuilt))
      {
        for (int index = this.FacilitiesWithEmployees.Count - 1; index > -1; --index)
        {
          if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(Loction))
          {
            if (!_IsMove)
            {
              if (TileData.IsABreedingRoom(thingYouBuilt))
                player.breeds.SoldABreedingChamber(this.FacilitiesWithEmployees[index].ShopUID, player);
              else if (TileData.IsACRISPRBuilding(thingYouBuilt))
                player.crisprBreeds.SoldCRIPSRBuilding(this.FacilitiesWithEmployees[index].ShopUID);
              else if (TileData.IsAQuarantineBuilding(thingYouBuilt))
                player.animalquarantine.SoldQuarantineBuilding(this.FacilitiesWithEmployees[index].ShopUID);
            }
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.FacilitiesWithEmployees[index];
            this.FacilitiesWithEmployees.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsAFactory(thingYouBuilt))
      {
        for (int index = this.FacilitiesWithEmployees.Count - 1; index > -1; --index)
        {
          if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.FacilitiesWithEmployees[index];
            this.FacilitiesWithEmployees.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsThisaToilet(thingYouBuilt))
      {
        for (int index = this.Toilets.Count - 1; index > -1; --index)
        {
          if (this.Toilets[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.Toilets[index];
            this.Toilets.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsThisABench(thingYouBuilt))
      {
        for (int index = this.Benches.Count - 1; index > -1; --index)
        {
          if (this.Benches[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.Benches[index];
            this.Benches.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
            break;
          }
        }
      }
      else if (TileData.IsThisAnATM(thingYouBuilt))
      {
        for (int index = this.ATMS.Count - 1; index > -1; --index)
        {
          if (this.ATMS[index].LocationOfThisShop.CompareMatches(Loction))
          {
            MechanicController.ResetMechanicStructures = true;
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.ATMS[index];
            this.ATMS.RemoveAt(index);
            break;
          }
        }
      }
      else if (TileData.IsThisAnyKindOfDeco(thingYouBuilt) || TileData.DoesThisImapactPublicty(thingYouBuilt))
      {
        int num1 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
        DecoCalculator.AddOrRemovedDeco(Loction);
        if (TileData.DoesThisImapactPublicty(thingYouBuilt))
        {
          int publicity1 = PublicityCalculator.CalculatePublicity(player);
          ParkRating.NeedsRecalculating = true;
          PublicityCalculator.RecalculatePublicity = true;
          double deco = (double) DecoCalculator.CalculateDeco(player);
          int publicity2 = PublicityCalculator.CalculatePublicity(player);
          NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
        }
        else
        {
          int num2 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
          NotificationBubbleManager.QuickAddNotification((float) num1, (float) num2, BubbleMainType.Deco);
        }
      }
      else
      {
        if (!TileData.IsThisAShopWithShopStats(thingYouBuilt))
          return;
        for (int index = this.shopentries.Count - 1; index > -1; --index)
        {
          if (this.shopentries[index].LocationOfThisShop.CompareMatches(Loction))
          {
            ShopNavigation.RemoveShop(Loction, thingYouBuilt);
            ShopStatus.LastSoldBuilding = this.shopentries[index];
            this.shopentries.RemoveAt(index);
            ParkRating.NeedsRecalculating = true;
          }
        }
      }
    }

    public ShopEntry GetThisFacility(int UID, bool SkipException = false)
    {
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].ShopUID == UID)
          return this.FacilitiesWithEmployees[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisFacility(Vector2Int Location, bool SkipException = false)
    {
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(Location))
          return this.FacilitiesWithEmployees[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisArchitectsOffice(int UID, bool SkipException = false)
    {
      for (int index = 0; index < this.ArchitectOffice.Count; ++index)
      {
        if (this.ArchitectOffice[index].ShopUID == UID)
          return this.ArchitectOffice[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisArchitectsOffice(Vector2Int Location, bool SkipException = false)
    {
      for (int index = 0; index < this.ArchitectOffice.Count; ++index)
      {
        if (this.ArchitectOffice[index].LocationOfThisShop.CompareMatches(Location))
          return this.ArchitectOffice[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisFacility(
      Vector2Int Location,
      TILETYPE tiletype,
      bool SkipException = false)
    {
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
      {
        if (this.FacilitiesWithEmployees[index].LocationOfThisShop.CompareMatches(Location) && this.FacilitiesWithEmployees[index].tiletype == tiletype)
          return this.FacilitiesWithEmployees[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisShop(int UID, bool SkipException = false)
    {
      for (int index = 0; index < this.shopentries.Count; ++index)
      {
        if (this.shopentries[index].ShopUID == UID)
          return this.shopentries[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisArchitectsOffice(
      Vector2Int Location,
      TILETYPE tiletype,
      bool SkipException = false)
    {
      for (int index = 0; index < this.ArchitectOffice.Count; ++index)
      {
        if (this.ArchitectOffice[index].LocationOfThisShop.CompareMatches(Location) && this.ArchitectOffice[index].tiletype == tiletype)
          return this.ArchitectOffice[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisShop(
      Vector2Int Location,
      TILETYPE tiletype,
      bool SkipException = false)
    {
      for (int index = 0; index < this.shopentries.Count; ++index)
      {
        if (this.shopentries[index].LocationOfThisShop.CompareMatches(Location) && this.shopentries[index].tiletype == tiletype)
          return this.shopentries[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public ShopEntry GetThisATM(
      Vector2Int Location,
      TILETYPE tiletype,
      bool SkipException = false)
    {
      for (int index = 0; index < this.ATMS.Count; ++index)
      {
        if (this.ATMS[index].LocationOfThisShop.CompareMatches(Location) && this.ATMS[index].tiletype == tiletype)
          return this.ATMS[index];
      }
      if (SkipException)
        return (ShopEntry) null;
      throw new Exception("YOU MADE A MISTAKE - THERE IS NO SHOP HERE MATCHING THAT DESCRIPTON!");
    }

    public void SaveShopStatus(Writer writer)
    {
      writer.WriteInt("s", this.shopentries.Count);
      for (int index = 0; index < this.shopentries.Count; ++index)
        this.shopentries[index].SaveShopEntry(writer);
      writer.WriteInt("s", Z_GameFlags.ShopID);
      writer.WriteInt("s", this.Toilets.Count);
      for (int index = 0; index < this.Toilets.Count; ++index)
        this.Toilets[index].SaveShopEntry(writer);
      writer.WriteInt("s", this.Benches.Count);
      for (int index = 0; index < this.Benches.Count; ++index)
        this.Benches[index].SaveShopEntry(writer);
      writer.WriteInt("s", this.ArchitectOffice.Count);
      for (int index = 0; index < this.ArchitectOffice.Count; ++index)
        this.ArchitectOffice[index].SaveShopEntry(writer);
      writer.WriteInt("s", this.FacilitiesWithEmployees.Count);
      for (int index = 0; index < this.FacilitiesWithEmployees.Count; ++index)
        this.FacilitiesWithEmployees[index].SaveShopEntry(writer);
      writer.WriteInt("s", this.Bins.Count);
      for (int index = 0; index < this.Bins.Count; ++index)
        this.Bins[index].SaveShopEntry(writer);
      writer.WriteInt("s", this.ATMS.Count);
      for (int index = 0; index < this.ATMS.Count; ++index)
        this.ATMS[index].SaveShopEntry(writer);
    }

    public void populatefromemployees(Employees employees)
    {
      for (int index1 = 0; index1 < employees.employees.Count; ++index1)
      {
        if (employees.employees[index1].quickemplyeedescription != null && employees.employees[index1].quickemplyeedescription.WorksHere != TILETYPE.Logo)
        {
          if (TileData.IsAnArchitectOffice(employees.employees[index1].quickemplyeedescription.WorksHere))
          {
            for (int index2 = 0; index2 < this.ArchitectOffice.Count; ++index2)
            {
              if (this.ArchitectOffice[index2].ShopUID == employees.employees[index1].quickemplyeedescription.ShopUID)
                this.ArchitectOffice[index2].AddEmployee(employees.employees[index1]);
            }
          }
          else if (TileData.IsThisAShopWithShopStats(employees.employees[index1].quickemplyeedescription.WorksHere))
          {
            for (int index2 = 0; index2 < this.shopentries.Count; ++index2)
            {
              if (this.shopentries[index2].ShopUID == employees.employees[index1].quickemplyeedescription.ShopUID)
                this.shopentries[index2].AddEmployee(employees.employees[index1]);
            }
          }
          else
          {
            for (int index2 = 0; index2 < this.FacilitiesWithEmployees.Count; ++index2)
            {
              if (this.FacilitiesWithEmployees[index2].ShopUID == employees.employees[index1].quickemplyeedescription.ShopUID)
                this.FacilitiesWithEmployees[index2].AddEmployee(employees.employees[index1]);
            }
          }
        }
      }
    }

    public ShopStatus(Reader reader, int VersionNumberForLoad, Player player)
    {
      this.shopentries = new List<ShopEntry>();
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("s", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.shopentries.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      int num2 = (int) reader.ReadInt("s", ref Z_GameFlags.ShopID);
      this.Toilets = new List<ShopEntry>();
      int _out2 = 0;
      int num3 = (int) reader.ReadInt("s", ref _out2);
      for (int index = 0; index < _out2; ++index)
        this.Toilets.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      this.Benches = new List<ShopEntry>();
      int _out3 = 0;
      int num4 = (int) reader.ReadInt("s", ref _out3);
      for (int index = 0; index < _out3; ++index)
        this.Benches.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      this.ArchitectOffice = new List<ShopEntry>();
      int _out4 = 0;
      int num5 = (int) reader.ReadInt("s", ref _out4);
      for (int index = 0; index < _out4; ++index)
        this.ArchitectOffice.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      this.FacilitiesWithEmployees = new List<ShopEntry>();
      int _out5 = 0;
      int num6 = (int) reader.ReadInt("s", ref _out5);
      for (int index = 0; index < _out5; ++index)
        this.FacilitiesWithEmployees.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      this.Bins = new List<ShopEntry>();
      int _out6 = 0;
      int num7 = (int) reader.ReadInt("s", ref _out6);
      for (int index = 0; index < _out6; ++index)
        this.Bins.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      if (VersionNumberForLoad > 33)
      {
        this.ATMS = new List<ShopEntry>();
        int _out7 = 0;
        int num8 = (int) reader.ReadInt("s", ref _out7);
        for (int index = 0; index < _out7; ++index)
          this.ATMS.Add(new ShopEntry(reader, VersionNumberForLoad, player));
      }
      else
        this.ATMS = new List<ShopEntry>();
    }

    public void AddToShopNavigationAfterLoad(Player player)
    {
      for (int index = 0; index < this.Benches.Count; ++index)
        ShopNavigation.AddShop(this.Benches[index].LocationOfThisShop, this.Benches[index].tiletype, player.prisonlayout.layout.BaseTileTypes[this.Benches[index].LocationOfThisShop.X, this.Benches[index].LocationOfThisShop.Y].RotationClockWise, this.Benches[index]);
      for (int index = 0; index < this.Toilets.Count; ++index)
        ShopNavigation.AddShop(this.Toilets[index].LocationOfThisShop, this.Toilets[index].tiletype, player.prisonlayout.layout.BaseTileTypes[this.Toilets[index].LocationOfThisShop.X, this.Toilets[index].LocationOfThisShop.Y].RotationClockWise, this.Toilets[index]);
      for (int index = 0; index < this.shopentries.Count; ++index)
        ShopNavigation.AddShop(this.shopentries[index].LocationOfThisShop, this.shopentries[index].tiletype, player.prisonlayout.layout.BaseTileTypes[this.shopentries[index].LocationOfThisShop.X, this.shopentries[index].LocationOfThisShop.Y].RotationClockWise, this.shopentries[index]);
      for (int index = 0; index < this.Bins.Count; ++index)
        ShopNavigation.AddShop(this.Bins[index].LocationOfThisShop, this.Bins[index].tiletype, player.prisonlayout.layout.BaseTileTypes[this.Bins[index].LocationOfThisShop.X, this.Bins[index].LocationOfThisShop.Y].RotationClockWise, this.Bins[index]);
      for (int index = 0; index < this.ATMS.Count; ++index)
        ShopNavigation.AddShop(this.ATMS[index].LocationOfThisShop, this.ATMS[index].tiletype, player.prisonlayout.layout.BaseTileTypes[this.ATMS[index].LocationOfThisShop.X, this.ATMS[index].LocationOfThisShop.Y].RotationClockWise, this.ATMS[index]);
    }
  }
}
