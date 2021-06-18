// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.DeveloperMenu.RebuildShopStatuses
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Tile_Data;

namespace TinyZoo.Utils.DeveloperMenu
{
  internal class RebuildShopStatuses
  {
    internal static void DoRebuildShopStatuses(Player player)
    {
      for (int _X = 0; _X < player.prisonlayout.layout.BaseTileTypes.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < player.prisonlayout.layout.BaseTileTypes.GetLength(1); ++_Y)
        {
          if (player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype != TILETYPE.None && !player.prisonlayout.layout.BaseTileTypes[_X, _Y].isChild())
          {
            if (TileData.IsThisABench(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
            {
              bool flag = false;
              for (int index = 0; index < player.shopstatus.Benches.Count; ++index)
              {
                if (player.shopstatus.Benches[index].LocationOfThisShop.X == _X && player.shopstatus.Benches[index].LocationOfThisShop.Y == _Y)
                  flag = true;
              }
              if (!flag)
                player.shopstatus.BuiltABuilding(new Vector2Int(_X, _Y), player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, player.prisonlayout.layout.BaseTileTypes[_X, _Y].RotationClockWise, player, false, out int _);
            }
            else if (TileData.IsThisaToilet(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
            {
              bool flag = false;
              for (int index = 0; index < player.shopstatus.Toilets.Count; ++index)
              {
                if (player.shopstatus.Toilets[index].LocationOfThisShop.X == _X && player.shopstatus.Toilets[index].LocationOfThisShop.Y == _Y)
                  flag = true;
              }
              if (!flag)
                player.shopstatus.BuiltABuilding(new Vector2Int(_X, _Y), player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, player.prisonlayout.layout.BaseTileTypes[_X, _Y].RotationClockWise, player, false, out int _);
            }
            else if (TileData.IsAShopOrProfitMakingThing(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
            {
              bool flag = false;
              for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
              {
                if (player.shopstatus.shopentries[index].LocationOfThisShop.X == _X && player.shopstatus.shopentries[index].LocationOfThisShop.Y == _Y)
                  flag = true;
              }
              if (!flag)
                player.shopstatus.BuiltABuilding(new Vector2Int(_X, _Y), player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, player.prisonlayout.layout.BaseTileTypes[_X, _Y].RotationClockWise, player, false, out int _);
            }
            else if (TileData.IsThisABin(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
            {
              bool flag = false;
              for (int index = 0; index < player.shopstatus.Bins.Count; ++index)
              {
                if (player.shopstatus.Bins[index].LocationOfThisShop.X == _X && player.shopstatus.Bins[index].LocationOfThisShop.Y == _Y)
                  flag = true;
              }
              if (!flag)
                player.shopstatus.BuiltABuilding(new Vector2Int(_X, _Y), player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, player.prisonlayout.layout.BaseTileTypes[_X, _Y].RotationClockWise, player, false, out int _);
            }
            else if (TileData.IsAnArchitectOffice(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
            {
              bool flag = false;
              for (int index = 0; index < player.shopstatus.ArchitectOffice.Count; ++index)
              {
                if (player.shopstatus.ArchitectOffice[index].LocationOfThisShop.X == _X && player.shopstatus.ArchitectOffice[index].LocationOfThisShop.Y == _Y)
                  flag = true;
              }
              if (!flag)
                player.shopstatus.BuiltABuilding(new Vector2Int(_X, _Y), player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, player.prisonlayout.layout.BaseTileTypes[_X, _Y].RotationClockWise, player, false, out int _);
            }
          }
        }
      }
    }
  }
}
