// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell.SelectedAndSellManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Audio;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Layout.Graves;
using TinyZoo.PlayerDir.Layout.HoldingCells;
using TinyZoo.Tile_Data;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.OverWorld.OverworldSelectedThing.SellUI.SelectedAndSell
{
  internal class SelectedAndSellManager
  {
    private SellBar sellbar;
    private Vector2Int Location;
    private SelectionOutline selectionoutline;

    public SelectedAndSellManager(
      LayoutEntry layout,
      Vector2Int _Location,
      PrisonZone zone,
      HoldingCellInfo holdingcell,
      GraveYardBlockInfo graveyard,
      Player player)
    {
      this.Location = _Location;
      TileInfo tileInfo = TileData.GetTileInfo(layout.tiletype);
      int num1 = -1;
      if (layout != null && zone == null)
        num1 = layout.RotationClockWise;
      this.selectionoutline = new SelectionOutline(this.Location, tileInfo, zone, graveyard, layout.tiletype, num1);
      Vector2Int _CenterBottomTile = new Vector2Int(this.Location);
      _CenterBottomTile.X -= tileInfo.GetIntOrigin(num1).X;
      _CenterBottomTile.X += tileInfo.GetTileWidth(num1) / 2;
      _CenterBottomTile.Y += tileInfo.GetTileHeight(num1) - tileInfo.GetIntOrigin(num1).Y;
      float _ExtraX = 0.0f;
      if (tileInfo.GetTileWidth(num1) == 4)
        _ExtraX = 8f;
      --_CenterBottomTile.X;
      if (zone != null)
      {
        _CenterBottomTile = zone.GetBottomLeft();
        zone.GetBottomRight();
        if (zone.WidthAndHeight == null)
          zone.GetPlaceToDisplayCellInfoButton(out bool _);
        _CenterBottomTile.X += zone.WidthAndHeight.X / 2;
        ++_CenterBottomTile.Y;
        _ExtraX = zone.WidthAndHeight.X % 2 == 0 ? 8f : 16f;
      }
      else if (graveyard != null && layout.tiletype != TILETYPE.GraveYard_FloorGraveStone)
      {
        _CenterBottomTile = new Vector2Int(graveyard.TopLeft);
        _CenterBottomTile.X += graveyard.Size.X / 2;
        if (graveyard.Size.X % 2 == 0)
          _ExtraX = 8f;
        _CenterBottomTile.Y += graveyard.Size.Y + 1;
      }
      int num2 = 0;
      if (TileData.GetTileInfo(layout.tiletype) != null)
        num2 = TileData.GetTileInfo(layout.tiletype).GetTileWidth(layout.RotationClockWise);
      if (num2 % 2 == 0)
        _ExtraX = -16f;
      switch (TileData.GetTileInfo(layout.tiletype).buildingtype)
      {
        case BUILDINGTYPE.MoonPlant:
          this.selectionoutline = (SelectionOutline) null;
          break;
        case BUILDINGTYPE.PrisonWall:
          this.selectionoutline = (SelectionOutline) null;
          break;
        default:
          if (this.sellbar != null && this.sellbar.SelectedStructureInfoBar != null && !this.sellbar.SelectedStructureInfoBar.IsMouseOverButton(player) || (layout.tiletype == TILETYPE.WaterPumpStation || layout.tiletype == TILETYPE.Logo) && !player.Stats.TutorialsComplete[29] || !player.Stats.TutorialsComplete[29] && zone != null)
            break;
          int Earnings = 0;
          int PotentialEarnings = 0;
          if (zone != null)
          {
            int num3 = zone.IsFarm ? 1 : 0;
          }
          this.sellbar = new SellBar(layout.tiletype, _CenterBottomTile, _ExtraX, zone != null || holdingcell != null || (TileData.IsAStoreRoom(layout.tiletype) || TileData.IsAnArchitectOffice(layout.tiletype)) || TileData.IsAModifableBuilding(layout.tiletype), layout.tiletype == TILETYPE.Research_PrisonPlanet, layout.tiletype == TILETYPE.GraveYard_FloorGraveStone, graveyard != null && layout.tiletype != TILETYPE.GraveYard_FloorGraveStone, zone != null, Earnings, PotentialEarnings, player, zone, _Location);
          break;
      }
    }

    public BAR_TYPE GetCurrentBarType() => this.sellbar != null ? this.sellbar.GetCurrentBarType() : BAR_TYPE.Count;

    public bool IsMouseOverButton(Player player) => this.sellbar != null && this.sellbar.IsMouseOverButton(player);

    public bool IsSomethingSelected() => this.selectionoutline != null;

    public void TempLerpOff() => this.sellbar.TempLerpOff();

    public void LerpBackOn() => this.sellbar.LerpBackOn();

    public void UpdateTempExitLerp(float DeltaTime) => this.sellbar.UpdateTempExitLerp(DeltaTime);

    public bool UpdateSelectedAndSellManager(
      Player player,
      float DeltaTime,
      ref BuildingManageButton pressedthis,
      ref bool IsReanimate,
      ref bool IsExplainRevive)
    {
      if (FeatureFlags.DemolishEnabled)
      {
        if (this.selectionoutline != null)
          this.selectionoutline.UpdateSelectionOutline();
        if (this.sellbar != null)
        {
          bool ExitNow;
          if (this.sellbar.UpdateSellBar(player, DeltaTime, this.selectionoutline, ref pressedthis, out ExitNow) && !ExitNow)
          {
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
            IsReanimate = this.sellbar.IsReanimate;
            if (pressedthis == BuildingManageButton.Transfer)
              IsExplainRevive = this.sellbar.IsReanimate_SelectGraveYardMessage;
            return true;
          }
          if (ExitNow)
          {
            if (!OverWorldManager.zoopopupHolder.IsNull() && OverWorldManager.zoopopupHolder.ScrubOnExitMainBar())
              OverWorldManager.zoopopupHolder.SetNull();
            if (Z_GameFlags.DRAW_heatmaptype == HeatMapType.Water)
              Z_GameFlags.SetHeatMapType(HeatMapType.None);
            pressedthis = BuildingManageButton.ForceExit;
            return true;
          }
        }
      }
      return false;
    }

    public void DrawSelectedAndSellManager(Player player)
    {
      if (!FeatureFlags.DemolishEnabled)
        return;
      if (this.sellbar != null)
        this.sellbar.DrawSellBar(player);
      if (this.selectionoutline == null)
        return;
      this.selectionoutline.DrawSelectionOutline();
    }
  }
}
