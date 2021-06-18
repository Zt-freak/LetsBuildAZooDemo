// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.HiringManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Z_Manage.Hiring.CurrentStaff;
using TinyZoo.Z_Manage.Hiring.Interview;
using TinyZoo.Z_Manage.Hiring.PossibleHires;

namespace TinyZoo.Z_Manage.Hiring
{
  internal class HiringManager
  {
    internal static PossibleHireManager possiblehires;
    private CurrentStaffMNG currentstaff;
    private bool ShowingCurrentStaff;
    private InterviewManager interviewmanger;

    public HiringManager(Player player)
    {
      this.ShowingCurrentStaff = true;
      this.currentstaff = new CurrentStaffMNG(player);
    }

    public bool TryToSwitch(Player player)
    {
      if (this.interviewmanger != null)
      {
        this.interviewmanger.Exit();
        return true;
      }
      if (this.ShowingCurrentStaff)
        return false;
      if (HiringManager.possiblehires.jobdescriptiondisplay != null)
        HiringManager.possiblehires.jobdescriptiondisplay.Exit();
      this.ShowingCurrentStaff = true;
      this.currentstaff = new CurrentStaffMNG(player);
      return true;
    }

    public void UpdateHiringManager(Player player, float DeltaTime, Vector2 MasterOffset)
    {
      if (this.interviewmanger != null)
      {
        if (!this.interviewmanger.UpdateInterviewManager(player, DeltaTime, MasterOffset))
          return;
        if (!PossibleHireManager.ForceExitAllTheWay && !this.interviewmanger.newPersonGotJob)
        {
          HiringManager.possiblehires = new PossibleHireManager(this.currentstaff.emplyeetype, player);
        }
        else
        {
          this.interviewmanger.newPersonGotJob = false;
          HiringManager.possiblehires = (PossibleHireManager) null;
        }
        this.interviewmanger = (InterviewManager) null;
      }
      else
      {
        if (this.ShowingCurrentStaff && this.currentstaff.UpdatePossibleCurrentStaff(DeltaTime, player))
        {
          HiringManager.possiblehires = PossibleHireManager.ForceExitAllTheWay ? (PossibleHireManager) null : new PossibleHireManager(this.currentstaff.emplyeetype, player);
          this.ShowingCurrentStaff = false;
        }
        if (this.ShowingCurrentStaff || HiringManager.possiblehires == null)
          return;
        bool GoInterview;
        HiringManager.possiblehires.UpdatePossibleHireManager(DeltaTime, player, out GoInterview);
        if (!GoInterview)
          return;
        this.interviewmanger = new InterviewManager(HiringManager.possiblehires.GetSelectedPotentialHire(), player);
      }
    }

    public void DrawHiringManager(Vector2 MasterOffset)
    {
      if (this.interviewmanger != null)
        this.interviewmanger.DrawInterviewManager(MasterOffset);
      else if (this.ShowingCurrentStaff)
      {
        this.currentstaff.DrawPossibleHireManager();
      }
      else
      {
        if (HiringManager.possiblehires == null)
          return;
        HiringManager.possiblehires.DrawPossibleHireManager(MasterOffset);
      }
    }
  }
}
