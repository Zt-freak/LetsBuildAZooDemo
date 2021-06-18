// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.BusTimetable.BusTimes
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.BusTimetable
{
  internal class BusTimes
  {
    public float TravelTime;
    public BUSROUTE busroute;
    public float StartTime;
    public BUSTYPE bustype;
    public int UID;
    public bool IsDeleted;

    public BusTimes(BUSTYPE _bustype, BUSROUTE route)
    {
      this.UID = BusInfo.BUSUID_Cntr;
      ++BusInfo.BUSUID_Cntr;
      this.busroute = route;
      this.bustype = _bustype;
      this.TravelTime = BusTimes.GetRouteTime(route);
    }

    public static float GetRouteTime(BUSROUTE route) => route != BUSROUTE.Route01 ? (float) (((double) route + 1.0) * (double) Z_GameFlags.SecondsInDay / 48.0) : Z_GameFlags.SecondsInDay / 12f;

    internal static int GetMaintenenceCost() => 75;

    internal static string GetBusRouteName(BUSROUTE route, bool IncludeNewLine = true)
    {
      string str = "~";
      if (!IncludeNewLine)
        str = " ";
      switch (route)
      {
        case BUSROUTE.Route01:
          return "Horizon" + str + "Village";
        case BUSROUTE.Route02:
          return "Factory" + str + "District";
        case BUSROUTE.Route03:
          return "Riverside" + str + "Suburbs";
        case BUSROUTE.Route04:
          return "The Old" + str + "Pines";
        case BUSROUTE.Route05:
          return "High Rise" + str + "Apartments";
        case BUSROUTE.Route06:
          return "Countryside" + str + "Farms";
        case BUSROUTE.Route07:
          return "The City" + str + "center";
        case BUSROUTE.Route08:
          return "Golden Hill" + str + "Retirement";
        case BUSROUTE.Route09:
          return "Grand Manor" + str + "Acres";
        case BUSROUTE.Route10:
          return "Coastal" + str + "Fishery";
        default:
          return "NOT FOUND";
      }
    }

    internal static string GetBusName(BUSTYPE bustype)
    {
      switch (bustype)
      {
        case BUSTYPE.StartingBus_01:
          return "Mini Bus";
        case BUSTYPE.BiggerBus_02:
          return "Coach";
        case BUSTYPE.LargeBus_03:
          return "Bus";
        case BUSTYPE.DoubleDeckerBus_04:
          return "Double Decker";
        default:
          throw new Exception("asof");
      }
    }

    internal static float GetPopulationMultiplier(BUSROUTE route)
    {
      switch (route)
      {
        case BUSROUTE.Route01:
          return 0.3f;
        case BUSROUTE.Route02:
          return 0.44f;
        case BUSROUTE.Route03:
          return 0.6f;
        case BUSROUTE.Route04:
          return 0.4f;
        case BUSROUTE.Route05:
          return 0.9f;
        case BUSROUTE.Route06:
          return 0.3f;
        case BUSROUTE.Route07:
          return 1f;
        case BUSROUTE.Route08:
          return 0.5f;
        case BUSROUTE.Route09:
          return 0.2f;
        case BUSROUTE.Route10:
          return 0.4f;
        default:
          return 1f;
      }
    }

    public void SaveBusTime(Writer writer)
    {
      writer.WriteInt("b", (int) this.busroute);
      writer.WriteInt("b", (int) this.bustype);
      writer.WriteInt("b", this.UID);
    }

    public BusTimes(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("b", ref _out);
      this.busroute = (BUSROUTE) _out;
      int num2 = (int) reader.ReadInt("b", ref _out);
      this.bustype = (BUSTYPE) _out;
      int num3 = (int) reader.ReadInt("b", ref this.UID);
    }
  }
}
