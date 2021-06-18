// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.GenericTopBarPopOutFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp
{
  internal class GenericTopBarPopOutFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private BackButton previousButton;
    private LerpHandler_Float lerper;
    public UIScaleHelper scaleHelper;
    public float BaseScale;
    private Vector2 ExtraHeightToHideCorners;
    public bool NeedToRefreshValues;
    private bool DoCropAboveCertainHeight;
    private float CropHeight;
    private Vector2 frameSize;

    public bool IsPanelActive => this.customerFrame.Active;

    public GenericTopBarPopOutFrame(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.lerper = new LerpHandler_Float();
      this.LerpIn(true);
    }

    public void FinalizeSize(
      Vector2 _frameSize,
      Vector3 frameColor,
      float extraXTohideCorners = 0.0f,
      float extraYtoHideCorners = 2f)
    {
      this.frameSize = _frameSize;
      this.ExtraHeightToHideCorners = this.scaleHelper.ScaleVector2(new Vector2(extraXTohideCorners, extraYtoHideCorners));
      this.frameSize += this.ExtraHeightToHideCorners;
      this.customerFrame = new CustomerFrame(this.frameSize, frameColor, this.BaseScale);
      this.customerFrame.location -= this.ExtraHeightToHideCorners;
    }

    public void FinalizeSize(
      Vector2 _frameSize,
      float extraXTohideCorners = 0.0f,
      float extraYtoHideCorners = 2f)
    {
      this.frameSize = _frameSize;
      this.FinalizeSize(this.frameSize, ColourData.Z_FrameDarkBrown, extraXTohideCorners, extraYtoHideCorners);
    }

    public virtual void AddPreviousButton()
    {
      this.previousButton = new BackButton(true, BaseScale: this.BaseScale, _IsPrevious: true);
      this.previousButton.vLocation = new Vector2(this.frameSize.X * 0.5f, (float) (-(double) this.frameSize.Y * 0.5));
      this.previousButton.vLocation.X -= this.scaleHelper.DefaultBuffer.X;
      this.previousButton.vLocation.Y += this.scaleHelper.DefaultBuffer.Y;
      this.previousButton.vLocation.Y -= this.previousButton.GetSize().X;
      this.previousButton.vLocation.Y += this.previousButton.GetSize().Y;
    }

    public virtual bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.GetDrawOffset();
      return MathStuff.CheckPointCollision(true, offset, 1f, this.GetSize().X, this.GetSize().Y, player.inputmap.PointerLocation);
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public virtual void ToggleLerp()
    {
      if (this.LerpIn())
        return;
      this.LerpOff();
    }

    public virtual bool LerpIn(bool ForceLerp = false)
    {
      if (!((double) this.lerper.TargetValue != 1.0 | ForceLerp))
        return false;
      this.lerper.SetLerp(true, 0.0f, 1f, 3f);
      return true;
    }

    public virtual bool LerpOff()
    {
      if ((double) this.lerper.TargetValue == 0.0)
        return false;
      this.lerper.SetLerp(false, 1f, 0.0f, 3f);
      return true;
    }

    public bool IsOffScreen() => (double) this.lerper.Value == 0.0;

    public bool IsDoneLerping() => this.lerper.IsComplete();

    public Vector2 GetFrameOffset() => -this.customerFrame.VSCale * 0.5f;

    public virtual bool UpdatePopOutFrame(Player player, float DeltaTime, ref Vector2 offset)
    {
      offset += this.GetDrawOffset();
      this.lerper.UpdateLerpHandler(DeltaTime);
      return this.customerFrame.UpdateForMouseOverAndClick(player, DeltaTime, offset, out bool _);
    }

    public virtual bool ClickedPreviousButton(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.GetDrawOffset();
      return this.previousButton.UpdateBackButton(player, DeltaTime, offset);
    }

    public void LockForBeta() => this.customerFrame.LockForBeta();

    public Vector2 GetDrawOffset() => this.location + new Vector2(0.0f, (float) ((1.0 - (double) this.lerper.Value) * -(double) this.customerFrame.VSCale.Y));

    public virtual void DrawPopOutFrame(ref Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.GetDrawOffset();
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.previousButton == null)
        return;
      this.previousButton.DrawBackButton(offset, spriteBatch);
    }

    public virtual void PostDrawPopOutFrame(Vector2 offset, SpriteBatch spriteBatch) => this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
  }
}
