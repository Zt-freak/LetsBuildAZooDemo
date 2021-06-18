// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.CarbonMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld;

namespace TinyZoo.Z_HeatMaps
{
  internal class CarbonMap
  {
    private GameObject HeatTile;
    private GameObject BaseTile;
    private Vector2Int TempLoc;
    private static int[,] CarbonMapData;
    private static List<Vector2Int> Trees;
    internal static bool NeedtORecreateCarbonMap = true;
    private static int TreeArray;
    private static int ResetArray;

    public CarbonMap(Player player)
    {
      this.TempLoc = new Vector2Int();
      this.HeatTile = new GameObject();
      this.HeatTile.scale = 15f;
      this.HeatTile.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.HeatTile.SetDrawOriginToCentre();
      this.HeatTile.SetAllColours(1f, 1f, 1f);
      this.BaseTile = new GameObject(this.HeatTile);
      if (CarbonMap.CarbonMapData != null)
        return;
      CarbonMap.RecreateCarbonMap(player);
    }

    internal static void AddTree(Vector2Int TreeLoc)
    {
      if (CarbonMap.Trees == null)
        CarbonMap.Trees = new List<Vector2Int>();
      CarbonMap.NeedtORecreateCarbonMap = true;
      CarbonMap.Trees.Add(TreeLoc);
      CarbonMap.SetTreeTimer();
    }

    internal static void RemoveTree(Vector2Int TreeLoc)
    {
      for (int index = CarbonMap.Trees.Count - 1; index > -1; --index)
      {
        if (CarbonMap.Trees[index].CompareMatches(TreeLoc))
        {
          CarbonMap.NeedtORecreateCarbonMap = true;
          CarbonMap.Trees.RemoveAt(index);
          CarbonMap.SetTreeTimer();
          return;
        }
      }
      throw new Exception("NO WAY");
    }

    internal static void SpawnC02()
    {
      if (Z_DebugFlags.IsBetaVersion || CarbonMap.Trees.Count <= 0)
        return;
      int num = CarbonMap.Trees.Count / 4;
      if (num == 0)
        num = 1;
      CarbonMap.TreeArray += num;
      if (CarbonMap.TreeArray >= CarbonMap.Trees.Count)
      {
        CarbonMap.TreeArray = CarbonMap.ResetArray;
        ++CarbonMap.ResetArray;
        if (CarbonMap.ResetArray >= num)
          CarbonMap.ResetArray = 0;
        if (CarbonMap.TreeArray >= CarbonMap.Trees.Count)
          CarbonMap.TreeArray = TinyZoo.Game1.Rnd.Next(0, CarbonMap.Trees.Count);
      }
      MoneyRenderer.DropCarbon(TileMath.GetTileToWorldSpace(CarbonMap.Trees[CarbonMap.TreeArray]) + new Vector2(-8f, -25f * Sengine.ScreenRatioUpwardsMultiplier.Y), -3);
    }

    internal static void RecreateCarbonMap(Player player)
    {
      CarbonMap.Trees = new List<Vector2Int>();
      CarbonMap.CarbonMapData = new int[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      for (int _X = 0; _X < player.prisonlayout.layout.BaseTileTypes.GetLength(0); ++_X)
      {
        for (int _Y = 0; _Y < player.prisonlayout.layout.BaseTileTypes.GetLength(1); ++_Y)
        {
          if (player.prisonlayout.layout.BaseTileTypes[_X, _Y] != null && player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype != TILETYPE.None)
          {
            POINT_OF_INTEREST subcat_pointofinterest;
            CATEGORYTYPE tileTypeToCetagory = CategoryData.GetTileTypeToCetagory(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype, out subcat_pointofinterest);
            int num = 0;
            switch (tileTypeToCetagory)
            {
              case CATEGORYTYPE.Shops:
                switch (subcat_pointofinterest)
                {
                  case POINT_OF_INTEREST.DrinksStore:
                    num = 2;
                    break;
                  case POINT_OF_INTEREST.FoodStore:
                    num = 2;
                    break;
                  case POINT_OF_INTEREST.GiftShop:
                    num = 2;
                    break;
                }
                break;
              case CATEGORYTYPE.Amenities:
                switch (subcat_pointofinterest)
                {
                  case POINT_OF_INTEREST.Bin:
                    num = -3;
                    break;
                  case POINT_OF_INTEREST.Toilet:
                    num = -3;
                    break;
                }
                break;
              case CATEGORYTYPE.Nature:
                num = 3;
                if (CategoryData.IsThisATree(player.prisonlayout.layout.BaseTileTypes[_X, _Y].tiletype))
                {
                  CarbonMap.Trees.Add(new Vector2Int(_X, _Y));
                  break;
                }
                break;
            }
            if (num != 0)
            {
              CarbonMap.CarbonMapData[_X, _Y] = num;
              if (_X > 0)
              {
                if (num > 0)
                {
                  if (CarbonMap.CarbonMapData[_X - 1, _Y] < 3)
                    ++CarbonMap.CarbonMapData[_X - 1, _Y];
                }
                else if (num > -3)
                  --CarbonMap.CarbonMapData[_X - 1, _Y];
              }
              if (_Y > 0)
              {
                if (num < 0 && CarbonMap.CarbonMapData[_X, _Y - 1] < 3)
                  ++CarbonMap.CarbonMapData[_X, _Y - 1];
              }
              else if (num > -3)
                --CarbonMap.CarbonMapData[_X, _Y - 1];
            }
          }
        }
      }
      CarbonMap.SetTreeTimer();
    }

    private static void SetTreeTimer()
    {
      if (CarbonMap.Trees.Count > 0)
        LiveStats.SetCOTWO_TreeTimer(Z_GameFlags.SecondsZooOpenPerDay / (float) CarbonMap.Trees.Count);
      else
        LiveStats.SetCOTWO_TreeTimer(-1f);
    }

    public int GetPollutionLevel(int XLoc, int YLoc) => XLoc > -1 && YLoc > -1 && (XLoc < CarbonMap.CarbonMapData.GetLength(0) && YLoc < CarbonMap.CarbonMapData.GetLength(1)) ? CarbonMap.CarbonMapData[XLoc, YLoc] : 0;

    public void DrawCarbonMap()
    {
      if (CarbonMap.CarbonMapData == null)
        return;
      int StartX;
      int StartY;
      int ENDX;
      int ENDY;
      TileMath.GetDrawArrayLimits(out StartX, out StartY, out ENDX, out ENDY);
      if (ENDX > CarbonMap.CarbonMapData.GetLength(0))
        ENDX = CarbonMap.CarbonMapData.GetLength(0);
      if (ENDY > CarbonMap.CarbonMapData.GetLength(1))
        ENDY = CarbonMap.CarbonMapData.GetLength(1);
      for (int _X = StartX; _X < ENDX; ++_X)
      {
        for (int _Y = StartY; _Y < ENDY; ++_Y)
        {
          this.TempLoc.X = _X;
          this.TempLoc.Y = _Y;
          if (CarbonMap.CarbonMapData[_X, _Y] > 0)
          {
            this.HeatTile.fAlpha = (float) (0.5 + ((double) CarbonMap.CarbonMapData[_X, _Y] - 1.0) * 0.100000001490116);
            this.HeatTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            float num = 0.4f - (float) (((double) CarbonMap.CarbonMapData[_X, _Y] - 1.0) * 0.100000001490116);
            this.HeatTile.SetAllColours(num, num, 1f);
            this.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else
          {
            this.BaseTile.fAlpha = 0.4f;
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
        }
      }
    }
  }
}
