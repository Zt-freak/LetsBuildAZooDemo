// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.CampaignSummaryFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class CampaignSummaryFrame
  {
    public Vector2 location;
    private MiniHeading miniHeading;
    private CustomerFrame customerFrame;
    private ZGenericText TotalSpentSoFar;
    private ZGenericText TotalDaysPassed;
    private ZGenericText ApplicantsRecruited;
    private EstimatedWaitTimeFrame EstimatedWaitTime;
    private LittleSummaryInfoCloseToggle infoButton;
    private OpenPositions TEMPOPENPOSITIONS;
    private OpenPositions ORIGINALOPENPOSITION;
    private CampaignReachBreakdown breakDown;
    private bool IsViewingInfo;

    public CampaignSummaryFrame(
      OpenPositions _TEMPOPENPOSITIONS,
      OpenPositions _ORIGINALOPENPOSITION,
      float forceWidth,
      float BaseScale,
      Player player)
    {
      this.TEMPOPENPOSITIONS = _TEMPOPENPOSITIONS;
      this.ORIGINALOPENPOSITION = _ORIGINALOPENPOSITION;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      Vector2 vector2_1 = Vector2.One * 10f;
      float num1 = 0.0f;
      this.miniHeading = new MiniHeading(Vector2.Zero, "Campaign Summary", 1f, BaseScale);
      float num2 = num1 + (this.miniHeading.GetTextHeight(true) + uiScaleHelper.ScaleY(vector2_1.Y) + defaultYbuffer);
      this.EstimatedWaitTime = new EstimatedWaitTimeFrame(BaseScale);
      this.EstimatedWaitTime.location.Y = num2;
      this.EstimatedWaitTime.location.Y += this.EstimatedWaitTime.GetSize().Y * 0.5f;
      float num3 = num2 + this.EstimatedWaitTime.GetSize().Y + defaultYbuffer;
      this.TotalSpentSoFar = new ZGenericText("Xg", BaseScale);
      Vector2 size = this.TotalSpentSoFar.GetSize();
      this.TotalSpentSoFar.vLocation.Y = num3 + size.Y * 0.5f;
      float num4 = num3 + size.Y;
      this.TotalDaysPassed = new ZGenericText("X", BaseScale);
      this.TotalDaysPassed.vLocation.Y = num4 + size.Y * 0.5f;
      float num5 = num4 + size.Y;
      this.ApplicantsRecruited = new ZGenericText("X", BaseScale);
      this.ApplicantsRecruited.vLocation.Y = num5 + size.Y * 0.5f;
      float y = num5 + size.Y + defaultYbuffer;
      Vector2 vector2_2 = new Vector2(forceWidth, y);
      this.infoButton = new LittleSummaryInfoCloseToggle(BaseScale);
      this.infoButton.location.X = forceWidth - defaultXbuffer;
      this.infoButton.location.Y = defaultYbuffer;
      this.infoButton.location.X -= this.infoButton.GetSize().X * 0.5f;
      this.infoButton.location.Y += this.infoButton.GetSize().Y * 0.5f;
      this.breakDown = new CampaignReachBreakdown(BaseScale, vector2_2, this.TEMPOPENPOSITIONS, player);
      this.customerFrame = new CustomerFrame(vector2_2, BaseScale: BaseScale);
      this.miniHeading.SetTextPosition(this.customerFrame.VSCale, vector2_1.X, vector2_1.Y);
      Vector2 vector2_3 = -this.customerFrame.VSCale * 0.5f;
      this.EstimatedWaitTime.location.Y += vector2_3.Y;
      this.TotalSpentSoFar.vLocation.Y += vector2_3.Y;
      this.TotalDaysPassed.vLocation.Y += vector2_3.Y;
      this.ApplicantsRecruited.vLocation.Y += vector2_3.Y;
      this.infoButton.location += vector2_3;
      this.ReflectNewData(player);
    }

    private void SetActive(bool isPanelActive)
    {
      if (!isPanelActive)
        this.customerFrame.Active = false;
      else
        this.customerFrame.Active = true;
    }

    public void ReflectNewData(Player player)
    {
      this.SetActive(this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0);
      if (this.TEMPOPENPOSITIONS.NumberOfPositionsOpened == 0)
        this.EstimatedWaitTime.SetTimeString("-");
      else
        this.EstimatedWaitTime.SetTimeString(JobApplicants_Calculator.GetEstimatedTimeForAnApplicant(this.TEMPOPENPOSITIONS, out int _, out int _, player, out int _, out float _));
      this.breakDown.ReflectNewData(this.TEMPOPENPOSITIONS, player);
    }

    private void UpdateDataEveryFrame()
    {
      if (this.TEMPOPENPOSITIONS.NumberOfPositionsOpened == 0)
      {
        this.TotalSpentSoFar.textToWrite = "Total Spent: $" + (object) 0;
        this.TotalDaysPassed.textToWrite = "Days Searching: " + (object) 0;
        this.ApplicantsRecruited.textToWrite = "Total Applicants: " + (object) 0;
      }
      else
      {
        this.TotalSpentSoFar.textToWrite = "Total Spent: $" + (object) this.ORIGINALOPENPOSITION.TotalAmountSpent;
        int num = 0;
        if (this.ORIGINALOPENPOSITION.DayStarted != -1)
          num = (int) (Player.financialrecords.GetDaysPassed() - (long) this.ORIGINALOPENPOSITION.DayStarted);
        this.TotalDaysPassed.textToWrite = "Days Searching: " + (object) num;
        this.ApplicantsRecruited.textToWrite = "Total Applicants: " + (object) this.ORIGINALOPENPOSITION.GetNumberOfApplicants();
      }
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void UpdateCampaignSummaryFrame(Player player, float DeltaTIme, Vector2 offset)
    {
      offset += this.location;
      if (this.customerFrame.Active)
      {
        this.infoButton.UpdateLittleSummaryInfoCloseToggle(player, DeltaTIme, offset);
        if (this.infoButton.isShowingCloseButton)
          this.breakDown.UpdateCampaignReachBreakdown(player, DeltaTIme, offset);
      }
      this.UpdateDataEveryFrame();
    }

    public void DrawCampaignSummaryFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.infoButton.DrawLittleSummaryInfoCloseToggle(offset, spriteBatch);
      if (this.infoButton.isShowingCloseButton)
      {
        this.breakDown.DrawCampaignReachBreakdown(offset, spriteBatch);
      }
      else
      {
        this.miniHeading.DrawMiniHeading(offset, spriteBatch);
        this.EstimatedWaitTime.DrawTextInFrame(offset, spriteBatch);
        this.TotalSpentSoFar.DrawZGenericText(offset, spriteBatch);
        this.TotalDaysPassed.DrawZGenericText(offset, spriteBatch);
        this.ApplicantsRecruited.DrawZGenericText(offset, spriteBatch);
      }
      this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
