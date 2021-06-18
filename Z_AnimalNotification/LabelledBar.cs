// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.LabelledBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_AnimalNotification
{
  internal class LabelledBar
  {
    private SatisfactionBar bar;
    public Vector2 location;
    private float basescale;
    private static Color colour = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private UIScaleHelper uiScale;
    private Vector2 framescale;
    private ZGenericText label;
    private CustomerFrame frame;

    public LabelledBar(
      float value0to1,
      Vector3 colour,
      string label_,
      float basescale_,
      bool drawFromBetweenTextAndBar = true,
      bool useLargerFont = false,
      BarSIze barsize = BarSIze.Normal)
    {
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(basescale_);
      this.label = new ZGenericText(label_, this.basescale, _UseOnePointFiveFont: useLargerFont);
      this.bar = barsize != BarSIze.Thin ? new SatisfactionBar(value0to1, this.basescale, barsize) : new SatisfactionBar(value0to1, this.basescale * 2f, barsize);
      this.bar.SetBarColours(colour);
      Vector2 size = this.label.GetSize();
      this.framescale = this.bar.GetSize();
      this.framescale.X += 0.5f * this.uiScale.GetDefaultXBuffer();
      this.framescale.X += size.X;
      if (drawFromBetweenTextAndBar)
      {
        this.bar.vLocation.X = 0.5f * this.bar.GetSize().X + this.uiScale.ScaleX(2.5f);
        this.label.vLocation.X = -0.5f * this.label.GetSize().X - this.uiScale.ScaleX(2.5f);
      }
      else
      {
        this.label.vLocation = 0.5f * this.label.GetSize();
        this.bar.vLocation = this.label.vLocation;
        this.bar.vLocation.X += size.X + 0.5f * this.uiScale.GetDefaultXBuffer();
      }
    }

    public void SetNewValues(float fullness, int layer = 0, bool DoSetColorsBasedOnValues = false) => this.bar.SetFullness(fullness, layer, DoSetColorsBasedOnValues);

    public void DrawLabelledBar(Vector2 offset, SpriteBatch spritebatch)
    {
      offset += this.location;
      this.bar.DrawSatisfactionBar(offset, spritebatch);
      this.label.DrawZGenericText(offset, spritebatch);
    }

    public Vector2 GetBarSize()
    {
      Vector2 size = this.bar.GetSize();
      size.X += 0.25f * this.uiScale.DefaultBuffer.X;
      return size;
    }

    public Vector2 GetLabelSize()
    {
      Vector2 size = this.label.GetSize();
      size.X += 0.25f * this.uiScale.DefaultBuffer.X;
      return size;
    }

    public Vector2 GetSize() => this.framescale;
  }
}
