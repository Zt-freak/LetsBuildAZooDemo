// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_FoodData.AnimalFoodIconData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_FoodData
{
  internal class AnimalFoodIconData
  {
    private static AFoodIconInfo[] animalfodicondata;

    internal static AFoodIconInfo GetAnimalFoodIconData(AnimalFoodType animalfoodtype)
    {
      if (AnimalFoodIconData.animalfodicondata == null)
        AnimalFoodIconData.animalfodicondata = new AFoodIconInfo[88];
      if (AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] == null)
      {
        switch (animalfoodtype)
        {
          case AnimalFoodType.Straw:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(598, 233, 16, 16), new Rectangle(615, 233, 16, 16), new Rectangle(632, 233, 16, 16));
            break;
          case AnimalFoodType.VegetablePellets:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle((int) byte.MaxValue, 382, 15, 15), new Rectangle(293, 303, 15, 15), new Rectangle(273, 317, 15, 15));
            break;
          case AnimalFoodType.BlendedPellets:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle((int) byte.MaxValue, 350, 15, 15), new Rectangle((int) byte.MaxValue, 366, 15, 15), new Rectangle((int) byte.MaxValue, 414, 15, 15));
            break;
          case AnimalFoodType.MeatPellet:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(272, 333, 15, 15), new Rectangle(271, 349, 15, 15), new Rectangle(271, 365, 15, 15));
            break;
          case AnimalFoodType.OceanFlakes:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle((int) byte.MaxValue, 398, 15, 15), new Rectangle(271, 381, 15, 15), new Rectangle(271, 397, 15, 15));
            break;
          case AnimalFoodType.Carrots:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(531, 233, 16, 16), new Rectangle(548, 233, 16, 16), new Rectangle((int) byte.MaxValue, 333, 16, 16));
            break;
          case AnimalFoodType.Grains:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(565, 234, 15, 15), new Rectangle(309, 304, 15, 15), new Rectangle(271, 413, 15, 15));
            break;
          case AnimalFoodType.Greens:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1465, 113, 16, 14), new Rectangle(1482, 113, 16, 14), new Rectangle(1499, 113, 16, 14));
            break;
          case AnimalFoodType.Grass:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(289, 319, 16, 16), new Rectangle(288, 336, 16, 16), new Rectangle(287, 353, 16, 16));
            break;
          case AnimalFoodType.Bread:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(352, 313, 16, 14), new Rectangle(369, 315, 16, 14), new Rectangle(386, 315, 16, 14));
            break;
          case AnimalFoodType.MealWorms:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1037, 0, 14, 14), new Rectangle(1037, 15, 14, 14), new Rectangle(1030, 30, 14, 14));
            break;
          case AnimalFoodType.WaterPlants:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1163, 78, 16, 16), new Rectangle(1180, 78, 16, 16), new Rectangle(1197, 78, 16, 16));
            break;
          case AnimalFoodType.Poop:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1097, 0, 16, 15), new Rectangle(1097, 16, 16, 15), new Rectangle(1090, 32, 16, 15));
            break;
          case AnimalFoodType.RootVegetables:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1093, 48, 16, 15), new Rectangle(1110, 48, 16, 15), new Rectangle(1127, 48, 16, 15));
            break;
          case AnimalFoodType.Beans:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1046, 45, 15, 15), new Rectangle(1062, 45, 15, 15), new Rectangle(1030, 45, 15, 15));
            break;
          case AnimalFoodType.LeftOvers:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1158, 45, 16, 15), new Rectangle(1175, 45, 16, 15), new Rectangle(1192, 45, 16, 15));
            break;
          case AnimalFoodType.Eggs:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1043, 61, 16, 16), new Rectangle(1061, 61, 16, 16), new Rectangle(1078, 61, 16, 16));
            break;
          case AnimalFoodType.SmallCarcass:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1078, 45, 13, 10), new Rectangle(1144, 48, 13, 10), new Rectangle(763, 86, 13, 10));
            break;
          case AnimalFoodType.Insects:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1225, 0, 16, 16), new Rectangle(1225, 17, 16, 16), new Rectangle(1225, 34, 16, 16));
            break;
          case AnimalFoodType.EarthWorms:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1165, 0, 14, 14), new Rectangle(1165, 15, 14, 14), new Rectangle(1165, 30, 14, 14));
            break;
          case AnimalFoodType.Apples:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(338, 413, 16, 15), new Rectangle(355, 413, 16, 15), new Rectangle(372, 413, 16, 15));
            break;
          case AnimalFoodType.Carrion:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1095, 64, 13, 13), new Rectangle(1109, 64, 13, 13), new Rectangle(1123, 64, 13, 13));
            break;
          case AnimalFoodType.Berries:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1112, 78, 16, 15), new Rectangle(1129, 78, 16, 15), new Rectangle(1146, 78, 16, 15));
            break;
          case AnimalFoodType.TreeBark:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1112, 108, 16, 12), new Rectangle(1129, 108, 16, 12), new Rectangle(1146, 108, 16, 12));
            break;
          case AnimalFoodType.Roots:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1144, 61, 15, 16), new Rectangle(1160, 61, 15, 16), new Rectangle(1176, 61, 15, 16));
            break;
          case AnimalFoodType.Fish:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(306, 320, 16, 16), new Rectangle(306, 337, 16, 16), new Rectangle(304, 354, 16, 16));
            break;
          case AnimalFoodType.Honey:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(993, 90, 16, 15), new Rectangle(1010, 90, 16, 15), new Rectangle(1027, 90, 16, 15));
            break;
          case AnimalFoodType.Spiders:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1044, 78, 16, 16), new Rectangle(1044, 95, 16, 16), new Rectangle(1044, 112, 16, 16));
            break;
          case AnimalFoodType.Scorpions:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1293, 0, 15, 14), new Rectangle(1293, 15, 15, 14), new Rectangle(1293, 30, 15, 14));
            break;
          case AnimalFoodType.CoffeeBerries:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1163, 95, 16, 13), new Rectangle(1180, 95, 16, 13), new Rectangle(1197, 95, 16, 13));
            break;
          case AnimalFoodType.SugarCubes:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1010, 76, 16, 13), new Rectangle(1027, 76, 16, 13), new Rectangle(993, 76, 16, 13));
            break;
          case AnimalFoodType.Plants:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(999, 60, 14, 15), new Rectangle(1014, 60, 14, 15), new Rectangle(1029, 60, 14, 15));
            break;
          case AnimalFoodType.SaltBlock:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1112, 94, 16, 13), new Rectangle(1129, 94, 16, 13), new Rectangle(1146, 94, 16, 13));
            break;
          case AnimalFoodType.Bananas:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(993, 106, 16, 15), new Rectangle(1010, 106, 16, 15), new Rectangle(1027, 106, 16, 15));
            break;
          case AnimalFoodType.Seeds:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1477, 48, 15, 15), new Rectangle(1493, 48, 15, 15), new Rectangle(1509, 48, 15, 15));
            break;
          case AnimalFoodType.Lettuce:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1392, 48, 16, 14), new Rectangle(1409, 48, 16, 14), new Rectangle(1426, 48, 16, 14));
            break;
          case AnimalFoodType.Grit:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1344, 48, 15, 15), new Rectangle(1360, 48, 15, 15), new Rectangle(1376, 48, 15, 15));
            break;
          case AnimalFoodType.ThornyShrubs:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1525, 48, 16, 15), new Rectangle(1542, 48, 16, 15), new Rectangle(1511, 64, 16, 15));
            break;
          case AnimalFoodType.SaltBush:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1293, 45, 16, 16), new Rectangle(1310, 45, 16, 16), new Rectangle(1327, 45, 16, 16));
            break;
          case AnimalFoodType.Squid:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1192, 61, 16, 16), new Rectangle(1209, 61, 16, 16), new Rectangle(1226, 61, 16, 16));
            break;
          case AnimalFoodType.Krill:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1557, 0, 15, 14), new Rectangle(1557, 15, 15, 14), new Rectangle(1557, 30, 15, 14));
            break;
          case AnimalFoodType.Branches:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1414, 94, 16, 16), new Rectangle(1431, 94, 16, 16), new Rectangle(1448, 94, 16, 16));
            break;
          case AnimalFoodType.LargeCarcass:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1528, 64, 16, 15), new Rectangle(1516, 80, 16, 15), new Rectangle(1533, 80, 16, 15));
            break;
          case AnimalFoodType.PreparedMeat:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1163, 109, 16, 16), new Rectangle(1180, 109, 16, 16), new Rectangle(1197, 109, 16, 16));
            break;
          case AnimalFoodType.LargeFish:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1465, 97, 16, 15), new Rectangle(1482, 97, 16, 15), new Rectangle(1499, 97, 16, 15));
            break;
          case AnimalFoodType.Hay:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(483, 273, 16, 16), new Rectangle(581, 233, 16, 16), new Rectangle(460, 312, 16, 16));
            break;
          case AnimalFoodType.Fruit:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1414, 78, 16, 15), new Rectangle(1431, 78, 16, 15), new Rectangle(1448, 78, 16, 15));
            break;
          case AnimalFoodType.Offspring:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1350, 78, 15, 16), new Rectangle(1350, 95, 15, 16), new Rectangle(1350, 112, 15, 16));
            break;
          case AnimalFoodType.Clams:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1344, 64, 16, 13), new Rectangle(1361, 64, 16, 13), new Rectangle(1378, 64, 16, 13));
            break;
          case AnimalFoodType.Nightshade:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1443, 48, 16, 14), new Rectangle(1460, 48, 16, 14), new Rectangle(1446, 63, 16, 14));
            break;
          case AnimalFoodType.HippoPoop:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1489, 0, 16, 15), new Rectangle(1489, 16, 16, 15), new Rectangle(1489, 32, 16, 15));
            break;
          case AnimalFoodType.Mice:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1282, 78, 16, 16), new Rectangle(1282, 95, 16, 16), new Rectangle(1282, 112, 16, 16));
            break;
          case AnimalFoodType.Garbage:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1421, 0, 16, 15), new Rectangle(1421, 16, 16, 15), new Rectangle(1421, 32, 16, 15));
            break;
          case AnimalFoodType.Termites:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1214, 78, 16, 16), new Rectangle(1214, 95, 16, 16), new Rectangle(1214, 112, 16, 16));
            break;
          case AnimalFoodType.Leaves:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1395, 63, 16, 14), new Rectangle(1412, 63, 16, 14), new Rectangle(1429, 63, 16, 14));
            break;
          case AnimalFoodType.PigmentTablets:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1293, 62, 16, 15), new Rectangle(1310, 62, 16, 15), new Rectangle(1327, 62, 16, 15));
            break;
          case AnimalFoodType.Bamboo:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1465, 80, 16, 16), new Rectangle(1482, 80, 16, 16), new Rectangle(1499, 80, 16, 16));
            break;
          case AnimalFoodType.Bones:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1414, 111, 16, 16), new Rectangle(1431, 111, 16, 16), new Rectangle(1448, 111, 16, 16));
            break;
          case AnimalFoodType.Snails:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1357, 0, 15, 15), new Rectangle(1357, 16, 15, 15), new Rectangle(1357, 32, 15, 15));
            break;
          case AnimalFoodType.Corn:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(287, 413, 16, 15), new Rectangle(304, 413, 16, 15), new Rectangle(321, 413, 16, 15));
            break;
          case AnimalFoodType.WaterMelon:
            AnimalFoodIconData.animalfodicondata[(int) animalfoodtype] = new AFoodIconInfo(new Rectangle(1463, 64, 15, 15), new Rectangle(1479, 64, 15, 15), new Rectangle(1495, 64, 15, 15));
            break;
        }
      }
      return AnimalFoodIconData.animalfodicondata[(int) animalfoodtype];
    }

    internal static Vector3 GetAnimalFoodColour(AnimalFoodType animalfoodtype)
    {
      switch (animalfoodtype)
      {
        case AnimalFoodType.Straw:
          return new Vector3(0.8f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.VegetablePellets:
          return new Vector3(0.3f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.3f);
        case AnimalFoodType.BlendedPellets:
          return new Vector3(0.7f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.MeatPellet:
          return new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.OceanFlakes:
          return new Vector3(0.0f, MathStuff.getRandomFloat(0.4f, 0.8f), 1f);
        case AnimalFoodType.Carrots:
          return TinyZoo.Game1.Rnd.Next(0, 30) == 0 ? new Vector3(0.0f, 0.5f, 0.0f) : new Vector3(1f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Greens:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(0.4f, MathStuff.getRandomFloat(0.6f, 0.9f), 0.2f);
        case AnimalFoodType.Grass:
          return new Vector3(0.3f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.3f);
        case AnimalFoodType.Bread:
          return new Vector3(0.6f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.MealWorms:
          return new Vector3(0.8f, MathStuff.getRandomFloat(0.8f, 1f), 0.8f);
        case AnimalFoodType.WaterPlants:
          return new Vector3(0.4f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.2f);
        case AnimalFoodType.Poop:
          return new Vector3(0.5f, MathStuff.getRandomFloat(0.3f, 0.5f), 0.0f);
        case AnimalFoodType.RootVegetables:
          if (TinyZoo.Game1.Rnd.Next(0, 3) == 0)
            return new Vector3(0.5f, 0.4f, 0.0f);
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(0.3f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.3f);
        case AnimalFoodType.Beans:
          return new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.2f, 0.0f);
        case AnimalFoodType.LeftOvers:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.5f, 0.4f, 0.0f) : new Vector3(1f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Eggs:
          float randomFloat1 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(0.9f, randomFloat1, randomFloat1 - 0.2f);
        case AnimalFoodType.SmallCarcass:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.5f, 0.4f, 0.0f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.Insects:
          return new Vector3(0.5f, MathStuff.getRandomFloat(0.3f, 0.5f), 0.0f);
        case AnimalFoodType.EarthWorms:
          float randomFloat2 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(1f, randomFloat2, randomFloat2 - 0.1f);
        case AnimalFoodType.Apples:
          return TinyZoo.Game1.Rnd.Next(0, 10) == 0 ? new Vector3(1f, 0.0f, 0.0f) : new Vector3(0.7f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Carrion:
          float randomFloat3 = MathStuff.getRandomFloat(0.4f, 0.6f);
          return new Vector3(1f, randomFloat3, randomFloat3 - 0.1f);
        case AnimalFoodType.Berries:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.6f, 0.2f, 0.0f) : new Vector3(0.0f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.9f);
        case AnimalFoodType.TreeBark:
          return new Vector3(0.6f, MathStuff.getRandomFloat(0.3f, 0.5f), 0.0f);
        case AnimalFoodType.Roots:
          return new Vector3(0.5f, MathStuff.getRandomFloat(0.3f, 0.5f), 0.0f);
        case AnimalFoodType.Fish:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.Honey:
          return new Vector3(0.7f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Spiders:
          float randomFloat4 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat4, randomFloat4, randomFloat4);
        case AnimalFoodType.Scorpions:
          float randomFloat5 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat5, randomFloat5, randomFloat5);
        case AnimalFoodType.CoffeeBerries:
          float randomFloat6 = MathStuff.getRandomFloat(0.3f, 0.5f);
          return new Vector3(0.9f, randomFloat6, randomFloat6);
        case AnimalFoodType.SugarCubes:
          float randomFloat7 = MathStuff.getRandomFloat(0.7f, 0.9f);
          return new Vector3(randomFloat7, randomFloat7, randomFloat7);
        case AnimalFoodType.Plants:
          return TinyZoo.Game1.Rnd.Next(0, 10) == 0 ? new Vector3(0.6f, 0.4f, 0.0f) : new Vector3(0.3f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.3f);
        case AnimalFoodType.SaltBlock:
          float randomFloat8 = MathStuff.getRandomFloat(0.7f, 0.9f);
          return new Vector3(1f, randomFloat8, randomFloat8);
        case AnimalFoodType.Bananas:
          return new Vector3(0.6f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Seeds:
          float randomFloat9 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat9, randomFloat9, randomFloat9);
        case AnimalFoodType.Lettuce:
          return new Vector3(0.4f, MathStuff.getRandomFloat(0.5f, 0.9f), 0.2f);
        case AnimalFoodType.Grit:
          if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
            return new Vector3(0.5f, 0.4f, 0.0f);
          float randomFloat10 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat10, randomFloat10, randomFloat10);
        case AnimalFoodType.ThornyShrubs:
          return new Vector3(0.4f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.2f);
        case AnimalFoodType.SaltBush:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(0.4f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.2f);
        case AnimalFoodType.Squid:
          float randomFloat11 = MathStuff.getRandomFloat(0.6f, 0.8f);
          return new Vector3(1f, randomFloat11, randomFloat11 - 0.1f);
        case AnimalFoodType.Krill:
          float randomFloat12 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(1f, randomFloat12, randomFloat12 - 0.2f);
        case AnimalFoodType.Branches:
          return new Vector3(0.6f, MathStuff.getRandomFloat(0.3f, 0.5f), 0.0f);
        case AnimalFoodType.LargeCarcass:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.LargeFish:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.Hay:
          return new Vector3(0.7f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Fruit:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.3f, 0.3f, 0.6f) : new Vector3(0.7f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.Offspring:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.Clams:
          if (TinyZoo.Game1.Rnd.Next(0, 3) == 0)
            return new Vector3(0.7f, 0.6f, 0.0f);
          float randomFloat13 = MathStuff.getRandomFloat(0.6f, 0.9f);
          return new Vector3(randomFloat13, randomFloat13, randomFloat13);
        case AnimalFoodType.Nightshade:
          float randomFloat14 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat14, randomFloat14, randomFloat14);
        case AnimalFoodType.Shrimps:
          float randomFloat15 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(1f, randomFloat15, randomFloat15 - 0.2f);
        case AnimalFoodType.HippoPoop:
          float randomFloat16 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(randomFloat16, randomFloat16, 0.0f);
        case AnimalFoodType.Mice:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.8f, 0.8f, 0.8f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        case AnimalFoodType.Garbage:
          float randomFloat17 = MathStuff.getRandomFloat(0.1f, 0.3f);
          return new Vector3(randomFloat17, randomFloat17 + 0.1f, randomFloat17);
        case AnimalFoodType.Termites:
          float randomFloat18 = MathStuff.getRandomFloat(0.7f, 0.9f);
          return new Vector3(randomFloat18, randomFloat18, randomFloat18 - 0.2f);
        case AnimalFoodType.Leaves:
          return new Vector3(0.3f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.3f);
        case AnimalFoodType.PigmentTablets:
          float randomFloat19 = MathStuff.getRandomFloat(0.5f, 0.7f);
          return new Vector3(1f, randomFloat19, randomFloat19 - 0.1f);
        case AnimalFoodType.Bamboo:
          return new Vector3(0.4f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.2f);
        case AnimalFoodType.Bones:
          float randomFloat20 = MathStuff.getRandomFloat(0.7f, 0.9f);
          return new Vector3(randomFloat20, randomFloat20, randomFloat20 - 0.2f);
        case AnimalFoodType.Snails:
          return new Vector3(0.5f, MathStuff.getRandomFloat(0.2f, 0.5f), 0.0f);
        case AnimalFoodType.Corn:
          return TinyZoo.Game1.Rnd.Next(0, 30) == 0 ? new Vector3(0.0f, 0.5f, 0.0f) : new Vector3(0.9f, MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f);
        case AnimalFoodType.WaterMelon:
          return TinyZoo.Game1.Rnd.Next(0, 3) == 0 ? new Vector3(0.0f, 0.5f, 0.0f) : new Vector3(MathStuff.getRandomFloat(0.4f, 0.8f), 0.0f, 0.0f);
        default:
          return new Vector3(1f, 1f, 1f);
      }
    }
  }
}
