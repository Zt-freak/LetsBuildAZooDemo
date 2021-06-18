// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Current_FarmDestinations
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_BalanceSystems.Farm;

namespace TinyZoo.Z_BalanceSystems
{
  internal class Current_FarmDestinations
  {
    internal static List<LocationsByFarm> FarmIDsReadyForHarvesting;
    internal static List<LocationsByFarm> FarmIDsReadyForSeeding;

    internal static void FarmeWentHere(int FieldUID, Vector2Int Location)
    {
      for (int index = Current_FarmDestinations.FarmIDsReadyForHarvesting.Count - 1; index > -1; --index)
      {
        if (Current_FarmDestinations.FarmIDsReadyForHarvesting[index].CheckLoc(Location))
          Current_FarmDestinations.FarmIDsReadyForHarvesting.RemoveAt(index);
      }
      for (int index = Current_FarmDestinations.FarmIDsReadyForSeeding.Count - 1; index > -1; --index)
      {
        if (Current_FarmDestinations.FarmIDsReadyForSeeding[index].CheckLoc(Location))
          Current_FarmDestinations.FarmIDsReadyForSeeding.RemoveAt(index);
      }
    }
  }
}
