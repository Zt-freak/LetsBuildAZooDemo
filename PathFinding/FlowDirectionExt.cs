// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.FlowDirectionExt
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.PathFinding
{
  public static class FlowDirectionExt
  {
    public static FlowDirection SetDirection(
      this FlowDirection flag,
      FlowDirection toSet)
    {
      return flag | toSet;
    }

    public static FlowDirection UnsetDirection(
      this FlowDirection flag,
      FlowDirection toUnset)
    {
      return flag & ~toUnset;
    }

    public static List<FlowDirection> SplitToIndividualDirections(
      this FlowDirection flag)
    {
      List<FlowDirection> flowDirectionList = new List<FlowDirection>();
      for (int index = 0; index < 8; ++index)
      {
        if (((int) flag >> index & 1) != 0)
          flowDirectionList.Add((FlowDirection) (1 << index));
      }
      return flowDirectionList;
    }

    public static bool IsSet(this FlowDirection flag, FlowDirection direction) => flag.HasFlag((Enum) direction);

    public static bool IsIndividualDirection(this FlowDirection flag) => Enum.IsDefined(typeof (FlowDirection), (object) flag);

    public static Vector2Int ToVector2Int(this FlowDirection flag)
    {
      switch (flag)
      {
        case FlowDirection.E:
          return new Vector2Int(1, 0);
        case FlowDirection.NE:
          return new Vector2Int(1, -1);
        case FlowDirection.N:
          return new Vector2Int(0, -1);
        case FlowDirection.NW:
          return new Vector2Int(-1, -1);
        case FlowDirection.W:
          return new Vector2Int(-1, 0);
        case FlowDirection.SW:
          return new Vector2Int(-1, 1);
        case FlowDirection.S:
          return new Vector2Int(0, 1);
        case FlowDirection.SE:
          return new Vector2Int(1, 1);
        default:
          return (Vector2Int) null;
      }
    }

    public static FlowDirection FromVector(this FlowDirection flag, Vector2Int vec) => flag.FromVector(vec.X, vec.Y);

    public static FlowDirection FromVector(this FlowDirection flag, int x, int y)
    {
      if (x * y != 0)
      {
        if (x > 0)
        {
          if (y > 0)
            return FlowDirection.SE;
          if (y < 0)
            return FlowDirection.NE;
        }
        else if (x < 0)
        {
          if (y > 0)
            return FlowDirection.SW;
          if (y < 0)
            return FlowDirection.NW;
        }
      }
      else
      {
        if (y > 0)
          return FlowDirection.S;
        if (y < 0)
          return FlowDirection.N;
        if (x > 0)
          return FlowDirection.E;
        if (x < 0)
          return FlowDirection.W;
      }
      return FlowDirection.NONE;
    }
  }
}
