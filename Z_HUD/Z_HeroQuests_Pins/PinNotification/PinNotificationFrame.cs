// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_HeroQuests_Pins.PinNotification.PinNotificationFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_HUD.Z_HeroQuests_Pins.PinNotification
{
  internal class PinNotificationFrame
  {
    public Vector2 location;
    public CustomerFrame customerFrame;
    private SatisfactionBar bar;
    private ZGenericText header;
    private SimpleTextHandler body;
    private ZGenericText progressText;
    private BackButton crossButton;
    private PinIcon icon;
    private float BaseScale;
    private Vector2 buffer;

    public PinNotificationFrame(
      string HeaderText,
      string BodyText,
      float _BaseScale,
      float barPercent,
      bool AllowCloseButton = true,
      OffscreenPointerType _offscreenpointertype = OffscreenPointerType.Count)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.buffer = uiScaleHelper.DefaultBuffer;
      float num = uiScaleHelper.ScaleX(160f);
      this.header = new ZGenericText(HeaderText, this.BaseScale, false, _UseOnePointFiveFont: true);
      this.header.SetAllColours(ColourData.Z_TextBrown);
      if (_offscreenpointertype != OffscreenPointerType.Count)
      {
        this.icon = new PinIcon(this.BaseScale, _offscreenpointertype);
      }
      else
      {
        this.bar = new SatisfactionBar(barPercent, this.BaseScale, BarSIze.LightNormal);
        this.bar.SetBarColours(ColourData.Z_BarYellow);
        this.progressText = new ZGenericText("000", this.BaseScale, false, _UseOnePointFiveFont: true);
        this.progressText.SetAllColours(ColourData.Z_TextBrown);
      }
      float width_ = num - this.buffer.X * 2f;
      if (this.icon != null)
      {
        width_ -= this.icon.GetSize().X + this.buffer.X;
        if (AllowCloseButton)
          width_ -= uiScaleHelper.ScaleX(15f);
      }
      this.body = new SimpleTextHandler(BodyText, width_, _Scale: this.BaseScale, AutoComplete: true);
      this.body.SetAllColours(ColourData.Z_TextBrown);
      Vector2 buffer = this.buffer;
      this.header.vLocation = buffer;
      buffer.Y += this.header.GetSize().Y;
      this.body.Location = buffer;
      buffer.Y += this.body.GetHeightOfParagraph();
      if (this.bar != null)
      {
        this.bar.vLocation = buffer;
        SatisfactionBar bar = this.bar;
        bar.vLocation = bar.vLocation + this.bar.GetSize() * 0.5f;
        this.progressText.vLocation = this.bar.vLocation;
        this.progressText.vLocation.Y -= this.progressText.GetSize().Y * 0.5f;
        this.progressText.vLocation.X += this.bar.GetSize().X * 0.5f;
        this.progressText.vLocation.X += this.buffer.X;
        buffer.Y += Math.Max(this.bar.GetSize().Y, this.progressText.GetSize().Y);
      }
      if (this.icon != null)
      {
        float val1 = Math.Max(this.header.GetSize().X, this.body.GetSize(true).X);
        if (this.bar != null)
          val1 = Math.Max(val1, this.bar.GetSize().X + this.progressText.GetSize().X);
        this.icon.location.X = val1 + buffer.X;
        this.icon.location.X += this.buffer.X * 0.5f;
        this.icon.location.Y = buffer.Y;
        this.icon.location.X += this.icon.GetSize().X * 0.5f;
        this.icon.location.Y -= this.icon.GetSize().Y * 0.5f;
      }
      buffer.Y += this.buffer.Y * 0.5f;
      buffer.X = num;
      this.customerFrame = new CustomerFrame(buffer, CustomerFrameColors.CreamWithBorder, this.BaseScale);
      Vector2 vector2 = -this.customerFrame.VSCale * 0.5f;
      ZGenericText header = this.header;
      header.vLocation = header.vLocation + vector2;
      this.body.Location += vector2;
      if (this.bar != null)
      {
        SatisfactionBar bar = this.bar;
        bar.vLocation = bar.vLocation + vector2;
      }
      if (this.progressText != null)
      {
        ZGenericText progressText = this.progressText;
        progressText.vLocation = progressText.vLocation + vector2;
      }
      if (this.icon != null)
        this.icon.location += vector2;
      if (!AllowCloseButton)
        return;
      this.CreateCloseButton();
    }

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public void CreateCloseButton()
    {
      this.crossButton = new BackButton(true, BaseScale: this.BaseScale, isSmallVersion: true);
      this.crossButton.vLocation.X = this.customerFrame.VSCale.X - this.buffer.X * 0.7f;
      this.crossButton.vLocation.Y = this.buffer.Y * 0.7f;
      this.crossButton.vLocation.X -= this.crossButton.GetSize().X * 0.5f;
      this.crossButton.vLocation.Y += this.crossButton.GetSize().Y * 0.5f;
      BackButton crossButton = this.crossButton;
      crossButton.vLocation = crossButton.vLocation + -this.customerFrame.VSCale * 0.5f;
      this.crossButton.OnlyAllowMouseClicks = true;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.customerFrame.CheckMouseOver(player, offset);
    }

    public void SetBarValues(float barPercent)
    {
      if (this.bar == null)
        return;
      barPercent = MathHelper.Clamp(barPercent, 0.0f, 1f);
      this.bar.SetFullness(barPercent);
      if ((double) barPercent >= 1.0)
        this.bar.SetBarColours(ColourData.Z_BarBabyGreen);
      this.progressText.textToWrite = Math.Round((double) barPercent * 100.0, 1).ToString() + "%";
    }

    public void SetHeaderText(string text) => this.header.textToWrite = text;

    public bool UpdatePinNotificationFrame(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out bool ClickedOnCross,
      bool BlockMouseOverMainPanel)
    {
      offset += this.location;
      ClickedOnCross = false;
      if (this.crossButton != null)
        ClickedOnCross = this.crossButton.UpdateBackButton(player, DeltaTime, offset);
      return !BlockMouseOverMainPanel && this.customerFrame.UpdateForMouseOverAndClick(player, DeltaTime, offset, out bool _);
    }

    public void DrawPinNotificationFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.header.DrawZGenericText(offset, spriteBatch);
      this.body.DrawSimpleTextHandler(offset, 1f, spriteBatch);
      if (this.bar != null)
        this.bar.DrawSatisfactionBar(offset, spriteBatch);
      if (this.progressText != null)
        this.progressText.DrawZGenericText(offset, spriteBatch);
      if (this.crossButton == null || !this.crossButton.MouseOver)
        this.customerFrame.PostDrawMouseoverOverlay(offset, spriteBatch);
      if (this.crossButton != null)
        this.crossButton.DrawBackButton(offset, spriteBatch);
      if (this.icon == null)
        return;
      this.icon.DrawPinIcon(offset, spriteBatch);
    }
  }
}
