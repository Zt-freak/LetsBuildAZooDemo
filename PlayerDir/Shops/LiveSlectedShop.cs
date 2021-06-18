// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.LiveSlectedShop
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Shops
{
  internal class LiveSlectedShop
  {
    public Vector2Int Location;
    public TILETYPE tiletype;

    public LiveSlectedShop(Vector2Int _Location, TILETYPE _tile)
    {
      this.Location = new Vector2Int(_Location);
      this.tiletype = _tile;
    }
  }
}
