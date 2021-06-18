// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Commodities.WarehouseStock
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;

namespace TinyZoo.PlayerDir.Commodities
{
  internal class WarehouseStock
  {
    private int TotalStored;
    public int SellValue;

    public WarehouseStock() => this.TotalStored = -1;

    public void AddStock(int StockToAdd)
    {
      if (this.TotalStored < 0)
        this.TotalStored = 0;
      this.TotalStored += StockToAdd;
    }

    public int GetTotalStored() => this.TotalStored;

    public void SaveWarehouseStock(Writer writer)
    {
      writer.WriteInt("f", this.TotalStored);
      writer.WriteInt("f", this.SellValue);
    }

    public WarehouseStock(Reader reader)
    {
      int num1 = (int) reader.ReadInt("f", ref this.TotalStored);
      int num2 = (int) reader.ReadInt("f", ref this.SellValue);
    }
  }
}
