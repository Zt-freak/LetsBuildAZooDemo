// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Morality.MoralityUnlocksData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Morality
{
  internal class MoralityUnlocksData
  {
    private static List<MoralityUnlock> GoodUnlocks_Ordered;
    private static List<MoralityUnlock> EvilUnlocks_Ordered;
    private static HashSet<TILETYPE> moralityBuildings;

    public static bool PlayerHasEnoughPointsToUseThis(MoralityUnlock moralityUnlock, Player player)
    {
      double moralityScore = (double) player.livestats.MoralityScore;
      TILETYPE tileTypeIfBuilding = MoralityUnlocksData.GetMoralityUnlockToTileType_IfBuilding(moralityUnlock);
      return tileTypeIfBuilding != TILETYPE.None ? MoralityUnlocksData.PlayerHasEnoughPointsToUseThis(tileTypeIfBuilding, player) : throw new NotImplementedException("not done");
    }

    public static bool PlayerHasEnoughPointsToUseThis(TILETYPE tileType, Player player)
    {
      if (Z_DebugFlags.DisableMoralityBlocks)
        return true;
      float moralityScore = player.livestats.MoralityScore;
      int toUseThisBuilding = MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(tileType);
      return toUseThisBuilding < 0 ? (double) moralityScore < (double) toUseThisBuilding : (double) moralityScore > (double) toUseThisBuilding;
    }

    public static int GetPointsNeededToUnlock(MoralityUnlock moralityUnlock)
    {
      TILETYPE tileTypeIfBuilding = MoralityUnlocksData.GetMoralityUnlockToTileType_IfBuilding(moralityUnlock);
      return tileTypeIfBuilding != TILETYPE.None ? MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding(tileTypeIfBuilding) : throw new NotImplementedException("Not coded with actions yet");
    }

    public static bool IsAMoralityBuilding(TILETYPE tileType)
    {
      if (MoralityUnlocksData.moralityBuildings == null)
      {
        MoralityUnlocksData.moralityBuildings = new HashSet<TILETYPE>();
        for (int index = 0; index < 743; ++index)
        {
          if (MoralityUnlocksData.GetNumberOfPointsNeededToUseThisBuilding((TILETYPE) index) != 0)
            MoralityUnlocksData.moralityBuildings.Add((TILETYPE) index);
        }
      }
      return MoralityUnlocksData.moralityBuildings.Contains(tileType);
    }

    public static int GetNumberOfPointsNeededToUseThisBuilding(TILETYPE building)
    {
      switch (building)
      {
        case TILETYPE.GlueFactory:
          return -20;
        case TILETYPE.BuffaloWingFactory:
          return -40;
        case TILETYPE.BaconFactory:
          return -50;
        case TILETYPE.WindTurbine:
          return 30;
        case TILETYPE.SolarPanel:
          return 30;
        case TILETYPE.RecyclingBin:
          return 10;
        case TILETYPE.AnimalColosseum:
          return -60;
        case TILETYPE.CrocHandbagFactory:
          return -50;
        case TILETYPE.SnakeSkinFactory:
          return -50;
        case TILETYPE.Farmhouse:
          return 20;
        default:
          return 0;
      }
    }

    public static TILETYPE GetMoralityUnlockToTileType_IfBuilding(MoralityUnlock unlockType)
    {
      switch (unlockType)
      {
        case MoralityUnlock.FarmBuilding:
          return TILETYPE.Farm;
        case MoralityUnlock.WindTurbine:
          return TILETYPE.WindTurbine;
        case MoralityUnlock.SolarPower:
          return TILETYPE.SolarPanel;
        case MoralityUnlock.GlueFactory:
          return TILETYPE.GlueFactory;
        case MoralityUnlock.BuffaloWingFactory:
          return TILETYPE.BuffaloWingFactory;
        case MoralityUnlock.RecyclingBin:
          return TILETYPE.RecyclingBin;
        case MoralityUnlock.AnimalFightAttraction:
          return TILETYPE.AnimalColosseum;
        default:
          return TILETYPE.None;
      }
    }

    public static bool PlayerHasResearchUnlockedForThis(MoralityUnlock unlockType, Player player)
    {
      TILETYPE tileTypeIfBuilding = MoralityUnlocksData.GetMoralityUnlockToTileType_IfBuilding(unlockType);
      return tileTypeIfBuilding == TILETYPE.None || player.Stats.research.BuildingsResearched.Contains(tileTypeIfBuilding);
    }

    public static Rectangle GetIconRectangle(MoralityUnlock moralityUnlock)
    {
      Rectangle rectangle = Rectangle.Empty;
      switch (moralityUnlock)
      {
        case MoralityUnlock.WindTurbine:
          rectangle = new Rectangle(907, 789, 32, 32);
          break;
        case MoralityUnlock.SolarPower:
          rectangle = new Rectangle(874, 789, 32, 32);
          break;
        case MoralityUnlock.GlueFactory:
          rectangle = new Rectangle(66, 122, 32, 32);
          break;
        case MoralityUnlock.BuffaloWingFactory:
          rectangle = new Rectangle(66, 89, 32, 32);
          break;
        case MoralityUnlock.RecyclingBin:
          rectangle = new Rectangle(973, 822, 32, 32);
          break;
      }
      return rectangle;
    }

    public static string GetMoralityUnlockToNameString(MoralityUnlock moralityUnlock)
    {
      switch (moralityUnlock)
      {
        case MoralityUnlock.FarmBuilding:
          return "Farm Building";
        case MoralityUnlock.WindTurbine:
          return "Wind Turbine";
        case MoralityUnlock.SolarPower:
          return "Solar Power";
        case MoralityUnlock.GlueFactory:
          return "Glue Factory";
        case MoralityUnlock.BuffaloWingFactory:
          return "Buffalo Wing Factory";
        case MoralityUnlock.RecyclingBin:
          return "Recycling Bin";
        case MoralityUnlock.AnimalFightAttraction:
          return "Animal Colosseum";
        default:
          return "NA_" + moralityUnlock.ToString();
      }
    }

    public static List<MoralityUnlock> GetAllUnlocksForThisMoralityAlignment_Ordered(
      bool IsGoodNotEvil)
    {
      if (IsGoodNotEvil && MoralityUnlocksData.GoodUnlocks_Ordered != null)
        return MoralityUnlocksData.GoodUnlocks_Ordered;
      if (!IsGoodNotEvil && MoralityUnlocksData.EvilUnlocks_Ordered != null)
        return MoralityUnlocksData.EvilUnlocks_Ordered;
      Dictionary<MoralityUnlock, int> source = new Dictionary<MoralityUnlock, int>();
      for (int index = 0; index < 7; ++index)
      {
        if (!Z_DebugFlags.IsBetaVersion || index != 6)
        {
          int pointsNeededToUnlock = MoralityUnlocksData.GetPointsNeededToUnlock((MoralityUnlock) index);
          if (IsGoodNotEvil && pointsNeededToUnlock > 0 || !IsGoodNotEvil && pointsNeededToUnlock < 0)
          {
            int num = Math.Abs(pointsNeededToUnlock);
            source.Add((MoralityUnlock) index, num);
          }
        }
      }
      List<MoralityUnlock> list = source.OrderBy<KeyValuePair<MoralityUnlock, int>, int>((Func<KeyValuePair<MoralityUnlock, int>, int>) (x => x.Value)).ToDictionary<KeyValuePair<MoralityUnlock, int>, MoralityUnlock, int>((Func<KeyValuePair<MoralityUnlock, int>, MoralityUnlock>) (x => x.Key), (Func<KeyValuePair<MoralityUnlock, int>, int>) (x => x.Value)).Keys.ToList<MoralityUnlock>();
      if (IsGoodNotEvil)
        MoralityUnlocksData.GoodUnlocks_Ordered = list;
      else
        MoralityUnlocksData.EvilUnlocks_Ordered = list;
      return list;
    }
  }
}
