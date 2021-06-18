// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Profit.SummaryProfitFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Profit
{
  internal class SummaryProfitFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private TableAndTimeSelector table;

    public SummaryProfitFrame(Player player, float BaseScale, Vector2 forcedSize)
    {
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num = 0.0f + defaultYbuffer;
      Vector2 vector2 = forcedSize;
      vector2.Y -= num;
      vector2.Y -= defaultYbuffer;
      this.table = new TableAndTimeSelector(ParkSummaryTableType.SummaryProfit, player, BaseScale, vector2.Y);
      double height = (double) this.table.GetHeight();
      this.table.location.Y = num;
      this.table.location.Y -= forcedSize.Y * 0.5f;
      this.customerFrame = new CustomerFrame(forcedSize, CustomerFrameColors.DarkBrown, BaseScale);
    }

    public void UpdateSummaryProfitFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.table.UpdateTableAndTimeSelector(player, DeltaTime, offset);
    }

    public void DrawSummaryProfitFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.table.DrawTableAndTimeSelector(offset, spriteBatch);
    }
  }
}
