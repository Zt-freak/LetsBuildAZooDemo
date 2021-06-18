// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.DeleteBuildings.Z_DeleteBuildingManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.MoveBuilding;
using TinyZoo.Z_CharacterSelect;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_HUD.VirtualMouse;
using TinyZoo.Z_PenInfo.MainBar;

namespace TinyZoo.Z_BuldMenu.DeleteBuildings
{
  internal class Z_DeleteBuildingManager
  {
    private Vector2Int SelectedLocation;
    private Vector2Int LastIconLoc;
    private DeleteBuildingCursor deletebuildingcursor;
    private SelectionFrame mouselocator;
    private Vector2 WorldSpaceLoc;
    private static DeleteFrame deleteframe;
    private bool CanDestroy;

    public Z_DeleteBuildingManager(Player player)
    {
      this.SelectedLocation = TileMath.GetWorldSpaceToTile(RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation));
      this.mouselocator = new SelectionFrame(16, 16, UseWhite: true);
      if (Z_DeleteBuildingManager.deleteframe == null)
        Z_DeleteBuildingManager.deleteframe = new DeleteFrame();
      this.CreateCursor(player);
    }

    public bool UpdateZ_DeleteBuildingManager(Player player, WallsAndFloorsManager wallsandfloors)
    {
      if (player.inputmap.ReleasedThisFrame[7])
        return true;
      MainBarManager.BarIsOnScreen = true;
      FeatureFlags.InstantBlockTopBar = true;
      Vector2Int worldSpaceToTile = TileMath.GetWorldSpaceToTile(RenderMath.TranslateScreenSpaceToWorldSpace(player.inputmap.PointerLocation));
      this.WorldSpaceLoc = TileMath.GetTileToWorldSpace(worldSpaceToTile);
      if (!worldSpaceToTile.CompareMatches(this.SelectedLocation))
      {
        if (this.deletebuildingcursor != null)
          this.deletebuildingcursor.Active = false;
        this.SelectedLocation = worldSpaceToTile;
        this.CreateCursor(player);
      }
      if (this.CanDestroy && this.deletebuildingcursor != null && (this.deletebuildingcursor.Active && (double) player.player.touchinput.ReleaseTapArray[0].X > 0.0))
      {
        OverWorldManager.movebuilding = new Z_MoveBuildingManager(this.deletebuildingcursor.layoutentry, this.SelectedLocation, player, OverWorldManager.overworldenvironment, WasDestroyButton: true);
        OverWorldManager.movebuilding = (Z_MoveBuildingManager) null;
        this.LastIconLoc = (Vector2Int) null;
        this.deletebuildingcursor = (DeleteBuildingCursor) null;
        this.CreateCursor(player);
      }
      return false;
    }

    private void CreateCursor(Player player)
    {
      if (this.SelectedLocation.X <= -1 || this.SelectedLocation.X >= player.prisonlayout.layout.BaseTileTypes.GetLength(0) || (this.SelectedLocation.Y <= -1 || this.SelectedLocation.Y >= player.prisonlayout.layout.BaseTileTypes.GetLength(1)) || (player.prisonlayout.layout.BaseTileTypes[this.SelectedLocation.X, this.SelectedLocation.Y] == null || player.prisonlayout.layout.BaseTileTypes[this.SelectedLocation.X, this.SelectedLocation.Y].tiletype == TILETYPE.None))
        return;
      TILETYPE tiletype = player.prisonlayout.layout.BaseTileTypes[this.SelectedLocation.X, this.SelectedLocation.Y].tiletype;
      if (player.prisonlayout.layout.BaseTileTypes[this.SelectedLocation.X, this.SelectedLocation.Y].GetIsChild())
        this.SelectedLocation = player.prisonlayout.layout.BaseTileTypes[this.SelectedLocation.X, this.SelectedLocation.Y].GetParentLocation();
      this.CanDestroy = true;
      if (TileData.IsAStoreRoom(tiletype))
        this.CanDestroy = false;
      else if (Enclosure_Farm_Map.GetCell(this.SelectedLocation.X, this.SelectedLocation.Y, player) != 0)
        this.CanDestroy = false;
      else if (TileData.IsACRISPRBuilding(tiletype))
        this.CanDestroy = false;
      else if (tiletype == TILETYPE.DefaultFence_InnerCorner || tiletype == TILETYPE.DefaultFence_WallCorner || tiletype == TILETYPE.DefaultFence_WallSide)
        this.CanDestroy = false;
      else if (TileData.IsThisAPenBoundary(tiletype))
        this.CanDestroy = false;
      else if (TileData.IsAnArchitectOffice(tiletype))
        this.CanDestroy = false;
      else if (TileData.IsAManagementOffice(tiletype))
        this.CanDestroy = false;
      else if (this.SelectedLocation.Y >= WalkingPerson.LogoLocation.Y)
        this.CanDestroy = false;
      else if (TileData.IsATicketOffice(tiletype))
        this.CanDestroy = false;
      else if (!TileMath.TileIsInBuildablePartOfWorld(this.SelectedLocation.X, this.SelectedLocation.Y))
        this.CanDestroy = false;
      if (this.LastIconLoc == null || !this.LastIconLoc.CompareMatches(this.SelectedLocation))
        this.deletebuildingcursor = new DeleteBuildingCursor(this.SelectedLocation, player, this.CanDestroy, false);
      this.deletebuildingcursor.Active = true;
    }

    public void DrawZ_DeleteBuildingManager()
    {
      Z_DeleteBuildingManager.deleteframe.DrawDeleteFrame();
      if (this.deletebuildingcursor != null && this.deletebuildingcursor.Active)
        this.deletebuildingcursor.DrawDeleteBuildingCursor();
      else
        this.mouselocator.ScreenSpaceDrawInBuild(RenderMath.TranslateWorldSpaceToScreenSpace(this.WorldSpaceLoc), AssetContainer.pointspritebatchTop05);
      VirtualMouseManager.DrawBin = true;
    }
  }
}
