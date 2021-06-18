// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.TileData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Tile_Data
{
  internal class TileData
  {
    private static TileInfo[] tileinfo;
    private static TileStats[] tilestats;
    private static HashSet<TILETYPE> MeaningfulStructures;
    private static HashSet<TILETYPE> ShopsWithStats;
    private static HashSet<TILETYPE> Drinks;
    private static HashSet<TILETYPE> Food;
    private static HashSet<TILETYPE> Cooling;
    private static HashSet<TILETYPE> ProfitMaking_NonShop;
    private static HashSet<TILETYPE> SouvenirShop;
    private static HashSet<TILETYPE> Toilets;
    private static HashSet<TILETYPE> ATMs;
    private static HashSet<TILETYPE> Benches;
    private static HashSet<TILETYPE> TrashBins;
    private static HashSet<TILETYPE> SignBoards;
    private static HashSet<TILETYPE> Lamppost;
    private static HashSet<TILETYPE> WaterTroughs;
    private static HashSet<TILETYPE> PenFloorTileTypes;
    private static HashSet<TILETYPE> PenBoundaries;
    private static HashSet<TILETYPE> Shelters;
    private static HashSet<TILETYPE> PenDeco;
    private static HashSet<TILETYPE> ManagementOffices;
    private static HashSet<TILETYPE> StoreRooms;
    private static HashSet<TILETYPE> VendingMachines;
    private static HashSet<TILETYPE> TicketOffices;
    private static HashSet<TILETYPE> TicketedRides;
    private static HashSet<TILETYPE> AirVehicleBuildings;
    private static HashSet<TILETYPE> Facilities;
    private static HashSet<TILETYPE> FactoryBuildings;
    private static HashSet<TILETYPE> SlowFloors;
    private static HashSet<TILETYPE> Trevelators;
    private static HashSet<TILETYPE> MaskingItems;
    private static HashSet<TILETYPE> PartialFloors;
    private static HashSet<TILETYPE> WarpGates;
    private static HashSet<TILETYPE> TopFloors;

    internal static TileInfo GetTileInfo(TILETYPE tiletype)
    {
      // ISSUE: The method is too long to display (99804 instructions)
    }

    internal static bool DoesThisFloorAnimate(TILETYPE tiletype) => tiletype == TILETYPE.Volume_Water;

    private static void SetWalls(TILETYPE tiletype)
    {
      TileData.tileinfo[(int) tiletype].SetFence(0, 0);
      TileData.tileinfo[(int) tiletype].SetFence(1, 1);
      TileData.tileinfo[(int) tiletype].SetFence(2, 2);
      TileData.tileinfo[(int) tiletype].SetFence(3, 3);
      TileData.tileinfo[(int) tiletype].SetFence(4, 0);
      TileData.tileinfo[(int) tiletype].SetFence(4, 1);
      TileData.tileinfo[(int) tiletype].SetFence(5, 1);
      TileData.tileinfo[(int) tiletype].SetFence(5, 2);
      TileData.tileinfo[(int) tiletype].SetFence(6, 2);
      TileData.tileinfo[(int) tiletype].SetFence(6, 3);
      TileData.tileinfo[(int) tiletype].SetFence(7, 3);
      TileData.tileinfo[(int) tiletype].SetFence(7, 0);
      TileData.tileinfo[(int) tiletype].SetFence(8, 1);
      TileData.tileinfo[(int) tiletype].SetFence(8, 3);
      TileData.tileinfo[(int) tiletype].SetFence(9, 0);
      TileData.tileinfo[(int) tiletype].SetFence(9, 2);
      TileData.tileinfo[(int) tiletype].SetFence(10, 0);
      TileData.tileinfo[(int) tiletype].SetFence(10, 1);
      TileData.tileinfo[(int) tiletype].SetFence(10, 2);
      TileData.tileinfo[(int) tiletype].SetFence(11, 1);
      TileData.tileinfo[(int) tiletype].SetFence(11, 2);
      TileData.tileinfo[(int) tiletype].SetFence(11, 3);
      TileData.tileinfo[(int) tiletype].SetFence(12, 2);
      TileData.tileinfo[(int) tiletype].SetFence(12, 3);
      TileData.tileinfo[(int) tiletype].SetFence(12, 0);
      TileData.tileinfo[(int) tiletype].SetFence(13, 0);
      TileData.tileinfo[(int) tiletype].SetFence(13, 1);
      TileData.tileinfo[(int) tiletype].SetFence(13, 2);
      TileData.tileinfo[(int) tiletype].SetFence(13, 3);
    }

    internal static bool IsThisFloorAVolume(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.Volume_RedPathway:
        case TILETYPE.Volume_Grass:
        case TILETYPE.Volume_WoodenBoards:
        case TILETYPE.Volume_WhiteSnow:
        case TILETYPE.Volume_Water:
        case TILETYPE.Volume_WoodenBridge:
        case TILETYPE.Volume_DarkGrass:
        case TILETYPE.Volume_YellowGrass:
        case TILETYPE.Volume_Sand:
        case TILETYPE.Volume_SoilPlot:
        case TILETYPE.Volume_LightMud:
        case TILETYPE.Volume_LightGrass:
        case TILETYPE.CorruptedDirtFloor_Volume:
        case TILETYPE.CorruptedDirtGrass_Volume:
        case TILETYPE.CorruptedSnow_Volume:
        case TILETYPE.CorruptedSand_Volume:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsThisADuplicatePlacable(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.Floor_GreenGrass:
        case TILETYPE.Floor_Dirt:
        case TILETYPE.Floor_RedCircles:
        case TILETYPE.Floor_GreyBricks:
        case TILETYPE.Floor_OrangeTiles:
        case TILETYPE.Floor_ColorfulBrickTile:
        case TILETYPE.Floor_BlueCircleTiles:
        case TILETYPE.Floor_WoodenBoards:
        case TILETYPE.Floor_MetalDecoTile:
        case TILETYPE.Floor_PawDecoTile:
        case TILETYPE.Floor_Cobblestone:
        case TILETYPE.Floor_PinkSmallTiles:
        case TILETYPE.Floor_BrownOctoTile:
        case TILETYPE.Floor_BrownOctoTile2:
        case TILETYPE.Floor_GreenAndBlueDiamondTile:
        case TILETYPE.Floor_BrownSquareTile:
          return true;
        default:
          return false;
      }
    }

    internal static CellBlockType GetTileTypeToCellBlockType(TILETYPE tile)
    {
      switch (tile)
      {
        case TILETYPE.DesertEnclosure:
          return CellBlockType.Desert;
        case TILETYPE.MountainEnclosure:
          return CellBlockType.Mountain;
        case TILETYPE.ArcticEnclosure:
          return CellBlockType.Arctic;
        case TILETYPE.TropicalEnclosure:
          return CellBlockType.Tropical;
        case TILETYPE.ForestEnclosure:
          return CellBlockType.Forest;
        case TILETYPE.SavannahEnclosure:
          return CellBlockType.Savannah;
        case TILETYPE.FieldPicketFenceEnclosure:
          return CellBlockType.FieldPicketFence;
        case TILETYPE.CorruptedGrassEnclosure:
          return CellBlockType.CorruptedGrass;
        case TILETYPE.CorruptedForestEnclosure:
          return CellBlockType.CorruptedForest;
        case TILETYPE.GraveYard:
          return CellBlockType.GraveYard;
        default:
          return CellBlockType.Grasslands;
      }
    }

    internal static string GetCellBlockName(CellBlockType cellblock)
    {
      switch (cellblock)
      {
        case CellBlockType.Grasslands:
          return TileData.GetTileStats(TILETYPE.GrassEnclosure).Name;
        case CellBlockType.Forest:
          return TileData.GetTileStats(TILETYPE.ForestEnclosure).Name;
        case CellBlockType.Savannah:
          return TileData.GetTileStats(TILETYPE.SavannahEnclosure).Name;
        case CellBlockType.Desert:
          return TileData.GetTileStats(TILETYPE.DesertEnclosure).Name;
        case CellBlockType.Mountain:
          return TileData.GetTileStats(TILETYPE.MountainEnclosure).Name;
        case CellBlockType.Arctic:
          return TileData.GetTileStats(TILETYPE.ArcticEnclosure).Name;
        case CellBlockType.Tropical:
          return TileData.GetTileStats(TILETYPE.TropicalEnclosure).Name;
        case CellBlockType.FieldPicketFence:
          return TileData.GetTileStats(TILETYPE.FieldPicketFenceEnclosure).Name;
        case CellBlockType.CorruptedGrass:
          return TileData.GetTileStats(TILETYPE.CorruptedGrassEnclosure).Name;
        case CellBlockType.CorruptedForest:
          return TileData.GetTileStats(TILETYPE.CorruptedForestEnclosure).Name;
        default:
          return "NO NAME";
      }
    }

    internal static TILETYPE GetCellBlockToTileType(CellBlockType cellblock)
    {
      switch (cellblock)
      {
        case CellBlockType.Grasslands:
          return TILETYPE.GrassEnclosure;
        case CellBlockType.Forest:
          return TILETYPE.ForestEnclosure;
        case CellBlockType.Savannah:
          return TILETYPE.SavannahEnclosure;
        case CellBlockType.Desert:
          return TILETYPE.DesertEnclosure;
        case CellBlockType.Mountain:
          return TILETYPE.MountainEnclosure;
        case CellBlockType.Arctic:
          return TILETYPE.ArcticEnclosure;
        case CellBlockType.Tropical:
          return TILETYPE.TropicalEnclosure;
        case CellBlockType.FieldPicketFence:
          return TILETYPE.FieldPicketFenceEnclosure;
        case CellBlockType.CorruptedGrass:
          return TILETYPE.CorruptedGrassEnclosure;
        case CellBlockType.CorruptedForest:
          return TILETYPE.CorruptedForestEnclosure;
        default:
          return TILETYPE.None;
      }
    }

    internal static TILETYPE GetCellBlockTypeToPice(
      CellBlockType celltype,
      CellBlockPiece piece)
    {
      switch (celltype)
      {
        case CellBlockType.Grasslands:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Grasslands_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Grasslands_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Grasslands_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Grasslands_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Grasslands_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Grass;
          }
          break;
        case CellBlockType.Forest:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Forest_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Forest_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Forest_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Forest_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Forest_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Forest;
          }
          break;
        case CellBlockType.Savannah:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Savanah_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Savanah_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Savanah_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Savanah_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Savanah_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Savanah;
          }
          break;
        case CellBlockType.Desert:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Desert_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Desert_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Desert_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Desert_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Desert_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Desert;
          }
          break;
        case CellBlockType.Mountain:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Mountain_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Mountain_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Mountain_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Mountain_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Mountain_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Mountain;
          }
          break;
        case CellBlockType.Arctic:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Arctic_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Arctic_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Arctic_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Arctic_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Arctic_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Arctic;
          }
          break;
        case CellBlockType.Tropical:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.Tropical_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.Tropical_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.Tropical_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.Tropical_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.Tropical_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_Tropical;
          }
          break;
        case CellBlockType.FieldPicketFence:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.FieldPicketFence_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.FieldPicketFence_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.FieldPicketFence_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.FieldPicketFence_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.FieldPicketFence_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.Gate_FieldPicketFence;
          }
          break;
        case CellBlockType.CorruptedGrass:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.CorruptedGrass_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.CorruptedGrass_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.CorruptedGrass_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.CorruptedGrass_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.CorruptedGrass_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.CorruptedGrass_Gate;
          }
          break;
        case CellBlockType.CorruptedForest:
          switch (piece)
          {
            case CellBlockPiece.Corner:
              return TILETYPE.CorruptedForest_WallCorner;
            case CellBlockPiece.Wall:
              return TILETYPE.CorruptedForest_WallSide;
            case CellBlockPiece.Floor:
              return TILETYPE.CorruptedForest_Floor;
            case CellBlockPiece.Decorative:
              return TILETYPE.CorruptedForest_Floor;
            case CellBlockPiece.InnerCorner:
              return TILETYPE.CorruptedForest_WallInnerCorner;
            case CellBlockPiece.Gate:
              return TILETYPE.CorruptedForest_Gate;
          }
          break;
        case CellBlockType.GraveYard:
          throw new Exception("FROM PRISON PLANET");
        case CellBlockType.GreenWalls:
          throw new Exception("FROM PRISON PLANET");
      }
      throw new Exception("NO PIRCE RETURNED");
    }

    internal static bool IsThisACellBlock(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.GreenWallCorner:
        case TILETYPE.GreenWallSide:
        case TILETYPE.Grasslands_WallCorner:
        case TILETYPE.Grasslands_WallSide:
        case TILETYPE.Grasslands_Floor:
        case TILETYPE.GraveYard_WallCorner:
        case TILETYPE.GraveYard_WallSide:
        case TILETYPE.GrassEnclosure:
        case TILETYPE.DesertEnclosure:
        case TILETYPE.MountainEnclosure:
        case TILETYPE.ArcticEnclosure:
        case TILETYPE.TropicalEnclosure:
        case TILETYPE.ForestEnclosure:
        case TILETYPE.SavannahEnclosure:
        case TILETYPE.FieldPicketFenceEnclosure:
        case TILETYPE.CorruptedGrassEnclosure:
        case TILETYPE.CorruptedForestEnclosure:
        case TILETYPE.GraveYard:
        case TILETYPE.Forest_WallCorner:
        case TILETYPE.Savanah_WallCorner:
        case TILETYPE.Desert_WallCorner:
        case TILETYPE.Mountain_WallCorner:
        case TILETYPE.Arctic_WallCorner:
        case TILETYPE.Tropical_WallCorner:
        case TILETYPE.CorruptedGrass_WallCorner:
        case TILETYPE.CorruptedForest_WallCorner:
        case TILETYPE.Forest_WallSide:
        case TILETYPE.Savanah_WallSide:
        case TILETYPE.Desert_WallSide:
        case TILETYPE.Mountain_WallSide:
        case TILETYPE.Arctic_WallSide:
        case TILETYPE.Tropical_WallSide:
        case TILETYPE.FieldPicketFence_WallSide:
        case TILETYPE.CorruptedGrass_WallSide:
        case TILETYPE.CorruptedForest_WallSide:
        case TILETYPE.Forest_Floor:
        case TILETYPE.Savanah_Floor:
        case TILETYPE.Desert_Floor:
        case TILETYPE.Mountain_Floor:
        case TILETYPE.Arctic_Floor:
        case TILETYPE.Tropical_Floor:
        case TILETYPE.CorruptedGrass_Floor:
        case TILETYPE.CorruptedForest_Floor:
        case TILETYPE.Rockwall_WallSide:
        case TILETYPE.Mudwall_WallSide:
        case TILETYPE.GrassWall_WallSide:
        case TILETYPE.Grasslands_WallInnerCorner:
        case TILETYPE.Forest_WallInnerCorner:
        case TILETYPE.Savanah_WallInnerCorner:
        case TILETYPE.Desert_WallInnerCorner:
        case TILETYPE.Mountain_WallInnerCorner:
        case TILETYPE.Arctic_WallInnerCorner:
        case TILETYPE.Tropical_WallInnerCorner:
        case TILETYPE.CorruptedGrass_WallInnerCorner:
        case TILETYPE.CorruptedForest_WallInnerCorner:
          return true;
        default:
          return false;
      }
    }

    internal static bool IsThisABrokenGate(TILETYPE Gate)
    {
      switch (Gate)
      {
        case TILETYPE.BrokenGate_Grass:
        case TILETYPE.BrokenGate_Forest:
        case TILETYPE.BrokenGate_Desert:
        case TILETYPE.BrokenGate_Ice:
        case TILETYPE.BrokenGate_Savannah:
        case TILETYPE.BrokenGate_Tropical:
        case TILETYPE.BrokenGate_Mountain:
          return true;
        default:
          return false;
      }
    }

    internal static TILETYPE GetGateTileTypeToBrokenGateTileType(TILETYPE Gate)
    {
      switch (Gate)
      {
        case TILETYPE.Gate_Grass:
          return TILETYPE.BrokenGate_Grass;
        case TILETYPE.Gate_Forest:
          return TILETYPE.BrokenGate_Forest;
        case TILETYPE.Gate_Savanah:
          return TILETYPE.BrokenGate_Savannah;
        case TILETYPE.Gate_Desert:
          return TILETYPE.BrokenGate_Desert;
        case TILETYPE.Gate_Mountain:
          return TILETYPE.BrokenGate_Mountain;
        case TILETYPE.Gate_Arctic:
          return TILETYPE.BrokenGate_Ice;
        case TILETYPE.Gate_Tropical:
          return TILETYPE.BrokenGate_Tropical;
        case TILETYPE.CorruptedGrass_Gate:
          return TILETYPE.BrokenGate_Grass;
        case TILETYPE.CorruptedForest_Gate:
          return TILETYPE.BrokenGate_Forest;
        default:
          return TILETYPE.None;
      }
    }

    internal static TileStats GetTileStats(TILETYPE getthis)
    {
      if (TileData.tilestats == null)
        TileData.tilestats = new TileStats[743];
      if (TileData.tilestats[(int) getthis] == null)
      {
        switch (getthis)
        {
          case TILETYPE.GreenWallSide:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.Floor_GreenGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_GreenGrass, StringID.Floor_GreenGrassDesc);
            break;
          case TILETYPE.Floor_Dirt:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_Dirt, StringID.Floor_DirtDesc);
            break;
          case TILETYPE.Floor_RedCircles:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_RedCircles, StringID.Floor_RedCirclesDesc);
            break;
          case TILETYPE.Floor_GreyBricks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_GreyBricks, StringID.Floor_GreyBricksDesc);
            break;
          case TILETYPE.PowerStation:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Power, 50);
            break;
          case TILETYPE.KitchenZone:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.FoodKitchen, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Research_PrisonPlanet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Research, 1);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Medical:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Medicine, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Security:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Security, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Bank:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Bank, 5);
            break;
          case TILETYPE.Solitary:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 2);
            break;
          case TILETYPE.Janitor:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Janitor, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Farm:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Farm, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.Water:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.Water, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.LifeSupport:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetProduction(ProductionType.LifeSupport, 25);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.HoldingCell:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            TileData.tilestats[(int) getthis].SetConsumption(ProductionType.Power, 5);
            break;
          case TILETYPE.GrassEnclosure:
          case TILETYPE.GrassEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GrassEnclosure, StringID.GrassEnclosureDesc);
            break;
          case TILETYPE.DesertEnclosure:
          case TILETYPE.DesertEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DesertEnclosure, StringID.DesertEnclosureDesc);
            break;
          case TILETYPE.MountainEnclosure:
          case TILETYPE.MountainEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MountainEnclosure, StringID.MountainEnclosureDesc);
            break;
          case TILETYPE.ArcticEnclosure:
          case TILETYPE.ArcticEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ArcticEnclosure, StringID.ArcticEnclosureDesc);
            break;
          case TILETYPE.TropicalEnclosure:
          case TILETYPE.TropicalEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TropicalEnclosure, StringID.TropicalEnclosureDesc);
            break;
          case TILETYPE.ForestEnclosure:
          case TILETYPE.ForestEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ForestEnclosure, StringID.ForestEnclosureDesc);
            break;
          case TILETYPE.SavannahEnclosure:
          case TILETYPE.SavannahEnclosureIcon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SavannahEnclosure, StringID.SavannahEnclosureDesc);
            break;
          case TILETYPE.FieldPicketFenceEnclosure:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FieldPicketFenceEnclosure, StringID.FieldPicketFenceEnclosureDesc);
            break;
          case TILETYPE.CorruptedGrassEnclosure:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CorruptedGrassEnclosure, StringID.CorruptedGrassEnclosureDesc);
            break;
          case TILETYPE.CorruptedForestEnclosure:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CorruptedForestEnclosure, StringID.CorruptedForestEnclosureDesc);
            break;
          case TILETYPE.GraveYard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Cemetary, StringID.Placeholder);
            break;
          case TILETYPE.PinkTreeIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.BlueTreeIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.GoatIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.ResearchIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.TrashCompactorIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.FlowerIAP:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.SmallGiftShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallGiftShop, StringID.SmallGiftShopDesc);
            break;
          case TILETYPE.LionHotDogShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LionHotDogShop, StringID.LionHotDogShopDesc);
            break;
          case TILETYPE.ElephantGiftShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ElephantGiftShop, StringID.ElephantGiftShopDesc);
            break;
          case TILETYPE.IceCreamTruck:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceCreamTruck, StringID.IceCreamTruckDesc);
            break;
          case TILETYPE.BigIceCreamShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BigIceCreamShop, StringID.BigIceCreamShopDesc);
            break;
          case TILETYPE.CoconutShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CoconutShop, StringID.CoconutShopDesc);
            break;
          case TILETYPE.DrinksVendingMachine:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DrinksVendingMachine, StringID.DrinksVendingMachineDesc);
            break;
          case TILETYPE.SnacksVendingMachine:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnacksVendingMachine, StringID.SnacksVendingMachineDesc);
            break;
          case TILETYPE.SignboardFront:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SignboardFront, StringID.SignboardFrontDesc);
            break;
          case TILETYPE.Lamppost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Lamppost, StringID.LamppostDesc);
            break;
          case TILETYPE.GreenDustbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GreenDustbin, StringID.GreenDustbinDesc);
            break;
          case TILETYPE.WhiteDustbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WhiteDustbin, StringID.WhiteDustbinDesc);
            break;
          case TILETYPE.PlantPot:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PlantPot, StringID.PlantPotDesc);
            break;
          case TILETYPE.RedFlag:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RedFlag, StringID.RedFlagDesc);
            break;
          case TILETYPE.RoundFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RoundFountain, StringID.RoundFountainDesc);
            break;
          case TILETYPE.PandaBurgerShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PandaBurgerShop, StringID.PandaBurgerShopDesc);
            break;
          case TILETYPE.BalloonShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BalloonShop, StringID.BalloonShopDesc);
            break;
          case TILETYPE.FlowerPatch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FlowerPatch, StringID.FlowerPatchDesc);
            break;
          case TILETYPE.DangerSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DangerSign, StringID.DangerSignDesc);
            break;
          case TILETYPE.SmallDesertRockDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallDesertRockDeco, StringID.SmallDesertRockDecoDesc);
            break;
          case TILETYPE.DesertRockDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DesertRockDeco, StringID.DesertRockDecoDesc);
            break;
          case TILETYPE.DesertCactusDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DesertCactusDeco, StringID.DesertCactusDecoDesc);
            break;
          case TILETYPE.KangarooPizzaShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.KangarooPizzaShop, StringID.KangarooPizzaShopDesc);
            break;
          case TILETYPE.CottonCandyShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CottonCandyShop, StringID.CottonCandyShopDesc);
            break;
          case TILETYPE.SlushieShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SlushieShop, StringID.SlushieShopDesc);
            break;
          case TILETYPE.ChurroShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ChurroShop, StringID.ChurroShopDesc);
            break;
          case TILETYPE.WoodenToilet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WoodenToilet, StringID.WoodenTolietDesc);
            break;
          case TILETYPE.SnakeHuggingBooth:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnakeHuggingBooth, StringID.SnakeHuggingBoothDesc);
            break;
          case TILETYPE.TreeExhibit:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeExhibit, StringID.TreeExhibitDesc);
            break;
          case TILETYPE.InfoBooth:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.InfoBooth, StringID.InfoBoothDesc);
            break;
          case TILETYPE.BusStop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BusStop, StringID.BusStopDesc);
            break;
          case TILETYPE.BarSignboard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SignboardFront, StringID.BarSignboardDesc);
            break;
          case TILETYPE.Umbrella:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Umbrella, StringID.UmbrellaDesc);
            break;
          case TILETYPE.BigTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BigTree, StringID.BigTreeDesc);
            break;
          case TILETYPE.TikiShelter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TikiShelter, StringID.TikiShelterDesc);
            break;
          case TILETYPE.ZooMap:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ZooMap, StringID.ZooMapDesc);
            break;
          case TILETYPE.BrownBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BrownBench, StringID.BrownBenchDesc);
            break;
          case TILETYPE.WhiteBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WhiteBench, StringID.WhiteBenchDesc);
            break;
          case TILETYPE.NoSwimmingSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.NoSwimmingSign, StringID.NoSwimmingSignDesc);
            break;
          case TILETYPE.PottedPlant:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PlantPot, StringID.PottedPlantDesc);
            break;
          case TILETYPE.ThickSignboard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SignboardFront, StringID.ThickSignboardDesc);
            break;
          case TILETYPE.ElephantFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ElephantFountain, StringID.ElephantFountainDesc);
            break;
          case TILETYPE.FloorLight:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FloorLight, StringID.FloorLightDesc);
            break;
          case TILETYPE.RustyKegShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RustyKegShop, StringID.RustyKegShopDesc);
            break;
          case TILETYPE.PopcornWeaselShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PopcornWeaselShop, StringID.PopcornWeaselShopDesc);
            break;
          case TILETYPE.Nursery:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Nursery, StringID.NurseryDesc);
            break;
          case TILETYPE.QuarantineOffice:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.QuarantineOffice, StringID.QuarantineOfficeDesc);
            break;
          case TILETYPE.VetOffice:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.VetOffice, StringID.VetOfficeDesc);
            break;
          case TILETYPE.ElephantMarbleFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ElephantMarbleFountain, StringID.ElephantMarbleFountainDesc);
            break;
          case TILETYPE.FlamingoHedge:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FlamingoHedge, StringID.FlamingoHedgeDesc);
            break;
          case TILETYPE.GiraffeHedge:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiraffeHedge, StringID.GiraffeHedgeDesc);
            break;
          case TILETYPE.ElephantHedge:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ElephantHedge, StringID.ElephantHedgeDesc);
            break;
          case TILETYPE.MonkeyStatue:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MonkeyStatue, StringID.MonkeyStatueDesc);
            break;
          case TILETYPE.GiantSunFlower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiantSunFlower, StringID.GiantSunFlowerDesc);
            break;
          case TILETYPE.ZooStandee:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ZooStandee, StringID.ZooStandeeDesc);
            break;
          case TILETYPE.PenguinStandee:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PenguinStandee, StringID.PenguinStandeeDesc);
            break;
          case TILETYPE.BearStandee:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BearStandee, StringID.BearStandeeDesc);
            break;
          case TILETYPE.SnakeBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnakeBench, StringID.SnakeBenchDesc);
            break;
          case TILETYPE.ZooTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ZooTree, StringID.ZooTreeDesc);
            break;
          case TILETYPE.SnakeSignpost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnakeSignpost, StringID.SnakeSignpostDesc);
            break;
          case TILETYPE.UmbrellaBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.UmbrellaBench, StringID.UmbrellaBenchDesc);
            break;
          case TILETYPE.MonkeyBanner:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MonkeyBanner, StringID.MonkeyBannerDesc);
            break;
          case TILETYPE.SakuraTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SakuraTree, StringID.SakuraTreeDesc);
            break;
          case TILETYPE.LionPlayground:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LionPlayground, StringID.LionPlaygroundDesc);
            break;
          case TILETYPE.SpringChicken:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SpringChicken, StringID.SpringChickenDesc);
            break;
          case TILETYPE.SpringHorse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SpringHorse, StringID.SpringHorseDesc);
            break;
          case TILETYPE.RockElephant:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RockElephant, StringID.RockElephantDesc);
            break;
          case TILETYPE.YellowBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.YellowBush, StringID.YellowBushDesc);
            break;
          case TILETYPE.RedFlower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RedFlower, StringID.RedFlowerDesc);
            break;
          case TILETYPE.WhiteFlower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WhiteFlower, StringID.WhiteFlowerDesc);
            break;
          case TILETYPE.PurpleFlower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PurpleFlower, StringID.PurpleFlowerDesc);
            break;
          case TILETYPE.PurpleFlowerPatch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PurpleFlowerPatch, StringID.PurpleFlowerPatchDesc);
            break;
          case TILETYPE.PenguinTrashbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PenguinTrashbin, StringID.PenguinTrashbinDesc);
            break;
          case TILETYPE.SealStandee:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SealStandee, StringID.SealStandeeDesc);
            break;
          case TILETYPE.RedShelter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RedShelter, StringID.RedShelterDesc);
            break;
          case TILETYPE.CrocCrossingSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CrocCrossingSign, StringID.CrocCrossingSignDesc);
            break;
          case TILETYPE.MenuSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MenuSign, StringID.MenuSignDesc);
            break;
          case TILETYPE.SmallBarTable:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallBarTable, StringID.SmallBarTableDesc);
            break;
          case TILETYPE.NoPhotoSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.NoPhotoSign, StringID.NoPhotoSignDesc);
            break;
          case TILETYPE.OwlClock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.OwlClock, StringID.OwlClockDesc);
            break;
          case TILETYPE.HeShee:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HeShee, StringID.HeSheeDesc);
            break;
          case TILETYPE.Totem:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Totem, StringID.TotemDesc);
            break;
          case TILETYPE.AztecSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztecSign, StringID.AztecSignDesc);
            break;
          case TILETYPE.AztecTorch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztecTorch, StringID.AztecTorchDesc);
            break;
          case TILETYPE.AztecPlant:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztecPlant, StringID.AztecPlantDesc);
            break;
          case TILETYPE.WoodenTotem:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WoodenTotem, StringID.WoodenTotemDesc);
            break;
          case TILETYPE.StoreRoom:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.StoreRoom, StringID.StoreRoomDesc);
            break;
          case TILETYPE.ArchitectOffice:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ArchitectOffice, StringID.ArchitectOfficeDesc);
            break;
          case TILETYPE.LargeArchitectOffice:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LargeArchitectOffice, StringID.LargeArchitectOfficeDesc);
            break;
          case TILETYPE.GlueFactory:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GlueFactory, StringID.GlueFactoryDesc);
            break;
          case TILETYPE.BuffaloWingFactory:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BuffaloWingFactory, StringID.BuffaloWingFactoryDesc);
            break;
          case TILETYPE.BaconFactory:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BaconFactory, StringID.BaconFactoryDesc);
            break;
          case TILETYPE.Barn:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Barn, StringID.BarnDesc);
            break;
          case TILETYPE.EmptySoilPatch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.EmptySoilPatch, StringID.EmptySoilPatchDesc);
            break;
          case TILETYPE.Wheat_SmallGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WheatFarmPlot, StringID.WheatFarmPlotDesc);
            break;
          case TILETYPE.Wheat_HalfGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WheatFarmPlot, StringID.WheatFarmPlotDesc);
            break;
          case TILETYPE.Wheat_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WheatFarmPlot, StringID.WheatFarmPlotDesc);
            break;
          case TILETYPE.Silo:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Silo, StringID.SiloDesc);
            break;
          case TILETYPE.Floor_OrangeTiles:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_OrangeTiles, StringID.Floor_OrangeTilesDesc);
            break;
          case TILETYPE.Floor_ColorfulBrickTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_ColorfulBrickTile, StringID.Floor_ColorfulBrickTileDesc);
            break;
          case TILETYPE.Floor_BlueCircleTiles:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_BlueCircleTiles, StringID.Floor_BlueCircleTilesDesc);
            break;
          case TILETYPE.Floor_WoodenBoards:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_WoodenBoards, StringID.Floor_WoodenBoardsDesc);
            break;
          case TILETYPE.Floor_MetalDecoTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_MetalDecoTile, StringID.Floor_MetalDecoTileDesc);
            break;
          case TILETYPE.Floor_PawDecoTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_PawDecoTile, StringID.Floor_PawDecoTileDesc);
            break;
          case TILETYPE.PalmTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PalmTree, StringID.PalmTreeDesc);
            break;
          case TILETYPE.GreenTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GreenTree, StringID.GreenTreeDesc);
            break;
          case TILETYPE.LongGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LongGrass, StringID.LongGrassDesc);
            break;
          case TILETYPE.IrisPlantPot:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IrisPlantPot, StringID.IrisPlantPotDesc);
            break;
          case TILETYPE.YellowPlantPot:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.YellowPlantPot, StringID.YellowPlantPotDesc);
            break;
          case TILETYPE.BonsaiPlantPot:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BonsaiPlantPot, StringID.BonsaiPlantPotDesc);
            break;
          case TILETYPE.Ferns:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Ferns, StringID.FernsDesc);
            break;
          case TILETYPE.SmallRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallRock, StringID.SmallRockDesc);
            break;
          case TILETYPE.MediumRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MediumRock, StringID.MediumRockDesc);
            break;
          case TILETYPE.LargeMossyRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LargeMossyRock, StringID.LargeMossyRockDesc);
            break;
          case TILETYPE.TigerPhoto:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TigerPhoto, StringID.TigerPhotoDesc);
            break;
          case TILETYPE.NoticeBoard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.NoticeBoard, StringID.NoticeBoardDesc);
            break;
          case TILETYPE.BeetleGlassExhibit:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BeetleGlassExhibit, StringID.BeetleGlassExhibitDesc);
            break;
          case TILETYPE.ButterflyGlassExhibit:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ButterflyGlassExhibit, StringID.ButterflyGlassExhibitDesc);
            break;
          case TILETYPE.GlassExhibit:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GlassExhibit, StringID.GlassExhibitDesc);
            break;
          case TILETYPE.MiniFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MiniFountain, StringID.Placeholder);
            break;
          case TILETYPE.ElegantTallFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ElegantTallFountain, StringID.Placeholder);
            break;
          case TILETYPE.HedgeArchYellowFlowers:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HedgeArchYellowFlowers, StringID.Placeholder);
            break;
          case TILETYPE.HedgeArchWhiteFlowers:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HedgeArchWhiteFlowers, StringID.Placeholder);
            break;
          case TILETYPE.WhiteMetalVineArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WhiteMetalVineArch, StringID.Placeholder);
            break;
          case TILETYPE.BlackMetalRoseArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BlackMetalRoseArch, StringID.Placeholder);
            break;
          case TILETYPE.PeacockBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PeacockBush, StringID.Placeholder);
            break;
          case TILETYPE.Binoculars:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Binoculars, StringID.Placeholder);
            break;
          case TILETYPE.TwinLamppost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TwinLamppost, StringID.Placeholder);
            break;
          case TILETYPE.CurlLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CurlLampPost, StringID.Placeholder);
            break;
          case TILETYPE.TwinCurlsLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TwinCurlsLampPost, StringID.Placeholder);
            break;
          case TILETYPE.WoodenLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WoodenLampPost, StringID.Placeholder);
            break;
          case TILETYPE.TripletsLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TripletsLampPost, StringID.Placeholder);
            break;
          case TILETYPE.ClassicLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ClassicLampPost, StringID.Placeholder);
            break;
          case TILETYPE.WhiteClassicLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WhiteClassicLampPost, StringID.Placeholder);
            break;
          case TILETYPE.FlamingoLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FlamingoLampPost, StringID.Placeholder);
            break;
          case TILETYPE.SealLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SealLampPost, StringID.Placeholder);
            break;
          case TILETYPE.BallLampPost:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BallLampPost, StringID.Placeholder);
            break;
          case TILETYPE.SwingingBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SwingingBench, StringID.Placeholder);
            break;
          case TILETYPE.GreenGardenBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GreenGardenBench, StringID.Placeholder);
            break;
          case TILETYPE.GreenChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GreenChair, StringID.Placeholder);
            break;
          case TILETYPE.WoodenChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WoodenChair, StringID.Placeholder);
            break;
          case TILETYPE.LongWoodenBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LongWoodenBench, StringID.Placeholder);
            break;
          case TILETYPE.CamelChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CamelChair, StringID.Placeholder);
            break;
          case TILETYPE.Greenhouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Greenhouse, StringID.Placeholder);
            break;
          case TILETYPE.PandaChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PandaChair, StringID.Placeholder);
            break;
          case TILETYPE.ATMMachine:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ATMMachine, StringID.Placeholder);
            break;
          case TILETYPE.SmallSpeaker:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallSpeaker, StringID.Placeholder);
            break;
          case TILETYPE.LargeSpeaker:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LargeSpeaker, StringID.Placeholder);
            break;
          case TILETYPE.RockSpeaker:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RockSpeaker, StringID.Placeholder);
            break;
          case TILETYPE.IceSpeaker:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceSpeaker, StringID.Placeholder);
            break;
          case TILETYPE.KatCoffeeShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.KatCoffeeShop, StringID.Placeholder);
            break;
          case TILETYPE.ShellShackShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ShellShackShop, StringID.Placeholder);
            break;
          case TILETYPE.TacoTruck:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TacoTruck, StringID.Placeholder);
            break;
          case TILETYPE.PretzelShop:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PretzelShop, StringID.Placeholder);
            break;
          case TILETYPE.Slaughterhouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Slaughterhouse, StringID.Placeholder);
            break;
          case TILETYPE.IglooToilet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IglooToliet, StringID.Placeholder);
            break;
          case TILETYPE.LionTrashbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LionTrashbin, StringID.Placeholder);
            break;
          case TILETYPE.BearTrashbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BearTrashbin, StringID.Placeholder);
            break;
          case TILETYPE.Floor_Cobblestone:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_Cobblestone, StringID.Placeholder);
            break;
          case TILETYPE.Floor_PinkSmallTiles:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_PinkSmallTiles, StringID.Placeholder);
            break;
          case TILETYPE.Floor_BrownOctoTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_BrownOctoTile, StringID.Placeholder);
            break;
          case TILETYPE.Floor_BrownOctoTile2:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_BrownOctoTile2, StringID.Placeholder);
            break;
          case TILETYPE.Floor_GreenAndBlueDiamondTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_GreenAndBlueDiamondTile, StringID.Placeholder);
            break;
          case TILETYPE.Floor_BrownSquareTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_BrownSquareTile, StringID.Placeholder);
            break;
          case TILETYPE.PineTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PineTree, StringID.Placeholder);
            break;
          case TILETYPE.BrickToilet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BrickToliet, StringID.Placeholder);
            break;
          case TILETYPE.EggBatteryFarm:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.EggBatteryFarm, StringID.Placeholder);
            break;
          case TILETYPE.ColoredTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ColoredTree, StringID.Placeholder);
            break;
          case TILETYPE.DarkBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DarkBush, StringID.Placeholder);
            break;
          case TILETYPE.Cactus:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Cactus, StringID.Placeholder);
            break;
          case TILETYPE.Floor_Snow:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_Snow, StringID.Placeholder);
            break;
          case TILETYPE.PenguinSignboard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PenguinSignboard, StringID.Placeholder);
            break;
          case TILETYPE.IceArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceArch, StringID.Placeholder);
            break;
          case TILETYPE.SmallIceRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallIceRocks, StringID.Placeholder);
            break;
          case TILETYPE.ThinIceRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ThinIceRocks, StringID.Placeholder);
            break;
          case TILETYPE.IceCrystals:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceCrystals, StringID.Placeholder);
            break;
          case TILETYPE.Snowman:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Snowman, StringID.Placeholder);
            break;
          case TILETYPE.IcyTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IcyTree, StringID.Placeholder);
            break;
          case TILETYPE.LionSnowSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LionSnowSculpture, StringID.Placeholder);
            break;
          case TILETYPE.SealIceSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SealIceSculpture, StringID.Placeholder);
            break;
          case TILETYPE.DeerIceSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DeerIceSculpture, StringID.Placeholder);
            break;
          case TILETYPE.BearIceSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BearIceSculpture, StringID.Placeholder);
            break;
          case TILETYPE.BirdIceSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BirdIceSculpture, StringID.Placeholder);
            break;
          case TILETYPE.GiraffeIceSculpture:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiraffeIceSculpture, StringID.Placeholder);
            break;
          case TILETYPE.BlueGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BlueGrass, StringID.Placeholder);
            break;
          case TILETYPE.IceShelter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceShelter, StringID.Placeholder);
            break;
          case TILETYPE.ZooIceSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ZooIceSign, StringID.Placeholder);
            break;
          case TILETYPE.IceCastle:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceCastle, StringID.Placeholder);
            break;
          case TILETYPE.IceChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.IceChair, StringID.Placeholder);
            break;
          case TILETYPE.GiantTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiantTree, StringID.Placeholder);
            break;
          case TILETYPE.ZooRockSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ZooRockSign, StringID.Placeholder);
            break;
          case TILETYPE.GiantPigBalloon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiantPigBalloon, StringID.Placeholder);
            break;
          case TILETYPE.Bamboo:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Bamboo, StringID.Placeholder);
            break;
          case TILETYPE.Volume_RedPathway:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_RedCircles, StringID.Floor_RedCirclesDesc);
            break;
          case TILETYPE.WaterTower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTower, StringID.Placeholder);
            break;
          case TILETYPE.Cart:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Cart, StringID.Placeholder);
            break;
          case TILETYPE.SmallBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallBush, StringID.Placeholder);
            break;
          case TILETYPE.DeadTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DeadTree, StringID.Placeholder);
            break;
          case TILETYPE.WantedPoster:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WantedPoster, StringID.Placeholder);
            break;
          case TILETYPE.BigRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BigRocks, StringID.Placeholder);
            break;
          case TILETYPE.Pyramid:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Pyramid, StringID.Placeholder);
            break;
          case TILETYPE.SandTower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SandTower, StringID.Placeholder);
            break;
          case TILETYPE.SmallPyramid:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallPyramid, StringID.Placeholder);
            break;
          case TILETYPE.Sphinx:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Sphinx, StringID.Placeholder);
            break;
          case TILETYPE.PalmTreesTall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PalmTreesTall, StringID.Placeholder);
            break;
          case TILETYPE.OldWestShelter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TileBusStop, StringID.Placeholder);
            break;
          case TILETYPE.Anubis:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Anubis, StringID.Placeholder);
            break;
          case TILETYPE.WesternWindmill:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WesternWindmill, StringID.Placeholder);
            break;
          case TILETYPE.Volume_Grass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_GreenGrass, StringID.Floor_GreenGrassDesc);
            break;
          case TILETYPE.Volume_WoodenBoards:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_WoodenBoards, StringID.Floor_WoodenBoardsDesc);
            break;
          case TILETYPE.JungleToliet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.JungleToliet, StringID.Placeholder);
            break;
          case TILETYPE.TreeLogBench:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeLogBench, StringID.Placeholder);
            break;
          case TILETYPE.TreeStump:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeStump, StringID.Placeholder);
            break;
          case TILETYPE.GlowingPlant:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GlowingPlant, StringID.Placeholder);
            break;
          case TILETYPE.RedMushrooms:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RedMushrooms, StringID.Placeholder);
            break;
          case TILETYPE.BrownMushrooms:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BrownMushrooms, StringID.Placeholder);
            break;
          case TILETYPE.LeafyFern:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LeafyFern, StringID.Placeholder);
            break;
          case TILETYPE.LargeFern:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LargeFern, StringID.Placeholder);
            break;
          case TILETYPE.TreeWithVines:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeWithVines, StringID.Placeholder);
            break;
          case TILETYPE.TreeSwing:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeSwing, StringID.Placeholder);
            break;
          case TILETYPE.JungleArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.JungleArch, StringID.Placeholder);
            break;
          case TILETYPE.JungleWaterfall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.JungleWaterfall, StringID.Placeholder);
            break;
          case TILETYPE.MushroomShelter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MushroomShelter, StringID.Placeholder);
            break;
          case TILETYPE.Floor_WoodenPlanks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_WoodenPlanks, StringID.Placeholder);
            break;
          case TILETYPE.Floor_WoodenTrunk:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_WoodenTrunk, StringID.Placeholder);
            break;
          case TILETYPE.Floor_StonePebbles:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_StonePebbles, StringID.Placeholder);
            break;
          case TILETYPE.Volume_WhiteSnow:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_Snow, StringID.Placeholder);
            break;
          case TILETYPE.Volume_Water:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_Water, StringID.Placeholder);
            break;
          case TILETYPE.Volume_WoodenBridge:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_WoodenBridge, StringID.Placeholder);
            break;
          case TILETYPE.WaterTrough_Metal:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.WaterTrough_Metal_Single:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.WaterTrough_Wooden:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.WaterTrough_Wooden_Single:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.RockyWaterfall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RockyWaterfall, StringID.Placeholder);
            break;
          case TILETYPE.ChocolateVendingMachine:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ChocolateVendingMachine, StringID.Placeholder);
            break;
          case TILETYPE.Water_SmallLilyPads:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SmallLilyPads, StringID.Placeholder);
            break;
          case TILETYPE.Water_LargeLilyPads:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_LargeLilyPads, StringID.Placeholder);
            break;
          case TILETYPE.Water_Reeds:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_Reeds, StringID.Placeholder);
            break;
          case TILETYPE.Water_LotusFlower:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_LotusFlower, StringID.Placeholder);
            break;
          case TILETYPE.Water_Rock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallRock, StringID.Placeholder);
            break;
          case TILETYPE.Water_FlatRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FlatRock, StringID.Placeholder);
            break;
          case TILETYPE.Water_WoodenBoards:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Floor_WoodenBoards, StringID.Placeholder);
            break;
          case TILETYPE.Water_Lanturn:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_Lanturn, StringID.Placeholder);
            break;
          case TILETYPE.Water_LightBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_LightBall, StringID.Placeholder);
            break;
          case TILETYPE.Water_BirdStatue:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_BirdStatue, StringID.Placeholder);
            break;
          case TILETYPE.Water_FlappingBirdStatue:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FlappingBirdStatue, StringID.Placeholder);
            break;
          case TILETYPE.Water_FishFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FishFountain, StringID.Placeholder);
            break;
          case TILETYPE.Water_WaterJarFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_WaterJarFountain, StringID.Placeholder);
            break;
          case TILETYPE.Water_Fountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_Fountain, StringID.Placeholder);
            break;
          case TILETYPE.Water_FloatingCrate:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FloatingCrate, StringID.Placeholder);
            break;
          case TILETYPE.Water_FloatingBarrel:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FloatingBarrel, StringID.Placeholder);
            break;
          case TILETYPE.Water_WaterJet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_WaterJet, StringID.Placeholder);
            break;
          case TILETYPE.Water_TreasureChest:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_TreasureChest, StringID.Placeholder);
            break;
          case TILETYPE.Water_SunkenShip:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SunkenShip, StringID.Placeholder);
            break;
          case TILETYPE.Water_IceRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_IceRocks, StringID.Placeholder);
            break;
          case TILETYPE.Water_FlatIce:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FlatIce, StringID.Placeholder);
            break;
          case TILETYPE.Water_IceBoulders:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_IceBoulders, StringID.Placeholder);
            break;
          case TILETYPE.Water_RockBoulders:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_RockBoulders, StringID.Placeholder);
            break;
          case TILETYPE.Water_FloatHouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FloatHouse, StringID.Placeholder);
            break;
          case TILETYPE.WaterTrough_Leaf:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.WaterTrough_TreeTrunk:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.Enrichment_WaterSprinklers:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_WaterSprinklers, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_BlueTrampoline:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Trampoline, StringID.Enrichment_TrampolineDesc);
            break;
          case TILETYPE.Enrichment_ScratchingPostWood:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ScratchingPost, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeRedBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_LargeBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_SmallBlueBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SmallBall, StringID.Enrichment_SmallBallDesc);
            break;
          case TILETYPE.Enrichment_TugRopeToy:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_TugRopeToy, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ChewToyPurpleBone:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ChewToy, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_TunnelGreen:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Tunnel, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_WoodenLogs:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_WoodenLogs, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_YellowRockPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_SmallRockCave:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_SmallRockCave, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_LargeRockCave:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_LargeRockCave, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_HighPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_HighPerch, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_LargeWoodenHouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_LargeWoodenHouse, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_Igloo:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_Igloo, StringID.Placeholder);
            break;
          case TILETYPE.DarkTreeDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DarkTreeDeco, StringID.Placeholder);
            break;
          case TILETYPE.SwingTreeDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SwingTreeDeco, StringID.Placeholder);
            break;
          case TILETYPE.WaterPumpStation:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterPumpStation, StringID.Placeholder);
            break;
          case TILETYPE.Water_Skull:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_Skull, StringID.Placeholder);
            break;
          case TILETYPE.Water_CannonballJet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_CannonballJet, StringID.Placeholder);
            break;
          case TILETYPE.Water_WaterMill:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_WaterMill, StringID.Placeholder);
            break;
          case TILETYPE.Water_FrogFountain:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_FrogFountain, StringID.Placeholder);
            break;
          case TILETYPE.Water_WaterLanturn:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_WaterLanturn, StringID.Placeholder);
            break;
          case TILETYPE.GiantBearBalloon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiantBearBalloon, StringID.Placeholder);
            break;
          case TILETYPE.LightHouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LightHouse, StringID.Placeholder);
            break;
          case TILETYPE.Water_IceArchRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_IceArchRock, StringID.Placeholder);
            break;
          case TILETYPE.Water_MangroveTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_MangroveTree, StringID.Placeholder);
            break;
          case TILETYPE.Water_SmallMangroveTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SmallMangroveTree, StringID.Placeholder);
            break;
          case TILETYPE.Water_GrassyRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_GrassyRock, StringID.Placeholder);
            break;
          case TILETYPE.Water_SmallGrassyRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SmallGrassyRock, StringID.Placeholder);
            break;
          case TILETYPE.Water_DeerScare:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_DeerScare, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_Metal:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_Metal_Single:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_Wooden:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_Wooden_Single:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_Leaf:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.EmptyWaterTrough_TreeTrunk:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.CorruptedDirtTile:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.CorruptedTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.CorruptedDeadTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.CorruptedBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.CorruptedBushSmall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.GiraffeAirDancer:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.GiraffeAirDancer, StringID.Placeholder);
            break;
          case TILETYPE.SnakeAirDancer:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnakeAirDancer, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_MossyRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_MossyRock, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_HangingCarTire:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_CarTire, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_HighWoodBeamPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_HighPerch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_RockCliff:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_RockCliff, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_RockPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_SaltBlock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SaltBlock, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_MirrorRect:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Mirror, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ScentMarkerGrey:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ScentMarker, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_TugBallJollyBallYellow:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_JollyBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeCardboardBox:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_CardboardBox, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ScentMarkerGreen:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ScentMarker, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ScentMarkerBrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ScentMarker, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_MirrorRound:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Mirror, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeWhiteBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_LargeBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeGreenBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_LargeBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeYellowBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_LargeBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LargeBlueBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_LargeBall, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_TunnelWoodenLog:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Tunnel, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_PinkTrampoline:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Trampoline, StringID.Enrichment_TrampolineDesc);
            break;
          case TILETYPE.Enrichment_FlatCarTire:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_CarTire, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ChewToyBrownBone:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ChewToy, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ChewToyRope:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ChewToy, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_SmallCyanBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SmallBall, StringID.Enrichment_SmallBallDesc);
            break;
          case TILETYPE.Enrichment_SmallGreenBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SmallBall, StringID.Enrichment_SmallBallDesc);
            break;
          case TILETYPE.Enrichment_SmallRedBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SmallBall, StringID.Enrichment_SmallBallDesc);
            break;
          case TILETYPE.Enrichment_SmallPinkBall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_SmallBall, StringID.Enrichment_SmallBallDesc);
            break;
          case TILETYPE.WaterTrough_Rock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.EmptyWaterTrough_Rock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.WaterTrough_IceRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.WaterTroughDesc);
            break;
          case TILETYPE.EmptyWaterTrough_IceRock:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WaterTrough, StringID.Placeholder);
            break;
          case TILETYPE.Shelter_IceRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Shelter_IceRocks, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_IceCliff:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_RockCliff, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_BrownCliff:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_RockCliff, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_ScratchingPoleTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_ScratchingPost, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_BrownRockPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_LogPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_NetPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_WoodenBeam2:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_HighPerch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_WoodenBeam3:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_HighPerch, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_CardboardBox2:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_CardboardBox, StringID.Placeholder);
            break;
          case TILETYPE.Enrichment_TreeHighPerch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Enrichment_Perch, StringID.Placeholder);
            break;
          case TILETYPE.Water_SunkenRocksLarge:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SunkenRocksLarge, StringID.Placeholder);
            break;
          case TILETYPE.Water_SunkenRocksMed:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SunkenRocksMed, StringID.Placeholder);
            break;
          case TILETYPE.Water_SunkenRocksSmall:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Water_SunkenRocksSmall, StringID.Placeholder);
            break;
          case TILETYPE.WindTurbine:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WindTurbine, StringID.Placeholder);
            break;
          case TILETYPE.SolarPanel:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SolarPanel, StringID.Placeholder);
            break;
          case TILETYPE.RecyclingBin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RecyclingBin, StringID.Placeholder);
            break;
          case TILETYPE.CrossSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CrossSign, StringID.Placeholder);
            break;
          case TILETYPE.Volume_DarkGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_DarkGrass, StringID.Placeholder);
            break;
          case TILETYPE.Volume_YellowGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_YellowGrass, StringID.Placeholder);
            break;
          case TILETYPE.Volume_Sand:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_Sand, StringID.Placeholder);
            break;
          case TILETYPE.MilkBatteryFarm:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MilkBatteryFarm, StringID.Placeholder);
            break;
          case TILETYPE.HelicopterRide:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HelicopterRide, StringID.Placeholder);
            break;
          case TILETYPE.Caravan:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Caravan, StringID.Placeholder);
            break;
          case TILETYPE.OldWestToliet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.OldWestToliet, StringID.Placeholder);
            break;
          case TILETYPE.WestBarrel:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WestBarrel, StringID.Placeholder);
            break;
          case TILETYPE.WestWoodenBox:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WestWoodenBox, StringID.Placeholder);
            break;
          case TILETYPE.WoodenTrashbin:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WoodenTrashbin, StringID.Placeholder);
            break;
          case TILETYPE.WesternArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WesternArch, StringID.Placeholder);
            break;
          case TILETYPE.OrangeStoneArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.OrangeStoneArch, StringID.Placeholder);
            break;
          case TILETYPE.BoneArch:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BoneArch, StringID.Placeholder);
            break;
          case TILETYPE.BoneDeco:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BoneDeco, StringID.Placeholder);
            break;
          case TILETYPE.SmallCactus:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallCactus, StringID.Placeholder);
            break;
          case TILETYPE.ArrowSignboard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ArrowSignboard, StringID.Placeholder);
            break;
          case TILETYPE.SmallGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SmallGrass, StringID.Placeholder);
            break;
          case TILETYPE.WestWoodenBoard:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.WestWoodenBoard, StringID.Placeholder);
            break;
          case TILETYPE.Volume_SoilPlot:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.EmptySoilPatch, StringID.Placeholder);
            break;
          case TILETYPE.Carrots_SmallGrown:
          case TILETYPE.Carrots_HalfGrown:
          case TILETYPE.Carrots_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Carrots, StringID.Placeholder);
            break;
          case TILETYPE.Cabbage_SmallGrown:
          case TILETYPE.Cabbage_HalfGrown:
          case TILETYPE.Cabbage_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Cabbage, StringID.Placeholder);
            break;
          case TILETYPE.Corn_SmallGrown:
          case TILETYPE.Corn_HalfGrown:
          case TILETYPE.Corn_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Corn, StringID.Placeholder);
            break;
          case TILETYPE.Potato_SmallGrown:
          case TILETYPE.Potato_HalfGrown:
          case TILETYPE.Potato_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Potato, StringID.Placeholder);
            break;
          case TILETYPE.Watermelon_SmallGrown:
          case TILETYPE.Watermelon_HalfGrown:
          case TILETYPE.Watermelon_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Watermelon, StringID.Placeholder);
            break;
          case TILETYPE.Grass_SmallGrown:
          case TILETYPE.Grass_HalfGrown:
          case TILETYPE.Grass_FullGrown:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Grass, StringID.Placeholder);
            break;
          case TILETYPE.TreeWithLights:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TreeWithLights, StringID.Placeholder);
            break;
          case TILETYPE.CarpStreamers:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CarpStreamers, StringID.Placeholder);
            break;
          case TILETYPE.HorizonValleyZooBalloon:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HorizonValleyZooBalloon, StringID.Placeholder);
            break;
          case TILETYPE.PineTreeDark:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.PineTreeDark, StringID.Placeholder);
            break;
          case TILETYPE.ArticBush:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ArticBush, StringID.Placeholder);
            break;
          case TILETYPE.DarkSmallPlant:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DarkSmallPlant, StringID.Placeholder);
            break;
          case TILETYPE.DeadWinterTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DeadWinterTree, StringID.Placeholder);
            break;
          case TILETYPE.TallRoofHouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.TallRoofHouse, StringID.Placeholder);
            break;
          case TILETYPE.DesertTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DesertTree, StringID.Placeholder);
            break;
          case TILETYPE.CactusLong:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CactusLong, StringID.Placeholder);
            break;
          case TILETYPE.AztacTempleFloor:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacTempleFloor, StringID.Placeholder);
            break;
          case TILETYPE.AztacSnakeStatue:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacSnakeStatue, StringID.Placeholder);
            break;
          case TILETYPE.AztacTemple:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacTemple, StringID.Placeholder);
            break;
          case TILETYPE.Volume_LightMud:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_LightMud, StringID.Placeholder);
            break;
          case TILETYPE.Volume_LightGrass:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Volume_LightGrass, StringID.Placeholder);
            break;
          case TILETYPE.AztacToliet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacToliet, StringID.Placeholder);
            break;
          case TILETYPE.AztacMap:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacMap, StringID.Placeholder);
            break;
          case TILETYPE.AztacChair:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacChair, StringID.Placeholder);
            break;
          case TILETYPE.BigMountainRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BigMountainRocks, StringID.Placeholder);
            break;
          case TILETYPE.OrangeLargeRocks:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.OrangeLargeRocks, StringID.Placeholder);
            break;
          case TILETYPE.AztacTempleGate:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AztacTempleGate, StringID.Placeholder);
            break;
          case TILETYPE.AsianToliet:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AsianToliet, StringID.Placeholder);
            break;
          case TILETYPE.AsianGate:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AsianGate, StringID.Placeholder);
            break;
          case TILETYPE.AsianPavillion:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AsianPavillion, StringID.Placeholder);
            break;
          case TILETYPE.AsianLight:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AsianLight, StringID.Placeholder);
            break;
          case TILETYPE.CoinSquasher:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CoinSquasher, StringID.Placeholder);
            break;
          case TILETYPE.RockyZooSign:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RockyZooSign, StringID.Placeholder);
            break;
          case TILETYPE.MediumStones:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MediumStones, StringID.Placeholder);
            break;
          case TILETYPE.LightGreenTree:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.LightGreenTree, StringID.Placeholder);
            break;
          case TILETYPE.ImpossibleBuilding:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.ImpossibleBuilding, StringID.Placeholder);
            break;
          case TILETYPE.DNABuilding:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.DNABuilding, StringID.Placeholder);
            break;
          case TILETYPE.MeatProcessor:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.MeatProcessor, StringID.Placeholder);
            break;
          case TILETYPE.AnimalColosseum:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AnimalColosseum, StringID.Placeholder);
            break;
          case TILETYPE.Incinerator:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Incinerator, StringID.Placeholder);
            break;
          case TILETYPE.CrocHandbagFactory:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.CrocHandbagFactory, StringID.Placeholder);
            break;
          case TILETYPE.SnakeSkinFactory:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SnakeSkinFactory, StringID.Placeholder);
            break;
          case TILETYPE.ManagmentOffice_BrownWood:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.ManagmentOffice_SimpleBlack:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.ManagmentOffice_BlueCottage:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.ManagmentOffice_Rabbit:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.Storeroom_BrownWood:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.Storeroom_Victorian:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.Storeroom_Monkey:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
          case TILETYPE.SurveillanceBuilding:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.SurveillanceBuilding, StringID.Placeholder);
            break;
          case TILETYPE.HotAirBalloonRide:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.HotAirBalloonRide, StringID.Placeholder);
            break;
          case TILETYPE.Farmhouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Farmhouse, StringID.Placeholder);
            break;
          case TILETYPE.BeerBrewery:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.BeerBrewery, StringID.Placeholder);
            break;
          case TILETYPE.Windmill:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Windmill, StringID.Placeholder);
            break;
          case TILETYPE.FarmProcessor:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.FarmProcessor, StringID.Placeholder);
            break;
          case TILETYPE.RecyclingCenter:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RecyclingCenter, StringID.Placeholder);
            break;
          case TILETYPE.AnimalRehabilitationBuilding:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.AnimalRehabilitationBuilding, StringID.Placeholder);
            break;
          case TILETYPE.Warehouse:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Warehouse, StringID.Placeholder);
            break;
          case TILETYPE.RainCollectionBuilding:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.RainCollectionBuilding, StringID.Placeholder);
            break;
          default:
            TileData.tilestats[(int) getthis] = new TileStats(StringID.Placeholder, StringID.Placeholder);
            break;
        }
      }
      return TileData.tilestats[(int) getthis];
    }

    private static void SetFloorVolumeTileData(
      int FloorX,
      int FloorY,
      TILETYPE tiletype,
      int LongerYRect = 0,
      int IncreasedSpace = 0)
    {
      TileData.tileinfo[(int) tiletype] = new TileInfo(new Rectangle(FloorX, FloorY, 16, 16), BUILDINGTYPE.Floor, false, LockToThisRotation: 0, _DrawTexture: AssetContainer.EnvironmentSheetHolder, TotalRotations: 51);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY - 16, 16, 16), LockToThisRotation: 1);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 16, FloorY, 16, 16), LockToThisRotation: 2);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX - 16, FloorY, 16, 16 + LongerYRect), LockToThisRotation: 3);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 16, 16, 16), LockToThisRotation: 4);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX - 16, FloorY - 16, 16, 16), LockToThisRotation: 5);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 16, FloorY - 16, 16, 16), LockToThisRotation: 6);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX - 16, FloorY + 16, 16, 16 + LongerYRect), LockToThisRotation: 7);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 16, FloorY + 16, 16, 16 + LongerYRect), LockToThisRotation: 8);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 16, FloorY + 34 + IncreasedSpace, 16, 16), LockToThisRotation: 9);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 34 + IncreasedSpace, 16, 16), LockToThisRotation: 10);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 32, FloorY + 34 + IncreasedSpace, 16, 16), LockToThisRotation: 11);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX - 16, FloorY + 34 + IncreasedSpace, 16, 16), LockToThisRotation: 12);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 68, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 13);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 34, FloorY, 16, 16), LockToThisRotation: 14);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 84, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 15);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 52, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 16);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 34, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 17);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 34, FloorY + 16, 16, 16), LockToThisRotation: 18);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 102, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 19);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 52, FloorY + 1 + IncreasedSpace, 16, 16), LockToThisRotation: 20);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 68, FloorY + 1 + IncreasedSpace, 16, 16), LockToThisRotation: 21);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 120, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 22);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 136, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 23);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 52, FloorY + 19 + IncreasedSpace, 16, 16), LockToThisRotation: 24);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 52, FloorY + 35 + IncreasedSpace, 16, 16), LockToThisRotation: 25);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 70, FloorY + 19 + IncreasedSpace, 16, 16), LockToThisRotation: 26);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 70, FloorY + 35 + IncreasedSpace, 16, 16), LockToThisRotation: 27);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 88, FloorY + 2 + IncreasedSpace, 16, 16), LockToThisRotation: 28);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 104, FloorY + 2 + IncreasedSpace, 16, 16), LockToThisRotation: 29);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 88, FloorY + 20 + IncreasedSpace, 16, 16 + LongerYRect), LockToThisRotation: 31);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 104, FloorY + 20 + IncreasedSpace, 16, 16 + LongerYRect), LockToThisRotation: 30);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX - 16, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 32);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 2, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 33);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 20, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 34);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 38, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 35);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 56, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 36);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 74, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 37);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 92, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 38);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 110, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 39);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 128, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 40);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 146, FloorY - 34 - IncreasedSpace, 16, 16), LockToThisRotation: 41);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 154, FloorY - 16, 16, 16), LockToThisRotation: 42);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 122, FloorY + 2 + IncreasedSpace, 16, 16), LockToThisRotation: 43);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 140, FloorY + 2 + IncreasedSpace, 16, 16), LockToThisRotation: 44);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 140, FloorY + 20 + IncreasedSpace, 16, 16), LockToThisRotation: 45);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 122, FloorY + 20 + IncreasedSpace, 16, 16 + LongerYRect), LockToThisRotation: 46);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 164, FloorY - 34 - IncreasedSpace, 16, 16 + LongerYRect), LockToThisRotation: 47);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 172, FloorY - 16, 16, 16 + LongerYRect), LockToThisRotation: 48);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 158, FloorY + 2 + IncreasedSpace, 16, 16), LockToThisRotation: 49);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX + 158, FloorY + 20 + IncreasedSpace, 16, 16), LockToThisRotation: 50);
    }

    private static void SetAnimatedtFloorVolumeTileData(
      int FloorX,
      int FloorY,
      TILETYPE tiletype,
      int TotalFrames,
      float FrameRate)
    {
      TileData.tileinfo[(int) tiletype] = new TileInfo(new Rectangle(FloorX, FloorY, 16, 16), BUILDINGTYPE.Floor, false, LockToThisRotation: 0, _DrawTexture: AssetContainer.EnvironmentSheetHolder, TotalRotations: 51);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 18, 16, 16), LockToThisRotation: 1);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 116, 16, 16), LockToThisRotation: 2);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 67, 16, 16), LockToThisRotation: 3);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 33, 16, 16), LockToThisRotation: 4);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 51, 16, 16), LockToThisRotation: 5);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 100, 16, 16), LockToThisRotation: 6);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 82, 16, 16), LockToThisRotation: 7);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 131, 16, 16), LockToThisRotation: 8);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 199, 16, 16), LockToThisRotation: 9);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 165, 16, 16), LockToThisRotation: 10);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 183, 16, 16), LockToThisRotation: 11);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 149, 16, 16), LockToThisRotation: 12);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 253, 16, 16), LockToThisRotation: 13);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 235, 16, 16), LockToThisRotation: 14);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 289, 16, 16), LockToThisRotation: 15);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 271, 16, 16), LockToThisRotation: 16);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 325, 16, 16), LockToThisRotation: 17);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 307, 16, 16), LockToThisRotation: 18);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 217, 16, 16), LockToThisRotation: 19);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 697, 16, 16), LockToThisRotation: 20);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 733, 16, 16), LockToThisRotation: 21);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 715, 16, 16), LockToThisRotation: 22);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 751, 16, 16), LockToThisRotation: 23);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 805, 16, 16), LockToThisRotation: 24);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 823, 16, 16), LockToThisRotation: 25);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 841, 16, 16), LockToThisRotation: 26);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 859, 16, 16), LockToThisRotation: 27);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 397, 16, 16), LockToThisRotation: 28);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 361, 16, 16), LockToThisRotation: 29);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 415, 16, 16), LockToThisRotation: 31);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 379, 16, 16), LockToThisRotation: 30);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 343, 16, 16), LockToThisRotation: 32);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 607, 16, 16), LockToThisRotation: 33);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 643, 16, 16), LockToThisRotation: 34);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 625, 16, 16), LockToThisRotation: 35);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 589, 16, 16), LockToThisRotation: 36);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 501, 16, 16), LockToThisRotation: 37);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 553, 16, 16), LockToThisRotation: 38);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 535, 16, 16), LockToThisRotation: 39);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 877, 16, 16), LockToThisRotation: 40);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 571, 16, 16), LockToThisRotation: 41);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 517, 16, 16), LockToThisRotation: 42);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 769, 16, 16), LockToThisRotation: 43);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 787, 16, 16), LockToThisRotation: 44);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 661, 16, 16), LockToThisRotation: 45);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 679, 16, 16), LockToThisRotation: 46);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 715, 16, 16), LockToThisRotation: 47);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 751, 16, 16), LockToThisRotation: 48);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 733, 16, 16), LockToThisRotation: 49);
      TileData.tileinfo[(int) tiletype].AddRotationVariant(new Rectangle(FloorX, FloorY + 697, 16, 16), LockToThisRotation: 50);
    }

    public static bool IsSlowTervelator(TILETYPE tiletype)
    {
      if (TileData.Trevelators == null)
        TileData.Trevelators = new HashSet<TILETYPE>();
      return TileData.Trevelators.Contains(tiletype);
    }

    internal static bool IsThisEnrichmentItemMsking(TILETYPE tiletype)
    {
      if (TileData.MaskingItems == null)
      {
        TileData.MaskingItems = new HashSet<TILETYPE>();
        TileData.MaskingItems.Add(TILETYPE.Enrichment_LargeCardboardBox);
        TileData.MaskingItems.Add(TILETYPE.Enrichment_CardboardBox2);
      }
      return TileData.MaskingItems.Contains(tiletype);
    }

    public static bool IsSlowFloor(TILETYPE tiletype)
    {
      if (TileData.SlowFloors == null)
      {
        TileData.SlowFloors = new HashSet<TILETYPE>();
        TileData.SlowFloors.Add(TILETYPE.EMPTY_DIRT_WALKABLE_TILE);
        TileData.SlowFloors.Add(TILETYPE.Floor_GreenGrass);
        TileData.SlowFloors.Add(TILETYPE.Floor_Dirt);
        TileData.SlowFloors.Add(TILETYPE.CorruptedGreenGrass_Floor);
        TileData.SlowFloors.Add(TILETYPE.Desert_Floor);
        TileData.SlowFloors.Add(TILETYPE.Floor_Snow);
        TileData.SlowFloors.Add(TILETYPE.Volume_Grass);
        TileData.SlowFloors.Add(TILETYPE.Grasslands_Floor);
        TileData.SlowFloors.Add(TILETYPE.Arctic_Floor);
        TileData.SlowFloors.Add(TILETYPE.Mountain_Floor);
        TileData.SlowFloors.Add(TILETYPE.Forest_Floor);
        TileData.SlowFloors.Add(TILETYPE.Desert_Floor);
        TileData.SlowFloors.Add(TILETYPE.Savanah_Floor);
        TileData.SlowFloors.Add(TILETYPE.Tropical_Floor);
      }
      return TileData.SlowFloors.Contains(tiletype);
    }

    public static bool ThisIsAMeaningfullBuilding(TILETYPE tiletype)
    {
      if (TileData.MeaningfulStructures == null)
      {
        TileData.IsThisAShopWithShopStats(tiletype);
        TileData.IsThisaToilet(tiletype);
        TileData.IsThisABin(tiletype);
        TileData.IsAShopOrProfitMakingThing(tiletype);
        TileData.IsThisAnATM(tiletype);
        TileData.IsThisABench(tiletype);
        TileData.IsThisASignBoard(tiletype);
        TileData.MeaningfulStructures = new HashSet<TILETYPE>();
        TileData.MeaningfulStructures.Add(TILETYPE.Logo);
        TileData.MeaningfulStructures.Add(TILETYPE.ZooEntrance_Deer);
        TileData.MeaningfulStructures.Add(TILETYPE.ZooEntrance_Modern);
        TileData.MeaningfulStructures.Add(TILETYPE.ZooEntrance_Cliff);
        TileData.MeaningfulStructures.Add(TILETYPE.ArchitectOffice);
        TileData.MeaningfulStructures.Add(TILETYPE.StoreRoom);
        TileData.MeaningfulStructures.Add(TILETYPE.Storeroom_BrownWood);
        TileData.MeaningfulStructures.Add(TILETYPE.Storeroom_Victorian);
        TileData.MeaningfulStructures.Add(TILETYPE.Storeroom_Monkey);
        TileData.MeaningfulStructures.Add(TILETYPE.ManagmentOffice_BrownWood);
        TileData.MeaningfulStructures.Add(TILETYPE.ManagmentOffice_SimpleBlack);
        TileData.MeaningfulStructures.Add(TILETYPE.ManagmentOffice_BlueCottage);
        TileData.MeaningfulStructures.Add(TILETYPE.ManagmentOffice_Rabbit);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.ShopsWithStats);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.Toilets);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.TrashBins);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.ProfitMaking_NonShop);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.ATMs);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.Benches);
        TileData.MeaningfulStructures.UnionWith((IEnumerable<TILETYPE>) TileData.SignBoards);
      }
      return TileData.MeaningfulStructures.Contains(tiletype);
    }

    internal static bool IsThisAFacility(TILETYPE tiletype)
    {
      if (TileData.Facilities == null)
      {
        TileData.Facilities = new HashSet<TILETYPE>();
        TileData.Facilities.Add(TILETYPE.Incinerator);
        TileData.Facilities.Add(TILETYPE.MeatProcessor);
        TileData.Facilities.Add(TILETYPE.FarmProcessor);
        TileData.Facilities.Add(TILETYPE.VetOffice);
        TileData.Facilities.Add(TILETYPE.QuarantineOffice);
        TileData.Facilities.Add(TILETYPE.Farmhouse);
        TileData.Facilities.Add(TILETYPE.Slaughterhouse);
        TileData.Facilities.Add(TILETYPE.Warehouse);
      }
      return TileData.Facilities.Contains(tiletype);
    }

    internal static bool ThisIsADynamicEnrichmentItem(
      TILETYPE tiletype,
      out ENRICHMENTBEHAVIOUR enrchmentbehaviour)
    {
      bool flag = false;
      enrchmentbehaviour = ENRICHMENTBEHAVIOUR.Count;
      switch (EnrichmentData.GetTILETYPEToEnrichmentClass(tiletype))
      {
        case ENRICHMENTCLASS.Trampoline:
          flag = true;
          enrchmentbehaviour = ENRICHMENTBEHAVIOUR.Trampoline;
          break;
        case ENRICHMENTCLASS.LargeBall:
        case ENRICHMENTCLASS.SmallBall:
          flag = true;
          enrchmentbehaviour = ENRICHMENTBEHAVIOUR.Ball;
          break;
        case ENRICHMENTCLASS.BoneToy:
        case ENRICHMENTCLASS.Hats:
          flag = true;
          enrchmentbehaviour = ENRICHMENTBEHAVIOUR.AnimalAttachment;
          break;
        case ENRICHMENTCLASS.WoodenLogs:
        case ENRICHMENTCLASS.CarTire:
        case ENRICHMENTCLASS.RockPerch:
        case ENRICHMENTCLASS.HighWoodBeamPerch:
        case ENRICHMENTCLASS.RockCliff_Perch:
          flag = true;
          enrchmentbehaviour = ENRICHMENTBEHAVIOUR.Perch;
          break;
      }
      return flag;
    }

    internal static bool IsThisAShopWithShopStats(TILETYPE tiletype)
    {
      if (TileData.ShopsWithStats == null)
      {
        TileData.ShopsWithStats = new HashSet<TILETYPE>();
        TileData.ShopsWithStats.Add(TILETYPE.LionHotDogShop);
        TileData.ShopsWithStats.Add(TILETYPE.ElephantGiftShop);
        TileData.ShopsWithStats.Add(TILETYPE.SmallGiftShop);
        TileData.ShopsWithStats.Add(TILETYPE.IceCreamTruck);
        TileData.ShopsWithStats.Add(TILETYPE.BigIceCreamShop);
        TileData.ShopsWithStats.Add(TILETYPE.CoconutShop);
        TileData.ShopsWithStats.Add(TILETYPE.PandaBurgerShop);
        TileData.ShopsWithStats.Add(TILETYPE.BalloonShop);
        TileData.ShopsWithStats.Add(TILETYPE.ChurroShop);
        TileData.ShopsWithStats.Add(TILETYPE.SlushieShop);
        TileData.ShopsWithStats.Add(TILETYPE.DrinksVendingMachine);
        TileData.ShopsWithStats.Add(TILETYPE.SnacksVendingMachine);
        TileData.ShopsWithStats.Add(TILETYPE.KangarooPizzaShop);
        TileData.ShopsWithStats.Add(TILETYPE.CottonCandyShop);
        TileData.ShopsWithStats.Add(TILETYPE.ChocolateVendingMachine);
        TileData.ShopsWithStats.Add(TILETYPE.KatCoffeeShop);
        TileData.ShopsWithStats.Add(TILETYPE.PopcornWeaselShop);
        TileData.ShopsWithStats.Add(TILETYPE.RustyKegShop);
        TileData.ShopsWithStats.Add(TILETYPE.ShellShackShop);
        TileData.ShopsWithStats.Add(TILETYPE.PretzelShop);
        TileData.ShopsWithStats.Add(TILETYPE.TacoTruck);
      }
      return TileData.ShopsWithStats.Contains(tiletype);
    }

    internal static bool IsForThirst(TILETYPE tiletype)
    {
      if (TileData.Drinks == null)
      {
        TileData.Drinks = new HashSet<TILETYPE>();
        TileData.Drinks.Add(TILETYPE.CoconutShop);
        TileData.Drinks.Add(TILETYPE.DrinksVendingMachine);
        TileData.Drinks.Add(TILETYPE.SlushieShop);
        TileData.Drinks.Add(TILETYPE.RustyKegShop);
        TileData.Drinks.Add(TILETYPE.KatCoffeeShop);
      }
      return TileData.Drinks.Contains(tiletype);
    }

    internal static bool ThisShopHasNoEmplyees(TILETYPE tiletype) => tiletype == TILETYPE.SnacksVendingMachine || tiletype == TILETYPE.DrinksVendingMachine || tiletype == TILETYPE.SnacksVendingMachine;

    internal static bool IsForSouvenir(TILETYPE tiletype)
    {
      if (TileData.SouvenirShop == null)
      {
        TileData.SouvenirShop = new HashSet<TILETYPE>();
        TileData.SouvenirShop.Add(TILETYPE.BalloonShop);
        TileData.SouvenirShop.Add(TILETYPE.ElephantGiftShop);
        TileData.SouvenirShop.Add(TILETYPE.SmallGiftShop);
      }
      if (TileData.ProfitMaking_NonShop == null)
        TileData.IsAShopOrProfitMakingThing(tiletype);
      return TileData.ProfitMaking_NonShop.Contains(tiletype) || TileData.SouvenirShop.Contains(tiletype);
    }

    internal static bool IsForFood(TILETYPE tiletype)
    {
      if (TileData.Food == null)
      {
        TileData.Food = new HashSet<TILETYPE>();
        TileData.Food.Add(TILETYPE.LionHotDogShop);
        TileData.Food.Add(TILETYPE.IceCreamTruck);
        TileData.Food.Add(TILETYPE.BigIceCreamShop);
        TileData.Food.Add(TILETYPE.PandaBurgerShop);
        TileData.Food.Add(TILETYPE.ChurroShop);
        TileData.Food.Add(TILETYPE.SnacksVendingMachine);
        TileData.Food.Add(TILETYPE.KangarooPizzaShop);
        TileData.Food.Add(TILETYPE.CottonCandyShop);
        TileData.Food.Add(TILETYPE.PopcornWeaselShop);
        TileData.Food.Add(TILETYPE.ShellShackShop);
        TileData.Food.Add(TILETYPE.PretzelShop);
        TileData.Food.Add(TILETYPE.TacoTruck);
        TileData.Food.Add(TILETYPE.ChocolateVendingMachine);
      }
      return TileData.Food.Contains(tiletype);
    }

    internal static bool IsCooling(TILETYPE tiletype)
    {
      if (TileData.Cooling == null)
      {
        TileData.Cooling = new HashSet<TILETYPE>();
        TileData.Cooling.Add(TILETYPE.IceCreamTruck);
        TileData.Cooling.Add(TILETYPE.BigIceCreamShop);
        TileData.Cooling.Add(TILETYPE.SlushieShop);
      }
      return TileData.Cooling.Contains(tiletype);
    }

    internal static bool IsAShopOrProfitMakingThing(TILETYPE tiletype)
    {
      if (TileData.ProfitMaking_NonShop == null)
      {
        TileData.ProfitMaking_NonShop = new HashSet<TILETYPE>();
        TileData.ProfitMaking_NonShop.Add(TILETYPE.SnakeHuggingBooth);
        TileData.ProfitMaking_NonShop.Add(TILETYPE.TigerPhoto);
      }
      return TileData.ProfitMaking_NonShop.Contains(tiletype) || TileData.IsThisAShopWithShopStats(tiletype);
    }

    internal static bool IsThisAnyKindOfDeco(TILETYPE tiletype) => CategoryData.IsThisADeocration(tiletype) || CategoryData.IsThisAWaterDeco(tiletype) || CategoryData.IsThisANatireDeocration(tiletype);

    internal static bool IsThisanArchitectOffice(TILETYPE tiletype) => tiletype == TILETYPE.ArchitectOffice || tiletype == TILETYPE.LargeArchitectOffice;

    internal static bool IsThisaToilet(TILETYPE tiletype)
    {
      if (TileData.Toilets == null)
      {
        TileData.Toilets = new HashSet<TILETYPE>();
        TileData.Toilets.Add(TILETYPE.WoodenToilet);
        TileData.Toilets.Add(TILETYPE.IglooToilet);
        TileData.Toilets.Add(TILETYPE.BrickToilet);
        TileData.Toilets.Add(TILETYPE.JungleToliet);
        TileData.Toilets.Add(TILETYPE.OldWestToliet);
        TileData.Toilets.Add(TILETYPE.AztacToliet);
        TileData.Toilets.Add(TILETYPE.AsianToliet);
        TileData.Toilets.Add(TILETYPE.CorruptedWoodenToilet);
        TileData.Toilets.Add(TILETYPE.CorruptedIglooToilet);
        TileData.Toilets.Add(TILETYPE.CorruptedJungleToliet);
      }
      return TileData.Toilets.Contains(tiletype);
    }

    internal static bool IsThisaLamppost(TILETYPE tiletype)
    {
      if (TileData.Lamppost == null)
      {
        TileData.Lamppost = new HashSet<TILETYPE>();
        TileData.Lamppost.Add(TILETYPE.Lamppost);
        TileData.Lamppost.Add(TILETYPE.FloorLight);
        TileData.Lamppost.Add(TILETYPE.AztecTorch);
        TileData.Lamppost.Add(TILETYPE.TwinLamppost);
        TileData.Lamppost.Add(TILETYPE.CurlLampPost);
        TileData.Lamppost.Add(TILETYPE.TwinCurlsLampPost);
        TileData.Lamppost.Add(TILETYPE.WoodenLampPost);
        TileData.Lamppost.Add(TILETYPE.TripletsLampPost);
        TileData.Lamppost.Add(TILETYPE.ClassicLampPost);
        TileData.Lamppost.Add(TILETYPE.WhiteClassicLampPost);
        TileData.Lamppost.Add(TILETYPE.FlamingoLampPost);
        TileData.Lamppost.Add(TILETYPE.SealLampPost);
        TileData.Lamppost.Add(TILETYPE.BallLampPost);
        TileData.Lamppost.Add(TILETYPE.TreeWithLights);
        TileData.Lamppost.Add(TILETYPE.AsianLight);
      }
      return TileData.Lamppost.Contains(tiletype);
    }

    internal static bool IsThisAnATM(TILETYPE tiletype)
    {
      if (TileData.ATMs == null)
      {
        TileData.ATMs = new HashSet<TILETYPE>();
        TileData.ATMs.Add(TILETYPE.ATMMachine);
      }
      return TileData.ATMs.Contains(tiletype);
    }

    internal static bool IsThisABin(TILETYPE tiletype)
    {
      if (TileData.TrashBins == null)
      {
        TileData.TrashBins = new HashSet<TILETYPE>();
        TileData.TrashBins.Add(TILETYPE.BearTrashbin);
        TileData.TrashBins.Add(TILETYPE.PenguinTrashbin);
        TileData.TrashBins.Add(TILETYPE.LionTrashbin);
        TileData.TrashBins.Add(TILETYPE.WhiteDustbin);
        TileData.TrashBins.Add(TILETYPE.GreenDustbin);
        TileData.TrashBins.Add(TILETYPE.RecyclingBin);
        TileData.TrashBins.Add(TILETYPE.WoodenTrashbin);
      }
      return TileData.TrashBins.Contains(tiletype);
    }

    internal static bool IsThisABench(TILETYPE tiletype)
    {
      if (TileData.Benches == null)
      {
        TileData.Benches = new HashSet<TILETYPE>();
        TileData.Benches.Add(TILETYPE.BrownBench);
        TileData.Benches.Add(TILETYPE.WhiteBench);
        TileData.Benches.Add(TILETYPE.SnakeBench);
        TileData.Benches.Add(TILETYPE.UmbrellaBench);
        TileData.Benches.Add(TILETYPE.SwingingBench);
        TileData.Benches.Add(TILETYPE.GreenGardenBench);
        TileData.Benches.Add(TILETYPE.GreenChair);
        TileData.Benches.Add(TILETYPE.WoodenChair);
        TileData.Benches.Add(TILETYPE.LongWoodenBench);
        TileData.Benches.Add(TILETYPE.CamelChair);
        TileData.Benches.Add(TILETYPE.PandaChair);
        TileData.Benches.Add(TILETYPE.IceChair);
        TileData.Benches.Add(TILETYPE.TreeLogBench);
        TileData.Benches.Add(TILETYPE.TreeSwing);
        TileData.Benches.Add(TILETYPE.SmallBarTable);
        TileData.Benches.Add(TILETYPE.AztacChair);
      }
      return TileData.Benches.Contains(tiletype);
    }

    internal static bool DoesThisImapactPublicty(TILETYPE tiletype) => TileData.SignBoards == null ? TileData.IsThisASignBoard(tiletype) : TileData.SignBoards.Contains(tiletype);

    internal static bool IsThisASignBoard(TILETYPE tiletype)
    {
      if (TileData.SignBoards == null)
      {
        TileData.SignBoards = new HashSet<TILETYPE>();
        TileData.SignBoards.Add(TILETYPE.ThickSignboard);
        TileData.SignBoards.Add(TILETYPE.DangerSign);
        TileData.SignBoards.Add(TILETYPE.BarSignboard);
        TileData.SignBoards.Add(TILETYPE.ZooMap);
        TileData.SignBoards.Add(TILETYPE.NoSwimmingSign);
        TileData.SignBoards.Add(TILETYPE.SnakeSignpost);
        TileData.SignBoards.Add(TILETYPE.CrocCrossingSign);
        TileData.SignBoards.Add(TILETYPE.MenuSign);
        TileData.SignBoards.Add(TILETYPE.NoPhotoSign);
        TileData.SignBoards.Add(TILETYPE.AztecSign);
        TileData.SignBoards.Add(TILETYPE.NoticeBoard);
        TileData.SignBoards.Add(TILETYPE.PenguinSignboard);
      }
      return TileData.SignBoards.Contains(tiletype);
    }

    internal static bool IsThisAWaterTrough(TILETYPE tiletype)
    {
      if (TileData.WaterTroughs == null)
      {
        TileData.WaterTroughs = new HashSet<TILETYPE>();
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Metal);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Metal_Single);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Wooden);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Wooden_Single);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Leaf);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_TreeTrunk);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_Rock);
        TileData.WaterTroughs.Add(TILETYPE.WaterTrough_IceRock);
      }
      return TileData.WaterTroughs.Contains(tiletype);
    }

    internal static int GetWaterVolume(TILETYPE tiletype) => tiletype == TILETYPE.WaterTrough_Metal_Single || tiletype == TILETYPE.WaterTrough_Wooden_Single || tiletype == TILETYPE.WaterTrough_TreeTrunk ? 1 : 2;

    internal static TILETYPE GetWaterTroughToEmptyTrough(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.WaterTrough_Metal:
          return TILETYPE.EmptyWaterTrough_Metal;
        case TILETYPE.WaterTrough_Metal_Single:
          return TILETYPE.EmptyWaterTrough_Metal_Single;
        case TILETYPE.WaterTrough_Wooden:
          return TILETYPE.EmptyWaterTrough_Wooden;
        case TILETYPE.WaterTrough_Wooden_Single:
          return TILETYPE.EmptyWaterTrough_Wooden_Single;
        case TILETYPE.WaterTrough_Leaf:
          return TILETYPE.EmptyWaterTrough_Leaf;
        case TILETYPE.WaterTrough_TreeTrunk:
          return TILETYPE.EmptyWaterTrough_TreeTrunk;
        case TILETYPE.WaterTrough_Rock:
          return TILETYPE.EmptyWaterTrough_Rock;
        case TILETYPE.WaterTrough_IceRock:
          return TILETYPE.EmptyWaterTrough_IceRock;
        default:
          throw new Exception("IUGDYGDGYD");
      }
    }

    internal static bool IsThisAPenFloor(TILETYPE tiletype)
    {
      if (TileData.PenFloorTileTypes == null)
      {
        TileData.PenFloorTileTypes = new HashSet<TILETYPE>();
        TileData.PenFloorTileTypes.Add(TILETYPE.Arctic_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Mountain_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Desert_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Savanah_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Forest_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Grasslands_Floor);
        TileData.PenFloorTileTypes.Add(TILETYPE.Tropical_Floor);
      }
      return TileData.PenFloorTileTypes.Contains(tiletype);
    }

    internal static bool IsThisAPenDecoration(TILETYPE tiletype)
    {
      if (TileData.PenDeco == null)
      {
        TileData.PenDeco = new HashSet<TILETYPE>();
        TileData.PenDeco.Add(TILETYPE.SwingTreeDeco);
        TileData.PenDeco.Add(TILETYPE.DarkTreeDeco);
      }
      return TileData.PenDeco.Contains(tiletype);
    }

    internal static bool IsThisAShelter(TILETYPE tiletype)
    {
      if (TileData.Shelters == null)
      {
        TileData.Shelters = new HashSet<TILETYPE>();
        TileData.Shelters.Add(TILETYPE.Shelter_Igloo);
        TileData.Shelters.Add(TILETYPE.Shelter_LargeRockCave);
        TileData.Shelters.Add(TILETYPE.Shelter_LargeWoodenHouse);
        TileData.Shelters.Add(TILETYPE.Shelter_SmallRockCave);
        TileData.Shelters.Add(TILETYPE.Shelter_MossyRock);
        TileData.Shelters.Add(TILETYPE.Shelter_IceRocks);
      }
      return TileData.Shelters.Contains(tiletype);
    }

    internal static bool IsThisAPenBoundary(TILETYPE tiletype)
    {
      if (TileData.PenBoundaries == null)
      {
        TileData.PenBoundaries = new HashSet<TILETYPE>();
        TileData.PenBoundaries.Add(TILETYPE.Arctic_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Arctic_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Arctic_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Arctic);
        TileData.PenBoundaries.Add(TILETYPE.Mountain_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Mountain_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Mountain_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Mountain);
        TileData.PenBoundaries.Add(TILETYPE.Desert_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Desert_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Desert_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Mountain);
        TileData.PenBoundaries.Add(TILETYPE.Savanah_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Savanah_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Savanah_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Savanah);
        TileData.PenBoundaries.Add(TILETYPE.Forest_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Forest_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Forest_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Forest);
        TileData.PenBoundaries.Add(TILETYPE.Grasslands_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Grasslands_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Grasslands_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Grass);
        TileData.PenBoundaries.Add(TILETYPE.Tropical_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.Tropical_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.Tropical_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_Tropical);
        TileData.PenBoundaries.Add(TILETYPE.FieldPicketFence_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.FieldPicketFence_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.FieldPicketFence_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.Gate_FieldPicketFence);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedGrass_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedGrass_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedGrass_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedGrass_Gate);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedForest_WallCorner);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedForest_WallInnerCorner);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedForest_WallSide);
        TileData.PenBoundaries.Add(TILETYPE.CorruptedForest_Gate);
      }
      return TileData.PenBoundaries.Contains(tiletype);
    }

    internal static bool IsAModifableBuilding(TILETYPE tiletype) => tiletype == TILETYPE.LionHotDogShop || tiletype == TILETYPE.ElephantGiftShop || (tiletype == TILETYPE.SmallGiftShop || tiletype == TILETYPE.IceCreamTruck) || (tiletype == TILETYPE.BigIceCreamShop || tiletype == TILETYPE.CoconutShop || (tiletype == TILETYPE.PandaBurgerShop || tiletype == TILETYPE.BalloonShop)) || (tiletype == TILETYPE.KangarooPizzaShop || tiletype == TILETYPE.CottonCandyShop || (tiletype == TILETYPE.SlushieShop || tiletype == TILETYPE.ChurroShop) || (tiletype == TILETYPE.DrinksVendingMachine || tiletype == TILETYPE.SnacksVendingMachine || (tiletype == TILETYPE.ChocolateVendingMachine || tiletype == TILETYPE.KatCoffeeShop))) || (tiletype == TILETYPE.PopcornWeaselShop || tiletype == TILETYPE.RustyKegShop || (tiletype == TILETYPE.ShellShackShop || tiletype == TILETYPE.PretzelShop)) || tiletype == TILETYPE.TacoTruck;

    internal static bool IsAStoreRoom(TILETYPE tiletype)
    {
      if (TileData.StoreRooms == null)
      {
        TileData.StoreRooms = new HashSet<TILETYPE>();
        TileData.StoreRooms.Add(TILETYPE.StoreRoom);
        TileData.StoreRooms.Add(TILETYPE.Storeroom_BrownWood);
        TileData.StoreRooms.Add(TILETYPE.Storeroom_Victorian);
        TileData.StoreRooms.Add(TILETYPE.Storeroom_Monkey);
      }
      return TileData.StoreRooms.Contains(tiletype);
    }

    internal static bool IsThisAVendingMachine(TILETYPE tiletype)
    {
      if (TileData.VendingMachines == null)
      {
        TileData.VendingMachines = new HashSet<TILETYPE>();
        TileData.VendingMachines.Add(TILETYPE.ChocolateVendingMachine);
        TileData.VendingMachines.Add(TILETYPE.DrinksVendingMachine);
        TileData.VendingMachines.Add(TILETYPE.SnacksVendingMachine);
      }
      return TileData.VendingMachines.Contains(tiletype);
    }

    internal static int StoreRoomCapacity(TILETYPE tiletype) => 1000;

    internal static bool IsAMeatProcessingPlant(TILETYPE tiletype) => tiletype == TILETYPE.MeatProcessor;

    internal static bool IsAVegetableProcessingPlant(TILETYPE tiletype) => tiletype == TILETYPE.FarmProcessor;

    internal static bool IsAFactory(TILETYPE tiletype)
    {
      if (TileData.FactoryBuildings == null)
      {
        TileData.FactoryBuildings = new HashSet<TILETYPE>();
        TileData.FactoryBuildings.Add(TILETYPE.GlueFactory);
        TileData.FactoryBuildings.Add(TILETYPE.BuffaloWingFactory);
        TileData.FactoryBuildings.Add(TILETYPE.BaconFactory);
        TileData.FactoryBuildings.Add(TILETYPE.CrocHandbagFactory);
        TileData.FactoryBuildings.Add(TILETYPE.SnakeSkinFactory);
        TileData.FactoryBuildings.Add(TILETYPE.MilkBatteryFarm);
        TileData.FactoryBuildings.Add(TILETYPE.EggBatteryFarm);
        TileData.FactoryBuildings.Add(TILETYPE.BeerBrewery);
        TileData.FactoryBuildings.Add(TILETYPE.Windmill);
        TileData.FactoryBuildings.Add(TILETYPE.ImpossibleBuilding);
      }
      return TileData.FactoryBuildings.Contains(tiletype);
    }

    internal static bool IsACRISPRBuilding(TILETYPE tiletype) => tiletype == TILETYPE.DNABuilding;

    internal static bool IsABreedingRoom(TILETYPE tiletype) => tiletype == TILETYPE.Nursery;

    internal static bool IsASlaughterhouse(TILETYPE tiletype) => tiletype == TILETYPE.Slaughterhouse;

    internal static bool IsASurveillanceBuilding(TILETYPE tiletype) => tiletype == TILETYPE.SurveillanceBuilding;

    internal static bool IsAQuarantineBuilding(TILETYPE tiletype) => tiletype == TILETYPE.QuarantineOffice;

    internal static bool IsAnIncinerator(TILETYPE tiletype) => tiletype == TILETYPE.Incinerator;

    internal static bool IsAWarehouse(TILETYPE tiletype) => tiletype == TILETYPE.Warehouse;

    internal static HashSet<TILETYPE> GetStoreRoomTileTypes() => TileData.StoreRooms;

    internal static HashSet<TILETYPE> GetFoodShops() => TileData.Food;

    internal static HashSet<TILETYPE> GetDrinksShops() => TileData.Drinks;

    internal static HashSet<TILETYPE> GetGiftShops() => TileData.SouvenirShop;

    internal static HashSet<TILETYPE> GetFactories()
    {
      if (TileData.FactoryBuildings == null)
        TileData.IsAFactory(TILETYPE.Count);
      return TileData.FactoryBuildings;
    }

    internal static bool HasAirVehicle(TILETYPE tiletype)
    {
      if (TileData.AirVehicleBuildings == null)
      {
        TileData.AirVehicleBuildings = new HashSet<TILETYPE>();
        TileData.AirVehicleBuildings.Add(TILETYPE.HelicopterRide);
        TileData.AirVehicleBuildings.Add(TILETYPE.HotAirBalloonRide);
        TileData.AirVehicleBuildings.Add(TILETYPE.CorruptedHotAirBalloonRide);
      }
      return TileData.AirVehicleBuildings.Contains(tiletype);
    }

    internal static bool IsATicketedRide(TILETYPE tiletype)
    {
      if (TileData.TicketedRides == null)
      {
        TileData.TicketedRides = new HashSet<TILETYPE>();
        TileData.TicketedRides.Add(TILETYPE.HelicopterRide);
        TileData.TicketedRides.Add(TILETYPE.HotAirBalloonRide);
        TileData.TicketedRides.Add(TILETYPE.CorruptedHotAirBalloonRide);
      }
      return TileData.TicketedRides.Contains(tiletype);
    }

    internal static bool IsATicketOffice(TILETYPE tiletype)
    {
      if (TileData.TicketOffices == null)
      {
        TileData.TicketOffices = new HashSet<TILETYPE>();
        TileData.TicketOffices.Add(TILETYPE.Logo);
        TileData.TicketOffices.Add(TILETYPE.ZooEntrance_Deer);
        TileData.TicketOffices.Add(TILETYPE.ZooEntrance_Modern);
        TileData.TicketOffices.Add(TILETYPE.ZooEntrance_Cliff);
      }
      return TileData.TicketOffices.Contains(tiletype);
    }

    internal static HashSet<TILETYPE> GetTicketOfficeTileTypes() => TileData.TicketOffices;

    internal static bool IsAManagementOffice(TILETYPE tiletype)
    {
      if (TileData.ManagementOffices == null)
      {
        TileData.ManagementOffices = new HashSet<TILETYPE>();
        TileData.ManagementOffices.Add(TILETYPE.ManagmentOffice_BrownWood);
        TileData.ManagementOffices.Add(TILETYPE.ManagmentOffice_SimpleBlack);
        TileData.ManagementOffices.Add(TILETYPE.ManagmentOffice_BlueCottage);
        TileData.ManagementOffices.Add(TILETYPE.ManagmentOffice_Rabbit);
      }
      return TileData.ManagementOffices.Contains(tiletype);
    }

    internal static HashSet<TILETYPE> GetManagementOfficeTileTypes() => TileData.ManagementOffices;

    internal static bool IsAnArchitectOffice(TILETYPE tiletype) => tiletype == TILETYPE.ArchitectOffice || tiletype == TILETYPE.LargeArchitectOffice;

    internal static bool GetIsPrisonWall(TILETYPE tiletype) => TileData.GetTileInfo(tiletype).buildingtype == BUILDINGTYPE.PrisonWall;

    internal static bool GetHasRoof(LayoutEntry layoutentry)
    {
      if (layoutentry != null && layoutentry.tiletype != TILETYPE.None && layoutentry.tiletype != TILETYPE.Logo)
      {
        switch (TileData.GetTileInfo(layoutentry.tiletype).buildingtype)
        {
          case BUILDINGTYPE.Floor:
            return true;
          case BUILDINGTYPE.Wall:
            return true;
          case BUILDINGTYPE.Building:
            return true;
        }
      }
      return false;
    }

    internal static bool CanSellThis(TILETYPE tiletype) => tiletype != TILETYPE.Moon && tiletype != TILETYPE.BoundaryTree && (tiletype != TILETYPE.PinkMoonPlant && tiletype != TILETYPE.Road) && (tiletype != TILETYPE.DefaultFence_WallSide && tiletype != TILETYPE.DefaultFence_WallCorner && tiletype != TILETYPE.DefaultFence_InnerCorner);

    internal static bool isWallNotCorner(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.Grasslands_WallSide:
        case TILETYPE.Forest_WallSide:
        case TILETYPE.Savanah_WallSide:
        case TILETYPE.Desert_WallSide:
        case TILETYPE.Mountain_WallSide:
        case TILETYPE.Arctic_WallSide:
        case TILETYPE.Tropical_WallSide:
        case TILETYPE.FieldPicketFence_WallSide:
        case TILETYPE.CorruptedGrass_WallSide:
        case TILETYPE.CorruptedForest_WallSide:
        case TILETYPE.Rockwall_WallSide:
        case TILETYPE.Mudwall_WallSide:
        case TILETYPE.GrassWall_WallSide:
          return true;
        default:
          return false;
      }
    }

    internal static bool ThisStructureBlocksPathFinding(TILETYPE tiletype) => !CategoryData.IsThisAFarmPlant(tiletype) && tiletype != TILETYPE.PinkMoonPlant && (tiletype != TILETYPE.BoundaryTree && tiletype != TILETYPE.EMPTY_DIRT_WALKABLE_TILE) && (tiletype != TILETYPE.FloorLight && tiletype != TILETYPE.TestFence);

    internal static bool ThisStructureIsBlocking(TILETYPE tiletype) => tiletype != TILETYPE.None && tiletype != TILETYPE.FloorLight && (tiletype != TILETYPE.TestFence && tiletype != TILETYPE.ForSaleSignboard);

    public static bool IsThisWater(TILETYPE tiletype) => tiletype == TILETYPE.Volume_Water;

    internal static bool IsThisAWarpGate(TILETYPE tiletype)
    {
      if (TileData.WarpGates == null)
      {
        TileData.WarpGates = new HashSet<TILETYPE>();
        TileData.WarpGates.Add(TILETYPE.SubwayEntrance);
        TileData.WarpGates.Add(TILETYPE.SubwayEntrance_Jungle);
        TileData.WarpGates.Add(TILETYPE.SubwayEntrance_Ice);
        TileData.WarpGates.Add(TILETYPE.SubwayEntrance_Capy);
        TileData.WarpGates.Add(TILETYPE.SubwayEntrance_RedRoof);
      }
      return TileData.WarpGates.Contains(tiletype);
    }

    internal static bool IsThisAPartialFloor(LayoutEntry entry)
    {
      if (TileData.PartialFloors == null)
      {
        TileData.PartialFloors = new HashSet<TILETYPE>();
        TileData.PartialFloors.Add(TILETYPE.Volume_Water);
        TileData.PartialFloors.Add(TILETYPE.Volume_WhiteSnow);
        TileData.PartialFloors.Add(TILETYPE.Volume_Grass);
        TileData.PartialFloors.Add(TILETYPE.Volume_WoodenBoards);
        TileData.PartialFloors.Add(TILETYPE.Floor_WoodenTrunk);
        TileData.PartialFloors.Add(TILETYPE.Floor_StonePebbles);
        TileData.PartialFloors.Add(TILETYPE.Floor_WoodenPlanks);
        TileData.PartialFloors.Add(TILETYPE.Floor_Snow);
        TileData.PartialFloors.Add(TILETYPE.Volume_DarkGrass);
        TileData.PartialFloors.Add(TILETYPE.Volume_YellowGrass);
        TileData.PartialFloors.Add(TILETYPE.Volume_Sand);
        TileData.PartialFloors.Add(TILETYPE.Volume_LightMud);
        TileData.PartialFloors.Add(TILETYPE.Volume_LightGrass);
        TileData.PartialFloors.Add(TILETYPE.CorruptedDirtFloor_Volume);
        TileData.PartialFloors.Add(TILETYPE.CorruptedDirtGrass_Volume);
        TileData.PartialFloors.Add(TILETYPE.CorruptedSand_Volume);
        TileData.PartialFloors.Add(TILETYPE.CorruptedSnow_Volume);
        TileData.PartialFloors.Add(TILETYPE.Floor_GreenGrass);
        TileData.PartialFloors.Add(TILETYPE.Floor_Dirt);
        TileData.PartialFloors.Add(TILETYPE.Floor_WoodenBoards);
        TileData.PartialFloors.Add(TILETYPE.Floor_MetalDecoTile);
        TileData.PartialFloors.Add(TILETYPE.Floor_GreyBricks);
        TileData.PartialFloors.Add(TILETYPE.Floor_BlueCircleTiles);
        TileData.PartialFloors.Add(TILETYPE.Floor_OrangeTiles);
        TileData.PartialFloors.Add(TILETYPE.Floor_ColorfulBrickTile);
        TileData.PartialFloors.Add(TILETYPE.Floor_Cobblestone);
        TileData.PartialFloors.Add(TILETYPE.Floor_PinkSmallTiles);
        TileData.PartialFloors.Add(TILETYPE.Floor_BrownOctoTile);
        TileData.PartialFloors.Add(TILETYPE.Floor_GreenAndBlueDiamondTile);
        TileData.PartialFloors.Add(TILETYPE.Floor_BrownSquareTile);
        TileData.PartialFloors.Add(TILETYPE.Floor_PawDecoTile);
        TileData.PartialFloors.Add(TILETYPE.AztacTempleFloor);
        TileData.PartialFloors.Add(TILETYPE.CorruptedSnow_Floor);
        TileData.PartialFloors.Add(TILETYPE.CorruptedGreenGrass_Floor);
        TileData.PartialFloors.Add(TILETYPE.CorruptedDirt_Floor);
      }
      if (!TileData.PartialFloors.Contains(entry.tiletype))
        return false;
      return TileData.IsThisATopFloorOnly(entry.tiletype) || (uint) entry.RotationClockWise > 0U;
    }

    internal static bool IsThisATopFloorOnly(TILETYPE tiltype)
    {
      if (TileData.TopFloors == null)
      {
        TileData.TopFloors = new HashSet<TILETYPE>();
        TileData.TopFloors.Add(TILETYPE.Floor_StonePebbles);
        TileData.TopFloors.Add(TILETYPE.Floor_WoodenPlanks);
        TileData.TopFloors.Add(TILETYPE.Floor_WoodenTrunk);
      }
      return TileData.TopFloors.Contains(tiltype);
    }

    internal static bool WillTintAtNight(TILETYPE tiletype)
    {
      switch (tiletype)
      {
        case TILETYPE.SealIceSculpture:
        case TILETYPE.DeerIceSculpture:
        case TILETYPE.BearIceSculpture:
        case TILETYPE.BirdIceSculpture:
        case TILETYPE.GiraffeIceSculpture:
          return false;
        default:
          return true;
      }
    }
  }
}
