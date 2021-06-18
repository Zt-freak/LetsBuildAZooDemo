// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.MeatProcessorController
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
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class MeatProcessorController
  {
    private DeliveryManController deliveryman;
    private Employee Ref_Employee;
    private bool NoMoreCorpses;
    private int currentPenUID;
    private float pickUpTimer;
    private float TimeNeededToPickUpCorpse = 3f;
    private float timerAtIncinerator;
    private float TimeNeededToPutCorpseInIncincerator = 3f;
    private List<DeadAnimal> corpseHeld;
    private bool WasBlockedByCollision;
    private MeatProcessorController.MeatProcessorDestination currentDestination;
    private ShopEntry pickedCorpseFromHere_Processor;
    private PrisonZone pickedCorpseFromHere_Pen;
    private ShopEntry destinationShopEntry;

    public MeatProcessorController(Employee employee)
    {
      this.Ref_Employee = employee;
      this.deliveryman = new DeliveryManController();
      this.corpseHeld = new List<DeadAnimal>();
    }

    public void StartNewDay()
    {
      this.NoMoreCorpses = false;
      this.WasBlockedByCollision = false;
    }

    public void CheckJob() => this.NoMoreCorpses = false;

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
    }

    public void UpdateMeatProcessorController(
      float DeltaTime,
      WalkingPerson parent,
      ref bool IsPlayingWalkAnimation,
      ref bool BlockAutoWalk,
      Player player)
    {
      if (this.NoMoreCorpses)
        return;
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
        if (this.currentDestination == MeatProcessorController.MeatProcessorDestination.PickCorpses_Pen || this.currentDestination == MeatProcessorController.MeatProcessorDestination.PickCorpses_MeatProcessor)
        {
          this.pickUpTimer += DeltaTime;
          if ((double) this.pickUpTimer <= (double) this.TimeNeededToPickUpCorpse)
            return;
          this.pickUpTimer = 0.0f;
          this.TryToCollectCorpseFromDestination(player, parent);
          this.deliveryman.ExitJobLocation();
        }
        else
        {
          this.timerAtIncinerator += DeltaTime;
          if ((double) this.timerAtIncinerator <= (double) this.TimeNeededToPutCorpseInIncincerator)
            return;
          this.timerAtIncinerator = 0.0f;
          this.DumpCorpseInDestination(player);
          this.deliveryman.ExitJobLocation();
        }
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
        return;
      if (this.deliveryman.IsDoingDelivery)
      {
        this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
      }
      else
      {
        if (!Z_GameFlags.IsDay)
          return;
        if (this.corpseHeld.Count > 0)
        {
          this.TryHeadBackToIncineratorOrMeatProcessor(player, parent, ref ForceGoHere, ref BlockAutoWalk);
        }
        else
        {
          if (!Z_GameFlags.IsDay)
            return;
          this.TryToGoToNextCorpse(player, parent, ref ForceGoHere, ref BlockAutoWalk);
        }
      }
    }

    private bool TryToGoToNextCorpse(
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool BlockAutoWalk)
    {
      this.pickedCorpseFromHere_Pen = (PrisonZone) null;
      this.pickedCorpseFromHere_Processor = (ShopEntry) null;
      this.destinationShopEntry = (ShopEntry) null;
      int num = FactoryDeliveryHelper.FindAndGoToCollectCorpse(this.Ref_Employee, this.deliveryman, player, parent, ref ForceGoHere, out this.WasBlockedByCollision, out this.NoMoreCorpses, ref this.currentPenUID, ref this.destinationShopEntry) ? 1 : 0;
      if (num == 0)
        BlockAutoWalk = false;
      if (this.destinationShopEntry != null && TileData.IsAMeatProcessingPlant(this.destinationShopEntry.tiletype))
      {
        this.currentDestination = MeatProcessorController.MeatProcessorDestination.PickCorpses_MeatProcessor;
        return num != 0;
      }
      this.currentDestination = MeatProcessorController.MeatProcessorDestination.PickCorpses_Pen;
      return num != 0;
    }

    private bool TryToCollectCorpseFromDestination(Player player, WalkingPerson parent)
    {
      if (this.currentDestination == MeatProcessorController.MeatProcessorDestination.PickCorpses_MeatProcessor)
      {
        this.pickedCorpseFromHere_Processor = this.destinationShopEntry;
        return FactoryDeliveryHelper.TryToCollectThingsFromHere(this.destinationShopEntry, (PrisonZone) null, ref this.corpseHeld, parent);
      }
      if (this.currentDestination != MeatProcessorController.MeatProcessorDestination.PickCorpses_Pen)
        return false;
      PrisonZone destinationIsPen = (PrisonZone) null;
      foreach (PrisonZone prisonzone in player.prisonlayout.cellblockcontainer.prisonzones)
      {
        if (prisonzone.Cell_UID == this.currentPenUID)
        {
          destinationIsPen = prisonzone;
          this.pickedCorpseFromHere_Pen = destinationIsPen;
          break;
        }
      }
      int num = FactoryDeliveryHelper.TryToCollectThingsFromHere((ShopEntry) null, destinationIsPen, ref this.corpseHeld, parent) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.pickedCorpseFromHere_Pen = destinationIsPen;
      return num != 0;
    }

    private bool TryHeadBackToIncineratorOrMeatProcessor(
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      ref bool BlockAutoWalk)
    {
      if (FactoryDeliveryHelper.GoBackToHomeBuilding(player, parent, ref ForceGoHere, this.Ref_Employee, this.deliveryman, out this.destinationShopEntry))
      {
        this.currentDestination = MeatProcessorController.MeatProcessorDestination.PlaceCorpses;
        return true;
      }
      this.WasBlockedByCollision = true;
      BlockAutoWalk = false;
      return false;
    }

    private void DropCorpseOnFloor()
    {
    }

    private bool DumpCorpseInDestination(Player player) => FactoryDeliveryHelper.OnDeliveryDestinationReached_Dropoff(this.destinationShopEntry, this.pickedCorpseFromHere_Processor, this.pickedCorpseFromHere_Pen, ref this.corpseHeld);

    private enum MeatProcessorDestination
    {
      None,
      PickCorpses_Pen,
      PickCorpses_MeatProcessor,
      PlaceCorpses,
      Count,
    }
  }
}
