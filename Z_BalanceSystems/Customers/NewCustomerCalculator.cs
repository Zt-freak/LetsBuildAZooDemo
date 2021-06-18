// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Customers.NewCustomerCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_BalanceSystems.Customers.NewCustomers;

namespace TinyZoo.Z_BalanceSystems.Customers
{
  internal class NewCustomerCalculator
  {
    private static BusPeopleAndRoutes[] buspeopleandroutes;
    internal static int TotalPeopleWhoLeftBecauseOfProtestors;

    internal static void Calc_NewCustomers(Player player, float Popularity)
    {
      NewCustomerCalculator.buspeopleandroutes = new BusPeopleAndRoutes[10];
      Player.financialrecords.daystats[Player.financialrecords.daystats.Count - 1].PeopleWhoWantedToCome = 0;
      int _BasePeopleValue = 3 + (int) ((double) Popularity / 200.0 * 295.0);
      for (int index = 0; index < NewCustomerCalculator.buspeopleandroutes.Length; ++index)
        NewCustomerCalculator.buspeopleandroutes[index] = new BusPeopleAndRoutes(_BasePeopleValue, (BUSROUTE) index, player.busroutes.GetBussesByRoute((BUSROUTE) index).Count > 0);
      double num = (double) Popularity / 200.0;
    }

    internal static void SetUpVIP(Player player)
    {
      NewCustomerCalculator.TotalPeopleWhoLeftBecauseOfProtestors = 0;
      BusVIPs.CreateVIPsOnStarteDay(player);
    }

    internal static void FinalizePropleWaitingRecordsAtEndOfDay()
    {
      if (NewCustomerCalculator.buspeopleandroutes == null)
        return;
      for (int index = 0; index < NewCustomerCalculator.buspeopleandroutes.Length; ++index)
        NewCustomerCalculator.buspeopleandroutes[index].FinalizePropleWaitingRecordsAtEndOfDay();
    }

    internal static int GetPeopleAtThisBusStop(
      BUSROUTE busroute,
      int MaximumPeopleOnThisBus,
      bool IsLastBus)
    {
      if (NewCustomerCalculator.buspeopleandroutes == null)
        return 0;
      int peopleForThisBus = NewCustomerCalculator.buspeopleandroutes[(int) busroute].GetPeopleForThisBus(MaximumPeopleOnThisBus, IsLastBus);
      if (BusVIPs.HasFootBallTeam && NewCustomerCalculator.buspeopleandroutes[(int) busroute].WasFootballTeam)
      {
        for (int index = 0; index < BusVIPs.FootballTeam.Count; ++index)
          Z_GameFlags.SpecialPeopleOnBus.Add(BusVIPs.FootballTeam[index]);
      }
      else if (BusVIPs.VIPsToday.Count > 0)
      {
        int num = 0;
        for (int index = 0; index < peopleForThisBus; ++index)
        {
          if (index < BusVIPs.VIPsToday.Count)
          {
            ++num;
            Z_GameFlags.SpecialPeopleOnBus.Add(BusVIPs.VIPsToday[index]);
          }
        }
        for (int index = num - 1; index > -1; --index)
          BusVIPs.VIPsToday.RemoveAt(index);
      }
      return peopleForThisBus;
    }
  }
}
