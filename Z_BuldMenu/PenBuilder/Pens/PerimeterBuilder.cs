// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.PerimeterBuilder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens
{
  internal class PerimeterBuilder
  {
    public List<BuildTile> CommitedBuildTiles;
    private List<BuildTile> Uncommited;
    private List<BuildTile> floors;
    private BuildTile MousePointer;
    private bool ClosingLoop;
    private List<PerimiterWayPointSet> waypoints;
    private bool IsInPreviewStateForPlacingGate;
    private bool IsBlockedDueToHack;
    private bool TryingToBuildInAPen;
    private bool UseLessBlocks = true;
    private bool AFloorIsBlocked;

    public PerimeterBuilder(TILETYPE buildthispen, Player player)
    {
      this.waypoints = new List<PerimiterWayPointSet>();
      this.floors = new List<BuildTile>();
      this.ClosingLoop = false;
      this.Uncommited = new List<BuildTile>();
      this.CommitedBuildTiles = new List<BuildTile>();
      this.MousePointer = new BuildTile(TileMath.GetScreenCenter(), PerimeterTileStatus.AttachedToMouse);
      this.MousePointer.SetLinkIcon(IsMouse: true);
    }

    public List<Vector2Int> GetFloorsAsPositions()
    {
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      for (int index = 0; index < this.floors.Count; ++index)
        vector2IntList.Add(new Vector2Int(this.floors[index].TileLocation));
      return vector2IntList;
    }

    public void ReconstructFromPrisonZone(PrisonZone prisonzone, Player player)
    {
      for (int index = 0; index < prisonzone.FenceTiles.Count; ++index)
        this.CommitedBuildTiles.Add(new BuildTile(prisonzone.FenceTiles[index], PerimeterTileStatus.Commited));
    }

    public void SimpleSetCornerType(int ThisIndex, List<BuildTile> _CommitedBuildTiles) => PerimeterBuilderHelper.SimpleSetCornerType(_CommitedBuildTiles, ThisIndex);

    public int GetPenUsableSpace() => this.floors.Count;

    public List<BuildTile> GetFloors() => this.floors;

    public int GetMaxWidth()
    {
      int num1 = 100000;
      int num2 = -1;
      for (int index = 0; index < this.Uncommited.Count; ++index)
      {
        if (this.Uncommited[index].TileLocation.X < num1)
          num1 = this.Uncommited[index].TileLocation.X;
        if (this.Uncommited[index].TileLocation.X > num2)
          num2 = this.Uncommited[index].TileLocation.X;
      }
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
      {
        if (this.CommitedBuildTiles[index].TileLocation.X < num1)
          num1 = this.CommitedBuildTiles[index].TileLocation.X;
        if (this.CommitedBuildTiles[index].TileLocation.X > num2)
          num2 = this.CommitedBuildTiles[index].TileLocation.X;
      }
      return num2 - num1;
    }

    public bool IsBlockingGate()
    {
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
      {
        if (this.CommitedBuildTiles[index].TileLocation.X == 161 && this.CommitedBuildTiles[index].TileLocation.Y == 221)
          flag1 = true;
        if (this.CommitedBuildTiles[index].TileLocation.X == 163 && this.CommitedBuildTiles[index].TileLocation.Y == 221)
          flag2 = true;
      }
      for (int index = 0; index < this.Uncommited.Count; ++index)
      {
        if (this.Uncommited[index].TileLocation.X == 161 && this.Uncommited[index].TileLocation.Y == 221)
          flag1 = true;
        if (this.Uncommited[index].TileLocation.X == 163 && this.Uncommited[index].TileLocation.Y == 221)
          flag2 = true;
      }
      if (this.MousePointer.TileLocation.X == 161 && this.MousePointer.TileLocation.Y == 221)
        flag1 = true;
      if (this.MousePointer.TileLocation.X == 163 && this.MousePointer.TileLocation.Y == 221)
        flag2 = true;
      return flag2 & flag1;
    }

    public void CleanUpOnCancel()
    {
      for (int index = 0; index < this.Uncommited.Count; ++index)
        Z_GameFlags.pathfinder.UnblockTile(this.Uncommited[index].TileLocation.X, this.Uncommited[index].TileLocation.Y, true);
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
        Z_GameFlags.pathfinder.UnblockTile(this.CommitedBuildTiles[index].TileLocation.X, this.CommitedBuildTiles[index].TileLocation.Y, true);
      Z_GameFlags.pathfinder.ForceResolvePathFinding();
    }

    public void SetMouseLocation(Vector2Int TileLoc, bool ForceRemake, Player player)
    {
      if (!TileMath.TileIsInWorld(TileLoc.X, TileLoc.Y) || !(this.MousePointer.TryAndMove(TileLoc) | ForceRemake))
        return;
      this.TryingToBuildInAPen = false;
      if (this.CommitedBuildTiles.Count > 0)
      {
        this.Uncommited = new List<BuildTile>();
        PathNavigator pathNavigator = new PathNavigator(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation, 1f, false, NavigationTypeEnum.TileCenter);
        if (this.CommitedBuildTiles.Count == 1)
          this.CommitedBuildTiles[0].TileLocation.CompareMatches(TileLoc);
        if (!this.UseLessBlocks)
          Z_GameFlags.pathfinder.UnblockTile(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y);
        if (this.ClosingLoop)
        {
          this.ClosingLoop = false;
          for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
            this.CommitedBuildTiles[index].SetGreen();
          for (int index = 0; index < this.Uncommited.Count; ++index)
            this.Uncommited[index].SetGreen();
        }
        this.IsBlockedDueToHack = false;
        if (this.CommitedBuildTiles.Count > 0)
          this.CommitedBuildTiles[0].SetLinkIcon();
        if (!TileMath.TileIsInWorld(TileLoc.X, TileLoc.Y) && !TileLoc.CompareMatches(this.CommitedBuildTiles[0].TileLocation))
          pathNavigator.TryToGoHereSquare(TileLoc, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind);
        if (TileMath.TileIsInWorld(TileLoc.X, TileLoc.Y) && !TileLoc.CompareMatches(this.CommitedBuildTiles[0].TileLocation) && pathNavigator.TryToGoHereSquare(TileLoc, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
        {
          List<PathNode> currentPath = pathNavigator.GetCurrentPath();
          if (currentPath != null)
          {
            for (int index = 0; index < currentPath.Count; ++index)
            {
              if (!currentPath[index].Location.CompareMatches(TileLoc))
                this.Uncommited.Add(new BuildTile(currentPath[index].Location, PerimeterTileStatus.TempWall));
            }
          }
          int num = 0;
          bool flag = false;
          if (this.CommitedBuildTiles.Count > 1)
          {
            for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
            {
              if (this.CommitedBuildTiles[index].TileLocation.ThisIsNextToThis(TileLoc))
              {
                if (index == 0)
                  flag = true;
                ++num;
              }
            }
          }
          if (num == 1)
          {
            if (this.Uncommited.Count == 0 || flag)
              ;
          }
          else if (num <= 1 || !(num == 2 & flag))
            ;
        }
        else if (TileLoc.CompareMatches(this.CommitedBuildTiles[0].TileLocation) && this.CurrentLayoutHasDualAxisSoLoopCanBeClosed())
        {
          Z_GameFlags.pathfinder.UnblockTile(this.CommitedBuildTiles[0].TileLocation.X, this.CommitedBuildTiles[0].TileLocation.Y);
          if (pathNavigator.TryToGoHereSquare(TileLoc, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
          {
            this.ClosingLoop = true;
            this.CommitedBuildTiles[0].SetLinkIcon(true);
            List<PathNode> currentPath = pathNavigator.GetCurrentPath();
            if (currentPath != null)
            {
              for (int index = 0; index < currentPath.Count; ++index)
              {
                if (!currentPath[index].Location.CompareMatches(TileLoc))
                  this.Uncommited.Add(new BuildTile(currentPath[index].Location, PerimeterTileStatus.TempWall));
              }
            }
            if (this.floors.Count > 0)
            {
              if (this.AFloorIsBlocked)
              {
                for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
                  this.CommitedBuildTiles[index].SetRed();
                for (int index = 0; index < this.Uncommited.Count; ++index)
                  this.Uncommited[index].SetRed();
              }
              else
              {
                for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
                  this.CommitedBuildTiles[index].SetReady();
                for (int index = 0; index < this.Uncommited.Count; ++index)
                  this.Uncommited[index].SetReady();
              }
            }
          }
          Z_GameFlags.pathfinder.BlockTile(this.CommitedBuildTiles[0].TileLocation.X, this.CommitedBuildTiles[0].TileLocation.Y);
        }
        PerimeterBuilderHelper.ChechBuggyEdge(this.CommitedBuildTiles, this.Uncommited);
        if (this.ClosingLoop && this.GeturrentAnimalFloorSpace().Count == 0)
          this.MousePointer.SetMouseIsBlocked(true);
        if (!this.UseLessBlocks)
          Z_GameFlags.pathfinder.BlockTile(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y);
      }
      else if (TileLoc.X >= 0 && TileLoc.X < player.prisonlayout.layout.FloorTileTypes.GetLength(0) && (TileLoc.Y >= 0 && TileLoc.Y < player.prisonlayout.layout.FloorTileTypes.GetLength(1)))
      {
        if (TileData.IsThisAPenFloor(player.prisonlayout.layout.FloorTileTypes[TileLoc.X, TileLoc.Y].tiletype))
        {
          this.TryingToBuildInAPen = true;
          this.MousePointer.SetMouseIsBlocked(true);
        }
        else if (player.prisonlayout.layout.BaseTileTypes[TileLoc.X, TileLoc.Y] != null && TileData.IsThisAPenBoundary(player.prisonlayout.layout.BaseTileTypes[TileLoc.X, TileLoc.Y].tiletype))
        {
          this.MousePointer.SetMouseIsBlocked(true);
          this.TryingToBuildInAPen = true;
        }
      }
      this.RelayFoorDuringBuild();
      if (this.AFloorIsBlocked || this.Uncommited.Count != 0 || (this.CommitedBuildTiles.Count <= 0 || this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.ThisIsNextToThis(this.MousePointer.TileLocation)))
        return;
      this.IsBlockedDueToHack = true;
    }

    private void RelayFoorDuringBuild()
    {
      this.floors = new List<BuildTile>();
      this.AFloorIsBlocked = false;
      List<Vector2Int> vector2IntList = this.GeturrentAnimalFloorSpace(true);
      for (int index = 0; index < vector2IntList.Count; ++index)
      {
        this.floors.Add(new BuildTile(vector2IntList[index], PerimeterTileStatus.Floor));
        if (Z_GameFlags.pathfinder.GetIsBlocked(vector2IntList[index].X, vector2IntList[index].Y))
        {
          this.AFloorIsBlocked = true;
          this.floors[this.floors.Count - 1].SetRed();
        }
      }
    }

    public List<Vector2Int> GeturrentAnimalFloorSpace(bool IncludeMouseAndUncommited = false) => IncludeMouseAndUncommited && this.MousePointer != null ? PerimeterBuilderHelper.GeturrentAnimalFloorSpace(this.CommitedBuildTiles, this.Uncommited, this.MousePointer, IncludeMouseAndUncommited) : PerimeterBuilderHelper.GeturrentAnimalFloorSpace(this.CommitedBuildTiles, this.Uncommited, (BuildTile) null, IncludeMouseAndUncommited);

    private bool CurrentLayoutHasDualAxisSoLoopCanBeClosed()
    {
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
      {
        if (this.CommitedBuildTiles[0].TileLocation.X != this.CommitedBuildTiles[index].TileLocation.X)
          flag1 = true;
        if (this.CommitedBuildTiles[0].TileLocation.Y != this.CommitedBuildTiles[index].TileLocation.Y)
          flag2 = true;
      }
      return flag2 & flag1;
    }

    public int GetVolume() => this.floors.Count + this.CommitedBuildTiles.Count + this.Uncommited.Count;

    public int GetNumberOfFencePiecesToPayFor() => PerimeterBuilderHelper.GetNumberOfFencePiecesToPayFor(this.CommitedBuildTiles);

    public void SetToGatePlacement()
    {
      for (int index = this.CommitedBuildTiles.Count - 1; index > -1; --index)
        this.CommitedBuildTiles[index].SetGreen();
      this.IsInPreviewStateForPlacingGate = true;
    }

    public bool UpdatePerimeterBuilder(
      Player player,
      float DeltaTime,
      out int CurrentTileCount,
      bool CanAfford,
      out bool TriedToBuyButCouldNotAfford)
    {
      TriedToBuyButCouldNotAfford = false;
      CurrentTileCount = 0;
      bool flag1 = false;
      if ((player.inputmap.RightMousReleaseClick || player.inputmap.PressedBackOnController()) && this.waypoints.Count > 0)
      {
        bool flag2 = false;
        for (int index1 = 0; index1 < this.waypoints[this.waypoints.Count - 1].buildtiles.Count; ++index1)
        {
          for (int index2 = this.CommitedBuildTiles.Count - 1; index2 > -1; --index2)
          {
            if (this.CommitedBuildTiles[index2] == this.waypoints[this.waypoints.Count - 1].buildtiles[index1])
            {
              if (Z_GameFlags.pathfinder.GetIsBlocked(this.CommitedBuildTiles[index2].TileLocation.X, this.CommitedBuildTiles[index2].TileLocation.Y))
              {
                flag2 = true;
                Z_GameFlags.pathfinder.UnblockTile(this.CommitedBuildTiles[index2].TileLocation.X, this.CommitedBuildTiles[index2].TileLocation.Y, true);
              }
              this.CommitedBuildTiles.RemoveAt(index2);
            }
          }
        }
        if (this.CommitedBuildTiles.Count > 0 && Z_GameFlags.pathfinder.GetIsBlocked(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y))
        {
          flag2 = true;
          Z_GameFlags.pathfinder.UnblockTile(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y, true);
        }
        if (flag2)
          Z_GameFlags.pathfinder.ForceResolvePathFinding();
        this.Uncommited = new List<BuildTile>();
        this.waypoints.RemoveAt(this.waypoints.Count - 1);
        flag1 = true;
      }
      if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !flag1)
      {
        if (!CanAfford || this.AFloorIsBlocked && this.ClosingLoop || this.IsBlockedDueToHack)
        {
          if (!CanAfford)
            TriedToBuyButCouldNotAfford = true;
        }
        else if (this.ClosingLoop)
        {
          for (int index1 = 0; index1 < this.CommitedBuildTiles.Count; ++index1)
          {
            for (int index2 = 0; index2 < this.Uncommited.Count; ++index2)
            {
              if (this.Uncommited[index2].TileLocation.CompareMatches(this.CommitedBuildTiles[index1].TileLocation))
              {
                this.ClosingLoop = false;
                this.IsBlockedDueToHack = true;
              }
            }
          }
          if (!this.ClosingLoop)
            this.MousePointer.RedFlash();
        }
        if (!CanAfford || this.AFloorIsBlocked && this.ClosingLoop || (this.IsBlockedDueToHack || this.TryingToBuildInAPen))
        {
          for (int index = 0; index < this.Uncommited.Count; ++index)
            this.Uncommited[index].RedFlash();
          for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
            this.CommitedBuildTiles[index].RedFlash();
        }
        else if (!Z_GameFlags.pathfinder.GetIsBlocked(this.MousePointer.TileLocation.X, this.MousePointer.TileLocation.Y) && !this.MousePointer.MouseIsBlocked)
        {
          int count = this.CommitedBuildTiles.Count;
          if (this.Uncommited.Count > 0)
          {
            if (this.UseLessBlocks && this.CommitedBuildTiles.Count > 0)
            {
              if (Z_GameFlags.pathfinder.GetIsBlocked(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y))
                throw new Exception("THIS GOEAD AGAINST YOUR DESIGN");
              Z_GameFlags.pathfinder.BlockTile(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y, true);
            }
            if (this.Uncommited.Count != 1 || !this.Uncommited[0].TileLocation.CompareMatches(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation))
            {
              for (int index = 0; index < this.Uncommited.Count; ++index)
              {
                Z_GameFlags.pathfinder.BlockTile(this.Uncommited[index].TileLocation.X, this.Uncommited[index].TileLocation.Y, true);
                this.CommitedBuildTiles.Add(new BuildTile(this.Uncommited[index].TileLocation, PerimeterTileStatus.Commited));
              }
              Z_GameFlags.pathfinder.ForceResolvePathFinding();
            }
          }
          else if (this.UseLessBlocks && this.CommitedBuildTiles.Count > 0 && (!this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.CompareMatches(this.MousePointer.TileLocation) && !Z_GameFlags.pathfinder.GetIsBlocked(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y)))
            Z_GameFlags.pathfinder.BlockTile(this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.X, this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.Y, true);
          if (this.ClosingLoop || this.CommitedBuildTiles.Count <= 0 || !this.CommitedBuildTiles[this.CommitedBuildTiles.Count - 1].TileLocation.CompareMatches(this.MousePointer.TileLocation))
          {
            this.CommitedBuildTiles.Add(new BuildTile(this.MousePointer.TileLocation, PerimeterTileStatus.Commited));
            PerimiterWayPointSet perimiterWayPointSet = new PerimiterWayPointSet();
            for (int index = count; index < this.CommitedBuildTiles.Count; ++index)
              perimiterWayPointSet.buildtiles.Add(this.CommitedBuildTiles[index]);
            if (perimiterWayPointSet.buildtiles.Count > 0)
              this.waypoints.Add(perimiterWayPointSet);
            this.CommitedBuildTiles[0].SetLinkIcon();
          }
        }
        else if (this.ClosingLoop)
        {
          if (this.floors.Count > 0)
          {
            for (int index = 0; index < this.Uncommited.Count; ++index)
            {
              if (!this.Uncommited[index].TileLocation.CompareMatches(this.CommitedBuildTiles[0].TileLocation))
              {
                Z_GameFlags.pathfinder.BlockTile(this.Uncommited[index].TileLocation.X, this.Uncommited[index].TileLocation.Y);
                this.CommitedBuildTiles.Add(new BuildTile(this.Uncommited[index].TileLocation, PerimeterTileStatus.Commited));
              }
            }
            GameFlags.CollisionChanged = true;
            this.RelayFoorDuringBuild();
            return true;
          }
          for (int index = 0; index < this.Uncommited.Count; ++index)
            this.Uncommited[index].RedFlash();
          for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
            this.CommitedBuildTiles[index].RedFlash();
        }
      }
      if (flag1)
        this.SetMouseLocation(this.MousePointer.TileLocation, true, player);
      for (int index = 0; index < this.Uncommited.Count; ++index)
        this.Uncommited[index].UpdateBuildTile(DeltaTime);
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
        this.CommitedBuildTiles[index].UpdateBuildTile(DeltaTime);
      this.MousePointer.UpdateBuildTile(DeltaTime);
      CurrentTileCount = this.floors.Count + this.CommitedBuildTiles.Count + this.Uncommited.Count;
      return false;
    }

    public bool HasWayPoints() => this.waypoints.Count > 0;

    public List<Vector2Int> GetFenceTiles()
    {
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
        vector2IntList.Add(this.CommitedBuildTiles[index].TileLocation);
      return vector2IntList;
    }

    public List<BaseTileDescriptor> GetCommitedTiles(
      CellBlockType cellblocktype)
    {
      List<Vector2Int> floor = this.GeturrentAnimalFloorSpace();
      List<BaseTileDescriptor> baseTileDescriptorList = new List<BaseTileDescriptor>();
      for (int ThisIndex = 0; ThisIndex < this.CommitedBuildTiles.Count; ++ThisIndex)
        this.SimpleSetCornerType(ThisIndex, this.CommitedBuildTiles);
      bool flag = true;
      for (int index = 0; index < this.CommitedBuildTiles.Count; ++index)
      {
        Vector2Int tileLocation = this.CommitedBuildTiles[index].TileLocation;
        CellCornerType cellcornertype = this.CommitedBuildTiles[index].cellcornertype;
        InternalDirection iternaldirection;
        InnerDirectionCalculator.GetThisInnerDirection(index, tileLocation, floor, out iternaldirection, this.CommitedBuildTiles);
        BaseTileDescriptor baseTileDescriptor = new BaseTileDescriptor(new Vector2Int(tileLocation.X, tileLocation.Y), cellcornertype);
        switch (cellcornertype)
        {
          case CellCornerType.StraightLeftRight:
            baseTileDescriptor.RotationClockWise = 3;
            if (iternaldirection == InternalDirection.Down)
              baseTileDescriptor.RotationClockWise = 1;
            if (flag)
            {
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
              break;
            }
            break;
          case CellCornerType.StraightUpDown:
            baseTileDescriptor.RotationClockWise = 2;
            if (iternaldirection == InternalDirection.Right)
              baseTileDescriptor.RotationClockWise = 0;
            if (flag)
            {
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
              break;
            }
            break;
          case CellCornerType.OuterCorner_TopLeft:
            baseTileDescriptor.RotationClockWise = 0;
            baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
            if (iternaldirection == InternalDirection.Up)
            {
              baseTileDescriptor.RotationClockWise = 3;
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.InnerCorner);
              break;
            }
            break;
          case CellCornerType.OuterCorner_TopRight:
            baseTileDescriptor.RotationClockWise = 1;
            baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
            if (iternaldirection == InternalDirection.Up)
            {
              baseTileDescriptor.RotationClockWise = 2;
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.InnerCorner);
              break;
            }
            break;
          case CellCornerType.OuterCorner_BottomLeft:
            baseTileDescriptor.RotationClockWise = 3;
            baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
            if (iternaldirection == InternalDirection.Down)
            {
              baseTileDescriptor.RotationClockWise = 1;
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.InnerCorner);
              break;
            }
            break;
          case CellCornerType.OuterCorner_BottomRight:
            baseTileDescriptor.RotationClockWise = 2;
            baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
            if (iternaldirection == InternalDirection.Down)
            {
              baseTileDescriptor.RotationClockWise = 0;
              baseTileDescriptor.tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.InnerCorner);
              break;
            }
            break;
        }
        baseTileDescriptorList.Add(baseTileDescriptor);
      }
      return baseTileDescriptorList;
    }

    public void DrawPerimeterBuilder()
    {
      for (int index = 0; index < this.floors.Count; ++index)
        this.floors[index].DrawBuildTile();
      if (!this.IsInPreviewStateForPlacingGate)
      {
        for (int index = 0; index < this.Uncommited.Count; ++index)
          this.Uncommited[index].DrawBuildTile();
      }
      for (int index = this.CommitedBuildTiles.Count - 1; index > -1; --index)
        this.CommitedBuildTiles[index].DrawBuildTile(this.IsInPreviewStateForPlacingGate);
      if (this.ClosingLoop || this.IsInPreviewStateForPlacingGate)
        return;
      this.MousePointer.DrawBuildTile();
    }
  }
}
