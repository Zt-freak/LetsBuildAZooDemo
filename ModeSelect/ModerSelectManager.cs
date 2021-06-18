// Decompiled with JetBrains decompiler
// Type: TinyZoo.ModeSelect.ModerSelectManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine.Buttons;
using TinyZoo.Audio;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir;

namespace TinyZoo.ModeSelect
{
  internal class ModerSelectManager
  {
    private ModeSelectIcon arcade;
    private ModeSelectIcon build;
    private StoreBGManager storeBG;
    private int SelectedIndex;
    private ButtonRepeater repeater;

    public ModerSelectManager()
    {
      this.arcade = new ModeSelectIcon(true);
      this.build = new ModeSelectIcon(false);
      this.arcade.Location = new Vector2(762f, 340f);
      this.build.Location = new Vector2(262f, 340f);
      MusicManager.playsong(SongTitle.AC01, false, false);
      this.storeBG = new StoreBGManager();
      this.storeBG.SetSpecial();
      this.storeBG.SetSpecialRed();
      GameStateManager.tutorialmanager.ForceEndAllTutorials();
      this.repeater = new ButtonRepeater();
    }

    public void UpdateCharacterSelectManager(Player player, float DeltaTime)
    {
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if ((double) Game1.screenfade.fAlpha != 0.0)
        return;
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, false, false, player.inputmap.HeldButtons[18], player.inputmap.HeldButtons[19]))
      {
        switch (Direction)
        {
          case DirectionPressed.Right:
            if (this.SelectedIndex < 1)
            {
              ++this.SelectedIndex;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              break;
            }
            break;
          case DirectionPressed.Left:
            if (this.SelectedIndex > 0)
            {
              --this.SelectedIndex;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
              break;
            }
            break;
        }
      }
      int MouseOver = -1;
      bool flag = false;
      if (this.arcade.UpdateModeSelectIcon(ref MouseOver, player))
        flag = true;
      if (this.build.UpdateModeSelectIcon(ref MouseOver, player))
        flag = true;
      if (MouseOver != this.SelectedIndex && MouseOver > -1)
      {
        this.SelectedIndex = MouseOver;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
      }
      if (!(player.inputmap.PressedThisFrame[0] | flag))
        return;
      if (this.SelectedIndex == 1)
      {
        Game1.SetNextGameState(GAMESTATE.ArcadeModeSetUp);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        FeatureFlags.ResetFeatureFlagsForArcadeMode();
        FeatureFlags.BlockShake = true;
        GameFlags.IsArcadeMode = true;
      }
      else
      {
        FeatureFlags.BlockShake = false;
        GameFlags.IsArcadeMode = false;
        if (DebugFlags.ShowIhtro && PlayerStats.WillPlayIntro && !player.Stats.HasPickedCharacter)
        {
          Game1.SetNextGameState(GAMESTATE.CharacterSelectSetUp);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        }
        else
        {
          Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        }
      }
      Game1.screenfade.BeginFade(true);
    }

    public void DrawCharacterSelectManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      this.build.DrawModeSelectIcon(this.SelectedIndex == 0);
      this.arcade.DrawModeSelectIcon(this.SelectedIndex == 1);
    }
  }
}
