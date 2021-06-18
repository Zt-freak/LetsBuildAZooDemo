// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Money.TopBarMoney
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.TopBar.Elements.Money
{
  internal class TopBarMoney
  {
    public Vector2 location;
    private TopBarHeaderBase headerBase;
    private ZGenericText moneyText;
    private ZGenericText appendText;
    private UIScaleHelper scaleHelper;
    private float Xbuffer;
    private float appendTime;
    private LerpHandler_Float appendAlphaLerper;
    private LerpHandler_Float numberLerper;
    private int lastMoneyValue;
    private int LastValue;
    private bool JustDollars;
    private MoneyPopOut popOut;
    private float BaseScale;

    public TopBarMoney(float _BaseScale, float FrameHeight)
    {
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Xbuffer = this.scaleHelper.GetDefaultXBuffer();
      this.lastMoneyValue = -1;
      this.headerBase = new TopBarHeaderBase(this.BaseScale, FrameHeight, this.scaleHelper.ScaleX(110f), true);
      this.moneyText = new ZGenericText("X", this.BaseScale, _UseOnePointFiveFont: true);
      this.appendText = new ZGenericText("X", this.BaseScale, false);
      this.appendText.vLocation.Y = this.moneyText.GetSize().Y * 0.5f - this.appendText.GetSize().Y;
      this.appendAlphaLerper = new LerpHandler_Float();
      this.appendTime = 3f;
      this.numberLerper = new LerpHandler_Float();
    }

    public Vector2 GetSize() => this.headerBase.GetSize();

    public void SetMoneyValue(int newValue, bool _JustDollars)
    {
      if (this.lastMoneyValue != -1 || this.JustDollars != _JustDollars)
      {
        if (this.lastMoneyValue != newValue)
        {
          this.numberLerper.SetLerp(true, (float) this.lastMoneyValue, (float) newValue, 3f);
          this.GotOrUsedMoney(newValue - this.lastMoneyValue, newValue - this.lastMoneyValue > DollarPanel.DollarTollerance);
        }
        if (this.JustDollars != _JustDollars)
          this.numberLerper.SetLerp(true, (float) newValue, (float) newValue, 3f);
      }
      else
        this.numberLerper.Value = (float) newValue;
      this.JustDollars = _JustDollars;
      this.lastMoneyValue = newValue;
    }

    private void GotOrUsedMoney(int difference, bool JustDollars)
    {
      if (difference == 0)
        return;
      string str1 = string.Empty;
      if (difference > 0)
      {
        str1 = "+";
        this.headerBase.DoFlash(true);
        this.appendText.SetAllColours(new Vector3(0.2f, 1f, 0.2f));
      }
      else
      {
        this.headerBase.DoFlash(false);
        this.appendText.SetAllColours(new Vector3(1f, 0.2f, 0.2f));
      }
      string str2 = !JustDollars ? str1 + Z_GameFlags.GetCostAsDOllarsAndCentsFromInt(difference) : str1 + (object) (difference / 100);
      this.appendText.vLocation.X = this.moneyText.GetSize().X * 0.5f + this.Xbuffer;
      this.appendText.textToWrite = str2;
      this.appendAlphaLerper.SetLerp(true, 1f, 0.0f, 1f / this.appendTime, true);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.popOut != null && this.popOut.CheckMouseOver(player, offset);
    }

    public void UpdateTopBarMoney(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.appendAlphaLerper.UpdateLerpHandler(DeltaTime);
      this.numberLerper.UpdateLerpHandler(DeltaTime);
      int num = (int) Math.Round((double) this.numberLerper.Value);
      if (this.LastValue != num)
      {
        this.LastValue = num;
        this.moneyText.textToWrite = !this.JustDollars ? "$" + Z_GameFlags.GetCostAsDOllarsAndCentsFromInt(this.LastValue) : "$" + (object) (this.LastValue / 100);
      }
      if (this.headerBase.UpdateTopBarHeaderBase(player, DeltaTime, offset))
      {
        this.OnClick_PopOutMoneyFrame(player, offset);
      }
      else
      {
        if (this.popOut == null)
          return;
        this.popOut.UpdateMoneyPopOut(player, DeltaTime, offset);
        if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && !this.popOut.CheckMouseOver(player, offset))
          this.popOut.LerpOff();
        if (!FeatureFlags.BlockAllUI)
          return;
        this.popOut.LerpOff();
      }
    }

    private void OnClick_PopOutMoneyFrame(Player player, Vector2 offset)
    {
      if (this.popOut == null || this.popOut.IsOffScreen())
      {
        this.popOut = new MoneyPopOut(this.BaseScale, player);
        this.popOut.location.Y += TopBarManager.GetMiddleOfBar();
        this.popOut.location.Y += this.popOut.GetSize().Y * 0.5f;
        float num = (float) ((double) this.popOut.GetSize().X * 0.5 - (double) this.GetSize().X * 0.5);
        if ((double) this.popOut.location.X + (double) offset.X + (double) num + (double) this.popOut.GetSize().X * 0.5 < 1024.0)
          this.popOut.location.X += num;
        else
          this.popOut.location.X -= num;
      }
      else
        this.popOut.ToggleLerp();
    }

    public void PreDrawTopBarMoney(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.popOut == null)
        return;
      this.popOut.DrawMoneyPopOut(offset, spriteBatch);
    }

    public void DrawTopBarMoney(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerBase.DrawTopBarHeaderBase(offset, spriteBatch);
      this.moneyText.DrawZGenericText(offset, spriteBatch);
      this.appendText.DrawZGenericText(offset, spriteBatch, this.appendAlphaLerper.Value);
      this.headerBase.PostDrawTopBarHeaderBase(offset, spriteBatch);
    }
  }
}
