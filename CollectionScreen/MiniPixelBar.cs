// Decompiled with JetBrains decompiler
// Type: TinyZoo.CollectionScreen.MiniPixelBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.CollectionScreen
{
  internal class MiniPixelBar
  {
    public Vector2 location;
    private GameObject baseBar;
    private GameObject fillBar;
    private Vector2 fullBarSize;
    private Vector2 filledBarSize;

    public MiniPixelBar(float BaseScale, float barWidth, float barHeight)
    {
      this.baseBar = new GameObject();
      this.baseBar.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.baseBar.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.baseBar.SetAllColours(ColourData.Z_BarDarkBrown);
      this.fillBar = new GameObject(this.baseBar);
      this.fillBar.SetAllColours(Color.White.ToVector3());
      this.fullBarSize = new Vector2(barWidth, barHeight);
      this.filledBarSize = new Vector2(0.0f, barHeight);
    }

    public void SetBarValue(float progress) => this.filledBarSize.X = this.fullBarSize.X * progress;

    public void SetEmpyBarColor(Vector3 color) => this.baseBar.SetAllColours(color);

    public void SetFillColor(Vector3 color) => this.fillBar.SetAllColours(color);

    public Vector2 GetSize() => this.fullBarSize;

    public void UpdateMiniPixelBar()
    {
    }

    public void DrawMiniPixelBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.baseBar.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.fullBarSize);
      this.fillBar.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.filledBarSize);
    }
  }
}
