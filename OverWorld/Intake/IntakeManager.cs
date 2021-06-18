// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Intake.IntakeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.AdvertPlayer;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Intake.InmateSummary;
using TinyZoo.OverWorld.Intake.JobList;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.PlayerDir;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.Intake
{
  internal class IntakeManager
  {
    private JobButtonManager jobbuttonmanager;
    private LerpHandler_Float lerper;
    private Vector2 Offset;
    private InmateSummaryManager inmatesummarymanager;
    private StoreBGManager storeBG;
    private BackButton backbtn;
    private bool Exiting;
    private bool GoToCellBlockSelect;
    private HCAdvertPlayer advertplayer;
    private TextButton Shuffle;
    private WatchVideoButton watchvideo;

    public IntakeManager(Player player)
    {
      GameFlags.BountyMode = false;
      player.livestats.LevelIsTransferFromHoldingCell = false;
      if (player.intakes.intakeinfos.Count < 3)
        player.intakes = new Intakes(player, player.intakes.Wave);
      this.jobbuttonmanager = new JobButtonManager(player);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.storeBG = new StoreBGManager(true);
      this.inmatesummarymanager = new InmateSummaryManager();
      this.inmatesummarymanager.SelectedNewIcon(this.jobbuttonmanager.Selected, true, player);
      this.backbtn = new BackButton(true);
      this.GoToCellBlockSelect = false;
      this.Shuffle = new TextButton(nameof (Shuffle), 80f);
      this.Shuffle.AddControllerButton(ControllerButton.XboxY);
      int num = GameFlags.IsConsoleVersion ? 1 : 0;
      if (!player.Stats.ADisabled(false, player))
      {
        this.watchvideo = new WatchVideoButton(nameof (Shuffle));
        this.watchvideo.AddControllerButton(ControllerButton.XboxY);
      }
      this.Shuffle.vLocation = new Vector2(130f, 700f);
    }

    private bool UpdateAdvertPlayer(float DeltaTime, Player player, out bool Remake)
    {
      Remake = false;
      if (this.advertplayer == null)
        return false;
      this.advertplayer.UpdateAdvertPlayer(DeltaTime);
      if (!this.advertplayer.IsWaiting)
      {
        if (this.advertplayer.WasSuccess)
        {
          this.advertplayer = (HCAdvertPlayer) null;
          player.intakes = new Intakes(player, player.intakes.Wave);
          player.tracking.WatchedAdvert(AdvertLocation.ShuffleIntake, player);
          Remake = true;
        }
        else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0])
          this.advertplayer = (HCAdvertPlayer) null;
      }
      else if (this.advertplayer.WasTimeout && ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.PressedThisFrame[0]))
        this.advertplayer = (HCAdvertPlayer) null;
      return true;
    }

    public bool UpdateIntakeManager(float DeltaTime, Player player, out bool _GoToCellBlockSelect)
    {
      if (this.advertplayer != null)
      {
        bool Remake = false;
        int num = this.UpdateAdvertPlayer(DeltaTime, player, out Remake) ? 1 : 0;
        if (Remake)
        {
          this.jobbuttonmanager = new JobButtonManager(player);
          this.inmatesummarymanager = new InmateSummaryManager();
          this.inmatesummarymanager.SelectedNewIcon(this.jobbuttonmanager.Selected, true, player);
        }
        if (num != 0)
        {
          _GoToCellBlockSelect = false;
          return false;
        }
      }
      _GoToCellBlockSelect = this.GoToCellBlockSelect;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.storeBG.UpdateStoreBGManager(DeltaTime);
      this.Offset = new Vector2(1024f * this.lerper.Value, 0.0f);
      if (this.jobbuttonmanager.UpdateEntryManager(DeltaTime, this.Offset, player))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
        this.inmatesummarymanager.SelectedNewIcon(this.jobbuttonmanager.Selected, false, player);
      }
      bool GoToCellBlockSelect;
      this.inmatesummarymanager.UpdateInmateSummaryManager(DeltaTime, player, out GoToCellBlockSelect);
      if (GoToCellBlockSelect)
        this.GoToCellBlockSelect = true;
      if (TinyZoo.Game1.GetNextGameState() != GAMESTATE.GamePlaySetUp && TutorialManager.currenttutorial != TUTORIALTYPE.SelectIntake)
      {
        if ((this.backbtn.UpdateBackButton(player, DeltaTime) || player.inputmap.PressedBackOnController()) && !this.Exiting)
        {
          this.lerper.SetLerp(false, 0.0f, 1f, 3f, true);
          this.Exiting = true;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        }
        if (TutorialManager.currenttutorial != TUTORIALTYPE.SelectIntake)
        {
          bool flag = false;
          if (this.watchvideo != null)
            flag = this.watchvideo.UpdateWatchVideoButton(player, this.Shuffle.vLocation, GameFlags.IsUsingController);
          else if (this.Shuffle.UpdateTextButton(player, Vector2.Zero, DeltaTime))
            flag = true;
          if (flag && (double) TinyZoo.Game1.screenfade.fTargetAlpha != 1.0)
          {
            if (player.Stats.ADisabled(false, player))
            {
              player.intakes = new Intakes(player, player.intakes.Wave);
              this.jobbuttonmanager = new JobButtonManager(player);
              this.inmatesummarymanager = new InmateSummaryManager();
              this.inmatesummarymanager.SelectedNewIcon(this.jobbuttonmanager.Selected, true, player);
              this.jobbuttonmanager.UpdateEntryManager(0.0f, new Vector2(10000f, 0.0f), player);
              this.inmatesummarymanager.UpdateInmateSummaryManager(0.0f, player, out bool _);
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.Rotate, 0.3f);
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick, 0.7f, 1f);
            }
            else
            {
              this.advertplayer = new HCAdvertPlayer(false, player);
              return false;
            }
          }
        }
      }
      return this.Exiting && (double) this.lerper.Value == 1.0;
    }

    public void DrawIntakeManager()
    {
      if (this.advertplayer != null)
      {
        this.advertplayer.DrawAdvertPlayer();
      }
      else
      {
        this.storeBG.DrawStoreBGManager(this.Offset);
        this.jobbuttonmanager.DraEntryManager(this.Offset, AssetContainer.pointspritebatchTop05);
        this.inmatesummarymanager.DrawInmateSummaryManager(this.Offset, AssetContainer.pointspritebatchTop05);
        if (TutorialManager.currenttutorial == TUTORIALTYPE.SelectIntake)
          return;
        this.backbtn.DrawBackButton(this.Offset);
        if (this.watchvideo != null)
          this.watchvideo.DrawWatchVideoButton(new Vector2(this.lerper.Value * 912f, 0.0f) + this.Offset + this.Shuffle.vLocation, GameFlags.IsUsingController);
        else
          this.Shuffle.DrawTextButton(new Vector2(this.lerper.Value * 912f, 0.0f) + this.Offset, 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
