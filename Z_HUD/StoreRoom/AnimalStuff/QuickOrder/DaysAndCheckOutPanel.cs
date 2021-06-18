// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder.DaysAndCheckOutPanel
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_GenericUI;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_SummaryPopUps.People.Customer;

namespace TinyZoo.Z_HUD.StoreRoom.AnimalStuff.QuickOrder
{
  internal class DaysAndCheckOutPanel
  {
    private CustomerFrame customerframe;
    public Vector2 Location;
    private PlaceOrder placeorderbutton;
    private QuickOrderDisplayString DaysLeft;
    private AnimalFoodType orderThis;

    public DaysAndCheckOutPanel(
      float BaseScale,
      Vector2 OtherScale,
      TempAnimalInfo animalinfo,
      Player player)
    {
      Vector2 defaultBuffer = new UIScaleHelper(BaseScale).DefaultBuffer;
      this.orderThis = animalinfo.CriticalFood;
      this.customerframe = new CustomerFrame(OtherScale, true, BaseScale);
      this.DaysLeft = new QuickOrderDisplayString(animalinfo, BaseScale, player);
      this.DaysLeft.location.Y -= this.customerframe.VSCale.Y * 0.5f;
      this.DaysLeft.location.Y += defaultBuffer.Y;
      this.placeorderbutton = new PlaceOrder(BaseScale, animalinfo.CriticalFood);
    }

    public bool UpdateDaysAndCheckOutPanel(
      Player player,
      Vector2 Offset,
      float DeltaTime,
      int BasketCount)
    {
      Offset += this.Location;
      this.DaysLeft.UpdateQuickOrderDisplayString(BasketCount, Offset, player, DeltaTime);
      if (this.DaysLeft.BlockBuy() || !this.placeorderbutton.UpdatePlaceOrder(player, Offset, DeltaTime, BasketCount))
        return false;
      player.storerooms.OrderAThing(this.orderThis, BasketCount, AnimalFoodData.GetAnimalFoodInfo(this.orderThis).DeliveryTime);
      return true;
    }

    public void DrawDaysAndCheckOutPanel(Vector2 Offset, SpriteBatch spritebatch)
    {
      Offset += this.Location;
      this.customerframe.DrawCustomerFrame(Offset, spritebatch);
      this.DaysLeft.DrawQuickOrderDisplayString(Offset, spritebatch);
      if (this.DaysLeft.BlockBuy())
        return;
      this.placeorderbutton.DrawPlaceOrder(Offset, spritebatch);
    }
  }
}
