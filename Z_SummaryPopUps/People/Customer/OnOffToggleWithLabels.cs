// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.OnOffToggleWithLabels
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class OnOffToggleWithLabels
  {
    private Vector2 pad;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private CustomerFrame frame;
    public Vector2 location;
    private OnOffToggle toggle;
    private ZGenericText text;
    private List<string> labels;

    public bool On => this.toggle.On;

    public OnOffToggleWithLabels(float basescale_, string offlabel = "Off", string onlabel = "On", bool startOn = false)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.text = new ZGenericText("No selection", this.basescale, AssetContainer.SpringFontX1AndHalf);
      this.toggle = new OnOffToggle(this.basescale, startOn);
      this.labels = new List<string>();
      this.labels.Add(offlabel);
      this.labels.Add(onlabel);
      this.framescale = this.toggle.GetSize();
      this.framescale.Y += 0.5f * this.pad.Y;
      this.framescale.Y += this.text.GetSize().Y;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.Y = -0.5f * this.framescale.Y;
      this.text.vLocation = vector2;
      this.text.vLocation.Y += 0.5f * this.text.GetSize().Y;
      vector2.Y += this.text.GetSize().Y + 0.5f * this.pad.Y;
      this.toggle.location = vector2;
      this.toggle.location.Y += 0.5f * this.toggle.GetSize().Y;
      vector2.Y += this.toggle.GetSize().Y + 2f * this.pad.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateOnOffToggleWithLabels(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.toggle.UpdateOnOffToggle(player, offset, DeltaTime);
      this.text.textToWrite = !this.toggle.On ? this.labels[0] : this.labels[1];
      return false;
    }

    public void DrawOnOffToggleWithLabels(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spritebatch);
      this.toggle.DrawOnOffToggle(spritebatch, offset);
    }
  }
}
