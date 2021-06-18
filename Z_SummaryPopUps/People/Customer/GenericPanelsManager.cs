// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.GenericPanelsManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.CustomerActions;
using TinyZoo.Z_SummaryPopUps.People.Customer.VIPSpecificInfo;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class GenericPanelsManager
  {
    private float basescale;
    private UIScaleHelper scaleHelper;
    private Vector2 pad;
    private VIPProfile profile;
    private CustomerActionList actions;
    private VIPInfo info;
    private Vector2 framescale;
    private WalkingPerson person;

    public GenericPanelsManager(WalkingPerson person_, float basescale_)
    {
      this.basescale = basescale_;
      this.scaleHelper = new UIScaleHelper(this.basescale);
      this.pad = this.scaleHelper.DefaultBuffer;
      this.person = person_;
      this.profile = new VIPProfile(this.person, this.person.simperson.GetName(), this.basescale);
      this.actions = new CustomerActionList(this.basescale);
      CustomerType typeOfThisPerson = CustomerViewManager.GetCustomerTypeOfThisPerson(this.person);
      switch (typeOfThisPerson)
      {
        case CustomerType.Protestor:
          this.info = (VIPInfo) new ProtestorInfo(this.person, this.basescale);
          this.actions.AddAction(CustomerActionType.Bribe);
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.Demands);
          this.actions.AddAction(CustomerActionType.Security);
          break;
        case CustomerType.HealthInspector:
          this.info = (VIPInfo) new HealthInspectorInfo(this.person, this.basescale, this.profile.GetSize().X);
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.Bribe);
          this.actions.AddAction(CustomerActionType.ReassignWorkers);
          break;
        case CustomerType.AnimalWelfareOfficer:
          this.info = (VIPInfo) new AnimalWelfareOfficerInfo(this.person, this.basescale, this.profile.GetSize().X);
          bool isBribed = this.person.simperson.memberofthepublic.animalwelfarecontroller.GetIsBribed();
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.Bribe, !isBribed);
          break;
        case CustomerType.SafetyInspector:
          this.info = (VIPInfo) new SafetyInspectorInfo(this.person, this.basescale, this.profile.GetSize().X);
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.Bribe);
          break;
        case CustomerType.Influencer:
          this.info = (VIPInfo) new InfluencerInfo(this.person, this.basescale);
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.SpecialTreatment);
          this.actions.AddAction(CustomerActionType.Sponsor);
          break;
        case CustomerType.Biker:
          this.info = (VIPInfo) new BikerInfo(this.basescale, this.profile.GetSize().X);
          this.actions.AddAction(CustomerActionType.Security);
          this.actions.AddAction(CustomerActionType.CloseThePubs);
          this.actions.AddAction(CustomerActionType.PrivateBar);
          break;
        case CustomerType.Teacher:
          this.info = (VIPInfo) new TeacherInfo(this.person, this.basescale);
          this.actions.AddAction(CustomerActionType.SpecialTreatment);
          break;
        case CustomerType.Student:
          this.info = (VIPInfo) new StudentInfo(this.person, this.basescale);
          break;
        case CustomerType.FoodCritic:
          this.info = (VIPInfo) new FoodCriticInfo(this.basescale, this.profile.GetSize().X);
          this.actions.AddAction(CustomerActionType.AnimalEncounter);
          this.actions.AddAction(CustomerActionType.FoodDelivery);
          break;
        case CustomerType.Policeman:
          this.info = (VIPInfo) new PolicemanInfo(this.person, this.basescale, this.profile.GetSize().X);
          this.actions.AddAction(CustomerActionType.Bribe);
          break;
        case CustomerType.ResearchGrantGuy:
        case CustomerType.AnimalArtist:
        case CustomerType.GenomeBetaGiver:
          this.info = (VIPInfo) new CriticalPersonInfo(this.basescale, this.profile.GetSize().X, typeOfThisPerson, this.person);
          break;
      }
      if (this.actions.Count < 1)
        this.actions = (CustomerActionList) null;
      this.framescale = new Vector2();
      this.framescale.X += this.profile.GetSize().X;
      this.framescale.Y = this.profile.GetSize().Y + this.info.GetSize().Y + this.pad.Y;
      if (this.actions != null)
      {
        this.framescale.X += this.actions.GetSize().X + this.pad.X;
        this.framescale.Y = Math.Max(this.actions.GetSize().Y, this.framescale.Y);
        this.actions.ForceToHeight(this.framescale.Y);
      }
      Vector2 vector2 = -0.5f * this.framescale;
      this.profile.location = vector2 + 0.5f * this.profile.GetSize();
      vector2.X += this.profile.GetSize().X + this.pad.X;
      if (this.actions != null)
        this.actions.location = vector2 + 0.5f * this.actions.GetSize();
      vector2.Y += this.profile.GetSize().Y + this.pad.Y;
      this.info.location.X = (float) (-0.5 * (double) this.framescale.X + 0.5 * (double) this.info.GetSize().X);
      this.info.location.Y = vector2.Y + 0.5f * this.info.GetSize().Y;
      if (this.actions == null)
        return;
      for (int index = 0; index < this.actions.Count; ++index)
      {
        bool IsLockedBecauseofBeta;
        bool isActive = GenericPanelsManager.IsThisActionEnabled(this.actions.customerActions[index].actiontype, out IsLockedBecauseofBeta);
        if (this.actions.customerActions[index].actiontype != CustomerActionType.Bribe || !isActive)
          this.actions.LockThisAction(this.actions.customerActions[index].actiontype, isActive, IsLockedBecauseofBeta);
      }
    }

    public static bool IsThisActionEnabled(
      CustomerActionType actionType,
      out bool IsLockedBecauseofBeta)
    {
      IsLockedBecauseofBeta = false;
      switch (actionType)
      {
        case CustomerActionType.AnimalEncounter:
        case CustomerActionType.Demands:
        case CustomerActionType.Security:
        case CustomerActionType.SpecialTreatment:
        case CustomerActionType.Sponsor:
        case CustomerActionType.GiveTourGuide:
        case CustomerActionType.ReassignWorkers:
        case CustomerActionType.Blackmail:
        case CustomerActionType.CloseThePubs:
        case CustomerActionType.ExtremeRides:
        case CustomerActionType.FoodDelivery:
        case CustomerActionType.OfferJob:
        case CustomerActionType.CallMedic:
        case CustomerActionType.OfferHunt:
        case CustomerActionType.Report:
        case CustomerActionType.PrivateBar:
        case CustomerActionType.Negotiate:
        case CustomerActionType.AdjustBreaks:
        case CustomerActionType.GiveBonus:
          if (!Z_DebugFlags.IsBetaVersion)
            return true;
          IsLockedBecauseofBeta = true;
          return false;
        default:
          return true;
      }
    }

    public Vector2 GetSize() => this.framescale;

    public bool UpdateGenericPanelsManager(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTactionType)
    {
      OUTactionType = CustomerActionType.None;
      return this.actions != null && this.actions.UpdateCustomerActionsList(player, offset, DeltaTime, out OUTactionType);
    }

    public void DrawGenericPanelsManager(SpriteBatch spriteBatch, Vector2 offset)
    {
      this.profile.DrawVIPProfile(spriteBatch, offset);
      if (this.actions != null)
        this.actions.DrawCustomerActionsList(spriteBatch, offset);
      this.info.DrawVIPInfo(spriteBatch, offset);
    }
  }
}
