// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.ManageEmployeeManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment;
using TinyZoo.Z_ManageEmployees.HiringSummary;
using TinyZoo.Z_Notification;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_ManageEmployees
{
  internal class ManageEmployeeManager
  {
    private ManageEmployeePanel manageEmployeePanel;
    private JobPostingPanel jobPostingPanel;
    private ApplicantViewPanel applicantViewPanel;
    internal static float LerpDistance = 768f;
    private EmployeeDisplayType displayType;
    private ShopEntry shopEntry;
    private float BaseScale;
    private OpenPositions currentOpenPositions;
    internal static bool DisableLerp = true;
    private bool HasPreviousButton;
    public bool ClickedPreviousButton;
    private POPUPSTATE cameFromThisState;
    private bool RemakeHiringTabLater;
    private int lastApplicantCount;

    public EmployeeType RoamingEmployeeType { get; private set; }

    public ManageEmployeeManager(
      Vector2Int location,
      TILETYPE tileType,
      Player player,
      EmployeeType _RoamingEmployeeType = EmployeeType.None,
      POPUPSTATE _cameFromThisState = POPUPSTATE.None)
    {
      if (TileData.IsAStoreRoom(tileType))
        _RoamingEmployeeType = EmployeeType.Keeper;
      this.RoamingEmployeeType = _RoamingEmployeeType;
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      this.cameFromThisState = _cameFromThisState;
      this.HasPreviousButton = (uint) this.cameFromThisState > 0U;
      if (this.RoamingEmployeeType == EmployeeType.None)
      {
        this.displayType = ManageEmployeeDisplayData.GetTileTypeToEmployeeDisplayType(tileType, location, player, out this.shopEntry);
        this.currentOpenPositions = player.employees.openPositionsContainer.GetOpenPositionForThisShop(this.shopEntry.tiletype);
      }
      else
      {
        this.displayType = EmployeeDisplayType.Facility;
        this.currentOpenPositions = player.employees.openPositionsContainer.GetOpenPositionForThisEmployee(this.RoamingEmployeeType);
      }
      this.ConstructMainSummaryPanel(player);
      this.CheckApplicantCounter();
    }

    private void ConstructMainSummaryPanel(Player player) => this.manageEmployeePanel = new ManageEmployeePanel(this.shopEntry, this.displayType, this.currentOpenPositions, player, this.BaseScale, this.RoamingEmployeeType, this.HasPreviousButton);

    public bool CheckMouseOver(Player player)
    {
      bool flag = false;
      if (this.applicantViewPanel != null)
        flag |= this.applicantViewPanel.CheckMouseOver(player);
      if (this.manageEmployeePanel != null)
        flag |= this.manageEmployeePanel.CheckMouseOver(player);
      if (this.jobPostingPanel != null)
        flag |= this.jobPostingPanel.CheckMouseOver(player);
      return flag;
    }

    public bool CheckApplicantCounter()
    {
      bool flag = false;
      if (this.currentOpenPositions != null)
      {
        int numberOfApplicants = this.currentOpenPositions.GetNumberOfApplicants();
        if (numberOfApplicants != this.lastApplicantCount)
          flag = true;
        this.lastApplicantCount = numberOfApplicants;
      }
      return flag;
    }

    public bool UpdateManageEmployeeManager(Player player, float DeltaTime)
    {
      bool ExitCompletely = false;
      if (this.CheckApplicantCounter())
      {
        if (this.applicantViewPanel != null)
          this.applicantViewPanel.OnApplicantListChangedFromOutside();
        if (this.manageEmployeePanel != null)
          this.manageEmployeePanel.ForceRefreshThisTabWhenItsActive(player, TabType.Employees_Hire);
      }
      if (this.applicantViewPanel != null)
      {
        Applicant selectedThisApplicant;
        bool somethingChanged_prolonged;
        bool SomeoneWasJustDismissedOrHired;
        if (this.applicantViewPanel.UpdateApplicantViewPanel(player, DeltaTime, out selectedThisApplicant, out somethingChanged_prolonged, out ExitCompletely, out SomeoneWasJustDismissedOrHired) && selectedThisApplicant == null)
        {
          this.applicantViewPanel.LerpOff();
          if (!ExitCompletely)
          {
            this.manageEmployeePanel.LerpIn();
            if (somethingChanged_prolonged)
            {
              this.manageEmployeePanel.ForceRefreshThisTabWhenItsActive(player, TabType.Employees_Hire);
              this.manageEmployeePanel.ForceRefreshThisTabWhenItsActive(player, TabType.Employees_View);
            }
          }
        }
        if (SomeoneWasJustDismissedOrHired)
          this.CheckApplicantCounter();
      }
      bool ApplyChanges;
      if (this.jobPostingPanel != null && this.jobPostingPanel.UpdateJobPostingPanel(player, DeltaTime, out bool _, out ExitCompletely, out ApplyChanges))
      {
        if (ApplyChanges)
        {
          OpenPositions tempOpenPositions = this.jobPostingPanel.GetTempOpenPositions();
          if (player.employees.openPositionsContainer.GetOpenPositionForThis(this.currentOpenPositions.tileType, this.currentOpenPositions.RoamingEmployeeType) == null)
            player.employees.openPositionsContainer.AddNewOpenPosition(this.currentOpenPositions);
          this.currentOpenPositions.ApplyAndCommitChanges(tempOpenPositions, player);
          this.RemakeHiringTabLater = true;
          Z_NotificationManager.RescrubJobApplicants = true;
          this.CheckApplicantCounter();
        }
        else
        {
          this.jobPostingPanel.LerpOff();
          if (!ExitCompletely)
          {
            this.manageEmployeePanel.LerpIn();
            if (this.RemakeHiringTabLater)
            {
              this.manageEmployeePanel.ForceRefreshThisTabWhenItsActive(player, TabType.Employees_Hire);
              this.RemakeHiringTabLater = false;
            }
          }
        }
      }
      Employee ViewThisEmployeeInfo;
      RecruitmentButtonType recruitmentButtonSelected;
      if (this.manageEmployeePanel.UpdateManageEmployeePanel(player, DeltaTime, Vector2.Zero, out ViewThisEmployeeInfo, out recruitmentButtonSelected, out this.ClickedPreviousButton))
      {
        switch (recruitmentButtonSelected)
        {
          case RecruitmentButtonType.ViewJobPostings:
            if (this.currentOpenPositions == null)
            {
              TILETYPE _tileType = TILETYPE.None;
              if (this.shopEntry != null)
              {
                int shopUid = this.shopEntry.ShopUID;
                _tileType = this.shopEntry.tiletype;
              }
              this.currentOpenPositions = new OpenPositions(_tileType, this.RoamingEmployeeType);
            }
            this.jobPostingPanel = new JobPostingPanel(this.shopEntry, new OpenPositions(this.currentOpenPositions), this.currentOpenPositions, player, this.BaseScale, this.RoamingEmployeeType);
            this.jobPostingPanel.location = new Vector2(512f, 384f);
            this.manageEmployeePanel.LerpOff();
            break;
          case RecruitmentButtonType.ViewApplicantsForJobPostings:
            this.applicantViewPanel = new ApplicantViewPanel(this.shopEntry, this.currentOpenPositions, player, this.BaseScale, _RoamingEmployeeType: this.RoamingEmployeeType);
            this.applicantViewPanel.location = new Vector2(512f, 384f);
            this.manageEmployeePanel.LerpOff();
            break;
          case RecruitmentButtonType.HireFromAgency:
            this.applicantViewPanel = new ApplicantViewPanel(this.shopEntry, this.currentOpenPositions, player, this.BaseScale, true, this.RoamingEmployeeType);
            this.applicantViewPanel.location = new Vector2(512f, 384f);
            this.manageEmployeePanel.LerpOff();
            break;
          default:
            if (ViewThisEmployeeInfo != null)
            {
              OverWorldManager.zoopopupHolder.CreateZooPopUps(CustomerManager.GetThisEmployee(ViewThisEmployeeInfo), player);
              break;
            }
            if (!this.ClickedPreviousButton)
            {
              ExitCompletely = true;
              break;
            }
            break;
        }
      }
      return ExitCompletely;
    }

    public void DrawManageEmployeeManager()
    {
      this.manageEmployeePanel.DrawManageEmployeePanel(Vector2.Zero, AssetContainer.pointspritebatchTop05);
      if (this.jobPostingPanel != null)
        this.jobPostingPanel.DrawJobPostingPanel(AssetContainer.pointspritebatchTop05);
      if (this.applicantViewPanel == null)
        return;
      this.applicantViewPanel.DrawApplicantViewPanel(AssetContainer.pointspritebatchTop05);
    }
  }
}
