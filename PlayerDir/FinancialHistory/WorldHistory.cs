// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.FinancialHistory.WorldHistory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.FinancialHistory
{
  internal class WorldHistory
  {
    private List<WorldHistoryWeek> worldhistorybyweek;

    public WorldHistory()
    {
      this.worldhistorybyweek = new List<WorldHistoryWeek>();
      this.worldhistorybyweek.Add(new WorldHistoryWeek());
    }

    public WorldHistoryWeek GetWeekBeforeThisOne() => this.worldhistorybyweek.Count > 1 ? this.worldhistorybyweek[this.worldhistorybyweek.Count - 2] : (WorldHistoryWeek) null;

    public void StartNewDay() => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].StatNewDay(OverWorldManager.trashmanager.GetTotalTrash());

    public void StartNewWeek() => this.worldhistorybyweek.Add(new WorldHistoryWeek());

    public void GotNewAnimal(AnimalType animal, int Skin) => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].GotNewAnimal(animal, Skin);

    public void EmployedSomeone(AnimalType employeetype) => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].EmployedSomeone(employeetype);

    public void FiredSomeone(AnimalType employeetype) => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].FiredSomeone(employeetype);

    public void DroppedTrash() => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].DroppedTrash();

    public void TrashPickedUp() => this.worldhistorybyweek[this.worldhistorybyweek.Count - 1].TrashPickedUp();

    public void SaveWorldHistory(Writer writer)
    {
      writer.WriteInt("e", this.worldhistorybyweek.Count);
      for (int index = 0; index < this.worldhistorybyweek.Count; ++index)
        this.worldhistorybyweek[index].SaveWorldHistoryWeek(writer);
    }

    public WorldHistory(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("e", ref _out);
      this.worldhistorybyweek = new List<WorldHistoryWeek>();
      for (int index = 0; index < _out; ++index)
        this.worldhistorybyweek.Add(new WorldHistoryWeek(reader));
    }
  }
}
