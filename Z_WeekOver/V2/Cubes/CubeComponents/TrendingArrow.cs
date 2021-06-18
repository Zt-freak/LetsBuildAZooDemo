// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents.TrendingArrow
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WeekOver.V2.Cubes.CubeComponents
{
  internal class TrendingArrow
  {
    private Rectangle arrowrect = new Rectangle(293, 543, 55, 31);
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private Vector2 Vscale;
    private ZGenericUIDrawObject arrow;

    public TrendingArrow(float basescale_, bool TrendingUp = true)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.Vscale = new Vector2(this.basescale, this.basescale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      if (TrendingUp)
        this.Vscale.Y *= -1f;
      this.arrow = new ZGenericUIDrawObject(this.arrowrect, this.basescale, AssetContainer.UISheet);
      this.framescale = this.arrow.GetSize();
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
    }

    public void SetColour(Vector3 colour) => this.arrow.SetColour(colour);

    public void SetLocationAt(PositionInFrame position) => this.SetLocationAt(position, this.scalehelper.ScaleVector2(new Vector2(EndPOfWeekSummaryManager.SIZE)));

    public void SetLocationAt(PositionInFrame position, Vector2 frameSize, float frameborder_raw = 10f)
    {
      Vector2 vector2 = this.scalehelper.ScaleVector2(new Vector2(frameborder_raw));
      switch (position)
      {
        case PositionInFrame.BottomLeft:
        case PositionInFrame.BottomCenter:
        case PositionInFrame.BottomRight:
          this.location.Y = (float) (0.5 * (double) frameSize.Y - 0.5 * (double) this.framescale.Y) - vector2.Y;
          break;
        case PositionInFrame.CenterLeft:
        case PositionInFrame.TrueCenter:
        case PositionInFrame.CenterRight:
          this.location.Y = 0.0f;
          break;
        case PositionInFrame.TopLeft:
        case PositionInFrame.TopCenter:
        case PositionInFrame.TopRight:
          this.location.Y = (float) (-0.5 * (double) frameSize.Y + 0.5 * (double) this.framescale.Y) + vector2.Y;
          break;
      }
      switch (position)
      {
        case PositionInFrame.BottomLeft:
        case PositionInFrame.CenterLeft:
        case PositionInFrame.TopLeft:
          this.location.X = (float) (-0.5 * (double) frameSize.X + 0.5 * (double) this.framescale.X) + vector2.X;
          break;
        case PositionInFrame.BottomCenter:
        case PositionInFrame.TrueCenter:
        case PositionInFrame.TopCenter:
          this.location.X = 0.0f;
          break;
        case PositionInFrame.BottomRight:
        case PositionInFrame.CenterRight:
        case PositionInFrame.TopRight:
          this.location.X = (float) (0.5 * (double) frameSize.X - 0.5 * (double) this.framescale.X) - vector2.X;
          break;
      }
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateTrendingArrow(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      return false;
    }

    public void DrawTrendingArrow(SpriteBatch spritebatch, Vector2 offset, float AlphaMult)
    {
      offset += this.location;
      this.arrow.obj.Draw(spritebatch, AssetContainer.UISheet, offset, this.Vscale, this.arrow.obj.fAlpha * AlphaMult);
    }
  }
}
