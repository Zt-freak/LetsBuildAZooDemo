// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.PeopleCollected
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class PeopleCollected
  {
    private BusHeading busheading;
    private string TEXT;
    public Vector2 Location;
    private RowSegmentRectangle rowshighlighter;

    public PeopleCollected(
      BUSROUTE busroute,
      float LeftX,
      float BaseScale,
      Player player,
      float BarHeight,
      bool IsPercentage = false)
    {
      if (busroute == BUSROUTE.Count)
      {
        this.busheading = !IsPercentage ? new BusHeading("Passengers", BaseScale) : new BusHeading("% Collected", BaseScale);
      }
      else
      {
        int peopleCollectedToday = Player.financialrecords.GetPeopleCollectedToday(busroute);
        string WriteThis = string.Concat((object) peopleCollectedToday);
        float num = 1f;
        if (IsPercentage)
        {
          WriteThis = Player.financialrecords.GetPeopleWhoWantedToBeOnBusToday(busroute) != 0 ? string.Concat((object) Math.Round((double) peopleCollectedToday / (double) Player.financialrecords.GetPeopleWhoWantedToBeOnBusToday(busroute) * 100.0, 1)) + "%" : "0%";
          this.rowshighlighter = new RowSegmentRectangle(40f * BaseScale, BarHeight * 0.6f);
          this.rowshighlighter.SetAllColours(new Vector3(0.3f, 0.8f, 0.3f));
          this.rowshighlighter.SetAlpha(1f);
        }
        this.busheading = new BusHeading(WriteThis, BaseScale * num, true);
      }
      this.Location.X = LeftX;
      if (!IsPercentage)
      {
        this.Location.X += 30f * BaseScale;
        this.rowshighlighter = new RowSegmentRectangle(60f * BaseScale, BarHeight);
      }
      else
        this.Location.X += 35f * BaseScale;
    }

    public void DrawPAssengerList(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      if (this.rowshighlighter != null)
        this.rowshighlighter.DrawRowSegmentRectangle(Offset, spritebatch);
      this.busheading.DrawBusHeading(Offset, spritebatch);
    }
  }
}
