// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue.ParkRevenueSummaryFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Revenue
{
  internal class ParkRevenueSummaryFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private TableAndTimeSelector table;
    private Vector2 size;

    public ParkRevenueSummaryFrame(
      Player player,
      float BaseScale,
      Vector2 forcedSize,
      ParkSummaryTableType tableType)
    {
      this.size = Vector2.Zero;
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.size.Y += defaultBuffer.Y;
      Vector2 vector2 = forcedSize;
      forcedSize.Y -= this.size.Y;
      forcedSize.Y -= defaultBuffer.Y;
      this.table = new TableAndTimeSelector(tableType, player, BaseScale, vector2.Y);
      this.table.location.Y = this.size.Y;
      this.size.Y += this.table.GetHeight();
      this.size.Y += defaultBuffer.Y;
      this.size.X = this.table.GetWidth();
      this.customerFrame = new CustomerFrame(forcedSize, CustomerFrameColors.DarkBrown, BaseScale);
      this.table.location.Y += (-this.customerFrame.VSCale * 0.5f).Y;
    }

    public Vector2 GetSize(bool GetContentsSize) => GetContentsSize ? this.size : this.customerFrame.VSCale;

    public void UpdateParkRevenueSummaryFrame(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.table.UpdateTableAndTimeSelector(player, DeltaTime, offset);
    }

    public void DrawParkRevenueSummaryFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.table.DrawTableAndTimeSelector(offset, spriteBatch);
    }
  }
}
