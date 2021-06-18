// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.CoinAndString
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class CoinAndString
  {
    private Coin coin;
    public Vector2 Location;
    private int Value;
    private GameObject TextClr;

    public CoinAndString(int _Value)
    {
      this.Value = _Value;
      this.TextClr = new GameObject();
      this.TextClr.SetAllColours(ColourData.Z_Cream);
      this.coin = new Coin();
    }

    public void UpdateCoinAndString()
    {
    }

    public void SetTextColour(Vector3 Clr) => this.TextClr.SetAllColours(Clr);

    public void DrawCoinAndStringSmall(SpriteBatch spritebatch, Vector2 Offset)
    {
      float scale = 2f;
      Vector2 vector2 = SpringFontUtil.MeasureString("$" + (object) this.Value, AssetContainer.springFont);
      vector2.X *= scale;
      vector2.X += 15f;
      vector2.X += (float) ((double) this.coin.DrawRect.Width * (double) this.coin.scale * 0.5);
      Offset.X -= vector2.X * 0.5f;
      this.coin.DrawCoin(spritebatch, Offset + this.Location + new Vector2(0.0f, 0.0f));
      TextFunctions.DrawTextWithDropShadow("$" + (object) this.Value, scale, Offset + this.Location + new Vector2(15f, -9f), this.TextClr.GetColour(), 1f, AssetContainer.springFont, spritebatch, false);
    }

    public void DrawCoinAndString(SpriteBatch spritebatch, Vector2 Offset)
    {
      this.coin.DrawCoin(spritebatch, Offset + this.Location + new Vector2(-30f, 0.0f));
      TextFunctions.DrawJustifiedText("x" + (object) this.Value, 3f, Offset + this.Location + new Vector2(-20f, 0.0f), Color.White, 1f, AssetContainer.springFont, spritebatch);
    }
  }
}
