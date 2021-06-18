// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Employees.JobApplicants_Calculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment;
using TinyZoo.Z_ManageShop.Shop_Data;

namespace TinyZoo.Z_BalanceSystems.Employees
{
  internal class JobApplicants_Calculator
  {
    private static int MaxCapForDaysOpenedUp = 10;
    internal static bool LimitSearchingWhenFull = false;
    internal static bool UseSingleOpenPositions = true;

    public static void CalculateJobApplicant_OnNewDay(
      Player player,
      OpenPositions JustCheckThisOnInstantOpen = null)
    {
      List<OpenPositions> allOpenPositions = player.employees.openPositionsContainer.GetAllOpenPositions();
      int count = player.employees.employees.Count;
      double parkPopularity = (double) ParkPopularity.GetParkPopularity(player);
      for (int index1 = 0; index1 < allOpenPositions.Count; ++index1)
      {
        if (JustCheckThisOnInstantOpen == null || JustCheckThisOnInstantOpen == allOpenPositions[index1])
        {
          int num1 = 0;
          int ofPositionsOpened = allOpenPositions[index1].NumberOfPositionsOpened;
          int totalReach = JobApplicants_Calculator.CalculateTotalReach(ofPositionsOpened, allOpenPositions[index1].IsSocialMediaEnabled, allOpenPositions[index1].IsJobPortalEnabled);
          if (ofPositionsOpened != 0)
          {
            int num2 = (int) Player.financialrecords.GetDaysPassed() - allOpenPositions[index1].DayStarted;
            EmployeeType employeeType = allOpenPositions[index1].GetEmployeeType();
            int requirementForJob = JobApplicants_Calculator.GetSkillRequirementForJob(employeeType);
            for (int index2 = 0; index2 < totalReach; ++index2)
            {
              if (Game1.Rnd.Next(0, 100) >= requirementForJob)
                ++num1;
            }
            if (Z_DebugFlags.ForceNumberOfJobApplicantsEachDay != -1)
              num1 = Z_DebugFlags.ForceNumberOfJobApplicantsEachDay;
            if (Z_DebugFlags.IsBetaVersion && (employeeType == EmployeeType.Janitor || employeeType == EmployeeType.Architect) && (num1 == 0 && allOpenPositions[index1].GetNumberOfApplicants() == 0 && num2 == 1))
              ++num1;
            if (num1 > 0)
            {
              if (JustCheckThisOnInstantOpen != null)
              {
                float num3 = (Z_GameFlags.GetClosingTime() - Z_GameFlags.DayTimer) / Z_GameFlags.SecondsZooOpenPerDay;
                if ((double) num3 > 1.0)
                  num3 = 1f;
                num1 = (int) ((double) num1 * (double) num3);
              }
              for (int index2 = 0; index2 < num1; ++index2)
                LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.NewApplicant, allOpenPositions[index1].tileType, allOpenPositions[index1].RoamingEmployeeType, JustCheckThisOnInstantOpen != null));
            }
            int totalCostPerDay = JobApplicants_Calculator.CalculateTotalCostPerDay(ofPositionsOpened, allOpenPositions[index1].IsSocialMediaEnabled, allOpenPositions[index1].IsJobPortalEnabled);
            if (JustCheckThisOnInstantOpen != null)
            {
              int SpendThis = 0;
              if (!JustCheckThisOnInstantOpen.TempPaidForAdminOnOpen)
              {
                SpendThis += JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.AdminCost);
                JustCheckThisOnInstantOpen.TempPaidForAdminOnOpen = true;
              }
              if (JustCheckThisOnInstantOpen.IsJobPortalEnabled && !JustCheckThisOnInstantOpen.TempPaidForJobPortalOnOpen)
              {
                SpendThis += JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.JobPortal);
                JustCheckThisOnInstantOpen.TempPaidForJobPortalOnOpen = true;
              }
              if (JustCheckThisOnInstantOpen.IsSocialMediaEnabled && !JustCheckThisOnInstantOpen.TempPaidForSocialMediaOnOpen)
              {
                SpendThis += JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.SocialMedia);
                JustCheckThisOnInstantOpen.TempPaidForSocialMediaOnOpen = true;
              }
              player.Stats.SpendCash(SpendThis, SpendingCashOnThis.HiringCampaign, player);
              allOpenPositions[index1].TotalAmountSpent += SpendThis;
            }
            else
            {
              player.Stats.SpendCash(totalCostPerDay, SpendingCashOnThis.HiringCampaign, player);
              allOpenPositions[index1].TotalAmountSpent += totalCostPerDay;
            }
          }
        }
      }
    }

    public static string GetEstimatedTimeForAnApplicant(
      OpenPositions openPosition,
      out int PeopleWhoSawJobAdvert,
      out int PercentageOfPopulationQualifiedForRole,
      Player player,
      out int PopulationSize,
      out float TotalPerDay)
    {
      PeopleWhoSawJobAdvert = -1;
      PercentageOfPopulationQualifiedForRole = -1;
      PopulationSize = -1;
      TotalPerDay = -1f;
      if (openPosition == null)
        return "-";
      int employeeType = (int) openPosition.GetEmployeeType();
      PopulationSize = 1000;
      PeopleWhoSawJobAdvert = JobApplicants_Calculator.CalculateTotalReach(1, openPosition.IsSocialMediaEnabled, openPosition.IsJobPortalEnabled);
      int requirementForJob = JobApplicants_Calculator.GetSkillRequirementForJob((EmployeeType) employeeType);
      PercentageOfPopulationQualifiedForRole = 100 - requirementForJob;
      TotalPerDay = (float) PeopleWhoSawJobAdvert * ((float) PercentageOfPopulationQualifiedForRole * 0.01f);
      return Z_GameFlags.GetTimeAsStringFomMinutes((int) Math.Ceiling((double) Z_GameFlags.GetInGameSecondsToMinutes(Z_GameFlags.SecondsInDay / TotalPerDay)));
    }

    public static int GetSkillRequirementForJob(EmployeeType employeeType)
    {
      switch (employeeType)
      {
        case EmployeeType.Mascot:
          return 35;
        case EmployeeType.Guide:
          return 25;
        case EmployeeType.Janitor:
          return 5;
        case EmployeeType.Keeper:
          return 15;
        case EmployeeType.Vet:
          return 70;
        case EmployeeType.Mechanic:
          return 55;
        case EmployeeType.SecurityGuard:
          return 25;
        case EmployeeType.Architect:
          return 60;
        case EmployeeType.ShopKeeper:
          return 10;
        case EmployeeType.Breeder:
          return 50;
        case EmployeeType.DNAResearcher:
          return 99;
        case EmployeeType.MeatProcessorWorker:
          return 45;
        case EmployeeType.SlaughterhouseEmployee:
          return 40;
        case EmployeeType.FactoryWorker:
          return 40;
        case EmployeeType.Farmer:
          return 40;
        case EmployeeType.Surgeon:
          return 85;
        case EmployeeType.Trainer:
          return 60;
        case EmployeeType.AnimalPsycologist:
          return 60;
        default:
          return -1;
      }
    }

    public static int GetCostOfThisPerDay(JobPostingModifiers modifier)
    {
      switch (modifier)
      {
        case JobPostingModifiers.AdminCost:
          return 5;
        case JobPostingModifiers.SocialMedia:
          return 30;
        case JobPostingModifiers.JobPortal:
          return 50;
        case JobPostingModifiers.Totals:
          return -1;
        default:
          return 0;
      }
    }

    public static int CalculateTotalReach(
      int NumberOfPositions,
      bool isSocialMediaEnabled,
      bool isJobPortalEnabled)
    {
      int num = 2;
      if (isSocialMediaEnabled)
        num += 3;
      if (isJobPortalEnabled)
        num += 5;
      return num * NumberOfPositions;
    }

    public static int CalculateTotalCostPerDay(
      int NumberOfPositions,
      bool isSocialMediaEnabled,
      bool isJobPortalEnabled)
    {
      int costOfThisPerDay = JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.AdminCost);
      if (isSocialMediaEnabled)
        costOfThisPerDay += JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.SocialMedia);
      if (isJobPortalEnabled)
        costOfThisPerDay += JobApplicants_Calculator.GetCostOfThisPerDay(JobPostingModifiers.JobPortal);
      return costOfThisPerDay * NumberOfPositions;
    }

    public static int GetMaximumOpenPositionsYouCanHave() => 1;

    public static bool IsShopAtMaxCapacity(
      ShopEntry shopEntry,
      EmployeeType roamingEmployeeType,
      Player player,
      out int currentEmployeeCount,
      out int maxEmployeeCount)
    {
      maxEmployeeCount = -1;
      currentEmployeeCount = -1;
      if (shopEntry == null)
      {
        currentEmployeeCount = player.employees.GetEmployeesOfThisType(roamingEmployeeType).Count;
        return false;
      }
      currentEmployeeCount = player.employees.GetEmployeesInThisSpecificShop(shopEntry.ShopUID).Count;
      maxEmployeeCount = ShopData.GetMaximumEmployeesForThisShop(shopEntry.tiletype, player);
      return currentEmployeeCount >= maxEmployeeCount;
    }
  }
}
