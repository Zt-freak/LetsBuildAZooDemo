// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.InspectionStatusFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class InspectionStatusFrame : ReportStatusFrame
  {
    private static Rectangle paperbg = new Rectangle(559, 496, 465, 423);
    private static Rectangle agrade = new Rectangle(464, 825, 94, 94);
    private static Rectangle bgrade = new Rectangle(464, 730, 94, 94);
    private static Rectangle cgrade = new Rectangle(464, 635, 94, 94);
    private static Rectangle fgrade = new Rectangle(464, 540, 94, 94);
    private static Rectangle animalWelfareLogo = new Rectangle(363, 818, 100, 102);
    private static Rectangle safetyCompanyLogo = new Rectangle(0, 910, 190, 58);
    private static Rectangle monehrect = new Rectangle(89, 248, 174, 169);
    private ZGenericUIDrawObject moneh;
    private ZGenericUIDrawObject background;
    private ZGenericUIDrawObject grade;
    private ZGenericUIDrawObject logo;
    private ReportSignature sign;
    private ZGenericText heading;
    private ZGenericText date;
    private SimpleTextHandler description;
    private ZGenericChecklist checklist;
    private ReportStamp stamp;
    private CustomerFrame frame;
    private UIShake shaker;
    private bool showgrade;
    private bool isbribed;
    private Vector2 shake;

    public InspectionStatusFrame(
      WalkingPerson person,
      ReportResultType type,
      ReportResultRank rank,
      float basescale_)
      : base(basescale_)
    {
      this.showgrade = false;
      this.shaker = new UIShake(this.basescale, 0.2f, 3f, 40f);
      this.moneh = new ZGenericUIDrawObject(InspectionStatusFrame.monehrect, this.basescale, AssetContainer.UISheet);
      this.background = new ZGenericUIDrawObject(InspectionStatusFrame.paperbg, this.basescale, AssetContainer.UISheet);
      this.sign = new ReportSignature(this.basescale, SignatureType.HelloGuy);
      this.stamp = new ReportStamp(this.basescale);
      this.date = new ZGenericText(Z_GameFlags.GetGameDateToday_AsString(), this.basescale, false, _UseOnePointFiveFont: true);
      this.date.SetAllColours(ColourData.Z_DarkTextGray);
      List<string> resultFlavourText = person.simperson.memberofthepublic.animalwelfarecontroller.GetFinalResultFlavourText();
      string TextToWrite = "";
      this.checklist = new ZGenericChecklist(this.basescale, useSpringfont1point5_: true);
      if (resultFlavourText.Count > 0)
      {
        TextToWrite = resultFlavourText[0];
        for (int index = 1; index < resultFlavourText.Count; ++index)
          this.checklist.Add(resultFlavourText[index], useThisNum: index);
      }
      this.checklist.SetTextColour(ColourData.Z_DarkTextGray);
      Rectangle drawrect1 = Game1.WhitePixelRect;
      Rectangle whitePixelRect = Game1.WhitePixelRect;
      Rectangle drawrect2;
      if (type != ReportResultType.AnimalWelfare)
      {
        if (type != ReportResultType.Safety)
          throw new NotImplementedException();
        this.heading = new ZGenericText("SAFETY REPORT", this.basescale * 2f, false, _UseOnePointFiveFont: true);
        this.heading.SetAllColours(ColourData.Z_DarkTextGray);
        drawrect2 = InspectionStatusFrame.safetyCompanyLogo;
        this.description = new SimpleTextHandler("This is an extremely dangerous workplace", this.scalehelper.ScaleX(260f), _Scale: this.basescale, _UseFontOnePointFive: true);
        this.description.AutoCompleteParagraph();
      }
      else
      {
        this.heading = new ZGenericText("WELFARE REPORT", this.basescale * 2f, false, _UseOnePointFiveFont: true);
        this.heading.SetAllColours(ColourData.Z_DarkTextGray);
        drawrect2 = InspectionStatusFrame.animalWelfareLogo;
        this.description = new SimpleTextHandler(TextToWrite, this.scalehelper.ScaleX(260f), _Scale: this.basescale, _UseFontOnePointFive: true);
        this.description.AutoCompleteParagraph();
        this.isbribed = person.simperson.memberofthepublic.animalwelfarecontroller.GetIsBribed();
      }
      this.description.SetAllColours(ColourData.Z_DarkTextGray);
      switch (rank)
      {
        case ReportResultRank.A:
          drawrect1 = InspectionStatusFrame.agrade;
          break;
        case ReportResultRank.B:
          drawrect1 = InspectionStatusFrame.bgrade;
          break;
        case ReportResultRank.C:
          drawrect1 = InspectionStatusFrame.cgrade;
          break;
        case ReportResultRank.F:
          drawrect1 = InspectionStatusFrame.fgrade;
          break;
      }
      this.grade = new ZGenericUIDrawObject(drawrect1, this.basescale, AssetContainer.UISheet);
      this.grade.Alpha = 0.75f;
      this.logo = new ZGenericUIDrawObject(drawrect2, this.basescale, AssetContainer.UISheet);
      this.framescale = this.background.GetSize();
      this.framescale = this.framescale + 2f * this.pad;
      this.frame = new CustomerFrame(this.framescale, BaseScale: this.basescale);
      int maxValue = 10;
      this.grade.location = this.scalehelper.ScaleVector2(new Vector2(130f, -114f) + new Vector2((float) Game1.Rnd.Next(-maxValue, maxValue), (float) Game1.Rnd.Next(-maxValue, maxValue)));
      this.stamp.location = this.grade.location;
      this.logo.location = this.scalehelper.ScaleVector2(new Vector2(90f, 130f));
      this.sign.location = this.scalehelper.ScaleVector2(new Vector2(-120f, 140f));
      this.heading.vLocation = -0.5f * this.framescale + this.scalehelper.ScaleVector2(new Vector2(40f, 40f));
      this.date.vLocation = this.heading.vLocation;
      this.date.vLocation.Y += this.heading.GetSize().Y;
      this.description.Location = this.date.vLocation;
      this.description.Location.Y += this.date.GetSize().Y + 1.5f * this.pad.Y;
      this.checklist.location = this.description.Location + 0.5f * this.checklist.GetSize();
      this.checklist.location.Y += this.description.GetSize().Y + 1f * this.pad.Y;
      this.moneh.location = this.scalehelper.ScaleVector2(new Vector2(92f, -139f));
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
      this.logo.DrawZGenericUIDrawObject(spritebatch, offset);
      this.sign.DrawReportSignature(spritebatch, offset);
      this.heading.DrawZGenericText(offset, spritebatch);
      this.date.DrawZGenericText(offset, spritebatch);
      this.description.DrawSimpleTextHandler(offset, 1f, spritebatch);
      this.checklist.DrawZGenericChecklist(offset, spritebatch);
      this.stamp.DrawReportStamp(spritebatch, offset);
      if (!this.isbribed)
        return;
      this.moneh.DrawZGenericUIDrawObject(spritebatch, offset);
    }
  }
}
