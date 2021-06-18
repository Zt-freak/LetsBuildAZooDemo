// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table.TableAndTimeSelector
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_Records;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table
{
  internal class TableAndTimeSelector
  {
    public Vector2 location;
    private TimeSegmentButton timeSegmentSelector;
    private float refBaseScale;
    private float totalHeight;
    private UIScaleHelper scaleHelper;
    private ParkSummaryTableType reftableType;
    private ZGenericText tableName;
    private List<RevenueDisplayTable> tableStack;
    private BackButton previousButton;
    private Z_GenericScrollMasks scrollMasks;
    private float heightForTable;
    private CheckBoxAndInfo checkbox;
    private bool FirstPageHasPreviousButton;

    public TableAndTimeSelector(
      ParkSummaryTableType tableType,
      Player player,
      float BaseScale,
      float forcedHeight,
      bool IncludeTableName = false)
    {
      this.refBaseScale = BaseScale;
      this.reftableType = tableType;
      this.scaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = this.scaleHelper.GetDefaultYBuffer();
      this.totalHeight = 0.0f;
      if (IncludeTableName)
      {
        this.totalHeight += defaultYbuffer;
        this.tableName = new ZGenericText("Xg", BaseScale, false, _UseOnePointFiveFont: true);
        this.tableName.vLocation = new Vector2(defaultXbuffer, this.totalHeight);
        this.totalHeight += this.tableName.GetSize().Y;
      }
      this.totalHeight += defaultYbuffer;
      this.timeSegmentSelector = new TimeSegmentButton(BaseScale: BaseScale);
      Vector2 size = this.timeSegmentSelector.priceadjsueter.GetSize();
      this.timeSegmentSelector.priceadjsueter.location.Y = this.totalHeight;
      this.totalHeight += size.Y;
      this.totalHeight += defaultYbuffer;
      RecordCalculator.GetRevenueSummary(this.timeSegmentSelector.SegmentType, player, out RevenueSummaryData _);
      RevenueDisplayTable revenueDisplayTable = new RevenueDisplayTable(tableType);
      this.tableStack = new List<RevenueDisplayTable>();
      this.tableStack.Add(revenueDisplayTable);
      this.checkbox = new CheckBoxAndInfo(BaseScale);
      this.checkbox.SetTick(true);
      this.SetData(this.timeSegmentSelector.SegmentType, player);
      this.checkbox.SetPara(revenueDisplayTable.GetSize().X - defaultXbuffer);
      float heightOfMask = revenueDisplayTable.GetLargestEntrySize().Y + defaultYbuffer * 0.5f;
      this.heightForTable = forcedHeight - this.totalHeight;
      this.heightForTable -= heightOfMask;
      this.heightForTable -= this.scaleHelper.ScaleY(2f);
      revenueDisplayTable.location.Y = this.totalHeight;
      this.totalHeight += this.heightForTable;
      this.checkbox.location.X = this.scaleHelper.DefaultBuffer.X;
      this.checkbox.location.Y = this.totalHeight + heightOfMask * 0.5f;
      this.scrollMasks = new Z_GenericScrollMasks(BaseScale, ColourData.Z_FrameDarkBrown, revenueDisplayTable.GetSize().X, heightOfMask, this.heightForTable);
      this.scrollMasks.location.Y = revenueDisplayTable.location.Y;
      this.totalHeight += heightOfMask;
      revenueDisplayTable.AddScroll(this.heightForTable);
      float num = (float) (-(double) this.GetWidth() * 0.5);
      if (this.tableName != null)
        this.tableName.vLocation.X += num;
      this.checkbox.location.X += num;
    }

    public float GetHeight() => this.totalHeight;

    public float GetWidth() => this.tableStack[0].GetSize().X;

    public void AddPreviousButtonForPopOut()
    {
      this.AddPreviousButton();
      this.FirstPageHasPreviousButton = true;
    }

    public bool UpdateTableAndTimeSelector(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.timeSegmentSelector.UpdateTimeSegmentButton(player, offset, DeltaTime))
        this.SetData(this.timeSegmentSelector.SegmentType, player);
      if (this.checkbox.UpdateCheckBoxAndInfo(player, DeltaTime, offset))
        this.SetData(this.timeSegmentSelector.SegmentType, player);
      if (this.previousButton != null && this.previousButton.UpdateBackButton(player, DeltaTime, offset))
      {
        if (this.tableStack.Count > 1)
        {
          this.tableStack.RemoveAt(this.tableStack.Count - 1);
          this.SetData(this.timeSegmentSelector.SegmentType, player);
        }
        else if (this.FirstPageHasPreviousButton)
          return true;
      }
      FinanceTableRowTypeContainer rowClicked = this.tableStack[this.tableStack.Count - 1].UpdateRevenueDisplayTable(player, DeltaTime, offset);
      if (rowClicked != null)
        this.CreateDetailedBreakdown(player, rowClicked);
      this.UpdateValuesEveryFrame(player);
      return false;
    }

    private void CreateDetailedBreakdown(Player player, FinanceTableRowTypeContainer rowClicked)
    {
      ParkSummaryTableType tableTypeFromThisRow = FinanceTableDataHelper.GetDetailBreakdownTableTypeFromThisRow(rowClicked);
      if (tableTypeFromThisRow == ParkSummaryTableType.Count)
        return;
      RevenueDisplayTable revenueDisplayTable = new RevenueDisplayTable(tableTypeFromThisRow);
      revenueDisplayTable.location = this.tableStack[0].location;
      this.tableStack.Add(revenueDisplayTable);
      if (this.previousButton == null)
        this.AddPreviousButton();
      this.SetData(this.timeSegmentSelector.SegmentType, player);
      revenueDisplayTable.AddScroll(this.heightForTable);
      this.checkbox.SetTick(false);
    }

    private void AddPreviousButton()
    {
      this.previousButton = new BackButton(true, BaseScale: this.refBaseScale, _IsPrevious: true);
      this.previousButton.vLocation = Vector2.Zero;
      this.previousButton.vLocation.Y = this.timeSegmentSelector.priceadjsueter.location.Y;
      this.previousButton.vLocation.Y += this.previousButton.GetSize().Y * 0.5f;
      this.previousButton.vLocation.Y -= this.scaleHelper.ScaleY(3f);
      this.previousButton.vLocation.X = this.tableStack[0].GetSize().X * 0.5f;
      this.previousButton.vLocation.X -= this.previousButton.GetSize().X * 0.5f;
    }

    public void SetData(TimeSegmentType timeSegmentType, Player player)
    {
      RevenueSummaryData PreviousSegmentData;
      RevenueSummaryData revenueSummary = RecordCalculator.GetRevenueSummary(timeSegmentType, player, out PreviousSegmentData);
      this.tableStack[this.tableStack.Count - 1].Create(timeSegmentType, revenueSummary, PreviousSegmentData, this.refBaseScale);
      this.reftableType = this.tableStack[this.tableStack.Count - 1].refTableType;
      this.SetTableName();
      this.UpdateValuesEveryFrame(player);
    }

    public void UpdateValuesEveryFrame(Player player)
    {
      RevenueSummaryData PreviousSegmentData;
      this.tableStack[this.tableStack.Count - 1].UpdateValuesEveryFrame(RecordCalculator.GetRevenueSummary(this.timeSegmentSelector.SegmentType, player, out PreviousSegmentData), PreviousSegmentData, this.checkbox.GetIsTicked());
    }

    public void SetTableName()
    {
      if (this.tableName == null)
        return;
      ParkSummaryTableType tableType = this.reftableType;
      if (this.tableStack.Count > 0)
        tableType = this.tableStack[this.tableStack.Count - 1].refTableType;
      this.tableName.textToWrite = FinanceTableDataHelper.GetTableName(tableType);
    }

    public void DrawTableAndTimeSelector(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tableStack[this.tableStack.Count - 1].DrawRevenueDisplayTable(offset, spriteBatch);
      this.scrollMasks.DrawZ_GenericScrollMasks(offset, spriteBatch);
      this.timeSegmentSelector.DrawTimeSegmentButton(offset, spriteBatch);
      if (this.tableStack.Count > 1 || this.FirstPageHasPreviousButton)
        this.previousButton.DrawBackButton(offset, spriteBatch);
      if (this.tableName != null)
        this.tableName.DrawZGenericText(offset, spriteBatch);
      this.checkbox.DrawCheckBoxAndInfo(offset, spriteBatch);
    }
  }
}
