// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Layout.CellBlocks.PrisonerContainer
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.OverWorld.OverWorldEnv.Customers;
using TinyZoo.PlayerDir.HeroQuests;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Tile_Data;
using TinyZoo.Z_BalanceSystems.Animals;
using TinyZoo.Z_Diseases;
using TinyZoo.Z_Notification;
using TinyZoo.Z_StoreRoom;

namespace TinyZoo.PlayerDir.Layout.CellBlocks
{
  internal class PrisonerContainer
  {
    public List<PrisonerInfo> prisoners;
    public int Earnings;
    private int PotentialEarnings;
    public bool ThisWasTehCellBlockThatChanged;
    public int PrisonersNotInRightCell;
    public AnimalFoodDistribution FoodForAnimals;
    public List<TempAnimalInfo> tempAnimalInfo;

    public PrisonerContainer()
    {
      this.FoodForAnimals = new AnimalFoodDistribution();
      this.prisoners = new List<PrisonerInfo>();
    }

    public bool EveryoneIsDeadOrCellIsEmpty()
    {
      if (this.prisoners.Count == 0)
        return true;
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (!this.prisoners[index].IsDead)
          return false;
      }
      return true;
    }

    public bool RemoveThisPrisonerByUID(int UID, AnimalType animaltype)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson.UID == UID && this.prisoners[index].intakeperson.animaltype == animaltype)
        {
          this.ThisWasTehCellBlockThatChanged = true;
          this.prisoners.RemoveAt(index);
          return true;
        }
      }
      return false;
    }

    public void ClearTheDead()
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].IsDead)
        {
          this.prisoners.RemoveAt(index);
          this.ThisWasTehCellBlockThatChanged = true;
          GameFlags.CellBlockContentsChanged = true;
        }
      }
    }

    public bool HasLivingAnimals()
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].IsDead)
          return true;
      }
      return false;
    }

    public void RemoveFromOverworldRendering()
    {
      for (int index = 0; index < this.prisoners.Count; ++index)
        CustomerManager.RemoveAnimal(this.prisoners[index]);
    }

    public List<AnimalType> GetAllTypesOfAnimal()
    {
      List<AnimalType> animalTypeList = new List<AnimalType>();
      for (int index = 0; index < this.prisoners.Count; ++index)
      {
        if (!animalTypeList.Contains(this.prisoners[index].intakeperson.animaltype))
          animalTypeList.Add(this.prisoners[index].intakeperson.animaltype);
      }
      return animalTypeList;
    }

    public int GetTotalOfThisAnimal(AnimalType animal)
    {
      int num = 0;
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson.animaltype == animal)
          ++num;
      }
      return num;
    }

    public int GetTotalCrossbreed(AnimalType AnimalA, AnimalType AnimalB)
    {
      int num = 0;
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (!this.prisoners[index].IsDead && this.prisoners[index].intakeperson.HeadType != AnimalType.None)
        {
          if (AnimalA == AnimalType.None && AnimalB == AnimalType.None)
            ++num;
          else if (AnimalA != AnimalType.None && AnimalB != AnimalType.None)
          {
            if (this.prisoners[index].intakeperson.HeadType == AnimalA && this.prisoners[index].intakeperson.animaltype != AnimalB)
              ++num;
            else if (this.prisoners[index].intakeperson.HeadType == AnimalB && this.prisoners[index].intakeperson.animaltype != AnimalA)
              ++num;
          }
          else if (AnimalA != AnimalType.None && (this.prisoners[index].intakeperson.HeadType == AnimalA || this.prisoners[index].intakeperson.animaltype != AnimalA))
            ++num;
        }
      }
      return num;
    }

    public void GetHungryAnaimals(Player player, Vector2Int GateLocation, int Cell_UID)
    {
      for (int index1 = this.prisoners.Count - 1; index1 > -1; --index1)
      {
        if ((double) this.prisoners[index1].Hunger > 0.0)
        {
          for (int index2 = 0; index2 < this.FoodForAnimals.FoodSet.Count; ++index2)
          {
            if (this.FoodForAnimals.FoodSet[index2].animal == this.prisoners[index1].intakeperson.animaltype)
              player.livestats.AddHungryAnimal(this.prisoners[index1], GateLocation, this.FoodForAnimals.FoodSet[index2], Cell_UID);
          }
        }
      }
    }

    public bool HasMoreThanOneTypeOfAnimal()
    {
      if (this.prisoners.Count > 1)
      {
        for (int index = 1; index < this.prisoners.Count; ++index)
        {
          if (this.prisoners[index].intakeperson.animaltype != this.prisoners[0].intakeperson.animaltype)
            return true;
        }
      }
      return false;
    }

    public bool TryToInfectAnimal(Disease disease, int OnlyInfectThisMany = -1)
    {
      bool flag = false;
      for (int index = 0; index < this.prisoners.Count; ++index)
      {
        if (this.prisoners[index].TryToInfect(disease))
        {
          if (OnlyInfectThisMany > 0)
          {
            --OnlyInfectThisMany;
            if (OnlyInfectThisMany <= 0)
              return true;
          }
          flag = true;
        }
      }
      return flag;
    }

    public void SetUpTempAnimals(
      int Cell_UID,
      CellBlockType cellblocktype,
      Player player,
      bool IncludeFoodForStoreRoom = false)
    {
      this.tempAnimalInfo = new List<TempAnimalInfo>();
      List<AnimalType> animalTypeList = new List<AnimalType>();
      for (int index1 = 0; index1 < this.prisoners.Count; ++index1)
      {
        if (!animalTypeList.Contains(this.prisoners[index1].intakeperson.animaltype))
        {
          animalTypeList.Add(this.prisoners[index1].intakeperson.animaltype);
          this.tempAnimalInfo.Add(new TempAnimalInfo(this.prisoners[index1].intakeperson.animaltype, index1, this.prisoners[index1], cellblocktype, Cell_UID, this.prisoners[index1].intakeperson.HeadType, this.prisoners[index1].intakeperson.HeadVariant, this.prisoners[index1].intakeperson.CLIndex));
          if (IncludeFoodForStoreRoom && !player.storerooms.HasRunOutOfSomethingThatIsNeededByAnimals)
          {
            FoodSet thisSet = this.FoodForAnimals.GetThisSet(this.prisoners[index1].intakeperson.animaltype);
            FoodCollection foodCollection = AnimalFoodData.GetFoodCollection(this.prisoners[index1].intakeperson.animaltype);
            for (int index2 = 0; index2 < thisSet.FoodUnitsPerDay.Count; ++index2)
            {
              if ((double) thisSet.FoodUnitsPerDay[index2] > 0.0 && (double) player.storerooms.GetTotalStockOfThis(foodCollection.animalfoodentry[index2].foodtype) <= 0.0)
                player.storerooms.HasRunOutOfSomethingThatIsNeededByAnimals = true;
            }
          }
        }
        else
        {
          bool flag = false;
          for (int index2 = 0; index2 < this.tempAnimalInfo.Count; ++index2)
          {
            if (this.tempAnimalInfo[index2].animaltype == this.prisoners[index1].intakeperson.animaltype && this.tempAnimalInfo[index2].animalHead == this.prisoners[index1].intakeperson.HeadType)
            {
              flag = true;
              this.tempAnimalInfo[index2].AddAnimal(this.prisoners[index1], index1, Cell_UID);
            }
          }
          if (!flag)
            this.tempAnimalInfo.Add(new TempAnimalInfo(this.prisoners[index1].intakeperson.animaltype, index1, this.prisoners[index1], cellblocktype, Cell_UID, this.prisoners[index1].intakeperson.HeadType, this.prisoners[index1].intakeperson.HeadVariant, this.prisoners[index1].intakeperson.CLIndex));
        }
      }
    }

    public void SetUpAllStock(Player player)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
        player.livestats.stocktimes.AddOne(this.prisoners[index]);
      player.livestats.stocktimes.FinalizeSet(this.FoodForAnimals, player);
      QuestScrubber.ScrubOnRecievingAnimal(player);
      QuestScrubber.ScrubOnNewAnimalVariant(player);
    }

    public void SetPrisonersOnMapEnd(
      EnemyManager enemyrenderer,
      CellBlockType CellBLOCKTYPE,
      Player player)
    {
      throw new Exception("NOT IN GAME - IF USED remember to add food");
    }

    public bool DoParole(IntakePerson person, CellBlockType CellBLOCKTYPE)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson == person)
        {
          GameFlags.CellBlockContentsChanged = true;
          this.ThisWasTehCellBlockThatChanged = true;
          this.prisoners.RemoveAt(index);
          this.PrisonersNotInRightCell = 0;
          this.REcalculateEarnings(CellBLOCKTYPE, ref this.PrisonersNotInRightCell);
          return true;
        }
      }
      return false;
    }

    public void ResetForLanguage()
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
        this.prisoners[index].intakeperson.ResetForLanguage();
    }

    public bool RemoveThisAnimalFromCellBlock(PrisonerInfo prisonerinfo)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson == prisonerinfo.intakeperson)
        {
          this.ThisWasTehCellBlockThatChanged = true;
          this.prisoners.RemoveAt(index);
          Z_NotificationManager.RescrubNewbornsOnTradeAnimal = true;
          return true;
        }
      }
      return false;
    }

    public PrisonerInfo GetThisPrisoner(IntakePerson intakeperson)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson == intakeperson)
          return this.prisoners[index];
      }
      return (PrisonerInfo) null;
    }

    public PrisonerInfo GetThisPrisoner(int _UID)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index].intakeperson.UID == _UID)
          return this.prisoners[index];
      }
      return (PrisonerInfo) null;
    }

    public bool RemoveThisPrisoner(PrisonerInfo prisonerinfo, CellBlockType CellBLOCKTYPE)
    {
      for (int index = this.prisoners.Count - 1; index > -1; --index)
      {
        if (this.prisoners[index] == prisonerinfo)
        {
          GameFlags.CellBlockContentsChanged = true;
          this.ThisWasTehCellBlockThatChanged = true;
          this.prisoners.RemoveAt(index);
          this.PrisonersNotInRightCell = 0;
          this.REcalculateEarnings(CellBLOCKTYPE, ref this.PrisonersNotInRightCell);
          return true;
        }
      }
      return false;
    }

    public void SetConsumption(ConsumptionStatus consumptionstatus)
    {
      for (int index = 0; index < this.prisoners.Count; ++index)
        this.prisoners[index].SetConsumption(consumptionstatus);
    }

    public void GetDailyEanings(
      ref int Profit,
      bool IsDayEnd_CalculateParoleEtc,
      ref int AllMoneyIncludingWrngCell)
    {
      AllMoneyIncludingWrngCell += this.PotentialEarnings;
      if (IsDayEnd_CalculateParoleEtc)
        Profit += this.Earnings;
      else
        Profit += this.Earnings;
    }

    private void REcalculateEarnings(CellBlockType CellBLOCKTYPE, ref int PeopleInWrongCell)
    {
      this.Earnings = 0;
      this.PotentialEarnings = 0;
      for (int index = 0; index < this.prisoners.Count; ++index)
      {
        if (!this.prisoners[index].IsDead)
        {
          if (LiveStats.reqforpeople.wantsbyperson[(int) this.prisoners[index].intakeperson.animaltype].CellRequirement > -1 && (CellBlockType) LiveStats.reqforpeople.wantsbyperson[(int) this.prisoners[index].intakeperson.animaltype].CellRequirement != CellBLOCKTYPE && CellBLOCKTYPE != CellBlockType.HoldingCell)
          {
            this.prisoners[index].intakeperson.WrongCell = true;
            ++PeopleInWrongCell;
            this.PotentialEarnings += this.prisoners[index].intakeperson.P_PerDay;
          }
          else
          {
            this.prisoners[index].intakeperson.WrongCell = false;
            this.Earnings += this.prisoners[index].intakeperson.P_PerDay;
            this.PotentialEarnings += this.prisoners[index].intakeperson.P_PerDay;
          }
        }
        if (CellBLOCKTYPE == CellBlockType.HoldingCell)
          this.prisoners[index].intakeperson.WrongCell = false;
      }
    }

    public void SavePrisonContainer(Writer writer)
    {
      writer.WriteInt("a", this.prisoners.Count);
      for (int index = 0; index < this.prisoners.Count; ++index)
        this.prisoners[index].SavePrisonerInfo(writer);
      this.FoodForAnimals.SaveAnimalFoodDistribution(writer);
    }

    public PrisonerContainer(Reader reader, CellBlockType celltype, int VersionNumberForLoad)
    {
      this.FoodForAnimals = new AnimalFoodDistribution();
      this.prisoners = new List<PrisonerInfo>();
      int num1 = 0;
      int num2 = (int) reader.ReadInt("a", ref num1);
      for (int index = 0; index < num1; ++index)
      {
        this.prisoners.Add(new PrisonerInfo(reader, VersionNumberForLoad));
        double habitatMatch = (double) this.prisoners[index].GetHabitatMatch(celltype);
        this.FoodForAnimals.AddAnimal(this.prisoners[index].intakeperson.animaltype);
      }
      this.PrisonersNotInRightCell = 0;
      this.FoodForAnimals = new AnimalFoodDistribution(reader, VersionNumberForLoad);
      this.REcalculateEarnings(celltype, ref num1);
    }
  }
}
