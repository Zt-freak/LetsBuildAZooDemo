// Decompiled with JetBrains decompiler
// Type: TinyZoo.ShopNavigation
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo
{
  internal class ShopNavigation
  {
    internal static List<ShopNavigationSet> shopnavigationsets = new List<ShopNavigationSet>();
    internal static ShopNavCollection[] shopsbytype = new ShopNavCollection[19];

    internal static void ResetShopNavigation()
    {
      ShopNavigation.shopsbytype = new ShopNavCollection[19];
      ShopNavigation.shopnavigationsets = new List<ShopNavigationSet>();
    }

    internal static void AddShop(
      Vector2Int Loction,
      TILETYPE thingYouBuilt,
      int RotationClockWise,
      ShopEntry shopentry = null)
    {
      ShopNavigationSet shopn = new ShopNavigationSet(Loction, RotationClockWise, thingYouBuilt, shopentry);
      ShopNavigation.shopnavigationsets.Add(shopn);
      switch (TileData.GetTileInfo(thingYouBuilt).categorytype)
      {
        case CATEGORYTYPE.Shops:
          if (TileData.IsForThirst(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[2] == null)
              ShopNavigation.shopsbytype[2] = new ShopNavCollection();
            ShopNavigation.shopsbytype[2].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.Thirst;
            break;
          }
          if (TileData.IsForSouvenir(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[11] == null)
              ShopNavigation.shopsbytype[11] = new ShopNavCollection();
            ShopNavigation.shopsbytype[11].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.Souvenirs;
            break;
          }
          if (TileData.IsForFood(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[1] == null)
              ShopNavigation.shopsbytype[1] = new ShopNavCollection();
            ShopNavigation.shopsbytype[1].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.Hunger;
            break;
          }
          if (TileData.IsCooling(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[8] == null)
              ShopNavigation.shopsbytype[8] = new ShopNavCollection();
            ShopNavigation.shopsbytype[8].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.IceCream;
            break;
          }
          break;
        case CATEGORYTYPE.Amenities:
          if (TileData.IsThisaToilet(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[3] == null)
              ShopNavigation.shopsbytype[3] = new ShopNavCollection();
            ShopNavigation.shopsbytype[3].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.Bathroom;
            break;
          }
          goto default;
        case CATEGORYTYPE.Benches:
          if (ShopNavigation.shopsbytype[0] == null)
            ShopNavigation.shopsbytype[0] = new ShopNavCollection();
          ShopNavigation.shopsbytype[0].AddBuiling(shopn);
          shopn.satisfactiontype = SatisfactionType.Energy;
          break;
        default:
          if (TileData.IsThisAnATM(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[18] == null)
              ShopNavigation.shopsbytype[18] = new ShopNavCollection();
            ShopNavigation.shopsbytype[18].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.GetCash;
            break;
          }
          if (TileData.IsThisABin(thingYouBuilt))
          {
            if (ShopNavigation.shopsbytype[14] == null)
              ShopNavigation.shopsbytype[14] = new ShopNavCollection();
            ShopNavigation.shopsbytype[14].AddBuiling(shopn);
            shopn.satisfactiontype = SatisfactionType.Bin;
            break;
          }
          break;
      }
      int satisfactiontype = (int) shopn.satisfactiontype;
    }

    internal static Vector2Int GetNearestSpeificShop(
      int CashHeld,
      SatisfactionType shophunt,
      Vector2Int Personlocation,
      Vector2Int LastSHopTriedToGoTo,
      List<Vector2Int> ShopsUsed)
    {
      return ShopNavigation.shopsbytype[(int) shophunt] != null ? ShopNavigation.shopsbytype[(int) shophunt].GetNearestSpeificShop(CashHeld, Personlocation, LastSHopTriedToGoTo) : (Vector2Int) null;
    }

    internal static Vector2Int TryToGoToNerestSpecificShop(
      int CashHeld,
      SatisfactionType shophunt,
      PathNavigator pathnavigator,
      List<Vector2Int> ShopsUsed,
      ref ShopEntry FOUNDTHIS_ShopEntry,
      List<int> ShopsUsed_UID,
      ref Vector2Int InternalBuildingOffset,
      out bool WillGoOffGridToReachTarget)
    {
      WillGoOffGridToReachTarget = false;
      return ShopNavigation.shopsbytype[(int) shophunt] != null ? ShopNavigation.shopsbytype[(int) shophunt].TryToGoToNerestSpecificShop(CashHeld, pathnavigator, ShopsUsed, ShopsUsed_UID, ref FOUNDTHIS_ShopEntry, ref InternalBuildingOffset, shophunt, out WillGoOffGridToReachTarget) : (Vector2Int) null;
    }

    internal static TILETYPE GetShopTypeThatIsHere(Vector2Int ShopRoot)
    {
      for (int index = ShopNavigation.shopnavigationsets.Count - 1; index > -1; --index)
      {
        if (ShopNavigation.shopnavigationsets[index].RootLocation.X + ShopNavigation.shopnavigationsets[index].PurchasingLocation.X == ShopRoot.X && ShopNavigation.shopnavigationsets[index].RootLocation.Y + ShopNavigation.shopnavigationsets[index].PurchasingLocation.Y == ShopRoot.Y)
          return ShopNavigation.shopnavigationsets[index].buildingtype;
      }
      return TILETYPE.None;
    }

    internal static Vector2Int GetAnyUnusedShopLocation(
      out Vector2Int ShopRoot,
      List<Vector2Int> BlockThese)
    {
      ShopRoot = (Vector2Int) null;
      if (ShopNavigation.shopnavigationsets.Count > 0)
      {
        List<int> intList = new List<int>();
        for (int index1 = ShopNavigation.shopnavigationsets.Count - 1; index1 > -1; --index1)
        {
          bool flag = false;
          for (int index2 = BlockThese.Count - 1; index2 > -1; --index2)
          {
            if (ShopNavigation.shopnavigationsets[index1].RootLocation.CompareMatches(BlockThese[index2]))
              flag = true;
          }
          if (!flag)
            intList.Add(index1);
        }
        if (intList.Count > 0)
        {
          int index = intList[Game1.Rnd.Next(0, intList.Count)];
          ShopRoot = new Vector2Int(ShopNavigation.shopnavigationsets[index].RootLocation);
          return ShopNavigation.shopnavigationsets[index].GetShopTarget(ShopRoot);
        }
      }
      return (Vector2Int) null;
    }

    internal static Vector2Int GetShopTarget(out Vector2Int ShopRoot)
    {
      ShopRoot = (Vector2Int) null;
      if (ShopNavigation.shopnavigationsets.Count <= 0)
        return (Vector2Int) null;
      int index = Game1.Rnd.Next(0, ShopNavigation.shopnavigationsets.Count);
      ShopRoot = new Vector2Int(ShopNavigation.shopnavigationsets[index].RootLocation);
      return ShopNavigation.shopnavigationsets[index].GetShopTarget(ShopRoot);
    }

    internal static void RemoveShop(Vector2Int Loction, TILETYPE thingYouBuilt)
    {
      for (int index = ShopNavigation.shopnavigationsets.Count - 1; index > -1; --index)
      {
        if (ShopNavigation.shopnavigationsets[index].RootLocation.CompareMatches(Loction))
        {
          if (ShopNavigation.shopnavigationsets[index].satisfactiontype != SatisfactionType.Count)
            ShopNavigation.shopsbytype[(int) ShopNavigation.shopnavigationsets[index].satisfactiontype].RemoveThis(ShopNavigation.shopnavigationsets[index]);
          ShopNavigation.shopnavigationsets.RemoveAt(index);
        }
      }
    }
  }
}
