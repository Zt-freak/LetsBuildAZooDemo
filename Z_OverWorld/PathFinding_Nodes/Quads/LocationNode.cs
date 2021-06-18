// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads.LocationNode
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads
{
  internal class LocationNode
  {
    public bool Blocked;
    public TILETYPE tiletype;
    public Vector2Int Location;
    public Vector2Int QuadRelativeLocation;
    private List<TargetPathLengths> RemoteLinkPaths;
    private HashSet<LocationNode> RemoteLinks = new HashSet<LocationNode>();
    private List<DestinationPath> LocalDirectLinkPaths;
    private HashSet<LocationNode> DirectLinks = new HashSet<LocationNode>();
    private HashSet<TILETYPE> TilesTypesLinkedFromHere = new HashSet<TILETYPE>();
    private Vector2Int OffsetToParentBuilding;
    private List<LocationNode> BrokenRemoteLinks = new List<LocationNode>();
    public bool Repath;
    public Vector2Int Ref_TopLeft;
    public LocationNode HaveToGoHereNext_ForReverseLink;

    public LocationNode(
      int _XLoc,
      int _YLoc,
      bool _Blocked,
      Vector2Int TopLeft,
      Vector2Int _OffsetToParentBuilding)
    {
      this.Ref_TopLeft = TopLeft;
      this.RemoteLinkPaths = new List<TargetPathLengths>();
      this.QuadRelativeLocation = new Vector2Int(_XLoc - TopLeft.X, _YLoc - TopLeft.Y);
      this.OffsetToParentBuilding = _OffsetToParentBuilding;
      this.Location = new Vector2Int(_XLoc, _YLoc);
      this.tiletype = TILETYPE.None;
      this.Blocked = _Blocked;
      this.LocalDirectLinkPaths = new List<DestinationPath>();
    }

    public bool CreateDirectLinkPair(
      LocationNode OtherNode,
      List<PathNode> path,
      Vector2Int TopLeft = null)
    {
      if (!this.CreateDirectLink(OtherNode, path, TopLeft))
        return false;
      List<PathNode> path1 = new List<PathNode>();
      for (int index = path.Count - 1; index > -1; --index)
        path1.Add(path[index]);
      OtherNode.CreateDirectLink(this, path1);
      this.TryToAddOrUpdateRemoteLink(OtherNode, OtherNode, path.Count);
      OtherNode.TryToAddOrUpdateRemoteLink(this, this, path.Count);
      return true;
    }

    private bool CreateDirectLink(LocationNode nodetoAdd, List<PathNode> path, Vector2Int TopLeft = null)
    {
      if (this.Location.X == 161 && this.Location.Y == 150 && nodetoAdd.Location.X == 162)
      {
        int y1 = nodetoAdd.Location.Y;
      }
      if (this.Location.X == 162 && this.Location.Y == 150 && nodetoAdd.Location.X == 161)
      {
        int y2 = nodetoAdd.Location.Y;
      }
      bool flag = false;
      int count = path.Count;
      if (!this.DirectLinks.Contains(nodetoAdd))
      {
        flag = true;
        this.DirectLinks.Add(nodetoAdd);
        if (TopLeft != null)
        {
          for (int index = 0; index < path.Count; ++index)
          {
            path[index] = new PathNode(path[index]);
            path[index].Location.X += TopLeft.X;
            path[index].Location.Y += TopLeft.Y;
            path[index].XLoc += TopLeft.X;
            path[index].YLoc += TopLeft.Y;
          }
        }
        this.LocalDirectLinkPaths.Add(new DestinationPath(path, nodetoAdd));
      }
      else
      {
        for (int index1 = 0; index1 < this.LocalDirectLinkPaths.Count; ++index1)
        {
          if (this.LocalDirectLinkPaths[index1].locationnodepointer == nodetoAdd && path.Count < this.LocalDirectLinkPaths[index1].Path.Count)
          {
            if (TopLeft != null)
            {
              for (int index2 = 0; index2 < path.Count; ++index2)
              {
                path[index2] = new PathNode(path[index1]);
                path[index2].Location.X += TopLeft.X;
                path[index2].Location.Y += TopLeft.Y;
                path[index2].XLoc += TopLeft.X;
                path[index2].YLoc += TopLeft.Y;
              }
            }
            this.LocalDirectLinkPaths[index1].Path = path;
          }
        }
      }
      return flag;
    }

    public void CheckCreateRemoteLinksOnDirectCreate(
      LocationNode HaveToGoHereNext,
      LocationNode TargetNode,
      int DistanceBetweenThisAndThat,
      bool ForceCheckChildren = false)
    {
      if (TargetNode.Location.X == 159 && TargetNode.Location.Y == 151)
      {
        if (this.Location.X == 159)
        {
          int y1 = this.Location.Y;
        }
        if (HaveToGoHereNext.Location.X == 159)
        {
          int y2 = HaveToGoHereNext.Location.Y;
        }
      }
      TargetNode.HaveToGoHereNext_ForReverseLink = this;
      for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
      {
        if (this.LocalDirectLinkPaths[index].locationnodepointer != HaveToGoHereNext)
        {
          if (TargetNode.Location.X == 161 && TargetNode.Location.Y == 150 && this.Location.X == 160)
          {
            int y = this.Location.Y;
          }
          this.LocalDirectLinkPaths[index].locationnodepointer.CreateRemoteLink(this, TargetNode, DistanceBetweenThisAndThat);
        }
      }
    }

    public void ReverseDirectLink(LocationNode ChildCallingRoot, int DistanceToChild)
    {
      if (!this.DirectLinks.Contains(this.HaveToGoHereNext_ForReverseLink))
        throw new Exception("The variable has been set wrongly?");
    }

    public void ReverseLink(LocationNode ChildCallingRoot, int DistanceToChild)
    {
      if (this.Location.X == 160 && this.Location.Y == 151 && ChildCallingRoot.Location.X == 159)
      {
        int y = ChildCallingRoot.Location.Y;
      }
      if (!this.DirectLinks.Contains(this.HaveToGoHereNext_ForReverseLink))
        throw new Exception("The variable has been set wrongly?");
      if (ChildCallingRoot == this)
        throw new Exception("You cant link to yourself");
      this.TryToAddOrUpdateRemoteLink(ChildCallingRoot, this.HaveToGoHereNext_ForReverseLink, DistanceToChild);
    }

    public void BreakDirectLink(LocationNode nodetoremove)
    {
      if (!this.DirectLinks.Contains(nodetoremove))
        return;
      this.DirectLinks.Remove(nodetoremove);
      foreach (LocationNode remoteLink in this.RemoteLinks)
        remoteLink.BreakRemoteLink(nodetoremove);
    }

    private bool TryToAddOrUpdateRemoteLink(
      LocationNode TargetNode,
      LocationNode GoViaThis,
      int Distance)
    {
      if (!this.RemoteLinks.Contains(TargetNode))
      {
        if (!this.DirectLinks.Contains(GoViaThis))
          throw new Exception("We have to skip this, because we do not know how to local path");
        this.RemoteLinks.Add(TargetNode);
        for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
        {
          if (this.RemoteLinkPaths[index].GoViaThis == GoViaThis)
          {
            this.RemoteLinkPaths[index].AddTarget(TargetNode, Distance, this);
            return true;
          }
        }
        this.RemoteLinkPaths.Add(new TargetPathLengths(GoViaThis));
        this.RemoteLinkPaths[this.RemoteLinkPaths.Count - 1].AddTarget(TargetNode, Distance, this);
        return true;
      }
      if (TargetNode.Location.X == 158 && TargetNode.Location.Y == 159 && (GoViaThis.Location.X == 157 && GoViaThis.Location.Y == 159))
        ;
      if (!this.DirectLinks.Contains(GoViaThis))
        throw new Exception("We have to skip this, because we do not know how to local path");
      int index1 = -1;
      int num = -1;
      bool flag = false;
      for (int index2 = 0; index2 < this.RemoteLinkPaths.Count; ++index2)
      {
        if (this.RemoteLinkPaths[index2].GoViaThis == GoViaThis)
        {
          index1 = index2;
          if (this.RemoteLinkPaths[index2].TargetNodes.Contains(TargetNode))
          {
            int distance = this.RemoteLinkPaths[index2].GetDistance(TargetNode);
            if (Distance >= distance)
              return false;
            if (this.RemoteLinkPaths[index2].CheckThisExistingPath(TargetNode, Distance))
              return true;
            throw new Exception("We just set a new shoter path how did it not change!?");
          }
        }
        else if (this.RemoteLinkPaths[index2].TargetNodes.Contains(TargetNode))
        {
          num = index2;
          if (this.RemoteLinkPaths[index2].GetDistance(TargetNode) > Distance)
          {
            flag = true;
            this.RemoteLinkPaths[index2].RemoveLink(TargetNode);
            num = -1;
          }
        }
      }
      if (index1 > -1 && num == -1)
      {
        this.RemoteLinkPaths[index1].AddTarget(TargetNode, Distance, this);
        return true;
      }
      if (num > -1 && !flag)
        return false;
      if (index1 != -1)
        throw new Exception("I have no idea what happened to get here");
      this.RemoteLinkPaths.Add(new TargetPathLengths(GoViaThis));
      this.RemoteLinkPaths[this.RemoteLinkPaths.Count - 1].AddTarget(TargetNode, Distance, this);
      return true;
    }

    public void DoubleCheckLengthToDirectLink(LocationNode directlink, int Distance)
    {
      if (!this.DirectLinks.Contains(directlink))
        return;
      bool flag = false;
      for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
      {
        if (this.LocalDirectLinkPaths[index].locationnodepointer == directlink)
        {
          if (this.LocalDirectLinkPaths[index].Path.Count != Distance)
            throw new Exception("osf");
          flag = true;
        }
      }
      if (!flag)
        throw new Exception("isf");
      if (!this.RemoteLinks.Contains(directlink))
        throw new Exception("sdsdf");
      for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
      {
        this.RemoteLinkPaths[index].TargetNodes.Contains(directlink);
        LocationNode goViaThis = this.RemoteLinkPaths[index].GoViaThis;
      }
    }

    public bool CreateRemoteLink(
      LocationNode HaveToGoHereNext,
      LocationNode TargetNode,
      int DistanceBetweenNextAndTarget)
    {
      if (this.Location.X == 162 && this.Location.Y == 162 && (TargetNode.Location.X == 161 && TargetNode.Location.Y == 162) && HaveToGoHereNext.Location.X == 162)
      {
        int y = HaveToGoHereNext.Location.Y;
      }
      if (!this.DirectLinks.Contains(HaveToGoHereNext))
        throw new Exception();
      for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
      {
        if (this.LocalDirectLinkPaths[index].locationnodepointer == HaveToGoHereNext)
          DistanceBetweenNextAndTarget += this.LocalDirectLinkPaths[index].Path.Count;
      }
      bool updateRemoteLink = this.TryToAddOrUpdateRemoteLink(TargetNode, HaveToGoHereNext, DistanceBetweenNextAndTarget);
      if (updateRemoteLink && TargetNode != this)
      {
        TargetNode.ReverseLink(this, DistanceBetweenNextAndTarget);
        for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
          this.LocalDirectLinkPaths[index].locationnodepointer.CreateRemoteLink(this, TargetNode, DistanceBetweenNextAndTarget);
      }
      return updateRemoteLink;
    }

    public void BreakRemoteLink(LocationNode linktothis) => this.BrokenRemoteLinks.Add(linktothis);

    public void DebugCheckRemoteTargets()
    {
      for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
        this.RemoteLinkPaths[index].DebugCheckRemoteTarget();
    }

    private void CheckRemoteLinksViaAllLinkToTarget() => Console.WriteLine("Test Skipped");

    private void CheckForDoubles(LocationNode TargetNode)
    {
      int num = 0;
      for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
      {
        if (this.RemoteLinkPaths[index].TargetNodes.Contains(TargetNode))
          ++num;
      }
      if (num > 1)
        throw new Exception("Duplicate");
    }

    public void AddRemoteLink(LocationNode linktothis)
    {
      if (this.RemoteLinks.Contains(linktothis))
        throw new Exception("CANNOT DOUBLE LINK");
      this.RemoteLinks.Add(linktothis);
      if (this.TilesTypesLinkedFromHere.Contains(linktothis.tiletype))
        return;
      this.TilesTypesLinkedFromHere.Add(linktothis.tiletype);
    }

    public LocationNode DoPath(LocationNode TargetLocation, ref List<PathNode> Path)
    {
      for (int index1 = 0; index1 < this.RemoteLinkPaths.Count; ++index1)
      {
        if (this.RemoteLinkPaths[index1].TargetNodes.Contains(TargetLocation))
        {
          if (!this.DirectLinks.Contains(this.RemoteLinkPaths[index1].GoViaThis))
            throw new Exception("We are about to get stuck");
          for (int index2 = 0; index2 < this.LocalDirectLinkPaths.Count; ++index2)
          {
            if (this.LocalDirectLinkPaths[index2].locationnodepointer == this.RemoteLinkPaths[index1].GoViaThis)
            {
              List<PathNode> worldSpacePath = this.LocalDirectLinkPaths[index2].GetWorldSpacePath(this.Ref_TopLeft);
              for (int index3 = 0; index3 < worldSpacePath.Count; ++index3)
                Path.Add(worldSpacePath[index3]);
              return this.RemoteLinkPaths[index1].GoViaThis;
            }
          }
        }
      }
      throw new Exception("CANNOT ONE");
    }

    public bool CanGetToThisDirectLinkFromHere(LocationNode locationnode) => this.DirectLinks.Contains(locationnode);

    public bool CanGetToThisRemoteLinkFromHere(LocationNode locationnode) => this.RemoteLinks.Contains(locationnode);

    public int GetDistanceToHere(LocationNode locationnode)
    {
      if (this.RemoteLinks.Contains(locationnode))
      {
        for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
        {
          if (this.RemoteLinkPaths[index].TargetNodes.Contains(locationnode))
            return this.RemoteLinkPaths[index].GetDistance(locationnode);
        }
      }
      if (this.DirectLinks.Contains(locationnode))
      {
        for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
        {
          if (this.LocalDirectLinkPaths[index].locationnodepointer == locationnode)
            return this.LocalDirectLinkPaths[index].Path.Count;
        }
      }
      return -1;
    }

    public string GetDistanceToHereASSTRING(LocationNode locationnode)
    {
      if (!Z_DebugFlags.DrawDistancesToDirectConnections)
      {
        if (this.RemoteLinks.Contains(locationnode))
        {
          for (int index = 0; index < this.RemoteLinkPaths.Count; ++index)
          {
            if (this.RemoteLinkPaths[index].TargetNodes.Contains(locationnode))
              return string.Concat((object) this.RemoteLinkPaths[index].GetDistance(locationnode));
          }
        }
        return "r";
      }
      if (this.DirectLinks.Contains(locationnode))
      {
        for (int index = 0; index < this.LocalDirectLinkPaths.Count; ++index)
        {
          if (this.LocalDirectLinkPaths[index].locationnodepointer == locationnode)
            return string.Concat((object) this.LocalDirectLinkPaths[index].Path.Count);
        }
      }
      return "d";
    }
  }
}
