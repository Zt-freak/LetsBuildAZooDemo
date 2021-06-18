// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.PrisonZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.GamePlay.Results;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;
using TinyZoo.Z_Trailer;

namespace TinyZoo.PlayerDir.Layout
{
  internal class PrisonZone
  {
    public bool IsFarm;
    public CropStatus cropsatus;
    public float LastCalculatedQualityForSpace = 1f;
    public PrisonerContainer prisonercontainer;
    public BeamLayout beams;
    public Vector2Int TopLeftFloorSpace;
    public Vector2Int WidthAndHeight;
    public int Cell_UID;
    private InfrastructureSave infrastructure;
    public CellBlockType CellBLOCKTYPE;
    public List<Vector2Int> FenceTiles;
    public List<Vector2Int> FloorTiles;
    private Vector2 WorldLocation_TopLeft;
    private Vector2 WorldLocation_BottomRight;
    public PenItems penItems;
    private Vector2Int Centre;
    private Vector2 CentreForIcon;
    private int Irregular_XLeft;
    private int Irregular_Right;
    private int Irregular_Top;
    private int Irregular_Bottom;
    public Vector2Int TopLeft;
    public Vector2Int BottomRight;
    public bool IsIerregular;
    public List<Vector2Int> Gates;
    public Vector2Int TEMPSpaceBehindGate;
    public Poop poop;
    public int TEMP_LakeSize;
    public int Cleanliness_LastCalculatedDIRTYNESS;
    public int LargeGroup_DiseaseBonus;
    public int LastCalculatedCorpseValue;
    public int LastCalculatedDirtyLake;
    public int LastCalculatedPoopValue;
    public bool TEMP_LakeHasCleanWater;
    public int DaysOfDirtyLake;
    public int Temp_UnblockedFloorSpace;
    public int Temp_ShelteredFloorSpace;
    public int Temp_Popularity;
    public int Temp_UnenrichedAnimals;
    public float TempTotalWater;
    public float TempAnimalEnrichmentUnfulfillment;
    public bool TempHasEnoughWater;
    public float Temp_ShortThisMuchEnrichment;
    public float WelfareAndCleanliness;
    public float PoopWelfareContribution;
    public float CorpseWelfareContribution;
    public int TotalVolumeOfPoo;
    public int TotalPoops;
    public int CorpseCount;
    public List<ViewingLocation> viewinglocations;
    public List<ViewingLocation> SecondRowViewingLocations;
    public int StartingViewingCycleLoc;
    public int ViewingCycleDir;
    public float GateIntegrity = 100f;
    public bool GateIsBroken;
    public Vector2Int IncomingSignLocation;
    private static float[] CellBlockPowerCost;

    public PrisonZone(int _UID, CellBlockType _cellblocktype)
    {
      Enclosure_Farm_Map.MustRecreateMap = true;
      this.Gates = new List<Vector2Int>();
      this.infrastructure = new InfrastructureSave();
      this.prisonercontainer = new PrisonerContainer();
      this.IsIerregular = true;
      this.Cell_UID = _UID;
      this.CellBLOCKTYPE = _cellblocktype;
      this.FloorTiles = new List<Vector2Int>();
      this.FenceTiles = new List<Vector2Int>();
      this.penItems = new PenItems();
      this.poop = new Poop();
    }

    public void YV_SetGate(int XL, int YL) => this.Gates.Add(new Vector2Int());

    public PrisonZone(
      int LeftFloowSpace,
      int TopFloorSpace,
      int Width,
      int Height,
      int _UID,
      CellBlockType _cellblocktype,
      Vector2Int _GateLocation)
    {
      Enclosure_Farm_Map.MustRecreateMap = true;
      this.LargeGroup_DiseaseBonus = 0;
      this.Cleanliness_LastCalculatedDIRTYNESS = 0;
      this.poop = new Poop();
      this.penItems = new PenItems();
      this.TEMPSpaceBehindGate = (Vector2Int) null;
      this.Gates = new List<Vector2Int>();
      this.Gates.Add(_GateLocation);
      this.CellBLOCKTYPE = _cellblocktype;
      this.infrastructure = new InfrastructureSave();
      this.prisonercontainer = new PrisonerContainer();
      this.beams = new BeamLayout();
      this.Cell_UID = _UID;
      this.TopLeftFloorSpace = new Vector2Int(LeftFloowSpace, TopFloorSpace);
      this.WidthAndHeight = new Vector2Int(Width, Height);
    }

    public Vector2Int GetNavigableGateLoction()
    {
      if (this.TEMPSpaceBehindGate.X < this.Gates[0].X)
        return new Vector2Int(this.Gates[0].X + 1, this.Gates[0].Y);
      if (this.TEMPSpaceBehindGate.X > this.Gates[0].X)
        return new Vector2Int(this.Gates[0].X - 1, this.Gates[0].Y);
      return this.TEMPSpaceBehindGate.Y < this.Gates[0].Y ? new Vector2Int(this.Gates[0].X, this.Gates[0].Y + 1) : new Vector2Int(this.Gates[0].X, this.Gates[0].Y - 1);
    }

    public void CheckFarmSign(Player player) => OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.IncomingSignLocation.X, this.IncomingSignLocation.Y].CreateFarmSign(player, this.cropsatus.cropgrowinghere, this.Cell_UID);

    public void CheckOrderSign(Player player)
    {
      int TotalEntries = 0;
      for (int index = 0; index < player.animalsonorder.animalsonorder.Count; ++index)
      {
        if (player.animalsonorder.animalsonorder[index].PrisonUID == this.Cell_UID)
          ++TotalEntries;
      }
      if (this.IncomingSignLocation == null)
        this.ForceSetIncomingSignLocation();
      if (OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.IncomingSignLocation.X, this.IncomingSignLocation.Y] == null)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.IncomingSignLocation.X, this.IncomingSignLocation.Y].CreateOrderSign(player, TotalEntries, this.Cell_UID);
    }

    private void ForceSetIncomingSignLocation()
    {
      for (int index = 0; index < this.FenceTiles.Count; ++index)
      {
        if (this.FenceTiles[index].Y == this.Gates[0].Y)
        {
          if (this.FenceTiles[index].X == this.Gates[0].X - 1 || this.FenceTiles[index].X == this.Gates[0].X + 1)
            this.IncomingSignLocation = new Vector2Int(this.FenceTiles[index]);
        }
        else if (this.FenceTiles[index].X == this.Gates[0].X && (this.FenceTiles[index].Y == this.Gates[0].Y - 1 || this.FenceTiles[index].Y == this.Gates[0].Y + 1))
          this.IncomingSignLocation = new Vector2Int(this.FenceTiles[index]);
      }
      if (this.IncomingSignLocation != null)
        return;
      this.IncomingSignLocation = new Vector2Int(this.FenceTiles[0]);
    }

    public void FixGate(Player player)
    {
      this.GateIntegrity = 100f;
      if (!this.GateIsBroken)
        return;
      this.GateIsBroken = false;
      player.prisonlayout.layout.BaseTileTypes[this.Gates[0].X, this.Gates[0].Y].tiletype = TileData.GetCellBlockTypeToPice(this.CellBLOCKTYPE, CellBlockPiece.Gate);
      OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: this.Gates[0]);
      Z_GameFlags.pathfinder.BlockTile(this.Gates[0].X, this.Gates[0].Y, true);
    }

    public bool BreakGate(Player player)
    {
      if (this.GateIsBroken)
        return false;
      this.GateIsBroken = true;
      player.prisonlayout.layout.BaseTileTypes[this.Gates[0].X, this.Gates[0].Y].tiletype = TileData.GetGateTileTypeToBrokenGateTileType(player.prisonlayout.layout.BaseTileTypes[this.Gates[0].X, this.Gates[0].Y].tiletype);
      OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: this.Gates[0]);
      Z_GameFlags.pathfinder.UnblockTile(this.Gates[0].X, this.Gates[0].Y, true);
      return this.prisonercontainer.prisoners.Count > 0;
    }

    public bool HasWaterTrough() => this.penItems.HasWaterTrough();

    public bool HasEnrichment() => this.penItems.HasEnrichment();

    public bool CanBuildDecoOnThisTile(int Xloc, int YLoc)
    {
      if (this.TEMPSpaceBehindGate != null && this.TEMPSpaceBehindGate.X == Xloc && this.TEMPSpaceBehindGate.Y == YLoc)
        return true;
      if (this.TEMPSpaceBehindGate == null)
        throw new Exception("Track back from here, and call -GetSpaceBehindGate- ");
      return this.penItems.GetItemBlockingThisTile(Xloc, YLoc, true) != null;
    }

    public PrisonZone(
      PerimeterBuilder perimeterBuilder,
      int _UID,
      CellBlockType _cellblocktype,
      GatePlacementManager gateplacer,
      bool AddToCollisionCheckList = false,
      bool _IsFarm = false)
    {
      this.IsFarm = _IsFarm;
      if (this.IsFarm)
        this.cropsatus = new CropStatus(perimeterBuilder);
      this.penItems = new PenItems();
      this.TEMPSpaceBehindGate = (Vector2Int) null;
      this.Gates = new List<Vector2Int>();
      this.Gates.Add(gateplacer.GateLocation);
      this.TEMPSpaceBehindGate = new Vector2Int(this.Gates[0]);
      Vector2Int vector2Int = new Vector2Int(this.TEMPSpaceBehindGate);
      switch (gateplacer.GateDescription.RotationClockWise)
      {
        case 0:
          --this.TEMPSpaceBehindGate.Y;
          ++vector2Int.Y;
          break;
        case 1:
          ++this.TEMPSpaceBehindGate.X;
          --vector2Int.X;
          break;
        case 2:
          --vector2Int.Y;
          ++this.TEMPSpaceBehindGate.Y;
          break;
        default:
          ++vector2Int.X;
          --this.TEMPSpaceBehindGate.X;
          break;
      }
      PathFindingManager.entranceblockmanager.AddBlock(vector2Int.X, vector2Int.Y, gateplacer.GateDescription.RotationClockWise, TileData.GetCellBlockTypeToPice(_cellblocktype, CellBlockPiece.Gate));
      this.IsIerregular = true;
      this.FloorTiles = new List<Vector2Int>();
      this.FenceTiles = new List<Vector2Int>();
      bool flag = false;
      for (int index = 0; index < perimeterBuilder.CommitedBuildTiles.Count; ++index)
      {
        this.FenceTiles.Add(new Vector2Int(perimeterBuilder.CommitedBuildTiles[index].TileLocation));
        if (!flag && perimeterBuilder.CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_BottomRight)
        {
          flag = true;
          this.IncomingSignLocation = new Vector2Int(perimeterBuilder.CommitedBuildTiles[index].TileLocation);
        }
      }
      if (!flag)
        throw new Exception("NOOOOOOT FOUND");
      this.FloorTiles = perimeterBuilder.GeturrentAnimalFloorSpace();
      this.CellBLOCKTYPE = _cellblocktype;
      this.infrastructure = new InfrastructureSave();
      this.prisonercontainer = new PrisonerContainer();
      this.beams = new BeamLayout();
      this.Cell_UID = _UID;
      if (AddToCollisionCheckList)
        Z_GameFlags.SetPenFloorTilesOnCollsionsChanged(this.FloorTiles);
      this.poop = new Poop();
      if (this.IsFarm)
        return;
      Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
    }

    public void GetHungryAnaimals(Player player) => this.prisonercontainer.GetHungryAnaimals(player, this.Gates[0], this.Cell_UID);

    public int GetTotalCrossbreed(AnimalType AnimalA, AnimalType AnimalB) => this.prisonercontainer.GetTotalCrossbreed(AnimalA, AnimalB);

    public void AddViewingLocation(
      int XLoc,
      int Yloc,
      float Privacy,
      DirectionPressed faceThisWayToLookAtPen,
      int ViewingDistance,
      bool RemoveFromSecondary = false)
    {
      if (ViewingDistance == 0)
      {
        this.viewinglocations.Add(new ViewingLocation(XLoc, Yloc, Privacy, faceThisWayToLookAtPen));
        if (!RemoveFromSecondary)
          return;
        for (int index = this.SecondRowViewingLocations.Count - 1; index > -1; --index)
        {
          if (this.SecondRowViewingLocations[index].Location.CompareMatches(this.viewinglocations[this.viewinglocations.Count - 1].Location))
            this.SecondRowViewingLocations.RemoveAt(index);
        }
      }
      else
        this.SecondRowViewingLocations.Add(new ViewingLocation(XLoc, Yloc, Privacy, faceThisWayToLookAtPen));
    }

    public bool TryAndGetPathToViewingLocation(
      PathNavigator pathnavigator,
      out DirectionPressed LookThisWayToInteract)
    {
      int num = 0;
      LookThisWayToInteract = DirectionPressed.None;
      for (; num < this.viewinglocations.Count; ++num)
      {
        this.StartingViewingCycleLoc += this.ViewingCycleDir;
        if (this.StartingViewingCycleLoc < 0)
          this.StartingViewingCycleLoc = this.viewinglocations.Count - 1;
        else if (this.StartingViewingCycleLoc >= this.viewinglocations.Count - 1)
          this.StartingViewingCycleLoc = 0;
        if (pathnavigator.TryToGoHereSquare(this.viewinglocations[this.StartingViewingCycleLoc].Location, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
        {
          LookThisWayToInteract = this.viewinglocations[this.StartingViewingCycleLoc].faceThisWayToLookAtPen;
          return true;
        }
      }
      return false;
    }

    public void SetViewingRandomStart()
    {
      if (this.viewinglocations.Count <= 0)
        return;
      this.StartingViewingCycleLoc = TinyZoo.Game1.Rnd.Next(0, this.viewinglocations.Count);
      if (TinyZoo.Game1.Rnd.Next(0, 2) == 0)
        this.ViewingCycleDir = 1;
      else
        this.ViewingCycleDir = -1;
    }

    private void RemoveEntryArrow(out int Rotatiooons, out Vector2Int PointerLocation)
    {
      PointerLocation = new Vector2Int(this.Gates[0]);
      Rotatiooons = 0;
      if (this.TEMPSpaceBehindGate.X < this.Gates[0].X)
      {
        Rotatiooons = 3;
        ++PointerLocation.X;
      }
      else if (this.TEMPSpaceBehindGate.X > this.Gates[0].X)
      {
        --PointerLocation.X;
        Rotatiooons = 1;
      }
      else if (this.TEMPSpaceBehindGate.Y < this.Gates[0].Y)
      {
        Rotatiooons = 0;
        ++PointerLocation.Y;
      }
      else
      {
        if (this.TEMPSpaceBehindGate.Y <= this.Gates[0].Y)
          throw new Exception("Wah Happen?");
        Rotatiooons = 2;
        --PointerLocation.Y;
      }
      PathFindingManager.entranceblockmanager.RemoveBlock(PointerLocation.X, PointerLocation.Y, Rotatiooons, TileData.GetCellBlockTypeToPice(this.CellBLOCKTYPE, CellBlockPiece.Gate));
    }

    public void MoveGate(GatePlacementManager gateplacer, Player player)
    {
      if (this.TEMPSpaceBehindGate == null)
        throw new Exception("NOW THIS IS BAD");
      this.RemoveEntryArrow(out int _, out Vector2Int _);
      this.TEMPSpaceBehindGate = (Vector2Int) null;
      this.Gates = new List<Vector2Int>();
      this.Gates.Add(gateplacer.GateLocation);
      this.GetSpaceBehindGate(player);
      this.TEMPSpaceBehindGate = new Vector2Int(this.Gates[0]);
      Vector2Int vector2Int = new Vector2Int(this.TEMPSpaceBehindGate);
      switch (gateplacer.GateDescription.RotationClockWise)
      {
        case 0:
          --this.TEMPSpaceBehindGate.Y;
          ++vector2Int.Y;
          break;
        case 1:
          ++this.TEMPSpaceBehindGate.X;
          --vector2Int.X;
          break;
        case 2:
          --vector2Int.Y;
          ++this.TEMPSpaceBehindGate.Y;
          break;
        default:
          ++vector2Int.X;
          --this.TEMPSpaceBehindGate.X;
          break;
      }
      PathFindingManager.entranceblockmanager.AddBlock(vector2Int.X, vector2Int.Y, gateplacer.GateDescription.RotationClockWise, TileData.GetCellBlockTypeToPice(this.CellBLOCKTYPE, CellBlockPiece.Gate));
    }

    public void TrySetTopeLeftAndWidthHeight()
    {
      if (this.TopLeft != null)
        return;
      this.TopLeft = new Vector2Int(-1, -1);
      this.BottomRight = new Vector2Int(-1, -1);
      for (int index = 0; index < this.FenceTiles.Count; ++index)
      {
        if (this.FenceTiles[index].X < this.TopLeft.X || this.TopLeft.X == -1)
          this.TopLeft.X = this.FenceTiles[index].X;
        if (this.FenceTiles[index].Y < this.TopLeft.Y || this.TopLeft.Y == -1)
          this.TopLeft.Y = this.FenceTiles[index].Y;
        if (this.FenceTiles[index].X > this.BottomRight.X || this.BottomRight.X == -1)
          this.BottomRight.X = this.FenceTiles[index].X;
        if (this.FenceTiles[index].Y > this.BottomRight.Y || this.BottomRight.Y == -1)
          this.BottomRight.Y = this.FenceTiles[index].Y;
      }
    }

    public bool ThisIsInThisEnclosure(int XLoc, int YLoc)
    {
      for (int index = 0; index < this.FloorTiles.Count; ++index)
      {
        if (this.FloorTiles[index].X == XLoc && this.FloorTiles[index].Y == YLoc)
          return true;
      }
      return false;
    }

    public void DestroyCellBlock() => this.RemoveEntryArrow(out int _, out Vector2Int _);

    public void MoveWholeZone(Vector2Int Offset)
    {
      int Rotatiooons;
      Vector2Int PointerLocation;
      this.RemoveEntryArrow(out Rotatiooons, out PointerLocation);
      for (int index = 0; index < this.FloorTiles.Count; ++index)
        PathFindingManager.entranceblockmanager.RemoveBlockByLocation(this.FloorTiles[index]);
      for (int index = 0; index < this.FenceTiles.Count; ++index)
        PathFindingManager.entranceblockmanager.RemoveBlockByLocation(this.FenceTiles[index]);
      PointerLocation.X += Offset.X;
      PointerLocation.Y += Offset.Y;
      PathFindingManager.entranceblockmanager.AddBlock(PointerLocation.X, PointerLocation.Y, Rotatiooons, TileData.GetCellBlockTypeToPice(this.CellBLOCKTYPE, CellBlockPiece.Gate));
      Z_GameFlags.TryAndRemoveOnOrderSign(this.Cell_UID);
      if (this.TopLeft != null)
      {
        this.TopLeft.X += Offset.X;
        this.TopLeft.Y += Offset.Y;
        this.BottomRight.X += Offset.X;
        this.BottomRight.Y += Offset.Y;
      }
      this.IncomingSignLocation.X += Offset.X;
      this.IncomingSignLocation.Y += Offset.Y;
      this.TEMPSpaceBehindGate = (Vector2Int) null;
      this.WorldLocation_TopLeft.X += (float) Offset.X;
      this.WorldLocation_TopLeft.Y += (float) Offset.Y;
      this.WorldLocation_BottomRight.X += (float) Offset.X;
      this.WorldLocation_BottomRight.Y += (float) Offset.Y;
      Z_GameFlags.JUSTMovedTheseEnclosures.Add(this.Cell_UID);
      if (this.TopLeftFloorSpace != null)
      {
        this.TopLeftFloorSpace.X += Offset.X;
        this.TopLeftFloorSpace.Y += Offset.Y;
      }
      for (int index = 0; index < this.FenceTiles.Count; ++index)
      {
        this.FenceTiles[index].X += Offset.X;
        this.FenceTiles[index].Y += Offset.Y;
      }
      for (int index = 0; index < this.FloorTiles.Count; ++index)
      {
        this.FloorTiles[index].X += Offset.X;
        this.FloorTiles[index].Y += Offset.Y;
      }
      for (int index = 0; index < this.Gates.Count; ++index)
      {
        this.Gates[index].X += Offset.X;
        this.Gates[index].Y += Offset.Y;
      }
      for (int index = 0; index < this.penItems.items.Count; ++index)
      {
        this.penItems.items[index].Location.X += Offset.X;
        this.penItems.items[index].Location.Y += Offset.Y;
      }
      this.Centre = (Vector2Int) null;
      this.GetPlaceToDisplayCellInfoButton(out bool _);
      GameFlags.CollisionChanged = true;
      Z_GameFlags.SetPenFloorTilesOnCollsionsChanged(this.FloorTiles);
      Z_GameFlags.MustRebuildPrivacyMap = true;
    }

    public void SetUpAllStock(Player player) => this.prisonercontainer.SetUpAllStock(player);

    public float GetSpaceRequiredByAniamlsInThisPen(ref float TerritorySize)
    {
      float num = 0.0f;
      float MustHaveAtleastThisMuchSpace = 0.0f;
      for (int index = 0; index < this.prisonercontainer.prisoners.Count; ++index)
      {
        num += AnimalData.GetRequiredFloorSpacePerAnimal(this.prisonercontainer.prisoners[index].intakeperson.animaltype, ref MustHaveAtleastThisMuchSpace);
        if ((double) TerritorySize < (double) MustHaveAtleastThisMuchSpace)
          TerritorySize = MustHaveAtleastThisMuchSpace;
      }
      return num;
    }

    public int GetBeamsInUse() => this.beams.beamentries.Count;

    public void SellStructure(Vector2Int position, LayoutEntry _layoutentry) => this.infrastructure.SellStructure(position, _layoutentry);

    public void GetLimits(out Vector2 TopLeft, out Vector2 BottomRight)
    {
      if (this.IsIerregular)
      {
        if ((double) this.WorldLocation_BottomRight.X == 0.0 && (double) this.WorldLocation_BottomRight.Y == 0.0)
          this.GetPlaceToDisplayCellInfoButton(out bool _);
        TopLeft = this.WorldLocation_TopLeft;
        BottomRight = this.WorldLocation_BottomRight;
      }
      else
      {
        TopLeft = Vector2.Zero;
        BottomRight = Vector2.Zero;
      }
    }

    public bool HasThisAlienSomewhere(AnimalType enemytype)
    {
      for (int index = 0; index < this.prisonercontainer.prisoners.Count; ++index)
      {
        if (this.prisonercontainer.prisoners[index].intakeperson.animaltype == enemytype)
          return true;
      }
      return false;
    }

    public Vector2 GetPlaceToDisplayCellInfoButton(out bool HasFloorTiles)
    {
      HasFloorTiles = false;
      if (this.IsIerregular)
      {
        if (this.Centre == null && this.FloorTiles.Count > 0)
        {
          HasFloorTiles = true;
          this.Irregular_XLeft = this.FloorTiles[0].X;
          this.Irregular_Right = this.FloorTiles[0].X;
          this.Irregular_Top = this.FenceTiles[0].Y;
          this.Irregular_Bottom = this.FenceTiles[0].Y;
          for (int index = 0; index < this.FenceTiles.Count; ++index)
          {
            if (this.FenceTiles[index].X < this.Irregular_XLeft)
              this.Irregular_XLeft = this.FenceTiles[index].X;
            if (this.FenceTiles[index].X > this.Irregular_Right)
              this.Irregular_Right = this.FenceTiles[index].X;
            if (this.FenceTiles[index].Y < this.Irregular_Top)
              this.Irregular_Top = this.FenceTiles[index].Y;
            if (this.FenceTiles[index].Y > this.Irregular_Bottom)
              this.Irregular_Bottom = this.FenceTiles[index].Y;
          }
          this.WidthAndHeight = new Vector2Int(this.Irregular_Right - this.Irregular_XLeft, this.Irregular_Bottom - this.Irregular_Top);
          bool flag = false;
          for (int index = 0; index < this.FloorTiles.Count; ++index)
          {
            if (this.FloorTiles[index].X == this.Irregular_XLeft + (this.Irregular_Right - this.Irregular_XLeft) / 2 && !flag)
            {
              this.Centre = new Vector2Int(this.FloorTiles[index]);
              if (this.FloorTiles[index].Y >= this.Irregular_Top + (this.Irregular_Bottom - this.Irregular_Top) / 2)
                flag = true;
            }
          }
          this.WorldLocation_TopLeft = TileMath.GetTileToWorldSpace(new Vector2Int(this.Irregular_XLeft, this.Irregular_Top));
          this.WorldLocation_BottomRight = TileMath.GetTileToWorldSpace(new Vector2Int(this.Irregular_Right, this.Irregular_Bottom));
          if (!flag && this.Centre == null)
            this.Centre = new Vector2Int(this.FloorTiles[0]);
          if (!flag)
          {
            this.Centre = new Vector2Int(this.FloorTiles[0]);
            this.CentreForIcon = TileMath.GetTileToWorldSpace(this.Centre);
          }
          else
          {
            this.Centre = new Vector2Int(this.Irregular_XLeft + (this.Irregular_Right - this.Irregular_XLeft) / 2, this.Irregular_Top + (this.Irregular_Bottom - this.Irregular_Top) / 2);
            this.CentreForIcon = TileMath.GetTileToWorldSpace(this.Centre);
            if ((double) this.Irregular_XLeft + ((double) this.Irregular_Right - (double) this.Irregular_XLeft) * 0.5 != (double) this.Centre.X)
              this.CentreForIcon.X += TileMath.TileSize * 0.5f;
            if ((double) this.Irregular_Top + ((double) this.Irregular_Bottom - (double) this.Irregular_Top) * 0.5 != (double) this.Centre.Y)
              this.CentreForIcon.Y += TileMath.TileSize * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          }
        }
      }
      else if (this.Centre == null)
      {
        this.Centre = new Vector2Int(this.TopLeftFloorSpace + new Vector2Int(this.WidthAndHeight.X / 2, this.WidthAndHeight.Y / 2));
        this.CentreForIcon = TileMath.GetTileToWorldSpace(this.Centre);
        if (this.WidthAndHeight.X % 2 == 0)
          this.CentreForIcon.X -= TileMath.TileSize * 0.5f;
        if (this.WidthAndHeight.Y % 2 == 0)
          this.CentreForIcon.Y -= TileMath.TileSize * 0.5f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      }
      return this.CentreForIcon;
    }

    public void BuildStructure(Vector2Int position, TILETYPE ThingToBuld) => this.infrastructure.BuildStructure(position, ThingToBuld);

    public bool IsThisLocationInThisDungeon(Vector2Int locaaation)
    {
      if (this.IsIerregular)
      {
        for (int index = 0; index < this.FenceTiles.Count; ++index)
        {
          if (this.FenceTiles[index].CompareMatches(locaaation))
            return true;
        }
        for (int index = 0; index < this.FloorTiles.Count; ++index)
        {
          if (this.FloorTiles[index].CompareMatches(locaaation))
            return true;
        }
      }
      else if (locaaation.X >= this.TopLeftFloorSpace.X - 1 && locaaation.X <= this.TopLeftFloorSpace.X + this.WidthAndHeight.X && (locaaation.Y >= this.TopLeftFloorSpace.Y - 1 && locaaation.Y <= this.TopLeftFloorSpace.Y + this.WidthAndHeight.Y))
        return true;
      return false;
    }

    public bool RemoveThisPrisoner(PrisonerInfo prisonerinfo) => this.prisonercontainer.RemoveThisPrisoner(prisonerinfo, this.CellBLOCKTYPE);

    public PrisonerInfo GetThisPrisoner(IntakePerson intakeperson) => this.prisonercontainer.GetThisPrisoner(intakeperson);

    public bool PaintAnAnimal(PrisonerInfo prisonerInfo)
    {
      WalkingPerson animalByUid = CustomerManager.GetAnimalByUID(prisonerInfo.intakeperson.UID);
      if (animalByUid == null || prisonerInfo.IsDead)
        return false;
      prisonerInfo.IsPainted = true;
      animalByUid.animalrenderer.enemyrenderere.ReconstructAsNew(prisonerInfo.GetAnimalPainted(), prisonerInfo.intakeperson.CLIndex, prisonerInfo.intakeperson.HeadType, prisonerInfo.intakeperson.HeadVariant);
      return true;
    }

    public void AddNewStructureThing(TileRenderer tilerenderer) => this.infrastructure.BuildTileFromTileRenderer(tilerenderer);

    public void SetMapLimits()
    {
      if (this.IsIerregular)
        return;
      TileMath.SetPlaySpaceLeft((float) ((this.TopLeftFloorSpace.X - 1) * 16 + 8));
      TileMath.SetPlaySpaceTop((float) ((this.TopLeftFloorSpace.Y - 1) * 16 + 8));
      TileMath.SetPlaySpaceRight((float) ((this.TopLeftFloorSpace.X + this.WidthAndHeight.X - 1) * 16 + 8));
      TileMath.SetPlaySpaceBottom((float) ((this.TopLeftFloorSpace.Y + this.WidthAndHeight.Y - 1) * 16 + 8));
    }

    public Vector2Int TryToGetRandomVector2IntInCellBlock() => this.FloorTiles[TinyZoo.Game1.Rnd.Next(0, this.FloorTiles.Count)];

    public Vector2 GetRandomLocationInCellBlock()
    {
      if (!this.IsIerregular)
        return Vector2.Zero;
      return this.FloorTiles != null && this.FloorTiles.Count > 0 ? TileMath.GetTileToWorldSpace(this.FloorTiles[TinyZoo.Game1.Rnd.Next(0, this.FloorTiles.Count)]) : new Vector2(1000f, 10000f);
    }

    public int GetFloorSpaceVolume() => this.IsIerregular ? this.FloorTiles.Count : this.WidthAndHeight.X * this.WidthAndHeight.Y;

    public Vector2Int GetTopLeft() => this.IsIerregular ? new Vector2Int(this.Irregular_XLeft, this.Irregular_Top) : new Vector2Int(this.TopLeftFloorSpace.X - 1, this.TopLeftFloorSpace.Y - 1);

    public Vector2Int GetTopRight() => this.IsIerregular ? new Vector2Int(this.Irregular_Right, this.Irregular_Top) : new Vector2Int(this.TopLeftFloorSpace.X + this.WidthAndHeight.X, this.TopLeftFloorSpace.Y - 1);

    public Vector2Int GetBottomLeft() => this.IsIerregular ? new Vector2Int(this.Irregular_XLeft, this.Irregular_Bottom) : new Vector2Int(this.TopLeftFloorSpace.X - 1, this.TopLeftFloorSpace.Y + this.WidthAndHeight.Y);

    public Vector2Int GetBottomRight() => this.IsIerregular ? new Vector2Int(this.Irregular_Right, this.Irregular_Bottom) : new Vector2Int(this.TopLeftFloorSpace.X + this.WidthAndHeight.X, this.TopLeftFloorSpace.Y + this.WidthAndHeight.Y);

    public void GetDailyEanings(
      ref int Profit,
      bool IsDayEnd_CalculateParoleEtc,
      ref int AllMoneyIncludingWrngCell)
    {
      this.prisonercontainer.GetDailyEanings(ref Profit, IsDayEnd_CalculateParoleEtc, ref AllMoneyIncludingWrngCell);
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus)
    {
      if (PrisonZone.CellBlockPowerCost == null)
      {
        PrisonZone.CellBlockPowerCost = new float[11];
        for (int index = 0; index < PrisonZone.CellBlockPowerCost.Length; ++index)
          PrisonZone.CellBlockPowerCost[index] = 5f;
        PrisonZone.CellBlockPowerCost[0] = 0.25f;
        PrisonZone.CellBlockPowerCost[1] = 0.4f;
        PrisonZone.CellBlockPowerCost[2] = 0.5f;
        PrisonZone.CellBlockPowerCost[2] = 0.6f;
        PrisonZone.CellBlockPowerCost[3] = 0.7f;
        PrisonZone.CellBlockPowerCost[4] = 0.8f;
        PrisonZone.CellBlockPowerCost[5] = 0.9f;
        PrisonZone.CellBlockPowerCost[6] = 1f;
        PrisonZone.CellBlockPowerCost[7] = 1.05f;
        PrisonZone.CellBlockPowerCost[8] = 1.1f;
        PrisonZone.CellBlockPowerCost[9] = 1.12f;
      }
      this.infrastructure.SetConsumption(consumptionstatus);
      int num1 = 0;
      if (!this.IsIerregular)
        num1 = this.WidthAndHeight.X * this.WidthAndHeight.Y;
      int num2 = (int) Math.Ceiling((double) num1 * (double) PrisonZone.CellBlockPowerCost[(int) this.CellBLOCKTYPE]);
      consumptionstatus.ConsumptionValues[0] += (float) num2;
      this.prisonercontainer.SetConsumption(consumptionstatus);
    }

    public void SetBeamLayout(BeamLayout beamlayout) => this.beams = beamlayout;

    public void SetPrisonersOnMapEnd(
      EnemyManager enemyrenderer,
      Player player,
      GameResult resulttt)
    {
      this.prisonercontainer.SetPrisonersOnMapEnd(enemyrenderer, this.CellBLOCKTYPE, player);
    }

    public EnemyManager GetEnemyRenderer(BoxZoneManager boxzonemanager) => new EnemyManager(this.prisonercontainer, boxzonemanager, this);

    public Vector2Int GetSpaceBehindGate(Player player = null)
    {
      if (this.TEMPSpaceBehindGate == null)
      {
        if (player == null)
          throw new Exception("NOT HIT - You are caling this in the update - and somehow - when adding a gate - you didnt all this function to creat ethe space behind the gate");
        this.TEMPSpaceBehindGate = new Vector2Int(this.Gates[0]);
        switch (player.prisonlayout.layout.BaseTileTypes[this.Gates[0].X, this.Gates[0].Y].RotationClockWise)
        {
          case 0:
            --this.TEMPSpaceBehindGate.Y;
            break;
          case 1:
            ++this.TEMPSpaceBehindGate.X;
            break;
          case 2:
            ++this.TEMPSpaceBehindGate.Y;
            break;
          default:
            --this.TEMPSpaceBehindGate.X;
            break;
        }
      }
      return this.TEMPSpaceBehindGate;
    }

    public Vector2Int GetSpaceInfrontOfGate()
    {
      Vector2Int vector2Int = new Vector2Int(this.GetSpaceBehindGate());
      if (vector2Int.X != this.Gates[0].X)
      {
        if (this.Gates[0].Y < vector2Int.Y)
        {
          vector2Int.Y += 2;
          return vector2Int;
        }
        vector2Int.Y -= 2;
        return vector2Int;
      }
      if (this.Gates[0].X < vector2Int.X)
      {
        vector2Int.X += 2;
        return vector2Int;
      }
      vector2Int.X -= 2;
      return vector2Int;
    }

    public Vector2Int GetGateLocation() => this.Gates[0];

    public BeamManager GetBeamLayout(BoxZoneManager boxzonemanager) => new BeamManager(this.beams, boxzonemanager);

    public void CalculateWaterCapacity(Player player)
    {
      this.TEMP_LakeSize = 0;
      this.TEMP_LakeHasCleanWater = false;
      for (int index = 0; index < this.FloorTiles.Count; ++index)
      {
        if (player.prisonlayout.layout.FloorTileTypes[this.FloorTiles[index].X, this.FloorTiles[index].Y].tiletype == TILETYPE.Volume_Water)
        {
          ++this.TEMP_LakeSize;
          if (!this.TEMP_LakeHasCleanWater && OverWorldManager.heatmapmanager.GetHasWaterAccess(this.FloorTiles[index].X, this.FloorTiles[index].Y))
            this.TEMP_LakeHasCleanWater = true;
        }
      }
    }

    public void StartDay(Player player)
    {
      this.penItems.StartDay(player);
      this.CalculateWaterCapacity(player);
      if (this.TEMP_LakeSize > 0 && !this.TEMP_LakeHasCleanWater)
        ++this.DaysOfDirtyLake;
      else
        this.DaysOfDirtyLake = 0;
    }

    public void AddAnimalFromBreedingRoom(PrisonerInfo animal)
    {
      Player.financialrecords.AnimalAddedToZoo();
      this.prisonercontainer.prisoners.Add(animal);
      animal.ResetOnMoveCell(this.CellBLOCKTYPE);
      this.prisonercontainer.FoodForAnimals.AddAnimal(animal.intakeperson.animaltype);
      this.prisonercontainer.ThisWasTehCellBlockThatChanged = true;
      GameFlags.CellBlockContentsChanged = true;
    }

    public void SavePrisonZone(Writer writer)
    {
      writer.WriteInt("z", (int) this.CellBLOCKTYPE);
      writer.WriteInt("z", this.Cell_UID);
      writer.WriteBool("z", this.IsIerregular);
      if (this.IsIerregular)
      {
        writer.WriteInt("z", this.FenceTiles.Count);
        for (int index = 0; index < this.FenceTiles.Count; ++index)
          this.FenceTiles[index].SaveVector2Int(writer);
        writer.WriteInt("z", this.FloorTiles.Count);
        for (int index = 0; index < this.FloorTiles.Count; ++index)
          this.FloorTiles[index].SaveVector2Int(writer);
      }
      else
      {
        this.TopLeftFloorSpace.SaveVector2Int(writer);
        this.WidthAndHeight.SaveVector2Int(writer);
      }
      if (this.IsFarm)
      {
        this.cropsatus.SaveCropStatus(writer);
      }
      else
      {
        this.prisonercontainer.SavePrisonContainer(writer);
        this.infrastructure.SaveInfrastructureSave(writer);
      }
      writer.WriteInt("z", this.Gates.Count);
      for (int index = 0; index < this.Gates.Count; ++index)
        this.Gates[index].SaveVector2Int(writer);
      if (!this.IsFarm)
      {
        this.penItems.SavePenItems(writer);
        this.poop.SavePoop(writer);
        writer.WriteInt("z", this.DaysOfDirtyLake);
        writer.WriteInt("z", this.Cleanliness_LastCalculatedDIRTYNESS);
        writer.WriteFloat("z", this.GateIntegrity);
      }
      if (this.IncomingSignLocation == null)
        this.ForceSetIncomingSignLocation();
      this.IncomingSignLocation.SaveVector2Int(writer);
    }

    public PrisonZone(Reader reader, int VersionNumberForLoad, bool _IsFarm = false)
    {
      Enclosure_Farm_Map.MustRecreateMap = true;
      this.IsFarm = _IsFarm;
      int _out = 0;
      int num1 = (int) reader.ReadInt("z", ref _out);
      this.CellBLOCKTYPE = (CellBlockType) _out;
      int num2 = (int) reader.ReadInt("z", ref this.Cell_UID);
      int num3 = (int) reader.ReadBool("z", ref this.IsIerregular);
      if (this.IsIerregular)
      {
        int num4 = (int) reader.ReadInt("z", ref _out);
        this.FenceTiles = new List<Vector2Int>();
        for (int index = 0; index < _out; ++index)
          this.FenceTiles.Add(new Vector2Int(reader));
        int num5 = (int) reader.ReadInt("z", ref _out);
        this.FloorTiles = new List<Vector2Int>();
        for (int index = 0; index < _out; ++index)
          this.FloorTiles.Add(new Vector2Int(reader));
      }
      else
      {
        this.TopLeftFloorSpace = new Vector2Int(reader);
        this.WidthAndHeight = new Vector2Int(reader);
      }
      if (this.IsFarm)
      {
        this.cropsatus = new CropStatus(reader);
      }
      else
      {
        this.prisonercontainer = new PrisonerContainer(reader, this.CellBLOCKTYPE, VersionNumberForLoad);
        if (TrailerDemoFlags.HasTrailerFlag)
        {
          if (TrailerDemoFlags.PenRevealIndexes.Contains(this.Cell_UID))
          {
            Z_Trailermanager.prisonersbycellblock.Add(new PrisonersByCellBlock(this.prisonercontainer.prisoners, this.Cell_UID));
            this.prisonercontainer.prisoners = new List<PrisonerInfo>();
          }
          else
          {
            AnimalType _enemytype = (AnimalType) TinyZoo.Game1.Rnd.Next(0, 56);
            AnimalType _HeadType = AnimalType.None;
            if (TinyZoo.Game1.Rnd.Next(0, 10) == 0)
              _HeadType = (AnimalType) TinyZoo.Game1.Rnd.Next(0, 56);
            int num4 = TinyZoo.Game1.Rnd.Next(5, 12);
            for (int index = 0; index < num4; ++index)
            {
              this.prisonercontainer.prisoners.Add(new PrisonerInfo(new IntakePerson(_enemytype, Variant: TinyZoo.Game1.Rnd.Next(0, 10), _HeadType: _HeadType, _HeadVariant: TinyZoo.Game1.Rnd.Next(0, 10)), false, Vector2.Zero, CellBlockType.Arctic));
              this.prisonercontainer.prisoners[this.prisonercontainer.prisoners.Count - 1].ResetOnMoveCell(this.CellBLOCKTYPE);
            }
          }
        }
        this.infrastructure = new InfrastructureSave(reader);
      }
      this.TEMPSpaceBehindGate = (Vector2Int) null;
      this.Gates = new List<Vector2Int>();
      int num6 = (int) reader.ReadInt("z", ref _out);
      for (int index = 0; index < _out; ++index)
        this.Gates.Add(new Vector2Int(reader));
      if (!this.IsFarm)
      {
        this.penItems = new PenItems(reader);
        this.poop = new Poop(reader);
        int num4 = (int) reader.ReadInt("z", ref this.Cleanliness_LastCalculatedDIRTYNESS);
        int num5 = (int) reader.ReadInt("z", ref this.DaysOfDirtyLake);
        int num7 = (int) reader.ReadFloat("z", ref this.GateIntegrity);
      }
      this.IncomingSignLocation = new Vector2Int(reader);
    }

    public void SetBehindGateAndArrowsOnLoad(LayoutData layout)
    {
      this.TEMPSpaceBehindGate = new Vector2Int(this.Gates[0]);
      Vector2Int vector2Int = new Vector2Int(this.TEMPSpaceBehindGate);
      int rotationClockWise = layout.BaseTileTypes[this.Gates[0].X, this.Gates[0].Y].RotationClockWise;
      switch (rotationClockWise)
      {
        case 0:
          --this.TEMPSpaceBehindGate.Y;
          ++vector2Int.Y;
          break;
        case 1:
          ++this.TEMPSpaceBehindGate.X;
          --vector2Int.X;
          break;
        case 2:
          --vector2Int.Y;
          ++this.TEMPSpaceBehindGate.Y;
          break;
        default:
          ++vector2Int.X;
          --this.TEMPSpaceBehindGate.X;
          break;
      }
      PathFindingManager.entranceblockmanager.AddBlock(vector2Int.X, vector2Int.Y, rotationClockWise, TileData.GetCellBlockTypeToPice(this.CellBLOCKTYPE, CellBlockPiece.Gate));
    }
  }
}
