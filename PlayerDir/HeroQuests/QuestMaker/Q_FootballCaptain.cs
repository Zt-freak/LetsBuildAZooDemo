// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestMaker.Q_FootballCaptain
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.BusTimetable;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir.HeroQuests.QuestMaker
{
  internal class Q_FootballCaptain
  {
    internal static HeroQuestPack GetQuests()
    {
      HeroQuestDescription questDescription1 = new HeroQuestDescription();
      if (Z_DebugFlags.IsBetaVersion)
        questDescription1.SetUpDaysPassed(20);
      else
        questDescription1.SetUpDaysPassed(4);
      int _UID = 0;
      HeroQuestPack heroQuestPack = new HeroQuestPack(AnimalType.FootballCaptain, HeroCharacter.FootballCaptain);
      HeroQuestDescription questDescription2 = new HeroQuestDescription(ref _UID, HeroCharacter.FootballCaptain);
      questDescription2.ThisQuestHeading = StringID.FOOTBALLCAPT_Heading;
      questDescription2.ThisQuestDescriptin = StringID.FOOTBALLCAPT_Desc;
      questDescription2.UseCustomQuestDescriptionString = true;
      questDescription2.SetUpBuyBus(BUSROUTE.Route01, BUSTYPE.BiggerBus_02);
      questDescription2.UnlockThisQuestCriteria.Add(questDescription1);
      questDescription2.TaskShortSummary = StringID.FOOTBALLCAPT_TaskSummary;
      heroQuestPack.heroquests.Add(questDescription2);
      return heroQuestPack;
    }
  }
}
