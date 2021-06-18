// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment.PerchData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment
{
  internal class PerchData
  {
    private static List<PerchInfo> AllPerchInfo;

    internal static PerchInfo GetPerchData(TILETYPE tiletype)
    {
      if (PerchData.AllPerchInfo == null)
        PerchData.AllPerchInfo = new List<PerchInfo>();
      for (int index = 0; index < PerchData.AllPerchInfo.Count; ++index)
      {
        if (PerchData.AllPerchInfo[index].TileType == tiletype)
          return PerchData.AllPerchInfo[index];
      }
      PerchInfo perchInfo;
      switch (tiletype)
      {
        case TILETYPE.Enrichment_WoodenLogs:
          perchInfo = new PerchInfo(tiletype, new Vector2(-16f, -4f));
          perchInfo.AddPoint(new Vector2(1f, -18f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, -4f), 2);
          perchInfo.AddPoint(new Vector2(-1f, -18f), 3);
          break;
        case TILETYPE.Enrichment_YellowRockPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-16f, -11f));
          perchInfo.AddPoint(new Vector2(-2f, -10f), 0);
          perchInfo.AddPoint(new Vector2(-1f, -22f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, -9f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, -10f), 2);
          perchInfo.AddPoint(new Vector2(-15f, -11f), 2);
          perchInfo.AddPoint(new Vector2(2f, -22f), 3);
          perchInfo.AddPoint(new Vector2(0.0f, -9f), 3);
          break;
        case TILETYPE.Enrichment_HighPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(3f, -23f));
          perchInfo.AddPoint(new Vector2(-4f, -8f), 0);
          perchInfo.AddPoint(new Vector2(-4f, -19f), 1);
          perchInfo.AddPoint(new Vector2(-18f, -7f), 1);
          perchInfo.AddPoint(new Vector2(-3f, -21f), 2);
          perchInfo.AddPoint(new Vector2(3f, -4f), 2);
          perchInfo.AddPoint(new Vector2(-19f, -19f), 3);
          perchInfo.AddPoint(new Vector2(-5f, -7f), 3);
          break;
        case TILETYPE.Enrichment_HangingCarTire:
          perchInfo = new PerchInfo(tiletype, new Vector2(0.0f, 2f));
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 2);
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 3);
          break;
        case TILETYPE.Enrichment_HighWoodBeamPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-17f, -24f));
          perchInfo.AddPoint(new Vector2(-2f, -18f), 0);
          perchInfo.AddPoint(new Vector2(-17f, -6f), 0);
          perchInfo.AddPoint(new Vector2(4f, -27f), 1);
          perchInfo.AddPoint(new Vector2(-5f, -17f), 1);
          perchInfo.AddPoint(new Vector2(4f, -9f), 1);
          perchInfo.AddPoint(new Vector2(2f, -25f), 2);
          perchInfo.AddPoint(new Vector2(-16f, -14f), 2);
          perchInfo.AddPoint(new Vector2(-4f, -26f), 3);
          perchInfo.AddPoint(new Vector2(6f, -10f), 3);
          break;
        case TILETYPE.Enrichment_RockCliff:
          perchInfo = new PerchInfo(tiletype, new Vector2(-12f, -40f));
          perchInfo.AddPoint(new Vector2(6f, -28f), 0);
          perchInfo.AddPoint(new Vector2(-12f, -10f), 0);
          perchInfo.AddPoint(new Vector2(-10f, -50f), 1);
          perchInfo.AddPoint(new Vector2(-9f, -35f), 1);
          perchInfo.AddPoint(new Vector2(-12f, -10f), 1);
          perchInfo.AddPoint(new Vector2(12f, -40f), 2);
          perchInfo.AddPoint(new Vector2(-6f, -28f), 2);
          perchInfo.AddPoint(new Vector2(12f, -10f), 2);
          perchInfo.AddPoint(new Vector2(-10f, -50f), 3);
          perchInfo.AddPoint(new Vector2(-7f, -35f), 3);
          perchInfo.AddPoint(new Vector2(-4f, -10f), 3);
          break;
        case TILETYPE.Enrichment_RockPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-3f, -14f));
          perchInfo.AddPoint(new Vector2(-12f, -8f), 0);
          perchInfo.AddPoint(new Vector2(-4f, -18f), 1);
          perchInfo.AddPoint(new Vector2(4f, -8f), 1);
          perchInfo.AddPoint(new Vector2(-14f, -14f), 2);
          perchInfo.AddPoint(new Vector2(-5f, -8f), 2);
          perchInfo.AddPoint(new Vector2(5f, -18f), 3);
          perchInfo.AddPoint(new Vector2(-3f, -8f), 3);
          break;
        case TILETYPE.Enrichment_FlatCarTire:
          perchInfo = new PerchInfo(tiletype, new Vector2(0.0f, 2f));
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 2);
          perchInfo.AddPoint(new Vector2(0.0f, 2f), 3);
          break;
        case TILETYPE.Enrichment_IceCliff:
          perchInfo = new PerchInfo(tiletype, new Vector2(12f, -39f));
          perchInfo.AddPoint(new Vector2(-9f, -25f), 0);
          perchInfo.AddPoint(new Vector2(3f, -11f), 0);
          perchInfo.AddPoint(new Vector2(-14f, -39f), 1);
          perchInfo.AddPoint(new Vector2(1f, -20f), 1);
          perchInfo.AddPoint(new Vector2(-16f, -16f), 1);
          perchInfo.AddPoint(new Vector2(-6f, -39f), 2);
          perchInfo.AddPoint(new Vector2(13f, -25f), 2);
          perchInfo.AddPoint(new Vector2(1f, -11f), 2);
          perchInfo.AddPoint(new Vector2(-6f, -39f), 3);
          perchInfo.AddPoint(new Vector2(-16f, -20f), 3);
          perchInfo.AddPoint(new Vector2(0.0f, -16f), 3);
          break;
        case TILETYPE.Enrichment_BrownCliff:
          perchInfo = new PerchInfo(tiletype, new Vector2(12f, -39f));
          perchInfo.AddPoint(new Vector2(-9f, -25f), 0);
          perchInfo.AddPoint(new Vector2(3f, -11f), 0);
          perchInfo.AddPoint(new Vector2(-14f, -39f), 1);
          perchInfo.AddPoint(new Vector2(1f, -20f), 1);
          perchInfo.AddPoint(new Vector2(-16f, -16f), 1);
          perchInfo.AddPoint(new Vector2(-6f, -39f), 2);
          perchInfo.AddPoint(new Vector2(13f, -25f), 2);
          perchInfo.AddPoint(new Vector2(1f, -11f), 2);
          perchInfo.AddPoint(new Vector2(-6f, -39f), 3);
          perchInfo.AddPoint(new Vector2(-16f, -20f), 3);
          perchInfo.AddPoint(new Vector2(0.0f, -16f), 3);
          break;
        case TILETYPE.Enrichment_BrownRockPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-15f, -9f));
          perchInfo.AddPoint(new Vector2(-2f, -6f), 0);
          perchInfo.AddPoint(new Vector2(1f, -22f), 1);
          perchInfo.AddPoint(new Vector2(-3f, -11f), 1);
          perchInfo.AddPoint(new Vector2(-2f, -9f), 2);
          perchInfo.AddPoint(new Vector2(-15f, -6f), 2);
          perchInfo.AddPoint(new Vector2(0.0f, -22f), 3);
          perchInfo.AddPoint(new Vector2(4f, -11f), 3);
          break;
        case TILETYPE.Enrichment_LogPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-3f, -12f));
          perchInfo.AddPoint(new Vector2(-12f, 0.0f), 0);
          perchInfo.AddPoint(new Vector2(-3f, -18f), 1);
          perchInfo.AddPoint(new Vector2(3f, -5f), 1);
          perchInfo.AddPoint(new Vector2(-14f, -12f), 2);
          perchInfo.AddPoint(new Vector2(-5f, -4f), 2);
          perchInfo.AddPoint(new Vector2(4f, -18f), 3);
          perchInfo.AddPoint(new Vector2(-2f, -5f), 3);
          break;
        case TILETYPE.Enrichment_NetPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-7f, 0.0f));
          perchInfo.AddPoint(new Vector2(0.0f, -8f), 1);
          perchInfo.AddPoint(new Vector2(-7f, 0.0f), 2);
          perchInfo.AddPoint(new Vector2(1f, -8f), 3);
          break;
        case TILETYPE.Enrichment_WoodenBeam2:
          perchInfo = new PerchInfo(tiletype, new Vector2(-8f, -7f));
          perchInfo.AddPoint(new Vector2(2f, -19f), 1);
          perchInfo.AddPoint(new Vector2(-9f, -7f), 2);
          perchInfo.AddPoint(new Vector2(-2f, -19f), 3);
          break;
        case TILETYPE.Enrichment_WoodenBeam3:
          perchInfo = new PerchInfo(tiletype, new Vector2(0.0f, -10f));
          perchInfo.AddPoint(new Vector2(-14f, -7f), 0);
          perchInfo.AddPoint(new Vector2(4f, -24f), 1);
          perchInfo.AddPoint(new Vector2(-2f, -8f), 1);
          perchInfo.AddPoint(new Vector2(-14f, -13f), 2);
          perchInfo.AddPoint(new Vector2(-2f, -4f), 2);
          perchInfo.AddPoint(new Vector2(-2f, -24f), 3);
          perchInfo.AddPoint(new Vector2(0.0f, -8f), 3);
          break;
        case TILETYPE.Enrichment_TreeHighPerch:
          perchInfo = new PerchInfo(tiletype, new Vector2(-10f, -36f));
          perchInfo.AddPoint(new Vector2(-19f, -27f), 0);
          perchInfo.AddPoint(new Vector2(0.0f, -25f), 0);
          perchInfo.AddPoint(new Vector2(7f, -37f), 1);
          perchInfo.AddPoint(new Vector2(-5f, -26f), 1);
          perchInfo.AddPoint(new Vector2(5f, -14f), 1);
          perchInfo.AddPoint(new Vector2(-10f, -36f), 2);
          perchInfo.AddPoint(new Vector2(-19f, -27f), 2);
          perchInfo.AddPoint(new Vector2(0.0f, -25f), 2);
          perchInfo.AddPoint(new Vector2(-7f, -37f), 3);
          perchInfo.AddPoint(new Vector2(5f, -26f), 3);
          perchInfo.AddPoint(new Vector2(-5f, -14f), 3);
          break;
        case TILETYPE.Enrichment_HighStriker:
          perchInfo = new PerchInfo(tiletype, new Vector2(0.0f, 0.0f));
          perchInfo.AddPoint(new Vector2(0.0f, 0.0f), 1);
          perchInfo.AddPoint(new Vector2(0.0f, 0.0f), 2, true);
          perchInfo.AddPoint(new Vector2(0.0f, 0.0f), 3);
          break;
        default:
          throw new Exception();
      }
      PerchData.AllPerchInfo.Add(perchInfo);
      return perchInfo;
    }
  }
}
