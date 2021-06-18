// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_Zoo_Australia
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_Zoo_Australia
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.AustralianZookeeper, HeroCharacter.Zoo_Austrailia);
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      questDescription1.SetUpBuildPen(1);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.Zoo_Austrailia);
      questDescription2.ThisQuestHeading = StringID.AUSZOO_Heading;
      questDescription2.ThisQuestDescriptin = StringID.AUSZOO_Desc;
      questDescription2.SetUpDoZooTrade(AnimalType.AustralianZookeeper, 1);
      questDescription2.WillAutoPopOnUnlock = true;
      questDescription2.tutorialquestspecial = TutorialQuestSpecial.HasCompletedEnoughQuestsToStartDay;
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.WillAutoPopOnComplete = true;
      questDescription2.TaskShortSummary = StringID.AUSZOO_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription2);
      return heroQuestPack;
    }
  }
}
