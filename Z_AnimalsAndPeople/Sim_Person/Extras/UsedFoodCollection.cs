// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.UsedFoodCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class UsedFoodCollection
  {
    public List<UsedFoodEntry> usedfoodentries;
    public float ALLFOODUsed;

    public UsedFoodCollection() => this.usedfoodentries = new List<UsedFoodEntry>();

    public void UsedThis(AnimalFoodType FoodTypeUsed, float TotalUsed)
    {
      this.ALLFOODUsed += TotalUsed;
      for (int index = 0; index < this.usedfoodentries.Count; ++index)
      {
        if (this.usedfoodentries[index].FoodTypeUsed == FoodTypeUsed)
        {
          this.usedfoodentries[index].TotalUsed += TotalUsed;
          return;
        }
      }
      this.usedfoodentries.Add(new UsedFoodEntry(FoodTypeUsed, TotalUsed));
    }
  }
}
