// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.NodeList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PathFinding
{
  internal class NodeList
  {
    public List<PathNode> pathnodes;
    private Vector2Int TargetNode;

    public NodeList(Vector2Int _TargetNode, List<PathNode> nodes)
    {
      this.pathnodes = nodes;
      this.TargetNode = _TargetNode;
    }

    public static int SortNodeList(NodeList A, NodeList B)
    {
      if (A.pathnodes.Count > B.pathnodes.Count)
        return -1;
      return A.pathnodes.Count < B.pathnodes.Count ? 1 : 0;
    }
  }
}
