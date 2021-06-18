// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Enrichment.EnrichmentCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_BalanceSystems.Animals.Enrichment
{
  internal class EnrichmentCalculator
  {
    private static List<PenItemDuplicateInfo> penitems;

    internal static float GetBarForThis_Enichment(
      PrisonZone prisonzone,
      AnimalType animaltocheck,
      TILETYPE tile,
      out float BarFullnessWithOneMoreOfThese,
      bool IsDuringDayCalc = false)
    {
      if (Z_DebugFlags.developerOverrides[6] > 0)
      {
        BarFullnessWithOneMoreOfThese = 1f;
        return 1f;
      }
      BarFullnessWithOneMoreOfThese = 0.0f;
      prisonzone.prisonercontainer.SetUpTempAnimals(prisonzone.Cell_UID, prisonzone.CellBLOCKTYPE, (Player) null);
      EnrichmentCalculator.Cal_Enichment(prisonzone, IsDuringDayCalc);
      ENRICHMENTCLASS toEnrichmentClass = EnrichmentData.GetTILETYPEToEnrichmentClass(tile);
      EnrichmentEntry enrichmentEntry = EnrichmentData.GetEnrichmentEntry(toEnrichmentClass);
      float num = 0.0f;
      bool flag = false;
      for (int index1 = 0; index1 < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index1)
      {
        if (prisonzone.prisonercontainer.tempAnimalInfo[index1].animaltype == animaltocheck)
        {
          for (int index2 = 0; index2 < EnrichmentCalculator.penitems.Count; ++index2)
          {
            if (EnrichmentCalculator.penitems[index2].enrichmentclass == toEnrichmentClass)
            {
              for (int index3 = 0; index3 < enrichmentEntry.Animal_EnrichValue.Count; ++index3)
              {
                if ((AnimalType) enrichmentEntry.Animal_EnrichValue[index3].X == animaltocheck)
                {
                  flag = true;
                  num = EnrichmentCalculator.penitems[index2].GetTotalPoints((float) enrichmentEntry.Animal_EnrichValue[index3].Y, EnrichmentCalculator.penitems[index2].TotalOfThese + 1);
                }
              }
            }
          }
          if (!flag)
          {
            for (int index2 = 0; index2 < enrichmentEntry.Animal_EnrichValue.Count; ++index2)
            {
              if ((AnimalType) enrichmentEntry.Animal_EnrichValue[index2].X == animaltocheck)
                num = new PenItemDuplicateInfo(enrichmentEntry.enrichmentclass).GetTotalPoints((float) enrichmentEntry.Animal_EnrichValue[index2].Y, 1);
            }
          }
          NewDay_ByPen.Day_TotalRequiredEnrichment += prisonzone.prisonercontainer.tempAnimalInfo[index1].RequiredEnrichment;
          BarFullnessWithOneMoreOfThese = num / prisonzone.prisonercontainer.tempAnimalInfo[index1].RequiredEnrichment;
          return prisonzone.prisonercontainer.tempAnimalInfo[index1].TotalEnrichmentValue / prisonzone.prisonercontainer.tempAnimalInfo[index1].RequiredEnrichment;
        }
      }
      return 1f;
    }

    internal static void Cal_Enichment(PrisonZone prisonzone, bool IsDuringDayCal = false)
    {
      if (IsDuringDayCal && prisonzone.prisonercontainer.tempAnimalInfo != null)
      {
        for (int index = 0; index < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index)
          NewDay_ByPen.Day_TotalEnrichmentAnimalsHave -= prisonzone.prisonercontainer.tempAnimalInfo[index].TotalEnrichmentValue;
        for (int index = 0; index < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index)
          prisonzone.prisonercontainer.tempAnimalInfo[index].TotalEnrichmentValue = 0.0f;
      }
      EnrichmentCalculator.penitems = prisonzone.penItems.GetItemsHereByType();
      for (int index1 = 0; index1 < EnrichmentCalculator.penitems.Count; ++index1)
      {
        if (EnrichmentCalculator.penitems[index1].enrichmentclass < ENRICHMENTCLASS.Count)
        {
          EnrichmentEntry enrichmentEntry = EnrichmentData.GetEnrichmentEntry(EnrichmentCalculator.penitems[index1].enrichmentclass);
          if (prisonzone.prisonercontainer.tempAnimalInfo != null)
          {
            for (int index2 = 0; index2 < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index2)
            {
              if (enrichmentEntry.animalsthatusethis.Contains(prisonzone.prisonercontainer.tempAnimalInfo[index2].animaltype))
              {
                for (int index3 = 0; index3 < enrichmentEntry.Animal_EnrichValue.Count; ++index3)
                {
                  if ((AnimalType) enrichmentEntry.Animal_EnrichValue[index3].X == prisonzone.prisonercontainer.tempAnimalInfo[index2].animaltype)
                  {
                    float totalPoints = EnrichmentCalculator.penitems[index1].GetTotalPoints((float) enrichmentEntry.Animal_EnrichValue[index3].Y);
                    prisonzone.prisonercontainer.tempAnimalInfo[index2].TotalEnrichmentValue += totalPoints;
                    NewDay_ByPen.Day_TotalEnrichmentAnimalsHave += totalPoints;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
