// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Breed.RandomBreedCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_Breeding;

namespace TinyZoo.Z_BalanceSystems.Animals.Breed
{
  internal class RandomBreedCalculator
  {
    internal static void DoCheckRandomBreedsOnStartDay(
      Player player,
      PrisonZone pisonzone,
      float Conditions,
      bool PenHasSpaceLeft)
    {
      int count = pisonzone.prisonercontainer.prisoners.Count;
      if (!PenHasSpaceLeft && Z_DebugFlags.developerOverrides[13] != 1)
        return;
      List<PrisonerInfo> Males1 = new List<PrisonerInfo>();
      List<PrisonerInfo> Female1 = new List<PrisonerInfo>();
      List<AnimalType> animalTypeList = new List<AnimalType>();
      for (int index = 0; index < pisonzone.prisonercontainer.prisoners.Count; ++index)
      {
        if (!pisonzone.prisonercontainer.prisoners[index].IsPregnant && !pisonzone.prisonercontainer.prisoners[index].IsOnContraceptive)
        {
          if (!animalTypeList.Contains(pisonzone.prisonercontainer.prisoners[index].intakeperson.animaltype))
            animalTypeList.Add(pisonzone.prisonercontainer.prisoners[index].intakeperson.animaltype);
          if (pisonzone.prisonercontainer.prisoners[index].intakeperson.IsAGirl && !pisonzone.prisonercontainer.prisoners[index].IsPregnant && !pisonzone.prisonercontainer.prisoners[index].GetIsABaby())
            Female1.Add(pisonzone.prisonercontainer.prisoners[index]);
          else if (!pisonzone.prisonercontainer.prisoners[index].GetIsABaby() && !pisonzone.prisonercontainer.prisoners[index].intakeperson.IsAGirl)
            Males1.Add(pisonzone.prisonercontainer.prisoners[index]);
        }
      }
      if (animalTypeList.Count > 0)
      {
        for (int index1 = 0; index1 < animalTypeList.Count; ++index1)
        {
          List<PrisonerInfo> Males2 = new List<PrisonerInfo>();
          List<PrisonerInfo> Female2 = new List<PrisonerInfo>();
          for (int index2 = 0; index2 < Males1.Count; ++index2)
          {
            if (Males1[index2].intakeperson.animaltype == animalTypeList[index1])
              Males2.Add(Males1[index2]);
          }
          for (int index2 = 0; index2 < Female1.Count; ++index2)
          {
            if (Female1[index2].intakeperson.animaltype == animalTypeList[index1])
              Female2.Add(Female1[index2]);
          }
          RandomBreedCalculator.TryAndBreed(Males2, Female2, Conditions, player);
        }
      }
      else
        RandomBreedCalculator.TryAndBreed(Males1, Female1, Conditions, player);
    }

    private static void TryAndBreed(
      List<PrisonerInfo> Males,
      List<PrisonerInfo> Female,
      float Conditions,
      Player player)
    {
      if (Males.Count <= 0 || Female.Count <= 0)
        return;
      float num1 = AnimalData.GetBreedChance(Males[0].intakeperson.animaltype) * Conditions;
      if (Z_DebugFlags.developerOverrides[13] == 1)
        num1 = 100f;
      int num2 = 0;
      for (int index = 0; index < Math.Min(Males.Count, Female.Count); ++index)
      {
        if ((double) Game1.Rnd.Next(0, 100) < (double) num1)
          ++num2;
      }
      if (num2 <= 0)
        return;
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      for (int index = 0; index < Males.Count; ++index)
        intList1.Add(index);
      for (int index = 0; index < Female.Count; ++index)
        intList2.Add(index);
      while (num2 > 0)
      {
        --num2;
        int index1 = Game1.Rnd.Next(0, intList1.Count);
        int index2 = Game1.Rnd.Next(0, intList2.Count);
        Female[intList2[index2]].IsPregnant = true;
        int clIndex1 = Males[intList1[index1]].intakeperson.CLIndex;
        int clIndex2 = Female[intList2[index2]].intakeperson.CLIndex;
        int Percent = -1;
        int IndexOfLuckyBaby = -1;
        for (int index3 = 0; index3 < BreedData.breedinfo[(int) Males[0].intakeperson.animaltype].breedentries.Count; ++index3)
        {
          if (BreedData.breedinfo[(int) Males[0].intakeperson.animaltype].breedentries[index3].Parent1_girl == clIndex2 && BreedData.breedinfo[(int) Males[0].intakeperson.animaltype].breedentries[index3].Parent2 == clIndex1)
          {
            IndexOfLuckyBaby = index3;
            Percent = BreedData.breedinfo[(int) Males[0].intakeperson.animaltype].breedentries[index3].PercentChanceOfThisChild;
          }
        }
        player.breeds.StartBreed(-1, Percent, Males[0].intakeperson.animaltype, IndexOfLuckyBaby, player, clIndex1, clIndex2, Female[intList2[index2]].intakeperson.UID, Males[intList1[index1]].intakeperson.UID);
        intList1.RemoveAt(index1);
        intList2.RemoveAt(index2);
      }
    }
  }
}
