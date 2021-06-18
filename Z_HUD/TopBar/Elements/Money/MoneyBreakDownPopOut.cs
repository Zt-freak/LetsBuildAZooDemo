// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.TopBar.Elements.Money.MoneyBreakDownPopOut
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_HUD.TopBar.MoralityPopUp;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances.Revenue.Table;

namespace TinyZoo.Z_HUD.TopBar.Elements.Money
{
  internal class MoneyBreakDownPopOut : GenericTopBarPopOutFrame
  {
    private TableAndTimeSelector table;

    public MoneyBreakDownPopOut(Player player, float _BaseScale)
      : base(_BaseScale)
    {
      Vector2 zero = Vector2.Zero;
      float forcedHeight = this.scaleHelper.ScaleY(350f);
      this.table = new TableAndTimeSelector(ParkSummaryTableType.SummaryProfit, player, this.BaseScale, forcedHeight, true);
      this.table.location.Y = zero.Y;
      zero.Y += forcedHeight;
      zero.X = this.table.GetWidth();
      zero.X += this.scaleHelper.DefaultBuffer.X * 2f;
      this.FinalizeSize(zero);
      this.table.location.Y += (-zero * 0.5f).Y;
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      this.LockForBeta();
    }

    public override void AddPreviousButton() => this.table.AddPreviousButtonForPopOut();

    public bool UpdateMoneyBreakDownPopOut(Player player, float DeltaTime, Vector2 offset)
    {
      this.UpdatePopOutFrame(player, DeltaTime, ref offset);
      return this.IsPanelActive && this.table.UpdateTableAndTimeSelector(player, DeltaTime, offset);
    }

    public void DrawMoneyBreakDownPopOut(Vector2 offset, SpriteBatch spriteBatch)
    {
      this.DrawPopOutFrame(ref offset, spriteBatch);
      this.table.DrawTableAndTimeSelector(offset, spriteBatch);
      this.PostDrawPopOutFrame(offset, spriteBatch);
    }
  }
}
