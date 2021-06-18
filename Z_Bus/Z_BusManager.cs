// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.Z_BusManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_Bus.TrafficSystem;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus
{
  internal class Z_BusManager : GameObject
  {
    internal static List<Z_BusObject> busses;
    internal static List<Z_BusObject> OtherTraffic;
    internal static Vector2Int StartPosition;
    internal static bool BusWillPickUp;
    private float TargetXL;
    private bool HasDroppedPeople;
    private float DelayToDrop;
    private int TotalPeopleToDrop;
    private float BusMAX;
    private bool StartedNIght;
    private static bool ABusISWaitingToCollect;
    internal static float DropOffLocation;
    internal static float EndLocation;
    internal static Vector2 StartLocation;
    private static List<int> BussesOnTheirWayToDropOffByUID = new List<int>();
    private static List<int> BussesOnTheirWayToPickUp = new List<int>();
    private static TrafficQueue trafficqueue;

    public Z_BusManager(Player player)
    {
      if (Z_BusManager.StartPosition == null)
        Z_BusManager.StartPosition = new Vector2Int(0, 227);
      Z_BusManager.StartLocation = TileMath.GetTileToWorldSpace(Z_BusManager.StartPosition);
      Z_BusManager.DropOffLocation = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault() / 2, 0)).X;
      Z_BusManager.EndLocation = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault(), 0)).X;
      Z_BusManager.busses = new List<Z_BusObject>();
      for (int index = 0; index < player.busroutes.bustimes.Count; ++index)
        Z_BusManager.busses.Add(new Z_BusObject(player.busroutes.bustimes[index]));
      this.HasDroppedPeople = false;
      Z_BusManager.OtherTraffic = new List<Z_BusObject>();
      Z_BusManager.trafficqueue = new TrafficQueue();
    }

    internal static void AddGarbageTruck()
    {
      Z_BusManager.OtherTraffic.Add(new Z_BusObject((BusTimes) null, _overridetraffictype: BUSTYPE.GarbageTruck));
      Z_BusManager.OtherTraffic[Z_BusManager.OtherTraffic.Count - 1].StartDrivingIn(true, Z_BusManager.trafficqueue);
    }

    internal static void AddDropOffBus(int UID)
    {
      if (Z_BusManager.BussesOnTheirWayToDropOffByUID == null)
        Z_BusManager.BussesOnTheirWayToDropOffByUID = new List<int>();
      Z_BusManager.BussesOnTheirWayToDropOffByUID.Add(UID);
    }

    internal static void SendBusToPickPeopleUp() => Z_BusManager.BussesOnTheirWayToPickUp = Z_BusManager.BussesOnTheirWayToPickUp == null ? new List<int>() : throw new Exception("BALLZ");

    internal static void StartNewDay()
    {
      int[] TotalCounted = new int[10];
      int[] TotalByRoute = new int[10];
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
        ++TotalByRoute[(int) Z_BusManager.busses[index].RefBusTime.busroute];
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
        Z_BusManager.busses[index].StartNewDay(TotalByRoute, ref TotalCounted);
    }

    public bool NoBussesAreDroppingOff()
    {
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
      {
        if (Z_BusManager.busses[index].IsDroppingOff())
          return false;
      }
      return true;
    }

    public bool GetCanStartNight()
    {
      bool CanStartNight = true;
      if (CustomerManager.PeopleOutAndAbout > 0)
        return false;
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
        Z_BusManager.busses[index].GetCanStartNight(ref CanStartNight);
      return CanStartNight;
    }

    internal static bool TryToPutThisCustomerOnABus()
    {
      if (Z_BusManager.ABusISWaitingToCollect)
      {
        for (int index = 0; index < Z_BusManager.busses.Count; ++index)
        {
          if (Z_BusManager.busses[index].drivestate == DriveState.PickingUp && !Z_BusManager.busses[index].IsFullOfPeopleLeaving())
          {
            Z_BusManager.busses[index].AddPersonWhoWantsToLeave();
            return true;
          }
        }
      }
      return false;
    }

    public void UpdateZ_BusManager(float DeltaTime, Player player)
    {
      Z_BusManager.trafficqueue.UpdateTrafficQueue();
      if (!Z_GameFlags.HasStartedFirstDay)
        return;
      if (Z_GameFlags.PurchasedOrSoldABus)
      {
        Z_GameFlags.PurchasedOrSoldABus = false;
        for (int index = Z_BusManager.busses.Count - 1; index > -1; --index)
        {
          if ((Z_BusManager.busses[index].RefBusTime.busroute == BUSROUTE.Count || Z_BusManager.busses[index].RefBusTime.IsDeleted) && Z_BusManager.busses[index].drivestate == DriveState.None)
            Z_BusManager.busses.RemoveAt(0);
        }
        for (int index1 = 0; index1 < player.busroutes.bustimes.Count; ++index1)
        {
          bool flag = false;
          for (int index2 = Z_BusManager.busses.Count - 1; index2 > -1; --index2)
          {
            if (Z_BusManager.busses[index2].RefBusTime == player.busroutes.bustimes[index1] || Z_BusManager.busses[index2].RefBusTime.IsDeleted || (Z_BusManager.busses[index2].RefBusTime.busroute == BUSROUTE.Count || player.busroutes.bustimes[index1].busroute == BUSROUTE.Count))
              flag = true;
          }
          if (!flag)
            Z_BusManager.busses.Add(new Z_BusObject(player.busroutes.bustimes[index1], true));
        }
      }
      Z_BusManager.ABusISWaitingToCollect = false;
      bool flag1 = false;
      bool ParkIsOpen = Z_GameFlags.ParkIsOpen();
      for (int index = Z_BusManager.OtherTraffic.Count - 1; index > -1; --index)
      {
        Z_BusManager.OtherTraffic[index].UpdateZ_BusObject(DeltaTime, player, out bool _, ParkIsOpen);
        if (Z_BusManager.OtherTraffic[index].drivestate == DriveState.None)
          Z_BusManager.OtherTraffic.RemoveAt(index);
      }
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
      {
        bool StartJourneyNow;
        if (Z_BusManager.busses[index].UpdateZ_BusObject(DeltaTime, player, out StartJourneyNow, ParkIsOpen) && Z_BusManager.busses[index].RefBusTime.IsDeleted)
          flag1 = true;
        Z_BusManager.ABusISWaitingToCollect |= Z_BusManager.busses[index].drivestate == DriveState.PickingUp;
        if (StartJourneyNow)
          Z_BusManager.AddDropOffBus(Z_BusManager.busses[index].RefBusTime.UID);
      }
      if (flag1)
      {
        for (int index = Z_BusManager.busses.Count - 1; index > -1; --index)
        {
          if (Z_BusManager.busses[index].RefBusTime.IsDeleted)
            Z_BusManager.busses.RemoveAt(index);
        }
      }
      if (Z_BusManager.BussesOnTheirWayToPickUp.Count <= 0 && !Z_BusManager.BusWillPickUp && Z_BusManager.BussesOnTheirWayToDropOffByUID.Count <= 0)
        return;
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
      {
        int uid = Z_BusManager.busses[index].RefBusTime.UID;
        int num = Z_BusManager.BussesOnTheirWayToDropOffByUID[0];
      }
      BusData.GetBusCapacity(Z_BusManager.busses[0].bus_type);
      bool flag2 = false;
      int num1 = Z_BusManager.BussesOnTheirWayToDropOffByUID.Count <= 0 ? Z_BusManager.BussesOnTheirWayToPickUp[0] : Z_BusManager.BussesOnTheirWayToDropOffByUID[0];
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
      {
        if (!flag2 && Z_BusManager.busses[index].drivestate == DriveState.None && Z_BusManager.busses[index].RefBusTime.UID == num1)
        {
          flag2 = true;
          Z_BusManager.busses[index].StartDrivingIn(Z_BusManager.BussesOnTheirWayToDropOffByUID.Count > 0, Z_BusManager.trafficqueue);
        }
      }
      if (!flag2)
        throw new Exception("NOT POSSIBLE");
      if (Z_BusManager.BussesOnTheirWayToDropOffByUID.Count > 0)
        Z_BusManager.BussesOnTheirWayToDropOffByUID.RemoveAt(0);
      else
        Z_BusManager.BussesOnTheirWayToPickUp.RemoveAt(0);
      Z_BusManager.BusWillPickUp = false;
    }

    private void DriveInBus()
    {
      if (!Z_BusManager.BusWillPickUp)
        this.TotalPeopleToDrop = 10;
      this.DrawOrigin.Y = 48f;
      this.vLocation = TileMath.GetTileToWorldSpace(Z_BusManager.StartPosition);
      this.vLocation.X = (float) -this.DrawRect.Width;
      this.TargetXL = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault() / 2, 0)).X;
      this.BusMAX = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault(), 0)).X;
      this.BusMAX += (float) this.DrawRect.Width;
    }

    public void DrawZ_BusManager()
    {
      for (int index = 0; index < Z_BusManager.busses.Count; ++index)
        Z_BusManager.busses[index].DrawZ_BusObject();
      for (int index = 0; index < Z_BusManager.OtherTraffic.Count; ++index)
        Z_BusManager.OtherTraffic[index].DrawZ_BusObject();
    }
  }
}
