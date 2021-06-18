// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.SimPerson
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.LoadValues;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class SimPerson
  {
    private static bool BetaShownJoe;
    public CustomerType customertype;
    public float MovementSpeedMultiplier = -1f;
    public bool IsFootballer;
    public bool IsGroupLeader;
    public Employee Ref_Employee;
    public BlackMarketTraderInfo blackmarkettrader;
    private JanitorController janitorcontroller;
    public KeeperController keepercontroller;
    private DeliveryManController deliveryman;
    public ShopkeeperController shopvendor;
    private VetController vetcontroller;
    private CorpseCollectorManager corpsecollector;
    private MechanicController mechaniccontroller;
    public PoliceOfficerController policeofficecontroller;
    public MemberOfThePublic memberofthepublic;
    public RoleInSociety roleinsociety;
    public AvatarController avatar;
    private float TempWalkSpeed;
    private ArchitectController architectcontroller;
    public bool IsDead;
    public GroupNavigator CustomerProtestGroup;
    public GroupNavigator FootballerGroup;
    private MeatProcessorController meatprocessorcontroller;
    private FarmerController farmercontroller;
    private FactoryWorkerController factoryworkercontroller;
    private VegProcessorController vegprocessorcontroller;
    public AnimalType persontype;
    public bool ForcePopUpOnEnterPark;

    public SimPerson(
      Employee _Ref_Employee,
      bool IsEmployee,
      AnimalType _persontype,
      bool IsRenderOnly,
      CellblockMananger cellblockcontainer,
      Player player,
      int ShopUID,
      WalkingPerson walkingperson)
    {
      this.persontype = _persontype;
      this.Ref_Employee = _Ref_Employee;
      if (IsRenderOnly)
        return;
      if (IsEmployee)
      {
        this.roleinsociety = RoleInSociety.Employee;
        if (this.Ref_Employee != null)
        {
          if (this.Ref_Employee.employeetype == EmployeeType.Janitor)
          {
            this.janitorcontroller = new JanitorController();
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.Vet)
          {
            this.vetcontroller = new VetController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.Mechanic)
          {
            this.mechaniccontroller = new MechanicController(this.Ref_Employee, walkingperson, player);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.Keeper)
          {
            this.keepercontroller = new KeeperController(this.Ref_Employee, walkingperson);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.MeatProcessorWorker)
          {
            this.meatprocessorcontroller = new MeatProcessorController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.VegProcessorWorker)
          {
            this.vegprocessorcontroller = new VegProcessorController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.WarehouseWorker)
          {
            this.factoryworkercontroller = new FactoryWorkerController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.Architect)
            this.architectcontroller = new ArchitectController(this.Ref_Employee);
          else if (this.Ref_Employee.employeetype == EmployeeType.ShopKeeper)
          {
            this.roleinsociety = RoleInSociety.Employee;
            this.shopvendor = new ShopkeeperController(this.Ref_Employee.quickemplyeedescription, this.Ref_Employee);
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.FactoryWorker)
          {
            this.roleinsociety = RoleInSociety.Employee;
            this.factoryworkercontroller = new FactoryWorkerController(this.Ref_Employee);
          }
          else if (this.Ref_Employee.employeetype == EmployeeType.Vet)
            this.roleinsociety = RoleInSociety.Employee;
          else if (this.Ref_Employee.employeetype == EmployeeType.Police)
          {
            this.policeofficecontroller = new PoliceOfficerController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
            this.Ref_Employee.quickemplyeedescription = new QuickEmployeeDescription(TILETYPE.Count, -1, 3f);
          }
          else
          {
            if (this.Ref_Employee.employeetype != EmployeeType.Farmer)
              return;
            this.farmercontroller = new FarmerController(this.Ref_Employee);
            this.roleinsociety = RoleInSociety.Employee;
          }
        }
        else
        {
          if (this.persontype != AnimalType.MaleZookeeper && this.persontype != AnimalType.FemaleZookeeper && (this.persontype != AnimalType.MaleAsianZookeeper && this.persontype != AnimalType.FemaleAsianZookeeper) && (this.persontype != AnimalType.MaleDarkZookeeper && this.persontype != AnimalType.FemaleDarkZookeeper))
            throw new Exception("NOT FIGUED OUT");
          this.roleinsociety = RoleInSociety.Avatar;
          this.avatar = new AvatarController(walkingperson);
          Z_GameFlags.avatarcontroller = this.avatar;
        }
      }
      else
      {
        if (this.persontype == AnimalType.TigerKing || this.persontype == AnimalType.BlackMarketDealer)
        {
          this.roleinsociety = RoleInSociety.BlackMarket;
          this.customertype = CustomerType.BlackMarket;
          this.memberofthepublic = new MemberOfThePublic(this.persontype, cellblockcontainer, true, player, this.customertype, walkingperson);
          ++CustomerManager.CustomersInPark_NotWaitingForBus;
          if (Z_DebugFlags.IsBetaVersion && Player.criticalchoices.BadChoicesMade > 0 && !SimPerson.BetaShownJoe)
          {
            SimPerson.BetaShownJoe = true;
            OverWorldManager.zoopopupHolder.CreaLockingCharacterStory(new FeatureUnlockSpeechPack("Well, now I know what side of the team you play for, I can start offering you better deals.~~Painting that horse to look like a zebra was a wise choice. Why pay for exotic animals when you can make your own...? Unless you are buying them from me of course!", AnimalType.TigerKing, "Joined the darkside"));
          }
          this.blackmarkettrader = new BlackMarketTraderInfo();
        }
        else if (EnemyData.IsThisAAnimalWelfarePerson(this.persontype))
        {
          this.roleinsociety = RoleInSociety.Customer;
          this.customertype = CustomerType.AnimalWelfareOfficer;
          this.memberofthepublic = new MemberOfThePublic(this.persontype, cellblockcontainer, true, player, this.customertype, walkingperson);
          ++CustomerManager.CustomersInPark_NotWaitingForBus;
        }
        else if (EnemyData.IsThisACriticalChoicePerson(this.persontype))
        {
          this.roleinsociety = RoleInSociety.Customer;
          switch (this.persontype)
          {
            case AnimalType.SpecialEvent_Artist:
              this.customertype = CustomerType.AnimalArtist;
              break;
            case AnimalType.SpecialEvent_Scientist:
              this.customertype = CustomerType.ResearchGrantGuy;
              break;
            case AnimalType.SpecialEvent_GenomeScientist:
              this.customertype = CustomerType.GenomeBetaGiver;
              break;
          }
          this.memberofthepublic = new MemberOfThePublic(this.persontype, cellblockcontainer, true, player, this.customertype, walkingperson);
          ++CustomerManager.CustomersInPark_NotWaitingForBus;
        }
        else
        {
          ++CustomerManager.CustomersInPark_NotWaitingForBus;
          this.roleinsociety = RoleInSociety.Customer;
          this.memberofthepublic = new MemberOfThePublic(this.persontype, cellblockcontainer, false, player, this.customertype, walkingperson);
          if (this.persontype >= AnimalType.Protestor1 && this.persontype <= AnimalType.Protestor6)
          {
            this.customertype = CustomerType.Protestor;
            this.CustomerProtestGroup = CustomerManager.ProtestGroupNavigator;
            this.memberofthepublic.AddGroupNavigation(this.CustomerProtestGroup);
            this.CustomerProtestGroup.AddMe(walkingperson);
          }
          else if (EnemyData.IsThisAAnimalWelfarePerson(this.persontype))
            this.customertype = CustomerType.AnimalWelfareOfficer;
          else if (AnimalData.IsThisAFoorballer(this.persontype))
            this.IsFootballer = true;
        }
        if (CustomerManager.IsAVIP(this.customertype))
          ++CustomerManager.VIP_BlackMarketEtc;
        this.SetWalkSpeed();
      }
    }

    public Vector2Int CheckGroupNavigation(WalkingPerson walkingperson) => this.CustomerProtestGroup != null ? this.CustomerProtestGroup.TryFindAPlaceToWalkTo(walkingperson) : (Vector2Int) null;

    public Vector2Int CheckFootballesNavigation(WalkingPerson walkingperson) => this.FootballerGroup != null ? this.FootballerGroup.TryFindAPlaceToWalkTo(walkingperson) : (Vector2Int) null;

    public void StartStrike(EmployeeType employeetype)
    {
    }

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
      if (this.roleinsociety != RoleInSociety.Employee)
        return;
      if (this.Ref_Employee.employeetype == EmployeeType.Keeper)
        this.keepercontroller.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.Ref_Employee.employeetype == EmployeeType.ShopKeeper)
        this.shopvendor.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.Ref_Employee.employeetype == EmployeeType.MeatProcessorWorker)
        this.meatprocessorcontroller.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.farmercontroller != null)
        this.farmercontroller.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.factoryworkercontroller != null)
      {
        this.factoryworkercontroller.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      }
      else
      {
        if (this.vegprocessorcontroller == null)
          return;
        this.vegprocessorcontroller.TeleportedToGate(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      }
    }

    public int GetTotalProtestors() => this.CustomerProtestGroup != null ? this.CustomerProtestGroup.GetGroupSize() : 0;

    public void StartNewDay()
    {
      if (this.roleinsociety != RoleInSociety.Employee)
        return;
      if (this.Ref_Employee.employeetype == EmployeeType.Keeper)
        this.keepercontroller.StartNewDay();
      else if (this.Ref_Employee.employeetype == EmployeeType.ShopKeeper)
        this.shopvendor.StartNewDay();
      else if (this.Ref_Employee.employeetype == EmployeeType.MeatProcessorWorker)
        this.meatprocessorcontroller.StartNewDay();
      else if (this.farmercontroller != null)
        this.farmercontroller.StartNewDay();
      else if (this.factoryworkercontroller != null)
      {
        this.factoryworkercontroller.StartNewDay();
      }
      else
      {
        if (this.vegprocessorcontroller == null)
          return;
        this.vegprocessorcontroller.StartNewDay();
      }
    }

    public void TeleportToJob()
    {
      if (this.Ref_Employee == null || this.Ref_Employee.employeetype != EmployeeType.Mechanic)
        throw new Exception("Not handled yet");
      this.mechaniccontroller.StartFixing();
    }

    public string GetName()
    {
      if (this.memberofthepublic != null)
        return this.memberofthepublic.Name;
      return this.Ref_Employee == null || this.Ref_Employee.intakeperson == null ? "NO NAME" : this.Ref_Employee.intakeperson.Name;
    }

    public void WalkedUsedEnergy(int TileWalked)
    {
      if (this.memberofthepublic == null)
        return;
      this.memberofthepublic.WalkedUsedEnergy(TileWalked);
      this.SetWalkSpeed();
    }

    public void UpdateNeedsAndWants(float Cycles, Player player, WalkingPerson walkingperson)
    {
      if (this.memberofthepublic != null)
      {
        this.memberofthepublic.UpdateNeedsAndWants(Cycles, walkingperson);
      }
      else
      {
        if (this.architectcontroller == null)
          return;
        this.architectcontroller.UpdateResearchProgress(Cycles, player);
      }
    }

    private void SetWalkSpeed()
    {
      float currentWantValue = this.memberofthepublic.customerneeds.CurrentWantValues[0];
      double walkSpeedEnergyMult = (double) BVal.WalkSpeedEnergyMult;
      this.TempWalkSpeed = Math.Max(0.4f, (float) (((double) this.memberofthepublic.customerneeds.Fitness + 1.0) * ((1.0 - (double) this.memberofthepublic.customerneeds.Fitness) * (double) currentWantValue)) + this.memberofthepublic.customerneeds.Fitness);
      this.TempWalkSpeed *= BVal.WalkSpeedMult;
    }

    public float GetEnergyWalkSpeedMultiplier()
    {
      if (this.roleinsociety != RoleInSociety.Customer)
        return 1f;
      return (double) this.memberofthepublic.customerneeds.CurrentWantValues[17] > 0.0 ? this.TempWalkSpeed * 2f : this.TempWalkSpeed;
    }

    public void UpdateAvatar(
      PathNavigator pathnavigator,
      float DeltaTime,
      Player player,
      ref bool IsWalking,
      WalkingPerson Person)
    {
      this.avatar.UpdateAvatarController(pathnavigator, player, DeltaTime, this.Ref_Employee, ref IsWalking, Person);
    }

    public void UpdateEmployee(
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation,
      float DeltaTime,
      out bool TeleportToGate,
      Player player,
      WalkingPerson parent)
    {
      TeleportToGate = false;
      if (this.shopvendor != null)
        this.shopvendor.UpdateShopkeeperController(pathnavigator, ref BlockAutoWalk, ref IsPlayingWalkAnimation, DeltaTime, out TeleportToGate, player, this.Ref_Employee, parent);
      else if (this.keepercontroller != null)
        this.keepercontroller.UpdateKeeperController(pathnavigator, ref BlockAutoWalk, ref IsPlayingWalkAnimation, DeltaTime, out TeleportToGate, player, this.Ref_Employee, parent, ref IsPlayingWalkAnimation);
      else if (this.mechaniccontroller != null)
        this.mechaniccontroller.UpdateMechanicController(pathnavigator, ref BlockAutoWalk, ref IsPlayingWalkAnimation, DeltaTime, out TeleportToGate, player, this.Ref_Employee, parent, ref IsPlayingWalkAnimation);
      else if (this.vetcontroller != null)
        this.vetcontroller.UpdateVetController(DeltaTime, player, parent, ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.policeofficecontroller != null)
        this.policeofficecontroller.UpdatePoliceOfficermanager(DeltaTime, parent, ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      else if (this.meatprocessorcontroller != null)
        this.meatprocessorcontroller.UpdateMeatProcessorController(DeltaTime, parent, ref IsPlayingWalkAnimation, ref BlockAutoWalk, player);
      else if (this.farmercontroller != null)
        this.farmercontroller.UpdateFarmerController(DeltaTime, parent, ref IsPlayingWalkAnimation, ref BlockAutoWalk, player);
      else if (this.factoryworkercontroller != null)
      {
        this.factoryworkercontroller.UpdateFactoryWorkerController(DeltaTime, parent, ref IsPlayingWalkAnimation, ref BlockAutoWalk, player);
      }
      else
      {
        if (this.vegprocessorcontroller == null)
          return;
        this.vegprocessorcontroller.UpdateVegProcessorController(DeltaTime, parent, ref IsPlayingWalkAnimation, ref BlockAutoWalk, player);
      }
    }

    public void UpdateCustomer(
      WalkingPerson parent,
      float DeltaTime,
      Player player,
      ref bool WalkPaused,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      this.memberofthepublic.UpdateMemberOfThePublic(parent, DeltaTime, player, ref WalkPaused, ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      if (this.memberofthepublic.UnnequipedAttachments.Count <= 0)
        return;
      for (int index = 0; index < this.memberofthepublic.UnnequipedAttachments.Count; ++index)
      {
        if (parent.AddAttachment(this.memberofthepublic.UnnequipedAttachments[index]))
          this.memberofthepublic.AddCurrentAction(this.memberofthepublic.UnnequipedAttachments[index]);
        else
          this.memberofthepublic.AddTrash(TrashDrop.GetPersonAttachementTypeToTrash(this.memberofthepublic.UnnequipedAttachments[index]));
      }
      this.memberofthepublic.UnnequipedAttachments = new List<PersonAttachementType>();
    }

    public void ReachedTarget(
      Vector2Int CurrentLocation,
      int YThreshold,
      out Vector2Int ForceGoHere,
      Player player,
      Vector2 vLocation,
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      out bool DidSetNewPath,
      WalkingPerson parent,
      ref bool IsPlayingWalkAnimation)
    {
      DidSetNewPath = false;
      ForceGoHere = (Vector2Int) null;
      if (CurrentLocation.Y < YThreshold)
      {
        if (this.memberofthepublic != null)
          this.memberofthepublic.ReachedTarget(CurrentLocation, player, vLocation, pathnavigator, out DidSetNewPath, parent, ref BlockAutoWalk, ref IsPlayingWalkAnimation, ref ForceGoHere);
        else if (this.shopvendor != null)
          this.shopvendor.ReachedTargetLocation(pathnavigator, ref BlockAutoWalk, parent, out DidSetNewPath, ref IsPlayingWalkAnimation);
        else if (this.vetcontroller != null)
          this.vetcontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, player, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.janitorcontroller != null)
          this.janitorcontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, player);
        else if (this.policeofficecontroller != null)
          this.policeofficecontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.keepercontroller != null)
          this.keepercontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, player, pathnavigator, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.mechaniccontroller != null)
          this.mechaniccontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, player, pathnavigator, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.meatprocessorcontroller != null)
          this.meatprocessorcontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, player, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.corpsecollector != null)
          this.corpsecollector.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, player, pathnavigator, ref BlockAutoWalk);
        else if (this.farmercontroller != null)
          this.farmercontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, player, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        else if (this.factoryworkercontroller != null)
        {
          this.factoryworkercontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, player, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        }
        else
        {
          if (this.vegprocessorcontroller == null)
            return;
          this.vegprocessorcontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, player, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
        }
      }
      else if (this.memberofthepublic != null)
      {
        this.memberofthepublic.ReachedOutOfThresholdTarget(CurrentLocation, parent);
      }
      else
      {
        if (this.policeofficecontroller == null)
          return;
        this.policeofficecontroller.ReachedTargetLocation(CurrentLocation, ref ForceGoHere, this.Ref_Employee, ref BlockAutoWalk, parent, ref IsPlayingWalkAnimation);
      }
    }

    public void CheckJob()
    {
      if (this.meatprocessorcontroller == null)
        return;
      this.meatprocessorcontroller.CheckJob();
    }

    public void CheckPathOnSetNewTarget(PathNavigator pathnavigator)
    {
      if (this.janitorcontroller == null)
        return;
      this.janitorcontroller.CheckPathOnSetNewTarget(pathnavigator);
    }

    public void GetNextTarget()
    {
    }
  }
}
