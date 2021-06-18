// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.HierarchicalPathFinding.HierarchicalPathFind
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TinyZoo.PathFinding.HierarchicalPathFinding
{
  internal class HierarchicalPathFind
  {
    private List<NodeCluster[,]> hierarchy;
    private int numlayers;
    private int clustersize;
    private bool preferstraight;
    private List<HashSet<HierarchicalNode>> needsPathRecache;
    private static BinaryHeap<HierarchicalNode> openSet;

    public HierarchicalPathFind()
    {
      this.hierarchy = new List<NodeCluster[,]>();
      this.needsPathRecache = new List<HashSet<HierarchicalNode>>();
    }

    public void ConstructHierarchy(
      PathNode[,] grid,
      int numLayers = 2,
      int clusterSize = 10,
      bool allowDiagonals = false,
      bool preferStraight = false)
    {
      this.numlayers = numLayers;
      this.clustersize = clusterSize;
      this.preferstraight = preferStraight;
      int length1 = grid.GetLength(0);
      int length2 = grid.GetLength(1);
      this.hierarchy = new List<NodeCluster[,]>();
      this.needsPathRecache = new List<HashSet<HierarchicalNode>>();
      HierarchicalPathFind.openSet = new BinaryHeap<HierarchicalNode>(length1 * length2 / 2);
      for (int index1 = 0; index1 < this.numlayers; ++index1)
      {
        length1 = (int) Math.Ceiling((double) length1 / (double) this.clustersize);
        length2 = (int) Math.Ceiling((double) length2 / (double) this.clustersize);
        this.hierarchy.Add(new NodeCluster[length1, length2]);
        for (int index2 = 0; index2 < length2; ++index2)
        {
          for (int index3 = 0; index3 < length1; ++index3)
            this.hierarchy[index1][index3, index2] = new NodeCluster()
            {
              layer = index1,
              x = index3,
              y = index2
            };
        }
        this.needsPathRecache.Add(new HashSet<HierarchicalNode>());
      }
      this.ConstructNodesOnGrid(grid);
      this.ConnectNodesOnGrid(grid, this.clustersize, allowDiagonals);
      for (int index = 1; index < this.numlayers; ++index)
      {
        this.ConstructLayerFromSublayer(this.hierarchy[index - 1], index);
        this.ConnectLayerNodesBasedOnSublayer(this.hierarchy[index], index);
        this.CacheSubnodePaths(this.hierarchy[index]);
      }
    }

    public void ConstructNodesOnGrid(PathNode[,] grid)
    {
      int length1 = grid.GetLength(0);
      int length2 = grid.GetLength(1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
        {
          PathNode pathNode = grid[index2, index1];
          if (!pathNode.IsBlocking)
          {
            HierarchicalNode hierarchicalNode = new HierarchicalNode();
            hierarchicalNode.location.X = (float) index2;
            hierarchicalNode.location.Y = (float) index1;
            hierarchicalNode.layer = 0;
            NodeCluster nodeCluster = this.hierarchy[0][index2 / this.clustersize, index1 / this.clustersize];
            nodeCluster.nodes.Add(hierarchicalNode);
            hierarchicalNode.cluster = nodeCluster;
            pathNode.hierarchicalnode = hierarchicalNode;
          }
        }
      }
    }

    public void ConnectNodesOnGrid(PathNode[,] grid, int clustersize, bool allowDiagonals = false)
    {
      int length1 = grid.GetLength(0);
      int length2 = grid.GetLength(1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
        {
          PathNode pathNode1 = grid[index2, index1];
          if (!pathNode1.IsBlocking)
          {
            HierarchicalNode hierarchicalnode = pathNode1.hierarchicalnode;
            for (int index3 = -1; index3 < 2; ++index3)
            {
              for (int index4 = -1; index4 < 2; ++index4)
              {
                if (index4 != 0 || index3 != 0)
                {
                  bool flag = (uint) (index4 * index3) > 0U;
                  if (!(!allowDiagonals & flag))
                  {
                    int index5 = index2 + index4;
                    int index6 = index1 + index3;
                    if (index5 >= 0 && index5 < length1 && (index6 >= 0 && index6 < length2))
                    {
                      PathNode pathNode2 = grid[index5, index6];
                      if (!pathNode2.IsBlocking)
                        hierarchicalnode.neighbours.Add(pathNode2.hierarchicalnode, new HashSet<HierarchicalNode>());
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    public void ConnectLayerNodesBasedOnSublayer(NodeCluster[,] layer, int layerindex)
    {
      int length1 = layer.GetLength(0);
      int length2 = layer.GetLength(1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
        {
          foreach (HierarchicalNode node in layer[index2, index1].nodes)
            node.ConnectNeighboursBasedOnSubnodes();
        }
      }
    }

    public void ConstructLayerFromSublayer(NodeCluster[,] sublayer, int layerindex)
    {
      for (int index1 = 0; index1 < sublayer.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < sublayer.GetLength(0); ++index2)
          this.ClusterFlood(sublayer[index2, index1]);
      }
    }

    public HashSet<HierarchicalNode> ClusterFlood(NodeCluster cluster)
    {
      HashSet<HierarchicalNode> closedSet = new HashSet<HierarchicalNode>();
      HashSet<HierarchicalNode> hierarchicalNodeSet = new HashSet<HierarchicalNode>();
      foreach (HierarchicalNode node in cluster.nodes)
      {
        if (!closedSet.Contains(node))
        {
          int index1 = cluster.x / this.clustersize;
          int index2 = cluster.y / this.clustersize;
          NodeCluster nodeCluster = this.hierarchy[cluster.layer + 1][index1, index2];
          HierarchicalNode supernode = new HierarchicalNode();
          supernode.location.X = (float) cluster.x;
          supernode.location.Y = (float) cluster.y;
          supernode.layer = nodeCluster.layer;
          supernode.cluster = nodeCluster;
          nodeCluster.nodes.Add(supernode);
          this.ClusterFloodRecursive(node, supernode, cluster, closedSet);
          hierarchicalNodeSet.Add(supernode);
        }
      }
      return hierarchicalNodeSet;
    }

    public void ClusterFloodRecursive(
      HierarchicalNode node,
      HierarchicalNode supernode,
      NodeCluster cluster,
      HashSet<HierarchicalNode> closedSet)
    {
      closedSet.Add(node);
      node.supernode = supernode;
      supernode.subnodes.Add(node);
      if (node.warplinks.Count > 0)
        supernode.AddWarpnode(node);
      foreach (HierarchicalNode key in node.neighbours.Keys)
      {
        if (!cluster.nodes.Contains(key))
          supernode.AddEdgenode(node);
        else if (!closedSet.Contains(key))
          this.ClusterFloodRecursive(key, supernode, cluster, closedSet);
      }
    }

    public void CacheSubnodePaths(NodeCluster[,] layer)
    {
      int length1 = layer.GetLength(0);
      int length2 = layer.GetLength(1);
      for (int index1 = 0; index1 < length2; ++index1)
      {
        for (int index2 = 0; index2 < length1; ++index2)
        {
          foreach (HierarchicalNode node in layer[index2, index1].nodes)
            this.CacheSubnodePaths(node);
        }
      }
    }

    public void CacheSubnodePaths(HierarchicalNode node)
    {
      List<HashSet<HierarchicalNode>> list = node.neighbours.Values.ToList<HashSet<HierarchicalNode>>();
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        HashSet<HierarchicalNode> hierarchicalNodeSet1 = list[index1];
        for (int index2 = index1 + 1; index2 < list.Count; ++index2)
        {
          HashSet<HierarchicalNode> hierarchicalNodeSet2 = list[index2];
          foreach (HierarchicalNode startNode in hierarchicalNodeSet1)
          {
            foreach (HierarchicalNode targetNode in hierarchicalNodeSet2)
            {
              if (startNode != targetNode)
              {
                List<HierarchicalNode> hierarchicalNodeList1 = this.AStar(startNode, targetNode, true);
                if (hierarchicalNodeList1 != null)
                {
                  hierarchicalNodeList1.RemoveAt(hierarchicalNodeList1.Count - 1);
                  node.paths[new Tuple<HierarchicalNode, HierarchicalNode>(startNode, targetNode)] = hierarchicalNodeList1;
                  List<HierarchicalNode> hierarchicalNodeList2 = new List<HierarchicalNode>((IEnumerable<HierarchicalNode>) hierarchicalNodeList1);
                  hierarchicalNodeList2.Reverse();
                  node.paths[new Tuple<HierarchicalNode, HierarchicalNode>(targetNode, startNode)] = hierarchicalNodeList2;
                }
              }
            }
          }
        }
      }
    }

    public void FenceNode(
      PathNode[,] grid,
      int x,
      int y,
      bool north,
      bool east,
      bool south,
      bool west,
      bool delayRepathing = true)
    {
      PathNode pathNode = grid[x, y];
      HierarchicalNode hierarchicalnode = pathNode.hierarchicalnode;
      if (pathNode.IsBlocking)
        return;
      foreach (HierarchicalNode key in new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) hierarchicalnode.neighbours.Keys))
      {
        bool flag = false;
        if ((int) key.location.X == (int) hierarchicalnode.location.X)
        {
          if ((int) key.location.Y == (int) hierarchicalnode.location.Y - 1)
          {
            if (north)
              flag = true;
          }
          else if ((int) key.location.Y == (int) hierarchicalnode.location.Y + 1 && south)
            flag = true;
        }
        else if ((int) key.location.Y == (int) hierarchicalnode.location.Y)
        {
          if ((int) key.location.X == (int) hierarchicalnode.location.X - 1)
          {
            if (west)
              flag = true;
          }
          else if ((int) key.location.X == (int) hierarchicalnode.location.X + 1 && east)
            flag = true;
        }
        if (flag)
        {
          key.neighbours.Remove(hierarchicalnode);
          hierarchicalnode.neighbours.Remove(key);
        }
      }
      this.RecontructLocalHierarchy(hierarchicalnode.cluster.x, hierarchicalnode.cluster.y);
      if (delayRepathing)
        return;
      this.UpdateCachedPaths();
    }

    public void BlockNode(PathNode[,] grid, int x, int y, bool delayRepathing = false)
    {
      PathNode pathNode = grid[x, y];
      HierarchicalNode hierarchicalnode = pathNode.hierarchicalnode;
      if (pathNode.IsBlocking)
        return;
      foreach (HierarchicalNode hierarchicalNode in new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) hierarchicalnode.neighbours.Keys))
        hierarchicalNode.neighbours.Remove(hierarchicalnode);
      foreach (HierarchicalNode node2 in new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) hierarchicalnode.warplinks.Keys))
        this.BreakWarpLink(hierarchicalnode, node2);
      hierarchicalnode.cluster.nodes.Remove(hierarchicalnode);
      pathNode.hierarchicalnode = (HierarchicalNode) null;
      this.RecontructLocalHierarchy(hierarchicalnode.cluster.x, hierarchicalnode.cluster.y);
      if (delayRepathing)
        return;
      this.UpdateCachedPaths();
    }

    private void RecontructLocalHierarchy(int clusterX_, int clusterY_)
    {
      int index1 = 0;
      int index2 = clusterX_;
      int index3 = clusterY_;
      while (index1 < this.numlayers - 1)
      {
        NodeCluster cluster = this.hierarchy[index1][index2, index3];
        foreach (HierarchicalNode key1 in new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) this.hierarchy[index1 + 1][index2 / this.clustersize, index3 / this.clustersize].nodes))
        {
          if ((double) key1.location.X == (double) index2 && (double) key1.location.Y == (double) index3)
          {
            foreach (HierarchicalNode key2 in key1.neighbours.Keys)
              key2.neighbours.Remove(key1);
            foreach (HierarchicalNode subnode in key1.subnodes)
              subnode.supernode = (HierarchicalNode) null;
            foreach (HierarchicalNode key2 in key1.warplinks.Keys)
              key2.warplinks.Remove(key1);
            key1.cluster.nodes.Remove(key1);
            if (this.needsPathRecache[key1.layer].Contains(key1))
              this.needsPathRecache[key1.layer].Remove(key1);
          }
        }
        foreach (HierarchicalNode hierarchicalNode in this.ClusterFlood(cluster))
        {
          hierarchicalNode.ConnectNeighboursBasedOnSubnodes();
          this.needsPathRecache[hierarchicalNode.layer].Add(hierarchicalNode);
        }
        ++index1;
        index2 /= this.clustersize;
        index3 /= this.clustersize;
      }
    }

    public void BreakWarpGateLink(PathNode[,] grid, Vector2Int Start, Vector2Int End)
    {
      HierarchicalNode hierarchicalnode1 = grid[Start.X, Start.Y].hierarchicalnode;
      HierarchicalNode hierarchicalnode2 = grid[End.X, End.Y].hierarchicalnode;
      if (hierarchicalnode1.supernode == hierarchicalnode2.supernode)
        return;
      this.BreakWarpLink(hierarchicalnode1, hierarchicalnode2);
    }

    private void BreakWarpLink(HierarchicalNode node1, HierarchicalNode node2)
    {
      node1.warplinks.Remove(node2);
      node2.warplinks.Remove(node1);
      if (node1.supernode == node2.supernode)
        return;
      this.BreakWarpLink(node1.supernode, node2.supernode);
    }

    public void CreateWarpGateLink(PathNode[,] grid, Vector2Int Start, Vector2Int End)
    {
      PathNode pathNode1 = grid[Start.X, Start.Y];
      PathNode pathNode2 = grid[End.X, End.Y];
      HierarchicalNode hierarchicalnode1 = pathNode1.hierarchicalnode;
      HierarchicalNode hierarchicalnode2 = pathNode2.hierarchicalnode;
      if (hierarchicalnode1.supernode != hierarchicalnode2.supernode)
        this.CreateSupernodeWarpLink(hierarchicalnode1, hierarchicalnode2);
      hierarchicalnode1.warplinks.Add(hierarchicalnode2, new HashSet<HierarchicalNode>());
      hierarchicalnode2.warplinks.Add(hierarchicalnode1, new HashSet<HierarchicalNode>());
    }

    public void CreateSupernodeWarpLink(HierarchicalNode node1, HierarchicalNode node2)
    {
      HierarchicalNode supernode1 = node1.supernode;
      HierarchicalNode supernode2 = node2.supernode;
      if (supernode1.supernode != supernode2.supernode)
        this.CreateSupernodeWarpLink(supernode1, supernode2);
      if (!supernode1.warplinks.Keys.Contains<HierarchicalNode>(supernode2))
        supernode1.warplinks.Add(supernode2, new HashSet<HierarchicalNode>());
      supernode1.warplinks[supernode2].Add(node1);
      if (!supernode2.warplinks.Keys.Contains<HierarchicalNode>(supernode1))
        supernode2.warplinks.Add(supernode1, new HashSet<HierarchicalNode>());
      supernode2.warplinks[supernode1].Add(node2);
    }

    public void UnblockNode(PathNode[,] grid, int x, int y, bool delayRepathing = false)
    {
      PathNode pathNode1 = grid[x, y];
      if (!pathNode1.IsBlocking)
        return;
      HierarchicalNode hierarchicalNode1 = new HierarchicalNode();
      hierarchicalNode1.location.X = (float) x;
      hierarchicalNode1.location.Y = (float) y;
      hierarchicalNode1.layer = 0;
      NodeCluster nodeCluster1 = this.hierarchy[0][x / this.clustersize, y / this.clustersize];
      nodeCluster1.nodes.Add(hierarchicalNode1);
      hierarchicalNode1.cluster = nodeCluster1;
      pathNode1.hierarchicalnode = hierarchicalNode1;
      int length1 = grid.GetLength(0);
      int length2 = grid.GetLength(1);
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          if ((index2 != 0 || index1 != 0) && (uint) (index2 * index1) <= 0U)
          {
            int index3 = x + index2;
            int index4 = y + index1;
            if (index3 >= 0 && index3 < length1 && (index4 >= 0 && index4 < length2))
            {
              PathNode pathNode2 = grid[index3, index4];
              if (!pathNode2.IsBlocking && (!pathNode2.InternalBlocksToExit_Up_R_D_L[0] || index2 != 0 || index1 != -1) && ((!pathNode2.InternalBlocksToExit_Up_R_D_L[1] || index2 != 1 || index1 != 0) && (!pathNode2.InternalBlocksToExit_Up_R_D_L[2] || index2 != 0 || index1 != 1)) && (!pathNode2.InternalBlocksToExit_Up_R_D_L[3] || index2 != -1 || index1 != 0))
              {
                hierarchicalNode1.neighbours.Add(pathNode2.hierarchicalnode, new HashSet<HierarchicalNode>());
                pathNode2.hierarchicalnode.neighbours.Add(hierarchicalNode1, new HashSet<HierarchicalNode>());
              }
            }
          }
        }
      }
      int num1 = x;
      int num2 = y;
      HierarchicalNode hierarchicalNode2 = hierarchicalNode1;
      for (int index = 0; index < this.numlayers - 1; ++index)
      {
        num1 /= this.clustersize;
        num2 /= this.clustersize;
        foreach (HierarchicalNode key in hierarchicalNode2.neighbours.Keys)
        {
          if (key.cluster == hierarchicalNode2.cluster)
          {
            key.supernode.subnodes.Add(hierarchicalNode2);
            hierarchicalNode2.supernode = key.supernode;
            break;
          }
        }
        if (hierarchicalNode2.supernode == null)
        {
          HierarchicalNode hierarchicalNode3 = new HierarchicalNode();
          hierarchicalNode3.location.X = (float) num1;
          hierarchicalNode3.location.Y = (float) num2;
          hierarchicalNode3.layer = index + 1;
          hierarchicalNode3.subnodes.Add(hierarchicalNode2);
          hierarchicalNode2.supernode = hierarchicalNode3;
          NodeCluster nodeCluster2 = this.hierarchy[index + 1][num1 / this.clustersize, num2 / this.clustersize];
          nodeCluster2.nodes.Add(hierarchicalNode3);
          hierarchicalNode3.cluster = nodeCluster2;
        }
        hierarchicalNode2 = hierarchicalNode2.supernode;
      }
      foreach (HierarchicalNode node2 in new List<HierarchicalNode>((IEnumerable<HierarchicalNode>) hierarchicalNode1.neighbours.Keys))
      {
        if (node2.supernode != hierarchicalNode1.supernode)
        {
          if (node2.cluster == hierarchicalNode1.cluster)
            this.MergeNodes_PropagateUp(hierarchicalNode1.supernode, node2.supernode);
          else
            this.LinkSuperNodes_PropagateUp(hierarchicalNode1, node2);
        }
      }
      if (hierarchicalNode1.supernode == null)
        throw new Exception("supernode is null, something went wrong somewhere because if it was null earlier it should've been created");
      for (HierarchicalNode supernode = hierarchicalNode1.supernode; supernode != null; supernode = supernode.supernode)
        this.needsPathRecache[supernode.layer].Add(supernode);
      if (delayRepathing)
        return;
      this.UpdateCachedPaths();
    }

    private void LinkSuperNodes_PropagateUp(HierarchicalNode node1, HierarchicalNode node2)
    {
      HierarchicalNode supernode1 = node1.supernode;
      HierarchicalNode supernode2 = node2.supernode;
      if (supernode1.supernode != supernode2.supernode)
      {
        if (supernode1.cluster == supernode2.cluster)
          this.MergeNodes_PropagateUp(supernode1.supernode, supernode2.supernode);
        else
          this.LinkSuperNodes_PropagateUp(supernode1, supernode2);
      }
      if (!supernode1.neighbours.ContainsKey(supernode2))
        supernode1.neighbours[supernode2] = new HashSet<HierarchicalNode>();
      supernode1.neighbours[supernode2].Add(node1);
      if (!supernode2.neighbours.ContainsKey(supernode1))
        supernode2.neighbours[supernode1] = new HashSet<HierarchicalNode>();
      supernode2.neighbours[supernode1].Add(node2);
    }

    private void MergeNodes_PropagateUp(HierarchicalNode nommer, HierarchicalNode nommee)
    {
      if (nommer.layer != nommee.layer || nommer.cluster != nommee.cluster || nommer == nommee)
        return;
      if (nommer.supernode != nommee.supernode && nommer.cluster == nommee.cluster)
        this.MergeNodes_PropagateUp(nommer.supernode, nommee.supernode);
      nommer.subnodes.UnionWith((IEnumerable<HierarchicalNode>) nommee.subnodes);
      foreach (HierarchicalNode key in nommee.neighbours.Keys)
      {
        if (nommer.neighbours.ContainsKey(key))
          nommer.neighbours[key].UnionWith((IEnumerable<HierarchicalNode>) nommee.neighbours[key]);
        else
          nommer.neighbours[key] = new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) nommee.neighbours[key]);
      }
      foreach (HierarchicalNode subnode in nommee.subnodes)
        subnode.supernode = nommer;
      foreach (HierarchicalNode key in nommee.neighbours.Keys)
      {
        if (key.neighbours.ContainsKey(nommer))
          key.neighbours[nommer].UnionWith((IEnumerable<HierarchicalNode>) key.neighbours[nommee]);
        else
          key.neighbours[nommer] = new HashSet<HierarchicalNode>((IEnumerable<HierarchicalNode>) key.neighbours[nommee]);
        key.neighbours.Remove(nommee);
      }
      nommee.cluster.nodes.Remove(nommee);
      if (nommee.supernode != null)
      {
        nommee.supernode.subnodes.Remove(nommee);
        foreach (KeyValuePair<HierarchicalNode, HashSet<HierarchicalNode>> neighbour in nommee.supernode.neighbours)
        {
          if (neighbour.Value.Contains(nommee))
          {
            neighbour.Value.Remove(nommee);
            neighbour.Value.Add(nommer);
          }
        }
      }
      this.needsPathRecache[nommee.layer].Remove(nommee);
    }

    public void UpdateCachedPaths()
    {
      foreach (HashSet<HierarchicalNode> hierarchicalNodeSet in this.needsPathRecache)
      {
        foreach (HierarchicalNode node in hierarchicalNodeSet)
          this.CacheSubnodePaths(node);
        hierarchicalNodeSet.Clear();
      }
    }

    public List<HierarchicalNode> FindPath(
      HierarchicalNode startNode,
      HierarchicalNode targetNode,
      bool randomedgenode = true)
    {
      return this.FindPathRecursive(startNode, targetNode, randomedgenode);
    }

    private List<HierarchicalNode> FindPathRecursive(
      HierarchicalNode startNode,
      HierarchicalNode targetNode,
      bool randomedgenode = true)
    {
      if (startNode == null || targetNode == null)
        return (List<HierarchicalNode>) null;
      if (startNode.layer != targetNode.layer)
        return new List<HierarchicalNode>();
      List<HierarchicalNode> hierarchicalNodeList1 = new List<HierarchicalNode>();
      HierarchicalNode startNode1 = startNode;
      if (startNode.supernode != null && targetNode.supernode != null)
      {
        List<HierarchicalNode> pathRecursive = this.FindPathRecursive(startNode.supernode, targetNode.supernode, randomedgenode);
        if (pathRecursive == null)
          return (List<HierarchicalNode>) null;
        HierarchicalNode hierarchicalNode1 = startNode.supernode;
        foreach (HierarchicalNode key1 in pathRecursive)
        {
          HierarchicalNode targetNode1 = (HierarchicalNode) null;
          float num1 = (float) int.MaxValue;
          HashSet<HierarchicalNode> hierarchicalNodeSet1 = new HashSet<HierarchicalNode>();
          if (hierarchicalNode1.warplinks.ContainsKey(key1))
            hierarchicalNodeSet1.UnionWith((IEnumerable<HierarchicalNode>) hierarchicalNode1.warplinks[key1]);
          if (hierarchicalNode1.neighbours.ContainsKey(key1))
            hierarchicalNodeSet1.UnionWith((IEnumerable<HierarchicalNode>) hierarchicalNode1.neighbours[key1]);
          foreach (HierarchicalNode other in hierarchicalNodeSet1)
          {
            float num2 = !randomedgenode || this.preferstraight ? (!this.preferstraight ? startNode1.GetDistanceEuclidean(other) + 0.5f * targetNode.GetDistanceEuclidean(other) : startNode1.GetDistanceEuclidean(other)) : (float) TinyZoo.Game1.Rnd.NextDouble();
            if ((double) num2 < (double) num1)
            {
              num1 = num2;
              targetNode1 = other;
            }
          }
          Tuple<HierarchicalNode, HierarchicalNode> key2 = Tuple.Create<HierarchicalNode, HierarchicalNode>(startNode1, targetNode1);
          if (hierarchicalNode1.paths.ContainsKey(key2))
          {
            hierarchicalNodeList1.AddRange((IEnumerable<HierarchicalNode>) hierarchicalNode1.paths[key2]);
            hierarchicalNodeList1.Add(targetNode1);
          }
          else
          {
            List<HierarchicalNode> hierarchicalNodeList2 = this.AStar(startNode1, targetNode1, true);
            if (hierarchicalNodeList2 == null)
              return (List<HierarchicalNode>) null;
            hierarchicalNodeList1.AddRange((IEnumerable<HierarchicalNode>) hierarchicalNodeList2);
          }
          HashSet<HierarchicalNode> hierarchicalNodeSet2 = new HashSet<HierarchicalNode>();
          hierarchicalNodeSet2.UnionWith((IEnumerable<HierarchicalNode>) targetNode1.neighbours.Keys);
          hierarchicalNodeSet2.UnionWith((IEnumerable<HierarchicalNode>) targetNode1.warplinks.Keys);
          foreach (HierarchicalNode hierarchicalNode2 in hierarchicalNodeSet2)
          {
            if (hierarchicalNode2.supernode == key1)
            {
              hierarchicalNodeList1.Add(hierarchicalNode2);
              break;
            }
          }
          hierarchicalNode1 = key1;
          if (hierarchicalNodeList1.Count > 0)
            startNode1 = hierarchicalNodeList1[hierarchicalNodeList1.Count - 1];
        }
      }
      List<HierarchicalNode> hierarchicalNodeList3 = this.AStar(startNode1, targetNode);
      if (hierarchicalNodeList3 == null)
        return (List<HierarchicalNode>) null;
      hierarchicalNodeList1.AddRange((IEnumerable<HierarchicalNode>) hierarchicalNodeList3);
      return hierarchicalNodeList1;
    }

    public List<HierarchicalNode> AStar(
      HierarchicalNode startNode,
      HierarchicalNode targetNode,
      bool withinCluster = false,
      DiagonalsAllowed diagonalsAllowed = DiagonalsAllowed.NO_DIAGONALS)
    {
      HashSet<HierarchicalNode> hierarchicalNodeSet = new HashSet<HierarchicalNode>();
      HierarchicalPathFind.openSet.PooledItemReset();
      HierarchicalPathFind.openSet.Add(startNode);
      while (HierarchicalPathFind.openSet.Count > 0)
      {
        HierarchicalNode hierarchicalNode = HierarchicalPathFind.openSet.PopRoot();
        hierarchicalNodeSet.Add(hierarchicalNode);
        if (hierarchicalNode == targetNode)
          return this.RetracePath(startNode, targetNode);
        List<HierarchicalNode> hierarchicalNodeList = new List<HierarchicalNode>();
        hierarchicalNodeList.AddRange((IEnumerable<HierarchicalNode>) hierarchicalNode.neighbours.Keys);
        hierarchicalNodeList.AddRange((IEnumerable<HierarchicalNode>) hierarchicalNode.warplinks.Keys);
        foreach (HierarchicalNode other in hierarchicalNodeList)
        {
          if (!hierarchicalNodeSet.Contains(other) && (!withinCluster || other.cluster == hierarchicalNode.cluster))
          {
            int num1 = diagonalsAllowed == DiagonalsAllowed.ALLOW_DIAGONALS ? 1 : (diagonalsAllowed == DiagonalsAllowed.ALLOW_UNBLOCKED ? 1 : 0);
            float num2 = hierarchicalNode.gCost + hierarchicalNode.GetDistanceManhattan(other);
            if (hierarchicalNode.parent != null)
            {
              Vector2 vector2_1 = other.location - hierarchicalNode.location;
              Vector2 vector2_2 = hierarchicalNode.location - hierarchicalNode.parent.location;
              float num3 = (float) ((double) vector2_2.X * (double) vector2_1.X + (double) vector2_2.Y * (double) vector2_1.Y);
              if (this.preferstraight)
              {
                if ((double) Math.Abs(num3) < 1.40129846432482E-45)
                  num2 += 0.3f;
              }
              else if ((double) Math.Abs(1f - num3) < 1.40129846432482E-45)
                num2 += 0.3f;
            }
            if (!HierarchicalPathFind.openSet.Contains(other))
            {
              other.gCost = num2;
              other.hCost = other.GetDistanceEuclidean(targetNode);
              other.fCost = other.gCost + other.hCost;
              other.parent = hierarchicalNode;
              HierarchicalPathFind.openSet.Add(other);
            }
            else if ((double) num2 < (double) other.gCost)
            {
              other.gCost = num2;
              other.parent = hierarchicalNode;
              other.fCost = other.gCost + other.hCost;
              HierarchicalPathFind.openSet.UpdateItem(other.binaryHeapIndex);
            }
          }
        }
      }
      return (List<HierarchicalNode>) null;
    }

    private List<HierarchicalNode> RetracePath(
      HierarchicalNode startNode,
      HierarchicalNode endNode)
    {
      List<HierarchicalNode> hierarchicalNodeList = new List<HierarchicalNode>();
      for (HierarchicalNode hierarchicalNode = endNode; hierarchicalNode != startNode; hierarchicalNode = hierarchicalNode.parent)
        hierarchicalNodeList.Add(hierarchicalNode);
      hierarchicalNodeList.Reverse();
      return hierarchicalNodeList;
    }
  }
}
