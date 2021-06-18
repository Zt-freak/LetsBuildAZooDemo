// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.WaterMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_BalanceSystems;

namespace TinyZoo.Z_HeatMaps
{
  internal class WaterMap
  {
    private GameObject HeatTile;
    private GameObject BaseTile;
    private Vector2Int TempLoc;
    private static int[,] WaterMapData;
    private static Vector2Int LOC = new Vector2Int();

    public WaterMap(Player player)
    {
      this.TempLoc = new Vector2Int();
      this.HeatTile = new GameObject();
      this.HeatTile.scale = 15f;
      this.HeatTile.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.HeatTile.SetDrawOriginToCentre();
      this.HeatTile.SetAllColours(1f, 1f, 1f);
      this.BaseTile = new GameObject(this.HeatTile);
      if (WaterMap.WaterMapData != null)
        return;
      WaterMap.RecreateWaterMap(player);
    }

    internal static void RecreateWaterMap(Player player)
    {
      WaterMap.WaterMapData = new int[TileMath.GetOverWorldMapSize_XDefault(), TileMath.GetOverWorldMapSize_YSize()];
      for (int index1 = 0; index1 < player.prisonlayout.Facilities.WaterPumps.Count; ++index1)
      {
        Vector2 vector2 = new Vector2((float) player.prisonlayout.Facilities.WaterPumps[index1].Location.X, (float) player.prisonlayout.Facilities.WaterPumps[index1].Location.Y);
        for (int index2 = player.prisonlayout.Facilities.WaterPumps[index1].Location.X - Z_Facilities.WaterRadius; index2 < player.prisonlayout.Facilities.WaterPumps[index1].Location.X + Z_Facilities.WaterRadius; ++index2)
        {
          for (int index3 = player.prisonlayout.Facilities.WaterPumps[index1].Location.Y - Z_Facilities.WaterRadius; index3 < player.prisonlayout.Facilities.WaterPumps[index1].Location.Y + Z_Facilities.WaterRadius; ++index3)
          {
            if (index2 > -1 && index2 < WaterMap.WaterMapData.GetLength(0) && (index3 > -1 && index3 < WaterMap.WaterMapData.GetLength(1)) && (double) (vector2 - new Vector2((float) index2, (float) index3)).Length() < (double) Z_Facilities.WaterRadius)
              ++WaterMap.WaterMapData[index2, index3];
          }
        }
      }
    }

    public void UpdateWaterMap(Player player)
    {
      if (!Z_GameFlags.MustRebuildWaterMap)
        return;
      Z_GameFlags.MustRebuildWaterMap = false;
      WaterMap.RecreateWaterMap(player);
    }

    public bool GetHasWaterAccess(int XLoc, int YLoc) => XLoc > -1 && YLoc > -1 && (XLoc < WaterMap.WaterMapData.GetLength(0) && YLoc < WaterMap.WaterMapData.GetLength(1)) && WaterMap.WaterMapData[XLoc, YLoc] > 0;

    public void DrawWaterMap()
    {
      if (WaterMap.WaterMapData == null)
        return;
      int StartX;
      int StartY;
      int ENDX;
      int ENDY;
      TileMath.GetDrawArrayLimits(out StartX, out StartY, out ENDX, out ENDY);
      if (ENDX > WaterMap.WaterMapData.GetLength(0))
        ENDX = WaterMap.WaterMapData.GetLength(0);
      if (ENDY > WaterMap.WaterMapData.GetLength(1))
        ENDY = WaterMap.WaterMapData.GetLength(1);
      for (int _X = StartX; _X < ENDX; ++_X)
      {
        for (int _Y = StartY; _Y < ENDY; ++_Y)
        {
          this.TempLoc.X = _X;
          this.TempLoc.Y = _Y;
          if (WaterMap.WaterMapData[_X, _Y] > 0)
          {
            this.HeatTile.fAlpha = (float) (0.5 + ((double) WaterMap.WaterMapData[_X, _Y] - 1.0) * 0.100000001490116);
            this.HeatTile.vLocation = TileMath.GetTileToWorldSpace(new Vector2Int(_X, _Y));
            float num = 0.4f - (float) (((double) WaterMap.WaterMapData[_X, _Y] - 1.0) * 0.100000001490116);
            this.HeatTile.SetAllColours(num, num, 1f);
            this.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
          else
          {
            this.BaseTile.fAlpha = 0.4f;
            WaterMap.LOC.X = _X;
            WaterMap.LOC.Y = _Y;
            this.BaseTile.vLocation = TileMath.GetTileToWorldSpace(WaterMap.LOC);
            this.BaseTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
        }
      }
    }
  }
}
