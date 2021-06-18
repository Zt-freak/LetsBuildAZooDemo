// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Notification.NotificationPackage
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tile_Data;
using TinyZoo.Tutorials.Z_Tips;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_Notification
{
  internal class NotificationPackage
  {
    public int DaysWithoutFood;
    public AnimalType Animal;
    public Z_NotificationType notificationtype;
    public int AnimalOrPenUID;
    public Vector2 Location;
    public AnimalFoodType foodtype;
    public float TotalAnimalFoodExpired;
    public TILETYPE tiletype;
    public CauseOfDeath CauseOfDeath;
    public ZTipType TipType;
    public HeroQuestDescription questDesc;
    public int CustomerHasTalkedAboutThis = 1;
    public WalkingPerson RefPerson;
    public int PenUID = -1;
    public int TotalDuplicates;
    public NotificationAlertStatus AlertStatus = NotificationAlertStatus.Count;
    public List<int> ListOfImpactedAnimalsByUID;
    public List<int> ListOfImpactedAnimalsByValue;
    public ReasonForNotification reasonfornotification;
    public AnimalRenderDescriptor hybridAnimal;

    public NotificationPackage(Z_NotificationType _notificationtype, Vector2 _Location)
    {
      this.Location = _Location;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(Z_NotificationType _notificationtype, ZTipType _MoneyCost)
    {
      this.TipType = _MoneyCost;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(
      Z_NotificationType _notificationtype,
      HeroQuestDescription _questDesc)
    {
      this.questDesc = _questDesc;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(Z_NotificationType _notificationtype, WalkingPerson _walkingperson)
    {
      this.RefPerson = _walkingperson;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(Z_NotificationType _notificationtype, TILETYPE _tiletype)
    {
      this.tiletype = _tiletype;
      this.notificationtype = _notificationtype;
    }

    internal static string GetZ_NotificationTypeToString(Z_NotificationType notificationtype)
    {
      switch (notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          return "Birth";
        case Z_NotificationType.A_AnimalBirthInBreedingRoom:
          return "Planned Birth";
        case Z_NotificationType.A_AnimalTransferedFromBreedingRoom:
          return "Transfer";
        case Z_NotificationType.A_AnimalHunger:
          return "Hunger";
        case Z_NotificationType.A_AnimalDeath:
          return "Death";
        case Z_NotificationType.A_CRISPR_HybridBirth:
          return "CRISPR";
        case Z_NotificationType.CannotReachGate:
          return "Path Blocked";
        case Z_NotificationType.F_GateBroke:
          return "Animal Breakout";
        case Z_NotificationType.C_Population_BuyThing_GenericStore:
          return "Commerce";
        case Z_NotificationType.Dep_Population_OpenTheZoo:
          return "Open now";
        case Z_NotificationType.F_BuildArchitect:
          return "Research";
        case Z_NotificationType.F_BuildABench:
          return "Benches";
        case Z_NotificationType.F_BuildABin:
          return "Bins";
        case Z_NotificationType.A_BuildFirstPen:
        case Z_NotificationType.A_BuildAnotherPen:
          return "Build Pen";
        case Z_NotificationType.A_AddAnimalsToYourPen:
          return "Add Animals";
        case Z_NotificationType.A_NoWater:
          return "Water";
        case Z_NotificationType.A_NoEnrichment:
          return "Enrichment";
        case Z_NotificationType.F_BuildAnyShop:
          return "Shops";
        case Z_NotificationType.F_BuildAFoodShop:
          return "Food Shops";
        case Z_NotificationType.F_BuildADrinksShop:
          return "Drinks Shops";
        case Z_NotificationType.F_BuildAGiftShop:
          return "Gift Shops";
        case Z_NotificationType.F_AShopNeedsAnEmployee:
        case Z_NotificationType.F_ShopHasExtraOpenPositions:
          return "Hire";
        case Z_NotificationType.F_AJobHasApplicants:
          return "Applicants";
        case Z_NotificationType.Q_QuestComplete:
          return "Quest";
        case Z_NotificationType.F_FoodTrash:
          return "Trash";
        case Z_NotificationType.F_VomitTrash:
          return "Barf";
        case Z_NotificationType.A_NoWaterConnection:
          return "Water Link";
        case Z_NotificationType.A_AnimalSick:
          return "Sick";
        case Z_NotificationType.A_AnimalStarving:
          return "Starvation";
        case Z_NotificationType.ExpiredAnimalFood:
          return "Expired Food";
        case Z_NotificationType.F_ResearchComplete:
          return "Research Complete!";
        case Z_NotificationType.F_TicketPrice:
          return "Ticket Prices";
        case Z_NotificationType.E_VetHasNoRoute:
          return "Needs Work";
        default:
          return "NOT FOUND TEXT_" + (object) notificationtype;
      }
    }

    internal static OffscreenPointerType GetZ_NotificationTypeToOffscreenPointerType(
      Z_NotificationType notificationtype)
    {
      switch (notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          return OffscreenPointerType.None;
        case Z_NotificationType.A_AnimalHunger:
          return OffscreenPointerType.HungryAnimals;
        case Z_NotificationType.A_AnimalDeath:
          return OffscreenPointerType.DeadAnimals;
        case Z_NotificationType.A_CRISPR_HybridBirth:
          return OffscreenPointerType.CripserBirth;
        case Z_NotificationType.A_AddAnimalsToYourPen:
          return OffscreenPointerType.AddAnimalsToYourPen;
        case Z_NotificationType.A_NoWater:
          return OffscreenPointerType.PointAtNoWater;
        case Z_NotificationType.A_NoEnrichment:
          return OffscreenPointerType.NoEnrichment;
        case Z_NotificationType.F_AJobHasApplicants:
          return OffscreenPointerType.JobApplicants;
        case Z_NotificationType.A_NoWaterConnection:
          return OffscreenPointerType.NoWaterConnection;
        case Z_NotificationType.A_AnimalSick:
          return OffscreenPointerType.SickAnimals;
        case Z_NotificationType.F_TicketPrice:
          return OffscreenPointerType.TicketPrice;
        default:
          return OffscreenPointerType.None;
      }
    }

    public NotificationPackage(
      Z_NotificationType _notificationtype,
      AnimalFoodType _foodtype,
      float TotalExpired)
    {
      this.notificationtype = _notificationtype;
      this.foodtype = _foodtype;
      this.TotalAnimalFoodExpired = TotalExpired;
    }

    public NotificationPackage(Z_NotificationType _notificationtype, int _AnimalOrPenUID = -1)
    {
      this.AnimalOrPenUID = _AnimalOrPenUID;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(
      Z_NotificationType _notificationtype,
      int _AnimalUID,
      int _DaysWithoutFood = -1,
      int _PenUID = -1)
    {
      this.PenUID = _PenUID;
      if (_AnimalUID == -1 && (_notificationtype != Z_NotificationType.A_AnimalHunger && _notificationtype != Z_NotificationType.A_AddAnimalsToYourPen || _AnimalUID != -1))
        throw new Exception("CANNOT LOR");
      this.DaysWithoutFood = _DaysWithoutFood;
      this.AnimalOrPenUID = _AnimalUID;
      this.notificationtype = _notificationtype;
    }

    public NotificationPackage(
      Z_NotificationType _notificationtype,
      int _AnimalUID,
      CauseOfDeath _CauseOfDeath,
      AnimalType _Animal,
      int _PenUID = -1)
    {
      if (_AnimalUID == -1)
        throw new Exception("CANNOT LOR");
      this.PenUID = _PenUID;
      this.Animal = _Animal;
      this.CauseOfDeath = _CauseOfDeath;
      this.AnimalOrPenUID = _AnimalUID;
      this.notificationtype = _notificationtype;
    }
  }
}
