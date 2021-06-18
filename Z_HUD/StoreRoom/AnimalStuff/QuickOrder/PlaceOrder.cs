// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder.PlaceOrder
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SEngine;
using TinyZoo.PlayerDir;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_StoreRoom.Ani_MAll.ShopThing;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder
{
  internal class PlaceOrder
  {
    private AddToCart addtocartbutton;
    public Vector2 Location;
    private int CostPerUnit;
    private int WholeCost;
    private GameObject CostStrig;
    private int Total;
    private bool CanAfford;

    public PlaceOrder(float _BaseScale, AnimalFoodType foodtype)
    {
      this.CostPerUnit = AnimalFoodData.GetAnimalFoodInfo(foodtype).Cost;
      this.addtocartbutton = new AddToCart(_BaseScale, true, true);
      this.addtocartbutton.vLocation = new Vector2(0.0f, 83f * _BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y);
      this.CanAfford = false;
      this.CostStrig = new GameObject();
      this.CostStrig.scale = _BaseScale;
      this.CostStrig.SetAllColours(ColourData.Z_Cream);
      this.CostStrig.vLocation = this.addtocartbutton.vLocation;
      this.CostStrig.vLocation.Y -= 20f * _BaseScale * Sengine.ScreenRatioUpwardsMultiplier.Y;
      this.CanAfford = false;
      this.Total = -1;
      this.addtocartbutton.Disable();
    }

    public bool UpdatePlaceOrder(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      int TotalInBasket)
    {
      if (this.Total != TotalInBasket)
      {
        this.Total = TotalInBasket;
        this.WholeCost = TotalInBasket * this.CostPerUnit;
        if (player.Stats.GetCashHeld() >= this.WholeCost && TotalInBasket > 0)
        {
          this.CanAfford = true;
          this.addtocartbutton.Enable();
        }
        else
        {
          this.CanAfford = false;
          this.addtocartbutton.Disable();
        }
      }
      Offset += this.Location;
      return this.addtocartbutton.UpdateAddToCart(player, DeltaTime, Offset) && player.Stats.SpendCash(this.WholeCost, SpendingCashOnThis.AnimalFood, player);
    }

    public void DrawPlaceOrder(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      TextFunctions.DrawJustifiedText("$" + (object) this.WholeCost, this.CostStrig.scale, this.CostStrig.vLocation + Offset, this.CostStrig.GetColour(), this.CostStrig.fAlpha, AssetContainer.SpringFontX1AndHalf, spritebatch);
      this.addtocartbutton.DrawAddToCart(Offset, spritebatch);
    }
  }
}
