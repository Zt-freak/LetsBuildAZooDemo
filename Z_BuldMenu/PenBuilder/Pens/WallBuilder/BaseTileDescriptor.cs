// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder.BaseTileDescriptor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder
{
  internal class BaseTileDescriptor
  {
    public Vector2Int Location;
    public int RotationClockWise;
    public TILETYPE tiletype;
    private CellCornerType cellcornertype;

    public BaseTileDescriptor(Vector2Int _Location, CellCornerType _cellcornertype)
    {
      this.cellcornertype = _cellcornertype;
      this.Location = _Location;
    }

    public int TranslateRotationForGate()
    {
      switch (this.cellcornertype)
      {
        case CellCornerType.StraightLeftRight:
          return this.RotationClockWise == 1 ? 2 : 0;
        case CellCornerType.StraightUpDown:
          return this.RotationClockWise == 0 ? 1 : 3;
        default:
          return -1;
      }
    }
  }
}
