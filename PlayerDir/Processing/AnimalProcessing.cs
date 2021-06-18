// Decompiled with JetBrains decompiler
// Type: TinyZoo.PlayerDir.Processing.AnimalProcessing
// Assembly: LetsBuildAZoo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DEB7467C-E857-4369-A6B7-78A1E0037E5E
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Let's Build a Zoo Demo\LetsBuildAZoo.exe

using SEngine;
using System;
using System.Collections.Generic;

namespace TinyZoo.PlayerDir.Processing
{
  internal class AnimalProcessing
  {
    public List<ProcessingBuilding> Buildings;

    public AnimalProcessing() => this.Buildings = new List<ProcessingBuilding>();

    public ProcessingBuilding GetBuildingByUID(int ShopUID, out int ArrayIndex)
    {
      for (int index = this.Buildings.Count - 1; index > -1; --index)
      {
        if (this.Buildings[index].UID == ShopUID)
        {
          ArrayIndex = index;
          return this.Buildings[index];
        }
      }
      throw new Exception("This Building DOES NOT Exist! There was some horror show stuff back in SellUIManager -  Z_ProcessingManager.SelectedBuildingUID = player.shopstatus.GetThisFacility(Pen_SelectedPenManager.SelectedTileLocation).ShopUID;");
    }

    public void SoldAProcessingBuilding(int ShopUID, Player player)
    {
      for (int index = this.Buildings.Count - 1; index > -1; --index)
      {
        if (this.Buildings[index].UID == ShopUID)
        {
          this.Buildings[index].SoldThisBuilding(player);
          this.Buildings.RemoveAt(index);
          return;
        }
      }
      throw new Exception("No Building To Remove");
    }

    public void MoveBuilding_NotNeededUnlessWeAdLocation(
      Vector2Int OldLocation,
      Vector2Int NewLocation)
    {
    }

    public void AddMeatProcessorBuilding(int ShopUID) => this.Buildings.Add(new ProcessingBuilding(ShopUID));

    public AnimalProcessing(Reader reader, int VersionForLoad)
    {
      this.Buildings = new List<ProcessingBuilding>();
      int _out = 0;
      int num = (int) reader.ReadInt("a", ref _out);
      for (int index = 0; index < _out; ++index)
        this.Buildings.Add(new ProcessingBuilding(reader, VersionForLoad));
    }

    public void SaveAnimalProcessing(Writer writer)
    {
      writer.WriteInt("a", this.Buildings.Count);
      for (int index = 0; index < this.Buildings.Count; ++index)
        this.Buildings[index].SaveProcessingBuilding(writer);
    }
  }
}
