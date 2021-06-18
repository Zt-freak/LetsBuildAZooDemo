// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.SellCosts.AnimalSellCostCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.Z_BalanceSystems.Animals.SellCosts
{
  internal class AnimalSellCostCalculator
  {
    public static int GetSellCostOfPlayerAnimal(PrisonerInfo animal)
    {
      AnimalType animaltype = animal.intakeperson.animaltype;
      AnimalType headType = animal.intakeperson.HeadType;
      int clIndex = animal.intakeperson.CLIndex;
      int headVariant = animal.intakeperson.HeadVariant;
      float num1 = (float) animal.Age / animal.LifeExpetancy;
      int num2 = animal.GetIsSick() ? 1 : 0;
      int num3 = animal.IsFertile ? 1 : 0;
      float num4 = 0.0f;
      float num5 = AnimalData.GetAnimalStat(animaltype).Popularity;
      if (headType != AnimalType.None)
      {
        num4 = 2.5f;
        num5 = AnimalData.GetAnimalStat(animaltype).Popularity + AnimalData.GetAnimalStat(headType).Popularity * 0.5f;
      }
      float num6 = 400f * num5;
      float num7 = num6 + num6 * num4 - num1 * num6;
      if (num3 != 0)
        num7 *= 2f;
      if (num2 != 0)
        num7 /= 2f;
      return (int) Math.Round((double) num7);
    }

    public static int GetBuyCostfromBlackMarket(
      AnimalType bodyAnimalType,
      int bodyVariant,
      AnimalType headAnimalType,
      int headVariant)
    {
      float num1 = 2000f;
      AnimalStat animalStat1 = AnimalData.GetAnimalStat(bodyAnimalType);
      float num2 = animalStat1.Popularity;
      float num3 = (float) bodyVariant;
      float num4 = 0.0f;
      if (bodyAnimalType != headAnimalType)
      {
        AnimalStat animalStat2 = AnimalData.GetAnimalStat(headAnimalType);
        num2 = (float) (((double) animalStat1.Popularity + (double) animalStat2.Popularity) * 0.5);
        num4 = 3f;
        num3 = (float) (bodyVariant + headVariant) * 0.5f;
      }
      float num5 = num1 * num2;
      int num6 = Player.criticalchoices.GoodCoicesMade - Player.criticalchoices.BadChoicesMade;
      float num7 = 0.0f + num5 + num4 * num5 + num3 * num5;
      if (Z_DebugFlags.IsBetaVersion && num6 < 0)
        num7 /= 2f;
      return (int) Math.Round((double) num7);
    }
  }
}
