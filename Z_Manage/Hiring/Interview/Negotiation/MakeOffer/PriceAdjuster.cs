// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer.PriceAdjuster
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;

namespace TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer
{
  internal class PriceAdjuster
  {
    private TextButton Plus;
    private TextButton Minus;
    private StringInBox stringinaboxx;
    private bool HoldingLeft;
    private bool HoldingRight;
    public Vector2 Location;
    private float PulseTimer;
    private bool IsFirstPule;
    public int CurrentValue;
    public int MaxValue;
    private int MinValue;
    private int Cycles;
    private bool WillDoSimpleRender;
    public GameObject SimleRendererTextColour;
    private float OverallMultiplier;
    private float BaseScale;
    private GameObject MinusBut;
    private GameObject PlusBut;
    private bool UseDecimal;
    private bool UsingSPringFont;
    public float FontScale = 1.5f;
    public string PreString = "$";
    public string EndString = "";
    public bool AddZero;
    private int Incrmemnt = 1;
    private bool isDisabled;
    private float EXSpread;
    private float SpaceBetweenPlusMinusToBar;
    private float stringInBoxBaseScaleMult = 2f;
    private List<string> ButtonNames;
    private bool NoButtons;

    public PriceAdjuster(
      int Min,
      int Max,
      int _CurrentValue = -1,
      bool _SimpleRenderer = false,
      float _OverallMultiplier = 1f,
      bool _UseDecimal = false,
      float _BaseScale = -1f)
    {
      this.UseDecimal = _UseDecimal;
      this.OverallMultiplier = _OverallMultiplier;
      this.BaseScale = _BaseScale;
      this.WillDoSimpleRender = _SimpleRenderer;
      this.MinValue = Min;
      this.MaxValue = Max;
      this.CurrentValue = Max - Min;
      this.CurrentValue /= 2;
      this.CurrentValue += Min;
      this.SpaceBetweenPlusMinusToBar = 5f * this.BaseScale;
      if ((double) this.BaseScale != -1.0)
      {
        this.Plus = new TextButton(this.BaseScale, "+", 12f);
        this.Minus = new TextButton(this.BaseScale, "+", 12f);
      }
      else
      {
        this.Plus = new TextButton("+", 12f, OverAllMultiplier: this.OverallMultiplier);
        this.Minus = new TextButton("-", 12f, OverAllMultiplier: this.OverallMultiplier);
        this.Plus.vLocation.X = 140f * this.OverallMultiplier;
        this.Minus.vLocation.X = -140f * this.OverallMultiplier;
      }
      if (_CurrentValue != -1)
        this.CurrentValue = _CurrentValue;
      this.CreateStringInABox("$" + (object) this.CurrentValue, 50f);
      if (this.WillDoSimpleRender)
      {
        double baseScale = (double) this.BaseScale;
        this.Plus = new TextButton("+", 20f * this.BaseScale, OverAllMultiplier: this.BaseScale);
        this.Minus = new TextButton("-", 20f * this.BaseScale, OverAllMultiplier: this.BaseScale);
        this.Plus.vLocation.X = 40f * this.BaseScale;
        this.Minus.vLocation.X = -40f * this.BaseScale;
      }
      this.MinusBut = new GameObject();
      this.PlusBut = new GameObject();
      if ((double) this.BaseScale != -1.0)
      {
        this.PlusBut.scale = this.BaseScale;
        this.MinusBut.scale = this.BaseScale;
      }
      else
      {
        this.PlusBut.scale = 2f * this.OverallMultiplier;
        this.MinusBut.scale = 2f * this.OverallMultiplier;
      }
      if (this.WillDoSimpleRender)
      {
        this.PlusBut.vLocation.X += this.BaseScale * 6f;
        this.Minus.vLocation.X -= this.BaseScale * 6f;
      }
      this.SetNormalGreen();
      this.MinusBut.SetDrawOriginToCentre();
      this.PlusBut.SetDrawOriginToCentre();
      if (!this.WillDoSimpleRender)
        return;
      this.SetSImpleRendererTextColur(Color.Black.ToVector3());
    }

    public void SpreadButtons(float Distance_IncludingBaseScale)
    {
      this.EXSpread = Distance_IncludingBaseScale;
      this.MinusBut.vLocation.X -= Distance_IncludingBaseScale;
      this.Plus.vLocation.X += Distance_IncludingBaseScale;
    }

    private void CreateStringInABox(string text, float Length)
    {
      if ((double) this.BaseScale != -1.0)
      {
        this.stringinaboxx = new StringInBox(this.BaseScale * this.stringInBoxBaseScaleMult, text, Length / this.stringInBoxBaseScaleMult, true);
        float num = (float) ((double) this.stringinaboxx.GetVScale_Depricated().X * 0.5 + (double) this.Plus.GetSize_True().X * 0.5) + this.SpaceBetweenPlusMinusToBar;
        this.Plus.vLocation.X = num + this.EXSpread;
        this.Minus.vLocation.X = (float) -((double) num + (double) this.EXSpread);
        this.stringinaboxx.SetAsButtonFrame(BTNColour.Green, this.BaseScale);
      }
      else
      {
        this.stringinaboxx = new StringInBox(text, 4f * this.OverallMultiplier, Length, true);
        this.Plus.vLocation.X += Length * 0.5f;
        this.Minus.vLocation.X -= Length * 0.5f;
        this.stringinaboxx.SetAsButtonFrame(BTNColour.Green);
      }
    }

    public void SetToString(string NewString, float Length, bool IsCreate = false)
    {
      if (IsCreate)
        this.CreateStringInABox(NewString, Length);
      else
        this.stringinaboxx.SetText(NewString);
    }

    public void SetIncrmemnt(int _Incrmemnt) => this.Incrmemnt = _Incrmemnt;

    public void SetAsStrings(float ButtonMiddleLegth, List<string> _ButtonNames)
    {
      if ((double) this.BaseScale != -1.0)
        throw new Exception("Have not fixed this function for basescale application yet");
      this.ButtonNames = _ButtonNames;
      this.stringinaboxx.VScale.X = ButtonMiddleLegth * this.OverallMultiplier;
      float num = (float) ((double) ButtonMiddleLegth * 0.5 + 20.0);
      this.Plus.vLocation = new Vector2(num * this.OverallMultiplier + this.EXSpread, 0.0f);
      this.Minus.vLocation = new Vector2(num * -this.OverallMultiplier, 0.0f);
      this.Minus.vLocation.X -= this.EXSpread;
    }

    public void SetToSpringFont()
    {
      this.FontScale = RenderMath.GetPixelSizeBestMatch(4f);
      this.UsingSPringFont = true;
    }

    public void SetSImpleRendererTextColur(Vector3 CLR)
    {
      this.SimleRendererTextColour = new GameObject();
      this.SimleRendererTextColour.SetAllColours(CLR);
    }

    public void SetCost(int COST) => this.CurrentValue = COST;

    public void SetDisabled(bool _isDisabled)
    {
      this.isDisabled = _isDisabled;
      if (this.isDisabled)
        this.SetGrey();
      else
        this.SetNormalGreen();
    }

    private void SetGrey()
    {
      this.MinusBut.DrawRect = new Rectangle(763, 120, 22, 22);
      this.PlusBut.DrawRect = new Rectangle(786, 120, 22, 22);
      this.stringinaboxx.SetAsButtonFrame(BTNColour.Grey, this.BaseScale);
    }

    private void SetNormalGreen()
    {
      this.MinusBut.DrawRect = new Rectangle(897, 327, 22, 22);
      this.PlusBut.DrawRect = new Rectangle(920, 327, 22, 22);
      this.stringinaboxx.SetAsButtonFrame(BTNColour.Green, this.BaseScale);
    }

    public Vector2 GetSize()
    {
      double num1 = (double) this.MinusBut.DrawRect.Width * (double) this.MinusBut.scale;
      float num2 = this.Plus.vLocation.X - this.Minus.vLocation.X;
      float y = this.stringinaboxx.GetVScale_Depricated().Y * Sengine.ScreenRatioUpwardsMultiplier.Y;
      double num3 = (double) num2;
      return new Vector2((float) (num1 + num3), y);
    }

    public bool UpdatePriceAdjuster(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      bool ForceLeft = false,
      bool ForceRight = false)
    {
      if (this.isDisabled || this.NoButtons)
        return false;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      Offset += this.Location;
      if (MathStuff.CheckPointCollision(true, this.Plus.vLocation + Offset + new Vector2(this.EXSpread, 0.0f), this.MinusBut.scale, (float) this.MinusBut.DrawRect.Width, (float) this.MinusBut.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]) | ForceRight && (!DebugFlags.IsPCVersion || player.inputmap.LeftMouseHeld))
        flag3 = true;
      if (MathStuff.CheckPointCollision(true, this.Minus.vLocation + Offset + new Vector2(-this.EXSpread, 0.0f), this.MinusBut.scale, (float) this.MinusBut.DrawRect.Width, (float) this.MinusBut.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.player.touchinput.MultiTouchTouchLocations[0]) | ForceLeft && (!DebugFlags.IsPCVersion || player.inputmap.LeftMouseHeld))
        flag2 = true;
      if (!flag2 && !flag3)
      {
        this.HoldingLeft = false;
        this.HoldingRight = false;
        this.PulseTimer = 0.0f;
        this.IsFirstPule = true;
      }
      else if (flag2 && !flag3)
      {
        this.HoldingRight = false;
        if (!this.HoldingLeft)
        {
          this.Cycles = 0;
          this.HoldingLeft = true;
          this.PulseTimer = 0.0f;
          this.IsFirstPule = true;
          if (this.Subtract())
            flag1 = true;
        }
        else
        {
          this.PulseTimer += DeltaTime;
          if (this.IsFirstPule)
          {
            if ((double) this.PulseTimer > 0.200000002980232)
            {
              this.IsFirstPule = false;
              if (this.Subtract())
                flag1 = true;
            }
          }
          else if ((double) this.PulseTimer > 0.0500000007450581 && this.Cycles < 15)
          {
            this.PulseTimer = 0.0f;
            if (this.Subtract())
              flag1 = true;
          }
          else if ((double) this.PulseTimer > 0.00999999977648258 && this.Cycles >= 15)
          {
            this.PulseTimer = 0.0f;
            if (this.Subtract())
              flag1 = true;
          }
        }
      }
      else if (flag3 && !flag2)
      {
        this.HoldingLeft = false;
        if (!this.HoldingRight)
        {
          this.Cycles = 0;
          this.HoldingRight = true;
          this.PulseTimer = 0.0f;
          this.IsFirstPule = true;
          if (this.Add())
            flag1 = true;
        }
        else
        {
          this.PulseTimer += DeltaTime;
          if (this.IsFirstPule)
          {
            if ((double) this.PulseTimer > 0.200000002980232)
            {
              this.IsFirstPule = false;
              if (this.Add())
                flag1 = true;
            }
          }
          else if ((double) this.PulseTimer > 0.0500000007450581 && this.Cycles < 15)
          {
            this.PulseTimer = 0.0f;
            if (this.Add())
              flag1 = true;
          }
          else if ((double) this.PulseTimer > 0.00999999977648258 && this.Cycles >= 15)
          {
            this.PulseTimer = 0.0f;
            if (this.Add())
              flag1 = true;
          }
        }
      }
      return flag1;
    }

    private bool Add()
    {
      ++this.Cycles;
      this.PulseTimer = 0.0f;
      if (this.CurrentValue < this.MaxValue)
      {
        this.CurrentValue += this.Incrmemnt;
        this.stringinaboxx.SetText("$" + (object) this.CurrentValue);
        this.Minus.stringinabox.SetAsButtonFrame(BTNColour.Green);
        this.MinusBut.SetAlpha(1f);
        return true;
      }
      this.Plus.stringinabox.SetAsButtonFrame(BTNColour.Grey);
      this.PlusBut.SetAlpha(0.5f);
      return false;
    }

    public void UnsetMax()
    {
      if ((double) this.PlusBut.fAlpha == 1.0)
        return;
      this.Plus.stringinabox.SetAsButtonFrame(BTNColour.Green);
      this.PlusBut.SetAlpha(1f);
    }

    public void LockToMax()
    {
      this.MaxValue = this.CurrentValue;
      this.Plus.stringinabox.SetAsButtonFrame(BTNColour.Grey);
      this.PlusBut.SetAlpha(0.5f);
    }

    private bool Subtract()
    {
      ++this.Cycles;
      this.PulseTimer = 0.0f;
      if (this.CurrentValue > this.MinValue)
      {
        this.Plus.stringinabox.SetAsButtonFrame(BTNColour.Green);
        this.CurrentValue -= this.Incrmemnt;
        this.stringinaboxx.SetText("$" + (object) this.CurrentValue);
        this.PlusBut.SetAlpha(1f);
        return true;
      }
      this.Minus.stringinabox.SetAsButtonFrame(BTNColour.Grey);
      this.MinusBut.SetAlpha(0.5f);
      return false;
    }

    public void RemoveButtons() => this.NoButtons = true;

    public void DrawPriceAdjuster(Vector2 Offset) => this.DrawPriceAdjuster(Offset, AssetContainer.pointspritebatchTop05);

    public void DrawPriceAdjuster(Vector2 Offset, SpriteBatch spritebatch)
    {
      string str1 = "";
      if (this.AddZero)
        str1 = "0";
      Offset += this.Location;
      if (!this.NoButtons)
      {
        this.PlusBut.Draw(spritebatch, AssetContainer.SpriteSheet, this.Plus.vLocation + Offset);
        this.MinusBut.Draw(spritebatch, AssetContainer.SpriteSheet, this.Minus.vLocation + Offset);
      }
      if (this.WillDoSimpleRender)
      {
        this.PreString + (object) this.CurrentValue + str1 + this.EndString;
        if (this.ButtonNames != null)
        {
          string buttonName = this.ButtonNames[this.CurrentValue];
        }
        if (this.UseDecimal)
        {
          string str2 = string.Concat((object) Math.Round((double) this.CurrentValue * 0.100000001490116, 1));
          if (this.CurrentValue % 10 == 0)
            str2 += ".0";
          if (this.UsingSPringFont)
            TextFunctions.DrawJustifiedText(this.PreString + str2 + this.EndString, this.BaseScale, Offset + new Vector2(0.0f, 10f * this.OverallMultiplier), this.SimleRendererTextColour.GetColour(), 1f, AssetContainer.springFont, spritebatch);
          else
            TextFunctions.DrawJustifiedText(this.PreString + str2 + str1 + this.EndString, this.BaseScale, Offset + new Vector2(0.0f, 10f * this.OverallMultiplier), this.SimleRendererTextColour.GetColour(), 1f, AssetContainer.roundaboutFont, spritebatch);
        }
        else
          TextFunctions.DrawJustifiedText(this.PreString + (object) this.CurrentValue + str1 + this.EndString, this.BaseScale, Offset + new Vector2(0.0f, 10f * this.OverallMultiplier), this.SimleRendererTextColour.GetColour(), 1f, AssetContainer.roundaboutFont, spritebatch);
      }
      else
        this.stringinaboxx.DrawStringInBox(Offset, spritebatch);
    }
  }
}
