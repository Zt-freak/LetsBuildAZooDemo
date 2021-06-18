// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.MiniHeading_V2
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class MiniHeading_V2 : GameObject
  {
    private string Text;
    private float BaseScale;
    private float Xbuffer;
    private float Ybuffer;

    public MiniHeading_V2(string WriteThis, float _BaseScale, float xbuffer = 10f, float ybuffer = 10f)
    {
      this.BaseScale = _BaseScale;
      this.scale = _BaseScale;
      this.Text = WriteThis;
      this.SetAllColours(ColourData.Z_Cream);
      this.Xbuffer = xbuffer;
      this.Ybuffer = ybuffer;
    }

    public void SetPostionFromVSCale(Vector2 VSCaleForFrame)
    {
      this.vLocation.X -= VSCaleForFrame.X * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.X;
      this.vLocation.X += this.Xbuffer * this.BaseScale;
      this.vLocation.Y -= VSCaleForFrame.Y * 0.5f;
      this.vLocation.Y += this.Ybuffer * this.BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public void DrawMiniHeading_V2(Vector2 PanelCenter, SpriteBatch spritebatch) => TextFunctions.DrawTextWithDropShadow(this.Text, this.scale, PanelCenter + this.vLocation, this.GetColour(), this.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch, false);

    public Vector2 GetSize() => (SpringFontUtil.MeasureString(this.Text, AssetContainer.SpringFontX1AndHalf) + new Vector2(this.Xbuffer, this.Ybuffer)) * this.BaseScale;

    public Vector2 GetSize_True() => new UIScaleHelper(this.BaseScale).ScaleVector2(AssetContainer.SpringFontX1AndHalf.MeasureString(this.Text) + new Vector2(this.Xbuffer, this.Ybuffer));
  }
}
