// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.ShopKeeper.PerformanceRecord.PerformanceRecEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Z_Employees.QuickPick;

namespace TinyZoo.Z_SummaryPopUps.People.Employee.ShopKeeper.PerformanceRecord
{
  internal class PerformanceRecEntry
  {
    private string DrawThis;
    private string DrawThisValue;
    private GameObject textobj;
    public Vector2 Location;

    public PerformanceRecEntry(float ThisValue, HistoryEntryType historyentry)
    {
      switch (historyentry)
      {
        case HistoryEntryType.ShopOpenTime:
          this.DrawThis = "Shop open time: ";
          Z_GameFlags.FormatFloatToTime(ThisValue);
          this.DrawThisValue = Z_GameFlags.FormatFloatToTime(ThisValue);
          break;
        case HistoryEntryType.CustomersServed:
          this.DrawThis = "Customers Served: ";
          this.DrawThisValue = string.Concat((object) (int) Math.Round((double) ThisValue));
          break;
        case HistoryEntryType.PercentServing:
          this.DrawThis = "Time serving customers: ";
          this.DrawThisValue = ((int) Math.Round((double) ThisValue * 100.0)).ToString() + "%";
          break;
        case HistoryEntryType.PercentOnBreak:
          this.DrawThis = "Time spend on breaks: ";
          this.DrawThisValue = ((int) Math.Round((double) ThisValue * 100.0)).ToString() + "%";
          break;
        case HistoryEntryType.TimeAtWork:
          this.DrawThis = "Time Spend at work: ";
          this.DrawThisValue = ((int) Math.Round((double) ThisValue * 100.0)).ToString() + "%";
          break;
        case HistoryEntryType.Count:
          this.DrawThis = "No Data For Selected Time Period";
          this.DrawThisValue = " ";
          break;
      }
      this.textobj = new GameObject();
      this.textobj.scale = RenderMath.GetNearestPerfectPixelZoom(1f);
    }

    public void DrawPerformanceRecEntry(Vector2 Offset)
    {
      Offset += this.Location;
      TextFunctions.DrawTextWithDropShadow(this.DrawThis, this.textobj.scale, this.textobj.vLocation + Offset, this.textobj.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false, false);
      TextFunctions.DrawTextWithDropShadow(this.DrawThisValue, this.textobj.scale, this.textobj.vLocation + Offset + new Vector2(200f, 0.0f), this.textobj.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatchTop05, false, true);
    }
  }
}
