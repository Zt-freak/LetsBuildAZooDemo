// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.InputMap
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Input.Switch;
using System;
using System.Collections.Generic;
using TinyZoo.Settings.Sound;
using TinyZoo.Z_BuldMenu.DragBuilder;

namespace TinyZoo.PlayerDir
{
  internal class InputMap
  {
    public bool DebugSpeedUpTapped;
    public bool DebugPauseTapped;
    public bool SlowDownPressed;
    public bool AutoScrollWorld;
    public bool AddInventoryTapped;
    public bool DebugSkipGameTapped;
    public bool FireWeapon;
    public bool DebugRotate;
    public bool DebugToggleTrackerHasBeenClaimed;
    internal static VirtualStickStatus virtualstick = VirtualStickStatus.Off;
    public bool BackTapped;
    public Vector2 Movementstick;
    public Vector2 GatePlacementStick;
    public Vector2 CharacterMovementStick;
    public Vector2 RightStick;
    public Vector2 CameraStick;
    public bool[] PressedThisFrame;
    public bool[] HeldButtons;
    public bool[] ReleasedThisFrame;
    public float ZoomValue;
    private float RTriggerAnalogueFilterredLastFrame;
    private float LTriggerAnalogueFilterredLastFrame;
    public RightMouseDrag rightmousedrag;
    private Vector2 MousePosition;
    private float RightMousDownTime;
    public bool RightMousReleaseClick;
    public bool LeftMouseHeld;
    public MomentumMouseWheel momentumwheel;
    public Vector2 KeyboardCameraMovement;
    public Vector2 PointerLocation;

    public InputMap()
    {
      this.momentumwheel = new MomentumMouseWheel();
      this.rightmousedrag = new RightMouseDrag();
      this.MousePosition = MouseStatus.MousePosition;
      SwitchControllerConfig.InitializeSwitchConsole(SwitchConfig.ProController | SwitchConfig.Handheld, SwitchOrient.JoyConHorizontal, HandheldActivationType.Dual);
    }

    public void UpdateInputMap(
      GenericInput genericinput,
      SEngine.Player.Player player,
      float DeltaTime,
      UserKeyBindings keyBindings)
    {
      this.CheckSwitchControllerIndexSwap(player);
      PC_KeyState.SetUpKeyboardState();
      this.PressedThisFrame = new bool[36];
      this.ReleasedThisFrame = new bool[36];
      if (!Z_DebugFlags.IsBetaVersion)
      {
        this.PressedThisFrame[2] = genericinput.Controller_Xbox_X_PressedThisFrame;
        this.PressedThisFrame[1] = genericinput.Controller_Xbox_A_PressedThisFrame;
        this.PressedThisFrame[0] = genericinput.Controller_Xbox_A_PressedThisFrame;
        this.PressedThisFrame[6] = genericinput.Controller_DPadRightPressedThisFrame;
        this.PressedThisFrame[3] = genericinput.Controller_DPadDownPressedThisFrame;
        this.PressedThisFrame[4] = genericinput.Controller_DPadUpPressedThisFrame;
        this.PressedThisFrame[5] = genericinput.Controller_DPadLeftPressedThisFrame;
        this.PressedThisFrame[7] = genericinput.Controller_Xbox_B_PressedThisFrame;
        this.PressedThisFrame[8] = genericinput.Controller_L3_PressedThisFrame;
        this.PressedThisFrame[9] = genericinput.Controller_L1_PressedThisFrame;
        this.PressedThisFrame[10] = genericinput.Controller_R1_PressedThisFrame;
        this.PressedThisFrame[11] = genericinput.Controller_Xbox_A_PressedThisFrame || genericinput.Controller_Xbox_B_PressedThisFrame;
        this.PressedThisFrame[15] = genericinput.Controller_Xbox_X_PressedThisFrame;
        this.PressedThisFrame[12] = genericinput.Controller_Xbox_Y_PressedThisFrame;
        this.PressedThisFrame[14] = genericinput.Controller_Xbox_A_PressedThisFrame;
        this.PressedThisFrame[13] = genericinput.Controller_Xbox_B_PressedThisFrame;
        this.PressedThisFrame[19] = genericinput.Controller_DPadRightPressedThisFrame || (double) genericinput.LStick_Filtered.X > 0.0;
        this.PressedThisFrame[16] = genericinput.Controller_DPadDownPressedThisFrame || (double) genericinput.LStick_Filtered.Y < 0.0;
        this.PressedThisFrame[17] = genericinput.Controller_DPadUpPressedThisFrame || (double) genericinput.LStick_Filtered.Y > 0.0;
        this.PressedThisFrame[18] = genericinput.Controller_DPadLeftPressedThisFrame || (double) genericinput.LStick_Filtered.X < 0.0;
        this.PressedThisFrame[20] = genericinput.Controller_StartButtonPressedThisFrame;
        this.PressedThisFrame[21] = (double) genericinput.RTriggerAnalogueFilterred > 0.0 && (double) this.RTriggerAnalogueFilterredLastFrame == 0.0;
        this.PressedThisFrame[25] = (double) genericinput.LTriggerAnalogueFilterred > 0.0 && (double) this.LTriggerAnalogueFilterredLastFrame == 0.0;
        this.PressedThisFrame[24] = genericinput.Controller_R3_PressedThisFrame;
        this.PressedThisFrame[26] = genericinput.Controller_Xbox_Y_PressedThisFrame;
        this.PressedThisFrame[28] = genericinput.Controller_Xbox_X_PressedThisFrame;
        this.PressedThisFrame[29] = genericinput.Controller_Xbox_A_PressedThisFrame;
        this.PressedThisFrame[30] = this.PressedThisFrame[0];
        if (TinyZoo.FlagSettings.SwapButtonsForSwitch)
        {
          this.PressedThisFrame[0] = genericinput.Controller_Xbox_B_PressedThisFrame;
          this.PressedThisFrame[7] = genericinput.Controller_Xbox_A_PressedThisFrame;
        }
        this.RTriggerAnalogueFilterredLastFrame = genericinput.RTriggerAnalogueFilterred;
        this.LTriggerAnalogueFilterredLastFrame = genericinput.LTriggerAnalogueFilterred;
      }
      this.RightMousReleaseClick = false;
      if (MouseStatus.RMouseHeld)
        this.RightMousDownTime += DeltaTime;
      else if (MouseStatus.RMouseReleased && (double) this.RightMousDownTime > 0.0)
      {
        if ((double) this.RightMousDownTime < 0.200000002980232)
          this.RightMousReleaseClick = true;
        this.RightMousDownTime = 0.0f;
      }
      this.ReleasedThisFrame = new bool[36];
      if (this.HeldButtons != null)
      {
        this.ReleasedThisFrame[0] = this.HeldButtons[0] && !genericinput.Controller_Xbox_A_ButtonHeld;
        if (TinyZoo.FlagSettings.SwapButtonsForSwitch)
          this.ReleasedThisFrame[0] = this.HeldButtons[0] && !genericinput.Controller_Xbox_B_ButtonHeld;
      }
      this.ReleasedThisFrame[29] = genericinput.Controller_Xbox_A_ButtonReleased;
      this.ReleasedThisFrame[7] = genericinput.Controller_Xbox_B_ButtonReleased;
      if (!Z_DebugFlags.IsBetaVersion)
      {
        this.CameraStick = genericinput.LStick_Filtered;
        this.ZoomValue = 0.0f;
        this.ZoomValue = genericinput.RStick_Filtered.Y;
        this.CharacterMovementStick = genericinput.LStick_Filtered;
      }
      else
        this.ZoomValue = 0.0f;
      this.HeldButtons = new bool[36];
      if (!Z_DebugFlags.IsBetaVersion)
      {
        this.HeldButtons[2] = genericinput.Controller_Xbox_X_ButtonHeld;
        this.HeldButtons[1] = genericinput.Controller_Xbox_A_ButtonHeld;
        this.HeldButtons[0] = genericinput.Controller_Xbox_A_ButtonHeld;
        this.HeldButtons[6] = genericinput.Controller_DPadRightHeld;
        this.HeldButtons[3] = genericinput.Controller_DPadDownHeld;
        this.HeldButtons[4] = genericinput.Controller_DPadUpHeld;
        this.HeldButtons[5] = genericinput.Controller_DPadLeftHeld;
        this.HeldButtons[7] = genericinput.Controller_Xbox_B_PressedThisFrame;
        this.HeldButtons[8] = genericinput.Controller_L3_ButtonHeld;
        this.HeldButtons[9] = genericinput.Controller_L1_ButtonHeld;
        this.HeldButtons[15] = genericinput.Controller_Xbox_X_ButtonHeld;
        this.HeldButtons[12] = genericinput.Controller_Xbox_Y_ButtonHeld;
        this.HeldButtons[14] = genericinput.Controller_Xbox_A_ButtonHeld;
        this.HeldButtons[13] = genericinput.Controller_Xbox_B_ButtonHeld;
        this.HeldButtons[10] = genericinput.Controller_R1_ButtonHeld;
        this.HeldButtons[11] = genericinput.Controller_Xbox_A_ButtonHeld || genericinput.Controller_Xbox_B_PressedThisFrame;
        this.HeldButtons[19] = genericinput.Controller_DPadRightHeld || (double) genericinput.LStick_Filtered.X > 0.0;
        this.HeldButtons[16] = genericinput.Controller_DPadDownHeld || (double) genericinput.LStick_Filtered.Y < 0.0;
        this.HeldButtons[17] = genericinput.Controller_DPadUpHeld || (double) genericinput.LStick_Filtered.Y > 0.0;
        this.HeldButtons[18] = genericinput.Controller_DPadLeftHeld || (double) genericinput.LStick_Filtered.X < 0.0;
        this.HeldButtons[20] = genericinput.Controller_StartButtonHeld;
        this.HeldButtons[21] = (double) genericinput.RTriggerAnalogueFilterred > 0.0;
        this.HeldButtons[24] = genericinput.Controller_R3_PressedThisFrame;
        this.HeldButtons[25] = (double) genericinput.LTriggerAnalogueFilterred > 0.0;
        this.HeldButtons[26] = genericinput.Controller_Xbox_Y_ButtonHeld;
        this.HeldButtons[29] = genericinput.Controller_Xbox_A_ButtonHeld;
        if (TinyZoo.FlagSettings.SwapButtonsForSwitch)
        {
          this.HeldButtons[0] = genericinput.Controller_Xbox_B_ButtonHeld;
          this.HeldButtons[7] = genericinput.Controller_Xbox_A_PressedThisFrame;
        }
      }
      this.ReleasedThisFrame[30] = this.ReleasedThisFrame[0];
      this.HeldButtons[30] = this.ReleasedThisFrame[0];
      this.PressedThisFrame[22] = PC_KeyState.Backspace_PressedThisFrame;
      this.PressedThisFrame[23] = PC_KeyState.M_PressedThisFrame;
      this.PressedThisFrame[30] |= PC_KeyState.Enter_Pressed;
      this.ReleasedThisFrame[30] |= PC_KeyState.Enter_Released;
      this.HeldButtons[30] |= PC_KeyState.Enter_Held;
      this.HeldButtons[35] |= PC_KeyState.LShift_held;
      this.PressedThisFrame[31] = PC_KeyState.One_PressedThisFrame;
      this.PressedThisFrame[32] = PC_KeyState.Two_PressedThisFrame;
      this.PressedThisFrame[33] = PC_KeyState.Three_PressedThisFrame;
      this.PressedThisFrame[34] = PC_KeyState.Four_PressedThisFrame;
      if (PC_KeyState.Up_Held || PC_KeyState.Down_Held)
      {
        if (PC_KeyState.Up_Held && !PC_KeyState.Down_Held)
          this.CharacterMovementStick.Y = 1f;
        else if (!PC_KeyState.Up_Held && PC_KeyState.Down_Held)
          this.CharacterMovementStick.Y = -1f;
      }
      if (PC_KeyState.Left_Held || PC_KeyState.Right_Held)
      {
        if (PC_KeyState.Left_Held && !PC_KeyState.Right_Held)
          this.CharacterMovementStick.X = -1f;
        else if (!PC_KeyState.Left_Held && PC_KeyState.Right_Held)
          this.CharacterMovementStick.X = 1f;
        this.CharacterMovementStick.Normalize();
      }
      if (PC_KeyState.Left_Pressed || PC_KeyState.Right_Pressed || (PC_KeyState.Up_Pressed || PC_KeyState.Down_Pressed))
        this.PressedThisFrame[28] = true;
      this.LeftMouseHeld = false;
      if (MouseStatus.LMouseHeld)
        this.LeftMouseHeld = true;
      if (this.HeldButtons[0])
        this.LeftMouseHeld = true;
      this.PressedThisFrame[7] |= PC_KeyState.Backspace_PressedThisFrame;
      this.ReleasedThisFrame[7] |= PC_KeyState.Backspace_Released;
      this.PressedThisFrame[7] |= PC_KeyState.Escape_PressedThisFrame;
      this.ReleasedThisFrame[7] |= PC_KeyState.Escape_Released;
      if (!Z_DebugFlags.IsBetaVersion)
      {
        this.Movementstick = genericinput.LStick_Filtered;
        this.Movementstick.Y *= -1f;
        this.GatePlacementStick = this.Movementstick;
        if ((double) Math.Abs(this.GatePlacementStick.Y) > (double) Math.Abs(this.GatePlacementStick.X))
          this.GatePlacementStick.X = 0.0f;
        else
          this.GatePlacementStick.Y = 0.0f;
        this.RightStick = genericinput.RStick_Filtered;
      }
      this.DebugPauseTapped = PC_KeyState.P_PressedThisFrame;
      this.DebugSpeedUpTapped = PC_KeyState.Z_PressedThisFrame;
      this.SlowDownPressed = PC_KeyState.X_PressedThisFrame;
      int num1 = PC_KeyState.Enter_Pressed ? 1 : 0;
      this.AutoScrollWorld = PC_KeyState.Spacebar_PressedThisFrame;
      this.AddInventoryTapped = PC_KeyState.I_PressedThisFrame;
      this.DebugRotate = PC_KeyState.R_PressedThisFrame;
      this.DebugToggleTrackerHasBeenClaimed = PC_KeyState.B_PressedThisFrame;
      this.BackTapped = false;
      this.BackTapped |= PC_KeyState.Backspace_PressedThisFrame;
      if (this.BackTapped)
        Console.WriteLine("InputMap: back button tapped");
      if (!GameFlags.IsUsingMouse && (this.MousePosition != MouseStatus.MousePosition || MouseStatus.LMouseClicked || MouseStatus.RMouseClicked))
      {
        this.MousePosition = MouseStatus.MousePosition;
        GameFlags.IsUsingMouse = true;
      }
      if (!Z_DebugFlags.IsBetaVersion && (genericinput.AnyButtonPressedThisFrame || genericinput.LStick_Filtered != Vector2.Zero))
      {
        GameFlags.IsUsingController = true;
        GameFlags.IsUsingMouse = false;
      }
      this.rightmousedrag.UpdateRightMouseDrag(this);
      this.momentumwheel.UpdateMomentumMouseWheel(DeltaTime);
      this.PressedThisFrame[27] = this.PressedThisFrame[0] || this.PressedThisFrame[7];
      this.HeldButtons[27] = this.HeldButtons[0] || this.HeldButtons[7];
      int num2 = PC_KeyState.Left_Held ? 1 : 0;
      int num3 = PC_KeyState.Right_Held ? 1 : 0;
      int num4 = PC_KeyState.Up_Held ? 1 : 0;
      int num5 = PC_KeyState.Down_Held ? 1 : 0;
      this.KeyboardCameraMovement = Vector2.Zero;
      if (PC_KeyState.A_Held)
        this.KeyboardCameraMovement.X = 1f;
      if (PC_KeyState.D_Held)
        this.KeyboardCameraMovement.X = -1f;
      if (PC_KeyState.W_Held)
        this.KeyboardCameraMovement.Y = 1f;
      if (PC_KeyState.S_Held)
        this.KeyboardCameraMovement.Y = -1f;
      if ((double) this.KeyboardCameraMovement.X != 0.0 && (double) this.KeyboardCameraMovement.Y != 0.0)
        this.KeyboardCameraMovement.Normalize();
      if (PC_KeyState.E_Held)
        this.ZoomValue = 1f;
      if (PC_KeyState.Q_Held)
        this.ZoomValue = -1f;
      if ((!player.touchinput.DragActive || (double) player.touchinput.TouchStartLocation.X >= 0.0) && (double) player.touchinput.TouchStartLocation.X <= 1024.0)
        return;
      player.touchinput.DragActive = false;
      player.touchinput.DragVectorThisFrame = Vector2.Zero;
    }

    public void QuickCenter() => this.PointerLocation = Sengine.ReferenceScreenRes * 0.5f;

    public bool PressedBackOnController() => this.ReleasedThisFrame[7];

    private void CheckSwitchControllerIndexSwap(SEngine.Player.Player SEnginePlayer)
    {
      SwitchControllerConfig.UpdateForHandheldSwap();
      int NewPlayerID = (int) SEnginePlayer.GetPlayerIndexForController();
      if (SwitchControllerConfig.GetSwitchMode() != SwitchMode.Handheld)
        return;
      List<SwitchControllerStatus> allControllers = SwitchControllerConfig.GetAllControllers();
      for (int index = 0; index < allControllers.Count; ++index)
      {
        if ((NewPlayerID != 0 || index != 0) && (NewPlayerID != 1 || index != 1) && ((NewPlayerID != 2 || index != 2) && (NewPlayerID != 3 || index != 3)) && allControllers[index].isAnyButtonPressed)
        {
          switch (index)
          {
            case 0:
              NewPlayerID = 0;
              break;
            case 1:
              NewPlayerID = 1;
              break;
            case 2:
              NewPlayerID = 2;
              break;
            default:
              NewPlayerID = 3;
              break;
          }
          SEnginePlayer.SetPlayerIndexForController(NewPlayerID);
        }
      }
    }

    public void ClearAllInput(TinyZoo.Player player, bool WillClearDrag = true)
    {
      this.HeldButtons = new bool[36];
      this.PressedThisFrame = new bool[36];
      player.inputmap.Movementstick = Vector2.Zero;
      player.player.touchinput.EmptyTouchInput(WillClearDrag);
    }
  }
}
