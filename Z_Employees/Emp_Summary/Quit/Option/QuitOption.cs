// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.Quit.Option.QuitOption
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Employees.Emp_Summary.Quit.Option
{
  internal class QuitOption
  {
    public Vector2 location;
    private QuitOptions option;
    public CustomerFrame frame;
    private TextButton textButton;
    private DragAndBar paySlider;
    private int currentSalary;
    private int minSalary;
    private int maxSalary;
    private GameObject salaryTextObject;
    private GameObject salaryIncreaseTextObject;
    private int tempSalarySet;

    public QuitOption(QuitOptions _option, QuickEmployeeDescription employee)
    {
      this.option = _option;
      this.frame = new CustomerFrame(new Vector2(310f, 70f));
      float OverAllMultiplier = 0.6666f;
      string upper = QuitOption.GetQuitOptionToString(this.option).ToUpper();
      float Length = SpringFontUtil.MeasureString(upper, AssetContainer.springFont).X + 10f;
      this.textButton = new TextButton(upper, Length, OverAllMultiplier: OverAllMultiplier);
      switch (_option)
      {
        case QuitOptions.OfferPayrise:
          EmployeeType employeetype;
          EmployeeData.IsThisAnEmployee(employee.thisemployee, out employeetype);
          this.maxSalary = EmployeesStats.GetEmployeestats(employeetype, employee.thisemployee).MaximumWage;
          this.currentSalary = employee.CurrentSalary;
          this.tempSalarySet = this.currentSalary;
          this.paySlider = new DragAndBar(1f, false, 0.0f, this.frame.VSCale.X * 0.4f, 0.4f);
          this.paySlider.Location.X = (float) (-(double) this.frame.VSCale.X * 0.5 + (double) this.paySlider.VSCALEOutSide.X * 0.5 + 15.0);
          this.textButton.vLocation.X += 70f;
          this.textButton.SetButtonColour(BTNColour.Grey);
          this.salaryTextObject = new GameObject();
          this.salaryTextObject.SetAllColours(ColourData.Z_Cream);
          this.salaryTextObject.scale = 1.2f;
          this.salaryTextObject.vLocation = this.paySlider.Location;
          this.salaryTextObject.vLocation.X -= this.paySlider.VSCALEOutSide.X * 0.5f;
          this.salaryTextObject.vLocation.Y -= this.paySlider.VSCALEOutSide.Y + 10f;
          this.salaryIncreaseTextObject = new GameObject();
          this.salaryIncreaseTextObject.SetAllColours(ColourData.Z_Cream);
          this.salaryIncreaseTextObject.scale = 1.2f;
          this.salaryIncreaseTextObject.vLocation = this.salaryTextObject.vLocation;
          this.salaryIncreaseTextObject.vLocation.Y += 40f;
          break;
        case QuitOptions.AcceptResignation:
          this.textButton.SetButtonColour(BTNColour.Red);
          this.textButton.Locked = false;
          break;
      }
      this.textButton.stringinabox.Frame.scale *= 0.5f;
    }

    public QuitOptions UpdateQuitOption(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out int NewPayIfApplicable)
    {
      offset += this.location;
      NewPayIfApplicable = 0;
      if (this.paySlider != null)
      {
        this.paySlider.UpdateDragAndBar(player, DeltaTime, offset);
        this.tempSalarySet = (int) ((double) this.currentSalary + (double) this.maxSalary * (double) this.paySlider.CurrentDragPercent);
        if (this.tempSalarySet > this.currentSalary && this.textButton.Locked)
        {
          this.textButton.SetButtonColour(BTNColour.Green);
          this.textButton.Locked = false;
          this.textButton.stringinabox.Frame.scale *= 0.5f;
        }
        else if (!this.textButton.Locked)
        {
          this.textButton.SetButtonColour(BTNColour.Grey);
          this.textButton.Locked = true;
          this.textButton.stringinabox.Frame.scale *= 0.5f;
        }
      }
      if (!this.textButton.UpdateTextButton(player, offset, DeltaTime) || this.textButton.Locked)
        return QuitOptions.Count;
      if (this.paySlider != null)
        NewPayIfApplicable = this.tempSalarySet;
      return this.option;
    }

    public static string GetQuitOptionToString(QuitOptions _option)
    {
      if (_option == QuitOptions.OfferPayrise)
        return "Offer Pay Rise";
      return _option == QuitOptions.AcceptResignation ? "Accept Resignation" : "NA";
    }

    public void DrawQuitOption(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spriteBatch);
      if (this.paySlider != null)
        this.paySlider.DrawDragAndBar(spriteBatch, offset);
      if (this.salaryTextObject != null)
        TextFunctions.DrawTextWithDropShadow("Current Salary: $" + (object) this.currentSalary, this.salaryTextObject.scale, this.salaryTextObject.vLocation + offset, this.salaryTextObject.GetColour(), 1f, AssetContainer.springFont, spriteBatch, false);
      if (this.salaryIncreaseTextObject != null)
      {
        TextFunctions.DrawTextWithDropShadow("Pay Increase: ", this.salaryIncreaseTextObject.scale, this.salaryIncreaseTextObject.vLocation + offset, this.salaryIncreaseTextObject.GetColour(), 1f, AssetContainer.springFont, spriteBatch, false);
        TextFunctions.DrawTextWithDropShadow("$" + (this.tempSalarySet - this.currentSalary).ToString(), this.salaryIncreaseTextObject.scale * 2f, this.salaryIncreaseTextObject.vLocation + offset + new Vector2(120f, -5f), this.salaryIncreaseTextObject.GetColour(), 1f, AssetContainer.springFont, spriteBatch, false, true);
      }
      this.textButton.DrawTextButton(offset, 1f, spriteBatch);
    }
  }
}
