// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirDeliveryExtendingRope
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirDeliveryExtendingRope : GameObject
  {
    private Vector2 size;
    private float maxlength;
    public float length;
    private static float extendspeed = 12f;

    public AirDeliveryExtendingRope()
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.length = 1f;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.size = new Vector2(2f, this.length);
    }

    public bool UpdateAirDeliveryExtendingRope(
      RopeState state,
      float extendLength,
      float DeltaTime)
    {
      this.maxlength = extendLength;
      switch (state)
      {
        case RopeState.Retracted:
          return false;
        case RopeState.Extending:
          if ((double) this.length < (double) this.maxlength)
          {
            this.length += AirDeliveryExtendingRope.extendspeed * DeltaTime;
            return false;
          }
          this.length = this.maxlength;
          return true;
        case RopeState.Extended:
          return false;
        case RopeState.Retracting:
          if ((double) this.length > 1.0)
          {
            this.length -= 2f * AirDeliveryExtendingRope.extendspeed * DeltaTime;
            return false;
          }
          this.length = 1f;
          return true;
        default:
          return false;
      }
    }

    public void DrawAirDeliveryExtendingRope(
      SpriteBatch spritebatch,
      Vector2 offset,
      float rotation)
    {
      this.size.Y = this.length;
      this.WorldOffsetDraw(spritebatch, AssetContainer.SpriteSheet, offset + this.vLocation, this.size, rotation);
    }
  }
}
