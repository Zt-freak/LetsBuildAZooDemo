// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_AnimalShelterGirl
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_AnimalShelterGirl
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.AnimalShelterGirl, HeroCharacter.AnimalShelterGirl);
      HeroQuestDescription questDescription1 = new HeroQuestDescription(ref _UID, HeroCharacter.AnimalShelterGirl);
      questDescription1.ThisQuestHeading = StringID.ANIMALSHELTERGIRL_Heading;
      questDescription1.ThisQuestDescriptin = StringID.ANIMALSHELTERGIRL_Desc;
      questDescription1.SetUpGiveMoney(5000);
      HeroQuestDescription questDescription2 = new HeroQuestDescription();
      questDescription2.SetUpDaysPassed(21);
      questDescription1.UnlockThisQuestCriteria.Add(questDescription2);
      HeroQuestDescription questDescription3 = new HeroQuestDescription();
      questDescription3.SetUpGoodPoints(10);
      questDescription1.UnlockThisQuestCriteria.Add(questDescription3);
      questDescription1.TaskShortSummary = StringID.ANIMALSHELTERGIRL_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription1);
      HeroQuestDescription questDescription4 = new HeroQuestDescription(ref _UID, HeroCharacter.AnimalShelterGirl);
      questDescription4.ThisQuestHeading = StringID.ANIMALSHELTERGIRL_Heading;
      questDescription4.ThisQuestDescriptin = StringID.ANIMALSHELTERGIRL_Desc;
      questDescription4.SetUpGiveMoney(8000);
      HeroQuestDescription questDescription5 = new HeroQuestDescription();
      questDescription5.SetUpDaysPassed(77);
      questDescription4.UnlockThisQuestCriteria.Add(questDescription5);
      HeroQuestDescription questDescription6 = new HeroQuestDescription();
      questDescription6.SetUpGoodPoints(10);
      questDescription4.UnlockThisQuestCriteria.Add(questDescription6);
      questDescription4.TaskShortSummary = StringID.ANIMALSHELTERGIRL_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription4);
      questDescription4.WillAutoPin = true;
      HeroQuestDescription questDescription7 = new HeroQuestDescription(ref _UID, HeroCharacter.AnimalShelterGirl);
      questDescription7.ThisQuestHeading = StringID.ANIMALSHELTERGIRL_Heading;
      questDescription7.ThisQuestDescriptin = StringID.ANIMALSHELTERGIRL_Desc;
      questDescription7.SetUpGiveMoney(10000);
      HeroQuestDescription questDescription8 = new HeroQuestDescription();
      questDescription8.SetUpDaysPassed(168);
      questDescription7.UnlockThisQuestCriteria.Add(questDescription8);
      HeroQuestDescription questDescription9 = new HeroQuestDescription();
      questDescription9.SetUpGoodPoints(20);
      questDescription7.UnlockThisQuestCriteria.Add(questDescription9);
      questDescription7.TaskShortSummary = StringID.ANIMALSHELTERGIRL_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription7);
      questDescription7.WillAutoPin = true;
      return heroQuestPack;
    }
  }
}
