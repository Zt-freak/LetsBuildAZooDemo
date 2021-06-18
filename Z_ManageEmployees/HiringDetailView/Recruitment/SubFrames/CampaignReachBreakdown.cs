// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames.CampaignReachBreakdown
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.Z_BalanceSystems.Employees;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Animal.Shared;

namespace TinyZoo.Z_ManageEmployees.HiringDetailView.Recruitment.SubFrames
{
  internal class CampaignReachBreakdown
  {
    public Vector2 location;
    private CampaignReachBarWithIcon reachBar;
    private CampaignReachBarWithIcon skillBar;
    private MiniHeading miniHeading;
    private ZGenericText applicantsPerDayText;

    public CampaignReachBreakdown(
      float BaseScale,
      Vector2 frameSize,
      OpenPositions TEMPOPENPOSITIONS,
      Player player)
    {
      UIScaleHelper uiScaleHelper = new UIScaleHelper(BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      this.miniHeading = new MiniHeading(frameSize, "Campaign Reach", 1f, BaseScale);
      this.reachBar = new CampaignReachBarWithIcon(TEMPOPENPOSITIONS, BaseScale, player, CampaignReachType.CampaignReach);
      this.skillBar = new CampaignReachBarWithIcon(TEMPOPENPOSITIONS, BaseScale, player, CampaignReachType.SkillRequirement);
      this.applicantsPerDayText = new ZGenericText("X", BaseScale, _UseOnePointFiveFont: true);
      Vector2 vector2_1 = defaultBuffer;
      vector2_1.Y += this.miniHeading.GetTextHeight(true);
      vector2_1.Y += defaultBuffer.Y;
      float num = uiScaleHelper.ScaleX(15f);
      this.reachBar.location.X -= num;
      this.reachBar.location.X -= this.reachBar.GetSize().X;
      this.reachBar.location.Y = vector2_1.Y;
      this.skillBar.location.X += num;
      this.skillBar.location.Y = vector2_1.Y;
      vector2_1.Y += this.reachBar.GetSize().Y;
      vector2_1.Y += defaultBuffer.Y * 2f;
      this.applicantsPerDayText.vLocation.Y = vector2_1.Y;
      this.applicantsPerDayText.vLocation.Y += this.applicantsPerDayText.GetSize().Y * 0.5f;
      Vector2 vector2_2 = frameSize * -0.5f;
      this.reachBar.location.Y += vector2_2.Y;
      this.skillBar.location.Y += vector2_2.Y;
      this.applicantsPerDayText.vLocation.Y += vector2_2.Y;
    }

    public void ReflectNewData(OpenPositions TEMPOPENPOSITIONS, Player player)
    {
      string str;
      if (TEMPOPENPOSITIONS.NumberOfPositionsOpened > 0)
      {
        float TotalPerDay;
        JobApplicants_Calculator.GetEstimatedTimeForAnApplicant(TEMPOPENPOSITIONS, out int _, out int _, player, out int _, out TotalPerDay);
        str = ((int) Math.Round((double) TotalPerDay)).ToString();
      }
      else
        str = "-";
      this.applicantsPerDayText.textToWrite = "Average Applicants Per Day: " + str;
      this.reachBar.SetNewNumbers(TEMPOPENPOSITIONS, player);
      this.skillBar.SetNewNumbers(TEMPOPENPOSITIONS, player);
    }

    public void UpdateCampaignReachBreakdown(Player player, float DeltaTime, Vector2 offset)
    {
      offset += this.location;
      this.reachBar.UpdateCampaignReachBarWithIcon(player, DeltaTime, offset);
      this.skillBar.UpdateCampaignReachBarWithIcon(player, DeltaTime, offset);
    }

    public void DrawCampaignReachBreakdown(Vector2 offset, SpriteBatch spriteBatch)
    {
      offset += this.location;
      this.miniHeading.DrawMiniHeading(offset, spriteBatch);
      this.reachBar.DrawCampaignReachBarWithIcon(offset, spriteBatch);
      this.skillBar.DrawCampaignReachBarWithIcon(offset, spriteBatch);
      this.applicantsPerDayText.DrawZGenericText(offset, spriteBatch);
    }
  }
}
