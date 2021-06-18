// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ZooValues.AnimalTicketValue
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems;

namespace TinyZoo.Z_ZooValues
{
  internal class AnimalTicketValue
  {
    internal static bool MustUpdateTicketCost = true;
    private static int LastTicketCost;
    private static bool SetUp = false;

    internal static float GetAnimalValue(AnimalType animal, int VariantIndex)
    {
      float num1 = 10f;
      float num2;
      switch (animal)
      {
        case AnimalType.Rabbit:
          num2 = 10f;
          break;
        case AnimalType.Goose:
          num2 = 15f;
          break;
        case AnimalType.Capybara:
          num2 = 20f;
          break;
        default:
          return num1 = 30f;
      }
      return num2 + num2 / 5f * (float) VariantIndex;
    }

    internal static float CalculateShopValue(Player player, out float DecorativesValue) => player.prisonlayout.cellblockcontainer.GetInfrastuctureValue(out DecorativesValue);

    internal static float GetIdealParkTicketCost(Player player)
    {
      if (AnimalTicketValue.MustUpdateTicketCost)
      {
        AnimalTicketValue.MustUpdateTicketCost = false;
        float AnimalValue;
        int HasHybrid;
        double valueAndPopularity = (double) ParkPopularity.CalculateAnimalValueAndPopularity(player, out AnimalValue, out HasHybrid);
        if (!AnimalTicketValue.SetUp)
        {
          AnimalTicketValue.SetUp = true;
          for (int index = 0; index < 14; ++index)
            CategoryData.GetEntriesInThisCategory((CATEGORYTYPE) index);
        }
        float DecorativesValue;
        float num = (AnimalTicketValue.CalculateShopValue(player, out DecorativesValue) + DecorativesValue + AnimalValue * 3f) * 0.15f;
        if ((double) num > 0.0)
          num += 5f;
        if (HasHybrid > 0)
          num += 3f;
        if (HasHybrid > 2)
          num += 2f;
        if (HasHybrid > 5)
          ++num;
        if (HasHybrid > 10)
          ++num;
        AnimalTicketValue.LastTicketCost = (int) Math.Round((double) num);
      }
      return (float) AnimalTicketValue.LastTicketCost;
    }

    internal static Expensiveness GetTicketExpensivenss(
      Player player,
      int OverrideCost = -1)
    {
      int idealParkTicketCost = (int) AnimalTicketValue.GetIdealParkTicketCost(player);
      if (idealParkTicketCost == 0)
        return Expensiveness.ParkHasNoValue;
      int num1 = (int) ((double) idealParkTicketCost * 0.200000002980232);
      int num2 = player.Stats.GetTicketCost();
      if (player.Stats.GetTicketIsFree())
        num2 = 0;
      if (OverrideCost > -1)
        num2 = OverrideCost;
      if (num2 < idealParkTicketCost - num1)
        return Expensiveness.TooCheap;
      if (num2 > idealParkTicketCost + num1)
        return Expensiveness.TooExpensive;
      if (num2 < idealParkTicketCost - num1 / 2)
        return Expensiveness.SlightlyTooCheap;
      return num2 > idealParkTicketCost + num1 / 2 ? Expensiveness.SLightlyTooExpensive : Expensiveness.AboutRight;
    }
  }
}
