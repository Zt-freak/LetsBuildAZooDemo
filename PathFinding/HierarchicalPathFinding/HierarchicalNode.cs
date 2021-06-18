// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.HierarchicalPathFinding.HierarchicalNode
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TinyZoo.PathFinding.HierarchicalPathFinding
{
  public class HierarchicalNode : IBinaryHeapItem<HierarchicalNode>, IComparable<HierarchicalNode>
  {
    public int layer;
    public NodeCluster cluster;
    public HierarchicalNode supernode;
    public HashSet<HierarchicalNode> subnodes;
    public Dictionary<Tuple<HierarchicalNode, HierarchicalNode>, List<HierarchicalNode>> paths;
    public Dictionary<HierarchicalNode, HashSet<HierarchicalNode>> neighbours;
    private HashSet<HierarchicalNode> edgenodes_TMP;
    public Dictionary<HierarchicalNode, HashSet<HierarchicalNode>> warplinks;
    private HashSet<HierarchicalNode> warpnodes_TMP;
    public Vector2 location;
    public float fCost;
    public float gCost;
    public float hCost;
    public HierarchicalNode parent;
    private int heapIndex;

    internal void AddWarpnode(HierarchicalNode warpnode) => this.warpnodes_TMP.Add(warpnode);

    internal void AddEdgenode(HierarchicalNode edgenode) => this.edgenodes_TMP.Add(edgenode);

    public int binaryHeapIndex
    {
      get => this.heapIndex;
      set => this.heapIndex = value;
    }

    public int CompareTo(HierarchicalNode rhs) => (double) this.fCost == (double) rhs.fCost ? this.hCost.CompareTo(rhs.hCost) : this.fCost.CompareTo(rhs.fCost);

    public HierarchicalNode()
    {
      this.neighbours = new Dictionary<HierarchicalNode, HashSet<HierarchicalNode>>();
      this.subnodes = new HashSet<HierarchicalNode>();
      this.edgenodes_TMP = new HashSet<HierarchicalNode>();
      this.paths = new Dictionary<Tuple<HierarchicalNode, HierarchicalNode>, List<HierarchicalNode>>();
      this.warplinks = new Dictionary<HierarchicalNode, HashSet<HierarchicalNode>>();
      this.warpnodes_TMP = new HashSet<HierarchicalNode>();
    }

    public float GetDistanceManhattan(HierarchicalNode other) => Math.Abs(this.location.X - other.location.X) + Math.Abs(this.location.Y - other.location.Y);

    public float GetDistanceEuclidean(HierarchicalNode other) => (this.location - other.location).Length();

    public void ConnectNeighboursBasedOnSubnodes()
    {
      foreach (HierarchicalNode hierarchicalNode in this.edgenodes_TMP)
      {
        foreach (HierarchicalNode key in hierarchicalNode.neighbours.Keys)
        {
          if (key.supernode != this)
          {
            HierarchicalNode supernode = key.supernode;
            if (!this.neighbours.ContainsKey(supernode))
              this.neighbours[supernode] = new HashSet<HierarchicalNode>();
            this.neighbours[supernode].Add(hierarchicalNode);
            if (!supernode.neighbours.ContainsKey(this))
              supernode.neighbours[this] = new HashSet<HierarchicalNode>();
            supernode.neighbours[this].Add(key);
          }
        }
      }
      foreach (HierarchicalNode hierarchicalNode in this.warpnodes_TMP)
      {
        foreach (HierarchicalNode key in hierarchicalNode.warplinks.Keys)
        {
          if (key.supernode != this)
          {
            HierarchicalNode supernode = key.supernode;
            if (!this.warplinks.ContainsKey(supernode))
              this.warplinks[supernode] = new HashSet<HierarchicalNode>();
            this.warplinks[supernode].Add(hierarchicalNode);
            if (!supernode.warplinks.ContainsKey(this))
              supernode.warplinks[this] = new HashSet<HierarchicalNode>();
            supernode.warplinks[this].Add(key);
          }
        }
      }
    }
  }
}
