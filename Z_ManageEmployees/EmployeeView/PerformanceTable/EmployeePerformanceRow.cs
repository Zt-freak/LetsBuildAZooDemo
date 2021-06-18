// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.EmployeePerformanceRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces;

namespace TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable
{
  internal class EmployeePerformanceRow
  {
    public Vector2 location;
    private PerformanceTableRowFrame frame;
    private List<PerformanceSegmentContent> contents;
    private Employee refEmployee;

    public EmployeePerformanceRow(
      Employee employee,
      float BaseScale,
      float HeightPerRow,
      float[] widths,
      bool isHeader = false,
      bool isSummary = false)
    {
      this.refEmployee = employee;
      this.frame = new PerformanceTableRowFrame(BaseScale, HeightPerRow, isHeader | isSummary, isHeader, widths);
      float num1 = 0.0f;
      this.contents = new List<PerformanceSegmentContent>();
      for (int index = 0; index < 5; ++index)
      {
        float num2 = num1 + widths[index] * 0.5f;
        PerformanceSegmentContent performanceSegmentContent = new PerformanceSegmentContent((PerformanceColumn) index, isHeader, BaseScale, new Vector2(widths[index], HeightPerRow), isSummary);
        performanceSegmentContent.location.X = num2;
        performanceSegmentContent.location.X -= this.frame.GetSize().X * 0.5f;
        this.contents.Add(performanceSegmentContent);
        num1 = num2 + widths[index] * 0.5f;
      }
    }

    public Vector2 GetSize() => this.frame.GetSize();

    public void SetData(PerformanceSummaryData data)
    {
      for (int index = 0; index < this.contents.Count; ++index)
        this.contents[index].SetUp(this.refEmployee, data);
    }

    public bool UpdateEmployeePerformanceRow(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out Employee ViewThisEmployeeInfo)
    {
      offset += this.location;
      ViewThisEmployeeInfo = (Employee) null;
      for (int index = 0; index < this.contents.Count; ++index)
      {
        if (this.contents[index].UpdatePerformanceSegmentContent(player, DeltaTime, offset))
        {
          ViewThisEmployeeInfo = this.refEmployee;
          return true;
        }
      }
      return false;
    }

    public void DrawEmployeePerformanceRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.frame.DrawPerformanceTableRowFrame(offset, spriteBatch);
      for (int index = 0; index < this.contents.Count; ++index)
        this.contents[index].DrawPerformanceSegmentContent(offset, spriteBatch);
    }
  }
}
