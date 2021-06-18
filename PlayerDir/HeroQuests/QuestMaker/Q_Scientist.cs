// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_Scientist
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_Scientist
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.SpecialEvent_Scientist, HeroCharacter.Scientist2);
      HeroQuestDescription questDescription1 = new HeroQuestDescription(ref _UID, HeroCharacter.Scientist2);
      questDescription1.ThisQuestHeading = StringID.SCIENTISTB2_Heading;
      questDescription1.ThisQuestDescriptin = StringID.SCIENTISTB2_Desc;
      questDescription1.SetUpResearch(1);
      HeroQuestDescription questDescription2 = new HeroQuestDescription();
      questDescription2.SetUpThingsBuilt(1, TILETYPE.ArchitectOffice);
      questDescription1.UnlockThisQuestCriteria.Add(questDescription2);
      questDescription1.TaskShortSummary = StringID.SCIENTISTB2_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription1);
      questDescription1.WillAutoPopOnUnlock = false;
      questDescription1.WillAutoPin = true;
      questDescription1.WillAutoPopOnComplete = true;
      HeroQuestDescription questDescription3 = new HeroQuestDescription(ref _UID, HeroCharacter.Scientist2);
      questDescription3.ThisQuestHeading = StringID.SCIENTISTB1_Heading;
      questDescription3.ThisQuestDescriptin = StringID.SCIENTISTB1_Desc;
      questDescription3.SetUpEmployThisPerson(EmployeeType.Architect, 2);
      HeroQuestDescription questDescription4 = new HeroQuestDescription();
      questDescription4.SetUpResearch(0, UnlockTYPE.ArchitextPlusOne);
      questDescription3.UnlockThisQuestCriteria.Add(questDescription4);
      questDescription3.WillAutoPopOnComplete = false;
      questDescription3.TaskShortSummary = StringID.SCIENTISTB1_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription3);
      return heroQuestPack;
    }
  }
}
