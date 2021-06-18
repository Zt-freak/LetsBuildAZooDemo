// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.Z_BuildingIconPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Pen.BuildMenu;
using TinyZoo.Z_BuldMenu.CatSelector;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_BuldMenu.IconGrid;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.WaterBuild;
using TinyZoo.Z_ControllerLayouts;

namespace TinyZoo.Z_BuldMenu
{
  internal class Z_BuildingIconPanel
  {
    public BackButton closebutton;
    public Z_IconPanel z_iconpanel;
    private LerpHandler_Float lerper;
    public Vector2 Offset;
    internal static float MinHeight = 668f;
    private Z_CatSelect catselect;
    private int SelectedBuildingIndex;
    private bool HasDoneFirstSelect;
    private Controller_BuildMenu buildmenucontrolermatrix;
    private bool SkipSound;

    public Z_BuildingIconPanel(Player player)
    {
      this.buildmenucontrolermatrix = new Controller_BuildMenu();
      this.closebutton = new BackButton();
      this.z_iconpanel = new Z_IconPanel(CATEGORYTYPE.Enclosure, player, Z_BuildingIconPanel.MinHeight);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, 1f, 0.0f, 3f);
      this.catselect = new Z_CatSelect(player);
      this.closebutton.vLocation.Y = Z_BuildingIconPanel.MinHeight - 50f;
      this.SelectedBuildingIndex = -1;
      ObjectInfoPanel.z_penbuilder = (Z_PenBuilder) null;
      if (Z_GameFlags.ForceToNewScreen != ForceToNewScreen.MoveGate && Z_GameFlags.ForceToNewScreen != ForceToNewScreen.MovePen)
        return;
      this.buildmenucontrolermatrix = (Controller_BuildMenu) null;
      ObjectInfoPanel.z_penbuilder = new Z_PenBuilder(TileData.GetCellBlockToTileType((player.prisonlayout.GetThisCellBlock(Z_GameFlags.SelectedPrisonZoneUID) ?? player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID)).CellBLOCKTYPE), player, true);
    }

    internal static bool DisableDrag(Player player) => (double) player.player.touchinput.TouchStartLocation.Y > (double) Z_BuildingIconPanel.MinHeight;

    public TILETYPE GetSelectedTileType() => this.SelectedBuildingIndex == -1 ? TILETYPE.None : this.z_iconpanel.buildables[this.SelectedBuildingIndex];

    public bool BlocksThis(Vector2 TouchLoc) => (double) TouchLoc.Y > (double) Z_BuildingIconPanel.MinHeight;

    public void Exit()
    {
      this.catselect.Exit();
      this.lerper.SetLerp(true, 0.0f, 1f, 3f);
    }

    public bool CheckBackButtonOverLap(Vector2 Position) => this.closebutton.CheckCollision(Position);

    public void UpdateZ_BuildingIconPanel(
      Player player,
      float DeltaTime,
      out bool ExitPressed,
      WallsAndFloorsManager wallsandfloors)
    {
      if (this.z_iconpanel.UsingExternalIconPanel)
      {
        ExitPressed = false;
      }
      else
      {
        ExitPressed = false;
        this.Offset.Y = Z_BuildingIconPanel.MinHeight + this.lerper.Value * 200f;
        this.lerper.UpdateLerpHandler(DeltaTime);
        if (!this.HasDoneFirstSelect)
          this.HasDoneFirstSelect = true;
        if (this.buildmenucontrolermatrix != null)
          this.buildmenucontrolermatrix.UpdateController_BuildMenu(player, DeltaTime, this.catselect, this.z_iconpanel);
        CATEGORYTYPE category = this.catselect.UpdateZ_CatSelect(DeltaTime, player, this.z_iconpanel.ThisCategory);
        if (category != CATEGORYTYPE.Count && category != this.z_iconpanel.ThisCategory)
        {
          SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle);
          this.z_iconpanel = new Z_IconPanel(category, player, Z_BuildingIconPanel.MinHeight);
          this.SelectedBuildingIndex = -1;
          ObjectInfoPanel.z_dragbuilder = (Z_DragBuildManager) null;
          ObjectInfoPanel.z_penbuilder = (Z_PenBuilder) null;
          this.buildmenucontrolermatrix.SelectNewIconTab(BIconAndCost.PerRow, BIconAndCost.Total, 0);
          if (GameFlags.IsUsingController)
            this.SkipSound = true;
        }
        int SelectedThis = this.z_iconpanel.UpateBuildThisGrid(this.Offset, player, DeltaTime, Z_BuildingIconPanel.MinHeight);
        if (SelectedThis > -1 && SelectedThis != this.SelectedBuildingIndex)
        {
          if (!this.SkipSound)
            SoundEffectsManager.PlaySpecificSound(SoundEffectType.ClickSingle, 0.6f, 0.5f);
          else
            this.SkipSound = false;
          this.buildmenucontrolermatrix.SelectedBuildIcon(SelectedThis);
          this.SelectedBuildingIndex = SelectedThis;
          ObjectInfoPanel.z_objectinfomanager.ChangeItem(player, this.z_iconpanel.buildables[this.SelectedBuildingIndex], this.z_iconpanel.bicons[this.SelectedBuildingIndex].Locked);
          this.EnterBuildMode(this.SelectedBuildingIndex, player, wallsandfloors);
        }
        if (FeatureFlags.BlockCloseBuildMenu || !this.closebutton.UpdateBackButton(player, DeltaTime) && !FeatureFlags.ForceExitBuild || GameFlags.IsUsingController && ObjectInfoPanel.z_penbuilder != null && ObjectInfoPanel.z_penbuilder.BlockExitWithController())
          return;
        FeatureFlags.ForceExitBuild = false;
        ExitPressed = true;
        player.inputmap.ClearAllInput(player);
      }
    }

    public void ForceEnterBuildMode(
      Player player,
      WallsAndFloorsManager wallsandfloors,
      CATEGORYTYPE categorytype,
      int SelectedIndex,
      PrisonZone _decoratethisprisonzone,
      bool _CameFromMainBarManager)
    {
      this.z_iconpanel = new Z_IconPanel(categorytype, player, Z_BuildingIconPanel.MinHeight, _CameFromMainBarManager);
      this.EnterBuildMode(SelectedIndex, player, wallsandfloors, _decoratethisprisonzone, _CameFromMainBarManager);
    }

    private void EnterBuildMode(
      int Selected,
      Player player,
      WallsAndFloorsManager wallsandfloors,
      PrisonZone _decoratethisprisonzone = null,
      bool _CameFromMainBarManager = false)
    {
      this.SelectedBuildingIndex = Selected;
      ObjectInfoPanel.z_dragbuilder = (Z_DragBuildManager) null;
      ObjectInfoPanel.z_penbuilder = (Z_PenBuilder) null;
      TILETYPE tiletype = BuildMenuBarManager.barmanager.BuildingOnfoButtons[this.SelectedBuildingIndex].tiletype;
      if (BuildMenuBarManager.barmanager.BuildingOnfoButtons[this.SelectedBuildingIndex].Disabled)
        return;
      if (this.z_iconpanel.ThisCategory != CATEGORYTYPE.Enclosure)
      {
        if (TileData.IsThisFloorAVolume(tiletype))
        {
          ObjectInfoPanel.z_dragbuilder = (Z_DragBuildManager) null;
          ObjectInfoPanel.volumebuilder = new VolumeBuilderManager(tiletype);
        }
        else
        {
          ObjectInfoPanel.volumebuilder = (VolumeBuilderManager) null;
          ObjectInfoPanel.z_dragbuilder = new Z_DragBuildManager(tiletype, player, this.z_iconpanel.ThisCategory == CATEGORYTYPE.Floors, this.z_iconpanel.ThisCategory == CATEGORYTYPE.Floors, _decoratethisprisonzone: _decoratethisprisonzone, _CameFromMainBarManager: _CameFromMainBarManager, _IsPenWater: (this.z_iconpanel.ThisCategory == CATEGORYTYPE.Pen_Water));
          ObjectInfoPanel.z_dragbuilder.SetLocation(player, wallsandfloors, true);
        }
      }
      else
        ObjectInfoPanel.z_penbuilder = new Z_PenBuilder(tiletype, player, false);
    }

    public void DrawZ_BuildingIconPanelFrame()
    {
      this.z_iconpanel.DrawBuildThisGrid(this.Offset, this.SelectedBuildingIndex, Z_BuildingIconPanel.MinHeight, true);
      if (FeatureFlags.BlockCloseBuildMenu)
        return;
      this.closebutton.DrawBackButton(Vector2.Zero);
    }

    public void DrawZ_BuildingIconPanel()
    {
      if (this.z_iconpanel.UsingExternalIconPanel)
        return;
      this.z_iconpanel.DrawBuildThisGrid(this.Offset, this.SelectedBuildingIndex, Z_BuildingIconPanel.MinHeight);
      this.catselect.DrawZ_CatSelect(this.z_iconpanel.ThisCategory);
      if (FeatureFlags.BlockCloseBuildMenu)
        return;
      this.closebutton.DrawBackButton(Vector2.Zero);
    }
  }
}
