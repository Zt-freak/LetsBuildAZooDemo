// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.ActionPopUpManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions.ActionPopUps.Employee;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class ActionPopUpManager
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private BigBrownPanel panel;
    private Vector2 framescale;
    private CustomerActionPopUp actionpopup;
    public bool hidemainpanel;

    public ActionPopUpManager(
      CustomerActionType actiontype,
      float basescale_,
      WalkingPerson walkingPerson,
      Player player)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.hidemainpanel = false;
      this.panel = new BigBrownPanel(Vector2.Zero, true, ActionPopUpManager.GetActionTypeToPanelHeaderString(actiontype), this.basescale, true);
      switch (actiontype)
      {
        case CustomerActionType.Bribe:
          this.actionpopup = (CustomerActionPopUp) new BribeActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.AnimalEncounter:
          this.actionpopup = (CustomerActionPopUp) new AnimalEncounterActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.Demands:
          this.actionpopup = (CustomerActionPopUp) new DemandsActionPopUp(player, this.basescale);
          break;
        case CustomerActionType.Security:
          this.actionpopup = (CustomerActionPopUp) new SecurityActionPopUp(this.basescale, 0.0f);
          break;
        case CustomerActionType.SpecialTreatment:
          this.actionpopup = (CustomerActionPopUp) new SpecialTreatmentActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.Sponsor:
          this.actionpopup = (CustomerActionPopUp) new SponsorActionPopUp(this.basescale);
          break;
        case CustomerActionType.ReassignWorkers:
          this.actionpopup = (CustomerActionPopUp) new ReassignWorkerActionPopUp(walkingPerson, this.basescale);
          this.panel.location = new Vector2(350f, -200f);
          break;
        case CustomerActionType.CloseThePubs:
          this.actionpopup = (CustomerActionPopUp) new ClosePubsActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.FoodDelivery:
          this.actionpopup = (CustomerActionPopUp) new FoodDeliveryActionPopUp(this.basescale);
          break;
        case CustomerActionType.BuyAnimals:
          this.actionpopup = (CustomerActionPopUp) new BuyAnimalActionPopUp(player, this.basescale, walkingPerson);
          break;
        case CustomerActionType.SellAnimals:
          this.actionpopup = (CustomerActionPopUp) new SellAnimalActionPopUp(player, this.basescale, walkingPerson);
          break;
        case CustomerActionType.Report:
          this.actionpopup = (CustomerActionPopUp) new ReportActionPopUp(this.basescale);
          break;
        case CustomerActionType.PrivateBar:
          this.actionpopup = (CustomerActionPopUp) new PrivateBarActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.Negotiate:
          this.actionpopup = (CustomerActionPopUp) new NegotiateActionPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.AdjustSalary:
          this.actionpopup = (CustomerActionPopUp) new AdjustSalaryPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.AdjustBreaks:
          this.actionpopup = (CustomerActionPopUp) new AdjustBreaksPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.GiveBonus:
          this.actionpopup = (CustomerActionPopUp) new GiveBonusPopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.Fire:
          this.actionpopup = (CustomerActionPopUp) new FireEmployeePopUp(walkingPerson, this.basescale);
          break;
        case CustomerActionType.MoveToGate:
          this.actionpopup = (CustomerActionPopUp) new MoveToGatePopUp(walkingPerson, this.basescale);
          break;
        default:
          throw new NotImplementedException();
      }
      this.framescale = this.actionpopup.GetSize();
      this.panel.Finalize(this.framescale);
      this.panel.HasPreviousButton = false;
    }

    public Vector2 GetSize() => this.framescale;

    public static string GetActionTypeToPanelHeaderString(CustomerActionType actionType)
    {
      switch (actionType)
      {
        case CustomerActionType.AnimalEncounter:
          return "Animal Encounter";
        case CustomerActionType.Demands:
          return "Satisfy Demands";
        case CustomerActionType.Security:
          return "Call Security";
        case CustomerActionType.SpecialTreatment:
          return "VIP treatment";
        case CustomerActionType.ReassignWorkers:
          return "Reassign";
        case CustomerActionType.BuyAnimals:
          return "Animals For Sale";
        case CustomerActionType.SellAnimals:
          return "Sell Animals";
        case CustomerActionType.Report:
          return "Report";
        case CustomerActionType.AdjustSalary:
          return "Adjust Salary";
        case CustomerActionType.AdjustBreaks:
          return "Adjust Breaks";
        case CustomerActionType.GiveBonus:
          return "Give Bonus";
        case CustomerActionType.MoveToGate:
          return "Reset Position";
        default:
          return actionType.ToString();
      }
    }

    public bool CheckMouseOver(Player player)
    {
      Vector2 offset = Sengine.HalfReferenceScreenRes + this.location;
      return this.panel.CheckMouseOver(player, offset);
    }

    public bool UpdateActionPopUpManager(
      Player player,
      float DeltaTime,
      out bool ForceCloseEverythingOnClose)
    {
      Vector2 referenceScreenRes = Sengine.HalfReferenceScreenRes;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime, referenceScreenRes);
      Vector2 offset = referenceScreenRes + this.location;
      int num = 0 | (this.panel.UpdatePanelCloseButton(player, DeltaTime, offset) ? 1 : 0) | (this.actionpopup.UpdateCustomerActionPopUp(player, offset, DeltaTime) ? 1 : 0);
      if (num != 0)
        this.actionpopup.OnPanelClosed();
      this.hidemainpanel = this.actionpopup.hidemainpanel;
      ForceCloseEverythingOnClose = this.actionpopup.ForceCloseEverythingOnClose;
      this.panel.HasPreviousButton = this.actionpopup.HasPreviousButton;
      if (!this.panel.UpdatePanelpreviousButton(player, DeltaTime, offset))
        return num != 0;
      this.actionpopup.OnPreviousButtonClicked();
      return num != 0;
    }

    public void DrawActionPopUpManager(SpriteBatch spritebatch)
    {
      Vector2 offset1 = Sengine.HalfReferenceScreenRes + this.location;
      this.panel.DrawBigBrownPanel(offset1, spritebatch);
      Vector2 offset2 = offset1 + this.panel.location;
      this.actionpopup.DrawCustomerActionPopUp(spritebatch, offset2);
    }
  }
}
