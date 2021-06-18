// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HeatMaps.WaterInfluenceZone
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_BalanceSystems;

namespace TinyZoo.Z_HeatMaps
{
  internal class WaterInfluenceZone
  {
    private static GameObject HeatTile;
    private static Vector2Int LOC = new Vector2Int();

    public void UpdateDrawWater()
    {
    }

    internal static void DrawDrawWater(Vector2Int PumpLocation)
    {
      if (WaterInfluenceZone.HeatTile == null)
      {
        WaterInfluenceZone.HeatTile = new GameObject();
        WaterInfluenceZone.HeatTile.scale = 15f;
        WaterInfluenceZone.HeatTile.DrawRect = TinyZoo.Game1.WhitePixelRect;
        WaterInfluenceZone.HeatTile.SetDrawOriginToCentre();
        WaterInfluenceZone.HeatTile.SetAllColours(1f, 1f, 1f);
        WaterInfluenceZone.HeatTile.fAlpha = 0.4f;
        WaterInfluenceZone.HeatTile.SetAllColours(0.5f, 0.5f, 1f);
      }
      Vector2 vector2 = new Vector2((float) PumpLocation.X, (float) PumpLocation.Y);
      for (int index1 = PumpLocation.X - Z_Facilities.WaterRadius; index1 < PumpLocation.X + Z_Facilities.WaterRadius; ++index1)
      {
        for (int index2 = PumpLocation.Y - Z_Facilities.WaterRadius; index2 < PumpLocation.Y + Z_Facilities.WaterRadius; ++index2)
        {
          if (index1 > -1 && index1 < TileMath.GetOverWorldMapSize_XDefault() && (index2 > -1 && index2 < TileMath.GetOverWorldMapSize_YSize()) && (double) (vector2 - new Vector2((float) index1, (float) index2)).Length() < (double) Z_Facilities.WaterRadius)
          {
            WaterInfluenceZone.LOC.X = index1;
            WaterInfluenceZone.LOC.Y = index2;
            WaterInfluenceZone.HeatTile.vLocation = TileMath.GetTileToWorldSpace(WaterInfluenceZone.LOC);
            WaterInfluenceZone.HeatTile.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
          }
        }
      }
    }
  }
}
