// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.RecipeView.FoodSlidr.DragAndBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageShop.RecipeView.FoodSlidr
{
  internal class DragAndBar
  {
    private GameObject BarHandle;
    private GameObject FRAME;
    private GameObject LeftSide;
    private GameObject RightSide;
    public Vector2 VSCALEOutSide;
    private Vector2 VSCALEInside;
    public Vector2 Location;
    public float CurrentDragPercent;
    private float drawDragPercent;
    public string LeftText;
    public string RightText;
    private float OverallScaleMultipler;
    private UIScaleHelper uiscale;
    private bool isDiscrete;
    private int numDiscreteValues;
    private Vector2 hiddenbarloc;
    private bool hidehandle;
    private float BaseScale;
    private bool isActive = true;
    private static Vector3 leftSideColor = new Vector3(0.9529412f, 0.6980392f, 0.3960784f);
    private static Vector3 rightSideColor = new Vector3(0.3960784f, 0.9098039f, 0.9529412f);
    private static Vector3 leftSideColor_Free = new Vector3(0.8588235f, 0.9254902f, 0.2666667f);
    private static Vector3 rightSideColor_Free = new Vector3(0.9529412f, 0.6980392f, 0.3960784f);
    private bool IsFreeItem;
    public float ExtraHeight;
    public float ExtraWidth;

    public bool HideHandle
    {
      get => this.hidehandle;
      set => this.hidehandle = value;
    }

    public DragAndBar(
      float MasterScale,
      bool _IsFreeItem,
      float _CurrentDragPercent,
      float FullWidth = 800f,
      float _BaseScale = 1f,
      float ScaleForHandleAndBarMult = 2f)
    {
      this.OverallScaleMultipler = _BaseScale;
      this.BaseScale = _BaseScale;
      this.IsFreeItem = _IsFreeItem;
      this.LeftText = "";
      this.RightText = "";
      this.CurrentDragPercent = _CurrentDragPercent;
      this.BarHandle = new GameObject();
      this.BarHandle.DrawRect = new Rectangle(929, 560, 18, 18);
      this.BarHandle.SetDrawOriginToCentre();
      this.BarHandle.scale = MasterScale * ScaleForHandleAndBarMult;
      this.FRAME = new GameObject();
      this.FRAME.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FRAME.SetDrawOriginToCentre();
      this.FRAME.scale = _BaseScale;
      this.LeftSide = new GameObject(this.FRAME);
      this.RightSide = new GameObject(this.FRAME);
      this.SetDefaultColors();
      this.VSCALEOutSide = new Vector2(FullWidth, 15f * this.OverallScaleMultipler * ScaleForHandleAndBarMult);
      this.VSCALEInside = this.VSCALEOutSide - new Vector2(4f * this.OverallScaleMultipler * ScaleForHandleAndBarMult, 4f * Sengine.ScreenRatioUpwardsMultiplier.Y * this.OverallScaleMultipler * ScaleForHandleAndBarMult);
      this.LeftSide.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.RightSide.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
      this.RightSide.vLocation.X = this.VSCALEInside.X * 0.5f;
      this.LeftSide.vLocation.X = this.VSCALEInside.X * -0.5f;
      this.BarHandle.vLocation.X = this.VSCALEInside.X * -0.5f;
      this.BarHandle.vLocation.X += this.VSCALEInside.X * this.CurrentDragPercent;
      this.hiddenbarloc = this.BarHandle.vLocation;
    }

    public DragAndBar(
      bool IsFreeItem,
      float _CurrentDragPercent,
      float FullWidth,
      float basescale_,
      int numDiscreteValues_ = 0)
    {
      this.BaseScale = basescale_;
      float basescale_1 = basescale_;
      this.OverallScaleMultipler = basescale_1;
      this.uiscale = new UIScaleHelper(basescale_1);
      this.numDiscreteValues = numDiscreteValues_;
      this.isDiscrete = this.numDiscreteValues > 1;
      this.LeftText = "";
      this.RightText = "";
      this.CurrentDragPercent = _CurrentDragPercent;
      this.BarHandle = new GameObject();
      this.BarHandle.DrawRect = new Rectangle(929, 560, 18, 18);
      this.BarHandle.SetDrawOriginToCentre();
      this.BarHandle.scale = this.OverallScaleMultipler;
      this.FRAME = new GameObject();
      this.FRAME.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.FRAME.SetDrawOriginToCentre();
      this.LeftSide = new GameObject(this.FRAME);
      this.RightSide = new GameObject(this.FRAME);
      this.SetDefaultColors();
      this.VSCALEOutSide = new Vector2(FullWidth, 15f);
      this.VSCALEInside = this.VSCALEOutSide - new Vector2(4f, 4f);
      this.LeftSide.SetDrawOriginToPoint(DrawOriginPosition.CentreLeft);
      this.RightSide.SetDrawOriginToPoint(DrawOriginPosition.CenterRight);
      this.RightSide.vLocation.X = this.VSCALEInside.X * 0.5f;
      this.LeftSide.vLocation.X = this.VSCALEInside.X * -0.5f;
      this.BarHandle.vLocation.X = this.VSCALEInside.X * -0.5f;
      this.BarHandle.vLocation.X += this.VSCALEInside.X * this.CurrentDragPercent;
      this.drawDragPercent = this.CurrentDragPercent;
      this.hiddenbarloc = this.BarHandle.vLocation;
    }

    public void ForceFullness(Player player, float Movement)
    {
      this.CurrentDragPercent = MathHelper.Clamp(this.CurrentDragPercent += Movement, 0.0f, 1f);
      this.BarHandle.vLocation.X = this.VSCALEInside.X * this.CurrentDragPercent;
      this.BarHandle.vLocation.X -= this.VSCALEInside.X * 0.5f;
      this.drawDragPercent = this.CurrentDragPercent;
    }

    public Vector2 GetSize() => this.VSCALEOutSide;

    private void SetDefaultColors()
    {
      if (this.IsFreeItem)
      {
        this.LeftSide.SetAllColours(DragAndBar.leftSideColor_Free);
        this.RightSide.SetAllColours(DragAndBar.rightSideColor_Free);
      }
      else
      {
        this.LeftSide.SetAllColours(DragAndBar.leftSideColor);
        this.RightSide.SetAllColours(DragAndBar.rightSideColor);
      }
      Vector3 SecondaryColour;
      StringInBox.GetFrameColourRect(BTNColour.Cream, out SecondaryColour);
      this.FRAME.SetAllColours(SecondaryColour);
      this.BarHandle.SetAllColours(Color.White.ToVector3());
    }

    public void SetIsActive(bool _isActive)
    {
      this.isActive = _isActive;
      if (this.isActive)
      {
        this.SetDefaultColors();
      }
      else
      {
        Vector3 vector3 = Color.Gray.ToVector3();
        this.BarHandle.SetAllColours(vector3);
        this.LeftSide.SetAllColours(vector3);
        this.RightSide.SetAllColours(vector3);
      }
    }

    public void UpdateDragAndBar(Player player, float DeltaTime, Vector2 Offset)
    {
      Offset += this.Location;
      if (!this.isActive)
        return;
      if (!this.isDiscrete)
      {
        if (MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCALEOutSide.X + this.ExtraWidth, this.VSCALEOutSide.Y + this.ExtraHeight, player.player.touchinput.TouchStartLocation))
        {
          this.BarHandle.vLocation.X += player.player.touchinput.DragVectorThisFrame.X;
          if ((double) this.BarHandle.vLocation.X < -(double) this.VSCALEInside.X * 0.5)
            this.BarHandle.vLocation.X = (float) (-(double) this.VSCALEInside.X * 0.5);
          else if ((double) this.BarHandle.vLocation.X > (double) this.VSCALEInside.X * 0.5)
            this.BarHandle.vLocation.X = this.VSCALEInside.X * 0.5f;
          this.CurrentDragPercent = (this.BarHandle.vLocation.X + this.VSCALEInside.X * 0.5f) / this.VSCALEInside.X;
        }
        this.drawDragPercent = this.CurrentDragPercent;
      }
      else if (MathStuff.CheckPointCollision(true, Offset, 1f, this.VSCALEOutSide.X + this.ExtraWidth, this.VSCALEOutSide.Y + this.ExtraHeight, player.player.touchinput.TouchStartLocation))
      {
        this.hiddenbarloc.X += player.player.touchinput.DragVectorThisFrame.X;
        if ((double) this.hiddenbarloc.X < -(double) this.VSCALEInside.X * 0.5)
          this.hiddenbarloc.X = (float) (-(double) this.VSCALEInside.X * 0.5);
        else if ((double) this.hiddenbarloc.X > (double) this.VSCALEInside.X * 0.5)
          this.hiddenbarloc.X = this.VSCALEInside.X * 0.5f;
        this.BarHandle.vLocation.X = ((float) Math.Round(((double) this.hiddenbarloc.X + (double) this.VSCALEInside.X * 0.5) / (double) this.VSCALEInside.X * (double) (this.numDiscreteValues - 1)) / (float) (this.numDiscreteValues - 1) - 0.5f) * this.VSCALEInside.X;
        this.CurrentDragPercent = (this.BarHandle.vLocation.X + this.VSCALEInside.X * 0.5f) / this.VSCALEInside.X;
        this.drawDragPercent = this.CurrentDragPercent;
      }
      else
        this.CurrentDragPercent = this.drawDragPercent;
    }

    public void DrawDragAndBar(SpriteBatch UseThis, Vector2 Offset)
    {
      Offset += this.Location;
      this.FRAME.Draw(UseThis, AssetContainer.SpriteSheet, Offset, this.VSCALEOutSide);
      this.LeftSide.Draw(UseThis, AssetContainer.SpriteSheet, Offset, new Vector2(this.VSCALEInside.X * this.drawDragPercent, this.VSCALEInside.Y));
      this.RightSide.Draw(UseThis, AssetContainer.SpriteSheet, Offset, new Vector2(this.VSCALEInside.X * (1f - this.drawDragPercent), this.VSCALEInside.Y));
      if (!this.hidehandle)
        this.BarHandle.Draw(UseThis, AssetContainer.SpriteSheet, Offset);
      TextFunctions.DrawTextWithDropShadow(this.LeftText, this.BaseScale, Offset + this.FRAME.vLocation + new Vector2(0.0f, -20f * this.BaseScale) + this.VSCALEOutSide * -0.5f, this.FRAME.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, UseThis, true);
      TextFunctions.DrawTextWithDropShadow(this.RightText, this.BaseScale, Offset + this.FRAME.vLocation + new Vector2(0.0f, -20f * this.BaseScale) + new Vector2(this.VSCALEOutSide.X * 0.5f, this.VSCALEOutSide.Y * -0.5f), this.FRAME.GetColour(), 1f, AssetContainer.SpringFontX1AndHalf, UseThis, false, true);
    }
  }
}
