// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.OperationBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary
{
  internal class OperationBar
  {
    public Vector2 location;
    private SatisfactionBar satisfactionBar;
    private ZGenericText statusText;
    private Vector2 size;

    public OperationBar(float BaseScale, bool IncludeSmallText = true, bool SmallTextBelow = false)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.satisfactionBar = new SatisfactionBar(0.0f, BaseScale);
      this.satisfactionBar.vLocation.X += this.satisfactionBar.GetSize().X * 0.5f;
      this.satisfactionBar.vLocation.Y += this.satisfactionBar.GetSize().Y * 0.5f;
      this.size.X += this.satisfactionBar.GetSize().X;
      this.size.Y += this.satisfactionBar.GetSize().Y;
      if (!IncludeSmallText)
        return;
      if (SmallTextBelow)
      {
        this.size.Y += uiScaleHelper.ScaleY(2f);
        this.statusText = new ZGenericText("Xg", BaseScale);
        this.statusText.vLocation.Y = this.size.Y;
        this.statusText.vLocation.Y += this.statusText.GetSize().Y * 0.5f;
        this.statusText.vLocation.X += this.satisfactionBar.GetSize().X * 0.5f;
        this.size.Y += this.statusText.GetSize().Y;
      }
      else
      {
        this.statusText = new ZGenericText("Xg", BaseScale, false);
        this.size.X += uiScaleHelper.DefaultBuffer.X;
        this.statusText.vLocation.Y = this.size.Y * 0.5f;
        this.statusText.vLocation.X = this.size.X;
        this.statusText.vLocation.Y -= this.statusText.GetSize().Y * 0.5f;
        this.size.X += this.statusText.GetSize().X;
      }
      this.statusText.SetAllColours(ColourData.Z_BarYellow);
    }

    public Vector2 GetSize() => this.size;

    public void SetValueAndColour(float _value)
    {
      this.satisfactionBar.SetFullness(_value);
      string str;
      if ((double) _value >= 1.0)
      {
        float num = _value - 1f;
        if ((double) num > 0.0)
        {
          this.satisfactionBar.SetBarColours(ColourData.Z_BarRed);
          str = string.Format("{0}% Overload", (object) (int) Math.Floor((double) num * 100.0));
        }
        else
        {
          this.satisfactionBar.SetBarColours(ColourData.Z_BarBabyGreen);
          str = "";
        }
      }
      else if ((double) _value >= 0.75)
      {
        this.satisfactionBar.SetBarColours(ColourData.Z_BarBabyGreen);
        str = "";
      }
      else if ((double) _value >= 0.5)
      {
        this.satisfactionBar.SetBarColours(ColourData.Z_BarYellow);
        str = "Underused";
      }
      else
      {
        this.satisfactionBar.SetBarColours(ColourData.Z_BarBabyGreen);
        str = "Underused";
      }
      if (this.statusText == null)
        return;
      this.statusText.textToWrite = str;
    }

    public void DrawOperationBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
      if (this.statusText == null)
        return;
      this.statusText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
