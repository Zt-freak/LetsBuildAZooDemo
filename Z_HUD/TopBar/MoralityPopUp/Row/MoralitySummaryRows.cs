// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row.MoralitySummaryRows
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Morality;

namespace TinyZoo.Z_HUD.TopBar.MoralityPopUp.Row
{
  internal class MoralitySummaryRows
  {
    public Vector2 location;
    private MoralitySummaryRow headerRow;
    private List<MoralitySummaryRow> rows;
    private MoralitySummaryRow unlockRow;
    private Vector2 size;

    public MoralitySummaryRows(Player player, float BaseScale)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.rows = new List<MoralitySummaryRow>();
      this.size = Vector2.Zero;
      this.headerRow = new MoralitySummaryRow(MoralityCategory.Count, player, BaseScale, true);
      this.headerRow.location.Y += this.headerRow.GetSize().Y * 0.5f;
      this.size.Y += this.headerRow.GetSize().Y + defaultBuffer.Y;
      for (int index = 0; index < 6; ++index)
      {
        if (index != 0)
          this.size.Y += defaultBuffer.Y;
        MoralitySummaryRow moralitySummaryRow = new MoralitySummaryRow((MoralityCategory) index, player, BaseScale);
        Vector2 size = moralitySummaryRow.GetSize();
        moralitySummaryRow.location.Y = this.size.Y;
        moralitySummaryRow.location.Y += size.Y * 0.5f;
        this.size.Y += size.Y;
        this.rows.Add(moralitySummaryRow);
      }
      this.size.Y += defaultBuffer.Y;
      this.unlockRow = new MoralitySummaryRow(MoralityCategory.Count, player, BaseScale, _IsUnlockRow: true);
      this.unlockRow.location = this.size;
      this.unlockRow.location.Y += this.unlockRow.GetSize().Y * 0.5f;
      this.size.Y += this.unlockRow.GetSize().Y;
      this.size.X = this.rows[0].GetSize().X;
    }

    public Vector2 GetSize() => this.size;

    public void RefreshValues(Player player)
    {
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].RefreshValues(player);
    }

    public bool UpdateMoralitySummaryRows(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out MoralityCategory categoryClicked,
      out bool ClickedOnUnlocks)
    {
      offset += this.location;
      categoryClicked = MoralityCategory.Count;
      ClickedOnUnlocks = false;
      this.headerRow.UpdateMoralitySummaryRow(player, DeltaTime, offset, out categoryClicked);
      for (int index = 0; index < this.rows.Count; ++index)
      {
        if (this.rows[index].UpdateMoralitySummaryRow(player, DeltaTime, offset, out categoryClicked) && categoryClicked != MoralityCategory.Count)
          return true;
      }
      if (!this.unlockRow.UpdateMoralitySummaryRow(player, DeltaTime, offset, out categoryClicked))
        return false;
      ClickedOnUnlocks = true;
      return true;
    }

    public void DrawMoralitySummaryRows(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.headerRow.DrawMoralitySummaryRow(offset, spriteBatch);
      for (int index = 0; index < this.rows.Count; ++index)
        this.rows[index].DrawMoralitySummaryRow(offset, spriteBatch);
      this.unlockRow.DrawMoralitySummaryRow(offset, spriteBatch);
    }
  }
}
