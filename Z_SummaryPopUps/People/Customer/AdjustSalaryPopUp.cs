// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.AdjustSalaryPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.Emp_Summary;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_SummaryPopUps.People.Employee;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class AdjustSalaryPopUp : CustomerActionPopUp
  {
    private EmployeeSummaryPanel summary;
    private SalarySliderWithAverage slider;
    private TextButton button;
    private float min;
    private float max;
    private float average;
    private float currentsalary;
    private WalkingPerson walkingperson;
    private QuickEmployeeDescription QuickEmployeeDesc;

    public float CurrentSalary => this.currentsalary;

    public AdjustSalaryPopUp(WalkingPerson walkingperson_, float basescale_)
      : base(basescale_)
    {
      this.walkingperson = walkingperson_;
      this.Init(this.walkingperson.simperson.Ref_Employee.employeetype, this.walkingperson.simperson.Ref_Employee.quickemplyeedescription);
    }

    public AdjustSalaryPopUp(QuickEmployeeDescription qed, float basescale_)
      : base(basescale_)
    {
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(qed.thisemployee, out employeetype);
      this.Init(employeetype, qed);
    }

    private void Init(EmployeeType employeetype, QuickEmployeeDescription qed)
    {
      EmployeeStats employeestats = EmployeesStats.GetEmployeestats(employeetype, qed.thisemployee, (int) qed.seniorityLevel);
      this.QuickEmployeeDesc = qed;
      this.min = (float) employeestats.MinimumWage;
      this.max = (float) employeestats.MaximumWage;
      this.average = (float) (((double) this.max - (double) this.min) / 2.0) + this.min;
      this.currentsalary = (float) qed.CurrentSalary;
      this.summary = new EmployeeSummaryPanel(qed, false, false, this.basescale, true);
      this.slider = new SalarySliderWithAverage(this.basescale, this.min, this.max, this.currentsalary, this.uiscale.ScaleX(140f), averageval: this.average);
      this.button = new TextButton(this.basescale, this.walkingperson == null ? "Hire" : "Confirm", 50f);
      this.framescale = 2f * this.pad;
      this.framescale = this.framescale + this.summary.GetSize();
      this.framescale.Y += this.pad.Y;
      this.framescale.Y += this.slider.GetSize().Y;
      this.framescale.Y += this.button.GetSize_True().Y + this.pad.Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.summary.location = vector2 + 0.5f * this.summary.GetSize();
      vector2.Y += this.summary.GetSize().Y + this.pad.Y;
      this.slider.location = vector2 + 0.5f * this.slider.GetSize();
      this.slider.location.X = 0.0f;
      vector2.Y += this.slider.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize_True();
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.summary.UpdateEmployeeSummary(DeltaTime, player, offset);
      this.slider.UpdateSalarySliderWithAverage(player, offset, DeltaTime);
      int num1 = 0 | (this.button.UpdateTextButton(player, offset, DeltaTime) ? 1 : 0);
      float num2 = (float) (((double) this.currentsalary - (double) this.min) / ((double) this.max - (double) this.min));
      this.summary.PreviewStatChange(1, this.summary.GetStatValue(1) + (float) (((double) this.slider.Value - (double) num2) * 0.600000023841858));
      this.summary.PreviewStatChange(4, this.summary.GetStatValue(4) + (float) (((double) this.slider.Value - (double) num2) * 0.600000023841858));
      if (num1 == 0)
        return num1 != 0;
      this.currentsalary = this.slider.Salary;
      this.summary.ApplyChange(1);
      this.summary.ApplyChange(4);
      if (this.walkingperson == null)
        return num1 != 0;
      this.walkingperson.simperson.Ref_Employee.ChangeSalary((int) this.currentsalary);
      return num1 != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.summary.DrawEmployeeSummary(offset, spritebatch);
      this.slider.DrawSalarySliderWithAverage(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
