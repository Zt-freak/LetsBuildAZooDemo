// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.PeopleViewPagesContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row
{
  internal class PeopleViewPagesContainer
  {
    public Vector2 location;
    private List<PeopleViewPage> pages;
    private float BaseScale;
    private float ForcedWidth;
    private UIScaleHelper scaleHelper;
    private int LastPageIndex;
    private bool PeopleEntered_PopulateLastPage;
    private PeopleInParkFilter refFilterType;
    private PeopleViewInfoType refInfoType;

    public int currentPageIndex { get; private set; }

    public PeopleViewPagesContainer(
      PeopleInParkFilter peopleFilterType,
      float _BaseScale,
      float _ForcedWidth)
    {
      this.BaseScale = _BaseScale;
      this.ForcedWidth = _ForcedWidth;
      this.refFilterType = peopleFilterType;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.currentPageIndex = -1;
      this.LastPageIndex = -1;
      this.PeopleEntered_PopulateLastPage = false;
      this.refInfoType = PeopleViewInfoType.Count;
      this.pages = new List<PeopleViewPage>();
      this.GoToThisPage(0);
    }

    public void SetInfoType(PeopleViewInfoType infoType)
    {
      this.refInfoType = infoType;
      this.pages[this.currentPageIndex].SetInfoType(infoType);
    }

    public void ChangePage(int forwardOrBack)
    {
      int pageIndex = this.currentPageIndex + forwardOrBack;
      if (pageIndex < 0)
        pageIndex = 0;
      else if (this.LastPageIndex != -1 && pageIndex > this.LastPageIndex)
        pageIndex = this.LastPageIndex;
      this.GoToThisPage(pageIndex);
    }

    public bool IsOnLastPage() => this.LastPageIndex != -1 && this.currentPageIndex == this.LastPageIndex;

    public void TryRefresh()
    {
      this.PeopleEntered_PopulateLastPage = true;
      this.GoToThisPage(this.currentPageIndex);
    }

    private void GoToThisPage(int pageIndex)
    {
      Console.WriteLine("Going to page: " + (object) pageIndex);
      bool flag1 = pageIndex > this.pages.Count - 1;
      bool flag2 = !flag1 && this.pages[pageIndex].imTheLastPage && this.PeopleEntered_PopulateLastPage;
      if (this.currentPageIndex == pageIndex && !flag2)
        return;
      if (flag2)
      {
        int withLastCustomerUid = this.GetNewStartingListIndexWithLastCustomerUID(this.pages[pageIndex].lastPersonCustomerUID);
        this.pages[pageIndex].PopulateRows(withLastCustomerUid + 1);
        this.PeopleEntered_PopulateLastPage = false;
      }
      if (flag1)
      {
        Console.WriteLine("CreateBrandNewPage " + (object) pageIndex);
        int startIndex = 0;
        if (pageIndex > 0 && this.pages[pageIndex - 1] != null)
          startIndex = this.pages[pageIndex - 1].lastPersonIndex + 1;
        this.pages.Add(new PeopleViewPage(pageIndex, startIndex, this.BaseScale, this.ForcedWidth, this.refFilterType));
      }
      if (this.pages[pageIndex].imTheLastPage)
        this.LastPageIndex = pageIndex;
      else if (this.LastPageIndex == pageIndex)
        this.LastPageIndex = -1;
      Console.WriteLine("LastPageIndex: " + (object) this.LastPageIndex);
      this.currentPageIndex = pageIndex;
      this.SetInfoType(this.refInfoType);
    }

    private int GetNewStartingListIndexWithLastCustomerUID(int customerUID)
    {
      int num = -1;
      List<WalkingPerson> walkingPeoplePointer = PeopleViewer.refWalkingPeoplePointer;
      for (int index = walkingPeoplePointer.Count - 1; index > -1 && walkingPeoplePointer[index].UID >= customerUID; --index)
        num = index;
      return num;
    }

    public Vector2 GetFullSize() => this.pages[0].GetSize();

    public Vector2 GetSize() => this.pages.Count == 0 ? Vector2.Zero : this.pages[this.currentPageIndex].GetSize();

    public WalkingPerson UpdatePeopleViewPagesContainer(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      return this.pages[this.currentPageIndex].UpdatePeopleViewPage(player, DeltaTime, offset);
    }

    public void DrawPeopleViewPagesContainer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.pages[this.currentPageIndex].DrawPeopleViewPage(offset, spriteBatch);
    }
  }
}
