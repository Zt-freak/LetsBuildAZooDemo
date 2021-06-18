// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Employee
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.employees;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir
{
  internal class Employee
  {
    public IntakePerson intakeperson;
    public EmployeeType employeetype;
    public int _Salary_DEPRICATEDREMOVEFROMSAVE;
    public int ActionsLeftToday;
    public int Determination;
    public int DaysEmployed;
    public EHistoryHolder ehistory;
    private bool IsFirstDay;
    public float CarryCapacity;
    public int ResearchProgress;
    public QuickEmployeeDescription quickemplyeedescription;
    public List<Vector2Int> ResearchPointHistory;
    public WorkZoneInfo workzoneinfo;
    private float SalarySatisfaction = -100f;

    public Employee(
      IntakePerson _intakeperson,
      EmployeeType employthis,
      int __Salary,
      int _Determination,
      QuickEmployeeDescription quickemployeeDesc = null)
    {
      this.quickemplyeedescription = quickemployeeDesc;
      this.Determination = _Determination;
      this.Salary = __Salary;
      this.intakeperson = _intakeperson;
      this.employeetype = employthis;
      this.ehistory = new EHistoryHolder();
      if (this.employeetype == EmployeeType.Architect)
        this.ResearchPointHistory = new List<Vector2Int>();
      if (!Employees.ThisEmplyeeSupportsZoning(employthis))
        return;
      this.workzoneinfo = new WorkZoneInfo(1);
      this.workzoneinfo.workzonetype = Employees.GetThisEmplyeesWorkZoneType(employthis);
      if (this.workzoneinfo.workzonetype != WorkZoneType.Pens)
        return;
      this.workzoneinfo.ZoneCap = -1;
    }

    public void EndedADay()
    {
      if (this.IsFirstDay)
      {
        this.IsFirstDay = false;
      }
      else
      {
        ++this.DaysEmployed;
        if (this.ehistory.ehistory.Count > 0 && this.ehistory.ehistory[this.ehistory.ehistory.Count - 1].ehistory.Count > 0)
        {
          E_History eHistory = this.ehistory.ehistory[this.ehistory.ehistory.Count - 1].ehistory[this.ehistory.ehistory[this.ehistory.ehistory.Count - 1].ehistory.Count - 1];
          if (this.quickemplyeedescription != null)
            this.quickemplyeedescription.XP += (float) eHistory.ActionsPerformed * this.GetPerformance();
        }
        if (this.quickemplyeedescription != null)
          return;
        this.ehistory.EndedADay(this.ActionsLeftToday);
      }
    }

    private void SetActionsLeftToday()
    {
      if (this.quickemplyeedescription == null)
        this.ActionsLeftToday = 5;
      else
        this.ActionsLeftToday = 5 + (int) ((double) this.quickemplyeedescription.Level / 50.0 * 15.0);
    }

    public void StartNewDay()
    {
      this.SetActionsLeftToday();
      if (this.quickemplyeedescription == null || this.quickemplyeedescription.Level >= 50 || (double) this.quickemplyeedescription.XP < (double) (this.quickemplyeedescription.Level * 10))
        return;
      this.quickemplyeedescription.XP -= (float) (this.quickemplyeedescription.Level * 10);
      ++this.quickemplyeedescription.Level;
    }

    public void StartNewWeek()
    {
      this.IsFirstDay = true;
      this.SetActionsLeftToday();
      this.ehistory.StartNewWeek(this.ActionsLeftToday);
    }

    public void ChangeSalary(int salary)
    {
      this._Salary_DEPRICATEDREMOVEFROMSAVE = salary;
      this.quickemplyeedescription.ChangedSalary(salary);
      this.SalarySatisfaction = -100f;
    }

    public float GetPaySatisfactionSalary()
    {
      if ((double) this.SalarySatisfaction <= -99.0)
      {
        EmployeeStats employeestats = EmployeesStats.GetEmployeestats(this.employeetype, this.quickemplyeedescription.thisemployee, (int) this.quickemplyeedescription.seniorityLevel);
        int num = employeestats.MinimumWage + (employeestats.MaximumWage - employeestats.MinimumWage) / 2;
        if (this.quickemplyeedescription.CurrentSalary > num)
          this.SalarySatisfaction = (float) ((double) num / (double) this.quickemplyeedescription.CurrentSalary - 1.0);
        else if (this.quickemplyeedescription.CurrentSalary < num)
          this.SalarySatisfaction = (float) (1.0 - (double) this.quickemplyeedescription.CurrentSalary / (double) num);
        this.SalarySatisfaction = MathHelper.Clamp(this.SalarySatisfaction, 0.0f, 1f);
      }
      return this.SalarySatisfaction;
    }

    public float GetPerformance() => (float) this.Determination * 0.01f;

    public int Salary
    {
      get => this.quickemplyeedescription != null ? this.quickemplyeedescription.CurrentSalary : this._Salary_DEPRICATEDREMOVEFROMSAVE / 2;
      set
      {
        if (this.quickemplyeedescription != null)
          this.quickemplyeedescription.CurrentSalary = value;
        else
          this._Salary_DEPRICATEDREMOVEFROMSAVE = value * 2;
      }
    }

    public int GetAllResearchPoints(TimeSegmentType timesegment, out bool NoData)
    {
      NoData = false;
      int num = 0;
      for (int index = 0; index < this.ResearchPointHistory.Count; ++index)
        num += this.ResearchPointHistory[index].Y;
      return num;
    }

    public void SaveEmployee(Writer writer)
    {
      writer.WriteBool("e", this.intakeperson != null);
      if (this.intakeperson != null)
        this.intakeperson.SaveIntakePerson(writer);
      writer.WriteInt("e", (int) this.employeetype);
      if (this.employeetype == EmployeeType.Architect)
        writer.WriteInt("e", this.ResearchProgress);
      writer.WriteBool("e", this.quickemplyeedescription != null);
      if (this.quickemplyeedescription != null)
        this.quickemplyeedescription.SaveQuickEmployeeDescription(writer);
      else
        writer.WriteInt("e", this._Salary_DEPRICATEDREMOVEFROMSAVE);
      writer.WriteInt("e", this.Determination);
      this.ehistory.SaveEHistoryHolder(writer);
      writer.WriteBool("e", this.IsFirstDay);
      if (this.employeetype == EmployeeType.Architect)
      {
        writer.WriteInt("e", this.ResearchPointHistory.Count);
        for (int index = 0; index < this.ResearchPointHistory.Count; ++index)
          this.ResearchPointHistory[index].SaveVector2Int(writer);
      }
      writer.WriteBool("e", this.workzoneinfo != null);
      if (this.workzoneinfo == null)
        return;
      this.workzoneinfo.SaveWorkZoneInfo(writer);
    }

    public Employee(Reader reader, int VersionForLoad)
    {
      bool _out1 = false;
      int num1 = (int) reader.ReadBool("e", ref _out1);
      if (_out1)
        this.intakeperson = new IntakePerson(reader);
      int _out2 = -1;
      int num2 = (int) reader.ReadInt("e", ref _out2);
      if (VersionForLoad < 36)
      {
        if (_out2 == 19)
          _out2 = 0;
        else
          ++_out2;
      }
      this.employeetype = (EmployeeType) _out2;
      if (this.employeetype == EmployeeType.Architect)
      {
        int num3 = (int) reader.ReadInt("e", ref this.ResearchProgress);
      }
      bool _out3 = false;
      int num4 = (int) reader.ReadBool("e", ref _out3);
      if (_out3)
      {
        this.quickemplyeedescription = new QuickEmployeeDescription(reader, VersionForLoad);
      }
      else
      {
        int num5 = (int) reader.ReadInt("e", ref this._Salary_DEPRICATEDREMOVEFROMSAVE);
      }
      int num6 = (int) reader.ReadInt("e", ref this.Determination);
      this.ehistory = new EHistoryHolder(reader);
      int num7 = (int) reader.ReadBool("e", ref this.IsFirstDay);
      if (this.employeetype == EmployeeType.Architect)
      {
        this.ResearchPointHistory = new List<Vector2Int>();
        int num8 = (int) reader.ReadInt("e", ref _out2);
        for (int index = 0; index < _out2; ++index)
          this.ResearchPointHistory.Add(new Vector2Int(reader));
      }
      if (VersionForLoad > 6)
      {
        bool _out4 = false;
        int num8 = (int) reader.ReadBool("e", ref _out4);
        if (!_out4)
          return;
        this.workzoneinfo = new WorkZoneInfo(reader, VersionForLoad);
      }
      else
      {
        if (!Employees.ThisEmplyeeSupportsZoning(this.employeetype))
          return;
        this.workzoneinfo = new WorkZoneInfo(1);
        this.workzoneinfo.workzonetype = Employees.GetThisEmplyeesWorkZoneType(this.employeetype);
        if (this.workzoneinfo.workzonetype != WorkZoneType.Pens)
          return;
        this.workzoneinfo.ZoneCap = -1;
      }
    }
  }
}
