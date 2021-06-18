// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.GiveBonusPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Employees.Emp_Summary;
using TinyZoo.Z_SummaryPopUps.People.Employee;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class GiveBonusPopUp : CustomerActionPopUp
  {
    private EmployeeSummaryPanel summary;
    private SalarySliderWithAverage slider;
    private TextButton button;
    private float min;
    private float max;
    private bool disablebutton;
    private WalkingPerson walkingperson;

    public GiveBonusPopUp(WalkingPerson walkingperson_, float basescale_)
      : base(basescale_)
    {
      this.min = 1f;
      this.max = 150f;
      this.walkingperson = walkingperson_;
      this.summary = new EmployeeSummaryPanel(this.walkingperson.simperson.Ref_Employee.quickemplyeedescription, false, false, this.basescale, true);
      this.slider = new SalarySliderWithAverage(this.basescale, this.min, this.max, this.min, this.uiscale.ScaleX(140f), label: "Bonus");
      this.button = new TextButton(this.basescale, "Apply", 50f);
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
      bool flag = false;
      this.summary.UpdateEmployeeSummary(DeltaTime, player, offset);
      this.slider.UpdateSalarySliderWithAverage(player, offset, DeltaTime);
      this.summary.PreviewStatChange(1, this.summary.GetStatValue(1) + this.slider.Value * 0.2f);
      this.summary.PreviewStatChange(4, this.summary.GetStatValue(4) + this.slider.Value * 0.3f);
      if ((double) this.slider.Value == 0.0)
      {
        this.button.SetButtonColour(BTNColour.Grey);
        this.disablebutton = true;
      }
      else
      {
        this.disablebutton = false;
        this.button.SetButtonColour(BTNColour.Green);
      }
      if (!this.disablebutton)
        flag |= this.button.UpdateTextButton(player, offset, DeltaTime);
      if (flag)
      {
        this.walkingperson.simperson.Ref_Employee.quickemplyeedescription.GiveBonus((int) this.slider.Salary);
        this.summary.ApplyChange(1);
        this.summary.ApplyChange(4);
        this.slider.SetToMinimum();
      }
      return false;
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
