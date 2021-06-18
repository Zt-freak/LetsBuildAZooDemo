// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns.SatisfactionBarWithBigNumber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns
{
  internal class SatisfactionBarWithBigNumber
  {
    public Vector2 location;
    private SatisfactionBar satisfactionBar;
    private MoralityBarWithIcon moralityBar;
    private ZGenericText percentText;
    private float width;
    private bool isUsingCustomBarColor;

    public SatisfactionBarWithBigNumber(float BaseScale, float textSpace_Raw = 25f, bool AddMoralityIcon = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.width = 0.0f;
      Vector2 zero = Vector2.Zero;
      Vector2 size;
      if (AddMoralityIcon)
      {
        this.moralityBar = new MoralityBarWithIcon(true, BaseScale);
        size = this.moralityBar.GetSize();
        this.moralityBar.location.X = size.X * 0.5f;
      }
      else
      {
        this.satisfactionBar = new SatisfactionBar(1f, BaseScale);
        size = this.satisfactionBar.GetSize();
        this.satisfactionBar.vLocation.X = size.X * 0.5f;
      }
      this.width += size.X;
      this.width += defaultXbuffer + uiScaleHelper.ScaleX(textSpace_Raw * 0.5f);
      this.percentText = new ZGenericText(BaseScale, _UseOnePointFiveFont: true);
      this.percentText.vLocation.X = this.width;
      this.width += uiScaleHelper.ScaleX(textSpace_Raw * 0.5f);
    }

    public void SetBarColours(Vector3 color)
    {
      this.isUsingCustomBarColor = true;
      if (this.satisfactionBar == null)
        return;
      this.satisfactionBar.SetBarColours(color);
    }

    public void SetGoodness_ForMorality(bool isGoodOrEvil)
    {
      if (this.moralityBar == null)
        throw new Exception("WHY YOU CALL THIS?");
      this.moralityBar.ChangeGoodness(isGoodOrEvil);
    }

    public Vector2 GetSize() => new Vector2(this.width, this.percentText.GetSize().Y);

    public void SetBarValues(
      float percent_float,
      string displayThisValueInstead = "",
      bool DontDisplayValue = false)
    {
      if (this.moralityBar != null)
        this.moralityBar.SetFullness(percent_float);
      else
        this.satisfactionBar.SetFullness(percent_float, DoSetColorBasedOnValue: (!this.isUsingCustomBarColor));
      if (DontDisplayValue)
        return;
      if (string.IsNullOrEmpty(displayThisValueInstead))
        this.percentText.textToWrite = ((int) Math.Round((double) percent_float * 100.0)).ToString() + "%";
      else
        this.percentText.textToWrite = displayThisValueInstead;
    }

    public void DrawSatisfactionBarWithBigNumber(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.moralityBar != null)
        this.moralityBar.DrawMoralityBarWithIcon(offset);
      else
        this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
      this.percentText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
