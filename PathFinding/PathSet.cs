// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.PathSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.Z_OverWorld.PathFinding_Nodes;

namespace TinyZoo.PathFinding
{
  internal class PathSet
  {
    internal static int maxMapSize = PathFindingManager.CityBlockSize * PathFindingManager.CityBlockSize;
    public List<PathNode> nodes;
    public PathNode[,] nodesasGrid;
    public bool CanfitThrughDiagonalGaps;
    private static Random rand = new Random();
    private bool preferStraight;
    private BinaryHeap<PathNode> openSet = new BinaryHeap<PathNode>(10000);

    public PathSet()
    {
      this.nodes = new List<PathNode>();
      this.preferStraight = true;
    }

    public bool GetIsBlocked(Vector2Int Location) => Location.X >= this.nodesasGrid.GetLength(0) || Location.Y >= this.nodesasGrid.GetLength(1) || this.nodesasGrid[Location.X, Location.Y].IsBlocking;

    public bool GetIsBlocked(int Xloc, int YLoc) => Xloc >= this.nodesasGrid.GetLength(0) || YLoc >= this.nodesasGrid.GetLength(1) || this.nodesasGrid[Xloc, YLoc].IsBlocking;

    public bool GetCanCrossFast(int CurrentXloc, int CurrentYLoc, DirectionPressed GoingThisWay)
    {
      switch (GoingThisWay)
      {
        case DirectionPressed.Up:
          return !this.nodesasGrid[CurrentXloc, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[0] && !this.nodesasGrid[CurrentXloc, CurrentYLoc - 1].InternalBlocksToExit_Up_R_D_L[2] && !this.nodesasGrid[CurrentXloc, CurrentYLoc - 1].IsBlocking;
        case DirectionPressed.Right:
          return !this.nodesasGrid[CurrentXloc, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[1] && !this.nodesasGrid[CurrentXloc + 1, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[3] && !this.nodesasGrid[CurrentXloc + 1, CurrentYLoc].IsBlocking;
        case DirectionPressed.Down:
          return !this.nodesasGrid[CurrentXloc, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[2] && !this.nodesasGrid[CurrentXloc, CurrentYLoc + 1].InternalBlocksToExit_Up_R_D_L[0] && !this.nodesasGrid[CurrentXloc, CurrentYLoc + 1].IsBlocking;
        case DirectionPressed.Left:
          return !this.nodesasGrid[CurrentXloc, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[3] && !this.nodesasGrid[CurrentXloc - 1, CurrentYLoc].InternalBlocksToExit_Up_R_D_L[1] && !this.nodesasGrid[CurrentXloc - 1, CurrentYLoc].IsBlocking;
        default:
          throw new Exception("For now - HAVE TO PASS IN CARDINAL DIRECTION");
      }
    }

    public bool GetCanCross(int CurrentXloc, int CurrentYLoc, DirectionPressed GoingThisWay) => throw new Exception("NOT CODED, TRY AND USE GETCANCROSSFAST instead");

    public bool GetCanCross(int CurrentXloc, int CurrentYLoc, int TargetXloc, int TargetYLoc) => throw new Exception("NOT CODED, TRY AND USE GETCANCROSSFAST instead");

    public FlowDirection GetFlow(Vector2Int Location) => this.nodesasGrid[Location.X, Location.Y].flowDirection;

    public Vector2Int GetNearestUnblockedTile(
      Vector2Int SeacrhAroundHere,
      out bool HasNothingInBounds)
    {
      bool flag1 = false;
      int num1 = 3;
      HasNothingInBounds = false;
      int num2 = 1;
      Vector2Int vector2Int = new Vector2Int(SeacrhAroundHere);
      while (!flag1)
      {
        bool flag2 = false;
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int num3 = 0;
          switch (index1)
          {
            case 0:
              vector2Int.Y = SeacrhAroundHere.Y + num2;
              vector2Int.X = SeacrhAroundHere.X - num2;
              break;
            case 1:
              num3 = 2;
              vector2Int.Y = SeacrhAroundHere.Y - num2 + 1;
              vector2Int.X = SeacrhAroundHere.X - num2;
              break;
            case 2:
              vector2Int.Y = SeacrhAroundHere.Y - num2;
              vector2Int.X = SeacrhAroundHere.X - num2;
              break;
            default:
              num3 = 2;
              vector2Int.Y = SeacrhAroundHere.Y - num2 + 1;
              vector2Int.X = SeacrhAroundHere.X + num2;
              break;
          }
          for (int index2 = 0; index2 < num1 - num3; ++index2)
          {
            if (BoundsChecker.IsThisInBounds(vector2Int))
            {
              flag2 = true;
              if (!this.GetIsBlocked(vector2Int))
                return vector2Int;
            }
            if (index1 == 0 || index1 == 2)
              ++vector2Int.X;
            else
              ++vector2Int.Y;
          }
        }
        num1 += 2;
        ++num2;
        if (!flag2)
        {
          HasNothingInBounds = true;
          return SeacrhAroundHere;
        }
      }
      throw new Exception("THIS IS IMPOSSIBLE the function should have already returned domething");
    }

    public PathNode GetNode(int XLoc, int YLoc) => this.nodesasGrid[XLoc, YLoc];

    public void CreateGrid(int XWidth, int YHeight)
    {
      this.nodesasGrid = new PathNode[XWidth, YHeight];
      for (int _XLoc = 0; _XLoc < XWidth; ++_XLoc)
      {
        for (int _YLoc = 0; _YLoc < YHeight; ++_YLoc)
        {
          this.nodes.Add(new PathNode(_XLoc, _YLoc));
          this.nodesasGrid[_XLoc, _YLoc] = this.nodes[this.nodes.Count - 1];
        }
      }
    }

    public void BlockAllTiles()
    {
      for (int index1 = 0; index1 < this.nodesasGrid.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.nodesasGrid.GetLength(1); ++index2)
          this.nodesasGrid[index1, index2].IsBlocking = true;
      }
    }

    public bool IsWithinBounds(Vector2Int loc) => loc.X >= 0 && loc.X < this.nodesasGrid.GetLength(0) && loc.Y >= 0 && loc.Y < this.nodesasGrid.GetLength(1);

    public List<Vector2Int> GetValidLocationsWithinSquare(Vector2Int here, int radius)
    {
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      for (int index1 = -radius; index1 <= radius; ++index1)
      {
        for (int index2 = -radius; index2 <= radius; ++index2)
        {
          if (index1 != 0 || index2 != 0)
          {
            int _X = here.X + index1;
            int _Y = here.Y + index2;
            if (_X >= 1 && _X < this.nodesasGrid.GetLength(0) - 1 && (_Y >= 1 && _Y < this.nodesasGrid.GetLength(1) - 1) && !this.nodesasGrid[_X, _Y].IsBlocking)
              vector2IntList.Add(new Vector2Int(_X, _Y));
          }
        }
      }
      return vector2IntList;
    }

    public List<Vector2Int> GetTilesWithFlow(Vector2Int here, int radius) => new List<Vector2Int>();

    public List<Vector2Int> GetValidLocationsAroundThisLocation(
      Vector2Int ThisLocation,
      bool IncludeDiagonals = false)
    {
      List<Vector2Int> vector2IntList = new List<Vector2Int>();
      if (ThisLocation.X > 0)
      {
        if (!this.nodesasGrid[ThisLocation.X - 1, ThisLocation.Y].IsBlocking)
          vector2IntList.Add(new Vector2Int(ThisLocation.X - 1, ThisLocation.Y));
        if (IncludeDiagonals)
        {
          if (ThisLocation.Y > 0 && !this.nodesasGrid[ThisLocation.X - 1, ThisLocation.Y - 1].IsBlocking)
            vector2IntList.Add(new Vector2Int(ThisLocation.X - 1, ThisLocation.Y - 1));
          if (ThisLocation.Y < this.nodesasGrid.GetLength(1) - 1 && !this.nodesasGrid[ThisLocation.X - 1, ThisLocation.Y + 1].IsBlocking)
            vector2IntList.Add(new Vector2Int(ThisLocation.X - 1, ThisLocation.Y + 1));
        }
      }
      if (ThisLocation.X < this.nodesasGrid.GetLength(0) - 1)
      {
        if (!this.nodesasGrid[ThisLocation.X + 1, ThisLocation.Y].IsBlocking)
          vector2IntList.Add(new Vector2Int(ThisLocation.X + 1, ThisLocation.Y));
        if (IncludeDiagonals)
        {
          if (ThisLocation.Y > 0 && !this.nodesasGrid[ThisLocation.X + 1, ThisLocation.Y - 1].IsBlocking)
            vector2IntList.Add(new Vector2Int(ThisLocation.X + 1, ThisLocation.Y - 1));
          if (ThisLocation.Y < this.nodesasGrid.GetLength(1) - 1 && !this.nodesasGrid[ThisLocation.X + 1, ThisLocation.Y + 1].IsBlocking)
            vector2IntList.Add(new Vector2Int(ThisLocation.X + 1, ThisLocation.Y + 1));
        }
      }
      if (ThisLocation.Y > 0 && !this.nodesasGrid[ThisLocation.X, ThisLocation.Y - 1].IsBlocking)
        vector2IntList.Add(new Vector2Int(ThisLocation.X, ThisLocation.Y - 1));
      if (ThisLocation.Y < this.nodesasGrid.GetLength(1) - 1 && !this.nodesasGrid[ThisLocation.X, ThisLocation.Y + 1].IsBlocking)
        vector2IntList.Add(new Vector2Int(ThisLocation.X, ThisLocation.Y + 1));
      return vector2IntList;
    }

    public void UnblockTile(Vector2Int Loc) => this.UnblockTile(Loc.X, Loc.Y);

    public void BlockTile(Vector2Int Loc) => this.BlockTile(Loc.X, Loc.Y);

    public void UnblockTile(int X, int Y) => this.nodesasGrid[X, Y].IsBlocking = false;

    public void UnblockTile(int X, int Y, FlowDirection flowDirection)
    {
      this.nodesasGrid[X, Y].IsBlocking = false;
      this.nodesasGrid[X, Y].UsingFlowDirection = true;
      this.nodesasGrid[X, Y].flowDirection = flowDirection;
    }

    public void BlockTile(int X, int Y) => this.nodesasGrid[X, Y].IsBlocking = true;

    public void CreateWall(
      int X,
      int Y,
      bool All,
      bool North = false,
      bool East = false,
      bool South = false,
      bool West = false)
    {
      this.MakeOrReomveWall(true, X, Y, All, North, East, South, West);
    }

    public void RemoveWall(
      int X,
      int Y,
      bool All,
      bool North = false,
      bool East = false,
      bool South = false,
      bool West = false)
    {
      this.MakeOrReomveWall(false, X, Y, All, North, East, South, West);
    }

    private void MakeOrReomveWall(
      bool IsMakeWall,
      int X,
      int Y,
      bool All,
      bool North = false,
      bool East = false,
      bool South = false,
      bool West = false)
    {
      if (All | North)
        this.nodesasGrid[X, Y].InternalBlocksToExit_Up_R_D_L[0] = IsMakeWall;
      if (All | East)
        this.nodesasGrid[X, Y].InternalBlocksToExit_Up_R_D_L[1] = IsMakeWall;
      if (All | South)
        this.nodesasGrid[X, Y].InternalBlocksToExit_Up_R_D_L[2] = IsMakeWall;
      if (All | West)
        this.nodesasGrid[X, Y].InternalBlocksToExit_Up_R_D_L[3] = IsMakeWall;
      if (X > 0 && All | West)
        this.nodesasGrid[X - 1, Y].InternalBlocksToExit_Up_R_D_L[1] = IsMakeWall;
      if (X < this.nodesasGrid.GetLength(0) && All | East)
        this.nodesasGrid[X + 1, Y].InternalBlocksToExit_Up_R_D_L[3] = IsMakeWall;
      if (Y > 0 && All | North)
        this.nodesasGrid[X, Y - 1].InternalBlocksToExit_Up_R_D_L[2] = IsMakeWall;
      if (Y >= this.nodesasGrid.GetLength(1) || !(All | South))
        return;
      this.nodesasGrid[X, Y + 1].InternalBlocksToExit_Up_R_D_L[0] = IsMakeWall;
    }

    public List<PathNode> GetFullPathToLocation(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      bool AllowDiagonals,
      Vector2Int topLeftBoundary = null,
      Vector2Int bottomRightBoundary = null)
    {
      DiagonalsAllowed diagonalsAllowed = DiagonalsAllowed.NO_DIAGONALS;
      return this.FindPath(StartPoint, EndPoint, diagonalsAllowed, topLeftBoundary, bottomRightBoundary);
    }

    public List<PathNode> FindPathWithFlow(Vector2Int StartPoint, Vector2Int EndPoint)
    {
      PathNode StartNode = this.nodesasGrid[StartPoint.X, StartPoint.Y];
      PathNode EndNode = this.nodesasGrid[EndPoint.X, EndPoint.Y];
      this.openSet.PooledItemReset();
      HashSet<PathNode> pathNodeSet = new HashSet<PathNode>();
      this.openSet.Add(StartNode);
      while (this.openSet.Count > 0)
      {
        PathNode PreviousNode = this.openSet.PopRoot();
        pathNodeSet.Add(PreviousNode);
        if (PreviousNode == EndNode)
          return this.RetracePath(StartNode, EndNode);
        foreach (PathNode neighbour in this.GetNeighbours(PreviousNode.Location, DiagonalsAllowed.ALLOW_DIAGONALS))
        {
          if (!neighbour.IsBlocking && !pathNodeSet.Contains(neighbour) && !neighbour.IsAgainstFlow(PreviousNode))
          {
            float num = PreviousNode.gCost + PathNode.GetDistanceManhattan(PreviousNode.Location, neighbour.Location);
            if ((double) num < (double) neighbour.gCost || !this.openSet.Contains(neighbour))
            {
              neighbour.gCost = num;
              neighbour.hCost = PathNode.GetDistanceEuclidean(neighbour.Location, EndNode.Location);
              neighbour.Parent = PreviousNode;
              if (!this.openSet.Contains(neighbour) && (neighbour.Location.X == PreviousNode.Location.X || neighbour.Location.Y == PreviousNode.Location.Y))
                this.openSet.Add(neighbour);
            }
          }
        }
      }
      return (List<PathNode>) null;
    }

    public List<PathNode> FindPath(
      Vector2Int StartPoint,
      Vector2Int EndPoint,
      DiagonalsAllowed diagonalsAllowed,
      Vector2Int topLeftBoundary = null,
      Vector2Int bottomRightBoundary = null,
      bool useFlow = false)
    {
      PathNode StartNode = this.nodesasGrid[StartPoint.X, StartPoint.Y];
      PathNode EndNode = this.nodesasGrid[EndPoint.X, EndPoint.Y];
      this.openSet.PooledItemReset();
      HashSet<PathNode> pathNodeSet = new HashSet<PathNode>();
      this.openSet.Add(StartNode);
      while (this.openSet.Count > 0)
      {
        PathNode pathNode = this.openSet.PopRoot();
        pathNodeSet.Add(pathNode);
        if (pathNode == EndNode)
          return this.RetracePath(StartNode, EndNode);
        foreach (PathNode neighbour in this.GetNeighbours(pathNode.Location, diagonalsAllowed, useFlow))
        {
          if (!neighbour.IsBlocking && !pathNodeSet.Contains(neighbour) && (topLeftBoundary == null || neighbour.XLoc >= topLeftBoundary.X && neighbour.YLoc >= topLeftBoundary.Y) && (bottomRightBoundary == null || neighbour.XLoc <= bottomRightBoundary.X && neighbour.YLoc <= bottomRightBoundary.Y))
          {
            bool AllowDiagonals = diagonalsAllowed == DiagonalsAllowed.ALLOW_DIAGONALS || diagonalsAllowed == DiagonalsAllowed.ALLOW_UNBLOCKED;
            float num1 = pathNode.gCost + PathNode.GetDistanceManhattan(pathNode.Location, neighbour.Location, AllowDiagonals);
            if (pathNode.Parent != null)
            {
              Vector2 vector2_1 = new Vector2((float) pathNode.Location.X, (float) pathNode.Location.Y);
              Vector2 vector2_2 = new Vector2((float) neighbour.Location.X, (float) neighbour.Location.Y);
              Vector2 vector2_3 = new Vector2((float) pathNode.Parent.Location.X, (float) pathNode.Parent.Location.Y);
              Vector2 vector2_4 = vector2_1;
              Vector2 vector2_5 = vector2_2 - vector2_4;
              Vector2 vector2_6 = vector2_1 - vector2_3;
              float num2 = (float) ((double) vector2_6.X * (double) vector2_5.X + (double) vector2_6.Y * (double) vector2_5.Y);
              if (!this.preferStraight && (double) Math.Abs(1f - num2) < 1.40129846432482E-45)
                num1 += 0.3f;
              if (this.preferStraight && (double) Math.Abs(num2) < 1.40129846432482E-45)
                num1 += 0.3f;
            }
            if (!this.openSet.Contains(neighbour))
            {
              neighbour.gCost = num1;
              neighbour.hCost = PathNode.GetDistanceEuclidean(neighbour.Location, EndNode.Location, AllowDiagonals);
              neighbour.fCost = neighbour.gCost + neighbour.hCost;
              neighbour.Parent = pathNode;
              this.openSet.Add(neighbour);
            }
            else if ((double) num1 < (double) neighbour.gCost)
            {
              neighbour.gCost = num1;
              neighbour.Parent = pathNode;
              neighbour.fCost = neighbour.gCost + neighbour.hCost;
              this.openSet.UpdateItem(neighbour.binaryHeapIndex);
            }
          }
        }
      }
      return (List<PathNode>) null;
    }

    private List<PathNode> RetracePath(PathNode StartNode, PathNode EndNode)
    {
      List<PathNode> pathNodeList = new List<PathNode>();
      for (PathNode pathNode = EndNode; pathNode != StartNode; pathNode = pathNode.Parent)
        pathNodeList.Add(pathNode);
      pathNodeList.Reverse();
      return pathNodeList;
    }

    private static void Shuffle<T>(IList<T> list)
    {
      for (int index1 = list.Count - 1; index1 > 1; --index1)
      {
        int index2 = PathSet.rand.Next(index1 + 1);
        T obj = list[index2];
        list[index2] = list[index1];
        list[index1] = obj;
      }
    }

    public List<PathNode> GetNeighbours(
      Vector2Int Loc,
      DiagonalsAllowed diagonalsAllowed,
      bool useFlow = false)
    {
      List<PathNode> pathNodeList = new List<PathNode>();
      for (int x = -1; x < 2; ++x)
      {
        for (int y = -1; y < 2; ++y)
        {
          if (x != 0 || y != 0)
          {
            bool flag = (uint) (x * y) > 0U;
            if (!(diagonalsAllowed == DiagonalsAllowed.NO_DIAGONALS & flag))
            {
              int index1 = Loc.X + x;
              int index2 = Loc.Y + y;
              if (index1 >= 0 && index1 < this.nodesasGrid.GetLength(0) && (index2 >= 0 && index2 < this.nodesasGrid.GetLength(1)))
              {
                if (!this.CanfitThrughDiagonalGaps & flag)
                {
                  switch (diagonalsAllowed)
                  {
                    case DiagonalsAllowed.ALLOW_DIAGONALS:
                      if (!this.nodesasGrid[Loc.X, index2].IsBlocking || !this.nodesasGrid[index1, Loc.Y].IsBlocking)
                        break;
                      continue;
                    case DiagonalsAllowed.ALLOW_UNBLOCKED:
                      if (this.nodesasGrid[Loc.X, index2].IsBlocking || this.nodesasGrid[index1, Loc.Y].IsBlocking)
                        continue;
                      break;
                  }
                }
                PathNode pathNode = this.nodesasGrid[index1, index2];
                if (useFlow && pathNode.UsingFlowDirection)
                {
                  int num1 = 0;
                  int num2 = (int) ((FlowDirection) num1).FromVector(x, y);
                  if ((FlowDirection) num1 != pathNode.flowDirection)
                    continue;
                }
                pathNodeList.Add(pathNode);
              }
            }
          }
        }
      }
      return pathNodeList;
    }

    private DirectionPressed GetDirection(int x, int y)
    {
      if (x * y != 0)
      {
        if (x > 0)
        {
          if (y > 0)
            return DirectionPressed.SouthEast;
          if (y < 0)
            return DirectionPressed.NorthEast;
        }
        else if (x < 0)
        {
          if (y > 0)
            return DirectionPressed.SouthWest;
          if (y < 0)
            return DirectionPressed.NorthWest;
        }
      }
      else
      {
        if (y > 0)
          return DirectionPressed.Down;
        if (y < 0)
          return DirectionPressed.Up;
        if (x > 0)
          return DirectionPressed.Right;
        if (x < 0)
          return DirectionPressed.Left;
      }
      return DirectionPressed.None;
    }
  }
}
