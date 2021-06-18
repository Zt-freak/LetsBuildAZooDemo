// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer.PenMoveCollision
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuldMenu.PenBuilder.MovePen;
using TinyZoo.Z_BuldMenu.PenBuilder.Pens;
using TinyZoo.Z_HeatMaps;
using TinyZoo.Z_Notification;

namespace TinyZoo.Z_BuldMenu.PenBuilder.GatePlacer
{
  internal class PenMoveCollision
  {
    private List<EntranceArrow> BlockedPlaces;
    private List<Vector2Int> OriginalPlaces;
    private List<bool> OriginalFloor;
    private List<Vector2Int> BLOCKS;
    private List<Vector5Int> Children;
    private bool[,] MOVE_Blocks;
    private Vector2Int TopLeftBlock;
    private int RefCellFarmUID;
    private static Vector2Int ParentPosTemp = new Vector2Int();
    private static Vector2Int newPos;
    private bool Canplace;
    internal static Vector2Int Offset;

    public PenMoveCollision(
      List<TileRenderer> tilerenderers,
      PerimeterBuilder perimeterBuilder,
      PrisonZone prisonzone,
      Player player)
    {
      this.RefCellFarmUID = prisonzone.Cell_UID;
      if (prisonzone.IsFarm)
        this.RefCellFarmUID *= -1;
      if (PenMoveCollision.Offset == null)
        PenMoveCollision.Offset = new Vector2Int();
      this.BLOCKS = new List<Vector2Int>();
      PenMoveCollision.newPos = new Vector2Int();
      this.OriginalPlaces = new List<Vector2Int>();
      this.BlockedPlaces = new List<EntranceArrow>();
      this.OriginalFloor = new List<bool>();
      this.MOVE_Blocks = new bool[prisonzone.WidthAndHeight.X + 1, prisonzone.WidthAndHeight.Y + 1];
      this.TopLeftBlock = new Vector2Int(prisonzone.TopLeft);
      this.Children = new List<Vector5Int>();
      for (int index = 0; index < tilerenderers.Count; ++index)
      {
        this.BlockedPlaces.Add(new EntranceArrow(0));
        this.BlockedPlaces[index].bActive = false;
        this.BlockedPlaces[index].SetAsBlocked();
        this.OriginalPlaces.Add(new Vector2Int(tilerenderers[index].TileLocation));
        this.OriginalFloor.Add(false);
        this.MOVE_Blocks[tilerenderers[index].TileLocation.X - this.TopLeftBlock.X, tilerenderers[index].TileLocation.Y - this.TopLeftBlock.Y] = true;
        if (Z_GameFlags.pathfinder.GetIsBlocked(tilerenderers[index].TileLocation.X, tilerenderers[index].TileLocation.Y))
          this.BLOCKS.Add(new Vector2Int(tilerenderers[index].TileLocation));
      }
      perimeterBuilder.GetFloors();
      for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
      {
        if (player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].GetIsChild())
          this.Children.Add(new Vector5Int((int) player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].tiletype, prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y, player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].ParentLocation.X, player.prisonlayout.layout.BaseTileTypes[prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y].ParentLocation.Y));
      }
      for (int index = 0; index < prisonzone.penItems.items.Count; ++index)
      {
        this.BlockedPlaces.Add(new EntranceArrow(0));
        this.BlockedPlaces[this.BlockedPlaces.Count - 1].bActive = false;
        this.BlockedPlaces[this.BlockedPlaces.Count - 1].SetAsBlocked();
        if (!this.MOVE_Blocks[prisonzone.penItems.items[index].Location.X - this.TopLeftBlock.X, prisonzone.penItems.items[index].Location.Y - this.TopLeftBlock.Y])
        {
          this.MOVE_Blocks[prisonzone.penItems.items[index].Location.X - this.TopLeftBlock.X, prisonzone.penItems.items[index].Location.Y - this.TopLeftBlock.Y] = true;
          this.OriginalPlaces.Add(new Vector2Int(prisonzone.penItems.items[index].Location));
        }
        this.OriginalFloor.Add(false);
      }
      for (int index = 0; index < prisonzone.FloorTiles.Count; ++index)
      {
        this.BlockedPlaces.Add(new EntranceArrow(0));
        this.BlockedPlaces[this.BlockedPlaces.Count - 1].bActive = false;
        this.BlockedPlaces[this.BlockedPlaces.Count - 1].SetAsBlocked();
        if (!this.MOVE_Blocks[prisonzone.FloorTiles[index].X - this.TopLeftBlock.X, prisonzone.FloorTiles[index].Y - this.TopLeftBlock.Y])
        {
          this.MOVE_Blocks[prisonzone.FloorTiles[index].X - this.TopLeftBlock.X, prisonzone.FloorTiles[index].Y - this.TopLeftBlock.Y] = true;
          this.OriginalPlaces.Add(new Vector2Int(prisonzone.FloorTiles[index]));
        }
        this.OriginalFloor.Add(true);
        if (Z_GameFlags.pathfinder.GetIsBlocked(prisonzone.FloorTiles[index].X, prisonzone.FloorTiles[index].Y))
        {
          this.BLOCKS.Add(new Vector2Int(prisonzone.FloorTiles[index]));
          this.MOVE_Blocks[prisonzone.FloorTiles[index].X - this.TopLeftBlock.X, prisonzone.FloorTiles[index].Y - this.TopLeftBlock.Y] = true;
        }
      }
    }

    public void ConfirmMove(Player player, WallsAndFloorsManager wallsandfloors, int CELLUID)
    {
      List<BaseTileDesc> baseTileDescList = new List<BaseTileDesc>();
      for (int index = 0; index < this.Children.Count; ++index)
      {
        player.prisonlayout.layout.BaseTileTypes[this.Children[index].W, this.Children[index].X].UnsetChild();
        PenMoveCollision.ParentPosTemp.X = this.Children[index].Y + PenMoveCollision.Offset.X;
        PenMoveCollision.ParentPosTemp.Y = this.Children[index].Z + PenMoveCollision.Offset.Y;
      }
      for (int index = 0; index < this.OriginalPlaces.Count; ++index)
      {
        if (this.OriginalFloor[index])
          baseTileDescList.Add(new BaseTileDesc(player.prisonlayout.layout.FloorTileTypes[this.OriginalPlaces[index].X, this.OriginalPlaces[index].Y]));
        else
          baseTileDescList.Add(new BaseTileDesc(player.prisonlayout.layout.BaseTileTypes[this.OriginalPlaces[index].X, this.OriginalPlaces[index].Y]));
        player.prisonlayout.layout.BaseTileTypes[this.OriginalPlaces[index].X, this.OriginalPlaces[index].Y].tiletype = TILETYPE.None;
        wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: this.OriginalPlaces[index], DoRemakeTileLists: false);
        player.prisonlayout.layout.FloorTileTypes[this.OriginalPlaces[index].X, this.OriginalPlaces[index].Y].tiletype = TILETYPE.EMPTY_DIRT_WALKABLE_TILE;
        wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, this.OriginalPlaces[index], false);
      }
      for (int index = 0; index < this.OriginalPlaces.Count; ++index)
      {
        if (this.OriginalFloor[index])
        {
          player.prisonlayout.layout.FloorTileTypes[this.OriginalPlaces[index].X + PenMoveCollision.Offset.X, this.OriginalPlaces[index].Y + PenMoveCollision.Offset.Y].CloneFromBaseTileDesc(baseTileDescList[index]);
          wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, new Vector2Int(this.OriginalPlaces[index].X + PenMoveCollision.Offset.X, this.OriginalPlaces[index].Y + PenMoveCollision.Offset.Y), false);
        }
        else
        {
          player.prisonlayout.layout.BaseTileTypes[this.OriginalPlaces[index].X + PenMoveCollision.Offset.X, this.OriginalPlaces[index].Y + PenMoveCollision.Offset.Y].CloneFromBaseTileDesc(baseTileDescList[index]);
          wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, JustThisTile: new Vector2Int(this.OriginalPlaces[index].X + PenMoveCollision.Offset.X, this.OriginalPlaces[index].Y + PenMoveCollision.Offset.Y), DoRemakeTileLists: false);
        }
      }
      PrisonZone prisonZone;
      if (Z_GameFlags.SelectedPrisonZoneisFarm)
      {
        prisonZone = player.farms.GetThisFarmFieldByUID(Z_GameFlags.SelectedPrisonZoneUID);
        prisonZone.cropsatus.MoveCrop(PenMoveCollision.Offset);
      }
      else
        prisonZone = player.prisonlayout.cellblockcontainer.GetThisCellBlock(CELLUID);
      TILETYPE cellBlockTypeToPice = TileData.GetCellBlockTypeToPice(prisonZone.CellBLOCKTYPE, CellBlockPiece.Floor);
      for (int index = 0; index < prisonZone.penItems.items.Count; ++index)
      {
        player.prisonlayout.layout.FloorTileTypes[prisonZone.penItems.items[index].Location.X, prisonZone.penItems.items[index].Location.Y].tiletype = cellBlockTypeToPice;
        wallsandfloors.VallidateAgainstLayout(player.prisonlayout.layout, true, new Vector2Int(prisonZone.penItems.items[index].Location.X, prisonZone.penItems.items[index].Location.Y), false);
      }
      for (int index = 0; index < this.BLOCKS.Count; ++index)
        Z_GameFlags.pathfinder.UnblockTile(this.BLOCKS[index].X, this.BLOCKS[index].Y, true);
      for (int index = 0; index < this.BLOCKS.Count; ++index)
        Z_GameFlags.pathfinder.BlockTile(this.BLOCKS[index].X + PenMoveCollision.Offset.X, this.BLOCKS[index].Y + PenMoveCollision.Offset.Y, true);
      for (int index = 0; index < this.Children.Count; ++index)
      {
        PenMoveCollision.ParentPosTemp.X = this.Children[index].Y + PenMoveCollision.Offset.X;
        PenMoveCollision.ParentPosTemp.Y = this.Children[index].Z + PenMoveCollision.Offset.Y;
        player.prisonlayout.layout.BaseTileTypes[this.Children[index].W + PenMoveCollision.Offset.X, this.Children[index].X + PenMoveCollision.Offset.Y].SetChild(PenMoveCollision.ParentPosTemp, (TILETYPE) this.Children[index].V);
      }
      wallsandfloors.RemakeTileList();
      Z_NotificationManager.AddPenIDTorecountWater(CELLUID);
      Z_NotificationManager.RescrubWater = true;
    }

    public bool CanPlace() => this.Canplace;

    public void SetNewLocations(Vector2Int _Offset, Player player)
    {
      this.Canplace = true;
      PenMoveCollision.Offset.X = _Offset.X;
      PenMoveCollision.Offset.Y = _Offset.Y;
      for (int index = 0; index < this.OriginalPlaces.Count; ++index)
      {
        PenMoveCollision.newPos.X = PenMoveCollision.Offset.X + this.OriginalPlaces[index].X;
        PenMoveCollision.newPos.Y = PenMoveCollision.Offset.Y + this.OriginalPlaces[index].Y;
        bool flag = false;
        this.BlockedPlaces[index].bActive = false;
        if (PenMoveCollision.newPos.X >= this.TopLeftBlock.X && PenMoveCollision.newPos.Y >= this.TopLeftBlock.Y && (PenMoveCollision.newPos.X - this.TopLeftBlock.X < this.MOVE_Blocks.GetLength(0) && PenMoveCollision.newPos.Y - this.TopLeftBlock.Y < this.MOVE_Blocks.GetLength(1)) && this.MOVE_Blocks[PenMoveCollision.newPos.X - this.TopLeftBlock.X, PenMoveCollision.newPos.Y - this.TopLeftBlock.Y])
          flag = true;
        if (!flag)
        {
          int cell = Enclosure_Farm_Map.GetCell(PenMoveCollision.newPos.X, PenMoveCollision.newPos.Y, player);
          if (cell != 0 && cell != this.RefCellFarmUID || Z_GameFlags.pathfinder.GetIsBlocked(PenMoveCollision.newPos.X, PenMoveCollision.newPos.Y))
          {
            this.BlockedPlaces[index].bActive = true;
            this.Canplace = false;
            this.BlockedPlaces[index].vLocation = TileMath.GetTileToWorldSpace(PenMoveCollision.newPos);
          }
          else if (PenMoveCollision.newPos.Y > TileMath.GetGateLocationV2Int().Y - 4)
          {
            TileMath.GetGateLocationV2Int();
            if (PenMoveCollision.newPos.X > WalkingPerson.LogoLocation.X - 3 && PenMoveCollision.newPos.X < WalkingPerson.LogoLocation.X + 2)
            {
              this.BlockedPlaces[index].bActive = true;
              this.Canplace = false;
              this.BlockedPlaces[index].vLocation = TileMath.GetTileToWorldSpace(PenMoveCollision.newPos);
            }
          }
        }
      }
    }

    public void DrawPenMoveCollision()
    {
      for (int index = 0; index < this.BlockedPlaces.Count; ++index)
      {
        if (this.BlockedPlaces[index].bActive)
          this.BlockedPlaces[index].DrawEntrance(Vector2.Zero, AssetContainer.pointspritebatch03);
      }
    }
  }
}
