// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.CrimeData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.GamePlay.Enemies
{
  internal class CrimeData
  {
    internal static List<string> CrimeNames;

    internal static string GetCrime(out int Index, int GetThis = -1)
    {
      if (CrimeData.CrimeNames == null)
      {
        CrimeData.CrimeNames = new List<string>();
        CrimeData.CrimeNames.Add(SEngine.Localization.Localization.GetText(362));
      }
      if (GetThis > -1)
      {
        Index = GetThis;
        return CrimeData.CrimeNames[GetThis];
      }
      Index = Game1.Rnd.Next(0, CrimeData.CrimeNames.Count - 1);
      return CrimeData.CrimeNames[Index];
    }
  }
}
