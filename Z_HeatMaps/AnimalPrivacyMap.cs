// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.AnimalPrivacyMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Objects;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.Z_HeatMaps
{
  internal class AnimalPrivacyMap
  {
    private GameObject HeatTile;
    private GameObject BaseTile;
    private Vector2Int TempLoc;
    private Vector2Int SelectedLocation;
    private static int[,] PrivacyMapData;

    public AnimalPrivacyMap(Player player)
    {
      this.TempLoc = new Vector2Int();
      this.HeatTile = new GameObject();
      this.HeatTile.scale = 15f;
      this.HeatTile.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.HeatTile.SetDrawOriginToCentre();
      this.HeatTile.SetAllColours(1f, 1f, 1f);
      this.BaseTile = new GameObject(this.HeatTile);
      if (AnimalPrivacyMap.PrivacyMapData != null)
        return;
      AnimalPrivacyMap.RecreatePrivacyMap(player);
    }

    internal static void RecreatePrivacyMap(Player player)
    {
      AnimalPrivacyMap.PrivacyMapData = new int[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        player.prisonlayout.cellblockcontainer.prisonzones[index1].viewinglocations = new List<ViewingLocation>();
        player.prisonlayout.cellblockcontainer.prisonzones[index1].SecondRowViewingLocations = new List<ViewingLocation>();
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles.Count; ++index2)
          AnimalPrivacyMap.PrivacyMapData[player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].X, player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].Y] = player.prisonlayout.GetPrivacyForAnimalPenTile(player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].X, player.prisonlayout.cellblockcontainer.prisonzones[index1].FloorTiles[index2].Y, AnimalPrivacyMap.PrivacyMapData, player.prisonlayout.cellblockcontainer.prisonzones[index1]);
        player.prisonlayout.cellblockcontainer.prisonzones[index1].SetViewingRandomStart();
      }
    }

    public void UpdateAnimalPrivacyMap(Player player)
    {
      if (Z_GameFlags.MustRebuildPrivacyMap)
      {
        Z_GameFlags.MustRebuildPrivacyMap = false;
        AnimalPrivacyMap.RecreatePrivacyMap(player);
      }
      if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0)
        return;
      Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(RenderMath.TranslateScreenSpaceToWorldSpace(player.player.touchinput.ReleaseTapArray[0]));
      this.SelectedLocation = this.SelectedLocation == null || !this.SelectedLocation.CompareMatches(worldSpaceToTile) ? worldSpaceToTile : (Vector2Int) null;
      if (!player.inputmap.RightMousReleaseClick)
        return;
      this.SelectedLocation = (Vector2Int) null;
    }

    public int GetPrivacayPercentage(int XLoc, int YLoc) => XLoc > -1 && YLoc > -1 && (XLoc < AnimalPrivacyMap.PrivacyMapData.GetLength(0) && YLoc < AnimalPrivacyMap.PrivacyMapData.GetLength(1)) ? AnimalPrivacyMap.PrivacyMapData[XLoc, YLoc] : 0;

    public void DrawAnimalPrivacyMap()
    {
      if (AnimalPrivacyMap.PrivacyMapData == null)
        return;
      int StartX;
      int StartY;
      int ENDX;
      int ENDY;
      TileMath.GetDrawArrayLimits(out StartX, out StartY, out ENDX, out ENDY);
      if (ENDX > AnimalPrivacyMap.PrivacyMapData.GetLength(0))
        ENDX = AnimalPrivacyMap.PrivacyMapData.GetLength(0);
      if (ENDY > AnimalPrivacyMap.PrivacyMapData.GetLength(1))
        ENDY = AnimalPrivacyMap.PrivacyMapData.GetLength(1);
      for (int _X = StartX; _X < ENDX; ++_X)
      {
        for (int _Y = StartY; _Y < ENDY; ++_Y)
        {
          this.TempLoc.X = _X;
          this.TempLoc.Y = _Y;
          if (this.SelectedLocation != null && this.SelectedLocation.X != _X && this.SelectedLocation.Y != _Y)
          {
            this.BaseTile.fAlpha = 0.55f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else if (AnimalPrivacyMap.PrivacyMapData[_X, _Y] > 0)
          {
            this.BaseTile.fAlpha = 0.9f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
            this.HeatTile.fAlpha = (float) (0.200000002980232 + (double) AnimalPrivacyMap.PrivacyMapData[_X, _Y] * 0.00999999977648258 * 0.800000011920929);
            this.HeatTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.HeatTile.SetAllColours(0.0f, 0.8078431f, 0.8196079f);
            this.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else if (AnimalPrivacyMap.PrivacyMapData[_X, _Y] <= -1000)
          {
            this.BaseTile.fAlpha = 0.9f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
            this.HeatTile.fAlpha = 1f;
            float num1 = 1f;
            if (AnimalPrivacyMap.PrivacyMapData[_X, _Y] < -1000)
            {
              int num2 = AnimalPrivacyMap.PrivacyMapData[_X, _Y] + 1100;
              this.HeatTile.fAlpha = (float) num2 * 0.01f;
              num1 = (float) (0.649999976158142 + (double) num2 * 0.00999999977648258 * 0.349999994039536);
            }
            this.HeatTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.HeatTile.SetAllColours(0.9f, 0.9f * num1, 0.1f);
            this.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else if (AnimalPrivacyMap.PrivacyMapData[_X, _Y] < 0)
          {
            this.BaseTile.fAlpha = 0.9f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
            this.HeatTile.fAlpha = (float) (0.400000005960464 + (double) AnimalPrivacyMap.PrivacyMapData[_X, _Y] * 0.00999999977648258 * -0.600000023841858);
            this.HeatTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.HeatTile.SetAllColours(0.6f, 0.0f, 0.4f);
            this.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else
          {
            this.BaseTile.fAlpha = 0.55f;
            this.BaseTile.scale = 16f;
            this.BaseTile.SetAllColours(1f, 1f, 1f);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          if (this.SelectedLocation != null && this.SelectedLocation.X == _X && this.SelectedLocation.Y == _Y)
          {
            this.BaseTile.SetAllColours(0.5f, 1f, 0.2f);
            this.BaseTile.fAlpha = (float) ((double) FlashingAlpha.Medium.fAlpha * 0.5 + 0.5);
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
        }
      }
    }
  }
}
