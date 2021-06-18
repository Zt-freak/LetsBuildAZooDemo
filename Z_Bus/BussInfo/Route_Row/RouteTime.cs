// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.RouteTime
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.BusTimetable;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class RouteTime
  {
    private BusHeading busheading;
    private string TEXT;
    public Vector2 Location;

    public RouteTime(BUSROUTE busroute, float LeftX, float BaseScale)
    {
      this.busheading = busroute != BUSROUTE.Count ? new BusHeading(Z_GameFlags.FormatFloatToTime(BusTimes.GetRouteTime(busroute) / Z_GameFlags.SecondsInDay), BaseScale, true) : new BusHeading("Route Time", BaseScale);
      this.Location.X = LeftX;
      this.Location.X += 30f * BaseScale;
    }

    public void DrawRouteTime(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.busheading.DrawBusHeading(Offset, spritebatch);
    }
  }
}
