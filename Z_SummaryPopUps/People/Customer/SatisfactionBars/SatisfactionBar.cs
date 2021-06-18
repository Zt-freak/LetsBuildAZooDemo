// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars.SatisfactionBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars
{
  internal class SatisfactionBar : GameObject
  {
    private static Rectangle verySmallBarRect = new Rectangle(649, 36, 16, 4);
    private static Rectangle verySmallFillRect = new Rectangle(666, 36, 16, 4);
    private static Rectangle normalBarRect = new Rectangle(918, 235, 64, 10);
    private static Rectangle normalFillRect = new Rectangle(918, 224, 64, 10);
    private static Rectangle thinBarRect = new Rectangle(170, 358, 64, 6);
    private static Rectangle thinFillRect = new Rectangle(367, 375, 64, 6);
    private GameObject Fullness;
    private GameObject Fullness2;
    private GameObject Fullness3;
    private List<float> fullnessvalues;
    private bool MouseOver;
    private GameObject MouseOverObject;
    private float SpecialInnerSize;
    private float deltaval;
    private float val;

    public SatisfactionBar(float FullPercent, float _BaseScale = 1f, BarSIze barsize = BarSIze.Normal)
    {
      this.fullnessvalues = new List<float>();
      for (int index = 0; index < 3; ++index)
        this.fullnessvalues.Add(0.0f);
      FullPercent = MathHelper.Clamp(FullPercent, 0.0f, 1f);
      this.Fullness = new GameObject();
      this.Fullness.scale = _BaseScale;
      this.fullnessvalues[0] = FullPercent;
      this.val = FullPercent;
      switch (barsize)
      {
        case BarSIze.Normal:
          this.DrawRect = SatisfactionBar.normalBarRect;
          this.Fullness.DrawRect = SatisfactionBar.normalFillRect;
          this.SpecialInnerSize = 62f;
          break;
        case BarSIze.VerySmall:
          this.DrawRect = SatisfactionBar.verySmallBarRect;
          this.Fullness.DrawRect = SatisfactionBar.verySmallFillRect;
          this.SpecialInnerSize = 14f;
          break;
        case BarSIze.Thin:
          this.DrawRect = SatisfactionBar.thinBarRect;
          this.Fullness.DrawRect = SatisfactionBar.thinFillRect;
          this.SpecialInnerSize = 61f;
          break;
        case BarSIze.LightNormal:
          this.DrawRect = new Rectangle(413, 695, 64, 10);
          this.Fullness.DrawRect = SatisfactionBar.normalFillRect;
          this.SpecialInnerSize = 62f;
          break;
      }
      this.SetDrawOriginToCentre();
      this.Fullness.SetDrawOriginToCentre();
      this.scale = _BaseScale;
      this.Fullness.DrawRect.Width = 1 + (int) ((double) this.fullnessvalues[0] * (double) this.SpecialInnerSize);
      this.Fullness.SetAllColours(0.01568628f, 0.7294118f, 0.4705882f);
      this.MouseOverObject = new GameObject((GameObject) this);
      this.MouseOverObject.DrawRect = new Rectangle(234, 358, 64, 6);
      this.MouseOverObject.SetAlpha(0.6f);
      this.MouseOverObject.vLocation = Vector2.Zero;
    }

    public void SetBarColours(Vector3 SetColour, int Layer = 0)
    {
      switch (Layer)
      {
        case 0:
          this.Fullness.SetAllColours(SetColour);
          break;
        case 1:
          if (this.Fullness2 == null)
            break;
          this.Fullness2.SetAllColours(SetColour);
          break;
        default:
          if (this.Fullness3 == null)
            break;
          this.Fullness3.SetAllColours(SetColour);
          break;
      }
    }

    public void PreviewChange(float newvalue, Vector3 positiveColour, Vector3 negativeColour)
    {
      newvalue = MathHelper.Clamp(newvalue, 0.0f, 1f);
      this.deltaval = newvalue - this.val;
      this.deltaval = MathHelper.Clamp(this.deltaval, -1f, 1f);
      if ((double) this.deltaval >= 0.0)
      {
        this.SetFullness(this.val);
        this.SetFullness(newvalue, 1);
        this.SetBarColours(positiveColour, 1);
      }
      else
      {
        this.SetFullness(newvalue);
        this.SetFullness(this.val, 1);
        this.SetBarColours(negativeColour, 1);
      }
    }

    public void ApplyChange()
    {
      double val = (double) this.val;
      this.val = MathHelper.Clamp(this.val + this.deltaval, 0.0f, 1f);
      this.SetFullness(this.val);
      this.SetFullness(0.0f, 1);
    }

    public float Value
    {
      get => this.val;
      set => this.val = value;
    }

    public void SetFullness(float _Fullness, int Layer = 0, bool DoSetColorBasedOnValue = false)
    {
      _Fullness = MathHelper.Clamp(_Fullness, 0.0f, 1f);
      this.fullnessvalues[Layer] = _Fullness;
      if (DoSetColorBasedOnValue)
        this.SetColorBasedOnValue(this.fullnessvalues[Layer], Layer);
      switch (Layer)
      {
        case 0:
          this.Fullness.DrawRect.Width = 1 + (int) ((double) this.fullnessvalues[Layer] * (double) this.SpecialInnerSize);
          break;
        case 1:
          if (this.Fullness2 == null)
          {
            this.AddNewBar(this.fullnessvalues[Layer], ColourData.Z_Cream, Layer);
            break;
          }
          this.Fullness2.DrawRect.Width = 1 + (int) ((double) this.fullnessvalues[Layer] * (double) this.SpecialInnerSize);
          break;
        case 2:
          if (this.Fullness3 == null)
          {
            this.AddNewBar(this.fullnessvalues[Layer], ColourData.Z_Cream, Layer);
            break;
          }
          this.Fullness3.DrawRect.Width = 1 + (int) ((double) this.fullnessvalues[Layer] * (double) this.SpecialInnerSize);
          break;
      }
    }

    public float GetFullness(int layer = 0) => this.fullnessvalues[layer];

    private void SetColorBasedOnValue(float _Fullness, int Layer)
    {
      if ((double) _Fullness > 0.600000023841858)
        this.SetBarColours(ColourData.Z_BarBabyGreen, Layer);
      else if ((double) _Fullness > 0.300000011920929)
        this.SetBarColours(ColourData.Z_BarYellow, Layer);
      else
        this.SetBarColours(ColourData.Z_BarRed, Layer);
    }

    public void Darken()
    {
      this.SetAllColours(Color.Gray.ToVector3());
      this.SetBarColours(Color.Gray.ToVector3());
    }

    public void AddNewBar(float Percentage, Vector3 Colour, int BarIndex)
    {
      if (this.Fullness2 == null && BarIndex == 1)
      {
        this.Fullness2 = new GameObject(this.Fullness);
        this.Fullness2.scale = this.scale;
        Percentage *= this.SpecialInnerSize;
        this.Fullness2.DrawRect.Width = 1 + (int) Percentage;
        this.Fullness2.SetAllColours(Colour);
      }
      else
      {
        this.Fullness3 = this.Fullness3 == null && BarIndex == 2 ? new GameObject(this.Fullness) : throw new Exception("Time to make this a list you lazy bugger");
        this.Fullness3.scale = this.scale;
        Percentage *= this.SpecialInnerSize;
        this.Fullness3.DrawRect.Width = 1 + (int) Percentage;
        this.Fullness3.SetAllColours(Colour);
      }
    }

    public void SetScale(float _Scale)
    {
      this.scale = _Scale;
      this.Fullness.scale = _Scale;
      if (this.Fullness2 != null)
        this.Fullness2.scale = _Scale;
      if (this.Fullness3 == null)
        return;
      this.Fullness3.scale = _Scale;
    }

    public void SetAllAlphas(float NewAlpha)
    {
      this.SetAlpha(NewAlpha);
      this.Fullness.SetAlpha(NewAlpha);
      if (this.Fullness2 != null)
        this.Fullness2.SetAlpha(NewAlpha);
      if (this.Fullness3 == null)
        return;
      this.Fullness3.SetAlpha(NewAlpha);
    }

    public bool CheckMouseOver(Player player, Vector2 Offset)
    {
      Offset += this.vLocation;
      this.MouseOver = MathStuff.CheckPointCollision(true, Offset, this.scale, (float) this.DrawRect.Width, (float) this.DrawRect.Height * Sengine.ScreenRatioUpwardsMultiplier.Y, player.inputmap.PointerLocation);
      return this.MouseOver;
    }

    public void DrawSatisfactionBar(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      Offset += this.vLocation;
      this.Fullness.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (this.Fullness2 != null)
        this.Fullness2.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (this.Fullness3 != null)
        this.Fullness3.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (!this.MouseOver)
        return;
      this.MouseOver = false;
      this.MouseOverObject.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }

    public void DrawSatisfactionBar_InverseOrder(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      Offset += this.vLocation;
      if (this.Fullness3 != null)
        this.Fullness3.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (this.Fullness2 != null)
        this.Fullness2.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      this.Fullness.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
      if (!this.MouseOver)
        return;
      this.MouseOver = false;
      this.MouseOverObject.Draw(spritebatch, AssetContainer.SpriteSheet, Offset);
    }

    public Vector2 GetVScale() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale;

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;
  }
}
