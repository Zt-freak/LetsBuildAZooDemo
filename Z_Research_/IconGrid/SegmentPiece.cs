// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.SegmentPiece
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class SegmentPiece
  {
    public Vector2 location;
    private GameObject segment;
    private SegmentPieceType type;

    public SegmentPiece(SegmentPieceType _type, bool isFilled, float BaseScale, int maxWidth = -1)
    {
      this.type = _type;
      this.segment = new GameObject();
      this.segment.scale = BaseScale;
      switch (this.type)
      {
        case SegmentPieceType.Left:
          this.segment.DrawRect = !isFilled ? new Rectangle(918, 235, 15, 10) : new Rectangle(937, 449, 15, 10);
          if (maxWidth != -1 && (double) maxWidth < (double) this.segment.DrawRect.Width * (double) this.segment.scale)
          {
            this.segment.DrawRect.Width = (int) ((double) maxWidth / (double) this.segment.scale);
            break;
          }
          break;
        case SegmentPieceType.Middle:
          this.segment.DrawRect = !isFilled ? new Rectangle(933, 235, 15, 10) : new Rectangle(955, 449, 15, 10);
          if (maxWidth != -1 && (double) maxWidth < (double) this.segment.DrawRect.Width * (double) this.segment.scale)
          {
            this.segment.DrawRect.Width = (int) ((double) maxWidth / (double) this.segment.scale);
            break;
          }
          break;
        case SegmentPieceType.Right:
          this.segment.DrawRect = !isFilled ? new Rectangle(967, 235, 15, 10) : new Rectangle(975, 449, 15, 10);
          if (maxWidth != -1 && (double) maxWidth < (double) this.segment.DrawRect.Width * (double) this.segment.scale)
          {
            this.segment.DrawRect.X += 15 - (int) ((double) maxWidth / (double) this.segment.scale);
            this.segment.DrawRect.Width = (int) ((double) maxWidth / (double) this.segment.scale);
            break;
          }
          break;
      }
      this.segment.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.segment.DrawRect.Width * this.segment.scale, (float) this.segment.DrawRect.Height * this.segment.scale);

    public void DrawSegmentPiece(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.segment.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
    }
  }
}
