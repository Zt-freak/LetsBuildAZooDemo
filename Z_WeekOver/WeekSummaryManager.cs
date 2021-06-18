// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.WeekSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.Store_Local.StoreBG;
using TinyZoo.Z_WeekOver.Accounts_S;
using TinyZoo.Z_WeekOver.StaffPayment;
using TinyZoo.Z_WeekOver.V2;
using TinyZoo.Z_WeekOver.ZooWeek;

namespace TinyZoo.Z_WeekOver
{
  internal class WeekSummaryManager
  {
    private StoreBGManager storeBGManager;
    private BackButton backbutton;
    private ScreenHeading heading;
    private bool Exiting;
    private PayStaffManager paystaffmanager;
    private AccountsSummary accountssummary;
    private ZooProgressThisWeek zooprogress;
    private LerpHandler_Float ScreenLerper;
    private EndPOfWeekSummaryManager endofweekV2;

    public WeekSummaryManager(Player player)
    {
      this.Exiting = false;
      this.backbutton = new BackButton(true);
      this.storeBGManager = new StoreBGManager(IsAutumnal: true);
      ++player.Stats.Z_WeekEndsShown;
      int zWeekEndsShown = player.Stats.Z_WeekEndsShown;
      int num = (int) (Player.financialrecords.GetDaysPassed() / 7L);
      this.heading = new ScreenHeading("END OF THE WEEK", 100f);
      this.ScreenLerper = new LerpHandler_Float();
      if (Z_DebugFlags.UseNewEndOfWeek)
      {
        this.endofweekV2 = new EndPOfWeekSummaryManager(player);
      }
      else
      {
        this.paystaffmanager = new PayStaffManager(player);
        player.worldhistory.StartNewWeek();
        player.employees.StartNewWeek();
      }
    }

    public void UpdateWeekSummaryManager(
      Player player,
      float DeltaTime,
      OverWorldManager overworldmanager,
      Player[] players)
    {
      if (Z_DebugFlags.UseNewEndOfWeek)
      {
        overworldmanager.QuickUpdateOverWorldManager(players, 0.0f, 0.0f);
        if (!this.endofweekV2.UpdateEndPOfWeekSummaryManager(player, DeltaTime) || this.Exiting)
          return;
        this.Exiting = true;
        player.worldhistory.StartNewWeek();
        player.employees.StartNewWeek();
        if (Player.financialrecords.GetDaysPassed() > 10L && Z_DebugFlags.IsBetaVersion)
        {
          TinyZoo.Game1.screenfade.BeginFade(true);
          TinyZoo.Game1.SetNextGameState(GAMESTATE.BetaResultsSetUp);
        }
        else
        {
          TinyZoo.Game1.ForceSwitchToNextGameState = true;
          TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
        }
      }
      else
      {
        GameStateManager.tutorialmanager.UpdateTutorialManager(ref DeltaTime, ref DeltaTime, player);
        if (this.backbutton.UpdateBackButton(player, DeltaTime))
        {
          TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
          TinyZoo.Game1.screenfade.BeginFade(true);
        }
        this.ScreenLerper.UpdateLerpHandler(DeltaTime);
        this.storeBGManager.UpdateStoreBGManager(DeltaTime);
        this.heading.UpdateScreenHeading();
        Vector2 Offset = new Vector2(this.ScreenLerper.Value * 1024f, 0.0f);
        if (this.paystaffmanager != null && this.paystaffmanager.UpdatePayStaffManager(player, DeltaTime, Offset))
        {
          this.paystaffmanager = (PayStaffManager) null;
          this.accountssummary = new AccountsSummary(player);
          this.ScreenLerper.SetLerp(true, 1f, 0.0f, 3f, true);
          Offset = new Vector2(this.ScreenLerper.Value * 1024f, 0.0f);
          this.backbutton.LerpOn();
        }
        if (this.accountssummary != null && this.accountssummary.UpdateAccountsSummary(player, DeltaTime, Offset))
        {
          this.accountssummary = (AccountsSummary) null;
          this.zooprogress = new ZooProgressThisWeek(player);
          this.ScreenLerper.SetLerp(true, 1f, 0.0f, 3f, true);
          Offset = new Vector2(this.ScreenLerper.Value * 1024f, 0.0f);
          this.backbutton.LerpOn();
        }
        if (this.zooprogress == null || !this.zooprogress.UpdateZooProgressThisWeek(player, DeltaTime, Offset))
          return;
        TinyZoo.Game1.SetNextGameState(GAMESTATE.OverWorld);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
    }

    public void DrawWeekSummaryManager(OverWorldManager overworldmanager, Player player)
    {
      if (Z_DebugFlags.UseNewEndOfWeek)
      {
        overworldmanager.DrawOverWorldManager(player);
        this.endofweekV2.DrawEndPOfWeekSummaryManager();
      }
      else
      {
        Vector2 Offset = new Vector2(this.ScreenLerper.Value * 1024f, 0.0f);
        this.storeBGManager.DrawStoreBGManager(Vector2.Zero);
        this.backbutton.DrawBackButton(Vector2.Zero);
        this.heading.DrawScreenHeading(Vector2.Zero, AssetContainer.pointspritebatch03);
        if (this.paystaffmanager != null)
          this.paystaffmanager.DrawPayStaffManager(Offset);
        if (this.accountssummary != null)
          this.accountssummary.DrawAccountsSummary(Offset);
        if (this.zooprogress == null)
          return;
        this.zooprogress.DrawZooProgressThisWeek(Offset);
      }
    }
  }
}
