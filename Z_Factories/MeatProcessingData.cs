// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_Factories.MeatProcessingData
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir._Factories;
using TinyZoo.PlayerDir.Incinerator;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BuildingInfo.Z_Processing.Data;
using TinyZoo.Z_HUD.Z_Notification.NotificationBubble;
using TinyZoo.Z_Processing;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.Z_Factories
{
  internal class MeatProcessingData
  {
    private static float[,] YieldMultipliers;

    internal static void ProcessCorpse(
      DeadAnimal deadanimal,
      TILETYPE tiletype,
      FactoryProductionLine factoryproduction,
      int ShopUID,
      Player player)
    {
      if (!TileData.IsAMeatProcessingPlant(tiletype) && !TileData.IsAVegetableProcessingPlant(tiletype))
        return;
      AnimalProductionList animalProductionList = !TileData.IsAVegetableProcessingPlant(tiletype) ? PcessedMeatData.GetAnmalToBaseMeatType(deadanimal.animalType) : ProcessedVeg.GetVegetableToOutput(deadanimal.cropType);
      for (int index = 0; index < animalProductionList.animalfoodtypes.Length; ++index)
      {
        if (PcessedMeatData.CanProcessThisProduce(animalProductionList.animalfoodtypes[index], player))
        {
          NotificationBubbleManager.Instance.AddNotificationBubbleToQueue(new NotificationBubbleInfo(AnimalFoodData.GetAnimalFoodTypeToString(animalProductionList.animalfoodtypes[index]) ?? "", "Processed +1"));
          if (factoryproduction.additionfactoryproducts[(int) animalProductionList.animalfoodtypes[index]] == null)
            factoryproduction.additionfactoryproducts[(int) animalProductionList.animalfoodtypes[index]] = new AdditionalFactoryProduct(animalProductionList.animalfoodtypes[index]);
          float num = deadanimal.weight * 1f;
          float UnitsHarvested = 1f;
          factoryproduction.additionfactoryproducts[(int) animalProductionList.animalfoodtypes[index]].AddProduct(UnitsHarvested, ShopUID);
        }
      }
    }

    internal static void GetYieldMultiplier(AnimalType animaltype)
    {
      if (MeatProcessingData.YieldMultipliers != null)
        return;
      MeatProcessingData.YieldMultipliers = new float[56, 2];
    }
  }
}
