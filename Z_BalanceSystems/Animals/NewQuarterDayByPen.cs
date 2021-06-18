// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.NewQuarterDayByPen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_BalanceSystems.Animals.SicknessAndWounds;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class NewQuarterDayByPen
  {
    internal static void DoQuarterDay(Player player, int QuarterIndex)
    {
      if (QuarterIndex == 0)
        return;
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].GetIsSick())
            SicknessWoundCalculator.Calculate_SicknessOnQuarterUpdate(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], player, player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        }
      }
    }
  }
}
