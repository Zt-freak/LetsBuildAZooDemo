// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.CRISPR.CRISPRCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_BalanceSystems.Animals.CRISPR
{
  internal class CRISPRCalculator
  {
    public static float GetDaysForThisCRISPRBreed(AnimalType animalOne, AnimalType animalTwo)
    {
      if (Z_DebugFlags.QuickCRISPRBreeds)
        return 1f;
      float num = (float) (ActiveBreed.GetDaysForpregnancy(animalOne) + ActiveBreed.GetDaysForpregnancy(animalTwo)) * 1.5f;
      return Z_DebugFlags.IsBetaVersion ? 4f : (float) Math.Ceiling((double) num);
    }
  }
}
