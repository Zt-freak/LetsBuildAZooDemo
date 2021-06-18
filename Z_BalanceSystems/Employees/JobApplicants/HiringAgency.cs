// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.Employees.JobApplicants.HiringAgency
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.PlayerDir;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment;

namespace TinyZoo.Z_BalanceSystems.Employees.JobApplicants
{
  internal class HiringAgency
  {
    public static int GetAgencyFeeToHireThisPerson(EmployeeType employeeType)
    {
      int requirementForJob = JobApplicants_Calculator.GetSkillRequirementForJob(employeeType);
      int num = 0;
      for (int index = 0; index < 3; ++index)
        num += JobApplicants_Calculator.GetCostOfThisPerDay((JobPostingModifiers) index);
      return num * 4 + (int) ((double) (num * requirementForJob) * 0.100000001490116);
    }
  }
}
