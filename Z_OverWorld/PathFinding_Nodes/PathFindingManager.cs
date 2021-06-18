// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.PathFindingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using SEngine.Input;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.PathFinding.HierarchicalPathFinding;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes
{
  internal class PathFindingManager
  {
    internal static bool UseCityBlocks = false;
    internal static bool useHierarchical = true;
    private static int[,] FloorIsFastSurface;
    internal static EntranceBlockManager entranceblockmanager;
    internal static int CityBlockSize = 10;
    private CityMap city;
    internal HierarchicalPathFind hierachicalPathFind;
    internal static bool WaitUntilInitialized = true;
    private static List<Vector2Int> AllTeleports;
    private Vector2Int DebugLocation;
    private Vector2Int DebugTargetLocation;
    private float num;

    public PathFindingManager()
    {
      if (PathFindingManager.UseCityBlocks)
        this.city = new CityMap(TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize(), PathFindingManager.CityBlockSize);
      if (PathFindingManager.useHierarchical)
        this.hierachicalPathFind = new HierarchicalPathFind();
      PathFindingManager.CreateDirectory();
    }

    internal static void CreateDirectory() => Z_GameFlags.Location_Directory = new LocationDirectory(TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize(), PathFindingManager.CityBlockSize);

    internal static void SetFloorSpeed(int LocX, int LocY, FloorSpeedType floorspeed) => PathFindingManager.FloorIsFastSurface[LocX, LocY] = (int) floorspeed;

    internal static float GetFloorSpeed(int XLoc, int YLoc)
    {
      switch (PathFindingManager.FloorIsFastSurface[XLoc, YLoc])
      {
        case 0:
          return 0.7f;
        case 1:
          return 1.5f;
        case 2:
          return 10f;
        default:
          return 1f;
      }
    }

    internal static void CreateFloor(int XSize, int YSize)
    {
      PathFindingManager.FloorIsFastSurface = new int[XSize, YSize];
      PathFindingManager.entranceblockmanager = new EntranceBlockManager(XSize, YSize);
    }

    public void ForceItemIntoCityBlock(
      TILETYPE tiletype,
      int LocationX,
      int LocationY,
      int RotationColockWise)
    {
      Z_GameFlags.Location_Directory.AddImportantStructure(tiletype, LocationX, LocationY, RotationColockWise);
    }

    public void ConstructPathfinding()
    {
      if (!PathFindingManager.useHierarchical)
        return;
      this.hierachicalPathFind.ConstructHierarchy(GameFlags.pathset.nodesasGrid, 4, preferStraight: true);
    }

    public void RecreateCityBlocks(Player player)
    {
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.shopentries[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.shopentries[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      for (int index = 0; index < player.shopstatus.Toilets.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.Toilets[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.Toilets[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      for (int index = 0; index < player.shopstatus.ArchitectOffice.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.ArchitectOffice[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.ArchitectOffice[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      for (int index = 0; index < player.shopstatus.Benches.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.Benches[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.Benches[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      for (int index = 0; index < player.shopstatus.FacilitiesWithEmployees.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.FacilitiesWithEmployees[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.FacilitiesWithEmployees[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      for (int index = 0; index < player.shopstatus.Bins.Count; ++index)
      {
        Vector2Int locationOfThisShop = player.shopstatus.Bins[index].LocationOfThisShop;
        Z_GameFlags.pathfinder.ForceItemIntoCityBlock(player.shopstatus.Bins[index].tiletype, locationOfThisShop.X, locationOfThisShop.Y, player.prisonlayout.layout.BaseTileTypes[locationOfThisShop.X, locationOfThisShop.Y].RotationClockWise);
      }
      if (!player.storerooms.HasBuiltStoreRoom)
        return;
      Z_GameFlags.pathfinder.ForceItemIntoCityBlock(TILETYPE.StoreRoom, player.storerooms.StorRoomcontents.StoreRoomLocation.X, player.storerooms.StorRoomcontents.StoreRoomLocation.Y, player.prisonlayout.layout.BaseTileTypes[player.storerooms.StorRoomcontents.StoreRoomLocation.X, player.storerooms.StorRoomcontents.StoreRoomLocation.Y].RotationClockWise);
    }

    public List<PathNode> GetFullPathToLocation(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      bool AllowDiagonals,
      bool UseHierarchical,
      PathSet pathset,
      Vector2Int topLeftBoundary = null,
      Vector2Int bottomRightBoundary = null)
    {
      return UseHierarchical ? this.GetFullPathToLocation(StartPoint, EndPoint, AllowDiagonals) : pathset.GetFullPathToLocation(StartPoint, EndPoint, AllowDiagonals, topLeftBoundary, bottomRightBoundary);
    }

    public void ForceResolvePathFinding()
    {
      if (!PathFindingManager.useHierarchical)
        return;
      this.hierachicalPathFind.UpdateCachedPaths();
    }

    public void AddNode(TILETYPE tiletype, int LocationX, int LocationY, int RotationClockwise)
    {
      if (PathFindingManager.UseCityBlocks)
        this.city.AddNode(tiletype, LocationX, LocationY, RotationClockwise);
      Z_GameFlags.Location_Directory.AddImportantStructure(tiletype, LocationX, LocationY, RotationClockwise);
    }

    public void RemoveNode(TILETYPE tiletype, Vector2Int Location, int RotationClockwise)
    {
      if (PathFindingManager.UseCityBlocks)
        this.city.RemoveNode(tiletype, Location, RotationClockwise);
      Z_GameFlags.Location_Directory.RemoveImportantStructure(tiletype, Location.X, Location.Y, RotationClockwise);
    }

    public bool GetIsBlocked(int Xloc, int YLoc) => GameFlags.pathset.GetIsBlocked(new Vector2Int(Xloc, YLoc));

    public bool GetCanCrossFast(int CurrentXloc, int CurrentYLoc, DirectionPressed GoingThisWay) => GameFlags.pathset.GetCanCrossFast(CurrentXloc, CurrentYLoc, GoingThisWay);

    public bool GetCanCross(int CurrentXloc, int CurrentYLoc, int TargetXloc, int TargetYLoc) => GameFlags.pathset.GetCanCross(CurrentXloc, CurrentYLoc, TargetXloc, TargetYLoc);

    public bool GetIsBlockedWater(int Xloc, int YLoc) => GameFlags.Water_PathSet.GetIsBlocked(new Vector2Int(Xloc, YLoc));

    public void CreateWarpGate(Vector2Int TeleportStartPoint)
    {
      GameFlags.pathset.nodesasGrid[TeleportStartPoint.X, TeleportStartPoint.Y].IsWarp = true;
      if (PathFindingManager.AllTeleports == null)
        PathFindingManager.AllTeleports = new List<Vector2Int>();
      for (int index = 0; index < PathFindingManager.AllTeleports.Count; ++index)
        this.hierachicalPathFind.CreateWarpGateLink(GameFlags.pathset.nodesasGrid, TeleportStartPoint, PathFindingManager.AllTeleports[index]);
      PathFindingManager.AllTeleports.Add(TeleportStartPoint);
    }

    public void BreakWarpGate(Vector2Int TeleportToDestroy)
    {
      GameFlags.pathset.nodesasGrid[TeleportToDestroy.X, TeleportToDestroy.Y].IsWarp = false;
      bool flag = false;
      if (PathFindingManager.AllTeleports == null)
        throw new Exception("No teleports constructed!");
      for (int index = PathFindingManager.AllTeleports.Count - 1; index > -1; --index)
      {
        if (PathFindingManager.AllTeleports[index].CompareMatches(TeleportToDestroy))
        {
          flag = true;
          PathFindingManager.AllTeleports.RemoveAt(index);
        }
        else
          this.hierachicalPathFind.BreakWarpGateLink(GameFlags.pathset.nodesasGrid, TeleportToDestroy, PathFindingManager.AllTeleports[index]);
      }
      if (!flag)
        throw new Exception("THIS DIDNT EXIST!");
    }

    public void UnblockTile(int Xloc, int YLoc, bool DelayRePathingQuad = false)
    {
      int num = DelayRePathingQuad ? 1 : 0;
      if (PathFindingManager.UseCityBlocks)
        this.city.UnblockTile(Xloc, YLoc, DelayRePathingQuad);
      if (PathFindingManager.useHierarchical && !PathFindingManager.WaitUntilInitialized)
        this.hierachicalPathFind.UnblockNode(GameFlags.pathset.nodesasGrid, Xloc, YLoc, DelayRePathingQuad);
      GameFlags.pathset.UnblockTile(Xloc, YLoc);
    }

    public void BlockWaterTile(int Xloc, int YLoc) => GameFlags.Water_PathSet.BlockTile(Xloc, YLoc);

    public void UnblockWaterTile(int Xloc, int YLoc) => GameFlags.Water_PathSet.UnblockTile(Xloc, YLoc);

    public void RemoveWall(
      int Xloc,
      int YLoc,
      bool All,
      bool North,
      bool East,
      bool South,
      bool West,
      bool delayRepathing = true)
    {
      int num = PathFindingManager.WaitUntilInitialized ? 1 : 0;
      GameFlags.pathset.RemoveWall(Xloc, YLoc, All, North, East, South, West);
    }

    public void CreateWall(
      int Xloc,
      int YLoc,
      bool All,
      bool North,
      bool East,
      bool South,
      bool West,
      bool delayRepathing = true)
    {
      if (!PathFindingManager.WaitUntilInitialized)
        this.hierachicalPathFind.FenceNode(GameFlags.pathset.nodesasGrid, Xloc, YLoc, North, East, South, West, delayRepathing);
      GameFlags.pathset.CreateWall(Xloc, YLoc, All, North, East, South, West);
    }

    public void BlockTile(int Xloc, int YLoc, bool delayRepathing = false)
    {
      int num = delayRepathing ? 1 : 0;
      if (PathFindingManager.UseCityBlocks)
        this.city.BlockTile(Xloc, YLoc);
      if (GameFlags.pathset.nodesasGrid[Xloc, YLoc].IsWarp)
        return;
      if (PathFindingManager.useHierarchical && !PathFindingManager.WaitUntilInitialized)
        this.hierachicalPathFind.BlockNode(GameFlags.pathset.nodesasGrid, Xloc, YLoc, delayRepathing);
      GameFlags.pathset.BlockTile(Xloc, YLoc);
    }

    public void UpdatePathFindingManager(Player player)
    {
      if (PathFindingManager.UseCityBlocks)
      {
        this.city.UpdatePathFinding();
        if ((double) player.player.touchinput.MultiTouchTapArray[0].X > 0.0)
          this.DebugLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.MultiTouchTapArray[0]);
        else if (MouseStatus.RMouseClicked)
          this.DebugTargetLocation = this.DebugTargetLocation != null ? (Vector2Int) null : TileMath.GetScreenSPaceToTileLocation(player.inputmap.PointerLocation);
      }
      if (PathFindingManager.useHierarchical)
        this.hierachicalPathFind.UpdateCachedPaths();
      ++this.num;
    }

    public List<PathNode> GetFullPathToLocation(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      bool AllowDiagonals)
    {
      if (!PathFindingManager.useHierarchical)
        return GameFlags.pathset.GetFullPathToLocation(StartPoint, EndPoint, AllowDiagonals);
      PathNode[,] nodesasGrid = GameFlags.pathset.nodesasGrid;
      List<HierarchicalNode> path = this.hierachicalPathFind.FindPath(nodesasGrid[StartPoint.X, StartPoint.Y].hierarchicalnode, nodesasGrid[EndPoint.X, EndPoint.Y].hierarchicalnode);
      List<PathNode> pathNodeList = new List<PathNode>();
      if (path == null)
        return (List<PathNode>) null;
      foreach (HierarchicalNode hierarchicalNode in path)
        pathNodeList.Add(new PathNode((int) hierarchicalNode.location.X, (int) hierarchicalNode.location.Y));
      return pathNodeList;
    }

    public void DrawEntranceBlocks() => PathFindingManager.entranceblockmanager.DrawAllBlocks();

    public void DrawPathFindingManager(float TileScale)
    {
      if (!PathFindingManager.UseCityBlocks)
        return;
      if (this.DebugLocation != null)
        this.city.DrawCityMapCollisionWithInfo(this.DebugLocation, TileScale, this.DebugTargetLocation);
      else
        this.city.DrawCityMapCollision(TileScale);
    }
  }
}
