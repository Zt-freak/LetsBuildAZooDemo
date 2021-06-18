// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items.PenItemDuplicateInfo
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Z_Animal_Data;

namespace TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items
{
  internal class PenItemDuplicateInfo
  {
    public ENRICHMENTCLASS enrichmentclass;
    public int TotalOfThese;

    public PenItemDuplicateInfo(ENRICHMENTCLASS _enrichmentclass)
    {
      this.TotalOfThese = 1;
      this.enrichmentclass = _enrichmentclass;
    }

    public float GetTotalPoints(
      float BasePointsForThisEnrichmentClassAndThisAnimal,
      int _TotalOfTheseEnrishmentItemsInPen = -1)
    {
      if (_TotalOfTheseEnrishmentItemsInPen == -1)
        _TotalOfTheseEnrishmentItemsInPen = this.TotalOfThese;
      if (_TotalOfTheseEnrishmentItemsInPen == 1)
        return BasePointsForThisEnrichmentClassAndThisAnimal;
      if (_TotalOfTheseEnrishmentItemsInPen == 2)
        return BasePointsForThisEnrichmentClassAndThisAnimal * 1.75f;
      return _TotalOfTheseEnrishmentItemsInPen > 2 ? (float) ((double) BasePointsForThisEnrichmentClassAndThisAnimal * 1.75 + 0.5 * ((double) _TotalOfTheseEnrishmentItemsInPen - 2.0)) : 0.0f;
    }

    public PenItemDuplicateInfo(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("c", ref _out);
      this.enrichmentclass = (ENRICHMENTCLASS) _out;
      int num2 = (int) reader.ReadInt("c", ref this.TotalOfThese);
    }

    public void SavePenItemDuplicateInfo(Writer writer)
    {
      writer.WriteInt("c", (int) this.enrichmentclass);
      writer.WriteInt("c", this.TotalOfThese);
    }
  }
}
