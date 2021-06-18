// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.FoodSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.FoodDayCalc;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.Layout.CellBlocks
{
  internal class FoodSet
  {
    public AnimalType animal;
    public List<float> FoodUnitsPerDay;

    public FoodSet(AnimalType _animal)
    {
      this.animal = _animal;
      this.FoodUnitsPerDay = new List<float>();
      FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(_animal);
      for (int index = 0; index < foodCollection.animalfoodentry.Count; ++index)
      {
        this.FoodUnitsPerDay.Add(0.0f);
        if (index == 0)
          this.FoodUnitsPerDay[index] = foodCollection.FullDailyFoodRquirement;
      }
    }

    public void SetConsumptionPerDay(ref float[] AllFoodUnits, TempAnimalInfo tempanimalinfo)
    {
      FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(this.animal);
      float foodUnitsMultiplier = tempanimalinfo.GetFoodUnitsMultiplier();
      for (int index = 0; index < foodCollection.animalfoodentry.Count; ++index)
      {
        float _TotalUnitsPerDay = this.FoodUnitsPerDay[index] * foodUnitsMultiplier;
        tempanimalinfo.EatsThis.Add(new FoodTemp(foodCollection.animalfoodentry[index].foodtype, _TotalUnitsPerDay));
        AllFoodUnits[(int) foodCollection.animalfoodentry[index].foodtype] += _TotalUnitsPerDay;
      }
    }

    public FoodSet(Reader reader, int VersionForLoad)
    {
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("c", ref _out1);
      this.FoodUnitsPerDay = new List<float>();
      for (int index = 0; index < _out1; ++index)
      {
        float _out2 = 0.0f;
        int num2 = (int) reader.ReadFloat("c", ref _out2);
        this.FoodUnitsPerDay.Add(_out2);
      }
      if (VersionForLoad <= 15)
        return;
      int num3 = (int) reader.ReadInt("c", ref _out1);
      this.animal = (AnimalType) _out1;
    }

    public void SaveFoodSet(Writer writer)
    {
      writer.WriteInt("c", this.FoodUnitsPerDay.Count);
      for (int index = 0; index < this.FoodUnitsPerDay.Count; ++index)
        writer.WriteFloat("c", this.FoodUnitsPerDay[index]);
      writer.WriteInt("c", (int) this.animal);
    }
  }
}
