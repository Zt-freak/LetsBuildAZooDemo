// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.JobPostingPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment
{
  internal class JobPostingPanel
  {
    public Vector2 location;
    private BigBrownPanel bigBrownPanel;
    private LerpHandler_Float lerper;
    private CurrentJobPostingFrame jobPostingFrame;
    private OpenPositions TEMPOPENPOSITIONS;
    private ConfirmationDialog unsavedChangesPopup;
    private float BaseScale;
    private UIScaleHelper scaleHelper;
    private bool PlayerTriedToExitCompletely;

    public JobPostingPanel(
      ShopEntry shopEntry,
      OpenPositions _TEMPOPENPOSITIONS,
      OpenPositions _ORIGINALOPENPOSITIONS,
      Player player,
      float _BaseScale,
      EmployeeType _RoamingEmplyeeType = EmployeeType.None)
    {
      this.TEMPOPENPOSITIONS = _TEMPOPENPOSITIONS;
      this.BaseScale = _BaseScale;
      this.scaleHelper = new UIScaleHelper(this.BaseScale);
      double defaultYbuffer = (double) this.scaleHelper.GetDefaultYBuffer();
      double defaultXbuffer = (double) this.scaleHelper.GetDefaultXBuffer();
      float num = 0.0f;
      this.bigBrownPanel = new BigBrownPanel(Vector2.Zero, true, "Recruitment", this.BaseScale, true);
      this.jobPostingFrame = new CurrentJobPostingFrame(shopEntry, _TEMPOPENPOSITIONS, _ORIGINALOPENPOSITIONS, player, this.BaseScale, _RoamingEmplyeeType);
      this.jobPostingFrame.location.Y = num;
      Vector2 size = this.jobPostingFrame.GetSize();
      this.jobPostingFrame.location.Y += size.Y * 0.5f;
      float y = num + size.Y;
      this.bigBrownPanel.Finalize(new Vector2(size.X, y));
      this.jobPostingFrame.location.Y += this.bigBrownPanel.GetFrameOffsetFromTop().Y;
      this.lerper = new LerpHandler_Float();
      this.LerpIn();
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

    public OpenPositions GetTempOpenPositions() => this.TEMPOPENPOSITIONS;

    public bool CheckMouseOver(Player player) => this.bigBrownPanel.CheckMouseOver(player, this.location);

    public bool UpdateJobPostingPanel(
      Player player,
      float DeltaTime,
      out bool GoToAgencyRecruit,
      out bool ExitCompletely,
      out bool ApplyChanges)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      Vector2 location = this.location;
      GoToAgencyRecruit = false;
      ExitCompletely = false;
      ApplyChanges = false;
      this.bigBrownPanel.UpdateDragger(player, ref this.location, DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return false;
      if (this.jobPostingFrame.UpdateCurrentJobPostingFrame(player, DeltaTime, location))
      {
        ApplyChanges = true;
        return true;
      }
      bool flag = false;
      if (this.bigBrownPanel.UpdatePanelpreviousButton(player, DeltaTime, location))
        flag = true;
      if (this.bigBrownPanel.UpdatePanelCloseButton(player, DeltaTime, location))
      {
        ExitCompletely = true;
        flag = true;
      }
      if (flag && this.CheckForUnsavedChanged_PopUp())
      {
        flag = false;
        this.PlayerTriedToExitCompletely = ExitCompletely;
        ExitCompletely = false;
      }
      bool confirmed;
      if (this.unsavedChangesPopup != null && this.unsavedChangesPopup.UpdateConfirmationDialog(player, location, DeltaTime, out confirmed))
      {
        if (confirmed)
        {
          ExitCompletely = this.PlayerTriedToExitCompletely;
          flag = true;
        }
        else
        {
          this.unsavedChangesPopup = (ConfirmationDialog) null;
          this.bigBrownPanel.Active = true;
        }
      }
      return flag;
    }

    private bool CheckForUnsavedChanged_PopUp()
    {
      if (!this.jobPostingFrame.GetHasUnsavedChanges())
        return false;
      this.unsavedChangesPopup = new ConfirmationDialog("Discard Changes?", "You have changes that have not been applied, are you sure you want to exit?", this.BaseScale, customTextX_raw: this.scaleHelper.ScaleX(400f));
      this.bigBrownPanel.Active = false;
      return true;
    }

    public void DrawJobPostingPanel(SpriteBatch spriteBatch)
    {
      if ((double) this.lerper.Value == 1.0)
        return;
      Vector2 location = this.location;
      location.X += this.lerper.Value * ManageEmployeeManager.LerpDistance;
      this.bigBrownPanel.DrawBigBrownPanel(location, spriteBatch);
      this.jobPostingFrame.DrawCurrentJobPostingFrame(location, spriteBatch);
      this.bigBrownPanel.DrawDarkOverlay(location, spriteBatch);
      if (this.unsavedChangesPopup == null)
        return;
      this.unsavedChangesPopup.DrawConfirmationDialog(spriteBatch, location);
    }
  }
}
