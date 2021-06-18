// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringSummary.HiringQuickInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames;
using TinyZoo.Z_ManageEmployees.ManageEmployeeMain.HiringSummary;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringSummary
{
  internal class HiringQuickInfoFrame
  {
    public Vector2 location;
    private ZGenericText header;
    private SimpleTextHandler para;
    private CustomerFrame customerFrame;
    private List<TextButton> textButton;
    private RecruitmentInfoType recruitmentInfo;
    private List<RecruitmentButtonType> buttonTypes;
    private List<RecruitmentInfoIcon> icons;
    private SpinningProgressIconWithText inProgressIcon;

    public HiringQuickInfoFrame(
      RecruitmentInfoType _infoType,
      OpenPositions currentOpenPositions,
      float BaseScale,
      Player player,
      float ForceHeight = -1f,
      float ForceWidth = -1f)
    {
      this.recruitmentInfo = _infoType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num1 = 0.0f;
      float num2 = 0.0f;
      float y1 = num1 + defaultYbuffer;
      float x1 = num2 + defaultXbuffer;
      this.buttonTypes = new List<RecruitmentButtonType>();
      string TextToWrite = string.Empty;
      string _textToWrite = string.Empty;
      bool flag1 = false;
      bool flag2 = false;
      switch (this.recruitmentInfo)
      {
        case RecruitmentInfoType.JobPostings:
          int num3 = 0;
          int num4 = 0;
          if (currentOpenPositions != null)
          {
            num3 = currentOpenPositions.NumberOfPositionsOpened;
            num4 = currentOpenPositions.GetCostPerDay();
            if (num3 > 0)
            {
              flag2 = true;
              this.icons = new List<RecruitmentInfoIcon>();
              this.icons.Add(new RecruitmentInfoIcon(JobPostingModifiers.AdminCost, BaseScale));
              if (currentOpenPositions.IsJobPortalEnabled)
                this.icons.Add(new RecruitmentInfoIcon(JobPostingModifiers.JobPortal, BaseScale));
              if (currentOpenPositions.IsSocialMediaEnabled)
                this.icons.Add(new RecruitmentInfoIcon(JobPostingModifiers.SocialMedia, BaseScale));
              if (currentOpenPositions.IsReferralEnabled)
                this.icons.Add(new RecruitmentInfoIcon(JobPostingModifiers.Referrals, BaseScale));
              this.inProgressIcon = new SpinningProgressIconWithText(BaseScale, "Searching");
            }
          }
          string str1 = TextToWrite + "Current Openings: " + (object) num3 + "~Cost Per Day: $" + (object) num4;
          string str2 = "-";
          if (currentOpenPositions == null || currentOpenPositions.NumberOfPositionsOpened > 0)
            str2 = JobApplicants_Calculator.GetEstimatedTimeForAnApplicant(currentOpenPositions, out int _, out int _, player, out int _, out float _);
          TextToWrite = str1 + "~Average Waiting Time: " + str2;
          _textToWrite = "Job Postings";
          this.buttonTypes.Add(RecruitmentButtonType.ViewJobPostings);
          break;
        case RecruitmentInfoType.Applicants:
          int num5 = 0;
          if (currentOpenPositions != null)
            num5 = currentOpenPositions.GetNumberOfApplicants();
          if (num5 > 0)
          {
            TextToWrite = TextToWrite + "Current Applicants: " + (object) num5;
            flag2 = true;
          }
          else
            TextToWrite += "You have no applicants yet!";
          _textToWrite = "Job Applications";
          this.buttonTypes.Add(RecruitmentButtonType.ViewApplicantsForJobPostings);
          break;
        case RecruitmentInfoType.HiringAgency:
          _textToWrite = "Recruitment Agency";
          TextToWrite = "Use money to instantly hire a staff";
          this.buttonTypes.Add(RecruitmentButtonType.HireFromAgency);
          if (Z_DebugFlags.IsBetaVersion)
          {
            flag1 = true;
            break;
          }
          break;
      }
      this.header = new ZGenericText(_textToWrite, BaseScale, false, _UseOnePointFiveFont: true);
      this.header.vLocation = new Vector2(x1, y1);
      float y2 = y1 + this.header.GetSize().Y + defaultYbuffer;
      if (this.icons != null)
      {
        for (int index = 0; index < this.icons.Count; ++index)
        {
          float x2 = this.header.vLocation.X + this.header.GetSize().X + defaultXbuffer;
          this.icons[index].vLocation = new Vector2(x2, this.header.vLocation.Y + this.header.GetSize().Y * 0.5f);
          this.icons[index].vLocation.X += this.icons[index].GetSize().X * 0.5f;
          this.icons[index].vLocation.X += (this.icons[index].GetSize().X + defaultXbuffer * 0.5f) * (float) index;
        }
      }
      float Length = 55f;
      float width_ = uiScaleHelper.ScaleX(150f);
      if ((double) ForceWidth != -1.0)
        width_ = ForceWidth - defaultXbuffer - uiScaleHelper.ScaleX(Length) - defaultXbuffer;
      this.para = new SimpleTextHandler(TextToWrite, width_, _Scale: BaseScale, AutoComplete: true);
      this.para.AutoCompleteParagraph();
      this.para.Location = new Vector2(x1, y2);
      this.para.SetAllColours(ColourData.Z_Cream);
      float y3 = y2 + this.para.GetHeightOfParagraph() + defaultYbuffer;
      float x3 = x1 + Math.Max(this.header.GetSize().X, this.para.GetSize(true).X) + defaultXbuffer;
      if ((double) ForceWidth != -1.0)
        x3 = ForceWidth;
      if ((double) ForceHeight != -1.0)
        y3 = ForceHeight;
      this.textButton = new List<TextButton>();
      for (int index = 0; index < this.buttonTypes.Count; ++index)
      {
        TextButton textButton1 = new TextButton(BaseScale, this.GetButtonString(this.buttonTypes[index], this.inProgressIcon != null), Length, _OverrideFrameScale: BaseScale);
        textButton1.vLocation.X = x3 - defaultXbuffer;
        textButton1.vLocation.Y = y3 - defaultYbuffer;
        TextButton textButton2 = textButton1;
        textButton2.vLocation = textButton2.vLocation - textButton1.GetSize_True() * 0.5f;
        if (this.buttonTypes[index] == RecruitmentButtonType.ViewApplicantsForJobPostings)
        {
          if (currentOpenPositions == null || currentOpenPositions.GetNumberOfApplicants() == 0)
          {
            textButton1.SetButtonColour(BTNColour.Grey);
            textButton1.Locked = true;
          }
        }
        else if (this.buttonTypes[index] == RecruitmentButtonType.HireFromAgency)
          textButton1.SetButtonColour(BTNColour.Blue);
        this.textButton.Add(textButton1);
      }
      if (this.inProgressIcon != null)
      {
        this.inProgressIcon.location.X = this.textButton[0].vLocation.X;
        this.inProgressIcon.location.X -= this.inProgressIcon.GetSize().X * 0.5f;
        this.inProgressIcon.location.Y += this.inProgressIcon.GetSize().Y * 0.5f;
        this.inProgressIcon.location.Y += defaultYbuffer * 0.7f;
      }
      Vector3 color = ColourData.Z_FrameMidBrown;
      if (flag2)
        color = ColourData.Z_FrameGreenPale;
      this.customerFrame = new CustomerFrame(new Vector2(x3, y3), color, BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      this.para.Location += vector2;
      for (int index = 0; index < this.textButton.Count; ++index)
      {
        TextButton textButton = this.textButton[index];
        textButton.vLocation = textButton.vLocation + vector2;
      }
      ZGenericText header = this.header;
      header.vLocation = header.vLocation + vector2;
      if (this.icons != null)
      {
        for (int index = 0; index < this.icons.Count; ++index)
        {
          RecruitmentInfoIcon icon = this.icons[index];
          icon.vLocation = icon.vLocation + vector2;
        }
      }
      if (this.inProgressIcon != null)
        this.inProgressIcon.location += vector2;
      bool flag3 = Z_DebugFlags.IsBetaVersion && _infoType == RecruitmentInfoType.HiringAgency;
      if (!(flag1 | flag3))
        return;
      if (flag3)
        this.customerFrame.LockForBeta();
      else
        this.customerFrame.Active = false;
      for (int index = 0; index < this.textButton.Count; ++index)
        this.textButton[index].Locked = true;
    }

    private string GetButtonString(RecruitmentButtonType buttonType, bool isActive = false)
    {
      switch (buttonType)
      {
        case RecruitmentButtonType.ViewJobPostings:
          return isActive ? "Edit" : "Recruit";
        case RecruitmentButtonType.ViewApplicantsForJobPostings:
          return "View";
        case RecruitmentButtonType.HireFromAgency:
          return "Quick Hire";
        default:
          return "NA";
      }
    }

    public RecruitmentButtonType UpdateHiringQuickInfoFrame(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.textButton.Count; ++index)
      {
        if (!this.textButton[index].Locked && this.textButton[index].UpdateTextButton(player, offset, DeltaTime))
          return this.buttonTypes[index];
      }
      if (this.inProgressIcon != null)
        this.inProgressIcon.UpdateSpinningProgressIconWithText(DeltaTime);
      return RecruitmentButtonType.Count;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void DrawHiringQuickInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
      this.para.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      for (int index = 0; index < this.textButton.Count; ++index)
        this.textButton[index].DrawTextButton(offset, 1f, spriteBatch);
      if (this.icons != null)
      {
        for (int index = 0; index < this.icons.Count; ++index)
          this.icons[index].DrawRecruitmentInfoIcon(offset, spriteBatch);
      }
      if (this.inProgressIcon != null)
        this.inProgressIcon.DrawSpinningProgressIconWithText(offset, spriteBatch);
      this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
