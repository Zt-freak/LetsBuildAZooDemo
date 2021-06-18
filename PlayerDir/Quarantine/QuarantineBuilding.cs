// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Quarantine.QuarantineBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.Breeding;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.PlayerDir.Layout;

namespace TinyZoo.PlayerDir.Quarantine
{
  internal class QuarantineBuilding
  {
    public int UID;
    private List<QuarantinedAnimal> quarantinedAnimals;
    public static bool SomethingInQuaratineBuildingChanged;

    public QuarantineBuilding(int _UID)
    {
      this.UID = _UID;
      this.quarantinedAnimals = new List<QuarantinedAnimal>();
    }

    public void StartNewDay(Player player)
    {
      List<int> animalUID = new List<int>();
      for (int index = 0; index < this.quarantinedAnimals.Count; ++index)
      {
        this.quarantinedAnimals[index].StartNewDay();
        if (this.quarantinedAnimals[index].GetIsAutoReadyToTransferOut())
          animalUID.Add(this.quarantinedAnimals[index].AnimalUID);
      }
      this.TransferQuarantinedAnimalToPen(animalUID, player, false);
    }

    public void AddNewQuaratinedAnimal(
      PrisonerInfo animal,
      Player player,
      bool TransferredFromPen,
      CityName cameFromHere)
    {
      int CellBoockUID;
      player.prisonlayout.GetThisAnimal(animal.intakeperson.UID, out CellBoockUID);
      int daysForQuarantine = player.animalquarantine.GetNumberOfDaysForQuarantine(TransferredFromPen, cameFromHere);
      this.quarantinedAnimals.Add(new QuarantinedAnimal(animal.intakeperson.UID, CellBoockUID, daysForQuarantine));
      player.prisonlayout.AddAnimalNotInPen(animal);
    }

    public List<QuarantinedAnimal> GetListOfQuarantinedAnimals() => this.quarantinedAnimals;

    public List<PrisonerInfo> GetListOfQuarantinedAnimals(Player player)
    {
      List<PrisonerInfo> prisonerInfoList = new List<PrisonerInfo>(this.quarantinedAnimals.Count);
      for (int index = 0; index < this.quarantinedAnimals.Count; ++index)
        prisonerInfoList.Add(player.prisonlayout.GetThisNotInPenAnimal(this.quarantinedAnimals[index].AnimalUID));
      return prisonerInfoList;
    }

    public QuarantinedAnimal GetQuarantinedAnimal(int animalUID)
    {
      for (int index = this.quarantinedAnimals.Count - 1; index > -1; --index)
      {
        if (this.quarantinedAnimals[index].AnimalUID == animalUID)
          return this.quarantinedAnimals[index];
      }
      return (QuarantinedAnimal) null;
    }

    public void TransferQuarantinedAnimalToPen(
      List<int> animalUID,
      Player player,
      bool GoToPenSelect)
    {
      WaveInfo waveInfo = new WaveInfo(new IntakeInfo());
      for (int index1 = 0; index1 < animalUID.Count; ++index1)
      {
        QuarantinedAnimal quarantinedAnimal = (QuarantinedAnimal) null;
        for (int index2 = this.quarantinedAnimals.Count - 1; index2 > -1; --index2)
        {
          if (this.quarantinedAnimals[index2].AnimalUID == animalUID[index1])
          {
            quarantinedAnimal = this.quarantinedAnimals[index2];
            this.quarantinedAnimals.RemoveAt(index2);
            break;
          }
        }
        PrisonerInfo thisNotInPenAnimal = player.prisonlayout.GetThisNotInPenAnimal(quarantinedAnimal.AnimalUID);
        player.prisonlayout.RemoveThisNotInPenAnimal(quarantinedAnimal.AnimalUID);
        if (!GoToPenSelect)
          BreedingBuilding.FindBestCellBlockToPutAnimalIn(quarantinedAnimal.CellBlockUID, player, quarantinedAnimal.AnimalUID).AddAnimalFromBreedingRoom(thisNotInPenAnimal);
        else
          waveInfo.AddPrisonerInfo(thisNotInPenAnimal);
      }
      if (!GoToPenSelect)
        return;
      player.livestats.AnimalsJustTraded = waveInfo;
      FeatureFlags.NewAnimalGot = true;
    }

    public void SaveQuarantineBuilding(Writer writer)
    {
      writer.WriteInt("q", this.UID);
      writer.WriteInt("q", this.quarantinedAnimals.Count);
      for (int index = 0; index < this.quarantinedAnimals.Count; ++index)
        this.quarantinedAnimals[index].SaveQuarantinedAnimal(writer);
    }

    public QuarantineBuilding(Reader reader)
    {
      int num1 = (int) reader.ReadInt("q", ref this.UID);
      int _out = 0;
      int num2 = (int) reader.ReadInt("q", ref _out);
      this.quarantinedAnimals = new List<QuarantinedAnimal>();
      for (int index = 0; index < _out; ++index)
        this.quarantinedAnimals.Add(new QuarantinedAnimal(reader));
    }
  }
}
