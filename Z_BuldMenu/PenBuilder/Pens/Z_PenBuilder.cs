// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.Z_PenBuilder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Input;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens
{
  internal class Z_PenBuilder
  {
    public bool DidBuild;
    public GatePlacementManager gateplacementmanager;
    private CellBlockMakerManager cellblockmaker;
    private TILETYPE Buildinthispen;
    private Vector2Int MouseLocation;
    private PerimeterBuilder perimeterBuilder;
    private TILETYPE buildthispen;
    private int _CurrentTileCount;
    private bool IsMovingGate;
    private bool IsFarm;
    public PenMover penmover;

    public Z_PenBuilder(TILETYPE _buildthispen, Player player, bool MovingGate)
    {
      if (_buildthispen == TILETYPE.FieldPicketFenceEnclosure)
        this.IsFarm = true;
      this.IsMovingGate = MovingGate;
      if (!this.IsMovingGate)
        OverworldBuildManager.currentbuildstate = BUILDSTATEFORCONTROLLERHINT.Pen;
      this.buildthispen = _buildthispen;
      this.MouseLocation = new Vector2Int();
      this.Buildinthispen = this.buildthispen;
      this.cellblockmaker = new CellBlockMakerManager(this.buildthispen, player);
      this.penmover = (PenMover) null;
      this.perimeterBuilder = new PerimeterBuilder(this.buildthispen, player);
      if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.MoveGate)
      {
        PrisonZone prisonzone = !Z_GameFlags.SelectedPrisonZoneisFarm ? player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID) : player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
        this.perimeterBuilder.ReconstructFromPrisonZone(prisonzone, player);
        this.perimeterBuilder.SetToGatePlacement();
        this.gateplacementmanager = new GatePlacementManager(this.perimeterBuilder, prisonzone.CellBLOCKTYPE, player);
      }
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.MovePen)
        return;
      PrisonZone prisonzone1 = !Z_GameFlags.SelectedPrisonZoneisFarm ? player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID) : player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
      this.perimeterBuilder.ReconstructFromPrisonZone(prisonzone1, player);
      this.gateplacementmanager = new GatePlacementManager(this.perimeterBuilder, prisonzone1.CellBLOCKTYPE, player);
      this.penmover = new PenMover(this.perimeterBuilder, prisonzone1.Cell_UID);
    }

    public int GetPenUsableSpace() => this.perimeterBuilder != null ? this.perimeterBuilder.GetPenUsableSpace() : -1;

    public int GetMaxWidth() => this.perimeterBuilder.GetMaxWidth();

    public bool IsCurrentlyMovingThisPen(int CELLBLOCKUID) => this.penmover != null && this.penmover.CELLUID == CELLBLOCKUID;

    public bool IsBlockingGate() => this.perimeterBuilder.IsBlockingGate();

    public void CleanUpOnCancel()
    {
      if (this.perimeterBuilder == null)
        return;
      this.perimeterBuilder.CleanUpOnCancel();
    }

    public TILETYPE GetTileTypeBeingBult() => this.Buildinthispen;

    public int GetVolume() => this.perimeterBuilder != null ? this.perimeterBuilder.GetVolume() : this.cellblockmaker.GetVolume();

    public bool GetIsBlocked() => this.cellblockmaker.GetIsBlocked();

    public bool UpdateZ_PenBuilder(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      out int CurrentTileCount,
      AnimalsInPens peopleandbeams,
      out bool SwitchToGatePlacement,
      out bool TryToExit,
      out bool ForceExitFromGateMove,
      bool CanAfford,
      out bool TriedToBuyButCouldNotAfford)
    {
      TriedToBuyButCouldNotAfford = false;
      TryToExit = false;
      SwitchToGatePlacement = false;
      FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = true;
      ForceExitFromGateMove = false;
      if ((double) player.inputmap.PointerLocation.Y < (double) Z_BuildingIconPanel.MinHeight && !MouseStatus.LMouseHeld)
      {
        GameFlags.IsUsingMouse = !MouseStatus.RMouseHeld;
        Z_GameFlags.ForceRightMouseDrag = true;
        FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = false;
      }
      if (this.penmover != null)
      {
        this.penmover.UpdatePenMover(DeltaTime, player);
        CurrentTileCount = 0;
        this.MouseLocation = TileMath.GetScreenSPaceToTileLocation(player.inputmap.PointerLocation);
        this.gateplacementmanager.SetMouseLocation(this.MouseLocation, this.perimeterBuilder, player);
        if (!this.gateplacementmanager.UpdateGatePlacementManager(DeltaTime, player, this.perimeterBuilder, wallsandfloors, peopleandbeams))
          return false;
        TryToExit = true;
        return true;
      }
      if ((double) player.inputmap.PointerLocation.Y < (double) Z_BuildingIconPanel.MinHeight && !MouseStatus.LMouseHeld)
      {
        this.MouseLocation = TileMath.GetScreenSPaceToTileLocation(player.inputmap.PointerLocation);
        if (this.gateplacementmanager == null)
          this.perimeterBuilder.SetMouseLocation(this.MouseLocation, false, player);
        else if (!GameFlags.IsUsingController)
          this.gateplacementmanager.SetMouseLocation(this.MouseLocation, this.perimeterBuilder, player);
      }
      if (this.gateplacementmanager != null)
      {
        CurrentTileCount = this._CurrentTileCount;
        if (!this.gateplacementmanager.UpdateGatePlacementManager(DeltaTime, player, this.perimeterBuilder, wallsandfloors, peopleandbeams))
          return false;
        TryToExit = true;
        if (this.IsMovingGate)
        {
          player.prisonlayout.ConfirmMoveGate(this.perimeterBuilder, wallsandfloors, TileData.GetTileTypeToCellBlockType(this.buildthispen), player, this.gateplacementmanager, Z_GameFlags.SelectedPrisonZoneUID);
        }
        else
        {
          if (this.IsFarm)
          {
            player.farms.AddField(this.perimeterBuilder, wallsandfloors, TileData.GetTileTypeToCellBlockType(this.buildthispen), player, this.gateplacementmanager, true);
          }
          else
          {
            player.prisonlayout.AddNewIrregularCellBlock(this.perimeterBuilder, wallsandfloors, TileData.GetTileTypeToCellBlockType(this.buildthispen), player, this.gateplacementmanager, true);
            peopleandbeams.AddCellBlockOnTheFly(player);
          }
          this.DidBuild = true;
          Z_GameFlags.MustRebuildPrivacyMap = true;
          OverWorldManager.heatmapmanager.DoubleCheckAnimalPrivacySetUp(player);
          QuestScrubber.ScrubOnBuildingPen(player);
        }
        player.OldSaveThisPlayer();
        return true;
      }
      if (!this.perimeterBuilder.UpdatePerimeterBuilder(player, DeltaTime, out CurrentTileCount, CanAfford, out TriedToBuyButCouldNotAfford))
        return false;
      this._CurrentTileCount = CurrentTileCount;
      this.perimeterBuilder.SetToGatePlacement();
      SwitchToGatePlacement = true;
      this.gateplacementmanager = new GatePlacementManager(this.perimeterBuilder, TileData.GetTileTypeToCellBlockType(this.buildthispen), player);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
      return false;
    }

    public void BuyInCurrentState(Player player, WallsAndFloorsManager wallsandfloors) => this.cellblockmaker.BuyInCurrentState(player, wallsandfloors);

    public bool BlockExitWithController() => this.perimeterBuilder.HasWayPoints();

    public void DrawZ_PenBuilder(TileRenderer[,] tilesasarray)
    {
      if (this.penmover != null)
      {
        this.gateplacementmanager.DrawGatePlacementManager();
        this.penmover.DrawPenMover(this.perimeterBuilder);
      }
      else
      {
        this.perimeterBuilder.DrawPerimeterBuilder();
        if (this.gateplacementmanager == null)
          return;
        this.gateplacementmanager.DrawGatePlacementManager();
      }
    }
  }
}
