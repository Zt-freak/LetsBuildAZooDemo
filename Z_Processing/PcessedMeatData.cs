// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Processing.PcessedMeatData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_Processing
{
  internal class PcessedMeatData
  {
    private static AnimalProductionList[] animalproductionLists;
    private static List<AnimalFoodType> foodstoconvert;

    internal static List<AnimalFoodType> GetConvertableAnimalFoodTypesInDisplayOrder()
    {
      if (PcessedMeatData.foodstoconvert == null)
      {
        PcessedMeatData.foodstoconvert = new List<AnimalFoodType>();
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.Pork);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.ChickenMeat);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.AlligatorSkin);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.SnakeSkin);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.Gelatin);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.Bones);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.Carrion);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.PreparedMeat);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.LargeCarcass);
        PcessedMeatData.foodstoconvert.Add(AnimalFoodType.SmallCarcass);
      }
      return PcessedMeatData.foodstoconvert;
    }

    internal static AnimalProductionList GetAnmalToBaseMeatType(
      AnimalType animal)
    {
      if (PcessedMeatData.animalproductionLists == null)
        PcessedMeatData.animalproductionLists = new AnimalProductionList[70];
      if (PcessedMeatData.animalproductionLists[(int) animal] == null)
      {
        switch (animal)
        {
          case AnimalType.Rabbit:
          case AnimalType.Goose:
          case AnimalType.Capybara:
          case AnimalType.Duck:
          case AnimalType.Meerkat:
          case AnimalType.Lemur:
          case AnimalType.Peacock:
          case AnimalType.Platypus:
          case AnimalType.Monkey:
          case AnimalType.Flamingo:
          case AnimalType.Beavers:
          case AnimalType.RedPanda:
          case AnimalType.Otter:
          case AnimalType.Owl:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[2]
            {
              AnimalFoodType.SmallCarcass,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f);
            break;
          case AnimalType.Pig:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Pork,
              AnimalFoodType.Gelatin,
              AnimalFoodType.Bones,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f, 1f, 1f);
            break;
          case AnimalType.Snake:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[2]
            {
              AnimalFoodType.SnakeSkin,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f);
            break;
          case AnimalType.Badger:
          case AnimalType.Camel:
          case AnimalType.Antelope:
          case AnimalType.Panther:
          case AnimalType.Seal:
          case AnimalType.Wolf:
          case AnimalType.Alpaca:
          case AnimalType.Orangutan:
          case AnimalType.PolarBear:
          case AnimalType.Gorilla:
          case AnimalType.Elephant:
          case AnimalType.Rhino:
          case AnimalType.Hippopotamus:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.PreparedMeat,
              AnimalFoodType.LargeCarcass,
              AnimalFoodType.Bones,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f, 1f, 1f);
            break;
          case AnimalType.Hyena:
          case AnimalType.Porcupine:
          case AnimalType.Armadillo:
          case AnimalType.Tortoise:
          case AnimalType.Penguin:
          case AnimalType.KomodoDragon:
          case AnimalType.Walrus:
          case AnimalType.Skunk:
          case AnimalType.Fox:
          case AnimalType.Raccoon:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[1]
            {
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f);
            break;
          case AnimalType.Bear:
          case AnimalType.Tapir:
          case AnimalType.Ostrich:
          case AnimalType.WildBoar:
          case AnimalType.Deer:
          case AnimalType.Tiger:
          case AnimalType.Kangaroo:
          case AnimalType.Zebra:
          case AnimalType.Cheetah:
          case AnimalType.Panda:
          case AnimalType.Giraffe:
          case AnimalType.Lion:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.PreparedMeat,
              AnimalFoodType.LargeCarcass,
              AnimalFoodType.Bones,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f, 1f, 1f);
            break;
          case AnimalType.Horse:
          case AnimalType.Cow:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.PreparedMeat,
              AnimalFoodType.Gelatin,
              AnimalFoodType.LargeCarcass,
              AnimalFoodType.Bones,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f, 1f, 1f, 1f);
            break;
          case AnimalType.Donkey:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Gelatin,
              AnimalFoodType.LargeCarcass,
              AnimalFoodType.Bones,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f, 1f, 1f);
            break;
          case AnimalType.Chicken:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[2]
            {
              AnimalFoodType.ChickenMeat,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f);
            break;
          case AnimalType.Crocodile:
            PcessedMeatData.animalproductionLists[(int) animal] = new AnimalProductionList(new AnimalFoodType[2]
            {
              AnimalFoodType.AlligatorSkin,
              AnimalFoodType.Carrion
            });
            PcessedMeatData.animalproductionLists[(int) animal].SetYield(1f, 1f);
            break;
          default:
            throw new Exception("Animal Not Added");
        }
      }
      return PcessedMeatData.animalproductionLists[(int) animal];
    }

    public static bool CanProcessThisProduce(AnimalFoodType foodType, Player player)
    {
      int numberBuilt;
      return PcessedMeatData.HasPlayerResearchedBuildingToProcessThisProduce(foodType, player, out numberBuilt) && numberBuilt != 0;
    }

    public static bool HasPlayerResearchedBuildingToProcessThisProduce(
      AnimalFoodType foodType,
      Player player,
      out int numberBuilt)
    {
      bool flag = true;
      numberBuilt = -1;
      TILETYPE tiletype = TILETYPE.Count;
      switch (foodType)
      {
        case AnimalFoodType.Pork:
          tiletype = TILETYPE.BaconFactory;
          break;
        case AnimalFoodType.AlligatorSkin:
          tiletype = TILETYPE.CrocHandbagFactory;
          break;
        case AnimalFoodType.SnakeSkin:
          tiletype = TILETYPE.SnakeSkinFactory;
          break;
        case AnimalFoodType.ChickenMeat:
          tiletype = TILETYPE.BuffaloWingFactory;
          break;
        case AnimalFoodType.HorseHoof:
          tiletype = TILETYPE.GlueFactory;
          break;
        case AnimalFoodType.BeerFromProcessor:
          tiletype = TILETYPE.BeerBrewery;
          break;
        case AnimalFoodType.FlourFromProcessor:
          tiletype = TILETYPE.Windmill;
          break;
      }
      if (tiletype != TILETYPE.Count)
      {
        flag = player.Stats.research.BuildingsResearched.Contains(tiletype);
        if (flag)
          numberBuilt = player.shopstatus.GetTotalOfThese(tiletype);
      }
      return flag;
    }

    public static List<AnimalFoodType> GetProductOutputFromFactory(
      TILETYPE tileType)
    {
      List<AnimalFoodType> animalFoodTypeList = new List<AnimalFoodType>();
      switch (tileType)
      {
        case TILETYPE.GlueFactory:
          animalFoodTypeList.Add(AnimalFoodType.Glue);
          break;
        case TILETYPE.BuffaloWingFactory:
          animalFoodTypeList.Add(AnimalFoodType.ChickenWing);
          break;
        case TILETYPE.BaconFactory:
          animalFoodTypeList.Add(AnimalFoodType.Bacon);
          break;
        case TILETYPE.CrocHandbagFactory:
          animalFoodTypeList.Add(AnimalFoodType.CrocHandbag);
          break;
        case TILETYPE.SnakeSkinFactory:
          animalFoodTypeList.Add(AnimalFoodType.SnakeSkinBelt);
          break;
        case TILETYPE.BeerBrewery:
          animalFoodTypeList.Add(AnimalFoodType.BeerFromProcessor);
          break;
        case TILETYPE.Windmill:
          animalFoodTypeList.Add(AnimalFoodType.FlourFromProcessor);
          animalFoodTypeList.Add(AnimalFoodType.Bread);
          break;
      }
      return animalFoodTypeList;
    }

    public static AnimalFoodType GetProductInputForFactory(TILETYPE tileType)
    {
      switch (tileType)
      {
        case TILETYPE.GlueFactory:
          return AnimalFoodType.HorseHoof;
        case TILETYPE.BuffaloWingFactory:
          return AnimalFoodType.ChickenMeat;
        case TILETYPE.BaconFactory:
          return AnimalFoodType.Pork;
        case TILETYPE.CrocHandbagFactory:
          return AnimalFoodType.AlligatorSkin;
        case TILETYPE.SnakeSkinFactory:
          return AnimalFoodType.SnakeSkin;
        case TILETYPE.BeerBrewery:
          return AnimalFoodType.Hops;
        case TILETYPE.Windmill:
          return AnimalFoodType.Grains;
        default:
          return AnimalFoodType.Count;
      }
    }

    public static int GetMaxCapacityForThisBuilding(TILETYPE tileType) => 99;
  }
}
