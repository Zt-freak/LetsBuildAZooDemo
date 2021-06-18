// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.SalarySliderWithAverage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee
{
  internal class SalarySliderWithAverage
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private SliderWithAverage slider;
    private ZGenericText salarytext;
    private ZGenericText salaryvaltext;
    private ZGenericText totaltext;
    private ZGenericText agencytext;
    private float salary;
    private GameObject line;
    private bool withAgencyFee;
    private float min;
    private float max;
    private bool usePercentNotDollars;
    private float normalisedval;

    public float Value => this.normalisedval;

    public float Salary => this.salary;

    public float Average => this.slider.Average;

    public SalarySliderWithAverage(
      float basescale_,
      float min_,
      float max_,
      float initialval,
      float sliderwidth,
      bool hidehandle = false,
      bool withAgencyFee_ = false,
      float averageval = -1f,
      bool usePercentNotDollars_ = false,
      string label = "Salary")
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.scalehelper.DefaultBuffer;
      this.usePercentNotDollars = usePercentNotDollars_;
      this.withAgencyFee = withAgencyFee_;
      this.salary = initialval;
      this.min = min_;
      this.max = max_;
      float initial0to1 = (float) (((double) initialval - (double) this.min) / ((double) this.max - (double) this.min));
      float average0to1 = (float) (((double) averageval - (double) this.min) / ((double) this.max - (double) this.min));
      this.slider = new SliderWithAverage(this.basescale, initial0to1, sliderwidth, hidehandle, average0to1, "Market Rate");
      if (this.usePercentNotDollars)
      {
        this.salarytext = new ZGenericText("Percent:", this.basescale, false);
        this.salaryvaltext = new ZGenericText(this.salary.ToString() + "%", this.basescale, false, _UseOnePointFiveFont: true);
      }
      else
      {
        this.salarytext = new ZGenericText(label, this.basescale, false);
        this.salaryvaltext = new ZGenericText("$" + (object) this.salary, this.basescale, false, _UseOnePointFiveFont: true);
      }
      double num1 = (double) this.scalehelper.ScaleX(40f);
      this.framescale = new Vector2();
      float num2 = Math.Max(this.salarytext.GetSize().Y + this.salaryvaltext.GetSize().Y, this.slider.GetSize().Y);
      this.framescale.Y = num2;
      if (this.withAgencyFee)
      {
        this.framescale.Y += 0.5f * defaultBuffer.Y;
        this.framescale.Y += this.totaltext.GetSize().Y;
        this.framescale.Y += this.agencytext.GetSize().Y;
      }
      this.framescale.X += this.salarytext.GetSize().X + 0.5f * defaultBuffer.X;
      this.framescale.X += this.slider.GetSize().X;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2_1 = -0.5f * this.framescale;
      vector2_1.Y += 0.5f * num2;
      this.salarytext.vLocation = vector2_1;
      this.salarytext.vLocation.Y -= this.salarytext.GetSize().Y;
      this.salaryvaltext.vLocation = vector2_1;
      this.slider.location = vector2_1;
      this.slider.location.X += (float) ((double) this.salarytext.GetSize().X + 0.5 * (double) defaultBuffer.X + 0.5 * (double) this.slider.GetSize().X);
      Vector2 vector2_2 = -0.5f * this.framescale;
      vector2_2.Y += num2 + 0.5f * defaultBuffer.Y;
      if (!this.withAgencyFee)
        return;
      this.totaltext.vLocation = vector2_2;
      vector2_2.Y += this.totaltext.GetSize().Y;
      this.agencytext.vLocation = vector2_2;
      vector2_2.Y += this.agencytext.GetSize().Y;
    }

    public void SetToMinimum() => this.slider.SetToMinimum();

    public Vector2 GetSize() => this.framescale;

    public bool UpdateSalarySliderWithAverage(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num = 0;
      this.slider.UpdateSliderWithAverage(player, offset, DeltaTime);
      this.normalisedval = this.slider.Value;
      this.salary = this.min + this.normalisedval * (this.max - this.min);
      if (this.usePercentNotDollars)
      {
        this.salaryvaltext.textToWrite = this.salary.ToString("f0") + "%";
        return num != 0;
      }
      this.salaryvaltext.textToWrite = "$" + this.salary.ToString("f0");
      return num != 0;
    }

    public void DrawSalarySliderWithAverage(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.slider.DrawSliderWithAverage(spritebatch, offset);
      this.salarytext.DrawZGenericText(offset, spritebatch);
      this.salaryvaltext.DrawZGenericText(offset, spritebatch);
      if (!this.withAgencyFee)
        return;
      this.totaltext.DrawZGenericText(offset, spritebatch);
      this.agencytext.DrawZGenericText(offset, spritebatch);
    }
  }
}
