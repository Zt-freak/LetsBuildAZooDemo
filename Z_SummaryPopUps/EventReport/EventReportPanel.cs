// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.EventReportPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport.EventData;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class EventReportPanel
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private BigBrownPanel panel;
    private Vector2 framescale;
    private ReportResultsFrame results;
    private ReportStatusFrame report;
    private ReportActionSet reportset;

    public EventReportPanel(
      WalkingPerson person,
      ReportResultType type,
      ReportResultRank rank,
      float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      Vector2 defaultBuffer = this.uiscale.DefaultBuffer;
      this.reportset = ReportActionsGetter.GetDataSet(person.simperson.customertype);
      switch (type)
      {
        case ReportResultType.AnimalWelfare:
        case ReportResultType.Safety:
          this.report = (ReportStatusFrame) new InspectionStatusFrame(person, type, rank, this.basescale);
          break;
        case ReportResultType.SocialMedia:
          this.report = (ReportStatusFrame) new SocialMediaStatusFrame(AnimalType.Protestor1, rank, this.basescale);
          break;
        case ReportResultType.Teacher:
          this.report = (ReportStatusFrame) new TeacherStatusFrame(ReportResultType.Teacher, rank, this.basescale);
          break;
        case ReportResultType.FoodCritic:
          this.report = (ReportStatusFrame) new FoodCriticStatusFrame(AnimalType.FoodCritic_BlackHairGlasses, rank, this.basescale);
          break;
      }
      this.results = new ReportResultsFrame(rank, this.basescale);
      this.results.Add(type, ReportResultRank.A, this.reportset);
      this.results.Add(type, ReportResultRank.B, this.reportset);
      this.results.Add(type, ReportResultRank.C, this.reportset);
      this.results.Add(type, ReportResultRank.F, this.reportset);
      this.framescale = new Vector2();
      this.framescale += this.report.GetSize();
      this.framescale.X += defaultBuffer.X;
      this.framescale.X += this.results.GetSize().X;
      Vector2 vector2 = -0.5f * this.framescale;
      this.report.location = vector2 + 0.5f * this.report.GetSize();
      vector2.X += this.report.GetSize().X + defaultBuffer.X;
      this.results.location = vector2 + 0.5f * this.results.GetSize();
      vector2.Y += this.results.GetSize().Y + defaultBuffer.Y;
      this.panel = new BigBrownPanel(this.framescale, addHeaderText: "Event Report", _BaseScale: this.basescale);
      this.panel.Finalize(this.framescale);
    }

    public bool UpdateEventReportPanel(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      this.panel.UpdateDragger(player, ref this.location, DeltaTime);
      int num1 = 0 | (this.panel.UpdatePanelCloseButton(player, DeltaTime, offset) ? 1 : 0);
      if (this.report.UpdateReportStatusFrame(player, offset, DeltaTime))
        this.results.Animating = true;
      int num2 = this.results.UpdateQuestResultsFrame(player, offset, DeltaTime) ? 1 : 0;
      return (num1 | num2) != 0;
    }

    public void DrawEventReportPanel(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.panel.DrawBigBrownPanel(offset, spritebatch);
      this.report.DrawReportStatusFrame(spritebatch, offset);
      this.results.DrawQuestResultsFrame(spritebatch, offset);
    }
  }
}
