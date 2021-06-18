// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable.RowSegmentRectangle
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_ManageEmployees.EmployeeView.PerformanceTable
{
  internal class RowSegmentRectangle : GameObject
  {
    public Vector2 vScale;

    public RowSegmentRectangle(float width, float height)
    {
      this.SetUp(width, height);
      this.SetAllColours(Color.Black.ToVector3());
      this.SetAlpha(0.1f);
    }

    public RowSegmentRectangle(float width, float height, Vector3 thisColour, float alpha = 0.1f)
    {
      this.SetUp(width, height);
      this.SetAllColours(thisColour);
      this.SetAlpha(alpha);
    }

    private void SetUp(float width, float height)
    {
      this.DrawRect = TinyZoo.Game1.WhitePixelRect;
      this.vScale = new Vector2(width, height);
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => this.vScale;

    public void DrawRowSegmentRectangle(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset, this.vScale, this.fAlpha);
  }
}
