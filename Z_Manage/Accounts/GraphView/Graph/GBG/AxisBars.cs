// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.AxisBars
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG
{
  internal class AxisBars
  {
    private GameObject AxisUp;
    private GameObject AxisRight;
    private Vector2 VSCALEY;
    private Vector2 VSCALEX;

    public AxisBars(
      Vector3 SecondaryColour,
      float BarHeight,
      float GridWidth,
      float PadSpaceForAxis = 10f)
    {
      this.AxisUp = new GameObject();
      this.AxisUp.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.AxisUp.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.AxisRight = new GameObject();
      this.AxisRight.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.AxisRight.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
      this.AxisRight.SetAllColours(SecondaryColour);
      this.AxisUp.SetAllColours(SecondaryColour);
      this.VSCALEY = new Vector2(2f, BarHeight + PadSpaceForAxis);
      this.VSCALEX = new Vector2(GridWidth + PadSpaceForAxis, 2f);
      this.AxisUp.vLocation = new Vector2(-PadSpaceForAxis, PadSpaceForAxis);
      this.AxisRight.vLocation = new Vector2(-PadSpaceForAxis, PadSpaceForAxis);
    }

    public void DrawAxisBars(Vector2 Offset)
    {
      this.AxisRight.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALEX);
      this.AxisUp.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALEY);
    }
  }
}
