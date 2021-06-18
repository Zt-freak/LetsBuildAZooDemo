// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.PrisonLayout
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.ArcadeMode;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GamePlay.Results;
using TinyZoo.Maps;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.PlayerDir.Layout.facilities;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_CreateGame;
using TinyZoo.Z_Data;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld.SpawnAnimations;
using TinyZoo.Z_Quests;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir
{
  internal class PrisonLayout
  {
    public LayoutData layout;
    public CellblockMananger cellblockcontainer;
    public TrashAndStuff trashandstuff;
    public Z_Facilities Facilities;
    public List<PrisonerInfo> AnimalsNotInPens;
    private static int CELLNUMBER = 1;

    public PrisonLayout(ConsumptionStatus consumptionstatus, Player player)
    {
      this.AnimalsNotInPens = new List<PrisonerInfo>();
      this.Facilities = new Z_Facilities();
      Z_SetUpGame.CreateDefaultMap();
      this.cellblockcontainer = new CellblockMananger();
      this.trashandstuff = new TrashAndStuff();
      this.layout = new LayoutData();
      if (DebugFlags.LoadYvonnesZoo)
      {
        this.layout = YvonneMap.GetMap();
        this.cellblockcontainer = TheseEnclosures.GetCellblockMananger();
      }
      int num1 = TileMath.GetOverWorldMapSize_XDefault() / 2;
      int num2 = TileMath.GetOverWorldMapSize_YSize() / 2;
      if (!DebugFlags.LoadYvonnesZoo)
        ZMapSetUp.SetUpMapFences(0, this, player, consumptionstatus);
      else
        ZMapSetUp.CreatePathSet();
      ZMapSetUp.UnlockAllExistingLand();
      if (Z_DebugFlags.IsBetaVersion)
      {
        player.storerooms.InstantAddStock(AnimalFoodType.Straw, 50);
        player.storerooms.InstantAddStock(AnimalFoodType.VegetablePellets, 30);
        player.storerooms.InstantAddStock(AnimalFoodType.BlendedPellets, 30);
        player.storerooms.InstantAddStock(AnimalFoodType.MeatPellet, 30);
        for (int index = 0; index < 88; ++index)
          player.storerooms.InstantAddStock((AnimalFoodType) index, 60);
      }
      else
      {
        player.storerooms.InstantAddStock(AnimalFoodType.Straw, 50);
        player.storerooms.InstantAddStock(AnimalFoodType.VegetablePellets, 30);
        player.storerooms.InstantAddStock(AnimalFoodType.BlendedPellets, 30);
        player.storerooms.InstantAddStock(AnimalFoodType.MeatPellet, 30);
        player.storerooms.InstantAddStock(AnimalFoodType.Greens, 20);
      }
    }

    public void SetUpAllStock(Player player) => this.cellblockcontainer.SetUpAllStock(player);

    public int GetTotalCrossbreed(AnimalType AnimalA, AnimalType AnimalB) => this.cellblockcontainer.GetTotalCrossbreed(AnimalA, AnimalB);

    public void GetHungryAnaimals(Player player)
    {
      player.livestats.hungryanimals = new List<HungryAnimal>();
      this.cellblockcontainer.GetHungryAnaimals(player);
    }

    public void AddAnimalNotInPen(PrisonerInfo animal) => this.AnimalsNotInPens.Add(animal);

    public bool RemoveThisNotInPenAnimal(int UID)
    {
      for (int index = this.AnimalsNotInPens.Count - 1; index > -1; --index)
      {
        if (this.AnimalsNotInPens[index].intakeperson.UID == UID)
        {
          this.AnimalsNotInPens.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public PrisonerInfo GetThisNotInPenAnimal(int UID)
    {
      for (int index = 0; index < this.AnimalsNotInPens.Count; ++index)
      {
        if (this.AnimalsNotInPens[index].intakeperson.UID == UID)
          return this.AnimalsNotInPens[index];
      }
      return (PrisonerInfo) null;
    }

    public bool RemoveTheseAnimalsOnTrade(
      int Total,
      AnimalType animal,
      int VariantIndex,
      int BoysUsed)
    {
      return this.cellblockcontainer.RemoveTheseAnimalsOnTrade(Total, animal, VariantIndex, BoysUsed);
    }

    public bool RemoveSpecificAnimals_TradeOrSell(List<PrisonerInfo> animals, Player player) => this.cellblockcontainer.RemoveSpecificAnimals_TradeOrSell(animals, player);

    public bool HasABuildng_Shop() => this.cellblockcontainer.HasABuildng_Shop();

    public bool HasAnArchitectureBuilding() => this.cellblockcontainer.HasAnArchitectureBuilding();

    public PrisonerInfo GetThisAnimal(int UID, out int CellBoockUID) => this.cellblockcontainer.GetThisAnimal(UID, out CellBoockUID);

    public void AddNewAnimal(
      AnimalType animaltype,
      int CellID,
      int Variant,
      bool IsAGirl,
      out int ChildUID,
      int MotherUID,
      int FatherUID)
    {
      ChildUID = -1;
      this.cellblockcontainer.AddNewAnimal(animaltype, CellID, Variant, IsAGirl, out ChildUID, MotherUID, FatherUID);
      if (ChildUID == -1)
        throw new Exception("EVEN MORE CRAPS");
    }

    public bool HasTheseAnimals(
      QuestPack Ref_questpack,
      out bool StillHasBreedingPair,
      out int BoysUsed)
    {
      StillHasBreedingPair = false;
      BoysUsed = 0;
      if (Ref_questpack.trades_ListOnlyOne.Count > 1)
        throw new Exception("NOPE");
      int GirlsIncludingVariants;
      int BoysIncludingVariants;
      int ThisVariantGirls;
      int ThisVariantBoys;
      int totalOfThisAlien = this.cellblockcontainer.GetTotalOfThisALien(Ref_questpack.trades_ListOnlyOne[0].animal, Ref_questpack.trades_ListOnlyOne[0].VariantIndex, out GirlsIncludingVariants, out BoysIncludingVariants, out ThisVariantGirls, out ThisVariantBoys);
      int num1 = Ref_questpack.trades_ListOnlyOne[0].Total.SmartGetValue(false, 10);
      bool flag1 = GirlsIncludingVariants - ThisVariantGirls > 0;
      bool flag2 = BoysIncludingVariants - ThisVariantBoys > 0;
      int num2 = num1;
      if (totalOfThisAlien < num2)
        return false;
      int num3 = num1;
      int num4 = BoysIncludingVariants;
      int num5 = GirlsIncludingVariants;
      for (int index = 0; index < num1; ++index)
      {
        if (num4 == 1 && !flag2 && num5 > 0)
        {
          --num5;
          ++num3;
          if (!flag1 && num5 == 0)
          {
            StillHasBreedingPair = false;
            return true;
          }
        }
        else if (num5 == 1 && !flag1 && num4 > 0)
        {
          --num4;
          ++num3;
          ++BoysUsed;
          if (!flag2 && num4 == 0)
          {
            StillHasBreedingPair = false;
            return true;
          }
        }
        else if (num4 > num5)
        {
          --num3;
          --num4;
          ++BoysUsed;
        }
        else if (num5 > num4)
        {
          --num3;
          --num5;
        }
        else if (num4 > 0)
        {
          --num3;
          --num4;
          ++BoysUsed;
        }
        else
        {
          if (num5 <= 0)
            throw new Exception("WAAAH! not enough....But said enough already");
          --num3;
          --num5;
        }
      }
      if (flag1 & flag2)
        StillHasBreedingPair = true;
      else if (flag1 && num4 > 0)
        StillHasBreedingPair = true;
      else if (flag2 && num5 > 0)
        StillHasBreedingPair = true;
      else if (num5 > 0 && num4 > 0)
        StillHasBreedingPair = true;
      return true;
    }

    public void ResetForLanguage() => this.cellblockcontainer.ResetForLanguage();

    public bool HasThisAlienSomewhere(AnimalType enemytype) => this.cellblockcontainer.HasThisAlienSomewhere(enemytype);

    public bool ThesePeopleAreInThePrison(IntakeInfo intakeUseForQuit) => this.cellblockcontainer.ThesePeopleAreInThePrison(intakeUseForQuit);

    public int AddNewCellBlock(
      int XWallLoc,
      int YWallLocation,
      int WidthIncludingWalls,
      int HeightIncludingWalls,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      Player player,
      bool IsGraveYard = false)
    {
      int cellnumber = PrisonLayout.CELLNUMBER;
      this.cellblockcontainer.AddNewCellBlock(XWallLoc, YWallLocation, WidthIncludingWalls, HeightIncludingWalls, cellnumber, IsGraveYard, cellblocktype);
      if (!IsGraveYard)
        ++PrisonLayout.CELLNUMBER;
      this.layout.AddNewCellBlock(new Vector2Int(XWallLoc + 1, YWallLocation + 1), new Vector2Int(WidthIncludingWalls - 2, HeightIncludingWalls - 2), true, wallsandfloors, cellblocktype);
      PrisonWalls.CheckPrisonWalls(this.layout.BaseTileTypes);
      this.SetConsumption(player.livestats.consumptionstatus, player);
      return cellnumber;
    }

    public void ConfirmMoveGate(
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      Player player,
      GatePlacementManager gateplacer,
      int CellBlockUID)
    {
      this.layout.ModifyGateLocation(perimeterBuilder, wallsandfloors, cellblocktype, gateplacer);
      this.cellblockcontainer.MoveGate(CellBlockUID, gateplacer, player);
    }

    public int AddNewIrregularCellBlock(
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      Player player,
      GatePlacementManager gateplacer,
      bool AddToCollisionCheckList = false)
    {
      int cellnumber = PrisonLayout.CELLNUMBER;
      CascadeSpawner.SetUpFloorForCascade(perimeterBuilder.GeturrentAnimalFloorSpace(), player);
      Enclosure_Farm_Map.MustRecreateMap = true;
      Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
      this.layout.AddNewIrregularCellBlockCellBlock(perimeterBuilder, wallsandfloors, cellblocktype, gateplacer);
      this.cellblockcontainer.AddNewIrregularCellBlock(perimeterBuilder, cellnumber, cellblocktype, gateplacer, AddToCollisionCheckList);
      CascadeSpawner.DoCascadeSpawnForPen(this.cellblockcontainer.prisonzones[this.cellblockcontainer.prisonzones.Count - 1], player);
      ++PrisonLayout.CELLNUMBER;
      this.SetConsumption(player.livestats.consumptionstatus, player);
      return cellnumber;
    }

    public bool IsThisPrisonerInAHoldingCell(IntakePerson person) => this.cellblockcontainer.IsThisPrisonerInAHoldingCell(person);

    public bool IsNotEarningMoney(Player player) => this.cellblockcontainer.GetDailyEanings(true, out int _, out int _, player) == 0;

    public void AddNewlyDead(IntakePerson intakeperson, ref Vector2Int LocationOfGrave) => this.cellblockcontainer.AddNewlyDead(intakeperson, ref LocationOfGrave);

    public void DoParole(IntakePerson person) => this.cellblockcontainer.DoParole(person);

    public int GetTotalOfThisAnimal(AnimalType animal, int Variant = -1) => this.cellblockcontainer.GetTotalOfThisAnimal(animal, Variant);

    public int GetNumberOfGraveYards() => this.cellblockcontainer.GetNumberOfGraveYards();

    public int HasThisMuchSpaceInGraveYard() => this.cellblockcontainer.HasThisMuchSpaceInGraveYard();

    public void CheckAndRemoveDuplicatesFromHoldingCells(EnemyManager enemyrenderer) => this.cellblockcontainer.CheckAndRemoveDuplicatesFromHoldingCells(enemyrenderer);

    public void CheckAndRemoveDeadFromHoldingCells(EnemyManager enemyrenderer) => this.cellblockcontainer.CheckAndRemoveDeadFromHoldingCells(enemyrenderer);

    public void BuildStructure(
      TILETYPE tiletype,
      Vector2Int Location,
      ConsumptionStatus consumptionstatus,
      Player player,
      int RotationClockwise,
      bool CheckPen = true)
    {
      Z_GameFlags.pathfinder.AddNode(tiletype, Location.X, Location.Y, RotationClockwise);
      this.layout.AddTile(tiletype, Location, 0);
      if (CheckPen)
        this.cellblockcontainer.BuildStructure(Location, tiletype);
      this.SetConsumption(consumptionstatus, player);
      if (tiletype != TILETYPE.WaterPumpStation)
        return;
      this.Facilities.AddPumpStation(Location, player);
    }

    public void SellStructure(
      Vector2Int position,
      LayoutEntry _layoutentry,
      ConsumptionStatus consumptionstatus,
      Player player,
      bool _IsMove = false,
      bool IsPenItem = false)
    {
      if (_layoutentry.tiletype == TILETYPE.WaterPumpStation)
        this.Facilities.RemoveWaterPump(position, player);
      Z_GameFlags.pathfinder.RemoveNode(_layoutentry.tiletype, position, _layoutentry.RotationClockWise);
      this.layout.SellStructure(position, _layoutentry, IsPenItem: IsPenItem);
      this.cellblockcontainer.SellStructure(position, _layoutentry, player);
      player.shopstatus.SellBuilding(position, _layoutentry.tiletype, player, _IsMove);
      this.SetConsumption(consumptionstatus, player);
    }

    public int GetTotalResearch() => this.cellblockcontainer.GetTotalResearch();

    public bool HasAnyHoldingCells() => this.cellblockcontainer.HasAnyHoldingCells();

    public void CheckGraveStonesOnLoad() => this.cellblockcontainer.CheckGraveStonesOnLoad(this.layout);

    public int HasSpaceInHoldingCells() => this.cellblockcontainer.HasSpaceInHoldingCells();

    public int HasHoldingCellTransferSlots(Vector2Int HoldingCellLocation) => this.cellblockcontainer.HasHoldingCellTransferSlots(HoldingCellLocation);

    public DeadPerson GetThisDeadPerson(Vector2Int SelectedLocation) => this.cellblockcontainer.GetThisDeadPerson(SelectedLocation);

    public void RemoveThisAnimalFromCellBlock()
    {
    }

    public bool IsThisAlreadyHere(
      TILETYPE tiletype,
      Vector2Int TileLocation,
      bool IsFloorLayer = false,
      bool IsUnderFloor = false)
    {
      if (TileLocation.X >= this.layout.FloorTileTypes.GetLength(0) || TileLocation.Y >= this.layout.FloorTileTypes.GetLength(1) || this.layout.FloorTileTypes[TileLocation.X, TileLocation.Y] == null)
        return false;
      return IsUnderFloor ? this.layout.FloorTileTypes[TileLocation.X, TileLocation.Y].UnderFloorTiletype == tiletype : this.layout.FloorTileTypes[TileLocation.X, TileLocation.Y].tiletype == tiletype;
    }

    public bool IsThisAlreadyHere(TileRenderer tilerenderer, bool IsFloorLayer = false, bool IsUnderFloor = false)
    {
      if (!IsFloorLayer)
        throw new Exception("YOU WERE TOO LAZY TO CHECK FOR ANYTHING LARGER THAN ONE TILE");
      if (tilerenderer.TileLocation.X >= this.layout.FloorTileTypes.GetLength(0) || tilerenderer.TileLocation.Y >= this.layout.FloorTileTypes.GetLength(1) || this.layout.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y] == null)
        return false;
      return IsUnderFloor ? this.layout.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].UnderFloorTiletype == tilerenderer.tiletypeonconstruct : this.layout.FloorTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y].tiletype == tilerenderer.tiletypeonconstruct;
    }

    public void RevertFloor(Vector2Int Location, BuildHistory buildhistory) => this.layout.RevertFloor(Location, buildhistory);

    public void RevertFloor(Vector2Int Location, bool IsUnderFloor = false) => this.layout.RevertFloor(Location, IsUnderFloor);

    public void BuildTileFromTileRenderer(
      TileRenderer tilerenderer,
      ConsumptionStatus consumptionstatus,
      Player player,
      bool IsFloorLayer = false,
      bool IsUnderFloor = false,
      bool IsPenItem = false)
    {
      if (IsFloorLayer)
      {
        this.layout.AddTileFromTileRenderer(tilerenderer, IsFloorLayer, IsUnderFloor);
      }
      else
      {
        if (tilerenderer.TileLocation.X >= this.layout.BaseTileTypes.GetLength(0) || tilerenderer.TileLocation.Y >= this.layout.BaseTileTypes.GetLength(1) || (tilerenderer.TileLocation.X <= -1 || tilerenderer.TileLocation.Y <= -1))
          return;
        this.SellStructure(tilerenderer.TileLocation, this.layout.BaseTileTypes[tilerenderer.TileLocation.X, tilerenderer.TileLocation.Y], consumptionstatus, player);
        this.layout.AddTileFromTileRenderer(tilerenderer, IsPenItem: IsPenItem);
        QuestScrubber.ScrubOnPlacingBuilding(player);
        this.cellblockcontainer.BuildTileFromTileRenderer(tilerenderer);
        if (tilerenderer.tiletypeonconstruct == TILETYPE.WaterPumpStation)
          this.Facilities.AddPumpStation(tilerenderer.TileLocation, player);
        this.SetConsumption(consumptionstatus, player);
      }
    }

    private void SetConsumption(ConsumptionStatus consumptionstatus, Player player) => this.cellblockcontainer.SetConsumption(consumptionstatus, player);

    public void AddTile(
      TILETYPE AddThis,
      Vector2Int Location,
      int RotationClockWise,
      Player player,
      bool IsFloorLayer = false)
    {
      if (TrailerDemoFlags.AutoReveal)
        return;
      this.layout.AddTile(AddThis, Location, RotationClockWise, IsFloorLayer);
      if (AddThis == TILETYPE.WaterPumpStation)
        throw new Exception("IS THIS BEING ADDED TO THE WATER THING");
    }

    public LayoutEntry GetThisDungeonTile(int X, int Y) => GameFlags.IsArcadeMode ? ArcadeData.layout.GetThisDungeonTile(X, Y) : this.layout.GetThisDungeonTile(X, Y);

    public PrisonZone GetThisCellBlock(int PrisonUID) => this.cellblockcontainer.GetThisCellBlock(PrisonUID);

    public int FindCellBlockWIthThisAnimal(AnimalType animal)
    {
      int blockWithThisAnimal = this.cellblockcontainer.FindCellBlockWIthThisAnimal(animal);
      return blockWithThisAnimal == -1 ? this.cellblockcontainer.prisonzones[TinyZoo.Game1.Rnd.Next(0, this.cellblockcontainer.prisonzones.Count)].Cell_UID : blockWithThisAnimal;
    }

    public void SellGraveYard(GraveYardBlockInfo grave)
    {
      Vector2Int position = new Vector2Int();
      for (int index1 = grave.TopLeft.X - 1; index1 < grave.TopLeft.X + grave.Size.X + 1; ++index1)
      {
        for (int index2 = grave.TopLeft.Y - 1; index2 < grave.TopLeft.Y + grave.Size.Y + 1; ++index2)
        {
          position.X = index1;
          position.Y = index2;
          if (index2 == grave.TopLeft.Y + grave.Size.Y && index1 == grave.TopLeft.X + grave.Size.X)
            this.layout.SellStructure(position, (LayoutEntry) null, true);
          else
            this.layout.SellStructure(position, (LayoutEntry) null, true, true);
        }
      }
      this.cellblockcontainer.SellGraveYard(grave);
    }

    public void DestroyCellBlock(
      int CellUID,
      Player player,
      WallsAndFloorsManager wallansfloorsmanager)
    {
      Vector2Int position = new Vector2Int();
      PrisonZone thisCellBlock = this.GetThisCellBlock(CellUID);
      thisCellBlock.DestroyCellBlock();
      Z_NotificationManager.ScrubEmptyPensForNewAnimals = true;
      if (thisCellBlock.IsIerregular)
      {
        for (int index = 0; index < thisCellBlock.FloorTiles.Count; ++index)
        {
          this.layout.SellStructure(thisCellBlock.FloorTiles[index], (LayoutEntry) null, true, true, true);
          this.layout.FloorTileTypes[thisCellBlock.FloorTiles[index].X, thisCellBlock.FloorTiles[index].Y].tiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
          wallansfloorsmanager.VallidateAgainstLayout(this.layout, true, thisCellBlock.FloorTiles[index]);
        }
        for (int index = 0; index < thisCellBlock.FenceTiles.Count; ++index)
          this.layout.SellStructure(thisCellBlock.FenceTiles[index], (LayoutEntry) null, true, true, true);
      }
      else
      {
        for (int index1 = thisCellBlock.TopLeftFloorSpace.X - 1; index1 < thisCellBlock.TopLeftFloorSpace.X + thisCellBlock.WidthAndHeight.X + 1; ++index1)
        {
          for (int index2 = thisCellBlock.TopLeftFloorSpace.Y - 1; index2 < thisCellBlock.TopLeftFloorSpace.Y + thisCellBlock.WidthAndHeight.Y + 1; ++index2)
          {
            position.X = index1;
            position.Y = index2;
            if (index2 == thisCellBlock.TopLeftFloorSpace.Y + thisCellBlock.WidthAndHeight.Y && index1 == thisCellBlock.TopLeftFloorSpace.X + thisCellBlock.WidthAndHeight.X)
              this.layout.SellStructure(position, (LayoutEntry) null, true);
            else
              this.layout.SellStructure(position, (LayoutEntry) null, true, true);
          }
        }
      }
      this.cellblockcontainer.Sellprison(CellUID, player);
      this.SetConsumption(player.livestats.consumptionstatus, player);
    }

    public PrisonZone GetThisCellBlock(Vector2Int TileLocation, bool WillThrow = true) => this.cellblockcontainer.GetThisCellBlock(TileLocation, WillThrow);

    public HoldingCellInfo GetThisHoldingCell(Vector2Int TileLocation) => this.cellblockcontainer.GetThisHoldingCell(TileLocation);

    public GraveYardBlockInfo GetThisGraveyardBlockInfo(Vector2Int TileLocation) => this.cellblockcontainer.GetThisGraveyardBlockInfo(TileLocation);

    public int GetDailyEanings(
      bool IsDayEnd_CalculateParoleEtc,
      out int EarningsWithoutMod,
      out int AllMoneyIncludingWrongCell,
      Player player)
    {
      int dailyEanings = this.cellblockcontainer.GetDailyEanings(IsDayEnd_CalculateParoleEtc, out EarningsWithoutMod, out AllMoneyIncludingWrongCell, player);
      if (player.Stats.ADisabled(true, player))
      {
        dailyEanings += (int) Math.Round((double) dailyEanings * 0.0500000007450581);
        EarningsWithoutMod += (int) Math.Round((double) EarningsWithoutMod * 0.0500000007450581);
        AllMoneyIncludingWrongCell += (int) Math.Round((double) AllMoneyIncludingWrongCell * 0.0500000007450581);
      }
      return dailyEanings;
    }

    public void SetDataFromMap(
      int PrisonUID,
      BeamManager beammanager,
      EnemyManager enemyrenderer,
      Player player,
      GameResult resulttt)
    {
      if (resulttt == GameResult.NoBombs)
        return;
      PrisonZone thisCellBlock = this.GetThisCellBlock(PrisonUID);
      thisCellBlock.SetBeamLayout(beammanager.GetBeamDataForSave());
      thisCellBlock.SetPrisonersOnMapEnd(enemyrenderer, player, resulttt);
    }

    public int GetPrivacyForAnimalPenTile(
      int InPenFloorX,
      int InPenFloorY,
      int[,] PrivacyMapData,
      PrisonZone prisonzone)
    {
      if (TileData.IsThisAShelter(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype))
        return 100;
      return this.layout.BaseTileTypes[InPenFloorX, InPenFloorY] != null && this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.None && TileData.GetTileInfo(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype).PrivacyBlock > 0 ? -TileData.GetTileInfo(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype).PrivacyBlock : Math.Min(Math.Min(Math.Min(this.GetPrivacyFromInsidePen(InPenFloorX, InPenFloorY, 1, 0, PrivacyMapData, prisonzone, DirectionPressed.Left), this.GetPrivacyFromInsidePen(InPenFloorX, InPenFloorY, -1, 0, PrivacyMapData, prisonzone, DirectionPressed.Right)), this.GetPrivacyFromInsidePen(InPenFloorX, InPenFloorY, 0, 1, PrivacyMapData, prisonzone, DirectionPressed.Up)), this.GetPrivacyFromInsidePen(InPenFloorX, InPenFloorY, 0, -1, PrivacyMapData, prisonzone, DirectionPressed.Down));
    }

    private int GetPrivacyFromInsidePen(
      int InPenFloorX,
      int InPenFloorY,
      int XInc,
      int YInc,
      int[,] PrivacyMapData,
      PrisonZone prisonzone,
      DirectionPressed LookThisWayToSeePen)
    {
      int num1 = 0;
      float num2 = 1f;
      int num3 = 1;
      int num4 = 0;
      float num5 = 2f;
      int num6 = 2;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = true;
      if (152 != InPenFloorX || 220 != InPenFloorY)
        ;
      while (num6 > 0)
      {
        InPenFloorX += XInc;
        InPenFloorY += YInc;
        if (InPenFloorX < 0 || InPenFloorY < 0 || (InPenFloorX >= this.layout.FloorTileTypes.GetLength(0) || InPenFloorY >= this.layout.FloorTileTypes.GetLength(1)))
          return num4;
        if (flag3)
        {
          if (TileData.IsThisAPenFloor(this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype))
          {
            if (this.layout.BaseTileTypes[InPenFloorX, InPenFloorY] != null)
            {
              if (TileData.IsThisAShelter(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype))
                return 100;
              if (this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.None)
              {
                num4 += (int) ((double) TileData.GetTileInfo(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype).PrivacyBlock * (double) num2);
                if (num4 >= 100)
                  return 100;
              }
            }
            if (flag1)
            {
              bool flag4 = false;
              for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
              {
                if (prisonzone.FloorTiles[index].X == InPenFloorX && prisonzone.FloorTiles[index].Y == InPenFloorY)
                  flag4 = true;
              }
              if (!flag4)
              {
                flag2 = true;
                flag3 = false;
              }
            }
          }
          else if (this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype == TILETYPE.Volume_Water && TileData.IsThisAPenFloor(this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].UnderFloorTiletype))
          {
            if (flag1)
            {
              bool flag4 = false;
              for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
              {
                if (prisonzone.FloorTiles[index].X == InPenFloorX && prisonzone.FloorTiles[index].Y == InPenFloorY)
                  flag4 = true;
              }
              if (!flag4)
              {
                flag2 = true;
                flag3 = false;
              }
            }
          }
          else if (this.layout.BaseTileTypes[InPenFloorX, InPenFloorY] != null && TileData.IsThisAPenBoundary(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype))
          {
            flag1 = true;
            if (num1 == 1)
              num4 += 50;
            ++num1;
          }
          else
          {
            --num6;
            if (this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.Volume_Water && this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.Volume_Grass)
            {
              if (this.layout.BaseTileTypes[InPenFloorX, InPenFloorY] != null && this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.None)
              {
                num4 += (int) ((double) TileData.GetTileInfo(this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype).PrivacyBlock * (double) num2);
                if (num4 >= 100)
                  return 100;
              }
              else
              {
                if ((double) num6 < (double) num5 - 1.0)
                {
                  float num7 = (float) (1.0 - ((double) num6 + 1.0) / (double) num5);
                  if (TileMath.TileIsInBuildablePartOfWorld(InPenFloorX, InPenFloorY))
                  {
                    int num8 = -(1000 + (int) ((double) num7 * 100.0));
                    if (num8 > PrivacyMapData[InPenFloorX, InPenFloorY] || PrivacyMapData[InPenFloorX, InPenFloorY] > -500)
                      PrivacyMapData[InPenFloorX, InPenFloorY] = num8;
                  }
                  return (int) ((double) num4 * (double) num7);
                }
                if (PrivacyMapData[InPenFloorX, InPenFloorY] != -1000)
                  prisonzone.AddViewingLocation(InPenFloorX, InPenFloorY, (float) num4, LookThisWayToSeePen, 0, PrivacyMapData[InPenFloorX, InPenFloorY] < -1000);
                PrivacyMapData[InPenFloorX, InPenFloorY] = -1000;
                for (int index = num6; index > 0; --index)
                {
                  --num6;
                  InPenFloorX += XInc;
                  InPenFloorY += YInc;
                  if (InPenFloorX >= 0 && InPenFloorY >= 0 && (InPenFloorX < this.layout.FloorTileTypes.GetLength(0) && InPenFloorY < this.layout.FloorTileTypes.GetLength(1)) && (this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.Volume_Water && this.layout.FloorTileTypes[InPenFloorX, InPenFloorY].tiletype != TILETYPE.Volume_Grass && (this.layout.BaseTileTypes[InPenFloorX, InPenFloorY] == null || this.layout.BaseTileTypes[InPenFloorX, InPenFloorY].tiletype == TILETYPE.None)))
                  {
                    float num7 = (float) (1.0 - ((double) num6 + 1.0) / (double) num5);
                    int num8 = -(1000 + (int) ((double) num7 * 100.0));
                    if (num8 > PrivacyMapData[InPenFloorX, InPenFloorY] || PrivacyMapData[InPenFloorX, InPenFloorY] > -500)
                    {
                      PrivacyMapData[InPenFloorX, InPenFloorY] = num8;
                      prisonzone.AddViewingLocation(InPenFloorX, InPenFloorY, (float) num4 * num7, LookThisWayToSeePen, 1);
                    }
                  }
                }
                return num4;
              }
            }
            if (num6 == 0)
              return 100;
          }
        }
        if ((double) num2 > 0.0500000007450581)
        {
          ++num3;
          if (num3 > 2)
          {
            num2 -= 0.15f;
            if ((double) num2 < 0.0500000007450581)
              num2 = 0.05f;
          }
        }
        if (flag2)
          return 100;
      }
      return num4;
    }

    public void SavePrisonLayout(Writer writer)
    {
      this.trashandstuff.SaveTrashAndStuff(writer);
      this.cellblockcontainer.SaveSaveCellblockMananger(writer);
      this.Facilities.SaveZ_Facilities(writer);
      this.layout.SaveLayoutData(writer);
      writer.WriteInt("c", PrisonLayout.CELLNUMBER);
      writer.WriteInt("c", this.AnimalsNotInPens.Count);
      for (int index = 0; index < this.AnimalsNotInPens.Count; ++index)
        this.AnimalsNotInPens[index].SavePrisonerInfo(writer);
    }

    public PrisonLayout(Reader reader, Player player, int VersionNumberForLoad)
    {
      this.trashandstuff = new TrashAndStuff(reader);
      this.cellblockcontainer = new CellblockMananger(reader, VersionNumberForLoad);
      this.Facilities = new Z_Facilities(reader);
      this.layout = new LayoutData(reader);
      int num1 = (int) reader.ReadInt("c", ref PrisonLayout.CELLNUMBER);
      this.AnimalsNotInPens = new List<PrisonerInfo>();
      int _out = 0;
      int num2 = (int) reader.ReadInt("c", ref _out);
      for (int index = 0; index < _out; ++index)
        this.AnimalsNotInPens.Add(new PrisonerInfo(reader, VersionNumberForLoad));
      this.cellblockcontainer.SetBehindGateAndArrowsOnLoad(this.layout);
    }
  }
}
