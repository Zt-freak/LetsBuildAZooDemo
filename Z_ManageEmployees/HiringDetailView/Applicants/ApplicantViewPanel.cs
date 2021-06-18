// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ApplicantViewPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_BalanceSystems.Employees.JobApplicants;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants.ExtraPopUps;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Applicants
{
  internal class ApplicantViewPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private LerpHandler_Float lerper;
    private ApplicantsTable applicantTable;
    private SimpleTextHandler InfoPara;
    private ZGenericText applicantDisplayedCountText;
    private string applicantDisplayedBaseString;
    private ZGenericText noMoreApplicantText;
    private string noMoreApplicantsString;
    private TextButton DismissAllButton;
    public bool isAgencyInstantHire;
    private OpenPositions refcurrentOpenPositions;
    private ShopEntry refShopEntry;
    private float refBaseScale;
    private float refYBuffer;
    private bool SomethingChanged;
    private EmployeeType RoamingEmployeeType;
    private NoVacanciesPopup noVacanciesPopup;
    private ConfirmationDialog closePostionPopup;
    private QuickPickEmployeeManager quickPickManager;
    private Applicant lastApplicantSelected;
    private bool PlayerDismissedClosePositionPopUpOnce;
    private bool TryToRePull;
    private LerpHandler_Float dynamicScaling_lerper;
    private float dynamicExtraHeight;
    private float heightOfOneRow;
    private int originalNumberOfRows;
    private Vector2 contentsSize;
    private float accumulatedExtraHeight;

    public ApplicantViewPanel(
      ShopEntry shopEntry,
      OpenPositions _currentOpenPositions,
      Player player,
      float BaseScale,
      bool _isAgencyInstantHire = false,
      EmployeeType _RoamingEmployeeType = EmployeeType.None)
    {
      this.RoamingEmployeeType = _RoamingEmployeeType;
      this.isAgencyInstantHire = _isAgencyInstantHire;
      this.refcurrentOpenPositions = _currentOpenPositions;
      this.refShopEntry = shopEntry;
      this.refBaseScale = BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      float defaultYbuffer = uiScaleHelper.GetDefaultYBuffer();
      this.refYBuffer = defaultYbuffer;
      float num1 = 0.0f;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Applicants", BaseScale, true);
      float[] widths = new float[6]
      {
        uiScaleHelper.ScaleX(110f),
        uiScaleHelper.ScaleX(100f),
        uiScaleHelper.ScaleX(100f),
        uiScaleHelper.ScaleX(80f),
        uiScaleHelper.ScaleX(45f),
        uiScaleHelper.ScaleX(45f)
      };
      float num2 = 0.0f;
      for (int index = 0; index < widths.Length; ++index)
        num2 += widths[index];
      int currentEmployeeCount;
      int maxEmployeeCount;
      JobApplicants_Calculator.IsShopAtMaxCapacity(shopEntry, _RoamingEmployeeType, player, out currentEmployeeCount, out maxEmployeeCount);
      string TextToWrite = string.Empty;
      List<Applicant> listOfApplicants;
      if (this.isAgencyInstantHire)
      {
        listOfApplicants = new List<Applicant>();
        for (int index = 0; index < ApplicantGenerator.MaxApplicantsForDisplay; ++index)
        {
          if (this.RoamingEmployeeType != EmployeeType.None)
            listOfApplicants.Add(ApplicantGenerator.CreateNewApplicant(TILETYPE.Count, this.RoamingEmployeeType, this.isAgencyInstantHire, player));
          else
            listOfApplicants.Add(ApplicantGenerator.CreateNewApplicant(shopEntry.tiletype, EmployeeType.None, this.isAgencyInstantHire, player));
        }
        TextToWrite = "Agency Fee: $" + (object) HiringAgency.GetAgencyFeeToHireThisPerson(listOfApplicants[0].GetEmployeeType());
      }
      else
      {
        listOfApplicants = this.refcurrentOpenPositions.GetAndPopulateApplicantsForDisplay(player);
        if (JobApplicants_Calculator.UseSingleOpenPositions)
        {
          if (maxEmployeeCount != -1)
            TextToWrite = TextToWrite + "~Available Vacancies: " + (object) (maxEmployeeCount - currentEmployeeCount);
        }
        else
          TextToWrite = "Job Positions Opened: " + (object) this.refcurrentOpenPositions.NumberOfPositionsOpened + "~After hiring a new employee, the job position will be closed.";
      }
      this.InfoPara = new SimpleTextHandler(TextToWrite, num2, true, BaseScale, this.isAgencyInstantHire, true);
      this.InfoPara.AutoCompleteParagraph();
      this.InfoPara.SetAllColours(ColourData.Z_Cream);
      this.InfoPara.Location.Y = num1;
      float num3 = num1 + this.InfoPara.GetHeightOfParagraph();
      if (!this.isAgencyInstantHire)
      {
        int numberOfApplicants = this.refcurrentOpenPositions.GetNumberOfApplicants();
        this.applicantDisplayedBaseString = "Displaying: ";
        this.applicantDisplayedCountText = new ZGenericText(this.applicantDisplayedBaseString + (object) listOfApplicants.Count + "/" + (object) numberOfApplicants, BaseScale, false, true);
        this.applicantDisplayedCountText.vLocation.Y = num3;
        num3 = num3 + this.applicantDisplayedCountText.GetSize().Y + defaultYbuffer * 0.5f;
      }
      this.applicantTable = new ApplicantsTable(listOfApplicants, BaseScale, defaultYbuffer, ref widths, this.isAgencyInstantHire);
      this.applicantTable.location.Y = num3;
      float num4 = num3 + this.applicantTable.GetSize().Y;
      this.heightOfOneRow = this.applicantTable.GetSizeOfOneRow().Y;
      this.originalNumberOfRows = this.applicantTable.GetNumberOfEntries();
      float num5 = num4 + this.refYBuffer;
      this.noMoreApplicantsString = "No more applicants.";
      this.noMoreApplicantText = new ZGenericText(this.noMoreApplicantsString, this.refBaseScale);
      Vector2 size = this.noMoreApplicantText.GetSize();
      this.noMoreApplicantText.vLocation.Y = num5 + size.Y * 0.5f;
      float num6 = num5 + size.Y + defaultYbuffer;
      this.DismissAllButton = new TextButton(BaseScale, "Dismiss All", 70f, _OverrideFrameScale: (BaseScale * 2f));
      this.DismissAllButton.SetButtonColour(BTNColour.Red);
      Vector2 sizeTrue = this.DismissAllButton.GetSize_True();
      this.DismissAllButton.vLocation.X -= sizeTrue.X * 0.5f;
      this.DismissAllButton.vLocation.Y = num6 + sizeTrue.Y * 0.5f;
      float y = num6 + sizeTrue.Y;
      this.contentsSize = new Vector2(num2, y);
      this.bigBrownPanel.Finalize(this.contentsSize);
      Vector2 frameOffsetFromTop = this.bigBrownPanel.GetFrameOffsetFromTop();
      if (this.applicantDisplayedCountText != null)
      {
        this.applicantDisplayedCountText.vLocation.Y += frameOffsetFromTop.Y;
        this.applicantDisplayedCountText.vLocation.X += num2 * 0.5f;
      }
      this.applicantTable.location.Y += frameOffsetFromTop.Y;
      if (this.InfoPara != null)
        this.InfoPara.Location.Y += frameOffsetFromTop.Y;
      if (this.noMoreApplicantText != null)
        this.noMoreApplicantText.vLocation.Y += frameOffsetFromTop.Y;
      this.DismissAllButton.vLocation.Y += frameOffsetFromTop.Y;
      this.DismissAllButton.vLocation.X += num2 * 0.5f;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
      this.dynamicScaling_lerper = new LerpHandler_Float();
      this.RefreshTextAndButtonStates();
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

    private void DismissApplicant(
      List<Applicant> applicantsDismissed,
      Player player,
      bool WasHired = false)
    {
      List<Applicant> addThisApplicantToo = new List<Applicant>();
      if (this.isAgencyInstantHire)
      {
        TILETYPE tileType = TILETYPE.Count;
        if (this.refShopEntry != null)
          tileType = this.refShopEntry.tiletype;
        for (int index = 0; index < applicantsDismissed.Count; ++index)
          addThisApplicantToo.Add(ApplicantGenerator.CreateNewApplicant(tileType, this.RoamingEmployeeType, true, player));
      }
      else
      {
        int count = applicantsDismissed.Count;
        this.refcurrentOpenPositions.GetNumberOfApplicants();
        int numberOfEntries = this.applicantTable.GetNumberOfEntries();
        if (this.refcurrentOpenPositions.GetNumberOfApplicants() > numberOfEntries && count > 0)
        {
          List<Applicant> applicantsForDisplay = this.refcurrentOpenPositions.GetAndPopulateApplicantsForDisplay(player);
          for (int index = numberOfEntries; index < applicantsForDisplay.Count && count != 0; ++index)
          {
            addThisApplicantToo.Add(applicantsForDisplay[index]);
            --count;
          }
        }
      }
      this.applicantTable.DismissApplicants(applicantsDismissed, addThisApplicantToo);
      if (!this.isAgencyInstantHire && !WasHired)
      {
        for (int index = 0; index < applicantsDismissed.Count; ++index)
          this.refcurrentOpenPositions.RemoveApplicant(applicantsDismissed[index]);
      }
      this.SomethingChanged = true;
      int num = this.isAgencyInstantHire ? 1 : 0;
      this.RefreshTextAndButtonStates();
    }

    public void OnApplicantListChangedFromOutside()
    {
      if (this.isAgencyInstantHire)
        return;
      this.TryToRePull = true;
    }

    public void RepullAndAddNewApplicantsIfApplicable(Player player)
    {
      if (!this.applicantTable.AreLerpsAllDone() || this.applicantTable.IsBusy())
        return;
      int numberOfEntries = this.applicantTable.GetNumberOfEntries();
      int numberOfApplicants = this.refcurrentOpenPositions.GetNumberOfApplicants();
      int numberOfRowsAdded = 0;
      if (numberOfEntries < ApplicantGenerator.MaxApplicantsForDisplay)
      {
        int num = Math.Min(ApplicantGenerator.MaxApplicantsForDisplay - numberOfEntries, numberOfApplicants - numberOfEntries);
        if (numberOfApplicants > numberOfEntries)
        {
          List<Applicant> applicantsForDisplay = this.refcurrentOpenPositions.GetAndPopulateApplicantsForDisplay(player);
          List<Applicant> addThisApplicant = new List<Applicant>();
          for (int index = numberOfEntries; index < applicantsForDisplay.Count && num != 0; ++index)
          {
            addThisApplicant.Add(applicantsForDisplay[index]);
            --num;
          }
          numberOfRowsAdded = addThisApplicant.Count;
          this.applicantTable.AddApplicants(addThisApplicant);
        }
      }
      this.TryToRePull = false;
      this.OnNewEntryAdded_ScalePanel(numberOfRowsAdded);
      this.RefreshTextAndButtonStates();
    }

    private void RefreshTextAndButtonStates()
    {
      int numberOfEntries = this.applicantTable.GetNumberOfEntries();
      int applicantsForDisplay = ApplicantGenerator.MaxApplicantsForDisplay;
      if (!this.isAgencyInstantHire)
        this.refcurrentOpenPositions.GetNumberOfApplicants();
      if (this.applicantDisplayedCountText != null)
        this.applicantDisplayedCountText.textToWrite = this.applicantDisplayedBaseString + (object) numberOfEntries + "/" + (object) applicantsForDisplay;
      if (numberOfEntries > 0)
      {
        this.DismissAllButton.SetButtonColour(BTNColour.Red);
        this.DismissAllButton.Locked = false;
      }
      else
      {
        this.DismissAllButton.SetButtonColour(BTNColour.Grey);
        this.DismissAllButton.Locked = true;
      }
      if (this.isAgencyInstantHire)
        this.noMoreApplicantText.textToWrite = "";
      else if (applicantsForDisplay == numberOfEntries)
        this.noMoreApplicantText.textToWrite = this.noMoreApplicantsString;
      else
        this.noMoreApplicantText.textToWrite = "";
    }

    public bool CheckMouseOver(Player player)
    {
      Vector2 location = this.location;
      return this.quickPickManager != null && this.quickPickManager.CheckMouseOver(player, location) || this.noVacanciesPopup != null && this.noVacanciesPopup.CheckMouseOver(player, location) || this.closePostionPopup != null && this.closePostionPopup.CheckMouseOver(player, location) || this.bigBrownPanel.CheckMouseOver(player, this.location);
    }

    public void SetActive(bool IsActive) => this.bigBrownPanel.Active = IsActive;

    public bool CheckAndCreateClosePositionPrompt()
    {
      if (!this.isAgencyInstantHire && this.refcurrentOpenPositions != null && (this.refcurrentOpenPositions.NumberOfPositionsOpened > 0 && !this.PlayerDismissedClosePositionPopUpOnce))
      {
        this.closePostionPopup = new ConfirmationDialog("Close Position?", "Would you like to close this position? Applicants will no longer be available.~(You can manually close the position any time later.)", this.refBaseScale, customTextX_raw: 300f);
        this.SetActive(false);
        return true;
      }
      this.SetActive(true);
      return false;
    }

    public void CreateNoVacanciesPopup()
    {
      this.noVacanciesPopup = new NoVacanciesPopup(this.refBaseScale);
      this.SetActive(false);
    }

    public void CreateQuickPick(Applicant applicantSelected, Player player)
    {
      this.lastApplicantSelected = applicantSelected;
      QuickEmployeeDescription _quickEmployeeDescription = applicantSelected.ConstructQuickEmployeeDescriptionForHiring(this.refShopEntry, this.RoamingEmployeeType, player);
      OpenPositions _cameFromOpenPosition = (OpenPositions) null;
      if (!this.isAgencyInstantHire)
        _cameFromOpenPosition = this.refcurrentOpenPositions;
      this.quickPickManager = new QuickPickEmployeeManager(applicantSelected, _cameFromOpenPosition, _quickEmployeeDescription, player);
      this.SetActive(false);
    }

    public void OnHiredApplicantAndReturnedBackToApplicantView(Player player) => this.DismissApplicant(new List<Applicant>()
    {
      this.lastApplicantSelected
    }, player, true);

    private void Sub_UpdateDynamicScalingPanel(float DeltaTime)
    {
      this.dynamicScaling_lerper.UpdateLerpHandler(DeltaTime);
      double num = (double) this.dynamicScaling_lerper.Value;
    }

    private void OnNewEntryAdded_ScalePanel(int numberOfRowsAdded)
    {
      if (numberOfRowsAdded <= 0 || this.applicantTable.GetNumberOfEntries() <= this.originalNumberOfRows)
        return;
      this.accumulatedExtraHeight += this.dynamicExtraHeight;
      this.dynamicExtraHeight = (float) numberOfRowsAdded * (this.heightOfOneRow + this.refYBuffer);
      this.dynamicScaling_lerper.SetLerp(true, 0.0f, 1f, 3f);
      this.contentsSize.Y += this.dynamicExtraHeight;
      this.bigBrownPanel.Finalize(this.contentsSize);
    }

    public bool UpdateApplicantViewPanel(
      Player player,
      float DeltaTime,
      out Applicant selectedThisApplicant,
      out bool somethingChanged_prolonged,
      out bool ExitCompletely,
      out bool SomeoneWasJustDismissedOrHired)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.Sub_UpdateDynamicScalingPanel(DeltaTime);
      Vector2 location = this.location;
      selectedThisApplicant = (Applicant) null;
      somethingChanged_prolonged = this.SomethingChanged;
      ExitCompletely = false;
      SomeoneWasJustDismissedOrHired = false;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if (this.noVacanciesPopup != null && this.noVacanciesPopup.UpdateNoVacanciesPopup(player, DeltaTime, location))
      {
        this.noVacanciesPopup = (NoVacanciesPopup) null;
        this.SetActive(true);
      }
      bool confirmed;
      if (this.closePostionPopup != null && this.closePostionPopup.UpdateConfirmationDialog(player, location, DeltaTime, out confirmed))
      {
        this.SomethingChanged = true;
        this.closePostionPopup = (ConfirmationDialog) null;
        this.SetActive(true);
        if (confirmed)
        {
          this.refcurrentOpenPositions.NumberOfPositionsOpened = 0;
          this.refcurrentOpenPositions.ApplyAndCommitChanges(this.refcurrentOpenPositions, player);
          Z_NotificationManager.RescrubJobApplicants = true;
          return true;
        }
        this.PlayerDismissedClosePositionPopUpOnce = true;
        this.OnHiredApplicantAndReturnedBackToApplicantView(player);
      }
      if (this.quickPickManager != null && this.quickPickManager.UpdateQuickPickEmployeeManager(player, DeltaTime, location))
      {
        this.SetActive(true);
        if (!this.quickPickManager.WasCancelled)
        {
          if (!this.CheckAndCreateClosePositionPrompt())
            this.OnHiredApplicantAndReturnedBackToApplicantView(player);
          SomeoneWasJustDismissedOrHired = true;
        }
        this.quickPickManager = (QuickPickEmployeeManager) null;
      }
      if ((double) this.lerper.Value == 0.0 && this.bigBrownPanel.Active)
      {
        if (this.TryToRePull)
          this.RepullAndAddNewApplicantsIfApplicable(player);
        Vector2 vector2 = new Vector2(0.0f, (float) ((double) this.dynamicScaling_lerper.Value * (double) this.dynamicExtraHeight * 0.5));
        vector2.Y += this.accumulatedExtraHeight * 0.5f;
        if (this.bigBrownPanel.UpdatePanelpreviousButton(player, DeltaTime, location))
          return true;
        if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
        {
          ExitCompletely = true;
          return true;
        }
        bool isDismiss;
        Applicant applicant = this.applicantTable.UpdateApplicantsTable(player, DeltaTime, location - vector2, out isDismiss);
        if (applicant != null)
        {
          if (isDismiss)
          {
            this.DismissApplicant(new List<Applicant>()
            {
              applicant
            }, player);
            SomeoneWasJustDismissedOrHired = true;
            this.SomethingChanged = true;
          }
          else if (JobApplicants_Calculator.IsShopAtMaxCapacity(this.refShopEntry, this.RoamingEmployeeType, player, out int _, out int _))
          {
            this.CreateNoVacanciesPopup();
          }
          else
          {
            selectedThisApplicant = applicant;
            this.SomethingChanged = true;
            this.CreateQuickPick(selectedThisApplicant, player);
            return true;
          }
        }
        if (!this.DismissAllButton.Locked && this.DismissAllButton.UpdateTextButton(player, location + vector2, DeltaTime) && !this.applicantTable.IsBusy())
        {
          this.DismissApplicant(this.applicantTable.GetAllApplicantsDisplayed(), player);
          SomeoneWasJustDismissedOrHired = true;
        }
      }
      return false;
    }

    public void DrawApplicantViewPanel(SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      Vector2 location = this.location;
      location.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      Vector2 vector2 = new Vector2(0.0f, (float) ((double) this.dynamicScaling_lerper.Value * (double) this.dynamicExtraHeight * 0.5));
      vector2.Y += this.accumulatedExtraHeight * 0.5f;
      this.bigBrownPanel.DrawBigBrownPanel(location, spriteBatch);
      if (this.applicantDisplayedCountText != null)
        this.applicantDisplayedCountText.DrawZGenericText(location - vector2, spriteBatch);
      this.applicantTable.DrawApplicantsTable(location - vector2, spriteBatch);
      if (this.InfoPara != null)
        this.InfoPara.DrawSimpleTextHandler(location - vector2, 1f, spriteBatch);
      if (this.noMoreApplicantText != null)
        this.noMoreApplicantText.DrawZGenericText(location + vector2, spriteBatch);
      this.DismissAllButton.DrawTextButton(location + vector2, 1f, spriteBatch);
      this.bigBrownPanel.DrawDarkOverlay(location, spriteBatch);
      if (this.noVacanciesPopup != null)
        this.noVacanciesPopup.DrawNoVacanciesPopup(location, spriteBatch);
      if (this.closePostionPopup != null)
        this.closePostionPopup.DrawConfirmationDialog(spriteBatch, location);
      if (this.quickPickManager == null)
        return;
      this.quickPickManager.DrawQuickPickEmployeeManager(location, spriteBatch);
    }
  }
}
