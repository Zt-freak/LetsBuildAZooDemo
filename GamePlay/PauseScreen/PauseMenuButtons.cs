// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.PauseScreen.PauseMenuButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.Lerp;
using SEngine.Localization;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;

namespace TinyZoo.GamePlay.PauseScreen
{
  internal class PauseMenuButtons
  {
    private LerpHandler_FloatArray lerparray;
    private TextButton[] textbutton;
    private bool Exiting;
    private WatchVideoButton watchbutton;
    private bool IsOverWorldMenu;
    private ButtonRepeater repeater;
    private int SelectedIndex;
    private bool AllowQuit;

    public PauseMenuButtons(Player player, bool _IsOverWorldMenu)
    {
      this.SelectedIndex = -1;
      this.IsOverWorldMenu = _IsOverWorldMenu;
      string str = "";
      float Length = 40f;
      if (GameFlags.IsConsoleVersion || GameFlags.IsUsingController)
        Length = 50f;
      if (GameFlags.IsUsingController)
        this.SelectedIndex = 0;
      if (this.IsOverWorldMenu)
      {
        this.textbutton = new TextButton[2];
        this.textbutton[0] = new TextButton(SEngine.Localization.Localization.GetText(57), 100f, OverAllMultiplier: Z_GameFlags.GetBaseScaleForUI());
        SEngine.Localization.Localization.GetText(61);
        string TextToDraw = "Quit Game";
        if (Flags.PlatformIsMobile)
          TextToDraw = SEngine.Localization.Localization.GetText(58);
        this.textbutton[1] = new TextButton(TextToDraw, 100f, OverAllMultiplier: Z_GameFlags.GetBaseScaleForUI());
        this.textbutton[0].AddControllerButton(ControllerButton.XboxA);
        this.textbutton[1].AddControllerButton(ControllerButton.XboxA);
      }
      else
      {
        int num1 = -1;
        if (GameFlags.IsArcadeMode)
          num1 = 0;
        if (player.Stats.TutorialsComplete[14] || GameFlags.IsArcadeMode || GameFlags.BountyMode)
          this.AllowQuit = true;
        if (this.AllowQuit)
          num1 = 0;
        this.textbutton = new TextButton[5 + num1];
        this.textbutton[0] = new TextButton(str + SEngine.Localization.Localization.GetText(57), Length, OverAllMultiplier: 1.3f);
        if (player.Stats.TutorialsComplete[14] || GameFlags.IsArcadeMode || GameFlags.BountyMode)
        {
          this.textbutton[1] = new TextButton(str + "Retry", Length, OverAllMultiplier: 1.3f);
          if (!player.Stats.ADisabled(false, player))
          {
            this.watchbutton = new WatchVideoButton("Retry");
            if (PlayerStats.language != Language.English)
            {
              float num2 = SpringFontUtil.MeasureString("Retry", AssetContainer.springFont).X * this.watchbutton.TextScale;
              float num3 = 160f;
              if ((double) num2 > (double) num3)
                this.watchbutton.TextScale = num3 / num2 * this.watchbutton.TextScale;
            }
            this.watchbutton.AddControllerButton(ControllerButton.XboxA, true);
          }
        }
        if (GameFlags.IsArcadeMode || this.AllowQuit || GameFlags.BountyMode)
        {
          this.textbutton[2] = new TextButton(str + SEngine.Localization.Localization.GetText(58), Length, OverAllMultiplier: 1.3f);
          this.textbutton[2].AddControllerButton(ControllerButton.XboxA);
        }
        this.textbutton[0].AddControllerButton(ControllerButton.XboxA);
        if (this.textbutton[1] != null)
          this.textbutton[1].AddControllerButton(ControllerButton.XboxA);
      }
      this.lerparray = new LerpHandler_FloatArray(5, 0.1f, -1f, 0.0f);
      for (int index = 0; index < this.textbutton.Length; ++index)
      {
        if (this.textbutton[index] != null)
          this.textbutton[index].vLocation = new Vector2(512f, (float) (250 + 120 * index));
      }
      this.repeater = new ButtonRepeater();
    }

    public void UpdatePauseMenuButtons(
      float DeltaTime,
      Player player,
      out PauseScreenButton buttonpressed)
    {
      buttonpressed = PauseScreenButton.Count;
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      if (GameFlags.IsUsingController)
      {
        int num = player.inputmap.HeldButtons[16] ? 1 : 0;
        DirectionPressed Direction;
        if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
        {
          if (Direction == DirectionPressed.Down && this.SelectedIndex < this.textbutton.Length - 1 && this.textbutton[this.SelectedIndex + 1] != null)
          {
            ++this.SelectedIndex;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          }
          if (Direction == DirectionPressed.Up && this.SelectedIndex > 0)
          {
            --this.SelectedIndex;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          }
        }
      }
      for (int index = 0; index < this.textbutton.Length; ++index)
      {
        if ((double) this.lerparray.arrayoflerpers[index].Value == 0.0)
        {
          if (index == 0 && (player.inputmap.PressedThisFrame[20] || player.inputmap.PressedBackOnController()))
            buttonpressed = (PauseScreenButton) index;
          if (this.textbutton[index] != null)
          {
            if (index == 1 && this.watchbutton != null)
            {
              if (this.watchbutton.UpdateWatchVideoButton(player, new Vector2(this.lerparray.arrayoflerpers[index].Value * 1024f, 0.0f) + this.textbutton[index].vLocation))
                buttonpressed = (PauseScreenButton) index;
            }
            else if (this.textbutton[index].UpdateTextButton(player, new Vector2(this.lerparray.arrayoflerpers[index].Value * 1024f, 0.0f), DeltaTime, BlockControllerIcon: (this.SelectedIndex != index)))
              buttonpressed = (PauseScreenButton) index;
          }
        }
      }
    }

    public void Exit() => this.lerparray.LerpOff(0.05f);

    public bool ExitComplete() => this.lerparray.IsComplete();

    public void DrawPauseMenuButtons()
    {
      for (int index = 0; index < this.textbutton.Length; ++index)
      {
        if (this.textbutton[index] != null)
        {
          if (index == 1 && this.watchbutton != null)
            this.watchbutton.DrawWatchVideoButton(this.textbutton[index].vLocation + new Vector2(this.lerparray.arrayoflerpers[index].Value * 1024f, 0.0f), this.SelectedIndex == 1 && GameFlags.IsUsingController);
          else
            this.textbutton[index].DrawTextButton(new Vector2(this.lerparray.arrayoflerpers[index].Value * 1024f, 0.0f), 1f, AssetContainer.pointspritebatchTop05, this.SelectedIndex != index);
        }
      }
    }
  }
}
