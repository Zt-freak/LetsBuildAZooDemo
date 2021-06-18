// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.EnrichmentData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.FileInOut;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Animal_Data
{
  internal class EnrichmentData
  {
    private static EnrichmentEntry[] enrichmententries;
    internal static HashSet<TILETYPE> AllEnrichmnetItems;

    internal static bool IsThisAnEnrcihmentItem(TILETYPE tiletype)
    {
      if (EnrichmentData.enrichmententries == null)
      {
        double num = (double) AnimalData.SetUp(AnimalType.Seal);
      }
      return EnrichmentData.AllEnrichmnetItems.Contains(tiletype);
    }

    internal static ENRICHMENTCLASS GetTILETYPEToEnrichmentClass(
      TILETYPE buildingtype)
    {
      if (EnrichmentData.enrichmententries == null)
      {
        double num = (double) AnimalData.SetUp(AnimalType.Seal);
      }
      for (int index = 0; index < EnrichmentData.enrichmententries.Length; ++index)
      {
        if (EnrichmentData.enrichmententries[index].TheseItems.Contains(buildingtype))
          return EnrichmentData.enrichmententries[index].enrichmentclass;
      }
      if (TileData.IsThisAWaterTrough(buildingtype))
        return ENRICHMENTCLASS.WaterTrough;
      if (TileData.IsThisAShelter(buildingtype) || TileData.IsThisAPenDecoration(buildingtype))
        return ENRICHMENTCLASS.Shelter;
      throw new Exception("TILETYPE IS NOT AN ENRICHMENT ITEM");
    }

    internal static bool IsThisAnAnimatedPerch(TILETYPE tiletype) => tiletype == TILETYPE.Enrichment_HighStriker;

    internal static EnrichmentEntry GetEnrichmentEntry(ENRICHMENTCLASS enrichment) => enrichment >= ENRICHMENTCLASS.Count ? (EnrichmentEntry) null : EnrichmentData.enrichmententries[(int) enrichment];

    internal static void SecondayLoadEnrichment()
    {
      if (EnrichmentData.enrichmententries != null)
        return;
      AnimalData.GetAnimalStat(AnimalType.Rabbit);
      CSVFileReader csvFileReader = new CSVFileReader();
      csvFileReader.Read("EnrichmentData.csv");
      EnrichmentData.enrichmententries = new EnrichmentEntry[22];
      for (int index = 0; index < EnrichmentData.enrichmententries.Length; ++index)
        EnrichmentData.enrichmententries[index] = new EnrichmentEntry((ENRICHMENTCLASS) index);
      for (int Row = 1; Row < csvFileReader.GetRowCount(); ++Row)
      {
        AnimalType stringToAnimal = AnimalData.GetStringToAnimal(csvFileReader.GetCell(Row, 0));
        for (int Column = 1; Column < 22; ++Column)
        {
          string cell = csvFileReader.GetCell(Row, Column);
          if (cell.Length > 0)
          {
            EnrichmentData.enrichmententries[Column - 1].AddAnimal(stringToAnimal, Convert.ToInt32(cell));
            AnimalData.GetAnimalStat(stringToAnimal).AddEnrichment((ENRICHMENTCLASS) (Column - 1));
          }
        }
      }
    }

    internal static ENRICHMENTCLASS GetStringToENRICHMENTCLASS(string Text)
    {
      switch (Text)
      {
        case "BoneToy":
          return ENRICHMENTCLASS.BoneToy;
        case "CarTire":
          return ENRICHMENTCLASS.CarTire;
        case "CardboardBoxes":
          return ENRICHMENTCLASS.CardboardBoxes;
        case "Hats":
          return ENRICHMENTCLASS.Hats;
        case "HighWoodBeamPerch":
          return ENRICHMENTCLASS.HighWoodBeamPerch;
        case "LargeBall":
          return ENRICHMENTCLASS.LargeBall;
        case "Mirror":
          return ENRICHMENTCLASS.Mirror;
        case "Mud":
          return ENRICHMENTCLASS.Mud;
        case "Pond":
          return ENRICHMENTCLASS.Pond;
        case "RockCliff":
          return ENRICHMENTCLASS.RockCliff_Perch;
        case "RockPerch":
          return ENRICHMENTCLASS.RockPerch;
        case "RopeToy":
          return ENRICHMENTCLASS.RopeToy;
        case "SaltBlock":
          return ENRICHMENTCLASS.SaltBlock;
        case "ScentMarkers":
          return ENRICHMENTCLASS.ScentMarkers;
        case "SmallBall":
          return ENRICHMENTCLASS.SmallBall;
        case "TallGrassVegetation":
          return ENRICHMENTCLASS.TallGrassVegetation;
        case "Trampoline":
          return ENRICHMENTCLASS.Trampoline;
        case "TugBallJollyBall":
          return ENRICHMENTCLASS.TugBallJollyBall;
        case "Tunnel":
          return ENRICHMENTCLASS.Tunnel;
        case "WaterSprinklers":
          return ENRICHMENTCLASS.WaterSprinklers;
        case "WoodenLogs":
          return ENRICHMENTCLASS.WoodenLogs;
        case "WoodenPost":
          return ENRICHMENTCLASS.WoodenPost;
        default:
          throw new Exception("MISSED IT");
      }
    }
  }
}
