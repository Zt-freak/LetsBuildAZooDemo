// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Gates.GateIntegrity_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_BalanceSystems.Animals.Gates
{
  internal class GateIntegrity_Calculator
  {
    public static void AdjustGateIntegrity(
      PrisonZone prisonzone,
      Player player,
      float RequiredSpace,
      float TotalTiles)
    {
      if ((double) RequiredSpace > (double) TotalTiles)
        prisonzone.GateIntegrity -= (float) (((double) RequiredSpace - (double) TotalTiles) * 0.200000002980232);
      prisonzone.GateIntegrity -= Math.Max(1f, TotalTiles * 0.02f);
      if (Z_DebugFlags.developerOverrides[4] == 3)
        return;
      if (Z_DebugFlags.developerOverrides[4] == 1)
        prisonzone.GateIntegrity = 0.0f;
      else if (Z_DebugFlags.developerOverrides[4] == 2)
        prisonzone.GateIntegrity -= (float) (((double) RequiredSpace - (double) TotalTiles) * 1.0);
      if ((double) prisonzone.GateIntegrity > 0.0)
        return;
      LiveStats.AddGateToBreak(prisonzone.Cell_UID);
    }
  }
}
