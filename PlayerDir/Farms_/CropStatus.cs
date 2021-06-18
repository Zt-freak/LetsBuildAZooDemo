// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Farms_.CropStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.Farm;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_Farms;

namespace TinyZoo.PlayerDir.Farms_
{
  internal class CropStatus
  {
    public CROPTYPE cropgrowinghere;
    private List<PlantStatus> plants;

    public CropStatus(PerimeterBuilder perimeterBuilder)
    {
      this.cropgrowinghere = CROPTYPE.None;
      this.plants = new List<PlantStatus>();
      List<BuildTile> floors = perimeterBuilder.GetFloors();
      for (int index = 0; index < floors.Count; ++index)
        this.plants.Add(new PlantStatus(floors[index].TileLocation));
    }

    public void ClearFarmOfCrop(Player player)
    {
      this.cropgrowinghere = CROPTYPE.None;
      bool WillRemakeTileList = false;
      for (int index = 0; index < this.plants.Count; ++index)
      {
        this.plants[index].SetNewPlant();
        this.plants[index].SetNewRenderState(player, this.cropgrowinghere, ref WillRemakeTileList);
      }
      if (!WillRemakeTileList)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }

    public string GetPlantedFieldDescription()
    {
      int[] numArray = new int[9];
      for (int index = 0; index < this.plants.Count; ++index)
        ++numArray[(int) this.plants[index].plantstate];
      return "Current status of this crop:~" + "Total Alotments x" + (object) this.plants.Count + "~Waiting for seed x" + (object) numArray[1] + "~Growing x" + (object) (numArray[5] + numArray[7] + numArray[6]) + "~Ready for harvesting x" + (object) numArray[8] + "~Dead Plants x" + (object) numArray[3] + "~Average Growth time: TEMP - 2 days" + "~Average Crop Value: TEMP - $4.30" + "~Average Crop Yield When Harvested: TEMP - 130%";
    }

    public void MoveCrop(Vector2Int MovedThisMuch)
    {
      for (int index = 0; index < this.plants.Count; ++index)
        this.plants[index].MoveCrop(MovedThisMuch);
    }

    public void SetNewCrop(CROPTYPE crop, Player player)
    {
      bool WillRemakeTileList = false;
      bool flag = false;
      if (this.cropgrowinghere == crop)
        return;
      this.cropgrowinghere = crop;
      for (int index = 0; index < this.plants.Count; ++index)
      {
        if (this.plants[index].plantstate != PlantState.Sprinkler)
        {
          flag = true;
          this.plants[index].SetNewPlant();
          this.plants[index].SetNewRenderState(player, this.cropgrowinghere, ref WillRemakeTileList);
        }
      }
      if (!flag)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }

    public void CropPickerReachedPlantLocation(
      Vector2Int location,
      Player player,
      out float Harvested)
    {
      Harvested = 0.0f;
      if (this.cropgrowinghere == CROPTYPE.None)
        return;
      bool WillRemakeTileList = false;
      for (int index = 0; index < this.plants.Count; ++index)
      {
        if (this.plants[index].plantstate != PlantState.Sprinkler && (this.plants[index].plantstate == PlantState.Dead || this.plants[index].plantstate == PlantState.Fruited || this.plants[index].plantstate == PlantState.WaitingForSeed))
          this.plants[index].CropPickerReachedPlot(this.cropgrowinghere, player, this.cropgrowinghere, ref WillRemakeTileList, ref Harvested);
      }
      double num = (double) Harvested;
      if (!WillRemakeTileList)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }

    public void FarmerReachedPlantLocation(Vector2Int location, Player player)
    {
      if (this.cropgrowinghere == CROPTYPE.None)
        return;
      float Harvested = 0.0f;
      bool WillRemakeTileList = false;
      for (int index = 0; index < this.plants.Count; ++index)
      {
        if (this.plants[index].plantstate != PlantState.Sprinkler && (this.plants[index].plantstate == PlantState.Dead || this.plants[index].plantstate == PlantState.Fruited || this.plants[index].plantstate == PlantState.WaitingForSeed))
          this.plants[index].FarmerReachPlot(this.cropgrowinghere, player, this.cropgrowinghere, ref WillRemakeTileList, ref Harvested);
      }
      if (!WillRemakeTileList)
        return;
      OverWorldManager.overworldenvironment.wallsandfloors.RemakeTileList();
    }

    public void StartNewQuarterDay(
      Player player,
      bool IsFirstDaySegemnet,
      int FarmUID,
      ref bool RemakeTileList)
    {
      if (this.cropgrowinghere == CROPTYPE.None)
        return;
      LocationsByFarm Harvest = new LocationsByFarm(FarmUID);
      LocationsByFarm Seeds = new LocationsByFarm(FarmUID);
      CropInfo cropInfo = CropData.GetCropInfo(this.cropgrowinghere);
      for (int index = 0; index < this.plants.Count; ++index)
      {
        if (this.plants[index].plantstate != PlantState.Sprinkler)
          this.plants[index].StartQuarterDay(true, cropInfo, player, this.cropgrowinghere, Harvest, Seeds, ref RemakeTileList);
      }
      if (Harvest.Locations.Count > 0)
        Current_FarmDestinations.FarmIDsReadyForHarvesting.Add(Harvest);
      if (Seeds.Locations.Count <= 0)
        return;
      Current_FarmDestinations.FarmIDsReadyForSeeding.Add(Seeds);
    }

    public CropStatus(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("f", ref _out);
      this.cropgrowinghere = (CROPTYPE) _out;
      int num2 = (int) reader.ReadInt("f", ref _out);
      this.plants = new List<PlantStatus>();
      for (int index = 0; index < _out; ++index)
        this.plants.Add(new PlantStatus(reader));
      int num3 = 0;
      while (num3 < _out)
        ++num3;
    }

    public void SaveCropStatus(Writer writer)
    {
      writer.WriteInt("f", (int) this.cropgrowinghere);
      writer.WriteInt("f", this.plants.Count);
      for (int index = 0; index < this.plants.Count; ++index)
        this.plants[index].SavePlantStatus(writer);
    }
  }
}
