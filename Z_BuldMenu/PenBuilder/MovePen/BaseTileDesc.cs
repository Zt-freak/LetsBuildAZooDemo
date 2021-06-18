// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.MovePen.BaseTileDesc
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.PenBuilder.MovePen
{
  internal class BaseTileDesc
  {
    public int Rotation;
    public TILETYPE tiletype;
    public TILETYPE underfloortype;
    public int Variant;

    public BaseTileDesc(LayoutEntry layoutentry)
    {
      this.Variant = layoutentry.Variant;
      if (this.Variant != -1)
        throw new Exception("THIS IS NO LONGER ALLLLLOWED! - to save space on teh save");
      this.tiletype = layoutentry.tiletype;
      this.Rotation = layoutentry.RotationClockWise;
      this.underfloortype = layoutentry.UnderFloorTiletype;
    }
  }
}
