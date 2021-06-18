// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Settings.SettingsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SpringIAP;
using TinyZoo.Audio;
using TinyZoo.CollectionScreen;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Settings;
using TinyZoo.Settings.Credits;
using TinyZoo.Settings.LanguageSelect;
using TinyZoo.Settings.Manual;
using TinyZoo.Settings.Offer;
using TinyZoo.Settings.Restore;
using TinyZoo.Settings.Sound;
using TinyZoo.SpringUI;
using TinyZoo.Z_Settings.Z_Debug;

namespace TinyZoo.OverWorld.Settings
{
  internal class SettingsManager
  {
    private StoreBGManager storeBG;
    private SettingsMainButtons settingsmainbuttons;
    private BackButton back;
    private SoundManager soundmanager;
    private CreditsManager creditsmanager;
    private CollectionScreenManager collectionanager;
    private ManualManager manualmanager;
    private SpringUIManager springUImanager;
    private RestoreManager restoreManager;
    private SetDrone drone;
    private DebugMenu debugmenu;
    private SignInOffer signinoffer;
    private LanSelectManager languageselectmanager;

    public SettingsManager(Player player)
    {
      SpringIAPManager.Instance.RetryAllUnconsumedPurchases(player.iapuser);
      this.storeBG = new StoreBGManager();
      this.settingsmainbuttons = new SettingsMainButtons();
      this.back = new BackButton();
      this.drone = new SetDrone();
      this.drone.vLocation = new Vector2(300f, 300f);
      this.MakeScreen(player);
    }

    private void MakeScreen(Player player)
    {
      this.soundmanager = new SoundManager(player);
      this.back = new BackButton();
      this.settingsmainbuttons = new SettingsMainButtons();
      if (player.Stats.TDDLink || GameFlags.IsConsoleVersion)
        return;
      this.signinoffer = new SignInOffer(player);
    }

    public void UpdateSettingsManager(
      float DeltaTime,
      Player player,
      GraphicsDeviceManager graphics,
      GraphicsDevice _GraphicsDevice)
    {
      Z_GameFlags.BlockPointer = true;
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      bool StartedExit = false;
      this.drone.UpdateDrone(DeltaTime);
      if (this.signinoffer != null)
        this.signinoffer.UpdateSignInOffer(DeltaTime);
      if (this.soundmanager == null && this.debugmenu == null && (this.creditsmanager == null && this.restoreManager == null) && (this.languageselectmanager == null && this.manualmanager == null && (this.collectionanager == null && this.springUImanager == null)) && this.settingsmainbuttons.UpdateSettingsMainButtons(player, DeltaTime, out StartedExit))
      {
        if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.Sound)
        {
          this.soundmanager = new SoundManager(player);
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.Credits)
        {
          this.creditsmanager = new CreditsManager();
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.Manual)
        {
          this.manualmanager = new ManualManager();
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.Collection)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuOpen);
          this.collectionanager = new CollectionScreenManager(player);
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.Language)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuOpen);
          this.languageselectmanager = new LanSelectManager(player);
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.SignIn)
        {
          this.springUImanager = new SpringUIManager(player);
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.DebugMenu)
        {
          this.debugmenu = new DebugMenu();
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.RestorePurchases)
        {
          this.restoreManager = new RestoreManager(player);
          if (this.signinoffer != null)
            this.signinoffer.Exit();
        }
        else if (this.settingsmainbuttons.settingss[this.settingsmainbuttons.Selected] == SettingsButton.PhotoMode && TinyZoo.Game1.GetNextGameState() != GAMESTATE.OverWorldSetUp)
        {
          StartedExit = false;
          TinyZoo.Game1.screenfade.BeginFade(true);
          if (Z_GameFlags.SettingWasFromTitleScreen)
          {
            Z_GameFlags.SettingWasFromTitleScreen = false;
            TinyZoo.Game1.SetNextGameState(GAMESTATE.TitleScreen);
          }
          else
            TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
        }
      }
      if (StartedExit)
        this.back.Exit();
      if (this.soundmanager != null)
      {
        if (!this.soundmanager.UpdateSoundManager(player, DeltaTime, graphics, _GraphicsDevice))
          return;
        this.soundmanager = (SoundManager) null;
        this.MakeScreen(player);
      }
      else if (this.creditsmanager != null)
      {
        this.drone.Exit();
        if (!this.creditsmanager.UpdateCreditsManager(player, DeltaTime))
          return;
        this.creditsmanager = (CreditsManager) null;
        this.MakeScreen(player);
      }
      else if (this.manualmanager != null)
      {
        this.drone.Exit();
        if (!this.manualmanager.UpdateManualManager(player, DeltaTime))
          return;
        this.manualmanager = (ManualManager) null;
        this.MakeScreen(player);
      }
      else if (this.restoreManager != null)
      {
        if (!this.restoreManager.UpdateRestoreManager(DeltaTime, player))
          return;
        this.restoreManager = (RestoreManager) null;
        this.MakeScreen(player);
      }
      else if (this.collectionanager != null)
      {
        this.drone.Exit();
        bool ExitDone;
        int num = (int) this.collectionanager.UpdateCollectionScreenManager(Vector2.Zero, DeltaTime, player, out ExitDone, out bool _);
        if (!ExitDone)
          return;
        this.collectionanager = (CollectionScreenManager) null;
        this.MakeScreen(player);
      }
      else if (this.springUImanager != null)
      {
        if (!this.springUImanager.UpdateSpringUI(player, DeltaTime))
          return;
        this.springUImanager = (SpringUIManager) null;
        this.MakeScreen(player);
      }
      else if (this.languageselectmanager != null)
      {
        this.drone.Exit();
        if (!this.languageselectmanager.UpdateLanguageSelectManager(player, DeltaTime))
          return;
        this.languageselectmanager = (LanSelectManager) null;
        this.MakeScreen(player);
      }
      else if (this.debugmenu != null)
      {
        if (!this.debugmenu.UpdateDebugMenu(player, DeltaTime))
          return;
        this.debugmenu = (DebugMenu) null;
        this.MakeScreen(player);
      }
      else
      {
        if (!this.back.UpdateBackButton(player, DeltaTime))
          return;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        TinyZoo.Game1.screenfade.BeginFade(true);
        if (Z_GameFlags.SettingWasFromTitleScreen)
        {
          Z_GameFlags.SettingWasFromTitleScreen = false;
          TinyZoo.Game1.SetNextGameState(GAMESTATE.TitleScreen);
        }
        else
          TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
      }
    }

    public void DrawSettingsManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      if (this.signinoffer != null)
      {
        double y = (double) Sengine.ScreenRationReductionMultiplier.Y;
      }
      SignInOffer signinoffer = this.signinoffer;
      this.settingsmainbuttons.DrawSettingsMainButtons();
      if (this.soundmanager != null)
        this.soundmanager.DrawSoundManager();
      else if (this.creditsmanager != null)
        this.creditsmanager.DrawCreditsManager();
      else if (this.manualmanager != null)
        this.manualmanager.DrawManualManager();
      else if (this.collectionanager != null)
        this.collectionanager.DrawCollectionScreenManager(Vector2.Zero);
      else if (this.springUImanager != null)
        this.springUImanager.DrawSpringUIManager(Vector2.Zero);
      else if (this.restoreManager != null)
        this.restoreManager.DrawRestoreManager();
      else if (this.debugmenu != null)
        this.debugmenu.DrawDebugMenu();
      else if (this.languageselectmanager != null)
        this.languageselectmanager.DrawLanguageSelectManager();
      else
        this.back.DrawBackButton(Vector2.Zero);
    }
  }
}
