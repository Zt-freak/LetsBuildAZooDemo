// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.BuyLand.AddForSaleSigns
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.BuyLand
{
  internal class AddForSaleSigns
  {
    internal static void RemoveSign(Player player, Vector2Int Position)
    {
      int num1 = TileMath.SectorSize * Position.X;
      int num2 = TileMath.SectorSize * Position.Y;
      int _X = num1 + TileMath.SectorSize / 2;
      int _Y = num2 + TileMath.SectorSize / 2;
      if (player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype != TILETYPE.ForSaleSignboard)
        return;
      LayoutEntry _layoutentry = new LayoutEntry(TILETYPE.ForSaleSignboard);
      Vector2Int position = new Vector2Int(_X, _Y);
      player.prisonlayout.SellStructure(position, _layoutentry, player.livestats.consumptionstatus, player);
      OverWorldManager.overworldenvironment.SellStructure(position, _layoutentry, player.prisonlayout.layout);
    }

    internal static void AddSigns(Player player)
    {
      Z_GameFlags.ScrubForSaleSigns = false;
      if (FeatureFlags.BlockBuyLand)
        return;
      for (int X = 0; X < PlayerStats.unblockedSectors.GetLength(0); ++X)
      {
        for (int Y = 0; Y < PlayerStats.unblockedSectors.GetLength(1); ++Y)
        {
          if (!PlayerStats.unblockedSectors[X, Y])
          {
            if (X < PlayerStats.unblockedSectors.GetLength(0) - 1 && PlayerStats.unblockedSectors[X + 1, Y])
              AddForSaleSigns.AddSignHere(player, X, Y);
            if (X > 0 && PlayerStats.unblockedSectors[X - 1, Y])
              AddForSaleSigns.AddSignHere(player, X, Y);
            if (Y < PlayerStats.unblockedSectors.GetLength(1) - 1 && PlayerStats.unblockedSectors[X, Y + 1])
              AddForSaleSigns.AddSignHere(player, X, Y);
            if (Y > 0 && PlayerStats.unblockedSectors[X, Y - 1])
              AddForSaleSigns.AddSignHere(player, X, Y);
          }
        }
      }
    }

    private static void AddSignHere(Player player, int X, int Y)
    {
      int num1 = TileMath.SectorSize * X;
      int num2 = TileMath.SectorSize * Y;
      int index1 = num1 + TileMath.SectorSize / 2;
      int index2 = num2 + TileMath.SectorSize / 2;
      if (player.prisonlayout.layout.BaseTileTypes[index1, index2].tiletype == TILETYPE.ForSaleSignboard)
        return;
      TileRenderer tilerenderer = new TileRenderer(new LayoutEntry(TILETYPE.ForSaleSignboard), index1, index2, false);
      player.prisonlayout.BuildTileFromTileRenderer(tilerenderer, player.livestats.consumptionstatus, player);
      OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: new Vector2Int(index1, index2));
    }
  }
}
