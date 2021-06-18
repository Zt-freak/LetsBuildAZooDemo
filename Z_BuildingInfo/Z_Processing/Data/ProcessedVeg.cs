// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuildingInfo.Z_Processing.Data.ProcessedVeg
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_BuildingInfo.Z_Processing.Data
{
  internal class ProcessedVeg
  {
    private static List<AnimalFoodType> vegList;
    private static AnimalProductionList[] vegProductionList;

    internal static List<AnimalFoodType> GetDisplayListOfVegProcessorOutput()
    {
      if (ProcessedVeg.vegList == null)
      {
        ProcessedVeg.vegList = new List<AnimalFoodType>();
        ProcessedVeg.vegList.Add(AnimalFoodType.Corn);
        ProcessedVeg.vegList.Add(AnimalFoodType.Soybean);
        ProcessedVeg.vegList.Add(AnimalFoodType.Carrots);
        ProcessedVeg.vegList.Add(AnimalFoodType.Potato);
        ProcessedVeg.vegList.Add(AnimalFoodType.Beetroot);
        ProcessedVeg.vegList.Add(AnimalFoodType.WaterMelon);
        ProcessedVeg.vegList.Add(AnimalFoodType.SugarCubes);
        ProcessedVeg.vegList.Add(AnimalFoodType.CoffeeBerries);
        ProcessedVeg.vegList.Add(AnimalFoodType.Hops);
        ProcessedVeg.vegList.Add(AnimalFoodType.Lettuce);
        ProcessedVeg.vegList.Add(AnimalFoodType.Grains);
        ProcessedVeg.vegList.Add(AnimalFoodType.Berries);
        ProcessedVeg.vegList.Add(AnimalFoodType.Grass);
        ProcessedVeg.vegList.Add(AnimalFoodType.Greens);
        ProcessedVeg.vegList.Add(AnimalFoodType.NaturalJuiceFromProcessor);
        ProcessedVeg.vegList.Add(AnimalFoodType.NaturalColouringFromProcessor);
        ProcessedVeg.vegList.Add(AnimalFoodType.LuwakFromProcessor);
        ProcessedVeg.vegList.Add(AnimalFoodType.StarchFromProcessor);
        ProcessedVeg.vegList.Add(AnimalFoodType.PopcornFromProcessor);
        ProcessedVeg.vegList.Add(AnimalFoodType.Fructose);
        ProcessedVeg.vegList.Add(AnimalFoodType.ThornyShrubs);
        ProcessedVeg.vegList.Add(AnimalFoodType.Straw);
        ProcessedVeg.vegList.Add(AnimalFoodType.Hay);
        ProcessedVeg.vegList.Add(AnimalFoodType.Fruit);
        ProcessedVeg.vegList.Add(AnimalFoodType.Seeds);
        ProcessedVeg.vegList.Add(AnimalFoodType.RootVegetables);
        ProcessedVeg.vegList.Add(AnimalFoodType.Branches);
        ProcessedVeg.vegList.Add(AnimalFoodType.Leaves);
        ProcessedVeg.vegList.Add(AnimalFoodType.Roots);
        ProcessedVeg.vegList.Add(AnimalFoodType.Plants);
      }
      return ProcessedVeg.vegList;
    }

    internal static int GetVegetableProcessingTimePerUnitInMinutes(CROPTYPE cropType) => 60;

    internal static AnimalProductionList GetVegetableToOutput(CROPTYPE cropType)
    {
      if (ProcessedVeg.vegProductionList == null)
        ProcessedVeg.vegProductionList = new AnimalProductionList[18];
      if (ProcessedVeg.vegProductionList[(int) cropType] == null)
      {
        switch (cropType)
        {
          case CROPTYPE.Corn:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[6]
            {
              AnimalFoodType.Corn,
              AnimalFoodType.PopcornFromProcessor,
              AnimalFoodType.StarchFromProcessor,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Tomatoes:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[1]
            {
              AnimalFoodType.Count
            });
            break;
          case CROPTYPE.Carrots:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Carrots,
              AnimalFoodType.RootVegetables,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves
            });
            break;
          case CROPTYPE.Cabbage:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Greens,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Wheat:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[3]
            {
              AnimalFoodType.Grains,
              AnimalFoodType.Straw,
              AnimalFoodType.Hay
            });
            break;
          case CROPTYPE.WaterMelon:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[6]
            {
              AnimalFoodType.WaterMelon,
              AnimalFoodType.Fruit,
              AnimalFoodType.Seeds,
              AnimalFoodType.Leaves,
              AnimalFoodType.Plants,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Potato:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.Potato,
              AnimalFoodType.StarchFromProcessor,
              AnimalFoodType.RootVegetables,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves
            });
            break;
          case CROPTYPE.Grass:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[1]
            {
              AnimalFoodType.Grass
            });
            break;
          case CROPTYPE.Lettuce:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.Lettuce,
              AnimalFoodType.Greens,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Soybeans:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.Soybean,
              AnimalFoodType.Seeds,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.CoffeeBerries:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[7]
            {
              AnimalFoodType.CoffeeBerries,
              AnimalFoodType.LuwakFromProcessor,
              AnimalFoodType.ThornyShrubs,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots,
              AnimalFoodType.Branches
            });
            break;
          case CROPTYPE.Sugarcane:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.SugarCubes,
              AnimalFoodType.Fructose,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Raspberry:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[7]
            {
              AnimalFoodType.Berries,
              AnimalFoodType.NaturalJuiceFromProcessor,
              AnimalFoodType.NaturalColouringFromProcessor,
              AnimalFoodType.ThornyShrubs,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Beetroot:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[5]
            {
              AnimalFoodType.Beetroot,
              AnimalFoodType.RootVegetables,
              AnimalFoodType.Greens,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves
            });
            break;
          case CROPTYPE.Bamboo:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Bamboo,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Nightshade:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Nightshade,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          case CROPTYPE.Hops:
            ProcessedVeg.vegProductionList[(int) cropType] = new AnimalProductionList(new AnimalFoodType[4]
            {
              AnimalFoodType.Hops,
              AnimalFoodType.Plants,
              AnimalFoodType.Leaves,
              AnimalFoodType.Roots
            });
            break;
          default:
            return new AnimalProductionList(new AnimalFoodType[1]
            {
              AnimalFoodType.Count
            });
        }
      }
      return ProcessedVeg.vegProductionList[(int) cropType];
    }
  }
}
