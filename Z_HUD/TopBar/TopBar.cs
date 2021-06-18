// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.TopBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_HUD.TopBar
{
  internal class TopBar : GameObject
  {
    public Vector2 VSCale;

    public TopBar(float BarHeight, float BaseScale)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.VSCale = new Vector2(1024f, BarHeight * Sengine.ScreenRatioUpwardsMultiplier.Y * BaseScale);
      this.SetAllColours(ColourData.Z_FrameLightBrown);
    }

    public void UpdateTopBar(float DeltaTime)
    {
    }

    public void DrawTopBar(SpriteBatch spriteBatch, Vector2 Offset) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset, this.VSCale);
  }
}
