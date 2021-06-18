// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.FinancialHistory.WorldHistoryDay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.FinancialHistory
{
  internal class WorldHistoryDay
  {
    public int TrashAtDayStart;
    public int TrashDropped;
    public int TrashCollected;
    public List<AnimalType> NewEmployees;
    public List<AnimalType> EmployeesFired;
    public List<Vector2Int> NewAnimalsBornOrEarned;

    public WorldHistoryDay(int _TrashAtDayStart)
    {
      this.NewEmployees = new List<AnimalType>();
      this.EmployeesFired = new List<AnimalType>();
      this.TrashAtDayStart = _TrashAtDayStart;
      this.NewAnimalsBornOrEarned = new List<Vector2Int>();
    }

    public void GotNewAnimal(AnimalType animal, int Skin) => this.NewAnimalsBornOrEarned.Add(new Vector2Int((int) animal, Skin));

    public void EmployedSomeone(AnimalType employeetype) => this.NewEmployees.Add(employeetype);

    public void FiredSomeone(AnimalType employeetype) => this.EmployeesFired.Add(employeetype);

    public void DroppedTrash() => ++this.TrashDropped;

    public void TrashPickedUp() => ++this.TrashCollected;

    public void Save_WorldHistoryDay(Writer writer)
    {
      writer.WriteInt("w", this.TrashAtDayStart);
      writer.WriteInt("w", this.TrashDropped);
      writer.WriteInt("w", this.TrashCollected);
      writer.WriteInt("w", this.NewEmployees.Count);
      for (int index = 0; index < this.NewEmployees.Count; ++index)
        writer.WriteInt("w", (int) this.NewEmployees[index]);
      writer.WriteInt("w", this.EmployeesFired.Count);
      for (int index = 0; index < this.EmployeesFired.Count; ++index)
        writer.WriteInt("w", (int) this.EmployeesFired[index]);
      writer.WriteInt("w", this.NewAnimalsBornOrEarned.Count);
      for (int index = 0; index < this.NewAnimalsBornOrEarned.Count; ++index)
        this.NewAnimalsBornOrEarned[index].SaveVector2Int(writer);
    }

    public WorldHistoryDay(Reader reader)
    {
      int num1 = (int) reader.ReadInt("w", ref this.TrashAtDayStart);
      int num2 = (int) reader.ReadInt("w", ref this.TrashDropped);
      int num3 = (int) reader.ReadInt("w", ref this.TrashCollected);
      int _out1 = 0;
      int num4 = (int) reader.ReadInt("w", ref _out1);
      this.NewEmployees = new List<AnimalType>();
      int _out2 = 0;
      for (int index = 0; index < _out1; ++index)
      {
        int num5 = (int) reader.ReadInt("w", ref _out2);
        this.NewEmployees.Add((AnimalType) _out2);
      }
      int num6 = (int) reader.ReadInt("w", ref _out1);
      this.EmployeesFired = new List<AnimalType>();
      for (int index = 0; index < _out1; ++index)
      {
        int num5 = (int) reader.ReadInt("w", ref _out2);
        this.EmployeesFired.Add((AnimalType) _out2);
      }
      this.NewAnimalsBornOrEarned = new List<Vector2Int>();
      int num7 = (int) reader.ReadInt("w", ref _out1);
      for (int index = 0; index < _out1; ++index)
        this.NewAnimalsBornOrEarned.Add(new Vector2Int(reader));
    }
  }
}
