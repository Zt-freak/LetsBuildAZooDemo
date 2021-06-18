// Decompiled with JetBrains decompiler
// Type: TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization.CustomizationData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;

namespace TinyZoo.TitleScreen.PickSaveSlot.SaveSumary.New.Customization
{
  internal class CustomizationData
  {
    internal static string GetCustomizationOptionToSTring(CustomizationOption option) => string.Concat((object) option);

    internal static int GetAvailableOptionCount(
      CustomizationOption option,
      int ThisOption,
      out string OptionName)
    {
      switch (option)
      {
        case CustomizationOption.StartMoney:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Highest: $100000";
              break;
            case 1:
              OptionName = "High: $10000";
              break;
            case 2:
              OptionName = "Default: $1000";
              break;
            default:
              OptionName = "Low: $500";
              break;
          }
          return 4;
        case CustomizationOption.CarryOverResearch:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Auto Unlock All Previously Researched";
              break;
            case 1:
              OptionName = "Previous Reserch Speed X2";
              break;
            default:
              OptionName = "Default: Off";
              break;
          }
          return 3;
        case CustomizationOption.Disease:
          switch (ThisOption)
          {
            case 0:
              OptionName = "None";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "High";
              break;
          }
          return 4;
        case CustomizationOption.GateIntegrity:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Indestructable";
              break;
            case 1:
              OptionName = "Hardy";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Fragile";
              break;
          }
          return 4;
        case CustomizationOption.AnimalFights:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Disabled";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Frequent";
              break;
          }
          return 4;
        case CustomizationOption.AnimalLifeSpans:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Unlimited";
              break;
            case 1:
              OptionName = "Double";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Short";
              break;
          }
          return 4;
        case CustomizationOption.AnimalTemperament:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Off";
              break;
            case 1:
              OptionName = "Docile";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Aggresive";
              break;
          }
          return 4;
        case CustomizationOption.Breeding:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Very High";
              break;
            case 1:
              OptionName = "High";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Low";
              break;
          }
          return 4;
        case CustomizationOption.PregnancyTime:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Very Short";
              break;
            case 1:
              OptionName = "Short";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Long";
              break;
          }
          return 4;
        case CustomizationOption.ZooTradeAnimalMultiplier:
          switch (ThisOption)
          {
            case 0:
              OptionName = "4X animals For Previously Completes Trades";
              break;
            case 1:
              OptionName = "2X animals for previously completed trades";
              break;
            default:
              OptionName = "Default";
              break;
          }
          return 3;
        case CustomizationOption.SocialGroupRequirements:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Off";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Precise";
              break;
          }
          return 4;
        case CustomizationOption.HabitatRequrements:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Off";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Extreme";
              break;
          }
          return 3;
        case CustomizationOption.AnimalDietResiliance:
          switch (ThisOption)
          {
            case 0:
              OptionName = "High";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Low";
              break;
          }
          return 3;
        case CustomizationOption.AnimalFoodVolume:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Half";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Double";
              break;
          }
          return 3;
        case CustomizationOption.EndlessMoney:
          OptionName = ThisOption != 0 ? "Default: Off" : "On";
          return 2;
        case CustomizationOption.IncomeMultiplier:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Double";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Half";
              break;
          }
          return 3;
        case CustomizationOption.CommoditiesValue:
          switch (ThisOption)
          {
            case 0:
              OptionName = "High";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Low";
              break;
          }
          return 3;
        case CustomizationOption.CommoditiesMarketFluctuation:
          switch (ThisOption)
          {
            case 0:
              OptionName = "None";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Unpredictable";
              break;
          }
          return 4;
        case CustomizationOption.InterestOnLoans:
          switch (ThisOption)
          {
            case 0:
              OptionName = "None";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "High";
              break;
          }
          return 4;
        case CustomizationOption.StaffFrustration:
          switch (ThisOption)
          {
            case 0:
              OptionName = "None";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "High";
              break;
          }
          return 4;
        case CustomizationOption.EmployeeSalaries:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Lowest";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "High";
              break;
          }
          return 3;
        case CustomizationOption.EmployeeMovementSpeed:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Fast";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Slow";
              break;
          }
          return 3;
        case CustomizationOption.EmployeeXPEarningRate:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Very Fast";
              break;
            case 1:
              OptionName = "Fast";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Slow";
              break;
          }
          return 4;
        case CustomizationOption.Police:
          switch (ThisOption)
          {
            case 0:
              OptionName = "No Police";
              break;
            case 1:
              OptionName = "Passive";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Aggresive";
              break;
          }
          return 4;
        case CustomizationOption.GuestEnticement:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Very Easy";
              break;
            case 1:
              OptionName = "Easy";
              break;
            case 2:
              OptionName = "Normal";
              break;
            default:
              OptionName = "Hard";
              break;
          }
          return 4;
        case CustomizationOption.CustomerEnergyUse:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Off";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "High";
              break;
          }
          return 4;
        case CustomizationOption.VIPVisits:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Off";
              break;
            case 1:
              OptionName = "Low";
              break;
            case 2:
              OptionName = "Default";
              break;
            default:
              OptionName = "Frequent";
              break;
          }
          return 4;
        case CustomizationOption.CropGrowingSpeed:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Fast";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Slow";
              break;
          }
          return 3;
        case CustomizationOption.FactoryProcessingTimes:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Fast";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Slow";
              break;
          }
          return 3;
        case CustomizationOption.CropYields:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Double";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Half";
              break;
          }
          return 3;
        case CustomizationOption.MeatProcessorYields:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Double";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Half";
              break;
          }
          return 3;
        case CustomizationOption.Quests:
          OptionName = ThisOption != 0 ? "Default: On" : "Off";
          return 2;
        case CustomizationOption.DayLength:
          switch (ThisOption)
          {
            case 0:
              OptionName = "Long";
              break;
            case 1:
              OptionName = "Default";
              break;
            default:
              OptionName = "Short";
              break;
          }
          return 3;
        case CustomizationOption.MoralityLimitsBuildings:
          OptionName = ThisOption != 0 ? "Default: On (Moriality limits structure usage)" : "Off (Can use buildings at all times)";
          return 2;
        default:
          throw new Exception("Missed This");
      }
    }

    internal static int GetDefault(CustomizationOption option)
    {
      switch (option)
      {
        case CustomizationOption.StartMoney:
          return 2;
        case CustomizationOption.CarryOverResearch:
          return 2;
        case CustomizationOption.Disease:
          return 2;
        case CustomizationOption.GateIntegrity:
          return 2;
        case CustomizationOption.AnimalFights:
          return 2;
        case CustomizationOption.AnimalLifeSpans:
          return 2;
        case CustomizationOption.AnimalTemperament:
          return 2;
        case CustomizationOption.Breeding:
          return 2;
        case CustomizationOption.PregnancyTime:
          return 2;
        case CustomizationOption.ZooTradeAnimalMultiplier:
          return 2;
        case CustomizationOption.SocialGroupRequirements:
          return 2;
        case CustomizationOption.HabitatRequrements:
          return 1;
        case CustomizationOption.AnimalDietResiliance:
          return 1;
        case CustomizationOption.AnimalFoodVolume:
          return 1;
        case CustomizationOption.EndlessMoney:
          return 1;
        case CustomizationOption.IncomeMultiplier:
          return 1;
        case CustomizationOption.CommoditiesValue:
          return 1;
        case CustomizationOption.CommoditiesMarketFluctuation:
          return 2;
        case CustomizationOption.InterestOnLoans:
          return 2;
        case CustomizationOption.StaffFrustration:
          return 2;
        case CustomizationOption.EmployeeSalaries:
          return 1;
        case CustomizationOption.EmployeeMovementSpeed:
          return 1;
        case CustomizationOption.EmployeeXPEarningRate:
          return 2;
        case CustomizationOption.Police:
          return 2;
        case CustomizationOption.GuestEnticement:
          return 1;
        case CustomizationOption.CustomerEnergyUse:
          return 2;
        case CustomizationOption.VIPVisits:
          return 2;
        case CustomizationOption.CropGrowingSpeed:
          return 1;
        case CustomizationOption.FactoryProcessingTimes:
          return 1;
        case CustomizationOption.CropYields:
          return 1;
        case CustomizationOption.MeatProcessorYields:
          return 1;
        case CustomizationOption.Quests:
          return 1;
        case CustomizationOption.DayLength:
          return 1;
        case CustomizationOption.MoralityLimitsBuildings:
          return 1;
        default:
          throw new Exception("Missed This");
      }
    }

    internal static bool GetCustomizationOptionAvailable(CustomizationOption option, int Option) => false;
  }
}
