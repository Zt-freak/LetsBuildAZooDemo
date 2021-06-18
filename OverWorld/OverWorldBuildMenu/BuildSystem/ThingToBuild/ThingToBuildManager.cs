// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.ThingToBuildManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.DuplicatePlacer;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild
{
  internal class ThingToBuildManager
  {
    public TileRenderer tilerenderer;
    private GameObject BlockedTile;
    private Vector2 StartLocation;
    private LayoutEntry layoutentry;
    public static Vector2Int LastLocation;
    private ThingToBuildFootPrint footprint;
    private DuplicatePlaceManager duplicateplacer;
    public static PlaceType placetype;
    private CellBlockMakerManager cellblockmaker;
    private bool HasDragged;
    public bool HideFootPrint;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public ThingToBuildManager(TILETYPE tiletype, Player player)
    {
      ThingToBuildManager.LastLocation = (Vector2Int) null;
      if (TileData.IsThisADuplicatePlacable(tiletype))
      {
        ThingToBuildManager.placetype = PlaceType.PlacingDuplcates;
        this.duplicateplacer = new DuplicatePlaceManager(tiletype);
      }
      else if (TileData.IsThisACellBlock(tiletype))
      {
        ThingToBuildManager.placetype = PlaceType.PlacingCellBlock;
        this.cellblockmaker = new CellBlockMakerManager(tiletype, player);
      }
      else
      {
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
        {
          if (TutorialManager.currenttutorial == TUTORIALTYPE.RevealCashAndBuild)
            BuildFirstThing.CheckTapForSnap(player);
          Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.ReleaseTapArray[0]);
          OverWorldEnvironmentManager.overworldcam.DoPan(new Vector3(worldSpace.X, worldSpace.Y, Sengine.WorldOriginandScale.Z), 0.1f);
          this.StartLocation = worldSpace;
          ThingToBuildManager.LastLocation = TileMath.GetWorldSpaceToTile(this.StartLocation);
        }
        else
        {
          ThingToBuildManager.LastLocation = TileMath.GetScreenCenter();
          this.StartLocation = new Vector2(Sengine.WorldOriginandScale.X, Sengine.WorldOriginandScale.Y);
        }
        TileInfo tileInfo = TileData.GetTileInfo(tiletype);
        this.layoutentry = new LayoutEntry(tiletype);
        this.layoutentry.RotationClockWise = 0;
        this.tilerenderer = new TileRenderer(this.layoutentry, ThingToBuildManager.LastLocation.X, ThingToBuildManager.LastLocation.Y, false, true);
        this.tilerenderer.vLocation = this.StartLocation;
        ThingToBuildManager.LastLocation = TileMath.GetWorldSpaceToTile(this.StartLocation);
        this.footprint = new ThingToBuildFootPrint(this.tilerenderer.XWidth, this.tilerenderer.YHeight, this.tilerenderer, tileInfo);
      }
    }

    public bool UpdateThingToBuildManager(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      BuildUI buildconfirmUI)
    {
      if (ThingToBuildManager.placetype == PlaceType.PlacingDuplcates)
      {
        this.duplicateplacer.UpdateDuplicatePlaceManager(player, DeltaTime);
        return false;
      }
      if (ThingToBuildManager.placetype == PlaceType.PlacingCellBlock)
      {
        this.cellblockmaker.UpdateCellBlockMakerManager(player, DeltaTime, wallsandfloors);
        return false;
      }
      if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !this.HasDragged)
      {
        this.HideFootPrint = false;
        if (!FeatureFlags.LockToBuildToCurrentLocation && !buildconfirmUI.CheckBlocked(player.player.touchinput.ReleaseTapArray[0]))
        {
          if (TutorialManager.currenttutorial == TUTORIALTYPE.RevealCashAndBuild)
            BuildFirstThing.CheckTapForSnap(player);
          Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.ReleaseTapArray[0]);
          OverWorldEnvironmentManager.overworldcam.DoPan(new Vector3(worldSpace.X, worldSpace.Y, Sengine.WorldOriginandScale.Z), 0.1f);
        }
      }
      if (this.HasDragged && (double) player.player.touchinput.MultiTouchTouchLocations[0].X < 0.0)
        this.HasDragged = false;
      bool flag = false;
      this.StartLocation = new Vector2(Sengine.WorldOriginandScale.X, Sengine.WorldOriginandScale.Y);
      if (!TileMath.GetWorldSpaceToTile(this.StartLocation).CompareMatches(ThingToBuildManager.LastLocation))
      {
        if ((double) player.player.touchinput.TotalDragLength > 4.0)
        {
          this.HideFootPrint = false;
          this.HasDragged = true;
        }
        ThingToBuildManager.LastLocation = TileMath.GetWorldSpaceToTile(this.StartLocation);
        if (ThingToBuildManager.LastLocation.X - this.tilerenderer.XOrigin < 0)
          ThingToBuildManager.LastLocation.X = this.tilerenderer.XOrigin;
        if (ThingToBuildManager.LastLocation.X + (this.tilerenderer.XWidth - this.tilerenderer.XOrigin) > TileMath.GetOverWorldMapSize_XDefault())
        {
          ThingToBuildManager.LastLocation.X = TileMath.GetOverWorldMapSize_XDefault();
          ThingToBuildManager.LastLocation.X -= this.tilerenderer.XWidth - this.tilerenderer.XOrigin;
        }
        if (ThingToBuildManager.LastLocation.Y - this.tilerenderer.YOrigin < 0)
          ThingToBuildManager.LastLocation.Y = this.tilerenderer.YOrigin;
        if (ThingToBuildManager.LastLocation.Y + (this.tilerenderer.YHeight - this.tilerenderer.YOrigin) > TileMath.GetOverWorldMapSize_XDefault())
        {
          ThingToBuildManager.LastLocation.Y = TileMath.GetOverWorldMapSize_XDefault();
          ThingToBuildManager.LastLocation.Y -= this.tilerenderer.YHeight - this.tilerenderer.YOrigin;
        }
        this.tilerenderer.SetLocation(ThingToBuildManager.LastLocation.X, ThingToBuildManager.LastLocation.Y);
        flag = true;
      }
      this.tilerenderer.UpdateTileRenderer(DeltaTime);
      return flag;
    }

    public void SetBlocks(BlockInfo block) => this.footprint.SetBlocks(block, this.tilerenderer);

    public void DrawThingToBuildManager(TileRenderer[,] tilesasarray)
    {
      if (ThingToBuildManager.placetype == PlaceType.PlacingDuplcates)
        this.duplicateplacer.DrawDuplicatePlaceManager();
      else if (ThingToBuildManager.placetype == PlaceType.PlacingCellBlock)
      {
        this.cellblockmaker.DrawCellBlockMakerManager(tilesasarray);
      }
      else
      {
        if (this.HideFootPrint)
          return;
        this.tilerenderer.vLocation = this.StartLocation;
        this.tilerenderer.fAlpha = 0.4f;
        this.tilerenderer.DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThingToBuildManager.ThreadLoc, ref ThingToBuildManager.ThreadScale);
        this.tilerenderer.SetLocation(ThingToBuildManager.LastLocation.X, ThingToBuildManager.LastLocation.Y);
        this.tilerenderer.fAlpha = 1f;
        this.tilerenderer.DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThingToBuildManager.ThreadLoc, ref ThingToBuildManager.ThreadScale);
        this.footprint.DrawThingToBuildFootPrint(this.tilerenderer.vLocation, AssetContainer.pointspritebatch01);
        this.tilerenderer.fAlpha = 0.4f;
        this.tilerenderer.DrawTileRenderer(AssetContainer.pointspritebatch01, ref ThingToBuildManager.ThreadLoc, ref ThingToBuildManager.ThreadScale);
      }
    }
  }
}
