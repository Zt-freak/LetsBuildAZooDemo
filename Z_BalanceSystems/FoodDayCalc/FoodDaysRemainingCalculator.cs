// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.FoodDayCalc.FoodDaysRemainingCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BalanceSystems.FoodDayCalc
{
  internal class FoodDaysRemainingCalculator
  {
    internal static float[] DaysWillLast;
    internal static float[] FoodByTypeUsedPerDay;

    internal static void CalculateFoodDaysRemainingCalculator(Player player)
    {
      FoodDaysRemainingCalculator.FoodByTypeUsedPerDay = new float[88];
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo.Count; ++index2)
        {
          player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo[index2].EatsThis = new List<FoodTemp>();
          for (int index3 = 0; index3 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.FoodForAnimals.FoodSet.Count; ++index3)
          {
            if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.FoodForAnimals.FoodSet[index3].animal == player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo[index2].animaltype)
              player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.FoodForAnimals.FoodSet[index3].SetConsumptionPerDay(ref FoodDaysRemainingCalculator.FoodByTypeUsedPerDay, player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo[index2]);
          }
        }
      }
      FoodDaysRemainingCalculator.DaysWillLast = new float[88];
      for (int index = 0; index < FoodDaysRemainingCalculator.FoodByTypeUsedPerDay.Length; ++index)
      {
        float totalStockOfThis = player.storerooms.GetTotalStockOfThis((AnimalFoodType) index);
        if ((double) totalStockOfThis > 0.0 && (double) FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[index] > 0.0)
          FoodDaysRemainingCalculator.DaysWillLast[index] = totalStockOfThis / FoodDaysRemainingCalculator.FoodByTypeUsedPerDay[index];
      }
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo.Count; ++index2)
          player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo[index2].CalculateDaysOfFood(FoodDaysRemainingCalculator.DaysWillLast);
      }
    }
  }
}
