// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.employees.openpositions.OpenPositions
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_BalanceSystems.Employees.JobApplicants;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.employees.openpositions
{
  internal class OpenPositions
  {
    public TILETYPE tileType;
    public int DayStarted;
    private List<Applicant> Applicants;
    public int NewApplicantsNotPopulated;
    public int TotalAmountSpent;
    public int NumberOfPositionsOpened;
    public bool IsSocialMediaEnabled;
    public bool IsJobPortalEnabled;
    public bool IsReferralEnabled;
    public EmployeeType RoamingEmployeeType;
    public bool TempPaidForAdminOnOpen;
    public bool TempPaidForSocialMediaOnOpen;
    public bool TempPaidForJobPortalOnOpen;

    public OpenPositions(TILETYPE _tileType, EmployeeType _RoamingEmployeeType = EmployeeType.None)
    {
      this.RoamingEmployeeType = _RoamingEmployeeType;
      this.Applicants = new List<Applicant>();
      this.tileType = _tileType;
      this.DayStarted = -1;
    }

    public AnimalType AddApplicantFromZooMoment(Player player)
    {
      this.Applicants.Add(ApplicantGenerator.CreateNewApplicant(this.tileType, this.RoamingEmployeeType, false, player));
      return this.Applicants[this.Applicants.Count - 1].animalType;
    }

    public OpenPositions(OpenPositions deepcopythis)
    {
      this.RoamingEmployeeType = deepcopythis.RoamingEmployeeType;
      this.DayStarted = deepcopythis.DayStarted;
      this.Applicants = deepcopythis.Applicants;
      this.NewApplicantsNotPopulated = deepcopythis.NewApplicantsNotPopulated;
      this.TotalAmountSpent = deepcopythis.TotalAmountSpent;
      this.NumberOfPositionsOpened = deepcopythis.NumberOfPositionsOpened;
      this.IsSocialMediaEnabled = deepcopythis.IsSocialMediaEnabled;
      this.IsJobPortalEnabled = deepcopythis.IsJobPortalEnabled;
      this.IsReferralEnabled = deepcopythis.IsReferralEnabled;
      this.tileType = deepcopythis.tileType;
    }

    public void ApplyAndCommitChanges(OpenPositions TempOpenPositions, Player player)
    {
      bool flag = this.NumberOfPositionsOpened == 0 && TempOpenPositions.NumberOfPositionsOpened > 0;
      this.NumberOfPositionsOpened = TempOpenPositions.NumberOfPositionsOpened;
      this.IsSocialMediaEnabled = TempOpenPositions.IsSocialMediaEnabled;
      this.IsJobPortalEnabled = TempOpenPositions.IsJobPortalEnabled;
      this.IsReferralEnabled = TempOpenPositions.IsReferralEnabled;
      if (this.NumberOfPositionsOpened == 0)
      {
        this.DeactivateSearch();
      }
      else
      {
        if (flag)
          this.StartNewSearch();
        JobApplicants_Calculator.CalculateJobApplicant_OnNewDay(player, this);
      }
      Console.WriteLine("OPENPOSITIONS CHANGE COMMITTED");
    }

    public bool CompareIfChanged(OpenPositions TempOpenPositions) => (0 | (this.NumberOfPositionsOpened != TempOpenPositions.NumberOfPositionsOpened ? 1 : 0) | (this.IsSocialMediaEnabled != TempOpenPositions.IsSocialMediaEnabled ? 1 : 0) | (this.IsJobPortalEnabled != TempOpenPositions.IsJobPortalEnabled ? 1 : 0) | (this.IsReferralEnabled != TempOpenPositions.IsReferralEnabled ? 1 : 0)) != 0;

    public int GetCostPerDay() => JobApplicants_Calculator.CalculateTotalCostPerDay(this.NumberOfPositionsOpened, this.IsSocialMediaEnabled, this.IsJobPortalEnabled);

    public int GetTotalReach() => JobApplicants_Calculator.CalculateTotalReach(this.NumberOfPositionsOpened, this.IsSocialMediaEnabled, this.IsJobPortalEnabled);

    public int GetNumberOfApplicants() => this.NewApplicantsNotPopulated + this.Applicants.Count;

    public List<Applicant> GetAndPopulateApplicantsForDisplay(Player player)
    {
      int num = Math.Min(ApplicantGenerator.MaxApplicantsForDisplay - this.Applicants.Count, this.NewApplicantsNotPopulated);
      for (int index = 0; index < num; ++index)
        this.GenerateNewApplicantForThisPosition(player);
      return this.Applicants;
    }

    public Applicant GenerateNewApplicantForThisPosition(Player player)
    {
      if (this.NewApplicantsNotPopulated <= 0)
        throw new Exception("NO");
      Applicant newApplicant = ApplicantGenerator.CreateNewApplicant(this.tileType, this.RoamingEmployeeType, false, player);
      this.Applicants.Add(newApplicant);
      --this.NewApplicantsNotPopulated;
      return newApplicant;
    }

    public void RemoveApplicant(Applicant thisApplicant)
    {
      this.Applicants.Remove(thisApplicant);
      if (this.Applicants.Count != 0)
        return;
      Z_NotificationManager.RescrubJobApplicants = true;
    }

    public void RemoveAndHireApplicant(Applicant thisApplicant)
    {
      this.RemoveApplicant(thisApplicant);
      if (JobApplicants_Calculator.UseSingleOpenPositions)
        return;
      if (this.NumberOfPositionsOpened > 0)
        --this.NumberOfPositionsOpened;
      if (this.NumberOfPositionsOpened != 0)
        return;
      this.Applicants.Clear();
      this.NewApplicantsNotPopulated = 0;
    }

    public void DeactivateSearch()
    {
      this.DayStarted = (int) Player.financialrecords.GetDaysPassed();
      this.TotalAmountSpent = 0;
      this.Applicants.Clear();
      this.NewApplicantsNotPopulated = 0;
      LiveStats.RemoveJobPosting(this.tileType, this.RoamingEmployeeType);
    }

    private void StartNewSearch() => this.DayStarted = (int) Player.financialrecords.GetDaysPassed();

    public EmployeeType GetEmployeeType()
    {
      if (this.RoamingEmployeeType != EmployeeType.None)
        return this.RoamingEmployeeType;
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(EmployeeData.GetBuildingtoEmployee(this.tileType, out bool _), out employeetype);
      return employeetype;
    }

    public OpenPositions(Reader reader, int VersionForLoad)
    {
      if (VersionForLoad < 35)
      {
        int _out = 0;
        int num = (int) reader.ReadInt("n", ref _out);
      }
      int _out1 = 0;
      int num1 = (int) reader.ReadInt("n", ref _out1);
      this.tileType = (TILETYPE) _out1;
      if (VersionForLoad < 36 && (this.tileType == TILETYPE.Count || this.tileType == TILETYPE.TestFence || (this.tileType == TILETYPE.SubwayEntrance || this.tileType == TILETYPE.ForSaleSignboard)))
        this.tileType = TILETYPE.None;
      int num2 = (int) reader.ReadInt("n", ref this.DayStarted);
      int num3 = (int) reader.ReadInt("n", ref _out1);
      this.Applicants = new List<Applicant>();
      for (int index = 0; index < _out1; ++index)
        this.Applicants.Add(new Applicant(reader));
      int num4 = (int) reader.ReadInt("n", ref this.NewApplicantsNotPopulated);
      int num5 = (int) reader.ReadInt("n", ref this.TotalAmountSpent);
      int num6 = (int) reader.ReadInt("n", ref this.NumberOfPositionsOpened);
      int num7 = (int) reader.ReadBool("n", ref this.IsSocialMediaEnabled);
      int num8 = (int) reader.ReadBool("n", ref this.IsJobPortalEnabled);
      int num9 = (int) reader.ReadBool("n", ref this.IsReferralEnabled);
      int num10 = (int) reader.ReadInt("n", ref _out1);
      if (VersionForLoad < 36)
      {
        if (_out1 == 19)
          _out1 = 0;
        else
          ++_out1;
      }
      this.RoamingEmployeeType = (EmployeeType) _out1;
    }

    public void SaveOpenPositions(Writer writer)
    {
      writer.WriteInt("n", (int) this.tileType);
      writer.WriteInt("n", this.DayStarted);
      writer.WriteInt("n", this.Applicants.Count);
      for (int index = 0; index < this.Applicants.Count; ++index)
        this.Applicants[index].SaveApplicant(writer);
      writer.WriteInt("n", this.NewApplicantsNotPopulated);
      writer.WriteInt("n", this.TotalAmountSpent);
      writer.WriteInt("n", this.NumberOfPositionsOpened);
      writer.WriteBool("n", this.IsSocialMediaEnabled);
      writer.WriteBool("n", this.IsJobPortalEnabled);
      writer.WriteBool("n", this.IsReferralEnabled);
      writer.WriteInt("n", (int) this.RoamingEmployeeType);
    }
  }
}
