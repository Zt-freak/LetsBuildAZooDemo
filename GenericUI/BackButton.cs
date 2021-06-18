// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.BackButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.GenericUI
{
  internal class BackButton : GameObject
  {
    public TextButton backbtn;
    private LerpHandler_Float lerper;
    private bool IsXButton;
    private Rectangle BaseRect;
    public bool MouseOver;
    public bool IsPrevious;
    private Rectangle mouseOverRect;
    public bool OnlyAllowMouseClicks;

    public BackButton(
      bool SkipLerp = false,
      bool IsNewOne = true,
      float BaseScale = -1f,
      bool _IsPrevious = false,
      bool isSmallVersion = false)
    {
      this.IsPrevious = _IsPrevious;
      float OverAllMultiplier = 1f;
      if (GameFlags.MobileUIScale)
        OverAllMultiplier = 1.3f;
      float Length = 30f;
      string str = "";
      if (GameFlags.IsConsoleVersion)
        Length = 40f;
      if ((double) BaseScale > -1.0)
        OverAllMultiplier = BaseScale;
      this.backbtn = new TextButton(str + SEngine.Localization.Localization.GetText(13), Length, OverAllMultiplier: OverAllMultiplier);
      float num = 30f;
      if (GameFlags.HasNotch)
        num += 10f;
      this.backbtn.vLocation = new Vector2(num + 40f * Sengine.ScreenRationReductionMultiplier.Y * OverAllMultiplier, 50f * Sengine.ScreenRationReductionMultiplier.Y * OverAllMultiplier);
      this.backbtn.AddControllerButton(ControllerButton.XboxB);
      if (GameFlags.MobileUIScale)
        this.backbtn.CollisionEx = new Vector2(10f, 20f);
      this.backbtn.SetYellow();
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      if (SkipLerp)
        this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);
      if (!IsNewOne)
        return;
      this.IsXButton = true;
      this.BaseRect = new Rectangle(983, 186, 30, 30);
      this.mouseOverRect = new Rectangle(983, 217, 30, 30);
      if (this.IsPrevious)
      {
        this.BaseRect = new Rectangle(987, 502, 26, 26);
        this.mouseOverRect = new Rectangle(987, 529, 26, 26);
      }
      if (isSmallVersion)
      {
        this.BaseRect = new Rectangle(964, 313, 17, 17);
        this.mouseOverRect = new Rectangle(944, 313, 17, 17);
      }
      this.DrawRect = this.BaseRect;
      this.SetDrawOriginToCentre();
      this.scale = 2f * Sengine.ScreenRationReductionMultiplier.Y;
      if (DebugFlags.IsPCVersion)
        this.scale *= 0.5f;
      if ((double) BaseScale > -1.0)
        this.scale = BaseScale;
      this.vLocation = new Vector2(980f, 50f * Sengine.ScreenRationReductionMultiplier.Y);
    }

    public void TryLerpOn()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return;
      this.LerpOn();
    }

    public void TryLerpOff()
    {
      if ((double) this.lerper.TargetValue != 0.0)
        return;
      this.Exit();
    }

    public bool ExitComplete() => (double) this.lerper.Value == -1.0;

    public void Exit() => this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public bool CheckCollision(Vector2 TouchPos) => MathStuff.CheckPointCollision(true, this.vLocation, this.scale, (float) (this.DrawRect.Width + 2), ((float) this.DrawRect.Height + 2f) * Sengine.ScreenRatioUpwardsMultiplier.Y, TouchPos);

    public bool UpdateBackButton(Player player, float DeltaTime) => this.UpdateBackButton(player, DeltaTime, Vector2.Zero);

    public bool UpdateBackButton(Player player, float DeltaTime, Vector2 Offset)
    {
      this.MouseOver = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == 0.0)
      {
        double x = (double) player.player.touchinput.ReleaseTapArray[0].X;
        if (this.IsXButton)
        {
          this.MouseOver |= MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) (this.DrawRect.Width + 2), ((float) this.DrawRect.Height + 2f) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]);
          if (!Flags.PlatformIsMobile)
            this.MouseOver |= MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) (this.DrawRect.Width + 2), ((float) this.DrawRect.Height + 2f) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
          if (MathStuff.CheckPointCollision(true, this.vLocation + Offset, this.scale, (float) (this.DrawRect.Width + 2), ((float) this.DrawRect.Height + 2f) * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.ReleaseTapArray[0]) || player.inputmap.PressedBackOnController() && !this.OnlyAllowMouseClicks)
          {
            player.inputmap.ReleasedThisFrame[7] = false;
            player.player.touchinput.ReleaseTapArray[0].X = -10000f;
            return true;
          }
        }
        else if (this.backbtn.UpdateTextButton(player, Vector2.Zero, DeltaTime) || player.inputmap.PressedBackOnController())
        {
          player.inputmap.ReleasedThisFrame[7] = false;
          return true;
        }
      }
      return false;
    }

    public void LerpOn()
    {
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
    }

    public void DrawBackButton(Vector2 Offset) => this.DrawBackButton(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawBackButton(Vector2 Offset, SpriteBatch spriteBatch, float AlphaMult = 1f)
    {
      if (this.IsXButton)
      {
        if (this.MouseOver)
          this.DrawRect = this.mouseOverRect;
        this.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset + new Vector2((float) (500.0 * -(double) this.lerper.Value), 0.0f), this.fAlpha * AlphaMult);
        if (!this.MouseOver)
          return;
        this.DrawRect = this.BaseRect;
      }
      else
        this.backbtn.DrawTextButton(Offset + new Vector2(500f * this.lerper.Value, 0.0f), 1f, spriteBatch);
    }
  }
}
