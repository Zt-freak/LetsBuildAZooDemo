// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.VolumeBuilding.VolumeRow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.WaterBuild;

namespace TinyZoo.Z_BuldMenu.VolumeBuilding
{
  internal class VolumeRow
  {
    public List<TileRenderContainer> tiles;
    public int YROW;
    private int MinX;
    private int MaxX;

    public VolumeRow(int _YROW)
    {
      this.YROW = _YROW;
      this.tiles = new List<TileRenderContainer>();
    }

    public bool HasThisHere(TILETYPE tile, Vector2Int position) => position.X - this.MinX > -1 && position.X - this.MinX < this.tiles.Count && this.tiles[position.X - this.MinX].Locked;

    public void UnSetTile_AndUnblockSurroundersForOtherTilesONTHISROW(
      Vector2Int location,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      VolumeMaker PARENT_volumemaker,
      int RowNumber,
      ref bool NewFloorTileIsAVolume,
      bool IsUnderFloorUnset = false)
    {
      if (this.tiles.Count <= 0 || location.X - this.MinX <= -1 || location.X - this.MinX >= this.tiles.Count)
        return;
      if (RowNumber == 0)
        this.tiles[location.X - this.MinX].UnSetTile(wallsandfloors, player, ref NewFloorTileIsAVolume, IsUnderFloorUnset);
      Vector2Int vector2Int = new Vector2Int();
      for (int index = -1; index < 2; ++index)
      {
        int num = location.X + index;
        if (num - this.MinX > -1 && num - this.MinX < this.tiles.Count && (index != 0 || RowNumber != 0) && this.tiles[num - this.MinX].Locked)
        {
          int Y = 1;
          switch (RowNumber)
          {
            case -1:
              Y = 2;
              break;
            case 1:
              Y = 0;
              break;
          }
          SurroundingInformation surroundClass = this.tiles[num - this.MinX].GetSurroundClass(false);
          switch (index)
          {
            case -1:
              surroundClass.UnBlockThis(2, Y);
              break;
            case 0:
              surroundClass.UnBlockThis(1, Y);
              break;
            default:
              surroundClass.UnBlockThis(0, Y);
              break;
          }
          vector2Int.X = location.X + index;
          vector2Int.Y = location.Y;
        }
      }
    }

    public void SetSurroudIncludingOriginalMapping(
      Vector2Int location,
      TILETYPE tiletype,
      Player player,
      bool IsFloor,
      bool SetSelf = false,
      bool ForceAllFalse = false)
    {
      this.tiles[location.X - this.MinX].SetSurroudIncludingOriginalMapping(player, tiletype, location, IsFloor, SetSelf, ForceAllFalse);
    }

    public SurroundingInformation CommitTile_AndBlockRow_SELF(
      Vector2Int location)
    {
      SurroundingInformation surroundingInformation = this.tiles[location.X - this.MinX].CommitTile();
      for (int index = -1; index < 2; ++index)
      {
        int num = location.X + index;
        if (num - this.MinX > -1 && num - this.MinX < this.tiles.Count && (index != 0 && this.tiles[num - this.MinX].Locked))
          surroundingInformation.BlockThis(index + 1, 1);
      }
      return surroundingInformation;
    }

    public void UnblockSurround_AroundThisTile(int Xlocation, bool CheckingFromRowAbove)
    {
      for (int index = -1; index < 2; ++index)
      {
        int num = Xlocation + index;
        if (num - this.MinX > -1 && num - this.MinX < this.tiles.Count && this.tiles[num - this.MinX].Locked)
        {
          int Y = 0;
          int X = 1;
          switch (index)
          {
            case -1:
              X = 2;
              break;
            case 1:
              X = 0;
              break;
          }
          if (!CheckingFromRowAbove)
            Y = 2;
          this.tiles[num - this.MinX].GetSurroundClass(false).UnBlockThis(X, Y);
        }
      }
    }

    public void CheckSurround_ArundThisTile(
      int Xlocation,
      bool CheckingFromRowAbove,
      SurroundingInformation surrounder)
    {
      for (int index = -1; index < 2; ++index)
      {
        int num = Xlocation + index;
        if (num - this.MinX > -1 && num - this.MinX < this.tiles.Count && this.tiles[num - this.MinX].Locked)
        {
          if (CheckingFromRowAbove)
            surrounder.BlockThis(index + 1, 0);
          else
            surrounder.BlockThis(index + 1, 2);
        }
      }
    }

    public void SetTile(Vector2Int location, TILETYPE tiletype, Player player)
    {
      if (this.tiles.Count == 0)
      {
        this.MinX = location.X;
        this.MaxX = location.X;
        this.tiles.Add(new TileRenderContainer(location.X, location.Y));
      }
      if (location.X < this.MinX)
      {
        while (this.MinX > location.X)
        {
          --this.MinX;
          this.tiles.Insert(0, new TileRenderContainer(this.MinX, location.Y));
        }
      }
      else if (location.X > this.MaxX)
      {
        while (this.MaxX < location.X)
        {
          ++this.MaxX;
          this.tiles.Add(new TileRenderContainer(this.MaxX, location.Y));
        }
      }
      else
      {
        int x = location.X;
        int minX = this.MinX;
      }
      this.tiles[location.X - this.MinX].SetTile(tiletype, location, player);
    }

    public void ForceLocked(Vector2Int location)
    {
      if (location.X - this.MinX <= -1 || location.X - this.MinX >= this.tiles.Count)
        return;
      this.tiles[location.X - this.MinX].ForceLocked();
    }

    public void BlockSurroundingTile(int TileXToBlock, int RelativeX, int RelativeY)
    {
      if (TileXToBlock - this.MinX <= -1 || TileXToBlock - this.MinX >= this.tiles.Count)
        return;
      this.tiles[TileXToBlock - this.MinX].BlockSurroundingTile(RelativeX, RelativeY);
    }

    public SurroundingInformation GetSurroundClass(
      int XLocation,
      bool IsDeleting)
    {
      return XLocation - this.MinX > -1 && XLocation - this.MinX < this.tiles.Count ? this.tiles[XLocation - this.MinX].GetSurroundClass(IsDeleting) : (SurroundingInformation) null;
    }

    public void SetRenderTileFromSurround(
      int XLocation,
      WallsAndFloorsManager wallsandfloors,
      Player player,
      TILETYPE tiletype,
      bool IsFloor)
    {
      if (XLocation - this.MinX <= -1 || XLocation - this.MinX >= this.tiles.Count)
        return;
      this.tiles[XLocation - this.MinX].SetSurroudIncludingOriginalMapping(player, tiletype, new Vector2Int(XLocation, this.YROW), IsFloor);
      this.tiles[XLocation - this.MinX].SetRenderTileFromSurround(wallsandfloors, player);
    }

    public void SetSurround(
      bool ThisIsCenterRow,
      int XLocation,
      SurroundingInformation surounder,
      int VerticalPosition)
    {
      if (surounder == null)
        return;
      for (int index1 = -1; index1 < 2; ++index1)
      {
        int index2 = XLocation + index1 - this.MinX;
        if (index1 == 0 & ThisIsCenterRow)
          surounder.BlockThis(1, 1);
        else if (index2 > -1 && index2 < this.tiles.Count && this.tiles[index2].Locked)
          surounder.BlockThis(index1 + 1, VerticalPosition + 1);
      }
    }

    public void DrawVolumeRow()
    {
      for (int index = 0; index < this.tiles.Count; ++index)
        this.tiles[index].DrawTileRenderContainer();
    }
  }
}
