// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_OverWorld.LocationDirectory
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Data;
using TinyZoo.Z_HUD.PointAtThings;
using TinyZoo.Z_OverWorld.AvatarUI.Selection;
using TinyZoo.Z_OverWorld.Location_Directory;
using TinyZoo.Z_TrashSystem;

namespace TinyZoo.Z_OverWorld
{
  internal class LocationDirectory
  {
    private Loc_CityBlock[,] CityGrid;
    private int BlockSize;
    private int[] TotalPointsOfInterestByType;
    private int CityWidth;
    private int CityHeight;
    private List<PenDirectory> ThingsInPens = new List<PenDirectory>();

    public LocationDirectory(int _CityWidth, int _CityHeight, int _BlockSize)
    {
      this.CityWidth = _CityWidth;
      this.CityHeight = _CityHeight;
      this.BlockSize = _BlockSize;
      this.Create();
    }

    public void RecreateLocationDirectory(LayoutData layoutdata, Player player)
    {
    }

    private void Create()
    {
      this.TotalPointsOfInterestByType = new int[10];
      this.CityGrid = new Loc_CityBlock[(this.CityWidth + this.BlockSize) / this.BlockSize, (this.CityHeight + this.BlockSize) / this.BlockSize];
      for (int index1 = 0; index1 < this.CityGrid.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < this.CityGrid.GetLength(1); ++index2)
          this.CityGrid[index1, index2] = new Loc_CityBlock(this.BlockSize, new Vector2Int(index1 * this.BlockSize, index2 * this.BlockSize));
      }
    }

    public void AddTrash(TrashDrop trashdrop) => this.CityGrid[trashdrop.TileLocation.X / this.BlockSize, trashdrop.TileLocation.Y / this.BlockSize].AddTrash(trashdrop);

    public bool HasPoopHere(int PenUID)
    {
      for (int index = 0; index < this.ThingsInPens.Count; ++index)
      {
        if (this.ThingsInPens[index].PenUID == PenUID)
          return this.ThingsInPens[index].HasPoopHere();
      }
      return false;
    }

    public TrashDrop GetNextPoop(int PenUID, int ListID, ref bool ReachedEndOfList)
    {
      for (int index = 0; index < this.ThingsInPens.Count; ++index)
      {
        if (this.ThingsInPens[index].PenUID == PenUID)
          return this.ThingsInPens[index].GetNextPoop(ListID, ref ReachedEndOfList);
      }
      return (TrashDrop) null;
    }

    public void PickUpPoop(TrashDrop trashdrop, int PenUID)
    {
    }

    public void AddPoop(TrashDrop trashdrop, int PenUID)
    {
      this.CityGrid[trashdrop.TileLocation.X / this.BlockSize, trashdrop.TileLocation.Y / this.BlockSize].AddPoop(trashdrop);
      if (trashdrop.trashtype != TrashType.AnimalPoop)
        return;
      for (int index = 0; index < this.ThingsInPens.Count; ++index)
      {
        if (this.ThingsInPens[index].PenUID == PenUID)
        {
          this.ThingsInPens[index].AddPoop(trashdrop);
          return;
        }
      }
      this.ThingsInPens.Add(new PenDirectory(PenUID));
      this.ThingsInPens[this.ThingsInPens.Count - 1].AddPoop(trashdrop);
    }

    public void RemovePoop(TrashDrop trashdrop, int PenUID)
    {
      this.CityGrid[trashdrop.TileLocation.X / this.BlockSize, trashdrop.TileLocation.Y / this.BlockSize].RemovePoop(trashdrop);
      if (trashdrop.trashtype != TrashType.AnimalPoop)
        return;
      for (int index = 0; index < this.ThingsInPens.Count; ++index)
      {
        if (this.ThingsInPens[index].PenUID == PenUID)
        {
          this.ThingsInPens[index].RemovePoop(trashdrop);
          return;
        }
      }
      this.ThingsInPens.Add(new PenDirectory(PenUID));
    }

    public void RemoveTrash(TrashDrop trashdrop) => this.CityGrid[trashdrop.TileLocation.X / this.BlockSize, trashdrop.TileLocation.Y / this.BlockSize].RemoveTrash(trashdrop);

    public CURSOR_ACTIONTYPE GetCursorThing(
      int LocationX,
      int LocationY,
      Player player)
    {
      if (Z_GameFlags.MouseIsOverAPanel)
        return CURSOR_ACTIONTYPE.Count;
      if (Z_GameFlags.HasCollidedWithAnimalsOnOrderSign(LocationX, LocationY))
        return CURSOR_ACTIONTYPE.InspectAnimalsOnOrder;
      if (LocationX > -1 && LocationY > -1 && (LocationX < player.prisonlayout.layout.BaseTileTypes.GetLength(0) && LocationY < player.prisonlayout.layout.BaseTileTypes.GetLength(1)))
      {
        if (TileMath.TileIsInWorld(LocationX, LocationY) && !TileMath.TileIsInBuildablePartOfWorld(LocationX, LocationY, true))
          return TileMath.IsBelowPark(LocationY) || !ZMapSetUp.IsSectorAdjacentForBuying(LocationX / TileMath.SectorSize, LocationY / TileMath.SectorSize) ? CURSOR_ACTIONTYPE.Count : CURSOR_ACTIONTYPE.LockedSector;
        if (player.prisonlayout.layout.BaseTileTypes[LocationX, LocationY].tiletype != TILETYPE.None)
          return TileData.IsThisACellBlock(player.prisonlayout.layout.BaseTileTypes[LocationX, LocationY].tiletype) ? CURSOR_ACTIONTYPE.SelectedPen : CURSOR_ACTIONTYPE.SelectedBuilding;
        if (TileData.IsThisACellBlock(player.prisonlayout.layout.FloorTileTypes[LocationX, LocationY].tiletype))
          return CURSOR_ACTIONTYPE.SelectedPen;
      }
      int index1 = LocationX / this.BlockSize;
      int index2 = LocationY / this.BlockSize;
      return index1 > -1 && index2 > -1 && (index1 < this.CityGrid.GetLength(0) && index2 < this.CityGrid.GetLength(1)) ? this.CityGrid[index1, index2].GetCursorThing(LocationX, LocationY) : CURSOR_ACTIONTYPE.Count;
    }

    public void AddImportantStructure(
      TILETYPE tiletype,
      int LocationX,
      int LocationY,
      int RotationColockWise)
    {
      if (!TileData.ThisIsAMeaningfullBuilding(tiletype))
        return;
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      int index1 = LocationX / this.BlockSize;
      int index2 = LocationY / this.BlockSize;
      if (TileData.IsAManagementOffice(tiletype))
        PointOffScreenManager.SetTaskPointerLocation(LocationX, LocationY);
      if (tiletype == TILETYPE.Logo)
        return;
      if (tileInfo.HasEntrance())
      {
        Vector2Int purchasingLocation = tileInfo.GetPurchasingLocation(RotationColockWise);
        this.CityGrid[(LocationX + purchasingLocation.X) / this.BlockSize, (LocationY + purchasingLocation.Y) / this.BlockSize].AddNodeOfImportance(LocationX + purchasingLocation.X, LocationY + purchasingLocation.Y, tiletype, new Vector2Int(purchasingLocation.X * -1, purchasingLocation.Y * -1), ref this.TotalPointsOfInterestByType);
      }
      else
        this.CityGrid[index1, index2].AddNodeOfImportance(LocationX, LocationY, tiletype, new Vector2Int(0, 0), ref this.TotalPointsOfInterestByType);
    }

    public bool OneOfTheseIsNearby(POINT_OF_INTEREST pointofinterest, Vector2Int CurrentTile)
    {
      int num1 = CurrentTile.X % this.BlockSize;
      int num2 = CurrentTile.Y % this.BlockSize;
      int index1 = CurrentTile.X / this.BlockSize;
      int index2 = CurrentTile.Y / this.BlockSize;
      if (this.CityGrid[index1, index2].HasOneOfThese(pointofinterest))
        return true;
      if (num1 < this.BlockSize / 2)
      {
        if (index1 > 0 && this.CityGrid[index1 - 1, index2].HasOneOfThese(pointofinterest))
          return true;
      }
      else if (num1 > this.BlockSize / 2 && index1 < this.CityGrid.GetLength(0) - 1 && this.CityGrid[index1 + 1, index2].HasOneOfThese(pointofinterest))
        return true;
      if (num2 < this.BlockSize / 2)
      {
        if (index2 > 0 && this.CityGrid[index1, index2 - 1].HasOneOfThese(pointofinterest))
          return true;
      }
      else if (num2 > this.BlockSize / 2 && index2 < this.CityGrid.GetLength(1) - 1 && this.CityGrid[index1, index2 + 1].HasOneOfThese(pointofinterest))
        return true;
      return false;
    }

    public bool TryAndGetToThis(POINT_OF_INTEREST pointofinterest, PathNavigator pathnavigator)
    {
      int num1 = pathnavigator.CurrentTile.X % this.BlockSize;
      int num2 = pathnavigator.CurrentTile.Y % this.BlockSize;
      int index1 = pathnavigator.CurrentTile.X / this.BlockSize;
      int index2 = pathnavigator.CurrentTile.Y / this.BlockSize;
      if (this.CityGrid[index1, index2].TryAndGetToThis(pointofinterest, pathnavigator))
        return true;
      if (num1 < this.BlockSize / 2)
      {
        if (index1 > 0 && this.CityGrid[index1 - 1, index2].TryAndGetToThis(pointofinterest, pathnavigator))
          return true;
      }
      else if (num1 > this.BlockSize / 2 && index1 < this.CityGrid.GetLength(0) - 1 && this.CityGrid[index1 + 1, index2].TryAndGetToThis(pointofinterest, pathnavigator))
        return true;
      if (num2 < this.BlockSize / 2)
      {
        if (index2 > 0 && this.CityGrid[index1, index2 - 1].TryAndGetToThis(pointofinterest, pathnavigator))
          return true;
      }
      else if (num2 > this.BlockSize / 2 && index2 < this.CityGrid.GetLength(1) - 1 && this.CityGrid[index1, index2 + 1].TryAndGetToThis(pointofinterest, pathnavigator))
        return true;
      return false;
    }

    public void RemoveImportantStructure(
      TILETYPE tiletype,
      int LocationX,
      int LocationY,
      int RotationColockWise)
    {
      if (!TileData.ThisIsAMeaningfullBuilding(tiletype))
        return;
      TileInfo tileInfo = TileData.GetTileInfo(tiletype);
      int index1 = LocationX / this.BlockSize;
      int index2 = LocationY / this.BlockSize;
      if (tiletype == TILETYPE.Logo || tiletype == TILETYPE.ZooEntrance_Modern || (tiletype == TILETYPE.ZooEntrance_Deer || tiletype == TILETYPE.ZooEntrance_Cliff))
        return;
      if (tileInfo.HasEntrance())
      {
        Vector2Int purchasingLocation = tileInfo.GetPurchasingLocation(RotationColockWise);
        this.CityGrid[(LocationX + purchasingLocation.X) / this.BlockSize, (LocationY + purchasingLocation.Y) / this.BlockSize].RemoveNodeOfImportance(LocationX + purchasingLocation.X, LocationY + purchasingLocation.Y, tiletype, new Vector2Int(purchasingLocation.X * -1, purchasingLocation.Y * -1), ref this.TotalPointsOfInterestByType);
      }
      else
        this.CityGrid[index1, index2].RemoveNodeOfImportance(LocationX, LocationY, tiletype, new Vector2Int(0, 0), ref this.TotalPointsOfInterestByType);
    }
  }
}
