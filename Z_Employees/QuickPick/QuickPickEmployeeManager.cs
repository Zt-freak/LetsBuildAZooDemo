// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Employees.QuickPick.QuickPickEmployeeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Employees.JobApplicants;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_Employees.Emp_Summary;
using TinyZoo.Z_HUD.Z_HeroQuests_Pins;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;

namespace TinyZoo.Z_Employees.QuickPick
{
  internal class QuickPickEmployeeManager
  {
    private QuickEmployeeDescription newemployee;
    private AutomaticJobOfferPanel jobOfferPanel;
    private TILETYPE BuildingToUse;
    private OpenPositions cameFromOpenPosition;

    public Applicant refApplicant { get; private set; }

    public bool Exiting { get; private set; }

    public bool WasCancelled { get; private set; }

    public QuickPickEmployeeManager(TILETYPE _BuildingToUse, int ShopUID, Player player)
    {
      this.BuildingToUse = _BuildingToUse;
      this.newemployee = new QuickEmployeeDescription(this.BuildingToUse, ShopUID);
      ApplicantGenerator.SetQuickPickEmployee(this.newemployee, player);
      this.SetUpPanel(player);
    }

    public QuickPickEmployeeManager(
      Applicant _applicant,
      OpenPositions _cameFromOpenPosition,
      QuickEmployeeDescription _quickEmployeeDescription,
      Player player)
    {
      this.BuildingToUse = _quickEmployeeDescription.WorksHere;
      this.refApplicant = _applicant;
      this.cameFromOpenPosition = _cameFromOpenPosition;
      this.newemployee = _quickEmployeeDescription;
      this.SetUpPanel(player, true);
      this.jobOfferPanel.location = new Vector2(0.0f, 0.0f);
    }

    private void SetUpPanel(Player player, bool AllowClose = false)
    {
      bool IsAgencyHire = this.refApplicant != null && this.refApplicant.HiredThroughAgency;
      this.jobOfferPanel = new AutomaticJobOfferPanel(this.newemployee, Z_GameFlags.GetBaseScaleForUI(), player, AllowClose, IsAgencyHire);
      this.jobOfferPanel.location = new Vector2(512f, 384f);
    }

    public bool CheckMouseOver(Player player, Vector2 offset) => this.jobOfferPanel.CheckMouseOver(player, offset);

    public bool UpdateQuickPickEmployeeManager(Player player, float DeltaTime) => this.UpdateQuickPickEmployeeManager(player, DeltaTime, Vector2.Zero);

    public bool UpdateQuickPickEmployeeManager(Player player, float DeltaTime, Vector2 offset)
    {
      bool isCancel;
      if (this.jobOfferPanel.UpdateAutomaticJobOfferPanel(DeltaTime, player, offset, out isCancel))
      {
        if (isCancel)
        {
          this.WasCancelled = true;
        }
        else
        {
          ShopEntry shopEntry = (ShopEntry) null;
          EmployeeType employeetype;
          EmployeeData.IsThisAnEmployee(this.newemployee.thisemployee, out employeetype);
          if (TileData.IsABreedingRoom(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisFacility(this.newemployee.ShopUID);
          else if (TileData.IsACRISPRBuilding(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisFacility(this.newemployee.ShopUID);
          else if (TileData.IsAStoreRoom(this.BuildingToUse))
            player.employees.AddThisEmplyee((IntakePerson) null, employeetype, this.jobOfferPanel.GetSalarySet(), -1, player, this.newemployee);
          else if (TileData.IsThisAFacility(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisFacility(this.newemployee.ShopUID);
          else if (TileData.IsASlaughterhouse(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisFacility(this.newemployee.ShopUID);
          else if (TileData.IsAnArchitectOffice(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisArchitectsOffice(this.newemployee.ShopUID);
          else if (TileData.IsAFactory(this.BuildingToUse))
            shopEntry = player.shopstatus.GetThisFacility(this.newemployee.ShopUID);
          else if (this.BuildingToUse == TILETYPE.Logo)
            player.employees.AddThisEmplyee((IntakePerson) null, employeetype, this.jobOfferPanel.GetSalarySet(), Game1.Rnd.Next(10, 100), player, this.newemployee);
          else
            shopEntry = player.shopstatus.GetThisShop(this.newemployee.ShopUID);
          int ShopUID = -1;
          if (shopEntry != null)
          {
            Employee employee = player.employees.AddThisEmplyee((IntakePerson) null, employeetype, this.jobOfferPanel.GetSalarySet(), -1, player, this.newemployee);
            shopEntry.AddEmployee(player.employees.employees[player.employees.employees.Count - 1]);
            ShopUID = shopEntry.ShopUID;
            employee.quickemplyeedescription.ShopUID = ShopUID;
          }
          Z_QuestPinManager.DoubleCheckTaskNotifications = true;
          CustomerManager.AddPerson(this.newemployee.thisemployee, player.employees.employees[player.employees.employees.Count - 1], player.prisonlayout.cellblockcontainer, player, ShopUID);
          if (this.cameFromOpenPosition != null)
            this.cameFromOpenPosition.RemoveAndHireApplicant(this.refApplicant);
          if (this.refApplicant != null && this.refApplicant.HiredThroughAgency)
            player.Stats.SpendCash(HiringAgency.GetAgencyFeeToHireThisPerson(this.refApplicant.GetEmployeeType()), SpendingCashOnThis.HiringCampaign, player);
          if ((double) player.livestats.LastCalculatedFacilities >= 0.0)
          {
            float facilities = FacilitiesCalulator.CalculateFacilities(player);
            NotificationBubbleManager.QuickAddNotification(player.livestats.LastCalculatedFacilities, facilities, BubbleMainType.Facilities);
            player.livestats.LastCalculatedFacilities = -1f;
          }
        }
        this.jobOfferPanel.LerpOff();
        this.Exiting = true;
      }
      return this.Exiting && this.jobOfferPanel.IsOffScreen();
    }

    public void DrawQuickPickEmployeeManager(Vector2 offset, SpriteBatch spriteBatch) => this.jobOfferPanel.DrawAutomaticJobOfferPanel(offset, spriteBatch);

    public void DrawQuickPickEmployeeManager() => this.DrawQuickPickEmployeeManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
  }
}
