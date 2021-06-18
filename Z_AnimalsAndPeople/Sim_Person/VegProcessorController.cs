// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.VegProcessorController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Farm;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class VegProcessorController
  {
    private DeliveryManController deliveryman;
    private Employee refEmployee;
    private ShopEntry destinationShopEntry;
    private LocationsByFarm destinationFarm;
    private PrisonZone destinationFarmZone;
    private List<DeadAnimal> cropsHeld;
    private bool NoMoreFarmsToHarvest;
    private bool WasBlockedByCollision;

    public VegProcessorController(Employee employee)
    {
      this.refEmployee = employee;
      this.deliveryman = new DeliveryManController();
      this.cropsHeld = new List<DeadAnimal>();
    }

    public void StartNewDay()
    {
      this.WasBlockedByCollision = false;
      this.NoMoreFarmsToHarvest = false;
    }

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
    }

    public void UpdateVegProcessorController(
      float DeltaTime,
      WalkingPerson parent,
      ref bool IsPlayingWalkAnimation,
      ref bool BlockAutoWalk,
      Player player)
    {
      if (!this.deliveryman.IsDoingDelivery)
        return;
      if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting)
      {
        if (this.cropsHeld.Count > 0)
          this.DropOffHarvestInProcessor();
        else
          this.HarvestFromFarm(player, parent);
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
      if (this.deliveryman.IsDoingDelivery)
      {
        this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
      }
      else
      {
        if (!Z_GameFlags.IsDay)
          return;
        if (this.cropsHeld.Count > 0)
          this.GoToVegetableProcessor(player, parent, ref ForceGoHere, ref BlockAutoWalk);
        else
          this.TryToGoToAFarm(player, parent, ref ForceGoHere, ref BlockAutoWalk);
      }
    }

    private bool TryToGoToAFarm(
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool BlockAutoWalk)
    {
      bool flag = false;
      this.NoMoreFarmsToHarvest = true;
      this.destinationFarm = (LocationsByFarm) null;
      this.destinationShopEntry = (ShopEntry) null;
      this.destinationFarmZone = (PrisonZone) null;
      for (int index = 0; index < Current_FarmDestinations.FarmIDsReadyForHarvesting.Count; ++index)
      {
        this.NoMoreFarmsToHarvest = false;
        if (this.deliveryman.TryToStartJourneyToPen(player, Current_FarmDestinations.FarmIDsReadyForHarvesting[index].FarmUID, parent, ref ForceGoHere, GoToFarm: true))
        {
          this.destinationFarm = Current_FarmDestinations.FarmIDsReadyForHarvesting[index];
          flag = true;
          break;
        }
      }
      if (this.NoMoreFarmsToHarvest)
        BlockAutoWalk = false;
      else if (!flag)
      {
        this.WasBlockedByCollision = true;
        BlockAutoWalk = false;
      }
      return flag;
    }

    private bool HarvestFromFarm(Player player, WalkingPerson parent)
    {
      this.destinationFarmZone = player.farms.GetThisFarmFieldByUID(this.destinationFarm.FarmUID);
      float Harvested;
      this.destinationFarmZone.cropsatus.CropPickerReachedPlantLocation(new Vector2Int(0, 0), player, out Harvested);
      bool flag = false;
      for (int index = 0; index < this.cropsHeld.Count; ++index)
      {
        if (this.cropsHeld[index].cropType == this.destinationFarmZone.cropsatus.cropgrowinghere)
        {
          this.cropsHeld[index].weight += Harvested;
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        DeadAnimal deadAnimal = new DeadAnimal(this.destinationFarmZone.cropsatus.cropgrowinghere);
        deadAnimal.weight += Harvested;
        if ((double) Harvested > 0.0)
          this.cropsHeld.Add(deadAnimal);
      }
      Current_FarmDestinations.FarmeWentHere(this.destinationFarm.FarmUID, parent.pathnavigator.CurrentTile);
      return true;
    }

    private bool GoToVegetableProcessor(
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool BlockAutoWalk)
    {
      return FactoryDeliveryHelper.GoBackToHomeBuilding(player, parent, ref ForceGoHere, this.refEmployee, this.deliveryman, out this.destinationShopEntry);
    }

    private bool DropOffHarvestInProcessor() => FactoryDeliveryHelper.OnDeliveryDestinationReached_Dropoff(this.destinationShopEntry, (ShopEntry) null, this.destinationFarmZone, ref this.cropsHeld);
  }
}
