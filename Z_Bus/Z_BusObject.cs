// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Bus.Z_BusObject
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Objects;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Tutorials;
using TinyZoo.Tutorials.Z_Tutorials;
using TinyZoo.Z_BalanceSystems.Customers;
using TinyZoo.Z_Bus.TrafficSystem;
using TinyZoo.Z_DayNight;
using TinyZoo.Z_Garbage;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Bus
{
  internal class Z_BusObject : GameObject
  {
    public DriveState drivestate;
    private float TargetXL;
    private float BusMAX;
    private static float BusSpeed = 85f;
    private float DelayToDrop;
    private int TotalPeopleToDrop;
    public BUSTYPE bus_type;
    public BusTimes RefBusTime;
    private float NextDriveTime;
    private int CustomersLeaving;
    public int BusCapacity;
    public float ForcedDropOffPause;
    private bool IsLastBus;
    private GameObject Chasis;
    private SinSocillator oscilator;
    private float TrafficGap;
    private BUSTYPE overridetraffictype;
    private BusExtrasRenderer busextrasmanager;
    private float OscilatorMultiplier;
    private float CarbonTimer;
    private int BinIndex;
    private float BusInfront;

    public Z_BusObject(BusTimes _bustimes, bool AddedDuringDay = false, BUSTYPE _overridetraffictype = BUSTYPE.Count)
    {
      this.overridetraffictype = _overridetraffictype;
      Z_BusObject.BusSpeed = 185f;
      this.RefBusTime = _bustimes;
      if (this.overridetraffictype != BUSTYPE.Count)
      {
        this.bus_type = this.overridetraffictype;
        if (this.bus_type == BUSTYPE.GarbageTruck)
          this.busextrasmanager = new BusExtrasRenderer();
      }
      else
        this.bus_type = this.RefBusTime.bustype;
      this.NextDriveTime = 0.0f;
      if (AddedDuringDay)
        this.NextDriveTime = Z_GameFlags.DayTimer;
      Rectangle Wheels;
      this.DrawRect = BusData.GetBusRectangle(this.bus_type, out Wheels);
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.BusMAX = Z_BusManager.EndLocation + (float) this.DrawRect.Width;
      this.BusCapacity = BusData.GetBusCapacity(this.bus_type);
      this.Chasis = new GameObject((GameObject) this);
      this.DrawRect = Wheels;
      this.oscilator = new SinSocillator(1.5f);
    }

    public void StartNewDay(int[] TotalByRoute, ref int[] TotalCounted)
    {
      this.IsLastBus = false;
      this.NextDriveTime = 0.0f;
      if (TotalByRoute[(int) this.RefBusTime.busroute] <= 0)
        return;
      if (TotalCounted[(int) this.RefBusTime.busroute] > 0)
      {
        float num = this.RefBusTime.StartTime / (float) TotalByRoute[(int) this.RefBusTime.busroute];
        this.NextDriveTime = Z_GameFlags.ZooOpenTime_InSeconds + num * (float) TotalCounted[(int) this.RefBusTime.busroute];
      }
      ++TotalCounted[(int) this.RefBusTime.busroute];
    }

    public bool IsDroppingOff() => this.drivestate == DriveState.DrivingInToDropOff || this.drivestate == DriveState.DroppingOff || this.drivestate == DriveState.DrivingAwayFromDropOff;

    public bool IsFullOfPeopleLeaving() => !this.IsLastBus && this.CustomersLeaving >= this.BusCapacity;

    public void AddPersonWhoWantsToLeave() => ++this.CustomersLeaving;

    public void GetCanStartNight(ref bool CanStartNight)
    {
      if (!CanStartNight)
        return;
      if (this.drivestate != DriveState.DrivingAwayFromDropOff && this.drivestate != DriveState.None)
        CanStartNight = false;
      if (this.drivestate != DriveState.DrivingAwayFromDropOff || (double) this.vLocation.X >= (double) Z_BusManager.DropOffLocation + 100.0)
        return;
      CanStartNight = false;
    }

    public void StartDrivingIn(bool IsForDropOff, TrafficQueue trafficqueue)
    {
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.BusMAX = Z_BusManager.EndLocation + (float) this.DrawRect.Width;
      this.CustomersLeaving = 0;
      this.TrafficGap = (float) TinyZoo.Game1.Rnd.Next(12, 20);
      trafficqueue.AddVehicleToRoad(this);
      this.CarbonTimer = (float) TinyZoo.Game1.Rnd.Next(30, 50);
      this.CarbonTimer *= 0.1f;
      this.vLocation = Z_BusManager.StartLocation;
      this.vLocation.Y += 8f;
      this.vLocation.X = Z_BusManager.StartLocation.X - (float) this.DrawRect.Width;
      if (TutorialManager.currenttutorial == TUTORIALTYPE.StartTheDay || Z_GameFlags.BusStartsOnSCreenEdge)
      {
        Z_GameFlags.BusStartsOnSCreenEdge = false;
        float x = RenderMath.TranslateScreenSpaceToWorldSpace(Vector2.Zero).X;
        if ((double) x < (double) Z_BusManager.DropOffLocation)
        {
          this.vLocation.X = x - (float) this.DrawRect.Width;
          this.vLocation.X -= 200f;
        }
        StartTheDayTutorial.DoPanNow = true;
      }
      this.drivestate = DriveState.DrivingIn;
      if (IsForDropOff)
      {
        this.drivestate = DriveState.DrivingInToDropOff;
        int num = this.IsLastBus ? 1 : 0;
      }
      if (this.bus_type == BUSTYPE.GarbageTruck)
        this.TargetXL = Z_GarbageManager.GetBinLocation(out this.BinIndex);
      else
        this.TargetXL = Z_BusManager.DropOffLocation;
    }

    public bool UpdateZ_BusObject(
      float DeltaTime,
      Player player,
      out bool StartJourneyNow,
      bool ParkIsOpen)
    {
      this.oscilator.UpdateSinOscillator(DeltaTime);
      StartJourneyNow = false;
      if (this.drivestate == DriveState.None)
      {
        if ((double) Z_GameFlags.DayTimer >= (double) this.NextDriveTime & ParkIsOpen && !this.IsLastBus)
        {
          StartJourneyNow = true;
          this.NextDriveTime = Z_GameFlags.DayTimer + BusTimes.GetRouteTime(this.RefBusTime.busroute);
        }
      }
      else
      {
        this.CarbonTimer -= DeltaTime;
        if ((double) this.CarbonTimer <= 0.0)
        {
          Player.carbon.DropCarbon(this.vLocation, 10);
          this.CarbonTimer = (float) TinyZoo.Game1.Rnd.Next(30, 50);
          this.CarbonTimer *= 0.1f;
        }
        float SpeedMult = 1f;
        this.GetSpeedMultipler(ref SpeedMult);
        this.OscilatorMultiplier = SpeedMult;
        if (this.drivestate == DriveState.DrivingIn || this.drivestate == DriveState.DrivingInToDropOff)
        {
          float num1 = this.TargetXL - this.vLocation.X;
          float num2;
          if ((double) num1 < 70.0)
          {
            num2 = num1 / 70f;
            if ((double) num2 < 0.100000001490116)
              num2 = 0.1f;
            this.OscilatorMultiplier *= num2;
          }
          else
            num2 = 1f;
          this.oscilator.UpdateSinOscillator((float) ((double) DeltaTime * (double) Z_BusObject.BusSpeed * (double) num2 * 0.00999999977648258 * 0.5));
          this.vLocation.X += DeltaTime * Z_BusObject.BusSpeed * num2 * SpeedMult;
          if ((double) this.vLocation.X >= (double) this.TargetXL)
          {
            this.vLocation.X = this.TargetXL;
            int busType = (int) this.bus_type;
            if (this.drivestate == DriveState.DrivingIn)
            {
              this.drivestate = DriveState.PickingUp;
            }
            else
            {
              this.drivestate = DriveState.DroppingOff;
              float num3 = Z_GameFlags.SecondsInDay / 24f * 0.7f;
              if (this.bus_type < BUSTYPE.Count)
              {
                this.IsLastBus = (double) Z_GameFlags.DayTimer + (double) BusTimes.GetRouteTime(this.RefBusTime.busroute) + (double) num3 > (double) Z_GameFlags.GetClosingTime();
                if (!this.IsLastBus)
                  this.TotalPeopleToDrop = NewCustomerCalculator.GetPeopleAtThisBusStop(this.RefBusTime.busroute, this.BusCapacity, this.IsLastBus);
                else if (this.TotalPeopleToDrop != 0)
                  throw new Exception("How is this the case?");
                if (this.IsLastBus)
                {
                  CustomerManager.Recall();
                  NewCustomerCalculator.GetPeopleAtThisBusStop(this.RefBusTime.busroute, this.BusCapacity, this.IsLastBus);
                }
              }
              else if (this.bus_type == BUSTYPE.GarbageTruck)
                this.ForcedDropOffPause = 3.5f;
            }
          }
        }
        else if (this.drivestate == DriveState.PickingUp)
        {
          if (this.IsLastBus)
          {
            CustomerManager.SpeedUpRecall = true;
            if (CustomerManager.PeopleOutAndAbout == 0)
            {
              this.drivestate = DriveState.DrivingAway;
              CustomerManager.SpeedUpRecall = false;
            }
          }
          else
            this.drivestate = DriveState.DrivingAway;
        }
        else if (this.drivestate == DriveState.DroppingOff)
        {
          if (this.bus_type == BUSTYPE.GarbageTruck)
          {
            if ((double) this.ForcedDropOffPause > 0.0)
            {
              this.ForcedDropOffPause -= DeltaTime;
              if ((double) this.ForcedDropOffPause <= 0.0)
                this.busextrasmanager.StartCollectingGarbage(true);
            }
            else if (this.busextrasmanager.bActive)
            {
              this.busextrasmanager.UpdateBusExtrasRenderer(DeltaTime);
            }
            else
            {
              Z_GarbageManager.CollectedGarbage(this.BinIndex);
              this.drivestate = DriveState.DrivingAway;
            }
          }
          else if ((double) this.ForcedDropOffPause > 0.0)
          {
            this.ForcedDropOffPause -= DeltaTime;
          }
          else
          {
            this.DelayToDrop -= DeltaTime;
            if (this.IsLastBus)
              this.TotalPeopleToDrop = 0;
            if ((double) this.DelayToDrop < 0.0 && this.TotalPeopleToDrop > 0)
            {
              --this.TotalPeopleToDrop;
              bool flag = false;
              if (Z_GameFlags.SpecialPeopleOnBus.Count > 0)
              {
                if (Z_GameFlags.SpecialPeopleOnBus.Count <= this.TotalPeopleToDrop)
                {
                  flag = true;
                  CustomerManager.AddPerson(Z_GameFlags.SpecialPeopleOnBus[0].animaltype, cellblockcontainer: player.prisonlayout.cellblockcontainer, player: player);
                }
                else if (TinyZoo.Game1.Rnd.Next(0, this.TotalPeopleToDrop) < Z_GameFlags.SpecialPeopleOnBus.Count)
                {
                  flag = true;
                  CustomerManager.AddPerson(Z_GameFlags.SpecialPeopleOnBus[0].animaltype, cellblockcontainer: player.prisonlayout.cellblockcontainer, player: player);
                }
                if (flag)
                  Z_GameFlags.SpecialPeopleOnBus.RemoveAt(0);
              }
              if (!flag)
                CustomerManager.AddPerson(cellblockcontainer: player.prisonlayout.cellblockcontainer, player: player);
              this.DelayToDrop = (float) TinyZoo.Game1.Rnd.Next(0, 100);
              this.DelayToDrop *= 0.01f;
              this.DelayToDrop += this.ForcedDropOffPause;
              this.ForcedDropOffPause = 0.0f;
            }
            if (this.TotalPeopleToDrop <= 0)
              this.drivestate = DriveState.PickingUp;
          }
        }
        else if (this.drivestate == DriveState.DrivingAway || this.drivestate == DriveState.DrivingAwayFromDropOff)
        {
          float num1 = this.vLocation.X - this.TargetXL;
          float num2;
          if ((double) num1 < 70.0)
          {
            num2 = num1 / 70f;
            if ((double) num2 < 0.100000001490116)
              num2 = 0.1f;
            this.OscilatorMultiplier *= num2;
          }
          else
            num2 = 1f;
          this.vLocation.X += DeltaTime * Z_BusObject.BusSpeed * num2 * SpeedMult;
          if ((double) this.vLocation.X > (double) this.BusMAX)
          {
            this.drivestate = DriveState.None;
            return true;
          }
        }
      }
      return false;
    }

    private void GetSpeedMultipler(ref float SpeedMult)
    {
      SpeedMult = 1f;
      float num = this.BusInfront - this.vLocation.X;
      if ((double) num >= 70.0)
        return;
      if ((double) num <= 0.0)
        SpeedMult = 0.0f;
      SpeedMult = num / 70f;
      if ((double) SpeedMult >= 0.100000001490116)
        return;
      SpeedMult = 0.1f;
    }

    public void SetLimit(float _BusInfront)
    {
      if ((double) this.vLocation.X > (double) _BusInfront)
        this.vLocation.X = this.BusInfront;
      this.BusInfront = _BusInfront;
    }

    public float GetBackOfBus() => this.vLocation.X - (this.DrawOrigin.X + this.TrafficGap * 2f) * Sengine.ScreenRatioUpwardsMultiplier.Y;

    public void DrawZ_BusObject()
    {
      if (this.drivestate == DriveState.None)
        return;
      this.SetAllColours(DayNightManager.SunShineValueR, DayNightManager.SunShineValueG, DayNightManager.SunShineValueB);
      this.Chasis.CloneColours((GameObject) this);
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
      this.Chasis.vLocation = this.vLocation;
      this.Chasis.vLocation.Y += (float) ((double) this.oscilator.CurrentOffset * 0.5 * (0.300000011920929 + (double) this.OscilatorMultiplier * 0.699999988079071));
      this.Chasis.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.AnimalSheet);
      if (this.busextrasmanager == null)
        return;
      this.busextrasmanager.DrawBusExtrasRenderer(this.Chasis.vLocation);
    }
  }
}
