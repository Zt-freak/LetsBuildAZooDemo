// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SelectCell.CellInfoManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.OverWorld.SelectCell.Orders;

namespace TinyZoo.OverWorld.SelectCell
{
  internal class CellInfoManager
  {
    private OrderAssignmentPanel orderAssignmentPanel;
    private List<CellInfo> cellinfo;
    private int Selected;

    public CellInfoManager(Player player)
    {
      this.Selected = -1;
      this.cellinfo = new List<CellInfo>();
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
        this.cellinfo.Add(new CellInfo(player.prisonlayout.cellblockcontainer.prisonzones[index]));
      bool flag = false;
      if (player.livestats.intakefornextlevel != null)
        flag = player.livestats.intakefornextlevel.People.Count <= player.prisonlayout.cellblockcontainer.HasSpaceInHoldingCells();
      if (player.livestats.LevelIsTransferFromHoldingCell)
        return;
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.holdingcells.Count; ++index)
      {
        bool _HasEnoughSpace = player.livestats.intakefornextlevel.People.Count <= player.prisonlayout.cellblockcontainer.holdingcells[index].GetFreeTransferSlots();
        this.cellinfo.Add(new CellInfo(player.prisonlayout.cellblockcontainer.holdingcells[index], _HasEnoughSpace));
      }
    }

    public bool CheckMouseOver(Player player) => false;

    public void UpdateCellInfoManager(Player player, float DeltaTime, ref bool ExitBackToGame)
    {
      if (GameFlags.IsUsingController)
      {
        int num1 = -1;
        float num2 = 0.0f;
        for (int index = 0; index < this.cellinfo.Count; ++index)
        {
          if (num1 == -1 || (double) this.cellinfo[index].GetDistanceFromScreenCenter() < (double) num2)
          {
            num2 = this.cellinfo[index].GetDistanceFromScreenCenter();
            num1 = index;
          }
        }
        if (num1 > -1)
        {
          if ((double) num2 < 200.0)
          {
            if (this.Selected != num1)
            {
              this.Selected = num1;
              this.DoEvent(false, num1);
            }
          }
          else if (this.Selected > -1)
          {
            this.Selected = -1;
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
            for (int index = 0; index < this.cellinfo.Count; ++index)
              this.cellinfo[num1].Deselect();
          }
        }
      }
      int selected = this.Selected;
      bool DeselectThis;
      if (this.Selected > -1 && this.cellinfo[this.Selected].UpdateCellInfo(player, DeltaTime, ref ExitBackToGame, out DeselectThis))
        this.DoEvent(DeselectThis, this.Selected);
      for (int index = 0; index < this.cellinfo.Count; ++index)
      {
        if (selected != index && this.cellinfo[index].UpdateCellInfo(player, DeltaTime, ref ExitBackToGame, out DeselectThis))
          this.DoEvent(DeselectThis, index);
      }
    }

    private void DoEvent(bool DeselectThis, int INDEX)
    {
      if (DeselectThis)
      {
        this.cellinfo[INDEX].Deselect();
        Z_GameFlags.DeselectPenInPenSelect = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
      }
      else
      {
        for (int index = 0; index < this.cellinfo.Count; ++index)
        {
          if (INDEX != index)
          {
            this.cellinfo[index].Deselect();
          }
          else
          {
            this.Selected = index;
            this.cellinfo[index].Select();
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
          }
        }
      }
    }

    public void DrawCellInfoManager()
    {
      for (int index = 0; index < this.cellinfo.Count; ++index)
      {
        if (index != this.Selected)
          this.cellinfo[index].DrawCellInfo();
      }
      if (this.Selected <= -1)
        return;
      this.cellinfo[this.Selected].DrawCellInfo();
    }
  }
}
