// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.WatchVideoButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.GenericUI
{
  internal class WatchVideoButton : GameObject
  {
    private bool MouseOver;
    private Rectangle baserect;
    private string Texts;
    public float TextScale;
    private bool HasAdvertIcon;
    private TRC_ButtonDisplay controllerButton;
    public ControllerButton controllerbutton;
    private Vector2 ContOffset;

    public WatchVideoButton(string Text)
    {
      this.Texts = Text;
      this.baserect = new Rectangle(888, 438, 94, 22);
      this.SetDrawOriginToCentre();
      this.scale = 2.5f;
      this.SetDrawOriginToCentre();
      this.TextScale = 3f;
      this.HasAdvertIcon = true;
    }

    public void SetAsGreenButton()
    {
      this.baserect = new Rectangle(683, 369, 94, 22);
      this.DrawRect = this.baserect;
      this.HasAdvertIcon = false;
    }

    public void AddControllerButton(ControllerButton button, bool IsLeft = false)
    {
      this.ContOffset = new Vector2(-75f, -10f);
      if (!IsLeft)
        this.ContOffset.X *= -1f;
      this.controllerbutton = button;
      this.controllerButton = new TRC_ButtonDisplay(RenderMath.GetPixelSizeBestMatch(2f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier));
      this.controllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, button);
    }

    public bool UpdateWatchVideoButton(Player player, Vector2 Offset, bool ControllerSelected = false)
    {
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]))
        this.MouseOver = true;
      else if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation))
        this.MouseOver = true;
      if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]))
      {
        player.player.touchinput.ReleaseTapArray[0].X = -1000f;
        return true;
      }
      if (TinyZoo.GameFlags.IsUsingController & ControllerSelected && this.controllerButton != null)
      {
        switch (this.controllerbutton)
        {
          case ControllerButton.XboxA:
            if (player.inputmap.PressedThisFrame[14])
            {
              player.inputmap.ClearAllInput(player);
              return true;
            }
            break;
          case ControllerButton.XboxB:
            if (player.inputmap.PressedThisFrame[13])
            {
              player.inputmap.ClearAllInput(player);
              return true;
            }
            break;
          case ControllerButton.XboxX:
            if (player.inputmap.PressedThisFrame[15])
            {
              player.inputmap.ClearAllInput(player);
              return true;
            }
            break;
          case ControllerButton.XboxY:
            if (player.inputmap.PressedThisFrame[12])
            {
              player.inputmap.ClearAllInput(player);
              return true;
            }
            break;
        }
      }
      return false;
    }

    public void DrawWatchVideoButton(Vector2 Offset, bool ControllerSelected = false)
    {
      this.DrawRect = this.baserect;
      this.SetDrawOriginToCentre();
      if (this.MouseOver)
        this.DrawRect.Y += this.baserect.Height + 1;
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset);
      if (this.MouseOver)
        Offset.Y += 1f * this.scale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      float x = 0.0f;
      if (this.HasAdvertIcon)
        x = 20f * Sengine.ScreenRationReductionMultiplier.Y * Sengine.UltraWideSreenUpwardsMultiplier;
      TextFunctions.DrawJustifiedText(this.Texts, this.TextScale, this.vLocation + Offset + new Vector2(x, 0.0f), Color.White, 1f, AssetContainer.springFont, AssetContainer.pointspritebatchTop05);
      this.MouseOver = false;
      if (!(TinyZoo.GameFlags.IsUsingController & ControllerSelected) || this.controllerButton == null)
        return;
      float num1 = 1f;
      int num2 = 15;
      if ((double) this.ContOffset.X < 0.0)
      {
        num1 = -1f;
        num2 = 4;
      }
      this.controllerButton.DrawTRC_ButtonDisplay(AssetContainer.pointspritebatchTop05, AssetContainer.TRC_Sprites, Offset + this.vLocation + new Vector2((float) ((double) (this.DrawRect.Width - num2) * (double) this.scale * 0.5) * num1, this.ContOffset.Y));
    }
  }
}
