// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.VolumeBuilding.TileRenderContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.VolumeBuilding
{
  internal class TileRenderContainer
  {
    private TileRenderer renderer;
    private int XLocation;
    private int YLocation;
    public bool Active;
    public bool Locked;
    private SurroundingInformation surrounder;
    private TILETYPE UNDO_OldUnderFloor;
    private int UNDO_TopRotation;
    private Vector2Int LOC;
    private LayoutEntry entry;
    private bool IsWater;

    public TileRenderContainer(int _XLocation, int _YLocation)
    {
      this.LOC = new Vector2Int(_XLocation, _YLocation);
      this.XLocation = _XLocation;
      this.YLocation = _YLocation;
    }

    public void UnSetTile(
      WallsAndFloorsManager wallsandfloors,
      Player player,
      ref bool NewFloorTileIsAVolume,
      bool IsUnderFloorUnset = false)
    {
      if (!this.Active)
        return;
      this.Locked = false;
      this.Active = false;
      this.surrounder.UnBlockThis(1, 1);
      if (IsUnderFloorUnset)
      {
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype = this.UNDO_OldUnderFloor;
      }
      else
      {
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype;
        if (player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype == TILETYPE.None)
          player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype = this.UNDO_OldUnderFloor;
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].RotationClockWise = this.UNDO_TopRotation;
        if (TileData.IsThisFloorAVolume(player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype))
        {
          NewFloorTileIsAVolume = true;
          if (player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype == TILETYPE.None)
            player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
        }
      }
      wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, this.LOC);
    }

    public void SetTile(TILETYPE tiletype, Vector2Int Loc, Player player)
    {
      if (this.Active)
        return;
      if (this.XLocation != Loc.X || this.YLocation != Loc.Y)
        throw new Exception("jns");
      this.Active = true;
      this.entry = new LayoutEntry(tiletype);
      this.entry.RotationClockWise = 0;
      this.CheckBackUp(player);
      this.renderer = new TileRenderer(this.entry, this.XLocation, this.YLocation, false);
    }

    private void CheckBackUp(Player player)
    {
      this.IsWater = TileData.IsThisWater(this.entry.tiletype);
      if (!this.IsWater && TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype))
      {
        if (this.entry.tiletype == player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype)
          return;
        this.UNDO_OldUnderFloor = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype;
        this.UNDO_TopRotation = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].RotationClockWise;
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype = this.entry.tiletype;
      }
      else
      {
        if (player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype == this.entry.tiletype)
          return;
        this.UNDO_OldUnderFloor = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype;
        this.UNDO_TopRotation = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].RotationClockWise;
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].UnderFloorTiletype = player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype;
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype = this.entry.tiletype;
      }
    }

    public void ForceLocked() => this.Locked = true;

    public void BlockSurroundingTile(int relativeX, int RelativeY)
    {
      if (!this.Active)
        return;
      this.surrounder.ModifyEdge(true, relativeX, RelativeY);
    }

    public SurroundingInformation GetSurroundClass(bool IsDeleting) => this.surrounder != null || IsDeleting ? this.surrounder : throw new Exception("CANNOT BE NULL FOR SELF");

    public void SetSurrounder(SurroundingInformation _surrounder) => this.surrounder = _surrounder;

    public void SetRenderTileFromSurround(WallsAndFloorsManager wallsandfloors, Player player)
    {
      if (!this.Active)
        return;
      bool flag = false;
      if (!this.IsWater && TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].tiletype))
        flag = true;
      if (!flag)
      {
        this.entry.RotationClockWise = (int) this.surrounder.GetEdgeType();
        this.renderer = new TileRenderer(this.entry, this.XLocation, this.YLocation, false);
      }
      this.CheckBackUp(player);
      if (!flag)
        player.prisonlayout.layout.FloorTileTypes[this.XLocation, this.YLocation].RotationClockWise = this.entry.RotationClockWise;
      wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, this.LOC);
    }

    public void SetSurroudIncludingOriginalMapping(
      Player player,
      TILETYPE tiletype,
      Vector2Int location,
      bool IsFloor,
      bool SetSelf = false,
      bool ForceAllFalse = false)
    {
      if (this.surrounder == null)
        this.surrounder = new SurroundingInformation();
      if (ForceAllFalse)
        this.surrounder.UnBlockAll();
      if (!IsFloor)
        throw new Exception("THE CODE BELOW ONLY KNOWS ABOUT FLOORS - JUST ADD NON FLOORS");
      for (int index1 = -1; index1 < 2; ++index1)
      {
        for (int index2 = -1; index2 < 2; ++index2)
        {
          if ((index1 != 0 || index2 != 0 || SetSelf) && (index1 + location.X >= 0 && index1 + location.X < player.prisonlayout.layout.FloorTileTypes.GetLength(0) && (index2 + location.Y >= 0 && index2 + location.Y < player.prisonlayout.layout.FloorTileTypes.GetLength(1))) && player.prisonlayout.layout.FloorTileTypes[index1 + location.X, index2 + location.Y].tiletype == tiletype)
            this.surrounder.BlockThis(index1 + 1, index2 + 1);
        }
      }
    }

    public SurroundingInformation CommitTile()
    {
      if (this.surrounder == null)
        this.surrounder = new SurroundingInformation();
      this.Locked = true;
      return this.surrounder;
    }

    public void DeleteThisTile()
    {
      this.Locked = false;
      this.Active = false;
    }

    public void DrawTileRenderContainer() => throw new Exception("This is preison planet right");
  }
}
