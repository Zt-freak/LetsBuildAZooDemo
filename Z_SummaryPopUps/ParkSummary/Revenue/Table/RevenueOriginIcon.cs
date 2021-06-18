// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table.RevenueOriginIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.Table
{
  internal class RevenueOriginIcon : GameObject
  {
    public RevenueOriginIcon(FinanceTableRowTypeContainer rowType, float BaseScale)
    {
      this.scale = BaseScale;
      switch (rowType.rowType)
      {
        case FinanceTableRowType.In_EntryTicket_DEP:
        case FinanceTableRowType.Park_Tickets:
          this.DrawRect = new Rectangle(964, 645, 16, 16);
          break;
        case FinanceTableRowType.In_Food_DEP:
        case FinanceTableRowType.ProfitCat_FoodShops:
          this.DrawRect = new Rectangle(972, 502, 15, 14);
          break;
        case FinanceTableRowType.In_Beverage_DEP:
        case FinanceTableRowType.ProfitCat_DrinksShops:
          this.DrawRect = new Rectangle(947, 641, 15, 20);
          break;
        case FinanceTableRowType.In_Souveniers_DEP:
        case FinanceTableRowType.ProfitCat_GiftShops:
          this.DrawRect = new Rectangle(976, 613, 15, 15);
          break;
        case FinanceTableRowType.In_Donations_DEP:
        case FinanceTableRowType.Others_DonationsIn:
        case FinanceTableRowType.Others_DonationsStaffOut:
          this.DrawRect = new Rectangle(633, 474, 22, 17);
          break;
        case FinanceTableRowType.In_Commodities_DEP:
        case FinanceTableRowType.ProfitCat_Commodities:
          this.DrawRect = new Rectangle(951, 572, 18, 16);
          break;
        case FinanceTableRowType.In_BlackMarket_DEP:
        case FinanceTableRowType.Animals_BlackMarketBuy:
        case FinanceTableRowType.Animals_BlackMarketSell:
          this.DrawRect = new Rectangle(835, 473, 19, 19);
          break;
        case FinanceTableRowType.ShopOrFactoryTILETYPE:
          int tileType = (int) rowType.tileType;
          this.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.scale = 16f * BaseScale;
          break;
        default:
          this.DrawRect = TinyZoo.Game1.WhitePixelRect;
          this.scale = 16f * BaseScale;
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height) * this.scale * Sengine.ScreenRatioUpwardsMultiplier;

    public void DrawRevenueOriginIcon(Vector2 offset, SpriteBatch spriteBatch) => this.Draw(spriteBatch, AssetContainer.SpriteSheet, offset);
  }
}
