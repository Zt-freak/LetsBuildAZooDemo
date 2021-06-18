// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Customers.WalkingPerson
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_Bus;
using TinyZoo.Z_Data;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.OverWorld.OverWorldEnv.Customers
{
  internal class WalkingPerson : AnimatedGameObject
  {
    public bool IsOnTravelator;
    private PersonAttachmentManager personattachmentmanager;
    internal static Vector2Int LogoLocation = new Vector2Int(162, 224);
    public SimPerson simperson;
    private EnemyInfoPack tispersoninfo;
    public PathNavigator pathnavigator;
    private bool IsPlayingWalkAnimation;
    public Vector2Int ThisPersonStartLocation;
    public DirectionPressed directionmoving;
    private bool ReturningToBus;
    public bool IsAtive;
    public bool IsOnFinalWalkToBus;
    public AnimalType thispersontype;
    public bool IsEmployee;
    private bool BlockAutoWalk;
    public bool HasPaid;
    private bool ForceAutoPopUp;
    private bool UseSimpleNavigation;
    public StatusIcon statusicon;
    private DirectionPressed CameFromHere;
    private bool WalkPaused;
    public AnimalRenderMan animalrenderer;
    private DynamicEnrichmentItem dynamicenrichmentitem;
    public bool HasExtraOffset;
    private bool HasExtraOffsetIsY;
    private int ExtraOffsetY_Sort;
    private Vector2 ExtraOffset;
    private int TileOffsetYForSort;
    public bool TeleportToGateNextUpdate;
    internal static int MouseOverUID;
    internal static bool SkipSmartCuror;
    public float HoldTime;
    private static Vector2Int TopLeft;
    private static Vector2Int BottomRight;
    private static Vector2 Ploc;
    private static Vector2 AVec = new Vector2();
    private static Vector2 BVec = new Vector2();
    private static Vector2 VSCALE = Vector2.One;
    private static bool[] UpRightDownLeft = new bool[4];
    public float TrailerRenderDelay;
    private static Vector2 ThreadLoc;
    private static Vector2 ThreadScale;

    public int UID { get; private set; }

    public WalkingPerson(AnimalRenderMan _animalrenderer)
    {
      this.UID = _animalrenderer.REF_prisonerinfo.intakeperson.UID;
      this.animalrenderer = _animalrenderer;
    }

    public WalkingPerson(DynamicEnrichmentItem _dynamicenrichmentitem)
    {
      this.dynamicenrichmentitem = _dynamicenrichmentitem;
      this.UID = _dynamicenrichmentitem.UID;
    }

    public void RandomizeLocation(Player player, int YMIN = 0)
    {
      bool flag = false;
      while (!flag)
      {
        Vector2Int locationInSector = TileMath.GetRandomLocationInSector(TileMath.GetRandomLocationInPlaySpace());
        if (locationInSector.Y >= YMIN && !Z_GameFlags.pathfinder.GetIsBlocked(locationInSector.X, locationInSector.Y) && !TileData.IsThisAPenFloor(player.prisonlayout.layout.FloorTileTypes[locationInSector.X, locationInSector.Y].tiletype))
        {
          this.IsPlayingWalkAnimation = false;
          this.pathnavigator.TeleportHere(locationInSector);
          flag = true;
        }
      }
    }

    public void SetExtraOffset(Vector2 ExtraOff, int _TileOffsetYForSort)
    {
      this.TileOffsetYForSort = _TileOffsetYForSort;
      this.HasExtraOffset = true;
      ExtraOff.Y -= (float) this.TileOffsetYForSort * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.ExtraOffset = ExtraOff;
      this.vLocation.Y += (float) this.TileOffsetYForSort * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
      if ((double) ExtraOff.Y == 0.0)
        return;
      this.ExtraOffsetY_Sort = 7;
      this.HasExtraOffsetIsY = true;
      if ((double) ExtraOff.Y >= 0.0)
        return;
      this.ExtraOffsetY_Sort = -7;
    }

    public void CancelExtraOffset()
    {
      this.HasExtraOffset = false;
      this.HasExtraOffsetIsY = false;
      this.ExtraOffsetY_Sort = 0;
      this.TileOffsetYForSort = 0;
      this.vLocation = TileMath.GetTileToWorldSpace(this.pathnavigator.CurrentTile);
      this.vLocation = this.vLocation + this.pathnavigator.TileRelativeLocation * Sengine.ScreenRatioUpwardsMultiplier * TileMath.TileSize * 0.5f;
      if (this.TileOffsetYForSort == 0)
        return;
      this.vLocation.Y += (float) this.TileOffsetYForSort * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void OverwriteGraphicsAndPersonType(AnimalType persontype)
    {
      this.thispersontype = persontype;
      this.tispersoninfo = EnemyData.GetEnemyRectangle(this.thispersontype);
    }

    public WalkingPerson(
      int _PersonIndex,
      AnimalType persontype = AnimalType.None,
      Employee employee = null,
      bool IsRenderOnly = false,
      CellblockMananger cellblockcontainer = null,
      Player player = null,
      int ShopUID = -1)
    {
      this.CameFromHere = DirectionPressed.None;
      this.IsAtive = true;
      Z_WorldExpansion.GetSizes(0, out WalkingPerson.TopLeft, out WalkingPerson.BottomRight, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      this.UID = _PersonIndex;
      if (persontype == AnimalType.None)
        persontype = (AnimalType) TinyZoo.Game1.Rnd.Next(71, 141);
      EmployeeType employeetype;
      this.IsEmployee = EmployeeData.IsThisAnEmployee(persontype, out employeetype);
      this.simperson = new SimPerson(employee, this.IsEmployee, persontype, IsRenderOnly, cellblockcontainer, player, ShopUID, this);
      this.thispersontype = persontype;
      this.tispersoninfo = EnemyData.GetEnemyRectangle(this.thispersontype);
      this.DrawRect = this.tispersoninfo.WalkUp;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.ThisPersonStartLocation = new Vector2Int(WalkingPerson.LogoLocation.X + TinyZoo.Game1.Rnd.Next(-1, 2), WalkingPerson.LogoLocation.Y + 1);
      if (this.IsEmployee && employeetype == EmployeeType.Keeper)
        this.ThisPersonStartLocation.X = WalkingPerson.LogoLocation.X;
      this.vLocation = TileMath.GetTileToWorldSpace(this.ThisPersonStartLocation);
      if (Z_GameFlags.pathfinder.GetIsBlocked(this.ThisPersonStartLocation.X, this.ThisPersonStartLocation.Y))
        Z_GameFlags.pathfinder.UnblockTile(this.ThisPersonStartLocation.X, this.ThisPersonStartLocation.Y);
      this.directionmoving = DirectionPressed.Up;
      float _MovementSpeed = 1.1f;
      if (this.simperson.roleinsociety == RoleInSociety.Customer)
        _MovementSpeed = (float) (1.10000002384186 + (double) this.simperson.memberofthepublic.customerneeds.Fitness * 0.666599988937378);
      this.pathnavigator = new PathNavigator(this.ThisPersonStartLocation, _MovementSpeed, false, NavigationTypeEnum.TileCenter);
      this.SetEmplyeeMovementSpeed();
      this.SetUpSimpleAnimation(4, 0.2f);
      this.IsPlayingWalkAnimation = false;
      if (this.simperson.roleinsociety != RoleInSociety.Customer && this.simperson.roleinsociety != RoleInSociety.BlackMarket)
        return;
      this.statusicon = new StatusIcon();
      this.UseSimpleNavigation = true;
      if (this.simperson.roleinsociety != RoleInSociety.BlackMarket)
        return;
      this.statusicon.SetStatucIconType(StatusIconType.BlackMarket);
    }

    public void SetEmplyeeMovementSpeed()
    {
      if (this.simperson.Ref_Employee == null)
        return;
      this.pathnavigator.MovementSpeed = 1.1f;
      this.pathnavigator.SetMovementSpeed(this.simperson.Ref_Employee.quickemplyeedescription.GetTopMovementSpeed());
    }

    public bool IsThisEmployee(Employee employeetoremove) => this.simperson.Ref_Employee == employeetoremove;

    public void RemoveAttachment(PersonAttachementType attachmenttype)
    {
      if (this.personattachmentmanager == null)
        return;
      this.personattachmentmanager.RemoveAttachment(attachmenttype);
    }

    public bool AddAttachment(PersonAttachementType attachmenttype)
    {
      if (this.personattachmentmanager == null)
        this.personattachmentmanager = new PersonAttachmentManager();
      return this.personattachmentmanager.AddItem(attachmenttype, this);
    }

    public void ForceRotationAndHold(DirectionPressed direction, float _HoldTime)
    {
      this.HoldTime = _HoldTime;
      if (direction == DirectionPressed.None)
        return;
      this.directionmoving = direction;
      this.SetFrame();
    }

    public void SetRecall()
    {
      if (this.IsEmployee)
        return;
      this.ReturningToBus = true;
      if (this.simperson.memberofthepublic == null)
        return;
      this.simperson.memberofthepublic.CancelActionAndStartReturn(ref this.BlockAutoWalk, ref this.WalkPaused, this);
    }

    public void UpdateNeedsAndWants(float Cycles, Player player) => this.simperson.UpdateNeedsAndWants(Cycles, player, this);

    public bool UpdateWalkingPerson(
      float DeltaTime,
      Player player,
      out bool RemoveThisPerson,
      Vector2 Mouse_WORLDLOC)
    {
      RemoveThisPerson = false;
      if (!this.IsAtive)
        return false;
      if (this.simperson.IsDead)
        return true;
      if (this.ReturningToBus && CustomerManager.SpeedUpRecall)
        DeltaTime *= 4f;
      if (GameFlags.CollisionChanged || this.TeleportToGateNextUpdate)
      {
        if (this.TeleportToGateNextUpdate || Z_GameFlags.pathfinder.GetIsBlocked(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y) || Z_GameFlags.IsTheBlockedByNewPenFloor(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y))
        {
          bool flag = true;
          if (this.simperson.roleinsociety == RoleInSociety.Employee)
          {
            if (this.simperson.Ref_Employee.employeetype == EmployeeType.ShopKeeper)
            {
              if (this.simperson.shopvendor.ShouldNotTeleportFromBlockedTile())
                flag = false;
            }
            else if (this.simperson.Ref_Employee.employeetype == EmployeeType.ShopKeeper && this.simperson.keepercontroller.ShouldNotTeleportFromBlockedTile(ref this.BlockAutoWalk, ref this.IsPlayingWalkAnimation))
              flag = false;
          }
          if (flag)
          {
            this.TeleportToGateNextUpdate = false;
            this.pathnavigator.TeleportHere(this.ThisPersonStartLocation);
            this.IsPlayingWalkAnimation = false;
            this.simperson.TeleportedToGate(ref this.BlockAutoWalk, ref this.IsPlayingWalkAnimation);
            if (this.simperson.roleinsociety == RoleInSociety.Employee && this.simperson.Ref_Employee.employeetype == EmployeeType.ShopKeeper)
              this.simperson.shopvendor.TeleportedToGate();
          }
        }
        else if (this.IsPlayingWalkAnimation)
        {
          PathNode nodeOnCurrentPath = this.pathnavigator.GetLastNodeOnCurrentPath();
          if (nodeOnCurrentPath != null)
          {
            if (!this.pathnavigator.TryToGoHere(nodeOnCurrentPath.Location, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
            {
              this.IsPlayingWalkAnimation = false;
              this.TryToWalk(player);
            }
          }
          else
          {
            this.IsPlayingWalkAnimation = false;
            this.TryToWalk(player);
          }
        }
      }
      if (this.simperson.roleinsociety != RoleInSociety.Avatar || OverWorldManager.overworldstate != OverWOrldState.PlayingAsAvatar && !Z_GameFlags.IsWaitingToReturnToControllerWalk)
      {
        if ((double) this.HoldTime > 0.0)
        {
          this.HoldTime -= DeltaTime;
          if ((double) this.HoldTime <= 0.0)
          {
            if (this.simperson.memberofthepublic != null)
              this.simperson.memberofthepublic.HoldTimeComplete();
            int num = this.HasExtraOffset ? 1 : 0;
          }
          return false;
        }
        if (!this.IsPlayingWalkAnimation)
        {
          if (this.ReturningToBus)
          {
            if (!this.IsOnFinalWalkToBus)
              this.TryToReturnToBus();
          }
          else if (!this.GetDecidedNotToPay() && (this.simperson.customertype == CustomerType.Normal || !this.simperson.memberofthepublic.BlockWalkFroSpecialCharacter(this)) && (this.simperson == null || this.simperson.memberofthepublic == null || !this.simperson.memberofthepublic.WaitingToUseShop))
          {
            if (this.simperson.Ref_Employee != null)
            {
              int employeetype = (int) this.simperson.Ref_Employee.employeetype;
            }
            this.TryToWalk(player);
          }
        }
        int pathLength1 = this.pathnavigator.GetPathLength();
        float num1 = 1f;
        if (!this.ReturningToBus)
          num1 = this.simperson.GetEnergyWalkSpeedMultiplier();
        int customertype = (int) this.simperson.customertype;
        if (!this.WalkPaused)
        {
          if (Z_GameFlags.breakoutAnimals.Count > 0 && (this.simperson.roleinsociety != RoleInSociety.Employee || !Z_DebugFlags.EmployeesCannotBeKilledByAnimals) && (this.simperson.roleinsociety != RoleInSociety.Avatar && (this.simperson.roleinsociety != RoleInSociety.Employee || this.simperson.Ref_Employee.employeetype != EmployeeType.Police)))
          {
            for (int index = 0; index < Z_GameFlags.breakoutAnimals.Count; ++index)
            {
              if (!Z_GameFlags.breakoutAnimals[index].Ref_prisoninfo.IsDead && this.pathnavigator.CurrentTile.CompareMatches(Z_GameFlags.breakoutAnimals[index].Location))
              {
                this.KillThisPerson();
                return true;
              }
            }
          }
          if (this.pathnavigator.UpdatePathNavigator(DeltaTime * num1 * PathFindingManager.GetFloorSpeed(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y)))
          {
            this.IsPlayingWalkAnimation = false;
            this.simperson.WalkedUsedEnergy(pathLength1);
          }
          else
          {
            int pathLength2 = this.pathnavigator.GetPathLength();
            if (pathLength2 < pathLength1)
              this.simperson.WalkedUsedEnergy(pathLength1 - pathLength2);
          }
        }
        if (!this.IsEmployee)
        {
          if (this.HasPaid)
          {
            if (this.pathnavigator.MovedTile && PathFindingManager.entranceblockmanager.PointOfInterst(this.pathnavigator.CurrentTile))
              this.simperson.memberofthepublic.HitPointOfInterest(this.pathnavigator.CurrentTile, player, this);
          }
          else if (this.pathnavigator.CurrentTile.Y < this.ThisPersonStartLocation.Y)
          {
            if (this.simperson.roleinsociety == RoleInSociety.BlackMarket)
            {
              this.HasPaid = true;
              this.simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
            }
            else if (this.simperson.customertype != CustomerType.Normal && this.simperson.customertype != CustomerType.Footballer)
            {
              this.HasPaid = true;
              this.simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
            }
            else
            {
              bool TryToReturnToBus;
              CustomerTicketDecider.CustomerBuyTicketEvent(this.simperson, ref this.HasPaid, player, this.vLocation, ref this.IsPlayingWalkAnimation, out TryToReturnToBus);
              if (TryToReturnToBus && !this.IsOnFinalWalkToBus)
                this.TryToReturnToBus();
            }
            if (!this.ForceAutoPopUp)
            {
              if (this.simperson != null && this.simperson.roleinsociety == RoleInSociety.BlackMarket)
                this.simperson.ForcePopUpOnEnterPark = OverWorldManager.zoopopupHolder.IsNull() && !player.Stats.TutorialsComplete[31] || true;
              else
                this.ForceAutoPopUp = true;
            }
          }
        }
        if ((double) this.HoldTime <= 0.0 && !this.WalkPaused && (this.pathnavigator.directionmovedthisframe != this.directionmoving && this.pathnavigator.directionmovedthisframe != DirectionPressed.None))
        {
          this.directionmoving = this.pathnavigator.directionmovedthisframe;
          this.SetFrame();
        }
        this.vLocation = TileMath.GetTileToWorldSpace(this.pathnavigator.CurrentTile);
        this.vLocation = this.vLocation + this.pathnavigator.TileRelativeLocation * Sengine.ScreenRatioUpwardsMultiplier * TileMath.TileSize * 0.5f;
        if (this.TileOffsetYForSort != 0)
          this.vLocation.Y += (float) this.TileOffsetYForSort * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
        if (this.IsPlayingWalkAnimation && !this.WalkPaused)
        {
          this.UpdateAnimation(DeltaTime * num1);
          if (this.personattachmentmanager != null)
            this.personattachmentmanager.UpdatePersonAttachmentManager(this, DeltaTime);
        }
        if (this.IsOnFinalWalkToBus && !this.IsPlayingWalkAnimation && Z_BusManager.TryToPutThisCustomerOnABus())
        {
          RemoveThisPerson = true;
          this.IsAtive = false;
        }
      }
      if (WalkingPerson.MouseOverUID == -1)
      {
        WalkingPerson.Ploc.X = this.vLocation.X;
        WalkingPerson.Ploc.Y = this.vLocation.Y + this.scale * ((float) this.DrawRect.Height * -0.5f);
        if (MathStuff.CheckPointCollision(true, ref WalkingPerson.Ploc, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, ref Mouse_WORLDLOC))
        {
          if (WalkingPerson.MouseOverUID == -1)
          {
            WalkingPerson.SkipSmartCuror = true;
            WalkingPerson.MouseOverUID = this.UID;
          }
          if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
            OverWorldManager.zoopopupHolder.CreateZooPopUps(this, player);
        }
      }
      if (this.statusicon != null && this.statusicon.IsImportant && this.statusicon.CheckMouseOver(this.vLocation, Mouse_WORLDLOC))
      {
        WalkingPerson.SkipSmartCuror = true;
        WalkingPerson.MouseOverUID = this.UID;
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
          OverWorldManager.zoopopupHolder.CreateZooPopUps(this, player);
      }
      if (this.simperson.roleinsociety == RoleInSociety.Customer)
        this.simperson.UpdateCustomer(this, DeltaTime, player, ref this.WalkPaused, ref this.BlockAutoWalk, ref this.IsPlayingWalkAnimation);
      else if (this.simperson.roleinsociety == RoleInSociety.BlackMarket)
        this.simperson.UpdateCustomer(this, DeltaTime, player, ref this.WalkPaused, ref this.BlockAutoWalk, ref this.IsPlayingWalkAnimation);
      else if (this.simperson.roleinsociety == RoleInSociety.Employee)
      {
        bool TeleportToGate;
        this.simperson.UpdateEmployee(this.pathnavigator, ref this.BlockAutoWalk, ref this.IsPlayingWalkAnimation, DeltaTime, out TeleportToGate, player, this);
        if (TeleportToGate)
        {
          this.IsPlayingWalkAnimation = false;
          this.pathnavigator.TeleportHere(this.ThisPersonStartLocation);
        }
      }
      else if (this.simperson.roleinsociety == RoleInSociety.Avatar)
        this.simperson.UpdateAvatar(this.pathnavigator, DeltaTime, player, ref this.IsPlayingWalkAnimation, this);
      return false;
    }

    private bool GetDecidedNotToPay() => this.simperson != null && this.simperson.memberofthepublic != null && this.simperson.memberofthepublic.ThisCustomerDecidedNotToPay;

    public void KillThisPerson()
    {
      OverWorldEnvironmentManager.deadPeople.AddDeadPerson(this);
      this.simperson.IsDead = true;
    }

    private void TryToWalk(Player player)
    {
      Vector2Int ForceGoHere = (Vector2Int) null;
      bool DidSetNewPath = false;
      if (this.simperson != null)
        this.simperson.ReachedTarget(this.pathnavigator.CurrentTile, WalkingPerson.BottomRight.Y, out ForceGoHere, player, this.vLocation, this.pathnavigator, ref this.BlockAutoWalk, out DidSetNewPath, this, ref this.IsPlayingWalkAnimation);
      if (this.simperson.roleinsociety == RoleInSociety.Avatar)
      {
        this.DoSImpleNav();
      }
      else
      {
        if (DidSetNewPath)
          this.IsPlayingWalkAnimation = true;
        else if (ForceGoHere != null && this.pathnavigator.TryToGoHere(ForceGoHere, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
          this.IsPlayingWalkAnimation = true;
        int customertype1 = (int) this.simperson.customertype;
        if (this.IsPlayingWalkAnimation || this.BlockAutoWalk || this.IsOnFinalWalkToBus)
          return;
        Vector2Int TargetLocation = (Vector2Int) null;
        if (this.simperson.roleinsociety == RoleInSociety.Customer)
        {
          this.simperson.memberofthepublic.CheckGoSomewhereSpecific(ref TargetLocation, this.pathnavigator.CurrentTile, this.pathnavigator, this.simperson.customertype, this);
          int customertype2 = (int) this.simperson.customertype;
          if (this.simperson.memberofthepublic.LeftParkEarly && !this.IsOnFinalWalkToBus)
          {
            this.IsPlayingWalkAnimation = false;
            this.TryToReturnToBus();
            return;
          }
          if (this.simperson.customertype == CustomerType.Protestor)
          {
            TargetLocation = this.simperson.CheckGroupNavigation(this);
            this.HoldTime = (float) TinyZoo.Game1.Rnd.Next(0, 30);
            this.HoldTime *= 0.2f;
          }
          else if (this.simperson.customertype == CustomerType.Normal && this.simperson.memberofthepublic.NextQuest != CustomerQuest.Count && this.simperson.memberofthepublic.CurrentQuest == CustomerQuest.Count)
          {
            ParkLeavingReason leavepark;
            if (this.simperson.memberofthepublic.TryToStartNextQuest(this.pathnavigator, player, out leavepark, this))
            {
              this.IsPlayingWalkAnimation = true;
              return;
            }
            if (leavepark != ParkLeavingReason.None)
            {
              this.simperson.memberofthepublic.LeftParkEarly = true;
              this.simperson.memberofthepublic.LeftTheParkBecauseOfThis = leavepark;
              this.IsPlayingWalkAnimation = false;
              this.TryToReturnToBus();
              return;
            }
          }
        }
        if (TargetLocation == null)
        {
          if (this.UseSimpleNavigation)
          {
            this.DoSImpleNav();
            return;
          }
          if (this.IsEmployee && this.simperson.Ref_Employee.workzoneinfo != null && this.simperson.Ref_Employee.workzoneinfo.workzones.Count > 0)
          {
            if (this.simperson.Ref_Employee.workzoneinfo.workzones[0].IsInWorkZone(this.pathnavigator.CurrentTile))
            {
              this.DoSImpleNav();
              return;
            }
            TargetLocation = this.simperson.Ref_Employee.workzoneinfo.workzones[0].GetRandomUnblockedLocaton();
            if (TargetLocation == null)
              this.DoSImpleNav();
          }
          else
            TargetLocation = WalkingPerson.GetRandomUnblockedLocaton();
        }
        if (TargetLocation != null && this.pathnavigator.TryToGoHere(TargetLocation, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
          this.IsPlayingWalkAnimation = true;
        if (this.simperson.roleinsociety != RoleInSociety.Employee)
          return;
        this.simperson.CheckPathOnSetNewTarget(this.pathnavigator);
      }
    }

    internal static Vector2Int GetRandomUnblockedLocaton()
    {
      bool flag = false;
      int num = 0;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out WalkingPerson.TopLeft, out WalkingPerson.BottomRight, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      while (!flag)
      {
        Vector2Int vector2Int = new Vector2Int(TinyZoo.Game1.Rnd.Next(WalkingPerson.TopLeft.X, WalkingPerson.BottomRight.X), TinyZoo.Game1.Rnd.Next(WalkingPerson.TopLeft.Y, WalkingPerson.BottomRight.Y));
        if (!Z_GameFlags.pathfinder.GetIsBlocked(vector2Int.X, vector2Int.Y))
          return vector2Int;
        ++num;
        if (num > 30)
          return (Vector2Int) null;
      }
      return (Vector2Int) null;
    }

    public void TryToReturnToBus()
    {
      if (this.IsOnFinalWalkToBus)
        return;
      if (this.simperson.roleinsociety == RoleInSociety.Customer)
      {
        int customertype = (int) this.simperson.customertype;
      }
      if (this.pathnavigator.TryToGoHere(this.ThisPersonStartLocation, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
      {
        this.IsOnFinalWalkToBus = true;
        this.IsPlayingWalkAnimation = true;
        this.pathnavigator.CurrentTile.CompareMatches(this.ThisPersonStartLocation);
      }
      else
      {
        this.IsOnFinalWalkToBus = true;
        this.pathnavigator.TeleportHere(this.ThisPersonStartLocation);
        if (this.simperson.roleinsociety != RoleInSociety.Customer)
          return;
        this.simperson.memberofthepublic.TeleportedBackToBus(this);
      }
    }

    public bool CheckCanWalkHere(Vector2Int Target, out int PathLength) => this.pathnavigator.Check_CanWalkHere(Target, GameFlags.pathset, this.pathnavigator.CurrentTile, out PathLength);

    internal static int SortWalkingPerson(WalkingPerson a, WalkingPerson b)
    {
      if (a.animalrenderer != null)
      {
        int uid1 = a.UID;
        int uid2 = b.UID;
      }
      if (a.HasExtraOffsetIsY || b.HasExtraOffsetIsY)
      {
        if (a.HasExtraOffsetIsY && b.HasExtraOffsetIsY)
        {
          if ((double) a.vLocation.Y + (double) a.ExtraOffsetY_Sort < (double) b.vLocation.Y + (double) b.ExtraOffsetY_Sort)
            return -1;
          if ((double) a.vLocation.Y + (double) a.ExtraOffsetY_Sort > (double) b.vLocation.Y + (double) b.ExtraOffsetY_Sort)
            return 1;
          if (a.UID < b.UID)
            return -1;
          if (a.UID > b.UID)
            return 1;
        }
        else if (a.HasExtraOffsetIsY)
        {
          if ((double) a.vLocation.Y + (double) a.ExtraOffsetY_Sort < (double) b.vLocation.Y)
            return -1;
          if ((double) a.vLocation.Y + (double) a.ExtraOffsetY_Sort > (double) b.vLocation.Y)
            return 1;
          if (a.UID < b.UID)
            return -1;
          if (a.UID > b.UID)
            return 1;
        }
        else if (b.HasExtraOffsetIsY)
        {
          if ((double) a.vLocation.Y < (double) b.vLocation.Y + (double) b.ExtraOffsetY_Sort)
            return -1;
          if ((double) a.vLocation.Y > (double) b.vLocation.Y + (double) b.ExtraOffsetY_Sort)
            return 1;
          if (a.UID < b.UID)
            return -1;
          if (a.UID > b.UID)
            return 1;
        }
      }
      else
      {
        if ((double) a.vLocation.Y < (double) b.vLocation.Y)
          return -1;
        if ((double) a.vLocation.Y > (double) b.vLocation.Y)
          return 1;
        if (a.UID < b.UID)
          return -1;
        if (a.UID > b.UID)
          return 1;
      }
      return 0;
    }

    public void SetFrame()
    {
      this.FlipRender = false;
      switch (this.directionmoving)
      {
        case DirectionPressed.Up:
          this.DrawRect = this.tispersoninfo.WalkUp;
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          break;
        case DirectionPressed.Right:
          this.DrawRect = this.tispersoninfo.WalkRight;
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          break;
        case DirectionPressed.Down:
          this.DrawRect = this.tispersoninfo.WalkDown;
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          break;
        case DirectionPressed.Left:
          this.DrawRect = this.tispersoninfo.WalkRight;
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          this.FlipRender = true;
          break;
      }
      this.SetUpSimpleAnimation(4, 0.2f);
    }

    private void DoSImpleNav()
    {
      int maxValue = 0;
      WalkingPerson.UpRightDownLeft[0] = false;
      WalkingPerson.UpRightDownLeft[1] = false;
      WalkingPerson.UpRightDownLeft[2] = false;
      WalkingPerson.UpRightDownLeft[3] = false;
      if (this.pathnavigator.CurrentTile.Y > 0)
      {
        if (Z_GameFlags.pathfinder.GetCanCrossFast(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y, DirectionPressed.Up))
        {
          WalkingPerson.UpRightDownLeft[0] = true;
          ++maxValue;
        }
      }
      else if (!WalkingPerson.UpRightDownLeft[0])
      {
        ++maxValue;
        WalkingPerson.UpRightDownLeft[0] = true;
      }
      if (this.pathnavigator.CurrentTile.X > 0 && Z_GameFlags.pathfinder.GetCanCrossFast(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y, DirectionPressed.Left))
      {
        WalkingPerson.UpRightDownLeft[3] = true;
        ++maxValue;
      }
      if (this.pathnavigator.CurrentTile.X < TileMath.GetOverWorldMapSize_XDefault() - 1 && Z_GameFlags.pathfinder.GetCanCrossFast(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y, DirectionPressed.Right))
      {
        WalkingPerson.UpRightDownLeft[1] = true;
        ++maxValue;
      }
      if (this.pathnavigator.CurrentTile.Y < TileMath.GetBottomParkPlayableSpace() && Z_GameFlags.pathfinder.GetCanCrossFast(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y, DirectionPressed.Down))
      {
        WalkingPerson.UpRightDownLeft[2] = true;
        ++maxValue;
      }
      if (maxValue <= 0)
        return;
      if (maxValue == 1)
        this.CameFromHere = DirectionPressed.None;
      else if (this.CameFromHere != DirectionPressed.None)
      {
        WalkingPerson.UpRightDownLeft[(int) this.CameFromHere] = false;
        --maxValue;
      }
      int num = TinyZoo.Game1.Rnd.Next(0, maxValue);
      List<PathNode> locations = new List<PathNode>();
      for (int index = 0; index < WalkingPerson.UpRightDownLeft.Length; ++index)
      {
        if (WalkingPerson.UpRightDownLeft[index])
        {
          if (num == 0)
          {
            switch (index)
            {
              case 0:
                locations.Add(new PathNode(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y - 1));
                this.pathnavigator.SetNewPath(locations);
                this.IsPlayingWalkAnimation = true;
                this.CameFromHere = DirectionPressed.Down;
                return;
              case 1:
                locations.Add(new PathNode(this.pathnavigator.CurrentTile.X + 1, this.pathnavigator.CurrentTile.Y));
                this.pathnavigator.SetNewPath(locations);
                this.IsPlayingWalkAnimation = true;
                this.CameFromHere = DirectionPressed.Left;
                return;
              case 2:
                locations.Add(new PathNode(this.pathnavigator.CurrentTile.X, this.pathnavigator.CurrentTile.Y + 1));
                this.pathnavigator.SetNewPath(locations);
                this.IsPlayingWalkAnimation = true;
                this.CameFromHere = DirectionPressed.Up;
                break;
              case 3:
                locations.Add(new PathNode(this.pathnavigator.CurrentTile.X - 1, this.pathnavigator.CurrentTile.Y));
                this.pathnavigator.SetNewPath(locations);
                this.IsPlayingWalkAnimation = true;
                this.CameFromHere = DirectionPressed.Right;
                return;
            }
          }
          --num;
        }
      }
    }

    public void DrawWalkingPerson()
    {
      if ((double) this.TrailerRenderDelay > 0.0)
        this.TrailerRenderDelay -= GameFlags.RefDeltaTime;
      else if (this.animalrenderer != null)
      {
        if (AmbienceHandler.CollectSoundInfo)
        {
          ++AmbienceHandler.TotalAnimals;
          ++AmbienceHandler.Animals[(int) this.animalrenderer.enemyrenderere.enemytype];
        }
        this.animalrenderer.DrawAnimal();
      }
      else if (this.dynamicenrichmentitem != null)
      {
        this.dynamicenrichmentitem.DrawEnrichmentItem();
      }
      else
      {
        if (!this.IsAtive || this.simperson.IsDead)
          return;
        if (this.IsEmployee)
          this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
        if (this.personattachmentmanager != null)
          this.personattachmentmanager.DrawUpdatePersonAttachmentManager(this);
        else if (this.HasExtraOffset)
        {
          Vector2 Location = this.vLocation + this.ExtraOffset;
          this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, ref Location, ref WalkingPerson.VSCALE, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, false, ref WalkingPerson.ThreadLoc, ref WalkingPerson.ThreadScale);
        }
        else
          this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet, ref this.vLocation, ref WalkingPerson.VSCALE, this.Rotation, this.DrawRect, this.fAlpha, this.GetColour(), this.scale, false, ref WalkingPerson.ThreadLoc, ref WalkingPerson.ThreadScale);
        if (WalkingPerson.MouseOverUID != -1 && WalkingPerson.MouseOverUID == this.UID)
        {
          if (this.statusicon != null)
            this.statusicon.DrawStatusIcon(this.vLocation, true);
          WalkingPerson.MouseOverUID = -1;
          if (this.HasExtraOffset)
          {
            Vector2 Location = this.vLocation + this.ExtraOffset;
            this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet_white, ref Location, ref WalkingPerson.VSCALE, this.Rotation, this.DrawRect, this.fAlpha * 0.4f, this.GetColour(), this.scale, false, ref WalkingPerson.ThreadLoc, ref WalkingPerson.ThreadScale);
          }
          else
            this.QuickWorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet_white, ref this.vLocation, ref WalkingPerson.VSCALE, this.Rotation, this.DrawRect, this.fAlpha * 0.4f, this.GetColour(), this.scale, false, ref WalkingPerson.ThreadLoc, ref WalkingPerson.ThreadScale);
        }
        else
        {
          if (this.statusicon == null || this.statusicon.statusicontype == StatusIconType.None)
            return;
          this.statusicon.DrawStatusIcon(this.vLocation, false);
        }
      }
    }

    public void ScreenSpaceDrawWalkingPerson(
      Vector2 Offset,
      SpriteBatch spritebatch,
      DirectionPressed facethisway,
      int Frame)
    {
      if (this.personattachmentmanager != null)
        throw new Exception("OH YOU NEED TO DRAW ATTACHMENTS");
      this.FlipRender = false;
      float num = 1f;
      this.DrawRect = this.tispersoninfo.WalkDown;
      switch (facethisway)
      {
        case DirectionPressed.Right:
          this.DrawRect = this.tispersoninfo.WalkRight;
          break;
        case DirectionPressed.Down:
          this.DrawRect = this.tispersoninfo.WalkDown;
          break;
        case DirectionPressed.Left:
          this.DrawRect = this.tispersoninfo.WalkRight;
          num = -1f;
          break;
      }
      this.DrawRect.X += (this.DrawRect.Width + 1) * Frame;
      this.Draw(spritebatch, AssetContainer.AnimalSheet, Offset, new Vector2(this.scale * num, this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y), this.fAlpha);
    }

    public void ScreenSpaceDraw(Vector2 Location, SpriteBatch spritebatch, float _Scale = 5f)
    {
      Vector2 drawOrigin = this.DrawOrigin;
      this.SetDrawOriginToCentre();
      if (this.IsEmployee)
        this.SetAllColours(1f, 1f, 1f);
      Vector2 vLocation = this.vLocation;
      this.vLocation = Location;
      float scale = this.scale;
      this.scale = _Scale;
      Rectangle drawRect = this.DrawRect;
      this.DrawRect = this.tispersoninfo.WalkDown;
      this.Draw(spritebatch, AssetContainer.AnimalSheet);
      this.scale = scale;
      this.vLocation = vLocation;
      this.DrawRect = drawRect;
      this.DrawOrigin = drawOrigin;
    }
  }
}
