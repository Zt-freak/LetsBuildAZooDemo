// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.FlowDirection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

namespace TinyZoo.PathFinding
{
  public enum FlowDirection
  {
    NONE = 0,
    E = 1,
    NE = 2,
    N = 4,
    NW = 8,
    W = 16, // 0x00000010
    SW = 32, // 0x00000020
    S = 64, // 0x00000040
    SE = 128, // 0x00000080
  }
}
