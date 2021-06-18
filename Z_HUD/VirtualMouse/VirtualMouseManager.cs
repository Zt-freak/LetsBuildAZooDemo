// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.VirtualMouse.VirtualMouseManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Layout;
using System.Collections.Generic;
using TinyZoo.Z_BuldMenu;

namespace TinyZoo.Z_HUD.VirtualMouse
{
  internal class VirtualMouseManager
  {
    private List<MouseParticle> MouseParticles;
    private float FadeDelay;
    private float MasterAlpha;
    private GameObject MousePointer;
    private MouseParticle Black;
    private Vector2 LastMousePos;
    private float HOLDATimer;
    private GameObject BIN;
    internal static bool TempBlockCameraMovement;
    internal static float MouseAlpha;
    internal static bool DrawBin;
    private int LastIndex;
    private float PulseTimer;
    private static float BlockDrawTimer;

    public VirtualMouseManager()
    {
      this.MousePointer = new GameObject();
      this.MousePointer.DrawRect = new Rectangle(231, 416, 20, 20);
      this.MousePointer.scale = Z_GameFlags.GetBaseScaleForUI();
      this.MouseParticles = new List<MouseParticle>();
      int TotalParticles = 48;
      for (int Idnex = 0; Idnex < TotalParticles; ++Idnex)
        this.MouseParticles.Add(new MouseParticle(Idnex, TotalParticles));
      this.Black = new MouseParticle(0, 10, true);
      this.BIN = new GameObject();
      this.BIN.DrawRect = new Rectangle(139, 72, 16, 16);
      this.BIN.DrawOrigin = new Vector2(-8f, -8f);
    }

    public void UpdateMousePointerManager(Player player, float DeltaTime)
    {
      if (!GameFlags.IsUsingController)
      {
        if (TinyZoo.Game1.gamestate == GAMESTATE.PhotoMode)
        {
          if (player.inputmap.PointerLocation != MouseStatus.MousePosition)
            VirtualMouseManager.BlockDrawTimer = 0.0f;
          VirtualMouseManager.BlockDrawTimer += GameFlags.RefDeltaTime;
        }
        else
          VirtualMouseManager.BlockDrawTimer = 0.0f;
        VirtualMouseManager.TempBlockCameraMovement = false;
        player.inputmap.PointerLocation = MouseStatus.MousePosition;
        if ((double) player.inputmap.PointerLocation.X > 1024.0)
          player.inputmap.PointerLocation.X = -1000f;
        else
          this.LastMousePos = MouseStatus.MousePosition;
        if (player.inputmap.PressedThisFrame[27] || player.inputmap.Movementstick != Vector2.Zero || player.inputmap.RightStick != Vector2.Zero)
          Z_GameFlags.HasUsedAControllerThisSession = true;
      }
      else
      {
        if (Z_GameFlags.BlockVirtualMouse() || VirtualMouseManager.TempBlockCameraMovement)
        {
          VirtualMouseManager.TempBlockCameraMovement = false;
          player.inputmap.Movementstick = Vector2.Zero;
          return;
        }
        if (player.inputmap.PressedThisFrame[24])
          this.CentreMouse(player);
        float num1 = 400f;
        if (player.inputmap.HeldButtons[25])
          num1 = 1200f;
        player.inputmap.PointerLocation.X += player.inputmap.CameraStick.X * DeltaTime * num1;
        player.inputmap.PointerLocation.Y -= player.inputmap.CameraStick.Y * DeltaTime * num1 * Sengine.ScreenRatioUpwardsMultiplier.Y;
        player.inputmap.PointerLocation.X = MathHelper.Clamp(player.inputmap.PointerLocation.X, 5f, 1019f);
        player.inputmap.PointerLocation.Y = MathHelper.Clamp(player.inputmap.PointerLocation.Y, 5f * Sengine.ScreenRatioUpwardsMultiplier.Y, (float) (768.0 - 5.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
        if (player.inputmap.PressedThisFrame[0])
          player.player.touchinput.MultiTouchTapArray[0] = player.inputmap.PointerLocation;
        if (player.inputmap.ReleasedThisFrame[0])
          player.player.touchinput.ReleaseTapArray[0] = player.inputmap.PointerLocation;
        if (player.inputmap.HeldButtons[0])
        {
          this.HOLDATimer += DeltaTime;
          player.player.touchinput.MultiTouchTouchLocations[0] = player.inputmap.PointerLocation;
        }
        else
          this.HOLDATimer = 0.0f;
        VirtualMouseManager.SetCameraStick(player, DeltaTime);
        int num2 = this.LastMousePos != MouseStatus.MousePosition ? 1 : 0;
      }
      if (!GameFlags.HasPassedSplash)
        return;
      Z_GameFlags.BlockPointer = false;
      this.MasterAlpha = 1f;
      this.MouseParticles[0].UpdateMouseParticle(player.inputmap.PointerLocation, DeltaTime, player.inputmap.PointerLocation, false);
      this.PulseTimer -= DeltaTime;
      if (player.inputmap.HeldButtons[25])
        this.PulseTimer -= DeltaTime;
      int num = -1;
      if ((double) this.PulseTimer < 0.0)
      {
        this.PulseTimer = 0.03f;
        num = this.LastIndex;
        ++this.LastIndex;
        if (this.LastIndex >= this.MouseParticles.Count)
          this.LastIndex = 1;
      }
      for (int index = 1; index < this.MouseParticles.Count; ++index)
      {
        if (index == 1)
          this.MouseParticles[index].UpdateMouseParticle(player.inputmap.PointerLocation, DeltaTime, this.MouseParticles[this.MouseParticles.Count - 1].vLocation, num == index);
        else
          this.MouseParticles[index].UpdateMouseParticle(player.inputmap.PointerLocation, DeltaTime, this.MouseParticles[index - 1].vLocation, num == index);
      }
      this.Black.UpdateMouseParticle(player.inputmap.PointerLocation, DeltaTime, player.inputmap.PointerLocation, false);
    }

    internal static void SetCameraStick(Player player, float DeltaTime)
    {
      float num1 = 350f;
      float num2 = 150f;
      float num3 = 1f;
      if (player.inputmap.HeldButtons[25])
      {
        num3 = 3f;
      }
      else
      {
        num1 = 150f;
        num2 = 50f;
      }
      if ((double) player.inputmap.PointerLocation.X < (double) num1 || (double) player.inputmap.PointerLocation.X > 1024.0 - (double) num1)
      {
        if ((double) player.inputmap.PointerLocation.X < (double) num1)
        {
          float num4 = player.inputmap.PointerLocation.X - (num1 - num2);
          if ((double) num4 > 0.0)
            num3 = (float) (1.0 - (double) num4 / (double) num2);
        }
        else
        {
          float num4 = (float) -((double) player.inputmap.PointerLocation.X - (1024.0 - ((double) num1 - (double) num2)));
          if ((double) num4 > 0.0)
            num3 = (float) (1.0 - (double) num4 / (double) num2);
        }
        player.inputmap.CameraStick.X += player.inputmap.CameraStick.X * num3;
      }
      float num5 = 768f;
      if (Z_GameFlags.UseBuildCam())
        num5 = Z_BuildingIconPanel.MinHeight;
      if ((double) player.inputmap.PointerLocation.Y >= (double) num1 && (double) player.inputmap.PointerLocation.Y <= (double) num5 - (double) num1)
        return;
      if ((double) player.inputmap.PointerLocation.Y < (double) num1)
      {
        float num4 = player.inputmap.PointerLocation.Y - (num1 - num2);
        if ((double) num4 > 0.0)
          num3 = (float) (1.0 - (double) num4 / (double) num2);
      }
      else
      {
        float num4 = (float) -((double) player.inputmap.PointerLocation.Y - ((double) num5 - ((double) num1 - (double) num2)));
        if ((double) num4 > 0.0)
          num3 = (float) (1.0 - (double) num4 / (double) num2);
      }
      if ((double) player.inputmap.PointerLocation.Y > (double) num5 - 1.0)
        player.inputmap.PointerLocation.Y = num5 - 1f;
      player.inputmap.CameraStick.Y += player.inputmap.CameraStick.Y * num3;
    }

    private void CentreMouse(Player player)
    {
      player.inputmap.PointerLocation = Sengine.ReferenceScreenRes * 0.5f;
      List<Vector2> spiralLayout = CircleSprialLayout.GetSPiralLayout(this.MouseParticles.Count - 1, 3f, 7f, Sengine.ScreenRatioUpwardsMultiplier.Y, true);
      for (int index = 1; index < this.MouseParticles.Count; ++index)
      {
        this.MouseParticles[index].SetAlpha(false, 0.5f, 1f, 0.0f);
        this.MouseParticles[index].vLocation = spiralLayout[index - 1] + player.inputmap.PointerLocation;
      }
    }

    public void DrawMousePointerManager(Player player)
    {
      if (TinyZoo.Game1.gamestate != GAMESTATE.SplashScreen && !GameFlags.IsUsingController)
      {
        this.MousePointer.scale = Z_GameFlags.GetBaseScaleForUI();
        this.MousePointer.vLocation = player.inputmap.PointerLocation;
        this.MousePointer.fAlpha = 1f;
        if (TinyZoo.Game1.gamestate == GAMESTATE.PhotoMode && (double) VirtualMouseManager.BlockDrawTimer > 0.5)
        {
          this.MousePointer.fAlpha = (float) (1.0 - ((double) VirtualMouseManager.BlockDrawTimer - 0.5) * 2.0);
          if ((double) this.MousePointer.fAlpha < 0.0)
            this.MousePointer.fAlpha = 0.0f;
        }
        VirtualMouseManager.MouseAlpha = this.MousePointer.fAlpha;
        if (VirtualMouseManager.DrawBin)
        {
          VirtualMouseManager.DrawBin = false;
          this.BIN.scale = this.MousePointer.scale * 2f;
          this.BIN.fAlpha = this.MousePointer.fAlpha;
          this.BIN.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.SpriteSheet, this.MousePointer.vLocation);
        }
        this.MousePointer.Draw(AssetContainer.pointspritebatch07Final, AssetContainer.SpriteSheet);
      }
      if (Z_GameFlags.TempBlockVirtualMouse)
      {
        Z_GameFlags.TempBlockVirtualMouse = false;
      }
      else
      {
        if (Z_GameFlags.BlockPointer || Z_GameFlags.UseBuildCam() || (OverWorldManager.overworldstate == OverWOrldState.PlayingAsAvatar || !GameFlags.HasPassedSplash) || !GameFlags.IsUsingController)
          return;
        if (AssetContainer.SpriteSheet != null)
        {
          for (int index = this.MouseParticles.Count - 1; index > -1; --index)
            this.MouseParticles[index].DrawMouseParticle(this.MasterAlpha);
        }
        this.Black.DrawMouseParticle(this.MasterAlpha);
      }
    }
  }
}
