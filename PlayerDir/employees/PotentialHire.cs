// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.PotentialHire
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.employees
{
  internal class PotentialHire
  {
    public IntakePerson intakeperson;
    public EmployeeStats employeestats;
    public EmployeeType employeetype;
    public HireResult CurrentHireResult;

    public PotentialHire(EmployeeType _employeetype, int Seniority = -1)
    {
      this.employeetype = _employeetype;
      this.CurrentHireResult = HireResult.NoResult;
      bool IsAGirl;
      AnimalType employee = Employees.GetEmployee(this.employeetype, out IsAGirl);
      this.intakeperson = new IntakePerson(employee, _IsAGirl: IsAGirl);
      this.employeestats = EmployeesStats.GetEmployeestats(this.employeetype, employee, Seniority);
    }

    public int GetMinimumWage() => this.employeestats.MinimumWage + (int) Math.Round((double) (this.employeestats.MaximumWage - this.employeestats.MinimumWage) * (double) this.employeestats.Greed * 0.00999999977648258);

    public string GetJobTitle() => EmployeesStats.GetJobTitle(this.employeetype, this.intakeperson.animaltype, this.employeestats.Seniority);

    public void SavePotentialHire(Writer writer)
    {
      this.intakeperson.SaveIntakePerson(writer);
      this.employeestats.SaveEmployeeStats(writer);
      writer.WriteInt("p", (int) this.employeetype);
      writer.WriteInt("p", (int) this.CurrentHireResult);
    }

    public PotentialHire(Reader reader, int VersionForLoad)
    {
      this.intakeperson = new IntakePerson(reader);
      this.employeestats = new EmployeeStats(reader);
      int _out = 0;
      int num1 = (int) reader.ReadInt("p", ref _out);
      if (VersionForLoad < 36)
      {
        if (_out == 19)
          _out = 0;
        else
          ++_out;
      }
      this.employeetype = (EmployeeType) _out;
      int num2 = (int) reader.ReadInt("p", ref _out);
      this.CurrentHireResult = (HireResult) _out;
    }
  }
}
