// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads.CityBlock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads
{
  internal class CityBlock
  {
    private Vector2Int Size;
    private PathSet pathset;
    private bool Mapped;
    public Vector2Int TopLeft;
    private static Vector2Int TempLocation;
    private static GameObject Block;
    public LocationNode[,] AllEdgeNodes;
    private LocationNode[,] ImportantNodes;
    private List<LocationNode> edgenodes;
    private List<LocationNode> NodesOfImportance;
    private int BlockIndex_X;
    private int BlockIndex_Y;
    private bool CheckAllInternalsNextFrame;

    public CityBlock(
      int Width,
      int Height,
      Vector2Int _TopLeft,
      int _BlockIndex_X,
      int _BlockIndex_Y)
    {
      this.BlockIndex_X = _BlockIndex_X;
      this.BlockIndex_Y = _BlockIndex_Y;
      CityBlock.TempLocation = new Vector2Int();
      this.NodesOfImportance = new List<LocationNode>();
      this.edgenodes = new List<LocationNode>();
      this.pathset = new PathSet();
      this.pathset.CreateGrid(Width, Height);
      this.pathset.BlockAllTiles();
      this.Size = new Vector2Int(Width, Height);
      this.TopLeft = new Vector2Int(_TopLeft);
      this.AllEdgeNodes = new LocationNode[Width, Height];
      this.ImportantNodes = new LocationNode[Width, Height];
      for (int index = 0; index < Width; ++index)
      {
        this.AllEdgeNodes[index, 0] = new LocationNode(index + this.TopLeft.X, this.TopLeft.Y, true, this.TopLeft, (Vector2Int) null);
        this.AllEdgeNodes[index, Height - 1] = new LocationNode(index + this.TopLeft.X, Height - 1 + this.TopLeft.Y, true, this.TopLeft, (Vector2Int) null);
      }
      for (int index = 1; index < Height - 1; ++index)
      {
        this.AllEdgeNodes[0, index] = new LocationNode(this.TopLeft.X, index + this.TopLeft.Y, true, this.TopLeft, (Vector2Int) null);
        this.AllEdgeNodes[Width - 1, index] = new LocationNode(Width - 1 + this.TopLeft.X, index + this.TopLeft.Y, true, this.TopLeft, (Vector2Int) null);
      }
    }

    public bool UnblockTile(int XLoc, int YLoc, bool DelayRePathingQuad)
    {
      if (XLoc == 174)
        ;
      if (this.pathset.GetIsBlocked(XLoc, YLoc))
      {
        this.pathset.UnblockTile(XLoc, YLoc);
        if (this.AllEdgeNodes[XLoc, YLoc] != null && this.AllEdgeNodes[XLoc, YLoc].Blocked)
        {
          this.AllEdgeNodes[XLoc, YLoc].Blocked = false;
          if (this.edgenodes.Contains(this.AllEdgeNodes[XLoc, YLoc]))
            throw new Exception("A blocked tile was in the list....");
          this.edgenodes.Add(this.AllEdgeNodes[XLoc, YLoc]);
          if (DelayRePathingQuad)
          {
            this.CheckAllInternalsNextFrame = true;
            this.AllEdgeNodes[XLoc, YLoc].Repath = true;
          }
          else
            this.ConstructInternalPaths(this.AllEdgeNodes[XLoc, YLoc]);
          return true;
        }
      }
      return false;
    }

    public bool BlockTile(int XLoc, int YLoc)
    {
      if (!this.pathset.GetIsBlocked(XLoc, YLoc))
      {
        this.pathset.BlockTile(XLoc, YLoc);
        if (this.AllEdgeNodes[XLoc, YLoc] != null && !this.AllEdgeNodes[XLoc, YLoc].Blocked)
        {
          this.AllEdgeNodes[XLoc, YLoc].Blocked = true;
          this.edgenodes.Remove(this.AllEdgeNodes[XLoc, YLoc]);
          return true;
        }
      }
      return false;
    }

    public void UpdateCityGrid(CityMap citymap)
    {
      if (!this.CheckAllInternalsNextFrame)
        return;
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        if (this.edgenodes[index].Repath)
        {
          if (this.edgenodes[index].Location.X == 170)
          {
            int y = this.edgenodes[index].Location.Y;
          }
          this.edgenodes[index].Repath = false;
          this.ConstructInternalPaths(this.edgenodes[index]);
          citymap.BondEdges(this.BlockIndex_X, this.BlockIndex_Y, this.edgenodes[index].Location.X, this.edgenodes[index].Location.Y);
        }
      }
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        if (this.NodesOfImportance[index].Repath)
        {
          this.NodesOfImportance[index].Repath = false;
          this.ConstructInternalPaths(this.NodesOfImportance[index]);
        }
      }
      this.CheckAllInternalsNextFrame = false;
    }

    public void BondExternalEdgeNodes(
      CityBlock otherquad,
      DirectionPressed ThisQuadIsToThisSideOfOtherQuad)
    {
      List<PathNode> pathNodeList = new List<PathNode>();
      switch (ThisQuadIsToThisSideOfOtherQuad)
      {
        case DirectionPressed.Up:
          for (int index = 0; index < this.Size.X; ++index)
            this.CheckCrossBoundaryLink(this.AllEdgeNodes[index, this.Size.Y - 1], this.AllEdgeNodes[index, 0]);
          break;
        case DirectionPressed.Right:
          for (int index = 0; index < this.Size.Y; ++index)
            this.CheckCrossBoundaryLink(this.AllEdgeNodes[0, index], otherquad.AllEdgeNodes[this.Size.X - 1, index]);
          break;
        case DirectionPressed.Down:
          for (int index = 0; index < this.Size.X; ++index)
            this.CheckCrossBoundaryLink(this.AllEdgeNodes[index, 0], this.AllEdgeNodes[index, this.Size.Y - 1]);
          break;
        case DirectionPressed.Left:
          for (int index = 0; index < this.Size.Y; ++index)
            this.CheckCrossBoundaryLink(this.AllEdgeNodes[this.Size.X - 1, index], otherquad.AllEdgeNodes[0, index]);
          break;
      }
    }

    public void CheckCrossBoundaryLink(LocationNode ThisNode, LocationNode OtherNode)
    {
      if (!ThisNode.Blocked && !OtherNode.Blocked)
      {
        List<PathNode> path = new List<PathNode>();
        path.Add(new PathNode(ThisNode.Location.X, ThisNode.Location.Y));
        if (!OtherNode.CreateDirectLinkPair(ThisNode, path))
          return;
        ThisNode.CheckCreateRemoteLinksOnDirectCreate(OtherNode, OtherNode, path.Count);
        OtherNode.CheckCreateRemoteLinksOnDirectCreate(ThisNode, ThisNode, path.Count);
      }
      else
      {
        ThisNode.BreakDirectLink(OtherNode);
        OtherNode.BreakDirectLink(ThisNode);
      }
    }

    private void ReMapThisTile()
    {
      for (int Xloc = 0; Xloc < this.Size.X; ++Xloc)
      {
        for (int YLoc = 0; YLoc < this.Size.Y; ++YLoc)
          this.pathset.GetIsBlocked(Xloc, YLoc);
      }
    }

    public void AddNodeOfImportance(
      int LocationX,
      int LocationY,
      TILETYPE tiletype,
      Vector2Int OffsetToChild)
    {
      CityBlock.TempLocation.X = LocationX;
      CityBlock.TempLocation.Y = LocationY;
      this.ImportantNodes[LocationX - this.TopLeft.X, LocationY - this.TopLeft.Y] = new LocationNode(LocationX, LocationY, false, this.TopLeft, OffsetToChild);
      this.ImportantNodes[LocationX - this.TopLeft.X, LocationY - this.TopLeft.Y].tiletype = tiletype;
      this.ConstructInternalPaths(this.ImportantNodes[LocationX - this.TopLeft.X, LocationY - this.TopLeft.Y]);
      this.NodesOfImportance.Add(this.ImportantNodes[LocationX - this.TopLeft.X, LocationY - this.TopLeft.Y]);
    }

    public void ConstructInternalPaths(LocationNode ThisNode)
    {
      if (ThisNode.Location.X == 162)
      {
        int y1 = ThisNode.Location.Y;
      }
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        if (this.NodesOfImportance[index].Location.X == 162)
        {
          int y2 = this.NodesOfImportance[index].Location.Y;
        }
        if (this.NodesOfImportance[index] != ThisNode)
        {
          List<PathNode> fullPathToLocation = this.pathset.GetFullPathToLocation(ThisNode.QuadRelativeLocation, this.NodesOfImportance[index].QuadRelativeLocation, false);
          if (fullPathToLocation != null && ThisNode.CreateDirectLinkPair(this.NodesOfImportance[index], fullPathToLocation, this.TopLeft) && ThisNode.Location.X == 159)
          {
            int y3 = ThisNode.Location.Y;
          }
        }
      }
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        if (this.edgenodes.Count > 1 && this.edgenodes[index].QuadRelativeLocation.X == 0)
        {
          int y2 = this.edgenodes[index].QuadRelativeLocation.Y;
        }
        if (this.edgenodes[index] != ThisNode)
        {
          List<PathNode> fullPathToLocation = this.pathset.GetFullPathToLocation(ThisNode.QuadRelativeLocation, this.edgenodes[index].QuadRelativeLocation, false);
          if (fullPathToLocation != null)
          {
            if (ThisNode.Location.X == 159)
            {
              int y3 = ThisNode.Location.Y;
            }
            if (ThisNode.CreateDirectLinkPair(this.edgenodes[index], fullPathToLocation, this.TopLeft))
            {
              ThisNode.CheckCreateRemoteLinksOnDirectCreate(this.edgenodes[index], this.edgenodes[index], fullPathToLocation.Count);
              this.edgenodes[index].CheckCreateRemoteLinksOnDirectCreate(ThisNode, ThisNode, fullPathToLocation.Count);
            }
            if (!ThisNode.CanGetToThisDirectLinkFromHere(this.edgenodes[index]) || !this.edgenodes[index].CanGetToThisDirectLinkFromHere(ThisNode))
              throw new Exception("NO PATH");
            if (ThisNode.GetDistanceToHere(this.edgenodes[index]) < 0 || this.edgenodes[index].GetDistanceToHere(ThisNode) < 0)
              throw new Exception("NO PATH");
          }
        }
      }
    }

    public LocationNode GetNode(int XLoc, int YLoc)
    {
      if (this.ImportantNodes[XLoc - this.TopLeft.X, YLoc - this.TopLeft.Y] != null)
        return this.ImportantNodes[XLoc - this.TopLeft.X, YLoc - this.TopLeft.Y];
      return this.AllEdgeNodes[XLoc - this.TopLeft.X, YLoc - this.TopLeft.Y] != null ? this.AllEdgeNodes[XLoc - this.TopLeft.X, YLoc - this.TopLeft.Y] : (LocationNode) null;
    }

    public List<PathNode> GeneratePath(Vector2Int StartLocation, Vector2Int EndLocation)
    {
      List<PathNode> fullPathToLocation = this.pathset.GetFullPathToLocation(StartLocation - this.TopLeft, EndLocation - this.TopLeft, false);
      for (int index = 0; index < fullPathToLocation.Count; ++index)
        fullPathToLocation[index] = new PathNode(fullPathToLocation[index].XLoc + this.TopLeft.X, fullPathToLocation[index].YLoc + this.TopLeft.Y);
      return fullPathToLocation;
    }

    public LocationNode GetThisNodeOrNerestPathable(
      Vector2Int StartLocation,
      ref List<PathNode> internalpath)
    {
      if (this.ImportantNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y] != null)
        return this.ImportantNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y];
      if (this.AllEdgeNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y] != null)
        return this.AllEdgeNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y];
      Vector2Int StartPoint = new Vector2Int(StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y);
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        internalpath = this.pathset.GetFullPathToLocation(StartPoint, this.NodesOfImportance[index].QuadRelativeLocation, false);
        if (internalpath != null)
          return this.NodesOfImportance[index];
      }
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        internalpath = this.pathset.GetFullPathToLocation(StartPoint, this.edgenodes[index].QuadRelativeLocation, false);
        if (internalpath != null)
          return this.edgenodes[index];
      }
      return (LocationNode) null;
    }

    public LocationNode GetFirstNodeOnPathToHere(
      Vector2Int StartLocation,
      LocationNode TargetLocation,
      ref List<PathNode> Path)
    {
      if (this.ImportantNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y] != null && this.ImportantNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y].CanGetToThisRemoteLinkFromHere(TargetLocation))
        return this.ImportantNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y];
      if (this.AllEdgeNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y] != null && this.AllEdgeNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y].CanGetToThisRemoteLinkFromHere(TargetLocation))
        return this.AllEdgeNodes[StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y];
      Vector2Int StartPoint = new Vector2Int(StartLocation.X - this.TopLeft.X, StartLocation.Y - this.TopLeft.Y);
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        if (this.NodesOfImportance[index].CanGetToThisRemoteLinkFromHere(TargetLocation))
        {
          Path = this.pathset.GetFullPathToLocation(StartPoint, this.NodesOfImportance[index].QuadRelativeLocation, false);
          if (Path != null)
            return this.NodesOfImportance[index];
        }
      }
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        if (this.edgenodes[index].CanGetToThisRemoteLinkFromHere(TargetLocation))
        {
          Path = this.pathset.GetFullPathToLocation(StartPoint, this.edgenodes[index].QuadRelativeLocation, false);
          if (Path != null)
            return this.edgenodes[index];
        }
      }
      return (LocationNode) null;
    }

    public void RemoveNode(int Xlox, int YLoc)
    {
      this.NodesOfImportance.Remove(this.ImportantNodes[Xlox - this.TopLeft.X, YLoc - this.TopLeft.Y]);
      this.ImportantNodes[Xlox - this.TopLeft.X, YLoc - this.TopLeft.Y] = (LocationNode) null;
    }

    public void DrawCityTile(Vector2 WorldLoc, float TileScale, bool IsRoot)
    {
      if (CityBlock.Block == null)
      {
        CityBlock.Block = new GameObject();
        CityBlock.Block.DrawRect = TinyZoo.Game1.WhitePixelRect;
        CityBlock.Block.SetDrawOriginToCentre();
        CityBlock.Block.scale = TileScale;
        CityBlock.Block.fAlpha = 0.4f;
      }
      CityBlock.Block.SetAllColours(0.2f, 1f, 0.0f);
      CityBlock.Block.fAlpha = 0.4f;
      if (IsRoot)
      {
        CityBlock.Block.fAlpha = FlashingAlpha.Fast.fAlpha;
        CityBlock.Block.SetAllColours(0.0f, 1f, 1f);
      }
      CityBlock.Block.vLocation = WorldLoc;
      CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
    }

    public void DrawCityMapCollisionWithInfo(
      Vector2Int DebugLocation,
      float TileScale,
      LocationNode locationnode)
    {
      if (CityBlock.Block == null)
      {
        CityBlock.Block = new GameObject();
        CityBlock.Block.DrawRect = TinyZoo.Game1.WhitePixelRect;
        CityBlock.Block.SetDrawOriginToCentre();
        CityBlock.Block.scale = TileScale;
        CityBlock.Block.fAlpha = 0.4f;
      }
      if (locationnode != null)
      {
        CityBlock.Block.fAlpha = FlashingAlpha.Fast.fAlpha;
        CityBlock.Block.SetAllColours(0.0f, 1f, 1f);
        CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(locationnode.Location.X, locationnode.Location.Y));
        CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      }
      CityBlock.Block.fAlpha = 0.4f;
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        bool flag = false;
        if (this.edgenodes[index].CanGetToThisDirectLinkFromHere(locationnode) && Z_DebugFlags.DrawDistancesToDirectConnections)
        {
          flag = true;
          CityBlock.Block.fAlpha = FlashingAlpha.Slow.fAlpha;
          CityBlock.Block.SetAllColours(1f, 0.0f, 1f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.edgenodes[index].Location.X, this.edgenodes[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          if (Z_DebugFlags.DrawReverseConnections)
            this.DrawNumber(this.edgenodes[index].GetDistanceToHereASSTRING(locationnode), CityBlock.Block.vLocation);
          else
            this.DrawNumber(locationnode.GetDistanceToHereASSTRING(this.edgenodes[index]), CityBlock.Block.vLocation);
        }
        if (this.edgenodes[index].CanGetToThisRemoteLinkFromHere(locationnode) && !Z_DebugFlags.DrawDistancesToDirectConnections)
        {
          flag = true;
          CityBlock.Block.fAlpha = 0.4f;
          CityBlock.Block.SetAllColours(0.0f, 1f, 0.0f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.edgenodes[index].Location.X, this.edgenodes[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          if (Z_DebugFlags.DrawReverseConnections)
            this.DrawNumber(this.edgenodes[index].GetDistanceToHereASSTRING(locationnode), CityBlock.Block.vLocation);
          else
            this.DrawNumber(locationnode.GetDistanceToHereASSTRING(this.edgenodes[index]), CityBlock.Block.vLocation);
        }
        if (!flag)
        {
          CityBlock.Block.fAlpha = 0.2f;
          CityBlock.Block.SetAllColours(0.0f, 0.0f, 0.0f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.edgenodes[index].Location.X, this.edgenodes[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
        }
      }
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        bool flag = false;
        if (this.NodesOfImportance[index].CanGetToThisDirectLinkFromHere(locationnode) && Z_DebugFlags.DrawDistancesToDirectConnections)
        {
          flag = true;
          CityBlock.Block.fAlpha = FlashingAlpha.Slow.fAlpha;
          CityBlock.Block.SetAllColours(1f, 0.0f, 1f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.NodesOfImportance[index].Location.X, this.NodesOfImportance[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          if (Z_DebugFlags.DrawReverseConnections)
            this.DrawNumber(this.NodesOfImportance[index].GetDistanceToHereASSTRING(locationnode), CityBlock.Block.vLocation);
          else
            this.DrawNumber(locationnode.GetDistanceToHereASSTRING(this.NodesOfImportance[index]), CityBlock.Block.vLocation);
        }
        if (this.NodesOfImportance[index].CanGetToThisRemoteLinkFromHere(locationnode) && !Z_DebugFlags.DrawDistancesToDirectConnections)
        {
          flag = true;
          CityBlock.Block.fAlpha = 0.4f;
          CityBlock.Block.SetAllColours(0.0f, 1f, 0.0f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.NodesOfImportance[index].Location.X, this.NodesOfImportance[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
          if (Z_DebugFlags.DrawReverseConnections)
            this.DrawNumber(this.NodesOfImportance[index].GetDistanceToHereASSTRING(locationnode), CityBlock.Block.vLocation);
          else
            this.DrawNumber(locationnode.GetDistanceToHereASSTRING(this.NodesOfImportance[index]), CityBlock.Block.vLocation);
        }
        if (!flag)
        {
          CityBlock.Block.fAlpha = 0.2f;
          CityBlock.Block.SetAllColours(0.0f, 0.0f, 0.0f);
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.NodesOfImportance[index].Location.X, this.NodesOfImportance[index].Location.Y));
          CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
        }
      }
    }

    private void DrawNumber(string Number, Vector2 WorldLoc)
    {
      WorldLoc = RenderMath.TranslateWorldSpaceToScreenSpace(WorldLoc);
      TextFunctions.DrawJustifiedText(Number, 1f * Sengine.WorldOriginandScale.Z, WorldLoc, Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, true);
    }

    public void DrawMapQuad(float TileScale)
    {
      if (CityBlock.Block == null)
      {
        CityBlock.Block = new GameObject();
        CityBlock.Block.DrawRect = TinyZoo.Game1.WhitePixelRect;
        CityBlock.Block.SetDrawOriginToCentre();
        CityBlock.Block.scale = TileScale;
        CityBlock.Block.fAlpha = 0.4f;
      }
      for (int index1 = 0; index1 < this.pathset.nodesasGrid.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.pathset.nodesasGrid.GetLength(1); ++index2)
        {
          CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.pathset.nodesasGrid[index1, index2].XLoc + this.TopLeft.X, this.pathset.nodesasGrid[index1, index2].YLoc + this.TopLeft.Y));
          if (this.pathset.nodesasGrid[index1, index2].IsBlocking)
            CityBlock.Block.SetAllColours(1f, 0.0f, 0.0f);
          else
            CityBlock.Block.SetAllColours(0.0f, 1f, 0.0f);
        }
      }
      CityBlock.Block.SetAllColours(1f, 1f, 0.0f);
      for (int index1 = 0; index1 < this.Size.X; ++index1)
      {
        for (int index2 = 0; index2 < this.Size.Y; ++index2)
        {
          LocationNode allEdgeNode = this.AllEdgeNodes[index1, index2];
        }
      }
      for (int index = 0; index < this.edgenodes.Count; ++index)
      {
        CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.edgenodes[index].Location.X, this.edgenodes[index].Location.Y));
        CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      }
      for (int index = 0; index < this.NodesOfImportance.Count; ++index)
      {
        CityBlock.Block.SetAllColours(1f, 0.0f, 1f);
        CityBlock.Block.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(this.NodesOfImportance[index].Location.X, this.NodesOfImportance[index].Location.Y));
        CityBlock.Block.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      }
    }
  }
}
