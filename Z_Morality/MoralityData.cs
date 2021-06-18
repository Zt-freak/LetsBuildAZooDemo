// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Morality.MoralityData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using System.Collections.Generic;
using TinyZoo.Z_Morality.Calculators;
using TinyZoo.Z_Morality.Calculators.Polution;

namespace TinyZoo.Z_Morality
{
  internal class MoralityData
  {
    private static List<MoralityType> AnimalWelfareContents;
    private static List<MoralityType> CustomerTreatmentContents;
    private static List<MoralityType> EmployeeTreatmentContents;
    private static List<MoralityType> IndustryContents;
    private static List<MoralityType> BusnessContents;
    private static List<MoralityType> PollutionContents;
    internal static float MaxIngredientScale = 10f;
    internal static float MaxUnionScale = 20f;
    internal static float MaxBreedingScale = 5f;
    internal static float MaxPollution_Fuel = 10f;
    internal static float MaxPollution_MeatScale = 30f;
    internal static float MaxPollution_ElectricityScale = 5f;
    internal static float MaxPollution_MaxGarbage = 30f;
    internal static float MaxCriticalChoice = 20f;

    internal static float GetMoralityScore(Player player, MoralityType moralityType, out float max)
    {
      float num = 0.0f;
      max = 0.0f;
      switch (moralityType)
      {
        case MoralityType.FoodQuality:
          Morality_ShopStats.CalculateMorality_ShopStats(player, ref num);
          max = MoralityData.MaxIngredientScale;
          break;
        case MoralityType.Unions:
          Morality_UnionCalculator.CalculateMorality(player, ref num);
          max = MoralityData.MaxUnionScale;
          break;
        case MoralityType.GarbageGenerated:
          Morality_Garbage.CalcMorality_Garbage(player, ref num);
          max = MoralityData.MaxPollution_MaxGarbage;
          break;
        case MoralityType.ElectricityUse:
          Morality_Electricity.CalcMorality_Electricity(player, ref num);
          max = MoralityData.MaxPollution_ElectricityScale;
          break;
        case MoralityType.CarbonMeat:
          Morality_CarbonMeat.CalcMorality_CarbonMeat(player, ref num);
          max = MoralityData.MaxPollution_MeatScale;
          break;
        case MoralityType.CarbonFuel:
          Morality_CarbonFuel.CalcMorality_CarbonFuel(player, ref num);
          max = MoralityData.MaxPollution_Fuel;
          break;
        case MoralityType.Breeding:
          Morality_Breeding.CalcMorality_Breeding(player, ref num);
          max = MoralityData.MaxBreedingScale;
          break;
        case MoralityType.CriticalChoice:
          Morality_CriticalChoice.CalculateMorality_CriticalChoice(ref num);
          max = MoralityData.MaxCriticalChoice;
          break;
        default:
          Console.WriteLine("MORALITY TYPE NOT DONE: " + (object) moralityType);
          break;
      }
      return num;
    }

    internal static float GetCategoryMoralityScore(
      MoralityCategory category,
      Player player,
      out float totalMax)
    {
      float num = 0.0f;
      totalMax = 0.0f;
      List<MoralityType> maralityEntres = MoralityData.GetMaralityEntres(category);
      for (int index = 0; index < maralityEntres.Count; ++index)
      {
        float max;
        num += MoralityData.GetMoralityScore(player, maralityEntres[index], out max);
        totalMax += max;
      }
      return num;
    }

    internal static string GetDescription(MoralityType moralityType)
    {
      switch (moralityType)
      {
        case MoralityType.FoodQuality:
          return "Quality of ingredients used in the food sold to visitors";
        case MoralityType.Unions:
          return "Union pressure";
        case MoralityType.GarbageGenerated:
          return "How much trash your zoo is generating";
        case MoralityType.ElectricityUse:
          return "How much energy unsustainable energy you use";
        case MoralityType.CarbonMeat:
          return "How many animals die to feed your customers and animals";
        case MoralityType.CarbonFuel:
          return "How much fuel is used for deliveries and bus services";
        case MoralityType.Breeding:
          return "How early babies are taken from their mother";
        case MoralityType.CriticalChoice:
          return "Your negotiations with other people.";
        case MoralityType.Beta:
          return "More morality options available in the final game!";
        default:
          throw new Exception("MISSED THIS");
      }
    }

    internal static string GetHeading(MoralityType moralityType)
    {
      switch (moralityType)
      {
        case MoralityType.FoodQuality:
          return "Food Quality";
        case MoralityType.Unions:
          return "Unions";
        case MoralityType.GarbageGenerated:
          return "Garbage/Recycling";
        case MoralityType.ElectricityUse:
          return "Electricity Consumption";
        case MoralityType.CarbonMeat:
          return "Death for Food";
        case MoralityType.CarbonFuel:
          return "Carbon From Fuel";
        case MoralityType.Breeding:
          return "Breed Building";
        case MoralityType.CriticalChoice:
          return "Critical Choices";
        case MoralityType.Beta:
          return "Beta Version";
        default:
          throw new Exception("MISSED THIS");
      }
    }

    internal static List<MoralityType> GetMaralityEntres(MoralityCategory category)
    {
      switch (category)
      {
        case MoralityCategory.AnimalWelfare:
          if (MoralityData.AnimalWelfareContents == null)
          {
            MoralityData.AnimalWelfareContents = new List<MoralityType>();
            MoralityData.AnimalWelfareContents.Add(MoralityType.Breeding);
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.AnimalWelfareContents.Add(MoralityType.Beta);
          }
          return MoralityData.AnimalWelfareContents;
        case MoralityCategory.CustomerTreatment:
          if (MoralityData.CustomerTreatmentContents == null)
          {
            MoralityData.CustomerTreatmentContents = new List<MoralityType>();
            MoralityData.CustomerTreatmentContents.Add(MoralityType.FoodQuality);
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.CustomerTreatmentContents.Add(MoralityType.Beta);
          }
          return MoralityData.CustomerTreatmentContents;
        case MoralityCategory.EmployeeTreatment:
          if (MoralityData.EmployeeTreatmentContents == null)
          {
            MoralityData.EmployeeTreatmentContents = new List<MoralityType>();
            MoralityData.EmployeeTreatmentContents.Add(MoralityType.Unions);
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.EmployeeTreatmentContents.Add(MoralityType.Beta);
          }
          return MoralityData.EmployeeTreatmentContents;
        case MoralityCategory.Industry:
          if (MoralityData.IndustryContents == null)
          {
            MoralityData.IndustryContents = new List<MoralityType>();
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.IndustryContents.Add(MoralityType.Beta);
          }
          return MoralityData.IndustryContents;
        case MoralityCategory.Business:
          if (MoralityData.BusnessContents == null)
          {
            MoralityData.BusnessContents = new List<MoralityType>();
            MoralityData.BusnessContents.Add(MoralityType.CriticalChoice);
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.BusnessContents.Add(MoralityType.Beta);
          }
          return MoralityData.BusnessContents;
        case MoralityCategory.Pollution:
          if (MoralityData.PollutionContents == null)
          {
            MoralityData.PollutionContents = new List<MoralityType>();
            MoralityData.PollutionContents.Add(MoralityType.GarbageGenerated);
            MoralityData.PollutionContents.Add(MoralityType.ElectricityUse);
            MoralityData.PollutionContents.Add(MoralityType.CarbonMeat);
            MoralityData.PollutionContents.Add(MoralityType.CarbonFuel);
            if (Z_DebugFlags.IsBetaVersion)
              MoralityData.PollutionContents.Add(MoralityType.Beta);
          }
          return MoralityData.PollutionContents;
        default:
          throw new Exception("MISSED THIS");
      }
    }

    public static string GetMoralityCategoryToString(MoralityCategory category, bool ShortVersion = false)
    {
      switch (category)
      {
        case MoralityCategory.AnimalWelfare:
          return ShortVersion ? "Animals" : "Animal Welfare";
        case MoralityCategory.CustomerTreatment:
          return ShortVersion ? "Visitors" : "Visitor Treatment";
        case MoralityCategory.EmployeeTreatment:
          return ShortVersion ? "Employees" : "Employee Treatment";
        case MoralityCategory.Industry:
          return "Industry";
        case MoralityCategory.Business:
          return "Business";
        case MoralityCategory.Pollution:
          return "Pollution";
        default:
          return "NA_" + (object) category;
      }
    }

    public static string GetDisplayStringForMoralityValue(float value, bool CropIfDecimal0 = true)
    {
      string str = Math.Abs(value).ToString("F1");
      if (CropIfDecimal0)
        str = str.Replace(".0", "");
      return str;
    }
  }
}
