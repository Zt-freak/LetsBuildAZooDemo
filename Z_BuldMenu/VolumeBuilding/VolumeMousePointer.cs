// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.VolumeBuilding.VolumeMousePointer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;

namespace TinyZoo.Z_BuldMenu.VolumeBuilding
{
  internal class VolumeMousePointer
  {
    private TileRenderer tilerenderer;
    private EntranceArrow entrancearrow;
    private LayoutEntry layout;
    private bool IsBlocked;
    private static Vector2 ThreadLoc = Vector2.Zero;
    private static Vector2 ThreadScale = Vector2.Zero;

    public VolumeMousePointer(TILETYPE tile)
    {
      this.layout = new LayoutEntry(tile);
      this.layout.RotationClockWise = 19;
      this.tilerenderer = new TileRenderer(this.layout, 0, 0, false);
      this.entrancearrow = new EntranceArrow(0);
      this.entrancearrow.SetAsBlocked();
    }

    public void SetNewMousePosition(
      Vector2Int location,
      bool IsWater,
      Player player,
      WallsAndFloorsManager wallsandfloors)
    {
      this.IsBlocked = false;
      this.tilerenderer = new TileRenderer(this.layout, location.X, location.Y, false);
      if (IsWater && Z_GameFlags.pathfinder.GetIsBlocked(location.X, location.Y) && (!TileMath.TileIsInBuildablePartOfWorld(location.X, location.Y) || !TileData.IsThisWater(player.prisonlayout.layout.FloorTileTypes[location.X, location.Y].tiletype)))
        this.IsBlocked = true;
      BlockInfo blockInfo = wallsandfloors.CanBuildThisHere(location, this.tilerenderer, true, true, layoudata: player.prisonlayout.layout);
      if (blockInfo == null || !blockInfo.SomethingIsBlocked)
        return;
      this.IsBlocked = true;
    }

    public bool GetIsBlocked() => this.IsBlocked;

    public void UpdateMousePointer()
    {
    }

    public void DrawMousePointer()
    {
      this.tilerenderer.HasDrawn = false;
      this.tilerenderer.DrawTileRenderer(AssetContainer.pointspritebatch03, ref VolumeMousePointer.ThreadLoc, ref VolumeMousePointer.ThreadScale);
      if (!this.IsBlocked)
        return;
      this.entrancearrow.DrawEntrance(this.tilerenderer.vLocation, AssetContainer.pointspritebatch03);
    }
  }
}
