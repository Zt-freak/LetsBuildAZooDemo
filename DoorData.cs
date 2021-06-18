// Decompiled with JetBrains decompiler
// Type: TinyZoo.DoorData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Tile_Data;

namespace TinyZoo
{
  internal class DoorData
  {
    private static HashSet<TILETYPE> BuildingsWithOpeningDoors;

    internal static bool ThisBuildingHasADoor(TILETYPE tiletype)
    {
      if (DoorData.BuildingsWithOpeningDoors == null)
      {
        DoorData.BuildingsWithOpeningDoors = new HashSet<TILETYPE>();
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.StoreRoom);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Storeroom_BrownWood);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Storeroom_Victorian);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Storeroom_Monkey);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.VetOffice);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Incinerator);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.QuarantineOffice);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.MeatProcessor);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.SurveillanceBuilding);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.ImpossibleBuilding);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Nursery);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.DNABuilding);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Barn);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Farmhouse);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.FarmProcessor);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.BeerBrewery);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.CrocHandbagFactory);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.GlueFactory);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.BaconFactory);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.BuffaloWingFactory);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.SnakeSkinFactory);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Slaughterhouse);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.EggBatteryFarm);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.MilkBatteryFarm);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.Warehouse);
        DoorData.BuildingsWithOpeningDoors.Add(TILETYPE.RecyclingCenter);
      }
      return DoorData.BuildingsWithOpeningDoors.Contains(tiletype);
    }

    internal static DoorInfo GetDoorInfo(TILETYPE tiletype)
    {
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      if (tileInfo.doorinfo == null)
      {
        switch (tiletype)
        {
          case TILETYPE.Nursery:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1245, 3909, 13, 16), new Vector2(7f, 8f));
            break;
          case TILETYPE.QuarantineOffice:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1259, 2153, 16, 18), new Vector2(56f, 12f));
            break;
          case TILETYPE.VetOffice:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1310, 2153, 13, 18), new Vector2(22f, 10f));
            break;
          case TILETYPE.StoreRoom:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1892, 2372, 18, 16), new Vector2(9f, 10f));
            break;
          case TILETYPE.GlueFactory:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(477, 4071, 13, 18), new Vector2(38f, 10f));
            break;
          case TILETYPE.BuffaloWingFactory:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(477, 4071, 13, 18), new Vector2(7f, 13f));
            break;
          case TILETYPE.BaconFactory:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(477, 4071, 13, 18), new Vector2(-9f, 12f));
            break;
          case TILETYPE.Barn:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1873, 572, 26, 16), new Vector2(13f, 9f));
            break;
          case TILETYPE.Slaughterhouse:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(693, 1283, 26, 27), new Vector2(13f, 19f));
            break;
          case TILETYPE.EggBatteryFarm:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1152, 3910, 30, 15), new Vector2(15f, 22f));
            break;
          case TILETYPE.MilkBatteryFarm:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(3129, 3219, 27, 15), new Vector2(12f, 22f));
            break;
          case TILETYPE.ImpossibleBuilding:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(561, 4071, 20, 19), new Vector2(26f, 23f));
            break;
          case TILETYPE.DNABuilding:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1669, 4080, 28, 15), new Vector2(22f, 12f));
            break;
          case TILETYPE.MeatProcessor:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(3605, 3759, 13, 18), new Vector2(38f, 9f));
            break;
          case TILETYPE.Incinerator:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(396, 4071, 26, 20), new Vector2(11f, 12f));
            break;
          case TILETYPE.CrocHandbagFactory:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1259, 2153, 16, 18), new Vector2(24f, 10f));
            break;
          case TILETYPE.SnakeSkinFactory:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1415, 1885, 31, 17), new Vector2(23f, 17f));
            break;
          case TILETYPE.Storeroom_BrownWood:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1958, 2389, 16, 16), new Vector2(8f, 8f));
            break;
          case TILETYPE.Storeroom_Victorian:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1949, 2371, 16, 8), new Vector2(8f, 0.0f));
            break;
          case TILETYPE.Storeroom_Monkey:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(1949, 2380, 16, 8), new Vector2(8f, 1f));
            break;
          case TILETYPE.SurveillanceBuilding:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(519, 4071, 13, 18), new Vector2(6f, 9f));
            break;
          case TILETYPE.Farmhouse:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(2642, 2457, 14, 17), new Vector2(7f, 12f));
            break;
          case TILETYPE.BeerBrewery:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(2733, 2549, 16, 18), new Vector2(-8f, 18f));
            break;
          case TILETYPE.FarmProcessor:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(219, 4082, 30, 14), new Vector2(38f, 10f));
            break;
          case TILETYPE.RecyclingCenter:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(312, 4082, 14, 13), new Vector2(7f, 9f));
            break;
          case TILETYPE.Warehouse:
            tileInfo.doorinfo = new DoorInfo(new Rectangle(3998, 122, 32, 18), new Vector2(24f, 10f));
            break;
        }
      }
      return tileInfo.doorinfo;
    }
  }
}
