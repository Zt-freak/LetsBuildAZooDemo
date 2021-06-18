// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.QuestComplete.QuestCompleteManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.GenericUI;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_HUD;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_SummaryPopUps.EventReport;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_Quests.QuestComplete
{
  internal class QuestCompleteManager
  {
    private CustomerFrame customerframe;
    private float BaseScale;
    private Vector2 Location;
    private AnimalInFrame animalinframe;
    private SimpleTextHandler headerparagraph;
    private SimpleTextHandler paragraph;
    private TextButton Confirm;
    private HeroProgressStatus RefHeroProgress;
    private HeroQuestDescription refquest;
    public HeroCharacter hero;
    private StampPrint print;
    private ReportStamp reportStamp;
    private Vector2 stampLoc;
    private bool AllowClaimNow;
    public bool ScrubHeroQuestsOnExit;

    public QuestCompleteManager(
      HeroQuestDescription _quest,
      Vector2 VScale,
      float _BaseScale,
      HeroCharacter _hero,
      HeroProgressStatus _RefHeroProgress,
      Player player,
      bool IsForClaim,
      bool ShowCompleteOnly = false)
    {
      this.hero = _hero;
      this.refquest = _quest;
      if (_quest.ThoughtBubbleHold != "" & IsForClaim)
        NotificationBubblePopUp.StaticForceExitCurrentBubble = true;
      this.RefHeroProgress = _RefHeroProgress;
      this.BaseScale = _BaseScale;
      UIScaleHelper uiScaleHelper = new UIScaleHelper(this.BaseScale);
      Vector2 defaultBuffer = uiScaleHelper.DefaultBuffer;
      Vector2 _VSCale = Vector2.Zero + defaultBuffer;
      _VSCale.Y += defaultBuffer.Y;
      this.animalinframe = new AnimalInFrame(_RefHeroProgress.RefHeroQuestPack.herocharacter <= HeroCharacter.Count ? HeroQuestData.GetHeroQuestData(this.hero).ThisCharacterAnimal : HeroQuestData.GetHeroCharacterToAnimalType(_RefHeroProgress.RefHeroQuestPack.herocharacter), AnimalType.None, TargetSize: (50f * this.BaseScale), FrameEdgeBuffer: (6f * this.BaseScale), BaseScale: (this.BaseScale * 2f));
      this.animalinframe.Location.Y = _VSCale.Y;
      this.animalinframe.Location.Y += this.animalinframe.GetSize().Y * 0.5f;
      _VSCale.Y += this.animalinframe.GetSize().Y;
      _VSCale.Y += defaultBuffer.Y;
      float width_ = uiScaleHelper.ScaleX(350f);
      string TextToWrite1 = "TASK COMPLETE:~" + this.refquest.GetQuestHeading();
      string TextToWrite2 = this.RefHeroProgress.GetNextQustUnlockText(player);
      if (ShowCompleteOnly && this.refquest.GetQuestCompleteText() != "")
        TextToWrite2 = this.refquest.GetQuestCompleteText();
      this.headerparagraph = new SimpleTextHandler(TextToWrite1, width_, true, this.BaseScale, true, true);
      this.headerparagraph.SetAllColours(ColourData.Z_DarkTextGray);
      this.headerparagraph.Location.Y = _VSCale.Y;
      this.headerparagraph.Location.Y += this.headerparagraph.GetHeightOfOneLine() * 0.5f;
      _VSCale.Y += this.headerparagraph.GetHeightOfParagraph();
      _VSCale.Y += defaultBuffer.Y;
      if (this.refquest.HasReward)
        TextToWrite2 = TextToWrite2 + "~~Reward: " + this.refquest.GetRewardString();
      if (this.refquest.UID == 0 && this.refquest.herocharacter == HeroCharacter.Zoo_Austrailia)
        player.Stats.TutorialsComplete[29] = true;
      this.paragraph = new SimpleTextHandler(TextToWrite2, width_, true, this.BaseScale, AutoComplete: true);
      this.paragraph.SetAllColours(ColourData.Z_DarkTextGray);
      this.paragraph.AutoCompleteParagraph();
      this.paragraph.Location.Y = _VSCale.Y;
      this.paragraph.Location.Y += this.paragraph.GetHeightOfOneLine() * 0.5f;
      _VSCale.Y += this.paragraph.GetHeightOfParagraph();
      _VSCale.Y += defaultBuffer.Y;
      _VSCale.X += width_;
      _VSCale.X += defaultBuffer.X;
      if (IsForClaim)
      {
        this.Confirm = new TextButton(this.BaseScale, SEngine.Localization.Localization.GetText(936), 50f);
        this.Confirm.SetButtonColour(BTNColour.Task_Gold);
        this.Confirm.vLocation.Y = _VSCale.Y;
        this.Confirm.AddControllerButton(ControllerButton.XboxA);
        this.Confirm.vLocation.Y += this.Confirm.GetSize_True().Y * 0.5f;
        _VSCale.Y += this.Confirm.GetSize_True().Y;
        _VSCale.Y += defaultBuffer.Y;
      }
      if (_quest.tutorialquestspecial != TutorialQuestSpecial.None)
      {
        switch (_quest.tutorialquestspecial)
        {
          case TutorialQuestSpecial.GoToManagementOffice:
            PointOffScreenManager.RemovePointer(SpecialEventType.GoToQuestBuilding, _quest.tutorialquestspecial);
            break;
          case TutorialQuestSpecial.HasCompletedEnoughQuestsToStartDay:
            GameFlags.HasCompletedEnoughQuestsToStartDay = true;
            FeatureFlags.BlockAllUI = false;
            FeatureFlags.ForceAllowBuild = false;
            FeatureFlags.ForceAllowWorldMap = false;
            FeatureFlags.FlashTrade = false;
            FeatureFlags.ForceAllowControlHint = false;
            break;
          case TutorialQuestSpecial.AllowWorldMap:
            FeatureFlags.FlashBuildFromNotificationTrack = false;
            break;
          case TutorialQuestSpecial.UnlockAbilityToBuyLand:
            if (FeatureFlags.BlockBuyLand)
            {
              FeatureFlags.BlockBuyLand = false;
              Z_GameFlags.ScrubForSaleSigns = true;
              break;
            }
            break;
        }
      }
      _VSCale.Y += defaultBuffer.Y;
      this.customerframe = new CustomerFrame(_VSCale, CustomerFrameColors.PaperWithPawPrint, this.BaseScale);
      Vector2 vector2 = -this.customerframe.VSCale * 0.5f;
      this.headerparagraph.Location.Y += vector2.Y;
      this.paragraph.Location.Y += vector2.Y;
      this.Confirm.vLocation.Y += vector2.Y;
      this.animalinframe.Location.Y += vector2.Y;
      this.stampLoc = new Vector2(this.customerframe.VSCale.X, -this.customerframe.VSCale.Y) * 0.5f;
      this.stampLoc += uiScaleHelper.ScaleVector2(new Vector2(-70f, 60f));
      this.DoStampEffect();
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    private void DoStampEffect()
    {
      this.reportStamp = new ReportStamp(this.BaseScale);
      this.reportStamp.location = this.stampLoc;
      this.reportStamp.StartAnimation();
    }

    private void AddStampPrintNow()
    {
      this.print = new StampPrint(this.BaseScale, StampPrintType.TaskComplete);
      this.print.location = this.stampLoc;
      this.AllowClaimNow = true;
    }

    public bool UpdateQuestCompleteManager(Vector2 Offset, Player player, float DeltaTime)
    {
      if (this.reportStamp != null && this.reportStamp.UpdateReportStamp(player, Offset, DeltaTime))
        this.AddStampPrintNow();
      if (this.Confirm != null && this.AllowClaimNow && this.Confirm.UpdateTextButton(player, Offset, DeltaTime))
      {
        int publicity1 = PublicityCalculator.CalculatePublicity(player);
        if (this.RefHeroProgress.TryAndCompleteQuest(player))
        {
          if (this.refquest.HasReward)
          {
            this.refquest.ProcessReward(player);
            if (this.refquest.herocharacter > HeroCharacter.Count)
              player.heroquestprogress.RemoveDaily(this.refquest);
          }
          ZHudManager.zquestpins.UnPinQuest(this.refquest, player);
          this.RefHeroProgress.TryAndUnlockNextQuest(player);
          this.ScrubHeroQuestsOnExit = true;
          if (this.refquest.tutorialquestspecial != TutorialQuestSpecial.HasCompletedEnoughQuestsToStartDay)
            QuestScrubber.ScrubOnCompleteHeroQuest(player);
          QuestScrubber.CheckCompletionAlerts(player);
          int publicity2 = PublicityCalculator.CalculatePublicity(player);
          NotificationBubbleManager.QuickAddNotification((float) publicity1, (float) publicity2, BubbleMainType.Publicity);
          return true;
        }
      }
      return false;
    }

    public void DrawQuestCompleteManager(Vector2 Offset, SpriteBatch spritebatch)
    {
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.animalinframe.DrawAnimalInFrame(Offset, spritebatch);
      this.headerparagraph.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      this.paragraph.DrawSimpleTextHandler(Offset, 1f, spritebatch);
      if (this.Confirm != null && this.AllowClaimNow)
        this.Confirm.DrawTextButton(Offset, 1f, spritebatch);
      if (this.print != null)
        this.print.DrawStampPrint(Offset, spritebatch);
      if (this.reportStamp == null)
        return;
      this.reportStamp.DrawReportStamp(spritebatch, Offset);
    }
  }
}
