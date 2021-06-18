// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.FinancialHistory.WorldHistoryWeek
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.FinancialHistory
{
  internal class WorldHistoryWeek
  {
    public List<WorldHistoryDay> worldhistoryday;

    public WorldHistoryWeek()
    {
      this.worldhistoryday = new List<WorldHistoryDay>();
      this.worldhistoryday.Add(new WorldHistoryDay(0));
    }

    public void StatNewDay(int TrashOnMap) => this.worldhistoryday.Add(new WorldHistoryDay(TrashOnMap));

    public void GotNewAnimal(AnimalType animal, int Skin) => this.worldhistoryday[this.worldhistoryday.Count - 1].GotNewAnimal(animal, Skin);

    public void EmployedSomeone(AnimalType employeetype) => this.worldhistoryday[this.worldhistoryday.Count - 1].EmployedSomeone(employeetype);

    public void FiredSomeone(AnimalType employeetype) => this.worldhistoryday[this.worldhistoryday.Count - 1].FiredSomeone(employeetype);

    public void DroppedTrash() => this.worldhistoryday[this.worldhistoryday.Count - 1].DroppedTrash();

    public void TrashPickedUp() => this.worldhistoryday[this.worldhistoryday.Count - 1].TrashPickedUp();

    public void SaveWorldHistoryWeek(Writer writer)
    {
      writer.WriteInt("e", this.worldhistoryday.Count);
      for (int index = 0; index < this.worldhistoryday.Count; ++index)
        this.worldhistoryday[index].Save_WorldHistoryDay(writer);
    }

    public WorldHistoryWeek(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("e", ref _out);
      this.worldhistoryday = new List<WorldHistoryDay>();
      for (int index = 0; index < _out; ++index)
        this.worldhistoryday.Add(new WorldHistoryDay(reader));
    }
  }
}
