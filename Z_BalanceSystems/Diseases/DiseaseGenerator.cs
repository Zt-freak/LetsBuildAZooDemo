// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Diseases.DiseaseGenerator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Diseases;

namespace TinyZoo.Z_BalanceSystems.Diseases
{
  internal class DiseaseGenerator
  {
    internal static int DiseaseBuffer = 15;

    internal static void CheckCreateNewDisease(
      PrisonZone prisonzone,
      Player player,
      bool DebugForceSpawnIllness = false)
    {
      if (prisonzone.prisonercontainer.prisoners.Count == 0 || !(prisonzone.Cleanliness_LastCalculatedDIRTYNESS < 100 + DiseaseGenerator.DiseaseBuffer | DebugForceSpawnIllness) || !(Game1.Rnd.Next(0, 100) > 100 - (prisonzone.Cleanliness_LastCalculatedDIRTYNESS + prisonzone.LargeGroup_DiseaseBonus) | DebugForceSpawnIllness))
        return;
      Game1.Rnd.Next(0, 100);
      int calculatedCorpseValue = prisonzone.LastCalculatedCorpseValue;
      int DirtValue1 = 0;
      if (!prisonzone.TEMP_LakeHasCleanWater)
        DirtValue1 = Game1.Rnd.Next(0, 100) - prisonzone.DaysOfDirtyLake;
      int DirtValue2 = Game1.Rnd.Next(0, 100) - prisonzone.LastCalculatedCorpseValue;
      int DirtValue3 = Game1.Rnd.Next(0, 100) - prisonzone.LastCalculatedPoopValue;
      if (DirtValue2 <= 0 && DirtValue1 <= 0 && DirtValue2 <= 0)
        return;
      Disease disease = new Disease();
      if (DirtValue3 > DirtValue2 && DirtValue3 > DirtValue1)
        disease.MakePoopDisease(DirtValue3);
      else if (DirtValue2 > DirtValue3 && DirtValue2 > DirtValue1)
        disease.MakeCorpseDisease(DirtValue2);
      else if (DirtValue1 > DirtValue3 && DirtValue1 > DirtValue2)
      {
        disease.MakeWaterDisease(DirtValue1);
      }
      else
      {
        int maxValue = 0;
        if (DirtValue3 > 0)
          ++maxValue;
        if (DirtValue2 > 0)
          ++maxValue;
        if (DirtValue1 > 0)
          ++maxValue;
        int num = Game1.Rnd.Next(0, maxValue);
        switch (maxValue)
        {
          case 2:
            if (DirtValue1 <= 0)
            {
              if (num == 0)
              {
                disease.MakePoopDisease(DirtValue3);
                break;
              }
              disease.MakeCorpseDisease(DirtValue2);
              break;
            }
            if (DirtValue2 <= 0)
            {
              if (num == 0)
              {
                disease.MakePoopDisease(DirtValue3);
                break;
              }
              disease.MakeWaterDisease(DirtValue1);
              break;
            }
            if (DirtValue3 <= 0)
            {
              if (num == 0)
              {
                disease.MakeWaterDisease(DirtValue1);
                break;
              }
              disease.MakeCorpseDisease(DirtValue2);
              break;
            }
            break;
          case 3:
            switch (num)
            {
              case 0:
                disease.MakePoopDisease(DirtValue3);
                break;
              case 1:
                disease.MakeCorpseDisease(DirtValue2);
                break;
              default:
                disease.MakeWaterDisease(DirtValue1);
                break;
            }
            break;
        }
      }
      disease.StartLoction = prisonzone.TryToGetRandomVector2IntInCellBlock();
      disease.CanInfectThese.Add(prisonzone.prisonercontainer.prisoners[0].intakeperson.animaltype);
      prisonzone.prisonercontainer.prisoners[0].MakeSick(disease.UID);
      player.Stats.AddNewDisease(disease);
      prisonzone.prisonercontainer.TryToInfectAnimal(disease, 1);
    }

    internal static void CheckAddNewDiseaseToExternalAnimal(AnimalSource animalsource)
    {
    }
  }
}
