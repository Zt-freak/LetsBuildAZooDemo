// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.WaterBuild.VolumeBuilderManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using SEngine.Input;
using System;
using TinyZoo.OverWorld.OverWorldEnv;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.WaterBuild
{
  internal class VolumeBuilderManager
  {
    private Vector2Int LastLocation;
    private VolumeMaker volumemmaker;
    private bool IsPainting;
    private bool IsDeleting;
    private TILETYPE tile;

    public VolumeBuilderManager(TILETYPE _tile)
    {
      this.tile = _tile;
      this.volumemmaker = new VolumeMaker(this.tile);
    }

    public void UpdateVolumeBuilder(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors,
      OverWorldEnvironmentManager overworldenvironment)
    {
      if ((double) player.inputmap.PointerLocation.Y < (double) Z_BuildingIconPanel.MinHeight)
      {
        GameFlags.IsUsingMouse = !MouseStatus.RMouseHeld;
        Z_GameFlags.ForceRightMouseDrag = true;
        Vector2Int spaceToTileLocation = TileMath.GetScreenSPaceToTileLocation(player.inputmap.PointerLocation);
        if (this.LastLocation == null || !spaceToTileLocation.CompareMatches(this.LastLocation))
        {
          if (this.LastLocation == null)
            this.LastLocation = new Vector2Int(spaceToTileLocation);
          this.LastLocation.X = spaceToTileLocation.X;
          this.LastLocation.Y = spaceToTileLocation.Y;
          this.volumemmaker.SetNewMouseLocation(this.LastLocation, player, wallsandfloors);
          if (this.IsPainting)
          {
            Console.WriteLine("LAST LOC" + (object) this.LastLocation.X + "-" + (object) this.LastLocation.Y);
            if (!this.volumemmaker.GetIsBlocked())
            {
              if (this.volumemmaker.IsWater && player.prisonlayout.layout.BaseTileTypes[this.LastLocation.X, this.LastLocation.Y].tiletype != TILETYPE.None)
                return;
              if ((this.IsDeleting || !this.IsDeleting && !player.prisonlayout.IsThisAlreadyHere(this.tile, this.LastLocation, this.volumemmaker.IsFloor)) && this.HandleMoney(player))
              {
                bool NewFloorTileIsAVolume_ONDELETE;
                this.volumemmaker.CommitTile(this.IsDeleting, wallsandfloors, player, this.LastLocation, out NewFloorTileIsAVolume_ONDELETE);
                this.CheckWaterLayer(this.LastLocation, player, this.IsDeleting, overworldenvironment);
                if (NewFloorTileIsAVolume_ONDELETE)
                  this.volumemmaker.FixThisTile(player, this.LastLocation, wallsandfloors, this.volumemmaker.IsFloor);
              }
            }
          }
        }
        int num = player.inputmap.RightMousReleaseClick ? 1 : 0;
        if ((double) player.player.touchinput.MultiTouchTapArray[0].X > 0.0)
        {
          this.IsPainting = !this.IsPainting ? true : throw new Exception("CANT HAPPEN!");
          this.IsDeleting = this.volumemmaker.HasThisHere(this.tile, this.LastLocation) || player.prisonlayout.IsThisAlreadyHere(this.tile, this.LastLocation, this.volumemmaker.IsFloor);
          if (!this.volumemmaker.GetIsBlocked())
          {
            if (this.volumemmaker.IsWater && player.prisonlayout.layout.BaseTileTypes[this.LastLocation.X, this.LastLocation.Y].tiletype != TILETYPE.None)
              return;
            if (this.HandleMoney(player))
            {
              bool NewFloorTileIsAVolume_ONDELETE;
              this.volumemmaker.CommitTile(this.IsDeleting, wallsandfloors, player, this.LastLocation, out NewFloorTileIsAVolume_ONDELETE);
              this.CheckWaterLayer(this.LastLocation, player, this.IsDeleting, overworldenvironment);
              if (NewFloorTileIsAVolume_ONDELETE)
                this.volumemmaker.FixThisTile(player, this.LastLocation, wallsandfloors, this.volumemmaker.IsFloor);
            }
          }
        }
      }
      if ((double) player.player.touchinput.MultiTouchTouchLocations[0].X >= 0.0)
        return;
      this.IsPainting = false;
    }

    private bool HandleMoney(Player player)
    {
      int cost = player.livestats.GetCost(this.tile, player, true);
      return this.IsDeleting || player.Stats.SpendCash(cost, SpendingCashOnThis.BuyBuilding, player);
    }

    private void CheckWaterLayer(
      Vector2Int Location,
      Player player,
      bool _IsDeleting,
      OverWorldEnvironmentManager overworldenvironment)
    {
      if (!this.volumemmaker.IsWater || !_IsDeleting || player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype == TILETYPE.None)
        return;
      BuildingEvents.SellStructureAndUpdateRenderer(player, Location, overworldenvironment);
    }

    public void DrawVolumeBuilder() => this.volumemmaker.DrawVolumeMaker();
  }
}
