// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldEnv.Customers.CustomerManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Buttons;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks;
using TinyZoo.Z_AnimalsAndPeople;
using TinyZoo.Z_AnimalsAndPeople.DynamicEnrichment;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_SummaryPopUps.People.PeopleInPark.PeopleView;

namespace TinyZoo.OverWorld.OverWorldEnv.Customers
{
  internal class CustomerManager
  {
    private static List<WalkingPerson> walkingpeople;
    private static List<WalkingPerson> DrawablePeople;
    private static List<WalkingPerson> Animals;
    private float SortTimer;
    private static int PersonIndex;
    internal static int PeopleOutAndAbout;
    internal static int[] CurrentSpecialCustomers = new int[27];
    internal static int CustomersInPark_NotWaitingForBus;
    internal static int VIP_BlackMarketEtc;
    private GameObject ZooTop;
    internal static GroupNavigator ProtestGroupNavigator = new GroupNavigator();
    internal static bool SpeedUpRecall = false;
    internal static WalkingPerson ZooKeeperAvatar;
    private bool HasRecalledAll;
    private float SimulationTime;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;
    private static int BUILDINGX;

    public CustomerManager()
    {
      this.ZooTop = new GameObject();
      this.ZooTop.DrawRect = new Rectangle(258, 70, 176, 64);
      this.ZooTop.vLocation = TileMath.GetTileToWorldSpace(WalkingPerson.LogoLocation);
      this.ZooTop.DrawRect.X = 435;
      this.ZooTop.DrawOrigin.X = 88f;
      this.ZooTop.DrawOrigin.Y = 40f;
      CustomerManager.walkingpeople = new List<WalkingPerson>();
      CustomerManager.DrawablePeople = new List<WalkingPerson>();
      CustomerManager.Animals = new List<WalkingPerson>();
    }

    internal static void RandomizeAllLocations(Player player)
    {
      for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
        CustomerManager.walkingpeople[index].RandomizeLocation(player, 100);
    }

    public void StartNewDay()
    {
      this.HasRecalledAll = false;
      this.SimulationTime = 0.0f;
      for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
        CustomerManager.walkingpeople[index].simperson.StartNewDay();
    }

    internal static WalkingPerson GetThisEmployee(Employee employee)
    {
      for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
      {
        if (CustomerManager.walkingpeople[index].IsEmployee && CustomerManager.walkingpeople[index].IsThisEmployee(employee))
          return CustomerManager.walkingpeople[index];
      }
      return (WalkingPerson) null;
    }

    internal static List<WalkingPerson> GetListOfWalkingPeople() => CustomerManager.walkingpeople;

    internal static bool RemoveThisEmployee(Employee employeetoremove)
    {
      for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
      {
        if (CustomerManager.walkingpeople[index].IsEmployee && CustomerManager.walkingpeople[index].IsThisEmployee(employeetoremove))
        {
          CustomerManager.DrawablePeople.Remove(CustomerManager.walkingpeople[index]);
          CustomerManager.walkingpeople.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    internal static void RemoveAllAnimals()
    {
      if (CustomerManager.Animals != null && CustomerManager.DrawablePeople != null)
      {
        for (int index = 0; index < CustomerManager.Animals.Count; ++index)
        {
          if (CustomerManager.DrawablePeople.Contains(CustomerManager.Animals[index]))
            CustomerManager.DrawablePeople.Remove(CustomerManager.Animals[index]);
        }
      }
      CustomerManager.Animals = new List<WalkingPerson>();
    }

    public void PutCustomersOnBus()
    {
    }

    public void AddZooKeeper(Player player)
    {
      WalkingPerson NewEmployee = CustomerManager.AddPerson((AnimalType) player.Stats.ZooKeeperAvatarIndex, player: player);
      NewEmployee.ForceRotationAndHold(DirectionPressed.Down, 2f);
      CustomerManager.ZooKeeperAvatar = NewEmployee;
      ParkGate.NewEmployeeWantsToGoThoughGate(NewEmployee);
      if (TrailerDemoFlags.HasTrailerFlag)
        return;
      for (int index = 0; index < player.employees.employees.Count; ++index)
        ParkGate.NewEmployeeWantsToGoThoughGate(player.employees.employees[index].intakeperson != null ? CustomerManager.AddPerson(player.employees.employees[index].intakeperson.animaltype, player.employees.employees[index], player: player) : CustomerManager.AddPerson(player.employees.employees[index].quickemplyeedescription.thisemployee, player.employees.employees[index], player: player));
    }

    internal static void Recall()
    {
      for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
        CustomerManager.walkingpeople[index].SetRecall();
    }

    internal static WalkingPerson AddPerson(
      AnimalType persontype = AnimalType.None,
      Employee employee = null,
      CellblockMananger cellblockcontainer = null,
      Player player = null,
      int ShopUID = -1)
    {
      CustomerManager.walkingpeople.Add(new WalkingPerson(CustomerManager.PersonIndex, persontype, employee, cellblockcontainer: cellblockcontainer, player: player, ShopUID: ShopUID));
      CustomerManager.DrawablePeople.Add(CustomerManager.walkingpeople[CustomerManager.walkingpeople.Count - 1]);
      ++CustomerManager.PersonIndex;
      PeopleViewer.NewPeopleEnteredPark = true;
      return CustomerManager.walkingpeople[CustomerManager.walkingpeople.Count - 1];
    }

    public void UpdateCustomerManager(float DeltaTime, Player player)
    {
      if (Math.Floor((double) Z_GameFlags.DayTimer) > (double) this.SimulationTime)
      {
        float Cycles = (float) (int) Math.Floor((double) Z_GameFlags.DayTimer) - this.SimulationTime;
        this.SimulationTime = (float) Math.Floor((double) Z_GameFlags.DayTimer);
        for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
          CustomerManager.walkingpeople[index].UpdateNeedsAndWants(Cycles, player);
      }
      CustomerManager.PeopleOutAndAbout = 0;
      CustomerManager.CurrentSpecialCustomers = new int[27];
      int Index = -1;
      Vector2 worldSpace = RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation);
      WalkingPerson.MouseOverUID = -1;
      WalkingPerson.SkipSmartCuror = false;
      for (int index = CustomerManager.walkingpeople.Count - 1; index > -1; --index)
      {
        if (CustomerManager.walkingpeople[index].IsAtive && CustomerManager.walkingpeople[index].simperson.customertype != CustomerType.Normal)
          ++CustomerManager.CurrentSpecialCustomers[(int) CustomerManager.walkingpeople[index].simperson.customertype];
        bool RemoveThisPerson;
        if (CustomerManager.walkingpeople[index].UpdateWalkingPerson(DeltaTime, player, out RemoveThisPerson, worldSpace) && Index == -1)
        {
          Index = index;
          this.CheckReomve(Index, true);
        }
        else if (RemoveThisPerson)
          this.CheckReomve(index, false);
        else if (CustomerManager.walkingpeople[index].IsAtive && !CustomerManager.walkingpeople[index].IsEmployee)
          ++CustomerManager.PeopleOutAndAbout;
      }
      Z_GameFlags.RecheckZooKeeperZones = false;
      if (CustomerManager.CustomersInPark_NotWaitingForBus == CustomerManager.VIP_BlackMarketEtc && !this.HasRecalledAll && CustomerManager.PeopleOutAndAbout > CustomerManager.VIP_BlackMarketEtc)
      {
        this.HasRecalledAll = true;
        for (int index = 0; index < CustomerManager.walkingpeople.Count; ++index)
        {
          if (CustomerManager.walkingpeople[index].simperson.roleinsociety == RoleInSociety.BlackMarket)
          {
            CustomerManager.walkingpeople[index].SetRecall();
            CustomerManager.walkingpeople[index].simperson.memberofthepublic.LeftParkEarly = true;
            CustomerManager.walkingpeople[index].simperson.memberofthepublic.LeftTheParkBecauseOfThis = ParkLeavingReason.VIPEND_EveryoneElseLeft;
          }
        }
      }
      Z_GameFlags.ResetCollisionBlocks();
      this.SortTimer -= DeltaTime;
      if ((double) this.SortTimer < 0.0)
      {
        this.SortTimer = 0.2f;
        for (int index = 0; index < CustomerManager.Animals.Count; ++index)
          CustomerManager.Animals[index].vLocation.Y = CustomerManager.Animals[index].animalrenderer.enemyrenderere.vLocation.Y + 3f;
        CustomerManager.DrawablePeople.Sort(new Comparison<WalkingPerson>(WalkingPerson.SortWalkingPerson));
      }
      if (Z_GameFlags.JUSTMovedTheseEnclosures.Count <= 0)
        return;
      Z_GameFlags.JUSTMovedTheseEnclosures = new List<int>();
    }

    private void CheckReomve(int Index, bool WasKilled)
    {
      if (Index <= -1)
        return;
      if (CustomerManager.IsAVIP(CustomerManager.walkingpeople[Index].simperson.customertype))
        --CustomerManager.VIP_BlackMarketEtc;
      if ((CustomerManager.walkingpeople[Index].simperson.roleinsociety == RoleInSociety.Customer || CustomerManager.walkingpeople[Index].simperson.roleinsociety == RoleInSociety.BlackMarket) && (CustomerManager.walkingpeople[Index].simperson.memberofthepublic != null && !CustomerManager.walkingpeople[Index].simperson.memberofthepublic.IsAtBusWaiting))
      {
        CustomerManager.walkingpeople[Index].simperson.memberofthepublic.SetWaitingForBus(CustomerManager.walkingpeople[Index]);
        --CustomerManager.CustomersInPark_NotWaitingForBus;
      }
      if (WasKilled)
        OverWorldManager.eventsmanager.HumanKilled(CustomerManager.walkingpeople[Index].simperson);
      CustomerManager.DrawablePeople.Remove(CustomerManager.walkingpeople[Index]);
      CustomerManager.walkingpeople.RemoveAt(Index);
      LiveStats.AHumanDied = true;
    }

    internal static bool IsAVIP(CustomerType customertype) => customertype != CustomerType.Normal && customertype != CustomerType.Count && customertype != CustomerType.Protestor;

    internal static void RemoveAnimal(PrisonerInfo animalinfo)
    {
      for (int index = CustomerManager.Animals.Count - 1; index > -1; --index)
      {
        if (CustomerManager.Animals[index].animalrenderer.REF_prisonerinfo == animalinfo)
        {
          int num = CustomerManager.Animals[index].animalrenderer.IsBeingEnriched ? 1 : 0;
          CustomerManager.DrawablePeople.Remove(CustomerManager.Animals[index]);
          CustomerManager.Animals.RemoveAt(index);
          break;
        }
      }
    }

    internal static void RemoveAnimal(AnimalRenderMan animal, int CellUID)
    {
      int num = animal.IsBeingEnriched ? 1 : 0;
      for (int index = CustomerManager.Animals.Count - 1; index > -1; --index)
      {
        if (CustomerManager.Animals[index].animalrenderer == animal)
        {
          CustomerManager.DrawablePeople.Remove(CustomerManager.Animals[index]);
          CustomerManager.Animals.RemoveAt(index);
          return;
        }
      }
      throw new Exception("Not in list");
    }

    internal static void CallPersonToLocation(
      Vector2Int TargetLocation,
      Employee employee,
      WalkingPerson walkingperson = null)
    {
      if (walkingperson == null)
        walkingperson = CustomerManager.GetThisEmployee(employee);
      walkingperson.simperson.TeleportToJob();
      walkingperson.pathnavigator.TeleportHere(TargetLocation);
    }

    internal static WalkingPerson GetAnimalByUID(int UID)
    {
      for (int index = 0; index < CustomerManager.Animals.Count; ++index)
      {
        if (CustomerManager.Animals[index].animalrenderer.REF_prisonerinfo.intakeperson.UID == UID)
          return CustomerManager.Animals[index];
      }
      return (WalkingPerson) null;
    }

    internal static void AddAnimalToPeople(AnimalRenderMan animal)
    {
      CustomerManager.Animals.Add(new WalkingPerson(animal));
      CustomerManager.DrawablePeople.Add(CustomerManager.Animals[CustomerManager.Animals.Count - 1]);
    }

    internal static void RemoveDynamicEnrichmentItem(DynamicEnrichmentItem dynamicenrichmentitem) => CustomerManager.DrawablePeople.Remove(dynamicenrichmentitem.walkingPerson_RefFromCustomerManager);

    internal static void AddDynamicEnrichmentItemToPeople(
      DynamicEnrichmentItem dynamicenrichmentitem)
    {
      CustomerManager.DrawablePeople.Add(new WalkingPerson(dynamicenrichmentitem));
      dynamicenrichmentitem.SetWalkingPerson(CustomerManager.DrawablePeople[CustomerManager.DrawablePeople.Count - 1]);
    }

    public void DrawCustomerManager(int BusY)
    {
      float num = TileMath.HalfTileSize * Sengine.ScreenRatioUpwardsMultiplier.Y;
      OverWorldManager.trashmanager.DrawZ_TrashManager();
      CustomerManager.BUILDINGX = 0;
      for (int index = 0; index < CustomerManager.DrawablePeople.Count; ++index)
      {
        for (; CustomerManager.BUILDINGX < WallsAndFloorsManager.zoobuildingrenderers.Count && (double) WallsAndFloorsManager.zoobuildingrenderers[CustomerManager.BUILDINGX].vLocation.Y < (double) CustomerManager.DrawablePeople[index].vLocation.Y - (double) num; ++CustomerManager.BUILDINGX)
          WallsAndFloorsManager.zoobuildingrenderers[CustomerManager.BUILDINGX].DrawZooBuildingTopRenderer(ref CustomerManager.ThreadLoc, ref CustomerManager.ThreadScale);
        CustomerManager.DrawablePeople[index].DrawWalkingPerson();
      }
      bool flag = true;
      while (flag && CustomerManager.BUILDINGX < WallsAndFloorsManager.zoobuildingrenderers.Count)
      {
        WallsAndFloorsManager.zoobuildingrenderers[CustomerManager.BUILDINGX].DrawZooBuildingTopRenderer(ref CustomerManager.ThreadLoc, ref CustomerManager.ThreadScale);
        ++CustomerManager.BUILDINGX;
        if (WallsAndFloorsManager.zoobuildingrenderers[CustomerManager.BUILDINGX].YLocation >= BusY)
          flag = false;
      }
    }

    public void DrawCustomerManagerAfterBus()
    {
      for (; CustomerManager.BUILDINGX < WallsAndFloorsManager.zoobuildingrenderers.Count; ++CustomerManager.BUILDINGX)
        WallsAndFloorsManager.zoobuildingrenderers[CustomerManager.BUILDINGX].DrawZooBuildingTopRenderer(ref CustomerManager.ThreadLoc, ref CustomerManager.ThreadScale);
    }
  }
}
