// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir._Factories.FactoryProductionLine
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Farms_;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BalanceSystems.Animals.Incineration;
using TinyZoo.Z_BalanceSystems.ProductionLines;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_Factories;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir._Factories
{
  internal class FactoryProductionLine
  {
    private int Stock;
    private bool IsManufacturing;
    public float EmployeeProductivityMultiplier;
    private int ShopUID;
    private int CompletedProducts;
    private int CompletedProductsInTransit;
    private int MinutesLeftAtStartOfNextDay;
    private bool IsPaused;
    private int MinutesLeftOnPause;
    private List<DeadAnimal> deadanimals;
    private List<DeadAnimal> deadanimalLeftOvers;
    private List<DeadAnimal> deadanimalLeftOversInTransit;
    private AnimalFoodType ProductMadeHere = AnimalFoodType.Count;
    public AdditionalFactoryProduct[] additionfactoryproducts;
    private bool HasMultipleProducts_ButNotDeadAnimal;
    private int TEMPTotalThingsMadeToday;
    private int TotalThingsMadeYesterday;
    private int FullProcessingTime;

    public TILETYPE factorytiletype { get; private set; }

    public FactoryProductionLine(TILETYPE building, int _ShopUID)
    {
      this.HasMultipleProducts_ButNotDeadAnimal = false;
      this.EmployeeProductivityMultiplier = 1f;
      this.ShopUID = _ShopUID;
      this.factorytiletype = building;
      this.HasMultipleProducts_ButNotDeadAnimal = this.factorytiletype == TILETYPE.Windmill;
      this.CheckUseDeadAnimals();
    }

    private void CheckUseDeadAnimals()
    {
      if (TileData.IsAMeatProcessingPlant(this.factorytiletype) || TileData.IsAnIncinerator(this.factorytiletype) || TileData.IsAVegetableProcessingPlant(this.factorytiletype))
      {
        this.deadanimals = new List<DeadAnimal>();
        if (TileData.IsAMeatProcessingPlant(this.factorytiletype))
          this.deadanimalLeftOvers = new List<DeadAnimal>();
        if (!TileData.IsAVegetableProcessingPlant(this.factorytiletype) && !TileData.IsAMeatProcessingPlant(this.factorytiletype))
          return;
        this.additionfactoryproducts = new AdditionalFactoryProduct[88];
      }
      else
      {
        if (this.factorytiletype != TILETYPE.Windmill)
          return;
        this.additionfactoryproducts = new AdditionalFactoryProduct[88];
      }
    }

    public void AddSmokeController(FactorySmoke smoke)
    {
    }

    public void AddDeadAnimal(DeadAnimal deadanimal)
    {
      this.deadanimals.Add(deadanimal);
      if (!this.IsManufacturing)
        this.StartManufacturing();
      this.Stock = this.deadanimals.Count;
    }

    public bool IsCurrentlyManufacturing() => this.IsManufacturing;

    public float GetPercentComplete(out string TimeLeft, out bool _IsPaused)
    {
      TimeLeft = "";
      _IsPaused = false;
      if (!this.IsManufacturing)
        return 0.0f;
      if (this.FullProcessingTime == 0)
        this.FullProcessingTime = this.deadanimals == null ? FactoryData.GetManufacturingTimeInMinutes(this.factorytiletype, this.EmployeeProductivityMultiplier) : (int) IncinerationCalculator.GetMinutesToProcess(this.deadanimals[0], this.EmployeeProductivityMultiplier, TileData.IsAnIncinerator(this.factorytiletype));
      float fullProcessingTime = (float) this.FullProcessingTime;
      if (!Z_GameFlags.ParkIsOpen())
      {
        if ((double) Z_GameFlags.DayTimer > (double) Z_GameFlags.ZooOpenTime_InSeconds + (double) Z_GameFlags.SecondsZooOpenPerDay)
        {
          TimeLeft = this.MinutesLeftAtStartOfNextDay >= 0 ? Z_GameFlags.GetTimeAsStringFomMinutes(this.MinutesLeftAtStartOfNextDay) : throw new Exception("The zoo has closed for the day, - this manufacturing should have already finished");
          return (float) (1.0 - (double) this.MinutesLeftAtStartOfNextDay / (double) fullProcessingTime);
        }
        if (this.MinutesLeftAtStartOfNextDay > 0)
        {
          int Minutes = this.MinutesLeftAtStartOfNextDay + Z_GameFlags.GetAbstractTimerToMinutes(Z_GameFlags.SecondsZooOpenPerDay);
          TimeLeft = Z_GameFlags.GetTimeAsStringFomMinutes(Minutes);
          return (float) (1.0 - (double) Minutes / (double) fullProcessingTime);
        }
        int Minutes1 = this.MinutesLeftAtStartOfNextDay + (int) Z_GameFlags.GetClosingTimeInMinutes();
        TimeLeft = Z_GameFlags.GetTimeAsStringFomMinutes(Minutes1);
        return (float) (1.0 - (double) Minutes1 / (double) fullProcessingTime);
      }
      if (this.MinutesLeftAtStartOfNextDay <= 0)
      {
        _IsPaused = true;
        int Minutes = Z_GameFlags.GetTimeUntilThisInMinutes(Z_GameFlags.GetClosingTime()) + this.MinutesLeftAtStartOfNextDay;
        if (Minutes <= 0)
          return 1f;
        TimeLeft = Z_GameFlags.GetTimeAsStringFomMinutes(Minutes);
        return (float) (1.0 - (double) Minutes / (double) fullProcessingTime);
      }
      int Minutes2 = Z_GameFlags.GetTimeUntilThisInMinutes(Z_GameFlags.GetClosingTime()) + this.MinutesLeftAtStartOfNextDay;
      TimeLeft = Z_GameFlags.GetTimeAsStringFomMinutes(Minutes2);
      return (float) (1.0 - (double) Minutes2 / (double) fullProcessingTime);
    }

    public void AddStock(int AddThis)
    {
      this.Stock += AddThis;
      if (this.IsManufacturing)
        return;
      this.StartManufacturing();
    }

    public int GetStockToDisplay() => this.IsManufacturing ? this.Stock - 1 : this.Stock;

    public DeadAnimal GetCurrentlyManufacturingDeadAnimal() => this.deadanimals != null && this.deadanimals.Count > 0 ? this.deadanimals[0] : (DeadAnimal) null;

    public List<DeadAnimal> GetDeadAnimalQueue() => this.deadanimals;

    public int GetCompletedProductCount() => this.CompletedProducts - this.CompletedProductsInTransit;

    public List<DeadAnimal> GetDeadAnimalsLeftOvers() => this.deadanimalLeftOvers;

    private void StartManufacturing()
    {
      if (this.IsManufacturing)
        return;
      this.IsManufacturing = true;
      float _Buid_MinutesRemaining = this.deadanimals == null ? (float) FactoryData.GetManufacturingTimeInMinutes(this.factorytiletype, this.EmployeeProductivityMultiplier) : (this.deadanimals[0].cropType == CROPTYPE.Count ? IncinerationCalculator.GetMinutesToProcess(this.deadanimals[0], this.EmployeeProductivityMultiplier, TileData.IsAnIncinerator(this.factorytiletype)) : (float) ProcessedVeg.GetVegetableProcessingTimePerUnitInMinutes(this.deadanimals[0].cropType));
      this.FullProcessingTime = (int) _Buid_MinutesRemaining;
      this.CheckEndTime(_Buid_MinutesRemaining);
    }

    private void CheckEndTime(float _Buid_MinutesRemaining)
    {
      int closingTimeInMinutes = (int) Z_GameFlags.GetClosingTimeInMinutes();
      int num1 = 0;
      if (!Z_GameFlags.ParkIsOpen() && (double) Z_GameFlags.DayTimer < (double) Z_GameFlags.ZooOpenTime_InSeconds)
        num1 = Z_GameFlags.GetTimeUntilThisInMinutes(Z_GameFlags.ZooOpenTime_InSeconds);
      int num2 = Z_GameFlags.GetTimeUntilThisInMinutesFromMinutes((float) closingTimeInMinutes);
      if (!Z_GameFlags.ParkIsOpen())
        num2 = (double) Z_GameFlags.DayTimer >= (double) Z_GameFlags.GetTimeThatParkOpensInMorning_Seconds() ? 0 : (int) Z_GameFlags.GetInGameSecondsToMinutes(Z_GameFlags.ZooOpenTime_InSeconds) + (int) Z_GameFlags.GetInGameSecondsToMinutes(Z_GameFlags.SecondsZooOpenPerDay);
      if ((double) (num2 - num1) > (double) _Buid_MinutesRemaining)
      {
        this.MinutesLeftAtStartOfNextDay = (int) _Buid_MinutesRemaining - (num2 - num1);
        float num3 = Z_GameFlags.ZooOpenTime_InSeconds + Z_GameFlags.GetMinutesToSecondsInDay(_Buid_MinutesRemaining);
        if ((double) Z_GameFlags.DayTimer > (double) Z_GameFlags.ZooOpenTime_InSeconds)
          num3 += Z_GameFlags.DayTimer - Z_GameFlags.ZooOpenTime_InSeconds;
        LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.FactoryBuildComplete, (int) num3, this.ShopUID, true));
      }
      else
        this.MinutesLeftAtStartOfNextDay = (int) _Buid_MinutesRemaining - (num2 - num1);
    }

    public void CompleteManufacturing(TILETYPE tiletype, Player player)
    {
      if (this.deadanimals != null)
      {
        if (TileData.IsAVegetableProcessingPlant(tiletype))
          MeatProcessingData.ProcessCorpse(this.deadanimals[0], tiletype, this, this.ShopUID, player);
        if (this.deadanimalLeftOvers != null)
        {
          MeatProcessingData.ProcessCorpse(this.deadanimals[0], tiletype, this, this.ShopUID, player);
          this.deadanimals[0].weight *= 0.1f;
          this.deadanimalLeftOvers.Add(this.deadanimals[0]);
          if (ProductionLineCalc.totalsAndBuildings[87] == null)
            ProductionLineCalc.totalsAndBuildings[87] = new TotalsAndBuildings();
          ProductionLineCalc.totalsAndBuildings[87].AddStockWithUID(this.ShopUID, 1);
        }
        if (TileData.IsAnIncinerator(this.factorytiletype))
        {
          NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(AnimalData.GetAnimalName(this.deadanimals[0].animalType), "Processed"));
        }
        else
        {
          NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(AnimalData.GetAnimalName(this.deadanimals[0].animalType), "Incinerated"));
          MeatProcessingData.ProcessCorpse(this.deadanimals[0], this.factorytiletype, this, this.ShopUID, player);
        }
        this.deadanimals.RemoveAt(0);
        this.IsManufacturing = false;
        this.Stock = this.deadanimals.Count;
      }
      else
      {
        if (this.HasMultipleProducts_ButNotDeadAnimal)
        {
          List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype);
          if (this.additionfactoryproducts == null)
            this.additionfactoryproducts = new AdditionalFactoryProduct[88];
          for (int index = 1; index < outputFromFactory.Count; ++index)
          {
            if (this.additionfactoryproducts[(int) outputFromFactory[index]] == null)
              this.additionfactoryproducts[(int) outputFromFactory[index]] = new AdditionalFactoryProduct(outputFromFactory[index]);
            this.additionfactoryproducts[(int) outputFromFactory[index]].AddProduct(1f, this.ShopUID);
            Player.financialrecords.ManufacturedAThing(this.ShopUID, outputFromFactory[index]);
            ++this.TEMPTotalThingsMadeToday;
          }
        }
        --this.Stock;
      }
      ++this.CompletedProducts;
      ++this.TEMPTotalThingsMadeToday;
      Player.financialrecords.ManufacturedAThing(this.ShopUID, this.ProductMadeHere);
      if (!TileData.IsAnIncinerator(this.factorytiletype) && !TileData.IsAMeatProcessingPlant(this.factorytiletype) && !TileData.IsAVegetableProcessingPlant(this.factorytiletype))
        NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(AnimalFoodData.GetAnimalFoodTypeToString(PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype)[0]) ?? "", "Manufactured +1"));
      if (this.deadanimals != null)
      {
        if (this.deadanimals.Count > 0)
          this.StartManufacturing();
        else
          this.IsManufacturing = false;
        this.Stock = this.deadanimals.Count;
      }
      else if (this.Stock > 0)
      {
        this.IsManufacturing = false;
        this.StartManufacturing();
      }
      else
        this.IsManufacturing = false;
      if (this.deadanimals != null)
      {
        TileData.IsAMeatProcessingPlant(this.factorytiletype);
      }
      else
      {
        if (this.ProductMadeHere != AnimalFoodType.Count)
          return;
        List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype);
        this.ProductMadeHere = outputFromFactory[0];
        if (outputFromFactory.Count > 1)
        {
          for (int index = 1; index < outputFromFactory.Count; ++index)
          {
            if (this.additionfactoryproducts == null)
              this.additionfactoryproducts = new AdditionalFactoryProduct[88];
            if (this.additionfactoryproducts[(int) outputFromFactory[index]] == null)
              this.additionfactoryproducts[(int) outputFromFactory[index]] = new AdditionalFactoryProduct(outputFromFactory[index]);
            this.TryToAddThisFoodToStatus(outputFromFactory[index]);
          }
        }
        this.TryToAddThisFoodToStatus(this.ProductMadeHere);
      }
    }

    private void TryToAddThisFoodToStatus(AnimalFoodType ThisProduct)
    {
      if (ProductionLineCalc.totalsAndBuildings[(int) ThisProduct] == null)
      {
        ProductionLineCalc.totalsAndBuildings[(int) ThisProduct] = new TotalsAndBuildings();
        ProductionLineCalc.totalsAndBuildings[(int) ThisProduct].UID_AndCount.Add(new Vector3Int(this.ShopUID, 1, 0));
      }
      else
      {
        for (int index = ProductionLineCalc.totalsAndBuildings[(int) ThisProduct].UID_AndCount.Count - 1; index > -1; --index)
        {
          if (ProductionLineCalc.totalsAndBuildings[(int) ThisProduct].UID_AndCount[index].X == this.ShopUID)
          {
            ++ProductionLineCalc.totalsAndBuildings[(int) ThisProduct].UID_AndCount[index].Y;
            return;
          }
        }
        ProductionLineCalc.totalsAndBuildings[(int) ThisProduct].UID_AndCount.Add(new Vector3Int(this.ShopUID, 1, 0));
      }
    }

    public DeadAnimal TryToCollectProcessedDeadAnimal()
    {
      if (this.deadanimalLeftOvers != null)
      {
        if (this.deadanimalLeftOversInTransit == null)
          this.deadanimalLeftOversInTransit = new List<DeadAnimal>();
        for (int index = 0; index < this.deadanimalLeftOvers.Count; ++index)
        {
          if (!this.deadanimalLeftOversInTransit.Contains(this.deadanimalLeftOvers[index]))
          {
            this.deadanimalLeftOversInTransit.Add(this.deadanimalLeftOvers[index]);
            return this.deadanimalLeftOvers[index];
          }
        }
      }
      return (DeadAnimal) null;
    }

    public bool TryToRemoveProcessedDeadAnimalFromHereOnDeliveryToSomehwreElse(DeadAnimal deadanimal)
    {
      if (this.deadanimalLeftOvers != null)
      {
        if (this.deadanimalLeftOversInTransit == null)
          this.deadanimalLeftOversInTransit = new List<DeadAnimal>();
        if (this.deadanimalLeftOversInTransit.Contains(deadanimal))
          this.deadanimalLeftOversInTransit.Remove(deadanimal);
        if (this.deadanimalLeftOvers.Contains(deadanimal))
        {
          this.deadanimalLeftOvers.Remove(deadanimal);
          return true;
        }
      }
      return false;
    }

    public int TryToCollectCompletedProducts(
      int ICanCarryThisMuch = 1,
      AnimalFoodType TryingToCollectThis = AnimalFoodType.Count)
    {
      if (TryingToCollectThis == AnimalFoodType.Count)
        throw new Exception("Need to pass in thing you want to collect");
      if (this.ProductMadeHere == AnimalFoodType.Count)
      {
        List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype);
        if (outputFromFactory.Count > 0)
          this.ProductMadeHere = outputFromFactory[0];
      }
      if (TryingToCollectThis != this.ProductMadeHere)
        return this.additionfactoryproducts[(int) TryingToCollectThis] != null ? this.additionfactoryproducts[(int) TryingToCollectThis].TryToCollectCompletedProducts(ICanCarryThisMuch) : 0;
      if (this.deadanimalLeftOvers != null)
      {
        if (this.deadanimalLeftOvers.Count <= 0)
          ;
      }
      else
      {
        List<DeadAnimal> deadanimals = this.deadanimals;
        if (this.CompletedProducts > this.CompletedProductsInTransit)
        {
          if (ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere] != null)
          {
            for (int index = ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount.Count - 1; index > -1; --index)
            {
              if (ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount[index].X == this.ShopUID)
              {
                ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount[index].Y -= ICanCarryThisMuch;
                if (ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount[index].Y <= 0)
                  ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount.RemoveAt(index);
              }
            }
            if (ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount.Count == 0)
              ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere] = (TotalsAndBuildings) null;
          }
          int num = this.CompletedProducts - this.CompletedProductsInTransit;
          if (num > ICanCarryThisMuch)
          {
            this.CompletedProductsInTransit += ICanCarryThisMuch;
            return ICanCarryThisMuch;
          }
          this.CompletedProductsInTransit += num;
          return num;
        }
      }
      return 0;
    }

    public int CompletedProductsFromHereDeliveredToOtherLocation(
      int IWasCarryingThisMuch,
      AnimalFoodType FoodTypeIWasCarrying = AnimalFoodType.Count)
    {
      if (FoodTypeIWasCarrying == AnimalFoodType.Count)
        throw new Exception("Need to pass in thing you want to collect");
      if (this.ProductMadeHere == AnimalFoodType.Count)
      {
        List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype);
        if (outputFromFactory.Count > 0)
          this.ProductMadeHere = outputFromFactory[0];
      }
      if (FoodTypeIWasCarrying != this.ProductMadeHere)
        return this.additionfactoryproducts[(int) FoodTypeIWasCarrying] != null ? this.additionfactoryproducts[(int) FoodTypeIWasCarrying].CompletedProductsFromHereDeliveredToOtherLocation(IWasCarryingThisMuch) : 0;
      this.CompletedProducts -= IWasCarryingThisMuch;
      this.CompletedProductsInTransit -= IWasCarryingThisMuch;
      if (this.CompletedProducts >= 0)
        return IWasCarryingThisMuch;
      IWasCarryingThisMuch += this.CompletedProducts;
      this.CompletedProducts = 0;
      return IWasCarryingThisMuch;
    }

    public void StartNewDay(int ShopUID)
    {
      this.TotalThingsMadeYesterday = this.TEMPTotalThingsMadeToday;
      if (this.IsManufacturing)
      {
        this.CheckEndTime((float) this.MinutesLeftAtStartOfNextDay);
      }
      else
      {
        if (this.deadanimals != null)
          this.Stock = this.deadanimals.Count;
        if (this.Stock > 0)
          this.StartManufacturing();
      }
      if (this.additionfactoryproducts != null)
      {
        for (int index = 0; index < this.additionfactoryproducts.Length; ++index)
        {
          if (this.additionfactoryproducts[index] != null)
            this.additionfactoryproducts[index].StartNewDay(ShopUID);
        }
      }
      if (this.deadanimalLeftOvers != null)
        this.deadanimalLeftOversInTransit = new List<DeadAnimal>();
      if (this.deadanimalLeftOvers != null && this.deadanimalLeftOvers.Count > 0)
      {
        if (ProductionLineCalc.totalsAndBuildings[87] == null)
          ProductionLineCalc.totalsAndBuildings[87] = new TotalsAndBuildings();
        ProductionLineCalc.totalsAndBuildings[87].UID_AndCount.Add(new Vector3Int(ShopUID, this.deadanimalLeftOvers.Count, 0));
      }
      else
      {
        if (this.CompletedProducts <= 0)
          return;
        List<AnimalFoodType> outputFromFactory = PcessedMeatData.GetProductOutputFromFactory(this.factorytiletype);
        if (outputFromFactory == null || outputFromFactory.Count <= 0)
          return;
        this.ProductMadeHere = outputFromFactory[0];
        if (ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere] == null)
          ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere] = new TotalsAndBuildings();
        ProductionLineCalc.totalsAndBuildings[(int) this.ProductMadeHere].UID_AndCount.Add(new Vector3Int(ShopUID, this.CompletedProducts, 0));
      }
    }

    public void SaveFactoryProductionLine(Writer writer)
    {
      if (this.deadanimals != null)
      {
        writer.WriteInt("s", this.deadanimals.Count);
        for (int index = 0; index < this.deadanimals.Count; ++index)
          this.deadanimals[index].SaveDeadAnimal(writer);
        if (this.deadanimalLeftOvers != null)
        {
          writer.WriteInt("s", this.deadanimalLeftOvers.Count);
          for (int index = 0; index < this.deadanimalLeftOvers.Count; ++index)
            this.deadanimalLeftOvers[index].SaveDeadAnimal(writer);
        }
        if (this.additionfactoryproducts != null)
          this.SaveAdditionalProducts(writer);
        this.Stock = this.deadanimals.Count;
      }
      else
      {
        writer.WriteInt("s", this.Stock);
        writer.WriteInt("s", this.CompletedProducts);
        writer.WriteBool("s", this.HasMultipleProducts_ButNotDeadAnimal);
        if (this.HasMultipleProducts_ButNotDeadAnimal)
          this.SaveAdditionalProducts(writer);
      }
      writer.WriteBool("s", this.IsManufacturing);
      writer.WriteFloat("s", this.EmployeeProductivityMultiplier);
      writer.WriteInt("s", this.MinutesLeftOnPause);
      writer.WriteBool("s", this.IsPaused);
      writer.WriteInt("s", this.TEMPTotalThingsMadeToday);
      writer.WriteInt("s", this.MinutesLeftAtStartOfNextDay);
      writer.WriteInt("s", this.FullProcessingTime);
    }

    private void SaveAdditionalProducts(Writer writer)
    {
      int _x = 0;
      for (int index = 0; index < this.additionfactoryproducts.Length; ++index)
      {
        if (this.additionfactoryproducts[index] != null && (double) this.additionfactoryproducts[index].TotalCompletedProductsHeld > 0.0)
          ++_x;
      }
      writer.WriteInt("s", _x);
      for (int index = 0; index < this.additionfactoryproducts.Length; ++index)
      {
        if (this.additionfactoryproducts[index] != null && (double) this.additionfactoryproducts[index].TotalCompletedProductsHeld > 0.0)
          this.additionfactoryproducts[index].SavedditionalFactoryProduct(writer);
      }
    }

    public FactoryProductionLine(
      Reader reader,
      TILETYPE parenttiletype,
      int _ShopUID,
      int VersionForLoad)
    {
      this.factorytiletype = parenttiletype;
      this.CheckUseDeadAnimals();
      this.ShopUID = _ShopUID;
      if (this.deadanimals != null)
      {
        int _out = 0;
        int num1 = (int) reader.ReadInt("s", ref _out);
        for (int index = 0; index < _out; ++index)
          this.deadanimals.Add(new DeadAnimal(reader, VersionForLoad));
        if (this.deadanimalLeftOvers != null)
        {
          _out = 0;
          int num2 = (int) reader.ReadInt("s", ref _out);
          for (int index = 0; index < _out; ++index)
            this.deadanimalLeftOvers.Add(new DeadAnimal(reader, VersionForLoad));
        }
        if (this.additionfactoryproducts != null)
        {
          this.additionfactoryproducts = new AdditionalFactoryProduct[88];
          int num2 = (int) reader.ReadInt("s", ref _out);
          for (int index = 0; index < _out; ++index)
          {
            AdditionalFactoryProduct additionalFactoryProduct = new AdditionalFactoryProduct(reader);
            this.additionfactoryproducts[(int) additionalFactoryProduct.animalfoodtype] = additionalFactoryProduct;
          }
        }
        this.Stock = this.deadanimals.Count;
      }
      else
      {
        int num1 = (int) reader.ReadInt("s", ref this.Stock);
        int num2 = (int) reader.ReadInt("s", ref this.CompletedProducts);
        int num3 = (int) reader.ReadBool("s", ref this.HasMultipleProducts_ButNotDeadAnimal);
        if (this.HasMultipleProducts_ButNotDeadAnimal)
        {
          this.additionfactoryproducts = new AdditionalFactoryProduct[88];
          int _out = 0;
          int num4 = (int) reader.ReadInt("s", ref _out);
          for (int index = 0; index < _out; ++index)
          {
            AdditionalFactoryProduct additionalFactoryProduct = new AdditionalFactoryProduct(reader);
            this.additionfactoryproducts[(int) additionalFactoryProduct.animalfoodtype] = additionalFactoryProduct;
          }
        }
        this.HasMultipleProducts_ButNotDeadAnimal = this.factorytiletype == TILETYPE.Windmill;
      }
      int num5 = (int) reader.ReadBool("s", ref this.IsManufacturing);
      this.factorytiletype = parenttiletype;
      int num6 = (int) reader.ReadFloat("s", ref this.EmployeeProductivityMultiplier);
      int num7 = (int) reader.ReadInt("s", ref this.MinutesLeftOnPause);
      int num8 = (int) reader.ReadBool("s", ref this.IsPaused);
      if (VersionForLoad > 38)
      {
        int num9 = (int) reader.ReadInt("s", ref this.TotalThingsMadeYesterday);
      }
      if (VersionForLoad > 40)
      {
        int num10 = (int) reader.ReadInt("s", ref this.MinutesLeftAtStartOfNextDay);
      }
      if (VersionForLoad <= 41)
        return;
      int num11 = (int) reader.ReadInt("s", ref this.FullProcessingTime);
    }
  }
}
