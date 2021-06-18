// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items.PenItem
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items
{
  internal class PenItem
  {
    public TILETYPE tiletype;
    public Vector2Int Location;
    public int Rotation;
    public int WaterUnits;
    public ENRICHMENTCLASS enrichmentclass;
    public bool IsSelectedInEditView;
    private List<Vector2Int> TEMP_blockedLocations;

    public PenItem(TILETYPE _tiletype, Vector2Int _Location, int _Rotation)
    {
      this.Rotation = _Rotation;
      this.Location = new Vector2Int(_Location);
      this.tiletype = _tiletype;
      this.enrichmentclass = EnrichmentData.GetTILETYPEToEnrichmentClass(_tiletype);
      this.SetWaterUnits();
    }

    private void SetWaterUnits() => this.WaterUnits = TileData.GetTileInfo(this.tiletype).GetTileWidth(0) * TileData.GetTileInfo(this.tiletype).GetTileHeight(0);

    public bool IsBlockingThisWorldSpace(int XLoc, int YLoc, bool GetCollisionBlockingOnly)
    {
      if (GetCollisionBlockingOnly)
      {
        switch (EnrichmentData.GetTILETYPEToEnrichmentClass(this.tiletype))
        {
          case ENRICHMENTCLASS.Trampoline:
          case ENRICHMENTCLASS.RockPerch:
          case ENRICHMENTCLASS.HighWoodBeamPerch:
          case ENRICHMENTCLASS.RockCliff_Perch:
            break;
          default:
            return false;
        }
      }
      if (this.TEMP_blockedLocations == null)
        this.CreateBlocks();
      for (int index = 0; index < this.TEMP_blockedLocations.Count; ++index)
      {
        if (this.TEMP_blockedLocations[index].X == XLoc && this.TEMP_blockedLocations[index].Y == YLoc)
          return true;
      }
      return false;
    }

    private void CreateBlocks()
    {
      this.TEMP_blockedLocations = new List<Vector2Int>();
      TileInfo tileInfo = TileData.GetTileInfo(this.tiletype);
      for (int _X = this.Location.X - tileInfo.GetXTileOrigin(this.Rotation); _X < this.Location.X - tileInfo.GetXTileOrigin(this.Rotation) + tileInfo.GetTileWidth(this.Rotation); ++_X)
      {
        for (int _Y = this.Location.Y - tileInfo.GetYTileOrigin(this.Rotation); _Y < this.Location.Y - tileInfo.GetYTileOrigin(this.Rotation) + tileInfo.GetTileHeight(this.Rotation); ++_Y)
          this.TEMP_blockedLocations.Add(new Vector2Int(_X, _Y));
      }
    }

    public void SavePenItem(Writer writer)
    {
      writer.WriteInt("c", this.Rotation);
      this.Location.SaveVector2Int(writer);
      writer.WriteInt("c", (int) this.tiletype);
    }

    public PenItem(Reader reader)
    {
      int num1 = (int) reader.ReadInt("c", ref this.Rotation);
      this.Location = new Vector2Int(reader);
      int _out = 0;
      int num2 = (int) reader.ReadInt("c", ref _out);
      this.tiletype = (TILETYPE) _out;
      this.enrichmentclass = EnrichmentData.GetTILETYPEToEnrichmentClass(this.tiletype);
      this.SetWaterUnits();
    }
  }
}
