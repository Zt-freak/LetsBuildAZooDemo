// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.QuestScrubber
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.Z_HUD;

namespace TinyZoo.PlayerDir.HeroQuests
{
  internal class QuestScrubber
  {
    internal static void ScrubQuests(Player player)
    {
      for (int index = 0; index < player.heroquestprogress.ProgressArray.Length; ++index)
      {
        if (!player.heroquestprogress.ProgressArray[index].IsUnlocked)
          QuestScrubber.TryAndUnlockCharacterorNewQuest(player.heroquestprogress.ProgressArray[index], (HeroCharacter) index, player);
        else if (player.heroquestprogress.ProgressArray[index].CurrentQuestIndex == -1)
          QuestScrubber.TryAndUnlockCharacterorNewQuest(player.heroquestprogress.ProgressArray[index], (HeroCharacter) index, player);
      }
    }

    internal static void CheckCompletionAlerts(Player player)
    {
      Z_GameFlags.HasCompleteQuestsToView = false;
      for (int index = 0; index < player.heroquestprogress.ProgressArray.Length; ++index)
      {
        if (!player.heroquestprogress.ProgressArray[index].IsUnlocked)
        {
          HeroQuestDescription activeQuest = player.heroquestprogress.ProgressArray[index].GetActiveQuest();
          if (activeQuest != null && activeQuest.CheckIsComplete(player))
          {
            Z_GameFlags.HasCompleteQuestsToView = true;
            break;
          }
        }
      }
    }

    private static bool TryAndUnlockCharacterorNewQuest(
      HeroProgressStatus heroprpgressstatus,
      HeroCharacter thishero,
      Player player)
    {
      if (!heroprpgressstatus.TryAndUnlockNextQuest(player))
        return false;
      heroprpgressstatus.IsUnlocked = true;
      return true;
    }

    private static void ScrubAndProcessSpecificQuest(HEROQUESTTYPE questtypetocheck, Player player)
    {
      for (int index1 = 0; index1 < player.heroquestprogress.ProgressArray.Length; ++index1)
      {
        if (player.heroquestprogress.ProgressArray[index1].CurrentQuestIndex > -1)
        {
          if (player.heroquestprogress.ProgressArray[index1].GetActiveQuest().heroquesttype == questtypetocheck && player.heroquestprogress.ProgressArray[index1].GetActiveQuest().CheckIsComplete(player))
          {
            HeroQuestDescription activeQuest = player.heroquestprogress.ProgressArray[index1].GetActiveQuest();
            if (activeQuest.WillAutoPopOnComplete)
              OverWorldManager.zoopopupHolder.CreateZooPopUps(activeQuest, player, POPUPSTATE.HeroQuests, true);
            else
              Z_GameFlags.HasCompleteQuestsToView = true;
          }
        }
        else
        {
          HeroQuestDescription nextUnlockCriteria = player.heroquestprogress.ProgressArray[index1].GetNextUnlockCriteria();
          if (nextUnlockCriteria != null)
          {
            bool flag = false;
            for (int index2 = 0; index2 < nextUnlockCriteria.UnlockThisQuestCriteria.Count; ++index2)
            {
              if (nextUnlockCriteria.UnlockThisQuestCriteria[index2].heroquesttype == questtypetocheck)
                flag = true;
            }
            if (flag && nextUnlockCriteria.CheckUnlockQuestsCriteriaComplete(player))
            {
              player.heroquestprogress.ProgressArray[index1].CurrentQuestIndex = nextUnlockCriteria.UID;
              player.heroquestprogress.ProgressArray[index1].IsNew = true;
              player.heroquestprogress.ProgressArray[index1].IsUnlocked = true;
              if (!Z_GameFlags.HasStartedFirstDay || nextUnlockCriteria.WillAutoPin)
                ZHudManager.zquestpins.PinQuest(nextUnlockCriteria, player);
              if (nextUnlockCriteria.WillAutoPopOnUnlock)
                OverWorldManager.zoopopupHolder.CreateZooPopUps(nextUnlockCriteria, player, POPUPSTATE.HeroQuests, false);
              else
                Z_GameFlags.HasCompleteQuestsToView = true;
            }
          }
        }
      }
    }

    internal static void ScrubOnParkRatingChange(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.ParkRating, player);

    internal static void ScrubOnResearchingOrUnlockBuilding(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.UnlockBuilding, player);

    internal static void ScrubOnOpeningTaskList(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.ViewTasks, player);

    internal static void ScrubOnBuildingPen(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.BuildPen, player);

    internal static void ScrubOnPlacingBuilding(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.ThingsBuilt, player);

    internal static void ScrubOnGettingNewEmployee(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.EmployThisPerson, player);

    internal static void ScrubOnSettingNewDecoValue(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.ReachDecoLevel, player);

    internal static void ScrubOnCompletingTrade(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.CompleteZooTrade, player);

    internal static void ScrubOnBuyingBus(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.BuyBus, player);

    internal static void ScrubOnRecievingAnimal(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.CurrentAnimalNumber, player);

    internal static void ScrubOnOpenForBusinessFirstTime(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.OpenForBusiness, player);

    internal static void ScrubOnStartNewDay(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.DaysPassed, player);

    internal static void ScrubOnCompleteHeroQuest(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.OtherHeroCharacterprogress, player);

    internal static void ScrubOnMoralityPointChange(Player player)
    {
      if ((double) player.livestats.MoralityScore < 0.0)
        QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.EvilPoints, player);
      else
        QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.GoodPoints, player);
    }

    internal static void ScrubOnHumanDeath(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.HumanDeaths, player);

    internal static void ScrubOnAnimalDeath(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.AnimalDeaths, player);

    internal static void ScrubOnBirth(Player player, bool WasBreedingRoomBirth = false) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.Birth, player);

    internal static void ScrubOnNewAnimalVariant(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.GenomeMap_OrVariantsFound, player);

    internal static void ScrubOnCash(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.Wealth, player);

    internal static void ScrubOnCrossBreed(Player player)
    {
    }

    internal static void ScrubOnVistor(Player player)
    {
    }

    internal static void ScrubOnReuptation(Player player)
    {
    }

    internal static void ScrubOnResearch(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.Research, player);

    internal static void ScrubOnZooSIzeIncreaseBuyLand(Player player) => QuestScrubber.ScrubAndProcessSpecificQuest(HEROQUESTTYPE.ZooSize, player);

    internal static void ScrubOnMakeDonation(Player player)
    {
    }

    internal static void ScrubOnBuyFromBlackMarket(Player player)
    {
    }

    internal static void ScrubOnSellOnBlackMarket(Player player)
    {
    }
  }
}
