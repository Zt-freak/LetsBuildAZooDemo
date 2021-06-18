// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirDeliveryCrateAndRope
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirDeliveryCrateAndRope
  {
    private AirDeliveryRope rope;
    private AirDeliveryCrate crate;
    private AirDeliveryCrateAndRope.CrateAndRopeState state;
    public Vector2 location;
    private float rotation;
    private bool startunload;
    private float timer;
    private WaveInfo delivery;
    private int OrderUID;
    private bool usecrisprcrate;
    public float alpha = 1f;
    private bool firstcratedraw = true;
    private Vector2 deliveredpos;

    public AirDeliveryCrateAndRope(WaveInfo delivery_, int _OrderUID, bool usecrisprcrate_ = false)
    {
      this.OrderUID = _OrderUID;
      this.delivery = delivery_;
      this.usecrisprcrate = usecrisprcrate_;
      this.rope = new AirDeliveryRope(usecrisprcrate_);
    }

    public void StartUnload() => this.startunload = true;

    public bool UpdateAirDeliveryCrateAndRope(
      float altitude,
      float oscillateRatio,
      float DeltaTime,
      Player player,
      ref bool HasDelivered)
    {
      this.rope.alpha = this.alpha;
      float num = SinOscillator.OscillateWithSin(ref this.timer, 0.7f, DeltaTime);
      if (this.crate != null)
        this.crate.UpdateAirDeliveryCrate(DeltaTime, player, ref HasDelivered);
      switch (this.state)
      {
        case AirDeliveryCrateAndRope.CrateAndRopeState.Transporting:
          this.rotation = (float) ((0.0500000007450581 + (double) num * 0.0149999996647239) * (double) oscillateRatio * 3.14159274101257);
          if (this.startunload)
          {
            this.state = AirDeliveryCrateAndRope.CrateAndRopeState.Lowering;
            this.rope.StartExtend();
            this.startunload = false;
          }
          return false;
        case AirDeliveryCrateAndRope.CrateAndRopeState.Lowering:
          if (this.rope.UpdateAirDeliveryRope(altitude, DeltaTime))
          {
            this.state = AirDeliveryCrateAndRope.CrateAndRopeState.Retracting;
            this.rope.StartRetract();
            this.crate = new AirDeliveryCrate(this.delivery, this.OrderUID, this.usecrisprcrate);
            this.crate.DelayedOpen(4f);
            this.crate.alpha = this.alpha;
          }
          return false;
        case AirDeliveryCrateAndRope.CrateAndRopeState.Retracting:
          this.crate.alpha = this.alpha;
          if (!this.rope.UpdateAirDeliveryRope(altitude, DeltaTime))
            return false;
          this.state = AirDeliveryCrateAndRope.CrateAndRopeState.Delivered;
          return true;
        case AirDeliveryCrateAndRope.CrateAndRopeState.Delivered:
          this.rope.UpdateAirDeliveryRope(altitude, DeltaTime);
          return false;
        default:
          return false;
      }
    }

    public void DrawAirDeliveryCrateAndRope(SpriteBatch spritebatch, Vector2 offset, float alpha = 1f)
    {
      offset += this.location;
      if (this.crate != null && this.firstcratedraw)
      {
        this.crate.location = offset + this.location;
        this.crate.location.Y += (this.rope.Length + 46f) * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.firstcratedraw = false;
      }
      if (this.crate != null)
        this.crate.DrawAirDeliveryCrate(spritebatch, Vector2.Zero);
      this.rope.DrawAirDeliveryRope(spritebatch, offset, this.rotation);
    }

    private enum CrateAndRopeState
    {
      Transporting,
      Lowering,
      Retracting,
      Delivered,
      Count,
    }
  }
}
