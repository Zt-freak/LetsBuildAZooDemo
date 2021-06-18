// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CRISPR.CRISPR_Breed
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;
using TinyZoo.GamePlay.Enemies;
using TinyZoo.PlayerDir.IntakeStuff;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_Notification;

namespace TinyZoo.PlayerDir.CRISPR
{
  internal class CRISPR_Breed
  {
    public List<CRISPRBuilding> CRISPRBuildings;
    private int BuildingUIDSPawn;
    private int BreedUIDToSpawn;

    public CRISPR_Breed() => this.CRISPRBuildings = new List<CRISPRBuilding>();

    public CRISPRBuilding GetCRISPRBuilding(Vector2Int Location)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].Location.CompareMatches(Location))
          return this.CRISPRBuildings[index];
      }
      throw new Exception("FAILED TO FIND A CRISPR BUILDING AT THIS LOCATION");
    }

    public void AddNewCRISPRBuilding(Vector2Int Location)
    {
      this.CRISPRBuildings.Add(new CRISPRBuilding(Location, this.BuildingUIDSPawn));
      ++this.BuildingUIDSPawn;
    }

    public CRISPRBuilding GetBuildingForThisBreed(int UID)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].HasThisBreed(UID))
          return this.CRISPRBuildings[index];
      }
      return (CRISPRBuilding) null;
    }

    public bool BreedIsComplete(
      int UID,
      Player player,
      ref bool WasNewVariant,
      out AnimalRenderDescriptor animalBorn)
    {
      animalBorn = (AnimalRenderDescriptor) null;
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].BreedIsComplete(UID, player, ref WasNewVariant, out animalBorn))
          return true;
      }
      return false;
    }

    public void MoveCRISPRBuilding(Vector2Int OldLocation, Vector2Int NewLocation)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].Location.CompareMatches(OldLocation))
        {
          this.CRISPRBuildings[index].Location = new Vector2Int(NewLocation);
          break;
        }
      }
    }

    public void SoldCRIPSRBuilding(int UID)
    {
      for (int index = this.CRISPRBuildings.Count - 1; index > -1; --index)
      {
        if (this.CRISPRBuildings[index].BuildingUID == UID)
        {
          this.CRISPRBuildings.RemoveAt(index);
          break;
        }
      }
    }

    public void AddBreedToCRISPRBuilding(
      int buildingUID,
      AnimalType animalOne,
      AnimalType animalTwo,
      int slotIndex)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].BuildingUID == buildingUID)
        {
          CrisprActiveBreed breed = new CrisprActiveBreed(animalOne, animalTwo, this.BreedUIDToSpawn);
          ++this.BreedUIDToSpawn;
          this.CRISPRBuildings[index].AddGenomePair(breed, slotIndex);
        }
      }
    }

    public void RemoveBreedFromCRISPRBuilding(int buildingUID, CrisprActiveBreed breed)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
      {
        if (this.CRISPRBuildings[index].BuildingUID == buildingUID)
        {
          this.CRISPRBuildings[index].RemoveGenomePair(breed);
          break;
        }
      }
    }

    public void ReleaseBreedToPen(CrisprActiveBreed breed, Player player, int BuildingUID)
    {
      IntakePerson intakePerson = new IntakePerson(breed.resultBody, _IsAGirl: (!breed.isBoy), Variant: breed.resultBodyVariant, _HeadType: breed.resultHead, _HeadVariant: breed.resultHeadVariant);
      player.livestats.AnimalsJustTraded = new WaveInfo(new IntakeInfo());
      player.livestats.AnimalsJustTraded.People.Add(intakePerson);
      FeatureFlags.NewAnimalGot = true;
      Z_NotificationManager.RemoveThis(Z_NotificationType.A_CRISPR_HybridBirth, breed.UID);
      this.RemoveBreedFromCRISPRBuilding(BuildingUID, breed);
    }

    public void StartNewDay(Player player)
    {
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
        this.CRISPRBuildings[index].StartNewDay(player);
    }

    public void UpdateCRIPSRBreeds(float DeltaTime)
    {
    }

    public CRISPR_Breed(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("c", ref this.BuildingUIDSPawn);
      int _out = 0;
      this.CRISPRBuildings = new List<CRISPRBuilding>();
      int num2 = (int) reader.ReadInt("c", ref _out);
      for (int index = 0; index < _out; ++index)
        this.CRISPRBuildings.Add(new CRISPRBuilding(reader, VersionForLoad));
      int num3 = (int) reader.ReadInt("c", ref this.BreedUIDToSpawn);
    }

    public void SaveCRISPR_Breeds(Writer writer)
    {
      writer.WriteInt("c", this.BuildingUIDSPawn);
      writer.WriteInt("c", this.CRISPRBuildings.Count);
      for (int index = 0; index < this.CRISPRBuildings.Count; ++index)
        this.CRISPRBuildings[index].SaveCRISPRBuilding(writer);
      writer.WriteInt("c", this.BreedUIDToSpawn);
    }
  }
}
