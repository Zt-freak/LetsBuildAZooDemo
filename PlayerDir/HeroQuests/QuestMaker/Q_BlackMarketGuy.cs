// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_BlackMarketGuy
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_BlackMarketGuy
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.TigerKing, HeroCharacter.BlackMarketGuy);
      HeroQuestDescription questDescription1 = new HeroQuestDescription(ref _UID, HeroCharacter.BlackMarketGuy);
      questDescription1.ThisQuestHeading = StringID.BLACKMARKEYGUY_Heading;
      questDescription1.ThisQuestDescriptin = StringID.BLACKMARKEYGUY_Desc;
      questDescription1.SetUpBuyAnimalFromBlackMarket(1);
      HeroQuestDescription questDescription2 = new HeroQuestDescription();
      if (Z_DebugFlags.IsBetaVersion)
      {
        questDescription2.SetUpDaysPassed(21);
        questDescription1.UnlockThisQuestCriteria.Add(questDescription2);
      }
      else
      {
        questDescription2.SetUpDaysPassed(7);
        questDescription1.UnlockThisQuestCriteria.Add(questDescription2);
        HeroQuestDescription questDescription3 = new HeroQuestDescription();
        questDescription3.SetUpEvilPoints(10);
        questDescription1.UnlockThisQuestCriteria.Add(questDescription3);
      }
      questDescription1.TaskShortSummary = StringID.BLACKMARKEYGUY_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription1);
      return heroQuestPack;
    }
  }
}
