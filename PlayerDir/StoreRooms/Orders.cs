// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.StoreRooms.Orders
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.StoreRooms
{
  internal class Orders
  {
    private List<OrderEntry> orders;

    public Orders() => this.orders = new List<OrderEntry>();

    public int GetDeliveryTime(AnimalFoodType thisfoodtype)
    {
      int num = -1;
      for (int index = 0; index < this.orders.Count; ++index)
      {
        if (this.orders[index].Item == thisfoodtype && (num == -1 || num > this.orders[index].DaysUntilDelivery))
          num = this.orders[index].DaysUntilDelivery;
      }
      return num;
    }

    public int GetTotalOfTheseOnOrder(AnimalFoodType foodtype)
    {
      int num = 0;
      for (int index = 0; index < this.orders.Count; ++index)
      {
        if (this.orders[index].Item == foodtype)
          num += this.orders[index].TotalOrdered;
      }
      return num;
    }

    public void StartNewDay(StoreRoomContents storerooms)
    {
      List<OrderEntry> orderEntryList = new List<OrderEntry>();
      for (int index = this.orders.Count - 1; index > -1; --index)
      {
        --this.orders[index].DaysUntilDelivery;
        if (this.orders[index].DaysUntilDelivery <= 0)
        {
          orderEntryList.Add(this.orders[index]);
          this.orders.RemoveAt(index);
        }
      }
      if (orderEntryList.Count <= 0)
        return;
      if (false)
      {
        int remainingSpace = storerooms.GetRemainingSpace();
        if (remainingSpace > 0)
        {
          for (int index = orderEntryList.Count - 1; index > -1; --index)
          {
            if (orderEntryList[index].TotalOrdered <= remainingSpace)
            {
              storerooms.AddStock(orderEntryList[index].Item, orderEntryList[index].TotalOrdered, AnimalFoodData.GetAnimalFoodInfo(orderEntryList[index].Item).ShelfLife);
              remainingSpace -= orderEntryList[index].TotalOrdered;
              orderEntryList.RemoveAt(index);
            }
            else if (remainingSpace > 0)
            {
              storerooms.AddStock(orderEntryList[index].Item, remainingSpace, AnimalFoodData.GetAnimalFoodInfo(orderEntryList[index].Item).ShelfLife);
              orderEntryList[index].TotalOrdered -= remainingSpace;
              break;
            }
          }
        }
      }
      else
      {
        for (int index = orderEntryList.Count - 1; index > -1; --index)
          storerooms.AddStock(orderEntryList[index].Item, orderEntryList[index].TotalOrdered, AnimalFoodData.GetAnimalFoodInfo(orderEntryList[index].Item).ShelfLife);
        orderEntryList = new List<OrderEntry>();
      }
      int count = orderEntryList.Count;
    }

    public void AddNewORder(OrderEntry orderentry)
    {
      for (int index = 0; index < this.orders.Count; ++index)
      {
        if (this.orders[index].DaysUntilDelivery == orderentry.DaysUntilDelivery && this.orders[index].Item == orderentry.Item)
        {
          this.orders[index].TotalOrdered += orderentry.TotalOrdered;
          return;
        }
      }
      this.orders.Add(orderentry);
    }

    public string GetStuffOnOrderString(out string RightString)
    {
      string str = "";
      RightString = "";
      for (int index = 0; index < this.orders.Count; ++index)
      {
        str = str + AnimalFoodData.GetAnimalFoodTypeToString(this.orders[index].Item) + " x" + (object) this.orders[index].TotalOrdered + "~";
        RightString = RightString + (object) this.orders[index].DaysUntilDelivery + "d~";
      }
      if (str == "")
        str = "No Items On Order";
      return str;
    }

    public void SaveOrders(Writer writer)
    {
      writer.WriteInt("s", this.orders.Count);
      for (int index = 0; index < this.orders.Count; ++index)
        this.orders[index].SaveOrderEntry(writer);
    }

    public Orders(Reader reader)
    {
      this.orders = new List<OrderEntry>();
      int _out = 0;
      int num = (int) reader.ReadInt("s", ref _out);
      for (int index = 0; index < _out; ++index)
        this.orders.Add(new OrderEntry(reader));
    }
  }
}
