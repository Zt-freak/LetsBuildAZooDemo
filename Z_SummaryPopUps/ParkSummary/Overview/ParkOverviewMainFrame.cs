// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.Overview.ParkOverviewMainFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary.Overview
{
  internal class ParkOverviewMainFrame
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private MiniHeading miniHeading;

    public ParkOverviewMainFrame(float BaseScale, Vector2 ForcedSize)
    {
      Vector2 vector2_1 = new Vector2(10f, 10f);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      double defaultXbuffer = (double) uiScaleHelper.GetDefaultXBuffer();
      uiScaleHelper.GetDefaultYBuffer();
      this.miniHeading = new MiniHeading(Vector2.Zero, "Park Overview", 1f, BaseScale);
      double num = 0.0 + ((double) this.miniHeading.GetTextHeight(true) + (double) uiScaleHelper.ScaleY(vector2_1.Y));
      this.customerFrame = new CustomerFrame(ForcedSize, CustomerFrameColors.DarkBrown, BaseScale);
      Vector2 vector2_2 = -this.customerFrame.VSCale * 0.5f;
      MiniHeading miniHeading = this.miniHeading;
      miniHeading.vLocation = miniHeading.vLocation + vector2_2;
      if (!Z_DebugFlags.IsBetaVersion)
        return;
      this.customerFrame.LockForBeta();
    }

    public void UpdateParkOverviewMainFrame(Vector2 offset) => offset += this.location;

    public void DrawParkOverviewMainFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.customerFrame.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
