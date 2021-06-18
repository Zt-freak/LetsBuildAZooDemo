// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalNotification.StatsListerBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections;
using System.Collections.Specialized;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_AnimalNotification
{
  internal class StatsListerBox
  {
    private OrderedDictionary statsList = new OrderedDictionary();
    private CustomerFrame frame;
    private float basescale;
    public Vector2 location;
    private static Color cream = new Color(ColourData.Z_Cream.X, ColourData.Z_Cream.Y, ColourData.Z_Cream.Z);
    private static float padX10;
    private static float padY10;
    private Vector2 framescale;
    private float lineHeight;
    private UIScaleHelper uiScale;
    private bool hasFrame;
    private SpringFont font;
    private bool centered;

    public int Count => this.statsList.Count;

    public StatsListerBox(float basescale_, bool hasFrame_ = true, bool centered_ = true, bool useLargerFont = true)
    {
      this.hasFrame = hasFrame_;
      this.centered = centered_;
      this.basescale = basescale_;
      this.uiScale = new UIScaleHelper(basescale_);
      StatsListerBox.padX10 = this.uiScale.ScaleX(10f);
      StatsListerBox.padY10 = this.uiScale.ScaleY(10f);
      this.font = !useLargerFont ? AssetContainer.springFont : AssetContainer.SpringFontX1AndHalf;
      this.lineHeight = this.uiScale.ScaleY(this.font.MeasureString("some arbitrary").Y);
      this.SizeFrameToFit();
    }

    public void AddOrUpdate(string name, string value)
    {
      this.statsList[(object) name] = (object) value;
      this.SizeFrameToFit();
    }

    public void SizeFrameToFit(float rawWidthNoPadding = 170f)
    {
      this.framescale = Vector2.Zero;
      if (this.centered)
        this.framescale.Y = 0.5f * this.lineHeight;
      this.framescale.X = this.uiScale.ScaleX(rawWidthNoPadding);
      if (this.hasFrame)
      {
        this.framescale.X += this.uiScale.ScaleX(20f);
        this.framescale.Y += this.uiScale.ScaleY(20f);
      }
      foreach (DictionaryEntry stats in this.statsList)
        this.framescale.Y += this.lineHeight;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
    }

    public void DrawStatsListerBox(Vector2 offset, SpriteBatch spritebatch)
    {
      Vector2 vector2 = offset + this.location;
      if (this.hasFrame)
      {
        this.frame.DrawCustomerFrame(vector2, spritebatch);
        vector2.X += StatsListerBox.padX10;
        vector2.Y += StatsListerBox.padY10;
      }
      vector2.Y -= 0.5f * this.GetSize().Y;
      if (this.centered)
        vector2.Y += 0.5f * this.lineHeight;
      if (!this.centered)
        vector2.X += -0.5f * this.framescale.X;
      foreach (DictionaryEntry stats in this.statsList)
      {
        if (this.centered)
          TextFunctions.DrawJustifiedText(stats.Key.ToString() + ": " + stats.Value, this.basescale, vector2, StatsListerBox.cream, 1f, this.font, spritebatch);
        else
          TextFunctions.DrawTextWithDropShadow(stats.Key.ToString() + ": " + stats.Value, this.basescale, vector2, StatsListerBox.cream, 1f, this.font, spritebatch, false);
        vector2.Y += this.lineHeight;
      }
    }

    public Vector2 GetSize() => this.framescale;
  }
}
