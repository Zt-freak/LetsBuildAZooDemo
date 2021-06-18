// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Collection.CollectionMainPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_Collection.Animals;
using TinyZoo.Z_Collection.Summary;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_Collection
{
  internal class CollectionMainPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AnimalTabmanager tabManager;
    private AnimalCollectionPage animalPage;
    private CollectionSummaryPage summaryPage;
    private TabType currentTab;
    private float refBaseScale;
    private Vector2 hardCodedSize;

    public CollectionMainPanel(Player player, float BaseScale)
    {
      this.refBaseScale = BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.hardCodedSize = new Vector2(uiScaleHelper.ScaleX(550f), uiScaleHelper.ScaleY(424f));
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "BLAH", BaseScale);
      float num = (float) AnimalTabmanager.PreferredWidthOfEachTab_Raw * BaseScale;
      TabType[] tabTypeArray = new TabType[4]
      {
        TabType.Collection_Summary,
        TabType.Collection_Animal,
        TabType.Collection_Employees,
        TabType.Collection_Achievements
      };
      float WidthForTabs = (float) tabTypeArray.Length * num;
      this.tabManager = new AnimalTabmanager(BaseScale, WidthForTabs, tabTypeArray);
      this.bigBrownPanel.Finalize(this.hardCodedSize);
      this.tabManager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
      this.OnTabClicked(tabTypeArray[0], player);
    }

    public void OnTabClicked(TabType tabClicked, Player player)
    {
      if (this.currentTab == tabClicked)
        return;
      Vector2 hardCodedSize = this.hardCodedSize;
      string _NewText = string.Empty;
      if (Z_DebugFlags.IsBetaVersion)
        this.bigBrownPanel.LockForBeta(true);
      switch (tabClicked)
      {
        case TabType.Collection_Animal:
          float y1 = this.bigBrownPanel.GetEdgeBuffers().Y;
          hardCodedSize.Y += y1;
          this.animalPage = new AnimalCollectionPage(CollectionType.Animals, player, this.refBaseScale, hardCodedSize);
          this.animalPage.location.Y -= y1 * 0.5f;
          _NewText = SEngine.Localization.Localization.GetText(855);
          break;
        case TabType.Collection_Employees:
          float y2 = this.bigBrownPanel.GetEdgeBuffers().Y;
          hardCodedSize.Y += y2;
          this.animalPage = new AnimalCollectionPage(CollectionType.EmployeesJobs, player, this.refBaseScale, hardCodedSize);
          this.animalPage.location.Y -= y2 * 0.5f;
          _NewText = SEngine.Localization.Localization.GetText(1040);
          if (Z_DebugFlags.IsBetaVersion)
          {
            this.bigBrownPanel.LockForBeta();
            break;
          }
          break;
        case TabType.Collection_Achievements:
          _NewText = SEngine.Localization.Localization.GetText(1041);
          if (Z_DebugFlags.IsBetaVersion)
          {
            this.bigBrownPanel.LockForBeta();
            break;
          }
          break;
        case TabType.Collection_Summary:
          this.summaryPage = new CollectionSummaryPage(player, this.refBaseScale, hardCodedSize);
          _NewText = SEngine.Localization.Localization.GetText(1039);
          if (Z_DebugFlags.IsBetaVersion)
          {
            this.bigBrownPanel.LockForBeta();
            break;
          }
          break;
      }
      this.currentTab = tabClicked;
      this.bigBrownPanel.SetNewHeading(_NewText);
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return (0 | (this.tabManager.CheckMouseOver(player, offset) ? 1 : 0) | (this.bigBrownPanel.CheckMouseOver(player, offset) ? 1 : 0)) != 0;
    }

    public bool UpdateCollectionMainPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      ref bool WillClearInput)
    {
      offset += this.location;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (!this.bigBrownPanel.CheckCollision(player.inputmap.PointerLocation, offset))
        WillClearInput = false;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      TabType tabClicked = this.tabManager.UpdateAnimalTabmanager(offset, player, DeltaTime);
      if (tabClicked != TabType.Count)
        this.OnTabClicked(tabClicked, player);
      if (this.bigBrownPanel.Active)
      {
        switch (this.currentTab)
        {
          case TabType.Collection_Animal:
          case TabType.Collection_Employees:
            this.animalPage.UpdateAnimalCollectionPage(player, DeltaTime, offset, out bool _);
            break;
          case TabType.Collection_Summary:
            this.summaryPage.UpdateCollectionSummaryFrame();
            break;
        }
      }
      return false;
    }

    public void DrawCollectionMainPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tabManager.PreDrawAnimalTabmanager(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      if (this.bigBrownPanel.Active)
      {
        switch (this.currentTab)
        {
          case TabType.Collection_Animal:
          case TabType.Collection_Employees:
            this.animalPage.DrawAnimalCollectionPage(offset, spriteBatch);
            break;
          case TabType.Collection_Summary:
            this.summaryPage.DrawCollectionSummaryPage(offset, spriteBatch);
            break;
        }
      }
      this.tabManager.DrawAnimalTabmanager(offset, spriteBatch);
      this.bigBrownPanel.DrawDarkOverlay(offset, spriteBatch);
    }
  }
}
