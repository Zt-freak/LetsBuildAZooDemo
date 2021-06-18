// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.CharacterQuests.HeroQuestPanelManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Quests.Advice;
using TinyZoo.Z_Quests.CharacterQuests.QuestList;
using TinyZoo.Z_Quests.QuestComplete;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;

namespace TinyZoo.Z_Quests.CharacterQuests
{
  internal class HeroQuestPanelManager
  {
    private BigBrownPanel brownpanel;
    private float BaseScale;
    public Vector2 Location;
    private QuestListManager questlist;
    private NarrativeInfoManager narrative;
    private QuestCompleteManager questcomplete;
    private bool WasForcedOpen;
    private HeroQuestDescription thisquest;
    private LerpHandler_Float lerper;
    private bool TempBlockClose;
    public HeroQuestDescription forceToThisQuest_TempForReshow;
    private bool NewtaskForced;

    public HeroQuestPanelManager(
      Player player,
      HeroQuestDescription forceToThisQuest = null,
      bool forcedQuest_IsForQuestComplete = false)
    {
      if (FeatureFlags.BlockBuyLand && this.thisquest != null && this.thisquest.tutorialquestspecial == TutorialQuestSpecial.UnlockAbilityToBuyLand)
      {
        FeatureFlags.BlockBuyLand = false;
        Z_GameFlags.ScrubForSaleSigns = true;
      }
      player.player.touchinput.ReleaseTapArray[0].X = -1000f;
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      if (forceToThisQuest == null && !Z_GameFlags.HasStartedFirstDay)
      {
        Z_GameFlags.HasViewedTasks = true;
        QuestScrubber.ScrubOnOpeningTaskList(player);
      }
      this.BaseScale = Z_GameFlags.GetBaseScaleForUI();
      string addHeaderText = "TASKS";
      if (forceToThisQuest != null && !forcedQuest_IsForQuestComplete)
      {
        this.NewtaskForced = true;
        addHeaderText = "NEW TASK";
        if (forceToThisQuest.heroquesttype == HEROQUESTTYPE.OpenForBusiness)
          Z_GameFlags.QuestToOpenZooStarted = true;
        if (forceToThisQuest.heroquesttype == HEROQUESTTYPE.CompleteZooTrade && forceToThisQuest.UID == 0)
          FeatureFlags.BlockBuild = true;
      }
      this.brownpanel = new BigBrownPanel(new Vector2(100f, 100f), true, addHeaderText, this.BaseScale, true);
      this.questlist = new QuestListManager(player, this.BaseScale, panel: this.brownpanel);
      this.Location = new Vector2(512f, 384f);
      this.brownpanel.Finalize(this.questlist.GetSize());
      this.brownpanel.HasPreviousButton = false;
      if (forceToThisQuest != null)
      {
        this.WasForcedOpen = true;
        if (forcedQuest_IsForQuestComplete)
        {
          HeroProgressStatus progressFromQuest = player.heroquestprogress.GetHeroProgressFromQuest(forceToThisQuest);
          this.questcomplete = new QuestCompleteManager(forceToThisQuest, this.questlist.GetSize(), this.BaseScale, progressFromQuest.RefHeroQuestPack.herocharacter, progressFromQuest, player, true);
          this.brownpanel.BlockCloseButton = true;
          Vector2 vector2 = this.brownpanel.AddExtraBorderFrame();
          this.brownpanel.Finalize(this.questcomplete.GetSize(), vector2.X);
        }
        else
        {
          HeroProgressStatus progressFromQuest = player.heroquestprogress.GetHeroProgressFromQuest(forceToThisQuest);
          this.narrative = new NarrativeInfoManager(forceToThisQuest, this.questlist.GetSize(), this.BaseScale, progressFromQuest.RefHeroQuestPack.herocharacter, progressFromQuest, player, true);
          this.brownpanel.Finalize(this.narrative.GetSize());
        }
        this.thisquest = forceToThisQuest;
      }
      if (this.questcomplete != null || !this.questlist.HasWillShortcutOnPanelOpen)
        return;
      if (forceToThisQuest != null)
        this.forceToThisQuest_TempForReshow = forceToThisQuest;
      HeroQuestDescription willShortCutOnOpen = this.questlist.GetFirstWillSHortCutOnOpen();
      if (willShortCutOnOpen == null)
        return;
      this.thisquest = willShortCutOnOpen;
      HeroProgressStatus progressFromQuest1 = player.heroquestprogress.GetHeroProgressFromQuest(this.thisquest);
      this.questcomplete = new QuestCompleteManager(this.thisquest, this.questlist.GetSize(), this.BaseScale, progressFromQuest1.RefHeroQuestPack.herocharacter, progressFromQuest1, player, true, true);
      if (!this.brownpanel.BlockCloseButton)
      {
        this.TempBlockClose = true;
        this.brownpanel.BlockCloseButton = true;
        FeatureFlags.BlockMouseOverOnBuildBar = true;
      }
      Vector2 vector2_1 = this.brownpanel.AddExtraBorderFrame();
      this.brownpanel.SetNewHeading("TASK COMPLETE");
      this.brownpanel.Finalize(this.questcomplete.GetSize(), vector2_1.X);
    }

    public bool CheckMouseOver(Player player) => this.brownpanel.CheckMouseOver(player, this.Location);

    public bool GetBlockExitFroTimboTutorial() => this.thisquest != null && this.thisquest.herocharacter == HeroCharacter.Zoo_Austrailia && (this.thisquest.heroquesttype == HEROQUESTTYPE.CompleteZooTrade && this.NewtaskForced);

    public bool UpdateHeroQuestPanelManager(
      float DeltaTime,
      Player player,
      ref bool WillClearInput)
    {
      this.lerper.UpdateLerpHandler(DeltaTime);
      if ((double) this.lerper.Value != 0.0)
        return false;
      if (!this.brownpanel.BlockCloseButton && this.brownpanel.close != null && !this.brownpanel.CheckCollision(player.inputmap.PointerLocation, this.Location))
        WillClearInput = false;
      if (WillClearInput && (this.GetBlockExitFroTimboTutorial() || this.TempBlockClose || this.brownpanel.BlockCloseButton))
        WillClearInput = false;
      if (this.questlist != null)
      {
        bool GoToHistory = false;
        HeroQuestDescription _quest = (HeroQuestDescription) null;
        if (this.narrative == null && this.questcomplete == null)
          _quest = this.questlist.UpdateQuestListManager(DeltaTime, player, this.Location, out GoToHistory);
        if (_quest != null | GoToHistory)
        {
          this.narrative = (NarrativeInfoManager) null;
          this.narrative = new NarrativeInfoManager(_quest, this.questlist.GetSize(), this.BaseScale, this.questlist.herocharacter, this.questlist.RefHeroProgress, player);
          this.brownpanel.SetNewHeading("CURRENT TASK");
          if (this.brownpanel.BlockCloseButton && !this.TempBlockClose)
          {
            if (!player.Stats.TutorialsComplete[28])
            {
              Z_GameFlags.BusStartsOnSCreenEdge = true;
              player.Stats.TutorialsComplete[28] = true;
              this.brownpanel.BlockCloseButton = false;
              FeatureFlags.BlockMouseOverOnBuildBar = false;
              FeatureFlags.BlockBackFromMainBar = false;
              FeatureFlags.ForceAllowBuild = true;
              FeatureFlags.ForceAllowControlHint = true;
              Z_GameFlags.TopBarIsBlockedForTutorial = false;
            }
          }
          else if (this.narrative.IsComplete && !this.brownpanel.BlockCloseButton)
          {
            this.brownpanel.BlockCloseButton = true;
            this.TempBlockClose = true;
            FeatureFlags.BlockMouseOverOnBuildBar = true;
          }
          if (!this.narrative.IsComplete)
            this.brownpanel.HasPreviousButton = true;
          this.brownpanel.Finalize(this.narrative.GetSize());
        }
      }
      if (this.questcomplete != null && this.questcomplete.UpdateQuestCompleteManager(this.Location, player, DeltaTime))
      {
        this.narrative = (NarrativeInfoManager) null;
        if (this.TempBlockClose)
        {
          this.TempBlockClose = false;
          this.brownpanel.BlockCloseButton = false;
          FeatureFlags.BlockMouseOverOnBuildBar = false;
        }
        this.questlist = new QuestListManager(player, this.BaseScale, this.questcomplete.hero, this.brownpanel);
        this.brownpanel.RemoveExtraBorderFrame();
        this.brownpanel.Finalize(this.questlist.GetSize());
        this.questcomplete = (QuestCompleteManager) null;
        this.brownpanel.SetNewHeading("NEW TASK");
        if (this.WasForcedOpen)
        {
          if (this.thisquest.ThoughtBubbleHold != "")
          {
            int tutorialquestspecial = (int) this.thisquest.tutorialquestspecial;
            NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("TASK", this.thisquest.ThoughtBubbleHold, true));
          }
          return true;
        }
      }
      if (this.narrative != null)
      {
        if (this.narrative.UpdateNarrativeInfoManager(this.Location, player, DeltaTime))
        {
          this.questcomplete = new QuestCompleteManager(this.narrative.quest, this.narrative.GetSize(), this.BaseScale, this.narrative.herocharacter, this.narrative.RefHeroProgress, player, true);
          this.brownpanel.HasPreviousButton = false;
          if (this.WasForcedOpen)
          {
            if (this.thisquest.ThoughtBubbleHold != "")
              NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("TASK", this.thisquest.ThoughtBubbleHold, true, this.thisquest.AlsoDrawThistileTypeOnComplete));
            return true;
          }
          this.brownpanel.BlockCloseButton = true;
          Vector2 vector2 = this.brownpanel.AddExtraBorderFrame();
          this.brownpanel.Finalize(this.questcomplete.GetSize(), vector2.X);
        }
        if (this.brownpanel.UpdatePanelpreviousButton(player, DeltaTime, this.Location))
        {
          this.questlist = new QuestListManager(player, this.BaseScale, panel: this.brownpanel);
          this.brownpanel.HasPreviousButton = false;
          this.brownpanel.Finalize(this.questlist.GetSize());
          this.narrative = (NarrativeInfoManager) null;
          this.brownpanel.SetNewHeading("TASKS");
        }
      }
      if (FeatureFlags.BlockBuyLand && this.thisquest != null && (this.thisquest.heroquesttype == HEROQUESTTYPE.ZooSize && this.thisquest.herocharacter == HeroCharacter.Mayor))
        WillClearInput = true;
      this.brownpanel.UpdateDragger(player, ref this.Location, DeltaTime);
      int num = this.brownpanel.UpdatePanelCloseButton(player, DeltaTime, this.Location) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.TryAndClose(player);
      return num != 0;
    }

    public bool TryAndClose(Player player)
    {
      if (this.thisquest != null && this.thisquest.tutorialquestspecial == TutorialQuestSpecial.UnlockAbilityToBuyLand && (this.WasForcedOpen && !this.thisquest.CheckIsComplete(player)))
      {
        FeatureFlags.BlockBuyLand = false;
        Z_GameFlags.ScrubForSaleSigns = true;
        OverWorldManager.zoopopupHolder.CreateFeatureReveal(FeatureUnlockDisplayType.LandExpansion);
      }
      if (this.thisquest != null && this.thisquest.tutorialquestspecial == TutorialQuestSpecial.GoToManagementOffice && (!player.heroquestprogress.HasThisQuestBeenCompleted(this.thisquest) && this.thisquest.tutorialquestspecial == TutorialQuestSpecial.GoToManagementOffice))
        FeatureFlags.BlockBackFromMainBar = true;
      if (this.thisquest != null && this.thisquest.ThoughtBubbleHold != "" && (this.thisquest != null && !player.heroquestprogress.HasThisQuestBeenCompleted(this.thisquest)))
      {
        if (this.thisquest.tutorialquestspecial == TutorialQuestSpecial.GoToManagementOffice)
          FeatureFlags.BlockBackFromMainBar = true;
        NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("TASK", this.thisquest.ThoughtBubbleHold, true, this.thisquest.AlsoDrawThistileTypeOnComplete));
      }
      if (this.thisquest != null && this.thisquest.tutorialquestspecial != TutorialQuestSpecial.None && !player.heroquestprogress.ProgressArray[(int) this.thisquest.herocharacter].HasCompletedThis(this.thisquest))
      {
        switch (this.thisquest.tutorialquestspecial)
        {
          case TutorialQuestSpecial.GoToManagementOffice:
            Z_GameFlags.HasCompleteQuestsToView = true;
            break;
          case TutorialQuestSpecial.HasCompletedEnoughQuestsToStartDay:
            FeatureFlags.ForceAllowWorldMap = true;
            break;
          case TutorialQuestSpecial.AllowWorldMap:
            FeatureFlags.FlashBuildFromNotificationTrack = false;
            break;
        }
      }
      return true;
    }

    public void DrawHeroQuestPanelManager(SpriteBatch spritebatch)
    {
      Vector2 vector2 = this.Location + new Vector2(0.0f, this.lerper.Value * 768f);
      this.brownpanel.DrawBigBrownPanel(vector2);
      if (this.questcomplete != null)
        this.questcomplete.DrawQuestCompleteManager(vector2, spritebatch);
      else if (this.narrative != null)
        this.narrative.DrawNarrativeInfoManager(vector2, spritebatch);
      else
        this.questlist.DrawQuestListManager(vector2, spritebatch);
    }
  }
}
