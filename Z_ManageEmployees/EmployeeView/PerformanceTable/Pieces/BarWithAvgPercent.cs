// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.BarWithAvgPercent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.Content;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces
{
  internal class BarWithAvgPercent
  {
    public Vector2 location;
    private SatisfactionBar satisfactionBar;
    private ArrowWithPercent arrowWithPercent;
    private GameObject textObj;
    private string textToWrite;
    private float BaseScale;
    private float Xbuffer;
    private float Ybuffer;

    public BarWithAvgPercent(float percentFilled, float _BaseScale, bool displayPercentage = true)
    {
      this.SetScales(_BaseScale);
      this.satisfactionBar = new SatisfactionBar(1f, this.BaseScale * 2f, BarSIze.VerySmall);
      this.SetValues(percentFilled, displayPercentage);
    }

    public BarWithAvgPercent(
      float percentFilled,
      float referenceAverage,
      float _BaseScale,
      bool MoreIsBad = false,
      bool NoBar_JustArrow = false)
    {
      this.SetScales(_BaseScale);
      if (!NoBar_JustArrow)
        this.satisfactionBar = new SatisfactionBar(1f, this.BaseScale * 2f, BarSIze.VerySmall);
      this.SetValues(percentFilled, referenceAverage, MoreIsBad);
    }

    private void SetScales(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = uiScaleHelper.ScaleX(12f);
      this.Ybuffer = uiScaleHelper.ScaleY(5f);
    }

    private void SetValues(float percentFilled, bool displayPercentage)
    {
      this.satisfactionBar.SetFullness(percentFilled);
      if (!displayPercentage)
        return;
      this.textToWrite = Math.Floor((double) percentFilled * 100.0).ToString() + "%";
      this.textObj = new GameObject();
      this.textObj.scale = this.BaseScale;
      this.textObj.SetAllColours(ColourData.Z_Cream);
      this.textObj.vLocation.X += this.satisfactionBar.GetVScale().X * 0.5f;
      this.textObj.vLocation.X += this.Xbuffer;
    }

    public void SetValues(float percentFilled, float referenceAverage, bool MoreIsBad = false)
    {
      if (this.satisfactionBar != null)
        this.satisfactionBar.SetFullness(percentFilled);
      this.arrowWithPercent = new ArrowWithPercent((float) Math.Floor(((double) percentFilled - (double) referenceAverage) * 100.0), this.BaseScale, MoreIsBad);
      if (this.satisfactionBar != null)
        this.arrowWithPercent.location.X += this.satisfactionBar.GetVScale().X * 0.5f;
      this.arrowWithPercent.location.X += this.Xbuffer;
    }

    public Vector2 GetSize_BarOnly() => this.satisfactionBar.GetVScale();

    public float GetExtraLengthFromText() => this.Xbuffer;

    public void DrawBarWithAvgPercent(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.satisfactionBar != null)
        this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
      if (this.textObj != null)
        TextFunctions.DrawJustifiedText(this.textToWrite, this.textObj.scale, this.textObj.vLocation + offset, this.textObj.GetColour(), 1f, AssetContainer.springFont, spriteBatch);
      if (this.arrowWithPercent == null)
        return;
      this.arrowWithPercent.DrawArrowWithPercent(offset, spriteBatch);
    }
  }
}
