// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Breeding.Breeds
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Tutorials;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo.PlayerDir.Breeding
{
  internal class Breeds
  {
    public List<ActiveBreed> Unplannedbreeds;
    private int BuildingUIDSPawn;
    private List<BreedingBuilding> NurseryBuildings;

    public Breeds()
    {
      this.Unplannedbreeds = new List<ActiveBreed>();
      this.NurseryBuildings = new List<BreedingBuilding>();
    }

    public void SoldABreedingChamber(int BuildingUID, Player player)
    {
      for (int index = this.NurseryBuildings.Count - 1; index > -1; --index)
      {
        if (this.NurseryBuildings[index].UID == BuildingUID)
        {
          this.NurseryBuildings[index].SellThis(player);
          this.NurseryBuildings.RemoveAt(index);
          break;
        }
      }
    }

    public bool AbortThisBaby(Parents_AndChild REF_parents_and_child, Player player)
    {
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
      {
        if (this.NurseryBuildings[index].TryAndAbortThisBaby(REF_parents_and_child, player))
          return true;
      }
      return false;
    }

    public bool IsThisMotherPregnant(int MotherUID)
    {
      for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
      {
        if (this.Unplannedbreeds[index].MotherUID == MotherUID && this.Unplannedbreeds[index].ISActive)
          return true;
      }
      for (int index = this.NurseryBuildings.Count - 1; index > -1; --index)
      {
        if (this.NurseryBuildings[index].IsThisMotherPregnant(MotherUID))
          return true;
      }
      return false;
    }

    public void RemoveBreedingPair(
      Player player,
      BreedingBuilding building,
      Parents_AndChild parnetsandchildren)
    {
      for (int index = this.NurseryBuildings.Count - 1; index > -1; --index)
      {
        if (this.NurseryBuildings[index] == building)
          this.NurseryBuildings[index].RemoveBreeedingPair(parnetsandchildren, player);
      }
      GameFlags.CellBlockContentsChanged = true;
    }

    public void KilledAnAnimal(int UID)
    {
      int index = 0;
      while (index < this.NurseryBuildings.Count && !this.NurseryBuildings[index].KilledAnAnimal(UID))
        ++index;
    }

    public BreedingBuilding GetNurseryBuilding(Vector2Int Location)
    {
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
      {
        if (this.NurseryBuildings[index].Location.CompareMatches(Location))
          return this.NurseryBuildings[index];
      }
      throw new Exception("FAILED TO FIND BUILDING T MOVE");
    }

    public void AddBreedToNurseryBuilding(
      int BuildingUIDSPawn,
      Parents_AndChild Ref_ParentsAndChild,
      Player player)
    {
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
      {
        if (this.NurseryBuildings[index].UID == BuildingUIDSPawn)
        {
          this.NurseryBuildings[index].AddAnimalsToBreed(Ref_ParentsAndChild);
          int num;
          player.prisonlayout.AddAnimalNotInPen(player.prisonlayout.GetThisAnimal(Ref_ParentsAndChild.MaleUID, out num));
          player.prisonlayout.AddAnimalNotInPen(player.prisonlayout.GetThisAnimal(Ref_ParentsAndChild.FemaleUID, out num));
          player.prisonlayout.cellblockcontainer.RemoveThisAnimalOnMoveToOtherBuilding(Ref_ParentsAndChild.MaleUID, Ref_ParentsAndChild.animaltype, out num, player);
          Ref_ParentsAndChild.MaleCellBlockUD = num;
          player.prisonlayout.cellblockcontainer.RemoveThisAnimalOnMoveToOtherBuilding(Ref_ParentsAndChild.FemaleUID, Ref_ParentsAndChild.animaltype, out num, player);
          Ref_ParentsAndChild.FemaleCellBlockUID = num;
          GameFlags.CellBlockContentsChanged = true;
        }
      }
    }

    public void AddNurseryBuilding(Vector2Int Location)
    {
      this.NurseryBuildings.Add(new BreedingBuilding(Location, this.BuildingUIDSPawn));
      ++this.BuildingUIDSPawn;
    }

    public void MoveBuilding(Vector2Int OldLocation, Vector2Int NewLocation)
    {
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
      {
        if (this.NurseryBuildings[index].Location.CompareMatches(OldLocation))
        {
          this.NurseryBuildings[index].Location = new Vector2Int(NewLocation);
          return;
        }
      }
      throw new Exception("FAILED TO FIND BUILDING T MOVE");
    }

    public List<ActiveBreed> GetBreedsInThisNurseryBuilding(Vector2Int Location)
    {
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
      {
        if (this.NurseryBuildings[index].Location.CompareMatches(Location))
          return this.NurseryBuildings[index].breeds;
      }
      throw new Exception("No Building here");
    }

    public ActiveBreed GetThisBreed(int MotherUID)
    {
      for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
      {
        if (this.Unplannedbreeds[index].MotherUID == MotherUID)
          return this.Unplannedbreeds[index];
      }
      for (int index1 = 0; index1 < this.NurseryBuildings.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.NurseryBuildings[index1].breeds.Count; ++index2)
        {
          if (this.NurseryBuildings[index1].breeds[index2].MotherUID == MotherUID)
            return this.NurseryBuildings[index1].breeds[index2];
        }
      }
      return (ActiveBreed) null;
    }

    public void TransferBabyFromBreedingRoomToPen(Player player, int BabyUID)
    {
      for (int index1 = 0; index1 < this.NurseryBuildings.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.NurseryBuildings[index1].ParentsAndChildrenhere.Count; ++index2)
        {
          if (this.NurseryBuildings[index1].ParentsAndChildrenhere[index2].HeldBaby != null && this.NurseryBuildings[index1].ParentsAndChildrenhere[index2].HeldBaby.intakeperson.UID == BabyUID)
          {
            this.NurseryBuildings[index1].ParentsAndChildrenhere[index2].RemovedBaby_SetMarality(player);
            this.NurseryBuildings[index1].MoveBabyToPen(player, BabyUID);
          }
        }
      }
    }

    public void TryAndBirth(
      Player player,
      out int ChildUI,
      out bool ParentAlreadyDead,
      out bool IsBreedingRoomBaby)
    {
      ChildUI = -1;
      IsBreedingRoomBaby = false;
      ParentAlreadyDead = false;
      int maxValue = 0;
      for (int index = this.Unplannedbreeds.Count - 1; index > -1; --index)
      {
        PrisonerInfo thisAnimal = player.prisonlayout.GetThisAnimal(this.Unplannedbreeds[index].MotherUID, out int _);
        if (thisAnimal == null || thisAnimal.IsDead)
        {
          this.Unplannedbreeds.RemoveAt(index);
          ParentAlreadyDead = true;
          return;
        }
        if (this.Unplannedbreeds[index].DaysLeft <= 0)
          ++maxValue;
      }
      for (int index1 = 0; index1 < this.NurseryBuildings.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.NurseryBuildings[index1].breeds.Count; ++index2)
        {
          PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(this.NurseryBuildings[index1].breeds[index2].MotherUID);
          if (thisNotInPenAnimal == null || thisNotInPenAnimal.IsDead)
          {
            ParentAlreadyDead = true;
            this.NurseryBuildings[index1].ParentDied(this.NurseryBuildings[index1].breeds[index2]);
            return;
          }
          if (this.NurseryBuildings[index1].breeds[index2].DaysLeft <= 0)
            ++maxValue;
        }
      }
      int num1 = TinyZoo.Game1.Rnd.Next(0, maxValue);
      for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
      {
        if (this.Unplannedbreeds[index].DaysLeft <= 0)
        {
          if (num1 == 0)
          {
            ChildUI = this.DoBirth(this.Unplannedbreeds[index], player, false);
            this.Unplannedbreeds.RemoveAt(index);
            int num2 = ChildUI;
            return;
          }
          --num1;
        }
      }
      for (int index1 = 0; index1 < this.NurseryBuildings.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.NurseryBuildings[index1].breeds.Count; ++index2)
        {
          PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(this.NurseryBuildings[index1].breeds[index2].MotherUID);
          if (num1 == 0)
          {
            if (thisNotInPenAnimal == null || thisNotInPenAnimal.IsDead)
            {
              ParentAlreadyDead = true;
              this.NurseryBuildings[index1].breeds.RemoveAt(index2);
              return;
            }
            if (this.NurseryBuildings[index1].breeds[index2].ISActive && this.NurseryBuildings[index1].breeds[index2].DaysLeft <= 0)
            {
              IsBreedingRoomBaby = true;
              ChildUI = this.NurseryBuildings[index1].GaveBirth(this.NurseryBuildings[index1].breeds[index2], player);
              this.NurseryBuildings[index1].breeds[index2].ISActive = false;
              this.NurseryBuildings[index1].breeds.RemoveAt(index2);
              int num2 = ChildUI;
              return;
            }
          }
          else
            --num1;
        }
      }
    }

    private int DoBirth(ActiveBreed breed, Player player, bool IsFromBreedBuilding, int _CELLUID = -1)
    {
      int CellBoockUID = _CELLUID;
      PrisonerInfo prisonerInfo = !IsFromBreedBuilding ? player.prisonlayout.GetThisAnimal(breed.MotherUID, out CellBoockUID) : player.prisonlayout.GetThisNotInPenAnimal(breed.MotherUID);
      if (prisonerInfo == null || prisonerInfo.IsDead)
        return -1;
      prisonerInfo.IsPregnant = false;
      int ChildUID;
      player.prisonlayout.AddNewAnimal(breed.animalType, CellBoockUID, breed.ChildType, !breed.boy, out ChildUID, breed.MotherUID, breed.FatherUID);
      prisonerInfo.Children_UID.Add(ChildUID);
      player.Stats.AnimalBredOrFound(breed.animalType, breed.ChildType, !breed.boy);
      return ChildUID;
    }

    public void StartNewDay(Player player)
    {
      for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
      {
        if (!Z_DebugFlags.DisableRandomBirths)
          this.Unplannedbreeds[index].StartNewDay(false);
      }
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
        this.NurseryBuildings[index].StartNewDay(player);
    }

    public int GetTotalSlots(bool QuickGet = true) => 2;

    public bool IsBreeding(int Index) => throw new Exception("DepricatedBreeds IsBreeding");

    public bool QueickCheckSomethingFinished(Player player, bool IncludeUnplannedBirths = false)
    {
      if (IncludeUnplannedBirths)
      {
        for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
        {
          if (this.Unplannedbreeds[index].ISActive)
          {
            if (DebugFlags.IsPCVersion)
            {
              if (this.Unplannedbreeds[index].DaysLeft <= 0)
                return true;
            }
            else if (this.Unplannedbreeds[index].researchtimeleft.QuestGetIsComplete(player.Stats.datetimemanager))
              return true;
          }
        }
      }
      return false;
    }

    public void StartBreed(
      int BreedSlot,
      int Percent,
      AnimalType calssificationoffauna,
      int IndexOfLuckyBaby,
      Player player,
      int _MaleParent,
      int _FemaleParent,
      int MotherUID = -1,
      int FatherUID = -1)
    {
      if (TinyZoo.Game1.Rnd.Next(0, 100) < Percent || TutorialManager.currenttutorial == TUTORIALTYPE.BuildResearch)
      {
        ActiveBreed activeBreed = new ActiveBreed(calssificationoffauna, IndexOfLuckyBaby, player, _MaleParent, _FemaleParent, TinyZoo.Game1.Rnd.Next(0, 2) == 0, MotherUID, FatherUID);
        if (BreedSlot != -1)
          throw new Exception("GIANT Depricated Breed");
        this.Unplannedbreeds.Add(activeBreed);
      }
      else
      {
        ActiveBreed activeBreed = new ActiveBreed(calssificationoffauna, 0, player, _MaleParent, _FemaleParent, TinyZoo.Game1.Rnd.Next(0, 2) == 0, MotherUID, FatherUID);
        if (BreedSlot != -1)
          throw new Exception("iuhsfsdf NO OLD DEPRICATED BREEDS ALLOWED");
        this.Unplannedbreeds.Add(activeBreed);
      }
      player.OldSaveThisPlayer();
    }

    public Breeds(Reader reader, int VersionNumberForLoad)
    {
      int num1 = (int) reader.ReadInt("b", ref this.BuildingUIDSPawn);
      int _out = 0;
      if (VersionNumberForLoad < 4)
      {
        int num2 = (int) reader.ReadInt("b", ref _out);
        List<ActiveBreed> activeBreedList = new List<ActiveBreed>();
        for (int index = 0; index < _out; ++index)
          activeBreedList.Add(new ActiveBreed(reader));
      }
      this.Unplannedbreeds = new List<ActiveBreed>();
      int num3 = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
        this.Unplannedbreeds.Add(new ActiveBreed(reader));
      this.NurseryBuildings = new List<BreedingBuilding>();
      int num4 = (int) reader.ReadInt("b", ref _out);
      for (int index = 0; index < _out; ++index)
        this.NurseryBuildings.Add(new BreedingBuilding(reader, VersionNumberForLoad));
    }

    public void SaveBreeds(Writer writer)
    {
      writer.WriteInt("b", this.BuildingUIDSPawn);
      writer.WriteInt("b", this.Unplannedbreeds.Count);
      for (int index = 0; index < this.Unplannedbreeds.Count; ++index)
        this.Unplannedbreeds[index].SaveActiveBreed(writer);
      writer.WriteInt("b", this.NurseryBuildings.Count);
      for (int index = 0; index < this.NurseryBuildings.Count; ++index)
        this.NurseryBuildings[index].SaveBreedingBuilding(writer);
    }
  }
}
