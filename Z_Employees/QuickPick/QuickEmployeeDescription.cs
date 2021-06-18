// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.QuickPick.QuickEmployeeDescription
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Employees.QuickPick
{
  internal class QuickEmployeeDescription
  {
    public int Level = 1;
    public int CurrentSalary;
    public float[] StoreEmployeeStatValues;
    private int Rank;
    public AnimalType thisemployee;
    public TILETYPE WorksHere;
    public string NAME;
    public int NameIndex;
    private ShopStatsCollection Ref_ShopStatsCollection;
    public float XP;
    public float TimeAtWork_IncludingBreaks;
    public float TimeSpentServing;
    public float TimeSpentOnBreaks;
    public float LiveServingTimeLeft;
    public float BreakTime;
    public int CustomersServed;
    public List<EmployeeHistory> employeehistory;
    private float Frustration;
    public int FrustrationDaysFromOverwork;
    public float Determination;
    public int ShopUID;
    public bool IsAGirl;
    public Seniority seniorityLevel;
    public bool JustMovedShop = true;
    public bool FireThisEmployee;
    public bool WillQuitToday;
    private EmployeeType employeeType;

    public QuickEmployeeDescription(TILETYPE buildingthis, int _ShopUID, float StarRating = -1f)
    {
      this.ShopUID = _ShopUID;
      this.Determination = (float) TinyZoo.Game1.Rnd.Next(0, 100);
      this.Determination *= 0.01f;
      this.employeehistory = new List<EmployeeHistory>();
      this.WorksHere = buildingthis;
      this.Rank = 0;
      this.StoreEmployeeStatValues = new float[5];
      this.StoreEmployeeStatValues[3] = 1f;
      this.StoreEmployeeStatValues[1] = (float) TinyZoo.Game1.Rnd.Next(35, 65) * 0.01f;
      this.StoreEmployeeStatValues[4] = 0.5f;
      if (buildingthis != TILETYPE.Count)
        this.thisemployee = EmployeeData.GetBuildingtoEmployee(buildingthis, out this.IsAGirl);
      this.NAME = PeopleNames.GetName(!this.IsAGirl, out this.NameIndex, this.thisemployee);
      this.seniorityLevel = Seniority.Junior;
      if ((double) StarRating > -1.0)
        this.Level = (int) ((double) StarRating * 10.0);
      EmployeeData.IsThisAnEmployee(this.thisemployee, out this.employeeType);
      EmployeeStats employeestats = EmployeesStats.GetEmployeestats(this.employeeType, this.thisemployee, (int) this.seniorityLevel);
      this.CurrentSalary = (employeestats.MinimumWage + employeestats.MaximumWage) / 2;
    }

    public float GetTopMovementSpeed()
    {
      double num = 1.0 + (double) this.Level / 50.0 * 2.0;
      return (float) (num + num * ((double) this.StoreEmployeeStatValues[4] - 0.5) * 0.400000005960464 + 0.300000011920929);
    }

    public void GiveBonus(int amount)
    {
    }

    public void ChangedSalary(int newSalary) => this.CurrentSalary = newSalary;

    public void SetStatValues()
    {
      this.StoreEmployeeStatValues[0] = this.XP / ((float) this.Level * 10f);
      this.StoreEmployeeStatValues[0] = MathHelper.Clamp(this.StoreEmployeeStatValues[0], 0.0f, 1f);
      this.StoreEmployeeStatValues[2] = this.Determination * (1f - this.Frustration);
    }

    public void StartNewDay(float SecondsZooWasOpen, Employee EmployeeData)
    {
      if (this.employeehistory.Count == 0)
        this.employeehistory.Add(new EmployeeHistory((int) Player.financialrecords.GetDaysPassed()));
      if ((double) this.Frustration > 1.0)
      {
        ++this.FrustrationDaysFromOverwork;
        this.StoreEmployeeStatValues[1] -= Math.Min((float) (((double) this.Frustration - 1.0) * 0.0500000007450581), 0.1f);
      }
      this.Frustration = 0.0f;
      if ((this.FrustrationDaysFromOverwork > 0 || Z_DebugFlags.developerOverrides[3] > 0) && (TinyZoo.Game1.Rnd.Next(0, this.FrustrationDaysFromOverwork) > (int) this.Determination * 20 || Z_DebugFlags.developerOverrides[3] > 0))
      {
        this.WillQuitToday = true;
        LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.EmployeeQuit));
        LiveStats.AddEmployeeToQuitList(EmployeeData);
      }
      if ((double) SecondsZooWasOpen > 0.0)
      {
        this.employeehistory[this.employeehistory.Count - 1].PercentOnBreak = this.TimeSpentOnBreaks / SecondsZooWasOpen;
        this.employeehistory[this.employeehistory.Count - 1].PercentServing = this.TimeSpentServing / SecondsZooWasOpen;
        this.employeehistory[this.employeehistory.Count - 1].PercentAtWork = this.TimeAtWork_IncludingBreaks / SecondsZooWasOpen;
        this.employeehistory[this.employeehistory.Count - 1].ShopOpenTime = SecondsZooWasOpen / Z_GameFlags.SecondsInDay;
      }
      int marketAverage = EmployeesStats.GetEmployeestats(this.employeeType, this.thisemployee).GetMarketAverage();
      if (this.CurrentSalary > marketAverage)
        this.StoreEmployeeStatValues[1] += (float) (((double) this.CurrentSalary / (double) marketAverage - 1.0) * 0.00999999977648258);
      else
        this.StoreEmployeeStatValues[1] -= (float) ((1.0 - (double) this.CurrentSalary / (double) marketAverage) * 0.00999999977648258);
      this.StoreEmployeeStatValues[1] = MathHelper.Clamp(this.StoreEmployeeStatValues[1], 0.0f, 1f);
      this.employeehistory[this.employeehistory.Count - 1].CustomersServed = this.CustomersServed;
      this.StoreEmployeeStatValues[3] = 1f;
      this.TimeSpentServing = 0.0f;
      this.TimeSpentOnBreaks = 0.0f;
      this.TimeAtWork_IncludingBreaks = 0.0f;
      this.CustomersServed = 0;
    }

    public float GetServingTimeAndServe()
    {
      if (this.Ref_ShopStatsCollection == null)
        this.Ref_ShopStatsCollection = ShopData.GetShopInfo(this.WorksHere);
      float num = (this.Ref_ShopStatsCollection.MaxServingTime - (float) (((double) this.Ref_ShopStatsCollection.MaxServingTime - (double) this.Ref_ShopStatsCollection.MinServingTime) * (((double) this.Level + 10.0) / 60.0))) * (float) (0.800000011920929 + (double) this.StoreEmployeeStatValues[4] * 0.400000005960464);
      float val2 = num + num * (1f - Math.Max(this.StoreEmployeeStatValues[3], this.StoreEmployeeStatValues[2] * 0.5f));
      this.StoreEmployeeStatValues[3] -= 0.05f;
      ++this.CustomersServed;
      this.XP += (float) (0.200000002980232 + (double) this.Determination * 0.5);
      this.LiveServingTimeLeft = Math.Max(this.LiveServingTimeLeft, val2);
      if ((double) this.StoreEmployeeStatValues[3] <= 0.300000011920929)
        this.Frustration += (float) ((0.300000011920929 - (double) this.StoreEmployeeStatValues[3]) * 0.100000001490116);
      return val2;
    }

    public void GoOnBreak(float SecondsForBreak) => this.BreakTime = SecondsForBreak;

    public bool IsOnBreak() => (double) this.BreakTime > 0.0 && (double) this.LiveServingTimeLeft <= 0.0;

    public static string GetStoreEmployeeStatToString(StoreEmployeeStat stat)
    {
      switch (stat)
      {
        case StoreEmployeeStat.Experience:
          return "Experience";
        case StoreEmployeeStat.Politeness:
          return "Politeness";
        case StoreEmployeeStat.WorkEthic:
          return "Work Ethic";
        case StoreEmployeeStat.Energy:
          return "Energy";
        case StoreEmployeeStat.JobSatisfaction:
          return "Job Satisfaction";
        default:
          return "PLACEHOLDER";
      }
    }

    public QuickEmployeeDescription(Reader reader, int VersionNumberForLoad)
    {
      int num1 = (int) reader.ReadInt("e", ref this.CurrentSalary);
      if (VersionNumberForLoad < 37)
      {
        int _out = 0;
        int num2 = (int) reader.ReadInt("e", ref _out);
      }
      int _out1 = 0;
      int num3 = (int) reader.ReadInt("e", ref _out1);
      this.StoreEmployeeStatValues = new float[5];
      for (int index = 0; index < _out1; ++index)
      {
        int num2 = (int) reader.ReadFloat("e", ref this.StoreEmployeeStatValues[index]);
      }
      int num4 = (int) reader.ReadInt("e", ref this.Rank);
      int num5 = (int) reader.ReadInt("e", ref _out1);
      this.thisemployee = (AnimalType) _out1;
      int num6 = (int) reader.ReadInt("e", ref _out1);
      this.WorksHere = (TILETYPE) _out1;
      int num7 = (int) reader.ReadInt("e", ref this.NameIndex);
      int num8 = (int) reader.ReadBool("e", ref this.IsAGirl);
      this.NAME = PeopleNames.GetName(!this.IsAGirl, out this.NameIndex, this.thisemployee, this.NameIndex);
      int num9 = (int) reader.ReadFloat("e", ref this.XP);
      int num10 = (int) reader.ReadFloat("e", ref this.Frustration);
      int num11 = (int) reader.ReadFloat("e", ref this.Determination);
      int num12 = (int) reader.ReadInt("e", ref this.FrustrationDaysFromOverwork);
      int num13 = (int) reader.ReadInt("e", ref this.ShopUID);
      int num14 = (int) reader.ReadInt("e", ref _out1);
      this.seniorityLevel = (Seniority) _out1;
      int num15 = (int) reader.ReadInt("e", ref _out1);
      this.employeehistory = new List<EmployeeHistory>();
      for (int index = 0; index < _out1; ++index)
        this.employeehistory.Add(new EmployeeHistory(reader));
      if (VersionNumberForLoad > 27)
      {
        int num16 = (int) reader.ReadInt("e", ref this.Level);
      }
      else
        this.Level = 1;
      this.Ref_ShopStatsCollection = ShopData.GetShopInfo(this.WorksHere);
      EmployeeData.IsThisAnEmployee(this.thisemployee, out this.employeeType);
    }

    public void SaveQuickEmployeeDescription(Writer writer)
    {
      writer.WriteInt("e", this.CurrentSalary);
      writer.WriteInt("e", this.StoreEmployeeStatValues.Length);
      for (int index = 0; index < this.StoreEmployeeStatValues.Length; ++index)
        writer.WriteFloat("e", this.StoreEmployeeStatValues[index]);
      writer.WriteInt("e", this.Rank);
      writer.WriteInt("e", (int) this.thisemployee);
      writer.WriteInt("e", (int) this.WorksHere);
      writer.WriteInt("e", this.NameIndex);
      writer.WriteBool("e", this.IsAGirl);
      writer.WriteFloat("e", this.XP);
      writer.WriteFloat("e", this.Frustration);
      writer.WriteFloat("e", this.Determination);
      writer.WriteInt("e", this.FrustrationDaysFromOverwork);
      writer.WriteInt("e", this.ShopUID);
      writer.WriteInt("e", (int) this.seniorityLevel);
      writer.WriteInt("e", this.employeehistory.Count);
      for (int index = 0; index < this.employeehistory.Count; ++index)
        this.employeehistory[index].SaveEmployeeHistory(writer);
      writer.WriteInt("e", this.Level);
    }
  }
}
