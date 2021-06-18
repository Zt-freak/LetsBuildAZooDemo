// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Notification.Z_NotificationManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI.OveWorldUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.Speech;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.CRISPR;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_Notification;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_OverWorld.Speech;
using TinyZoo.Z_TrashSystem;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_Notification
{
  internal class Z_NotificationManager
  {
    internal static bool RecountAllEvents;
    internal static bool MadeABuilding = false;
    internal static bool OpenedZoo;
    internal static bool RescrubTickets;
    internal static bool RescrubNewbornsOnTradeAnimal = false;
    internal static bool RescrubJobApplicants;
    internal static bool RescrubAnimals;
    internal static bool RescubEnrichment;
    internal static bool ScrubCrisprBirths;
    internal static bool ScrubEmptyPensForNewAnimals = false;
    internal static bool RescrubShops = false;
    internal static bool ScrubTrash = false;
    internal static List<int> RecountWaterHere;
    private static bool RecountAllWater = false;
    private static List<ButtHolder> ShortCutButts;
    private OWCategoryButton ShortcutbuttEmergency;
    private OWCategoryButton ShortcutbuttFacilities;
    private OWCategoryButton ShortcutbuttAnimals;
    private static LerpHandler_Float lerper;
    private static List<NotificationPackage> notifications = new List<NotificationPackage>();
    private static List<NotificationPackage> UnsaidNotifications = new List<NotificationPackage>();
    private NotificationRibbonManager SummaryRibbon;
    private static List<int> EnrichmentsAddedToday;
    private static List<int> AnimalsFFedToday = new List<int>();
    internal static bool RescrubWater = false;
    internal static bool RescrubDeath = false;
    private static bool HasDiscussedTrashToday;

    public Z_NotificationManager()
    {
      Z_NotificationManager.RecountAllEvents = true;
      Z_NotificationManager.lerper = new LerpHandler_Float();
      Z_NotificationManager.lerper.SetLerp(true, 1f, 0.0f, 3f);
      Z_NotificationManager.ShortCutButts = new List<ButtHolder>();
      for (int index = 0; index < 5; ++index)
        Z_NotificationManager.ShortCutButts.Add(new ButtHolder((NotificationCategory) index));
    }

    internal static void JustFedThis(int UID)
    {
      Z_NotificationManager.AnimalsFFedToday.Add(UID);
      Z_NotificationManager.RescrubAnimals = true;
    }

    internal static void JustAddedEnrichmentToThis(int CellUID)
    {
      if (Z_NotificationManager.EnrichmentsAddedToday == null)
        Z_NotificationManager.EnrichmentsAddedToday = new List<int>();
      Z_NotificationManager.EnrichmentsAddedToday.Add(CellUID);
    }

    internal static void AddNotificationPackageToCustomerSpeechOnly(
      NotificationPackage notificationpackage)
    {
      Z_NotificationManager.UnsaidNotifications.Add(notificationpackage);
    }

    public static bool ThisIsAnUnderEmployedBuilding() => false;

    internal static void SetRecountAll() => Z_NotificationManager.RecountAllEvents = true;

    internal static NotificationPackage TryGetNotificationPackage(
      Z_NotificationType notificationtype)
    {
      for (int index = 0; index < Z_NotificationManager.notifications.Count; ++index)
      {
        if (Z_NotificationManager.notifications[index].notificationtype == notificationtype)
          return Z_NotificationManager.notifications[index];
      }
      return (NotificationPackage) null;
    }

    internal static void AddPenIDTorecountWater(int UID)
    {
      if (UID == -1)
        Z_NotificationManager.RecountAllWater = true;
      if (Z_NotificationManager.RecountWaterHere == null)
      {
        Z_NotificationManager.RecountWaterHere = new List<int>();
        Z_NotificationManager.RecountWaterHere.Add(UID);
      }
      if (Z_NotificationManager.RecountWaterHere.Contains(UID))
        return;
      Z_NotificationManager.RecountWaterHere.Add(UID);
    }

    internal static void TryAndSaySomething(Player player, int PenUID = -1, WalkingPerson walkingperson = null)
    {
      if (!SpeechManager.CustomerCanSpeak())
        return;
      if (!Z_NotificationManager.HasDiscussedTrashToday && Z_TrashManager.TrashLevelNeedsMessage(ref Z_NotificationManager.HasDiscussedTrashToday))
      {
        Z_NotificationManager.HasDiscussedTrashToday = true;
        string SayThis;
        switch (TinyZoo.Game1.Rnd.Next(0, 6))
        {
          case 0:
            SayThis = "This place has~way too much trash.";
            break;
          case 1:
            SayThis = "Why is there so~much garbage?.";
            break;
          case 2:
            SayThis = "Doesn't anyone clean this place?";
            break;
          case 3:
            SayThis = "There is probably more wildlife living~in the trash than is on display.";
            break;
          case 4:
            SayThis = "This zoo needs a janitor.";
            break;
          case 5:
            SayThis = "The sign should have said Trash-Land.";
            break;
          default:
            SayThis = "Why are people throwing so much trash on the floor?";
            break;
        }
        SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, SayThis);
      }
      else
      {
        for (int index = 0; index < Z_NotificationManager.UnsaidNotifications.Count; ++index)
        {
          if (Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis > 0)
          {
            if (PenUID > -1)
            {
              if (Z_NotificationManager.UnsaidNotifications[index].PenUID == PenUID)
              {
                if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_NoWater)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_NoEnrichment)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_AnimalBirth)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_AnimalDeath)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_AnimalHunger)
                {
                  if (player.livestats.HasHungyAnimalsInThisPen(Z_NotificationManager.UnsaidNotifications[index].PenUID))
                  {
                    --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                    SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                  }
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.A_AnimalSick)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype);
                }
                else if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == Z_NotificationType.F_TicketPrice)
                {
                  --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
                  SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype, TipType: Z_NotificationManager.UnsaidNotifications[index].TipType);
                }
                Z_NotificationManager.UnsaidNotifications.RemoveAt(index);
                break;
              }
            }
            else if (Z_NotificationManager.UnsaidNotifications[index].PenUID <= -1)
            {
              --Z_NotificationManager.UnsaidNotifications[index].CustomerHasTalkedAboutThis;
              SpeechManager.AddPersonAndComment(walkingperson, SpeechEvent.Count, notificationtype: Z_NotificationManager.UnsaidNotifications[index].notificationtype, TipType: Z_NotificationManager.UnsaidNotifications[index].TipType);
              break;
            }
          }
        }
      }
    }

    internal static void RemoveThis(Z_NotificationType notificationtype, int AnimalOrPenUID)
    {
      NotificationCategory notificationCategory = Z_NotificationManager.GetZ_NotificationTypeToNotificationCategory(notificationtype);
      Z_NotificationManager.ShortCutButts[(int) notificationCategory].RemoveThis(notificationtype, AnimalOrPenUID);
    }

    private void DoScrubTrash(Player player)
    {
      if (Player.financialrecords.GetDaysPassed() <= 2L)
        return;
      NotificationPackage notpackage1 = new NotificationPackage(Z_NotificationType.F_VomitTrash);
      int totalLandUnlocked = PlayerStats.GetTotalLandUnlocked();
      notpackage1.AlertStatus = player.prisonlayout.trashandstuff.TotalVomit <= 6 * totalLandUnlocked ? (player.prisonlayout.trashandstuff.TotalVomit <= 3 * totalLandUnlocked ? NotificationAlertStatus.Tick : NotificationAlertStatus.Exclamation) : NotificationAlertStatus.Danger_Worst;
      Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_VomitTrash);
      Z_NotificationManager.ShortCutButts[3].AddNotification(notpackage1);
      NotificationPackage notpackage2 = new NotificationPackage(Z_NotificationType.F_FoodTrash);
      notpackage2.AlertStatus = player.prisonlayout.trashandstuff.TotalFoodTrash <= 8 * totalLandUnlocked ? (player.prisonlayout.trashandstuff.TotalFoodTrash <= 4 * totalLandUnlocked ? NotificationAlertStatus.Tick : NotificationAlertStatus.Exclamation) : NotificationAlertStatus.Danger_Worst;
      Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_FoodTrash);
      Z_NotificationManager.ShortCutButts[3].AddNotification(notpackage2);
    }

    private void DoRescrubNewbornsOnTradeAnimal(Player player)
    {
      List<NotificationPackage> notificationsOfThisType = Z_NotificationManager.ShortCutButts[0].GetAllNotificationsOfThisType(Z_NotificationType.A_AnimalBirth);
      if (notificationsOfThisType.Count <= 0)
        return;
      for (int index = notificationsOfThisType.Count - 1; index > -1; --index)
      {
        if (player.prisonlayout.cellblockcontainer.GetThisAnimal(notificationsOfThisType[index].AnimalOrPenUID, out int _) == null)
          notificationsOfThisType.RemoveAt(0);
      }
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_AnimalBirth);
      if (notificationsOfThisType.Count > 0)
      {
        for (int index = notificationsOfThisType.Count - 1; index > -1; --index)
          Z_NotificationManager.ShortCutButts[0].AddNotification(notificationsOfThisType[index]);
      }
      if (!PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_AnimalBirth))
        return;
      PointOffScreenManager.AddPointerFromNotification(notificationsOfThisType);
    }

    internal static string GetNotificationCategoryToSTring(NotificationCategory notcat)
    {
      switch (notcat)
      {
        case NotificationCategory.Animals:
          return "Animals";
        case NotificationCategory.Staff:
          return "Staff";
        case NotificationCategory.Customers:
          return "Visitors";
        case NotificationCategory.Facilities:
          return "Facilities";
        case NotificationCategory.Quests:
          return "Quests";
        default:
          return "NotificationCategory NOT FOUND";
      }
    }

    internal static void LerpAllOn()
    {
      if (Z_NotificationManager.lerper == null)
        return;
      Z_NotificationManager.lerper.SetLerp(false, 1f, 0.0f, 3f, true);
    }

    internal static void LerpAllOff() => Z_NotificationManager.lerper.SetLerp(false, 0.0f, 1f, 3f);

    internal static void RemoveThese(Z_NotificationType notificationtype)
    {
      for (int index = Z_NotificationManager.notifications.Count - 1; index > -1; --index)
      {
        if (Z_NotificationManager.notifications[index].notificationtype == notificationtype)
          Z_NotificationManager.notifications.RemoveAt(index);
      }
      for (int index = 0; index < Z_NotificationManager.ShortCutButts.Count; ++index)
        Z_NotificationManager.ShortCutButts[index].RemoveThis(notificationtype);
    }

    internal static void StartNewDay()
    {
      Z_NotificationManager.AnimalsFFedToday = new List<int>();
      Z_NotificationManager.UnsaidNotifications = new List<NotificationPackage>();
      Z_NotificationManager.HasDiscussedTrashToday = false;
      Z_NotificationManager.EnrichmentsAddedToday = new List<int>();
      Z_NotificationManager.RemoveThese(Z_NotificationType.A_AnimalBirth);
    }

    internal static void AddNotificationPackage(NotificationPackage notification, Player player)
    {
      if (notification.AlertStatus != NotificationAlertStatus.Tick)
        Z_NotificationManager.UnsaidNotifications.Add(notification);
      if (!player.Stats.TutorialsComplete[4] && !DebugFlags.DisableTutorials_DEP_KeepTrue && notification.notificationtype != Z_NotificationType.C_Population_BuyThing_GenericStore)
        return;
      Z_NotificationManager.notifications.Add(notification);
    }

    internal static void StripNotificationsFromCustomerSpeech(Z_NotificationType notificationtype)
    {
      for (int index = Z_NotificationManager.UnsaidNotifications.Count - 1; index > -1; --index)
      {
        if (Z_NotificationManager.UnsaidNotifications[index].notificationtype == notificationtype)
          Z_NotificationManager.UnsaidNotifications.RemoveAt(index);
      }
    }

    internal static bool HasThisNotification(Z_NotificationType notification)
    {
      for (int index = 0; index < Z_NotificationManager.notifications.Count; ++index)
      {
        if (Z_NotificationManager.notifications[index].notificationtype == notification)
          return true;
      }
      for (int index = 0; index < Z_NotificationManager.ShortCutButts.Count; ++index)
      {
        if (Z_NotificationManager.ShortCutButts[index].HasThisNotification(notification))
          return true;
      }
      return false;
    }

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      if (this.SummaryRibbon != null)
        flag |= this.SummaryRibbon.CheckMouseOver(player);
      return flag;
    }

    public void UpdateZ_NotificationManager(Player player, float DeltaTime)
    {
      if (Z_NotificationManager.ScrubTrash)
      {
        Z_NotificationManager.ScrubTrash = false;
        this.DoScrubTrash(player);
      }
      if (Z_NotificationManager.ScrubCrisprBirths)
      {
        this.ScrubCRISPRBabies(player);
        Z_NotificationManager.ScrubCrisprBirths = false;
      }
      if (Z_NotificationManager.RescrubNewbornsOnTradeAnimal)
      {
        Z_NotificationManager.RescrubNewbornsOnTradeAnimal = false;
        this.DoRescrubNewbornsOnTradeAnimal(player);
      }
      if (Z_NotificationManager.ScrubEmptyPensForNewAnimals)
      {
        Z_NotificationManager.ScrubEmptyPensForNewAnimals = false;
        this.JustScrubEmptyPens(player);
      }
      if (Z_NotificationManager.RescrubShops || Z_NotificationManager.MadeABuilding)
      {
        this.DoShops(player);
        Z_NotificationManager.RescrubShops = false;
        Z_NotificationManager.MadeABuilding = false;
      }
      if (Z_NotificationManager.RescrubJobApplicants)
      {
        this.DoShops(player);
        Z_NotificationManager.RescrubJobApplicants = false;
      }
      if (Z_NotificationManager.RescrubTickets)
      {
        Z_NotificationManager.RescrubTickets = false;
        Z_NotificationManager.DoTicketPrice(player);
      }
      if (Z_NotificationManager.RescrubAnimals || Z_NotificationManager.RescrubDeath || Z_NotificationManager.RescubEnrichment)
      {
        Z_NotificationManager.RescrubDeath = false;
        this.DoAnimals(player);
        Z_NotificationManager.DoTicketPrice(player);
        Z_NotificationManager.RescrubAnimals = false;
        Z_NotificationManager.RescubEnrichment = false;
      }
      if (Z_NotificationManager.RecountAllEvents || Z_NotificationManager.RescrubDeath || Z_NotificationManager.RescrubShops)
      {
        Z_NotificationManager.RescrubWater = false;
        Z_NotificationManager.RecountAllEvents = false;
        this.RecountAndResetPermanentNotifications(player);
        this.DoScrubTrash(player);
        Z_NotificationManager.RescrubDeath = false;
        Z_NotificationManager.RescrubJobApplicants = false;
        Z_NotificationManager.RescrubShops = false;
        Z_NotificationManager.DoTicketPrice(player);
      }
      else if (Z_NotificationManager.RescrubWater)
      {
        this.RecountAndResetPermanentNotifications(player);
        Z_NotificationManager.RescrubWater = false;
      }
      if (Z_NotificationManager.notifications.Count > 0)
      {
        for (int index = 0; index < Z_NotificationManager.notifications.Count; ++index)
          this.HandleNotification(Z_NotificationManager.notifications[index], player);
        Z_NotificationManager.notifications = new List<NotificationPackage>();
      }
      Z_NotificationManager.lerper.UpdateLerpHandler(DeltaTime);
      int ActiveBeforeThis = 0;
      if (this.SummaryRibbon == null)
      {
        for (int index = 0; index < Z_NotificationManager.ShortCutButts.Count; ++index)
        {
          if (Z_NotificationManager.ShortCutButts[index].UpdateButtHolder(ref ActiveBeforeThis, DeltaTime, player))
          {
            UIScaleHelper uiScaleHelper = new UIScaleHelper(Z_GameFlags.GetBaseScaleForUI());
            Vector2 buttLocation = Z_NotificationManager.ShortCutButts[0].GetButtLocation();
            buttLocation.Y -= Z_NotificationManager.ShortCutButts[0].GetButtSize().Y * 0.5f;
            buttLocation.X = uiScaleHelper.GetDefaultXBuffer();
            this.SummaryRibbon = new NotificationRibbonManager(RibbonType.Summary, Z_NotificationManager.ShortCutButts[index], buttLocation);
          }
        }
      }
      if (this.SummaryRibbon != null)
      {
        bool remakeRibbon = false;
        NotificationInfo notificationinfo;
        if (this.SummaryRibbon.UpdateNotificationRibbonManager(player, DeltaTime, out notificationinfo, ref remakeRibbon))
          this.SummaryRibbon = (NotificationRibbonManager) null;
        if (this.SummaryRibbon != null && notificationinfo != null)
          this.SummaryRibbon.REF_buttonpressed.HandleNotification(this.SummaryRibbon.REF_buttonpressed.GetAllNotificationsOfThisType(notificationinfo.notificationType), out bool _, player);
        if (!remakeRibbon || this.SummaryRibbon == null || !this.SummaryRibbon.RefreshRibbon())
          return;
        this.SummaryRibbon = (NotificationRibbonManager) null;
      }
      else
      {
        FeatureFlags.BlockAlerts = ActiveBeforeThis == 0;
        FeatureFlags.BlockAlerts = false;
        if (!Z_NotificationManager.MadeABuilding && !Z_NotificationManager.OpenedZoo)
          return;
        Z_NotificationManager.ShortCutButts[2].CheckNotifications(player);
        Z_NotificationManager.MadeABuilding = false;
      }
    }

    private void ScrubThisAnimalFromAllNotifications(int AnimalUID) => Z_NotificationManager.ShortCutButts[0].ScrubThisAnimalFromAllNotifications(AnimalUID);

    internal static NotificationCategory GetZ_NotificationTypeToNotificationCategory(
      Z_NotificationType notificationtype)
    {
      switch (notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
        case Z_NotificationType.A_AnimalTransferedFromBreedingRoom:
        case Z_NotificationType.A_AnimalHunger:
        case Z_NotificationType.A_AnimalDeath:
        case Z_NotificationType.A_CRISPR_HybridBirth:
        case Z_NotificationType.A_BuildFirstPen:
        case Z_NotificationType.A_AddAnimalsToYourPen:
        case Z_NotificationType.A_NoWater:
        case Z_NotificationType.A_NoEnrichment:
        case Z_NotificationType.A_BuildAnotherPen:
        case Z_NotificationType.A_AnimalSick:
        case Z_NotificationType.A_AnimalStarving:
        case Z_NotificationType.A_CuredAnimal:
          return NotificationCategory.Animals;
        case Z_NotificationType.F_GateBroke:
        case Z_NotificationType.F_BuildArchitect:
        case Z_NotificationType.F_BuildAnyShop:
        case Z_NotificationType.F_BuildAFoodShop:
        case Z_NotificationType.F_BuildADrinksShop:
        case Z_NotificationType.F_BuildAGiftShop:
        case Z_NotificationType.F_AShopNeedsAnEmployee:
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
        case Z_NotificationType.F_AJobHasApplicants:
        case Z_NotificationType.F_ResearchComplete:
          return NotificationCategory.Facilities;
        case Z_NotificationType.C_Population_BuyThing_GenericStore:
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          return NotificationCategory.Customers;
        case Z_NotificationType.Q_QuestComplete:
          return NotificationCategory.Quests;
        case Z_NotificationType.E_VetHasNoRoute:
          return NotificationCategory.Staff;
        default:
          throw new Exception("MISSED IT");
      }
    }

    private void HandleNotification(NotificationPackage notification, Player player)
    {
      if (notification.notificationtype == Z_NotificationType.A_AnimalDeath)
        this.ScrubThisAnimalFromAllNotifications(notification.AnimalOrPenUID);
      switch (notification.notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
        case Z_NotificationType.A_AnimalTransferedFromBreedingRoom:
        case Z_NotificationType.A_AnimalHunger:
        case Z_NotificationType.A_AnimalDeath:
        case Z_NotificationType.A_CRISPR_HybridBirth:
        case Z_NotificationType.A_BuildFirstPen:
        case Z_NotificationType.A_AddAnimalsToYourPen:
        case Z_NotificationType.A_NoWater:
        case Z_NotificationType.A_NoEnrichment:
        case Z_NotificationType.A_BuildAnotherPen:
        case Z_NotificationType.A_NoWaterConnection:
        case Z_NotificationType.A_AnimalSick:
        case Z_NotificationType.A_AnimalStarving:
          Z_NotificationManager.ShortCutButts[0].TryAndScrubAnimalDuplicates(notification);
          Z_NotificationManager.ShortCutButts[0].AddNotification(notification);
          break;
        case Z_NotificationType.CannotReachGate:
        case Z_NotificationType.E_VetHasNoRoute:
          Z_NotificationManager.ShortCutButts[1].AddNotification(notification);
          break;
        case Z_NotificationType.F_GateBroke:
          Z_NotificationManager.ShortCutButts[3].AddNotification(notification);
          break;
        case Z_NotificationType.C_Population_BuyThing_GenericStore:
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          Z_NotificationManager.ShortCutButts[2].AddNotification(notification);
          break;
        case Z_NotificationType.F_BuildArchitect:
          if (Z_NotificationManager.ShortCutButts[3].HasThisNotification(Z_NotificationType.F_BuildArchitect))
            break;
          Z_NotificationManager.ShortCutButts[3].AddNotification(notification);
          break;
        case Z_NotificationType.F_BuildAnyShop:
        case Z_NotificationType.F_BuildAFoodShop:
        case Z_NotificationType.F_BuildADrinksShop:
        case Z_NotificationType.F_BuildAGiftShop:
        case Z_NotificationType.F_AShopNeedsAnEmployee:
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
        case Z_NotificationType.F_AJobHasApplicants:
          if (Z_NotificationManager.ShortCutButts[3].HasThisNotification(notification.notificationtype))
            break;
          Z_NotificationManager.ShortCutButts[3].AddNotification(notification);
          break;
        case Z_NotificationType.Q_QuestComplete:
          Z_NotificationManager.ShortCutButts[4].TryAndScrubQuestDuplicates(notification);
          Z_NotificationManager.ShortCutButts[4].AddNotification(notification);
          break;
        case Z_NotificationType.A_CuredAnimal:
          Z_NotificationManager.ShortCutButts[0].TryAndRemoveSickAnimal(notification.AnimalOrPenUID);
          Z_NotificationManager.ShortCutButts[0].TryAndScrubAnimalDuplicates(notification);
          Z_NotificationManager.ShortCutButts[0].AddNotification(notification);
          break;
        case Z_NotificationType.F_ResearchComplete:
          Z_NotificationManager.ShortCutButts[3].AddNotification(notification);
          break;
        case Z_NotificationType.F_TicketPrice:
          Z_NotificationManager.ShortCutButts[3].AddNotification(notification);
          break;
        default:
          throw new Exception("ASSIGN THIS");
      }
    }

    public void RecountAndResetPermanentNotifications(Player player)
    {
      this.DoAnimals(player);
      this.DoShops(player);
    }

    private void JustScrubEmptyPens(Player player)
    {
      List<int> EmptyPensUID = new List<int>();
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
      {
        if (player.prisonlayout.cellblockcontainer.prisonzones[index].prisonercontainer.prisoners.Count == 0 && !player.animalsonorder.HasOrderToThisPen(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID))
          EmptyPensUID.Add(player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID);
      }
      this.DeEmptyPenNotificationCheck(EmptyPensUID, player);
    }

    private void DeEmptyPenNotificationCheck(List<int> EmptyPensUID, Player player)
    {
      NotificationPackage notpackage = new NotificationPackage(Z_NotificationType.A_AddAnimalsToYourPen);
      if (EmptyPensUID.Count > 0)
      {
        notpackage.ListOfImpactedAnimalsByUID = EmptyPensUID;
        notpackage.AlertStatus = NotificationAlertStatus.Exclamation;
        if (EmptyPensUID.Count > 5)
          notpackage.AlertStatus = NotificationAlertStatus.Danger_Worst;
      }
      else
      {
        notpackage.AlertStatus = NotificationAlertStatus.Tick;
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.AddAnimalsToYourPen, player);
      }
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_AddAnimalsToYourPen);
      Z_NotificationManager.ShortCutButts[0].AddNotification(notpackage);
      if (!PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_AddAnimalsToYourPen))
        return;
      PointOffScreenManager.RemovePointer(OffscreenPointerType.AddAnimalsToYourPen);
      if (notpackage.AlertStatus == NotificationAlertStatus.Tick)
        return;
      PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
      {
        notpackage
      });
    }

    private void ScrubCRISPRBabies(Player player)
    {
      int num = 0;
      bool flag = PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_CRISPR_HybridBirth);
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_CRISPR_HybridBirth);
      for (int index1 = 0; index1 < player.crisprBreeds.CRISPRBuildings.Count; ++index1)
      {
        for (int index2 = 0; index2 < player.crisprBreeds.CRISPRBuildings[index1].crisprBreeds.Length; ++index2)
        {
          if (player.crisprBreeds.CRISPRBuildings[index1].crisprBreeds != null && player.crisprBreeds.CRISPRBuildings[index1].crisprBreeds[index2] != null && player.crisprBreeds.CRISPRBuildings[index1].crisprBreeds[index2].IsBorn_CanCollect)
          {
            CrisprActiveBreed crisprBreed = player.crisprBreeds.CRISPRBuildings[index1].crisprBreeds[index2];
            int uid = crisprBreed.UID;
            AnimalRenderDescriptor renderDescriptor = new AnimalRenderDescriptor(crisprBreed.resultBody, crisprBreed.resultBodyVariant, crisprBreed.resultHead, crisprBreed.resultHeadVariant);
            ++num;
            NotificationPackage notpackage = new NotificationPackage(Z_NotificationType.A_CRISPR_HybridBirth, uid);
            notpackage.AlertStatus = NotificationAlertStatus.Special_Heart;
            notpackage.hybridAnimal = renderDescriptor;
            Z_NotificationManager.ShortCutButts[0].AddNotification(notpackage);
            if (flag)
              PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
              {
                notpackage
              });
          }
        }
      }
      if (num > 0 || !flag)
        return;
      ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.CripserBirth, player);
      PointOffScreenManager.RemovePointer(OffscreenPointerType.CripserBirth);
    }

    private void DoAnimals(Player player)
    {
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_NoWater);
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_NoEnrichment);
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      float num5 = 0.0f;
      List<int> intList1 = new List<int>();
      List<int> intList2 = new List<int>();
      List<int> intList3 = new List<int>();
      List<int> intList4 = new List<int>();
      List<int> EmptyPensUID = new List<int>();
      int num6 = 0;
      int num7 = 0;
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        if (Z_NotificationManager.RecountWaterHere != null && Z_NotificationManager.RecountWaterHere.Contains(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID))
          NewDay_ByPen.RecountWaterCoverage(player, player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID);
        else if (Z_NotificationManager.RecountAllWater)
          NewDay_ByPen.RecountWaterCoverage(player, player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID);
        if ((double) player.prisonlayout.cellblockcontainer.prisonzones[index1].TempTotalWater <= 0.0 && player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count > 0)
        {
          if (!player.prisonlayout.cellblockcontainer.prisonzones[index1].HasWaterTrough())
          {
            ++num1;
            if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.HasLivingAnimals())
              ++num3;
          }
          else if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.HasLivingAnimals())
            ++num2;
        }
        if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count == 0 && !player.animalsonorder.HasOrderToThisPen(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID))
          EmptyPensUID.Add(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID);
        if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo == null)
          player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.SetUpTempAnimals(player.prisonlayout.cellblockcontainer.prisonzones[index1].Cell_UID, player.prisonlayout.cellblockcontainer.prisonzones[index1].CellBLOCKTYPE, player, true);
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo.Count; ++index2)
        {
          if ((double) player.prisonlayout.cellblockcontainer.prisonzones[index1].TempAnimalEnrichmentUnfulfillment > 0.0)
          {
            num5 += player.prisonlayout.cellblockcontainer.prisonzones[index1].TempAnimalEnrichmentUnfulfillment;
            num4 += player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.tempAnimalInfo[index2].AllOfThese.Count;
          }
        }
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2]._IsDead)
          {
            intList3.Add(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID);
            if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysSinceDeath > num7)
              num7 = player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysSinceDeath;
            intList4.Add(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysSinceDeath);
          }
          else if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysWithoutFood > 2 && !Z_NotificationManager.AnimalsFFedToday.Contains(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID))
          {
            intList1.Add(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.UID);
            intList2.Add(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysWithoutFood);
            if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysWithoutFood > num6)
              num6 = player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].DaysWithoutFood;
          }
        }
      }
      Z_NotificationManager.RecountAllWater = false;
      Z_NotificationManager.RecountWaterHere = (List<int>) null;
      NotificationPackage notificationPackage1 = new NotificationPackage(Z_NotificationType.A_NoWater);
      notificationPackage1.TotalDuplicates = num1;
      if (num3 > 0)
      {
        notificationPackage1.AlertStatus = NotificationAlertStatus.Danger_Worst;
        notificationPackage1.CustomerHasTalkedAboutThis = 2;
      }
      else
      {
        notificationPackage1.CustomerHasTalkedAboutThis = 0;
        notificationPackage1.AlertStatus = NotificationAlertStatus.Tick;
      }
      Z_NotificationManager.ShortCutButts[0].TryAndScrubAnimalDuplicates(notificationPackage1);
      Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage1);
      if (notificationPackage1.CustomerHasTalkedAboutThis > 0)
        Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage1);
      else
        Z_NotificationManager.StripNotificationsFromCustomerSpeech(Z_NotificationType.A_NoWater);
      if (notificationPackage1.AlertStatus == NotificationAlertStatus.Tick)
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.PointAtNoWater, player);
      if (PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_NoWater))
      {
        if (notificationPackage1.AlertStatus == NotificationAlertStatus.Tick)
          PointOffScreenManager.RemovePointer(OffscreenPointerType.PointAtNoWater);
        else
          PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
          {
            notificationPackage1
          });
      }
      NotificationPackage notificationPackage2 = new NotificationPackage(Z_NotificationType.A_NoWaterConnection);
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_NoWaterConnection);
      notificationPackage2.TotalDuplicates = 1;
      if (num2 > 1)
      {
        notificationPackage2.AlertStatus = NotificationAlertStatus.Danger_Worst;
        notificationPackage2.CustomerHasTalkedAboutThis = 2;
        Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage2);
      }
      else if (num2 > 0)
      {
        notificationPackage2.CustomerHasTalkedAboutThis = 1;
        notificationPackage2.AlertStatus = NotificationAlertStatus.Exclamation;
        Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage2);
      }
      else
      {
        notificationPackage2.CustomerHasTalkedAboutThis = 0;
        notificationPackage2.AlertStatus = NotificationAlertStatus.Tick;
      }
      if (notificationPackage2.CustomerHasTalkedAboutThis > 0)
        Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage2);
      if (notificationPackage2.AlertStatus == NotificationAlertStatus.Tick)
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.NoWaterConnection, player);
      if (PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_NoWaterConnection))
      {
        if (notificationPackage2.AlertStatus == NotificationAlertStatus.Tick)
          PointOffScreenManager.RemovePointer(OffscreenPointerType.NoWaterConnection);
        else
          PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
          {
            notificationPackage2
          });
      }
      int num8 = Z_NotificationManager.RescrubWater ? 1 : 0;
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_NoEnrichment);
      NotificationPackage notificationPackage3 = new NotificationPackage(Z_NotificationType.A_NoEnrichment);
      notificationPackage3.TotalDuplicates = num4;
      if ((double) num5 > 10.0)
      {
        notificationPackage3.AlertStatus = NotificationAlertStatus.Danger_Worst;
        notificationPackage3.CustomerHasTalkedAboutThis = 2;
      }
      else if ((double) num5 > 0.0)
      {
        notificationPackage3.CustomerHasTalkedAboutThis = 1;
        notificationPackage3.AlertStatus = NotificationAlertStatus.Exclamation;
      }
      else
      {
        Z_NotificationManager.StripNotificationsFromCustomerSpeech(Z_NotificationType.A_NoEnrichment);
        notificationPackage3.CustomerHasTalkedAboutThis = 0;
        notificationPackage3.AlertStatus = NotificationAlertStatus.Tick;
      }
      Z_NotificationManager.ShortCutButts[0].TryAndScrubAnimalDuplicates(notificationPackage3);
      Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage3);
      if (notificationPackage3.CustomerHasTalkedAboutThis > 0)
        Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage3);
      if (notificationPackage3.AlertStatus == NotificationAlertStatus.Tick)
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.NoEnrichment, player);
      if (PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_NoEnrichment))
      {
        if (notificationPackage3.AlertStatus == NotificationAlertStatus.Tick)
          PointOffScreenManager.RemovePointer(OffscreenPointerType.NoEnrichment);
        else
          PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
          {
            notificationPackage3
          });
      }
      new NotificationPackage(Z_NotificationType.A_AddAnimalsToYourPen, -1, -1, -1).AlertStatus = NotificationAlertStatus.Tick;
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_AddAnimalsToYourPen);
      this.DeEmptyPenNotificationCheck(EmptyPensUID, player);
      NotificationPackage notificationPackage4 = new NotificationPackage(Z_NotificationType.A_AnimalHunger, -1, -1, -1);
      notificationPackage4.AlertStatus = NotificationAlertStatus.Tick;
      if (num6 > 0 && num6 < 4 && intList1.Count < 6)
      {
        notificationPackage4.AlertStatus = NotificationAlertStatus.Danger_Worst;
        notificationPackage4.CustomerHasTalkedAboutThis = 2;
      }
      else if (num6 > 0)
      {
        notificationPackage4.CustomerHasTalkedAboutThis = 1;
        notificationPackage4.AlertStatus = NotificationAlertStatus.Danger_Worst;
      }
      else
      {
        notificationPackage4.CustomerHasTalkedAboutThis = 0;
        notificationPackage4.AlertStatus = NotificationAlertStatus.Tick;
      }
      notificationPackage4.ListOfImpactedAnimalsByUID = intList1;
      notificationPackage4.ListOfImpactedAnimalsByValue = intList2;
      Z_NotificationManager.ShortCutButts[0].TryAndScrubAnimalDuplicates(notificationPackage4);
      if (notificationPackage4.CustomerHasTalkedAboutThis > 0)
      {
        Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage4);
        notificationPackage4.reasonfornotification = ReasonForNotification.HungryAnimals_Logistics;
        if (!player.storerooms.HasBuiltStoreRoom)
          notificationPackage4.reasonfornotification = ReasonForNotification.HungryAnimals_NoStoreRoom;
        else if (player.employees.GetEmployeesOfThisType(EmployeeType.Keeper).Count == 0)
          notificationPackage4.reasonfornotification = ReasonForNotification.HungryAnimals_NoZooKeeper;
        else if (player.storerooms.HasRunOutOfSomethingThatIsNeededByAnimals)
          notificationPackage4.reasonfornotification = ReasonForNotification.HungryAnimals_NoFood;
      }
      Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage4);
      if (notificationPackage4.AlertStatus == NotificationAlertStatus.Tick)
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.HungryAnimals, player);
      if (PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.A_AnimalHunger))
      {
        if (notificationPackage4.AlertStatus == NotificationAlertStatus.Tick)
          PointOffScreenManager.RemovePointer(OffscreenPointerType.HungryAnimals);
        else
          PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
          {
            notificationPackage4
          });
      }
      Z_NotificationManager.ShortCutButts[0].RemoveThis(Z_NotificationType.A_AnimalDeath);
      NotificationPackage notificationPackage5 = new NotificationPackage(Z_NotificationType.A_AnimalDeath);
      notificationPackage5.TotalDuplicates = intList3.Count;
      if (intList3.Count > 3 || num7 > 3)
      {
        notificationPackage5.AlertStatus = NotificationAlertStatus.Danger_Worst;
        notificationPackage5.CustomerHasTalkedAboutThis = 2;
      }
      else if (intList3.Count > 0)
      {
        notificationPackage5.CustomerHasTalkedAboutThis = 1;
        notificationPackage5.AlertStatus = NotificationAlertStatus.Exclamation;
      }
      else
      {
        notificationPackage5.CustomerHasTalkedAboutThis = 0;
        notificationPackage5.AlertStatus = NotificationAlertStatus.Tick;
      }
      notificationPackage5.ListOfImpactedAnimalsByUID = intList3;
      notificationPackage5.ListOfImpactedAnimalsByValue = intList2;
      if (notificationPackage5.AlertStatus == NotificationAlertStatus.Tick)
        Z_NotificationManager.ShortCutButts[0].AddNotification(notificationPackage5);
      if (notificationPackage5.CustomerHasTalkedAboutThis > 0)
        Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage5);
      for (int index = 0; index < Z_NotificationManager.ShortCutButts[0].notificationpackages.Count; ++index)
        Console.WriteLine("Animal Not-" + (object) Z_NotificationManager.ShortCutButts[0].notificationpackages[index].notificationtype);
    }

    private static void DoTicketPrice(Player player)
    {
      Expensiveness ticketExpensivenss = AnimalTicketValue.GetTicketExpensivenss(player);
      NotificationPackage notificationPackage = new NotificationPackage(Z_NotificationType.F_TicketPrice);
      Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_TicketPrice);
      switch (ticketExpensivenss)
      {
        case Expensiveness.TooCheap:
          notificationPackage.AlertStatus = NotificationAlertStatus.Exclamation;
          notificationPackage.TipType = ZTipType.TicketPriceTooCheap;
          Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage);
          break;
        case Expensiveness.TooExpensive:
          notificationPackage.AlertStatus = NotificationAlertStatus.Exclamation;
          notificationPackage.TipType = ZTipType.TicketPriceTooExpensive;
          Z_NotificationManager.AddNotificationPackageToCustomerSpeechOnly(notificationPackage);
          break;
        default:
          notificationPackage.AlertStatus = NotificationAlertStatus.Tick;
          if (notificationPackage.AlertStatus == NotificationAlertStatus.Tick)
            ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.TicketPrice, player);
          Z_NotificationManager.StripNotificationsFromCustomerSpeech(Z_NotificationType.F_TicketPrice);
          break;
      }
      Z_NotificationManager.ShortCutButts[3].AddNotification(notificationPackage);
      if (!PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.F_TicketPrice))
        return;
      if (notificationPackage.AlertStatus == NotificationAlertStatus.Tick)
        PointOffScreenManager.RemovePointer(OffscreenPointerType.TicketPrice);
      else
        PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
        {
          notificationPackage
        });
    }

    public void DoShops(Player player)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      int num4 = 0;
      List<int> intList = new List<int>();
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        ++num4;
        if (TileData.IsForFood(player.shopstatus.shopentries[index].tiletype))
          ++num2;
        else if (TileData.IsForThirst(player.shopstatus.shopentries[index].tiletype))
          ++num1;
        else if (TileData.IsForSouvenir(player.shopstatus.shopentries[index].tiletype))
          ++num3;
        if (player.shopstatus.shopentries[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.shopentries[index].tiletype, player) && !TileData.IsThisAVendingMachine(player.shopstatus.shopentries[index].tiletype))
          intList.Add(player.shopstatus.shopentries[index].ShopUID);
      }
      for (int index = 0; index < player.shopstatus.FacilitiesWithEmployees.Count; ++index)
      {
        if (player.shopstatus.FacilitiesWithEmployees[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.FacilitiesWithEmployees[index].tiletype, player))
          intList.Add(player.shopstatus.FacilitiesWithEmployees[index].ShopUID);
      }
      for (int index = 0; index < player.shopstatus.ArchitectOffice.Count; ++index)
      {
        if (player.shopstatus.ArchitectOffice[index].GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(player.shopstatus.ArchitectOffice[index].tiletype, player))
          intList.Add(player.shopstatus.ArchitectOffice[index].ShopUID);
      }
      Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_AShopNeedsAnEmployee);
      if (intList.Count > 0)
      {
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_AShopNeedsAnEmployee);
        Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_AShopNeedsAnEmployee)
        {
          AlertStatus = NotificationAlertStatus.Danger_Worst,
          ListOfImpactedAnimalsByUID = intList
        });
      }
      else
        Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_AShopNeedsAnEmployee)
        {
          AlertStatus = NotificationAlertStatus.Tick
        });
      if (num4 == 0)
      {
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildAnyShop);
        Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildAnyShop)
        {
          AlertStatus = NotificationAlertStatus.Exclamation
        });
      }
      else
      {
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildAnyShop);
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildAnyShop, player);
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildAFoodShop);
        if (num2 > 0)
          ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildAFoodShop, player);
        else
          Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildAFoodShop)
          {
            AlertStatus = NotificationAlertStatus.Exclamation
          });
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildADrinksShop);
        if (num1 > 0)
          ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildADrinksShop, player);
        else
          Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildADrinksShop)
          {
            AlertStatus = NotificationAlertStatus.Exclamation
          });
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildAGiftShop);
        if (num3 > 0)
          ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildAGiftShop, player);
        else
          Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildAGiftShop)
          {
            AlertStatus = NotificationAlertStatus.Exclamation
          });
      }
      if (num4 > 1 && Player.financialrecords.GetDaysPassed() > 1L)
      {
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildABin);
        if (player.shopstatus.Bins.Count > 0)
          ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildABin, player);
        else
          Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildABin)
          {
            AlertStatus = NotificationAlertStatus.Exclamation
          });
      }
      if (Player.financialrecords.GetDaysPassed() > 4L)
      {
        Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_BuildABench);
        if (player.shopstatus.Benches.Count > 0)
          ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.BuildABench, player);
        else
          Z_NotificationManager.ShortCutButts[3].AddNotification(new NotificationPackage(Z_NotificationType.F_BuildABench)
          {
            AlertStatus = NotificationAlertStatus.Exclamation
          });
      }
      List<OpenPositions> applicantsOpenPositions = player.employees.openPositionsContainer.GetAllJobsWithApplicantsOpenPositions();
      Z_NotificationManager.ShortCutButts[3].RemoveThis(Z_NotificationType.F_AJobHasApplicants);
      if (applicantsOpenPositions.Count > 0)
      {
        NotificationPackage notpackage = new NotificationPackage(Z_NotificationType.F_AJobHasApplicants);
        notpackage.AlertStatus = NotificationAlertStatus.Special_Star;
        Z_NotificationManager.ShortCutButts[3].AddNotification(notpackage);
        if (!PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.F_AJobHasApplicants))
          return;
        PointOffScreenManager.AddPointerFromNotification(new List<NotificationPackage>()
        {
          notpackage
        });
      }
      else
      {
        if (!PointOffScreenManager.DoubleCheckNotification(Z_NotificationType.F_AJobHasApplicants))
          return;
        ZHudManager.zquestpins.UnPinQuest(OffscreenPointerType.JobApplicants, player);
      }
    }

    public void DrawZ_NotificationManager()
    {
      if (OverWorldManager.overworldstate == OverWOrldState.CellSelect)
        return;
      float val1 = 0.0f;
      if (this.SummaryRibbon != null)
        val1 = this.SummaryRibbon.GetOffset();
      for (int index = 0; index < Z_NotificationManager.ShortCutButts.Count; ++index)
        Z_NotificationManager.ShortCutButts[index].DrawButtHolder(Math.Max(val1, Z_NotificationManager.lerper.Value) * -300f);
      if (this.SummaryRibbon == null)
        return;
      this.SummaryRibbon.DrawNotificationRibbonManager(AssetContainer.pointspritebatchTop05);
    }
  }
}
