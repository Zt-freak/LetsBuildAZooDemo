// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.PrisonWalls
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.Layout
{
  internal class PrisonWalls
  {
    internal static void CheckPrisonWalls(LayoutEntry[,] BaseTileTypes)
    {
    }

    private static void SetTile(
      int X,
      int Y,
      LayoutEntry[,] BaseTileTypes,
      TILETYPE tileype,
      int Rotation)
    {
      if (X == 103)
        ;
      PrisonWalls.IsThisTileBlocked(X, Y, BaseTileTypes);
      if (X == 95 && Y == 103)
        ;
      BaseTileTypes[X, Y].tiletype = tileype;
      if (Rotation <= -1)
        return;
      BaseTileTypes[X, Y].RotationClockWise = Rotation;
    }

    private static bool IsThisTileBlocked(int X, int Y, LayoutEntry[,] BaseTileTypes)
    {
      if (X >= 0 && Y >= 0 && (X < BaseTileTypes.GetLength(0) && Y < BaseTileTypes.GetLength(1)) && BaseTileTypes[X, Y].tiletype != TILETYPE.None)
      {
        switch (TileData.GetTileInfo(BaseTileTypes[X, Y].tiletype).buildingtype)
        {
          case BUILDINGTYPE.EmptyMoon:
          case BUILDINGTYPE.Plant:
          case BUILDINGTYPE.MoonPlant:
          case BUILDINGTYPE.PrisonWall:
            break;
          default:
            return true;
        }
      }
      return false;
    }

    private static void SetVertical(int X, int Y, int rotation, LayoutEntry[,] BaseTileTypes)
    {
      if (rotation != 0)
        ;
      if (Y % 2 == 0)
        PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonOuterWallLight, rotation);
      else
        PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonOuterWall, rotation);
      BaseTileTypes[X, Y].RotationClockWise = rotation;
    }

    private static void SetHorizontal(int X, int Y, int rotation, LayoutEntry[,] BaseTileTypes)
    {
      if (rotation != 1)
        ;
      if (X % 2 == 0)
        PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonOuterWallLight, rotation);
      else
        PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonOuterWall, rotation);
      BaseTileTypes[X, Y].RotationClockWise = rotation;
    }

    private static void SetSolid(int XL, int YL, LayoutEntry[,] BaseTileTypes)
    {
      PrisonWalls.SetTile(XL, YL, BaseTileTypes, TILETYPE.EMPTY_DIRT_WALKABLE_TILE, -1);
      if (XL % 2 == 0 && YL % 2 == 0)
        BaseTileTypes[XL, YL].RotationClockWise = 0;
      else if (XL % 2 == 1 && YL % 2 == 0)
        BaseTileTypes[XL, YL].RotationClockWise = 1;
      if (XL % 2 == 0 && YL % 2 == 1)
      {
        BaseTileTypes[XL, YL].RotationClockWise = 0;
      }
      else
      {
        if (XL % 2 != 1 || YL % 2 != 1)
          return;
        BaseTileTypes[XL, YL].RotationClockWise = 1;
      }
    }

    private static bool CheckTileCanHaveAWall(
      LayoutEntry[,] BaseTileTypes,
      int X,
      int Y,
      out bool OverwroteWall,
      bool AllowPotentialWallOverwrite = false)
    {
      OverwroteWall = false;
      if (X >= 0 && X < BaseTileTypes.GetLength(0) && (Y >= 0 && Y < BaseTileTypes.GetLength(1)) && BaseTileTypes[X, Y] != null)
      {
        if (BaseTileTypes[X, Y].tiletype == TILETYPE.None)
          return true;
        BUILDINGTYPE buildingtype = TileData.GetTileInfo(BaseTileTypes[X, Y].tiletype).buildingtype;
        switch (buildingtype)
        {
          case BUILDINGTYPE.EmptyMoon:
          case BUILDINGTYPE.Plant:
          case BUILDINGTYPE.MoonPlant:
            if (TileData.GetTileInfo(BaseTileTypes[X, Y].tiletype).buildingtype != BUILDINGTYPE.PrisonWall)
              return true;
            break;
          default:
            if (AllowPotentialWallOverwrite && buildingtype == BUILDINGTYPE.PrisonWall)
            {
              OverwroteWall = true;
              return true;
            }
            break;
        }
      }
      return false;
    }

    private static bool CheckDiagonalSeam(
      int X,
      int Y,
      int ThisWallRotation,
      int ExpectedCollisionWallRotation,
      int DiagonalRotation,
      LayoutEntry[,] BaseTileTypes,
      out bool WasOuterCorner,
      out bool ForcedSolid)
    {
      ForcedSolid = false;
      WasOuterCorner = false;
      if (X == 95)
        ;
      if (Y > 0 && X > 0 && (BaseTileTypes[X, Y].tiletype != TILETYPE.None && TileData.GetTileInfo(BaseTileTypes[X, Y].tiletype).buildingtype == BUILDINGTYPE.PrisonWall))
      {
        if (BaseTileTypes[X, Y].tiletype == TILETYPE.PrisonOuterWallLight || BaseTileTypes[X, Y].tiletype == TILETYPE.PrisonOuterWall)
        {
          if (BaseTileTypes[X, Y].RotationClockWise != ThisWallRotation)
          {
            if (BaseTileTypes[X, Y].RotationClockWise == ExpectedCollisionWallRotation || ExpectedCollisionWallRotation == -1)
            {
              bool flag = true;
              if (DiagonalRotation == 0 && X < BaseTileTypes.GetLength(0) - 1 && (Y < BaseTileTypes.GetLength(1) - 1 && PrisonWalls.IsThisTileBlocked(X + 1, Y + 1, BaseTileTypes)))
              {
                flag = false;
                PrisonWalls.SetSolid(X, Y, BaseTileTypes);
              }
              if (flag)
                PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonWallInnerCorner, DiagonalRotation);
              return true;
            }
            if (ThisWallRotation == 3 && BaseTileTypes[X, Y].RotationClockWise == 1)
              PrisonWalls.SetTile(X, Y, BaseTileTypes, TILETYPE.PrisonWallInnerCorner, 2);
          }
        }
        else if (BaseTileTypes[X, Y].tiletype == TILETYPE.PrisonWallInnerCorner)
        {
          if (BaseTileTypes[X, Y].RotationClockWise != DiagonalRotation)
          {
            bool flag = false;
            if (Y > 0 && BaseTileTypes[X, Y - 1].tiletype == TILETYPE.PrisonWallInnerCorner && BaseTileTypes[X, Y - 1].RotationClockWise == 0)
              flag = true;
            if (X < BaseTileTypes.GetLength(1) - 1 && X > 0 && (PrisonWalls.IsThisTileBlocked(X - 1, Y, BaseTileTypes) && !PrisonWalls.IsThisTileBlocked(X + 1, Y, BaseTileTypes)) && !PrisonWalls.IsThisTileBlocked(X, Y - 1, BaseTileTypes))
              flag = true;
            if (X > 0 && Y < BaseTileTypes.GetLength(1) - 1 && (BaseTileTypes[X, Y].tiletype == TILETYPE.PrisonWallInnerCorner && BaseTileTypes[X, Y].RotationClockWise == 1) && !PrisonWalls.IsThisTileBlocked(X - 1, Y + 1, BaseTileTypes))
              flag = true;
            if (!flag)
            {
              PrisonWalls.SetSolid(X, Y, BaseTileTypes);
              ForcedSolid = true;
            }
          }
        }
        else if (BaseTileTypes[X, Y].tiletype == TILETYPE.PrisonWallOuterCorner)
          WasOuterCorner = true;
        else if (BaseTileTypes[X, Y].tiletype != TILETYPE.EMPTY_DIRT_WALKABLE_TILE)
        {
          int tiletype = (int) BaseTileTypes[X, Y].tiletype;
        }
      }
      return false;
    }

    private static void CheckTopRightForOuterCorner(int X, int Y, LayoutEntry[,] BaseTileTypes)
    {
      if (!PrisonWalls.IsThisTileBlocked(X, Y, BaseTileTypes) || X >= BaseTileTypes.GetLength(0) - 1 || (PrisonWalls.IsThisTileBlocked(X + 1, Y, BaseTileTypes) || Y <= 0) || (PrisonWalls.IsThisTileBlocked(X + 1, Y - 1, BaseTileTypes) || BaseTileTypes[X + 1, Y - 1].tiletype != TILETYPE.None && TileData.GetTileInfo(BaseTileTypes[X + 1, Y - 1].tiletype).buildingtype == BUILDINGTYPE.PrisonWall))
        return;
      PrisonWalls.SetTile(X + 1, Y - 1, BaseTileTypes, TILETYPE.PrisonWallOuterCorner, 3);
    }

    private static void CheckTopForLeftOuterCorner(int X, int Y, LayoutEntry[,] BaseTileTypes)
    {
      if (X <= 0 || PrisonWalls.IsThisTileBlocked(X - 1, Y, BaseTileTypes) || !PrisonWalls.CheckTileCanHaveAWall(BaseTileTypes, X - 1, Y - 1, out bool _))
        return;
      PrisonWalls.SetTile(X - 1, Y - 1, BaseTileTypes, TILETYPE.PrisonWallOuterCorner, 2);
      if (X <= 1)
        return;
      if (BaseTileTypes[X - 2, Y - 1].tiletype == TILETYPE.PrisonWallInnerCorner && BaseTileTypes[X - 2, Y - 1].RotationClockWise == 3)
        PrisonWalls.SetHorizontal(X - 2, Y - 1, 2, BaseTileTypes);
      if (BaseTileTypes[X - 2, Y - 1].tiletype != TILETYPE.PrisonOuterWall && BaseTileTypes[X - 2, Y - 1].tiletype != TILETYPE.PrisonOuterWallLight)
        return;
      int rotationClockWise = BaseTileTypes[X - 2, Y - 1].RotationClockWise;
    }
  }
}
