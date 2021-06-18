// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.LimitedLineRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class LimitedLineRenderer : GameObject
  {
    private SpringFont fontpointer;
    private int MaxLines;
    private List<string> textx;
    private bool LeftJustify;
    private float LineHeight;
    private float BaseScale;

    public LimitedLineRenderer(int _MaxLines, bool _LeftJustify, float _BaseScale)
    {
      this.SetAllColours(ColourData.Z_Cream);
      this.BaseScale = _BaseScale;
      this.LeftJustify = _LeftJustify;
      this.textx = new List<string>();
      this.MaxLines = _MaxLines;
      this.scale = this.BaseScale;
      this.fontpointer = Z_GameFlags.GetSmallFont(ref this.scale);
      this.LineHeight = this.fontpointer.MeasureString("yYLmg").Y;
      if (this.MaxLines == 0)
        throw new Exception("cannot have single line");
    }

    public void AddLine(string TextToAdd)
    {
      while (this.textx.Count >= this.MaxLines)
        this.textx.RemoveAt(0);
      this.textx.Add(TextToAdd);
    }

    public void DrawLimitedLineRenderer(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.vLocation;
      for (int index = 0; index < this.textx.Count; ++index)
      {
        if (index < this.MaxLines)
        {
          if (this.LeftJustify)
          {
            TextFunctions.DrawTextWithDropShadow(this.textx[index], this.scale, Offset, this.GetColour(), this.fAlpha, this.fontpointer, spritebatch, false);
            Offset.Y += this.LineHeight * this.scale;
            Offset.Y += this.BaseScale * 5f;
          }
          else
          {
            TextFunctions.DrawJustifiedText(this.textx[index], this.scale, Offset, this.GetColour(), this.fAlpha, this.fontpointer, spritebatch);
            Offset.Y += this.LineHeight * this.scale;
          }
        }
      }
    }
  }
}
