// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder.InnerDirectionCalculator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_BuldMenu.PenBuilder.Pens.WallBuilder
{
  internal class InnerDirectionCalculator
  {
    internal static void GetThisInnerDirection(
      int ThisIndex,
      Vector2Int TileLoc,
      List<Vector2Int> floor,
      out InternalDirection iternaldirection,
      List<BuildTile> _CommitedBuildTiles,
      bool CalculateInternal = true)
    {
      iternaldirection = InternalDirection.Count;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < _CommitedBuildTiles.Count; ++index)
      {
        if (index != ThisIndex)
        {
          if ((_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.StraightLeftRight || _CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_TopLeft || (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomRight || _CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_TopRight) || _CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomLeft) && _CommitedBuildTiles[ThisIndex].TileLocation.X == _CommitedBuildTiles[index].TileLocation.X)
          {
            if (_CommitedBuildTiles[index].cellcornertype != CellCornerType.StraightUpDown)
            {
              if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomRight || _CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomLeft)
              {
                if (TileLoc.Y > _CommitedBuildTiles[index].TileLocation.Y)
                {
                  if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.StraightLeftRight)
                    ++num1;
                  else if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_TopRight || _CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_BottomRight)
                    ++num2;
                  else
                    ++num3;
                }
              }
              else if (TileLoc.Y < _CommitedBuildTiles[index].TileLocation.Y)
              {
                if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.StraightLeftRight)
                  ++num1;
                else if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_TopRight || _CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_BottomRight)
                  ++num2;
                else
                  ++num3;
              }
            }
          }
          else if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.StraightUpDown && TileLoc.Y == _CommitedBuildTiles[index].TileLocation.Y && (_CommitedBuildTiles[index].cellcornertype != CellCornerType.StraightLeftRight && _CommitedBuildTiles[ThisIndex].TileLocation.X < _CommitedBuildTiles[index].TileLocation.X))
          {
            if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.StraightUpDown)
              ++num1;
            else if (_CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_TopRight || _CommitedBuildTiles[index].cellcornertype == CellCornerType.OuterCorner_TopLeft)
              ++num2;
            else
              ++num3;
          }
        }
      }
      if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_TopLeft)
      {
        if (num2 + num3 <= 0)
          throw new Exception("Has to be a corner");
        if (num2 % 2 == 1 && num3 % 2 == 0)
        {
          iternaldirection = InternalDirection.Up;
        }
        else
        {
          if (num2 % 2 != 0 || num3 % 2 != 1)
            throw new Exception("Just corners must add up to Odd number");
          iternaldirection = InternalDirection.Down;
        }
        if (num1 % 2 == 1)
          iternaldirection = iternaldirection != InternalDirection.Down ? InternalDirection.Down : InternalDirection.Up;
      }
      else if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_TopRight)
      {
        if (num2 + num3 <= 0)
          throw new Exception("Has to be a corner");
        if (num2 % 2 == 1 && num3 % 2 == 0)
        {
          iternaldirection = InternalDirection.Down;
        }
        else
        {
          if (num2 % 2 != 0 || num3 % 2 != 1)
            throw new Exception("Just corners must add up to Odd number");
          iternaldirection = InternalDirection.Up;
        }
        if (num1 % 2 == 1)
          iternaldirection = iternaldirection != InternalDirection.Down ? InternalDirection.Down : InternalDirection.Up;
      }
      if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomLeft)
      {
        if (num2 + num3 <= 0)
          throw new Exception("Has to be a corner");
        if (num2 % 2 == 1 && num3 % 2 == 0)
        {
          iternaldirection = InternalDirection.Down;
        }
        else
        {
          if (num2 % 2 != 0 || num3 % 2 != 1)
            throw new Exception("Just corners must add up to Odd number");
          iternaldirection = InternalDirection.Up;
        }
        if (num1 % 2 == 1)
          iternaldirection = iternaldirection != InternalDirection.Down ? InternalDirection.Down : InternalDirection.Up;
      }
      if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.OuterCorner_BottomRight)
      {
        if (num2 + num3 <= 0)
          throw new Exception("Has to be a corner");
        if (num2 % 2 == 1 && num3 % 2 == 0)
        {
          iternaldirection = InternalDirection.Up;
        }
        else
        {
          if (num2 % 2 != 0 || num3 % 2 != 1)
            throw new Exception("Just corners must add up to Odd number");
          iternaldirection = InternalDirection.Down;
        }
        if (num1 % 2 == 1)
          iternaldirection = iternaldirection != InternalDirection.Down ? InternalDirection.Down : InternalDirection.Up;
      }
      if (_CommitedBuildTiles[ThisIndex].cellcornertype == CellCornerType.StraightLeftRight)
      {
        iternaldirection = num2 % 2 != 1 ? InternalDirection.Up : InternalDirection.Down;
        if (num1 % 2 == 1)
          iternaldirection = iternaldirection != InternalDirection.Down ? InternalDirection.Down : InternalDirection.Up;
      }
      if (_CommitedBuildTiles[ThisIndex].cellcornertype != CellCornerType.StraightUpDown)
        return;
      iternaldirection = num2 % 2 != 1 ? InternalDirection.Left : InternalDirection.Right;
      if (num1 % 2 != 1)
        return;
      if (iternaldirection == InternalDirection.Right)
        iternaldirection = InternalDirection.Left;
      else
        iternaldirection = InternalDirection.Right;
    }
  }
}
