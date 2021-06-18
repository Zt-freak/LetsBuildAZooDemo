// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BarMenu.Pen.BuildMenu.BuildMenuBarManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_ManagePen.Enrichment;
using TinyZoo.Z_PenInfo;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BarMenu.Pen.BuildMenu
{
  internal class BuildMenuBarManager
  {
    internal static MainBarManager barmanager;
    private static List<TILETYPE> Buildings;
    private static CATEGORYTYPE CatType;
    private static EnrichmentInfoManager EnrichmentInfoPanelManager;
    internal static bool BuiltSomethingThisFrame;
    private static OverworldBuildManager buildmanagerPOINTER_HACK;

    public BuildMenuBarManager(Player player, CATEGORYTYPE category, float BaseScale)
    {
      BuildMenuBarManager.CatType = category;
      BuildMenuBarManager.barmanager = new MainBarManager(BAR_TYPE.Pen_ViewItems, TILETYPE.None, false, player);
      BuildMenuBarManager.Buildings = CategoryData.GetEntriesInThisCategory(category);
      BuildMenuBarManager.barmanager.SetUpBuildings(BuildMenuBarManager.Buildings, player, BuildMenuBarManager.CatType, BaseScale);
      BuildMenuBarManager.TryConstructEnrichmentInfoPopup(player);
    }

    private static void TryConstructEnrichmentInfoPopup(Player player)
    {
      if (BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Enrichment || BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Deco || (BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Water || BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Shelter))
      {
        BuildMenuBarManager.EnrichmentInfoPanelManager = new EnrichmentInfoManager(player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID));
        BuildMenuBarManager.EnrichmentInfoPanelManager.location = new Vector2(994f, 600f);
      }
      else
        BuildMenuBarManager.EnrichmentInfoPanelManager = (EnrichmentInfoManager) null;
    }

    internal static bool UpdateBuildMenuBarManagerFromExternal(
      Player player,
      float DeltaTime,
      out bool MouseOverlappingSomething,
      WallsAndFloorsManager wallsandfloors,
      PrisonZone prisonzone)
    {
      MouseOverlappingSomething = BuildMenuBarManager.barmanager != null && BuildMenuBarManager.barmanager.CheckMouseOver(player);
      bool GoBack = false;
      BuildingManageButton buildingManageButton = BuildingManageButton.Count;
      if (BuildMenuBarManager.barmanager != null)
        buildingManageButton = BuildMenuBarManager.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
      {
        PenInfoManager.RemakeThisBarNow = true;
        return true;
      }
      if (buildingManageButton != BuildingManageButton.Count && !MainBarManager.IsThisBuildingDisabled(BuildMenuBarManager.barmanager.GetTileType(), player))
      {
        ObjectInfoPanel.z_dragbuilder = new Z_DragBuildManager(BuildMenuBarManager.barmanager.GetTileType(), player, false, _decoratethisprisonzone: prisonzone, _CameFromMainBarManager: true, _IsPenWater: (BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Water));
        ObjectInfoPanel.z_dragbuilder.SetLocation(player, wallsandfloors, true);
        OverWorldManager.overworldstate = OverWOrldState.Build;
        BuildMenuBarManager.buildmanagerPOINTER_HACK.ForceEnterBuildMode(BuildMenuBarManager.barmanager.GetTileType(), player, wallsandfloors, BuildMenuBarManager.CatType, prisonzone, BuildMenuBarManager.barmanager.GetBuildingIndex());
      }
      if (buildingManageButton != BuildingManageButton.Count && BuildMenuBarManager.barmanager.GetTileType() != TILETYPE.Count)
      {
        if (BuildMenuBarManager.EnrichmentInfoPanelManager == null)
          BuildMenuBarManager.TryConstructEnrichmentInfoPopup(player);
        if (BuildMenuBarManager.EnrichmentInfoPanelManager != null)
          BuildMenuBarManager.EnrichmentInfoPanelManager.SetNewItem(BuildMenuBarManager.barmanager.GetTileType(), player);
      }
      if (BuildMenuBarManager.EnrichmentInfoPanelManager != null)
      {
        if (BuildMenuBarManager.BuiltSomethingThisFrame)
          BuildMenuBarManager.EnrichmentInfoPanelManager.RefreshBar();
        if (BuildMenuBarManager.EnrichmentInfoPanelManager.UpdateEnrichmentInfoManager(player, DeltaTime, Vector2.Zero))
          BuildMenuBarManager.EnrichmentInfoPanelManager = (EnrichmentInfoManager) null;
      }
      BuildMenuBarManager.BuiltSomethingThisFrame = false;
      return false;
    }

    public void ShrinkLerp(TILETYPE SelectedTile, bool IsShrink)
    {
      if (IsShrink)
        BuildMenuBarManager.barmanager.Shrink(SelectedTile);
      else
        BuildMenuBarManager.barmanager.UnShrink();
    }

    public bool UpdateBuildMenuBarManager(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager,
      PrisonZone prisonzone,
      out TILETYPE SelectedThisToBuild)
    {
      SelectedThisToBuild = TILETYPE.Count;
      if (player.inputmap.PressedBackOnController())
      {
        OverWorldManager.overworldstate = OverWOrldState.MainMenu;
        return true;
      }
      bool GoBack;
      BuildingManageButton buildingManageButton = BuildMenuBarManager.barmanager.UpdateMainBarManager(DeltaTime, player, out GoBack);
      if (GoBack)
        return true;
      if (buildingManageButton != BuildingManageButton.Count)
      {
        SelectedThisToBuild = BuildMenuBarManager.barmanager.GetTileType();
        if (Z_DebugFlags.TempNewBuildingMenu)
          return false;
        if (!MainBarManager.IsThisBuildingDisabled(BuildMenuBarManager.barmanager.GetTileType(), player))
        {
          BuildMenuBarManager.buildmanagerPOINTER_HACK = buildmanager;
          ObjectInfoPanel.z_dragbuilder = new Z_DragBuildManager(BuildMenuBarManager.barmanager.GetTileType(), player, false, _decoratethisprisonzone: prisonzone, _CameFromMainBarManager: true, _IsPenWater: (BuildMenuBarManager.CatType == CATEGORYTYPE.Pen_Water));
          ObjectInfoPanel.z_dragbuilder.SetLocation(player, wallsandfloors, true);
          OverWorldManager.overworldstate = OverWOrldState.Build;
          buildmanager.ForceEnterBuildMode(BuildMenuBarManager.barmanager.GetTileType(), player, wallsandfloors, BuildMenuBarManager.CatType, prisonzone, BuildMenuBarManager.barmanager.GetBuildingIndex());
        }
      }
      if (buildingManageButton != BuildingManageButton.Count && BuildMenuBarManager.barmanager.GetTileType() != TILETYPE.Count)
      {
        if (BuildMenuBarManager.EnrichmentInfoPanelManager == null)
          BuildMenuBarManager.TryConstructEnrichmentInfoPopup(player);
        if (BuildMenuBarManager.EnrichmentInfoPanelManager != null)
          BuildMenuBarManager.EnrichmentInfoPanelManager.SetNewItem(BuildMenuBarManager.barmanager.GetTileType(), player);
      }
      if (BuildMenuBarManager.EnrichmentInfoPanelManager != null && BuildMenuBarManager.EnrichmentInfoPanelManager.UpdateEnrichmentInfoManager(player, DeltaTime, Vector2.Zero))
        BuildMenuBarManager.EnrichmentInfoPanelManager = (EnrichmentInfoManager) null;
      return false;
    }

    internal static void DrawShitStaticFunction(Player player)
    {
      BuildMenuBarManager.barmanager.DrawMainBarManager(player);
      if (BuildMenuBarManager.EnrichmentInfoPanelManager == null)
        return;
      BuildMenuBarManager.EnrichmentInfoPanelManager.DrawEnrichmentInfoManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }

    public bool CheckMouseOver(Player player) => BuildMenuBarManager.barmanager.CheckMouseOver(player) || BuildMenuBarManager.EnrichmentInfoPanelManager != null && BuildMenuBarManager.EnrichmentInfoPanelManager.CheckMouseOver(player, Vector2.Zero);

    public Vector2 GetShrinkOffset(out float ShrinkValue) => BuildMenuBarManager.barmanager.GetShrinkOffset(out ShrinkValue);

    public void DrawAddDecoManager(Player player)
    {
      BuildMenuBarManager.barmanager.DrawMainBarManager(player);
      if (BuildMenuBarManager.EnrichmentInfoPanelManager == null)
        return;
      BuildMenuBarManager.EnrichmentInfoPanelManager.DrawEnrichmentInfoManager(Vector2.Zero, AssetContainer.pointspritebatchTop05);
    }
  }
}
