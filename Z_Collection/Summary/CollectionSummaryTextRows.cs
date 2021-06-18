// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.Summary.CollectionSummaryTextRows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Collection.Summary
{
  internal class CollectionSummaryTextRows
  {
    public Vector2 location;
    private List<CollectionSummaryTextRow> textRows;

    public CollectionSummaryTextRows(Player player, float BaseScale, float width)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      width -= uiScaleHelper.GetDefaultXBuffer() * 2f;
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      float num = 0.0f;
      this.textRows = new List<CollectionSummaryTextRow>();
      for (int index = 0; index < 9; ++index)
      {
        CollectionSummaryTextRow collectionSummaryTextRow = new CollectionSummaryTextRow((CollectionSummaryRowType) index, player, BaseScale, width);
        Vector2 size = collectionSummaryTextRow.GetSize();
        collectionSummaryTextRow.location.Y = num + size.Y * 0.5f;
        collectionSummaryTextRow.location.X += size.X * 0.5f;
        num = num + collectionSummaryTextRow.GetSize().Y + defaultYbuffer * 0.5f;
        this.textRows.Add(collectionSummaryTextRow);
      }
    }

    public void DrawCollectionSummaryTextRows(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.textRows.Count; ++index)
        this.textRows[index].DrawCollectionSummaryTextRow(offset, spriteBatch);
    }
  }
}
