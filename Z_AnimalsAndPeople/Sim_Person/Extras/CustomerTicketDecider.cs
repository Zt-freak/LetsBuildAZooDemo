// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.CustomerTicketDecider
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_BalanceSystems.Customers;
using TinyZoo.Z_ManageShop.FoodIcon;
using TinyZoo.Z_OverWorld;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class CustomerTicketDecider
  {
    internal static void CustomerBuyTicketEvent(
      SimPerson simperson,
      ref bool HasPaid,
      Player player,
      Vector2 vLocation,
      ref bool IsWalking,
      out bool TryToReturnToBus)
    {
      TryToReturnToBus = false;
      simperson.memberofthepublic.ThisCustomerDecidedNotToPay = true;
      HasPaid = true;
      int num1 = player.Stats.GetTicketCost();
      if (player.Stats.GetTicketIsFree())
        num1 = 0;
      if (simperson.customertype != CustomerType.Normal)
      {
        simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
      }
      else
      {
        if (CustomerManager.CurrentSpecialCustomers[1] * 2 > NewCustomerCalculator.TotalPeopleWhoLeftBecauseOfProtestors && Game1.Rnd.Next(0, 3) == 0)
        {
          MoneyRenderer.SomethingIsTooExpensive(vLocation - new Vector2(0.0f, 16f));
          TryToReturnToBus = true;
          simperson.memberofthepublic.LeftTheParkBecauseOfThis = ParkLeavingReason.ProtestsUpsetMe;
          ++NewCustomerCalculator.TotalPeopleWhoLeftBecauseOfProtestors;
        }
        if (simperson.memberofthepublic.HasSeasonPass)
        {
          int TicketValue = 0;
          simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
          MoneyRenderer.EarnMoney(vLocation - new Vector2(0.0f, 16f), TicketValue * 100, false);
          Player.financialrecords.PurchasedATicket(TicketValue);
        }
        else
        {
          bool flag = false;
          if (simperson.customertype == CustomerType.Footballer)
            flag = true;
          if (Player.financialrecords.GetDaysPassed() < 3L)
            flag = true;
          simperson.memberofthepublic.SetUpCashHeld(num1, player.prisonlayout.cellblockcontainer);
          int idealParkTicketCost = (int) AnimalTicketValue.GetIdealParkTicketCost(player);
          float num2 = idealParkTicketCost != 0 ? (float) num1 / (float) idealParkTicketCost : (float) num1 * 0.05f + 1f;
          if ((double) num2 <= 1.0)
          {
            int num3 = (int) ((double) num2 * 10.0);
            if (Game1.Rnd.Next(0, 100) > num3 | flag)
            {
              simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
              MoneyRenderer.EarnMoney(vLocation - new Vector2(0.0f, 16f), num1 * 100, false);
              Player.financialrecords.PurchasedATicket(num1);
            }
          }
          else
          {
            int maxValue = (int) ((double) (num2 - 1f) * 100.0);
            if (maxValue > 99)
            {
              if (Game1.Rnd.Next(0, maxValue) == 0 && Game1.Rnd.Next(0, 100) > 19)
              {
                simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
                Player.financialrecords.PurchasedATicket(num1);
                MoneyRenderer.EarnMoney(vLocation - new Vector2(0.0f, 16f), num1 * 100, false);
              }
            }
            else if (Game1.Rnd.Next(0, 100) > maxValue && Game1.Rnd.Next(0, 100) > 24)
            {
              simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
              MoneyRenderer.EarnMoney(vLocation - new Vector2(0.0f, 16f), num1 * 100, false);
              Player.financialrecords.PurchasedATicket(num1);
            }
          }
          if (simperson.memberofthepublic.CashHeld < num1 * 100)
            simperson.memberofthepublic.ThisCustomerDecidedNotToPay = true;
          if (simperson.memberofthepublic.ThisCustomerDecidedNotToPay)
          {
            IsWalking = false;
            MoneyRenderer.SomethingIsTooExpensive(vLocation - new Vector2(0.0f, 16f));
            TryToReturnToBus = true;
            simperson.memberofthepublic.LeftTheParkBecauseOfThis = ParkLeavingReason.TicketTooExpensive;
            Player.financialrecords.DidNotPurchasedATicket();
          }
          else
            simperson.memberofthepublic.BuyThing(num1 * 100, FOODTYPE.ZooTicket);
        }
      }
    }
  }
}
