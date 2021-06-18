// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.SponsorActionPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps
{
  internal class SponsorActionPopUp : CustomerActionPopUp
  {
    private List<int> costs;
    private SelectorBarWithLabels selector;
    private TextButton button;
    private ZGenericText coststr;
    private bool disablebutton;

    public SponsorActionPopUp(float basescale_)
      : base(basescale_)
    {
      this.costs = new List<int>();
      this.costs.Add(40);
      this.costs.Add(80);
      this.costs.Add(160);
      this.costs.Add(320);
      this.selector = new SelectorBarWithLabels(this.basescale);
      this.selector.Add("Wearing the zoo t-shirt");
      this.selector.Add("Shout-out in a video");
      this.selector.Add("Full sponsored video");
      this.selector.Add("Zoo ambassador");
      this.button = new TextButton(this.basescale, "Confirm", 50f);
      this.coststr = new ZGenericText("$888", this.basescale, _UseOnePointFiveFont: true);
      this.framescale = 2f * this.pad;
      this.framescale = this.framescale + this.selector.GetSize();
      this.framescale.Y += this.pad.Y + this.button.GetSize_True().Y;
      this.framescale.Y += this.coststr.GetSize().Y;
      this.framescale.X = Math.Max(this.framescale.X, 2f * this.pad.X + this.uiscale.ScaleX(200f));
      this.SizeFrame();
      Vector2 vector2 = -0.5f * this.framescale + this.pad;
      this.coststr.vLocation = vector2 + 0.5f * this.coststr.GetSize();
      this.coststr.vLocation.X = 0.0f;
      vector2.Y += this.coststr.GetSize().Y;
      this.selector.location = vector2 + 0.5f * this.selector.GetSize();
      this.selector.location.X = 0.0f;
      vector2.Y += this.selector.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize_True();
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y + this.pad.Y;
    }

    public override bool UpdateCustomerActionPopUp(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.selector.UpdateSelectorBarWithLabels(player, offset, DeltaTime);
      int selected = this.selector.Selected;
      if (selected == -1)
      {
        this.coststr.textToWrite = "$0";
        this.button.SetButtonColour(BTNColour.Grey);
        this.disablebutton = true;
      }
      else
      {
        this.coststr.textToWrite = "$" + (object) this.costs[selected];
        this.disablebutton = false;
        this.button.SetButtonColour(BTNColour.Green);
      }
      if (!this.disablebutton)
        flag |= this.button.UpdateTextButton(player, offset, DeltaTime);
      return flag;
    }

    public override void DrawCustomerActionPopUp(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.selector.DrawSelectorBarWithLabels(spritebatch, offset);
      this.button.DrawTextButton(offset, 1f, spritebatch);
      this.coststr.DrawZGenericText(offset, spritebatch);
    }
  }
}
