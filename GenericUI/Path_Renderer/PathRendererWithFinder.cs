// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.Path_Renderer.PathRendererWithFinder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.GenericUI.Path_Renderer
{
  internal class PathRendererWithFinder
  {
    private PathRenderer pathrendererForPens;
    private List<PathNode> path;

    public PathRendererWithFinder()
    {
      this.pathrendererForPens = new PathRenderer();
      this.pathrendererForPens.IsActive = false;
      this.pathrendererForPens.pathpiece.SetAllColours(new Vector3(184f, (float) byte.MaxValue, 149f) / (float) byte.MaxValue);
    }

    public void TrySetPathInPrisonZone(
      ThingToBuildFootPrint footprint,
      PrisonZone decoratethisprisonzone,
      Vector2Int tilerendererTileLocation)
    {
      if (footprint.Entrances != null && footprint.Entrances.Count > 0 && !footprint.SomethingIsBlocked)
        this.TrySetPath(tilerendererTileLocation, decoratethisprisonzone.GetSpaceBehindGate());
      else
        this.pathrendererForPens.IsActive = false;
    }

    public void TrySetPath(Vector2Int Start, Vector2Int End)
    {
      int x = Start.X;
      this.path = Z_GameFlags.pathfinder.GetFullPathToLocation(Start, End, false, true, GameFlags.pathset);
      this.pathrendererForPens.IsActive = this.path != null && this.path.Count > 0;
    }

    public void DrawPathRendererWithFinder()
    {
      if (!this.pathrendererForPens.IsActive)
        return;
      this.pathrendererForPens.DrawPathRenderer(this.path, (Vector2Int) null, Vector2.Zero, 0.0f, true);
    }
  }
}
