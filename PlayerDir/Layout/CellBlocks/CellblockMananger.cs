// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.CellblockMananger
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout.CellBlocks.OtherBuildings;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.PlayerDir.StoreRooms;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.CustomerStats;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_HeatMaps;

namespace TinyZoo.PlayerDir.Layout.CellBlocks
{
  internal class CellblockMananger
  {
    private InfrastructureSave infrastructure;
    public List<PrisonZone> prisonzones;
    public List<HoldingCellInfo> holdingcells;
    public List<GraveYardBlockInfo> graveblocks;
    public int TEMP_TotalPrizoneZonePopularity;

    public CellblockMananger()
    {
      this.infrastructure = new InfrastructureSave();
      this.prisonzones = new List<PrisonZone>();
      this.holdingcells = new List<HoldingCellInfo>();
      this.graveblocks = new List<GraveYardBlockInfo>();
    }

    public bool RemoveThisAnimalFromCellBlock(PrisonerInfo prisonerinfo, Player player)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].prisonercontainer.RemoveThisAnimalFromCellBlock(prisonerinfo))
        {
          this.prisonzones[index].prisonercontainer.SetUpTempAnimals(this.prisonzones[index].Cell_UID, this.prisonzones[index].CellBLOCKTYPE, player, true);
          player.prisonlayout.GetHungryAnaimals(player);
          return true;
        }
      }
      return false;
    }

    public float GetAnimalWellfareFromPen(int CellUID)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].Cell_UID == CellUID)
          return this.prisonzones[index].WelfareAndCleanliness;
      }
      throw new Exception("PEN DOESNT EXIST");
    }

    public int FindCellBlockWIthThisAnimal(AnimalType animal)
    {
      for (int index1 = 0; index1 < this.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype == animal)
            return this.prisonzones[index1].Cell_UID;
        }
      }
      return -1;
    }

    public bool BreakGate(int CellUID, Player player)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].Cell_UID == CellUID)
          return this.prisonzones[index].BreakGate(player);
      }
      return false;
    }

    public bool RemoveThisAnimalOnMoveToOtherBuilding(
      int UID,
      AnimalType animaltype,
      out int CellBlockUID,
      Player player)
    {
      CellBlockUID = 0;
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].prisonercontainer.RemoveThisPrisonerByUID(UID, animaltype))
        {
          CellBlockUID = this.prisonzones[index].Cell_UID;
          player.prisonlayout.cellblockcontainer.prisonzones[index].prisonercontainer.SetUpTempAnimals(this.prisonzones[index].Cell_UID, this.prisonzones[index].CellBLOCKTYPE, player, true);
          player.prisonlayout.GetHungryAnaimals(player);
          return true;
        }
      }
      return false;
    }

    public bool ThesePeopleAreInThePrison(IntakeInfo intakeUseForQuit)
    {
      for (int index1 = 0; index1 < this.holdingcells.Count; ++index1)
      {
        for (int index2 = 0; index2 < intakeUseForQuit.People.Count; ++index2)
        {
          if (this.holdingcells[index1].prisonercontainer.GetThisPrisoner(intakeUseForQuit.People[index2]) != null)
            return true;
        }
      }
      return false;
    }

    public void CheckGraveStonesOnLoad(LayoutData layout)
    {
      for (int index = 0; index < this.graveblocks.Count; ++index)
        this.graveblocks[index].CheckGraveStonesOnLoad(layout);
    }

    public void SetUpAllStock(Player player)
    {
      player.livestats.stocktimes = new StockTime(player);
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].SetUpAllStock(player);
    }

    public float GetInfrastuctureValue(out float DecorativesValue) => this.infrastructure.GetInfrastuctureValue(out DecorativesValue);

    public void GetHungryAnaimals(Player player)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].GetHungryAnaimals(player);
    }

    public int GetTotalCrossbreed(AnimalType AnimalA, AnimalType AnimalB)
    {
      int num = 0;
      for (int index = 0; index < this.prisonzones.Count; ++index)
        num += this.prisonzones[index].GetTotalCrossbreed(AnimalA, AnimalB);
      return num;
    }

    public int AddNewAnimal(
      AnimalType animaltype,
      int CellID,
      int Variant,
      bool IsAGirl,
      out int ChildUID,
      int MotherUID,
      int FatherUID)
    {
      ChildUID = -1;
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].Cell_UID == CellID)
        {
          PrisonerInfo prisonerInfo = new PrisonerInfo(new IntakePerson(animaltype, _IsAGirl: IsAGirl, Variant: Variant), false, this.prisonzones[index].GetRandomLocationInCellBlock(), this.prisonzones[index].CellBLOCKTYPE);
          prisonerInfo.MotherUID = MotherUID;
          prisonerInfo.FatherUID = FatherUID;
          Player.financialrecords.AnimalAddedToZoo();
          this.prisonzones[index].prisonercontainer.prisoners.Add(prisonerInfo);
          this.prisonzones[index].prisonercontainer.prisoners[this.prisonzones[index].prisonercontainer.prisoners.Count - 1].ResetOnMoveCell(this.prisonzones[index].CellBLOCKTYPE);
          this.prisonzones[index].prisonercontainer.FoodForAnimals.AddAnimal(prisonerInfo.intakeperson.animaltype);
          this.prisonzones[index].prisonercontainer.ThisWasTehCellBlockThatChanged = true;
          ChildUID = this.prisonzones[index].prisonercontainer.prisoners[this.prisonzones[index].prisonercontainer.prisoners.Count - 1].intakeperson.UID;
          GameFlags.CellBlockContentsChanged = true;
          return prisonerInfo.intakeperson.UID;
        }
      }
      return -1;
    }

    public PrisonerInfo GetThisAnimal(int UID, out int Cell_UID)
    {
      Cell_UID = 0;
      for (int index1 = 0; index1 < this.prisonzones.Count; ++index1)
      {
        for (int index2 = this.prisonzones[index1].prisonercontainer.prisoners.Count - 1; index2 > -1; --index2)
        {
          if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID == UID)
          {
            Cell_UID = this.prisonzones[index1].Cell_UID;
            return this.prisonzones[index1].prisonercontainer.prisoners[index2];
          }
        }
      }
      return (PrisonerInfo) null;
    }

    public bool HasABuildng_Shop() => this.infrastructure.HasABuildng_Shop();

    public bool HasAnArchitectureBuilding() => this.infrastructure.HasAnArchitectureBuilding();

    public bool RemoveTheseAnimalsOnTrade(
      int Total,
      AnimalType animal,
      int VariantIndex,
      int BoysUsed)
    {
      int num = Total - BoysUsed;
      for (int index1 = 0; index1 < this.prisonzones.Count; ++index1)
      {
        for (int index2 = this.prisonzones[index1].prisonercontainer.prisoners.Count - 1; index2 > -1; --index2)
        {
          if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype == animal && this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex == VariantIndex)
          {
            if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl && num > 0)
            {
              --num;
              GameFlags.CellBlockContentsChanged = true;
              this.prisonzones[index1].prisonercontainer.prisoners.RemoveAt(index2);
              this.prisonzones[index1].prisonercontainer.ThisWasTehCellBlockThatChanged = true;
            }
            else if (!this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl && BoysUsed > 0)
            {
              --BoysUsed;
              this.prisonzones[index1].prisonercontainer.prisoners.RemoveAt(index2);
              this.prisonzones[index1].prisonercontainer.ThisWasTehCellBlockThatChanged = true;
              GameFlags.CellBlockContentsChanged = true;
            }
          }
        }
      }
      if (BoysUsed > 0 || num > 0)
        throw new Exception("NO WAY");
      return true;
    }

    public bool RemoveSpecificAnimals_TradeOrSell(List<PrisonerInfo> animals, Player player)
    {
      if (animals == null)
        return true;
      bool flag = true;
      for (int index = 0; index < animals.Count; ++index)
      {
        if (!this.RemoveThisAnimalFromCellBlock(animals[index], player))
          flag = false;
      }
      GameFlags.CellBlockContentsChanged = true;
      return flag;
    }

    public bool HasThisAlienSomewhere(AnimalType enemytype)
    {
      for (int index = 0; index < this.graveblocks.Count; ++index)
      {
        if (this.graveblocks[index].HasThisAlienSomewhere(enemytype))
          return true;
      }
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].HasThisAlienSomewhere(enemytype))
          return true;
      }
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].HasThisAlienSomewhere(enemytype))
          return true;
      }
      return false;
    }

    public void TransferPrisonersToHoldingCell_FromIntake(
      WaveInfo waveinfo,
      HoldingCellInfo Transfertothis,
      Player player)
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].IsThisLocationTheSame(Transfertothis.HoldingCellRoot))
          this.holdingcells[index].TransferPrisonersToHoldingCell_FromIntake(waveinfo.People, player);
      }
    }

    public void TransferPrisonerToHoldingCell_FromMap(IntakePerson thisperson) => throw new Exception("NOT IN GAME - IF USED remember to add food");

    public int GetCountOfThisSpecialBuilding(TILETYPE strcture) => this.infrastructure.GetCountOfThisSpecialBuilding(strcture);

    public DeadPerson GetThisGraveyardBlockDeadPerson(Vector2Int location)
    {
      for (int index = this.graveblocks.Count - 1; index > -1; --index)
      {
        if (this.graveblocks[index].IsThisLocationInThisDungeon(location))
          return this.graveblocks[index].GetThisGraveyardBlockDeadPerson(location);
      }
      return (DeadPerson) null;
    }

    public void SellGraveYard(GraveYardBlockInfo grave)
    {
      for (int index = this.graveblocks.Count - 1; index > -1; --index)
      {
        if (this.graveblocks[index] == grave)
          this.graveblocks.RemoveAt(index);
      }
    }

    public PrisonZone GetThisCellBlock(int PrisonUID)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].Cell_UID == PrisonUID)
          return this.prisonzones[index];
      }
      return (PrisonZone) null;
    }

    public void Sellprison(int CellUID, Player player)
    {
      for (int index1 = this.prisonzones.Count - 1; index1 > -1; --index1)
      {
        if (this.prisonzones[index1].Cell_UID == CellUID)
        {
          if (this.prisonzones[index1].prisonercontainer.prisoners.Count > 0)
          {
            for (int index2 = 0; index2 < this.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
              this.AddPrisonerToFirstEmotyHoldingCell(this.prisonzones[index1].prisonercontainer.prisoners[index2]);
          }
          this.prisonzones.RemoveAt(index1);
          Enclosure_Farm_Map.MustRecreateMap = true;
          CalculateStat.RebuildAnimalMap = true;
          Z_GameFlags.RecheckZooKeeperZones = true;
          for (int index2 = 0; index2 < player.animalsonorder.animalsonorder.Count; ++index2)
          {
            if (player.animalsonorder.animalsonorder[index2].PrisonUID == CellUID)
            {
              if (this.prisonzones.Count <= 0)
                throw new Exception("YUOU CANNOT DESTROY LAST ENCLOSURE");
              player.animalsonorder.animalsonorder[index2].PrisonUID = this.prisonzones[TinyZoo.Game1.Rnd.Next(0, this.prisonzones.Count)].Cell_UID;
            }
          }
        }
      }
    }

    private void AddPrisonerToFirstEmotyHoldingCell(PrisonerInfo prisoner)
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].GetFreeTransferSlots() > 0)
        {
          this.holdingcells[index].StickPrisonerInThisCell(prisoner);
          break;
        }
      }
    }

    public HoldingCellInfo GetThisHoldingCell(Vector2Int TileLocation)
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].IsThisLocationTheSame(TileLocation))
          return this.holdingcells[index];
      }
      return (HoldingCellInfo) null;
    }

    public GraveYardBlockInfo GetThisGraveyardBlockInfo(Vector2Int TileLocation)
    {
      for (int index = 0; index < this.graveblocks.Count; ++index)
      {
        if (this.graveblocks[index].IsThisLocationInThisDungeon(TileLocation))
          return this.graveblocks[index];
      }
      return (GraveYardBlockInfo) null;
    }

    public PrisonZone GetThisCellBlock(Vector2Int Location, bool WillThrow = true)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].IsThisLocationInThisDungeon(Location))
          return this.prisonzones[index];
      }
      if (!WillThrow)
        return (PrisonZone) null;
      throw new Exception("NO ZONE");
    }

    public void DoParole(IntakePerson person)
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].prisonercontainer.DoParole(person, CellBlockType.HoldingCell))
          return;
      }
      int index1 = 0;
      while (index1 < this.prisonzones.Count && !this.prisonzones[index1].prisonercontainer.DoParole(person, this.prisonzones[index1].CellBLOCKTYPE))
        ++index1;
    }

    public void AddNewlyDead(IntakePerson intakeperson, ref Vector2Int GraveMiddle)
    {
      for (int index = 0; index < this.graveblocks.Count; ++index)
      {
        if (this.graveblocks[index].HasThisMuchSpaceInGraveYard() > 0)
        {
          GraveMiddle = this.graveblocks[index].AddNewlyDead(intakeperson);
          break;
        }
      }
    }

    public int GetTotalOfThisAnimal(AnimalType enemy, int Variant = -1)
    {
      int num = 0;
      for (int index1 = 0; index1 < this.prisonzones.Count; ++index1)
      {
        if (enemy == AnimalType.None)
        {
          num += this.prisonzones[index1].prisonercontainer.prisoners.Count;
        }
        else
        {
          for (int index2 = 0; index2 < this.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
          {
            if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype == enemy)
            {
              if (Variant > -1)
              {
                if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex == Variant)
                  ++num;
              }
              else
                ++num;
            }
          }
        }
      }
      return num;
    }

    public int GetTotalOfThisALien(AnimalType enemy, int Variant, bool IsAGirl)
    {
      int ThisVariantGirls;
      int ThisVariantBoys;
      this.GetTotalOfThisALien(enemy, Variant, out int _, out int _, out ThisVariantGirls, out ThisVariantBoys);
      return IsAGirl ? ThisVariantGirls : ThisVariantBoys;
    }

    public int GetTotalOfThisALien(
      AnimalType enemy,
      int Variant,
      out int GirlsIncludingVariants,
      out int BoysIncludingVariants,
      out int ThisVariantGirls,
      out int ThisVariantBoys)
    {
      ThisVariantGirls = 0;
      ThisVariantBoys = 0;
      GirlsIncludingVariants = 0;
      BoysIncludingVariants = 0;
      int num = 0;
      for (int index1 = 0; index1 < this.holdingcells.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.holdingcells[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (this.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype == enemy)
          {
            if (this.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex == Variant)
            {
              if (this.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl)
                ++ThisVariantGirls;
              else
                ++ThisVariantBoys;
              ++num;
            }
            if (this.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl)
              ++GirlsIncludingVariants;
            else
              ++BoysIncludingVariants;
          }
        }
      }
      for (int index1 = 0; index1 < this.prisonzones.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype == enemy)
          {
            if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.CLIndex == Variant)
            {
              if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl)
                ++ThisVariantGirls;
              else
                ++ThisVariantBoys;
              ++num;
            }
            if (this.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.IsAGirl)
              ++GirlsIncludingVariants;
            else
              ++BoysIncludingVariants;
          }
        }
      }
      return num;
    }

    public void ResetForLanguage()
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
        this.holdingcells[index].prisonercontainer.ResetForLanguage();
      for (int index = 0; index < this.graveblocks.Count; ++index)
        this.graveblocks[index].ResetForLanguage();
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].prisonercontainer.ResetForLanguage();
    }

    public void MoveGate(int CELLNUMBER, GatePlacementManager gateplacer, Player player)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
      {
        if (this.prisonzones[index].Cell_UID == CELLNUMBER)
          this.prisonzones[index].MoveGate(gateplacer, player);
      }
    }

    public void AddNewIrregularCellBlock(
      PerimeterBuilder perimeterBuilder,
      int CELLNUMBER,
      CellBlockType cellblocktype,
      GatePlacementManager gateplacer,
      bool AddToCollisionCheckList = false)
    {
      this.prisonzones.Add(new PrisonZone(perimeterBuilder, CELLNUMBER, cellblocktype, gateplacer, AddToCollisionCheckList));
      if (this.prisonzones[this.prisonzones.Count - 1].IsFarm)
        return;
      Z_GameFlags.RecheckZooKeeperZones = true;
    }

    public void AddNewCellBlock(
      int XWallLoc,
      int YWallLocation,
      int WidthIncludingWalls,
      int HeightIncludingWalls,
      int CELLNUMBER,
      bool IsGraveYard,
      CellBlockType cellblocktype)
    {
      if (IsGraveYard)
        this.graveblocks.Add(new GraveYardBlockInfo(XWallLoc + 1, YWallLocation + 1, WidthIncludingWalls - 2, HeightIncludingWalls - 2));
      else
        this.prisonzones.Add(new PrisonZone(XWallLoc + 1, YWallLocation + 1, WidthIncludingWalls - 2, HeightIncludingWalls - 2, CELLNUMBER, cellblocktype, new Vector2Int(-1, -1)));
    }

    public bool IsThisPrisonerInAHoldingCell(IntakePerson person)
    {
      for (int index1 = 0; index1 < this.holdingcells.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.holdingcells[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (this.holdingcells[index1].prisonercontainer.prisoners[index2].intakeperson == person)
            return true;
        }
      }
      return false;
    }

    public void CheckAndRemoveDuplicatesFromHoldingCells(EnemyManager enemyrenderer) => throw new Exception("THIS ISNT IN THE GAME");

    public void CheckAndRemoveDeadFromHoldingCells(EnemyManager enemyrenderer) => throw new Exception("THIS ISNT IN THE GAME");

    public int GetNumberOfGraveYards() => this.graveblocks.Count;

    public int HasThisMuchSpaceInGraveYard()
    {
      int num = 0;
      for (int index = 0; index < this.graveblocks.Count; ++index)
        num += this.graveblocks[index].HasThisMuchSpaceInGraveYard();
      return num;
    }

    public void BuildStructure(Vector2Int position, TILETYPE ThingToBuld)
    {
      if (TileData.GetTileStats(ThingToBuld) != null)
      {
        PrisonZone thisCellBlock = this.GetThisCellBlock(position, false);
        if (thisCellBlock != null)
          thisCellBlock.BuildStructure(position, ThingToBuld);
        else
          this.infrastructure.BuildStructure(position, ThingToBuld);
      }
      if (ThingToBuld != TILETYPE.HoldingCell)
        return;
      this.holdingcells.Add(new HoldingCellInfo(position));
    }

    public bool HasAnyHoldingCells() => this.holdingcells.Count > 0;

    public int HasSpaceInHoldingCells()
    {
      int num = 0;
      for (int index = 0; index < this.holdingcells.Count; ++index)
        num += this.holdingcells[index].GetFreeTransferSlots();
      return num;
    }

    public int HasHoldingCellTransferSlots(Vector2Int HoldingCellLocation)
    {
      for (int index = 0; index < this.holdingcells.Count; ++index)
      {
        if (this.holdingcells[index].HoldingCellIshere(HoldingCellLocation))
          return this.holdingcells[index].GetFreeTransferSlots();
      }
      return 0;
    }

    public int GetTotalResearch() => this.infrastructure.GetTotalResearch();

    public void TryToAddPrisonersToHoldingCells(PrisonerInfo AddThisGuy, Player player) => throw new Exception("NOT IN GAME - IF USED remember to add food");

    public void SellStructure(Vector2Int position, LayoutEntry _layoutentry, Player player)
    {
      TileStats tileStats = TileData.GetTileStats(_layoutentry.tiletype);
      if (_layoutentry.tiletype == TILETYPE.HoldingCell)
      {
        for (int index1 = this.holdingcells.Count - 1; index1 > -1; --index1)
        {
          if (this.holdingcells[index1].IsThisLocationTheSame(position))
          {
            PrisonerContainer prisonercontainer = this.holdingcells[index1].prisonercontainer;
            this.holdingcells.RemoveAt(index1);
            for (int index2 = 0; index2 < prisonercontainer.prisoners.Count; ++index2)
              this.TryToAddPrisonersToHoldingCells(prisonercontainer.prisoners[index2], player);
          }
        }
      }
      if (tileStats == null || !tileStats.ImpactsConsumption())
        return;
      PrisonZone thisCellBlock = this.GetThisCellBlock(position, false);
      if (thisCellBlock != null)
        thisCellBlock.SellStructure(position, _layoutentry);
      else
        this.infrastructure.SellStructure(position, _layoutentry);
    }

    public int GetTotalAliensHeldIncludingDeadAndHoldingCells() => this.GetTotalAliensInGraveYards() + this.GetTotalAliensInCellBlocks() + this.GetTotalAliensInHoldingCells();

    public int GetTotalAliensInGraveYards()
    {
      int num = 0;
      for (int index = 0; index < this.graveblocks.Count; ++index)
        num += this.graveblocks[index].deadpeople.deadpeople.Count;
      return num;
    }

    public int GetTotalAliensInCellBlocks()
    {
      int num = 0;
      for (int index = 0; index < this.prisonzones.Count; ++index)
        num += this.prisonzones[index].prisonercontainer.prisoners.Count;
      return num;
    }

    public int GetTotalAliensInHoldingCells()
    {
      int num = 0;
      for (int index = 0; index < this.holdingcells.Count; ++index)
        num += this.holdingcells[index].prisonercontainer.prisoners.Count;
      return num;
    }

    public void BuildTileFromTileRenderer(TileRenderer tilerenderer)
    {
      if (tilerenderer.Ref_layoutentry.tiletype == TILETYPE.HoldingCell)
        this.holdingcells.Add(new HoldingCellInfo(tilerenderer.TileLocation));
      if (!TileData.GetTileStats(tilerenderer.Ref_layoutentry.tiletype).ImpactsConsumption() && !TileData.ThisIsAMeaningfullBuilding(tilerenderer.Ref_layoutentry.tiletype))
        return;
      if (TileData.GetTileInfo(tilerenderer.Ref_layoutentry.tiletype).buildingtype == BUILDINGTYPE.Building)
        this.infrastructure.BuildTileFromTileRenderer(tilerenderer);
      else
        this.GetThisCellBlock(tilerenderer.TileLocation).AddNewStructureThing(tilerenderer);
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus, Player player)
    {
      consumptionstatus.Reset();
      this.infrastructure.SetConsumption(consumptionstatus);
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].SetConsumption(consumptionstatus);
      for (int index = 0; index < this.holdingcells.Count; ++index)
        this.holdingcells[index].prisonercontainer.SetConsumption(consumptionstatus);
      if (player.Stats.GetFlower())
        consumptionstatus.MultiplyByHalf();
      consumptionstatus.CheckConsuption();
      if (player.prisonlayout == null || !Researcher.IsCurrentlyResearching || player.prisonlayout.GetTotalResearch() == player.Stats.research.GetCurrentScientists())
        return;
      player.Stats.research.AddedOrRemovedScientist(player);
    }

    public int GetDailyEanings(
      bool IsDayEnd_CalculateParoleEtc,
      out int EarningsWithoutMod,
      out int AllMoneyIncludingWrongCell,
      Player player)
    {
      int Profit = 0;
      AllMoneyIncludingWrongCell = 0;
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].GetDailyEanings(ref Profit, IsDayEnd_CalculateParoleEtc, ref AllMoneyIncludingWrongCell);
      EarningsWithoutMod = Profit;
      Profit = player.livestats.consumptionstatus.ModifyDailyEarnings(Profit);
      return Profit;
    }

    public DeadPerson GetThisDeadPerson(Vector2Int SelectedLocation)
    {
      for (int index = 0; index < this.graveblocks.Count; ++index)
      {
        DeadPerson thisDeadPerson = this.graveblocks[index].GetThisDeadPerson(SelectedLocation);
        if (thisDeadPerson != null)
          return thisDeadPerson;
      }
      return (DeadPerson) null;
    }

    public void RemoveThisDeadPerson(DeadPerson deadperson)
    {
      int index = 0;
      while (index < this.graveblocks.Count && !this.graveblocks[index].RemoveThisDeadPerson(deadperson))
        ++index;
    }

    public void SaveSaveCellblockMananger(Writer writer)
    {
      writer.WriteInt("z", this.prisonzones.Count);
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].SavePrisonZone(writer);
      this.infrastructure.SaveInfrastructureSave(writer);
    }

    public void SetBehindGateAndArrowsOnLoad(LayoutData layout)
    {
      for (int index = 0; index < this.prisonzones.Count; ++index)
        this.prisonzones[index].SetBehindGateAndArrowsOnLoad(layout);
    }

    public CellblockMananger(Reader reader, int VersionNumberForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("z", ref _out);
      this.prisonzones = new List<PrisonZone>();
      for (int index = 0; index < _out; ++index)
        this.prisonzones.Add(new PrisonZone(reader, VersionNumberForLoad));
      this.infrastructure = new InfrastructureSave(reader);
      this.holdingcells = new List<HoldingCellInfo>();
      this.graveblocks = new List<GraveYardBlockInfo>();
    }
  }
}
