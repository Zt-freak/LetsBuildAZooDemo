// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Factories.FactoryData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tile_Data;

namespace TinyZoo.Z_Factories
{
  internal class FactoryData
  {
    internal static int GetManufacturingTimeInMinutes(
      TILETYPE tiletype,
      float EmployeeProductivityMultiplier)
    {
      return tiletype == TILETYPE.SnakeSkinFactory ? (int) (480.0 * (double) EmployeeProductivityMultiplier) : (int) (120.0 * (double) EmployeeProductivityMultiplier);
    }
  }
}
