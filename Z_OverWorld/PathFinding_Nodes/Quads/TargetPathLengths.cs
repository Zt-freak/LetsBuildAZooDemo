// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads.TargetPathLengths
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads
{
  internal class TargetPathLengths
  {
    private List<PathToRemoteTarget> RemoteTargetPathInfo;
    public LocationNode GoViaThis;
    public HashSet<LocationNode> TargetNodes;

    public TargetPathLengths(LocationNode _GoViaThis)
    {
      this.RemoteTargetPathInfo = new List<PathToRemoteTarget>();
      this.TargetNodes = new HashSet<LocationNode>();
      this.GoViaThis = _GoViaThis;
    }

    public void AddTarget(LocationNode targetnode, int Distance, LocationNode THISLOCTEMP)
    {
      this.TargetNodes.Add(targetnode);
      if (targetnode.Location.X == 161 && targetnode.Location.Y == 150 && THISLOCTEMP.Location.X == 160)
      {
        int y = THISLOCTEMP.Location.Y;
      }
      this.RemoteTargetPathInfo.Add(new PathToRemoteTarget(targetnode, Distance));
    }

    public void RemoveLink(LocationNode removethisnode)
    {
      this.TargetNodes.Remove(removethisnode);
      for (int index = this.RemoteTargetPathInfo.Count - 1; index > -1; --index)
      {
        if (this.RemoteTargetPathInfo[index].FinalTarget == removethisnode)
        {
          this.RemoteTargetPathInfo.RemoveAt(index);
          return;
        }
      }
      throw new Exception("Failed to remove the node");
    }

    public void DebugCheckRemoteTarget()
    {
      if (!DebugFlags.DebugAllPathFinding)
        return;
      foreach (LocationNode targetNode in this.TargetNodes)
        this.DebugCheckRemoteTarget(targetNode);
    }

    public void DebugCheckRemoteTarget(LocationNode Target)
    {
      if (Target != this.GoViaThis && !this.GoViaThis.CanGetToThisRemoteLinkFromHere(Target))
        throw new Exception("Well look you added the target to a go via....");
    }

    public int GetDistance(LocationNode _TargetNode)
    {
      for (int index = 0; index < this.RemoteTargetPathInfo.Count; ++index)
      {
        if (this.RemoteTargetPathInfo[index].FinalTarget == _TargetNode)
          return this.RemoteTargetPathInfo[index].PathLength;
      }
      throw new Exception("this has to be here, its in the hash list");
    }

    public void RemoveThis(LocationNode _TargetNode, bool RemoveFromHash = false)
    {
      for (int index = this.RemoteTargetPathInfo.Count - 1; index > -1; --index)
      {
        if (this.RemoteTargetPathInfo[index].FinalTarget == _TargetNode)
          this.RemoteTargetPathInfo.RemoveAt(index);
      }
      if (!RemoveFromHash)
        return;
      this.TargetNodes.Remove(_TargetNode);
    }

    public bool CheckThisExistingPath(LocationNode _TargetNode, int Distance)
    {
      for (int index = 0; index < this.RemoteTargetPathInfo.Count; ++index)
      {
        if (this.RemoteTargetPathInfo[index].FinalTarget == _TargetNode)
          return this.RemoteTargetPathInfo[index].CheckPath(Distance);
      }
      throw new Exception("Target _TargetNode must in here");
    }
  }
}
