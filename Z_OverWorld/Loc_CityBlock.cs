// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Loc_CityBlock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;
using TinyZoo.Z_OverWorld.Location_Directory;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_OverWorld
{
  internal class Loc_CityBlock
  {
    private List<ImportantPlace> l_Bins;
    private List<ImportantPlace> l_DrinksStores;
    private List<ImportantPlace> l_FoodStores;
    private List<ImportantPlace> l_Toilets;
    private List<ImportantPlace> l_Benchs;
    private List<ImportantPlace> l_CoolingFoods;
    private List<ImportantPlace> l_ATMs;
    private List<ImportantPlace> l_AnimalWatchingPoints;
    private List<ImportantPlace> l_StoreRoomOrCupboard;
    private HashSet<POINT_OF_INTEREST> TypesOfThingHere;
    private DirTileContents[,] tiles;
    private Vector2Int TopLeft;

    public Loc_CityBlock(int BlockSize, Vector2Int _TopLeft)
    {
      this.TopLeft = _TopLeft;
      this.tiles = new DirTileContents[BlockSize, BlockSize];
      this.TypesOfThingHere = new HashSet<POINT_OF_INTEREST>();
      this.l_Bins = new List<ImportantPlace>();
      this.l_DrinksStores = new List<ImportantPlace>();
      this.l_FoodStores = new List<ImportantPlace>();
      this.l_Toilets = new List<ImportantPlace>();
      this.l_Benchs = new List<ImportantPlace>();
      this.l_CoolingFoods = new List<ImportantPlace>();
      this.l_ATMs = new List<ImportantPlace>();
      this.l_AnimalWatchingPoints = new List<ImportantPlace>();
      this.l_StoreRoomOrCupboard = new List<ImportantPlace>();
    }

    public bool HasOneOfThese(POINT_OF_INTEREST pointofinterest) => this.TypesOfThingHere.Contains(pointofinterest);

    public bool TryAndGetToThis(POINT_OF_INTEREST pointofinterest, PathNavigator pathnavigator)
    {
      switch (pointofinterest)
      {
        case POINT_OF_INTEREST.Bin:
          for (int index = 0; index < this.l_Bins.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_Bins[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.DrinksStore:
          for (int index = 0; index < this.l_DrinksStores.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_DrinksStores[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.FoodStore:
          for (int index = 0; index < this.l_FoodStores.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_FoodStores[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.Toilet:
          for (int index = 0; index < this.l_Toilets.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_Toilets[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.Bench:
          for (int index = 0; index < this.l_Benchs.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_Benchs[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.CoolingFood:
          for (int index = 0; index < this.l_CoolingFoods.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_CoolingFoods[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.ATM:
          for (int index = 0; index < this.l_ATMs.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_ATMs[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.AnimalWatchingPoint:
          for (int index = 0; index < this.l_AnimalWatchingPoints.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_AnimalWatchingPoints[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        case POINT_OF_INTEREST.StoreRoomOrCupboard:
          for (int index = 0; index < this.l_StoreRoomOrCupboard.Count; ++index)
          {
            if (pathnavigator.TryToGoHere(this.l_StoreRoomOrCupboard[index].Location, GameFlags.pathset))
              return true;
          }
          return false;
        default:
          throw new Exception("ihsdf");
      }
    }

    public void AddTrash(TrashDrop trashdrop)
    {
      this.CheckNull(trashdrop.TileLocation);
      this.tiles[trashdrop.TileLocation.X - this.TopLeft.X, trashdrop.TileLocation.Y - this.TopLeft.Y].AddTrash(trashdrop);
    }

    public void AddPoop(TrashDrop trashdrop)
    {
      this.CheckNull(trashdrop.TileLocation);
      this.tiles[trashdrop.TileLocation.X - this.TopLeft.X, trashdrop.TileLocation.Y - this.TopLeft.Y].AddPoop(trashdrop);
    }

    public void RemovePoop(TrashDrop trashdrop)
    {
      this.CheckNull(trashdrop.TileLocation);
      this.tiles[trashdrop.TileLocation.X - this.TopLeft.X, trashdrop.TileLocation.Y - this.TopLeft.Y].RemoveTrash(trashdrop);
    }

    public void RemoveTrash(TrashDrop trashdrop)
    {
      this.CheckNull(trashdrop.TileLocation);
      this.tiles[trashdrop.TileLocation.X - this.TopLeft.X, trashdrop.TileLocation.Y - this.TopLeft.Y].RemoveTrash(trashdrop);
    }

    private void CheckNull(Vector2Int Loc)
    {
      if (this.tiles[Loc.X - this.TopLeft.X, Loc.Y - this.TopLeft.Y] != null)
        return;
      this.tiles[Loc.X - this.TopLeft.X, Loc.Y - this.TopLeft.Y] = new DirTileContents();
    }

    public CURSOR_ACTIONTYPE GetCursorThing(int LocationX, int LocationY)
    {
      LocationX -= this.TopLeft.X;
      LocationY -= this.TopLeft.Y;
      return LocationX >= 0 && LocationY >= 0 && (LocationX < this.tiles.GetLength(0) && LocationY < this.tiles.GetLength(1)) && this.tiles[LocationX, LocationY] != null ? this.tiles[LocationX, LocationY].GetCursorThing() : CURSOR_ACTIONTYPE.Count;
    }

    public void RemoveNodeOfImportance(
      int LocationX,
      int LocationY,
      TILETYPE tiletype,
      Vector2Int OffsetToChild,
      ref int[] TotalPointsOfInterestByType)
    {
      if (TileData.IsAStoreRoom(tiletype) && Loc_CityBlock.TryRemove(ref this.l_StoreRoomOrCupboard, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[8];
        if (TotalPointsOfInterestByType[8] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.StoreRoomOrCupboard);
      }
      if (TileData.IsForFood(tiletype) && Loc_CityBlock.TryRemove(ref this.l_FoodStores, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[2];
        if (TotalPointsOfInterestByType[2] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.FoodStore);
      }
      if (TileData.IsForThirst(tiletype) && Loc_CityBlock.TryRemove(ref this.l_DrinksStores, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[1];
        if (TotalPointsOfInterestByType[1] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.DrinksStore);
      }
      if (TileData.IsThisAnATM(tiletype) && Loc_CityBlock.TryRemove(ref this.l_ATMs, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[6];
        if (TotalPointsOfInterestByType[6] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.ATM);
      }
      if (TileData.IsThisaToilet(tiletype) && Loc_CityBlock.TryRemove(ref this.l_Toilets, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[3];
        if (TotalPointsOfInterestByType[3] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.Toilet);
      }
      if (TileData.IsThisABin(tiletype) && Loc_CityBlock.TryRemove(ref this.l_Bins, LocationX, LocationY))
      {
        --TotalPointsOfInterestByType[0];
        if (TotalPointsOfInterestByType[0] <= 0)
          this.TypesOfThingHere.Remove(POINT_OF_INTEREST.Bin);
      }
      if (!TileData.IsThisABench(tiletype) || !Loc_CityBlock.TryRemove(ref this.l_Benchs, LocationX, LocationY))
        return;
      --TotalPointsOfInterestByType[4];
      if (TotalPointsOfInterestByType[4] > 0)
        return;
      this.TypesOfThingHere.Remove(POINT_OF_INTEREST.Bench);
    }

    private static bool TryRemove(ref List<ImportantPlace> imortantplaces, int LOCX, int LOCY)
    {
      for (int index = imortantplaces.Count - 1; index > -1; --index)
      {
        if (imortantplaces[index].Location.X == LOCX || imortantplaces[index].Location.X == LOCY)
        {
          imortantplaces.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public void AddNodeOfImportance(
      int LocationX,
      int LocationY,
      TILETYPE tiletype,
      Vector2Int OffsetToChild,
      ref int[] TotalPointsOfInterestByType)
    {
      ImportantPlace importantPlace = new ImportantPlace(LocationX, LocationY, tiletype);
      if (TileData.IsAStoreRoom(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.StoreRoomOrCupboard))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.StoreRoomOrCupboard);
        ++TotalPointsOfInterestByType[8];
        this.l_StoreRoomOrCupboard.Add(importantPlace);
      }
      if (TileData.IsThisAShopWithShopStats(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.FoodStore))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.FoodStore);
        ++TotalPointsOfInterestByType[2];
        this.l_FoodStores.Add(importantPlace);
      }
      if (TileData.IsForThirst(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.DrinksStore))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.DrinksStore);
        ++TotalPointsOfInterestByType[1];
        this.l_DrinksStores.Add(importantPlace);
      }
      if (TileData.IsThisAnATM(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.ATM))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.ATM);
        ++TotalPointsOfInterestByType[6];
        this.l_ATMs.Add(importantPlace);
      }
      if (TileData.IsThisaToilet(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.Toilet))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.Toilet);
        ++TotalPointsOfInterestByType[3];
        this.l_Toilets.Add(importantPlace);
      }
      if (TileData.IsThisABin(tiletype))
      {
        if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.Bin))
          this.TypesOfThingHere.Add(POINT_OF_INTEREST.Bin);
        ++TotalPointsOfInterestByType[0];
        this.l_Bins.Add(importantPlace);
      }
      if (!TileData.IsThisABench(tiletype))
        return;
      if (!this.TypesOfThingHere.Contains(POINT_OF_INTEREST.Bench))
        this.TypesOfThingHere.Add(POINT_OF_INTEREST.Bench);
      ++TotalPointsOfInterestByType[4];
      this.l_Benchs.Add(importantPlace);
    }
  }
}
