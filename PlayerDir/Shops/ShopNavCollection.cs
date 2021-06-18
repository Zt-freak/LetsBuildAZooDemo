// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopNavCollection
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PathFinding;
using TinyZoo.Z_SummaryPopUps.People.Customer.SatisfactionBars;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopNavCollection
  {
    public List<ShopNavigationSet> shops;

    public ShopNavCollection() => this.shops = new List<ShopNavigationSet>();

    public void AddBuiling(ShopNavigationSet shopn) => this.shops.Add(shopn);

    public void RemoveThis(ShopNavigationSet remover)
    {
      for (int index = this.shops.Count - 1; index > -1; --index)
      {
        if (this.shops[index] == remover)
        {
          this.shops.RemoveAt(index);
          return;
        }
      }
      throw new Exception("COULD NOT FIND");
    }

    public Vector2Int TryToGoToNerestSpecificShop(
      int CashHeld,
      PathNavigator pathnavigator,
      List<Vector2Int> SkipThese,
      List<int> ShopsUsed_UID,
      ref ShopEntry FOUNDTHIS_ShopEntry,
      ref Vector2Int InternalBuildingOffset,
      SatisfactionType shophunt,
      out bool WillGoOffGridToReachTarget,
      int MaxSearchRange = -1)
    {
      WillGoOffGridToReachTarget = false;
      if (MaxSearchRange > 0)
        MaxSearchRange *= MaxSearchRange;
      List<int> intList = new List<int>();
      bool flag1 = false;
      if (shophunt == SatisfactionType.Energy || shophunt == SatisfactionType.Bathroom)
        flag1 = true;
      if (SkipThese == null || SkipThese.Count == 0)
      {
        for (int index = this.shops.Count - 1; index > -1; --index)
          intList.Add(index);
      }
      else
      {
        for (int index1 = this.shops.Count - 1; index1 > -1; --index1)
        {
          bool flag2 = false;
          if (this.shops[index1].Ref_ShopEntry != null)
          {
            for (int index2 = 0; index2 < ShopsUsed_UID.Count; ++index2)
            {
              if (ShopsUsed_UID[index2] == this.shops[index1].Ref_ShopEntry.ShopUID)
                flag2 = true;
            }
          }
          if (shophunt == SatisfactionType.Energy || shophunt == SatisfactionType.Bathroom)
          {
            flag1 = true;
            for (int index2 = 0; index2 < SkipThese.Count; ++index2)
            {
              if (SkipThese[index2].CompareMatches(this.shops[index1].RootLocation))
                flag2 = true;
            }
            if (FOUNDTHIS_ShopEntry.shoptype == ShopEntryType.Bench)
            {
              if (this.shops[index1].Ref_ShopEntry.people_usingShop.Count > 0)
                flag2 = true;
            }
            else
            {
              int shoptype = (int) FOUNDTHIS_ShopEntry.shoptype;
            }
          }
          if (!flag2)
            intList.Add(index1);
        }
      }
      List<Vector3Int> vector3IntList = new List<Vector3Int>();
      for (int index = 0; index < intList.Count; ++index)
      {
        int num = this.shops[intList[index]].RootLocation.LengthSquaredInt(pathnavigator.CurrentTile);
        if (num < MaxSearchRange || MaxSearchRange < 0)
        {
          if (flag1)
            vector3IntList.Add(new Vector3Int(intList[index], this.shops[intList[index]].GetQueueAndCustomers(), num));
          else
            vector3IntList.Add(new Vector3Int(intList[index], num, 0));
        }
      }
      vector3IntList.Sort(new Comparison<Vector3Int>(this.SortIndexesAndDisances));
      for (int index = 0; index < vector3IntList.Count; ++index)
      {
        if (this.shops[vector3IntList[index].X].PurchasingLocation != null && !this.shops[vector3IntList[index].X].CustomerCanLeaveNavToReachPurchasePoint)
        {
          if (pathnavigator.TryToGoHereSquare(this.shops[vector3IntList[index].X].RootLocation + this.shops[vector3IntList[index].X].PurchasingLocation, GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
          {
            FOUNDTHIS_ShopEntry = this.shops[vector3IntList[index].X].Ref_ShopEntry;
            if (this.shops[intList[index]].InternalInteractionPoint != null)
              InternalBuildingOffset = new Vector2Int(this.shops[vector3IntList[index].X].InternalInteractionPoint);
            return this.shops[vector3IntList[index].X].RootLocation;
          }
        }
        else if (pathnavigator.TryToGoHereSquare(this.shops[intList[vector3IntList[index].X]].ShopEntrances[0], GameFlags.pathset, hierarchy: Z_GameFlags.pathfinder.hierachicalPathFind))
        {
          WillGoOffGridToReachTarget = this.shops[vector3IntList[index].X].CustomerCanLeaveNavToReachPurchasePoint;
          FOUNDTHIS_ShopEntry = this.shops[vector3IntList[index].X].Ref_ShopEntry;
          return this.shops[vector3IntList[index].X].RootLocation;
        }
      }
      return (Vector2Int) null;
    }

    private int SortIndexesAndDisances(Vector3Int a, Vector3Int b)
    {
      if (a.Y < b.Y)
        return -1;
      if (a.Y > b.Y)
        return 1;
      if (a.Z < b.Z)
        return -1;
      return a.Z > b.Z ? 1 : 0;
    }

    public Vector2Int GetNearestSpeificShop(
      int CashHeld,
      Vector2Int Personlocation,
      Vector2Int SkipThis)
    {
      int num1 = -1;
      int index1 = -1;
      for (int index2 = this.shops.Count - 1; index2 > -1; --index2)
      {
        if (SkipThis == null || !SkipThis.CompareMatches(this.shops[index2].PurchasingLocation))
        {
          int num2 = this.shops[index2].RootLocation.LengthSquaredInt(Personlocation);
          if (num2 < num1 || num1 == -1)
          {
            num1 = num2;
            index1 = index2;
          }
        }
      }
      return index1 > -1 ? this.shops[index1].PurchasingLocation : (Vector2Int) null;
    }
  }
}
