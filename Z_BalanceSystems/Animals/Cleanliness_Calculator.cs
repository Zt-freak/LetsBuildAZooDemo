// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cleanliness_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class Cleanliness_Calculator
  {
    internal static void DoCheckCleanliness(PrisonZone prisonzone)
    {
      prisonzone.TotalVolumeOfPoo = 0;
      prisonzone.TotalPoops = 0;
      int num1 = 0;
      for (int index = 0; index < prisonzone.poop.poo.Count; ++index)
      {
        prisonzone.TotalVolumeOfPoo += AnimalFoodData.GetFoodCollection(prisonzone.poop.poo[index].animal).Calc_FullServingSize;
        ++prisonzone.TotalPoops;
        prisonzone.WelfareAndCleanliness -= 0.03f;
        prisonzone.PoopWelfareContribution += 0.03f;
        prisonzone.PoopWelfareContribution += 0.03f;
        NewDay_ByPen.PoopLeavingReason += 3;
        if (prisonzone.poop.poo[index].DaysInPen > 1)
          num1 += prisonzone.poop.poo[index].DaysInPen - 1;
      }
      prisonzone.CorpseCount = 0;
      int num2 = 0;
      for (int index = 0; index < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index)
      {
        prisonzone.CorpseCount += prisonzone.prisonercontainer.tempAnimalInfo[index].Corpses;
        prisonzone.WelfareAndCleanliness -= (float) prisonzone.prisonercontainer.tempAnimalInfo[index].Corpses * 0.15f;
        prisonzone.CorpseWelfareContribution += (float) prisonzone.prisonercontainer.tempAnimalInfo[index].Corpses * 0.15f;
        NewDay_ByPen.CorpseLeavingReason += prisonzone.prisonercontainer.tempAnimalInfo[index].Corpses * 15;
        NewDay_ByPen.Day_CollectiveCorpses += prisonzone.prisonercontainer.tempAnimalInfo[index].Corpses;
        NewDay_ByPen.Day_CollectiveCorpseAge += prisonzone.prisonercontainer.tempAnimalInfo[index].CollectiveCorpseAge;
        num2 += prisonzone.prisonercontainer.tempAnimalInfo[index].CollectiveCorpseAge;
      }
      prisonzone.LastCalculatedPoopValue = Math.Min((prisonzone.TotalPoops + num1) * prisonzone.TotalVolumeOfPoo, 100);
      prisonzone.LastCalculatedCorpseValue = Math.Min(prisonzone.CorpseCount * num2, 100);
      prisonzone.LastCalculatedDirtyLake = 0;
      if (!prisonzone.TEMP_LakeHasCleanWater)
        prisonzone.LastCalculatedDirtyLake = Math.Min(prisonzone.DaysOfDirtyLake * prisonzone.TEMP_LakeSize * 3, 100);
      prisonzone.Cleanliness_LastCalculatedDIRTYNESS = Math.Min((int) ((double) prisonzone.LastCalculatedPoopValue * 0.899999976158142 + (double) prisonzone.LastCalculatedCorpseValue * 0.899999976158142 + (double) prisonzone.LastCalculatedDirtyLake * 0.449999988079071), 100);
      NewDay_ByPen.DayDirtyness += (float) prisonzone.Cleanliness_LastCalculatedDIRTYNESS;
    }
  }
}
