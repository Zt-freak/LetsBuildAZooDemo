// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DragBuilder.Z_DragBuildManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using SEngine.Input;
using SEngine.Objects;
using System;
using System.Collections.Generic;
using TinyZoo.Audio;
using TinyZoo.GenericUI.Path_Renderer;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.CellBlocks.Pen_Items;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_BalanceSystems.Animals.Enrichment;
using TinyZoo.Z_BalanceSystems.Park;
using TinyZoo.Z_BalanceSystems.Publicity;
using TinyZoo.Z_BarMenu.Pen.BuildMenu;
using TinyZoo.Z_Employees.QuickPick;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD.Z_HeroQuests_Pins;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Notification;
using TinyZoo.Z_OverWorld.SpawnAnimations;

namespace TinyZoo.Z_BuldMenu.DragBuilder
{
  internal class Z_DragBuildManager
  {
    private TileRenderer tilerenderer;
    private TileRenderer EmptyTroughtilerenderer;
    private ZooBuildingTopRenderer toprenderer;
    private ZooBuildingTopRenderer EmptyTroughTopRenderer;
    private Vector2Int LastLocation;
    private ThingToBuildFootPrint footprint;
    private Vector2 StartLocation;
    private BlockInfo block;
    private List<BuildHistory> buildhistory;
    private int RotationClockWise;
    private TILETYPE tiletype;
    private TileInfo info;
    private bool AllowPaintMode;
    private bool IsFloorLayer;
    private bool IsMove;
    private PrisonZone decoratethisprisonzone;
    public bool SwitchedPrisonZone;
    public bool CameFromMainBarManager;
    private bool IsPenWater;
    private bool WaitForReleaseAButton;
    private bool BlockUntilMove;
    private float BlockTimer;
    public bool WillSave;
    private bool WasPainting;
    private bool IsDeletingInPaint;
    public Vector2Int BuiltHereOnMove;
    private bool IsWater;
    private bool HasNOTMovedSelectionSincePurchase;
    private TileInfo Troughinfo;
    private PathRendererWithFinder pathrenderer;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public Z_DragBuildManager(
      TILETYPE _tiletype,
      Player player,
      bool _AllowPaintMode,
      bool _IsFloorLayer = false,
      bool _IsMove = false,
      PrisonZone _decoratethisprisonzone = null,
      bool _CameFromMainBarManager = false,
      bool _IsPenWater = false)
    {
      this.pathrenderer = new PathRendererWithFinder();
      if (GameFlags.IsUsingController)
        this.WaitForReleaseAButton = player.inputmap.HeldButtons[0];
      OverworldBuildManager.currentbuildstate = !_AllowPaintMode ? BUILDSTATEFORCONTROLLERHINT.Structure : BUILDSTATEFORCONTROLLERHINT.Paint;
      if (OverWorldManager.zoopopupHolder.ScrubOnEnteringBuildMode(player))
        OverWorldManager.zoopopupHolder.SetNull();
      this.IsPenWater = _IsPenWater;
      this.CameFromMainBarManager = _CameFromMainBarManager;
      this.decoratethisprisonzone = _decoratethisprisonzone;
      this.IsMove = _IsMove;
      this.IsFloorLayer = _IsFloorLayer;
      this.AllowPaintMode = _AllowPaintMode;
      this.tiletype = _tiletype;
      this.RotationClockWise = 0;
      this.buildhistory = new List<BuildHistory>();
      this.info = TileData.GetTileInfo(this.tiletype);
      this.LastLocation = TileMath.GetScreenCenter();
      this.tilerenderer = new TileRenderer(new LayoutEntry(this.tiletype)
      {
        RotationClockWise = this.RotationClockWise
      }, this.LastLocation.X, this.LastLocation.Y, false, true);
      if (this.info.HasBuildingLayer)
        this.toprenderer = new ZooBuildingTopRenderer(this.info, this.LastLocation.X, this.LastLocation.Y, this.RotationClockWise, this.tilerenderer);
      this.footprint = new ThingToBuildFootPrint(this.tilerenderer.XWidth, this.tilerenderer.YHeight, this.tilerenderer, this.info, _decoratethisprisonzone);
      this.StartLocation = new Vector2(Sengine.WorldOriginandScale.X, Sengine.WorldOriginandScale.Y);
      if (this.tiletype == TILETYPE.WaterPumpStation || this.IsPenWater)
      {
        Z_GameFlags.SetHeatMapType(HeatMapType.Water);
        if (!this.IsPenWater)
          return;
        this.Troughinfo = TileData.GetTileInfo(TileData.GetWaterTroughToEmptyTrough(this.tiletype));
        this.EmptyTroughtilerenderer = new TileRenderer(new LayoutEntry(TileData.GetWaterTroughToEmptyTrough(this.tiletype)), this.LastLocation.X, this.LastLocation.Y, false, true);
        this.EmptyTroughTopRenderer = new ZooBuildingTopRenderer(this.Troughinfo, this.LastLocation.X, this.LastLocation.Y, this.RotationClockWise, this.EmptyTroughtilerenderer);
      }
      else
        Z_GameFlags.SetHeatMapType(HeatMapType.None);
    }

    public bool GetIsBlocked() => this.block != null && !this.block.SomethingIsBlocked;

    public void UpdateZ_DragBuildManager(
      Player player,
      WallsAndFloorsManager wallsandfloors,
      out bool Built,
      float DeltaTime,
      out bool ExitBackToMainBarManager,
      bool MouseBlockedByUI,
      out bool TriedToBuyButCouldNotAfford)
    {
      TriedToBuyButCouldNotAfford = false;
      ExitBackToMainBarManager = false;
      Built = false;
      if (this.BlockUntilMove)
      {
        this.BlockTimer -= DeltaTime;
        if ((double) this.BlockTimer <= 0.0)
          this.BlockUntilMove = false;
      }
      if (this.WaitForReleaseAButton)
      {
        if (GameFlags.IsUsingController)
        {
          if ((double) player.player.touchinput.ReleaseTapArray[0].X >= 0.0)
          {
            player.player.touchinput.ReleaseTapArray[0].X = -10000f;
            this.WaitForReleaseAButton = false;
          }
        }
        else
          this.WaitForReleaseAButton = false;
      }
      if (player.inputmap.RightMousReleaseClick || player.inputmap.PressedThisFrame[12])
      {
        LayoutEntry layourentry = new LayoutEntry(this.tiletype);
        ++this.RotationClockWise;
        if (this.RotationClockWise >= TileData.GetTileInfo(this.tiletype).GetMaximumRotations())
          this.RotationClockWise = 0;
        Console.WriteLine("Rotation = " + (object) this.RotationClockWise);
        layourentry.RotationClockWise = this.RotationClockWise;
        this.tilerenderer = new TileRenderer(layourentry, this.LastLocation.X, this.LastLocation.Y, false, true);
        this.IsWater = TileData.IsThisWater(this.tiletype);
        this.footprint = new ThingToBuildFootPrint(this.tilerenderer.XWidth, this.tilerenderer.YHeight, this.tilerenderer, this.info);
        this.toprenderer = !this.info.HasBuildingLayer ? (ZooBuildingTopRenderer) null : new ZooBuildingTopRenderer(this.info, this.LastLocation.X, this.LastLocation.Y, this.RotationClockWise, this.tilerenderer);
        this.LastLocation.X = -10000;
      }
      bool flag1 = !MouseBlockedByUI;
      if (MouseBlockedByUI && this.IsMove)
        flag1 = false;
      if (this.CameFromMainBarManager)
      {
        bool MouseOverlappingSomething = false;
        if (BuildMenuBarManager.UpdateBuildMenuBarManagerFromExternal(player, DeltaTime, out MouseOverlappingSomething, wallsandfloors, this.decoratethisprisonzone))
        {
          Z_GameFlags.SetHeatMapType(HeatMapType.None);
          ExitBackToMainBarManager = true;
        }
        if (!MouseOverlappingSomething)
          flag1 = true;
      }
      if (flag1)
        this.SetLocation(player, wallsandfloors, false);
      if (Z_GameFlags.MouseIsOverAPanel)
        return;
      if (this.AllowPaintMode)
      {
        if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X > 0.0 && (double) player.player.touchinput.MultiTouchTouchLocations[0].Y < (double) Z_BuildingIconPanel.MinHeight)
        {
          if (this.block != null && !this.block.SomethingIsBlocked)
          {
            bool NEWBUILD = true;
            if (!this.WasPainting)
            {
              this.WasPainting = true;
              this.IsDeletingInPaint = player.prisonlayout.IsThisAlreadyHere(this.tilerenderer, true);
            }
            else if (!this.IsDeletingInPaint && player.prisonlayout.IsThisAlreadyHere(this.tilerenderer, true))
              NEWBUILD = false;
            if (NEWBUILD)
            {
              int cost = player.livestats.GetCost(this.tilerenderer.tiletypeonconstruct, player, true);
              bool flag2 = false;
              if (!this.IsDeletingInPaint)
              {
                if (player.Stats.SpendCash(cost, SpendingCashOnThis.BuyBuilding, player))
                  flag2 = true;
                else
                  TriedToBuyButCouldNotAfford = true;
              }
              else
              {
                flag2 = true;
                bool flag3 = false;
                for (int index = this.buildhistory.Count - 1; index > -1; --index)
                {
                  if (this.buildhistory[index].Overlaps(this.tilerenderer.TileLocation))
                  {
                    flag3 = true;
                    break;
                  }
                }
                if (flag3)
                  player.Stats.GiveCash(cost, player);
              }
              if (flag2)
              {
                if (!this.IsFloorLayer)
                  throw new Exception("NOT WRITTEN FOR ANYTHING OTHER THAN THE FLOOR");
                int num = this.IsWater ? 1 : 0;
                bool IsUnderFloor = false;
                if (TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[this.tilerenderer.TileLocation.X, this.tilerenderer.TileLocation.Y].tiletype))
                  IsUnderFloor = true;
                if (player.prisonlayout.IsThisAlreadyHere(this.tilerenderer, true, IsUnderFloor))
                {
                  if (this.IsDeletingInPaint)
                    this.DeleteTile(player, wallsandfloors, ref NEWBUILD, IsUnderFloor);
                }
                else if (!this.IsDeletingInPaint)
                {
                  this.WillSave = true;
                  this.buildhistory.Add(new BuildHistory(this.tilerenderer.TileLocation, this.tilerenderer.tiletypeonconstruct, this.tilerenderer, player));
                  SoundEffectsManager.PlaySpecificSound(SoundEffectType.Build_Paint);
                  player.prisonlayout.BuildTileFromTileRenderer(this.tilerenderer, player.livestats.consumptionstatus, player, this.IsFloorLayer, IsUnderFloor);
                  wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true);
                }
              }
            }
          }
        }
        else
          this.WasPainting = false;
      }
      else if (this.block != null)
      {
        if (!this.block.SomethingIsBlocked && !this.HasNOTMovedSelectionSincePurchase)
        {
          if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0 && (!MouseBlockedByUI || this.IsMove))
          {
            int num1 = 0;
            int SpendThis = player.livestats.GetCost(this.tilerenderer.tiletypeonconstruct, player, true);
            if (this.IsMove)
              SpendThis = 0;
            else if (TileData.IsThisAnyKindOfDeco(this.tilerenderer.tiletypeonconstruct))
              num1 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
            else if (TileData.DoesThisImapactPublicty(this.tilerenderer.tiletypeonconstruct))
              num1 = PublicityCalculator.CalculatePublicity(player);
            if (player.Stats.SpendCash(SpendThis, SpendingCashOnThis.BuyBuilding, player))
            {
              if (!this.IsMove)
              {
                if (TileData.IsThisAnyKindOfDeco(this.tilerenderer.tiletypeonconstruct))
                {
                  int num2 = (int) Math.Round((double) DecoCalculator.CalculateDeco(player));
                  NotificationBubbleManager.QuickAddNotification((float) num1, (float) num2, BubbleMainType.Deco);
                }
                else if (TileData.DoesThisImapactPublicty(this.tilerenderer.tiletypeonconstruct))
                {
                  DecoCalculator.RecalculateDeco = true;
                  PublicityCalculator.RecalculatePublicity = true;
                  double deco = (double) DecoCalculator.CalculateDeco(player);
                  int publicity = PublicityCalculator.CalculatePublicity(player);
                  NotificationBubbleManager.QuickAddNotification((float) num1, (float) publicity, BubbleMainType.Publicity);
                }
                else if (TileData.IsACRISPRBuilding(this.tilerenderer.tiletypeonconstruct))
                  Z_QuestPinManager.DoubleCheckTaskNotifications = true;
              }
              this.HasNOTMovedSelectionSincePurchase = true;
              this.WillSave = true;
              player.player.touchinput.ReleaseTapArray[0].X = -1000f;
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
              BuildMenuBarManager.BuiltSomethingThisFrame = true;
              bool IsDynamicItemForPen = false;
              if (this.decoratethisprisonzone != null)
              {
                ENRICHMENTBEHAVIOUR enrchmentbehaviour;
                PenItem penitem = this.decoratethisprisonzone.penItems.AddNewItem(this.tilerenderer, out IsDynamicItemForPen, out enrchmentbehaviour);
                if (IsDynamicItemForPen || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Trampoline || enrchmentbehaviour == ENRICHMENTBEHAVIOUR.Perch)
                {
                  OverWorldManager.overworldenvironment.animalsinpens.AddDynamicItemToCellBlock(this.tilerenderer, this.decoratethisprisonzone.Cell_UID, this.toprenderer, enrchmentbehaviour, penitem);
                  if (this.IsMove)
                    Built = true;
                  Z_NotificationManager.JustAddedEnrichmentToThis(this.decoratethisprisonzone.Cell_UID);
                  Z_NotificationManager.RescubEnrichment = true;
                  EnrichmentCalculator.Cal_Enichment(this.decoratethisprisonzone, true);
                  FinalizeAnimalStats.SetEnrichment(this.decoratethisprisonzone);
                  return;
                }
                if (TileData.IsThisAWaterTrough(this.tilerenderer.tiletypeonconstruct))
                {
                  Z_NotificationManager.RescrubWater = true;
                  NewDay_ByPen.RecountWaterCoverage(player, this.decoratethisprisonzone.Cell_UID);
                }
                else
                {
                  EnrichmentCalculator.Cal_Enichment(this.decoratethisprisonzone, true);
                  FinalizeAnimalStats.SetEnrichment(this.decoratethisprisonzone);
                }
                Z_NotificationManager.JustAddedEnrichmentToThis(this.decoratethisprisonzone.Cell_UID);
                Z_NotificationManager.RescubEnrichment = true;
              }
              if (!IsDynamicItemForPen)
              {
                player.prisonlayout.BuildTileFromTileRenderer(this.tilerenderer, player.livestats.consumptionstatus, player, IsPenItem: (this.decoratethisprisonzone != null));
                if (TileData.IsThisAShopWithShopStats(this.tilerenderer.tiletypeonconstruct))
                  Z_NotificationManager.RescrubShops = true;
                else if (TileData.IsAStoreRoom(this.tilerenderer.tiletypeonconstruct))
                  Z_NotificationManager.RecountAllEvents = true;
              }
              player.livestats.LastCalculatedFacilities = FacilitiesCalulator.CalculateFacilities(player);
              int Shop_UID;
              player.shopstatus.BuiltABuilding(this.tilerenderer.TileLocation, this.tilerenderer.tiletypeonconstruct, this.tilerenderer.RotationOnConstruct, player, this.IsMove, out Shop_UID);
              wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout);
              CascadeSpawner.DoCascadeForBuildingorTree(wallsandfloors.tilesasarray[this.tilerenderer.TileLocation.X, this.tilerenderer.TileLocation.Y]);
              this.BlockUntilMove = true;
              this.BlockTimer = 0.4f;
              this.buildhistory.Add(new BuildHistory(this.tilerenderer.TileLocation, this.tilerenderer.tiletypeonconstruct, this.tilerenderer, player));
              if (this.IsMove)
              {
                this.BuiltHereOnMove = new Vector2Int(this.tilerenderer.TileLocation);
                Built = true;
              }
              else if (EmployeeData.ThisStoreHasAnEmployee(this.tilerenderer.tiletypeonconstruct))
              {
                OverWorldManager.quickpickemployeemanager = new QuickPickEmployeeManager(this.tilerenderer.tiletypeonconstruct, Shop_UID, player);
                OverWorldManager.overworldstate = OverWOrldState.QuickPickEmployee;
              }
            }
            else
              TriedToBuyButCouldNotAfford = true;
          }
        }
        else if ((double) player.player.touchinput.ReleaseTapArray[0].X > 0.0)
        {
          for (int index = this.buildhistory.Count - 1; index > -1; --index)
          {
            if (this.buildhistory[index].SmartOverlaps(this.tilerenderer.TileLocation, this.tilerenderer))
            {
              int GiveThis = player.livestats.GetCost(this.tilerenderer.tiletypeonconstruct, player, true);
              if (this.IsMove)
              {
                GiveThis = 0;
                Built = true;
              }
              player.player.touchinput.ReleaseTapArray[0].X = -1000f;
              player.Stats.GiveCash(GiveThis, player);
              SoundEffectsManager.PlaySpecificSound(SoundEffectType.Unbuild);
              LayoutEntry baseTileType = player.prisonlayout.layout.BaseTileTypes[this.buildhistory[index].TileLocation.X, this.buildhistory[index].TileLocation.Y];
              player.prisonlayout.SellStructure(this.buildhistory[index].TileLocation, baseTileType, player.livestats.consumptionstatus, player);
              if (this.decoratethisprisonzone != null)
                this.decoratethisprisonzone.penItems.RemoveItem(this.buildhistory[index].TileLocation, this.buildhistory[index].tiletypeonconstruct, this.decoratethisprisonzone.Cell_UID, player);
              player.shopstatus.SellBuilding(this.buildhistory[index].TileLocation, this.buildhistory[index].tiletypeonconstruct, player, false);
              this.buildhistory.RemoveAt(index);
              wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout);
            }
          }
        }
      }
      int num3 = MouseStatus.RMouseHeld ? 1 : 0;
      this.tilerenderer.UpdateTileRenderer(DeltaTime);
    }

    private void DeleteTile(
      Player player,
      WallsAndFloorsManager wallsandfloors,
      ref bool NEWBUILD,
      bool IsUnderFloor)
    {
      bool flag = false;
      for (int index = this.buildhistory.Count - 1; index > -1; --index)
      {
        if (this.buildhistory[index].TileLocation.CompareMatches(this.tilerenderer.TileLocation))
        {
          player.prisonlayout.RevertFloor(this.tilerenderer.TileLocation, this.buildhistory[index]);
          this.buildhistory.RemoveAt(index);
          wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, this.tilerenderer.TileLocation);
          NEWBUILD = false;
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      player.prisonlayout.RevertFloor(this.tilerenderer.TileLocation, IsUnderFloor);
      wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, this.tilerenderer.TileLocation);
    }

    public void SetLocation(
      Player player,
      WallsAndFloorsManager wallsandfloors,
      bool ForceRefreshFootPrint)
    {
      GameFlags.IsUsingMouse = !MouseStatus.RMouseHeld;
      Z_GameFlags.ForceRightMouseDrag = true;
      Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.inputmap.PointerLocation);
      if (!spaceToTileLocation.CompareMatches(this.LastLocation) | ForceRefreshFootPrint)
      {
        this.BlockUntilMove = false;
        this.HasNOTMovedSelectionSincePurchase = false;
        this.LastLocation = spaceToTileLocation;
        this.tilerenderer.TileLocation = new Vector2Int(this.LastLocation);
        if (this.decoratethisprisonzone != null)
        {
          int cell = Enclosure_Farm_Map.GetCell(this.tilerenderer.TileLocation.X, this.tilerenderer.TileLocation.Y, player);
          if (cell > 0 && cell != this.decoratethisprisonzone.Cell_UID)
          {
            this.SwitchedPrisonZone = true;
            this.decoratethisprisonzone = player.prisonlayout.GetThisCellBlock(cell);
          }
          this.block = wallsandfloors.CanBuildThisHere(this.tilerenderer.TileLocation, this.tilerenderer, this.IsFloorLayer, true, this.decoratethisprisonzone, this.IsPenWater);
        }
        else
          this.block = wallsandfloors.CanBuildThisHere(this.tilerenderer.TileLocation, this.tilerenderer, this.IsFloorLayer, layoudata: player.prisonlayout.layout);
        this.footprint.SetBlocks(this.block, this.tilerenderer, this.IsPenWater, this.decoratethisprisonzone);
        if (this.decoratethisprisonzone != null)
          this.pathrenderer.TrySetPathInPrisonZone(this.footprint, this.decoratethisprisonzone, this.tilerenderer.TileLocation);
        if (this.IsWater && !this.footprint.HasAccessToWater)
          this.EmptyTroughtilerenderer.TileLocation = new Vector2Int(this.LastLocation);
      }
      if (this.BlockUntilMove)
        return;
      this.StartLocation = RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation);
    }

    public void DrawZ_DragBuildManager(bool BlockBuildingDrawPreview)
    {
      Z_GameFlags.pathfinder.DrawEntranceBlocks();
      if (Z_GameFlags.MouseIsOverAPanel || BlockBuildingDrawPreview || this.BlockUntilMove)
        return;
      if (this.tilerenderer.Ref_layoutentry.tiletype == TILETYPE.WaterPumpStation)
        WaterInfluenceZone.DrawDrawWater(this.LastLocation);
      this.tilerenderer.vLocation = this.StartLocation;
      this.tilerenderer.SetLocation(this.LastLocation.X, this.LastLocation.Y);
      this.tilerenderer.fAlpha = 1f;
      this.tilerenderer.scale = 1f;
      this.tilerenderer.DrawTileRenderer(AssetContainer.pointspritebatch01, ref Z_DragBuildManager.ThreadLoc, ref Z_DragBuildManager.ThreadScale);
      this.tilerenderer.HasDrawn = false;
      this.tilerenderer.fAlpha = 0.5f;
      this.tilerenderer.scale = FlashingAlpha.GetUpwardsPulseMedium();
      this.tilerenderer.DrawTileRenderer(AssetContainer.PointBlendBatch04, ref Z_DragBuildManager.ThreadLoc, ref Z_DragBuildManager.ThreadScale);
      if (this.IsPenWater && !this.footprint.HasAccessToWater)
      {
        this.EmptyTroughTopRenderer.fAlpha = 1f;
        this.EmptyTroughTopRenderer.scale = 1f;
        this.EmptyTroughTopRenderer.vLocation = this.tilerenderer.vLocation;
        this.EmptyTroughTopRenderer.DrawZooBuildingTopRenderer(ref Z_DragBuildManager.ThreadLoc, ref Z_DragBuildManager.ThreadScale);
        this.EmptyTroughTopRenderer.fAlpha = 0.5f;
        this.EmptyTroughTopRenderer.scale = FlashingAlpha.GetUpwardsPulseMedium();
        this.EmptyTroughTopRenderer.DrawZooBuildingTopRendererWithoutNight(AssetContainer.PointBlendBatch04);
      }
      else if (this.toprenderer != null)
      {
        this.toprenderer.fAlpha = 1f;
        this.toprenderer.scale = 1f;
        this.toprenderer.vLocation = this.tilerenderer.vLocation;
        this.toprenderer.DrawZooBuildingTopRenderer(ref Z_DragBuildManager.ThreadLoc, ref Z_DragBuildManager.ThreadScale);
        this.toprenderer.fAlpha = 0.5f;
        this.toprenderer.scale = FlashingAlpha.GetUpwardsPulseMedium();
        this.toprenderer.DrawZooBuildingTopRendererWithoutNight(AssetContainer.PointBlendBatch04);
      }
      this.footprint.DrawThingToBuildFootPrint(this.tilerenderer.vLocation, AssetContainer.pointspritebatch01);
      this.pathrenderer.DrawPathRendererWithFinder();
    }
  }
}
