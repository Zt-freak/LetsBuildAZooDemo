// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.RouteRows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class RouteRows
  {
    private List<RouteRow> routerows;
    private float FullHeight;
    public BUSROUTE SelectedRoute;

    public RouteRows(Player player, float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.routerows = new List<RouteRow>();
      this.routerows.Add(new RouteRow(BaseScale, player, BUSROUTE.Count));
      float y = this.routerows[0].GetSize().Y;
      this.routerows[0].Location.Y = y * 0.5f;
      float num = y + uiScaleHelper.ScaleY(5f);
      for (int index = 0; index < 10; ++index)
      {
        this.routerows.Add(new RouteRow(BaseScale, player, (BUSROUTE) index));
        this.routerows[index + 1].Location.Y = num + (this.routerows[index + 1].GetSize().Y + uiScaleHelper.ScaleY(5f)) * (float) index;
        this.routerows[index + 1].Location.Y += this.routerows[index + 1].GetSize().Y * 0.5f;
        this.FullHeight = this.routerows[index + 1].Location.Y + this.routerows[index + 1].GetSize().Y * 0.5f;
      }
      for (int index = 0; index < this.routerows.Count; ++index)
        this.routerows[index].Location.Y -= this.FullHeight * 0.5f;
    }

    public Vector2 GetSize() => new Vector2(this.routerows[0].GetSize().X, this.FullHeight);

    public bool UpdateRouteRows(Vector2 Offset, Player player, float DeltaTime)
    {
      this.SelectedRoute = BUSROUTE.Count;
      for (int index = 0; index < this.routerows.Count; ++index)
      {
        if (this.routerows[index].UpdateRouteRow(Offset, player, DeltaTime))
          this.SelectedRoute = (BUSROUTE) (index - 1);
      }
      return this.SelectedRoute != BUSROUTE.Count;
    }

    public void DrawRouteRows(Vector2 Offset, SpriteBatch spritebatch)
    {
      for (int index = 0; index < this.routerows.Count; ++index)
        this.routerows[index].DrawRouteRow(Offset, spritebatch);
    }
  }
}
