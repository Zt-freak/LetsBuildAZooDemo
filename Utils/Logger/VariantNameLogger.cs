// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.Logger.VariantNameLogger
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.IO;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Utils.Logger
{
  internal class VariantNameLogger
  {
    internal static void CreateVariantListCSV()
    {
      using (FileStream fileStream = new FileStream("HybridNames.csv", FileMode.Create))
      {
        StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
        string str1 = "Body,Head,CombionedName,";
        streamWriter.WriteLine(str1);
        for (int index1 = 0; index1 < 56; ++index1)
        {
          for (int index2 = 0; index2 < 56; ++index2)
          {
            string str2 = AnimalData.GetAnimalName((AnimalType) index1) + "," + AnimalData.GetAnimalName((AnimalType) index2) + "," + HybridNames.GetAnimalCombinedName((AnimalType) index1, (AnimalType) index2) + ",";
            streamWriter.WriteLine(str2);
          }
        }
        streamWriter.Close();
      }
    }
  }
}
