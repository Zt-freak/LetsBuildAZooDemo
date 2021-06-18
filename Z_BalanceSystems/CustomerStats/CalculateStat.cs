// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.CustomerStats.CalculateStat
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BalanceSystems.CustomerStats
{
  internal class CalculateStat
  {
    internal static bool RecalculateCash = false;
    internal static int TotalCash = 0;
    internal static int TotalPeople = 0;
    internal static int AverageCash;
    private static AnimalMapper[] animalmaps;
    internal static bool RebuildAnimalMap = true;
    private static int TotalLivingAnimalsInZoo = -1;
    private static int TotalDeadAnimalsInZoo = -1;
    internal static float BaseAnimalSatisfaction;

    public static int GetAverageCashHeld()
    {
      if (CalculateStat.RecalculateCash)
      {
        CalculateStat.TotalCash = 0;
        CalculateStat.TotalPeople = 0;
        List<WalkingPerson> listOfWalkingPeople = CustomerManager.GetListOfWalkingPeople();
        for (int index = 0; index < listOfWalkingPeople.Count; ++index)
        {
          if (listOfWalkingPeople[index].simperson.roleinsociety == RoleInSociety.Customer && listOfWalkingPeople[index].simperson.customertype == CustomerType.Normal && !listOfWalkingPeople[index].simperson.memberofthepublic.IsAtBusWaiting)
          {
            ++CalculateStat.TotalPeople;
            CalculateStat.TotalCash += listOfWalkingPeople[index].simperson.memberofthepublic.CashHeld;
          }
        }
        CalculateStat.AverageCash = (int) ((double) CalculateStat.TotalCash / (double) CalculateStat.TotalPeople);
      }
      return CalculateStat.AverageCash;
    }

    internal static SatisfactionType GetHighestFrustration()
    {
      int[] numArray = new int[6];
      List<WalkingPerson> listOfWalkingPeople = CustomerManager.GetListOfWalkingPeople();
      for (int index = 0; index < listOfWalkingPeople.Count; ++index)
      {
        if (!listOfWalkingPeople[index].simperson.IsDead && listOfWalkingPeople[index].simperson.roleinsociety == RoleInSociety.Customer && (listOfWalkingPeople[index].simperson.customertype == CustomerType.Normal && !listOfWalkingPeople[index].simperson.memberofthepublic.IsAtBusWaiting))
        {
          SatisfactionType frustratedNeedsIfAny = listOfWalkingPeople[index].simperson.memberofthepublic.customerneeds.GetMostFrustratedNeedsIfAny(out float _);
          if (frustratedNeedsIfAny != SatisfactionType.Count)
            ++numArray[(int) frustratedNeedsIfAny];
        }
      }
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] > num2)
        {
          num2 = numArray[index];
          num1 = index;
        }
      }
      return num1 > -1 ? (SatisfactionType) num1 : SatisfactionType.Count;
    }

    internal static int GetCellUIDWithThisAnimal(Player player, AnimalType thisanimal)
    {
      if (CalculateStat.animalmaps == null || CalculateStat.RebuildAnimalMap || CalculateStat.TotalLivingAnimalsInZoo == -1)
      {
        CalculateStat.TotalLivingAnimalsInZoo = 0;
        CalculateStat.TotalDeadAnimalsInZoo = 0;
        CalculateStat.RebuildAnimalMap = false;
        CalculateStat.animalmaps = new AnimalMapper[56];
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
          {
            if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].IsDead)
              ++CalculateStat.TotalDeadAnimalsInZoo;
            else
              ++CalculateStat.TotalLivingAnimalsInZoo;
            int animaltype = (int) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype;
            if (CalculateStat.animalmaps[animaltype] == null)
              CalculateStat.animalmaps[animaltype] = new AnimalMapper();
            CalculateStat.animalmaps[animaltype].AddAnimal(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID);
          }
        }
        float totalLandUnlocked = (float) PlayerStats.GetTotalLandUnlocked();
        CalculateStat.BaseAnimalSatisfaction = MathHelper.Clamp((float) (((double) CalculateStat.TotalDeadAnimalsInZoo + (double) CalculateStat.TotalLivingAnimalsInZoo) / ((double) totalLandUnlocked * 15.0)), 0.0f, 1f);
      }
      return CalculateStat.animalmaps[(int) thisanimal] != null ? CalculateStat.animalmaps[(int) thisanimal].GetCellUID() : -1;
    }

    internal static float GetAnimalTotalSatisfaction()
    {
      if (CalculateStat.TotalLivingAnimalsInZoo == -1)
        throw new Exception("CALCULATE THE OTHER THING FIRST");
      return CalculateStat.BaseAnimalSatisfaction;
    }

    internal static CustomerQuest GetMostInProgressQuest()
    {
      int[] numArray = new int[12];
      List<WalkingPerson> listOfWalkingPeople = CustomerManager.GetListOfWalkingPeople();
      for (int index = 0; index < listOfWalkingPeople.Count; ++index)
      {
        if (!listOfWalkingPeople[index].simperson.IsDead && listOfWalkingPeople[index].simperson.roleinsociety == RoleInSociety.Customer && (listOfWalkingPeople[index].simperson.customertype == CustomerType.Normal && !listOfWalkingPeople[index].simperson.memberofthepublic.IsAtBusWaiting))
          ++numArray[(int) listOfWalkingPeople[index].simperson.memberofthepublic.CurrentQuest];
      }
      int num1 = -1;
      int num2 = 0;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] > num2)
        {
          num2 = numArray[index];
          num1 = index;
        }
      }
      return num1 > -1 ? (CustomerQuest) num1 : CustomerQuest.Count;
    }
  }
}
