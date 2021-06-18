// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.Cohabitation_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.Animals.Cohabitation;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class Cohabitation_Calculator
  {
    internal static CohabThreatPack cohabThreat_pack;

    internal static ThreatPackV2[] GetThreatForNewAnimalsInPen(
      PrisonZone prisonZone,
      List<IntakePerson> SelectednewAnimals)
    {
      List<SimpleThreatPack> AllAnimals = new List<SimpleThreatPack>();
      if (prisonZone.prisonercontainer.tempAnimalInfo != null)
      {
        for (int index = 0; index < prisonZone.prisonercontainer.tempAnimalInfo.Count; ++index)
        {
          AllAnimals.Add(new SimpleThreatPack(prisonZone.prisonercontainer.tempAnimalInfo[index].animaltype));
          AllAnimals[index].Total = prisonZone.prisonercontainer.tempAnimalInfo[index].AllOfThese.Count;
        }
      }
      for (int index1 = 0; index1 < SelectednewAnimals.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < AllAnimals.Count; ++index2)
        {
          if (AllAnimals[index2].animaltype == SelectednewAnimals[index1].animaltype)
          {
            flag = true;
            ++AllAnimals[index2].Total;
          }
        }
        if (!flag)
          AllAnimals.Add(new SimpleThreatPack(SelectednewAnimals[index1].animaltype));
      }
      return Cohabitation_Calculator.SimpleThreatCalculator(AllAnimals);
    }

    internal static ThreatPackV2[] SimpleThreatCalculator(
      List<SimpleThreatPack> AllAnimals)
    {
      ThreatPackV2[] threatPackV2Array = new ThreatPackV2[AllAnimals.Count];
      for (int index = 0; index < AllAnimals.Count; ++index)
      {
        threatPackV2Array[index] = new ThreatPackV2(AllAnimals[index]);
        AnimalStat animalStat = AnimalData.GetAnimalStat(AllAnimals[index].animaltype);
        threatPackV2Array[index].AnimalStatsForThisAnimal = animalStat;
      }
      for (int index = 0; index < AllAnimals.Count; ++index)
      {
        if (AllAnimals[index].animaltype != threatPackV2Array[index].simplethreatpack.animaltype)
        {
          AnimalStat animalStat = AnimalData.GetAnimalStat(AllAnimals[index].animaltype);
          if ((animalStat.diettype == DietType.Carnivore || animalStat.diettype == DietType.Omnivore) && (threatPackV2Array[index].AnimalStatsForThisAnimal.diettype == DietType.Carnivore || threatPackV2Array[index].AnimalStatsForThisAnimal.diettype == DietType.Omnivore) && (double) threatPackV2Array[index].AnimalStatsForThisAnimal.Strength < (double) animalStat.Strength)
          {
            float Diff = (animalStat.Strength - threatPackV2Array[index].AnimalStatsForThisAnimal.Strength) * (animalStat.Aggression / 100f) * 0.01f;
            float TotalPackSizeMultiplier = (float) AllAnimals[index].Total / (float) threatPackV2Array[index].simplethreatpack.Total;
            threatPackV2Array[index].AddThreat(Diff, TotalPackSizeMultiplier, AllAnimals[index].animaltype, AllAnimals[index].Total);
          }
        }
      }
      return threatPackV2Array;
    }

    internal static CohabThreatPack GetOtherAnimalThreats(
      PrisonZone prisonzone,
      IntakePerson GetThisAnimalsThreat)
    {
      Cohabitation_Calculator.cohabThreat_pack = new CohabThreatPack();
      prisonzone.prisonercontainer.SetUpTempAnimals(prisonzone.Cell_UID, prisonzone.CellBLOCKTYPE, (Player) null);
      Cohabitation_Calculator.Cal_Cohabitation(prisonzone, GetThisAnimalsThreat);
      return Cohabitation_Calculator.cohabThreat_pack;
    }

    internal static void Cal_Cohabitation(PrisonZone prisonzone, IntakePerson GetThisAnimalsThreat = null)
    {
      if (!prisonzone.prisonercontainer.HasMoreThanOneTypeOfAnimal())
        return;
      for (int index1 = 0; index1 < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index1)
      {
        AnimalStat animalStat1 = AnimalData.GetAnimalStat(prisonzone.prisonercontainer.tempAnimalInfo[index1].animaltype);
        for (int index2 = 0; index2 < prisonzone.prisonercontainer.tempAnimalInfo.Count; ++index2)
        {
          if (GetThisAnimalsThreat != null && GetThisAnimalsThreat.animaltype != prisonzone.prisonercontainer.tempAnimalInfo[index1].animaltype)
          {
            AnimalStat animalStat2 = AnimalData.GetAnimalStat(prisonzone.prisonercontainer.tempAnimalInfo[index1].animaltype);
            Cohabitation_Calculator.cohabThreat_pack.ThisVictimIsACarnivore = animalStat2.diettype == DietType.Carnivore;
          }
          else if (index2 != index1)
          {
            AnimalStat animalStat2 = AnimalData.GetAnimalStat(prisonzone.prisonercontainer.tempAnimalInfo[index2].animaltype);
            if (GetThisAnimalsThreat != null)
              Cohabitation_Calculator.cohabThreat_pack.cohabitationrelationshipsJustQuerried.Add(new AnimalCohabitRelationship(prisonzone.prisonercontainer.tempAnimalInfo[index2].animaltype));
            if (animalStat2.diettype == DietType.Carnivore)
            {
              if ((double) animalStat1.Strength < (double) animalStat2.Strength)
              {
                float ThreatValue = (float) (((double) animalStat2.Strength - (double) animalStat1.Strength) * ((double) animalStat2.Aggression / 100.0) * 0.00999999977648258) * ((float) prisonzone.prisonercontainer.tempAnimalInfo[index1].AllOfThese.Count / (float) prisonzone.prisonercontainer.tempAnimalInfo[index2].AllOfThese.Count);
                NewDay_ByPen.Day_CohabitationStress += ThreatValue;
                if (GetThisAnimalsThreat != null)
                  Cohabitation_Calculator.cohabThreat_pack.cohabitationrelationshipsJustQuerried[Cohabitation_Calculator.cohabThreat_pack.cohabitationrelationshipsJustQuerried.Count - 1].SetStrongerCarnivore(ThreatValue);
                if ((double) ThreatValue < 0.0)
                  Console.WriteLine("Need to adjust Day Stats for this");
                prisonzone.prisonercontainer.tempAnimalInfo[index1].StressFromCohabitation += ThreatValue;
              }
              else if (GetThisAnimalsThreat != null)
                Cohabitation_Calculator.cohabThreat_pack.cohabitationrelationshipsJustQuerried[Cohabitation_Calculator.cohabThreat_pack.cohabitationrelationshipsJustQuerried.Count - 1].SetWeakerCarnivore();
            }
          }
        }
      }
    }
  }
}
