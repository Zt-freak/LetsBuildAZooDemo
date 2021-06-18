// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.LayoutData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.PlayerDir
{
  internal class LayoutData
  {
    public LayoutEntry[,] BaseTileTypes;
    public LayoutEntry[,] FloorTileTypes;
    private static TileInfo infoStatic;

    public LayoutData() => this.Create();

    private void Create()
    {
      this.FloorTileTypes = new LayoutEntry[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      this.BaseTileTypes = new LayoutEntry[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      PathFindingManager.CreateFloor(this.FloorTileTypes.GetLength(0), this.FloorTileTypes.GetLength(1));
      for (int index1 = 0; index1 < TileMath.GetOverWorldMapSize_XDefault(); ++index1)
      {
        for (int index2 = 0; index2 < TileMath.GetOverWorldMapSize_YSize(); ++index2)
        {
          this.BaseTileTypes[index1, index2] = new LayoutEntry(TILETYPE.None);
          this.FloorTileTypes[index1, index2] = new LayoutEntry(TILETYPE.None);
        }
      }
      Random random = new Random(99);
      for (int index1 = 0; index1 < 5400; ++index1)
      {
        int index2 = random.Next(0, TileMath.GetOverWorldMapSize_XDefault());
        int index3 = random.Next(0, TileMath.GetOverWorldMapSize_YSize());
        if (index2 % 2 == 1 && index3 % 2 == 1)
        {
          if (index3 != 51 && (index2 >= TileMath.GetOverWorldMapSize_XDefault() || this.BaseTileTypes[index2 + 1, index3].tiletype != TILETYPE.PinkMoonPlant))
            this.BaseTileTypes[index2, index3] = new LayoutEntry(TILETYPE.BoundaryTree);
        }
        else if ((index2 <= 2 || this.BaseTileTypes[index2 - 2, index3].tiletype != TILETYPE.BoundaryTree) && (index2 <= 1 || this.BaseTileTypes[index2 - 1, index3].tiletype != TILETYPE.BoundaryTree))
          this.BaseTileTypes[index2, index3] = new LayoutEntry(TILETYPE.PinkMoonPlant);
      }
      for (int index1 = 0; index1 < 600; ++index1)
      {
        int index2 = random.Next(0, TileMath.GetOverWorldMapSize_XDefault());
        int index3 = random.Next(0, TileMath.GetOverWorldMapSize_YSize());
        if (index2 % 2 == 1 && index3 % 2 == 1 && index3 != 51 && (index2 >= TileMath.GetOverWorldMapSize_XDefault() || this.BaseTileTypes[index2 + 1, index3].tiletype != TILETYPE.PinkMoonPlant))
        {
          this.BaseTileTypes[index2, index3] = new LayoutEntry(TILETYPE.BoundaryTree);
          this.BaseTileTypes[index2 - 1, index3] = new LayoutEntry(TILETYPE.None);
        }
      }
    }

    public int GetTotalOfThese(TILETYPE buildingtype)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.BaseTileTypes.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.BaseTileTypes.GetLength(1); ++index2)
        {
          if (this.BaseTileTypes[index1, index2].tiletype == buildingtype && !this.BaseTileTypes[index1, index2].isChild())
            ++num;
        }
      }
      return num;
    }

    public int GetTotalOfThese(CATEGORYTYPE categorytype)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.BaseTileTypes.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.BaseTileTypes.GetLength(1); ++index2)
        {
          if (this.BaseTileTypes[index1, index2].tiletype != TILETYPE.None && !this.BaseTileTypes[index1, index2].isChild() && TileData.GetTileInfo(this.BaseTileTypes[index1, index2].tiletype).categorytype == categorytype)
            ++num;
        }
      }
      return num;
    }

    public void SellStructure(
      Vector2Int position,
      LayoutEntry layout,
      bool ForceSell = false,
      bool SkipRebuildWalls = false,
      bool IsSingleTile = false,
      bool IsPenItem = false)
    {
      if (this.BaseTileTypes[position.X, position.Y] == null)
        throw new Exception("DAMMN");
      if (this.BaseTileTypes[position.X, position.Y].ParentLocation != null && (this.BaseTileTypes[position.X, position.Y].ParentLocation.X != 0 || this.BaseTileTypes[position.X, position.Y].ParentLocation.Y != 0))
        throw new Exception("CANNOT SELL A CHILD!");
      if (this.BaseTileTypes[position.X, position.Y].isChild())
        throw new Exception("CANNOT");
      if (!ForceSell && layout.tiletype != this.BaseTileTypes[position.X, position.Y].tiletype)
        throw new Exception("CANNOT");
      if (!ForceSell && layout.tiletype == TILETYPE.None)
        return;
      if (layout != null && CategoryData.IsThisATree(layout.tiletype))
        CarbonMap.AddTree(position);
      TileInfo tileInfo = TileData.GetTileInfo(this.BaseTileTypes[position.X, position.Y].tiletype);
      bool flag = false;
      if (tileInfo != null && !IsPenItem)
        PathFindingManager.entranceblockmanager.RemoveBlock(tileInfo, this.BaseTileTypes[position.X, position.Y].RotationClockWise, position, this.BaseTileTypes[position.X, position.Y].RotationClockWise, this.BaseTileTypes[position.X, position.Y].tiletype);
      if (IsSingleTile)
      {
        this.BaseTileTypes[position.X, position.Y].tiletype = TILETYPE.None;
        this.BaseTileTypes[position.X, position.Y].UnsetChild();
        if (Z_GameFlags.pathfinder.GetIsBlocked(position.X, position.Y))
          Z_GameFlags.pathfinder.UnblockTile(position.X, position.Y);
      }
      else
      {
        if (TileData.IsThisAWarpGate(layout.tiletype))
          Z_GameFlags.pathfinder.BreakWarpGate(position + TileData.GetTileInfo(layout.tiletype).GetPurchasingLocation(layout.RotationClockWise));
        for (int index1 = 0; index1 < tileInfo.GetTileWidth(layout.RotationClockWise); ++index1)
        {
          for (int index2 = 0; index2 < tileInfo.GetTileHeight(layout.RotationClockWise); ++index2)
          {
            this.BaseTileTypes[position.X - tileInfo.GetXTileOrigin(layout.RotationClockWise) + index1, position.Y - tileInfo.GetYTileOrigin(layout.RotationClockWise) + index2].tiletype = TILETYPE.None;
            this.BaseTileTypes[position.X - tileInfo.GetXTileOrigin(layout.RotationClockWise) + index1, position.Y - tileInfo.GetYTileOrigin(layout.RotationClockWise) + index2].UnsetChild();
            if (Z_GameFlags.pathfinder.GetIsBlocked(position.X - tileInfo.GetXTileOrigin(layout.RotationClockWise) + index1, position.Y - tileInfo.GetYTileOrigin(layout.RotationClockWise) + index2))
            {
              flag = true;
              Z_GameFlags.pathfinder.UnblockTile(position.X - tileInfo.GetXTileOrigin(layout.RotationClockWise) + index1, position.Y - tileInfo.GetYTileOrigin(layout.RotationClockWise) + index2, true);
            }
          }
        }
        if (flag)
          Z_GameFlags.pathfinder.ForceResolvePathFinding();
      }
      if (SkipRebuildWalls)
        return;
      PrisonWalls.CheckPrisonWalls(this.BaseTileTypes);
    }

    public void RevertFloor(Vector2Int Location, BuildHistory buildhistory)
    {
      this.FloorTileTypes[Location.X, Location.Y].TEMP_OldUnderFloorType = buildhistory.OGUnderFloor;
      this.FloorTileTypes[Location.X, Location.Y].tiletype = buildhistory.OGTileType;
      this.FloorTileTypes[Location.X, Location.Y].RotationClockWise = buildhistory.OG_rotation;
    }

    public void RevertFloor(Vector2Int Location, bool IsUnderFloor = false)
    {
      if (this.FloorTileTypes[Location.X, Location.Y] == null)
        return;
      if (IsUnderFloor)
      {
        if (this.FloorTileTypes[Location.X, Location.Y].TEMP_OldUnderFloorType != TILETYPE.None)
          this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype = this.FloorTileTypes[Location.X, Location.Y].TEMP_OldUnderFloorType;
        else
          this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
      }
      else if (this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype != TILETYPE.None)
        this.FloorTileTypes[Location.X, Location.Y].tiletype = this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype;
      else
        this.FloorTileTypes[Location.X, Location.Y].tiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
    }

    public void AddTileFromTileRenderer(
      TileRenderer tilerenderer,
      bool IsFloorLayer = false,
      bool IsUnderFloor = false,
      bool IsPenItem = false)
    {
      if (IsFloorLayer)
      {
        if (tilerenderer.XWidth > 1 || tilerenderer.YHeight > 1)
          throw new Exception("Does not support this");
        if (IsUnderFloor)
        {
          if (this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype != this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].TEMP_OldUnderFloorType)
            this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].TEMP_OldUnderFloorType = this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype;
          this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype = tilerenderer.tiletypeonconstruct;
        }
        else
        {
          if (tilerenderer.RotationOnConstruct > 0)
          {
            if (this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype == TILETYPE.None)
              this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype = this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype;
            else if (this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype != tilerenderer.tiletypeonconstruct)
              this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype = this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype;
            if (this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype == this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype)
              this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
          }
          if (this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y] == null)
          {
            this.TryToShiftTopFloorToUnderFloor(tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y);
            this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y] = new LayoutEntry(tilerenderer.tiletypeonconstruct);
          }
          else
          {
            this.TryToShiftTopFloorToUnderFloor(tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y);
            this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype = tilerenderer.tiletypeonconstruct;
            this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].RotationClockWise = tilerenderer.RotationOnConstruct;
          }
          if (TileData.IsSlowFloor(this.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype))
            PathFindingManager.SetFloorSpeed(tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y, FloorSpeedType.SlowDirt);
          else
            PathFindingManager.SetFloorSpeed(tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y, FloorSpeedType.FastConcrete);
        }
      }
      else
      {
        bool flag1 = false;
        bool flag2 = TileData.ThisStructureIsBlocking(tilerenderer.Ref_layoutentry.tiletype);
        if (TileData.IsThisAWarpGate(tilerenderer.Ref_layoutentry.tiletype))
          Z_GameFlags.pathfinder.CreateWarpGate(tilerenderer.TileLocation + TileData.GetTileInfo(tilerenderer.Ref_layoutentry.tiletype).GetPurchasingLocation(tilerenderer.Ref_layoutentry.RotationClockWise));
        if (CategoryData.IsThisATree(tilerenderer.Ref_layoutentry.tiletype))
          CarbonMap.AddTree(tilerenderer.TileLocation);
        for (int index1 = tilerenderer.TileLocation.X - tilerenderer.XOrigin; index1 < tilerenderer.TileLocation.X - tilerenderer.XOrigin + tilerenderer.XWidth; ++index1)
        {
          for (int index2 = tilerenderer.TileLocation.Y - tilerenderer.YOrigin; index2 < tilerenderer.TileLocation.Y - tilerenderer.YOrigin + tilerenderer.YHeight; ++index2)
          {
            if (this.BaseTileTypes[index1, index2] == null)
            {
              this.BaseTileTypes[index1, index2] = new LayoutEntry(tilerenderer.Ref_layoutentry.tiletype);
              this.BaseTileTypes[index1, index2].RotationClockWise = tilerenderer.RotationOnConstruct;
            }
            if (tilerenderer.TileLocation.X == index1 && tilerenderer.TileLocation.Y == index2)
            {
              this.BaseTileTypes[index1, index2].tiletype = tilerenderer.Ref_layoutentry.tiletype;
              this.BaseTileTypes[index1, index2].RotationClockWise = tilerenderer.RotationOnConstruct;
            }
            else
            {
              this.BaseTileTypes[index1, index2].tiletype = tilerenderer.Ref_layoutentry.tiletype;
              this.BaseTileTypes[index1, index2].SetChild(tilerenderer.TileLocation, tilerenderer.Ref_layoutentry.tiletype);
            }
            if (flag2)
            {
              Z_GameFlags.pathfinder.BlockTile(index1, index2, true);
              GameFlags.CollisionChanged = true;
              flag1 = true;
            }
            else if (tilerenderer.Refinfo.HasWall(tilerenderer.RotationOnConstruct))
              tilerenderer.Refinfo.BlockWall(index1, index2, tilerenderer.RotationOnConstruct);
          }
        }
        if (!IsPenItem)
          PathFindingManager.entranceblockmanager.AddBlock(tilerenderer.Refinfo, tilerenderer.RotationOnConstruct, tilerenderer.TileLocation, tilerenderer.RotationOnConstruct, tilerenderer.tiletypeonconstruct);
        List<Vector2Int> footPrintHoles = tilerenderer.Refinfo.GetFootPrintHoles(tilerenderer.RotationOnConstruct);
        for (int index = 0; index < footPrintHoles.Count; ++index)
        {
          Z_GameFlags.pathfinder.UnblockTile(footPrintHoles[index].X + tilerenderer.TileLocation.X, footPrintHoles[index].Y + tilerenderer.TileLocation.Y);
          GameFlags.CollisionChanged = true;
          flag1 = true;
        }
        PrisonWalls.CheckPrisonWalls(this.BaseTileTypes);
        if (!flag1)
          return;
        Z_GameFlags.pathfinder.ForceResolvePathFinding();
      }
    }

    private void TryToShiftTopFloorToUnderFloor(int Xloc, int YLoc)
    {
      if (this.FloorTileTypes[Xloc, YLoc].UnderFloorTiletype == this.FloorTileTypes[Xloc, YLoc].tiletype)
        return;
      if (!TileData.IsThisATopFloorOnly(this.FloorTileTypes[Xloc, YLoc].tiletype))
      {
        this.FloorTileTypes[Xloc, YLoc].UnderFloorTiletype = this.FloorTileTypes[Xloc, YLoc].tiletype;
      }
      else
      {
        if (this.FloorTileTypes[Xloc, YLoc].UnderFloorTiletype != TILETYPE.None)
          return;
        this.FloorTileTypes[Xloc, YLoc].UnderFloorTiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
      }
    }

    public LayoutEntry GetThisDungeonTile(int X, int Y) => this.BaseTileTypes[X, Y];

    public void ModifyGateLocation(
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      GatePlacementManager gateplacer)
    {
      List<BaseTileDescriptor> commitedTiles = perimeterBuilder.GetCommitedTiles(cellblocktype);
      for (int index = 0; index < commitedTiles.Count; ++index)
      {
        this.BaseTileTypes[commitedTiles[index].Location.X, commitedTiles[index].Location.Y].RotationClockWise = commitedTiles[index].RotationClockWise;
        this.BaseTileTypes[commitedTiles[index].Location.X, commitedTiles[index].Location.Y].tiletype = commitedTiles[index].tiletype;
        wallsandfloors.VallidateAgainstLayout(this, JustThisTile: commitedTiles[index].Location);
      }
      this.BaseTileTypes[gateplacer.GateLocation.X, gateplacer.GateLocation.Y].RotationClockWise = gateplacer.GateDescription.RotationClockWise;
      this.BaseTileTypes[gateplacer.GateLocation.X, gateplacer.GateLocation.Y].tiletype = gateplacer.GateDescription.tiletype;
      wallsandfloors.VallidateAgainstLayout(this, JustThisTile: gateplacer.GateLocation);
    }

    public void AddNewIrregularCellBlockCellBlock(
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      GatePlacementManager gateplacer)
    {
      List<Vector2Int> vector2IntList = perimeterBuilder.GeturrentAnimalFloorSpace();
      List<BaseTileDescriptor> commitedTiles = perimeterBuilder.GetCommitedTiles(cellblocktype);
      for (int index = 0; index < commitedTiles.Count; ++index)
      {
        this.BaseTileTypes[commitedTiles[index].Location.X, commitedTiles[index].Location.Y].RotationClockWise = commitedTiles[index].RotationClockWise;
        this.BaseTileTypes[commitedTiles[index].Location.X, commitedTiles[index].Location.Y].tiletype = commitedTiles[index].tiletype;
        wallsandfloors?.VallidateAgainstLayout(this, JustThisTile: commitedTiles[index].Location, DoRemakeTileLists: false);
      }
      this.BaseTileTypes[gateplacer.GateLocation.X, gateplacer.GateLocation.Y].RotationClockWise = gateplacer.GateDescription.RotationClockWise;
      this.BaseTileTypes[gateplacer.GateLocation.X, gateplacer.GateLocation.Y].tiletype = gateplacer.GateDescription.tiletype;
      wallsandfloors.VallidateAgainstLayout(this, JustThisTile: gateplacer.GateLocation, DoRemakeTileLists: false);
      for (int index = 0; index < vector2IntList.Count; ++index)
      {
        this.FloorTileTypes[vector2IntList[index].X, vector2IntList[index].Y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Floor);
        wallsandfloors?.VallidateAgainstLayout(this, true, vector2IntList[index], false);
      }
      wallsandfloors.RemakeTileList();
    }

    public void AddNewCellBlock(
      Vector2Int TopLeftOfgamePlaySpace,
      Vector2Int WidthAndHeightOfGamePlaySpace,
      bool AddWalls,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype)
    {
      if (AddWalls)
      {
        for (int x = TopLeftOfgamePlaySpace.X; x < TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X + 1; ++x)
        {
          this.BaseTileTypes[x, TopLeftOfgamePlaySpace.Y - 1].RotationClockWise = 1;
          this.BaseTileTypes[x, TopLeftOfgamePlaySpace.Y - 1].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
          this.BaseTileTypes[x, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].RotationClockWise = 3;
          this.BaseTileTypes[x, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
        }
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, TopLeftOfgamePlaySpace.Y - 1].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, TopLeftOfgamePlaySpace.Y - 1].RotationClockWise = 0;
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, TopLeftOfgamePlaySpace.Y - 1].RotationClockWise = 1;
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, TopLeftOfgamePlaySpace.Y - 1].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
        for (int y = TopLeftOfgamePlaySpace.Y; y < TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y; ++y)
        {
          this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, y].RotationClockWise = 0;
          this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
          this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, y].RotationClockWise = 2;
          this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Wall);
        }
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X - 1, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].RotationClockWise = 3;
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].RotationClockWise = 2;
        this.BaseTileTypes[TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X, TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Corner);
      }
      for (int x = TopLeftOfgamePlaySpace.X; x < TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X; ++x)
      {
        for (int y = TopLeftOfgamePlaySpace.Y; y < TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y; ++y)
          this.BaseTileTypes[x, y].tiletype = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Floor);
      }
      if (GameFlags.pathset != null)
      {
        for (int Xloc = TopLeftOfgamePlaySpace.X - 1; Xloc < TopLeftOfgamePlaySpace.X + WidthAndHeightOfGamePlaySpace.X + 1; ++Xloc)
        {
          for (int YLoc = TopLeftOfgamePlaySpace.Y - 1; YLoc < TopLeftOfgamePlaySpace.Y + WidthAndHeightOfGamePlaySpace.Y + 1; ++YLoc)
          {
            Z_GameFlags.pathfinder.BlockTile(Xloc, YLoc);
            GameFlags.CollisionChanged = true;
          }
        }
      }
      PrisonWalls.CheckPrisonWalls(this.BaseTileTypes);
      wallsandfloors?.VallidateAgainstLayout(this);
    }

    public void AddTile(
      TILETYPE AddThis,
      Vector2Int Location,
      int RotationClockWise,
      bool IsFloorLayer = false)
    {
      if (IsFloorLayer)
      {
        if (TileData.IsSlowFloor(AddThis))
          PathFindingManager.SetFloorSpeed(Location.X, Location.Y, FloorSpeedType.SlowDirt);
        else if (TileData.IsSlowTervelator(AddThis))
          PathFindingManager.SetFloorSpeed(Location.X, Location.Y, FloorSpeedType.SlowDirt);
        else
          PathFindingManager.SetFloorSpeed(Location.X, Location.Y, FloorSpeedType.FastConcrete);
        if (RotationClockWise > 0)
        {
          if (this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype == TILETYPE.None)
            this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype = this.FloorTileTypes[Location.X, Location.Y].tiletype;
          else if (this.FloorTileTypes[Location.X, Location.Y].tiletype != AddThis)
            this.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype = this.FloorTileTypes[Location.X, Location.Y].tiletype;
        }
        this.FloorTileTypes[Location.X, Location.Y].tiletype = AddThis;
        this.FloorTileTypes[Location.X, Location.Y].RotationClockWise = RotationClockWise;
        LayoutData.infoStatic = TileData.GetTileInfo(AddThis);
        if (LayoutData.infoStatic.GetTileWidth(RotationClockWise) > 1 || LayoutData.infoStatic.GetTileHeight(RotationClockWise) > 1)
          throw new Exception("Only Written for single things loook at the code below, and pass in the layer to a function...");
      }
      else
      {
        this.BaseTileTypes[Location.X, Location.Y].tiletype = AddThis;
        this.BaseTileTypes[Location.X, Location.Y].RotationClockWise = RotationClockWise;
        LayoutData.infoStatic = TileData.GetTileInfo(AddThis);
        PathFindingManager.entranceblockmanager.AddBlock(LayoutData.infoStatic, RotationClockWise, Location, RotationClockWise, AddThis);
        if (LayoutData.infoStatic.GetTileWidth(RotationClockWise) > 1 || LayoutData.infoStatic.GetTileHeight(RotationClockWise) > 1)
        {
          int num1 = Location.X - LayoutData.infoStatic.GetXTileOrigin(RotationClockWise);
          int num2 = num1 + LayoutData.infoStatic.GetTileWidth(RotationClockWise);
          int num3 = Location.Y - LayoutData.infoStatic.GetYTileOrigin(RotationClockWise);
          int num4 = num3 + LayoutData.infoStatic.GetTileHeight(RotationClockWise);
          for (int index1 = num3; index1 < num4; ++index1)
          {
            for (int index2 = num1; index2 < num2; ++index2)
            {
              int index3 = index2;
              int index4 = index1;
              if (Location.X != index3 || Location.Y != index4)
              {
                this.BaseTileTypes[index3, index4].tiletype = AddThis;
                this.BaseTileTypes[index3, index4].SetChild(Location, AddThis);
              }
            }
          }
        }
        PrisonWalls.CheckPrisonWalls(this.BaseTileTypes);
      }
    }

    public void SaveLayoutData(Writer writer)
    {
      writer.WriteInt("t", this.BaseTileTypes.GetLength(0));
      writer.WriteInt("t", this.BaseTileTypes.GetLength(1));
      for (int index1 = 0; index1 < this.BaseTileTypes.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.BaseTileTypes.GetLength(1); ++index2)
        {
          this.BaseTileTypes[index1, index2].SaveLayoutEntry(writer, false);
          this.FloorTileTypes[index1, index2].SaveLayoutEntry(writer, true);
        }
      }
    }

    public LayoutData(Reader reader)
    {
      this.Create();
      int _out1 = 0;
      int _out2 = 0;
      int num1 = (int) reader.ReadInt("t", ref _out1);
      int num2 = (int) reader.ReadInt("t", ref _out2);
      PathFindingManager.CreateFloor(_out1, _out2);
      Vector2Int BuildLocation = new Vector2Int();
      for (int LocX = 0; LocX < _out1; ++LocX)
      {
        for (int LocY = 0; LocY < _out2; ++LocY)
        {
          this.BaseTileTypes[LocX, LocY] = new LayoutEntry(reader, false);
          this.FloorTileTypes[LocX, LocY] = new LayoutEntry(reader, true);
          int tiletype1 = (int) this.BaseTileTypes[LocX, LocY].tiletype;
          int tiletype2 = (int) this.FloorTileTypes[LocX, LocY].tiletype;
          if (!TileData.IsSlowFloor(this.FloorTileTypes[LocX, LocY].tiletype))
            PathFindingManager.SetFloorSpeed(LocX, LocY, FloorSpeedType.FastConcrete);
          if (this.BaseTileTypes[LocX, LocY].tiletype != TILETYPE.None && !this.BaseTileTypes[LocX, LocY].isChild() && (!TileData.IsThisAWaterTrough(this.BaseTileTypes[LocX, LocY].tiletype) && !EnrichmentData.IsThisAnEnrcihmentItem(this.BaseTileTypes[LocX, LocY].tiletype)))
          {
            BuildLocation.X = LocX;
            BuildLocation.Y = LocY;
            PathFindingManager.entranceblockmanager.AddBlock(TileData.GetTileInfo(this.BaseTileTypes[LocX, LocY].tiletype), this.BaseTileTypes[LocX, LocY].RotationClockWise, BuildLocation, this.BaseTileTypes[LocX, LocY].RotationClockWise, this.BaseTileTypes[LocX, LocY].tiletype);
          }
        }
      }
    }
  }
}
