// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Rides.RideTicketPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Tile_Data;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_TicketPrice.Panel;

namespace TinyZoo.Z_TicketPrice.Rides
{
  internal class RideTicketPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private BigBrownPanel panel;
    private Vector2 framescale;
    private TicketPriceHistoryFrame tickethistory;
    private RideOptionsPanel optionspanel;
    private TicketType tickettype;

    public RideTicketPanel(Player player, TILETYPE tiletype, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.tickettype = TicketData.GetTileTypeToTicketType(tiletype);
      this.tickethistory = new TicketPriceHistoryFrame(this.tickettype, player, this.basescale);
      this.optionspanel = new RideOptionsPanel(player, this.tickettype, this.basescale);
      this.framescale.X = Math.Max(this.optionspanel.GetSize().X, this.tickethistory.GetSize().X);
      this.framescale.Y = defaultBuffer.Y;
      this.framescale.Y += this.optionspanel.GetSize().Y;
      this.framescale.Y += this.tickethistory.GetSize().Y;
      Vector2 vector2 = new Vector2();
      vector2.Y += -0.5f * this.framescale.Y;
      this.optionspanel.location = vector2 + 0.5f * this.optionspanel.GetSize();
      this.optionspanel.location.X = 0.0f;
      vector2.Y += this.optionspanel.GetSize().Y + defaultBuffer.Y;
      this.tickethistory.location.Y = vector2.Y + 0.5f * this.tickethistory.GetSize().Y;
      this.panel = new BigBrownPanel(Vector2.Zero, true, TicketData.GetTicketTypeToString(this.tickettype), this.basescale);
      this.panel.Finalize(this.framescale);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateRideTicketPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      int num = 0 | (this.panel.UpdatePanelCloseButton(player, DeltaTime, offset) ? 1 : 0);
      this.tickethistory.UpdateTicketPriceHistoryPanel(player, DeltaTime, offset, true);
      this.optionspanel.UpdateRideOptionsPanel(player, offset, DeltaTime);
      return num != 0;
    }

    public void DrawRideTicketPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.optionspanel.DrawRideOptionsPanel(spritebatch, offset);
      this.tickethistory.DrawTicketPriceHistoryPanel(spritebatch, offset);
    }
  }
}
