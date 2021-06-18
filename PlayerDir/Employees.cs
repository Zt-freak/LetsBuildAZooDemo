// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Employees
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.employees;
using TinyZoo.PlayerDir.employees.openpositions;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_Notification;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.PlayerDir
{
  internal class Employees
  {
    public List<Employee> employees;
    public PotentialHires potentialhires;
    public OpenPositionsContainer openPositionsContainer;
    private static List<AnimalType> Mascots;
    private static List<AnimalType> Keepers;
    private static List<AnimalType> Guide;
    private static List<AnimalType> Janitor;
    private static List<AnimalType> Vet;
    private static List<AnimalType> Breeder;
    private static List<AnimalType> DNAResearcher;
    private static List<AnimalType> MeatProcessorWorker;
    private static List<AnimalType> SlaughterhouseEmployee;
    private static List<AnimalType> Architect;
    private static List<AnimalType> Mechanic;
    private static List<AnimalType> SecurityGuard;
    private static List<AnimalType> Police;
    private static List<AnimalType> ShopKeeper;
    private static List<AnimalType> FactoryWorker;
    private static List<AnimalType> Farmer;
    private static List<AnimalType> Deliveryman;
    private static List<AnimalType> VegProcessorWorker;
    private static List<AnimalType> WarehouseWorker;

    public Employees()
    {
      Z_Research.ResetResearchGuys();
      this.employees = new List<Employee>();
      this.potentialhires = new PotentialHires(this.employees);
      this.openPositionsContainer = new OpenPositionsContainer();
    }

    public void RemoveEmployee(Employee employee)
    {
      if (employee.employeetype == EmployeeType.Architect)
        Z_Research.AddResearchGuy(employee, false);
      this.employees.Remove(employee);
    }

    public void EndedADay()
    {
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].EndedADay();
    }

    public void StartNewDay()
    {
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].StartNewDay();
    }

    internal static WorkZoneType GetThisEmplyeesWorkZoneType(EmployeeType employeetype)
    {
      switch (employeetype)
      {
        case EmployeeType.Mascot:
        case EmployeeType.Janitor:
        case EmployeeType.Mechanic:
        case EmployeeType.SecurityGuard:
          return WorkZoneType.SingleZone;
        case EmployeeType.Keeper:
        case EmployeeType.Vet:
          return WorkZoneType.Pens;
        default:
          throw new Exception("CANNOT - CHCK FUNCTION BELOW");
      }
    }

    internal static bool ThisEmplyeeSupportsZoning(EmployeeType employeetype)
    {
      switch (employeetype)
      {
        case EmployeeType.Mascot:
        case EmployeeType.Janitor:
        case EmployeeType.Keeper:
        case EmployeeType.Vet:
        case EmployeeType.Mechanic:
        case EmployeeType.SecurityGuard:
          return true;
        default:
          return false;
      }
    }

    public void StartNewWeek()
    {
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].StartNewWeek();
    }

    public int GetSalaries()
    {
      int num = 0;
      for (int index = 0; index < this.employees.Count; ++index)
        num += this.employees[index].Salary;
      return num;
    }

    public Employee AddThisEmplyee(
      IntakePerson intakeperson,
      EmployeeType employthis,
      int AgreedWage,
      int Determination,
      Player player,
      QuickEmployeeDescription quickemployeeDesc = null)
    {
      Z_NotificationManager.RescrubJobApplicants = true;
      this.employees.Add(new Employee(intakeperson, employthis, AgreedWage, Determination, quickemployeeDesc));
      if (this.employees[this.employees.Count - 1].employeetype == EmployeeType.Architect)
        Z_Research.AddResearchGuy(this.employees[this.employees.Count - 1], true);
      Z_NotificationManager.RecountAllEvents = true;
      Z_NotificationManager.RescrubShops = true;
      QuestScrubber.ScrubOnGettingNewEmployee(player);
      return this.employees[this.employees.Count - 1];
    }

    public int GetTotalArchitects()
    {
      int num = 0;
      for (int index = 0; index < this.employees.Count; ++index)
      {
        if (this.employees[index].employeetype == EmployeeType.Architect)
          ++num;
      }
      return num;
    }

    public List<Employee> GetEmployeesInThisSpecificShop(int ShopUID)
    {
      List<Employee> employeeList = new List<Employee>();
      foreach (Employee employee in this.employees)
      {
        if (employee.quickemplyeedescription != null && employee.quickemplyeedescription.ShopUID == ShopUID)
          employeeList.Add(employee);
      }
      return employeeList;
    }

    public int GetCountEmployeesOfThisType(EmployeeType enmployeeType)
    {
      int num = 0;
      foreach (Employee employee in this.employees)
      {
        if (employee.employeetype == enmployeeType)
          ++num;
      }
      return num;
    }

    public List<Employee> GetEmployeesOfThisType(EmployeeType enmployeeType)
    {
      List<Employee> employeeList = new List<Employee>();
      foreach (Employee employee in this.employees)
      {
        if (employee.employeetype == enmployeeType)
          employeeList.Add(employee);
      }
      return employeeList;
    }

    public List<Employee> GetEmployeesInThisTileType(TILETYPE tileType)
    {
      List<Employee> employeeList = new List<Employee>();
      foreach (Employee employee in this.employees)
      {
        if (employee.quickemplyeedescription.WorksHere == tileType)
          employeeList.Add(employee);
      }
      return employeeList;
    }

    public void SaveEmployees(Writer writer)
    {
      writer.WriteInt("e", this.employees.Count);
      for (int index = 0; index < this.employees.Count; ++index)
        this.employees[index].SaveEmployee(writer);
      this.openPositionsContainer.SaveOpenPositionsContainer(writer);
    }

    public Employees(Reader reader, int VersionForLoad)
    {
      int _out = 0;
      int num = (int) reader.ReadInt("e", ref _out);
      this.employees = new List<Employee>();
      Z_Research.ResetResearchGuys();
      for (int index = 0; index < _out; ++index)
      {
        this.employees.Add(new Employee(reader, VersionForLoad));
        if (this.employees[index].employeetype == EmployeeType.Architect)
          Z_Research.AddResearchGuy(this.employees[index], true);
      }
      this.openPositionsContainer = new OpenPositionsContainer(reader, VersionForLoad);
    }

    internal static string GetEmployeeTypeDescription(EmployeeType employeetype)
    {
      switch (employeetype)
      {
        case EmployeeType.Mascot:
          return "Entertains customers, collects donations.";
        case EmployeeType.Guide:
          return "Leads groups around the zoo for money.";
        case EmployeeType.Janitor:
          return "Cleans up trash";
        case EmployeeType.Keeper:
          return "Feeds and cleans enclosures";
        case EmployeeType.Vet:
          return "Check diseases, and research cures.";
        case EmployeeType.Mechanic:
          return "Repairs enclosures, structures and rides.";
        case EmployeeType.Architect:
          return "Designs or invents new things";
        case EmployeeType.ShopKeeper:
          return "Works in shops to sell things.";
        case EmployeeType.Breeder:
          return "Manages selective breeding programs";
        case EmployeeType.DNAResearcher:
          return "Splices mapped Genomes using CRISPR to create new animal species.";
        case EmployeeType.MeatProcessorWorker:
          return "Converts dead animals into useful raw materials";
        case EmployeeType.SlaughterhouseEmployee:
          return "Converts living animals to dead ones.";
        case EmployeeType.FactoryWorker:
          return "Works prodction lines, wen turning animals into industry";
        case EmployeeType.Farmer:
          return "Plants seeds, and looks after crops.";
        case EmployeeType.VegProcessorWorker:
          return "Converts farmed organics into useful things.";
        case EmployeeType.WarehouseWorker:
          return "Transports products ready for sale and storage.";
        case EmployeeType.Trainer:
          return "NA";
        default:
          return "NA_DESC_" + (object) employeetype;
      }
    }

    internal static string GetEmployeeThypeToString(EmployeeType employeetype) => EmployeesStats.GetJobTitle(employeetype, AnimalType.None, IncludeSeniorityPrepend: false);

    internal static List<AnimalType> GetEmployeeAsLict(EmployeeType employee)
    {
      switch (employee)
      {
        case EmployeeType.Mascot:
          if (Employees.Mascots == null)
          {
            Employees.Mascots = new List<AnimalType>();
            Employees.Mascots.Add(AnimalType.MascotGonky);
            Employees.Mascots.Add(AnimalType.MascotOctoman);
            Employees.Mascots.Add(AnimalType.MascotBear);
            Employees.Mascots.Add(AnimalType.MascotShark);
            Employees.Mascots.Add(AnimalType.MascotSharkFace);
            Employees.Mascots.Add(AnimalType.MascotPenguin);
            Employees.Mascots.Add(AnimalType.MascotPig);
            Employees.Mascots.Add(AnimalType.MascotPanda);
          }
          return Employees.Mascots;
        case EmployeeType.Guide:
          if (Employees.Guide == null)
          {
            Employees.Guide = new List<AnimalType>();
            Employees.Guide.Add(AnimalType.TourGuideWhite);
            Employees.Guide.Add(AnimalType.TourGuideBlack);
            Employees.Guide.Add(AnimalType.TourGuideAsian);
            Employees.Guide.Add(AnimalType.TourGuideWhite2);
            Employees.Guide.Add(AnimalType.TourGuideBlack2);
            Employees.Guide.Add(AnimalType.TourGuideAsian2);
          }
          return Employees.Guide;
        case EmployeeType.Janitor:
          if (Employees.Janitor == null)
          {
            Employees.Janitor = new List<AnimalType>();
            Employees.Janitor.Add(AnimalType.CleanerWhite);
            Employees.Janitor.Add(AnimalType.CleanerBlack);
            Employees.Janitor.Add(AnimalType.CleanerAsian);
            Employees.Janitor.Add(AnimalType.CleanerWhite2);
            Employees.Janitor.Add(AnimalType.CleanerBlack2);
            Employees.Janitor.Add(AnimalType.CleanerAsian2);
          }
          return Employees.Janitor;
        case EmployeeType.Keeper:
          if (Employees.Keepers == null)
          {
            Employees.Keepers = new List<AnimalType>();
            Employees.Keepers.Add(AnimalType.KeeperAsian);
            Employees.Keepers.Add(AnimalType.KeeperBlack);
            Employees.Keepers.Add(AnimalType.KeeperWhite);
            Employees.Keepers.Add(AnimalType.KeeperAsian2);
            Employees.Keepers.Add(AnimalType.KeeperBlack2);
            Employees.Keepers.Add(AnimalType.KeeperWhite2);
          }
          return Employees.Keepers;
        case EmployeeType.Vet:
          if (Employees.Vet == null)
          {
            Employees.Vet = new List<AnimalType>();
            Employees.Vet.Add(AnimalType.VetWhite);
            Employees.Vet.Add(AnimalType.VetBlack);
            Employees.Vet.Add(AnimalType.VetAsian);
            Employees.Vet.Add(AnimalType.VetWhite2);
            Employees.Vet.Add(AnimalType.VetBlack2);
            Employees.Vet.Add(AnimalType.VetAsian2);
          }
          return Employees.Vet;
        case EmployeeType.Mechanic:
          if (Employees.Mechanic == null)
          {
            Employees.Mechanic = new List<AnimalType>();
            Employees.Mechanic.Add(AnimalType.MechanicAsian);
            Employees.Mechanic.Add(AnimalType.MechanicAsian2);
            Employees.Mechanic.Add(AnimalType.MechanicBlack);
            Employees.Mechanic.Add(AnimalType.MechanicBlack2);
            Employees.Mechanic.Add(AnimalType.MechanicWhite);
            Employees.Mechanic.Add(AnimalType.MechanicWhite2);
          }
          return Employees.Mechanic;
        case EmployeeType.SecurityGuard:
          if (Employees.SecurityGuard == null)
          {
            Employees.SecurityGuard = new List<AnimalType>();
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardAsian);
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardAsian2);
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardBlack);
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardBlack2);
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardWhite);
            Employees.SecurityGuard.Add(AnimalType.SecurityGuardWhite2);
          }
          return Employees.SecurityGuard;
        case EmployeeType.Architect:
          if (Employees.Architect == null)
          {
            Employees.Architect = new List<AnimalType>();
            Employees.Architect.Add(AnimalType.ArchitectAsian);
            Employees.Architect.Add(AnimalType.ArchitectAsian2);
            Employees.Architect.Add(AnimalType.ArchitectBlack);
            Employees.Architect.Add(AnimalType.ArchitectBlack2);
            Employees.Architect.Add(AnimalType.ArchitectWhite);
            Employees.Architect.Add(AnimalType.ArchitectWhite2);
          }
          return Employees.Architect;
        case EmployeeType.ShopKeeper:
          if (Employees.ShopKeeper == null)
          {
            Employees.ShopKeeper = new List<AnimalType>();
            Employees.ShopKeeper.Add(AnimalType.Vendor_PretzelShop_1);
            Employees.ShopKeeper.Add(AnimalType.Vendor_ShellShack_1);
            Employees.ShopKeeper.Add(AnimalType.Vendor_Slushie_2);
            Employees.ShopKeeper.Add(AnimalType.Vendor_SouvenirStall_5);
            Employees.ShopKeeper.Add(AnimalType.Vendor_TacoTruck_6);
          }
          return Employees.ShopKeeper;
        case EmployeeType.Breeder:
          if (Employees.Breeder == null)
          {
            Employees.Breeder = new List<AnimalType>();
            Employees.Breeder.Add(AnimalType.BreederWhiteMale);
            Employees.Breeder.Add(AnimalType.BreederBlackMale);
            Employees.Breeder.Add(AnimalType.BreederAsianMale);
            Employees.Breeder.Add(AnimalType.BreederWhiteFemale);
            Employees.Breeder.Add(AnimalType.BreederBlackFemale);
            Employees.Breeder.Add(AnimalType.BreederAsianFemale);
          }
          return Employees.Breeder;
        case EmployeeType.DNAResearcher:
          if (Employees.DNAResearcher == null)
          {
            Employees.DNAResearcher = new List<AnimalType>();
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherAsianWithGoggles);
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherAsianNoGoggles);
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherBlackWithGoggles);
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherBlackNoGoggles);
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherWhiteWithGoggles);
            Employees.DNAResearcher.Add(AnimalType.DNAResearcherWhiteNoGoggles);
          }
          return Employees.DNAResearcher;
        case EmployeeType.MeatProcessorWorker:
          if (Employees.MeatProcessorWorker == null)
          {
            Employees.MeatProcessorWorker = new List<AnimalType>();
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerAsianMale);
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerAsianFemale);
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerWhiteMale);
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerWhiteFemale);
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerBlackMale);
            Employees.MeatProcessorWorker.Add(AnimalType.MeatProcessorWorkerBlackFemale);
          }
          return Employees.MeatProcessorWorker;
        case EmployeeType.SlaughterhouseEmployee:
          if (Employees.SlaughterhouseEmployee == null)
          {
            Employees.SlaughterhouseEmployee = new List<AnimalType>();
            Employees.SlaughterhouseEmployee.Add(AnimalType.SlaughterhouseEmployeeAsian);
            Employees.SlaughterhouseEmployee.Add(AnimalType.SlaughterhouseEmployeeWhite);
            Employees.SlaughterhouseEmployee.Add(AnimalType.SlaughterhouseEmployeeBlack);
          }
          return Employees.SlaughterhouseEmployee;
        case EmployeeType.FactoryWorker:
          if (Employees.FactoryWorker == null)
          {
            Employees.FactoryWorker = new List<AnimalType>();
            Employees.FactoryWorker.Add(AnimalType.FactoryWorkerAsian);
            Employees.FactoryWorker.Add(AnimalType.FactoryWorkerWhite);
            Employees.FactoryWorker.Add(AnimalType.FactoryWorkerBlack);
          }
          return Employees.FactoryWorker;
        case EmployeeType.Police:
          if (Employees.Police == null)
          {
            Employees.Police = new List<AnimalType>();
            Employees.Police.Add(AnimalType.PoliceAsian);
            Employees.Police.Add(AnimalType.PoliceAsian2);
            Employees.Police.Add(AnimalType.PoliceBlack);
            Employees.Police.Add(AnimalType.PoliceBlack2);
            Employees.Police.Add(AnimalType.PoliceWhite);
            Employees.Police.Add(AnimalType.PoliceWhite2);
            Employees.Police.Add(AnimalType.PoliceWithGun);
          }
          return Employees.Police;
        case EmployeeType.Farmer:
          if (Employees.Farmer == null)
          {
            Employees.Farmer = new List<AnimalType>();
            Employees.Farmer.Add(AnimalType.FarmerAsianFemale);
            Employees.Farmer.Add(AnimalType.FarmerAsianMale);
            Employees.Farmer.Add(AnimalType.FarmerBlackFemale);
            Employees.Farmer.Add(AnimalType.FarmerBlackMale);
            Employees.Farmer.Add(AnimalType.FarmerWhiteFemale);
            Employees.Farmer.Add(AnimalType.FarmerWhiteMale);
          }
          return Employees.Farmer;
        case EmployeeType.Deliveryman:
          if (Employees.Deliveryman == null)
          {
            Employees.Deliveryman = new List<AnimalType>();
            Employees.Deliveryman.Add(AnimalType.Deliveryman_AsianFemale);
            Employees.Deliveryman.Add(AnimalType.Deliveryman_AsianMale);
            Employees.Deliveryman.Add(AnimalType.Deliveryman_BlackFemale);
            Employees.Deliveryman.Add(AnimalType.Deliveryman_BlackMale);
            Employees.Deliveryman.Add(AnimalType.Deliveryman_WhiteFemale);
            Employees.Deliveryman.Add(AnimalType.Deliveryman_WhiteMale);
          }
          return Employees.Deliveryman;
        case EmployeeType.VegProcessorWorker:
          if (Employees.VegProcessorWorker == null)
          {
            Employees.VegProcessorWorker = new List<AnimalType>();
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_AsianFemale);
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_AsianMale);
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_BlackFemale);
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_BlackMale);
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_WhiteFemale);
            Employees.VegProcessorWorker.Add(AnimalType.CropPicker_WhiteMale);
          }
          return Employees.VegProcessorWorker;
        case EmployeeType.WarehouseWorker:
          if (Employees.WarehouseWorker == null)
          {
            Employees.WarehouseWorker = new List<AnimalType>();
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_AsianFemale);
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_AsianMale);
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_BlackFemale);
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_BlackMale);
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_WhiteFemale);
            Employees.WarehouseWorker.Add(AnimalType.WarehouseWorker_WhiteMale);
          }
          return Employees.WarehouseWorker;
        default:
          throw new Exception();
      }
    }

    internal static AnimalType GetEmployee(EmployeeType employee, out bool IsAGirl)
    {
      IsAGirl = TinyZoo.Game1.Rnd.Next(0, 2) == 0;
      List<AnimalType> animalTypeList1 = new List<AnimalType>();
      List<AnimalType> animalTypeList2;
      switch (employee)
      {
        case EmployeeType.Mascot:
          if (Employees.Mascots == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Mascots;
          break;
        case EmployeeType.Guide:
          if (Employees.Guide == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Guide;
          break;
        case EmployeeType.Janitor:
          if (Employees.Janitor == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Janitor;
          break;
        case EmployeeType.Keeper:
          if (Employees.Keepers == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Keepers;
          break;
        case EmployeeType.Vet:
          if (Employees.Vet == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Vet;
          break;
        case EmployeeType.Mechanic:
          if (Employees.Mechanic == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Mechanic;
          break;
        case EmployeeType.SecurityGuard:
          if (Employees.SecurityGuard == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.SecurityGuard;
          break;
        case EmployeeType.Architect:
          if (Employees.Architect == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Architect;
          break;
        case EmployeeType.ShopKeeper:
          if (Employees.ShopKeeper == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.ShopKeeper;
          break;
        case EmployeeType.Breeder:
          if (Employees.Breeder == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Breeder;
          break;
        case EmployeeType.DNAResearcher:
          if (Employees.DNAResearcher == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.DNAResearcher;
          break;
        case EmployeeType.MeatProcessorWorker:
          if (Employees.MeatProcessorWorker == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.MeatProcessorWorker;
          break;
        case EmployeeType.SlaughterhouseEmployee:
          if (Employees.SlaughterhouseEmployee == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.SlaughterhouseEmployee;
          break;
        case EmployeeType.FactoryWorker:
          if (Employees.FactoryWorker == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.FactoryWorker;
          break;
        case EmployeeType.Police:
          if (Employees.Police == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Police;
          break;
        case EmployeeType.Farmer:
          if (Employees.Farmer == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Farmer;
          break;
        case EmployeeType.Deliveryman:
          if (Employees.Deliveryman == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.Deliveryman;
          break;
        case EmployeeType.VegProcessorWorker:
          if (Employees.VegProcessorWorker == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.VegProcessorWorker;
          break;
        case EmployeeType.WarehouseWorker:
          if (Employees.WarehouseWorker == null)
            Employees.GetEmployeeAsLict(employee);
          animalTypeList2 = Employees.WarehouseWorker;
          break;
        default:
          throw new Exception("NO MORE");
      }
      return animalTypeList2[TinyZoo.Game1.Rnd.Next(0, animalTypeList2.Count)];
    }
  }
}
