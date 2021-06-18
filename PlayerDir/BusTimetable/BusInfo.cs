// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.BusTimetable.BusInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.BusTimetable
{
  internal class BusInfo
  {
    public List<BusTimes> bustimes;
    internal static int BUSUID_Cntr;
    private int[] BussesNotInUse;
    public bool[] UnlockedRoutes;

    public BusInfo()
    {
      this.Create();
      this.AddBus(BUSTYPE.StartingBus_01, BUSROUTE.Route01, (Player) null);
    }

    private void Create()
    {
      this.bustimes = new List<BusTimes>();
      this.BussesNotInUse = new int[4];
      this.UnlockedRoutes = new bool[10];
      this.UnlockedRoutes[0] = true;
    }

    public float[] GetBusByRouteArray(bool AddMoreForBetterBusses = true)
    {
      float[] numArray = new float[10];
      for (int index = this.bustimes.Count - 1; index > -1; --index)
      {
        if (this.bustimes[index].busroute != BUSROUTE.Count)
        {
          if (AddMoreForBetterBusses)
            numArray[(int) this.bustimes[index].busroute] += (float) this.bustimes[index].bustype + 1f;
          else
            ++numArray[(int) this.bustimes[index].busroute];
        }
      }
      return numArray;
    }

    public void TransferInactiveBusToThisoute(BUSROUTE route, BUSTYPE bustype, Player player)
    {
      int publicity1 = PublicityCalculator.CalculatePublicity(player);
      ParkRating.NeedsRecalculating = true;
      PublicityCalculator.RecalculatePublicity = true;
      if (this.BussesNotInUse[(int) bustype] <= 0)
        throw new Exception("NO BUSES TO TRANFER");
      --this.BussesNotInUse[(int) bustype];
      this.bustimes.Add(new BusTimes(bustype, route));
      this.ResetAllBusTimes();
      Z_GameFlags.PurchasedOrSoldABus = true;
      int publicity2 = PublicityCalculator.CalculatePublicity(player);
      NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
    }

    public void RemoveABusFromThisoute(BUSROUTE route, BUSTYPE bustype, Player player)
    {
      int publicity1 = PublicityCalculator.CalculatePublicity(player);
      ParkRating.NeedsRecalculating = true;
      PublicityCalculator.RecalculatePublicity = true;
      for (int index = this.bustimes.Count - 1; index > -1; --index)
      {
        if (this.bustimes[index].busroute == route && this.bustimes[index].bustype == bustype)
        {
          this.bustimes[index].IsDeleted = true;
          ++this.BussesNotInUse[(int) bustype];
          this.bustimes.RemoveAt(index);
          Z_GameFlags.PurchasedOrSoldABus = true;
          int publicity2 = PublicityCalculator.CalculatePublicity(player);
          NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
          return;
        }
      }
      throw new Exception("DID NOT FIND BUS TO REMOVE");
    }

    public int[] GetBussesNotInUse() => this.BussesNotInUse;

    public int[] GetBusCount(BUSROUTE busroute)
    {
      List<BusTimes> bussesByRoute = this.GetBussesByRoute(busroute);
      int[] numArray = new int[4];
      for (int index = 0; index < bussesByRoute.Count; ++index)
        ++numArray[(int) bussesByRoute[index].bustype];
      return numArray;
    }

    public int[] GetBusCountAllRoutes()
    {
      int[] numArray = new int[10];
      for (int index = 0; index < this.bustimes.Count; ++index)
        ++numArray[(int) this.bustimes[index].busroute];
      return numArray;
    }

    public int GetTotalBussesOwned(BUSTYPE bustype)
    {
      int num = 0;
      for (int index = 0; index < this.bustimes.Count; ++index)
      {
        if (bustype == BUSTYPE.Count)
          ++num;
        else if (this.bustimes[index].bustype == bustype)
          ++num;
      }
      return num + this.BussesNotInUse[(int) bustype];
    }

    public bool ThisRouteIsUnLocked(BUSROUTE busroute) => this.UnlockedRoutes[(int) busroute];

    public int GetAllPurchasedBasses()
    {
      int count = this.bustimes.Count;
      for (int index = 0; index < this.BussesNotInUse.Length; ++index)
        count += this.BussesNotInUse[index];
      return count;
    }

    public int GetAllActiveBusses() => this.bustimes.Count;

    public List<BusTimes> GetBussesByRoute(
      BUSROUTE busroute,
      BUSTYPE bustype = BUSTYPE.Count,
      bool ExcludeThisBus = false)
    {
      List<BusTimes> busTimesList = new List<BusTimes>();
      for (int index = 0; index < this.bustimes.Count; ++index)
      {
        if (this.bustimes[index].busroute == busroute)
        {
          if (bustype == BUSTYPE.Count)
            busTimesList.Add(this.bustimes[index]);
          else if (bustype == this.bustimes[index].bustype && !ExcludeThisBus)
            busTimesList.Add(this.bustimes[index]);
          else if (ExcludeThisBus && bustype != this.bustimes[index].bustype)
            busTimesList.Add(this.bustimes[index]);
        }
      }
      return busTimesList;
    }

    public void AddBus(BUSTYPE bustype, BUSROUTE busroute, Player player)
    {
      int num1 = 0;
      if (player != null)
        num1 = PublicityCalculator.CalculatePublicity(player);
      ParkRating.NeedsRecalculating = true;
      PublicityCalculator.RecalculatePublicity = true;
      this.bustimes.Add(new BusTimes(bustype, busroute));
      int num2 = 0;
      for (int index = 0; index < this.bustimes.Count; ++index)
      {
        if (this.bustimes[index].busroute == busroute)
          ++num2;
      }
      float num3 = BusTimes.GetRouteTime(busroute) / (float) num2;
      for (int index = 0; index < this.bustimes.Count; ++index)
      {
        if (this.bustimes[index].busroute == busroute)
          this.bustimes[index].StartTime = num3 * (float) index;
      }
      Z_GameFlags.PurchasedOrSoldABus = true;
      if (player != null)
        QuestScrubber.ScrubOnBuyingBus(player);
      if (player == null)
        return;
      int publicity = PublicityCalculator.CalculatePublicity(player);
      NotificationBubbleManager.QuickAddNotification((float) num1, (float) publicity, BubbleMainType.Publicity);
    }

    public void SaveBusInfo(Writer writer)
    {
      writer.WriteInt("b", BusInfo.BUSUID_Cntr);
      writer.WriteInt("b", this.BussesNotInUse.Length);
      for (int index = 0; index < this.BussesNotInUse.Length; ++index)
        writer.WriteInt("b", this.BussesNotInUse[index]);
      writer.WriteInt("b", this.UnlockedRoutes.Length);
      for (int index = 0; index < this.UnlockedRoutes.Length; ++index)
        writer.WriteBool("b", this.UnlockedRoutes[index]);
      writer.WriteInt("b", this.bustimes.Count);
      for (int index = 0; index < this.bustimes.Count; ++index)
        this.bustimes[index].SaveBusTime(writer);
      this.ResetAllBusTimes();
    }

    private void ResetAllBusTimes()
    {
      int[] busCountAllRoutes = this.GetBusCountAllRoutes();
      int[] numArray = new int[10];
      for (int index = 0; index < this.bustimes.Count; ++index)
      {
        float routeTime = BusTimes.GetRouteTime(this.bustimes[index].busroute);
        this.bustimes[index].TravelTime = routeTime;
        float num = routeTime / (float) busCountAllRoutes[(int) this.bustimes[index].busroute];
        this.bustimes[index].StartTime = num * (float) numArray[(int) this.bustimes[index].busroute];
        ++numArray[(int) this.bustimes[index].busroute];
      }
    }

    public BusInfo(Reader reader)
    {
      this.Create();
      int num1 = (int) reader.ReadInt("b", ref BusInfo.BUSUID_Cntr);
      int _out = 0;
      int num2 = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num3 = (int) reader.ReadInt("b", ref this.BussesNotInUse[index]);
      }
      int num4 = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
      {
        int num3 = (int) reader.ReadBool("b", ref this.UnlockedRoutes[index]);
      }
      int num5 = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
        this.bustimes.Add(new BusTimes(reader));
      if (this.bustimes.Count != 0)
        return;
      int num6 = 0;
      for (int index = 0; index < this.BussesNotInUse.Length; ++index)
        ++num6;
      if (num6 != 0)
        return;
      this.AddBus(BUSTYPE.StartingBus_01, BUSROUTE.Route01, (Player) null);
    }
  }
}
