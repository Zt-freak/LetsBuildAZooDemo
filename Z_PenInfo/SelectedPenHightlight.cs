// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_PenInfo.SelectedPenHightlight
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_PenInfo
{
  internal class SelectedPenHightlight : GameObject
  {
    private GameObject WhiteFrame;

    public SelectedPenHightlight(Vector2Int Loc)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.SetDrawOriginToCentre();
      this.SetAllColours(0.1f, 0.1f, 0.7f);
      this.scale = 16f;
      this.vLocation = TileMath.GetTileToWorldSpace(Loc);
      this.SetAlpha(0.7f);
      this.WhiteFrame = new GameObject((GameObject) this);
      this.WhiteFrame.DrawRect = new Rectangle(345, 88, 17, 17);
      this.WhiteFrame.SetDrawOriginToCentre();
      this.WhiteFrame.scale = 1f;
      this.WhiteFrame.SetAlpha(0.3f);
      this.WhiteFrame.SetAllColours(1f, 1f, 1f);
    }

    public void DrawSelectedPenHightlight()
    {
      this.WorldOffsetDraw(AssetContainer.PointBlendBatch04, AssetContainer.SpriteSheet);
      this.WhiteFrame.vLocation = this.vLocation;
      this.WhiteFrame.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }
  }
}
