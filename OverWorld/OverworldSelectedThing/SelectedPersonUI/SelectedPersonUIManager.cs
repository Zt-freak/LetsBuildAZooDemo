// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectedPersonUIManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SelectionPanel;
using TinyZoo.OverWorld.PopUps;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI
{
  internal class SelectedPersonUIManager
  {
    private Enemy selectedenemy;
    private static SmallESelectedHighlight highlight;
    private SelectedPersonInfo selectedperson;
    private IntakePerson seletedintakeperson;
    public IntakePerson intakeperson;
    private bool WaitingForPopUp;

    public SelectedPersonUIManager()
    {
      if (SelectedPersonUIManager.highlight != null)
        return;
      SelectedPersonUIManager.highlight = new SmallESelectedHighlight();
    }

    public void Deselect()
    {
      if (this.WaitingForPopUp)
        return;
      this.selectedenemy = (Enemy) null;
      this.seletedintakeperson = (IntakePerson) null;
      this.intakeperson = (IntakePerson) null;
    }

    public void SelectedAPerson(IntakePerson enemy, Enemy _fullenemy, Player player)
    {
      this.intakeperson = enemy;
      this.selectedperson = new SelectedPersonInfo(enemy, _fullenemy == null, player);
      this.selectedenemy = _fullenemy;
      this.seletedintakeperson = enemy;
    }

    public bool UpdateSelectedPersonUIManager(float DeltaTime, Player player)
    {
      if (this.WaitingForPopUp)
      {
        switch (PopUpPanel.LastButtonPressed)
        {
          case 0:
            this.WaitingForPopUp = false;
            break;
          case 1:
          case 2:
            if (PopUpPanel.LastButtonPressed == 1)
              player.Stats.GiveReputation(1, player);
            else
              player.Stats.GiveCash(player.livestats.HCH / 2, player);
            player.livestats.HCH = 0;
            player.prisonlayout.DoParole(this.intakeperson);
            this.WaitingForPopUp = false;
            player.prisonlayout.cellblockcontainer.SetConsumption(player.livestats.consumptionstatus, player);
            int EarningsWithoutMod = 0;
            player.prisonlayout.GetDailyEanings(true, out EarningsWithoutMod, out int _, player);
            player.OldSaveThisPlayer();
            return true;
        }
        return false;
      }
      if (TutorialManager.currenttutorial != TUTORIALTYPE.None)
        this.seletedintakeperson = (IntakePerson) null;
      if (this.seletedintakeperson != null)
      {
        bool OnParole;
        bool Transfered;
        bool flag = this.selectedperson.UpdateSelectedPersonInfo(DeltaTime, player, out OnParole, out Transfered);
        if (OnParole)
        {
          if (this.selectedenemy.enemyrenderere.IsDead)
          {
            OWHUDManager.DoRevivePopUp(this.intakeperson, player);
            this.WaitingForPopUp = true;
          }
          else
          {
            OWHUDManager.DoParolePopUp(this.intakeperson, player);
            this.WaitingForPopUp = true;
          }
        }
        else
        {
          if (Transfered)
          {
            player.prisonlayout.cellblockcontainer.TransferPrisonerToHoldingCell_FromMap(this.intakeperson);
            return true;
          }
          if (flag)
            return flag;
        }
      }
      return false;
    }

    public void DrawSelectedPersonUIManager()
    {
      if (this.seletedintakeperson == null)
        return;
      if (this.selectedenemy != null)
      {
        SelectedPersonUIManager.highlight.DrawSmallESelectedHighlight(this.selectedenemy.enemyrenderere.vLocation);
        TextFunctions.DrawJustifiedText(this.selectedenemy.refperson.Name.ToUpper(), 2f, RenderMath.TranslateWorldSpaceToScreenSpace(this.selectedenemy.enemyrenderere.vLocation) + new Vector2(0.0f, (float) (9.0 * (double) Sengine.WorldOriginandScale.Z * (double) Sengine.ScreenRatioUpwardsMultiplier.Y + 6.0)), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatch03, true);
      }
      this.selectedperson.DrawSelectedPersonInfo(Vector2.Zero);
    }
  }
}
