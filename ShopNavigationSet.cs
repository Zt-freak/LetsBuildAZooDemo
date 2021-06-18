// Decompiled with JetBrains decompiler
// Type: TinyZoo.ShopNavigationSet
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo
{
  internal class ShopNavigationSet
  {
    public List<Vector2Int> ShopEntrances = new List<Vector2Int>();
    public Vector2Int PurchasingLocation;
    public Vector2Int RootLocation;
    public Vector2Int InternalInteractionPoint;
    public TILETYPE buildingtype;
    public ShopEntry Ref_ShopEntry;
    public bool CustomerCanLeaveNavToReachPurchasePoint;
    public SatisfactionType satisfactiontype = SatisfactionType.Count;

    public ShopNavigationSet(
      Vector2Int _Location,
      int RotationClockwise,
      TILETYPE _buildingtype,
      ShopEntry shopentry)
    {
      this.Ref_ShopEntry = shopentry;
      this.buildingtype = _buildingtype;
      this.RootLocation = new Vector2Int(_Location);
      this.ShopEntrances = TileData.GetTileInfo(_buildingtype).GetEntrances(RotationClockwise);
      this.CustomerCanLeaveNavToReachPurchasePoint = TileData.GetTileInfo(_buildingtype).CustomerCanLeaveNavToReachPurchasePoint;
      this.PurchasingLocation = _buildingtype == TILETYPE.Nursery || this.buildingtype == TILETYPE.DNABuilding || (this.buildingtype == TILETYPE.MeatProcessor || this.buildingtype == TILETYPE.Slaughterhouse) || (this.buildingtype == TILETYPE.FarmProcessor || this.buildingtype == TILETYPE.RecyclingCenter || this.buildingtype == TILETYPE.AnimalRehabilitationBuilding) ? new Vector2Int() : TileData.GetTileInfo(_buildingtype).GetPurchasingLocation(RotationClockwise);
      if (TileData.IsThisABench(this.buildingtype))
      {
        switch (RotationClockwise)
        {
          case 0:
            this.InternalInteractionPoint = new Vector2Int(0, -1);
            break;
          case 1:
            this.InternalInteractionPoint = new Vector2Int(-1, 0);
            break;
          case 2:
            this.InternalInteractionPoint = new Vector2Int(0, 1);
            break;
          default:
            this.InternalInteractionPoint = new Vector2Int(1, 0);
            break;
        }
      }
      if (this.ShopEntrances == null)
        return;
      for (int index = 0; index < this.ShopEntrances.Count; ++index)
        this.ShopEntrances[index] += _Location;
    }

    public int GetQueueAndCustomers() => this.Ref_ShopEntry.people_usingShop.Count + this.Ref_ShopEntry.people_walkingTo_SHOP.Count;

    public Vector2Int GetShopTarget(Vector2Int ShopRootLocation)
    {
      if (this.PurchasingLocation != null)
        return new Vector2Int(this.PurchasingLocation.X + ShopRootLocation.X, this.PurchasingLocation.Y + ShopRootLocation.Y);
      if (this.ShopEntrances == null || this.ShopEntrances.Count <= 0)
        return (Vector2Int) null;
      int index = Game1.Rnd.Next(0, this.ShopEntrances.Count);
      return new Vector2Int(this.ShopEntrances[index].X + ShopRootLocation.X, this.ShopEntrances[index].Y + ShopRootLocation.Y);
    }
  }
}
