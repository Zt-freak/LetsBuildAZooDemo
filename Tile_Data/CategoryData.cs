// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.CategoryData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.Tile_Data
{
  internal class CategoryData
  {
    private static List<TILETYPE> _Amenities;
    private static List<TILETYPE> _CellBlock;
    private static List<TILETYPE> _Decorative;
    private static List<TILETYPE> _Floors;
    private static List<TILETYPE> _Nature;
    private static List<TILETYPE> _Shops;
    private static List<TILETYPE> _Attractions;
    private static List<TILETYPE> _Facilities;
    private static List<TILETYPE> _Signboards;
    private static List<TILETYPE> _Walls;
    private static List<TILETYPE> _Light;
    private static List<TILETYPE> _Benches;
    private static List<TILETYPE> _Farm;
    private static List<TILETYPE> _Factories;
    private static List<TILETYPE> _Water;
    private static List<TILETYPE> _FarmPlants;
    private static List<TILETYPE> _Pen_Water;
    private static List<TILETYPE> _Pen_Deco;
    private static List<TILETYPE> _Pen_Enrichment;
    private static List<TILETYPE> _Pen_Shelter;
    private static List<TILETYPE> _Trees;
    private static HashSet<TILETYPE> Decoratives;
    private static HashSet<TILETYPE> DecorativesNature;
    private static HashSet<TILETYPE> DecorativesWater;
    private static HashSet<TILETYPE> Floors;
    private static HashSet<TILETYPE> Amenities;
    private static HashSet<TILETYPE> FarmPlants;
    private static HashSet<TILETYPE> Attractions;
    private static HashSet<TILETYPE> Farm;
    private static HashSet<TILETYPE> StretchedFoors;

    internal static string GetCategoryToname(CATEGORYTYPE category)
    {
      switch (category)
      {
        case CATEGORYTYPE.Enclosure:
          return "Habitat";
        case CATEGORYTYPE.Shops:
          return "Shops";
        case CATEGORYTYPE.Facilities:
          return "Facilities";
        case CATEGORYTYPE.Amenities:
          return "Amenities";
        case CATEGORYTYPE.Floors:
          return "Floor Tiles";
        case CATEGORYTYPE.Attractions:
          return "Attractions";
        case CATEGORYTYPE.Nature:
          return "Nature";
        case CATEGORYTYPE.Signboards:
          return "Signboards";
        case CATEGORYTYPE.Decorative:
          return "Decoration";
        case CATEGORYTYPE.Light:
          return "Light";
        case CATEGORYTYPE.Benches:
          return "Benches";
        case CATEGORYTYPE.Farm:
          return "Farm";
        case CATEGORYTYPE.Water:
          return "Water";
        case CATEGORYTYPE.Factories:
          return "Factories";
        case CATEGORYTYPE.Walls:
          return "Walls";
        default:
          return "NO NAME";
      }
    }

    internal static bool IsThisAFarmPlant(TILETYPE tiletype)
    {
      if (CategoryData.FarmPlants == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.FarmPlants);
      return CategoryData.FarmPlants.Contains(tiletype);
    }

    internal static CATEGORYTYPE GetTileTypeToCetagory(
      TILETYPE tiletype,
      out POINT_OF_INTEREST subcat_pointofinterest)
    {
      subcat_pointofinterest = POINT_OF_INTEREST.Count;
      if (CategoryData.IsThisADeocration(tiletype))
        return CATEGORYTYPE.Decorative;
      if (CategoryData.IsThisANatireDeocration(tiletype))
        return CATEGORYTYPE.Nature;
      if (CategoryData.IsThisAFloor(tiletype))
      {
        TileData.IsThisFloorAVolume(tiletype);
        return CATEGORYTYPE.Floors;
      }
      if (TileData.IsThisABench(tiletype))
        return CATEGORYTYPE.Benches;
      if (tiletype == TILETYPE.Nursery)
        return CATEGORYTYPE.Facilities;
      if (tiletype == TILETYPE.WaterPumpStation)
        return CATEGORYTYPE.Amenities;
      if (TileData.IsThisABin(tiletype))
      {
        subcat_pointofinterest = POINT_OF_INTEREST.Bin;
        return CATEGORYTYPE.Amenities;
      }
      if (TileData.IsThisanArchitectOffice(tiletype))
      {
        subcat_pointofinterest = POINT_OF_INTEREST.ArchitectsOffice;
        return CATEGORYTYPE.Facilities;
      }
      if (TileData.IsThisAnATM(tiletype))
      {
        subcat_pointofinterest = POINT_OF_INTEREST.ATM;
        return CATEGORYTYPE.Amenities;
      }
      if (TileData.IsAStoreRoom(tiletype))
      {
        subcat_pointofinterest = POINT_OF_INTEREST.StoreRoomOrCupboard;
        return CATEGORYTYPE.Facilities;
      }
      if (TileData.IsThisACellBlock(tiletype))
        return CATEGORYTYPE.Enclosure;
      if (tiletype == TILETYPE.QuarantineOffice || tiletype == TILETYPE.ImpossibleBuilding || (tiletype == TILETYPE.MeatProcessor || tiletype == TILETYPE.FarmProcessor) || (tiletype == TILETYPE.RecyclingCenter || tiletype == TILETYPE.AnimalRehabilitationBuilding || (tiletype == TILETYPE.RainCollectionBuilding || tiletype == TILETYPE.Incinerator)) || (tiletype == TILETYPE.InfoBooth || tiletype == TILETYPE.SurveillanceBuilding || tiletype == TILETYPE.DNABuilding))
        return CATEGORYTYPE.Facilities;
      if (TileData.IsAFactory(tiletype) || tiletype == TILETYPE.AnimalColosseum || tiletype == TILETYPE.Slaughterhouse)
        return CATEGORYTYPE.Factories;
      if (TileData.IsThisAFacility(tiletype))
        return CATEGORYTYPE.Facilities;
      if (TileData.IsThisAShopWithShopStats(tiletype))
      {
        if (TileData.IsForThirst(tiletype))
          subcat_pointofinterest = POINT_OF_INTEREST.DrinksStore;
        else if (TileData.IsForFood(tiletype))
          subcat_pointofinterest = POINT_OF_INTEREST.FoodStore;
        else if (TileData.IsForSouvenir(tiletype))
          subcat_pointofinterest = POINT_OF_INTEREST.GiftShop;
        return CATEGORYTYPE.Shops;
      }
      if (TileData.IsThisaToilet(tiletype))
      {
        subcat_pointofinterest = POINT_OF_INTEREST.Toilet;
        return CATEGORYTYPE.Amenities;
      }
      if (TileData.IsThisASignBoard(tiletype))
        return CATEGORYTYPE.Signboards;
      if (TileData.IsThisaLamppost(tiletype))
        return CATEGORYTYPE.Light;
      if (tiletype == TILETYPE.TikiShelter || tiletype == TILETYPE.RedShelter || (tiletype == TILETYPE.IceShelter || tiletype == TILETYPE.MushroomShelter) || (tiletype == TILETYPE.OldWestShelter || tiletype == TILETYPE.AsianPavillion || (tiletype == TILETYPE.Umbrella || tiletype == TILETYPE.IceSpeaker)) || (tiletype == TILETYPE.LargeSpeaker || tiletype == TILETYPE.RockSpeaker || (tiletype == TILETYPE.SmallSpeaker || tiletype == TILETYPE.SolarPanel) || tiletype == TILETYPE.WindTurbine))
        return CATEGORYTYPE.Amenities;
      if (CategoryData.IsThisAWaterDeco(tiletype))
        return CATEGORYTYPE.Water;
      if (CategoryData.IsThisAnAttraction(tiletype))
        return CATEGORYTYPE.Attractions;
      return CategoryData.IsThisAFarm(tiletype) ? CATEGORYTYPE.Farm : CATEGORYTYPE.Count;
    }

    internal static string GetCategoryDescription(TILETYPE tiletype)
    {
      POINT_OF_INTEREST subcat_pointofinterest;
      return CategoryData.GetCategoryDescription(CategoryData.GetTileTypeToCetagory(tiletype, out subcat_pointofinterest), subcat_pointofinterest, tiletype);
    }

    internal static bool IsThisAFloor(TILETYPE thistile)
    {
      if (CategoryData.Floors == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Floors);
      return CategoryData.Floors.Contains(thistile);
    }

    internal static bool IsThisAnAmenity(TILETYPE thistile)
    {
      if (CategoryData.Floors == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Amenities);
      return CategoryData.Floors.Contains(thistile);
    }

    internal static string GetCategoryDescription(
      CATEGORYTYPE category,
      POINT_OF_INTEREST pointofinteresttype,
      TILETYPE thisbuilding)
    {
      string str = "NO CATEGORY DESCRIPTION FOUND";
      switch (category)
      {
        case CATEGORYTYPE.Enclosure:
          str = SEngine.Localization.Localization.GetText(460);
          break;
        case CATEGORYTYPE.Shops:
          switch (pointofinteresttype)
          {
            case POINT_OF_INTEREST.DrinksStore:
              str = SEngine.Localization.Localization.GetText(471);
              if (thisbuilding == TILETYPE.RustyKegShop)
                str = SEngine.Localization.Localization.GetText(472);
              if (thisbuilding == TILETYPE.KatCoffeeShop)
              {
                str = SEngine.Localization.Localization.GetText(473);
                break;
              }
              break;
            case POINT_OF_INTEREST.FoodStore:
              return SEngine.Localization.Localization.GetText(470);
            case POINT_OF_INTEREST.GiftShop:
              return SEngine.Localization.Localization.GetText(474);
          }
          break;
        case CATEGORYTYPE.Facilities:
          if (pointofinteresttype == POINT_OF_INTEREST.StoreRoomOrCupboard)
          {
            str = SEngine.Localization.Localization.GetText(461);
            break;
          }
          if (TileData.IsAnArchitectOffice(thisbuilding))
          {
            str = SEngine.Localization.Localization.GetText(462);
            break;
          }
          if (thisbuilding == TILETYPE.Nursery)
          {
            str = SEngine.Localization.Localization.GetText(463);
            break;
          }
          if (thisbuilding == TILETYPE.QuarantineOffice)
          {
            str = SEngine.Localization.Localization.GetText(464);
            break;
          }
          if (thisbuilding == TILETYPE.ImpossibleBuilding)
          {
            str = SEngine.Localization.Localization.GetText(465);
            break;
          }
          if (thisbuilding == TILETYPE.MeatProcessor)
          {
            str = SEngine.Localization.Localization.GetText(466);
            break;
          }
          if (thisbuilding == TILETYPE.Incinerator)
          {
            str = SEngine.Localization.Localization.GetText(467);
            break;
          }
          if (thisbuilding == TILETYPE.SurveillanceBuilding)
          {
            str = SEngine.Localization.Localization.GetText(468);
            break;
          }
          if (thisbuilding == TILETYPE.DNABuilding)
          {
            str = SEngine.Localization.Localization.GetText(469);
            break;
          }
          if (thisbuilding == TILETYPE.MeatProcessor)
          {
            str = "Turn your dead animals into usable meat! Recycling your animals - Good for the planet.";
            break;
          }
          if (thisbuilding == TILETYPE.VetOffice)
          {
            str = "Build this to employ vets who can treat ill animals and give them check-ups!";
            break;
          }
          if (thisbuilding == TILETYPE.RecyclingCenter)
          {
            str = "If you are going green for the environment, you will need one of these!";
            break;
          }
          if (thisbuilding == TILETYPE.AnimalRehabilitationBuilding)
          {
            str = "Release animals into the wild - Stop them from being endangered.";
            break;
          }
          if (thisbuilding == TILETYPE.Warehouse)
          {
            str = "Products from your farm or factories will be placed here to be sold.";
            break;
          }
          if (thisbuilding == TILETYPE.RainCollectionBuilding)
          {
            str = "Collect water in a green way!";
            break;
          }
          if (thisbuilding == TILETYPE.InfoBooth)
          {
            str = "Always great to have one in the park, for those lost in the zoo, but not those lost in life.";
            break;
          }
          break;
        case CATEGORYTYPE.Amenities:
          switch (pointofinteresttype)
          {
            case POINT_OF_INTEREST.Bin:
              str = SEngine.Localization.Localization.GetText(454);
              break;
            case POINT_OF_INTEREST.Toilet:
              str = SEngine.Localization.Localization.GetText(453);
              break;
            case POINT_OF_INTEREST.ATM:
              str = SEngine.Localization.Localization.GetText(455);
              break;
            default:
              switch (thisbuilding)
              {
                case TILETYPE.Umbrella:
                case TILETYPE.TikiShelter:
                case TILETYPE.RedShelter:
                case TILETYPE.IceShelter:
                case TILETYPE.OldWestShelter:
                case TILETYPE.MushroomShelter:
                case TILETYPE.AsianPavillion:
                  str = "Place some shelters, stay in the shade. Increases deco.";
                  break;
                case TILETYPE.SmallSpeaker:
                case TILETYPE.LargeSpeaker:
                case TILETYPE.RockSpeaker:
                case TILETYPE.IceSpeaker:
                  str = "For a musically enriching experience.";
                  break;
                case TILETYPE.WaterPumpStation:
                  str = SEngine.Localization.Localization.GetText(456);
                  break;
                case TILETYPE.WindTurbine:
                case TILETYPE.SolarPanel:
                  str = "Generate electricity in a green way!";
                  break;
              }
              break;
          }
          break;
        case CATEGORYTYPE.Floors:
          str = "Add some nice new floors to floor your customers with.";
          break;
        case CATEGORYTYPE.Attractions:
          str = "Add some exciting rides around your zoo for more popularity and extra cash!";
          break;
        case CATEGORYTYPE.Nature:
          str = "Everyone likes a bit of scenery! Adds decoration points. A good variety of items will keep your park looking good.";
          break;
        case CATEGORYTYPE.Signboards:
          str = "Add some colorful signs to your park for some additional deco!";
          break;
        case CATEGORYTYPE.Decorative:
          str = "Adds some cool decorations to your park! Adds deco points. A good variety of items will keep your park fun.";
          break;
        case CATEGORYTYPE.Light:
          str = "Light up your life and your park as well!";
          break;
        case CATEGORYTYPE.Benches:
          str = "Benches will give a small boost of energy to visitors when they sit on it!";
          break;
        case CATEGORYTYPE.Farm:
          str = "Plant and grow crops, in the hopes that everything will go green and all will be well again.";
          break;
        case CATEGORYTYPE.Water:
          str = "Add some water decorations to make your zoo more beautiful!";
          break;
        case CATEGORYTYPE.Factories:
          switch (thisbuilding)
          {
            case TILETYPE.Slaughterhouse:
              str = "Turn live animals into dead animals - the best way to get meat quickly";
              break;
            case TILETYPE.AnimalColosseum:
              str = "Want to see your animals battle to the death? Say no more.";
              break;
            case TILETYPE.Windmill:
              str = "Make bread from your crops!";
              break;
            default:
              str = "Money is power! Make more money by sacrificing your morality.";
              break;
          }
          break;
      }
      return str;
    }

    internal static bool IsThisAnAttraction(TILETYPE tiletype)
    {
      if (CategoryData.Attractions == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Attractions);
      return CategoryData.Attractions.Contains(tiletype);
    }

    internal static bool IsThisAFarm(TILETYPE tiletype)
    {
      if (CategoryData.Farm == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Farm);
      return CategoryData.Farm.Contains(tiletype);
    }

    internal static bool IsThisAWaterDeco(TILETYPE tiletype)
    {
      if (CategoryData.DecorativesWater == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Water);
      return CategoryData.DecorativesWater.Contains(tiletype);
    }

    internal static bool IsThisANatireDeocration(TILETYPE tiletype)
    {
      if (CategoryData.DecorativesNature == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Nature);
      return CategoryData.DecorativesNature.Contains(tiletype);
    }

    internal static bool IsThisADeocration(TILETYPE tiletype)
    {
      if (CategoryData.Decoratives == null)
        CategoryData.GetEntriesInThisCategory(CATEGORYTYPE.Decorative);
      return CategoryData.Decoratives.Contains(tiletype);
    }

    internal static bool IsThisACroppedFloor(TILETYPE tiletype)
    {
      if (CategoryData.StretchedFoors == null)
      {
        CategoryData.StretchedFoors = new HashSet<TILETYPE>();
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_GreenGrass);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_Dirt);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_GreyBricks);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_BlueCircleTiles);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_ColorfulBrickTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_OrangeTiles);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_WoodenBoards);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_MetalDecoTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_PawDecoTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_Cobblestone);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_PinkSmallTiles);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_BrownOctoTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_GreenAndBlueDiamondTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_BrownSquareTile);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_Snow);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_RedPathway);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_Grass);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_WoodenBoards);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_WhiteSnow);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_WoodenBridge);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_DarkGrass);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_YellowGrass);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_Sand);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_LightMud);
        CategoryData.StretchedFoors.Add(TILETYPE.Volume_LightGrass);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_WoodenPlanks);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_WoodenTrunk);
        CategoryData.StretchedFoors.Add(TILETYPE.Floor_StonePebbles);
        CategoryData.StretchedFoors.Add(TILETYPE.AztacTempleFloor);
        CategoryData.StretchedFoors.Add(TILETYPE.CorruptedSnow_Floor);
        CategoryData.StretchedFoors.Add(TILETYPE.CorruptedGreenGrass_Floor);
        CategoryData.StretchedFoors.Add(TILETYPE.CorruptedDirt_Floor);
      }
      return CategoryData.StretchedFoors.Contains(tiletype);
    }

    internal static List<TILETYPE> GetEntriesInThisCategory(CATEGORYTYPE category)
    {
      switch (category)
      {
        case CATEGORYTYPE.Enclosure:
          if (CategoryData._CellBlock == null)
          {
            CategoryData._CellBlock = new List<TILETYPE>();
            CategoryData._CellBlock.Add(TILETYPE.GrassEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.DesertEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.MountainEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.ArcticEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.TropicalEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.ForestEnclosure);
            CategoryData._CellBlock.Add(TILETYPE.SavannahEnclosure);
            int num = 0;
            while (num < CategoryData._CellBlock.Count)
              ++num;
          }
          return CategoryData._CellBlock;
        case CATEGORYTYPE.Shops:
          if (CategoryData._Shops == null)
          {
            CategoryData._Shops = new List<TILETYPE>();
            CategoryData._Shops.Add(TILETYPE.DrinksVendingMachine);
            CategoryData._Shops.Add(TILETYPE.SnacksVendingMachine);
            CategoryData._Shops.Add(TILETYPE.SmallGiftShop);
            CategoryData._Shops.Add(TILETYPE.LionHotDogShop);
            CategoryData._Shops.Add(TILETYPE.SlushieShop);
            CategoryData._Shops.Add(TILETYPE.BalloonShop);
            CategoryData._Shops.Add(TILETYPE.ChurroShop);
            CategoryData._Shops.Add(TILETYPE.IceCreamTruck);
            CategoryData._Shops.Add(TILETYPE.BigIceCreamShop);
            CategoryData._Shops.Add(TILETYPE.CoconutShop);
            CategoryData._Shops.Add(TILETYPE.PandaBurgerShop);
            CategoryData._Shops.Add(TILETYPE.KangarooPizzaShop);
            CategoryData._Shops.Add(TILETYPE.CottonCandyShop);
            CategoryData._Shops.Add(TILETYPE.ElephantGiftShop);
            CategoryData._Shops.Add(TILETYPE.RustyKegShop);
            CategoryData._Shops.Add(TILETYPE.PopcornWeaselShop);
            CategoryData._Shops.Add(TILETYPE.KatCoffeeShop);
            CategoryData._Shops.Add(TILETYPE.ShellShackShop);
            CategoryData._Shops.Add(TILETYPE.TacoTruck);
            CategoryData._Shops.Add(TILETYPE.PretzelShop);
            CategoryData._Shops.Add(TILETYPE.ChocolateVendingMachine);
            for (int index = 0; index < CategoryData._Shops.Count; ++index)
              TileData.GetTileInfo(CategoryData._Shops[index]).categorytype = CATEGORYTYPE.Shops;
          }
          return CategoryData._Shops;
        case CATEGORYTYPE.Facilities:
          if (CategoryData._Facilities == null)
          {
            CategoryData._Facilities = new List<TILETYPE>();
            CategoryData._Facilities.Add(TILETYPE.StoreRoom);
            CategoryData._Facilities.Add(TILETYPE.ArchitectOffice);
            CategoryData._Facilities.Add(TILETYPE.DNABuilding);
            CategoryData._Facilities.Add(TILETYPE.InfoBooth);
            CategoryData._Facilities.Add(TILETYPE.Nursery);
            CategoryData._Facilities.Add(TILETYPE.QuarantineOffice);
            CategoryData._Facilities.Add(TILETYPE.VetOffice);
            CategoryData._Facilities.Add(TILETYPE.LargeArchitectOffice);
            CategoryData._Facilities.Add(TILETYPE.ImpossibleBuilding);
            CategoryData._Facilities.Add(TILETYPE.MeatProcessor);
            CategoryData._Facilities.Add(TILETYPE.Incinerator);
            CategoryData._Facilities.Add(TILETYPE.SurveillanceBuilding);
            CategoryData._Facilities.Add(TILETYPE.FarmProcessor);
            CategoryData._Facilities.Add(TILETYPE.RecyclingCenter);
            CategoryData._Facilities.Add(TILETYPE.AnimalRehabilitationBuilding);
            CategoryData._Facilities.Add(TILETYPE.Warehouse);
            CategoryData._Facilities.Add(TILETYPE.RainCollectionBuilding);
            for (int index = 0; index < CategoryData._Facilities.Count; ++index)
              TileData.GetTileInfo(CategoryData._Facilities[index]).categorytype = CATEGORYTYPE.Facilities;
          }
          return CategoryData._Facilities;
        case CATEGORYTYPE.Amenities:
          if (CategoryData._Amenities == null)
          {
            CategoryData.Amenities = new HashSet<TILETYPE>();
            CategoryData._Amenities = new List<TILETYPE>();
            CategoryData._Amenities.Add(TILETYPE.WaterPumpStation);
            CategoryData._Amenities.Add(TILETYPE.SubwayEntrance);
            CategoryData._Amenities.Add(TILETYPE.SubwayEntrance_Jungle);
            CategoryData._Amenities.Add(TILETYPE.SubwayEntrance_Ice);
            CategoryData._Amenities.Add(TILETYPE.SubwayEntrance_Capy);
            CategoryData._Amenities.Add(TILETYPE.SubwayEntrance_RedRoof);
            CategoryData._Amenities.Add(TILETYPE.WoodenToilet);
            CategoryData._Amenities.Add(TILETYPE.WhiteDustbin);
            CategoryData._Amenities.Add(TILETYPE.GreenDustbin);
            CategoryData._Amenities.Add(TILETYPE.RedShelter);
            CategoryData._Amenities.Add(TILETYPE.TikiShelter);
            CategoryData._Amenities.Add(TILETYPE.Umbrella);
            CategoryData._Amenities.Add(TILETYPE.BrickToilet);
            CategoryData._Amenities.Add(TILETYPE.ATMMachine);
            CategoryData._Amenities.Add(TILETYPE.SmallSpeaker);
            CategoryData._Amenities.Add(TILETYPE.LargeSpeaker);
            CategoryData._Amenities.Add(TILETYPE.RockSpeaker);
            CategoryData._Amenities.Add(TILETYPE.IceSpeaker);
            CategoryData._Amenities.Add(TILETYPE.IglooToilet);
            CategoryData._Amenities.Add(TILETYPE.PenguinTrashbin);
            CategoryData._Amenities.Add(TILETYPE.LionTrashbin);
            CategoryData._Amenities.Add(TILETYPE.BearTrashbin);
            CategoryData._Amenities.Add(TILETYPE.IceShelter);
            CategoryData._Amenities.Add(TILETYPE.JungleToliet);
            CategoryData._Amenities.Add(TILETYPE.MushroomShelter);
            CategoryData._Amenities.Add(TILETYPE.WindTurbine);
            CategoryData._Amenities.Add(TILETYPE.SolarPanel);
            CategoryData._Amenities.Add(TILETYPE.RecyclingBin);
            CategoryData._Amenities.Add(TILETYPE.OldWestToliet);
            CategoryData._Amenities.Add(TILETYPE.WoodenTrashbin);
            CategoryData._Amenities.Add(TILETYPE.AztacToliet);
            CategoryData._Amenities.Add(TILETYPE.AsianToliet);
            CategoryData._Amenities.Add(TILETYPE.AsianPavillion);
            CategoryData._Amenities.Add(TILETYPE.OldWestShelter);
            for (int index = 0; index < CategoryData._Amenities.Count; ++index)
              TileData.GetTileInfo(CategoryData._Amenities[index]).categorytype = CATEGORYTYPE.Amenities;
          }
          return CategoryData._Amenities;
        case CATEGORYTYPE.Floors:
          if (CategoryData._Floors == null)
          {
            CategoryData.Floors = new HashSet<TILETYPE>();
            CategoryData._Floors = new List<TILETYPE>();
            CategoryData._Floors.Add(TILETYPE.Floor_Dirt);
            CategoryData._Floors.Add(TILETYPE.Volume_Grass);
            CategoryData._Floors.Add(TILETYPE.Floor_GreyBricks);
            CategoryData._Floors.Add(TILETYPE.Floor_WoodenBoards);
            CategoryData._Floors.Add(TILETYPE.Floor_MetalDecoTile);
            CategoryData._Floors.Add(TILETYPE.Floor_Cobblestone);
            CategoryData._Floors.Add(TILETYPE.Volume_RedPathway);
            CategoryData._Floors.Add(TILETYPE.Floor_BlueCircleTiles);
            CategoryData._Floors.Add(TILETYPE.Floor_GreenGrass);
            CategoryData._Floors.Add(TILETYPE.Floor_OrangeTiles);
            CategoryData._Floors.Add(TILETYPE.Floor_ColorfulBrickTile);
            CategoryData._Floors.Add(TILETYPE.Floor_PinkSmallTiles);
            CategoryData._Floors.Add(TILETYPE.Floor_BrownOctoTile);
            CategoryData._Floors.Add(TILETYPE.Floor_GreenAndBlueDiamondTile);
            CategoryData._Floors.Add(TILETYPE.Floor_BrownSquareTile);
            CategoryData._Floors.Add(TILETYPE.Floor_PawDecoTile);
            CategoryData._Floors.Add(TILETYPE.Floor_Snow);
            CategoryData._Floors.Add(TILETYPE.Volume_WoodenBoards);
            CategoryData._Floors.Add(TILETYPE.Floor_WoodenPlanks);
            CategoryData._Floors.Add(TILETYPE.Floor_WoodenTrunk);
            CategoryData._Floors.Add(TILETYPE.Floor_StonePebbles);
            CategoryData._Floors.Add(TILETYPE.Volume_WhiteSnow);
            CategoryData._Floors.Add(TILETYPE.Volume_WoodenBridge);
            CategoryData._Floors.Add(TILETYPE.Volume_DarkGrass);
            CategoryData._Floors.Add(TILETYPE.Volume_YellowGrass);
            CategoryData._Floors.Add(TILETYPE.Volume_Sand);
            CategoryData._Floors.Add(TILETYPE.AztacTempleFloor);
            CategoryData._Floors.Add(TILETYPE.Volume_LightMud);
            CategoryData._Floors.Add(TILETYPE.Volume_LightGrass);
            for (int index = 0; index < CategoryData._Floors.Count; ++index)
            {
              TileData.GetTileInfo(CategoryData._Floors[index]).categorytype = CATEGORYTYPE.Floors;
              CategoryData.Floors.Add(CategoryData._Floors[index]);
            }
          }
          return CategoryData._Floors;
        case CATEGORYTYPE.Attractions:
          if (CategoryData._Attractions == null)
          {
            CategoryData._Attractions = new List<TILETYPE>();
            CategoryData._Attractions.Add(TILETYPE.SnakeHuggingBooth);
            CategoryData._Attractions.Add(TILETYPE.TreeExhibit);
            CategoryData._Attractions.Add(TILETYPE.LionPlayground);
            CategoryData._Attractions.Add(TILETYPE.SpringChicken);
            CategoryData._Attractions.Add(TILETYPE.SpringHorse);
            CategoryData._Attractions.Add(TILETYPE.TigerPhoto);
            CategoryData._Attractions.Add(TILETYPE.Binoculars);
            CategoryData._Attractions.Add(TILETYPE.CoinSquasher);
            CategoryData._Attractions.Add(TILETYPE.HelicopterRide);
            CategoryData._Attractions.Add(TILETYPE.HotAirBalloonRide);
            CategoryData.Attractions = new HashSet<TILETYPE>();
            for (int index = 0; index < CategoryData._Attractions.Count; ++index)
            {
              CategoryData.Attractions.Add(CategoryData._Attractions[index]);
              TileData.GetTileInfo(CategoryData._Attractions[index]).categorytype = CATEGORYTYPE.Attractions;
            }
          }
          return CategoryData._Attractions;
        case CATEGORYTYPE.Nature:
          if (CategoryData._Nature == null)
          {
            CategoryData._Nature = new List<TILETYPE>();
            CategoryData._Nature.Add(TILETYPE.PlantPot);
            CategoryData._Nature.Add(TILETYPE.FlowerPatch);
            CategoryData._Nature.Add(TILETYPE.ZooTree);
            CategoryData._Nature.Add(TILETYPE.RedFlower);
            CategoryData._Nature.Add(TILETYPE.WhiteFlower);
            CategoryData._Nature.Add(TILETYPE.PurpleFlower);
            CategoryData._Nature.Add(TILETYPE.PurpleFlowerPatch);
            CategoryData._Nature.Add(TILETYPE.GreenTree);
            CategoryData._Nature.Add(TILETYPE.LongGrass);
            CategoryData._Nature.Add(TILETYPE.YellowPlantPot);
            CategoryData._Nature.Add(TILETYPE.Ferns);
            CategoryData._Nature.Add(TILETYPE.SmallRock);
            CategoryData._Nature.Add(TILETYPE.MediumRock);
            CategoryData._Nature.Add(TILETYPE.SmallDesertRockDeco);
            CategoryData._Nature.Add(TILETYPE.PottedPlant);
            CategoryData._Nature.Add(TILETYPE.BonsaiPlantPot);
            CategoryData._Nature.Add(TILETYPE.PineTree);
            CategoryData._Nature.Add(TILETYPE.PineTreeDark);
            CategoryData._Nature.Add(TILETYPE.PeacockBush);
            CategoryData._Nature.Add(TILETYPE.LargeMossyRock);
            CategoryData._Nature.Add(TILETYPE.DesertRockDeco);
            CategoryData._Nature.Add(TILETYPE.DesertCactusDeco);
            CategoryData._Nature.Add(TILETYPE.BigTree);
            CategoryData._Nature.Add(TILETYPE.FlamingoHedge);
            CategoryData._Nature.Add(TILETYPE.GiraffeHedge);
            CategoryData._Nature.Add(TILETYPE.ElephantHedge);
            CategoryData._Nature.Add(TILETYPE.SakuraTree);
            CategoryData._Nature.Add(TILETYPE.YellowBush);
            CategoryData._Nature.Add(TILETYPE.AztecPlant);
            CategoryData._Nature.Add(TILETYPE.PalmTree);
            CategoryData._Nature.Add(TILETYPE.IrisPlantPot);
            CategoryData._Nature.Add(TILETYPE.ColoredTree);
            CategoryData._Nature.Add(TILETYPE.DarkBush);
            CategoryData._Nature.Add(TILETYPE.Cactus);
            CategoryData._Nature.Add(TILETYPE.SmallIceRocks);
            CategoryData._Nature.Add(TILETYPE.ThinIceRocks);
            CategoryData._Nature.Add(TILETYPE.IceCrystals);
            CategoryData._Nature.Add(TILETYPE.IcyTree);
            CategoryData._Nature.Add(TILETYPE.BlueGrass);
            CategoryData._Nature.Add(TILETYPE.GiantTree);
            CategoryData._Nature.Add(TILETYPE.Bamboo);
            CategoryData._Nature.Add(TILETYPE.SmallBush);
            CategoryData._Nature.Add(TILETYPE.DeadTree);
            CategoryData._Nature.Add(TILETYPE.BigRocks);
            CategoryData._Nature.Add(TILETYPE.PalmTreesTall);
            CategoryData._Nature.Add(TILETYPE.TreeStump);
            CategoryData._Nature.Add(TILETYPE.GlowingPlant);
            CategoryData._Nature.Add(TILETYPE.RedMushrooms);
            CategoryData._Nature.Add(TILETYPE.BrownMushrooms);
            CategoryData._Nature.Add(TILETYPE.LeafyFern);
            CategoryData._Nature.Add(TILETYPE.LargeFern);
            CategoryData._Nature.Add(TILETYPE.TreeWithVines);
            CategoryData._Nature.Add(TILETYPE.SmallCactus);
            CategoryData._Nature.Add(TILETYPE.SmallGrass);
            CategoryData._Nature.Add(TILETYPE.ArticBush);
            CategoryData._Nature.Add(TILETYPE.DarkSmallPlant);
            CategoryData._Nature.Add(TILETYPE.DeadWinterTree);
            CategoryData._Nature.Add(TILETYPE.DesertTree);
            CategoryData._Nature.Add(TILETYPE.CactusLong);
            CategoryData._Nature.Add(TILETYPE.BigMountainRocks);
            CategoryData._Nature.Add(TILETYPE.OrangeLargeRocks);
            CategoryData._Nature.Add(TILETYPE.MediumStones);
            CategoryData._Nature.Add(TILETYPE.LightGreenTree);
            CategoryData.DecorativesNature = new HashSet<TILETYPE>();
            for (int index = 0; index < CategoryData._Nature.Count; ++index)
            {
              CategoryData.DecorativesNature.Add(CategoryData._Nature[index]);
              TileData.GetTileInfo(CategoryData._Nature[index]).categorytype = CATEGORYTYPE.Nature;
            }
          }
          return CategoryData._Nature;
        case CATEGORYTYPE.Signboards:
          if (CategoryData._Signboards == null)
          {
            CategoryData._Signboards = new List<TILETYPE>();
            CategoryData._Signboards.Add(TILETYPE.ZooMap);
            CategoryData._Signboards.Add(TILETYPE.ThickSignboard);
            CategoryData._Signboards.Add(TILETYPE.SnakeSignpost);
            CategoryData._Signboards.Add(TILETYPE.PenguinSignboard);
            CategoryData._Signboards.Add(TILETYPE.NoPhotoSign);
            CategoryData._Signboards.Add(TILETYPE.SignboardFront);
            CategoryData._Signboards.Add(TILETYPE.DangerSign);
            CategoryData._Signboards.Add(TILETYPE.BarSignboard);
            CategoryData._Signboards.Add(TILETYPE.NoSwimmingSign);
            CategoryData._Signboards.Add(TILETYPE.CrocCrossingSign);
            CategoryData._Signboards.Add(TILETYPE.MenuSign);
            CategoryData._Signboards.Add(TILETYPE.AztecSign);
            CategoryData._Signboards.Add(TILETYPE.NoticeBoard);
            CategoryData._Signboards.Add(TILETYPE.CrossSign);
            CategoryData._Signboards.Add(TILETYPE.ArrowSignboard);
            CategoryData._Signboards.Add(TILETYPE.WestWoodenBoard);
            CategoryData._Signboards.Add(TILETYPE.AztacMap);
            for (int index = 0; index < CategoryData._Signboards.Count; ++index)
              TileData.GetTileInfo(CategoryData._Signboards[index]).categorytype = CATEGORYTYPE.Signboards;
          }
          return CategoryData._Signboards;
        case CATEGORYTYPE.Decorative:
          if (CategoryData._Decorative == null)
          {
            CategoryData._Decorative = new List<TILETYPE>();
            CategoryData._Decorative.Add(TILETYPE.RedFlag);
            CategoryData._Decorative.Add(TILETYPE.BearStandee);
            CategoryData._Decorative.Add(TILETYPE.RoundFountain);
            CategoryData._Decorative.Add(TILETYPE.OwlClock);
            CategoryData._Decorative.Add(TILETYPE.ElegantTallFountain);
            CategoryData._Decorative.Add(TILETYPE.ZooStandee);
            CategoryData._Decorative.Add(TILETYPE.MonkeyStatue);
            CategoryData._Decorative.Add(TILETYPE.GiantSunFlower);
            CategoryData._Decorative.Add(TILETYPE.PenguinStandee);
            CategoryData._Decorative.Add(TILETYPE.SealStandee);
            CategoryData._Decorative.Add(TILETYPE.MonkeyBanner);
            CategoryData._Decorative.Add(TILETYPE.RockElephant);
            CategoryData._Decorative.Add(TILETYPE.HeShee);
            CategoryData._Decorative.Add(TILETYPE.Totem);
            CategoryData._Decorative.Add(TILETYPE.WoodenTotem);
            CategoryData._Decorative.Add(TILETYPE.ElephantFountain);
            CategoryData._Decorative.Add(TILETYPE.ElephantMarbleFountain);
            CategoryData._Decorative.Add(TILETYPE.MiniFountain);
            CategoryData._Decorative.Add(TILETYPE.IceArch);
            CategoryData._Decorative.Add(TILETYPE.Snowman);
            CategoryData._Decorative.Add(TILETYPE.LionSnowSculpture);
            CategoryData._Decorative.Add(TILETYPE.SealIceSculpture);
            CategoryData._Decorative.Add(TILETYPE.DeerIceSculpture);
            CategoryData._Decorative.Add(TILETYPE.BearIceSculpture);
            CategoryData._Decorative.Add(TILETYPE.BirdIceSculpture);
            CategoryData._Decorative.Add(TILETYPE.GiraffeIceSculpture);
            CategoryData._Decorative.Add(TILETYPE.ZooIceSign);
            CategoryData._Decorative.Add(TILETYPE.IceCastle);
            CategoryData._Decorative.Add(TILETYPE.ZooRockSign);
            CategoryData._Decorative.Add(TILETYPE.GiantPigBalloon);
            CategoryData._Decorative.Add(TILETYPE.WaterTower);
            CategoryData._Decorative.Add(TILETYPE.Cart);
            CategoryData._Decorative.Add(TILETYPE.WantedPoster);
            CategoryData._Decorative.Add(TILETYPE.Pyramid);
            CategoryData._Decorative.Add(TILETYPE.SandTower);
            CategoryData._Decorative.Add(TILETYPE.SmallPyramid);
            CategoryData._Decorative.Add(TILETYPE.Sphinx);
            CategoryData._Decorative.Add(TILETYPE.Anubis);
            CategoryData._Decorative.Add(TILETYPE.WesternWindmill);
            CategoryData._Decorative.Add(TILETYPE.JungleArch);
            CategoryData._Decorative.Add(TILETYPE.JungleWaterfall);
            CategoryData._Decorative.Add(TILETYPE.RockyWaterfall);
            CategoryData._Decorative.Add(TILETYPE.GiantBearBalloon);
            CategoryData._Decorative.Add(TILETYPE.LightHouse);
            CategoryData._Decorative.Add(TILETYPE.GiraffeAirDancer);
            CategoryData._Decorative.Add(TILETYPE.SnakeAirDancer);
            CategoryData._Decorative.Add(TILETYPE.Caravan);
            CategoryData._Decorative.Add(TILETYPE.WestBarrel);
            CategoryData._Decorative.Add(TILETYPE.WestWoodenBox);
            CategoryData._Decorative.Add(TILETYPE.WesternArch);
            CategoryData._Decorative.Add(TILETYPE.OrangeStoneArch);
            CategoryData._Decorative.Add(TILETYPE.BoneArch);
            CategoryData._Decorative.Add(TILETYPE.BoneDeco);
            CategoryData._Decorative.Add(TILETYPE.CarpStreamers);
            CategoryData._Decorative.Add(TILETYPE.HorizonValleyZooBalloon);
            CategoryData._Decorative.Add(TILETYPE.TallRoofHouse);
            CategoryData._Decorative.Add(TILETYPE.AztacSnakeStatue);
            CategoryData._Decorative.Add(TILETYPE.AztacTemple);
            CategoryData._Decorative.Add(TILETYPE.AztacTempleGate);
            CategoryData._Decorative.Add(TILETYPE.AsianGate);
            CategoryData._Decorative.Add(TILETYPE.RockyZooSign);
            CategoryData._Decorative.Add(TILETYPE.HedgeArchYellowFlowers);
            CategoryData._Decorative.Add(TILETYPE.HedgeArchWhiteFlowers);
            CategoryData._Decorative.Add(TILETYPE.WhiteMetalVineArch);
            CategoryData._Decorative.Add(TILETYPE.BlackMetalRoseArch);
            CategoryData.Decoratives = new HashSet<TILETYPE>();
            for (int index = 0; index < CategoryData._Decorative.Count; ++index)
            {
              TileData.GetTileInfo(CategoryData._Decorative[index]).categorytype = CATEGORYTYPE.Decorative;
              CategoryData.Decoratives.Add(CategoryData._Decorative[index]);
            }
          }
          return CategoryData._Decorative;
        case CATEGORYTYPE.Light:
          if (CategoryData._Light == null)
          {
            CategoryData._Light = new List<TILETYPE>();
            CategoryData._Light.Add(TILETYPE.Lamppost);
            CategoryData._Light.Add(TILETYPE.ClassicLampPost);
            CategoryData._Light.Add(TILETYPE.WhiteClassicLampPost);
            CategoryData._Light.Add(TILETYPE.FloorLight);
            CategoryData._Light.Add(TILETYPE.AztecTorch);
            CategoryData._Light.Add(TILETYPE.TwinLamppost);
            CategoryData._Light.Add(TILETYPE.CurlLampPost);
            CategoryData._Light.Add(TILETYPE.TwinCurlsLampPost);
            CategoryData._Light.Add(TILETYPE.WoodenLampPost);
            CategoryData._Light.Add(TILETYPE.TripletsLampPost);
            CategoryData._Light.Add(TILETYPE.FlamingoLampPost);
            CategoryData._Light.Add(TILETYPE.SealLampPost);
            CategoryData._Light.Add(TILETYPE.BallLampPost);
            CategoryData._Light.Add(TILETYPE.TreeWithLights);
            CategoryData._Light.Add(TILETYPE.AsianLight);
            for (int index = 0; index < CategoryData._Light.Count; ++index)
              TileData.GetTileInfo(CategoryData._Light[index]).categorytype = CATEGORYTYPE.Light;
          }
          return CategoryData._Light;
        case CATEGORYTYPE.Benches:
          if (CategoryData._Benches == null)
          {
            CategoryData._Benches = new List<TILETYPE>();
            CategoryData._Benches.Add(TILETYPE.BrownBench);
            CategoryData._Benches.Add(TILETYPE.WhiteBench);
            CategoryData._Benches.Add(TILETYPE.GreenGardenBench);
            CategoryData._Benches.Add(TILETYPE.LongWoodenBench);
            CategoryData._Benches.Add(TILETYPE.SnakeBench);
            CategoryData._Benches.Add(TILETYPE.UmbrellaBench);
            CategoryData._Benches.Add(TILETYPE.SmallBarTable);
            CategoryData._Benches.Add(TILETYPE.SwingingBench);
            CategoryData._Benches.Add(TILETYPE.GreenChair);
            CategoryData._Benches.Add(TILETYPE.WoodenChair);
            CategoryData._Benches.Add(TILETYPE.CamelChair);
            CategoryData._Benches.Add(TILETYPE.PandaChair);
            CategoryData._Benches.Add(TILETYPE.IceChair);
            CategoryData._Benches.Add(TILETYPE.TreeLogBench);
            CategoryData._Benches.Add(TILETYPE.TreeSwing);
            CategoryData._Benches.Add(TILETYPE.AztacChair);
            for (int index = 0; index < CategoryData._Benches.Count; ++index)
              TileData.GetTileInfo(CategoryData._Benches[index]).categorytype = CATEGORYTYPE.Benches;
          }
          return CategoryData._Benches;
        case CATEGORYTYPE.Farm:
          if (CategoryData._Farm == null)
          {
            CategoryData._Farm = new List<TILETYPE>();
            CategoryData._Farm.Add(TILETYPE.TestFence);
            CategoryData._Farm.Add(TILETYPE.Farmhouse);
            CategoryData._Farm.Add(TILETYPE.Barn);
            CategoryData._Farm.Add(TILETYPE.Silo);
            CategoryData._Farm.Add(TILETYPE.EmptySoilPatch);
            CategoryData._Farm.Add(TILETYPE.Greenhouse);
            CategoryData.Farm = new HashSet<TILETYPE>();
            for (int index = 0; index < CategoryData._Farm.Count; ++index)
            {
              CategoryData.Farm.Add(CategoryData._Farm[index]);
              TileData.GetTileInfo(CategoryData._Farm[index]).categorytype = CATEGORYTYPE.Farm;
            }
          }
          return CategoryData._Farm;
        case CATEGORYTYPE.Water:
          if (CategoryData._Water == null)
          {
            CategoryData._Water = new List<TILETYPE>();
            CategoryData._Water.Add(TILETYPE.Volume_Water);
            CategoryData._Water.Add(TILETYPE.Water_SmallLilyPads);
            CategoryData._Water.Add(TILETYPE.Water_LargeLilyPads);
            CategoryData._Water.Add(TILETYPE.Water_Reeds);
            CategoryData._Water.Add(TILETYPE.Water_LotusFlower);
            CategoryData._Water.Add(TILETYPE.Water_Rock);
            CategoryData._Water.Add(TILETYPE.Water_FlatRock);
            CategoryData._Water.Add(TILETYPE.Water_WoodenBoards);
            CategoryData._Water.Add(TILETYPE.Water_Lanturn);
            CategoryData._Water.Add(TILETYPE.Water_LightBall);
            CategoryData._Water.Add(TILETYPE.Water_BirdStatue);
            CategoryData._Water.Add(TILETYPE.Water_FlappingBirdStatue);
            CategoryData._Water.Add(TILETYPE.Water_FishFountain);
            CategoryData._Water.Add(TILETYPE.Water_WaterJarFountain);
            CategoryData._Water.Add(TILETYPE.Water_Fountain);
            CategoryData._Water.Add(TILETYPE.Water_FloatingCrate);
            CategoryData._Water.Add(TILETYPE.Water_FloatingBarrel);
            CategoryData._Water.Add(TILETYPE.Water_WaterJet);
            CategoryData._Water.Add(TILETYPE.Water_TreasureChest);
            CategoryData._Water.Add(TILETYPE.Water_SunkenShip);
            CategoryData._Water.Add(TILETYPE.Water_IceRocks);
            CategoryData._Water.Add(TILETYPE.Water_FlatIce);
            CategoryData._Water.Add(TILETYPE.Water_IceBoulders);
            CategoryData._Water.Add(TILETYPE.Water_RockBoulders);
            CategoryData._Water.Add(TILETYPE.Water_FloatHouse);
            CategoryData._Water.Add(TILETYPE.Water_Skull);
            CategoryData._Water.Add(TILETYPE.Water_CannonballJet);
            CategoryData._Water.Add(TILETYPE.Water_WaterMill);
            CategoryData._Water.Add(TILETYPE.Water_WaterLanturn);
            CategoryData._Water.Add(TILETYPE.Water_FrogFountain);
            CategoryData._Water.Add(TILETYPE.Water_IceArchRock);
            CategoryData._Water.Add(TILETYPE.Water_MangroveTree);
            CategoryData._Water.Add(TILETYPE.Water_SmallMangroveTree);
            CategoryData._Water.Add(TILETYPE.Water_GrassyRock);
            CategoryData._Water.Add(TILETYPE.Water_SmallGrassyRock);
            CategoryData._Water.Add(TILETYPE.Water_DeerScare);
            CategoryData._Water.Add(TILETYPE.Water_SunkenRocksLarge);
            CategoryData._Water.Add(TILETYPE.Water_SunkenRocksMed);
            CategoryData._Water.Add(TILETYPE.Water_SunkenRocksSmall);
            CategoryData.DecorativesWater = new HashSet<TILETYPE>();
            for (int index = 0; index < CategoryData._Water.Count; ++index)
            {
              CategoryData.DecorativesWater.Add(CategoryData._Water[index]);
              TileData.GetTileInfo(CategoryData._Water[index]).categorytype = CATEGORYTYPE.Water;
            }
          }
          return CategoryData._Water;
        case CATEGORYTYPE.Factories:
          if (CategoryData._Factories == null)
          {
            CategoryData._Factories = new List<TILETYPE>();
            CategoryData._Factories.Add(TILETYPE.GlueFactory);
            CategoryData._Factories.Add(TILETYPE.BuffaloWingFactory);
            CategoryData._Factories.Add(TILETYPE.BaconFactory);
            CategoryData._Factories.Add(TILETYPE.Slaughterhouse);
            CategoryData._Factories.Add(TILETYPE.EggBatteryFarm);
            CategoryData._Factories.Add(TILETYPE.MilkBatteryFarm);
            CategoryData._Factories.Add(TILETYPE.AnimalColosseum);
            CategoryData._Factories.Add(TILETYPE.CrocHandbagFactory);
            CategoryData._Factories.Add(TILETYPE.SnakeSkinFactory);
            CategoryData._Factories.Add(TILETYPE.BeerBrewery);
            CategoryData._Factories.Add(TILETYPE.Windmill);
            for (int index = 0; index < CategoryData._Factories.Count; ++index)
              TileData.GetTileInfo(CategoryData._Factories[index]).categorytype = CATEGORYTYPE.Factories;
          }
          return CategoryData._Factories;
        case CATEGORYTYPE.Walls:
          if (CategoryData._Walls == null)
          {
            CategoryData._Walls = new List<TILETYPE>();
            CategoryData._Walls.Add(TILETYPE.Fence_White);
            CategoryData._Walls.Add(TILETYPE.Fence_Rope);
            CategoryData._Walls.Add(TILETYPE.Fence_TwoLines);
            CategoryData._Walls.Add(TILETYPE.Fence_CrossPlanks);
            CategoryData._Walls.Add(TILETYPE.Fence_RedPlank);
            CategoryData._Walls.Add(TILETYPE.Fence_GreenWire);
            CategoryData._Walls.Add(TILETYPE.Fence_BlueIce);
            CategoryData._Walls.Add(TILETYPE.Fence_BlackBar);
            CategoryData._Walls.Add(TILETYPE.Fence_BlackBarbed);
            CategoryData._Walls.Add(TILETYPE.Fence_BlackPillar);
            CategoryData._Walls.Add(TILETYPE.Fence_WhitePillar);
            for (int index = 0; index < CategoryData._Walls.Count; ++index)
              TileData.GetTileInfo(CategoryData._Walls[index]).categorytype = CATEGORYTYPE.Walls;
          }
          return CategoryData._Walls;
        case CATEGORYTYPE.Pen_Water:
          if (CategoryData._Pen_Water == null)
          {
            CategoryData._Pen_Water = new List<TILETYPE>();
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Metal_Single);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Metal);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Wooden_Single);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Wooden);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Leaf);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_TreeTrunk);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_Rock);
            CategoryData._Pen_Water.Add(TILETYPE.WaterTrough_IceRock);
          }
          return CategoryData._Pen_Water;
        case CATEGORYTYPE.Pen_Deco:
          if (CategoryData._Pen_Deco == null)
          {
            CategoryData._Pen_Deco = new List<TILETYPE>();
            CategoryData._Pen_Deco.Add(TILETYPE.DarkTreeDeco);
            CategoryData._Pen_Deco.Add(TILETYPE.SwingTreeDeco);
          }
          return CategoryData._Pen_Deco;
        case CATEGORYTYPE.Pen_Enrichment:
          if (CategoryData._Pen_Enrichment == null)
          {
            CategoryData._Pen_Enrichment = new List<TILETYPE>();
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_HighStriker);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_Sunglasses);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_FlowerBlueHat);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_SmallPinkHat);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_RedRibbon);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_FootballHelmet);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_AngelHalo);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_RedStripedCap);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_Attachment_ColorfulAfroWig);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SmallBlueBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SmallCyanBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SmallGreenBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SmallRedBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SmallPinkBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_BlueTrampoline);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_PinkTrampoline);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_WaterSprinklers);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ScratchingPostWood);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_TugRopeToy);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ChewToyPurpleBone);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ChewToyBrownBone);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ChewToyRope);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_TunnelGreen);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_TunnelWoodenLog);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_WoodenLogs);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_FlatCarTire);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_HangingCarTire);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_HighWoodBeamPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_RockCliff);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_RockPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_SaltBlock);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_MirrorRect);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_MirrorRound);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_TugBallJollyBallYellow);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ScentMarkerGrey);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeCardboardBox);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ScentMarkerGreen);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ScentMarkerBrown);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeRedBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeWhiteBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeGreenBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeYellowBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LargeBlueBall);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_IceCliff);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_BrownCliff);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_ScratchingPoleTree);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_BrownRockPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_HighPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_YellowRockPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_LogPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_NetPerch);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_WoodenBeam2);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_WoodenBeam3);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_CardboardBox2);
            CategoryData._Pen_Enrichment.Add(TILETYPE.Enrichment_TreeHighPerch);
          }
          return CategoryData._Pen_Enrichment;
        case CATEGORYTYPE.Pen_Shelter:
          if (CategoryData._Pen_Shelter == null)
          {
            CategoryData._Pen_Shelter = new List<TILETYPE>();
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_SmallRockCave);
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_LargeRockCave);
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_LargeWoodenHouse);
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_Igloo);
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_MossyRock);
            CategoryData._Pen_Shelter.Add(TILETYPE.Shelter_IceRocks);
          }
          return CategoryData._Pen_Shelter;
        case CATEGORYTYPE.FarmPlants:
          if (CategoryData._FarmPlants == null)
          {
            CategoryData._FarmPlants = new List<TILETYPE>();
            CategoryData.FarmPlants = new HashSet<TILETYPE>();
            CategoryData._FarmPlants.Add(TILETYPE.SoilMound);
            CategoryData._FarmPlants.Add(TILETYPE.Cabbage_Wiltered);
            CategoryData._FarmPlants.Add(TILETYPE.Potato_Wiltered);
            CategoryData._FarmPlants.Add(TILETYPE.Sugarcane_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Sugarcane_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Sugarcane_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Raspberry_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Raspberry_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Raspberry_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Beetroot_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Beetroot_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Beetroot_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Bamboo_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Bamboo_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Bamboo_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Nightshade_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Nightshade_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Nightshade_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.CoffeeBerries_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.CoffeeBerries_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.CoffeeBerries_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Soybeans_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Soybeans_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Soybeans_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Lettuce_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Lettuce_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Lettuce_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Wheat_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Wheat_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Wheat_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Carrots_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Carrots_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Carrots_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Cabbage_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Cabbage_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Cabbage_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Corn_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Corn_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Corn_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Potato_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Potato_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Potato_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Watermelon_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Watermelon_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Watermelon_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Grass_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Grass_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Grass_FullGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Hops_SmallGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Hops_HalfGrown);
            CategoryData._FarmPlants.Add(TILETYPE.Hops_FullGrown);
            for (int index = 0; index < CategoryData._FarmPlants.Count; ++index)
              CategoryData.FarmPlants.Add(CategoryData._FarmPlants[index]);
            break;
          }
          break;
      }
      return (List<TILETYPE>) null;
    }

    internal static bool IsThisATree(TILETYPE tiletype)
    {
      if (CategoryData._Trees == null)
      {
        CategoryData._Trees = new List<TILETYPE>();
        CategoryData._Trees.Add(TILETYPE.ZooTree);
        CategoryData._Trees.Add(TILETYPE.GreenTree);
        CategoryData._Trees.Add(TILETYPE.PineTree);
        CategoryData._Trees.Add(TILETYPE.PineTreeDark);
        CategoryData._Trees.Add(TILETYPE.DesertCactusDeco);
        CategoryData._Trees.Add(TILETYPE.BigTree);
        CategoryData._Trees.Add(TILETYPE.SakuraTree);
        CategoryData._Trees.Add(TILETYPE.PalmTree);
        CategoryData._Trees.Add(TILETYPE.ColoredTree);
        CategoryData._Trees.Add(TILETYPE.Cactus);
        CategoryData._Trees.Add(TILETYPE.IcyTree);
        CategoryData._Trees.Add(TILETYPE.GiantTree);
        CategoryData._Trees.Add(TILETYPE.PalmTreesTall);
        CategoryData._Trees.Add(TILETYPE.GlowingPlant);
        CategoryData._Trees.Add(TILETYPE.TreeWithVines);
        CategoryData._Trees.Add(TILETYPE.DesertTree);
        CategoryData._Trees.Add(TILETYPE.CactusLong);
        CategoryData._Trees.Add(TILETYPE.LightGreenTree);
      }
      return CategoryData._Trees.Contains(tiletype);
    }
  }
}
