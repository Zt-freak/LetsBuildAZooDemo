// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.BusInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_Bus.BussInfo.Buy;
using TinyZoo.Z_Bus.BussInfo.Route_Row;
using TinyZoo.Z_Bus.BussInfo.Viewer;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Bus.BussInfo
{
  internal class BusInfoPanel
  {
    private BigBrownPanel brownpanel;
    private RouteRows routerows;
    private Vector2 Location;
    private BusBuyer busbuyer;
    private BusRouteViewer busrouteviewer;
    private float BaseScale;
    private Exdetails exdetails;
    private BUSROUTE selectedburoute;
    private Vector2 BasePanelInteriorSize;

    public BusInfoPanel(Player player)
    {
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.brownpanel = new BigBrownPanel(Vector2.Zero, true, "Transport", this.BaseScale, true);
      this.routerows = new RouteRows(player, this.BaseScale);
      this.BasePanelInteriorSize = this.routerows.GetSize();
      this.brownpanel.Finalize(this.BasePanelInteriorSize);
      this.Location = new Vector2(512f, 384f);
      this.brownpanel.HasPreviousButton = false;
    }

    public bool CheckMouseOver(Player player) => this.brownpanel.CheckMouseOver(player, this.Location);

    public bool UpdateBusInfoPanel(Player player, float DeltaTime)
    {
      if (this.busbuyer != null)
      {
        if (this.busbuyer.UpdateBusBuyer(this.Location, player, DeltaTime))
          this.busbuyer = new BusBuyer(player, this.BaseScale, this.BasePanelInteriorSize, this.busbuyer.FrameY, this.busbuyer.SelectedRoute);
        if (this.brownpanel.UpdatePanelpreviousButton(player, DeltaTime, this.Location))
        {
          this.busbuyer = (BusBuyer) null;
          this.ViewRoute(player);
        }
      }
      if (this.busrouteviewer != null)
      {
        bool Remake = false;
        if (this.busrouteviewer.UpdateBusRouteViewer(this.Location, player, DeltaTime, out Remake))
        {
          if (Remake)
          {
            this.busrouteviewer = new BusRouteViewer(this.busrouteviewer.route, player, this.BaseScale, this.busrouteviewer.BasePanelInteriorSize);
          }
          else
          {
            this.busbuyer = new BusBuyer(player, this.BaseScale, this.BasePanelInteriorSize, this.busrouteviewer.BusShop.vLocation.Y, this.selectedburoute);
            this.brownpanel.SetNewHeading("Transport: Buy & Destroy");
            this.brownpanel.HasPreviousButton = true;
            this.brownpanel.PopPreviousButton();
            this.exdetails = new Exdetails("Total Buses owned: " + (object) player.busroutes.GetAllPurchasedBasses() + " Buses in service: " + (object) player.busroutes.GetAllActiveBusses() + "~Total running costs for all buses is $" + (object) (player.busroutes.GetAllActiveBusses() * BusTimes.GetMaintenenceCost()) + " per week", this.BaseScale, true);
            this.exdetails.SetForBrownPanel(this.brownpanel.vScale, this.BaseScale);
            this.busrouteviewer = (BusRouteViewer) null;
          }
        }
        else if (this.brownpanel.UpdatePanelpreviousButton(player, DeltaTime, this.Location))
        {
          this.busrouteviewer = (BusRouteViewer) null;
          this.exdetails = (Exdetails) null;
          this.routerows = new RouteRows(player, this.BaseScale);
          this.brownpanel.HasPreviousButton = false;
        }
      }
      if (this.routerows != null && this.routerows.UpdateRouteRows(this.Location, player, DeltaTime))
      {
        this.selectedburoute = this.routerows.SelectedRoute;
        this.ViewRoute(player);
      }
      this.brownpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    private void ViewRoute(Player player)
    {
      this.busrouteviewer = new BusRouteViewer(this.selectedburoute, player, this.BaseScale, this.BasePanelInteriorSize);
      this.brownpanel.SetNewHeading("Transport Timetable");
      this.exdetails = new Exdetails((BusTimes.GetBusRouteName(this.selectedburoute, false) ?? "") + "~Current running cost per week: " + (object) (player.busroutes.GetBussesByRoute(this.selectedburoute).Count * BusTimes.GetMaintenenceCost()), this.BaseScale, true);
      this.exdetails.SetForBrownPanel(this.brownpanel.vScale, this.BaseScale);
      this.routerows = (RouteRows) null;
      this.brownpanel.HasPreviousButton = true;
      this.brownpanel.PopPreviousButton();
    }

    public void DrawBusInfoPanel()
    {
      this.brownpanel.DrawBigBrownPanel(this.Location);
      if (this.exdetails != null)
        this.exdetails.DrawExdetails(this.Location + this.brownpanel.InternalOffset, AssetContainer.pointspritebatchTop05);
      if (this.busbuyer != null)
        this.busbuyer.DrawBusBuyer(this.Location, AssetContainer.pointspritebatchTop05);
      else if (this.busrouteviewer != null)
        this.busrouteviewer.DrawBusRouteViewer(this.Location, AssetContainer.pointspritebatchTop05);
      else
        this.routerows.DrawRouteRows(this.Location, AssetContainer.pointspritebatchTop05);
    }
  }
}
