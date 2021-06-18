// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.ShopkeeperController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class ShopkeeperController
  {
    private bool GoToWorkNow;
    private QuickEmployeeDescription quickemplyeedescription;
    private Employee Ref_Employee;
    private Vector2Int ShopEntry;
    private Vector2 MicroOffset;
    private ShopKeeperState shopkeeperstate;
    private Vector2Int WorkingLocation;
    private Vector2 TargetOffsetForJob;
    private DirectionPressed lookthiswaywhenworking;

    public ShopkeeperController(
      QuickEmployeeDescription _quickemplyeedescription,
      Employee _Ref_Employee)
    {
      this.shopkeeperstate = ShopKeeperState.None;
      this.Ref_Employee = _Ref_Employee;
      this.quickemplyeedescription = _quickemplyeedescription;
      if (CustomerManager.CustomersInPark_NotWaitingForBus <= 0)
        return;
      this.StartNewDay();
    }

    public void TeleportedToGate(ref bool BlockAutoWalk, ref bool IsPlayingWalkAnimation)
    {
    }

    public bool ShouldNotTeleportFromBlockedTile() => this.shopkeeperstate == ShopKeeperState.AtWork;

    public void TeleportedToGate()
    {
    }

    public void StartNewDay() => this.GoToWorkNow = true;

    public void UpdateShopkeeperController(
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      ref bool IsWalking,
      float SimulationTime,
      out bool TeleportToGate,
      Player player,
      Employee Ref_Employee,
      WalkingPerson parent)
    {
      if (Ref_Employee.quickemplyeedescription.JustMovedShop)
      {
        if (this.shopkeeperstate == ShopKeeperState.AtWork || this.shopkeeperstate == ShopKeeperState.OnTheWayToWork)
        {
          this.GoToWorkNow = true;
          this.shopkeeperstate = ShopKeeperState.None;
        }
        Ref_Employee.quickemplyeedescription.JustMovedShop = false;
      }
      if (this.GoToWorkNow)
      {
        this.GoToWorkNow = false;
        TinyZoo.PlayerDir.Shops.ShopEntry thisShop = player.shopstatus.GetThisShop(this.quickemplyeedescription.ShopUID);
        TileInfo tileInfo = TileData.GetTileInfo(thisShop.tiletype);
        int rotationClockWise = player.prisonlayout.layout.BaseTileTypes[thisShop.LocationOfThisShop.X, thisShop.LocationOfThisShop.Y].RotationClockWise;
        this.ShopEntry = tileInfo.GetTradesmansEntrance(rotationClockWise) == null ? new Vector2Int(tileInfo.GetEntrances(rotationClockWise)[0]) : new Vector2Int(tileInfo.GetTradesmansEntrance(rotationClockWise));
        this.ShopEntry.X += thisShop.LocationOfThisShop.X;
        this.ShopEntry.Y += thisShop.LocationOfThisShop.Y;
        this.shopkeeperstate = ShopKeeperState.OnTheWayToWork;
        this.WorkingLocation = new Vector2Int(thisShop.LocationOfThisShop);
        this.WorkingLocation += tileInfo.GetShopKeeperLocation(rotationClockWise, ref this.TargetOffsetForJob);
        switch (rotationClockWise)
        {
          case 0:
            this.lookthiswaywhenworking = DirectionPressed.Down;
            break;
          case 1:
            this.lookthiswaywhenworking = DirectionPressed.Left;
            break;
          case 2:
            this.lookthiswaywhenworking = DirectionPressed.Up;
            break;
          default:
            this.lookthiswaywhenworking = DirectionPressed.Right;
            break;
        }
        if (pathnavigator.TryToGoHere(this.ShopEntry, GameFlags.pathset, true))
        {
          this.shopkeeperstate = ShopKeeperState.OnTheWayToWork;
        }
        else
        {
          this.ShopEntry = new Vector2Int(tileInfo.GetEntrances(rotationClockWise)[0]);
          this.ShopEntry += thisShop.LocationOfThisShop;
          if (pathnavigator.TryToGoHere(this.ShopEntry, GameFlags.pathset, true))
          {
            this.shopkeeperstate = ShopKeeperState.OnTheWayToWork;
          }
          else
          {
            this.shopkeeperstate = ShopKeeperState.AtWork;
            IsWalking = false;
            pathnavigator.TeleportHere(this.WorkingLocation);
            parent.ForceRotationAndHold(this.lookthiswaywhenworking, 0.0f);
            BlockAutoWalk = true;
          }
        }
      }
      if (this.shopkeeperstate == ShopKeeperState.AtWork && Z_GameFlags.IsDay)
        this.quickemplyeedescription.TimeAtWork_IncludingBreaks += SimulationTime;
      if ((double) this.quickemplyeedescription.LiveServingTimeLeft > 0.0)
      {
        this.quickemplyeedescription.LiveServingTimeLeft -= SimulationTime;
        this.quickemplyeedescription.TimeSpentServing += SimulationTime;
        if ((double) this.quickemplyeedescription.LiveServingTimeLeft < 0.0)
        {
          this.quickemplyeedescription.TimeSpentServing += this.quickemplyeedescription.LiveServingTimeLeft;
          this.quickemplyeedescription.LiveServingTimeLeft = 0.0f;
        }
      }
      else if ((double) this.quickemplyeedescription.BreakTime > 0.0)
      {
        this.quickemplyeedescription.BreakTime -= SimulationTime;
        if ((double) Ref_Employee.quickemplyeedescription.StoreEmployeeStatValues[3] < 1.0)
        {
          Ref_Employee.quickemplyeedescription.StoreEmployeeStatValues[3] += (float) ((double) SimulationTime * (double) Ref_Employee.quickemplyeedescription.Determination * 0.400000005960464);
          if ((double) Ref_Employee.quickemplyeedescription.StoreEmployeeStatValues[3] > 1.0)
            Ref_Employee.quickemplyeedescription.StoreEmployeeStatValues[3] = 1f;
        }
      }
      if (this.shopkeeperstate == ShopKeeperState.AtWork && CustomerManager.CustomersInPark_NotWaitingForBus == 0 && OverWorldManager.zbus.NoBussesAreDroppingOff())
      {
        if (this.ShopEntry == null)
          throw new Exception(" guess you need to handle this");
        if (Z_GameFlags.pathfinder.GetIsBlocked(this.ShopEntry.X, this.ShopEntry.Y))
        {
          pathnavigator.TeleportHere(TileMath.GetGateLocationV2Int());
          this.shopkeeperstate = ShopKeeperState.None;
        }
        else
        {
          List<PathNode> path = this.GetPath(true);
          if (path.Count <= 0)
            throw new Exception(" guess you need to handle this");
          pathnavigator.SetNewPath(path);
          this.shopkeeperstate = ShopKeeperState.WalkingOutOfShop;
          BlockAutoWalk = true;
        }
      }
      TeleportToGate = false;
    }

    public void ReachedTargetLocation(
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      WalkingPerson parent,
      out bool DidSetNewPath,
      ref bool IsWalking)
    {
      DidSetNewPath = false;
      if (this.shopkeeperstate == ShopKeeperState.OnTheWayToWork)
      {
        if (!pathnavigator.CurrentTile.CompareMatches(this.ShopEntry))
          return;
        this.shopkeeperstate = ShopKeeperState.AtWork_WalkingToWorkPoint;
        List<PathNode> path = this.GetPath(false);
        pathnavigator.SetNewPath(path);
        pathnavigator.SetTargetOffset(this.TargetOffsetForJob);
        DidSetNewPath = true;
      }
      else if (this.shopkeeperstate == ShopKeeperState.AtWork_WalkingToWorkPoint)
      {
        if (!pathnavigator.CurrentTile.CompareMatches(this.WorkingLocation))
          return;
        this.shopkeeperstate = ShopKeeperState.AtWork;
        parent.ForceRotationAndHold(this.lookthiswaywhenworking, 0.0f);
        BlockAutoWalk = true;
      }
      else
      {
        if (this.shopkeeperstate == ShopKeeperState.AtWork || this.shopkeeperstate != ShopKeeperState.WalkingOutOfShop || !pathnavigator.CurrentTile.CompareMatches(this.ShopEntry))
          return;
        this.shopkeeperstate = ShopKeeperState.None;
        BlockAutoWalk = false;
      }
    }

    public List<PathNode> GetPath(bool getreverse)
    {
      int num = 1;
      if (getreverse)
        num = 0;
      List<PathNode> pathNodeList = new List<PathNode>();
      if (this.WorkingLocation.X > this.ShopEntry.X)
      {
        for (int _XLoc = this.ShopEntry.X + num; _XLoc < this.WorkingLocation.X + num; ++_XLoc)
        {
          if (getreverse)
            pathNodeList.Insert(0, new PathNode(_XLoc, this.WorkingLocation.Y));
          else
            pathNodeList.Add(new PathNode(_XLoc, this.WorkingLocation.Y));
        }
      }
      else if (this.WorkingLocation.X < this.ShopEntry.X)
      {
        for (int _XLoc = this.ShopEntry.X - num; _XLoc > this.WorkingLocation.X - num; --_XLoc)
        {
          if (getreverse)
            pathNodeList.Insert(0, new PathNode(_XLoc, this.WorkingLocation.Y));
          else
            pathNodeList.Add(new PathNode(_XLoc, this.WorkingLocation.Y));
        }
      }
      else if (this.WorkingLocation.Y > this.ShopEntry.Y)
      {
        for (int _YLoc = this.ShopEntry.Y + num; _YLoc < this.WorkingLocation.Y + num; ++_YLoc)
        {
          if (getreverse)
            pathNodeList.Insert(0, new PathNode(this.WorkingLocation.X, _YLoc));
          else
            pathNodeList.Add(new PathNode(this.WorkingLocation.X, _YLoc));
        }
      }
      else if (this.WorkingLocation.Y < this.ShopEntry.Y)
      {
        for (int _YLoc = this.ShopEntry.Y - num; _YLoc > this.WorkingLocation.Y - num; --_YLoc)
        {
          if (getreverse)
            pathNodeList.Insert(0, new PathNode(this.WorkingLocation.X, _YLoc));
          else
            pathNodeList.Add(new PathNode(this.WorkingLocation.X, _YLoc));
        }
      }
      return pathNodeList;
    }
  }
}
