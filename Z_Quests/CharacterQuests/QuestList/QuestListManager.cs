// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.CharacterQuests.QuestList.QuestListManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;

namespace TinyZoo.Z_Quests.CharacterQuests.QuestList
{
  internal class QuestListManager
  {
    private List<HeroQuestsDisplayEntry> QuestEntries;
    private SimpleArrowPageButtons arrowbuttons;
    private Vector2 Size;
    private Vector2 Location;
    public HeroCharacter herocharacter;
    public HeroProgressStatus RefHeroProgress;
    private bool BlockClicks;
    public bool HasWillShortcutOnPanelOpen;
    private int CurrentPage;
    private int MaxPages;

    public QuestListManager(
      Player player,
      float BaseScale,
      HeroCharacter ThisHeroIsDoingANewTask = HeroCharacter.Count,
      BigBrownPanel panel = null)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.CurrentPage = 0;
      this.BlockClicks = true;
      this.QuestEntries = new List<HeroQuestsDisplayEntry>();
      float Width = 405f;
      this.Size.X = Width * BaseScale;
      int num = 5;
      this.CurrentPage = 0;
      List<HeroProgressStatus> questsInDisplayOrder = player.heroquestprogress.GetActiveQuestsInDisplayOrder();
      for (int index = 0; index < questsInDisplayOrder.Count; ++index)
      {
        this.QuestEntries.Add(new HeroQuestsDisplayEntry(BaseScale, questsInDisplayOrder[index], Width, player, ThisHeroIsDoingANewTask, panel));
        this.QuestEntries[index].Location.Y = this.QuestEntries[index].GetSize().Y * ((float) (index % num) + 0.5f);
        this.QuestEntries[index].Location.Y += 10f * BaseScale * (float) (index % num);
        if ((double) this.Size.Y < (double) this.QuestEntries[index].Location.Y)
          this.Size.Y = this.QuestEntries[index].Location.Y;
        this.HasWillShortcutOnPanelOpen |= this.QuestEntries[index].HasWillShortcutOnPanelOpen;
        this.QuestEntries[index].Page = index / num;
      }
      this.MaxPages = (int) Math.Ceiling((double) this.QuestEntries.Count / 5.0);
      if (this.QuestEntries.Count > 0)
        this.Size.Y += this.QuestEntries[0].GetSize().Y * 0.5f;
      this.CurrentPage = 0;
      if (this.MaxPages > 1)
      {
        this.Size.Y += defaultBuffer.Y;
        this.arrowbuttons = new SimpleArrowPageButtons(BaseScale);
        this.arrowbuttons.Location = this.Size;
        this.arrowbuttons.Location.X -= this.arrowbuttons.GetSize().X * 0.5f;
        this.arrowbuttons.Location.Y += this.arrowbuttons.GetSize().Y * 0.5f;
        this.Size.Y += this.arrowbuttons.GetSize().Y;
        this.Size.Y += defaultBuffer.Y * 0.5f;
      }
      Vector2 vector2 = -this.Size * 0.5f;
      if (this.arrowbuttons != null)
        this.arrowbuttons.Location += vector2;
      for (int index = 0; index < this.QuestEntries.Count; ++index)
        this.QuestEntries[index].Location.Y += vector2.Y;
    }

    public Vector2 GetSize() => this.Size;

    public HeroQuestDescription GetFirstWillSHortCutOnOpen()
    {
      for (int index = 0; index < this.QuestEntries.Count; ++index)
      {
        if (this.QuestEntries[index].HasWillShortcutOnPanelOpen)
          return this.QuestEntries[index].quest;
      }
      return (HeroQuestDescription) null;
    }

    public HeroQuestDescription UpdateQuestListManager(
      float DeltaTime,
      Player player,
      Vector2 Offset,
      out bool GoToHistory)
    {
      GoToHistory = false;
      HeroQuestDescription questDescription = (HeroQuestDescription) null;
      Offset += this.Location;
      if (this.arrowbuttons != null)
      {
        int num = this.arrowbuttons.UpdateSimpleArrowPageButtons(DeltaTime, player, Offset);
        if (num != 0)
        {
          if (num > 0)
          {
            if (this.CurrentPage == 0 || this.CurrentPage < this.MaxPages - 1)
            {
              ++this.CurrentPage;
              this.arrowbuttons.SetAsDisabled(true, false);
              if (this.CurrentPage == this.MaxPages - 1)
                this.arrowbuttons.SetAsDisabled(false, true);
            }
          }
          else if (this.CurrentPage > 0)
          {
            --this.CurrentPage;
            this.arrowbuttons.SetAsDisabled(false, false);
            if (this.CurrentPage == 0)
              this.arrowbuttons.SetAsDisabled(true, true);
          }
        }
      }
      for (int index = 0; index < this.QuestEntries.Count; ++index)
      {
        if (this.QuestEntries[index].Page == this.CurrentPage && this.QuestEntries[index].UpdateHeroQuestsDisplayEntry(Offset, DeltaTime, player) && !this.BlockClicks)
        {
          this.RefHeroProgress = this.QuestEntries[index].RefHeroProgress;
          this.herocharacter = this.QuestEntries[index].herocharacter;
          questDescription = this.QuestEntries[index].quest;
          if (questDescription == null)
            GoToHistory = true;
        }
      }
      this.BlockClicks = false;
      return questDescription;
    }

    public void DrawQuestListManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      for (int index = 0; index < this.QuestEntries.Count; ++index)
      {
        if (this.QuestEntries[index].Page == this.CurrentPage)
          this.QuestEntries[index].DrawHeroQuestsDisplayEntry(Offset, spritebatch);
      }
      if (this.arrowbuttons == null)
        return;
      this.arrowbuttons.DrawSimpleArrowPageButtons(Offset, spritebatch);
    }
  }
}
