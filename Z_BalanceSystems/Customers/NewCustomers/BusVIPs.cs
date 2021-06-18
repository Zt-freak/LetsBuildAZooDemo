// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Customers.NewCustomers.BusVIPs
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_BalanceSystems.Customers.NewCustomers
{
  internal class BusVIPs
  {
    internal static List<VIP_Entry> VIPsToday;
    internal static bool HasFootBallTeam;
    internal static List<VIP_Entry> FootballTeam;

    internal static void CreateVIPsOnStarteDay(Player player)
    {
      BusVIPs.VIPsToday = new List<VIP_Entry>();
      BusVIPs.CalculateFootballTeam(player);
      BusVIPs.CalculateProtestors();
      BusVIPs.CalculateBlackMarket();
      BusVIPs.CalculateAnimalWelfare(player);
      BusVIPs.CalculateAnimalPainter(player);
      BusVIPs.CalculateGenetecist();
    }

    internal static void AddVIPonTheFly(VIP_Entry entry) => BusVIPs.VIPsToday.Add(entry);

    private static void CalculateFootballTeam(Player player)
    {
      BusVIPs.HasFootBallTeam = false;
      if ((int) Player.financialrecords.GetDaysPassed() % 7 == 6 && player.heroquestprogress.ProgressArray[2].GetCompletetedQuests() > 0 && player.busroutes.GetBussesByRoute(BUSROUTE.Route01, BUSTYPE.StartingBus_01, true).Count > 0)
        BusVIPs.HasFootBallTeam = true;
      if (!BusVIPs.HasFootBallTeam)
        return;
      BusVIPs.FootballTeam = new List<VIP_Entry>();
      BusVIPs.FootballTeam.Add(new VIP_Entry(AnimalType.FootballCaptain, CustomerType.Footballer));
      for (int index = 0; index < 9; ++index)
        BusVIPs.FootballTeam.Add(new VIP_Entry((AnimalType) Game1.Rnd.Next(409, 412), CustomerType.Footballer));
      BusVIPs.FootballTeam.Add(new VIP_Entry(AnimalType.Footballer_Goalkeep, CustomerType.Footballer));
    }

    private static void CalculateProtestors()
    {
      if (NewDay_ByPen.Day_CollectiveCorpses < 1 || NewDay_ByPen.Day_CollectiveCorpseAge <= 2)
        return;
      for (int index = 0; index < 3; ++index)
      {
        if (!Z_DebugFlags.IsBetaVersion)
          BusVIPs.VIPsToday.Add(new VIP_Entry((AnimalType) Game1.Rnd.Next(391, 397), CustomerType.Protestor));
      }
    }

    private static void CalculateBlackMarket()
    {
      if (Player.financialrecords.GetDaysPassed() % 8L == 3L)
      {
        BusVIPs.VIPsToday.Add(new VIP_Entry(AnimalType.TigerKing, CustomerType.Count));
      }
      else
      {
        if (Player.financialrecords.GetDaysPassed() % 11L != 6L)
          return;
        BusVIPs.VIPsToday.Add(new VIP_Entry(AnimalType.TigerKing, CustomerType.Count));
      }
    }

    private static void CalculateAnimalWelfare(Player player)
    {
      if (player.Stats.TutorialsComplete[30] || Player.financialrecords.GetDaysPassed() <= 0L)
        return;
      BusVIPs.VIPsToday.Add(new VIP_Entry(EnemyData.GetAnimalWelfarePerson(), CustomerType.AnimalWelfareOfficer));
    }

    private static void CalculateAnimalPainter(Player player)
    {
      if (Player.criticalchoices.ChoiceIndexes[1] != -1 || player.Stats.variantsfound.GetTotalOfThisVariantFound(AnimalType.Horse, -1) <= 0 || player.prisonlayout.GetTotalOfThisAnimal(AnimalType.Horse) <= 0)
        return;
      BusVIPs.VIPsToday.Add(new VIP_Entry(AnimalType.SpecialEvent_Artist, CustomerType.AnimalArtist));
    }

    private static void CalculateGenetecist()
    {
      if (Player.financialrecords.GetDaysPassed() != 7L)
        return;
      BusVIPs.VIPsToday.Add(new VIP_Entry(AnimalType.SpecialEvent_GenomeScientist, CustomerType.GenomeBetaGiver));
    }
  }
}
