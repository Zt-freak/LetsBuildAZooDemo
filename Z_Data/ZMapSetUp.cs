// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Data.ZMapSetUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using System;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Bus;

namespace TinyZoo.Z_Data
{
  internal class ZMapSetUp
  {
    private static Vector2Int temp_thisTile = new Vector2Int();

    internal static void CreatePathSet()
    {
      GameFlags.pathset = new PathSet();
      GameFlags.pathset.CreateGrid(TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      GameFlags.pathset.BlockAllTiles();
      GameFlags.Water_PathSet = new PathSet();
      GameFlags.Water_PathSet.CreateGrid(TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      GameFlags.Water_PathSet.BlockAllTiles();
      if (DebugFlags.LoadYvonnesZoo)
      {
        for (int index1 = 0; index1 < PlayerStats.unblockedSectors.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < PlayerStats.unblockedSectors.GetLength(1); ++index2)
            PlayerStats.unblockedSectors[index1, index2] = true;
        }
        ZMapSetUp.UnblockTilesUnderLogo();
      }
      ZMapSetUp.UnlockAllExistingLand();
    }

    internal static void UnblockTilesUnderLogo()
    {
      for (int index = -1; index < 3; ++index)
      {
        Vector2Int vector2Int1 = new Vector2Int(new Vector2Int(WalkingPerson.LogoLocation.X, WalkingPerson.LogoLocation.Y - index));
        Z_GameFlags.pathfinder.UnblockTile(vector2Int1.X, vector2Int1.Y, true);
        Vector2Int vector2Int2 = new Vector2Int(WalkingPerson.LogoLocation.X - 1, WalkingPerson.LogoLocation.Y - index);
        Z_GameFlags.pathfinder.UnblockTile(vector2Int2.X, vector2Int2.Y, true);
        Vector2Int vector2Int3 = new Vector2Int(WalkingPerson.LogoLocation.X + 1, WalkingPerson.LogoLocation.Y - index);
        Z_GameFlags.pathfinder.UnblockTile(vector2Int3.X, vector2Int3.Y, true);
      }
    }

    internal static void SetUpMapFences(
      int Level,
      PrisonLayout prison,
      Player player,
      ConsumptionStatus consumptionstatus)
    {
      ZMapSetUp.CreatePathSet();
      Vector2Int TopLeft;
      Vector2Int BottomRight;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out TopLeft, out BottomRight, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      PlayerStats.unblockedSectors[TopLeft.X / TileMath.SectorSize, TopLeft.Y / TileMath.SectorSize] = true;
      PlayerStats.RecountUnblockedSectors();
      Vector2Int Location1 = new Vector2Int(TopLeft.X + 11, BottomRight.Y + 1);
      if (!DebugFlags.LoadYvonnesZoo)
      {
        prison.BuildStructure(TILETYPE.Logo, Location1, consumptionstatus, player, 0);
        Vector2Int Location2 = new Vector2Int(TopLeft.X + 1, TopLeft.Y + 2);
        prison.BuildStructure(TILETYPE.ManagmentOffice_SimpleBlack, Location2, consumptionstatus, player, 0);
        prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, new Vector2Int(Location2.X + 2, Location2.Y - 2), 0, player, true);
        prison.BuildStructure(TILETYPE.WaterPumpStation, new Vector2Int(Location2.X + 2, Location2.Y - 2), consumptionstatus, player, 0);
        for (int x = TopLeft.X; x < TopLeft.X + 3; ++x)
        {
          for (int y = TopLeft.Y; y < TopLeft.Y + 3; ++y)
            prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, new Vector2Int(x, y), 0, player, true);
        }
        player.shopstatus.BuiltABuilding(Location2, TILETYPE.StoreRoom, 0, player, false, out int _);
        player.storerooms.HasBuiltStoreRoom = false;
      }
      WalkingPerson.LogoLocation = new Vector2Int(TopLeft.X + 11, BottomRight.Y + 1);
      for (int index = -1; index < 3; ++index)
      {
        Vector2Int vector2Int1 = new Vector2Int(new Vector2Int(WalkingPerson.LogoLocation.X, WalkingPerson.LogoLocation.Y - index));
        Z_GameFlags.pathfinder.UnblockTile(vector2Int1.X, vector2Int1.Y, true);
        Vector2Int vector2Int2 = new Vector2Int(WalkingPerson.LogoLocation.X - 1, WalkingPerson.LogoLocation.Y - index);
        Z_GameFlags.pathfinder.UnblockTile(vector2Int2.X, vector2Int2.Y, true);
        Vector2Int vector2Int3 = new Vector2Int(WalkingPerson.LogoLocation.X + 1, WalkingPerson.LogoLocation.Y - index);
        Z_GameFlags.pathfinder.UnblockTile(vector2Int3.X, vector2Int3.Y, true);
      }
      for (int x = TopLeft.X; x < TopLeft.X + 13; ++x)
      {
        for (int _Y = BottomRight.Y - 1; _Y < BottomRight.Y + 2; ++_Y)
          prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, new Vector2Int(x, _Y), 0, player, true);
      }
      int num1 = (BottomRight.X - TopLeft.X) / 2 + TopLeft.X;
      for (int x = TopLeft.X; x < BottomRight.X + 1; ++x)
      {
        int num2 = 2;
        prison.AddTile(TILETYPE.DefaultFence_WallSide, new Vector2Int(x, TopLeft.Y - 1), 1, player);
        prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, new Vector2Int(x, TopLeft.Y - 1), 0, player, true);
        if (x < num1 - num2 || x > num1 + num2)
        {
          prison.AddTile(TILETYPE.DefaultFence_WallSide, new Vector2Int(x, BottomRight.Y + 1), 3, player);
          prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, new Vector2Int(x, BottomRight.Y + 1), 0, player, true);
        }
      }
      Vector2Int Location3 = new Vector2Int();
      for (int y = TopLeft.Y; y < BottomRight.Y + 1; ++y)
      {
        Location3.X = TopLeft.X - 1;
        Location3.Y = y;
        prison.AddTile(TILETYPE.DefaultFence_WallSide, Location3, 0, player);
        prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
        Location3.X = BottomRight.X + 1;
        prison.AddTile(TILETYPE.DefaultFence_WallSide, Location3, 2, player);
        prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
      }
      Location3.X = TopLeft.X - 1;
      Location3.Y = TopLeft.Y - 1;
      prison.AddTile(TILETYPE.DefaultFence_WallCorner, Location3, 0, player);
      prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
      Location3.X = BottomRight.X + 1;
      Location3.Y = TopLeft.Y - 1;
      prison.AddTile(TILETYPE.DefaultFence_WallCorner, Location3, 1, player);
      prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
      Location3.X = TopLeft.X - 1;
      Location3.Y = BottomRight.Y + 1;
      prison.AddTile(TILETYPE.DefaultFence_WallCorner, Location3, 2, player);
      prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
      Location3.X = BottomRight.X + 1;
      Location3.Y = BottomRight.Y + 1;
      prison.AddTile(TILETYPE.DefaultFence_WallCorner, Location3, 3, player);
      prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
      for (int x = TopLeft.X; x < BottomRight.X + 1; ++x)
      {
        for (int y1 = TopLeft.Y; y1 < BottomRight.Y + 1; ++y1)
        {
          if (ZMapSetUp.CanPlace(prison.layout.BaseTileTypes[x, y1].tiletype))
          {
            Location3.X = x;
            Location3.Y = y1;
            prison.AddTile(TILETYPE.EMPTY_DIRT_WALKABLE_TILE, Location3, 0, player, true);
            prison.layout.BaseTileTypes[Location3.X, Location3.Y] = new LayoutEntry(TILETYPE.None);
            Z_GameFlags.pathfinder.UnblockTile(Location3.X, Location3.Y, true);
            if (x == TopLeft.X)
            {
              if (y1 != TopLeft.Y)
                ;
            }
            else if (x == BottomRight.X)
            {
              if (y1 != TopLeft.Y)
                ;
            }
            else if (y1 == BottomRight.Y)
            {
              if (x < num1 - 3 || x <= num1 + 3)
                ;
            }
            else
            {
              int y2 = TopLeft.Y;
            }
          }
        }
      }
      for (int _X = 0; _X < TileMath.GetOverWorldMapSize_XDefault(); ++_X)
      {
        if (_X < num1)
          prison.AddTile(TILETYPE.Zoo_PathDoubleSided, new Vector2Int(_X, BottomRight.Y + 2), 0, player);
        prison.AddTile(TILETYPE.Road, new Vector2Int(_X, BottomRight.Y + 4), 0, player);
      }
      for (int _X = Location1.X - 1; _X < Location1.X + 2; ++_X)
      {
        for (int _Y = Location1.Y - 6; _Y < Location1.Y + 2; ++_Y)
        {
          prison.AddTile(TILETYPE.Floor_GreyBricks, new Vector2Int(_X, _Y), 0, player, true);
          if (_X == Location1.X - 1 && _Y < Location1.Y - 2)
          {
            if (_Y != Location1.Y - 6)
            {
              prison.AddTile(TILETYPE.Ferns, new Vector2Int(_X - 1, _Y), 3, player);
              prison.AddTile(TILETYPE.Ferns, new Vector2Int(_X + 3, _Y), 3, player);
            }
            else
            {
              prison.AddTile(TILETYPE.Lamppost, new Vector2Int(_X - 1, _Y), 3, player);
              prison.AddTile(TILETYPE.Lamppost, new Vector2Int(_X + 3, _Y), 3, player);
            }
          }
        }
      }
      Z_BusManager.StartPosition = new Vector2Int(0, BottomRight.Y + 4);
      Z_GameFlags.pathfinder.ForceResolvePathFinding();
    }

    internal static void BuyMoreLand(Player player) => throw new Exception("OLD FUNCTION -  use sector functions");

    private static bool CanPlace(TILETYPE tile) => tile == TILETYPE.None || tile == TILETYPE.PinkMoonPlant || tile == TILETYPE.BoundaryTree;

    internal static bool IsSectorAdjacentForBuying(int sectorGridLocationX, int sectorGridLocationY)
    {
      for (int index = 0; index < 4; ++index)
      {
        Vector2Int vector2Int = new Vector2Int(sectorGridLocationX, sectorGridLocationY) + ButtonRepeater.GetDirectionPressedToVector2Int((DirectionPressed) index);
        if (vector2Int.X > -1 && vector2Int.X < PlayerStats.unblockedSectors.GetLength(0) && (vector2Int.Y > -1 && vector2Int.Y < PlayerStats.unblockedSectors.GetLength(1)) && PlayerStats.unblockedSectors[vector2Int.X, vector2Int.Y])
          return true;
      }
      return false;
    }

    internal static void UnlockThisSector(
      Player player,
      Vector2Int sectorGridLocation,
      OverWorldEnvironmentManager overworldManager,
      bool RemakeTileList = true,
      bool SkipVallidateAndTileChangeForLoad = false)
    {
      PlayerStats.unblockedSectors[sectorGridLocation.X, sectorGridLocation.Y] = true;
      PlayerStats.RecountUnblockedSectors();
      ZMapSetUp.BuyMoreLandSector(player, overworldManager, sectorGridLocation, SkipVallidateAndTileChangeForLoad, RemakeTileList);
    }

    internal static void UnlockAllExistingLand()
    {
      for (int _X = 0; _X < PlayerStats.unblockedSectors.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < PlayerStats.unblockedSectors.GetLength(1); ++_Y)
        {
          if (PlayerStats.unblockedSectors[_X, _Y])
            ZMapSetUp.BuyMoreLandSector((Player) null, OverWorldManager.overworldenvironment, new Vector2Int(_X, _Y), true);
        }
      }
    }

    private static void BuyMoreLandSector(
      Player player,
      OverWorldEnvironmentManager overworldManager,
      Vector2Int SelectorJustUnlocked,
      bool SkipVallidateAndTileChangeForLoad = false,
      bool remakeTileList = false)
    {
      int x1 = SelectorJustUnlocked.X;
      int y1 = SelectorJustUnlocked.Y;
      Vector2Int vector2Int1 = new Vector2Int(x1 * TileMath.SectorSize, y1 * TileMath.SectorSize);
      Vector2Int vector2Int2 = new Vector2Int(x1 * TileMath.SectorSize + TileMath.SectorSize, y1 * TileMath.SectorSize + TileMath.SectorSize);
      for (int x2 = vector2Int1.X; x2 < vector2Int2.X; ++x2)
      {
        for (int y2 = vector2Int1.Y; y2 < vector2Int2.Y; ++y2)
        {
          bool flag1 = false;
          bool flag2 = false;
          if (!SkipVallidateAndTileChangeForLoad)
          {
            if (player.prisonlayout.layout.BaseTileTypes[x2, y2].tiletype == TILETYPE.None || player.prisonlayout.layout.BaseTileTypes[x2, y2].tiletype == TILETYPE.PinkMoonPlant || player.prisonlayout.layout.BaseTileTypes[x2, y2].tiletype == TILETYPE.BoundaryTree)
            {
              if (player.prisonlayout.layout.BaseTileTypes[x2, y2].tiletype != TILETYPE.None)
              {
                player.prisonlayout.layout.BaseTileTypes[x2, y2].tiletype = TILETYPE.None;
                flag2 = true;
              }
              if (player.prisonlayout.layout.FloorTileTypes[x2, y2].tiletype == TILETYPE.Moon || player.prisonlayout.layout.FloorTileTypes[x2, y2].tiletype == TILETYPE.None)
              {
                player.prisonlayout.layout.FloorTileTypes[x2, y2].tiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
                flag1 = true;
              }
              Z_GameFlags.pathfinder.UnblockTile(x2, y2, true);
            }
          }
          else
            Z_GameFlags.pathfinder.UnblockTile(x2, y2, true);
          if (!SkipVallidateAndTileChangeForLoad)
          {
            if (flag1)
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, new Vector2Int(x2, y2), false);
            if (flag2)
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: new Vector2Int(x2, y2), DoRemakeTileLists: false);
          }
        }
      }
      if (SkipVallidateAndTileChangeForLoad)
        return;
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          int index3 = SelectorJustUnlocked.X + index1;
          int index4 = SelectorJustUnlocked.Y + index2;
          if (index3 > -1 && index3 < PlayerStats.unblockedSectors.GetLength(0) && (index4 > -1 && index4 < PlayerStats.unblockedSectors.GetLength(1)) && PlayerStats.unblockedSectors[index3, index4])
          {
            Vector2Int vector2Int3 = new Vector2Int(index3 * TileMath.SectorSize, index4 * TileMath.SectorSize);
            Vector2Int vector2Int4 = new Vector2Int(index3 * TileMath.SectorSize + TileMath.SectorSize, index4 * TileMath.SectorSize + TileMath.SectorSize);
            bool flag1 = index3 > 0 && !PlayerStats.unblockedSectors[index3 - 1, index4] || index3 == 0;
            bool flag2 = index3 + 1 < PlayerStats.unblockedSectors.GetLength(0) && !PlayerStats.unblockedSectors[index3 + 1, index4] || index3 == PlayerStats.unblockedSectors.GetLength(0) - 1;
            bool flag3 = index4 > 0 && !PlayerStats.unblockedSectors[index3, index4 - 1] || index4 == 0;
            bool flag4 = index4 + 1 < PlayerStats.unblockedSectors.GetLength(1) && !PlayerStats.unblockedSectors[index3, index4 + 1] || TileMath.IsBelowPark(vector2Int4.Y + 1);
            bool flag5 = index3 > 0 && index4 > 0 && !PlayerStats.unblockedSectors[index3 - 1, index4 - 1] || index3 == 0;
            bool flag6 = index4 > 0 && index3 + 1 < PlayerStats.unblockedSectors.GetLength(0) && !PlayerStats.unblockedSectors[index3 + 1, index4 - 1] || index3 == PlayerStats.unblockedSectors.GetLength(0) - 1;
            bool flag7 = index3 > 0 && index4 + 1 < PlayerStats.unblockedSectors.GetLength(1) && !PlayerStats.unblockedSectors[index3 - 1, index4 + 1];
            bool flag8 = index3 + 1 < PlayerStats.unblockedSectors.GetLength(0) && index4 + 1 < PlayerStats.unblockedSectors.GetLength(1) && !PlayerStats.unblockedSectors[index3 + 1, index4 + 1];
            for (int y2 = vector2Int3.Y; y2 < vector2Int4.Y; ++y2)
            {
              ZMapSetUp.temp_thisTile.X = vector2Int3.X;
              ZMapSetUp.temp_thisTile.Y = y2;
              if (flag1)
              {
                player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallSide, ZMapSetUp.temp_thisTile, 0, player);
                overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
                ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int3.X - 1, y2);
                if (vector2Int3.X - 1 >= 0 && player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.BoundaryTree)
                {
                  player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype = TILETYPE.None;
                  overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
                }
              }
              else if (player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallCorner || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallSide || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_InnerCorner)
              {
                player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype = TILETYPE.None;
                Z_GameFlags.pathfinder.UnblockTile(ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y, true);
                overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
              }
            }
            for (int y2 = vector2Int3.Y; y2 < vector2Int4.Y; ++y2)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int4.X - 1, y2);
              if (flag2)
                player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallSide, ZMapSetUp.temp_thisTile, 2, player);
              else if (player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallCorner || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallSide || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_InnerCorner)
              {
                player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype = TILETYPE.None;
                Z_GameFlags.pathfinder.UnblockTile(ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y, true);
              }
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            for (int x2 = vector2Int3.X; x2 < vector2Int4.X; ++x2)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(x2, vector2Int3.Y);
              if (flag3)
                player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallSide, ZMapSetUp.temp_thisTile, 1, player);
              else if ((player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallCorner || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallSide || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_InnerCorner) && ((ZMapSetUp.temp_thisTile.X != vector2Int3.X || ZMapSetUp.temp_thisTile.Y != vector2Int3.Y) && (ZMapSetUp.temp_thisTile.Y != vector2Int3.Y || ZMapSetUp.temp_thisTile.X != vector2Int4.X - 1)))
              {
                player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype = TILETYPE.None;
                Z_GameFlags.pathfinder.UnblockTile(ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y, true);
              }
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            for (int x2 = vector2Int3.X; x2 < vector2Int4.X; ++x2)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(x2, vector2Int4.Y - 1);
              if (flag4)
              {
                if (player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.None)
                  player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallSide, ZMapSetUp.temp_thisTile, 3, player);
              }
              else if ((player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallCorner || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_WallSide || player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype == TILETYPE.DefaultFence_InnerCorner) && ((ZMapSetUp.temp_thisTile.X != vector2Int3.X || ZMapSetUp.temp_thisTile.Y != vector2Int4.Y - 1) && (ZMapSetUp.temp_thisTile.Y != vector2Int4.Y - 1 || ZMapSetUp.temp_thisTile.X != vector2Int4.X - 1)))
              {
                player.prisonlayout.layout.BaseTileTypes[ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y].tiletype = TILETYPE.None;
                Z_GameFlags.pathfinder.UnblockTile(ZMapSetUp.temp_thisTile.X, ZMapSetUp.temp_thisTile.Y, true);
              }
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (flag3 & flag1)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int3.X, vector2Int3.Y);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallCorner, ZMapSetUp.temp_thisTile, 0, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (flag3 & flag2)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int4.X - 1, vector2Int3.Y);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallCorner, ZMapSetUp.temp_thisTile, 1, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (flag4 & flag1)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int3.X, vector2Int4.Y - 1);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallCorner, ZMapSetUp.temp_thisTile, 2, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (flag4 & flag2)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int4.X - 1, vector2Int4.Y - 1);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_WallCorner, ZMapSetUp.temp_thisTile, 3, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (((flag3 ? 0 : (!flag1 ? 1 : 0)) & (flag5 ? 1 : 0)) != 0)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int3.X, vector2Int3.Y);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_InnerCorner, ZMapSetUp.temp_thisTile, 1, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (((flag3 ? 0 : (!flag2 ? 1 : 0)) & (flag6 ? 1 : 0)) != 0)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int4.X - 1, vector2Int3.Y);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_InnerCorner, ZMapSetUp.temp_thisTile, 3, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (((flag4 ? 0 : (!flag1 ? 1 : 0)) & (flag7 ? 1 : 0)) != 0)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int3.X, vector2Int4.Y - 1);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_InnerCorner, ZMapSetUp.temp_thisTile, 0, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
            if (((flag4 ? 0 : (!flag2 ? 1 : 0)) & (flag8 ? 1 : 0)) != 0)
            {
              ZMapSetUp.temp_thisTile = new Vector2Int(vector2Int4.X - 1, vector2Int4.Y - 1);
              player.prisonlayout.AddTile(TILETYPE.DefaultFence_InnerCorner, ZMapSetUp.temp_thisTile, 2, player);
              overworldManager.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: ZMapSetUp.temp_thisTile, DoRemakeTileLists: false);
            }
          }
        }
      }
      if (!remakeTileList)
        return;
      overworldManager.wallsandfloors.RemakeTileList();
    }
  }
}
