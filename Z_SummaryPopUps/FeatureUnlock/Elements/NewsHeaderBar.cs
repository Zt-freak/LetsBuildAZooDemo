// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements.NewsHeaderBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_SummaryPopUps.FeatureUnlock.Elements
{
  internal class NewsHeaderBar
  {
    public Vector2 location;
    private RowSegmentRectangle topLine;
    private RowSegmentRectangle bottomLine;
    private ZGenericText leftText;
    private ZGenericText middleText;
    private ZGenericText rightText;
    private Vector2 size;
    private UIScaleHelper scaleHelper;
    private Vector2 buffer;

    public NewsHeaderBar(
      float BaseScale,
      string leftString,
      string middleString,
      string rightString,
      float width)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.ConstructLines(width);
      this.ConstructStrings_Three(leftString, middleString, rightString, BaseScale, width);
    }

    public NewsHeaderBar(float BaseScale, string leftString, string rightString, float width)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.ConstructLines(width);
      this.ConstructStrings_Two(leftString, rightString, BaseScale, width);
    }

    public NewsHeaderBar(float BaseScale, string singleString, float width)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.buffer = this.scaleHelper.DefaultBuffer;
      this.ConstructLines(width);
      this.middleText = new ZGenericText(singleString, BaseScale, _UseOnePointFiveFont: true);
      this.middleText.SetAllColours(ColourData.Z_DarkTextGray);
      this.middleText.vLocation.X = width * 0.5f;
      float num1 = 0.0f;
      this.topLine.vLocation.Y = 0.0f;
      float num2 = num1 + this.topLine.GetSize().Y;
      this.middleText.vLocation.Y = num2;
      this.middleText.vLocation.Y += this.middleText.GetSize().Y * 0.5f;
      float num3 = num2 + this.middleText.GetSize().Y;
      this.bottomLine.vLocation.Y = num3;
      float y = num3 + this.bottomLine.GetSize().Y;
      this.size = new Vector2(width, y);
    }

    private void ConstructLines(float width)
    {
      float height = this.scaleHelper.ScaleY(3f);
      this.topLine = new RowSegmentRectangle(width, height, ColourData.Z_DarkTextGray, 1f);
      this.topLine.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.bottomLine = new RowSegmentRectangle(width, height, ColourData.Z_DarkTextGray, 1f);
      this.bottomLine.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.bottomLine.SetAllColours(ColourData.Z_DarkTextGray);
    }

    private void ConstructStrings_Two(
      string leftString,
      string rightString,
      float BaseScale,
      float width)
    {
      this.leftText = new ZGenericText(leftString, BaseScale, false, true, true);
      this.rightText = new ZGenericText(rightString, BaseScale, false, _UseOnePointFiveFont: true);
      this.leftText.SetAllColours(ColourData.Z_DarkTextGray);
      this.rightText.SetAllColours(ColourData.Z_DarkTextGray);
      float num1 = this.scaleHelper.ScaleX(50f);
      this.leftText.vLocation.X = width * 0.5f - num1;
      this.rightText.vLocation.X = width * 0.5f + num1;
      float num2 = 0.0f;
      this.topLine.vLocation.Y = 0.0f;
      float num3 = num2 + this.topLine.GetSize().Y;
      this.leftText.vLocation.Y = num3;
      this.rightText.vLocation.Y = num3;
      float num4 = num3 + this.leftText.GetSize().Y;
      this.bottomLine.vLocation.Y = num4;
      float y = num4 + this.bottomLine.GetSize().Y;
      this.size = new Vector2(width, y);
    }

    private void ConstructStrings_Three(
      string leftString,
      string middleString,
      string rightString,
      float BaseScale,
      float width)
    {
      this.leftText = new ZGenericText(leftString, BaseScale, false, _UseOnePointFiveFont: true);
      this.middleText = new ZGenericText(middleString, BaseScale, _UseOnePointFiveFont: true);
      this.rightText = new ZGenericText(rightString, BaseScale, false, true, true);
      this.leftText.SetAllColours(ColourData.Z_DarkTextGray);
      this.middleText.SetAllColours(ColourData.Z_DarkTextGray);
      this.rightText.SetAllColours(ColourData.Z_DarkTextGray);
      this.leftText.vLocation.X = 0.0f + this.buffer.X;
      this.middleText.vLocation.X = width * 0.5f;
      this.rightText.vLocation.X = width - this.buffer.X;
      float num1 = 0.0f;
      this.topLine.vLocation.Y = 0.0f;
      float num2 = num1 + this.topLine.GetSize().Y;
      this.leftText.vLocation.Y = num2;
      this.middleText.vLocation.Y = num2;
      this.middleText.vLocation.Y += this.middleText.GetSize().Y * 0.5f;
      this.rightText.vLocation.Y = num2;
      float num3 = num2 + this.leftText.GetSize().Y;
      this.bottomLine.vLocation.Y = num3;
      float y = num3 + this.bottomLine.GetSize().Y;
      this.size = new Vector2(width, y);
    }

    public Vector2 GetSize() => this.size;

    public void DrawNewsHeaderBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.leftText != null)
        this.leftText.DrawZGenericText(offset, spriteBatch);
      if (this.middleText != null)
        this.middleText.DrawZGenericText(offset, spriteBatch);
      if (this.rightText != null)
        this.rightText.DrawZGenericText(offset, spriteBatch);
      this.topLine.DrawRowSegmentRectangle(offset, spriteBatch);
      this.bottomLine.DrawRowSegmentRectangle(offset, spriteBatch);
    }
  }
}
