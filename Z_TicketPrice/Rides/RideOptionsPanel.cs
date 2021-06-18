// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Rides.RideOptionsPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_TicketPrice.Panel;

namespace TinyZoo.Z_TicketPrice.Rides
{
  internal class RideOptionsPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private TicketPriceSetRow priceset;
    private CheckBoxWithString checkbox;
    private DragAndBar slider;

    public RideOptionsPanel(Player player, TicketType tickettype, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.priceset = new TicketPriceSetRow(tickettype, player, this.basescale);
      this.checkbox = new CheckBoxWithString("Apply to all rides of this type", false, this.basescale, true);
      this.slider = new DragAndBar(false, 0.5f, 250f, this.basescale);
      this.framescale = new Vector2();
      this.framescale.X = this.uiscale.ScaleX(300f);
      this.framescale.Y += defaultBuffer.Y;
      this.framescale.Y += this.priceset.GetSize().Y;
      this.framescale.Y += defaultBuffer.Y;
      this.framescale.Y += this.checkbox.GetSize().Y;
      this.framescale.Y += defaultBuffer.Y;
      this.framescale.Y += this.slider.GetSize().Y;
      this.framescale.Y += defaultBuffer.Y;
      Vector2 vector2 = defaultBuffer;
      vector2.Y += -0.5f * this.framescale.Y;
      this.priceset.location.Y = vector2.Y + 0.5f * this.priceset.GetSize().Y;
      vector2.Y += this.priceset.GetSize().Y + defaultBuffer.Y;
      this.checkbox.Location.Y = vector2.Y + 0.5f * this.checkbox.GetSize().Y;
      vector2.Y += this.checkbox.GetSize().Y + defaultBuffer.Y;
      this.slider.Location.Y = vector2.Y + 0.5f * this.slider.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, BaseScale: (2f * this.basescale));
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateRideOptionsPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.priceset.UpdateTicketPriceSetRow(player, DeltaTime, offset);
      this.checkbox.UpdateCheckBoxWithString(player, offset);
      this.slider.UpdateDragAndBar(player, DeltaTime, offset);
      return false;
    }

    public void DrawRideOptionsPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.priceset.DrawTicketPriceSetRow(offset, spritebatch);
      this.checkbox.DrawCheckBoxWithString(offset, spritebatch);
      this.slider.DrawDragAndBar(spritebatch, offset);
    }
  }
}
