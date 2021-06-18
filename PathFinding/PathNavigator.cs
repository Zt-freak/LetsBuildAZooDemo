// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.PathNavigator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.PathFinding.HierarchicalPathFinding;

namespace TinyZoo.PathFinding
{
  internal class PathNavigator
  {
    private TileSmoothed tileSmoothedNavi;
    private TileDiagonal tileDiagonalNavi;
    private TileCenter tileCenterNavi;
    public Vector2Int CurrentTile;
    public Vector2Int ReliableCurrentTile;
    public float MovementSpeed;
    private List<PathNode> CurrentPath;
    public bool IsNavigating;
    public Vector2 TileRelativeLocation;
    private NavigationTypeEnum navtype;
    public DirectionPressed directionmovedthisframe = DirectionPressed.None;
    public bool facingLeft = true;
    private bool LastCall_AllowDiagonals;
    private Vector2Int prevTileCtr;
    private float timePassed;
    private float lerpfloat;
    private float relativeDistanceTarget;
    private float relativeDistance;
    private bool firstnode;
    private bool endAtCenter;
    public bool facingBackLastFrame;
    private PathNavigator.TileCenterNavState tcNavState;
    public bool MovedTile;
    private static Vector2Int Usefulvector2 = new Vector2Int();
    private static Vector2Int Usefulvector2_TWO = new Vector2Int();
    private Vector2 AdditionalEndOffset;
    private bool HasEndingOffset;
    private Vector2 AdditionalStartOffset;
    private bool HasStartingOffset;

    public PathNavigator(
      Vector2Int _CurrentTile,
      float _MovementSpeed,
      bool AllowTileOffset = true,
      NavigationTypeEnum _navtype = NavigationTypeEnum.TileSmoothed)
    {
      this.ReliableCurrentTile = new Vector2Int(_CurrentTile);
      this.navtype = _navtype;
      this.MovementSpeed = _MovementSpeed;
      this.CurrentTile = new Vector2Int(_CurrentTile);
      this.prevTileCtr = _CurrentTile;
      this.lerpfloat = 0.5f;
      this.relativeDistance = 1f;
      this.relativeDistanceTarget = 1f;
      this.timePassed = (float) (0.5 * ((double) this.relativeDistance / (double) this.MovementSpeed));
      this.firstnode = true;
      this.facingBackLastFrame = false;
      this.tileSmoothedNavi = new TileSmoothed(2f);
      if (AllowTileOffset)
        this.tileSmoothedNavi.GiveRandomOffset();
      this.tileDiagonalNavi = new TileDiagonal(1f);
      this.tileCenterNavi = new TileCenter();
    }

    public float GetPercentageThroughTile() => (double) this.TileRelativeLocation.X == 0.0 ? MathHelper.Clamp((float) (((double) this.TileRelativeLocation.Y + 1.0) * 0.5), 0.0f, 1f) : MathHelper.Clamp((float) (((double) this.TileRelativeLocation.X + 1.0) * 0.5), 0.0f, 1f);

    public void SetMovementSpeed(float _MovementSpeed) => this.MovementSpeed = _MovementSpeed;

    public List<PathNode> GetCurrentPath() => this.CurrentPath;

    public PathNode GetLastNodeOnCurrentPath() => this.CurrentPath != null && this.CurrentPath.Count > 0 ? this.CurrentPath[this.CurrentPath.Count - 1] : (PathNode) null;

    public PathNode GetPathTileByIndex(int TileIndex = 0) => this.CurrentPath != null && this.CurrentPath.Count > TileIndex ? this.CurrentPath[TileIndex] : (PathNode) null;

    public PathNode GetPenultimateNodeOnCurrentPath() => this.CurrentPath != null && this.CurrentPath.Count > 1 ? this.CurrentPath[this.CurrentPath.Count - 2] : (PathNode) null;

    public bool CurrentPathCrossesThis(int Xloc, int Yloc)
    {
      for (int index = 0; index < this.CurrentPath.Count; ++index)
      {
        if (this.CurrentPath[index].XLoc == Xloc && this.CurrentPath[index].YLoc == Yloc)
          return true;
      }
      return false;
    }

    public void TrimPath(int TilesToCutFromEnd)
    {
      for (int index = this.CurrentPath.Count - 1; index > -1; --index)
      {
        if (TilesToCutFromEnd > 0)
        {
          --TilesToCutFromEnd;
          this.CurrentPath.RemoveAt(index);
        }
      }
    }

    public void TeleportHere(Vector2Int Loc)
    {
      this.CurrentTile.Copy(Loc);
      this.ReliableCurrentTile.Copy(this.CurrentTile);
      this.ForceResetPath();
    }

    public void ForceResetPath()
    {
      this.TileRelativeLocation = Vector2.Zero;
      this.IsNavigating = false;
      this.CurrentPath = new List<PathNode>();
      if (this.navtype != NavigationTypeEnum.TileSmoothed)
        return;
      this.tileSmoothedNavi.ReInitialize();
    }

    public int GetPathLength() => this.CurrentPath == null ? 0 : this.CurrentPath.Count;

    public Vector2Int GetEndOfCurrentPath() => this.CurrentPath.Count > 0 ? new Vector2Int(this.CurrentPath[this.CurrentPath.Count - 1].XLoc, this.CurrentPath[this.CurrentPath.Count - 1].YLoc) : this.CurrentTile;

    public bool UpdatePathNavigator(float DeltaTime)
    {
      this.MovedTile = false;
      this.facingBackLastFrame = this.directionmovedthisframe == DirectionPressed.Up || this.directionmovedthisframe == DirectionPressed.Right || this.directionmovedthisframe == DirectionPressed.NorthEast;
      this.directionmovedthisframe = DirectionPressed.None;
      if (this.IsNavigating)
      {
        if (this.navtype == NavigationTypeEnum.TileCenter)
        {
          if (this.tcNavState == PathNavigator.TileCenterNavState.StartOffset)
          {
            this.IsNavigating = true;
            Vector2 vector2 = -this.AdditionalStartOffset * DeltaTime * this.MovementSpeed;
            if ((double) vector2.X > 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Right;
              this.facingLeft = false;
            }
            else if ((double) vector2.X < 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Left;
              this.facingLeft = true;
            }
            if ((double) vector2.Y > 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Down;
              this.facingLeft = false;
            }
            else if ((double) vector2.Y < 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Up;
              this.facingLeft = true;
            }
            if ((double) vector2.LengthSquared() > (double) this.TileRelativeLocation.LengthSquared())
            {
              this.HasStartingOffset = false;
              this.AdditionalStartOffset = Vector2.Zero;
              this.TileRelativeLocation = Vector2.Zero;
              this.tcNavState = PathNavigator.TileCenterNavState.Normal;
            }
            else
              this.TileRelativeLocation += vector2;
            return false;
          }
          if (this.tcNavState == PathNavigator.TileCenterNavState.Normal)
          {
            this.IsNavigating = !this.tileCenterNavi.UpdateNavigationTileCenter(DeltaTime, this.CurrentPath, ref this.CurrentTile, this.MovementSpeed, ref this.TileRelativeLocation, ref this.directionmovedthisframe, ref this.facingLeft, this.endAtCenter, this.IsNavigating, ref this.MovedTile);
            if (this.IsNavigating || !this.HasEndingOffset)
              return !this.IsNavigating;
            if ((double) this.AdditionalEndOffset.X > 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Right;
              this.facingLeft = false;
            }
            else if ((double) this.AdditionalEndOffset.X < 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Left;
              this.facingLeft = true;
            }
            if ((double) this.AdditionalEndOffset.Y > 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Down;
              this.facingLeft = false;
            }
            else if ((double) this.AdditionalEndOffset.Y < 0.0)
            {
              this.directionmovedthisframe = DirectionPressed.Up;
              this.facingLeft = true;
            }
            this.tcNavState = PathNavigator.TileCenterNavState.EndOffset;
            this.IsNavigating = true;
            return false;
          }
          if (this.tcNavState == PathNavigator.TileCenterNavState.EndOffset)
          {
            Vector2 vector2 = this.AdditionalEndOffset * DeltaTime * this.MovementSpeed;
            if ((double) (this.TileRelativeLocation + vector2).LengthSquared() > (double) this.AdditionalEndOffset.LengthSquared())
            {
              this.HasStartingOffset = true;
              this.AdditionalStartOffset = this.AdditionalEndOffset;
              this.tcNavState = PathNavigator.TileCenterNavState.StartOffset;
              this.TileRelativeLocation = this.AdditionalEndOffset;
              this.HasEndingOffset = false;
              this.AdditionalEndOffset = Vector2.Zero;
              this.IsNavigating = false;
              return true;
            }
            this.TileRelativeLocation += vector2;
            return false;
          }
        }
        else
        {
          if (this.navtype == NavigationTypeEnum.DigonalAcrossTiles)
          {
            this.IsNavigating = !this.tileDiagonalNavi.UpdateNavigationTileDiagonal(DeltaTime, this.CurrentPath, ref this.CurrentTile, ref this.TileRelativeLocation, ref this.directionmovedthisframe, ref this.facingLeft, this.endAtCenter);
            return !this.IsNavigating;
          }
          if (this.navtype == NavigationTypeEnum.TileSmoothed)
          {
            this.IsNavigating = !this.tileSmoothedNavi.UpdateNavigationTileSmoothed(DeltaTime, this.CurrentPath, ref this.CurrentTile, ref this.TileRelativeLocation, ref this.directionmovedthisframe, ref this.facingLeft, this.endAtCenter);
            return !this.IsNavigating;
          }
          if (this.CurrentPath.Count != 0)
          {
            if (this.CurrentPath[0].XLoc > this.CurrentTile.X)
            {
              this.TileRelativeLocation.X += DeltaTime * this.MovementSpeed;
              if ((double) this.TileRelativeLocation.X > 1.0)
              {
                this.TileRelativeLocation.X -= 2f;
                ++this.CurrentTile.X;
                this.MovedTile = true;
                this.CurrentPath.RemoveAt(0);
              }
            }
            else if (this.CurrentPath[0].XLoc < this.CurrentTile.X)
            {
              this.TileRelativeLocation.X -= DeltaTime * this.MovementSpeed;
              if ((double) this.TileRelativeLocation.X < -1.0)
              {
                this.MovedTile = true;
                this.TileRelativeLocation.X += 2f;
                this.ReliableCurrentTile.Copy(this.CurrentPath[0].Location);
                this.CurrentPath.RemoveAt(0);
                --this.CurrentTile.X;
              }
            }
            else if (this.CurrentPath[0].YLoc > this.CurrentTile.Y)
            {
              this.TileRelativeLocation.Y += DeltaTime * this.MovementSpeed;
              if ((double) this.TileRelativeLocation.Y > 1.0)
              {
                this.MovedTile = true;
                this.TileRelativeLocation.Y -= 2f;
                this.CurrentPath.RemoveAt(0);
                this.ReliableCurrentTile.Copy(this.CurrentPath[0].Location);
                ++this.CurrentTile.Y;
              }
            }
            else if (this.CurrentPath[0].YLoc < this.CurrentTile.Y)
            {
              this.TileRelativeLocation.Y -= DeltaTime * this.MovementSpeed;
              if ((double) this.TileRelativeLocation.Y < -1.0)
              {
                this.MovedTile = true;
                this.TileRelativeLocation.Y += 2f;
                this.CurrentPath.RemoveAt(0);
                this.ReliableCurrentTile.Copy(this.CurrentPath[0].Location);
                --this.CurrentTile.Y;
              }
            }
            else
              this.CurrentPath.RemoveAt(0);
          }
          if (this.CurrentPath.Count == 0)
          {
            this.IsNavigating = false;
            return true;
          }
        }
      }
      return false;
    }

    public bool TryToGoToClosest(
      List<Vector2Int> ValidLocationsAroundThis,
      PathSet pathset,
      bool endAtCenter = false,
      bool AllowDiagonals = false,
      Vector2Int OverrideStartPoint = null,
      HierarchicalPathFind hierarchicalPathFind = null)
    {
      throw new Exception("Change the called below to use the heirarch stuff, Game1.PathFinder.blah blah");
    }

    public bool CheckPathStillVallid_TrimToLastTile(
      PathSet pathset,
      HierarchicalPathFind hierarchicalPathFind)
    {
      int num = -1;
      if (this.CurrentPath != null && this.CurrentPath.Count > 0)
      {
        PathNavigator.Usefulvector2.X = this.CurrentPath[this.CurrentPath.Count - 1].XLoc;
        PathNavigator.Usefulvector2.Y = this.CurrentPath[this.CurrentPath.Count - 1].YLoc;
        List<PathNode> currentPath = this.CurrentPath;
        PathNavigator.Usefulvector2_TWO.X = this.CurrentPath[0].XLoc;
        PathNavigator.Usefulvector2_TWO.X = this.CurrentPath[0].YLoc;
        if (!this.TryToGoHere(PathNavigator.Usefulvector2, pathset, this.endAtCenter, this.LastCall_AllowDiagonals, PathNavigator.Usefulvector2_TWO, hierarchicalPathFind))
        {
          for (int index = 0; index < currentPath.Count; ++index)
          {
            if (num < 0 && pathset.GetNode(currentPath[index].XLoc, currentPath[index].YLoc).IsBlocking)
              num = index;
          }
          if (num > -1 && num != 0)
          {
            PathNavigator.Usefulvector2.X = currentPath[num - 1].XLoc;
            PathNavigator.Usefulvector2.Y = currentPath[num - 1].YLoc;
            if (!this.TryToGoHere(PathNavigator.Usefulvector2, pathset, this.endAtCenter, this.LastCall_AllowDiagonals, this.CurrentTile, hierarchicalPathFind))
              throw new Exception("THIS IS IMPOSSSSSIBLE!");
          }
        }
      }
      return num > -1;
    }

    public void AddExtraNodeToPath(Vector2Int NewTarget, PathSet pathset)
    {
      if (this.CurrentPath == null)
        return;
      if (!this.IsNavigating)
        this.IsNavigating = true;
      this.CurrentPath.Add(pathset.nodesasGrid[NewTarget.X, NewTarget.Y]);
    }

    public bool Check_CanWalkHere(
      Vector2Int NewTarget,
      PathSet pathset,
      Vector2Int _StartLocation,
      out int PathLength,
      bool UseHeirachy = true)
    {
      PathLength = 0;
      if (_StartLocation == null && this.CurrentTile.CompareMatches(NewTarget) || _StartLocation != null && _StartLocation.CompareMatches(NewTarget))
        return true;
      if (_StartLocation != null)
      {
        if (UseHeirachy)
        {
          List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(_StartLocation, NewTarget, this.LastCall_AllowDiagonals, true, pathset);
          if (fullPathToLocation != null)
          {
            PathLength = fullPathToLocation.Count;
            return true;
          }
        }
        else
        {
          List<PathNode> fullPathToLocation = pathset.GetFullPathToLocation(_StartLocation, NewTarget, this.LastCall_AllowDiagonals);
          if (fullPathToLocation != null)
          {
            PathLength = fullPathToLocation.Count;
            return true;
          }
        }
        return false;
      }
      if (UseHeirachy)
      {
        List<PathNode> fullPathToLocation = Z_GameFlags.pathfinder.GetFullPathToLocation(this.CurrentTile, NewTarget, this.LastCall_AllowDiagonals, true, pathset);
        if (fullPathToLocation != null)
        {
          PathLength = fullPathToLocation.Count;
          return true;
        }
      }
      else
      {
        List<PathNode> fullPathToLocation = pathset.GetFullPathToLocation(this.CurrentTile, NewTarget, this.LastCall_AllowDiagonals);
        if (fullPathToLocation != null)
        {
          PathLength = fullPathToLocation.Count;
          return true;
        }
      }
      return false;
    }

    public bool TryToGoHereSquare(
      Vector2Int NewTarget,
      PathSet pathset,
      bool _endAtCentre = false,
      bool _AllowDiagonals = false,
      Vector2Int OverrideStartPoint = null,
      HierarchicalPathFind hierarchy = null)
    {
      bool flag1 = false;
      if (this.Check_CanWalkHere(NewTarget, pathset, OverrideStartPoint, out int _))
      {
        bool flag2 = false;
        if (this.CurrentTile.X != NewTarget.X && this.CurrentTile.Y != NewTarget.Y)
        {
          List<PathNode> fullPathToLocation1 = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(this.CurrentTile.X, this.CurrentTile.Y), new Vector2Int(this.CurrentTile.X, NewTarget.Y), this.LastCall_AllowDiagonals, true, pathset);
          bool flag3;
          if (fullPathToLocation1 != null && fullPathToLocation1.Count == Math.Abs(this.CurrentTile.Y - NewTarget.Y))
          {
            List<PathNode> fullPathToLocation2 = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(this.CurrentTile.X, NewTarget.Y), new Vector2Int(NewTarget.X, NewTarget.Y), this.LastCall_AllowDiagonals, true, pathset);
            if (fullPathToLocation2 != null && fullPathToLocation2.Count == Math.Abs(this.CurrentTile.X - NewTarget.X))
            {
              flag3 = true;
              this.CurrentPath = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(this.CurrentTile.X, this.CurrentTile.Y), new Vector2Int(this.CurrentTile.X, NewTarget.Y), this.LastCall_AllowDiagonals, true, pathset);
              for (int index = 0; index < fullPathToLocation2.Count; ++index)
                this.CurrentPath.Add(fullPathToLocation2[index]);
              this.IsNavigating = true;
              return true;
            }
          }
          if (!flag2)
          {
            List<PathNode> fullPathToLocation2 = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(this.CurrentTile.X, this.CurrentTile.Y), new Vector2Int(NewTarget.X, this.CurrentTile.Y), this.LastCall_AllowDiagonals, true, pathset);
            if (fullPathToLocation2 != null && fullPathToLocation2.Count == Math.Abs(this.CurrentTile.X - NewTarget.X))
            {
              List<PathNode> fullPathToLocation3 = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(NewTarget.X, this.CurrentTile.Y), new Vector2Int(NewTarget.X, NewTarget.Y), this.LastCall_AllowDiagonals, true, pathset);
              if (fullPathToLocation3 != null && fullPathToLocation3.Count == Math.Abs(this.CurrentTile.Y - NewTarget.Y))
              {
                flag3 = true;
                this.CurrentPath = Z_GameFlags.pathfinder.GetFullPathToLocation(new Vector2Int(this.CurrentTile.X, this.CurrentTile.Y), new Vector2Int(NewTarget.X, this.CurrentTile.Y), this.LastCall_AllowDiagonals, true, pathset);
                for (int index = 0; index < fullPathToLocation3.Count; ++index)
                  this.CurrentPath.Add(fullPathToLocation3[index]);
                this.IsNavigating = true;
                return true;
              }
            }
          }
        }
        if (!flag2)
          return this.TryToGoHere(NewTarget, pathset, _endAtCentre, _AllowDiagonals, OverrideStartPoint, hierarchy);
      }
      return flag1;
    }

    public void SetTargetOffset(Vector2 _TargetOffset)
    {
      this.AdditionalEndOffset = _TargetOffset / TileMath.HalfTileSize;
      this.HasEndingOffset = this.AdditionalEndOffset != Vector2.Zero;
    }

    public bool TryToGoHere(
      Vector2Int NewTarget,
      PathSet pathset,
      bool _endAtCentre = false,
      bool _AllowDiagonals = false,
      Vector2Int OverrideStartPoint = null,
      HierarchicalPathFind hierarchy = null)
    {
      bool flag1 = OverrideStartPoint != null;
      bool flag2 = false;
      this.LastCall_AllowDiagonals = _AllowDiagonals;
      if (this.IsNavigating && !flag1 && (this.CurrentPath != null && this.CurrentPath.Count > 0))
      {
        flag1 = true;
        OverrideStartPoint = new Vector2Int(this.CurrentPath[0].Location);
      }
      if (OverrideStartPoint == null)
        OverrideStartPoint = new Vector2Int(this.CurrentTile);
      if (NewTarget.X == this.CurrentTile.X && NewTarget.Y == this.CurrentTile.Y)
      {
        this.CurrentPath = new List<PathNode>();
        this.IsNavigating = true;
        this.endAtCenter = _endAtCentre;
        this.CurrentPath.Add(new PathNode(NewTarget.X, NewTarget.Y));
        return true;
      }
      if (pathset.GetIsBlocked(OverrideStartPoint))
        return false;
      this.CurrentPath = hierarchy == null ? pathset.GetFullPathToLocation(OverrideStartPoint, NewTarget, this.LastCall_AllowDiagonals) : this.GetHierarchicalPath(hierarchy, pathset, OverrideStartPoint, NewTarget);
      if (flag1 && (OverrideStartPoint.X != this.CurrentTile.X || OverrideStartPoint.Y != this.CurrentTile.Y))
      {
        if (this.CurrentPath == null)
        {
          this.CurrentPath = new List<PathNode>();
          this.CurrentPath.Add(pathset.nodesasGrid[OverrideStartPoint.X, OverrideStartPoint.Y]);
        }
        else
          this.CurrentPath.Insert(0, pathset.nodesasGrid[OverrideStartPoint.X, OverrideStartPoint.Y]);
      }
      if (this.CurrentPath == null)
        return false;
      int num = flag2 ? 1 : 0;
      this.IsNavigating = true;
      this.endAtCenter = _endAtCentre;
      return this.CurrentPath.Last<PathNode>().Location.CompareMatches(NewTarget);
    }

    public void SetNewPath(List<PathNode> locations)
    {
      this.CurrentPath = locations;
      this.IsNavigating = locations.Count > 0;
    }

    private List<PathNode> GetHierarchicalPath(
      HierarchicalPathFind hierarchy,
      PathSet pathset,
      Vector2Int StartPoint,
      Vector2Int EndPoint)
    {
      List<HierarchicalNode> path = hierarchy.FindPath(pathset.nodesasGrid[StartPoint.X, StartPoint.Y].hierarchicalnode, pathset.nodesasGrid[EndPoint.X, EndPoint.Y].hierarchicalnode);
      if (path == null)
        return (List<PathNode>) null;
      List<PathNode> pathNodeList = new List<PathNode>();
      foreach (HierarchicalNode hierarchicalNode in path)
        pathNodeList.Add(new PathNode(pathset.nodesasGrid[(int) hierarchicalNode.location.X, (int) hierarchicalNode.location.Y]));
      return pathNodeList;
    }

    private void SmoothRelativeDistance(float dt)
    {
      if ((double) Math.Abs(this.relativeDistance - this.relativeDistanceTarget) <= 9.99999974737875E-06)
        return;
      this.relativeDistance += (float) (((double) this.relativeDistanceTarget - (double) this.relativeDistance) * 2.0) * dt;
    }

    private float CalcRelativeDistance(Vector2 vec) => (double) vec.X * (double) vec.Y != 0.0 ? 1.414f : 1f;

    private float DotProduct(Vector2 lhs, Vector2 rhs) => (float) ((double) lhs.X * (double) rhs.X + (double) lhs.Y * (double) rhs.Y);

    private void SetDirectionMoved(Vector2 vec)
    {
      if ((double) vec.Y > 0.0)
        this.directionmovedthisframe = DirectionPressed.Down;
      else if ((double) vec.Y < 0.0)
        this.directionmovedthisframe = DirectionPressed.Up;
      if ((double) vec.X > 0.0)
      {
        this.directionmovedthisframe = DirectionPressed.Right;
      }
      else
      {
        if ((double) vec.X >= 0.0)
          return;
        this.directionmovedthisframe = DirectionPressed.Left;
      }
    }

    private enum TileCenterNavState
    {
      Normal,
      StartOffset,
      EndOffset,
    }
  }
}
