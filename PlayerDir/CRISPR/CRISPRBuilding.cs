// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.CRISPR.CRISPRBuilding
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using TinyZoo.OverWorld.OverWorldEnv.WallsAndFloors;
using TinyZoo.Z_Collection.Shared.Grid;
using TinyZoo.Z_OverWorld._OverWorldEnv.WallsAndFloors.Components;

namespace TinyZoo.PlayerDir.CRISPR
{
  internal class CRISPRBuilding
  {
    public Vector2Int Location;
    public int BuildingUID;
    public CrisprActiveBreed[] crisprBreeds;

    public CRISPRBuilding(Vector2Int _Location, int _UID)
    {
      this.Location = new Vector2Int(_Location);
      this.BuildingUID = _UID;
      this.crisprBreeds = new CrisprActiveBreed[4];
    }

    public void AddGenomePair(CrisprActiveBreed breed, int slotIndex)
    {
      this.crisprBreeds[slotIndex] = breed;
      this.SetBuildingState_IsMakingBaby(true);
    }

    public bool HasThisBreed(int UID)
    {
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        if (this.crisprBreeds[index] != null && this.crisprBreeds[index].UID == UID)
          return true;
      }
      return false;
    }

    public bool BreedIsComplete(
      int UID,
      Player player,
      ref bool WasNewVariant,
      out AnimalRenderDescriptor animalBorn)
    {
      animalBorn = (AnimalRenderDescriptor) null;
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        if (this.crisprBreeds[index] != null && this.crisprBreeds[index].UID == UID)
        {
          if (!this.crisprBreeds[index].IsBorn_CanCollect)
          {
            animalBorn = this.crisprBreeds[index].DoBirth(player, ref WasNewVariant);
            this.SetBuildingState_IsBabyReady(true);
          }
          return true;
        }
      }
      return false;
    }

    private void SetBuildingState_IsMakingBaby(bool IsMakingBaby) => this.GetDNASplicerComponent().TurnOn(IsMakingBaby);

    private void SetBuildingState_IsBabyReady(bool IsBabyReady) => this.GetDNASplicerComponent().SetHasBabyReady(IsBabyReady);

    private DNASplicerComponent GetDNASplicerComponent()
    {
      TileRenderer tileRenderer = OverWorldManager.overworldenvironment.wallsandfloors.tilesasarray[this.Location.X, this.Location.Y];
      if (tileRenderer != null && tileRenderer.rendercomponent != null)
      {
        for (int index = 0; index < tileRenderer.rendercomponent.Count; ++index)
        {
          if (tileRenderer.rendercomponent[index].componenttype == RenderComponentType.DNASplicer)
            return tileRenderer.rendercomponent[index] as DNASplicerComponent;
        }
      }
      return (DNASplicerComponent) null;
    }

    private void ScrubSlotsToSetBuildingState()
    {
      bool IsMakingBaby = false;
      bool IsBabyReady = false;
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        if (this.crisprBreeds[index] != null)
        {
          IsMakingBaby = true;
          if (this.crisprBreeds[index].IsBorn_CanCollect)
            IsBabyReady = true;
        }
      }
      this.SetBuildingState_IsMakingBaby(IsMakingBaby);
      this.SetBuildingState_IsBabyReady(IsBabyReady);
    }

    private CrisprActiveBreed GetGenomePairBreed(int UID, out int slotIndex)
    {
      slotIndex = -1;
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        if (this.crisprBreeds[index] != null && this.crisprBreeds[index].UID == UID)
        {
          slotIndex = index;
          return this.crisprBreeds[index];
        }
      }
      return (CrisprActiveBreed) null;
    }

    public void RemoveGenomePair(CrisprActiveBreed breed)
    {
      int slotIndex;
      if (this.GetGenomePairBreed(breed.UID, out slotIndex) == null)
        return;
      this.crisprBreeds[slotIndex] = (CrisprActiveBreed) null;
      this.ScrubSlotsToSetBuildingState();
    }

    public void StartNewDay(Player player)
    {
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        if (this.crisprBreeds[index] != null)
          this.crisprBreeds[index].StartNewDay(player);
      }
      this.ScrubSlotsToSetBuildingState();
    }

    public CRISPRBuilding(Reader reader, int VersionForLoad)
    {
      int num1 = (int) reader.ReadInt("c", ref this.BuildingUID);
      this.Location = new Vector2Int(reader);
      int _out1 = 0;
      int num2 = (int) reader.ReadInt("c", ref _out1);
      this.crisprBreeds = new CrisprActiveBreed[4];
      if (VersionForLoad < 38)
      {
        for (int index = 0; index < _out1; ++index)
        {
          CrisprActiveBreed crisprActiveBreed = new CrisprActiveBreed(reader);
          this.crisprBreeds[index] = crisprActiveBreed;
        }
      }
      else
      {
        for (int index = 0; index < _out1; ++index)
        {
          bool _out2 = false;
          int num3 = (int) reader.ReadBool("c", ref _out2);
          if (!_out2)
            this.crisprBreeds[index] = new CrisprActiveBreed(reader);
        }
      }
    }

    public void SaveCRISPRBuilding(Writer writer)
    {
      writer.WriteInt("c", this.BuildingUID);
      this.Location.SaveVector2Int(writer);
      writer.WriteInt("c", this.crisprBreeds.Length);
      for (int index = 0; index < this.crisprBreeds.Length; ++index)
      {
        writer.WriteBool("c", this.crisprBreeds[index] == null);
        if (this.crisprBreeds[index] != null)
          this.crisprBreeds[index].SaveCrisprActiveBreed(writer);
      }
    }
  }
}
