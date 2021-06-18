// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.HygieneMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_HeatMaps
{
  internal class HygieneMap
  {
    private GameObject HeatTile;
    private GameObject BaseTile;
    private Vector2Int TempLoc;
    private Vector2Int SelectedLocation;
    private static int[,] MapData;
    private static List<BuildingHygieneDisplay> buildingDisplays;

    public HygieneMap(Player player)
    {
      this.TempLoc = new Vector2Int();
      this.HeatTile = new GameObject();
      this.HeatTile.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.HeatTile.SetDrawOriginToCentre();
      this.HeatTile.SetAllColours(1f, 1f, 1f);
      this.BaseTile = new GameObject(this.HeatTile);
      if (HygieneMap.MapData != null)
        return;
      this.RecreateMapData(player);
    }

    private void RecreateMapData(Player player)
    {
      HygieneMap.MapData = new int[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      HygieneMap.buildingDisplays = new List<BuildingHygieneDisplay>();
      foreach (ShopEntry shopentry in player.shopstatus.shopentries)
      {
        if (TileData.IsForFood(shopentry.tiletype) || TileData.IsForThirst(shopentry.tiletype))
        {
          Vector2Int vector2Int1 = new Vector2Int(shopentry.LocationOfThisShop.X, shopentry.LocationOfThisShop.Y);
          TileInfo tileInfo = TileData.GetTileInfo(shopentry.tiletype);
          int rotationClockWise = player.prisonlayout.layout.BaseTileTypes[vector2Int1.X, vector2Int1.Y].RotationClockWise;
          Vector2Int vector2Int2 = new Vector2Int(tileInfo.GetTileWidth(rotationClockWise), tileInfo.GetTileHeight(rotationClockWise));
          Vector2Int intOrigin = tileInfo.GetIntOrigin(rotationClockWise);
          Vector2Int vector2Int3 = vector2Int1 + new Vector2Int(-intOrigin.X, vector2Int2.Y - intOrigin.Y - 1);
          Vector2Int vector2Int4 = vector2Int1 + new Vector2Int(vector2Int2.X - intOrigin.X - 1, -intOrigin.Y);
          for (int x = vector2Int3.X; x <= vector2Int4.X; ++x)
          {
            for (int y = vector2Int4.Y; y <= vector2Int3.Y; ++y)
              HygieneMap.MapData[x, y] = 1;
          }
          BuildingHygieneDisplay buildingHygieneDisplay = new BuildingHygieneDisplay();
          buildingHygieneDisplay.worldSpaceLocation = TileMath.GetTileToWorldSpace(new Vector2Int(vector2Int3.X, vector2Int3.Y));
          buildingHygieneDisplay.worldSpaceLocation.X -= (TileMath.GetTileToWorldSpace(new Vector2Int(vector2Int3.X, 0)) - TileMath.GetTileToWorldSpace(new Vector2Int(vector2Int4.X, 0))).X * 0.5f;
          float Rotation = 0.0f;
          Rectangle rect = tileInfo.GetRect(rotationClockWise, ref Rotation);
          buildingHygieneDisplay.worldSpaceLocation.Y -= TileMath.GetTileToWorldSpace(new Vector2Int(0, rect.Height / 16)).Y;
          buildingHygieneDisplay.worldSpaceLocation.Y -= 5f;
          HygieneMap.buildingDisplays.Add(buildingHygieneDisplay);
        }
      }
      Z_GameFlags.MustRebuildHygieneMap = false;
    }

    public void UpdateHygieneMap(Player player)
    {
      if (!Z_GameFlags.MustRebuildHygieneMap)
        return;
      this.RecreateMapData(player);
    }

    public void DrawFoodDrinksShopsMap()
    {
      if (HygieneMap.MapData == null)
        return;
      int StartX;
      int StartY;
      int ENDX;
      int ENDY;
      TileMath.GetDrawArrayLimits(out StartX, out StartY, out ENDX, out ENDY);
      if (ENDX > HygieneMap.MapData.GetLength(0))
        ENDX = HygieneMap.MapData.GetLength(0);
      if (ENDY > HygieneMap.MapData.GetLength(1))
        ENDY = HygieneMap.MapData.GetLength(1);
      for (int _X = StartX; _X < ENDX; ++_X)
      {
        for (int _Y = StartY; _Y < ENDY; ++_Y)
        {
          if (HygieneMap.MapData[_X, _Y] != 1)
          {
            this.BaseTile.fAlpha = 0.55f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
        }
      }
      for (int index = 0; index < HygieneMap.buildingDisplays.Count; ++index)
        HygieneMap.buildingDisplays[index].DrawFoodDrinkBuildingDisplay(AssetContainer.pointspritebatch01);
    }
  }
}
