// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.MouseOverInfoBox
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable;
using TinyZoo.Z_Research_.ControlsHint;
using TinyZoo.Z_Research_.IconGrid.Elements.PackInformation;
using TinyZoo.Z_Research_.IconGrid.Morality;
using TinyZoo.Z_Research_.RData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class MouseOverInfoBox
  {
    public REntry rentry;
    public bool bActive;
    private Vector2 WorldSpaceLocation;
    private CustomerFrame customerframe;
    private SimpleTextHandler simpletext;
    private R_ControlHint controlHint;
    private Vector2 mouseOverOffset;
    private PackInfoDisplay packInfoDisplay;
    private MoralityRequirementDisplay moralityDisplay;
    private RowSegmentRectangle rectangleToHideFrameEdge;

    public MouseOverInfoBox(
      REntry _rentry,
      Vector2 _WorldSpaceLocation,
      R_Icon ricon,
      Player player,
      float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 vector2_1 = uiScaleHelper.DefaultBuffer * 0.5f;
      this.WorldSpaceLocation = _WorldSpaceLocation;
      this.rentry = _rentry;
      Vector2 vector2_2 = new Vector2((float) ricon.DrawRect.Width * ricon.scale, (float) ricon.DrawRect.Height * ricon.scale);
      this.mouseOverOffset = vector2_2;
      this.mouseOverOffset += (ricon.GetMouseOverVscale() - vector2_2) * 0.5f;
      float y1 = vector2_1.Y;
      float x = vector2_1.X;
      float PercentagePfScreenWidth = 0.2f * BaseScale;
      this.simpletext = new SimpleTextHandler(_rentry.Heading, false, PercentagePfScreenWidth, BaseScale, true, true);
      this.simpletext.Location.Y = y1;
      this.simpletext.Location.X = vector2_1.X;
      this.simpletext.SetAllColours(ColourData.Z_Cream);
      float heightOfParagraph = this.simpletext.GetHeightOfParagraph();
      float num1 = y1 + heightOfParagraph;
      float num2 = x + PercentagePfScreenWidth * 1024f;
      float y2 = num1 + vector2_1.Y;
      this.packInfoDisplay = new PackInfoDisplay(_rentry, ricon, player, BaseScale, num2 - vector2_1.X);
      this.packInfoDisplay.location = new Vector2(vector2_1.X, y2);
      this.packInfoDisplay.location += this.packInfoDisplay.GetSize() * 0.5f;
      float y3 = y2 + this.packInfoDisplay.GetSize().Y + vector2_1.Y;
      this.moralityDisplay = new MoralityRequirementDisplay(_rentry.WillUnlockThese, player, BaseScale, num2 - vector2_1.X, ColourData.Z_FrameDarkBrown);
      Vector2 size = this.moralityDisplay.GetSize();
      this.moralityDisplay.location = new Vector2(vector2_1.X, y3);
      this.moralityDisplay.location += this.moralityDisplay.GetSize() * 0.5f;
      if (size != Vector2.Zero)
        y3 += size.Y + vector2_1.Y;
      if (!GameFlags.IsUsingController && ricon.unlockstate == UnlockState.Preview)
      {
        this.controlHint = new R_ControlHint(BaseScale);
        this.controlHint.location.X += num2;
        this.controlHint.location.X -= this.controlHint.GetSize().X;
        this.controlHint.location.X -= 8f * BaseScale;
        this.controlHint.location.Y = y3 + this.controlHint.GetSize().Y * 0.5f;
        y3 = y3 + this.controlHint.GetSize().Y + vector2_1.Y;
      }
      this.customerframe = new CustomerFrame(new Vector2(num2 + vector2_1.X, y3), CustomerFrameColors.Brown, BaseScale);
      this.customerframe.location += this.customerframe.VSCale * 0.5f;
      this.rectangleToHideFrameEdge = new RowSegmentRectangle(uiScaleHelper.ScaleX(3f), uiScaleHelper.ScaleY(3f), ColourData.Z_FrameMidBrown, 1f);
    }

    private void SetEdgingMaskPosition(bool isLeft, bool isUp, bool None)
    {
      if (None)
        return;
      if (isLeft)
        this.rectangleToHideFrameEdge.vLocation.X = (float) (-(double) this.customerframe.VSCale.X * 0.5);
      else
        this.rectangleToHideFrameEdge.vLocation.X = this.customerframe.VSCale.X * 0.5f;
      if (isUp)
        this.rectangleToHideFrameEdge.vLocation.Y = (float) (-(double) this.customerframe.VSCale.Y * 0.5);
      else
        this.rectangleToHideFrameEdge.vLocation.Y = this.customerframe.VSCale.Y * 0.5f;
      this.rectangleToHideFrameEdge.SetDrawOriginToPoint(!isLeft ? (!isUp ? DrawOriginPosition.BottomRight : DrawOriginPosition.TopRight) : (!isUp ? DrawOriginPosition.BottomLeft : DrawOriginPosition.TopLeft));
    }

    public void DrawMouseOverInfoBox()
    {
      if (!this.bActive)
        return;
      SpriteBatch pointspritebatch03 = AssetContainer.pointspritebatch03;
      Vector2 screenSpace = RenderMath.TranslateWorldSpaceToScreenSpace(this.WorldSpaceLocation);
      screenSpace.X += this.mouseOverOffset.X * 0.5f * Sengine.WorldOriginandScale.Z;
      if (!GameFlags.IsUsingController)
      {
        bool flag1 = (double) screenSpace.X + (double) this.customerframe.VSCale.X > 1024.0;
        bool flag2 = (double) screenSpace.Y + (double) this.customerframe.VSCale.Y > 768.0;
        bool None = flag2 && (double) screenSpace.Y - (double) this.customerframe.VSCale.Y < 0.0;
        if (flag1)
          screenSpace.X -= this.customerframe.VSCale.X + this.mouseOverOffset.X * Sengine.WorldOriginandScale.Z;
        if (flag2)
        {
          if (None)
            screenSpace.Y -= this.customerframe.VSCale.Y * 0.5f;
          else
            screenSpace.Y -= this.customerframe.VSCale.Y;
        }
        this.SetEdgingMaskPosition(!flag1, !flag2, None);
      }
      this.customerframe.DrawCustomerFrame(screenSpace, pointspritebatch03);
      this.rectangleToHideFrameEdge.DrawRowSegmentRectangle(screenSpace + this.customerframe.location, pointspritebatch03);
      this.simpletext.DrawSimpleTextHandler(screenSpace, 1f, pointspritebatch03);
      this.packInfoDisplay.DrawPackInfoDisplay(screenSpace, pointspritebatch03);
      if (this.controlHint != null)
        this.controlHint.DrawR_ControlHint(screenSpace, pointspritebatch03);
      this.moralityDisplay.DrawMoralityRequirementDisplay(screenSpace, pointspritebatch03);
    }
  }
}
