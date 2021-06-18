// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.HeroQuestProgress
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_HUD;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;

namespace TinyZoo.PlayerDir.HeroQuests
{
  internal class HeroQuestProgress
  {
    public List<HeroQuestPack> heroquestpacks;
    public HeroProgressStatus[] ProgressArray;
    public List<HeroQuestPack> DailyChallenges;

    public HeroQuestProgress()
    {
      this.DailyChallenges = new List<HeroQuestPack>();
      this.heroquestpacks = new List<HeroQuestPack>();
      this.ProgressArray = new HeroProgressStatus[11];
      for (int index = 0; index < this.ProgressArray.Length; ++index)
        this.ProgressArray[index] = new HeroProgressStatus((HeroCharacter) index);
    }

    public void AutoCompleteAllHeroQuests()
    {
      for (int index = 0; index < this.ProgressArray.Length; ++index)
        this.ProgressArray[index].AutoCompleteAllHeroQuests();
    }

    public void StartNewDay()
    {
      if (Z_DebugFlags.IsBetaVersion)
        return;
      for (int index = 0; index < this.DailyChallenges.Count; ++index)
      {
        if (this.DailyChallenges[index].herocharacter != HeroCharacter.Critical_Scientist)
          throw new Exception("oshdsf");
        NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo("Research Task", "Offer Expired"));
      }
      this.DailyChallenges = new List<HeroQuestPack>();
    }

    public void RemoveDaily(HeroQuestDescription refquest)
    {
      for (int index = this.DailyChallenges.Count - 1; index > -1; --index)
      {
        if (this.DailyChallenges[index].heroquests[0] == refquest)
        {
          this.DailyChallenges.RemoveAt(index);
          break;
        }
      }
    }

    public void AddTempDailyQuest(HeroQuestPack heroquestpack, Player player)
    {
      this.DailyChallenges.Add(heroquestpack);
      ZHudManager.zquestpins.PinQuest(heroquestpack.heroquests[0], player);
    }

    public int GetTotalQuestsComplete()
    {
      int num = 0;
      for (int index = 0; index < this.ProgressArray.Length; ++index)
        num += this.ProgressArray[index].GetCompletetedQuests();
      return num;
    }

    public List<HeroProgressStatus> GetActiveQuestsInDisplayOrder()
    {
      List<HeroProgressStatus> heroProgressStatusList1 = new List<HeroProgressStatus>();
      List<HeroProgressStatus> heroProgressStatusList2 = new List<HeroProgressStatus>();
      for (int index = 0; index < this.DailyChallenges.Count; ++index)
      {
        heroProgressStatusList1.Add(new HeroProgressStatus(this.DailyChallenges[index].herocharacter, this.DailyChallenges[index]));
        heroProgressStatusList1[index].CurrentQuestIndex = 0;
      }
      for (int index = 0; index < this.ProgressArray.Length; ++index)
      {
        if (this.ProgressArray[index].IsUnlocked && this.ProgressArray[index].CurrentQuestIndex > -1 && !this.ProgressArray[index].HasCompletedThis(this.ProgressArray[index].CurrentQuestIndex))
          heroProgressStatusList1.Add(this.ProgressArray[index]);
        else if (this.ProgressArray[index].IsUnlocked)
          heroProgressStatusList2.Add(this.ProgressArray[index]);
      }
      for (int index = 0; index < heroProgressStatusList2.Count; ++index)
        heroProgressStatusList1.Add(heroProgressStatusList2[index]);
      return heroProgressStatusList1;
    }

    public HeroProgressStatus GetHeroProgressFromQuest(
      HeroQuestDescription questdescription)
    {
      for (int index = 0; index < this.ProgressArray.Length; ++index)
      {
        if (this.ProgressArray[index].RefHeroQuestPack.HasThisQuest(questdescription))
          return this.ProgressArray[index];
      }
      return (HeroProgressStatus) null;
    }

    public bool CheckCharacterProgress(HeroCharacter character, int QuestTarget) => QuestTarget == 0 ? this.ProgressArray[(int) character].IsUnlocked : this.ProgressArray[(int) character].GetCompletetedQuests() >= QuestTarget;

    public int GetQuestsCompleteFromThisHero(HeroCharacter character) => this.ProgressArray[(int) character].GetCompletetedQuests();

    public bool HasThisQuestBeenCompleted(HeroQuestDescription quest) => this.ProgressArray[(int) quest.herocharacter].HasCompletedThis(quest);

    public void SaveHeroQuestProgress(Writer writer)
    {
      writer.WriteInt("p", this.ProgressArray.Length);
      for (int index = 0; index < this.ProgressArray.Length; ++index)
        this.ProgressArray[index].SaveHeroProgressStatus(writer);
    }

    public HeroQuestProgress(Reader reader)
    {
      this.DailyChallenges = new List<HeroQuestPack>();
      this.heroquestpacks = new List<HeroQuestPack>();
      int _out = 0;
      int num = (int) reader.ReadInt("P", ref _out);
      this.ProgressArray = new HeroProgressStatus[11];
      for (int index = 0; index < 11; ++index)
      {
        this.ProgressArray[index] = new HeroProgressStatus((HeroCharacter) index);
        if (index < _out)
          this.ProgressArray[index].LoadHeroProgressStatus(reader);
      }
    }
  }
}
