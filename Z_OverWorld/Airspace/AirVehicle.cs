// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirVehicle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirVehicle : AnimatedGameObject
  {
    private static Rectangle shadowRect = new Rectangle(1669, 432, 40, 25);
    public static Rectangle rideHeliShadow = new Rectangle(236, 1355, 56, 14);
    public static Vector2 rideHeliShadowOrigin = new Vector2(36f, 7f);
    private Vector2 altitudeoffset;
    private static Rectangle newDeliveryChinookRect = new Rectangle(0, 1292, 115, 62);
    private static Vector2 newDeliveryChinookOrigin = new Vector2(62f, 48f);
    private static Rectangle blackHelicopterRect = new Rectangle(348, 1295, 97, 59);
    private static Vector2 blackHelicopterOrigin = new Vector2(43f, 46f);
    private static Rectangle colourfulBalloonRect = new Rectangle(141, 1197, 66, 89);
    private static Rectangle penguinBalloonRect = new Rectangle(0, 1107, 70, 89);
    private static Rectangle monkeyBalloonRect = new Rectangle(214, 1107, 80, 89);
    private static Rectangle leopardBalloonRect = new Rectangle(376, 1107, 68, 89);
    private Vector2 colourfulBalloonOrigin = new Vector2(33f, 84f);
    public static Rectangle helicopterE = new Rectangle(305, 1355, 61, 46);
    public static Rectangle helicopterS = new Rectangle(0, 1468, 51, 57);
    public static Rectangle helicopterW = new Rectangle(0, 1355, 60, 46);
    public static Rectangle helicopterN = new Rectangle(0, 1402, 51, 65);
    public static Rectangle helicopterFlyE = new Rectangle(491, 1355, 61, 46);
    public static Rectangle helicopterFlyS = new Rectangle(157, 1468, 51, 57);
    public static Rectangle helicopterFlyW = new Rectangle(183, 1355, 60, 46);
    public static Rectangle helicopterFlyN = new Rectangle(156, 1402, 51, 65);
    public static Rectangle helicopterNW = new Rectangle(260, 1402, 53, 57);
    public static Rectangle helicopterNE = new Rectangle(260, 1460, 53, 57);
    public static Rectangle helicopterSE = new Rectangle(285, 1518, 57, 49);
    public static Rectangle helicopterSW = new Rectangle(0, 1526, 56, 49);
    public static Rectangle helicopterFlyNW = new Rectangle(419, 1402, 52, 57);
    public static Rectangle helicopterFlyNE = new Rectangle(422, 1460, 53, 57);
    public static Rectangle helicopterFlySE = new Rectangle(459, 1518, 57, 49);
    public static Rectangle helicopterFlySW = new Rectangle(171, 1526, 56, 49);
    public static Vector2 helicopterEOrigin = new Vector2(35f, 36f);
    public static Vector2 helicopterSOrigin = new Vector2(25f, 47f);
    public static Vector2 helicopterWOrigin = new Vector2(25f, 35f);
    public static Vector2 helicopterNOrigin = new Vector2(25f, 36f);
    public static Vector2 helicopterNWOrigin = new Vector2(25f, 36f);
    public static Vector2 helicopterNEOrigin = new Vector2(27f, 36f);
    public static Vector2 helicopterSEOrigin = new Vector2(31f, 38f);
    public static Vector2 helicopterSWOrigin = new Vector2(25f, 38f);
    public static Rectangle droneRect = new Rectangle(732, 1044, 55, 40);
    public static Vector2 droneOrigin = new Vector2(28f, 23f);
    private int DeliveringToThiusPenUID;
    public GameObject shadow;
    public AirVehicleType vehicletype;
    private AirVehicleBehaviourType behaviourtype;
    private List<Vector2> anchorpositions;
    protected AirVehicleBehaviour behaviour;
    public float altitude;
    private WaveInfo delivery;
    public Vector2 drawoffset;
    private bool usecrisprcrate;
    private float flightduration;
    public bool active = true;
    private int OrderUID;
    private int shopUID;
    public float alpha = 1f;

    public AirVehicle()
    {
      this.anchorpositions = new List<Vector2>();
      this.shadow = new GameObject();
      this.shadow.DrawRect = AirVehicle.shadowRect;
      this.shadow.SetDrawOriginToCentre();
    }

    public void SetUpAsDeliveryChinook(
      Vector2 destination,
      WaveInfo delivery_,
      int _OrderUID,
      bool black = false,
      bool SnapToSideOfScreen = false,
      int PENUID = -1,
      bool usecrisprcrate_ = false)
    {
      this.behaviourtype = AirVehicleBehaviourType.AnimalDelivery;
      this.DeliveringToThiusPenUID = PENUID;
      this.usecrisprcrate = usecrisprcrate_;
      if (black)
      {
        this.vehicletype = AirVehicleType.BlackChinook;
        this.DrawRect = AirVehicle.blackHelicopterRect;
        this.DrawOrigin = AirVehicle.blackHelicopterOrigin;
      }
      else
      {
        this.vehicletype = AirVehicleType.DeliveryChinook;
        this.DrawRect = AirVehicle.newDeliveryChinookRect;
        this.DrawOrigin = AirVehicle.newDeliveryChinookOrigin;
      }
      this.delivery = delivery_;
      this.anchorpositions.Add(destination);
      this.altitude = 100f;
      this.SetUpSimpleAnimation(3, 0.1f);
      this.OrderUID = _OrderUID;
      this.behaviour = this.GetBehaviour(this.behaviourtype, SnapToSideOfScreen);
    }

    public void SetUpAsDrone(
      Vector2 startpoint_,
      Vector2 destination_,
      WaveInfo delivery_,
      int _OrderUID,
      int penuid = -1,
      bool usecrisprcrate_ = false)
    {
      this.behaviourtype = AirVehicleBehaviourType.AnimalDelivery;
      this.DeliveringToThiusPenUID = penuid;
      this.usecrisprcrate = usecrisprcrate_;
      this.vehicletype = AirVehicleType.Drone;
      this.DrawRect = AirVehicle.droneRect;
      this.DrawOrigin = AirVehicle.droneOrigin;
      this.delivery = delivery_;
      this.anchorpositions.Add(startpoint_);
      this.anchorpositions.Add(destination_);
      this.altitude = 100f;
      this.SetUpSimpleAnimation(3, 0.1f);
      this.OrderUID = _OrderUID;
      this.behaviour = this.GetBehaviour(this.behaviourtype);
    }

    public void SetUpAsBalloonRide(Vector2 startpoint, float flightduration_, int shopUID_)
    {
      this.vehicletype = AirVehicleType.RideBalloon;
      this.behaviourtype = AirVehicleBehaviourType.BalloonRide;
      this.flightduration = flightduration_;
      this.anchorpositions.Add(startpoint);
      this.DrawRect = AirVehicle.colourfulBalloonRect;
      this.DrawOrigin = this.colourfulBalloonOrigin;
      this.SetUpSimpleAnimation(1, 60f);
      this.behaviour = this.GetBehaviour(this.behaviourtype);
      this.shopUID = shopUID_;
    }

    public void SetUpAsHelicopterRide(Vector2 startpoint, float flightduration_, int shopUID_)
    {
      this.vehicletype = AirVehicleType.RideHelicopter;
      this.behaviourtype = AirVehicleBehaviourType.HelicopterRide;
      this.flightduration = flightduration_;
      this.anchorpositions.Add(startpoint);
      this.DrawRect = AirVehicle.helicopterE;
      this.DrawOrigin = AirVehicle.helicopterEOrigin;
      this.SetUpSimpleAnimation(1, 60f);
      this.behaviour = this.GetBehaviour(this.behaviourtype);
      this.shopUID = shopUID_;
    }

    private AirVehicleBehaviour GetBehaviour(
      AirVehicleBehaviourType behaviourtype,
      bool SnapToSideOfScreen = false)
    {
      switch (behaviourtype)
      {
        case AirVehicleBehaviourType.AnimalDelivery:
          return (AirVehicleBehaviour) new DeliveryAirBehaviour(this, this.anchorpositions, this.delivery, this.OrderUID, this.usecrisprcrate, SnapToSideOfScreen, this.DeliveringToThiusPenUID, this.vehicletype == AirVehicleType.Drone);
        case AirVehicleBehaviourType.BalloonRide:
          return (AirVehicleBehaviour) new BalloonRideBehaviour(this, this.anchorpositions[0], this.flightduration);
        case AirVehicleBehaviourType.HelicopterRide:
          return (AirVehicleBehaviour) new HelicopterRideBehaviour(this, this.anchorpositions[0], this.flightduration);
        default:
          throw new NotImplementedException();
      }
    }

    public bool UpdateAirVehicle(float SimulationTime, Player player)
    {
      this.SetAlpha(this.alpha);
      this.shadow.SetAlpha(this.alpha);
      this.behaviour.alpha = this.alpha;
      if (TrailerDemoFlags.HasTrailerFlag && this.vehicletype == AirVehicleType.DeliveryChinook)
        SimulationTime *= TrailerDemoFlags.ChopperMoveSpeed;
      this.UpdateAnimation(SimulationTime);
      return this.behaviour.UpdateAirVehicleBehaviour(SimulationTime, player);
    }

    public bool ThisIsOnOrder(int OrderUID) => this.behaviour.ThisIsOnOrder(OrderUID);

    public bool IsSomethingOnOrderToThisPen(int CellUID) => this.behaviour.IsSomethingOnOrderToThisPen(CellUID);

    public void DrawAirVehicle(SpriteBatch spritebatch, Vector2 offset)
    {
      this.altitudeoffset = new Vector2(0.0f, -this.altitude);
      this.altitudeoffset.Y *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.shadow.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, offset + this.vLocation, 1f, 0.0f);
      this.behaviour.DrawAirVehicleBehaviour(spritebatch, offset + this.vLocation + this.altitudeoffset);
      this.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, offset + this.vLocation + this.altitudeoffset + this.drawoffset, 1f, this.Rotation);
    }
  }
}
