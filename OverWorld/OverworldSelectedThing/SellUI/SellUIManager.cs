// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SellUIManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell;
using TinyZoo.OverWorld.PopUps;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.PlayerDir.Quarantine;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.MoveBuilding;
using TinyZoo.Z_CustomizePen;
using TinyZoo.Z_Employees.WorkZonePane;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_ManagePen;
using TinyZoo.Z_PenInfo.MainBar;
using TinyZoo.Z_SummaryPopUps.SellStructure;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SellUI
{
  internal class SellUIManager
  {
    internal static SelectedAndSellManager selectedtileandsell;
    private Vector2Int Selllocation;
    private LayoutEntry layout;
    private bool IsCellBlock;
    private PrisonZone zone;
    private Vector2Int location;
    private HoldingCellInfo holdingcell;
    private GraveYardBlockInfo graveYard;
    private LayoutEntry Newlayout;
    private bool IsBuilding;
    private int GiveThemThis;
    private static Vector2Int LOCC = new Vector2Int();
    private bool WaitingForPopUp;
    private bool ISACtive;

    public SellUIManager() => SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;

    internal static BAR_TYPE GetCurrentBarType() => SellUIManager.selectedtileandsell != null ? SellUIManager.selectedtileandsell.GetCurrentBarType() : BAR_TYPE.Count;

    internal static void ReselectExistingTile()
    {
    }

    public void SelectedThisTile(LayoutEntry _layout, Vector2Int _location, Player player)
    {
      this.location = new Vector2Int(_location);
      Pen_SelectedPenManager.SelectedTileLocation = this.location;
      this.zone = player.prisonlayout.GetThisCellBlock(this.location, false);
      if (this.zone == null)
        this.zone = player.farms.GetThisField(this.location, false);
      if (this.zone != null)
      {
        Z_GameFlags.SelectedPrisonZoneUID = this.zone.Cell_UID;
        Z_GameFlags.SelectedPrisonZoneisFarm = this.zone.IsFarm;
      }
      this.holdingcell = player.prisonlayout.GetThisHoldingCell(this.location);
      this.ISACtive = true;
      this.Selllocation = new Vector2Int(this.location);
      this.layout = _layout;
      this.graveYard = player.prisonlayout.GetThisGraveyardBlockInfo(this.location);
      if (!TileData.CanSellThis(this.layout.tiletype) && this.zone == null)
      {
        SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
        Z_GameFlags.SetHeatMapType(HeatMapType.None);
        if (!OverWorldManager.zoopopupHolder.ScrubOnCancelBar(player))
          return;
        OverWorldManager.zoopopupHolder.SetNull();
      }
      else if (this.layout.tiletype != TILETYPE.Logo)
      {
        if (OverWorldManager.zoopopupHolder.TimboTutorial())
          return;
        if (FeatureFlags.AllowPenSelectOnly && this.zone == null)
        {
          this.ISACtive = false;
          SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
        }
        else
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.Rotate, 0.25f, -0.5f);
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.5f, -1f);
          SellUIManager.selectedtileandsell = new SelectedAndSellManager(this.layout, this.location, this.zone, this.holdingcell, this.graveYard, player);
        }
      }
      else
        SellUIManager.selectedtileandsell = new SelectedAndSellManager(this.layout, this.location, this.zone, this.holdingcell, this.graveYard, player);
    }

    public bool CheckMouseOver(Player player) => SellUIManager.selectedtileandsell != null && SellUIManager.selectedtileandsell.IsMouseOverButton(player);

    public void UpdateSellUIManager(
      float DeltaTime,
      Player player,
      OverWorldEnvironmentManager overworldenvironment)
    {
      if (SellUIManager.selectedtileandsell == null || !this.ISACtive)
        return;
      if (this.WaitingForPopUp)
      {
        if (Z_SellStructureManager.IsMove)
        {
          OverWorldManager.movebuilding = new Z_MoveBuildingManager(this.layout, this.Selllocation, player, overworldenvironment);
          OverWorldManager.overworldstate = OverWOrldState.MoveBuilding;
          Z_SellStructureManager.IsMove = false;
          Z_GameFlags.IsMovingSomething = true;
        }
        else
        {
          switch (PopUpPanel.LastButtonPressed)
          {
            case 0:
              this.WaitingForPopUp = false;
              break;
            case 1:
              this.WaitingForPopUp = false;
              if (this.graveYard != null)
              {
                if (this.graveYard.deadpeople.deadpeople.Count == 0)
                {
                  this.Newlayout = new LayoutEntry(this.layout.tiletype);
                  if (this.layout.isChild())
                    this.Newlayout.SetChild(this.layout.GetParentLocation(), this.layout.tiletype);
                  else
                    this.Newlayout.UnsetChild();
                  player.prisonlayout.SellGraveYard(this.graveYard);
                  SoundEffectsManager.PlaySpecificSound(SoundEffectType.DemolishBuilding);
                  for (int index1 = this.graveYard.GetTopLeft().X - 1; index1 < this.graveYard.GetTopLeft().X + this.graveYard.Size.X + 1; ++index1)
                  {
                    for (int index2 = this.graveYard.GetTopLeft().Y - 1; index2 < this.graveYard.GetTopLeft().Y + this.graveYard.Size.Y + 1; ++index2)
                    {
                      SellUIManager.LOCC.X = index1;
                      SellUIManager.LOCC.Y = index2;
                      if (SellUIManager.LOCC.X == this.graveYard.GetTopLeft().X + this.graveYard.Size.X && index2 == this.graveYard.GetTopLeft().Y + this.graveYard.Size.Y)
                        overworldenvironment.SellStructure(SellUIManager.LOCC, this.Newlayout, player.prisonlayout.layout, true);
                      else
                        overworldenvironment.SellStructure(SellUIManager.LOCC, this.Newlayout, player.prisonlayout.layout, true, true);
                    }
                  }
                  this.graveYard = (GraveYardBlockInfo) null;
                  SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                  break;
                }
                break;
              }
              if (this.IsBuilding)
              {
                this.DestroyBuilding(player, overworldenvironment);
                break;
              }
              break;
          }
        }
      }
      bool IsReanimate = false;
      bool IsExplainRevive = false;
      if (!this.WaitingForPopUp && GameFlags.IsUsingController && player.inputmap.PressedBackOnController())
      {
        Z_GameFlags.HandleNullingselectedtileandsell();
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
      }
      if (SellUIManager.selectedtileandsell == null)
        return;
      BuildingManageButton pressedthis = BuildingManageButton.Count;
      if (!SellUIManager.selectedtileandsell.UpdateSelectedAndSellManager(player, DeltaTime, ref pressedthis, ref IsReanimate, ref IsExplainRevive))
        return;
      switch (pressedthis)
      {
        case BuildingManageButton.StoreRoom:
          if (!TileData.IsAStoreRoom(this.layout.tiletype))
            throw new Exception("BLAH BLAH");
          OverWorldManager.zoopopupHolder.CreateZooPopUps(BuildingManageButton.StoreRoom, player);
          PenManager.SelectedPen = new Vector2Int(this.Selllocation);
          break;
        case BuildingManageButton.StoreRoomShop:
          player.livestats.SelectedSHop = new LiveSlectedShop(this.Selllocation, this.layout.tiletype);
          TinyZoo.Game1.SetNextGameState(GAMESTATE.ManageStoreRoomSetUp);
          Z_GameFlags.ForceToShopOnEnteringStoreRoom = true;
          PenManager.SelectedPen = new Vector2Int(this.Selllocation);
          TinyZoo.Game1.screenfade.BeginFade(true);
          break;
        case BuildingManageButton.TicketPrice:
          if (FeatureFlags.BlockTicketPrice)
            break;
          OverWorldManager.zoopopupHolder.CreateZooPopUps(player, POPUPSTATE.Ticket);
          break;
        case BuildingManageButton.ForceExit:
          if (Z_GameFlags.IsWaitingToReturnToControllerWalk)
          {
            Z_GameFlags.IsWaitingToReturnToControllerWalk = false;
            OverWorldManager.overworldstate = OverWOrldState.PlayingAsAvatar;
          }
          SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
          break;
        default:
          if (pressedthis == BuildingManageButton.Pen_AddItemsToPen || pressedthis == BuildingManageButton.Pen_Animals || (pressedthis == BuildingManageButton.Pen_EditPen || pressedthis == BuildingManageButton.Pen_ItemsViewEdit) || pressedthis == BuildingManageButton.Pen_EditPen)
          {
            OverWorldManager.overworldstate = OverWOrldState.CustomizePen;
            Pen_SelectedPenManager.Reconstruct = true;
            Pen_SelectedPenManager.StateToStartOn = pressedthis;
            break;
          }
          switch (pressedthis)
          {
            case BuildingManageButton.Move:
              OverWorldManager.movebuilding = new Z_MoveBuildingManager(this.layout, this.Selllocation, player, overworldenvironment);
              OverWorldManager.overworldstate = OverWOrldState.MoveBuilding;
              Z_GameFlags.IsMovingSomething = OverWorldManager.movebuilding.MovingShopWithStats;
              Z_SellStructureManager.IsMove = false;
              return;
            case BuildingManageButton.Destroy:
              if (this.zone != null)
              {
                PrisonZone thisCellBlock = player.prisonlayout.GetThisCellBlock(this.Selllocation, false);
                if (thisCellBlock == null)
                  return;
                SellUIManager.DestroyEnclosure(thisCellBlock, player, overworldenvironment, this.layout);
                return;
              }
              OverWorldManager.movebuilding = new Z_MoveBuildingManager(this.layout, this.Selllocation, player, overworldenvironment, WasDestroyButton: true);
              SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
              return;
            case BuildingManageButton.AnimalInfo:
              if (TinyZoo.Game1.GetNextGameState() == GAMESTATE.ManagePenSetUp)
                return;
              TinyZoo.Game1.SetNextGameState(GAMESTATE.ManagePenSetUp);
              TinyZoo.Game1.screenfade.BeginFade(true);
              PenManager.SelectedPen = new Vector2Int(this.Selllocation);
              return;
            case BuildingManageButton.ManageShop:
              if (!TileData.IsAModifableBuilding(this.layout.tiletype))
                throw new Exception("BLAH BLAH");
              player.livestats.SelectedSHop = new LiveSlectedShop(this.Selllocation, this.layout.tiletype);
              TinyZoo.Game1.SetNextGameState(GAMESTATE.ManageShopSetUp);
              TinyZoo.Game1.ForceSwitchToNextGameState = true;
              Z_GameFlags.ForceToNewScreen = ForceToNewScreen.GoSTraightToSpecificShopInfo;
              PenManager.SelectedPen = new Vector2Int(this.Selllocation);
              return;
            case BuildingManageButton.Architect_Design:
              if (!TileData.IsAnArchitectOffice(this.layout.tiletype))
                throw new Exception("BLAH BLAH");
              player.livestats.SelectedSHop = new LiveSlectedShop(this.Selllocation, this.layout.tiletype);
              TinyZoo.Game1.SetNextGameState(GAMESTATE.ArchitectResearchSetUp);
              PenManager.SelectedPen = new Vector2Int(this.Selllocation);
              TinyZoo.Game1.screenfade.BeginFade(true);
              return;
            case BuildingManageButton.GetStaff:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.ManageEmployee, this.Selllocation, this.layout.tiletype, player);
              return;
            case BuildingManageButton.GetParkStaff:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(BuildingManageButton.GetParkStaff, player);
              return;
            case BuildingManageButton.Pen_GetMoreAnimals:
              TinyZoo.Game1.SetNextGameState(GAMESTATE.WorldMapSetUp);
              PenManager.SelectedPen = new Vector2Int(this.Selllocation);
              TinyZoo.Game1.screenfade.BeginFade(true);
              return;
            case BuildingManageButton.Nursery_Breeding:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(player, this.Selllocation, POPUPSTATE.BreedSelection);
              return;
            case BuildingManageButton.ManageProcessingPlant:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.ManageProcessing, player, this.layout.tiletype, this.location);
              return;
            case BuildingManageButton.AreaOfCollection:
              new WorkZoneInfo().workzonetype = WorkZoneType.Pens;
              WorkZoneInfo workzoneinfo;
              if (this.layout.tiletype == TILETYPE.MeatProcessor)
              {
                workzoneinfo = player.animalProcessing.GetBuildingByUID(player.shopstatus.GetThisFacility(Pen_SelectedPenManager.SelectedTileLocation).ShopUID, out int _).workzoneinfo;
              }
              else
              {
                if (!TileData.IsAnIncinerator(this.layout.tiletype))
                  throw new Exception("GE THIS THING");
                workzoneinfo = player.animalincineration.GetIncinerationBuilding(player.shopstatus.GetThisFacility(Pen_SelectedPenManager.SelectedTileLocation).ShopUID).workzoneinfo;
              }
              OverWorldManager.zoopopupHolder.CreateZooPopUps(workzoneinfo, this.layout.tiletype, SellUIManager.selectedtileandsell);
              return;
            case BuildingManageButton.CRISPR_menu:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(player, this.Selllocation, POPUPSTATE.CRISPR);
              return;
            case BuildingManageButton.ParkSummary:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(pressedthis, player);
              return;
            case BuildingManageButton.Transport:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(pressedthis, player);
              return;
            case BuildingManageButton.Surveillance_People:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(pressedthis, player);
              return;
            case BuildingManageButton.Quarantine:
              int shopUid = player.shopstatus.GetThisFacility(Pen_SelectedPenManager.SelectedTileLocation).ShopUID;
              QuarantineBuilding quarantineBuilding = player.animalquarantine.GetThisQuarantineBuilding(shopUid);
              OverWorldManager.zoopopupHolder.CreateZooPopUps(quarantineBuilding, player);
              return;
            case BuildingManageButton.QuarantineSettings:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(pressedthis, player);
              return;
            case BuildingManageButton.Farm_EditCrop:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(player, Z_GameFlags.SelectedPrisonZoneUID, POPUPSTATE.Crops);
              return;
            case BuildingManageButton.Slaughterhouse_Culling:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(pressedthis, player);
              return;
            case BuildingManageButton.Warehouse:
              OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.Warehouse, player, this.layout.tiletype, this.location);
              return;
            default:
              if (pressedthis == BuildingManageButton.Transfer | IsReanimate | IsExplainRevive)
              {
                TinyZoo.Game1.SetNextGameState(GAMESTATE.ManagePenSetUp);
                TinyZoo.Game1.screenfade.BeginFade(true);
                PenManager.SelectedPen = new Vector2Int(this.Selllocation);
                if (true)
                  return;
                if (this.layout.tiletype == TILETYPE.Research_PrisonPlanet)
                {
                  OWHUDManager.ActivateResearchUI(this.layout, player);
                  SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                  return;
                }
                if (this.layout.tiletype == TILETYPE.HoldingCell)
                {
                  OWHUDManager.ActivateTransferUI(this.zone, this.holdingcell, player, IsReanimate, this.location);
                  SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                  return;
                }
                if (IsExplainRevive)
                {
                  if (this.graveYard.deadpeople.deadpeople.Count == 0)
                  {
                    OWHUDManager.DoPopUp("There are no prisoners here to revive.", false, player);
                    return;
                  }
                  if (!player.Stats.research.BuildingsResearched.Contains(TILETYPE.HoldingCell))
                  {
                    OWHUDManager.DoPopUp("This feature is not available yet", false, player);
                    return;
                  }
                  OWHUDManager.DoPopUp("To revive a prisoner, select their gravestone.", false, player);
                  return;
                }
                if (player.prisonlayout.HasHoldingCellTransferSlots(this.location) > 0)
                {
                  OWHUDManager.ActivateTransferUI(this.zone, this.holdingcell, player, IsReanimate, this.location);
                  SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                  return;
                }
                if (!player.prisonlayout.HasAnyHoldingCells())
                {
                  string Text = "You must research and build Holding Cells in order to be able to move your prisoners between cell blocks.";
                  if (IsReanimate)
                    Text = "You must research and build Holding Cells in order to revive prisoners.";
                  OWHUDManager.DoPopUp(Text, false, player);
                  return;
                }
                if (player.prisonlayout.HasSpaceInHoldingCells() > 0)
                {
                  OWHUDManager.ActivateTransferUI(this.zone, this.holdingcell, player, IsReanimate, this.location);
                  SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
                  return;
                }
                string Text1 = "Your Holding Cells are all full. Please build more in order to transfer more prisoners.";
                if (IsReanimate)
                  Text1 = "Your Holding Cells are all full. Please build more in order to revive prisoners.";
                OWHUDManager.DoPopUp(Text1, false, player);
                return;
              }
              switch (pressedthis)
              {
                case BuildingManageButton.CRISPR_menu:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(player, this.Selllocation, POPUPSTATE.ChangeBuildingSkin);
                  return;
                case BuildingManageButton.ChangeBuildingSkin:
                  if (this.layout.tiletype == TILETYPE.StoreRoom && OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.location.X, this.location.Y].IsStillBuilding())
                    return;
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.ChangeBuildingSkin, player, this.layout.tiletype, this.location);
                  return;
                case BuildingManageButton.Collection:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(player, POPUPSTATE.Collection);
                  return;
                case BuildingManageButton.Tasks:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps((HeroQuestDescription) null, player, POPUPSTATE.HeroQuests, false);
                  return;
                case BuildingManageButton.RideTicketing:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.RideTicket, player, this.layout.tiletype, this.location);
                  return;
                case BuildingManageButton.Pen_DeliveryOrders:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(player, false, this.zone, POPUPSTATE.AnimalDelivery);
                  return;
                case BuildingManageButton.DiseaseResearch:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(BuildingManageButton.DiseaseResearch, player);
                  return;
                case BuildingManageButton.MedicalJournal:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(BuildingManageButton.MedicalJournal, player);
                  return;
                case BuildingManageButton.VetVistSummary:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(BuildingManageButton.VetVistSummary, player);
                  return;
                case BuildingManageButton.ManageIncinerator:
                  OverWorldManager.zoopopupHolder.CreateZooPopUps(POPUPSTATE.Incinerator, player, this.layout.tiletype, this.location);
                  return;
                default:
                  if (player.prisonlayout.GetThisCellBlock(this.Selllocation, false) != null)
                  {
                    this.IsBuilding = false;
                    if (player.prisonlayout.cellblockcontainer.prisonzones.Count == 1)
                    {
                      OWHUDManager.DoPopUp("You cannot destroy your last enclosure", false, player);
                      return;
                    }
                    int count = player.prisonlayout.GetThisCellBlock(this.Selllocation, false).prisonercontainer.prisoners.Count;
                    this.WaitingForPopUp = true;
                    OWHUDManager.DoSellBuildingPopUp("Are you sure you want to destroy this structure?~Any animals inside will be randomly distributed to other enclosures", true, true, player);
                    return;
                  }
                  if (this.graveYard != null)
                  {
                    this.IsBuilding = false;
                    this.WaitingForPopUp = true;
                    if (this.graveYard.deadpeople.deadpeople.Count == 0)
                    {
                      OWHUDManager.DoPopUp("Are you sure you want to destroy this graveyard?", true, player);
                      return;
                    }
                    if (!player.Stats.research.BuildingsResearched.Contains(TILETYPE.HoldingCell))
                    {
                      OWHUDManager.DoPopUp("You need to revive all of the people here before you can demolish this graveyard.~To do that you will need to research Holding Cells.", false, player);
                      return;
                    }
                    OWHUDManager.DoPopUp("You must revive all the prisoners from this cell before you can demolish this graveyard.", false, player);
                    return;
                  }
                  if (this.layout.tiletype == TILETYPE.HoldingCell && player.prisonlayout.cellblockcontainer.GetThisHoldingCell(this.location).prisonercontainer.prisoners.Count > 0)
                  {
                    OWHUDManager.DoPopUp("You cannot demolish a holding cell that still has prisoners inside.", false, player);
                    this.WaitingForPopUp = true;
                    return;
                  }
                  int Duplicates = player.prisonlayout.cellblockcontainer.GetCountOfThisSpecialBuilding(this.layout.tiletype) - 1;
                  int cost = player.livestats.GetBlingdingcosts().GetCost(this.layout.tiletype, Duplicates);
                  this.GiveThemThis = cost;
                  OWHUDManager.DoSellBuildingPopUp("Demolish this and get $" + (object) cost + " in return?", true, false, player);
                  this.WaitingForPopUp = true;
                  this.IsBuilding = true;
                  return;
              }
          }
      }
    }

    public void Deactivate() => this.ISACtive = false;

    internal static void DestroyEnclosure(
      PrisonZone prisonzone,
      Player player,
      OverWorldEnvironmentManager overworldenvironment,
      LayoutEntry layout)
    {
      LayoutEntry _layoutentry = new LayoutEntry(layout.tiletype);
      if (layout.isChild())
        _layoutentry.SetChild(layout.GetParentLocation(), layout.tiletype);
      else
        _layoutentry.UnsetChild();
      OverWorldManager.overworldenvironment.animalsinpens.MovePen_A_DeleteAllDynamicObjects(prisonzone.Cell_UID, player, prisonzone);
      player.prisonlayout.DestroyCellBlock(prisonzone.Cell_UID, player, overworldenvironment.wallsandfloors);
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.DemolishBuilding);
      if (prisonzone.IsIerregular)
      {
        for (int index = 0; index < prisonzone.FenceTiles.Count; ++index)
          overworldenvironment.SellStructure(prisonzone.FenceTiles[index], _layoutentry, player.prisonlayout.layout, true, true);
        for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
          overworldenvironment.SellStructure(prisonzone.FloorTiles[index], _layoutentry, player.prisonlayout.layout, true, index != prisonzone.FloorTiles.Count - 1);
      }
      else
      {
        for (int index1 = prisonzone.TopLeftFloorSpace.X - 1; index1 < prisonzone.TopLeftFloorSpace.X + prisonzone.WidthAndHeight.X + 1; ++index1)
        {
          for (int index2 = prisonzone.TopLeftFloorSpace.Y - 1; index2 < prisonzone.TopLeftFloorSpace.Y + prisonzone.WidthAndHeight.Y + 1; ++index2)
          {
            SellUIManager.LOCC.X = index1;
            SellUIManager.LOCC.Y = index2;
            if (SellUIManager.LOCC.X == prisonzone.TopLeftFloorSpace.X + prisonzone.WidthAndHeight.X && index2 == prisonzone.TopLeftFloorSpace.Y + prisonzone.WidthAndHeight.Y)
              overworldenvironment.SellStructure(SellUIManager.LOCC, _layoutentry, player.prisonlayout.layout, true);
            else
              overworldenvironment.SellStructure(SellUIManager.LOCC, _layoutentry, player.prisonlayout.layout, true, true);
          }
        }
      }
      overworldenvironment.DeletePeopleAfterSellingPrison(prisonzone.Cell_UID);
      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
      prisonzone = (PrisonZone) null;
      player.OldSaveThisPlayer();
    }

    internal static void DestroyBuildingStatic(
      Player player,
      OverWorldEnvironmentManager overworldenvironment,
      LayoutEntry destroythis,
      Vector2Int Selllocation,
      ref int CashRefund,
      bool IsPenItem)
    {
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.DemolishBuilding);
      LayoutEntry _layoutentry = new LayoutEntry(destroythis.tiletype);
      _layoutentry.RotationClockWise = destroythis.RotationClockWise;
      if (destroythis.isChild())
        _layoutentry.SetChild(destroythis.GetParentLocation(), destroythis.tiletype);
      else
        _layoutentry.UnsetChild();
      player.prisonlayout.SellStructure(Selllocation, _layoutentry, player.livestats.consumptionstatus, player, IsPenItem: IsPenItem);
      overworldenvironment.SellStructure(Selllocation, _layoutentry, player.prisonlayout.layout);
      CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd, 0.5f);
      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
      player.Stats.GiveCash(CashRefund, player);
      CashRefund = 0;
    }

    private void DestroyBuilding(Player player, OverWorldEnvironmentManager overworldenvironment)
    {
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.DemolishBuilding);
      LayoutEntry _layoutentry = new LayoutEntry(this.layout.tiletype);
      _layoutentry.RotationClockWise = this.layout.RotationClockWise;
      if (this.layout.isChild())
        _layoutentry.SetChild(this.layout.GetParentLocation(), this.layout.tiletype);
      else
        _layoutentry.UnsetChild();
      player.prisonlayout.SellStructure(this.Selllocation, _layoutentry, player.livestats.consumptionstatus, player);
      overworldenvironment.SellStructure(this.Selllocation, _layoutentry, player.prisonlayout.layout);
      CameraShake.BeginCameraShake(TinyZoo.Game1.Rnd, 0.5f);
      SellUIManager.selectedtileandsell = (SelectedAndSellManager) null;
      player.Stats.GiveCash(this.GiveThemThis, player);
      this.GiveThemThis = 0;
      this.IsBuilding = false;
    }

    public void DrawSellUIManager(Player player)
    {
      if (SellUIManager.selectedtileandsell == null || !this.ISACtive)
        return;
      SellUIManager.selectedtileandsell.DrawSelectedAndSellManager(player);
    }
  }
}
