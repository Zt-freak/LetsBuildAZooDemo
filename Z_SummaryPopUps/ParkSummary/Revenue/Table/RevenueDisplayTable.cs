// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table.RevenueDisplayTable
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_Records;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table
{
  internal class RevenueDisplayTable
  {
    public Vector2 location;
    private List<RevenueDisplayRow> rows;
    private RevenueDisplayRow headerRow;
    private RevenueDisplayRow summaryRow;
    private float totalHeight;
    private Z_ScrollHelper scrollHelper;

    public ParkSummaryTableType refTableType { get; private set; }

    public RevenueDisplayTable(ParkSummaryTableType tableType) => this.refTableType = tableType;

    public void Create(
      TimeSegmentType timeSegmentType,
      RevenueSummaryData summaryData,
      RevenueSummaryData lastSummaryData,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.totalHeight = 0.0f;
      List<RevenueSummaryColumn> columnsForThisTable = FinanceTableDataHelper.GetColumnsForThisTable(ParkSummaryTableType.ProfitBreakdown);
      float[] widths = new float[columnsForThisTable.Count];
      for (int index = 0; index < columnsForThisTable.Count; ++index)
      {
        float num = 0.0f;
        switch (columnsForThisTable[index])
        {
          case RevenueSummaryColumn.Category:
            num = 135f;
            break;
          case RevenueSummaryColumn.Revenue:
            num = 60f;
            break;
          case RevenueSummaryColumn.ProfitPercentageMargin:
            num = 60f;
            break;
          case RevenueSummaryColumn.LastRevenue:
            num = 60f;
            break;
          case RevenueSummaryColumn.PercentageComparison:
            num = 50f;
            break;
        }
        widths[index] = uiScaleHelper.ScaleX(num);
      }
      float rowHeight1 = uiScaleHelper.ScaleY(17f);
      float rowHeight2 = uiScaleHelper.ScaleY(23f);
      float rowHeight3 = uiScaleHelper.ScaleY(30f);
      this.headerRow = new RevenueDisplayRow(this.refTableType, (FinanceTableRowTypeContainer) null, columnsForThisTable, timeSegmentType, summaryData, lastSummaryData, widths, rowHeight1, BaseScale, true);
      this.headerRow.location.Y = this.totalHeight;
      this.headerRow.location.Y += rowHeight1 * 0.5f;
      this.totalHeight += rowHeight1;
      this.rows = new List<RevenueDisplayRow>();
      List<FinanceTableRowTypeContainer> reOrderedRows = FinanceTableDataHelper.GetReOrderedRows(this.refTableType, summaryData);
      for (int index = 0; index < reOrderedRows.Count; ++index)
      {
        this.totalHeight += defaultYbuffer;
        RevenueDisplayRow revenueDisplayRow = new RevenueDisplayRow(this.refTableType, reOrderedRows[index], columnsForThisTable, timeSegmentType, summaryData, lastSummaryData, widths, rowHeight2, BaseScale);
        revenueDisplayRow.location.Y = this.totalHeight;
        revenueDisplayRow.location.Y += rowHeight2 * 0.5f;
        this.totalHeight += rowHeight2;
        this.rows.Add(revenueDisplayRow);
      }
      this.totalHeight += defaultYbuffer;
      this.summaryRow = new RevenueDisplayRow(this.refTableType, (FinanceTableRowTypeContainer) null, columnsForThisTable, timeSegmentType, summaryData, lastSummaryData, widths, rowHeight3, BaseScale, _isSummary: true);
      this.summaryRow.location.Y = this.totalHeight + rowHeight3 * 0.5f;
      this.totalHeight += rowHeight3;
    }

    public void UpdateValuesEveryFrame(
      RevenueSummaryData newData,
      RevenueSummaryData oldData,
      bool UseEstimatedValues = false)
    {
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].UpdateValuesEveryFrame(newData, oldData, UseEstimatedValues);
      this.summaryRow.UpdateValuesEveryFrame(newData, oldData, UseEstimatedValues);
    }

    public Vector2 GetSize()
    {
      float x = 0.0f;
      if (this.rows.Count > 0)
        x = this.rows[0].GetSize().X;
      return new Vector2(x, this.totalHeight);
    }

    public Vector2 GetLargestEntrySize()
    {
      Vector2 size = this.headerRow.GetSize();
      size.Y = Math.Max(size.Y, this.summaryRow.GetSize().Y);
      if (this.rows.Count > 0)
        size.Y = Math.Max(size.Y, this.rows[0].GetSize().Y);
      return size;
    }

    public void AddScroll(float maxHeight) => this.scrollHelper = new Z_ScrollHelper(this.GetSize(), maxHeight);

    public FinanceTableRowTypeContainer UpdateRevenueDisplayTable(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      if (this.scrollHelper != null)
      {
        this.scrollHelper.UpdateZ_ScrollHelper(player, offset + new Vector2((float) (-(double) this.GetSize().X * 0.5), 0.0f));
        offset.Y += this.scrollHelper.YscrollOffset;
      }
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].UpdateRevenueDisplayRow(player, DeltaTime, offset))
          return this.rows[index].refRowType;
      }
      return (FinanceTableRowTypeContainer) null;
    }

    public void DrawRevenueDisplayTable(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.scrollHelper != null)
        offset.Y += this.scrollHelper.YscrollOffset;
      bool flag1 = true;
      bool flag2 = true;
      if (this.scrollHelper != null)
      {
        Vector2 size1 = this.headerRow.GetSize();
        float TopLocation1 = this.headerRow.location.Y - size1.Y * 0.5f;
        float BottomLocation1 = TopLocation1 + size1.Y;
        flag1 = this.scrollHelper.CheckIfShouldDrawThis(TopLocation1, BottomLocation1);
        Vector2 size2 = this.summaryRow.GetSize();
        float TopLocation2 = this.summaryRow.location.Y - size2.Y * 0.5f;
        float BottomLocation2 = TopLocation2 + size2.Y;
        flag2 = this.scrollHelper.CheckIfShouldDrawThis(TopLocation2, BottomLocation2);
      }
      if (flag1)
        this.headerRow.DrawRevenueDisplayRow(offset, spriteBatch);
      for (int index = 0; index < this.rows.Count; ++index)
      {
        bool flag3 = true;
        if (this.scrollHelper != null)
        {
          Vector2 size = this.rows[index].GetSize();
          float TopLocation = this.rows[index].location.Y - size.Y * 0.5f;
          float BottomLocation = TopLocation + size.Y;
          flag3 = this.scrollHelper.CheckIfShouldDrawThis(TopLocation, BottomLocation);
        }
        if (flag3)
          this.rows[index].DrawRevenueDisplayRow(offset, spriteBatch);
      }
      if (!flag2)
        return;
      this.summaryRow.DrawRevenueDisplayRow(offset, spriteBatch);
    }
  }
}
