// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Animal_Data.AnimatedPerchData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Animal_Data
{
  internal class AnimatedPerchData
  {
    private static AnimatedPerchInfo Enrichment_HighStriker;

    internal static AnimatedPerchInfo GetAnimatedPerchInfo(TILETYPE tiletype)
    {
      if (tiletype != TILETYPE.Enrichment_HighStriker)
        return (AnimatedPerchInfo) null;
      if (AnimatedPerchData.Enrichment_HighStriker == null)
      {
        AnimatedPerchData.Enrichment_HighStriker = new AnimatedPerchInfo(true);
        AnimatedPerchData.Enrichment_HighStriker.StartFastForHighStriker = true;
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(0, 2, new Rectangle(2864, 1898, 18, 54), 9);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(1, 2, new Rectangle(2864, 1845, 18, 52), 9);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(2, 2, new Rectangle(2866, 1953, 18, 48), 9);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(0, 1, new Rectangle(2864, 1898, 18, 54), 6);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(1, 1, new Rectangle(2864, 1845, 18, 52), 6);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(2, 1, new Rectangle(2866, 1953, 18, 48), 6);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(0, 0, new Rectangle(2864, 1898, 18, 54), 4);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(1, 0, new Rectangle(2864, 1845, 18, 52), 4);
        AnimatedPerchData.Enrichment_HighStriker.SetUpWeightStrike(2, 0, new Rectangle(2866, 1953, 18, 48), 4);
      }
      return AnimatedPerchData.Enrichment_HighStriker;
    }
  }
}
