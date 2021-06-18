// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SelectorBarWithLabels
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class SelectorBarWithLabels
  {
    private Vector2 pad;
    private float basescale;
    private UIScaleHelper uiscale;
    private Vector2 framescale;
    private CustomerFrame frame;
    public Vector2 location;
    private SelectorBar selector;
    private ZGenericText text;
    private List<string> labels;
    private bool calculate;

    public int Selected
    {
      get => this.selector.Selected;
      set => this.selector.Selected = value;
    }

    public SelectorBarWithLabels(float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.text = new ZGenericText("No selection", this.basescale, AssetContainer.SpringFontX1AndHalf);
      this.selector = new SelectorBar(this.basescale);
      this.labels = new List<string>();
    }

    public Vector2 GetSize()
    {
      if (this.calculate)
      {
        this.CalculateScaleAndPosition();
        this.calculate = false;
      }
      return this.framescale;
    }

    public void Add(string label, BTNColour colour = BTNColour.White)
    {
      this.selector.Add(colour);
      this.labels.Add(label);
      this.calculate = true;
    }

    public void CalculateScaleAndPosition()
    {
      this.framescale = this.selector.GetSize();
      this.framescale.Y += this.pad.Y;
      this.framescale.Y += this.text.GetSize().Y + this.pad.Y;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.Y = -0.5f * this.framescale.Y;
      this.text.vLocation = vector2;
      this.text.vLocation.Y += 0.5f * this.text.GetSize().Y;
      vector2.Y += this.text.GetSize().Y + this.pad.Y;
      this.selector.location = vector2;
      this.selector.location.Y += 0.5f * this.selector.GetSize().Y;
      vector2.Y += this.selector.GetSize().Y + 2f * this.pad.Y;
    }

    public bool UpdateSelectorBarWithLabels(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      if (this.calculate)
      {
        this.CalculateScaleAndPosition();
        this.calculate = true;
      }
      this.selector.UpdateSelectorBar(player, offset, DeltaTime);
      int selected = this.selector.Selected;
      if (selected > -1)
        this.text.textToWrite = this.labels[selected];
      return false;
    }

    public void DrawSelectorBarWithLabels(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.text.DrawZGenericText(offset, spritebatch);
      this.selector.DrawSelectorBar(spritebatch, offset);
    }
  }
}
