// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.PotentialHires
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.employees
{
  internal class PotentialHires
  {
    public List<PotentialHire> RandomHires;
    public int DayOfHires;

    public PotentialHires(List<Employee> employees) => this.Make(TinyZoo.Game1.Rnd, employees);

    public void ForceRandomHiresOnTutorial()
    {
      this.RandomHires = new List<PotentialHire>();
      this.RandomHires.Add(new PotentialHire(EmployeeType.Keeper, 0));
      this.RandomHires[0].employeestats.Greed = 0;
    }

    public void SetInterviewStatus(PotentialHire screwthisguy, HireResult hireresult) => this.CheckThisEntryForStatus(screwthisguy, hireresult, this.RandomHires);

    private void CheckThisEntryForStatus(
      PotentialHire screwthisguy,
      HireResult hireresult,
      List<PotentialHire> thesehires)
    {
      for (int index = thesehires.Count - 1; index > -1; --index)
      {
        if (thesehires[index] == screwthisguy)
          thesehires[index].CurrentHireResult = hireresult;
      }
    }

    public void ResetHires(List<Employee> employees)
    {
      this.Make(TinyZoo.Game1.Rnd, employees);
      int dayOfHires = this.DayOfHires;
    }

    private void Make(Random random, List<Employee> employees)
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index < employees.Count; ++index)
      {
        if (employees[index].employeetype == EmployeeType.Keeper)
          ++num1;
        else if (employees[index].employeetype == EmployeeType.Janitor)
          ++num2;
      }
      this.DayOfHires = 0;
      this.RandomHires = new List<PotentialHire>();
      if (employees.Count >= 5 && employees.Count < 10)
        random.Next(0, 2);
      for (int index = 0; index < 4; ++index)
        this.RandomHires.Add(new PotentialHire((EmployeeType) random.Next(1, 20)));
      int index1 = -1;
      if (num1 == 0)
      {
        index1 = TinyZoo.Game1.Rnd.Next(0, 4);
        this.RandomHires[index1] = new PotentialHire(EmployeeType.Keeper);
      }
      if (num2 != 0)
        return;
      int index2 = index1;
      while (index2 == index1)
        index2 = TinyZoo.Game1.Rnd.Next(0, 4);
      this.RandomHires[index2] = new PotentialHire(EmployeeType.Janitor);
    }

    public void SavePotentialHires(Writer writer)
    {
      writer.WriteInt("p", this.RandomHires.Count);
      for (int index = 0; index < this.RandomHires.Count; ++index)
        this.RandomHires[index].SavePotentialHire(writer);
    }

    public PotentialHires(Reader reader, int VersionNumberForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("p", ref _out);
      this.RandomHires = new List<PotentialHire>();
      for (int index = 0; index < _out; ++index)
        this.RandomHires.Add(new PotentialHire(reader, VersionNumberForLoad));
    }
  }
}
