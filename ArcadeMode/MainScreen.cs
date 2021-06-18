// Decompiled with JetBrains decompiler
// Type: TinyZoo.ArcadeMode.MainScreen
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.DragHandlers;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection;
using TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel;

namespace TinyZoo.ArcadeMode
{
  internal class MainScreen
  {
    private List<LevelIcon> LevelButons;
    private BackButton backbutton;
    private SpringDrag_ZoneManager springdrag;
    private int HighestLerpvalue;
    private bool Exiting;
    private LerpHandler_Float EXlerper;
    private SelectedPersonInfo selectedperson;
    private BuildMatrix buildmatrix;
    private bool Forced;
    private float Gap = 110f;
    private int SelectedLevel = -1;

    public MainScreen(Player player)
    {
      this.selectedperson = (SelectedPersonInfo) null;
      this.Exiting = false;
      this.backbutton = new BackButton();
      this.backbutton.backbtn.SetButtonYellow();
      this.LevelButons = new List<LevelIcon>();
      int ForceSelectThis = 0;
      for (int _LevelNumber = 0; _LevelNumber < GameFlags.TotalArcadeLevels; ++_LevelNumber)
      {
        this.LevelButons.Add(new LevelIcon(_LevelNumber, player.Stats.ArcadeProgress[_LevelNumber], player.Stats.ArcadeProgress[_LevelNumber] == 0));
        if (player.Stats.ArcadeProgress[_LevelNumber] == 0)
          ForceSelectThis = _LevelNumber;
      }
      int num1 = 0;
      int num2 = 0;
      int _TotalPerRow = 5;
      this.Gap = 110f;
      int num3 = 0;
      for (int index = 0; index < this.LevelButons.Count; ++index)
      {
        this.LevelButons[index].vLocation = new Vector2((float) num2 * this.Gap, (float) num1 * this.Gap * Sengine.ScreenRatioUpwardsMultiplier.Y);
        this.LevelButons[index].SetUpLerper(num3 + num2);
        this.HighestLerpvalue = num3 + num2;
        ++num2;
        if (num2 >= _TotalPerRow)
        {
          num2 = 0;
          ++num1;
          ++num3;
        }
        this.LevelButons[index].vLocation.X += 70f;
        this.LevelButons[index].vLocation.X += this.Gap;
        this.LevelButons[index].vLocation.Y += this.Gap * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      this.EXlerper = new LerpHandler_Float();
      this.buildmatrix = new BuildMatrix(_TotalPerRow, this.LevelButons.Count, ForceSelectThis);
      this.springdrag = new SpringDrag_ZoneManager((float) (((double) this.LevelButons[this.LevelButons.Count - 1].vLocation.Y - 650.0) * -1.0), new Vector2(70f, 0.0f), new Vector2(this.Gap * 5f, 768f));
    }

    public int UpdateMainScreen(Vector2 Offset, float DeltaTime, Player player, out bool ExitDone)
    {
      if (GameFlags.IsUsingController && !GameFlags.IsUsingMouse)
      {
        int num = this.SelectedLevel / 5;
        if (num > 0)
          --num;
        if (Math.Round((double) num * -((double) this.Gap * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)) != (double) this.springdrag.CurrentOffset.Y && (double) this.EXlerper.TargetValue != Math.Round((double) num * -((double) this.Gap * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)))
          this.EXlerper.SetLerp(true, this.springdrag.CurrentOffset.Y, (float) Math.Round((double) num * -((double) this.Gap * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)), 3f, true);
        this.EXlerper.UpdateLerpHandler(DeltaTime);
        this.springdrag.CurrentOffset.Y = this.EXlerper.Value;
      }
      else
        this.springdrag.UpdateSpringDrag_ZoneManager(player.player.touchinput, 100f);
      if (GameFlags.IsUsingController && this.buildmatrix.UpdateBuildMatrix(player, DeltaTime))
      {
        this.SelectedLevel = this.buildmatrix.Selected;
        if (this.Forced)
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
        this.Forced = true;
        this.selectedperson = new SelectedPersonInfo(this.buildmatrix.Selected, player);
        for (int index = 0; index < this.LevelButons.Count; ++index)
          this.LevelButons[index].baseframe.MouseOver = false;
        this.LevelButons[this.buildmatrix.Selected].baseframe.MouseOver = false;
      }
      if (this.backbutton.UpdateBackButton(player, DeltaTime) && !this.Exiting)
      {
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuClose);
        this.Exiting = true;
        for (int index = 0; index < this.LevelButons.Count; ++index)
          this.LevelButons[index].Exit(this.HighestLerpvalue);
        if (this.selectedperson != null)
          this.selectedperson.LerpOff(true);
        this.backbutton.Exit();
        TinyZoo.Game1.SetNextGameState(GAMESTATE.ModeSelectSetUp);
        TinyZoo.Game1.screenfade.BeginFade(true);
      }
      for (int index = 0; index < this.LevelButons.Count; ++index)
      {
        if (this.LevelButons[index].UpdateLevelIcon(Offset + this.springdrag.CurrentOffset, DeltaTime, player) && this.SelectedLevel != index)
        {
          this.SelectedLevel = index;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          this.buildmatrix.ForceSelect(index);
          this.selectedperson = new SelectedPersonInfo(this.SelectedLevel, player);
        }
      }
      ExitDone = false;
      if (this.Exiting && this.backbutton.ExitComplete())
      {
        ExitDone = true;
        for (int index = 0; index < this.LevelButons.Count; ++index)
        {
          if ((double) this.LevelButons[index].existencelerper.Value != 0.0)
            ExitDone = false;
        }
      }
      if (this.selectedperson != null)
      {
        bool OnParole;
        this.selectedperson.UpdateSelectedPersonInfo(DeltaTime, player, out OnParole, out bool _);
        if (OnParole)
          return this.SelectedLevel;
      }
      return -1;
    }

    public void DrawMainScreenManager(Vector2 Offset)
    {
      for (int index = 0; index < this.LevelButons.Count; ++index)
      {
        if (this.selectedperson != null && index == this.SelectedLevel)
          this.LevelButons[index].baseframe.MouseOver = true;
        this.LevelButons[index].DrawLevelIcon(Offset + this.springdrag.CurrentOffset);
      }
      if (this.selectedperson != null)
        this.selectedperson.DrawSelectedPersonInfo(Vector2.Zero);
      this.backbutton.DrawBackButton(Offset);
    }
  }
}
