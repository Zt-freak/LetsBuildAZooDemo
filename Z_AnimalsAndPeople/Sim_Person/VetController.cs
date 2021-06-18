// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.VetController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class VetController
  {
    private DeliveryManController deliveryman;
    private Employee Ref_Employee;
    private bool HasNoPens;
    private PrisonZone refprisonzone;
    private float TimerToCheckThisAnimal;
    private int AnimalsChecked;

    public VetController(Employee _Ref_Employee)
    {
      this.Ref_Employee = _Ref_Employee;
      this.deliveryman = new DeliveryManController();
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
        this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
      if (this.HasNoPens)
        return;
      if (!this.deliveryman.IsDoingDelivery)
      {
        int PenUID;
        if (this.deliveryman.TryToStartJourneyToFirstAccessiblePenPen(player, out PenUID, parent, ref ForceGoHere))
        {
          this.refprisonzone = player.prisonlayout.GetThisCellBlock(PenUID);
          this.AnimalsChecked = 0;
        }
        else
          this.HasNoPens = true;
      }
      this.deliveryman.ReachedTargetLocation(CurrentLocation, ref BlockAutoWalk, ref IsWalking, out bool _);
    }

    public void UpdateVetController(
      float DeltaTime,
      Player player,
      WalkingPerson parent,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation)
    {
      if (!this.deliveryman.IsDoingDelivery)
        return;
      if (this.deliveryman.deliveryguystatus == DeliveryGuyStatus.AtJobWaiting)
      {
        if (this.AnimalsChecked < this.refprisonzone.prisonercontainer.prisoners.Count)
        {
          BlockAutoWalk = false;
          this.TimerToCheckThisAnimal += DeltaTime;
          if ((double) this.TimerToCheckThisAnimal <= 10.0)
            return;
          this.TimerToCheckThisAnimal = 0.0f;
          this.refprisonzone.prisonercontainer.prisoners[this.AnimalsChecked].DoVetCheck(player);
          ++this.AnimalsChecked;
        }
        else
        {
          parent.pathnavigator.TeleportHere(this.deliveryman.InsideLocation);
          this.deliveryman.ExitJobLocation();
        }
      }
      else
        this.deliveryman.UpdateDelivery(DeltaTime, ref IsPlayingWalkAnimation, player, parent, ref BlockAutoWalk, out bool _);
    }
  }
}
