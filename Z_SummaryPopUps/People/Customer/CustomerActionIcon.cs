// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActionIcon
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerActionIcon : GameObject
  {
    private Rectangle bribeRect = new Rectangle(889, 120, 20, 20);
    private Rectangle animalEncounterRect = new Rectangle(889, 141, 20, 20);
    private Rectangle demandsRect = new Rectangle(889, 162, 20, 20);
    private Rectangle securityRect = new Rectangle(963, 246, 20, 20);
    private Rectangle specialTreatmentRect = new Rectangle(942, 246, 20, 20);
    private Rectangle sponsorRect = new Rectangle(921, 246, 20, 20);
    private Rectangle tourRect = new Rectangle(963, 267, 20, 20);
    private Rectangle reassignWorkersRect = new Rectangle(942, 267, 20, 20);
    private Rectangle blackmailRect = new Rectangle(921, 267, 20, 20);
    private Rectangle closePubsRect = new Rectangle(899, (int) byte.MaxValue, 20, 20);
    private Rectangle FoodDeliveryRect = new Rectangle(303, 735, 20, 20);
    private Rectangle callPoliceRect = new Rectangle(282, 735, 20, 20);
    private Rectangle offerJobRect = new Rectangle(261, 735, 20, 20);
    private Rectangle callMedicRect = new Rectangle(240, 735, 20, 20);
    private Rectangle offerHuntRect = new Rectangle(207, 675, 20, 20);
    private Rectangle buyAnimalRect = new Rectangle(360, 693, 20, 20);
    private Rectangle sellAnimalRect = new Rectangle(360, 714, 20, 20);
    private Rectangle reportRect = new Rectangle(365, 900, 20, 20);
    private Rectangle AdjustSalaryRect = new Rectangle(386, 781, 20, 20);
    private Rectangle AdjustBreaksRect = new Rectangle(407, 781, 20, 20);
    private Rectangle GiveBonusRect = new Rectangle(428, 780, 20, 20);
    private Rectangle FireStaffRect = new Rectangle(0, 376, 20, 20);
    private Rectangle PrivateBarRect = new Rectangle(449, 778, 20, 20);
    private Rectangle NegotiateRect = new Rectangle(470, 778, 20, 20);
    private Rectangle ResetPositionRect = new Rectangle(472, 799, 20, 20);
    private float basescale;
    private CustomerActionType actiontype;
    private UIScaleHelper uiscale;

    public CustomerActionIcon(CustomerActionType actiontype_, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.actiontype = actiontype_;
      this.scale = this.basescale;
      switch (this.actiontype)
      {
        case CustomerActionType.Bribe:
          this.DrawRect = this.bribeRect;
          break;
        case CustomerActionType.AnimalEncounter:
          this.DrawRect = this.animalEncounterRect;
          break;
        case CustomerActionType.Demands:
          this.DrawRect = this.demandsRect;
          break;
        case CustomerActionType.Security:
          this.DrawRect = this.securityRect;
          break;
        case CustomerActionType.SpecialTreatment:
          this.DrawRect = this.specialTreatmentRect;
          break;
        case CustomerActionType.Sponsor:
          this.DrawRect = this.sponsorRect;
          break;
        case CustomerActionType.GiveTourGuide:
          this.DrawRect = this.tourRect;
          break;
        case CustomerActionType.ReassignWorkers:
          this.DrawRect = this.reassignWorkersRect;
          break;
        case CustomerActionType.Blackmail:
          this.DrawRect = this.blackmailRect;
          break;
        case CustomerActionType.CloseThePubs:
          this.DrawRect = this.closePubsRect;
          break;
        case CustomerActionType.FoodDelivery:
          this.DrawRect = this.FoodDeliveryRect;
          break;
        case CustomerActionType.CallPolice:
          this.DrawRect = this.callPoliceRect;
          break;
        case CustomerActionType.OfferJob:
          this.DrawRect = this.offerJobRect;
          break;
        case CustomerActionType.CallMedic:
          this.DrawRect = this.callMedicRect;
          break;
        case CustomerActionType.OfferHunt:
          this.DrawRect = this.offerHuntRect;
          break;
        case CustomerActionType.BuyAnimals:
          this.DrawRect = this.buyAnimalRect;
          break;
        case CustomerActionType.SellAnimals:
          this.DrawRect = this.sellAnimalRect;
          break;
        case CustomerActionType.Report:
          this.DrawRect = this.reportRect;
          break;
        case CustomerActionType.PrivateBar:
          this.DrawRect = this.PrivateBarRect;
          break;
        case CustomerActionType.Negotiate:
          this.DrawRect = this.NegotiateRect;
          break;
        case CustomerActionType.AdjustSalary:
          this.DrawRect = this.AdjustSalaryRect;
          break;
        case CustomerActionType.AdjustBreaks:
          this.DrawRect = this.AdjustBreaksRect;
          break;
        case CustomerActionType.GiveBonus:
          this.DrawRect = this.GiveBonusRect;
          break;
        case CustomerActionType.Fire:
          this.DrawRect = this.FireStaffRect;
          break;
        case CustomerActionType.MoveToGate:
          this.DrawRect = this.ResetPositionRect;
          break;
      }
      this.SetDrawOriginToCentre();
    }

    public Vector2 GetSize() => this.uiscale.ScaleVector2(new Vector2((float) this.DrawRect.Width, (float) this.DrawRect.Height));

    public void DrawCustomerActionIcon(SpriteBatch spritebatch, Vector2 offset) => this.Draw(spritebatch, AssetContainer.SpriteSheet, offset);
  }
}
