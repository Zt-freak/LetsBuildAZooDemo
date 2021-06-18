// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Morality.MoralityCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_Morality.Calculators;
using TinyZoo.Z_Morality.Calculators.Polution;

namespace TinyZoo.Z_Morality
{
  internal class MoralityCalculator
  {
    internal static void CalculateMorality(Player player)
    {
      Z_GameFlags.RecalculatedMorality = true;
      float moralityScore = player.livestats.MoralityScore;
      float num = 0.0f;
      Morality_ShopStats.CalculateMorality_ShopStats(player, ref num);
      Morality_Breeding.CalcMorality_Breeding(player, ref num);
      Morality_UnionCalculator.CalculateMorality(player, ref num);
      Morality_CriticalChoice.CalculateMorality_CriticalChoice(ref num);
      Morality_CarbonFuel.CalcMorality_CarbonFuel(player, ref num);
      Morality_CarbonMeat.CalcMorality_CarbonMeat(player, ref num);
      Morality_Electricity.CalcMorality_Electricity(player, ref num);
      Morality_Garbage.CalcMorality_Garbage(player, ref num);
      if ((double) num == (double) moralityScore)
        return;
      player.livestats.MoralityScore = num;
    }
  }
}
