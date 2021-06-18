// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Settings.Z_Debug.DebugMenu
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.Utils;
using TinyZoo.Z_CustomMaps;

namespace TinyZoo.Z_Settings.Z_Debug
{
  internal class DebugMenu
  {
    private BackButton Back;
    private TextButton[] textbuttons;
    private LerpHandler_FloatArray lerparray;
    private ButtonRepeater repeater;
    public int Selected;

    public DebugMenu()
    {
      this.Back = new BackButton();
      this.repeater = new ButtonRepeater();
      this.textbuttons = new TextButton[2];
      for (int index = 0; index < this.textbuttons.Length; ++index)
        this.textbuttons[index] = new TextButton(DebugMenu.GetDebugButtonTypeToString((DebugButtonType) index), 100f);
      for (int index = 0; index < this.textbuttons.Length; ++index)
      {
        this.textbuttons[index].vLocation = new Vector2(780f, (float) (200.0 + (double) (50 * index) * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
        this.textbuttons[index].SetLemonANdBlue();
      }
      this.lerparray = new LerpHandler_FloatArray(this.textbuttons.Length, 0.1f, 1f, 0.0f);
    }

    private static string GetDebugButtonTypeToString(DebugButtonType btn)
    {
      switch (btn)
      {
        case DebugButtonType.EndlessMoney:
          return DebugFlags.HasEndlessMoney ? "Infinite Cash: On" : "Infinite Cash: Off";
        case DebugButtonType.UnlockAllResearch:
          return Z_DebugFlags.UnlockAllBuildingsHack ? "Research Unlocked" : "Unlock All research";
        case DebugButtonType.SpecialSave:
          return "Load Yvonne's SUPER ZOO!";
        default:
          return "NOT FOUND";
      }
    }

    public bool UpdateDebugMenu(Player player, float DeltaTime)
    {
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      if (this.lerparray.IsComplete())
      {
        if (this.Back.UpdateBackButton(player, DeltaTime))
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          this.BeginExit();
          return true;
        }
        int index1 = -1;
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
              if (this.Selected < this.textbuttons.Length - 1)
              {
                SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
                ++this.Selected;
                break;
              }
              break;
          }
        }
        for (int index2 = 0; index2 < this.textbuttons.Length; ++index2)
        {
          if (this.textbuttons[index2].UpdateTextButton(player, new Vector2(this.lerparray.arrayoflerpers[index2].Value * 400f, 0.0f), DeltaTime))
          {
            index1 = index2;
            this.Selected = index2;
          }
          if (this.textbuttons[index2].MouseOver && !GameFlags.IsUsingController)
            this.Selected = index2;
        }
        if (player.inputmap.PressedThisFrame[0])
          index1 = this.Selected;
        if (index1 > -1)
        {
          switch (index1)
          {
            case 0:
              DebugFlags.HasEndlessMoney = !DebugFlags.HasEndlessMoney;
              this.textbuttons[index1].SetText(DebugMenu.GetDebugButtonTypeToString(DebugButtonType.EndlessMoney));
              break;
            case 1:
              Z_DebugFlags.UnlockAllBuildingsHack = true;
              player.Stats.research.DebugUnlockAllResearch();
              this.textbuttons[index1].SetText(DebugMenu.GetDebugButtonTypeToString(DebugButtonType.UnlockAllResearch));
              break;
            case 3:
              Z_DebugFlags.ForceLoadString = Map_Yvonne.GetSave();
              TinyZoo.Game1.screenfade.BeginFade(true);
              CloudSaveUtil.JustLoadedFromCloud = true;
              DebugFlags.LoadGame = true;
              TinyZoo.Game1.SetNextGameState(GAMESTATE.SplashScreenSetUp);
              this.textbuttons[index1].SetText(DebugMenu.GetDebugButtonTypeToString(DebugButtonType.SpecialSave));
              break;
          }
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        }
      }
      return false;
    }

    public void BeginExit() => this.lerparray.LerpOff(0.08f, HoldThisForThisLong: 0.34f);

    public void DrawDebugMenu()
    {
      for (int index = 0; index < this.textbuttons.Length; ++index)
      {
        this.textbuttons[index].MouseOver = index == this.Selected;
        this.textbuttons[index].DrawTextButton(new Vector2(this.lerparray.arrayoflerpers[index].Value * 400f, 0.0f), 1f, AssetContainer.pointspritebatchTop05);
      }
      this.Back.DrawBackButton(Vector2.Zero);
    }
  }
}
