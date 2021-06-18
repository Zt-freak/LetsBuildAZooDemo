// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.RData.RStar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Research_.RData
{
  internal class RStar : GameObject
  {
    private StarColour starColour;

    public RStar(StarColour _starColour, float BaseScale, bool IsBig = false)
    {
      this.starColour = _starColour;
      switch (this.starColour)
      {
        case StarColour.Good_Yellow:
          if (IsBig)
          {
            this.DrawRect = new Rectangle(456, 920, 28, 28);
            break;
          }
          this.DrawRect = new Rectangle(558, 288, 14, 14);
          break;
        case StarColour.Evil_Purple:
          if (IsBig)
          {
            this.DrawRect = new Rectangle(427, 920, 28, 28);
            break;
          }
          this.DrawRect = new Rectangle(543, 288, 14, 14);
          break;
        case StarColour.Neutral:
          if (IsBig)
          {
            this.DrawRect = new Rectangle(485, 920, 28, 28);
            break;
          }
          this.DrawRect = new Rectangle(466, 397, 14, 14);
          break;
      }
      this.scale = BaseScale;
      this.SetDrawOriginToCentre();
    }

    public void DrawRStar(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet);

    public void DrawRStar_WorldOffsetDraw(SpriteBatch spriteBatch) => this.WorldOffsetDraw(spriteBatch, AssetContainer.SpriteSheet);
  }
}
