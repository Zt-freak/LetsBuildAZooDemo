// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData.CriticalChoiceAction
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.Tile_Data;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Quests.HeroQuests.OneDayQuests;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData
{
  internal class CriticalChoiceAction
  {
    public int ValueMainOrPercent;
    public int Days_OtherValue;
    public string TEXXXT;
    public CriticalChoiceType criticalchoicerype;
    public StarColour moralitytype;

    public CriticalChoiceAction(
      CriticalChoiceType _choicetype,
      CriticalChoiceDetailType detailtype,
      int _ValueMainOrPercent,
      int _Days_OtherValue,
      string _TEXXXT,
      StarColour _moralitytype = StarColour.Neutral)
    {
      this.moralitytype = _moralitytype;
      this.TEXXXT = _TEXXXT;
      this.Days_OtherValue = _Days_OtherValue;
      this.ValueMainOrPercent = _ValueMainOrPercent;
      this.criticalchoicerype = _choicetype;
    }

    internal static string GetCriticalChoiceCharacterToString(CriticalChoiceCharacter character)
    {
      switch (character)
      {
        case CriticalChoiceCharacter.Scientist:
          return "Isaac";
        case CriticalChoiceCharacter.Painter:
          return "Salvador";
        case CriticalChoiceCharacter.GenomeGuy:
          return "Nicole";
        default:
          return "NA";
      }
    }

    public string GetButtonLabel()
    {
      switch (this.criticalchoicerype)
      {
        case CriticalChoiceType.Reject:
          return "Reject";
        case CriticalChoiceType.AddTimeLimitedQuest:
        case CriticalChoiceType.GetPaintedAnimal:
          return "Accept";
        default:
          return "Choose";
      }
    }

    public void SetUpTimeLimitedQuest()
    {
    }

    public void Process(Player player, CustomerType customer)
    {
      if (customer == CustomerType.ResearchGrantGuy)
        player.Stats.research.BuildingsResearched.Add(TILETYPE.ArchitectOffice);
      if (this.criticalchoicerype == CriticalChoiceType.AddTimeLimitedQuest)
      {
        player.heroquestprogress.AddTempDailyQuest(OneDayQuestGetter.GetDayQuest(customer, player, this), player);
      }
      else
      {
        if (this.criticalchoicerype != CriticalChoiceType.PickGenome)
          return;
        player.Stats.research.BuildingsResearched.Add(TILETYPE.DNABuilding);
        player.Stats.variantsfound.ForceUnlockGenome((AnimalType) this.ValueMainOrPercent);
        player.Stats.variantsfound.ForceUnlockGenome((AnimalType) this.Days_OtherValue);
        QuestScrubber.ScrubOnResearchingOrUnlockBuilding(player);
      }
    }
  }
}
