// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.Coin
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class Coin : GameObject
  {
    public Coin()
    {
      this.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.DrawRect = new Rectangle(138, 0, 7, 7);
      this.SetDrawOriginToCentre();
    }

    public void DrawCoin(SpriteBatch DrawWithThis, Vector2 Offset) => this.Draw(DrawWithThis, AssetContainer.SpriteSheet, Offset);
  }
}
