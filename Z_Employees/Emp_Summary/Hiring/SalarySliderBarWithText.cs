// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.Emp_Summary.Hiring.SalarySliderBarWithText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Employees.Emp_Summary.Hiring
{
  internal class SalarySliderBarWithText
  {
    public Vector2 location;
    private ZGenericText salaryHeaderText;
    private ZGenericText salaryAmountText;
    private string baseAgencyText;
    private string SecondAgencyText;
    private DragAndBar paySlider;
    private int minSalary;
    private int maxSalary;
    private int currentSalaryAmount;
    private float totalWidth;
    private float extraTextHeight;

    public SalarySliderBarWithText(
      QuickEmployeeDescription emplyeedescription,
      float BaseScale,
      float forceWidth,
      float Xbuffer,
      float Ybuffer)
    {
      this.extraTextHeight = 0.0f;
      this.totalWidth = forceWidth;
      float num1 = 0.0f;
      EmployeeType employeetype;
      EmployeeData.IsThisAnEmployee(emplyeedescription.thisemployee, out employeetype);
      EmployeeStats employeestats = EmployeesStats.GetEmployeestats(employeetype, emplyeedescription.thisemployee, (int) emplyeedescription.seniorityLevel);
      this.minSalary = employeestats.MinimumWage;
      this.maxSalary = employeestats.MaximumWage;
      float num2 = num1 + Xbuffer * 3f;
      this.salaryHeaderText = new ZGenericText("Salary:", BaseScale);
      this.salaryHeaderText.GetSize();
      this.salaryHeaderText.vLocation.X = num2;
      this.salaryHeaderText.vLocation.Y -= 5f;
      this.salaryAmountText = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
      this.salaryAmountText.vLocation.X = num2;
      this.salaryAmountText.vLocation.Y += 5f;
      float num3 = num2 + Xbuffer * 3f;
      this.paySlider = new DragAndBar(false, (float) (emplyeedescription.CurrentSalary - this.minSalary) / (float) (this.maxSalary - this.minSalary), this.totalWidth * 0.6f, BaseScale);
      this.paySlider.Location.X = num3 + this.paySlider.VSCALEOutSide.X * 0.5f;
    }

    public Vector2 GetSize() => new Vector2(this.totalWidth, this.paySlider.VSCALEOutSide.Y + this.extraTextHeight);

    public float GetOffsetFromTop() => this.paySlider.VSCALEOutSide.Y * 0.5f;

    public void UpdateSalarySliderBarWithText(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.paySlider.UpdateDragAndBar(player, DeltaTime, offset);
      this.currentSalaryAmount = (int) ((double) this.minSalary + (double) (this.maxSalary - this.minSalary) * (double) this.paySlider.CurrentDragPercent);
      this.salaryAmountText.textToWrite = "$" + this.currentSalaryAmount.ToString();
    }

    public int GetPaySliderSalaryValue() => this.currentSalaryAmount;

    public void DrawSalarySliderBarWithText(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.paySlider.DrawDragAndBar(spriteBatch, offset);
      this.salaryHeaderText.DrawZGenericText(offset, spriteBatch);
      this.salaryAmountText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
