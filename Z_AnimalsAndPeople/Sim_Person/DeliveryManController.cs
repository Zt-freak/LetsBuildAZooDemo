// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.DeliveryManController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class DeliveryManController
  {
    public bool IsDoingDelivery;
    public DeliveryGuyStatus deliveryguystatus;
    private JobLocationType joblocation;
    public Vector2Int DoorLocation;
    public Vector2Int InsideLocation;
    public Vector2Int SpaceInfrontOfDoor;
    private bool TargetHasDoor;
    private bool IsGoingThroughPenDoor;
    private EnclosureGate currentgate;
    private TILETYPE _tiletype;
    private int _PENUID;

    public DeliveryManController()
    {
      this.IsDoingDelivery = false;
      this.deliveryguystatus = DeliveryGuyStatus.DoingNothing;
    }

    public bool TryGoToSpecificBuilding(
      TILETYPE buildingtype,
      int ShopUID,
      Player player,
      WalkingPerson Parent,
      ref Vector2Int ForceGoHere)
    {
      return this.TryGoToSpecificBuilding(buildingtype, ShopUID, player, Parent, ref ForceGoHere, out ShopEntry _);
    }

    public bool TryToInternalNavigate(
      Vector2Int GoHere,
      WalkingPerson walkingperson,
      ref bool IsWalking)
    {
      List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(walkingperson.pathnavigator.CurrentTile, GoHere, false);
      if (fullPathToLocation == null || fullPathToLocation.Count <= 0)
        return false;
      this.deliveryguystatus = DeliveryGuyStatus.isInternalNavigating;
      IsWalking = true;
      walkingperson.pathnavigator.SetNewPath(fullPathToLocation);
      return true;
    }

    public bool TryGoToSpecificBuilding(
      TILETYPE buildingtype,
      int ShopUID,
      Player player,
      WalkingPerson Parent,
      ref Vector2Int ForceGoHere,
      out ShopEntry shopEntry)
    {
      shopEntry = (ShopEntry) null;
      if (TileData.IsThisAFacility(buildingtype) || TileData.IsAFactory(buildingtype))
      {
        ShopEntry thisFacility = player.shopstatus.GetThisFacility(ShopUID);
        if (player.prisonlayout.layout.BaseTileTypes[thisFacility.LocationOfThisShop.X, thisFacility.LocationOfThisShop.Y].tiletype != thisFacility.tiletype)
          throw new Exception("This  building is no longer here");
        if (this.TryToGoToThisBuilding(buildingtype, thisFacility.LocationOfThisShop, player, ref ForceGoHere, Parent))
        {
          shopEntry = thisFacility;
          return true;
        }
      }
      else
      {
        if (!TileData.IsAStoreRoom(buildingtype))
          throw new Exception("HANDLE THIS TYPE OF TBUILDING THING");
        if (this.TryToGoToThisBuilding(buildingtype, player.storerooms.StorRoomcontents.StoreRoomLocation, player, ref ForceGoHere, Parent))
          return true;
      }
      return false;
    }

    public bool TryGoToBuilding(
      TILETYPE buildingtype,
      Player player,
      WalkingPerson Parent,
      ref Vector2Int ForceGoHere)
    {
      return this.TryGoToBuilding(buildingtype, player, Parent, ref ForceGoHere, out ShopEntry _);
    }

    public bool CheckCanReachThisBuilding_DONOTGO(
      TILETYPE tiletype,
      Player player,
      Vector2Int BuildingLocation,
      WalkingPerson Parent)
    {
      Vector2Int vector2Int = TileData.GetTileInfo(tiletype).GetTradesmansEntrance(player.prisonlayout.layout.BaseTileTypes[BuildingLocation.X, BuildingLocation.Y].RotationClockWise) ?? new Vector2Int(0, 0);
      if (Parent.pathnavigator.CurrentTile.CompareMatches(BuildingLocation + vector2Int))
        return true;
      List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(Parent.pathnavigator.CurrentTile, BuildingLocation + vector2Int, false);
      return fullPathToLocation != null && fullPathToLocation.Count > 0;
    }

    public bool TryGoToBuilding(
      TILETYPE buildingtype,
      Player player,
      WalkingPerson Parent,
      ref Vector2Int ForceGoHere,
      out ShopEntry shopEntry)
    {
      if (TileData.IsThisAFacility(buildingtype))
        return this.TryToGoToOneOfThses(player.shopstatus.FacilitiesWithEmployees, buildingtype, Parent, player, ref ForceGoHere, out shopEntry);
      throw new Exception("HANDLE THIS TYPE OF TBUILDING THING");
    }

    public bool ShouldNotTeleportFromBlockedTile() => this.IsDoingDelivery && (this.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting || this.deliveryguystatus == DeliveryGuyStatus.WalkingThroughTheDoorAndOffCollision);

    public void CancelDelivery(ref bool BlockWalk, ref bool PlayingWalkAnimation)
    {
      BlockWalk = false;
      PlayingWalkAnimation = false;
      if (!this.IsDoingDelivery)
        return;
      this.IsDoingDelivery = false;
      if (this.currentgate == null)
        return;
      this.currentgate.CloseGate();
    }

    private bool TryToGoToOneOfThses(
      List<ShopEntry> theseshopentries,
      TILETYPE buildingtype,
      WalkingPerson Parent,
      Player player,
      ref Vector2Int ForceGoHere,
      out ShopEntry shopEntry)
    {
      int num = 0;
      shopEntry = (ShopEntry) null;
      List<BuildingDistance> buildingDistanceList = new List<BuildingDistance>();
      for (int index = 0; index < theseshopentries.Count; ++index)
      {
        if (theseshopentries[index].tiletype == buildingtype)
        {
          ++num;
          buildingDistanceList.Add(new BuildingDistance(theseshopentries[index].LocationOfThisShop, Parent.pathnavigator.CurrentTile, theseshopentries[index]));
        }
      }
      if (num == 0)
        return false;
      buildingDistanceList.Sort(new Comparison<BuildingDistance>(BuildingDistance.SortBuildingDistance));
      for (int index = 0; index < buildingDistanceList.Count; ++index)
      {
        if (this.TryToGoToThisBuilding(buildingDistanceList[index].REF_shopentry.tiletype, buildingDistanceList[index].REF_shopentry.LocationOfThisShop, player, ref ForceGoHere, Parent))
        {
          shopEntry = buildingDistanceList[index].REF_shopentry;
          return true;
        }
      }
      return false;
    }

    private bool TryToGoToThisBuilding(
      TILETYPE tiletype,
      Vector2Int ShopLoc,
      Player player,
      ref Vector2Int ForceGoHere,
      WalkingPerson Parent)
    {
      this.joblocation = JobLocationType.None;
      Vector2Int vector2Int = TileData.GetTileInfo(tiletype).GetTradesmansEntrance(player.prisonlayout.layout.BaseTileTypes[ShopLoc.X, ShopLoc.Y].RotationClockWise) ?? new Vector2Int(0, 0);
      List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(Parent.pathnavigator.CurrentTile, ShopLoc + vector2Int, false);
      if (fullPathToLocation == null || fullPathToLocation.Count <= 0)
        return false;
      this.TargetHasDoor = DoorData.ThisBuildingHasADoor(tiletype);
      this.IsDoingDelivery = true;
      this.DoorLocation = vector2Int + ShopLoc;
      this.SpaceInfrontOfDoor = new Vector2Int(this.DoorLocation);
      ForceGoHere = new Vector2Int(this.SpaceInfrontOfDoor);
      if (this.TargetHasDoor)
      {
        this.DoorLocation = new Vector2Int(ShopLoc);
        this.IsGoingThroughPenDoor = false;
        Vector2 one = Vector2.One;
        this.InsideLocation = ShopLoc + TileData.GetTileInfo(tiletype).GetShopKeeperLocation(player.prisonlayout.layout.BaseTileTypes[ShopLoc.X, ShopLoc.Y].RotationClockWise, ref one);
      }
      this.joblocation = JobLocationType.Building;
      this._tiletype = tiletype;
      this.deliveryguystatus = DeliveryGuyStatus.OnWayToJob;
      return true;
    }

    public bool TryToStartJourneyToFirstAccessiblePenPen(
      Player player,
      out int PenUID,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere)
    {
      PenUID = -1;
      if (!parent.IsEmployee)
        throw new Exception("NEED TO HANDLE VIP's AND STUFF - IF THEY ARE GOING FOR AN ANIMAL ENCOUNTER");
      if (parent.simperson.Ref_Employee.workzoneinfo != null)
      {
        if (parent.simperson.Ref_Employee.workzoneinfo.workzonetype != WorkZoneType.Pens)
          throw new Exception("Employees that go to pens should have pen wok zones right? - maybe not");
        if (parent.simperson.Ref_Employee.workzoneinfo.PenUIDs.Count > 0)
        {
          int num = 0;
          for (int index = 0; index < parent.simperson.Ref_Employee.workzoneinfo.PenUIDs.Count; ++index)
          {
            if (parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index] == parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID)
            {
              num = index + 1;
              if (num >= parent.simperson.Ref_Employee.workzoneinfo.PenUIDs.Count)
                num = 0;
            }
          }
          for (int index = num; index < parent.simperson.Ref_Employee.workzoneinfo.PenUIDs.Count; ++index)
          {
            if (this.TryToStartJourneyToPen(player, parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index], parent, ref ForceGoHere))
            {
              parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID = parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index];
              PenUID = parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index];
              return true;
            }
          }
          for (int index = 0; index < num; ++index)
          {
            if (this.TryToStartJourneyToPen(player, parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index], parent, ref ForceGoHere))
            {
              parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID = parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index];
              PenUID = parent.simperson.Ref_Employee.workzoneinfo.PenUIDs[index];
              return true;
            }
          }
        }
        else
        {
          int num = 0;
          for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          {
            if (player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID == parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID)
            {
              num = index + 1;
              if (num >= player.prisonlayout.cellblockcontainer.prisonzones.Count)
                num = 0;
            }
          }
          for (int index = num; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          {
            if (this.TryToStartJourneyToPen(player, player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID, parent, ref ForceGoHere, index))
            {
              parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID = player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID;
              PenUID = player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID;
              return true;
            }
          }
          for (int index = 0; index < num; ++index)
          {
            if (index < player.prisonlayout.cellblockcontainer.prisonzones.Count && this.TryToStartJourneyToPen(player, player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID, parent, ref ForceGoHere, index))
            {
              parent.simperson.Ref_Employee.workzoneinfo.LastPenVisitedUID = player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID;
              PenUID = player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID;
              return true;
            }
          }
        }
      }
      return false;
    }

    public bool TryToStartJourneyToPen(
      Player player,
      int PenUID,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      int ArrayShortCut = -1,
      bool GoToFarm = false)
    {
      this.TargetHasDoor = true;
      this.IsGoingThroughPenDoor = true;
      PrisonZone prisonZone = !GoToFarm ? (ArrayShortCut <= -1 ? player.prisonlayout.cellblockcontainer.GetThisCellBlock(PenUID) : player.prisonlayout.cellblockcontainer.prisonzones[ArrayShortCut]) : player.farms.GetThisFarmFieldByUID(PenUID);
      if (prisonZone == null)
        return false;
      this.DoorLocation = prisonZone.GetGateLocation();
      this.SpaceInfrontOfDoor = new Vector2Int(this.DoorLocation);
      switch (player.prisonlayout.layout.BaseTileTypes[this.DoorLocation.X, this.DoorLocation.Y].RotationClockWise)
      {
        case 0:
          ++this.SpaceInfrontOfDoor.Y;
          break;
        case 1:
          --this.SpaceInfrontOfDoor.X;
          break;
        case 2:
          --this.SpaceInfrontOfDoor.Y;
          break;
        default:
          ++this.SpaceInfrontOfDoor.X;
          break;
      }
      List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(parent.pathnavigator.CurrentTile, this.SpaceInfrontOfDoor, false);
      if (fullPathToLocation == null || fullPathToLocation.Count <= 0)
        return false;
      this.joblocation = JobLocationType.Enclosure;
      ForceGoHere = new Vector2Int(this.SpaceInfrontOfDoor);
      this.IsDoingDelivery = true;
      this._PENUID = PenUID;
      this.deliveryguystatus = DeliveryGuyStatus.OnWayToJob;
      return true;
    }

    public void EndJob()
    {
    }

    public bool ReachedTargetLocation(
      Vector2Int Location,
      ref bool BlockAutoWalk,
      ref bool IsWalking,
      out bool JobCancelled)
    {
      JobCancelled = false;
      if (this.IsDoingDelivery)
      {
        if (this.deliveryguystatus == DeliveryGuyStatus.isInternalNavigating)
        {
          BlockAutoWalk = true;
          IsWalking = false;
          this.deliveryguystatus = DeliveryGuyStatus.InternalNavigationWaiting;
          return true;
        }
        if (this.deliveryguystatus == DeliveryGuyStatus.WalkingToGateToLeaveInterior)
        {
          BlockAutoWalk = true;
          IsWalking = false;
          this.ExitJobLocation();
        }
        else
        {
          if (this.deliveryguystatus == DeliveryGuyStatus.InternalNavigationWaiting)
            return true;
          if (this.deliveryguystatus == DeliveryGuyStatus.OnWayToJob)
          {
            if (this.TargetHasDoor)
            {
              if (this.SpaceInfrontOfDoor.CompareMatches(Location))
              {
                this.currentgate = OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y] == null ? (EnclosureGate) null : OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y].GetGate();
                if (this.currentgate != null)
                {
                  JobCancelled = true;
                  BlockAutoWalk = true;
                  IsWalking = false;
                  this.deliveryguystatus = DeliveryGuyStatus.WaitingForDoorToOpen;
                  this.currentgate.OpenGateNow();
                }
                else
                {
                  this.deliveryguystatus = DeliveryGuyStatus.DoingNothing;
                  this.IsDoingDelivery = false;
                }
              }
            }
            else if (this.DoorLocation.CompareMatches(Location))
            {
              BlockAutoWalk = true;
              IsWalking = false;
              this.deliveryguystatus = DeliveryGuyStatus.AtJobWaiting;
              return true;
            }
          }
          else if (this.deliveryguystatus == DeliveryGuyStatus.WalkingThroughTheDoorAndOffCollision)
          {
            BlockAutoWalk = true;
            IsWalking = false;
            this.deliveryguystatus = DeliveryGuyStatus.AtLocationWaitingForDoorToClose;
            this.currentgate = OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y] == null ? (EnclosureGate) null : OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y].GetGate();
            if (this.currentgate != null)
              this.currentgate.CloseGate();
          }
          else if (this.deliveryguystatus == DeliveryGuyStatus.ExitingThoughDoor)
          {
            BlockAutoWalk = true;
            IsWalking = false;
            this.deliveryguystatus = DeliveryGuyStatus.WaitingForDoorToCloseAfterExit;
            this.currentgate = OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y] == null ? (EnclosureGate) null : OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y].GetGate();
            if (this.currentgate != null)
              this.currentgate.CloseGate();
          }
        }
      }
      return false;
    }

    public void ReturnToGateAndThenExitJob(WalkingPerson walkingperson, ref bool IsWalking)
    {
      if (walkingperson.pathnavigator.CurrentTile.CompareMatches(this.InsideLocation))
        this.ExitJobLocation();
      else if (this.TryToInternalNavigate(this.InsideLocation, walkingperson, ref IsWalking))
        this.deliveryguystatus = DeliveryGuyStatus.WalkingToGateToLeaveInterior;
      else if (!Z_GameFlags.pathfinder.GetIsBlocked(this.InsideLocation.X, this.InsideLocation.Y))
      {
        walkingperson.pathnavigator.TeleportHere(this.InsideLocation);
        this.ExitJobLocation();
      }
      else
        walkingperson.TeleportToGateNextUpdate = true;
    }

    public void ExitJobLocation()
    {
      if (this.TargetHasDoor)
      {
        this.deliveryguystatus = DeliveryGuyStatus.WaitingForDoorToOpenToExit;
        this.currentgate = OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y] == null ? (EnclosureGate) null : OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.DoorLocation.X, this.DoorLocation.Y].GetGate();
        if (this.currentgate == null)
          return;
        this.currentgate.OpenGateNow();
      }
      else
      {
        this.deliveryguystatus = DeliveryGuyStatus.DoingNothing;
        this.IsDoingDelivery = false;
      }
    }

    public int GetCurrentPenUID() => this._PENUID;

    public bool UpdateDelivery(
      float DeltaTime,
      ref bool IsPlayingWalkAnimation,
      Player player,
      WalkingPerson walkingperson,
      ref bool BlockAutoWalk,
      out bool CancelQuest)
    {
      CancelQuest = false;
      if (this.IsDoingDelivery)
      {
        if (GameFlags.CollisionChanged && this.joblocation == JobLocationType.Enclosure && player.prisonlayout.GetThisCellBlock(this._PENUID) == null)
        {
          CancelQuest = true;
          this.IsDoingDelivery = false;
          this.deliveryguystatus = DeliveryGuyStatus.DoingNothing;
        }
        if (this.deliveryguystatus == DeliveryGuyStatus.WaitingForDoorToOpen)
        {
          if (this.currentgate == null || this.currentgate.HasBeenDestroyed || this.currentgate.IsOpen())
            this.SetPathToInterior(ref IsPlayingWalkAnimation, walkingperson);
        }
        else if (this.deliveryguystatus == DeliveryGuyStatus.AtLocationWaitingForDoorToClose)
        {
          if (this.currentgate == null || this.currentgate.HasBeenDestroyed || this.currentgate.IsClosed())
            this.deliveryguystatus = DeliveryGuyStatus.AtJobWaiting;
        }
        else if (this.deliveryguystatus == DeliveryGuyStatus.WaitingForDoorToOpenToExit)
        {
          if (this.currentgate == null || this.currentgate.HasBeenDestroyed || this.currentgate.IsOpen())
            this.SetPathToInterior(ref IsPlayingWalkAnimation, walkingperson, true);
        }
        else if (this.deliveryguystatus == DeliveryGuyStatus.WaitingForDoorToCloseAfterExit && (this.currentgate == null || this.currentgate.HasBeenDestroyed || this.currentgate.IsClosed()))
        {
          BlockAutoWalk = false;
          this.IsDoingDelivery = false;
          return true;
        }
      }
      return false;
    }

    private void SetPathToInterior(ref bool IsWalking, WalkingPerson walkingperson, bool IsLeaving = false)
    {
      List<PathNode> targets = new List<PathNode>();
      if (this.IsGoingThroughPenDoor)
      {
        targets.Add(new PathNode(this.DoorLocation.X, this.DoorLocation.Y));
        DoorPathFinder.GetPathToPenInterior(ref targets, IsLeaving, this.SpaceInfrontOfDoor, this.DoorLocation, ref this.InsideLocation);
      }
      else
        DoorPathFinder.GetPathToBuildingInterior(ref targets, IsLeaving, this.SpaceInfrontOfDoor, this.InsideLocation);
      IsWalking = true;
      walkingperson.pathnavigator.SetNewPath(targets);
      if (IsLeaving)
        this.deliveryguystatus = DeliveryGuyStatus.ExitingThoughDoor;
      else
        this.deliveryguystatus = DeliveryGuyStatus.WalkingThroughTheDoorAndOffCollision;
    }
  }
}
