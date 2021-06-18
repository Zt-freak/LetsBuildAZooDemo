// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.TopBarElements
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD.GoodEvil;
using TinyZoo.Z_HUD.TopBar.Elements.Customers;
using TinyZoo.Z_HUD.TopBar.Elements.HeatMap;
using TinyZoo.Z_HUD.TopBar.Elements.Rating;
using TinyZoo.Z_HUD.TopBar.Elements.Research;
using TinyZoo.Z_WeekOver;

namespace TinyZoo.Z_HUD.TopBar.Elements
{
  internal class TopBarElements
  {
    public Vector2 location;
    private LerpHandler_Float mainElementsLerper;
    private DayOfWeekDisplay dayOfWeek;
    private DayOfWeekDisplay time;
    private SpeedUpTimeButton speedUpButt;
    private GoodEvilRating morality;
    private DollarPanel dollarPanel;
    private TopBarRating ratingDisplay;
    private TopBarResearch researchDisplay;
    private TopBarCustomer customerDisplay;
    private HeatMapTopBarElements heatMapElements;

    public TopBarElements(Player player, float BaseScale, float TopBarHeight)
    {
      float defaultXbuffer = new UIScaleHelper(BaseScale).GetDefaultXBuffer();
      float num1 = 0.0f;
      float num2 = TopBarHeight * 0.65f;
      float num3 = num1 + defaultXbuffer;
      Vector2 vector2 = Vector2.Zero;
      int num4 = 8;
      for (int index = 0; index < num4; ++index)
      {
        switch (index)
        {
          case 0:
            this.dayOfWeek = new DayOfWeekDisplay(player, BaseScale, num2, false);
            vector2 = this.dayOfWeek.GetSize();
            this.dayOfWeek.location.Y += TopBarHeight * 0.5f;
            this.dayOfWeek.location.X = num3 + vector2.X * 0.5f;
            break;
          case 1:
            this.time = new DayOfWeekDisplay(player, BaseScale, num2, true);
            vector2 = this.time.GetSize();
            this.time.location.Y += TopBarHeight * 0.5f;
            this.time.location.X = num3 + vector2.X * 0.5f;
            break;
          case 2:
            this.speedUpButt = new SpeedUpTimeButton(player, BaseScale, true);
            vector2 = this.speedUpButt.GetSize();
            this.speedUpButt.Location.Y += TopBarHeight * 0.5f;
            this.speedUpButt.Location.X = num3 + vector2.X * 0.5f;
            break;
          case 3:
            this.morality = new GoodEvilRating(player, BaseScale, num2);
            vector2 = this.morality.GetSize();
            this.morality.location.Y += TopBarHeight * 0.5f;
            this.morality.location.X = num3 + vector2.X * 0.5f;
            break;
          case 4:
            this.dollarPanel = new DollarPanel(player, BaseScale, num2);
            vector2 = this.dollarPanel.GetSize();
            this.dollarPanel.Location.Y += TopBarHeight * 0.5f;
            this.dollarPanel.Location.X = num3 + vector2.X * 0.5f;
            break;
          case 5:
            this.ratingDisplay = new TopBarRating(player, BaseScale, num2);
            vector2 = this.ratingDisplay.GetSize();
            this.ratingDisplay.location.Y += TopBarHeight * 0.5f;
            this.ratingDisplay.location.X = num3 + vector2.X * 0.5f;
            break;
          case 6:
            this.researchDisplay = new TopBarResearch(BaseScale, num2, player);
            vector2 = this.researchDisplay.GetSize();
            this.researchDisplay.location.Y += TopBarHeight * 0.5f;
            this.researchDisplay.location.X = num3 + vector2.X * 0.5f;
            break;
          case 7:
            this.customerDisplay = new TopBarCustomer(BaseScale, num2);
            vector2 = this.customerDisplay.GetSize();
            this.customerDisplay.location.Y += TopBarHeight * 0.5f;
            this.customerDisplay.location.X = num3 + vector2.X * 0.5f;
            break;
        }
        num3 = num3 + vector2.X + defaultXbuffer;
      }
      this.mainElementsLerper = new LerpHandler_Float();
      this.mainElementsLerper.Value = 0.0f;
      this.heatMapElements = new HeatMapTopBarElements(BaseScale, TopBarHeight, num2);
    }

    private void LerpIn()
    {
      if ((double) this.mainElementsLerper.TargetValue == 0.0)
        return;
      this.mainElementsLerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    private void LerpOff()
    {
      if ((double) this.mainElementsLerper.TargetValue == -1.0)
        return;
      this.mainElementsLerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset) => this.morality.CheckMouseOver(player, offset) || this.ratingDisplay.CheckMouseOver(player, offset) || (this.researchDisplay.CheckMouseOver(player, offset) || this.dayOfWeek.CheckMouseOver(player, offset)) || (this.time.CheckMouseOver(player, offset) || this.customerDisplay.CheckMouseOver(player, offset) || this.dollarPanel.CheckMouseOver(player, offset));

    public void UpdateTopBarElements(Player player, float DeltaTime, Vector2 offset)
    {
      if (FeatureFlags.InstantBlockTopBar)
      {
        FeatureFlags.InstantBlockTopBar = false;
      }
      else
      {
        offset += this.location;
        this.heatMapElements.UpdateHeatMapTopBarElements(player, DeltaTime, offset);
        if (OverWorldManager.overworldstate == OverWOrldState.ShowHeatMaps && Z_GameFlags.DRAW_heatmaptype != HeatMapType.None)
        {
          this.LerpOff();
          this.heatMapElements.LerpIn();
        }
        else
        {
          this.LerpIn();
          this.heatMapElements.LerpOff();
        }
        this.mainElementsLerper.UpdateLerpHandler(DeltaTime);
        offset.Y += this.mainElementsLerper.Value * TopBarManager.TopBarLerpDistance;
        this.dayOfWeek.UpdateDayOfWeekDisplay(player, DeltaTime, offset);
        this.time.UpdateDayOfWeekDisplay(player, DeltaTime, offset);
        this.speedUpButt.UpdateSpeedUpTimeButton(player, DeltaTime, offset);
        this.morality.UpdateGoodEvilRating(player, DeltaTime, offset);
        this.dollarPanel.UpdateDollarPanel(DeltaTime, player, offset);
        this.ratingDisplay.UpdateTopBarRating(player, DeltaTime, offset);
        this.researchDisplay.UpdateTopBarResearch(player, DeltaTime, offset);
        this.customerDisplay.UpdateTopBarCustomer(player, DeltaTime, offset);
      }
    }

    public void PreDrawTopBarElements(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (FeatureFlags.InstantBlockTopBar)
        return;
      offset += this.location;
      offset.Y += this.mainElementsLerper.Value * TopBarManager.TopBarLerpDistance;
      this.morality.PreDrawGoodEvilRating(offset, spriteBatch);
      this.ratingDisplay.PreDrawTopBarRating(offset, spriteBatch);
      this.researchDisplay.PreDrawTopBarRating(offset, spriteBatch);
      this.dollarPanel.PreDrawDollarPanel(offset, spriteBatch);
      this.dayOfWeek.PreDrawDayOfWeekDisplay(offset, spriteBatch);
      this.time.PreDrawDayOfWeekDisplay(offset, spriteBatch);
      this.customerDisplay.PreDrawTopBarCustomer(offset, spriteBatch);
    }

    public void DrawTopBarElements(Vector2 offset, SpriteBatch spriteBatch)
    {
      if (FeatureFlags.InstantBlockTopBar)
        return;
      offset += this.location;
      this.heatMapElements.DrawHeatMapTopBarElements(offset, spriteBatch);
      offset.Y += this.mainElementsLerper.Value * TopBarManager.TopBarLerpDistance;
      this.dayOfWeek.DrawDayOfWeekDisplay(offset, spriteBatch);
      this.time.DrawDayOfWeekDisplay(offset, spriteBatch);
      this.speedUpButt.DrawSpeedUpButton(offset, spriteBatch);
      this.morality.DrawGoodEvilRating(offset, spriteBatch);
      this.ratingDisplay.DrawTopBarRating(offset, spriteBatch);
      this.researchDisplay.DrawTopBarResearch(offset, spriteBatch);
      this.customerDisplay.DrawTopBarCustomer(offset, spriteBatch);
      this.dollarPanel.DrawDollarPanel(offset, spriteBatch, false);
    }
  }
}
