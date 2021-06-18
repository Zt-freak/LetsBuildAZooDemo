// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Events.PoliceSniper.PoliceSnipermanger
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Buttons;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_Events.ChoiceUI;

namespace TinyZoo.Z_Events.PoliceSniper
{
  internal class PoliceSnipermanger
  {
    private PoliceCar car;
    private Z_Choicemanager choicemanager;
    private List<WalkingPerson> policepeople;
    private int PoliceSpawned;
    private static int Delete;

    public PoliceSnipermanger(Player player)
    {
      this.policepeople = new List<WalkingPerson>();
      this.car = new PoliceCar();
      QuickEmployeeDescription employeeDescription = new QuickEmployeeDescription(TILETYPE.Logo, -1);
      employeeDescription.thisemployee = AnimalType.PoliceWithGun;
      IntakePerson intakeperson = new IntakePerson(AnimalType.PoliceWithGun, _IsAGirl: true);
      WalkingPerson NewEmployee = CustomerManager.AddPerson(employeeDescription.thisemployee, player.employees.AddThisEmplyee(intakeperson, EmployeeType.Police, 0, 50, player), player: player);
      this.policepeople.Add(NewEmployee);
      NewEmployee.ForceRotationAndHold(DirectionPressed.Down, 2f);
      ParkGate.NewEmployeeWantsToGoThoughGate(NewEmployee);
      this.PoliceSpawned = 1;
      PoliceSnipermanger.Delete = 0;
    }

    internal static void DeletePoliceMan() => ++PoliceSnipermanger.Delete;

    public bool UpdatePoliceSnipermanger(float DeltaTime, bool AGateWasFixed)
    {
      if (PoliceSnipermanger.Delete > 0)
      {
        this.PoliceSpawned -= PoliceSnipermanger.Delete;
        PoliceSnipermanger.Delete = 0;
      }
      if (AGateWasFixed)
      {
        for (int index = 0; index < this.policepeople.Count; ++index)
        {
          if (!this.policepeople[index].pathnavigator.Check_CanWalkHere(this.policepeople[index].ThisPersonStartLocation, GameFlags.pathset, this.policepeople[index].pathnavigator.CurrentTile, out int _))
          {
            this.policepeople[index].pathnavigator.TeleportHere(this.policepeople[index].ThisPersonStartLocation);
            this.policepeople[index].simperson.policeofficecontroller.JustTeleported = true;
          }
        }
      }
      return this.PoliceSpawned == 0;
    }

    public void DrawPoliceSnipermanger() => this.car.DrawPoliceCar();
  }
}
