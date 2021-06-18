// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HungryAnimal
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir
{
  internal class HungryAnimal
  {
    private List<PrisonerInfo> REF_prisoners;
    public float TotalHunger;
    public Vector2Int GateLocation;
    public Employee Ref_EmployeeGoingHere;
    private List<AnimalType> animals;
    private List<FoodSet> foodsets;
    public int Cell_UID;

    public HungryAnimal(
      PrisonerInfo animal,
      Vector2Int _GateLocation,
      FoodSet _foodsetforthisanimal,
      int _Cell_UID)
    {
      this.Cell_UID = _Cell_UID;
      this.animals = new List<AnimalType>();
      this.animals.Add(animal.intakeperson.animaltype);
      this.GateLocation = new Vector2Int(_GateLocation);
      this.TotalHunger += animal.Hunger;
      this.REF_prisoners = new List<PrisonerInfo>();
      this.REF_prisoners.Add(animal);
      this.foodsets = new List<FoodSet>();
      this.foodsets.Add(_foodsetforthisanimal);
    }

    public int TryToFeed(
      Player player,
      float CarryCap,
      ref UsedFoodCollection AllFoodGivenToAnimals,
      AnimalFoodDistribution fooddistribution)
    {
      float percentageThroughDay = Z_GameFlags.GetPercentageThroughDay();
      CarryCap = 100f;
      int num1 = 0;
      for (int index1 = 0; index1 < this.animals.Count; ++index1)
      {
        FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(this.animals[index1]);
        float num2 = 0.0f;
        for (int index2 = 0; index2 < this.REF_prisoners.Count; ++index2)
        {
          if (this.REF_prisoners[index2].intakeperson.animaltype == this.animals[index1])
          {
            if (this.REF_prisoners[index2].GetIsABaby())
              num2 += this.REF_prisoners[index2].Hunger;
            else
              num2 += this.REF_prisoners[index2].Hunger;
          }
        }
        if ((double) num2 != 0.0)
        {
          FoodSet foodSet = this.GetFoodSet(this.animals[index1]);
          float num3 = 0.0f;
          for (int index2 = 0; index2 < foodSet.FoodUnitsPerDay.Count; ++index2)
            num3 += foodSet.FoodUnitsPerDay[index2] * num2;
          if ((double) CarryCap < (double) num3)
            num3 = CarryCap;
          float PercentDeleivered = 1f;
          for (int index2 = 0; index2 < foodCollection.animalfoodentry.Count; ++index2)
          {
            float num4 = foodSet.FoodUnitsPerDay[index2] * num2;
            if ((double) num4 > 0.0)
            {
              float num5 = player.storerooms.GetTotalStockOfThis(foodCollection.animalfoodentry[index2].foodtype);
              if ((double) num5 >= (double) num4)
                num5 = num4;
              else if ((double) num5 < (double) num4)
                PercentDeleivered -= (float) ((double) num2 / (double) num3 * ((double) num5 / (double) num4));
              player.storerooms.UseThis(foodCollection.animalfoodentry[index2].foodtype, num5);
              AllFoodGivenToAnimals.UsedThis(foodCollection.animalfoodentry[index2].foodtype, num5);
            }
          }
          for (int index2 = 0; index2 < this.REF_prisoners.Count; ++index2)
          {
            if (!this.REF_prisoners[index2].IsDead && this.REF_prisoners[index2].intakeperson.animaltype == this.animals[index1])
            {
              this.REF_prisoners[index2].Feed(PercentDeleivered, percentageThroughDay, fooddistribution);
              ++num1;
            }
          }
        }
        else
          break;
      }
      return num1;
    }

    private FoodSet GetFoodSet(AnimalType animal)
    {
      for (int index = 0; index < this.foodsets.Count; ++index)
      {
        if (this.foodsets[index].animal == animal)
          return this.foodsets[index];
      }
      return (FoodSet) null;
    }

    public void AddAnimal(PrisonerInfo animal, FoodSet _foodsetforthisanimal)
    {
      if (!this.animals.Contains(animal.intakeperson.animaltype))
        this.animals.Add(animal.intakeperson.animaltype);
      this.TotalHunger += animal.Hunger;
      this.REF_prisoners.Add(animal);
      this.AddFoodSet(_foodsetforthisanimal);
    }

    public void AddFoodSet(FoodSet _foodsetforthisanimal)
    {
      for (int index = 0; index < this.foodsets.Count; ++index)
      {
        if (this.foodsets[index].animal == _foodsetforthisanimal.animal)
        {
          this.foodsets[index] = _foodsetforthisanimal;
          return;
        }
      }
      this.foodsets.Add(_foodsetforthisanimal);
    }

    internal static int SortHungryAnimal(HungryAnimal a, HungryAnimal b)
    {
      if ((double) a.TotalHunger > (double) b.TotalHunger)
        return -1;
      return (double) a.TotalHunger < (double) b.TotalHunger ? 1 : 0;
    }
  }
}
