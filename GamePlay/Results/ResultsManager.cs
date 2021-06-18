// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Results.ResultsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.Intro;
using TinyZoo.GamePlay.Results.RestryorContinue;
using TinyZoo.Tutorials;

namespace TinyZoo.GamePlay.Results
{
  internal class ResultsManager
  {
    public GameResult resulttt;
    private GameObject Back;
    private LerpHandler_Float lerper;
    public bool IsActive;
    private string TEMPTEXT;
    private LockDownIntro lockdownresults;
    private RetryCOnt retry;
    private float Delay;

    public ResultsManager()
    {
      this.resulttt = GameResult.None;
      this.Back = new GameObject();
      this.Back.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.Back.scale = 1024f;
      this.Back.SetAllColours(0.0f, 0.0f, 0.0f);
      this.Back.SetAlpha(0.5f);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.IsActive = false;
      this.Delay = 1f;
    }

    public void UpdateResultsManager(
      float DeltaTime,
      Player[] players,
      BeamManager beammanager,
      EnemyManager enemyrenderer,
      out bool StartAdvertForRetry)
    {
      StartAdvertForRetry = false;
      int num1 = 0;
      if (GameFlags.IsArcadeMode && GameFlags.DifficultyIsEasy && GameFlags.ArcadeLevel < 3)
        num1 = 1;
      if (!this.IsActive)
      {
        if (GameFlags.EnemyCount == 0)
        {
          this.retry = new RetryCOnt(false, players[0], false, TYPENAMETHING.Dead);
          this.IsActive = true;
          this.TEMPTEXT = "ALL PRISONERS LOST";
          this.resulttt = GameResult.PeopleDead;
          this.lockdownresults = new LockDownIntro(TYPENAMETHING.Dead);
        }
        else if (GameFlags.CurrentReclamedZones / GameFlags.FullZoneSize >= (Decimal) GameFlags.TargetPercent)
        {
          bool WasWin = true;
          TYPENAMETHING result = TYPENAMETHING.Win;
          if (GameFlags.IsArcadeMode && GameFlags.EnemyCount < GameFlags.EnemyCountAtStart - num1)
          {
            result = TYPENAMETHING.ArcadeFail;
            WasWin = false;
          }
          this.retry = new RetryCOnt(WasWin, players[0], false, result, GameFlags.EnemyCount < GameFlags.EnemyCountAtStart);
          this.IsActive = true;
          this.TEMPTEXT = "PRISONERS SECURE";
          this.resulttt = GameResult.Win;
          if (GameFlags.IsArcadeMode && GameFlags.EnemyCount < GameFlags.EnemyCountAtStart - num1)
          {
            this.lockdownresults = new LockDownIntro(TYPENAMETHING.ArcadeFail);
            this.resulttt = GameResult.ArcadeFail;
          }
          else
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.Menu_Splash);
            this.lockdownresults = new LockDownIntro(TYPENAMETHING.Win);
          }
        }
        else if (GameFlags.CurrentBeamInventory == 0 && !beammanager.AreAnyBeamsActive() && !GameFlags.BountyMode)
        {
          this.retry = new RetryCOnt(false, players[0], true, TYPENAMETHING.NoBeam);
          this.IsActive = true;
          this.TEMPTEXT = "NO BEAMS LEFT - PRISONERS ESCAPED";
          this.resulttt = GameResult.NoBombs;
          this.lockdownresults = new LockDownIntro(TYPENAMETHING.NoBeam);
        }
        else if (GameFlags.IsArcadeMode && GameFlags.EnemyCount < GameFlags.EnemyCountAtStart - num1)
        {
          this.retry = new RetryCOnt(false, players[0], true, TYPENAMETHING.ArcadeFail);
          this.IsActive = true;
          this.TEMPTEXT = "FAIL";
          this.resulttt = GameResult.ArcadeFail;
          this.lockdownresults = new LockDownIntro(TYPENAMETHING.ArcadeFail);
        }
        int num2 = this.IsActive ? 1 : 0;
      }
      else
      {
        if (TutorialManager.currenttutorial == TUTORIALTYPE.GamePlayIntro)
          return;
        if ((double) this.Delay > 0.0)
        {
          this.Delay -= DeltaTime;
        }
        else
        {
          this.lerper.UpdateLerpHandler(DeltaTime);
          this.lockdownresults.UpdateLockDownIntro(DeltaTime, (Player) null);
          bool ContinueNoe;
          this.retry.UpdateRetryCOnt(DeltaTime, players[0], out ContinueNoe, out StartAdvertForRetry);
          if ((double) this.lerper.Value != 0.0 || !ContinueNoe || (double) TinyZoo.Game1.screenfade.fAlpha != 0.0)
            return;
          if (GameFlags.IsArcadeMode)
          {
            if (this.resulttt == GameResult.Win && GameFlags.EnemyCount >= GameFlags.EnemyCountAtStart - num1 && players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] > -1)
            {
              if (GameFlags.DifficultyIsEasy && players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] == 0)
              {
                bool flag = true;
                for (int index = 0; index < GameFlags.TotalArcadeLevels; ++index)
                {
                  if (players[0].Stats.ArcadeProgress[index] < 1 && index != GameFlags.ArcadeLevel)
                    flag = false;
                }
                if (flag && players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] < 1)
                  players[0].livestats.ArcadeModeJustCompleted = true;
                players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] = 1;
              }
              else if (!GameFlags.DifficultyIsEasy)
              {
                bool flag = true;
                for (int index = 0; index < GameFlags.TotalArcadeLevels; ++index)
                {
                  if (players[0].Stats.ArcadeProgress[index] < 2 && index != GameFlags.ArcadeLevel)
                    flag = false;
                }
                if (flag && (players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] < 2 || GameFlags.ArcadeLevel == GameFlags.TotalArcadeLevels - 1))
                  players[0].livestats.ArcadeModeJustCompleted = true;
                players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel] = 2;
              }
              if (GameFlags.ArcadeLevel < 50 && players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel + 1] == -1)
                players[0].Stats.ArcadeProgress[GameFlags.ArcadeLevel + 1] = 0;
            }
            players[0].OldSaveThisPlayer();
            TinyZoo.Game1.screenfade.BeginFade(true);
            if (players[0].livestats.ArcadeModeJustCompleted)
              TinyZoo.Game1.SetNextGameState(GAMESTATE.ArcadeCreditsSetUp);
            else
              TinyZoo.Game1.SetNextGameState(GAMESTATE.ArcadeModeSetUp);
          }
          else if (GameFlags.BountyMode)
          {
            players[0].OldSaveThisPlayer();
            TinyZoo.Game1.screenfade.BeginFade(true);
            if (GameFlags.EnemyCount == 0)
            {
              players[0].livestats.GaveUpBounty = true;
              players[0].livestats.WasNotPerfectButFinishedBounty = false;
            }
            else if (GameFlags.EnemyCount < GameFlags.EnemyCountAtStart)
            {
              players[0].livestats.GaveUpBounty = false;
              players[0].livestats.WasNotPerfectButFinishedBounty = true;
            }
            else
            {
              players[0].livestats.GaveUpBounty = false;
              players[0].livestats.WasNotPerfectButFinishedBounty = false;
            }
            players[0].livestats.waveinfo = (WaveInfo) null;
            TinyZoo.Game1.SetNextGameState(GAMESTATE.BountyResultsSetUp);
          }
          else
          {
            if (this.resulttt != GameResult.NoBombs)
              enemyrenderer.RemoveDeadPeopleFromPrison(players[0]);
            TinyZoo.Game1.screenfade.BeginFade(true);
            TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorldSetUp);
            players[0].livestats.ReturnedToOverWorldAfterGame();
            players[0].prisonlayout.SetDataFromMap(players[0].livestats.SelectedPrisonID, beammanager, enemyrenderer, players[0], this.resulttt);
            if (players[0].livestats.LevelIsTransferFromHoldingCell)
            {
              if (this.resulttt != GameResult.NoBombs)
                players[0].prisonlayout.CheckAndRemoveDuplicatesFromHoldingCells(enemyrenderer);
              else
                players[0].prisonlayout.CheckAndRemoveDeadFromHoldingCells(enemyrenderer);
              players[0].livestats.LevelIsTransferFromHoldingCell = false;
              FeatureFlags.DemolishEnabled = true;
            }
            players[0].prisonlayout.cellblockcontainer.SetConsumption(players[0].livestats.consumptionstatus, players[0]);
            ++GameFlags.CrrentStage;
            players[0].tracking.ContinuedFromResults();
            players[0].OldSaveThisPlayer();
          }
        }
      }
    }

    public void DrawResultsManager()
    {
      if (!this.IsActive || TutorialManager.currenttutorial == TUTORIALTYPE.GamePlayIntro)
        return;
      this.lockdownresults.DrawLockDownIntro();
      this.retry.DrawRetryCOnt();
    }
  }
}
