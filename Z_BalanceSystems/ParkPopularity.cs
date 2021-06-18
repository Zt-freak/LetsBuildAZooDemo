// Decompiled with JetBrains decompiler
// Type: TinyZoo.Z_BalanceSystems.ParkPopularity
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using TinyZoo.GamePlay.Enemies;
using TinyZoo.Z_StoreRoom;
using TinyZoo.Z_ZooValues;

namespace TinyZoo.Z_BalanceSystems
{
  internal class ParkPopularity
  {
    internal static float GetParkPopularity(Player player) => ParkPopularity.CalculateAnimalValueAndPopularity(player, out float _, out int _);

    internal static float CalculateAnimalValueAndPopularity(
      Player player,
      out float AnimalValue,
      out int HasHybrid)
    {
      AnimalValue = 0.0f;
      HasHybrid = 0;
      AnimalTempData[] animals = new AnimalTempData[56];
      if (AnimalFoodData.foodcollections == null)
        AnimalFoodData.GetFoodCollection(AnimalType.Rabbit);
      Z_GameFlags.TotalLivingAnimalsInZoo = 0;
      player.prisonlayout.cellblockcontainer.TEMP_TotalPrizoneZonePopularity = 0;
      for (int index1 = 0; index1 < player.prisonlayout.cellblockcontainer.prisonzones.Count; ++index1)
      {
        player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_Popularity = 0;
        for (int index2 = 0; index2 < player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners.Count; ++index2)
        {
          AnimalType animaltype = player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.animaltype;
          if (animals[(int) animaltype] == null)
            animals[(int) animaltype] = new AnimalTempData(animaltype);
          animals[(int) animaltype].AddAnimal(player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2], ref player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_Popularity);
          if (player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.HeadType != AnimalType.None)
          {
            ++HasHybrid;
            AnimalValue += AnimalFoodData.foodcollections[(int) animaltype].RelativeCostOfIngredientsNormalized * 1.5f;
            AnimalValue += AnimalFoodData.foodcollections[(int) player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2].intakeperson.HeadType].RelativeCostOfIngredientsNormalized * 1.5f;
            ++AnimalValue;
          }
          else
            AnimalValue += AnimalFoodData.foodcollections[(int) animaltype].RelativeCostOfIngredientsNormalized;
          if (!player.prisonlayout.cellblockcontainer.prisonzones[index1].prisonercontainer.prisoners[index2]._IsDead)
            ++Z_GameFlags.TotalLivingAnimalsInZoo;
        }
        player.prisonlayout.cellblockcontainer.TEMP_TotalPrizoneZonePopularity += player.prisonlayout.cellblockcontainer.prisonzones[index1].Temp_Popularity;
      }
      return ParkPopularity.Finalize(animals);
    }

    private static float Finalize(AnimalTempData[] animals)
    {
      float num = 0.0f;
      for (int index = 0; index < animals.Length; ++index)
      {
        if (animals[index] != null)
        {
          animals[index].Finalize();
          num += animals[index].AllPopularity;
        }
      }
      return num;
    }
  }
}
