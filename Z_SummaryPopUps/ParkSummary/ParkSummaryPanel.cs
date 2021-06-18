// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.ParkSummary.ParkSummaryPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Finances;
using TinyZoo.Z_SummaryPopUps.ParkSummary.Overview;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_SummaryPopUps.ParkSummary
{
  internal class ParkSummaryPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AnimalTabmanager tabManager;
    private TabType currentTabType;
    private float refBaseScale;
    private Vector2 hardCodedSizeToBeConsistentAcrossPanels;
    private ParkFinancesPanelManager financeManager;
    private ParkOverviewMainFrame parkOverviewFrame;

    public ParkSummaryPanel(Player player, float BaseScale)
    {
      this.refBaseScale = BaseScale;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Park Summary", BaseScale);
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.hardCodedSizeToBeConsistentAcrossPanels = new Vector2(uiScaleHelper.ScaleX(385f), uiScaleHelper.ScaleY(440f));
      this.tabManager = new AnimalTabmanager(BaseScale, uiScaleHelper.ScaleX(100f), new TabType[2]
      {
        TabType.ParkFinances,
        TabType.ParkReputation
      });
      this.bigBrownPanel.Finalize(this.hardCodedSizeToBeConsistentAcrossPanels);
      this.tabManager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
      this.OnTabSwitched(TabType.ParkFinances, player);
    }

    private void OnTabSwitched(TabType newTabType, Player player)
    {
      if (this.currentTabType == newTabType)
        return;
      string _NewText = string.Empty;
      switch (newTabType)
      {
        case TabType.ParkFinances:
          this.financeManager = new ParkFinancesPanelManager(player, this.refBaseScale, this.hardCodedSizeToBeConsistentAcrossPanels);
          _NewText = "Finances";
          break;
        case TabType.ParkReputation:
          this.parkOverviewFrame = new ParkOverviewMainFrame(this.refBaseScale, this.hardCodedSizeToBeConsistentAcrossPanels);
          _NewText = "Reputation";
          break;
      }
      this.currentTabType = newTabType;
      this.bigBrownPanel.SetNewHeading(_NewText);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return (0 | (this.bigBrownPanel.CheckMouseOver(player, offset) ? 1 : 0) | (this.tabManager.CheckMouseOver(player, offset) ? 1 : 0)) != 0;
    }

    public bool UpdateParkSummaryPanel(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      TabType newTabType = this.tabManager.UpdateAnimalTabmanager(offset, player, DeltaTime);
      if (newTabType != TabType.Count)
        this.OnTabSwitched(newTabType, player);
      switch (this.currentTabType)
      {
        case TabType.ParkFinances:
          this.financeManager.UpdateParkFinancesPanelManager(player, DeltaTime, offset);
          break;
        case TabType.ParkReputation:
          this.parkOverviewFrame.UpdateParkOverviewMainFrame(offset);
          break;
      }
      return false;
    }

    public void DrawParkSummaryPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tabManager.PreDrawAnimalTabmanager(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      switch (this.currentTabType)
      {
        case TabType.ParkFinances:
          this.financeManager.DrawParkFinancesPanelManager(offset, spriteBatch);
          break;
        case TabType.ParkReputation:
          this.parkOverviewFrame.DrawParkOverviewMainFrame(offset, spriteBatch);
          break;
      }
      this.tabManager.DrawAnimalTabmanager(offset, spriteBatch);
    }
  }
}
