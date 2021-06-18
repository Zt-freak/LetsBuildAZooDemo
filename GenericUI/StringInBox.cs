// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.StringInBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using TinyZoo.PlayerDir;

namespace TinyZoo.GenericUI
{
  internal class StringInBox : GameObject
  {
    public Vector2 VScale;
    private float BaseScale;
    private string TextToDraw;
    private bool DrawFromCenter;
    public Color TextColour;
    public GameObject TextThing;
    public GameObjectNineSlice Frame;
    private bool UseRoundaboutFont;
    private bool UseOnePointFiveFont;
    private SpringFont usethis;
    private Texture2D spritesheet;
    private bool LockedPixelScale;
    private bool DoNotRescaleRoundabout;
    private bool IsAllCapsRoundAbout;
    public LerpHandler_Float ActivationLerper;
    private bool IsBigFont;
    private static Vector2 Multiplier;

    public StringInBox(
      string Text,
      float _BaseScale,
      float Length,
      bool _DrawFromCenter = false,
      bool _UseRoundaboutFont = false)
    {
      this.ActivationLerper = new LerpHandler_Float();
      this.ActivationLerper.SetLerp(true, 1f, 1f, 3f);
      this.UseRoundaboutFont = _UseRoundaboutFont;
      this.usethis = AssetContainer.springFont;
      if (this.UseRoundaboutFont)
        this.usethis = AssetContainer.roundaboutFont;
      this.spritesheet = AssetContainer.SpriteSheet;
      this.TextThing = new GameObject();
      this.TextThing.SetAllColours(Color.White.ToVector3());
      this.DrawFromCenter = _DrawFromCenter;
      this.TextToDraw = Text;
      this.BaseScale = _BaseScale;
      this.VScale = new Vector2(Length, 10f);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      if (!this.DrawFromCenter)
        return;
      this.SetDrawOriginToCentre();
    }

    public StringInBox(
      float _BaseScale,
      string Text,
      float Length,
      bool _DrawFromCenter = false,
      bool _UseRoundaboutFont = false,
      bool _IsAllCapsRoundAbut = false,
      bool _UseOnePointFiveFont = false)
    {
      this.ActivationLerper = new LerpHandler_Float();
      this.ActivationLerper.SetLerp(true, 1f, 1f, 3f);
      this.IsAllCapsRoundAbout = _IsAllCapsRoundAbut;
      this.LockedPixelScale = true;
      this.UseRoundaboutFont = _UseRoundaboutFont;
      this.UseOnePointFiveFont = _UseOnePointFiveFont;
      this.usethis = AssetContainer.springFont;
      if (this.UseRoundaboutFont)
      {
        this.DoNotRescaleRoundabout = true;
        this.usethis = AssetContainer.roundaboutFont;
      }
      if (this.UseOnePointFiveFont)
      {
        this.DoNotRescaleRoundabout = true;
        this.usethis = AssetContainer.SpringFontX1AndHalf;
      }
      this.spritesheet = AssetContainer.SpriteSheet;
      this.TextThing = new GameObject();
      this.TextThing.SetAllColours(Color.White.ToVector3());
      this.DrawFromCenter = _DrawFromCenter;
      this.TextToDraw = Text;
      this.BaseScale = _BaseScale;
      this.VScale = new Vector2(Length, 10f * this.BaseScale);
      if (this.UseRoundaboutFont)
        this.VScale = !this.IsAllCapsRoundAbout ? new Vector2(Length, 40f * this.BaseScale) : new Vector2(Length, 50f * this.BaseScale);
      else if (this.UseOnePointFiveFont)
        this.VScale = new Vector2(Length, 23f * this.BaseScale);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      if (!this.DrawFromCenter)
        return;
      this.SetDrawOriginToCentre();
    }

    internal static Rectangle GetFrameColourRect(
      BTNColour btnclr,
      out Vector3 SecondaryColour)
    {
      SecondaryColour = Vector3.One;
      Rectangle rectangle = new Rectangle(921, 350, 21, 21);
      switch (btnclr)
      {
        case BTNColour.Cream:
          rectangle = new Rectangle(877, 350, 21, 21);
          SecondaryColour = new Vector3(117f, 83f, 62f) / (float) byte.MaxValue;
          break;
        case BTNColour.Pink:
          rectangle = new Rectangle(899, 350, 21, 21);
          break;
        case BTNColour.Green:
          rectangle = new Rectangle(873, 394, 21, 21);
          break;
        case BTNColour.PaleYellow:
          rectangle = new Rectangle(873, 372, 21, 21);
          SecondaryColour = new Vector3(0.4666667f, 0.3333333f, 0.2235294f);
          break;
        case BTNColour.White:
          rectangle = new Rectangle(948, 484, 21, 21);
          break;
        case BTNColour.Grey:
          rectangle = new Rectangle(948, 506, 21, 21);
          break;
        case BTNColour.Brown:
          SecondaryColour = new Vector3(1f, 0.9882353f, 0.9372549f);
          rectangle = new Rectangle(939, 416, 21, 21);
          break;
        case BTNColour.Red:
          rectangle = new Rectangle(907, 560, 21, 21);
          break;
        case BTNColour.EvilPurple:
          rectangle = new Rectangle(909, 678, 21, 21);
          SecondaryColour = ColourData.Z_Cream;
          break;
        case BTNColour.EvilPurpleLocked:
          rectangle = new Rectangle(884, 462, 21, 21);
          SecondaryColour = ColourData.Z_Cream;
          break;
        case BTNColour.GoodYellow:
          rectangle = new Rectangle(909, 700, 21, 21);
          SecondaryColour = ColourData.Z_Cream;
          break;
        case BTNColour.GoodYellowLocked:
          rectangle = new Rectangle(906, 462, 21, 21);
          SecondaryColour = ColourData.Z_Cream;
          break;
        case BTNColour.CriticalNeutralBlue:
          rectangle = new Rectangle(493, 496, 21, 21);
          break;
        case BTNColour.CriticalEvilPurple:
          rectangle = new Rectangle(537, 496, 21, 21);
          break;
        case BTNColour.CriticalGoodYellow:
          rectangle = new Rectangle(515, 496, 21, 21);
          break;
        case BTNColour.CriticalNeutralBlue_MO:
          rectangle = new Rectangle(493, 518, 21, 21);
          break;
        case BTNColour.CriticalEvilPurple_MO:
          rectangle = new Rectangle(537, 518, 21, 21);
          break;
        case BTNColour.CriticalGoodYellow_MO:
          rectangle = new Rectangle(515, 518, 21, 21);
          break;
        case BTNColour.Task_Gold:
          rectangle = new Rectangle(212, 911, 21, 21);
          SecondaryColour = ColourData.Z_BrownTextOnGoldButton;
          break;
      }
      return rectangle;
    }

    internal static Texture2D GetSpriteSheet(BTNColour btnclr)
    {
      Texture2D texture2D;
      switch (btnclr)
      {
        case BTNColour.CriticalNeutralBlue:
        case BTNColour.Task_Gold:
          texture2D = AssetContainer.UISheet;
          break;
        case BTNColour.CriticalEvilPurple:
          texture2D = AssetContainer.UISheet;
          break;
        case BTNColour.CriticalGoodYellow:
          texture2D = AssetContainer.UISheet;
          break;
        case BTNColour.CriticalNeutralBlue_MO:
          texture2D = AssetContainer.UISheet;
          break;
        case BTNColour.CriticalEvilPurple_MO:
          texture2D = AssetContainer.UISheet;
          break;
        case BTNColour.CriticalGoodYellow_MO:
          texture2D = AssetContainer.UISheet;
          break;
        default:
          texture2D = AssetContainer.SpriteSheet;
          break;
      }
      return texture2D;
    }

    public void SetActivateAnimation()
    {
      this.ActivationLerper = new LerpHandler_Float();
      this.ActivationLerper.SetLerp(true, 0.0f, 1f, 3f, true);
    }

    public void SetAsButtonFrame(BTNColour btnclr, float BaseScale = -1f)
    {
      Vector3 SecondaryColour;
      Rectangle frameColourRect = StringInBox.GetFrameColourRect(btnclr, out SecondaryColour);
      this.spritesheet = StringInBox.GetSpriteSheet(btnclr);
      this.TextThing.SetAllColours(SecondaryColour);
      this.Frame = new GameObjectNineSlice(frameColourRect, 7);
      if ((double) BaseScale < 0.0)
      {
        this.Frame.scale = 2f;
        if (DebugFlags.IsPCVersion)
          this.Frame.scale = RenderMath.GetPixelSizeBestMatch(1.5f);
      }
      else
        this.Frame.scale = BaseScale;
      if (!this.IsAllCapsRoundAbout)
        this.VScale.Y = 12f;
      this.Frame.SetAllColours(1f, 1f, 1f);
    }

    public void SetBigFont() => this.IsBigFont = true;

    public void SetYellow() => this.SetAllColours(ColourData.FernLemon);

    public void SetLemonANdBlue()
    {
      this.TextThing.SetAllColours(ColourData.FernLemon);
      this.SetAllColours(ColourData.FernDarkBlue);
    }

    public void checklength()
    {
      float x = SpringFontUtil.MeasureString(this.TextToDraw, AssetContainer.springFont).X;
      if ((double) this.VScale.X >= (double) x + 15.0)
        return;
      this.VScale.X = x + 15f;
    }

    public string GetText() => this.TextToDraw;

    public void SetText(string NewText) => this.TextToDraw = NewText;

    public void SetGreen(bool SetTextToCream = true)
    {
      this.SetAllColours(ColourData.Z_ButtonDarkGreen);
      this.TextThing.SetAllColours(ColourData.Z_Cream);
    }

    public void SetRed()
    {
      this.SetAllColours(0.5f, 0.0f, 0.0f);
      this.TextThing.SetAllColours(ColourData.Z_Cream);
    }

    public void SetButtonCyan()
    {
      this.SetAllColours(0.0f, 0.0f, 0.0f);
      this.TextThing.SetAllColours(ColourData.Cyannz);
    }

    public void SetButtonTan()
    {
      this.SetAllColours(ColourData.TanBase);
      this.TextThing.SetAllColours(ColourData.TanGoldText);
    }

    public void SetButtonGreen()
    {
      this.SetAllColours(ColourData.FernGreen);
      this.TextThing.SetAllColours(ColourData.FernVeryDarkBlue);
    }

    public void SetButtonRed() => this.SetAllColours(ColourData.FernRed);

    public void SetButtonPurple()
    {
      this.TextThing.SetAllColours(Color.White.ToVector3());
      this.SetAllColours(new Vector3(172f, 53f, 239f) / (float) byte.MaxValue);
    }

    public void SetWhite()
    {
      this.SetAllColours(1f, 1f, 1f);
      this.TextThing.SetAllColours(Color.Black.ToVector3());
    }

    public void SetButtonBlack()
    {
      this.SetAllColours(1f, 1f, 1f);
      this.TextThing.SetAllColours(Color.Black.ToVector3());
    }

    public void SetButtonYellow() => this.SetAllColours(1f, 0.7529412f, 0.0f);

    public Vector2 GetVScale_Depricated() => this.LockedPixelScale ? RenderMath.GetPixelSizeBestMatch(this.BaseScale) * this.VScale : this.BaseScale * this.VScale;

    public void UpdateStringInBox(float DeltaTime)
    {
      this.ActivationLerper.UpdateLerpHandler(DeltaTime);
      this.UpdateColours(DeltaTime);
    }

    public void DrawStringInABoxWithoutBox(Vector2 Offset, float ALphaMultiplier = 1f, float ScaleMult = 1f)
    {
      if (this.DrawFromCenter)
        TextFunctions.DrawJustifiedText(this.TextToDraw, ScaleMult * this.BaseScale, this.vLocation + Offset + new Vector2(0.0f, 1f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.BaseScale, this.TextColour, 1f * ALphaMultiplier, this.usethis, AssetContainer.pointspritebatch03);
      else
        TextFunctions.DrawTextWithDropShadow(this.TextToDraw, ScaleMult * this.BaseScale, this.vLocation + Offset + new Vector2(2f, -3f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.BaseScale, this.TextColour, 1f * ALphaMultiplier, this.usethis, AssetContainer.pointspritebatch03, false);
    }

    public Vector2 GetFrameScaleForMouseOver() => this.BaseScale * this.VScale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawStringInBox(
      Vector2 Offset,
      SpriteBatch DrawWithThis,
      float ALphaMultiplier = 1f,
      float ScaleMult = 1f,
      bool AddForController = false)
    {
      float num1 = ScaleMult * this.BaseScale;
      if (this.spritesheet == null)
        this.spritesheet = AssetContainer.SpriteSheet;
      if (this.Frame != null)
      {
        StringInBox.Multiplier = new Vector2(this.ActivationLerper.Value, 1f);
        this.Frame.fAlpha = this.fAlpha * ALphaMultiplier;
        if (this.ActivationLerper != null && (double) this.ActivationLerper.Value == 0.0)
          return;
        if (this.LockedPixelScale && (this.UseRoundaboutFont || this.UseOnePointFiveFont))
        {
          if (this.IsAllCapsRoundAbout)
          {
            Vector2 vector2 = Vector2.Zero;
            if (this.UseRoundaboutFont)
              vector2 = new Vector2(0.0f, -6f * this.BaseScale);
            this.Frame.DrawGameObjectNineSlice(DrawWithThis, this.spritesheet, Offset + this.vLocation + vector2, this.VScale * Sengine.ScreenRatioUpwardsMultiplier * StringInBox.Multiplier);
          }
          else
            this.Frame.DrawGameObjectNineSlice(DrawWithThis, this.spritesheet, Offset + this.vLocation + new Vector2(0.0f, -4f * this.BaseScale), num1 * this.VScale * Sengine.ScreenRatioUpwardsMultiplier * StringInBox.Multiplier);
        }
        else
          this.Frame.DrawGameObjectNineSlice(DrawWithThis, this.spritesheet, Offset + this.vLocation, num1 * this.VScale * Sengine.ScreenRatioUpwardsMultiplier * StringInBox.Multiplier);
      }
      else
        this.Draw(DrawWithThis, this.spritesheet, Offset, num1 * this.VScale * Sengine.ScreenRatioUpwardsMultiplier, this.fAlpha * ALphaMultiplier);
      float num2 = 1f;
      if (PlayerStats.language != Language.English)
      {
        float num3 = SpringFontUtil.MeasureString(this.TextToDraw, this.usethis).X * num1;
        if ((double) num3 > (double) num1 * ((double) this.VScale.X - 4.0))
          num2 = num1 * (this.VScale.X - 4f) / num3;
      }
      string str = "";
      if (GameFlags.IsUsingController & AddForController)
        str = "  ";
      if ((double) this.ActivationLerper.Value < 1.0)
      {
        if ((double) this.ActivationLerper.Value > 0.75)
          ALphaMultiplier *= (float) (((double) this.ActivationLerper.Value - 0.75) * 4.0);
        else
          ALphaMultiplier = 0.0f;
      }
      if (this.UseRoundaboutFont && !this.LockedPixelScale)
        num1 *= 0.23f;
      if (this.DrawFromCenter)
        TextFunctions.DrawJustifiedText(str + this.TextToDraw, num1 * num2, this.vLocation + Offset + new Vector2(0.0f, 1f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.BaseScale, this.TextThing.GetColour(), 1f * ALphaMultiplier, this.usethis, DrawWithThis);
      else
        TextFunctions.DrawTextWithDropShadow(str + this.TextToDraw, num1 * num2, this.vLocation + Offset + new Vector2(2f, -3f * Sengine.ScreenRatioUpwardsMultiplier.Y) * this.BaseScale, this.TextThing.GetColour(), 1f * ALphaMultiplier, this.usethis, DrawWithThis, false);
    }

    public void DrawStringInBox(
      Vector2 Offset,
      float ALphaMultiplier = 1f,
      float ScaleMult = 1f,
      bool AddForController = false)
    {
      this.DrawStringInBox(Offset, AssetContainer.pointspritebatch03, ALphaMultiplier, ScaleMult, AddForController);
    }
  }
}
