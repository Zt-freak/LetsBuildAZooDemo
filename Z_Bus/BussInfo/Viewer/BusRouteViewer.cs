// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Viewer.BusRouteViewer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus.BussInfo.Viewer
{
  internal class BusRouteViewer
  {
    public BUSROUTE route;
    private BusRow busrow;
    private BusRow NotInUse;
    public TextButton BusShop;
    private CustomerFrame frame;
    private Exdetails exdetails;
    public Vector2 BasePanelInteriorSize;

    public BusRouteViewer(
      BUSROUTE _route,
      Player player,
      float BaseScale,
      Vector2 _BasePanelInteriorSize)
    {
      this.BasePanelInteriorSize = _BasePanelInteriorSize;
      this.route = _route;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = 0.0f;
      this.busrow = new BusRow(player, _route, BaseScale);
      this.NotInUse = new BusRow(player, _route, BaseScale, true);
      this.BusShop = new TextButton(BaseScale, "Order More", 70f);
      this.exdetails = new Exdetails("Increase your transport capacity", BaseScale, true, 1f);
      this.frame = new CustomerFrame(new Vector2(this.NotInUse.GetSize().X, this.BusShop.GetSize_True().Y + uiScaleHelper.DefaultBuffer.Y * 2f), true, BaseScale);
      this.busrow.Location.Y += this.busrow.GetSize().Y * 0.5f;
      float num2 = num1 + (this.busrow.GetSize().Y + uiScaleHelper.DefaultBuffer.Y);
      this.NotInUse.Location.Y = num2;
      this.NotInUse.Location.Y += this.NotInUse.GetSize().Y * 0.5f;
      this.frame.frame.vLocation.Y = num2 + (this.NotInUse.GetSize().Y + uiScaleHelper.DefaultBuffer.Y);
      this.frame.frame.vLocation.Y += this.frame.VSCale.Y * 0.5f;
      this.BusShop.vLocation.X = this.busrow.GetSize().X * 0.5f;
      this.BusShop.vLocation.X -= this.BusShop.GetSize_True().X * 0.5f;
      this.BusShop.vLocation.X -= uiScaleHelper.DefaultBuffer.X;
      this.BusShop.vLocation.Y = this.frame.frame.vLocation.Y;
      this.exdetails.vLocation.X = this.frame.VSCale.X * -0.5f;
      this.exdetails.vLocation.X += uiScaleHelper.DefaultBuffer.X;
      this.exdetails.vLocation.Y -= this.exdetails.GetSize().Y * 0.5f;
      float num3 = uiScaleHelper.ScaleY(27f) - this.BasePanelInteriorSize.Y * 0.5f;
      this.NotInUse.Location.Y += num3;
      this.busrow.Location.Y += num3;
      this.BusShop.vLocation.Y += num3;
      this.frame.frame.vLocation.Y += num3;
    }

    public bool UpdateBusRouteViewer(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      out bool Remake)
    {
      Remake = false;
      int num1 = this.busrow.UpdateBusRow(player, DeltaTime, Offset);
      if (num1 > -1)
      {
        player.busroutes.RemoveABusFromThisoute(this.route, (BUSTYPE) num1, player);
        Remake = true;
        return true;
      }
      int num2 = this.NotInUse.UpdateBusRow(player, DeltaTime, Offset);
      if (num2 <= -1)
        return this.BusShop.UpdateTextButton(player, Offset, DeltaTime);
      player.busroutes.TransferInactiveBusToThisoute(this.route, (BUSTYPE) num2, player);
      Remake = true;
      return true;
    }

    public void DrawBusRouteViewer(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.busrow.DrawBusRow(Offset, spritebatch);
      this.NotInUse.DrawBusRow(Offset, spritebatch);
      this.frame.DrawCustomerFrame(Offset, spritebatch);
      this.exdetails.DrawExdetails(Offset + this.frame.frame.vLocation, spritebatch);
      this.BusShop.DrawTextButton(Offset, 1f, spritebatch);
    }
  }
}
