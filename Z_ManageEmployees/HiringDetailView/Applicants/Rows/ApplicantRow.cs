// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.Rows.ApplicantRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.Rows
{
  internal class ApplicantRow
  {
    public Vector2 location;
    private ZGenericText[] headerTexts;
    private PerformanceTableRowFrame colouredFrame;
    private AnimalInFrame animalInFrame;
    private ZGenericText nameText;
    private StarBar starRating;
    private SimpleTextHandler seniorityText;
    private ZGenericText salaryText;
    private ZGenericText agencySalaryText;
    private LittleSummaryButton littleSummaryButton_hire;
    private LittleSummaryButton littleSummaryButton_dismiss;
    private LerpHandler_Float slidingLerper;
    private float lerpDistance;
    public bool DoneSliding;
    public LerpHandler_Float entryExitLerper;

    public Applicant refApplicant { get; private set; }

    public ApplicantRow(
      Applicant applicant,
      float BaseScale,
      ref float[] widths,
      bool isAgencyHire = false,
      bool isHeader = false,
      bool LerpInSmoothly = false)
    {
      this.refApplicant = applicant;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultYbuffer = (double) uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num1 = 0.0f;
      for (int index = 0; index < widths.Length; ++index)
        num1 += widths[index];
      float num2 = num1 * 0.5f;
      float num3 = 0.0f;
      for (int index = 0; index < 6; ++index)
      {
        float num4 = num3 + widths[index] * 0.5f;
        if (isHeader)
        {
          if (this.headerTexts == null)
            this.headerTexts = new ZGenericText[6];
          string _textToWrite = string.Empty;
          switch (index)
          {
            case 0:
              _textToWrite = "Applicant";
              break;
            case 1:
              _textToWrite = "Rating";
              break;
            case 2:
              _textToWrite = "Experience";
              break;
            case 3:
              _textToWrite = "Salary";
              break;
            case 4:
              _textToWrite = "Hire";
              break;
            case 5:
              _textToWrite = "Dismiss";
              break;
          }
          this.headerTexts[index] = new ZGenericText(_textToWrite, BaseScale);
          this.headerTexts[index].vLocation.X = num4 - num2;
        }
        else
        {
          switch (index)
          {
            case 0:
              this.animalInFrame = new AnimalInFrame(applicant.animalType, AnimalType.None, TargetSize: (25f * BaseScale), FrameEdgeBuffer: (6f * BaseScale), BaseScale: BaseScale);
              Vector2 size = this.animalInFrame.GetSize();
              this.animalInFrame.Location.X = -num2;
              this.animalInFrame.Location.X += (float) ((double) size.X * 0.5 + (double) defaultXbuffer * 0.75);
              this.nameText = new ZGenericText(applicant.Name, BaseScale);
              this.nameText.vLocation.X = (float) ((double) this.animalInFrame.Location.X + (double) this.animalInFrame.FrameVSCALE.X * 0.5 + (double) this.nameText.GetSize().X * 0.5 + (double) defaultXbuffer * 0.75);
              break;
            case 1:
              this.starRating = new StarBar(applicant.StarRating, BaseScale, true);
              this.starRating.Location.X = num4 - num2;
              break;
            case 2:
              this.seniorityText = new SimpleTextHandler(EmployeesStats.GetSeniorityPrepend(applicant.SeniorityLevel).Trim(), true, (float) ((double) widths[index] / 1024.0 * 0.899999976158142), BaseScale);
              this.seniorityText.SetAllColours(ColourData.Z_Cream);
              this.seniorityText.AutoCompleteParagraph();
              this.seniorityText.Location.X = num4 - num2;
              if (this.seniorityText.paragraph.linemaker.GetNumberOfLines() > 1)
              {
                this.seniorityText.Location.Y -= this.seniorityText.GetHeightOfOneLine() * 0.5f;
                break;
              }
              break;
            case 3:
              this.salaryText = new ZGenericText("$" + (object) applicant.minSalary + " - $" + (object) applicant.maxSalary, BaseScale);
              this.salaryText.vLocation.X = num4 - num2;
              break;
            case 4:
              this.littleSummaryButton_hire = new LittleSummaryButton(LittleSummaryButtonType.Hire, _BaseScale: BaseScale);
              this.littleSummaryButton_hire.vLocation.X = num4 - num2;
              break;
            case 5:
              this.littleSummaryButton_dismiss = new LittleSummaryButton(LittleSummaryButtonType.TrashBin, _BaseScale: BaseScale);
              this.littleSummaryButton_dismiss.vLocation.X = num4 - num2;
              break;
          }
        }
        num3 = num4 + widths[index] * 0.5f;
      }
      float num5 = 35f;
      if (isHeader)
        num5 = 20f;
      this.colouredFrame = new PerformanceTableRowFrame(BaseScale, uiScaleHelper.ScaleY(num5), false, false, widths);
      this.slidingLerper = new LerpHandler_Float();
      this.entryExitLerper = new LerpHandler_Float();
      if (LerpInSmoothly)
        this.entryExitLerper.SetLerp(true, 0.0f, 1f, 3f);
      else
        this.entryExitLerper.Value = 1f;
    }

    public Vector2 GetSize() => this.colouredFrame.GetSize();

    public Applicant UpdateApplicantRow(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool isDismiss,
      bool BlockButtonClick = false)
    {
      offset += this.location;
      isDismiss = false;
      this.slidingLerper.UpdateLerpHandler(DeltaTime);
      this.entryExitLerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.slidingLerper.Value != 0.0 || !this.IsDoneLerping())
        return (Applicant) null;
      if (BlockButtonClick)
        return (Applicant) null;
      if (this.littleSummaryButton_hire.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return this.refApplicant;
      if (!this.littleSummaryButton_dismiss.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return (Applicant) null;
      isDismiss = true;
      return this.refApplicant;
    }

    public void RemoveRow()
    {
      this.entryExitLerper = new LerpHandler_Float();
      this.entryExitLerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void LerpThisUp(float distance)
    {
      this.slidingLerper = new LerpHandler_Float();
      this.DoneSliding = false;
      this.slidingLerper.SetLerp(true, 1f, 0.0f, 3f);
      this.lerpDistance += distance;
      this.location.Y -= this.lerpDistance;
    }

    public bool IsDoneLerping() => this.DoneSliding && (double) this.entryExitLerper.Value == 1.0;

    public void DrawApplicantRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.slidingLerper.Value * this.lerpDistance;
      if ((double) this.slidingLerper.Value == (double) this.slidingLerper.TargetValue)
      {
        this.DoneSliding = true;
        this.lerpDistance = 0.0f;
      }
      this.colouredFrame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.headerTexts != null)
      {
        for (int index = 0; index < this.headerTexts.Length; ++index)
          this.headerTexts[index].DrawZGenericText(offset, spriteBatch, this.entryExitLerper.Value);
      }
      else
      {
        this.animalInFrame.DrawAnimalInFrame(offset, spriteBatch);
        this.nameText.DrawZGenericText(offset, spriteBatch);
        this.starRating.DrawStarBar(offset, spriteBatch);
        this.seniorityText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
        this.salaryText.DrawZGenericText(offset, spriteBatch, this.entryExitLerper.Value);
        if (this.agencySalaryText != null)
          this.agencySalaryText.DrawZGenericText(offset, spriteBatch, this.entryExitLerper.Value);
        this.littleSummaryButton_hire.DrawLittleSummaryButton(offset, spriteBatch);
        this.littleSummaryButton_dismiss.DrawLittleSummaryButton(offset, spriteBatch);
      }
    }
  }
}
