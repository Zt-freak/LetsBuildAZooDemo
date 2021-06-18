// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.AnimalFoodData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.FileInOut;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_StoreRoom
{
  internal class AnimalFoodData
  {
    private static AnimalFoodInfo[] animalfoodinfo;
    internal static FoodCollection[] foodcollections;

    internal static AnimalFoodInfo GetAnimalFoodInfo(AnimalFoodType thisfood)
    {
      if (AnimalFoodData.animalfoodinfo == null)
      {
        AnimalFoodData.animalfoodinfo = new AnimalFoodInfo[88];
        for (int index = 0; index < 88; ++index)
        {
          switch (index)
          {
            case 0:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(578, 104, 28, 26), 3, 100, 5);
              break;
            case 1:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(655, 81, 27, 26), 2, 100, 6);
              break;
            case 2:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(633, 109, 25, 26), 2, 100, 8);
              break;
            case 3:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(629, 82, 25, 26), 2, 100, 10);
              break;
            case 4:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(607, 109, 25, 26), 3, 100, 9);
              break;
            case 5:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(500, 273, 27, 16), 7, 100, 50);
              break;
            case 6:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(559, 129, 22, 25), 13, 100, 100);
              break;
            case 7:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(659, 108, 26, 28), 13, 100, 15);
              break;
            case 8:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(771, 125, 29, 29), 13, 100, 20);
              break;
            case 9:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(801, 33, 28, 29), 13, 100, 80);
              break;
            case 10:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(801, 126, 20, 28), 13, 100, 80);
              break;
            case 11:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(858, 96, 30, 30), 13, 100, 95);
              break;
            case 12:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(777, 82, 23, 19), 13, 100, 0);
              break;
            case 13:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(828, 81, 30, 20), 13, 100, 140);
              break;
            case 14:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(801, 102, 24, 23), 13, 100, 110);
              break;
            case 15:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1002, 32, 27, 27), 13, 100, 0);
              break;
            case 16:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(711, 130, 29, 24), 13, 100, 150);
              break;
            case 17:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(654, 137, 31, 17), 13, 100, 200);
              break;
            case 18:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(768, 103, 32, 21), 13, 100, 90);
              break;
            case 19:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(743, 47, 31, 23), 13, 100, 120);
              break;
            case 20:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(740, 71, 22, 25), 13, 100, 210);
              break;
            case 21:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(933, 0, 30, 29), 13, 100, 400);
              break;
            case 22:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(775, 61, 25, 20), 13, 100, 150);
              break;
            case 23:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(713, 79, 26, 25), 13, 100, 190);
              break;
            case 24:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(854, (int) sbyte.MaxValue, 31, 27), 13, 100, 160);
              break;
            case 25:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(914, 84, 30, 21), 13, 100, 150);
              break;
            case 26:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(686, 124, 24, 30), 13, 100, 150);
              break;
            case 27:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(831, 63, 24, 17), 13, 100, 150);
              break;
            case 28:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(830, 33, 30, 29), 13, 100, 150);
              break;
            case 29:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(582, 131, 21, 23), 13, 100, 150);
              break;
            case 30:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(713, 105, 27, 24), 13, 100, 150);
              break;
            case 31:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(689, 74, 19, 25), 13, 100, 150);
              break;
            case 32:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(801, 63, 29, 23), 13, 100, 150);
              break;
            case 33:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(741, 97, 26, 29), 13, 100, 150);
              break;
            case 34:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(889, 33, 25, 28), 13, 100, 150);
              break;
            case 35:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(686, 100, 26, 23), 13, 100, 150);
              break;
            case 36:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1036, 130, 28, 24), 13, 100, 150);
              break;
            case 37:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(942, 30, 25, 24), 13, 100, 150);
              break;
            case 38:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(861, 33, 26, 28), 13, 100, 150);
              break;
            case 39:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(822, 128, 31, 26), 13, 100, 150);
              break;
            case 40:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(888, 86, 25, 20), 13, 100, 150);
              break;
            case 41:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(920, 129, 28, 25), 13, 100, 150);
              break;
            case 42:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(964, 0, 40, 26), 13, 100, 150);
              break;
            case 43:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(886, 128, 33, 26), 13, 100, 150);
              break;
            case 44:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(826, 102, 31, 25), 13, 100, 150);
              break;
            case 45:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(949, 128, 30, 26), 13, 100, 150);
              break;
            case 46:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(980, 133, 28, 21), 13, 100, 150);
              break;
            case 47:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(968, 27, 33, 24), 13, 100, 150);
              break;
            case 48:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(857, 62, 31, 25), 13, 100, 150);
              break;
            case 49:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(889, 62, 26, 23), 13, 100, 150);
              break;
            case 50:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(914, 106, 28, 22), 13, 100, 150);
              break;
            case 51:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(944, 55, 23, 22), 13, 100, 150);
              break;
            case 52:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(604, 136, 25, 18), 13, 100, 150);
              break;
            case 53:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1005, 0, 31, 31), 13, 100, 150);
              break;
            case 54:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(968, 52, 30, 28), 13, 100, 150);
              break;
            case 55:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(741, 129, 28, 25), 13, 100, 150);
              break;
            case 56:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(943, 102, 26, 25), 13, 100, 150);
              break;
            case 57:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(780, 33, 20, 27), 13, 100, 150);
              break;
            case 58:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(915, 33, 26, 27), 13, 100, 150);
              break;
            case 59:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(916, 61, 27, 22), 13, 100, 150);
              break;
            case 60:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(889, 107, 24, 20), 13, 100, 150);
              break;
            case 61:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1009, 129, 26, 25), 13, 100, 150);
              break;
            case 62:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1454, 128, 32, 24), 13, 100, 150);
              break;
            case 63:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1487, 128, 25, 26), 13, 100, 150);
              break;
            case 64:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1559, 46, 33, 23), 13, 100, 150);
              break;
            case 65:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(945, 78, 24, 23), 13, 100, 150);
              break;
            case 66:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1516, 96, 29, 27), 13, 100, 150);
              break;
            case 67:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1029, 710, 27, 24), 13, 100, 150);
              break;
            case 68:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1942, 329, 28, 26), 13, 100, 150);
              break;
            case 69:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1686, 410, 28, 21), 13, 100, 150);
              break;
            case 70:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1643, 531, 30, 27), 13, 100, 150);
              break;
            case 71:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1674, 525, 26, 34), 13, 100, 150);
              break;
            case 72:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1701, 529, 30, 29), 13, 100, 150);
              break;
            case 73:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1057, 709, 24, 25), 13, 100, 150);
              break;
            case 74:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1082, 708, 24, 26), 13, 100, 150);
              break;
            case 75:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1964, 310, 29, 18), 13, 100, 150);
              break;
            case 76:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1715, 410, 26, 24), 13, 100, 150);
              break;
            case 77:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1943, 290, 28, 18), 13, 100, 150);
              break;
            case 78:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1742, 410, 22, 25), 13, 100, 150);
              break;
            case 79:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1752, 436, 22, 21), 13, 100, 150);
              break;
            case 80:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1972, 290, 26, 19), 13, 100, 150);
              break;
            case 81:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1765, 410, 28, 23), 13, 100, 150);
              break;
            case 82:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1397, 868, 26, 21), 13, 100, 150);
              break;
            case 83:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1851, 290, 20, 21), 13, 100, 150);
              break;
            case 84:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1639, 410, 22, 21), 13, 100, 150);
              break;
            case 85:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1710, 435, 20, 22), 13, 100, 150);
              break;
            case 86:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(1732, 531, 23, 28), 13, 100, 150);
              break;
            case 87:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(730, 47, 12, 9), 13, 100, 150);
              break;
            default:
              AnimalFoodData.animalfoodinfo[index] = new AnimalFoodInfo(new Rectangle(500, 273, 27, 16), 13, 100, 150);
              break;
          }
        }
        CSVFileReader csvFileReader = new CSVFileReader();
        csvFileReader.Read("Food.csv");
        for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
        {
          AnimalFoodType toAnimalFoodType = AnimalFoodData.GetStringToAnimalFoodType(csvFileReader.GetCell(Row, 0));
          AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost = Convert.ToInt32(csvFileReader.GetCell(Row, 1));
          AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].ShelfLife = Convert.ToInt32(csvFileReader.GetCell(Row, 2));
          AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].DeliveryTime = Convert.ToInt32(csvFileReader.GetCell(Row, 3));
          if (Z_DebugFlags.IsBetaVersion)
          {
            AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].DeliveryTime = AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].DeliveryTime <= 7 ? 1 : 2;
            AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost /= 2;
            if (AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost > 15)
              AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost = 15 + (AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost - 15) / 3;
            if (AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost == 0)
              AnimalFoodData.animalfoodinfo[(int) toAnimalFoodType].Cost = 1;
          }
        }
      }
      return AnimalFoodData.animalfoodinfo[(int) thisfood];
    }

    internal static AnimalFoodType GetStringToAnimalFoodType(string FoodType)
    {
      switch (FoodType)
      {
        case "AlligatorSkin":
          return AnimalFoodType.AlligatorSkin;
        case "Apples":
          return AnimalFoodType.Apples;
        case "Bamboo":
          return AnimalFoodType.Bamboo;
        case "Banana":
          return AnimalFoodType.Bananas;
        case "Beans":
          return AnimalFoodType.Beans;
        case "Berries":
          return AnimalFoodType.Berries;
        case "Blended Pellets":
          return AnimalFoodType.BlendedPellets;
        case "Bones":
          return AnimalFoodType.Bones;
        case "Branches":
          return AnimalFoodType.Branches;
        case "Bread":
          return AnimalFoodType.Bread;
        case "Carrion":
          return AnimalFoodType.Carrion;
        case "Carrots":
          return AnimalFoodType.Carrots;
        case "ChickenMeat":
          return AnimalFoodType.ChickenMeat;
        case "Clams":
          return AnimalFoodType.Clams;
        case "CoffeeBerry":
          return AnimalFoodType.CoffeeBerries;
        case "Corn":
          return AnimalFoodType.Corn;
        case "EarthWorms":
          return AnimalFoodType.EarthWorms;
        case "Eggs":
          return AnimalFoodType.Eggs;
        case "Fish":
          return AnimalFoodType.Fish;
        case "Fruit":
          return AnimalFoodType.Fruit;
        case "Garbage":
          return AnimalFoodType.Garbage;
        case "Gelatin":
          return AnimalFoodType.Gelatin;
        case "Grains":
          return AnimalFoodType.Grains;
        case "Grass":
          return AnimalFoodType.Grass;
        case "Greens":
          return AnimalFoodType.Greens;
        case "Grit":
          return AnimalFoodType.Grit;
        case "Hay":
          return AnimalFoodType.Hay;
        case "HippoPoop":
          return AnimalFoodType.HippoPoop;
        case "Honey":
          return AnimalFoodType.Honey;
        case "Insects":
          return AnimalFoodType.Insects;
        case "Krill":
          return AnimalFoodType.Krill;
        case "LargeCarcass":
          return AnimalFoodType.LargeCarcass;
        case "LargeFish":
          return AnimalFoodType.LargeFish;
        case "Leaves":
          return AnimalFoodType.Leaves;
        case "LeftOvers":
          return AnimalFoodType.LeftOvers;
        case "Lettuce":
          return AnimalFoodType.Lettuce;
        case "MealWorms":
          return AnimalFoodType.MealWorms;
        case "Mealworms":
          return AnimalFoodType.MealWorms;
        case "Meat Pellet":
          return AnimalFoodType.MeatPellet;
        case "Mice":
          return AnimalFoodType.Mice;
        case "Nightshade":
          return AnimalFoodType.Nightshade;
        case "OceanFlakes":
          return AnimalFoodType.OceanFlakes;
        case "Offspring":
          return AnimalFoodType.Offspring;
        case "PigmentTablets":
          return AnimalFoodType.PigmentTablets;
        case "Plants":
          return AnimalFoodType.Plants;
        case "Poop":
          return AnimalFoodType.Poop;
        case "Pork":
          return AnimalFoodType.Pork;
        case "PreparedMeat":
          return AnimalFoodType.PreparedMeat;
        case "Roots":
          return AnimalFoodType.Roots;
        case "SaltBlock":
          return AnimalFoodType.SaltBlock;
        case "SaltBush":
          return AnimalFoodType.SaltBush;
        case "Scorpions":
          return AnimalFoodType.Scorpions;
        case "Seeds":
          return AnimalFoodType.Seeds;
        case "Shrimps":
          return AnimalFoodType.Shrimps;
        case "SmallCarcass":
          return AnimalFoodType.SmallCarcass;
        case "Snails":
          return AnimalFoodType.Snails;
        case "SnakeSkin":
          return AnimalFoodType.SnakeSkin;
        case "Spiders":
          return AnimalFoodType.Spiders;
        case "Squid":
          return AnimalFoodType.Squid;
        case "Straw":
          return AnimalFoodType.Straw;
        case "SugarCubes":
          return AnimalFoodType.SugarCubes;
        case "Termites":
          return AnimalFoodType.Termites;
        case "ThornyShrubs":
          return AnimalFoodType.ThornyShrubs;
        case "TreeBark":
          return AnimalFoodType.TreeBark;
        case "Vegetable Pellets":
          return AnimalFoodType.VegetablePellets;
        case "Vegetables":
          return AnimalFoodType.RootVegetables;
        case "WaterMelon":
          return AnimalFoodType.WaterMelon;
        case "WaterPlants":
          return AnimalFoodType.WaterPlants;
        default:
          throw new Exception("MSSED" + FoodType);
      }
    }

    internal static bool IsThisMeat(AnimalFoodType animalfood)
    {
      if (animalfood <= AnimalFoodType.OceanFlakes)
      {
        if (animalfood != AnimalFoodType.MeatPellet && animalfood != AnimalFoodType.OceanFlakes)
          goto label_4;
      }
      else if (animalfood != AnimalFoodType.SmallCarcass && animalfood != AnimalFoodType.Fish)
        goto label_4;
      return true;
label_4:
      return false;
    }

    internal static void SetUpNutritionAndHardyness()
    {
      float num1 = 0.0f;
      float num2 = 100000f;
      float num3 = 0.0f;
      for (int index1 = 0; index1 < 56; ++index1)
      {
        double num4 = (double) AnimalData.SetUp((AnimalType) index1);
        float num5 = 0.0f;
        for (int index2 = 0; index2 < AnimalFoodData.foodcollections[index1].animalfoodentry.Count; ++index2)
        {
          AnimalFoodData.foodcollections[index1].animalfoodentry[index2].CostTemp = (float) AnimalFoodData.GetAnimalFoodInfo(AnimalFoodData.foodcollections[index1].animalfoodentry[index2].foodtype).Cost * AnimalFoodData.foodcollections[index1].animalfoodentry[index2].PercentOfDailyIdeal * AnimalFoodData.foodcollections[index1].FullDailyFoodRquirement;
          num5 += AnimalFoodData.foodcollections[index1].animalfoodentry[index2].CostTemp;
        }
        AnimalFoodData.foodcollections[index1].PriceForEverythingTemp = num5;
        num3 += num5;
        if ((double) num2 > (double) num5)
          num2 = num5;
        if ((double) num1 < (double) num5)
          num1 = num5;
      }
      float num6 = 0.0f;
      for (int index1 = 0; index1 < 56; ++index1)
      {
        AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized = (float) (((double) AnimalFoodData.foodcollections[index1].PriceForEverythingTemp - (double) num2) / ((double) num1 - (double) num2));
        if ((double) AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized > 1.0 || (double) AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized < 0.0)
          throw new Exception("Maximum Esceeded");
        AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized = (float) ((double) AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized * 0.800000011920929 + 0.200000002980232);
        float num4 = (float) ((double) AnimalData.SetUp((AnimalType) index1) * 0.800000011920929 + 0.200000002980232);
        AnimalFoodData.foodcollections[index1].Hardynees = AnimalFoodData.foodcollections[index1].RelativeCostOfIngredientsNormalized / num4;
        if ((double) num6 < (double) AnimalFoodData.foodcollections[index1].Hardynees)
          num6 = AnimalFoodData.foodcollections[index1].Hardynees;
        for (int index2 = 0; index2 < AnimalFoodData.foodcollections[index1].animalfoodentry.Count; ++index2)
        {
          AnimalFoodData.foodcollections[index1].animalfoodentry[index2].CostNormalized = AnimalFoodData.foodcollections[index1].animalfoodentry[index2].CostTemp / AnimalFoodData.foodcollections[index1].PriceForEverythingTemp;
          AnimalFoodData.foodcollections[index1].animalfoodentry[index2].NutritionValue = AnimalFoodData.foodcollections[index1].animalfoodentry[index2].CostNormalized;
        }
      }
      for (int index = 0; index < 56; ++index)
        AnimalFoodData.foodcollections[index].Hardynees /= num6;
    }

    internal static float GetHardyness(AnimalType animal)
    {
      if (AnimalFoodData.foodcollections == null)
        AnimalFoodData.SetUpNutritionAndHardyness();
      return AnimalFoodData.foodcollections[(int) animal].Hardynees;
    }

    internal static FoodCollection GetFoodCollection(AnimalType animal)
    {
      if (AnimalFoodData.foodcollections == null)
        AnimalFoodData.foodcollections = new FoodCollection[56];
      if (AnimalFoodData.foodcollections[(int) animal] == null)
      {
        CSVFileReader csvFileReader = new CSVFileReader();
        csvFileReader.Read("AnimalFoodRequirements.csv");
        for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
        {
          AnimalType stringToAnimal = AnimalData.GetStringToAnimal(csvFileReader.GetCell(Row, 0));
          float int32 = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 10));
          AnimalFoodData.foodcollections[(int) stringToAnimal] = new FoodCollection(int32 * 0.01f);
          for (int index = 0; index < 4; ++index)
          {
            AnimalFoodEntry foodentry = new AnimalFoodEntry(AnimalFoodData.GetStringToAnimalFoodType(csvFileReader.GetCell(Row, 2 + index)), (float) Convert.ToInt32(csvFileReader.GetCell(Row, 6 + index)) * 0.01f);
            AnimalFoodData.foodcollections[(int) stringToAnimal].AddNewFoodType(foodentry);
          }
        }
        AnimalFoodData.SetUpNutritionAndHardyness();
      }
      return AnimalFoodData.foodcollections[(int) animal];
    }

    internal static string GetAnimalFoodTypeToString(AnimalFoodType thisfood)
    {
      switch (thisfood)
      {
        case AnimalFoodType.Straw:
          return SEngine.Localization.Localization.GetText(663);
        case AnimalFoodType.VegetablePellets:
          return SEngine.Localization.Localization.GetText(665);
        case AnimalFoodType.BlendedPellets:
          return SEngine.Localization.Localization.GetText(669);
        case AnimalFoodType.MeatPellet:
          return SEngine.Localization.Localization.GetText(664);
        case AnimalFoodType.OceanFlakes:
          return SEngine.Localization.Localization.GetText(668);
        case AnimalFoodType.Carrots:
          return SEngine.Localization.Localization.GetText(666);
        case AnimalFoodType.Grains:
          return SEngine.Localization.Localization.GetText(667);
        case AnimalFoodType.Greens:
          return SEngine.Localization.Localization.GetText(670);
        case AnimalFoodType.Grass:
          return SEngine.Localization.Localization.GetText(671);
        case AnimalFoodType.Bread:
          return SEngine.Localization.Localization.GetText(673);
        case AnimalFoodType.MealWorms:
          return SEngine.Localization.Localization.GetText(674);
        case AnimalFoodType.WaterPlants:
          return SEngine.Localization.Localization.GetText(675);
        case AnimalFoodType.Poop:
          return SEngine.Localization.Localization.GetText(676);
        case AnimalFoodType.RootVegetables:
          return SEngine.Localization.Localization.GetText(677);
        case AnimalFoodType.Beans:
          return SEngine.Localization.Localization.GetText(678);
        case AnimalFoodType.LeftOvers:
          return SEngine.Localization.Localization.GetText(679);
        case AnimalFoodType.Eggs:
          return SEngine.Localization.Localization.GetText(680);
        case AnimalFoodType.SmallCarcass:
          return SEngine.Localization.Localization.GetText(681);
        case AnimalFoodType.Insects:
          return SEngine.Localization.Localization.GetText(682);
        case AnimalFoodType.EarthWorms:
          return SEngine.Localization.Localization.GetText(683);
        case AnimalFoodType.Apples:
          return SEngine.Localization.Localization.GetText(684);
        case AnimalFoodType.Carrion:
          return SEngine.Localization.Localization.GetText(685);
        case AnimalFoodType.Berries:
          return SEngine.Localization.Localization.GetText(686);
        case AnimalFoodType.TreeBark:
          return SEngine.Localization.Localization.GetText(687);
        case AnimalFoodType.Roots:
          return SEngine.Localization.Localization.GetText(688);
        case AnimalFoodType.Fish:
          return SEngine.Localization.Localization.GetText(689);
        case AnimalFoodType.Honey:
          return SEngine.Localization.Localization.GetText(690);
        case AnimalFoodType.Spiders:
          return SEngine.Localization.Localization.GetText(691);
        case AnimalFoodType.Scorpions:
          return SEngine.Localization.Localization.GetText(692);
        case AnimalFoodType.CoffeeBerries:
          return SEngine.Localization.Localization.GetText(693);
        case AnimalFoodType.SugarCubes:
          return SEngine.Localization.Localization.GetText(694);
        case AnimalFoodType.Plants:
          return SEngine.Localization.Localization.GetText(695);
        case AnimalFoodType.SaltBlock:
          return SEngine.Localization.Localization.GetText(696);
        case AnimalFoodType.Bananas:
          return SEngine.Localization.Localization.GetText(697);
        case AnimalFoodType.Seeds:
          return SEngine.Localization.Localization.GetText(698);
        case AnimalFoodType.Lettuce:
          return SEngine.Localization.Localization.GetText(699);
        case AnimalFoodType.Grit:
          return SEngine.Localization.Localization.GetText(672);
        case AnimalFoodType.ThornyShrubs:
          return SEngine.Localization.Localization.GetText(700);
        case AnimalFoodType.SaltBush:
          return SEngine.Localization.Localization.GetText(701);
        case AnimalFoodType.Squid:
          return SEngine.Localization.Localization.GetText(702);
        case AnimalFoodType.Krill:
          return SEngine.Localization.Localization.GetText(703);
        case AnimalFoodType.Branches:
          return SEngine.Localization.Localization.GetText(704);
        case AnimalFoodType.LargeCarcass:
          return SEngine.Localization.Localization.GetText(705);
        case AnimalFoodType.PreparedMeat:
          return SEngine.Localization.Localization.GetText(706);
        case AnimalFoodType.LargeFish:
          return SEngine.Localization.Localization.GetText(707);
        case AnimalFoodType.Hay:
          return SEngine.Localization.Localization.GetText(662);
        case AnimalFoodType.Fruit:
          return SEngine.Localization.Localization.GetText(661);
        case AnimalFoodType.Offspring:
          return SEngine.Localization.Localization.GetText(708);
        case AnimalFoodType.Clams:
          return SEngine.Localization.Localization.GetText(709);
        case AnimalFoodType.Nightshade:
          return SEngine.Localization.Localization.GetText(710);
        case AnimalFoodType.HippoPoop:
          return "Hippo Poop";
        case AnimalFoodType.Leaves:
          return SEngine.Localization.Localization.GetText(735);
        case AnimalFoodType.Bamboo:
          return SEngine.Localization.Localization.GetText(411);
        case AnimalFoodType.Bones:
          return SEngine.Localization.Localization.GetText(713);
        case AnimalFoodType.Corn:
          return SEngine.Localization.Localization.GetText(733);
        case AnimalFoodType.WaterMelon:
          return SEngine.Localization.Localization.GetText(736);
        case AnimalFoodType.Pork:
          return SEngine.Localization.Localization.GetText(711);
        case AnimalFoodType.AlligatorSkin:
          return SEngine.Localization.Localization.GetText(715);
        case AnimalFoodType.SnakeSkin:
          return SEngine.Localization.Localization.GetText(716);
        case AnimalFoodType.Gelatin:
          return SEngine.Localization.Localization.GetText(712);
        case AnimalFoodType.ChickenMeat:
          return SEngine.Localization.Localization.GetText(714);
        case AnimalFoodType.HorseHoof:
          return SEngine.Localization.Localization.GetText(717);
        case AnimalFoodType.CrocHandbag:
          return SEngine.Localization.Localization.GetText(718);
        case AnimalFoodType.ChickenWing:
          return SEngine.Localization.Localization.GetText(719);
        case AnimalFoodType.Bacon:
          return SEngine.Localization.Localization.GetText(720);
        case AnimalFoodType.Glue:
          return SEngine.Localization.Localization.GetText(721);
        case AnimalFoodType.SnakeSkinBelt:
          return SEngine.Localization.Localization.GetText(722);
        case AnimalFoodType.Hops:
          return SEngine.Localization.Localization.GetText(724);
        case AnimalFoodType.BeerFromProcessor:
          return SEngine.Localization.Localization.GetText(723);
        case AnimalFoodType.FlourFromProcessor:
          return SEngine.Localization.Localization.GetText(725);
        case AnimalFoodType.PopcornFromProcessor:
          return SEngine.Localization.Localization.GetText(726);
        case AnimalFoodType.StarchFromProcessor:
          return SEngine.Localization.Localization.GetText(727);
        case AnimalFoodType.NaturalJuiceFromProcessor:
          return SEngine.Localization.Localization.GetText(728);
        case AnimalFoodType.LuwakFromProcessor:
          return SEngine.Localization.Localization.GetText(729);
        case AnimalFoodType.NaturalColouringFromProcessor:
          return SEngine.Localization.Localization.GetText(730);
        case AnimalFoodType.CreamIconFromProcessor:
          return SEngine.Localization.Localization.GetText(731);
        case AnimalFoodType.ImpossibleMeat:
          return SEngine.Localization.Localization.GetText(732);
        case AnimalFoodType.Potato:
          return SEngine.Localization.Localization.GetText(734);
        case AnimalFoodType.Soybean:
          return SEngine.Localization.Localization.GetText(739);
        case AnimalFoodType.Beetroot:
          return SEngine.Localization.Localization.GetText(737);
        case AnimalFoodType.Fructose:
          return SEngine.Localization.Localization.GetText(738);
        default:
          return "NA_" + (object) thisfood;
      }
    }

    internal static string GetEater(AnimalFoodType thisfood)
    {
      switch (thisfood)
      {
        case AnimalFoodType.Straw:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.VegetablePellets:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.BlendedPellets:
          return SEngine.Localization.Localization.GetText(745);
        case AnimalFoodType.MeatPellet:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.OceanFlakes:
          return SEngine.Localization.Localization.GetText(745);
        case AnimalFoodType.Carrots:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Grains:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Greens:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Grass:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Bread:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.MealWorms:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.WaterPlants:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Poop:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.RootVegetables:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Beans:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.LeftOvers:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Eggs:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.SmallCarcass:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Insects:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.EarthWorms:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Apples:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Carrion:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Berries:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.TreeBark:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Roots:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Fish:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Honey:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Spiders:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Scorpions:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.CoffeeBerries:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.SugarCubes:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Plants:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.SaltBlock:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Bananas:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Seeds:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Lettuce:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Grit:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.ThornyShrubs:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.SaltBush:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Squid:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Krill:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Branches:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.LargeCarcass:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.PreparedMeat:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.LargeFish:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Hay:
          return SEngine.Localization.Localization.GetText(743);
        case AnimalFoodType.Fruit:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Offspring:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.Clams:
          return SEngine.Localization.Localization.GetText(744);
        case AnimalFoodType.Nightshade:
          return SEngine.Localization.Localization.GetText(742);
        case AnimalFoodType.HippoPoop:
          return SEngine.Localization.Localization.GetText(742);
        default:
          return "I AM A LONG test string look at me";
      }
    }
  }
}
