// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Trailer.Z_Trailermanager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Bus;
using TinyZoo.Z_OverWorld.AvatarUI;
using TinyZoo.Z_OverWorld.SpawnAnimations;
using TinyZoo.Z_SummaryPopUps.People;

namespace TinyZoo.Z_Trailer
{
  internal class Z_Trailermanager
  {
    internal static List<PrisonersByCellBlock> prisonersbycellblock = new List<PrisonersByCellBlock>();
    private TrailerState trailerstate;
    private float BusZoom;
    private float DelayForBus;
    private float SpecialTimer;
    internal static bool AllowSpawns;
    internal static int SpawnProbablity = 0;
    private List<Vector2Int> LocationForPeople;
    private int EventIndex;
    private static int OrderUID = 100;

    public Z_Trailermanager(Player player)
    {
      this.DelayForBus = 2f;
      this.BusZoom = RenderMath.GetNearestPerfectPixelZoom(2f);
      OverWorldManager.IsGameIntro = false;
      Z_GameFlags.HasStartedFirstDay = true;
      Z_DebugFlags.ZooTutoriallsDisabled = true;
      Z_BusManager.AddDropOffBus(0);
      this.LocationForPeople = new List<Vector2Int>();
    }

    public void UpdateZ_TrailerManager(
      float DeltaTime,
      AvatarUIManager avatarUIManager,
      CustomerManager crowd,
      Player player)
    {
      if (SpawnBlockArray.DoingBigRing)
      {
        SpawnBlockArray.BigRingMult += DeltaTime * 0.0811f;
        if ((double) SpawnBlockArray.BigRingMult > 1.29999995231628)
          SpawnBlockArray.BigRingMult = 1.3f;
      }
      if (PC_KeyState.Seven_PressedThisFrame)
        this.DoPen(player);
      SpawnBlockArray.UpdateSpawnBlockArray(player);
      if (this.trailerstate == TrailerState.BusOnTheWayIn && Z_BusManager.busses.Count > 0)
      {
        if ((double) this.DelayForBus > 0.0)
        {
          Z_BusManager.busses[0].ForcedDropOffPause = this.DelayForBus;
          this.DelayForBus = 0.0f;
        }
        OverWorldEnvironmentManager.overworldcam.DoSmoothedRepeatingPan(new Vector3(2592f, Z_BusManager.busses[0].vLocation.Y - 10f, this.BusZoom), 0.1f, true);
        if (Z_BusManager.busses[0].drivestate != DriveState.DrivingIn && Z_BusManager.busses[0].drivestate != DriveState.None && Z_BusManager.busses[0].drivestate != DriveState.DrivingInToDropOff)
        {
          this.SpecialTimer += DeltaTime;
          if ((double) this.SpecialTimer > 1.0)
          {
            this.trailerstate = TrailerState.GateOpening;
            crowd.AddZooKeeper(player);
            OverWorldManager.overworldstate = OverWOrldState.PlayingAsAvatar;
            AvatarDisplay.DoLerp = true;
          }
        }
      }
      int trailerstate = (int) this.trailerstate;
      if (player.inputmap.PressedThisFrame[24])
        TrailerDemoFlags.FreeCam = !TrailerDemoFlags.FreeCam;
      if (PC_KeyState.F3_PressedThisFrame)
        this.LocationForPeople.Add(new Vector2Int(SpawnBlockArray.LastSetAvatarLocation));
      if (PC_KeyState.RightControl_PressedThisFrame)
        this.DoNextCameraEvent(player);
      if (PC_KeyState.F5_PressedThisFrame)
        this.LocationForPeople = new List<Vector2Int>();
      if (!PC_KeyState.Minus_PressedThisFrame || TinyZoo.Game1.gamestate == GAMESTATE.DebugAnimalViewer)
        return;
      PC_KeyState.Minus_PressedThisFrame = false;
      for (int index = 0; index < TrailerDemoFlags.CustomersToSpawn; ++index)
      {
        WalkingPerson walkingPerson = CustomerManager.AddPerson(cellblockcontainer: player.prisonlayout.cellblockcontainer, player: player);
        walkingPerson.simperson.memberofthepublic.HasSeasonPass = true;
        walkingPerson.simperson.memberofthepublic.ThisCustomerDecidedNotToPay = false;
        if (this.LocationForPeople != null && this.LocationForPeople.Count > 0)
          walkingPerson.pathnavigator.TeleportHere(this.GetLocationAroundThis(this.LocationForPeople[TinyZoo.Game1.Rnd.Next(0, this.LocationForPeople.Count)]));
      }
    }

    private void DoNextCameraEvent(Player player)
    {
      switch (this.EventIndex)
      {
        case 0:
          this.DoPen(player);
          break;
        case 1:
          Z_Trailermanager.SpawnProbablity = 10;
          this.DoPen(player);
          OverWorldEnvironmentManager.overworldcam.AddPermanentOffset(new Vector3(0.0f, 100f, -0.25f), 2f);
          ++TrailerDemoFlags.AutoRevealRange;
          break;
        case 2:
          Z_Trailermanager.SpawnProbablity = 8;
          OverWorldEnvironmentManager.overworldcam.AddPermanentOffset(new Vector3(-70f, 0.0f, -0.75f), 4f);
          ++TrailerDemoFlags.AutoRevealRange;
          break;
        case 3:
          Z_Trailermanager.SpawnProbablity = 6;
          this.DoPen(player);
          break;
        case 4:
          Z_Trailermanager.SpawnProbablity = 4;
          this.DoPen(player);
          break;
        case 5:
          Z_Trailermanager.SpawnProbablity = 2;
          OverWorldEnvironmentManager.overworldcam.AddPermanentOffset(new Vector3(0.0f, 0.0f, -1f), 4f);
          ++TrailerDemoFlags.AutoRevealRange;
          break;
        case 6:
          Z_Trailermanager.SpawnProbablity = -1;
          OverWorldEnvironmentManager.overworldcam.AddPermanentOffset(new Vector3(0.0f, 0.0f, -2.5f), 9.5f);
          SpawnBlockArray.UpdateSpawnBlockArray(player, true);
          break;
      }
      ++this.EventIndex;
    }

    private Vector2Int GetLocationAroundThis(Vector2Int Loca)
    {
      if (TrailerDemoFlags.CustomerSpawnRange <= 2)
        return Loca;
      for (int index = 0; index < 10; ++index)
      {
        int num1 = TinyZoo.Game1.Rnd.Next(-TrailerDemoFlags.CustomerSpawnRange, TrailerDemoFlags.CustomerSpawnRange);
        int num2 = TinyZoo.Game1.Rnd.Next(-TrailerDemoFlags.CustomerSpawnRange, TrailerDemoFlags.CustomerSpawnRange);
        if (TileMath.TileIsInWorld(Loca.X + num1, Loca.Y + num2) && !Z_GameFlags.pathfinder.GetIsBlocked(Loca.X + num1, Loca.Y + num2))
          return new Vector2Int(Loca.X + num1, Loca.Y + num2);
      }
      return Loca;
    }

    private void DoPen(Player player)
    {
      if (TrailerDemoFlags.PenRevealIndexes.Count <= 0)
        return;
      PrisonZone thisCellBlock = player.prisonlayout.cellblockcontainer.GetThisCellBlock(TrailerDemoFlags.PenRevealIndexes[0]);
      if (TrailerDemoFlags.PenRevealIndexes[0] == 209)
      {
        thisCellBlock.Gates[0] = new Vector2Int(151, 222);
        thisCellBlock.IncomingSignLocation = new Vector2Int(153, 222);
        thisCellBlock.TEMPSpaceBehindGate = new Vector2Int(149, 215);
      }
      else if (TrailerDemoFlags.PenRevealIndexes[0] == 208)
      {
        thisCellBlock.Gates[0] = new Vector2Int(140, 198);
        thisCellBlock.IncomingSignLocation = new Vector2Int(140, 200);
        thisCellBlock.TEMPSpaceBehindGate = new Vector2Int(145, 202);
      }
      else if (TrailerDemoFlags.PenRevealIndexes[0] == 211)
      {
        SpawnBlockArray.SetAvatarPostion(new Vector2(2757.303f, 4300.769f));
        thisCellBlock.Gates[0] = new Vector2Int(147, 195);
        thisCellBlock.IncomingSignLocation = new Vector2Int(146, 195);
        thisCellBlock.TEMPSpaceBehindGate = new Vector2Int(150, 192);
      }
      else if (TrailerDemoFlags.PenRevealIndexes[0] == 207)
      {
        thisCellBlock.Gates[0] = new Vector2Int(180, 189);
        thisCellBlock.IncomingSignLocation = new Vector2Int(165, 189);
        thisCellBlock.TEMPSpaceBehindGate = new Vector2Int(180, 189);
      }
      CascadeSpawner.DoCascadeSpawnForPen(thisCellBlock, player, true);
      PrisonersByCellBlock prisonersByCellBlock = new PrisonersByCellBlock();
      for (int index = 0; index < Z_Trailermanager.prisonersbycellblock.Count; ++index)
      {
        if (Z_Trailermanager.prisonersbycellblock[index].CellUID == thisCellBlock.Cell_UID)
          prisonersByCellBlock = Z_Trailermanager.prisonersbycellblock[index];
      }
      WaveInfo asWave = prisonersByCellBlock.GetAsWave();
      if (asWave.People.Count > 0)
      {
        asWave.OrderUID = Z_Trailermanager.OrderUID;
        player.animalsonorder.AddNewOrder(asWave, thisCellBlock.Cell_UID, player);
        player.animalsonorder.animalsonorder[player.animalsonorder.animalsonorder.Count - 1].InTransit = true;
        player.animalsonorder.animalsonorder[player.animalsonorder.animalsonorder.Count - 1].OrderUID = asWave.OrderUID;
        Vector2 tileToWorldSpace = TileMath.GetTileToWorldSpace(thisCellBlock.GetSpaceBehindGate(player));
        OverWorldEnvironmentManager.airspacemanager.AddChinookDelivery(asWave, tileToWorldSpace, Z_Trailermanager.OrderUID, false, true);
        ++Z_Trailermanager.OrderUID;
      }
      TrailerDemoFlags.PenRevealIndexes.RemoveAt(0);
    }
  }
}
