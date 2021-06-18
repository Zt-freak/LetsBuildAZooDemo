// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleInParkPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark
{
  internal class PeopleInParkPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private AnimalTabmanager tabsManager;
    private TabType currentTab;
    private Vector2 hardCodedSize;
    private PeopleViewer page;
    private float BaseScale;

    public PeopleInParkPanel(float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "BLAH", this.BaseScale);
      this.hardCodedSize = uiScaleHelper.ScaleVector2(new Vector2(385f, 516f));
      TabType[] tabTypeArray = new TabType[2]
      {
        TabType.People_VIPs,
        TabType.People_Customers
      };
      this.tabsManager = new AnimalTabmanager(this.BaseScale, (float) AnimalTabmanager.PreferredWidthOfEachTab_Raw * this.BaseScale * (float) tabTypeArray.Length, tabTypeArray);
      this.bigBrownPanel.Finalize(this.hardCodedSize);
      this.tabsManager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
      this.OnTabClicked(tabTypeArray[0]);
    }

    private void OnTabClicked(TabType tabClicked)
    {
      if (this.currentTab == tabClicked)
        return;
      string _NewText = string.Empty;
      switch (tabClicked)
      {
        case TabType.People_VIPs:
          this.page = new PeopleViewer(this.GetTabClickedToPeopleFilter(tabClicked), this.BaseScale, this.hardCodedSize);
          _NewText = "VIPs";
          break;
        case TabType.People_Customers:
          this.page = new PeopleViewer(this.GetTabClickedToPeopleFilter(tabClicked), this.BaseScale, this.hardCodedSize);
          _NewText = "Visitors";
          break;
      }
      this.bigBrownPanel.SetNewHeading(_NewText);
      this.currentTab = tabClicked;
    }

    private PeopleInParkFilter GetTabClickedToPeopleFilter(TabType tabClicked)
    {
      if (tabClicked == TabType.People_VIPs)
        return PeopleInParkFilter.VIPs;
      return tabClicked == TabType.People_Customers ? PeopleInParkFilter.Customers : PeopleInParkFilter.Count;
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.location;
      return this.bigBrownPanel.CheckMouseOver(player, offset);
    }

    public bool UpdatePeopleInParkPanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out WalkingPerson goToThisPerson)
    {
      offset += this.location;
      goToThisPerson = (WalkingPerson) null;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
        return true;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      TabType tabClicked = this.tabsManager.UpdateAnimalTabmanager(offset, player, DeltaTime);
      if (tabClicked != TabType.Count)
        this.OnTabClicked(tabClicked);
      if (this.page != null)
        goToThisPerson = this.page.UpdatePeopleViewer(player, DeltaTime, offset);
      return false;
    }

    public void DrawPeopleInParkPanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.tabsManager.PreDrawAnimalTabmanager(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset, spriteBatch);
      this.tabsManager.DrawAnimalTabmanager(offset, spriteBatch);
      if (this.page == null)
        return;
      this.page.DrawPeopleViewer(offset, spriteBatch);
    }
  }
}
