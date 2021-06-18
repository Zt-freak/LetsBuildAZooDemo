// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir._Factories.AdditionalFactoryProduct
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using TinyZoo.Z_BalanceSystems.ProductionLines;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir._Factories
{
  internal class AdditionalFactoryProduct
  {
    public AnimalFoodType animalfoodtype;
    public float TotalCompletedProductsHeld;
    public int TotalOutOnDelivery;

    public AdditionalFactoryProduct(AnimalFoodType _animalfoodtype) => this.animalfoodtype = _animalfoodtype;

    public void StartNewDay(int ShopUID)
    {
      int completedProductsHeld = (int) this.TotalCompletedProductsHeld;
      if (completedProductsHeld <= 0)
        return;
      if (ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype] == null)
        ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype] = new TotalsAndBuildings();
      ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype].AddStockWithUID(ShopUID, completedProductsHeld);
    }

    public void AddProduct(float UnitsHarvested, int ShopUID)
    {
      this.TotalCompletedProductsHeld += UnitsHarvested;
      if ((double) this.TotalCompletedProductsHeld <= 0.0)
        return;
      if (ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype] == null)
        ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype] = new TotalsAndBuildings();
      ProductionLineCalc.totalsAndBuildings[(int) this.animalfoodtype].AddStockWithUID(ShopUID, (int) UnitsHarvested);
    }

    public int TryToCollectCompletedProducts(int IcanCarryThisMuch = 1)
    {
      int completedProductsHeld = (int) this.TotalCompletedProductsHeld;
      if (this.TotalOutOnDelivery + IcanCarryThisMuch <= completedProductsHeld)
      {
        this.TotalOutOnDelivery += IcanCarryThisMuch;
        return IcanCarryThisMuch;
      }
      if (this.TotalOutOnDelivery >= completedProductsHeld)
        return 0;
      int num = completedProductsHeld - this.TotalOutOnDelivery;
      if (num > IcanCarryThisMuch)
        throw new Exception("WHY DO I SUCK AT MATH");
      this.TotalOutOnDelivery += num;
      return num;
    }

    public int CompletedProductsFromHereDeliveredToOtherLocation(int IWasCarryingThisMuch)
    {
      this.TotalCompletedProductsHeld -= (float) IWasCarryingThisMuch;
      this.TotalOutOnDelivery -= IWasCarryingThisMuch;
      if ((double) this.TotalCompletedProductsHeld >= 0.0)
        return IWasCarryingThisMuch;
      IWasCarryingThisMuch += (int) this.TotalCompletedProductsHeld;
      this.TotalCompletedProductsHeld = 0.0f;
      return IWasCarryingThisMuch;
    }

    public AdditionalFactoryProduct(Reader reader)
    {
      int _out = 0;
      int num1 = (int) reader.ReadInt("c", ref _out);
      this.animalfoodtype = (AnimalFoodType) _out;
      int num2 = (int) reader.ReadFloat("c", ref this.TotalCompletedProductsHeld);
    }

    public void SavedditionalFactoryProduct(Writer writer)
    {
      writer.WriteInt("c", (int) this.animalfoodtype);
      writer.WriteFloat("c", this.TotalCompletedProductsHeld);
    }
  }
}
