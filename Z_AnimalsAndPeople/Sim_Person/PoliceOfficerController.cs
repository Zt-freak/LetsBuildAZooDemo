// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.PoliceOfficerController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Events.BreakOut;
using TinyZoo.Z_Events.PoliceSniper;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class PoliceOfficerController
  {
    private bool HasGun;
    private AnimalRenderMan HuntingThis;
    private Vector2Int TryingToHuntHere;
    private bool IsShooting;
    private int Frame;
    private float FrameTimer;
    private DirectionPressed ShootThisWay;
    private Vector2 OriginalOrigin;
    private bool IsLeaving;
    private Rectangle OriginalRect;
    private bool HasBeenDeleted;
    public bool JustTeleported;

    public PoliceOfficerController(Employee Ref_Employee)
    {
      if (Ref_Employee.intakeperson.animaltype == AnimalType.PoliceWithGun)
        this.HasGun = true;
      this.FindANimalToHunt();
    }

    private void FindANimalToHunt()
    {
      if (!BreakOutManager.HasActiveBreakOut())
        return;
      this.HuntingThis = BreakOutManager.GetAnimalToHunt();
    }

    public void ReachedTargetLocation(
      Vector2Int CurrentLocation,
      ref Vector2Int ForceGoHere,
      Employee Ref_Employee,
      ref bool BlockAutoWalk,
      WalkingPerson parent,
      ref bool IsWalking)
    {
      if (this.IsLeaving)
      {
        if (this.HasBeenDeleted)
          return;
        if (parent.ThisPersonStartLocation.CompareMatches(CurrentLocation))
        {
          if (this.HasBeenDeleted)
            return;
          this.HasBeenDeleted = true;
          LiveStats.RemoveThisTempEmployee(parent.simperson.Ref_Employee);
          PoliceSnipermanger.DeletePoliceMan();
        }
        else
          ForceGoHere = new Vector2Int(parent.ThisPersonStartLocation);
      }
      else
      {
        if (this.IsShooting)
          return;
        if (this.HuntingThis != null)
        {
          Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(this.HuntingThis.enemyrenderere.vLocation);
          if (this.TryingToHuntHere != null)
          {
            if (worldSpaceToTile.X == CurrentLocation.X)
              this.StartShooting(parent, ref BlockAutoWalk, ref IsWalking);
            else if (worldSpaceToTile.Y == CurrentLocation.Y)
              this.StartShooting(parent, ref BlockAutoWalk, ref IsWalking);
          }
          this.TryingToHuntHere = new Vector2Int(worldSpaceToTile);
          if (this.IsShooting)
            return;
          ForceGoHere = new Vector2Int(worldSpaceToTile);
        }
        else
        {
          ForceGoHere = new Vector2Int(parent.ThisPersonStartLocation);
          this.IsLeaving = true;
        }
      }
    }

    public void UpdatePoliceOfficermanager(
      float DeltaTime,
      WalkingPerson parent,
      ref bool BlockAutoWalk,
      ref bool IsWalking)
    {
      if (this.IsLeaving || this.HasBeenDeleted)
        return;
      if (this.IsShooting)
      {
        this.FrameTimer += DeltaTime;
        if (this.Frame == 0)
        {
          if ((double) this.FrameTimer > 0.5)
          {
            this.FrameTimer = 0.0f;
            ++this.Frame;
          }
        }
        else if ((double) this.FrameTimer > 0.100000001490116)
        {
          this.FrameTimer -= 0.1f;
          ++this.Frame;
        }
        if (this.Frame > 3)
        {
          this.Frame = 3;
          this.IsShooting = false;
          BlockAutoWalk = false;
          this.HuntingThis.REF_prisonerinfo.IsDead = true;
          this.HuntingThis.REF_prisonerinfo.causeofdeath = CauseOfDeath.KilledByThePolice;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.Gun);
          BreakOutManager.ScrubDeadAnimals();
          this.HuntingThis.CheckDeath();
          this.HuntingThis = (AnimalRenderMan) null;
          this.FindANimalToHunt();
          parent.FlipRender = parent.directionmoving == DirectionPressed.Left;
        }
        else
        {
          if (this.Frame >= 4)
            return;
          parent.FlipRender = false;
          switch (this.ShootThisWay)
          {
            case DirectionPressed.Up:
              parent.DrawRect = new Rectangle(1617 + 13 * this.Frame, 432, 12, 25);
              break;
            case DirectionPressed.Right:
              parent.DrawRect = new Rectangle(1676 + 30 * this.Frame, 290, 29, 19);
              break;
            case DirectionPressed.Down:
              parent.DrawRect = new Rectangle(1587 + 14 * this.Frame, 528, 13, 30);
              break;
            case DirectionPressed.Left:
              parent.FlipRender = true;
              parent.DrawRect = new Rectangle(1676 + 30 * this.Frame, 290, 29, 19);
              break;
          }
        }
      }
      else if (this.JustTeleported)
      {
        this.IsShooting = false;
        this.FindANimalToHunt();
        IsWalking = false;
        this.JustTeleported = false;
      }
      else
      {
        if (this.HuntingThis == null || (double) (parent.vLocation - this.HuntingThis.enemyrenderere.vLocation).Length() >= 150.0)
          return;
        if ((double) parent.vLocation.Y == (double) this.HuntingThis.enemyrenderere.vLocation.Y)
        {
          this.StartShooting(parent, ref BlockAutoWalk, ref IsWalking);
        }
        else
        {
          if ((double) parent.vLocation.X != (double) this.HuntingThis.enemyrenderere.vLocation.X)
            return;
          this.StartShooting(parent, ref BlockAutoWalk, ref IsWalking);
        }
      }
    }

    private void StartShooting(WalkingPerson parent, ref bool BlockAutoWalk, ref bool IsWalking)
    {
      this.OriginalRect = parent.DrawRect;
      IsWalking = false;
      this.OriginalOrigin = parent.DrawOrigin;
      this.Frame = 0;
      BlockAutoWalk = true;
      this.IsShooting = true;
      parent.FlipRender = false;
      if ((double) parent.vLocation.X == (double) this.HuntingThis.enemyrenderere.vLocation.X || (double) parent.vLocation.Y != (double) this.HuntingThis.enemyrenderere.vLocation.Y && (double) Math.Abs(parent.vLocation.X - this.HuntingThis.enemyrenderere.vLocation.X) < (double) Math.Abs(parent.vLocation.Y - this.HuntingThis.enemyrenderere.vLocation.Y))
      {
        if ((double) parent.vLocation.Y <= (double) this.HuntingThis.enemyrenderere.vLocation.Y)
          this.ShootThisWay = DirectionPressed.Up;
        else
          this.ShootThisWay = DirectionPressed.Down;
      }
      else if ((double) parent.vLocation.X <= (double) this.HuntingThis.enemyrenderere.vLocation.X)
      {
        this.ShootThisWay = DirectionPressed.Right;
      }
      else
      {
        this.ShootThisWay = DirectionPressed.Left;
        parent.FlipRender = true;
      }
    }
  }
}
