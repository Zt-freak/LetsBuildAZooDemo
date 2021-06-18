// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.DayOfWeekDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_HUD.TopBar;
using TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek;

namespace TinyZoo.Z_WeekOver
{
  internal class DayOfWeekDisplay
  {
    private string DayOfWeek;
    private StringInBox stringinabox;
    private StringInBox stringinaboxBus;
    private long LastDayOfWeek;
    private LerpHandler_Float lerper;
    private bool UseNewUI;
    public Vector2 location;
    private TopBarDayOfWeek dayOfWeekButton;
    private bool IsTimeDisplay_NewUI;

    public DayOfWeekDisplay(Player player)
    {
      this.SetUp(player);
      this.stringinabox = new StringInBox("NA", 2f, 50f, true, true);
      this.stringinabox.SetAsButtonFrame(BTNColour.Cream);
      this.stringinabox.vLocation = new Vector2(120f, 20f);
      this.stringinaboxBus = new StringInBox("NA", 2f, 120f, true, true);
      this.stringinaboxBus.SetAsButtonFrame(BTNColour.Cream);
      this.stringinaboxBus.vLocation = new Vector2(340f, 20f);
    }

    public DayOfWeekDisplay(
      Player player,
      float BaseScale,
      float FrameHeight,
      bool _IsTimeDisplay_NewUI)
    {
      this.UseNewUI = true;
      this.IsTimeDisplay_NewUI = _IsTimeDisplay_NewUI;
      this.SetUp(player);
      this.dayOfWeekButton = new TopBarDayOfWeek(BaseScale, FrameHeight, this.IsTimeDisplay_NewUI);
    }

    public void SetUp(Player player)
    {
      this.lerper = new LerpHandler_Float();
      if (Player.financialrecords.GetDaysPassed() == -1L)
        this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      this.LastDayOfWeek = -1L;
    }

    public Vector2 GetSize()
    {
      if (this.UseNewUI)
        return this.dayOfWeekButton.GetSize();
      throw new Exception("Not coded for old UI");
    }

    public void UpdateDayOfWeekDisplay(Player player, float DeltaTime) => this.UpdateDayOfWeekDisplay(player, DeltaTime, Vector2.Zero);

    public void LerpIn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.lerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if ((double) this.lerper.TargetValue == -1.0)
        return;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.UseNewUI && this.dayOfWeekButton.CheckMouseOver(player, offset);
    }

    public void UpdateDayOfWeekDisplay(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.UseNewUI)
        this.dayOfWeekButton.UpdateTopBarDayOfWeek(player, DeltaTime, offset);
      if (FeatureFlags.BlockAllUI || FeatureFlags.BlockTimer)
        this.LerpOff();
      else if ((double) this.lerper.TargetValue != 0.0)
        this.LerpIn();
      if (Player.financialrecords.GetDaysPassed() < 0L)
        return;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.LastDayOfWeek != Player.financialrecords.GetDaysPassed())
      {
        this.LastDayOfWeek = Player.financialrecords.GetDaysPassed();
        DateTime dateTime = new DateTime(new TimeSpan((int) Player.financialrecords.GetDaysPassed(), 0, 0, 0).Ticks);
        SEngine.Localization.Localization.GetText(964);
        string dayOfTheWeek = DayOfWeekDisplay.GetDayOfTheWeek((int) Player.financialrecords.GetDaysPassed() % 7);
        if (this.UseNewUI)
        {
          if (!this.IsTimeDisplay_NewUI)
          {
            this.dayOfWeekButton.SetDayText(dayOfTheWeek);
            this.dayOfWeekButton.SetDateOrExtraText(Z_GameFlags.GetGameDateToday_AsString());
          }
        }
        else
          this.stringinabox.SetText(dayOfTheWeek);
      }
      string TimeLeft;
      if (OverWorldManager.z_daynightmanager.IsCountingDown(out TimeLeft, this.IsTimeDisplay_NewUI))
      {
        if (this.UseNewUI)
        {
          if (!this.IsTimeDisplay_NewUI)
            return;
          this.dayOfWeekButton.SetDayText(Z_GameFlags.GetTimeAsString());
          this.dayOfWeekButton.SetDateOrExtraText(TimeLeft);
        }
        else
          this.stringinaboxBus.SetText(TimeLeft);
      }
      else
      {
        string text = SEngine.Localization.Localization.GetText(971);
        if (this.UseNewUI)
        {
          if (!this.IsTimeDisplay_NewUI)
            return;
          this.dayOfWeekButton.SetDayText(text);
        }
        else
          this.stringinaboxBus.SetText(text);
      }
    }

    public void DrawDayOfWeekDisplay(Vector2 Offset) => this.DrawDayOfWeekDisplay(Offset, AssetContainer.pointspritebatch03);

    public void PreDrawDayOfWeekDisplay(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      Offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (!this.UseNewUI)
        return;
      this.dayOfWeekButton.PreDrawTopBarDayOfWeek(Offset, spriteBatch);
    }

    public void DrawDayOfWeekDisplay(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.location;
      Offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (this.UseNewUI)
      {
        this.dayOfWeekButton.DrawTopBarDayOfWeek(Offset, spriteBatch);
      }
      else
      {
        this.stringinabox.DrawStringInBox(Offset, spriteBatch);
        this.stringinaboxBus.DrawStringInBox(Offset, spriteBatch);
      }
    }

    internal static string GetDayOfTheWeek(int Index)
    {
      string str = "DOW FAIL";
      switch (Index)
      {
        case 0:
          str = SEngine.Localization.Localization.GetText(964);
          break;
        case 1:
          str = SEngine.Localization.Localization.GetText(965);
          break;
        case 2:
          str = SEngine.Localization.Localization.GetText(966);
          break;
        case 3:
          str = SEngine.Localization.Localization.GetText(967);
          break;
        case 4:
          str = SEngine.Localization.Localization.GetText(968);
          break;
        case 5:
          str = SEngine.Localization.Localization.GetText(969);
          break;
        case 6:
          str = SEngine.Localization.Localization.GetText(970);
          break;
      }
      return str;
    }
  }
}
