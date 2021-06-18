// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.Customer.CustomerAction
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_MoralitySummary;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_SummaryPopUps.People.Customer
{
  internal class CustomerAction
  {
    private UIScaleHelper uiscale;
    private float basescale;
    private Vector2 framescale;
    private Vector2 loc;
    public Vector2 location;
    private Vector2 currloc;
    private CustomerFrame frame;
    private MiniHeading miniheading;
    private SimpleTextHandler descpara;
    private float descheight;
    private LittleSummaryButton button;
    private GoodEvilIcon moralityicon;
    private CustomerActionIcon actionicon;
    private bool active;

    public CustomerActionType actiontype { get; private set; }

    public bool Active
    {
      get => this.active;
      set
      {
        this.active = value;
        this.frame.Active = value;
      }
    }

    public CustomerAction(CustomerActionType actiontype_, float basescale_, bool active_ = true)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.actiontype = actiontype_;
      string text = "";
      string TextToWrite = "";
      LittleSummaryButtonType _Buttontype = LittleSummaryButtonType.Count;
      int morality = this.GetMorality(this.actiontype);
      switch (morality)
      {
        case -1:
          _Buttontype = LittleSummaryButtonType.Action_Evil;
          break;
        case 0:
          _Buttontype = LittleSummaryButtonType.Action_Neutral;
          break;
        case 1:
          _Buttontype = LittleSummaryButtonType.Action_Good;
          break;
      }
      switch (this.actiontype)
      {
        case CustomerActionType.Bribe:
          text = "Bribe";
          TextToWrite = "Everyone has a price. If you can afford it.";
          break;
        case CustomerActionType.AnimalEncounter:
          text = "Animal Encounter";
          TextToWrite = "Give an up close and personal experience with some wildlife. Results may vary.";
          break;
        case CustomerActionType.Demands:
          text = "Satisfy Demands";
          TextToWrite = "Meet this person's demands so they will stop causing trouble for you.";
          break;
        case CustomerActionType.Security:
          text = "Call Security";
          TextToWrite = "Get your security personnel to deal with problematic people in any way you see fit.";
          break;
        case CustomerActionType.SpecialTreatment:
          text = "VIP Treatment";
          TextToWrite = "Offer a perfect experience to improve this person's impression of the zoo.";
          break;
        case CustomerActionType.Sponsor:
          text = "Sponsor";
          TextToWrite = "Spend money on paid promotions to increase the fame of your zoo and influence the sentiment of the person.";
          break;
        case CustomerActionType.ReassignWorkers:
          text = "Reassign Workers";
          TextToWrite = "Temporarily reassign your workers to give a better impression for this person.";
          break;
        case CustomerActionType.CloseThePubs:
          text = "Close Pubs";
          TextToWrite = "Close all pubs in the zoo in hopes that they would get bored and leave.";
          break;
        case CustomerActionType.FoodDelivery:
          text = "Serve Delivered Food";
          TextToWrite = "Order food to be delivered from outside your zoo to pass off as your own.";
          break;
        case CustomerActionType.BuyAnimals:
          text = "Buy Animals";
          TextToWrite = "Buy exotic animals from questionable sources.";
          break;
        case CustomerActionType.SellAnimals:
          text = "Sell Animals";
          TextToWrite = "Sell any of your animals into the market.";
          break;
        case CustomerActionType.Report:
          text = "Report";
          TextToWrite = "Report this person to the police.";
          break;
        case CustomerActionType.PrivateBar:
          text = "Provide Private Bar";
          TextToWrite = "Provide free-flow drinks in a private bar isolated from the other customers.";
          break;
        case CustomerActionType.Negotiate:
          text = "Negotiate";
          TextToWrite = "Negotiate changes in salary with this employee.";
          break;
        case CustomerActionType.AdjustSalary:
          text = "Adjust Salary";
          TextToWrite = "Adjust the salary of this employee.";
          break;
        case CustomerActionType.AdjustBreaks:
          text = "Adjust Breaks";
          TextToWrite = "Adjust the amount of break time for this employee.";
          break;
        case CustomerActionType.GiveBonus:
          text = "Give Bonus";
          TextToWrite = "Pay this employee a bonus.";
          break;
        case CustomerActionType.Fire:
          text = "Fire";
          TextToWrite = "Fire this employee.";
          break;
        case CustomerActionType.MoveToGate:
          text = "Reset Position";
          TextToWrite = "Teleport this person to the park entrance.";
          break;
      }
      this.miniheading = new MiniHeading(Vector2.Zero, text, 1f, this.basescale);
      this.descpara = new SimpleTextHandler(TextToWrite, false, this.uiscale.ScaleX(200f) / Sengine.ReferenceScreenRes.X, this.basescale, false, false);
      this.descpara.SetAllColours(ColourData.Z_Cream);
      this.descpara.AutoCompleteParagraph();
      this.descheight = this.descpara.GetHeightOfParagraph();
      this.button = new LittleSummaryButton(_Buttontype, _BaseScale: this.basescale);
      if (morality != 0)
        this.moralityicon = new GoodEvilIcon(morality == 1, basescale_: this.basescale);
      this.actionicon = new CustomerActionIcon(this.actiontype, this.basescale);
      this.framescale = new Vector2();
      this.framescale.X = this.uiscale.ScaleX(280f);
      this.framescale.Y = 2f * defaultBuffer.Y;
      this.framescale.Y += this.miniheading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.framescale.Y += Math.Max(this.descheight, this.actionicon.GetSize().Y);
      this.miniheading.SetTextPosition(this.framescale);
      this.frame = new CustomerFrame(this.framescale, true, 2f * this.basescale);
      this.Active = active_;
      this.currloc = -0.5f * this.framescale;
      this.currloc += this.uiscale.DefaultBuffer;
      if (this.moralityicon != null)
      {
        this.moralityicon.vLocation = this.currloc;
        this.moralityicon.vLocation.X += this.miniheading.GetSize().X + 0.5f * this.moralityicon.GetSize().X;
      }
      this.currloc.Y += this.miniheading.GetSize().Y + 0.5f * defaultBuffer.Y;
      this.actionicon.vLocation = this.currloc + 0.5f * this.actionicon.GetSize();
      this.currloc.X += this.actionicon.GetSize().X;
      this.descpara.Location = this.currloc;
      this.descpara.Location.X += 0.5f * defaultBuffer.X;
      this.button.vLocation = new Vector2();
      this.button.vLocation.Y = 0.0f;
      this.button.vLocation.X = (float) (0.5 * (double) this.framescale.X - 0.5 * (double) this.button.GetSize().X) - defaultBuffer.X;
    }

    public Vector2 GetSize() => this.framescale;

    public int GetMorality(CustomerActionType actionType)
    {
      switch (this.actiontype)
      {
        case CustomerActionType.Bribe:
          return -1;
        case CustomerActionType.AnimalEncounter:
          return 0;
        case CustomerActionType.Demands:
          return 1;
        case CustomerActionType.Security:
          return 0;
        case CustomerActionType.SpecialTreatment:
          return 0;
        case CustomerActionType.Sponsor:
          return 0;
        case CustomerActionType.ReassignWorkers:
          return 0;
        case CustomerActionType.CloseThePubs:
          return 0;
        case CustomerActionType.FoodDelivery:
          return 0;
        case CustomerActionType.BuyAnimals:
          return -1;
        case CustomerActionType.SellAnimals:
          return -1;
        case CustomerActionType.Report:
          return 1;
        case CustomerActionType.PrivateBar:
          return 0;
        case CustomerActionType.Negotiate:
          return 0;
        case CustomerActionType.AdjustSalary:
          return 0;
        case CustomerActionType.AdjustBreaks:
          return 0;
        case CustomerActionType.GiveBonus:
          return 0;
        case CustomerActionType.Fire:
          return 0;
        case CustomerActionType.MoveToGate:
          return 0;
        default:
          return 0;
      }
    }

    public void LockForBeta()
    {
      this.Active = false;
      this.frame.LockForBeta();
    }

    public bool UpdateCustomerAction(
      Player player,
      Vector2 offset,
      float DeltaTime,
      out CustomerActionType OUTactiontype)
    {
      offset += this.location;
      OUTactiontype = CustomerActionType.None;
      if (!this.active || !this.button.UpdateLittleSummaryButton(DeltaTime, player, offset))
        return false;
      OUTactiontype = this.actiontype;
      return true;
    }

    public void DrawCustomerAction(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      this.miniheading.DrawMiniHeading(offset, spritebatch);
      this.descpara.DrawSimpleTextHandler(offset, 1f, spritebatch);
      if (this.moralityicon != null)
        this.moralityicon.DrawGoodEvilIcon(offset, spritebatch);
      this.button.DrawLittleSummaryButton(offset, spritebatch);
      this.actionicon.DrawCustomerActionIcon(spritebatch, offset);
      this.frame.DrawDarkOverlay(offset, spritebatch);
    }
  }
}
