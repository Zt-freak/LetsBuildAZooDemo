// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Avatar.CirclePiece
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Avatar
{
  internal class CirclePiece : GameObject
  {
    public CirclePiece()
    {
      this.DrawRect = new Rectangle(53, 175, 64, 11);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.DrawOrigin.Y += 256f;
    }

    public void UpdateCirclePiece()
    {
    }

    public void DrawCirclePiece(SpriteBatch spritebatch, Vector2 Offset) => this.Draw(spritebatch, AssetContainer.EnvironmentSheet2, Offset);
  }
}
