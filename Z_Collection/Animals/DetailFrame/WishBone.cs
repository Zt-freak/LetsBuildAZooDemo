// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Animals.DetailFrame.WishBone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Collection.Animals.DetailFrame
{
  internal class WishBone : GameObject
  {
    public WishBone(float BaseScale)
    {
      this.scale = BaseScale;
      this.DrawRect = new Rectangle(858, 920, 155, 38);
      this.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
    }

    public void DrawWishBone(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
