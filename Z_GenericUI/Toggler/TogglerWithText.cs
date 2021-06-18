// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.Toggler.TogglerWithText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_GenericUI.Toggler
{
  internal class TogglerWithText
  {
    public Vector2 location;
    private ToggleArrowButton rightArrow;
    private ToggleArrowButton leftArrow;
    private SimpleTextHandler text;
    private string[] strings;
    private float maxWidth;
    private float maxParaWidth;
    private bool UseOnePointFive;
    private float BaseScale;
    private Vector2 size;
    private bool TextTallerThanArrows;

    public int currentIndex { get; private set; }

    public TogglerWithText(
      float _BaseScale,
      float _maxWidth,
      SpringFont fontToUse,
      params string[] _strings)
    {
      this.strings = _strings;
      this.maxWidth = _maxWidth;
      this.BaseScale = _BaseScale;
      Vector2 defaultBuffer = new UIScaleHelper(this.BaseScale).DefaultBuffer;
      this.rightArrow = new ToggleArrowButton(this.BaseScale);
      this.leftArrow = new ToggleArrowButton(this.BaseScale, false);
      Vector2 size1 = this.rightArrow.GetSize();
      this.maxParaWidth = (float) ((double) this.maxWidth - (double) size1.X - (double) size1.X - (double) defaultBuffer.X * 2.0);
      string empty = string.Empty;
      float num = 0.0f;
      for (int index = 0; index < this.strings.Length; ++index)
      {
        Vector2 vector2 = fontToUse.MeasureString(this.strings[index]);
        if ((double) num == 0.0 || (double) vector2.X > (double) num)
        {
          empty = this.strings[index];
          num = vector2.X;
        }
      }
      this.UseOnePointFive = fontToUse == AssetContainer.SpringFontX1AndHalf;
      this.text = new SimpleTextHandler(empty, this.maxParaWidth, true, this.BaseScale, this.UseOnePointFive, true);
      this.text.SetAllColours(ColourData.Z_Cream);
      Vector2 size2 = this.text.GetSize(true);
      this.size.X = size2.X;
      this.size.Y = Math.Max(size1.Y, size2.Y);
      this.leftArrow.location.X = (float) (-(double) this.size.X * 0.5) - defaultBuffer.X;
      this.rightArrow.location.X = this.size.X * 0.5f + defaultBuffer.X;
      if ((double) size1.Y > (double) size2.Y)
      {
        this.rightArrow.location.Y += size1.Y * 0.5f;
      }
      else
      {
        this.TextTallerThanArrows = true;
        this.rightArrow.location.Y = size2.Y * 0.5f;
      }
      this.leftArrow.location.Y = this.rightArrow.location.Y;
      this.leftArrow.location.X -= size1.X * 0.5f;
      this.rightArrow.location.X += size1.X * 0.5f;
      this.size.X += (float) ((double) size1.X + (double) size1.X + (double) defaultBuffer.X * 2.0);
      this.ChangeToThisIndex(0);
    }

    private void SetUp()
    {
    }

    public Vector2 GetSize() => this.size;

    public void ChangeToThisIndex(int newindex)
    {
      this.currentIndex = newindex >= 0 ? (newindex <= this.strings.Length - 1 ? newindex : 0) : this.strings.Length - 1;
      this.SetNewText(this.strings[this.currentIndex]);
    }

    private void SetNewText(string textToWrite)
    {
      this.text = new SimpleTextHandler(textToWrite, this.maxParaWidth, true, this.BaseScale, this.UseOnePointFive, true);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.Location.Y = this.size.Y * 0.5f;
      if (!this.TextTallerThanArrows)
        return;
      this.text.Location.Y += (float) (-(double) this.text.GetHeightOfParagraph() * 0.5 + (double) this.text.GetHeightOfOneLine() * 0.5);
    }

    public bool UpdateTogglerWithText(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.strings.Length > 1)
      {
        if (this.rightArrow.UpdateToggleArrowButton(player, DeltaTime, offset))
        {
          this.ChangeToThisIndex(this.currentIndex + 1);
          return true;
        }
        if (this.leftArrow.UpdateToggleArrowButton(player, DeltaTime, offset))
        {
          this.ChangeToThisIndex(this.currentIndex - 1);
          return true;
        }
      }
      return false;
    }

    public void DrawTogglerWithText(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.strings.Length <= 1)
        return;
      this.leftArrow.DrawToggleArrowButton(offset, spriteBatch);
      this.rightArrow.DrawToggleArrowButton(offset, spriteBatch);
    }
  }
}
