// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.DuplicatePlacer.DuplicatePlaceManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.DuplicatePlacer
{
  internal class DuplicatePlaceManager
  {
    private List<Vector2Int> locations;

    public DuplicatePlaceManager(TILETYPE placethis) => this.locations = new List<Vector2Int>();

    public void UpdateDuplicatePlaceManager(Player player, float DeltaTime)
    {
    }

    public void DrawDuplicatePlaceManager()
    {
    }
  }
}
