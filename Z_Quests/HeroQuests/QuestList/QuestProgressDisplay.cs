// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.HeroQuests.QuestList.QuestProgressDisplay
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.Z_Quests.HeroQuests.QuestList
{
  internal class QuestProgressDisplay
  {
    private string Progress;
    private string Body;
    public NameAndQuest nameandquest;
    private SatisfactionBar bar;
    public Vector2 Location;
    private Vector2 size;

    public QuestProgressDisplay(
      HeroProgressStatus heroprogress,
      Player player,
      float BaseScale,
      bool IsSummaryScreen = false)
    {
      bool _UseBigFont = IsSummaryScreen;
      HeroQuestDescription activeQuest = heroprogress.GetActiveQuest();
      int ProgressStart = 0;
      int Progressend = 0;
      if (activeQuest == null)
      {
        this.Body = heroprogress.GetCompletetedQuests() != heroprogress.RefHeroQuestPack.heroquests.Count ? "There are no tasks from this person right now" : "All tasks from this person are complete.";
      }
      else
      {
        string objectiveHeading = activeQuest.GetObjectiveHeading(out this.Progress, player, out ProgressStart, out Progressend);
        this.Body = !IsSummaryScreen ? activeQuest.GetTaskShortSummary() : objectiveHeading;
      }
      this.nameandquest = new NameAndQuest(this.Body + "~" + this.Progress, BaseScale, _UseBigFont, 1f);
      float FullPercent = 0.0f;
      if (Progressend != 0)
        FullPercent = (float) ProgressStart / (float) Progressend;
      if (activeQuest != null)
      {
        this.bar = new SatisfactionBar(FullPercent, BaseScale);
        this.bar.vLocation.X = this.bar.GetSize().X * 0.5f;
        this.bar.vLocation.Y = this.nameandquest.GetSize().Y;
        this.bar.vLocation.Y += this.bar.GetSize().Y * 0.5f;
      }
      this.size.X = this.nameandquest.GetSize().X;
      if (this.bar != null)
        this.size.X = Math.Max(this.bar.GetSize().X, this.size.X);
      this.size.Y = this.nameandquest.GetSize().Y;
      if (this.bar == null)
        return;
      this.size.Y += this.bar.GetSize().Y;
    }

    public Vector2 GetSize() => this.size;

    public void UpdateQuestProgressDisplay()
    {
    }

    public void DrawQuestProgressDisplay(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.nameandquest.DrawNameAndQuest(Offset, spritebatch);
      if (this.bar == null)
        return;
      this.bar.DrawSatisfactionBar(Offset + this.nameandquest.vLocation, spritebatch);
    }
  }
}
