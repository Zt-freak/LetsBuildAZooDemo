// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.PeopleViewer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_Manage.Hiring.Interview.Negotiation.MakeOffer;
using TinyZoo.Z_SummaryPopUps.People.Customer;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Desc;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView
{
  internal class PeopleViewer
  {
    public Vector2 location;
    private CustomerFrame customerFrame;
    private ZGenericText countDisplayText;
    private PriceAdjuster informationTypeSelector;
    private PeopleViewPagesContainer pagesContainer;
    private SimpleArrowPageButtons pageButtons;
    private PeopleViewerDescFrame descFrame;
    private float BaseScale;
    private float forcedWidth;
    private UIScaleHelper scaleHelper;
    private Vector2 frameOffset;
    internal static bool NewPeopleEnteredPark;
    internal static List<WalkingPerson> refWalkingPeoplePointer;
    private int totalNumberOfRelevantPeople_InPark;
    private PeopleInParkFilter refPeopleFilterType;
    private bool ShowFilter;

    public PeopleViewer(PeopleInParkFilter peopleFilterType, float _BaseScale, Vector2 forcedSize)
    {
      this.BaseScale = _BaseScale;
      this.refPeopleFilterType = peopleFilterType;
      this.ShowFilter = (uint) this.refPeopleFilterType > 0U;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      float defaultXbuffer = this.scaleHelper.GetDefaultXBuffer();
      float defaultYbuffer = this.scaleHelper.GetDefaultYBuffer();
      this.customerFrame = new CustomerFrame(forcedSize, CustomerFrameColors.Brown, this.BaseScale);
      this.frameOffset = -this.customerFrame.VSCale * 0.5f;
      float num1 = defaultYbuffer + this.frameOffset.Y;
      this.forcedWidth = forcedSize.X - defaultXbuffer * 2f;
      this.countDisplayText = new ZGenericText("X", this.BaseScale, false, _UseOnePointFiveFont: true);
      this.countDisplayText.vLocation.X = defaultXbuffer + this.frameOffset.X;
      this.countDisplayText.vLocation.Y = num1 + this.countDisplayText.GetSize().Y * 0.5f;
      int num2 = 0;
      int Max = 2;
      if (peopleFilterType == PeopleInParkFilter.VIPs)
      {
        num2 = 4;
        Max = 4;
      }
      this.informationTypeSelector = new PriceAdjuster(num2, Max, num2, _BaseScale: this.BaseScale);
      this.informationTypeSelector.SetToString("NA", 125f, true);
      Vector2 size = this.informationTypeSelector.GetSize();
      this.informationTypeSelector.Location.X = (float) ((double) this.frameOffset.X + (double) this.scaleHelper.ScaleX(150f) + (double) size.X * 0.5);
      this.informationTypeSelector.Location.Y = num1 + size.Y * 0.5f;
      float num3 = num1 + (this.informationTypeSelector.GetSize().Y + defaultYbuffer);
      this.descFrame = new PeopleViewerDescFrame(this.BaseScale, this.forcedWidth);
      this.descFrame.location.Y = num3 + this.descFrame.GetSize().Y * 0.5f;
      float num4 = num3 + (this.descFrame.GetSize().Y + defaultYbuffer * 0.5f);
      this.OnNewPeopleEnteringPark(true);
      this.pagesContainer = new PeopleViewPagesContainer(peopleFilterType, this.BaseScale, this.forcedWidth);
      this.pagesContainer.location.Y = num4;
      float num5 = num4 + this.pagesContainer.GetFullSize().Y + defaultYbuffer * 0.5f;
      this.pageButtons = new SimpleArrowPageButtons(this.BaseScale);
      this.pageButtons.Location.Y = num5 + this.pageButtons.GetSize().Y * 0.5f;
      this.pageButtons.Location.X = forcedSize.X - defaultXbuffer + this.frameOffset.X;
      this.pageButtons.Location.X -= this.pageButtons.GetSize().X * 0.5f;
      float num6 = num5 + this.pageButtons.GetSize().Y + defaultYbuffer * 0.5f;
      this.pageButtons.SetAsDisabled(true, true);
      this.pageButtons.SetAsDisabled(false, this.pagesContainer.IsOnLastPage());
      this.OnSelectorChanged((PeopleViewInfoType) this.informationTypeSelector.CurrentValue);
    }

    private void OnNewPeopleEnteringPark(bool isCreate = false)
    {
      PeopleViewer.refWalkingPeoplePointer = CustomerManager.GetListOfWalkingPeople();
      if (!isCreate)
        this.pagesContainer.TryRefresh();
      PeopleViewer.NewPeopleEnteredPark = false;
      this.RefreshPageArrows(isCreate);
    }

    private int GetTotalNumberOfPeopleForThisCategory()
    {
      switch (this.refPeopleFilterType)
      {
        case PeopleInParkFilter.VIPs:
          return CustomerManager.VIP_BlackMarketEtc + CustomerManager.CurrentSpecialCustomers[1];
        case PeopleInParkFilter.Customers:
          return CustomerManager.CustomersInPark_NotWaitingForBus - CustomerManager.VIP_BlackMarketEtc;
        default:
          return -1;
      }
    }

    public void RefreshPageArrows(bool isCreate = false)
    {
      if (isCreate)
        return;
      this.pageButtons.SetAsDisabled(false, this.pagesContainer.IsOnLastPage());
      this.pageButtons.SetAsDisabled(true, this.pagesContainer.currentPageIndex == 0);
    }

    public WalkingPerson UpdatePeopleViewer(
      Player player,
      float DeltaTime,
      Vector2 offset)
    {
      offset += this.location;
      if (this.ShowFilter && this.informationTypeSelector.UpdatePriceAdjuster(player, offset, DeltaTime))
        this.OnSelectorChanged((PeopleViewInfoType) this.informationTypeSelector.CurrentValue);
      int forwardOrBack = this.pageButtons.UpdateSimpleArrowPageButtons(DeltaTime, player, offset);
      if (forwardOrBack != 0)
      {
        this.pagesContainer.ChangePage(forwardOrBack);
        this.RefreshPageArrows();
      }
      if (PeopleViewer.NewPeopleEnteredPark)
        this.OnNewPeopleEnteringPark();
      this.totalNumberOfRelevantPeople_InPark = this.GetTotalNumberOfPeopleForThisCategory();
      this.countDisplayText.textToWrite = "Total In Park: " + (object) this.totalNumberOfRelevantPeople_InPark;
      this.descFrame.UpdatePeopleViewerDescFrame(this.totalNumberOfRelevantPeople_InPark == 0);
      return this.pagesContainer.UpdatePeopleViewPagesContainer(player, DeltaTime, offset);
    }

    private void OnSelectorChanged(PeopleViewInfoType infoType)
    {
      this.informationTypeSelector.SetToString(PeopleViewer.GetPeopleViewInfoTypeToString(infoType), 0.0f);
      this.pagesContainer.SetInfoType(infoType);
      this.descFrame.SetInfoType(infoType);
    }

    private static string GetPeopleViewInfoTypeToString(PeopleViewInfoType filterType)
    {
      switch (filterType)
      {
        case PeopleViewInfoType.Actions:
          return "Actions";
        case PeopleViewInfoType.Frustrations:
          return "Frustration";
        case PeopleViewInfoType.MoneyHeld:
          return "Cash Held";
        default:
          return "NA";
      }
    }

    public void DrawPeopleViewer(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
      this.countDisplayText.DrawZGenericText(offset, spriteBatch);
      if (this.ShowFilter)
        this.informationTypeSelector.DrawPriceAdjuster(offset, spriteBatch);
      this.descFrame.DrawPeopleViewerDescFrame(offset, spriteBatch);
      this.pagesContainer.DrawPeopleViewPagesContainer(offset, spriteBatch);
      this.pageButtons.DrawSimpleArrowPageButtons(offset, spriteBatch);
    }
  }
}
