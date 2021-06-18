// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.ParkFinancesPanelManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Profit;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances
{
  internal class ParkFinancesPanelManager
  {
    public Vector2 location;
    private ParkRevenueSummaryFrame profitBreakdownFrame;
    private SummaryProfitFrame summaryProfitFrame;
    private ParkFinancePanelPage currentPage;
    private HeaderAndArrows headerAndArrows;
    private Vector2 refForcedSize;
    private float refBaseScale;
    private float Ybuffer;

    public ParkFinancesPanelManager(Player player, float BaseScale, Vector2 forcedSize)
    {
      this.refForcedSize = forcedSize;
      this.refBaseScale = BaseScale;
      this.Ybuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      this.headerAndArrows = new HeaderAndArrows(BaseScale, forcedSize.X);
      this.headerAndArrows.location.Y -= (float) ((double) forcedSize.Y * 0.5 - (double) this.headerAndArrows.GetSize().Y * 0.5);
      this.ChangePage(ParkFinancePanelPage.SummaryProfit, player);
    }

    private void ChangePage(ParkFinancePanelPage thisPage, Player player)
    {
      Vector2 size = this.headerAndArrows.GetSize();
      size.Y += this.Ybuffer;
      Vector2 refForcedSize = this.refForcedSize;
      refForcedSize.Y -= size.Y;
      string newText = string.Empty;
      switch (thisPage)
      {
        case ParkFinancePanelPage.SummaryProfit:
          newText = "Profit Summary";
          this.summaryProfitFrame = new SummaryProfitFrame(player, this.refBaseScale, refForcedSize);
          this.summaryProfitFrame.location.Y += size.Y * 0.5f;
          break;
        case ParkFinancePanelPage.ProfitBreakdown:
          this.profitBreakdownFrame = new ParkRevenueSummaryFrame(player, this.refBaseScale, refForcedSize, ParkSummaryTableType.ProfitBreakdown);
          this.profitBreakdownFrame.location.Y += size.Y * 0.5f;
          newText = "Profit Breakdown";
          break;
      }
      this.currentPage = thisPage;
      this.headerAndArrows.ChangeHeaderText(newText);
    }

    public void UpdateParkFinancesPanelManager(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      switch (this.currentPage)
      {
        case ParkFinancePanelPage.SummaryProfit:
          this.summaryProfitFrame.UpdateSummaryProfitFrame(player, DeltaTime, offset);
          break;
        case ParkFinancePanelPage.ProfitBreakdown:
          this.profitBreakdownFrame.UpdateParkRevenueSummaryFrame(player, DeltaTime, offset);
          break;
      }
      int num1 = this.headerAndArrows.UpdateHeaderAndArrows(player, DeltaTime, offset);
      if (num1 == 0)
        return;
      int num2 = (int) (this.currentPage + num1);
      if (num2 == 2)
        num2 = 0;
      else if (num2 < 0)
        num2 = 1;
      this.ChangePage((ParkFinancePanelPage) num2, player);
    }

    public void DrawParkFinancesPanelManager(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      switch (this.currentPage)
      {
        case ParkFinancePanelPage.SummaryProfit:
          this.summaryProfitFrame.DrawSummaryProfitFrame(offset, spriteBatch);
          break;
        case ParkFinancePanelPage.ProfitBreakdown:
          this.profitBreakdownFrame.DrawParkRevenueSummaryFrame(offset, spriteBatch);
          break;
      }
      this.headerAndArrows.DrawHeaderAndArrows(offset, spriteBatch);
    }
  }
}
