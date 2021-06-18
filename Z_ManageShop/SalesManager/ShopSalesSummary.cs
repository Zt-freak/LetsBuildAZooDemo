// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.SalesManager.ShopSalesSummary
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.GenericUI;
using TinyZoo.Z_ManageShop.AllShopSummary;

namespace TinyZoo.Z_ManageShop.SalesManager
{
  internal class ShopSalesSummary
  {
    private SimpleTextHandler paragraph;
    private ShopSummaryRow shopsummaryrow;
    public Vector2 Location;

    public ShopSalesSummary(Player player, float BaseScale) => this.shopsummaryrow = new ShopSummaryRow(player.shopstatus.GetThisShop(player.livestats.SelectedSHop.Location, player.livestats.SelectedSHop.tiletype), player, new Vector2(600f, 100f), BaseScale);

    public Vector2 GetSize() => this.shopsummaryrow.GetSize();

    public void UpdateSalesSummary()
    {
    }

    public void SetExtraIngredient(float Perc) => this.shopsummaryrow.SetExtraIngredient(Perc);

    public void DrawSalesSummary(Vector2 Offset)
    {
      Offset += this.Location;
      this.shopsummaryrow.DrawShopSummaryRow(Offset);
    }
  }
}
