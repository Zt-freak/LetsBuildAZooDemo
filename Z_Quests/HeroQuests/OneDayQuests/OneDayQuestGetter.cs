// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Quests.HeroQuests.OneDayQuests.OneDayQuestGetter
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData;

namespace TinyZoo.Z_Quests.HeroQuests.OneDayQuests
{
  internal class OneDayQuestGetter
  {
    internal static HeroQuestPack GetDayQuest(
      CustomerType customertype,
      Player player,
      CriticalChoiceAction choiceaction)
    {
      if (customertype != CustomerType.ResearchGrantGuy)
        return (HeroQuestPack) null;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.SpecialEvent_Scientist, HeroCharacter.Critical_Scientist);
      int _UID = 0;
      HeroQuestDescription questDescription = new HeroQuestDescription(ref _UID, HeroCharacter.Critical_Scientist);
      questDescription.ThisQuestHeading = StringID.SPECIALResearcher_Heading;
      questDescription.ThisQuestDescriptin = StringID.SPECIALResearcher_TaskDescription;
      questDescription.SetUpThingsBuilt(1, TILETYPE.ArchitectOffice, CATEGORYTYPE.Facilities);
      heroQuestPack.heroquests.Add(questDescription);
      questDescription.TaskShortSummary = StringID.SPECIALResearcher_TaskSummary;
      questDescription.SetReward(new RewardPack(REWARDTYPE.DailySponsorship, choiceaction.ValueMainOrPercent, choiceaction.Days_OtherValue));
      questDescription.WillAutoPopOnComplete = true;
      return heroQuestPack;
    }
  }
}
