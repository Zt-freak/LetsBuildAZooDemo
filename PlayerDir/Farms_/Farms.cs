// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Farms_.Farms
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Farm;
using TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_OverWorld.SpawnAnimations;

namespace TinyZoo.PlayerDir.Farms_
{
  internal class Farms
  {
    private static int CELLNUMBER;
    public FarmFields farmfields;

    public Farms() => this.farmfields = new FarmFields();

    public void SetUpFarmSigns(Player player, int CellBlockUID = -1)
    {
      if (CellBlockUID == -1)
      {
        for (int index = 0; index < this.farmfields.farmfields.Count; ++index)
          this.farmfields.farmfields[index].CheckFarmSign(player);
      }
      else
        this.GetThisFarmFieldByUID(CellBlockUID).CheckFarmSign(player);
    }

    public PrisonZone GetThisFarmFieldByUID(int UID)
    {
      for (int index = 0; index < this.farmfields.farmfields.Count; ++index)
      {
        if (this.farmfields.farmfields[index].Cell_UID == UID)
          return this.farmfields.farmfields[index];
      }
      throw new Exception("NO ZONE");
    }

    public void ClearFarmOfCrop(int FarmUID, Player player)
    {
      PrisonZone thisFarmFieldByUid = this.GetThisFarmFieldByUID(FarmUID);
      thisFarmFieldByUid.cropsatus.ClearFarmOfCrop(player);
      OverWorldManager.overworldenvironment.wallsandfloors.SetUpFarmSigns(player, thisFarmFieldByUid.Cell_UID);
    }

    public string GetPlantedFieldDescription(int UID)
    {
      for (int index = 0; index < this.farmfields.farmfields.Count; ++index)
      {
        if (this.farmfields.farmfields[index].Cell_UID == UID)
          return this.farmfields.farmfields[index].cropsatus.GetPlantedFieldDescription();
      }
      return "No Info";
    }

    public void SetSeedType(CROPTYPE croptype, Player player)
    {
      if (!Z_GameFlags.SelectedPrisonZoneisFarm)
        throw new Exception("THIS SHOULD BE A FARM!");
      PrisonZone thisFarmFieldByUid = this.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
      thisFarmFieldByUid.cropsatus.SetNewCrop(croptype, player);
      OverWorldManager.overworldenvironment.wallsandfloors.SetUpFarmSigns(player, thisFarmFieldByUid.Cell_UID);
    }

    public void StartNewQuarterDay(Player player, bool IsDayStart = false)
    {
      if (IsDayStart)
      {
        Current_FarmDestinations.FarmIDsReadyForHarvesting = new List<LocationsByFarm>();
        Current_FarmDestinations.FarmIDsReadyForSeeding = new List<LocationsByFarm>();
      }
      bool RemakeTileList = false;
      for (int index = 0; index < this.farmfields.farmfields.Count; ++index)
        this.farmfields.farmfields[index].cropsatus.StartNewQuarterDay(player, IsDayStart, this.farmfields.farmfields[index].Cell_UID, ref RemakeTileList);
      if (!RemakeTileList)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }

    public int AddField(
      PerimeterBuilder perimeterBuilder,
      WallsAndFloorsManager wallsandfloors,
      CellBlockType cellblocktype,
      Player player,
      GatePlacementManager gateplacer,
      bool AddToCollisionCheckList = false)
    {
      int cellnumber = Farms.CELLNUMBER;
      CascadeSpawner.SetUpFloorForCascade(perimeterBuilder.GeturrentAnimalFloorSpace(), player);
      Enclosure_Farm_Map.MustRecreateMap = true;
      player.prisonlayout.layout.AddNewIrregularCellBlockCellBlock(perimeterBuilder, wallsandfloors, cellblocktype, gateplacer);
      this.farmfields.AddNewIrregularCellBlock(perimeterBuilder, cellnumber, cellblocktype, gateplacer, AddToCollisionCheckList);
      CascadeSpawner.DoCascadeSpawnForPen(this.farmfields.farmfields[this.farmfields.farmfields.Count - 1], player);
      ++Farms.CELLNUMBER;
      return cellnumber;
    }

    public PrisonZone GetThisField(Vector2Int Location, bool WillThrow = true)
    {
      for (int index = 0; index < this.farmfields.farmfields.Count; ++index)
      {
        if (this.farmfields.farmfields[index].IsThisLocationInThisDungeon(Location))
          return this.farmfields.farmfields[index];
      }
      if (!WillThrow)
        return (PrisonZone) null;
      throw new Exception("NO ZONE");
    }

    public Farms(Reader reader, int VersionNumberForLoad)
    {
      int num = (int) reader.ReadInt("f", ref Farms.CELLNUMBER);
      this.farmfields = new FarmFields(reader, VersionNumberForLoad);
    }

    public void SaveFarms(Writer writer)
    {
      writer.WriteInt("f", Farms.CELLNUMBER);
      this.farmfields.SaveFarmFields(writer);
    }
  }
}
