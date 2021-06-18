// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SliderWithValue
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class SliderWithValue
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private DragAndBar slider;
    private string valuestr;
    private Vector2 valueloc;
    private string prefix;
    private string suffix;
    private int minval;
    private int maxval;
    private int value;
    private bool discrete;
    private int numDiscreteValues;
    private bool forcePlusSymbol;
    private bool useCustomLabels;
    private List<string> customlabels;

    public SliderWithValue(
      int minvalue,
      int maxvalue,
      float width,
      float basescale_,
      float startval0to1 = 0.5f,
      string prefix_ = "$",
      string suffix_ = "",
      int numDiscreteValues_ = 0,
      bool forcePlusSymbol_ = false)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.minval = minvalue;
      this.maxval = maxvalue;
      this.prefix = prefix_;
      this.suffix = suffix_;
      this.numDiscreteValues = numDiscreteValues_;
      this.discrete = this.numDiscreteValues > 1;
      this.forcePlusSymbol = forcePlusSymbol_;
      this.slider = new DragAndBar(false, startval0to1, width, this.basescale, numDiscreteValues_);
      this.slider.ExtraHeight = 2f * defaultBuffer.Y;
      this.slider.ExtraWidth = 2f * defaultBuffer.X;
      this.value = (int) Math.Round((double) this.minval + (double) this.slider.CurrentDragPercent * (double) (this.maxval - this.minval));
      this.valuestr = this.prefix + this.value.ToString() + this.suffix;
      float num = 2f * this.uiscale.ScaleVector2(AssetContainer.springFont.MeasureString(this.valuestr)).Y;
      this.framescale = this.slider.GetSize();
      this.framescale.Y += num;
      this.frame = new CustomerFrame(this.framescale, true, this.basescale);
      Vector2 vector2 = new Vector2();
      vector2.Y = -0.5f * this.framescale.Y;
      this.valueloc = vector2;
      this.valueloc.Y += 0.5f * num;
      vector2.Y += num;
      this.slider.Location = vector2;
      this.slider.Location.Y += 0.5f * this.slider.GetSize().Y;
      vector2.Y += this.slider.GetSize().Y;
    }

    public Vector2 GetSize() => this.framescale;

    public int GetValue() => this.value;

    public void UseCustomLabels(List<string> labels)
    {
      this.useCustomLabels = true;
      this.customlabels = labels;
    }

    public bool UpdateSliderWithValue(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      int num1 = 0;
      float num2 = 0.0f;
      this.slider.UpdateDragAndBar(player, DeltaTime, offset);
      if (this.discrete)
      {
        num2 = (float) Math.Round((double) this.slider.CurrentDragPercent * (double) (this.numDiscreteValues - 1));
        this.value = (int) ((double) this.minval + (double) (num2 / (float) (this.numDiscreteValues - 1)) * (double) (this.maxval - this.minval));
      }
      else
        this.value = (int) Math.Round((double) this.minval + (double) this.slider.CurrentDragPercent * (double) (this.maxval - this.minval));
      if (this.discrete && this.useCustomLabels && (double) num2 < (double) this.customlabels.Count)
      {
        this.valuestr = this.customlabels[(int) num2];
        return num1 != 0;
      }
      string str = "";
      if (this.forcePlusSymbol && (double) this.value > 0.0)
        str = "+";
      this.valuestr = str + this.prefix + this.value.ToString() + this.suffix;
      return num1 != 0;
    }

    public void DrawSliderWithValue(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.slider.DrawDragAndBar(spritebatch, offset);
      TextFunctions.DrawJustifiedText(this.valuestr, this.basescale, this.valueloc + offset, new Color(ColourData.Z_Cream), 1f, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}
