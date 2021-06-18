// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras.FactoryDeliveryHelper
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.PlayerDir.Shops;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems;
using TinyZoo.Z_BalanceSystems.ProductionLines;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_AnimalsAndPeople.Sim_Person.Extras
{
  internal class FactoryDeliveryHelper
  {
    public static bool OnDeliveryDestinationReached_Dropoff(
      ShopEntry destination,
      ShopEntry cameFromHere,
      ref int stockHeld,
      AnimalFoodType productHeld = AnimalFoodType.Count)
    {
      if (!TileData.IsAFactory(destination.tiletype))
        return false;
      stockHeld = cameFromHere.factoryproduction.CompletedProductsFromHereDeliveredToOtherLocation(stockHeld, productHeld);
      destination.factoryproduction.AddStock(stockHeld);
      stockHeld = 0;
      return true;
    }

    public static bool OnDeliveryDestinationReached_Dropoff(
      ShopEntry destination,
      ShopEntry cameFromHere,
      Player player,
      ref Dictionary<AnimalFoodType, int> inventory)
    {
      if (!TileData.IsAWarehouse(destination.tiletype))
        return false;
      foreach (KeyValuePair<AnimalFoodType, int> keyValuePair in inventory)
      {
        int otherLocation = cameFromHere.factoryproduction.CompletedProductsFromHereDeliveredToOtherLocation(keyValuePair.Value, keyValuePair.Key);
        player.warehouse.AddStockToWareHouse(keyValuePair.Key, otherLocation);
      }
      inventory.Clear();
      return true;
    }

    public static bool OnDeliveryDestinationReached_Dropoff(
      ShopEntry destination,
      ShopEntry cameFromProcessor,
      PrisonZone cameFromPen,
      ref List<DeadAnimal> deadAnimalsHeld)
    {
      if (!TileData.IsAMeatProcessingPlant(destination.tiletype) && !TileData.IsAnIncinerator(destination.tiletype) && !TileData.IsAVegetableProcessingPlant(destination.tiletype))
        return false;
      for (int index = 0; index < deadAnimalsHeld.Count; ++index)
        destination.factoryproduction.AddDeadAnimal(deadAnimalsHeld[index]);
      if (cameFromProcessor != null)
      {
        for (int index = 0; index < deadAnimalsHeld.Count; ++index)
          cameFromProcessor.factoryproduction.TryToRemoveProcessedDeadAnimalFromHereOnDeliveryToSomehwreElse(deadAnimalsHeld[index]);
      }
      deadAnimalsHeld.Clear();
      return true;
    }

    public static bool TryToCollectThingsFromHere(
      ShopEntry destination,
      ref int stockHeld,
      Employee employee)
    {
      AnimalFoodType TryingToCollectThis = AnimalFoodType.Count;
      if (TileData.IsAMeatProcessingPlant(destination.tiletype) || TileData.IsAVegetableProcessingPlant(destination.tiletype))
        TryingToCollectThis = PcessedMeatData.GetProductInputForFactory(employee.quickemplyeedescription.WorksHere);
      stockHeld = destination.factoryproduction.TryToCollectCompletedProducts(TryingToCollectThis: TryingToCollectThis);
      return true;
    }

    public static bool TryToCollectThingsFromHere(
      ShopEntry destination,
      ref Dictionary<AnimalFoodType, int> inventory,
      Employee employee)
    {
      bool flag = false;
      List<AnimalFoodType> animalFoodTypeList1 = new List<AnimalFoodType>();
      List<AnimalFoodType> animalFoodTypeList2 = !TileData.IsAFactory(destination.tiletype) ? WarehouseData.GetDisplayListOfWarehouseItems() : PcessedMeatData.GetProductOutputFromFactory(destination.tiletype);
      for (int index = 0; index < animalFoodTypeList2.Count; ++index)
      {
        int completedProducts = destination.factoryproduction.TryToCollectCompletedProducts(TryingToCollectThis: animalFoodTypeList2[index]);
        if (completedProducts > 0)
        {
          inventory.Add(animalFoodTypeList2[index], completedProducts);
          flag = true;
        }
      }
      return flag;
    }

    public static bool TryToCollectThingsFromHere(
      ShopEntry destination,
      PrisonZone destinationIsPen,
      ref List<DeadAnimal> deadAnimals,
      WalkingPerson parent)
    {
      if (destination != null)
      {
        DeadAnimal processedDeadAnimal = destination.factoryproduction.TryToCollectProcessedDeadAnimal();
        if (processedDeadAnimal == null)
          return false;
        deadAnimals.Add(processedDeadAnimal);
        return true;
      }
      if (destinationIsPen == null)
        return false;
      PrisonerInfo prisonerInfo = (PrisonerInfo) null;
      foreach (PrisonerInfo prisoner in destinationIsPen.prisonercontainer.prisoners)
      {
        if (prisoner.IsDead)
        {
          prisonerInfo = prisoner;
          break;
        }
      }
      if (prisonerInfo == null || !destinationIsPen.RemoveThisPrisoner(prisonerInfo))
        return false;
      deadAnimals.Add(new DeadAnimal(prisonerInfo));
      for (int index = 0; index < CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector.Count; ++index)
      {
        if (CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].Y == parent.UID && CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].X == destinationIsPen.Cell_UID)
        {
          CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector.RemoveAt(index);
          break;
        }
      }
      return true;
    }

    public static bool FindAndGoToCollectCorpse(
      Employee Ref_Employee,
      DeliveryManController deliveryman,
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      out bool HaveThingsButAllBlockedByCollision,
      out bool NothingLeft,
      ref int penID,
      ref ShopEntry shopEntry)
    {
      HaveThingsButAllBlockedByCollision = false;
      NothingLeft = false;
      bool flag1 = false;
      if (TileData.IsAMeatProcessingPlant(Ref_Employee.quickemplyeedescription.WorksHere))
        flag1 = true;
      else if (player.animalProcessing.Buildings.Count <= 0)
        flag1 = true;
      if (!flag1)
        return FactoryDeliveryHelper.FindAndGoToABuildingToCollectThingsFrom(Ref_Employee, deliveryman, player, parent, ref ForceGoHere, out HaveThingsButAllBlockedByCollision, out NothingLeft, ref shopEntry);
      bool flag2 = false;
      for (int index = 0; index < CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector.Count; ++index)
      {
        if (CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].Y == 0 || CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].Y == parent.UID)
        {
          flag2 = true;
          if (CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].X != -1 && deliveryman.TryToStartJourneyToPen(player, CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].X, parent, ref ForceGoHere))
          {
            penID = CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].X;
            CurrentDeadAnimals.DeadAnimalsByCellUID_AndCollector[index].Y = parent.UID;
            return true;
          }
        }
      }
      if (!flag2)
        NothingLeft = true;
      else
        HaveThingsButAllBlockedByCollision = true;
      return false;
    }

    public static bool FindAndGoToABuildingToCollectThingsFrom(
      Employee refEmployee,
      DeliveryManController deliveryman,
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      out bool HaveThingsButAllBlockedByCollision,
      out bool NothingLeft,
      ref ShopEntry shopEntry)
    {
      HaveThingsButAllBlockedByCollision = false;
      NothingLeft = false;
      List<AnimalFoodType> animalFoodTypeList = new List<AnimalFoodType>();
      if (TileData.IsAnIncinerator(refEmployee.quickemplyeedescription.WorksHere))
        animalFoodTypeList.Add(AnimalFoodType.DeadAnimal_Processed);
      else if (TileData.IsAFactory(refEmployee.quickemplyeedescription.WorksHere))
        animalFoodTypeList.Add(PcessedMeatData.GetProductInputForFactory(refEmployee.quickemplyeedescription.WorksHere));
      else if (TileData.IsAWarehouse(refEmployee.quickemplyeedescription.WorksHere))
        animalFoodTypeList.AddRange((IEnumerable<AnimalFoodType>) WarehouseData.GetDisplayListOfWarehouseItems());
      for (int index1 = 0; index1 < animalFoodTypeList.Count; ++index1)
      {
        if (ProductionLineCalc.totalsAndBuildings != null && ProductionLineCalc.totalsAndBuildings[(int) animalFoodTypeList[index1]] != null && ProductionLineCalc.totalsAndBuildings[(int) animalFoodTypeList[index1]].UID_AndCount.Count > 0)
        {
          List<Vector3Int> uidAndCount = ProductionLineCalc.totalsAndBuildings[(int) animalFoodTypeList[index1]].UID_AndCount;
          for (int index2 = 0; index2 < uidAndCount.Count; ++index2)
          {
            ShopEntry thisFacility = player.shopstatus.GetThisFacility(uidAndCount[index2].X);
            if (deliveryman.TryGoToSpecificBuilding(thisFacility.tiletype, thisFacility.ShopUID, player, parent, ref ForceGoHere))
            {
              shopEntry = thisFacility;
              return true;
            }
          }
          HaveThingsButAllBlockedByCollision = true;
        }
      }
      if (!HaveThingsButAllBlockedByCollision)
        NothingLeft = true;
      return false;
    }

    public static bool GoBackToHomeBuilding(
      Player player,
      WalkingPerson parent,
      ref Vector2Int ForceGoHere,
      Employee refEmployee,
      DeliveryManController deliveryman,
      out ShopEntry destination)
    {
      return deliveryman.TryGoToSpecificBuilding(refEmployee.quickemplyeedescription.WorksHere, refEmployee.quickemplyeedescription.ShopUID, player, parent, ref ForceGoHere, out destination);
    }
  }
}
