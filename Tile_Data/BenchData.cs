// Decompiled with JetBrains decompiler
// Type: TinyZoo.Tile_Data.BenchData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;

namespace TinyZoo.Tile_Data
{
  internal class BenchData
  {
    internal static float GetBenchOffset(
      TILETYPE bench,
      DirectionPressed PersonFacesThisWayWhenSitting)
    {
      switch (bench)
      {
        case TILETYPE.BrownBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 15f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -12f;
            case DirectionPressed.Left:
              return 18f;
          }
          break;
        case TILETYPE.WhiteBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 15f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -12f;
            case DirectionPressed.Left:
              return 18f;
          }
          break;
        case TILETYPE.SnakeBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 14f;
            case DirectionPressed.Right:
              return -12f;
            case DirectionPressed.Down:
              return -15f;
            case DirectionPressed.Left:
              return 30f;
          }
          break;
        case TILETYPE.SmallBarTable:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 16f;
            case DirectionPressed.Right:
              return -16f;
            case DirectionPressed.Down:
              return -12f;
            case DirectionPressed.Left:
              return 15f;
          }
          break;
        case TILETYPE.GreenGardenBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 19f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 16f;
          }
          break;
        case TILETYPE.GreenChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 17f;
            case DirectionPressed.Right:
              return -15f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 15f;
          }
          break;
        case TILETYPE.WoodenChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 17f;
            case DirectionPressed.Right:
              return -15f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 15f;
          }
          break;
        case TILETYPE.LongWoodenBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 19f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 17f;
          }
          break;
        case TILETYPE.CamelChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 12f;
            case DirectionPressed.Right:
              return -16f;
            case DirectionPressed.Down:
              return -18f;
            case DirectionPressed.Left:
              return 16f;
          }
          break;
        case TILETYPE.PandaChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 19f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -12f;
            case DirectionPressed.Left:
              return 17f;
          }
          break;
        case TILETYPE.IceChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 19f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 17f;
          }
          break;
        case TILETYPE.TreeLogBench:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 14f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -12f;
            case DirectionPressed.Left:
              return 14f;
          }
          break;
        case TILETYPE.AztacChair:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 14f;
            case DirectionPressed.Right:
              return -17f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 17f;
          }
          break;
        default:
          switch (PersonFacesThisWayWhenSitting)
          {
            case DirectionPressed.Up:
              return 15f;
            case DirectionPressed.Right:
              return -12f;
            case DirectionPressed.Down:
              return -10f;
            case DirectionPressed.Left:
              return 12f;
          }
          break;
      }
      return 0.0f;
    }
  }
}
