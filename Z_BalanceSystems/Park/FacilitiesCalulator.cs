// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Park.FacilitiesCalulator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_BalanceSystems.Park
{
  internal class FacilitiesCalulator
  {
    internal static float CalculateFacilities(Player player)
    {
      float num = 0.0f;
      int totalLandUnlocked = PlayerStats.GetTotalLandUnlocked();
      int[] numArray = new int[743];
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        if (player.shopstatus.shopentries[index].GetEmplyeeCount() > 0)
        {
          ++numArray[(int) player.shopstatus.shopentries[index].tiletype];
          if (numArray[(int) player.shopstatus.shopentries[index].tiletype] == 1 || numArray[(int) player.shopstatus.shopentries[index].tiletype] < totalLandUnlocked / 4)
          {
            num += 2f;
          }
          else
          {
            switch (numArray[(int) player.shopstatus.shopentries[index].tiletype] - totalLandUnlocked / 4)
            {
              case 1:
                num += 0.5f;
                continue;
              case 2:
                num += 0.3f;
                continue;
              default:
                num += 0.1f;
                continue;
            }
          }
        }
      }
      bool[,] flagArray1 = new bool[TileMath.GetTotalSectors().X, TileMath.GetTotalSectors().Y];
      for (int index = 0; index < player.shopstatus.Toilets.Count; ++index)
      {
        Vector2Int locationToSector = TileMath.GetLocationToSector(player.shopstatus.Toilets[index].LocationOfThisShop.X, player.shopstatus.Toilets[index].LocationOfThisShop.Y);
        if (!flagArray1[locationToSector.X, locationToSector.Y])
        {
          flagArray1[locationToSector.X, locationToSector.Y] = true;
          num += 0.5f;
        }
        else
          num += 0.1f;
      }
      bool[,] flagArray2 = new bool[TileMath.GetTotalSectors().X, TileMath.GetTotalSectors().Y];
      for (int index = 0; index < player.shopstatus.Bins.Count; ++index)
      {
        Vector2Int locationToSector = TileMath.GetLocationToSector(player.shopstatus.Bins[index].LocationOfThisShop.X, player.shopstatus.Bins[index].LocationOfThisShop.Y);
        if (!flagArray2[locationToSector.X, locationToSector.Y])
        {
          flagArray2[locationToSector.X, locationToSector.Y] = true;
          num += 0.3f;
        }
        else
          num += 0.05f;
      }
      bool[,] flagArray3 = new bool[TileMath.GetTotalSectors().X, TileMath.GetTotalSectors().Y];
      for (int index = 0; index < player.shopstatus.Benches.Count; ++index)
      {
        Vector2Int locationToSector = TileMath.GetLocationToSector(player.shopstatus.Benches[index].LocationOfThisShop.X, player.shopstatus.Benches[index].LocationOfThisShop.Y);
        if (!flagArray3[locationToSector.X, locationToSector.Y])
        {
          flagArray3[locationToSector.X, locationToSector.Y] = true;
          num += 0.3f;
        }
        else
          num += 0.05f;
      }
      if (player.shopstatus.ArchitectOffice.Count > 0)
        num += 3f;
      if (player.storerooms.HasBuiltStoreRoom)
        num += 3f;
      return num * 20f;
    }
  }
}
