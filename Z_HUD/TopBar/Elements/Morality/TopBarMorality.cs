// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Morality.TopBarMorality
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row;
using TinyZoo.Z_Morality;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_HUD.TopBar.Elements.Morality
{
  internal class TopBarMorality
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private ZGenericText moralityWord;
    private ZGenericText moralityStatus;
    private GoodEvilIcon goodEvilIcon;
    private LittleSummaryButton infoIcon;
    private LerpHandler_Float lerper;
    private float BaseScale;
    private MoralityPopOutFrame moralitySummaryFrame;

    public TopBarMorality(float _BaseScale, float frameHeight)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultXbuffer = uiScaleHelper.GetDefaultXBuffer();
      this.headerBase = new TopBarHeaderBase(this.BaseScale, frameHeight, uiScaleHelper.ScaleX(130f), true);
      Vector2 vector2 = -this.headerBase.GetSize() * 0.5f;
      float num1 = defaultXbuffer + vector2.X;
      if (false)
      {
        this.infoIcon = new LittleSummaryButton(LittleSummaryButtonType.BlueInfoCircle, _BaseScale: this.BaseScale);
        this.infoIcon.vLocation.X = num1 + this.infoIcon.GetSize().X * 0.5f;
        num1 += this.infoIcon.GetSize().X + defaultXbuffer;
      }
      this.moralityWord = new ZGenericText(SEngine.Localization.Localization.GetText(985) + ": ", this.BaseScale, false, _UseOnePointFiveFont: true);
      Vector2 size1 = this.moralityWord.GetSize();
      this.moralityWord.vLocation.Y -= size1.Y * 0.5f;
      this.moralityWord.vLocation.X = num1;
      float num2 = num1 + size1.X + defaultXbuffer * 0.5f;
      this.goodEvilIcon = new GoodEvilIcon(true, basescale_: this.BaseScale);
      this.goodEvilIcon.vLocation.X = num2;
      this.goodEvilIcon.SetDrawOriginToCentre();
      this.goodEvilIcon.vLocation.Y -= uiScaleHelper.ScaleY(1f);
      float num3 = num2 + this.goodEvilIcon.GetSize().X;
      this.moralityStatus = new ZGenericText("X", this.BaseScale, false, _UseOnePointFiveFont: true);
      Vector2 size2 = this.moralityStatus.GetSize();
      this.moralityStatus.vLocation.X = num3;
      this.moralityStatus.vLocation.Y -= size2.Y * 0.5f;
      float num4 = num3 + uiScaleHelper.ScaleX(20f);
    }

    public Vector2 GetSize() => this.headerBase.GetSize();

    public void SetMoralityValue(float value)
    {
      string empty = string.Empty;
      if ((double) value >= 0.0)
        this.goodEvilIcon.SetAlignment(true);
      else if ((double) value < 0.0)
        this.goodEvilIcon.SetAlignment(false);
      this.moralityStatus.textToWrite = MoralityData.GetDisplayStringForMoralityValue(value);
      if (this.moralitySummaryFrame == null)
        return;
      this.moralitySummaryFrame.NeedToRefreshValues = true;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.moralitySummaryFrame != null && this.moralitySummaryFrame.CheckMouseOver(player, offset);
    }

    private void OnClick_PopOutSummaryFrame(Player player)
    {
      if (this.moralitySummaryFrame == null)
      {
        this.moralitySummaryFrame = new MoralityPopOutFrame(player, this.BaseScale);
        this.moralitySummaryFrame.location.Y += TopBarManager.GetMiddleOfBar();
        this.moralitySummaryFrame.location.Y += this.moralitySummaryFrame.GetSize().Y * 0.5f;
        this.moralitySummaryFrame.location.X += (float) ((double) this.moralitySummaryFrame.GetSize().X * 0.5 - (double) this.GetSize().X * 0.5);
      }
      else
        this.moralitySummaryFrame.ToggleLerp();
    }

    private void UpdatePopOutSummaryFrame(Player player, float DeltaTime, Vector2 offset)
    {
      if (this.moralitySummaryFrame == null)
        return;
      this.moralitySummaryFrame.UpdatePopOutFrame(player, DeltaTime, offset);
      if (FeatureFlags.BlockAllUI)
      {
        this.moralitySummaryFrame.LerpOff();
      }
      else
      {
        if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || this.CheckMouseOver(player, offset))
          return;
        this.moralitySummaryFrame.LerpOff();
      }
    }

    public bool UpdateTopBarMorality(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset))
      {
        this.OnClick_PopOutSummaryFrame(player);
        return true;
      }
      this.UpdatePopOutSummaryFrame(player, DeltaTime, offset);
      return false;
    }

    public void PreDrawTopBarMorality(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.moralitySummaryFrame == null)
        return;
      this.moralitySummaryFrame.DrawPopOutFrame(offset, spriteBatch);
    }

    public void DrawTopBarMorality(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.moralityWord.DrawZGenericText(offset, spriteBatch);
      if (this.infoIcon != null)
        this.infoIcon.DrawLittleSummaryButton(offset, spriteBatch);
      this.moralityStatus.DrawZGenericText(offset, spriteBatch);
      this.goodEvilIcon.DrawGoodEvilIcon(offset, spriteBatch);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}
