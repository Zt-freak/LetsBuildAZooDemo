// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_WeekOver.V2.EndPOfWeekSummaryManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_WeekOver.V2
{
  internal class EndPOfWeekSummaryManager
  {
    private CubeLayoutController cubelayoutcontroller;
    internal static float SIZE = 200f;
    internal static float BaseScale;
    private BigBrownPanel brownpanel;
    private Vector2 Location;
    private CustomerFrame customer;

    public EndPOfWeekSummaryManager(Player player)
    {
      EndPOfWeekSummaryManager.BaseScale = RenderMath.GetPixelSizeBestMatch(1f);
      EndPOfWeekSummaryManager.SIZE = 200f;
      this.cubelayoutcontroller = new CubeLayoutController(EndPOfWeekSummaryManager.BaseScale, player);
      this.brownpanel = new BigBrownPanel(Vector2.One, true, "END OF WEEK SUMMARY", EndPOfWeekSummaryManager.BaseScale);
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.customer = new CustomerFrame(new Vector2(10f * baseScaleForUi, 10f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y) + new Vector2(EndPOfWeekSummaryManager.SIZE * 5f * baseScaleForUi, EndPOfWeekSummaryManager.SIZE * 2f * baseScaleForUi * Sengine.ScreenRatioUpwardsMultiplier.Y), true, baseScaleForUi);
      this.brownpanel.Finalize(this.customer.VSCale);
      this.brownpanel.BlockCloseButton = true;
      this.Location = new Vector2(512f, 384f);
      this.Location.Y += this.brownpanel.InternalOffset.Y * 0.5f;
    }

    public bool UpdateEndPOfWeekSummaryManager(Player player, float DeltaTime)
    {
      bool BlockClose;
      this.cubelayoutcontroller.UpdateCubeLayoutController(player, DeltaTime, this.Location, out BlockClose);
      this.brownpanel.BlockCloseButton = BlockClose;
      return this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location);
    }

    public void DrawEndPOfWeekSummaryManager()
    {
      this.brownpanel.DrawBigBrownPanel(this.Location, AssetContainer.pointspritebatchTop05);
      this.customer.DrawCustomerFrame(this.Location, AssetContainer.pointspritebatchTop05);
      this.cubelayoutcontroller.DrawCubeLayoutController(this.Location);
    }
  }
}
