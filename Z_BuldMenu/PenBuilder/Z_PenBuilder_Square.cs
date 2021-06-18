// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.Z_PenBuilder_Square
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine.Input;
using TinyZoo.OverWorld.OverWorldBuildMenu.BuildSystem.ThingToBuild.CellBlockMaker;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;

namespace TinyZoo.Z_BuldMenu.PenBuilder
{
  internal class Z_PenBuilder_Square
  {
    private CellBlockMakerManager cellblockmaker;
    private TILETYPE Buildinthispen;

    public Z_PenBuilder_Square(TILETYPE buildthispen, Player player)
    {
      this.Buildinthispen = buildthispen;
      this.cellblockmaker = new CellBlockMakerManager(buildthispen, player);
    }

    public TILETYPE GetTileTypeBeingBult() => this.Buildinthispen;

    public int GetVolume() => this.cellblockmaker.GetVolume();

    public bool GetIsBlocked() => this.cellblockmaker.GetIsBlocked();

    public void UpdaeteZ_PenBuilderSquare(
      Player player,
      float DeltaTime,
      WallsAndFloorsManager wallsandfloors)
    {
      FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = true;
      if ((double) player.inputmap.PointerLocation.Y < (double) Z_BuildingIconPanel.MinHeight && !MouseStatus.LMouseHeld)
      {
        GameFlags.IsUsingMouse = !MouseStatus.RMouseHeld;
        Z_GameFlags.ForceRightMouseDrag = true;
        FeatureFlags.BlockPlayerMoveCameraDuringCellBlockDrag = false;
      }
      if ((double) player.player.touchinput.ReleaseTapArray[0].Y > (double) Z_BuildingIconPanel.MinHeight || (double) player.player.touchinput.MultiTouchTouchLocations[0].Y > (double) Z_BuildingIconPanel.MinHeight)
        return;
      this.cellblockmaker.UpdateCellBlockMakerManager(player, DeltaTime, wallsandfloors);
    }

    public void BuyInCurrentState(Player player, WallsAndFloorsManager wallsandfloors) => this.cellblockmaker.BuyInCurrentState(player, wallsandfloors);

    public void DrawZ_PenBuilderOldSquare(TileRenderer[,] tilesasarray) => this.cellblockmaker.DrawCellBlockMakerManager(tilesasarray);
  }
}
