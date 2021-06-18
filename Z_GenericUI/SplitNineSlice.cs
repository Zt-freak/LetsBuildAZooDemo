// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.SplitNineSlice
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class SplitNineSlice
  {
    public GameObjectNineSlice FrameTop;
    public GameObjectNineSlice FrameBottom;
    public Vector2 VScaleTop;
    public Vector2 VScaleBottom;
    public Vector2 Location;
    private Vector2 FullSize;

    public SplitNineSlice(Vector2 FullVScale, float TopSize, bool IsDark = false, bool IsBlock = false)
    {
      this.FullSize = FullVScale;
      this.VScaleTop = FullVScale;
      this.VScaleTop.Y = TopSize;
      this.VScaleBottom = FullVScale;
      this.VScaleBottom.Y = FullVScale.Y - TopSize;
      if (IsBlock)
      {
        this.FrameTop = new GameObjectNineSlice(new Rectangle(952, 471, 12, 12), 4);
        this.FrameBottom = new GameObjectNineSlice(new Rectangle(952, 471, 12, 12), 4);
      }
      else
      {
        this.FrameTop = new GameObjectNineSlice(new Rectangle(965, 471, 12, 12), 4);
        this.FrameBottom = new GameObjectNineSlice(new Rectangle(978, 471, 12, 12), 4);
      }
      this.FrameTop.vLocation.Y = FullVScale.Y * -0.5f;
      this.FrameTop.vLocation.Y += TopSize * 0.5f;
      this.FrameBottom.vLocation.Y = FullVScale.Y * 0.5f;
      this.FrameBottom.vLocation.Y -= this.VScaleBottom.Y * 0.5f;
      if (IsDark)
      {
        this.FrameBottom.SetAllColours(0.4156863f, 0.3019608f, 0.1882353f);
        this.FrameTop.SetAllColours(0.5176471f, 0.3764706f, 0.2509804f);
      }
      else
      {
        this.FrameBottom.SetAllColours(0.5176471f, 0.3764706f, 0.2509804f);
        this.FrameTop.SetAllColours(0.6117647f, 0.4745098f, 0.3254902f);
      }
    }

    public Vector2 GetSize() => this.FullSize;

    public void DrawSplitNineSlice(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.FrameTop.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScaleTop);
      this.FrameBottom.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VScaleBottom);
    }
  }
}
