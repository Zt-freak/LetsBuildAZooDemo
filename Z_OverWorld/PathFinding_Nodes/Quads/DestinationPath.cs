// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads.DestinationPath
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PathFinding;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads
{
  internal class DestinationPath
  {
    public List<PathNode> Path;
    public List<PathNode> GlobalPath;
    public LocationNode locationnodepointer;

    public DestinationPath(List<PathNode> _Path, LocationNode locationnode)
    {
      this.Path = _Path;
      this.locationnodepointer = locationnode;
    }

    public List<PathNode> GetWorldSpacePath(Vector2Int TopLeft)
    {
      if (this.GlobalPath == null)
      {
        this.GlobalPath = new List<PathNode>();
        for (int index = 0; index < this.Path.Count; ++index)
          this.GlobalPath.Add(new PathNode(this.Path[index].Location.X, this.Path[index].Location.Y));
      }
      return this.GlobalPath;
    }
  }
}
