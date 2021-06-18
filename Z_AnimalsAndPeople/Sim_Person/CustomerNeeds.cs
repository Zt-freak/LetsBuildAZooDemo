// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.CustomerNeeds
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_BalanceSystems.CustomerStats;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers.PeopleAttachments;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class CustomerNeeds
  {
    public float[] CurrentWantValues;
    public float[] Multipliers;
    public float Fitness;
    private float EnergyPerTile;
    private float PersonsLoveForLotsOfAnimals;
    private float PersonsLoveForFavouriteAnimal;
    private float PersonHorrorForFilfthAndDeath;
    public float PersonHorrorForPoop;
    public float PersonHorrorForDeath;
    public int MaxVistsToATM;
    public int WillLookForATMWhenLessThanThis;
    public int TotalTilesWalks;

    public CustomerNeeds(bool HasFavouriteAnimal, bool SkipAnimalCalculations)
    {
      this.CurrentWantValues = new float[19];
      this.Multipliers = new float[19];
      this.Multipliers[3] = MathStuff.getRandomFloat(Z_GameFlags.SecondsInDay * 0.3f, Z_GameFlags.SecondsInDay * 1.3f) / Z_GameFlags.SecondsInDay;
      this.Multipliers[3] /= Z_GameFlags.SecondsInDay;
      this.Multipliers[1] = MathStuff.getRandomFloat(Z_GameFlags.SecondsInDay * 0.3f, Z_GameFlags.SecondsInDay * 1.3f) / Z_GameFlags.SecondsInDay;
      this.Multipliers[1] /= Z_GameFlags.SecondsInDay;
      this.Multipliers[2] = MathStuff.getRandomFloat(Z_GameFlags.SecondsInDay * 0.3f, Z_GameFlags.SecondsInDay * 1.3f) / Z_GameFlags.SecondsInDay;
      this.Multipliers[2] /= Z_GameFlags.SecondsInDay;
      this.Multipliers[17] = MathStuff.getRandomFloat(Z_GameFlags.SecondsInDay * 0.8f, Z_GameFlags.SecondsInDay * 1.3f) / Z_GameFlags.SecondsInDay;
      this.Multipliers[17] /= Z_GameFlags.SecondsInDay;
      this.Multipliers[13] = MathStuff.getRandomFloat(0.5f, 1.2f);
      this.CurrentWantValues[10] = MathStuff.getRandomFloat(Z_GameFlags.SecondsInDay * 0.3f, Z_GameFlags.SecondsInDay * 0.6f) / Z_GameFlags.SecondsInDay;
      this.Multipliers[10] /= Z_GameFlags.SecondsInDay;
      if (Player.financialrecords.GetDaysPassed() < 3L)
      {
        this.CurrentWantValues[2] = MathStuff.getRandomFloat(0.0f, 0.3f);
        this.CurrentWantValues[1] = MathStuff.getRandomFloat(0.0f, 0.3f);
        this.CurrentWantValues[3] = MathStuff.getRandomFloat(0.0f, 0.3f);
      }
      else if (Player.financialrecords.GetDaysPassed() < 6L)
      {
        this.CurrentWantValues[2] = MathStuff.getRandomFloat(0.0f, 0.9f);
        this.CurrentWantValues[1] = MathStuff.getRandomFloat(0.0f, 0.9f);
        this.CurrentWantValues[3] = MathStuff.getRandomFloat(0.0f, 0.3f);
      }
      else
      {
        this.CurrentWantValues[2] = MathStuff.getRandomFloat(0.0f, 0.9f);
        this.CurrentWantValues[1] = MathStuff.getRandomFloat(0.0f, 0.9f);
        this.CurrentWantValues[3] = MathStuff.getRandomFloat(0.0f, 0.9f);
      }
      this.CurrentWantValues[0] = 1f;
      this.WillLookForATMWhenLessThanThis = TinyZoo.Game1.Rnd.Next(0, 5000);
      this.MaxVistsToATM = TinyZoo.Game1.Rnd.Next(0, 4);
      this.Multipliers[7] = 1f;
      this.Fitness = MathStuff.getRandomFloat(0.0f, 1f);
      this.EnergyPerTile = (float) (0.00499999988824129 + (1.0 - (double) this.Fitness) * 0.0149999996647239);
      if (Player.currentActiveResearchBonuses.TypesOfUpgradeAndLevel[4] > 0)
        this.EnergyPerTile *= 0.8f;
      this.PersonHorrorForDeath = (float) TinyZoo.Game1.Rnd.Next(0, 100);
      this.PersonHorrorForPoop = (float) TinyZoo.Game1.Rnd.Next(0, 100);
      this.PersonHorrorForPoop *= 0.01f;
      this.PersonHorrorForDeath *= 0.01f;
      this.CurrentWantValues[9] = (float) TinyZoo.Game1.Rnd.Next(40, 60);
      if (SkipAnimalCalculations)
        return;
      this.PersonsLoveForLotsOfAnimals = (float) TinyZoo.Game1.Rnd.Next(20, 40) * 0.01f;
      this.PersonsLoveForFavouriteAnimal = (float) TinyZoo.Game1.Rnd.Next(15, 30) * 0.01f;
      this.PersonHorrorForFilfthAndDeath = (float) TinyZoo.Game1.Rnd.Next(30, 110) * 0.01f;
      float num = 1f;
      if (!HasFavouriteAnimal)
        num -= this.PersonsLoveForFavouriteAnimal;
      this.CurrentWantValues[4] = num - (1f - CalculateStat.GetAnimalTotalSatisfaction()) * this.PersonsLoveForLotsOfAnimals;
    }

    public void WalkedUsedEnergy(int TileWalked)
    {
      this.TotalTilesWalks += TileWalked;
      if ((double) this.CurrentWantValues[17] > 0.0)
        return;
      this.CurrentWantValues[0] -= (float) TileWalked * this.EnergyPerTile;
    }

    public bool VisitedPen(float WelfareAndCleanliness)
    {
      this.CurrentWantValues[4] -= (1f - WelfareAndCleanliness) * this.PersonHorrorForFilfthAndDeath;
      if ((double) this.CurrentWantValues[4] > (double) MemberOfThePublic.WelfareLeaveParkThreshold)
        return false;
      if ((double) this.CurrentWantValues[4] < 0.0)
        this.CurrentWantValues[4] = 0.0f;
      return true;
    }

    public SatisfactionType GetMostFrustratedNeedsIfAny(out float lowestValue)
    {
      float num = 0.5f;
      lowestValue = 1f;
      SatisfactionType satisfactionType = SatisfactionType.Count;
      for (int index = 0; index < 6; ++index)
      {
        if ((double) this.CurrentWantValues[index] < (double) lowestValue)
        {
          lowestValue = this.CurrentWantValues[index];
          satisfactionType = (SatisfactionType) index;
        }
      }
      return (double) lowestValue < (double) num ? satisfactionType : SatisfactionType.Count;
    }

    public void AddThought(ThoughtType thoughttype, float DisgustValue)
    {
      if (thoughttype != ThoughtType.Death)
      {
        if (thoughttype != ThoughtType.Poop)
          return;
        this.CurrentWantValues[15] += DisgustValue;
      }
      else
        this.CurrentWantValues[16] += DisgustValue;
    }

    public string GetThought()
    {
      bool flag1 = (double) this.CurrentWantValues[3] > 0.600000023841858;
      bool flag2 = (double) this.CurrentWantValues[2] > 0.600000023841858;
      bool flag3 = (double) this.CurrentWantValues[1] > 0.600000023841858;
      bool flag4 = (double) this.CurrentWantValues[10] > 0.600000023841858;
      bool flag5 = (double) this.CurrentWantValues[9] > 0.600000023841858;
      bool flag6 = (double) this.CurrentWantValues[0] < 0.400000005960464;
      int maxValue = 0;
      if (flag1)
        ++maxValue;
      if (flag2)
        ++maxValue;
      if (flag3)
        ++maxValue;
      if (flag4)
        ++maxValue;
      if (flag5)
        ++maxValue;
      if (flag6)
        ++maxValue;
      if (maxValue > 0)
      {
        int num1 = TinyZoo.Game1.Rnd.Next(0, maxValue);
        if (flag1)
        {
          if (num1 <= 0)
            return "I am having fun, but I think I need to take a moment to powder my nose!";
          --num1;
        }
        if (flag2)
        {
          if (num1 <= 0)
            return "I am really thirsty.";
          --num1;
        }
        if (flag3)
        {
          if (num1 <= 0)
            return "I am really hungry!";
          --num1;
        }
        if (flag4)
        {
          if (num1 <= 0)
            return "I think I must have eaten something spicy, my mouth is on fire!";
          --num1;
        }
        if (flag5)
        {
          if (num1 <= 0)
            return "I am really not having much fun! This day out isn't really turning out as I expected.";
          --num1;
        }
        if (flag6)
        {
          if (num1 <= 0)
            return "All this walking is tiring me out!";
          int num2 = num1 - 1;
        }
      }
      switch (TinyZoo.Game1.Rnd.Next(0, 5))
      {
        case 0:
          return "All this walking is making me hungry, now where can I get a good burger?";
        case 1:
          return "I wish there were a wider variety of animals to see. Once you have seen the rabbits, you have seen them all!";
        case 2:
          return "I want some souvenirs to take home, I want to remember this trip for ever!";
        case 3:
          return "This park is a lot smaller than I imagined.";
        default:
          return "I know this place is full of nature, but - I don't see anywhere that allows me to err... answer the call of nature, if you know what I mean?";
      }
    }

    public void UpdateNeedsAndWants(float Cycles, WalkingPerson parent)
    {
      this.CurrentWantValues[3] += Cycles * this.Multipliers[3];
      this.CurrentWantValues[1] += Cycles * this.Multipliers[1];
      this.CurrentWantValues[2] += Cycles * this.Multipliers[2];
      if ((double) this.CurrentWantValues[17] > 0.0)
      {
        if (parent.statusicon.statusicontype != StatusIconType.Caffiene)
          parent.statusicon.SetStatucIconType(StatusIconType.Caffiene);
        this.CurrentWantValues[17] -= Cycles * 0.0005f;
        if ((double) this.CurrentWantValues[17] <= 0.0)
        {
          this.CurrentWantValues[17] = 0.0f;
          parent.statusicon.SetStatucIconType(StatusIconType.None);
        }
      }
      if ((double) this.CurrentWantValues[3] > 1.0)
        this.CurrentWantValues[3] = 1f;
      if ((double) this.CurrentWantValues[1] > 1.0)
        this.CurrentWantValues[1] = 1f;
      if ((double) this.CurrentWantValues[2] > 1.0)
        this.CurrentWantValues[2] = 1f;
      if ((double) this.CurrentWantValues[10] > 1.0)
        this.CurrentWantValues[10] = 1f;
      if ((double) this.CurrentWantValues[10] > 0.300000011920929)
        this.CurrentWantValues[10] -= Cycles * this.Multipliers[10];
      float num = 0.0f;
      if ((double) this.CurrentWantValues[0] < 0.300000011920929)
        num += Cycles * (0.3f - this.CurrentWantValues[0]);
      if ((double) this.CurrentWantValues[1] > 0.600000023841858)
        num += Cycles * (this.CurrentWantValues[1] - 0.6f);
      if ((double) this.CurrentWantValues[3] > 0.600000023841858)
        num += Cycles * (this.CurrentWantValues[1] - 0.6f);
      if ((double) this.CurrentWantValues[2] > 0.600000023841858)
        num += Cycles * (this.CurrentWantValues[1] - 0.6f);
      if ((double) num <= 0.0)
        return;
      this.CurrentWantValues[9] -= num * 2f;
    }
  }
}
