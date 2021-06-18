// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub.FactorySmokeData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components.Sub
{
  internal class FactorySmokeData
  {
    internal static Vector2 GetSmokeOffset(
      TILETYPE building,
      int Rotation,
      int Index,
      out bool AddAnother,
      out SMOKETYPE smoketype)
    {
      smoketype = SMOKETYPE.RegularSmoke;
      AddAnother = false;
      switch (building)
      {
        case TILETYPE.GlueFactory:
          if (Index < 2)
            AddAnother = true;
          switch (Rotation)
          {
            case 0:
              if (Index == 0)
                return new Vector2(-42f, -84f);
              return Index == 1 ? new Vector2(-42f, -58f) : new Vector2(-25f, -58f);
            case 1:
              if (Index == 0)
                return new Vector2(-2f, -92f);
              return Index == 1 ? new Vector2(-27f, -92f) : new Vector2(-2f, -76f);
            case 2:
              if (Index == 0)
                return new Vector2(22f, -77f);
              return Index == 1 ? new Vector2(5f, -77f) : new Vector2(22f, -56f);
            case 3:
              if (Index == 0)
                return new Vector2(-28f, -40f);
              return Index == 1 ? new Vector2(-2f, -56f) : new Vector2(-2f, -40f);
          }
          break;
        case TILETYPE.BuffaloWingFactory:
          if (Index < 1)
            AddAnother = true;
          smoketype = SMOKETYPE.SmallSmoke;
          switch (Rotation)
          {
            case 0:
              return Index == 0 ? new Vector2(-30f, -72f) : new Vector2(-30f, -56f);
            case 1:
              return Index == 0 ? new Vector2(0.0f, -30f) : new Vector2(-23f, -30f);
            case 2:
              return Index == 0 ? new Vector2(28f, -56f) : new Vector2(28f, -72f);
            case 3:
              return Index == 0 ? new Vector2(16f, -30f) : new Vector2(-7f, -30f);
          }
          break;
        case TILETYPE.BaconFactory:
          if (Index < 1)
            AddAnother = true;
          switch (Rotation)
          {
            case 0:
              if (Index == 0)
                return new Vector2(7f, -71f);
              if (Index == 1)
                return new Vector2(24f, -71f);
              break;
            case 1:
              if (Index == 0)
                return new Vector2(31f, -29f);
              if (Index == 1)
                return new Vector2(31f, -10f);
              break;
            case 2:
              if (Index == 0)
                return new Vector2(-25f, -10f);
              if (Index == 1)
                return new Vector2(-42f, -10f);
              break;
            case 3:
              if (Index == 0)
                return new Vector2(-32f, -72f);
              if (Index == 1)
                return new Vector2(-32f, -53f);
              break;
          }
          break;
        case TILETYPE.Slaughterhouse:
          switch (Rotation)
          {
            case 0:
              return new Vector2(27f, -58f);
            case 1:
              return new Vector2(-1f, -38f);
            case 2:
              return new Vector2(-27f, -58f);
            case 3:
              return new Vector2(0.0f, -73f);
          }
          break;
        case TILETYPE.MeatProcessor:
          if (Index < 3)
            AddAnother = true;
          switch (Rotation)
          {
            case 0:
              switch (Index)
              {
                case 0:
                  return new Vector2(-44f, -37f);
                case 1:
                  return new Vector2(-44f, -56f);
                case 2:
                  return new Vector2(8f, -97f);
                default:
                  return new Vector2(26f, -97f);
              }
            case 1:
              switch (Index)
              {
                case 0:
                  return new Vector2(26f, -36f);
                case 1:
                  return new Vector2(26f, -51f);
                case 2:
                  return new Vector2(-37f, -94f);
                default:
                  return new Vector2(-18f, -94f);
              }
            case 2:
              switch (Index)
              {
                case 0:
                  return new Vector2(-43f, -36f);
                case 1:
                  return new Vector2(-24f, -36f);
                case 2:
                  return new Vector2(28f, -80f);
                default:
                  return new Vector2(28f, -99f);
              }
            case 3:
              switch (Index)
              {
                case 0:
                  return new Vector2(21f, -36f);
                case 1:
                  return new Vector2(2f, -36f);
                case 2:
                  return new Vector2(-42f, -93f);
                default:
                  return new Vector2(-42f, -77f);
              }
          }
          break;
        case TILETYPE.Incinerator:
          smoketype = SMOKETYPE.IncineratorSmoke;
          switch (Rotation)
          {
            case 0:
              return new Vector2(1f, -90f);
            case 1:
              return new Vector2(-8f, -93f);
            case 2:
              return new Vector2(1f, -90f);
            case 3:
              return new Vector2(-8f, -93f);
          }
          break;
        case TILETYPE.CrocHandbagFactory:
          if (Index < 2)
            AddAnother = true;
          smoketype = SMOKETYPE.SmallSmoke;
          if (Index == 0)
            smoketype = SMOKETYPE.RegularSmoke;
          switch (Rotation)
          {
            case 0:
              if (Index == 0)
                return new Vector2(20f, -95f);
              return Index == 1 ? new Vector2(-26f, -116f) : new Vector2(-11f, -116f);
            case 1:
              if (Index == 0)
                return new Vector2(21f, -52f);
              return Index == 1 ? new Vector2(22f, -97f) : new Vector2(22f, -81f);
            case 2:
              if (Index == 0)
                return new Vector2(-22f, -65f);
              return Index == 1 ? new Vector2(10f, -77f) : new Vector2(25f, -77f);
            case 3:
              if (Index == 0)
                return new Vector2(-37f, -91f);
              return Index == 1 ? new Vector2(-36f, -63f) : new Vector2(-36f, -79f);
          }
          break;
        case TILETYPE.SnakeSkinFactory:
          if (Index < 1)
            AddAnother = true;
          switch (Rotation)
          {
            case 0:
              if (Index == 0)
                return new Vector2(-27f, -46f);
              if (Index == 1)
                return new Vector2(-26f, -62f);
              break;
            case 1:
              if (Index == 0)
                return new Vector2(-9f, -72f);
              if (Index == 1)
                return new Vector2(13f, -72f);
              break;
            case 2:
              if (Index == 0)
                return new Vector2(26f, -47f);
              if (Index == 1)
                return new Vector2(25f, -63f);
              break;
            case 3:
              if (Index == 0)
                return new Vector2(-13f, -42f);
              if (Index == 1)
                return new Vector2(9f, -42f);
              break;
          }
          break;
      }
      return Vector2.Zero;
    }
  }
}
