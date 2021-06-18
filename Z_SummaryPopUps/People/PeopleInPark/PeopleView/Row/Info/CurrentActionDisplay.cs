// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info.CurrentActionDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView.Row.Info
{
  internal class CurrentActionDisplay
  {
    public Vector2 location;
    private ZGenericText text;
    private ZGenericText subText;
    private CurrentActionIcon icon;
    private CustomerQuest currentQuest;
    private bool WalkingToBus;
    private bool IsDead;
    private bool LeftPark_WaitingForBus;
    private float doubleTextOffsetY;
    private float height;

    public CurrentActionDisplay(float BaseScale)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.currentQuest = CustomerQuest.Count;
      this.WalkingToBus = false;
      this.LeftPark_WaitingForBus = false;
      this.icon = new CurrentActionIcon(BaseScale);
      this.icon.vLocation.X += this.icon.GetSize().X * 0.5f;
      this.text = new ZGenericText("X", BaseScale, false, _UseOnePointFiveFont: true);
      this.text.textToWrite = CurrentActionDisplay.GetCustomerQuestToString(this.currentQuest);
      this.text.vLocation.X = this.icon.GetSize().X + uiScaleHelper.GetDefaultXBuffer() * 0.5f;
      this.text.vLocation.Y -= this.text.GetSize().Y * 0.5f;
      this.subText = new ZGenericText("X", BaseScale, false);
      this.subText.vLocation.X = this.text.vLocation.X;
      this.subText.vLocation.Y = this.text.GetSize().Y - this.subText.GetSize().Y * 0.5f - uiScaleHelper.ScaleY(2f);
      this.doubleTextOffsetY = uiScaleHelper.ScaleY(5f);
      this.height = Math.Max(this.icon.GetSize().Y, this.text.GetSize().Y + this.subText.GetSize().Y);
      this.subText.textToWrite = string.Empty;
    }

    public void Darken()
    {
      this.text.SetInactiveColor();
      this.icon.Deactivate();
    }

    public float GetHeight() => this.height;

    public void UpdateCurrentAction(SimPerson simperson, WalkingPerson walkingperson)
    {
      if (this.LeftPark_WaitingForBus || this.IsDead)
        return;
      bool flag = false;
      if (simperson.memberofthepublic.IsAtBusWaiting)
      {
        this.LeftPark_WaitingForBus = true;
        this.text.textToWrite = "Left Park";
        this.subText.textToWrite = CurrentActionDisplay.GetLeaveParkReasonToString(simperson.memberofthepublic.LeftTheParkBecauseOfThis);
        flag = true;
      }
      else if (simperson.IsDead)
      {
        this.IsDead = true;
        this.text.textToWrite = "Died";
        this.subText.textToWrite = "";
        flag = true;
      }
      else if (!this.WalkingToBus && (simperson.memberofthepublic.LeftParkEarly || walkingperson.IsOnFinalWalkToBus))
      {
        this.WalkingToBus = true;
        if ((double) walkingperson.HoldTime <= 0.0)
        {
          this.text.textToWrite = "Leaving Park";
          ParkLeavingReason parkBecauseOfThis = simperson.memberofthepublic.LeftTheParkBecauseOfThis;
          if (parkBecauseOfThis != ParkLeavingReason.None)
            this.subText.textToWrite = CurrentActionDisplay.GetLeaveParkReasonToString(parkBecauseOfThis);
          else if (walkingperson.IsOnFinalWalkToBus)
            this.subText.textToWrite = "End of Day";
        }
        flag = true;
      }
      else if (simperson.memberofthepublic.CurrentQuestForText != this.currentQuest)
      {
        this.currentQuest = simperson.memberofthepublic.CurrentQuestForText;
        this.subText.textToWrite = string.Empty;
        this.text.textToWrite = simperson.memberofthepublic.GetCurrentActionDisplayString(simperson, walkingperson, ref this.subText.textToWrite);
        flag = true;
      }
      if (!flag)
        return;
      this.icon.SetAction(this.currentQuest, this.WalkingToBus, this.LeftPark_WaitingForBus, this.IsDead);
    }

    public static string GetCustomerQuestToString(CustomerQuest quest)
    {
      switch (quest)
      {
        case CustomerQuest.SeekingBin:
          return "Finding Trash Bin";
        case CustomerQuest.SeekingBathroom:
          return "Finding Toilet";
        case CustomerQuest.SeekingDrink:
          return "Finding Drink";
        case CustomerQuest.SeekingFood:
          return "Finding Food";
        case CustomerQuest.SeekingSouvenier:
          return "Finding Gifts";
        case CustomerQuest.SeekingIceCream:
          return "Finding Cold Food";
        case CustomerQuest.SeekingBench:
          return "Finding Bench";
        case CustomerQuest.WantsToSeeAnimal:
          return "Finding Animal";
        case CustomerQuest.SeekingATM:
          return "Finding ATM";
        case CustomerQuest.SeekingBuyingSouvenirsBeforeLeavingPark:
          return "Finding Gifts";
        case CustomerQuest.SeekingAnyFoodOrDrink:
          return "Finding Food or Drink";
        case CustomerQuest.Count:
          return "Idle";
        default:
          return "NA_" + (object) quest;
      }
    }

    public static string GetLeaveParkReasonToString(ParkLeavingReason reason)
    {
      string empty = string.Empty;
      string str;
      switch (reason)
      {
        case ParkLeavingReason.None:
        case ParkLeavingReason.VIPEND_EveryoneElseLeft:
          str = "";
          break;
        case ParkLeavingReason.NoIcecreamForChilli:
          str = "Chilli Burn";
          break;
        case ParkLeavingReason.NoToilets:
          str = "No Toilet";
          break;
        case ParkLeavingReason.NoFood:
          str = "Hungry";
          break;
        case ParkLeavingReason.NoDrinks:
          str = "Thirsty";
          break;
        case ParkLeavingReason.NoBenches:
          str = "Tired";
          break;
        case ParkLeavingReason.NoMoreShopsToGoTo:
          str = "Nothing Left To Do";
          break;
        case ParkLeavingReason.NothingLeftToDo:
          str = "Nothing Left To Do";
          break;
        case ParkLeavingReason.TicketTooExpensive:
          str = "Ticket Too Expensive";
          break;
        case ParkLeavingReason.ProtestsUpsetMe:
          str = "Too Much Protests";
          break;
        case ParkLeavingReason.BikersAreUnpleasant:
          str = "Bikers are unpleasant";
          break;
        case ParkLeavingReason.DeadAnimals:
          str = "Upset By Dead Animals";
          break;
        case ParkLeavingReason.EnclosureFilfth:
          str = "Enclosures Too Dirty";
          break;
        case ParkLeavingReason.GarbageInZoo:
          str = "Too Dirty";
          break;
        case ParkLeavingReason.NoATM_NoMoney:
          str = "Low Cash / No ATM";
          break;
        default:
          str = "NA";
          break;
      }
      return str;
    }

    public void DrawCurrentAction(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.icon.DrawCurrentActionIcon(offset, spriteBatch);
      if (!string.IsNullOrEmpty(this.subText.textToWrite))
      {
        offset.Y -= this.doubleTextOffsetY;
        this.subText.DrawZGenericText(offset, spriteBatch);
      }
      this.text.DrawZGenericText(offset, spriteBatch);
    }
  }
}
