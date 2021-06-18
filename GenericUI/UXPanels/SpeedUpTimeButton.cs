// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.SpeedUpTimeButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.Z_HUD.TopBar;
using TRC_Helper;
using TRC_Helper.ControllerUI;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class SpeedUpTimeButton
  {
    private UXFrame frame;
    private GameObject Speed;
    public Vector2 Location;
    private bool Paused;
    private int IsFast;
    private TRC_ButtonDisplay controllerButton;
    private LerpHandler_Float lerper;
    private float extraOffsetForSpeedIconForMouseOver;

    public SpeedUpTimeButton(Player player, float BaseScale = -1f, bool DoNotSetOwnPosition = false)
    {
      float _Scale1 = RenderMath.GetPixelSizeBestMatch(2f);
      float _Scale2 = 2f;
      if (TinyZoo.GameFlags.MobileUIScale)
        _Scale1 = RenderMath.GetPixelSizeBestMatch(2f * Sengine.UltraWideSreenDownardsMultiplier);
      if ((double) BaseScale != -1.0)
      {
        _Scale1 = BaseScale * 2f;
        _Scale2 = BaseScale;
      }
      this.frame = new UXFrame(_Scale1, VerySmall: true);
      this.frame.scale *= 0.5f;
      this.Speed = new GameObject();
      this.Speed.scale = this.frame.scale;
      this.SetSpeed(player);
      this.controllerButton = new TRC_ButtonDisplay(_Scale2);
      this.controllerButton.SetAsStaticButton(TinyZoo.GameFlags.SelectedControllerType, ButtonStyle.SuperSmall, ControllerButton.RT);
      if (!DoNotSetOwnPosition)
      {
        this.Location = new Vector2(650f, (float) ((double) this.frame.scale * (double) this.frame.DrawRect.Height * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y);
        this.Location.Y = this.Location.Y * 0.6f * Sengine.ScreenRatioUpwardsMultiplier.Y;
        this.Location.Y = 20f;
        this.Location.X -= 150f;
        this.controllerButton.vLocation = new Vector2((float) -this.frame.DrawRect.Width * this.frame.scale, 0.0f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      }
      else
        this.controllerButton.vLocation = new Vector2((float) ((double) this.frame.DrawRect.Width * (double) this.frame.scale * 0.5), 0.0f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.lerper = new LerpHandler_Float();
      this.extraOffsetForSpeedIconForMouseOver = 2f * BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Vector2 GetSize() => this.frame.GetSize();

    private void SetSpeed(Player player)
    {
      this.IsFast = player.livestats.SpeedUpSimulation;
      this.Speed.DrawRect = this.IsFast != 0 ? (this.IsFast != 1 ? (this.IsFast != 2 ? new Rectangle(990, 766, 23, 22) : new Rectangle(95, 30, 23, 22)) : new Rectangle(966, 766, 23, 22)) : new Rectangle(942, 766, 23, 22);
      this.Speed.SetDrawOriginToCentre();
      this.Speed.DrawOrigin.X -= 0.5f;
    }

    public void LerpIn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.lerper.SetLerp(false, -1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if ((double) this.lerper.TargetValue == -1.0)
        return;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f);
    }

    public void UpdateSpeedUpTimeButton(Player player, float DeltaTime, Vector2 Offset)
    {
      if (FeatureFlags.BlockAllUI || FeatureFlags.BlockSpeedUp)
        this.LerpOff();
      else
        this.LerpIn();
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (!this.Paused)
      {
        if (this.IsFast != player.livestats.SpeedUpSimulation)
          this.SetSpeed(player);
        if (player.inputmap.PressedThisFrame[31])
        {
          if (player.livestats.SpeedUpSimulation != 0)
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
            player.livestats.SpeedUpSimulation = 0;
          }
        }
        else if (player.inputmap.PressedThisFrame[32])
        {
          if (player.livestats.SpeedUpSimulation != 1)
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
            player.livestats.SpeedUpSimulation = 1;
          }
        }
        else if (player.inputmap.PressedThisFrame[33])
        {
          if (player.livestats.SpeedUpSimulation != 2)
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
            player.livestats.SpeedUpSimulation = 2;
          }
        }
        else if (player.inputmap.PressedThisFrame[34] && player.livestats.SpeedUpSimulation != 3)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
          player.livestats.SpeedUpSimulation = 3;
        }
        if (this.frame.CheckTaps(player, this.Location + Offset, true) || player.inputmap.PressedThisFrame[21])
        {
          player.tracking.TappedPlayButton(player);
          player.player.touchinput.ReleaseTapArray[0].X = -1000f;
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, Pitch: 0.4f);
          ++player.livestats.SpeedUpSimulation;
          if (player.livestats.SpeedUpSimulation > 3)
            player.livestats.SpeedUpSimulation = 0;
          this.SetSpeed(player);
        }
        if (this.Paused || !player.livestats.SimulationIsPaused)
          return;
        this.Paused = true;
        this.Speed.DrawRect = new Rectangle(146, 0, 9, 8);
        this.Speed.SetDrawOriginToCentre();
      }
      else
      {
        if (player.livestats.SimulationIsPaused)
          return;
        this.Paused = false;
        this.SetSpeed(player);
      }
    }

    public void DrawSpeedUpButton(Vector2 Offset) => this.DrawSpeedUpButton(Offset, AssetContainer.pointspritebatch03);

    public void DrawSpeedUpButton(Vector2 Offset, SpriteBatch spriteBatch)
    {
      Offset += this.Location;
      Offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.frame.DrawUXFrame(Offset, spriteBatch);
      Vector2 zero = Vector2.Zero;
      if (this.frame.IsMouseOver)
        zero.Y += this.extraOffsetForSpeedIconForMouseOver;
      this.Speed.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset + zero);
      if (!TinyZoo.GameFlags.IsUsingController)
        return;
      this.controllerButton.DrawTRC_ButtonDisplay(spriteBatch, AssetContainer.TRC_Sprites, Offset);
    }
  }
}
