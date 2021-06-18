// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BreedScreen.SelectBreedingPair.MiniChromosone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_BreedScreen.SelectBreedingPair
{
  internal class MiniChromosone : GameObject
  {
    public MiniChromosone(bool isFemale, float BaseScale)
    {
      if (isFemale)
        this.DrawRect = new Rectangle(46, 16, 5, 7);
      else
        this.DrawRect = new Rectangle(52, 16, 7, 7);
      this.SetDrawOriginToCentre();
      this.scale = BaseScale * 2f;
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void UpdateMiniChromosone()
    {
    }

    public void DrawMiniChromosone(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
