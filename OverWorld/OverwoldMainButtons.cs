// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverwoldMainButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Lerp;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GamePlay.PauseScreen;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.Tutorials;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar;
using TinyZoo.Z_Notification;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.OverWorld
{
  internal class OverwoldMainButtons
  {
    internal static OWCategoryButton[] textbuttons;
    private LerpHandler_FloatArray lerparray;
    internal static int Selected;
    public static int SelectedNeed;
    public OverworldButtons buttonpressed;
    private int Removeshop;
    private TRC_ButtonDisplay contbuton;
    private static bool ResetExit;
    internal static bool MouseIsOverAButton;

    internal static void RestExitStatus() => OverwoldMainButtons.ResetExit = true;

    public OverwoldMainButtons()
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      UIScaleHelper uiScaleHelper = new UIScaleHelper(baseScaleForUi);
      List<OverworldButtons> overworldButtonsList = new List<OverworldButtons>();
      overworldButtonsList.Add(OverworldButtons.Settings);
      overworldButtonsList.Add(OverworldButtons.Intake);
      overworldButtonsList.Add(OverworldButtons.Build);
      overworldButtonsList.Add(OverworldButtons.HeatMapView);
      OverwoldMainButtons.textbuttons = new OWCategoryButton[overworldButtonsList.Count];
      float num1 = uiScaleHelper.ScaleX(30f);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      float num2 = 1.2f;
      float x = (float) (1024.0 - (double) num1 * 0.5 - (double) defaultXbuffer * (double) num2);
      float middleOfBar = TopBarManager.GetMiddleOfBar();
      for (int index = 0; index < OverwoldMainButtons.textbuttons.Length; ++index)
      {
        OverwoldMainButtons.textbuttons[index] = new OWCategoryButton(overworldButtonsList[index], baseScaleForUi);
        OverwoldMainButtons.textbuttons[index].Location = new Vector2(x, middleOfBar);
        if (index == 0)
          x -= num1 + defaultXbuffer * num2;
        else
          x -= num1 + defaultXbuffer;
      }
      this.lerparray = new LerpHandler_FloatArray(OverwoldMainButtons.textbuttons.Length, 0.1f, 1f, 0.0f);
      Z_NotificationManager.LerpAllOn();
      this.buttonpressed = OverworldButtons.Count;
      float _Scale = TinyZoo.GameFlags.GetTRCButtonScale();
      if ((double) baseScaleForUi != -1.0)
        _Scale = baseScaleForUi;
      this.contbuton = new TRC_ButtonDisplay(_Scale);
      this.contbuton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.XboxY);
    }

    public bool IsLerpArrayComplete() => this.lerparray.IsComplete();

    public bool CheckMouseOver(Player player)
    {
      for (int index = 0; index < OverwoldMainButtons.textbuttons.Length; ++index)
      {
        if (OverwoldMainButtons.textbuttons[index].CheckMouseOver(player))
          return true;
      }
      return false;
    }

    public bool UpdateOverwoldMainButtons(Player player, float DeltaTime, out bool StartedExit)
    {
      if (OverwoldMainButtons.ResetExit)
      {
        OverwoldMainButtons.ResetExit = false;
        this.buttonpressed = OverworldButtons.Count;
      }
      StartedExit = false;
      if (OverWorldManager.IsGameIntro)
        return false;
      this.lerparray.UpdateArrayLerpers(DeltaTime);
      int HoldThisButton = -1;
      for (int index = 0; index < OverwoldMainButtons.textbuttons.Length; ++index)
      {
        if (OverwoldMainButtons.textbuttons[index].UpdateOWCategoryButton(DeltaTime, player))
        {
          HoldThisButton = (int) OverwoldMainButtons.textbuttons[index].buttontype;
          OverwoldMainButtons.Selected = index;
        }
        if (OverwoldMainButtons.textbuttons[index].MouseOver)
        {
          OverwoldMainButtons.Selected = index;
          OverwoldMainButtons.MouseIsOverAButton = true;
        }
      }
      if (player.inputmap.PressedThisFrame[26] && OverwoldMainButtons.Selected > -1 && OverwoldMainButtons.textbuttons[OverwoldMainButtons.Selected].CanPress())
        HoldThisButton = OverwoldMainButtons.Selected;
      if (HoldThisButton > -1 && this.buttonpressed == OverworldButtons.Count)
      {
        this.buttonpressed = (OverworldButtons) HoldThisButton;
        this.buttonpressed = OverwoldMainButtons.textbuttons[OverwoldMainButtons.Selected].buttontype;
        if (this.buttonpressed != OverworldButtons.Intake && this.buttonpressed != OverworldButtons.Settings)
        {
          this.lerparray.LerpOff(0.0f, HoldThisButton, 0.34f);
          this.lerparray.ForceComplete();
          Z_NotificationManager.LerpAllOff();
        }
        if (Z_DebugFlags.IsBetaVersion && this.buttonpressed == OverworldButtons.Settings)
          PauseManager.ForcePause = true;
        GameStateManager.tutorialmanager.pressedMainMenuShortcut(this.buttonpressed);
        StartedExit = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      }
      return this.buttonpressed != OverworldButtons.Count && this.lerparray.IsComplete();
    }

    public void DrawOverwoldMainButtons(SpriteBatch spriteBatch, Vector2 offset)
    {
      if (DebugFlags.HideAllUI_DEBUG)
        return;
      for (int index = 0; index < OverwoldMainButtons.textbuttons.Length; ++index)
      {
        if (TinyZoo.GameFlags.IsUsingController)
          OverwoldMainButtons.textbuttons[index].MouseOver = index == OverwoldMainButtons.Selected;
        OverwoldMainButtons.textbuttons[index].DrawOWCategoryButton(offset + new Vector2(0.0f, this.lerparray.arrayoflerpers[index].Value * -200f), spriteBatch);
        if (TinyZoo.GameFlags.IsUsingController && OverwoldMainButtons.Selected == index && (OWHUDManager.transferselection == null && TutorialManager.currenttutorial != TUTORIALTYPE.BuildFinished))
          this.contbuton.DrawTRC_ButtonDisplay(spriteBatch, AssetContainer.TRC_Sprites, offset + new Vector2(0.0f, this.lerparray.arrayoflerpers[index].Value * 400f) + OverwoldMainButtons.textbuttons[index].Location + OverwoldMainButtons.textbuttons[index].GetOffset() + new Vector2(-15f * OverwoldMainButtons.textbuttons[index].Icon.scale, -15f * OverwoldMainButtons.textbuttons[index].Icon.scale * Sengine.ScreenRatioUpwardsMultiplier.Y));
      }
    }
  }
}
