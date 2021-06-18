// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.StoreRooms.StockEntry
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.Z_Notification;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.StoreRooms
{
  internal class StockEntry
  {
    public int ShelfLifeRemaining;
    private float TotalStock;
    public AnimalFoodType foodtype;

    public StockEntry(AnimalFoodType _foodtype, int TotalOrdered, int ShelfLife)
    {
      this.foodtype = _foodtype;
      this.TotalStock = (float) TotalOrdered;
      this.ShelfLifeRemaining = ShelfLife;
    }

    public float GetTotalStock() => this.TotalStock;

    public void ModifyStock(float AddThis, TinyZoo.PlayerDir.StoreRooms.StoreRooms storerooms, bool WasExpired)
    {
      if ((double) AddThis < 0.0 && !WasExpired)
        storerooms.TodaysUse[(int) this.foodtype] += -AddThis;
      this.TotalStock += AddThis;
    }

    public bool StartDay(Player player)
    {
      --this.ShelfLifeRemaining;
      if (this.ShelfLifeRemaining >= 0)
        return false;
      Z_NotificationManager.AddNotificationPackage(new NotificationPackage(Z_NotificationType.ExpiredAnimalFood, this.foodtype, this.TotalStock), player);
      return true;
    }

    public void SaveStockEntry(Writer writer)
    {
      writer.WriteInt("s", this.ShelfLifeRemaining);
      writer.WriteFloat("s", this.TotalStock);
      writer.WriteInt("s", (int) this.foodtype);
    }

    public StockEntry(Reader reader)
    {
      int num1 = (int) reader.ReadInt("s", ref this.ShelfLifeRemaining);
      int num2 = (int) reader.ReadFloat("s", ref this.TotalStock);
      int _out = 0;
      int num3 = (int) reader.ReadInt("s", ref _out);
      this.foodtype = (AnimalFoodType) _out;
    }
  }
}
