// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.LifeExpectancy.LifeExpectancy_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BalanceSystems.Animals.LifeExpectancy
{
  internal class LifeExpectancy_Calculator
  {
    internal static void SimpleCalculate_LifeExpectancy(
      PrisonerInfo animal,
      float QualityOfLife,
      Player player)
    {
      if (animal.IsDead)
      {
        ++animal.DaysSinceDeath;
      }
      else
      {
        if (animal.IsDead || !FeatureFlags.FoodAndDeathEnabled)
          return;
        ++animal.Age;
        if (animal.IsFertile && (double) animal.Age > (double) animal.LifeExpetancy * (double) animal.Fertility)
          animal.IsFertile = false;
        if ((double) animal.Age <= (double) animal.LifeExpetancy)
          return;
        animal.WillDieOfOldAge = true;
        LifeExpectancy_Calculator.TryAndKill(animal, CauseOfDeath.OldAge, false);
      }
    }

    internal static void Calculate_Water(PrisonerInfo animal, ref float Water)
    {
      if ((double) Water <= 0.0)
        return;
      Water -= AnimalFoodData.GetFoodCollection(animal.intakeperson.animaltype).GetWaterRequirement();
    }

    internal static void Calculate_LifeExpectancy(
      PrisonerInfo animal,
      float QualityOfLife,
      Player player,
      ref float Water,
      float PrisonCleanliness)
    {
      if (animal.IsDead)
      {
        ++animal.DaysSinceDeath;
      }
      else
      {
        if (animal.IsDead || !FeatureFlags.FoodAndDeathEnabled)
          return;
        ++animal.LastFed;
        --animal.LifeExpetancy;
        if ((double) QualityOfLife >= 1.0)
          animal.LifeExpetancy += 0.85f;
        else if ((double) QualityOfLife > 0.800000011920929)
          animal.LifeExpetancy += (float) (((double) QualityOfLife - 0.200000002980232) * 25.0);
        else
          animal.LifeExpetancy -= (float) ((1.0 - (double) QualityOfLife / 0.800000011920929) * 5.0);
        if (animal.DaysWithoutFood > 2)
          animal.LifeExpetancy -= (float) (((double) animal.Hunger - 2.0) * 0.300000011920929);
        if ((double) animal.EnrichmentValue < 1.0)
          animal.LifeExpetancy -= (float) ((1.0 - (double) animal.EnrichmentValue) * 0.300000011920929);
        if ((double) Water > 0.0)
        {
          Water -= AnimalFoodData.GetFoodCollection(animal.intakeperson.animaltype).GetWaterRequirement();
          if ((double) Water >= 0.0)
          {
            animal.DaysWithoutWater = 0;
            animal.HydrationValue += 0.2f;
            if ((double) animal.HydrationValue > 1.0)
              animal.HydrationValue = 1f;
          }
        }
        if ((double) PrisonCleanliness >= 0.899999976158142)
          animal.HygieneValue += (float) (((double) PrisonCleanliness - 0.899999976158142) * 0.5);
        else
          animal.HygieneValue = (float) ((0.899999976158142 - (double) PrisonCleanliness) * 0.200000002980232);
        if ((double) Water <= 0.0)
        {
          ++animal.DaysWithoutWater;
          animal.HydrationValue -= 0.2f;
          if (animal.DaysWithoutWater > 5)
          {
            animal.WillDieOfThirst = Game1.Rnd.Next(0, 15) < animal.DaysWithoutWater;
            if (Z_DebugFlags.IsBetaVersion)
              animal.WillDieOfThirst = false;
            if (animal.WillDieOfThirst)
              LifeExpectancy_Calculator.TryAndKill(animal, CauseOfDeath.Thirst, true);
          }
        }
        if (animal.DaysWithoutFood > 5)
          animal.WillDieOfHunger = (double) Game1.Rnd.Next(0, 25) < (double) animal.Hunger;
        if (Z_DebugFlags.IsBetaVersion)
          animal.WillDieOfHunger = false;
        if (animal.WillDieOfHunger)
          LifeExpectancy_Calculator.TryAndKill(animal, CauseOfDeath.Hunger, true);
        ++animal.DaysWithoutFood;
        if (animal.DaysWithoutFood > 1)
        {
          animal.WeightValue -= 0.05f;
          animal.NutritionValue -= 0.06f;
        }
        animal.PoopNeed += 0.05f;
        ++NewDay_ByPen.Day_TotalHungryAnimalValue;
        if (animal.GetIsABaby())
          animal.Hunger += 0.5f;
        else
          ++animal.Hunger;
        ++animal.Age;
        if (animal.IsFertile && (double) animal.Age > (double) animal.LifeExpetancy * (double) animal.Fertility)
          animal.IsFertile = false;
        if ((double) animal.Age <= (double) animal.LifeExpetancy)
          return;
        animal.WillDieOfOldAge = true;
        LifeExpectancy_Calculator.TryAndKill(animal, CauseOfDeath.OldAge, true);
      }
    }

    internal static void TryAndKill(
      PrisonerInfo animal,
      CauseOfDeath causeofdeath,
      bool IsPenAnimal)
    {
      animal.causeofdeath = causeofdeath;
      if (IsPenAnimal)
        LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.AnimalDeath));
      else
        LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.NonPenAnimalDeath));
      if (LiveStats.DeathUIDs == null)
        LiveStats.DeathUIDs = new List<TheDead>();
      bool flag = false;
      for (int index = 0; index < LiveStats.DeathUIDs.Count; ++index)
      {
        if (LiveStats.DeathUIDs[index].UID == animal.intakeperson.UID)
          flag = true;
      }
      if (flag)
        return;
      LiveStats.DeathUIDs.Add(new TheDead(animal.intakeperson.UID, causeofdeath));
    }
  }
}
