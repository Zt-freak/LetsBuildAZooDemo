// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Customers.EmployeeData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_OverWorld._OverWorldEnv.Customers;

namespace TinyZoo.OverWorld.OverWorldEnv.Customers
{
  internal class EmployeeData
  {
    private static List<TILETYPE> ListBuildingsWithEmployees;
    private static HashSet<TILETYPE> BuildingsWithEmployees;
    private static EmployeeBuildingList[] emplyeebuildings;

    internal static bool IsThisAnEmployee(AnimalType persontype, out EmployeeType employeetype)
    {
      employeetype = EmployeeType.None;
      switch (persontype)
      {
        case AnimalType.MaleZookeeper:
        case AnimalType.FemaleZookeeper:
        case AnimalType.MaleAsianZookeeper:
        case AnimalType.FemaleAsianZookeeper:
        case AnimalType.MaleDarkZookeeper:
        case AnimalType.FemaleDarkZookeeper:
          employeetype = EmployeeType.Keeper;
          return true;
        case AnimalType.MascotGonky:
        case AnimalType.MascotOctoman:
        case AnimalType.MascotBear:
        case AnimalType.MascotShark:
        case AnimalType.MascotSharkFace:
        case AnimalType.MascotPenguin:
        case AnimalType.MascotPig:
        case AnimalType.MascotPanda:
          employeetype = EmployeeType.Mascot;
          return true;
        case AnimalType.TourGuideBlack:
        case AnimalType.TourGuideAsian:
        case AnimalType.TourGuideWhite:
        case AnimalType.TourGuideBlack2:
        case AnimalType.TourGuideAsian2:
        case AnimalType.TourGuideWhite2:
          employeetype = EmployeeType.Guide;
          return true;
        case AnimalType.KeeperBlack:
        case AnimalType.KeeperAsian:
        case AnimalType.KeeperWhite:
        case AnimalType.KeeperBlack2:
        case AnimalType.KeeperAsian2:
        case AnimalType.KeeperWhite2:
          employeetype = EmployeeType.Keeper;
          return true;
        case AnimalType.CleanerBlack:
        case AnimalType.CleanerAsian:
        case AnimalType.CleanerWhite:
        case AnimalType.CleanerBlack2:
        case AnimalType.CleanerAsian2:
        case AnimalType.CleanerWhite2:
          employeetype = EmployeeType.Janitor;
          return true;
        case AnimalType.VetBlack:
        case AnimalType.VetAsian:
        case AnimalType.VetWhite:
        case AnimalType.VetBlack2:
        case AnimalType.VetAsian2:
        case AnimalType.VetWhite2:
          employeetype = EmployeeType.Vet;
          return true;
        case AnimalType.MechanicBlack:
        case AnimalType.MechanicAsian:
        case AnimalType.MechanicWhite:
        case AnimalType.MechanicBlack2:
        case AnimalType.MechanicAsian2:
        case AnimalType.MechanicWhite2:
          employeetype = EmployeeType.Mechanic;
          return true;
        case AnimalType.SecurityGuardBlack:
        case AnimalType.SecurityGuardAsian:
        case AnimalType.SecurityGuardWhite:
        case AnimalType.SecurityGuardBlack2:
        case AnimalType.SecurityGuardAsian2:
        case AnimalType.SecurityGuardWhite2:
          employeetype = EmployeeType.SecurityGuard;
          return true;
        case AnimalType.ArchitectBlack:
        case AnimalType.ArchitectAsian:
        case AnimalType.ArchitectWhite:
        case AnimalType.ArchitectBlack2:
        case AnimalType.ArchitectAsian2:
        case AnimalType.ArchitectWhite2:
          employeetype = EmployeeType.Architect;
          return true;
        case AnimalType.Vendor_PandaBurger_1:
        case AnimalType.Vendor_PandaBurger_2:
        case AnimalType.Vendor_PandaBurger_3:
        case AnimalType.Vendor_PandaBurger_4:
        case AnimalType.Vendor_PandaBurger_5:
        case AnimalType.Vendor_PandaBurger_6:
        case AnimalType.Vendor_SouvenirStall_1:
        case AnimalType.Vendor_SouvenirStall_2:
        case AnimalType.Vendor_SouvenirStall_3:
        case AnimalType.Vendor_SouvenirStall_4:
        case AnimalType.Vendor_SouvenirStall_5:
        case AnimalType.Vendor_SouvenirStall_6:
        case AnimalType.Vendor_CottonCandy_1:
        case AnimalType.Vendor_CottonCandy_2:
        case AnimalType.Vendor_CottonCandy_3:
        case AnimalType.Vendor_CottonCandy_4:
        case AnimalType.Vendor_CottonCandy_5:
        case AnimalType.Vendor_CottonCandy_6:
        case AnimalType.Vendor_LionHotDog_1:
        case AnimalType.Vendor_LionHotDog_2:
        case AnimalType.Vendor_LionHotDog_3:
        case AnimalType.Vendor_LionHotDog_4:
        case AnimalType.Vendor_LionHotDog_5:
        case AnimalType.Vendor_LionHotDog_6:
        case AnimalType.Vendor_Popcorn_1:
        case AnimalType.Vendor_Popcorn_2:
        case AnimalType.Vendor_Popcorn_3:
        case AnimalType.Vendor_Popcorn_4:
        case AnimalType.Vendor_Popcorn_5:
        case AnimalType.Vendor_Popcorn_6:
        case AnimalType.Vendor_Churro_1:
        case AnimalType.Vendor_Churro_2:
        case AnimalType.Vendor_Churro_3:
        case AnimalType.Vendor_Churro_4:
        case AnimalType.Vendor_Churro_5:
        case AnimalType.Vendor_Churro_6:
        case AnimalType.Vendor_Slushie_1:
        case AnimalType.Vendor_Slushie_2:
        case AnimalType.Vendor_Slushie_3:
        case AnimalType.Vendor_Slushie_4:
        case AnimalType.Vendor_Slushie_5:
        case AnimalType.Vendor_Slushie_6:
        case AnimalType.Vendor_Balloon_1:
        case AnimalType.Vendor_Balloon_2:
        case AnimalType.Vendor_Balloon_3:
        case AnimalType.Vendor_Balloon_4:
        case AnimalType.Vendor_Balloon_5:
        case AnimalType.Vendor_Balloon_6:
        case AnimalType.Vendor_RustyKeg_1:
        case AnimalType.Vendor_RustyKeg_2:
        case AnimalType.Vendor_RustyKeg_3:
        case AnimalType.Vendor_RustyKeg_4:
        case AnimalType.Vendor_RustyKeg_5:
        case AnimalType.Vendor_RustyKeg_6:
        case AnimalType.Vendor_KangarooPizza_1:
        case AnimalType.Vendor_KangarooPizza_2:
        case AnimalType.Vendor_KangarooPizza_3:
        case AnimalType.Vendor_KangarooPizza_4:
        case AnimalType.Vendor_KangarooPizza_5:
        case AnimalType.Vendor_KangarooPizza_6:
        case AnimalType.Vendor_IceCreamVan_1:
        case AnimalType.Vendor_IceCreamVan_2:
        case AnimalType.Vendor_IceCreamVan_3:
        case AnimalType.Vendor_IceCreamVan_4:
        case AnimalType.Vendor_IceCreamVan_5:
        case AnimalType.Vendor_IceCreamVan_6:
        case AnimalType.Vendor_IceCreamStore_1:
        case AnimalType.Vendor_IceCreamStore_2:
        case AnimalType.Vendor_IceCreamStore_3:
        case AnimalType.Vendor_IceCreamStore_4:
        case AnimalType.Vendor_IceCreamStore_5:
        case AnimalType.Vendor_IceCreamStore_6:
        case AnimalType.Vendor_Coconut_1:
        case AnimalType.Vendor_Coconut_2:
        case AnimalType.Vendor_Coconut_3:
        case AnimalType.Vendor_Coconut_4:
        case AnimalType.Vendor_Coconut_5:
        case AnimalType.Vendor_Coconut_6:
        case AnimalType.Vendor_ElephantSouvenir_1:
        case AnimalType.Vendor_ElephantSouvenir_2:
        case AnimalType.Vendor_ElephantSouvenir_3:
        case AnimalType.Vendor_ElephantSouvenir_4:
        case AnimalType.Vendor_ElephantSouvenir_5:
        case AnimalType.Vendor_ElephantSouvenir_6:
        case AnimalType.Vendor_KatCoffee_1:
        case AnimalType.Vendor_KatCoffee_2:
        case AnimalType.Vendor_KatCoffee_3:
        case AnimalType.Vendor_KatCoffee_4:
        case AnimalType.Vendor_KatCoffee_5:
        case AnimalType.Vendor_KatCoffee_6:
        case AnimalType.Vendor_ShellShack_1:
        case AnimalType.Vendor_ShellShack_2:
        case AnimalType.Vendor_ShellShack_3:
        case AnimalType.Vendor_ShellShack_4:
        case AnimalType.Vendor_ShellShack_5:
        case AnimalType.Vendor_ShellShack_6:
        case AnimalType.Vendor_TacoTruck_1:
        case AnimalType.Vendor_TacoTruck_2:
        case AnimalType.Vendor_TacoTruck_3:
        case AnimalType.Vendor_TacoTruck_4:
        case AnimalType.Vendor_TacoTruck_5:
        case AnimalType.Vendor_TacoTruck_6:
        case AnimalType.Vendor_PretzelShop_1:
        case AnimalType.Vendor_PretzelShop_2:
        case AnimalType.Vendor_PretzelShop_3:
        case AnimalType.Vendor_PretzelShop_4:
        case AnimalType.Vendor_PretzelShop_5:
        case AnimalType.Vendor_PretzelShop_6:
        case AnimalType.HotAirBalloonEmployeeAsianMale:
        case AnimalType.HotAirBalloonEmployeeAsianFemale:
        case AnimalType.HotAirBalloonEmployeeWhiteMale:
        case AnimalType.HotAirBalloonEmployeeWhiteFemale:
        case AnimalType.HotAirBalloonEmployeeBlackMale:
        case AnimalType.HotAirBalloonEmployeeBlackFemale:
        case AnimalType.HelicopterEmployeeAsianMale:
        case AnimalType.HelicopterEmployeeAsianFemale:
        case AnimalType.HelicopterEmployeeWhiteMale:
        case AnimalType.HelicopterEmployeeWhiteFemale:
        case AnimalType.HelicopterEmployeeBlackMale:
        case AnimalType.HelicopterEmployeeBlackFemale:
          employeetype = EmployeeType.ShopKeeper;
          return true;
        case AnimalType.BreederWhiteMale:
        case AnimalType.BreederBlackMale:
        case AnimalType.BreederAsianMale:
        case AnimalType.BreederWhiteFemale:
        case AnimalType.BreederBlackFemale:
        case AnimalType.BreederAsianFemale:
          employeetype = EmployeeType.Breeder;
          return true;
        case AnimalType.DNAResearcherAsianWithGoggles:
        case AnimalType.DNAResearcherAsianNoGoggles:
        case AnimalType.DNAResearcherBlackWithGoggles:
        case AnimalType.DNAResearcherBlackNoGoggles:
        case AnimalType.DNAResearcherWhiteWithGoggles:
        case AnimalType.DNAResearcherWhiteNoGoggles:
          employeetype = EmployeeType.DNAResearcher;
          return true;
        case AnimalType.MeatProcessorWorkerAsianMale:
        case AnimalType.MeatProcessorWorkerAsianFemale:
        case AnimalType.MeatProcessorWorkerWhiteMale:
        case AnimalType.MeatProcessorWorkerWhiteFemale:
        case AnimalType.MeatProcessorWorkerBlackMale:
        case AnimalType.MeatProcessorWorkerBlackFemale:
          employeetype = EmployeeType.MeatProcessorWorker;
          return true;
        case AnimalType.SlaughterhouseEmployeeAsian:
        case AnimalType.SlaughterhouseEmployeeWhite:
        case AnimalType.SlaughterhouseEmployeeBlack:
          employeetype = EmployeeType.SlaughterhouseEmployee;
          return true;
        case AnimalType.FactoryWorkerAsian:
        case AnimalType.FactoryWorkerWhite:
        case AnimalType.FactoryWorkerBlack:
          employeetype = EmployeeType.FactoryWorker;
          return true;
        case AnimalType.FarmerWhiteMale:
        case AnimalType.FarmerWhiteFemale:
        case AnimalType.FarmerAsianMale:
        case AnimalType.FarmerAsianFemale:
        case AnimalType.FarmerBlackMale:
        case AnimalType.FarmerBlackFemale:
          employeetype = EmployeeType.Farmer;
          return true;
        case AnimalType.CropPicker_AsianFemale:
        case AnimalType.CropPicker_AsianMale:
        case AnimalType.CropPicker_BlackFemale:
        case AnimalType.CropPicker_BlackMale:
        case AnimalType.CropPicker_WhiteFemale:
        case AnimalType.CropPicker_WhiteMale:
          employeetype = EmployeeType.VegProcessorWorker;
          return true;
        case AnimalType.WarehouseWorker_AsianFemale:
        case AnimalType.WarehouseWorker_AsianMale:
        case AnimalType.WarehouseWorker_BlackFemale:
        case AnimalType.WarehouseWorker_BlackMale:
        case AnimalType.WarehouseWorker_WhiteFemale:
        case AnimalType.WarehouseWorker_WhiteMale:
          employeetype = EmployeeType.WarehouseWorker;
          return true;
        case AnimalType.PoliceBlack:
        case AnimalType.PoliceAsian:
        case AnimalType.PoliceWhite:
        case AnimalType.PoliceBlack2:
        case AnimalType.PoliceAsian2:
        case AnimalType.PoliceWhite2:
        case AnimalType.PoliceWithGun:
          employeetype = EmployeeType.Police;
          return true;
        case AnimalType.Deliveryman_AsianFemale:
        case AnimalType.Deliveryman_AsianMale:
        case AnimalType.Deliveryman_BlackFemale:
        case AnimalType.Deliveryman_BlackMale:
        case AnimalType.Deliveryman_WhiteFemale:
        case AnimalType.Deliveryman_WhiteMale:
          employeetype = EmployeeType.Deliveryman;
          return true;
        default:
          return false;
      }
    }

    internal static bool ThisStoreHasAnEmployee(TILETYPE building)
    {
      if (EmployeeData.BuildingsWithEmployees == null)
        EmployeeData.BuildingsWithEmployees = new HashSet<TILETYPE>((IEnumerable<TILETYPE>) EmployeeData.GetBuildingWithEmployees());
      return EmployeeData.BuildingsWithEmployees.Contains(building);
    }

    internal static List<TILETYPE> GetBuildingWithEmployees()
    {
      if (EmployeeData.ListBuildingsWithEmployees == null)
      {
        EmployeeData.ListBuildingsWithEmployees = new List<TILETYPE>();
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.SmallGiftShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.ChurroShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.IceCreamTruck);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.LionHotDogShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.BigIceCreamShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.CoconutShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.PandaBurgerShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.BalloonShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.KangarooPizzaShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.CottonCandyShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.SlushieShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.ElephantGiftShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.RustyKegShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.PopcornWeaselShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.KatCoffeeShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.ShellShackShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.TacoTruck);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.PretzelShop);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.ArchitectOffice);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Nursery);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.DNABuilding);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Slaughterhouse);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.MeatProcessor);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.StoreRoom);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Storeroom_Monkey);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Storeroom_BrownWood);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Storeroom_Victorian);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.HotAirBalloonRide);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.CorruptedHotAirBalloonRide);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.HelicopterRide);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Incinerator);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.GlueFactory);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.BuffaloWingFactory);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.BaconFactory);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.CrocHandbagFactory);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.SnakeSkinFactory);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.EggBatteryFarm);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.MilkBatteryFarm);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.VetOffice);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Farmhouse);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.BeerBrewery);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Windmill);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.FarmProcessor);
        EmployeeData.ListBuildingsWithEmployees.Add(TILETYPE.Warehouse);
      }
      return EmployeeData.ListBuildingsWithEmployees;
    }

    internal static AnimalType GetBuildingtoEmployee(
      TILETYPE building,
      out bool IsAGirl,
      int ForceChoice = -1)
    {
      int num = Game1.Rnd.Next(0, 6);
      if (ForceChoice > -1)
        num = ForceChoice;
      IsAGirl = false;
      switch (building)
      {
        case TILETYPE.SmallGiftShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_SouvenirStall_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_SouvenirStall_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_SouvenirStall_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_SouvenirStall_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_SouvenirStall_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_SouvenirStall_6;
          }
        case TILETYPE.LionHotDogShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_LionHotDog_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_LionHotDog_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_LionHotDog_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_LionHotDog_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_LionHotDog_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_LionHotDog_6;
          }
        case TILETYPE.ElephantGiftShop:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_ElephantSouvenir_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_ElephantSouvenir_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_ElephantSouvenir_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_ElephantSouvenir_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_ElephantSouvenir_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_ElephantSouvenir_6;
          }
        case TILETYPE.IceCreamTruck:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamVan_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamVan_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamVan_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamVan_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamVan_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamVan_6;
          }
        case TILETYPE.BigIceCreamShop:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamStore_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamStore_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_IceCreamStore_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamStore_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamStore_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_IceCreamStore_6;
          }
        case TILETYPE.CoconutShop:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_Coconut_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_Coconut_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_Coconut_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_Coconut_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_Coconut_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_Coconut_6;
          }
        case TILETYPE.PandaBurgerShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_PandaBurger_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_PandaBurger_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_PandaBurger_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_PandaBurger_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_PandaBurger_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_PandaBurger_6;
          }
        case TILETYPE.BalloonShop:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_Balloon_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_Balloon_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_Balloon_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_Balloon_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_Balloon_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_Balloon_6;
          }
        case TILETYPE.KangarooPizzaShop:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_KangarooPizza_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_KangarooPizza_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_KangarooPizza_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_KangarooPizza_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_KangarooPizza_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_KangarooPizza_6;
          }
        case TILETYPE.CottonCandyShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_CottonCandy_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_CottonCandy_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_CottonCandy_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_CottonCandy_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_CottonCandy_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_CottonCandy_6;
          }
        case TILETYPE.SlushieShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_Slushie_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_Slushie_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_Slushie_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_Slushie_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_Slushie_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_Slushie_6;
          }
        case TILETYPE.ChurroShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_Churro_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_Churro_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_Churro_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_Churro_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_Churro_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_Churro_6;
          }
        case TILETYPE.RustyKegShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_RustyKeg_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_RustyKeg_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_RustyKeg_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_RustyKeg_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_RustyKeg_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_RustyKeg_6;
          }
        case TILETYPE.PopcornWeaselShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_Popcorn_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_Popcorn_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_Popcorn_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_Popcorn_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_Popcorn_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_Popcorn_6;
          }
        case TILETYPE.Nursery:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.BreederAsianFemale;
            case 1:
              IsAGirl = false;
              return AnimalType.BreederAsianMale;
            case 2:
              IsAGirl = true;
              return AnimalType.BreederBlackFemale;
            case 3:
              IsAGirl = false;
              return AnimalType.BreederBlackMale;
            case 4:
              IsAGirl = false;
              return AnimalType.BreederWhiteFemale;
            default:
              IsAGirl = true;
              return AnimalType.BreederWhiteMale;
          }
        case TILETYPE.VetOffice:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.VetBlack;
            case 1:
              IsAGirl = false;
              return AnimalType.VetAsian;
            case 2:
              IsAGirl = true;
              return AnimalType.VetWhite;
            case 3:
              IsAGirl = false;
              return AnimalType.VetBlack2;
            case 4:
              IsAGirl = true;
              return AnimalType.VetAsian2;
            default:
              IsAGirl = false;
              return AnimalType.VetWhite2;
          }
        case TILETYPE.StoreRoom:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.KeeperAsian;
            case 1:
              IsAGirl = false;
              return AnimalType.KeeperAsian2;
            case 2:
              IsAGirl = true;
              return AnimalType.KeeperBlack;
            case 3:
              IsAGirl = false;
              return AnimalType.KeeperBlack2;
            case 4:
              IsAGirl = false;
              return AnimalType.KeeperWhite;
            default:
              IsAGirl = true;
              return AnimalType.KeeperWhite2;
          }
        case TILETYPE.ArchitectOffice:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.ArchitectAsian;
            case 1:
              IsAGirl = false;
              return AnimalType.ArchitectAsian2;
            case 2:
              IsAGirl = true;
              return AnimalType.ArchitectBlack;
            case 3:
              IsAGirl = false;
              return AnimalType.ArchitectBlack2;
            case 4:
              IsAGirl = false;
              return AnimalType.ArchitectWhite;
            default:
              IsAGirl = true;
              return AnimalType.ArchitectWhite2;
          }
        case TILETYPE.GlueFactory:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.BuffaloWingFactory:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.BaconFactory:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.KatCoffeeShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_KatCoffee_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_KatCoffee_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_KatCoffee_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_KatCoffee_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_KatCoffee_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_KatCoffee_6;
          }
        case TILETYPE.ShellShackShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_ShellShack_1;
            case 1:
              IsAGirl = false;
              return AnimalType.Vendor_ShellShack_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_ShellShack_3;
            case 3:
              IsAGirl = true;
              return AnimalType.Vendor_ShellShack_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_ShellShack_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_ShellShack_6;
          }
        case TILETYPE.TacoTruck:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.Vendor_TacoTruck_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_TacoTruck_2;
            case 2:
              IsAGirl = false;
              return AnimalType.Vendor_TacoTruck_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_TacoTruck_4;
            case 4:
              IsAGirl = true;
              return AnimalType.Vendor_TacoTruck_5;
            default:
              IsAGirl = true;
              return AnimalType.Vendor_TacoTruck_6;
          }
        case TILETYPE.PretzelShop:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.Vendor_PretzelShop_1;
            case 1:
              IsAGirl = true;
              return AnimalType.Vendor_PretzelShop_2;
            case 2:
              IsAGirl = true;
              return AnimalType.Vendor_PretzelShop_3;
            case 3:
              IsAGirl = false;
              return AnimalType.Vendor_PretzelShop_4;
            case 4:
              IsAGirl = false;
              return AnimalType.Vendor_PretzelShop_5;
            default:
              IsAGirl = false;
              return AnimalType.Vendor_PretzelShop_6;
          }
        case TILETYPE.Slaughterhouse:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.SlaughterhouseEmployeeWhite;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.SlaughterhouseEmployeeBlack;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.SlaughterhouseEmployeeAsian;
        case TILETYPE.EggBatteryFarm:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.MilkBatteryFarm:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.HelicopterRide:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.HelicopterEmployeeAsianMale;
            case 1:
              IsAGirl = true;
              return AnimalType.HelicopterEmployeeAsianFemale;
            case 2:
              IsAGirl = false;
              return AnimalType.HelicopterEmployeeWhiteMale;
            case 3:
              IsAGirl = true;
              return AnimalType.HelicopterEmployeeWhiteFemale;
            case 4:
              IsAGirl = false;
              return AnimalType.HelicopterEmployeeBlackMale;
            default:
              IsAGirl = true;
              return AnimalType.HelicopterEmployeeBlackFemale;
          }
        case TILETYPE.DNABuilding:
          switch (num)
          {
            case 0:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherAsianWithGoggles;
            case 1:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherAsianNoGoggles;
            case 2:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherBlackWithGoggles;
            case 3:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherBlackNoGoggles;
            case 4:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherWhiteWithGoggles;
            default:
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.DNAResearcherWhiteNoGoggles;
          }
        case TILETYPE.MeatProcessor:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerAsianMale;
            case 1:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerAsianFemale;
            case 2:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerWhiteMale;
            case 3:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerWhiteFemale;
            case 4:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerBlackMale;
            default:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerBlackFemale;
          }
        case TILETYPE.Incinerator:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerAsianFemale;
            case 1:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerAsianMale;
            case 2:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerBlackFemale;
            case 3:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerBlackMale;
            case 4:
              IsAGirl = false;
              return AnimalType.MeatProcessorWorkerWhiteFemale;
            default:
              IsAGirl = true;
              return AnimalType.MeatProcessorWorkerWhiteMale;
          }
        case TILETYPE.CrocHandbagFactory:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.SnakeSkinFactory:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.HotAirBalloonRide:
        case TILETYPE.CorruptedHotAirBalloonRide:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.HotAirBalloonEmployeeAsianMale;
            case 1:
              IsAGirl = true;
              return AnimalType.HotAirBalloonEmployeeAsianFemale;
            case 2:
              IsAGirl = false;
              return AnimalType.HotAirBalloonEmployeeWhiteMale;
            case 3:
              IsAGirl = true;
              return AnimalType.HotAirBalloonEmployeeWhiteFemale;
            case 4:
              IsAGirl = false;
              return AnimalType.HotAirBalloonEmployeeBlackMale;
            default:
              IsAGirl = true;
              return AnimalType.HotAirBalloonEmployeeBlackFemale;
          }
        case TILETYPE.Farmhouse:
          switch (num)
          {
            case 0:
              IsAGirl = true;
              return AnimalType.FarmerAsianFemale;
            case 1:
              return AnimalType.FarmerAsianMale;
            case 2:
              IsAGirl = true;
              return AnimalType.FarmerBlackFemale;
            case 3:
              return AnimalType.FarmerBlackMale;
            case 4:
              IsAGirl = true;
              return AnimalType.FarmerWhiteFemale;
            default:
              return AnimalType.FarmerWhiteMale;
          }
        case TILETYPE.BeerBrewery:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.Windmill:
          if (num != 0)
          {
            if (num == 1)
            {
              IsAGirl = Game1.Rnd.Next(0, 2) == 0;
              return AnimalType.FactoryWorkerBlack;
            }
            IsAGirl = Game1.Rnd.Next(0, 2) == 0;
            return AnimalType.FactoryWorkerWhite;
          }
          IsAGirl = Game1.Rnd.Next(0, 2) == 0;
          return AnimalType.FactoryWorkerAsian;
        case TILETYPE.FarmProcessor:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.CropPicker_AsianMale;
            case 1:
              IsAGirl = true;
              return AnimalType.CropPicker_AsianFemale;
            case 2:
              IsAGirl = false;
              return AnimalType.CropPicker_WhiteMale;
            case 3:
              IsAGirl = true;
              return AnimalType.CropPicker_WhiteFemale;
            case 4:
              IsAGirl = false;
              return AnimalType.CropPicker_BlackMale;
            default:
              IsAGirl = true;
              return AnimalType.CropPicker_BlackFemale;
          }
        case TILETYPE.Warehouse:
          switch (num)
          {
            case 0:
              IsAGirl = false;
              return AnimalType.WarehouseWorker_AsianMale;
            case 1:
              IsAGirl = true;
              return AnimalType.WarehouseWorker_AsianFemale;
            case 2:
              IsAGirl = false;
              return AnimalType.WarehouseWorker_WhiteMale;
            case 3:
              IsAGirl = true;
              return AnimalType.WarehouseWorker_WhiteFemale;
            case 4:
              IsAGirl = false;
              return AnimalType.WarehouseWorker_BlackMale;
            default:
              IsAGirl = true;
              return AnimalType.WarehouseWorker_BlackFemale;
          }
        default:
          return AnimalType.EmployeeCount;
      }
    }

    internal static List<TILETYPE> GetEmplyeeTypeToBuilding(EmployeeType employee)
    {
      if (EmployeeData.emplyeebuildings == null)
        EmployeeData.emplyeebuildings = new EmployeeBuildingList[20];
      switch (employee)
      {
        case EmployeeType.Keeper:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.StoreRoom);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.Storeroom_BrownWood);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.Storeroom_Monkey);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.Storeroom_Victorian);
          break;
        case EmployeeType.Vet:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.VetOffice);
          break;
        case EmployeeType.Architect:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.ArchitectOffice);
          break;
        case EmployeeType.ShopKeeper:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.SmallGiftShop);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.LionHotDogShop);
          break;
        case EmployeeType.Breeder:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.Nursery);
          break;
        case EmployeeType.DNAResearcher:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.DNABuilding);
          break;
        case EmployeeType.MeatProcessorWorker:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.MeatProcessor);
          break;
        case EmployeeType.SlaughterhouseEmployee:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.Slaughterhouse);
          break;
        case EmployeeType.FactoryWorker:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.GlueFactory);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.BuffaloWingFactory);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.BaconFactory);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.CrocHandbagFactory);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.SnakeSkinFactory);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.MilkBatteryFarm);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.EggBatteryFarm);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.BeerBrewery);
          EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork.Add(TILETYPE.Windmill);
          break;
        case EmployeeType.Farmer:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.Farmhouse);
          break;
        case EmployeeType.VegProcessorWorker:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.FarmProcessor);
          break;
        case EmployeeType.WarehouseWorker:
          EmployeeData.emplyeebuildings[(int) employee] = new EmployeeBuildingList(TILETYPE.Warehouse);
          break;
      }
      return EmployeeData.emplyeebuildings[(int) employee].buildingsthispersoncanwork;
    }
  }
}
