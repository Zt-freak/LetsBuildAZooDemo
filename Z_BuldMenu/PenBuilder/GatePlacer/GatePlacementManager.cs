// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer.GatePlacementManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder;
using TinyZoo.Z_ControllerLayouts;
using TinyZoo.Z_HUD.VirtualMouse;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer
{
  internal class GatePlacementManager
  {
    private List<TileRenderer> tilerenderers;
    private TILETYPE gatetile;
    private TileRenderer gaterenderer;
    private List<BaseTileDescriptor> basetiles;
    private bool CannotPlaceHere;
    public LayoutEntry GateDescription;
    public Vector2Int GateLocation;
    private EntranceArrow entrancearrow;
    private bool IsMovePen;
    private Vector2Int MOVE_StartMiddle;
    private Vector2Int MOVE_CurrentOffset;
    private PenMoveCollision penmovercollision;
    private bool IsMoveWHolePen;
    private List<Vector2> OGPositions;
    private PrisonZone prisonzone_ForMove;
    private GatePlacement_Controls gateplacementcontols;
    private Vector2Int LastTileLoc;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public GatePlacementManager(
      PerimeterBuilder perimeterBuilder,
      CellBlockType cellblocktype,
      Player player)
    {
      this.IsMovePen = false;
      OverworldBuildManager.currentbuildstate = this.IsMovePen ? BUILDSTATEFORCONTROLLERHINT.MovePen : BUILDSTATEFORCONTROLLERHINT.PlaceGate;
      this.tilerenderers = new List<TileRenderer>();
      this.basetiles = perimeterBuilder.GetCommitedTiles(cellblocktype);
      for (int index = 0; index < this.basetiles.Count; ++index)
        this.tilerenderers.Add(new TileRenderer(new LayoutEntry(this.basetiles[index].tiletype)
        {
          RotationClockWise = this.basetiles[index].RotationClockWise
        }, this.basetiles[index].Location.X, this.basetiles[index].Location.Y, false));
      this.gatetile = TileData.GetCellBlockTypeToPice(cellblocktype, CellBlockPiece.Gate);
      if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.MovePen)
      {
        this.OGPositions = new List<Vector2>();
        for (int index = 0; index < this.basetiles.Count; ++index)
          this.OGPositions.Add(this.tilerenderers[index].vLocation);
        this.IsMoveWHolePen = true;
        this.MOVE_CurrentOffset = new Vector2Int();
        this.MOVE_StartMiddle = new Vector2Int(this.tilerenderers[this.tilerenderers.Count - 1].TileLocation);
        this.prisonzone_ForMove = !Z_GameFlags.SelectedPrisonZoneisFarm ? player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID) : player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
        this.penmovercollision = new PenMoveCollision(this.tilerenderers, perimeterBuilder, this.prisonzone_ForMove, player);
        for (int index = 0; index < this.prisonzone_ForMove.penItems.items.Count; ++index)
        {
          this.tilerenderers.Add(new TileRenderer(new LayoutEntry(this.prisonzone_ForMove.penItems.items[index].tiletype), this.prisonzone_ForMove.penItems.items[index].Location.X, this.prisonzone_ForMove.penItems.items[index].Location.Y, false));
          this.OGPositions.Add(this.tilerenderers[this.tilerenderers.Count - 1].vLocation);
        }
        for (int index = 0; index < this.prisonzone_ForMove.FloorTiles.Count; ++index)
        {
          this.tilerenderers.Add(new TileRenderer(new LayoutEntry(player.prisonlayout.layout.FloorTileTypes[this.prisonzone_ForMove.FloorTiles[index].X, this.prisonzone_ForMove.FloorTiles[index].Y].tiletype), this.prisonzone_ForMove.FloorTiles[index].X, this.prisonzone_ForMove.FloorTiles[index].Y, false));
          this.OGPositions.Add(this.tilerenderers[this.tilerenderers.Count - 1].vLocation);
        }
      }
      if (this.IsMoveWHolePen)
        return;
      this.gateplacementcontols = new GatePlacement_Controls();
    }

    public void SetMouseLocation(
      Vector2Int TileLoc,
      PerimeterBuilder perimeterBuilder,
      Player player)
    {
      if (this.IsMoveWHolePen)
      {
        if (this.LastTileLoc != null && this.LastTileLoc.CompareMatches(TileLoc))
          return;
        if (this.LastTileLoc == null)
          this.LastTileLoc = new Vector2Int();
        this.LastTileLoc.X = TileLoc.X;
        this.LastTileLoc.Y = TileLoc.Y;
        Vector2 vector2 = TileMath.GetTileToWorldSpace(this.MOVE_StartMiddle) - TileMath.GetTileToWorldSpace(TileLoc);
        for (int index = 0; index < this.tilerenderers.Count; ++index)
          this.tilerenderers[index].vLocation = this.OGPositions[index] - vector2;
        Console.WriteLine("hsfs" + (object) TinyZoo.Game1.Rnd.Next(0, 100000));
        this.penmovercollision.SetNewLocations(TileLoc - this.MOVE_StartMiddle, player);
      }
      else
      {
        this.CannotPlaceHere = true;
        for (int index = 0; index < this.basetiles.Count; ++index)
        {
          if (this.basetiles[index].Location.CompareMatches(TileLoc))
          {
            this.GateDescription = new LayoutEntry(this.gatetile);
            this.GateDescription.RotationClockWise = this.basetiles[index].TranslateRotationForGate();
            this.CannotPlaceHere = this.GateDescription.RotationClockWise < 0;
            if (this.CannotPlaceHere || this.SomethingIsBlockingBehind(TileLoc, perimeterBuilder))
            {
              this.CannotPlaceHere = true;
              this.entrancearrow = new EntranceArrow(0);
              this.entrancearrow.SetAsBlocked();
              this.entrancearrow.vLocation = TileMath.GetTileToWorldSpace(TileLoc);
              this.GateDescription.RotationClockWise = 0;
            }
            else
            {
              Vector2Int Location = new Vector2Int(TileLoc);
              if (this.GateDescription.RotationClockWise == 0)
                ++Location.Y;
              else if (this.GateDescription.RotationClockWise == 1)
                --Location.X;
              else if (this.GateDescription.RotationClockWise == 2)
                --Location.Y;
              else if (this.GateDescription.RotationClockWise == 3)
                ++Location.X;
              this.entrancearrow = new EntranceArrow(this.GateDescription.RotationClockWise);
              if (Z_GameFlags.pathfinder.GetIsBlocked(Location.X, Location.Y))
                this.entrancearrow.SetAsBlocked();
              this.entrancearrow.vLocation = TileMath.GetTileToWorldSpace(Location);
            }
            this.GateLocation = new Vector2Int(TileLoc);
            this.gaterenderer = new TileRenderer(this.GateDescription, this.basetiles[index].Location.X, this.basetiles[index].Location.Y, false);
            if (this.CannotPlaceHere)
              this.gaterenderer.SetAllColours(1f, 0.0f, 0.0f);
          }
        }
      }
    }

    private bool SomethingIsBlockingBehind(Vector2Int TileLoc, PerimeterBuilder perimeterBuilder)
    {
      Vector2Int othervector = new Vector2Int(TileLoc);
      if (this.GateDescription.RotationClockWise == 0)
        --othervector.Y;
      else if (this.GateDescription.RotationClockWise == 1)
        ++othervector.X;
      else if (this.GateDescription.RotationClockWise == 2)
        ++othervector.Y;
      else if (this.GateDescription.RotationClockWise == 3)
        --othervector.X;
      List<Vector2Int> fenceTiles = perimeterBuilder.GetFenceTiles();
      for (int index = 0; index < fenceTiles.Count; ++index)
      {
        if (fenceTiles[index].CompareMatches(othervector))
          return true;
      }
      return false;
    }

    public bool UpdateGatePlacementManager(
      float DeltaTime,
      Player player,
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      AnimalsInPens peopleandbeams)
    {
      if (GameFlags.IsUsingController && !this.IsMoveWHolePen)
      {
        VirtualMouseManager.TempBlockCameraMovement = true;
        if (this.gateplacementcontols.UpdateGatePlacement_Controls(DeltaTime, player, perimeterBuilder))
        {
          this.SetMouseLocation(this.gateplacementcontols.GetSelectedLocation(), perimeterBuilder, player);
          Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(this.gateplacementcontols.GetSelectedLocation());
          OverWorldEnvironmentManager.overworldcam.DoSmoothedRepeatingPan(new Vector3(tileToWorldSpace.X, tileToWorldSpace.Y, Sengine.WorldOriginandScale.Z), 0.3f, true);
        }
      }
      for (int index = 0; index < this.tilerenderers.Count; ++index)
        this.tilerenderers[index].UpdateTileRenderer(DeltaTime);
      if (((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 || player.inputmap.ReleasedThisFrame[0]) && !Z_GameFlags.MouseIsOverAPanel)
      {
        if (this.IsMoveWHolePen)
        {
          if (this.penmovercollision.CanPlace())
          {
            PenMover.ConfirmMovePen(player, this.penmovercollision, this.prisonzone_ForMove, wallsandfloors, peopleandbeams);
            SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
            return true;
          }
        }
        else if (this.entrancearrow != null && this.entrancearrow.entrancespritetype != EntranceSpriteType.Block)
          return true;
      }
      return false;
    }

    public void DrawGatePlacementManager()
    {
      for (int index = 0; index < this.tilerenderers.Count; ++index)
      {
        if (this.penmovercollision != null)
          this.tilerenderers[index].HasDrawn = false;
        this.tilerenderers[index].DrawTileRenderer(AssetContainer.pointspritebatch03, ref GatePlacementManager.ThreadLoc, ref GatePlacementManager.ThreadScale, 0.9f);
      }
      if (!Z_GameFlags.MouseIsOverAPanel && this.gaterenderer != null)
      {
        this.gaterenderer.HasDrawn = false;
        this.gaterenderer.DrawTileRenderer(AssetContainer.pointspritebatch03, ref GatePlacementManager.ThreadLoc, ref GatePlacementManager.ThreadScale, 0.9f);
        if (this.entrancearrow != null)
          this.entrancearrow.DrawEntrance(Vector2.Zero, AssetContainer.pointspritebatch03);
      }
      if (this.penmovercollision != null)
        this.penmovercollision.DrawPenMoveCollision();
      PathFindingManager.entranceblockmanager.DrawAllBlocks();
    }
  }
}
