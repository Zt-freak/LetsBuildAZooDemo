// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.Airspace.AirDeliveryRope
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_OverWorld.Airspace
{
  internal class AirDeliveryRope
  {
    private Rectangle ropeCrateRect = new Rectangle(330, 1044, 26, 54);
    private Rectangle ropeCrateRect_crispr = new Rectangle(770, 1632, 28, 54);
    private Rectangle ropeRect = new Rectangle(357, 1047, 34, 40);
    private AnimatedGameObject ropeSprite;
    private AirDeliveryExtendingRope extendingRope;
    private Vector2 location;
    private RopeState state;
    private Vector2 ropeSpriteScale = Vector2.One;
    private bool spritescaledone;
    private static float draworiginoffset = 46f;
    public float alpha = 1f;
    private bool startextend;
    private bool startretract;

    public AirDeliveryRope(bool usecrisprcrate = false)
    {
      this.ropeSprite = new AnimatedGameObject();
      if (usecrisprcrate)
        this.ropeSprite.DrawRect = this.ropeCrateRect_crispr;
      else
        this.ropeSprite.DrawRect = this.ropeCrateRect;
      this.ropeSprite.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.extendingRope = new AirDeliveryExtendingRope();
      this.state = RopeState.Retracted;
    }

    public void StartExtend() => this.startextend = true;

    public void StartRetract() => this.startretract = true;

    public float Length => this.extendingRope.length;

    public bool UpdateAirDeliveryRope(float altitude, float DeltaTime)
    {
      float extendLength = altitude - AirDeliveryRope.draworiginoffset;
      this.ropeSprite.SetAlpha(this.alpha);
      this.extendingRope.SetAlpha(this.alpha);
      bool flag1 = false;
      bool flag2 = this.extendingRope.UpdateAirDeliveryExtendingRope(this.state, extendLength, DeltaTime);
      switch (this.state)
      {
        case RopeState.Retracted:
          if (this.startextend)
          {
            this.state = RopeState.Extending;
            this.startextend = false;
            break;
          }
          break;
        case RopeState.Extending:
          if (flag2)
          {
            this.ropeSprite.DrawRect = this.ropeRect;
            this.ropeSprite.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
            this.ropeSprite.SetUpSimpleAnimation(5, 0.1f);
            this.ropeSprite.PlayOnlyOnce = true;
            this.state = RopeState.Extended;
            flag1 = true;
            break;
          }
          break;
        case RopeState.Extended:
          this.ropeSprite.UpdateAnimation(DeltaTime);
          if (this.startretract & this.ropeSprite.AnimationFinished)
          {
            this.state = RopeState.Retracting;
            this.startretract = false;
            break;
          }
          break;
        case RopeState.Retracting:
          if ((double) this.ropeSpriteScale.Y > 0.100000001490116)
          {
            this.ropeSpriteScale.Y -= 0.5f * DeltaTime;
          }
          else
          {
            this.ropeSpriteScale.Y = 0.1f;
            this.spritescaledone = true;
          }
          if (flag2 && this.spritescaledone)
          {
            this.state = RopeState.Retracted;
            flag1 = true;
            break;
          }
          break;
      }
      this.ropeSprite.vLocation = new Vector2(0.0f, this.extendingRope.length * Sengine.ScreenRatioUpwardsMultiplier.Y);
      return flag1;
    }

    public void DrawAirDeliveryRope(SpriteBatch spritebatch, Vector2 offset, float rotation)
    {
      offset += this.location;
      this.ropeSprite.WorldOffsetDraw(spritebatch, AssetContainer.AnimalSheet, offset + this.ropeSprite.vLocation, this.ropeSpriteScale, rotation);
      this.extendingRope.DrawAirDeliveryExtendingRope(spritebatch, offset, rotation);
    }
  }
}
