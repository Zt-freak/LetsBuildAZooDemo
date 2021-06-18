// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.Content.ArrowWithPercent
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.Pieces.Content
{
  internal class ArrowWithPercent
  {
    public Vector2 location;
    private MiniArrow arrow;
    private GameObject textObj;
    private string textToWrite;
    private ZGenericText dash;
    private bool DrawDashIf0;
    private bool MoreIsBad;
    private bool DrawDash;

    public ArrowWithPercent(
      float percentIncrease,
      float BaseScale,
      bool _MoreIsBad = false,
      bool _DrawDashIf0 = false)
    {
      this.DrawDashIf0 = _DrawDashIf0;
      this.MoreIsBad = _MoreIsBad;
      float num = new UIScaleHelper(BaseScale).ScaleY(5f);
      this.arrow = new MiniArrow(BaseScale);
      this.arrow.vLocation.Y -= num * 0.5f;
      this.textObj = new GameObject();
      this.textObj.scale = BaseScale;
      this.textObj.vLocation.X = this.arrow.vLocation.X;
      this.textObj.vLocation.Y += (float) ((double) (AssetContainer.springFont.MeasureString("X").Y * this.textObj.scale * Sengine.ScreenRatioUpwardsMultiplier.Y) * 0.5 + (double) num * 0.5);
      this.dash = new ZGenericText("-", BaseScale);
      Vector2 size = this.dash.GetSize();
      this.dash.vLocation = this.arrow.location;
      this.dash.vLocation.Y -= size.Y * 0.25f;
      this.SetNewPercent(percentIncrease);
    }

    public void SetNewPercent(float percentIncrease)
    {
      this.textToWrite = "";
      this.DrawDash = false;
      percentIncrease = (float) Math.Floor((double) percentIncrease);
      if ((double) percentIncrease > 0.0 && !this.MoreIsBad || (double) percentIncrease < 0.0 && this.MoreIsBad)
      {
        this.textObj.SetAllColours(ColourData.Z_ArrowAndTextGreen);
        if (!this.MoreIsBad)
          this.textToWrite = "+";
        this.arrow.SetPointingUp();
      }
      else if ((double) percentIncrease < 0.0 && !this.MoreIsBad || (double) percentIncrease > 0.0 && this.MoreIsBad)
      {
        this.textObj.SetAllColours(ColourData.Z_ArrowAndTextRed);
        this.arrow.SetPointingDown();
      }
      else
      {
        this.textObj.SetAllColours(ColourData.Z_Cream);
        this.arrow.SetNeutral();
      }
      this.textToWrite = this.textToWrite + (object) percentIncrease + "%";
      if (!this.DrawDashIf0 || (double) percentIncrease != 0.0)
        return;
      this.DrawDash = true;
    }

    public void DrawArrowWithPercent(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.DrawDash)
        this.dash.DrawZGenericText(offset, spriteBatch);
      else
        this.arrow.DrawMiniArrow(offset, spriteBatch);
      TextFunctions.DrawJustifiedText(this.textToWrite, this.textObj.scale, this.textObj.vLocation + offset, this.textObj.GetColour(), 1f, AssetContainer.springFont, spriteBatch);
    }
  }
}
