// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks.EntrancesByQuad
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks
{
  internal class EntrancesByQuad
  {
    private List<Vector5Int> BlocksByLocation;
    private BlockList[,] BlockedFloors;
    private int Left;
    private int Top;
    private static Vector2Int Loc;
    private static EntranceArrow[] entrancearrow;

    public EntrancesByQuad(int _Left, int _Top)
    {
      this.Left = _Left;
      this.Top = _Top;
      this.BlocksByLocation = new List<Vector5Int>();
      this.BlockedFloors = new BlockList[TileMath.SectorSize, TileMath.SectorSize];
    }

    public void AddBlock(int XLoc, int YLoc, int ArrowRotation, TILETYPE EntryIsForThis)
    {
      int _V = 0;
      if (TileData.IsThisAShopWithShopStats(EntryIsForThis) || TileData.IsThisAnATM(EntryIsForThis))
        _V = 1;
      this.BlocksByLocation.Add(new Vector5Int(_V, (int) EntryIsForThis, XLoc, YLoc, ArrowRotation));
      if (this.BlockedFloors[XLoc - this.Left, YLoc - this.Top] == null)
        this.BlockedFloors[XLoc - this.Left, YLoc - this.Top] = new BlockList();
      if (_V <= 0)
        return;
      this.BlockedFloors[XLoc - this.Left, YLoc - this.Top].CustomerLocationsHere.Add(this.BlocksByLocation[this.BlocksByLocation.Count - 1]);
    }

    public void RemoveBlockByLocation(Vector2Int Location)
    {
      if (this.BlockedFloors[Location.X - this.Left, Location.Y - this.Top] == null)
        return;
      this.BlockedFloors[Location.X - this.Left, Location.Y - this.Top] = (BlockList) null;
      for (int index = this.BlocksByLocation.Count - 1; index > -1; --index)
      {
        if (this.BlocksByLocation[index].X == Location.X && this.BlocksByLocation[index].Y == Location.Y)
          this.BlocksByLocation.RemoveAt(index);
      }
    }

    public Vector5Int GetPointOfInterest(int XLoc, int YLoc)
    {
      int index = TinyZoo.Game1.Rnd.Next(0, this.BlockedFloors[XLoc - this.Left, YLoc - this.Top].CustomerLocationsHere.Count);
      return this.BlockedFloors[XLoc - this.Left, YLoc - this.Top].CustomerLocationsHere[index];
    }

    public bool PointOfInterst(int XLoc, int YLoc) => this.BlockedFloors[XLoc - this.Left, YLoc - this.Top] != null && this.BlockedFloors[XLoc - this.Left, YLoc - this.Top].CustomerLocationsHere.Count > 0;

    public void RemoveBlock(int XLoc, int YLoc, int ArrowRotation, TILETYPE EntryIsForThis)
    {
      for (int index = this.BlocksByLocation.Count - 1; index > -1; --index)
      {
        if (this.BlocksByLocation[index].X == XLoc && this.BlocksByLocation[index].Y == YLoc && this.BlocksByLocation[index].Z == ArrowRotation)
        {
          if (this.BlocksByLocation[index].V > 0)
            this.BlockedFloors[XLoc - this.Left, YLoc - this.Top].CustomerLocationsHere.Remove(this.BlocksByLocation[index]);
          this.BlocksByLocation.RemoveAt(index);
          break;
        }
      }
    }

    public void UpdateEntrancesByQuad()
    {
    }

    public void DrawAllBlocks()
    {
      if (EntrancesByQuad.entrancearrow == null)
      {
        EntrancesByQuad.Loc = new Vector2Int();
        EntrancesByQuad.entrancearrow = new EntranceArrow[5];
        for (int Rotation = 0; Rotation < 4; ++Rotation)
          EntrancesByQuad.entrancearrow[Rotation] = new EntranceArrow(Rotation);
        EntrancesByQuad.entrancearrow[4] = new EntranceArrow(0);
        EntrancesByQuad.entrancearrow[4].SetAsBlocked();
      }
      for (int index = 0; index < this.BlocksByLocation.Count; ++index)
      {
        EntrancesByQuad.Loc.X = this.BlocksByLocation[index].X;
        EntrancesByQuad.Loc.Y = this.BlocksByLocation[index].Y;
        if (Z_GameFlags.pathfinder.GetIsBlocked(EntrancesByQuad.Loc.X, EntrancesByQuad.Loc.Y))
        {
          EntrancesByQuad.entrancearrow[4].vLocation = TileMath.GetTileToWorldSpace(EntrancesByQuad.Loc);
          EntrancesByQuad.entrancearrow[4].DrawEntrance(AssetContainer.pointspritebatch01);
        }
        else
        {
          EntrancesByQuad.entrancearrow[this.BlocksByLocation[index].Z].vLocation = TileMath.GetTileToWorldSpace(EntrancesByQuad.Loc);
          EntrancesByQuad.entrancearrow[this.BlocksByLocation[index].Z].DrawEntrance(AssetContainer.pointspritebatch01);
        }
      }
    }
  }
}
