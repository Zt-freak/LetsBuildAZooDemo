// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_PublicIdiot
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_PublicIdiot
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.AfroOldLady, HeroCharacter.Complainer);
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      questDescription1.SetUpDaysPassed(1);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.Complainer);
      questDescription2.ThisQuestHeading = StringID.PUBIDIOT_Heading;
      questDescription2.ThisQuestDescriptin = StringID.PUBIDIOT_Desc;
      questDescription2.SetUpThingsBuilt(1, TILETYPE.StoreRoom);
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.TaskShortSummary = StringID.PUBIDIOT_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription2);
      HeroQuestDescription questDescription3 = new HeroQuestDescription(ref _UID, HeroCharacter.Complainer);
      questDescription3.ThisQuestHeading = StringID.PUBIDIOT_Heading2;
      questDescription3.ThisQuestDescriptin = StringID.PUBIDIOT_Desc2;
      questDescription3.SetUpEmployThisPerson(EmployeeType.Janitor);
      HeroQuestDescription questDescription4 = new HeroQuestDescription();
      if (Z_DebugFlags.IsBetaVersion)
        questDescription4.SetUpUnlockAtStartOfGame();
      else
        questDescription4.SetUpDaysPassed(2);
      questDescription3.UnlockThisQuestCriteria.Add(questDescription4);
      questDescription3.WillAutoPopOnComplete = false;
      questDescription3.TaskShortSummary = StringID.PUBIDIOT_TaskSummary2;
      heroQuestPack.heroquests.Add(questDescription3);
      return heroQuestPack;
    }
  }
}
