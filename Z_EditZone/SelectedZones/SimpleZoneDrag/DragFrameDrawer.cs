// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag.DragFrameDrawer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.Z_EditZone.SelectedZones.SimpleZoneDrag
{
  internal class DragFrameDrawer
  {
    public GameObject gameobject;

    public DragFrameDrawer()
    {
      this.gameobject = new GameObject();
      this.gameobject.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.gameobject.SetAlpha(0.5f);
      this.gameobject.SetDrawOriginToPoint(DrawOriginPosition.TopLeft);
    }

    public void UpdateDragFrame()
    {
    }

    public void DrawDragFrame(Vector2 TopLeft, Vector2 BottomRight)
    {
      this.gameobject.vLocation = TopLeft;
      this.gameobject.WorldOffsetDraw(AssetContainer.pointspritebatchTop05, AssetContainer.SpriteSheet, TopLeft, (BottomRight - TopLeft) * Sengine.ScreenRationReductionMultiplier, 0.0f);
    }
  }
}
