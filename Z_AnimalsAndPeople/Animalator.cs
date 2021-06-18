// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Animalator
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Lerp;
using System;

namespace TinyZoo.Z_AnimalsAndPeople
{
  internal class Animalator
  {
    public Vector2 CurrentScale;
    private Vector2 JumpStretch;
    private Vector2 LandSquish;
    public float CurrentRotation;
    public Vector2 PositionalOffset;
    public CurrentAnimationType currentanimation;
    private AnimationProfile Ref_animationprofile;
    private SinLerper JumpLerper;
    private SinLerper FatLandingLerper;
    private float GetReadyToJumpTime;
    private bool FatSquishStarted;
    private float JumpPause;
    public bool GoingUpwards;
    private float FullJumpTime;
    private float JumpHeight;
    private float DeltaTimeMultiplier;
    private float JumpTimer;
    private bool Isbaby;
    private bool StopAutoJump;
    private bool ForceJump;
    public bool JustHitJumpPeak;
    public Vector2 TargetJumpPeakLoc;
    private bool UsingJumpPeakLoc;
    private bool UsingJumpPeakLocNextJump;
    public bool JumpingToSpecialTaget;
    private float SpecialDownMultiplier = 1f;
    private Vector2 OverrideTargetLocation;
    public bool IsJumping;

    public Animalator(AnimationProfile animationprofile)
    {
      this.UsingJumpPeakLocNextJump = false;
      this.UsingJumpPeakLoc = false;
      this.ForceJump = false;
      this.Ref_animationprofile = animationprofile;
      this.currentanimation = CurrentAnimationType.Walk;
      this.CurrentScale = Vector2.One;
      this.JumpStretch = Vector2.One;
      this.JumpLerper = new SinLerper();
      this.JumpPause = 0.0f;
      this.FatLandingLerper = new SinLerper();
    }

    public void TryToJump()
    {
      this.JustHitJumpPeak = false;
      this.ForceJump = true;
      this.JumpPause = 0.0f;
    }

    public void JumpHere(Vector2 TargetJumpLocation, Vector2 CurrentLocation)
    {
      this.OverrideTargetLocation = TargetJumpLocation - CurrentLocation;
      this.TryToJumpForAttack(new Vector2(this.OverrideTargetLocation.X * 0.5f, Math.Min(this.OverrideTargetLocation.Y, 0.0f) - this.JumpHeight));
      this.UsingJumpPeakLoc = true;
      this.StartJump();
      this.JumpingToSpecialTaget = true;
      this.ForceJump = false;
      this.UsingJumpPeakLocNextJump = false;
      this.SpecialDownMultiplier = 2f;
      if ((double) CurrentLocation.Y < (double) TargetJumpLocation.Y)
        this.SpecialDownMultiplier = 1f;
      this.JumpPause = 0.0f;
    }

    public void TryToJumpForAttack(Vector2 TargetCollision)
    {
      this.SpecialDownMultiplier = 1f;
      if ((double) this.JumpPause <= 0.0)
      {
        this.UsingJumpPeakLocNextJump = true;
      }
      else
      {
        this.UsingJumpPeakLocNextJump = true;
        this.JumpPause = 0.0f;
      }
      this.TargetJumpPeakLoc = TargetCollision;
      this.JustHitJumpPeak = false;
      this.ForceJump = true;
    }

    public void StopJumping() => this.StopAutoJump = true;

    public void UnStopJumping() => this.StopAutoJump = false;

    public void SetAsBaby(bool _Isbaby = true) => this.Isbaby = _Isbaby;

    public bool GetCanMove() => (double) this.JumpLerper.CurrentValue > 0.0;

    public void UpdateAnimalator(float DeltaTime)
    {
      int num1 = this.ForceJump ? 1 : 0;
      if ((double) this.JumpPause <= 0.0)
      {
        if (!this.GoingUpwards && this.JumpingToSpecialTaget)
          DeltaTime *= this.SpecialDownMultiplier;
        this.JumpLerper.UpdateSinLerper(DeltaTime / this.FullJumpTime);
        if (this.JumpLerper.IsComplete)
        {
          if (this.GoingUpwards)
          {
            this.GoingUpwards = false;
            this.JustHitJumpPeak = true;
            this.JumpLerper.SetLerp(SinCurveType.EaseIn, this.FullJumpTime * 0.5f, 1f, 0.0f, true);
          }
          else
          {
            this.IsJumping = false;
            this.JumpPause = this.Ref_animationprofile.GetJumpGap();
            if ((double) this.JumpPause <= 0.0 || this.ForceJump)
            {
              this.ForceJump = false;
              this.StartJump();
              this.JumpPause = 0.0f;
            }
            else
            {
              float num2 = 0.05f;
              this.GetReadyToJumpTime = (double) this.JumpPause >= (double) num2 * 2.0 ? num2 : this.JumpPause * 0.5f;
            }
            if (this.UsingJumpPeakLocNextJump)
            {
              this.UsingJumpPeakLocNextJump = false;
              this.UsingJumpPeakLoc = true;
              this.ForceJump = true;
              this.JumpPause = 0.0f;
            }
            else
              this.UsingJumpPeakLoc = false;
            if (this.JumpingToSpecialTaget)
            {
              this.UsingJumpPeakLoc = false;
              this.UsingJumpPeakLocNextJump = false;
              this.JumpingToSpecialTaget = false;
              this.JumpPause = 1f;
              this.PositionalOffset = Vector2.Zero;
            }
          }
        }
        float num3 = 1f;
        float num4 = 0.0f;
        if ((double) this.JumpLerper.CurrentValue < (1.0 - (double) num4) * (double) num3)
        {
          float num2 = (float) (1.0 - (double) this.JumpLerper.CurrentValue / ((1.0 - (double) num4) * (double) num3));
          this.JumpStretch.X = (float) (1.0 + (double) num2 * -(double) this.Ref_animationprofile.GetVerticalSTretchiness());
          this.JumpStretch.Y = (float) (1.0 + (double) num2 * (double) this.Ref_animationprofile.GetVerticalSTretchiness() * 0.5);
        }
      }
      if (((double) this.JumpPause > 0.0 || this.ForceJump) && !this.StopAutoJump)
      {
        if ((double) this.JumpPause <= (double) this.GetReadyToJumpTime)
        {
          int num2 = this.FatSquishStarted ? 1 : 0;
        }
        this.JumpPause -= DeltaTime;
        if ((double) this.JumpPause <= 0.0 || this.ForceJump)
        {
          this.JumpPause = 0.0f;
          this.ForceJump = false;
          this.StartJump();
        }
      }
      this.CheckSquash();
      this.UpdateSquash(DeltaTime);
      this.UpdateStretchynessReset();
      this.CurrentScale = this.JumpStretch + this.LandSquish;
      if (this.UsingJumpPeakLoc)
      {
        this.PositionalOffset = this.JumpLerper.CurrentValue * this.TargetJumpPeakLoc;
        if (this.JumpingToSpecialTaget && !this.GoingUpwards)
        {
          this.PositionalOffset = this.TargetJumpPeakLoc;
          this.PositionalOffset += (1f - this.JumpLerper.CurrentValue) * (this.OverrideTargetLocation - this.TargetJumpPeakLoc);
        }
        if (!this.Isbaby)
          return;
        this.CurrentScale *= 0.5f;
      }
      else if (this.Isbaby)
      {
        this.CurrentScale *= 0.5f;
        this.PositionalOffset.Y = this.JumpLerper.CurrentValue * (float) -((double) this.JumpHeight * 0.5);
      }
      else
        this.PositionalOffset.Y = this.JumpLerper.CurrentValue * -this.JumpHeight;
    }

    private void StartJump()
    {
      if (this.JumpingToSpecialTaget || this.GoingUpwards)
        return;
      this.IsJumping = true;
      this.FatSquishStarted = false;
      this.GoingUpwards = true;
      this.FullJumpTime = this.Ref_animationprofile.GetJumpTime();
      this.JumpLerper.SetLerp(SinCurveType.EaseOut, this.FullJumpTime * 0.5f, 0.0f, 1f, true);
      this.JumpHeight = this.Ref_animationprofile.GetJumpHeight();
    }

    private void CheckSquash()
    {
      float num = 0.2f;
      if (!this.GoingUpwards)
        num = 0.5f;
      if ((double) this.JumpLerper.CurrentValue < (double) num && !this.GoingUpwards && !this.FatSquishStarted)
      {
        this.FatSquishStarted = true;
        this.FatLandingLerper.SetLerp(SinCurveType.EaseInAndOut, (float) ((double) this.FullJumpTime * (double) num * 0.5), this.FatLandingLerper.CurrentValue, 1f, true);
      }
      if ((double) this.FatLandingLerper.CurrentValue <= 0.990000009536743 || (double) this.FatLandingLerper.TargetValue != 1.0 || !this.GoingUpwards && (double) this.JumpPause <= 0.0)
        return;
      this.FatLandingLerper.SetLerp(SinCurveType.EaseInAndOut, (float) ((double) this.FullJumpTime * (double) num * 0.5), this.FatLandingLerper.CurrentValue, 0.0f, true);
    }

    private void UpdateSquash(float DeltaTime)
    {
      this.LandSquish = Vector2.Zero;
      bool isComplete = this.FatLandingLerper.IsComplete;
      this.FatLandingLerper.UpdateSinLerper(DeltaTime);
      if ((double) this.FatLandingLerper.CurrentValue != 0.0)
      {
        float currentValue = this.FatLandingLerper.CurrentValue;
        this.LandSquish.X = this.Ref_animationprofile.GetSquichiness() * 0.5f * currentValue;
        this.LandSquish.Y = (float) ((double) this.Ref_animationprofile.GetSquichiness() * 0.5 * (double) currentValue * -1.0);
      }
      else
      {
        if (isComplete || (double) this.JumpPause >= 0.100000001490116)
          return;
        this.JumpPause = 0.0f;
        this.StartJump();
      }
    }

    private void UpdateStretchynessReset()
    {
      float num1 = 0.2f;
      if ((double) this.JumpLerper.CurrentValue >= (double) num1 || (double) this.JumpStretch.X == 1.0 && (double) this.JumpStretch.Y == 1.0)
        return;
      Vector2 jumpStretch = this.JumpStretch;
      float num2 = 1f - this.JumpLerper.CurrentValue / num1;
      this.JumpStretch.X += (1f - this.JumpStretch.X) * num2;
      this.JumpStretch.Y += (1f - this.JumpStretch.Y) * num2;
    }
  }
}
