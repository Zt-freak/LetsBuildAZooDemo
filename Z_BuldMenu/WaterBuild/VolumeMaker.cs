// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.WaterBuild.VolumeMaker
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.VolumeBuilding;

namespace TinyZoo.Z_BuldMenu.WaterBuild
{
  internal class VolumeMaker
  {
    private List<VolumeRow> volumerows;
    private Vector2Int LastMouseLoc;
    private int MinY;
    private int MaxY;
    private TILETYPE tiletype;
    private VolumeMousePointer volumepointer;
    private bool IsCreatingFloor;
    public bool IsWater;
    public bool IsFloor = true;

    public VolumeMaker(TILETYPE tile)
    {
      this.IsWater = TileData.IsThisWater(tile);
      this.IsCreatingFloor = TileData.IsThisFloorAVolume(tile);
      this.volumerows = new List<VolumeRow>();
      this.tiletype = tile;
      this.volumepointer = new VolumeMousePointer(tile);
    }

    public bool HasThisHere(TILETYPE tile, Vector2Int position) => position.Y - this.MinY > -1 && position.Y - this.MinY < this.volumerows.Count && this.volumerows[position.Y - this.MinY].HasThisHere(tile, position);

    private void AddRowsIfNeedeIncludeSurrounding_AndLockIfExists(
      Vector2Int Location,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      bool IsDelete)
    {
      Vector2Int vector2Int = new Vector2Int(Location);
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          vector2Int.X = Location.X + index1;
          vector2Int.Y = Location.Y + index2;
          this.AddRowsIfNeeded(vector2Int, wallsandfloors, player, IsDelete);
          if (player.prisonlayout.IsThisAlreadyHere(this.tiletype, vector2Int, this.IsFloor))
          {
            this.volumerows[vector2Int.Y - this.MinY].SetSurroudIncludingOriginalMapping(vector2Int, this.tiletype, player, this.IsFloor, true);
            this.volumerows[vector2Int.Y - this.MinY].ForceLocked(vector2Int);
          }
        }
      }
    }

    private void AddRowsIfNeeded(
      Vector2Int Location,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      bool IsDelete)
    {
      if (IsDelete && (Location.X <= 0 || Location.X >= player.prisonlayout.layout.FloorTileTypes.GetLength(0) || (Location.Y <= 0 || Location.Y >= player.prisonlayout.layout.FloorTileTypes.GetLength(1)) || player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype != this.tiletype))
        return;
      if (this.volumerows.Count == 0)
      {
        this.volumerows.Add(new VolumeRow(Location.Y));
        this.MinY = Location.Y;
        this.MaxY = this.MinY;
      }
      else
      {
        if (Location.Y < this.MinY)
        {
          while (this.MinY > Location.Y)
          {
            --this.MinY;
            this.volumerows.Insert(0, new VolumeRow(this.MinY));
          }
        }
        if (Location.Y > this.MaxY)
        {
          while (this.MaxY < Location.Y)
          {
            ++this.MaxY;
            this.volumerows.Add(new VolumeRow(this.MaxY));
          }
        }
        else
        {
          int y = Location.Y;
          int minY = this.MinY;
        }
      }
      Vector2Int lastMouseLoc = this.LastMouseLoc;
      this.volumerows[Location.Y - this.MinY].SetTile(Location, this.tiletype, player);
    }

    private void CheckAroundThisForExistingMatches(
      Vector2Int location,
      Player player,
      WallsAndFloorsManager wallsandfloors,
      bool IsRecursive)
    {
      Vector2Int vector2Int = new Vector2Int();
      if (false)
      {
        bool flag1 = true;
        int num = 0;
        bool flag2 = true;
        bool flag3 = true;
        while (flag1)
        {
          ++num;
          if (flag3)
          {
            if (location.X - num > 0)
            {
              vector2Int.X = location.X - num;
              vector2Int.Y = location.Y;
            }
            else
              flag3 = false;
          }
          if (flag2)
          {
            if (location.X + num < player.prisonlayout.layout.BaseTileTypes.GetLength(0))
            {
              vector2Int.X = location.X + num;
              vector2Int.Y = location.Y;
            }
            else
              flag2 = false;
          }
        }
      }
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          if ((index1 != 0 || index2 != 0) && (location.X + index1 > -1 && location.X + index1 < player.prisonlayout.layout.BaseTileTypes.GetLength(0)) && (location.Y + index2 > -1 && location.Y + index2 < player.prisonlayout.layout.BaseTileTypes.GetLength(1)))
          {
            vector2Int.X = location.X + index1;
            vector2Int.Y = location.Y + index2;
            if (!this.HasThisHere(this.tiletype, vector2Int))
            {
              if (this.IsCreatingFloor)
              {
                if (player.prisonlayout.layout.FloorTileTypes[location.X + index1, location.Y + index2].tiletype == this.tiletype)
                {
                  this.AddRowsIfNeeded(vector2Int, wallsandfloors, player, false);
                  if (!IsRecursive)
                  {
                    this.CheckExistingBlocksFromBaseMap(vector2Int, wallsandfloors, player);
                    this.CommitAndBlock(vector2Int, wallsandfloors, player);
                  }
                }
              }
              else if (player.prisonlayout.layout.BaseTileTypes[location.X + index1, location.Y + index2].tiletype == this.tiletype)
              {
                this.AddRowsIfNeeded(vector2Int, wallsandfloors, player, false);
                if (!IsRecursive)
                {
                  this.CheckAroundThisForExistingMatches(vector2Int, player, wallsandfloors, true);
                  this.CommitAndBlock(vector2Int, wallsandfloors, player);
                }
                else
                  this.CommitAndBlock(vector2Int, wallsandfloors, player);
                this.CascadeAndFindAllConnectedTiles(vector2Int, player, this.tiletype);
              }
            }
          }
        }
      }
    }

    private bool CascadeAndFindAllConnectedTiles(
      Vector2Int location,
      Player player,
      TILETYPE tiletype)
    {
      return false;
    }

    public void SetNewMouseLocation(
      Vector2Int mouseloc,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      this.volumepointer.SetNewMousePosition(mouseloc, this.IsWater, player, wallsandfloors);
      this.LastMouseLoc = new Vector2Int(mouseloc);
    }

    public bool GetIsBlocked() => this.volumepointer.GetIsBlocked();

    public void ApplyEdging(
      Vector2Int Location,
      bool IsDeleting,
      WallsAndFloorsManager wallsandfloors,
      Player player)
    {
      this.AddRowsIfNeeded(Location, wallsandfloors, player, IsDeleting);
      SurroundingInformation surroundClass = this.volumerows[Location.Y - this.MinY].GetSurroundClass(Location.X, IsDeleting);
      if (surroundClass == null & IsDeleting)
        return;
      if (Location.Y - (this.MinY + 1) > 0)
        this.volumerows[Location.Y - (this.MinY + 1)].SetSurround(false, Location.X, surroundClass, -1);
      this.volumerows[Location.Y - this.MinY].SetSurround(true, Location.X, surroundClass, 0);
      int num = Location.Y - (this.MinY - 1);
      int maxY = this.MaxY;
      surroundClass.ApplyRenderingToSelfAndAllAround(this.volumerows, Location, this.MinY, wallsandfloors, player, false, this.tiletype, this.IsFloor);
    }

    public void FixThisTile(
      Player player,
      Vector2Int Location,
      WallsAndFloorsManager wallsandfloors,
      bool IsFloor)
    {
      SurroundingInformation surroundingInformation = new SurroundingInformation();
      TILETYPE tiletype = player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype;
      surroundingInformation.SimpleSetUpFromPlayer(Location, player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype, player, IsFloor);
      surroundingInformation.ApplyRenderingToSelf(wallsandfloors, player, IsFloor, Location);
      Vector2Int vector2Int = new Vector2Int();
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          int index3 = Location.X + index1;
          int index4 = Location.Y + index2;
          if (index3 > -1 && index4 > -1 && (index3 < player.prisonlayout.layout.FloorTileTypes.GetLength(0) && index4 < player.prisonlayout.layout.FloorTileTypes.GetLength(1)) && player.prisonlayout.layout.FloorTileTypes[index3, index4].tiletype == tiletype)
          {
            vector2Int.X = index3;
            vector2Int.Y = index4;
            surroundingInformation.SimpleSetUpFromPlayer(vector2Int, player.prisonlayout.layout.FloorTileTypes[index3, index4].tiletype, player, IsFloor);
            surroundingInformation.ApplyRenderingToSelf(wallsandfloors, player, IsFloor, vector2Int);
          }
        }
      }
    }

    public void CommitTile(
      bool IsDeleting,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      Vector2Int Location,
      out bool NewFloorTileIsAVolume_ONDELETE)
    {
      NewFloorTileIsAVolume_ONDELETE = false;
      if (this.IsWater)
      {
        if (IsDeleting)
        {
          Z_GameFlags.pathfinder.UnblockTile(Location.X, Location.Y);
          Z_GameFlags.pathfinder.BlockWaterTile(Location.X, Location.Y);
          GameFlags.CollisionChanged = true;
        }
        else
        {
          Z_GameFlags.pathfinder.BlockTile(Location.X, Location.Y);
          Z_GameFlags.pathfinder.UnblockWaterTile(Location.X, Location.Y);
          GameFlags.CollisionChanged = true;
        }
      }
      if (!this.IsWater)
        TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype);
      if (IsDeleting)
      {
        this.AddRowsIfNeedeIncludeSurrounding_AndLockIfExists(Location, wallsandfloors, player, true);
        if (Location.Y - this.MinY <= -1 || Location.Y - this.MinY >= this.volumerows.Count)
          return;
        bool flag = false;
        bool IsUnderFloorUnset = false;
        if (!this.IsWater && TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[Location.X, Location.Y].tiletype))
        {
          flag = true;
          if (this.volumerows[Location.Y - this.MinY].HasThisHere(this.tiletype, Location))
          {
            flag = false;
            IsUnderFloorUnset = true;
          }
        }
        if (flag)
          return;
        Vector2Int vector2Int = new Vector2Int(Location);
        for (int index1 = 0; index1 < 1; ++index1)
        {
          for (int index2 = 0; index2 < 1; ++index2)
          {
            vector2Int.X = Location.X + index1;
            vector2Int.Y = Location.Y + index2;
            if (player.prisonlayout.IsThisAlreadyHere(this.tiletype, vector2Int, this.IsFloor))
            {
              int index3 = Location.Y - index2 + index2 - this.MinY;
              if (index3 > -1 && index3 < this.volumerows.Count)
              {
                vector2Int.X = Location.X;
                vector2Int.Y = Location.Y;
                vector2Int.X += index1;
                vector2Int.Y += index2;
                if (!this.volumerows[index3].HasThisHere(this.tiletype, vector2Int))
                {
                  this.volumerows[index3].SetTile(vector2Int, this.tiletype, player);
                  this.volumerows[index3].ForceLocked(vector2Int);
                  this.volumerows[index3].SetSurroudIncludingOriginalMapping(vector2Int, this.tiletype, player, this.IsFloor, true);
                }
              }
            }
          }
        }
        if (!this.volumerows[Location.Y - this.MinY].HasThisHere(this.tiletype, Location))
          return;
        this.volumerows[Location.Y - this.MinY].UnSetTile_AndUnblockSurroundersForOtherTilesONTHISROW(Location, wallsandfloors, player, this, 0, ref NewFloorTileIsAVolume_ONDELETE, IsUnderFloorUnset);
        if (Location.Y - 1 - this.MinY > -1)
          this.volumerows[Location.Y - 1 - this.MinY].UnSetTile_AndUnblockSurroundersForOtherTilesONTHISROW(Location, wallsandfloors, player, this, -1, ref NewFloorTileIsAVolume_ONDELETE);
        if (Location.Y + 1 - this.MinY < this.volumerows.Count)
          this.volumerows[Location.Y + 1 - this.MinY].UnSetTile_AndUnblockSurroundersForOtherTilesONTHISROW(Location, wallsandfloors, player, this, 1, ref NewFloorTileIsAVolume_ONDELETE);
        this.volumerows[Location.Y - this.MinY].GetSurroundClass(Location.X, false)?.ApplyRenderingToSelfAndAllAround(this.volumerows, Location, this.MinY, wallsandfloors, player, true, this.tiletype, this.IsFloor);
      }
      else
      {
        this.CheckAroundThisForExistingMatches(Location, player, wallsandfloors, false);
        this.AddRowsIfNeeded(Location, wallsandfloors, player, false);
        this.CommitAndBlock(Location, wallsandfloors, player);
        this.ApplyEdging(Location, IsDeleting, wallsandfloors, player);
      }
    }

    private void CheckExistingBlocksFromBaseMap(
      Vector2Int Location,
      WallsAndFloorsManager wallsandfloors,
      Player player)
    {
      this.volumerows[Location.Y - this.MinY].SetSurroudIncludingOriginalMapping(Location, this.tiletype, player, this.IsFloor);
    }

    private void CommitAndBlock(
      Vector2Int Location,
      WallsAndFloorsManager wallsandfloors,
      Player player)
    {
      if (this.IsWater)
        Z_GameFlags.pathfinder.BlockTile(Location.X, Location.Y);
      this.volumerows[Location.Y - this.MinY].SetSurroudIncludingOriginalMapping(Location, this.tiletype, player, this.IsFloor, ForceAllFalse: true);
      SurroundingInformation surrounder = this.volumerows[Location.Y - this.MinY].CommitTile_AndBlockRow_SELF(Location);
      if (Location.Y - 1 - this.MinY > -1)
        this.volumerows[Location.Y - 1 - this.MinY].CheckSurround_ArundThisTile(Location.X, true, surrounder);
      if (Location.Y + 1 - this.MinY < this.volumerows.Count)
        this.volumerows[Location.Y + 1 - this.MinY].CheckSurround_ArundThisTile(Location.X, false, surrounder);
      surrounder.ApplyRenderingToSelfAndAllAround(this.volumerows, Location, this.MinY, wallsandfloors, player, false, this.tiletype, this.IsFloor);
    }

    public void DrawVolumeMaker() => this.volumepointer.DrawMousePointer();
  }
}
