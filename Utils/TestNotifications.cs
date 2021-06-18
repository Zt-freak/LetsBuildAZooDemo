// Decompiled with JetBrains decompiler
// Type: TinyZoo.Utils.TestNotifications
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Buttons;
using System;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Notification;
using TinyZoo.Z_Quests.Advice;

namespace TinyZoo.Utils
{
  internal class TestNotifications
  {
    internal static void SetTestNotifications(Player player)
    {
      GetAdvice.GetCurrentAdvice(player);
      if (player.prisonlayout.cellblockcontainer.prisonzones.Count == 0)
        throw new Exception("Must have an enclosure to run tests");
      TileRenderer tilerenderer = new TileRenderer(new LayoutEntry(TILETYPE.ArchitectOffice), 156, 203, false);
      player.prisonlayout.BuildTileFromTileRenderer(tilerenderer, player.livestats.consumptionstatus, player);
      player.shopstatus.BuiltABuilding(new Vector2Int(156, 203), TILETYPE.ArchitectOffice, 0, player, false, out int _);
      OverWorldManager.overworldenvironment.wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout);
      TestNotifications.AddEmplyee(player, AnimalType.ArchitectAsian, EmployeeType.Architect);
      int cellUid = player.prisonlayout.cellblockcontainer.prisonzones[0].Cell_UID;
      int ChildUID;
      int _AnimalUID = player.prisonlayout.cellblockcontainer.AddNewAnimal(AnimalType.Rabbit, player.prisonlayout.cellblockcontainer.prisonzones[0].Cell_UID, 0, true, out ChildUID, 0, 0);
      for (int index = 0; index < 30; ++index)
      {
        switch (index)
        {
          case 0:
            Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalBirth, _AnimalUID, _PenUID: cellUid), player);
            break;
          case 3:
            NotificationPackage notificationPackage = new NotificationPackage(Z_NotificationType.A_AnimalHunger, ChildUID, _PenUID: cellUid);
            int _DaysWithoutFood = 2;
            Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.A_AnimalBirth, _AnimalUID, _DaysWithoutFood, cellUid), player);
            break;
        }
      }
    }

    private static void AddEmplyee(
      Player player,
      AnimalType animaltype,
      EmployeeType employeeetype)
    {
      IntakePerson intakeperson = new IntakePerson(animaltype, _IsAGirl: true);
      player.employees.AddThisEmplyee(intakeperson, employeeetype, 5, 10, player);
      player.worldhistory.EmployedSomeone(intakeperson.animaltype);
      WalkingPerson NewEmployee = CustomerManager.AddPerson(intakeperson.animaltype, player.employees.employees[player.employees.employees.Count - 1], player: player);
      NewEmployee.ForceRotationAndHold(DirectionPressed.Down, 2f);
      ParkGate.NewEmployeeWantsToGoThoughGate(NewEmployee);
    }
  }
}
