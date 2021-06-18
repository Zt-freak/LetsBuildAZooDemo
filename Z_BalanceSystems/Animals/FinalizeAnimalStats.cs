// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.FinalizeAnimalStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class FinalizeAnimalStats
  {
    internal static void ApplyAnimalStats(
      PrisonZone prisonzone,
      Player player,
      float SpaceInPenValue)
    {
      float num1 = 0.0f;
      float num2 = 0.0f;
      if ((double) SpaceInPenValue < 1.0)
      {
        num1 = (float) ((1.0 - (double) SpaceInPenValue) * 0.5);
        num2 = (float) ((1.0 - (double) SpaceInPenValue) * 1.0);
      }
      else
        num1 = -Math.Min((float) (((double) SpaceInPenValue - 1.0) * 0.0199999995529652), 0.08f);
      prisonzone.TempAnimalEnrichmentUnfulfillment = 0.0f;
      for (int index1 = 0; index1 < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index1)
      {
        double num3 = 1.0 - (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].AnimalHabitatMatch;
        float num4 = 0.0f;
        if (num3 < 0.5)
        {
          float num5 = (float) (1.0 - ((double) num4 + 0.5));
        }
        if (prisonzone.prisonercontainer.tempAnimalInfo[index1].GroupSizeLoneliness < 100)
        {
          int groupSizeLoneliness1 = prisonzone.prisonercontainer.tempAnimalInfo[index1].GroupSizeLoneliness;
          int groupSizeLoneliness2 = prisonzone.prisonercontainer.tempAnimalInfo[index1].GroupSizeLoneliness;
        }
        float num6 = 0.0f;
        FinalizeAnimalStats.SetEnrichment(prisonzone, index1);
        if (prisonzone.prisonercontainer.tempAnimalInfo[index1].LargeGroupStress > 0)
        {
          int largeGroupStress = prisonzone.prisonercontainer.tempAnimalInfo[index1].LargeGroupStress;
          num6 = (float) ((double) prisonzone.prisonercontainer.tempAnimalInfo[index1].LargeGroupStress * 0.00999999977648258 * 0.200000002980232);
          if (prisonzone.prisonercontainer.tempAnimalInfo[index1].LargeGroupStress > 70)
            prisonzone.LargeGroup_DiseaseBonus += prisonzone.prisonercontainer.tempAnimalInfo[index1].LargeGroupStress - 70;
        }
        for (int index2 = 0; index2 < prisonzone.prisonercontainer.tempAnimalInfo[index1].AllOfThese.Count; ++index2)
        {
          PrisonerInfo prisoner = prisonzone.prisonercontainer.prisoners[prisonzone.prisonercontainer.tempAnimalInfo[index1].AllOfThese[index2]];
          if (!prisoner.IsDead)
          {
            prisoner.FightDesire += (float) (((double) num2 + (double) num6) * 0.5);
            if ((double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment < 1.0)
            {
              prisoner.Appetite -= (float) ((1.0 - (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment) * 0.00999999977648258);
              prisoner.Sleepyness += (float) ((1.0 - (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment) * 0.100000001490116);
              prisoner.EnrichmentValue -= (float) ((1.0 - (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment) * 0.100000001490116);
              if ((double) prisoner.EnrichmentValue < (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment)
                prisoner.EnrichmentValue = Math.Min(prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment, prisoner.EnrichmentValue + 0.07f);
            }
            else
            {
              prisoner.EnrichmentValue += (float) (((double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment - 1.0) * 0.100000001490116);
              if ((double) prisoner.EnrichmentValue > (double) prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment)
                prisoner.EnrichmentValue = Math.Max(prisonzone.prisonercontainer.tempAnimalInfo[index1].EnrichmentFullfillment, prisoner.EnrichmentValue - 0.07f);
            }
            if (Z_DebugFlags.developerOverrides[0] != 3)
            {
              if (Z_DebugFlags.developerOverrides[0] > 0)
              {
                if (Z_DebugFlags.developerOverrides[0] == 1)
                  prisoner.FightDesire = 5f;
                else
                  prisoner.FightDesire += (float) (((double) num2 + (double) num6) * 0.5);
              }
              if ((double) prisoner.FightDesire >= 1.0)
                LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.AnimalFight, (double) num6 > (double) num2, prisonzone.Cell_UID, prisoner.intakeperson.UID));
            }
          }
        }
      }
    }

    internal static void SetEnrichment(PrisonZone prisonzone)
    {
      prisonzone.TempAnimalEnrichmentUnfulfillment = 0.0f;
      if (prisonzone.prisonercontainer.tempAnimalInfo == null)
        return;
      for (int X = 0; X < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++X)
        FinalizeAnimalStats.SetEnrichment(prisonzone, X);
    }

    private static void SetEnrichment(PrisonZone prisonzone, int X)
    {
      prisonzone.prisonercontainer.tempAnimalInfo[X].EnrichmentFullfillment = prisonzone.prisonercontainer.tempAnimalInfo[X].TotalEnrichmentValue / prisonzone.prisonercontainer.tempAnimalInfo[X].RequiredEnrichment;
      if ((double) prisonzone.prisonercontainer.tempAnimalInfo[X].EnrichmentFullfillment >= 1.0)
        return;
      prisonzone.TempAnimalEnrichmentUnfulfillment += 1f - prisonzone.prisonercontainer.tempAnimalInfo[X].EnrichmentFullfillment;
    }
  }
}
