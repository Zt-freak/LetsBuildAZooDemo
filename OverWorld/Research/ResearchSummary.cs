// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Research.ResearchSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using TinyZoo.AdvertPlayer;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Research.ResOpt;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.Research
{
  internal class ResearchSummary
  {
    private StoreBGManager storeBG;
    private LerpHandler_Float lerper;
    private SmartCharacterBox charactertextbox;
    private BackButton backbutton;
    private ResearchTimer researchtimer;
    private ResearchOptionsPanel researchoptionspanel;
    private HCAdvertPlayer advertplayer;
    private TextButton ShortenWithAdvert;
    private GameObject Textt;
    private string TimeToAdvert;
    private bool HasWatchedAdvert;
    private WatchVideoButton watchadbutton;
    private TextButton Cancel;
    private GameObject VortextMind;
    private DualSinOscillator oscialltor;

    public ResearchSummary(Player player)
    {
      FeatureFlags.BlockStats = true;
      this.storeBG = new StoreBGManager();
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.backbutton = new BackButton();
      this.charactertextbox = new SmartCharacterBox(SEngine.Localization.Localization.GetText(42), AnimalType.Scientist);
      player.Stats.research.Vallidate();
      if (Researcher.IsCurrentlyResearching)
        this.researchtimer = new ResearchTimer(player);
      else
        this.researchoptionspanel = new ResearchOptionsPanel(player);
      this.TimeToAdvert = " ";
      if (!GameFlags.IsConsoleVersion)
      {
        this.ShortenWithAdvert = new TextButton("Reduce Time", 100f, OverAllMultiplier: 1.2f);
        this.ShortenWithAdvert.vLocation = new Vector2(750f, 700f);
        this.ShortenWithAdvert.CollisionEx = new Vector2(10f, 20f);
        this.ShortenWithAdvert.AddControllerButton(ControllerButton.XboxA);
      }
      if (!player.Stats.ADisabled(false, player))
      {
        this.watchadbutton = new WatchVideoButton("Speed Up");
        this.watchadbutton.AddControllerButton(ControllerButton.XboxA);
      }
      if (player.Stats.HasPlayedResearchAdvertToday())
      {
        this.HasWatchedAdvert = true;
        this.TimeToAdvert = string.Format(SEngine.Localization.Localization.GetText(362), (object) player.Stats.GetTimeUntilAdvertAvaialbel());
      }
      else
        this.HasWatchedAdvert = false;
      this.Textt = new GameObject();
      this.Textt.SetAllColours(ColourData.FernLemon);
      this.Textt.scale = 3f;
      this.Textt.vLocation = new Vector2(800f, 700f);
      if (!player.Stats.Vortex())
        return;
      this.oscialltor = new DualSinOscillator(0.4f, 0.38f);
      this.VortextMind = new GameObject();
      this.VortextMind.DrawRect = new Rectangle(589, 391, 93, 82);
    }

    private void CreateCancel()
    {
    }

    public bool UpdateResearchSummary(float DeltaTime, Player player)
    {
      if (this.oscialltor != null)
        this.oscialltor.UpdateDualSinOscillator(DeltaTime);
      if (this.advertplayer != null)
      {
        this.advertplayer.UpdateAdvertPlayer(DeltaTime);
        if (!this.advertplayer.IsWaiting)
        {
          if (this.advertplayer.WasSuccess)
          {
            this.advertplayer = (HCAdvertPlayer) null;
            player.tracking.WatchedAdvert(AdvertLocation.SpeedUpResearch, player);
            player.Stats.ReduceFromAdvert(player);
          }
          else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0])
          {
            this.advertplayer = (HCAdvertPlayer) null;
            this.charactertextbox = new SmartCharacterBox("Our sponsors seem to be letting us down, maybe try again later.", AnimalType.Administrator);
          }
        }
        else if (this.advertplayer.WasTimeout && ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0]))
          this.advertplayer = (HCAdvertPlayer) null;
        return false;
      }
      this.charactertextbox.UpdateSmartCharacterBox(DeltaTime, player, true);
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      if (this.researchtimer != null && !Researcher.IsCurrentlyResearching)
      {
        this.researchtimer = (ResearchTimer) null;
        this.researchoptionspanel = new ResearchOptionsPanel(player);
      }
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      if (this.researchoptionspanel != null)
      {
        if (this.researchoptionspanel.UpdateResearchOptionsPanel(player, DeltaTime, Offset))
        {
          this.researchtimer = new ResearchTimer(player);
          this.researchoptionspanel = (ResearchOptionsPanel) null;
        }
      }
      else if (this.researchtimer != null)
      {
        this.researchtimer.UpdateResearchTimer(DeltaTime, player);
        if (this.Cancel != null)
          this.Cancel.UpdateTextButton(player, Offset, DeltaTime);
        if (this.ShortenWithAdvert != null)
        {
          if (!this.HasWatchedAdvert && player.Stats.HasPlayedResearchAdvertToday())
          {
            this.HasWatchedAdvert = true;
            this.ShortenWithAdvert = (TextButton) null;
          }
          if (!this.researchtimer.IsReadyToClaim() && !this.HasWatchedAdvert)
          {
            if (this.watchadbutton != null)
            {
              if (this.watchadbutton.UpdateWatchVideoButton(player, Offset + this.ShortenWithAdvert.vLocation, GameFlags.IsUsingController))
                this.advertplayer = new HCAdvertPlayer(false, player);
            }
            else if (this.ShortenWithAdvert.UpdateTextButton(player, Offset, DeltaTime))
            {
              if (player.Stats.ADisabled(false, player))
                player.Stats.ReduceFromAdvert(player);
              else
                this.advertplayer = new HCAdvertPlayer(false, player);
            }
          }
        }
      }
      if (this.HasWatchedAdvert)
      {
        if (player.Stats.HasPlayedResearchAdvertToday())
          this.TimeToAdvert = string.Format(SEngine.Localization.Localization.GetText(362), (object) player.Stats.GetTimeUntilAdvertAvaialbel());
        else
          this.HasWatchedAdvert = false;
      }
      if ((double) this.lerper.Value == 0.0 && TinyZoo.Game1.GetNextGameState() != GAMESTATE.RewardSetUp && this.backbutton.UpdateBackButton(player, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
      }
      return (double) this.lerper.TargetValue == 1.0 && (double) this.lerper.Value == 1.0;
    }

    public void DrawResearchSummary()
    {
      Vector2 Offset = new Vector2(this.lerper.Value * 1024f, 0.0f);
      this.storeBG.DrawStoreBGManager(Offset);
      this.charactertextbox.DrawSmartCharacterBox(Offset + new Vector2(0.0f, 70f));
      if ((double) this.lerper.Value == 0.0)
        this.backbutton.DrawBackButton(new Vector2(this.lerper.Value * 1024f, 0.0f));
      if (this.researchoptionspanel != null)
        this.researchoptionspanel.DrawResearchOptionsPanel(Offset);
      else if (this.researchtimer != null)
      {
        if (this.Cancel != null)
          this.Cancel.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
        if (this.VortextMind != null)
        {
          this.VortextMind.scale = 2f;
          this.VortextMind.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset + new Vector2(700f, 450f) + this.oscialltor.CurrentOffset * 10f);
        }
        this.researchtimer.DrawResearchTimer(Offset);
        if (this.ShortenWithAdvert != null && !this.HasWatchedAdvert)
        {
          if (!this.researchtimer.IsReadyToClaim())
          {
            if (this.watchadbutton != null)
              this.watchadbutton.DrawWatchVideoButton(Offset + this.ShortenWithAdvert.vLocation, GameFlags.IsUsingController);
            else
              this.ShortenWithAdvert.DrawTextButton(Offset, 1f, AssetContainer.pointspritebatchTop05);
          }
        }
        else
          TextFunctions.DrawJustifiedText(this.TimeToAdvert, this.Textt.scale, this.Textt.vLocation + Offset, this.Textt.GetColour(), 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      }
      if (this.advertplayer == null)
        return;
      this.advertplayer.DrawAdvertPlayer();
    }
  }
}
