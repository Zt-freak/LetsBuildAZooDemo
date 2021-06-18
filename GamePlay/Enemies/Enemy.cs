// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Enemies.Enemy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.SwitchRandom;

namespace TinyZoo.GamePlay.Enemies
{
  internal class Enemy
  {
    public EnemyRenderer enemyrenderere;
    private DirectionMoving movingLikeThis;
    private Vector2 DirectionVector;
    public BoxZone reffedboxzone;
    private float MovementSpeed;
    public IntakePerson refperson;
    private SinOscillator oscilator;
    private float OsiclationSpeed;
    private float OcilationTimer;
    private float OsilationMin;
    private bool[] MoveCycles;
    private static GameObject GIB;

    public Enemy(RandomResultContainer rand, IntakePerson person)
    {
      this.refperson = person;
      this.enemyrenderere = new EnemyRenderer(person.animaltype, person.CLIndex, person.HeadType, person.HeadVariant);
      this.MovementSpeed = EnemyData.GetMovementsSpeed(person.animaltype);
      double playSpaceLeft = (double) TileMath.GetPlaySpaceLeft();
      double playSpaceRight = (double) TileMath.GetPlaySpaceRight();
      double playSpaceTop = (double) TileMath.GetPlaySpaceTop();
      double playSpaceBottom = (double) TileMath.GetPlaySpaceBottom();
      this.enemyrenderere.vLocation = new Vector2(0.0f, 0.0f);
      this.movingLikeThis = (DirectionMoving) rand.Next(0, 4);
      this.SetVector();
      if (TinyZoo.Game1.gamestate == GAMESTATE.OverWorldSetUp && person.WrongCell)
        this.enemyrenderere.SetWrongCell();
      bool CancelOscilateIfNotArcadeMode = false;
      this.OsiclationSpeed = EnemyData.GetOsclationSpeed(person.animaltype, ref this.OsilationMin, out CancelOscilateIfNotArcadeMode);
      bool CancelMoveCyclerIfNotArcadeMode = false;
      this.MoveCycles = EnemyData.GetMoveCycle(person.animaltype, out CancelMoveCyclerIfNotArcadeMode);
      if (!GameFlags.IsArcadeMode & CancelOscilateIfNotArcadeMode)
        this.OsiclationSpeed = -1f;
      if (!(!GameFlags.IsArcadeMode & CancelMoveCyclerIfNotArcadeMode))
        return;
      this.MoveCycles = (bool[]) null;
    }

    private void SetVector()
    {
      switch (this.movingLikeThis)
      {
        case DirectionMoving.UpRight:
          this.DirectionVector = new Vector2(1f, -1f);
          break;
        case DirectionMoving.DownRight:
          this.DirectionVector = new Vector2(1f, 1f);
          break;
        case DirectionMoving.UpLeft:
          this.DirectionVector = new Vector2(-1f, -1f);
          break;
        case DirectionMoving.DownLeft:
          this.DirectionVector = new Vector2(-1f, 1f);
          break;
      }
    }

    public void LaunchProgressParticles()
    {
    }

    public void ScrubBoxZones(BoxZoneManager boxzonemanager)
    {
      if (!this.reffedboxzone.IsDestroyed)
        return;
      this.RefreshZones(boxzonemanager);
    }

    public void Shuffle()
    {
      if (this.enemyrenderere.IsDead)
        return;
      if ((double) this.reffedboxzone.TopLeft.X + 15.0 < (double) this.reffedboxzone.BottomRight.X)
        this.enemyrenderere.vLocation.X = MathStuff.getRandomFloat(this.reffedboxzone.TopLeft.X + 6f, this.reffedboxzone.BottomRight.X - 6f);
      if ((double) this.reffedboxzone.TopLeft.Y + 15.0 < (double) this.reffedboxzone.BottomRight.Y)
        this.enemyrenderere.vLocation.Y = MathStuff.getRandomFloat(this.reffedboxzone.TopLeft.Y + 6f, this.reffedboxzone.BottomRight.Y - 6f);
      this.movingLikeThis = (DirectionMoving) TinyZoo.Game1.Rnd.Next(0, 4);
      this.SetVector();
    }

    public void RefreshZones(BoxZoneManager boxzone)
    {
      this.reffedboxzone = boxzone.GetMyZone(ref this.enemyrenderere.vLocation);
      if (this.enemyrenderere.IsDead)
        return;
      this.reffedboxzone.SetHasPerson();
    }

    public bool UpdateEnemyBouncing(float DeltaTime)
    {
      if (this.enemyrenderere.IsDead)
        return false;
      if ((double) this.OsiclationSpeed > -1.0)
      {
        float num = (SinOscillator.OscillateWithSin(ref this.OcilationTimer, this.OsiclationSpeed, DeltaTime) + 1f) * 0.5f * (1f - this.OsilationMin) + this.OsilationMin;
        DeltaTime *= num;
      }
      if (this.MoveCycles != null && !this.MoveCycles[LiveStats.EnemyMovementCycle])
        DeltaTime *= 0.05f;
      Vector2 vLocation = this.enemyrenderere.vLocation;
      if (this.enemyrenderere.CanMove())
      {
        EnemyRenderer enemyrenderere = this.enemyrenderere;
        enemyrenderere.vLocation = enemyrenderere.vLocation + this.DirectionVector * Sengine.ScreenRatioUpwardsMultiplier * DeltaTime * this.MovementSpeed;
        this.enemyrenderere.DirectionFacing.X = (double) this.DirectionVector.X <= 0.0 ? -1f : 1f;
      }
      bool flag = false;
      if ((double) this.enemyrenderere.vLocation.Y < (double) vLocation.Y)
      {
        if ((double) this.enemyrenderere.vLocation.Y - (double) this.enemyrenderere.HalfEnemyHeight < (double) this.reffedboxzone.TopLeft.Y && (double) this.enemyrenderere.vLocation.X + (double) this.enemyrenderere.HalfEnemyWidth >= (double) this.reffedboxzone.BottomRight.X)
        {
          flag = true;
          this.movingLikeThis = this.movingLikeThis != DirectionMoving.UpRight ? DirectionMoving.DownLeft : DirectionMoving.DownLeft;
        }
        else if ((double) this.enemyrenderere.vLocation.Y - (double) this.enemyrenderere.HalfEnemyHeight < (double) this.reffedboxzone.TopLeft.Y && (double) this.enemyrenderere.vLocation.X - (double) this.enemyrenderere.HalfEnemyWidth < (double) this.reffedboxzone.TopLeft.X)
        {
          flag = true;
          if (this.movingLikeThis == DirectionMoving.UpLeft)
          {
            this.movingLikeThis = DirectionMoving.DownRight;
          }
          else
          {
            this.movingLikeThis = DirectionMoving.DownRight;
            Console.WriteLine("BOUNCE WENT WRONG");
          }
        }
        else if ((double) this.enemyrenderere.vLocation.Y - (double) this.enemyrenderere.HalfEnemyHeight < (double) this.reffedboxzone.TopLeft.Y)
        {
          flag = true;
          if (this.movingLikeThis == DirectionMoving.UpLeft)
            this.movingLikeThis = DirectionMoving.DownLeft;
          else
            this.movingLikeThis = this.movingLikeThis == DirectionMoving.UpRight ? DirectionMoving.DownRight : throw new Exception("HUH");
        }
      }
      else if ((double) this.enemyrenderere.vLocation.Y > (double) vLocation.Y)
      {
        if ((double) this.enemyrenderere.vLocation.Y + (double) this.enemyrenderere.HalfEnemyHeight >= (double) this.reffedboxzone.BottomRight.Y && (double) this.enemyrenderere.vLocation.X + (double) this.enemyrenderere.HalfEnemyWidth >= (double) this.reffedboxzone.BottomRight.X)
        {
          flag = true;
          if (this.movingLikeThis == DirectionMoving.DownRight)
            this.movingLikeThis = DirectionMoving.UpLeft;
        }
        else if ((double) this.enemyrenderere.vLocation.Y + (double) this.enemyrenderere.HalfEnemyHeight >= (double) this.reffedboxzone.BottomRight.Y && (double) this.enemyrenderere.vLocation.X - (double) this.enemyrenderere.HalfEnemyWidth < (double) this.reffedboxzone.TopLeft.X)
        {
          flag = true;
          this.movingLikeThis = this.movingLikeThis != DirectionMoving.DownLeft ? DirectionMoving.UpRight : DirectionMoving.UpRight;
        }
        if ((double) this.enemyrenderere.vLocation.Y + (double) this.enemyrenderere.HalfEnemyHeight >= (double) this.reffedboxzone.BottomRight.Y)
        {
          flag = true;
          if (this.movingLikeThis == DirectionMoving.DownRight)
            this.movingLikeThis = DirectionMoving.UpRight;
          else if (this.movingLikeThis == DirectionMoving.DownLeft)
            this.movingLikeThis = DirectionMoving.UpLeft;
        }
      }
      if ((double) this.enemyrenderere.vLocation.X > (double) vLocation.X && !flag)
      {
        if ((double) this.enemyrenderere.vLocation.X + (double) this.enemyrenderere.HalfEnemyWidth >= (double) this.reffedboxzone.BottomRight.X)
        {
          flag = true;
          if (this.movingLikeThis == DirectionMoving.UpRight)
            this.movingLikeThis = DirectionMoving.UpLeft;
          else
            this.movingLikeThis = this.movingLikeThis == DirectionMoving.DownRight ? DirectionMoving.DownLeft : throw new Exception("HUH");
        }
      }
      else if ((double) this.enemyrenderere.vLocation.X < (double) vLocation.X && !flag && (double) this.enemyrenderere.vLocation.X - (double) this.enemyrenderere.HalfEnemyWidth < (double) this.reffedboxzone.TopLeft.X)
      {
        flag = true;
        if (this.movingLikeThis == DirectionMoving.UpLeft)
          this.movingLikeThis = DirectionMoving.UpRight;
        else
          this.movingLikeThis = this.movingLikeThis == DirectionMoving.DownLeft ? DirectionMoving.DownRight : throw new Exception("HUH");
      }
      if (flag)
      {
        this.enemyrenderere.vLocation = vLocation;
        this.SetVector();
      }
      this.enemyrenderere.UpdateAnimalRenderer(DeltaTime);
      return false;
    }

    public bool CheckCollision(Vector2 LocationInWorldSpace)
    {
      if (this.enemyrenderere.corpse != null)
        return MathStuff.CheckPointCollision(true, this.enemyrenderere.corpse.vLocation + new Vector2(0.0f, this.enemyrenderere.corpse.DrawOrigin.Y * 0.5f), this.enemyrenderere.corpse.scale * Sengine.WorldOriginandScale.Z, (float) this.enemyrenderere.corpse.DrawRect.Width, (float) this.enemyrenderere.corpse.DrawRect.Height, LocationInWorldSpace);
      return this.enemyrenderere.animator != null ? MathStuff.CheckPointCollision(true, this.enemyrenderere.vLocation + (this.enemyrenderere.animator.PositionalOffset - new Vector2(0.0f, (float) (this.enemyrenderere.DrawRect.Height / 2))), this.enemyrenderere.scale, (float) this.enemyrenderere.DrawRect.Width, ((float) this.enemyrenderere.DrawRect.Height - this.enemyrenderere.animator.PositionalOffset.Y) * Sengine.ScreenRatioUpwardsMultiplier.Y, LocationInWorldSpace) : MathStuff.CheckPointCollision(true, this.enemyrenderere.vLocation, this.enemyrenderere.scale * Sengine.WorldOriginandScale.Z, (float) this.enemyrenderere.DrawRect.Width, (float) this.enemyrenderere.DrawRect.Height, LocationInWorldSpace);
    }

    public void DrawEnemy()
    {
      if (AnimalsInPens.AnimalUID > -1 && AnimalsInPens.AnimalUID == this.refperson.UID)
      {
        this.enemyrenderere.DrawEnemyRenderer(true);
        AnimalsInPens.AnimalUID = -1;
      }
      else
        this.enemyrenderere.DrawEnemyRenderer();
    }
  }
}
