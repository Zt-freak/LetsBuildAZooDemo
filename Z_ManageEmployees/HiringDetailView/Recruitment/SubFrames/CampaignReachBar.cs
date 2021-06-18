// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.CampaignReachBar
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class CampaignReachBar
  {
    public Vector2 location;
    private SatisfactionBar satisfactionBar;
    private ZGenericText barLabel;
    private Vector3 originalBarColour;
    private string baseString;
    private ZGenericText barBottomLabel;
    private CampaignReachType reachType;
    private Vector2 size;
    private CustomerFrameMouseOverBox mouseOverBox;
    private bool Active;

    public CampaignReachBar(
      OpenPositions TEMPOPENPOSITIONS,
      float BaseScale,
      Player player,
      CampaignReachType _reachType)
    {
      this.reachType = _reachType;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      this.satisfactionBar = new SatisfactionBar(1f, BaseScale);
      this.originalBarColour = this.satisfactionBar.GetColourAsVector3();
      this.baseString = this.reachType != CampaignReachType.SkillRequirement ? "Population Reach" : "% Qualified People";
      this.barLabel = new ZGenericText(this.baseString, BaseScale);
      this.barBottomLabel = new ZGenericText("X", BaseScale, _UseOnePointFiveFont: true);
      this.barLabel.vLocation.Y += this.barLabel.GetSize().Y * 0.5f;
      this.size.Y += this.barLabel.GetSize().Y;
      this.satisfactionBar.vLocation.Y = this.size.Y;
      this.satisfactionBar.vLocation.Y += this.satisfactionBar.GetSize().Y * 0.5f;
      this.size.Y += this.satisfactionBar.GetSize().Y;
      this.size.Y += uiScaleHelper.ScaleY(3f);
      this.barBottomLabel.vLocation.Y = this.size.Y;
      this.barBottomLabel.vLocation.Y += this.barBottomLabel.GetSize().Y * 0.5f;
      this.size.Y += this.barBottomLabel.GetSize().Y;
      this.size.X = Math.Max(this.barLabel.GetSize().X, this.satisfactionBar.GetSize().X);
      this.size.X = Math.Max(this.size.X, this.barBottomLabel.GetSize().X);
      this.mouseOverBox = new CustomerFrameMouseOverBox(BaseScale, this.GetDescriptionForThis(), uiScaleHelper.ScaleX(350f));
      this.SetNewNumbers(TEMPOPENPOSITIONS, player);
      this.Active = true;
    }

    private string GetDescriptionForThis() => this.reachType == CampaignReachType.SkillRequirement ? "Percentage skill match of the population. Each job requires different skills - the more specialised, the fewer people have them." : "How widespread your campaign is among the population. More advertisement, more attention! Bigger population, more attention!";

    public void SetNewNumbers(OpenPositions TEMPOPENPOSITIONS, Player player)
    {
      int PercentageOfPopulationQualifiedForRole;
      int PopulationSize;
      JobApplicants_Calculator.GetEstimatedTimeForAnApplicant(TEMPOPENPOSITIONS, out int _, out PercentageOfPopulationQualifiedForRole, player, out PopulationSize, out float _);
      if (this.reachType == CampaignReachType.SkillRequirement)
      {
        this.barBottomLabel.textToWrite = PercentageOfPopulationQualifiedForRole.ToString() + "%";
        this.satisfactionBar.SetFullness((float) PercentageOfPopulationQualifiedForRole * 0.01f);
      }
      else
      {
        int totalReach = TEMPOPENPOSITIONS.GetTotalReach();
        this.barBottomLabel.textToWrite = TEMPOPENPOSITIONS.GetTotalReach().ToString() + "/" + (object) PopulationSize;
        this.satisfactionBar.SetFullness((float) totalReach / (float) PopulationSize);
      }
    }

    public void SetActive(bool _isActive)
    {
      this.Active = _isActive;
      if (_isActive)
      {
        this.satisfactionBar.SetAllColours(Color.White.ToVector3());
        this.barLabel.SetAllColours(ColourData.Z_Cream);
      }
      else
      {
        SatisfactionBar satisfactionBar = this.satisfactionBar;
        Color color = Color.DarkGray;
        Vector3 vector3_1 = color.ToVector3();
        satisfactionBar.SetAllColours(vector3_1);
        ZGenericText barLabel = this.barLabel;
        color = Color.LightGray;
        Vector3 vector3_2 = color.ToVector3();
        barLabel.SetAllColours(vector3_2);
      }
    }

    public Vector2 GetSize() => this.size;

    public Vector2 GetBarSizeOnly() => this.satisfactionBar.GetSize();

    public float GetExtraTextOffsetFromTop() => 0.0f;

    public void UpdateCampaignReachBar(Player player, Vector2 offset, bool MouseIsOverIcon = false)
    {
      offset += this.location;
      if (!this.Active)
        return;
      if (this.satisfactionBar.CheckMouseOver(player, offset) | MouseIsOverIcon)
        this.mouseOverBox.Active = true;
      else
        this.mouseOverBox.Active = false;
    }

    public void DrawCampaignReachBar(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.satisfactionBar.DrawSatisfactionBar(offset, spriteBatch);
      this.barLabel.DrawZGenericText(offset, spriteBatch);
      this.barBottomLabel.DrawZGenericText(offset, spriteBatch);
      this.mouseOverBox.DrawCustomerFrameMouseOverBoc(offset, spriteBatch);
    }
  }
}
