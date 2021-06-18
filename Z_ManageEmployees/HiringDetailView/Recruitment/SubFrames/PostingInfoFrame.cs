// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.PostingInfoFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.V2;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class PostingInfoFrame
  {
    public Vector2 location;
    private MiniHeading miniHeading;
    private CustomerFrame customerFrame;
    private SimpleTextHandler text;
    private ZCheckBox checkBox;
    private ZGenericText costHeader;
    private ZGenericText costNumberText;
    private string costNumberBaseString;
    private RecruitmentInfoIcon icon;
    private bool isCheckBoxActive;
    private OpenPositions TEMPOPENPOSITIONS;
    private bool UseV2;
    private PostingInfoFrame_V2 V2InfoFrame;

    public JobPostingModifiers refInfoType { get; private set; }

    public PostingInfoFrame(
      JobPostingModifiers infoType,
      OpenPositions _TEMPOPENPOSITIONS,
      float forceWidth,
      float BaseScale,
      bool _UseV2)
    {
      this.refInfoType = infoType;
      this.TEMPOPENPOSITIONS = _TEMPOPENPOSITIONS;
      this.UseV2 = _UseV2;
      if (!this.UseV2)
        return;
      this.costNumberBaseString = "$";
      this.V2InfoFrame = new PostingInfoFrame_V2(infoType, _TEMPOPENPOSITIONS, BaseScale);
      this.ReflectNewData();
    }

    private void SetActive(bool isActive, bool _checkBoxIsActive = false)
    {
      if (this.UseV2)
      {
        this.V2InfoFrame.SetActive(isActive, _checkBoxIsActive);
      }
      else
      {
        this.isCheckBoxActive = _checkBoxIsActive;
        if (!isActive)
        {
          this.customerFrame.SetInactiveGrey();
          Vector3 vector3 = Color.LightGray.ToVector3();
          this.miniHeading.SetAllColours(vector3);
          this.text.SetAllColours(vector3);
          this.costNumberText.SetAllColours(vector3);
          this.costHeader.SetAllColours(vector3);
        }
        else
        {
          this.customerFrame.ResetColor();
          Vector3 zCream = ColourData.Z_Cream;
          this.miniHeading.SetAllColours(zCream);
          this.text.SetAllColours(zCream);
          this.costNumberText.SetAllColours(zCream);
          this.costHeader.SetAllColours(zCream);
        }
        if (this.checkBox == null)
          return;
        if (this.isCheckBoxActive)
        {
          this.checkBox.SetColorTint(Color.White.ToVector3());
          this.icon.SetAllColours(Color.White.ToVector3());
        }
        else
        {
          this.checkBox.SetColorTint(Color.DarkGray.ToVector3());
          this.icon.SetAllColours(Color.DarkGray.ToVector3());
        }
      }
    }

    private void SetCheckboxTick(bool ticked)
    {
      if (this.UseV2)
        this.V2InfoFrame.SetCheckbox(ticked);
      else
        this.checkBox.SetTicked(ticked);
    }

    public Vector2 GetSize() => this.UseV2 ? this.V2InfoFrame.GetSize() : this.customerFrame.VSCale;

    public void ReflectNewData()
    {
      switch (this.refInfoType)
      {
        case JobPostingModifiers.AdminCost:
          this.SetActive(this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0);
          break;
        case JobPostingModifiers.SocialMedia:
          this.SetActive(this.TEMPOPENPOSITIONS.IsSocialMediaEnabled && this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0, this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0);
          this.SetCheckboxTick(this.TEMPOPENPOSITIONS.IsSocialMediaEnabled);
          break;
        case JobPostingModifiers.JobPortal:
          this.SetActive(this.TEMPOPENPOSITIONS.IsJobPortalEnabled && this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0, this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0);
          this.SetCheckboxTick(this.TEMPOPENPOSITIONS.IsJobPortalEnabled);
          break;
        case JobPostingModifiers.Totals:
          this.SetActive(this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0);
          break;
      }
      string numberBaseString = this.costNumberBaseString;
      string newString;
      if (this.refInfoType == JobPostingModifiers.Totals)
      {
        newString = numberBaseString + (object) JobApplicants_Calculator.CalculateTotalCostPerDay(this.TEMPOPENPOSITIONS.NumberOfPositionsOpened, this.TEMPOPENPOSITIONS.IsSocialMediaEnabled, this.TEMPOPENPOSITIONS.IsJobPortalEnabled);
      }
      else
      {
        newString = numberBaseString + (object) JobApplicants_Calculator.GetCostOfThisPerDay(this.refInfoType);
        if (this.TEMPOPENPOSITIONS.NumberOfPositionsOpened > 1)
          newString = newString + "x" + (object) this.TEMPOPENPOSITIONS.NumberOfPositionsOpened;
      }
      if (this.UseV2)
        this.V2InfoFrame.SetNewCostString(newString);
      else
        this.costNumberText.textToWrite = newString;
    }

    public static string GetHeaderTextForThisModifier(JobPostingModifiers modifier)
    {
      switch (modifier)
      {
        case JobPostingModifiers.AdminCost:
          return "Admin Cost";
        case JobPostingModifiers.SocialMedia:
          return "Social Media";
        case JobPostingModifiers.JobPortal:
          return "Job Portal";
        case JobPostingModifiers.Totals:
          return "Cost / Day";
        default:
          return "MODIFIER HEADER HERE";
      }
    }

    public static string GetDescriptionTextForThisModifier(JobPostingModifiers modifier)
    {
      switch (modifier)
      {
        case JobPostingModifiers.AdminCost:
          return "Opening a job position will require a basic admin cost.";
        case JobPostingModifiers.SocialMedia:
          return "Post your job openings on social media to spread the word!";
        case JobPostingModifiers.JobPortal:
          return "Advertise your job openings on job portals and trade magazines to gain an even wider reach!";
        case JobPostingModifiers.Totals:
          return "";
        default:
          return "MODIFIER DESC HERE";
      }
    }

    public bool UpdatePostingInfoFrame(Player player, float DeltaTIme, Vector2 offset)
    {
      offset += this.location;
      bool flag1 = false;
      if (this.UseV2)
        flag1 = this.V2InfoFrame.UpdatePostingInfoFrame_V2(player, DeltaTIme, offset);
      else if (this.checkBox != null && this.isCheckBoxActive && this.checkBox.UpdateCheckBox(player, offset))
        flag1 = true;
      if (flag1)
      {
        bool flag2 = !this.UseV2 ? this.checkBox.GetIsTicked() : this.V2InfoFrame.GetIsTicked();
        switch (this.refInfoType)
        {
          case JobPostingModifiers.SocialMedia:
            this.TEMPOPENPOSITIONS.IsSocialMediaEnabled = !flag2;
            break;
          case JobPostingModifiers.JobPortal:
            this.TEMPOPENPOSITIONS.IsJobPortalEnabled = !flag2;
            break;
        }
      }
      return flag1;
    }

    public void DrawPostingInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (this.UseV2)
      {
        this.V2InfoFrame.DrawPostingInfoFrame_V2(offset, spriteBatch);
      }
      else
      {
        this.customerFrame.DrawCustomerFrame(offset, spriteBatch);
        this.miniHeading.DrawMiniHeading(offset, spriteBatch);
        this.icon.DrawRecruitmentInfoIcon(offset, spriteBatch);
        this.text.DrawSimpleTextHandler(offset, 1f, spriteBatch);
        if (this.checkBox != null)
          this.checkBox.DrawCheckBox(spriteBatch, offset);
        this.costHeader.DrawZGenericText(offset, spriteBatch);
        this.costNumberText.DrawZGenericText(offset, spriteBatch);
      }
    }

    public void PostDrawPostingInfoFrame(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      if (!this.UseV2)
        return;
      this.V2InfoFrame.PostDrawPostingInfoFrame_v2(offset, spriteBatch);
    }
  }
}
