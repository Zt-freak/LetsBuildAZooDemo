// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.StoreRooms.StockTime
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.StoreRooms
{
  internal class StockTime
  {
    private StockPredictor[] stocks;
    private List<AnimalCounter> animals;

    public StockTime(Player player)
    {
      this.stocks = new StockPredictor[88];
      for (int index = 0; index < this.stocks.Length; ++index)
        this.stocks[index] = new StockPredictor(player.storerooms.GetTotalStockOfThis((AnimalFoodType) index), AnimalFoodData.GetAnimalFoodInfo((AnimalFoodType) index).ShelfLife);
    }

    public void AddOne(PrisonerInfo prisoner)
    {
      if (this.animals == null)
        this.animals = new List<AnimalCounter>();
      for (int index = 0; index < this.animals.Count; ++index)
      {
        if (this.animals[index].animal == prisoner.intakeperson.animaltype)
        {
          this.animals[index].AddPerson(prisoner.GetIsABaby());
          return;
        }
      }
      this.animals.Add(new AnimalCounter(prisoner.intakeperson.animaltype, prisoner.GetIsABaby()));
    }

    public int GetDays(AnimalFoodType animalfood) => this.stocks[(int) animalfood].GetDaysStockWIllLast();

    public void FinalizeSet(AnimalFoodDistribution FoodForAnimals, Player player)
    {
      if (this.animals != null)
      {
        for (int index1 = 0; index1 < this.animals.Count; ++index1)
        {
          for (int index2 = 0; index2 < FoodForAnimals.FoodSet.Count; ++index2)
          {
            if (FoodForAnimals.FoodSet[index2].animal == this.animals[index1].animal)
            {
              FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(this.animals[index1].animal);
              for (int index3 = 0; index3 < FoodForAnimals.FoodSet[index2].FoodUnitsPerDay.Count; ++index3)
                this.stocks[(int) foodCollection.animalfoodentry[index3].foodtype].AddPerDayOfThis(FoodForAnimals.FoodSet[index2].FoodUnitsPerDay[index3] * this.animals[index1].Total);
            }
          }
        }
      }
      this.animals = (List<AnimalCounter>) null;
    }
  }
}
