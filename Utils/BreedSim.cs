// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.BreedSim
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Breeding;

namespace TinyZoo.Utils
{
  internal class BreedSim
  {
    internal static void CheckBreedingSim()
    {
      BreedData.SetUpBreedData();
      bool[] flagArray1 = new bool[11];
      bool[] flagArray2 = new bool[11];
      int num = 0;
      AnimalType animalType = AnimalType.Lion;
      for (int index1 = 0; index1 < 100; ++index1)
      {
        for (int index2 = 0; index2 < BreedData.breedinfo[(int) animalType].breedentries.Count; ++index2)
        {
          if (BreedData.breedinfo[(int) animalType].breedentries[index2].Parent1_girl > -1)
          {
            bool flag1 = false;
            bool flag2 = false;
            for (int index3 = index2 + 1; index3 < BreedData.breedinfo[(int) animalType].breedentries.Count; ++index3)
            {
              if (BreedData.breedinfo[(int) animalType].breedentries[index3].Parent1_girl == index2)
                flag1 = true;
              if (BreedData.breedinfo[(int) animalType].breedentries[index3].Parent2 == index2)
                flag2 = true;
            }
            while (flag1 | flag2)
            {
              num += BreedSim.GetTries(BreedData.breedinfo[(int) animalType].breedentries[index2].PercentChanceOfThisChild);
              if (Game1.Rnd.Next(0, 2) == 0)
                flag1 = false;
              else
                flag2 = false;
            }
          }
        }
      }
      Math.Round((double) num / 100.0);
      Game1.ClsCLR.SetAllColours(0.0f, 0.4f, 0.0f);
    }

    internal static int GetTries(int Probability)
    {
      Console.WriteLine("PROBAB" + (object) Probability);
      int num = 0;
      bool flag = false;
      while (!flag)
      {
        ++num;
        if (Game1.Rnd.Next(0, 100) < Probability)
          return num;
      }
      return 0;
    }
  }
}
