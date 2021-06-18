// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.CityMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes
{
  internal class CityMap
  {
    private CityBlock[,] CityGrid;
    private int CityHeight;
    private int BlockSize;

    public CityMap(int CityWidth, int CityHeight, int _BlockSize)
    {
      this.BlockSize = _BlockSize;
      this.CityGrid = new CityBlock[(CityWidth + this.BlockSize) / this.BlockSize, (CityHeight + this.BlockSize) / this.BlockSize];
      for (int _BlockIndex_Y = 0; _BlockIndex_Y < this.CityGrid.GetLength(1); ++_BlockIndex_Y)
      {
        for (int _BlockIndex_X = 0; _BlockIndex_X < this.CityGrid.GetLength(0); ++_BlockIndex_X)
          this.CityGrid[_BlockIndex_X, _BlockIndex_Y] = new CityBlock(this.BlockSize, this.BlockSize, new Vector2Int(_BlockIndex_X * _BlockSize, _BlockIndex_Y * _BlockSize), _BlockIndex_X, _BlockIndex_Y);
      }
    }

    public void GetPathToBuilding(TILETYPE tiletypetofind, Vector2Int Location)
    {
      int num1 = Location.X / this.BlockSize;
      int num2 = Location.Y / this.BlockSize;
    }

    public void AddNode(TILETYPE tiletype, int LocationX, int LocationY, int RotationColockWise)
    {
      if (!TileData.ThisIsAMeaningfullBuilding(tiletype))
        return;
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      int index1 = LocationX / this.BlockSize;
      int index2 = LocationY / this.BlockSize;
      if (tiletype == TILETYPE.Logo || tiletype == TILETYPE.ZooEntrance_Modern || (tiletype == TILETYPE.ZooEntrance_Deer || tiletype == TILETYPE.ZooEntrance_Cliff))
      {
        this.CityGrid[index1, index2].AddNodeOfImportance(LocationX, LocationY, tiletype, new Vector2Int(0, 0));
        this.CityGrid[index1, index2].AddNodeOfImportance(LocationX - 1, LocationY, tiletype, new Vector2Int(1, 0));
        this.CityGrid[index1, index2].AddNodeOfImportance(LocationX + 1, LocationY, tiletype, new Vector2Int(-1, 0));
      }
      else
      {
        Vector2Int purchasingLocation = tileInfo.GetPurchasingLocation(RotationColockWise);
        this.CityGrid[(LocationX + purchasingLocation.X) / this.BlockSize, (LocationY + purchasingLocation.Y) / this.BlockSize].AddNodeOfImportance(LocationX + purchasingLocation.X, LocationY + purchasingLocation.Y, tiletype, new Vector2Int(purchasingLocation.X * -1, purchasingLocation.Y * -1));
      }
    }

    public void RemoveNode(TILETYPE tiletype, Vector2Int Location, int RotationColockWise)
    {
      if (!TileData.ThisIsAMeaningfullBuilding(tiletype))
        return;
      Vector2Int purchasingLocation = TileData.GetTileInfo(tiletype).GetPurchasingLocation(RotationColockWise);
      this.CityGrid[(Location.X + purchasingLocation.X) / this.BlockSize, (Location.Y + purchasingLocation.Y) / this.BlockSize].RemoveNode(Location.X + purchasingLocation.X, Location.Y + purchasingLocation.Y);
    }

    public void UnblockTile(int Xloc, int YLoc, bool DelayRePathingQuad)
    {
      if (Xloc == 150)
        ;
      int ArrayX = Xloc / this.BlockSize;
      int ArrayY = YLoc / this.BlockSize;
      if (!this.CityGrid[Xloc / this.BlockSize, YLoc / this.BlockSize].UnblockTile(Xloc - ArrayX * this.BlockSize, YLoc - ArrayY * this.BlockSize, DelayRePathingQuad) || DelayRePathingQuad)
        return;
      this.BondEdges(ArrayX, ArrayY, Xloc, YLoc);
    }

    public void BlockTile(int Xloc, int YLoc)
    {
      int ArrayX = Xloc / this.BlockSize;
      int ArrayY = YLoc / this.BlockSize;
      if (!this.CityGrid[ArrayX, ArrayY].BlockTile(Xloc - ArrayX * this.BlockSize, YLoc - ArrayY * this.BlockSize))
        return;
      this.BondEdges(ArrayX, ArrayY, Xloc, YLoc);
    }

    public void BondEdges(int ArrayX, int ArrayY, int Xloc = -1, int YLoc = -1)
    {
      if (Xloc <= -1)
        return;
      int index1 = Xloc - ArrayX * this.BlockSize;
      int index2 = YLoc - ArrayY * this.BlockSize;
      if (index1 == 0)
      {
        if (ArrayX > 0)
          this.CityGrid[ArrayX, ArrayY].CheckCrossBoundaryLink(this.CityGrid[ArrayX, ArrayY].AllEdgeNodes[index1, index2], this.CityGrid[ArrayX - 1, ArrayY].AllEdgeNodes[this.BlockSize - 1, index2]);
      }
      else if (index1 == this.BlockSize - 1 && ArrayX < this.CityGrid.GetLength(0) - 1)
        this.CityGrid[ArrayX, ArrayY].CheckCrossBoundaryLink(this.CityGrid[ArrayX, ArrayY].AllEdgeNodes[index1, index2], this.CityGrid[ArrayX + 1, ArrayY].AllEdgeNodes[0, index2]);
      if (index2 == 0)
      {
        if (ArrayY <= 0)
          return;
        this.CityGrid[ArrayX, ArrayY].CheckCrossBoundaryLink(this.CityGrid[ArrayX, ArrayY].AllEdgeNodes[index1, index2], this.CityGrid[ArrayX, ArrayY - 1].AllEdgeNodes[index1, this.BlockSize - 1]);
      }
      else
      {
        if (index2 != this.BlockSize - 1 || ArrayY >= this.CityGrid.GetLength(1) - 1)
          return;
        this.CityGrid[ArrayX, ArrayY].CheckCrossBoundaryLink(this.CityGrid[ArrayX, ArrayY].AllEdgeNodes[index1, index2], this.CityGrid[ArrayX, ArrayY + 1].AllEdgeNodes[index1, 0]);
      }
    }

    public LocationNode GetThisNodeOrNerestPathable(Vector2Int StartLocation)
    {
      List<PathNode> internalpath = (List<PathNode>) null;
      return this.CityGrid[StartLocation.X / this.BlockSize, StartLocation.Y / this.BlockSize].GetThisNodeOrNerestPathable(StartLocation, ref internalpath);
    }

    public List<PathNode> GetFullPath(
      Vector2Int StartLocation,
      Vector2Int TargetLocation)
    {
      if (this.CityGrid[TargetLocation.X / this.BlockSize, TargetLocation.Y / this.BlockSize] == this.CityGrid[StartLocation.X / this.BlockSize, StartLocation.Y / this.BlockSize])
        return this.CityGrid[TargetLocation.X / this.BlockSize, TargetLocation.Y / this.BlockSize].GeneratePath(StartLocation, TargetLocation) ?? throw new Exception("Maybe just check this  - the path to the target means leaving the quad and going round....will it work....WHO KNOWS!");
      List<PathNode> Path1 = new List<PathNode>();
      List<PathNode> internalpath = (List<PathNode>) null;
      List<PathNode> Path2 = (List<PathNode>) null;
      LocationNode orNerestPathable = this.CityGrid[TargetLocation.X / this.BlockSize, TargetLocation.Y / this.BlockSize].GetThisNodeOrNerestPathable(TargetLocation, ref internalpath);
      if (orNerestPathable == null)
        return (List<PathNode>) null;
      int index1 = StartLocation.X / this.BlockSize;
      int index2 = StartLocation.Y / this.BlockSize;
      LocationNode locationNode = this.CityGrid[index1, index2].GetFirstNodeOnPathToHere(StartLocation, orNerestPathable, ref Path2);
      if (locationNode == orNerestPathable)
      {
        if (internalpath != null)
          throw new Exception("Need to add this to the path...");
        if (Path2 != null)
          throw new Exception("Need to add this to the path...");
      }
      if (locationNode != null && Path2 != null)
      {
        for (int index3 = 0; index3 < Path2.Count; ++index3)
          Path1.Add(new PathNode(Path2[index3].Location.X + this.CityGrid[index1, index2].TopLeft.X, Path2[index3].Location.Y + this.CityGrid[index1, index2].TopLeft.Y));
      }
      if (locationNode != null)
      {
        while (locationNode != orNerestPathable)
          locationNode = locationNode.DoPath(orNerestPathable, ref Path1);
      }
      if (internalpath != null)
      {
        for (int index3 = internalpath.Count - 1; index3 > -1; --index3)
          Path1.Add(new PathNode(internalpath[index3].Location.X + this.CityGrid[TargetLocation.X / this.BlockSize, TargetLocation.Y / this.BlockSize].TopLeft.X, internalpath[index3].Location.Y + this.CityGrid[TargetLocation.X / this.BlockSize, TargetLocation.Y / this.BlockSize].TopLeft.Y));
      }
      return Path1;
    }

    public void UpdatePathFinding()
    {
      for (int index1 = 0; index1 < this.CityGrid.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < this.CityGrid.GetLength(0); ++index2)
          this.CityGrid[index2, index1].UpdateCityGrid(this);
      }
    }

    public void DrawCityMapCollisionWithInfo(
      Vector2Int DebugLocation,
      float TileScale,
      Vector2Int DebugTargetLocation)
    {
      if (DebugTargetLocation != null && DebugLocation != null)
      {
        List<PathNode> pathNodeList = !Z_DebugFlags.DrawReverseConnections ? this.GetFullPath(DebugLocation, DebugTargetLocation) : this.GetFullPath(DebugTargetLocation, DebugLocation);
        if (pathNodeList != null)
        {
          for (int index = 0; index < pathNodeList.Count; ++index)
            this.CityGrid[0, 0].DrawCityTile(TileMath.GetTileToWorldSpace(pathNodeList[index].Location), TileScale, false);
        }
        this.CityGrid[0, 0].DrawCityTile(TileMath.GetTileToWorldSpace(DebugLocation), TileScale, true);
        this.CityGrid[0, 0].DrawCityTile(TileMath.GetTileToWorldSpace(DebugTargetLocation), TileScale, true);
      }
      else
      {
        int index1 = DebugLocation.X / this.BlockSize;
        int index2 = DebugLocation.Y / this.BlockSize;
        if (index1 > 0 && index2 > 0 && (index1 < this.CityGrid.GetLength(0) && index2 < this.CityGrid.GetLength(1)))
        {
          LocationNode node = this.CityGrid[index1, index2].GetNode(DebugLocation.X, DebugLocation.Y);
          for (int index3 = 0; index3 < this.CityGrid.GetLength(1); ++index3)
          {
            for (int index4 = 0; index4 < this.CityGrid.GetLength(0); ++index4)
              this.CityGrid[index4, index3].DrawCityMapCollisionWithInfo(DebugLocation, TileScale, node);
          }
        }
      }
      TextFunctions.DrawTextWithDropShadow("Drawing Path Nodes", 2f, new Vector2(50f, 20f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
      string stringToDraw1 = "Drawing Remote Links";
      if (Z_DebugFlags.DrawDistancesToDirectConnections)
        stringToDraw1 = "Drawing Direct Links";
      TextFunctions.DrawTextWithDropShadow(stringToDraw1, 2f, new Vector2(50f, 40f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
      string stringToDraw2 = "Target Back to root";
      if (Z_DebugFlags.DrawReverseConnections)
        stringToDraw2 = "From Root to Target";
      TextFunctions.DrawTextWithDropShadow(stringToDraw2, 2f, new Vector2(50f, 60f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
    }

    public void DrawCityMapCollision(float TileScale)
    {
      for (int index1 = 0; index1 < this.CityGrid.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < this.CityGrid.GetLength(1); ++index2)
          this.CityGrid[index2, index1].DrawMapQuad(TileScale);
      }
    }
  }
}
