// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.AnimalFoodDistribution
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.Layout.CellBlocks
{
  internal class AnimalFoodDistribution
  {
    public List<TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet> FoodSet;

    public AnimalFoodDistribution() => this.FoodSet = new List<TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet>();

    public float GetNutrition(AnimalType animal)
    {
      TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet thisSet = this.GetThisSet(animal);
      FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(animal);
      float num1 = 0.0f;
      for (int index = 0; index < thisSet.FoodUnitsPerDay.Count; ++index)
      {
        float num2 = Math.Min(thisSet.FoodUnitsPerDay[index] / foodCollection.FullDailyFoodRquirement / foodCollection.animalfoodentry[index].PercentOfDailyIdeal, 1f);
        num1 += num2 * foodCollection.animalfoodentry[index].NutritionValue;
      }
      return num1;
    }

    public TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet GetThisSet(AnimalType animal)
    {
      for (int index = 0; index < this.FoodSet.Count; ++index)
      {
        if (this.FoodSet[index].animal == animal)
          return this.FoodSet[index];
      }
      throw new Exception("HOW DID THIS ANIMAL GET ERE, WITHOUT HAVING ANY FOOD!");
    }

    public void AddAnimal(AnimalType animal)
    {
      bool flag = false;
      for (int index = 0; index < this.FoodSet.Count; ++index)
      {
        if (this.FoodSet[index].animal == animal)
          flag = true;
      }
      if (flag)
        return;
      this.FoodSet.Add(new TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet(animal));
    }

    public AnimalFoodDistribution(Reader reader, int VersionNumberForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("a", ref _out);
      this.FoodSet = new List<TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet>();
      for (int index = 0; index < _out; ++index)
        this.FoodSet.Add(new TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet(reader, VersionNumberForLoad));
    }

    public void SaveAnimalFoodDistribution(Writer writer)
    {
      writer.WriteInt("a", this.FoodSet.Count);
      for (int index = 0; index < this.FoodSet.Count; ++index)
        this.FoodSet[index].SaveFoodSet(writer);
    }
  }
}
