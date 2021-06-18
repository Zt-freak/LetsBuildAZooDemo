// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.LoansPaper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_Quests.QuestComplete;
using TinyZoo.Z_SummaryPopUps.EventReport;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements
{
  internal class LoansPaper
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText bigHeader;
    private NewsHeaderBar headerBar;
    private ZGenericText maxLoanText;
    private ZGenericText maxLoanNumber;
    private RowSegmentRectangle lineTop;
    private ZGenericText reasonForLoanHeader;
    private ZGenericText reasonForLoanText;
    private RowSegmentRectangle lineBottom;
    private ReportSignature reportSignature;
    private StampPrint stampPrint;

    public LoansPaper(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      float width = uiScaleHelper.ScaleX(450f);
      this.bigHeader = new ZGenericText("Loan Application".ToUpper(), BaseScale, _UseRoundaboutHugeFont: true);
      this.bigHeader.SetAllColours(ColourData.Z_DarkTextGray);
      this.headerBar = new NewsHeaderBar(BaseScale, "Horizon Bank", Z_GameFlags.GetGameDateToday_AsString(), width);
      this.maxLoanText = new ZGenericText("Maximum Loan Available", BaseScale);
      this.maxLoanText.SetAllColours(ColourData.Z_DarkTextGray);
      this.maxLoanNumber = new ZGenericText("$1,000,000", BaseScale, _UseRoundaboutHugeFont: true);
      this.maxLoanNumber.SetAllColours(ColourData.Z_DarkTextGray);
      this.lineTop = new RowSegmentRectangle(width, uiScaleHelper.ScaleY(1f), ColourData.Z_DarkTextGray, 1f);
      this.lineTop.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.reasonForLoanHeader = new ZGenericText("Reason for Loan", BaseScale);
      this.reasonForLoanHeader.SetAllColours(ColourData.Z_DarkTextGray);
      this.reasonForLoanText = new ZGenericText("Pay Staff Salaries, Fines, Etc", BaseScale, _UseOnePointFiveFont: true);
      this.reasonForLoanText.SetAllColours(ColourData.Z_DarkTextGray);
      this.lineBottom = new RowSegmentRectangle(width, uiScaleHelper.ScaleY(1f), ColourData.Z_DarkTextGray, 1f);
      this.lineBottom.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.reportSignature = new ReportSignature(BaseScale, SignatureType.Banker);
      this.stampPrint = new StampPrint(BaseScale, StampPrintType.Approved);
      this.stampPrint.SetRotation(-0.1f);
      Vector2 zero = Vector2.Zero;
      Vector2 vector2_1 = defaultBuffer + defaultBuffer;
      this.bigHeader.vLocation.Y = vector2_1.Y;
      this.bigHeader.vLocation.Y += this.bigHeader.GetSize().Y * 0.5f;
      vector2_1.Y += this.bigHeader.GetSize().Y;
      this.headerBar.location.Y = vector2_1.Y;
      this.headerBar.location.X -= this.headerBar.GetSize().X * 0.5f;
      vector2_1.Y += this.headerBar.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.maxLoanText.vLocation.Y = vector2_1.Y;
      this.maxLoanText.vLocation.Y += this.maxLoanText.GetSize().Y * 0.5f;
      vector2_1.Y += this.maxLoanText.GetSize().Y;
      this.maxLoanNumber.vLocation.Y += vector2_1.Y;
      this.maxLoanNumber.vLocation.Y += this.maxLoanNumber.GetSize().Y * 0.5f;
      vector2_1.Y += this.maxLoanNumber.GetSize().Y;
      this.lineTop.vLocation.Y += vector2_1.Y;
      vector2_1.Y += this.lineTop.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y * 0.5f;
      this.reasonForLoanHeader.vLocation.Y = vector2_1.Y;
      this.reasonForLoanHeader.vLocation.Y += this.reasonForLoanHeader.GetSize().Y * 0.5f;
      vector2_1.Y += this.reasonForLoanHeader.GetSize().Y;
      this.reasonForLoanText.vLocation.Y = vector2_1.Y;
      this.reasonForLoanText.vLocation.Y += this.reasonForLoanText.GetSize().Y * 0.5f;
      vector2_1.Y += this.reasonForLoanText.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y * 0.5f;
      this.lineBottom.vLocation.Y = vector2_1.Y;
      vector2_1.Y += this.lineBottom.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y;
      this.stampPrint.location = vector2_1;
      this.stampPrint.location.Y += this.stampPrint.GetSize().Y * 0.5f;
      this.stampPrint.location.X += this.stampPrint.GetSize().X * 0.5f;
      this.reportSignature.location.Y = vector2_1.Y;
      this.reportSignature.location.Y += this.reportSignature.GetSize().Y * 0.5f;
      this.reportSignature.location.X = width;
      this.reportSignature.location.X -= this.reportSignature.GetSize().X * 0.5f;
      vector2_1.Y += this.reportSignature.GetSize().Y;
      this.stampPrint.location.Y = this.reportSignature.location.Y;
      vector2_1.Y += defaultBuffer.Y;
      vector2_1.X += width;
      this.customerFrame = new CustomerFrame(vector2_1 + defaultBuffer * 2f, CustomerFrameColors.Paper, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      this.bigHeader.vLocation.Y += vector2_2.Y;
      this.headerBar.location.Y += vector2_2.Y;
      this.maxLoanText.vLocation.Y += vector2_2.Y;
      this.maxLoanNumber.vLocation.Y += vector2_2.Y;
      this.lineTop.vLocation.Y += vector2_2.Y;
      this.reasonForLoanHeader.vLocation.Y += vector2_2.Y;
      this.reasonForLoanText.vLocation.Y += vector2_2.Y;
      this.lineBottom.vLocation.Y += vector2_2.Y;
      this.reportSignature.location += vector2_2;
      this.stampPrint.location += vector2_2;
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void DrawLoansPaper(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.bigHeader.DrawZGenericText(offset, spriteBatch);
      this.headerBar.DrawNewsHeaderBar(offset, spriteBatch);
      this.maxLoanText.DrawZGenericText(offset, spriteBatch);
      this.maxLoanNumber.DrawZGenericText(offset, spriteBatch);
      this.lineTop.DrawRowSegmentRectangle(offset, spriteBatch);
      this.reasonForLoanHeader.DrawZGenericText(offset, spriteBatch);
      this.reasonForLoanText.DrawZGenericText(offset, spriteBatch);
      this.lineBottom.DrawRowSegmentRectangle(offset, spriteBatch);
      this.reportSignature.DrawReportSignature(spriteBatch, offset);
      this.stampPrint.DrawStampPrint(offset, spriteBatch);
    }
  }
}
