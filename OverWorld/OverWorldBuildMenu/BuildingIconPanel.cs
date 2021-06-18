// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildingIconPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection;
using TinyZoo.OverWorld.OverWorldBuildMenu.CategorySelection.CatHeading;
using TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.OverWorld.OverWorldRenderer;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Build;
using TinyZoo.Z_BuldMenu;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.WaterBuild;
using TinyZoo.Z_BuldMenu.Z_NewCostInfo;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.OverWorld.OverWorldBuildMenu
{
  internal class BuildingIconPanel
  {
    public Z_BuildingIconPanel zbuildingiconpanel;
    public bool BlockDraw;
    private CatHeadingManager catheading;
    private BuildThisGrid buildinggrid;
    private GameObjectNineSlice Framee;
    private Vector2 Location;
    private LerpHandler_Float lerper;
    private bool Exiting;
    private BackButton backbuttob;
    public int SelectedIndex = -1;
    private Vector2 VSCALE;
    private VolumeBuilderManager volumebuilder;
    private Z_DragBuildManager newdragbuilder;
    private Z_PenBuilder z_penbuilder;
    private SimpleTextHandler PlaceGateDescription;
    private Vector2 EX = Vector2.Zero;
    private Vector2 NotchPosOffset = Vector2.Zero;
    private Build_BarManager buildbar;
    private CostInfoPanel costinfopanel;
    private bool MouseWasBlocked;

    public BuildingIconPanel(Player player)
    {
      this.MouseWasBlocked = true;
      this.backbuttob = new BackButton(true);
      this.lerper = new LerpHandler_Float();
      this.lerper.SetLerp(true, -1f, 0.0f, 3f);
      if (DebugFlags.IsPCVersion)
      {
        if (Z_DebugFlags.TempNewBuildingMenu)
        {
          this.buildbar = new Build_BarManager(player);
          if (!OverWorldManager.zoopopupHolder.IsNull() && OverWorldManager.zoopopupHolder.ScrubOnOpenBuildMenu(player))
            OverWorldManager.zoopopupHolder.SetNull();
        }
        this.zbuildingiconpanel = new Z_BuildingIconPanel(player);
      }
      else
      {
        this.catheading = new CatHeadingManager();
        this.VSCALE = new Vector2(256f, 600f);
        this.Framee = new GameObjectNineSlice(new Rectangle(877, 350, 21, 21), 7);
        this.Location = new Vector2(128f, 468f);
        this.buildinggrid = new BuildThisGrid(this.catheading.category, player);
        this.SelectedIndex = -1;
        if (!GameFlags.HasNotch)
          return;
        this.EX = new Vector2(8f, 8f);
        this.NotchPosOffset = new Vector2(-4f, 4f);
      }
    }

    public bool AllowClickOut() => this.newdragbuilder == null && this.volumebuilder == null && (this.z_penbuilder == null && ObjectInfoPanel.z_dragbuilder == null) && ObjectInfoPanel.z_penbuilder == null;

    public TILETYPE GetTileByIndex(int SelectedIndex) => DebugFlags.IsPCVersion ? this.zbuildingiconpanel.GetSelectedTileType() : this.buildinggrid.buildables[SelectedIndex];

    public CATEGORYTYPE GetSelectedCategory() => DebugFlags.IsPCVersion ? this.zbuildingiconpanel.z_iconpanel.ThisCategory : this.catheading.category;

    public bool IsThisLocked(int Index) => this.zbuildingiconpanel != null ? this.zbuildingiconpanel.z_iconpanel.IsThisLocked(Index) : this.buildinggrid.IsThisLocked(Index);

    public bool BlocksThis(Vector2 TouchLocation) => this.zbuildingiconpanel != null ? this.zbuildingiconpanel.BlocksThis(TouchLocation) : (double) TouchLocation.X < 256.0;

    public void ReActivate() => this.lerper.SetLerp(true, -1f, 0.0f, 3f, true);

    public void SelectThis(int _SelectThis) => this.SelectedIndex = _SelectThis;

    public bool CheckReleaseTapWithBack(Player player)
    {
      Vector2 releaseTap = player.player.touchinput.ReleaseTapArray[0];
      if (this.backbuttob.UpdateBackButton(player, 0.0f))
      {
        player.player.touchinput.ReleaseTapArray[0] = releaseTap;
        return true;
      }
      return this.zbuildingiconpanel != null && this.zbuildingiconpanel.CheckBackButtonOverLap(releaseTap);
    }

    public bool UpdateBuildingIconPanel(
      float DeltaTime,
      Player player,
      ref BuildMatrix buildmatrix,
      out bool StartedExit,
      ref bool skipGridSound,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager_fornewmenu)
    {
      StartedExit = false;
      return this.UpdatePCVersion(player, DeltaTime, ref StartedExit, wallsandfloors, buildmanager_fornewmenu);
    }

    public bool CheckMouseOver(Player player)
    {
      if (this.costinfopanel != null && this.costinfopanel.CheckMouseOver(player))
      {
        this.MouseWasBlocked = true;
        return true;
      }
      if (this.buildbar == null || !this.buildbar.CheckMouseOver(player))
        return false;
      this.MouseWasBlocked = true;
      return true;
    }

    public bool UpdatePCVersion(
      Player player,
      float DeltaTime,
      ref bool StartedExit,
      WallsAndFloorsManager wallsandfloors,
      OverworldBuildManager buildmanager_fornewmenu)
    {
      if (Z_DebugFlags.TempNewBuildingMenu)
      {
        if (this.volumebuilder != null)
          this.volumebuilder.UpdateVolumeBuilder(player, DeltaTime, OverWorldManager.overworldenvironment.wallsandfloors, OverWorldManager.overworldenvironment);
        else if (this.newdragbuilder != null)
        {
          bool TriedToBuyButCouldNotAfford = false;
          this.newdragbuilder.UpdateZ_DragBuildManager(player, wallsandfloors, out bool _, DeltaTime, out bool _, Z_GameFlags.MouseIsOverAPanel, out TriedToBuyButCouldNotAfford);
          if (TriedToBuyButCouldNotAfford && this.costinfopanel != null)
            this.costinfopanel.FlashRedCantAfford();
        }
        else if (this.z_penbuilder != null)
        {
          bool CanAfford = true;
          if (this.costinfopanel != null)
            CanAfford = this.costinfopanel.CanAfford;
          bool TriedToBuyButCouldNotAfford = false;
          if (this.z_penbuilder.UpdateZ_PenBuilder(player, DeltaTime, wallsandfloors, out int _, OverWorldManager.overworldenvironment.animalsinpens, out bool _, out bool _, out bool _, CanAfford, out TriedToBuyButCouldNotAfford))
          {
            if (!player.Stats.TutorialsComplete[29] && this.z_penbuilder.DidBuild)
              return true;
            this.z_penbuilder = (Z_PenBuilder) null;
            this.buildbar.ShrinkLerp(TILETYPE.None, false);
            this.costinfopanel = (CostInfoPanel) null;
          }
          else if (TriedToBuyButCouldNotAfford && this.costinfopanel != null)
            this.costinfopanel.FlashRedCantAfford();
        }
        if (this.costinfopanel != null && this.costinfopanel.UpdateCostInfoPanel(player, DeltaTime, this.z_penbuilder))
        {
          this.volumebuilder = (VolumeBuilderManager) null;
          this.costinfopanel = (CostInfoPanel) null;
          this.buildbar.ShrinkLerp(TILETYPE.Count, false);
          if (this.z_penbuilder != null)
            this.z_penbuilder.CleanUpOnCancel();
          this.z_penbuilder = (Z_PenBuilder) null;
          this.newdragbuilder = (Z_DragBuildManager) null;
        }
        this.MouseWasBlocked = false;
        TILETYPE TiedToBuildThis = TILETYPE.Count;
        bool ChangedCategory = false;
        bool flag1 = true;
        if (this.buildbar != null)
          flag1 = this.buildbar.UpdateBuild_BarManager(player, DeltaTime, wallsandfloors, buildmanager_fornewmenu, out TiedToBuildThis, out ChangedCategory);
        if (flag1)
        {
          if (this.z_penbuilder != null)
            this.z_penbuilder.CleanUpOnCancel();
          return true;
        }
        if (ChangedCategory)
          this.costinfopanel = (CostInfoPanel) null;
        else if ((this.costinfopanel == null || this.costinfopanel.tiletype != TiedToBuildThis) && TiedToBuildThis != TILETYPE.Count)
        {
          bool CanPlayerBuildThis = !MainBarManager.IsThisBuildingDisabled(TiedToBuildThis, player);
          if (CanPlayerBuildThis)
          {
            bool flag2 = false;
            if (this.buildbar.selectedCategory == CATEGORYTYPE.Farm && TiedToBuildThis == TILETYPE.EmptySoilPatch)
            {
              flag2 = true;
              TiedToBuildThis = TILETYPE.FieldPicketFenceEnclosure;
            }
            if (this.buildbar.selectedCategory == CATEGORYTYPE.Enclosure | flag2)
            {
              if (this.z_penbuilder != null)
                this.z_penbuilder.CleanUpOnCancel();
              this.z_penbuilder = new Z_PenBuilder(TiedToBuildThis, player, false);
              this.volumebuilder = (VolumeBuilderManager) null;
              this.newdragbuilder = (Z_DragBuildManager) null;
            }
            else if (TileData.IsThisFloorAVolume(TiedToBuildThis))
            {
              this.volumebuilder = new VolumeBuilderManager(TiedToBuildThis);
            }
            else
            {
              this.newdragbuilder = new Z_DragBuildManager(TiedToBuildThis, player, this.buildbar.selectedCategory == CATEGORYTYPE.Floors || TiedToBuildThis == TILETYPE.Volume_Water, this.buildbar.selectedCategory == CATEGORYTYPE.Floors || TiedToBuildThis == TILETYPE.Volume_Water, _IsPenWater: (this.buildbar.selectedCategory == CATEGORYTYPE.Water));
              this.volumebuilder = (VolumeBuilderManager) null;
            }
          }
          else
          {
            if (this.z_penbuilder != null)
              this.z_penbuilder.CleanUpOnCancel();
            this.z_penbuilder = (Z_PenBuilder) null;
          }
          this.buildbar.ShrinkLerp(TiedToBuildThis, true);
          this.costinfopanel = new CostInfoPanel(this.buildbar.selectedCategory, TiedToBuildThis, player, CanPlayerBuildThis);
        }
        return false;
      }
      bool ExitPressed;
      this.zbuildingiconpanel.UpdateZ_BuildingIconPanel(player, DeltaTime, out ExitPressed, wallsandfloors);
      this.BlockDraw = this.zbuildingiconpanel.closebutton.MouseOver;
      if (this.CheckBackButton(player, DeltaTime) | ExitPressed)
        this.TryToExit(ref StartedExit);
      this.lerper.UpdateLerpHandler(DeltaTime);
      return this.Exiting && (double) this.lerper.Value == -1.0;
    }

    private bool CheckBackButton(Player player, float DeltaTime)
    {
      if (player.inputmap.PressedBackOnController() && !FeatureFlags.BlockCloseBuildMenu && !this.Exiting)
      {
        this.Exiting = true;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
        this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
      }
      bool flag = false;
      if (!FeatureFlags.BlockCloseBuildMenu)
        flag = this.backbuttob.UpdateBackButton(player, DeltaTime);
      return flag;
    }

    public void SetToGatePlace()
    {
      this.PlaceGateDescription = new SimpleTextHandler(SEngine.Localization.Localization.GetText(747), true, 0.6f, 2f * Z_GameFlags.GetBaseScaleForUI(), true);
      this.PlaceGateDescription.AutoCompleteParagraph();
    }

    public void SetToPenMove()
    {
      this.PlaceGateDescription = new SimpleTextHandler("Choose pen location", true, 0.6f, 2f * Z_GameFlags.GetBaseScaleForUI(), true);
      this.PlaceGateDescription.AutoCompleteParagraph();
    }

    public void TryToExit(ref bool StartedExit)
    {
      if (this.Exiting)
        return;
      if (this.zbuildingiconpanel != null)
        this.zbuildingiconpanel.Exit();
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.BackClick);
      StartedExit = true;
      this.Exiting = true;
      this.lerper.SetLerp(false, 0.0f, -1f, 3f, true);
    }

    public void DrawBuildingIconPanel(Player player)
    {
      if (Z_DebugFlags.TempNewBuildingMenu)
      {
        if (this.volumebuilder != null)
          this.volumebuilder.DrawVolumeBuilder();
        if (this.newdragbuilder != null)
          this.newdragbuilder.DrawZ_DragBuildManager(false);
        else if (this.z_penbuilder != null)
          this.z_penbuilder.DrawZ_PenBuilder(OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray);
      }
      if (this.costinfopanel != null)
        this.costinfopanel.DrawCostInfoPanel();
      if (this.zbuildingiconpanel != null)
      {
        if (Z_DebugFlags.TempNewBuildingMenu)
          this.buildbar.DrawBuild_BarManager(player);
        else if (this.PlaceGateDescription != null)
        {
          this.zbuildingiconpanel.DrawZ_BuildingIconPanelFrame();
          this.PlaceGateDescription.DrawSimpleTextHandler(this.zbuildingiconpanel.Offset + this.Location + new Vector2(512f, 30f));
        }
        else
          this.zbuildingiconpanel.DrawZ_BuildingIconPanel();
      }
      else
      {
        Vector2 Offset = this.Location + new Vector2(this.lerper.Value * 512f, 0.0f);
        this.catheading.PreDrawCatHeadingManager(Offset);
        this.Framee.scale = 3f;
        this.Framee.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + this.NotchPosOffset, this.VSCALE + this.EX);
        this.catheading.DrawCatHeadingManager(Offset);
        this.buildinggrid.DrawBuildThisGrid(Offset + new Vector2(0.0f, -200f), this.SelectedIndex);
        if (FeatureFlags.BlockCloseBuildMenu)
          return;
        this.backbuttob.DrawBackButton(new Vector2(this.lerper.Value * 512f, 0.0f));
      }
    }
  }
}
