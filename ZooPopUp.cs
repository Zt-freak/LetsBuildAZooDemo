// Decompiled with JetBrains decompiler
// Type: TinyZoo.ZooPopUp
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalDonation.PopUp;
using TinyZoo.Z_AnimalNotification;
using TinyZoo.Z_BreedResult.VariantDiscovered;
using TinyZoo.Z_BreedScreen;
using TinyZoo.Z_BuildingInfo.IncineratorBuildingInfo;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo;
using TinyZoo.Z_BuildingInfo.QuarantineBuildingInfo.Settings;
using TinyZoo.Z_BuildingInfo.SlaughterhouseBuildingInfo.CullingSettings;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.MedicalJournal;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.ResearchManager;
using TinyZoo.Z_BuildingInfo.VetBuilding.Disease.VisitSummary;
using TinyZoo.Z_BuildingInfo.WarehouseBuildingInfo;
using TinyZoo.Z_BuldMenu.ChangeBuildingSkin;
using TinyZoo.Z_BuldMenu.Confirmation;
using TinyZoo.Z_Bus.BussInfo;
using TinyZoo.Z_Collection;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_CRISPR;
using TinyZoo.Z_Employees;
using TinyZoo.Z_Employees.GeneralEmployees;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_Farms.CropSum;
using TinyZoo.Z_HUD;
using TinyZoo.Z_HUD.AnimalDeliveryUI;
using TinyZoo.Z_HUD.StoreRoom;
using TinyZoo.Z_HUD.Z_Notification.NotificationRibbon;
using TinyZoo.Z_HUD.Z_Notification.Notifications.Cleanliness;
using TinyZoo.Z_HUD.Z_Notification.Notifications.Generic;
using TinyZoo.Z_ManageEmployees;
using TinyZoo.Z_Notification;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_Processing;
using TinyZoo.Z_Quests.CharacterQuests;
using TinyZoo.Z_SummaryPopUps.CriticalChoice;
using TinyZoo.Z_SummaryPopUps.EventReport;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;
using TinyZoo.Z_SummaryPopUps.ParkSummary;
using TinyZoo.Z_SummaryPopUps.People;
using TinyZoo.Z_SummaryPopUps.People.Animal;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark;
using TinyZoo.Z_TicketPrice;
using TinyZoo.Z_TicketPrice.Rides;
using TinyZoo.Z_WorldMap.WorldMapPopUps.NewThingRenderer;

namespace TinyZoo
{
  internal class ZooPopUp
  {
    private CustomerPopUp customrinfo;
    public POPUPSTATE thisstate;
    private GenericNotificationManager genericnotificationpopup;
    private AnimalPopUpManager animalpopupmanager;
    private QuitManager quitjob;
    private BreedPopUp breedpopup;
    private WorkZoneFullPanelManager zonemanager;
    private TicketPriceManager_Holder ticketmanager;
    private CRISPRManager crisprmanager;
    private ManageEmployeeManager manageEmployeeManager;
    private GeneralEmployeeManager generalemplyeemanager;
    private AnimalNotificationManager animalnotificationmanager;
    private VariantDiscoveredManager variantdiscoveredmanager;
    private ParkSummaryManager parksummarymanager;
    private BusInfoPanel businfopanel;
    public HeroQuestPanelManager heroquestPanel;
    public ChangeBuildingSkinManager changebuildingskinmanager;
    private CollectionManager collectionmanager;
    private RideTicketManager rideticketmanager;
    private SaveNotification savenotification;
    private AnimalDeliveryPanelManager animaldeliverypanelmanager;
    private AnimalFoodSummary animalfoodsummary;
    private PeopleInParkManager peopleinparkmanager;
    private DiseaseResearchManager diseasemanager;
    private MedicalJournalManager medicaljournalmanager;
    private PenVisitSummary penvistsummary;
    private QuaratinedAnimalsManager quaratinemanager;
    private QuarantineSettingsManager quarantineSettingsManager;
    private IncineratorBuildingManager incineratorBuildingManager;
    private Z_ProcessingManager processingManager;
    private CropSummary cropsummary;
    private CullingSettingManager cullingSettingsManager;
    private WarehouseInfoManager warehouseManager;
    private EventReportManager eventreportmanager;
    private CriticalChoiceManager criticalchoicemanager;
    private FeatureUnlockManager featureunlockmanager;
    private CleanlinessNotificationManager cleanlinessnotificationmanager;
    private DestroyPenConfirmationPopUp destroypenpopup;
    private NewThingPopUpManager newThingPanel;
    private AnimalDonationPopUpManager animaldonationmanager;
    public float HOLDTIMER;
    private NewThingPanel UI_Test;
    public FeatureUnlockDisplayType ForceThisFeatureUnlockedPopUp = FeatureUnlockDisplayType.Count;

    public ZooPopUp(WalkingPerson person, Player player)
    {
      if (FeatureFlags.BlockPersonInfo || OverWorldManager.overworldstate != OverWOrldState.MainMenu)
      {
        this.thisstate = POPUPSTATE.None;
      }
      else
      {
        this.customrinfo = new CustomerPopUp(person, 0.66666f, player);
        this.thisstate = POPUPSTATE.People;
      }
    }

    public ZooPopUp(POPUPSTATE popupstate, WalkingPerson walkingperson)
    {
      if (popupstate != POPUPSTATE.EventReport)
      {
        if (popupstate != POPUPSTATE.CriticalChoice)
          throw new Exception("THIS ISNT WHAT YOU MEAN TO CALL");
        this.criticalchoicemanager = new CriticalChoiceManager(walkingperson);
      }
      else
        this.eventreportmanager = new EventReportManager(walkingperson);
      this.thisstate = popupstate;
    }

    public ZooPopUp(Player player, bool IsBreakOut, PrisonZone prisonzone, POPUPSTATE popupstate)
    {
      if (popupstate == POPUPSTATE.AnimalDelivery)
      {
        this.thisstate = POPUPSTATE.AnimalDelivery;
        this.animaldeliverypanelmanager = new AnimalDeliveryPanelManager(player, prisonzone);
      }
      else
      {
        this.animalnotificationmanager = new AnimalNotificationManager(player, AnimalNotificationType.Breakout, prisonzone.prisonercontainer.prisoners[0]);
        this.thisstate = POPUPSTATE.AnimalNotification;
      }
    }

    public ZooPopUp(BuildingManageButton actiontype, Player player)
    {
      switch (actiontype)
      {
        case BuildingManageButton.StoreRoom:
          this.thisstate = POPUPSTATE.ViewAnimalFoodSupply;
          this.animalfoodsummary = new AnimalFoodSummary(player);
          break;
        case BuildingManageButton.GetParkStaff:
          this.ConstructGeneralEmployeeManager(player);
          break;
        case BuildingManageButton.ParkSummary:
          this.thisstate = POPUPSTATE.ParkSummary;
          this.parksummarymanager = new ParkSummaryManager(player);
          break;
        case BuildingManageButton.Transport:
          this.thisstate = POPUPSTATE.Transport;
          this.businfopanel = new BusInfoPanel(player);
          break;
        case BuildingManageButton.Surveillance_People:
          this.thisstate = POPUPSTATE.PeopleInPark;
          this.peopleinparkmanager = new PeopleInParkManager();
          break;
        case BuildingManageButton.DiseaseResearch:
          this.thisstate = POPUPSTATE.Vet_DiseaseResearch;
          this.diseasemanager = new DiseaseResearchManager(Z_GameFlags.GetBaseScaleForUI(), player);
          break;
        case BuildingManageButton.MedicalJournal:
          this.thisstate = POPUPSTATE.Vet_MedicalJounral;
          this.medicaljournalmanager = new MedicalJournalManager(Z_GameFlags.GetBaseScaleForUI(), player);
          break;
        case BuildingManageButton.VetVistSummary:
          this.thisstate = POPUPSTATE.Vet_PenVisitSummary;
          this.penvistsummary = new PenVisitSummary(Z_GameFlags.GetBaseScaleForUI());
          break;
        case BuildingManageButton.QuarantineSettings:
          this.thisstate = POPUPSTATE.QuarantineSettings;
          this.quarantineSettingsManager = new QuarantineSettingsManager(player);
          break;
        case BuildingManageButton.Slaughterhouse_Culling:
          this.thisstate = POPUPSTATE.Slaughterhouse_Culling;
          this.cullingSettingsManager = new CullingSettingManager(player);
          break;
        default:
          throw new Exception("WRONG CONSTRUCTOR");
      }
    }

    public ZooPopUp(BuildingManageButton actiontype, PrisonZone prisonzone, Player player)
    {
      if (actiontype != BuildingManageButton.Destroy)
        return;
      this.thisstate = POPUPSTATE.DestroyPen;
      this.destroypenpopup = new DestroyPenConfirmationPopUp(prisonzone);
    }

    public ZooPopUp(bool ISTEST_CONSTRUCTOR, Player player)
    {
      this.animaldonationmanager = new AnimalDonationPopUpManager(player);
      this.thisstate = POPUPSTATE.DonateAnimals;
    }

    public ZooPopUp(
      POPUPSTATE popstate,
      Player player,
      TILETYPE buildingtype,
      Vector2Int SelectedLcation = null)
    {
      switch (popstate)
      {
        case POPUPSTATE.ChangeBuildingSkin:
          this.thisstate = popstate;
          this.changebuildingskinmanager = new ChangeBuildingSkinManager(buildingtype, SelectedLcation);
          break;
        case POPUPSTATE.RideTicket:
          this.thisstate = popstate;
          this.rideticketmanager = new RideTicketManager(player, buildingtype);
          break;
        case POPUPSTATE.Incinerator:
          this.thisstate = popstate;
          this.incineratorBuildingManager = new IncineratorBuildingManager(player.shopstatus.GetThisFacility(SelectedLcation).ShopUID, buildingtype, player);
          break;
        case POPUPSTATE.ManageProcessing:
          this.thisstate = popstate;
          this.processingManager = new Z_ProcessingManager(player.shopstatus.GetThisFacility(SelectedLcation).ShopUID, buildingtype, player);
          break;
        case POPUPSTATE.Warehouse:
          this.thisstate = popstate;
          this.warehouseManager = new WarehouseInfoManager(player.shopstatus.GetThisFacility(SelectedLcation).ShopUID, buildingtype, player);
          break;
        default:
          throw new Exception("HUH?!?! WHAT STATE??");
      }
    }

    public ZooPopUp(FeatureUnlockDisplayType featureunlocktype, FeatureUnlockSpeechPack pack = null)
    {
      this.thisstate = POPUPSTATE.FeatureUnlock;
      this.featureunlockmanager = new FeatureUnlockManager(featureunlocktype, pack);
    }

    public ZooPopUp(
      WorkZoneInfo workZoneInfo,
      TILETYPE buildingtype = TILETYPE.Count,
      SelectedAndSellManager selectedtileandsell = null)
    {
      this.thisstate = POPUPSTATE.Zoning;
      this.zonemanager = new WorkZoneFullPanelManager(Z_GameFlags.GetBaseScaleForUI(), workZoneInfo, buildingtype, selectedtileandsell);
    }

    public ZooPopUp(QuarantineBuilding quarantineBuilding, Player player)
    {
      this.thisstate = POPUPSTATE.QuarantinedAnimals;
      this.quaratinemanager = new QuaratinedAnimalsManager(quarantineBuilding, player);
    }

    public ZooPopUp(Player player, Vector2Int Buildinglocation, POPUPSTATE popupstate)
    {
      if (popupstate == POPUPSTATE.BreedSelection)
      {
        this.breedpopup = new BreedPopUp(player.breeds.GetNurseryBuilding(Buildinglocation), player);
        this.thisstate = POPUPSTATE.BreedSelection;
      }
      else
      {
        if (popupstate != POPUPSTATE.CRISPR)
          throw new Exception("oihsdfsdf UNEXPECTED STATE");
        this.crisprmanager = new CRISPRManager(player.crisprBreeds.GetCRISPRBuilding(Buildinglocation), player);
        this.thisstate = POPUPSTATE.CRISPR;
      }
    }

    public ZooPopUp(Employee employeetoquit, ZOOMOMENT moment)
    {
      this.quitjob = new QuitManager(employeetoquit.quickemplyeedescription);
      this.thisstate = POPUPSTATE.EmployeeQuit;
    }

    public ZooPopUp(
      POPUPSTATE popstate,
      Vector2Int builingLocation,
      TILETYPE tileType,
      Player player)
    {
      if (popstate != POPUPSTATE.ManageEmployee)
        throw new Exception("DONT USE THIS!");
      this.ConstructManageEmployeeManager(builingLocation, tileType, player);
    }

    private void ConstructManageEmployeeManager(
      Vector2Int builingLocation,
      TILETYPE tileType,
      Player player,
      EmployeeType employeetype = EmployeeType.None,
      POPUPSTATE cameFromThisState = POPUPSTATE.None)
    {
      this.manageEmployeeManager = new ManageEmployeeManager(builingLocation, tileType, player, employeetype, cameFromThisState);
      this.thisstate = POPUPSTATE.ManageEmployee;
    }

    private void ConstructGeneralEmployeeManager(
      Player player,
      EmployeeType forceToPageWithThisEmployeeType = EmployeeType.None)
    {
      this.thisstate = POPUPSTATE.GeneralEmployees;
      this.generalemplyeemanager = new GeneralEmployeeManager(player, forceToPageWithThisEmployeeType);
    }

    public ZooPopUp(PrisonerInfo anaimal, Player player)
    {
      this.animalpopupmanager = new AnimalPopUpManager(anaimal, player);
      this.thisstate = POPUPSTATE.Animal;
    }

    public ZooPopUp(AnimalType genomeUnlocked, POPUPSTATE popUpState)
    {
      this.newThingPanel = new NewThingPopUpManager(genomeUnlocked);
      this.thisstate = POPUPSTATE.NewThing;
    }

    public ZooPopUp(
      List<NotificationPackage> notifications,
      out bool RemoveThisNotification,
      Player player)
    {
      NotificationPackage notification1 = notifications[0];
      bool flag = false;
      if (notification1.AlertStatus == NotificationAlertStatus.Tick)
        flag = true;
      switch (notifications[0].notificationtype)
      {
        case Z_NotificationType.A_AnimalBirth:
          List<PrisonerInfo> infolist1 = new List<PrisonerInfo>();
          foreach (NotificationPackage notification2 in notifications)
            infolist1.Add(player.prisonlayout.GetThisAnimal(notification2.AnimalOrPenUID, out int _));
          this.animalnotificationmanager = new AnimalNotificationManager(player, AnimalNotificationType.Birth, infolist1, notifications);
          this.thisstate = POPUPSTATE.AnimalNotification;
          RemoveThisNotification = false;
          break;
        case Z_NotificationType.A_AnimalHunger:
          if (!flag)
          {
            List<PrisonerInfo> infolist2 = new List<PrisonerInfo>();
            foreach (int UID in notifications[0].ListOfImpactedAnimalsByUID)
              infolist2.Add(player.prisonlayout.GetThisAnimal(UID, out int _));
            this.animalnotificationmanager = new AnimalNotificationManager(player, AnimalNotificationType.Hunger, infolist2, notifications);
            this.thisstate = POPUPSTATE.AnimalNotification;
          }
          else
          {
            this.genericnotificationpopup = new GenericNotificationManager(notifications);
            this.thisstate = POPUPSTATE.Notification;
          }
          RemoveThisNotification = false;
          break;
        case Z_NotificationType.A_AnimalDeath:
          if (!flag)
          {
            List<PrisonerInfo> infolist2 = new List<PrisonerInfo>();
            foreach (int UID in notifications[0].ListOfImpactedAnimalsByUID)
              infolist2.Add(player.prisonlayout.GetThisAnimal(UID, out int _));
            this.animalnotificationmanager = new AnimalNotificationManager(player, AnimalNotificationType.Death, infolist2, notifications);
            this.thisstate = POPUPSTATE.AnimalNotification;
          }
          else
          {
            this.genericnotificationpopup = new GenericNotificationManager(notifications);
            this.thisstate = POPUPSTATE.Notification;
          }
          RemoveThisNotification = true;
          break;
        case Z_NotificationType.A_CRISPR_HybridBirth:
          List<AnimalRenderDescriptor> hybridAnimals = new List<AnimalRenderDescriptor>();
          foreach (NotificationPackage notification2 in notifications)
            hybridAnimals.Add(notification2.hybridAnimal);
          this.animalnotificationmanager = new AnimalNotificationManager(player, AnimalNotificationType.CRIPSRBirth, hybridAnimals, notifications);
          this.thisstate = POPUPSTATE.AnimalNotification;
          RemoveThisNotification = false;
          break;
        case Z_NotificationType.F_GateBroke:
          PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(notifications[0].ListOfImpactedAnimalsByUID[0]);
          this.animalnotificationmanager = thisCellBlock.prisonercontainer.prisoners.Count <= 0 ? new AnimalNotificationManager(player, AnimalNotificationType.Breakout) : new AnimalNotificationManager(player, AnimalNotificationType.Breakout, thisCellBlock.prisonercontainer.prisoners);
          this.thisstate = POPUPSTATE.AnimalNotification;
          RemoveThisNotification = false;
          break;
        case Z_NotificationType.Q_QuestComplete:
          this.heroquestPanel = new HeroQuestPanelManager(player, notifications[0].questDesc);
          RemoveThisNotification = false;
          this.thisstate = POPUPSTATE.HeroQuests;
          break;
        case Z_NotificationType.F_FoodTrash:
          this.cleanlinessnotificationmanager = new CleanlinessNotificationManager(Z_NotificationType.F_FoodTrash, notifications[0].AlertStatus);
          RemoveThisNotification = false;
          this.thisstate = POPUPSTATE.Cleanliness;
          break;
        case Z_NotificationType.F_VomitTrash:
          this.cleanlinessnotificationmanager = new CleanlinessNotificationManager(Z_NotificationType.F_VomitTrash, notifications[0].AlertStatus);
          RemoveThisNotification = false;
          this.thisstate = POPUPSTATE.Cleanliness;
          break;
        default:
          this.genericnotificationpopup = new GenericNotificationManager(notifications);
          RemoveThisNotification = false;
          this.thisstate = POPUPSTATE.Notification;
          break;
      }
    }

    public ZooPopUp(ZooMoment zoomoment, Player player, out bool Launched) => throw new Exception("YOU DIDNT USE HIS");

    public ZooPopUp(Player player, POPUPSTATE popupstate)
    {
      switch (popupstate)
      {
        case POPUPSTATE.Ticket:
          this.ticketmanager = new TicketPriceManager_Holder(player);
          this.thisstate = POPUPSTATE.Ticket;
          break;
        case POPUPSTATE.Collection:
          this.collectionmanager = new CollectionManager(player);
          this.thisstate = popupstate;
          break;
        case POPUPSTATE.SaveAlert:
          this.savenotification = new SaveNotification(player);
          this.thisstate = POPUPSTATE.SaveAlert;
          break;
        case POPUPSTATE.DonateAnimals:
          this.animaldonationmanager = new AnimalDonationPopUpManager(player);
          this.thisstate = popupstate;
          break;
      }
    }

    public ZooPopUp(Player player, int FarmFieldUID, POPUPSTATE popupstate)
    {
      if (popupstate != POPUPSTATE.Crops)
        throw new Exception("NOT THIS");
      this.thisstate = POPUPSTATE.Crops;
      this.cropsummary = new CropSummary(player, FarmFieldUID);
    }

    public ZooPopUp(
      HeroQuestDescription questDesc,
      Player player,
      POPUPSTATE popupstate,
      bool isForQuestCompleted)
    {
      this.heroquestPanel = new HeroQuestPanelManager(player, questDesc, isForQuestCompleted);
      this.thisstate = POPUPSTATE.HeroQuests;
    }

    public void SetbreakOutData(int HumanDeaths, int AnimalDeaths, int AnimalsLoose)
    {
      if (this.animalnotificationmanager == null)
        return;
      this.animalnotificationmanager.SetbreakOutData(HumanDeaths, AnimalDeaths, AnimalsLoose);
    }

    public bool ShouldCancelDeltaTime() => !OverWorldManager.IsGameIntro && this.thisstate == POPUPSTATE.SaveAlert;

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      switch (this.thisstate)
      {
        case POPUPSTATE.People:
          flag = this.customrinfo.CheckMouseOver(player);
          break;
        case POPUPSTATE.Notification:
          flag = this.genericnotificationpopup.CheckMouseOver(player);
          break;
        case POPUPSTATE.Animal:
          flag = this.animalpopupmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.EmployeeQuit:
          flag = this.quitjob.CheckMouseOver(player);
          break;
        case POPUPSTATE.BreedSelection:
          flag = this.breedpopup.CheckMouseOver(player);
          break;
        case POPUPSTATE.Zoning:
          flag = this.zonemanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Ticket:
          flag = this.ticketmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.CRISPR:
          flag = this.crisprmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.ManageEmployee:
          flag = this.manageEmployeeManager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.GeneralEmployees:
          flag = this.generalemplyeemanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.HeroQuests:
          flag = this.heroquestPanel.CheckMouseOver(player);
          break;
        case POPUPSTATE.AnimalNotification:
          flag = this.animalnotificationmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.VariantDiscovered:
          flag = this.variantdiscoveredmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.ParkSummary:
          flag = this.parksummarymanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Transport:
          flag = this.businfopanel.CheckMouseOver(player);
          break;
        case POPUPSTATE.ChangeBuildingSkin:
          flag = this.changebuildingskinmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Collection:
          flag = this.collectionmanager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.RideTicket:
          flag = this.rideticketmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.SaveAlert:
          flag = this.savenotification.CheckMouseOver(player, Vector2.Zero);
          break;
        case POPUPSTATE.AnimalDelivery:
          flag = this.animaldeliverypanelmanager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.ViewAnimalFoodSupply:
          flag = this.animalfoodsummary.CheckMouseOver(player);
          if (flag)
          {
            Z_GameFlags.MouseIsOverAPanel_SoBlockZoom = true;
            break;
          }
          break;
        case POPUPSTATE.PeopleInPark:
          flag = this.peopleinparkmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Vet_DiseaseResearch:
          flag = this.diseasemanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Vet_PenVisitSummary:
          flag = this.penvistsummary.CheckMouseOver(player);
          break;
        case POPUPSTATE.Vet_MedicalJounral:
          flag = this.medicaljournalmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.QuarantinedAnimals:
          flag = this.quaratinemanager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.QuarantineSettings:
          flag = this.quarantineSettingsManager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Incinerator:
          flag = this.incineratorBuildingManager.CheckMouseOver(player);
          break;
        case POPUPSTATE.ManageProcessing:
          flag = this.processingManager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.Slaughterhouse_Culling:
          flag = this.cullingSettingsManager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.Crops:
          flag = this.cropsummary.CheckMouseOver(player);
          break;
        case POPUPSTATE.Warehouse:
          flag = this.warehouseManager.CheckMouseOver(player);
          Z_GameFlags.MouseIsOverAPanel_SoBlockZoom |= flag;
          break;
        case POPUPSTATE.FeatureUnlock:
          flag = this.featureunlockmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.Cleanliness:
          flag = this.cleanlinessnotificationmanager.CheckMouseOver(player);
          break;
        case POPUPSTATE.NewThing:
          flag = this.newThingPanel.CheckMouseOver(player);
          break;
        case POPUPSTATE.DonateAnimals:
          flag = this.animaldonationmanager.CheckMouseOver(player);
          break;
      }
      return flag;
    }

    public bool CheckHolderTimer() => (double) this.HOLDTIMER < 1.0;

    public bool UpdateZooPopUps(Player player, float DeltaTime)
    {
      this.HOLDTIMER += DeltaTime;
      if (OverWorldManager.IsGameIntro)
        return false;
      if (this.thisstate == POPUPSTATE.None)
        return true;
      bool flag = false;
      bool WillClearInput = true;
      switch (this.thisstate)
      {
        case POPUPSTATE.People:
          WillClearInput = this.customrinfo.CheckMouseOver(player);
          flag = this.customrinfo.UpdateCustomerPopUp(player, DeltaTime);
          break;
        case POPUPSTATE.Notification:
          flag = this.genericnotificationpopup.UpdateGenericNotificationManager(player, DeltaTime, ref WillClearInput);
          break;
        case POPUPSTATE.Animal:
          WillClearInput = false;
          flag = this.animalpopupmanager.UpdateAnimalPopUpManager(player, DeltaTime);
          break;
        case POPUPSTATE.EmployeeQuit:
          this.quitjob.UpdateQuitManager(player, DeltaTime);
          break;
        case POPUPSTATE.BreedSelection:
          flag = this.breedpopup.UpdateBreedPopUp(player, DeltaTime);
          break;
        case POPUPSTATE.Zoning:
          flag = this.zonemanager.UpdateWorkZoneFullPanelManager(player, DeltaTime, Vector2.Zero);
          break;
        case POPUPSTATE.UITest:
          flag = this.UI_Test.UpdateNewThingPanel(player, DeltaTime, Vector2.Zero);
          break;
        case POPUPSTATE.Ticket:
          WillClearInput = this.ticketmanager.CheckMouseOver(player);
          flag = this.ticketmanager.UpdateTicketPriceManager_Holder(player, DeltaTime, true);
          break;
        case POPUPSTATE.CRISPR:
          flag = this.crisprmanager.UpdateCRISPRManager(player, DeltaTime);
          break;
        case POPUPSTATE.ManageEmployee:
          WillClearInput = this.manageEmployeeManager.CheckMouseOver(player);
          flag = this.manageEmployeeManager.UpdateManageEmployeeManager(player, DeltaTime);
          if (this.manageEmployeeManager.ClickedPreviousButton)
          {
            this.ConstructGeneralEmployeeManager(player, this.manageEmployeeManager.RoamingEmployeeType);
            this.generalemplyeemanager.UpdateGemeralEmployeeManager(DeltaTime, player, out bool _);
            break;
          }
          break;
        case POPUPSTATE.GeneralEmployees:
          WillClearInput = this.generalemplyeemanager.CheckMouseOver(player);
          bool SwitchToManage;
          flag = this.generalemplyeemanager.UpdateGemeralEmployeeManager(DeltaTime, player, out SwitchToManage);
          if (SwitchToManage)
          {
            this.ConstructManageEmployeeManager((Vector2Int) null, TILETYPE.None, player, this.generalemplyeemanager.SwitchToDetailViewForThis, this.thisstate);
            this.manageEmployeeManager.UpdateManageEmployeeManager(player, DeltaTime);
            break;
          }
          break;
        case POPUPSTATE.HeroQuests:
          flag = this.heroquestPanel.UpdateHeroQuestPanelManager(DeltaTime, player, ref WillClearInput);
          if (flag && this.heroquestPanel.forceToThisQuest_TempForReshow != null)
          {
            flag = false;
            this.heroquestPanel = new HeroQuestPanelManager(player, this.heroquestPanel.forceToThisQuest_TempForReshow);
            break;
          }
          break;
        case POPUPSTATE.AnimalNotification:
          this.animalnotificationmanager.CheckMouseOver(player);
          flag = this.animalnotificationmanager.UpdateAnimalNotificationManager(player, DeltaTime);
          WillClearInput = false;
          break;
        case POPUPSTATE.VariantDiscovered:
          flag = this.variantdiscoveredmanager.UpdateVariantDiscoveredManager(player, DeltaTime);
          break;
        case POPUPSTATE.ParkSummary:
          WillClearInput = this.parksummarymanager.CheckMouseOver(player);
          flag = this.parksummarymanager.UpdateParkSummaryManager(player, DeltaTime);
          break;
        case POPUPSTATE.Transport:
          WillClearInput = this.businfopanel.CheckMouseOver(player);
          flag = this.businfopanel.UpdateBusInfoPanel(player, DeltaTime);
          break;
        case POPUPSTATE.ChangeBuildingSkin:
          WillClearInput = this.changebuildingskinmanager.CheckMouseOver(player);
          flag = this.changebuildingskinmanager.UpdateChangeBuildingSkinManager(player, DeltaTime, ref WillClearInput);
          break;
        case POPUPSTATE.Collection:
          flag = this.collectionmanager.UpdateCollectionManager(player, DeltaTime, Vector2.Zero, ref WillClearInput);
          break;
        case POPUPSTATE.RideTicket:
          flag = this.rideticketmanager.UpdateRideTicketManager(player, DeltaTime);
          break;
        case POPUPSTATE.SaveAlert:
          flag = this.savenotification.UpdateSaveNotification(DeltaTime, player, Vector2.Zero);
          break;
        case POPUPSTATE.AnimalDelivery:
          flag = this.animaldeliverypanelmanager.UpdateAnimalDeliveryPanelManager(player, DeltaTime);
          break;
        case POPUPSTATE.ViewAnimalFoodSupply:
          WillClearInput = false;
          flag = this.animalfoodsummary.UpdateAnimalFoodSummary(player, DeltaTime, ref WillClearInput);
          break;
        case POPUPSTATE.PeopleInPark:
          flag = this.peopleinparkmanager.UpdatePeopleInParkManager(player, DeltaTime);
          break;
        case POPUPSTATE.Vet_DiseaseResearch:
          flag = this.diseasemanager.UpdateDiseaseResearchManager(player, DeltaTime);
          break;
        case POPUPSTATE.Vet_PenVisitSummary:
          flag = this.penvistsummary.UpdatePenVisitSummary(player, DeltaTime);
          break;
        case POPUPSTATE.Vet_MedicalJounral:
          flag = this.medicaljournalmanager.UpdateMedicalJournalManager(player, DeltaTime);
          break;
        case POPUPSTATE.QuarantinedAnimals:
          flag = this.quaratinemanager.UpdateQuarantinedAnimalsManager(player, DeltaTime);
          break;
        case POPUPSTATE.QuarantineSettings:
          flag = this.quarantineSettingsManager.UpdateQuarantineSettingsManager(player, DeltaTime);
          break;
        case POPUPSTATE.Incinerator:
          flag = this.incineratorBuildingManager.UpdateIncineratorBuildingManager(player, DeltaTime);
          break;
        case POPUPSTATE.ManageProcessing:
          flag = this.processingManager.UpdateZ_ProcessingManager(DeltaTime, player);
          break;
        case POPUPSTATE.Slaughterhouse_Culling:
          flag = this.cullingSettingsManager.UpdateCullingSettingManager(player, DeltaTime);
          break;
        case POPUPSTATE.Crops:
          flag = this.cropsummary.UpdateCropSummary(player, DeltaTime);
          break;
        case POPUPSTATE.Warehouse:
          flag = this.warehouseManager.UpdateWarehouseInfoManager(player, DeltaTime);
          break;
        case POPUPSTATE.EventReport:
          flag = this.eventreportmanager.UpdateEventReportManager(player, DeltaTime);
          break;
        case POPUPSTATE.CriticalChoice:
          flag = this.criticalchoicemanager.UpdateCriticalChoiceManager(player, DeltaTime);
          if (flag && this.criticalchoicemanager.newfeaturepopup != FeatureUnlockDisplayType.Count)
          {
            this.ForceThisFeatureUnlockedPopUp = this.criticalchoicemanager.newfeaturepopup;
            break;
          }
          break;
        case POPUPSTATE.FeatureUnlock:
          flag = this.featureunlockmanager.UpdateFeatureUnlockManager(player, DeltaTime);
          break;
        case POPUPSTATE.Cleanliness:
          flag = this.cleanlinessnotificationmanager.UpdateCleanlinessNotificationManager(player, DeltaTime);
          break;
        case POPUPSTATE.DestroyPen:
          flag = this.destroypenpopup.UpdateDestroyPenConfirmation(player, DeltaTime);
          break;
        case POPUPSTATE.NewThing:
          flag = this.newThingPanel.UpdateNewThingPopUpManager(player, DeltaTime);
          break;
        case POPUPSTATE.DonateAnimals:
          flag = this.animaldonationmanager.UpdateAnimalDonationPopUpManager(player, DeltaTime);
          break;
      }
      if (WillClearInput)
        player.inputmap.ClearAllInput(player, false);
      return flag;
    }

    public void DrawZooPopUp()
    {
      if (OverWorldManager.IsGameIntro)
        return;
      switch (this.thisstate)
      {
        case POPUPSTATE.People:
          this.customrinfo.DrawCustomerPopUp();
          break;
        case POPUPSTATE.Notification:
          this.genericnotificationpopup.DrawGenericNotificationManager();
          break;
        case POPUPSTATE.Animal:
          this.animalpopupmanager.DrawAnimalPopUpManager();
          break;
        case POPUPSTATE.EmployeeQuit:
          this.quitjob.DrawQuitManager();
          break;
        case POPUPSTATE.BreedSelection:
          this.breedpopup.DrawBreedPopUp();
          break;
        case POPUPSTATE.Zoning:
          this.zonemanager.DrawWorkZoneFullPanelManager(Vector2.Zero);
          break;
        case POPUPSTATE.UITest:
          this.UI_Test.DrawNewThingPanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
          break;
        case POPUPSTATE.Ticket:
          this.ticketmanager.DrawTicketPriceManager_Holder();
          break;
        case POPUPSTATE.CRISPR:
          this.crisprmanager.DrawCRISPRManager();
          break;
        case POPUPSTATE.ManageEmployee:
          this.manageEmployeeManager.DrawManageEmployeeManager();
          break;
        case POPUPSTATE.GeneralEmployees:
          this.generalemplyeemanager.DrawGemeralEmployeeManager();
          break;
        case POPUPSTATE.HeroQuests:
          this.heroquestPanel.DrawHeroQuestPanelManager(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.AnimalNotification:
          this.animalnotificationmanager.DrawAnimalNotificationManager();
          break;
        case POPUPSTATE.VariantDiscovered:
          this.variantdiscoveredmanager.DrawVariantDiscoveredManager();
          break;
        case POPUPSTATE.ParkSummary:
          this.parksummarymanager.DrawParkSummaryManager();
          break;
        case POPUPSTATE.Transport:
          this.businfopanel.DrawBusInfoPanel();
          break;
        case POPUPSTATE.ChangeBuildingSkin:
          this.changebuildingskinmanager.DrawChangeBuildingSkinManager();
          break;
        case POPUPSTATE.Collection:
          this.collectionmanager.DrawCollectionManager(Vector2.Zero);
          break;
        case POPUPSTATE.RideTicket:
          this.rideticketmanager.DrawRideTicketManager();
          break;
        case POPUPSTATE.SaveAlert:
          this.savenotification.DrawSaveNotification();
          break;
        case POPUPSTATE.AnimalDelivery:
          this.animaldeliverypanelmanager.DrawAnimalDeliveryPanelManager();
          break;
        case POPUPSTATE.ViewAnimalFoodSupply:
          this.animalfoodsummary.DrawAnimalFoodSummary(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.PeopleInPark:
          this.peopleinparkmanager.DrawPeopleInParkManager();
          break;
        case POPUPSTATE.Vet_DiseaseResearch:
          this.diseasemanager.DrawDiseaseResearchManager(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.Vet_PenVisitSummary:
          this.penvistsummary.DrawPenVisitSummary(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.Vet_MedicalJounral:
          this.medicaljournalmanager.DrawMedicalJournalManager(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.QuarantinedAnimals:
          this.quaratinemanager.DrawQuarantinedAnimalsManager();
          break;
        case POPUPSTATE.QuarantineSettings:
          this.quarantineSettingsManager.DrawQuarantineSettingsManager();
          break;
        case POPUPSTATE.Incinerator:
          this.incineratorBuildingManager.DrawIncineratorBuildingManager();
          break;
        case POPUPSTATE.ManageProcessing:
          this.processingManager.DrawZ_ProcessingManager();
          break;
        case POPUPSTATE.Slaughterhouse_Culling:
          this.cullingSettingsManager.DrawCullingSettingManager();
          break;
        case POPUPSTATE.Crops:
          this.cropsummary.DrawCropSummary(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.Warehouse:
          this.warehouseManager.DrawWarehouseInfoManager();
          break;
        case POPUPSTATE.EventReport:
          this.eventreportmanager.DrawEventReportManager(AssetContainer.pointspritebatchTop05);
          break;
        case POPUPSTATE.CriticalChoice:
          this.criticalchoicemanager.DrawCriticalChoiceManager();
          break;
        case POPUPSTATE.FeatureUnlock:
          this.featureunlockmanager.DrawFeatureUnlockManager();
          break;
        case POPUPSTATE.Cleanliness:
          this.cleanlinessnotificationmanager.DrawCleanlinessNotificationManager();
          break;
        case POPUPSTATE.DestroyPen:
          this.destroypenpopup.DrawDestroyPenConfirmation();
          break;
        case POPUPSTATE.NewThing:
          this.newThingPanel.DrawNewThingPopUpManager();
          break;
        case POPUPSTATE.DonateAnimals:
          this.animaldonationmanager.DrawAnimalDonationPopUpManager();
          break;
      }
    }
  }
}
