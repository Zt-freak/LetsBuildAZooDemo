// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table.FinanceTableRowTypeContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table
{
  internal class FinanceTableRowTypeContainer
  {
    public FinanceTableRowType rowType;
    public TILETYPE tileType;

    public FinanceTableRowTypeContainer(FinanceTableRowType _rowType, TILETYPE _tileType = TILETYPE.Count)
    {
      this.rowType = _rowType;
      this.tileType = _tileType;
    }
  }
}
