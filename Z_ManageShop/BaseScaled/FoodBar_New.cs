// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_ManageShop.BaseScaled.FoodBar_New
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using TinyZoo.Z_ManageShop.AllShopSummary;
using TinyZoo.Z_ManageShop.RecipeView;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_ManageShop.BaseScaled
{
  internal class FoodBar_New
  {
    private List<ShopSummaryRow> shopsummaryows;
    private Vector2 Location;
    private CustomerFrame customerframe;
    public FoodViewManager foodviewstuff;
    private float BaseScale;
    public Vector2 Position;

    public FoodBar_New(Player player, float _BaseScale)
    {
      this.BaseScale = _BaseScale;
      this.shopsummaryows = new List<ShopSummaryRow>();
      Vector2 VscaleOfParent = new Vector2();
      float num = 0.0f;
      for (int index = 0; index < player.shopstatus.shopentries.Count; ++index)
      {
        this.shopsummaryows.Add(new ShopSummaryRow(player.shopstatus.shopentries[index], player, VscaleOfParent, this.BaseScale));
        this.shopsummaryows[index].Location.Y = num;
        this.shopsummaryows[index].Location.Y += 15f;
        this.shopsummaryows[index].Location.Y += (float) (((double) index + 0.5) * ((double) this.shopsummaryows[index].Height + 10.0 * (double) this.BaseScale));
      }
      this.foodviewstuff = new FoodViewManager(player, this.BaseScale);
      this.foodviewstuff.Position.Y += (float) (6.0 * -(double) this.BaseScale);
      this.Location.Y = -50f * this.BaseScale;
      this.customerframe = new CustomerFrame(new Vector2(910f, 205f) * this.BaseScale, BaseScale: this.BaseScale);
      this.customerframe.location.Y = 50f * this.BaseScale;
    }

    public Vector2 GetSize() => this.customerframe.VSCale;

    public void CheckAddToShopLedgerOnExit(Player player) => this.foodviewstuff.CheckAddToShopLedgerOnExit(player);

    public void UpdateFoodBar_New(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      out bool SwitchState)
    {
      Offset += this.Location;
      Offset += this.Position;
      this.foodviewstuff.UpdateFoodViewManager(player, Offset, DeltaTime, out SwitchState);
    }

    public void DrawFoorBar_New(Vector2 Offset)
    {
      Offset += this.Location;
      Offset += this.Position;
      this.customerframe.DrawCustomerFrame(Offset, AssetContainer.pointspritebatchTop05);
      int num = 0;
      while (num < this.shopsummaryows.Count)
        ++num;
      this.foodviewstuff.DrawFoodViewManager(Offset);
    }
  }
}
