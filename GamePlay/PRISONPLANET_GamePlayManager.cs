// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.PRISONPLANET_GamePlayManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.AdvertPlayer;
using TinyZoo.ArcadeMode;
using TinyZoo.Audio;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.Dungeon;
using TinyZoo.GamePlay.Effects;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.HUD;
using TinyZoo.GamePlay.Intro;
using TinyZoo.GamePlay.PauseScreen;
using TinyZoo.GamePlay.Progress;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.GamePlay.Results;
using TinyZoo.GamePlay.Ships;
using TinyZoo.GamePlay.VirtualJoystick;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tutorials;

namespace TinyZoo.GamePlay
{
  internal class PRISONPLANET_GamePlayManager
  {
    private WorldCamera worldcamera;
    private ShipManager shipmanager;
    private DungeonManager dungeonmanager;
    internal static BeamManager beammanager;
    private EnemyManager enemyrenderer;
    private BoxZoneManager boxzonemanager;
    private EffectsManager effectsmanager;
    private HUDManager hudmanager;
    private ProgressSystem progresssytem;
    internal static LockDownIntro lockdownintro;
    private ResultsManager resultsmanager;
    private VirtualStickManager virtualstick;
    private HCAdvertPlayer advertplayer;
    private bool WatchingAdvertForRetry;
    private PauseManager pausemanager;

    public PRISONPLANET_GamePlayManager(Player player)
    {
      player.tracking.StartedGame(player);
      this.pausemanager = new PauseManager();
      this.WatchingAdvertForRetry = false;
      Game1.ClsCLR.SetAllColours(0.0f, 0.5f, 0.5f);
      PRISONPLANET_GamePlayManager.lockdownintro = new LockDownIntro(TYPENAMETHING.Intro);
      GameStateManager.tutorialmanager.StartedGamePlay(player);
      this.dungeonmanager = new DungeonManager(player);
      this.virtualstick = new VirtualStickManager();
      GameFlags.BeamInventoryAtStart = GameFlags.CurrentBeamInventory;
      if (GameFlags.IsArcadeMode)
      {
        GameFlags.BeamInventoryAtStart = ArcadeData.GetBeamsForThisLevel();
        GameFlags.CurrentBeamInventory = GameFlags.BeamInventoryAtStart;
      }
      if (GameFlags.BountyMode)
      {
        GameFlags.BeamInventoryAtStart = 1;
        GameFlags.CurrentBeamInventory = GameFlags.BeamInventoryAtStart;
      }
      GameFlags.BeamsLockedOrDead = 0;
      this.shipmanager = new ShipManager(1);
      this.worldcamera = new WorldCamera();
      PRISONPLANET_GamePlayManager.beammanager = new BeamManager(player);
      this.boxzonemanager = new BoxZoneManager();
      this.enemyrenderer = !GameFlags.IsArcadeMode ? new EnemyManager(player.livestats.SelectedPrisonID, this.boxzonemanager, player.livestats.waveinfo, player) : new EnemyManager(-1, this.boxzonemanager, ArcadeData.GetWaveInfo(), player);
      this.enemyrenderer.AddEnemiesFromCell(player.livestats.SelectedPrisonID + 667, player.livestats.waveinfoFromPrison, this.boxzonemanager);
      this.effectsmanager = new EffectsManager();
      this.hudmanager = new HUDManager();
      this.progresssytem = new ProgressSystem(GameFlags.CrrentStage);
      this.resultsmanager = new ResultsManager();
      MusicManager.BeginFadeOut(0.5f, SongTitle.RandomBattleMusic);
      if (player.livestats.intakefornextlevel == null)
        return;
      player.intakes.UseThis(player.livestats.intakefornextlevel, player);
      player.livestats.intakefornextlevel = (IntakeInfo) null;
    }

    public void UpdateGamePlayManager(Player[] players, float DeltaTime)
    {
      if (this.WatchingAdvertForRetry)
      {
        this.advertplayer.UpdateAdvertPlayer(DeltaTime);
        if (!this.advertplayer.IsWaiting)
        {
          if (this.advertplayer.WasSuccess)
          {
            if (GameFlags.EnemyCount == 0)
              players[0].tracking.WatchedAdvert(AdvertLocation.RetryOnFullDeath, players[0]);
            else
              players[0].tracking.WatchedAdvert(AdvertLocation.RetryOnPartialDeath, players[0]);
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
            players[0].livestats.ResetWaveForRetry();
            Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
            Game1.screenfade.BeginFade(true);
            this.advertplayer = (HCAdvertPlayer) null;
          }
          else if ((double) players[0].player.touchinput.ReleaseTapArray[0].X > 0.0 || players[0].inputmap.PressedThisFrame[0])
            this.advertplayer = (HCAdvertPlayer) null;
        }
        else if (this.advertplayer.WasTimeout && ((double) players[0].player.touchinput.ReleaseTapArray[0].X > 0.0 || players[0].inputmap.PressedThisFrame[0]))
          this.advertplayer = (HCAdvertPlayer) null;
        this.WatchingAdvertForRetry = this.advertplayer != null;
      }
      else
      {
        if (PRISONPLANET_GamePlayManager.lockdownintro != null)
        {
          if (PRISONPLANET_GamePlayManager.lockdownintro.UpdateLockDownIntro(DeltaTime, players[0]))
            PRISONPLANET_GamePlayManager.lockdownintro = (LockDownIntro) null;
          else
            DeltaTime = 0.0f;
        }
        if (this.pausemanager.UpdatePauseManager(ref DeltaTime, players[0], PRISONPLANET_GamePlayManager.lockdownintro != null || this.resultsmanager.IsActive))
        {
          players[0].tracking.RetriedAStage(true, GameResult.None, players[0]);
          if (players[0].Stats.ADisabled(false, players[0]))
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
            players[0].livestats.ResetWaveForRetry();
            Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
            Game1.screenfade.BeginFade(true);
          }
          else
          {
            this.WatchingAdvertForRetry = true;
            this.advertplayer = new HCAdvertPlayer(false, players[0]);
            return;
          }
        }
        float SimulationTime = 0.0f;
        GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref SimulationTime, players[0]);
        this.progresssytem.UpdateProgressSystem();
        float DeltaTime1 = DeltaTime;
        if (this.hudmanager.UpdateHUDManager(DeltaTime, players[0], this.shipmanager.ships[0].BeamFiring, PRISONPLANET_GamePlayManager.lockdownintro != null || this.resultsmanager.IsActive))
        {
          if (GameFlags.IsArcadeMode)
            DeltaTime1 *= 4f;
          else
            this.enemyrenderer.DoShuffle();
        }
        this.virtualstick.UpdateVirtualStickManager(players[0], GameFlags.RefDeltaTime);
        if (PRISONPLANET_GamePlayManager.lockdownintro == null)
          this.shipmanager.UpdateShipManager(players, DeltaTime, PRISONPLANET_GamePlayManager.beammanager, this.progresssytem.IsGoingToNextLevel || this.resultsmanager.IsActive, this.boxzonemanager);
        this.worldcamera.UpateDrag(players[0].player.touchinput.DragVectorThisFrame);
        this.worldcamera.UpdateWorldCamera(DeltaTime);
        this.dungeonmanager.UpdateDungeonManager(DeltaTime);
        this.enemyrenderer.UpdateEnemyManager(DeltaTime1, this.boxzonemanager);
        if (PRISONPLANET_GamePlayManager.beammanager.UpdateBeamManager(DeltaTime, this.boxzonemanager))
          this.enemyrenderer.ScrubBoxZones(this.boxzonemanager);
        this.boxzonemanager.UpdateBoxZoneManager(DeltaTime);
        this.effectsmanager.UpdateEffectsManager(DeltaTime);
        bool StartAdvertForRetry = false;
        this.resultsmanager.UpdateResultsManager(DeltaTime, players, PRISONPLANET_GamePlayManager.beammanager, this.enemyrenderer, out StartAdvertForRetry);
        if (StartAdvertForRetry)
        {
          players[0].tracking.RetriedAStage(false, this.resultsmanager.resulttt, players[0]);
          if (players[0].livestats.skp || players[0].Stats.ADisabled(false, players[0]))
          {
            players[0].livestats.skp = false;
            players[0].livestats.ResetWaveForRetry();
            Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
            Game1.screenfade.BeginFade(true);
          }
          else
          {
            this.WatchingAdvertForRetry = true;
            this.advertplayer = new HCAdvertPlayer(false, players[0]);
          }
        }
        else if (!GameFlags.IsArcadeMode)
          throw new Exception("NOPE");
      }
    }

    public void DrawGamePlayManager()
    {
      if (this.WatchingAdvertForRetry)
      {
        this.advertplayer.DrawAdvertPlayer();
      }
      else
      {
        this.dungeonmanager.DrawDungeonManager();
        this.boxzonemanager.DrawBoxZoneManager();
        PRISONPLANET_GamePlayManager.beammanager.DrawBeamManager();
        this.enemyrenderer.DrawEnemyManager();
        this.shipmanager.DrawShipManager();
        this.effectsmanager.DrawEffectsManager();
        this.hudmanager.DrawHUDManager(PRISONPLANET_GamePlayManager.lockdownintro != null);
        this.resultsmanager.DrawResultsManager();
        if (PRISONPLANET_GamePlayManager.lockdownintro != null)
          PRISONPLANET_GamePlayManager.lockdownintro.DrawLockDownIntro();
        if (!this.resultsmanager.IsActive && TutorialManager.currenttutorial == TUTORIALTYPE.None)
          this.virtualstick.DrawVirtualStickManager();
        this.pausemanager.DrawPauseManager();
      }
    }
  }
}
