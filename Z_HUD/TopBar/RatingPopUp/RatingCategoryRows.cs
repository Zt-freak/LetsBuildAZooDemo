// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.RatingPopUp.RatingCategoryRows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_HUD.TopBar.RatingPopUp
{
  internal class RatingCategoryRows
  {
    public Vector2 location;
    private RatingCategoryRow header;
    private List<RatingCategoryRow> rows;
    private float totalHeight;

    public RatingCategoryRows(Player player, float BaseScale)
    {
      float num = new UIScaleHelper(BaseScale).ScaleY(5f);
      this.header = new RatingCategoryRow(RatingCategory.Count, player, BaseScale, true);
      this.header.location.Y += this.header.GetSize().Y * 0.5f;
      this.totalHeight += this.header.GetSize().Y + num;
      this.rows = new List<RatingCategoryRow>();
      for (int index = 0; index < 4; ++index)
      {
        if (index != 0)
          this.totalHeight += num;
        RatingCategoryRow ratingCategoryRow = new RatingCategoryRow((RatingCategory) index, player, BaseScale);
        Vector2 size = ratingCategoryRow.GetSize();
        ratingCategoryRow.location.Y = this.totalHeight;
        ratingCategoryRow.location.Y += size.Y * 0.5f;
        this.totalHeight += size.Y;
        this.rows.Add(ratingCategoryRow);
      }
    }

    public Vector2 GetSize() => new Vector2(this.rows[0].GetSize().X, this.totalHeight);

    public void RefreshValues(Player player)
    {
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].RefreshValues(player);
    }

    public void UpdateRatingCategoryRows(Player player, Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].UpdateRatingCategoryRow(player, offset);
    }

    public void DrawRatingCategoryRows(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.header.DrawRatingCategoryRow(offset, spriteBatch);
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].DrawRatingCategoryRow(offset, spriteBatch);
    }
  }
}
