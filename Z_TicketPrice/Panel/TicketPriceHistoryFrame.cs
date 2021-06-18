// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.TicketPriceHistoryFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Records;
using TinyZoo.Z_SummaryPopUps.Generic;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class TicketPriceHistoryFrame
  {
    public Vector2 location;
    public CustomerFrame frame;
    private MiniHeading_V2 miniHeading;
    private TimeSegmentButton timeSegmentSelector;
    private PieChart pieChart;
    private TicketType ticketType;
    private bool noData;
    private SimpleTextHandler noDataText;
    private UIScaleHelper uiscale;
    private ZGenericText ticketsSoldText;
    private string summaryText;
    private string summaryTextValue;

    public TicketPriceHistoryFrame(TicketType _ticketType, Player player, float BaseScale)
    {
      this.uiscale = new UIScaleHelper(BaseScale);
      this.ticketType = _ticketType;
      float num1 = 0.0f;
      float y1 = this.uiscale.DefaultBuffer.Y;
      this.miniHeading = new MiniHeading_V2("Sales History", BaseScale);
      float num2 = num1 + this.miniHeading.GetSize().Y + y1;
      this.timeSegmentSelector = new TimeSegmentButton(BaseScale: BaseScale);
      this.timeSegmentSelector.priceadjsueter.location.Y = num2;
      float num3 = num2 + this.timeSegmentSelector.priceadjsueter.GetSize().Y + y1;
      this.summaryText = "Tickets Sold: ";
      this.summaryTextValue = "";
      this.ticketsSoldText = new ZGenericText(this.summaryText + this.summaryTextValue, BaseScale);
      this.pieChart = new PieChart(new string[2]
      {
        "People Who Purchased Ticket",
        "People Who Did Not Purchase Ticket"
      }, new Vector3[2]
      {
        ColourData.ACPaleBlue,
        ColourData.FernRed
      }, BaseScale);
      this.pieChart.location.Y = num3;
      this.pieChart.location.Y += 0.5f * this.pieChart.GetSize().Y;
      this.SetData(this.timeSegmentSelector.SegmentType, player);
      float num4 = num3 + this.pieChart.GetSize().Y + y1;
      float y2 = this.ticketsSoldText.GetSize().Y;
      this.ticketsSoldText.vLocation.Y = num4 + y2 * 0.5f;
      float y3 = num4 + y2 + y1;
      this.frame = new CustomerFrame(new Vector2(this.uiscale.ScaleX(300f), y3), BaseScale: (2f * BaseScale));
      this.miniHeading.SetPostionFromVSCale(this.frame.VSCale);
      this.noDataText = new SimpleTextHandler("There is no data for this time period yet.", true, this.frame.VSCale.X * 0.5f / Sengine.ReferenceScreenRes.Y, BaseScale);
      this.noDataText.AutoCompleteParagraph();
      this.noDataText.SetAllColours(ColourData.Z_Cream);
      this.noDataText.Location.Y = (float) ((double) y3 * 0.5 + (double) this.timeSegmentSelector.priceadjsueter.GetSize().Y * 0.5);
    }

    public Vector2 GetSize() => this.frame.VSCale;

    public void UpdateTicketPriceHistoryPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      bool UpdateValues)
    {
      offset += this.location;
      offset.Y -= this.frame.VSCale.Y * 0.5f;
      if (this.timeSegmentSelector.UpdateTimeSegmentButton(player, offset, DeltaTime))
      {
        this.SetData(this.timeSegmentSelector.SegmentType, player);
      }
      else
      {
        if (!UpdateValues)
          return;
        this.SetData(this.timeSegmentSelector.SegmentType, player);
      }
    }

    public void SetData(TimeSegmentType timeSegment, Player player)
    {
      float num = 0.0f;
      int TotalPeople = 0;
      int TotalPayingCustomers = 0;
      switch (this.ticketType)
      {
        case TicketType.StandardTicket:
          num = RecordCalculator.GetPercentageOfPeopleWhoPurchasedNormalTicket(timeSegment, out this.noData, out TotalPeople, out TotalPayingCustomers);
          break;
        case TicketType.SeasonPass:
          this.noData = true;
          break;
      }
      if (this.noData)
        return;
      this.pieChart.SetValues(new float[2]{ num, 1f - num });
      this.summaryTextValue = TotalPayingCustomers.ToString() + "/" + (object) TotalPeople;
      this.ticketsSoldText.textToWrite = this.summaryText + this.summaryTextValue;
    }

    public void DrawTicketPriceHistoryPanel(SpriteBatch spriteBatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading_V2(offset, spriteBatch);
      offset.Y -= this.frame.VSCale.Y * 0.5f;
      this.timeSegmentSelector.DrawTimeSegmentButton(offset, spriteBatch);
      if (!this.noData)
      {
        this.pieChart.DrawPieChart(spriteBatch, offset);
        this.ticketsSoldText.DrawZGenericText(offset, spriteBatch);
      }
      else
        this.noDataText.DrawSimpleTextHandler(offset, 1f, spriteBatch);
    }
  }
}
