// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks.EntranceBlockManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks
{
  internal class EntranceBlockManager
  {
    private EntrancesByQuad[,] entrancesbyquad;

    public EntranceBlockManager(int XSize, int YSize)
    {
      this.entrancesbyquad = new EntrancesByQuad[(int) Math.Ceiling((double) XSize / (double) TileMath.SectorSize), (int) Math.Ceiling((double) YSize / (double) TileMath.SectorSize)];
      for (int index1 = 0; index1 < this.entrancesbyquad.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.entrancesbyquad.GetLength(1); ++index2)
          this.entrancesbyquad[index1, index2] = new EntrancesByQuad(index1 * TileMath.SectorSize, index2 * TileMath.SectorSize);
      }
    }

    public void RemoveBlockByLocation(Vector2Int Location) => this.entrancesbyquad[Location.X / TileMath.SectorSize, Location.Y / TileMath.SectorSize].RemoveBlockByLocation(Location);

    public Vector5Int GetPointOfInterest(Vector2Int Location) => this.entrancesbyquad[Location.X / TileMath.SectorSize, Location.Y / TileMath.SectorSize].GetPointOfInterest(Location.X, Location.Y);

    public bool PointOfInterst(Vector2Int Location) => this.entrancesbyquad[Location.X / TileMath.SectorSize, Location.Y / TileMath.SectorSize].PointOfInterst(Location.X, Location.Y);

    public void AddBlock(
      TileInfo Refinfo,
      int RotationOnConstruct,
      Vector2Int BuildLocation,
      int RotationOfArrow,
      TILETYPE EntryIsForThis)
    {
      List<Vector2Int> entrances = Refinfo.GetEntrances(RotationOnConstruct);
      if (entrances == null)
        return;
      for (int index = 0; index < entrances.Count; ++index)
        this.AddBlock(entrances[index].X + BuildLocation.X, entrances[index].Y + BuildLocation.Y, RotationOfArrow, EntryIsForThis);
    }

    public void AddBlock(int XLoc, int YLoc, int RotationOfArrow, TILETYPE EntryIsForThis) => this.entrancesbyquad[XLoc / TileMath.SectorSize, YLoc / TileMath.SectorSize].AddBlock(XLoc, YLoc, RotationOfArrow, EntryIsForThis);

    public void RemoveBlock(
      TileInfo Refinfo,
      int RotationOnConstruct,
      Vector2Int BuildLocation,
      int RotationOfArrow,
      TILETYPE RemovedThis)
    {
      List<Vector2Int> entrances = Refinfo.GetEntrances(RotationOnConstruct);
      if (entrances == null)
        return;
      for (int index = 0; index < entrances.Count; ++index)
        this.RemoveBlock(entrances[index].X + BuildLocation.X, entrances[index].Y + BuildLocation.Y, RotationOfArrow, RemovedThis);
    }

    public void RemoveBlock(int XLoc, int YLoc, int RotationForArrow, TILETYPE RemovedThis) => this.entrancesbyquad[XLoc / TileMath.SectorSize, YLoc / TileMath.SectorSize].RemoveBlock(XLoc, YLoc, RotationForArrow, RemovedThis);

    public void DrawAllBlocks()
    {
      for (int index1 = 0; index1 < this.entrancesbyquad.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.entrancesbyquad.GetLength(1); ++index2)
          this.entrancesbyquad[index1, index2].DrawAllBlocks();
      }
    }
  }
}
