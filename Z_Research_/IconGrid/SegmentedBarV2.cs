// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.SegmentedBarV2
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class SegmentedBarV2
  {
    public Vector2 Location;
    private List<SegmentPiece> segmentPieces;
    private List<PointerAndText> pointers;
    private float baseScale;
    private float segmentWidth;
    private float lineHeight;
    private float XcenterOffset;
    private List<PointerAndText> lines;

    public SegmentedBarV2(int segmentCount, int segmentsFilled, float _baseScale, float maxWidth = -1f)
    {
      this.baseScale = _baseScale;
      this.segmentPieces = new List<SegmentPiece>();
      this.lines = new List<PointerAndText>();
      int maxWidth1 = -1;
      if ((double) maxWidth != -1.0)
        maxWidth1 = (int) maxWidth / segmentCount;
      for (int index = 0; index < segmentCount; ++index)
      {
        SegmentPieceType _type = index != 0 ? (index != segmentCount - 1 ? SegmentPieceType.Middle : SegmentPieceType.Right) : SegmentPieceType.Left;
        SegmentPiece segmentPiece = new SegmentPiece(_type, index < segmentsFilled, this.baseScale, maxWidth1);
        float x = segmentPiece.GetSize().X;
        segmentPiece.location.X = x * (float) index;
        segmentPiece.location.X += x * 0.5f;
        this.segmentPieces.Add(segmentPiece);
        PointerAndText pointerAndText = new PointerAndText("", 1f, this.baseScale, segmentPiece.GetSize().Y / this.baseScale);
        switch (_type)
        {
          case SegmentPieceType.Left:
            pointerAndText.vLocation.X += segmentPiece.location.X + segmentPiece.GetSize().X * 0.5f;
            break;
          case SegmentPieceType.Middle:
            pointerAndText.vLocation.X += segmentPiece.location.X + segmentPiece.GetSize().X * 0.5f;
            break;
          case SegmentPieceType.Right:
            pointerAndText.vLocation.X += segmentPiece.location.X - segmentPiece.GetSize().X * 0.5f;
            break;
        }
        this.lines.Add(pointerAndText);
      }
      this.pointers = new List<PointerAndText>();
      this.XcenterOffset = (float) (-(double) this.GetBarSize().X * 0.5);
    }

    public void SetPointer(int segment, string text)
    {
      PointerAndText pointerAndText = new PointerAndText(text, 1f, this.baseScale, (float) ((double) this.segmentPieces[0].GetSize().Y / (double) this.baseScale + 6.0 * (double) Sengine.ScreenRatioUpwardsMultiplier.Y));
      pointerAndText.vLocation.X = this.segmentPieces[0].GetSize().X * (float) segment;
      if (segment == this.segmentPieces.Count)
        pointerAndText.vLocation.X -= 2f * this.baseScale;
      this.pointers.Add(pointerAndText);
    }

    public Vector2 GetBarSize()
    {
      Vector2 vector2 = new Vector2(this.segmentPieces[0].GetSize().X * (float) this.segmentPieces.Count, this.segmentPieces[0].GetSize().Y);
      if (this.pointers.Count > 0)
        vector2.Y += this.pointers[0].GetLineVScale().Y - vector2.Y;
      return vector2;
    }

    public float GetExtraHeightFromText() => this.pointers.Count > 0 ? this.pointers[0].GetLineAndTextHeight() - this.pointers[0].GetLineVScale().Y : 0.0f;

    public void DrawSegmentedBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.Location;
      offset.X += this.XcenterOffset;
      for (int index = 0; index < this.segmentPieces.Count; ++index)
        this.segmentPieces[index].DrawSegmentPiece(offset, spriteBatch);
      for (int index = 0; index < this.lines.Count; ++index)
        this.lines[index].DrawPointerAndText(offset, spriteBatch);
      for (int index = 0; index < this.pointers.Count; ++index)
        this.pointers[index].DrawPointerAndText(offset, spriteBatch);
    }
  }
}
