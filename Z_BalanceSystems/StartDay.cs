// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.StartDay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.Customers;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_BalanceSystems.Finances;
using TinyZoo.Z_BalanceSystems.ProductionLines;
using TinyZoo.Z_Notification;
using TinyZoo.Z_Quests.Advice;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_BalanceSystems
{
  internal class StartDay
  {
    internal static void StartNewDay(Player player)
    {
      Z_NotificationManager.StartNewDay();
      AnimalTicketValue.MustUpdateTicketCost = true;
      CurrentDeadAnimals.StartNewDay();
      Z_DebugFlags.TempBlockMorePeopleSpawning = false;
      OverWorldManager.heatmapmanager.DoubleCheckAnimalPrivacySetUp(player);
      player.sponsorships.StartNewDay(player);
      player.heroquestprogress.StartNewDay();
      if (Player.financialrecords.GetDaysPassed() > 3L && !player.prisonlayout.cellblockcontainer.HasAnArchitectureBuilding())
        Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.F_BuildArchitect), player);
      for (int index = 0; index < player.Stats.ActiveDiseases.Count; ++index)
        player.Stats.ActiveDiseases[index].StartNewDay(player);
      player.shelterstocks.StartNewDay(player);
      player.animalsonorder.StartNewDay();
      OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
      NewDay_ByPen.CalcAnimalsNonPen(player);
      player.breeds.StartNewDay(player);
      player.crisprBreeds.StartNewDay(player);
      player.farms.StartNewQuarterDay(player, true);
      NewDay_ByPen.CalcAnimals(player);
      EmployeeCheck.CheckEmployees(player);
      OverWorldManager.crowd.StartNewDay();
      player.z_research.StartNewDay(player);
      player.storerooms.StartNewDay();
      NewCustomerCalculator.SetUpVIP(player);
      GetAdvice.GetCurrentAdvice(player);
      QuestScrubber.ScrubQuests(player);
      player.animalquarantine.StartNewDay(player);
      player.animalincineration.StartNewDay();
      ProductionLineCalc.StartNewDay(player);
      Player.garbage.StartDay();
      GetProfits.CalculatedRealTimeProfitBase_DayStart(player);
      Z_NotificationManager.SetRecountAll();
      OverWorldManager.overworldenvironment.animalsinpens.StartNewDay();
    }

    internal static void DoQuarterDayUpdate(Player player)
    {
    }
  }
}
