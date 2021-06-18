// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTableRowFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.EmployeeView
{
  internal class PerformanceTableRowFrame
  {
    public Vector2 location;
    private SplitFrame splitFrame;
    private CustomerFrame customerFrame;
    private RowSegmentRectangle[] rectangles;
    private RowSegmentRectangle[] colouredOverlay;
    private MouseoverHandler mouseOverHandler;
    private UIScaleHelper scaleHelper;
    private float[] refWidths;
    private float refHeight;
    public bool Active;

    public PerformanceTableRowFrame(
      float BaseScale,
      float Height,
      bool isSplitHeader,
      bool isBlueSplitHeader,
      params float[] widths)
    {
      this.Create(BaseScale, Height, isSplitHeader, isBlueSplitHeader, CustomerFrameColors.Brown, Vector3.Zero, false, widths);
    }

    public PerformanceTableRowFrame(
      float BaseScale,
      float Height,
      CustomerFrameColors frameColor,
      params float[] widths)
    {
      this.Create(BaseScale, Height, false, false, frameColor, Vector3.Zero, false, widths);
    }

    public PerformanceTableRowFrame(
      float BaseScale,
      float Height,
      CustomerFrameColors frameColor,
      bool IsLightFirst = false,
      params float[] widths)
    {
      this.Create(BaseScale, Height, false, false, frameColor, Vector3.Zero, IsLightFirst, widths);
    }

    public PerformanceTableRowFrame(
      float BaseScale,
      float Height,
      Vector3 frameColor,
      params float[] widths)
    {
      this.Create(BaseScale, Height, false, false, CustomerFrameColors.Count, frameColor, false, widths);
    }

    private void Create(
      float BaseScale,
      float Height,
      bool isSplitHeader,
      bool isBlueSplitHeader,
      CustomerFrameColors frameColor,
      Vector3 customFrameColor,
      bool IsLightFirst = false,
      params float[] widths)
    {
      this.scaleHelper = new UIScaleHelper(BaseScale);
      this.Active = true;
      this.refWidths = widths;
      this.refHeight = Height;
      this.rectangles = new RowSegmentRectangle[widths.Length];
      float x = 0.0f;
      for (int index = 0; index < widths.Length; ++index)
      {
        if (IsLightFirst && index % 2 == 1 || !IsLightFirst && index % 2 == 0)
        {
          RowSegmentRectangle segmentRectangle = new RowSegmentRectangle(widths[index], Height);
          segmentRectangle.vLocation.X = x + widths[index] * 0.5f;
          this.rectangles[index] = segmentRectangle;
        }
        x += widths[index];
      }
      Vector2 vector2 = new Vector2(x, Height);
      if (isSplitHeader)
        this.splitFrame = !isBlueSplitHeader ? new SplitFrame(vector2, BaseScale, 0.5f) : new SplitFrame(vector2, ColourData.Z_FrameBlueDarker, ColourData.Z_FrameBluePale, BaseScale, 0.5f);
      else
        this.customerFrame = frameColor == CustomerFrameColors.Count ? new CustomerFrame(vector2, customFrameColor, BaseScale) : new CustomerFrame(vector2, frameColor, BaseScale);
      for (int index = 0; index < this.rectangles.Length; ++index)
      {
        if (this.rectangles[index] != null)
          this.rectangles[index].vLocation.X -= x * 0.5f;
      }
      this.mouseOverHandler = new MouseoverHandler(vector2, BaseScale);
    }

    public Vector2 GetSize() => this.splitFrame != null ? this.splitFrame.VSCale : this.customerFrame.VSCale;

    public void RemoveColumnColor(int columnIndex)
    {
      if (this.colouredOverlay == null)
        return;
      this.colouredOverlay[columnIndex] = (RowSegmentRectangle) null;
    }

    public void ColorThisColumnRed(int columnIndex, float marginX_raw = 0.0f, float marginY_raw = 0.0f) => this.ColorThisColumn(columnIndex, ColourData.Z_FrameRedPale, marginX_raw, marginY_raw);

    public void ColorThisColumnGreen(int columnIndex, float marginX_raw = 0.0f, float marginY_raw = 0.0f) => this.ColorThisColumn(columnIndex, ColourData.Z_FrameGreenPale, marginX_raw, marginY_raw);

    public void ColorThisColumn(
      int columnIndex,
      Vector3 colour,
      float marginX_raw = 0.0f,
      float marginY_raw = 0.0f)
    {
      if (this.colouredOverlay == null)
        this.colouredOverlay = new RowSegmentRectangle[this.rectangles.Length];
      Vector2 vector2 = new Vector2(this.refWidths[columnIndex], this.refHeight);
      if ((double) marginX_raw != 0.0)
      {
        vector2.X -= this.scaleHelper.ScaleX(marginX_raw) * 2f;
        vector2.Y -= this.scaleHelper.ScaleY(marginY_raw) * 2f;
      }
      RowSegmentRectangle segmentRectangle = new RowSegmentRectangle(vector2.X, vector2.Y, colour, 1f);
      float num = 0.0f;
      for (int index = 0; index < this.refWidths.Length; ++index)
      {
        num += this.refWidths[index];
        if (index == columnIndex)
        {
          num -= this.refWidths[index] * 0.5f;
          break;
        }
      }
      segmentRectangle.vLocation.X = num - this.customerFrame.VSCale.X * 0.5f;
      this.colouredOverlay[columnIndex] = segmentRectangle;
    }

    public bool UpdateFrameForMouseOver(Player player, float DeltaTime, Vector2 offset)
    {
      this.UpdateWithoutMouseOver(player, DeltaTime, offset);
      offset += this.location;
      if (this.Active)
        this.mouseOverHandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      return this.mouseOverHandler.Clicked;
    }

    public void UpdateWithoutMouseOver(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (this.splitFrame != null)
        this.splitFrame.SetActive(this.Active);
      else
        this.customerFrame.Active = this.Active;
    }

    public void DrawPerformanceTableRowFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.splitFrame != null)
        this.splitFrame.DrawSplitFrame(offset, spriteBatch);
      else
        this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      for (int index = 0; index < this.rectangles.Length; ++index)
      {
        if (this.colouredOverlay != null && this.colouredOverlay[index] != null)
          this.colouredOverlay[index].DrawRowSegmentRectangle(offset, spriteBatch);
        if (this.rectangles[index] != null)
          this.rectangles[index].DrawRowSegmentRectangle(offset, spriteBatch);
      }
    }

    public void PostDrawPerformanceTableRowFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.mouseOverHandler.DrawMouseOverHandler(spriteBatch, offset);
      if (this.splitFrame != null)
        this.splitFrame.DrawDarkOverlay(offset, spriteBatch);
      else
        this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
