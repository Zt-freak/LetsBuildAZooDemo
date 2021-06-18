// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.EnemyData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Blance;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;

namespace TinyZoo.GamePlay.Enemies
{
  internal class EnemyData
  {
    private static EnemyInfoPack[] enemydata;
    private static AnimationProfile[] animators;
    private static HashSet<AnimalType> PeopleWhoCannotWearHats;
    private static HashSet<AnimalType> animalwelfarepeople;
    private static HashSet<AnimalType> FoodHygieneHealthInspector;
    private static HashSet<AnimalType> Influencer;
    private static HashSet<AnimalType> SafetyInspector;
    private static HashSet<AnimalType> Teachers;
    private static HashSet<AnimalType> Students;
    private static HashSet<AnimalType> AnimalRightsActivists;
    private static HashSet<AnimalType> FootballPlayer;
    private static HashSet<AnimalType> Bikers;
    private static HashSet<AnimalType> FoodCritic;
    private static HashSet<AnimalType> MenInBlack;
    private static HashSet<AnimalType> Hunters;
    private static HashSet<AnimalType> PureLife;
    private static HashSet<AnimalType> MovieStars;
    private static HashSet<AnimalType> BlackMarket;
    private static HashSet<AnimalType> CriticalChoice;

    public static Rectangle GetEnemyIdleRectangle(AnimalType enemytype, int CLIndex = -1) => EnemyData.GetEnemyRectangle(enemytype).GetIdleFrame(CLIndex);

    internal static bool GetIsSingle(AnimalType enemy) => enemy == AnimalType.Riddick || enemy == AnimalType.Leeloo || enemy == AnimalType.Bill;

    public static AnimalType GetZooKeeper(CityName city)
    {
      switch (city)
      {
        case CityName.Sydney:
          return AnimalType.AustralianZookeeper;
        case CityName.London:
          return AnimalType.LondonZookeeper;
        case CityName.Africa:
          return AnimalType.AfricaZookeeper;
        case CityName.Bangladesh:
          return AnimalType.BangladeshZookeeper;
        case CityName.Singapore:
          return AnimalType.SingaporeZookeeper;
        case CityName.Tokyo:
          return AnimalType.TokyoZookeeper;
        case CityName.Toronto:
          return AnimalType.TorontoZookeeper;
        case CityName.Oakland:
          return AnimalType.OaklandZookeeper;
        case CityName.Utah:
          return AnimalType.UtahZookeeper;
        case CityName.Moscow:
          return AnimalType.MoscowZookeeper;
        case CityName.Surabaya:
          return AnimalType.SurabayaZookeeper;
        case CityName.Berlin:
          return AnimalType.BerlinZookeeper;
        case CityName.Cuba:
          return AnimalType.CubaZookeeper;
        case CityName.Beijing:
          return AnimalType.ChinaZookeeper;
        case CityName.Brazil:
          return AnimalType.RioZookeeper;
        default:
          return AnimalType.TokyoZookeeper;
      }
    }

    public static ShadowInfo GetEnemyShadow(AnimalType enemytype)
    {
      if (EnemyData.enemydata == null)
        EnemyData.enemydata = new EnemyInfoPack[70];
      if (EnemyData.enemydata[(int) enemytype] == null)
        EnemyData.GetEnemyRectangle(enemytype);
      if (EnemyData.enemydata[(int) enemytype].shadowdata == null)
      {
        switch (enemytype)
        {
          case AnimalType.Rabbit:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(161, 13, 15, 6), 2.5f);
            break;
          case AnimalType.Goose:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(98, 69, 12, 6), 2.5f);
            break;
          case AnimalType.Capybara:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(40, 196, 18, 8), 2.5f);
            break;
          case AnimalType.Pig:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(41, 173, 21, 7), 2.5f);
            break;
          case AnimalType.Duck:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(72, 194, 12, 6), 2.5f);
            break;
          case AnimalType.Snake:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(67, 44, 15, 7), 2.5f);
            break;
          case AnimalType.Badger:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(111, 62, 18, 6), 2.5f);
            break;
          case AnimalType.Hyena:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(84, 44, 20, 7), 2.5f);
            break;
          case AnimalType.Porcupine:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(104, 115, 21, 7), 2.5f);
            break;
          case AnimalType.Bear:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(19, 98, 21, 9), 2.5f);
            break;
          case AnimalType.Meerkat:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(59, 197, 12, 5), 2.5f);
            break;
          case AnimalType.Horse:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(84, 118, 19, 8), 2.5f);
            break;
          case AnimalType.Armadillo:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(70, 91, 19, 7), 2.5f);
            break;
          case AnimalType.Donkey:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(146, 38, 19, 8), 2.5f);
            break;
          case AnimalType.Cow:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(41, 123, 23, 9), 2.5f);
            break;
          case AnimalType.Tapir:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(19, 98, 21, 9), 2.5f);
            break;
          case AnimalType.Ostrich:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 212, 15, 7), 2.5f);
            break;
          case AnimalType.Tortoise:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(63, 172, 18, 8), 2.5f);
            break;
          case AnimalType.Chicken:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(130, 61, 10, 6), 2.5f);
            break;
          case AnimalType.Camel:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(69, 22, 19, 8), 2.5f);
            break;
          case AnimalType.Penguin:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(41, 97, 12, 7), 2.5f);
            break;
          case AnimalType.Antelope:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(106, 41, 18, 8), 2.5f);
            break;
          case AnimalType.Panther:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(21, 72, 22, 8), 2.5f);
            break;
          case AnimalType.Seal:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(19, 141, 20, 9), 2.5f);
            break;
          case AnimalType.Wolf:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(41, 149, 21, 7), 2.5f);
            break;
          case AnimalType.Lemur:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(125, 41, 20, 7), 2.5f);
            break;
          case AnimalType.Alpaca:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(65, 125, 18, 8), 2.5f);
            break;
          case AnimalType.KomodoDragon:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(141, 60, 28, 7), 2.5f);
            break;
          case AnimalType.Walrus:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(79, 69, 18, 7), 2.5f);
            break;
          case AnimalType.Orangutan:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(62, 70, 16, 7), 2.5f);
            break;
          case AnimalType.PolarBear:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(19, 98, 21, 9), 2.5f);
            break;
          case AnimalType.Skunk:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(110, 90, 18, 7), 2.5f);
            break;
          case AnimalType.Crocodile:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(15, 161, 25, 6), 2.5f);
            break;
          case AnimalType.WildBoar:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(82, 174, 16, 7), 2.5f);
            break;
          case AnimalType.Peacock:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(89, 20, 18, 6), 2.5f);
            break;
          case AnimalType.Platypus:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(89, 12, 26, 7), 2.5f);
            break;
          case AnimalType.Deer:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 141, 18, 8), 2.5f);
            break;
          case AnimalType.Monkey:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(86, 141, 17, 6), 2.5f);
            break;
          case AnimalType.Flamingo:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 175, 14, 7), 2.5f);
            break;
          case AnimalType.Gorilla:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(54, 98, 15, 6), 2.5f);
            break;
          case AnimalType.Tiger:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 84, 18, 8), 2.5f);
            break;
          case AnimalType.Kangaroo:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(63, 153, 22, 6), 2.5f);
            break;
          case AnimalType.Beavers:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(16, 180, 24, 6), 2.5f);
            break;
          case AnimalType.RedPanda:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(48, 12, 19, 7), 2.5f);
            break;
          case AnimalType.Zebra:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 113, 18, 8), 2.5f);
            break;
          case AnimalType.Fox:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(44, 72, 17, 6), 2.5f);
            break;
          case AnimalType.Raccoon:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(140, 13, 20, 6), 2.5f);
            break;
          case AnimalType.Elephant:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(22, 19, 25, 9), 2.5f);
            break;
          case AnimalType.Cheetah:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(21, 72, 22, 8), 2.5f);
            break;
          case AnimalType.Otter:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(45, 33, 23, 6), 2.5f);
            break;
          case AnimalType.Owl:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(229, 368, 11, 7), 2.5f);
            break;
          case AnimalType.Rhino:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(22, 46, 22, 9), 2.5f);
            break;
          case AnimalType.Panda:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(19, 98, 21, 9), 2.5f);
            break;
          case AnimalType.Giraffe:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 32, 21, 7), 2.5f);
            break;
          case AnimalType.Hippopotamus:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(41, 123, 23, 9), 2.5f);
            break;
          case AnimalType.Lion:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 58, 20, 8), 2.5f);
            break;
          default:
            EnemyData.enemydata[(int) enemytype].shadowdata = new ShadowInfo(new Rectangle(0, 58, 20, 8), 2.5f);
            break;
        }
      }
      return EnemyData.enemydata[(int) enemytype].shadowdata;
    }

    public static EnemyInfoPack GetEnemyRectangle(AnimalType enemytype)
    {
      if (EnemyData.enemydata == null)
        EnemyData.enemydata = new EnemyInfoPack[464];
      if (EnemyData.enemydata[(int) enemytype] == null)
      {
        switch (enemytype)
        {
          case AnimalType.Rabbit:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(161, 0, 14, 10), new Vector2(6f, 10f), 1);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(177, 0, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(193, 0, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(209, 0, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(225, 0, 15, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 0, 18, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(86, 148, 16, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(58, 203, 16, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(166, 20, 15, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(39, 205, 18, 13));
            break;
          case AnimalType.Goose:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(98, 52, 12, 16), new Vector2(6f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(182, 13, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(196, 13, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(210, 13, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(224, 12, 16, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(240, 12, 13, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(254, 12, 12, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(267, 11, 14, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(75, 201, 12, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(85, 181, 13, 18));
            break;
          case AnimalType.Capybara:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(40, 181, 18, 14), new Vector2(8f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(527, 34, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(545, 0, 17, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(579, 17, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(582, 0, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(563, 0, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(543, 27, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(541, 14, 18, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(560, 15, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(562, 30, 18, 14));
            break;
          case AnimalType.Pig:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(82, 160, 16, 13), new Vector2(9f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(129, 68, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(146, 68, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(129, 82, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(146, 82, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(104, 123, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(120, 137, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(103, 149, 15, 10));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(99, 160, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(121, 123, 16, 13));
            break;
          case AnimalType.Duck:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(72, 181, 12, 12), new Vector2(8f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(173, 156, 14, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(119, 151, 12, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(132, 151, 12, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(145, 151, 13, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(116, 164, 11, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(128, 165, 13, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(188, 156, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(142, 165, 15, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(159, 154, 13, 15));
            break;
          case AnimalType.Snake:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(67, 31, 15, 12), new Vector2(8f, 13f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(1733, 0, 17, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(99, 174, 16, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(116, 180, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(157, 170, 14, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(132, 179, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(163, 182, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(172, 169, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(188, 169, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(148, 182, 14, 12));
            break;
          case AnimalType.Badger:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(111, 50, 18, 11), new Vector2(8f, 8f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(261, 159, 20, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(281, 144, 22, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(421, 90, 23, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(282, 156, 19, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(303, 151, 24, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(400, 94, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 85, 24, 10));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(194, 223, 21, 10));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(144, 319, 21, 10));
            break;
          case AnimalType.Hyena:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(84, 27, 20, 16), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(685, 0, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(706, 0, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(144, 330, 20, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(727, 0, 19, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(747, 0, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(768, 0, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 101, 14, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(400, 107, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(693, 17, 20, 16));
            break;
          case AnimalType.Porcupine:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(104, 98, 21, 16), new Vector2(7f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(421, 102, 23, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(421, 116, 23, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(714, 17, 29, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 96, 23, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(365, 125, 19, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 109, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(400, 123, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 125, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(124, 400, 19, 9));
            break;
          case AnimalType.Bear:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(45, 40, 20, 16), new Vector2(8f, 12f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(41, 339, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 338, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(67, 321, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(21, 338, 19, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 319, 20, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 354, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(62, 337, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(21, 319, 23, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(45, 321, 21, 17));
            break;
          case AnimalType.Meerkat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(59, 181, 12, 15), new Vector2(9f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(202, 254, 11, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 115, 13, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(328, 144, 14, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(149, 344, 14, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(469, 57, 12, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(470, 76, 12, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(146, 361, 16, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(469, 105, 13, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(470, 89, 12, 15));
            break;
          case AnimalType.Horse:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(84, 98, 19, 19), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(58, 240, 19, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(17, 240, 19, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(37, 240, 20, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 241, 16, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 220, 19, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(40, 220, 20, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(21, 219, 19, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(61, 220, 20, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(82, 218, 24, 20));
            break;
          case AnimalType.Armadillo:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(70, 78, 19, 12), new Vector2(8f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(204, 173, 14, 8));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(744, 17, 27, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(704, 34, 19, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(724, 33, 20, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 51, 28, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(195, 182, 21, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(194, 210, 21, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(192, 194, 23, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(789, 0, 30, 17));
            break;
          case AnimalType.Donkey:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(146, 20, 19, 17), new Vector2(8f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(86, 373, 19, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(130, 345, 18, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(124, 381, 19, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(90, 354, 19, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(86, 392, 17, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(110, 348, 19, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(106, 366, 19, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(104, 384, 19, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(126, 361, 19, 19));
            break;
          case AnimalType.Cow:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(41, 105, 23, 17), new Vector2(8f, 7f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(236, 55, 23, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(236, 73, 23, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(235, 91, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(210, 92, 24, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(210, 55, 25, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(210, 109, 24, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(210, 75, 24, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(170, 102, 23, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(180, 79, 29, 22));
            break;
          case AnimalType.Tapir:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(16, 187, 22, 15), new Vector2(8f, 7.5f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(485, 207, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(890, 18, 20, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(485, 223, 21, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(465, 215, 19, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(911, 18, 21, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(485, 240, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(463, 229, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 215, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(307, 259, 19, 13));
            break;
          case AnimalType.Ostrich:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 183, 15, 28), new Vector2(9f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(48, 382, 20, 28));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(21, 354, 19, 25));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(41, 355, 17, 25));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(30, 380, 17, 25));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(59, 353, 15, 28));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(75, 353, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(16, 380, 13, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 372, 15, 31));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(70, 378, 15, 30));
            break;
          case AnimalType.Tortoise:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(63, 160, 18, 11), new Vector2(8f, 7.5f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(126, 115, 11, 7));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(345, 82, 18, 8));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(402, 15, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(447, 0, 28, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(423, 15, 19, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(443, 15, 28, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(381, 14, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(398, 0, 27, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(426, 0, 20, 13));
            break;
          case AnimalType.Chicken:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(130, 49, 10, 11), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(260, 0, 11, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(272, 0, 12, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(285, 0, 11, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(297, 0, 8, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(306, 0, 11, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(282, 12, 14, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(297, 13, 13, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(311, 13, 11, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(195, 102, 14, 11));
            break;
          case AnimalType.Camel:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(69, 0, 19, 21), new Vector2(8f, 7.5f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(179, 182, 15, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(128, 194, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(172, 195, 19, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(105, 198, 22, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(149, 195, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(129, 212, 21, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(107, 218, 21, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(151, 212, 21, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(104, 237, 22, 18));
            break;
          case AnimalType.Penguin:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(41, 81, 12, 15), new Vector2(7f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(294, 78, 11, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(260, 56, 13, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(260, 75, 11, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(272, 79, 10, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(283, 78, 10, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(259, 91, 10, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(281, 92, 10, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(270, 91, 10, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(292, 93, 12, 13));
            break;
          case AnimalType.Antelope:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(106, 20, 18, 20), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(125, 252, 15, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(105, 256, 18, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(403, 293, 17, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(129, 231, 22, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(97, 279, 19, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(462, 289, 19, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(61, 298, 20, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 281, 18, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 295, 22, 23));
            break;
          case AnimalType.Panther:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(16, 203, 22, 15), new Vector2(8f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(166, 316, 18, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(219, 172, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(216, 189, 21, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(820, 0, 23, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(844, 0, 22, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(144, 377, 17, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(343, 136, 20, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(216, 203, 22, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(240, 172, 21, 15));
            break;
          case AnimalType.Seal:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(19, 125, 20, 15), new Vector2(8.5f, 9.5f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(235, 108, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(261, 43, 16, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(261, 30, 21, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(297, 61, 24, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(274, 61, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(283, 27, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(301, 45, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(304, 29, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(278, 43, 22, 17));
            break;
          case AnimalType.Wolf:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(41, 133, 21, 15), new Vector2(8f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(344, 67, 19, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(364, 66, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(306, 76, 17, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(323, 50, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(342, 50, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(325, 81, 19, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(324, 65, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(362, 50, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(305, 93, 19, 15));
            break;
          case AnimalType.Lemur:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(125, 19, 20, 21), new Vector2(7.5f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(266, 125, 15, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(292, 107, 17, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(404, 59, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(274, 107, 17, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(263, 137, 17, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(142, 252, 21, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(457, 33, 25, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(282, (int) sbyte.MaxValue, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(152, 230, 23, 19));
            break;
          case AnimalType.Alpaca:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(65, 104, 18, 20), new Vector2(7f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(581, 32, 12, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(124, 321, 19, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(84, 332, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(89, 311, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(103, 326, 20, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(128, 299, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(107, 304, 20, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(166, 294, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(147, 297, 18, 21));
            break;
          case AnimalType.KomodoDragon:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(141, 47, 28, 12), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(772, 18, 30, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(482, 64, 29, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(803, 18, 34, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(838, 18, 30, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 415, 26, 10));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(512, 50, 31, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(867, 0, 29, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(512, 64, 30, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(745, 33, 30, 13));
            break;
          case AnimalType.Walrus:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(79, 52, 18, 16), new Vector2(8f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(606, 0, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(627, 0, 18, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(463, 272, 19, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(666, 0, 18, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(598, 17, 18, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(617, 16, 18, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(636, 16, 18, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(674, 17, 18, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(655, 17, 18, 18));
            break;
          case AnimalType.Orangutan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(62, 52, 16, 17), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(897, 0, 17, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(544, 42, 17, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(468, 123, 14, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(543, 62, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(562, 45, 19, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(144, 390, 17, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(165, 328, 19, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(216, 223, 21, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(262, 171, 15, 16));
            break;
          case AnimalType.PolarBear:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(19, 108, 21, 16), new Vector2(7f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(466, 138, 16, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(582, 47, 15, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(238, 188, 25, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(239, 206, 24, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(562, 66, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(214, 242, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(264, 188, 13, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 130, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 140, 20, 17));
            break;
          case AnimalType.Skunk:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(110, 69, 18, 20), new Vector2(7f, 13f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(598, 47, 22, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(164, 348, 19, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(621, 47, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 77, 26, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(343, 151, 20, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(238, 226, 23, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(278, 169, 24, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(364, 140, 20, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 135, 18, 20));
            break;
          case AnimalType.Crocodile:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(15, 151, 25, 9), new Vector2(7.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 89, 26, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(214, 259, 18, 7));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 99, 26, 8));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(202, 267, 27, 6));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 108, 25, 6));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(278, 194, 23, 7));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 115, 26, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(510, 76, 32, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 125, 25, 9));
            break;
          case AnimalType.WildBoar:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(41, 157, 21, 15), new Vector2(7.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(582, 68, 20, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(603, 68, 16, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 147, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(104, 405, 19, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(303, 164, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 162, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 177, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(303, 181, 22, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 192, 22, 16));
            break;
          case AnimalType.Peacock:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(90, 77, 18, 19), new Vector2(7.5f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(162, 371, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 158, 19, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(264, 203, 21, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 179, 19, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(162, 393, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 162, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(144, 408, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(125, 410, 18, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(195, 274, 18, 19));
            break;
          case AnimalType.Platypus:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(89, 0, 26, 11), new Vector2(7.5f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(318, 0, 24, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(343, 0, 26, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(354, 14, 26, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 38, 26, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(323, 14, 30, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(370, 0, 27, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(354, 38, 26, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(354, 26, 26, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 26, 26, 11));
            break;
          case AnimalType.Deer:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 122, 18, 18), new Vector2(7f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(365, 84, 15, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(310, 109, 15, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(381, 78, 18, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(346, 115, 18, 20));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(365, 101, 19, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(326, 98, 18, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 121, 18, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(345, 91, 19, 23));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(303, (int) sbyte.MaxValue, 23, 22));
            break;
          case AnimalType.Monkey:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(86, (int) sbyte.MaxValue, 17, 13), new Vector2(8f, 13f), 1);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(443, 33, 13, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(381, 27, 18, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(439, 45, 16, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(401, 44, 19, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 59, 18, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(400, 28, 18, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(421, 44, 17, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(382, 45, 18, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(419, 28, 23, 15));
            break;
          case AnimalType.Flamingo:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 150, 14, 24), new Vector2(7.5f, 9f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(185, 29, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(200, 29, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(215, 29, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(230, 29, 15, 25));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(246, 29, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(170, 39, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(181, 54, 13, 22));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(195, 54, 14, 24));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(164, 71, 14, 24));
            break;
          case AnimalType.Gorilla:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(54, 80, 15, 17), new Vector2(9f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(214, 274, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(640, 49, 18, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(620, 68, 15, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(465, 151, 16, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(404, 136, 17, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(403, 155, 18, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(326, 183, 19, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(659, 49, 16, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(465, 171, 15, 15));
            break;
          case AnimalType.Tiger:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 67, 18, 16), new Vector2(7f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(262, 238, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(869, 18, 20, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 256, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 214, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(308, 226, 18, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(285, 228, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(307, 242, 19, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(348, 261, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(262, 254, 22, 15));
            break;
          case AnimalType.Kangaroo:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(63, 134, 22, 18), new Vector2(8f, 10f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(126, 96, 22, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(149, 96, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(138, 115, 22, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(138, 134, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(159, 137, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(161, 118, 22, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(180, 137, 22, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(207, 126, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(184, 118, 22, 18));
            break;
          case AnimalType.Beavers:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(16, 168, 24, 11), new Vector2(9.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 156, 17, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(185, 294, 27, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(104, 417, 20, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(286, 202, 15, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 135, 25, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(482, 148, 25, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(302, 199, 23, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(326, 202, 20, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(236, 250, 24, 10));
            break;
          case AnimalType.RedPanda:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(48, 0, 19, 11), new Vector2(8f, 12f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(262, 226, 22, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(445, 201, 19, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(286, 218, 15, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(213, 294, 24, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(346, 163, 17, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(302, 212, 23, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(185, 306, 23, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(364, 162, 19, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(326, 212, 20, 11));
            break;
          case AnimalType.Zebra:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 93, 18, 19), new Vector2(7f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(1999, 290, 19, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(135, 275, 21, 21));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(117, 279, 17, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(157, 274, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(164, 254, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(176, 274, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(195, 234, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(176, 234, 18, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(183, 254, 18, 19));
            break;
          case AnimalType.Fox:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(44, 57, 17, 14), new Vector2(7.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(204, 158, 17, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(257, 108, 16, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(223, 143, 17, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(242, 157, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 141, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(204, 143, 18, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(228, 126, 18, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(222, 157, 19, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(247, 125, 18, 15));
            break;
          case AnimalType.Raccoon:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(140, 0, 20, 12), new Vector2(8f, 12f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(446, 71, 22, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(446, 58, 22, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(400, 81, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(423, 77, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(594, 34, 22, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(617, 34, 24, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(662, 36, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(683, 34, 20, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(642, 36, 19, 12));
            break;
          case AnimalType.Elephant:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(22, 0, 25, 18), new Vector2(7.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(78, 240, 25, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(76, 279, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(25, 259, 28, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 259, 24, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(54, 260, 25, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(0, 277, 25, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(51, 279, 24, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(26, 279, 24, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(80, 259, 24, 19));
            break;
          case AnimalType.Cheetah:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(21, 56, 22, 15), new Vector2(7.5f, 13f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(181, 404, 24, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(463, 245, 21, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(304, 273, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle((int) byte.MaxValue, 284, 26, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 232, 23, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(304, 288, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 248, 23, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 249, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 265, 22, 15));
            break;
          case AnimalType.Otter:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(45, 20, 23, 12), new Vector2(7.5f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(482, 160, 24, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(233, 261, 25, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(256, 270, 24, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(285, 244, 21, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(285, 256, 21, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(482, 176, 24, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(207, 324, 22, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 179, 16, 9));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(508, 152, 23, 12));
            break;
          case AnimalType.Owl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(143, 429, 13, 18), new Vector2(7f, 16f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(229, 352, 11, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 349, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 333, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(369, 287, 15, 18));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(228, 376, 12, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(532, 144, 9, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 365, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(241, 381, 13, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(257, 317, 15, 15));
            break;
          case AnimalType.Rhino:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(22, 29, 22, 16), new Vector2(7f, 14f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(506, 34, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(499, 0, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(522, 0, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(517, 17, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(476, 0, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(472, 16, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(494, 16, 22, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 34, 22, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(423, 57, 22, 19));
            break;
          case AnimalType.Panda:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(19, 81, 21, 16), new Vector2(9f, 13f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 272, 20, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(636, 68, 14, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(207, 338, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 165, 23, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(465, 187, 17, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(233, 274, 22, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 181, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(348, 279, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(483, 189, 23, 18));
            break;
          case AnimalType.Giraffe:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 0, 21, 31), new Vector2(7.5f, 12f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(185, 324, 21, 31));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(510, 88, 21, 31));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(510, 120, 21, 31));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(347, 174, 21, 34));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(84, 408, 19, 29));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(62, 409, 21, 33));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(402, 174, 19, 29));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(347, 209, 21, 33));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(402, 204, 19, 27));
            break;
          case AnimalType.Hippopotamus:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(116, 0, 23, 16), new Vector2(5.5f, 14f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(465, 203, 17, 11));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(507, 198, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(184, 356, 22, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(207, 356, 21, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(281, 270, 22, 13));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(181, 371, 23, 16));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(205, 372, 22, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(181, 388, 24, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(206, 388, 21, 15));
            break;
          case AnimalType.Lion:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 40, 20, 17), new Vector2(6f, 11f), 4);
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 224, 19, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(385, 166, 17, 12));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 209, 22, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(422, 229, 22, 19));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(163, 414, 17, 14));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(209, 306, 20, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(327, 240, 20, 15));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(402, 232, 19, 17));
            EnemyData.enemydata[(int) enemytype].AddVariant(new Rectangle(348, 243, 20, 17));
            break;
          case AnimalType.Riddick:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 811, 13, 19), new Vector2(6f, 11f), 4);
            break;
          case AnimalType.Tardigrade:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(56, 811, 17, 18), new Vector2(10f, 10f), 4);
            break;
          case AnimalType.Galaxian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(423, 829, 21, 22), new Vector2(11f, 14f), 4);
            break;
          case AnimalType.Bill:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(600, 943, 13, 19), new Vector2(6.5f, 11f), 4);
            break;
          case AnimalType.Gremlin:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(324, 833, 15, 18), new Vector2(7.5f, 10f), 4);
            break;
          case AnimalType.Krampus:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(568, 1004, 16, 20), new Vector2(8f, 12f), 4);
            break;
          case AnimalType.Grinch:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(588, 982, 14, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkSkinBlueVest:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 831, 12, 18), new Rectangle(52, 831, 13, 18), new Rectangle(108, 831, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BrownHairMaskedLady:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(160, 831, 12, 18), new Rectangle(212, 831, 13, 18), new Rectangle(268, 831, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkSkinOldMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(320, 833, 12, 16), new Rectangle(372, 833, 12, 16), new Rectangle(424, 833, 12, 16), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.LightSkinOldMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(476, 833, 12, 16), new Rectangle(528, 833, 12, 16), new Rectangle(580, 833, 12, 16), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkSkinPineappleHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 850, 10, 21), new Rectangle(44, 850, 14, 21), new Rectangle(104, 850, 10, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BigDarkHairGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(148, 850, 18, 21), new Rectangle(224, 850, 17, 21), new Rectangle(296, 850, 18, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AliceBlueRibbon:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(372, 850, 14, 20), new Rectangle(432, 850, 14, 21), new Rectangle(492, 850, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WhiteHatBlueDress:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(552, 851, 18, 19), new Rectangle(628, 851, 18, 19), new Rectangle(704, 851, 18, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PinkHat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(780, 851, 16, 19), new Rectangle(848, 851, 16, 19), new Rectangle(916, 851, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkSkinGreenShirtKid:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 872, 14, 17), new Rectangle(60, 872, 14, 17), new Rectangle(120, 872, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedBuns:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(180, 872, 16, 17), new Rectangle(248, 872, 14, 17), new Rectangle(308, 872, 16, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OrangeBeardMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(376, 872, 12, 17), new Rectangle(428, 872, 12, 17), new Rectangle(480, 872, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BeardedOldMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(532, 872, 12, 17), new Rectangle(584, 872, 12, 17), new Rectangle(636, 872, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PurpleShirtOldLady:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(688, 871, 14, 18), new Rectangle(748, 871, 14, 18), new Rectangle(808, 871, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PinkDressFlowerGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(868, 871, 12, 18), new Rectangle(920, 871, 12, 18), new Rectangle(972, 871, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.LightBrownHairBlueShirtMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 890, 14, 18), new Rectangle(60, 890, 14, 18), new Rectangle(120, 890, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkSkinPinkJacketGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(180, 890, 14, 18), new Rectangle(240, 890, 13, 18), new Rectangle(296, 890, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OrangeHairBlueHeadband:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(356, 890, 12, 18), new Rectangle(408, 890, 13, 18), new Rectangle(464, 890, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.GreenBandana:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(516, 890, 12, 18), new Rectangle(568, 890, 13, 18), new Rectangle(624, 890, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlueHoodie:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(676, 890, 12, 18), new Rectangle(728, 890, 12, 18), new Rectangle(780, 890, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedBandanaBiker:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(832, 890, 14, 18), new Rectangle(892, 890, 13, 18), new Rectangle(948, 890, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CenterPartHairYellowShirtMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 909, 12, 18), new Rectangle(52, 909, 12, 18), new Rectangle(104, 909, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OrangePlaitsGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(156, 909, 16, 18), new Rectangle(224, 909, 13, 18), new Rectangle(280, 909, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedHeadbandMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(348, 909, 14, 18), new Rectangle(408, 909, 13, 18), new Rectangle(464, 909, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.YellowShirtKid:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(524, 909, 14, 18), new Rectangle(584, 909, 13, 18), new Rectangle(640, 909, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PurpleBandanaLady:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(700, 909, 14, 18), new Rectangle(760, 909, 15, 18), new Rectangle(824, 909, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PinkShirtOldLady:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 928, 12, 18), new Rectangle(52, 928, 12, 18), new Rectangle(104, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondePunk:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(156, 928, 12, 18), new Rectangle(208, 928, 13, 18), new Rectangle(264, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlueCapBoy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(316, 928, 12, 18), new Rectangle(368, 928, 14, 18), new Rectangle(428, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlueHeadphones:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(480, 928, 14, 18), new Rectangle(540, 928, 12, 18), new Rectangle(592, 928, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondeTwintails:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(652, 928, 22, 18), new Rectangle(744, 928, 18, 18), new Rectangle(820, 928, 22, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SailorBoy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(892, 948, 15, 19), new Rectangle(956, 948, 14, 19), new Rectangle(912, 928, 15, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AfroPinkDress:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 947, 14, 20), new Rectangle(60, 947, 14, 20), new Rectangle(120, 947, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HighPonytailBlueJumper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(180, 947, 12, 20), new Rectangle(232, 947, 14, 20), new Rectangle(292, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AfroOldLady:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(344, 947, 14, 20), new Rectangle(404, 947, 14, 20), new Rectangle(464, 947, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AfroGreenShirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(524, 947, 14, 20), new Rectangle(584, 947, 14, 20), new Rectangle(644, 947, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PinkHairGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(704, 947, 15, 20), new Rectangle(768, 947, 14, 20), new Rectangle(828, 947, 15, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BleachedHairBoy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 968, 12, 18), new Rectangle(52, 968, 12, 18), new Rectangle(104, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondeHairPurpleShirtGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(156, 968, 12, 18), new Rectangle(208, 968, 13, 18), new Rectangle(264, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedTwintails:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(316, 968, 20, 18), new Rectangle(400, 968, 16, 18), new Rectangle(468, 968, 20, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedBeanie:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(552, 968, 12, 18), new Rectangle(604, 968, 12, 18), new Rectangle(656, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.LongHairPinkJumper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(848, 968, 14, 17), new Rectangle(908, 968, 13, 17), new Rectangle(964, 968, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PinkPatchHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 987, 10, 18), new Rectangle(44, 987, 12, 18), new Rectangle(0, 1006, 10, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.GreenMohawk:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(96, 987, 10, 18), new Rectangle(140, 987, 12, 18), new Rectangle(192, 987, 10, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BrownHairRedShirtGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(236, 987, 12, 18), new Rectangle(288, 987, 13, 18), new Rectangle(344, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BowlCutGreenShirtGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(396, 987, 12, 18), new Rectangle(448, 987, 12, 18), new Rectangle(500, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.StrawHat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(552, 987, 16, 18), new Rectangle(620, 987, 17, 18), new Rectangle(692, 987, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SunglassesBlondeHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(760, 987, 12, 18), new Rectangle(812, 987, 13, 18), new Rectangle(796, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpikyHairBlueSuit:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(868, 986, 12, 19), new Rectangle(920, 986, 12, 19), new Rectangle(972, 986, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlueShirtGuy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(44, 1006, 12, 18), new Rectangle(96, 1006, 13, 18), new Rectangle(152, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OrangeHairGreenShirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(204, 1006, 12, 18), new Rectangle(256, 1006, 12, 18), new Rectangle(308, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CenterPartingRedShirtGuy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(360, 1006, 12, 18), new Rectangle(412, 1006, 12, 18), new Rectangle(464, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlackSingletGuy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(516, 1006, 12, 18), new Rectangle(568, 1006, 13, 18), new Rectangle(624, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BleachedHairPinkSkirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(676, 1006, 12, 18), new Rectangle(728, 1006, 13, 18), new Rectangle(784, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedHairBlueOveralls:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(836, 1006, 14, 18), new Rectangle(896, 1006, 16, 18), new Rectangle(964, 1006, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PurpleBeanieGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(787, 753, 12, 18), new Rectangle(839, 753, 14, 18), new Rectangle(899, 753, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlackHatOldMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(474, 733, 16, 19), new Rectangle(542, 733, 16, 19), new Rectangle(610, 733, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.GreenHatGreenVestMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(270, 733, 16, 19), new Rectangle(338, 733, 16, 19), new Rectangle(406, 733, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondeHairLightBlueShirtMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(94, 714, 12, 17), new Rectangle(146, 714, 12, 17), new Rectangle(198, 714, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.YellowHatKid:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(708, 968, 16, 18), new Rectangle(884, 909, 16, 18), new Rectangle(952, 909, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RedRibbonGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(94, 732, 14, 20), new Rectangle(154, 732, 13, 21), new Rectangle(210, 732, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondeSpikyRedShirtMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(678, 733, 12, 19), new Rectangle(730, 733, 12, 19), new Rectangle(782, 733, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlueHairGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(270, 714, 12, 18), new Rectangle(322, 714, 15, 18), new Rectangle(386, 714, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OrangeHijabGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(438, 714, 12, 18), new Rectangle(490, 714, 12, 18), new Rectangle(542, 714, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WhiteHijabGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(594, 714, 12, 18), new Rectangle(646, 714, 12, 18), new Rectangle(698, 714, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KittyHeadphonesGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(750, 714, 14, 18), new Rectangle(810, 714, 12, 18), new Rectangle(862, 714, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ChinaGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(462, 696, 16, 17), new Rectangle(530, 696, 14, 17), new Rectangle(590, 696, 16, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DarkHatRedShirtGuy:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(94, 694, 14, 19), new Rectangle(154, 693, 14, 20), new Rectangle(214, 693, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlackHairBlackSuitMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(834, 734, 14, 18), new Rectangle(894, 734, 14, 18), new Rectangle(954, 734, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlondeSidePonytailGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(274, 693, 15, 20), new Rectangle(338, 693, 14, 20), new Rectangle(398, 693, 15, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MaleZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 811, 16, 19), new Rectangle(68, 811, 16, 19), new Rectangle(136, 811, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FemaleZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(204, 811, 16, 19), new Rectangle(272, 811, 16, 19), new Rectangle(340, 811, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AustralianZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(548, 814, 16, 18), new Rectangle(408, 813, 17, 19), new Rectangle(480, 813, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.LondonZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(616, 813, 14, 18), new Rectangle(676, 813, 14, 18), new Rectangle(736, 813, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TokyoZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(632, 832, 12, 18), new Rectangle(684, 832, 14, 18), new Rectangle(744, 832, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.UtahZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(465, 655, 14, 18), new Rectangle(525, 656, 14, 18), new Rectangle(585, 656, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.OaklandZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(349, 635, 14, 19), new Rectangle(409, 635, 14, 20), new Rectangle(469, 634, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MoscowZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(297, 655, 12, 18), new Rectangle(349, 655, 15, 18), new Rectangle(413, 655, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TorontoZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(117, 655, 14, 18), new Rectangle(177, 655, 14, 18), new Rectangle(237, 655, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.QuestionMark:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(88, 200, 16, 17), new Vector2(6f, 11f), 4);
            break;
          case AnimalType.MaleAsianZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(84, 475, 16, 19), new Rectangle(152, 475, 16, 19), new Rectangle(381, 489, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FemaleAsianZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(413, 452, 16, 19), new Rectangle(481, 452, 16, 19), new Rectangle(549, 451, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MaleDarkZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(617, 451, 16, 19), new Rectangle(685, 451, 16, 19), new Rectangle(753, 451, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FemaleDarkZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(821, 451, 16, 19), new Rectangle(889, 451, 16, 19), new Rectangle(957, 451, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalShelterGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1451, 193, 12, 18), new Rectangle(1503, 193, 15, 18), new Rectangle(1567, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SingaporeZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1180, 1006, 16, 18), new Rectangle(1248, 1006, 13, 18), new Rectangle(1304, 1006, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.RioZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1372, 1006, 12, 18), new Rectangle(1424, 1006, 14, 18), new Rectangle(1484, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ChinaZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1344, 947, 12, 20), new Rectangle(1396, 947, 16, 19), new Rectangle(1464, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CubaZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1536, 1006, 14, 18), new Rectangle(1596, 1006, 14, 18), new Rectangle(1656, 1006, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AfricaZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1516, 947, 12, 20), new Rectangle(1568, 947, 15, 19), new Rectangle(1632, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SurabayaZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1716, 1006, 12, 18), new Rectangle(1768, 1006, 14, 18), new Rectangle(1828, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BerlinZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1880, 1006, 12, 17), new Rectangle(1932, 1006, 12, 17), new Rectangle(1984, 1006, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BangladeshZookeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1824, 969, 14, 18), new Rectangle(1884, 969, 14, 18), new Rectangle(1944, 969, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotGonky:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(796, 829, 18, 21), new Rectangle(872, 829, 12, 21), new Rectangle(924, 829, 18, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotOctoman:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(932, 787, 22, 20), new Rectangle(948, 766, 18, 20), new Rectangle(932, 808, 22, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(259, 792, 18, 18), new Rectangle(335, 792, 14, 18), new Rectangle(395, 792, 19, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(475, 792, 20, 20), new Rectangle(559, 792, 15, 20), new Rectangle(623, 792, 21, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(711, 794, 18, 18), new Rectangle(787, 794, 15, 18), new Rectangle(851, 794, 19, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(259, 773, 12, 18), new Rectangle(311, 773, 17, 18), new Rectangle(383, 773, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(435, 773, 12, 18), new Rectangle(487, 773, 14, 18), new Rectangle(547, 773, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(599, 773, 14, 18), new Rectangle(659, 773, 16, 18), new Rectangle(727, 775, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(259, 753, 13, 19), new Rectangle(315, 753, 14, 19), new Rectangle(375, 753, 13, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(431, 753, 13, 19), new Rectangle(487, 753, 14, 19), new Rectangle(547, 753, 13, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(603, 753, 14, 19), new Rectangle(663, 753, 15, 19), new Rectangle(727, 755, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotBear:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1, 642, 22, 22), new Rectangle(1, 619, 15, 22), new Rectangle(1, 665, 22, 22), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotShark:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(529, 629, 21, 26), new Rectangle(617, 629, 17, 26), new Rectangle(689, 629, 21, 26), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotSharkFace:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(777, 629, 21, 26), new Rectangle(865, 629, 17, 26), new Rectangle(937, 629, 21, 26), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(742, 675, 18, 18), new Rectangle(818, 675, 14, 18), new Rectangle(878, 675, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 656, 18, 18), new Rectangle(954, 675, 14, 18), new Rectangle(945, 656, 19, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TourGuideWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(645, 656, 19, 18), new Rectangle(725, 656, 14, 18), new Rectangle(785, 656, 20, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(94, 674, 12, 18), new Rectangle(146, 674, 14, 18), new Rectangle(206, 674, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(258, 674, 14, 18), new Rectangle(318, 674, 16, 18), new Rectangle(386, 674, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.KeeperWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(446, 674, 12, 18), new Rectangle(630, 675, 14, 18), new Rectangle(690, 675, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(658, 694, 14, 19), new Rectangle(718, 694, 16, 19), new Rectangle(786, 694, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(846, 694, 13, 19), new Rectangle(902, 694, 14, 19), new Rectangle(962, 694, 13, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CleanerWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(498, 675, 16, 19), new Rectangle(566, 675, 15, 19), new Rectangle(922, 714, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(177, 634, 12, 20), new Rectangle(229, 634, 16, 20), new Rectangle(297, 634, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(357, 594, 12, 18), new Rectangle(409, 594, 13, 18), new Rectangle(465, 589, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(693, 589, 12, 18), new Rectangle(745, 588, 16, 18), new Rectangle(813, 588, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(517, 589, 14, 18), new Rectangle(577, 589, 13, 18), new Rectangle(633, 589, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(173, 595, 14, 18), new Rectangle(233, 594, 15, 18), new Rectangle(297, 594, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.VetWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(865, 588, 12, 18), new Rectangle(917, 588, 13, 18), new Rectangle(973, 588, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotPenguin:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(233, 613, 21, 20), new Rectangle(321, 613, 15, 20), new Rectangle(385, 613, 21, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotPig:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(473, 608, 27, 20), new Rectangle(585, 608, 19, 20), new Rectangle(665, 608, 27, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MascotPanda:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(777, 607, 22, 21), new Rectangle(869, 607, 15, 21), new Rectangle(933, 607, 22, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(70, 555, 12, 18), new Rectangle(122, 555, 14, 18), new Rectangle(182, 555, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(398, 555, 14, 18), new Rectangle(497, 550, 15, 18), new Rectangle(117, 636, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(234, 555, 12, 18), new Rectangle(286, 555, 14, 18), new Rectangle(346, 555, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(665, 529, 12, 18), new Rectangle(717, 529, 17, 18), new Rectangle(789, 529, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(501, 530, 12, 18), new Rectangle(553, 530, 14, 18), new Rectangle(613, 530, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MechanicWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(841, 530, 14, 18), new Rectangle(901, 530, 15, 18), new Rectangle(965, 530, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(418, 535, 12, 19), new Rectangle(409, 509, 14, 19), new Rectangle(469, 509, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(70, 535, 12, 19), new Rectangle(122, 535, 14, 19), new Rectangle(182, 535, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(234, 535, 14, 19), new Rectangle(294, 535, 15, 19), new Rectangle(358, 535, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(521, 509, 12, 19), new Rectangle(573, 509, 14, 19), new Rectangle(633, 509, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(685, 509, 12, 19), new Rectangle(737, 509, 17, 19), new Rectangle(809, 509, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SecurityGuardWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(861, 510, 12, 19), new Rectangle(913, 510, 14, 19), new Rectangle(973, 510, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1360, 271, 12, 18), new Rectangle(1412, 271, 14, 18), new Rectangle(1472, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1423, 252, 12, 18), new Rectangle(1475, 252, 14, 18), new Rectangle(1535, 252, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1387, 174, 12, 18), new Rectangle(1439, 174, 14, 18), new Rectangle(1499, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1418, 155, 14, 18), new Rectangle(1478, 155, 15, 18), new Rectangle(1542, 156, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 457, 12, 18), new Rectangle(1077, 457, 17, 18), new Rectangle(1149, 457, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ArchitectWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1201, 457, 12, 18), new Rectangle(1253, 457, 14, 18), new Rectangle(1313, 457, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(693, 413, 14, 18), new Rectangle(753, 413, 13, 18), new Rectangle(809, 413, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 413, 12, 18), new Rectangle(921, 413, 12, 18), new Rectangle(973, 413, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(341, 433, 12, 18), new Rectangle(393, 433, 18, 18), new Rectangle(469, 433, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(521, 432, 14, 18), new Rectangle(581, 432, 12, 18), new Rectangle(633, 432, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(693, 432, 12, 18), new Rectangle(745, 432, 12, 18), new Rectangle(797, 432, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PandaBurger_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(849, 432, 14, 18), new Rectangle(909, 432, 13, 18), new Rectangle(965, 432, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(528, 271, 12, 18), new Rectangle(580, 271, 13, 18), new Rectangle(636, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(688, 271, 12, 18), new Rectangle(740, 271, 14, 18), new Rectangle(800, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(852, 271, 12, 18), new Rectangle(904, 271, 14, 18), new Rectangle(964, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(825, 311, 16, 18), new Rectangle(893, 311, 15, 18), new Rectangle(957, 311, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 370, 12, 20), new Rectangle(1077, 372, 16, 18), new Rectangle(1145, 372, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_SouvenirStall_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(867, 193, 14, 18), new Rectangle(927, 193, 14, 18), new Rectangle(987, 193, 15, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(477, 311, 14, 18), new Rectangle(537, 311, 13, 18), new Rectangle(593, 311, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(689, 330, 14, 20), new Rectangle(749, 330, 14, 20), new Rectangle(809, 330, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 330, 12, 20), new Rectangle(921, 330, 12, 20), new Rectangle(973, 330, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(501, 351, 12, 18), new Rectangle(553, 351, 14, 18), new Rectangle(613, 351, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(665, 351, 12, 18), new Rectangle(717, 351, 18, 18), new Rectangle(793, 351, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_CottonCandy_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(845, 351, 12, 18), new Rectangle(897, 351, 18, 18), new Rectangle(973, 351, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(641, 290, 18, 20), new Rectangle(717, 290, 14, 20), new Rectangle(777, 290, 18, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(853, 290, 12, 20), new Rectangle(905, 290, 16, 20), new Rectangle(973, 290, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(517, 370, 12, 21), new Rectangle(569, 370, 12, 21), new Rectangle(621, 370, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(325, 351, 14, 19), new Rectangle(385, 351, 13, 19), new Rectangle(441, 351, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(357, 330, 14, 20), new Rectangle(417, 330, 13, 20), new Rectangle(473, 330, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_LionHotDog_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(531, 174, 14, 18), new Rectangle(591, 174, 12, 18), new Rectangle(643, 174, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(699, 212, 12, 20), new Rectangle(751, 212, 12, 20), new Rectangle(803, 212, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(887, 252, 14, 18), new Rectangle(947, 252, 14, 18), new Rectangle(1007, 252, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(533, 330, 12, 20), new Rectangle(585, 330, 12, 20), new Rectangle(637, 330, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(297, 371, 20, 20), new Rectangle(381, 371, 12, 20), new Rectangle(433, 371, 20, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(313, 392, 16, 20), new Rectangle(381, 392, 14, 20), new Rectangle(441, 392, 16, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Popcorn_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(157, 430, 14, 20), new Rectangle(217, 430, 14, 20), new Rectangle(277, 430, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(517, 413, 14, 18), new Rectangle(577, 413, 13, 18), new Rectangle(633, 413, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(689, 391, 12, 21), new Rectangle(741, 391, 12, 21), new Rectangle(793, 391, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(845, 391, 14, 21), new Rectangle(905, 391, 14, 21), new Rectangle(965, 391, 14, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(673, 370, 16, 20), new Rectangle(741, 370, 14, 20), new Rectangle(801, 370, 16, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 370, 12, 20), new Rectangle(921, 370, 12, 20), new Rectangle(973, 370, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Churro_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(152, 451, 12, 23), new Rectangle(204, 451, 12, 23), new Rectangle(256, 451, 12, 23), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(886, 155, 12, 18), new Rectangle(938, 155, 16, 18), new Rectangle(1006, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 233, 12, 18), new Rectangle(1077, 233, 14, 18), new Rectangle(1137, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1016, 271, 12, 18), new Rectangle(1068, 271, 14, 18), new Rectangle(1128, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 330, 12, 20), new Rectangle(1077, 330, 13, 20), new Rectangle(1133, 330, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 391, 12, 21), new Rectangle(1077, 391, 15, 21), new Rectangle(1141, 391, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Slushie_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 432, 12, 24), new Rectangle(1077, 432, 12, 24), new Rectangle(1129, 432, 12, 24), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 432, 12, 23), new Rectangle(1233, 432, 16, 23), new Rectangle(1301, 432, 12, 23), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1067, 252, 14, 18), new Rectangle(1127, 252, 13, 18), new Rectangle(1183, 252, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(711, 252, 14, 18), new Rectangle(771, 252, 13, 18), new Rectangle(827, 252, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(531, 250, 14, 20), new Rectangle(591, 251, 14, 19), new Rectangle(651, 252, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(653, 311, 14, 18), new Rectangle(713, 311, 12, 18), new Rectangle(765, 311, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Balloon_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(509, 392, 14, 20), new Rectangle(569, 392, 14, 20), new Rectangle(629, 392, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(531, 193, 12, 18), new Rectangle(583, 193, 15, 18), new Rectangle(647, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(699, 193, 12, 18), new Rectangle(751, 193, 15, 18), new Rectangle(815, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(895, 174, 12, 18), new Rectangle(947, 174, 15, 18), new Rectangle(1011, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(413, 413, 12, 18), new Rectangle(465, 413, 12, 18), new Rectangle(661, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(713, 233, 12, 18), new Rectangle(765, 233, 12, 18), new Rectangle(817, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_RustyKeg_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 233, 12, 18), new Rectangle(921, 233, 12, 18), new Rectangle(973, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1035, 212, 16, 20), new Rectangle(1103, 212, 16, 20), new Rectangle(1171, 212, 16, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1051, 193, 16, 18), new Rectangle(1119, 193, 14, 18), new Rectangle(1179, 193, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(855, 212, 14, 20), new Rectangle(915, 212, 14, 20), new Rectangle(975, 212, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(703, 174, 16, 18), new Rectangle(771, 174, 13, 18), new Rectangle(827, 174, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(531, 212, 12, 20), new Rectangle(583, 212, 15, 20), new Rectangle(647, 212, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KangarooPizza_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(485, 290, 12, 20), new Rectangle(537, 290, 12, 20), new Rectangle(589, 290, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(542, 155, 12, 18), new Rectangle(594, 155, 14, 18), new Rectangle(654, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(706, 155, 12, 18), new Rectangle(758, 155, 18, 18), new Rectangle(834, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1058, 155, 12, 18), new Rectangle(1110, 155, 18, 18), new Rectangle(1186, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1063, 174, 12, 18), new Rectangle(1115, 174, 13, 18), new Rectangle(1171, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 311, 12, 18), new Rectangle(1077, 311, 18, 18), new Rectangle(1153, 311, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamVan_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 351, 12, 18), new Rectangle(1077, 351, 12, 18), new Rectangle(1129, 351, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1243, 252, 14, 18), new Rectangle(1303, 252, 14, 18), new Rectangle(1363, 252, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 290, 12, 20), new Rectangle(1077, 290, 12, 20), new Rectangle(1129, 290, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 290, 12, 20), new Rectangle(1233, 290, 12, 20), new Rectangle(1285, 290, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1185, 330, 12, 20), new Rectangle(1237, 330, 12, 20), new Rectangle(1289, 330, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 351, 14, 19), new Rectangle(1241, 351, 14, 19), new Rectangle(1301, 351, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_IceCreamStore_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1197, 371, 12, 19), new Rectangle(1249, 371, 13, 19), new Rectangle(1305, 371, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1238, 155, 14, 18), new Rectangle(1298, 155, 14, 18), new Rectangle(1358, 155, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1223, 174, 13, 18), new Rectangle(1278, 174, 12, 18), new Rectangle(1331, 174, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1189, 233, 12, 18), new Rectangle(1241, 233, 12, 18), new Rectangle(1293, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1180, 271, 14, 18), new Rectangle(1240, 271, 14, 18), new Rectangle(1300, 271, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1205, 311, 13, 18), new Rectangle(1261, 311, 12, 18), new Rectangle(1313, 311, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_Coconut_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1369, 311, 12, 18), new Rectangle(1421, 311, 13, 18), new Rectangle(1477, 311, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1247, 193, 18, 18), new Rectangle(1323, 193, 12, 18), new Rectangle(1375, 193, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1239, 212, 18, 20), new Rectangle(1315, 213, 14, 19), new Rectangle(1375, 214, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1345, 233, 12, 18), new Rectangle(1397, 233, 15, 18), new Rectangle(1461, 233, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1337, 290, 14, 20), new Rectangle(1397, 290, 13, 20), new Rectangle(1453, 290, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 413, 22, 18), new Rectangle(1117, 413, 16, 18), new Rectangle(1185, 413, 22, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ElephantSouvenir_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1277, 414, 22, 18), new Rectangle(1369, 413, 16, 18), new Rectangle(1437, 413, 22, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1365, 457, 15, 18), new Rectangle(1429, 457, 14, 18), new Rectangle(1489, 457, 15, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1189, 515, 12, 21), new Rectangle(1241, 515, 13, 21), new Rectangle(1297, 515, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1349, 515, 12, 21), new Rectangle(1401, 515, 12, 21), new Rectangle(1453, 515, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1369, 537, 16, 21), new Rectangle(1437, 537, 13, 20), new Rectangle(1493, 537, 17, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1513, 372, 14, 18), new Rectangle(1573, 372, 14, 18), new Rectangle(1633, 372, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_KatCoffee_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 559, 14, 18), new Rectangle(1085, 559, 14, 18), new Rectangle(1145, 559, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1361, 352, 14, 19), new Rectangle(1369, 413, 16, 18), new Rectangle(1437, 413, 22, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1205, 559, 14, 17), new Rectangle(1265, 559, 14, 17), new Rectangle(1325, 559, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 578, 14, 20), new Rectangle(1085, 578, 19, 20), new Rectangle(1165, 578, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1225, 578, 14, 19), new Rectangle(1285, 578, 12, 19), new Rectangle(1337, 577, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1385, 559, 16, 17), new Rectangle(1453, 559, 14, 17), new Rectangle(1513, 559, 16, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_ShellShack_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1525, 476, 12, 18), new Rectangle(1577, 476, 14, 18), new Rectangle(1637, 476, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 476, 12, 18), new Rectangle(1077, 476, 12, 18), new Rectangle(1129, 476, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 495, 14, 18), new Rectangle(1085, 495, 14, 18), new Rectangle(1145, 495, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 476, 14, 18), new Rectangle(1241, 476, 14, 18), new Rectangle(1301, 476, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1361, 476, 12, 18), new Rectangle(1413, 476, 14, 18), new Rectangle(1473, 476, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1197, 495, 15, 18), new Rectangle(1261, 495, 13, 18), new Rectangle(1317, 495, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_TacoTruck_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1305, 391, 12, 21), new Rectangle(1357, 391, 15, 21), new Rectangle(1421, 392, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 514, 12, 22), new Rectangle(1077, 514, 14, 22), new Rectangle(1137, 514, 12, 22), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 537, 14, 21), new Rectangle(1241, 537, 16, 21), new Rectangle(1309, 537, 14, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1357, 372, 12, 18), new Rectangle(1409, 372, 12, 18), new Rectangle(1461, 372, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1377, 495, 12, 18), new Rectangle(1429, 495, 13, 18), new Rectangle(1485, 495, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 537, 12, 21), new Rectangle(1077, 537, 12, 21), new Rectangle(1129, 537, 12, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Vendor_PretzelShop_6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1341, 330, 12, 20), new Rectangle(1393, 330, 12, 20), new Rectangle(1445, 330, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederWhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1551, 174, 12, 18), new Rectangle(1603, 174, 12, 18), new Rectangle(1655, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederBlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1553, 458, 12, 17), new Rectangle(1605, 458, 12, 17), new Rectangle(1657, 458, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederAsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1537, 495, 12, 18), new Rectangle(1589, 495, 12, 18), new Rectangle(1641, 495, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederWhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1397, 577, 15, 20), new Rectangle(1461, 577, 14, 20), new Rectangle(1521, 577, 15, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederBlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1576, 271, 12, 18), new Rectangle(1628, 271, 15, 18), new Rectangle(1692, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BreederAsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1529, 312, 14, 17), new Rectangle(1589, 312, 13, 17), new Rectangle(1645, 312, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherAsianWithGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1025, 599, 12, 18), new Rectangle(1077, 599, 12, 18), new Rectangle(1129, 599, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherAsianNoGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1181, 599, 12, 18), new Rectangle(1233, 599, 12, 18), new Rectangle(1285, 599, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherBlackWithGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1337, 598, 12, 18), new Rectangle(1389, 598, 12, 18), new Rectangle(1441, 598, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherBlackNoGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1493, 598, 12, 18), new Rectangle(1545, 598, 12, 18), new Rectangle(1597, 598, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherWhiteWithGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1587, 252, 12, 18), new Rectangle(1639, 252, 12, 18), new Rectangle(1691, 252, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.DNAResearcherWhiteNoGoggles:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1619, 193, 12, 18), new Rectangle(1671, 193, 12, 18), new Rectangle(1723, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerAsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1762, 155, 12, 18), new Rectangle(1814, 155, 12, 18), new Rectangle(1866, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerAsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1451, 212, 12, 20), new Rectangle(1503, 212, 15, 20), new Rectangle(1567, 212, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerWhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1617, 234, 12, 17), new Rectangle(1669, 234, 12, 17), new Rectangle(1721, 234, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerWhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1473, 391, 12, 20), new Rectangle(1525, 391, 15, 18), new Rectangle(1589, 391, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerBlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1602, 155, 12, 18), new Rectangle(1654, 155, 13, 18), new Rectangle(1710, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MeatProcessorWorkerBlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1707, 174, 12, 18), new Rectangle(1759, 174, 15, 18), new Rectangle(1823, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SlaughterhouseEmployeeAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1775, 193, 12, 18), new Rectangle(1827, 193, 12, 18), new Rectangle(1879, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SlaughterhouseEmployeeWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1875, 174, 12, 18), new Rectangle(1927, 174, 12, 18), new Rectangle(1879, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SlaughterhouseEmployeeBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1918, 155, 12, 18), new Rectangle(1970, 155, 12, 18), new Rectangle(1879, 193, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeAsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1762, 155, 12, 18), new Rectangle(1814, 155, 12, 18), new Rectangle(1866, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeAsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1451, 212, 12, 20), new Rectangle(1503, 212, 15, 20), new Rectangle(1567, 212, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeWhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1617, 234, 12, 17), new Rectangle(1669, 234, 12, 17), new Rectangle(1721, 234, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeWhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1473, 391, 12, 20), new Rectangle(1525, 391, 15, 18), new Rectangle(1589, 391, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeBlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1602, 155, 12, 18), new Rectangle(1654, 155, 13, 18), new Rectangle(1710, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HotAirBalloonEmployeeBlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1707, 174, 12, 18), new Rectangle(1759, 174, 15, 18), new Rectangle(1823, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeAsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1762, 155, 12, 18), new Rectangle(1814, 155, 12, 18), new Rectangle(1866, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeAsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1451, 212, 12, 20), new Rectangle(1503, 212, 15, 20), new Rectangle(1567, 212, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeWhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1617, 234, 12, 17), new Rectangle(1669, 234, 12, 17), new Rectangle(1721, 234, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeWhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1473, 391, 12, 20), new Rectangle(1525, 391, 15, 18), new Rectangle(1589, 391, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeBlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1602, 155, 12, 18), new Rectangle(1654, 155, 13, 18), new Rectangle(1710, 155, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HelicopterEmployeeBlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1707, 174, 12, 18), new Rectangle(1759, 174, 15, 18), new Rectangle(1823, 174, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FactoryWorkerAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1020, 909, 12, 18), new Rectangle(1072, 909, 12, 18), new Rectangle(1124, 909, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FactoryWorkerWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1176, 909, 12, 18), new Rectangle(1228, 909, 12, 18), new Rectangle(1280, 909, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FactoryWorkerBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1332, 909, 12, 18), new Rectangle(1384, 909, 12, 18), new Rectangle(1436, 909, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerWhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1692, 909, 13, 18), new Rectangle(1748, 909, 14, 18), new Rectangle(1808, 909, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerWhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1488, 909, 16, 18), new Rectangle(1556, 909, 16, 18), new Rectangle(1624, 909, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerAsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1676, 734, 16, 18), new Rectangle(1744, 734, 16, 18), new Rectangle(1812, 734, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerAsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1864, 909, 12, 18), new Rectangle(1916, 909, 14, 18), new Rectangle(1976, 909, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerBlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1880, 734, 12, 18), new Rectangle(1932, 734, 14, 18), new Rectangle(1992, 734, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FarmerBlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1597, 889, 16, 19), new Rectangle(1665, 889, 16, 19), new Rectangle(1733, 889, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_AsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 968, 14, 18), new Rectangle(1084, 967, 14, 19), new Rectangle(1144, 968, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_AsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1184, 987, 12, 18), new Rectangle(1236, 987, 14, 18), new Rectangle(1296, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_BlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1180, 947, 12, 20), new Rectangle(1232, 948, 14, 19), new Rectangle(1292, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_BlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1204, 968, 12, 18), new Rectangle(1256, 968, 15, 18), new Rectangle(1320, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_WhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1348, 987, 12, 18), new Rectangle(1400, 987, 14, 18), new Rectangle(1460, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.CropPicker_WhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 987, 12, 18), new Rectangle(1076, 987, 13, 18), new Rectangle(1132, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_AsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1372, 968, 12, 18), new Rectangle(1424, 968, 17, 18), new Rectangle(1496, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_AsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1512, 987, 12, 18), new Rectangle(1564, 987, 12, 18), new Rectangle(1616, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_BlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1965, 889, 12, 19), new Rectangle(1712, 968, 14, 18), new Rectangle(1772, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_BlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1548, 968, 12, 18), new Rectangle(1600, 968, 14, 18), new Rectangle(1660, 968, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_WhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1692, 928, 18, 18), new Rectangle(1768, 928, 17, 18), new Rectangle(1840, 928, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.WarehouseWorker_WhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1668, 987, 12, 18), new Rectangle(1720, 987, 13, 18), new Rectangle(1776, 987, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceBlack:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(353, 574, 12, 19), new Rectangle(405, 574, 14, 19), new Rectangle(469, 569, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(521, 569, 12, 19), new Rectangle(573, 569, 14, 19), new Rectangle(633, 569, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(685, 569, 12, 19), new Rectangle(737, 568, 14, 19), new Rectangle(797, 568, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceBlack2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(849, 568, 12, 19), new Rectangle(901, 568, 17, 19), new Rectangle(973, 568, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceAsian2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(189, 574, 12, 19), new Rectangle(241, 574, 14, 19), new Rectangle(301, 574, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceWhite2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(649, 549, 14, 19), new Rectangle(709, 548, 15, 19), new Rectangle(773, 548, 14, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.PoliceWithGun:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(561, 549, 21, 19), new Rectangle(141, 614, 22, 19), new Rectangle(65, 617, 12, 24), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.NewsReporter:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(57, 576, 16, 18), new Rectangle(125, 576, 15, 18), new Rectangle(105, 595, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Cameraman:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(833, 549, 14, 18), new Rectangle(893, 549, 16, 18), new Rectangle(961, 549, 15, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(70, 514, 12, 20), new Rectangle(122, 514, 12, 20), new Rectangle(174, 514, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(226, 514, 12, 20), new Rectangle(278, 514, 15, 20), new Rectangle(342, 514, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(869, 489, 12, 20), new Rectangle(921, 489, 12, 20), new Rectangle(973, 489, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(787, 772, 12, 20), new Rectangle(839, 772, 13, 20), new Rectangle(0, 504, 14, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(449, 489, 19, 19), new Rectangle(529, 489, 13, 19), new Rectangle(585, 489, 19, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Protestor6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(665, 489, 18, 19), new Rectangle(741, 489, 12, 19), new Rectangle(793, 489, 18, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker1:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(204, 496, 12, 17), new Rectangle(256, 496, 12, 17), new Rectangle(308, 496, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker2:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(557, 471, 12, 17), new Rectangle(609, 471, 13, 17), new Rectangle(665, 471, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker3:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(425, 472, 10, 16), new Rectangle(469, 472, 10, 16), new Rectangle(513, 472, 10, 16), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker4:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(60, 495, 10, 18), new Rectangle(104, 495, 13, 18), new Rectangle(160, 495, 10, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker5:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(717, 471, 10, 17), new Rectangle(761, 471, 10, 17), new Rectangle(805, 471, 10, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Biker6:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(849, 471, 14, 17), new Rectangle(909, 471, 13, 17), new Rectangle(965, 471, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.BlackMarketDealer:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(220, 475, 16, 20), new Rectangle(84, 455, 16, 19), new Rectangle(288, 475, 16, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TigerKing:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1513, 233, 12, 18), new Rectangle(1565, 233, 12, 18), new Rectangle(1524, 271, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Investor:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(0, 1025, 14, 18), new Rectangle(60, 1025, 15, 18), new Rectangle(124, 1025, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Mayor:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(184, 1025, 12, 18), new Rectangle(236, 1025, 13, 18), new Rectangle(292, 1025, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FootballCaptain:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(344, 1025, 14, 18), new Rectangle(404, 1025, 14, 18), new Rectangle(464, 1025, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.TransportPlanner:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1786, 329, 12, 17), new Rectangle(1838, 329, 12, 17), new Rectangle(1890, 329, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Footballer_SpikyHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1023, 753, 12, 19), new Rectangle(1075, 753, 12, 19), new Rectangle(1127, 753, 12, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Footballer_CombedBackHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 773, 12, 17), new Rectangle(1076, 773, 12, 17), new Rectangle(1128, 773, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Footballer_Blond:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1180, 772, 12, 18), new Rectangle(1232, 772, 12, 18), new Rectangle(1284, 772, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Footballer_Goalkeep:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1179, 753, 14, 18), new Rectangle(1239, 753, 13, 18), new Rectangle(1295, 753, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Teacher_Glasses:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1336, 772, 12, 18), new Rectangle(1388, 772, 12, 18), new Rectangle(1440, 772, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Teacher_WhiteHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1355, 753, 14, 18), new Rectangle(1415, 753, 14, 18), new Rectangle(1475, 753, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Teacher_OrangeBun:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 791, 12, 20), new Rectangle(1076, 791, 15, 20), new Rectangle(1140, 791, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalRightsActivist_RedShirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1535, 753, 12, 18), new Rectangle(1587, 753, 12, 18), new Rectangle(1639, 753, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalRightsActivist_GreenShirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1492, 772, 12, 18), new Rectangle(1544, 772, 12, 18), new Rectangle(1596, 772, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalRightsActivist_BlueShirt:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1648, 772, 12, 18), new Rectangle(1700, 772, 12, 18), new Rectangle(1752, 772, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SafetyInspector_White:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 812, 13, 17), new Rectangle(1080, 812, 14, 17), new Rectangle(1140, 812, 13, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SafetyInspector_AsianGirl:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1196, 812, 13, 17), new Rectangle(1252, 812, 17, 17), new Rectangle(1324, 812, 13, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SafetyInspector_Black:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1380, 812, 13, 17), new Rectangle(1436, 812, 14, 17), new Rectangle(1496, 812, 13, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalWelfare_BlackHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1552, 812, 13, 17), new Rectangle(1608, 812, 12, 17), new Rectangle(1660, 812, 13, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalWelfare_BlondeHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1691, 753, 14, 18), new Rectangle(1751, 753, 15, 18), new Rectangle(1815, 753, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalWelfare_BlackPonytail:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1804, 772, 13, 18), new Rectangle(1860, 772, 15, 18), new Rectangle(1924, 772, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HealthInspector_BlackLongHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1021, 830, 13, 18), new Rectangle(1077, 830, 13, 18), new Rectangle(1133, 830, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HealthInspector_Asian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1189, 830, 13, 18), new Rectangle(1245, 830, 12, 18), new Rectangle(1297, 830, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.HealthInspector_BlondeHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1716, 812, 14, 17), new Rectangle(1776, 812, 14, 17), new Rectangle(1836, 812, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MIB_White:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1028, 735, 12, 17), new Rectangle(1080, 735, 12, 17), new Rectangle(1132, 735, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MIB_Dark:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1184, 735, 12, 17), new Rectangle(1236, 735, 12, 17), new Rectangle(1288, 735, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MIB_Asian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1353, 830, 12, 18), new Rectangle(1405, 830, 13, 18), new Rectangle(1461, 830, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Hunter_RedCheckeredHat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1513, 830, 12, 18), new Rectangle(1565, 830, 14, 18), new Rectangle(1625, 830, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Hunter_BrownFurHat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1192, 791, 13, 19), new Rectangle(1248, 791, 13, 19), new Rectangle(1304, 791, 13, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_MaleWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1677, 830, 12, 18), new Rectangle(1729, 830, 13, 18), new Rectangle(1785, 830, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_FemaleWhite:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1360, 791, 15, 20), new Rectangle(1424, 791, 14, 20), new Rectangle(1484, 791, 15, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_MaleAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1875, 753, 12, 18), new Rectangle(1927, 753, 12, 18), new Rectangle(1979, 753, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_FemaleAsian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1241, 849, 18, 18), new Rectangle(1317, 849, 12, 18), new Rectangle(1369, 849, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_MaleDark:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1837, 830, 14, 18), new Rectangle(1897, 830, 13, 18), new Rectangle(1304, 791, 13, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Student_FemaleDark:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1021, 849, 18, 18), new Rectangle(1097, 849, 16, 18), new Rectangle(1165, 849, 18, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ChurchOfPureLife_White:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1445, 849, 15, 18), new Rectangle(1509, 849, 12, 18), new Rectangle(1561, 849, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ChurchOfPureLife_Asian:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1629, 849, 15, 18), new Rectangle(1693, 849, 12, 18), new Rectangle(1745, 849, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.ChurchOfPureLife_Black:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1813, 849, 15, 18), new Rectangle(1877, 849, 12, 18), new Rectangle(1929, 849, 16, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FoodCritic_BlackHairGlasses:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1093, 891, 13, 17), new Rectangle(1149, 891, 12, 17), new Rectangle(1201, 891, 13, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FoodCritic_BlondeMan:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1500, 735, 14, 17), new Rectangle(1560, 735, 13, 17), new Rectangle(1616, 735, 14, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.FoodCritic_GreenDress:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1429, 890, 13, 18), new Rectangle(1485, 890, 13, 18), new Rectangle(1541, 890, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MovieStar_WhiteJumpsuit:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1340, 735, 12, 17), new Rectangle(1392, 735, 13, 17), new Rectangle(1448, 735, 12, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MovieStar_WhiteHeadpiece:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1173, 868, 18, 22), new Rectangle(1249, 868, 16, 21), new Rectangle(1548, 791, 16, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MovieStar_RedFeatherHat:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1093, 868, 19, 22), new Rectangle(1025, 868, 16, 23), new Rectangle(1317, 868, 19, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.MovieStar_RedCap:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1257, 890, 13, 18), new Rectangle(1313, 890, 14, 18), new Rectangle(1373, 890, 13, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Influencer_BlackPinkHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1832, 791, 19, 19), new Rectangle(1912, 791, 18, 19), new Rectangle(1397, 868, 20, 21), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Influencer_BlondeHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1756, 791, 18, 19), new Rectangle(1597, 890, 16, 18), new Rectangle(1665, 890, 17, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Influencer_BlondePinkHair:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1616, 791, 17, 19), new Rectangle(1737, 890, 17, 18), new Rectangle(1688, 791, 16, 19), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_AsianFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1016, 947, 12, 20), new Rectangle(1068, 947, 14, 19), new Rectangle(1128, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_AsianMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1044, 928, 12, 18), new Rectangle(1096, 928, 14, 18), new Rectangle(1156, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_BlackFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1208, 928, 12, 18), new Rectangle(1260, 928, 14, 18), new Rectangle(1320, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_BlackMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1372, 928, 12, 18), new Rectangle(1424, 928, 12, 18), new Rectangle(1476, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_WhiteFemale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1801, 889, 12, 19), new Rectangle(1853, 890, 14, 18), new Rectangle(1913, 890, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.Deliveryman_WhiteMale:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1528, 928, 12, 18), new Rectangle(1580, 928, 14, 18), new Rectangle(1640, 928, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpecialEvent_Artist:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1828, 988, 16, 17), new Rectangle(1896, 988, 14, 17), new Rectangle(1956, 988, 17, 17), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpecialEvent_Scientist:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1024, 1006, 12, 18), new Rectangle(1076, 1006, 12, 18), new Rectangle(1128, 1006, 12, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpecialEvent_GenomeScientist:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(739, 1025, 14, 18), new Rectangle(799, 1025, 15, 18), new Rectangle(863, 1025, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpecialEvent_Banker:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(739, 1025, 14, 18), new Rectangle(799, 1025, 15, 18), new Rectangle(863, 1025, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.SpecialEvent_PipTheZooKeeper:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(739, 1025, 14, 18), new Rectangle(799, 1025, 15, 18), new Rectangle(863, 1025, 14, 18), new Vector2(8f, 13f), 4);
            break;
          case AnimalType.AnimalSpotter:
            EnemyData.enemydata[(int) enemytype] = new EnemyInfoPack(new Rectangle(1684, 947, 12, 20), new Rectangle(1736, 947, 13, 20), new Rectangle(1792, 947, 12, 20), new Vector2(8f, 13f), 4);
            break;
        }
      }
      return EnemyData.enemydata[(int) enemytype];
    }

    internal static string GetBabyNameString(AnimalType enemytype) => "infant";

    internal static string GetEnemyTypeName(AnimalType enemytype, AnimalType headType = AnimalType.None)
    {
      if (headType != AnimalType.None)
        return HybridNames.GetAnimalCombinedName(enemytype, headType);
      switch (enemytype)
      {
        case AnimalType.Rabbit:
          return SEngine.Localization.Localization.GetText(657);
        case AnimalType.Goose:
          return SEngine.Localization.Localization.GetText(659);
        case AnimalType.Capybara:
          return SEngine.Localization.Localization.GetText(614);
        case AnimalType.Pig:
          return SEngine.Localization.Localization.GetText(656);
        case AnimalType.Duck:
          return SEngine.Localization.Localization.GetText(654);
        case AnimalType.Snake:
          return SEngine.Localization.Localization.GetText(641);
        case AnimalType.Badger:
          return SEngine.Localization.Localization.GetText(652);
        case AnimalType.Hyena:
          return SEngine.Localization.Localization.GetText(650);
        case AnimalType.Porcupine:
          return SEngine.Localization.Localization.GetText(638);
        case AnimalType.Bear:
          return SEngine.Localization.Localization.GetText(611);
        case AnimalType.Meerkat:
          return SEngine.Localization.Localization.GetText(646);
        case AnimalType.Horse:
          return SEngine.Localization.Localization.GetText(642);
        case AnimalType.Armadillo:
          return SEngine.Localization.Localization.GetText(624);
        case AnimalType.Donkey:
          return SEngine.Localization.Localization.GetText(655);
        case AnimalType.Cow:
          return SEngine.Localization.Localization.GetText(658);
        case AnimalType.Tapir:
          return SEngine.Localization.Localization.GetText(634);
        case AnimalType.Ostrich:
          return SEngine.Localization.Localization.GetText(633);
        case AnimalType.Tortoise:
          return SEngine.Localization.Localization.GetText(647);
        case AnimalType.Chicken:
          return SEngine.Localization.Localization.GetText(660);
        case AnimalType.Camel:
          return SEngine.Localization.Localization.GetText(630);
        case AnimalType.Penguin:
          return SEngine.Localization.Localization.GetText(619);
        case AnimalType.Antelope:
          return SEngine.Localization.Localization.GetText(637);
        case AnimalType.Panther:
          return SEngine.Localization.Localization.GetText(612);
        case AnimalType.Seal:
          return SEngine.Localization.Localization.GetText(629);
        case AnimalType.Wolf:
          return SEngine.Localization.Localization.GetText(622);
        case AnimalType.Lemur:
          return SEngine.Localization.Localization.GetText(644);
        case AnimalType.Alpaca:
          return SEngine.Localization.Localization.GetText(615);
        case AnimalType.KomodoDragon:
          return SEngine.Localization.Localization.GetText(621);
        case AnimalType.Walrus:
          return SEngine.Localization.Localization.GetText(640);
        case AnimalType.Orangutan:
          return SEngine.Localization.Localization.GetText(625);
        case AnimalType.PolarBear:
          return SEngine.Localization.Localization.GetText(610);
        case AnimalType.Skunk:
          return SEngine.Localization.Localization.GetText(653);
        case AnimalType.Crocodile:
          return SEngine.Localization.Localization.GetText(635);
        case AnimalType.WildBoar:
          return SEngine.Localization.Localization.GetText(649);
        case AnimalType.Peacock:
          return SEngine.Localization.Localization.GetText(645);
        case AnimalType.Platypus:
          return SEngine.Localization.Localization.GetText(632);
        case AnimalType.Deer:
          return SEngine.Localization.Localization.GetText(648);
        case AnimalType.Monkey:
          return SEngine.Localization.Localization.GetText(628);
        case AnimalType.Flamingo:
          return SEngine.Localization.Localization.GetText(639);
        case AnimalType.Gorilla:
          return SEngine.Localization.Localization.GetText(620);
        case AnimalType.Tiger:
          return SEngine.Localization.Localization.GetText(606);
        case AnimalType.Kangaroo:
          return SEngine.Localization.Localization.GetText(627);
        case AnimalType.Beavers:
          return SEngine.Localization.Localization.GetText(643);
        case AnimalType.RedPanda:
          return SEngine.Localization.Localization.GetText(617);
        case AnimalType.Zebra:
          return SEngine.Localization.Localization.GetText(623);
        case AnimalType.Fox:
          return SEngine.Localization.Localization.GetText(626);
        case AnimalType.Raccoon:
          return SEngine.Localization.Localization.GetText(651);
        case AnimalType.Elephant:
          return SEngine.Localization.Localization.GetText(609);
        case AnimalType.Cheetah:
          return SEngine.Localization.Localization.GetText(608);
        case AnimalType.Otter:
          return SEngine.Localization.Localization.GetText(636);
        case AnimalType.Owl:
          return SEngine.Localization.Localization.GetText(631);
        case AnimalType.Rhino:
          return SEngine.Localization.Localization.GetText(616);
        case AnimalType.Panda:
          return SEngine.Localization.Localization.GetText(607);
        case AnimalType.Giraffe:
          return SEngine.Localization.Localization.GetText(613);
        case AnimalType.Hippopotamus:
          return SEngine.Localization.Localization.GetText(618);
        case AnimalType.Lion:
          return SEngine.Localization.Localization.GetText(605);
        case AnimalType.Triclops:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Bunsen:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Tribble:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.EmperorPorcupine:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Hal9000:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Leeloo:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Riddick:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Tardigrade:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Galaxian:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Bill:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Gremlin:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Krampus:
          return SEngine.Localization.Localization.GetText(362);
        case AnimalType.Grinch:
          return SEngine.Localization.Localization.GetText(362);
        default:
          return "NO NAME";
      }
    }

    internal static float GetOsclationSpeed(
      AnimalType enemytype,
      ref float OsilationMin,
      out bool CancelOscilateIfNotArcadeMode)
    {
      CancelOscilateIfNotArcadeMode = false;
      if (enemytype != AnimalType.Horse)
      {
        if (enemytype != AnimalType.Triclops)
          return -1f;
        OsilationMin = 0.3f;
        return 1f;
      }
      OsilationMin = 0.3f;
      return 1f;
    }

    internal static bool[] GetMoveCycle(
      AnimalType enemytype,
      out bool CancelMoveCyclerIfNotArcadeMode)
    {
      bool[] flagArray = new bool[24];
      CancelMoveCyclerIfNotArcadeMode = false;
      return (bool[]) null;
    }

    internal static float GetMovementsSpeed(AnimalType enemytype) => 10f;

    internal static string GetDecription(IntakePerson enemy) => string.Format(SEngine.Localization.Localization.GetText(38).ToUpper(), (object) EnemyData.GetEnemyTypeName(enemy.animaltype)) + "~" + string.Format(SEngine.Localization.Localization.GetText(39).ToUpper(), (object) enemy.Name) + "~" + string.Format(SEngine.Localization.Localization.GetText(40).ToUpper(), (object) EnemyData.getHabitatString(enemy.animaltype));

    internal static string GetCellectinDecription(AnimalType enemytype, Player player)
    {
      bool flag = false;
      if (player.Stats.GetTotalVaiantsFound(enemytype) > 0)
        flag = true;
      else if (enemytype > AnimalType.None && enemytype < AnimalType.SecretAnimalsCount)
      {
        for (int Index = 0; Index < player.inventory.SecretAliensAvailable.Length; ++Index)
        {
          if (player.inventory.SecretAliensAvailable[Index] && player.inventory.GetThisSecretAlien(Index) == enemytype)
          {
            flag = true;
            break;
          }
        }
      }
      if (!flag)
        return SEngine.Localization.Localization.GetText(4).ToUpper();
      string str1 = string.Format(SEngine.Localization.Localization.GetText(38), (object) EnemyData.GetEnemyTypeName(enemytype)) + "~";
      if (EnemyData.GetIsSingle(enemytype))
        str1 += "UNIQUE: ONLY ONE EXISTS~";
      string str2 = str1 + "~" + string.Format(SEngine.Localization.Localization.GetText(40), (object) EnemyData.getHabitatString(enemytype));
      if (LiveStats.reqforpeople == null)
        LiveStats.reqforpeople = new ReqForPeople();
      return str2 + "~";
    }

    internal static string getHabitatString(AnimalType enemy)
    {
      if (LiveStats.reqforpeople.wantsbyperson[(int) enemy].CellRequirement == -1)
        return SEngine.Localization.Localization.GetText(77);
      switch (LiveStats.reqforpeople.wantsbyperson[(int) enemy].CellRequirement)
      {
        case 0:
          return TileData.GetTileStats(TILETYPE.GrassEnclosure).Name;
        case 1:
          return TileData.GetTileStats(TILETYPE.ForestEnclosure).Name;
        case 2:
          return TileData.GetTileStats(TILETYPE.SavannahEnclosure).Name;
        case 3:
          return TileData.GetTileStats(TILETYPE.DesertEnclosure).Name;
        case 4:
          return TileData.GetTileStats(TILETYPE.MountainEnclosure).Name;
        case 5:
          return TileData.GetTileStats(TILETYPE.ArcticEnclosure).Name;
        case 6:
          return TileData.GetTileStats(TILETYPE.TropicalEnclosure).Name;
        case 7:
          return TileData.GetTileStats(TILETYPE.FieldPicketFenceEnclosure).Name;
        case 8:
          return TileData.GetTileStats(TILETYPE.CorruptedGrassEnclosure).Name;
        case 9:
          return TileData.GetTileStats(TILETYPE.CorruptedForestEnclosure).Name;
        default:
          return "NA";
      }
    }

    internal static bool IsThisAGirl(AnimalType enemytype)
    {
      switch (enemytype)
      {
        case AnimalType.BrownHairMaskedLady:
        case AnimalType.BigDarkHairGirl:
        case AnimalType.AliceBlueRibbon:
        case AnimalType.WhiteHatBlueDress:
        case AnimalType.RedBuns:
        case AnimalType.PurpleShirtOldLady:
        case AnimalType.PinkDressFlowerGirl:
        case AnimalType.DarkSkinPinkJacketGirl:
        case AnimalType.GreenBandana:
        case AnimalType.OrangePlaitsGirl:
        case AnimalType.PurpleBandanaLady:
        case AnimalType.PinkShirtOldLady:
        case AnimalType.BlondeTwintails:
        case AnimalType.AfroPinkDress:
        case AnimalType.HighPonytailBlueJumper:
        case AnimalType.AfroOldLady:
        case AnimalType.AfroGreenShirt:
        case AnimalType.PinkHairGirl:
        case AnimalType.BlondeHairPurpleShirtGirl:
        case AnimalType.RedTwintails:
        case AnimalType.LongHairPinkJumper:
        case AnimalType.PinkPatchHair:
        case AnimalType.BrownHairRedShirtGirl:
        case AnimalType.BowlCutGreenShirtGirl:
        case AnimalType.SunglassesBlondeHair:
        case AnimalType.BleachedHairPinkSkirt:
        case AnimalType.RedHairBlueOveralls:
        case AnimalType.PurpleBeanieGirl:
        case AnimalType.YellowHatKid:
        case AnimalType.RedRibbonGirl:
        case AnimalType.BlueHairGirl:
        case AnimalType.OrangeHijabGirl:
        case AnimalType.WhiteHijabGirl:
        case AnimalType.KittyHeadphonesGirl:
        case AnimalType.ChinaGirl:
        case AnimalType.BlondeSidePonytailGirl:
          return true;
        default:
          return false;
      }
    }

    public static Rectangle GetEnemyPortraitIcon(AnimalType enemytype, int Variant)
    {
      if (enemytype < AnimalType.SecretAnimalsCount)
        return new Rectangle(587, 633, 48, 48);
      switch (enemytype - 143)
      {
        case AnimalType.Rabbit:
          return new Rectangle(640, 542, 48, 48);
        case AnimalType.Goose:
        case AnimalType.Capybara:
        case AnimalType.Pig:
        case AnimalType.Horse:
        case AnimalType.Armadillo:
        case AnimalType.Donkey:
        case AnimalType.Cow:
        case AnimalType.Tapir:
label_28:
          return new Rectangle(0, 0, 0, 0);
        case AnimalType.Duck:
          return new Rectangle(607, 493, 48, 48);
        case AnimalType.Snake:
          return new Rectangle(689, 542, 48, 48);
        case AnimalType.Badger:
          return new Rectangle(591, 542, 48, 48);
        case AnimalType.Hyena:
          return new Rectangle(787, 542, 48, 48);
        case AnimalType.Porcupine:
          return new Rectangle(738, 493, 48, 48);
        case AnimalType.Bear:
          return new Rectangle(738, 542, 48, 48);
        case AnimalType.Meerkat:
          return new Rectangle(836, 542, 48, 48);
        case AnimalType.Ostrich:
          return new Rectangle(787, 493, 48, 48);
        case AnimalType.Tortoise:
          return new Rectangle(545, 728, 48, 48);
        case AnimalType.Chicken:
          return new Rectangle(496, 728, 48, 48);
        case AnimalType.Camel:
          return new Rectangle(398, 730, 48, 48);
        case AnimalType.Penguin:
          return new Rectangle(531, 214, 48, 48);
        case AnimalType.Antelope:
          return new Rectangle(580, 214, 48, 48);
        case AnimalType.Panther:
          return new Rectangle(447, 729, 48, 48);
        case AnimalType.Seal:
          return new Rectangle(496, 777, 48, 48);
        case AnimalType.Wolf:
          return new Rectangle(545, 777, 48, 48);
        default:
          if (enemytype == AnimalType.TigerKing)
            return new Rectangle(89, 409, 48, 48);
          switch (enemytype - 458)
          {
            case AnimalType.Rabbit:
              return new Rectangle(594, 777, 48, 48);
            case AnimalType.Goose:
              return new Rectangle(594, 728, 48, 48);
            case AnimalType.Capybara:
              return new Rectangle(284, 488, 48, 48);
            case AnimalType.Pig:
              return new Rectangle(235, 488, 48, 48);
            case AnimalType.Duck:
              return new Rectangle(138, 409, 48, 48);
            default:
              goto label_28;
          }
      }
    }

    internal static AnimationProfile GetEnemyAnimator(AnimalType enemytype)
    {
      EnemyData.animators = new AnimationProfile[70];
      if (EnemyData.animators[(int) enemytype] == null)
      {
        switch (enemytype)
        {
          case AnimalType.Rabbit:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(12f, 0.6f, 0.7f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Goose:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(2f, 0.5f, 0.2f, 0.2f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Capybara:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.4f, 0.4f, 0.4f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Pig:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(10f, 0.8f, 0.5f, 0.2f);
            break;
          case AnimalType.Duck:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(2f, 0.4f, 0.2f, 0.2f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 1.2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Snake:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.8f, 0.3f, 0.5f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 3f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Badger:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.8f, 0.3f, 0.3f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Hyena:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(10f, 0.8f, 0.3f, 0.2f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Porcupine:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.3f, 0.5f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Bear:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 1f, 0.4f, 0.8f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Meerkat:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.4f, 0.3f, 0.3f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Horse:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.3f, 0.2f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Armadillo:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 0.4f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Donkey:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.3f, 0.2f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Cow:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 1f, 0.4f, 0.8f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Tapir:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 1f, 0.4f, 0.6f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Ostrich:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(2f, 0.4f, 0.3f, 0.6f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 2.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Tortoise:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 1.2f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 4f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Chicken:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(1f, 0.4f, 0.2f, 0.2f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 1.2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Camel:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 1f, 0.3f, 0.4f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Penguin:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 0.7f, 0.4f, 0.4f, 0.4f);
            EnemyData.animators[(int) enemytype].LongGap = 1.2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Antelope:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 0.3f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Panther:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 0.8f, 0.3f, 0.3f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Seal:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(9f, 0.7f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Wolf:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.6f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Lemur:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(11f, 0.7f, 0.3f, 0.3f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Alpaca:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 0.8f, 0.3f, 0.2f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.KomodoDragon:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.6f, 0.2f, 1f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 2.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Walrus:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 1f, 0.5f, 1f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 2.2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Orangutan:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 0.5f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.PolarBear:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 1f, 0.4f, 0.8f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Skunk:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.7f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Crocodile:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.6f, 0.2f, 0.8f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 2.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.WildBoar:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 0.7f, 0.2f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Peacock:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(1f, 0.4f, 0.3f, 1f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Platypus:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.8f, 0.3f, 0.3f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Deer:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(6f, 0.8f, 0.3f, 0.3f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Monkey:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(12f, 0.7f, 0.7f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Flamingo:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.5f, 0.3f, 0.6f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 2.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Gorilla:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.3f, 0.5f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Tiger:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 0.4f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Kangaroo:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(12f, 0.9f, 0.5f, 0.3f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Beavers:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.8f, 0.3f, 0.3f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.RedPanda:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.7f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Zebra:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 0.8f, 0.3f, 0.4f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 1f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Fox:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.7f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Raccoon:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(7f, 0.7f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Elephant:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(2f, 0.7f, 0.3f, 1f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Cheetah:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.3f, 0.4f, 0.2f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          case AnimalType.Otter:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.7f, 0.2f, 0.3f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 1.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Rhino:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.7f, 0.3f, 0.9f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Panda:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(5f, 1f, 0.4f, 0.8f, 0.3f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Giraffe:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.5f, 0.3f, 0.6f, 0.0f);
            EnemyData.animators[(int) enemytype].LongGap = 2.5f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 15;
            break;
          case AnimalType.Hippopotamus:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(3f, 0.7f, 0.3f, 0.9f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 2f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 20;
            break;
          case AnimalType.Lion:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(4f, 0.8f, 0.2f, 0.4f, 0.1f);
            EnemyData.animators[(int) enemytype].LongGap = 1.8f;
            EnemyData.animators[(int) enemytype].ProbablityOfLongGap = 10;
            break;
          default:
            EnemyData.animators[(int) enemytype] = new AnimationProfile(10f, 1.2f, 0.5f, 0.2f);
            break;
        }
      }
      return EnemyData.animators[(int) enemytype];
    }

    internal static bool CanThisPersonWaeraHat(AnimalType person)
    {
      if (EnemyData.PeopleWhoCannotWearHats == null)
      {
        EnemyData.PeopleWhoCannotWearHats = new HashSet<AnimalType>();
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.AliceBlueRibbon);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.WhiteHatBlueDress);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.PinkHat);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedBuns);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.GreenBandana);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlueHoodie);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedBandanaBiker);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedHeadbandMan);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.PurpleBandanaLady);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlueCapBoy);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlueHeadphones);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlondeTwintails);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.SailorBoy);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.PinkHairGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedTwintails);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedBeanie);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.StrawHat);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.PurpleBeanieGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlackHatOldMan);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.GreenHatGreenVestMan);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.YellowHatKid);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.RedRibbonGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.OrangeHijabGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.WhiteHijabGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.KittyHeadphonesGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.ChinaGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.DarkHatRedShirtGuy);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlondeSidePonytailGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.AfroGreenShirt);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.AfroPinkDress);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.AfroOldLady);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.HighPonytailBlueJumper);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BigDarkHairGirl);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.DarkSkinPineappleHair);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.SunglassesBlondeHair);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.SpikyHairBlueSuit);
        EnemyData.PeopleWhoCannotWearHats.Add(AnimalType.BlondeSpikyRedShirtMan);
      }
      return !EnemyData.PeopleWhoCannotWearHats.Contains(person);
    }

    internal static bool IsThisAAnimalWelfarePerson(AnimalType persontype)
    {
      if (EnemyData.animalwelfarepeople == null)
      {
        EnemyData.animalwelfarepeople = new HashSet<AnimalType>();
        EnemyData.animalwelfarepeople.Add(AnimalType.AnimalWelfare_BlackHair);
        EnemyData.animalwelfarepeople.Add(AnimalType.AnimalWelfare_BlackPonytail);
        EnemyData.animalwelfarepeople.Add(AnimalType.AnimalWelfare_BlondeHair);
      }
      return EnemyData.animalwelfarepeople.Contains(persontype);
    }

    internal static bool IsThisACriticalChoicePerson(AnimalType persontype)
    {
      if (EnemyData.CriticalChoice == null)
      {
        EnemyData.CriticalChoice = new HashSet<AnimalType>();
        EnemyData.CriticalChoice.Add(AnimalType.SpecialEvent_Scientist);
        EnemyData.CriticalChoice.Add(AnimalType.SpecialEvent_Artist);
        EnemyData.CriticalChoice.Add(AnimalType.SpecialEvent_GenomeScientist);
      }
      return EnemyData.CriticalChoice.Contains(persontype);
    }

    internal static AnimalType GetAnimalWelfarePerson()
    {
      if (EnemyData.animalwelfarepeople == null)
        EnemyData.IsThisAAnimalWelfarePerson(AnimalType.TigerKing);
      return EnemyData.animalwelfarepeople.ElementAt<AnimalType>(Game1.Rnd.Next(0, EnemyData.animalwelfarepeople.Count));
    }

    internal static bool IsThisAFoodHygieneHealthInspectorPerson(AnimalType persontype)
    {
      if (EnemyData.FoodHygieneHealthInspector == null)
      {
        EnemyData.FoodHygieneHealthInspector = new HashSet<AnimalType>();
        EnemyData.FoodHygieneHealthInspector.Add(AnimalType.HealthInspector_Asian);
        EnemyData.FoodHygieneHealthInspector.Add(AnimalType.HealthInspector_BlackLongHair);
        EnemyData.FoodHygieneHealthInspector.Add(AnimalType.HealthInspector_BlondeHair);
      }
      return EnemyData.FoodHygieneHealthInspector.Contains(persontype);
    }

    internal static AnimalType GetFoodHygieneHealthInspector()
    {
      if (EnemyData.FoodHygieneHealthInspector == null)
        EnemyData.IsThisAFoodHygieneHealthInspectorPerson(AnimalType.TigerKing);
      return EnemyData.animalwelfarepeople.ElementAt<AnimalType>(Game1.Rnd.Next(0, EnemyData.animalwelfarepeople.Count));
    }

    internal static bool IsThisAnInfluencer(AnimalType persontype)
    {
      if (EnemyData.Influencer == null)
      {
        EnemyData.Influencer = new HashSet<AnimalType>();
        EnemyData.Influencer.Add(AnimalType.Influencer_BlackPinkHair);
        EnemyData.Influencer.Add(AnimalType.Influencer_BlondeHair);
        EnemyData.Influencer.Add(AnimalType.Influencer_BlondePinkHair);
      }
      return EnemyData.Influencer.Contains(persontype);
    }

    internal static bool IsThisASafetyInspector(AnimalType persontype)
    {
      if (EnemyData.SafetyInspector == null)
      {
        EnemyData.SafetyInspector = new HashSet<AnimalType>();
        EnemyData.SafetyInspector.Add(AnimalType.SafetyInspector_AsianGirl);
        EnemyData.SafetyInspector.Add(AnimalType.SafetyInspector_Black);
        EnemyData.SafetyInspector.Add(AnimalType.SafetyInspector_White);
      }
      return EnemyData.SafetyInspector.Contains(persontype);
    }

    internal static bool IsThisATeacher(AnimalType persontype)
    {
      if (EnemyData.Teachers == null)
      {
        EnemyData.Teachers = new HashSet<AnimalType>();
        EnemyData.Teachers.Add(AnimalType.Teacher_OrangeBun);
        EnemyData.Teachers.Add(AnimalType.Teacher_Glasses);
        EnemyData.Teachers.Add(AnimalType.Teacher_WhiteHair);
      }
      return EnemyData.Teachers.Contains(persontype);
    }

    internal static bool IsThisAStudent(AnimalType persontype)
    {
      if (EnemyData.Students == null)
      {
        EnemyData.Students = new HashSet<AnimalType>();
        EnemyData.Students.Add(AnimalType.Student_FemaleAsian);
        EnemyData.Students.Add(AnimalType.Student_FemaleDark);
        EnemyData.Students.Add(AnimalType.Student_FemaleWhite);
        EnemyData.Students.Add(AnimalType.Student_MaleAsian);
        EnemyData.Students.Add(AnimalType.Student_MaleDark);
        EnemyData.Students.Add(AnimalType.Student_MaleWhite);
      }
      return EnemyData.Students.Contains(persontype);
    }

    internal static bool IsThisAnAnimalRightsActivists(AnimalType persontype)
    {
      if (EnemyData.AnimalRightsActivists == null)
      {
        EnemyData.AnimalRightsActivists = new HashSet<AnimalType>();
        EnemyData.AnimalRightsActivists.Add(AnimalType.AnimalRightsActivist_BlueShirt);
        EnemyData.AnimalRightsActivists.Add(AnimalType.AnimalRightsActivist_GreenShirt);
        EnemyData.AnimalRightsActivists.Add(AnimalType.AnimalRightsActivist_RedShirt);
      }
      return EnemyData.AnimalRightsActivists.Contains(persontype);
    }

    internal static bool IsThisAFootballPlayer(AnimalType persontype)
    {
      if (EnemyData.FootballPlayer == null)
      {
        EnemyData.FootballPlayer = new HashSet<AnimalType>();
        EnemyData.FootballPlayer.Add(AnimalType.Footballer_CombedBackHair);
        EnemyData.FootballPlayer.Add(AnimalType.FootballCaptain);
        EnemyData.FootballPlayer.Add(AnimalType.Footballer_Goalkeep);
        EnemyData.FootballPlayer.Add(AnimalType.Footballer_SpikyHair);
      }
      return EnemyData.FootballPlayer.Contains(persontype);
    }

    internal static bool IsThisABiker(AnimalType persontype)
    {
      if (EnemyData.Bikers == null)
      {
        EnemyData.Bikers = new HashSet<AnimalType>();
        EnemyData.Bikers.Add(AnimalType.Biker1);
        EnemyData.Bikers.Add(AnimalType.Biker2);
        EnemyData.Bikers.Add(AnimalType.Biker3);
        EnemyData.Bikers.Add(AnimalType.Biker4);
        EnemyData.Bikers.Add(AnimalType.Biker5);
        EnemyData.Bikers.Add(AnimalType.Biker6);
      }
      return EnemyData.Bikers.Contains(persontype);
    }

    internal static bool IsThisAFoodCritic(AnimalType persontype)
    {
      if (EnemyData.FoodCritic == null)
      {
        EnemyData.FoodCritic = new HashSet<AnimalType>();
        EnemyData.FoodCritic.Add(AnimalType.FoodCritic_BlackHairGlasses);
        EnemyData.FoodCritic.Add(AnimalType.FoodCritic_BlondeMan);
        EnemyData.FoodCritic.Add(AnimalType.FoodCritic_GreenDress);
      }
      return EnemyData.AnimalRightsActivists.Contains(persontype);
    }

    internal static bool IsThisAManInBlack(AnimalType persontype)
    {
      if (EnemyData.MenInBlack == null)
      {
        EnemyData.MenInBlack = new HashSet<AnimalType>();
        EnemyData.MenInBlack.Add(AnimalType.AnimalRightsActivist_BlueShirt);
        EnemyData.MenInBlack.Add(AnimalType.AnimalRightsActivist_GreenShirt);
        EnemyData.MenInBlack.Add(AnimalType.AnimalRightsActivist_RedShirt);
      }
      return EnemyData.MenInBlack.Contains(persontype);
    }

    internal static bool IsThisAHunter(AnimalType persontype)
    {
      if (EnemyData.Hunters == null)
      {
        EnemyData.Hunters = new HashSet<AnimalType>();
        EnemyData.Hunters.Add(AnimalType.AnimalRightsActivist_BlueShirt);
        EnemyData.Hunters.Add(AnimalType.AnimalRightsActivist_GreenShirt);
        EnemyData.Hunters.Add(AnimalType.AnimalRightsActivist_RedShirt);
      }
      return EnemyData.Hunters.Contains(persontype);
    }

    internal static bool IsThisAChurchOfPureLife(AnimalType persontype)
    {
      if (EnemyData.PureLife == null)
      {
        EnemyData.PureLife = new HashSet<AnimalType>();
        EnemyData.PureLife.Add(AnimalType.ChurchOfPureLife_Asian);
        EnemyData.PureLife.Add(AnimalType.ChurchOfPureLife_Black);
        EnemyData.PureLife.Add(AnimalType.ChurchOfPureLife_White);
      }
      return EnemyData.PureLife.Contains(persontype);
    }

    internal static bool IsThisAMovieStar(AnimalType persontype)
    {
      if (EnemyData.MovieStars == null)
      {
        EnemyData.MovieStars = new HashSet<AnimalType>();
        EnemyData.MovieStars.Add(AnimalType.MovieStar_RedCap);
        EnemyData.MovieStars.Add(AnimalType.MovieStar_RedFeatherHat);
        EnemyData.MovieStars.Add(AnimalType.MovieStar_WhiteHeadpiece);
        EnemyData.MovieStars.Add(AnimalType.MovieStar_WhiteJumpsuit);
      }
      return EnemyData.MovieStars.Contains(persontype);
    }

    internal static bool IsThisABlackMarket(AnimalType persontype)
    {
      if (EnemyData.BlackMarket == null)
      {
        EnemyData.BlackMarket = new HashSet<AnimalType>();
        EnemyData.BlackMarket.Add(AnimalType.BlackMarketDealer);
        EnemyData.BlackMarket.Add(AnimalType.TigerKing);
      }
      return EnemyData.BlackMarket.Contains(persontype);
    }

    internal static AnimalType GetStringToEnemyType(string Text)
    {
      switch (Text)
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
          throw new Exception("osdf");
      }
    }
  }
}
