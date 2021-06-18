// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI.SmallESelectedHighlight
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SelectedPersonUI
{
  internal class SmallESelectedHighlight
  {
    private GameObject TopLeft;
    private GameObject TopRight;
    private GameObject BottomLeft;
    private GameObject BottomRight;

    public SmallESelectedHighlight()
    {
      float num = 11f;
      this.TopLeft = new GameObject();
      this.TopLeft.DrawRect = new Rectangle(128, 12, 8, 8);
      this.TopLeft.DrawOrigin = new Vector2(num, num);
      this.TopRight = new GameObject();
      this.TopRight.DrawRect = new Rectangle(137, 12, 8, 8);
      this.TopRight.DrawOrigin = new Vector2(8f - num, num);
      this.BottomLeft = new GameObject();
      this.BottomLeft.DrawRect = new Rectangle(128, 21, 8, 8);
      this.BottomLeft.DrawOrigin = new Vector2(num, 8f - num);
      this.BottomRight = new GameObject();
      this.BottomRight.DrawRect = new Rectangle(137, 21, 8, 8);
      this.BottomRight.DrawOrigin = new Vector2(8f - num, 8f - num);
    }

    public void UpdateSmallESelectedHighlight()
    {
    }

    public void DrawSmallESelectedHighlight(Vector2 PersonLocation)
    {
      PersonLocation.Y -= 4f;
      this.TopLeft.vLocation = PersonLocation;
      this.TopRight.vLocation = PersonLocation;
      this.BottomLeft.vLocation = PersonLocation;
      this.BottomRight.vLocation = PersonLocation;
      this.TopLeft.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.TopRight.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.BottomLeft.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
      this.BottomRight.WorldOffsetDraw(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet);
    }
  }
}
