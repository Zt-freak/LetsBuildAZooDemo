// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.BussInfo.Route_Row.BusHeading
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Bus.BussInfo.Route_Row
{
  internal class BusHeading : GameObject
  {
    private SpringFont pointerfont;
    private string Heading;
    private bool UseBigFont;

    public BusHeading(string WriteThis, float BaseScale, bool _UseBigFont = false)
    {
      this.UseBigFont = _UseBigFont;
      this.Heading = WriteThis;
      this.scale = BaseScale;
      this.SetAllColours(ColourData.Z_Cream);
      this.pointerfont = Z_GameFlags.GetSmallFont(ref BaseScale);
    }

    public void DrawBusHeading(Vector2 Offset, SpriteBatch spritebatch)
    {
      if (!this.UseBigFont)
        TextFunctions.DrawJustifiedText(this.Heading, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, this.pointerfont, spritebatch);
      else
        TextFunctions.DrawJustifiedText(this.Heading, this.scale, this.vLocation + Offset, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
    }
  }
}
