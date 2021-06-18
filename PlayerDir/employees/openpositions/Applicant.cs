// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.openpositions.Applicant
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.employees.openpositions
{
  internal class Applicant
  {
    public bool IsReferred;
    public bool HiredThroughAgency;
    public AnimalType animalType;
    public float StarRating;
    public Seniority SeniorityLevel;
    public int minSalary;
    public int maxSalary;
    public string Name;
    public bool IsAGirl;
    private int NameIndex;

    public Applicant(
      AnimalType _animalType,
      float _StarRating,
      Seniority _SeniorityLevel,
      bool _IsAGirl,
      string _Name,
      bool _HiredThroughAgency,
      int _NameIndex)
    {
      this.NameIndex = _NameIndex;
      this.animalType = _animalType;
      this.StarRating = _StarRating;
      this.SeniorityLevel = _SeniorityLevel;
      this.IsAGirl = _IsAGirl;
      this.Name = _Name;
      this.HiredThroughAgency = _HiredThroughAgency;
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(this.animalType, out employeetype);
      EmployeeStats employeestats = EmployeesStats.GetEmployeestats(employeetype, this.animalType, (int) this.SeniorityLevel);
      this.minSalary = employeestats.MinimumWage;
      this.maxSalary = employeestats.MaximumWage;
    }

    public string GetJobTitle()
    {
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(this.animalType, out employeetype);
      return EmployeesStats.GetJobTitle(employeetype, this.animalType, (int) this.SeniorityLevel);
    }

    public QuickEmployeeDescription ConstructQuickEmployeeDescriptionForHiring(
      ShopEntry _shopEntryForBuilding,
      EmployeeType RoamingEmployeeType,
      Player player)
    {
      TILETYPE buildingthis;
      int _ShopUID;
      if (RoamingEmployeeType != EmployeeType.None)
      {
        buildingthis = TILETYPE.Logo;
        _ShopUID = -1;
      }
      else
      {
        buildingthis = _shopEntryForBuilding.tiletype;
        _ShopUID = _shopEntryForBuilding.ShopUID;
      }
      return new QuickEmployeeDescription(buildingthis, _ShopUID, this.StarRating)
      {
        thisemployee = this.animalType,
        IsAGirl = this.IsAGirl,
        NAME = this.Name,
        NameIndex = this.NameIndex,
        seniorityLevel = this.SeniorityLevel,
        CurrentSalary = (this.minSalary + this.maxSalary) / 2
      };
    }

    public EmployeeType GetEmployeeType()
    {
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(this.animalType, out employeetype);
      return employeetype;
    }

    public Applicant(Reader reader)
    {
      int num1 = (int) reader.ReadBool("l", ref this.IsReferred);
      int num2 = (int) reader.ReadBool("l", ref this.HiredThroughAgency);
      int _out = 0;
      int num3 = (int) reader.ReadInt("l", ref _out);
      this.animalType = (AnimalType) _out;
      int num4 = (int) reader.ReadFloat("l", ref this.StarRating);
      int num5 = (int) reader.ReadInt("l", ref _out);
      this.SeniorityLevel = (Seniority) _out;
      int num6 = (int) reader.ReadInt("l", ref this.minSalary);
      int num7 = (int) reader.ReadInt("l", ref this.maxSalary);
      int num8 = (int) reader.ReadBool("l", ref this.IsAGirl);
      int num9 = (int) reader.ReadInt("l", ref this.NameIndex);
      this.Name = PeopleNames.GetName(!this.IsAGirl, out this.NameIndex, this.animalType, this.NameIndex);
    }

    public void SaveApplicant(Writer writer)
    {
      writer.WriteBool("l", this.IsReferred);
      writer.WriteBool("l", this.HiredThroughAgency);
      writer.WriteInt("l", (int) this.animalType);
      writer.WriteFloat("l", this.StarRating);
      writer.WriteInt("l", (int) this.SeniorityLevel);
      writer.WriteInt("l", this.minSalary);
      writer.WriteInt("l", this.maxSalary);
      writer.WriteBool("l", this.IsAGirl);
      writer.WriteInt("l", this.NameIndex);
    }
  }
}
