// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_MoralityActionUI.MoralityActionPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_MoralityActionUI
{
  internal class MoralityActionPanel
  {
    public Vector2 location;
    private float basescale;
    private bool isGood;
    private bool locked;
    private Vector2 framescale;
    private CustomerFrame frame;
    private MoralityActionContextBox contextBox;
    private MoralityActionHeading heading;
    private UIScaleHelper uiScale;
    private Vector2 currLoc;
    private string headingText;
    private string messageText;
    private Vector2 messageLoc = Vector2.Zero;
    private Vector2 messageSize;

    public MoralityActionPanel(
      bool isGood_,
      bool locked_,
      string messageText_,
      string headingText_,
      float basescale_)
    {
      this.basescale = basescale_;
      this.isGood = isGood_;
      this.locked = locked_;
      this.headingText = headingText_;
      this.messageText = messageText_;
      this.uiScale = new UIScaleHelper(this.basescale);
      this.contextBox = new MoralityActionContextBox(this.isGood, this.locked, this.basescale);
      this.heading = new MoralityActionHeading(this.isGood, this.locked, this.headingText, this.basescale);
      this.framescale = this.contextBox.GetSize() + 4f * this.uiScale.ScaleVector2(new Vector2(10f));
      this.framescale.Y += this.messageSize.Y + this.heading.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, this.isGood ? CustomerFrameColors.GoodYellowFrame : CustomerFrameColors.EvilPurpleFrame, this.basescale);
      this.messageSize = this.uiScale.ScaleVector2(AssetContainer.springFont.MeasureString(this.messageText));
      this.currLoc = Vector2.Zero;
      this.currLoc.Y = -0.5f * this.framescale.Y;
      this.currLoc.Y += this.uiScale.ScaleY(10f);
      this.heading.location.Y = this.currLoc.Y;
      this.heading.location.Y += 0.5f * this.heading.GetSize().Y;
      this.currLoc.Y += this.heading.GetSize().Y + this.uiScale.ScaleY(10f);
      this.messageLoc = this.currLoc;
      this.currLoc.Y += this.messageSize.Y;
      this.contextBox.location.Y = this.currLoc.Y + 0.5f * this.contextBox.GetSize().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateMoralityActionPanel(Player player, Vector2 offset, float DeltaTime) => (0 | (this.contextBox.UpdateMoralityActionContextBox(player, offset + this.location, DeltaTime) ? 1 : 0)) != 0;

    public void DrawMoralityActionPanel(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.contextBox.DrawMoralityActionContextBox(offset, spritebatch);
      this.heading.DrawMoralityActionHeading(offset, spritebatch);
      TextFunctions.DrawJustifiedText(this.messageText, this.basescale, offset + this.messageLoc, new Color(ColourData.Z_Cream), 1f, AssetContainer.springFont, spritebatch);
    }
  }
}
