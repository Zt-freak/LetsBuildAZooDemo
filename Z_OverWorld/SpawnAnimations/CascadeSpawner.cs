// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.SpawnAnimations.CascadeSpawner
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Trailer;

namespace TinyZoo.Z_OverWorld.SpawnAnimations
{
  internal class CascadeSpawner
  {
    private static List<TILETYPE> UnderFloor;

    internal static void SetUpFloorForCascade(List<Vector2Int> FloorVects, Player player)
    {
      CascadeSpawner.UnderFloor = new List<TILETYPE>();
      for (int index = 0; index < FloorVects.Count; ++index)
        CascadeSpawner.UnderFloor.Add(player.prisonlayout.layout.FloorTileTypes[FloorVects[index].X, FloorVects[index].Y].tiletype);
    }

    internal static void DoCascadeSpawnForPen(
      PrisonZone prisonzone,
      Player player,
      bool SpecialTrailerSpawn = false)
    {
      int num1 = 10000;
      int num2 = -1;
      for (int index = 0; index < prisonzone.FenceTiles.Count; ++index)
      {
        if (prisonzone.FenceTiles[index].X < num1)
          num1 = prisonzone.FenceTiles[index].X;
        if (prisonzone.FenceTiles[index].X > num2)
          num2 = prisonzone.FenceTiles[index].X;
      }
      for (int index1 = num1; index1 < num2 + 1; ++index1)
      {
        float Delay = (float) (index1 - num1) * 0.1f;
        for (int index2 = 0; index2 < prisonzone.FenceTiles.Count; ++index2)
        {
          if (prisonzone.FenceTiles[index2] != null && prisonzone.FenceTiles[index2].X == index1)
          {
            if (SpecialTrailerSpawn)
            {
              OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[prisonzone.FenceTiles[index2].X, prisonzone.FenceTiles[index2].Y].RemoveComponentByType(RenderComponentType.SpawnAnimBuilding);
              OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[prisonzone.FenceTiles[index2].X, prisonzone.FenceTiles[index2].Y].spawnblocker = (SpawnBlockComponent) null;
            }
            OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[prisonzone.FenceTiles[index2].X, prisonzone.FenceTiles[index2].Y].DoPenSpawnAnim(Delay, TILETYPE.Count, prisonzone.FenceTiles[index2].CompareMatches(prisonzone.GetGateLocation()));
          }
        }
        for (int index2 = 0; index2 < prisonzone.FloorTiles.Count; ++index2)
        {
          if (prisonzone.FloorTiles[index2].X == index1)
          {
            if (SpecialTrailerSpawn)
            {
              OverWorldManager.overworldenvironment.wallsandfloors.FloorTilesArray[prisonzone.FloorTiles[index2].X, prisonzone.FloorTiles[index2].Y].spawnblocker = (SpawnBlockComponent) null;
              OverWorldManager.overworldenvironment.wallsandfloors.FloorTilesArray[prisonzone.FloorTiles[index2].X, prisonzone.FloorTiles[index2].Y].DoPenSpawnAnim(Delay, TILETYPE.EMPTY_DIRT_WALKABLE_TILE, false);
            }
            else
              OverWorldManager.overworldenvironment.wallsandfloors.FloorTilesArray[prisonzone.FloorTiles[index2].X, prisonzone.FloorTiles[index2].Y].DoPenSpawnAnim(Delay, CascadeSpawner.UnderFloor[index2], false);
            OverWorldManager.overworldenvironment.wallsandfloors.UpdatableFloors.Add(OverWorldManager.overworldenvironment.wallsandfloors.FloorTilesArray[prisonzone.FloorTiles[index2].X, prisonzone.FloorTiles[index2].Y]);
          }
        }
      }
    }

    internal static void DoCascadeForBuildingorTree(TileRenderer tilerenderer)
    {
      if (CategoryData.IsThisANatireDeocration(tilerenderer.tiletypeonconstruct))
        tilerenderer.DoNatureSpawnAnimation();
      else
        tilerenderer.DoBuildingSpawnAnimation();
    }

    internal static void DoCascadeForBuilding(
      TILETYPE tiletype,
      Vector2Int Location,
      Player player)
    {
    }
  }
}
