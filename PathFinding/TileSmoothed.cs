// Decompiled with JetBrains decompiler
// Type: TinyZoo.PathFinding.TileSmoothed
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;

namespace TinyZoo.PathFinding
{
  internal class TileSmoothed
  {
    private Vector2Int prevTileCtr;
    private Vector2Int nextTileCtr;
    private float relativeDistance;
    private bool firstnode;
    private float moveSpeed;
    private Vector2 nextTurnVec;
    private bool nextTurnChecked;
    private Vector2 offset;
    private Vector2 currOffset;
    private bool nextFacingIsLeft;
    private static Random rand = new Random();
    private static float[] offsetsvals = new float[3]
    {
      -1f,
      0.0f,
      1f
    };
    private float t;
    private Vector2 nextTileCenter;
    private Vector2 currTileCenter;
    private Vector2 prevTileCenter;
    private Vector2 comingVec;
    private Vector2 goingVec;
    private bool firstrun;
    private bool noSmooth;
    private bool lastTile;
    private float speedMult;
    private bool movingToOffset;
    private bool movingBackToCenter;
    private bool lastEdge;
    private bool sameTileWalkToCenter;
    private Vector2 sameTileVec;
    private Vector2 v1;
    private Vector2 v2;

    public TileSmoothed(float _moveSpeed)
    {
      this.moveSpeed = _moveSpeed;
      this.offset = Vector2.Zero;
      this.ReInitialize();
    }

    public Vector2 GiveRandomOffset()
    {
      this.offset = new Vector2(0.49f * TileSmoothed.offsetsvals[TileSmoothed.rand.Next() % 3], 0.49f * TileSmoothed.offsetsvals[TileSmoothed.rand.Next() % 3]);
      return this.offset;
    }

    public void ReInitialize()
    {
      this.firstnode = true;
      this.firstrun = true;
      this.nextTurnVec = Vector2.Zero;
      this.nextTurnChecked = false;
      this.nextFacingIsLeft = false;
      this.t = 0.0f;
      this.lastTile = false;
      this.currOffset = Vector2.Zero;
      this.lastEdge = false;
      this.sameTileWalkToCenter = false;
    }

    public bool UpdateNavigationTileSmoothed(
      float dt,
      List<PathNode> CurrentPath,
      ref Vector2Int CurrentTile,
      ref Vector2 relativeLocation,
      ref DirectionPressed directionmovedthisframe,
      ref bool facingLeft,
      bool endAtCenter = false)
    {
      bool flag1 = true;
      if (CurrentPath == null)
        return true;
      Vector2Int vector2Int = CurrentTile;
      Vector2 vector2_1 = relativeLocation;
      if (this.sameTileWalkToCenter)
      {
        this.SetDirectionMoved(this.sameTileVec, ref directionmovedthisframe);
        this.GetFacingFromVector(this.sameTileVec, ref facingLeft);
        this.currOffset += dt * this.moveSpeed * this.sameTileVec;
        if ((double) this.DotProduct(-this.currOffset, this.sameTileVec) < 0.00700000021606684)
        {
          this.firstnode = true;
          this.firstrun = true;
          this.movingBackToCenter = false;
          this.lastTile = false;
          this.lastEdge = false;
          relativeLocation = vector2_1 = this.currOffset = Vector2.Zero;
          CurrentTile = vector2Int;
          this.sameTileWalkToCenter = false;
          return true;
        }
        relativeLocation = this.currOffset;
        CurrentTile = vector2Int;
        return false;
      }
      if (this.lastTile)
      {
        if ((this.movingToOffset || this.movingBackToCenter) && (!this.movingToOffset || !this.movingBackToCenter))
          this.UpdateT_ConstantSpeedBezier(dt, this.speedMult * this.moveSpeed, ref this.t);
        else
          this.UpdateT_ConstantSpeedBezier(dt, this.moveSpeed, ref this.t);
        Vector2 vector2_2 = (float) (2.0 * (0.5 - (double) this.t)) * -this.comingVec;
        if (!this.movingBackToCenter || !this.movingToOffset)
        {
          if (this.movingToOffset)
          {
            float num = this.t + 0.5f;
            if ((double) num > 1.0)
            {
              num = 1f;
              this.movingToOffset = false;
            }
            this.currOffset = num * this.offset;
          }
          if (this.movingBackToCenter)
          {
            float num = 0.5f - this.t;
            if ((double) num < 0.0)
            {
              num = 0.0f;
              this.movingBackToCenter = false;
            }
            this.currOffset = num * this.offset;
          }
        }
        this.SetDirectionMoved(this.goingVec, ref directionmovedthisframe);
        this.GetFacingFromVector(this.goingVec, ref facingLeft);
        if ((double) this.t > 0.5)
        {
          this.firstnode = true;
          this.firstrun = true;
          relativeLocation = vector2_1 = this.currOffset;
          CurrentTile = vector2Int;
          this.lastTile = false;
          this.movingBackToCenter = false;
          this.lastEdge = false;
          if (CurrentPath.Count != 0)
            return false;
          if (!endAtCenter || this.currOffset == Vector2.Zero)
            return true;
          this.sameTileVec = -this.currOffset;
          this.sameTileVec.Normalize();
          this.sameTileWalkToCenter = true;
          return false;
        }
        relativeLocation = vector2_2 + this.currOffset;
        CurrentTile = vector2Int;
        return false;
      }
      if (this.firstrun)
      {
        if (CurrentPath.Count == 0)
        {
          if (!endAtCenter || this.currOffset == Vector2.Zero)
            return true;
          this.sameTileVec = -this.currOffset;
          this.sameTileVec.Normalize();
          this.sameTileWalkToCenter = true;
          return false;
        }
        this.t = 0.5f;
        this.currTileCenter = new Vector2((float) vector2Int.X, (float) vector2Int.Y);
        this.nextTileCtr = CurrentPath[0].Location;
        this.nextTileCenter = new Vector2((float) this.nextTileCtr.X, (float) this.nextTileCtr.Y);
        this.goingVec = this.nextTileCenter - this.currTileCenter;
        this.comingVec = this.goingVec;
        this.SetDirectionMoved(this.goingVec, ref directionmovedthisframe);
        this.GetFacingFromVector(this.goingVec, ref facingLeft);
        this.noSmooth = true;
        this.lastEdge = false;
        this.firstrun = false;
        this.SetVectors_ConstantSpeedBezier();
        bool flag2 = false;
        if (CurrentPath.Count == 1)
        {
          this.lastEdge = true;
          if (endAtCenter)
          {
            flag2 = true;
            if (this.currOffset != Vector2.Zero)
            {
              this.movingBackToCenter = true;
              this.speedMult = 1f / (this.nextTileCenter - (this.currTileCenter + this.offset)).Length();
            }
          }
        }
        if (this.currOffset == Vector2.Zero && this.offset != Vector2.Zero && !flag2)
        {
          this.movingToOffset = true;
          this.speedMult = 1f / (this.nextTileCenter + this.offset - this.currTileCenter).Length();
        }
      }
      if ((this.movingToOffset || this.movingBackToCenter) && (!this.movingToOffset || !this.movingBackToCenter))
        this.UpdateT_ConstantSpeedBezier(dt, this.speedMult * this.moveSpeed, ref this.t);
      else
        this.UpdateT_ConstantSpeedBezier(dt, this.moveSpeed, ref this.t);
      Vector2 vector2_3;
      if (this.noSmooth)
      {
        vector2_3 = (double) this.t < 0.5 ? (float) (1.0 - 2.0 * (double) this.t) * -this.comingVec : (float) (2.0 * ((double) this.t - 0.5)) * this.goingVec;
      }
      else
      {
        Vector2 vector2_2 = (1f - this.t) * -this.comingVec;
        Vector2 vector2_4 = this.t * this.goingVec;
        vector2_3 = vector2_2 + this.t * (vector2_4 - vector2_2);
      }
      if (!this.movingToOffset || !this.movingBackToCenter)
      {
        if (this.movingToOffset)
        {
          float num = !this.firstnode ? this.t + 0.5f : this.t - 0.5f;
          if ((double) num > 1.0)
          {
            num = 1f;
            this.movingToOffset = false;
          }
          this.currOffset = num * this.offset;
        }
        if (this.movingBackToCenter)
        {
          float num = 1f - (this.t - 0.5f);
          if ((double) num < 0.0)
          {
            num = 0.0f;
            this.movingBackToCenter = false;
          }
          this.currOffset = num * this.offset;
        }
      }
      this.SetDirectionMoved(this.goingVec, ref directionmovedthisframe);
      this.SetFacing(CurrentPath, ref facingLeft);
      if ((double) this.t > 0.5 && CurrentPath.Count == 1)
      {
        this.lastEdge = true;
        if (endAtCenter && this.currOffset == this.offset)
        {
          this.movingBackToCenter = true;
          this.speedMult = 1f / (this.nextTileCenter - (this.currTileCenter + this.offset)).Length();
        }
      }
      if ((double) this.t > 1.0)
      {
        this.t = 0.0f;
        vector2_3 = -this.goingVec;
        this.SetDirectionMoved(this.goingVec, ref directionmovedthisframe);
        this.GetFacingFromVector(this.goingVec, ref facingLeft);
        this.prevTileCtr = vector2Int;
        vector2Int = new Vector2Int(CurrentPath[0].Location);
        CurrentPath.RemoveAt(0);
        if (CurrentPath.Count == 0 || this.lastEdge)
        {
          this.nextTileCtr = (Vector2Int) null;
          this.nextTileCenter = Vector2.Zero;
          this.lastTile = true;
        }
        else
        {
          this.nextTileCtr = new Vector2Int(CurrentPath[0].Location);
          this.nextTileCenter = new Vector2((float) this.nextTileCtr.X, (float) this.nextTileCtr.Y);
        }
        this.prevTileCenter = new Vector2((float) this.prevTileCtr.X, (float) this.prevTileCtr.Y);
        this.currTileCenter = new Vector2((float) vector2Int.X, (float) vector2Int.Y);
        this.comingVec = this.currTileCenter - this.prevTileCenter;
        this.goingVec = this.nextTileCtr == null ? this.comingVec : this.nextTileCenter - this.currTileCenter;
        this.SetVectors_ConstantSpeedBezier();
        Vector2 vector2_2 = this.comingVec + this.goingVec;
        bool flag2 = (double) this.DotProduct(this.comingVec, this.goingVec) == 0.0 && (double) vector2_2.X * (double) vector2_2.Y < 0.0;
        this.noSmooth = flag1 & flag2;
        this.firstnode = false;
      }
      relativeLocation = vector2_3 + this.currOffset;
      CurrentTile = vector2Int;
      return false;
    }

    private void SetFacing(List<PathNode> currentPath, ref bool facingLeft)
    {
      if ((double) this.goingVec.X * (double) this.goingVec.Y < 0.0)
      {
        if (!this.nextTurnChecked)
        {
          this.LookForwardForNextTurnDirection(currentPath);
          this.nextTurnChecked = true;
        }
        if (this.nextTurnChecked && this.nextTurnVec != Vector2.Zero)
          facingLeft = this.nextFacingIsLeft;
        else
          this.GetFacingFromVector(this.goingVec, ref facingLeft);
      }
      else
      {
        if ((double) this.t > 0.5)
          this.GetFacingFromVector(this.goingVec, ref facingLeft);
        else
          this.GetFacingFromVector(this.comingVec, ref facingLeft);
        this.nextTurnChecked = false;
        this.nextTurnVec = Vector2.Zero;
      }
    }

    private void UpdateT_ConstantSpeedBezier(float dt, float moveSpeed, ref float t)
    {
      float num = moveSpeed * dt;
      t += num / (t * this.v1 + this.v2).Length();
    }

    private void SetVectors_ConstantSpeedBezier()
    {
      Vector2 vector2 = -this.comingVec;
      Vector2 zero = Vector2.Zero;
      Vector2 goingVec = this.goingVec;
      this.v1 = 2f * vector2 - 4f * zero + 2f * goingVec;
      this.v2 = -2f * vector2 + 2f * zero;
    }

    private void LookForwardForNextTurnDirection(List<PathNode> CurrentPath)
    {
      Vector2 vector2_1 = this.nextTileCenter;
      for (int index = 1; index < CurrentPath.Count; ++index)
      {
        Vector2Int location = CurrentPath[index].Location;
        Vector2 vector2_2 = new Vector2((float) location.X, (float) location.Y);
        Vector2 vector2_3 = vector2_2 - vector2_1;
        vector2_1 = vector2_2;
        if ((double) vector2_3.X * (double) vector2_3.Y != -1.0)
        {
          this.nextTurnVec = vector2_3;
          this.GetFacingFromVector(this.nextTurnVec, ref this.nextFacingIsLeft);
          break;
        }
      }
    }

    private float DotProduct(Vector2 lhs, Vector2 rhs) => (float) ((double) lhs.X * (double) rhs.X + (double) lhs.Y * (double) rhs.Y);

    private void GetFacingFromVector(Vector2 vec, ref bool facingLeft)
    {
      DirectionPressed directionmoved = DirectionPressed.None;
      this.SetDirectionMoved(vec, ref directionmoved);
      switch (directionmoved)
      {
        case DirectionPressed.Up:
        case DirectionPressed.Left:
        case DirectionPressed.NorthWest:
          facingLeft = true;
          break;
        case DirectionPressed.Right:
        case DirectionPressed.Down:
        case DirectionPressed.SouthEast:
          facingLeft = false;
          break;
      }
    }

    private void SetDirectionMoved(Vector2 vec, ref DirectionPressed directionmoved)
    {
      if ((double) vec.X * (double) vec.Y != 0.0)
      {
        if ((double) vec.X > 0.0)
        {
          if ((double) vec.Y > 0.0)
          {
            directionmoved = DirectionPressed.SouthEast;
          }
          else
          {
            if ((double) vec.Y >= 0.0)
              return;
            directionmoved = DirectionPressed.NorthEast;
          }
        }
        else
        {
          if ((double) vec.X >= 0.0)
            return;
          if ((double) vec.Y > 0.0)
          {
            directionmoved = DirectionPressed.SouthWest;
          }
          else
          {
            if ((double) vec.Y >= 0.0)
              return;
            directionmoved = DirectionPressed.NorthWest;
          }
        }
      }
      else
      {
        if ((double) vec.Y > 0.0)
          directionmoved = DirectionPressed.Down;
        else if ((double) vec.Y < 0.0)
          directionmoved = DirectionPressed.Up;
        if ((double) vec.X > 0.0)
        {
          directionmoved = DirectionPressed.Right;
        }
        else
        {
          if ((double) vec.X >= 0.0)
            return;
          directionmoved = DirectionPressed.Left;
        }
      }
    }
  }
}
