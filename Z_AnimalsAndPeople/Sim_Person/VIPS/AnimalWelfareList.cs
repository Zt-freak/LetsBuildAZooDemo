// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS.AnimalWelfareList
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_SummaryPopUps.EventReport;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.VIPS
{
  internal class AnimalWelfareList
  {
    public int PenUID;
    public PrisonerInfo MostDaysWithoutWater;
    public PrisonerInfo MostDaysWithoutFood;
    public float OverallWelfare;
    public float OverallPoop;
    public float OverallCorpse;
    public float QualityForSpace;
    private List<string> F_Rated;
    private List<string> C_Rated;
    private List<string> B_Rated;
    private List<string> A_Rated;
    private string Heading;
    public float EnrichmentDefecit;
    public float CollectiveCorpseAge;
    public float StressFromCohabitation;
    public float GroupSizeLoneliness;
    public float LargeGroupStress;
    public float AnimalHabitatMatch;
    public int TotalDaysWithoutFood;
    public int PeakDaysWithoutFood;
    public int TotalPeakNoFood;
    public int TotalDaysWithoutWater;
    public int PeakDaysWithoutWater;
    public int TotalPeakNoWater;
    public bool HasBuiltStoreRoom;
    public string Description;
    private bool WasMissingWater;
    private bool WasMissingFood;

    public AnimalWelfareList(int _PenUID) => this.PenUID = _PenUID;

    public List<string> GetFlavourText(bool WasBribed, bool IsTutorial)
    {
      List<string> stringList1 = new List<string>();
      if (WasBribed)
      {
        List<string> stringList2 = new List<string>();
        switch (Game1.Rnd.Next(0, 4))
        {
          case 0:
            stringList2.Add("Great zoo, Err, yeah it was great, almost as great as the new car I am going to buy.");
            break;
          case 1:
            stringList2.Add("I didn't see anything bad at the zoo. I left my glasses at home, so I can't be sure. But yes, perfect rating.");
            break;
          case 2:
            stringList2.Add("What a swell zoo, really good. I will sign this report with my real name. Yes, I am now known as Alan Smithee.");
            break;
          default:
            stringList2.Add("This zoo is a golden goose among zoos. I am overjoyed with the rewarding experience I had during my visit");
            break;
        }
        switch (Game1.Rnd.Next(0, 4))
        {
          case 0:
            stringList2.Add("Everything is perfect.");
            stringList2.Add("Yes, perfect.");
            stringList2.Add("Honestly, just perfect.");
            break;
          case 1:
            stringList2.Add("Nothing in this zoo feels like a prison.");
            stringList2.Add("I don't like prisons.");
            stringList2.Add("I hope to never visit one.");
            break;
          case 2:
            stringList2.Add("Best Zoo EVER.");
            stringList2.Add("Happiest animals.");
            stringList2.Add("Nicest payoff, I mean nicest people.");
            break;
          default:
            stringList2.Add("This zoo wins my award for best zoo.");
            stringList2.Add("No corruption. None at all.");
            stringList2.Add("I will be out of the office on a cruise for the next 2 weeks.");
            break;
        }
        return stringList2;
      }
      if (IsTutorial && (this.WasMissingWater || this.WasMissingFood))
      {
        stringList1.Add("Great to see a new zoo! Seems like you have a few things to solve. I will just give you some advice for now. But next time, I won't be so generous.");
        if (this.WasMissingWater)
          stringList1.Add("You do not have water coverage for your animals. Build water troughs and ensure water pump coverage.");
        if (this.WasMissingFood)
        {
          if (!this.HasBuiltStoreRoom)
            stringList1.Add("Your animals have gone without food! Buy a store room right away!");
          else
            stringList1.Add("Your animals need more time to be fed and raised lovingly.");
        }
        stringList1.Add("Your animals are not being treated very well, please try and do better. Next time, I won't be so lenient.");
        return stringList1;
      }
      int rank = (int) this.GetRank(true, WasBribed);
      List<string> stringList3 = new List<string>();
      if (IsTutorial)
        stringList3.Add("It is a promising start to a new zoo! But there wasn't much animals to even see, so it's difficult to say!");
      else
        stringList3.Add(this.Heading);
      for (int index = 0; index < this.F_Rated.Count; ++index)
      {
        if (stringList3.Count < 4)
          stringList3.Add(this.F_Rated[index]);
      }
      for (int index = 0; index < this.C_Rated.Count; ++index)
      {
        if (stringList3.Count < 4)
          stringList3.Add(this.C_Rated[index]);
      }
      for (int index = 0; index < this.B_Rated.Count; ++index)
      {
        if (stringList3.Count < 4)
          stringList3.Add(this.B_Rated[index]);
      }
      for (int index = 0; index < this.A_Rated.Count; ++index)
      {
        if (stringList3.Count < 4)
          stringList3.Add(this.A_Rated[index]);
      }
      return stringList3;
    }

    public ReportResultRank GetRank(bool GenerateString = false, bool WasBribed = false)
    {
      int num1 = this.GetDaysToRank(this.PeakDaysWithoutWater);
      this.WasMissingWater = false;
      this.WasMissingFood = false;
      int num2 = 0;
      this.F_Rated = new List<string>();
      this.C_Rated = new List<string>();
      this.B_Rated = new List<string>();
      this.A_Rated = new List<string>();
      this.WasMissingWater = this.PeakDaysWithoutWater > 0;
      if (GenerateString)
      {
        switch (num1)
        {
          case 0:
            this.A_Rated.Add("Hygiene was perfect.");
            break;
          case 1:
            ++num2;
            this.B_Rated.Add("Some animals have had no access to water for a couple of days.");
            break;
          case 2:
            ++num2;
            this.C_Rated.Add("Some animals have had no access to water for more than 5 days.");
            break;
          case 3:
            ++num2;
            this.F_Rated.Add("Some animals have had no access to water for more than 10 days.");
            break;
        }
      }
      int daysToRank = this.GetDaysToRank(this.PeakDaysWithoutFood);
      this.WasMissingFood = this.PeakDaysWithoutFood > 0;
      if (GenerateString)
      {
        switch (daysToRank)
        {
          case 0:
            this.A_Rated.Add("All animals are happy, watered and fed.");
            break;
          case 1:
            ++num2;
            this.B_Rated.Add("Some of your animals have not been fed for a couple of days.");
            break;
          case 2:
            ++num2;
            this.C_Rated.Add("Some of your animals have not been fed for more than 5 days.");
            break;
          case 3:
            ++num2;
            this.F_Rated.Add("Some of your animals have not been fed for more than 10 days.");
            break;
        }
      }
      if (daysToRank < num1)
        num1 = daysToRank;
      if ((double) this.QualityForSpace < 1.0)
      {
        int floatToRating = Z_GameFlags.GetFloatToRating(this.QualityForSpace);
        if (floatToRating > num1)
          num1 = floatToRating;
        switch (floatToRating)
        {
          case 0:
            this.A_Rated.Add("All animals look to be in good shape.");
            break;
          case 1:
            ++num2;
            this.B_Rated.Add("Your zoo has some overcrowding problems.");
            break;
          case 2:
            ++num2;
            this.C_Rated.Add("There are some quite overcrowded enclosures in your zoo.");
            break;
          case 3:
            ++num2;
            this.F_Rated.Add("There are some unacceptable overcrowding issues in your zoo.");
            break;
        }
      }
      int floatToRating1 = Z_GameFlags.GetFloatToRating(this.OverallWelfare);
      if (GenerateString)
      {
        switch (floatToRating1)
        {
          case 1:
            int num3 = num2 + 1;
            if ((double) this.OverallPoop > (double) this.OverallCorpse)
            {
              num2 = num3 + 1;
              this.B_Rated.Add("Some enclosures have animal waste that needs cleaning.");
              break;
            }
            num2 = num3 + 1;
            this.B_Rated.Add("Some enclosures have dead animals inside.");
            break;
          case 2:
            int num4 = num2 + 1;
            if ((double) this.OverallPoop > (double) this.OverallCorpse)
            {
              num2 = num4 + 1;
              this.C_Rated.Add("There are quite a lot of animal droppings contaminating the living space.");
              break;
            }
            num2 = num4 + 1;
            this.C_Rated.Add("There are quite a few corpses present in the living space.");
            break;
          case 3:
            int num5 = num2 + 1;
            if ((double) this.OverallPoop > (double) this.OverallCorpse)
            {
              num2 = num5 + 1;
              this.F_Rated.Add("The volume of animal faeces is unacceptably high. The stench and health risk is shocking.");
              break;
            }
            num2 = num5 + 1;
            this.F_Rated.Add("The volume of carcasses is shocking to behold.");
            break;
        }
      }
      if (floatToRating1 > num1)
        num1 = Z_GameFlags.GetFloatToRating(this.OverallWelfare);
      if (Z_GameFlags.GetFloatToRating((float) (1.0 - (double) this.CollectiveCorpseAge * 0.200000002980232)) > num1)
      {
        num1 = Z_GameFlags.GetFloatToRating(this.CollectiveCorpseAge);
        ++num2;
        if (GenerateString)
          this.C_Rated.Add("These corpses, are not fresh.");
      }
      int num6 = 0;
      if ((double) this.EnrichmentDefecit > 10.0)
      {
        ++num6;
        ++num2;
        if (GenerateString)
          this.B_Rated.Add("Your animals are under enriched.");
      }
      if ((double) this.StressFromCohabitation > 3.0)
      {
        ++num6;
        ++num2;
        if (GenerateString)
          this.B_Rated.Add("Some of your animals are sharing their enclosure with animals they consider a threat.");
      }
      if ((double) this.GroupSizeLoneliness > 3.0)
      {
        ++num6;
        ++num2;
        if (GenerateString)
          this.B_Rated.Add("Some of your animals are in such small groups of their own species that they feel lonely.");
      }
      if ((double) this.LargeGroupStress > 3.0)
      {
        ++num6;
        ++num2;
        if (GenerateString)
          this.B_Rated.Add("Some of your animals are in such large social groups that they are negatively impacted.");
      }
      if ((double) this.AnimalHabitatMatch < 1.0)
      {
        ++num6;
        int num7;
        if ((double) this.AnimalHabitatMatch < 0.5)
        {
          num7 = num2 + 1;
          if (GenerateString)
            this.B_Rated.Add("Some of your animals are not in their ideal habitats.");
          ++num6;
        }
        else
        {
          num7 = num2 + 1;
          if (GenerateString)
            this.C_Rated.Add("Some animals are in an extreme mismatch of environment.");
        }
      }
      if (num6 > num1)
      {
        num1 = num6;
        if (num1 > 3)
          num1 = 3;
      }
      switch (num1)
      {
        case 0:
          switch (Game1.Rnd.Next(0, 4))
          {
            case 0:
              this.Heading = "Your zoo is a Nirvana for animals, they all look very happy and content. Keep up the exceptional work!";
              break;
            case 1:
              this.Heading = "This zoo is just wonderful! I wish everyone cared for animals as much as you do.";
              break;
            case 2:
              this.Heading = "So I came here with high expectations, and you met them. I am going to tell all other zoos to look to here as an example of how to treat animals well!";
              break;
            default:
              this.Heading = "The animals here are so happy! I doubt they'd even want to return to the wild! You have created a wonderful environment for them and are looking after them exceptionally well!";
              break;
          }
          break;
        case 1:
          switch (Game1.Rnd.Next(0, 4))
          {
            case 0:
              this.Heading = "Overall you are doing well, but there is room for improvement.";
              break;
            case 1:
              this.Heading = "I can see you really care for the animals, but there is space to care a little bit more!";
              break;
            case 2:
              this.Heading = "I respect what you have done here, and on the whole, the animals seem happy. But just push their lives up a little more and we could have a perfect environment!";
              break;
            default:
              this.Heading = "This is a good zoo, not a great zoo, but a good zoo. Let's carry on trying to make zoos great again.";
              break;
          }
          break;
        case 3:
          switch (Game1.Rnd.Next(0, 4))
          {
            case 0:
              this.Heading = "You make me very unhappy, and if I feel this way, I can't imagine how your animals are feeling right now.";
              break;
            case 1:
              this.Heading = "The abuse here is unacceptable. You know the evil people in those super hero movies? You are more despicable than them.";
              break;
            case 2:
              this.Heading = "If I could lock you up and ban you from this industry, I would. You have to improve the quality of life for your animals!";
              break;
            default:
              this.Heading = "The legal system has my hands tied. All I can do is fine you, and if you carry on paying them, there is nothing I can do to end this cruelty.";
              break;
          }
          break;
      }
      return (ReportResultRank) num1;
    }

    private int GetDaysToRank(int Value)
    {
      if (Value > 7)
        return 3;
      if (Value > 5)
        return 2;
      return Value > 1 ? 1 : 0;
    }
  }
}
