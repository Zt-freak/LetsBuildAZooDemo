// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG.GraphBG
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_Manage.Accounts.GraphView.Graph.GBG
{
  internal class GraphBG
  {
    private GameObject BGGG;
    private Vector2 VSCale;

    public GraphBG(Vector2 GraphSize)
    {
      this.VSCale = GraphSize;
      this.BGGG = new GameObject();
      this.BGGG.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.BGGG.SetDrawOriginToPoint(DrawOriginPosition.BottomLeft);
      this.BGGG.SetAllColours(0.0f, 0.0f, 0.0f);
      this.BGGG.SetAlpha(0.1f);
    }

    public void UpdateGraphBG()
    {
    }

    public void DrawGraphBG(Vector2 Offset) => this.BGGG.Draw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, Offset, this.VSCale);
  }
}
