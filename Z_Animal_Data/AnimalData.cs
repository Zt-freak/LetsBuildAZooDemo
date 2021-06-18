// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.AnimalData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.FileInOut;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;

namespace TinyZoo.Z_Animal_Data
{
  internal class AnimalData
  {
    private static AnimalStat[] AnimalStats;
    private static int AllPopularity;
    private static HashSet<AnimalType> fottballers;

    internal static float SetUp(AnimalType animaltype)
    {
      AnimalData.AllPopularity = 0;
      if (AnimalData.AnimalStats == null)
      {
        AnimalData.AnimalStats = new AnimalStat[56];
        CSVFileReader csvFileReader = new CSVFileReader();
        csvFileReader.Read("Popularity.csv");
        for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
        {
          int stringToAnimal = (int) AnimalData.GetStringToAnimal(csvFileReader.GetCell(Row, 0));
          AnimalData.AnimalStats[stringToAnimal] = new AnimalStat();
          AnimalData.AnimalStats[stringToAnimal].Popularity = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 1));
          AnimalData.AllPopularity += (int) AnimalData.AnimalStats[stringToAnimal].Popularity;
          AnimalData.AnimalStats[stringToAnimal].Popularity *= 0.01f;
          AnimalData.AnimalStats[stringToAnimal].GroupSize = Convert.ToInt32(csvFileReader.GetCell(Row, 2));
          float int32 = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 3));
          AnimalData.AnimalStats[stringToAnimal].OverGroupSizeSubtraction = int32 * 0.01f;
          AnimalData.AnimalStats[stringToAnimal].OverGroupSizeMinimum = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 4));
          AnimalData.AnimalStats[stringToAnimal].OverGroupSizeMinimum *= 0.01f;
          AnimalData.AnimalStats[stringToAnimal].EnclosuerSpacePerAnimal = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 5));
          AnimalData.AnimalStats[stringToAnimal].EnclosuerSpacePerAnimal *= 0.5f;
          AnimalData.AnimalStats[stringToAnimal].TerritorySize = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 6));
          AnimalData.AnimalStats[stringToAnimal].Aggression = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 7));
          AnimalData.AnimalStats[stringToAnimal].Strength = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 8));
          AnimalData.AnimalStats[stringToAnimal].AttackSpeed = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 9));
          AnimalData.AnimalStats[stringToAnimal].Defence = (float) Convert.ToInt32(csvFileReader.GetCell(Row, 10));
          string cell = csvFileReader.GetCell(Row, 11);
          if (!(cell == "H"))
          {
            if (!(cell == "C"))
            {
              if (cell == "O")
                AnimalData.AnimalStats[stringToAnimal].diettype = DietType.Omnivore;
            }
            else
              AnimalData.AnimalStats[stringToAnimal].diettype = DietType.Carnivore;
          }
          else
            AnimalData.AnimalStats[stringToAnimal].diettype = DietType.Herbivore;
          AnimalData.AnimalStats[stringToAnimal].Compatibility_GrassLand = Convert.ToInt32(csvFileReader.GetCell(Row, 12));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Forest = Convert.ToInt32(csvFileReader.GetCell(Row, 13));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Desert = Convert.ToInt32(csvFileReader.GetCell(Row, 14));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Mountain = Convert.ToInt32(csvFileReader.GetCell(Row, 15));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Arctic = Convert.ToInt32(csvFileReader.GetCell(Row, 16));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Tropical = Convert.ToInt32(csvFileReader.GetCell(Row, 17));
          AnimalData.AnimalStats[stringToAnimal].Compatibility_Savannah = Convert.ToInt32(csvFileReader.GetCell(Row, 18));
        }
        EnrichmentData.SecondayLoadEnrichment();
      }
      return AnimalData.AnimalStats[(int) animaltype].Popularity;
    }

    internal static bool IsThisAFoorballer(AnimalType animal)
    {
      if (AnimalData.fottballers == null)
      {
        AnimalData.fottballers = new HashSet<AnimalType>();
        AnimalData.fottballers.Add(AnimalType.FootballCaptain);
        AnimalData.fottballers.Add(AnimalType.Footballer_Blond);
        AnimalData.fottballers.Add(AnimalType.Footballer_CombedBackHair);
        AnimalData.fottballers.Add(AnimalType.Footballer_Goalkeep);
        AnimalData.fottballers.Add(AnimalType.Footballer_SpikyHair);
      }
      return AnimalData.fottballers.Contains(animal);
    }

    internal static int GetPriceForAnimalFromSheler(AnimalType animaltype, int Varian)
    {
      if (AnimalData.AnimalStats == null || AnimalData.AnimalStats[(int) animaltype] == null)
      {
        double num = (double) AnimalData.SetUp(animaltype);
      }
      return (int) Math.Round((double) AnimalData.AnimalStats[(int) animaltype].Popularity * 10.0 + (double) Varian * ((double) AnimalData.AnimalStats[(int) animaltype].Popularity * 6.0));
    }

    internal static int GetAnimalWeight(AnimalType animaltype, bool isMale = true)
    {
      switch (animaltype)
      {
        case AnimalType.Rabbit:
          return 2;
        case AnimalType.Goose:
          return 2;
        case AnimalType.Capybara:
          return 50;
        case AnimalType.Pig:
          return 220;
        case AnimalType.Duck:
          return 1;
        case AnimalType.Snake:
          return 12;
        case AnimalType.Badger:
          return 10;
        case AnimalType.Hyena:
          return 130;
        case AnimalType.Porcupine:
          return 9;
        case AnimalType.Bear:
          return isMale ? 250 : 180;
        case AnimalType.Meerkat:
          return 1;
        case AnimalType.Horse:
          return 700;
        case AnimalType.Armadillo:
          return 5;
        case AnimalType.Donkey:
          return 200;
        case AnimalType.Cow:
          return 345;
        case AnimalType.Tapir:
          return 225;
        case AnimalType.Ostrich:
          return 105;
        case AnimalType.Tortoise:
          return isMale ? 227 : 250;
        case AnimalType.Chicken:
          return 2;
        case AnimalType.Camel:
          return 600;
        case AnimalType.Penguin:
          return 10;
        case AnimalType.Antelope:
          return isMale ? 53 : 41;
        case AnimalType.Panther:
          return isMale ? 78 : 56;
        case AnimalType.Seal:
          return 315;
        case AnimalType.Wolf:
          return isMale ? 55 : 39;
        case AnimalType.Lemur:
          return 2;
        case AnimalType.Alpaca:
          return 66;
        case AnimalType.KomodoDragon:
          return isMale ? 85 : 70;
        case AnimalType.Walrus:
          return 1000;
        case AnimalType.Orangutan:
          return isMale ? 75 : 40;
        case AnimalType.PolarBear:
          return isMale ? 500 : 200;
        case AnimalType.Skunk:
          return 3;
        case AnimalType.Crocodile:
          return isMale ? 450 : 100;
        case AnimalType.WildBoar:
          return isMale ? 88 : 70;
        case AnimalType.Peacock:
          return 5;
        case AnimalType.Platypus:
          return 1;
        case AnimalType.Deer:
          return isMale ? 80 : 50;
        case AnimalType.Monkey:
          return isMale ? 8 : 5;
        case AnimalType.Flamingo:
          return 3;
        case AnimalType.Gorilla:
          return isMale ? 180 : 90;
        case AnimalType.Tiger:
          return isMale ? 220 : 140;
        case AnimalType.Kangaroo:
          return isMale ? 70 : 30;
        case AnimalType.Beavers:
          return 25;
        case AnimalType.RedPanda:
          return isMale ? 5 : 4;
        case AnimalType.Zebra:
          return 400;
        case AnimalType.Fox:
          return 6;
        case AnimalType.Raccoon:
          return 6;
        case AnimalType.Elephant:
          return isMale ? 4000 : 3150;
        case AnimalType.Cheetah:
          return 50;
        case AnimalType.Otter:
          return isMale ? 36 : 20;
        case AnimalType.Owl:
          return 2;
        case AnimalType.Rhino:
          return 2200;
        case AnimalType.Panda:
          return isMale ? 105 : 85;
        case AnimalType.Giraffe:
          return isMale ? 1192 : 828;
        case AnimalType.Hippopotamus:
          return isMale ? 1500 : 1300;
        case AnimalType.Lion:
          return isMale ? 190 : 130;
        default:
          throw new Exception("No Weight!");
      }
    }

    internal static int GetIdealGroupSize(AnimalType animaltype)
    {
      if (AnimalData.AnimalStats == null)
      {
        double num = (double) AnimalData.SetUp(animaltype);
      }
      return AnimalData.AnimalStats[(int) animaltype].GroupSize;
    }

    internal static float GetOverGroupSizeSubtraction(AnimalType animaltype)
    {
      if (AnimalData.AnimalStats == null)
      {
        double num = (double) AnimalData.SetUp(animaltype);
      }
      return AnimalData.AnimalStats[(int) animaltype].OverGroupSizeSubtraction;
    }

    internal static float GetOverGroupSizeMinimum(AnimalType animaltype)
    {
      if (AnimalData.AnimalStats == null)
      {
        double num = (double) AnimalData.SetUp(animaltype);
      }
      return AnimalData.AnimalStats[(int) animaltype].OverGroupSizeMinimum;
    }

    internal static AnimalType GetFavouriteAnimal()
    {
      int num = Game1.Rnd.Next(0, AnimalData.AllPopularity);
      for (int index = 0; index < AnimalData.AnimalStats.Length; ++index)
      {
        num -= (int) ((double) AnimalData.AnimalStats[index].Popularity * 100.0);
        if (num <= 0)
          return (AnimalType) index;
      }
      return AnimalType.None;
    }

    internal static AnimalType GetStringToAnimal(string animalname)
    {
      switch (animalname)
      {
        case "Alpaca":
          return AnimalType.Alpaca;
        case "Antelope":
          return AnimalType.Antelope;
        case "Armadillo":
          return AnimalType.Armadillo;
        case "Badger":
          return AnimalType.Badger;
        case "Bear":
          return AnimalType.Bear;
        case "Beavers":
          return AnimalType.Beavers;
        case "Camel":
          return AnimalType.Camel;
        case "Capybara":
          return AnimalType.Capybara;
        case "Cheetah":
          return AnimalType.Cheetah;
        case "Chicken":
          return AnimalType.Chicken;
        case "Cow":
          return AnimalType.Cow;
        case "Crocodile":
          return AnimalType.Crocodile;
        case "Deer":
          return AnimalType.Deer;
        case "Donkey":
          return AnimalType.Donkey;
        case "Duck":
          return AnimalType.Duck;
        case "Elephant":
          return AnimalType.Elephant;
        case "Flamingo":
          return AnimalType.Flamingo;
        case "Fox":
          return AnimalType.Fox;
        case "Giraffe":
          return AnimalType.Giraffe;
        case "Goose":
          return AnimalType.Goose;
        case "Gorilla":
          return AnimalType.Gorilla;
        case "Hippopotamus":
          return AnimalType.Hippopotamus;
        case "Horse":
          return AnimalType.Horse;
        case "Hyena":
          return AnimalType.Hyena;
        case "Kangaroo":
          return AnimalType.Kangaroo;
        case "KomodoDragon":
          return AnimalType.KomodoDragon;
        case "Lemur":
          return AnimalType.Lemur;
        case "Lion":
          return AnimalType.Lion;
        case "Meerkat":
          return AnimalType.Meerkat;
        case "Monkey":
          return AnimalType.Monkey;
        case "OrangUtan":
          return AnimalType.Orangutan;
        case "Orangutan":
          return AnimalType.Orangutan;
        case "Ostrich":
          return AnimalType.Ostrich;
        case "Otter":
          return AnimalType.Otter;
        case "Owl":
          return AnimalType.Owl;
        case "Panda":
          return AnimalType.Panda;
        case "Panther":
          return AnimalType.Panther;
        case "Peacock":
          return AnimalType.Peacock;
        case "Penguin":
          return AnimalType.Penguin;
        case "Pig":
          return AnimalType.Pig;
        case "Platypus":
          return AnimalType.Platypus;
        case "PolarBear":
          return AnimalType.PolarBear;
        case "Porcupine":
          return AnimalType.Porcupine;
        case "Rabbit":
          return AnimalType.Rabbit;
        case "Raccoon":
          return AnimalType.Raccoon;
        case "RedPanda":
          return AnimalType.RedPanda;
        case "Rhino":
          return AnimalType.Rhino;
        case "Seal":
          return AnimalType.Seal;
        case "Skunk":
          return AnimalType.Skunk;
        case "Snake":
          return AnimalType.Snake;
        case "Tapir":
          return AnimalType.Tapir;
        case "Tiger":
          return AnimalType.Tiger;
        case "Tortoise":
          return AnimalType.Tortoise;
        case "Walrus":
          return AnimalType.Walrus;
        case "WildBoar":
          return AnimalType.WildBoar;
        case "Wolf":
          return AnimalType.Wolf;
        case "Zebra":
          return AnimalType.Zebra;
        default:
          throw new Exception("MISSED" + animalname);
      }
    }

    internal static NotificationAlertStatus GetHabitatAlert(
      CellBlockType cellblock,
      AnimalType animal)
    {
      int num;
      switch (cellblock)
      {
        case CellBlockType.Grasslands:
          num = AnimalData.GetAnimalStat(animal).Compatibility_GrassLand;
          break;
        case CellBlockType.Forest:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Forest;
          break;
        case CellBlockType.Savannah:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Savannah;
          break;
        case CellBlockType.Desert:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Desert;
          break;
        case CellBlockType.Mountain:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Mountain;
          break;
        case CellBlockType.Arctic:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Arctic;
          break;
        case CellBlockType.Tropical:
          num = AnimalData.GetAnimalStat(animal).Compatibility_Tropical;
          break;
        default:
          throw new Exception("NOT COVERED");
      }
      if (num == 100)
        return NotificationAlertStatus.Special_Heart;
      if (num > 90)
        return NotificationAlertStatus.Tick;
      return num > 60 ? NotificationAlertStatus.Exclamation : NotificationAlertStatus.Danger_Worst;
    }

    internal static AnimalStat GetAnimalStat(AnimalType animal)
    {
      if (AnimalData.AnimalStats == null)
      {
        double num = (double) AnimalData.SetUp(animal);
      }
      return AnimalData.AnimalStats[(int) animal];
    }

    internal static float GetRequiredFloorSpacePerAnimal(
      AnimalType animal,
      ref float MustHaveAtleastThisMuchSpace)
    {
      if (AnimalData.AnimalStats == null)
      {
        double num = (double) AnimalData.SetUp(animal);
      }
      MustHaveAtleastThisMuchSpace = AnimalData.AnimalStats[(int) animal].TerritorySize;
      return AnimalData.AnimalStats[(int) animal].EnclosuerSpacePerAnimal;
    }

    internal static int GetIdealSocialGroupNumber(AnimalType animal)
    {
      switch (animal)
      {
        case AnimalType.Rabbit:
          return 4;
        case AnimalType.Goose:
          return 2;
        case AnimalType.Capybara:
          return 4;
        case AnimalType.Pig:
          return 6;
        case AnimalType.Duck:
          return 2;
        case AnimalType.Snake:
          return 2;
        case AnimalType.Badger:
          return 4;
        case AnimalType.Hyena:
          return 8;
        case AnimalType.Porcupine:
          return 6;
        default:
          return (int) animal;
      }
    }

    internal static float GetBreedChance(AnimalType animal)
    {
      switch (animal)
      {
        case AnimalType.Rabbit:
          return 50f;
        case AnimalType.Goose:
        case AnimalType.Capybara:
        case AnimalType.Pig:
        case AnimalType.Duck:
        case AnimalType.Snake:
        case AnimalType.Badger:
        case AnimalType.Hyena:
        case AnimalType.Porcupine:
        case AnimalType.Bear:
        case AnimalType.Meerkat:
        case AnimalType.Horse:
        case AnimalType.Armadillo:
        case AnimalType.Donkey:
        case AnimalType.Cow:
        case AnimalType.Tapir:
        case AnimalType.Ostrich:
        case AnimalType.Tortoise:
        case AnimalType.Chicken:
        case AnimalType.Camel:
        case AnimalType.Penguin:
        case AnimalType.Antelope:
        case AnimalType.Panther:
        case AnimalType.Seal:
        case AnimalType.Wolf:
        case AnimalType.Lemur:
        case AnimalType.Alpaca:
        case AnimalType.KomodoDragon:
        case AnimalType.Walrus:
        case AnimalType.Orangutan:
        case AnimalType.PolarBear:
        case AnimalType.Skunk:
        case AnimalType.Crocodile:
        case AnimalType.WildBoar:
        case AnimalType.Peacock:
        case AnimalType.Platypus:
        case AnimalType.Deer:
        case AnimalType.Monkey:
        case AnimalType.Flamingo:
        case AnimalType.Gorilla:
        case AnimalType.Tiger:
        case AnimalType.Kangaroo:
        case AnimalType.Beavers:
        case AnimalType.RedPanda:
        case AnimalType.Zebra:
        case AnimalType.Fox:
        case AnimalType.Raccoon:
        case AnimalType.Elephant:
        case AnimalType.Cheetah:
        case AnimalType.Otter:
        case AnimalType.Owl:
        case AnimalType.Rhino:
        case AnimalType.Panda:
        case AnimalType.Giraffe:
        case AnimalType.Hippopotamus:
        case AnimalType.Lion:
          return 10f;
        default:
          return 10f;
      }
    }

    internal static string GetAnimalName(AnimalType animal) => EnemyData.GetEnemyTypeName(animal);

    internal static float GetLifeExectancy(AnimalType animal, bool IsAGirl, out int DaysAsABaby)
    {
      DaysAsABaby = 1;
      float num1;
      float num2;
      switch (animal)
      {
        case AnimalType.Rabbit:
          num1 = 6f;
          num2 = 24f;
          break;
        case AnimalType.Goose:
          num1 = 10f;
          num2 = 240f;
          break;
        case AnimalType.Capybara:
          num1 = 12f;
          num2 = 96f;
          break;
        case AnimalType.Pig:
          num1 = 5f;
          num2 = 60f;
          break;
        case AnimalType.Duck:
          num1 = 4f;
          num2 = 120f;
          break;
        case AnimalType.Snake:
          num1 = 18f;
          num2 = 240f;
          break;
        case AnimalType.Badger:
          num1 = 12f;
          num2 = 108f;
          break;
        case AnimalType.Hyena:
          num1 = 24f;
          num2 = 144f;
          break;
        case AnimalType.Porcupine:
          num1 = 24f;
          num2 = 96f;
          break;
        case AnimalType.Bear:
          num1 = 36f;
          num2 = 300f;
          break;
        case AnimalType.Meerkat:
          num1 = 12f;
          num2 = 96f;
          break;
        case AnimalType.Horse:
          num1 = 12f;
          num2 = 180f;
          break;
        case AnimalType.Armadillo:
          num1 = 9f;
          num2 = 144f;
          break;
        case AnimalType.Cow:
          num1 = 11f;
          num2 = 180f;
          break;
        case AnimalType.Tapir:
          num1 = 36f;
          num2 = 300f;
          break;
        case AnimalType.Ostrich:
          num1 = 30f;
          num2 = 600f;
          break;
        case AnimalType.Tortoise:
          num1 = 120f;
          num2 = 960f;
          break;
        case AnimalType.Chicken:
          num1 = 1f;
          num2 = 24f;
          break;
        case AnimalType.Camel:
          num1 = 48f;
          num2 = 480f;
          break;
        case AnimalType.Penguin:
          num1 = 36f;
          num2 = 180f;
          break;
        case AnimalType.Antelope:
          num1 = 13f;
          num2 = 120f;
          break;
        case AnimalType.Panther:
          num1 = 6f;
          num2 = 240f;
          break;
        case AnimalType.Seal:
          num1 = 36f;
          num2 = 360f;
          break;
        case AnimalType.Wolf:
          num1 = 22f;
          num2 = 96f;
          break;
        case AnimalType.Lemur:
          num1 = 18f;
          num2 = 180f;
          break;
        case AnimalType.Alpaca:
          num1 = 12f;
          num2 = 240f;
          break;
        case AnimalType.KomodoDragon:
          num1 = 96f;
          num2 = 360f;
          break;
        case AnimalType.Walrus:
          num1 = 60f;
          num2 = 480f;
          break;
        case AnimalType.Orangutan:
          num1 = 120f;
          num2 = 420f;
          break;
        case AnimalType.PolarBear:
          num1 = 60f;
          num2 = 300f;
          break;
        case AnimalType.Skunk:
          num1 = 10f;
          num2 = 84f;
          break;
        case AnimalType.Crocodile:
          num1 = 120f;
          num2 = 600f;
          break;
        case AnimalType.WildBoar:
          num1 = 18f;
          num2 = 240f;
          break;
        case AnimalType.Peacock:
          num1 = 11f;
          num2 = 240f;
          break;
        case AnimalType.Platypus:
          num1 = 12f;
          num2 = 240f;
          break;
        case AnimalType.Deer:
          num1 = 6f;
          num2 = 132f;
          break;
        case AnimalType.Monkey:
          num1 = 18f;
          num2 = 300f;
          break;
        case AnimalType.Flamingo:
          num1 = 6f;
          num2 = 240f;
          break;
        case AnimalType.Gorilla:
          num1 = 84f;
          num2 = 480f;
          break;
        case AnimalType.Tiger:
          num1 = 48f;
          num2 = 312f;
          break;
        case AnimalType.Kangaroo:
          num1 = 20f;
          num2 = 72f;
          break;
        case AnimalType.Beavers:
          num1 = 24f;
          num2 = 120f;
          break;
        case AnimalType.RedPanda:
          num1 = 18f;
          num2 = 84f;
          break;
        case AnimalType.Zebra:
          num1 = 12f;
          num2 = 240f;
          break;
        case AnimalType.Fox:
          num1 = 10f;
          num2 = 108f;
          break;
        case AnimalType.Raccoon:
          num1 = 12f;
          num2 = 24f;
          break;
        case AnimalType.Elephant:
          num1 = 120f;
          num2 = 840f;
          break;
        case AnimalType.Cheetah:
          num1 = 20f;
          num2 = 144f;
          break;
        case AnimalType.Otter:
          num1 = 24f;
          num2 = 144f;
          break;
        case AnimalType.Owl:
          num1 = 12f;
          num2 = 108f;
          break;
        case AnimalType.Rhino:
          num1 = 96f;
          num2 = 540f;
          break;
        case AnimalType.Panda:
          num1 = 18f;
          num2 = 180f;
          break;
        case AnimalType.Giraffe:
          num1 = 36f;
          num2 = 312f;
          break;
        case AnimalType.Hippopotamus:
          num1 = 60f;
          num2 = 480f;
          break;
        case AnimalType.Lion:
          num1 = 36f;
          num2 = 180f;
          break;
        default:
          return 10f;
      }
      DaysAsABaby = (int) ((double) num1 * (double) Z_GameFlags.DaysInOneYear * 0.0833333283662796);
      return (float) (int) ((double) num2 * (double) Z_GameFlags.DaysInOneYear);
    }

    internal static float GetEnclosureMultiplier(AnimalType animal, CellBlockType cellblocktype)
    {
      switch (cellblocktype)
      {
        case CellBlockType.Grasslands:
          switch (animal)
          {
            case AnimalType.Rabbit:
            case AnimalType.Goose:
            case AnimalType.Capybara:
            case AnimalType.Pig:
            case AnimalType.Duck:
            case AnimalType.Badger:
            case AnimalType.Horse:
            case AnimalType.Cow:
            case AnimalType.Chicken:
              return 1f;
            case AnimalType.Snake:
            case AnimalType.Hyena:
            case AnimalType.Porcupine:
            case AnimalType.Meerkat:
            case AnimalType.Armadillo:
            case AnimalType.Ostrich:
            case AnimalType.Tortoise:
              return 0.75f;
            case AnimalType.Bear:
            case AnimalType.Panther:
            case AnimalType.Alpaca:
              return 0.4f;
            case AnimalType.Donkey:
            case AnimalType.Tapir:
            case AnimalType.Antelope:
              return 0.45f;
            case AnimalType.Camel:
              return 0.3f;
            case AnimalType.Penguin:
            case AnimalType.Seal:
            case AnimalType.Walrus:
            case AnimalType.PolarBear:
              return 0.0f;
            case AnimalType.Wolf:
              return 0.2f;
            case AnimalType.Lemur:
            case AnimalType.KomodoDragon:
            case AnimalType.Orangutan:
            case AnimalType.Skunk:
            case AnimalType.Platypus:
            case AnimalType.Monkey:
            case AnimalType.Gorilla:
              return 0.22f;
            case AnimalType.Crocodile:
              return 0.05f;
          }
          break;
        case CellBlockType.Forest:
          switch (animal)
          {
            case AnimalType.WildBoar:
            case AnimalType.Peacock:
            case AnimalType.Deer:
            case AnimalType.Beavers:
            case AnimalType.RedPanda:
            case AnimalType.Fox:
            case AnimalType.Raccoon:
            case AnimalType.Otter:
            case AnimalType.Owl:
            case AnimalType.Panda:
              return 1f;
          }
          break;
        case CellBlockType.Savannah:
          switch (animal)
          {
            case AnimalType.Flamingo:
            case AnimalType.Tiger:
            case AnimalType.Kangaroo:
            case AnimalType.Zebra:
            case AnimalType.Elephant:
            case AnimalType.Cheetah:
            case AnimalType.Rhino:
            case AnimalType.Giraffe:
            case AnimalType.Hippopotamus:
            case AnimalType.Lion:
              return 1f;
          }
          break;
        case CellBlockType.Desert:
          switch (animal)
          {
            case AnimalType.Snake:
            case AnimalType.Hyena:
            case AnimalType.Porcupine:
            case AnimalType.Meerkat:
            case AnimalType.Armadillo:
            case AnimalType.Ostrich:
            case AnimalType.Tortoise:
            case AnimalType.Camel:
              return 1f;
          }
          break;
        case CellBlockType.Mountain:
          if (animal <= AnimalType.Tapir)
          {
            if (animal != AnimalType.Bear && animal != AnimalType.Donkey && animal != AnimalType.Tapir)
              break;
          }
          else if (animal != AnimalType.Antelope && animal != AnimalType.Panther && animal != AnimalType.Alpaca)
            break;
          return 1f;
        case CellBlockType.Arctic:
          switch (animal)
          {
            case AnimalType.Penguin:
            case AnimalType.Seal:
            case AnimalType.Wolf:
            case AnimalType.Walrus:
            case AnimalType.PolarBear:
              return 1f;
          }
          break;
        case CellBlockType.Tropical:
          switch (animal)
          {
            case AnimalType.Lemur:
            case AnimalType.KomodoDragon:
            case AnimalType.Orangutan:
            case AnimalType.Skunk:
            case AnimalType.Crocodile:
            case AnimalType.Platypus:
            case AnimalType.Monkey:
            case AnimalType.Gorilla:
              return 1f;
          }
          break;
      }
      return 0.1f;
    }

    public static string GetDietTypeToString(DietType dietType)
    {
      switch (dietType)
      {
        case DietType.Omnivore:
          return "Omnivore";
        case DietType.Carnivore:
          return "Carnivore";
        case DietType.Herbivore:
          return "Herbivore";
        default:
          return "NA_DIET";
      }
    }

    public static CellBlockType GetBestEnclosureTypeForThisAnimal(AnimalType animalType)
    {
      AnimalStat animalStat = AnimalData.GetAnimalStat(animalType);
      CellBlockType cellBlockType = CellBlockType.Count;
      float num1 = 0.0f;
      for (int index = 0; index < 11; ++index)
      {
        float num2 = 0.0f;
        switch (index)
        {
          case 0:
            num2 = (float) animalStat.Compatibility_GrassLand;
            break;
          case 1:
            num2 = (float) animalStat.Compatibility_Forest;
            break;
          case 2:
            num2 = (float) animalStat.Compatibility_Savannah;
            break;
          case 3:
            num2 = (float) animalStat.Compatibility_Desert;
            break;
          case 4:
            num2 = (float) animalStat.Compatibility_Mountain;
            break;
          case 5:
            num2 = (float) animalStat.Compatibility_Arctic;
            break;
          case 6:
            num2 = (float) animalStat.Compatibility_Tropical;
            break;
        }
        if ((double) num2 > (double) num1)
        {
          num1 = num2;
          cellBlockType = (CellBlockType) index;
        }
      }
      return cellBlockType;
    }

    internal static AnimalType GetThisPaintedAnimalOther(AnimalType animal)
    {
      if (animal == AnimalType.Horse)
        return AnimalType.Zebra;
      return animal == AnimalType.Zebra ? AnimalType.Horse : AnimalType.None;
    }
  }
}
