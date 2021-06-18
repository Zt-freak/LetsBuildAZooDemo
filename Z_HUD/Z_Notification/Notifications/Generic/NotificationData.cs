// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.Z_Notification.Notifications.Generic.NotificationData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_HUD.Z_Notification.Notifications.Generic
{
  internal class NotificationData
  {
    public static string GetNotificationHeaderText(NotificationPackage notification)
    {
      switch (notification.notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          return "Births";
        case Z_NotificationType.A_AnimalHunger:
          return "Hunger";
        case Z_NotificationType.A_AnimalDeath:
          return "Deaths";
        case Z_NotificationType.CannotReachGate:
          return "Gate Blocked";
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          return "Open Zoo";
        case Z_NotificationType.F_BuildArchitect:
          return "Research";
        case Z_NotificationType.F_BuildABench:
          return "Build Bench";
        case Z_NotificationType.F_BuildABin:
          return "Build a Bin";
        case Z_NotificationType.A_BuildFirstPen:
          return "Build Pen";
        case Z_NotificationType.A_AddAnimalsToYourPen:
          return "Add Animals";
        case Z_NotificationType.A_NoWater:
          return "Water";
        case Z_NotificationType.A_NoEnrichment:
          return "Enrichment";
        case Z_NotificationType.A_BuildAnotherPen:
          return "Build Pen";
        case Z_NotificationType.F_BuildAnyShop:
          return "Build Shop";
        case Z_NotificationType.F_BuildAFoodShop:
          return "Food Shop";
        case Z_NotificationType.F_BuildADrinksShop:
          return "Drink Shop";
        case Z_NotificationType.F_BuildAGiftShop:
          return "Gift Shop";
        case Z_NotificationType.F_AShopNeedsAnEmployee:
          return "Hire Employee";
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
          return "Hire Employee";
        case Z_NotificationType.F_AJobHasApplicants:
          return "Applicants";
        case Z_NotificationType.A_NoWaterConnection:
          return "No Water Link";
        case Z_NotificationType.A_AnimalSick:
          return "Animal Sick";
        case Z_NotificationType.F_TicketPrice:
          return "Ticket Price";
        case Z_NotificationType.E_VetHasNoRoute:
          return "Work Needed";
        default:
          return "NA_Found: " + (object) notification.notificationtype;
      }
    }

    public static string GetNotificationBodyText(
      NotificationPackage notification,
      bool everythingsok)
    {
      switch (notification.notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          return "There are no new births.";
        case Z_NotificationType.A_AnimalHunger:
          return "All your animals have adequate food.";
        case Z_NotificationType.A_AnimalDeath:
          return "There are no new deaths.";
        case Z_NotificationType.CannotReachGate:
          return "A zookeeper cannot reach an enclosure gate to feed an animal.";
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          return "Open your zoo to begin a new day and start earning money!";
        case Z_NotificationType.F_BuildArchitect:
          return "Build a Research Hub to start researching new buildings.";
        case Z_NotificationType.F_BuildABench:
          return "When visitors get tired, they will walk very slowly.~Building benches and cafes can help give the visitor a chance to recharge their batteries!";
        case Z_NotificationType.F_BuildABin:
          return "Build bins so visitors have a place to put their garbage.~Otherwise it will just pile up on the floor and ruin your Zoo's reputation.";
        case Z_NotificationType.A_BuildFirstPen:
          return "You do not have any pens. Build one now!";
        case Z_NotificationType.A_AddAnimalsToYourPen:
          return everythingsok ? "Your pens all have animals inside of inbound." : "There is a pen without animals. Add some animals to fill it up!";
        case Z_NotificationType.A_NoWater:
          return everythingsok ? "All your pens have an adequate water supply." : "You have a pen that does not have any water supply! Build some water troughs for your animals.";
        case Z_NotificationType.A_NoEnrichment:
          return everythingsok ? "All your animals have enrichment." : "You have a pen without any enrichment items placed. Build some enrichment items for your animals to play with!";
        case Z_NotificationType.A_BuildAnotherPen:
          return "Grow your zoo and its reputation by having more animals. Start by building more pens!";
        case Z_NotificationType.F_BuildAnyShop:
          return "Build a shop to start earning revenue from your visitors!";
        case Z_NotificationType.F_BuildAFoodShop:
          return "Visitors get hungry as they walk around the zoo. Build a food shop to fill their hunger and earn more revenue!";
        case Z_NotificationType.F_BuildADrinksShop:
          return "Visitors get thirsty as they walk around the zoo. Build a drink shop to quench their thirst and earn more revenue!";
        case Z_NotificationType.F_BuildAGiftShop:
          return "Build a gift shop to earn more revenue from your visitors!";
        case Z_NotificationType.F_AShopNeedsAnEmployee:
          if (notification.AlertStatus == NotificationAlertStatus.Tick)
            return "Your shops are all fully staffed to their maximum employee capacity.";
          return notification.AlertStatus == NotificationAlertStatus.Exclamation ? "One or more of your shops has less than the maximum number of employees." : "One or more of your shops does not have an employee. Hire one to open the shop for business.";
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
          return "Your shop has space for more employees. Hire more employees to serve customers more efficiently!";
        case Z_NotificationType.F_AJobHasApplicants:
          return "You have got new applicants for a job position you have posted! Review the applicants to hire one!";
        case Z_NotificationType.A_NoWaterConnection:
          return everythingsok ? "All your water basins are linked to water punmps." : "An animal water trough is beyond the range of a water pump, either move a water pump nearer or build a new one close to the water trough.";
        case Z_NotificationType.A_AnimalSick:
          return "None of your animals are sick.";
        case Z_NotificationType.F_TicketPrice:
          if (notification.TipType == ZTipType.TicketPriceTooExpensive)
            return "Your ticket price is too high! To change it click on the ticket office.";
          return notification.TipType == ZTipType.TicketPriceTooCheap ? "Your ticket price is too low! To change it click on the ticket office." : "Your ticket price is just about right.";
        case Z_NotificationType.E_VetHasNoRoute:
          return "Assign a work route for your vet. Without one, they will be inefficient.";
        default:
          return "NA_Data:" + (object) notification.notificationtype;
      }
    }
  }
}
