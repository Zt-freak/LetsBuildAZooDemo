// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DeleteBuildings.DeleteBuildingCursor
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.DeleteBuildings
{
  internal class DeleteBuildingCursor
  {
    private ThingToBuildFootPrint footprint;
    public bool Active;
    private Vector2 vLocation;
    private bool IsDestroy;
    public LayoutEntry layoutentry;
    private bool DrawEntrances;

    public DeleteBuildingCursor(
      Vector2Int Location,
      Player player,
      bool _IsDestroy = false,
      bool _DrawEntrances = true)
    {
      this.DrawEntrances = _DrawEntrances;
      this.IsDestroy = _IsDestroy;
      this.layoutentry = new LayoutEntry(player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype);
      this.vLocation = TileMath.GetTileToWorldSpace(Location);
      this.layoutentry.RotationClockWise = player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].RotationClockWise;
      TileRenderer renderer = new TileRenderer(this.layoutentry, Location.X, Location.Y, false, true);
      this.footprint = new ThingToBuildFootPrint(renderer.XWidth, renderer.YHeight, renderer, TileData.GetTileInfo(player.prisonlayout.layout.BaseTileTypes[Location.X, Location.Y].tiletype), IsDestroy: this.IsDestroy);
    }

    public void UpdateDeleteBuildingCursor()
    {
    }

    public void DrawDeleteBuildingCursor() => this.footprint.DrawThingToBuildFootPrint(this.vLocation, AssetContainer.pointspritebatch03, this.DrawEntrances);
  }
}
