// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.MoreButton
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class MoreButton
  {
    private static Rectangle arrowrect = new Rectangle(0, 570, 12, 7);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private ZGenericUIDrawObject arrow;
    private ZGenericText label;
    private MouseoverHandler mouseoverhandler;
    private bool arrowonly;
    private Vector3 colour;
    private Vector3 mouseovercolour;
    private LerpHandler_Float lerper;
    private bool prevMO;
    private float framescalemult;

    public MoreButton(float basescale_, bool arrowonly_ = false, string labelstring = "More")
    {
      this.arrowonly = arrowonly_;
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.colour = ColourData.Z_Cream;
      this.mouseovercolour = ColourData.MoreButtonMouseoverOrange;
      if (!this.arrowonly)
      {
        this.label = new ZGenericText(labelstring, this.basescale, false, _UseOnePointFiveFont: true);
        this.label.SetAllColours(this.colour);
      }
      this.arrow = new ZGenericUIDrawObject(MoreButton.arrowrect, this.basescale, AssetContainer.SpriteSheet);
      this.arrow.Rotation = 1.570796f;
      this.arrow.SetColour(this.colour);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(2f * this.basescale);
      Vector2 vector2_1 = this.scalehelper.ScaleVector2(new Vector2((float) MoreButton.arrowrect.Height, (float) MoreButton.arrowrect.Width));
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 0.0f, 0.0f, 3f);
      this.framescale = new Vector2();
      if (this.arrowonly)
      {
        this.framescale = vector2_1;
      }
      else
      {
        this.framescale.Y = Math.Max(this.label.GetSize().Y, vector2_1.Y);
        this.framescale.X += (float) ((double) this.label.GetSize().X + (double) vector2_1.X + 0.5 * (double) this.pad.X);
      }
      this.framescale += this.pad;
      this.frame = new CustomerFrame(this.framescale, CustomerFrameColors.WhiteOval, this.basescale);
      this.frame.SetColour(new Vector3(232f, 197f, 114f) / (float) byte.MaxValue);
      this.mouseoverhandler = new MouseoverHandler(this.framescale, this.basescale);
      Vector2 vector2_2 = -0.5f * this.framescale + 0.5f * this.pad;
      if (this.arrowonly)
        return;
      this.label.vLocation = vector2_2;
      this.label.vLocation.Y = (float) (0.0 - 0.5 * (double) this.label.GetSize().Y);
      vector2_2.X += this.label.GetSize().X + 0.5f * this.pad.X;
      this.arrow.location = vector2_2 + 0.5f * vector2_1;
      this.arrow.location.Y = 0.0f;
    }

    public void SetTextColour(Vector3 colour_) => this.colour = colour_;

    public void SetMouseoverTextColour(Vector3 colour_) => this.mouseovercolour = colour_;

    public void SetToBottomRightHandCorner(float BaseScale)
    {
      this.location = new Vector2(BaseScale * 100f, BaseScale * 100f * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.location.X -= this.GetSize().X * 0.5f;
      this.location.Y -= this.GetSize().Y * 0.5f;
      this.location.X -= BaseScale * 10f;
      this.location.Y -= BaseScale * 10f * Sengine.ScreenRatioUpwardsMultiplier.Y;
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateMoreButton(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag1 = false;
      bool flag2 = this.mouseoverhandler.UpdateMouseoverHandler(player, offset, DeltaTime);
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (flag2)
      {
        if (this.label != null)
          this.label.SetAllColours(this.mouseovercolour);
        this.arrow.SetColour(this.mouseovercolour);
        flag1 |= this.mouseoverhandler.Clicked;
        if (!this.prevMO)
          this.lerper.SetLerp(true, this.lerper.Value, 1f, 6f);
      }
      else
      {
        if (this.label != null)
          this.label.SetAllColours(this.colour);
        this.arrow.SetColour(this.colour);
        if (!this.prevMO)
          this.lerper.SetLerp(true, this.lerper.Value, 0.0f, 6f);
      }
      this.prevMO = flag2;
      this.framescalemult = (float) (1.0 + (double) this.lerper.Value * 0.100000001490116);
      return flag1;
    }

    public void DrawMoreButton(SpriteBatch spritebatch, Vector2 offset, float AlphaMult = -1f)
    {
      offset += this.location;
      if ((double) AlphaMult > -1.0 && (double) AlphaMult != (double) this.frame.frame.fAlpha)
        this.frame.frame.fAlpha = AlphaMult;
      this.frame.DrawCustomerFrameWithScaleMult(offset, spritebatch, this.framescalemult);
      if (!this.arrowonly)
        this.label.DrawZGenericText(offset, spritebatch, AlphaMult);
      this.arrow.DrawZGenericUIDrawObject(spritebatch, offset, AlphaMult);
    }
  }
}
