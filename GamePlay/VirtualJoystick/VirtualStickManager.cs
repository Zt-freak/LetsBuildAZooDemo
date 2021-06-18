// Decompiled with JetBrains decompiler
// Type: TinyZoo.GamePlay.VirtualJoystick.VirtualStickManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.PlayerDir;
using TinyZoo.Settings.Sound;

namespace TinyZoo.GamePlay.VirtualJoystick
{
  internal class VirtualStickManager
  {
    private bool WasTouchingLastFrame;
    private Vector2 TouchStartLocation;
    private GameObject Ring;
    private GameObject Stick;
    internal static bool IsMoving;
    private bool HasMoved;

    public VirtualStickManager()
    {
      this.Ring = new GameObject();
      this.Stick = new GameObject();
      this.Ring.DrawRect = new Rectangle(128, 630, 196, 196);
      this.Stick.DrawRect = new Rectangle(0, 730, 80, 80);
      this.Stick.SetDrawOriginToCentre();
      this.Ring.SetDrawOriginToCentre();
      this.Stick.SetAlpha(0.0f);
      this.Ring.SetAlpha(0.0f);
    }

    public void UpdateVirtualStickManager(Player player, float DeltaTime)
    {
      VirtualStickManager.IsMoving = false;
      if (!FeatureFlags.VirtualSTickEnabled || player.livestats.IsDraggingShip || InputMap.virtualstick == VirtualStickStatus.Off)
        return;
      if (!this.WasTouchingLastFrame)
      {
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0 && (double) player.player.touchinput.TouchTimeStamp > 0.100000001490116)
        {
          this.HasMoved = false;
          this.WasTouchingLastFrame = true;
          this.TouchStartLocation = player.player.touchinput.TouchStartLocation;
          this.Stick.SetAlpha(false, 0.05f, 0.0f, 0.5f);
          this.Ring.SetAlpha(false, 0.05f, 0.0f, 0.5f);
          this.Ring.vLocation = this.TouchStartLocation;
        }
      }
      else
      {
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X < 0.0)
        {
          this.Stick.SetAlpha(true, 0.5f, 0.0f, 0.0f);
          this.Ring.SetAlpha(true, 0.5f, 0.0f, 0.0f);
          this.WasTouchingLastFrame = false;
          player.player.touchinput.MultiTouchTouchLocations[0].X = -10000f;
        }
        else
        {
          Vector2 vector2_1 = player.player.touchinput.MultiTouchTouchLocations[0] - this.TouchStartLocation;
          float num1 = 80f;
          if ((double) vector2_1.Length() > (double) num1)
            vector2_1 = Vector2.Normalize(vector2_1) * num1;
          this.Stick.vLocation = vector2_1 + this.TouchStartLocation;
          float num2 = 8f;
          float num3 = vector2_1.Length();
          if ((double) num3 > (double) num2)
          {
            this.HasMoved = true;
            Vector2 vector2_2 = Vector2.Normalize(vector2_1);
            player.inputmap.Movementstick = InputMap.virtualstick == VirtualStickStatus.DigitalOnly || InputMap.virtualstick == VirtualStickStatus.Digital ? vector2_2 : vector2_2 * (float) (((double) num3 - (double) num2) / ((double) num1 - (double) num2));
          }
        }
        VirtualStickManager.IsMoving = this.HasMoved;
      }
      this.Stick.UpdateColours(DeltaTime);
      this.Ring.UpdateColours(DeltaTime);
    }

    public void DrawVirtualStickManager()
    {
      if (!FeatureFlags.VirtualSTickEnabled || GameFlags.GamePaused || InputMap.virtualstick == VirtualStickStatus.Off)
        return;
      this.Stick.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
      this.Ring.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet);
    }
  }
}
