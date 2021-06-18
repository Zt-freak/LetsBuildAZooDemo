// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.PauseScreen.PauseScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.Z_Pause;
using TinyZoo.Z_Pause.Elements;

namespace TinyZoo.GamePlay.PauseScreen
{
  internal class PauseScreenManager
  {
    private PausePanel pausePanel;
    private bool Exiting;
    public bool WillRetry;
    private BlackOut blackout;
    private bool WillFullQuitGameOnMobile;
    private bool IsOverWorldMenu;

    public PauseScreenManager(Player player, bool _IsOverWorldMenu = false, bool _WillFullQuitGameOnMobile = false)
    {
      this.WillFullQuitGameOnMobile = _WillFullQuitGameOnMobile;
      this.IsOverWorldMenu = _IsOverWorldMenu;
      this.WillRetry = false;
      this.blackout = new BlackOut();
      this.blackout.SetAlpha(false, 0.2f, 0.0f, 0.8f);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      this.pausePanel = new PausePanel();
      this.pausePanel.location = new Vector2(512f, 384f);
    }

    public bool UpdatePauseScreenManager(float DeltaTime, Player player)
    {
      this.blackout.UpdateColours(DeltaTime);
      PauseScreenButton pauseScreenButton = this.pausePanel.UpdatePausePanel(player, DeltaTime, Vector2.Zero);
      if (pauseScreenButton != PauseScreenButton.Count && !this.Exiting)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        switch (pauseScreenButton)
        {
          case PauseScreenButton.Quit:
            GameVariables.QuitNextFrame = true;
            break;
          case PauseScreenButton.Camera:
            PhotoModeHelper.EnterPhotoMode();
            return true;
        }
        this.Exiting = true;
        this.pausePanel.LerpOff();
        this.blackout.SetAlpha(true, 0.2f, 1f, 0.0f);
      }
      if (Game1.gamestate != GAMESTATE.OverWorld)
        player.inputmap.ClearAllInput(player);
      return this.Exiting && (double) this.blackout.fAlpha == 0.0;
    }

    public void DrawPauseScreenManager()
    {
      this.blackout.DrawBlackOut(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      this.pausePanel.DrawPausePanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }
  }
}
