// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.beams.BeamCenter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.ArcadeMode;
using TinyZoo.Audio;
using TinyZoo.GamePlay.Effects;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout.CellBlocks;

namespace TinyZoo.GamePlay.beams
{
  internal class BeamCenter : GameObject
  {
    private Beam LeftUpBeam;
    private Beam RightDownBeam;
    private float FlashTimer;
    private Rectangle BaseRect;
    private BeamSpawnEffect spawnWanve;
    public bool IsLockedInPlace;
    public int ThisID;
    private static int UIDSet;
    public bool BeamWasHitByHuman;
    internal static Rectangle BeamRect = new Rectangle(95, 0, 5, 5);
    public List<IntersectionPoint> intersectionpoints;
    public int BeamIndex;
    private float LifeTimeForBreakOutMode;
    private bool BeamBreakOutTimeOut;
    public bool IsHorizontal;

    public BeamCenter(int _BeamIndex) => this.Create(_BeamIndex);

    public BeamCenter(BeamEntry entry, int Index, bool PlaySpawnEffect = true)
    {
      this.Create(Index);
      this.FireBeam(entry.GetLocation(), entry.Horizontal, new BeamInfo(true), PlaySpawnEffect);
    }

    private void Create(int _BeamIndex)
    {
      this.BeamIndex = _BeamIndex;
      this.ThisID = BeamCenter.UIDSet;
      ++BeamCenter.UIDSet;
      this.BaseRect = BeamCenter.BeamRect;
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.LeftUpBeam = new Beam(true);
      this.RightDownBeam = new Beam(false);
    }

    public bool StillActive() => true;

    public void FireBeam(
      Vector2 Location,
      bool IsLeftRight,
      BeamInfo beaminfo,
      bool PlaySpawnEffect = true)
    {
      this.BeamWasHitByHuman = false;
      this.intersectionpoints = new List<IntersectionPoint>();
      this.IsHorizontal = IsLeftRight;
      this.IsLockedInPlace = false;
      this.spawnWanve = new BeamSpawnEffect(Location);
      if (!PlaySpawnEffect)
        this.spawnWanve.bActive = false;
      this.vLocation = Location;
      float beamSpeed = beaminfo.BeamSpeed;
      if (GameFlags.IsArcadeMode)
        beamSpeed = ArcadeData.GetBeamSpeed();
      else if (GameFlags.BountyMode)
        beamSpeed = Bounty.BeamSpeed;
      this.LeftUpBeam.Start(Location, IsLeftRight, beamSpeed, beaminfo.IsInstantBeam);
      this.RightDownBeam.Start(Location, IsLeftRight, beamSpeed, beaminfo.IsInstantBeam);
      this.LifeTimeForBreakOutMode = -1f;
      this.BeamBreakOutTimeOut = false;
      if (!GameFlags.IsBreakOut)
        return;
      this.LifeTimeForBreakOutMode = 2f;
    }

    public bool CheckCollision(out Vector2 CollslionPoint, Vector2 CorePoint, Vector2 EndPoint)
    {
      if (GameFlags.IsBreakOut)
      {
        CollslionPoint = Vector2.Zero;
        return false;
      }
      CollslionPoint = Vector2.Zero;
      if ((double) CorePoint.X != (double) EndPoint.X)
      {
        if ((double) CorePoint.X < (double) EndPoint.X)
        {
          if ((double) this.vLocation.X > (double) CorePoint.X && (double) this.vLocation.X < (double) EndPoint.X)
          {
            if ((double) CorePoint.Y < (double) this.vLocation.Y)
            {
              if ((double) this.vLocation.Y - (double) this.LeftUpBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y < (double) CorePoint.Y)
              {
                CollslionPoint = new Vector2(this.vLocation.X, CorePoint.Y);
                return true;
              }
            }
            else if ((double) this.vLocation.Y + (double) this.RightDownBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y > (double) CorePoint.Y)
            {
              CollslionPoint = new Vector2(this.vLocation.X, CorePoint.Y);
              return true;
            }
          }
        }
        else if ((double) this.vLocation.X <= (double) CorePoint.X && (double) this.vLocation.X > (double) EndPoint.X)
        {
          if ((double) CorePoint.Y < (double) this.vLocation.Y)
          {
            if ((double) this.vLocation.Y - (double) this.LeftUpBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y < (double) CorePoint.Y)
            {
              CollslionPoint = new Vector2(this.vLocation.X, CorePoint.Y);
              return true;
            }
          }
          else if ((double) this.vLocation.Y + (double) this.RightDownBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y > (double) CorePoint.Y)
          {
            CollslionPoint = new Vector2(this.vLocation.X, CorePoint.Y);
            return true;
          }
        }
      }
      else if ((double) CorePoint.Y != (double) EndPoint.Y)
      {
        if ((double) CorePoint.Y < (double) EndPoint.Y)
        {
          if ((double) this.vLocation.Y > (double) CorePoint.Y && (double) this.vLocation.Y < (double) EndPoint.Y)
          {
            if ((double) CorePoint.X < (double) this.vLocation.X)
            {
              if ((double) this.vLocation.X - (double) this.LeftUpBeam.VSCale.X < (double) CorePoint.X)
              {
                CollslionPoint = new Vector2(CorePoint.X, this.vLocation.Y);
                return true;
              }
            }
            else if ((double) this.vLocation.X + (double) this.RightDownBeam.VSCale.X > (double) CorePoint.X)
            {
              CollslionPoint = new Vector2(CorePoint.X, this.vLocation.Y);
              return true;
            }
          }
        }
        else if ((double) this.vLocation.Y <= (double) CorePoint.Y && (double) this.vLocation.Y > (double) EndPoint.Y)
        {
          if ((double) CorePoint.X < (double) this.vLocation.X)
          {
            if ((double) this.vLocation.X - (double) this.LeftUpBeam.VSCale.X < (double) CorePoint.X)
            {
              CollslionPoint = new Vector2(CorePoint.X, this.vLocation.Y);
              return true;
            }
          }
          else if ((double) this.vLocation.X + (double) this.RightDownBeam.VSCale.X > (double) CorePoint.X)
          {
            CollslionPoint = new Vector2(CorePoint.X, this.vLocation.Y);
            return true;
          }
        }
      }
      return false;
    }

    public bool UpdateBeamCenter(float DeltaTme, List<BeamCenter> beams)
    {
      if ((double) this.LifeTimeForBreakOutMode > 0.0)
      {
        this.LifeTimeForBreakOutMode -= DeltaTme;
        if ((double) this.LifeTimeForBreakOutMode < 0.0)
        {
          this.BeamBreakOutTimeOut = true;
          this.BeamWasHitByHuman = true;
        }
      }
      if ((double) this.FlashTimer < 0.5)
      {
        this.FlashTimer += DeltaTme;
        if ((double) this.FlashTimer >= 0.5)
        {
          this.DrawRect = this.BaseRect;
          this.DrawRect.X += 6;
        }
      }
      else if ((double) this.FlashTimer < 0.699999988079071)
      {
        this.FlashTimer += DeltaTme;
        if ((double) this.FlashTimer >= 0.699999988079071)
        {
          this.DrawRect = this.BaseRect;
          this.FlashTimer = 0.0f;
        }
      }
      this.LeftUpBeam.UpdateBeam(DeltaTme, beams, this);
      this.RightDownBeam.UpdateBeam(DeltaTme, beams, this);
      this.spawnWanve.UpdateBeamSpawnEffect(DeltaTme);
      if (this.IsLockedInPlace || !this.RightDownBeam.EndCollision.bActive || !this.LeftUpBeam.EndCollision.bActive)
        return false;
      if (TinyZoo.Game1.gamestate == GAMESTATE.GamePlay)
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BeamConnect);
      this.IsLockedInPlace = true;
      ++GameFlags.BeamsLockedOrDead;
      return true;
    }

    public bool CheckForEnemyHits(
      Vector2 OldLocation,
      Vector2 NewLocation,
      EnemyRenderer thisenemy)
    {
      bool flag1 = false;
      if (!this.IsLockedInPlace && !this.BeamWasHitByHuman)
      {
        if (this.IsHorizontal)
        {
          bool flag2 = false;
          if ((double) OldLocation.Y > (double) NewLocation.Y)
          {
            if ((double) OldLocation.Y > (double) this.vLocation.Y && (double) NewLocation.Y - (double) thisenemy.HalfEnemyHeight <= (double) this.vLocation.Y)
              flag2 = true;
          }
          else if ((double) OldLocation.Y - (double) thisenemy.HalfEnemyHeight < (double) this.vLocation.Y && (double) NewLocation.Y + (double) thisenemy.HalfEnemyHeight >= (double) this.vLocation.Y)
            flag2 = true;
          if (flag2)
          {
            if ((double) this.vLocation.X >= (double) NewLocation.X)
            {
              if ((double) NewLocation.X > (double) this.vLocation.X - (double) this.LeftUpBeam.VSCale.X || (double) OldLocation.X > (double) this.vLocation.X - (double) this.LeftUpBeam.VSCale.X)
              {
                DamageFlash.FlashLocations.Add(new Vector2(NewLocation.X, this.vLocation.Y));
                flag1 = true;
              }
            }
            else if ((double) NewLocation.X < (double) this.vLocation.X + (double) this.RightDownBeam.VSCale.X || (double) OldLocation.X < (double) this.vLocation.X + (double) this.RightDownBeam.VSCale.X)
            {
              DamageFlash.FlashLocations.Add(new Vector2(NewLocation.X, this.vLocation.Y));
              flag1 = true;
            }
          }
        }
        else
        {
          bool flag2 = false;
          if ((double) OldLocation.X < (double) NewLocation.X)
          {
            if ((double) OldLocation.X < (double) this.vLocation.X && (double) NewLocation.X + (double) thisenemy.HalfEnemyWidth >= (double) this.vLocation.X)
              flag2 = true;
          }
          else if ((double) OldLocation.X > (double) this.vLocation.X && (double) NewLocation.X - (double) thisenemy.HalfEnemyWidth <= (double) this.vLocation.X)
            flag2 = true;
          if (flag2)
          {
            if ((double) this.vLocation.Y >= (double) NewLocation.Y)
            {
              if ((double) NewLocation.Y > (double) this.vLocation.Y - (double) this.LeftUpBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y || (double) OldLocation.Y > (double) this.vLocation.Y - (double) this.LeftUpBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
              {
                DamageFlash.FlashLocations.Add(new Vector2(this.vLocation.X, NewLocation.Y));
                flag1 = true;
              }
            }
            else if ((double) NewLocation.Y < (double) this.vLocation.Y + (double) this.RightDownBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y || (double) OldLocation.Y < (double) this.vLocation.Y + (double) this.RightDownBeam.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y)
            {
              DamageFlash.FlashLocations.Add(new Vector2(this.vLocation.X, NewLocation.Y));
              flag1 = true;
            }
          }
        }
      }
      return flag1;
    }

    public void BeamGotHitByHuman()
    {
      this.BeamWasHitByHuman = true;
      ++GameFlags.BeamsLockedOrDead;
    }

    public void DrawBeamCenter()
    {
      this.WorldOffsetDraw(AssetContainer.pointspritebatch01, AssetContainer.SpriteSheet);
      if (this.BeamWasHitByHuman)
        return;
      this.spawnWanve.DrawBeamSpawnEffect();
      this.LeftUpBeam.DrawBeam();
      this.RightDownBeam.DrawBeam();
    }
  }
}
