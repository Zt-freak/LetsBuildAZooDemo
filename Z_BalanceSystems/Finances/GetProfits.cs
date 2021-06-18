// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Finances.GetProfits
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BalanceSystems.Finances
{
  internal class GetProfits
  {
    internal static int ProfitToday;
    internal static float SalaryPerDay;
    internal static float FoodCostPerDay;

    internal static int GetProfitsTodayRealTime_Est()
    {
      double profitToday = (double) Player.financialrecords.GetProfitToday();
      float PercentOfDayOpen = 0.0f;
      double payoutSoFarToday = (double) GetProfits.GetSalaryPayoutSoFarToday(ref PercentOfDayOpen);
      return (int) (profitToday - payoutSoFarToday - (double) GetProfits.GetFoodCostsSoFarToday(PercentOfDayOpen));
    }

    internal static void CalculatedRealTimeProfitBase_DayStart(Player player)
    {
      float num = 0.0f;
      GetProfits.FoodCostPerDay = 0.0f;
      for (int index = 0; index < player.employees.employees.Count; ++index)
        num += (float) player.employees.employees[index].Salary;
      GetProfits.SalaryPerDay = num / 7f;
      FoodDaysRemainingCalculator.CalculateFoodDaysRemainingCalculator(player);
      for (int index = 0; index < FoodDaysRemainingCalculator.FoodByTypeUsedPerDay.Length; ++index)
      {
        if ((double) FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[index] > 0.0)
          GetProfits.FoodCostPerDay += (float) AnimalFoodData.GetAnimalFoodInfo((AnimalFoodType) index).Cost * FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[index];
      }
    }

    private static float GetSalaryPayoutSoFarToday(ref float PercentOfDayOpen)
    {
      PercentOfDayOpen = Z_GameFlags.DayTimer - Z_GameFlags.ZooOpenTime_InSeconds;
      PercentOfDayOpen /= Z_GameFlags.SecondsZooOpenPerDay;
      if ((double) PercentOfDayOpen > 1.0)
        PercentOfDayOpen = 1f;
      return GetProfits.SalaryPerDay * PercentOfDayOpen;
    }

    private static float GetFoodCostsSoFarToday(float PercentOfDayOpen) => GetProfits.FoodCostPerDay * PercentOfDayOpen;
  }
}
