// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.RouteRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_Bus.BussInfo.Route_Row;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Bus.BussInfo
{
  internal class RouteRow
  {
    private CustomerFrame customerframe;
    private RouteName routename;
    private BusListManager buslist;
    private RouteTime routetime;
    private PeopleCollected peoplecollected;
    private PeopleCollected PERCENTCOllected;
    public Vector2 Location;
    private bool Locked;
    private bool HasNoBuses;

    public RouteRow(float BaseScale, Player player, BUSROUTE route)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 _VSCale = uiScaleHelper.ScaleVector2(new Vector2(500f, 42f));
      if (route == BUSROUTE.Count)
        _VSCale = uiScaleHelper.ScaleVector2(new Vector2(500f, 20f));
      this.customerframe = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      if (route != BUSROUTE.Count && !player.busroutes.ThisRouteIsUnLocked(route))
      {
        this.customerframe = new CustomerFrame(_VSCale, true, BaseScale);
        this.Locked = true;
      }
      this.routename = new RouteName(route, BaseScale, this.Locked);
      this.routename.Location.X = _VSCale.X * -0.5f;
      float LeftX1 = this.routename.Location.X + this.routename.GetWidth();
      this.buslist = new BusListManager(LeftX1, player, route, BaseScale, _VSCale.Y);
      this.buslist.Location.X = LeftX1 + this.buslist.GetSize().X * 0.5f;
      float LeftX2 = LeftX1 + this.buslist.GetSize().X;
      this.routetime = new RouteTime(route, LeftX2, BaseScale);
      float LeftX3 = LeftX2 + 60f * BaseScale;
      this.peoplecollected = new PeopleCollected(route, LeftX3, BaseScale, player, _VSCale.Y);
      float LeftX4 = LeftX3 + 60f * BaseScale;
      this.PERCENTCOllected = new PeopleCollected(route, LeftX4, BaseScale, player, _VSCale.Y, true);
      float num = LeftX4 + 70f * BaseScale;
      if (!this.Locked)
        return;
      this.routename.Location.X -= 40f * BaseScale;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public bool UpdateRouteRow(Vector2 Offset, Player player, float DeltaTime)
    {
      Offset += this.Location;
      return !this.Locked && this.buslist.UpdateBusListManager(Offset, player, DeltaTime);
    }

    public void DrawRouteRow(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      if (this.Locked)
      {
        this.routename.DrawRouteName(spritebatch, Offset);
      }
      else
      {
        this.routename.DrawRouteName(spritebatch, Offset);
        this.buslist.DrawBusListManager(Offset, spritebatch);
        this.routetime.DrawRouteTime(Offset, spritebatch);
        this.peoplecollected.DrawPAssengerList(Offset, spritebatch);
        this.PERCENTCOllected.DrawPAssengerList(Offset, spritebatch);
      }
    }
  }
}
