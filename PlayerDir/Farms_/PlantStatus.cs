// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Farms_.PlantStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Farm;
using TinyZoo.Z_Farms;

namespace TinyZoo.PlayerDir.Farms_
{
  internal class PlantStatus
  {
    public Vector2Int Location;
    private float Hydration;
    private float GrowthProgress;
    private float YieldProgress;
    public PlantState plantstate;

    public PlantStatus(Vector2Int _Location)
    {
      this.Location = new Vector2Int(_Location);
      this.plantstate = PlantState.None;
    }

    public void SetNewPlant()
    {
      this.plantstate = PlantState.WaitingForSeed;
      this.GrowthProgress = 0.0f;
      this.Hydration = 0.0f;
      this.YieldProgress = 0.0f;
    }

    public void MoveCrop(Vector2Int MovedThisMuch)
    {
      this.Location.X += MovedThisMuch.X;
      this.Location.Y += MovedThisMuch.Y;
    }

    public void SetNewRenderState(Player player, CROPTYPE crophere, ref bool WillRemakeTileList)
    {
      LayoutEntry baseTileType = player.prisonlayout.layout.BaseTileTypes[this.Location.X, this.Location.Y];
      player.prisonlayout.SellStructure(this.Location, baseTileType, player.livestats.consumptionstatus, player);
      TILETYPE cropToTileType = CropData.GetCropToTileType(crophere, this.plantstate);
      switch (cropToTileType)
      {
        case TILETYPE.None:
        case TILETYPE.Count:
          if (OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: this.Location, DoRemakeTileLists: false, HyperOptimized: true))
            break;
          WillRemakeTileList = true;
          break;
        default:
          player.prisonlayout.BuildStructure(cropToTileType, this.Location, player.livestats.consumptionstatus, player, 0, false);
          goto case TILETYPE.None;
      }
    }

    public void CropPickerReachedPlot(
      CROPTYPE crop,
      Player player,
      CROPTYPE croptype,
      ref bool WillRemakeTileList,
      ref float Harvested)
    {
      if (this.plantstate == PlantState.Fruited)
      {
        Harvested += this.YieldProgress;
        this.SetNewPlant();
        this.plantstate = PlantState.WaitingForSeed;
        this.SetNewRenderState(player, croptype, ref WillRemakeTileList);
      }
      else
      {
        if (this.plantstate != PlantState.Dead)
          return;
        this.SetNewPlant();
        this.plantstate = PlantState.Seeded;
        this.SetNewRenderState(player, croptype, ref WillRemakeTileList);
      }
    }

    public void FarmerReachPlot(
      CROPTYPE crop,
      Player player,
      CROPTYPE croptype,
      ref bool WillRemakeTileList,
      ref float Harvested)
    {
      if (this.plantstate == PlantState.WaitingForSeed)
      {
        this.plantstate = PlantState.Seeded;
        this.SetNewRenderState(player, croptype, ref WillRemakeTileList);
      }
      else if (this.plantstate == PlantState.Fruited)
      {
        this.SetNewPlant();
        Harvested += this.YieldProgress;
        this.plantstate = PlantState.WaitingForSeed;
        this.SetNewRenderState(player, croptype, ref WillRemakeTileList);
      }
      else
      {
        if (this.plantstate != PlantState.Dead)
          return;
        this.SetNewPlant();
        this.plantstate = PlantState.WaitingForSeed;
        this.SetNewRenderState(player, croptype, ref WillRemakeTileList);
      }
    }

    public void StartQuarterDay(
      bool HasWater,
      CropInfo info,
      Player player,
      CROPTYPE croptype,
      LocationsByFarm Harvest,
      LocationsByFarm Seeds,
      ref bool RemakeTileList)
    {
      if (this.plantstate >= PlantState.Seeded)
      {
        if (HasWater)
        {
          this.Hydration += 0.25f;
          if ((double) this.Hydration > 1.0)
            this.Hydration = 1f;
        }
        else
          this.Hydration -= 0.25f;
        if ((double) this.GrowthProgress < 1.0)
        {
          this.GrowthProgress += info.ProgressPerQuarterDay;
          this.YieldProgress += info.ProgressPerQuarterDay * this.Hydration;
        }
        else
          this.GrowthProgress += info.ProgressPerQuarterDay;
        bool flag = false;
        if (this.plantstate == PlantState.Seeded && (double) this.GrowthProgress > 0.100000001490116)
        {
          flag = true;
          this.plantstate = PlantState.Seedling;
        }
        if (this.plantstate == PlantState.Seedling && (double) this.GrowthProgress > 0.400000005960464)
        {
          flag = true;
          this.plantstate = PlantState.YoungPlant;
        }
        if (this.plantstate == PlantState.YoungPlant && (double) this.GrowthProgress >= 1.0)
        {
          flag = true;
          this.plantstate = PlantState.Fruited;
        }
        if (this.plantstate == PlantState.Fruited && (double) this.GrowthProgress >= 1.5)
        {
          flag = true;
          this.plantstate = PlantState.Dead;
        }
        if (flag)
          this.SetNewRenderState(player, croptype, ref RemakeTileList);
      }
      if (this.plantstate == PlantState.WaitingForSeed || this.plantstate == PlantState.Dead)
      {
        Seeds.Locations.Add(this.Location);
      }
      else
      {
        if (this.plantstate != PlantState.Fruited)
          return;
        Harvest.Locations.Add(this.Location);
      }
    }

    public PlantStatus(Reader reader)
    {
      this.Location = new Vector2Int(reader);
      int num1 = (int) reader.ReadFloat("f", ref this.Hydration);
      int num2 = (int) reader.ReadFloat("f", ref this.GrowthProgress);
      int num3 = (int) reader.ReadFloat("f", ref this.YieldProgress);
      int _out = 0;
      int num4 = (int) reader.ReadInt("f", ref _out);
      this.plantstate = (PlantState) _out;
    }

    public void SavePlantStatus(Writer writer)
    {
      this.Location.SaveVector2Int(writer);
      writer.WriteFloat("f", this.Hydration);
      writer.WriteFloat("f", this.GrowthProgress);
      writer.WriteFloat("f", this.YieldProgress);
      writer.WriteInt("f", (int) this.plantstate);
    }
  }
}
