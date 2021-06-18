// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Buy.BusBuyer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_Bus.BussInfo.Viewer;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus.BussInfo.Buy
{
  internal class BusBuyer
  {
    private BusRow busrow;
    private BusRow SellRow;
    private Exdetails exdetails;
    private CustomerFrame frame;
    public float FrameY;
    public BUSROUTE SelectedRoute;

    public BusBuyer(
      Player player,
      float BaseScale,
      Vector2 BasePanelInteriorSize,
      float _FrameY,
      BUSROUTE _SelectedRoute)
    {
      this.FrameY = _FrameY;
      this.SelectedRoute = _SelectedRoute;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float num1 = 0.0f;
      this.busrow = new BusRow(player, BUSROUTE.Count, BaseScale, IsBuyScreen: true);
      this.SellRow = new BusRow(player, BUSROUTE.Count, BaseScale, IsSell: true);
      this.frame = new CustomerFrame(new Vector2(this.busrow.GetSize().X, uiScaleHelper.ScaleY(44f)), true, BaseScale);
      this.exdetails = new Exdetails("You have no buses currently on order", BaseScale, true, 1f);
      this.busrow.Location.Y += this.busrow.GetSize().Y * 0.5f;
      float num2 = num1 + (this.busrow.GetSize().Y + uiScaleHelper.DefaultBuffer.Y);
      this.SellRow.Location.Y = num2;
      this.SellRow.Location.Y += this.SellRow.GetSize().Y * 0.5f;
      this.frame.frame.vLocation.Y = num2 + (this.SellRow.GetSize().Y + uiScaleHelper.DefaultBuffer.Y);
      this.frame.frame.vLocation.Y += this.frame.VSCale.Y * 0.5f;
      this.exdetails.vLocation.X = this.frame.VSCale.X * -0.5f;
      this.exdetails.vLocation.X += uiScaleHelper.DefaultBuffer.X;
      this.exdetails.vLocation.Y -= this.exdetails.GetSize().Y * 0.5f;
      float num3 = uiScaleHelper.ScaleY(27f) - BasePanelInteriorSize.Y * 0.5f;
      this.SellRow.Location.Y += num3;
      this.busrow.Location.Y += num3;
      this.frame.frame.vLocation.Y += num3;
    }

    public bool UpdateBusBuyer(Vector2 Offset, Player player, float DeltaTime)
    {
      int num = this.busrow.UpdateBusRow(player, DeltaTime, Offset);
      if (num > -1)
      {
        player.busroutes.AddBus((BUSTYPE) num, this.SelectedRoute, player);
        return true;
      }
      int index = this.SellRow.UpdateBusRow(player, DeltaTime, Offset);
      if (index <= -1)
        return false;
      --player.busroutes.GetBussesNotInUse()[index];
      return true;
    }

    public void DrawBusBuyer(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.busrow.DrawBusRow(Offset, spritebatch);
      this.SellRow.DrawBusRow(Offset, spritebatch);
      this.frame.DrawCustomerFrame(Offset, spritebatch);
      this.exdetails.DrawExdetails(Offset + this.frame.frame.vLocation, spritebatch);
    }
  }
}
