// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.GroupSizeCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class GroupSizeCalculator
  {
    internal static void CalculateGroupSize(PrisonZone prisonzone)
    {
      for (int index = 0; index < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index)
      {
        int idealGroupSize = AnimalData.GetIdealGroupSize(prisonzone.prisonercontainer.tempAnimalInfo[index].animaltype);
        int count = prisonzone.prisonercontainer.tempAnimalInfo[index].AllOfThese.Count;
        if (count < idealGroupSize)
          prisonzone.prisonercontainer.tempAnimalInfo[index].GroupSizeLoneliness = (int) ((1.0 - (double) (count - 1) / (double) (idealGroupSize - 1)) * 100.0);
        else if (count > idealGroupSize)
          prisonzone.prisonercontainer.tempAnimalInfo[index].LargeGroupStress = (int) Math.Min((float) (((double) (count - 1) / (double) (idealGroupSize - 1) - 1.0) * 50.0), 100f);
      }
    }
  }
}
