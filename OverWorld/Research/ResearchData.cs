// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResearchData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.Research
{
  internal class ResearchData
  {
    private static List<TILETYPE> CellTypeInOrder;
    private static List<TILETYPE> BuildingTypesInOrder;
    private static List<AnimalType> AliensReseachedInOrder;

    internal static List<TILETYPE> GetCellTypeInOrder()
    {
      if (ResearchData.CellTypeInOrder == null)
      {
        ResearchData.CellTypeInOrder = new List<TILETYPE>();
        ResearchData.CellTypeInOrder.Add(TILETYPE.DesertEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.MountainEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.ArcticEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.TropicalEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.SavannahEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.ForestEnclosure);
        ResearchData.CellTypeInOrder.Add(TILETYPE.FieldPicketFenceEnclosure);
      }
      return ResearchData.CellTypeInOrder;
    }

    internal static List<TILETYPE> GetBuildingTypesInOrder()
    {
      if (ResearchData.BuildingTypesInOrder == null)
      {
        ResearchData.BuildingTypesInOrder = new List<TILETYPE>();
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.SmallGiftShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.IceCreamTruck);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.CottonCandyShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.LionHotDogShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.BigIceCreamShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.CoconutShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.PandaBurgerShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.BalloonShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.ElephantGiftShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.KangarooPizzaShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.SlushieShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.ChurroShop);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.SnacksVendingMachine);
        ResearchData.BuildingTypesInOrder.Add(TILETYPE.DrinksVendingMachine);
      }
      return ResearchData.BuildingTypesInOrder;
    }

    internal static List<AnimalType> GetAliensReseachedInOrder()
    {
      if (ResearchData.AliensReseachedInOrder == null)
      {
        ResearchData.AliensReseachedInOrder = new List<AnimalType>();
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Walrus);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Goose);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Capybara);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Duck);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Snake);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Badger);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Hyena);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Porcupine);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Bear);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Meerkat);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Horse);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Armadillo);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Donkey);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Cow);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Tapir);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Ostrich);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Tortoise);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Chicken);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Camel);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Penguin);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Antelope);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Panther);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Pig);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Seal);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Wolf);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Lemur);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Alpaca);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.KomodoDragon);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Orangutan);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.PolarBear);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Peacock);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Crocodile);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.WildBoar);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Platypus);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Deer);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Monkey);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Flamingo);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Gorilla);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Tiger);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Kangaroo);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Beavers);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.RedPanda);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Zebra);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Fox);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Raccoon);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Elephant);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Cheetah);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Otter);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Lion);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Skunk);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Rhino);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Panda);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Giraffe);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Hippopotamus);
        ResearchData.AliensReseachedInOrder.Add(AnimalType.Owl);
      }
      return ResearchData.AliensReseachedInOrder;
    }

    internal static int GetReaserchTimeAliens(int ThisResearchIndex)
    {
      if (GameFlags.IsConsoleVersion)
      {
        switch (ThisResearchIndex)
        {
          case 0:
            return 1;
          case 1:
            return 4;
          case 2:
            return 12;
          default:
            return 12 + (ThisResearchIndex - 2) * 5;
        }
      }
      else
      {
        int num;
        switch (ThisResearchIndex)
        {
          case 0:
            num = 1;
            break;
          case 1:
            num = 30;
            break;
          case 2:
            num = 120;
            break;
          case 3:
            num = 480;
            break;
          default:
            num = (ThisResearchIndex - 2) * 960;
            break;
        }
        return num;
      }
    }

    internal static int GetReaserchTimeBuildings(int ThisResearchIndex)
    {
      if (GameFlags.IsConsoleVersion)
      {
        switch (ThisResearchIndex)
        {
          case 0:
            return 1;
          case 1:
            return 4;
          case 2:
            return 12;
          default:
            return 12 + (ThisResearchIndex - 2) * 10;
        }
      }
      else
      {
        int num;
        switch (ThisResearchIndex)
        {
          case 0:
            num = 5;
            break;
          case 1:
            num = 60;
            break;
          default:
            num = (ThisResearchIndex - 1) * 12 * 120;
            break;
        }
        return num;
      }
    }

    internal static int GetReaserchTimeCellBlocks(int ThisResearchIndex)
    {
      if (GameFlags.IsConsoleVersion)
      {
        switch (ThisResearchIndex)
        {
          case 0:
            return 1;
          case 1:
            return 4;
          case 2:
            return 12;
          default:
            return 12 + (ThisResearchIndex - 2) * 15;
        }
      }
      else
      {
        int num;
        switch (ThisResearchIndex)
        {
          case 0:
            num = 480;
            break;
          case 1:
            num = 1260;
            break;
          default:
            num = (ThisResearchIndex + 1) * 2400;
            break;
        }
        return num;
      }
    }
  }
}
