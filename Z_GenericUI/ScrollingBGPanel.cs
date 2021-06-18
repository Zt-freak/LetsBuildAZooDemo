// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_GenericUI.ScrollingBGPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI.Z_ScrollingBG;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;

namespace TinyZoo.Z_GenericUI
{
  internal class ScrollingBGPanel : BigBrownPanel
  {
    private ScrollingBGFrame customerFrame;
    private RowSegmentRectangle rectangleMask_Top;
    private RowSegmentRectangle rectangleMask_Bottom;

    public ScrollingBGPanel(
      Vector2 _vScale,
      bool HasCloseButton = false,
      string addHeaderText = "",
      float _BaseScale = -1f,
      bool _HasPreviousButton = false)
      : base(_vScale, HasCloseButton, addHeaderText, _BaseScale, _HasPreviousButton, PanelFrameType.Gold)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(_BaseScale);
      this.customerFrame = new ScrollingBGFrame(_vScale, _BaseScale);
      this.rectangleMask_Top = new RowSegmentRectangle(_vScale.X, uiScaleHelper.ScaleY(1f), ColourData.Z_FrameGold, 1f);
      this.rectangleMask_Top.vLocation.Y -= this.customerFrame.VSCale.Y * 0.5f;
      this.rectangleMask_Top.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.rectangleMask_Bottom = new RowSegmentRectangle(_vScale.X, uiScaleHelper.ScaleY(1f), ColourData.Z_FrameGold, 1f);
      this.rectangleMask_Bottom.vLocation.Y = this.customerFrame.VSCale.Y * 0.5f;
      this.rectangleMask_Bottom.SetDrawOriginToPoint(DrawOriginPosition.CentreTop);
      this.Finalize(_vScale);
    }

    public void UpdateScrollingBG(float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.customerFrame.UpdateScrollingBGFrame(DeltaTime, offset);
    }

    public void DrawScrollingBGPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawBigBrownPanel(offset, spriteBatch);
      this.customerFrame.DrawScrollingBGFrame(offset, spriteBatch);
      this.rectangleMask_Top.DrawRowSegmentRectangle(offset, spriteBatch);
      this.rectangleMask_Bottom.DrawRowSegmentRectangle(offset, spriteBatch);
    }
  }
}
