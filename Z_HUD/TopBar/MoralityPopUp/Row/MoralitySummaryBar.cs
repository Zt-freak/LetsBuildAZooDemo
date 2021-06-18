// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row.MoralitySummaryBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_HUD.TopBar.RatingPopUp.Row.Columns;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row
{
  internal class MoralitySummaryBar : SatisfactionBarWithBigNumber
  {
    private MoralityCategory moralityCategory;

    public MoralitySummaryBar(MoralityCategory _moralityCategory, Player player, float BaseScale)
      : base(BaseScale, 10f, true)
    {
      this.moralityCategory = _moralityCategory;
      this.RefreshValues(player);
    }

    public void RefreshValues(Player player)
    {
      float totalMax;
      float categoryMoralityScore = MoralityData.GetCategoryMoralityScore(this.moralityCategory, player, out totalMax);
      this.SetGoodness_ForMorality((double) categoryMoralityScore >= 0.0);
      float percent_float = Math.Abs(categoryMoralityScore / totalMax);
      if ((double) totalMax == 0.0)
        percent_float = 0.0f;
      this.SetBarValues(percent_float, MoralityData.GetDisplayStringForMoralityValue(categoryMoralityScore));
    }

    public void DrawMoralitySummaryBar(Vector2 offset, SpriteBatch spriteBatch) => this.DrawSatisfactionBarWithBigNumber(offset, spriteBatch);
  }
}
