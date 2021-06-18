// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.Employee.MoveToGatePopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_Employees.Emp_Summary;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.Employee
{
  internal class MoveToGatePopUp : CustomerActionPopUp
  {
    private EmployeeSummaryPanel summary;
    private TextButton button;
    private float min;
    private float max;
    private float average;
    private float currentsalary;
    private SimpleTextHandler text;
    private WalkingPerson walkingperson;

    public MoveToGatePopUp(WalkingPerson walkingperson_, float basescale_, bool allcheckbox_ = false)
      : base(basescale_)
    {
      this.min = 60f;
      this.max = 100f;
      this.average = 80f;
      this.walkingperson = walkingperson_;
      this.text = new SimpleTextHandler("If a person gets lost, the best thing to do is to lend them a helping hand and transport them back to the gate.", 250f * this.basescale, true, basescale_, AutoComplete: true);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.button = new TextButton(this.basescale, "Move", 50f);
      this.framescale = 2f * this.pad;
      this.framescale = this.framescale + this.text.GetSize(true);
      this.framescale.Y += this.pad.Y;
      this.framescale.Y += this.button.GetSize_True().Y;
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize_True();
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y;
      vector2.Y += this.pad.Y;
      this.text.Location.Y = vector2.Y;
      this.text.Location.Y += this.text.GetHeightOfOneLine() * 0.5f;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num1 = 0 | (this.button.UpdateTextButton(player, offset, DeltaTime) ? 1 : 0);
      double num2 = ((double) this.currentsalary - (double) this.min) / ((double) this.max - (double) this.min);
      if (num1 == 0)
        return num1 != 0;
      this.walkingperson.TeleportToGateNextUpdate = true;
      return num1 != 0;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.text.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.button.DrawTextButton(offset, 1f, spritebatch);
    }
  }
}
