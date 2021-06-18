// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Animals.TempAnimalInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BalanceSystems.Animals
{
  internal class TempAnimalInfo
  {
    public AnimalType animaltype;
    public List<int> AllOfThese;
    private int OldAnimals;
    private int Babies;
    public int Corpses;
    public int CollectiveCorpseAge;
    public float StressFromCohabitation;
    public int GroupSizeLoneliness;
    public int LargeGroupStress;
    public float AnimalHabitatMatch;
    public float TotalEnrichmentValue;
    public float RequiredEnrichment;
    public AnimalFoodType CriticalFood;
    public List<FoodTemp> EatsThis;
    public AnimalType animalHead;
    public int HeadVariant;
    public int BodyVariant;
    public float EnrichmentFullfillment;
    public float DaysOfFood;

    public TempAnimalInfo(
      AnimalType _animaltype,
      int Index,
      PrisonerInfo prisonerinfo,
      CellBlockType cellblocktype,
      int CellUID,
      AnimalType _animalHead,
      int _HeadVariant,
      int _BodyVariant)
    {
      this.BodyVariant = _BodyVariant;
      this.HeadVariant = _HeadVariant;
      this.animalHead = _animalHead;
      this.animaltype = _animaltype;
      this.AllOfThese = new List<int>();
      this.AddAnimal(prisonerinfo, Index, CellUID);
      this.AnimalHabitatMatch = prisonerinfo.GetHabitatMatch(cellblocktype);
    }

    public float GetFoodUnitsMultiplier() => (float) (this.AllOfThese.Count - this.Corpses) - (float) this.Babies * 0.5f;

    public void AddAnimal(PrisonerInfo prisonerinfo, int Index, int CellUID)
    {
      if (prisonerinfo.IsDead)
      {
        CurrentDeadAnimals.AddDeadAnimal(CellUID);
        ++this.Corpses;
        this.CollectiveCorpseAge += prisonerinfo.DaysSinceDeath;
      }
      else if (prisonerinfo.GetIsABaby())
        ++this.Babies;
      else if (prisonerinfo.GetIsOld())
        ++this.OldAnimals;
      if (!prisonerinfo.IsDead)
      {
        this.RequiredEnrichment += 1f - AnimalFoodData.GetHardyness(prisonerinfo.intakeperson.animaltype);
        this.RequiredEnrichment += 0.25f;
      }
      this.AllOfThese.Add(Index);
    }

    public void CalculateDaysOfFood(float[] DaysOfStocks)
    {
      this.DaysOfFood = -1f;
      this.CriticalFood = AnimalFoodType.Count;
      for (int index = 0; index < this.EatsThis.Count; ++index)
      {
        if ((double) this.EatsThis[index].TotalUnitsPerDay > 0.0)
        {
          if ((double) this.DaysOfFood == -1.0)
          {
            this.DaysOfFood = DaysOfStocks[(int) this.EatsThis[index].EatsThis];
            this.CriticalFood = this.EatsThis[index].EatsThis;
          }
          else
          {
            this.DaysOfFood = Math.Min(DaysOfStocks[(int) this.EatsThis[index].EatsThis], this.DaysOfFood);
            this.CriticalFood = this.EatsThis[index].EatsThis;
          }
        }
      }
      if ((double) this.DaysOfFood != -1.0)
        return;
      this.DaysOfFood = 0.0f;
    }
  }
}
