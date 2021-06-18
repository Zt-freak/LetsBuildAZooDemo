// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.DeliveryAirBehaviour
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_HUD;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class DeliveryAirBehaviour : AirVehicleBehaviour
  {
    private Vector2 start;
    private Vector2 destination;
    private DeliveryAirBehaviour.DeliveryBehaviourState state;
    private AirDeliveryCrateAndRope crate;
    private List<Vector2> anchorpositions;
    private Z_BusStatus InboundArrow;
    private bool SnapToSideOfScreen;
    private int OrderUID;
    private int PenDeliveryID;
    private bool fade;
    private float timer;
    private float fadetimer;
    private static float maxspeed = 200f;
    private static float slowdowndistance = 100f;
    private float speed;
    private bool HasDelivered;
    private Vector2 dirVec;

    public DeliveryAirBehaviour(
      AirVehicle vehicle_,
      List<Vector2> anchorpositions_,
      WaveInfo delivery_,
      int _OrderUID,
      bool usecrisprcrate,
      bool _SnapToSideOfScreen,
      int PenUID,
      bool fade_ = false)
      : base(vehicle_)
    {
      this.OrderUID = _OrderUID;
      this.PenDeliveryID = PenUID;
      this.SnapToSideOfScreen = _SnapToSideOfScreen;
      this.vehicle = vehicle_;
      this.anchorpositions = anchorpositions_;
      this.fade = fade_;
      this.destination = this.anchorpositions[this.anchorpositions.Count - 1];
      this.behaviourtype = AirVehicleBehaviourType.AnimalDelivery;
      this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Start;
      this.crate = new AirDeliveryCrateAndRope(delivery_, _OrderUID, usecrisprcrate);
      this.InboundArrow = new Z_BusStatus();
    }

    public override bool UpdateAirVehicleBehaviour(float SimulationTime, Player player)
    {
      this.InboundArrow.UpdateZ_BusStatusAsChopper(SimulationTime, this.vehicle.vLocation, this.state == DeliveryAirBehaviour.DeliveryBehaviourState.Start || this.state == DeliveryAirBehaviour.DeliveryBehaviourState.Entering);
      this.HasDelivered = false;
      float oscillateRatio = (float) Math.Log((double) (this.speed / DeliveryAirBehaviour.maxspeed) + 1.0, 2.0);
      this.vehicle.Rotation = oscillateRatio * 0.1570796f;
      if ((double) this.dirVec.X < 0.0)
        this.vehicle.Rotation = -this.vehicle.Rotation;
      this.vehicle.drawoffset = 0.3f * new Vector2(0.0f, SinOscillator.OscillateWithSin(ref this.timer, 5f, SimulationTime));
      float num1 = 1f;
      if (TrailerDemoFlags.HasTrailerFlag)
        num1 *= TrailerDemoFlags.ChopperDropSpeed;
      this.crate.alpha = this.alpha;
      bool flag = this.crate.UpdateAirDeliveryCrateAndRope(this.vehicle.altitude, oscillateRatio, SimulationTime * num1, player, ref this.HasDelivered);
      switch (this.state)
      {
        case DeliveryAirBehaviour.DeliveryBehaviourState.Start:
          this.start = this.vehicle.vehicletype != AirVehicleType.Drone ? (!this.SnapToSideOfScreen ? new Vector2(-0.5f * (float) this.vehicle.DrawRect.Width, this.destination.Y) : new Vector2(RenderMath.TranslateScreenSpaceToWorldSpace(Vector2.Zero).X + -0.5f * (float) this.vehicle.DrawRect.Width * Sengine.WorldOriginandScale.Z, this.destination.Y)) : this.anchorpositions[0];
          this.vehicle.vLocation = this.start;
          if (this.fade)
          {
            this.alpha = 0.0f;
            this.fadetimer = 0.0f;
            this.state = DeliveryAirBehaviour.DeliveryBehaviourState.FadeIn;
          }
          else
            this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Entering;
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.FadeIn:
          this.vehicle.alpha = Math.Min(this.fadetimer * 2f, 1f);
          this.fadetimer += SimulationTime;
          if ((double) this.vehicle.alpha >= 1.0)
            this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Entering;
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.Entering:
          this.dirVec = Vector2.Normalize(this.destination - this.start);
          Vector2 vector2_1 = this.destination - this.vehicle.vLocation;
          float num2 = vector2_1.Length();
          float num3 = (float) ((double) this.dirVec.X * (double) vector2_1.X + (double) this.dirVec.Y * (double) vector2_1.Y);
          if ((double) num2 < 0.0500000007450581 || (double) num3 < 0.0)
          {
            this.speed = 0.0f;
            this.vehicle.vLocation = this.destination;
            this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Hovering;
            this.crate.StartUnload();
          }
          else if ((double) num2 > (double) DeliveryAirBehaviour.slowdowndistance)
          {
            this.speed = DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          else
          {
            this.speed = Math.Max(0.025f, (float) Math.Log((double) num2 / (double) DeliveryAirBehaviour.slowdowndistance + 1.0, 2.0)) * DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.Hovering:
          if (flag)
          {
            this.speed = 0.0f;
            if (this.vehicle.vehicletype == AirVehicleType.Drone)
            {
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Returning;
            }
            else
            {
              this.start = this.destination;
              this.destination.X = TileMath.GetTileToWorldSpace(new Vector2Int(TileMath.GetOverWorldMapSize_XDefault(), 0)).X + (float) this.vehicle.DrawRect.Width;
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Exiting;
            }
          }
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.Exiting:
          this.dirVec = Vector2.Normalize(this.destination - this.start);
          float num4 = (this.start - this.vehicle.vLocation).Length();
          Vector2 vector2_2 = this.destination - this.vehicle.vLocation;
          if ((double) this.dirVec.X * (double) vector2_2.X + (double) this.dirVec.Y * (double) vector2_2.Y < 0.0)
          {
            if (this.fade)
            {
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.FadeOut;
              this.fadetimer = 0.0f;
            }
            else
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Exited;
            return false;
          }
          if (this.start == this.vehicle.vLocation || (double) num4 < 0.0500000007450581)
          {
            this.speed = 0.025f * DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          else if ((double) num4 > (double) DeliveryAirBehaviour.slowdowndistance)
          {
            this.speed = DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          else
          {
            this.speed = Math.Max(0.025f, (float) Math.Log((double) num4 / (double) DeliveryAirBehaviour.slowdowndistance + 1.0, 2.0)) * DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.Returning:
          this.dirVec = Vector2.Normalize(this.start - this.destination);
          Vector2 vector2_3 = this.start - this.vehicle.vLocation;
          float num5 = vector2_3.Length();
          float num6 = (float) ((double) this.dirVec.X * (double) vector2_3.X + (double) this.dirVec.Y * (double) vector2_3.Y);
          if ((double) num5 < 0.0500000007450581 || (double) num6 < 0.0)
          {
            this.speed = 0.0f;
            this.vehicle.vLocation = this.start;
            if (this.fade)
            {
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.FadeOut;
              this.fadetimer = 0.0f;
            }
            else
              this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Exited;
          }
          else if ((double) num5 > (double) DeliveryAirBehaviour.slowdowndistance)
          {
            this.speed = DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          else
          {
            this.speed = Math.Max(0.025f, (float) Math.Log((double) num5 / (double) DeliveryAirBehaviour.slowdowndistance + 1.0, 2.0)) * DeliveryAirBehaviour.maxspeed;
            AirVehicle vehicle = this.vehicle;
            vehicle.vLocation = vehicle.vLocation + this.speed * SimulationTime * this.dirVec;
          }
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.FadeOut:
          this.vehicle.alpha = Math.Max((float) (1.0 - (double) this.fadetimer * 2.0), 0.0f);
          this.fadetimer += SimulationTime;
          if ((double) this.vehicle.alpha <= 0.0)
          {
            this.state = DeliveryAirBehaviour.DeliveryBehaviourState.Exited;
            this.fadetimer = 0.0f;
          }
          return false;
        case DeliveryAirBehaviour.DeliveryBehaviourState.Exited:
          return true;
        default:
          return false;
      }
    }

    public override bool ThisIsOnOrder(int _OrderUID) => this.OrderUID == _OrderUID;

    public override bool IsSomethingOnOrderToThisPen(int _PenUID) => this.PenDeliveryID == _PenUID && !this.HasDelivered;

    public override void DrawAirVehicleBehaviour(SpriteBatch spritebatch, Vector2 offset)
    {
      if (!TrailerDemoFlags.HasTrailerFlag && !GameFlags.PhotoMode)
        this.InboundArrow.DrawZ_BusStatusForChopper();
      this.crate.DrawAirDeliveryCrateAndRope(spritebatch, offset);
    }

    private enum DeliveryBehaviourState
    {
      Start,
      FadeIn,
      Entering,
      Hovering,
      Exiting,
      Returning,
      FadeOut,
      Exited,
      Count,
    }
  }
}
