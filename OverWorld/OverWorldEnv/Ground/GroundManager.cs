// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Ground.GroundManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Fog;
using TinyZoo.Z_OverWorld._OverWorldEnv.PhotoModeSurround;
using TinyZoo.Z_Threading;

namespace TinyZoo.OverWorld.OverWorldEnv.Ground
{
  internal class GroundManager
  {
    private List<MoonBase> moonbases;
    private FogLayer groudnforphotomode;
    private PhotoModeSurroundRenderer photmoderenderer;

    public GroundManager()
    {
      this.moonbases = new List<MoonBase>();
      float num = (float) ((double) (TileMath.GetOverWorldMapSize_XDefault() + 1) * (double) TileMath.TileSize / 256.0);
      for (int IndexX = -1; (double) IndexX < (double) num + 3.0; ++IndexX)
      {
        for (int INDEXY = -1; (double) INDEXY < (double) num + 3.0; ++INDEXY)
        {
          if (IndexX > 0 && INDEXY > 0 && ((double) INDEXY < (double) num + 1.0 && (double) IndexX < (double) num + 2.0))
            this.moonbases.Add(new MoonBase(IndexX, INDEXY, true));
        }
      }
      this.photmoderenderer = new PhotoModeSurroundRenderer();
      this.groudnforphotomode = new FogLayer(0);
      this.groudnforphotomode.DrawRect = new Rectangle(0, 0, 256, 256);
      this.groudnforphotomode.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
      this.groudnforphotomode.SetAllColours(1f, 1f, 1f);
    }

    public void UpdateGroundManager()
    {
    }

    public void DrawGroundManager()
    {
      if (TrailerDemoFlags.HasTrailerFlag && TrailerDemoFlags.SkipDrawFloor)
      {
        ThreadFlags.THREAD_UnderGroundDraw = true;
      }
      else
      {
        if (GameFlags.PhotoMode)
          this.photmoderenderer.DrawPhotoModeSurroundRenderer();
        for (int index = 0; index < this.moonbases.Count; ++index)
          this.moonbases[index].DrawMoonBase();
        ThreadFlags.THREAD_UnderGroundDraw = true;
      }
    }
  }
}
