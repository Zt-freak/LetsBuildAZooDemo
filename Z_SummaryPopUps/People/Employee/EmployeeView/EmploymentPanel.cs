// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.EmployeeView.EmploymentPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_SummaryPopUps.People.Employee.EmployeeView
{
  internal class EmploymentPanel
  {
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    public Vector2 location;
    private MiniHeading heading;
    private ZGenericText timetext;
    private SalarySliderWithAverage slider;
    private WalkingPerson walkingperson;

    public EmploymentPanel(WalkingPerson walkingperson_, float basescale_, float forceToThisWidth = -1f)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.walkingperson = walkingperson_;
      TinyZoo.PlayerDir.Employee refEmployee = this.walkingperson.simperson.Ref_Employee;
      QuickEmployeeDescription quickemplyeedescription = refEmployee.quickemplyeedescription;
      this.heading = new MiniHeading(Vector2.Zero, "Employment", 1f, this.basescale);
      this.timetext = new ZGenericText("Days Employed: " + (object) refEmployee.DaysEmployed, this.basescale);
      EmployeeStats employeestats = EmployeesStats.GetEmployeestats(refEmployee.employeetype, this.walkingperson.thispersontype, (int) quickemplyeedescription.seniorityLevel);
      int minimumWage = employeestats.MinimumWage;
      int maximumWage = employeestats.MaximumWage;
      int num = (maximumWage - minimumWage) / 2 + minimumWage;
      this.slider = new SalarySliderWithAverage(this.basescale, (float) minimumWage, (float) maximumWage, (float) quickemplyeedescription.CurrentSalary, this.scalehelper.ScaleX(140f), true, averageval: ((float) num));
      this.framescale = 2f * defaultBuffer;
      this.framescale.Y += this.heading.GetSize().Y;
      this.framescale.Y += 0.5f * defaultBuffer.Y;
      this.framescale.Y += this.timetext.GetSize().Y;
      this.framescale.Y += this.slider.GetSize().Y;
      this.framescale.X = this.scalehelper.ScaleX(205f);
      this.framescale.X = Math.Max(this.framescale.X, forceToThisWidth);
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      this.heading.SetTextPosition(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + defaultBuffer;
      vector2.Y += this.heading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.timetext.vLocation = vector2 + 0.5f * this.timetext.GetSize();
      vector2.Y += this.timetext.GetSize().Y;
      this.slider.location = vector2 + 0.5f * this.slider.GetSize();
      this.slider.location.X = 0.0f;
      vector2.Y += this.slider.GetSize().Y + defaultBuffer.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateEmploymentPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawEmploymentPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      this.timetext.DrawZGenericText(offset, spritebatch);
      this.slider.DrawSalarySliderWithAverage(spritebatch, offset);
    }
  }
}
