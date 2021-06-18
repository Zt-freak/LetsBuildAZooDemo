// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_TrashSystem.PieceOfTrash
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_TrashSystem
{
  internal class PieceOfTrash : GameObject
  {
    public PieceOfTrash()
    {
      switch (TinyZoo.Game1.Rnd.Next(0, 4))
      {
        case 0:
          this.DrawRect = new Rectangle(0, 487, 9, 9);
          break;
        case 1:
          this.DrawRect = new Rectangle(11, 481, 10, 8);
          break;
        case 2:
          this.DrawRect = new Rectangle(22, 481, 9, 8);
          break;
        default:
          this.DrawRect = new Rectangle(32, 481, 9, 9);
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public void UpdatePieceOfTrash()
    {
    }

    public void DrawPieceOfTrash() => this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
  }
}
