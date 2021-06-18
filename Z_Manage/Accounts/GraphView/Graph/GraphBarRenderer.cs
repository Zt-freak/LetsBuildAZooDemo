// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GraphBarRenderer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph
{
  internal class GraphBarRenderer : GameObject
  {
    private Vector2 VSCALE;
    private GameObject OtherBar;
    private Vector2 OtherVSCALE;

    public GraphBarRenderer(
      float ScaleValue,
      Vector3 CLYSSS,
      float HighestValueInGraph,
      bool IsDarker,
      float ExtraBackColorScale,
      Vector3 ExtraColour)
    {
      float num = 1f;
      if (IsDarker)
        num = 0.9f;
      this.SetAllColours(CLYSSS * num);
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToPoint(DrawOriginPosition.CentreBottom);
      this.VSCALE = new Vector2(1f, ScaleValue / HighestValueInGraph);
      if ((double) ExtraBackColorScale <= 0.0)
        return;
      this.OtherBar = new GameObject((GameObject) this);
      this.OtherBar.SetAllColours(ExtraColour);
      this.OtherVSCALE = new Vector2(1f, ExtraBackColorScale / HighestValueInGraph);
    }

    public void UpdateGraphBarRenderer()
    {
    }

    public void DrawGraphBarRenderer(Vector2 Offset, Vector2 SCALEMULT)
    {
      if (this.OtherBar != null)
        this.OtherBar.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.OtherVSCALE * SCALEMULT, this.fAlpha);
      this.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCALE * SCALEMULT, this.fAlpha);
    }
  }
}
