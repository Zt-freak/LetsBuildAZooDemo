// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks.BlockList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.Z_OverWorld.PathFinding_Nodes.EntranceBlocks
{
  internal class BlockList
  {
    public List<Vector5Int> CustomerLocationsHere;

    public BlockList() => this.CustomerLocationsHere = new List<Vector5Int>();
  }
}
