// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.CriticalChoice.CriticalChoiceManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Morality;
using TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData;
using TinyZoo.Z_SummaryPopUps.FeatureUnlock;

namespace TinyZoo.Z_SummaryPopUps.CriticalChoice
{
  internal class CriticalChoiceManager
  {
    public FeatureUnlockDisplayType newfeaturepopup;
    private CriticalChoicePanel panel;
    private WalkingPerson REF_person;
    private CriticalChoiceSet criticalchoiceset;

    public CriticalChoiceManager(WalkingPerson person)
    {
      this.REF_person = person;
      float baseScaleForUi = Z_GameFlags.GetBaseScaleForUI();
      this.criticalchoiceset = TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData.CriticalChoiceData.GetCriticalChoiceSet(person.simperson.customertype);
      this.panel = new CriticalChoicePanel(this.criticalchoiceset, baseScaleForUi);
      this.newfeaturepopup = FeatureUnlockDisplayType.Count;
      this.panel.location = Sengine.HalfReferenceScreenRes;
    }

    public bool UpdateCriticalChoiceManager(Player player, float DeltaTime)
    {
      int choice;
      int num = 0 | (this.panel.UpdateCriticalChoicePanel(player, Vector2.Zero, DeltaTime, out choice) ? 1 : 0);
      if (num == 0)
        return num != 0;
      this.ProcessCriticalChoiceResult(choice, player);
      return num != 0;
    }

    private void ProcessCriticalChoiceResult(int OptionPressed, Player player)
    {
      if (this.REF_person.simperson.customertype == CustomerType.ResearchGrantGuy)
        this.newfeaturepopup = FeatureUnlockDisplayType.ResearchHubUnlocked;
      else if (this.REF_person.simperson.customertype == CustomerType.GenomeBetaGiver)
        this.newfeaturepopup = FeatureUnlockDisplayType.CRIPSRUnlocked;
      else if (this.REF_person.simperson.customertype == CustomerType.AnimalArtist)
      {
        if (OptionPressed == 0)
        {
          Z_GameFlags.RecalculatedMorality = false;
          ++Player.criticalchoices.BadChoicesMade;
          MoralityCalculator.CalculateMorality(player);
        }
        this.newfeaturepopup = FeatureUnlockDisplayType.MoralityUsed;
      }
      this.REF_person.simperson.memberofthepublic.criticalchoiceVIP.SelectedChoice = OptionPressed;
      this.criticalchoiceset.CriticalActions[OptionPressed].Process(player, this.REF_person.simperson.customertype);
    }

    public void DrawCriticalChoiceManager() => this.panel.DrawCriticalChoicePanel(AssetContainer.pointspritebatchTop05, Vector2.Zero);
  }
}
