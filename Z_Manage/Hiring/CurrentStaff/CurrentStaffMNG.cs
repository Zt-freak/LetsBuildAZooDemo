// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.CurrentStaff.CurrentStaffMNG
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees;
using TinyZoo.Z_Manage.Hiring.JobDesc;
using TinyZoo.Z_Manage.Hiring.PossibleHires;

namespace TinyZoo.Z_Manage.Hiring.CurrentStaff
{
  internal class CurrentStaffMNG
  {
    private List<EmployeeButton> employeebuttons;
    private HireInfo hireinfo;
    private int SelectedIndex;
    private JobDescriptionDisplay jobdescriptiondisplay;
    private LerpHandler_Float lerper;
    private ScreenHeading screenheading;
    public EmployeeType emplyeetype;

    public CurrentStaffMNG(Player player)
    {
      this.screenheading = new ScreenHeading("STAFF MANAGEMENT", 70f);
      this.employeebuttons = new List<EmployeeButton>();
      this.employeebuttons.Add(new EmployeeButton(true, (Employee) null, (PotentialHire) null, EmployeeType.Mascot));
      for (int index = 0; index < player.employees.employees.Count; ++index)
        this.employeebuttons.Add(new EmployeeButton(false, player.employees.employees[index], (PotentialHire) null, player.employees.employees[index].employeetype));
      for (int index = 0; index < this.employeebuttons.Count; ++index)
        this.employeebuttons[index].Location.Y = !DebugFlags.IsPCVersion ? (float) (185.0 + 160.0 * (double) index) : (float) (185.0 + 80.0 * (double) index * (double) Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.SelectedIndex = 0;
      this.employeebuttons[0].SelectThis(true);
      this.hireinfo = new HireInfo(this.employeebuttons[0], true);
    }

    public bool UpdatePossibleCurrentStaff(float DeltaTime, Player player)
    {
      if (this.jobdescriptiondisplay != null)
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
            this.hireinfo = new HireInfo(this.employeebuttons[this.SelectedIndex], true);
          }
        }
        bool DisplayJobDescription;
        if (this.hireinfo.UpdateHireInfo(player, Vector2.Zero, DeltaTime, out DisplayJobDescription))
          this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
        else if (DisplayJobDescription)
        {
          this.jobdescriptiondisplay = new JobDescriptionDisplay((PotentialHire) null);
          return false;
        }
      }
      return this.lerper.IsComplete() && (double) this.lerper.Value != 0.0;
    }

    public void DrawPossibleHireManager()
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      for (int index = 0; index < this.employeebuttons.Count; ++index)
        this.employeebuttons[index].DrawEmployeeButton(new Vector2(this.lerper.Value * -1000f, 0.0f));
      this.hireinfo.DrawHireInfo(new Vector2(this.lerper.Value * 1000f, 0.0f), this.employeebuttons[this.SelectedIndex].bigperson);
      if (this.jobdescriptiondisplay == null)
        return;
      this.jobdescriptiondisplay.DrawJobDescription(this.employeebuttons[this.SelectedIndex].bigperson, new Vector2(this.lerper.Value * 1000f, 0.0f));
    }
  }
}
