// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData.CriticalChoiceData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_Research_.RData;

namespace TinyZoo.Z_SummaryPopUps.EventReport.CriticalChoiceData
{
  internal class CriticalChoiceData
  {
    internal static CriticalChoiceSet GetCriticalChoiceSet(
      CustomerType customertype)
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      CriticalChoiceSet criticalChoiceSet;
      switch (customertype)
      {
        case CustomerType.ResearchGrantGuy:
          criticalChoiceSet = new CriticalChoiceSet(customertype, CriticalChoiceCharacter.Scientist);
          num1 = Player.criticalchoices.ChoiceIndexes[(int) criticalChoiceSet.criticalcharacter];
          criticalChoiceSet.animaltypeforprotrait = AnimalType.SpecialEvent_Scientist;
          if (num1 == -1)
            num1 = 0;
          if (num1 == 0 || num1 == 3)
          {
            int _ValueMainOrPercent = 250;
            int _Days_OtherValue = 14;
            if (num1 == 0)
              ++num1;
            criticalChoiceSet.SetMainText("Research Grant", "Build a research building", "Hi, I represent the Zoological Foundation for research, and I am here to offer you a grant!~If you build a research building before the end of the day, I will fund you for the next " + (object) _Days_OtherValue + " days!");
            criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.AddTimeLimitedQuest, CriticalChoiceDetailType.Funding, _ValueMainOrPercent, _Days_OtherValue, "Take on Task, and build a research office before the end of the day."));
            criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.Reject, CriticalChoiceDetailType.None, 50, 30, "Reject offer."));
            break;
          }
          int _ValueMainOrPercent1 = 500;
          int _Days_OtherValue1 = 14;
          criticalChoiceSet.SetMainText("Research Grant", "Build a research building", "So you rejected my offer... Well, let me double it. Start a research program here in your zoo and I will fund you at $" + (object) _ValueMainOrPercent1 + " for " + (object) _Days_OtherValue1 + " days.~This is my final offer.");
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.AddTimeLimitedQuest, CriticalChoiceDetailType.Funding, _ValueMainOrPercent1, _Days_OtherValue1, "Take on Task, and build a research office before the end of the day."));
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.Reject, CriticalChoiceDetailType.None, 0, 0, "Reject offer."));
          break;
        case CustomerType.AnimalArtist:
          criticalChoiceSet = new CriticalChoiceSet(customertype, CriticalChoiceCharacter.Painter);
          int choiceIndex = Player.criticalchoices.ChoiceIndexes[(int) criticalChoiceSet.criticalcharacter];
          criticalChoiceSet.animaltypeforprotrait = AnimalType.SpecialEvent_Artist;
          num1 = choiceIndex + 1;
          num2 = 25;
          num3 = 14;
          criticalChoiceSet.SetMainText("It's all art to me", "Commission a Fake Animal", "You have a horse, and I have an artistic eye! You know what a black horse with white stripes looks like?~That's right, a Zebra, I can get your customers thinking you have a far more exotic animal than you really do!~I just love painting, that's why I want to do this!");
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.GetPaintedAnimal, CriticalChoiceDetailType.Funding, 50, 30, "Paint your horse to look like a zebra?~Scam-imals earn you negative morality points", StarColour.Evil_Purple));
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.Reject, CriticalChoiceDetailType.None, 50, 30, "Reject offer."));
          break;
        case CustomerType.GenomeBetaGiver:
          criticalChoiceSet = new CriticalChoiceSet(customertype, CriticalChoiceCharacter.GenomeGuy);
          criticalChoiceSet.animaltypeforprotrait = AnimalType.SpecialEvent_GenomeScientist;
          criticalChoiceSet.SetMainText("Genome Donation", "Let's Splice the zoo", "The Mon-Santa company has asked me to donate some of their research to you. We have been mapping genomes of different animals for the last decade, and now with the discovery of CRISPR it is possible for you to create animals by literally editing their DNA. We are donating the blue prints for a CRISPR splicing facility and a pair of genomes of your choosing.");
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.PickGenome, CriticalChoiceDetailType.PickGenome, 0, 5, "Pick the RABBIT and SNAKE to create reptilian mammals"));
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.PickGenome, CriticalChoiceDetailType.PickGenome, 0, 54, "Pick the RABBIT and HIPPO for a Tiny-Big result"));
          break;
        default:
          criticalChoiceSet = new CriticalChoiceSet(customertype, CriticalChoiceCharacter.Painter);
          num1 = Player.criticalchoices.ChoiceIndexes[(int) criticalChoiceSet.criticalcharacter];
          criticalChoiceSet.animaltypeforprotrait = AnimalType.SpecialEvent_Artist;
          num2 = 25;
          num3 = 14;
          criticalChoiceSet.SetMainText("It's all art to me", "Commission an Fake Animal", "You have a horse, and I have an artistic eye! You know what a black horse with white stripes looks like?~That's right, a Zebra, I can get your customers thinking you have a far more exotic animal than you really do!~I just love painting, that's why I want to do this!");
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.AddQuest, CriticalChoiceDetailType.Funding, 50, 30, "Paint your horse to look like a zebra?~Scam-imals earn you negative morality points", StarColour.Evil_Purple));
          criticalChoiceSet.CriticalActions.Add(new CriticalChoiceAction(CriticalChoiceType.Reject, CriticalChoiceDetailType.None, 50, 30, "Reject offer."));
          break;
      }
      Player.criticalchoices.ChoiceIndexes[(int) criticalChoiceSet.criticalcharacter] = num1;
      return criticalChoiceSet;
    }
  }
}
