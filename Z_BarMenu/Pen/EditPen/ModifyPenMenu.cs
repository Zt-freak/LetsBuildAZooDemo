// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.EditPen.ModifyPenMenu
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.EditPen
{
  internal class ModifyPenMenu
  {
    private MainBarManager barmanager;

    public ModifyPenMenu(Player player) => this.barmanager = new MainBarManager(BAR_TYPE.Pen_EditPen, TILETYPE.None, false, player);

    public bool IsMouseOverButton(Player player) => this.barmanager.CheckMouseOver(player);

    public bool IsCurrentlyMovingThisPen(int PENUID) => ObjectInfoPanel.z_penbuilder != null && ObjectInfoPanel.z_penbuilder.IsCurrentlyMovingThisPen(PENUID);

    public bool UpdateModifyPenMenu(
      Player player,
      float DeltaTime,
      PrisonZone prisonzone,
      OverWorldEnvironmentManager overworldenvironment,
      Vector2Int SelectedTileLocation)
    {
      if (player.inputmap.PressedBackOnController())
        return true;
      bool GoBack;
      BuildingManageButton actiontype = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
        return true;
      switch (actiontype)
      {
        case BuildingManageButton.Move:
          if (OverWorldEnvironmentManager.airspacemanager.IsSomethingOnOrderToThisPen(prisonzone.Cell_UID))
          {
            this.barmanager.FlashRed(BuildingManageButton.Move);
            break;
          }
          Z_GameFlags.ForceToNewScreen = ForceToNewScreen.MovePen;
          SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
          break;
        case BuildingManageButton.Destroy:
          if (prisonzone != null)
          {
            if (OverWorldEnvironmentManager.airspacemanager.IsSomethingOnOrderToThisPen(prisonzone.Cell_UID))
            {
              this.barmanager.FlashRed(BuildingManageButton.Destroy);
              break;
            }
            OverWorldManager.zoopopupHolder.CreateZooPopUps(actiontype, prisonzone, player);
            break;
          }
          break;
        case BuildingManageButton.MoveGate:
          Z_GameFlags.ForceToNewScreen = ForceToNewScreen.MoveGate;
          break;
      }
      return false;
    }

    public void DrawModifyPenMenu(Player player) => this.barmanager.DrawMainBarManager(player);
  }
}
