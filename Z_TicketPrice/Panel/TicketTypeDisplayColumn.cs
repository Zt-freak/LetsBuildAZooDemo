// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.TicketTypeDisplayColumn
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class TicketTypeDisplayColumn
  {
    public Vector2 location;
    private TicketType ticketType;
    private MiniHeading_V2 miniHeading;
    public CustomerFrame brownFrame;
    private TicketPriceSetRow priceSetRow;
    private TicketPriceHistoryFrame historyDisplay;
    private bool isLocked;
    private SimpleTextHandler lockedText;
    private UIScaleHelper uiscale;

    public TicketTypeDisplayColumn(TicketType _ticketType, Player player, float BaseScale)
    {
      this.ticketType = _ticketType;
      this.uiscale = new UIScaleHelper(BaseScale);
      float num1 = 0.0f;
      float y1 = this.uiscale.DefaultBuffer.Y;
      float x = this.uiscale.ScaleX(320f);
      this.miniHeading = new MiniHeading_V2(TicketData.GetTicketTypeToString(this.ticketType), BaseScale);
      float num2 = num1 + this.miniHeading.GetSize().Y + y1;
      this.priceSetRow = new TicketPriceSetRow(this.ticketType, player, BaseScale);
      this.priceSetRow.location.Y = num2;
      this.priceSetRow.location.Y += this.priceSetRow.GetSize().Y * 0.5f;
      this.priceSetRow.location.X = 0.0f;
      float num3 = num2 + this.priceSetRow.priceadjuster.GetSize().Y + y1;
      this.historyDisplay = new TicketPriceHistoryFrame(this.ticketType, player, BaseScale);
      this.historyDisplay.location.Y = num3 + this.historyDisplay.frame.VSCale.Y * 0.5f;
      float y2 = num3 + this.historyDisplay.frame.VSCale.Y + y1;
      this.lockedText = new SimpleTextHandler("Locked".ToUpper(), true, _Scale: (BaseScale * 3f));
      this.lockedText.AutoCompleteParagraph();
      this.lockedText.SetAllColours(ColourData.Z_Cream);
      this.brownFrame = new CustomerFrame(new Vector2(x, y2), true, BaseScale * 2f);
      this.miniHeading.SetPostionFromVSCale(this.brownFrame.VSCale);
      if (_ticketType != TicketType.SeasonPass)
        return;
      this.SetLock(true);
    }

    public Vector2 GetSize() => this.brownFrame.VSCale;

    public void UpdateTicketTypeDisplayColumn(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool UpdateValues)
    {
      offset += this.location;
      offset.Y -= this.brownFrame.VSCale.Y * 0.5f;
      this.priceSetRow.UpdateTicketPriceSetRow(player, DeltaTime, offset);
      this.historyDisplay.UpdateTicketPriceHistoryPanel(player, DeltaTime, offset, UpdateValues);
    }

    public int GetTicketPricesSet(out bool IsFreeChecked) => this.priceSetRow.GetTicketPricesSet(out IsFreeChecked);

    public void SetLock(bool _isLocked)
    {
      this.isLocked = _isLocked;
      if (this.isLocked)
      {
        this.brownFrame.frame.SetAllColours(Color.Black.ToVector3());
        this.brownFrame.frame.SetAlpha(0.6f);
      }
      else
      {
        this.brownFrame.frame.SetAllColours(Color.White.ToVector3());
        this.brownFrame.frame.SetAlpha(1f);
      }
    }

    public void DrawTicketTypeDisplayColumn(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.brownFrame.DrawCustomerFrame(offset, spriteBatch);
      if (!this.isLocked)
        this.miniHeading.DrawMiniHeading_V2(offset, spriteBatch);
      else
        this.lockedText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      offset.Y -= this.brownFrame.VSCale.Y * 0.5f;
      if (this.isLocked)
        return;
      this.priceSetRow.DrawTicketPriceSetRow(offset, spriteBatch);
      this.historyDisplay.DrawTicketPriceHistoryPanel(spriteBatch, offset);
    }
  }
}
