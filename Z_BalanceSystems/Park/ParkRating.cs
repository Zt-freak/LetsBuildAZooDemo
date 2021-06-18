// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Park.ParkRating
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_BalanceSystems.Publicity;

namespace TinyZoo.Z_BalanceSystems.Park
{
  internal class ParkRating
  {
    internal static bool NeedsRecalculating = true;
    private static int LastValue = -1;

    internal static int GetParkRating(Player player)
    {
      if (ParkRating.NeedsRecalculating || ParkRating.LastValue == -1)
      {
        ParkRating.NeedsRecalculating = false;
        double num1 = (double) (int) Math.Round((double) FacilitiesCalulator.CalculateFacilities(player));
        float AnimalValue;
        double valueAndPopularity = (double) ParkPopularity.CalculateAnimalValueAndPopularity(player, out AnimalValue, out int _);
        float deco = DecoCalculator.CalculateDeco(player);
        float publicity = (float) PublicityCalculator.CalculatePublicity(player);
        double num2 = (double) (int) Math.Round((double) AnimalValue);
        int num3 = (int) Math.Round(num1 + num2 + (double) (int) Math.Round((double) deco) + (double) (int) Math.Round((double) publicity));
        if (num3 != ParkRating.LastValue)
        {
          ParkRating.LastValue = num3;
          QuestScrubber.ScrubOnParkRatingChange(player);
        }
      }
      return ParkRating.LastValue;
    }
  }
}
