// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ReportResultsFrame
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GenericUI;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport.EventData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ReportResultsFrame
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper uiscale;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private ZGenericText pickone;
    private ZGenericText outcomechosen;
    private SimpleTextHandler thinghappened;
    private TextButton button;
    private Dictionary<ReportResultRank, ReportResult> questresults;
    private bool needsresize;
    private float forceToThisWidth = -1f;
    private ReportResultRank rankachieved;
    private LerpHandler_Float lerper;
    private ReportResultsFrame.ReportResultState state;
    private float buttonalpha;
    private float thinghappenedalpha;
    private Vector2 animateExtraY;
    private bool animating;

    public ReportResultsFrame(ReportResultRank rankachieved_, float basescale_)
    {
      this.basescale = basescale_;
      this.uiscale = new UIScaleHelper(this.basescale);
      this.pad = this.uiscale.DefaultBuffer;
      this.rankachieved = rankachieved_;
      this.questresults = new Dictionary<ReportResultRank, ReportResult>();
      this.pickone = new ZGenericText("PICK AN OUTCOME:", this.basescale, false, _UseOnePointFiveFont: true);
      this.pickone.SetAlpha(0.0f);
      this.outcomechosen = new ZGenericText("OUTCOME CHOSEN: ", this.basescale, false, _UseOnePointFiveFont: true);
      this.outcomechosen.SetAlpha(0.0f);
      this.thinghappened = new SimpleTextHandler("Some Outcome~Another line", this.uiscale.ScaleX(200f), true, this.basescale, true);
      this.thinghappened.AutoCompleteParagraph();
      this.thinghappened.SetAlpha(0.0f);
      this.thinghappenedalpha = 0.0f;
      this.button = new TextButton(this.basescale, "Close", 40f);
      this.buttonalpha = 0.0f;
      this.frame = new CustomerFrame(Vector2.Zero, BaseScale: this.basescale);
      this.frame.AddMiniHeading("RESULTS");
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 0.0f, 1f, 2f);
      this.animateExtraY = new Vector2();
    }

    private void Resize()
    {
      this.framescale = new Vector2();
      foreach (ReportResult reportResult in this.questresults.Values)
      {
        this.framescale.Y += reportResult.GetSize().Y;
        this.framescale.X = Math.Max(this.framescale.X, reportResult.GetSize().X);
        this.framescale.Y += 0.5f * this.pad.Y;
      }
      if (this.questresults.Count > 0)
        this.framescale.Y -= 0.5f * this.pad.Y;
      this.framescale += 2f * this.pad;
      this.framescale.Y += this.frame.GetMiniHeadingHeight();
      this.framescale.Y += this.thinghappened.GetSize().Y + this.pad.Y;
      this.framescale.Y += this.outcomechosen.GetSize().Y;
      this.framescale.Y += this.button.GetSize_True().Y + this.pad.Y;
      this.framescale.X = Math.Max(this.framescale.X, this.forceToThisWidth);
      this.frame.Resize(this.framescale);
    }

    private void Reposition(Vector2 vscale)
    {
      Vector2 vector2 = -0.5f * vscale + this.pad;
      vector2.Y += this.frame.GetMiniHeadingHeight();
      foreach (ReportResult reportResult in this.questresults.Values)
      {
        if (reportResult.Rank == this.rankachieved)
          this.pickone.vLocation = vector2;
        reportResult.location = vector2 + 0.5f * reportResult.GetSize();
        vector2.Y += reportResult.GetSize().Y + 0.5f * this.pad.Y;
      }
      vector2.Y += 0.5f * this.pad.Y;
      this.outcomechosen.vLocation = vector2;
      this.outcomechosen.vLocation.X = -0.5f * this.outcomechosen.GetSize().X;
      vector2.Y += this.outcomechosen.GetSize().Y;
      this.thinghappened.Location = vector2;
      this.thinghappened.Location.X += 0.5f * this.thinghappened.GetSize().X;
      this.thinghappened.Location.Y += 0.5f * this.thinghappened.GetHeightOfParagraph();
      vector2.Y += this.thinghappened.GetSize().Y + this.pad.Y;
      this.button.vLocation = vector2 + 0.5f * this.button.GetSize_True();
      this.button.vLocation.X = 0.0f;
      vector2.Y += this.button.GetSize_True().Y;
    }

    public bool Animating
    {
      get => this.animating;
      set => this.animating = value;
    }

    private bool Animate(float DeltaTime)
    {
      bool flag = false;
      this.lerper.UpdateLerpHandler(DeltaTime);
      this.animateExtraY.Y = this.lerper.Value * this.pickone.GetSize().Y;
      this.frame.Resize(this.framescale + this.animateExtraY);
      this.Reposition(this.framescale + this.animateExtraY);
      this.pickone.SetAlpha(this.lerper.Value);
      if (this.lerper.IsComplete())
      {
        this.animating = false;
        flag = true;
        foreach (ReportResult reportResult in this.questresults.Values)
        {
          if (reportResult.Rank == this.rankachieved)
          {
            reportResult.Disabled = false;
            break;
          }
        }
      }
      return flag;
    }

    public void Add(ReportResultType type, ReportResultRank rank, ReportActionSet reportset)
    {
      this.questresults.Add(rank, new ReportResult(reportset, type, rank, this.basescale, true));
      this.needsresize = true;
    }

    public Vector2 GetSize()
    {
      if (this.needsresize)
      {
        this.Resize();
        this.Reposition(this.framescale);
        this.needsresize = false;
      }
      return this.framescale;
    }

    public bool UpdateQuestResultsFrame(Player player, Vector2 offset, float DeltaTime)
    {
      if (this.needsresize)
      {
        this.Resize();
        this.Reposition(this.framescale);
        this.needsresize = false;
      }
      offset += this.location + 0.5f * this.animateExtraY;
      bool flag1 = false;
      switch (this.state)
      {
        case ReportResultsFrame.ReportResultState.Start:
          if (this.animating)
          {
            this.state = ReportResultsFrame.ReportResultState.Animating;
            break;
          }
          break;
        case ReportResultsFrame.ReportResultState.Animating:
          if (this.Animate(DeltaTime))
          {
            this.state = ReportResultsFrame.ReportResultState.Waiting;
            break;
          }
          break;
        case ReportResultsFrame.ReportResultState.Waiting:
          int choice = -1;
          bool flag2 = false;
          foreach (ReportResult reportResult in this.questresults.Values)
          {
            if (reportResult.Rank >= this.rankachieved)
            {
              if (reportResult.UpdateReportResult(player, offset + this.animateExtraY, DeltaTime, out choice))
              {
                int type = (int) reportResult.Type;
                flag2 = true;
                break;
              }
            }
            else if (reportResult.UpdateReportResult(player, offset, DeltaTime, out choice))
            {
              int type = (int) reportResult.Type;
              flag2 = true;
              break;
            }
          }
          if (flag2)
          {
            Vector2 location = this.thinghappened.Location;
            this.thinghappened = new SimpleTextHandler(this.questresults[this.rankachieved].GetDescription(choice), this.uiscale.ScaleX(300f), true, this.basescale, true);
            this.thinghappened.AutoCompleteParagraph();
            this.thinghappened.SetAllColours(ColourData.Z_Cream);
            this.thinghappened.Location = location;
            this.thinghappened.Location.X = 0.0f;
            this.state = ReportResultsFrame.ReportResultState.Selected;
            this.thinghappened.SetAlpha(1f);
            this.outcomechosen.SetAlpha(1f);
            this.thinghappenedalpha = 1f;
            if (this.rankachieved <= ReportResultRank.B)
              this.thinghappened.SetAllColours(ColourData.Z_ButtonGreen);
            else if (this.rankachieved >= ReportResultRank.F)
              this.thinghappened.SetAllColours(ColourData.Z_ArrowAndTextRed);
            this.buttonalpha = 1f;
            break;
          }
          break;
        case ReportResultsFrame.ReportResultState.Selected:
          flag1 |= this.button.UpdateTextButton(player, offset + this.animateExtraY, DeltaTime);
          break;
      }
      return flag1;
    }

    public void DrawQuestResultsFrame(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location + 0.5f * this.animateExtraY;
      Vector2 vector2 = offset + this.animateExtraY;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      foreach (ReportResult reportResult in this.questresults.Values)
      {
        if (reportResult.Rank > this.rankachieved)
        {
          reportResult.DrawReportResult(spritebatch, vector2);
          reportResult.DrawDarkOverlay(spritebatch, vector2);
        }
        else if (reportResult.Rank == this.rankachieved)
        {
          reportResult.DrawReportResult(spritebatch, vector2);
          reportResult.DrawDarkOverlay(spritebatch, vector2, 1f - this.lerper.Value);
        }
        else
        {
          reportResult.DrawReportResult(spritebatch, offset);
          reportResult.DrawDarkOverlay(spritebatch, offset);
        }
      }
      this.pickone.DrawZGenericText(offset, spritebatch);
      this.outcomechosen.DrawZGenericText(vector2, spritebatch);
      this.button.DrawTextButton(vector2, this.buttonalpha, spritebatch);
      this.thinghappened.DrawSimpleTextHandler(vector2, this.thinghappenedalpha, spritebatch);
    }

    private enum ReportResultState
    {
      Start,
      Animating,
      Waiting,
      Selected,
      Count,
    }
  }
}
