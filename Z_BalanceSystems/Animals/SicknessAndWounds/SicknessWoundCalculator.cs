// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.SicknessAndWounds.SicknessWoundCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_BalanceSystems.Animals.LifeExpectancy;
using TinyZoo.Z_Diseases;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_BalanceSystems.Animals.SicknessAndWounds
{
  internal class SicknessWoundCalculator
  {
    internal static void Calculate_SicknessOnQuarterUpdate(
      PrisonerInfo animal,
      Player player,
      PrisonZone prisonzone)
    {
      if (!animal.GetIsSick())
        return;
      Disease thisDisease = player.Stats.GetThisDisease(animal.SicknessUID);
      if (animal.SicknessHasBeeDiagnosed && player.Stats.HasCureForThisDisease(animal.SicknessUID) && Game1.Rnd.Next(0, 3) == 0)
      {
        Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_CuredAnimal, animal.intakeperson.UID)
        {
          Animal = animal.intakeperson.animaltype
        }, player);
        animal.Cure();
      }
      else if (animal.SicknessGestationRemaining_quarterDays > 0)
      {
        --animal.SicknessGestationRemaining_quarterDays;
        if (animal.TEMP_SicknessFullRollDuration < 0)
          animal.TEMP_SicknessFullRollDuration = thisDisease.RollRegularityPerAnimal;
        --animal.Sickness_TimeUntilRollToInfectOtherAnimals;
        if (animal.Sickness_TimeUntilRollToInfectOtherAnimals > 0)
          return;
        animal.Sickness_TimeUntilRollToInfectOtherAnimals = animal.TEMP_SicknessFullRollDuration;
        SicknessWoundCalculator.TryToInfectOtherAnimals(player, prisonzone, thisDisease);
      }
      else
      {
        --animal.SicknessTimeRemaining_quarterDays;
        if (!animal.WillDieFromSickness || animal.SicknessTimeTODEATHRemaining_quarterDays <= 0)
          return;
        --animal.SicknessTimeTODEATHRemaining_quarterDays;
        if (animal.SicknessTimeTODEATHRemaining_quarterDays > 0)
          return;
        LifeExpectancy_Calculator.TryAndKill(animal, CauseOfDeath.Sickness, true);
      }
    }

    private static void TryToInfectOtherAnimals(
      Player player,
      PrisonZone prisonzone,
      Disease disease)
    {
      prisonzone.prisonercontainer.TryToInfectAnimal(disease, 1);
    }
  }
}
