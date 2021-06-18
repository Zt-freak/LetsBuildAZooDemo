// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.PrisonerInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_Diseases;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.Layout
{
  internal class PrisonerInfo
  {
    public bool DONOTRESETDATA_MovedAnimal;
    public IntakePerson intakeperson;
    public Vector2 Location;
    public bool _IsDead;
    public CauseOfDeath causeofdeath;
    public int DaysSinceDeath;
    public bool IsPregnant;
    public bool IsOnContraceptive;
    public AnimalHappyness animalhappyness;
    public float Fertility = 0.75f;
    public int ImmuneSystem;
    public bool IsPainted;
    public int Age;
    private int AgeWhenGot;
    public float Hunger;
    public float LastFed;
    public int DaysWithoutFood;
    public int DaysWithoutWater;
    public float LifeExpetancy;
    public int DaysAsABaby;
    public bool WillDieOfHunger;
    public bool WillDieOfThirst;
    public bool WillDieOfOldAge;
    public bool IsFertile;
    public int MotherUID = -1;
    public int FatherUID = -1;
    public List<int> Children_UID;
    public bool JustFed;
    public float TEMP_HabitatMultiplers = -1f;
    public float HygieneValue = 1f;
    public float NutritionValue = 1f;
    public float HydrationValue = 1f;
    public float WeightValue = 1f;
    public float PoopNeed;
    private bool IsSick;
    public int SicknessUID;
    public bool SicknessHasBeeDiagnosed;
    public bool WillDieFromSickness;
    public int SicknessGestationRemaining_quarterDays;
    public int SicknessTimeRemaining_quarterDays;
    public int SicknessTimeTODEATHRemaining_quarterDays;
    public int Sickness_TimeUntilRollToInfectOtherAnimals;
    public int TEMP_SicknessFullRollDuration = -1;
    public float FightDesire;
    public float Appetite = 1f;
    public float Sleepyness;
    public float EnrichmentValue = 2f;
    public bool IsCurrentlyFighting;
    public bool IsCurrentlyBrokenOut;
    public Vector2 TempOverrideSpawnLocation;
    public bool UseTempOverrideLocation;

    public PrisonerInfo(
      IntakePerson _intakeperson,
      bool _IsDead,
      Vector2 _Location,
      CellBlockType cellblocktype,
      Vector2 SpawnOverrideLocation)
    {
      this.PoopNeed = (float) TinyZoo.Game1.Rnd.Next(0, 70);
      this.PoopNeed *= 0.01f;
      this.Location = SpawnOverrideLocation;
      this.TempOverrideSpawnLocation = SpawnOverrideLocation;
      this.UseTempOverrideLocation = true;
      this.SetPrisonerInfo(_intakeperson, _IsDead, _Location, cellblocktype);
      this.causeofdeath = CauseOfDeath.Count;
    }

    public float GetHydration() => this.HydrationValue;

    public float GetHygiene() => this.HygieneValue;

    public float GetHabitatMatch(CellBlockType celltype)
    {
      if ((double) this.TEMP_HabitatMultiplers == -1.0)
      {
        this.TEMP_HabitatMultiplers = this.SetHabitatMatch(celltype);
        this.TEMP_HabitatMultiplers *= 0.01f;
      }
      return this.TEMP_HabitatMultiplers;
    }

    public float GetNutritionFromDiet(AnimalFoodDistribution FoodForAnimals) => FoodForAnimals.GetNutrition(this.intakeperson.animaltype);

    public void ResetOnMoveCell(CellBlockType celltype)
    {
      double habitatMatch = (double) this.GetHabitatMatch(celltype);
    }

    public float GetWeightValue() => this.WeightValue;

    public float GetWeightValueForBar() => this.WeightValue * 0.5f;

    public AnimalType GetAnimalPainted() => !this.IsPainted ? this.intakeperson.animaltype : AnimalData.GetThisPaintedAnimalOther(this.intakeperson.animaltype);

    public float GetCurrentWeightInKG()
    {
      float animalWeight = (float) AnimalData.GetAnimalWeight(this.intakeperson.animaltype);
      if (!this.GetIsABaby())
        return animalWeight * this.WeightValue;
      float throughChildhood = this.GetPercentThroughChildhood();
      return (float) ((double) animalWeight * 0.100000001490116 + (double) animalWeight * 0.899999976158142 * (double) throughChildhood) * this.WeightValue;
    }

    public float GetNutrition() => this.NutritionValue;

    public float GetEnrichment() => MathHelper.Clamp(this.EnrichmentValue, 0.0f, 1f);

    public void DoVetCheck(Player player)
    {
      if (!this.IsSick || this.SicknessHasBeeDiagnosed)
        return;
      this.SicknessHasBeeDiagnosed = true;
      Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalSick, this.intakeperson.UID)
      {
        Animal = this.intakeperson.animaltype
      }, player);
      for (int index = 0; index < player.Stats.ActiveDiseases.Count; ++index)
      {
        if (player.Stats.ActiveDiseases[index].UID == this.SicknessUID && !player.Stats.ActiveDiseases[index].IsResearched)
          player.Stats.ActiveDiseases[index].diseaseresearch.HasBeenDiscovered = true;
      }
    }

    private float SetHabitatMatch(CellBlockType cellblocktype)
    {
      float num = 0.0f;
      switch (cellblocktype)
      {
        case CellBlockType.Grasslands:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_GrassLand;
          break;
        case CellBlockType.Forest:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Forest;
          break;
        case CellBlockType.Savannah:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Savannah;
          break;
        case CellBlockType.Desert:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Desert;
          break;
        case CellBlockType.Mountain:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Mountain;
          break;
        case CellBlockType.Arctic:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Arctic;
          break;
        case CellBlockType.Tropical:
          num = (float) AnimalData.GetAnimalStat(this.intakeperson.animaltype).Compatibility_Tropical;
          break;
      }
      return num;
    }

    public PrisonerInfo(
      IntakePerson _intakeperson,
      bool _IsDead,
      Vector2 _Location,
      CellBlockType cellblocktype)
    {
      this.SetPrisonerInfo(_intakeperson, _IsDead, _Location, cellblocktype);
    }

    public bool IsDead
    {
      get => this._IsDead;
      set => this._IsDead = value;
    }

    private void SetPrisonerInfo(
      IntakePerson _intakeperson,
      bool _IsDead,
      Vector2 _Location,
      CellBlockType cellblocktype)
    {
      this.ImmuneSystem = 100;
      this.PoopNeed = (float) TinyZoo.Game1.Rnd.Next(0, 70);
      this.PoopNeed *= 0.01f;
      this.animalhappyness = new AnimalHappyness();
      this.IsFertile = true;
      this.Children_UID = new List<int>();
      _intakeperson.UID = PlayerStats.Z_AnimalUID;
      ++PlayerStats.Z_AnimalUID;
      this.intakeperson = _intakeperson;
      this.FinishedMap(_IsDead, _Location);
      this.LifeExpetancy = AnimalData.GetLifeExectancy(_intakeperson.animaltype, _intakeperson.IsAGirl, out this.DaysAsABaby);
      this.TEMP_HabitatMultiplers = this.GetHabitatMatch(cellblocktype);
    }

    public void MakeSick(int _SicknessUID)
    {
      this.IsSick = true;
      this.SicknessUID = _SicknessUID;
    }

    public bool TryToInfect(Disease disease)
    {
      int immuneSystem = this.ImmuneSystem;
      if (this.GetIsOld())
        immuneSystem /= 2;
      if (this.IsSick || TinyZoo.Game1.Rnd.Next(0, immuneSystem) >= disease.ProbabilityOfInfection)
        return false;
      this.IsSick = true;
      this.TEMP_SicknessFullRollDuration = disease.RollRegularityPerAnimal;
      this.SicknessUID = disease.UID;
      this.WillDieFromSickness = TinyZoo.Game1.Rnd.Next(0, 100) < disease.MortallityRate;
      this.SicknessGestationRemaining_quarterDays = TinyZoo.Game1.Rnd.Next(disease.IncubationPeriodMin_QuarterDays, disease.IncubationPeriodMax);
      this.SicknessTimeRemaining_quarterDays = TinyZoo.Game1.Rnd.Next(disease.DaysToHealMinQuarterDay, disease.DaysToHealMax);
      this.SicknessTimeTODEATHRemaining_quarterDays = TinyZoo.Game1.Rnd.Next(disease.DaysToDeathMinInQuarterDays, disease.DaysToDeathMax);
      if (this.WillDieFromSickness && this.SicknessTimeTODEATHRemaining_quarterDays > this.SicknessTimeRemaining_quarterDays)
        this.SicknessTimeTODEATHRemaining_quarterDays = this.SicknessTimeRemaining_quarterDays;
      this.Sickness_TimeUntilRollToInfectOtherAnimals = TinyZoo.Game1.Rnd.Next(disease.RollRegularityPerAnimal / 2, disease.RollRegularityPerAnimal);
      return true;
    }

    public void Cure() => this.IsSick = false;

    public bool GetIsSick() => this.IsSick;

    public int GetimeInZoo() => this.Age - this.AgeWhenGot;

    public void SetAgeOnGet(int _Age)
    {
      this.Age = _Age;
      this.AgeWhenGot = this.Age;
    }

    public bool GetCanHaveBaby() => this.Age >= this.DaysAsABaby && this.IsFertile;

    public void Feed(
      float PercentDeleivered,
      float PercentOfDayPassed,
      AnimalFoodDistribution fooddistribution)
    {
      if ((double) PercentDeleivered <= 0.0)
        return;
      this.DaysWithoutFood = 0;
      this.LastFed = PercentOfDayPassed;
      float num = this.GetNutritionFromDiet(fooddistribution) + PercentDeleivered;
      if ((double) num > 0.899999976158142)
      {
        if ((double) this.NutritionValue < 1.0)
          this.NutritionValue += num * 0.02f;
        else
          this.NutritionValue += num * (3f / 500f);
      }
      else
        this.NutritionValue -= (float) ((1.0 - (double) num) * 0.0199999995529652);
      if ((double) PercentDeleivered >= 1.0)
        Z_NotificationManager.JustFedThis(this.intakeperson.UID);
      if (this.GetIsABaby())
        this.PoopNeed += PercentDeleivered * 0.2f;
      else
        this.PoopNeed += PercentDeleivered * 0.34f;
      if (Z_DebugFlags.LotsOfPoop)
        ++this.PoopNeed;
      if (Z_DebugFlags.IsBetaVersion)
        this.PoopNeed = 0.0f;
      if ((double) PercentDeleivered > 1.0)
        this.WeightValue += (float) (((double) PercentDeleivered - 1.0) * 0.100000001490116);
      else if ((double) PercentDeleivered < 1.0)
      {
        this.Hunger *= 1f - PercentDeleivered;
        this.animalhappyness.AtePartialMeal(PercentDeleivered);
        this.WeightValue -= (float) ((1.0 - (double) PercentDeleivered) * 0.0299999993294477);
      }
      else
      {
        this.Hunger = 0.0f;
        this.animalhappyness.AteFullMeal();
      }
      this.JustFed = true;
    }

    public bool GetIsABaby() => this.Age < this.DaysAsABaby;

    public float GetPercentThroughAdulthood()
    {
      float num = (float) (this.Age - this.DaysAsABaby) / (this.LifeExpetancy * this.Fertility - (float) this.DaysAsABaby);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 1.0)
        num = 1f;
      return num;
    }

    public bool GetIsOld() => !this.IsFertile && !this.GetIsABaby();

    public float GetPercentThroughOldAge()
    {
      float num = (float) (((double) this.Age / (double) this.LifeExpetancy - (double) this.Fertility) * 4.0);
      if ((double) num < 0.0)
        num = 0.0f;
      if ((double) num > 1.0)
        num = 1f;
      return num;
    }

    public float GetPercentThroughChildhood() => this.Age < this.DaysAsABaby ? (float) this.Age / (float) this.DaysAsABaby : 1f;

    public float GetHealthWellBeing() => 1f;

    public Vector2 GetLocation()
    {
      if (!this.UseTempOverrideLocation)
        return this.Location * Sengine.ScreenRatioUpwardsMultiplier;
      this.UseTempOverrideLocation = false;
      return this.TempOverrideSpawnLocation;
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus) => this.intakeperson.SetConsumption(consumptionstatus);

    public void FinishedMap(bool _IsDead, Vector2 Vlocation)
    {
    }

    public void SavePrisonerInfo(Writer writer)
    {
      this.intakeperson.SaveIntakePerson(writer);
      writer.WriteFloat("p", this.Location.X);
      writer.WriteFloat("p", this.Location.Y);
      int num = this._IsDead ? 1 : 0;
      writer.WriteBool("p", this._IsDead);
      writer.WriteInt("p", this.DaysSinceDeath);
      writer.WriteInt("p", this.AgeWhenGot);
      writer.WriteBool("p", this.IsPregnant);
      writer.WriteBool("p", this.IsOnContraceptive);
      writer.WriteInt("p", this.DaysAsABaby);
      writer.WriteInt("p", this.MotherUID);
      writer.WriteInt("p", this.FatherUID);
      writer.WriteInt("p", this.Age);
      writer.WriteInt("p", this.Children_UID.Count);
      for (int index = 0; index < this.Children_UID.Count; ++index)
        writer.WriteInt("p", this.Children_UID[index]);
      this.animalhappyness.SaveAnimalHappyness(writer);
      writer.WriteBool("p", this.IsFertile);
      writer.WriteInt("p", this.DaysWithoutWater);
      writer.WriteFloat("p", this.LifeExpetancy);
      writer.WriteBool("p", this.SicknessHasBeeDiagnosed);
      writer.WriteFloat("p", this.HygieneValue);
      writer.WriteFloat("p", this.NutritionValue);
      writer.WriteFloat("p", this.HydrationValue);
      writer.WriteFloat("p", this.WeightValue);
      writer.WriteInt("p", this.DaysWithoutFood);
      writer.WriteBool("p", this.IsPainted);
      writer.WriteInt("p", (int) this.causeofdeath);
      writer.WriteFloat("p", this.PoopNeed);
    }

    public PrisonerInfo(Reader reader, int VersionForLoad)
    {
      this.TEMP_HabitatMultiplers = -1f;
      this.intakeperson = new IntakePerson(reader);
      float _out1 = 0.0f;
      float _out2 = 0.0f;
      int num1 = (int) reader.ReadFloat("p", ref _out1);
      int num2 = (int) reader.ReadFloat("p", ref _out2);
      this.Location = new Vector2(_out1, _out2);
      int num3 = (int) reader.ReadBool("p", ref this._IsDead);
      int num4 = (int) reader.ReadInt("p", ref this.DaysSinceDeath);
      int num5 = (int) reader.ReadInt("p", ref this.AgeWhenGot);
      int num6 = (int) reader.ReadBool("p", ref this.IsPregnant);
      int num7 = (int) reader.ReadBool("p", ref this.IsOnContraceptive);
      int num8 = (int) reader.ReadInt("p", ref this.DaysAsABaby);
      int num9 = (int) reader.ReadInt("p", ref this.MotherUID);
      int num10 = (int) reader.ReadInt("p", ref this.FatherUID);
      int num11 = (int) reader.ReadInt("p", ref this.Age);
      int _out3 = 0;
      int num12 = (int) reader.ReadInt("p", ref _out3);
      this.Children_UID = new List<int>();
      for (int index = 0; index < _out3; ++index)
      {
        int _out4 = 0;
        int num13 = (int) reader.ReadInt("p", ref _out4);
        this.Children_UID.Add(_out4);
      }
      this.animalhappyness = new AnimalHappyness(reader);
      int num14 = (int) reader.ReadBool("p", ref this.IsFertile);
      int num15 = (int) reader.ReadInt("p", ref this.DaysWithoutWater);
      int num16 = (int) reader.ReadFloat("p", ref this.LifeExpetancy);
      if (VersionForLoad > 4)
      {
        int num17 = (int) reader.ReadBool("p", ref this.SicknessHasBeeDiagnosed);
      }
      if (VersionForLoad > 20)
      {
        int num13 = (int) reader.ReadFloat("p", ref this.HygieneValue);
        int num18 = (int) reader.ReadFloat("p", ref this.NutritionValue);
        int num19 = (int) reader.ReadFloat("p", ref this.HydrationValue);
        int num20 = (int) reader.ReadFloat("p", ref this.WeightValue);
      }
      else
      {
        this.HygieneValue = 1f;
        this.NutritionValue = 1f;
        this.HydrationValue = 1f;
        this.WeightValue = 1f;
      }
      if (VersionForLoad > 21)
      {
        int num21 = (int) reader.ReadInt("p", ref this.DaysWithoutFood);
      }
      if (VersionForLoad > 23)
      {
        int num22 = (int) reader.ReadBool("p", ref this.IsPainted);
      }
      if (VersionForLoad > 29)
      {
        int num13 = (int) reader.ReadInt("p", ref _out3);
        this.causeofdeath = (CauseOfDeath) _out3;
      }
      if (VersionForLoad <= 31)
        return;
      int num23 = (int) reader.ReadFloat("p", ref this.PoopNeed);
    }
  }
}
