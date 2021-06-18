// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.HelicopterRideBehaviour
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class HelicopterRideBehaviour : AirVehicleBehaviour
  {
    private Vector2 startpoint;
    private HelicopterRideBehaviour.HelicopterRideState state;
    private HelicopterRideBehaviour.Direction direction = HelicopterRideBehaviour.Direction.None;
    private float duration;
    private float timer;
    private Vector2 mainlocation;
    private Vector2 destination;
    private float currDir;
    private Vector2 currDirVec;
    private float speed;
    private Vector2 extraoffset;
    private float oscillatorTime;
    private static float flightaltitude = 100f;
    private static float liftrate = 20f;
    private static float acceleration = 15f;
    private static float rotatespeed = 0.8f;
    private static float minspeed = 2f;
    private static float locationradius = 70f;
    private static float maxspeed = 50f;
    private static float distanceToGoStraight = 50f;
    private bool liftoff;
    private float distanceStartLanding;
    private float dirJustBeforeLanding;
    private float testtimer;

    public HelicopterRideBehaviour(AirVehicle vehicle_, Vector2 startpoint_, float duration_)
      : base(vehicle_)
    {
      this.startpoint = startpoint_;
      this.state = HelicopterRideBehaviour.HelicopterRideState.Landed;
      this.duration = duration_;
      this.vehicle.vLocation = this.startpoint;
      this.mainlocation = this.vehicle.vLocation;
      this.timer = 0.0f;
      this.oscillatorTime = 0.0f;
      this.direction = HelicopterRideBehaviour.Direction.East;
      this.currDir = 0.0f;
    }

    public void StartRide() => this.liftoff = true;

    public override bool UpdateAirVehicleBehaviour(float SimulationTime, Player player)
    {
      bool flag = false;
      this.extraoffset.Y = 0.4f * (this.vehicle.altitude / HelicopterRideBehaviour.flightaltitude) * SinOscillator.OscillateWithSin(ref this.oscillatorTime, 5f, SimulationTime);
      switch (this.state)
      {
        case HelicopterRideBehaviour.HelicopterRideState.Landed:
          this.StartRide();
          this.testtimer += SimulationTime;
          if (this.liftoff && (double) this.testtimer > 3.0)
          {
            this.testtimer = 0.0f;
            this.liftoff = false;
            this.state = HelicopterRideBehaviour.HelicopterRideState.SpinUp;
            this.vehicle.SetUpSimpleAnimation(5, 0.125f);
            this.vehicle.PlayOnlyOnce = true;
            break;
          }
          break;
        case HelicopterRideBehaviour.HelicopterRideState.SpinUp:
          if (this.vehicle.AnimationFinished)
          {
            this.state = HelicopterRideBehaviour.HelicopterRideState.Lifting;
            this.timer = 0.0f;
            this.SetDirectionFromAngle(this.currDir, true);
            this.vehicle.PlayOnlyOnce = false;
            this.vehicle.SetUpSimpleAnimation(2, 0.1f);
            break;
          }
          break;
        case HelicopterRideBehaviour.HelicopterRideState.Lifting:
          if ((double) this.vehicle.altitude < 0.5 * (double) HelicopterRideBehaviour.flightaltitude)
          {
            this.vehicle.altitude += HelicopterRideBehaviour.liftrate * SimulationTime;
          }
          else
          {
            this.currDir = (float) (TinyZoo.Game1.Rnd.NextDouble() * 2.0 * Math.PI);
            this.currDirVec = new Vector2((float) Math.Cos((double) this.currDir), (float) Math.Sin((double) this.currDir));
            this.destination = this.startpoint;
            this.state = HelicopterRideBehaviour.HelicopterRideState.Roaming;
            this.speed = 0.0f;
          }
          this.vehicle.vLocation = this.startpoint + this.extraoffset;
          this.timer += SimulationTime;
          break;
        case HelicopterRideBehaviour.HelicopterRideState.Roaming:
          if ((double) this.speed < (double) HelicopterRideBehaviour.maxspeed)
            this.speed += HelicopterRideBehaviour.acceleration * SimulationTime;
          else if ((double) this.speed > (double) HelicopterRideBehaviour.maxspeed)
            this.speed = HelicopterRideBehaviour.maxspeed;
          if ((double) this.vehicle.altitude < (double) HelicopterRideBehaviour.flightaltitude)
            this.vehicle.altitude += HelicopterRideBehaviour.liftrate * SimulationTime;
          else if ((double) this.vehicle.altitude > (double) HelicopterRideBehaviour.flightaltitude)
            this.vehicle.altitude = HelicopterRideBehaviour.flightaltitude;
          Vector2 vector2_1 = this.destination - this.mainlocation;
          if ((double) vector2_1.LengthSquared() < (double) HelicopterRideBehaviour.locationradius * (double) HelicopterRideBehaviour.locationradius)
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
            this.state = HelicopterRideBehaviour.HelicopterRideState.Returning;
            this.timer = 0.0f;
            this.destination = this.startpoint;
            break;
          }
          break;
        case HelicopterRideBehaviour.HelicopterRideState.Returning:
          Vector2 vector2_2 = this.destination - this.mainlocation;
          float num1 = vector2_2.Length();
          Vector2 dirVecToDest2 = Vector2.Normalize(vector2_2);
          this.currDirVec = Vector2.Normalize(this.currDirVec);
          this.currDir += this.CalculateTurnAngle(this.currDirVec, dirVecToDest2, SimulationTime);
          this.currDirVec = new Vector2((float) Math.Cos((double) this.currDir), (float) Math.Sin((double) this.currDir));
          this.mainlocation += this.speed * this.currDirVec * SimulationTime;
          this.vehicle.vLocation = this.mainlocation + this.extraoffset;
          if ((double) num1 < (double) HelicopterRideBehaviour.distanceToGoStraight)
          {
            this.state = HelicopterRideBehaviour.HelicopterRideState.Landing;
            this.distanceStartLanding = num1;
            this.dirJustBeforeLanding = this.currDir;
            this.dirJustBeforeLanding %= 6.283185f;
            break;
          }
          break;
        case HelicopterRideBehaviour.HelicopterRideState.Landing:
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
          float val1 = (this.destination - this.mainlocation).Length() / this.distanceStartLanding;
          this.speed = HelicopterRideBehaviour.minspeed + Math.Min(val1, 1f) * (HelicopterRideBehaviour.maxspeed - HelicopterRideBehaviour.minspeed);
          this.vehicle.altitude -= HelicopterRideBehaviour.liftrate * SimulationTime;
          this.vehicle.vLocation = this.mainlocation + this.extraoffset;
          this.currDir = this.vehicle.altitude / HelicopterRideBehaviour.flightaltitude * this.dirJustBeforeLanding;
          if ((double) this.vehicle.altitude < 0.00999999977648258)
          {
            this.vehicle.altitude = 0.0f;
            this.state = HelicopterRideBehaviour.HelicopterRideState.SpinDown;
            this.extraoffset = Vector2.Zero;
            this.oscillatorTime = 0.0f;
            this.currDir = 0.0f;
            this.vehicle.DrawRect = AirVehicle.helicopterE;
            this.vehicle.PlayOnlyOnce = true;
            this.vehicle.SetUpSimpleAnimation(5, 0.125f);
            this.vehicle.SetBackwards();
            break;
          }
          break;
        case HelicopterRideBehaviour.HelicopterRideState.SpinDown:
          if (this.vehicle.AnimationFinished)
          {
            this.state = HelicopterRideBehaviour.HelicopterRideState.Landed;
            this.vehicle.DrawRect = AirVehicle.helicopterE;
            this.vehicle.PlayOnlyOnce = false;
            this.vehicle.SetUpSimpleAnimation(1, 60f);
            break;
          }
          break;
        default:
          throw new NotImplementedException();
      }
      this.SetDirectionFromAngle(this.currDir);
      return flag;
    }

    private float CalculateTurnAngle(Vector2 currDirVec, Vector2 dirVecToDest, float DeltaTime)
    {
      float num = Vector2.Dot(currDirVec, dirVecToDest);
      float val2 = (float) Math.Acos((double) num > 1.0 ? 1.0 : (double) num);
      return ((double) Vector3.Cross(new Vector3(currDirVec, 0.0f), new Vector3(dirVecToDest, 0.0f)).Z > 0.0 ? 1f : -1f) * Math.Min(HelicopterRideBehaviour.rotatespeed * DeltaTime, val2);
    }

    private void SetDrawRectsFromAngle(double angle)
    {
    }

    private void SetDirectionFromAngle(float angle, bool forceset = false)
    {
      HelicopterRideBehaviour.Direction direction = this.direction;
      angle %= 6.283185f;
      double num1 = Math.Cos(3.0 * Math.PI / 8.0);
      double num2 = Math.Cos((double) angle);
      double num3 = Math.Sin((double) angle);
      if (num2 < num1 && num2 > -num1)
      {
        if (num3 < 0.0)
        {
          if ((uint) this.direction > 0U | forceset)
          {
            this.direction = HelicopterRideBehaviour.Direction.North;
            this.vehicle.DrawRect = AirVehicle.helicopterFlyN;
            this.vehicle.DrawOrigin = AirVehicle.helicopterNOrigin;
            this.vehicle.FlipRender = false;
          }
        }
        else if (this.direction != HelicopterRideBehaviour.Direction.South | forceset)
        {
          this.direction = HelicopterRideBehaviour.Direction.South;
          this.vehicle.DrawRect = AirVehicle.helicopterFlyS;
          this.vehicle.DrawOrigin = AirVehicle.helicopterSOrigin;
          this.vehicle.FlipRender = false;
        }
      }
      else if (num3 < num1 && num3 > -num1)
      {
        if (num2 > 0.0)
        {
          if (this.direction != HelicopterRideBehaviour.Direction.East | forceset)
          {
            this.direction = HelicopterRideBehaviour.Direction.East;
            this.vehicle.DrawRect = AirVehicle.helicopterFlyE;
            this.vehicle.DrawOrigin = AirVehicle.helicopterEOrigin;
            this.vehicle.FlipRender = false;
          }
        }
        else if (this.direction != HelicopterRideBehaviour.Direction.West | forceset)
        {
          this.direction = HelicopterRideBehaviour.Direction.West;
          this.vehicle.DrawRect = AirVehicle.helicopterFlyW;
          this.vehicle.DrawOrigin = AirVehicle.helicopterWOrigin;
          this.vehicle.FlipRender = false;
        }
      }
      else if (num2 > 0.0)
      {
        if (num3 < 0.0)
        {
          if (this.direction != HelicopterRideBehaviour.Direction.NE | forceset)
          {
            this.direction = HelicopterRideBehaviour.Direction.NE;
            this.vehicle.DrawRect = AirVehicle.helicopterFlyNE;
            this.vehicle.DrawOrigin = AirVehicle.helicopterNEOrigin;
            this.vehicle.FlipRender = false;
          }
        }
        else if (this.direction != HelicopterRideBehaviour.Direction.SE | forceset)
        {
          this.direction = HelicopterRideBehaviour.Direction.SE;
          this.vehicle.DrawRect = AirVehicle.helicopterFlySE;
          this.vehicle.DrawOrigin = AirVehicle.helicopterSEOrigin;
          this.vehicle.FlipRender = false;
        }
      }
      else if (num3 < 0.0)
      {
        if (this.direction != HelicopterRideBehaviour.Direction.NW | forceset)
        {
          this.direction = HelicopterRideBehaviour.Direction.NW;
          this.vehicle.DrawRect = AirVehicle.helicopterFlyNW;
          this.vehicle.DrawOrigin = AirVehicle.helicopterNWOrigin;
          this.vehicle.FlipRender = false;
        }
      }
      else if (this.direction != HelicopterRideBehaviour.Direction.SW | forceset)
      {
        this.direction = HelicopterRideBehaviour.Direction.SW;
        this.vehicle.DrawRect = AirVehicle.helicopterFlySW;
        this.vehicle.DrawOrigin = AirVehicle.helicopterSWOrigin;
        this.vehicle.FlipRender = false;
      }
      if (this.direction == direction)
        return;
      this.vehicle.SetUpSimpleAnimation(2, 0.1f);
    }

    private enum Direction
    {
      None = -1, // 0xFFFFFFFF
      North = 0,
      South = 1,
      East = 2,
      West = 3,
      NE = 4,
      NW = 5,
      SE = 6,
      SW = 7,
      Count = 8,
    }

    private enum HelicopterRideState
    {
      Landed,
      SpinUp,
      Lifting,
      Roaming,
      Returning,
      Landing,
      SpinDown,
      Count,
    }
  }
}
