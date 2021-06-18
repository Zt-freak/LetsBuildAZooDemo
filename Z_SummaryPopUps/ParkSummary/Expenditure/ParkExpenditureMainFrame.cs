// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Expenditure.ParkExpenditureMainFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Expenditure
{
  internal class ParkExpenditureMainFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private TableAndTimeSelector table;

    public ParkExpenditureMainFrame(Player player, float BaseScale, Vector2 ForcedSize)
    {
      float num1 = 0.0f;
      float defaultYbuffer = new UIScaleHelper(BaseScale).GetDefaultYBuffer();
      float num2 = num1 + defaultYbuffer;
      this.table = new TableAndTimeSelector(ParkSummaryTableType.Expenditure, player, BaseScale, ForcedSize.Y);
      this.table.location.Y = num2;
      float num3 = num2 + defaultYbuffer;
      this.customerFrame = new CustomerFrame(ForcedSize, CustomerFrameColors.DarkBrown, BaseScale);
      this.table.location.Y += (-this.customerFrame.VSCale * 0.5f).Y;
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      this.customerFrame.LockForBeta();
    }

    public void UpdateParkExpenditureMainFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      if (!this.customerFrame.Active)
        return;
      this.table.UpdateTableAndTimeSelector(player, DeltaTime, offset);
    }

    public void DrawParkExpenditureMainFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      if (this.customerFrame.Active)
        this.table.DrawTableAndTimeSelector(offset, spriteBatch);
      this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
