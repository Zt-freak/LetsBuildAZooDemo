// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Architcture.RData.ResearchEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Architcture.RData
{
  internal class ResearchEntry
  {
    public int TotalDaysToUnlock;
    public int Row;
    public int COLUMN;
    public TILETYPE ThingToDiscover;

    public ResearchEntry(int _TotalDaysToUnlock, TILETYPE _ThingToDiscover, int _Row, int _COLUMN)
    {
      this.Row = _Row;
      this.COLUMN = _COLUMN;
      this.TotalDaysToUnlock = _TotalDaysToUnlock;
      this.ThingToDiscover = _ThingToDiscover;
    }
  }
}
