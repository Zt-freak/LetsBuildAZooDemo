// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.SelectedPens
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;

namespace TinyZoo.Z_EditZone.SelectedZones
{
  internal class SelectedPens
  {
    private List<SelectionOutline> pens;
    private WorkZoneInfo REF_workzoneinfo;

    public SelectedPens(WorkZoneInfo _workzoneinfo, Player player)
    {
      this.REF_workzoneinfo = _workzoneinfo;
      this.pens = new List<SelectionOutline>();
      for (int index = 0; index < _workzoneinfo.PenUIDs.Count; ++index)
        this.AddPenFroMPrisonZone(player.prisonlayout.cellblockcontainer.GetThisCellBlock(_workzoneinfo.PenUIDs[index]), player);
    }

    public int GetNumberOfZones() => this.REF_workzoneinfo.PenUIDs.Count;

    private void AddPenFroMPrisonZone(PrisonZone pz, Player player)
    {
      TILETYPE tiletype = player.prisonlayout.layout.FloorTileTypes[pz.FloorTiles[0].X, pz.FloorTiles[0].Y].tiletype;
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      this.pens.Add(new SelectionOutline(pz.FloorTiles[0], tileInfo, pz, (GraveYardBlockInfo) null, tiletype, 0));
    }

    public void UpdateSelectedPens(Player player)
    {
      if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
      {
        Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.player.touchinput.ReleaseTapArray[0]);
        if (Z_GameFlags.Location_Directory.GetCursorThing(spaceToTileLocation.X, spaceToTileLocation.Y, player) == CURSOR_ACTIONTYPE.SelectedPen)
        {
          PrisonZone thisCellBlock = player.prisonlayout.cellblockcontainer.GetThisCellBlock(spaceToTileLocation);
          if (!this.RemovePen(thisCellBlock.Cell_UID))
          {
            if (this.REF_workzoneinfo.PenUIDs.Count < this.REF_workzoneinfo.ZoneCap || this.REF_workzoneinfo.ZoneCap == -1)
            {
              this.REF_workzoneinfo.AddPenID(thisCellBlock.Cell_UID);
              this.AddPenFroMPrisonZone(thisCellBlock, player);
            }
            else if (this.REF_workzoneinfo.PenUIDs.Count > 0)
            {
              this.RemovePen(this.REF_workzoneinfo.PenUIDs[0]);
              this.REF_workzoneinfo.AddPenID(thisCellBlock.Cell_UID);
              this.AddPenFroMPrisonZone(thisCellBlock, player);
            }
          }
        }
      }
      for (int index = 0; index < this.pens.Count; ++index)
        this.pens[index].UpdateSelectionOutline();
    }

    private bool RemovePen(int Cell_UID)
    {
      for (int index1 = this.pens.Count - 1; index1 > -1; --index1)
      {
        if (this.pens[index1].PrizonZoneUID == Cell_UID)
        {
          for (int index2 = this.REF_workzoneinfo.PenUIDs.Count - 1; index2 > -1; --index2)
          {
            if (this.REF_workzoneinfo.PenUIDs[index2] == Cell_UID)
            {
              this.REF_workzoneinfo.PenUIDs.RemoveAt(index2);
              break;
            }
          }
          this.pens.RemoveAt(index1);
          return true;
        }
      }
      return false;
    }

    public void DrawSelectedPens()
    {
      for (int index = 0; index < this.pens.Count; ++index)
        this.pens[index].DrawSelectionOutline();
    }
  }
}
