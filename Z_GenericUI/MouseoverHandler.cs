// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.MouseoverHandler
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_GenericUI
{
  internal class MouseoverHandler
  {
    private static Rectangle whitenineslice = new Rectangle(948, 484, 21, 21);
    private static Rectangle rect = TinyZoo.Game1.WhitePixelRect;
    private GameObjectNineSlice highlight;
    private Vector2 dimensions;
    private float alpha;
    private bool clicked;

    public bool mouseover { get; private set; }

    public bool Clicked => this.clicked;

    public MouseoverHandler(Vector2 dimensions_, float basescale_, float alpha_ = 0.3f) => this.Init(dimensions_, basescale_, alpha_);

    public MouseoverHandler(float width, float height, float basescale_, float alpha_ = 0.3f) => this.Init(new Vector2(width, height), basescale_, alpha_);

    private void Init(Vector2 dimensions_, float basescale_, float alpha_ = 0.3f)
    {
      this.dimensions = dimensions_;
      this.alpha = alpha_;
      this.highlight = new GameObjectNineSlice(MouseoverHandler.whitenineslice, 7);
      this.highlight.scale = basescale_;
      this.highlight.fAlpha = this.alpha;
      this.highlight.SetDrawOriginToCentre();
    }

    public bool UpdateMouseoverHandler(Player player, Vector2 offset, float DeltaTime)
    {
      this.clicked = false;
      this.mouseover = MathStuff.CheckPointCollision(true, offset, 1f, this.dimensions.X, this.dimensions.Y, player.inputmap.PointerLocation);
      if (this.mouseover)
        this.clicked = (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0;
      return this.mouseover;
    }

    public void DrawMouseOverHandler(SpriteBatch spritebatch, Vector2 offset)
    {
      if (!this.mouseover)
        return;
      this.highlight.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, offset, this.dimensions);
    }
  }
}
