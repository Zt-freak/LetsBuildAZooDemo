// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.BusListManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_Bus.BussInfo.Route_Row.Bus_List;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class BusListManager
  {
    private List<BusIconAndCount> busandcount;
    private float BaseScale;
    public Vector2 Location;
    private RowSegmentRectangle rowshighlighter;
    private TextButton addbutton;
    private BusHeading busheading;

    public BusListManager(
      float LeftX,
      Player player,
      BUSROUTE route,
      float _BaseScale,
      float RowHeight)
    {
      this.BaseScale = _BaseScale;
      if (route == BUSROUTE.Count)
        this.busheading = new BusHeading("BUSES ON THIS ROUTE", _BaseScale);
      this.addbutton = new TextButton(this.BaseScale, "Edit", 23f);
      int[] busCount = player.busroutes.GetBusCount(route);
      this.busandcount = new List<BusIconAndCount>();
      bool flag = false;
      for (int index = 0; index < 4; ++index)
      {
        if (busCount[index] > 0)
          flag = true;
        this.busandcount.Add(new BusIconAndCount((BUSTYPE) index, this.BaseScale, busCount[index]));
        this.busandcount[index].Location.X = (float) (index * 39) * this.BaseScale;
        this.busandcount[index].Location.X -= (float) (39.0 * (double) this.BaseScale * 1.5);
        this.busandcount[index].Location.X -= 25f * this.BaseScale;
      }
      if (!flag)
      {
        for (int index = 0; index < 4; ++index)
          this.busandcount[index].SetZeroRed();
        this.addbutton.SetButtonColour(BTNColour.Red);
      }
      this.addbutton.vLocation.X = (float) (39.0 * (double) this.BaseScale * 1.0);
      this.addbutton.vLocation.X += 38f * this.BaseScale;
      this.rowshighlighter = new RowSegmentRectangle(this.GetSize().X + 5f * this.BaseScale, RowHeight);
    }

    public Vector2 GetSize() => new Vector2((float) ((double) this.BaseScale * 39.0 * (double) this.busandcount.Count + 50.0 * (double) this.BaseScale), this.busandcount[0].GetSize().Y);

    public bool UpdateBusListManager(Vector2 Offset, Player player, float DeltaTime)
    {
      if (this.busheading != null)
        return false;
      Offset += this.Location;
      return this.addbutton.UpdateTextButton(player, Offset, DeltaTime);
    }

    public void DrawBusListManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.rowshighlighter.DrawRowSegmentRectangle(Offset, spritebatch);
      if (this.busheading != null)
      {
        this.busheading.DrawBusHeading(Offset, spritebatch);
      }
      else
      {
        for (int index = 0; index < this.busandcount.Count; ++index)
          this.busandcount[index].DrawBusIconAndCount(Offset, spritebatch);
        this.addbutton.DrawTextButton(Offset, 1f, spritebatch);
      }
    }
  }
}
