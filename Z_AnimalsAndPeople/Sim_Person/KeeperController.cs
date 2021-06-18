// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.KeeperController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class KeeperController
  {
    private EnclosureGate currentgate;
    private Vector2Int GateTarget;
    private CurentZooKeeperState zookeeperstate;
    private Vector2Int GateExitPoint;
    private float FeedAnimalsTimerPickPoop;
    private float TimeNeededToFeedAnimals = 1f;
    private float TimeInStoreRoom = 1f;
    private float StoreRoomTimer;
    private int Current_Cell_UID;
    private bool GateBroken;
    private DeliveryManController deliveryman;
    private Employee Ref_Employee;
    private bool NothingLeftToFeed;
    private int PoopSearchID;
    private bool WasTeleported;

    public KeeperController(Employee _employee, WalkingPerson walkingperson)
    {
      this.Current_Cell_UID = -1;
      this.Ref_Employee = _employee;
      this.zookeeperstate = CurentZooKeeperState.None;
      this.deliveryman = new DeliveryManController();
    }

    public void StartNewDay() => this.NothingLeftToFeed = false;

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
      this.WasTeleported = true;
      this.deliveryman.CancelDelivery(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
    }

    public bool ShouldNotTeleportFromBlockedTile(
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      if (this.deliveryman.ShouldNotTeleportFromBlockedTile())
        return true;
      this.deliveryman.CancelDelivery(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
      return false;
    }

    public void UpdateKeeperController(
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation,
      float DeltaTime,
      out bool TeleportToGate,
      Player player,
      Employee Ref_Employee,
      WalkingPerson parent,
      ref bool IsWalking)
    {
      TeleportToGate = false;
      if (this.WasTeleported)
        this.WasTeleported = false;
      if (GameFlags.CollisionChanged)
      {
        if (this.zookeeperstate == CurentZooKeeperState.CannotReachStoreRoom)
          this.zookeeperstate = CurentZooKeeperState.GoingToStoreRoom;
        else if (this.zookeeperstate == CurentZooKeeperState.CannotReachPen)
          this.zookeeperstate = CurentZooKeeperState.GoingToPen;
      }
      if (Z_GameFlags.RecheckZooKeeperZones)
        Ref_Employee.workzoneinfo.RecheckPens(player);
      if (!this.deliveryman.IsDoingDelivery)
        return;
      if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.InternalNavigationWaiting)
      {
        if (this.zookeeperstate != CurentZooKeeperState.CollectingPoop)
          return;
        this.FeedAnimalsTimerPickPoop += DeltaTime;
        if ((double) this.FeedAnimalsTimerPickPoop <= 1.0)
          return;
        OverWorldManager.trashmanager.TryToPickUpPoop(parent.pathnavigator.CurrentTile, this.deliveryman.GetCurrentPenUID());
        this.FinishedWaiting(parent, ref IsWalking);
        this.FeedAnimalsTimerPickPoop = 0.0f;
      }
      else if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting)
      {
        if (this.zookeeperstate == CurentZooKeeperState.GoingToPen)
        {
          this.FeedAnimalsTimerPickPoop += DeltaTime;
          if ((double) this.FeedAnimalsTimerPickPoop <= (double) this.TimeNeededToFeedAnimals)
            return;
          bool DidNotFindPen = false;
          if (!this.DoFeedAnimals_InPen(player, out DidNotFindPen) && DidNotFindPen)
          {
            MoneyRenderer.PopIcon(parent.vLocation, IconPopType.Confused);
            this.deliveryman.CancelDelivery(ref BlockAutoWalk, ref IsPlayingWalkAnimation);
          }
          this.FeedAnimalsTimerPickPoop = 0.0f;
          this.PoopSearchID = 0;
          this.FinishedWaiting(parent, ref IsWalking);
        }
        else
        {
          if (this.zookeeperstate != CurentZooKeeperState.GoingToStoreRoom)
            return;
          this.StoreRoomTimer += DeltaTime;
          if ((double) this.StoreRoomTimer <= (double) this.TimeInStoreRoom)
            return;
          this.DoGetFoodFromStoreRoom();
          this.StoreRoomTimer = 0.0f;
          this.zookeeperstate = CurentZooKeeperState.GoingToPen;
          this.deliveryman.ExitJobLocation();
        }
      }
      else
        this.deliveryman.UpdateDelivery(DeltaTime, ref IsPlayingWalkAnimation, player, parent, ref BlockAutoWalk, out bool _);
    }

    public void FinishedWaiting(WalkingPerson parent, ref bool IsWalking)
    {
      if (Z_TrashManager.HasPoopHere(this.deliveryman.GetCurrentPenUID()))
      {
        this.zookeeperstate = CurentZooKeeperState.SearchingForPoop;
        if (this.TryAndTravelToPoop(parent, ref IsWalking))
          return;
        this.deliveryman.ReturnToGateAndThenExitJob(parent, ref IsWalking);
        this.zookeeperstate = CurentZooKeeperState.GoingToStoreRoom;
      }
      else
      {
        this.deliveryman.ReturnToGateAndThenExitJob(parent, ref IsWalking);
        this.zookeeperstate = CurentZooKeeperState.GoingToStoreRoom;
      }
    }

    public bool TryAndTravelToPoop(WalkingPerson walkingPerson, ref bool IsWalking)
    {
      bool ReachedEndOfList = false;
      while (!ReachedEndOfList)
      {
        TrashDrop nextPoop = Z_GameFlags.Location_Directory.GetNextPoop(this.deliveryman.GetCurrentPenUID(), this.PoopSearchID, ref ReachedEndOfList);
        if (nextPoop != null)
        {
          if (this.deliveryman.TryToInternalNavigate(nextPoop.TileLocation, walkingPerson, ref IsWalking))
            ReachedEndOfList = true;
          else
            ++this.PoopSearchID;
        }
        else if (ReachedEndOfList)
          return false;
      }
      return true;
    }

    public void ReachedTargetLocation(
      Vector2Int CurrentLocation,
      ref Vector2Int ForceGoHere,
      Employee Ref_Employee,
      Player player,
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      WalkingPerson parent,
      ref bool IsWalking)
    {
      if (this.zookeeperstate == CurentZooKeeperState.CannotReachStoreRoom)
      {
        if (Z_GameFlags.pathfinder.GetIsBlocked(pathnavigator.CurrentTile.X, pathnavigator.CurrentTile.Y))
          parent.TeleportToGateNextUpdate = true;
        this.zookeeperstate = CurentZooKeeperState.None;
        this.NothingLeftToFeed = false;
      }
      if (this.zookeeperstate == CurentZooKeeperState.None && !this.NothingLeftToFeed && !this.deliveryman.IsDoingDelivery)
        this.zookeeperstate = CurentZooKeeperState.GoingToStoreRoom;
      if (this.deliveryman.IsDoingDelivery)
      {
        if (this.zookeeperstate == CurentZooKeeperState.SearchingForPoop)
        {
          if (!this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _) || this.zookeeperstate == CurentZooKeeperState.CollectingPoop)
            return;
          this.FeedAnimalsTimerPickPoop = 0.0f;
          this.zookeeperstate = CurentZooKeeperState.CollectingPoop;
        }
        else
          this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
      }
      else if (this.zookeeperstate == CurentZooKeeperState.GoingToStoreRoom)
      {
        bool storeRoom = this.GoToStoreRoom(player, parent, ref ForceGoHere);
        if (!storeRoom)
        {
          TileData.IsThisACellBlock(player.prisonlayout.layout.FloorTileTypes[parent.pathnavigator.CurrentTile.X, parent.pathnavigator.CurrentTile.Y].tiletype);
          this.zookeeperstate = CurentZooKeeperState.CannotReachStoreRoom;
        }
        BlockAutoWalk = storeRoom;
      }
      else
      {
        if (this.zookeeperstate != CurentZooKeeperState.GoingToPen)
          return;
        bool goPen = this.TryToGoPen(player, parent, ref ForceGoHere);
        if (!goPen)
          this.zookeeperstate = !this.NothingLeftToFeed ? CurentZooKeeperState.CannotReachPen : CurentZooKeeperState.None;
        BlockAutoWalk = goPen;
      }
    }

    private bool TryToGoPen(Player player, WalkingPerson parent, ref Vector2Int ForceGoHere)
    {
      this.Current_Cell_UID = -1;
      if (player.livestats.hungryanimals == null || player.livestats.hungryanimals.Count == 0)
      {
        this.NothingLeftToFeed = true;
        return false;
      }
      int PenUID;
      if (!this.deliveryman.TryToStartJourneyToFirstAccessiblePenPen(player, out PenUID, parent, ref ForceGoHere))
        return false;
      this.Current_Cell_UID = PenUID;
      return true;
    }

    private bool GoToStoreRoom(Player player, WalkingPerson parent, ref Vector2Int ForceGoHere)
    {
      this.zookeeperstate = CurentZooKeeperState.GoingToStoreRoom;
      TILETYPE tiletype = this.Ref_Employee.quickemplyeedescription.WorksHere;
      if (!TileData.IsAStoreRoom(tiletype))
        tiletype = TILETYPE.StoreRoom;
      return this.deliveryman.TryGoToSpecificBuilding(tiletype, this.Ref_Employee.quickemplyeedescription.ShopUID, player, parent, ref ForceGoHere);
    }

    private bool DoFeedAnimals_InPen(Player player, out bool DidNotFindPen)
    {
      DidNotFindPen = false;
      HungryAnimal hungryanaimalset = (HungryAnimal) null;
      if (player.livestats.hungryanimals != null)
      {
        foreach (HungryAnimal hungryanimal in player.livestats.hungryanimals)
        {
          if (hungryanimal.Cell_UID == this.Current_Cell_UID)
          {
            hungryanaimalset = hungryanimal;
            break;
          }
        }
      }
      if (hungryanaimalset == null)
        return false;
      CellBlockSet cellBlockByUid = OverWorldManager.overworldenvironment.animalsinpens.GetCellBlockByUID(this.Current_Cell_UID);
      if (cellBlockByUid == null)
      {
        DidNotFindPen = true;
        return false;
      }
      UsedFoodCollection foodused = new UsedFoodCollection();
      int num = Bal_KeeperFeedsAnimals.KeeperFeedAnimals(hungryanaimalset, player, this.Ref_Employee, ref foodused, cellBlockByUid.REF_prisonzone.prisonercontainer.FoodForAnimals);
      ++this.Ref_Employee.ehistory.TotalEvents;
      this.Ref_Employee.ehistory.TotalSubEvents += num;
      if (cellBlockByUid.REF_prisonzone.prisonercontainer.prisoners.Count > 0)
        cellBlockByUid.Feed(foodused);
      return true;
    }

    private void DoGetFoodFromStoreRoom()
    {
    }
  }
}
