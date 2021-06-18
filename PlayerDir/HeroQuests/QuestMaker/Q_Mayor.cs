// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_Mayor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_Mayor
  {
    internal static HeroQuestPack GetQuests()
    {
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.Mayor, HeroCharacter.Mayor);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.Mayor);
      questDescription2.ThisQuestHeading = StringID.MAYOR_Heading;
      questDescription2.ThisQuestDescriptin = StringID.MAYOR_Desc;
      questDescription2.SetUpLandUnlocked(2);
      questDescription2.tutorialquestspecial = TutorialQuestSpecial.UnlockAbilityToBuyLand;
      questDescription1.SetUpGetNumberOfAnAnimal(AnimalType.None, 5);
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      heroQuestPack.heroquests.Add(questDescription2);
      questDescription2.TaskShortSummary = StringID.MAYOR_TaskSummary;
      return heroQuestPack;
    }
  }
}
