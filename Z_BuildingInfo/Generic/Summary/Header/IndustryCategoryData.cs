// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Generic.Summary.Header.IndustryCategoryData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuildingInfo.Generic.Summary.Header
{
  internal class IndustryCategoryData
  {
    public static IndustryType GetBuildingToIndustryType(TILETYPE tileType)
    {
      if (TileData.IsAnIncinerator(tileType) || TileData.IsAFactory(tileType) || (TileData.IsAMeatProcessingPlant(tileType) || TileData.IsAVegetableProcessingPlant(tileType)) || TileData.IsAWarehouse(tileType))
        return IndustryType.Industry;
      return TileData.IsThisAShopWithShopStats(tileType) ? IndustryType.Commerce : IndustryType.Count;
    }

    public static string GetIndustryTypeToString(IndustryType industryType)
    {
      if (industryType == IndustryType.Industry)
        return "Industry";
      return industryType == IndustryType.Commerce ? "Commerce" : "NA_" + (object) industryType;
    }
  }
}
