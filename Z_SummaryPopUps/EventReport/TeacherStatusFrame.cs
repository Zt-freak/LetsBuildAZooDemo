// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.TeacherStatusFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class TeacherStatusFrame : ReportStatusFrame
  {
    private static Rectangle paperbg = new Rectangle(559, 496, 465, 423);
    private static Rectangle agrade = new Rectangle(464, 825, 94, 94);
    private static Rectangle bgrade = new Rectangle(464, 730, 94, 94);
    private static Rectangle cgrade = new Rectangle(464, 635, 94, 94);
    private static Rectangle fgrade = new Rectangle(464, 540, 94, 94);
    private ZGenericUIDrawObject background;
    private ZGenericUIDrawObject grade;
    private ZGenericText heading;
    private ZGenericText date;
    private ZGenericText checklistheading;
    private ZGenericChecklist checklist;
    private ParagraphWithHeading remarks;
    private ParagraphWithHeading summary;
    private ReportStamp stamp;
    private CustomerFrame frame;
    private UIShake shaker;
    private bool showgrade;
    private Vector2 shake;

    public TeacherStatusFrame(ReportResultType type, ReportResultRank rank, float basescale_)
      : base(basescale_)
    {
      this.showgrade = false;
      this.shaker = new UIShake(this.basescale, 0.2f, 3f, 40f);
      Vector3 zDarkTextGray = ColourData.Z_DarkTextGray;
      this.background = new ZGenericUIDrawObject(TeacherStatusFrame.paperbg, this.basescale, AssetContainer.UISheet);
      this.stamp = new ReportStamp(this.basescale);
      this.date = new ZGenericText(Z_GameFlags.GetGameDateToday_AsString(), this.basescale, false, _UseOnePointFiveFont: true);
      this.date.SetAllColours(zDarkTextGray);
      this.checklistheading = new ZGenericText("ANIMAL VISITED:", this.basescale, false, _UseOnePointFiveFont: true);
      this.checklistheading.SetAllColours(zDarkTextGray);
      this.checklist = new ZGenericChecklist(this.basescale, useSpringfont1point5_: true);
      this.checklist.Add("Lions", useThisNum: 1);
      this.checklist.Add("Tigers", useThisNum: 2);
      this.checklist.Add("Bears", useThisNum: 3);
      this.checklist.Add("Oh my", useThisNum: 4);
      this.checklist.SetTextColour(zDarkTextGray);
      Rectangle drawrect = Game1.WhitePixelRect;
      Rectangle whitePixelRect = Game1.WhitePixelRect;
      this.heading = new ZGenericText("SCHOOL TRIP ", this.basescale * 2f, false, _UseOnePointFiveFont: true);
      this.heading.SetAllColours(zDarkTextGray);
      this.remarks = new ParagraphWithHeading("REMARKS", "The guide was friendly and knowledgeable. The students had many opportunities to learn. It was an enriching exerience for us all.", this.scalehelper.ScaleX(400f), this.basescale);
      this.summary = new ParagraphWithHeading("SUMMARY", "The trip was very educational, will recommend adding it to the regular curriculum.", this.scalehelper.ScaleX(400f), this.basescale);
      switch (rank)
      {
        case ReportResultRank.A:
          drawrect = TeacherStatusFrame.agrade;
          break;
        case ReportResultRank.B:
          drawrect = TeacherStatusFrame.bgrade;
          break;
        case ReportResultRank.C:
          drawrect = TeacherStatusFrame.cgrade;
          break;
        case ReportResultRank.F:
          drawrect = TeacherStatusFrame.fgrade;
          break;
      }
      this.grade = new ZGenericUIDrawObject(drawrect, this.basescale, AssetContainer.UISheet);
      this.grade.Alpha = 0.75f;
      this.framescale = this.background.GetSize();
      this.framescale = this.framescale + 2f * this.pad;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      int maxValue = 10;
      this.grade.location = this.scalehelper.ScaleVector2(new Vector2(130f, -114f) + new Vector2((float) Game1.Rnd.Next(-maxValue, maxValue), (float) Game1.Rnd.Next(-maxValue, maxValue)));
      this.stamp.location = this.grade.location;
      Vector2 vector2 = new Vector2() + (-0.5f * this.framescale + this.scalehelper.ScaleVector2(new Vector2(40f, 40f)));
      this.heading.vLocation = vector2;
      vector2.Y += this.heading.GetSize().Y;
      this.date.vLocation = vector2;
      vector2.Y += this.date.GetSize().Y + this.pad.Y;
      this.checklistheading.vLocation = vector2;
      vector2.Y += this.checklistheading.GetSize().Y + this.pad.Y;
      this.checklist.location = vector2 + 0.5f * this.checklist.GetSize();
      vector2.Y += Math.Max(this.checklist.GetSize().Y, this.scalehelper.ScaleY(70f)) + this.pad.Y;
      this.remarks.location = vector2 + 0.5f * this.remarks.GetSize();
      vector2.Y += this.remarks.GetSize().Y + this.pad.Y;
      this.summary.location = vector2 + 0.5f * this.summary.GetSize();
      vector2.Y += this.summary.GetSize().Y + this.pad.Y;
    }

    public override bool UpdateReportStatusFrame(Player player, Vector2 offset, float DeltaTime)
    {
      offset += this.location;
      bool flag = false;
      this.shake = new Vector2();
      if (this.stamp.UpdateReportStamp(player, offset, DeltaTime))
      {
        this.showgrade = true;
        flag = true;
      }
      if (this.showgrade && !this.shaker.IsDone)
        this.shake = this.shaker.UpdateUIShake(DeltaTime);
      return flag;
    }

    public override void DrawReportStatusFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      offset += this.shake;
      this.background.DrawZGenericUIDrawObject(spritebatch, offset);
      if (this.showgrade)
        this.grade.DrawZGenericUIDrawObject(spritebatch, offset);
      this.heading.DrawZGenericText(offset, spritebatch);
      this.date.DrawZGenericText(offset, spritebatch);
      this.checklistheading.DrawZGenericText(offset, spritebatch);
      this.checklist.DrawZGenericChecklist(offset, spritebatch);
      this.remarks.DrawParagraphWithHeading(spritebatch, offset);
      this.summary.DrawParagraphWithHeading(spritebatch, offset);
      this.stamp.DrawReportStamp(spritebatch, offset);
    }
  }
}
