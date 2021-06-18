// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.Transfer.TransferScreen.PrisonerAndButtons
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using SEngine.DragHandlers;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.OverWorld.Transfer.TransferScreen.Prisoners;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.HoldingCells;

namespace TinyZoo.OverWorld.Transfer.TransferScreen
{
  internal class PrisonerAndButtons
  {
    private List<PrisonerIconSwitcher> prisonericons;
    private int MaximumFreeSlots;
    private int CurrentTransfer;
    private bool IsHoldingCell;
    private TransferConfirmPanel transferconfirm;
    private bool IsReadyToGo;
    private AssignementState assignmentstate;
    private PrisonZone refzone;
    private HoldingCellInfo holdingcell;
    private SpringDrag_ZoneManager springdrag;
    private float Gap;
    private int ControlelrSelected;
    private LerpHandler_Float ControllerButtonLerper;
    private ButtonRepeater repeater;
    private bool BlockAssign;

    public PrisonerAndButtons(PrisonZone zone, HoldingCellInfo _holdingcell, Player player)
    {
      this.holdingcell = _holdingcell;
      this.refzone = zone;
      this.ControllerButtonLerper = new LerpHandler_Float();
      this.repeater = new ButtonRepeater();
      this.assignmentstate = AssignementState.NothingTicked;
      this.prisonericons = new List<PrisonerIconSwitcher>();
      if (this.holdingcell != null)
      {
        this.IsHoldingCell = true;
        for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.holdingcells.Count; ++index1)
        {
          for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.holdingcells[index1].prisonercontainer.prisoners.Count; ++index2)
            this.prisonericons.Add(new PrisonerIconSwitcher(player.prisonlayout.cellblockcontainer.holdingcells[index1].prisonercontainer.prisoners[index2]));
        }
      }
      else
      {
        this.IsHoldingCell = false;
        for (int index = 0; index < player.prisonlayout.cellblockcontainer.holdingcells.Count; ++index)
          this.MaximumFreeSlots += player.prisonlayout.cellblockcontainer.holdingcells[index].GetFreeTransferSlots();
        for (int index = 0; index < zone.prisonercontainer.prisoners.Count; ++index)
        {
          this.prisonericons.Add(new PrisonerIconSwitcher(zone.prisonercontainer.prisoners[index]));
          if (this.holdingcell == null && zone.prisonercontainer.prisoners[index].intakeperson.WrongCell)
            this.prisonericons[index].AddUnhappyFace();
        }
      }
      this.Gap = 90f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      for (int index = 0; index < this.prisonericons.Count; ++index)
        this.prisonericons[index].Location = new Vector2(512f, (float) (250.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y + (double) index * (double) this.Gap));
      this.transferconfirm = new TransferConfirmPanel();
      this.SetNewTransferState();
      float MaxHeightOfDrag_SetNegativeForUpwardsDra = 0.0f;
      if (this.prisonericons.Count > 0)
      {
        MaxHeightOfDrag_SetNegativeForUpwardsDra = (float) (((double) this.prisonericons[this.prisonericons.Count - 1].Location.Y - 500.0) * -1.0);
        if ((double) MaxHeightOfDrag_SetNegativeForUpwardsDra > 0.0)
          MaxHeightOfDrag_SetNegativeForUpwardsDra = 0.0f;
      }
      this.springdrag = new SpringDrag_ZoneManager(MaxHeightOfDrag_SetNegativeForUpwardsDra, new Vector2(0.0f, 200f), new Vector2(1024f, 400f));
    }

    private void SetNewTransferState()
    {
      switch (this.assignmentstate)
      {
        case AssignementState.NothingTicked:
          if (this.IsHoldingCell)
          {
            this.transferconfirm.ForceText("Select the prisoners you wish to move to a Cell Block.", false);
            break;
          }
          this.transferconfirm.ForceText("Select the prisoners you wish to move to a Holding Cell.", false);
          break;
        case AssignementState.TickedAndReadyForBattle:
          this.transferconfirm.ForceText("Select Cell Block to move prisoners to.", true);
          break;
        case AssignementState.TickedAndReadyToTransferToHoldingCells:
          this.transferconfirm.ForceText("Transfer to empty Holding Cells.", true);
          break;
        case AssignementState.TickedButAtMax:
          this.transferconfirm.ForceText("Transfer to empty Holding Cells.~To get more space, build more cells.", true);
          break;
      }
    }

    public bool UpdatePrisonerAndButtons(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      ref bool GoToCellSelectFromTransfer)
    {
      DirectionPressed Direction;
      if (this.repeater.UpdateMenuRepeats(DeltaTime, out Direction, player.inputmap.HeldButtons[16], player.inputmap.HeldButtons[17], false, false))
      {
        switch (Direction)
        {
          case DirectionPressed.Up:
            if (this.ControlelrSelected > 0)
            {
              --this.ControlelrSelected;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.8f, 1f);
              break;
            }
            break;
          case DirectionPressed.Down:
            if (this.ControlelrSelected < this.prisonericons.Count - 1)
            {
              ++this.ControlelrSelected;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.8f, 1f);
              break;
            }
            break;
        }
        if ((double) this.ControllerButtonLerper.TargetValue != (double) (this.ControlelrSelected * -1))
          this.ControllerButtonLerper.SetLerp(false, 0.0f, (float) (this.ControlelrSelected * -1), 3f, true);
      }
      this.ControllerButtonLerper.UpdateLerpHandler(DeltaTime);
      if (GameFlags.IsUsingController)
        this.springdrag.CurrentOffset.Y = this.ControllerButtonLerper.Value * this.Gap;
      this.springdrag.UpdateSpringDrag_ZoneManager(player.player.touchinput, 100f);
      bool flag = false;
      if (this.transferconfirm.UpdateTransferConfirmPanel(player, Offset, DeltaTime))
      {
        if (this.assignmentstate == AssignementState.TickedAndReadyForBattle)
        {
          IntakeInfo _NewPrisoners = new IntakeInfo(true);
          for (int index = 0; index < this.prisonericons.Count; ++index)
          {
            if (this.prisonericons[index].IsTick())
              _NewPrisoners.People.Add(this.prisonericons[index].prisonerinfo.intakeperson);
          }
          player.livestats.SetPrisonersForNextPlay(_NewPrisoners);
          if (player.prisonlayout.cellblockcontainer.prisonzones.Count > 1)
          {
            player.livestats.LevelIsTransferFromHoldingCell = true;
            GoToCellSelectFromTransfer = true;
          }
          else
          {
            player.livestats.SelectedPrisonID = player.prisonlayout.cellblockcontainer.prisonzones[0].Cell_UID;
            player.livestats.AddExtraEnemiesForNextPlay(player, player.livestats.SelectedPrisonID);
            player.livestats.LevelIsTransferFromHoldingCell = true;
            TinyZoo.Game1.screenfade.BeginFade(true);
            FeatureFlags.DemolishEnabled = true;
            TinyZoo.Game1.SetNextGameState(GAMESTATE.GamePlaySetUp);
          }
        }
        else if (this.assignmentstate == AssignementState.TickedAndReadyToTransferToHoldingCells || this.assignmentstate == AssignementState.TickedButAtMax)
        {
          for (int index = 0; index < this.prisonericons.Count; ++index)
          {
            if (this.prisonericons[index].IsTick() && this.refzone.RemoveThisPrisoner(this.prisonericons[index].prisonerinfo))
            {
              player.prisonlayout.cellblockcontainer.TryToAddPrisonersToHoldingCells(this.prisonericons[index].prisonerinfo, player);
              flag = true;
            }
          }
          player.OldSaveThisPlayer();
        }
      }
      this.CurrentTransfer = 0;
      for (int index = 0; index < this.prisonericons.Count; ++index)
      {
        if (this.prisonericons[index].IsTick())
          ++this.CurrentTransfer;
      }
      this.BlockAssign = false;
      if (this.IsHoldingCell)
      {
        if (this.assignmentstate == AssignementState.NothingTicked)
        {
          if (this.CurrentTransfer > 0)
          {
            this.assignmentstate = AssignementState.TickedAndReadyForBattle;
            this.SetNewTransferState();
          }
        }
        else if (this.CurrentTransfer <= 0)
        {
          this.assignmentstate = AssignementState.NothingTicked;
          this.SetNewTransferState();
        }
      }
      else
      {
        this.BlockAssign = this.CurrentTransfer >= this.MaximumFreeSlots;
        if (this.assignmentstate == AssignementState.NothingTicked)
        {
          if (this.CurrentTransfer > 0)
          {
            this.assignmentstate = !this.BlockAssign ? AssignementState.TickedAndReadyToTransferToHoldingCells : AssignementState.TickedButAtMax;
            this.SetNewTransferState();
          }
        }
        else if (this.CurrentTransfer == 0 && this.assignmentstate != AssignementState.NothingTicked)
        {
          this.assignmentstate = AssignementState.NothingTicked;
          this.SetNewTransferState();
        }
      }
      for (int index = 0; index < this.prisonericons.Count; ++index)
      {
        if ((double) (this.prisonericons[index].Location + Offset + this.springdrag.CurrentOffset).Y >= 200.0)
          this.prisonericons[index].UpdatePrisonerIconSwitcher(this.springdrag.CurrentOffset, DeltaTime, player, this.BlockAssign, this.ControlelrSelected == index && GameFlags.IsUsingController);
      }
      return flag;
    }

    public void DrawPrisonerAndButtons(Vector2 Offset)
    {
      for (int index = 0; index < this.prisonericons.Count; ++index)
      {
        if ((double) (this.prisonericons[index].Location + Offset + this.springdrag.CurrentOffset).Y >= 200.0)
          this.prisonericons[index].DrawPrisonerIconSwitcher(Offset + this.springdrag.CurrentOffset, this.BlockAssign, this.ControlelrSelected == index && GameFlags.IsUsingController);
      }
      this.transferconfirm.DrawTransferConfirmPanel(Offset);
    }
  }
}
