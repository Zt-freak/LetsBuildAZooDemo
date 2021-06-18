// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.ManageEmployeePanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_ManageEmployees.EmployeeView;
using TinyZoo.Z_ManageEmployees.HiringSummary;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.People.Animal.TabFrame;

namespace TinyZoo.Z_ManageEmployees
{
  internal class ManageEmployeePanel
  {
    private BigBrownPanel bigBrownPanel;
    private Vector2 location;
    private EmployeePerformancePanel employeeViewPanel;
    private HiringSummaryPanel hiringSummaryPanel;
    private LerpHandler_Float lerper;
    private AnimalTabmanager tabManager;
    private EmployeeDisplayType refDisplayType;
    private EmployeeType refRoamingEmployee;
    private ShopEntry refShopEntry;
    private OpenPositions refOpenPositions;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private TabType currentTab;
    private HashSet<TabType> forceRefreshThisTabOnTabChange;

    public ManageEmployeePanel(
      ShopEntry shopEntry,
      EmployeeDisplayType displayType,
      OpenPositions currentOpenPositions,
      Player player,
      float _BaseScale,
      EmployeeType _RoamingEmployeeType = EmployeeType.None,
      bool HasPreviousButton = false)
    {
      this.refDisplayType = displayType;
      this.refRoamingEmployee = _RoamingEmployeeType;
      this.BaseScale = _BaseScale;
      this.refOpenPositions = currentOpenPositions;
      this.refShopEntry = shopEntry;
      this.currentTab = TabType.Count;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      double defaultYbuffer = (double) this.scaleHelper.GetDefaultYBuffer();
      double defaultXbuffer = (double) this.scaleHelper.GetDefaultXBuffer();
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Manage Employees", this.BaseScale, HasPreviousButton);
      float num = (float) AnimalTabmanager.PreferredWidthOfEachTab_Raw * this.BaseScale;
      TabType[] tabTypeArray = new TabType[2]
      {
        TabType.Employees_View,
        TabType.Employees_Hire
      };
      this.tabManager = new AnimalTabmanager(this.BaseScale, (float) tabTypeArray.Length * num, tabTypeArray);
      bool flag = this.refRoamingEmployee == EmployeeType.None ? player.employees.GetEmployeesInThisSpecificShop(this.refShopEntry.ShopUID).Count == 0 : player.employees.GetEmployeesOfThisType(this.refRoamingEmployee).Count == 0;
      this.forceRefreshThisTabOnTabChange = new HashSet<TabType>();
      this.OnClickTab(tabTypeArray[0], player);
      if (flag)
      {
        this.tabManager.ForceTabSwitch(1);
        this.OnClickTab(tabTypeArray[1], player);
      }
      this.SetNotification(player);
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
      this.location = new Vector2(512f, 300f);
    }

    public void LerpIn()
    {
      if (ManageEmployeeManager.DisableLerp)
        this.lerper.Value = 0.0f;
      else
        this.lerper.SetLerp(true, 1f, 0.0f, 3f);
    }

    public void LerpOff()
    {
      if (ManageEmployeeManager.DisableLerp)
        this.lerper.Value = 1f;
      else
        this.lerper.SetLerp(false, 0.0f, 1f, 3f);
    }

    public bool CheckMouseOver(Player player) => this.tabManager.CheckMouseOver(player, this.location) || this.bigBrownPanel.CheckMouseOver(player, this.location);

    public void ForceRefreshThisTabWhenItsActive(Player player, TabType thisTab)
    {
      int num = thisTab == this.currentTab ? 1 : 0;
      this.forceRefreshThisTabOnTabChange.Add(thisTab);
      if (num == 0)
        return;
      this.currentTab = TabType.Count;
      this.OnClickTab(thisTab, player);
    }

    private void SetNotification(Player player)
    {
      if (FeatureFlags.FlashHireStaffFromShop && this.refShopEntry != null && this.refShopEntry.GetEmplyeeCount() < ShopData.GetMaximumEmployeesForThisShop(this.refShopEntry.tiletype, player))
        this.tabManager.AddRedLight(OffscreenPointerType.JobApplicants, 1);
      if (FeatureFlags.FlashHireFromGateForQuest && this.refRoamingEmployee == EmployeeType.Janitor)
        this.tabManager.AddRedLight(OffscreenPointerType.JobApplicants, 1);
      if (this.refOpenPositions != null && this.refOpenPositions.GetNumberOfApplicants() > 0)
        this.tabManager.AddNotificationIcon(OffscreenPointerType.JobApplicants, 1);
      else
        this.tabManager.AddNotificationIcon(OffscreenPointerType.JobApplicants, 1, true);
    }

    private void OnClickTab(TabType tabType, Player player)
    {
      if (this.currentTab != tabType)
      {
        bool flag = this.forceRefreshThisTabOnTabChange.Contains(tabType);
        float num = this.location.Y + this.bigBrownPanel.GetFrameOffsetFromTop().Y;
        string _NewText = string.Empty;
        switch (tabType)
        {
          case TabType.Employees_View:
            if (this.employeeViewPanel == null | flag)
              this.employeeViewPanel = new EmployeePerformancePanel(this.refDisplayType, this.refShopEntry, player, this.BaseScale, this.refRoamingEmployee);
            this.bigBrownPanel.Finalize(this.employeeViewPanel.GetSize());
            this.tabManager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
            _NewText = "Manage Employees";
            break;
          case TabType.Employees_Hire:
            if (this.hiringSummaryPanel == null | flag)
            {
              float x = this.employeeViewPanel.GetSize().X;
              if (flag)
              {
                this.refOpenPositions = this.refRoamingEmployee != EmployeeType.None ? player.employees.openPositionsContainer.GetOpenPositionForThisEmployee(this.refRoamingEmployee) : player.employees.openPositionsContainer.GetOpenPositionForThisShop(this.refShopEntry.tiletype);
                this.SetNotification(player);
              }
              this.hiringSummaryPanel = new HiringSummaryPanel(this.refShopEntry, this.refOpenPositions, player, x, this.BaseScale, this.refRoamingEmployee);
            }
            this.bigBrownPanel.Finalize(this.hiringSummaryPanel.GetSize());
            this.tabManager.SetToTopLeftOfThisPanel(this.bigBrownPanel);
            _NewText = "Hire Employees";
            break;
        }
        this.bigBrownPanel.SetNewHeading(_NewText);
        this.location.Y = num;
        this.location.Y -= this.bigBrownPanel.GetFrameOffsetFromTop().Y;
        if (flag)
          this.forceRefreshThisTabOnTabChange.Remove(tabType);
      }
      this.currentTab = tabType;
    }

    public bool UpdateManageEmployeePanel(
      Player player,
      float DeltaTime,
      Vector2 offset,
      out Employee ViewThisEmployeeInfo,
      out RecruitmentButtonType recruitmentButtonSelected,
      out bool PreviousClicked)
    {
      offset += this.location;
      Vector2 localposition = offset;
      this.bigBrownPanel.UpdateDragger(player, ref localposition, DeltaTime);
      this.location = localposition - offset + this.location;
      this.lerper.UpdateLerpHandler(DeltaTime);
      ViewThisEmployeeInfo = (Employee) null;
      recruitmentButtonSelected = RecruitmentButtonType.Count;
      bool flag = false;
      PreviousClicked = false;
      if ((double) this.lerper.Value == 0.0)
      {
        TabType tabType = this.tabManager.UpdateAnimalTabmanager(offset, player, DeltaTime);
        if (tabType != TabType.Count)
          this.OnClickTab(tabType, player);
        if (this.currentTab == TabType.Employees_View)
        {
          if (this.employeeViewPanel.UpdateEmployeePerformancePanel(player, DeltaTime, offset, out ViewThisEmployeeInfo))
            return true;
        }
        else if (this.currentTab == TabType.Employees_Hire)
        {
          recruitmentButtonSelected = this.hiringSummaryPanel.UpdateHiringSummaryPanel(player, DeltaTime, offset);
          if (recruitmentButtonSelected != RecruitmentButtonType.Count)
            return true;
        }
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, offset))
          flag = true;
        if (this.bigBrownPanel.UpdatePanelpreviousButton(player, DeltaTime, offset))
        {
          PreviousClicked = true;
          return true;
        }
      }
      return flag;
    }

    public void DrawManageEmployeePanel(Vector2 offset, SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      offset += this.location;
      offset.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      this.tabManager.PreDrawAnimalTabmanager(offset, spriteBatch);
      this.bigBrownPanel.DrawBigBrownPanel(offset);
      if (this.currentTab == TabType.Employees_View)
        this.employeeViewPanel.DrawEmployeePerformancePanel(offset, spriteBatch);
      else if (this.currentTab == TabType.Employees_Hire)
        this.hiringSummaryPanel.DrawHiringSummaryPanel(offset, spriteBatch);
      this.tabManager.DrawAnimalTabmanager(offset, spriteBatch);
    }
  }
}
