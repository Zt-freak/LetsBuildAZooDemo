// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ZooValues.EmployeesStats
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;

namespace TinyZoo.Z_ZooValues
{
  internal class EmployeesStats
  {
    internal static EmployeeStats GetEmployeestats(
      EmployeeType employee,
      AnimalType _AnimalType,
      int Seniority = -1)
    {
      EmployeeStats employeeStats = new EmployeeStats();
      switch (employee)
      {
        case EmployeeType.Mascot:
          switch (_AnimalType)
          {
            case AnimalType.MascotGonky:
              employeeStats.MinimumWage = 500;
              employeeStats.MaximumWage = 600;
              employeeStats.JobDescription = "Do you have what it takes to become Gonky! Think Like Gonky, Live like Gonky, Dream like Gonky!~~We are looking for someone who can become one with our vistors through their realistic portrayal of Gonky!~Known as an imaginary friend, do you have what it takes to make that dream a reality for our vistors!~~5 years of professional Hollywood acting experience is a minimum requirement for this role!";
              break;
            case AnimalType.MascotOctoman:
              employeeStats.MinimumWage = 300;
              employeeStats.MaximumWage = 400;
              employeeStats.JobDescription = "It's the mighty Kraken!~~No, it's you dressed as one! Delight and terrify our customers with your portrayal of the gigantic octopus-human hybrid, born from the body of the mysterious GORSD.~~Have no idea what we are talking about? Then maybe this job isn't for you!";
              break;
            case AnimalType.MascotBear:
              employeeStats.MinimumWage = 200;
              employeeStats.MaximumWage = 250;
              employeeStats.JobDescription = "Rooooooar! Have you always wanted to be a bear, but lacked the hair to appeal to the bear lovers of the world?~~Well let your fantasies become a reality by working for us as a bear mascot!~Eat as much honey as you want, because to be a bear, you have gotta be big and huggable!~~We strictly enforce a no-romantic-interest-in-customers policy here at the zoo. This is a new policy since the bear's popularity has risen in recent years.";
              break;
            case AnimalType.MascotShark:
              employeeStats.MinimumWage = 100;
              employeeStats.MaximumWage = 150;
              employeeStats.JobDescription = "Despite being the name of a movie that only has one star on IMDB, we still feel that the zoo has space for a Sharkman Mascot!~~Rise from the oceans and delight our customers! Take on the role of Roy Scheider and Jaws at the same time, become an entire Steven Spielberg movie cast all at the same time!";
              break;
            case AnimalType.MascotSharkFace:
              employeeStats.MinimumWage = 100;
              employeeStats.MaximumWage = 150;
              employeeStats.JobDescription = "Despite being the name of a movie that only has one star on IMDB, we still feel that the zoo has space for a Sharkman Mascot!~~Rise from the oceans and delight our customers! Take on the role of Roy Scheider and Jaws at the same time, become an entire Steven Spielberg movie cast all at the same time!";
              break;
            case AnimalType.MascotPenguin:
              employeeStats.MinimumWage = 100;
              employeeStats.MaximumWage = 150;
              employeeStats.JobDescription = "Placeholder";
              break;
            case AnimalType.MascotPig:
              employeeStats.MinimumWage = 100;
              employeeStats.MaximumWage = 150;
              employeeStats.JobDescription = "Placeholder";
              break;
            case AnimalType.MascotPanda:
              employeeStats.MinimumWage = 100;
              employeeStats.MaximumWage = 150;
              employeeStats.JobDescription = "This Panda does martial arts! What kind I hear you ask? well no, not that kind....because that would infringe on Dreamworks Motions Pictures copyright! So We picked Karate!~You don't need to know Karate in order to apply, but having watched the Karate Kid 1, 2 & 3 is a plus.";
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Guide:
          employeeStats.MinimumWage = 250;
          employeeStats.MaximumWage = 350;
          employeeStats.JobDescription = "Do you like being the center of attention? Does it make you feel important when people hang on your every word?~Well listen up Narcisists, we have the perfect job for you, be a tour guide at our zoo, and tell people useless facts about things they don't care about.";
          switch (_AnimalType)
          {
            case AnimalType.TourGuideBlack:
            case AnimalType.TourGuideAsian:
            case AnimalType.TourGuideWhite:
            case AnimalType.TourGuideBlack2:
            case AnimalType.TourGuideAsian2:
            case AnimalType.TourGuideWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Janitor:
          employeeStats.MinimumWage = 20;
          employeeStats.MaximumWage = 80;
          employeeStats.JobDescription = "Do you hate to see a dirty floor? So do we! Come and help make our park goers happy by picking up the garbage they leave behind!~Expect a unique work enronments surrounded by animals, not that we usually refer to our customers that way.";
          switch (_AnimalType)
          {
            case AnimalType.CleanerBlack:
            case AnimalType.CleanerAsian:
            case AnimalType.CleanerWhite:
            case AnimalType.CleanerBlack2:
            case AnimalType.CleanerAsian2:
            case AnimalType.CleanerWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Keeper:
          int num1 = Game1.Rnd.Next(0, 3);
          if (Seniority > -1)
            num1 = Seniority;
          if (num1 == 0)
          {
            employeeStats.MinimumWage = 30;
            employeeStats.MaximumWage = 90;
            employeeStats.Seniority = 0;
            employeeStats.JobDescription = "Do you love animals and wish they loved you back?~~Take on the role of a zoo keeper and do your best to clean and feed the animals.~~Prerequisites:~Must love animals.~Have no history of animal cruelty.~~Bonus Skills:~Can tell the difference between a penguin and an elephant.";
          }
          else if (num1 == 0)
          {
            employeeStats.MinimumWage = 500;
            employeeStats.MaximumWage = 700;
            employeeStats.Seniority = 1;
            employeeStats.JobDescription = "Look after the animals in our zoo!~Feed them, hug them and pick up their excretions. It's all part of a day's work!~~Benefits: Uncompetitive salary, Animal hugs.~~Required Skills:~You must be alive.~Able to hold a shovel.~Respect Health and safety.";
          }
          else
          {
            employeeStats.MinimumWage = 800;
            employeeStats.MaximumWage = 1000;
            employeeStats.Seniority = 2;
            employeeStats.JobDescription = "Have you spent your life with animals? Can you tell the difference between the thinking end and the business end of a Giraffe?~~Then this might well be the job for you!~~Requirements:~Have owned a pet for at least 15 years of your life (hamsters, gerbils and guinea pigs are excluded).";
          }
          switch (_AnimalType)
          {
            case AnimalType.KeeperBlack:
            case AnimalType.KeeperAsian:
            case AnimalType.KeeperWhite:
            case AnimalType.KeeperBlack2:
            case AnimalType.KeeperAsian2:
            case AnimalType.KeeperWhite2:
              break;
            default:
              throw new Exception("NOT HIT");
          }
          break;
        case EmployeeType.Vet:
          int num2 = Game1.Rnd.Next(0, 3);
          if (Seniority > -1)
            num2 = Seniority;
          if (num2 == 0)
          {
            employeeStats.MinimumWage = 1000;
            employeeStats.MaximumWage = 1500;
            employeeStats.JobDescription = "Want to save animals but don't know where to start? Well work for us as a junior vet!~We give every inexperienced vet the opportunity to accidentally kill several animals before they get fired, so don't worry about a thing!";
          }
          else if (num2 == 0)
          {
            employeeStats.MinimumWage = 3000;
            employeeStats.MaximumWage = 4000;
            employeeStats.JobDescription = "Animals get sick just like people, and if you are applying for this job, then you know that already!~Have you ever administered an illegal dose of Stanozolol to a greyhound so it can win a race? If So then we want to hear from you, at our zoo the animals come firstm no matter the cost!";
          }
          else
          {
            employeeStats.MinimumWage = 10000;
            employeeStats.MaximumWage = 12000;
            employeeStats.JobDescription = "We are looking for someone who knows how to take care of animals! The Successful applicant will have worked with everying from house cats to elephants, and be able to tell whats wrong with an animal just from looking at them.";
          }
          switch (_AnimalType)
          {
            case AnimalType.VetBlack:
            case AnimalType.VetAsian:
            case AnimalType.VetWhite:
            case AnimalType.VetBlack2:
            case AnimalType.VetAsian2:
            case AnimalType.VetWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Mechanic:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.MechanicBlack:
            case AnimalType.MechanicAsian:
            case AnimalType.MechanicWhite:
            case AnimalType.MechanicBlack2:
            case AnimalType.MechanicAsian2:
            case AnimalType.MechanicWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.SecurityGuard:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.SecurityGuardBlack:
            case AnimalType.SecurityGuardAsian:
            case AnimalType.SecurityGuardWhite:
            case AnimalType.SecurityGuardBlack2:
            case AnimalType.SecurityGuardAsian2:
            case AnimalType.SecurityGuardWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Architect:
          employeeStats.MinimumWage = 40;
          employeeStats.MaximumWage = 100;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.ArchitectBlack:
            case AnimalType.ArchitectAsian:
            case AnimalType.ArchitectWhite:
            case AnimalType.ArchitectBlack2:
            case AnimalType.ArchitectAsian2:
            case AnimalType.ArchitectWhite2:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.ShopKeeper:
          employeeStats.MinimumWage = 30;
          employeeStats.MaximumWage = 70;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          break;
        case EmployeeType.Breeder:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.BreederWhiteMale:
            case AnimalType.BreederBlackMale:
            case AnimalType.BreederAsianMale:
            case AnimalType.BreederWhiteFemale:
            case AnimalType.BreederBlackFemale:
            case AnimalType.BreederAsianFemale:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.DNAResearcher:
          employeeStats.MinimumWage = 70;
          employeeStats.MaximumWage = 180;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.DNAResearcherAsianWithGoggles:
            case AnimalType.DNAResearcherAsianNoGoggles:
            case AnimalType.DNAResearcherBlackWithGoggles:
            case AnimalType.DNAResearcherBlackNoGoggles:
            case AnimalType.DNAResearcherWhiteWithGoggles:
            case AnimalType.DNAResearcherWhiteNoGoggles:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.MeatProcessorWorker:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.MeatProcessorWorkerAsianMale:
            case AnimalType.MeatProcessorWorkerAsianFemale:
            case AnimalType.MeatProcessorWorkerWhiteMale:
            case AnimalType.MeatProcessorWorkerWhiteFemale:
            case AnimalType.MeatProcessorWorkerBlackMale:
            case AnimalType.MeatProcessorWorkerBlackFemale:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.SlaughterhouseEmployee:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.SlaughterhouseEmployeeAsian:
            case AnimalType.SlaughterhouseEmployeeWhite:
            case AnimalType.SlaughterhouseEmployeeBlack:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.FactoryWorker:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.FactoryWorkerAsian:
            case AnimalType.FactoryWorkerWhite:
            case AnimalType.FactoryWorkerBlack:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.Farmer:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          switch (_AnimalType)
          {
            case AnimalType.FarmerWhiteMale:
            case AnimalType.FarmerWhiteFemale:
            case AnimalType.FarmerAsianMale:
            case AnimalType.FarmerAsianFemale:
            case AnimalType.FarmerBlackMale:
            case AnimalType.FarmerBlackFemale:
              break;
            default:
              throw new Exception("NO WAY");
          }
          break;
        case EmployeeType.VegProcessorWorker:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          break;
        case EmployeeType.WarehouseWorker:
          employeeStats.MinimumWage = 100;
          employeeStats.MaximumWage = 200;
          employeeStats.JobDescription = "PLACEHOLDER TEXT";
          break;
        case EmployeeType.Trainer:
          throw new Exception("NO WAY");
      }
      return employeeStats;
    }

    internal static string GetSeniorityPrepend(Seniority seniority)
    {
      switch (seniority)
      {
        case Seniority.Junior:
          return "Junior ";
        case Seniority.Intermediate:
          return "Intermediate ";
        case Seniority.Senior:
          return "Senior ";
        default:
          return "NA";
      }
    }

    internal static string GetJobTitle(
      EmployeeType employeetype,
      AnimalType _AnimalType,
      int Seniority = 0,
      bool IncludeSeniorityPrepend = true)
    {
      string str;
      switch (employeetype)
      {
        case EmployeeType.Mascot:
          switch (_AnimalType)
          {
            case AnimalType.MascotGonky:
              str = SEngine.Localization.Localization.GetText(994);
              break;
            case AnimalType.MascotOctoman:
              str = SEngine.Localization.Localization.GetText(996);
              break;
            case AnimalType.MascotBear:
              str = SEngine.Localization.Localization.GetText(995);
              break;
            case AnimalType.MascotShark:
              str = SEngine.Localization.Localization.GetText(997);
              break;
            case AnimalType.MascotSharkFace:
              str = SEngine.Localization.Localization.GetText(997);
              break;
            case AnimalType.MascotPenguin:
              str = SEngine.Localization.Localization.GetText(998);
              break;
            case AnimalType.MascotPig:
              str = SEngine.Localization.Localization.GetText(999);
              break;
            case AnimalType.MascotPanda:
              str = SEngine.Localization.Localization.GetText(1000);
              break;
            default:
              str = "Mascot";
              break;
          }
          break;
        case EmployeeType.Guide:
          str = SEngine.Localization.Localization.GetText(1008);
          break;
        case EmployeeType.Janitor:
          str = SEngine.Localization.Localization.GetText(1002);
          break;
        case EmployeeType.Keeper:
          str = SEngine.Localization.Localization.GetText(1001);
          break;
        case EmployeeType.Vet:
          str = SEngine.Localization.Localization.GetText(1011);
          break;
        case EmployeeType.Mechanic:
          str = SEngine.Localization.Localization.GetText(1004);
          break;
        case EmployeeType.SecurityGuard:
          str = SEngine.Localization.Localization.GetText(1003);
          break;
        case EmployeeType.Architect:
          str = SEngine.Localization.Localization.GetText(1016);
          break;
        case EmployeeType.ShopKeeper:
          str = SEngine.Localization.Localization.GetText(1005);
          break;
        case EmployeeType.Breeder:
          str = SEngine.Localization.Localization.GetText(1006);
          break;
        case EmployeeType.DNAResearcher:
          str = SEngine.Localization.Localization.GetText(1007);
          break;
        case EmployeeType.MeatProcessorWorker:
          str = SEngine.Localization.Localization.GetText(1009);
          break;
        case EmployeeType.SlaughterhouseEmployee:
          str = SEngine.Localization.Localization.GetText(1012);
          break;
        case EmployeeType.FactoryWorker:
          str = SEngine.Localization.Localization.GetText(1010);
          break;
        case EmployeeType.Farmer:
          str = SEngine.Localization.Localization.GetText(1013);
          break;
        case EmployeeType.VegProcessorWorker:
          str = SEngine.Localization.Localization.GetText(1014);
          break;
        case EmployeeType.WarehouseWorker:
          str = SEngine.Localization.Localization.GetText(1015);
          break;
        default:
          str = "NA_" + (object) employeetype;
          break;
      }
      return IncludeSeniorityPrepend ? string.Format(EmployeesStats.GetSeniorityPrepend((Seniority) Seniority) + "{0}", (object) str) : str;
    }
  }
}
