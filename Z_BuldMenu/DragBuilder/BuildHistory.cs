// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DragBuilder.BuildHistory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.DragBuilder
{
  internal class BuildHistory
  {
    public Vector2Int TileLocation;
    public TILETYPE tiletypeonconstruct;
    private int XOrigin;
    private int YOrigin;
    private int XWidth;
    private int YHeight;
    public TILETYPE OGTileType;
    public TILETYPE OGUnderFloor;
    public int OG_rotation;

    public BuildHistory(
      Vector2Int _TileLocation,
      TILETYPE _tiletypeonconstruct,
      TileRenderer tilerenderer,
      Player player)
    {
      this.OGTileType = player.prisonlayout.layout.FloorTileTypes[_TileLocation.X, _TileLocation.Y].tiletype;
      this.OGUnderFloor = player.prisonlayout.layout.FloorTileTypes[_TileLocation.X, _TileLocation.Y].UnderFloorTiletype;
      this.OG_rotation = player.prisonlayout.layout.FloorTileTypes[_TileLocation.X, _TileLocation.Y].RotationClockWise;
      this.XOrigin = tilerenderer.XOrigin;
      this.YOrigin = tilerenderer.YOrigin;
      this.XWidth = tilerenderer.XWidth;
      this.YHeight = tilerenderer.YHeight;
      this.tiletypeonconstruct = _tiletypeonconstruct;
      this.TileLocation = _TileLocation;
    }

    public bool SmartOverlaps(Vector2Int Location, TileRenderer tilerenderer)
    {
      int xorigin = tilerenderer.XOrigin;
      int yorigin = tilerenderer.YOrigin;
      int xwidth = tilerenderer.XWidth;
      int yheight = tilerenderer.YHeight;
      if (xwidth == 1 && yheight == 1 && (xorigin == 0 && yorigin == 0))
        return this.Overlaps(Location);
      Vector2Int Location1 = new Vector2Int();
      for (int index1 = Location.X - xorigin; index1 < Location.X - xorigin + xwidth; ++index1)
      {
        for (int index2 = Location.Y - yorigin; index2 < Location.Y - yorigin + yheight; ++index2)
        {
          Location1.X = index1;
          Location1.Y = index2;
          if (this.Overlaps(Location1))
            return true;
        }
      }
      return false;
    }

    public bool Overlaps(Vector2Int Location)
    {
      for (int index1 = this.TileLocation.X - this.XOrigin; index1 < this.TileLocation.X - this.XOrigin + this.XWidth; ++index1)
      {
        for (int index2 = this.TileLocation.Y - this.YOrigin; index2 < this.TileLocation.Y - this.YOrigin + this.YHeight; ++index2)
        {
          if (index1 == Location.X && index2 == Location.Y)
            return true;
        }
      }
      return false;
    }
  }
}
