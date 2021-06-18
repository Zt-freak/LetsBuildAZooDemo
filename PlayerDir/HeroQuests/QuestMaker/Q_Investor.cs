// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_Investor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Quests;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_Investor
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.Investor, HeroCharacter.Investor);
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      questDescription1.SetUpUnlockAtStartOfGame();
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
      questDescription2.ThisQuestHeading = StringID.INVESTOR1_Heading;
      questDescription2.ThisQuestDescriptin = StringID.INVESTOR1_Desc;
      questDescription2.ThisQuestCompleteText = StringID.INVESTOR1_Complete;
      questDescription2.SetUpViewTasks();
      questDescription2.WillAutoPopOnUnlock = false;
      questDescription2.AlsoDrawThistileTypeOnComplete = NoticicationExtraIcon.ManagementOffice;
      questDescription2.tutorialquestspecial = TutorialQuestSpecial.GoToManagementOffice;
      questDescription2.WillShortcutOnPanelOpenIfomplete = true;
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.WillAutoPopOnUnlock = true;
      questDescription2.WillAutoPopOnComplete = true;
      questDescription2.TaskShortSummary = StringID.INVESTOR1_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription2);
      HeroQuestDescription questDescription3 = new HeroQuestDescription();
      questDescription3.SetUpUnlockAtStartOfGame();
      HeroQuestDescription questDescription4 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
      questDescription4.ThisQuestHeading = StringID.INVESTOR2_Heading;
      questDescription4.ThisQuestDescriptin = StringID.INVESTOR2_Desc;
      questDescription4.ThisQuestCompleteText = StringID.INVESTOR2_Complete;
      questDescription4.TaskShortSummary = StringID.INVESTOR2_TaskSummary;
      questDescription4.WillAutoPopOnUnlock = false;
      questDescription4.SetUpBuildPen(1);
      questDescription4.tutorialquestspecial = TutorialQuestSpecial.AllowWorldMap;
      questDescription4.WillShortcutOnPanelOpenIfomplete = true;
      questDescription4.UnlockThisQuestCriteria.Add(questDescription3);
      heroQuestPack.heroquests.Add(questDescription4);
      HeroQuestDescription questDescription5 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
      questDescription5.ThisQuestHeading = StringID.INVESTOR3_Heading;
      questDescription5.ThisQuestDescriptin = StringID.INVESTOR3_Desc;
      questDescription5.SetUpOpenForBusiness();
      HeroQuestDescription questDescription6 = new HeroQuestDescription();
      questDescription6.SetUpOtherHeroCharacterprogress(HeroCharacter.Zoo_Austrailia, 1);
      questDescription5.UnlockThisQuestCriteria.Add(questDescription6);
      questDescription5.WillAutoPopOnComplete = true;
      questDescription5.TaskShortSummary = StringID.INVESTOR3_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription5);
      if (Z_DebugFlags.IsBetaVersion)
      {
        HeroQuestDescription questDescription7 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription7.ThisQuestHeading = StringID.INVESTORB1_Heading;
        questDescription7.ThisQuestDescriptin = StringID.INVESTORB1_Desc;
        questDescription7.SetUpVisitorTarget(false, 25);
        HeroQuestDescription questDescription8 = new HeroQuestDescription();
        questDescription8.SetUpUnlockAtStartOfGame();
        questDescription7.UnlockThisQuestCriteria.Add(questDescription8);
        questDescription7.SetReward(new RewardPack(REWARDTYPE.Money, 1500));
        questDescription7.TaskShortSummary = StringID.INVESTORB1_TaskSummary;
        heroQuestPack.heroquests.Add(questDescription7);
        questDescription7.WillAutoPopOnUnlock = true;
        questDescription7.WillAutoPin = true;
        HeroQuestDescription questDescription9 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription9.ThisQuestHeading = StringID.INVESTOR4_Heading;
        questDescription9.ThisQuestDescriptin = StringID.INVESTOR4_Desc;
        questDescription9.SetUpVisitorTarget(false, 100);
        HeroQuestDescription questDescription10 = new HeroQuestDescription();
        questDescription10.SetUpUnlockAtStartOfGame();
        questDescription9.UnlockThisQuestCriteria.Add(questDescription10);
        questDescription9.SetReward(new RewardPack(REWARDTYPE.Money, 1500));
        questDescription9.TaskShortSummary = StringID.INVESTOR4_TaskSummary;
        heroQuestPack.heroquests.Add(questDescription9);
        questDescription9.WillAutoPin = true;
        HeroQuestDescription questDescription11 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription11.ThisQuestHeading = StringID.INVESTORBETA_Heading;
        questDescription11.ThisQuestDescriptin = StringID.INVESTORBETA_Desc;
        questDescription11.SetUpVisitorTarget(false, 200);
        HeroQuestDescription questDescription12 = new HeroQuestDescription();
        questDescription12.SetUpUnlockAtStartOfGame();
        questDescription11.UnlockThisQuestCriteria.Add(questDescription12);
        questDescription11.SetReward(new RewardPack(REWARDTYPE.Money, 3000));
        questDescription11.TaskShortSummary = StringID.INVESTORBETA_TaskSummary;
        heroQuestPack.heroquests.Add(questDescription11);
        questDescription11.WillAutoPin = true;
      }
      else
      {
        HeroQuestDescription questDescription7 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription7.ThisQuestHeading = StringID.INVESTOR4_Heading;
        questDescription7.ThisQuestDescriptin = StringID.INVESTOR4_Desc;
        questDescription7.SetUpVisitorTarget(false, 100);
        HeroQuestDescription questDescription8 = new HeroQuestDescription();
        questDescription8.SetUpUnlockAtStartOfGame();
        questDescription7.UnlockThisQuestCriteria.Add(questDescription8);
        questDescription7.SetReward(new RewardPack(REWARDTYPE.Money, 250));
        questDescription7.TaskShortSummary = StringID.INVESTOR4_TaskSummary;
        heroQuestPack.heroquests.Add(questDescription7);
        HeroQuestDescription questDescription9 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription9.ThisQuestHeading = StringID.INVESTOR5_Heading;
        questDescription9.ThisQuestDescriptin = StringID.INVESTOR5_Desc;
        questDescription9.SetUpVisitorTarget(false, 50);
        HeroQuestDescription questDescription10 = new HeroQuestDescription();
        questDescription9.TaskShortSummary = StringID.INVESTOR5_TaskSummary;
        questDescription10.SetUpUnlockAtStartOfGame();
        questDescription9.UnlockThisQuestCriteria.Add(questDescription10);
        questDescription9.SetReward(new RewardPack(REWARDTYPE.Money, 500));
        heroQuestPack.heroquests.Add(questDescription9);
        HeroQuestDescription questDescription11 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription11.ThisQuestHeading = StringID.INVESTOR6_Heading;
        questDescription11.ThisQuestDescriptin = StringID.INVESTOR6_Desc;
        questDescription11.TaskShortSummary = StringID.INVESTOR6_TaskSummary;
        questDescription11.SetUpMoneyCashTarget(2000, false);
        HeroQuestDescription questDescription12 = new HeroQuestDescription();
        questDescription12.SetUpUnlockAtStartOfGame();
        questDescription11.UnlockThisQuestCriteria.Add(questDescription12);
        questDescription11.SetReward(new RewardPack(REWARDTYPE.Money, 1000));
        heroQuestPack.heroquests.Add(questDescription11);
        HeroQuestDescription questDescription13 = new HeroQuestDescription(ref _UID, HeroCharacter.Investor);
        questDescription13.ThisQuestHeading = StringID.INVESTOR7_Heading;
        questDescription13.ThisQuestDescriptin = StringID.INVESTOR7_Desc;
        questDescription13.SetUpMoneyCashTarget(25000, true);
        questDescription13.TaskShortSummary = StringID.INVESTOR7_TaskSummary;
        HeroQuestDescription questDescription14 = new HeroQuestDescription();
        questDescription14.SetUpUnlockAtStartOfGame();
        questDescription13.UnlockThisQuestCriteria.Add(questDescription14);
        heroQuestPack.heroquests.Add(questDescription13);
        questDescription13.SetReward(new RewardPack(REWARDTYPE.Money, 1000));
      }
      return heroQuestPack;
    }
  }
}
