// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.IntakeStuff.IntakeInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Blance;

namespace TinyZoo.PlayerDir.IntakeStuff
{
  internal class IntakeInfo
  {
    public List<IntakePerson> People;
    public CityName CameFromHere = CityName.Count;
    private bool IsTransferFromHoldingCells;

    public IntakeInfo() => this.People = new List<IntakePerson>();

    public IntakeInfo(Player player, ref int IntakesSpawned)
    {
      if (LiveStats.reqforpeople == null)
        LiveStats.reqforpeople = new ReqForPeople();
      this.People = new List<IntakePerson>();
      Random random = new Random(IntakesSpawned);
      int num1 = 0;
      for (int index = 0; index < player.inventory.SecretAliensAvailable.Length; ++index)
      {
        if (player.inventory.SecretAliensAvailable[index])
          ++num1;
      }
      int num2 = IntakesSpawned != 0 ? (IntakesSpawned >= 10 ? (IntakesSpawned >= 20 ? random.Next(1, 5) : random.Next(1, 4)) : random.Next(1, 3)) : 1;
      int num3 = 0;
      while (num3 < num2)
        ++num3;
      ++IntakesSpawned;
    }

    public int GetRecCost(bool QuickGet)
    {
      int num = 0;
      for (int index = 0; index < this.People.Count; ++index)
        num += LiveStats.reqforpeople.wantsbyperson[(int) this.People[index].animaltype].TRCST.SmartGetValue(QuickGet, 10000);
      return num;
    }

    public IntakeInfo(bool _IsTransferFromHoldingCells)
    {
      this.IsTransferFromHoldingCells = _IsTransferFromHoldingCells;
      this.People = new List<IntakePerson>();
    }

    public void ResetForLanguage()
    {
      for (int index = 0; index < this.People.Count; ++index)
        this.People[index].ResetForLanguage();
    }

    public void SaveIntakeInfo(Writer writer)
    {
      writer.WriteInt("n", this.People.Count);
      for (int index = 0; index < this.People.Count; ++index)
        this.People[index].SaveIntakePerson(writer);
    }

    public IntakeInfo(Reader reader)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("n", ref _out);
      this.People = new List<IntakePerson>();
      for (int index = 0; index < _out; ++index)
        this.People.Add(new IntakePerson(reader));
    }
  }
}
