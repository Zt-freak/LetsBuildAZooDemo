// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Employee.ShopKeeper.PerformanceRecord.ShopKeeperRecordDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.People.Employee.ShopKeeper.PerformanceRecord
{
  internal class ShopKeeperRecordDisplay
  {
    private TimeSegmentButton timesegmentbutton;
    public Vector2 Location;
    public CustomerFrame brownFrame;
    private List<PerformanceRecEntry> performanceentries;
    private QuickEmployeeDescription REF_quickemployeedesc;
    private GameObject Heading;
    private bool NoData;

    public ShopKeeperRecordDisplay(
      QuickEmployeeDescription quickemployeedesc,
      Player player,
      Vector2 OtherFrameScale)
    {
      this.brownFrame = new CustomerFrame(OtherFrameScale);
      this.REF_quickemployeedesc = quickemployeedesc;
      this.timesegmentbutton = new TimeSegmentButton();
      this.timesegmentbutton.priceadjsueter.location.Y = OtherFrameScale.Y * -0.5f;
      this.timesegmentbutton.priceadjsueter.location.Y += 50f;
      this.Heading = new GameObject();
      this.Heading.vLocation = OtherFrameScale * -0.5f;
      this.Heading.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.Heading.vLocation.Y += 10f;
      this.Heading.vLocation.X += 10f;
      this.Heading.SetAllColours(ColourData.Z_Cream);
      int num = 0;
      while (num < quickemployeedesc.employeehistory.Count)
        ++num;
      this.SetEntries(player);
    }

    private void SetEntries(Player player)
    {
      this.performanceentries = new List<PerformanceRecEntry>();
      float[] states = EmployeeHistory.GetStates(this.REF_quickemployeedesc, this.timesegmentbutton.SegmentType, (int) Player.financialrecords.GetDaysPassed(), out this.NoData);
      if (this.NoData)
      {
        this.performanceentries.Add(new PerformanceRecEntry(0.0f, HistoryEntryType.Count));
        this.performanceentries[0].Location.Y = 30f;
        this.performanceentries[0].Location.X = -100f;
      }
      else
      {
        for (int index = 0; index < states.Length; ++index)
        {
          this.performanceentries.Add(new PerformanceRecEntry(states[index], (HistoryEntryType) index));
          this.performanceentries[index].Location.Y = (float) (30 + index * 30);
          this.performanceentries[index].Location.X = -100f;
          this.performanceentries[index].Location.Y -= 50f;
        }
      }
    }

    public void UpdateShopKeeperRecordDisplay(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.timesegmentbutton.UpdateTimeSegmentButton(player, Offset, DeltaTime))
        return;
      this.SetEntries(player);
    }

    public void DrawShopKeeperRecordDisplay(Vector2 Offset)
    {
      Offset += this.Location;
      this.brownFrame.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      TextFunctions.DrawTextWithDropShadow("Performance:", this.Heading.scale, Offset + this.Heading.vLocation, this.Heading.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05, false);
      this.timesegmentbutton.DrawTimeSegmentButton(Offset, AssetContainer.pointspritebatchTop05);
      for (int index = 0; index < this.performanceentries.Count; ++index)
        this.performanceentries[index].DrawPerformanceRecEntry(Offset);
    }
  }
}
