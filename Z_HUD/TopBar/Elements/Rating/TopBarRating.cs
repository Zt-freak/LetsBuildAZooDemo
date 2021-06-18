// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Rating.TopBarRating
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.RatingPopUp;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_HUD.TopBar.Elements.Rating
{
  internal class TopBarRating
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private ZGenericText popText;
    private string baseString;
    private LerpHandler_Float lerper;
    private RatingInfoFrame infoFrame;
    private LittleSummaryButton infoIcon;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private int lastRatingValue = -1;
    private LerpHandler_Float numberLerper;

    public TopBarRating(Player player, float _BaseScale, float frameHeight)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.lastRatingValue = -1;
      this.headerBase = new TopBarHeaderBase(this.BaseScale, frameHeight, this.scaleHelper.ScaleX(150f), true);
      Vector2 vector2 = -this.GetSize() * 0.5f;
      float num1 = defaultXbuffer + vector2.X;
      if (false)
      {
        this.infoIcon = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: this.BaseScale);
        this.infoIcon.vLocation.X = num1 + this.infoIcon.GetSize().X * 0.5f;
        num1 += this.infoIcon.GetSize().X + defaultXbuffer;
      }
      this.baseString = "Park Rating: ";
      this.popText = new ZGenericText(this.baseString, this.BaseScale, _UseOnePointFiveFont: true);
      Vector2 size = this.popText.GetSize();
      float num2 = num1 + (size.X + this.scaleHelper.ScaleX(40f));
      this.lerper = new LerpHandler_Float();
      this.numberLerper = new LerpHandler_Float();
      this.SetNewRatingValue(ParkRating.GetParkRating(player));
    }

    public void SetNewRatingValue(int Rating)
    {
      if (this.lastRatingValue != -1)
      {
        if (Rating != this.lastRatingValue)
        {
          this.numberLerper.SetLerp(true, (float) this.lastRatingValue, (float) Rating, 3f);
          this.headerBase.DoFlash(this.lastRatingValue > Rating);
          if (this.infoFrame != null)
            this.infoFrame.NeedToRefreshValues = true;
        }
      }
      else
        this.numberLerper.Value = (float) Rating;
      this.lastRatingValue = Rating;
    }

    public Vector2 GetSize() => this.headerBase.GetSize();

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

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.infoFrame != null && this.infoFrame.CheckMouseOver(player, offset);
    }

    public void UpdateTopBarRating(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.numberLerper.UpdateLerpHandler(DeltaTime);
      if (FeatureFlags.BlockAllUI)
        this.LerpOff();
      else
        this.LerpIn();
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value == -1.0)
        return;
      this.SetNewRatingValue(ParkRating.GetParkRating(player));
      this.popText.textToWrite = this.baseString + (object) (int) Math.Round((double) this.numberLerper.Value);
      if (this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset))
      {
        if (this.infoFrame == null)
        {
          this.infoFrame = new RatingInfoFrame(player, this.BaseScale);
          this.infoFrame.location.Y += TopBarManager.GetMiddleOfBar();
          this.infoFrame.location.Y += this.infoFrame.GetSize().Y * 0.5f;
          this.infoFrame.location.X += (float) ((double) this.infoFrame.GetSize().X * 0.5 - (double) this.GetSize().X * 0.5);
        }
        else
          this.infoFrame.ToggleLerp();
      }
      else
      {
        if (this.infoFrame == null)
          return;
        this.infoFrame.UpdateRatingInfoFrame(player, DeltaTime, offset);
        if (FeatureFlags.BlockAllUI)
        {
          this.infoFrame.LerpOff();
        }
        else
        {
          if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || MathStuff.CheckPointCollision(true, this.infoFrame.location + offset, 1f, this.infoFrame.GetSize().X, this.infoFrame.GetSize().Y, player.player.touchinput.ReleaseTapArray[0]))
            return;
          this.infoFrame.LerpOff();
        }
      }
    }

    public void PreDrawTopBarRating(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (this.infoFrame == null)
        return;
      this.infoFrame.DrawRatingInfoFrame(offset, spriteBatch);
    }

    public void DrawTopBarRating(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.popText.DrawZGenericText(offset, spriteBatch);
      if (this.infoIcon != null)
        this.infoIcon.DrawLittleSummaryButton(offset, spriteBatch);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}
