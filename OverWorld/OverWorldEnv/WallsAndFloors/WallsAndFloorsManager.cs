// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.WallsAndFloorsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Smear;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Data;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors
{
  internal class WallsAndFloorsManager
  {
    private List<TileRenderer> tiles;
    private List<TileRenderer> Floor;
    private List<TileRenderer> Roof;
    internal static List<ZooBuildingTopRenderer> zoobuildingrenderers;
    public TileRenderer[,] tilesasarray;
    public TileRenderer[,] FloorTilesArray;
    public TileRenderer[,] UnderFloorTilesArray;
    private List<TileRenderer> Undertiles;
    public List<TileRenderer> UpdatableFloors;
    private float RoofAlpha;
    private int[,] ActiveTiles;
    public SmearManager smearmanager;
    private static int RotationClowise;

    public WallsAndFloorsManager(LayoutData layoutdata)
    {
      this.UpdatableFloors = new List<TileRenderer>();
      PathFindingManager.WaitUntilInitialized = true;
      ZMapSetUp.UnlockAllExistingLand();
      WallsAndFloorsManager.zoobuildingrenderers = new List<ZooBuildingTopRenderer>();
      GameFlags.IsConstructingMapOnLoad = true;
      this.tilesasarray = new TileRenderer[layoutdata.BaseTileTypes.GetLength(0), layoutdata.BaseTileTypes.GetLength(1)];
      this.ActiveTiles = new int[layoutdata.BaseTileTypes.GetLength(0), layoutdata.BaseTileTypes.GetLength(1)];
      this.FloorTilesArray = new TileRenderer[layoutdata.BaseTileTypes.GetLength(0), layoutdata.BaseTileTypes.GetLength(1)];
      this.UnderFloorTilesArray = new TileRenderer[layoutdata.BaseTileTypes.GetLength(0), layoutdata.BaseTileTypes.GetLength(1)];
      this.smearmanager = new SmearManager();
      this.tiles = new List<TileRenderer>();
      this.Roof = new List<TileRenderer>();
      this.Floor = new List<TileRenderer>();
      this.Undertiles = new List<TileRenderer>();
      this.VallidateAgainstLayout(layoutdata);
      this.VallidateAgainstLayout(layoutdata, true);
      this.RemakeTileList();
      this.RoofAlpha = 1f;
      GameFlags.IsConstructingMapOnLoad = false;
      this.RecreatePathFinder();
      PathFindingManager.WaitUntilInitialized = false;
      Z_GameFlags.pathfinder.ConstructPathfinding();
      Z_GameFlags.HasBuiltStoreRoom = false;
      bool flag = false;
      for (int index1 = 0; index1 < layoutdata.BaseTileTypes.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < layoutdata.BaseTileTypes.GetLength(0); ++index2)
        {
          this.ActiveTiles[index2, index1] = -1;
          if (!flag && layoutdata.BaseTileTypes[index2, index1] != null && !layoutdata.BaseTileTypes[index2, index1].isChild())
          {
            if (TileData.IsAManagementOffice(layoutdata.BaseTileTypes[index2, index1].tiletype))
              PointOffScreenManager.SetTaskPointerLocation(index2, index1);
            else if (TileData.IsAStoreRoom(layoutdata.BaseTileTypes[index2, index1].tiletype))
              Z_GameFlags.HasBuiltStoreRoom = true;
            else if (TileData.IsThisAWarpGate(layoutdata.BaseTileTypes[index2, index1].tiletype))
              Z_GameFlags.pathfinder.CreateWarpGate(new Vector2Int(index2, index1) + TileData.GetTileInfo(layoutdata.BaseTileTypes[index2, index1].tiletype).GetPurchasingLocation(layoutdata.BaseTileTypes[index2, index1].RotationClockWise));
          }
        }
      }
    }

    public void SetUpFarmSigns(Player player, int CellBlockUID = -1) => player.farms.SetUpFarmSigns(player, CellBlockUID);

    public void SetUpAnimalsOnOrder(Player player, int CellBlockUID = -1)
    {
      if (CellBlockUID == -1)
      {
        for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
          player.prisonlayout.cellblockcontainer.prisonzones[index].CheckOrderSign(player);
      }
      else
        player.prisonlayout.cellblockcontainer.GetThisCellBlock(CellBlockUID).CheckOrderSign(player);
    }

    public void RecreatePathFinder()
    {
      Vector2Int TopLeft;
      Vector2Int BottomRight;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out TopLeft, out BottomRight, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      for (int x = TopLeft.X; x < BottomRight.X + 1; ++x)
      {
        for (int y = TopLeft.Y; y < BottomRight.Y + 1; ++y)
        {
          if (this.tilesasarray[x, y] != null)
          {
            if (this.tilesasarray[x, y].Ref_layoutentry.tiletype == TILETYPE.EMPTY_DIRT_WALKABLE_TILE)
              Z_GameFlags.pathfinder.UnblockTile(x, y, true);
          }
          else
          {
            bool flag = false;
            if (this.FloorTilesArray[x, y] != null && TileData.IsThisWater(this.FloorTilesArray[x, y].tiletypeonconstruct))
            {
              flag = true;
              Z_GameFlags.pathfinder.BlockWaterTile(x, y);
            }
            if (!flag)
              Z_GameFlags.pathfinder.UnblockTile(x, y, true);
          }
        }
      }
      Z_GameFlags.pathfinder.ForceResolvePathFinding();
    }

    public void SetAsMoon(int XL, int YL)
    {
      this.tilesasarray[XL, YL] = new TileRenderer(new LayoutEntry(TILETYPE.Moon), XL, YL, false);
      this.tiles.Add(this.tilesasarray[XL, YL]);
    }

    public void SellStructure(
      Vector2Int position,
      LayoutEntry _layoutentry,
      LayoutData layoutdata,
      bool ForceSell = false,
      bool SkipRemakeList = false)
    {
      if (!ForceSell && this.tilesasarray[position.X, position.Y].Ref_layoutentry.tiletype != _layoutentry.tiletype)
        throw new Exception("CANNOT SELL ONE");
      if (SkipRemakeList)
        return;
      this.VallidateAgainstLayout(layoutdata);
      this.RemakeTileList();
    }

    public void BuildTileFromLayoutEntry(LayoutEntry tileentry, int LocationX, int LocationY)
    {
      if (tileentry.isChild())
        throw new Exception("YOU DIDINT DO THIS");
      TileRenderer _parent = new TileRenderer(tileentry, LocationX, LocationY, false);
      TileInfo tileInfo = TileData.GetTileInfo(tileentry.tiletype);
      if (tileInfo.HasBuildingLayer)
      {
        _parent.RefTopRenderer = new ZooBuildingTopRenderer(tileInfo, LocationX, LocationY, tileentry.RotationClockWise, _parent);
        WallsAndFloorsManager.zoobuildingrenderers.Add(_parent.RefTopRenderer);
      }
      this.tilesasarray[LocationX, LocationY] = _parent;
    }

    public bool VallidateAgainstLayout(
      LayoutData layout,
      bool DoFloor = false,
      Vector2Int JustThisTile = null,
      bool DoRemakeTileLists = true,
      bool HyperOptimized = false)
    {
      if (HyperOptimized)
      {
        if (DoFloor)
          throw new Exception("Not Written For Floor Yet");
        if (this.tilesasarray[JustThisTile.X, JustThisTile.Y] != null)
        {
          TileInfo tileInfo = TileData.GetTileInfo(layout.BaseTileTypes[JustThisTile.X, JustThisTile.Y].tiletype);
          if (!tileInfo.HasBuildingLayer || this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer != null)
          {
            ZooBuildingTopRenderer buildingTopRenderer = (ZooBuildingTopRenderer) null;
            if (tileInfo.HasBuildingLayer && this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer != null)
              buildingTopRenderer = this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer;
            else if (!tileInfo.HasBuildingLayer && this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer != null)
              WallsAndFloorsManager.zoobuildingrenderers.Remove(this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer);
            if (layout.BaseTileTypes[JustThisTile.X, JustThisTile.Y].tiletype == TILETYPE.None)
            {
              this.tiles.Remove(this.tilesasarray[JustThisTile.X, JustThisTile.Y]);
              this.tilesasarray[JustThisTile.X, JustThisTile.Y] = (TileRenderer) null;
            }
            else
            {
              this.tilesasarray[JustThisTile.X, JustThisTile.Y].Reconstruct(layout.BaseTileTypes[JustThisTile.X, JustThisTile.Y], JustThisTile.X, JustThisTile.Y, layout.BaseTileTypes[JustThisTile.X, JustThisTile.Y].isChild());
              buildingTopRenderer?.Reconstruct(tileInfo, JustThisTile.X, JustThisTile.Y, layout.BaseTileTypes[JustThisTile.X, JustThisTile.Y].RotationClockWise, this.tilesasarray[JustThisTile.X, JustThisTile.Y]);
              this.tilesasarray[JustThisTile.X, JustThisTile.Y].RefTopRenderer = buildingTopRenderer;
            }
            return true;
          }
        }
      }
      int num1 = 0;
      int num2 = this.FloorTilesArray.GetLength(0);
      int num3 = 0;
      int num4 = this.FloorTilesArray.GetLength(1);
      if (JustThisTile != null)
      {
        num1 = JustThisTile.X;
        num2 = num1 + 1;
        num3 = JustThisTile.Y;
        num4 = num3 + 1;
      }
      if (DoFloor)
      {
        for (int index1 = num3; index1 < num4; ++index1)
        {
          for (int index2 = num1; index2 < num2; ++index2)
          {
            if (layout.FloorTileTypes[index2, index1] != null)
            {
              if (layout.FloorTileTypes[index2, index1].tiletype != TILETYPE.None)
              {
                if (TileData.IsThisWater(layout.FloorTileTypes[index2, index1].tiletype))
                {
                  if (!Z_GameFlags.pathfinder.GetIsBlocked(index2, index1))
                    Z_GameFlags.pathfinder.BlockTile(index2, index1, true);
                  if (!Z_GameFlags.pathfinder.GetIsBlockedWater(index2, index1))
                    Z_GameFlags.pathfinder.BlockWaterTile(index2, index1);
                }
                if (this.FloorTilesArray[index2, index1] == null)
                {
                  this.FloorTilesArray[index2, index1] = new TileRenderer(layout.FloorTileTypes[index2, index1], index2, index1, false, IsFloor_Trailer: true);
                  if (TileData.DoesThisFloorAnimate(layout.FloorTileTypes[index2, index1].tiletype))
                    this.Floor.Add(this.FloorTilesArray[index2, index1]);
                  if (layout.FloorTileTypes[index2, index1].RotationClockWise > 0 && layout.FloorTileTypes[index2, index1].UnderFloorTiletype != TILETYPE.None)
                  {
                    LayoutEntry layourentry = new LayoutEntry(layout.FloorTileTypes[index2, index1].UnderFloorTiletype);
                    this.UnderFloorTilesArray[index2, index1] = new TileRenderer(layourentry, index2, index1, false, IsUnderfloor: true);
                  }
                }
              }
              else if (this.FloorTilesArray[index2, index1] != null)
              {
                if (TileData.DoesThisFloorAnimate(this.FloorTilesArray[index2, index1].tiletypeonconstruct))
                  this.Floor.Remove(this.FloorTilesArray[index2, index1]);
                this.FloorTilesArray[index2, index1] = (TileRenderer) null;
                this.UnderFloorTilesArray[index2, index1] = (TileRenderer) null;
              }
              if (this.FloorTilesArray[index2, index1] != null)
              {
                if (this.FloorTilesArray[index2, index1].tiletypeonconstruct != layout.FloorTileTypes[index2, index1].tiletype || this.FloorTilesArray[index2, index1].RotationOnConstruct != layout.FloorTileTypes[index2, index1].RotationClockWise)
                {
                  this.FloorTilesArray[index2, index1] = new TileRenderer(layout.FloorTileTypes[index2, index1], index2, index1, layout.FloorTileTypes[index2, index1].isChild(), IsFloor_Trailer: true);
                  if (TileData.DoesThisFloorAnimate(layout.FloorTileTypes[index2, index1].tiletype))
                    this.Floor.Add(this.FloorTilesArray[index2, index1]);
                  if (layout.FloorTileTypes[index2, index1].RotationClockWise > 0 && layout.FloorTileTypes[index2, index1].UnderFloorTiletype != TILETYPE.None && TileData.IsThisAPartialFloor(layout.FloorTileTypes[index2, index1]))
                  {
                    LayoutEntry layourentry = new LayoutEntry(layout.FloorTileTypes[index2, index1].UnderFloorTiletype);
                    this.UnderFloorTilesArray[index2, index1] = new TileRenderer(layourentry, index2, index1, false);
                  }
                  if (layout.FloorTileTypes[index2, index1].UnderFloorTiletype != TILETYPE.None)
                  {
                    if ((this.UnderFloorTilesArray[index2, index1] == null || this.UnderFloorTilesArray[index2, index1].tiletypeonconstruct != layout.FloorTileTypes[index2, index1].UnderFloorTiletype) && TileData.IsThisAPartialFloor(layout.FloorTileTypes[index2, index1]))
                    {
                      LayoutEntry layourentry = new LayoutEntry(layout.FloorTileTypes[index2, index1].UnderFloorTiletype);
                      this.UnderFloorTilesArray[index2, index1] = new TileRenderer(layourentry, index2, index1, false, IsUnderfloor: true);
                    }
                  }
                  else
                    this.UnderFloorTilesArray[index2, index1] = (TileRenderer) null;
                }
                else if (layout.FloorTileTypes[index2, index1].UnderFloorTiletype != TILETYPE.None && (this.UnderFloorTilesArray[index2, index1] == null || this.UnderFloorTilesArray[index2, index1].tiletypeonconstruct != layout.FloorTileTypes[index2, index1].UnderFloorTiletype) && TileData.IsThisAPartialFloor(layout.FloorTileTypes[index2, index1]))
                {
                  LayoutEntry layourentry = new LayoutEntry(layout.FloorTileTypes[index2, index1].UnderFloorTiletype);
                  this.UnderFloorTilesArray[index2, index1] = new TileRenderer(layourentry, index2, index1, false);
                }
              }
            }
          }
        }
      }
      else
      {
        LayoutEntry layoutEntry = new LayoutEntry(TILETYPE.None);
        bool flag = false;
        WallsAndFloorsManager.zoobuildingrenderers = new List<ZooBuildingTopRenderer>();
        for (int index1 = num3; index1 < num4; ++index1)
        {
          for (int index2 = num1; index2 < num2; ++index2)
          {
            if (this.tilesasarray[index2, index1] != null)
            {
              if (this.tilesasarray[index2, index1].RotationOnConstruct != layout.BaseTileTypes[index2, index1].RotationClockWise || this.tilesasarray[index2, index1].tiletypeonconstruct != layout.BaseTileTypes[index2, index1].tiletype)
              {
                if (layout.BaseTileTypes[index2, index1].tiletype == TILETYPE.None)
                {
                  flag = true;
                  this.tilesasarray[index2, index1] = (TileRenderer) null;
                }
                else
                {
                  flag = true;
                  this.tilesasarray[index2, index1] = new TileRenderer(layout.BaseTileTypes[index2, index1], index2, index1, layout.BaseTileTypes[index2, index1].isChild());
                }
              }
              if (layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.None && this.tilesasarray[index2, index1].Refinfo != null && (this.tilesasarray[index2, index1].Refinfo.HasBuildingLayer && this.tilesasarray[index2, index1].RefTopRenderer == null))
              {
                this.tilesasarray[index2, index1].RefTopRenderer = new ZooBuildingTopRenderer(this.tilesasarray[index2, index1].Refinfo, index2, index1, this.tilesasarray[index2, index1].RotationOnConstruct, this.tilesasarray[index2, index1]);
                WallsAndFloorsManager.zoobuildingrenderers.Add(this.tilesasarray[index2, index1].RefTopRenderer);
              }
            }
            else if (layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.None)
            {
              if (layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.PinkMoonPlant && layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.BoundaryTree && layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.DefaultFence_WallSide)
              {
                int tiletype = (int) layout.BaseTileTypes[index2, index1].tiletype;
              }
              flag = true;
              this.tilesasarray[index2, index1] = new TileRenderer(layout.BaseTileTypes[index2, index1], index2, index1, layout.BaseTileTypes[index2, index1].isChild());
            }
            if (layout.BaseTileTypes[index2, index1].tiletype != TILETYPE.None)
              TileData.GetHasRoof(layout.BaseTileTypes[index2, index1]);
          }
        }
        if (DoRemakeTileLists & flag)
          this.RemakeTileList();
      }
      return false;
    }

    public void BuildTileFromTileRenderer(TileRenderer tilerenderer, Player player)
    {
      TileRenderer tileRenderer = new TileRenderer(tilerenderer.Ref_layoutentry, tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y, false);
      this.tilesasarray[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y] = tileRenderer;
      for (int XL = tilerenderer.TileLocation.X - tilerenderer.XOrigin; XL < tilerenderer.TileLocation.X - tilerenderer.XOrigin + tilerenderer.XWidth; ++XL)
      {
        for (int YL = tilerenderer.TileLocation.Y - tilerenderer.YOrigin; YL < tilerenderer.TileLocation.Y - tilerenderer.YOrigin + tilerenderer.YHeight; ++YL)
        {
          if (XL != tilerenderer.TileLocation.X || YL != tilerenderer.TileLocation.Y)
            this.tilesasarray[XL, YL] = new TileRenderer(tilerenderer.Ref_layoutentry, XL, YL, true);
        }
      }
      this.tilesasarray[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y] = tileRenderer;
      this.RemakeTileList();
    }

    public void TryToReplaceTileInList(int XLoc, int YLoc)
    {
    }

    public void RemakeTileList()
    {
      if (WallsAndFloorsManager.zoobuildingrenderers != null && WallsAndFloorsManager.zoobuildingrenderers.Count > 0)
        WallsAndFloorsManager.zoobuildingrenderers.Sort(new Comparison<ZooBuildingTopRenderer>(ZooBuildingTopRenderer.SortThese));
      this.tiles = new List<TileRenderer>();
      this.Roof = new List<TileRenderer>();
      for (int index1 = 0; index1 < this.tilesasarray.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < this.tilesasarray.GetLength(0); ++index2)
        {
          this.ActiveTiles[index2, index1] = -1;
          if (this.tilesasarray[index2, index1] != null && !this.tilesasarray[index2, index1].Ref_layoutentry.isChild() && TileData.ThisStructureBlocksPathFinding(this.tilesasarray[index2, index1].Ref_layoutentry.tiletype))
          {
            WallsAndFloorsManager.RotationClowise = this.tilesasarray[index2, index1].Ref_layoutentry.RotationClockWise;
            for (int Xloc = index2 - this.tilesasarray[index2, index1].Refinfo.GetXTileOrigin(WallsAndFloorsManager.RotationClowise); Xloc < index2 - this.tilesasarray[index2, index1].Refinfo.GetXTileOrigin(WallsAndFloorsManager.RotationClowise) + this.tilesasarray[index2, index1].Refinfo.GetTileWidth(WallsAndFloorsManager.RotationClowise); ++Xloc)
            {
              for (int YLoc = index1 - this.tilesasarray[index2, index1].Refinfo.GetYTileOrigin(WallsAndFloorsManager.RotationClowise); YLoc < index1 - this.tilesasarray[index2, index1].Refinfo.GetYTileOrigin(WallsAndFloorsManager.RotationClowise) + this.tilesasarray[index2, index1].Refinfo.GetTileHeight(WallsAndFloorsManager.RotationClowise); ++YLoc)
                Z_GameFlags.pathfinder.BlockTile(Xloc, YLoc, true);
            }
            List<Vector2Int> footPrintHoles = this.tilesasarray[index2, index1].Refinfo.GetFootPrintHoles(this.tilesasarray[index2, index1].RotationOnConstruct);
            for (int index3 = 0; index3 < footPrintHoles.Count; ++index3)
            {
              Z_GameFlags.pathfinder.UnblockTile(footPrintHoles[index3].X + this.tilesasarray[index2, index1].TileLocation.X, footPrintHoles[index3].Y + this.tilesasarray[index2, index1].TileLocation.Y, true);
              GameFlags.CollisionChanged = true;
            }
          }
          if (this.tilesasarray[index2, index1] != null && !this.tilesasarray[index2, index1].Ref_layoutentry.isChild())
          {
            this.tiles.Add(this.tilesasarray[index2, index1]);
            if (this.tilesasarray[index2, index1].Ref_layoutentry != null && this.tilesasarray[index2, index1].Ref_layoutentry.tiletype != TILETYPE.PinkMoonPlant && this.tilesasarray[index2, index1].Ref_layoutentry.tiletype != TILETYPE.BoundaryTree)
              this.ActivateTile(index2, index1);
            if (this.tilesasarray[index2, index1].Refinfo != null && this.tilesasarray[index2, index1].Refinfo.HasBuildingLayer)
            {
              if (this.tilesasarray[index2, index1].RefTopRenderer == null)
              {
                this.tilesasarray[index2, index1].RefTopRenderer = new ZooBuildingTopRenderer(this.tilesasarray[index2, index1].Refinfo, index2, index1, this.tilesasarray[index2, index1].RotationOnConstruct, this.tilesasarray[index2, index1]);
                WallsAndFloorsManager.zoobuildingrenderers.Add(this.tilesasarray[index2, index1].RefTopRenderer);
                this.tilesasarray[index2, index1].RefTopRenderer.RefParent = this.tilesasarray[index2, index1];
                this.tilesasarray[index2, index1].RefTopRenderer.SetUpCoponentsAFterLinkingToTile(this.tilesasarray[index2, index1]);
              }
              else
              {
                if (this.tilesasarray[index2, index1].RefTopRenderer.buildingtype != this.tilesasarray[index2, index1].Refinfo.buildingtype)
                  throw new Exception("NO DOUBLE ADD");
                if (!WallsAndFloorsManager.zoobuildingrenderers.Contains(this.tilesasarray[index2, index1].RefTopRenderer))
                  WallsAndFloorsManager.zoobuildingrenderers.Add(this.tilesasarray[index2, index1].RefTopRenderer);
              }
            }
          }
        }
      }
      if (DebugFlags.UseTileLights)
      {
        for (int index1 = 0; index1 < this.tilesasarray.GetLength(1); ++index1)
        {
          for (int index2 = 0; index2 < this.tilesasarray.GetLength(0); ++index2)
          {
            float num1 = 0.2f;
            float num2 = 0.2f;
            if (this.ActiveTiles[index2, index1] != -1)
            {
              float activeTile = (float) this.ActiveTiles[index2, index1];
              num2 = (float) (1.0 - (double) num1 - (double) activeTile / (double) TileMath.FadeOutRange * (1.0 - (double) num1)) + num1;
            }
            if (this.tilesasarray[index2, index1] != null && this.tilesasarray[index2, index1].Ref_layoutentry != null && (this.tilesasarray[index2, index1].Ref_layoutentry.tiletype == TILETYPE.PinkMoonPlant || this.tilesasarray[index2, index1].Ref_layoutentry.tiletype == TILETYPE.BoundaryTree))
              this.tilesasarray[index2, index1].SetAllColours(num2, num2, num2);
          }
        }
      }
      if (PathFindingManager.WaitUntilInitialized)
        return;
      Z_GameFlags.pathfinder.ForceResolvePathFinding();
    }

    public void ActivateTile(int XLoc, int YLoc)
    {
    }

    public BlockInfo CanBuildThisHere(
      Vector2Int Location,
      TileRenderer renderer,
      bool IsFloor = false,
      bool CheckingForBuildInPen = false,
      PrisonZone RestrictToThisPrisonZone = null,
      bool CheckHasAccesToWaterSupply = false,
      LayoutData layoudata = null)
    {
      BlockInfo block = new BlockInfo(renderer.Refinfo.buildingtype);
      List<Vector2Int> entrances = renderer.Refinfo.GetEntrances(renderer.RotationOnConstruct);
      if (IsFloor)
      {
        if (!TileMath.TileIsInBuildablePartOfWorld(Location.X, Location.Y, true))
        {
          block.AddBlock(new Vector2Int(Location), false);
          block.SomethingIsBlocked = true;
        }
        else if (layoudata != null && !TileData.IsThisAPenFloor(renderer.tiletypeonconstruct) && TileData.IsThisAPenFloor(layoudata.FloorTileTypes[Location.X, Location.Y].tiletype))
        {
          block.AddBlock(new Vector2Int(Location), false);
          block.SomethingIsBlocked = true;
        }
      }
      else
      {
        if (entrances != null)
        {
          for (int index = 0; index < entrances.Count; ++index)
            this.CheckLocationForBlock(entrances[index].X + Location.X, entrances[index].Y + Location.Y, CheckingForBuildInPen, RestrictToThisPrisonZone, block, CheckHasAccesToWaterSupply, renderer, true);
        }
        for (int index1 = 0; index1 < renderer.XWidth; ++index1)
        {
          for (int index2 = 0; index2 < renderer.YHeight; ++index2)
            this.CheckLocationForBlock(index1 + Location.X - renderer.XOrigin, index2 + Location.Y - renderer.YOrigin, CheckingForBuildInPen, RestrictToThisPrisonZone, block, CheckHasAccesToWaterSupply, renderer);
        }
      }
      return block;
    }

    private void CheckLocationForBlock(
      int XLocWorldSpace,
      int YLocWorlSpace,
      bool CheckingForBuildInPen,
      PrisonZone RestrictToThisPrisonZone,
      BlockInfo block,
      bool CheckHasAccesToWaterSupply,
      TileRenderer renderer,
      bool IsForEntrance = false)
    {
      TileRenderer tileRenderer = (TileRenderer) null;
      bool flag = false;
      if (XLocWorldSpace < this.tilesasarray.GetLength(0) && YLocWorlSpace < this.tilesasarray.GetLength(1) && (XLocWorldSpace > -1 && YLocWorlSpace > -1))
      {
        if (this.FloorTilesArray[XLocWorldSpace, YLocWorlSpace] != null && TileData.IsThisAPenFloor(this.FloorTilesArray[XLocWorldSpace, YLocWorlSpace].tiletypeonconstruct))
        {
          if (CheckingForBuildInPen)
          {
            if (RestrictToThisPrisonZone != null && RestrictToThisPrisonZone.ThisIsInThisEnclosure(XLocWorldSpace, YLocWorlSpace))
            {
              if (RestrictToThisPrisonZone.CanBuildDecoOnThisTile(XLocWorldSpace, YLocWorlSpace))
              {
                block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
                if (!IsForEntrance)
                  block.SomethingIsBlocked = true;
                flag = true;
              }
              else
                tileRenderer = this.tilesasarray[XLocWorldSpace, YLocWorlSpace];
            }
            else
            {
              block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
              if (!IsForEntrance)
                block.SomethingIsBlocked = true;
              flag = true;
            }
          }
          else
          {
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (!IsForEntrance)
              block.SomethingIsBlocked = true;
            flag = true;
          }
          if (CheckHasAccesToWaterSupply && !OverWorldManager.heatmapmanager.GetHasWaterAccess(XLocWorldSpace, YLocWorlSpace))
            block.AddCantReachWater(new Vector2Int(XLocWorldSpace, YLocWorlSpace));
        }
        else
        {
          if (CheckingForBuildInPen)
          {
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (!IsForEntrance)
              block.SomethingIsBlocked = true;
            flag = true;
          }
          tileRenderer = this.tilesasarray[XLocWorldSpace, YLocWorlSpace];
        }
      }
      if (flag)
        return;
      switch (renderer.Refinfo.buildingtype)
      {
        case BUILDINGTYPE.Floor:
          if (tileRenderer == null)
            break;
          if (tileRenderer.Refinfo.buildingtype != BUILDINGTYPE.Floor)
          {
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (IsForEntrance)
              break;
            block.SomethingIsBlocked = true;
            break;
          }
          block.FloorReplacingFloor = true;
          break;
        case BUILDINGTYPE.Building:
          if (!TileMath.TileIsInBuildablePartOfWorld(XLocWorldSpace, YLocWorlSpace))
          {
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (IsForEntrance)
              break;
            block.SomethingIsBlocked = true;
            break;
          }
          if (tileRenderer == null)
          {
            if (XLocWorldSpace >= this.FloorTilesArray.GetLength(0) || YLocWorlSpace >= this.FloorTilesArray.GetLength(1) || (XLocWorldSpace <= -1 || YLocWorlSpace <= -1) || !TileData.IsThisWater(this.FloorTilesArray[XLocWorldSpace, YLocWorlSpace].tiletypeonconstruct))
              break;
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (IsForEntrance)
              break;
            block.SomethingIsBlocked = true;
            break;
          }
          if (tileRenderer.Refinfo != null && (tileRenderer.Refinfo.buildingtype == BUILDINGTYPE.MoonPlant || tileRenderer.Refinfo.buildingtype == BUILDINGTYPE.PrisonWall))
            break;
          block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
          if (IsForEntrance)
            break;
          block.SomethingIsBlocked = true;
          break;
        case BUILDINGTYPE.BuildOnWater:
          if (!TileMath.TileIsInBuildablePartOfWorld(XLocWorldSpace, YLocWorlSpace))
          {
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (IsForEntrance)
              break;
            block.SomethingIsBlocked = true;
            break;
          }
          if (XLocWorldSpace >= this.FloorTilesArray.GetLength(0) || YLocWorlSpace >= this.FloorTilesArray.GetLength(1) || (XLocWorldSpace <= -1 || YLocWorlSpace <= -1))
            break;
          if (XLocWorldSpace > -1 && YLocWorlSpace > -1 && (XLocWorldSpace < this.FloorTilesArray.GetLength(0) && YLocWorlSpace < this.FloorTilesArray.GetLength(1)))
          {
            if (this.FloorTilesArray[XLocWorldSpace, YLocWorlSpace] != null && !TileData.IsThisWater(this.FloorTilesArray[XLocWorldSpace, YLocWorlSpace].tiletypeonconstruct))
            {
              block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
              if (IsForEntrance)
                break;
              block.SomethingIsBlocked = true;
              break;
            }
            if (this.tilesasarray[XLocWorldSpace, YLocWorlSpace] == null || this.tilesasarray[XLocWorldSpace, YLocWorlSpace].tiletypeonconstruct == TILETYPE.None)
              break;
            block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
            if (IsForEntrance)
              break;
            block.SomethingIsBlocked = true;
            break;
          }
          block.AddBlock(new Vector2Int(XLocWorldSpace, YLocWorlSpace), IsForEntrance);
          if (IsForEntrance)
            break;
          block.SomethingIsBlocked = true;
          break;
      }
    }

    internal static bool GetThisIsnextToSomething(
      int LeftXOfStructure,
      int Top,
      int Right,
      int Bottom,
      TileRenderer[,] arrayoftiles)
    {
      for (int index = LeftXOfStructure - 1; index < Right + 1; ++index)
      {
        if (index >= 0 && index < TileMath.GetOverWorldMapSize_XDefault() && (Top > 0 && WallsAndFloorsManager.ThisTileCountsAsNextToSomething(arrayoftiles[index, Top - 1]) || Bottom < TileMath.GetOverWorldMapSize_XDefault() - 1 && WallsAndFloorsManager.ThisTileCountsAsNextToSomething(arrayoftiles[index, Bottom + 1])))
          return true;
      }
      for (int index = Top; index < Bottom; ++index)
      {
        if (index >= 0 && index < TileMath.GetOverWorldMapSize_XDefault() && (LeftXOfStructure > 0 && WallsAndFloorsManager.ThisTileCountsAsNextToSomething(arrayoftiles[LeftXOfStructure - 1, index]) || Right < TileMath.GetOverWorldMapSize_XDefault() - 1 && WallsAndFloorsManager.ThisTileCountsAsNextToSomething(arrayoftiles[Right + 1, index])))
          return true;
      }
      return false;
    }

    internal static bool ThisTileCountsAsNextToSomething(TileRenderer tilerender)
    {
      if (tilerender != null)
      {
        switch (TileData.GetTileInfo(tilerender.Ref_layoutentry.tiletype).buildingtype)
        {
          case BUILDINGTYPE.Floor:
          case BUILDINGTYPE.Wall:
          case BUILDINGTYPE.Building:
            return true;
        }
      }
      return false;
    }

    public void VallidateGraveYardAndApplyToLayout(Player player, bool ForceAll = false)
    {
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.graveblocks.Count; ++index)
      {
        if (player.prisonlayout.cellblockcontainer.graveblocks[index].PeopleChanged | ForceAll)
        {
          player.prisonlayout.cellblockcontainer.graveblocks[index].PeopleChanged = false;
          player.prisonlayout.cellblockcontainer.graveblocks[index].CheckAgainstLayoutAndMap(this.tilesasarray, player.prisonlayout);
        }
      }
    }

    public void UpdateDrawFlag()
    {
      for (int index = 0; index < this.tiles.Count; ++index)
        this.tiles[index].HasDrawn = false;
    }

    public void UpdateWallsAndFloorsManager(float DeltaTime, float SimulationTime)
    {
      this.smearmanager.UpdateSmearManager(SimulationTime);
      for (int index = 0; index < this.tiles.Count; ++index)
        this.tiles[index].UpdateTileRenderer(DeltaTime);
      if (TrailerDemoFlags.HasTrailerFlag)
      {
        for (int index1 = 0; index1 < this.FloorTilesArray.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < this.FloorTilesArray.GetLength(1); ++index2)
          {
            if (this.FloorTilesArray[index1, index2] != null)
              this.FloorTilesArray[index1, index2].UpdateTileRenderer(DeltaTime);
            if (this.UnderFloorTilesArray[index1, index2] != null)
              this.UnderFloorTilesArray[index1, index2].UpdateTileRenderer(DeltaTime);
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Floor.Count; ++index)
          this.Floor[index].UpdateTileRenderer(DeltaTime);
      }
      for (int index = 0; index < WallsAndFloorsManager.zoobuildingrenderers.Count; ++index)
        WallsAndFloorsManager.zoobuildingrenderers[index].UpdateZooBuildingTopRenderer(DeltaTime);
      for (int index = this.UpdatableFloors.Count - 1; index > -1; --index)
      {
        if (this.UpdatableFloors[index].rendercomponent == null)
          this.UpdatableFloors.RemoveAt(index);
        else
          this.UpdatableFloors[index].UpdateTileRenderer(DeltaTime);
      }
      if (!FeatureFlags.DrawRoofInOverWorld)
      {
        if ((double) this.RoofAlpha > 0.0)
        {
          this.RoofAlpha -= DeltaTime * 0.5f;
          if ((double) this.RoofAlpha < 0.0)
            this.RoofAlpha = 0.0f;
        }
      }
      else if ((double) this.RoofAlpha > 1.0)
      {
        this.RoofAlpha += DeltaTime * 0.5f;
        if ((double) this.RoofAlpha > 1.0)
          this.RoofAlpha = 1f;
      }
      Z_GameFlags.DidSomethingWithWater = false;
    }

    public void DrawFloorsManager(
      int StartX,
      int ENDX,
      int StartY,
      int ENDY,
      ref bool ThreadBool,
      SpriteBatch spritebatch,
      ref Vector2 ThreadVecLoc,
      ref Vector2 ThreadVecScale)
    {
      for (int index1 = StartX; index1 < ENDX; ++index1)
      {
        for (int index2 = StartY; index2 < ENDY; ++index2)
        {
          if (this.FloorTilesArray[index1, index2] != null)
          {
            if (this.UnderFloorTilesArray[index1, index2] != null)
            {
              this.UnderFloorTilesArray[index1, index2].HasDrawn = false;
              this.UnderFloorTilesArray[index1, index2].DrawTileRenderer(spritebatch, ref ThreadVecLoc, ref ThreadVecScale);
            }
            this.FloorTilesArray[index1, index2].HasDrawn = false;
            this.FloorTilesArray[index1, index2].DrawTileRenderer(spritebatch, ref ThreadVecLoc, ref ThreadVecScale);
          }
        }
      }
      ThreadBool = true;
    }

    public void DrawWallsAndFloorsManager(
      bool IsAfterBus,
      ref Vector2 ThreadLoc,
      ref Vector2 ThreadScale)
    {
      Vector2Int spaceToTileLocation1 = TileMath.GetScreenSPaceToTileLocation(Vector2.Zero);
      Vector2Int spaceToTileLocation2 = TileMath.GetScreenSPaceToTileLocation(Sengine.ReferenceScreenRes);
      int num1 = spaceToTileLocation1.X;
      if (num1 < 0)
        num1 = 0;
      int num2 = spaceToTileLocation1.Y;
      if (num2 < 0)
        num2 = 0;
      int y = spaceToTileLocation2.Y;
      int x = spaceToTileLocation2.X;
      int num3 = y + 2;
      int num4 = x + 1 + 1;
      int num5 = num3 + 1;
      if (IsAfterBus)
      {
        num2 = this.tilesasarray.GetLength(1) - 4;
        if (num5 < num2)
          return;
        if (num5 > this.tilesasarray.GetLength(1) - 3)
          num5 = this.tilesasarray.GetLength(1) - 3;
      }
      else if (num5 > this.tilesasarray.GetLength(1))
        num5 = this.tilesasarray.GetLength(1);
      if (num4 > this.tilesasarray.GetLength(0))
        num4 = this.tilesasarray.GetLength(0);
      for (int index1 = num1; index1 < num4; ++index1)
      {
        for (int index2 = num2; index2 < num5; ++index2)
        {
          if (this.tilesasarray[index1, index2] != null)
          {
            if (!this.tilesasarray[index1, index2].Ref_layoutentry.isChild())
              this.tilesasarray[index1, index2].DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThreadLoc, ref ThreadScale);
            else if (this.tilesasarray[index1, index2].Ref_layoutentry.isChild() && this.tilesasarray[index1, index2].Ref_layoutentry.ParentLocation != null && (this.tilesasarray[this.tilesasarray[index1, index2].Ref_layoutentry.ParentLocation.X, this.tilesasarray[index1, index2].Ref_layoutentry.ParentLocation.Y] != null && !this.tilesasarray[this.tilesasarray[index1, index2].Ref_layoutentry.ParentLocation.X, this.tilesasarray[index1, index2].Ref_layoutentry.ParentLocation.Y].HasDrawn))
              this.tilesasarray[this.tilesasarray[index1, index2].Ref_layoutentry.GetParentLocation().X, this.tilesasarray[index1, index2].Ref_layoutentry.GetParentLocation().Y].DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThreadLoc, ref ThreadScale);
          }
        }
      }
    }
  }
}
