// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.MoveBuilding.Z_MoveBuildingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BuldMenu.DragBuilder;

namespace TinyZoo.Z_BuldMenu.MoveBuilding
{
  internal class Z_MoveBuildingManager
  {
    private CATEGORYTYPE category;
    private TILETYPE BuildingThis;
    private Vector2Int OriginalLocation;
    private Z_DragBuildManager z_dragbuilder;
    private CancelMove cancelmove;
    private TileRenderer reverttilerenderer;
    private ShopEntry originalshop;
    private PrisonZone REF_MoveInsideThisEnclosure;
    private bool IsMovingDynamicPenItem;
    public bool MovingShopWithStats;

    public Z_MoveBuildingManager(
      LayoutEntry SellingThislayout,
      Vector2Int Selllocation,
      Player player,
      OverWorldEnvironmentManager overworldenvironment,
      PrisonZone MoveInsideThisEnclosure = null,
      bool _CameFromMainBarManager = false,
      bool WasDestroyButton = false,
      bool _IsMovingDynamicPenItem = false,
      bool IsPenItem = false)
    {
      this.MovingShopWithStats = false;
      this.REF_MoveInsideThisEnclosure = MoveInsideThisEnclosure;
      this.IsMovingDynamicPenItem = _IsMovingDynamicPenItem;
      if (TileData.IsThisAShopWithShopStats(SellingThislayout.tiletype))
      {
        this.MovingShopWithStats = true;
        this.originalshop = player.shopstatus.GetThisShop(Selllocation, SellingThislayout.tiletype);
        int num = WasDestroyButton ? 1 : 0;
        this.originalshop.OnDestroyThisShop(player, !WasDestroyButton);
      }
      else if (TileData.IsThisAFacility(SellingThislayout.tiletype))
      {
        this.MovingShopWithStats = true;
        this.originalshop = player.shopstatus.GetThisFacility(Selllocation, SellingThislayout.tiletype);
        int num = WasDestroyButton ? 1 : 0;
        this.originalshop.OnDestroyThisShop(player, !WasDestroyButton);
      }
      else if (TileData.IsAnArchitectOffice(SellingThislayout.tiletype))
      {
        this.MovingShopWithStats = true;
        this.originalshop = player.shopstatus.GetThisArchitectsOffice(Selllocation, SellingThislayout.tiletype);
        int num = WasDestroyButton ? 1 : 0;
        this.originalshop.OnDestroyThisShop(player, !WasDestroyButton);
      }
      else if (WasDestroyButton && !TileData.IsThisABench(SellingThislayout.tiletype) && !TileData.IsThisABin(SellingThislayout.tiletype))
        TileData.IsThisaToilet(SellingThislayout.tiletype);
      this.cancelmove = new CancelMove();
      this.BuildingThis = SellingThislayout.tiletype;
      this.reverttilerenderer = new TileRenderer(SellingThislayout, Selllocation.X, Selllocation.Y, false, true);
      this.z_dragbuilder = new Z_DragBuildManager(this.BuildingThis, player, false, _IsMove: true, _decoratethisprisonzone: MoveInsideThisEnclosure, _CameFromMainBarManager: _CameFromMainBarManager);
      this.category = TileData.GetTileInfo(this.BuildingThis).categorytype;
      LayoutEntry _layoutentry = new LayoutEntry(SellingThislayout.tiletype);
      _layoutentry.RotationClockWise = SellingThislayout.RotationClockWise;
      if (SellingThislayout.isChild())
      {
        _layoutentry.SetChild(SellingThislayout.GetParentLocation(), SellingThislayout.tiletype);
        this.OriginalLocation = SellingThislayout.GetParentLocation();
      }
      else
      {
        this.OriginalLocation = new Vector2Int(Selllocation);
        _layoutentry.UnsetChild();
      }
      if (this.IsMovingDynamicPenItem)
        return;
      player.prisonlayout.SellStructure(Selllocation, _layoutentry, player.livestats.consumptionstatus, player, !WasDestroyButton, IsPenItem);
      overworldenvironment.SellStructure(Selllocation, _layoutentry, player.prisonlayout.layout);
    }

    public bool UpdateZ_MoveBuildingManager(
      float DeltaTime,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      if (this.cancelmove.UpdateCancelMove(DeltaTime, player))
      {
        if (this.REF_MoveInsideThisEnclosure != null)
        {
          bool IsDynamicItemForPen;
          ENRICHMENTBEHAVIOUR enrchmentbehaviour;
          PenItem penitem = this.REF_MoveInsideThisEnclosure.penItems.AddNewItem(this.reverttilerenderer, out IsDynamicItemForPen, out enrchmentbehaviour);
          if (IsDynamicItemForPen || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
          {
            ZooBuildingTopRenderer toprenderer = (ZooBuildingTopRenderer) null;
            TileInfo tileInfo = TileData.GetTileInfo(this.reverttilerenderer.tiletypeonconstruct);
            if (tileInfo.HasBuildingLayer)
              toprenderer = new ZooBuildingTopRenderer(tileInfo, this.reverttilerenderer.TileLocation.X, this.reverttilerenderer.TileLocation.Y, this.reverttilerenderer.RotationOnConstruct, (TileRenderer) null);
            OverWorldManager.overworldenvironment.animalsinpens.AddDynamicItemToCellBlock(this.reverttilerenderer, this.REF_MoveInsideThisEnclosure.Cell_UID, toprenderer, enrchmentbehaviour, penitem);
            return true;
          }
        }
        player.prisonlayout.BuildTileFromTileRenderer(this.reverttilerenderer, player.livestats.consumptionstatus, player);
        player.shopstatus.BuiltABuilding(this.reverttilerenderer.TileLocation, this.reverttilerenderer.tiletypeonconstruct, this.reverttilerenderer.RotationOnConstruct, player, false, out int _);
        wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout);
        player.player.touchinput.ReleaseTapArray[0].X = -1000f;
        if (this.originalshop != null)
          player.shopstatus.ReplaceThisShopOnMove(this.reverttilerenderer.TileLocation, this.reverttilerenderer.tiletypeonconstruct, this.originalshop);
        return true;
      }
      bool Built;
      this.z_dragbuilder.UpdateZ_DragBuildManager(player, wallsandfloors, out Built, DeltaTime, out bool _, Z_GameFlags.MouseIsOverAPanel, out bool _);
      if (!Built)
        return false;
      ShopEntry originalshop = this.originalshop;
      if (!this.z_dragbuilder.CameFromMainBarManager)
        Z_GameFlags.HandleNullingselectedtileandsell();
      return true;
    }

    public void DrawZ_MoveBuildingManager(TileRenderer[,] tilesasarray)
    {
      this.z_dragbuilder.DrawZ_DragBuildManager(false);
      this.cancelmove.DrawCancelMove();
    }
  }
}
