// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder.PerimeterBuilderHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder
{
  internal class PerimeterBuilderHelper
  {
    internal static void SetFlagsFromDirection(
      DirectionPressed direction,
      ref bool _HasLeft,
      ref bool _HasRight,
      ref bool _HasABove,
      ref bool _HasBelow)
    {
      switch (direction)
      {
        case DirectionPressed.Up:
          _HasABove = true;
          break;
        case DirectionPressed.Right:
          _HasRight = true;
          break;
        case DirectionPressed.Down:
          _HasBelow = true;
          break;
        case DirectionPressed.Left:
          _HasLeft = true;
          break;
      }
    }

    internal static bool ChechBuggyEdge(List<BuildTile> CommitedTiles, List<BuildTile> Uncommited)
    {
      if (CommitedTiles.Count <= 1 || Uncommited.Count <= 0 || !CommitedTiles[CommitedTiles.Count - 2].TileLocation.CompareMatches(Uncommited[0].TileLocation))
        return false;
      Uncommited[0].SetRed();
      return true;
    }

    internal static void SimpleSetCornerType(List<BuildTile> _CommitedBuildTiles, int ThisIndex)
    {
      DirectionPressed direction = ThisIndex <= 0 ? _CommitedBuildTiles[ThisIndex].TileLocation.GetDirectionToThis(_CommitedBuildTiles[_CommitedBuildTiles.Count - 1].TileLocation) : _CommitedBuildTiles[ThisIndex].TileLocation.GetDirectionToThis(_CommitedBuildTiles[ThisIndex - 1].TileLocation);
      bool _HasABove = false;
      bool _HasBelow = false;
      bool _HasLeft = false;
      bool _HasRight = false;
      PerimeterBuilderHelper.SetFlagsFromDirection(direction, ref _HasLeft, ref _HasRight, ref _HasABove, ref _HasBelow);
      PerimeterBuilderHelper.SetFlagsFromDirection(ThisIndex >= _CommitedBuildTiles.Count - 1 ? _CommitedBuildTiles[ThisIndex].TileLocation.GetDirectionToThis(_CommitedBuildTiles[0].TileLocation) : _CommitedBuildTiles[ThisIndex].TileLocation.GetDirectionToThis(_CommitedBuildTiles[ThisIndex + 1].TileLocation), ref _HasLeft, ref _HasRight, ref _HasABove, ref _HasBelow);
      CellCornerType cellCornerType;
      if (_HasLeft & _HasRight && !_HasABove && !_HasBelow)
        cellCornerType = CellCornerType.StraightLeftRight;
      else if (((_HasLeft ? 0 : (!_HasRight ? 1 : 0)) & (_HasABove ? 1 : 0) & (_HasBelow ? 1 : 0)) != 0)
        cellCornerType = CellCornerType.StraightUpDown;
      else if (((_HasLeft ? 0 : (!_HasRight ? 1 : 0)) & (_HasABove ? 1 : 0) & (_HasBelow ? 1 : 0)) != 0)
        cellCornerType = CellCornerType.StraightUpDown;
      else if (((!(!_HasLeft & _HasRight) ? 0 : (!_HasABove ? 1 : 0)) & (_HasBelow ? 1 : 0)) != 0)
        cellCornerType = CellCornerType.OuterCorner_TopLeft;
      else if (((!_HasLeft || _HasRight ? 0 : (!_HasABove ? 1 : 0)) & (_HasBelow ? 1 : 0)) != 0)
        cellCornerType = CellCornerType.OuterCorner_TopRight;
      else if (((!_HasLeft ? 0 : (!_HasRight ? 1 : 0)) & (_HasABove ? 1 : 0)) != 0 && !_HasBelow)
      {
        cellCornerType = CellCornerType.OuterCorner_BottomRight;
      }
      else
      {
        if (!(!_HasLeft & _HasRight & _HasABove) || _HasBelow)
          throw new Exception("This PenShape can not exist");
        cellCornerType = CellCornerType.OuterCorner_BottomLeft;
      }
      _CommitedBuildTiles[ThisIndex].cellcornertype = cellCornerType;
    }

    internal static int GetNumberOfFencePiecesToPayFor(List<BuildTile> CommitedBuildTiles)
    {
      for (int index1 = 0; index1 < CommitedBuildTiles.Count; ++index1)
      {
        for (int index2 = 0; index2 < CommitedBuildTiles.Count; ++index2)
        {
          if (index1 != index2 && CommitedBuildTiles[index1].TileLocation.CompareMatches(CommitedBuildTiles[index2].TileLocation))
            throw new Exception("DUPLICATE LOCATION IN ARRAY");
        }
      }
      return CommitedBuildTiles.Count;
    }

    public static List<Vector2Int> GeturrentAnimalFloorSpace(
      List<BuildTile> CommitedBuildTiles,
      List<BuildTile> Uncommited,
      BuildTile MousePointerLocation,
      bool IncludeMouseAndUncommited = false)
    {
      List<BuildTile> buildTileList1 = new List<BuildTile>();
      List<BuildTile> buildTileList2 = CommitedBuildTiles;
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      if (buildTileList2.Count <= 0)
        return vector2IntList;
      int y1 = buildTileList2[0].TileLocation.Y;
      int y2 = buildTileList2[0].TileLocation.Y;
      for (int index = 0; index < buildTileList2.Count; ++index)
      {
        if (buildTileList2[index].TileLocation.Y < y2)
          y2 = buildTileList2[index].TileLocation.Y;
        if (buildTileList2[index].TileLocation.Y > y1)
          y1 = buildTileList2[index].TileLocation.Y;
        if (buildTileList2[index].TileLocation.X < num1)
          num1 = buildTileList2[index].TileLocation.X;
        if (buildTileList2[index].TileLocation.X > num2)
          num2 = buildTileList2[index].TileLocation.X;
      }
      if (IncludeMouseAndUncommited)
      {
        for (int index = 0; index < Uncommited.Count; ++index)
        {
          if (Uncommited[index].TileLocation.Y < y2)
            y2 = Uncommited[index].TileLocation.Y;
          if (Uncommited[index].TileLocation.Y > y1)
            y1 = Uncommited[index].TileLocation.Y;
          if (Uncommited[index].TileLocation.X < num1)
            num1 = Uncommited[index].TileLocation.X;
          if (Uncommited[index].TileLocation.X > num2)
            num2 = Uncommited[index].TileLocation.X;
        }
      }
      for (int index1 = 0; index1 < buildTileList2.Count; ++index1)
      {
        int num4 = -1;
        for (int index2 = 0; index2 < buildTileList2.Count; ++index2)
        {
          if (index2 != index1 && buildTileList2[index1].TileLocation.X == buildTileList2[index2].TileLocation.X && buildTileList2[index1].TileLocation.Y < buildTileList2[index2].TileLocation.Y && (num4 == -1 || buildTileList2[index2].TileLocation.Y - buildTileList2[index1].TileLocation.Y < num4))
            num4 = buildTileList2[index2].TileLocation.Y - buildTileList2[index1].TileLocation.Y;
        }
        if (IncludeMouseAndUncommited)
        {
          MousePointerLocation?.TileLocation.CompareMatches(buildTileList2[0].TileLocation);
          for (int index2 = 0; index2 < Uncommited.Count; ++index2)
          {
            Vector2Int vector2Int = index2 != Uncommited.Count ? Uncommited[index2].TileLocation : MousePointerLocation.TileLocation;
            if (buildTileList2[index1].TileLocation.X == vector2Int.X && buildTileList2[index1].TileLocation.Y < vector2Int.Y && (num4 == -1 || vector2Int.Y - buildTileList2[index1].TileLocation.Y < num4))
              num4 = vector2Int.Y - buildTileList2[index1].TileLocation.Y;
          }
        }
        int num5 = num4 - 1;
        if (num5 > -1)
        {
          num3 += num5;
          for (int index2 = 0; index2 < num5; ++index2)
            vector2IntList.Add(new Vector2Int(buildTileList2[index1].TileLocation.X, buildTileList2[index1].TileLocation.Y + (index2 + 1)));
        }
      }
      if (IncludeMouseAndUncommited)
      {
        int num4 = 0;
        if (MousePointerLocation != null && !MousePointerLocation.TileLocation.CompareMatches(buildTileList2[0].TileLocation))
          num4 = 1;
        for (int index1 = 0; index1 < Uncommited.Count + num4; ++index1)
        {
          int num5 = -1;
          Vector2Int vector2Int = index1 != Uncommited.Count ? Uncommited[index1].TileLocation : MousePointerLocation.TileLocation;
          for (int index2 = 0; index2 < buildTileList2.Count; ++index2)
          {
            if (vector2Int.X == buildTileList2[index2].TileLocation.X && vector2Int.Y < buildTileList2[index2].TileLocation.Y && (num5 == -1 || buildTileList2[index2].TileLocation.Y - vector2Int.Y < num5))
              num5 = buildTileList2[index2].TileLocation.Y - vector2Int.Y;
          }
          int num6 = num5 - 1;
          if (num6 > -1)
          {
            num3 += num6;
            for (int index2 = 0; index2 < num6; ++index2)
              vector2IntList.Add(new Vector2Int(vector2Int.X, vector2Int.Y + (index2 + 1)));
          }
        }
      }
      for (int index1 = vector2IntList.Count - 1; index1 > -1; --index1)
      {
        for (int index2 = 0; index2 < buildTileList2.Count; ++index2)
        {
          if (buildTileList2[index2].TileLocation.CompareMatches(vector2IntList[index1]))
          {
            vector2IntList.RemoveAt(index1);
            break;
          }
        }
      }
      if (IncludeMouseAndUncommited)
      {
        for (int index1 = vector2IntList.Count - 1; index1 > -1; --index1)
        {
          for (int index2 = 0; index2 < Uncommited.Count; ++index2)
          {
            if (Uncommited[index2].TileLocation.CompareMatches(vector2IntList[index1]))
            {
              vector2IntList.RemoveAt(index1);
              break;
            }
          }
        }
      }
      for (int index1 = vector2IntList.Count - 1; index1 > -1; --index1)
      {
        int num4 = 0;
        int num5 = 0;
        int num6 = 0;
        for (int index2 = 0; index2 < buildTileList2.Count; ++index2)
        {
          if (buildTileList2[index2].TileLocation.X == vector2IntList[index1].X && buildTileList2[index2].TileLocation.Y < vector2IntList[index1].Y && buildTileList2[index2].cellcornertype != CellCornerType.StraightUpDown)
          {
            ++num4;
            if (buildTileList2[index2].cellcornertype == CellCornerType.OuterCorner_BottomLeft || buildTileList2[index2].cellcornertype == CellCornerType.OuterCorner_TopLeft)
              ++num5;
            if (buildTileList2[index2].cellcornertype == CellCornerType.OuterCorner_BottomRight || buildTileList2[index2].cellcornertype == CellCornerType.OuterCorner_TopRight)
              ++num6;
          }
        }
        if (IncludeMouseAndUncommited)
        {
          for (int index2 = 0; index2 < Uncommited.Count; ++index2)
          {
            if (Uncommited[index2].TileLocation.X == vector2IntList[index1].X && Uncommited[index2].TileLocation.Y < vector2IntList[index1].Y && Uncommited[index2].cellcornertype != CellCornerType.StraightUpDown)
            {
              ++num4;
              if (Uncommited[index2].cellcornertype == CellCornerType.OuterCorner_BottomLeft || Uncommited[index2].cellcornertype == CellCornerType.OuterCorner_TopLeft)
                ++num5;
              if (Uncommited[index2].cellcornertype == CellCornerType.OuterCorner_BottomRight || Uncommited[index2].cellcornertype == CellCornerType.OuterCorner_TopRight)
                ++num6;
            }
          }
        }
        if (num4 % 2 == 0)
        {
          if (num5 % 2 == 0 && num6 % 2 == 0)
            vector2IntList.RemoveAt(index1);
          else if (num5 % 2 != 1 || num6 % 2 != 1)
            vector2IntList.RemoveAt(index1);
        }
        else if (num4 > 0 && (num5 % 2 != 0 || num6 % 2 != 0) && num4 % 2 == 1)
          vector2IntList.RemoveAt(index1);
      }
      return vector2IntList;
    }
  }
}
