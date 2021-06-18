// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.MechanicController
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person
{
  internal class MechanicController
  {
    internal static bool ResetMechanicStructures;
    private DeliveryManController deliveryManController;
    public bool WasTeleported;
    private static List<MechanicLocation> AllMechanicLocations;
    private List<Vector2Int> AllMechanicInZOneLocations;

    public MechanicController(Employee _employee, WalkingPerson walkingperson, Player player)
    {
      this.deliveryManController = new DeliveryManController();
      MechanicController.SetUpAllLocations(player);
    }

    internal static void SetUpAllLocations(Player player)
    {
      MechanicController.ResetMechanicStructures = false;
      MechanicController.AllMechanicLocations = new List<MechanicLocation>();
      for (int index = 0; index < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index)
        MechanicController.AllMechanicLocations.Add(new MechanicLocation(player.prisonlayout.cellblockcontainer.prisonzones[index].GetGateLocation(), player.prisonlayout.cellblockcontainer.prisonzones[index].GetSpaceInfrontOfGate(), player.prisonlayout.cellblockcontainer.prisonzones[index].Cell_UID, true));
      for (int index = 0; index < player.shopstatus.ATMS.Count; ++index)
        MechanicController.AllMechanicLocations.Add(new MechanicLocation(player.shopstatus.ATMS[index].LocationOfThisShop, player.shopstatus.ATMS[index]));
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        if (TileData.IsThisAVendingMachine(player.shopstatus.shopentries[index].tiletype))
          MechanicController.AllMechanicLocations.Add(new MechanicLocation(player.shopstatus.shopentries[index].LocationOfThisShop, player.shopstatus.shopentries[index]));
      }
    }

    public void StartFixing()
    {
    }

    public void UpdateMechanicController(
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      ref bool IsPlayingWalkAnimation,
      float DeltaTime,
      out bool TeleportToGate,
      Player player,
      Employee Ref_Employee,
      WalkingPerson parent,
      ref bool IsWalking)
    {
      TeleportToGate = false;
      if (MechanicController.ResetMechanicStructures)
        MechanicController.SetUpAllLocations(player);
      if (!this.WasTeleported)
        return;
      this.WasTeleported = false;
    }

    public void ReachedTargetLocation(
      Vector2Int CurrentLocation,
      ref Vector2Int ForceGoHere,
      Employee Ref_Employee,
      Player player,
      PathNavigator pathnavigator,
      ref bool BlockAutoWalk,
      WalkingPerson parent,
      ref bool IsWalking)
    {
    }
  }
}
