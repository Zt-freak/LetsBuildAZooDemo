// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_Critical_Geneticist
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Tile_Data;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_Critical_Geneticist
  {
    internal static HeroQuestPack GetQuests()
    {
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.SpecialEvent_GenomeScientist, HeroCharacter.Critical_Geneticist);
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      questDescription1.SetUpOnUnlockBuilding(TILETYPE.DNABuilding);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.Critical_Geneticist);
      questDescription2.ThisQuestHeading = StringID.GENOMESCIENTISTB1_Heading;
      questDescription2.ThisQuestDescriptin = StringID.GENOMESCIENTISTB1_Desc;
      questDescription2.SetUpThingsBuilt(1, TILETYPE.DNABuilding);
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.WillAutoPin = true;
      heroQuestPack.heroquests.Add(questDescription2);
      questDescription2.TaskShortSummary = StringID.GENOMESCIENTISTB1_TaskSummary;
      HeroQuestDescription questDescription3 = new HeroQuestDescription(ref _UID, HeroCharacter.Critical_Geneticist);
      questDescription3.ThisQuestHeading = StringID.GENOMESCIENTISTB2_Heading;
      questDescription3.ThisQuestDescriptin = StringID.GENOMESCIENTISTB2_Desc;
      questDescription3.SetUpCrossBreed(4);
      HeroQuestDescription questDescription4 = new HeroQuestDescription();
      questDescription4.SetUpUnlockAtStartOfGame();
      questDescription3.UnlockThisQuestCriteria.Add(questDescription4);
      questDescription3.TaskShortSummary = StringID.GENOMESCIENTISTB2_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription3);
      questDescription3.WillAutoPin = true;
      return heroQuestPack;
    }
  }
}
