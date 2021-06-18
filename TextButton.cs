// Decompiled with JetBrains decompiler
// Type: TinyZoo.TextButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Localization;
using System;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.PlayerDir;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo
{
  internal class TextButton : GameObject
  {
    public StringInBox stringinabox;
    private Vector2 VSCale;
    public Vector2 CollisionEx;
    public bool MouseOver;
    private bool MouseWasOver;
    private GameObjectNineSlice MouseOverThing;
    public bool Locked;
    private float BaseScale = -1f;
    private float OverrideFrameScale = -1f;
    private bool MouseOverSounds;
    public ButtonPressed UseButtonPressed = ButtonPressed.Count;
    public bool ClickSounds;
    private bool IsBuyButton;
    private Coin coin;
    private int CST;
    private bool CanAfford;
    private TRC_ButtonDisplay controllerButton;
    public ControllerButton controllerbutton;

    public bool IsDisabledAndDarkened { get; private set; }

    public TextButton(
      string TextToDraw,
      float Length = 30f,
      bool DrawFromCentre = true,
      float OverAllMultiplier = 1f,
      bool _UseRoundaboutFont = false)
    {
      this.CollisionEx = Vector2.Zero;
      this.Locked = false;
      this.stringinabox = new StringInBox(TextToDraw, 3f * OverAllMultiplier * Sengine.ScreenRationReductionMultiplier.Y, Length, DrawFromCentre, _UseRoundaboutFont);
      this.VSCale = new Vector2(6f * OverAllMultiplier, 6f * OverAllMultiplier);
      this.VSCale *= Sengine.ScreenRationReductionMultiplier.Y;
      this.DrawRect = Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(ColourData.FernVeryDarkBlue);
      this.stringinabox.SetAsButtonFrame(BTNColour.Green);
      this.MakeouseOver();
    }

    public void SetActivateAnimation() => this.stringinabox.SetActivateAnimation();

    public void DarkenAndDisable()
    {
      this.IsDisabledAndDarkened = true;
      this.SetButtonColour(BTNColour.Grey);
      this.stringinabox.TextThing.SetAllColours(0.5f, 0.5f, 0.5f);
    }

    public void Disable(bool IsDisable)
    {
      this.IsDisabledAndDarkened = IsDisable;
      if (IsDisable)
      {
        this.SetButtonColour(BTNColour.Grey);
        this.stringinabox.TextThing.SetAllColours(0.5f, 0.5f, 0.5f);
      }
      else
      {
        this.SetButtonColour(BTNColour.Green);
        this.stringinabox.TextThing.SetAllColours(1f, 1f, 1f);
      }
    }

    public TextButton(
      float _BaseScale,
      string TextToDraw,
      float Length = 30f,
      bool DrawFromCentre = true,
      bool _UseRoundaboutFont = false,
      float _OverrideFrameScale = -1f,
      bool AllowMouseOverSounds = true,
      bool _ClickSounds = true)
    {
      this.MouseOverSounds = AllowMouseOverSounds;
      this.BaseScale = _BaseScale;
      this.OverrideFrameScale = _OverrideFrameScale;
      this.ClickSounds = _ClickSounds;
      this.CollisionEx = Vector2.Zero;
      this.Locked = false;
      this.stringinabox = new StringInBox(this.BaseScale * 2f, TextToDraw, Length, DrawFromCentre, _UseRoundaboutFont);
      this.VSCale = new Vector2((float) (6.0 * (double) this.BaseScale * 2.0), (float) (6.0 * (double) this.BaseScale * 2.0));
      this.VSCale *= Sengine.ScreenRationReductionMultiplier.Y;
      this.DrawRect = Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(ColourData.FernVeryDarkBlue);
      if ((double) this.OverrideFrameScale > -1.0)
        this.stringinabox.SetAsButtonFrame(BTNColour.Green, this.OverrideFrameScale);
      else
        this.stringinabox.SetAsButtonFrame(BTNColour.Green, this.BaseScale);
      this.MakeouseOver();
    }

    public Vector2 GetSize_True() => this.GetSize() * Sengine.ScreenRatioUpwardsMultiplier;

    public Vector2 GetSize() => this.stringinabox.GetVScale_Depricated();

    public void SetAsBuyButton(int Cost, Player player)
    {
      this.SetText("$" + (object) Cost);
      this.coin = new Coin();
      this.CST = Cost * 2;
      this.IsBuyButton = true;
      if (player.Stats.GetCashHeld() < Cost)
      {
        this.CanAfford = false;
        this.SetButtonColour(BTNColour.Red);
      }
      else
        this.CanAfford = true;
    }

    public bool MouseOverUpdate(Player player, Vector2 Offset, float DeltaTime, float ScaleMult = 1f)
    {
      if (!MathStuff.CheckPointCollision(true, this.vLocation + Offset, ScaleMult, (this.VSCale + this.stringinabox.GetVScale_Depricated()).X + this.CollisionEx.X, ((this.VSCale + this.stringinabox.GetVScale_Depricated()).Y + this.CollisionEx.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        return false;
      this.MouseOver = true;
      return true;
    }

    public void SetYellow() => this.stringinabox.SetYellow();

    public void SetButtonBlue()
    {
      if ((double) this.OverrideFrameScale > -1.0)
        this.stringinabox.SetAsButtonFrame(BTNColour.Blue, this.OverrideFrameScale);
      else
        this.stringinabox.SetAsButtonFrame(BTNColour.Blue, this.BaseScale);
      this.MakeouseOver();
    }

    public void SetButtonColour(BTNColour btnclr)
    {
      if ((double) this.OverrideFrameScale > -1.0)
        this.stringinabox.SetAsButtonFrame(btnclr, this.OverrideFrameScale);
      else
        this.stringinabox.SetAsButtonFrame(btnclr, this.BaseScale);
      this.MakeouseOver();
    }

    private void MakeouseOver()
    {
      this.MouseOverThing = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.White, out Vector3 _), 7);
      this.MouseOverThing.fAlpha = 0.3f;
    }

    public void SetInnerColour(Vector3 Clr) => this.stringinabox.SetAllColours(Clr);

    public Vector2 GetVScale() => this.VSCale + this.stringinabox.GetVScale_Depricated();

    public void SetButtonCyan()
    {
      this.SetAllColours(ColourData.Cyannz);
      this.stringinabox.SetButtonCyan();
      this.Locked = false;
    }

    public void SetButtonTan()
    {
      this.SetAllColours(ColourData.TanGoldText);
      this.stringinabox.SetButtonTan();
      this.Locked = false;
    }

    public void SetButtonGreen()
    {
      this.stringinabox.SetButtonGreen();
      this.Locked = false;
    }

    public string GetText() => this.stringinabox.GetText();

    public void SetLemonANdBlue()
    {
      this.SetAllColours(ColourData.FernLemon);
      this.stringinabox.SetLemonANdBlue();
    }

    public void SetWhite()
    {
      this.stringinabox.SetWhite();
      this.Locked = false;
    }

    public void SetText(string NewText) => this.stringinabox.SetText(NewText);

    public void SetButtonRed()
    {
      this.Locked = true;
      this.stringinabox.SetButtonRed();
    }

    public void SetButtonPurple() => this.stringinabox.SetButtonPurple();

    public void SetButtonYellow()
    {
      this.Locked = true;
      this.stringinabox.SetButtonYellow();
    }

    public void SetButtonBlack() => this.stringinabox.SetButtonBlack();

    public void DoWhiteFlash() => this.stringinabox.SetPrimaryColours(0.5f, Vector3.One);

    public bool UpdateTextButton(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      bool UseReleaseTap = true,
      float ScaleMult = 1f,
      bool UseTouchStart = false,
      bool BlockControllerIcon = false)
    {
      if (this.IsDisabledAndDarkened)
        return false;
      if (this.IsBuyButton)
      {
        if (this.CanAfford)
        {
          if (player.Stats.GetCashHeld() < this.CST / 2)
          {
            this.CanAfford = false;
            this.SetButtonColour(BTNColour.Red);
          }
        }
        else if (player.Stats.GetCashHeld() >= this.CST / 2)
        {
          this.CanAfford = true;
          this.SetButtonColour(BTNColour.Green);
        }
      }
      this.stringinabox.UpdateStringInBox(DeltaTime);
      if ((double) this.stringinabox.ActivationLerper.Value < 1.0)
        return false;
      this.MouseOver |= MathStuff.CheckPointCollision(true, this.vLocation + Offset, ScaleMult, (this.VSCale + this.stringinabox.GetVScale_Depricated()).X + this.CollisionEx.X, ((this.VSCale + this.stringinabox.GetVScale_Depricated()).Y + this.CollisionEx.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]);
      if (!SEngine.Flags.PlatformIsMobile)
        this.MouseOver |= MathStuff.CheckPointCollision(true, this.vLocation + Offset, ScaleMult, (this.VSCale + this.stringinabox.GetVScale_Depricated()).X + this.CollisionEx.X, ((this.VSCale + this.stringinabox.GetVScale_Depricated()).Y + this.CollisionEx.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      if (UseTouchStart && (double) player.player.touchinput.MultiTouchTapArray[0].X > 0.0 && MathStuff.CheckPointCollision(true, this.vLocation + Offset, ScaleMult, (this.VSCale + this.stringinabox.GetVScale_Depricated()).X + this.CollisionEx.X, ((this.VSCale + this.stringinabox.GetVScale_Depricated()).Y + this.CollisionEx.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTapArray[0]))
      {
        if (this.ClickSounds)
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
        return true;
      }
      if (!this.MouseWasOver && this.MouseOver)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.1f, 0.4f);
      }
      else
      {
        if (!UseReleaseTap)
          throw new Exception("NO WAY");
        if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, ScaleMult, (this.VSCale + this.stringinabox.GetVScale_Depricated()).X + this.CollisionEx.X, ((this.VSCale + this.stringinabox.GetVScale_Depricated()).Y + this.CollisionEx.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
        {
          player.inputmap.ClearAllInput(player);
          if (this.ClickSounds)
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
          return true;
        }
      }
      if (this.UseButtonPressed != ButtonPressed.Count)
      {
        if (player.inputmap.PressedThisFrame[(int) this.UseButtonPressed])
        {
          if (this.ClickSounds)
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
          return true;
        }
      }
      else if (GameFlags.IsUsingController && !BlockControllerIcon && this.controllerButton != null)
      {
        switch (this.controllerbutton)
        {
          case ControllerButton.XboxA:
            if (player.inputmap.PressedThisFrame[14])
            {
              if (this.ClickSounds)
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
              return true;
            }
            break;
          case ControllerButton.XboxB:
            if (player.inputmap.PressedThisFrame[13])
            {
              if (this.ClickSounds)
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
              return true;
            }
            break;
          case ControllerButton.XboxX:
            if (player.inputmap.PressedThisFrame[15])
            {
              if (this.ClickSounds)
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
              return true;
            }
            break;
          case ControllerButton.XboxY:
            if (player.inputmap.PressedThisFrame[12])
            {
              if (this.ClickSounds)
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.5f, 0.4f);
              return true;
            }
            break;
        }
      }
      return false;
    }

    public void AddControllerButton(ControllerButton button)
    {
      this.controllerbutton = button;
      float pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier);
      if (GameFlags.IsConsoleVersion)
      {
        switch (PlayerStats.language)
        {
          case Language.English:
          case Language.Russian:
          case Language.Japanese:
          case Language.Chinese_Simplified:
          case Language.Chinese_Traditional:
          case Language.Korean:
            pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(3f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier);
            break;
          case Language.German:
          case Language.Spanish:
          case Language.Portuguese:
          case Language.French:
            pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier);
            break;
        }
      }
      this.controllerButton = new TRC_ButtonDisplay(pixelSizeBestMatch);
      this.controllerButton.SetAsStaticButton(GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, button);
    }

    public void DrawTextButton(
      Vector2 Offset,
      float AlphaMultipler,
      SpriteBatch UseThis,
      bool BlockControllerIcon = false)
    {
      this.MouseWasOver = this.MouseOver;
      this.stringinabox.DrawStringInBox(Offset + this.vLocation, UseThis, AlphaMultipler, AddForController: (this.controllerButton != null || this.IsBuyButton));
      if (!this.IsDisabledAndDarkened && this.MouseOver)
      {
        if (this.MouseOverThing != null)
        {
          this.MouseOverThing.scale = this.stringinabox.Frame.scale;
          this.MouseOverThing.DrawGameObjectNineSlice(UseThis, AssetContainer.SpriteSheet, Offset + this.vLocation, this.stringinabox.GetFrameScaleForMouseOver());
        }
        else
          this.Draw(UseThis, AssetContainer.SpriteSheet, Offset, this.VSCale + this.stringinabox.GetVScale_Depricated() * Sengine.ScreenRatioUpwardsMultiplier, ColourData.MouseOverAlpha);
      }
      if (this.IsBuyButton)
      {
        this.coin.fAlpha = AlphaMultipler;
        this.coin.DrawCoin(UseThis, Offset + this.vLocation + new Vector2((float) ((double) this.stringinabox.GetVScale_Depricated().X * -0.5 + 12.0), 0.0f));
      }
      if (this.controllerButton != null && GameFlags.IsUsingController && !BlockControllerIcon)
      {
        this.controllerButton.fAlpha = AlphaMultipler;
        this.controllerButton.DrawTRC_ButtonDisplay(UseThis, AssetContainer.TRC_Sprites, Offset + this.vLocation + new Vector2((float) ((double) this.stringinabox.GetVScale_Depricated().X * -0.5 + 12.0), 0.0f));
      }
      this.MouseOver = false;
      if (!this.IsDisabledAndDarkened || this.MouseOverThing == null)
        return;
      this.MouseOverThing.scale = this.stringinabox.Frame.scale;
      this.MouseOverThing.SetAllColours(0.0f, 0.0f, 0.0f);
      this.MouseOverThing.DrawGameObjectNineSlice(UseThis, AssetContainer.SpriteSheet, Offset + this.vLocation, this.stringinabox.GetFrameScaleForMouseOver());
    }

    public void DrawTextButton(Vector2 Offset, float AlphaMultipler = 1f) => this.DrawTextButton(Offset, AlphaMultipler, AssetContainer.pointspritebatch03);

    public void WorldOffsetDrawTextButton(Vector2 Location)
    {
      this.MouseWasOver = this.MouseOver;
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(Location + this.vLocation);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Location + this.vLocation, (this.VSCale + this.stringinabox.GetVScale_Depricated()) * Sengine.ScreenRatioUpwardsMultiplier, 0.0f);
      this.stringinabox.DrawStringInBox(screenSpace, ScaleMult: Sengine.WorldOriginandScale.Z, AddForController: (this.controllerButton != null));
      if (this.MouseOver)
        this.Draw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, RenderMath.TranslateWorldSpaceToScreenSpace(Location + this.vLocation), this.VSCale + this.stringinabox.GetVScale_Depricated() * Sengine.WorldOriginandScale.Z, ColourData.MouseOverAlpha);
      if (this.controllerButton != null && GameFlags.IsUsingController)
      {
        if (GameFlags.IsConsoleVersion)
          this.controllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatch03, AssetContainer.TRC_Sprites, screenSpace + new Vector2(this.stringinabox.GetVScale_Depricated().X * -0.5f, 0.0f));
        else
          this.controllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatch03, AssetContainer.TRC_Sprites, screenSpace + new Vector2((float) ((double) this.stringinabox.GetVScale_Depricated().X * -0.5 + 12.0), 0.0f));
      }
      this.MouseOver = false;
    }
  }
}
