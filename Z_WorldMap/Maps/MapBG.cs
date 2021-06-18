// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WorldMap.Maps.MapBG
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_WorldMap.Maps
{
  internal class MapBG : GameObject
  {
    public MapBG()
    {
      this.DrawOrigin = new Vector2(8f, 8f);
      this.DrawRect = new Rectangle(0, 0, 1024, 1024);
    }

    public void UpdateMapBG()
    {
    }

    public void DrawMapBG() => this.WorldOffsetDraw(AssetContainer.pointspritebatch0, AssetContainer.WorldSheet);
  }
}
