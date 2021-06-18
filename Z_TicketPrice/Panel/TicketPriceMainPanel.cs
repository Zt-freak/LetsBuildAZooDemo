// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TicketPrice.Panel.TicketPriceMainPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_TicketPrice.Panel
{
  internal class TicketPriceMainPanel
  {
    private BigBrownPanel mainPanel;
    private List<TicketTypeDisplayColumn> ticketDisplayColumns;
    public Vector2 location;
    private float Xbuffer;
    private float Ybuffer;
    public bool SomethingChanged;

    public TicketPriceMainPanel(Player player, float BaseScale = 1f)
    {
      float num1 = 0.0f;
      float x = 0.0f;
      float _EdgeBuffer = 10f;
      this.Ybuffer = 10f * Sengine.ScreenRatioUpwardsMultiplier.Y * BaseScale;
      this.Xbuffer = _EdgeBuffer * BaseScale;
      List<TicketType> ticketTypeList = new List<TicketType>();
      ticketTypeList.Add(TicketType.StandardTicket);
      this.ticketDisplayColumns = new List<TicketTypeDisplayColumn>(ticketTypeList.Count);
      for (int index = 0; index < ticketTypeList.Count; ++index)
      {
        TicketTypeDisplayColumn typeDisplayColumn = new TicketTypeDisplayColumn(ticketTypeList[index], player, BaseScale);
        typeDisplayColumn.location.X = x;
        typeDisplayColumn.location.Y = num1;
        typeDisplayColumn.location += typeDisplayColumn.brownFrame.VSCale * 0.5f;
        float num2 = this.Xbuffer * (float) index;
        typeDisplayColumn.location.X += num2;
        this.ticketDisplayColumns.Add(typeDisplayColumn);
        x += typeDisplayColumn.brownFrame.VSCale.X + num2;
      }
      float y = num1 + this.ticketDisplayColumns[0].brownFrame.VSCale.Y;
      this.mainPanel = new BigBrownPanel(Vector2.Zero, true, "Ticket Prices", BaseScale);
      this.mainPanel.Finalize(new Vector2(x, y), _EdgeBuffer);
      for (int index = 0; index < this.ticketDisplayColumns.Count; ++index)
      {
        this.ticketDisplayColumns[index].location += new Vector2(this.Xbuffer, this.Ybuffer);
        this.ticketDisplayColumns[index].location -= this.mainPanel.vScale * 0.5f + this.mainPanel.InternalOffset;
      }
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.mainPanel.CheckMouseOver(player, offset);
    }

    public bool UpdateTicketPriceMainPanel(
      Player player,
      float DeltaTime,
      bool UpdateValues,
      Vector2 offset)
    {
      offset += this.location;
      this.mainPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (this.mainPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
      {
        for (int index = 0; index < this.ticketDisplayColumns.Count; ++index)
        {
          switch ((TicketType) index)
          {
            case TicketType.StandardTicket:
              bool IsFreeChecked;
              int ticketPricesSet = this.ticketDisplayColumns[index].GetTicketPricesSet(out IsFreeChecked);
              if (player.Stats.GetTicketIsFree() != IsFreeChecked)
              {
                this.SomethingChanged = true;
                player.Stats.SetTicketFree(IsFreeChecked);
              }
              if (player.Stats.GetTicketCost() != ticketPricesSet)
              {
                this.SomethingChanged = true;
                player.Stats.SetTicketCost(ticketPricesSet);
                break;
              }
              break;
          }
        }
        return true;
      }
      for (int index = 0; index < this.ticketDisplayColumns.Count; ++index)
        this.ticketDisplayColumns[index].UpdateTicketTypeDisplayColumn(player, DeltaTime, offset, UpdateValues);
      return false;
    }

    public void DrawTicketPriceMainPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.mainPanel.DrawBigBrownPanel(offset, spriteBatch);
      for (int index = 0; index < this.ticketDisplayColumns.Count; ++index)
        this.ticketDisplayColumns[index].DrawTicketTypeDisplayColumn(offset, spriteBatch);
    }
  }
}
