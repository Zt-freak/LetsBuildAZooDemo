// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Data.Z_WorldExpansion
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;

namespace TinyZoo.Z_Data
{
  internal class Z_WorldExpansion
  {
    internal static void GetSizes(
      int Level,
      out Vector2Int TopLeft,
      out Vector2Int BottomRight,
      int WorldSizeX,
      int WorldSizeY)
    {
      TopLeft = new Vector2Int(1, 1);
      BottomRight = new Vector2Int(TileMath.GetOverWorldMapSize_XDefault() - 2, WorldSizeY - (TileMath.BufferAtWoldBottom - 1));
      int num1 = (TileMath.GetOverWorldMapSize_XDefault() + 1) / 2;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      switch (Level)
      {
        case 0:
          num2 = num1 - 13;
          num3 = num2;
          num4 = 26;
          break;
        case 1:
          num2 = num1 - 13;
          num3 = num2;
          num4 = 1;
          break;
        case 2:
          num2 = num1 - 13 - 25;
          num3 = num2;
          num4 = -24;
          break;
        case 3:
          num2 = num1 - 13 - 25;
          num3 = num2;
          num4 = -24;
          break;
        case 4:
          num2 = num1 - 13 - 25 - 25;
          num3 = num2;
          num4 = -49;
          break;
        case 5:
          num2 = num1 - 13 - 25 - 25;
          num3 = num2;
          num4 = -49;
          break;
        case 6:
          num2 = num1 - 13 - 25 - 25 - 25;
          num3 = num2;
          num4 = -74;
          break;
        case 7:
          num2 = num1 - 13 - 25 - 25 - 25;
          num3 = num2;
          num4 = -74;
          break;
        case 8:
          int num5 = num1 - 149;
          num2 = 0;
          num3 = 0;
          num4 = -99;
          break;
        case 9:
          num2 = 0;
          num3 = 0;
          num4 = -124;
          break;
        case 10:
          num2 = 0;
          num3 = 0;
          num4 = -149;
          break;
        case 11:
          num2 = 0;
          num3 = 0;
          num4 = -174;
          break;
        case 12:
          num2 = 0;
          num3 = 0;
          num4 = 0;
          break;
      }
      int num6 = num4 + (TileMath.GetOverWorldMapSize_YSize() - 56);
      TopLeft.X += num2;
      BottomRight.X -= num3;
      TopLeft.Y += num6;
      BottomRight.Y = TileMath.GetOverWorldMapSize_YSize() - 7;
      if (TopLeft.X < 1 || BottomRight.X >= TileMath.GetOverWorldMapSize_XDefault() - 1)
        throw new Exception("Leave Space For Fence");
    }
  }
}
