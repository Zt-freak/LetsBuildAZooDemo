// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_AnimalSpotter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_AnimalSpotter
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.AnimalSpotter, HeroCharacter.AnimalSpotter);
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      questDescription1.SetUpDaysPassed(4);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.AnimalSpotter);
      questDescription2.ThisQuestHeading = StringID.ANIMALSPOTTERB1_Heading;
      questDescription2.ThisQuestDescriptin = StringID.ANIMALSPOTTERB1_Desc;
      questDescription2.SetUpGetNumberOfAnAnimal(AnimalType.None, 10);
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.TaskShortSummary = StringID.ANIMALSPOTTERB1_TaskSummary;
      questDescription2.WillAutoPin = true;
      questDescription2.WillAutoPopOnComplete = true;
      heroQuestPack.heroquests.Add(questDescription2);
      HeroQuestDescription questDescription3 = new HeroQuestDescription(ref _UID, HeroCharacter.AnimalSpotter);
      questDescription3.ThisQuestHeading = StringID.ANIMALSPOTTERB2_Heading;
      questDescription3.ThisQuestDescriptin = StringID.ANIMALSPOTTERB2_Desc;
      questDescription3.SetUpGetNumberOfAnAnimal(AnimalType.None, 25);
      HeroQuestDescription questDescription4 = new HeroQuestDescription();
      questDescription4.SetUpUnlockAtStartOfGame();
      questDescription3.UnlockThisQuestCriteria.Add(questDescription4);
      questDescription3.TaskShortSummary = StringID.ANIMALSPOTTERB2_TaskSummary;
      questDescription3.WillAutoPin = true;
      questDescription3.WillAutoPopOnComplete = true;
      heroQuestPack.heroquests.Add(questDescription3);
      return heroQuestPack;
    }
  }
}
