// Decompiled with JetBrains decompiler
// Type: TinyZoo.IAPScreen.IAPScreenManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SpringIAP;
using TinyZoo.AdvertPlayer;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.IAPScreen.Version2;
using TinyZoo.IAPScreen.Version2.SmallerButton;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir;
using TinyZoo.Utils;

namespace TinyZoo.IAPScreen
{
  internal class IAPScreenManager
  {
    private StoreBGManager storeBG;
    private CharacterTextBox charactertextbox;
    private IAPAdvertPanel iapadvertoanel;
    private IAPIAPPAnel iapiap;
    private BackButton backbuttn;
    private HCAdvertPlayer advertplayer;
    private IAPWaitManager iapwaitmanager;
    private SideButtons sidebuttons;
    private MainPanelman mainpanelmanager;
    private bool startRestore;
    internal static string RESTORE_IDENTIFIER = "RESTORE";

    public IAPScreenManager(Player player)
    {
      this.backbuttn = new BackButton();
      this.storeBG = new StoreBGManager(IsAutumnal: true);
      player.tracking.OpenedIAPScreen(player);
      this.sidebuttons = new SideButtons(player);
      this.mainpanelmanager = new MainPanelman(this.sidebuttons.SelectedButton, player);
      this.CreateNewTextBox(this.sidebuttons.SelectedButton, player);
      this.startRestore = false;
    }

    private void CreateNewTextBox(OfferButtonType buttontype, Player player)
    {
      string TextToSay = "To control the speed of time for the next ten minutes, please engage with a sponsor! Alternatively, buy your own time control device and never engage with a sponsor again!";
      switch (buttontype)
      {
        case OfferButtonType.WatchTheAdvert:
          if (player.Stats.ADisabled(true, player))
          {
            TextToSay = "Thanks for being my greatest customer!";
            break;
          }
          if (player.Stats.TimeTravelIsActiveFromAdvert())
          {
            TextToSay = "The Time Travel button is currently enabled - Go and press it, and pretend you are HG Wells, or someone more contemporary like Doctor Strange... who was first published in 1963, so probably not that contemporary after all.";
            break;
          }
          break;
        case OfferButtonType.BuyTheGoat:
          TextToSay = !player.Stats.ADisabled(true, player) ? "Permanently disable all adverts, have the time travel button always active, earn and extra 5% income, and get Space Goats and Tress to decorate your prison." : "Thanks for buying this, how are the goats doing? I hope you are looking after them, and feeding them well. I think I probably sold them to you for too little!";
          break;
        case OfferButtonType.TheVortexMind:
          TextToSay = !player.Stats.Vortex() ? "Permanently reduce all research time by 25%, add the Vortex Mind to your prison, and build a decorative trash compactor, I know - decorative & trash are not normally said together." : "You are the proud owner of the Vortex Mind! Congratulations, how does it feel to have the most intelligent being in the universe working for you?";
          break;
        case OfferButtonType.BuyTheFlower:
          TextToSay = !player.Stats.GetFlower() ? "The Time Travel button is currently enabled - Go and press it, and pretend you are HG Wells, or someone more contemporary like Doctor Strange... who was first published in 1963, so probably not that contemporary after all." : "Thanks for being my greatest customer!";
          break;
      }
      this.charactertextbox = new CharacterTextBox(AnimalType.ShopKeeper, TextToSay, Sengine.UltraWideSreenDownardsMultiplier);
    }

    public void UpdateIAPScreenManager(Player player, float DeltaTime)
    {
      if ((double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
        return;
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (this.advertplayer != null)
      {
        this.advertplayer.UpdateAdvertPlayer(DeltaTime);
        if (!this.advertplayer.IsWaiting)
        {
          if (this.advertplayer.WasSuccess)
          {
            this.advertplayer = (HCAdvertPlayer) null;
            player.tracking.WatchedAdvert(AdvertLocation.ActivateSpeedUp, player);
            player.Stats.EnableTimeTravel(player);
            player.OldSaveThisPlayer();
          }
          else
          {
            if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 && !player.inputmap.PressedThisFrame[0])
              return;
            this.advertplayer = (HCAdvertPlayer) null;
            this.charactertextbox = new CharacterTextBox(AnimalType.ShopKeeper, "Our sponsors seem to be letting us down, maybe try again later");
          }
        }
        else
        {
          if (!this.advertplayer.WasTimeout || (double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 && !player.inputmap.PressedThisFrame[0])
            return;
          this.advertplayer = (HCAdvertPlayer) null;
        }
      }
      else if (this.iapwaitmanager != null)
      {
        this.iapwaitmanager.UpdateIAPWaitManager(DeltaTime, player);
        if (this.iapwaitmanager.IsActive)
          return;
        if (this.iapwaitmanager.isDone)
        {
          IAPHolder.CheckPurchases(SpringIAPManager.Instance, player);
          this.iapwaitmanager = (IAPWaitManager) null;
        }
        else
        {
          if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0)
            return;
          if (this.iapwaitmanager.startRestore)
            this.startRestore = true;
          this.iapwaitmanager = (IAPWaitManager) null;
        }
      }
      else
      {
        if (this.advertplayer == null)
          IAPHolder.CheckPurchases(SpringIAPManager.Instance, player);
        this.charactertextbox.UpdateCharacterTextBox(DeltaTime);
        OfferButtonType btn = this.sidebuttons.UpdateSideButtons(player, DeltaTime);
        if (btn != OfferButtonType.Count)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
          this.CreateNewTextBox(this.sidebuttons.SelectedButton, player);
          this.mainpanelmanager = new MainPanelman(btn, player);
        }
        if (this.backbuttn.UpdateBackButton(player, DeltaTime))
        {
          player.inputmap.ClearAllInput(player);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
          if (TinyZoo.Game1.gamestate == GAMESTATE.IAPStore)
            TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
          else
            TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
          TinyZoo.Game1.screenfade.BeginFade(true);
        }
        if (this.startRestore)
        {
          this.startRestore = false;
          this.iapwaitmanager = new IAPWaitManager("restore", player);
        }
        else
        {
          if (!this.mainpanelmanager.UpdateMainPanelman(player, DeltaTime))
            return;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          switch (this.sidebuttons.SelectedButton)
          {
            case OfferButtonType.WatchTheAdvert:
              this.advertplayer = new HCAdvertPlayer(false, player);
              break;
            case OfferButtonType.BuyTheGoat:
              player.tracking.PressedBuyUnblockedAdverts(player);
              this.iapwaitmanager = new IAPWaitManager(IAPHolder.GetIAPIDentifier(IAPTYPE.DisableAdverts), player);
              break;
            case OfferButtonType.TheVortexMind:
              player.tracking.PressedBuyVortexMind(player);
              this.iapwaitmanager = new IAPWaitManager(IAPHolder.GetIAPIDentifier(IAPTYPE.BuyVortex), player);
              break;
            case OfferButtonType.BuyTheFlower:
              player.tracking.PressedBuyVortexMind(player);
              this.iapwaitmanager = new IAPWaitManager(IAPHolder.GetIAPIDentifier(IAPTYPE.BuyFlower), player);
              break;
          }
        }
      }
    }

    public void DrawIAPScreenManager()
    {
      this.storeBG.DrawStoreBGManager(Vector2.Zero);
      if (this.advertplayer != null)
        this.advertplayer.DrawAdvertPlayer();
      else if (this.iapwaitmanager != null)
      {
        this.iapwaitmanager.DrawIAPWaitManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      }
      else
      {
        this.sidebuttons.DrawSideButtons();
        this.mainpanelmanager.DrawMainPanelman();
        this.charactertextbox.Location = new Vector2(512f, 200f * Sengine.ScreenRationReductionMultiplier.Y);
        this.charactertextbox.DrawCharacterTextBox(Vector2.Zero, AssetContainer.pointspritebatchTop05);
        this.backbuttn.DrawBackButton(Vector2.Zero);
      }
    }
  }
}
