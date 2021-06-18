// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Publicity.PublicityCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.Z_BalanceSystems.Park;

namespace TinyZoo.Z_BalanceSystems.Publicity
{
  internal class PublicityCalculator
  {
    internal static bool RecalculatePublicity = true;
    private static float LastCalculatedPublicity;

    internal static int CalculatePublicity(Player player)
    {
      if (PublicityCalculator.RecalculatePublicity)
      {
        PublicityCalculator.RecalculatePublicity = false;
        PublicityCalculator.LastCalculatedPublicity = 0.0f;
        PublicityCalculator.LastCalculatedPublicity += (float) player.heroquestprogress.GetTotalQuestsComplete();
        PublicityCalculator.LastCalculatedPublicity += DecoCalculator.TotalSignPoints;
        PublicityCalculator.LastCalculatedPublicity += PublicityCalculator.GetBusPublicity(player);
        PublicityCalculator.LastCalculatedPublicity += (float) player.unlocks.GetTotalResearchUnlocked();
      }
      return (int) Math.Round((double) PublicityCalculator.LastCalculatedPublicity);
    }

    private static float GetBusPublicity(Player player)
    {
      float[] busByRouteArray = player.busroutes.GetBusByRouteArray();
      float num = 0.0f;
      for (int index = 0; index < busByRouteArray.Length; ++index)
      {
        if ((double) busByRouteArray[index] > 0.0 && index != 0)
          num += 6f;
        num += busByRouteArray[index];
        if (index == 0)
          --num;
      }
      return num;
    }
  }
}
