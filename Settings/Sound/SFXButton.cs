// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Sound.SFXButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Audio;
using TinyZoo.PlayerDir;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageShop.RecipeView.FoodSlidr;

namespace TinyZoo.Settings.Sound
{
  internal class SFXButton
  {
    private DragAndBar slider;
    private ZGenericText label;
    public Vector2 Location;
    public bool SOmethingChanged;
    private bool SomethingJustChanged;
    private float lastValue;
    private Vector2 size;
    private int maxForUIScale;
    private int minForUIScale;
    private ZGenericText extraDisplayValueText;

    public SFXButtonType refButtonType { get; private set; }

    public SFXButton(SFXButtonType buttonType, float basescale) => this.SetUp(buttonType, basescale);

    public SFXButton(bool _IsMusic, float basescale)
    {
      if (_IsMusic)
        this.SetUp(SFXButtonType.Music, basescale);
      else
        this.SetUp(SFXButtonType.SFX, basescale);
    }

    private void SetUp(SFXButtonType buttonType, float basescale)
    {
      this.refButtonType = buttonType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(basescale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      int numDiscreteValues_ = 10;
      string _textToWrite = "";
      this.lastValue = 0.0f;
      if (this.refButtonType == SFXButtonType.SFX)
      {
        _textToWrite = SEngine.Localization.Localization.GetText(53);
        this.lastValue = SoundEffectsManager.SFXVolume;
      }
      else if (this.refButtonType == SFXButtonType.Music)
      {
        _textToWrite = SEngine.Localization.Localization.GetText(52);
        this.lastValue = MusicManager.MusicVol;
      }
      else if (this.refButtonType == SFXButtonType.UIScale)
      {
        _textToWrite = "UI Scale";
        UIScaleSettings.GetDefaultUIScale(out this.maxForUIScale);
        this.minForUIScale = UIScaleSettings.MinUIScaleMult;
        this.lastValue = (PlayerStats.UXMult - (float) this.minForUIScale) / (float) (this.maxForUIScale - this.minForUIScale);
        numDiscreteValues_ = this.maxForUIScale - this.minForUIScale + 1;
        this.extraDisplayValueText = new ZGenericText("XX", basescale, false, _UseOnePointFiveFont: true);
        this.SetExtraText();
        this.extraDisplayValueText.vLocation.Y -= this.extraDisplayValueText.GetSize().Y * 0.5f;
      }
      this.lastValue = MathHelper.Clamp(this.lastValue, 0.0f, 1f);
      this.label = new ZGenericText(_textToWrite, basescale, false, _UseOnePointFiveFont: true);
      this.slider = new DragAndBar(false, this.lastValue, uiScaleHelper.ScaleX(100f), basescale, numDiscreteValues_);
      this.size = Vector2.Zero;
      this.label.vLocation.Y -= this.label.GetSize().Y * 0.5f;
      this.size.X += uiScaleHelper.ScaleX(AssetContainer.SpringFontX1AndHalf.MeasureString(SEngine.Localization.Localization.GetText(52)).X);
      this.size.X += defaultBuffer.X * 2f;
      this.slider.Location.X = this.size.X;
      this.slider.Location.X += this.slider.GetSize().X * 0.5f;
      this.size.X += this.slider.GetSize().X;
      if (this.extraDisplayValueText != null)
      {
        this.size.X += defaultBuffer.X * 2f;
        this.extraDisplayValueText.vLocation.X = this.size.X;
        this.size.X += this.extraDisplayValueText.GetSize().X;
      }
      this.size.Y = Math.Max(this.label.GetSize().Y, this.slider.GetSize().Y);
    }

    public Vector2 GetSize() => this.size;

    public void UpdateSoundButton(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      this.slider.UpdateDragAndBar(player, DeltaTime, Offset);
      float currentDragPercent = this.slider.CurrentDragPercent;
      if ((double) this.lastValue != (double) currentDragPercent)
      {
        this.SOmethingChanged = true;
        float num = MathHelper.Clamp(currentDragPercent, 0.0f, 1f);
        this.lastValue = num;
        if (this.refButtonType == SFXButtonType.Music)
        {
          MusicManager.MusicVol = num;
          MusicManager.SetVolumeForOptions(MusicManager.MusicVol);
        }
        else if (this.refButtonType == SFXButtonType.SFX)
          SoundEffectsManager.SFXVolume = num;
        else if (this.refButtonType == SFXButtonType.UIScale)
        {
          PlayerStats.UXMult = (float) (int) Math.Round((double) (this.maxForUIScale - this.minForUIScale) * (double) num + (double) this.minForUIScale);
          this.SetExtraText();
        }
        this.SomethingJustChanged = true;
      }
      if (!this.SomethingJustChanged)
        return;
      if (this.refButtonType == SFXButtonType.SFX)
        SoundEffectsManager.PlaySpecificSound((SoundEffectType) Game1.Rnd.Next(42, 53));
      this.SomethingJustChanged = false;
    }

    private void SetExtraText()
    {
      if (this.extraDisplayValueText == null || this.refButtonType != SFXButtonType.UIScale)
        return;
      this.extraDisplayValueText.textToWrite = UIScaleSettings.GetUIScaleMultToDisplayValue(PlayerStats.UXMult).ToString();
      float defaultUiScale = (float) UIScaleSettings.GetDefaultUIScale(out int _);
      if ((double) PlayerStats.UXMult != (double) defaultUiScale)
        return;
      this.extraDisplayValueText.textToWrite += " (Default)";
    }

    public void DrawSoundButton(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      this.slider.DrawDragAndBar(spriteBatch, Offset);
      this.label.DrawZGenericText(Offset, spriteBatch);
      if (this.extraDisplayValueText == null)
        return;
      this.extraDisplayValueText.DrawZGenericText(Offset, spriteBatch);
    }
  }
}
