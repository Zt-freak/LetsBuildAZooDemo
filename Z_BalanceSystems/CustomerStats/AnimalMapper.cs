// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.CustomerStats.AnimalMapper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.Z_BalanceSystems.CustomerStats
{
  internal class AnimalMapper
  {
    private List<int> CellUIDs;
    private List<int> AnimalsHere;

    public AnimalMapper()
    {
      this.CellUIDs = new List<int>();
      this.AnimalsHere = new List<int>();
    }

    public int GetCellUID() => this.CellUIDs.Count == 1 ? this.CellUIDs[0] : this.CellUIDs[Game1.Rnd.Next(0, this.CellUIDs.Count)];

    public void AddAnimal(int CellUID)
    {
      for (int index = 0; index < this.CellUIDs.Count; ++index)
      {
        if (this.AnimalsHere[index] == CellUID)
        {
          this.AnimalsHere[index]++;
          return;
        }
      }
      this.CellUIDs.Add(CellUID);
      this.AnimalsHere.Add(1);
    }
  }
}
