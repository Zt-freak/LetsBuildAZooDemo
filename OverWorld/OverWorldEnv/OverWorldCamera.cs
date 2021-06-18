// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.OverWorldCamera
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Camera;
using SEngine.Input;
using SEngine.Lerp;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.OverWorld.OverWorldEnv
{
  internal class OverWorldCamera
  {
    internal static Vector2 CurrentPos;
    private static float Zoom;
    private Vector3 PanStartLocation;
    private Vector3 PanTargetLocation;
    private bool IsPanning;
    private SinLerper sinlerp;
    internal static Vector3 CameraStartPos;
    private bool AllowClampDuringPan;
    internal static Vector3 CameraLocationOnEnteringWorldMap;
    private bool IsWorldMapView;
    private SinLerper OFFSETsinlerp;
    private Vector3 OffsetTarget;
    private Vector3 OffsetStart;
    private bool BlockDrag_DueToMouseOverAtStart;
    private WalkingPerson lookAtThisPerson;
    private Vector3 PreZoomLoc;
    private float TargetZoomDistanceWhenControllAvatar;

    internal static void SetReturnForCamera() => OverWorldCamera.CameraLocationOnEnteringWorldMap = Sengine.WorldOriginandScale;

    public OverWorldCamera(Player player, bool _IsWorldMapView = false, float TargetPixelSnap = 2f)
    {
      OverWorldCamera.Zoom = RenderMath.GetNearestPerfectPixelZoom(TargetPixelSnap);
      if (_IsWorldMapView)
      {
        this.IsWorldMapView = true;
        TileMath.SetOverWorldMapSize_XDefault(64);
        TileMath.SetOverWorldMapSize_YSize(64);
        OverWorldCamera.CameraLocationOnEnteringWorldMap = Sengine.WorldOriginandScale;
        OverWorldCamera.CurrentPos.Y = 512f;
        OverWorldCamera.Zoom = 1f;
      }
      OverWorldCamera.CurrentPos = TileMath.GetGateLocation();
      if (this.IsWorldMapView)
        OverWorldCamera.CurrentPos.Y = 512f * Sengine.ScreenRatioUpwardsMultiplier.Y;
      CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));
    }

    internal static void ResetCameraOnReturnFromMap()
    {
      OverWorldCamera.CurrentPos.X = OverWorldCamera.CameraLocationOnEnteringWorldMap.X;
      OverWorldCamera.CurrentPos.Y = OverWorldCamera.CameraLocationOnEnteringWorldMap.Y;
      OverWorldCamera.Zoom = OverWorldCamera.CameraLocationOnEnteringWorldMap.Z;
      OverWorldCamera.CameraStartPos = OverWorldCamera.CameraLocationOnEnteringWorldMap;
    }

    public void SetPhotoMode() => CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, 0.1f));

    public void DoPan(
      Vector3 _PanTargetLocation,
      float BlendTime,
      bool _AllowClampDuringPan,
      bool ForceReset = false,
      bool WillResetLerpSpeed = true)
    {
      this.AllowClampDuringPan = _AllowClampDuringPan;
      this.DoPan(_PanTargetLocation, BlendTime, new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom), ForceReset, WillResetLerpSpeed);
    }

    public void AddPermanentOffset(Vector3 _TargetOffset, float BlendTime)
    {
      if (this.OFFSETsinlerp == null)
        this.OFFSETsinlerp = new SinLerper();
      this.OffsetStart = this.OffsetTarget;
      this.OffsetTarget = _TargetOffset;
      this.OFFSETsinlerp.SetLerp(SinCurveType.EaseInAndOut, BlendTime, 0.0f, 1f, true);
    }

    public void DoSmoothedRepeatingPan(
      Vector3 _PanTargetLocation,
      float BlendTime,
      bool _AllowClampDuringPan)
    {
      this.AllowClampDuringPan = _AllowClampDuringPan;
      this.IsPanning = true;
      this.PanTargetLocation = _PanTargetLocation;
      this.PanStartLocation = new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom);
      this.sinlerp = new SinLerper();
      this.sinlerp.SetLerp(SinCurveType.Linear_WithEaseOutQuarter, BlendTime, 0.0f, 1f, true);
    }

    public void DoPan(Vector3 _PanTargetLocation, float BlendTime) => this.DoPan(_PanTargetLocation, BlendTime, new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));

    public void DoPan(
      Vector3 _PanTargetLocation,
      float BlendTime,
      Vector3 _StartLocation,
      bool ForceReset = false,
      bool WillResetLerpSpeed = true)
    {
      if (!(!this.IsPanning | ForceReset))
        return;
      this.IsPanning = true;
      this.PanTargetLocation = _PanTargetLocation;
      if (!WillResetLerpSpeed && this.sinlerp != null)
        return;
      this.PanStartLocation = _StartLocation;
      this.sinlerp = new SinLerper();
      this.sinlerp.SetLerp(SinCurveType.EaseInAndOut, BlendTime, 0.0f, 1f, true);
    }

    private static float GetHardCodedYLoc() => TileMath.GetGateLocation().Y;

    public void DoPanForIntro()
    {
      if (this.IsPanning)
        return;
      Vector2 gateLocation = TileMath.GetGateLocation();
      this.sinlerp = new SinLerper();
      this.IsPanning = true;
      float z = 1f;
      this.AllowClampDuringPan = true;
      this.PanTargetLocation = new Vector3(gateLocation.X, gateLocation.Y, z);
      this.sinlerp.SetLerp(SinCurveType.EaseInAndOut, 4f, 0.0f, 1f, true);
      this.PanStartLocation = Sengine.WorldOriginandScale;
      CameraManager.HardSetCameraPosition(this.PanStartLocation);
    }

    public void DoIntro()
    {
      OverWorldCamera.Zoom = 4f;
      Vector2 gateLocation = TileMath.GetGateLocation();
      OverWorldCamera.CurrentPos.X = gateLocation.X;
      OverWorldCamera.CurrentPos.Y = gateLocation.Y;
      float num = 512f;
      CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y + num * Sengine.ScreenRatioUpwardsMultiplier.Y, OverWorldCamera.Zoom));
      OverWorldCamera.CurrentPos.X = Sengine.WorldOriginandScale.X;
      OverWorldCamera.CurrentPos.Y = Sengine.WorldOriginandScale.Y;
    }

    public void UpdateWorldCamera(
      float DeltaTime,
      Player player,
      bool BlockMove,
      bool BlockMoveOnTouchButNotontroller,
      bool AllowZoom,
      AnimalsInPens peopleandbeams)
    {
      if (player.player.touchinput.DragStartedThisFrame)
        this.BlockDrag_DueToMouseOverAtStart = Z_GameFlags.MouseIsOverAPanel;
      else if (!player.player.touchinput.DragActive)
        this.BlockDrag_DueToMouseOverAtStart = false;
      if (OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar && !TrailerDemoFlags.FreeCam)
      {
        if (AvatarDisplay.DoLerp)
        {
          AvatarDisplay.DoLerp = false;
          this.DoPan(new Vector3(CustomerManager.ZooKeeperAvatar.vLocation, 3f), 1f);
        }
        else if (!this.IsPanning)
        {
          if ((double) this.TargetZoomDistanceWhenControllAvatar < 3.0)
          {
            this.TargetZoomDistanceWhenControllAvatar += DeltaTime;
            if ((double) this.TargetZoomDistanceWhenControllAvatar > 3.0)
              this.TargetZoomDistanceWhenControllAvatar = 3f;
          }
          this.DoPan(new Vector3(CustomerManager.ZooKeeperAvatar.vLocation, this.TargetZoomDistanceWhenControllAvatar), 0.0f);
        }
      }
      else
        this.TargetZoomDistanceWhenControllAvatar = Sengine.WorldOriginandScale.Z;
      if (this.OFFSETsinlerp != null)
        this.OFFSETsinlerp.UpdateSinLerper(DeltaTime);
      if (Z_GameFlags.ForceToNewScreen == ForceToNewScreen.LookAtAnimal)
      {
        Vector2 vector2 = Vector2.Zero;
        if (this.lookAtThisPerson != null)
          vector2 = this.lookAtThisPerson.animalrenderer != null ? this.lookAtThisPerson.animalrenderer.enemyrenderere.vLocation : this.lookAtThisPerson.vLocation;
        this.DoPan(new Vector3(vector2, 3f), 1f);
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
        {
          player.player.touchinput.ReleaseTapArray[0].X = -10000f;
          this.StopLookingAtThis();
        }
      }
      else
        this.PreZoomLoc = Sengine.WorldOriginandScale;
      if (TinyZoo.Game1.gamestate != GAMESTATE.WorldMap && (Z_GameFlags.ForceRightMouseDrag || OverWorldManager.overworldstate == OverWOrldState.MainMenu || (Z_GameFlags.ForceRightAndLeftMouseDrag || GameFlags.PhotoMode)))
      {
        int num = Z_GameFlags.MouseIsOverAPanel ? 1 : 0;
        if (player.inputmap.rightmousedrag.RMouseDragActive)
        {
          player.player.touchinput.DragVectorThisFrame = player.inputmap.rightmousedrag.RMouseDrag;
          player.player.touchinput.DragActive = player.inputmap.rightmousedrag.RMouseDragActive;
        }
        else if (Z_GameFlags.ForceRightMouseDrag)
        {
          player.player.touchinput.DragVectorThisFrame = Vector2.Zero;
          player.player.touchinput.DragActive = false;
        }
        if (player.inputmap.KeyboardCameraMovement != Vector2.Zero)
        {
          player.player.touchinput.DragActive = true;
          player.player.touchinput.DragVectorThisFrame = player.inputmap.KeyboardCameraMovement * DeltaTime * 300f;
        }
        Z_GameFlags.ForceRightAndLeftMouseDrag = false;
        Z_GameFlags.ForceRightMouseDrag = false;
      }
      else if (player.inputmap.KeyboardCameraMovement != Vector2.Zero)
      {
        player.player.touchinput.DragActive = true;
        player.player.touchinput.DragVectorThisFrame = player.inputmap.KeyboardCameraMovement * DeltaTime * 300f;
      }
      if (GameFlags.BlockOverWorldCamera)
        return;
      if (this.IsPanning)
      {
        this.sinlerp.UpdateSinLerper(DeltaTime);
        if (this.sinlerp.IsComplete)
        {
          if (OverWorldManager.IsGameIntro)
            OverWorldManager.IsGameIntro = false;
          this.IsPanning = false;
          OverWorldCamera.CurrentPos.X = this.PanTargetLocation.X;
          OverWorldCamera.CurrentPos.Y = this.PanTargetLocation.Y;
          if (this.AllowClampDuringPan)
            this.CheckClamp();
          CameraManager.HardSetCameraPosition(this.PanTargetLocation);
          OverWorldCamera.Zoom = this.PanTargetLocation.Z;
          if (FeatureFlags.DoingMainCameraCutSceneIntro)
            FeatureFlags.EndIntroCameraCitScene();
          if (Z_GameFlags.ForceControllerMode)
            OverWorldManager.overworldstate = OverWOrldState.PlayingAsAvatar;
        }
        else
        {
          Vector3 vector3 = (this.PanTargetLocation - this.PanStartLocation) * this.sinlerp.CurrentValue + this.PanStartLocation;
          OverWorldCamera.CurrentPos.X = vector3.X;
          OverWorldCamera.CurrentPos.Y = vector3.Y;
          OverWorldCamera.Zoom = vector3.Z;
          if (this.AllowClampDuringPan)
            this.CheckClamp();
          CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));
          return;
        }
      }
      else if (DebugFlags.LockCameraForVideo)
      {
        int num1 = FeatureFlags.BlockAllUI ? 1 : 0;
      }
      if (BlockMoveOnTouchButNotontroller && !GameFlags.IsUsingController)
        BlockMove = true;
      if (!BlockMove | AllowZoom || TrailerDemoFlags.FreeCam)
      {
        if (player.inputmap.PressedThisFrame[8])
          this.CenterCam();
        if (!Z_GameFlags.MouseIsOverAPanel_SoBlockZoom)
        {
          float num2 = PinchInput.UpdatePinch(player.player.touchinput.MultiTouchTouchLocations, out Vector2 _);
          OverWorldCamera.Zoom += (float) ((double) num2 / 1024.0 * ((double) Sengine.WorldOriginandScale.Z * 2.0));
          if (OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar)
          {
            if (TrailerDemoFlags.FreeCam)
            {
              OverWorldCamera.Zoom += (float) ((double) player.inputmap.ZoomValue * (double) DeltaTime * 1.20000004768372) * Sengine.WorldOriginandScale.Z;
              OverWorldCamera.Zoom += player.inputmap.momentumwheel.MovementThisFrame * 0.1f * DeltaTime * Sengine.WorldOriginandScale.Z;
            }
            if (TrailerDemoFlags.DoNotDetachCameraOnR3)
            {
              OverWorldCamera.CurrentPos.X = CustomerManager.ZooKeeperAvatar.vLocation.X;
              OverWorldCamera.CurrentPos.Y = CustomerManager.ZooKeeperAvatar.vLocation.Y;
            }
          }
          else
          {
            OverWorldCamera.Zoom += (float) ((double) player.inputmap.ZoomValue * (double) DeltaTime * 1.20000004768372) * Sengine.WorldOriginandScale.Z;
            OverWorldCamera.Zoom += player.inputmap.momentumwheel.MovementThisFrame * 0.1f * DeltaTime * Sengine.WorldOriginandScale.Z;
          }
        }
        float min = RenderMath.GetNearestPerfectPixelZoom(1f) * 0.333f;
        if (GameFlags.PhotoMode)
        {
          TinyZoo.Game1.ClsCLR.SetAllColours(ColourData.FernVeryDarkBlue);
          float num2 = 0.25f * Sengine.ScreenRationReductionMultiplier.Y;
          min = Math.Min((float) (1024.0 / ((double) TileMath.GetOverWorldMapSize_XDefault() * 16.0)), (float) (768.0 / ((double) TileMath.GetOverWorldMapSize_YSize() * 16.0)) * Sengine.ScreenRationReductionMultiplier.Y);
        }
        if (this.IsWorldMapView)
          min = 1f;
        OverWorldCamera.Zoom = MathHelper.Clamp(OverWorldCamera.Zoom, min, 6f);
        float num3 = 140f;
        if (!BlockMove || TrailerDemoFlags.FreeCam)
        {
          if (GameFlags.IsUsingController)
            num3 = 100f;
          if (player.inputmap.HeldButtons[25])
            num3 = 300f;
          if (OverWorldManager.overworldstate == OverWOrldState.Build)
          {
            int gamestate = (int) TinyZoo.Game1.gamestate;
          }
          if (TrailerDemoFlags.FreeCam)
          {
            int num2 = player.inputmap.RightStick != Vector2.Zero ? 1 : 0;
            player.inputmap.Movementstick = player.inputmap.RightStick;
          }
          if (player.inputmap.Movementstick != Vector2.Zero && !GameFlags.TempDisableCamer)
          {
            if (GameFlags.IsUsingController)
            {
              if (OverWorldManager.overworldstate == OverWOrldState.Build && TinyZoo.Game1.gamestate == GAMESTATE.OverWorld)
              {
                OverWorldCamera.CurrentPos.X += player.inputmap.CameraStick.X * DeltaTime * num3;
                OverWorldCamera.CurrentPos.Y -= player.inputmap.CameraStick.Y * DeltaTime * num3;
              }
              else
              {
                OverWorldCamera.CurrentPos.X += player.inputmap.CameraStick.X * DeltaTime * num3;
                OverWorldCamera.CurrentPos.Y -= player.inputmap.CameraStick.Y * DeltaTime * num3;
              }
            }
            else
            {
              OverWorldCamera.CurrentPos.X += player.inputmap.Movementstick.X * DeltaTime * num3 / Sengine.WorldOriginandScale.Z;
              OverWorldCamera.CurrentPos.Y += player.inputmap.Movementstick.Y * DeltaTime * num3 / Sengine.WorldOriginandScale.Z;
            }
          }
          else if (player.player.touchinput.DragActive && (double) player.player.touchinput.MultiTouchTouchLocations[1].X < 0.0 && !this.BlockDrag_DueToMouseOverAtStart)
          {
            OverWorldCamera.CurrentPos.X -= player.player.touchinput.DragVectorThisFrame.X / Sengine.WorldOriginandScale.Z;
            OverWorldCamera.CurrentPos.Y -= player.player.touchinput.DragVectorThisFrame.Y / Sengine.WorldOriginandScale.Z;
          }
        }
        GameFlags.TempDisableCamer = false;
      }
      if (AllowZoom || OverWorldManager.overworldstate != OverWOrldState.GraveYard)
        this.CheckClamp();
      else
        this.CheckClamp(false);
      if (Z_DebugFlags.LockZoomToOne && PC_KeyState.C_PressedThisFrame)
      {
        OverWorldCamera.Zoom = RenderMath.GetNearestPerfectPixelZoom(0.0f, true);
        CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));
      }
      CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));
      if (this.OFFSETsinlerp == null)
        return;
      CameraManager.HardSetCameraPosition(Sengine.WorldOriginandScale + (this.OffsetStart + (this.OffsetTarget - this.OffsetStart) * this.OFFSETsinlerp.CurrentValue));
    }

    public void CenterCam()
    {
      OverWorldCamera.CurrentPos = new Vector2((float) (TileMath.GetOverWorldMapSize_XDefault() * 8), (float) TileMath.GetOverWorldMapSize_XDefault() * 8f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      CameraManager.HardSetCameraPosition(new Vector3(OverWorldCamera.CurrentPos.X, OverWorldCamera.CurrentPos.Y, OverWorldCamera.Zoom));
    }

    private void CheckClamp(bool IncludeZoom = true)
    {
      if (IncludeZoom)
      {
        float num1 = (float) (TileMath.GetOverWorldMapSize_XDefault() * 16 - 8);
        float num2 = (float) ((TileMath.GetOverWorldMapSize_YSize() - 1) * 16) * Sengine.ScreenRatioUpwardsMultiplier.Y + 8f - 384f / OverWorldCamera.Zoom;
        float num3 = num1 - 512f / OverWorldCamera.Zoom;
        bool flag = false;
        if (GameFlags.PhotoMode)
        {
          double num4 = (double) Math.Min((float) (1024.0 / ((double) TileMath.GetOverWorldMapSize_XDefault() * 16.0)), (float) (768.0 / ((double) TileMath.GetOverWorldMapSize_YSize() * 16.0)));
          if ((double) OverWorldCamera.Zoom < 1024.0 / ((double) TileMath.GetOverWorldMapSize_XDefault() * 16.0))
          {
            OverWorldCamera.CurrentPos.X = (float) (((double) num3 - 504.0 / (double) OverWorldCamera.Zoom) * 0.5);
            flag = true;
            OverWorldCamera.CurrentPos.X += 504f / OverWorldCamera.Zoom;
          }
        }
        float num5 = 384f;
        float num6 = 0.0f;
        if (OverWorldManager.overworldstate == OverWOrldState.Build)
          num6 = 300f / OverWorldCamera.Zoom;
        if (!flag)
        {
          if ((double) OverWorldCamera.CurrentPos.X < 512.0 / (double) OverWorldCamera.Zoom - 8.0)
            OverWorldCamera.CurrentPos.X = (float) (512.0 / (double) OverWorldCamera.Zoom - 8.0);
          if ((double) OverWorldCamera.CurrentPos.X > (double) num3)
            OverWorldCamera.CurrentPos.X = num3;
        }
        if ((double) OverWorldCamera.CurrentPos.Y < (double) num5 / (double) OverWorldCamera.Zoom - 8.0)
          OverWorldCamera.CurrentPos.Y = (float) ((double) num5 / (double) OverWorldCamera.Zoom - 8.0);
        if ((double) OverWorldCamera.CurrentPos.Y <= (double) num2 + (double) num6)
          return;
        OverWorldCamera.CurrentPos.Y = num2 + num6;
      }
      else
      {
        OverWorldCamera.CurrentPos.X = MathHelper.Clamp(OverWorldCamera.CurrentPos.X, 0.0f, (float) (TileMath.GetOverWorldMapSize_XDefault() * 16));
        OverWorldCamera.CurrentPos.Y = MathHelper.Clamp(OverWorldCamera.CurrentPos.Y, 0.0f, (float) TileMath.GetOverWorldMapSize_XDefault() * 16f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
    }

    public void LookAtThis(WalkingPerson person)
    {
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.LookAtAnimal;
      this.lookAtThisPerson = person;
    }

    public void LookAtThis(PrisonerInfo animal)
    {
      this.lookAtThisPerson = CustomerManager.GetAnimalByUID(animal.intakeperson.UID);
      if (this.lookAtThisPerson == null)
        return;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.LookAtAnimal;
    }

    public void StopLookingAtThis()
    {
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.LookAtAnimal)
        return;
      DebugFlags.HideAllUI_DEBUG = false;
      Z_GameFlags.ForceToNewScreen = ForceToNewScreen.None;
      this.DoPan(this.PreZoomLoc, 1f, Sengine.WorldOriginandScale, true);
      FeatureFlags.BlockAllUI = false;
      this.lookAtThisPerson = (WalkingPerson) null;
    }
  }
}
