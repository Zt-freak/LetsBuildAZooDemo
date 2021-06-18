// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Research_.IconGrid.SegmentedBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_Research_.IconGrid
{
  internal class SegmentedBar
  {
    public Vector2 Location;
    private SatisfactionBar bar;
    private List<PointerAndText> SegmentLines;
    private List<PointerAndText> pointers;
    private float baseScale;
    private float segmentWidth;
    private float lineHeight;

    public SegmentedBar(int segmentCount, int segmentProgress, float _baseScale)
    {
      this.baseScale = _baseScale;
      this.bar = new SatisfactionBar(1f, this.baseScale);
      this.bar.SetBarColours(ColourData.Z_BarDarkBrown);
      this.bar.AddNewBar((float) segmentProgress / (float) segmentCount, ColourData.Z_BarBabyGreen, 1);
      this.segmentWidth = this.bar.GetVScale().X / (float) segmentCount;
      this.lineHeight = this.bar.GetVScale().Y / this.baseScale;
      this.SegmentLines = new List<PointerAndText>();
      for (int index = 0; index < segmentCount - 1; ++index)
      {
        PointerAndText pointerAndText = new PointerAndText("", 1f, this.baseScale, this.lineHeight);
        pointerAndText.vLocation.X = (float) ((double) this.segmentWidth * (double) (index + 1) - (double) this.bar.GetVScale().X * 0.5);
        this.SegmentLines.Add(pointerAndText);
      }
      this.pointers = new List<PointerAndText>();
    }

    public void SetPointer(int segment, string text)
    {
      PointerAndText pointerAndText = new PointerAndText(text, 1f, this.baseScale, this.lineHeight + 6f);
      pointerAndText.vLocation.X = (float) ((double) this.segmentWidth * (double) segment - (double) this.bar.GetVScale().X * 0.5);
      this.pointers.Add(pointerAndText);
    }

    public void UpdateSegmentedBar()
    {
    }

    public void DrawSegmentedBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.bar.DrawSatisfactionBar(offset, spriteBatch);
      for (int index = 0; index < this.SegmentLines.Count; ++index)
        this.SegmentLines[index].DrawPointerAndText(offset);
      for (int index = 0; index < this.pointers.Count; ++index)
        this.pointers[index].DrawPointerAndText(offset);
    }
  }
}
