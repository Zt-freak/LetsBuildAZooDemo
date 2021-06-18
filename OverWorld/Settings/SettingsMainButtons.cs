// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Settings.SettingsMainButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.Utils;

namespace TinyZoo.OverWorld.Settings
{
  internal class SettingsMainButtons
  {
    private List<TextButton> textbuttons;
    public List<SettingsButton> settingss;
    private LerpHandler_FloatArray lerparray;
    private ButtonRepeater repeater;
    public int Selected;
    public SettingsButton buttonpressed;
    private bool Exiting;
    private ScreenHeading screenheading;

    public SettingsMainButtons()
    {
      this.screenheading = new ScreenHeading("SETTINGS", 70f);
      GameFlags.IsConsoleVersion = DebugFlags.IsPCVersion;
      this.Selected = 0;
      this.repeater = new ButtonRepeater();
      int num1 = 1;
      if (GameFlags.IsConsoleVersion)
        num1 = 3;
      if (!Z_DebugFlags.HasDebugMenu)
      {
        int num2 = num1 + 1;
      }
      this.textbuttons = new List<TextButton>();
      this.settingss = new List<SettingsButton>();
      int num3 = 110;
      int num4 = 0;
      int num5 = GameFlags.IsConsoleVersion ? 1 : 0;
      int num6 = GameFlags.IsConsoleVersion ? 1 : 0;
      this.textbuttons.Add(new TextButton(SEngine.Localization.Localization.GetText(33), (float) num3));
      this.settingss.Add(SettingsButton.Sound);
      int num7 = num4 + 1;
      this.textbuttons.Add(new TextButton(SEngine.Localization.Localization.GetText(78), (float) num3));
      this.settingss.Add(SettingsButton.Language);
      int index1 = num7 + 1;
      if (!GameFlags.IsConsoleVersion)
      {
        this.textbuttons[index1] = new TextButton(SEngine.Localization.Localization.GetText(36), (float) num3);
        this.settingss[index1] = SettingsButton.Contact;
        int index2 = index1 + 1;
        this.textbuttons[index2] = new TextButton(SEngine.Localization.Localization.GetText(37), (float) num3);
        this.settingss[index2] = SettingsButton.RestorePurchases;
      }
      if (Z_DebugFlags.HasDebugMenu)
      {
        this.textbuttons[this.textbuttons.Count - 1] = new TextButton("DEBUG", (float) num3);
        this.settingss[this.textbuttons.Count - 1] = SettingsButton.DebugMenu;
      }
      for (int index2 = 0; index2 < this.textbuttons.Count; ++index2)
      {
        this.textbuttons[index2].vLocation = new Vector2(750f, (float) (220.0 * (double) Sengine.ScreenRationReductionMultiplier.Y + (double) (50 * index2) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
        this.textbuttons[index2].SetLemonANdBlue();
      }
      this.lerparray = new LerpHandler_FloatArray(this.textbuttons.Count, 0.1f, 1f, 0.0f);
      this.buttonpressed = SettingsButton.Count;
    }

    public bool UpdateSettingsMainButtons(Player player, float DeltaTime, out bool StartedExit)
    {
      StartedExit = false;
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      DirectionPressed Direction;
      if (this.lerparray.IsComplete() && !this.Exiting && this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
      {
        switch (Direction)
        {
          case DirectionPressed.Up:
            if (this.Selected > 0)
            {
              --this.Selected;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              break;
            }
            break;
          case DirectionPressed.Down:
            if (this.Selected < this.textbuttons.Count - 1)
            {
              ++this.Selected;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              break;
            }
            break;
        }
      }
      int num = -1;
      if (!this.Exiting)
      {
        for (int index = 0; index < this.textbuttons.Count; ++index)
        {
          if (this.textbuttons[index].UpdateTextButton(player, new Vector2(this.lerparray.arrayoflerpers[index].Value * 400f, 0.0f), DeltaTime))
          {
            num = index;
            this.Selected = index;
          }
          if (this.textbuttons[index].MouseOver)
            this.Selected = index;
        }
        if (player.inputmap.PressedThisFrame[0])
          num = this.Selected;
        if (num > -1 && this.buttonpressed == SettingsButton.Count)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          if (this.settingss[num] == SettingsButton.Contact)
          {
            PIP_Emailer.SendEmail(player, false);
          }
          else
          {
            this.buttonpressed = this.settingss[num];
            this.lerparray.LerpOff(0.08f, num, 0.34f);
            this.Exiting = true;
            StartedExit = true;
          }
        }
      }
      return this.buttonpressed != SettingsButton.Count && this.lerparray.IsComplete();
    }

    public void DrawSettingsMainButtons()
    {
      if (this.screenheading != null)
        this.screenheading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
      for (int index = 0; index < this.textbuttons.Count; ++index)
      {
        this.textbuttons[index].MouseOver = index == this.Selected;
        this.textbuttons[index].DrawTextButton(new Vector2(this.lerparray.arrayoflerpers[index].Value * 450f, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
