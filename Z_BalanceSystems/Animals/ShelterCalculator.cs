// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.ShelterCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class ShelterCalculator
  {
    internal static void CalculateShelter(PrisonZone prisonzone, Player player)
    {
      prisonzone.Temp_UnblockedFloorSpace = 0;
      prisonzone.Temp_ShelteredFloorSpace = 0;
      for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
      {
        if (player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].tiletype != TILETYPE.None)
        {
          if (TileData.IsThisAShelter(player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].tiletype))
          {
            prisonzone.Temp_UnblockedFloorSpace += 100;
            prisonzone.Temp_ShelteredFloorSpace += 100;
          }
        }
        else
        {
          prisonzone.Temp_UnblockedFloorSpace += 100;
          prisonzone.Temp_ShelteredFloorSpace += OverWorldManager.heatmapmanager.animalprivacymap.GetPrivacayPercentage(prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y);
        }
      }
    }
  }
}
