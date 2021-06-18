// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Results.RestryorContinue.RetryCOnt
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Localization;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.Intro;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.Tutorials;

namespace TinyZoo.GamePlay.Results.RestryorContinue
{
  internal class RetryCOnt
  {
    private TextButton Retry;
    private TextButton Conttinue;
    private LerpHandler_Float lerper;
    private WatchVideoButton watchvideo;
    private bool RanOutOfBeams;

    public RetryCOnt(
      bool WasWin,
      Player player,
      bool _RanOutOfBeams,
      TYPENAMETHING result,
      bool SomeoneDiedOnWin = false)
    {
      this.RanOutOfBeams = _RanOutOfBeams;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
      this.lerper.SetDelay(1f);
      player.tracking.CompletedRound(player, result, SomeoneDiedOnWin, GameFlags.EnemyCountAtStart - GameFlags.EnemyCount);
      string str = "";
      int num1 = GameFlags.IsConsoleVersion ? 1 : 0;
      if (WasWin)
      {
        this.Conttinue = new TextButton(str + SEngine.Localization.Localization.GetText(14), 40f);
        this.Conttinue.SetButtonGreen();
        this.Conttinue.vLocation = new Vector2(512f, 700f);
        if (SomeoneDiedOnWin)
        {
          if (!player.Stats.ADisabled(false, player))
          {
            this.watchvideo = new WatchVideoButton(str ?? "");
            if (PlayerStats.language != Language.English)
            {
              float num2 = SpringFontUtil.MeasureString("", AssetContainer.springFont).X * this.watchvideo.TextScale;
              float num3 = 160f;
              if ((double) num2 > (double) num3)
                this.watchvideo.TextScale = num3 / num2 * this.watchvideo.TextScale;
            }
            this.watchvideo.AddControllerButton(ControllerButton.XboxA);
          }
          this.Retry = new TextButton(str ?? "", 40f);
          this.Retry.SetButtonGreen();
          this.Conttinue.vLocation = new Vector2(412f, 700f);
          this.Retry.vLocation = new Vector2(612f, 700f);
        }
      }
      else
      {
        this.Conttinue = result != TYPENAMETHING.NoBeam ? (!GameFlags.IsArcadeMode ? new TextButton(str + SEngine.Localization.Localization.GetText(14), 40f) : new TextButton(str + SEngine.Localization.Localization.GetText(58), 40f)) : new TextButton(str + "Give Up", 40f);
        this.Conttinue.SetButtonRed();
        this.Retry = new TextButton(str ?? "", 40f);
        if (!player.Stats.ADisabled(false, player))
        {
          this.watchvideo = new WatchVideoButton(str ?? "");
          this.watchvideo.AddControllerButton(ControllerButton.XboxA);
        }
        this.Retry.SetButtonGreen();
        this.Conttinue.vLocation = new Vector2(412f, 700f);
        this.Retry.vLocation = new Vector2(612f, 700f);
        if (!GameFlags.IsArcadeMode && !GameFlags.BountyMode)
        {
          if (!this.RanOutOfBeams && (!player.Stats.TutorialsComplete[14] || !player.Stats.TutorialsComplete[19]) && !GameFlags.IsArcadeMode)
          {
            this.Conttinue = (TextButton) null;
            this.Retry.vLocation.X = 512f;
            GameStateManager.tutorialmanager.DoDeathPopUp(player);
          }
          else if (this.RanOutOfBeams && !player.Stats.TutorialsComplete[20])
            GameStateManager.tutorialmanager.DoNoBeamPopUp(player);
        }
      }
      if (this.watchvideo != null)
      {
        this.Retry.vLocation.X = 512f;
        if (this.Conttinue != null)
          this.Conttinue.vLocation.X = 900f;
      }
      if (this.Conttinue != null)
      {
        if (WasWin)
          this.Conttinue.AddControllerButton(ControllerButton.XboxA);
        else
          this.Conttinue.AddControllerButton(ControllerButton.XboxB);
      }
      if (this.Retry != null)
      {
        if (WasWin)
          this.Retry.AddControllerButton(ControllerButton.XboxB);
        else
          this.Retry.AddControllerButton(ControllerButton.XboxA);
      }
      if (this.Conttinue == null)
        return;
      if (this.watchvideo != null && this.watchvideo.controllerbutton == this.Conttinue.controllerbutton)
      {
        if (this.Conttinue.controllerbutton == ControllerButton.XboxA)
          this.watchvideo.AddControllerButton(ControllerButton.XboxB);
        else
          this.watchvideo.AddControllerButton(ControllerButton.XboxA);
      }
      if (this.Retry == null || this.Retry.controllerbutton != this.Conttinue.controllerbutton)
        return;
      if (this.Conttinue.controllerbutton == ControllerButton.XboxA)
        this.Retry.AddControllerButton(ControllerButton.XboxB);
      else
        this.Retry.AddControllerButton(ControllerButton.XboxA);
    }

    public void UpdateRetryCOnt(
      float DeltaTime,
      Player player,
      out bool ContinueNoe,
      out bool StartAdvertForRetry)
    {
      if (this.watchvideo != null && (TutorialManager.currenttutorial == TUTORIALTYPE.DeathRetryPopUp || !player.Stats.TutorialsComplete[14]))
      {
        player.livestats.skp = true;
        this.Conttinue = (TextButton) null;
        this.watchvideo = (WatchVideoButton) null;
      }
      StartAdvertForRetry = false;
      ContinueNoe = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0 || (double) TinyZoo.Game1.screenfade.fTargetAlpha != 0.0)
        return;
      if (this.Conttinue != null && this.Conttinue.UpdateTextButton(player, Vector2.Zero, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        ContinueNoe = true;
        if (this.RanOutOfBeams && !GameFlags.IsArcadeMode)
        {
          if (player.livestats.intakeUseForQuit != null && !player.prisonlayout.ThesePeopleAreInThePrison(player.livestats.intakeUseForQuit))
            player.intakes.intakeinfos[player.livestats.RemovedIndex] = player.livestats.intakeUseForQuit;
          player.livestats.waveinfo = (WaveInfo) null;
        }
      }
      if (this.Retry == null)
        return;
      bool flag = false;
      if (this.watchvideo != null)
        flag = this.watchvideo.UpdateWatchVideoButton(player, this.Retry.vLocation, GameFlags.IsUsingController);
      else if (this.Retry.UpdateTextButton(player, Vector2.Zero, DeltaTime))
        flag = true;
      if (!flag)
        return;
      if (this.watchvideo == null)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        player.livestats.ResetWaveForRetry();
        TinyZoo.Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      else
        StartAdvertForRetry = true;
    }

    public void DrawRetryCOnt()
    {
      Vector2 Offset = new Vector2(0.0f, 300f * this.lerper.Value);
      if (this.Retry != null)
      {
        if (this.watchvideo != null)
          this.watchvideo.DrawWatchVideoButton(Offset + this.Retry.vLocation, GameFlags.IsUsingController);
        else
          this.Retry.DrawTextButton(Offset);
      }
      if (this.Conttinue == null)
        return;
      this.Conttinue.DrawTextButton(Offset);
    }
  }
}
