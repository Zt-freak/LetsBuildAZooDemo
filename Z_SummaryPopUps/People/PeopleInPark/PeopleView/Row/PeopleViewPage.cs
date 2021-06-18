// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.PeopleViewPage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row
{
  internal class PeopleViewPage
  {
    public Vector2 location;
    private List<PeopleInParkRow> peopleRows;
    private float totalHeight;
    private UIScaleHelper scaleHelper;
    private float Ybuffer;
    private float BaseScale;
    private PeopleInParkFilter peopleFilterType;
    internal const int numberOfPeoplePerPage = 10;

    public int lastPersonIndex { get; private set; }

    public int lastPersonCustomerUID { get; private set; }

    public bool imTheLastPage { get; private set; }

    public int numberOfPeoplePopulatedInThisPage { get; private set; }

    public PeopleViewPage(
      int pageIndex,
      int startIndex,
      float _BaseScale,
      float forcedWidth,
      PeopleInParkFilter _peopleFilterType)
    {
      this.BaseScale = _BaseScale;
      this.peopleFilterType = _peopleFilterType;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      this.Ybuffer = this.scaleHelper.GetDefaultYBuffer() * 0.5f;
      this.lastPersonIndex = -1;
      this.imTheLastPage = true;
      this.lastPersonCustomerUID = -1;
      this.peopleRows = new List<PeopleInParkRow>();
      this.SetUpEmptyRows(this.BaseScale, forcedWidth);
      this.PopulateRows(startIndex);
    }

    public void SetUpEmptyRows(float BaseScale, float forcedWidth)
    {
      this.totalHeight = 0.0f;
      for (int index = 0; index < 10; ++index)
      {
        if (index != 0)
          this.totalHeight += this.Ybuffer;
        PeopleInParkRow peopleInParkRow = new PeopleInParkRow(BaseScale, forcedWidth);
        Vector2 size = peopleInParkRow.GetSize();
        peopleInParkRow.location.Y = this.totalHeight;
        peopleInParkRow.location.Y += size.Y * 0.5f;
        this.totalHeight += size.Y;
        this.peopleRows.Add(peopleInParkRow);
      }
    }

    public void PopulateRows(int startIndex)
    {
      this.imTheLastPage = false;
      bool flag = false;
      List<WalkingPerson> walkingPeoplePointer = PeopleViewer.refWalkingPeoplePointer;
      for (int index = startIndex; index < walkingPeoplePointer.Count; ++index)
      {
        if (this.numberOfPeoplePopulatedInThisPage == 10 && !this.imTheLastPage)
        {
          flag = true;
          this.imTheLastPage = true;
        }
        if (PeopleViewPage.ShouldIncludeThisPerson(this.peopleFilterType, walkingPeoplePointer[index]))
        {
          if (flag)
          {
            this.imTheLastPage = false;
            break;
          }
          this.peopleRows[this.numberOfPeoplePopulatedInThisPage].PopulateRow(walkingPeoplePointer[index]);
          ++this.numberOfPeoplePopulatedInThisPage;
          this.lastPersonIndex = index;
          this.lastPersonCustomerUID = walkingPeoplePointer[index].UID;
        }
        if (index == walkingPeoplePointer.Count - 1)
        {
          if (this.numberOfPeoplePopulatedInThisPage <= 10)
          {
            this.imTheLastPage = true;
            Console.WriteLine("imTheLastPage! No more people in list");
            break;
          }
          Console.WriteLine("imTheLastPage! checked ahead and no more relevant ppl to add");
        }
      }
    }

    public Vector2 GetSize() => new Vector2(this.peopleRows[0].GetSize().X, this.totalHeight);

    public static bool ShouldIncludeThisPerson(
      PeopleInParkFilter peopleFilterType,
      WalkingPerson person)
    {
      switch (peopleFilterType)
      {
        case PeopleInParkFilter.VIPs:
          if (person.simperson.roleinsociety == RoleInSociety.Customer)
          {
            if (person.simperson.customertype != CustomerType.Normal)
              return true;
            break;
          }
          if (person.simperson.roleinsociety != RoleInSociety.Employee && person.simperson.roleinsociety != RoleInSociety.Avatar && person.simperson.roleinsociety != RoleInSociety.MeatProcessor)
            return true;
          break;
        case PeopleInParkFilter.Customers:
          if (person.simperson.roleinsociety == RoleInSociety.Customer)
            return true;
          break;
      }
      return false;
    }

    public void SetInfoType(PeopleViewInfoType infoType)
    {
      for (int index = 0; index < this.peopleRows.Count; ++index)
        this.peopleRows[index].SetInfoType(infoType);
    }

    public WalkingPerson UpdatePeopleViewPage(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      for (int index = 0; index < this.peopleRows.Count; ++index)
      {
        if (this.peopleRows[index].UpdatePeopleInParkRow(player, DeltaTime, offset))
          return this.peopleRows[index].refPerson;
      }
      return (WalkingPerson) null;
    }

    public void DrawPeopleViewPage(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      for (int index = 0; index < this.peopleRows.Count; ++index)
        this.peopleRows[index].DrawPeopleInParkRow(offset, spriteBatch);
    }
  }
}
