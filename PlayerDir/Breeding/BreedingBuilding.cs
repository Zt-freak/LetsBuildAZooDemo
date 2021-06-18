// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Breeding.BreedingBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.PlayerDir.Layout;
using TinyZoo.Z_Animal_Data;
using TinyZoo.Z_BreedScreen.ActiveBreedPair;
using TinyZoo.Z_BreedScreen.SelectNewBreed.SelectSpecies;

namespace TinyZoo.PlayerDir.Breeding
{
  internal class BreedingBuilding
  {
    public Vector2Int Location;
    public int UID;
    public List<ActiveBreed> breeds;
    public List<Parents_AndChild> ParentsAndChildrenhere;

    public BreedingBuilding(Vector2Int _Location, int _UID)
    {
      this.breeds = new List<ActiveBreed>();
      this.Location = new Vector2Int(_Location);
      this.UID = _UID;
      this.ParentsAndChildrenhere = new List<Parents_AndChild>();
    }

    public void SellThis(Player player)
    {
      for (int index = this.ParentsAndChildrenhere.Count - 1; index > -1; --index)
        this.RemoveBreeedingPair(this.ParentsAndChildrenhere[index], player);
    }

    public void ParentDied(ActiveBreed Deadbaby)
    {
    }

    public void SellThis()
    {
    }

    public bool IsThisMotherPregnant(int MotherUID)
    {
      for (int index = 0; index < this.breeds.Count; ++index)
      {
        if (this.breeds[index].MotherUID == MotherUID && this.breeds[index].ISActive)
          return true;
      }
      return false;
    }

    public void RemoveBreeedingPair(Parents_AndChild ParentAndChild, Player player)
    {
      for (int index = this.ParentsAndChildrenhere.Count - 1; index > -1; --index)
      {
        if (this.ParentsAndChildrenhere[index] == ParentAndChild)
        {
          this.TryAndTransferThisPerson(ParentAndChild.FemaleCellBlockUID, player, ParentAndChild.FemaleUID);
          this.TryAndTransferThisPerson(ParentAndChild.MaleCellBlockUD, player, ParentAndChild.MaleUID);
          if (ParentAndChild.HeldBaby != null)
          {
            ParentAndChild.RemovedBaby_SetMarality(player);
            this.TryAndTransferThisPerson(ParentAndChild.FemaleCellBlockUID, player, ParentAndChild.HeldBaby.intakeperson.UID);
          }
          this.ParentsAndChildrenhere.RemoveAt(index);
        }
      }
    }

    private bool TryAndTransferThisPerson(int CellBlockUID, Player player, int AnimalUID)
    {
      PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(AnimalUID);
      PrisonZone blockToPutAnimalIn = BreedingBuilding.FindBestCellBlockToPutAnimalIn(CellBlockUID, player, AnimalUID);
      if (blockToPutAnimalIn == null)
        return false;
      blockToPutAnimalIn.AddAnimalFromBreedingRoom(thisNotInPenAnimal);
      player.prisonlayout.RemoveThisNotInPenAnimal(AnimalUID);
      return true;
    }

    public static PrisonZone FindBestCellBlockToPutAnimalIn(
      int CellBlockUID_CameFromHere,
      Player player,
      int AnimalUID)
    {
      PrisonZone prisonZone = player.prisonlayout.GetThisCellBlock(CellBlockUID_CameFromHere);
      if (prisonZone == null)
      {
        PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(AnimalUID);
        int blockWithThisAnimal = player.prisonlayout.FindCellBlockWIthThisAnimal(thisNotInPenAnimal.intakeperson.animaltype);
        prisonZone = player.prisonlayout.GetThisCellBlock(blockWithThisAnimal);
      }
      if (prisonZone == null && player.prisonlayout.cellblockcontainer.prisonzones.Count > 0)
        prisonZone = player.prisonlayout.cellblockcontainer.prisonzones[0];
      return prisonZone;
    }

    public bool KilledAnAnimal(int UID)
    {
      for (int index1 = 0; index1 < this.ParentsAndChildrenhere.Count; ++index1)
      {
        if (this.ParentsAndChildrenhere[index1].KilledAnAnimal(UID) && this.ParentsAndChildrenhere[index1].FemaleUID == UID)
        {
          for (int index2 = this.breeds.Count - 1; index2 > -1; --index2)
          {
            if (this.breeds[index2].MotherUID == UID)
            {
              this.breeds.RemoveAt(index2);
              return true;
            }
          }
        }
      }
      return false;
    }

    public void AddAnimalsToBreed(Parents_AndChild Ref_ParentsAndChild) => this.ParentsAndChildrenhere.Add(Ref_ParentsAndChild);

    public int GaveBirth(ActiveBreed activebreed, Player player)
    {
      for (int index = 0; index < this.ParentsAndChildrenhere.Count; ++index)
      {
        if (this.ParentsAndChildrenhere[index].FemaleUID == activebreed.MotherUID)
          return this.ParentsAndChildrenhere[index].GiveBirth(activebreed, player);
      }
      return -1;
    }

    public bool MoveBabyToPen(Player player, int BabyUID)
    {
      for (int index = 0; index < this.ParentsAndChildrenhere.Count; ++index)
      {
        if (this.ParentsAndChildrenhere[index].MoveBabyToPen(player, BabyUID))
          return true;
      }
      return false;
    }

    public bool TryAndAbortThisBaby(Parents_AndChild REF_parents_and_child, Player player)
    {
      for (int index = 0; index < this.ParentsAndChildrenhere.Count; ++index)
      {
        if (this.ParentsAndChildrenhere[index] == REF_parents_and_child)
        {
          if (this.ParentsAndChildrenhere[index].AbortBaby(ref this.breeds))
            ++player.Stats.BabiesAborted;
          return true;
        }
      }
      return false;
    }

    public int GetCellBlock(int UID)
    {
      for (int index = 0; index < this.ParentsAndChildrenhere.Count; ++index)
      {
        if (this.ParentsAndChildrenhere[index].FemaleUID == UID)
          return this.ParentsAndChildrenhere[index].FemaleCellBlockUID;
        if (this.ParentsAndChildrenhere[index].MaleUID == UID)
          return this.ParentsAndChildrenhere[index].MaleCellBlockUD;
      }
      return -1;
    }

    public ActiveBreed GetThisBreed(Parents_AndChild Ref_ParentsAndChild)
    {
      for (int index = 0; index < this.breeds.Count; ++index)
      {
        if (this.breeds[index].FatherUID == Ref_ParentsAndChild.MaleUID)
          return this.breeds[index];
      }
      return (ActiveBreed) null;
    }

    public void StartNewDay(Player player)
    {
      for (int index = 0; index < this.breeds.Count; ++index)
        this.breeds[index].StartNewDay(true);
      for (int index1 = 0; index1 < this.ParentsAndChildrenhere.Count; ++index1)
      {
        if (this.ParentsAndChildrenhere[index1].HeldBaby != null)
        {
          ++this.ParentsAndChildrenhere[index1].NursingDays;
          int nursingDayOptionToDays = TimeWithParents.GetNursingDayOptionToDays((TIMEWITHPARENTS) this.ParentsAndChildrenhere[index1].NursingDaysOption);
          if (this.ParentsAndChildrenhere[index1].NursingDays >= nursingDayOptionToDays)
            LiveStats.AddEventToTheDay(new ZooMoment(ZOOMOMENT.TransferAnimalFromBreedingHouse, _UID: this.ParentsAndChildrenhere[index1].HeldBaby.intakeperson.UID));
        }
        else if (!this.ParentsAndChildrenhere[index1].FatherDead && !this.ParentsAndChildrenhere[index1].MotherIsDead && (this.ParentsAndChildrenhere[index1].ProductionTargetOption > 0 && player.prisonlayout.GetThisNotInPenAnimal(this.ParentsAndChildrenhere[index1].FemaleUID).IsFertile) && player.prisonlayout.GetThisNotInPenAnimal(this.ParentsAndChildrenhere[index1].MaleUID).IsFertile)
        {
          bool flag1 = false;
          for (int index2 = 0; index2 < this.breeds.Count; ++index2)
          {
            if (this.breeds[index2].FatherUID == this.ParentsAndChildrenhere[index1].MaleUID)
              flag1 = true;
          }
          if (!flag1 && this.ParentsAndChildrenhere[index1].HeldBaby == null)
          {
            ++this.ParentsAndChildrenhere[index1].Attempts;
            bool flag2 = (double) TinyZoo.Game1.Rnd.Next(0, 100) < (double) AnimalData.GetBreedChance(this.ParentsAndChildrenhere[index1].animaltype);
            if (this.ParentsAndChildrenhere[index1].IsArticificalInsemination && !flag2)
              flag2 = (double) TinyZoo.Game1.Rnd.Next(0, 100) < (double) AnimalData.GetBreedChance(this.ParentsAndChildrenhere[index1].animaltype);
            if (flag2)
              this.breeds.Add(new ActiveBreed(this.ParentsAndChildrenhere[index1]));
          }
        }
      }
    }

    public BreedingBuilding(Reader reader, int VersionNumberForLoad)
    {
      this.Location = new Vector2Int(reader);
      int num1 = (int) reader.ReadInt("b", ref this.UID);
      int _out = 0;
      int num2 = (int) reader.ReadInt("b", ref _out);
      this.breeds = new List<ActiveBreed>();
      for (int index = 0; index < _out; ++index)
        this.breeds.Add(new ActiveBreed(reader));
      int num3 = (int) reader.ReadInt("b", ref _out);
      this.ParentsAndChildrenhere = new List<Parents_AndChild>();
      for (int index = 0; index < _out; ++index)
        this.ParentsAndChildrenhere.Add(new Parents_AndChild(reader, VersionNumberForLoad));
    }

    public void SaveBreedingBuilding(Writer writer)
    {
      this.Location.SaveVector2Int(writer);
      writer.WriteInt("b", this.UID);
      writer.WriteInt("b", this.breeds.Count);
      for (int index = 0; index < this.breeds.Count; ++index)
        this.breeds[index].SaveActiveBreed(writer);
      writer.WriteInt("b", this.ParentsAndChildrenhere.Count);
      for (int index = 0; index < this.ParentsAndChildrenhere.Count; ++index)
        this.ParentsAndChildrenhere[index].SaveParents_AndChild(writer);
    }
  }
}
