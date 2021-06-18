// Decompiled with JetBrains decompiler
// Type: TinyZoo.Settings.Sound.SoundButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;
using SEngine.Utils;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.PlayerDir;

namespace TinyZoo.Settings.Sound
{
  internal class SoundButtons
  {
    private List<TextButton> textbuttons;
    private LerpHandler_FloatArray lerparray;
    private ButtonRepeater repeater;
    public int Selected;
    public SoundButtonType buttonpressed;
    private List<SoundButtonType> buttontypes;
    private ButtonRepeater ConfirmeRepeater;
    public bool SomethingChanged;
    private int EX;
    private int ResolutionSetting;
    private SFXButton UiScaleSlider;

    public SoundButtons(Player player) => this.CreateButtons(player);

    private void CreateButtons(Player player)
    {
      this.ResolutionSetting = (int) player.Stats.gfxresolution;
      this.ConfirmeRepeater = new ButtonRepeater();
      this.Selected = 0;
      this.repeater = new ButtonRepeater();
      this.textbuttons = new List<TextButton>();
      this.buttontypes = new List<SoundButtonType>();
      this.textbuttons.Add(new TextButton(SEngine.Localization.Localization.GetText(52) + ": " + (object) (int) ((double) MusicManager.MusicVol * 10.0), 100f));
      this.buttontypes.Add(SoundButtonType.Music);
      this.textbuttons.Add(new TextButton(SEngine.Localization.Localization.GetText(53) + ": " + (object) (int) ((double) SoundEffectsManager.SFXVolume * 10.0), 100f));
      this.buttontypes.Add(SoundButtonType.SFX);
      int num = Z_DebugFlags.IsBetaVersion ? 1 : 0;
      this.textbuttons.Add(new TextButton("BLAH BLAH", 100f));
      this.buttontypes.Add(SoundButtonType.Resolution);
      this.textbuttons.Add(new TextButton("Confirm Resolution", 100f));
      this.buttontypes.Add(SoundButtonType.ConfirmResolution);
      for (int index = 0; index < this.textbuttons.Count; ++index)
      {
        this.textbuttons[index].vLocation = new Vector2(780f, (float) (200.0 + (double) (50 * index) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
        this.textbuttons[index].SetLemonANdBlue();
      }
      this.lerparray = new LerpHandler_FloatArray(this.textbuttons.Count, 0.1f, 1f, 0.0f);
      this.buttonpressed = SoundButtonType.Count;
      int Max;
      float defaultUiScale = (float) UIScaleSettings.GetDefaultUIScale(out Max);
      if ((double) defaultUiScale == (double) UIScaleSettings.MinUIScaleMult && (double) Max == (double) defaultUiScale)
      {
        this.UiScaleSlider = (SFXButton) null;
      }
      else
      {
        float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
        this.UiScaleSlider = new SFXButton(SFXButtonType.UIScale, baseScaleForUi);
        this.UiScaleSlider.Location = this.textbuttons[this.textbuttons.Count - 1].vLocation;
        this.UiScaleSlider.Location.Y += this.textbuttons[this.textbuttons.Count - 1].GetSize_True().Y;
        this.UiScaleSlider.Location.Y += 10f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.UiScaleSlider.Location.Y += this.UiScaleSlider.GetSize().Y * 0.5f;
        this.UiScaleSlider.Location.X -= this.UiScaleSlider.GetSize().X * 0.5f;
      }
      this.SetResulution(player);
    }

    private void SetUIScale()
    {
      int index1 = -1;
      for (int index2 = 0; index2 < this.buttontypes.Count; ++index2)
      {
        if (this.buttontypes[index2] == SoundButtonType.UIScale)
          index1 = index2;
      }
      if ((double) PlayerStats.UXMult == 2.0)
        this.textbuttons[index1].SetText("UI Scale: 1");
      else if ((double) PlayerStats.UXMult == 3.0)
        this.textbuttons[index1].SetText("UI Scale: 1.5");
      else if ((double) PlayerStats.UXMult == 4.0)
        this.textbuttons[index1].SetText("UI Scale: 2");
      else if ((double) PlayerStats.UXMult == 5.0)
        this.textbuttons[index1].SetText("UI Scale: 2.5");
      else if ((double) PlayerStats.UXMult == 6.0)
      {
        this.textbuttons[index1].SetText("UI Scale: 3");
      }
      else
      {
        if ((double) PlayerStats.UXMult != 1.0)
          return;
        this.textbuttons[index1].SetText("UI Scale: .5");
      }
    }

    private void SetStrobe()
    {
      if (GameFlags.NoStrobe)
        this.textbuttons[5 - this.EX].SetText(SEngine.Localization.Localization.GetText(54));
      else
        this.textbuttons[5 - this.EX].SetText(SEngine.Localization.Localization.GetText(55));
    }

    private void SetIntro()
    {
      if (PlayerStats.WillPlayIntro)
        this.textbuttons[6 - this.EX].SetText("Intro On");
      else
        this.textbuttons[6 - this.EX].SetText("Intro Off");
    }

    private void SetAdvert()
    {
      if (PlayerStats.WillCacheAdverts)
        this.textbuttons[6 - this.EX].SetText("Preload Adverts On");
      else
        this.textbuttons[6 - this.EX].SetText("Preload Adverts Off");
    }

    private void ConfirmSetResolution(
      Player player,
      GraphicsDeviceManager graphics,
      GraphicsDevice _GraphicsDevice)
    {
      player.Stats.gfxresolution = (GFX_Resolution) this.ResolutionSetting;
      PCScreenResolutionManager.SetNewScreenresolution(graphics, player.Stats.gfxresolution, _GraphicsDevice);
      PlayerStats.UXMult = -1f;
      player.SaveThisPlayer(DelayUntilNextFrame: false, IsEndOfDay: true);
      this.CreateButtons(player);
    }

    private void SetResulution(Player player)
    {
      this.textbuttons[2].SetText(PCScreenResolutionManager.GetGFX_ResolutionToString((GFX_Resolution) this.ResolutionSetting));
      if ((GFX_Resolution) this.ResolutionSetting == player.Stats.gfxresolution)
        this.textbuttons[3].Disable(true);
      else
        this.textbuttons[3].Disable(false);
    }

    private string GetVirtualStickString()
    {
      switch (InputMap.virtualstick)
      {
        case VirtualStickStatus.Default:
          return SEngine.Localization.Localization.GetText(47);
        case VirtualStickStatus.Digital:
          return SEngine.Localization.Localization.GetText(48);
        case VirtualStickStatus.Off:
          return SEngine.Localization.Localization.GetText(49);
        case VirtualStickStatus.AnalogueOnly:
          return SEngine.Localization.Localization.GetText(50);
        case VirtualStickStatus.DigitalOnly:
          return SEngine.Localization.Localization.GetText(51);
        default:
          return "Error";
      }
    }

    public bool UpdateSettingsMainButtons(
      Player player,
      float DeltaTime,
      out bool StartedExit,
      GraphicsDeviceManager graphics,
      GraphicsDevice _GraphicsDevice)
    {
      StartedExit = false;
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      if (this.lerparray.IsComplete())
      {
        DirectionPressed Direction;
        if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
        {
          switch (Direction)
          {
            case DirectionPressed.Up:
              if (this.Selected > 0)
              {
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
                --this.Selected;
                break;
              }
              break;
            case DirectionPressed.Down:
              if (this.Selected < this.textbuttons.Count - 1)
              {
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
                ++this.Selected;
                break;
              }
              break;
          }
        }
        this.textbuttons[-this.EX].SetText(SEngine.Localization.Localization.GetText(52) + ": " + (object) (int) ((double) MusicManager.MusicVol * 10.0));
        this.textbuttons[1 - this.EX].SetText(SEngine.Localization.Localization.GetText(53) + ": " + (object) (int) ((double) SoundEffectsManager.SFXVolume * 10.0));
      }
      int num = -1;
      for (int index = 0; index < this.textbuttons.Count; ++index)
      {
        if (this.textbuttons[index].UpdateTextButton(player, new Vector2(this.lerparray.arrayoflerpers[index].Value * 400f, 0.0f), DeltaTime))
        {
          num = index;
          this.Selected = index;
        }
        if (this.textbuttons[index].MouseOver && !GameFlags.IsUsingController)
          this.Selected = index;
      }
      bool flag = false;
      DirectionPressed Direction1;
      if (this.ConfirmeRepeater.UpdateMenuRepeats(DeltaTime, out Direction1, player.inputmap.HeldButtons[0], num > -1, player.inputmap.HeldButtons[5], player.inputmap.HeldButtons[6]))
      {
        if (this.Selected == -this.EX)
        {
          this.SomethingChanged = true;
          if (Direction1 == DirectionPressed.Left)
          {
            MusicManager.MusicVol -= 0.2f;
            if ((double) MusicManager.MusicVol < 0.0)
              MusicManager.MusicVol = 1f;
          }
          else
          {
            MusicManager.MusicVol += 0.2f;
            if ((double) MusicManager.MusicVol > 1.0)
              MusicManager.MusicVol = 0.0f;
          }
          MusicManager.SetVolumeForOptions(MusicManager.MusicVol);
        }
        else if (this.Selected == 1 - this.EX)
        {
          if (Direction1 == DirectionPressed.Left)
          {
            SoundEffectsManager.SFXVolume -= 0.2f;
            if ((double) SoundEffectsManager.SFXVolume < 0.0)
              SoundEffectsManager.SFXVolume = 1f;
          }
          else
          {
            SoundEffectsManager.SFXVolume += 0.2f;
            if ((double) SoundEffectsManager.SFXVolume > 1.0)
              SoundEffectsManager.SFXVolume = 0.0f;
          }
          SoundEffectsManager.PlaySpecificSound((SoundEffectType) TinyZoo.Game1.Rnd.Next(42, 53));
        }
        else if (this.Selected == 9 - this.EX)
        {
          ++PlayerStats.UXMult;
          if ((double) PlayerStats.UXMult > 6.0)
            PlayerStats.UXMult = 1f;
          this.SetUIScale();
        }
        else if (this.Selected == 3 - this.EX)
          this.ConfirmSetResolution(player, graphics, _GraphicsDevice);
        else if (this.Selected == 2 - this.EX)
        {
          ++this.ResolutionSetting;
          if (this.ResolutionSetting >= 11)
            this.ResolutionSetting = 0;
          switch ((GFX_Resolution) this.ResolutionSetting)
          {
            case GFX_Resolution._800_600:
              this.ResolutionSetting = 2;
              break;
            case GFX_Resolution._1024_768:
              this.ResolutionSetting = 2;
              break;
            case GFX_Resolution._1280_1024:
              this.ResolutionSetting = 6;
              break;
            case GFX_Resolution._1680_1050:
              this.ResolutionSetting = 8;
              break;
          }
          this.SetResulution(player);
        }
        else if (this.Selected == 5 - this.EX)
        {
          this.SomethingChanged = true;
          flag = true;
          GameFlags.NoStrobe = !GameFlags.NoStrobe;
          this.SetStrobe();
        }
        else if (this.Selected == 7 - this.EX)
        {
          this.SomethingChanged = true;
          PlayerStats.WillCacheAdverts = !PlayerStats.WillCacheAdverts;
          this.SetAdvert();
        }
        else if (this.Selected == 6 - this.EX)
        {
          this.SomethingChanged = true;
          PlayerStats.WillPlayIntro = !PlayerStats.WillPlayIntro;
          this.SetIntro();
        }
        this.SomethingChanged = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
      }
      if (num == 5 - this.EX && this.buttonpressed == SoundButtonType.Count && !flag)
      {
        this.SomethingChanged = true;
        GameFlags.NoStrobe = !GameFlags.NoStrobe;
        this.SetStrobe();
      }
      if (num == 8 && this.buttonpressed == SoundButtonType.Count && !GameFlags.IsConsoleVersion)
      {
        this.SomethingChanged = true;
        ++InputMap.virtualstick;
        if (InputMap.virtualstick == VirtualStickStatus.Vount)
          InputMap.virtualstick = VirtualStickStatus.Default;
        this.textbuttons[8].SetText(this.GetVirtualStickString());
      }
      if (this.buttonpressed != SoundButtonType.Count)
        return this.lerparray.IsComplete();
      if (this.UiScaleSlider != null)
        this.UiScaleSlider.UpdateSoundButton(player, DeltaTime, Vector2.Zero);
      return false;
    }

    public void BeginExit() => this.lerparray.LerpOff(0.08f, HoldThisForThisLong: 0.34f);

    public void DrawSettingsMainButtons(Vector2 Offset)
    {
      for (int index = 0; index < this.textbuttons.Count; ++index)
      {
        this.textbuttons[index].MouseOver = index == this.Selected;
        this.textbuttons[index].DrawTextButton(new Vector2(this.lerparray.arrayoflerpers[index].Value * 400f, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
      if (this.UiScaleSlider == null)
        return;
      this.UiScaleSlider.DrawSoundButton(Offset, AssetContainer.pointspritebatchTop05);
    }
  }
}
