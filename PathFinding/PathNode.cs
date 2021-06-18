// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.PathNode
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding.HierarchicalPathFinding;

namespace TinyZoo.PathFinding
{
  internal class PathNode : IBinaryHeapItem<PathNode>, IComparable<PathNode>
  {
    private bool IsGridNode = true;
    public int XLoc;
    public int YLoc;
    public Vector2Int Location;
    public bool IsBlocking;
    public float fCost;
    public float hCost;
    public float gCost;
    public PathNode Parent;
    public FlowDirection flowDirection;
    public bool UsingFlowDirection;
    public bool[] InternalBlocksToExit_Up_R_D_L = new bool[4];
    public List<UniquePerson> IsInUseByTHisPerson = new List<UniquePerson>();
    public HierarchicalNode hierarchicalnode;
    private int heapIndex;
    public bool IsWarp;

    public int binaryHeapIndex
    {
      get => this.heapIndex;
      set => this.heapIndex = value;
    }

    public PathNode(PathNode deepcopythis)
    {
      this.IsGridNode = deepcopythis.IsGridNode;
      this.XLoc = deepcopythis.XLoc;
      this.YLoc = deepcopythis.YLoc;
      this.Location = new Vector2Int(deepcopythis.Location);
      this.IsBlocking = deepcopythis.IsBlocking;
      this.fCost = deepcopythis.fCost;
      this.hCost = deepcopythis.hCost;
      this.gCost = deepcopythis.gCost;
      this.flowDirection = deepcopythis.flowDirection;
      this.UsingFlowDirection = deepcopythis.UsingFlowDirection;
      this.IsWarp = deepcopythis.IsWarp;
      this.InternalBlocksToExit_Up_R_D_L = new bool[4];
      for (int index = 0; index < 4; ++index)
        this.InternalBlocksToExit_Up_R_D_L[index] = deepcopythis.InternalBlocksToExit_Up_R_D_L[index];
      this.IsInUseByTHisPerson = new List<UniquePerson>((IEnumerable<UniquePerson>) deepcopythis.IsInUseByTHisPerson);
    }

    public PathNode(int _XLoc, int _YLoc)
    {
      this.Location = new Vector2Int(_XLoc, _YLoc);
      this.XLoc = _XLoc;
      this.YLoc = _YLoc;
    }

    public bool IsAgainstFlow(PathNode PreviousNode) => PreviousNode.flowDirection == FlowDirection.S && this.flowDirection == FlowDirection.N || PreviousNode.flowDirection == FlowDirection.E && this.flowDirection == FlowDirection.W || (PreviousNode.flowDirection == FlowDirection.W && this.flowDirection == FlowDirection.E || PreviousNode.flowDirection == FlowDirection.N && this.flowDirection == FlowDirection.S);

    internal static float GetDistanceManhattan(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      bool AllowDiagonals = true)
    {
      int num1 = Math.Abs(StartPoint.Y - EndPoint.Y);
      int num2 = Math.Abs(StartPoint.X - EndPoint.X);
      if (!AllowDiagonals)
        return 10f * (float) (num2 + num1);
      return num2 > num1 ? (float) (14.0 * (double) num1 + 10.0 * (double) (num2 - num1)) : (float) (14.0 * (double) num2 + 10.0 * (double) (num1 - num2));
    }

    internal static float GetDistanceEuclidean(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      bool AllowDiagonals = true)
    {
      Vector2 vector2 = new Vector2((float) StartPoint.X, (float) StartPoint.Y);
      return (new Vector2((float) EndPoint.X, (float) EndPoint.Y) - vector2).Length();
    }

    public int CompareTo(PathNode rhs) => (double) this.fCost == (double) rhs.fCost ? this.hCost.CompareTo(rhs.hCost) : this.fCost.CompareTo(rhs.fCost);
  }
}
