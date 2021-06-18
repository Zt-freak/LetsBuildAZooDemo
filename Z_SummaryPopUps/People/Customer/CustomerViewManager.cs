// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerViewManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerPanelsManagers;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerViewManager
  {
    private Vector2 Location;
    private BigBrownPanel panel;
    private GenericPanelsManager genericPanels;
    private CustomerPanelsManager customerPanels;
    private BlackMarketPanelsManager blackMarketPanels;
    private InfluencerPanelsManager influencerPanels;
    private StrikePanelsManager strikepanels;
    private UIScaleHelper uiscale;
    private string Name;
    private CustomerType customertype;
    private Vector2 framescale;
    private ActionPopUpManager popup;
    private float basescale;
    private WalkingPerson refPerson;
    private bool hidemainpanel;
    private LookAtThisThingButton lookAtThisThingButton;
    public static Vector2 topRightLocBuffer_Raw = new Vector2(50f, 100f);

    public CustomerViewManager(
      SimPerson simperson,
      WalkingPerson person,
      Vector2 MasterVScale,
      Player player)
    {
      this.refPerson = person;
      this.basescale = Z_GameFlags.GetBaseScaleForUI();
      MasterVScale *= this.basescale;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.customertype = CustomerViewManager.GetCustomerTypeOfThisPerson(person);
      string inSocietyToString = PeopleInParkRow.GetRoleInSocietyToString(simperson.roleinsociety, this.customertype);
      this.Name = person.simperson.GetName();
      switch (this.customertype)
      {
        case CustomerType.Normal:
          this.customerPanels = new CustomerPanelsManager(simperson, person, MasterVScale, this.basescale);
          this.framescale = this.customerPanels.GetSize();
          break;
        case CustomerType.BlackMarket:
          this.blackMarketPanels = new BlackMarketPanelsManager(person, this.basescale, player);
          this.framescale = this.blackMarketPanels.GetSize();
          break;
        case CustomerType.Count:
          this.strikepanels = new StrikePanelsManager(person, this.basescale);
          this.framescale = this.strikepanels.GetSize();
          break;
        default:
          this.genericPanels = new GenericPanelsManager(person, this.basescale);
          this.framescale = this.genericPanels.GetSize();
          break;
      }
      this.panel = new BigBrownPanel(this.framescale, true, inSocietyToString, this.basescale);
      Vector2 vector2 = new Vector2(this.uiscale.GetDefaultXBuffer(), this.uiscale.GetDefaultYBuffer());
      this.panel.Finalize(this.framescale);
      this.Location = new Vector2((float) (1024.0 - (double) this.uiscale.ScaleX(CustomerViewManager.topRightLocBuffer_Raw.X) - (double) this.framescale.X * 0.5), 384f);
      this.lookAtThisThingButton = new LookAtThisThingButton(person, this.basescale);
      this.lookAtThisThingButton.location.X = this.panel.GetMiniHeadingSize(false).X + this.uiscale.DefaultBuffer.X;
      this.lookAtThisThingButton.location -= this.panel.vScale * 0.5f;
      this.lookAtThisThingButton.location.X += this.lookAtThisThingButton.GetSize().X * 0.5f + this.uiscale.DefaultBuffer.X;
      this.lookAtThisThingButton.location.Y += (float) ((double) this.panel.GetMiniHeadingSize(false).Y * 0.5 + (double) this.uiscale.DefaultBuffer.Y * 0.5);
      this.lookAtThisThingButton.location.Y -= this.lookAtThisThingButton.GetSize().Y * 0.5f;
    }

    public static CustomerType GetCustomerTypeOfThisPerson(WalkingPerson person)
    {
      if (person.simperson.roleinsociety == RoleInSociety.BlackMarket)
        return CustomerType.BlackMarket;
      if (person.simperson.roleinsociety != RoleInSociety.Employee)
        return person.simperson.customertype;
      if (person.simperson.Ref_Employee.employeetype == EmployeeType.Police)
        return CustomerType.Policeman;
      throw new Exception("WHO IS THIS PERSON");
    }

    public bool CheckMouseOver(Player player, Vector2 offset)
    {
      offset += this.Location;
      return this.popup != null && this.popup.CheckMouseOver(player) || this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateCustomerViewManager(Vector2 offset, Player player, float DeltaTime)
    {
      this.hidemainpanel = false;
      this.panel.UpdateDragger(player, ref this.Location, DeltaTime);
      bool flag1 = false;
      if (this.panel.UpdatePanelCloseButton(player, DeltaTime, this.Location + offset))
        flag1 = true;
      bool flag2 = false;
      CustomerActionType actiontype = CustomerActionType.None;
      if (this.panel.Active)
      {
        switch (this.customertype)
        {
          case CustomerType.Normal:
            flag2 |= this.customerPanels.UpdateCustomerPanelsManager(player, offset + this.Location, DeltaTime);
            break;
          case CustomerType.BlackMarket:
            flag2 |= this.blackMarketPanels.UpdateBlackMarketPanelsManager(player, offset + this.Location, DeltaTime, out actiontype);
            break;
          case CustomerType.Count:
            flag2 |= this.strikepanels.UpdateStrikePanelsManager(player, offset + this.Location, DeltaTime, out actiontype);
            break;
          default:
            flag2 |= this.genericPanels.UpdateGenericPanelsManager(player, offset + this.Location, DeltaTime, out actiontype);
            break;
        }
      }
      if (this.popup == null)
      {
        if (flag2)
        {
          if (actiontype != CustomerActionType.None)
            this.popup = new ActionPopUpManager(actiontype, this.basescale, this.refPerson, player);
          this.panel.Active = false;
        }
      }
      else
      {
        this.hidemainpanel = this.popup.hidemainpanel;
        bool ForceCloseEverythingOnClose;
        if (this.popup.UpdateActionPopUpManager(player, DeltaTime, out ForceCloseEverythingOnClose))
        {
          this.popup = (ActionPopUpManager) null;
          this.panel.Active = true;
          if (ForceCloseEverythingOnClose)
          {
            if (this.lookAtThisThingButton != null)
              this.lookAtThisThingButton.OnButtonDestroy();
            flag1 = true;
          }
        }
      }
      if (this.panel.Active && this.lookAtThisThingButton != null)
      {
        this.lookAtThisThingButton.UpdateLookAtThisThingButton(player, DeltaTime, offset + this.Location);
        if (flag1)
          this.lookAtThisThingButton.OnButtonDestroy();
      }
      return flag1;
    }

    public void DrawCustomerViewManager(Vector2 offset)
    {
      offset += this.Location;
      if (!this.hidemainpanel)
      {
        this.panel.DrawBigBrownPanel(offset);
        if (this.lookAtThisThingButton != null)
          this.lookAtThisThingButton.DrawLookAtThisThingButton(offset, AssetContainer.pointspritebatchTop05);
        switch (this.customertype)
        {
          case CustomerType.Normal:
            this.customerPanels.DrawCustomerPanelsManager(AssetContainer.pointspritebatchTop05, offset);
            break;
          case CustomerType.BlackMarket:
            this.blackMarketPanels.DrawBlackMarketPanelsManager(AssetContainer.pointspritebatchTop05, offset);
            break;
          case CustomerType.Count:
            this.strikepanels.DrawStrikePanelsManager(AssetContainer.pointspritebatchTop05, offset);
            break;
          default:
            this.genericPanels.DrawGenericPanelsManager(AssetContainer.pointspritebatchTop05, offset);
            break;
        }
        this.panel.DrawDarkOverlay(offset, AssetContainer.pointspritebatchTop05);
      }
      if (this.popup == null)
        return;
      this.popup.DrawActionPopUpManager(AssetContainer.pointspritebatchTop05);
    }
  }
}
