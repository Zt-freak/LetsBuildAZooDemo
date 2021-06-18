// Decompiled with JetBrains decompiler
// Type: TinyZoo.GenericUI.UXPanels.DollarPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using SEngine.Text;
using System;
using TinyZoo.Z_HUD.TopBar;
using TinyZoo.Z_HUD.TopBar.Elements.Money;

namespace TinyZoo.GenericUI.UXPanels
{
  internal class DollarPanel
  {
    private UXFrame uxframe;
    private GameObject Coin;
    private int CASHLastFrame;
    private string CASHLastFrameString = "";
    public Vector2 Location;
    private GameObject textthing;
    private GameObject TextThing2;
    private string Bonus;
    private string AppendText = "";
    private GameObject BG;
    private Vector2 VSCALE;
    private StringInBox stringinabox;
    private bool IsBuy;
    private bool CanAfford;
    private GameObjectNineSlice CashFrane;
    private GameObjectNineSlice CashFraneOVER;
    private bool UseNewUI;
    private TopBarMoney topBarMoney_NewUI;
    private LerpHandler_Float lerper;
    private bool JustDollars;
    public static int DollarTollerance = 10000;

    public DollarPanel(Player player, bool SmallFrame = true)
    {
      this.SetUp(player);
      this.Coin = new GameObject();
      this.Coin.scale = RenderMath.GetPixelSizeBestMatch(2f);
      this.Coin.DrawRect = new Rectangle(138, 0, 7, 7);
      this.Coin.SetDrawOriginToCentre();
      float pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(2f);
      if (DebugFlags.IsPCVersion)
      {
        pixelSizeBestMatch = RenderMath.GetPixelSizeBestMatch(1.5f);
        this.Coin.scale = pixelSizeBestMatch;
      }
      this.uxframe = new UXFrame(pixelSizeBestMatch, SmallFrame);
      this.Bonus = "";
      this.textthing = new GameObject();
      this.textthing.SetAllColours(ColourData.TanGoldText);
      this.textthing.scale = RenderMath.GetPixelSizeBestMatch(pixelSizeBestMatch);
      this.stringinabox = new StringInBox("$", 2f, 100f);
      this.stringinabox.SetLemonANdBlue();
      this.TextThing2 = new GameObject(this.textthing);
      this.textthing.SetAllColours(ColourData.FernLemon);
      this.BG = new GameObject();
      this.BG.SetAllColours(ColourData.FernDarkBlue);
      this.BG.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BG.SetDrawOriginToCentre();
      this.VSCALE = new Vector2(300f, 50f);
      Vector3 SecondaryColour;
      this.CashFrane = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour), 7);
      this.CashFrane.scale = RenderMath.GetPixelSizeBestMatch(1f);
      this.textthing.SetAllColours(SecondaryColour);
      this.CashFraneOVER = new GameObjectNineSlice(StringInBox.GetFrameColourRect(BTNColour.Green, out SecondaryColour), 7);
      this.CashFraneOVER.scale = RenderMath.GetPixelSizeBestMatch(1f);
    }

    public DollarPanel(Player player, float BaseScale, float FrameHeight)
    {
      this.UseNewUI = true;
      this.SetUp(player);
      this.topBarMoney_NewUI = new TopBarMoney(BaseScale, FrameHeight);
    }

    private void SetUp(Player player)
    {
      this.ForceCost(player.Stats.GetCashHeld());
      this.lerper = new LerpHandler_Float();
    }

    public Vector2 GetSize()
    {
      if (this.UseNewUI)
        return this.topBarMoney_NewUI.GetSize();
      throw new Exception("Not coded");
    }

    public void AddAppendText(string _AppendText) => this.AppendText = _AppendText;

    public void ForceCost(int Cst)
    {
      this.CASHLastFrame = Cst;
      this.JustDollars = this.CASHLastFrame >= 100000;
      this.CASHLastFrameString = StringUtils.AddCommasToANumber(this.CASHLastFrame);
    }

    public void SetBuy()
    {
      this.CanAfford = true;
      this.IsBuy = true;
      if (this.UseNewUI)
        return;
      this.uxframe.SetAllColours(0.2f, 0.8f, 0.2f);
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

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.topBarMoney_NewUI.CheckMouseOver(player, offset);
    }

    public void UpdateDollarPanel(float DeltaTime, Player player) => this.UpdateDollarPanel(DeltaTime, player, Vector2.Zero);

    public void UpdateDollarPanel(float DeltaTime, Player player, Vector2 offset)
    {
      offset += this.Location;
      if (FeatureFlags.BlockAllUI || FeatureFlags.BlockCash)
        this.LerpOff();
      else
        this.LerpIn();
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (!this.UseNewUI)
        this.uxframe.UpdateUXFrame(DeltaTime);
      if (this.IsBuy)
      {
        if (this.CanAfford && this.CASHLastFrame > player.Stats.GetCashHeld())
        {
          this.CanAfford = false;
          if (!this.UseNewUI)
            this.uxframe.SetAllColours(0.8f, 0.2f, 0.2f);
        }
        else if (!this.CanAfford && this.CASHLastFrame <= player.Stats.GetCashHeld() && !this.UseNewUI)
          this.uxframe.SetAllColours(0.2f, 0.8f, 0.2f);
      }
      else
      {
        if (player.Stats.HitCashCap)
        {
          this.Bonus = SEngine.Localization.Localization.GetText(63);
          if (!this.UseNewUI)
          {
            this.uxframe.SetPrimaryColours(0.5f, new Vector3(0.3f, 0.0f, 0.0f));
            this.TextThing2.SetAllColours(0.8f, 0.2f, 0.2f);
            player.Stats.HitCashCap = false;
            this.TextThing2.SetAlpha(false, 0.5f, 1f, 0.0f);
            this.TextThing2.SetColourDelay(1f);
            this.uxframe.DoFlash();
            this.textthing.SetPrimaryColours(0.5f, new Vector3(1f, 0.0f, 0.0f));
            this.Coin.SetPrimaryColours(0.5f, new Vector3(1f, 0.0f, 0.0f));
          }
        }
        if (!this.UseNewUI)
        {
          this.Coin.UpdateColours(DeltaTime);
          this.textthing.UpdateColours(DeltaTime);
          this.uxframe.UpdateColours(DeltaTime);
        }
        int num1 = player.Stats.GetCashHeld();
        bool flag = false;
        if (num1 >= DollarPanel.DollarTollerance)
        {
          if (!this.JustDollars)
            flag = true;
        }
        else
        {
          num1 = player.Stats.GetCashHeldAsCents();
          if (this.JustDollars)
            flag = true;
        }
        if (this.CASHLastFrame != num1 | flag)
        {
          this.CASHLastFrame = num1;
          if (this.CASHLastFrame >= DollarPanel.DollarTollerance)
          {
            if (!this.JustDollars)
              this.JustDollars = true;
          }
          else if (this.JustDollars)
          {
            this.CASHLastFrame = player.Stats.GetCashHeldAsCents();
            this.JustDollars = false;
          }
          int num2 = !this.JustDollars ? player.Stats.GetCashHeldAsCents() - this.CASHLastFrame : player.Stats.GetCashHeld() - this.CASHLastFrame;
          int num3 = this.JustDollars ? 1 : 0;
          if (!this.UseNewUI)
          {
            if (num2 > 0)
            {
              this.Bonus = "+" + (object) num2;
              this.TextThing2.SetAllColours(0.2f, 1f, 0.2f);
            }
            else
            {
              this.Bonus = string.Concat((object) num2);
              this.TextThing2.SetAllColours(1f, 0.2f, 0.2f);
            }
            this.TextThing2.SetAlpha(false, 0.5f, 1f, 0.0f);
            this.TextThing2.SetColourDelay(1f);
            this.uxframe.DoFlash();
          }
          this.ForceCost(this.CASHLastFrame);
        }
        if (!this.UseNewUI)
          this.TextThing2.UpdateColours(DeltaTime);
      }
      if (!this.UseNewUI)
        return;
      this.topBarMoney_NewUI.UpdateTopBarMoney(player, DeltaTime, offset);
      this.topBarMoney_NewUI.SetMoneyValue(this.CASHLastFrame, this.JustDollars);
    }

    public void DrawDollarPanel(Vector2 Offset, bool ForceLocation = true) => this.DrawDollarPanel(Offset, AssetContainer.pointspritebatch03, ForceLocation);

    public void PreDrawDollarPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (!this.UseNewUI)
        return;
      this.topBarMoney_NewUI.PreDrawTopBarMoney(offset, spriteBatch);
    }

    public void DrawDollarPanel(Vector2 Offset, SpriteBatch spriteBatch, bool ForceLocation = true)
    {
      if (ForceLocation)
        this.Location = new Vector2((float) (876 - this.uxframe.DrawRect.Width), (float) ((double) this.uxframe.DrawRect.Height * (double) this.uxframe.scale * 0.5) * Sengine.ScreenRatioUpwardsMultiplier.Y);
      Offset += this.Location;
      Offset.Y += this.lerper.Value * TopBarManager.TopBarLerpDistance;
      if (this.UseNewUI)
      {
        this.topBarMoney_NewUI.DrawTopBarMoney(Offset, spriteBatch);
      }
      else
      {
        this.CashFrane.DrawGameObjectNineSlice(spriteBatch, AssetContainer.SpriteSheet, Offset, new Vector2(120f, 30f));
        if ((double) this.uxframe.BrightFrame.fAlpha > 0.0)
        {
          this.CashFraneOVER.fAlpha = this.uxframe.BrightFrame.fAlpha;
          this.CashFraneOVER.DrawGameObjectNineSlice(spriteBatch, AssetContainer.SpriteSheet, Offset, new Vector2(120f, 30f));
        }
        this.VSCALE = new Vector2(150f, 30f);
        this.Coin.vLocation.X = (float) ((double) (-this.uxframe.DrawRect.Width / 2) * (double) this.uxframe.scale + 18.0);
        this.Coin.Draw(spriteBatch, AssetContainer.SpriteSheet, Offset);
        this.textthing.vLocation = new Vector2(this.Coin.vLocation.X + 10f, -4f * this.uxframe.scale * Sengine.ScreenRatioUpwardsMultiplier.Y);
        TextFunctions.DrawTextWithDropShadow(this.CASHLastFrameString + this.AppendText, this.textthing.scale, Offset + this.textthing.vLocation, this.textthing.GetColour(), 1f, AssetContainer.springFont, spriteBatch, false);
        this.TextThing2.vLocation = new Vector2(30f * this.uxframe.scale, -5f * this.uxframe.scale);
        TextFunctions.DrawTextWithDropShadow(this.Bonus, this.TextThing2.scale, Offset + this.TextThing2.vLocation + new Vector2(20f, 3f), this.TextThing2.GetColour(), this.TextThing2.fAlpha, AssetContainer.springFont, spriteBatch, false, false);
        this.uxframe.FlashDraw(Offset);
      }
    }
  }
}
