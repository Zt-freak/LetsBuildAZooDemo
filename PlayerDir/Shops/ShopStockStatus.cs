// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Shops.ShopStockStatus
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.Z_AnimalsAndPeople.Sim_Person;
using TinyZoo.Z_ManageShop.Shop_Data;
using TinyZoo.Z_SummaryPopUps.Generic;

namespace TinyZoo.PlayerDir.Shops
{
  internal class ShopStockStatus
  {
    public bool Unlocked;
    public int MaximumStock;
    private int CurrentPrice;
    public List<float> StockSliderValues;
    public int StockPrice = -1;
    public int CurrentStock;
    public ShopStatEntry REF_shopentry;
    private List<ShopDailyRecord> dailyrecords;

    public ShopStockStatus(ShopStatEntry shopentry)
    {
      this.dailyrecords = new List<ShopDailyRecord>();
      this.StockSliderValues = new List<float>();
      this.StockSliderValues.Add(0.5f);
      this.REF_shopentry = shopentry;
      this.MaximumStock = 20;
      this.CurrentPrice = shopentry.IdealCost;
      this.CurrentStock = 20;
      this.dailyrecords = new List<ShopDailyRecord>();
      this.StockPrice = this.GetStockPrice();
    }

    public void LeftQueueWithoutPaying() => this.dailyrecords[this.dailyrecords.Count - 1].LeftQueueWithoutPaying();

    public int GetProfits(TimeSegmentType timesegment, RevType revenuetype)
    {
      int num1 = 0;
      int num2 = 0;
      if (timesegment == TimeSegmentType.Last7Days)
      {
        for (int index = this.dailyrecords.Count - 1; index > -1; --index)
        {
          switch (revenuetype)
          {
            case RevType.Profit:
              num2 += this.dailyrecords[index].GetProfits();
              break;
            case RevType.CustomersServed:
              num2 += this.dailyrecords[index].GetPayingCustomers();
              break;
            case RevType.AllCustomers:
              num2 += this.dailyrecords[index].GetAllCustomers();
              break;
            default:
              throw new Exception("oikjsfdg");
          }
          ++num1;
          if (num1 >= 7)
            return num2;
        }
      }
      return num2;
    }

    public void SetSliderValuer(int SliderIndex, float Value) => this.StockSliderValues[SliderIndex] = Value;

    public int GetStockPrice() => (int) ((double) this.REF_shopentry.MinStockCost + ((double) this.REF_shopentry.MaxSellToPublicCost * 0.5 - (double) this.REF_shopentry.MinStockCost) * (double) this.StockSliderValues[0]);

    public void SetStockPrice() => this.StockPrice = this.GetStockPrice();

    public void SetCurrentPrice(int _CurrentPrice) => this.CurrentPrice = _CurrentPrice;

    public int GetCurrentPrice() => this.CurrentPrice;

    public void StartNewDay(int Day, int Cost) => this.dailyrecords.Add(new ShopDailyRecord(Day, Cost, this.GetStockPrice()));

    public void CheckSetNewPrice()
    {
      if (this.CurrentPrice == this.dailyrecords[this.dailyrecords.Count - 1].GetCost() && this.GetStockPrice() == this.dailyrecords[this.dailyrecords.Count - 1].GetStockCost())
        return;
      this.dailyrecords[this.dailyrecords.Count - 1].ChangeCost(this.CurrentPrice, this.GetStockPrice());
    }

    public void AddNewPurchaseToLedger() => this.dailyrecords[this.dailyrecords.Count - 1].MadePurchase();

    public int GetDesirability(GeneralWellbeing wellbeing, CustomerNeeds customerneeds)
    {
      float num1 = this.CurrentPrice != 0 ? (this.CurrentPrice != this.REF_shopentry.IdealCost ? (this.CurrentPrice <= this.REF_shopentry.IdealCost ? (float) (int) ((double) ((1f - (float) this.CurrentPrice / (float) this.REF_shopentry.IdealCost) * 0.5f + 0.5f) * 100.0) : (float) (int) ((double) this.REF_shopentry.IdealCost / (double) this.CurrentPrice * 0.5 * 100.0)) : 50f) : 100f;
      NeedFulFillmentEntry needFulfillment = ShopData.GetNeedFulfillment(this.REF_shopentry.MainItemForSale, (ShopStockStatus) null);
      float num2 = 0.0f;
      if ((double) needFulfillment.SatisfactionModifiers[0] != 0.0)
        num2 += Math.Max(0.0f, customerneeds.CurrentWantValues[0] + needFulfillment.SatisfactionModifiers[0]) - customerneeds.CurrentWantValues[0];
      for (int index = 1; index < customerneeds.CurrentWantValues.Length; ++index)
      {
        if ((double) customerneeds.CurrentWantValues[index] > 0.0 && (double) needFulfillment.SatisfactionModifiers[index] < 0.0)
          num2 += Math.Min(-needFulfillment.SatisfactionModifiers[index], customerneeds.CurrentWantValues[index]);
      }
      return (int) (num1 + num2 * 100f);
    }

    public void SaveShopStockStatus(Writer writer)
    {
      writer.WriteInt("s", this.MaximumStock);
      writer.WriteInt("s", this.CurrentPrice);
      writer.WriteInt("s", this.CurrentStock);
      writer.WriteBool("s", this.Unlocked);
      writer.WriteInt("s", this.StockSliderValues.Count);
      for (int index = 0; index < this.StockSliderValues.Count; ++index)
        writer.WriteFloat("s", this.StockSliderValues[index]);
      writer.WriteInt("s", this.dailyrecords.Count);
      for (int index = 0; index < this.dailyrecords.Count; ++index)
        this.dailyrecords[index].SaveShopDailyRecord(writer);
    }

    public ShopStockStatus(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("s", ref this.MaximumStock);
      int num2 = (int) reader.ReadInt("s", ref this.CurrentPrice);
      int num3 = (int) reader.ReadInt("s", ref this.CurrentStock);
      int num4 = (int) reader.ReadBool("s", ref this.Unlocked);
      this.StockSliderValues = new List<float>();
      int _out1 = 0;
      int num5 = (int) reader.ReadInt("s", ref _out1);
      for (int index = 0; index < _out1; ++index)
      {
        float _out2 = 0.0f;
        int num6 = (int) reader.ReadFloat("s", ref _out2);
        this.StockSliderValues.Add(_out2);
      }
      int num7 = (int) reader.ReadInt("s", ref _out1);
      this.dailyrecords = new List<ShopDailyRecord>();
      for (int index = 0; index < _out1; ++index)
        this.dailyrecords.Add(new ShopDailyRecord(reader, VersionForLoad));
    }
  }
}
