// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.PenNav.PenNavigator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_AnimalsAndPeople.PenNav.CurrentPens;
using TinyZoo.Z_OverWorld._OverWorldEnv.AnimalsInPens;

namespace TinyZoo.Z_AnimalsAndPeople.PenNav
{
  internal class PenNavigator
  {
    private PenNavCollection pennav;
    private Vector2Int CurrentArrayLocation;
    public float MovementSpeed;
    public BouncingBallFriction frictionhandler;
    public CellBlockPathFinder CellblockPathFinder;
    private NavigationType navigationtype;
    private bool BreakingOut;
    private bool HasBrokenOut;
    public Vector2Int CurrentWorldSpaceLocation;
    private List<PathNode> currentpath;
    private Vector2Int BreakoutNav_NextTarget;
    private Vector2Int BreakoutNav_LastTarget;
    private Vector2 Direction;
    private Vector2 Limits;
    private Rectangle AnimalSize;
    private static Vector2Int TempNavvLoc = new Vector2Int();

    public PenNavigator(List<Vector2Int> FloorLocations, int PenUID, AnimalType animaltype)
    {
      this.CellblockPathFinder = new CellBlockPathFinder();
      this.MovementSpeed = EnemyData.GetMovementsSpeed(animaltype);
      this.pennav = StaticPenNavPool.GetThisPenNav(FloorLocations, PenUID);
    }

    public void StartNavigating(
      ref Vector2 location,
      Rectangle _AnimalSize,
      bool ForceLocation = false,
      NavigationType _navigationtype = NavigationType.Standard)
    {
      this.navigationtype = _navigationtype;
      this.AnimalSize = _AnimalSize;
      if (this.navigationtype == NavigationType.RollingBall || this.navigationtype == NavigationType.ExternallyControlled)
      {
        this.MovementSpeed = 40f;
        this.pennav.GetLocationToArrayLocation(out this.CurrentArrayLocation, location, out this.CurrentWorldSpaceLocation);
      }
      else if (ForceLocation)
      {
        this.pennav.GetLocationToArrayLocation(out this.CurrentArrayLocation, location, out this.CurrentWorldSpaceLocation);
      }
      else
      {
        this.pennav.GetRandomStartLocation(out this.CurrentArrayLocation, out this.CurrentWorldSpaceLocation);
        location = TileMath.GetTileToWorldSpace(this.pennav.navlocations[this.CurrentArrayLocation.X, this.CurrentArrayLocation.Y].Position);
      }
      this.Direction = MathStuff.GetRandomVector2(new Vector2(-1f, -1f), Vector2.One, TinyZoo.Game1.Rnd);
      this.SetLimits();
      if (this.navigationtype != NavigationType.RollingBall)
        return;
      this.frictionhandler = new BouncingBallFriction();
    }

    public void EndBreakOut(Vector2 vLOCation)
    {
      this.HasBrokenOut = false;
      this.BreakingOut = false;
      this.currentpath = (List<PathNode>) null;
      this.pennav.GetLocationToArrayLocation(out this.CurrentArrayLocation, vLOCation, out this.CurrentWorldSpaceLocation);
    }

    public void StartBreakOut(Vector2Int GoHere, Vector2Int SpaceBehindGate)
    {
      if (this.BreakingOut)
        return;
      this.BreakingOut = true;
      this.GoToLocation(SpaceBehindGate);
      if (this.currentpath == null)
        this.currentpath = new List<PathNode>();
      this.currentpath.Add(new PathNode(GoHere.X, GoHere.Y));
      if (GoHere.X < SpaceBehindGate.X)
        this.currentpath.Add(new PathNode(GoHere.X - 1, GoHere.Y));
      else if (GoHere.X > SpaceBehindGate.X)
        this.currentpath.Add(new PathNode(GoHere.X + 1, GoHere.Y));
      else if (GoHere.Y < SpaceBehindGate.Y)
      {
        this.currentpath.Add(new PathNode(GoHere.X, GoHere.Y - 1));
      }
      else
      {
        if (GoHere.Y <= SpaceBehindGate.Y)
          return;
        this.currentpath.Add(new PathNode(GoHere.X, GoHere.Y + 1));
      }
    }

    public PenNavCollection GetPenNavCollection() => this.pennav;

    private void SetLimits()
    {
      Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(this.pennav.navlocations[this.CurrentArrayLocation.X, this.CurrentArrayLocation.Y].Position);
      if ((double) this.Direction.X > 0.0)
      {
        this.Limits.X = tileToWorldSpace.X + TileMath.HalfTileSize;
        this.Limits.X -= 4f;
      }
      else
      {
        this.Limits.X = tileToWorldSpace.X - TileMath.HalfTileSize;
        this.Limits.X += 4f;
      }
      if ((double) this.Direction.Y > 0.0)
        this.Limits.Y = tileToWorldSpace.Y + TileMath.HalfTileSize;
      else
        this.Limits.Y = tileToWorldSpace.Y - TileMath.HalfTileSize;
    }

    public void FixLocationsOnStartFight(
      EnemyRenderer Attacker,
      EnemyRenderer Defender,
      bool AttackerWentLeft)
    {
      Vector2Int GridLoc;
      if (this.pennav.IsInNavGrid(Attacker.vLocation, out GridLoc))
      {
        if (AttackerWentLeft)
        {
          if (this.pennav.CanGoHere(new Vector2Int(GridLoc.X - 1, GridLoc.Y)))
            return;
          GridLoc.X += this.pennav.Left;
          GridLoc.Y += this.pennav.Top;
          float num = TileMath.GetTileToWorldSpace(GridLoc).X - TileMath.HalfTileSize + 4f;
          if ((double) Attacker.vLocation.X >= (double) num)
            return;
          Attacker.vLocation.X = num;
        }
        else
        {
          if (this.pennav.CanGoHere(new Vector2Int(GridLoc.X + 1, GridLoc.Y)))
            return;
          GridLoc.X += this.pennav.Left;
          GridLoc.Y += this.pennav.Top;
          float num = TileMath.GetTileToWorldSpace(GridLoc).X + TileMath.HalfTileSize - 6f;
          if ((double) Attacker.vLocation.X <= (double) num)
            return;
          Attacker.vLocation.X = num;
        }
      }
      else
      {
        if (!this.pennav.IsInNavGrid(Defender.vLocation, out GridLoc))
          throw new Exception("DAMN the defender is already off the grid? Not POSSIBLE");
        GridLoc.X += this.pennav.Left;
        GridLoc.Y += this.pennav.Top;
        Attacker.vLocation.X = TileMath.GetTileToWorldSpace(GridLoc).X;
        Defender.vLocation.X = Attacker.vLocation.X;
        if (AttackerWentLeft)
        {
          Attacker.vLocation.X -= TileMath.HalfTileSize;
          Attacker.vLocation.X += 6f;
          Defender.vLocation.X += TileMath.HalfTileSize;
          Defender.vLocation.X -= 6f;
        }
        else
        {
          Defender.vLocation.X -= TileMath.HalfTileSize;
          Defender.vLocation.X += 6f;
          Attacker.vLocation.X += TileMath.HalfTileSize;
          Attacker.vLocation.X -= 6f;
        }
      }
    }

    public void GoToLocation(Vector2Int WorldSpaceLocationTargetLocation)
    {
      this.currentpath = this.pennav.GetPathToHere(WorldSpaceLocationTargetLocation, this.CurrentWorldSpaceLocation);
      for (int index = 0; index < this.currentpath.Count; ++index)
        this.currentpath[index] = new PathNode(this.currentpath[index].XLoc + this.pennav.Left, this.currentpath[index].YLoc + this.pennav.Top);
    }

    public void UpdatePenNavigator(
      ref Vector2 Location,
      float DeltaTime,
      EnemyRenderer enemyrenderere,
      PrisonerInfo prisoninfo = null)
    {
      if (this.navigationtype == NavigationType.RollingBall)
      {
        this.frictionhandler.UpdateBalls(ref DeltaTime);
      }
      else
      {
        if (this.navigationtype == NavigationType.ExternallyControlled)
          return;
        if (enemyrenderere.IsDead)
        {
          enemyrenderere.UpdateAnimalRenderer(DeltaTime);
          return;
        }
      }
      if (this.currentpath != null && this.currentpath.Count > 0)
      {
        if (!this.DoNav(this.currentpath[0].XLoc, this.currentpath[0].YLoc, DeltaTime, ref Location, enemyrenderere))
          return;
        this.currentpath.RemoveAt(0);
        if (this.currentpath.Count != 0 || !this.BreakingOut)
          return;
        this.BreakoutNav_LastTarget = new Vector2Int(this.CurrentWorldSpaceLocation);
        this.BreakoutNav_NextTarget = this.GetLocationAroundThis(this.CurrentWorldSpaceLocation, this.BreakoutNav_LastTarget);
        this.HasBrokenOut = true;
      }
      else if (this.BreakingOut && this.HasBrokenOut)
      {
        if (!this.DoNav(this.BreakoutNav_NextTarget.X, this.BreakoutNav_NextTarget.Y, DeltaTime, ref Location, enemyrenderere))
          return;
        int x = this.CurrentWorldSpaceLocation.X;
        int y = this.CurrentWorldSpaceLocation.Y;
        this.CurrentWorldSpaceLocation.X = this.BreakoutNav_NextTarget.X;
        this.CurrentWorldSpaceLocation.Y = this.BreakoutNav_NextTarget.Y;
        this.BreakoutNav_NextTarget = this.GetLocationAroundThis(this.CurrentWorldSpaceLocation, this.BreakoutNav_LastTarget);
        this.BreakoutNav_LastTarget.X = x;
        this.BreakoutNav_LastTarget.Y = y;
        Z_GameFlags.SetAnimalInBreakOut(this.CurrentWorldSpaceLocation, prisoninfo);
      }
      else
      {
        Location += this.Direction * DeltaTime * this.MovementSpeed;
        bool flag = false;
        if ((double) this.Direction.X < 0.0)
        {
          if (enemyrenderere != null)
            enemyrenderere.DirectionFacing.X = -1f;
          if ((double) Location.X < (double) this.Limits.X)
          {
            if (this.pennav.CanGoHere(this.CurrentArrayLocation + new Vector2Int(-1, 0)))
            {
              --this.CurrentArrayLocation.X;
              --this.CurrentWorldSpaceLocation.X;
              flag = true;
            }
            else
            {
              this.Direction.X *= -1f;
              flag = true;
            }
          }
        }
        else
        {
          if (enemyrenderere != null)
            enemyrenderere.DirectionFacing.X = 1f;
          if ((double) Location.X > (double) this.Limits.X)
          {
            if (this.pennav.CanGoHere(this.CurrentArrayLocation + new Vector2Int(1, 0)))
            {
              ++this.CurrentArrayLocation.X;
              ++this.CurrentWorldSpaceLocation.X;
              flag = true;
            }
            else
            {
              if ((double) this.Direction.X > 0.0)
                this.Direction.X *= -1f;
              flag = true;
            }
          }
        }
        if ((double) this.Direction.Y > 0.0)
        {
          if ((double) Location.Y > (double) this.Limits.Y)
          {
            if (this.pennav.CanGoHere(this.CurrentArrayLocation + new Vector2Int(0, 1)))
            {
              flag = true;
              ++this.CurrentArrayLocation.Y;
              ++this.CurrentWorldSpaceLocation.Y;
            }
            else
            {
              this.Direction.Y *= -1f;
              flag = true;
            }
          }
        }
        else if ((double) Location.Y < (double) this.Limits.Y)
        {
          if (this.pennav.CanGoHere(this.CurrentArrayLocation + new Vector2Int(0, -1)))
          {
            --this.CurrentArrayLocation.Y;
            --this.CurrentWorldSpaceLocation.Y;
            flag = true;
          }
          else
          {
            this.Direction.Y *= -1f;
            flag = true;
          }
        }
        if (!flag)
          return;
        this.SetLimits();
      }
    }

    public Vector2Int GetLocationAroundThis(
      Vector2Int CurrentLocation,
      Vector2Int lastLocation)
    {
      int maxValue1 = 0;
      bool flag1 = false;
      if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X - 1, CurrentLocation.Y))
      {
        if (lastLocation.X == CurrentLocation.X - 1 && lastLocation.Y == CurrentLocation.Y)
          flag1 = true;
        ++maxValue1;
      }
      if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X + 1, CurrentLocation.Y))
      {
        if (lastLocation.X == CurrentLocation.X + 1 && lastLocation.Y == CurrentLocation.Y)
          flag1 = true;
        ++maxValue1;
      }
      if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y + 1))
      {
        if (lastLocation.X == CurrentLocation.X && lastLocation.Y == CurrentLocation.Y + 1)
          flag1 = true;
        ++maxValue1;
      }
      if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y - 1))
      {
        if (lastLocation.X == CurrentLocation.X && lastLocation.Y == CurrentLocation.Y - 1)
          flag1 = true;
        ++maxValue1;
      }
      int num1;
      if (flag1 && maxValue1 > 1)
      {
        int maxValue2 = maxValue1 - 1;
        int num2 = maxValue2 <= 1 ? maxValue2 - 1 : TinyZoo.Game1.Rnd.Next(0, maxValue2);
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X - 1, CurrentLocation.Y) && (lastLocation.X != CurrentLocation.X - 1 || lastLocation.Y != CurrentLocation.Y))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X - 1, CurrentLocation.Y);
          --num2;
        }
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X + 1, CurrentLocation.Y) && (lastLocation.X != CurrentLocation.X + 1 || lastLocation.Y != CurrentLocation.Y))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X + 1, CurrentLocation.Y);
          --num2;
        }
        bool flag2;
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y + 1))
        {
          if (lastLocation.X == CurrentLocation.X && lastLocation.Y == CurrentLocation.Y + 1)
          {
            flag2 = true;
          }
          else
          {
            if (num2 == 0)
              return new Vector2Int(CurrentLocation.X, CurrentLocation.Y + 1);
            --num2;
          }
        }
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y - 1))
        {
          if (lastLocation.X == CurrentLocation.X && lastLocation.Y == CurrentLocation.Y - 1)
          {
            flag2 = true;
          }
          else
          {
            if (num2 == 0)
              return new Vector2Int(CurrentLocation.X, CurrentLocation.Y - 1);
            num1 = num2 - 1;
          }
        }
        throw new Exception("YOU HOULD HAVE RETURNED SOMETHIF");
      }
      if (flag1 && maxValue1 == 1)
        return new Vector2Int(lastLocation);
      if (maxValue1 > 0)
      {
        int num2 = maxValue1 <= 1 ? maxValue1 - 1 : TinyZoo.Game1.Rnd.Next(0, maxValue1);
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X - 1, CurrentLocation.Y))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X - 1, CurrentLocation.Y);
          --num2;
        }
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X + 1, CurrentLocation.Y))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X + 1, CurrentLocation.Y);
          --num2;
        }
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y + 1))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X, CurrentLocation.Y + 1);
          --num2;
        }
        if (!Z_GameFlags.pathfinder.GetIsBlocked(CurrentLocation.X, CurrentLocation.Y - 1))
        {
          if (num2 == 0)
            return new Vector2Int(CurrentLocation.X, CurrentLocation.Y - 1);
          num1 = num2 - 1;
        }
        throw new Exception("YOU HOULD HAVE RETURNED SOMETHIF");
      }
      return (Vector2Int) null;
    }

    private bool DoNav(
      int NextLocationX,
      int NextLocationY,
      float DeltaTime,
      ref Vector2 Location,
      EnemyRenderer enemyrenderere)
    {
      PenNavigator.TempNavvLoc.X = NextLocationX;
      PenNavigator.TempNavvLoc.Y = NextLocationY;
      Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(PenNavigator.TempNavvLoc);
      this.Direction = tileToWorldSpace - Location;
      if (this.Direction == Vector2.Zero)
        return true;
      float num = this.Direction.LengthSquared();
      this.Direction.Normalize();
      if ((double) (this.Direction * DeltaTime * this.MovementSpeed).LengthSquared() >= (double) num)
      {
        Location = tileToWorldSpace;
        this.CurrentWorldSpaceLocation = new Vector2Int(NextLocationX, NextLocationY);
        return true;
      }
      Location += this.Direction * DeltaTime * this.MovementSpeed;
      if ((double) this.Direction.X > 0.0)
        enemyrenderere.DirectionFacing.X = 1f;
      else if ((double) this.Direction.X < 0.0)
        enemyrenderere.DirectionFacing.X = -1f;
      return false;
    }
  }
}
