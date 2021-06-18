// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.WorldCamera
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Camera;
using System;

namespace TinyZoo.GamePlay
{
  internal class WorldCamera
  {
    private Vector2 WorldCenter;
    private bool HasCameraLeftRghtMove;
    private bool HasCameraUpDowntMove;
    private float LateralLimit;
    private float VeryicalLimit;
    private Vector2 LastCamLocation;
    private float TGTZoom;
    private bool UseZoomOut;

    public WorldCamera()
    {
      this.UseZoomOut = false;
      CameraManager.SetZoomOriginToScreenPercent(new Vector2(0.5f, 0.5f));
      CameraManager.HardSetCameraPosition(new Vector3(512f / GameFlags.PixelZoom, (float) (768.0 / (double) GameFlags.PixelZoom / 2.0), GameFlags.PixelZoom));
      this.WorldCenter = TileMath.GetPlayCenter();
      float num1 = TileMath.GetPlaySpaceRight() - TileMath.GetPlaySpaceLeft();
      if ((double) num1 > 228.0)
      {
        this.HasCameraLeftRghtMove = true;
        this.LateralLimit = (float) (((double) num1 - 228.0) * 0.5);
      }
      float num2 = TileMath.GetPlaySpaceBottom() - TileMath.GetPlaySpaceTop() + 32f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float num3 = 192f;
      if ((double) num2 > (double) num3)
      {
        this.HasCameraUpDowntMove = true;
        this.VeryicalLimit = (float) (((double) num2 - (double) num3) * 0.5);
      }
      this.LastCamLocation = this.WorldCenter;
      this.TGTZoom = GameFlags.PixelZoom;
      float val2 = GameFlags.PixelZoom;
      if (!this.HasCameraLeftRghtMove && !this.HasCameraUpDowntMove)
        return;
      float num4 = 0.0f;
      if (this.HasCameraLeftRghtMove)
      {
        float num5 = 228f / num1;
        this.TGTZoom = GameFlags.PixelZoom * num5;
      }
      if (this.HasCameraUpDowntMove)
      {
        float num5 = 192f / num2;
        val2 = GameFlags.PixelZoom * num5;
      }
      if ((double) val2 < (double) this.TGTZoom)
      {
        this.WorldCenter.Y -= num4;
        this.LastCamLocation = this.WorldCenter;
      }
      this.TGTZoom = Math.Min(this.TGTZoom, val2);
      CameraManager.HardSetCameraPosition(new Vector3(512f / GameFlags.PixelZoom, (float) (768.0 / (double) GameFlags.PixelZoom / 2.0), this.TGTZoom));
    }

    public void UpateDrag(Vector2 DragThisFrame)
    {
      if (!this.UseZoomOut)
        return;
      DragThisFrame.X /= Sengine.WorldOriginandScale.Z;
      DragThisFrame.Y /= Sengine.WorldOriginandScale.Z;
      DragThisFrame.Y *= Sengine.ScreenRatioUpwardsMultiplier.Y;
      if (this.HasCameraLeftRghtMove)
      {
        this.LastCamLocation.X -= DragThisFrame.X;
        this.LastCamLocation.X = MathHelper.Clamp(this.LastCamLocation.X, this.WorldCenter.X - this.LateralLimit, this.WorldCenter.X + this.LateralLimit);
      }
      if (!this.HasCameraUpDowntMove)
        return;
      this.LastCamLocation.Y -= DragThisFrame.Y;
    }

    public void UpdateWorldCamera(float DeltaTime)
    {
      CameraManager.SetZoomOriginToScreenPercent(new Vector2(0.5f, 0.5f));
      CameraManager.HardSetCameraPosition(new Vector3(this.LastCamLocation.X, this.LastCamLocation.Y, this.TGTZoom));
    }

    public void DrawWorldCamera()
    {
    }
  }
}
