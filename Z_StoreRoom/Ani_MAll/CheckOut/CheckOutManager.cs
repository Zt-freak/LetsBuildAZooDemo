// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut.CheckOutManager
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir;
using TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut.BottomBar;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThings;
using TinyZoo.Z_StoreRoom.Ani_MAll.ThanksFor;

namespace TinyZoo.Z_StoreRoom.Ani_MAll.CheckOut
{
  internal class CheckOutManager
  {
    private List<ThingToBuy> buythese;
    private BottomBarManager bottombar;
    private ThingsInShopManager REF_thingsinshop;
    public ThanksForSHppingManager thanks;

    public CheckOutManager(ThingsInShopManager thingsinshop)
    {
      this.REF_thingsinshop = thingsinshop;
      this.bottombar = new BottomBarManager(thingsinshop.GetTotalCost());
      this.buythese = new List<ThingToBuy>();
      int num = 0;
      for (int index = 0; index < thingsinshop.productentry.Count; ++index)
      {
        if (thingsinshop.productentry[index].stocknumber.Value > 0)
        {
          this.buythese.Add(new ThingToBuy(thingsinshop.productentry[index], num));
          this.buythese[num].Location.Y = 100f * Sengine.ScreenRatioUpwardsMultiplier.Y;
          ++num;
        }
      }
    }

    public bool UpdateCheckOutManager(
      Player player,
      float DeltaTime,
      ThingsInShopManager thingsinshop)
    {
      if (this.thanks != null)
        return false;
      for (int index = this.buythese.Count - 1; index > -1; --index)
      {
        if (this.buythese[index].UpdareThingToBuy(Vector2.Zero, player, DeltaTime) && this.buythese[index].WasRemove)
        {
          thingsinshop.RemoveThis(this.buythese[index].animalfoodtype, true);
          this.buythese.RemoveAt(index);
          this.bottombar = new BottomBarManager(thingsinshop.GetTotalCost());
        }
      }
      for (int index = 0; index < this.buythese.Count; ++index)
        this.buythese[index].UpdateLocation(DeltaTime, index);
      if (!this.bottombar.UpdateBottomBarManager(player, DeltaTime))
        return false;
      if (player.Stats.SpendCash(this.REF_thingsinshop.GetTotalCost(), SpendingCashOnThis.AnimalFood, player))
      {
        for (int index = 0; index < this.REF_thingsinshop.productentry.Count; ++index)
        {
          if (this.REF_thingsinshop.productentry[index].stocknumber.Value > 0)
            player.storerooms.OrderAThing(this.REF_thingsinshop.productentry[index].animalFoodType, this.REF_thingsinshop.productentry[index].stocknumber.Value, this.REF_thingsinshop.productentry[index].DeleiveryTime);
        }
        this.thanks = new ThanksForSHppingManager();
      }
      return true;
    }

    public void DrawCheckOutManager()
    {
      if (this.thanks != null)
      {
        this.thanks.DrawThanksForSHppingManager();
      }
      else
      {
        for (int index = 0; index < this.buythese.Count; ++index)
          this.buythese[index].DrawThingToBuy(Vector2.Zero);
        this.bottombar.DrawBottomBarManager();
      }
    }
  }
}
