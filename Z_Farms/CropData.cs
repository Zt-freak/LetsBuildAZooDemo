// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Farms.CropData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Farms
{
  internal class CropData
  {
    private static List<CROPTYPE> displayCropList;
    private static CropInfo[] cropinf;

    internal static string GetCropTypeToString(CROPTYPE crop)
    {
      switch (crop)
      {
        case CROPTYPE.Corn:
          return "Corn";
        case CROPTYPE.Tomatoes:
        case CROPTYPE.Carrots:
          return "Carrots";
        case CROPTYPE.Cabbage:
          return "Cabbage";
        case CROPTYPE.Wheat:
          return "Wheat";
        case CROPTYPE.WaterMelon:
          return "Water Melon";
        case CROPTYPE.Potato:
          return "Potato";
        case CROPTYPE.Grass:
          return "Grass";
        case CROPTYPE.Lettuce:
          return "Lettuce";
        case CROPTYPE.Soybeans:
          return "Soy beans";
        case CROPTYPE.CoffeeBerries:
          return "Coffee Berries";
        case CROPTYPE.Sugarcane:
          return "Sugarcane";
        case CROPTYPE.Raspberry:
          return "Raspberry";
        case CROPTYPE.Beetroot:
          return "Beetroot";
        case CROPTYPE.Bamboo:
          return "Bamboo";
        case CROPTYPE.Nightshade:
          return "Nightshade";
        case CROPTYPE.Hops:
          return "Hops";
        default:
          return "Nothing";
      }
    }

    internal static Rectangle GetCROPTYPEToRectangle(CROPTYPE crop)
    {
      switch (crop)
      {
        case CROPTYPE.Corn:
          return new Rectangle(1864, 373, 21, 18);
        case CROPTYPE.Carrots:
          return new Rectangle(1529, 410, 20, 22);
        case CROPTYPE.Cabbage:
          return new Rectangle(1595, 410, 23, 22);
        case CROPTYPE.Wheat:
          return new Rectangle(1572, 410, 22, 22);
        case CROPTYPE.WaterMelon:
          return new Rectangle(1872, 290, 22, 21);
        case CROPTYPE.Potato:
          return new Rectangle(1851, 290, 20, 21);
        case CROPTYPE.Grass:
          return new Rectangle(1550, 410, 21, 21);
        case CROPTYPE.Lettuce:
          return new Rectangle(1619, 410, 19, 21);
        case CROPTYPE.Soybeans:
          return new Rectangle(1639, 410, 22, 21);
        case CROPTYPE.CoffeeBerries:
          return new Rectangle(1561, 514, 21, 23);
        case CROPTYPE.Sugarcane:
          return new Rectangle(1895, 290, 23, 19);
        case CROPTYPE.Raspberry:
          return new Rectangle(1919, 290, 23, 19);
        case CROPTYPE.Beetroot:
          return new Rectangle(1710, 435, 20, 22);
        case CROPTYPE.Bamboo:
          return new Rectangle(1731, 436, 20, 22);
        case CROPTYPE.Nightshade:
          return new Rectangle(1662, 411, 23, 20);
        default:
          return new Rectangle(525, 135, 17, 16);
      }
    }

    public static Rectangle GetCropTypeToSeedPacketRectangle(CROPTYPE crop)
    {
      switch (crop)
      {
        case CROPTYPE.None:
        case CROPTYPE.Count:
          return new Rectangle(0, 0, 0, 0);
        case CROPTYPE.Corn:
          return new Rectangle(1322, 705, 26, 29);
        case CROPTYPE.Carrots:
          return new Rectangle(1484, 705, 26, 29);
        case CROPTYPE.Cabbage:
          return new Rectangle(1457, 705, 26, 29);
        case CROPTYPE.Wheat:
          return new Rectangle(1268, 705, 26, 29);
        case CROPTYPE.WaterMelon:
          return new Rectangle(1511, 705, 26, 29);
        case CROPTYPE.Potato:
          return new Rectangle(1295, 705, 26, 29);
        case CROPTYPE.Grass:
          return new Rectangle(1538, 705, 26, 29);
        case CROPTYPE.Lettuce:
          return new Rectangle(1241, 705, 26, 29);
        case CROPTYPE.Soybeans:
          return new Rectangle(1430, 705, 26, 29);
        case CROPTYPE.CoffeeBerries:
          return new Rectangle(1214, 705, 26, 29);
        case CROPTYPE.Sugarcane:
          return new Rectangle(1187, 705, 26, 29);
        case CROPTYPE.Raspberry:
          return new Rectangle(1349, 705, 26, 29);
        case CROPTYPE.Beetroot:
          return new Rectangle(1592, 705, 26, 29);
        case CROPTYPE.Bamboo:
          return new Rectangle(1403, 705, 26, 29);
        case CROPTYPE.Nightshade:
          return new Rectangle(1592, 705, 26, 29);
        case CROPTYPE.Hops:
          return new Rectangle(1376, 705, 26, 29);
        default:
          return Rectangle.Empty;
      }
    }

    internal static CropInfo GetCropInfo(CROPTYPE croptype)
    {
      if (CropData.cropinf == null)
        CropData.cropinf = new CropInfo[18];
      if (CropData.cropinf[(int) croptype] == null)
      {
        switch (croptype)
        {
          case CROPTYPE.Carrots:
            CropData.cropinf[(int) croptype] = new CropInfo(1, 4);
            break;
          case CROPTYPE.Cabbage:
            CropData.cropinf[(int) croptype] = new CropInfo(1, 4);
            break;
        }
      }
      return new CropInfo(1, 8);
    }

    internal static TILETYPE GetCropToTileType(CROPTYPE croptype, PlantState plantstate)
    {
      if (plantstate == PlantState.WaitingForSeed)
        return TILETYPE.UnseededEmptyPlot;
      if (plantstate == PlantState.Seeded)
        return TILETYPE.SoilMound;
      switch (croptype)
      {
        case CROPTYPE.Corn:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Corn_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Corn_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Corn_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Corn_FullGrown;
          }
          break;
        case CROPTYPE.Carrots:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Carrots_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Carrots_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Carrots_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Carrots_FullGrown;
          }
          break;
        case CROPTYPE.Cabbage:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Cabbage_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Cabbage_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Cabbage_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Cabbage_FullGrown;
          }
          break;
        case CROPTYPE.Wheat:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Wheat_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Wheat_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Wheat_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Wheat_FullGrown;
          }
          break;
        case CROPTYPE.WaterMelon:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Watermelon_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Watermelon_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Watermelon_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Watermelon_FullGrown;
          }
          break;
        case CROPTYPE.Potato:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Potato_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Potato_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Potato_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Potato_FullGrown;
          }
          break;
        case CROPTYPE.Grass:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Grass_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Grass_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Grass_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Grass_FullGrown;
          }
          break;
        case CROPTYPE.Lettuce:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Lettuce_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Lettuce_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Lettuce_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Lettuce_FullGrown;
          }
          break;
        case CROPTYPE.Soybeans:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Soybeans_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Soybeans_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Soybeans_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Soybeans_FullGrown;
          }
          break;
        case CROPTYPE.CoffeeBerries:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.CoffeeBerries_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.CoffeeBerries_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.CoffeeBerries_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.CoffeeBerries_FullGrown;
          }
          break;
        case CROPTYPE.Sugarcane:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Sugarcane_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Sugarcane_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Sugarcane_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Sugarcane_FullGrown;
          }
          break;
        case CROPTYPE.Raspberry:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Raspberry_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Raspberry_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Raspberry_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Raspberry_FullGrown;
          }
          break;
        case CROPTYPE.Beetroot:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Beetroot_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Beetroot_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Beetroot_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Beetroot_FullGrown;
          }
          break;
        case CROPTYPE.Bamboo:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Bamboo_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Bamboo_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Bamboo_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Bamboo_FullGrown;
          }
          break;
        case CROPTYPE.Nightshade:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Nightshade_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Nightshade_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Nightshade_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Nightshade_FullGrown;
          }
          break;
        case CROPTYPE.Hops:
          switch (plantstate)
          {
            case PlantState.Harvested:
              return TILETYPE.None;
            case PlantState.Dead:
              return TILETYPE.Hops_Wiltered;
            case PlantState.Seedling:
              return TILETYPE.Hops_SmallGrown;
            case PlantState.YoungPlant:
              return TILETYPE.Hops_HalfGrown;
            case PlantState.Fruited:
              return TILETYPE.Hops_FullGrown;
          }
          break;
      }
      return TILETYPE.Corn_FullGrown;
    }

    public static List<CROPTYPE> GetDisplayListOfCROPs()
    {
      if (CropData.displayCropList == null)
      {
        CropData.displayCropList = new List<CROPTYPE>();
        CropData.displayCropList.Add(CROPTYPE.Corn);
        CropData.displayCropList.Add(CROPTYPE.Carrots);
        CropData.displayCropList.Add(CROPTYPE.Cabbage);
        CropData.displayCropList.Add(CROPTYPE.Wheat);
        CropData.displayCropList.Add(CROPTYPE.WaterMelon);
        CropData.displayCropList.Add(CROPTYPE.Potato);
        CropData.displayCropList.Add(CROPTYPE.Grass);
        CropData.displayCropList.Add(CROPTYPE.Lettuce);
        CropData.displayCropList.Add(CROPTYPE.Soybeans);
        CropData.displayCropList.Add(CROPTYPE.CoffeeBerries);
        CropData.displayCropList.Add(CROPTYPE.Sugarcane);
        CropData.displayCropList.Add(CROPTYPE.Raspberry);
        CropData.displayCropList.Add(CROPTYPE.Beetroot);
        CropData.displayCropList.Add(CROPTYPE.Bamboo);
        CropData.displayCropList.Add(CROPTYPE.Nightshade);
        CropData.displayCropList.Add(CROPTYPE.Hops);
      }
      return CropData.displayCropList;
    }
  }
}
