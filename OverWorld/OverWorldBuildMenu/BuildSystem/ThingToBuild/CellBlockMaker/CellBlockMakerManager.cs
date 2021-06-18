// Decompiled with JetBrains decompiler
// Type: TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker.CellBlockMakerManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using System;
using TinyZoo.Audio;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.BuildThisInfo;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker
{
  internal class CellBlockMakerManager
  {
    private DragZone dragzone;
    private BuildUI buildUI;
    private int Volume;
    private DragZoneCamera dragzonecamera;
    private TILETYPE tletype;
    private bool Blocked;
    private float Timer;

    public CellBlockMakerManager(TILETYPE _tletype, Player player)
    {
      this.Blocked = true;
      this.tletype = _tletype;
      this.dragzone = new DragZone(player);
      FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = true;
      this.buildUI = new BuildUI(this.tletype, player);
      this.dragzonecamera = new DragZoneCamera();
    }

    public bool GetIsBlocked() => this.Blocked;

    public int GetVolume() => this.dragzone.GetVolume();

    public void UpdateCellBlockMakerManager(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors)
    {
      this.Blocked = this.dragzone.GetIsBlocked(wallsandfloors.tilesasarray);
      if (this.buildUI != null && this.buildUI.UpdateBuildUI(DeltaTime, player, true))
      {
        if (this.dragzone.GetIsBlocked(wallsandfloors.tilesasarray) || DebugFlags.IsPCVersion || !player.Stats.SpendCash(player.livestats.GetCost(this.tletype, player, true) * this.dragzone.GetVolume(), SpendingCashOnThis.BuyAnimalPen, player))
          return;
        SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
        player.prisonlayout.AddNewCellBlock(this.dragzone.GetTopLeft().X, this.dragzone.GetTopLeft().Y, this.dragzone.GetWidth(), this.dragzone.GetHeight(), wallsandfloors, TileData.GetTileTypeToCellBlockType(this.tletype), player, this.tletype == TILETYPE.GraveYard);
        player.OldSaveThisPlayer();
      }
      else
      {
        this.dragzonecamera.UpdateDragZoneCamera(player, DeltaTime);
        this.dragzone.UpdateDragZone(DeltaTime, player);
        LiveStats.DraggingDragZone = this.dragzone.IsDraggingging;
        int volume = this.dragzone.GetVolume();
        if (volume == this.Volume)
          return;
        this.Volume = volume;
        Console.WriteLine("VOL" + (object) this.Volume);
        this.buildUI.SetUp(this.tletype, player, this.Volume, this.dragzone.GetHeight() >= 8 && this.dragzone.GetWidth() >= 8);
        if (this.dragzone.GetIsBlocked(wallsandfloors.tilesasarray))
          this.buildUI.SetUp(BuildMessageType.Overlapping, player);
        else if (!this.dragzone.GetThisIsnextToSomething(wallsandfloors.tilesasarray))
          this.buildUI.SetUp(BuildMessageType.PlaceNextToExistingWall, player);
        else if (this.dragzone.GetWidth() < 3 && this.dragzone.GetHeight() < 3)
          this.buildUI.SetUp(BuildMessageType.TooSmall, player);
        else if (this.dragzone.GetHeight() < 3)
          this.buildUI.SetUp(BuildMessageType.MakeTaller, player);
        else if (this.dragzone.GetWidth() < 3)
          this.buildUI.SetUp(BuildMessageType.MakeWider, player);
        else
          this.buildUI.SetUp(BuildMessageType.CanBuild, player);
      }
    }

    public void BuyInCurrentState(Player player, WallsAndFloorsManager wallsandfloors)
    {
      SoundEffectsManager.PlaySpecificSound(SoundEffectType.BuildSomething);
      player.prisonlayout.AddNewCellBlock(this.dragzone.GetTopLeft().X, this.dragzone.GetTopLeft().Y, this.dragzone.GetWidth(), this.dragzone.GetHeight(), wallsandfloors, TileData.GetTileTypeToCellBlockType(this.tletype), player, this.tletype == TILETYPE.GraveYard);
      player.OldSaveThisPlayer();
    }

    public void DrawCellBlockMakerManager(TileRenderer[,] tilesasarray)
    {
      this.dragzone.DrawDragZone(tilesasarray);
      if (this.buildUI == null || DebugFlags.IsPCVersion)
        return;
      this.buildUI.DrawBuildUI();
    }
  }
}
