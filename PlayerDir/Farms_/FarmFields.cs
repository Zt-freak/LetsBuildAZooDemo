// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Farms_.FarmFields
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_HeatMaps;

namespace TinyZoo.PlayerDir.Farms_
{
  internal class FarmFields
  {
    public List<PrisonZone> farmfields;

    public FarmFields() => this.farmfields = new List<PrisonZone>();

    public void AddNewIrregularCellBlock(
      PerimeterBuilder perimeterBuilder,
      int CELLNUMBER,
      CellBlockType cellblocktype,
      GatePlacementManager gateplacer,
      bool AddToCollisionCheckList = false)
    {
      this.farmfields.Add(new PrisonZone(perimeterBuilder, CELLNUMBER, cellblocktype, gateplacer, AddToCollisionCheckList, true));
    }

    public void SellFarmField(int CellUID, Player player)
    {
      for (int index = this.farmfields.Count - 1; index > -1; --index)
      {
        if (this.farmfields[index].Cell_UID == CellUID)
        {
          this.farmfields.RemoveAt(index);
          Enclosure_Farm_Map.MustRecreateMap = true;
        }
      }
    }

    public FarmFields(Reader reader, int VersionNumberForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("f", ref _out);
      this.farmfields = new List<PrisonZone>();
      for (int index = 0; index < _out; ++index)
        this.farmfields.Add(new PrisonZone(reader, VersionNumberForLoad, true));
    }

    public void SaveFarmFields(Writer writer)
    {
      writer.WriteInt("f", this.farmfields.Count);
      for (int index = 0; index < this.farmfields.Count; ++index)
        this.farmfields[index].SavePrisonZone(writer);
    }
  }
}
