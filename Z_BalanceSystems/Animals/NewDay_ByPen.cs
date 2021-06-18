// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.NewDay_ByPen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_BalanceSystems.Animals.Breed;
using TinyZoo.Z_BalanceSystems.Animals.Enrichment;
using TinyZoo.Z_BalanceSystems.Animals.Gates;
using TinyZoo.Z_BalanceSystems.Animals.LifeExpectancy;
using TinyZoo.Z_BalanceSystems.Animals.SicknessAndWounds;
using TinyZoo.Z_BalanceSystems.Diseases;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class NewDay_ByPen
  {
    internal static float Day_TotalAnimalScore_UnderSpace;
    internal static float Day_TotalSpace;
    internal static float Day_TotalHungryAnimalValue;
    internal static float Day_TotalRequiredEnrichment;
    internal static float Day_TotalEnrichmentAnimalsHave;
    internal static float Day_CohabitationStress;
    internal static float DayDirtyness;
    internal static int Day_CollectiveCorpseAge;
    internal static int Day_CollectiveCorpses;
    internal static int PoopLeavingReason;
    internal static int CorpseLeavingReason;

    internal static void CalcAnimalsNonPen(Player player)
    {
      for (int index = 0; index < player.prisonlayout.AnimalsNotInPens.Count; ++index)
        LifeExpectancy_Calculator.SimpleCalculate_LifeExpectancy(player.prisonlayout.AnimalsNotInPens[index], 1f, player);
    }

    internal static void RecountWaterCoverage(Player player, int PenUID)
    {
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        if (player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID == PenUID)
        {
          float Water = (float) player.prisonlayout.cellblockcontainer.prisonzones[index1].TEMP_LakeSize * 0.2f + (float) player.prisonlayout.cellblockcontainer.prisonzones[index1].penItems.GetWaterSize(player);
          player.prisonlayout.cellblockcontainer.prisonzones[index1].TempTotalWater = Water;
          player.prisonlayout.cellblockcontainer.prisonzones[index1].TempHasEnoughWater = true;
          for (int index2 = player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count - 1; index2 > -1; --index2)
          {
            LifeExpectancy_Calculator.Calculate_Water(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], ref Water);
            if ((double) Water <= 0.0)
              player.prisonlayout.cellblockcontainer.prisonzones[index1].TempHasEnoughWater = false;
          }
        }
      }
    }

    internal static void CalcAnimals(Player player)
    {
      NewDay_ByPen.Day_TotalHungryAnimalValue = 0.0f;
      NewDay_ByPen.Day_TotalAnimalScore_UnderSpace = 0.0f;
      NewDay_ByPen.Day_TotalSpace = 0.0f;
      NewDay_ByPen.Day_TotalRequiredEnrichment = 0.0f;
      NewDay_ByPen.Day_TotalEnrichmentAnimalsHave = 0.0f;
      NewDay_ByPen.Day_CohabitationStress = 0.0f;
      NewDay_ByPen.DayDirtyness = 0.0f;
      NewDay_ByPen.Day_CollectiveCorpses = 0;
      NewDay_ByPen.Day_CollectiveCorpseAge = 0;
      NewDay_ByPen.PoopLeavingReason = 0;
      NewDay_ByPen.CorpseLeavingReason = 0;
      player.storerooms.HasRunOutOfSomethingThatIsNeededByAnimals = false;
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        player.prisonlayout.cellblockcontainer.prisonzones[index1].WelfareAndCleanliness = 1f;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].CorpseWelfareContribution = 0.0f;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].PoopWelfareContribution = 0.0f;
        float TerritorySize = 0.0f;
        float aniamlsInThisPen = player.prisonlayout.cellblockcontainer.prisonzones[index1].GetSpaceRequiredByAniamlsInThisPen(ref TerritorySize);
        float floorSpaceVolume = (float) player.prisonlayout.cellblockcontainer.prisonzones[index1].GetFloorSpaceVolume();
        player.prisonlayout.cellblockcontainer.prisonzones[index1].StartDay(player);
        if ((double) TerritorySize + (double) aniamlsInThisPen > (double) floorSpaceVolume)
          NewDay_ByPen.Day_TotalAnimalScore_UnderSpace += TerritorySize + aniamlsInThisPen - floorSpaceVolume;
        NewDay_ByPen.Day_TotalSpace += TerritorySize + aniamlsInThisPen;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.SetUpTempAnimals(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID, player.prisonlayout.cellblockcontainer.prisonzones[index1].CellBLOCKTYPE, player, true);
        float spaceInPen = NewDay_ByPen.GetSpaceInPen(player.prisonlayout.cellblockcontainer.prisonzones[index1], aniamlsInThisPen, floorSpaceVolume);
        Cohabitation_Calculator.Cal_Cohabitation(player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        GroupSizeCalculator.CalculateGroupSize(player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        ShelterCalculator.CalculateShelter(player.prisonlayout.cellblockcontainer.prisonzones[index1], player);
        GateIntegrity_Calculator.AdjustGateIntegrity(player.prisonlayout.cellblockcontainer.prisonzones[index1], player, aniamlsInThisPen, floorSpaceVolume);
        EnrichmentCalculator.Cal_Enichment(player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        float Water = (float) player.prisonlayout.cellblockcontainer.prisonzones[index1].TEMP_LakeSize * 0.2f + (float) player.prisonlayout.cellblockcontainer.prisonzones[index1].penItems.GetWaterSize(player);
        player.prisonlayout.cellblockcontainer.prisonzones[index1].TempTotalWater = Water;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].TempHasEnoughWater = true;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_ShortThisMuchEnrichment = 0.0f;
        player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_UnenrichedAnimals = 0;
        for (int index2 = player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count - 1; index2 > -1; --index2)
        {
          SicknessWoundCalculator.Calculate_SicknessOnQuarterUpdate(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], player, player.prisonlayout.cellblockcontainer.prisonzones[index1]);
          LifeExpectancy_Calculator.Calculate_LifeExpectancy(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], spaceInPen, player, ref Water, player.prisonlayout.cellblockcontainer.prisonzones[index1].WelfareAndCleanliness);
          if ((double) Water <= 0.0)
            player.prisonlayout.cellblockcontainer.prisonzones[index1].TempHasEnoughWater = false;
          if ((double) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].EnrichmentValue < 1.0)
          {
            player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_ShortThisMuchEnrichment += 1f - player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].EnrichmentValue;
            ++player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_UnenrichedAnimals;
          }
        }
        Cleanliness_Calculator.DoCheckCleanliness(player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        RandomBreedCalculator.DoCheckRandomBreedsOnStartDay(player, player.prisonlayout.cellblockcontainer.prisonzones[index1], spaceInPen, (double) floorSpaceVolume > (double) aniamlsInThisPen);
        FinalizeAnimalStats.ApplyAnimalStats(player.prisonlayout.cellblockcontainer.prisonzones[index1], player, spaceInPen);
        DiseaseGenerator.CheckCreateNewDisease(player.prisonlayout.cellblockcontainer.prisonzones[index1], player);
      }
    }

    internal static float GetSpaceInPen(
      PrisonZone prisonzone,
      float RequiredSpace,
      float TotalTiles)
    {
      float num = TotalTiles / RequiredSpace;
      prisonzone.LastCalculatedQualityForSpace = num;
      return num;
    }
  }
}
