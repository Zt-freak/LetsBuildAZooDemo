// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.Ships.Ship
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.GamePlay.beams;
using TinyZoo.GamePlay.ReclaimedZones;
using TinyZoo.GamePlay.VirtualJoystick;
using TinyZoo.PlayerDir;
using TinyZoo.Settings.Sound;

namespace TinyZoo.GamePlay.Ships
{
  internal class Ship
  {
    private ShipRenderer shiprenderer;
    private Vector2 Velocity;
    private int PlayerID;
    private BeamCenter currentbeam;
    private ArrowButtons arrowbuttons;
    private bool DraggingShip;
    public bool BeamFiring = true;
    private Vector2 StartLocation;
    private Vector2 TargetLocation;
    private LerpHandler_Float lerper;

    public Ship(int PlayerControllerShipIndex)
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 1f, 3f);
      this.PlayerID = PlayerControllerShipIndex;
      this.shiprenderer = new ShipRenderer();
      this.shiprenderer.vLocation = TileMath.GetPlayCenter();
      this.arrowbuttons = new ArrowButtons();
      this.StartLocation = this.shiprenderer.vLocation;
      this.TargetLocation = this.shiprenderer.vLocation;
    }

    public void UpdateDuringGameOver(float DeltaTime) => this.shiprenderer.UpdateShipRenderer(DeltaTime);

    public void UpdateShip(
      float DeltaTime,
      Player[] players,
      BeamManager beammanager,
      out bool AutoFireMobile,
      BoxZoneManager boxzonemanager)
    {
      bool FiredUp = false;
      bool FiredRight = false;
      this.BeamFiring = true;
      if (this.currentbeam == null || this.currentbeam.IsLockedInPlace || this.currentbeam.BeamWasHitByHuman)
        this.BeamFiring = false;
      Vector2 vLocation1 = this.shiprenderer.vLocation;
      BoxZone myZone = boxzonemanager.GetMyZone(ref vLocation1);
      bool HasUp = true;
      float num1 = myZone.TopLeft.X - this.shiprenderer.vLocation.X;
      float num2 = myZone.BottomRight.X - this.shiprenderer.vLocation.X;
      float num3 = myZone.TopLeft.Y - this.shiprenderer.vLocation.Y;
      float num4 = myZone.BottomRight.Y - this.shiprenderer.vLocation.Y;
      if ((double) Math.Abs(num1) < 3.0 && (double) myZone.TopLeft.X > (double) TileMath.GetPlaySpaceLeft() + 1.0)
        HasUp = false;
      if ((double) Math.Abs(num2) < 3.0 && (double) myZone.BottomRight.X < (double) TileMath.GetPlaySpaceRight() - 1.0)
        HasUp = false;
      bool HasLeftRight = true;
      if ((double) Math.Abs(num3) < 3.0 && (double) myZone.TopLeft.Y > (double) TileMath.GetPlaySpaceTop() + 1.0)
        HasLeftRight = false;
      if ((double) Math.Abs(num4) < 3.0 && (double) myZone.BottomRight.Y < (double) TileMath.GetPlaySpaceBottom() - 1.0)
        HasLeftRight = false;
      this.arrowbuttons.UpdateArrowButtons(DeltaTime, this.shiprenderer.vLocation, (double) this.lerper.Value != (double) this.lerper.TargetValue || !myZone.HasPerson, this.BeamFiring && !GameFlags.IsBreakOut, this.shiprenderer.IsHorizontal(), players[0], HasLeftRight, HasUp, out FiredUp, out FiredRight);
      this.UpdateShipMovement(players, DeltaTime, out AutoFireMobile);
      if (FiredUp && this.shiprenderer.IsHorizontal())
        players[this.PlayerID].inputmap.PressedThisFrame[2] = true;
      else if (FiredRight && !this.shiprenderer.IsHorizontal())
        players[this.PlayerID].inputmap.PressedThisFrame[2] = true;
      if (players[this.PlayerID].inputmap.PressedThisFrame[1] && (this.currentbeam == null || this.currentbeam.IsLockedInPlace || this.currentbeam.BeamWasHitByHuman) && GameFlags.CurrentBeamInventory > 0)
      {
        bool flag = false;
        if (!myZone.HasPerson)
          flag = true;
        if (!HasLeftRight && this.shiprenderer.IsHorizontal())
          flag = true;
        else if (!HasUp && !this.shiprenderer.IsHorizontal())
          flag = true;
        if (flag)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.MenuOpen, Pitch: 1f);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmUpgrade, Pitch: -1f);
          players[this.PlayerID].inputmap.PressedThisFrame[1] = false;
          this.shiprenderer.SetPrimaryColours(0.4f, new Vector3(0.1f, 0.1f, 1f));
        }
      }
      if (!this.shiprenderer.IsTransforming() | FiredUp | FiredRight)
      {
        if (players[this.PlayerID].inputmap.PressedThisFrame[2])
          this.shiprenderer.Transform();
        if (((players[this.PlayerID].inputmap.PressedThisFrame[2] ? 0 : (players[this.PlayerID].inputmap.PressedThisFrame[1] ? 1 : 0)) | (FiredUp ? 1 : 0) | (FiredRight ? 1 : 0)) != 0 && (this.currentbeam == null || this.currentbeam.IsLockedInPlace || (this.currentbeam.BeamWasHitByHuman || GameFlags.IsBreakOut)) && GameFlags.CurrentBeamInventory > 0)
        {
          Vector2 vLocation2 = this.shiprenderer.vLocation;
          if (FiredRight)
          {
            if ((double) num1 > -0.200000002980232)
              vLocation2.X += 0.2f;
            if ((double) num2 < 0.200000002980232)
              vLocation2.X -= 0.2f;
          }
          if (FiredUp)
          {
            if ((double) num3 > -0.200000002980232)
              vLocation2.Y += 0.2f;
            if ((double) num4 < 0.200000002980232)
              vLocation2.Y -= 0.2f;
          }
          this.currentbeam = !(FiredRight | FiredUp) ? beammanager.Firebeam(vLocation2, this.shiprenderer.IsHorizontal(), true) : beammanager.Firebeam(vLocation2, FiredRight, true);
          if ((!GameFlags.IsArcadeMode || !GameFlags.DifficultyIsEasy) && (GameFlags.IsArcadeMode || !GameFlags.BountyMode))
            --GameFlags.CurrentBeamInventory;
        }
      }
      this.shiprenderer.UpdateShipRenderer(DeltaTime);
    }

    private void UpdateShipMovement(Player[] players, float DeltaTime, out bool AutoFireMobile)
    {
      AutoFireMobile = false;
      if (InputMap.virtualstick != VirtualStickStatus.DigitalOnly && InputMap.virtualstick != VirtualStickStatus.AnalogueOnly)
      {
        if ((double) players[0].player.touchinput.ReleaseTapArray[0].X > 0.0 && GameFlags.AllowMouseControl && (!FeatureFlags.BlockDroneMovement && !VirtualStickManager.IsMoving))
        {
          this.StartLocation = this.shiprenderer.vLocation;
          this.lerper.SetLerp(true, 0.0f, 1f, 3f, true);
          this.TargetLocation = RenderMath.TranslateScreenSpaceToWorldSpace(players[0].player.touchinput.ReleaseTapArray[0]);
          if ((double) this.TargetLocation.X < (double) TileMath.GetPlaySpaceLeft())
            this.TargetLocation.X = TileMath.GetPlaySpaceLeft();
          if ((double) this.TargetLocation.Y < (double) TileMath.GetPlaySpaceTop())
            this.TargetLocation.Y = TileMath.GetPlaySpaceTop();
          if ((double) this.TargetLocation.X > (double) TileMath.GetPlaySpaceRight())
            this.TargetLocation.X = TileMath.GetPlaySpaceRight();
          if ((double) this.TargetLocation.Y > (double) TileMath.GetPlaySpaceBottom())
            this.TargetLocation.Y = TileMath.GetPlaySpaceBottom();
        }
        if (DebugFlags.UsingDragForShip)
        {
          if (!this.DraggingShip)
          {
            if ((double) players[0].player.touchinput.MultiTouchTapArray[0].X > 0.0)
            {
              float num = 15f;
              if (InputMap.virtualstick == VirtualStickStatus.Off)
                num = 25f;
              if (MathStuff.CheckPointCollision(true, RenderMath.TranslateWorldSpaceToScreenSpace(this.shiprenderer.vLocation), Math.Max(Sengine.WorldOriginandScale.Z, 2f), (float) this.shiprenderer.DrawRect.Width + num, ((float) this.shiprenderer.DrawRect.Height + num) * Sengine.ScreenRatioUpwardsMultiplier.Y, players[0].player.touchinput.MultiTouchTapArray[0]))
              {
                this.DraggingShip = true;
                this.TargetLocation = this.shiprenderer.vLocation;
              }
            }
          }
          else if ((double) players[0].player.touchinput.MultiTouchTouchLocations[0].X <= 0.0)
            this.DraggingShip = false;
          else if (this.DraggingShip && players[0].player.touchinput.DragActive)
            this.TargetLocation += players[0].player.touchinput.DragVectorThisFrame / Sengine.WorldOriginandScale.Z;
        }
      }
      if (this.TargetLocation != this.shiprenderer.vLocation && this.DraggingShip)
      {
        ShipRenderer shiprenderer = this.shiprenderer;
        shiprenderer.vLocation = shiprenderer.vLocation + (this.TargetLocation - this.shiprenderer.vLocation) * 0.25f;
      }
      if (this.TargetLocation != this.shiprenderer.vLocation && (double) this.lerper.Value != 1.0)
      {
        this.lerper.UpdateLerpHandler(DeltaTime);
        this.shiprenderer.vLocation = (this.TargetLocation - this.StartLocation) * this.lerper.Value + this.StartLocation;
        if ((double) this.lerper.Value == 1.0)
        {
          AutoFireMobile = true;
          ++FeatureFlags.ShipMoved;
          FeatureFlags.ShipMovedHere = this.shiprenderer.vLocation;
        }
      }
      else
      {
        if (FeatureFlags.BlockDroneMovement)
          return;
        if (GameFlags.Mobile_NoMomentum)
        {
          ShipRenderer shiprenderer = this.shiprenderer;
          shiprenderer.vLocation = shiprenderer.vLocation + players[this.PlayerID].inputmap.Movementstick * DeltaTime * 120f;
        }
        else
        {
          this.Velocity += players[this.PlayerID].inputmap.Movementstick * DeltaTime * 4f;
          this.Velocity.X = MathHelper.Clamp(this.Velocity.X, -1f, 1f);
          this.Velocity.Y = MathHelper.Clamp(this.Velocity.Y, -1f, 1f);
          if ((double) players[this.PlayerID].inputmap.Movementstick.X == 0.0)
            this.Velocity.X = this.DoDrag(this.Velocity.X, DeltaTime);
          if ((double) players[this.PlayerID].inputmap.Movementstick.Y == 0.0)
            this.Velocity.Y = this.DoDrag(this.Velocity.Y, DeltaTime);
          ShipRenderer shiprenderer = this.shiprenderer;
          shiprenderer.vLocation = shiprenderer.vLocation + this.Velocity * DeltaTime * 100f;
        }
        if ((double) this.shiprenderer.vLocation.X < (double) TileMath.GetPlaySpaceLeft())
        {
          this.shiprenderer.vLocation.X = TileMath.GetPlaySpaceLeft();
          if ((double) this.Velocity.X < 0.0)
            this.Velocity.X *= -1f;
        }
        if ((double) this.shiprenderer.vLocation.Y < (double) TileMath.GetPlaySpaceTop())
        {
          this.shiprenderer.vLocation.Y = TileMath.GetPlaySpaceTop();
          if ((double) this.Velocity.Y < 0.0)
            this.Velocity.Y *= -1f;
        }
        if ((double) this.shiprenderer.vLocation.X > (double) TileMath.GetPlaySpaceRight())
        {
          this.shiprenderer.vLocation.X = TileMath.GetPlaySpaceRight();
          if ((double) this.Velocity.X > 0.0)
            this.Velocity.X *= -1f;
        }
        if ((double) this.shiprenderer.vLocation.Y > (double) TileMath.GetPlaySpaceBottom())
        {
          this.shiprenderer.vLocation.Y = TileMath.GetPlaySpaceBottom();
          if ((double) this.Velocity.Y > 0.0)
            this.Velocity.Y *= -1f;
        }
        if (GameFlags.IsUsingController)
          FeatureFlags.ShipMovedHere = this.shiprenderer.vLocation;
      }
      players[0].livestats.IsDraggingShip = this.DraggingShip;
    }

    private float DoDrag(float VelocityVector, float DeltaTime)
    {
      if ((double) VelocityVector > 0.0)
      {
        VelocityVector -= DeltaTime * 3f;
        if ((double) VelocityVector < 0.0)
          VelocityVector = 0.0f;
      }
      else if ((double) VelocityVector < 0.0)
      {
        VelocityVector += DeltaTime * 3f;
        if ((double) VelocityVector > 0.0)
          VelocityVector = 0.0f;
      }
      return VelocityVector;
    }

    public void DrawShip()
    {
      this.arrowbuttons.DrawArrowButtons();
      this.shiprenderer.DrawShipRenderer();
    }
  }
}
