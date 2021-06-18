// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopPurchaseRecord
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopPurchaseRecord
  {
    public int SellCost;
    public int IngredientsCost;
    public int NumberPurchased;
    private int CustomersWhoLeft;

    public ShopPurchaseRecord(int _SellCost, int _IngredientsCost)
    {
      this.IngredientsCost = _IngredientsCost;
      this.SellCost = _SellCost;
    }

    public void LeftQueueWithoutPaying() => ++this.CustomersWhoLeft;

    public int GetCustomersServed() => this.NumberPurchased;

    public int GetAllCustomersServed() => this.CustomersWhoLeft + this.NumberPurchased;

    public void SaveShopPurchaseRecord(Writer writer)
    {
      writer.WriteInt("c", this.SellCost);
      writer.WriteInt("c", this.NumberPurchased);
      writer.WriteInt("c", this.IngredientsCost);
      writer.WriteInt("c", this.CustomersWhoLeft);
    }

    public ShopPurchaseRecord(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("c", ref this.SellCost);
      int num2 = (int) reader.ReadInt("c", ref this.NumberPurchased);
      if (VersionForLoad <= 26)
        return;
      int num3 = (int) reader.ReadInt("c", ref this.IngredientsCost);
      int num4 = (int) reader.ReadInt("c", ref this.CustomersWhoLeft);
    }
  }
}
