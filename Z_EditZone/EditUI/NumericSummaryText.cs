// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.EditUI.NumericSummaryText
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_EditZone.EditUI
{
  internal class NumericSummaryText : GameObject
  {
    private string texttowrite;

    public NumericSummaryText(float BaseScale, string _texttowrite)
    {
      this.texttowrite = _texttowrite;
      this.scale = BaseScale;
    }

    public void SetString(string NewText) => this.texttowrite = NewText;

    public void DrawNumericSummaryText(Vector2 Offset, SpriteBatch spritebatch) => TextFunctions.DrawTextWithDropShadow(this.texttowrite, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false, true);
  }
}
