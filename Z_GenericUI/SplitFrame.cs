// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.SplitFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_GenericUI
{
  internal class SplitFrame
  {
    public GameObjectNineSlice FrameTop;
    public GameObjectNineSlice FrameBottom;
    public Vector2 VScaleTop;
    public Vector2 VScaleBottom;
    public Vector2 location;
    public Vector2 VSCale;
    private bool isActive;
    private CustomerFrame darkOverlay;

    public SplitFrame(
      Vector2 _VSCale,
      Vector3 topColour,
      Vector3 bottomColour,
      float BaseScale,
      float TopRatio = 0.3f)
    {
      this.SetUp(_VSCale, topColour, bottomColour, BaseScale, TopRatio);
    }

    public SplitFrame(Vector2 _VSCale, float BaseScale, float TopRatio = 0.3f) => this.SetUp(_VSCale, ColourData.Z_FrameLightBrown, ColourData.Z_FrameMidBrown, BaseScale, TopRatio);

    public void SetUp(
      Vector2 _VSCale,
      Vector3 topColour,
      Vector3 bottomColour,
      float BaseScale,
      float TopRatio)
    {
      this.FrameTop = new GameObjectNineSlice(new Rectangle(965, 471, 12, 12), 4);
      this.FrameBottom = new GameObjectNineSlice(new Rectangle(978, 471, 12, 12), 4);
      this.VSCale = _VSCale;
      this.VScaleTop = this.VSCale;
      this.VScaleTop.Y = this.VSCale.Y * TopRatio;
      this.VScaleBottom = this.VSCale;
      this.VScaleBottom.Y = this.VSCale.Y * (1f - TopRatio);
      this.FrameTop.SetAllColours(topColour);
      this.FrameBottom.SetAllColours(bottomColour);
      this.FrameTop.scale = BaseScale;
      this.FrameBottom.scale = BaseScale;
      this.FrameTop.vLocation.Y -= (float) ((double) this.VSCale.Y * (1.0 - (double) TopRatio) * 0.5);
      this.FrameBottom.vLocation.Y += (float) ((double) this.VSCale.Y * (double) TopRatio * 0.5);
      this.darkOverlay = new CustomerFrame(this.VSCale, Color.Black.ToVector3(), this.FrameTop.scale);
      this.darkOverlay.SetAlphaed(0.5f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return MathStuff.CheckPointCollision(true, offset, 1f, this.VScaleTop.X, this.VScaleTop.Y + this.VScaleBottom.Y, player.inputmap.PointerLocation);
    }

    public void SetActive(bool _isActive) => this.isActive = _isActive;

    public void SetAlpha(float alpha = 0.6f)
    {
      this.FrameTop.SetAlpha(alpha);
      this.FrameBottom.SetAlpha(alpha);
    }

    public void DrawSplitFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.FrameTop.DrawGameObjectNineSlice(spriteBatch, AssetContainer.SpriteSheet, offset, this.VScaleTop);
      this.FrameBottom.DrawGameObjectNineSlice(spriteBatch, AssetContainer.SpriteSheet, offset, this.VScaleBottom);
    }

    public void DrawDarkOverlay(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.isActive)
        return;
      this.darkOverlay.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}
