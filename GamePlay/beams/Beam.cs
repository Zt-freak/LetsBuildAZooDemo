// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.beams.Beam
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;

namespace TinyZoo.GamePlay.beams
{
  internal class Beam : GameObject
  {
    public Vector2 VSCale;
    private bool IsLeftBeam;
    private bool Horizontal;
    public GameObject EndCollision;
    private float speed;
    private bool InstantBeam;
    private Vector2 VScaleMultiplier;
    private Vector2 VScaleMultiplierFat;
    private Vector2 VScaleMultiplierveryFat;
    private Vector2 ExtraPos;
    private Vector2 Randomizer;
    private Vector2 VScaleMultiplierMini;
    private float ExtraForCOllision = 6f;

    public Beam(bool _IsLeftBeam)
    {
      this.IsLeftBeam = _IsLeftBeam;
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.SetAllColours(1f, 0.0f, 0.0f);
      this.EndCollision = new GameObject();
      this.EndCollision.DrawRect = new Rectangle(107, 0, 7, 8);
      this.EndCollision.SetDrawOriginToCentre();
      this.EndCollision.bActive = false;
      this.EndCollision.scale = 0.5f;
    }

    public void Start(Vector2 Location, bool IsHorizontal, float _Speed, bool _IsInstant)
    {
      this.InstantBeam = _IsInstant;
      this.speed = _Speed;
      this.EndCollision.bActive = false;
      this.vLocation = Location;
      this.Horizontal = IsHorizontal;
      this.VSCale = new Vector2(0.5f, 0.5f);
      this.ExtraPos = Vector2.Zero;
      this.Randomizer = Vector2.Zero;
      if (this.IsLeftBeam)
      {
        if (IsHorizontal)
        {
          this.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
          --this.ExtraPos.X;
        }
        else
        {
          this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
          --this.ExtraPos.Y;
        }
      }
      else if (IsHorizontal)
      {
        this.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
        ++this.ExtraPos.X;
      }
      else
      {
        this.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
        ++this.ExtraPos.Y;
      }
      if (IsHorizontal)
      {
        this.VScaleMultiplier = new Vector2(1f, 2f);
        this.VScaleMultiplierFat = new Vector2(1f, 3f);
        this.VScaleMultiplierveryFat = new Vector2(1f, 5f);
        this.VScaleMultiplierMini = new Vector2(1f, 0.5f);
      }
      else
      {
        this.VScaleMultiplier = new Vector2(2f, 1f);
        this.VScaleMultiplierFat = new Vector2(3f, 1f);
        this.VScaleMultiplierveryFat = new Vector2(5f, 1f);
        this.VScaleMultiplierMini = new Vector2(0.5f, 1f);
      }
    }

    public void UpdateBeam(float DeltaTime, List<BeamCenter> beams, BeamCenter parentbeamcenter)
    {
      if (parentbeamcenter.BeamWasHitByHuman)
        return;
      if (this.Horizontal)
      {
        this.Randomizer.Y = MathStuff.getRandomFloat(-0.5f, 0.5f);
        if (this.EndCollision.bActive)
          return;
        this.VSCale.X += DeltaTime * this.speed;
        if (this.InstantBeam)
          this.VSCale.X += 1000f;
        if (this.IsLeftBeam)
        {
          if ((double) this.vLocation.X - (double) this.VSCale.X <= (double) TileMath.GetPlaySpaceLeft() - (double) this.ExtraForCOllision)
          {
            this.EndCollision.bActive = true;
            this.EndCollision.vLocation = this.vLocation;
            this.EndCollision.vLocation.X = TileMath.GetPlaySpaceLeft() - this.ExtraForCOllision;
            this.VSCale.X = this.vLocation.X - (TileMath.GetPlaySpaceLeft() - this.ExtraForCOllision);
          }
        }
        else if ((double) this.vLocation.X + (double) this.VSCale.X >= (double) TileMath.GetPlaySpaceRight() + (double) this.ExtraForCOllision)
        {
          this.EndCollision.bActive = true;
          this.EndCollision.vLocation = this.vLocation;
          this.EndCollision.vLocation.X = TileMath.GetPlaySpaceRight() + this.ExtraForCOllision;
          this.VSCale.X = TileMath.GetPlaySpaceRight() + this.ExtraForCOllision - this.vLocation.X;
        }
        this.CheckBeamsForBlock(beams, parentbeamcenter);
      }
      else
      {
        float num = this.ExtraForCOllision * Sengine.ScreenRationReductionMultiplier.Y;
        if (!this.EndCollision.bActive)
        {
          this.VSCale.Y += DeltaTime * this.speed;
          if (this.InstantBeam)
            this.VSCale.Y += 1000f;
          if (this.IsLeftBeam)
          {
            if ((double) this.vLocation.Y - (double) this.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y <= (double) TileMath.GetPlaySpaceTop() - (double) num)
            {
              this.EndCollision.bActive = true;
              this.EndCollision.vLocation = this.vLocation;
              this.EndCollision.vLocation.Y = TileMath.GetPlaySpaceTop() - num;
              this.VSCale.Y = this.vLocation.Y - (TileMath.GetPlaySpaceTop() - num);
              this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
            }
          }
          else if ((double) this.vLocation.Y + (double) this.VSCale.Y * (double) Sengine.ScreenRatioUpwardsMultiplier.Y >= (double) TileMath.GetPlaySpaceBottom() + (double) num)
          {
            this.EndCollision.bActive = true;
            this.EndCollision.vLocation = this.vLocation;
            this.EndCollision.vLocation.Y = TileMath.GetPlaySpaceBottom() + num;
            this.VSCale.Y = TileMath.GetPlaySpaceBottom() + num - this.vLocation.Y;
            this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
          }
          this.CheckBeamsForBlock(beams, parentbeamcenter);
        }
        this.Randomizer.X = MathStuff.getRandomFloat(-0.5f, 0.5f);
      }
    }

    private void CheckBeamsForBlock(List<BeamCenter> beams, BeamCenter ParentBeamCenter)
    {
      for (int index = 0; index < beams.Count; ++index)
      {
        if (beams[index].ThisID != ParentBeamCenter.ThisID && beams[index].IsHorizontal != ParentBeamCenter.IsHorizontal)
        {
          if (ParentBeamCenter.IsHorizontal)
          {
            if (this.IsLeftBeam)
            {
              Vector2 CollslionPoint;
              if (beams[index].CheckCollision(out CollslionPoint, this.vLocation, this.vLocation - new Vector2(this.VSCale.X, 0.0f)))
              {
                bool flag = true;
                if (this.EndCollision.bActive)
                  flag = (double) this.EndCollision.vLocation.X < (double) CollslionPoint.X;
                if (flag)
                {
                  this.EndCollision.bActive = true;
                  this.EndCollision.vLocation = CollslionPoint;
                  this.VSCale.X = this.vLocation.X - CollslionPoint.X;
                }
              }
            }
            else
            {
              Vector2 CollslionPoint;
              if (beams[index].CheckCollision(out CollslionPoint, this.vLocation, this.vLocation + new Vector2(this.VSCale.X, 0.0f)))
              {
                bool flag = true;
                if (this.EndCollision.bActive)
                  flag = (double) this.EndCollision.vLocation.X > (double) CollslionPoint.X;
                if (flag)
                {
                  this.EndCollision.bActive = true;
                  this.EndCollision.vLocation = CollslionPoint;
                  this.VSCale.X = CollslionPoint.X - this.vLocation.X;
                }
              }
            }
          }
          else if (!ParentBeamCenter.IsHorizontal)
          {
            if (this.IsLeftBeam)
            {
              Vector2 CollslionPoint;
              if (beams[index].CheckCollision(out CollslionPoint, this.vLocation, this.vLocation - new Vector2(0.0f, this.VSCale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y)))
              {
                bool flag = true;
                if (this.EndCollision.bActive)
                  flag = (double) this.EndCollision.vLocation.Y < (double) CollslionPoint.Y;
                if (flag)
                {
                  this.EndCollision.bActive = true;
                  this.EndCollision.vLocation = CollslionPoint;
                  this.VSCale.Y = this.vLocation.Y - CollslionPoint.Y;
                  this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
                }
              }
            }
            else
            {
              Vector2 CollslionPoint;
              if (beams[index].CheckCollision(out CollslionPoint, this.vLocation, this.vLocation + new Vector2(0.0f, this.VSCale.Y * Sengine.ScreenRatioUpwardsMultiplier.Y)))
              {
                bool flag = true;
                if (this.EndCollision.bActive)
                  flag = (double) this.EndCollision.vLocation.Y > (double) CollslionPoint.Y;
                if (flag)
                {
                  this.EndCollision.bActive = true;
                  this.EndCollision.vLocation = CollslionPoint;
                  this.VSCale.Y = CollslionPoint.Y - this.vLocation.Y;
                  this.VSCale.Y *= Sengine.ScreenRationReductionMultiplier.Y;
                }
              }
            }
          }
        }
      }
    }

    public void DrawBeam()
    {
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.vLocation + this.ExtraPos + this.Randomizer, this.VSCale * this.VScaleMultiplierveryFat * this.VScaleMultiplier, 0.0f, true, this.DrawRect, 0.3f, Color.Red);
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.vLocation + this.ExtraPos + this.Randomizer, this.VSCale * this.VScaleMultiplierFat * this.VScaleMultiplier, 0.0f, true, this.DrawRect, 0.3f, Color.Red);
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.vLocation + this.ExtraPos + this.Randomizer, this.VSCale * this.VScaleMultiplier, 0.0f, true, this.DrawRect, 0.5f, Color.Red);
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.vLocation + this.Randomizer, this.VSCale, 0.0f, true, this.DrawRect, 0.6f, Color.White);
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.vLocation + this.Randomizer, this.VSCale * this.VScaleMultiplierMini, 0.0f, true, this.DrawRect, 0.4f, Color.White);
      if (!this.EndCollision.bActive)
        return;
      this.EndCollision.fAlpha = 0.5f;
      this.EndCollision.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.EndCollision.vLocation + this.Randomizer, this.EndCollision.scale, 0.0f);
      this.EndCollision.WorldOffsetDraw(AssetContainer.PointBlendBatch02, AssetContainer.SpriteSheet, this.EndCollision.vLocation + this.Randomizer * -2f, this.EndCollision.scale, 0.0f);
    }
  }
}
