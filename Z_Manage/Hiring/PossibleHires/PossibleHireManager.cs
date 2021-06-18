// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.PossibleHires.PossibleHireManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_Manage.Hiring.JobDesc;

namespace TinyZoo.Z_Manage.Hiring.PossibleHires
{
  internal class PossibleHireManager
  {
    internal static bool ForceExitAllTheWay;
    private List<EmployeeButton> employeebuttons;
    private HireInfo hireinfo;
    private int SelectedIndex;
    private LerpHandler_Float lerper;
    private EmployeeType employthis;
    private ScreenHeading screenheading;
    public JobDescriptionDisplay jobdescriptiondisplay;
    private bool UseSmallJobDescription;

    public PossibleHireManager(EmployeeType _employthis, Player player)
    {
      this.screenheading = new ScreenHeading("HIRE STAFF", 70f);
      PossibleHireManager.ForceExitAllTheWay = false;
      this.employthis = _employthis;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.employeebuttons = new List<EmployeeButton>();
      List<PotentialHire> randomHires = player.employees.potentialhires.RandomHires;
      if (_employthis != EmployeeType.Keeper && _employthis != EmployeeType.Mascot)
        throw new Exception("uygfs");
      for (int index = 0; index < randomHires.Count; ++index)
        this.employeebuttons.Add(new EmployeeButton(false, (Employee) null, randomHires[index], _employthis));
      for (int index = 0; index < this.employeebuttons.Count; ++index)
        this.employeebuttons[index].Location.Y = (float) (185.0 + 160.0 * (double) index);
      this.SelectedIndex = 0;
      this.employeebuttons[0].SelectThis(true);
      this.hireinfo = new HireInfo(this.employeebuttons[this.SelectedIndex], false);
      this.UseSmallJobDescription = true;
      if (this.employeebuttons[this.SelectedIndex].thispotentialhire == null || this.employeebuttons[this.SelectedIndex].thispotentialhire.CurrentHireResult != HireResult.NoResult || !this.UseSmallJobDescription)
        return;
      this.jobdescriptiondisplay = new JobDescriptionDisplay(this.employeebuttons[this.SelectedIndex].thispotentialhire, true);
    }

    public PotentialHire GetSelectedPotentialHire() => this.employeebuttons[this.SelectedIndex].thispotentialhire;

    public bool UpdatePossibleHireManager(float DeltaTime, Player player, out bool GoInterview)
    {
      GoInterview = false;
      if (this.jobdescriptiondisplay != null && !this.UseSmallJobDescription)
      {
        if (!this.jobdescriptiondisplay.UpdateJobDescription(DeltaTime, player))
          return false;
        this.jobdescriptiondisplay = (JobDescriptionDisplay) null;
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        for (int index = 0; index < this.employeebuttons.Count; ++index)
        {
          if (this.employeebuttons[index].UpdateEmployeeButton(Vector2.Zero, DeltaTime, player) && index != this.SelectedIndex)
          {
            this.employeebuttons[index].SelectThis(true);
            this.employeebuttons[this.SelectedIndex].SelectThis(false);
            this.SelectedIndex = index;
            this.hireinfo = new HireInfo(this.employeebuttons[this.SelectedIndex], false);
            this.jobdescriptiondisplay = (JobDescriptionDisplay) null;
            if (this.employeebuttons[this.SelectedIndex].thispotentialhire != null && this.employeebuttons[this.SelectedIndex].thispotentialhire.CurrentHireResult == HireResult.NoResult && this.UseSmallJobDescription)
              this.jobdescriptiondisplay = new JobDescriptionDisplay(this.employeebuttons[this.SelectedIndex].thispotentialhire, true);
          }
        }
        bool DisplayJobDescription;
        if (this.hireinfo.UpdateHireInfo(player, Vector2.Zero, DeltaTime, out DisplayJobDescription))
        {
          if (DebugFlags.IsPCVersion)
          {
            GoInterview = true;
            return false;
          }
          player.employees.AddThisEmplyee(this.employeebuttons[this.SelectedIndex].intakeperson, this.employthis, 0, 50, player);
          WalkingPerson NewEmployee = CustomerManager.AddPerson(this.employeebuttons[this.SelectedIndex].intakeperson.animaltype, player: player);
          NewEmployee.ForceRotationAndHold(DirectionPressed.Down, 2f);
          ParkGate.NewEmployeeWantsToGoThoughGate(NewEmployee);
          PossibleHireManager.ForceExitAllTheWay = true;
          return true;
        }
        if (DisplayJobDescription && !this.UseSmallJobDescription)
        {
          this.jobdescriptiondisplay = new JobDescriptionDisplay(this.employeebuttons[this.SelectedIndex].thispotentialhire);
          return false;
        }
      }
      return this.lerper.IsComplete() && (double) this.lerper.Value != 0.0;
    }

    public void DrawPossibleHireManager(Vector2 MasterOffset)
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      for (int index = 0; index < this.employeebuttons.Count; ++index)
        this.employeebuttons[index].DrawEmployeeButton(new Vector2(this.lerper.Value * -1000f, 0.0f) + MasterOffset);
      this.hireinfo.DrawHireInfo(new Vector2(this.lerper.Value * 1000f, 0.0f) + MasterOffset, this.employeebuttons[this.SelectedIndex].bigperson);
      if (this.jobdescriptiondisplay == null)
        return;
      this.jobdescriptiondisplay.DrawJobDescription(this.employeebuttons[this.SelectedIndex].bigperson, this.hireinfo.frame.vLocation + new Vector2(this.lerper.Value * 1000f, 0.0f) + MasterOffset);
    }
  }
}
