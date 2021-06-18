// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopDailyRecord
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopDailyRecord
  {
    private List<ShopPurchaseRecord> Records;
    private int Day;

    public ShopDailyRecord(int _Day, int Cost, int IngredientsCost)
    {
      this.Day = _Day;
      this.Records = new List<ShopPurchaseRecord>();
      this.Records.Add(new ShopPurchaseRecord(Cost, IngredientsCost));
    }

    public void LeftQueueWithoutPaying() => this.Records[this.Records.Count - 1].LeftQueueWithoutPaying();

    public int GetCost() => this.Records[this.Records.Count - 1].SellCost;

    public int GetStockCost() => this.Records[this.Records.Count - 1].IngredientsCost;

    public void ChangeCost(int SellCost, int IngredientsCost) => this.Records.Add(new ShopPurchaseRecord(SellCost, IngredientsCost));

    public void MadePurchase() => ++this.Records[this.Records.Count - 1].NumberPurchased;

    public int GetProfits()
    {
      int num = 0;
      for (int index = 0; index < this.Records.Count; ++index)
        num += (this.Records[index].SellCost - this.Records[index].IngredientsCost) * this.Records[index].NumberPurchased;
      return num;
    }

    public int GetAllCustomers()
    {
      int num = 0;
      for (int index = 0; index < this.Records.Count; ++index)
        num += this.Records[index].GetAllCustomersServed();
      return num;
    }

    public int GetPayingCustomers()
    {
      int num = 0;
      for (int index = 0; index < this.Records.Count; ++index)
        num += this.Records[index].NumberPurchased;
      return num;
    }

    public void SaveShopDailyRecord(Writer writer)
    {
      writer.WriteInt("d", this.Day);
      writer.WriteInt("d", this.Records.Count);
      for (int index = 0; index < this.Records.Count; ++index)
        this.Records[index].SaveShopPurchaseRecord(writer);
    }

    public ShopDailyRecord(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("d", ref this.Day);
      int _out = 0;
      int num2 = (int) reader.ReadInt("d", ref _out);
      this.Records = new List<ShopPurchaseRecord>();
      for (int index = 0; index < _out; ++index)
        this.Records.Add(new ShopPurchaseRecord(reader, VersionForLoad));
    }
  }
}
