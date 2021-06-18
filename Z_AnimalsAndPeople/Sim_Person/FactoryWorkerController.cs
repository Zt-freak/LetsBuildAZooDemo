// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.FactoryWorkerController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class FactoryWorkerController
  {
    private Employee refEmployee;
    private DeliveryManController deliveryman;
    private ShopEntry cameFromHere;
    private ShopEntry destination;
    private int stockHeld;
    private Dictionary<AnimalFoodType, int> warehouseInventory;
    private bool WasBlockedByCollision;
    private bool NothingLeft;

    public FactoryWorkerController(Employee _refEmployee)
    {
      this.refEmployee = _refEmployee;
      this.deliveryman = new DeliveryManController();
      if (_refEmployee.employeetype != EmployeeType.WarehouseWorker)
        return;
      this.warehouseInventory = new Dictionary<AnimalFoodType, int>();
    }

    public void StartNewDay()
    {
      this.NothingLeft = false;
      this.WasBlockedByCollision = false;
    }

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
    }

    public void UpdateFactoryWorkerController(
      float DeltaTime,
      WalkingPerson parent,
      ref bool IsPlayingWalkAnimation,
      ref bool BlockAutoWalk,
      Player player)
    {
      if (this.WasBlockedByCollision)
      {
        if (!GameFlags.CollisionChanged)
          return;
        this.WasBlockedByCollision = false;
      }
      if (!this.deliveryman.IsDoingDelivery)
        return;
      if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting)
      {
        if (this.refEmployee.employeetype == EmployeeType.WarehouseWorker)
        {
          if (TileData.IsAWarehouse(this.destination.tiletype))
            FactoryDeliveryHelper.OnDeliveryDestinationReached_Dropoff(this.destination, this.cameFromHere, player, ref this.warehouseInventory);
          else
            FactoryDeliveryHelper.TryToCollectThingsFromHere(this.destination, ref this.warehouseInventory, this.refEmployee);
        }
        else if (TileData.IsAMeatProcessingPlant(this.destination.tiletype) || TileData.IsAVegetableProcessingPlant(this.destination.tiletype))
          FactoryDeliveryHelper.TryToCollectThingsFromHere(this.destination, ref this.stockHeld, this.refEmployee);
        else if (TileData.IsAFactory(this.destination.tiletype))
          FactoryDeliveryHelper.OnDeliveryDestinationReached_Dropoff(this.destination, this.cameFromHere, ref this.stockHeld, PcessedMeatData.GetProductInputForFactory(this.refEmployee.quickemplyeedescription.WorksHere));
        this.cameFromHere = this.destination;
        this.deliveryman.ExitJobLocation();
      }
      else
        this.deliveryman.UpdateDelivery(DeltaTime, ref IsPlayingWalkAnimation, player, parent, ref BlockAutoWalk, out bool _);
    }

    public void ReachedTargetLocation(
      Vector2Int CurrentLocation,
      ref Vector2Int ForceGoHere,
      Player player,
      Employee Ref_Employee,
      ref bool BlockAutoWalk,
      WalkingPerson parent,
      ref bool IsWalking)
    {
      if (this.WasBlockedByCollision)
      {
        if (!GameFlags.CollisionChanged)
          return;
        this.WasBlockedByCollision = false;
      }
      if (this.deliveryman.IsDoingDelivery)
      {
        this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
      }
      else
      {
        if (!Z_GameFlags.IsDay)
          return;
        if (this.stockHeld > 0 || this.warehouseInventory != null && this.warehouseInventory.Count > 0)
        {
          this.cameFromHere = this.destination;
          if (FactoryDeliveryHelper.GoBackToHomeBuilding(player, parent, ref ForceGoHere, this.refEmployee, this.deliveryman, out this.destination))
            return;
          this.WasBlockedByCollision = true;
          BlockAutoWalk = false;
        }
        else
        {
          if (FactoryDeliveryHelper.FindAndGoToABuildingToCollectThingsFrom(this.refEmployee, this.deliveryman, player, parent, ref ForceGoHere, out this.WasBlockedByCollision, out this.NothingLeft, ref this.destination))
            return;
          BlockAutoWalk = false;
        }
      }
    }
  }
}
