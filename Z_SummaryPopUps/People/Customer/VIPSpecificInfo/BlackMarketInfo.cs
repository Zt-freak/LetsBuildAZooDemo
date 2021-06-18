// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo.BlackMarketInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.BlackMarket;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo
{
  internal class BlackMarketInfo
  {
    public Vector2 location;
    private CustomerFrame frame;
    private MiniHeading heading;
    private LabelledBar relationshipBar;
    private List<ZGenericText> textInfoRows;
    private BlackMarketDealerHistory dealerHistory;

    public BlackMarketInfo(
      WalkingPerson person,
      Player player,
      float BaseScale,
      float forceThisWidth = -1f)
    {
      this.dealerHistory = player.blackmarketstats.GetDealerHistoryForThisBlackMarketDealer(person.thispersontype);
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      Vector2 zero = Vector2.Zero;
      this.heading = new MiniHeading(Vector2.Zero, "Info", 1f, BaseScale);
      Vector2 _VSCale = zero + defaultBuffer;
      _VSCale.Y += this.heading.GetTextHeight(true);
      _VSCale.Y += defaultBuffer.Y * 0.5f;
      this.relationshipBar = new LabelledBar(0.0f, ColourData.BlackMarketPaleBlue, "Relationship", BaseScale, false);
      this.relationshipBar.location = _VSCale;
      _VSCale.Y += this.relationshipBar.GetSize().Y;
      _VSCale.Y += defaultBuffer.Y * 0.5f;
      float x = this.relationshipBar.GetSize().X;
      this.textInfoRows = new List<ZGenericText>();
      for (int index = 0; index < 3; ++index)
      {
        ZGenericText zgenericText = new ZGenericText("X", BaseScale, false);
        zgenericText.vLocation = _VSCale;
        Vector2 size = zgenericText.GetSize();
        _VSCale.Y += size.Y;
        this.textInfoRows.Add(zgenericText);
      }
      this.RefreshValues();
      for (int index = 0; index < this.textInfoRows.Count; ++index)
      {
        Vector2 size = this.textInfoRows[index].GetSize();
        if ((double) size.X > (double) x)
          x = size.X;
      }
      _VSCale.X += x;
      _VSCale.X += defaultBuffer.X;
      _VSCale.Y += defaultBuffer.Y;
      if ((double) forceThisWidth != -1.0)
        _VSCale.X = forceThisWidth;
      this.frame = new CustomerFrame(_VSCale, CustomerFrameColors.Brown, BaseScale);
      this.heading.SetTextPosition(this.frame.VSCale);
      Vector2 vector2 = -this.frame.VSCale * 0.5f;
      this.relationshipBar.location += vector2;
      for (int index = 0; index < this.textInfoRows.Count; ++index)
      {
        ZGenericText textInfoRow = this.textInfoRows[index];
        textInfoRow.vLocation = textInfoRow.vLocation + vector2;
      }
    }

    public Vector2 GetSize() => this.frame.VSCale;

    public void RefreshValues()
    {
      this.relationshipBar.SetNewValues(this.dealerHistory.relationship_float);
      for (int index = 0; index < this.textInfoRows.Count; ++index)
      {
        switch (index)
        {
          case 0:
            this.textInfoRows[index].textToWrite = "Animals Bought From This Dealer: " + (object) this.dealerHistory.animalsBoughtFromThisDealer;
            break;
          case 1:
            this.textInfoRows[index].textToWrite = "Animals Sold To This Dealer: " + (object) this.dealerHistory.animalsSoldToThisDealer;
            break;
          case 2:
            this.textInfoRows[index].textToWrite = "Time Served in Prison: " + (object) this.dealerHistory.timeServedInPrison;
            break;
        }
      }
    }

    public void UpdateBlackMarketInfo()
    {
    }

    public void DrawBlackMarketInfo(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.relationshipBar.DrawLabelledBar(offset, spritebatch);
      this.heading.DrawMiniHeading(offset, spritebatch);
      for (int index = 0; index < this.textInfoRows.Count; ++index)
        this.textInfoRows[index].DrawZGenericText(offset, spritebatch);
    }
  }
}
