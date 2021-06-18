// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ZooValues.BusData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;

namespace TinyZoo.Z_ZooValues
{
  internal class BusData
  {
    internal static int GetBusCapacity(BUSTYPE bus)
    {
      switch (bus)
      {
        case BUSTYPE.StartingBus_01:
          return 8;
        case BUSTYPE.BiggerBus_02:
          return 14;
        case BUSTYPE.LargeBus_03:
          return 20;
        case BUSTYPE.DoubleDeckerBus_04:
          return 35;
        case BUSTYPE.GarbageTruck:
          return 0;
        case BUSTYPE.BlackLimo:
          return 1;
        case BUSTYPE.AnimalRescueVan:
          return 1;
        case BUSTYPE.BlackVan:
          return 1;
        case BUSTYPE.SafetyVan:
          return 1;
        case BUSTYPE.GenericCar_Taxi:
          return 1;
        case BUSTYPE.GenericCar_GreenCar:
          return 1;
        case BUSTYPE.GenericCar_PinkCar:
          return 1;
        case BUSTYPE.GenericCar_BlueCar:
          return 1;
        case BUSTYPE.BikerCar_Brown:
          return 1;
        case BUSTYPE.BikerCar_Black:
          return 1;
        case BUSTYPE.BikerCar_RedTruck:
          return 1;
        case BUSTYPE.BikerCar_Blue:
          return 1;
        case BUSTYPE.GenericCar_CyanStripeCar:
          return 1;
        case BUSTYPE.GenericCar_BlackRedVan:
          return 1;
        default:
          throw new Exception("You added a new bus and forgot about it");
      }
    }

    internal static int GetBusCost(BUSTYPE bus)
    {
      switch (bus)
      {
        case BUSTYPE.StartingBus_01:
          return 20000;
        case BUSTYPE.BiggerBus_02:
          return 38000;
        case BUSTYPE.LargeBus_03:
          return 55000;
        case BUSTYPE.DoubleDeckerBus_04:
          return 90000;
        default:
          throw new Exception("You added a new bus and forgot about it");
      }
    }

    internal static float GetBusTravelTime(BUSTYPE bus) => Z_GameFlags.SecondsInDay / 5f;

    internal static Rectangle GetBusRectangle(BUSTYPE bus, out Rectangle Wheels)
    {
      switch (bus)
      {
        case BUSTYPE.StartingBus_01:
          Wheels = new Rectangle(0, 1576, 78, 55);
          return new Rectangle(0, 755, 78, 55);
        case BUSTYPE.BiggerBus_02:
          Wheels = new Rectangle(170, 1576, 89, 55);
          return new Rectangle(79, 755, 89, 55);
        case BUSTYPE.LargeBus_03:
          Wheels = new Rectangle(169, 1576, 89, 55);
          return new Rectangle(169, 755, 89, 55);
        case BUSTYPE.DoubleDeckerBus_04:
          Wheels = new Rectangle(0, 1632, 93, 66);
          return new Rectangle(0, 688, 93, 66);
        case BUSTYPE.GarbageTruck:
          Wheels = new Rectangle(452, 1237, 87, 57);
          return new Rectangle(364, 1237, 87, 57);
        case BUSTYPE.Delivery_UPSVan:
          Wheels = new Rectangle(627, 1238, 86, 56);
          return new Rectangle(540, 1238, 86, 56);
        case BUSTYPE.Delivery_Truck:
          Wheels = new Rectangle(654, 1519, 78, 52);
          return new Rectangle(575, 1519, 78, 52);
        case BUSTYPE.Collection_Truck:
          Wheels = new Rectangle(395, 1573, 111, 58);
          return new Rectangle(283, 1573, 111, 58);
        case BUSTYPE.BlackLimo:
          Wheels = new Rectangle(591, 1128, 76, 32);
          return new Rectangle(514, 1128, 76, 32);
        case BUSTYPE.AnimalRescueVan:
          Wheels = new Rectangle(685, 1355, 69, 45);
          return new Rectangle(615, 1355, 69, 45);
        case BUSTYPE.BlackVan:
          Wheels = new Rectangle(819, 1356, 63, 44);
          return new Rectangle(755, 1356, 63, 44);
        case BUSTYPE.SafetyVan:
          Wheels = new Rectangle(662, 1402, 66, 48);
          return new Rectangle(595, 1402, 66, 48);
        case BUSTYPE.GenericCar_Taxi:
          Wheels = new Rectangle(621, 1204, 48, 33);
          return new Rectangle(572, 1204, 48, 33);
        case BUSTYPE.GenericCar_GreenCar:
          Wheels = new Rectangle(719, 1206, 48, 31);
          return new Rectangle(670, 1206, 48, 31);
        case BUSTYPE.GenericCar_PinkCar:
          Wheels = new Rectangle(778, 1401, 48, 31);
          return new Rectangle(729, 1401, 48, 31);
        case BUSTYPE.GenericCar_BlueCar:
          Wheels = new Rectangle(873, 1401, 45, 32);
          return new Rectangle(827, 1401, 45, 32);
        case BUSTYPE.BikerCar_Brown:
          Wheels = new Rectangle(532, 1039, 49, 44);
          return new Rectangle(582, 1039, 49, 44);
        case BUSTYPE.BikerCar_Black:
          Wheels = new Rectangle(632, 1039, 49, 44);
          return new Rectangle(682, 1039, 49, 44);
        case BUSTYPE.BikerCar_RedTruck:
          Wheels = new Rectangle(569, 1161, 48, 38);
          return new Rectangle(618, 1161, 48, 38);
        case BUSTYPE.BikerCar_Blue:
          Wheels = new Rectangle(514, 1088, 49, 39);
          return new Rectangle(564, 1088, 49, 39);
        case BUSTYPE.GenericCar_CyanStripeCar:
          Wheels = new Rectangle(530, 1487, 53, 30);
          return new Rectangle(530, 1456, 53, 30);
        case BUSTYPE.GenericCar_BlackRedVan:
          Wheels = new Rectangle(805, 1519, 71, 52);
          return new Rectangle(733, 1519, 71, 52);
        default:
          throw new Exception("You added a new bus and forgot about it");
      }
    }
  }
}
