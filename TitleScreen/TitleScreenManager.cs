// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.TitleScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.TitleScreen.MainButtons;
using TinyZoo.TitleScreen.PickSaveSlot;
using TinyZoo.Z_Save.CreateNewGame;
using TinyZoo.Z_TitleScreen.NewsFeed;
using TinyZoo.Z_TitleScreen.NewsFeed.DataManage;

namespace TinyZoo.TitleScreen
{
  internal class TitleScreenManager
  {
    private TitleImage BGO;
    private TitleSCreenState titlescreenstete;
    private GameLogo Logo;
    private float BaseScale;
    private MainButtonManager mainbuttonmanager;
    private SaveSlotSelectionManager saveslotselectionmanager;
    private BetaButtons BetaButtons;
    private NewsFeedManager newsfeedmanager;

    public TitleScreenManager()
    {
      this.BGO = new TitleImage();
      this.Logo = new GameLogo();
      this.Logo.Logo.vLocation = new Vector2(860f, 620f);
      S_Directory.ClearCache(TinyZoo.Game1.News_ContentManager, "News");
      this.titlescreenstete = TitleSCreenState.MainButtons;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.mainbuttonmanager = new MainButtonManager(this.BaseScale);
      MusicManager.playsong(SongTitle.TrailerV2Fade, true);
      this.newsfeedmanager = new NewsFeedManager(TinyZoo.Game1.News_ContentManager);
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      this.BetaButtons = new BetaButtons();
    }

    public void UpdateTitleScreenManager(
      ref Player[] player,
      float DeltaTime,
      ContentManager contentmanager)
    {
      double fAlpha = (double) TinyZoo.Game1.screenfade.fAlpha;
      if (Z_DebugFlags.IsBetaVersion)
        this.BetaButtons.UpdateBetaButtons(player[0], DeltaTime);
      else
        this.newsfeedmanager.UpdateNewsFeedManager(contentmanager, player[0], DeltaTime);
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return;
      int titlescreenstete = (int) this.titlescreenstete;
      if (this.titlescreenstete == TitleSCreenState.MainButtons)
      {
        MainMenuButton mainMenuButton = this.mainbuttonmanager.UpdateMainButtonManager(player[0], DeltaTime);
        if (mainMenuButton == MainMenuButton.Count)
          return;
        switch (mainMenuButton)
        {
          case MainMenuButton.ContinueGame:
            if (DebugFlags.LoadGame && Player.financialrecords.GetDaysPassed() > 0L)
            {
              TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
              TinyZoo.Game1.screenfade.BeginFade(true);
              break;
            }
            TinyZoo.Game1.SetNextGameState(GAMESTATE.CharacterSelectSetUp);
            TinyZoo.Game1.screenfade.BeginFade(true);
            break;
          case MainMenuButton.StartGame:
            TinyZoo.Game1.SetNextGameState(GAMESTATE.CharacterSelectSetUp);
            TinyZoo.Game1.screenfade.BeginFade(true);
            if (!Z_GameFlags.DidLoadSave)
              break;
            NewGameCreator.CreateNewGame(ref player[0]);
            break;
          case MainMenuButton.Settings:
            Z_GameFlags.SettingWasFromTitleScreen = true;
            TinyZoo.Game1.SetNextGameState(GAMESTATE.SettingsSetUp);
            TinyZoo.Game1.screenfade.BeginFade(true);
            break;
          case MainMenuButton.Quit:
            GameVariables.QuitNextFrame = true;
            break;
          case MainMenuButton.LoadGame:
            this.saveslotselectionmanager = new SaveSlotSelectionManager(this.BaseScale);
            this.titlescreenstete = TitleSCreenState.LoadGame;
            break;
        }
      }
      else
      {
        if (this.titlescreenstete != TitleSCreenState.LoadGame || !this.saveslotselectionmanager.UpdateSaveSlotSelectionManager(player[0], DeltaTime))
          return;
        this.titlescreenstete = TitleSCreenState.MainButtons;
        this.mainbuttonmanager = new MainButtonManager(this.BaseScale);
      }
    }

    public void DrawTitleScreenManager()
    {
      this.BGO.Draw(AssetContainer.spritebacth, AssetContainer.TitleScreen);
      TextFunctions.DrawTextWithDropShadow("Build: " + TinyZoo.Game1.BUILDNUMBER, this.BaseScale, new Vector2(1020f, 740f), Color.White, 1f, AssetContainer.SpringFontX1AndHalf, AssetContainer.pointspritebatch07Final, false, true);
      if (Z_DebugFlags.IsBetaVersion)
        this.BetaButtons.DrawBetaButtons();
      else
        this.newsfeedmanager.DrawNewsFeedManager();
      switch (this.titlescreenstete)
      {
        case TitleSCreenState.MainButtons:
          this.mainbuttonmanager.DrawMainButtonManager();
          break;
        case TitleSCreenState.LoadGame:
          this.saveslotselectionmanager.DrawSaveSlotSelectionManager();
          break;
        case TitleSCreenState.Loading:
          this.Logo.DrawGameLogo(Vector2.Zero);
          break;
      }
    }
  }
}
