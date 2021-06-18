// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.FarmerController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Farm;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class FarmerController
  {
    private Employee refEmployee;
    private DeliveryManController deliveryman;
    private bool WasBlockedByCollision;
    private bool NothingToGrow;
    private LocationsByFarm refCurrentFarm;

    public FarmerController(Employee ref_employee)
    {
      this.refEmployee = ref_employee;
      this.deliveryman = new DeliveryManController();
    }

    public void StartNewDay()
    {
      this.NothingToGrow = false;
      this.WasBlockedByCollision = false;
    }

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
    }

    public void UpdateFarmerController(
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
        this.DoJob_InFarm(player, parent);
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
      this.NothingToGrow = true;
      for (int index = 0; index < Current_FarmDestinations.FarmIDsReadyForSeeding.Count; ++index)
      {
        this.NothingToGrow = false;
        if (this.deliveryman.TryToStartJourneyToPen(player, Current_FarmDestinations.FarmIDsReadyForSeeding[index].FarmUID, parent, ref ForceGoHere, GoToFarm: true))
        {
          this.refCurrentFarm = Current_FarmDestinations.FarmIDsReadyForSeeding[index];
          flag = true;
          break;
        }
      }
      if (this.NothingToGrow)
        BlockAutoWalk = false;
      else if (!flag)
      {
        this.WasBlockedByCollision = true;
        BlockAutoWalk = false;
      }
      return flag;
    }

    private bool DoJob_InFarm(Player player, WalkingPerson parent)
    {
      player.farms.GetThisFarmFieldByUID(this.refCurrentFarm.FarmUID).cropsatus.FarmerReachedPlantLocation(new Vector2Int(0, 0), player);
      Current_FarmDestinations.FarmeWentHere(this.refCurrentFarm.FarmUID, parent.pathnavigator.CurrentTile);
      return true;
    }
  }
}
