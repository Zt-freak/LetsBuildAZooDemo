// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.SimpleTextBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class SimpleTextBox
  {
    public SimpleTextHandler text;
    public GameObjectNineSlice FrameForWHoleThing;
    public LerpHandler_Float lerpontoscreen;
    public Vector2 Location;
    private float Width;
    private float OriginalPerc;
    public GameObjectNineSlice MouseverFrane;
    private bool MouseOver;
    private float LINEHEIGHT;

    public SimpleTextBox(string TEXTTT, float _Width = 800f, bool WillLrp = true, float textScale = 3f)
    {
      this.LINEHEIGHT = 8.333324f * textScale;
      this.Width = _Width;
      this.FrameForWHoleThing = new GameObjectNineSlice(new Rectangle(302, 128, 21, 21), 7);
      this.FrameForWHoleThing.scale = 2f;
      Vector3 SecondaryColour;
      StringInBox.GetFrameColourRect(BTNColour.White, out SecondaryColour);
      this.MouseverFrane = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out SecondaryColour), 7);
      this.MouseverFrane.SetAlpha(0.3f);
      this.OriginalPerc = (float) ((double) this.Width / 1024.0 - 0.0500000007450581);
      this.text = new SimpleTextHandler(TEXTTT, true, this.OriginalPerc, RenderMath.GetPixelSizeBestMatch(textScale));
      this.lerpontoscreen = new LerpHandler_Float();
      if (WillLrp)
        this.lerpontoscreen.SetLerp(true, 1f, 0.0f, 3f);
      this.text.AutoCompleteParagraph();
    }

    public void SetTextBoxToALternateColour(BTNColour btncolour)
    {
      Vector3 SecondaryColour;
      this.FrameForWHoleThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(btncolour, out SecondaryColour), 7);
      this.FrameForWHoleThing.scale = 2f;
      this.text.paragraph.linemaker.SetAllColours(SecondaryColour);
    }

    public void ReplaceText(string TEXTTT)
    {
      this.text = new SimpleTextHandler(TEXTTT, true, this.OriginalPerc);
      this.text.AutoCompleteParagraph();
    }

    public bool LerpOffComplete() => (double) this.lerpontoscreen.Value == -1.0;

    public void LerpOff()
    {
      if ((double) this.lerpontoscreen.TargetValue == -1.0)
        return;
      this.lerpontoscreen.SetLerp(false, 0.0f, -1f, 3f, true);
    }

    public bool LerpOnComplete() => (double) this.lerpontoscreen.Value == 0.0;

    public bool UpdateSimpleTextBox(float DeltaTime, Player player)
    {
      this.lerpontoscreen.UpdateLerpHandler(DeltaTime);
      return (double) this.lerpontoscreen.Value == -1.0;
    }

    public void DrawSimpleTextBox(Vector2 Offset)
    {
      Offset.X += this.lerpontoscreen.Value * 1024f;
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, this.Location + Offset, this.GetScale());
      this.text.DrawSimpleTextHandler(Offset + this.Location + new Vector2(0.0f, (float) (-((double) this.LINEHEIGHT + 2.0) * (((double) this.text.paragraph.Numberoflines - 1.0) * 0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y))), 1f, AssetContainer.pointspritebatchTop05);
    }

    public void DrawSimpleTextBox(Vector2 Offset, float YPad)
    {
      Offset.X += this.lerpontoscreen.Value * 1024f;
      this.FrameForWHoleThing.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), new Vector2(this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y));
      this.text.DrawSimpleTextHandler(Offset + this.Location + new Vector2(0.0f, (float) (-27.0 * (((double) this.text.paragraph.Numberoflines - 1.0) * 0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y))), 1f, AssetContainer.pointspritebatch03);
      if (this.MouseOver)
      {
        this.MouseverFrane.scale = this.FrameForWHoleThing.scale;
        this.MouseverFrane.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.FrameForWHoleThing.vLocation + this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), new Vector2(this.Width, YPad + (float) (25 * this.text.paragraph.Numberoflines) * Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
      this.MouseOver = false;
    }

    public bool CheckForTaps(Vector2 Offset, Player player, bool UseMouseOver, float YPad)
    {
      if (UseMouseOver)
      {
        this.MouseOver = MathStuff.CheckPointCollision(true, this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), 1f, this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]);
        if (!this.MouseOver)
          this.MouseOver = MathStuff.CheckPointCollision(true, this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), 1f, this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      }
      return MathStuff.CheckPointCollision(true, this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), 1f, this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]);
    }

    public Vector2 GetScale(float YPad) => new Vector2(this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y);

    public Vector2 GetScale() => new Vector2(this.Width, (float) (50.0 + (double) this.LINEHEIGHT * (double) this.text.paragraph.Numberoflines * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));

    public void DrawSimpleTextBox(Vector2 Offset, float YPad, SpriteBatch spritebatch)
    {
      Offset.X += this.lerpontoscreen.Value * 1024f;
      this.FrameForWHoleThing.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), this.GetScale(YPad));
      this.text.DrawSimpleTextHandler(Offset + this.Location + new Vector2(0.0f, (float) (-27.0 * (((double) this.text.paragraph.Numberoflines - 1.0) * 0.5 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y))), 1f, spritebatch);
      if (this.MouseOver)
      {
        this.MouseverFrane.scale = this.FrameForWHoleThing.scale;
        this.MouseverFrane.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.FrameForWHoleThing.vLocation + this.Location + Offset + new Vector2(0.0f, YPad * -0.5f), new Vector2(this.Width, YPad + this.LINEHEIGHT * (float) this.text.paragraph.Numberoflines * Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
      this.MouseOver = false;
    }
  }
}
