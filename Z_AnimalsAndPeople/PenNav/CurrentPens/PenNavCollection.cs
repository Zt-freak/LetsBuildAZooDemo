// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.PenNav.CurrentPens.PenNavCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;

namespace TinyZoo.Z_AnimalsAndPeople.PenNav.CurrentPens
{
  internal class PenNavCollection
  {
    public int PenUID;
    public NavLocation[,] navlocations;
    private List<Vector2Int> FloorLocations;
    private PathSet PenPathSet;
    public int Left;
    public int Top;

    public PenNavCollection(int _PenUID, List<Vector2Int> _FloorLocations)
    {
      this.FloorLocations = _FloorLocations;
      this.PenUID = _PenUID;
      int num1 = 0;
      int num2 = 0;
      if (this.FloorLocations.Count > 0)
      {
        this.Left = this.FloorLocations[0].X;
        num1 = this.FloorLocations[0].X;
        num2 = this.FloorLocations[0].Y;
        this.Top = this.FloorLocations[0].Y;
      }
      if (this.FloorLocations.Count > 1)
      {
        for (int index = 0; index < this.FloorLocations.Count; ++index)
        {
          if (this.FloorLocations[index].X < this.Left)
            this.Left = this.FloorLocations[index].X;
          if (this.FloorLocations[index].X > num1)
            num1 = this.FloorLocations[index].X;
          if (this.FloorLocations[index].Y < this.Top)
            this.Top = this.FloorLocations[index].Y;
          if (this.FloorLocations[index].Y > num2)
            num2 = this.FloorLocations[index].Y;
        }
      }
      this.navlocations = new NavLocation[num1 - this.Left + 1, num2 - this.Top + 1];
      this.PenPathSet = new PathSet();
      this.PenPathSet.CreateGrid(num1 - this.Left + 1, num2 - this.Top + 1);
      this.PenPathSet.BlockAllTiles();
      for (int index1 = 0; index1 < this.navlocations.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.navlocations.GetLength(1); ++index2)
        {
          this.navlocations[index1, index2] = new NavLocation(new Vector2Int(this.Left + index1, this.Top + index2));
          for (int index3 = 0; index3 < this.FloorLocations.Count; ++index3)
          {
            if (this.FloorLocations[index3].CompareMatches(this.navlocations[index1, index2].Position))
            {
              this.navlocations[index1, index2].IsValidFloor = true;
              this.PenPathSet.UnblockTile(this.navlocations[index1, index2].Position.X - this.Left, this.navlocations[index1, index2].Position.Y - this.Top);
            }
          }
        }
      }
    }

    public void UnblockThisTile(int Location_WorldSpaceX, int Location_WorldSpaceY)
    {
      this.navlocations[Location_WorldSpaceX - this.Left, Location_WorldSpaceY - this.Top].IsValidFloor = true;
      this.PenPathSet.UnblockTile(Location_WorldSpaceX - this.Left, Location_WorldSpaceY - this.Top);
    }

    public List<PathNode> GetPathToHere(
      Vector2Int TargetLocation_WorldSpace,
      Vector2Int CurrentLocationWorldSpace)
    {
      return this.PenPathSet.GetFullPathToLocation(new Vector2Int(CurrentLocationWorldSpace.X - this.Left, CurrentLocationWorldSpace.Y - this.Top), new Vector2Int(TargetLocation_WorldSpace.X - this.Left, TargetLocation_WorldSpace.Y - this.Top), false);
    }

    public void BlockThisTile(int Location_WorldSpaceX, int Location_WorldSpaceY)
    {
      this.navlocations[Location_WorldSpaceX - this.Left, Location_WorldSpaceY - this.Top].IsValidFloor = false;
      this.PenPathSet.BlockTile(Location_WorldSpaceX - this.Left, Location_WorldSpaceY - this.Top);
    }

    public bool HasThisFloorTile(int XL, int YL)
    {
      XL -= this.Left;
      YL -= this.Top;
      if (XL >= 0 && XL < this.navlocations.GetLength(0) && (YL >= 0 && YL < this.navlocations.GetLength(1)))
      {
        for (int index = 0; index < this.FloorLocations.Count; ++index)
        {
          if (this.FloorLocations[index].X == XL + this.Left && this.FloorLocations[index].Y == YL + this.Top)
            return true;
        }
      }
      return false;
    }

    public bool CanGoHere(Vector2Int location) => location.X >= 0 && location.X < this.navlocations.GetLength(0) && (location.Y >= 0 && location.Y < this.navlocations.GetLength(1)) && this.navlocations[location.X, location.Y].IsValidFloor;

    public bool IsInNavGrid(Vector2 Vlocation, out Vector2Int GridLoc)
    {
      GridLoc = TileMath.GetWorldSpaceToTile(Vlocation);
      GridLoc.X -= this.Left;
      GridLoc.Y -= this.Top;
      return GridLoc.X >= 0 && GridLoc.X < this.navlocations.GetLength(0) && (GridLoc.Y >= 0 && GridLoc.Y < this.navlocations.GetLength(1)) && this.navlocations[GridLoc.X, GridLoc.Y].IsValidFloor;
    }

    public void GetLocationToArrayLocation(
      out Vector2Int StartLocation,
      Vector2 WorldSpaceLoc,
      out Vector2Int CurrentWorldSpaceLocation)
    {
      CurrentWorldSpaceLocation = TileMath.GetWorldSpaceToTile(WorldSpaceLoc);
      for (int _X = 0; _X < this.navlocations.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < this.navlocations.GetLength(1); ++_Y)
        {
          if (this.navlocations[_X, _Y].IsValidFloor && this.navlocations[_X, _Y].Position.X == CurrentWorldSpaceLocation.X && this.navlocations[_X, _Y].Position.Y == CurrentWorldSpaceLocation.Y)
          {
            StartLocation = new Vector2Int(_X, _Y);
            return;
          }
        }
      }
      this.GetRandomStartLocation(out StartLocation, out CurrentWorldSpaceLocation);
    }

    public void GetRandomStartLocation(
      out Vector2Int StartLocation,
      out Vector2Int CurrentWorldSpaceLocation)
    {
      int index = TinyZoo.Game1.Rnd.Next(0, this.FloorLocations.Count - 1);
      StartLocation = (Vector2Int) null;
      for (int _X = 0; _X < this.navlocations.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < this.navlocations.GetLength(1); ++_Y)
        {
          if (this.FloorLocations[index].CompareMatches(this.navlocations[_X, _Y].Position))
          {
            StartLocation = new Vector2Int(_X, _Y);
            CurrentWorldSpaceLocation = new Vector2Int(_X + this.Left, _Y + this.Top);
            return;
          }
        }
      }
      throw new Exception("NOT FOUND");
    }
  }
}
