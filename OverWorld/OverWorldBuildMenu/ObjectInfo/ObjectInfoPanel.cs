// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo.ObjectInfoPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System;
using TinyZoo.Audio;
using TinyZoo.GenericUI;
using TinyZoo.GenericUI.UXPanels;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.PeopleAndBeams;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BarMenu.Pen.BuildMenu;
using TinyZoo.Z_BuldMenu.DragBuilder;
using TinyZoo.Z_BuldMenu.ObjectInfo;
using TinyZoo.Z_BuldMenu.PenBuilder;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_BuldMenu.WaterBuild;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.ObjectInfo
{
  internal class ObjectInfoPanel
  {
    private TILETYPE selectedtiletype;
    private Vector2 Location;
    private SimpleTextHandler description;
    private SimpleTextHandler title;
    private SimpleTextHandler Cost;
    private DollarPanel dollars;
    private ConsumptionFrame consumption;
    private ConsumptionFrame generation;
    private TextButton Build;
    private LerpHandler_Float lerper;
    private bool IsLocked;
    private Vector2 VSCALE;
    private GameObjectNineSlice box;
    internal static Z_ObjectInfoManager z_objectinfomanager;
    internal static Z_DragBuildManager z_dragbuilder;
    internal static VolumeBuilderManager volumebuilder;
    internal static Z_PenBuilder_Square z_penbuilder_oldsquare;
    internal static Z_PenBuilder z_penbuilder;
    private int LastTileCount;

    public ObjectInfoPanel(
      TILETYPE tiletype,
      CATEGORYTYPE category,
      bool _IsLocked,
      Player player)
    {
      if (category == CATEGORYTYPE.Pen_Water)
        OverWorldManager.heatmapmanager.DoubleCheckWaterSetUp(player);
      Console.WriteLine("Removed Line For Drag Build Just Called");
      if (!DebugFlags.IsPCVersion)
        return;
      ObjectInfoPanel.z_objectinfomanager = new Z_ObjectInfoManager();
    }

    public void Reactivate()
    {
      if (DebugFlags.IsPCVersion)
        ObjectInfoPanel.z_objectinfomanager.Reactivate();
      else
        this.lerper.SetLerp(true, 1f, 0.0f, 3f, true);
    }

    public void Exit()
    {
      if (ObjectInfoPanel.z_penbuilder != null)
        ObjectInfoPanel.z_penbuilder.CleanUpOnCancel();
      if (this.lerper != null)
        this.lerper.SetLerp(false, 0.0f, 1f, 3f);
      if (!DebugFlags.IsPCVersion)
        return;
      ObjectInfoPanel.z_objectinfomanager.Exit();
    }

    public bool UpdateObjectInfoPanel(
      Vector2 Offset,
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      CATEGORYTYPE category,
      bool BlockBuildingDrawPreview,
      AnimalsInPens peopleandbeams,
      out bool SwitchToGatePlacement,
      out bool TryToExit,
      out bool ForceExitFromGateMove,
      OverWorldEnvironmentManager overworldenvironment)
    {
      ForceExitFromGateMove = false;
      TryToExit = false;
      SwitchToGatePlacement = false;
      if (DebugFlags.IsPCVersion)
      {
        ObjectInfoPanel.z_objectinfomanager.UpdateZ_ObjectInfoManager(DeltaTime, player, wallsandfloors);
        if (ObjectInfoPanel.z_dragbuilder != null || ObjectInfoPanel.volumebuilder != null)
        {
          if (category == CATEGORYTYPE.Enclosure)
          {
            ObjectInfoPanel.volumebuilder = (VolumeBuilderManager) null;
            ObjectInfoPanel.z_dragbuilder = (Z_DragBuildManager) null;
          }
          else if (!BlockBuildingDrawPreview)
          {
            if (ObjectInfoPanel.volumebuilder != null)
            {
              ObjectInfoPanel.volumebuilder.UpdateVolumeBuilder(player, DeltaTime, wallsandfloors, overworldenvironment);
            }
            else
            {
              bool ExitBackToMainBarManager;
              ObjectInfoPanel.z_dragbuilder.UpdateZ_DragBuildManager(player, wallsandfloors, out bool _, DeltaTime, out ExitBackToMainBarManager, Z_GameFlags.MouseIsOverAPanel, out bool _);
              if (ExitBackToMainBarManager)
                TryToExit = true;
            }
          }
        }
        else if (ObjectInfoPanel.z_penbuilder != null)
        {
          if (category != CATEGORYTYPE.Enclosure)
          {
            ObjectInfoPanel.z_penbuilder = (Z_PenBuilder) null;
          }
          else
          {
            bool CanAfford = true;
            SimpleTextHandler cost = this.Cost;
            int CurrentTileCount;
            if (ObjectInfoPanel.z_penbuilder.UpdateZ_PenBuilder(player, DeltaTime, wallsandfloors, out CurrentTileCount, peopleandbeams, out SwitchToGatePlacement, out TryToExit, out ForceExitFromGateMove, CanAfford, out bool _))
              ObjectInfoPanel.z_penbuilder = (Z_PenBuilder) null;
            if (this.Cost != null && this.LastTileCount != CurrentTileCount)
            {
              this.LastTileCount = CurrentTileCount;
              ObjectInfoPanel.z_objectinfomanager.ChangeItem(player, this.selectedtiletype, false, this.LastTileCount);
            }
          }
        }
        else if (ObjectInfoPanel.z_penbuilder_oldsquare != null)
        {
          if (category != CATEGORYTYPE.Enclosure)
            ObjectInfoPanel.z_penbuilder_oldsquare = (Z_PenBuilder_Square) null;
          else
            ObjectInfoPanel.z_penbuilder_oldsquare.UpdaeteZ_PenBuilderSquare(player, DeltaTime, wallsandfloors);
        }
        return false;
      }
      this.lerper.UpdateLerpHandler(DeltaTime);
      if (this.dollars != null)
        this.dollars.UpdateDollarPanel(DeltaTime, player);
      this.description.UpdateSimpleTextHandler(DeltaTime);
      if (this.Cost == null || !this.Build.UpdateTextButton(player, Offset + this.Location, DeltaTime) && (!(Offset == Vector2.Zero) || !player.inputmap.PressedThisFrame[0]))
        return false;
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.ConfirmClick);
      player.player.touchinput.ReleaseTapArray[0].X = -100000f;
      return true;
    }

    public bool GetSelectedThingIsLocked() => this.IsLocked;

    public bool BlocksThis(Vector2 TouchLocation) => (double) TouchLocation.Y > 568.0;

    public int GetPenUsableSpace() => ObjectInfoPanel.z_dragbuilder != null ? ObjectInfoPanel.z_penbuilder.GetPenUsableSpace() : -1;

    public void DrawObjectInfoPanel(
      Vector2 Offset,
      TileRenderer[,] tilesasarray,
      bool BlockBuildingDrawPreview,
      Player player)
    {
      if (DebugFlags.IsPCVersion)
      {
        int TotalPenSpace = -1;
        bool flag = false;
        if (ObjectInfoPanel.z_dragbuilder != null)
        {
          ObjectInfoPanel.z_dragbuilder.DrawZ_DragBuildManager(BlockBuildingDrawPreview);
          flag = ObjectInfoPanel.z_dragbuilder.CameFromMainBarManager;
        }
        else if (ObjectInfoPanel.volumebuilder != null)
          ObjectInfoPanel.volumebuilder.DrawVolumeBuilder();
        else if (ObjectInfoPanel.z_penbuilder != null)
        {
          ObjectInfoPanel.z_penbuilder.DrawZ_PenBuilder(tilesasarray);
          TotalPenSpace = ObjectInfoPanel.z_penbuilder.GetPenUsableSpace();
        }
        else if (ObjectInfoPanel.z_penbuilder_oldsquare != null)
          ObjectInfoPanel.z_penbuilder_oldsquare.DrawZ_PenBuilderOldSquare(tilesasarray);
        ObjectInfoPanel.z_objectinfomanager.DrawZ_ObjectInfoManager(TotalPenSpace);
        if (!flag)
          return;
        BuildMenuBarManager.DrawShitStaticFunction(player);
      }
      else
      {
        Offset.Y += this.lerper.Value * 400f;
        this.box.DrawGameObjectNineSlice(AssetContainer.pointspritebatch03, AssetContainer.SpriteSheet, Offset + this.Location, this.VSCALE);
        this.title.DrawSimpleTextHandler(Offset + this.Location + new Vector2(-360f, -85f * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
        SimpleTextHandler cost = this.Cost;
        if (this.dollars != null)
          this.dollars.DrawDollarPanel(Offset + this.Location + new Vector2(-495f, -95f * Sengine.ScreenRatioUpwardsMultiplier.Y));
        float num = -50f;
        this.description.DrawSimpleTextHandler(Offset + this.Location + new Vector2(-360f, num * Sengine.ScreenRatioUpwardsMultiplier.Y), 1f, AssetContainer.pointspritebatchTop05);
        if (this.Cost == null)
          return;
        if (this.generation != null)
          this.generation.DrawConsumptionFrame(Offset + this.Location + new Vector2(-100f, 70f));
        if (this.consumption != null)
          this.consumption.DrawConsumptionFrame(Offset + this.Location + new Vector2(-300f, 70f));
        this.Build.vLocation = new Vector2(200f, 50f);
        this.Build.DrawTextButton(Offset + this.Location, 1f, AssetContainer.pointspritebatchTop05);
      }
    }
  }
}
