// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.EventReportManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class EventReportManager
  {
    private EventReportPanel panel;

    public EventReportManager(WalkingPerson walkingperson)
    {
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      ReportResultType type = ReportResultType.SocialMedia;
      if (walkingperson.simperson.customertype == CustomerType.AnimalWelfareOfficer)
        type = ReportResultType.AnimalWelfare;
      ReportResultRank rank = ReportResultRank.Count;
      if (walkingperson.simperson.customertype == CustomerType.AnimalWelfareOfficer)
        rank = walkingperson.simperson.memberofthepublic.animalwelfarecontroller.GetFinalRank();
      this.panel = new EventReportPanel(walkingperson, type, rank, baseScaleForUi);
      this.panel.location = Sengine.HalfReferenceScreenRes;
    }

    public bool UpdateEventReportManager(Player player, float DeltaTime) => (0 | (this.panel.UpdateEventReportPanel(player, Vector2.Zero, DeltaTime) ? 1 : 0)) != 0;

    public void DrawEventReportManager(SpriteBatch spritebatch) => this.panel.DrawEventReportPanel(spritebatch, Vector2.Zero);
  }
}
