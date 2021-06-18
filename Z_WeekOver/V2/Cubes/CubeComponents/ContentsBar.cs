// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.ContentsBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class ContentsBar : GameObject
  {
    public Vector2 VScale;
    private SpringFont fontpointer;
    private string TEXT;
    private bool ShowOutline;
    private float YSCALE;
    private ZGenericText name;
    private ZGenericText Value;
    private float LerpScale = 1f;

    public ContentsBar(
      float BaseScale,
      string Text,
      Vector3 Colour,
      bool MoveDownForLine = false,
      bool _ShowOutline = false)
    {
      this.ShowOutline = _ShowOutline;
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.VScale = new Vector2(44f, 110f) * BaseScale;
      this.YSCALE = this.VScale.Y;
      this.name = new ZGenericText(BaseScale);
      this.name.textToWrite = Text;
      if (MoveDownForLine)
        this.name.vLocation.Y = BaseScale * 25f;
      else
        this.name.vLocation.Y = BaseScale * 10f;
      this.Value = new ZGenericText(BaseScale);
      this.Value.textToWrite = " ";
      this.Value.vLocation.Y = BaseScale * -10f;
      this.name.SetAllColours(Colour);
      this.SetAllColours(Colour);
      this.Value.SetAllColours(Colour);
    }

    public void SetBarValueText(string TT) => this.Value.textToWrite = TT;

    public void SetFinalFullness(float Fullness) => this.YSCALE = Fullness;

    public void SetLerpScale(float _LerpScale) => this.LerpScale = _LerpScale;

    public void SetUllness()
    {
    }

    public void UpdateContentsBar()
    {
    }

    public void DrawContentsBar(SpriteBatch spritebatch, Vector2 Offset, float AlphaMult)
    {
      if (this.ShowOutline)
        this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset + this.vLocation, this.VScale, 0.0f, this.DrawRect, (float) ((double) AlphaMult * (double) this.fAlpha * 0.200000002980232), Color.Black);
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale * new Vector2(1f, this.YSCALE * this.LerpScale), AlphaMult * this.fAlpha);
      this.name.DrawZGenericText(Offset + this.vLocation, spritebatch, AlphaMult);
      this.Value.DrawZGenericText(Offset + this.vLocation + new Vector2(0.0f, (float) ((double) this.YSCALE * (double) this.LerpScale * -(double) this.VScale.Y)), spritebatch, AlphaMult);
    }
  }
}
