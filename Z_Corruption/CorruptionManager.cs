// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Corruption.CorruptionManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.Z_Corruption
{
  internal class CorruptionManager
  {
    internal static void SwitchTileToCorruption(Vector2Int Location, Player player)
    {
      if (player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y] != null && player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype != TILETYPE.None)
      {
        switch (CategoryData.GetTileTypeToCetagory(player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype, out POINT_OF_INTEREST _))
        {
          case CATEGORYTYPE.Enclosure:
          case CATEGORYTYPE.Nature:
          case CATEGORYTYPE.Decorative:
            player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype = CorruptionData.GetTileToCorruption(player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype, false);
            OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: Location, DoRemakeTileLists: false);
            break;
        }
      }
      if (player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y] != null && player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype != TILETYPE.None)
      {
        player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype = CorruptionData.GetTileToCorruption(player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype, true);
        OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, Location, false);
      }
      if (player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y] == null || player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype == TILETYPE.None)
        return;
      player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype = CorruptionData.GetTileToCorruption(player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].UnderFloorTiletype, true);
      OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, Location, false);
    }

    internal static void SwitchMapToCorruption(Player player)
    {
      Vector2Int Location = new Vector2Int(0, 0);
      for (int index1 = 0; index1 < TileMath.GetOverWorldMapSize_XDefault(); ++index1)
      {
        for (int index2 = 0; index2 < TileMath.GetOverWorldMapSize_YSize(); ++index2)
        {
          Location.X = index1;
          Location.Y = index2;
          CorruptionManager.SwitchTileToCorruption(Location, player);
        }
      }
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }
  }
}
