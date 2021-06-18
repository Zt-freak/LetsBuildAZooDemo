// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.BlackOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class BlackOut : GameObject
  {
    public Vector2 VScale;

    public BlackOut(float Height = 768f)
    {
      this.VScale = new Vector2(1024f, Height);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetAllColours(0.0f, 0.0f, 0.0f);
    }

    public void UpdateBlackOut()
    {
    }

    public void DrawBlackOut(Vector2 Offset, SpriteBatch spritebatch) => this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScale);

    public void DrawBlackOut(Vector2 Offset, SpriteBatch spritebatch, Texture2D texxxtyre) => this.Draw(spritebatch, texxxtyre, Offset, this.VScale);
  }
}
