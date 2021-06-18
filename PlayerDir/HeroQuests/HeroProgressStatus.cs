// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.HeroQuests.HeroProgressStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_HUD;

namespace TinyZoo.PlayerDir.HeroQuests
{
  internal class HeroProgressStatus
  {
    public int CurrentQuestIndex = -1;
    private List<int> CompletedQuests;
    public int ValueForThis;
    public bool IsUnlocked;
    public HeroCharacter thishero;
    public bool IsNew;
    public HeroQuestPack RefHeroQuestPack;

    public HeroProgressStatus(HeroCharacter _thishero)
    {
      this.CompletedQuests = new List<int>();
      this.thishero = _thishero;
      this.RefHeroQuestPack = HeroQuestData.GetHeroQuestData(this.thishero);
    }

    public HeroProgressStatus(HeroCharacter _thishero, HeroQuestPack heroquests)
    {
      this.IsUnlocked = true;
      this.CompletedQuests = new List<int>();
      this.thishero = _thishero;
      this.RefHeroQuestPack = heroquests;
    }

    public int GetCompletetedQuests() => this.CompletedQuests.Count;

    public void AutoCompleteAllHeroQuests()
    {
      for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
      {
        if (!this.CompletedQuests.Contains(this.RefHeroQuestPack.heroquests[index].UID))
          this.CompletedQuests.Add(this.RefHeroQuestPack.heroquests[index].UID);
      }
    }

    public string GetQuestName() => this.CurrentQuestIndex > -1 ? this.GetActiveQuest().GetQuestHeading() : SEngine.Localization.Localization.GetText(757);

    public bool HasCompletedThis(int QuestUID) => this.CompletedQuests.Contains(QuestUID);

    public bool HasCompletedThis(HeroQuestDescription quest) => this.CompletedQuests.Contains(quest.UID);

    public string GetNextQustUnlockText(Player player)
    {
      HeroQuestDescription nextQuest = this.GetNextQuest();
      if (nextQuest == null)
        return SEngine.Localization.Localization.GetText(758);
      return nextQuest.CheckUnlockQuestsCriteriaComplete(player) ? SEngine.Localization.Localization.GetText(759) : nextQuest.GetHeaderForUnlockCriteria(out string _, player, out int _, out int _);
    }

    public HeroQuestDescription GetNextQuest()
    {
      for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
      {
        if (this.RefHeroQuestPack.heroquests[index].UID != this.CurrentQuestIndex && !this.CompletedQuests.Contains(this.RefHeroQuestPack.heroquests[index].UID))
          return this.RefHeroQuestPack.heroquests[index];
      }
      return (HeroQuestDescription) null;
    }

    public HeroQuestDescription GetActiveQuest()
    {
      if (this.CurrentQuestIndex == -1)
        return (HeroQuestDescription) null;
      for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
      {
        if (this.RefHeroQuestPack.heroquests[index].UID == this.CurrentQuestIndex)
          return this.RefHeroQuestPack.heroquests[index];
      }
      return (HeroQuestDescription) null;
    }

    public bool TryAndCompleteQuest(Player player)
    {
      if (this.IsUnlocked && this.CurrentQuestIndex > -1)
      {
        for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
        {
          if (this.RefHeroQuestPack.heroquests[index].UID == this.CurrentQuestIndex && this.RefHeroQuestPack.heroquests[index].CheckIsComplete(player))
          {
            this.CompletedQuests.Add(this.CurrentQuestIndex);
            this.CurrentQuestIndex = -1;
            Z_GameFlags.ScrubOtherHeroQuest = true;
            return true;
          }
        }
      }
      return false;
    }

    public HeroQuestDescription GetNextUnlockCriteria()
    {
      if (!this.IsUnlocked)
        return this.RefHeroQuestPack.heroquests[0];
      for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
      {
        if (!this.CompletedQuests.Contains(this.RefHeroQuestPack.heroquests[index].UID))
          return this.RefHeroQuestPack.heroquests[index];
      }
      return (HeroQuestDescription) null;
    }

    public bool TryAndUnlockNextQuest(Player player)
    {
      if (!this.IsUnlocked)
      {
        if (!this.CompletedQuests.Contains(this.RefHeroQuestPack.heroquests[0].UID) && this.RefHeroQuestPack.heroquests[0].CheckUnlockQuestsCriteriaComplete(player))
        {
          this.CurrentQuestIndex = this.RefHeroQuestPack.heroquests[0].UID;
          this.IsNew = true;
          this.IsUnlocked = true;
          if (FeatureFlags.BlockBuyLand && this.RefHeroQuestPack.heroquests[0].tutorialquestspecial == TutorialQuestSpecial.UnlockAbilityToBuyLand)
          {
            FeatureFlags.BlockBuyLand = false;
            Z_GameFlags.ScrubForSaleSigns = true;
          }
          if (this.RefHeroQuestPack.heroquests[0].WillAutoPopOnUnlock)
            OverWorldManager.zoopopupHolder.CreateZooPopUps(this.RefHeroQuestPack.heroquests[0], player, POPUPSTATE.HeroQuests, false);
          else if (this.RefHeroQuestPack.heroquests[0].CheckIsComplete(player))
            Z_GameFlags.HasCompleteQuestsToView = true;
          if (!Z_GameFlags.HasStartedFirstDay || this.RefHeroQuestPack.heroquests[0].WillAutoPin)
            ZHudManager.zquestpins.PinQuest(this.RefHeroQuestPack.heroquests[0], player);
          return true;
        }
      }
      else
      {
        for (int index = 0; index < this.RefHeroQuestPack.heroquests.Count; ++index)
        {
          if (!this.CompletedQuests.Contains(this.RefHeroQuestPack.heroquests[index].UID))
          {
            if (!this.RefHeroQuestPack.heroquests[index].CheckUnlockQuestsCriteriaComplete(player))
              return false;
            this.CurrentQuestIndex = this.RefHeroQuestPack.heroquests[index].UID;
            if (!Z_GameFlags.HasStartedFirstDay || this.RefHeroQuestPack.heroquests[index].WillAutoPin)
              ZHudManager.zquestpins.PinQuest(this.RefHeroQuestPack.heroquests[index], player);
            this.IsNew = true;
            if (this.RefHeroQuestPack.heroquests[index].WillAutoPopOnUnlock)
              OverWorldManager.zoopopupHolder.CreateZooPopUps(this.RefHeroQuestPack.heroquests[index], player, POPUPSTATE.HeroQuests, false);
            else
              Z_GameFlags.HasCompleteQuestsToView = true;
            return true;
          }
        }
      }
      return false;
    }

    public void LoadHeroProgressStatus(Reader reader)
    {
      int num1 = (int) reader.ReadInt("h", ref this.CurrentQuestIndex);
      int _out1 = 0;
      int num2 = (int) reader.ReadInt("h", ref _out1);
      this.CompletedQuests = new List<int>();
      for (int index = 0; index < _out1; ++index)
      {
        int _out2 = 0;
        int num3 = (int) reader.ReadInt("h", ref _out2);
        this.CompletedQuests.Add(_out2);
      }
      int num4 = (int) reader.ReadInt("h", ref this.ValueForThis);
      int num5 = (int) reader.ReadBool("h", ref this.IsUnlocked);
      int num6 = (int) reader.ReadBool("h", ref this.IsNew);
    }

    public void SaveHeroProgressStatus(Writer writer)
    {
      writer.WriteInt("h", this.CurrentQuestIndex);
      writer.WriteInt("h", this.CompletedQuests.Count);
      for (int index = 0; index < this.CompletedQuests.Count; ++index)
        writer.WriteInt("h", this.CompletedQuests[index]);
      writer.WriteInt("h", this.ValueForThis);
      writer.WriteBool("h", this.IsUnlocked);
      writer.WriteBool("h", this.IsNew);
    }
  }
}
