// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.ReportResult
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_SummaryPopUps.EventReport.EventData;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_SummaryPopUps.EventReport
{
  internal class ReportResult
  {
    public Vector2 location;
    private float basescale;
    private UIScaleHelper scalehelper;
    private CustomerFrame frame;
    private Vector2 framescale;
    private Vector2 pad;
    private bool needsresize;
    private List<ReportRewardSelect> rewards;
    private bool disabled;
    private ReportResultRank rank;
    private ReportResultType type;

    public bool Disabled
    {
      get => this.disabled;
      set
      {
        this.disabled = value;
        this.frame.Active = !value;
      }
    }

    public ReportResultRank Rank => this.rank;

    public ReportResultType Type => this.type;

    public ReportResult(
      ReportActionSet reportset,
      ReportResultType type_,
      ReportResultRank rank_,
      float basescale_,
      bool disabled_)
    {
      this.basescale = basescale_;
      this.scalehelper = new UIScaleHelper(this.basescale);
      this.pad = this.scalehelper.DefaultBuffer;
      this.rank = rank_;
      this.type = type_;
      this.frame = new CustomerFrame(Vector2.Zero, true, this.basescale);
      this.frame.AddMiniHeading(this.rank.ToString());
      this.disabled = disabled_;
      this.rewards = new List<ReportRewardSelect>();
      for (int index = 0; index < reportset.actions.Count; ++index)
      {
        if (reportset.actions[index].rank == rank_)
          this.Add(reportset.actions[index]);
      }
      if (!this.disabled)
        return;
      this.frame.Active = false;
    }

    public string GetDescription(int choice) => this.rewards[choice].GetDescription();

    public void Add(ReportAction action)
    {
      this.rewards.Add(new ReportRewardSelect(action, this.basescale));
      this.needsresize = true;
    }

    private void SizeAndPosition()
    {
      float num = this.scalehelper.ScaleX(20f);
      foreach (ReportRewardSelect reward in this.rewards)
      {
        this.framescale.Y += reward.GetSize().Y + 0.5f * this.pad.Y;
        this.framescale.X = Math.Max(this.framescale.X, reward.GetSize().X);
      }
      this.framescale.Y -= 0.5f * this.pad.Y;
      this.framescale += 1f * this.pad;
      this.framescale.X += num;
      this.frame.Resize(this.framescale);
      Vector2 vector2 = -0.5f * this.framescale + 0.5f * this.pad;
      vector2.X += num;
      foreach (ReportRewardSelect reward in this.rewards)
      {
        reward.location = vector2 + 0.5f * reward.GetSize();
        vector2.Y += reward.GetSize().Y + 0.5f * this.pad.Y;
      }
    }

    public Vector2 GetSize()
    {
      if (this.needsresize)
      {
        this.needsresize = false;
        this.SizeAndPosition();
      }
      return this.framescale;
    }

    public bool UpdateReportResult(Player player, Vector2 offset, float DeltaTime, out int choice)
    {
      if (this.needsresize)
      {
        this.needsresize = false;
        this.SizeAndPosition();
      }
      offset += this.location;
      bool flag = false;
      choice = -1;
      if (!this.disabled)
      {
        for (int index = 0; index < this.rewards.Count; ++index)
        {
          if (this.rewards[index].UpdateReportRewardSelect(player, offset, DeltaTime))
          {
            choice = index;
            flag = true;
            using (List<ReportRewardSelect>.Enumerator enumerator = this.rewards.GetEnumerator())
            {
              while (enumerator.MoveNext())
                enumerator.Current.hidetextbutton = true;
              break;
            }
          }
        }
      }
      return flag;
    }

    public void DrawReportResult(SpriteBatch spritebatch, Vector2 offset)
    {
      offset += this.location;
      this.frame.DrawCustomerFrame(offset, spritebatch);
      foreach (ReportRewardSelect reward in this.rewards)
        reward.DrawReportRewardSelect(spritebatch, offset);
    }

    public void DrawDarkOverlay(SpriteBatch spritebatch, Vector2 offset, float alpha = 1f)
    {
      offset += this.location;
      if (!this.disabled)
        return;
      this.frame.DrawDarkOverlay(offset, spritebatch, alpha);
    }
  }
}
