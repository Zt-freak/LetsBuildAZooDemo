// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.CharacterTextBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI.Characters;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.GenericUI
{
  internal class CharacterTextBox
  {
    private GenericBox boc;
    private SimpleTextHandler text;
    public Vector2 Location;
    private TalkingHead talkinghead;
    public GameObjectNineSlice Frame;
    private Vector2 CHTextLocation = new Vector2(-330f, -40f);
    private Vector2 HEadLoc;
    private Vector2 TextLocation;
    private Vector2 Extra;
    public Vector2 VSCale;
    private float BaseScale = -1f;

    public CharacterTextBox(
      string TextToSay,
      AnimalType character,
      float _BaseScale,
      bool ShortenForCloseButton = false,
      float OverrideWidth_Scaled = -1f,
      float OverrideHeight_Scaled = -1f,
      bool AutoCompleteParagraph = true)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float x1 = defaultXbuffer;
      float y1 = defaultYbuffer;
      this.Frame = new GameObjectNineSlice(new Rectangle(302, 128, 21, 21), 7);
      this.Frame.scale = this.BaseScale;
      float x2 = 950f;
      if ((double) OverrideWidth_Scaled != -1.0)
        x2 = OverrideWidth_Scaled;
      this.talkinghead = new TalkingHead(character, 0, BaseScale: this.BaseScale);
      this.talkinghead.vLocation = new Vector2(x1, y1);
      Vector2 size = this.talkinghead.GetSize();
      TalkingHead talkinghead1 = this.talkinghead;
      talkinghead1.vLocation = talkinghead1.vLocation + size * 0.5f;
      float x3 = x1 + (size.X + defaultXbuffer);
      this.text = new SimpleTextHandler(TextToSay, false, (float) (((double) x2 - (double) x3) / 1024.0), this.BaseScale, false, AutoCompleteParagraph);
      this.text.SetAllColours(ColourData.Z_Cream);
      this.text.Location = new Vector2(x3, y1);
      float y2 = y1 + Math.Max(size.Y, this.text.GetHeightOfParagraph()) + defaultYbuffer;
      if ((double) OverrideHeight_Scaled != -1.0)
        y2 = OverrideHeight_Scaled;
      this.VSCale = new Vector2(x2, y2);
      Vector2 vector2 = -this.VSCale * 0.5f;
      TalkingHead talkinghead2 = this.talkinghead;
      talkinghead2.vLocation = talkinghead2.vLocation + vector2;
      this.text.Location += vector2;
    }

    public Vector2 GetSize() => this.VSCale;

    public CharacterTextBox(
      AnimalType character,
      string TextToSay,
      float _ScaleMult = 1f,
      float Height = 115f,
      bool ShortenForCloseButton = false)
    {
      float num1 = _ScaleMult;
      if (DebugFlags.IsPCVersion)
        _ScaleMult *= Sengine.ScreenRationReductionMultiplier.Y;
      this.Frame = new GameObjectNineSlice(new Rectangle(302, 128, 21, 21), 7);
      this.Frame.scale = 2f;
      float num2 = 1f;
      float num3 = 0.0f;
      if (ShortenForCloseButton)
      {
        num3 = 100f;
        this.VSCale = new Vector2(950f - num3, Height * num2);
      }
      else
        this.VSCale = new Vector2(950f, Height * num2);
      this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
      this.boc = new GenericBox(new Vector2(950f, Height * num2) * _ScaleMult);
      this.Extra.Y = (float) (((double) Height - 115.0) * (double) num2 * (double) _ScaleMult * -1.0);
      this.Extra.Y *= 0.5f;
      this.boc.SetBlackWithShiteFrame();
      float num4 = 2f;
      float num5 = 0.7f;
      this.CHTextLocation = new Vector2(-330f, -40f);
      if (GameFlags.MobileUIScale)
      {
        num4 = 2.5f;
        num5 = 0.75f;
        this.CHTextLocation = new Vector2(-350f, -40f);
      }
      if ((double) num3 > 0.0)
        num5 -= num3 / 1024f;
      float num6 = num4 * (num2 * _ScaleMult);
      if (DebugFlags.IsPCVersion)
        num6 = GameFlags.GetSmallTextScale();
      float _Scale = Z_GameFlags.GetBaseScaleForUI() * 2f;
      this.text = new SimpleTextHandler(TextToSay, 1024f * num5 * num1, _Scale: _Scale, ForceThisFont: AssetContainer.SinglePixelFontX1AndHalf);
      this.talkinghead = new TalkingHead(character, 0, _ScaleMult);
      this.HEadLoc = new Vector2((float) -(420.0 - (double) num3 * 0.5) * _ScaleMult, 0.0f);
      this.TextLocation = new Vector2((float) -(330.0 - (double) num3 * 0.5) * _ScaleMult, -40f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      if (!DebugFlags.IsPCVersion)
        return;
      this.HEadLoc.X = (float) (-(double) this.VSCale.X * 0.5);
      this.HEadLoc.X += (float) ((double) this.talkinghead.DrawRect.Width * (double) this.talkinghead.scale * 0.5);
      this.HEadLoc.X += 20f;
      this.TextLocation.X = (float) (-(double) this.VSCale.X * 0.5);
      this.TextLocation.X += (float) this.talkinghead.DrawRect.Width * this.talkinghead.scale;
      this.TextLocation.X += 40f;
      this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
      if ((double) this.VSCale.Y >= (double) ((float) this.talkinghead.DrawRect.Height * this.talkinghead.scale) + 14.0)
        return;
      this.VSCale.Y = (float) this.talkinghead.DrawRect.Height * this.talkinghead.scale + 14f;
    }

    public bool TryToCompleteParagraph() => this.text.TryToCompleteParagraph();

    public void UpdateCharacterTextBox(float DeltaTme) => this.text.UpdateSimpleTextHandler(DeltaTme);

    public void SetRed()
    {
      this.text.paragraph.linemaker.SetAllColours(ColourData.FernRed);
      this.boc.SetEdgeRed();
      this.talkinghead.SetEdgeRed();
    }

    public void DrawCharacterTextBox(Vector2 Offset, SpriteBatch spritebatch)
    {
      if ((double) this.BaseScale != -1.0)
      {
        Offset += this.Location;
        this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, Offset, this.VSCale);
        this.text.DrawSimpleTextHandler(Offset, 1f, spritebatch);
        this.talkinghead.DrawTalkingHead(Offset, spritebatch);
      }
      else
      {
        Vector2 TotalScale = this.VSCale * Sengine.ScreenRatioUpwardsMultiplier;
        if (DebugFlags.IsPCVersion)
        {
          this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset + this.Extra, TotalScale);
          float heightOfParagraph = this.text.GetHeightOfParagraph();
          this.text.DrawSimpleTextHandler(this.Location + Offset + this.TextLocation + this.Extra + new Vector2(0.0f, (float) (((double) TotalScale.Y - (double) heightOfParagraph * (double) Sengine.ScreenRatioUpwardsMultiplier.Y) * 0.5)), 1f, spritebatch);
          this.talkinghead.DrawTalkingHead(this.Location + Offset + this.HEadLoc + this.Extra, spritebatch);
        }
        else
        {
          this.Frame.DrawGameObjectNineSlice(spritebatch, AssetContainer.SpriteSheet, this.Location + Offset, TotalScale);
          this.text.DrawSimpleTextHandler(this.Location + Offset + this.TextLocation + this.Extra, 1f, spritebatch);
          this.talkinghead.DrawTalkingHead(this.Location + Offset + this.HEadLoc + this.Extra, spritebatch);
        }
      }
    }
  }
}
