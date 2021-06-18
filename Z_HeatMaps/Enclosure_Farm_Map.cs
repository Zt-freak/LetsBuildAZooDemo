// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.Enclosure_Farm_Map
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

namespace TinyZoo.Z_HeatMaps
{
  internal class Enclosure_Farm_Map
  {
    private static int[,] EnclosureMap;
    internal static bool MustRecreateMap = true;

    internal static int GetCell(int locationX, int LocationY, Player player)
    {
      if (Enclosure_Farm_Map.MustRecreateMap)
        Enclosure_Farm_Map.RecreateEnclosure_Farm_Map(player);
      return locationX >= 0 && locationX < Enclosure_Farm_Map.EnclosureMap.GetLength(0) && (LocationY >= 0 && LocationY < Enclosure_Farm_Map.EnclosureMap.GetLength(1)) ? Enclosure_Farm_Map.EnclosureMap[locationX, LocationY] : 0;
    }

    internal static void RecreateEnclosure_Farm_Map(Player player)
    {
      Enclosure_Farm_Map.MustRecreateMap = false;
      Enclosure_Farm_Map.EnclosureMap = new int[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        if (player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID == 0)
          throw new Exception("NOT SUPPORTED");
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles.Count; ++index2)
          Enclosure_Farm_Map.EnclosureMap[player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles[index2].X, player.prisonlayout.cellblockcontainer.prisonzones[index1].FenceTiles[index2].Y] = player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID;
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles.Count; ++index2)
          Enclosure_Farm_Map.EnclosureMap[player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].X, player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].Y] = player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID;
      }
      for (int index1 = 0; index1 < player.farms.farmfields.farmfields.Count; ++index1)
      {
        if (player.farms.farmfields.farmfields[index1].Cell_UID == 0)
          throw new Exception("NOT SUPPORTED");
        for (int index2 = 0; index2 < player.farms.farmfields.farmfields[index1].FenceTiles.Count; ++index2)
          Enclosure_Farm_Map.EnclosureMap[player.farms.farmfields.farmfields[index1].FenceTiles[index2].X, player.farms.farmfields.farmfields[index1].FenceTiles[index2].Y] = -player.farms.farmfields.farmfields[index1].Cell_UID;
        for (int index2 = 0; index2 < player.farms.farmfields.farmfields[index1].FloorTiles.Count; ++index2)
          Enclosure_Farm_Map.EnclosureMap[player.farms.farmfields.farmfields[index1].FloorTiles[index2].X, player.farms.farmfields.farmfields[index1].FloorTiles[index2].Y] = -player.farms.farmfields.farmfields[index1].Cell_UID;
      }
    }

    public void UpdateEnclosure_Farm_Map()
    {
    }

    public void DrawEnclosure_Farm_Map()
    {
    }
  }
}
