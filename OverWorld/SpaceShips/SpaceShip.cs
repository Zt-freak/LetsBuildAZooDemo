// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.SpaceShips.SpaceShip
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using TinyZoo.PlayerDir;

namespace TinyZoo.OverWorld.SpaceShips
{
  internal class SpaceShip : GameObject
  {
    private Vector2 Directionvector;
    private float TGT;
    private DirectionPressed durection;
    private float Delay;
    public float Speed;

    public SpaceShip()
    {
      this.DrawRect = new Rectangle(0, 630, (int) sbyte.MaxValue, 99);
      this.SetDrawOriginToCentre();
      if (LiveStats.IsChristmas)
      {
        this.DrawRect = new Rectangle(0, 630, (int) sbyte.MaxValue, 99);
        this.SetDrawOriginToCentre();
      }
      else
        this.SetAllColours(0.3f, 0.3f, 0.3f);
      this.scale = 0.5f;
      this.SetZDepth(-0.5f);
    }

    public void SetDelay(float _Delay) => this.Delay = _Delay;

    public void StartFlight(DirectionPressed direction)
    {
      this.Speed = (float) TinyZoo.Game1.Rnd.Next(120, 400);
      this.durection = direction;
      switch (direction)
      {
        case DirectionPressed.Up:
          this.Rotation = 0.0f;
          this.vLocation.X = (float) TinyZoo.Game1.Rnd.Next(0, TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.vLocation.Y = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.vLocation.Y += 768f;
          this.Directionvector = new Vector2(0.0f, -1f);
          this.TGT = -768f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          break;
        case DirectionPressed.Right:
          this.Rotation = 1.570796f;
          this.Directionvector = new Vector2(1f, 0.0f);
          this.vLocation.Y = MathStuff.getRandomFloat(0.0f, (float) (TileMath.GetOverWorldMapSize_XDefault() * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y);
          this.vLocation.Y = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.vLocation.X += 768f;
          this.TGT = -768f;
          break;
        case DirectionPressed.Down:
          this.Rotation = 3.141593f;
          this.Directionvector = new Vector2(0.0f, 1f);
          this.vLocation.X = (float) TinyZoo.Game1.Rnd.Next(0, TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.vLocation.Y = -768f;
          this.TGT = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.TGT += 768f;
          this.TGT *= Sengine.ScreenRatioUpwardsMultiplier.Y;
          break;
        case DirectionPressed.Left:
          this.Rotation = -1.570796f;
          this.Directionvector = new Vector2(-1f, 0.0f);
          this.vLocation.Y = MathStuff.getRandomFloat(0.0f, (float) (TileMath.GetOverWorldMapSize_XDefault() * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y);
          this.vLocation.X = -768f;
          this.TGT = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16);
          this.TGT += 768f;
          break;
      }
    }

    public bool UpdateSpaceShip(float DeltaTime)
    {
      if ((double) this.Delay > 0.0)
      {
        this.Delay -= DeltaTime;
        return false;
      }
      this.vLocation = this.vLocation + this.Directionvector * DeltaTime * this.Speed;
      switch (this.durection)
      {
        case DirectionPressed.Up:
          if ((double) this.vLocation.Y < (double) this.TGT)
            return true;
          break;
        case DirectionPressed.Right:
          if ((double) this.vLocation.X > (double) this.TGT)
            return true;
          break;
        case DirectionPressed.Down:
          if ((double) this.vLocation.Y > (double) this.TGT)
            return true;
          break;
        case DirectionPressed.Left:
          if ((double) this.vLocation.X < (double) this.TGT)
            return true;
          break;
      }
      return false;
    }

    public void DrawSpaceShip()
    {
      if ((double) this.Delay > 0.0)
        return;
      this.SetAllColours(0.4f, 0.4f, 0.4f);
      this.SetAlpha(1f);
      this.WorldOffsetDistanceDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.vLocation);
      this.SetAllColours(0.0f, 0.0f, 0.0f);
      this.SetAlpha(0.5f);
      this.WorldOffsetDistanceDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, this.vLocation + new Vector2(20f, 20f));
    }
  }
}
