// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_DayNight.DayNightManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tutorials;
using TinyZoo.Tutorials.Z_Tutorials;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Customers;
using TinyZoo.Z_Bus;
using TinyZoo.Z_Notification;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.Z_DayNight
{
  internal class DayNightManager
  {
    internal static float SunShineValueR = 1f;
    internal static float SunShineValueG = 1f;
    internal static float SunShineValueB = 1f;
    internal static float NightLerpValue = 1f;
    private SinLerper NightLerp;
    private bool IsNight;
    private bool HasStartedDay;
    private LerpHandler_Float DayButtonlerper;
    private LerpHandler_Float SkipDayButtonlerper;
    private LerpHandler_Float DisplayLerper;
    private StartDayButton startdybutton;
    private StartDayButton SkipDayButton;
    private float NightTimer;
    private bool WaitingForNightToStart;
    private bool HasStartedPickingPeopleUp;
    internal static Vector2 BTNLocation = new Vector2(900f, 650f);
    private TRC_ButtonDisplay controllerButton;
    private static int DayQuarter;
    private bool ControllerSelected;
    public bool ForcePressButtonNextUpdate;

    public DayNightManager()
    {
      this.DisplayLerper = new LerpHandler_Float();
      if (Z_DebugFlags.HasSkipDay)
      {
        this.SkipDayButtonlerper = new LerpHandler_Float();
        this.SkipDayButtonlerper.SetLerp(true, -1f, 0.0f, 3f);
      }
      this.DisplayLerper.SetLerp(true, -1f, 0.0f, 3f);
      DayNightManager.BTNLocation = new Vector2(924f, 700f);
      this.DayButtonlerper = new LerpHandler_Float();
      this.DayButtonlerper.SetLerp(true, 1f, 1f, 0.0f);
      this.NightLerp = new SinLerper();
      this.IsNight = false;
      if (DebugFlags.IsPCVersion)
        DayNightManager.BTNLocation = new Vector2(914f, 700f);
      this.startdybutton = new StartDayButton(false);
      this.startdybutton.vLocation = DayNightManager.BTNLocation;
      if (Z_DebugFlags.HasSkipDay)
      {
        this.SkipDayButton = new StartDayButton(true);
        this.SkipDayButton.vLocation = DayNightManager.BTNLocation;
      }
      int num = Z_GameFlags.HasStartedFirstDay ? 1 : 0;
      this.controllerButton = new TRC_ButtonDisplay(TinyZoo.GameFlags.GetTRCButtonScale());
      this.controllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxY);
    }

    public void ReLerpButton()
    {
      this.DayButtonlerper = new LerpHandler_Float();
      this.DayButtonlerper.SetLerp(true, 1f, 1f, 0.0f);
      this.DayButtonlerper.SetDelay(0.5f);
    }

    public void SetControllerSelected(bool IsSelected) => this.ControllerSelected = IsSelected;

    private void SwitchToNight(Player player)
    {
      player.Stats.SunHasRisen = false;
      ParkGate.OpenGate(false);
      this.NightTimer = 0.0f;
      this.HasStartedDay = false;
      this.HasStartedPickingPeopleUp = false;
      if (!player.Stats.TutorialsComplete[7])
        GameStateManager.tutorialmanager.StartNewTutorial(TUTORIALTYPE.FirstNight, player);
      this.NightLerp.SetLerp(SinCurveType.EaseInAndOut, 5f, 0.0f, 1f, true);
    }

    private void SwitchToDay(Player player)
    {
      this.WaitingForNightToStart = false;
      this.NightLerp.SetLerp(SinCurveType.EaseInAndOut, 2f, 1f, 0.0f, true);
    }

    public bool IsCountingDown(out string TimeLeft, bool GetExtraTextAndCountDownOnly = false)
    {
      TimeLeft = "";
      if (!Z_GameFlags.HasStartedFirstDay)
      {
        TimeLeft = SEngine.Localization.Localization.GetText(979);
        return true;
      }
      if ((double) Z_GameFlags.DayTimer < (double) Z_GameFlags.SecondsInDay)
      {
        if (!GetExtraTextAndCountDownOnly)
        {
          TimeLeft = Z_GameFlags.GetTimeAsString();
          TimeLeft += " ";
        }
        TimeLeft = (double) Z_GameFlags.DayTimer > (double) Z_GameFlags.GetClosingTime() || (double) Z_GameFlags.DayTimer < (double) Z_GameFlags.ZooOpenTime_InSeconds && Z_GameFlags.HasPassedMidnight ? TimeLeft + SEngine.Localization.Localization.GetText(980) + Z_GameFlags.GetTimeAsString(Z_GameFlags.ZooOpenTime_InSeconds + 1f / 1000f) : TimeLeft + SEngine.Localization.Localization.GetText(981) + Z_GameFlags.GetTimeAsString((float) ((double) Z_GameFlags.ZooOpenTime_InSeconds + (double) Z_GameFlags.SecondsZooOpenPerDay + 1.0 / 1000.0));
        return true;
      }
      if (!this.HasStartedDay)
        return false;
      TimeLeft = SEngine.Localization.Localization.GetText(982);
      return true;
    }

    public bool AllowPressDayNightButton() => (Z_GameFlags.HasStartedFirstDay || (double) this.DayButtonlerper.Value == 0.0) && !OverWorldManager.IsGameIntro;

    public bool AllowSkipDayNightButton() => Z_DebugFlags.HasSkipDay && (double) this.SkipDayButtonlerper.Value == 0.0 && !OverWorldManager.IsGameIntro;

    public bool MouseIsOverStartDayButton(Player player) => this.startdybutton.CheckMouseOver(player);

    private void UpdateDayNightSwitchEvents(float DeltaTime, Player player, float SimulationTime)
    {
      if (Z_GameFlags.HasStartedFirstDay)
      {
        if (Z_GameFlags.ParkIsOpen())
        {
          if (Z_DebugFlags.developerOverrides[12] != 1)
          {
            if (Z_GameFlags.HoldTheClock)
            {
              if (!Z_HoldSave.ShouldHoldSave())
                Z_HoldSave.SetHold(false, player);
            }
            else
              Z_GameFlags.DayTimer += SimulationTime;
          }
        }
        else
          Z_GameFlags.ControlFastNight(SimulationTime, player);
      }
      if (this.IsNight)
      {
        if (TutorialManager.currenttutorial == TUTORIALTYPE.FirstNight || TrailerDemoFlags.AlwaysNight)
          return;
        this.NightTimer += SimulationTime;
        if ((double) Z_GameFlags.DayTimer < (double) Z_GameFlags.SunRise || (double) this.DayButtonlerper.TargetValue == 0.0 || !Z_GameFlags.HasPassedMidnight)
          return;
        this.SwitchToDay(player);
        this.IsNight = false;
      }
      else if (!this.HasStartedDay)
      {
        if (!TinyZoo.GameFlags.HasCompletedEnoughQuestsToStartDay)
        {
          if ((double) this.DayButtonlerper.Value == 1.0)
            return;
          this.DayButtonlerper.SetLerp(true, 1f, 1f, 3f);
        }
        else
        {
          if ((double) this.DayButtonlerper.TargetValue == 0.0)
            return;
          this.DayButtonlerper.SetLerp(true, 1f, 0.0f, 3f);
        }
      }
      else
      {
        if (!this.HasStartedDay || this.IsNight)
          return;
        Z_GameFlags.IsDay = true;
        if (!player.Stats.TutorialsComplete[4] && (double) Z_GameFlags.DayTimer > 5.0)
          GameStateManager.tutorialmanager.StartNewTutorial(TUTORIALTYPE.Z_Hints, player);
        if (DayNightManager.DayQuarter == 1 && (double) Z_GameFlags.DayTimer >= (double) Z_GameFlags.SecondsInQuarterDay)
        {
          QuarterDay.StartQuarterDay(DayNightManager.DayQuarter, player);
          ++DayNightManager.DayQuarter;
        }
        else if (DayNightManager.DayQuarter == 2 && (double) Z_GameFlags.DayTimer >= (double) Z_GameFlags.SecondsInQuarterDay * 2.0)
        {
          QuarterDay.StartQuarterDay(DayNightManager.DayQuarter, player);
          ++DayNightManager.DayQuarter;
        }
        else if (DayNightManager.DayQuarter == 3 && (double) Z_GameFlags.DayTimer >= (double) Z_GameFlags.SecondsInQuarterDay * 3.0)
        {
          QuarterDay.StartQuarterDay(DayNightManager.DayQuarter, player);
          ++DayNightManager.DayQuarter;
        }
        if ((double) Z_GameFlags.DayTimer > (double) Z_GameFlags.SecondsInDay)
        {
          this.HasStartedPickingPeopleUp = true;
          Z_BusManager.SendBusToPickPeopleUp();
        }
        if (!Z_GameFlags.IsDay || (double) Z_GameFlags.DayTimer <= (double) Z_GameFlags.NightFall)
          return;
        this.StartNight(player);
        Z_GameFlags.IsDay = false;
      }
    }

    public void UpdateDayNightManager(Player player, float DeltaTime, float SimulationTime)
    {
      if (FeatureFlags.BlockAllUI)
      {
        if ((double) this.DisplayLerper.TargetValue != -1.0)
          this.DisplayLerper.SetLerp(false, -1f, -1f, 3f, true);
      }
      else if ((double) this.DisplayLerper.TargetValue != 0.0)
        this.DisplayLerper.SetLerp(false, -1f, 0.0f, 3f, true);
      this.DisplayLerper.UpdateLerpHandler(DeltaTime);
      if (OverWorldManager.IsGameIntro)
        return;
      this.NightLerp.UpdateSinLerper(SimulationTime * 10f);
      DayNightManager.NightLerpValue = this.NightLerp.CurrentValue;
      this.UpdateDayNightSwitchEvents(DeltaTime, player, SimulationTime);
      this.DayButtonlerper.UpdateLerpHandler(DeltaTime);
      if (Z_DebugFlags.HasSkipDay)
        this.SkipDayButtonlerper.UpdateLerpHandler(DeltaTime);
      if (Z_GameFlags.IsDay)
      {
        if (Z_DebugFlags.HasSkipDay)
        {
          if (CustomerManager.CustomersInPark_NotWaitingForBus <= 0 && (double) Z_GameFlags.DayTimer < (double) Z_GameFlags.SecondsInDay)
          {
            if ((double) this.SkipDayButtonlerper.TargetValue != 0.0)
            {
              player.livestats.EndWorkingDay(Z_GameFlags.DayTimer);
              this.SkipDayButtonlerper.SetLerp(false, 0.0f, 0.0f, 3f, true);
            }
          }
          else if ((double) this.SkipDayButtonlerper.TargetValue != -1.0)
            this.SkipDayButtonlerper.SetLerp(false, 0.0f, -1f, 3f, true);
        }
      }
      else if (Z_DebugFlags.HasSkipDay && (double) this.SkipDayButtonlerper.TargetValue != -1.0)
        this.SkipDayButtonlerper.SetLerp(false, 0.0f, -1f, 3f, true);
      if (this.AllowSkipDayNightButton() && this.SkipDayButton.UpdateStartDayButton(player, DeltaTime))
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
        if ((double) Z_GameFlags.DayTimer < (double) Z_GameFlags.SecondsInDay)
          Z_GameFlags.DayTimer = Z_GameFlags.DayTimer = Z_GameFlags.SecondsInDay - 0.1f;
      }
      if (this.AllowPressDayNightButton())
      {
        if (Z_GameFlags.JustloadedGame && Z_GameFlags.ParkIsOpen())
        {
          Z_GameFlags.JustloadedGame = false;
          this.StartNewDay(player);
        }
        if ((this.startdybutton.UpdateStartDayButton(player, DeltaTime) || this.ControllerSelected && player.inputmap.PressedThisFrame[26] || Z_GameFlags.HasPassedMidnight && !this.HasStartedDay) && !TrailerDemoFlags.DisableSave)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          if (Player.financialrecords.GetDaysPassed() > 0L)
          {
            if (Z_HoldSave.ShouldHoldSave())
            {
              Z_HoldSave.SetHold(true, player);
            }
            else
            {
              OverWorldManager.zoopopupHolder.CreateZooPopUps(player, POPUPSTATE.SaveAlert);
              Z_HoldSave.SetHold(false, player);
            }
          }
          else
            this.StartNewDay(player);
        }
      }
      if ((double) this.NightLerp.CurrentValue == 0.0)
      {
        DayNightManager.SunShineValueR = 1f;
        DayNightManager.SunShineValueG = 1f;
        DayNightManager.SunShineValueB = 1f;
      }
      else
      {
        DayNightManager.SunShineValueR = (float) (1.0 - 0.75 * (double) this.NightLerp.CurrentValue);
        DayNightManager.SunShineValueG = (float) (1.0 - 0.75 * (double) this.NightLerp.CurrentValue);
        DayNightManager.SunShineValueB = (float) (1.0 - 0.150000005960464 * (double) this.NightLerp.CurrentValue);
      }
    }

    public void StartNewDay(Player player)
    {
      Player.financialrecords.StartedNewDay();
      TinyZoo.GameFlags.IsDay = true;
      DayNightManager.DayQuarter = 1;
      player.inputmap.PressedThisFrame[26] = false;
      Player.financialrecords.DoSomeWeirdFunction(player);
      player.employees.StartNewDay();
      float parkPopularity = ParkPopularity.GetParkPopularity(player);
      NewCustomerCalculator.Calc_NewCustomers(player, parkPopularity);
      CustomerManager.CustomersInPark_NotWaitingForBus = 0;
      CustomerManager.VIP_BlackMarketEtc = 0;
      ParkGate.OpenGate(true);
      this.DayButtonlerper.SetLerp(true, 0.0f, 1f, 3f);
      this.HasStartedDay = true;
      if (!Z_GameFlags.HasStartedFirstDay)
      {
        Z_GameFlags.HasStartedFirstDay = true;
        QuestScrubber.ScrubOnOpenForBusinessFirstTime(player);
      }
      StartDay.StartNewDay(player);
      Z_BusManager.StartNewDay();
      Z_NotificationManager.OpenedZoo = true;
      this.HasStartedPickingPeopleUp = false;
      player.Stats.SunHasRisen = false;
      StartTheDayTutorial.PressedStartDay();
      player.prisonlayout.GetHungryAnaimals(player);
      player.livestats.SortHungryAnimals();
      QuestScrubber.ScrubOnStartNewDay(player);
    }

    internal static void CommitFincncialsAtMidnight(Player player)
    {
      NewCustomerCalculator.FinalizePropleWaitingRecordsAtEndOfDay();
      Player.financialrecords.EndedADay(player);
      if (player.Stats.Z_WeekEndsShown >= (int) (Player.financialrecords.GetDaysPassed() / 7L) || (int) Player.financialrecords.GetDaysPassed() % 7 != 0)
        return;
      Player.financialrecords.EndedWeek();
      TinyZoo.Game1.SetNextGameState(GAMESTATE.WeekSummarySetUp);
      TinyZoo.Game1.ForceSwitchToNextGameState = true;
    }

    public void StartNight(Player player)
    {
      if (this.IsNight)
        return;
      Z_GameFlags.HasPassedMidnight = false;
      this.IsNight = true;
      this.SwitchToNight(player);
    }

    public void DrawDayNightManager()
    {
      if (OverWorldManager.IsGameIntro)
        return;
      if (Z_DebugFlags.HasSkipDay && (double) this.SkipDayButtonlerper.Value != -1.0)
      {
        Vector2 Offset = new Vector2(this.SkipDayButtonlerper.Value * -900f, this.SkipDayButtonlerper.Value * 200f);
        this.SkipDayButton.DrawStartDayButton(Offset);
        if (this.ControllerSelected && TinyZoo.GameFlags.IsUsingController)
          this.controllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.SkipDayButton.vLocation - new Vector2(30f, 25f) + Offset);
      }
      if ((double) this.DayButtonlerper.Value == 1.0 || (double) this.DisplayLerper.Value == -1.0)
        return;
      Vector2 Offset1 = new Vector2(this.DayButtonlerper.Value * 900f, this.DisplayLerper.Value * 200f);
      this.startdybutton.DrawStartDayButton(Offset1);
      if (!this.ControllerSelected || !TinyZoo.GameFlags.IsUsingController)
        return;
      this.controllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, this.startdybutton.vLocation - new Vector2(30f, 25f) + Offset1);
    }
  }
}
