// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.TicketPriceSetRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class TicketPriceSetRow
  {
    private ZGenericText text;
    public PriceAdjuster priceadjuster;
    private ZCheckBox checkBox;
    private ZGenericText freeText;
    public Vector2 location;
    public TicketType ticketType;
    private UIScaleHelper uiscale;

    public TicketPriceSetRow(TicketType _ticketType, Player player, float BaseScale)
    {
      this.ticketType = _ticketType;
      this.uiscale = new UIScaleHelper(BaseScale);
      this.text = new ZGenericText("Price:", BaseScale, false);
      this.text.GetSize();
      this.text.vLocation.X += -0.5f * this.uiscale.ScaleX(280f);
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      int _CurrentValue = 0;
      int Max = 0;
      bool flag1 = false;
      bool flag2 = false;
      switch (this.ticketType)
      {
        case TicketType.StandardTicket:
          _CurrentValue = player.Stats.GetTicketCost();
          Max = 100;
          flag1 = player.Stats.GetTicketIsFree();
          flag2 = true;
          break;
        case TicketType.Helicopter:
          _CurrentValue = 20;
          Max = 40;
          break;
      }
      this.priceadjuster = new PriceAdjuster(0, Max, _CurrentValue, _BaseScale: BaseScale);
      this.priceadjuster.Location.X = 0.0f;
      if (!flag2)
        return;
      this.checkBox = new ZCheckBox(BaseScale);
      this.checkBox.SetTicked(flag1);
      this.checkBox.location = this.priceadjuster.Location;
      this.checkBox.location.X = 0.5f * this.uiscale.ScaleX(280f);
      this.checkBox.location.X -= 0.5f * this.checkBox.GetSize().X;
      this.freeText = new ZGenericText("Free".ToUpper(), BaseScale, false, true);
      this.freeText.GetSize();
      this.freeText.vLocation = this.checkBox.location;
      this.freeText.vLocation.X -= this.checkBox.GetSize().X * 0.5f;
      this.freeText.vLocation.X -= this.uiscale.DefaultBuffer.X;
      this.freeText.vLocation.Y -= this.freeText.GetSize().Y * 0.5f;
      this.priceadjuster.SetDisabled(flag1);
    }

    public Vector2 GetSize() => new Vector2(this.checkBox == null ? (float) ((double) this.priceadjuster.Location.X - (double) this.text.vLocation.X + (double) this.priceadjuster.GetSize().X * 0.5) + this.text.GetSize().X : this.freeText.vLocation.X - this.text.vLocation.X + this.text.GetSize().X + this.freeText.GetSize().X, this.priceadjuster.GetSize().Y);

    public bool UpdateTicketPriceSetRow(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.checkBox != null && this.checkBox.UpdateCheckBox(player, offset))
      {
        if (this.ticketType != TicketType.StandardTicket)
          throw new Exception("DID NOT CODE FOR THIS YET, I ASSUME ONLY ONE TICKET PRICE NOW");
        this.checkBox.SetTicked(!this.checkBox.GetIsTicked());
        this.priceadjuster.SetDisabled(this.checkBox.GetIsTicked());
        return true;
      }
      return this.priceadjuster.UpdatePriceAdjuster(player, offset, DeltaTime);
    }

    public int GetTicketPricesSet(out bool IsFreeChecked)
    {
      IsFreeChecked = false;
      if (this.checkBox != null)
        IsFreeChecked = this.checkBox.GetIsTicked();
      return this.priceadjuster.CurrentValue;
    }

    public void DrawTicketPriceSetRow(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spriteBatch);
      this.priceadjuster.DrawPriceAdjuster(offset);
      if (this.checkBox == null)
        return;
      this.checkBox.DrawCheckBox(spriteBatch, offset);
      this.freeText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
