// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirspaceManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirspaceManager
  {
    private AirVehicleManager airvehiclemanager;

    public AirspaceManager() => this.airvehiclemanager = new AirVehicleManager();

    public bool IsSomethingOnOrderToThisPen(int CellUID) => this.airvehiclemanager.IsSomethingOnOrderToThisPen(CellUID);

    public void AddChinookDelivery(
      WaveInfo waveinfo,
      Vector2 DeliveryLocationInWorldSpace,
      int OrderUID,
      bool black,
      bool SnapToSideOfScreen = false,
      int PenUID = -1,
      bool usecrisprcrate = false)
    {
      Vector2 destination = DeliveryLocationInWorldSpace;
      AirVehicle vehicle = new AirVehicle();
      vehicle.SetUpAsDeliveryChinook(destination, waveinfo, OrderUID, black, SnapToSideOfScreen, PenUID, usecrisprcrate);
      this.airvehiclemanager.AddAirVehicle(vehicle);
    }

    public void AddDroneDelivery(
      WaveInfo waveinfo,
      Vector2 startlocation,
      Vector2 DeliveryLocationInWorldSpace,
      int OrderUID,
      int PenUID = -1,
      bool usecrisprcrate = false)
    {
      AirVehicle vehicle = new AirVehicle();
      vehicle.SetUpAsDrone(startlocation, DeliveryLocationInWorldSpace, waveinfo, OrderUID, PenUID, usecrisprcrate);
      this.airvehiclemanager.AddAirVehicle(vehicle);
    }

    public bool ThisIsOnOrder(int OrderUID) => this.airvehiclemanager.ThisIsOnOrder(OrderUID);

    public void AddBuildingWithRide(
      Vector2Int Location,
      TILETYPE thingYouBuilt,
      int RotationClockWise,
      bool IsMove,
      int Shop_UID)
    {
      Vector2Int Location1 = Location;
      switch (thingYouBuilt)
      {
        case TILETYPE.HelicopterRide:
          switch (RotationClockWise)
          {
            case 0:
              Location1.Y -= 4;
              break;
            case 1:
              ++Location1.X;
              Location1.Y -= 2;
              break;
            case 2:
              Location1.Y -= 2;
              break;
            case 3:
              --Location1.X;
              Location1.Y -= 2;
              break;
          }
          break;
        case TILETYPE.HotAirBalloonRide:
        case TILETYPE.CorruptedHotAirBalloonRide:
          switch (RotationClockWise)
          {
            case 0:
              Location1.Y -= 4;
              break;
            case 1:
              ++Location1.X;
              Location1.Y -= 2;
              break;
            case 2:
              Location1.Y -= 2;
              break;
            case 3:
              --Location1.X;
              Location1.Y -= 2;
              break;
          }
          break;
      }
      Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(Location1);
      if (IsMove)
        return;
      switch (thingYouBuilt)
      {
        case TILETYPE.HelicopterRide:
          this.AddHelicopterRide(tileToWorldSpace, Shop_UID);
          break;
        case TILETYPE.HotAirBalloonRide:
        case TILETYPE.CorruptedHotAirBalloonRide:
          this.AddBalloonRide(tileToWorldSpace, Shop_UID);
          break;
      }
    }

    public void AddBalloonRide(Vector2 location, int shopUID)
    {
      AirVehicle vehicle = new AirVehicle();
      vehicle.SetUpAsBalloonRide(location, 80f, shopUID);
      this.airvehiclemanager.AddAirVehicle(vehicle);
    }

    public void AddHelicopterRide(Vector2 location, int shopUID)
    {
      AirVehicle vehicle = new AirVehicle();
      vehicle.SetUpAsHelicopterRide(location, 60f, shopUID);
      this.airvehiclemanager.AddAirVehicle(vehicle);
    }

    public void UpdateAirspaceManager(float SimulationTime, float DeltaTime, Player player) => this.airvehiclemanager.UpdateAirVehicleManager(SimulationTime, player);

    public void DrawAirspaceManager() => this.airvehiclemanager.DrawAirVehicleManager();
  }
}
