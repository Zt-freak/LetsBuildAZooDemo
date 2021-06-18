// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.GenericBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class GenericBox
  {
    private GameObject FrameBack;
    private GameObject FrameFrame;
    private GameObject MouseOver;
    private Vector2 FrameScale;
    private Vector2 FrameScaleMiddle;
    public Vector2 Location;

    public GenericBox(Vector2 size)
    {
      this.FrameBack = new GameObject();
      this.FrameBack.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FrameBack.SetDrawOriginToCentre();
      this.FrameBack.SetAllColours(0.0f, 0.0f, 0.2f);
      this.FrameScale = size;
      this.FrameScaleMiddle = this.FrameScale;
      this.FrameFrame = new GameObject(this.FrameBack);
      this.FrameFrame.SetAllColours(ColourData.Cyannz);
      this.FrameScaleMiddle.Y -= 4f;
      this.FrameScaleMiddle.X -= 4f;
      this.MouseOver = new GameObject(this.FrameBack);
      this.MouseOver.SetAllColours(1f, 1f, 1f);
      this.MouseOver.SetAlpha(0.3f);
    }

    public void SetYellowOrange()
    {
      this.FrameBack.SetAllColours(new Vector3(91f, 30f, 8f) / (float) byte.MaxValue);
      this.FrameFrame.SetAllColours(ColourData.FernLemon);
    }

    public void SetEdgeLemon() => this.FrameFrame.SetAllColours(ColourData.FernLemon);

    public void SetEdgeRed() => this.FrameFrame.SetAllColours(ColourData.FernRed);

    public void SetBlackWithShiteFrame()
    {
      this.FrameFrame.SetAllColours(1f, 1f, 1f);
      this.FrameBack.SetAllColours(0.0f, 0.0f, 0.0f);
    }

    public void SetDarkestBlue()
    {
      this.FrameFrame.SetAllColours(new Vector3(8f, 13f, 22f) / (float) byte.MaxValue);
      this.FrameBack.SetAllColours(new Vector3(8f, 13f, 22f) / (float) byte.MaxValue);
      this.MouseOver.SetAllColours(new Vector3(28f, 42f, 68f) / (float) byte.MaxValue);
      this.MouseOver.SetAlpha(1f);
    }

    public void SetPurpleWithYellowFrame()
    {
      this.FrameFrame.SetAllColours(ColourData.YellowHighlight);
      this.FrameBack.SetAllColours(ColourData.DeepPurple);
    }

    public bool CheckForTaps(Player player, Vector2 Offset) => (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);

    public bool CheckForMouseOver(Player player, Vector2 Offset) => (double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.player.touchinput.MultiTouchTouchLocations[0]) || MathStuff.CheckPointCollision(true, this.Location + Offset, 1f, this.FrameScale.X, this.FrameScale.Y, player.inputmap.PointerLocation);

    public void DrawGenericBox(Vector2 Offset) => this.DrawGenericBox(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawGenericBox(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.FrameFrame.Draw(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScale * Sengine.ScreenRatioUpwardsMultiplier);
      this.FrameBack.Draw(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScaleMiddle * Sengine.ScreenRatioUpwardsMultiplier);
    }

    public void DrawMouseOver(Vector2 Offset, SpriteBatch spritebatch) => this.MouseOver.Draw(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset, this.FrameScaleMiddle * Sengine.ScreenRatioUpwardsMultiplier);
  }
}
