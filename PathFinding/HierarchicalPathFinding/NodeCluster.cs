// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.HierarchicalPathFinding.NodeCluster
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;

namespace TinyZoo.PathFinding.HierarchicalPathFinding
{
  public class NodeCluster
  {
    public int layer;
    public int x;
    public int y;
    public HashSet<HierarchicalNode> nodes = new HashSet<HierarchicalNode>();
  }
}
