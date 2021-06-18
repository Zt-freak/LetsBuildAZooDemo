// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads.PathToRemoteTarget
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.Quads
{
  internal class PathToRemoteTarget
  {
    public LocationNode FinalTarget;
    public int PathLength;

    public PathToRemoteTarget(LocationNode _FinalTarget, int Distance)
    {
      this.PathLength = Distance;
      this.FinalTarget = _FinalTarget;
    }

    public bool CheckPath(int _PathLength)
    {
      if (this.PathLength <= _PathLength)
        return false;
      this.PathLength = _PathLength;
      return true;
    }
  }
}
