// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ZGenericText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class ZGenericText : GameObject
  {
    public string textToWrite;
    private bool CenterJustify;
    private bool RightJustify;

    public SpringFont fontToUse { get; private set; }

    public ZGenericText(
      float _BaseScale,
      bool _CenterJustify = true,
      bool _RightJustify = false,
      bool _UseOnePointFiveFont = false,
      bool _UseRoundaboutHugeFont = false)
    {
      this.SetUp(string.Empty, _BaseScale, _CenterJustify, _RightJustify, _UseOnePointFiveFont, _UseRoundaboutHugeFont);
    }

    public ZGenericText(
      string _textToWrite,
      float _BaseScale,
      bool _CenterJustify = true,
      bool RightJustify = false,
      bool _UseOnePointFiveFont = false,
      bool _UseRoundaboutHugeFont = false)
    {
      this.SetUp(_textToWrite, _BaseScale, _CenterJustify, RightJustify, _UseOnePointFiveFont, _UseRoundaboutHugeFont);
    }

    public ZGenericText(
      string _textToWrite,
      float _BaseScale,
      SpringFont _fontToUse,
      bool _CenterJustify = true,
      bool _RightJustify = false)
    {
      this.SetUp(_textToWrite, _BaseScale, _fontToUse, _CenterJustify, _RightJustify);
    }

    private void SetUp(
      string _textToWrite,
      float _BaseScale,
      bool _CenterJustify = true,
      bool _RightJustify = false,
      bool _UseOnePointFiveFont = false,
      bool _UseRoundaboutHugeFont = false)
    {
      SpringFont _fontToUse = !_UseRoundaboutHugeFont ? (!_UseOnePointFiveFont ? Z_GameFlags.GetSmallFont(ref _BaseScale) : AssetContainer.SpringFontX1AndHalf) : AssetContainer.roundaboutFont;
      this.SetUp(_textToWrite, _BaseScale, _fontToUse, _CenterJustify, _RightJustify);
    }

    private void SetUp(
      string _textToWrite,
      float _BaseScale,
      SpringFont _fontToUse,
      bool _CenterJustify = true,
      bool _RightJustify = false)
    {
      this.scale = _BaseScale;
      this.SetAllColours(ColourData.Z_Cream);
      this.fontToUse = _fontToUse;
      this.textToWrite = _textToWrite;
      this.CenterJustify = _CenterJustify;
      this.RightJustify = _RightJustify;
    }

    public void SetInactiveColor() => this.SetAllColours(Color.LightGray.ToVector3());

    public Vector2 GetSize() => this.fontToUse.MeasureString(this.textToWrite) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawZGenericText(Vector2 offset, SpriteBatch spriteBatch, float alphaMult = 1f)
    {
      offset += this.vLocation;
      if (this.CenterJustify)
        TextFunctions.DrawJustifiedText(this.textToWrite, this.scale, offset, this.GetColour(), this.fAlpha * alphaMult, this.fontToUse, spriteBatch);
      else
        TextFunctions.DrawTextWithDropShadow(this.textToWrite, this.scale, offset, this.GetColour(), this.fAlpha * alphaMult, this.fontToUse, spriteBatch, false, this.RightJustify, false, 0.0f, 0);
    }
  }
}
