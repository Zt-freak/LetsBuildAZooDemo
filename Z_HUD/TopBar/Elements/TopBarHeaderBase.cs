// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.TopBarHeaderBase
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.TopBar.Elements.Customers;
using TinyZoo.Z_HUD.TopBar.Elements.DayOfWeek;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.TopBar.Elements
{
  internal class TopBarHeaderBase
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MouseoverHandler mouseOverHandler;
    private float BaseScale;
    private float PixelScale;
    private CustomerFrame flashFrame;
    private TimePopOut timePopOut;
    private DayPopOut dayPopOut;
    private CustomerPopOut customerPopOut;
    private TopBarPopOutType popOutType;

    public TopBarHeaderBase(
      float _BaseScale,
      float BarHeight,
      float BarWidth,
      bool IsButton_HasMouseOver = false,
      CustomerFrameColors customColor = CustomerFrameColors.DarkBrown,
      float pixelScaleMult = 2f)
    {
      this.BaseScale = _BaseScale;
      this.PixelScale = this.BaseScale * pixelScaleMult;
      this.customerFrame = new CustomerFrame(new Vector2(BarWidth, BarHeight), customColor, this.PixelScale);
      if (!IsButton_HasMouseOver)
        return;
      this.mouseOverHandler = new MouseoverHandler(this.customerFrame.VSCale, this.PixelScale);
    }

    public TopBarHeaderBase(
      float _BaseScale,
      float BarHeight,
      float BarWidth,
      Vector3 customColor,
      bool IsButton_HasMouseOver = false,
      float pixelScaleMult = 2f)
    {
      this.BaseScale = _BaseScale;
      this.PixelScale = this.BaseScale * pixelScaleMult;
      this.customerFrame = new CustomerFrame(new Vector2(BarWidth, BarHeight), customColor, this.PixelScale);
      if (!IsButton_HasMouseOver)
        return;
      this.mouseOverHandler = new MouseoverHandler(this.customerFrame.VSCale, this.PixelScale);
    }

    public void SetPopOutFrame(TopBarPopOutType _popOutType) => this.popOutType = _popOutType;

    public Vector2 GetSize() => this.customerFrame.VSCale;

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      if (this.customerFrame.CheckMouseOver(player, offset))
        return true;
      switch (this.popOutType)
      {
        case TopBarPopOutType.Time:
          if (this.timePopOut != null)
            return this.timePopOut.CheckMouseOver(player, offset);
          break;
        case TopBarPopOutType.Day:
          if (this.dayPopOut != null)
            return this.dayPopOut.CheckMouseOver(player, offset);
          break;
        case TopBarPopOutType.Customer:
          if (this.customerPopOut != null)
            return this.customerPopOut.CheckMouseOver(player, offset);
          break;
      }
      return false;
    }

    public void DoFlash(bool Good)
    {
      Vector3 color = Color.White.ToVector3();
      if (!Good)
        color = ColourData.Z_BarRed;
      if (this.flashFrame == null)
        this.flashFrame = new CustomerFrame(this.customerFrame.VSCale, color, this.PixelScale);
      this.flashFrame.frame.SetAlpha(false, 0.5f, 0.7f, 0.0f);
    }

    public bool UpdateTopBarHeaderBase(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.flashFrame != null)
        this.flashFrame.frame.UpdateColours(DeltaTime);
      if (this.mouseOverHandler == null)
        return false;
      this.mouseOverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      int num = this.mouseOverHandler.Clicked ? 1 : 0;
      if (num != 0)
      {
        this.OnClick_ConstructPopOut(player, offset);
        return num != 0;
      }
      this.UpdatePopOut(player, DeltaTime, offset);
      return num != 0;
    }

    private void OnClick_ConstructPopOut(Player player, Vector2 offset)
    {
      switch (this.popOutType)
      {
        case TopBarPopOutType.Time:
          if (this.timePopOut == null)
          {
            this.timePopOut = new TimePopOut(this.BaseScale);
            this.PositionPopOut((GenericTopBarPopOutFrame) this.timePopOut, offset);
            break;
          }
          this.timePopOut.ToggleLerp();
          break;
        case TopBarPopOutType.Day:
          if (this.dayPopOut == null)
          {
            this.dayPopOut = new DayPopOut(this.BaseScale);
            this.PositionPopOut((GenericTopBarPopOutFrame) this.dayPopOut, offset);
            break;
          }
          this.dayPopOut.ToggleLerp();
          break;
        case TopBarPopOutType.Customer:
          if (this.customerPopOut == null)
          {
            this.customerPopOut = new CustomerPopOut(this.BaseScale, player);
            this.PositionPopOut((GenericTopBarPopOutFrame) this.customerPopOut, offset);
            break;
          }
          this.customerPopOut.ToggleLerp();
          break;
      }
    }

    private void PositionPopOut(GenericTopBarPopOutFrame popOut, Vector2 offset)
    {
      popOut.location.Y += TopBarManager.GetMiddleOfBar();
      popOut.location.Y += popOut.GetSize().Y * 0.5f;
      float num = (float) ((double) popOut.GetSize().X * 0.5 - (double) this.GetSize().X * 0.5);
      if ((double) popOut.location.X + (double) offset.X + (double) num + (double) popOut.GetSize().X * 0.5 < 1024.0)
        popOut.location.X += num;
      else
        popOut.location.X -= num;
    }

    private void UpdatePopOut(Player player, float DeltaTime, Vector2 offset)
    {
      GenericTopBarPopOutFrame topBarPopOutFrame = (GenericTopBarPopOutFrame) null;
      if (this.popOutType == TopBarPopOutType.None)
        return;
      switch (this.popOutType)
      {
        case TopBarPopOutType.Time:
          if (this.timePopOut != null)
          {
            this.timePopOut.UpdateTimePopOut(player, DeltaTime, offset);
            topBarPopOutFrame = (GenericTopBarPopOutFrame) this.timePopOut;
            break;
          }
          break;
        case TopBarPopOutType.Day:
          if (this.dayPopOut != null)
          {
            this.dayPopOut.UpdateDayPopOut(player, DeltaTime, offset);
            topBarPopOutFrame = (GenericTopBarPopOutFrame) this.dayPopOut;
            break;
          }
          break;
        case TopBarPopOutType.Customer:
          if (this.customerPopOut != null)
          {
            this.customerPopOut.UpdateCustomerPopOut(player, DeltaTime, offset);
            topBarPopOutFrame = (GenericTopBarPopOutFrame) this.customerPopOut;
            break;
          }
          break;
      }
      if (topBarPopOutFrame == null)
        return;
      if (FeatureFlags.BlockAllUI)
        topBarPopOutFrame.LerpOff();
      if ((double) player.player.touchinput.ReleaseTapArray[0].X <= 0.0 || MathStuff.CheckPointCollision(true, topBarPopOutFrame.location + offset, 1f, topBarPopOutFrame.GetSize().X, topBarPopOutFrame.GetSize().Y, player.player.touchinput.ReleaseTapArray[0]))
        return;
      topBarPopOutFrame.LerpOff();
    }

    public void PreDrawTopBarHeaderBase(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.popOutType == TopBarPopOutType.None)
        return;
      switch (this.popOutType)
      {
        case TopBarPopOutType.Time:
          if (this.timePopOut == null)
            break;
          this.timePopOut.DrawTimePopOut(offset, spriteBatch);
          break;
        case TopBarPopOutType.Day:
          if (this.dayPopOut == null)
            break;
          this.dayPopOut.DrawDayPopOut(offset, spriteBatch);
          break;
        case TopBarPopOutType.Customer:
          if (this.customerPopOut == null)
            break;
          this.customerPopOut.DrawCustomerPopOut(offset, spriteBatch);
          break;
      }
    }

    public void DrawTopBarHeaderBase(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
    }

    public void PostDrawTopBarHeaderBase(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.mouseOverHandler != null && !FeatureFlags.BlockMouseOverOnBuildBar)
        this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
      if (this.flashFrame == null)
        return;
      this.flashFrame.DrawCustomerFrame(offset, spriteBatch);
    }
  }
}
