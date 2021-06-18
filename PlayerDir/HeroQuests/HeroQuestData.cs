// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.HeroQuestData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests.QuestMaker;
using TinyZoo.Z_Quests.HeroQuests.QuestMaker;

namespace TinyZoo.PlayerDir.HeroQuests
{
  internal class HeroQuestData
  {
    private static HeroQuestPack[] heroquestpacks;

    internal static AnimalType GetHeroCharacterToAnimalType(HeroCharacter herocharacter)
    {
      if (herocharacter == HeroCharacter.Critical_Scientist)
        return AnimalType.SpecialEvent_Scientist;
      throw new Exception("You need to add the rest of the heros");
    }

    internal static HeroQuestPack GetHeroQuestData(HeroCharacter hero)
    {
      if (HeroQuestData.heroquestpacks == null)
        HeroQuestData.heroquestpacks = new HeroQuestPack[11];
      if (hero <= HeroCharacter.Count && HeroQuestData.heroquestpacks[(int) hero] == null)
      {
        switch (hero)
        {
          case HeroCharacter.Investor:
            HeroQuestData.heroquestpacks[(int) hero] = Q_Investor.GetQuests();
            break;
          case HeroCharacter.Zoo_Austrailia:
            HeroQuestData.heroquestpacks[(int) hero] = Q_Zoo_Australia.GetQuests();
            break;
          case HeroCharacter.FootballCaptain:
            HeroQuestData.heroquestpacks[(int) hero] = Q_FootballCaptain.GetQuests();
            break;
          case HeroCharacter.Mayor:
            HeroQuestData.heroquestpacks[(int) hero] = Q_Mayor.GetQuests();
            break;
          case HeroCharacter.BlackMarketGuy:
            HeroQuestData.heroquestpacks[(int) hero] = Q_BlackMarketGuy.GetQuests();
            break;
          case HeroCharacter.Complainer:
            HeroQuestData.heroquestpacks[(int) hero] = Q_PublicIdiot.GetQuests();
            break;
          case HeroCharacter.AnimalShelterGirl:
            HeroQuestData.heroquestpacks[(int) hero] = Q_AnimalShelterGirl.GetQuests();
            break;
          case HeroCharacter.TransportPlaner:
            HeroQuestData.heroquestpacks[(int) hero] = Q_TransportPlaner.GetQuests();
            break;
          case HeroCharacter.Critical_Geneticist:
            HeroQuestData.heroquestpacks[(int) hero] = Q_Critical_Geneticist.GetQuests();
            break;
          case HeroCharacter.Scientist2:
            HeroQuestData.heroquestpacks[(int) hero] = Q_Scientist.GetQuests();
            break;
          case HeroCharacter.AnimalSpotter:
            HeroQuestData.heroquestpacks[(int) hero] = Q_AnimalSpotter.GetQuests();
            break;
          default:
            throw new Exception("NOT FOUND");
        }
      }
      return HeroQuestData.heroquestpacks[(int) hero];
    }

    internal static HeroCharacter GetAnimalTypeToHero(AnimalType animaltype)
    {
      if (animaltype == AnimalType.AustralianZookeeper)
        return HeroCharacter.Zoo_Austrailia;
      throw new Exception("MISSED");
    }

    internal static string GetHeroCompleteText(HeroCharacter hero)
    {
      switch (hero)
      {
        case HeroCharacter.Investor:
          return SEngine.Localization.Localization.GetText(777);
        case HeroCharacter.Zoo_Austrailia:
          return SEngine.Localization.Localization.GetText(779);
        case HeroCharacter.FootballCaptain:
          return SEngine.Localization.Localization.GetText(776);
        case HeroCharacter.Mayor:
          return SEngine.Localization.Localization.GetText(778);
        case HeroCharacter.BlackMarketGuy:
          return SEngine.Localization.Localization.GetText(775);
        case HeroCharacter.Complainer:
          return SEngine.Localization.Localization.GetText(780);
        case HeroCharacter.AnimalShelterGirl:
          return SEngine.Localization.Localization.GetText(781);
        case HeroCharacter.TransportPlaner:
          return SEngine.Localization.Localization.GetText(782);
        case HeroCharacter.Critical_Geneticist:
          return SEngine.Localization.Localization.GetText(1064);
        case HeroCharacter.Scientist2:
        case HeroCharacter.Critical_Scientist:
          return SEngine.Localization.Localization.GetText(1068);
        case HeroCharacter.AnimalSpotter:
          return SEngine.Localization.Localization.GetText(1072);
        default:
          return "Text Incomplete";
      }
    }

    internal static string GetHeroCharacterToString(HeroCharacter hero)
    {
      switch (hero)
      {
        case HeroCharacter.Investor:
          return SEngine.Localization.Localization.GetText(769);
        case HeroCharacter.Zoo_Austrailia:
          return SEngine.Localization.Localization.GetText(771);
        case HeroCharacter.FootballCaptain:
          return SEngine.Localization.Localization.GetText(768);
        case HeroCharacter.Mayor:
          return SEngine.Localization.Localization.GetText(770);
        case HeroCharacter.BlackMarketGuy:
          return SEngine.Localization.Localization.GetText(767);
        case HeroCharacter.Complainer:
          return SEngine.Localization.Localization.GetText(772);
        case HeroCharacter.AnimalShelterGirl:
          return SEngine.Localization.Localization.GetText(773);
        case HeroCharacter.TransportPlaner:
          return SEngine.Localization.Localization.GetText(774);
        case HeroCharacter.Critical_Geneticist:
          return "Nicole";
        case HeroCharacter.Scientist2:
          return "Octavio";
        case HeroCharacter.AnimalSpotter:
          return "Potter";
        case HeroCharacter.Critical_Scientist:
          return "Octavio";
        default:
          return "NOT DONE";
      }
    }
  }
}
