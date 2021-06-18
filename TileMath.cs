// Decompiled with JetBrains decompiler
// Type: TinyZoo.TileMath
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_Data;

namespace TinyZoo
{
  internal class TileMath
  {
    private static int OverWorldMapSize_XDefault = 325;
    internal static int OverWorldMapSize_ExtraSize = 0;
    private static int OverWorldMapSize_YSize = 230;
    internal static float TileSize = 16f;
    internal static float HalfTileSize = 8f;
    internal static int FadeOutRange = 20;
    internal static int BufferAtWoldBottom = 8;
    private static int _SectorSize = 25;
    private static List<Vector2Int> Sectors;
    private static Vector2Int TotalSectorCount;
    private static float Left;
    private static float Right;
    private static float Top;
    private static float Bottom;
    internal static int RightWallArrayLocation = 15;
    internal static int BottomeWallArrayLocation = 8;

    internal static void ResetMapSize()
    {
      TileMath.OverWorldMapSize_XDefault = 325;
      TileMath.OverWorldMapSize_YSize = 230;
    }

    internal static Vector2 GetGamePlayMapCenter() => new Vector2((float) (((double) TileMath.Right - (double) TileMath.Left) * 0.5) + TileMath.Left, (float) (((double) TileMath.Bottom - (double) TileMath.Top) * 0.5) + TileMath.Top);

    internal static Vector2Int GetRandomLocationInPlaySpace(bool ResetSectorLists = false)
    {
      if (TileMath.Sectors == null)
      {
        TileMath.Sectors = new List<Vector2Int>();
        for (int _X = 0; _X < PlayerStats.unblockedSectors.GetLength(0); ++_X)
        {
          for (int _Y = 0; _Y < PlayerStats.unblockedSectors.GetLength(1); ++_Y)
          {
            if (PlayerStats.unblockedSectors[_X, _Y])
              TileMath.Sectors.Add(new Vector2Int(_X, _Y));
          }
        }
      }
      return TileMath.Sectors[Game1.Rnd.Next(0, TileMath.Sectors.Count)];
    }

    internal static Vector2Int GetRandomLocationInSector(Vector2Int Sectorlocation) => new Vector2Int(Sectorlocation.X * TileMath._SectorSize, Sectorlocation.Y * TileMath._SectorSize) + new Vector2Int(Game1.Rnd.Next(0, TileMath._SectorSize), Game1.Rnd.Next(0, TileMath._SectorSize));

    internal static bool TileIsOverEntryPath(int XL, int YL) => XL >= 161 && XL <= 163 && (YL >= 222 && YL <= 224);

    public static Vector2Int GetWorldSpaceToTile(Vector2 Location)
    {
      Location.X += 8f;
      Location.Y += 8f;
      return new Vector2Int(Location.X / 16f, Location.Y / (16f * Sengine.ScreenRatioUpwardsMultiplier.Y));
    }

    internal static Vector2Int GetGarbageBinLocation_Right(int BinIndex) => new Vector2Int(WalkingPerson.LogoLocation.X + (20 + BinIndex * 2), WalkingPerson.LogoLocation.Y + 1);

    internal static Vector2Int GetGateLocationV2Int() => new Vector2Int(WalkingPerson.LogoLocation.X + Game1.Rnd.Next(-1, 2), WalkingPerson.LogoLocation.Y + 1);

    internal static void GetDrawArrayLimits(
      out int StartX,
      out int StartY,
      out int ENDX,
      out int ENDY)
    {
      Vector2Int spaceToTileLocation1 = TileMath.GetScreenSPaceToTileLocation(Vector2.Zero);
      Vector2Int spaceToTileLocation2 = TileMath.GetScreenSPaceToTileLocation(Sengine.ReferenceScreenRes);
      StartX = spaceToTileLocation1.X;
      if (StartX < 0)
        StartX = 0;
      StartY = spaceToTileLocation1.Y;
      if (StartY < 0)
        StartY = 0;
      ENDY = spaceToTileLocation2.Y;
      ENDX = spaceToTileLocation2.X;
      ENDY += 2;
      ++ENDX;
      ++ENDX;
      ++ENDY;
    }

    public static Vector2 GetPercentageThroughTile(Vector2 Location)
    {
      Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(Location);
      return new Vector2((float) (((double) Location.X - (double) (worldSpaceToTile.X * 16)) / 16.0), (Location.X - (float) (worldSpaceToTile.X * 16)) / 16f);
    }

    internal static bool IsBelowPark(int YLOC) => YLOC >= TileMath.OverWorldMapSize_YSize - 5;

    internal static int GetOverWorldMapSize_XDefault() => TileMath.OverWorldMapSize_XDefault;

    internal static void SetOverWorldMapSize_XDefault(int Value) => TileMath.OverWorldMapSize_XDefault = Value;

    internal static Vector2Int GetTotalSectors()
    {
      if (TileMath.TotalSectorCount == null)
        TileMath.TotalSectorCount = new Vector2Int(TileMath.GetOverWorldMapSize_XDefault() / TileMath.SectorSize, TileMath.GetOverWorldMapSize_YSize() / TileMath.SectorSize);
      return TileMath.TotalSectorCount;
    }

    internal static int GetLeftParkPlayableSpace()
    {
      Vector2Int TopLeft;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out TopLeft, out Vector2Int _, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      return TopLeft.X;
    }

    public static int GetRightParkPlayableSpace()
    {
      Vector2Int BottomRight;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out Vector2Int _, out BottomRight, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      return BottomRight.X;
    }

    internal static int GetTopParkPlayableSpace()
    {
      Vector2Int TopLeft;
      Z_WorldExpansion.GetSizes(PlayerStats.LandSize, out TopLeft, out Vector2Int _, TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize());
      return TopLeft.Y;
    }

    internal static int SectorSize
    {
      get => TileMath._SectorSize;
      set => throw new Exception("DO NOT CHANGE THIS!");
    }

    internal static Vector2Int GetLocationToSector(int Xloc, int YLoc) => new Vector2Int(Xloc / TileMath._SectorSize, YLoc / TileMath._SectorSize);

    public static int GetBottomParkPlayableSpace() => TileMath.GetOverWorldMapSize_YSize() - 7;

    internal static int GetOverWorldMapSize_YSize() => TileMath.OverWorldMapSize_YSize;

    internal static void SetOverWorldMapSize_YSize(int Value) => TileMath.OverWorldMapSize_YSize = Value;

    internal static bool TileIsInBuildablePartOfWorld(int LocX, int LocY, bool IncludeFences = false)
    {
      if (TileMath.IsBelowPark(LocY))
        return false;
      int index1 = LocX / TileMath.SectorSize;
      int index2 = LocY / TileMath.SectorSize;
      return index1 >= 0 && index1 <= PlayerStats.unblockedSectors.GetLength(0) - 1 && (index2 >= 0 && index2 <= PlayerStats.unblockedSectors.GetLength(1) - 1) && PlayerStats.unblockedSectors[index1, index2];
    }

    internal static bool TileIsInWorld(int LocX, int LocY) => LocX >= 0 && LocY >= 0 && LocY < TileMath.OverWorldMapSize_YSize && LocX < TileMath.OverWorldMapSize_XDefault;

    internal static Vector2Int GetScreenSPaceToTileLocation(Vector2 ScreenLocation)
    {
      ScreenLocation = RenderMath.TranslateScreenSpaceToWorldSpace(ScreenLocation);
      return TileMath.GetWorldSpaceToTile(ScreenLocation);
    }

    internal static float GetPlaySpaceLeft() => TileMath.Left;

    internal static float GetPlaySpaceTop() => TileMath.Top * Sengine.ScreenRatioUpwardsMultiplier.Y;

    internal static float GetPlaySpaceRight() => TileMath.Right;

    internal static float GetPlaySpaceBottom() => TileMath.Bottom * Sengine.ScreenRatioUpwardsMultiplier.Y;

    internal static void SetPlaySpaceLeft(float _Left) => TileMath.Left = _Left;

    internal static void SetPlaySpaceTop(float _Top) => TileMath.Top = _Top;

    internal static void SetPlaySpaceRight(float _Right) => TileMath.Right = _Right;

    internal static void SetPlaySpaceBottom(float _Bottom) => TileMath.Bottom = _Bottom;

    internal static Vector2Int GetScreenCenter() => new Vector2Int()
    {
      X = (int) ((double) Sengine.WorldOriginandScale.X / 16.0),
      Y = (int) ((double) Sengine.WorldOriginandScale.Y / (16.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y))
    };

    internal static Vector2Int GetGateLocationvector2Int() => new Vector2Int(TileMath.GetOverWorldMapSize_XDefault() / 2, TileMath.GetOverWorldMapSize_YSize() - 6);

    internal static Vector2 GetGateLocation()
    {
      float y = (float) ((TileMath.GetOverWorldMapSize_YSize() - 3) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y;
      return new Vector2((float) (TileMath.GetOverWorldMapSize_XDefault() * 16) * 0.5f, y);
    }

    internal static Vector2 GetPlayCenter() => new Vector2((float) (((double) TileMath.GetPlaySpaceRight() - (double) TileMath.GetPlaySpaceLeft()) * 0.5) + TileMath.GetPlaySpaceLeft(), (TileMath.GetPlaySpaceBottom() - TileMath.GetPlaySpaceTop()) * 0.5f + TileMath.GetPlaySpaceTop());

    internal static Vector2 GetTileToWorldSpace(Vector2Int Location) => new Vector2((float) Location.X * TileMath.TileSize, (float) Location.Y * TileMath.TileSize * Sengine.ScreenRatioUpwardsMultiplier.Y);
  }
}
