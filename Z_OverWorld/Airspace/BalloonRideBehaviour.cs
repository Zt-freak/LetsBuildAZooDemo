// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.BalloonRideBehaviour
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class BalloonRideBehaviour : AirVehicleBehaviour
  {
    private Vector2 startpoint;
    private BalloonRideBehaviour.BalloonRideState state;
    private float duration;
    private float timer;
    private Vector2 mainlocation;
    private Vector2 destination;
    private float currDir;
    private Vector2 currDirVec;
    private float speed;
    private Vector2 extraoffset;
    private float oscillatorTime;
    private static float flightaltitude = 120f;
    private static float liftrate = 15f;
    private static float acceleration = 5f;
    private static float rotatespeed = 0.6f;
    private static float minspeed = 2f;
    private static float locationradius = 50f;
    private static float maxspeed = 25f;
    private static float distanceToGoStraight = 40f;
    private bool liftoff;
    private float distanceStartLanding;
    private float testtimer;

    public BalloonRideBehaviour(AirVehicle vehicle_, Vector2 startpoint_, float duration_)
      : base(vehicle_)
    {
      this.startpoint = startpoint_;
      this.state = BalloonRideBehaviour.BalloonRideState.Landed;
      this.duration = duration_;
      this.vehicle.vLocation = this.startpoint;
      this.mainlocation = this.vehicle.vLocation;
      this.timer = 0.0f;
      this.oscillatorTime = 0.0f;
    }

    public void StartRide() => this.liftoff = true;

    public override bool UpdateAirVehicleBehaviour(float SimulationTime, Player player)
    {
      bool flag = false;
      this.extraoffset.X = 3f * (this.vehicle.altitude / BalloonRideBehaviour.flightaltitude) * SinOscillator.OscillateWithSin(ref this.oscillatorTime, 0.3f, SimulationTime);
      switch (this.state)
      {
        case BalloonRideBehaviour.BalloonRideState.Landed:
          this.StartRide();
          this.testtimer += SimulationTime;
          if (this.liftoff && (double) this.testtimer > 5.0)
          {
            this.testtimer = 0.0f;
            this.liftoff = false;
            this.state = BalloonRideBehaviour.BalloonRideState.Lifting;
            this.timer = 0.0f;
            break;
          }
          break;
        case BalloonRideBehaviour.BalloonRideState.Lifting:
          if ((double) this.vehicle.altitude < 0.5 * (double) BalloonRideBehaviour.flightaltitude)
          {
            this.vehicle.altitude += BalloonRideBehaviour.liftrate * SimulationTime;
          }
          else
          {
            this.currDir = (float) (TinyZoo.Game1.Rnd.NextDouble() * 2.0 * Math.PI);
            this.currDirVec = new Vector2((float) Math.Cos((double) this.currDir), (float) Math.Sin((double) this.currDir));
            this.destination = this.startpoint;
            this.state = BalloonRideBehaviour.BalloonRideState.Roaming;
            this.speed = 0.0f;
          }
          this.vehicle.vLocation = this.startpoint + this.extraoffset;
          this.timer += SimulationTime;
          break;
        case BalloonRideBehaviour.BalloonRideState.Roaming:
          if ((double) this.speed < (double) BalloonRideBehaviour.maxspeed)
            this.speed += BalloonRideBehaviour.acceleration * SimulationTime;
          else if ((double) this.speed > (double) BalloonRideBehaviour.maxspeed)
            this.speed = BalloonRideBehaviour.maxspeed;
          if ((double) this.vehicle.altitude < (double) BalloonRideBehaviour.flightaltitude)
            this.vehicle.altitude += BalloonRideBehaviour.liftrate * SimulationTime;
          else if ((double) this.vehicle.altitude > (double) BalloonRideBehaviour.flightaltitude)
            this.vehicle.altitude = BalloonRideBehaviour.flightaltitude;
          Vector2 vector2_1 = this.destination - this.mainlocation;
          if ((double) vector2_1.LengthSquared() < (double) BalloonRideBehaviour.locationradius * (double) BalloonRideBehaviour.locationradius)
          {
            this.destination = this.startpoint + new Vector2((float) (1.8 * TinyZoo.Game1.Rnd.NextDouble() - 0.9) * 300f, (float) (1.8 * TinyZoo.Game1.Rnd.NextDouble() - 0.9) * 300f);
            vector2_1 = this.destination - this.mainlocation;
          }
          Vector2 dirVecToDest1 = Vector2.Normalize(vector2_1);
          this.currDirVec = Vector2.Normalize(this.currDirVec);
          this.currDir += this.CalculateTurnAngle(this.currDirVec, dirVecToDest1, SimulationTime);
          this.currDirVec = new Vector2((float) Math.Cos((double) this.currDir), (float) Math.Sin((double) this.currDir));
          this.mainlocation += this.speed * this.currDirVec * SimulationTime;
          this.vehicle.vLocation = this.mainlocation + this.extraoffset;
          this.timer += SimulationTime;
          if ((double) this.timer >= (double) this.duration - (double) (this.startpoint - this.mainlocation).Length() / (double) this.speed)
          {
            this.state = BalloonRideBehaviour.BalloonRideState.Returning;
            this.timer = 0.0f;
            this.destination = this.startpoint;
            break;
          }
          break;
        case BalloonRideBehaviour.BalloonRideState.Returning:
          Vector2 vector2_2 = this.destination - this.mainlocation;
          float num1 = vector2_2.Length();
          Vector2 dirVecToDest2 = Vector2.Normalize(vector2_2);
          this.currDirVec = Vector2.Normalize(this.currDirVec);
          this.currDir += this.CalculateTurnAngle(this.currDirVec, dirVecToDest2, SimulationTime);
          this.currDirVec = new Vector2((float) Math.Cos((double) this.currDir), (float) Math.Sin((double) this.currDir));
          this.mainlocation += this.speed * this.currDirVec * SimulationTime;
          this.vehicle.vLocation = this.mainlocation + this.extraoffset;
          if ((double) num1 < (double) BalloonRideBehaviour.distanceToGoStraight)
          {
            this.state = BalloonRideBehaviour.BalloonRideState.Landing;
            this.distanceStartLanding = num1;
            break;
          }
          break;
        case BalloonRideBehaviour.BalloonRideState.Landing:
          Vector2 vector2_3 = this.destination - this.mainlocation;
          float num2 = vector2_3.Length();
          Vector2 vector2_4 = Vector2.Normalize(vector2_3);
          this.currDirVec = Vector2.Normalize(this.currDirVec);
          if (this.mainlocation != this.destination)
          {
            if ((double) num2 < (double) this.speed * (double) SimulationTime)
              this.mainlocation += num2 * vector2_4;
            else
              this.mainlocation += this.speed * SimulationTime * vector2_4;
          }
          float num3 = Math.Min((this.destination - this.mainlocation).Length() / this.distanceStartLanding, 1f);
          this.speed = BalloonRideBehaviour.minspeed + num3 * (BalloonRideBehaviour.maxspeed - BalloonRideBehaviour.minspeed);
          this.vehicle.altitude -= BalloonRideBehaviour.liftrate * SimulationTime;
          this.vehicle.vLocation = this.mainlocation + this.extraoffset;
          if ((double) this.vehicle.altitude < 0.00999999977648258)
          {
            this.vehicle.altitude = 0.0f;
            this.state = BalloonRideBehaviour.BalloonRideState.Landed;
            this.extraoffset = Vector2.Zero;
            this.oscillatorTime = 0.0f;
            break;
          }
          break;
        default:
          throw new NotImplementedException();
      }
      return flag;
    }

    private float CalculateTurnAngle(
      Vector2 currDirVec,
      Vector2 dirVecToDest,
      float SimulationTime)
    {
      float num = Vector2.Dot(currDirVec, dirVecToDest);
      float val2 = (float) Math.Acos((double) num > 1.0 ? 1.0 : (double) num);
      return ((double) Vector3.Cross(new Vector3(currDirVec, 0.0f), new Vector3(dirVecToDest, 0.0f)).Z > 0.0 ? 1f : -1f) * Math.Min(BalloonRideBehaviour.rotatespeed * SimulationTime, val2);
    }

    private enum BalloonRideState
    {
      Landed,
      Lifting,
      Roaming,
      Returning,
      Landing,
      Count,
    }
  }
}
