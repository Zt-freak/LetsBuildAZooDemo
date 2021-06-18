// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.RevenueDisplayRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.Content;
using TinyZoo.Z_Records;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue
{
  internal class RevenueDisplayRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame colouredFrame;
    private bool isHeader;
    private bool isSummary;
    private ZGenericText[] headerTexts;
    private RevenueOriginIcon revenueOriginIcon;
    private ZGenericText revenueOriginName;
    private ZGenericText thisRevenueText;
    private ZGenericText lastRevenueText;
    private ZGenericText noDataDash;
    private ZGenericText thisProfitMarginText;
    private ArrowWithPercent arrowPercent;
    private ParkSummaryTableType refTableType;
    private bool PercentageComparison_DrawDash;
    private bool HasMouseOver;

    public FinanceTableRowTypeContainer refRowType { get; private set; }

    public RevenueDisplayRow(
      ParkSummaryTableType tableType,
      FinanceTableRowTypeContainer rowType,
      List<RevenueSummaryColumn> columns,
      TimeSegmentType timeSegment,
      RevenueSummaryData summaryData,
      RevenueSummaryData lastSummaryData,
      float[] widths,
      float rowHeight,
      float BaseScale,
      bool _isHeader = false,
      bool _isSummary = false)
    {
      this.isHeader = _isHeader;
      this.isSummary = _isSummary;
      this.refRowType = rowType;
      this.refTableType = tableType;
      float Xbuffer = new UIScaleHelper(BaseScale).GetDefaultXBuffer() * 0.5f;
      float num = 0.0f;
      for (int index = 0; index < widths.Length; ++index)
        num += widths[index];
      float frameWidthOffset = num * 0.5f;
      if (_isHeader)
        this.CreateHeaderRow(tableType, columns, timeSegment, summaryData, lastSummaryData, BaseScale, widths, rowHeight, frameWidthOffset);
      else
        this.CreateRows(tableType, rowType, columns, timeSegment, summaryData, lastSummaryData, BaseScale, widths, rowHeight, frameWidthOffset, Xbuffer, _isSummary);
      this.HasMouseOver = FinanceTableDataHelper.GetDetailBreakdownTableTypeFromThisRow(rowType) != ParkSummaryTableType.Count;
    }

    private void CreateHeaderRow(
      ParkSummaryTableType tableType,
      List<RevenueSummaryColumn> columns,
      TimeSegmentType timeSegment,
      RevenueSummaryData thisSummaryData,
      RevenueSummaryData lastSummaryData,
      float BaseScale,
      float[] widths,
      float rowHeight,
      float frameWidthOffset)
    {
      float num1 = 0.0f;
      this.headerTexts = new ZGenericText[columns.Count];
      string[] headerRowStrings = FinanceTableDataHelper.GetTableHeaderRowStrings(tableType);
      for (int index = 0; index < widths.Length; ++index)
      {
        float num2 = num1 + widths[index] * 0.5f;
        this.headerTexts[index] = new ZGenericText(headerRowStrings[index], BaseScale);
        this.headerTexts[index].vLocation.X = num2 - frameWidthOffset;
        num1 = num2 + widths[index] * 0.5f;
      }
      this.colouredFrame = new PerformanceTableRowFrame(BaseScale, rowHeight, ColourData.Z_FrameBlueDarker, widths);
    }

    private void CreateRows(
      ParkSummaryTableType tableType,
      FinanceTableRowTypeContainer rowType,
      List<RevenueSummaryColumn> columns,
      TimeSegmentType timeSegment,
      RevenueSummaryData thisSummaryData,
      RevenueSummaryData lastSummaryData,
      float BaseScale,
      float[] widths,
      float rowHeight,
      float frameWidthOffset,
      float Xbuffer,
      bool isSummaryRow = false)
    {
      float num1 = 0.0f;
      float profitMargin1 = 0.0f;
      float profitMargin2 = 0.0f;
      if (rowType != null)
      {
        if (!thisSummaryData.NoData)
          FinanceTableDataHelper.GetRevenueOrProfit(tableType, rowType, thisSummaryData, isSummaryRow, out profitMargin1);
        if (!lastSummaryData.NoData)
          FinanceTableDataHelper.GetRevenueOrProfit(tableType, rowType, lastSummaryData, isSummaryRow, out profitMargin2);
      }
      for (int index = 0; index < columns.Count; ++index)
      {
        float num2 = num1 + widths[index] * 0.5f;
        switch (columns[index])
        {
          case RevenueSummaryColumn.Category:
            if (isSummaryRow)
            {
              this.revenueOriginName = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
              this.revenueOriginName.textToWrite = FinanceTableDataHelper.GetSummaryRowText(tableType);
              this.revenueOriginName.vLocation.X = num2 - frameWidthOffset;
              break;
            }
            this.revenueOriginName = new ZGenericText(BaseScale, false);
            this.revenueOriginName.textToWrite = FinanceTableDataHelper.GetCategoryString(rowType);
            this.revenueOriginName.vLocation.X = (float) (-(double) frameWidthOffset + (double) Xbuffer * 2.0);
            this.revenueOriginName.vLocation.Y -= this.revenueOriginName.GetSize().Y * 0.5f;
            break;
          case RevenueSummaryColumn.Revenue:
            this.thisRevenueText = new ZGenericText(BaseScale, _UseOnePointFiveFont: isSummaryRow);
            this.thisRevenueText.vLocation.X = num2 - frameWidthOffset;
            if (isSummaryRow)
            {
              this.thisRevenueText.SetAllColours(ColourData.TanGoldText);
              break;
            }
            break;
          case RevenueSummaryColumn.ProfitPercentageMargin:
            this.thisProfitMarginText = new ZGenericText("X", BaseScale);
            this.thisProfitMarginText.vLocation.X = num2 - frameWidthOffset;
            break;
          case RevenueSummaryColumn.LastRevenue:
            this.lastRevenueText = new ZGenericText(BaseScale);
            this.lastRevenueText.vLocation.X = num2 - frameWidthOffset;
            break;
          case RevenueSummaryColumn.PercentageComparison:
            this.noDataDash = new ZGenericText("-", BaseScale);
            this.noDataDash.vLocation.X = num2 - frameWidthOffset;
            this.arrowPercent = new ArrowWithPercent(0.0f, BaseScale, _DrawDashIf0: true);
            this.arrowPercent.location.X = num2 - frameWidthOffset;
            this.arrowPercent.location.Y -= 1f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
            break;
        }
        num1 = num2 + widths[index] * 0.5f;
      }
      if (isSummaryRow)
      {
        Vector3 zFrameMidBrown = ColourData.Z_FrameMidBrown;
        this.colouredFrame = new PerformanceTableRowFrame(BaseScale, rowHeight, zFrameMidBrown, widths);
        this.colouredFrame.ColorThisColumn(0, ColourData.Z_FrameGold);
      }
      else
        this.colouredFrame = new PerformanceTableRowFrame(BaseScale, rowHeight, false, false, widths);
    }

    public void UpdateValuesEveryFrame(
      RevenueSummaryData thisSummaryData,
      RevenueSummaryData lastSummaryData,
      bool UseEstimatedValues = false)
    {
      int num1 = UseEstimatedValues ? 1 : 0;
      int num2 = 0;
      int num3 = 0;
      float profitMargin1 = 0.0f;
      float profitMargin2 = 0.0f;
      if (!this.isHeader)
      {
        if (!thisSummaryData.NoData)
          num2 = FinanceTableDataHelper.GetRevenueOrProfit(this.refTableType, this.refRowType, thisSummaryData, this.isSummary, out profitMargin1);
        if (!lastSummaryData.NoData)
          num3 = FinanceTableDataHelper.GetRevenueOrProfit(this.refTableType, this.refRowType, lastSummaryData, this.isSummary, out profitMargin2);
      }
      this.thisRevenueText.textToWrite = !thisSummaryData.NoData ? (num2 >= 0 ? "$" + (object) num2 : "-$" + (object) Math.Abs(num2)) : "-";
      if (num2 > 0)
        this.colouredFrame.ColorThisColumnGreen(1);
      else if (num2 < 0)
        this.colouredFrame.ColorThisColumnRed(1);
      this.lastRevenueText.textToWrite = !lastSummaryData.NoData ? (num3 >= 0 ? "$" + (object) num3 : "-$" + (object) Math.Abs(num3)) : "-";
      this.PercentageComparison_DrawDash = false;
      if (lastSummaryData.NoData)
        this.PercentageComparison_DrawDash = true;
      else
        this.arrowPercent.SetNewPercent(FinanceTableDataHelper.GetPercentChange((float) num2, (float) num3));
      if (thisSummaryData.NoData || !FinanceTableDataHelper.GetColumnsForThisTable(this.refTableType).Contains(RevenueSummaryColumn.ProfitPercentageMargin))
        this.thisProfitMarginText.textToWrite = "-";
      else
        this.thisProfitMarginText.textToWrite = profitMargin1.ToString() + "%";
    }

    public Vector2 GetSize() => this.colouredFrame.GetSize();

    public bool UpdateRevenueDisplayRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      return this.HasMouseOver && this.colouredFrame.UpdateFrameForMouseOver(player, DeltaTime, offset);
    }

    public void DrawRevenueDisplayRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.colouredFrame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      if (this.isHeader)
      {
        for (int index = 0; index < this.headerTexts.Length; ++index)
          this.headerTexts[index].DrawZGenericText(offset, spriteBatch);
      }
      else
      {
        if (this.revenueOriginIcon != null)
          this.revenueOriginIcon.DrawRevenueOriginIcon(offset, spriteBatch);
        this.revenueOriginName.DrawZGenericText(offset, spriteBatch);
        this.thisRevenueText.DrawZGenericText(offset, spriteBatch);
        if (this.lastRevenueText != null)
          this.lastRevenueText.DrawZGenericText(offset, spriteBatch);
        if (this.PercentageComparison_DrawDash)
          this.noDataDash.DrawZGenericText(offset, spriteBatch);
        else
          this.arrowPercent.DrawArrowWithPercent(offset, spriteBatch);
        if (this.thisProfitMarginText != null)
          this.thisProfitMarginText.DrawZGenericText(offset, spriteBatch);
      }
      this.colouredFrame.PostDrawPerformanceTableRowFrame(offset, spriteBatch);
    }
  }
}
