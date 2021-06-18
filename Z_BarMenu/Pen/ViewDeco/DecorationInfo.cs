// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.ViewDeco.DecorationInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BuldMenu.MoveBuilding;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.SellStructure;

namespace TinyZoo.Z_BarMenu.Pen.ViewDeco
{
  internal class DecorationInfo
  {
    private MainBarManager barmanager;
    private PenItem penitem;

    public DecorationInfo(Player player, PenItem _penitem)
    {
      this.penitem = _penitem;
      this.barmanager = new MainBarManager(BAR_TYPE.Pen_ViewSpecificItem, TILETYPE.None, false, player);
    }

    public bool UpdateDecorationInfo(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors,
      PrisonZone prisonzone,
      OverworldBuildManager buildmanager,
      OverWorldEnvironmentManager overworldenvironment)
    {
      this.penitem.IsSelectedInEditView = true;
      Z_GameFlags.SelectedPenItem = this.penitem;
      bool GoBack;
      BuildingManageButton buildingManageButton = this.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
        return true;
      switch (buildingManageButton)
      {
        case BuildingManageButton.Move:
          LayoutEntry SellingThislayout = new LayoutEntry(this.penitem.tiletype);
          if (TileData.ThisIsADynamicEnrichmentItem(this.penitem.tiletype, out ENRICHMENTBEHAVIOUR _))
          {
            prisonzone.penItems.RemoveItem(this.penitem.Location, this.penitem.tiletype, prisonzone.Cell_UID, player, this.penitem);
            OverWorldManager.movebuilding = new Z_MoveBuildingManager(SellingThislayout, this.penitem.Location, player, overworldenvironment, prisonzone, true, _IsMovingDynamicPenItem: true);
            OverWorldManager.overworldstate = OverWOrldState.MoveBuilding;
            Z_SellStructureManager.IsMove = false;
            Z_GameFlags.IsMovingSomething = false;
            break;
          }
          prisonzone.penItems.RemoveItem(this.penitem.Location, this.penitem.tiletype, prisonzone.Cell_UID, player);
          OverWorldManager.movebuilding = new Z_MoveBuildingManager(SellingThislayout, this.penitem.Location, player, overworldenvironment, prisonzone, true, IsPenItem: true);
          OverWorldManager.overworldstate = OverWOrldState.MoveBuilding;
          Z_SellStructureManager.IsMove = false;
          Z_GameFlags.IsMovingSomething = false;
          break;
        case BuildingManageButton.Destroy:
          if (TileData.ThisIsADynamicEnrichmentItem(this.penitem.tiletype, out ENRICHMENTBEHAVIOUR _))
          {
            prisonzone.penItems.RemoveItem(this.penitem.Location, this.penitem.tiletype, prisonzone.Cell_UID, player, this.penitem);
          }
          else
          {
            prisonzone.penItems.RemoveItem(this.penitem.Location, this.penitem.tiletype, prisonzone.Cell_UID, player);
            int CashRefund = 0;
            SellUIManager.DestroyBuildingStatic(player, OverWorldManager.overworldenvironment, new LayoutEntry(this.penitem.tiletype), this.penitem.Location, ref CashRefund, prisonzone != null);
          }
          return true;
      }
      return false;
    }

    public void DrawDecorationInfo(Player player) => this.barmanager.DrawMainBarManager(player);
  }
}
