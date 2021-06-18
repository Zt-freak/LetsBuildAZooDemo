// Decompiled with JetBrains decompiler
// Type: TinyZoo.QuestData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Quests;

namespace TinyZoo
{
  internal class QuestData
  {
    private static QuestPack[] questpacks;
    private static QuestSet[] questSets;
    internal static int MaxQuests = 56;

    internal static Vector2 GetCityLocation(CityName city)
    {
      switch (city)
      {
        case CityName.Sydney:
          return new Vector2(958f, 658f);
        case CityName.London:
          return new Vector2(467f, 339f);
        case CityName.Africa:
          return new Vector2(545f, 624f);
        case CityName.Bangladesh:
          return new Vector2(775f, 445f);
        case CityName.Singapore:
          return new Vector2(832f, 537f);
        case CityName.Tokyo:
          return new Vector2(924f, 396f);
        case CityName.Toronto:
          return new Vector2(205f, 374f);
        case CityName.Oakland:
          return new Vector2(71f, 393f);
        case CityName.Utah:
          return new Vector2(100f, 397f);
        case CityName.Moscow:
          return new Vector2(556f, 318f);
        case CityName.Cuba:
          return new Vector2(193f, 458f);
        case CityName.Brazil:
          return new Vector2(317f, 623f);
        case CityName.Shelter:
          return new Vector2(190f, 430f);
        default:
          return new Vector2(10f, 10f);
      }
    }

    internal static AnimalType GetZooKepper(CityName city) => AnimalType.AvatarsCount;

    internal static int GetUnlockForThisQuest(CityName city) => QuestData.GetQuest(city).UnlockedAtThisMantCompleteQuests;

    internal static CityName GetZooKeeperToCity(AnimalType keeper)
    {
      switch (keeper)
      {
        case AnimalType.AustralianZookeeper:
          return CityName.Sydney;
        case AnimalType.LondonZookeeper:
          return CityName.London;
        case AnimalType.TokyoZookeeper:
          return CityName.Tokyo;
        case AnimalType.MoscowZookeeper:
          return CityName.Moscow;
        case AnimalType.SingaporeZookeeper:
          return CityName.Singapore;
        case AnimalType.RioZookeeper:
          return CityName.Brazil;
        case AnimalType.ChinaZookeeper:
          return CityName.Beijing;
        case AnimalType.CubaZookeeper:
          return CityName.Cuba;
        case AnimalType.AfricaZookeeper:
          return CityName.Africa;
        case AnimalType.SurabayaZookeeper:
          return CityName.Surabaya;
        case AnimalType.BerlinZookeeper:
          return CityName.Berlin;
        case AnimalType.BangladeshZookeeper:
          return CityName.Bangladesh;
        default:
          return CityName.Count;
      }
    }

    internal static string GetCityName(CityName city)
    {
      switch (city)
      {
        case CityName.Sydney:
          return "Sydney";
        case CityName.London:
          return "London";
        case CityName.Africa:
          return "Botswana";
        case CityName.Bangladesh:
          return "Bangladesh";
        case CityName.Singapore:
          return "Singapore";
        case CityName.Tokyo:
          return "Tokyo";
        case CityName.Toronto:
          return "Toronto";
        case CityName.Oakland:
          return "Oakland";
        case CityName.Utah:
          return "Utah";
        case CityName.Moscow:
          return "Moscow";
        case CityName.Surabaya:
          return "Surabaya";
        case CityName.Berlin:
          return "Berlin";
        case CityName.Cuba:
          return "Cuba";
        case CityName.Beijing:
          return "Beijing";
        case CityName.Brazil:
          return "Rio de Janeiro";
        default:
          return "NA_CITYNAME_" + city.ToString();
      }
    }

    internal static QuestSet GetQuest(CityName city)
    {
      if (QuestData.questSets == null)
      {
        if (Z_DebugFlags.IsBetaVersion)
        {
          QuestData.questSets = new QuestSet[17];
          QuestData.questSets[0] = new QuestSet(0);
          QuestData.questSets[0].questshere.Add(new QuestPack(AnimalType.Rabbit, CityName.Sydney, new TradeInfo(), 0));
          QuestData.questSets[6] = new QuestSet(1);
          QuestData.questSets[6].questshere.Add(new QuestPack(AnimalType.Horse, CityName.Toronto, new TradeInfo(AnimalType.Rabbit, 1, 1), 0));
          QuestData.questSets[5] = new QuestSet(1);
          QuestData.questSets[5].questshere.Add(new QuestPack(AnimalType.Capybara, CityName.Tokyo, new TradeInfo(AnimalType.Rabbit, 1, 1), 0));
          QuestData.questSets[14] = new QuestSet(2);
          QuestData.questSets[14].questshere.Add(new QuestPack(AnimalType.Snake, CityName.Brazil, new TradeInfo(AnimalType.Horse, 1, -1), 0));
          QuestData.questSets[9] = new QuestSet(3);
          QuestData.questSets[9].questshere.Add(new QuestPack(AnimalType.Wolf, CityName.Moscow, new TradeInfo(AnimalType.Snake, 2, -1), 0));
          QuestData.questSets[2] = new QuestSet(3);
          QuestData.questSets[2].questshere.Add(new QuestPack(AnimalType.Hippopotamus, CityName.Africa, new TradeInfo(AnimalType.Capybara, 3, -1), 0));
          QuestData.questSets[1] = new QuestSet(60);
          QuestData.questSets[1].questshere.Add(new QuestPack(AnimalType.Duck, CityName.Toronto, new TradeInfo(AnimalType.Pig, 1, 1), 0));
          QuestData.questSets[3] = new QuestSet(60);
          QuestData.questSets[3].questshere.Add(new QuestPack(AnimalType.Gorilla, CityName.Bangladesh, new TradeInfo(AnimalType.Seal, 1, 6), 0));
          QuestData.questSets[13] = new QuestSet(60);
          QuestData.questSets[13].questshere.Add(new QuestPack(AnimalType.Tortoise, CityName.Beijing, new TradeInfo(AnimalType.Meerkat, 1, 3), 0));
          QuestData.questSets[11] = new QuestSet(60);
          QuestData.questSets[11].questshere.Add(new QuestPack(AnimalType.Panther, CityName.Berlin, new TradeInfo(AnimalType.Ostrich, 4, -1), 0));
          QuestData.questSets[12] = new QuestSet(60);
          QuestData.questSets[12].questshere.Add(new QuestPack(AnimalType.Lemur, CityName.Cuba, new TradeInfo(AnimalType.Camel, 1, 2), 0));
          QuestData.questSets[4] = new QuestSet(60);
          QuestData.questSets[4].questshere.Add(new QuestPack(AnimalType.Crocodile, CityName.Singapore, new TradeInfo(AnimalType.Antelope, 1, 3), 0));
          QuestData.questSets[7] = new QuestSet(60);
          QuestData.questSets[7].questshere.Add(new QuestPack(AnimalType.Bear, CityName.Oakland, new TradeInfo(AnimalType.Badger, 2, 4), 0));
          QuestData.questSets[10] = new QuestSet(60);
          QuestData.questSets[10].questshere.Add(new QuestPack(AnimalType.KomodoDragon, CityName.Surabaya, new TradeInfo(AnimalType.Snake, 1, 8), 0));
          QuestData.questSets[8] = new QuestSet(60);
          QuestData.questSets[8].questshere.Add(new QuestPack(AnimalType.Porcupine, CityName.Utah, new TradeInfo(AnimalType.Pig, 1, 5), 0));
        }
        else
        {
          QuestData.questSets = new QuestSet[17];
          QuestData.questSets[0] = new QuestSet(0);
          QuestData.questSets[0].questshere.Add(new QuestPack(AnimalType.Rabbit, CityName.Sydney, new TradeInfo(), 0));
          QuestData.questSets[0].questshere.Add(new QuestPack(AnimalType.Hyena, CityName.Sydney, new TradeInfo(AnimalType.Snake, 4, -1), 0));
          QuestData.questSets[0].questshere.Add(new QuestPack(AnimalType.Donkey, CityName.Sydney, new TradeInfo(AnimalType.Porcupine, 2, 4), 0));
          QuestData.questSets[0].questshere.Add(new QuestPack(AnimalType.Kangaroo, CityName.Sydney, new TradeInfo(AnimalType.Flamingo, 1, 7), 0));
          QuestData.questSets[1] = new QuestSet(1);
          QuestData.questSets[1].questshere.Add(new QuestPack(AnimalType.Goose, CityName.London, new TradeInfo(AnimalType.Rabbit, 1, 1), 0));
          QuestData.questSets[1].questshere.Add(new QuestPack(AnimalType.Pig, CityName.London, new TradeInfo(AnimalType.Snake, 1, 2), 0));
          QuestData.questSets[1].questshere.Add(new QuestPack(AnimalType.Badger, CityName.London, new TradeInfo(AnimalType.Capybara, 2, 4), 0));
          QuestData.questSets[1].questshere.Add(new QuestPack(AnimalType.Cow, CityName.London, new TradeInfo(AnimalType.Duck, 1, 6), 0));
          QuestData.questSets[14] = new QuestSet(2);
          QuestData.questSets[14].questshere.Add(new QuestPack(AnimalType.Snake, CityName.Brazil, new TradeInfo(AnimalType.Goose, 2, -1), 0));
          QuestData.questSets[14].questshere.Add(new QuestPack(AnimalType.Armadillo, CityName.Brazil, new TradeInfo(AnimalType.Bear, 4, 2), 0));
          QuestData.questSets[14].questshere.Add(new QuestPack(AnimalType.Chicken, CityName.Brazil, new TradeInfo(AnimalType.Horse, 4, -1), 0));
          QuestData.questSets[6] = new QuestSet(2);
          QuestData.questSets[6].questshere.Add(new QuestPack(AnimalType.Duck, CityName.Toronto, new TradeInfo(AnimalType.Pig, 1, 1), 0));
          QuestData.questSets[6].questshere.Add(new QuestPack(AnimalType.Horse, CityName.Toronto, new TradeInfo(AnimalType.Hyena, 10, -1), 0));
          QuestData.questSets[6].questshere.Add(new QuestPack(AnimalType.Penguin, CityName.Toronto, new TradeInfo(AnimalType.Tapir, 2, 3), 0));
          QuestData.questSets[6].questshere.Add(new QuestPack(AnimalType.Walrus, CityName.Toronto, new TradeInfo(AnimalType.Wolf, 10, -1), 0));
          QuestData.questSets[5] = new QuestSet(3);
          QuestData.questSets[5].questshere.Add(new QuestPack(AnimalType.Capybara, CityName.Tokyo, new TradeInfo(AnimalType.Rabbit, 2, 2), 0));
          QuestData.questSets[5].questshere.Add(new QuestPack(AnimalType.Platypus, CityName.Tokyo, new TradeInfo(AnimalType.Cow, 2, 9), 0));
          QuestData.questSets[5].questshere.Add(new QuestPack(AnimalType.Raccoon, CityName.Tokyo, new TradeInfo(AnimalType.Beavers, 4, -1), 0));
          QuestData.questSets[5].questshere.Add(new QuestPack(AnimalType.Owl, CityName.Tokyo, new TradeInfo(AnimalType.Fox, 2, 5), 0));
          QuestData.questSets[8] = new QuestSet(6);
          QuestData.questSets[8].questshere.Add(new QuestPack(AnimalType.Porcupine, CityName.Utah, new TradeInfo(AnimalType.Pig, 1, 5), 0));
          QuestData.questSets[8].questshere.Add(new QuestPack(AnimalType.Meerkat, CityName.Utah, new TradeInfo(AnimalType.Duck, 2, 4), 0));
          QuestData.questSets[8].questshere.Add(new QuestPack(AnimalType.Camel, CityName.Utah, new TradeInfo(AnimalType.Tortoise, 3, 3), 0));
          QuestData.questSets[7] = new QuestSet(9);
          QuestData.questSets[7].questshere.Add(new QuestPack(AnimalType.Bear, CityName.Oakland, new TradeInfo(AnimalType.Badger, 2, 4), 0));
          QuestData.questSets[7].questshere.Add(new QuestPack(AnimalType.Tapir, CityName.Oakland, new TradeInfo(AnimalType.Pig, 4, 6), 0));
          QuestData.questSets[7].questshere.Add(new QuestPack(AnimalType.Ostrich, CityName.Oakland, new TradeInfo(AnimalType.Donkey, 1, 3), 0));
          QuestData.questSets[7].questshere.Add(new QuestPack(AnimalType.Antelope, CityName.Oakland, new TradeInfo(AnimalType.Armadillo, 2, 6), 0));
          QuestData.questSets[13] = new QuestSet(15);
          QuestData.questSets[13].questshere.Add(new QuestPack(AnimalType.Tortoise, CityName.Beijing, new TradeInfo(AnimalType.Meerkat, 1, 3), 0));
          QuestData.questSets[13].questshere.Add(new QuestPack(AnimalType.WildBoar, CityName.Beijing, new TradeInfo(AnimalType.Badger, 5, -1), 0));
          QuestData.questSets[13].questshere.Add(new QuestPack(AnimalType.RedPanda, CityName.Beijing, new TradeInfo(AnimalType.Chicken, 1, 9), 0));
          QuestData.questSets[13].questshere.Add(new QuestPack(AnimalType.Panda, CityName.Beijing, new TradeInfo(AnimalType.Monkey, 2, 8), 0));
          QuestData.questSets[11] = new QuestSet(18);
          QuestData.questSets[11].questshere.Add(new QuestPack(AnimalType.Panther, CityName.Berlin, new TradeInfo(AnimalType.Ostrich, 4, -1), 0));
          QuestData.questSets[11].questshere.Add(new QuestPack(AnimalType.Peacock, CityName.Berlin, new TradeInfo(AnimalType.Horse, 3, 0), 0));
          QuestData.questSets[11].questshere.Add(new QuestPack(AnimalType.Deer, CityName.Berlin, new TradeInfo(AnimalType.Skunk, 6, -1), 0));
          QuestData.questSets[11].questshere.Add(new QuestPack(AnimalType.Beavers, CityName.Berlin, new TradeInfo(AnimalType.WildBoar, 2, 6), 0));
          QuestData.questSets[9] = new QuestSet(21);
          QuestData.questSets[9].questshere.Add(new QuestPack(AnimalType.Seal, CityName.Moscow, new TradeInfo(AnimalType.Cow, 1, 8), 0));
          QuestData.questSets[9].questshere.Add(new QuestPack(AnimalType.Wolf, CityName.Moscow, new TradeInfo(AnimalType.Meerkat, 1, 5), 0));
          QuestData.questSets[9].questshere.Add(new QuestPack(AnimalType.PolarBear, CityName.Moscow, new TradeInfo(AnimalType.Goose, 2, 8), 0));
          QuestData.questSets[9].questshere.Add(new QuestPack(AnimalType.Elephant, CityName.Moscow, new TradeInfo(AnimalType.Lemur, 1, 1), 0));
          QuestData.questSets[12] = new QuestSet(25);
          QuestData.questSets[12].questshere.Add(new QuestPack(AnimalType.Lemur, CityName.Cuba, new TradeInfo(AnimalType.Camel, 1, 2), 0));
          QuestData.questSets[12].questshere.Add(new QuestPack(AnimalType.Alpaca, CityName.Cuba, new TradeInfo(AnimalType.Panther, 3, 1), 0));
          QuestData.questSets[12].questshere.Add(new QuestPack(AnimalType.PolarBear, CityName.Cuba, new TradeInfo(AnimalType.Capybara, 10, -1), 0));
          QuestData.questSets[12].questshere.Add(new QuestPack(AnimalType.Fox, CityName.Cuba, new TradeInfo(AnimalType.Tiger, 1, 9), 0));
          QuestData.questSets[10] = new QuestSet(28);
          QuestData.questSets[10].questshere.Add(new QuestPack(AnimalType.KomodoDragon, CityName.Surabaya, new TradeInfo(AnimalType.Snake, 1, 8), 0));
          QuestData.questSets[10].questshere.Add(new QuestPack(AnimalType.Orangutan, CityName.Surabaya, new TradeInfo(AnimalType.Walrus, 4, 2), 0));
          QuestData.questSets[10].questshere.Add(new QuestPack(AnimalType.Skunk, CityName.Surabaya, new TradeInfo(AnimalType.Penguin, 1, 5), 0));
          QuestData.questSets[4] = new QuestSet(32);
          QuestData.questSets[4].questshere.Add(new QuestPack(AnimalType.Crocodile, CityName.Singapore, new TradeInfo(AnimalType.Antelope, 1, 3), 0));
          QuestData.questSets[4].questshere.Add(new QuestPack(AnimalType.Monkey, CityName.Singapore, new TradeInfo(AnimalType.Tortoise, 2, 2), 0));
          QuestData.questSets[4].questshere.Add(new QuestPack(AnimalType.Otter, CityName.Singapore, new TradeInfo(AnimalType.Tiger, 2, 5), 0));
          QuestData.questSets[4].questshere.Add(new QuestPack(AnimalType.Lion, CityName.Singapore, new TradeInfo(AnimalType.Lion, 1, 9), 0));
          QuestData.questSets[3] = new QuestSet(39);
          QuestData.questSets[3].questshere.Add(new QuestPack(AnimalType.Gorilla, CityName.Bangladesh, new TradeInfo(AnimalType.Seal, 1, 6), 0));
          QuestData.questSets[3].questshere.Add(new QuestPack(AnimalType.Tiger, CityName.Bangladesh, new TradeInfo(AnimalType.Deer, 1, 7), 0));
          QuestData.questSets[3].questshere.Add(new QuestPack(AnimalType.Cheetah, CityName.Bangladesh, new TradeInfo(AnimalType.RedPanda, 1, 9), 0));
          QuestData.questSets[2] = new QuestSet(43);
          QuestData.questSets[2].questshere.Add(new QuestPack(AnimalType.Zebra, CityName.Africa, new TradeInfo(AnimalType.Horse, 1, 6), 0));
          QuestData.questSets[2].questshere.Add(new QuestPack(AnimalType.Rhino, CityName.Africa, new TradeInfo(AnimalType.Gorilla, 1, 7), 0));
          QuestData.questSets[2].questshere.Add(new QuestPack(AnimalType.Giraffe, CityName.Africa, new TradeInfo(AnimalType.Panda, 1, 9), 0));
          QuestData.questSets[2].questshere.Add(new QuestPack(AnimalType.Hippopotamus, CityName.Africa, new TradeInfo(AnimalType.Rhino, 1, 9), 0));
        }
      }
      return QuestData.questSets[(int) city];
    }

    internal static QuestSet GetQuestset(CityName cityname) => QuestData.questSets[(int) cityname];

    public static CityName GetQuestLocationToObtainThisAnimal(AnimalType animalType)
    {
      for (int index = 0; index < 15; ++index)
      {
        if (QuestData.questSets[index].WillUnlockThisAnimal(animalType))
          return (CityName) index;
      }
      return CityName.Count;
    }
  }
}
