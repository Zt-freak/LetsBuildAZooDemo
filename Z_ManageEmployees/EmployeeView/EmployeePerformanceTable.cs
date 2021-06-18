// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.EmployeePerformanceTable
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_GenericUI.Z_Scroll;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_ManageEmployees.EmployeeView
{
  internal class EmployeePerformanceTable
  {
    private List<EmployeePerformanceRow> rows;
    private EmployeePerformanceRow headerRow;
    private EmployeePerformanceRow summaryRow;
    private SimpleTextHandler noEmployeesText;
    private Z_ScrollHelper scrollHelper;
    private RowSegmentRectangle maskTop;
    private RowSegmentRectangle maskBottom;
    private float offsetOfEmployeeRowsHeight;
    public Vector2 location;
    private float totalHeight;
    internal static int HeightPerRow_Raw = 35;
    internal static float[] widths = new float[5]
    {
      150f,
      85f,
      85f,
      80f,
      60f
    };

    public EmployeePerformanceTable(List<Employee> employees, float BaseScale)
    {
      this.totalHeight = 0.0f;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float height = uiScaleHelper.ScaleY(10f);
      float HeightPerRow = uiScaleHelper.ScaleY((float) EmployeePerformanceTable.HeightPerRow_Raw);
      float[] widths = new float[EmployeePerformanceTable.widths.Length];
      EmployeePerformanceTable.widths.CopyTo((Array) widths, 0);
      for (int index = 0; index < widths.Length; ++index)
        widths[index] *= BaseScale;
      this.rows = new List<EmployeePerformanceRow>();
      this.headerRow = new EmployeePerformanceRow((Employee) null, BaseScale, HeightPerRow, widths, true);
      this.headerRow.location.Y += this.headerRow.GetSize().Y * 0.5f;
      this.totalHeight += this.headerRow.GetSize().Y + height;
      this.offsetOfEmployeeRowsHeight = this.totalHeight;
      if (employees.Count > 0)
      {
        this.maskTop = new RowSegmentRectangle(this.headerRow.GetSize().X, height, ColourData.Z_FrameDarkBrown, 1f);
        this.maskTop.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
        this.maskTop.vLocation.Y = this.totalHeight;
        float num1 = 0.0f;
        float num2 = uiScaleHelper.ScaleY(400f);
        for (int index = 0; index < employees.Count; ++index)
        {
          this.rows.Add(new EmployeePerformanceRow(employees[index], BaseScale, HeightPerRow, widths)
          {
            location = {
              Y = this.totalHeight + this.headerRow.GetSize().Y * 0.5f + num1
            }
          });
          num1 += this.headerRow.GetSize().Y;
          if (index != employees.Count - 1)
            num1 += height;
        }
        this.totalHeight += Math.Min(num2, num1);
        this.maskBottom = new RowSegmentRectangle(this.headerRow.GetSize().X, height, ColourData.Z_FrameDarkBrown, 1f);
        this.maskBottom.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
        this.maskBottom.vLocation.Y = this.totalHeight;
        this.scrollHelper = new Z_ScrollHelper(new Vector2(this.headerRow.GetSize().X, num1), num2);
        this.totalHeight += height;
      }
      else
      {
        this.totalHeight += height;
        this.noEmployeesText = new SimpleTextHandler("You do not have any employees here!~Hire some in the recruitment tab!", this.headerRow.GetSize().X, true, BaseScale, AutoComplete: true);
        this.noEmployeesText.SetAllColours(ColourData.Z_Cream);
        this.noEmployeesText.Location.Y = this.totalHeight;
        this.totalHeight += this.noEmployeesText.GetHeightOfParagraph();
        this.totalHeight += height;
      }
      this.summaryRow = new EmployeePerformanceRow((Employee) null, BaseScale, HeightPerRow, widths, isSummary: true);
      this.summaryRow.location.Y = this.totalHeight;
      this.summaryRow.location.Y += this.summaryRow.GetSize().Y * 0.5f;
      this.totalHeight += this.summaryRow.GetSize().Y;
    }

    public void SetData(PerformanceSummaryData data)
    {
      this.headerRow.SetData(data);
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].SetData(data);
    }

    public void SetSummaryData(PerformanceSummaryData yesterdayData) => this.summaryRow.SetData(yesterdayData);

    public Vector2 GetSize() => new Vector2(this.headerRow.GetSize().X, this.totalHeight);

    public bool UpdateEmployeePerformanceTable(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out Employee ViewThisEmployeeInfo)
    {
      offset += this.location;
      ViewThisEmployeeInfo = (Employee) null;
      if (this.scrollHelper != null)
      {
        this.scrollHelper.UpdateZ_ScrollHelper(player, offset + new Vector2((float) (-(double) this.GetSize().X * 0.5), this.offsetOfEmployeeRowsHeight));
        offset.Y += this.scrollHelper.YscrollOffset;
      }
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].UpdateEmployeePerformanceRow(player, DeltaTime, offset, out ViewThisEmployeeInfo))
          return true;
      }
      return false;
    }

    public void DrawEmployeePerformanceTable(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      Vector2 offset1 = offset;
      if (this.scrollHelper != null)
        offset1.Y += this.scrollHelper.YscrollOffset;
      for (int index = 0; index < this.rows.Count; ++index)
      {
        bool flag = false;
        if (this.scrollHelper != null)
        {
          float TopLocation = this.rows[index].location.Y - this.rows[index].GetSize().Y * 0.5f - this.offsetOfEmployeeRowsHeight;
          float BottomLocation = TopLocation + this.rows[index].GetSize().Y;
          flag = !this.scrollHelper.CheckIfShouldDrawThis(TopLocation, BottomLocation);
        }
        if (!flag)
          this.rows[index].DrawEmployeePerformanceRow(offset1, spriteBatch);
      }
      if (this.maskTop != null)
      {
        this.maskTop.DrawRowSegmentRectangle(offset, spriteBatch);
        this.maskBottom.DrawRowSegmentRectangle(offset, spriteBatch);
      }
      if (this.noEmployeesText != null)
        this.noEmployeesText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      this.headerRow.DrawEmployeePerformanceRow(offset, spriteBatch);
      this.summaryRow.DrawEmployeePerformanceRow(offset, spriteBatch);
    }
  }
}
